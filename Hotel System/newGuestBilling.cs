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
    public partial class newGuestBilling : Form
    {
        DataView v_glist;
        public String rom_code;
        public String rom_rate = "";
        thisDatabase db = new thisDatabase();
        Boolean forUp = false;
        public Boolean forView = false;
        public newGuestBilling()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (lbl_gfolio.Text != "")
            {
                add_charge chg = new add_charge(this);
                //chg.set_data(lbl_gfolio.Text, rtxt_gname.Text, lbl_rm.Text, lbl_rate.Text, lbl_rmtype.Text, rtxt_remarks.Text, lbl_arrdate.Text, lbl_depdate.Text, "", "", "", "", "", "", "", "", lbl_deposit.Text, panel11.Text, true);
                try
                {
                    chg.gisnew = true;
                    chg.an_frm_load(lbl_gfolio.Text.ToString(), "", "");
                    chg.lbl_balance.Text = lbl_balance.Text;
                    chg.textBox2.Text = label25.Text;
                    chg.textBox3.Text = label27.Text;
                    chg.textBox4.Text = label29.Text;
                    chg.textBox1.Text = label10.Text;
                    chg.lbl_deposit.Text = lbl_deposit.Text;
                    chg.chg_add(lbl_gfolio.Text.ToString(), "", "");
                    chg.ShowDialog();
                }
                catch (Exception er) { MessageBox.Show(er.Message); }
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
                add_charge chg = new add_charge(this);
                try
                {
                    chg.gisnew = false;
                    chg.an_frm_load(lbl_gfolio.Text.ToString(), dgv_gfolio["chg_code", dgv_gfolio.CurrentRow.Index].Value.ToString(), dgv_gfolio["chg_num", dgv_gfolio.CurrentRow.Index].Value.ToString());
                    chg.lbl_balance.Text = lbl_balance.Text;
                    chg.lbl_deposit.Text = lbl_deposit.Text;
                    chg.textBox2.Text = label25.Text;
                    chg.textBox3.Text = label27.Text;
                    chg.textBox4.Text = label29.Text;
                    chg.textBox1.Text = label10.Text;
                    chg.chg_add(lbl_gfolio.Text.ToString(), dgv_gfolio["chg_code", dgv_gfolio.CurrentRow.Index].Value.ToString(), dgv_gfolio["chg_num", dgv_gfolio.CurrentRow.Index].Value.ToString());
                    chg.ShowDialog();
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lbl_gfolio.Text != "")
            {
                thisDatabase db = new thisDatabase();
                //Report rpt = new Report("", "");
                //double val = (Convert.ToDateTime(lbl_depdate.Text) - Convert.ToDateTime(lbl_arrdate.Text)).TotalDays;
                //String noofnight = val.ToString();
                //String rmtyp_desc = db.get_colval("rtype", "typ_desc", "typ_code='" + lbl_rmtype.Text + "'");
                //String acct_no = db.get_colval("gfolio", "acct_no", "reg_num='" + lbl_gfolio.Text + "'");

                //if (val == 0)
                //{
                //    noofnight = "1";
                //}
                //rpt.Show();
                //rpt.printcurr_gfolio(lbl_gfolio.Text, acct_no, lbl_rm.Text, rtxt_gname.Text, lbl_rate.Text, lbl_pax.Text, rmtyp_desc, lbl_address.Text + " | " + lbl_company.Text, lbl_arrdate.Text, lbl_arrtime.Text, lbl_depdate.Text, lbl_deptime.Text, noofnight, db.get_folio_totalcharges(lbl_gfolio.Text).ToString(), db.get_folio_totalpayment(lbl_gfolio.Text).ToString(), lbl_disc.Text, lbl_grossrate.Text);
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
                GlobalMethod gm = new GlobalMethod();
                if (gm.toNormalDoubleFormat(all_bal) != 0)
                {
                    //transfer_balance frm = new transfer_balance(this, lbl_gfolio.Text, rtxt_gname.Text, lbl_rm.Text, cur_bal, all_bal);
                    //frm.ShowDialog();
                    //if (frm.isUpdated)
                    //{
                    //    disp_chgfil(lbl_gfolio.Text);
                    //}
                }
                else
                {
                    MessageBox.Show("Must be the balance not equal to zero(0).");
                }
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }
        }

        private void btn_paymttransfer_Click(object sender, EventArgs e)
        {
            String cur_bal = "0.00";
            String all_bal = lbl_deposit.Text;
            transfer_payment tb = new transfer_payment();

            if (lbl_gfolio.Text != "")
            {
                GlobalMethod gm = new GlobalMethod();
                if (gm.toNormalDoubleFormat(all_bal) != 0)
                {
                    //transfer_payment frm = new transfer_payment(this, lbl_gfolio.Text, rtxt_gname.Text, lbl_rm.Text, cur_bal, all_bal);
                    //frm.ShowDialog();
                    //if (frm.isUpdated)
                    //{
                    //    disp_chgfil(lbl_gfolio.Text);
                    //}
                }
                else
                {
                    MessageBox.Show("Must be has excess payment.");
                }
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }

            
        }
        public void dis_dgvguest(String cond)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();

            try
            {
                DateTime curdate = Convert.ToDateTime(db.get_systemdate(""));

                dt = ((forView == true) ? db.get_guest_histforview(cond) : db.get_guest_currentlycheckin(cond));

                if (forUp)
                {
                    lbl_gfolio.Text = db.QueryBySQLCodeRetStr("SELECT reg_num  FROM rssys.gfolio WHERE res_code = '" + dt.Rows[0]["res_code"].ToString() + "' LIMIT 1");
                    textBox1.Text = dt.Rows[0]["res_code"].ToString();
                    rtxt_gname.Text = dt.Rows[0]["full_name"].ToString();

                    label10.Text = ((dt.Rows[0]["ttlpax"].ToString() == "") ? (Convert.ToDouble(dt.Rows[0]["adult"].ToString() ?? "0") + Convert.ToDouble(dt.Rows[0]["kid"].ToString() ?? "0") + Convert.ToDouble(dt.Rows[0]["inf"].ToString() ?? "0")).ToString() : dt.Rows[0]["ttlpax"].ToString());

                    label25.Text = ((dt.Rows[0]["adult"].ToString() == "") ? "0" : dt.Rows[0]["adult"].ToString());
                    label27.Text = ((dt.Rows[0]["kid"].ToString() == "") ? "0" : dt.Rows[0]["kid"].ToString());
                    label29.Text = ((dt.Rows[0]["inf"].ToString() == "") ? "0" : dt.Rows[0]["inf"].ToString());

                    lbl_company.Text = dt.Rows[0]["name"].ToString();
                    label11.Text = dt.Rows[0]["rom_code"].ToString();

                    lbl_arrdate.Text = Convert.ToDateTime(dt.Rows[0]["arr_date"].ToString()).ToString("yyyy-MM-dd");
                    textBox2.Text = dt.Rows[0]["trv_name"].ToString();
                    textBox3.Text = dt.Rows[0]["seller"].ToString();

                    lbl_username.Text = dt.Rows[0]["user_id1"].ToString();
                    textBox4.Text = dt.Rows[0]["comttl"].ToString();
                    textBox5.Text = dt.Rows[0]["remarks1"].ToString();                    
                }
                else
                {
                    dgv_guestlist.DataSource = dt;
                }
            }
            catch (Exception er) {  }
        }
        private void clr_frm()
        {
            int length = 0;

            try
            {

            }
            catch (Exception er) { }
        }

        private void btn_chkout_Click(object sender, EventArgs e)
        {
            Double bal = Convert.ToDouble(lbl_balance.Text);
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            String reg_num = lbl_gfolio.Text;
            String chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, fol_num, chg_date, jrnlz, fol_name, food, misc, fcontract, res_code, tofr_fol, soa_code, doc_type, vat_amnt, sc_amnt;
            String acct_no, full_name, reg_date, arr_date, arr_time, dep_date, dep_time, typ_code, occ_type, rate_code, rom_rate, govt_tax, serv_chg, extra_bed, pay_code, mkt_code, src_code, trv_code, free_bfast, rm_features, bill_info, remarks, rom_class, rgrp_code, rgrp_ln, cancel, canc_reason, canc_user, canc_date, canc_time, grp_code, co_user, co_date, co_time, out_fol, fctr_code, disc_code, disc_pct, or_amnt, trace_no, ccrd_no;
            Boolean dt_result = false, dt2_result = false;

            String val2 = "";

            if (lbl_gfolio.Text == "")
            {
                MessageBox.Show("Pls select folio.");
            }
            else
            {
                if (bal != 0.00 || bal != 0)
                {
                    MessageBox.Show("Balance must be equal to ZERO.");
                }
                else if (Convert.ToDouble(lbl_deposit.Text) != 0.00 || Convert.ToDouble(lbl_deposit.Text) != 0)
                {
                    MessageBox.Show("Deposit must be equal to ZERO.");
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to check out this guest?", "Confirmation Dialog", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Boolean gf_ins = db.QueryBySQLCode_bool("INSERT INTO rssys.gfhist (reg_num, res_code, acct_no, full_name, arr_date, arr_time, dep_date, dep_time, rom_code, typ_code, occ_type, rate_code, rom_rate, govt_tax, serv_chg, extra_bed, pay_code, mkt_code, src_code, trv_code, free_bfast, rm_features, bill_info, remarks, rom_class, rgrp_code, rgrp_ln, user_id, t_date, t_time, cancel, canc_reason, canc_user, canc_date, canc_time, grp_code, co_user, co_date, co_time, out_fol, chg_code, fctr_code, disc_code, disc_pct, or_no1, or_amnt1, or_no2, or_amnt2, or_no3, or_amnt3, branch, transferred, paymentform, doc_ref, doctype, dep_amnt, nodeposit_rmrk, rmrttyp, ap_paymentform, ap_doc_ref, ap_doctype, ap_dep_amnt, ap_nodeposit_rmrk, d_currency_code, discount, hotel_code, p_typ, price, seller) SELECT reg_num, res_code, acct_no, full_name, arr_date, arr_time, dep_date, dep_time, rom_code, typ_code, occ_type, rate_code, rom_rate, govt_tax, serv_chg, extra_bed, pay_code, mkt_code, src_code, trv_code, free_bfast, rm_features, bill_info, remarks, rom_class, rgrp_code, rgrp_ln, user_id, t_date, t_time, cancel, canc_reason, canc_user, canc_date, canc_time, grp_code, '" + GlobalClass.username + "', current_date, '" + DateTime.Now.ToString("HH:mm") + "', out_fol, chg_code, fctr_code, disc_code, disc_pct, or_no1, or_amnt1, or_no2, or_amnt2, or_no3, or_amnt3, branch, transferred, paymentform, doc_ref, doctype, dep_amnt, nodeposit_rmrk, rmrttyp, ap_paymentform, ap_doc_ref, ap_doctype, ap_dep_amnt, ap_nodeposit_rmrk, d_currency_code, discount, hotel_code, p_typ, price, seller FROM rssys.gfolio WHERE reg_num = '" + reg_num + "'");
                        Boolean cf_ins = db.QueryBySQLCode_bool("INSERT INTO rssys.chghist (reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, fol_num, chg_date, jrnlz, fol_name, food, misc, fcontract, res_code, tofr_fol, soa_code, doc_type, vat_amnt, sc_amnt, trace_no, ccrd_no, currency_code, tax) SELECT reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, fol_num, chg_date, jrnlz, fol_name, food, misc, fcontract, res_code, tofr_fol, soa_code, doc_type, vat_amnt, sc_amnt, trace_no, ccrd_no, currency_code, tax FROM rssys.chgfil WHERE reg_num = '" + reg_num + "'");

                        if (gf_ins)
                        {
                            if (cf_ins)
                            {
                                db.DeleteOnTable("chgfil", "reg_num = '" + reg_num + "'");
                                db.DeleteOnTable("gfolio", "reg_num = '" + reg_num + "'");
                                dis_dgvguest("");
                                MessageBox.Show("Successfully checked out guest");
                            }
                            else
                            {
                                db.DeleteOnTable("gfhist", "reg_num = '" + reg_num + "'");
                                MessageBox.Show("Error on removing guest transactions.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error on checking out Guest");
                        }
                    }
                }
            }
            tbcntrl_option.SelectedTab = tpg_opt_1;
            clr_frm();
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show(); 
        }
        public void disp_chgfil(String reg_num)
        {

        }
        private void dgv_guestlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            up_dt();
        }
        private void disp_guestinfo(String reg_num)
        {

        }

        private void newGuestBilling_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            GlobalMethod gm = new GlobalMethod();
            v_glist = new DataView();

            gm.load_company(cbo_srch_company);
            cbo_disfolio.SelectedIndex = 0;
            cbo_balances.SelectedIndex = 0;

            forUp = false;
            dis_dgvguest("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String cond = "";

            dis_dgvguest(cond);
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            up_dt();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            forUp = false;
            dis_dgvguest("");
            tbcntrl_option.SelectedTab = tpg_opt_1;
            clr_frm();
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show(); 
        }

        private void dgv_gfolio_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void dgv_guestlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String cond = "";
            txt_srch_gname.Text = "";
            comboBox1.SelectedIndex = -1;
            cbo_srch_company.SelectedIndex = -1;
            cbo_contract.SelectedIndex = -1;
            dis_dgvguest(cond);
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {

        }

        private void btn_presentbill_Click(object sender, EventArgs e)
        {

            if (lbl_gfolio.Text != "")
            {
                selectsoa frm = new selectsoa(this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pls select folio.");
            }
        }

        public void print_soa(String dtfrom, String dtto)
        {
            //thisDatabase db = new thisDatabase();
            //Report rpt = new Report("", "");
            //double val = (Convert.ToDateTime(lbl_depdate.Text) - Convert.ToDateTime(lbl_arrdate.Text)).TotalDays;
            //String noofnight = val.ToString();
            //String rmtyp_desc = db.get_colval("rtype", "typ_desc", "typ_code='" + lbl_rmtype.Text + "'");
            //String acct_no = db.get_colval("gfolio", "acct_no", "reg_num='" + lbl_gfolio.Text + "'");

            //if (val == 0) noofnight = "1";

            //rpt.Show();
            //rpt.printprev_gfolio(lbl_gfolio.Text, acct_no, lbl_rm.Text, rtxt_gname.Text, lbl_rate.Text, lbl_pax.Text, rmtyp_desc, lbl_address.Text + " | " + lbl_company.Text, lbl_arrdate.Text, lbl_arrtime.Text, lbl_depdate.Text, lbl_deptime.Text, noofnight, db.get_folio_totalcharges(lbl_gfolio.Text).ToString(), db.get_folio_totalpayment(lbl_gfolio.Text).ToString(), lbl_disc.Text, lbl_grossrate.Text, dtfrom, dtto);
        }

        private void btn_finalizedsoa_Click(object sender, EventArgs e)
        {
            //if (lbl_gfolio.Text != "")
            //{
            //    if (!String.IsNullOrEmpty(db.get_colval("m99", "fnlz_chg", "")))
            //    {
            //        selectsoa frm = new selectsoa(this, lbl_gfolio.Text, lbl_rmrttyp.Text.Substring(0,1).ToUpper());
            //        frm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Make sure finalize unit billing has a charge.\nGo to Settings > System Setting > General, Select 'Finalized SOA Combo Box'");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Pls select folio.");
            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void up_dt()
        {
            GlobalMethod gm = new GlobalMethod();
            forUp = true;
            try
            {
                String guest = ((dgv_guestlist.SelectedRows.Count > 0) ? dgv_guestlist["res_code", dgv_guestlist.CurrentRow.Index].Value.ToString() : textBox1.Text.ToString());
                dis_dgvguest("res_code = '" + guest + "'");

                DataTable dt_stat = ((forView == true) ? db.QueryBySQLCode("SELECT reg_num, res_code AS r_code, chg_code, chg_num, rom_code AS room_code, reference, amount, user_id, t_date, t_time FROM rssys.chghist WHERE res_code = '" + guest + "'") : db.QueryBySQLCode("SELECT reg_num, res_code AS r_code, chg_code, chg_num, rom_code AS room_code, reference, amount, user_id, t_date, t_time FROM rssys.chgfil WHERE res_code = '" + guest + "'"));
                DataTable dt_pbd = ((forView == true) ? db.QueryBySQLCode("SELECT SUM(amount) AS ttl FROM rssys.chghist WHERE res_code = '" + guest + "'") : db.QueryBySQLCode("SELECT SUM(amount) AS ttl FROM rssys.chgfil WHERE res_code = '" + guest + "'"));
                dgv_gfolio.DataSource = null;
                dgv_gfolio.DataSource = dt_stat;
                if (dt_pbd.Rows.Count > 0)
                {
                    Double tl = 0.00;
                    try
                    {
                        tl = Convert.ToDouble(dt_pbd.Rows[0]["ttl"].ToString());
                    } catch {}
                    if (tl < 0)
                    {
                        lbl_balance.Text = gm.toAccountingFormat(0.00);
                        lbl_deposit.Text = gm.toAccountingFormat((tl * -1));
                    }
                    else
                    {
                        lbl_balance.Text = gm.toAccountingFormat((tl * -1));
                        lbl_deposit.Text = gm.toAccountingFormat(0.00);
                    }
                }

                tbcntrl_option.SelectedTab = tpg_opt_2;
                clr_frm();
                tbcntrl_res.SelectedTab = tpg_reg;
                tpg_reg.Show(); 
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_gfolio.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete this (" + dgv_gfolio["reference", dgv_gfolio.CurrentRow.Index].Value.ToString() + ") transaction?", "Cofirmation Dialog", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (db.DeleteOnTable("chgfil", "reg_num = '" + dgv_gfolio["reg_num", dgv_gfolio.CurrentRow.Index].Value.ToString() + "' AND chg_code = '" + dgv_gfolio["chg_code", dgv_gfolio.CurrentRow.Index].Value.ToString() + "' AND chg_num = '" + dgv_gfolio["chg_num", dgv_gfolio.CurrentRow.Index].Value.ToString() + "'"))
                        {
                            MessageBox.Show("Successfully deleted transaction");
                            up_dt();
                        }
                        else
                        {
                            MessageBox.Show("Error on deleting transaction");
                        }
                    }
                }
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable check_r = db.QueryBySQLCode("SELECT rg_code FROM rssys.com_p WHERE rg_code = '" + textBox1.Text.ToString() + "'");
            String check_rg = "";
            if (check_r.Rows.Count > 0)
            {
                check_rg = "Upd";
            }
            Boolean stat = ((check_rg == "") ? db.InsertOnTable("com_p", "rg_code, com, trv_code, seller, remarks, r_date, cashier", "'" + textBox1.Text.ToString() + "', '" + textBox4.Text.ToString() + "', '" + db.QueryBySQLCodeRetStr("SELECT trv_code FROM rssys.gfolio WHERE res_code = '" + textBox1.Text.ToString() + "'") + "', '" + textBox3.Text.ToString() + "', '" + textBox5.Text.ToString() + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + GlobalClass.username + "'") : db.UpdateOnTable("com_p", "com = '" + textBox4.Text.ToString() + "', remarks = '" + textBox5.Text.ToString() + "'", "rg_code = '" + textBox1.Text.ToString() + "'"));
            if (stat)
            {
                MessageBox.Show("Successfully " + ((check_rg == "") ? "added new" : "updated") + " entry for commission");
            }
            else
            {
                MessageBox.Show("Error on " + ((check_rg == "") ? "adding new" : "updating") + " entry for commission");
            }
        }

        private void dgv_guestlist_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            try
            {
                if (dgv_guestlist["remarks1", e.RowIndex].Value.ToString().ToUpper() == "NOT RELEASED" || dgv_guestlist["remarks1", e.RowIndex].Value.ToString().ToUpper() == "")
                {
                    CellStyle.BackColor = Color.Red;
                    CellStyle.ForeColor = Color.White;
                }
                else
                {
                    CellStyle.BackColor = Color.LimeGreen;
                    CellStyle.ForeColor = Color.Black;
                }

                dgv_guestlist.Rows[e.RowIndex].Cells["res_code"].Style = CellStyle;
            }
            catch { }
        }

        public void forVv()
        {
            if (forView == true)
            {
                btn_upditem.Text = "View Entry history";
                btn_upditem.Image = Hotel_System.Properties.Resources.note_search___32;
                btn_chkout.Visible = false;
                btn_add.Visible = false;
                btn_edit.Visible = false;
                button6.Visible = false;

                btn_chkout.Enabled = false;
                btn_add.Enabled = false;
                btn_edit.Enabled = false;
                button6.Enabled = false;
            }
            else
            {
                btn_upditem.Text = "View Entry history";
                btn_chkout.Visible = true;
                btn_add.Visible = true;
                btn_edit.Visible = true;
                button6.Visible = true;

                btn_chkout.Enabled = true;
                btn_add.Enabled = true;
                btn_edit.Enabled = true;
                button6.Enabled = true;
            }
        }
    }
}
