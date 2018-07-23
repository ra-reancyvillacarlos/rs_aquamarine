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
    public partial class InHouseGuest : Form
    {
        Main mainform;
        String modname = "";

        public InHouseGuest(Main m)
        {
            InitializeComponent();
            mainform = m;
            modname = mainform.get_modname();
        }
        public InHouseGuest()
        {
            InitializeComponent();
            
        }
        private void InHouseGuest_Load(object sender, EventArgs e)
        {
           this.WindowState = FormWindowState.Maximized;
            GlobalMethod gm = new GlobalMethod();
            pnl_left.Hide();
            dis_dgvguest();

            gm.load_romtype(cbo_type);
            gm.load_company(cbo_srch_company);
            gm.load_ratetype(cbo_rtcode);
            gm.load_market(cbo_mktsegment);
            gm.load_disctbl(cbo_disc);
            gm.load_romratetype(cbo_rmrttyp);


            clr_field();
            //btn_chg_rate.Enabled = false;
        }

        public void reset_modname()
        {
            mainform.set_modname(modname);
        }

        private void dis_dgvguest()
        {
            thisDatabase db = new thisDatabase();

            dgv_guestlist.DataSource = db.get_guest_currentlycheckin("");
        }

        private void dgv_guestlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String reg_num = dgv_guestlist[5, e.RowIndex].Value.ToString();

                disp_guestinfo(reg_num);
                disp_chgfil(reg_num);
            }
            catch (Exception) {}
        }

        private void disp_guestinfo(String reg_num)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            DataTable dt_guest = new DataTable();
            DataTable dt_otherguestid = new DataTable();
            String guestno = "";
            String rom_rate = "";
            String occ_code = "";
            String rmrttyp = "";
            Double night = 0, mo = 0;
            double val = 0;

            try
            {
                if (dgv_guestlist.SelectedRows.Count > 0)
                {
                    clr_field();

                    dt = db.get_guestfolio_all(reg_num);

                    foreach (DataRow r in dt.Rows)
                    {
                        rom_rate = r["rom_rate"].ToString();

                        dtp_resdt.Value = Convert.ToDateTime(r["reg_date"].ToString());
                        lbl_gfno.Text = r["reg_num"].ToString();
                        lbl_resno.Text = r["res_code"].ToString();
                        lbl_arrdt.Text = Convert.ToDateTime(r["arr_date"].ToString()).ToString("MM/dd/yyyy");
                        lbl_depdt.Text = Convert.ToDateTime(r["dep_date"].ToString()).ToString("MM/dd/yyyy");
                        lbl_rm.Text = r["rom_code"].ToString();
                        lbl_rmtyp.Text = db.get_romtyp_desc(r["typ_code"].ToString());
                        lbl_clerk.Text = r["user_id"].ToString();

                        val = (Convert.ToDateTime(lbl_depdt.Text) - Convert.ToDateTime(lbl_arrdt.Text)).TotalDays;

                        lbl_noofnight.Text = val.ToString();
                        lbl_noofnight_billing.Text = val.ToString();

                        if (val == 0)
                        {
                            lbl_noofnight.Text = "1";
                            lbl_noofnight_billing.Text = "1";
                        }
                        /*
                        rmrttyp = r["rmrttyp"].ToString();

                        cbo_rmrttyp.SelectedValue = rmrttyp;

                        if (rmrttyp == "M")
                        {           

                            lbl_noofnight_title.Text = "No. of Months";

                            night = Convert.ToDouble(lbl_noofnight.Text);

                            if (night > 30)
                            {
                                mo = night / 30;

                                if (night % 30 > 0)
                                {
                                    mo = mo + 1.00;
                                }
                            }
                            else
                            {
                                mo = 1.00;
                            }

                            lbl_noofnight_billing.Text = mo.ToString("0");
                        }
                        else
                        {
                            lbl_noofnight_title.Text = "No. of Nights";

                            lbl_noofnight_billing.Text = lbl_noofnight.Text;
                        }
                         */

                        cbo_rtcode.SelectedValue = r["rate_code"].ToString();

                        occ_code = r["occ_type"].ToString();
                        cbo_mktsegment.SelectedValue = r["mkt_code"].ToString();
                        cbo_disc.SelectedValue = r["disc_code"].ToString();

                        rtb_remarks.Text = r["remarks"].ToString() + " " + r["rm_features"].ToString() + " " + r["bill_info"].ToString();
                        txt_discamt.Text = r["free_bfast"].ToString();
                        guestno = r["acct_no"].ToString();
                    }

                    
                    if (occ_code == "1")
                    {
                        cbo_occtyp.Text = "Single";
                    }
                    else if (occ_code == "2")
                    {
                        cbo_occtyp.Text = "Double";
                    }
                    else if (occ_code == "3")
                    {
                        cbo_occtyp.Text = "Triple";
                    }
                    else if (occ_code == "4")
                    {
                        cbo_occtyp.Text = "Quad";
                    }

                    dt_guest = db.get_guest_info(guestno);
                    dgv_guest.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());
                    dt_otherguestid = db.get_resguest_id(reg_num);
                    dt_guest.Rows.Clear();

                    foreach (DataRow rowid in dt_otherguestid.Rows)
                    {
                        dt_guest = db.get_guest_info(rowid["acct_no"].ToString());
                        dgv_guest.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());
                        dt_guest.Rows.Clear();
                    }   

                    disp_computed_bill();

                    f_rom.Text = lbl_rm.Text;
                    f_rate.Text = txt_rmrate.Text;
                    f_type.Text = db.get_romtyp_code(lbl_rmtyp.Text);
                }
                else
                {
                    MessageBox.Show("Pls select the room.");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            } 
        }

        private void disp_chgfil(String reg_num)
        {
            thisDatabase db = new thisDatabase();
            dgv_gfolio.DataSource = db.get_guest_chargefil(reg_num, false);
        }

        private void disp_computed_bill()
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            Double amt = 0.00;
            Double lessdiscount = 0.00, lessdisc_amt = 0.00;
            Double grossamt = 0.00;
            int occ = 1;
            Boolean issenior_disc = false;
            Double db_rmrate = 0.00;
            Double db_rmgovt = 0.00;
            Decimal roundoff_amt;

            try
            {
                if (cbo_occtyp.Text == "Double")
                {
                    occ = 2;
                }
                else if (cbo_occtyp.Text == "Triple")
                {
                    occ = 3;
                }
                else if (cbo_occtyp.Text == "Quad")
                {
                    occ = 4;
                }

                if (cbo_disc.SelectedIndex >= 0)
                {
                    lessdiscount = db.get_discount(cbo_disc.SelectedValue.ToString());
                    issenior_disc = db.issenior_disc(cbo_disc.SelectedValue.ToString());
                }

                if (String.IsNullOrEmpty(txt_discamt.Text))
                {
                   txt_discamt.Text = "0.00";
                }

                lessdisc_amt = gm.toNormalDoubleFormat(txt_discamt.Text);

                grossamt = db.get_roomrateamt(cbo_rtcode.SelectedValue.ToString(), db.get_romtyp_code(lbl_rmtyp.Text), occ);
                txt_grossrt.Text = grossamt.ToString("0.00");

                txt_netrt.Text = db.get_netrate(grossamt, lessdiscount, lessdisc_amt).ToString("0.00");
                txt_govtrt.Text = db.get_tax(grossamt, lessdiscount, lessdisc_amt).ToString("0.00");
                //txt_srvccharge.Text = db.get_svccharge(grossamt, lessdiscount, lessdisc_amt).ToString("0.00");

                //regular rate w/ or w/out discount
                if (issenior_disc == false)
                {
                    //txt_rmrate.Text = (grossamt - (grossamt * (lessdiscount / 100))).ToString("0.00");
                }
                //senior citizen discount no tax
                else if (issenior_disc == true)
                {
                    //Double db_rmrate = Convert.ToDouble(txt_netrt.Text) + Convert.ToDouble(txt_srvccharge.Text);
                    
                    txt_govtrt.Text = "0.00";
                }

                db_rmrate = Convert.ToDouble(txt_netrt.Text);
                db_rmgovt = Convert.ToDouble(txt_govtrt.Text);

                roundoff_amt = Math.Round(Convert.ToDecimal(db_rmrate + db_rmgovt), 1);

                txt_rmrate.Text = roundoff_amt.ToString("0.00");

                if (cbo_rmrttyp.SelectedValue == "M")
                {

                }

                txt_total_amt.Text = (Convert.ToDouble(txt_rmrate.Text) * Convert.ToDouble(lbl_noofnight_billing.Text)).ToString("0.00");
            }
            catch (Exception)
            { }
        }

        private void clr_field()
        {
            lbl_gfno.Text = "";
            lbl_resno.Text = "";
            lbl_arrdt.Text = "";
            lbl_noofnight.Text = "0";
            lbl_rm.Text = "";
            lbl_rmtyp.Text = "";
            lbl_noofnight_billing.Text = "0";
            lbl_noofguest.Text = "0";
            lbl_depdt.Text = "";

            cbo_disc.SelectedIndex = -1;
            cbo_mktsegment.SelectedIndex = -1;
            cbo_rtcode.SelectedIndex = -1;
            cbo_occtyp.SelectedIndex = -1;

            txt_grossrt.Text = "0.00";
            txt_discamt.Text = "0";
            txt_govtrt.Text = "0.00";
            txt_netrt.Text = "0.00";
            txt_rmrate.Text = "0.00";
            //txt_srvccharge.Text = "0.00";
            txt_total_amt.Text = "0.00";
            rtb_remarks.Text = "";

            if (dgv_guest.Rows.Count > 1)
            {
                for (int i = 0; dgv_guest.Rows.Count > i; i++)
                {
                    dgv_guest.Rows.RemoveAt(i);
                }
            }
        }

        private void btn_rmtransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(lbl_gfno.Text))
                {
                    MessageBox.Show("Pls select folio.");
                }
                else
                {
                    RoomTransfer rmtrans = new RoomTransfer(this);
                    String disc_code = "";

                    if(cbo_disc.SelectedIndex != -1)
                    {
                        disc_code = cbo_disc.SelectedValue.ToString();
                    }
                    mainform.set_modname(modname + " > " + rmtrans.Name);
                    rmtrans.set_data(1, lbl_gfno.Text, dgv_guest[1, 0].Value.ToString(), lbl_rm.Text, lbl_arrdt.Text, lbl_depdt.Text, lbl_rmtyp.Text, txt_rmrate.Text, cbo_rtcode.SelectedValue.ToString(), disc_code, cbo_occtyp.Text.ToString());

                    rmtrans.MdiParent = this.MdiParent;
                    
                    rmtrans.Show();
                }
            }
            catch 
            {
                
            }
        }

        private void btn_folio_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String curdate = DateTime.Today.ToString("yyyy-MM-dd");
            String curtime = DateTime.Now.ToString("HH:mm");
            String reg_num = lbl_gfno.Text;
            String dacct_no = dgv_guestlist.Rows[0].Cells[0].Value.ToString();
            String dfull_name = dgv_guestlist.Rows[0].Cells[1].Value.ToString();
            String rmtyp = db.get_romtyp_code(lbl_rmtyp.Text);
            String rmrttyp = "D";
            String disc = "";
            String disc_amt = "0.00";
            int occ = 1;

            try
            {
                if (lbl_gfno.Text == "")
                {
                    MessageBox.Show("Pls select folio.");
                }
                else
                {
                    if (cbo_occtyp.Text == "Double")
                    {
                        occ = 2;
                    }
                    else if (cbo_occtyp.Text == "Triple")
                    {
                        occ = 3;
                    }
                    else if (cbo_occtyp.Text == "Quad")
                    {
                        occ = 4;
                    }

                    if (cbo_rmrttyp.SelectedIndex > -1)
                    {
                        rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
                    }

                    if (cbo_disc.SelectedIndex > -1)
                    {
                        disc = cbo_disc.SelectedValue.ToString();
                        disc_amt = db.get_discount(cbo_disc.SelectedValue.ToString()).ToString("0.00");
                    }

                    if (dgv_guestlist.Rows.Count <= 1)
                    {
                        MessageBox.Show("Pls select guest(s) at Guest list.");
                    }
                    else if (cbo_rtcode.SelectedIndex == -1)
                    {
                        MessageBox.Show("Pls select the type of rate code.");
                    }
                    else if (cbo_mktsegment.SelectedIndex == -1 && cbo_mktsegment.Text == "")
                    {
                        MessageBox.Show("Pls select the type of market segment.");
                    }
                    else if (lbl_rm.Text == "")
                    {
                        MessageBox.Show("Pls select room.");
                    }
                    else if (db.UpdateOnTable("gfolio", "occ_type='" + occ.ToString() + "', rate_code='" + cbo_rtcode.SelectedValue.ToString() + "', rom_rate='" + txt_netrt.Text + "', govt_tax='" + txt_govtrt.Text + "', mkt_code='" + cbo_mktsegment.SelectedValue.ToString() + "', free_bfast='" + txt_discamt.Text + "', remarks='" + rtb_remarks.Text + "', user_id='" + GlobalClass.username + "', t_date='" + Convert.ToDateTime(db.get_systemdate("")).ToString("yyyy-MM-dd") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "', disc_code='" + disc.ToString() + "', disc_pct='" + disc_amt.ToString() + "', arr_date='" + lbl_arrdt.Text + "', dep_date='" + lbl_depdt.Text + "', rom_code='" + lbl_rm.Text + "', typ_code='" + db.get_romtyp_code(lbl_rmtyp.Text) + "', rmrttyp='" + rmrttyp + "'", "reg_num='" + reg_num + "'"))
                    {
                        db.DeleteOnTable("gfguest", "reg_num='" + lbl_gfno.Text + "'");
                        //insert other guests
                        for (int r = 1; dgv_guest.Rows.Count - 1 > r; r++)
                        {
                            dacct_no = dgv_guest.Rows[r].Cells[0].Value.ToString();
                            dfull_name = dgv_guest.Rows[r].Cells[1].Value.ToString();

                            db.InsertOnTable("gfguest", "reg_num, acct_no, full_name, arr_date, dep_date", "'" + lbl_gfno.Text + "', '" + dacct_no + "', '" + dfull_name + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "'");
                        }

                        String chg_num = db.get_pk("chg_num");
                        String col_gfchg = "chg_num, reg_num, full_name, t_date, t_time, arr_date, dep_date, f_rom, f_type, f_rate, t_rom, t_type, t_rate, remarks, printed, user_id";
                        String val_gfchg = "'" + chg_num + "', '" + lbl_gfno.Text + "', '" + dfull_name + "','" + Convert.ToDateTime(db.get_systemdate("")).ToString("yyyy-MM-dd") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', '" + f_rom.Text + "', '" + f_type.Text + "', '" + f_rate.Text + "', '" + lbl_rm.Text + "', '" + rmtyp + "', '" + cbo_rtcode.SelectedValue.ToString() + "', '" + rtb_remarks.Text + "', 'N', '" + GlobalClass.username + "'";

                        db.InsertOnTable("gfchange", col_gfchg, val_gfchg);
                        //increment rescode to m99
                        db.set_pkm99("chg_num", db.get_nextincrementlimitchar(chg_num, 8));

                        MessageBox.Show("Record(s) updated successfully.");
                        clr_field();
                    }
                    else
                    {
                        MessageBox.Show("Problem occur on updating.");
                    }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void cbo_rtcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String rmrttyp;
            Double val = 0;
            String room_rate_code = "";

            try
            {
                DateTime date_arr = Convert.ToDateTime(lbl_arrdt.Text);
                DateTime date_dep = Convert.ToDateTime(lbl_depdt.Text);

                int time_diff;

                if (cbo_rtcode.SelectedIndex > -1)
                {
                    DataTable dt = db.QueryBySQLCode("SELECT rmrttyp from rssys.ratetype WHERE rate_code='" + cbo_rtcode.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {

                        room_rate_code = dt.Rows[0]["rmrttyp"].ToString();
                    }
                    cbo_rmrttyp.SelectedValue = db.get_romratetype_code(room_rate_code);

                    rmrttyp = cbo_rmrttyp.SelectedValue.ToString();

                    if (rmrttyp == "M")
                    {                       
                    
                        lbl_noofnight_title_top.Text = "No. of Months"; 
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        time_diff = 12*(date_dep.Year - date_arr.Year) + (date_dep.Month - date_arr.Month);

                        if (time_diff == 0)
                        {
                            time_diff = 1;
                        }

                        lbl_noofnight.Text = time_diff.ToString("0");
                        lbl_noofnight_billing.Text = lbl_noofnight.Text;
                    }
                    else if (rmrttyp == "W")
                    {
                        lbl_noofnight_title_top.Text = "No. of Weeks";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        time_diff = Convert.ToInt32((Convert.ToDateTime(lbl_depdt.Text) - Convert.ToDateTime(lbl_arrdt.Text)).TotalDays / 7);

                        if (time_diff == 0)
                        {
                            time_diff = 1;
                        }

                        lbl_noofnight.Text = time_diff.ToString("0");

                        lbl_noofnight_billing.Text = lbl_noofnight.Text;
                    }
                    else
                    {
                        val = (Convert.ToDateTime(lbl_depdt.Text) - Convert.ToDateTime(lbl_arrdt.Text)).TotalDays;

                        lbl_noofnight_title_top.Text = "No. of Nights";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        lbl_noofnight.Text = val.ToString("0");
                        lbl_noofnight_billing.Text = lbl_noofnight.Text;
                    }
                }
                else
                {
                    cbo_rmrttyp.SelectedIndex = -1;
                }
            }
            catch (Exception) { }

            disp_computed_bill();
        }

        private void cbo_disc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_disc.SelectedIndex > -1)
            {
                if (cbo_disc.SelectedValue.ToString() == "014")
                {
                    txt_discamt.Enabled = true;
                    txt_discamt.Text = "0.00";
                }
                else
                {
                    txt_discamt.Enabled = false;
                    txt_discamt.Text = "0.00";
                }
            }

            disp_computed_bill();
        }

        private void btn_selectcust_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalMethod gmethod = new GlobalMethod();
               // addGuest AG = new addGuest(null, null, this);
                GlobalClass.gdgv = gmethod.CopyDataGridView(dgv_guest);

                mainform.set_modname(modname + " > " + "Select Guest");

                //AG.MdiParent = this.MdiParent;
                //AG.reload_guest();
                //AG.Show();

            }
            catch (Exception) { }
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            
        }

        public void reload_guest()
        {
            DataGridViewRow row = new DataGridViewRow();

            if (GlobalClass.gdgv != null)
            {
                dgv_guest.Rows.Clear();

                for (int i = 0; i < GlobalClass.gdgv.Rows.Count - 1; i++)
                {
                    row = (DataGridViewRow)GlobalClass.gdgv.Rows[i].Clone();

                    int intColIndex = 0;

                    foreach (DataGridViewCell cell in GlobalClass.gdgv.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_guest.Rows.Add(row);
                }
            }

            lbl_noofguest.Text = (dgv_guest.Rows.Count - 1).ToString();

            reset_modname();
            GlobalClass.gdgv = null;
        }

        private void btn_removefrmguestlist_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dgv_guest.CurrentRow.Index;

                dgv_guest.Rows.RemoveAt(row);

                lbl_noofguest.Text = (dgv_guest.Rows.Count - 1).ToString();
            }
            catch (Exception er) { MessageBox.Show("Pls select/highlight the guest to remove."); }
        }

        private void btn_chg_rate_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(lbl_gfno.Text))
                {
                    MessageBox.Show("Pls select folio.");
                }
                else
                {
                    String approvedby = Prompt.ShowPasswordDialog("Pls enter password", "Superivisory Password");

                    if (String.IsNullOrEmpty(approvedby) == false)
                    {
                        RoomTransfer rmtrans = new RoomTransfer(this);
                        String disc_code = "";
                        rmtrans.Name = "Upgrade/Downgrade";

                        if (cbo_disc.SelectedIndex > -1)
                        {
                            disc_code = cbo_disc.SelectedValue.ToString();
                        }
                        mainform.set_modname(modname + " > " + rmtrans.Name);
                        rmtrans.set_data(2, lbl_gfno.Text, dgv_guest[1, 0].Value.ToString(), lbl_rm.Text, lbl_arrdt.Text, lbl_depdt.Text, lbl_rmtyp.Text, txt_rmrate.Text, cbo_rtcode.SelectedValue.ToString(), disc_code, cbo_occtyp.Text.ToString());

                        rmtrans.MdiParent = this.MdiParent;

                        rmtrans.Show();
                    }
                    else
                    {
                        MessageBox.Show("Password is unathorized or invalid.");
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            clr_field();
        }

        private void btn_reprint_regcard_Click(object sender, EventArgs e)
        {
            Report rpt = new Report("", "");
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            DataTable dt = new DataTable();
            Double noofnights = 0;
            DateTime arr_date = new DateTime();
            DateTime dep_date = new DateTime();
            String daddress = "";

            if (lbl_gfno.Text == "")
            {
                MessageBox.Show("Pls select folio.");
            }
            else
            {
                dt = db.get_guestfolio_all_withaddr(lbl_gfno.Text);

                foreach (DataRow row in dt.Rows)
                {
                    arr_date = Convert.ToDateTime(row["arr_date"].ToString());
                    dep_date = Convert.ToDateTime(row["dep_date"].ToString());
                    noofnights = (dep_date - arr_date).TotalDays;
                    daddress = db.get_addrcompmarket(row["acct_no"].ToString());
                    rpt.printprev_regform(row["reg_num"].ToString(), arr_date.ToString("yyyy-MM-dd"), row["arr_time"].ToString(), row["res_code"].ToString(), row["rom_code"].ToString(), "", row["full_name"].ToString(), gm.toNormalDoubleFormat(row["rom_rate"].ToString()) + gm.toNormalDoubleFormat(row["govt_tax"].ToString()) + gm.toNormalDoubleFormat(row["serv_chg"].ToString()).ToString("0.00"), row["occ_type"].ToString(), db.get_romtyp_code(row["typ_code"].ToString()), daddress, arr_date.ToString("yyyy-MM-dd"), row["arr_time"].ToString(), dep_date.ToString("yyyy-MM-dd"), row["dep_time"].ToString(), noofnights.ToString("0.00"), row["rm_features"].ToString() + " | " + row["bill_info"].ToString() + " | " + row["remarks"].ToString() + " | " + row["nodeposit_rmrk"].ToString(), row["paymentform"].ToString(), row["doc_ref"].ToString(), row["doctype"].ToString(), row["dep_amnt"].ToString(), row["user_id"].ToString(), "", "", "", "", "", "", "", "", "", row["ap_paymentform"].ToString(), row["ap_doc_ref"].ToString(), row["ap_dep_amnt"].ToString());
                }

                rpt.Show();
            }
        }


        private void btn_depdate_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            if (dgv_guest.Rows.Count <= 1)
            {
                MessageBox.Show("Pls select folio.");
            }
            else
            {
                dtp_checkin.Value = Convert.ToDateTime(db.get_systemdate(""));
                dtp_checkout.Value = Convert.ToDateTime(db.get_systemdate(""));
                txt_noofnights.Text = get_noofnights();
                pnl_left.Show();
            }
        }

        private void dtp_checkout_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txt_noofnights.Text) < 0)
            {
                MessageBox.Show("No. of Nights must not less than zero.");
            }
            else
            {
                txt_noofnights.Text = get_noofnights();
                load_rom_available();
            }
        }

        private String get_noofnights()
        {
            try
            {
                double val = (dtp_checkout.Value - dtp_checkin.Value).TotalDays;

                return val.ToString();              
            }
            catch (Exception) { }

            return "0";
        }

        private void txt_noofnights_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (e.Equals(""))
                {
                    txt_noofnights.Text = "1";
                }
                else
                {
                    dtp_checkout.Value = dtp_checkin.Value.AddDays(Convert.ToInt32(txt_noofnights.Text));
                }
            }
            catch (Exception) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txt_noofnights.Text) < 0)
            {
                MessageBox.Show("No. of Nights must not less than zero.");
            }
            else
            {
                load_rom_available();
            }
        }

        private void load_rom_available()
        {
            String typ_code = "";
            DataTable dt_allrooms = new DataTable();
            DataTable dt_reserved = new DataTable();
            DataTable dt_occupied = new DataTable();

            if (cbo_type.SelectedIndex == -1)
            {
                typ_code = "";
            }
            else
            {
                typ_code = cbo_type.SelectedValue.ToString();
            }

            thisDatabase db = new thisDatabase();

            dt_allrooms = db.get_allroomWithZ(typ_code, null, "");
            dt_reserved = db.get_reservedroomExptZ(dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), typ_code);
            dt_occupied = db.get_occupancyExptZandgfolioRoom("rom_code, res_code, full_name, arr_date, dep_date", dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), typ_code, lbl_rm.Text);

            DataRow[] drr;

            lbl_rom.Text = dt_allrooms.Rows.Count.ToString();
            lbl_res.Text = dt_reserved.Rows.Count.ToString();
            lbl_occ.Text = dt_occupied.Rows.Count.ToString();

            //remove reserved
            if (dt_reserved.Rows.Count > 0)
            {
                try
                {
                    for (int r = 0; r < dt_reserved.Rows.Count; r++)
                    {
                        //MessageBox.Show(dt_reserved.Rows[r]["dep_date"].ToString() + " - " + dtp_checkout.Value.ToString("yyyy-MM-dd") + " == " + Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).Equals(dtp_checkout.Value).ToString());
                        //MessageBox.Show(dt_reserved.Rows[r]["rom_code"].ToString() + ":: " +db.get_systemdate() + " => " + Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate())).ToString() + " and " + Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate())).ToString() + " and " + String.IsNullOrEmpty(dt_reserved.Rows[r]["arrived"].ToString()).ToString());
                        if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) <= 0 && Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) >= 0 && String.IsNullOrEmpty(dt_reserved.Rows[r]["arrived"].ToString()) == true)
                        {
                            //MessageBox.Show(dt_reserved.Rows[r]["arrived"].ToString() + " is not arrived.");
                            //if equal nothing to do.
                        }
                        else if (Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).Equals(dtp_checkin.Value))
                        {
                            //if equal nothing to do.
                        }
                        else if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).Equals(dtp_checkout.Value))
                        {
                            //if equal nothing to do.
                        }
                        else
                        {
                            drr = dt_allrooms.Select("rom_code='" + dt_reserved.Rows[r]["rom_code"].ToString() + "'");

                            if (drr.Length > 0)
                            {
                                drr[0].Delete();
                                dt_allrooms.AcceptChanges();
                            }
                        }
                    }
                }
                catch (Exception) { }
            }

            //remove occuppied
            if (dt_occupied.Rows.Count > 0)
            {
                try
                {
                    for (int o = 0; o < dt_occupied.Rows.Count; o++)
                    {
                        //MessageBox.Show(dt_occupied.Rows[o]["rom_code"].ToString() + ": " + dt_occupied.Rows[o]["dep_date"].ToString() + " - " + dtp_checkin.Value.ToString("yyyy-MM-dd") + " == " + Convert.ToDateTime(dt_occupied.Rows[o]["dep_date"].ToString()).Equals(dtp_checkin.Value).ToString());
                        if (Convert.ToDateTime(dt_occupied.Rows[o]["dep_date"].ToString()).Equals(dtp_checkin.Value))
                        {
                            //if equal nothing to do. means to be available
                        }
                        else
                        {
                            drr = dt_allrooms.Select("rom_code='" + dt_occupied.Rows[o]["rom_code"].ToString() + "' ");

                            if (drr.Length > 0)
                            {
                                drr[0].Delete();
                                dt_allrooms.AcceptChanges();
                            }
                        }
                    }
                }
                catch (Exception) { }
            }

            lbl_row.Text = dt_allrooms.Rows.Count.ToString();
            dgv_rom_available.DataSource = dt_allrooms;
        }

        private void dgv_rom_available_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_rom_available.SelectedRows.Count > 0)
                {
                    if (Convert.ToInt32(txt_noofnights.Text) < 0)
                    {
                        MessageBox.Show("No. of Nights must not less than zero.");
                    }
                    else
                    {
                        btn_cancel.Enabled = true;
                        //btn_save.Enabled = true;
                        btn_selectcust.Enabled = true;

                        grp_roominfo.Enabled = true;
                        grp_guest.Enabled = true;
                        grpbx_billing.Enabled = true;

                        //clr_field();
                        btn_cancel.Enabled = true;
                        //btn_save.Enabled = true;
                        btn_selectcust.Enabled = true;
                        grp_roominfo.Enabled = true;
                        grp_guest.Enabled = true;
                        grpbx_billing.Enabled = true;
                        int row = dgv_rom_available.CurrentCell.RowIndex;
                        MessageBox.Show("going to to romcode");
                        String rom_code = dgv_rom_available.Rows[row].Cells[0].Value.ToString().Trim();

                        MessageBox.Show("going to to romtyp");
                        String rom_typ = dgv_rom_available.Rows[row].Cells[2].Value.ToString().Trim();
                        //String rmttyp = dgv_rom_available.Rows[row].Cells[9].Value.ToString().Trim();
                        
                        lbl_rm.Text = rom_code;
                        lbl_rmtyp.Text = rom_typ;
                        MessageBox.Show("0");

                        lbl_arrdt.Text = dtp_checkin.Value.ToString("yyyy-MM-dd");
                        MessageBox.Show("1");
                        lbl_depdt.Text = dtp_checkout.Value.ToString("yyyy-MM-dd");
                        MessageBox.Show("2");

                        lbl_noofnight.Text = (Convert.ToDateTime(lbl_depdt.Text) - Convert.ToDateTime(lbl_arrdt.Text)).Days.ToString();
                        //lbl_noofnight.Text = lbl_noofnight_billing.Text;
                        lbl_noofnight_billing.Text = lbl_noofnight.Text;

                        cbo_rmrttyp.SelectedIndex = -1;
                        cbo_rtcode.SelectedIndex = -1;
                        cbo_disc.SelectedIndex = -1;
                        txt_discamt.Text = "0";
                        txt_netrt.Text = "0.00";
                        txt_govtrt.Text = "0.00";
                        //txt_srvccharge.Text = "0.00";
                        txt_total_amt.Text = "0.00";

                        tbcntrl_res.SelectedTab = tpg_reg;
                        tpg_reg.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Pls select the room.");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("error here " + er.Message);
            }

            pnl_left.Hide();
        }

        private void btnback_inhouse_Click(object sender, EventArgs e)
        {
            pnl_left.Hide();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            String cond = "";
            String join = "";

            if (txt_srch_gname.Text != "")
            {
                cond = " AND gf.full_name ILIKE '%" + txt_srch_gname.Text + "%'";
            }
            if (cbo_srch_company.SelectedIndex > -1)
            {
                cond = cond + " AND c.comp_code='" + cbo_srch_company.SelectedValue.ToString() + "'";
            }

            dgv_guestlist.DataSource = db.get_guest_currentlycheckin(cond);
            //dgv_guestlist.DataSource = db.QueryOnTableWithParams("guest", this.displist, cond, "ORDER BY full_name ASC");
        }

        private void txt_discamt_TextChanged(object sender, EventArgs e)
        {
            disp_computed_bill();
        }

        private void f_rom_Click(object sender, EventArgs e)
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

        private void dgv_guest_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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