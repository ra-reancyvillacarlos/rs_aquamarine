using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class rGuestBilling : Form
    {
        DataView v_glist;
        public String rom_code;
        public String rom_rate = "";
        
        public rGuestBilling()
        {
            InitializeComponent();
        }

        private void rGuestBilling_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            GlobalMethod gm = new GlobalMethod();
            v_glist = new DataView();

            gm.load_company(cbo_srch_company);
            cbo_disfolio.SelectedIndex = 0;
            cbo_balances.SelectedIndex = 0;
            

            dis_dgvguest("");
        }

        private void clr_frm()
        {
            int length = 0;

            try
            {
                lbl_gfolio.Text = "";
                lbl_arrdate.Text = "";
                lbl_depdate.Text = "";
                lbl_company.Text = "";
                lbl_username.Text = "";
                lbl_rm.Text = "";
                lbl_grossrate.Text = "";
                lbl_noofnights.Text = "";
                lbl_pax.Text = "";

                rtxt_remarks.Text = "";
                rtxt_gname.Text = "";

                length = dgv_gfolio.Rows.Count;

                dgv_gfolio.Rows.Clear();
            }
            catch (Exception er) {}
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (lbl_gfolio.Text != "")
            {
                //add_charge chg = new add_charge(this);
                //chg.MdiParent = this.MdiParent;
                //chg.Show();

                //chg.set_data(lbl_gfolio.Text, rtxt_gname.Text, lbl_rm.Text, lbl_rate.Text, lbl_rmtype.Text, rtxt_remarks.Text, lbl_arrdate.Text, lbl_depdate.Text, "", "", "", "", "", "", "", "", lbl_deposit.Text, lbl_balance.Text, true);                
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (dgv_gfolio.SelectedRows.Count > 0)
            {
                thisDatabase db = new thisDatabase();
                //add_charge chg = new add_charge(this);
                String trnxdt = dgv_gfolio.CurrentRow.Cells[0].Value.ToString();
                String chg_code= dgv_gfolio.CurrentRow.Cells[1].Value.ToString();
                String doctyp = dgv_gfolio.CurrentRow.Cells[3].Value.ToString(); //dgv_gfolio.CurrentRow.Cells[3].Value.ToString();
                String refer = dgv_gfolio.CurrentRow.Cells[4].Value.ToString();
                String amt = dgv_gfolio.CurrentRow.Cells[5].Value.ToString();
                String resnum = dgv_gfolio.CurrentRow.Cells[0].Value.ToString();
                String res_gname = dgv_gfolio.CurrentRow.Cells[0].Value.ToString();
                String rm = lbl_rm.Text;
                String chg_num = dgv_gfolio.CurrentRow.Cells[13].Value.ToString();

                if (Convert.ToDateTime(trnxdt).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) >= 0)
                {
                    //chg.MdiParent = this.MdiParent;
                    //chg.Show();
                    //chg.set_data(lbl_gfolio.Text, rtxt_gname.Text, rm, lbl_rate.Text, lbl_rmtype.Text, rtxt_remarks.Text, lbl_arrdate.Text, lbl_depdate.Text, trnxdt, chg_code, chg_num, doctyp, refer, amt, resnum, res_gname, lbl_deposit.Text, lbl_balance.Text, false);                    
                }
                else
                {
                    MessageBox.Show("Previous Transaction of Charge cannot be modified.");
                }
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }            
        }

        private void btn_baltransfer_Click(object sender, EventArgs e)
        {
            String cur_bal = "0.00";
            String all_bal = lbl_balance.Text;
            transfer_balance tb = new transfer_balance();

            if (lbl_gfolio.Text != "")
            {
                tb.MdiParent = this.MdiParent;
                tb.Show();
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }

            
            tb.set_data(lbl_gfolio.Text, rtxt_gname.Text, lbl_rm.Text, cur_bal, all_bal);
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (lbl_gfolio.Text != "")
            {
                thisDatabase db = new thisDatabase();
                Report rpt = new Report("", "");
                double val = (Convert.ToDateTime(lbl_depdate.Text) - Convert.ToDateTime(lbl_arrdate.Text)).TotalDays;
                String noofnight = val.ToString();

                if (val == 0)
                {
                    noofnight = "1";
                }
                rpt.Show();
                rpt.printprev_gfolio(lbl_gfolio.Text, lbl_rm.Text, rtxt_gname.Text, lbl_rate.Text, lbl_pax.Text, lbl_rmtype.Text, lbl_address.Text + " | " + lbl_company.Text, lbl_arrdate.Text, lbl_arrtime.Text, lbl_depdate.Text, lbl_deptime.Text, noofnight, db.get_folio_totalcharges(lbl_gfolio.Text).ToString(), db.get_folio_totalpayment(lbl_gfolio.Text).ToString(), lbl_disc.Text, lbl_grossrate.Text);
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }
        }

        private void txt_romsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                v_glist.RowFilter = "room like '%" + txt_romsearch.Text.Trim().Replace("'", "''") + "%'";
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String cond = "";

            if (txt_srch_gname.Text != "")
            {
                cond = " AND gf.full_name ILIKE '%" + txt_srch_gname.Text + "%'";
            }
            if (cbo_srch_company.SelectedIndex > -1)
            {
                cond = cond + " AND c.comp_code='" + cbo_srch_company.SelectedValue.ToString() + "'";
            }
            if (cbo_disfolio.Text == "In-house")
            {

            }
            if (cbo_disfolio.Text == "Expected Departures")
            {
                cond = cond + " AND gf.dep_date='" + db.get_systemdate("") + "'";
            }
            if (cbo_disfolio.Text == "Arrivals Today")
            {
                cond = cond + " AND gf.arr_date='" + db.get_systemdate("") + "'";
            }
            
            if (cbo_balances.Text == "Zero Balances")
            {
                cond = cond + " AND (SELECT SUM(cf1.amount) FROM "+db.get_schema()+".chgfil cf1 WHERE cf1.reg_num=gf.reg_num)=0";
            }
            if (cbo_balances.Text == "Open Balances")
            {
                cond = cond + " AND (SELECT SUM(cf1.amount) FROM " + db.get_schema() + ".chgfil cf1 WHERE cf1.reg_num=gf.reg_num)!=0";
            }

            dis_dgvguest(cond);
            
        }

        private void dis_dgvguest(String cond)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
           
            try
            {
                DateTime curdate = Convert.ToDateTime(db.get_systemdate(""));

                dt = db.get_guest_currentlycheckin(cond);

                dgv_guestlist.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv_guestlist.Rows[0].Clone();

                    row.Cells[0].Value = dt.Rows[i][0].ToString();
                    row.Cells[1].Value = dt.Rows[i][1].ToString();
                    row.Cells[2].Value = dt.Rows[i][2].ToString();
                    row.Cells[3].Value = Convert.ToDateTime(dt.Rows[i][3].ToString()).ToString("MM/dd/yyyy");
                    row.Cells[4].Value = Convert.ToDateTime(dt.Rows[i][4].ToString()).ToString("MM/dd/yyyy");
                    row.Cells[5].Value = dt.Rows[i][5].ToString();
                    row.Cells[6].Value = dt.Rows[i][6].ToString();
                    row.Cells[7].Value = Convert.ToDateTime(dt.Rows[i][7].ToString()).ToString("MM/dd/yyyy");
                    row.Cells[8].Value = Convert.ToDateTime(dt.Rows[i][8].ToString()).ToString("HH:mm");

                    if (curdate >= Convert.ToDateTime(dt.Rows[i][4].ToString()))
                    {
                        //no color if empty block by column
                        row.Cells[0].Style.ForeColor = Color.Red;
                        row.Cells[1].Style.ForeColor = Color.Red;
                        row.Cells[2].Style.ForeColor = Color.Red;
                        row.Cells[3].Style.ForeColor = Color.Red;
                        row.Cells[4].Style.ForeColor = Color.Red;
                        row.Cells[5].Style.ForeColor = Color.Red;
                        row.Cells[6].Style.ForeColor = Color.Red;
                        row.Cells[7].Style.ForeColor = Color.Red;
                        row.Cells[8].Style.ForeColor = Color.Red;
                        dgv_guestlist.Rows.Add(row);
                    }
                    else
                    {
                        row.Cells[0].Style.ForeColor = Color.Black;
                        row.Cells[1].Style.ForeColor = Color.Black;
                        row.Cells[2].Style.ForeColor = Color.Black;
                        row.Cells[3].Style.ForeColor = Color.Black;
                        row.Cells[4].Style.ForeColor = Color.Black;
                        row.Cells[5].Style.ForeColor = Color.Black;
                        row.Cells[6].Style.ForeColor = Color.Black;
                        row.Cells[7].Style.ForeColor = Color.Black;
                        row.Cells[8].Style.ForeColor = Color.Black;
                        dgv_guestlist.Rows.Add(row);
                        //color if block by column is not empty
                        //dgv_reslist.Rows[i] = (DataRowCollection)dt.Rows[i];                      
                    }
                }
            }
            catch (Exception) { }
        }        

        private void disp_gfolio()
        {
            thisDatabase db = new thisDatabase();
        }

        private void dgv_guestlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String reg_num = dgv_guestlist[5, e.RowIndex].Value.ToString();

                clr_frm();

                disp_guestinfo(reg_num);
                disp_chgfil(reg_num);

                rom_code = lbl_rm.Text;
                rom_rate = lbl_grossrate.Text;
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void disp_guestinfo(String reg_num)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            DataTable dt = new DataTable();
            Double db_amt = 0.00;
            String rmrttyp = "D";
            DateTime curdate = Convert.ToDateTime(db.get_systemdate(""));
            dt = db.get_guest_curchkin_selected(reg_num);

            foreach (DataRow row in dt.Rows)
            {
                lbl_gfolio.Text = row["reg_num"].ToString();
                lbl_arrdate.Text = Convert.ToDateTime(row["arr_date"].ToString()).ToString("MM/dd/yyyy");
                lbl_depdate.Text = Convert.ToDateTime(row["dep_date"].ToString()).ToString("MM/dd/yyyy");
                lbl_company.Text = "";
                lbl_username.Text = row["user_id"].ToString() + " " + Convert.ToDateTime(row["t_date"].ToString()).ToString("MM/dd/yyyy") + " " + row["t_time"].ToString();
                lbl_rm.Text = row["rom_code"].ToString();
                lbl_grossrate.Text = Convert.ToDouble(db.get_roomrateamt(row["rate_code"].ToString(), row["typ_code"].ToString(), int.Parse(row["occ_type"].ToString())).ToString()).ToString("0.00");
                lbl_rate.Text = (gm.toNormalDoubleFormat(row["rom_rate"].ToString()) + gm.toNormalDoubleFormat(row["govt_tax"].ToString()) + gm.toNormalDoubleFormat(row["serv_chg"].ToString())).ToString("0.00");
                lbl_noofnights.Text = "";
                lbl_rmtype.Text = row["typ_code"].ToString();
                lbl_pax.Text = row["occ_type"].ToString();
                lbl_disc.Text = row["disc_pct"].ToString();
                rtxt_remarks.Text = row["remarks"].ToString() + " " + row["bill_info"].ToString() + " " + row["rm_features"].ToString();
                rtxt_gname.Text = row["full_name"].ToString();
                lbl_address.Text = row["address"].ToString();
                lbl_company.Text = row["company"].ToString();

                rmrttyp = row["rmrttyp"].ToString();

                if (rmrttyp == "M")
                {
                    lbl_rmrttyp.Text = "Monthly";
                }
                else
                {
                    lbl_rmrttyp.Text = "Daily";
                }

                if (curdate >= Convert.ToDateTime(row["dep_date"].ToString()))
                {
                    //lbl_depdate.Font = new 
                }
            }
        }

        public void disp_chgfil(String reg_num)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String chg_code = "", chg_amt = "0.00", chg_amt_no_deposit = "0.00";
            Double bal_amnt = 0.00, dep_amnt=0.00;
            Double runbal = 0.00;
            DataTable dt = new DataTable();
            dt = db.get_guest_chargefil(reg_num, false);

            dgv_gfolio.Rows.Clear();

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataGridViewRow dgv_row = (DataGridViewRow)dgv_gfolio.Rows[0].Clone();

                    chg_code = row[1].ToString();
                    chg_amt = row[5].ToString();
                    
                    dgv_row.Cells[0].Value = Convert.ToDateTime(row[0].ToString()).ToString("MM/dd/yy");
                    dgv_row.Cells[1].Value = chg_code; //charge code
                    dgv_row.Cells[2].Value = row[2].ToString();
                    dgv_row.Cells[3].Value = row[3].ToString();
                    dgv_row.Cells[4].Value = row[4].ToString();
                    dgv_row.Cells[5].Value = gm.toAccountingFormat(Convert.ToDouble(chg_amt));

                    dgv_row.Cells[7].Value = row[6].ToString();
                    dgv_row.Cells[8].Value = row[7].ToString();
                    dgv_row.Cells[9].Value = row[8].ToString();
                    dgv_row.Cells[10].Value = row[9].ToString();
                    dgv_row.Cells[11].Value = row[10].ToString();
                    dgv_row.Cells[12].Value = row[11].ToString();
                    dgv_row.Cells[13].Value = row[12].ToString();
                    
                    if (db.is_chg_deposit(chg_code))
                    {
                        //Color Green for deposit
                        dgv_row.Cells[0].Style.ForeColor = Color.Green;
                        dgv_row.Cells[1].Style.ForeColor = Color.Green;
                        dgv_row.Cells[2].Style.ForeColor = Color.Green;
                        dgv_row.Cells[3].Style.ForeColor = Color.Green;
                        dgv_row.Cells[4].Style.ForeColor = Color.Green;
                        dgv_row.Cells[5].Style.ForeColor = Color.Green;
                        dgv_row.Cells[6].Style.ForeColor = Color.Green;
                        dgv_row.Cells[7].Style.ForeColor = Color.Green;
                        dgv_row.Cells[8].Style.ForeColor = Color.Green;
                        dgv_row.Cells[9].Style.ForeColor = Color.Green;
                        dgv_row.Cells[10].Style.ForeColor = Color.Green;
                        dgv_row.Cells[11].Style.ForeColor = Color.Green;
                        dgv_row.Cells[12].Style.ForeColor = Color.Green;
                        dgv_row.Cells[13].Style.ForeColor = Color.Green;
                    }
                    else
                    {
                        chg_amt_no_deposit = chg_amt;

                        runbal = runbal + (gm.toNormalDoubleFormat(chg_amt_no_deposit));

                        dgv_row.Cells[6].Value = gm.toAccountingFormat(runbal);

                        //Color Green for deposit
                        dgv_row.Cells[0].Style.ForeColor = Color.Black;
                        dgv_row.Cells[1].Style.ForeColor = Color.Black;
                        dgv_row.Cells[2].Style.ForeColor = Color.Black;
                        dgv_row.Cells[3].Style.ForeColor = Color.Black;
                        dgv_row.Cells[4].Style.ForeColor = Color.Black;
                        dgv_row.Cells[5].Style.ForeColor = Color.Black;
                        dgv_row.Cells[6].Style.ForeColor = Color.Black;
                        dgv_row.Cells[7].Style.ForeColor = Color.Black;
                        dgv_row.Cells[8].Style.ForeColor = Color.Black;
                        dgv_row.Cells[9].Style.ForeColor = Color.Black;
                        dgv_row.Cells[10].Style.ForeColor = Color.Black;
                        dgv_row.Cells[11].Style.ForeColor = Color.Black;
                        dgv_row.Cells[12].Style.ForeColor = Color.Black;
                        dgv_row.Cells[13].Style.ForeColor = Color.Black;
                    }

                    dgv_gfolio.Rows.Add(dgv_row);
                }
            }
            catch (Exception) { }

            bal_amnt = db.get_guest_charges_total(reg_num, false);
            dep_amnt = db.get_guest_charge_deposit(reg_num);

            if (bal_amnt > 0)
            {
                lbl_balance.Text = gm.toAccountingFormat(bal_amnt);
                lbl_deposit.Text = "0.00";
                lbl_deposit.Font = new Font(FontFamily.GenericSansSerif, (float)12.0, FontStyle.Regular);
                lbl_balance.Font = new Font(FontFamily.GenericSansSerif, (float)12.0, FontStyle.Bold);
            }
            else
            {
                lbl_balance.Text = "0.00";
                lbl_deposit.Text = gm.toAccountingFormat(Math.Abs(bal_amnt));
                lbl_deposit.Font = new Font(FontFamily.GenericSansSerif, (float)12.0, FontStyle.Bold);
                lbl_balance.Font = new Font(FontFamily.GenericSansSerif, (float)12.0, FontStyle.Regular);
            }

            btn_deposit.Text = gm.toAccountingFormat(Math.Abs(dep_amnt));
        }

        private void btn_chkout_Click(object sender, EventArgs e)
        {
            Double bal = Convert.ToDouble(lbl_balance.Text);
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            String reg_num = lbl_gfolio.Text;
            String chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, fol_num, chg_date,jrnlz, fol_name, food, misc, fcontract, res_code, tofr_fol, soa_code, doc_type, vat_amnt, sc_amnt;
            String acct_no, full_name, reg_date, arr_date, arr_time, dep_date, dep_time, typ_code, occ_type, rate_code, rom_rate, govt_tax, serv_chg, extra_bed, pay_code, mkt_code, src_code, trv_code, free_bfast, rm_features, bill_info, remarks, rom_class, rgrp_code, rgrp_ln, cancel, canc_reason, canc_user, canc_date, canc_time, grp_code, co_user, co_date, co_time, out_fol, fctr_code, disc_code, disc_pct, or_amnt, trace_no, ccrd_no;
            Boolean dt_result = false, dt2_result = false;

            String val2 = "";

            if (lbl_gfolio.Text == "")
            {
                MessageBox.Show("Pls select folio.");
            }
            else
            {
                if (bal != 0)
                {
                    MessageBox.Show("Balance must be equal to ZERO.");
                }
                else if (Convert.ToDouble(lbl_deposit.Text) != 0)
                {
                    MessageBox.Show("Deposit must be equal to ZERO.");
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to check out this guest?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        dt = db.QueryOnTableWithParams("chgfil", "*", "reg_num='" + reg_num + "'", "");

                        foreach (DataRow row in dt.Rows)
                        {
                            chg_code = row["chg_code"].ToString();
                            chg_num = row["chg_num"].ToString();
                            rom_code = row["rom_code"].ToString();
                            reference = row["reference"].ToString();
                            amount = row["amount"].ToString();
                            user_id = row["user_id"].ToString();
                            t_date = Convert.ToDateTime(row["t_date"].ToString()).ToString("yyyy-MM-dd");
                            t_time = row["t_time"].ToString();
                            fol_num = row["fol_num"].ToString();
                            chg_date = Convert.ToDateTime(row["chg_date"].ToString()).ToString("yyyy-MM-dd");
                            jrnlz = row["jrnlz"].ToString();
                            fol_name = row["fol_name"].ToString();
                            food = row["food"].ToString();
                            misc = row["misc"].ToString();
                            fcontract = row["fcontract"].ToString();
                            res_code = row["res_code"].ToString();
                            tofr_fol = row["tofr_fol"].ToString();
                            soa_code = row["soa_code"].ToString();
                            doc_type = row["doc_type"].ToString();
                            vat_amnt = row["vat_amnt"].ToString();
                            sc_amnt = row["sc_amnt"].ToString();

                            trace_no = row["trace_no"].ToString();
                            ccrd_no = row["ccrd_no"].ToString();

                            if (food == "")
                            {
                                food = "0.00";
                            }
                            if (misc == "")
                            {
                                misc = "0.00";
                            }
                            if (vat_amnt == "")
                            {
                                vat_amnt = "0.00";
                            }
                            if (sc_amnt == "")
                            {
                                sc_amnt = "0.00";
                            }

                            dt_result = db.InsertOnTable("chghist", "reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, fol_num, chg_date, jrnlz, fol_name, food, misc, fcontract, res_code, tofr_fol, soa_code, doc_type, vat_amnt, sc_amnt, trace_no, ccrd_no", "'" + reg_num + "', '" + chg_code + "', '" + chg_num + "', '" + rom_code + "', '" + reference + "', '" + amount + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + fol_num + "', '" + chg_date + "', '" + jrnlz + "', '" + fol_name + "', '" + food + "', '" + misc + "', '" + fcontract + "', '" + res_code + "', '" + tofr_fol + "', '" + soa_code + "', '" + doc_type + "', '" + vat_amnt + "', '" + sc_amnt + "', '" + trace_no + "', '" + ccrd_no + "'");
                        }
                        //gfhist
                        dt2 = db.QueryOnTableWithParams("gfolio", "*", "reg_num='" + reg_num + "'", "");

                        foreach (DataRow row2 in dt2.Rows)
                        {
                            reg_num = row2["reg_num"].ToString();
                            res_code = row2["res_code"].ToString();
                            acct_no = row2["acct_no"].ToString();
                            full_name = row2["full_name"].ToString();
                            reg_date = row2["reg_date"].ToString();
                            arr_date = row2["arr_date"].ToString();
                            arr_time = row2["arr_time"].ToString();
                            dep_date = row2["dep_date"].ToString();
                            dep_time = row2["dep_time"].ToString();
                            rom_code = row2["rom_code"].ToString();
                            typ_code = row2["typ_code"].ToString();
                            occ_type = row2["occ_type"].ToString();
                            rate_code = row2["rate_code"].ToString();
                            rom_rate = row2["rom_rate"].ToString();
                            govt_tax = row2["govt_tax"].ToString();
                            serv_chg = row2["serv_chg"].ToString();
                            extra_bed = row2["extra_bed"].ToString();
                            pay_code = row2["pay_code"].ToString();
                            mkt_code = row2["mkt_code"].ToString();
                            src_code = row2["src_code"].ToString();
                            trv_code = row2["trv_code"].ToString();
                            free_bfast = row2["free_bfast"].ToString();
                            rm_features = row2["rm_features"].ToString();
                            bill_info = row2["bill_info"].ToString();
                            remarks = row2["remarks"].ToString();
                            rom_class = row2["rom_class"].ToString();
                            rgrp_code = row2["rgrp_code"].ToString();
                            rgrp_ln = row2["rgrp_ln"].ToString();
                            user_id = row2["user_id"].ToString();
                            t_date = row2["t_date"].ToString();
                            t_time = row2["t_time"].ToString();
                            cancel = row2["cancel"].ToString();
                            canc_reason = row2["canc_reason"].ToString();
                            canc_user = row2["canc_user"].ToString();
                            canc_date = row2["canc_date"].ToString();
                            canc_time = row2["canc_time"].ToString();
                            grp_code = row2["grp_code"].ToString();
                            co_user = GlobalClass.username;
                            co_date = db.get_systemdate("");
                            co_time = DateTime.Now.ToString("HH:mm");
                            out_fol = row2["out_fol"].ToString();
                            fctr_code = row2["fctr_code"].ToString();
                            disc_code = row2["disc_code"].ToString();
                            disc_pct = row2["disc_pct"].ToString();

                            if (reg_date != "")
                            {
                                reg_date = Convert.ToDateTime(reg_date).ToString("yyyy-MM-dd");
                            }
                            if (arr_date != "")
                            {
                                arr_date = Convert.ToDateTime(arr_date).ToString("yyyy-MM-dd");
                            }
                            if (dep_date != "")
                            {
                                dep_date = Convert.ToDateTime(dep_date).ToString("yyyy-MM-dd");
                            }
                            if (t_date != "")
                            {
                                t_date = Convert.ToDateTime(t_date).ToString("yyyy-MM-dd");
                            }
                            if (canc_date != "")
                            {
                                canc_date = Convert.ToDateTime(canc_date).ToString("yyyy-MM-dd");
                            }
                            if (canc_date == "")
                            {
                                canc_date = "1899-12-30";
                            }
                            if (rom_rate == "")
                            {
                                rom_rate = "0.00";
                            }
                            if (govt_tax == "")
                            {
                                govt_tax = "0.00";
                            }
                            if (serv_chg == "")
                            {
                                serv_chg = "0.00";
                            }
                            if (extra_bed == "")
                            {
                                extra_bed = "0.00";
                            }
                            if (free_bfast == "")
                            {
                                free_bfast = "0.00";
                            }
                            if (rgrp_ln == "")
                            {
                                rgrp_ln = "0";
                            }
                            if (disc_pct == "")
                            {
                                disc_pct = "0.00";
                            }

                            val2 = "'" + reg_num + "', '" + res_code + "', '" + acct_no + "', '" + full_name + "', '" + reg_date + "', '" + arr_date + "', '" + arr_time + "', '" + dep_date + "', '" + dep_time + "', '" + rom_code + "', '" + typ_code + "', '" + occ_type + "', '" + rate_code + "', '" + rom_rate + "', '" + govt_tax + "', '" + serv_chg + "', '" + extra_bed + "', '" + pay_code + "', '" + mkt_code + "', '" + src_code + "', '" + trv_code + "', '" + free_bfast + "', '" + rm_features + "', '" + bill_info + "', '" + remarks + "', '" + rom_class + "', '" + rgrp_code + "', '" + rgrp_ln + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + cancel + "', '" + canc_reason + "', '" + canc_user + "', '" + canc_date + "', '" + canc_time + "', '" + grp_code + "', '" + co_user + "', '" + co_date + "', '" + co_time + "', '" + out_fol + "', '" + fctr_code + "', '" + disc_code + "', '" + disc_pct + "'";

                            dt2_result = db.InsertOnTable("gfhist", "reg_num, res_code, acct_no, full_name, reg_date, arr_date, arr_time, dep_date, dep_time, rom_code, typ_code, occ_type, rate_code, rom_rate, govt_tax, serv_chg, extra_bed, pay_code, mkt_code, src_code, trv_code, free_bfast, rm_features, bill_info, remarks, rom_class, rgrp_code, rgrp_ln, user_id, t_date, t_time, cancel, canc_reason, canc_user, canc_date, canc_time, grp_code, co_user, co_date, co_time, out_fol, fctr_code, disc_code, disc_pct", val2);
                        }

                        dt.Rows.Clear();
                        dt2.Rows.Clear();
                        //remove chgfil
                        if (dt_result)
                            db.DeleteOnTable("chgfil", "reg_num='" + lbl_gfolio.Text + "'");
                        if (dt2_result)
                            db.DeleteOnTable("gfolio", "reg_num='" + lbl_gfolio.Text + "'");

                        UpdateRoomStatus urs = new UpdateRoomStatus();

                        urs.set_rmstatus("VD", lbl_rm.Text);

                        dis_dgvguest("");
                        clr_frm();
                    }
                }
            }
        }

        private void btn_or_Click(object sender, EventArgs e)
        {
            if (lbl_gfolio.Text != "")
            {
                or_entry or = new or_entry();
                or.MdiParent = this.MdiParent;
                or.setdata(lbl_gfolio.Text, rtxt_gname.Text, lbl_rm.Text);
                or.Show();
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }
        }

        private void dgv_gfolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    String reg_num = lbl_gfolio.Text;

                    del_charge(reg_num);

                    clr_frm();

                    disp_guestinfo(reg_num);
                    disp_chgfil(reg_num);

                    rom_code = lbl_rm.Text;
                    rom_rate = lbl_grossrate.Text;
                }
                catch (Exception er) { MessageBox.Show(er.Message); }

                
            }
        }

        //only admin can access this functionality
        private void del_charge(String gfoliono)
        {
            String dttime="", chgcode="", chgdesc="", chgref="", chgamt="", chgno = "";
            int row = 0;
            thisDatabase db = new thisDatabase();

            if (GlobalClass.username.ToUpper() == "ADMIN" && dgv_gfolio.Rows.Count > 1)
            {
                row = dgv_gfolio.CurrentRow.Index;

                dttime = dgv_gfolio[0, row].Value.ToString();
                chgcode = dgv_gfolio[1, row].Value.ToString();
                chgdesc = dgv_gfolio[2, row].Value.ToString();
                chgref = dgv_gfolio[4, row].Value.ToString();
                chgamt = dgv_gfolio[5, row].Value.ToString();
                chgno = dgv_gfolio["chg_no", row].Value.ToString();
                
                if (MessageBox.Show("Are you sure you want to DELETE this line of Guest Folio No. "+gfoliono+"?\n"+dttime + " | " + chgcode + " | " + chgdesc + " | " + chgref + " | " + chgamt + " | " + chgno, "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (db.DeleteOnTable("chgfil", "reg_num='" + gfoliono + "' AND chg_code='" + chgcode + "' AND chg_num='" + chgno + "'"))
                    {
                        MessageBox.Show("Successfully deleted.");
                    }
                }
            }
        }

        private void btn_deposit_Click(object sender, EventArgs e)
        {

        }

        private void dgv_guestlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_paymttransfer_Click(object sender, EventArgs e)
        {
            String cur_bal = "0.00";
            String all_bal = lbl_deposit.Text;
            transfer_payment tb = new transfer_payment();

            if (lbl_gfolio.Text != "")
            {
                tb.MdiParent = this.MdiParent;
                tb.Show();
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }
            
            tb.set_data(lbl_gfolio.Text, rtxt_gname.Text, lbl_rm.Text, cur_bal, all_bal);
        }

        private void cbo_disfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbo_balances_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgv_guestlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.CellStyle.SelectionForeColor = Color.Brown;
            e.CellStyle.SelectionBackColor = Color.GreenYellow;

            if (e.RowIndex == -1)
            {
                SolidBrush br = new SolidBrush(Color.Gray);
                e.Graphics.FillRectangle(br, e.CellBounds);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
            else
            {
                if (e.RowIndex % 2 == 0)
                {
                    SolidBrush br = new SolidBrush(Color.Gainsboro);

                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
                else
                {
                    SolidBrush br = new SolidBrush(Color.White);
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
            }
        }

        private void dgv_gfolio_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.CellStyle.SelectionForeColor = Color.Brown;
            e.CellStyle.SelectionBackColor = Color.GreenYellow;

            if (e.RowIndex == -1)
            {
                SolidBrush br = new SolidBrush(Color.Gray);
                e.Graphics.FillRectangle(br, e.CellBounds);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
            else
            {
                if (e.RowIndex % 2 == 0)
                {
                    SolidBrush br = new SolidBrush(Color.Gainsboro);

                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
                else
                {
                    SolidBrush br = new SolidBrush(Color.White);
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
            }
        }
    }
}
