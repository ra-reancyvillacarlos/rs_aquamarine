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
    public partial class Reservation : Form
    {
        Main mainform;
        cancelReservation cRes;
        String schema = "";
        String t_date = "";
        Boolean isnew = true;
        String modname = "";
        public String active_res_where;

        public Reservation(Main m)
        {
            InitializeComponent();
            mainform = m;
            //cRes = new cancelReservation(this);

            modname = mainform.get_modname();
        }

        public Reservation()
        {
            InitializeComponent();

            //cRes = new cancelReservation(this);
        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();

            schema = db.get_schema();
            t_date = db.get_systemdate("");
            dtp_checkin.Value = DateTime.Today;
            dtp_checkout.Value = DateTime.Today;
            active_res_where = " AND r.arr_date >= '" + db.get_systemdate("") + "' AND (r.arrived IS NULL OR r.arrived !='Y') AND (r.cancel IS NULL OR r.cancel !='Y')";



            set_reslist(active_res_where);
            //set_compcbo();
            //set_rtcbo();
            //set_mscbo();
            //set_disccbo();
            //set_rmtypecbo();

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

        public void gotofront()
        {
            tbcntrl_res.SelectedTab = tpg_list;
            tpg_list.Show();
            pnl_leftfirst.Show();
        }

        public void reset_modname()
        {
            // mainform.set_modname(modname);
        }

        private void btn_selectcust_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalMethod gmethod = new GlobalMethod();
                //addGuest AG = new addGuest(this, null, null);
                GlobalClass.gdgv = gmethod.CopyDataGridView(dgv_guestlist);

                //mainform.set_modname(modname + " > " + "Select Guest");

                //AG.MdiParent = this.MdiParent;
                //AG.reload_guest();
                //AG.Show();

            }
            catch (Exception) { }
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

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_srchreservation_Click(object sender, EventArgs e)
        {

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

            dt_allrooms = db.get_allroomExpZ(typ_code, null, null);
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

        /////////////////// room availability //////////////////////

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

        private void dgv_reslist_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // if (e.RowIndex % 2 == 0)
            // {
            //     e.CellStyle.BackColor = Color.YellowGreen;
            // }
            //else
            // {
            //    e.CellStyle.BackColor = Color.White;
            //dgv_reslist.Rows[e.RowIndex].Cells[2].Style.BackColor = Color.Red;
            // }
            // if (e.RowIndex > 2) // condition
            //      e.CellStyle.BackColor = Color.YellowGreen;         
        }

        private void btn_search_Click_1(object sender, EventArgs e)
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
                        //clr_field();
                        //btn_cancel.Enabled = true;
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
                        txt_discamt.Text = "0";
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

        private void dtp_checkout_ValueChanged(object sender, EventArgs e)
        {
            double val = (dtp_checkout.Value - dtp_checkin.Value).TotalDays;

            txt_noofnights.Text = val.ToString();

            if (val == 0)
            {
                txt_noofnights.Text = "1";
            }
        }

        private void dtp_checkin_ValueChanged(object sender, EventArgs e)
        {
            double val = (dtp_checkout.Value - dtp_checkin.Value).TotalDays;

            txt_noofnights.Text = val.ToString();
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

        private void txt_noofnights_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
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
                    txt_discamt.Text = occ.ToString();
                }
                else
                {
                    txt_discamt.Text = "0";
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
                    Double db_rmrate = Convert.ToDouble(txt_netrt.Text);

                    Decimal roundoff_amt = Math.Round(Convert.ToDecimal(db_rmrate), 1);

                    txt_rmrate.Text = gm.toAccountingFormat(Convert.ToDouble(roundoff_amt));
                    txt_govtrt.Text = "0.00";
                }

                txt_total_amt.Text = gm.toAccountingFormat(Convert.ToDouble(txt_rmrate.Text) * Convert.ToDouble(lbl_noofnight_billing.Text));
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

                if (db.has_brkfast(cbo_rtcode.SelectedValue.ToString()))
                {
                    txt_discamt.Text = occ.ToString();
                }
                else
                {
                    txt_discamt.Text = "0";
                }

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
                    Double db_rmrate = Convert.ToDouble(txt_netrt.Text);

                    Decimal roundoff_amt = Math.Round(Convert.ToDecimal(db_rmrate), 1);

                    txt_rmrate.Text = gm.toAccountingFormat(Convert.ToDouble(roundoff_amt));
                    txt_govtrt.Text = "0.00";
                }

                txt_total_amt.Text = gm.toAccountingFormat(Convert.ToDouble(txt_rmrate.Text) * Convert.ToDouble(lbl_noofnight_billing.Text));
            }
            catch (Exception)
            { }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String curdate = DateTime.Today.ToString("yyyy-MM-dd");
            String curtime = DateTime.Now.ToString("HH:mm");
            String res_code = db.get_pk("res_code");
            String dacct_no = "";
            String dfull_name = "";
            String rmtyp = db.get_romtyp_code(lbl_rmtyp.Text);
            String rmrttyp = "";
            int occ = 1;
            String isblock = "N";
            String disc = "";
            String disc_amt = "0.00";
            Boolean cont = true;

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

            if (chk_blockres.Checked)
            {
                isblock = "Y";
            }
            if (cbo_disc.SelectedIndex > -1)
            {
                disc = cbo_disc.SelectedValue.ToString();
                disc_amt = db.get_discount(cbo_disc.SelectedValue.ToString()).ToString("0.00");
            }

            if (cbo_rmrttyp.SelectedIndex == -1)
            {
                MessageBox.Show("Pls select Room Rate Type.");
            }
            else if (dgv_guestlist.Rows.Count <= 1)
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
            else if (db.is_roomreserved(lbl_rm.Text) && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
            {
                MessageBox.Show("Room already reserved.");
            }
            else
            {
                if (db.get_romstatus(lbl_rm.Text) == "OCC" && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
                {
                    if (MessageBox.Show("Room is currently occupied, are you sure you want to continue?", "Room is occupied", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        cont = false;
                    }
                }
                else if (db.get_romstatus(lbl_rm.Text) == "VD" && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
                {
                    if (MessageBox.Show("Room is currently vacant dirty, are you sure you want to continue?", "Room is occupied", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        cont = false;
                    }
                }
                else if (db.get_romstatus(lbl_rm.Text) == "OOO" && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
                {
                    if (MessageBox.Show("Room is currently out of order, are you sure you want to continue?", "Room is occupied", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        cont = false;
                    }
                }

                if (cont == true)
                {
                    try
                    {
                        dacct_no = dgv_guestlist.Rows[0].Cells[0].Value.ToString();
                        dfull_name = dgv_guestlist.Rows[0].Cells[1].Value.ToString();

                        if (cbo_rmrttyp.SelectedIndex > -1)
                        {
                            rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
                        }

                        if (isnew == true)
                        {
                            String col = "res_code, acct_no, full_name, rmrttyp, res_date, arr_date, arr_time, dep_date, dep_time, rom_code, rom_rate, typ_code, rate_code, mkt_code, src_code, trv_code, rm_features, bill_info, remarks, reserv_by,  reserv_thru, reserv_tel, user_id, t_date, t_time, occ_type, fctr_code, free_bfast, blockresv, blockby, disc_code, disc_pct";

                            String val = "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "','" + rmrttyp + "','" + curdate + "', '" + lbl_arrdt.Text + "', '" + dtp_arrtime.Value.ToString("HH:mm") + "', '" + lbl_depdt.Text + "', '" + dtp_deptime.Value.ToString("HH:mm") + "', '" + lbl_rm.Text + "', '" + gm.toNormalDoubleFormat(txt_rmrate.Text) + "', '" + rmtyp + "', '" + cbo_rtcode.SelectedValue.ToString() + "', '" + cbo_mktsegment.SelectedValue.ToString() + "', '', '000001','','','" + rtb_remarks.Text + "'";
                            val = val + ", '" + txt_resby.Text + "', '','" + txt_contact.Text + "','" + GlobalClass.username + "','" + db.get_systemdate("") + "','" + curtime + "','" + occ.ToString() + "','','" + txt_discamt.Text + "','" + isblock + "','" + lbl_blockedby.Text + "','" + disc + "','" + disc_amt + "'";
                            //MessageBox.Show(res_code);
                            if (db.InsertOnTable("resfil", col, val))
                            {
                                //insert other guests
                                for (int r = 1; dgv_guestlist.Rows.Count - 1 > r; r++)
                                {
                                    dacct_no = dgv_guestlist.Rows[r].Cells[0].Value.ToString();
                                    dfull_name = dgv_guestlist.Rows[r].Cells[1].Value.ToString();

                                    db.InsertOnTable("resguest", "res_code, acct_no, full_name, arr_date, dep_date", "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "'");
                                }
                                //increment rescode to m99
                                db.set_pkm99("res_code", db.get_nextincrementlimitchar(res_code, 8));

                                MessageBox.Show("New Record(s) successfully added.");
                                tbcntrl_res.SelectedTab = tpg_list;
                                tpg_list.Show();
                                pnl_leftfirst.Show();
                                clr_field();
                            }
                            else
                            {
                                MessageBox.Show("Problem on Adding New Record.");
                            }
                        }
                        else
                        {
                            res_code = lbl_resno.Text;
                            if (db.UpdateOnTable("resfil", "acct_no='" + dacct_no + "', rom_code='" + lbl_rm.Text + "', full_name='" + dfull_name + "', res_date='" + curdate + "', arr_date='" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', arr_time='" + dtp_arrtime.Value.ToString("HH:mm") + "', dep_date='" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', dep_time='" + dtp_deptime.Value.ToString("HH:mm") + "', typ_code='" + rmtyp + "', rate_code='" + cbo_rtcode.SelectedValue.ToString() + "', mkt_code='" + cbo_mktsegment.SelectedValue.ToString() + "', src_code='', trv_code='000001', rm_features='', bill_info='', remarks='" + rtb_remarks.Text + "', reserv_by='" + txt_resby.Text + "',  reserv_thru='', reserv_tel='" + txt_contact.Text + "', user_id='" + GlobalClass.username + "', t_date='" + db.get_systemdate("") + "', t_time='" + curtime + "', occ_type='" + occ.ToString() + "', fctr_code='', free_bfast='" + txt_discamt.Text + "', blockresv='" + isblock + "', blockby='" + lbl_blockedby.Text + "', disc_code='" + disc + "', disc_pct='" + disc_amt + "'", "res_code='" + res_code + "'"))
                            {
                                db.DeleteOnTable("resguest", "res_code='" + res_code + "'");

                                //insert other guests
                                for (int r = 1; dgv_guestlist.Rows.Count - 1 > r; r++)
                                {
                                    dacct_no = dgv_guestlist.Rows[r].Cells[0].Value.ToString();
                                    dfull_name = dgv_guestlist.Rows[r].Cells[1].Value.ToString();

                                    db.InsertOnTable("resguest", "res_code, acct_no, full_name, arr_date, dep_date", "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "'");
                                }

                                MessageBox.Show("Record updated successfully.");
                                tbcntrl_res.SelectedTab = tpg_list;
                                tpg_list.Show();
                                pnl_leftfirst.Show();
                                clr_field();
                            }
                        }

                        set_reslist(active_res_where);
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Transaction cannot be saved.");
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Are you sure you want to CANCEL the Reservation?", "Confirm Cancel", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{

            //}

            //cancelReservation canres = new cancelReservation(this);

            //canres.Show();
            GlobalMethod gmethod = new GlobalMethod();
            GlobalClass.gdgv = gmethod.CopyDataGridView(dgv_guestlist);
            // mainform.set_modname(modname + " > " + cRes.Text);
            cRes.MdiParent = this.MdiParent;
            cRes.set_data(lbl_resno.Text);
            cRes.Show();

            //clr_field();
        }

        public void clr_field()
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
            txt_discamt.Text = "0";
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

            dgv_rom_available.DataSource = null;

            btn_cancel.Enabled = false;
            btn_save.Enabled = false;
            btn_selectcust.Enabled = false;

            grp_roominfo.Enabled = false;
            grp_guest.Enabled = false;
            grpbx_billing.Enabled = false;
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

        private void cbo_occtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            int occ = 1;

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

                if (db.has_brkfast(cbo_rtcode.SelectedValue.ToString()) && occ > 1)
                {
                    txt_discamt.Text = "2"; //occ.ToString();
                }
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

        private void btn_removefrmguestlist_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dgv_guestlist.CurrentCell.RowIndex;

                dgv_guestlist.Rows.RemoveAt(row);

                lbl_noofguest.Text = (dgv_guestlist.Rows.Count - 1).ToString();
            }
            catch (Exception er) { MessageBox.Show("Pls select/highlight the guest to remove."); }
        }

        private void dgv_reslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            DataTable dt_guest = new DataTable();
            DataTable dt_otherguestid = new DataTable();
            String guestno = "", res_code = "";
            String rom_rate = "", rom_rate_typ = "Monthly";
            String occ_code = "";
            int cnt = 1;

            try
            {
                if (dgv_reslist.SelectedRows.Count > 0)
                {
                    int row = dgv_reslist.CurrentCell.RowIndex;

                    isnew = false;
                    pnl_leftfirst.Hide();

                    clr_field();
                    btn_cancel.Enabled = true;
                    btn_save.Enabled = true;
                    btn_selectcust.Enabled = true;
                    grp_roominfo.Enabled = true;
                    grp_guest.Enabled = true;
                    grpbx_billing.Enabled = true;

                    //get reservation info and pass res_code;
                    dt = db.get_res_info(dgv_reslist[0, e.RowIndex].Value.ToString());

                    foreach (DataRow r in dt.Rows)
                    {
                        res_code = r["res_code"].ToString();
                        rom_rate = r["rom_rate"].ToString();

                        dtp_resdt.Value = Convert.ToDateTime(r["res_date"].ToString());
                        dtp_arrtime.Value = Convert.ToDateTime(r["arr_time"].ToString());
                        dtp_deptime.Value = Convert.ToDateTime(r["dep_time"].ToString());

                        lbl_resno.Text = res_code;
                        lbl_arrdt.Text = Convert.ToDateTime(r["arr_date"].ToString()).ToString("MM/dd/yyyy");
                        lbl_depdt.Text = Convert.ToDateTime(r["dep_date"].ToString()).ToString("MM/dd/yyyy");
                        lbl_rm.Text = r["rom_code"].ToString();
                        lbl_rmtyp.Text = db.get_romtyp_desc(r["typ_code"].ToString());
                        lbl_clerk.Text = r["user_id"].ToString();
                        lbl_blockedby.Text = r["blockby"].ToString();

                        rom_rate_typ = r["rmrttyp"].ToString();

                        cbo_rtcode.SelectedValue = r["rate_code"].ToString();

                        occ_code = r["occ_type"].ToString();
                        cbo_mktsegment.SelectedValue = r["mkt_code"].ToString();
                        cbo_disc.SelectedValue = r["disc_code"].ToString();

                        rtb_remarks.Text = r["remarks"].ToString() + " " + r["rm_features"].ToString() + " " + r["bill_info"].ToString();
                        txt_resby.Text = r["reserv_by"].ToString();
                        txt_discamt.Text = r["free_bfast"].ToString();

                        if (r["blockresv"].ToString() == "Y")
                        {
                            chk_blockres.Checked = true;
                        }

                        guestno = r["acct_no"].ToString();
                    }

                    double val = (Convert.ToDateTime(lbl_depdt.Text) - Convert.ToDateTime(lbl_arrdt.Text)).TotalDays;

                    lbl_noofnight.Text = val.ToString();
                    lbl_noofnight_billing.Text = val.ToString();

                    if (val == 0)
                    {
                        lbl_noofnight.Text = "1";
                        lbl_noofnight_billing.Text = "1";
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

                        cnt++;
                    }

                    lbl_noofguest.Text = cnt.ToString();

                    disp_computed_bill();

                    tbcntrl_res.SelectedTab = tpg_reg;

                    tpg_reg.Show();
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

        private void dgv_rom_available_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            /*e.CellStyle.SelectionForeColor = Color.Brown;
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
            } */
        }

        private void btn_srchreservation_Click_1(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String WHERE = "";
            String lname = txt_srchlname.Text;
            String fname = txt_srchfname.Text;

            if (String.IsNullOrEmpty(lname) == false)
            {
                WHERE = WHERE + " AND r.full_name LIKE '%" + lname + "%'";
            }
            if (String.IsNullOrEmpty(fname) == false)
            {
                WHERE = WHERE + " AND r.full_name LIKE '%" + fname + "%'";
            }
            if (cbo_srchcomp.SelectedIndex != -1)
            {
                WHERE = WHERE + " AND g.comp_code='" + cbo_srchcomp.SelectedValue.ToString() + "'";
            }
            if (chk_active.Checked)
            {
                WHERE = WHERE + " AND r.arr_date >= '" + db.get_systemdate("") + "' AND (r.arrived IS NULL OR r.arrived !='Y') AND (r.cancel IS NULL OR r.cancel !='Y')";
            }

            set_reslist(WHERE);
        }

        private void btn_update_Click(object sender, EventArgs e)
        {

        }

        private void grp_roominfo_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_reslist_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            pnl_leftfirst.Hide();

            tbcntrl_res.SelectedTab = tpg_reg;
            tpg_reg.Show();
        }

        private void txt_discamt_TextChanged(object sender, EventArgs e)
        {
            disp_computed_bill();
        }

        private void pnl_leftfirst_Paint(object sender, PaintEventArgs e)
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

        
    }
}
