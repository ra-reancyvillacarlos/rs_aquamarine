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
    public partial class Main : Form
    {
        z_Notification init_frm;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            init_frm = new z_Notification();

            init_frm.MdiParent = this;

            set_rights(0);
            
            //Home h = new Home();

           // h.MdiParent = this;

            //h.Show();

            lbl_m99company.Text = db.get_m99comp_name();

            lbl_trnxdate.Text = db.get_systemdate("");

            lbl_user.Text = GlobalClass.username;

            bgWorker.RunWorkerAsync();

        }

        //0-access to all; 1 - housekeeping
        public void set_rights(int the_right)
        {
            if (the_right == 0)
            {
                init_frm.Show();

                button2.BackColor = Color.SeaGreen;
                button2.ForeColor = Color.White;

            }
            else if (the_right == 1)
            {
                btn_rmavailability.Enabled = false;
                btn_reservation.Enabled = false;
                btn_arrivals.Enabled = false;
                btn_fcontract.Enabled = false;
                btn_inhouseguest.Enabled = false;
                button1.Enabled = false;
                btn_rmavailability.Enabled = false;

                btn_rmstatus.Enabled = true;
                btn_hkmisc.Enabled = true;
                btn_updrmstatus.Enabled = true;
                button2.Enabled = true;

                reservationToolStripMenuItem.Visible = false;
                arrivalsToolStripMenuItem1.Visible = false;
                //functionContractsToolStripMenuItem.Visible = false;
                reservationToolStripMenuItem2.Visible = false;
                arrivalsToolStripMenuItem.Visible = false;
                frontDeskToolStripMenuItem.Visible = false;
                guestFolioToolStripMenuItem.Visible = false;
                functionContractToolStripMenuItem.Visible = false;
                settingsToolStripMenuItem.Visible = false;

                init_frm.Show();

                button2.BackColor = Color.SeaGreen;
                button2.ForeColor = Color.White;
            }
        }

        public String get_modname()
        {
            return lbl_modname.Text;
        }

        public void set_modname(String name)
        {
            lbl_modname.Text = name;
        }

        private void roomStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoomStatus f = new RoomStatus();

            f.MdiParent = this;

            f.Show();
            
            lbl_modname.Text = "ROOM STATUS";
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void closechild()
        {
            foreach(Form f in this.MdiChildren)
            {
                f.Close();
            }
        }

        private void btn_color_reset()
        {
            btn_rmstatus.BackColor = menuStrip1.BackColor;
            btn_rmstatus.ForeColor = Color.Black;

            btn_rmavailability.BackColor = menuStrip1.BackColor;
            btn_rmavailability.ForeColor = Color.Black;

            btn_reservation.BackColor = menuStrip1.BackColor;
            btn_reservation.ForeColor = Color.Black;

            btn_arrivals.BackColor = menuStrip1.BackColor;
            btn_arrivals.ForeColor = Color.Black;

            btn_inhouseguest.BackColor = menuStrip1.BackColor;
            btn_inhouseguest.ForeColor = Color.Black;

            button1.BackColor = menuStrip1.BackColor;
            button1.ForeColor = Color.Black;

            btn_fcontract.BackColor = menuStrip1.BackColor;
            btn_fcontract.ForeColor = Color.Black;

            btn_hkmisc.BackColor = menuStrip1.BackColor;
            btn_hkmisc.ForeColor = Color.Black;

            btn_updrmstatus.BackColor = menuStrip1.BackColor;
            btn_updrmstatus.ForeColor = Color.Black;

            button2.BackColor = menuStrip1.BackColor;
            button2.ForeColor = Color.Black;
        }

        private void reservationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lbl_modname.Text = "RESERVATION";
            closechild();

            Reservation r = new Reservation(this);
            r.MdiParent = this;
            r.Show();

            btn_color_reset();
            btn_reservation.BackColor = Color.SeaGreen;
            btn_reservation.ForeColor = Color.White;
        }

        private void groupReservationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupReservation gr = new GroupReservation();

            gr.MdiParent = this;

            gr.Show();

            lbl_modname.Text = "GROUP RESERVATION";
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Guest g = new Guest();

            //g.MdiParent = this;

            g.ShowDialog();
        }

        private void roomNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoomNumber rn = new RoomNumber();

            //rn.MdiParent = this;

            rn.ShowDialog();
        }

        private void roomRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoomRate rr = new RoomRate();

            //rr.MdiParent = this;

            rr.ShowDialog();
        }

        private void discountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiscountTypes dt = new DiscountTypes();

            //dt.MdiParent = this;

            dt.ShowDialog();
        }

        private void otherMasterlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtherMasterList oml = new OtherMasterList();
            
            //oml.MdiParent = this;

            oml.ShowDialog();
        }

        private void btn_rmstatus_Click(object sender, EventArgs e)
        {
            RoomStatus rs = new RoomStatus();

            rs.MdiParent = this;

            rs.Show();
        }

        private void btn_grpreservation_Click(object sender, EventArgs e)
        {
            GroupReservation gp = new GroupReservation();

            gp.MdiParent = this;

            gp.Show();
        }

        private void btn_fcontract_Click(object sender, EventArgs e)
        {
            FunctionContract fc = new FunctionContract();

            fc.MdiParent = this;

            fc.Show();
        }

        private void housekeepingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Housekeeping h = new Housekeeping();

            //h.MdiParent = this;

            h.Show();
        }

        private void Main_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(this.MdiChildren.Rank.ToString());
           
                MessageBox.Show(this.ActiveMdiChild.Name);           
           
        }

        private void btn_reservation_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btn_rmstatus_Click_1(object sender, EventArgs e)
        {
            closechild();

            RoomStatus rs = new RoomStatus();

            rs.MdiParent = this;

            rs.Show();
            
            lbl_modname.Text = "ROOM STATUS";

            btn_color_reset();

            btn_rmstatus.BackColor = Color.SeaGreen;
            btn_rmstatus.ForeColor = Color.White;
        }

        private void btn_fcontract_Click_1(object sender, EventArgs e)
        {
            closechild();

            Hk_UtilitiesReading fc = new Hk_UtilitiesReading();

            fc.MdiParent = this;

            fc.Show();

            lbl_modname.Text = btn_fcontract.Text;

            btn_color_reset();

            btn_fcontract.BackColor = Color.SeaGreen;
            btn_fcontract.ForeColor = Color.White;
        }

        private void miscellaneousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_hkmisc_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            closechild();

            rGuestBilling gbill = new rGuestBilling();

            gbill.MdiParent = this;

            gbill.Show();

            lbl_modname.Text = "GUEST BILLING";

            btn_color_reset();

            button1.BackColor = Color.SeaGreen;
            button1.ForeColor = Color.White;
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            closechild();

            Home h = new Home();

            h.MdiParent = this;

            h.Show();

            lbl_modname.Text = "HOME";
        }

        private void btn_arrivals_Click(object sender, EventArgs e)
        {
            
        }

        private void arrivalsWalkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            lbl_modname.Text = "ARRIVALS";

            Arrivals a = new Arrivals(this);
            a.MdiParent = this;
            a.Show();

            btn_color_reset();

            btn_arrivals.BackColor = Color.SeaGreen;
            btn_arrivals.ForeColor = Color.White;
        }

        private void btn_hkmisc_Click_1(object sender, EventArgs e)
        {/*
            closechild();

            Hk_miscellaneous hkmisc = new Hk_miscellaneous();

            hkmisc.MdiParent = this;

            hkmisc.Show();

            lbl_modname.Text = "HK MISCELLANEOUS";

            btn_color_reset();

            btn_hkmisc.BackColor = Color.SeaGreen;
            btn_hkmisc.ForeColor = Color.White;*/
        }

        private void hKMiscellaneousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            Hk_miscellaneous hkmisc = new Hk_miscellaneous();

            hkmisc.MdiParent = this;

            hkmisc.Show();

            lbl_modname.Text = "HK MISCELLANEOUS";
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Group grp = new Group();

            grp.ShowDialog();
        }

        private void cancelReservationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("103");
            rpt.Show();
        }

        private void reservationReportByDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("101");
            rpt.Show();
        }

        private void blockedReservationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("102");
            rpt.Show();
        }

        private void btn_inhouseguest_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_color_selected(String code)
        {
           
        }

        private void btn_updrmstatus_Click(object sender, EventArgs e)
        {
            closechild();
            UpdateRoomStatus urs = new UpdateRoomStatus();
            urs.MdiParent = this;
            urs.Show();
            lbl_modname.Text = "UPDATE ROOM STATUS";

            btn_color_reset();

            btn_updrmstatus.BackColor = Color.SeaGreen;
            btn_updrmstatus.ForeColor = Color.White;
        }

        private void updateRoomStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            UpdateRoomStatus urs = new UpdateRoomStatus();
            urs.MdiParent = this;
            urs.Show();
            lbl_modname.Text = "UPDATE ROOM STATUS";
        }

        private void generateMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            GenerateMealCoupon gmc = new GenerateMealCoupon();
            gmc.MdiParent = this;
            gmc.Show();
            lbl_modname.Text = "Generate Meal Coupon";            
        }

        private void expectedArrivalReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("201");
            rpt.Show();
        }

        private void actualArrivalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("202");
            rpt.Show();
        }

        private void actualDepartureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("203");
            rpt.Show();
        }

        private void inhouseGuestReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("205");
            rpt.Show();
        }

        private void noShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("104");
            rpt.Show();
        }

        private void cashiersReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("301");
            rpt.Show();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime dt_st = new DateTime();
            
            dt_st = DateTime.Now;

            disp_hotelstatus();

            while (true)
            {
                if (dt_st.AddSeconds(5) < DateTime.Now)
                {
                    disp_hotelstatus();
                    dt_st = DateTime.Now;
                }
            }
        }

        private void disp_hotelstatus()
        {
            try
            {
                thisDatabase db = new thisDatabase();
                Boolean isfirst = true;
                String noofrooms = db.get_pk("tot_room");
                String exp_arr = "0";
                String act_arr = "0";
                String exp_dep = "0";
                String act_dep = "0";
                String cur_occ = "0";
                String trnx_dt = db.get_systemdate("");
                Double occ_pct = 0.00;
                int roomtosell = 0;

                exp_arr = db.get_arrivallist("", db.get_systemdate("")).Rows.Count.ToString();
                act_arr = db.get_cntrow("gfolio", "reg_num", "arr_date='" + trnx_dt + "'").ToString();

                exp_dep = db.get_cntrow("gfolio", "reg_num", "dep_date<='" + trnx_dt + "' AND typ_code !='Z'").ToString();
                act_dep = db.get_cntrow("gfhist", "reg_num", "co_date='" + trnx_dt + "'").ToString();

                cur_occ = db.get_cntrow("gfolio", "reg_num", "").ToString();

                occ_pct = (Convert.ToDouble(cur_occ) / Convert.ToDouble(noofrooms)) * 100;
                roomtosell = Convert.ToInt32(noofrooms) - Convert.ToInt32(cur_occ);

                lbl_totalrooms.Invoke(new Action(() =>
                {
                    lbl_totalrooms.Text = noofrooms;
                }));

                lbl_romtosell.Invoke(new Action(() =>
                {
                    lbl_romtosell.Text = roomtosell.ToString();
                }));

                lbl_cur_occ.Invoke(new Action(() =>
                {
                    lbl_cur_occ.Text = cur_occ;
                }));

                lbl_cur_pct.Invoke(new Action(() =>
                {
                    lbl_cur_pct.Text = occ_pct.ToString("0.00"); ;
                }));

                lbl_exp_arr.Invoke(new Action(() =>
                {
                    lbl_exp_arr.Text = exp_arr;
                }));

                lbl_exp_dep.Invoke(new Action(() =>
                {
                    lbl_exp_dep.Text = exp_dep;
                }));

                lbl_act_arr.Invoke(new Action(() =>
                {
                    lbl_act_arr.Text = act_arr;
                }));

                lbl_act_dep.Invoke(new Action(() =>
                {
                    lbl_act_dep.Text = act_dep;
                }));
            }
            catch (Exception) { }
        }

        private void actualDepartureReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("204");
            rpt.Show();
        }

        private void roomTransferReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("206");
            rpt.Show();
        }

        private void cashReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("302");
            rpt.Show();
        }

        private void cashCreditSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rpt_ccsummary cc = new rpt_ccsummary();

            cc.MdiParent = this;

            cc.Show();
        }

        private void creditCardReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rpt_DSR dsr = new rpt_DSR();

            dsr.MdiParent = this;

            dsr.Show();
        }

        private void dailySalesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("305");
            rpt.Show();
        }

        private void monthlySalesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("306");
            rpt.Show();
        }

        private void summaryOfChargesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("307");
            rpt.Show();
        }

        private void viewGuestHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuestHistory gh = new GuestHistory();

            gh.MdiParent = this;
            gh.Show();
        }

        private void guestBalanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rpt_GuestHighBalance gh = new rpt_GuestHighBalance();

            gh.MdiParent = this;
            gh.Show();
        }

        private void transactionLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("403");
            rpt.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }

        private void summaryOfMiscellaneousReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("309");
            rpt.Show();
        }

        private void monthlyChargesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("308");
            rpt.Show();
        }

        private void chargesBalanceTransferedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("310");
            rpt.Show();
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            GenerateRoomCharge grc = new GenerateRoomCharge();
            grc.MdiParent = this;
            grc.Show();
            lbl_modname.Text = "GENERATE ROOM CHARGE";
            btn_color_reset();
        }

        public void refresh_trnx_date()
        {
            thisDatabase db = new thisDatabase();

            lbl_trnxdate.Invoke(new Action(() =>
            {
                lbl_trnxdate.Text = db.get_systemdate("");
            }));
        }

        private void inHouseGuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbl_modname.Text = "IN HOUSE GUEST";
            closechild();
            InHouseGuest igh = new InHouseGuest(this);
            igh.MdiParent = this;
            igh.Show();

            btn_color_reset();

            btn_inhouseguest.BackColor = Color.SeaGreen;
            btn_inhouseguest.ForeColor = Color.White;
        }

        private void roomAvailabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbl_modname.Text = "ROOM AVAILABILITY";
            closechild();
            RoomAvailability igh = new RoomAvailability();
            igh.MdiParent = this;
            igh.Show();

            btn_color_reset();

            btn_rmavailability.BackColor = Color.SeaGreen;
            btn_rmavailability.ForeColor = Color.White;
        }

        private void dSRSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("311");
            rpt.Show();
        }

        private void summaryOfGuestBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("403");
            rpt.Show();
        }

        private void summaryOfGuestBalanceCheckOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("404");
            rpt.Show();
        }

        private void dailySalesReportCheckOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("305B");
            rpt.Show();
        }

        private void extraItemReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("312");
            rpt.Show();
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("205B");
            rpt.Show();
        }

        private void guestFolioByAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("207");
            rpt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbl_modname.Text = button2.Text;
            closechild();

            z_Notification m_frm = new z_Notification();

            m_frm.MdiParent = this;

            m_frm.Show();

            btn_color_reset();

            button2.BackColor = Color.SeaGreen;
            button2.ForeColor = Color.White;
        }

        private void functionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void functionRoomAmenitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dailyRemittanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("314");
            rpt.Show();
        }

        private void laundryOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void laundryEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void arrivalsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void hotelSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z_HotelSettings zh = new z_HotelSettings();

            zh.ShowDialog();
        }

        private void monthlySalesReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("306_ga");
            rpt.Show(); 
        }

        private void userSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void systemDataUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z_SystemDataUpdate frm = new z_SystemDataUpdate();


            frm.ShowDialog();
        }

        private void hotelChargesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void companiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void maintenancePersonnelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void shiftDetailReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void miscelleneousAddOnsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void laundrySalesReprotToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reservationAdvancePaymentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void functionContractPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void occupancyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void housekeepingReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
