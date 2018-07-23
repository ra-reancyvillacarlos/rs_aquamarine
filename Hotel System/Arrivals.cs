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
    public partial class Arrivals : Form
    {
        Main mainform;
        enterDeposit frmdeposit;
        String schema;
        String t_date;
        Boolean isnew = true;
        Boolean isarrival = true;
        String modname = "";
        public String active_res_where;
        public Arrivals(Main m)
        {
            InitializeComponent();
            mainform = m;
            modname = mainform.get_modname();

            txt_discamt.Enabled = false;
        }
        public Arrivals()
        {
            InitializeComponent();
             
            

            txt_discamt.Enabled = false;
        }
        private void Arrivals_Load(object sender, EventArgs e)
        {
            //label12.Text = "For Arrival Guest, search " + Environment.NewLine + " and select the guest";

            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();

            this.WindowState = FormWindowState.Maximized;

            schema = db.get_schema();
            t_date = db.get_systemdate("");
            dtp_checkin.Value = DateTime.Today;
            dtp_checkout.Value = DateTime.Today;
            //dtp_srch_dt.Value = Convert.ToDateTime(db.get_systemdate(""));
            active_res_where = " AND r.arr_date >= '" + db.get_systemdate("") + "' AND (r.arrived IS NULL OR r.arrived !='Y') AND (r.cancel IS NULL OR r.cancel !='Y')";



            set_reslist(active_res_where);
            //set_arrlist("");
            gm.load_company(cbo_srchcomp);
            gm.load_ratetype(cbo_rtcode);
            gm.load_market(cbo_mktsegment);
            //set_rmtypecbo();
            gm.load_disctbl(cbo_disc);
            gm.load_romratetype(cbo_rmrttyp);

            set_roomstatuslist();

            cbo_type.SelectedIndex = -1;
            clr_field();
            lbl_clerk.Text = GlobalClass.username;
        }

        public void reset_modname()
        {
            //mainform.set_modname(modname);
        }
        public void set_reslist(String search_code)
        {
            try
            {
                thisDatabase db = new thisDatabase();

                dgv_reslist.DataSource = db.get_reservationlist(search_code);
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }
        private void set_arrlist(String search)
        {
            try
            {
                thisDatabase db = new thisDatabase();
                DataTable dt = new DataTable();

                String schema = db.get_schema();
                //dgv_reslist.DataSource = db.QueryOnTableWithParams("resfil", "res_code, full_name, rom_code, typ_code, arr_date, dep_date, user_id, t_date, t_time", "cancel IS NULL AND arr_date > '2013-01-29' " + search, "ORDER BY res_code ASC");
                //dgv_reslist.DataSource = db.QueryBySQLCode("SELECT r.res_code AS \"Res Code\", r.full_name AS Guest, r.rom_code AS Room, r.typ_code AS Type, r.arr_date AS Arrival, r.dep_date AS Departure, r.user_id AS \"Check In By\", r.t_date AS \"Res Date\", r.t_time AS \"Res Time\", r.blockby AS \"Blocked By\" FROM " + schema + ".resfil r LEFT JOIN " + schema + ".guest g ON r.acct_no=g.acct_no WHERE r.arrived IS NULL AND (r.cancel IS NULL OR r.cancel='') AND r.arr_date = '"+db.get_systemdate()+"'" + search + " ORDER BY r.full_name ASC");
                dt = db.get_arrivallist(search, dtp_srch_dt.Value.ToString("yyyy-MM-dd"));
                //dgv_reslist.DataSource = dt;
                dgv_reslist.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv_reslist.Rows[0].Clone();

                    row.Cells[0].Value = dt.Rows[i][0].ToString();
                    row.Cells[1].Value = dt.Rows[i][1].ToString();
                    row.Cells[2].Value = dt.Rows[i][2].ToString();
                    row.Cells[3].Value = dt.Rows[i][3].ToString();
                    row.Cells[4].Value = Convert.ToDateTime(dt.Rows[i][4].ToString()).ToString("MM/dd/yyyy") + " " + Convert.ToDateTime(dt.Rows[i][5].ToString()).ToString("HH:mm");
                    row.Cells[5].Value = Convert.ToDateTime(dt.Rows[i][6].ToString()).ToString("MM/dd/yyyy") + " " + Convert.ToDateTime(dt.Rows[i][7].ToString()).ToString("HH:mm");
                    row.Cells[6].Value = dt.Rows[i][8].ToString();
                    row.Cells[7].Value = dt.Rows[i][9].ToString();
                    row.Cells[8].Value = dt.Rows[i][10].ToString();
                    row.Cells[9].Value = dt.Rows[i][11].ToString();
                    row.Cells[10].Value = dt.Rows[i][12].ToString();
                    row.Cells[11].Value = dt.Rows[i][13].ToString();

                    if (String.IsNullOrEmpty(dt.Rows[i][12].ToString()))
                    {
                        //no color if empty block by column
                        row.Cells[0].Style.ForeColor = Color.Black;
                        row.Cells[1].Style.ForeColor = Color.Black;
                        row.Cells[2].Style.ForeColor = Color.Black;
                        row.Cells[3].Style.ForeColor = Color.Black;
                        row.Cells[4].Style.ForeColor = Color.Black;
                        row.Cells[5].Style.ForeColor = Color.Black;
                        row.Cells[6].Style.ForeColor = Color.Black;
                        row.Cells[7].Style.ForeColor = Color.Black;
                        row.Cells[8].Style.ForeColor = Color.Black;
                        row.Cells[9].Style.ForeColor = Color.Black;
                        row.Cells[10].Style.ForeColor = Color.Black;
                        dgv_reslist.Rows.Add(row);
                    }
                    else
                    {
                        row.Cells[0].Style.ForeColor = Color.Red;
                        row.Cells[1].Style.ForeColor = Color.Red;
                        row.Cells[2].Style.ForeColor = Color.Red;
                        row.Cells[3].Style.ForeColor = Color.Red;
                        row.Cells[4].Style.ForeColor = Color.Red;
                        row.Cells[5].Style.ForeColor = Color.Red;
                        row.Cells[6].Style.ForeColor = Color.Red;
                        row.Cells[7].Style.ForeColor = Color.Red;
                        row.Cells[8].Style.ForeColor = Color.Red;
                        row.Cells[9].Style.ForeColor = Color.Red;
                        row.Cells[10].Style.ForeColor = Color.Black;
                        dgv_reslist.Rows.Add(row);
                        //color if block by column is not empty
                         //dgv_reslist.Rows[i] = (DataRowCollection)dt.Rows[i];                      
                    }
                }

                lbl_row_list.Text = dt.Rows.Count.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void set_rmtypecbo()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("rtype", "typ_code, typ_desc", "", "ORDER BY typ_code ASC;");

                //cbo_rmtype.DataSource = dt;
                /// cbo_rmtype.DisplayMember = "typ_desc";
                //cbo_rmtype.ValueMember = "typ_code";

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void set_roomstatuslist()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                dt.Rows.Add();
                dt.AcceptChanges();

                dt = db.get_roomtypExptZ();

                DataRow newRow = dt.NewRow();

                newRow["typ_code"] = "*";
                newRow["typ_desc"] = "";

                dt.Rows.Add(newRow);

                cbo_type.DataSource = dt;
                cbo_type.DisplayMember = "typ_desc";
                cbo_type.ValueMember = "typ_code";
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void load_rom_available()
        {
            String typ_code = "";
            DataTable dt_allrooms = new DataTable();
            //DataTable dt_allrom2 = new DataTable();
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

            dt_allrooms = db.get_allroomWithZ(typ_code, null, null); 
            //dt_allrooms = db.get_allroomExpZ(typ_code, null, null);
            //dt_allrom2 = db.get_allroomExpZ(typ_code);
            dt_reserved = db.get_reservedroomExptZ(dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), typ_code);
            dt_occupied = db.get_occupancyExptZ("rom_code, res_code, full_name, arr_date, dep_date", dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), typ_code);

            DataRow[] drr;

            //dgv_rom.DataSource = dt_allrom2;

            lbl_rom.Text = dt_allrooms.Rows.Count.ToString();
            lbl_res.Text = dt_reserved.Rows.Count.ToString();
            lbl_occ.Text = dt_occupied.Rows.Count.ToString();

            //remove reserved
            if (dt_reserved.Rows.Count > 0)
            {
                try
                {
                    //dgv_res.DataSource = dt_reserved;

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
                    //dgv_occ.DataSource = dt_occupied;

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

        private void clr_field()
        {
            lbl_resno.Text = "";
            lbl_arrdt.Text = "";
            lbl_noofnight.Text = "0";
            lbl_rm.Text = "";
            lbl_rmtyp.Text = "";
            lbl_noofnight_billing.Text = "0";
            lbl_noofguest.Text = "0";
            lbl_depdt.Text = "";
            lbl_blockedby.Text = "";

            chk_blockres.Checked = false;

            cbo_srchcomp.SelectedIndex = -1;
            cbo_disc.SelectedIndex = -1;
            cbo_mktsegment.SelectedIndex = -1;
            cbo_rtcode.SelectedIndex = -1;
            cbo_occtyp.SelectedIndex = -1;
            cbo_type.SelectedIndex = -1;

            txt_contact.Text = "";
            //txt_fbkfast.Text = "0";
            txt_govtrt.Text = "0.00";
            txt_netrt.Text = "0.00";
            txt_rmrate.Text = "0.00";
            txt_total_amt.Text = "0.00";
            txt_resby.Text = "";
            rtb_remarks.Text = "";

            if (dgv_guestlist.Rows.Count > 1)
            {
                for (int i = 0; dgv_guestlist.Rows.Count > i + 1; i++)
                {
                    dgv_guestlist.Rows.RemoveAt(i);
                }
            }
            btn_cancel.Enabled = false;
            btn_save.Enabled = false;
            btn_selectcust.Enabled = false;

            grp_roominfo.Enabled = false;
            grp_guest.Enabled = false;
            grpbx_billing.Enabled = false;
        }

        private void btn_search_Click(object sender, EventArgs e)
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

        private void sel_arrival_chkin(int RowIndex)
        {
            try
            {
                if (dgv_reslist.SelectedRows.Count > 0)
                {
                    String res_code = dgv_reslist[0, RowIndex].Value.ToString();

                    disp_arrival(res_code);
                }
                else
                {
                    MessageBox.Show("Pls select the room.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Pls select the arrival guest to check in.");
            }
        }

        private void dgv_reslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sel_arrival_chkin(e.RowIndex);
        }

        private void load_arrival_data(String code)
        {
            thisDatabase db = new thisDatabase();

            db.QueryBySQLCode("");
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
                        btn_save.Enabled = true;
                        btn_selectcust.Enabled = true;

                        grp_roominfo.Enabled = true;
                        grp_guest.Enabled = true;
                        grpbx_billing.Enabled = true;
                        
                        //clr_field();
                        btn_cancel.Enabled = true;
                        btn_save.Enabled = true;
                        btn_selectcust.Enabled = true;
                        grp_roominfo.Enabled = true;
                        grp_guest.Enabled = true;
                        grpbx_billing.Enabled = true;
                        int row = dgv_rom_available.CurrentCell.RowIndex;

                        String rom_code = dgv_rom_available.Rows[row].Cells[0].Value.ToString().Trim();
                        String rom_typ = dgv_rom_available.Rows[row].Cells[2].Value.ToString().Trim();

                        lbl_rm.Text = rom_code;
                        lbl_rmtyp.Text = rom_typ;

                        lbl_arrdt.Text = dtp_checkin.Value.ToString("yyyy-MM-dd");
                        lbl_depdt.Text = dtp_checkout.Value.ToString("yyyy-MM-dd");
                        lbl_noofnight.Text = txt_noofnights.Text;
                        lbl_noofnight_billing.Text = txt_noofnights.Text;

                        cbo_rtcode.SelectedIndex = -1;
                        cbo_disc.SelectedIndex = -1;
                        //txt_fbkfast.Text = "0";
                        txt_netrt.Text = "0.00";
                        txt_govtrt.Text = "0.00";
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
                //MessageBox.Show(er.Message);
            }
        }

        private void btn_selectcust_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    GlobalMethod gmethod = new GlobalMethod();
            //    addGuest AG = new addGuest(null, this, null);
            //    GlobalClass.gdgv = gmethod.CopyDataGridView(dgv_guestlist);

            //    //mainform.set_modname(modname + " > " + "Select Guest");

            //    //AG.MdiParent = this.MdiParent;
            //    AG.reload_guest();
            //    AG.ShowDialog();

            //}
            //catch (Exception) { }
        }

        public void reload_guest()
        {
            DataGridViewRow row = new DataGridViewRow();

            if (GlobalClass.gdgv != null)
            {
                dgv_guestlist.Rows.Clear();

                for (int i = 0; i < GlobalClass.gdgv.Rows.Count - 1; i++)
                {
                    row = (DataGridViewRow)GlobalClass.gdgv.Rows[i].Clone();

                    int intColIndex = 0;

                    foreach (DataGridViewCell cell in GlobalClass.gdgv.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_guestlist.Rows.Add(row);
                }
            }

            lbl_noofguest.Text = (dgv_guestlist.Rows.Count - 1).ToString();

            reset_modname();
            GlobalClass.gdgv = null;
        }

        public String get_room()
        {
            return lbl_rm.Text;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String reg_num = "";
            frmdeposit = new enterDeposit(this);

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
                MessageBox.Show("Pls select the room the you are going to reserve.");
            }
            else if (db.get_romstatus(lbl_rm.Text) == "OCC")
            {
                MessageBox.Show("Room still occupied");
            }
            else if (db.get_romstatus(lbl_rm.Text) == "VD")
            {
                MessageBox.Show("Room still vacant dirty");
            }
            else if (db.get_romstatus(lbl_rm.Text) == "OOO")
            {
                MessageBox.Show("Room is out of order.");
            }
            else
            {
                // added by: Reancy 05 21 2018
                DateTime arrdt_td = Convert.ToDateTime(lbl_arrdt.Text);
                DateTime depdt_td = Convert.ToDateTime(lbl_depdt.Text);
                String arrdt_dd = arrdt_td.ToString("yyyy-MM-dd");
                String depdt_dd = depdt_td.ToString("yyyy-MM-dd");
                if (isarrival == true && db.is_roomreservedBythisRescode(lbl_rm.Text, lbl_resno.Text, "", "") == true)
                {
                            MessageBox.Show("Room already reserved.");
                }
                // removed by: Reancy 05 21 2018
                //else if(isarrival == false && db.is_roomreserved(lbl_rm.Text))
                //{
                //            MessageBox.Show("Room already reserved.");
                //}
                else
                {
                   //frmdeposit.ShowDialog();
                    //string[] data = Prompt.ShowDialogDeposit(db.QueryOnTableWithParams("charge  ", "chg_code, chg_desc", "chg_type='P'", "ORDER BY chg_code ASC;"), panel1.BackColor);

                    frmdeposit.ShowDialog();

                    /*
                    if (data != null)
                    {
                        if (isarrival == true)
                        {
                            reg_num = chkin_arrival(data);                           
                        }
                        else
                        {
                            reg_num = chkin_walkin(data);
                        }

                        set_arrlist("");

                        load_rom_available();

                        pnl_leftfirst.Show();
                        tbcntrl_res.SelectedTab = tpg_list;
                        tpg_list.Show();
                    }
                    else
                    {
                        MessageBox.Show("Transaction cannot be saved. No deposit entered.");
                    }
                     * */
                }
            }            
        }

        public void guest_checkInSave()
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String curdate = DateTime.Today.ToString("yyyy-MM-dd");
            String curtime = DateTime.Now.ToString("HH:mm");
            String res_folio_code = db.get_pk("resv_fol");

            String reg_num = db.get_pk("reg_num");
            String dacct_no = dgv_guestlist.Rows[0].Cells[0].Value.ToString();
            String dfull_name = dgv_guestlist.Rows[0].Cells[1].Value.ToString();
            String daddress = "";
            String rmtyp = db.get_romtyp_code(lbl_rmtyp.Text);
            String rmrttyp = "D";

            int occ = 1;

            //advance payment
            String ap_chg = frmdeposit.get_ap_paymentform_code();
            String ap_doc = "NA";
            String ap_doc_ref = frmdeposit.get_ap_reference();
            String ap_currency = "PHP";
            String ap_dep_amt = frmdeposit.get_ap_amount();
            String ap_chg_name = frmdeposit.get_ap_paymentform_name();
            String ap_crdno = frmdeposit.get_ap_ccnumber();
            String ap_traceno = frmdeposit.get_ap_tracenumber();
            String ap_nodep_reason = frmdeposit.get_ap_reasonForNoPayment();
            String disc_code = "";

            //deposit
            String d_chg = frmdeposit.get_d_paymentform_code();
            String d_doc = "NA";
            String d_doc_ref = frmdeposit.get_d_reference();
            String d_currency = frmdeposit.get_currency_code();
            String d_dep_amt = frmdeposit.get_d_amount();
            String d_chg_name = frmdeposit.get_d_paymentform_name();
            String d_crdno = frmdeposit.get_d_ccnumber();
            String d_traceno = frmdeposit.get_d_tracenumber();
            String d_nodep_reason = frmdeposit.get_d_reasonForNoPayment();

            String lromtypduration = "", ldurtyp = "";
            String lpassportno = "", lissuedplace = "", lpassportvaldate = "", lcontactno = "", lemail = "", lnationality = "";

            Double vat_included = 0.00;
            Double sc_included = 0.00;
            Double lessdiscount = 0.00; // less discount percentage
            Double lessdisc_amt = 0.00; // less discount amount

            if (cbo_rmrttyp.SelectedIndex > -1)
            {
                rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
            }

            if (cbo_rmrttyp.SelectedValue.ToString() == "M")
            {
                lromtypduration = "Month(s)";
                ldurtyp = "Monthly";
            }
            else if (cbo_rmrttyp.SelectedValue.ToString() == "Y")
            {
                lromtypduration = "Year(s)";
                ldurtyp = "Yearly";
            }
            else
            {
                lromtypduration = "Day(s)";
                ldurtyp = "Daily";
            }

            if (cbo_disc.SelectedIndex > -1)
            {
                disc_code = cbo_disc.SelectedValue.ToString();
                lessdiscount = gm.toNormalDoubleFormat(db.get_discount(cbo_disc.SelectedValue.ToString()).ToString());
            }

            if (ap_nodep_reason != "")
            {
                ap_chg = "";
                ap_doc = "";
                ap_doc_ref = "";
                ap_dep_amt = "0.00";
                ap_chg_name = "";
                ap_crdno = "";
                ap_traceno = "";
                ap_nodep_reason = "";
            }
            if (ap_chg == "102" || ap_chg == "103" || ap_chg == "104" || ap_chg == "105")
            {
                ap_doc_ref = ap_crdno + "-" + ap_traceno;
            }

            if (db.has_vat(ap_chg))
            {
                vat_included = db.get_tax(Convert.ToDouble(ap_dep_amt), lessdiscount, lessdisc_amt);
            }
            if (db.has_sc(d_chg))
            {
                sc_included = db.get_svccharge(Convert.ToDouble(ap_dep_amt), lessdiscount, lessdisc_amt);
            }

            if (d_nodep_reason != "")
            {
                d_chg = "";
                d_doc = "";
                d_doc_ref = "";
                d_dep_amt = "0.00";
                d_chg_name = "";
                d_crdno = "";
                d_traceno = "";
                d_nodep_reason = "";
            }

            if (d_chg == "102" || d_chg == "103" || d_chg == "104" || d_chg == "105")
            {
                d_doc_ref = d_crdno + "-" + d_traceno;
            }

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
            
            String col = "reg_num, acct_no, full_name, reg_date, rmrttyp, arr_date, arr_time, dep_date, dep_time, rom_code, typ_code, occ_type, rate_code, rom_rate, govt_tax, extra_bed, pay_code, mkt_code, src_code, trv_code, free_bfast, rm_features, bill_info, remarks, rom_class, user_id, t_date, t_time, out_fol, fctr_code, disc_code, disc_pct, paymentform, doc_ref, doctype, dep_amnt, nodeposit_rmrk, ap_paymentform, ap_doc_ref, ap_doctype, ap_dep_amnt, ap_nodeposit_rmrk";
            String val = "'" + reg_num + "', '" + dacct_no + "', '" + dfull_name + "','" + curdate + "', '" + rmrttyp + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_arrtime.Value.ToString("HH:mm") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_deptime.Value.ToString("HH:mm") + "', '" + lbl_rm.Text + "', '" + rmtyp + "', '" + occ.ToString() + "', '" + cbo_rtcode.SelectedValue.ToString() + "', '" + gm.toNormalDoubleFormat(txt_netrt.Text) + "', '" + gm.toNormalDoubleFormat(txt_govtrt.Text) + "', '0.00', '" + "', '" + cbo_mktsegment.SelectedValue.ToString() + "', '', '000001','0', '', '','" + rtb_remarks.Text + "', 'H', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "','N','', '" + disc_code.ToString() + "', '" + lessdiscount.ToString() + "', '" + d_chg_name + "', '" + d_doc_ref + "', '" + d_doc + "', '" + d_dep_amt + "', '" + d_nodep_reason + "', '" + ap_chg_name + "', '" + ap_doc_ref + "', '" + ap_doc + "', '" + ap_dep_amt + "', '" + ap_nodep_reason + "'";

            if (db.InsertOnTable("gfolio", col, val))
            {
                daddress = db.get_addrcompmarket(dacct_no.ToString());

                //update resfil that arrived=Y
                if (String.IsNullOrEmpty(lbl_resno.Text) == false)
                {
                    db.UpdateOnTable("resfil", "arrived='Y'", "res_code='" + lbl_resno.Text + "'");
                    db.UpdateOnTable("chgfil", "reg_num='"+reg_num+"'", "reg_num='"+res_folio_code+"' AND res_code='" + lbl_resno.Text + "'");
                }

                //insert other guests
                for (int r = 0; dgv_guestlist.Rows.Count - 1 > r; r++)
                {
                    dacct_no = dgv_guestlist.Rows[r].Cells[0].Value.ToString();
                    dfull_name = dgv_guestlist.Rows[r].Cells[1].Value.ToString();

                    db.InsertOnTable("gfguest", "reg_num, acct_no, full_name, arr_date, dep_date", "'" + reg_num + "', '" + dacct_no + "', '" + dfull_name + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "'");
                }

                //increment rescode to m99
                db.set_pkm99("reg_num", db.get_nextincrementlimitchar(reg_num, 8));

                //advance payment
                if (Convert.ToDouble(ap_dep_amt) > 0.00)
                {
                    db.insert_charges(reg_num, dfull_name, ap_chg, lbl_rm.Text, ap_doc_ref, gm.toNormalDoubleFormat(ap_dep_amt), lbl_arrdt.Text, "", "", lbl_resno.Text, "", ap_doc, vat_included, sc_included, 0.00, ap_traceno, ap_crdno, ap_currency, true);
                }

                //deposit
                if (Convert.ToDouble(d_dep_amt) > 0.00)
                {
                    db.insert_charges(reg_num, dfull_name, d_chg, lbl_rm.Text, d_doc_ref, gm.toNormalDoubleFormat(d_dep_amt), lbl_arrdt.Text, "", "", lbl_resno.Text, "", d_doc, 0.00, 0.00, 0.00, d_traceno, d_crdno, d_currency, true);
                }

                if (get_room() != "Z01")
                {
                    UpdateRoomStatus urs = new UpdateRoomStatus();

                    urs.set_rmstatus("OCC", lbl_rm.Text);

                    //Report rpt = new Report("", "");

                    //rpt.Show();
                    //rpt.printprev_regform(reg_num, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm"), lbl_resno.Text, lbl_rm.Text, dfull_name, txt_rmrate.Text, occ.ToString(), db.get_romtyp_code(lbl_rmtyp.Text), daddress, lbl_arrdt.Text, dtp_arrtime.Value.ToString("HH:mm"), lbl_depdt.Text, dtp_deptime.Value.ToString("HH:mm"), lbl_noofnight.Text, rtb_remarks.Text + "" + d_nodep_reason + "\n" + ap_nodep_reason, d_chg_name, d_doc_ref, d_doc, d_currency + " " + gm.toAccountingFormat(gm.toNormalDoubleFormat(d_dep_amt)), GlobalClass.username, lromtypduration, ldurtyp, lpassportno, lpassportvaldate, lissuedplace, lnationality, lcontactno, lemail, ap_chg_name, ap_doc_ref, ap_currency + " " + gm.toAccountingFormat(gm.toNormalDoubleFormat(ap_dep_amt)));
                
                }

                clr_field();
            }
            else
            {
                MessageBox.Show("Problem occur on Checking in of Walk-in.");
            }

            set_reslist(active_res_where);

            load_rom_available();

            pnl_leftfirst.Show();
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show();
        }

        private String chkin_walkin(string[] data)
        {
            /*
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String curdate  = DateTime.Today.ToString("yyyy-MM-dd");
            String curtime  = DateTime.Now.ToString("HH:mm");
            String reg_num = db.get_pk("reg_num");
            String dacct_no = dgv_guestlist.Rows[0].Cells[0].Value.ToString();
            String dfull_name = dgv_guestlist.Rows[0].Cells[1].Value.ToString();
            String daddress = "";
            String rmtyp     = db.get_romtyp_code(lbl_rmtyp.Text);
            String rmrttyp = "D";
            String disc      = "";
            String disc_amt  = "0.00";
            int occ          = 1;
            String chg = data.GetValue(0).ToString();
            String doc = data.GetValue(1).ToString();
            String doc_ref = data.GetValue(2).ToString();
            String dep_amt = data.GetValue(3).ToString();
            String chg_name = data.GetValue(4).ToString();

            String crdno = data.GetValue(5).ToString();
            String traceno = data.GetValue(6).ToString();
            String nodep_reason = data.GetValue(7).ToString();
            String lromtypduration = "", ldurtyp = "";
            String lpassportno = "", lissuedplace = "", lpassportvaldate = "", lcontactno = "", lemail = "", lnationality = "";

            Double vat_included = 0.00;
            Double sc_included = 0.00;
            Double lessdiscount = 0.00, lessdisc_amt = 0.00;

            if (cbo_rmrttyp.SelectedIndex > -1)
            {
                rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
            }

            if (cbo_rmrttyp.SelectedValue.ToString() == "M")
            {
                lromtypduration = "Month(s)";
                ldurtyp = "Monthly";
            }
            else if (cbo_rmrttyp.SelectedValue.ToString() == "Y")
            {
                lromtypduration = "Year(s)";
                ldurtyp = "Yearly";
            }
            else
            {
                lromtypduration = "Day(s)";
                ldurtyp = "Daily";
            }

            if (cbo_rmrttyp.SelectedIndex == 0)
            {
                rmrttyp = "D";
            }

            if (nodep_reason != "")
            {
                doc = "";
                chg = "";
                dep_amt = "0.00";
                doc_ref = "";
                chg_name = "";
                crdno = "";
                traceno = "";
                nodep_reason = "/" + nodep_reason;
            }

            if (chg == "102" || chg == "103" || chg == "104" || chg == "105")
            {
                doc_ref = crdno + "-" + traceno;
            }

            if (db.has_vat(chg))
            {
                vat_included = db.get_tax(Convert.ToDouble(dep_amt), lessdiscount, lessdisc_amt);
            }
            if (db.has_sc(chg))
            {
                sc_included = db.get_svccharge(Convert.ToDouble(dep_amt), lessdiscount, lessdisc_amt);
            }
            
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

            if (cbo_disc.SelectedIndex > -1)
            {
                disc = cbo_disc.SelectedValue.ToString();
                disc_amt = db.get_discount(cbo_disc.SelectedValue.ToString()).ToString("0.00");
            }

            String col = "reg_num, acct_no, full_name, reg_date, rmrttyp, arr_date, arr_time, dep_date, dep_time, rom_code, typ_code, occ_type, rate_code, rom_rate, govt_tax, extra_bed, pay_code, mkt_code, src_code, trv_code, free_bfast, rm_features, bill_info, remarks, rom_class, user_id, t_date, t_time, out_fol, fctr_code, disc_code, disc_pct, paymentform, doc_ref, doctype, dep_amnt, nodeposit_rmrk";
            String val = "'" + reg_num + "', '" + dacct_no + "', '" + dfull_name + "','" + curdate + "', '" + rmrttyp + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_arrtime.Value.ToString("HH:mm") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_deptime.Value.ToString("HH:mm") + "', '" + lbl_rm.Text + "', '" + rmtyp + "', '" + occ.ToString() + "', '" + cbo_rtcode.SelectedValue.ToString() + "', '" + gm.toNormalDoubleFormat(txt_netrt.Text) + "', '" + gm.toNormalDoubleFormat(txt_govtrt.Text) + "', '0.00', '" + "', '" + cbo_mktsegment.SelectedValue.ToString() + "', '', '000001','0', '', '','" + rtb_remarks.Text + "', 'H', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "','N','', '" + disc.ToString() + "', '" + disc_amt.ToString() + "', '" + chg_name + "', '" + doc_ref + "', '" + doc + "', '" + dep_amt + "', '" + nodep_reason + "'";

            if (db.InsertOnTable("gfolio", col, val))
            {
                daddress = db.get_addrcompmarket(dacct_no.ToString());
                
                //insert other guests
                for (int r = 0; dgv_guestlist.Rows.Count - 1 > r; r++)
                {
                    dacct_no = dgv_guestlist.Rows[r].Cells[0].Value.ToString();
                    dfull_name = dgv_guestlist.Rows[r].Cells[1].Value.ToString();

                    db.InsertOnTable("gfguest", "reg_num, acct_no, full_name, arr_date, dep_date", "'" + reg_num + "', '" + dacct_no + "', '" + dfull_name + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "'");
                }

                //increment rescode to m99
                db.set_pkm99("reg_num", db.get_nextincrementlimitchar(reg_num, 8));
                
                String doctype = "NA";

                if(doc == "Cash Credit")
                {
                    doctype = "CC";
                }
                else if(doc == "Official Receipt")
                {
                   doctype = "OR";
                }
                if (Convert.ToDouble(dep_amt) > 0.00)
                {
                    db.insert_charges(reg_num, dfull_name, chg, lbl_rm.Text, doc_ref, gm.toNormalDoubleFormat(dep_amt), lbl_arrdt.Text, "", "", lbl_resno.Text, "", doctype, vat_included, sc_included, 0.00, traceno, crdno, true);
                }
                UpdateRoomStatus urs = new UpdateRoomStatus();

                urs.set_rmstatus("OCC", lbl_rm.Text);

                Report rpt = new Report("", "");
                
                rpt.Show();
                rpt.printprev_regform(reg_num, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm"), lbl_resno.Text, lbl_rm.Text, dfull_name, txt_rmrate.Text, occ.ToString(), db.get_romtyp_code(lbl_rmtyp.Text), daddress, lbl_arrdt.Text, dtp_arrtime.Value.ToString("HH:mm"), lbl_depdt.Text, dtp_deptime.Value.ToString("HH:mm"), lbl_noofnight.Text, rtb_remarks.Text + "" + nodep_reason, chg_name, doc_ref, doc, dep_amt, GlobalClass.username, lromtypduration, ldurtyp, lpassportno, lpassportvaldate, lissuedplace, lnationality, lcontactno, lemail);
                
                clr_field();
            }
            else
            {
                MessageBox.Show("Problem occur on Checking in of Walk-in.");
            }

            return reg_num; */
            return null;
        }

        private String chkin_arrival(string[] data)
        {
            /*
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String curdate = DateTime.Today.ToString("yyyy-MM-dd");
            String curtime = DateTime.Now.ToString("HH:mm");
            String reg_num = db.get_pk("reg_num");
            String dacct_no = dgv_guestlist.Rows[0].Cells[0].Value.ToString();
            String dfull_name = dgv_guestlist.Rows[0].Cells[1].Value.ToString();
            String daddress = "";
            String rmtyp = db.get_romtyp_code(lbl_rmtyp.Text);
            String rmrttyp = "D";
            String disc = "";
            String disc_amt = "0.00";
            int occ = 1;
            String chg = data.GetValue(0).ToString();
            String doc = data.GetValue(1).ToString();
            String doc_ref = data.GetValue(2).ToString();
            String dep_amt = data.GetValue(3).ToString();
            String chg_name = data.GetValue(4).ToString();

            String lromtypduration = "", ldurtyp = "";
            String lpassportno = "", lissuedplace = "", lpassportvaldate = "", lcontactno = "", lemail = "", lnationality = "";

            String crdno = data.GetValue(5).ToString();
            String traceno = data.GetValue(6).ToString();
            String nodep_reason = data.GetValue(7).ToString();

            Double vat_included = 0.00, sc_included = 0.00, lessdiscout = 0.00, lessdisc_amt = 0.00;

            if (cbo_rmrttyp.SelectedIndex > -1)
            {
                rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
            }

            if (cbo_rmrttyp.SelectedValue.ToString() == "M")
            {
                lromtypduration = "Month(s)";
                ldurtyp = "Monthly";
            }
            else if (cbo_rmrttyp.SelectedValue.ToString() == "Y")
            {
                lromtypduration = "Year(s)";
                ldurtyp = "Yearly";
            }
            else
            {
                lromtypduration = "Day(s)";
                ldurtyp = "Daily";
            }

            if (cbo_rmrttyp.SelectedIndex == 0)
            {
                rmrttyp = "D";
            }
            if (nodep_reason != "")
            {
                doc = "";
                chg = "";
                dep_amt = "0.00";
                doc_ref = "";
                chg_name = "";
                crdno = "";
                traceno = "";
                nodep_reason = "/" + nodep_reason;
            }
            if (chg == "102" || chg == "103" || chg == "104" || chg == "105")
            {
                doc_ref = crdno + "-" + traceno;
            }

            if (db.has_vat(chg))
            {
                vat_included = db.get_tax(Convert.ToDouble(dep_amt), lessdiscout, lessdisc_amt);
            }
            if (db.has_sc(chg))
            {
                sc_included = db.get_svccharge(Convert.ToDouble(dep_amt), lessdiscout, lessdisc_amt
                    );
            }

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

            if (cbo_disc.SelectedIndex > -1)
            {
                disc = cbo_disc.SelectedValue.ToString();
                disc_amt = db.get_discount(cbo_disc.SelectedValue.ToString()).ToString("0.00");
            }

            String col = "reg_num, res_code, acct_no, full_name, reg_date, rmrttyp, arr_date, arr_time, dep_date, dep_time, rom_code, typ_code, occ_type, rate_code, rom_rate, govt_tax, extra_bed, pay_code, mkt_code, src_code, trv_code, free_bfast, rm_features, bill_info, remarks, rom_class, user_id, t_date, t_time, out_fol, fctr_code, disc_code, disc_pct, paymentform, doc_ref, doctype, dep_amnt, nodeposit_rmrk";
            String val = "'" + reg_num + "', '" + lbl_resno.Text + "', '" + dacct_no + "', '" + dfull_name + "','" + curdate + "', '" + rmrttyp + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_arrtime.Value.ToString("HH:mm") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_deptime.Value.ToString("HH:mm") + "', '" + lbl_rm.Text + "', '" + rmtyp + "', '" + occ.ToString() + "', '" + cbo_rtcode.SelectedValue.ToString() + "', '" + gm.toNormalDoubleFormat(txt_netrt.Text) + "', '" + gm.toNormalDoubleFormat(txt_govtrt.Text) + "', '0.00', '" + "', '" + cbo_mktsegment.SelectedValue.ToString() + "', '', '000001','0', '', '','" + rtb_remarks.Text + "', 'H', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "','N','', '" + disc.ToString() + "', '" + disc_amt.ToString() + "', '" + chg_name + "', '" + doc_ref + "', '" + doc + "', '" + dep_amt + "', '" + nodep_reason + "'";

            if (db.InsertOnTable("gfolio", col, val))
            {
                daddress = db.get_addrcompmarket(dacct_no.ToString());
                //update resfil that arrived=Y
                db.UpdateOnTable("resfil","arrived='Y'","res_code='"+lbl_resno.Text+"'");
                //insert other guests
                for (int r = 1; dgv_guestlist.Rows.Count - 1 > r; r++)
                {
                    dacct_no = dgv_guestlist.Rows[r].Cells[0].Value.ToString();
                    dfull_name = dgv_guestlist.Rows[r].Cells[1].Value.ToString();

                    db.InsertOnTable("gfguest", "reg_num, acct_no, full_name, arr_date, dep_date", "'" + reg_num + "', '" + dacct_no + "', '" + dfull_name + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "'");
                }

                //increment rescode to m99
                db.set_pkm99("reg_num", db.get_nextincrementlimitchar(reg_num, 8));

                String doctype = "NA";

                if (doc == "Cash Credit")
                {
                    doctype = "CC";
                }
                else if (doc == "Official Receipt")
                {
                    doctype = "OR";
                }

                if (Convert.ToDouble(dep_amt) > 0.00)
                {
                    db.insert_charges(reg_num, dfull_name, chg, lbl_rm.Text, doc_ref, Convert.ToDouble(dep_amt), lbl_arrdt.Text, "", "", lbl_resno.Text, "", doctype, vat_included, sc_included, 0.00, traceno, crdno, true);
                }

                UpdateRoomStatus urs = new UpdateRoomStatus();

                urs.set_rmstatus("OCC", lbl_rm.Text);

                //report reg
                Report rpt = new Report("", "");
                rpt.Show();

                rpt.printprev_regform(reg_num, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm"), lbl_resno.Text, lbl_rm.Text, dfull_name, txt_rmrate.Text, occ.ToString(), db.get_romtyp_code(lbl_rmtyp.Text), daddress, lbl_arrdt.Text, dtp_arrtime.Value.ToString("HH:mm"), lbl_depdt.Text, dtp_deptime.Value.ToString("HH:mm"), lbl_noofnight.Text, rtb_remarks.Text + " " + nodep_reason, chg_name, doc_ref, doc, dep_amt, GlobalClass.username, lromtypduration, ldurtyp, lpassportno, lpassportvaldate, lissuedplace, lnationality, lcontactno, lemail);
                
                clr_field();
            }
            else
            {
                MessageBox.Show("Problem occur on Checking in of Arrival Guest.");
            }

            return reg_num; */

            return null;
        }

        public void disp_regform(String reg_num)
        {

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

                txt_total_amt.Text = (Convert.ToDouble(txt_rmrate.Text) * Convert.ToDouble(lbl_noofnight_billing.Text)).ToString("0.00");
            }
            catch (Exception)
            { }
        }

        private void disp_computed_bill_frmupdate()
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            Double amt = 0.00;
            Double lessdiscount = 0.00, lessdisc_amt = 0.00;
            Double grossamt = 0.00;
            int occ = 1;
            Boolean issenior_disc = false;

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
                /*
                if (db.has_brkfast(cbo_rtcode.SelectedValue.ToString()))
                {
                    txt_fbkfast.Text = occ.ToString();
                }
                else
                {
                    txt_fbkfast.Text = "0";
                }*/

                lessdisc_amt = gm.toNormalDoubleFormat(txt_discamt.Text);

                grossamt = db.get_roomrateamt(cbo_rtcode.SelectedValue.ToString(), db.get_romtyp_code(lbl_rmtyp.Text), occ);
                txt_grossrt.Text = gm.toAccountingFormat(grossamt);

                txt_netrt.Text = gm.toAccountingFormat(db.get_netrate(grossamt, lessdiscount, lessdisc_amt));
                txt_govtrt.Text = gm.toAccountingFormat(db.get_tax(grossamt, lessdiscount, lessdisc_amt));

                //regular rate w/ or w/out discount
                if (issenior_disc == false)
                {
                    txt_rmrate.Text = gm.toAccountingFormat(grossamt - (grossamt * (lessdiscount / 100)) - lessdisc_amt);
                }
                //senior citizen discount no tax
                else if (issenior_disc == true)
                {
                    Double db_rmrate = gm.toNormalDoubleFormat(txt_netrt.Text);

                    Decimal roundoff_amt = Math.Round(Convert.ToDecimal(db_rmrate), 1);

                    txt_rmrate.Text = roundoff_amt.ToString("0.00");
                    txt_govtrt.Text = "0.00";
                }

                txt_total_amt.Text = gm.toAccountingFormat(Convert.ToDouble(txt_rmrate.Text) * Convert.ToDouble(lbl_noofnight_billing.Text));
            }
            catch (Exception)
            { }
        }

        private void btn_walkin_Click(object sender, EventArgs e)
        {
            isarrival = false;
            clr_field();

            dtp_checkin.Value = DateTime.Today;

            btn_cancel.Enabled = true;
            btn_save.Enabled = true;
            btn_selectcust.Enabled = true;

            grp_roominfo.Enabled = true;
            grp_guest.Enabled = true;
            grpbx_billing.Enabled = true;

            label_contact.Hide();
            label_reservedby.Hide();
            txt_resby.Hide();
            txt_contact.Hide();
            chk_blockres.Hide();            
            dtp_resdt.Hide();

            pnl_leftfirst.Hide();
            tbcntrl_res.SelectedTab = tpg_reg;
            tpg_reg.Text = "Walk In Guest Entry";
            tpg_reg.Show();
        }

        private void chk_blockres_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_blockres.Checked == true)
            {
                lbl_blockedby.Text = GlobalClass.username;
            }
            else
            {
                lbl_blockedby.Text = "";
            }
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

        private void dtp_checkout_ValueChanged(object sender, EventArgs e)
        {
            double val = (dtp_checkout.Value - dtp_checkin.Value).TotalDays;

            txt_noofnights.Text = val.ToString();

            if (val == 0)
            {
                txt_noofnights.Text = "1";
            }
        }

        private void btn_chkin_Click(object sender, EventArgs e)
        {
            try
            {
                sel_arrival_chkin(dgv_reslist.CurrentRow.Index);
            }
            catch (Exception) { MessageBox.Show("No arrivals selected"); }
        }

        private void disp_arrival(String res_code)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            DataTable dt_guest = new DataTable();
            DataTable dt_otherguestid = new DataTable();
            String guestno = "";
            String rom_rate = "";
            String occ_code = "";
            String rmrttyp = "";
            Boolean different_arr_date = false;

            //try
            //{
                if (dgv_reslist.SelectedRows.Count > 0)
                {
                    pnl_leftfirst.Hide();
                    int row = dgv_reslist.CurrentCell.RowIndex;
                    isarrival = true;
                    clr_field();

                    isnew = false;

                    clr_field();
                    btn_cancel.Enabled = true;
                    btn_save.Enabled = true;
                    btn_selectcust.Enabled = true;
                    grp_roominfo.Enabled = true;
                    grp_guest.Enabled = true;
                    grpbx_billing.Enabled = true;

                    //get reservation info and pass res_code;
                    dt = db.get_res_info(res_code);
                    
                    foreach (DataRow r in dt.Rows)
                    {
                        lbl_resno.Text = res_code;                        
                        dtp_resdt.Value = Convert.ToDateTime(r["res_date"].ToString());

                        if (Convert.ToDateTime(r["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
                        {
                            rom_rate = r["rom_rate"].ToString();

                            dtp_arrtime.Value = DateTime.Now;
                            dtp_deptime.Value = Convert.ToDateTime(r["dep_time"].ToString());
                            dtp_checkin.Value = Convert.ToDateTime(r["arr_date"].ToString());

                            lbl_arrdt.Text = Convert.ToDateTime(r["arr_date"].ToString()).ToString("MM/dd/yyyy");
                            lbl_depdt.Text = Convert.ToDateTime(r["dep_date"].ToString()).ToString("MM/dd/yyyy");
                            lbl_rm.Text = r["rom_code"].ToString();
                            lbl_rmtyp.Text = db.get_romtyp_desc(r["typ_code"].ToString());                            
                            cbo_rtcode.SelectedValue = r["rate_code"].ToString();
                        }
                        else
                        {
                            different_arr_date = true;

                            dtp_arrtime.Value = Convert.ToDateTime(db.get_systemdate(""));
                            dtp_deptime.Value = Convert.ToDateTime(db.get_systemdate(""));
                            dtp_checkin.Value = Convert.ToDateTime(db.get_systemdate(""));

                            lbl_arrdt.Text = dtp_checkin.Value.ToString("MM/dd/yyyy");
                        }

                        rmrttyp = r["rmrttyp"].ToString();

                        if (rmrttyp == "D")
                        {
                            cbo_rmrttyp.SelectedIndex = 0;
                        }
                        else
                        {
                            cbo_rmrttyp.SelectedIndex = 1;
                        }

                        occ_code = r["occ_type"].ToString();
                        cbo_mktsegment.SelectedValue = r["mkt_code"].ToString();
                        cbo_disc.SelectedValue = r["disc_code"].ToString();

                        rtb_remarks.Text = r["remarks"].ToString() + " " + r["rm_features"].ToString() + " " + r["bill_info"].ToString();
                        txt_resby.Text = r["reserv_by"].ToString();
                        //txt_fbkfast.Text = r["free_bfast"].ToString();
                        lbl_blockedby.Text = r["blockby"].ToString();

                        if (r["blockresv"].ToString() == "Y")
                        {
                            chk_blockres.Checked = true;
                        }

                        guestno = r["acct_no"].ToString();
                    }

                    if (different_arr_date == false)
                    {
                        double val = (Convert.ToDateTime(lbl_depdt.Text) - Convert.ToDateTime(lbl_arrdt.Text)).TotalDays;

                        lbl_noofnight.Text = val.ToString();
                        lbl_noofnight_billing.Text = val.ToString();

                        if (val == 0)
                        {
                            lbl_noofnight.Text = "1";
                            lbl_noofnight_billing.Text = "1";
                        }
                    }
                    else
                    {
                        lbl_noofnight.Text = "0";
                        lbl_noofnight_billing.Text = "0";
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

                    dgv_guestlist.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());

                    dt_otherguestid = db.get_resguest_id(res_code);

                    dt_guest.Rows.Clear();

                    foreach (DataRow rowid in dt_otherguestid.Rows)
                    {
                        dt_guest = db.get_guest_info(rowid["acct_no"].ToString());

                        dgv_guestlist.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());

                        dt_guest.Rows.Clear();
                    }

                    disp_computed_bill();

                    dtp_arrtime.Enabled = false;
                    btn_cancel.Enabled = true;
                    btn_save.Enabled = true;
                    btn_selectcust.Enabled = true;

                    grp_roominfo.Enabled = true;
                    grp_guest.Enabled = true;
                    grpbx_billing.Enabled = true;

                    label_contact.Show();
                    label_reservedby.Show();
                    txt_resby.Show();
                    txt_contact.Show();
                    chk_blockres.Show();
                    dtp_resdt.Show();

                    tbcntrl_res.SelectedTab = tpg_reg;
                    tpg_reg.Text = "Arrival Guest Info";
                    tpg_reg.Show();
                }
                else
                {
                    MessageBox.Show("Pls select the room.");
                }
           // }
            ////catch (Exception er)
            //{
           //     MessageBox.Show(er.Message);
           // }            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            clr_field();
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show();
        }

        private void btn_removefrmguestlist_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dgv_guestlist.CurrentRow.Index;

                dgv_guestlist.Rows.RemoveAt(row);

                lbl_noofguest.Text = (dgv_guestlist.Rows.Count - 1).ToString();
            }
            catch (Exception er) { MessageBox.Show("Pls select/highlight the guest to remove."); }
        }

        private void btn_srchreservation_Click(object sender, EventArgs e)
        {
            String WHERE = "";

            if (String.IsNullOrEmpty(txt_srch_lname.Text) == false)
            {
                WHERE = WHERE + " AND r.full_name ILIKE '%" + txt_srch_lname.Text + "%'";
            }
            if (String.IsNullOrEmpty(txt_srch_fname.Text) == false)
            {
                WHERE = WHERE + " AND r.full_name ILIKE '%" + txt_srch_fname.Text + "%'";
            }
            if (String.IsNullOrEmpty(txt_srch_rom.Text) == false)
            {

                WHERE = WHERE + " AND r.rom_code ILIKE'%" + txt_srch_rom.Text + "%'";
            }
            if (String.IsNullOrEmpty(txt_srch_gfno.Text) == false)
            {
                WHERE = WHERE + " AND r.res_code ILIKE '%" + txt_srch_gfno.Text + "%'";
            }
            if (cbo_srchcomp.SelectedIndex != -1)
            {
                WHERE = WHERE + " AND g.comp_code ILIKE'%" + cbo_srchcomp.SelectedValue.ToString() + "%'";
            }

            set_reslist(WHERE);
        }

        private void dtp_srch_dt_ValueChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            String WHERE = "";           

            try
            {
                if (dtp_srch_dt.Value.CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == -1)
                {
                    lbl_status_dt.Text = "Previous Arrival List";
                }
                else if (dtp_srch_dt.Value.CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 1)
                {
                    lbl_status_dt.Text = "Upcoming Arrival List";
                }
                else
                {
                    lbl_status_dt.Text = "Expected Arrival Today";
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            if (String.IsNullOrEmpty(txt_srch_lname.Text) == false)
            {
                WHERE = WHERE + " AND r.full_name ILIKE '%" + txt_srch_lname.Text + "%'";
            }
            if (String.IsNullOrEmpty(txt_srch_fname.Text) == false)
            {
                WHERE = WHERE + " AND r.full_name ILIKE '%" + txt_srch_fname.Text + "%'";
            }
            if (String.IsNullOrEmpty(txt_srch_rom.Text) == false)
            {

                WHERE = WHERE + " AND r.rom_code='" + txt_srch_rom.Text + "'";
            }
            if (String.IsNullOrEmpty(txt_srch_gfno.Text) == false)
            {
                WHERE = WHERE + " AND r.res_code ILIKE '%" + txt_srch_gfno.Text + "%'";
            }
            if (cbo_srchcomp.SelectedIndex != -1)
            {
                WHERE = WHERE + " AND g.comp_code='" + cbo_srchcomp.SelectedValue.ToString() + "'";
            }

            set_reslist(WHERE);
        }

        private void cbo_occtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_computed_bill();
        }

        private void cbo_rmrttyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            Double night = 0.00, mo = 0.00;

            if (cbo_rmrttyp.SelectedIndex == 1)
            {
                lbl_noofnight_title.Text = "No. of Months";

                night = Convert.ToDouble(lbl_noofnight_billing.Text);

                if(night > 30)
                {
                    mo = night/30;
                    
                    if(night % 30 > 0)
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

                    if (rmrttyp == "M")
                    {

                        lbl_noofnight_title_top.Text = "No. of Months";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        night = Convert.ToDouble(lbl_noofnight.Text);

                        if (night > 30)
                        {
                            mo = night / 30;

                            if (night % 30 > 0) { mo = mo + 1.00; }
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
                        lbl_noofnight_title_top.Text = "No. of Months";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        night = Convert.ToDouble(lbl_noofnight.Text);

                        if (night > 7)
                        {
                            wk = night / 7;

                            if (night % 7 > 0) { wk = wk + 1.00; }
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

        private void txt_discamt_TextChanged(object sender, EventArgs e)
        {
            disp_computed_bill();
        }

        private void tbcntrl_res_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void grpbx_billing_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_reslist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_rom_available_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
    }
}