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
    public partial class z_Research : Form
    {
        public newReservation frm = null;
        public newArrivalWalkin frmarrival = null;
        public newInhouse frmhouse = null;
        public z_Research()
        {
            InitializeComponent();
        }
        public z_Research(newInhouse frm, String action,String date,String enddate)
        {
            InitializeComponent();
            this.frmhouse = frm;
            if (action == "departure")
            {
                //dtp_checkin.Enabled = false;
                
            }
            dtp_checkin.Value = Convert.ToDateTime(date);
            dtp_checkout.Value = Convert.ToDateTime(enddate);
            load_rom_available();
            set_roomstatuslist();
        }
        public z_Research(newReservation frm)
        {
            InitializeComponent();
            this.frm = frm;
            load_rom_available();
            set_roomstatuslist();
        }
        public z_Research(newArrivalWalkin frm,String action, String date,String enddate)
        {
            InitializeComponent();
            this.frmarrival = frm;
            try { dtp_checkin.Value = Convert.ToDateTime(date); } catch { }
            try { dtp_checkout.Value = Convert.ToDateTime(enddate); } catch { }
            compute_noofnights();
            load_rom_available();
            set_roomstatuslist();
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
            //MessageBox.Show(dt_reserved.Rows.Count.ToString());
            if (dt_reserved.Rows.Count > 0)
            {
                try
                {
                    //dgv_res.DataSource = dt_reserved;

                    for (int r = 0; r < dt_reserved.Rows.Count; r++)
                    {
                        //MessageBox.Show(dt_reserved.Rows[r]["dep_date"].ToString() + " - " + dtp_checkout.Value.ToString("yyyy-MM-dd") + " == " + Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).Equals(dtp_checkout.Value).ToString());
                        //MessageBox.Show(dt_reserved.Rows[r]["rom_code"].ToString() + ":: " +db.get_systemdate() + " => " + Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate())).ToString() + " and " + Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate())).ToString() + " and " + String.IsNullOrEmpty(dt_reserved.Rows[r]["arrived"].ToString()).ToString());
                        //String str = " " + dt_reserved.Rows[r]["rom_code"].ToString() + "  " + dt_reserved.Rows[r]["arr_date"].ToString() + " " + dt_reserved.Rows[r]["dep_date"].ToString() + " " + dt_reserved.Rows[r]["arrived"].ToString() + " .. " + db.get_systemdate("") + " ";
                        //Console.WriteLine(str);

                        // removed by: Reancy 05 29 2018
                        //if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) <= 0 && Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) >= 0 && String.IsNullOrEmpty(dt_reserved.Rows[r]["arrived"].ToString()) == true)
                        //{
                        //    //MessageBox.Show(dt_reserved.Rows[r]["arrived"].ToString() + " is not arrived.");
                        //    //if equal nothing to do.
                        //}
                        //else if (Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).Equals(dtp_checkin.Value))
                        //{
                        //    //if equal nothing to do.
                        //}
                        //else if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).Equals(dtp_checkout.Value))
                        //{
                        //    //if equal nothing to do.
                        //}
                        //else
                        //{
                        if (dt_reserved.Rows[r]["arr_date"].ToString() == dtp_checkout.Value.ToString() || dt_reserved.Rows[r]["dep_date"].ToString() == dtp_checkin.Value.ToString())
                        {

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
            //MessageBox.Show(dt_occupied.Rows.Count.ToString());
            if (dt_occupied.Rows.Count > 0)
            {
                try
                {
                    //dgv_occ.DataSource = dt_occupied;

                    for (int o = 0; o < dt_occupied.Rows.Count; o++)
                    {
                        //MessageBox.Show(dt_occupied.Rows[o]["rom_code"].ToString() + ": " + dt_occupied.Rows[o]["dep_date"].ToString() + " - " + dtp_checkin.Value.ToString("yyyy-MM-dd") + " == " + Convert.ToDateTime(dt_occupied.Rows[o]["dep_date"].ToString()).Equals(dtp_checkin.Value).ToString());
                        String str = " " + dt_occupied.Rows[o]["dep_date"].ToString()  + " " + dt_occupied.Rows[o]["rom_code"].ToString() + " .. " + db.get_systemdate("") + " ";
                        Console.WriteLine(str);

                        // removed by: Reancy 05 29 2018
                        //if (Convert.ToDateTime(dt_occupied.Rows[o]["dep_date"].ToString()).Equals(dtp_checkin.Value))
                        //{
                        //    //if equal nothing to do. means to be available
                        //}
                        if (dt_reserved.Rows[o]["arr_date"].ToString() == dtp_checkout.Value.ToString() || dt_reserved.Rows[o]["dep_date"].ToString() == dtp_checkin.Value.ToString())
                        {

                        } else {
                            drr = dt_allrooms.Select("rom_code='" + dt_occupied.Rows[o]["rom_code"].ToString() + "'");

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
        private void z_Research_Load(object sender, EventArgs e)
        {
            if(frmhouse==null)
            { 
            dtp_checkin.Value = DateTime.Now;
            dtp_checkout.Value = DateTime.Now;
            }
        }
        public void clr_field()
        {
            // frm.lbl_resno.Text = "";
            //frm.lbl_arrdt3.Text = "";
            //frm.lbl_arrival_date.Text = "";
            //frm.lbl_departure_date.Text = "";
            frm.lbl_noofnight.Text = "0";
            //frm.lbl_rm.Text = "";
            frm.lbl_rmtyp.Text = "";
            frm.lbl_noofnight_billing.Text = "0";
            frm.lbl_noofguest.Text = "0";
            //frm.lbl_depdt3.Text = "";
            //frm.lbl_blockedby.Text = "";

            frm.chk_blockres.Checked = false;

            // frm.cbo_srchcomp.SelectedIndex = -1; 
            frm.cbo_disc.SelectedIndex = -1;
            frm.cbo_mktsegment.SelectedIndex = -1;
            frm.cbo_rtcode.SelectedIndex = -1;
            frm.cbo_occtyp.SelectedIndex = -1;
            //cbo_type.SelectedIndex = -1;

            frm.txt_contact.Text = "";
            frm.txt_discamt.Text = "0";
            frm.txt_govtrt.Text = "0.00";
            frm.txt_netrt.Text = "0.00";
            frm.txt_rmrate.Text = "0.00";
            frm.txt_total_amt.Text = "0.00";
            frm.txt_resby.Text = "";
            frm.rtb_remarks.Text = "";

            //if (dgv_guestlist.Rows.Count > 1)
            //{
            //    for (int i = 0; dgv_guestlist.Rows.Count > i + 1; i++)
            //    {
            //        dgv_guestlist.Rows.RemoveAt(i);
            //    }
            //}

            //dgv_rom_available.DataSource = null;

            frm.btn_cancel.Enabled = false;
            frm.button1.Enabled = false;
            frm.btn_selectcust.Enabled = false;

            frm.grp_roominfo.Enabled = false;
            frm.grp_guest.Enabled = false;
            frm.grpbx_billing.Enabled = false;
        }
        private void dgv_rom_available_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_rom_available.SelectedRows.Count > 0)
                {

                    int days = 0;
                    TimeSpan span = dtp_checkout.Value.Subtract(dtp_checkin.Value);
                    days = (int)Math.Round(span.TotalDays, 1);

                    if (Convert.ToInt32(txt_noofnights.Text) < 0)
                    {
                        MessageBox.Show("No. of Nights must not less than zero.");
                    }
                    else if (Convert.ToInt32(txt_noofnights.Text) != days)
                    {
                        MessageBox.Show("Should be date is equal to no. of night.");
                    }
                    else
                    {
                        //clr_field();
                        if (frm != null)
                        {
                            frm.btn_cancel.Enabled = true;
                            frm.button1.Enabled = true;
                            frm.btn_selectcust.Enabled = true;
                            frm.grp_roominfo.Enabled = true;
                            frm.grp_guest.Enabled = true;
                            frm.grpbx_billing.Enabled = true;
                            int row = dgv_rom_available.CurrentRow.Index;

                            String rom_code = dgv_rom_available.Rows[row].Cells[0].Value.ToString().Trim();
                            String rom_typ = dgv_rom_available.Rows[row].Cells[2].Value.ToString().Trim();

                            //frm.lbl_rm.Text = rom_code;
                            //frm.lbl_rmtyp.Text = rom_typ;
                            //frm.lbl_arrdt.Text = dtp_checkin.Value.ToString("yyyy-MM-dd");
                            //frm.lbl_depdt.Text = dtp_checkout.Value.ToString("yyyy-MM-dd");

                            frm.lbl_noofnight.Text = txt_noofnights.Text;
                            frm.lbl_noofnight_billing.Text = txt_noofnights.Text;

                            frm.cbo_rtcode.SelectedIndex = -1;
                            frm.cbo_disc.SelectedIndex = -1;
                            frm.txt_discamt.Text = "0";
                            frm.txt_netrt.Text = "0.00";
                            frm.txt_govtrt.Text = "0.00";
                            frm.txt_total_amt.Text = "0.00";

                            frm.tbcntrl_res.SelectedTab = frm.tpg_reg;
                            frm.tpg_reg.Show();
                        }
                        else if (frmarrival != null)
                        {
                            frmarrival.btn_cancel.Enabled = true;
                            frmarrival.button1.Enabled = true;
                            frmarrival.btn_selectcust.Enabled = true;
                            frmarrival.grp_roominfo.Enabled = true;
                            frmarrival.grp_guest.Enabled = true;
                            frmarrival.grpbx_billing.Enabled = true;
                            int row = dgv_rom_available.CurrentRow.Index;

                            String rom_code = dgv_rom_available.Rows[row].Cells[0].Value.ToString().Trim();
                            String rom_typ = dgv_rom_available.Rows[row].Cells[2].Value.ToString().Trim();

                            frmarrival.lbl_rm.Text = rom_code;
                            frmarrival.lbl_rmtyp.Text = rom_typ;
                            frmarrival.lbl_arrdt.Text = dtp_checkin.Value.ToString("yyyy-MM-dd");
                            frmarrival.lbl_depdt.Text = dtp_checkout.Value.ToString("yyyy-MM-dd");

                            frmarrival.lbl_noofnight.Text = txt_noofnights.Text;
                            frmarrival.lbl_noofnight_billing.Text = txt_noofnights.Text;

                            frmarrival.cbo_rtcode.SelectedIndex = -1;
                            frmarrival.cbo_disc.SelectedIndex = -1;
                            frmarrival.txt_discamt.Text = "0";
                            frmarrival.txt_netrt.Text = "0.00";
                            frmarrival.txt_govtrt.Text = "0.00";
                            frmarrival.txt_total_amt.Text = "0.00";

                            //frmarrival.tbcntrl_res.SelectedTab = frm.tpg_reg;
                            //frmarrival.tpg_reg.Show(); 
                        }
                        else if (frmhouse!=null)
                        {
                            int row = dgv_rom_available.CurrentRow.Index;
                            String rom_code = dgv_rom_available.Rows[row].Cells[0].Value.ToString().Trim();
                            String rom_typ = dgv_rom_available.Rows[row].Cells[2].Value.ToString().Trim();

                            frmhouse.lbl_rm.Text = rom_code;
                            frmhouse.lbl_rmtyp.Text = rom_typ;
                            frmhouse.lbl_arrdt.Text = dtp_checkin.Value.ToString("yyyy-MM-dd");
                            frmhouse.lbl_depdt.Text = dtp_checkout.Value.ToString("yyyy-MM-dd");

                            frmhouse.lbl_noofnight.Text = txt_noofnights.Text;
                            frmhouse.lbl_noofnight_billing.Text = txt_noofnights.Text;

                            frmhouse.cbo_rtcode.SelectedIndex = -1;
                            frmhouse.cbo_disc.SelectedIndex = -1;
                            frmhouse.txt_discamt.Text = "0";
                            frmhouse.txt_netrt.Text = "0.00";
                            frmhouse.txt_govtrt.Text = "0.00";
                            frmhouse.txt_total_amt.Text = "0.00";

                        }
                    }
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
            this.Close();
        }

        private void dgv_rom_available_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            load_rom_available();
        }
        void disp_night()
        {
            int checkin = 0, checkout = 0, total = 0;
            checkin = Int32.Parse(dtp_checkin.Value.ToString("dd"));
            checkout = Int32.Parse(dtp_checkout.Value.ToString("dd"));
            total = checkout - checkin;
            TimeSpan diff = dtp_checkout.Value.Subtract(dtp_checkin.Value);
            //String go = Int32.TryParse(diff.ToString());
            MessageBox.Show(diff.ToString());
        }
        private void dtp_checkout_ValueChanged(object sender, EventArgs e)
        {
            compute_noofnights();
            load_rom_available();
        }

        private void compute_noofnights()
        {
            int days = 0;
            double das = 0.00;
            TimeSpan span = dtp_checkout.Value.Subtract(dtp_checkin.Value);
            das = Math.Round(span.TotalDays, 1);
            days = (int)das;
            txt_noofnights.Text = days.ToString();
        }


        private void txt_noofnights_TextChanged(object sender, EventArgs e)
        {
            //compute_NoOfNight();
        }

        private void compute_NoOfNight()
        {
            try
            {
                if (txt_noofnights.Text == string.Empty)
                {
                    txt_noofnights.Text = "1";
                }

                int last_day = 0;
                int extra_days = 0;
                int month_ctr = 0;
                int get_current_day = 0;
                int no_of_nights = Int32.Parse(txt_noofnights.Text);
                int total = 0;
                int diff = 0;
                get_current_day = Int32.Parse(dtp_checkin.Value.ToString("dd"));
                last_day = DateTime.DaysInMonth(Int32.Parse(dtp_checkin.Value.ToString("yyyy")), Int32.Parse(dtp_checkin.Value.ToString("MM")));
                //lbl_newdate.Text = Convert.ToDateTime(db.get_systemdate("")).AddDays(Convert.ToDouble(1)).ToString("yyyy-MM-dd");
                total = get_current_day + no_of_nights;
                if (total > last_day)
                {

                    //String gaw = dtp_checkout.Value.ToString();
                    diff = total - last_day;
                    for (int i = 0; i < (total / last_day); i++)
                    {
                        month_ctr++;
                    }
                    dtp_checkout.Value = dtp_checkin.Value.AddMonths(month_ctr).AddDays((get_current_day * -1) + diff);

                    String gaw = dtp_checkout.Value.ToString();
                }
                else
                {
                    dtp_checkout.Value = dtp_checkin.Value.AddDays(Convert.ToDouble(txt_noofnights.Text));
                }
            }
            catch { }
        }

        private void txt_noofnights_KeyPress(object sender, KeyPressEventArgs e)
        {
           

        }

        private void txt_noofnights_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                compute_NoOfNight();
            }
        }

        private void dtp_checkin_ValueChanged(object sender, EventArgs e)
        {
            compute_noofnights();
            load_rom_available();
        }

        private void cbo_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_rom_available();
        }
    }
}
