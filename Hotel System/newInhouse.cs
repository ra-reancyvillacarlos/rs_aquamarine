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
    public partial class newInhouse : Form
    {
        String updated_guest = "";

        public newInhouse()
        {
            InitializeComponent();
            dis_dgvguest();
        }

        private void newInhouse_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            GlobalMethod gm = new GlobalMethod();
            //pnl_left.Hide();

            gm.load_romtype(cbo_type);
            //gm.load_company(cbo_srch_company);
            gm.load_agency(cbo_agency);
            gm.load_ratetype(cbo_rtcode);
            gm.load_market(cbo_mktsegment);
            gm.load_disctbl(cbo_disc);
            gm.load_romratetype(cbo_rmrttyp);
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show();
            dis_dgvguest();
            clr_field();
        }
        private void fildis_dgvguest(String search)
        {
            thisDatabase db = new thisDatabase();
            String gg = "AND gf.typ_code='"+search+"'";
            if (cbo_type.SelectedIndex == -1)
            {
                dgv_guestlist.DataSource = db.get_guest_currentlycheckin("");
            }
            else {
                dgv_guestlist.DataSource = db.get_guest_currentlycheckin(gg);
            
            }
        }
        private void dis_dgvguest()
        {
            thisDatabase db = new thisDatabase();

            dgv_guestlist.DataSource = db.get_guest_currentlycheckin("");
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
            cbo_agency.SelectedIndex = -1;
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
            try { dgv_guest.Rows.Clear(); }
            catch { }

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
            dgv_guestlist.DataSource = dt_allrooms;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txt_noofnights.Text) < 0)
            {
                MessageBox.Show("No. of Nights must not less than zero.");
            }
            else
            {
                if (cbo_type.SelectedIndex != -1)
                {
                    fildis_dgvguest(cbo_type.SelectedValue.ToString());
                }
                else {
                    dis_dgvguest();
                }
            }
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
                        //mainform.set_modname(modname + " > " + rmtrans.Name);
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
            tbcntrl_option.SelectedTab = tpg_opt_1;
            clr_field();
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbcntrl_option.SelectedTab = tpg_opt_1;
            clr_field();
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show();
        }

        private void btn_folio_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String curdate = DateTime.Today.ToString("yyyy-MM-dd");
            String curtime = DateTime.Now.ToString("HH:mm");
            String reg_num = lbl_gfno.Text;
            String dacct_no = dgv_guestlist.Rows[0].Cells[0].Value.ToString();
            String dfull_name = dgv_guestlist.Rows[0].Cells[1].Value.ToString();
            String rmtyp = db.get_romtyp_code(lbl_rmtyp.Text);
            String rmrttyp = "D";
            String disc = "";
            String disc_amt = "0.00";
            String guestno = "", guestfullname;
            Boolean checkGuest = false;
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

                    /*if (cbo_disc.SelectedIndex > -1 && (cbo_disc.SelectedValue ?? "").ToString() != "014")
                    {
                        disc = cbo_disc.SelectedValue.ToString();
                        disc_amt = db.get_discount(cbo_disc.SelectedValue.ToString()).ToString("0.00");
                    }
                    else
                    {
                        disc = "014";
                        disc_amt = txt_discamt.Text;
                    }*/
                    disc_amt = txt_discount_display.Text;

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
                    else if (cbo_agency.SelectedIndex == -1)
                    {
                        MessageBox.Show("Pls select travel agency.");
                    }
                    else
                    {
                        guestno = (dgv_guest.Rows[0].Cells[0].Value??"").ToString();
                        guestfullname = (dgv_guest.Rows[0].Cells[1].Value??"").ToString();
                        if (String.IsNullOrEmpty(guestno))
                        {
                            MessageBox.Show("Please select Tenant.");
                            checkGuest = true;
                        }
                        else 
                        {
                            if (guestno != updated_guest)
                            {
                                checkGuest = true;

                                if (MessageBox.Show("You changed the Tenant.\nAre you sure you want to Save this tenant?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                {
                                    checkGuest = false;
                                }
                            }
                        }

                        if (!checkGuest)
                        {
                            if (db.UpdateOnTable("gfolio", "occ_type='" + occ.ToString() + "', rate_code='" + cbo_rtcode.SelectedValue.ToString() + "', rom_rate='" + gm.toNormalDoubleFormat(txt_netrt.Text) + "', govt_tax='" + gm.toNormalDoubleFormat(txt_govtrt.Text) + "', mkt_code='" + cbo_mktsegment.SelectedValue.ToString() + "', trv_code='" + cbo_agency.SelectedValue.ToString() + "', free_bfast='" + gm.toNormalDoubleFormat(txt_discamt.Text) + "', remarks='" + rtb_remarks.Text + "', user_id='" + GlobalClass.username + "', t_date='" + Convert.ToDateTime(db.get_systemdate("")).ToString("yyyy-MM-dd") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "', disc_code='" + disc.ToString() + "', disc_pct='" + gm.toNormalDoubleFormat(disc_amt) + "', arr_date='" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', dep_date='" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', rom_code='" + lbl_rm.Text + "', typ_code='" + db.get_romtyp_code(lbl_rmtyp.Text) + "', rmrttyp='" + rmrttyp + "',acct_no='" + guestno + "',full_name='" + guestfullname + "'", "reg_num='" + reg_num + "'"))
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
                                String val_gfchg = "'" + chg_num + "', '" + lbl_gfno.Text + "', '" + dfull_name + "','" + Convert.ToDateTime(db.get_systemdate("")).ToString("yyyy-MM-dd") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', '" + f_rom.Text + "', '" + f_type.Text + "', '" + gm.toNormalDoubleFormat(f_rate.Text) + "', '" + lbl_rm.Text + "', '" + rmtyp + "', '" + cbo_rtcode.SelectedValue.ToString() + "', '" + rtb_remarks.Text + "', 'N', '" + GlobalClass.username + "'";

                                db.InsertOnTable("gfchange", col_gfchg, val_gfchg);
                                //increment rescode to m99
                                db.set_pkm99("chg_num", db.get_nextincrementlimitchar(chg_num, 8));

                                MessageBox.Show("Record(s) updated successfully.");
                                clr_field();

                                tbcntrl_option.SelectedTab = tpg_opt_1;
                                clr_field();
                                tbcntrl_res.SelectedTab = tpg_list;
                                tpg_list.Show();
                                dis_dgvguest();
                            }
                            else
                            {
                                MessageBox.Show("Problem occur on updating.");
                            }
                        }
                    }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
           
        }

        private void btn_reprint_regcard_Click(object sender, EventArgs e)
        {
            Report rpt = new Report("", "");
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            DataTable dt, dtg;
            Double noofnights = 0;
            DateTime arr_date = new DateTime();
            DateTime dep_date = new DateTime();
            String daddress = "", passport_no = "", passport_expiry = "", passport_issued = "", place_issue = "", dt_issue = "", remarks = "";


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
                    //daddress = db.get_addrcompmarket(row["acct_no"].ToString());
                    dtg = db.get_guest_info(row["acct_no"].ToString());

                    daddress = dtg.Rows[0]["address1"].ToString() + "; " + db.get_colval("country", "cntry_desc", "cntry_code='" + dtg.Rows[0]["cntry_code"].ToString() + "'") + "; " + db.get_colval("company", "comp_name", "comp_code='" + dtg.Rows[0]["comp_code"].ToString() + "'");
                    passport_no = dtg.Rows[0]["passport_no"].ToString();
                    if (!String.IsNullOrEmpty(passport_no))
                    {
                        passport_expiry = DateTime.Parse(dtg.Rows[0]["passport_expiry"].ToString()).ToString("yyyy-MM-dd");
                        place_issue = dtg.Rows[0]["passport_place"].ToString();
                        dt_issue = DateTime.Parse(dtg.Rows[0]["passport_issued"].ToString()).ToString("yyyy-MM-dd");
                    }

                    if (!String.IsNullOrEmpty(row["rm_features"].ToString()))
                        remarks += row["rm_features"].ToString();
                    if (!String.IsNullOrEmpty(row["bill_info"].ToString()))
                        remarks += row["bill_info"].ToString();
                    if (!String.IsNullOrEmpty(row["remarks"].ToString()))
                        remarks += row["remarks"].ToString();
                    if (!String.IsNullOrEmpty(row["nodeposit_rmrk"].ToString()))
                        remarks += row["nodeposit_rmrk"].ToString();

                    rpt.printprev_regform(row["reg_num"].ToString(), arr_date.ToString("yyyy-MM-dd"), row["arr_time"].ToString(), row["res_code"].ToString(), row["rom_code"].ToString(), row["rmrttyp"].ToString(), row["full_name"].ToString(), (gm.toNormalDoubleFormat(row["rom_rate"].ToString()) + gm.toNormalDoubleFormat(row["govt_tax"].ToString())).ToString("0.00"), row["occ_type"].ToString(), db.get_romtyp_desc(row["typ_code"].ToString()), daddress, arr_date.ToString("yyyy-MM-dd"), row["arr_time"].ToString(), dep_date.ToString("yyyy-MM-dd"), row["dep_time"].ToString(), noofnights.ToString("0.00"), remarks, row["paymentform"].ToString(), row["doc_ref"].ToString(), row["doctype"].ToString(), row["dep_amnt"].ToString(), row["user_id"].ToString(), "", "", passport_no, passport_expiry, place_issue, dt_issue, db.get_colval("nationality", "nat_desc", "nat_code='" + dtg.Rows[0]["nat_code"].ToString() + "'"), dtg.Rows[0]["tel_num"].ToString(), dtg.Rows[0]["email"].ToString(),
                        row["ap_paymentform"].ToString(), row["ap_doc_ref"].ToString(), row["ap_dep_amnt"].ToString());
                    //+ gm.toNormalDoubleFormat(row["serv_chg"].ToString())
                }

                rpt.Show();
            }
            tbcntrl_option.SelectedTab = tpg_opt_1;
            clr_field();
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show();
        }

	

        public void reset_modname()
        {
            tbcntrl_option.SelectedTab = tpg_opt_1;
            clr_field();
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show();
            dis_dgvguest();
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

                    if (cbo_disc.SelectedIndex != -1)
                    {
                        disc_code = cbo_disc.SelectedValue.ToString();
                    }

                    // lbl_arrdt.Text changed to datetoday by: Reancy 06 02 2018
                    rmtrans.set_data(1, lbl_gfno.Text, dgv_guest[1, 0].Value.ToString(), lbl_rm.Text, DateTime.Now.ToString("yyyy-MM-dd"), lbl_depdt.Text, lbl_rmtyp.Text, txt_rmrate.Text, cbo_rtcode.SelectedValue.ToString(), disc_code, cbo_occtyp.Text.ToString());

                    rmtrans.MdiParent = this.MdiParent;

                    rmtrans.Show();
                }
            }
            catch
            {

            }
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
            String acct_no = "";
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
                        // lbl_clerk.Text = r["user_id"].ToString();

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
                        cbo_agency.SelectedValue = r["trv_code"].ToString();
                        cbo_disc.SelectedValue = r["disc_code"].ToString();

                        rtb_remarks.Text = r["remarks"].ToString() + " " + r["rm_features"].ToString() + " " + r["bill_info"].ToString();
                        cbo_disc.SelectedValue = r["disc_code"].ToString();
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
                    updated_guest = guestno;
                    dgv_guest.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());
                    //dt_otherguestid = db.get_resguest_id(reg_num); 
                    dt_otherguestid = db.QueryBySQLCode("SELECT * FROM rssys.gfguest WHERE reg_num='" + reg_num + "'");

                    acct_no = dt_guest.Rows[0]["acct_no"].ToString();
                    dt_guest.Rows.Clear();

                    foreach (DataRow rowid in dt_otherguestid.Rows)
                    {
                        if (rowid["acct_no"].ToString() != acct_no)
                        {
                            dt_guest = db.get_guest_info(rowid["acct_no"].ToString());
                            dgv_guest.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());
                            dt_guest.Rows.Clear();
                        }
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
            Double disc_amount = 0.00;
            Double db_rmgovt = 0.00;
            Double net_rate = 0.00;
            Double vat = 0.00;
            Double roundoff_amt;

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

                if (cbo_disc.SelectedIndex != -1)
                {
                    if (cbo_disc.SelectedValue.ToString() != "014")
                    {
                        lessdiscount = db.get_discount(cbo_disc.SelectedValue.ToString());
                        issenior_disc = db.issenior_disc(cbo_disc.SelectedValue.ToString());

                        txt_discount_display.Text = gm.toAccountingFormat((lessdiscount / 100) * grossamt);
                    }
                   
                }

                if (String.IsNullOrEmpty(txt_discamt.Text) ||txt_discamt.Text == "0.00")
                {
                    txt_discamt.Text = "0.00";
                }
                else if (gm.toNormalDoubleFormat(txt_discamt.Text) != 0.00|| !String.IsNullOrEmpty(txt_discamt.Text))
               {
                    lessdisc_amt = gm.toNormalDoubleFormat(txt_discamt.Text);
                    txt_discount_display.Text = gm.toAccountingFormat(lessdisc_amt);/*gm.toAccountingFormat(grossamt - lessdisc_amt)*/;
                }



                grossamt = db.get_roomrateamt(cbo_rtcode.SelectedValue.ToString(), db.get_romtyp_code(lbl_rmtyp.Text), occ);

                if (cbo_disc.SelectedIndex != -1 && (cbo_disc.SelectedValue??"").ToString() != "014")
                {
                    disc_amount = grossamt * (gm.toNormalDoubleFormat(db.get_colval("disctbl", "disc1", "disc_code='" + (cbo_disc.SelectedValue ?? "").ToString() + "'")) / 100);
                }
                else 
                {
                    disc_amount = lessdisc_amt;
                }


                txt_grossrt.Text = grossamt.ToString("0.00");
                //String discoun = (grossamt * (gm.toNormalDoubleFormat(cbo_disc.SelectedValue.ToString()) / 100)).ToString();
                net_rate = (grossamt - disc_amount) / 1.12;
                vat = (grossamt - disc_amount) - net_rate;
                txt_discount_display.Text = gm.toAccountingFormat(disc_amount);
                txt_netrt.Text = net_rate.ToString("0.00");/*db.get_netrate(grossamt, lessdiscount, lessdisc_amt).ToString("0.00")*/
                txt_govtrt.Text = gm.toAccountingFormat(vat); /*db.get_tax(grossamt, lessdiscount, lessdisc_amt).ToString("0.00")*/
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

                roundoff_amt = Convert.ToDouble(Math.Round(Convert.ToDecimal(db_rmrate + db_rmgovt), 1));

                txt_rmrate.Text = gm.toAccountingFormat(roundoff_amt);

                if (cbo_rmrttyp.SelectedValue == "M")
                {

                }

                txt_total_amt.Text = gm.toAccountingFormat((Convert.ToDouble(txt_rmrate.Text) * Convert.ToDouble(lbl_noofnight_billing.Text)));
            }
            catch (Exception)
            { }
        }
        private void disp_chgfil(String reg_num)
        {
            thisDatabase db = new thisDatabase();
            dgv_gfolio.DataSource = db.get_guest_chargefil(reg_num, false);
        }

        private void dgv_guestlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgv_guestlist.CurrentRow.Index;
                if (dgv_guestlist.Rows.Count > 0)
                {
                    String reg_num = dgv_guestlist[5, r].Value.ToString();

                    disp_guestinfo(reg_num);
                    disp_chgfil(reg_num);
                }
            }
            catch (Exception) { }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            tbcntrl_option.SelectedTab = tpg_opt_1;
            clr_field();
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            int r = dgv_guestlist.CurrentRow.Index;
            if (r != -1)
            {
                tbcntrl_option.SelectedTab = tpg_opt_2;
                clr_field();
                tbcntrl_res.SelectedTab = tpg_reg;
                tpg_reg.Show();
                try
                {

                    if (dgv_guestlist.Rows.Count > 0)
                    {
                        String reg_num = dgv_guestlist[5, r].Value.ToString();

                        disp_guestinfo(reg_num);
                        disp_chgfil(reg_num);
                        lbl_rm.Enabled = false;
                    }
                }
                catch (Exception er) { MessageBox.Show(er.Message); }
            }
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

        private void btn_selectcust_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalMethod gmethod = new GlobalMethod();
                addGuest AG = new addGuest(null, null, this);
                GlobalClass.gdgv = gmethod.CopyDataGridView(dgv_guest);



                AG.MdiParent = this.MdiParent;
                AG.reload_guest();
                AG.Show();

            }
            catch (Exception) { }
        }
        public void reload_guest()
        {
            DataGridViewRow row = new DataGridViewRow();

            if (GlobalClass.gdgv != null)
            {
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

            GlobalClass.gdgv = null;
        }

        private void tpg_list_Click(object sender, EventArgs e)
        {

        }

        private void cbo_rtcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String rmrttyp;
            Double night = 0, mo = 0, wk = 0, val = 0;
            String room_rate_code = "";
            try
            {
                if (cbo_rtcode.SelectedIndex > -1)
                {
                    DataTable dt = db.QueryBySQLCode("SELECT rmrttyp from rssys.ratetype WHERE rate_code='" + cbo_rtcode.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {

                        room_rate_code = dt.Rows[0]["rmrttyp"].ToString();
                    }
                    cbo_rmrttyp.SelectedValue = db.get_romratetype_code(room_rate_code);
                    rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
                    if (night % 30 > 0) { mo = mo + 1.00; }
                    if (rmrttyp == "M")
                    {

                        lbl_noofnight_title_top.Text = "No. of Month(s)";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        night = Convert.ToDouble(lbl_noofnight.Text);

                        if (night > 30)
                        {
                            mo = night / 30;


                        }

                        else
                        {
                            mo = 1.00;
                        }

                        lbl_noofnight.Text = mo.ToString("0");
                        lbl_noofnight_billing.Text = lbl_noofnight.Text;
                    }
                    else if (rmrttyp == "W")
                    {
                        lbl_noofnight_title_top.Text = "No. of Week(s)";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        night = Convert.ToDouble(lbl_noofnight.Text);
                        if (night % 7 > 0) { wk = wk + 1.00; }
                        if (night > 7)
                        {
                            wk = night / 7;


                        }

                        else
                        {
                            wk = 1.00;
                        }

                        lbl_noofnight.Text = wk.ToString("0");
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

        private void lbl_rm_Click(object sender, EventArgs e)
        {
            z_Research frm = new z_Research(this, "room", lbl_arrdt.Text, lbl_depdt.Text);
            frm.ShowDialog();
        }

        private void cbo_rmrttyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbo_rmrttyp.SelectedValue.ToString() == "M")
            //{
            //    btn_print_contract.Enabled = true;
            //    btn_reprint_regcard.Enabled = false;
            //}
            //else {
            //    btn_print_contract.Enabled = false;
            //    btn_reprint_regcard.Enabled = true;
            //}
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

        private void btn_edit_departure_Click(object sender, EventArgs e)
        {
            //z_Research frm = new z_Research(this, "departure", lbl_arrdt.Text, lbl_depdt.Text);
            z_departure frm = new z_departure(this, lbl_arrdt.Text, lbl_depdt.Text);
            frm.ShowDialog();

        }

        private void btn_print_contract_Click(object sender, EventArgs e)
        {

            Report rpt = new Report("", "");
            rpt.Show();
            rpt.print_contract(lbl_gfno.Text);
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

        private void dgv_guestlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lbl_depdt_TextChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String rmrttyp;
            Double night = 0, mo = 0, wk = 0, val = 0;
            String room_rate_code = "";
            try
            {
                if (cbo_rtcode.SelectedIndex > -1)
                {
                    DataTable dt = db.QueryBySQLCode("SELECT rmrttyp from rssys.ratetype WHERE rate_code='" + cbo_rtcode.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {

                        room_rate_code = dt.Rows[0]["rmrttyp"].ToString();
                    }
                    cbo_rmrttyp.SelectedValue = db.get_romratetype_code(room_rate_code);
                    rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
                    if (night % 30 > 0) { mo = mo + 1.00; }
                    if (rmrttyp == "M")
                    {

                        lbl_noofnight_title_top.Text = "No. of Month(s)";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        night = Convert.ToDouble(lbl_noofnight.Text);

                        if (night > 30)
                        {
                            mo = night / 30;


                        }

                        else
                        {
                            mo = 1.00;
                        }

                        lbl_noofnight.Text = mo.ToString("0");
                        lbl_noofnight_billing.Text = lbl_noofnight.Text;
                    }
                    else if (rmrttyp == "W")
                    {
                        lbl_noofnight_title_top.Text = "No. of Week(s)";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        night = Convert.ToDouble(lbl_noofnight.Text);
                        if (night % 7 > 0) { wk = wk + 1.00; }
                        if (night > 7)
                        {
                            wk = night / 7;


                        }

                        else
                        {
                            wk = 1.00;
                        }

                        lbl_noofnight.Text = wk.ToString("0");
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

        private void lbl_arrdt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_discamt_TextChanged(object sender, EventArgs e)
        {
            if (txt_discamt.Text != "0.00")
            {
                //txt_discount_display.Text = txt_discamt.Text;
                disp_computed_bill();
            }
            else {

                txt_discount_display.Text = "0";
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dis_dgvguest();
        }
    }
}
