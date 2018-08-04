using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Deployment.Application;

namespace Accounting_Application_System
{
    public partial class Main : Form
    {
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        Report _Report;
        public Color colormain;
        public Color color2;
        private String company = "3"; //1A-Construction = GOLDFIN, 1B= 2-Hotel = Basic Residential Vistana  3-Hotel = Standard GrandApartelle 4. POS/Inventory = Headway 5. Car Services POS Inventory Accounting System = BALAI

        public Main()
        {
            InitializeComponent();
            //menu rights code dynimically

            //
            color2 = new Color();
            colormain = new Color();
            _Report = new Report();
            db = new thisDatabase();
            gm = new GlobalMethod();
            GlobalClass.user_fullname = db.getFullName();
            GlobalClass.projcompany = company;
            gc = new GlobalClass();
            String verion = "";

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                verion = String.Format("Version {0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
            }



            this.Text = this.Text + " " + verion + " - Branch: " + db.get_branchname(db.get_m99branch())+" - Server IP: "+ GlobalClass.server_ip;
            lbl_m99company.Text = db.get_m99comp_name();
            lbl_trnxdate.Text = db.get_systemdate("ddd, MM/dd/yy");
            lbl_user.Text = GlobalClass.username;

            Hotel_System.GlobalClass.user_fullname = GlobalClass.user_fullname;
            Hotel_System.GlobalClass.username = GlobalClass.username;


            if (company == "1")
            {
                btn_1.Text = "Purch. Request";
                btn_2.Text = "Purchae Order";
                btn_3.Text = "Receiving PO";
                btn_4.Text = "Direct Purchase";
                btn_5.Text = "Purchase Return";
                btn_6.Text = "Stock Issuance";
                btn_8.Text = "Stock Transfer";
                btn_9.Text = "Item Trans.Card";
                btn_10.Text = "Item Search";

                btn_1.Image = Properties.Resources.document_construction_icon___38x38;
                btn_2.Image = Properties.Resources.payment_icon___30x30;
                btn_3.Image = Properties.Resources.soa___30x30;
                btn_5.Image = Properties.Resources._1399425964_van;
                btn_6.Image = Properties.Resources.aging_report;
                btn_8.Image = Properties.Resources.room_transfer_2;
                btn_9.Image = Properties.Resources.aging_report;
                btn_10.Image = Properties.Resources.search_32x32;

                fmi_s000.Visible = false;
                fmi_m300.Visible = false;
            }
            else if (company == "3")
            {

            }
            else if (company == "4")
            {
                btn_1.Text = "Sales Order";
                btn_2.Text = "Purchae Order";
                btn_3.Text = "Receiving PO";
                btn_4.Text = "Direct Purchase";
                btn_5.Text = "Purchase Return";
                btn_6.Text = "Stock Issuance";
                btn_8.Text = "Stock Transfer";
                btn_9.Text = "Item Trans.Card";
                btn_10.Text = "Item Search";

                btn_1.Image = Properties.Resources.cashbox;
                btn_2.Image = Properties.Resources.payment_icon___30x30;
                btn_3.Image = Properties.Resources.soa___30x30;

                fmi_a000.Visible = false;

                journalizeSalesToolStripMenuItem.Visible = false;
                journalizedDirectPurchaseToolStripMenuItem.Visible = false;
                journalizedToolStripMenuItem.Visible = false;
                toolStripMenuItem10.Visible = false;
                toolStripMenuItem6.Visible = false;
                fmi_m100.Visible = false;
            }
            else if (company == "5")
            {
                btn_1.Text = "Auto Loan Status";
                btn_2.Text = "Parts Entry";
                btn_3.Text = "Parts Cashier";
                btn_4.Text = "Service Status";
                btn_5.Text = "Service Entry";
                btn_6.Text = "Service Cashier";
                btn_8.Text = "Maintenance";
                btn_9.Text = "Service History";
                btn_10.Text = "Vehicle Search";
                btn_11.Text = "Item Search";
                btn_12.Text = "Menu Search";

                btn_1.Image = Properties.Resources.car_loan_32;
                btn_2.Image = Properties.Resources.cashbox;
                btn_3.Image = Properties.Resources.payment_icon___30x30;
                btn_4.Image = Properties.Resources.service_status;

                btn_5.Image = Properties.Resources.car_repair_blue_32;
                btn_6.Image = Properties.Resources.payment_icon___30x30;
                btn_8.Image = Properties.Resources.car_technician_icon_28___36;
                btn_9.Image = Properties.Resources.landing_icons_auto_search___34;
                btn_10.Image = Properties.Resources.search_32x32;

            }
            else
            {
            }

            //back office main office
            if (GlobalClass.branch == "001")
            {
                //this.Text = this.Text + " | Back Office";
            }
            //branches
            else
            {
                if (this.company == "3")
                {
                    fmi_m100.Visible = false;
                    fmi_a000.Visible = false;
                }
            }

            ////////////////////////////USER RIGHTS/////////////////////////////////////
            try
            {
                DataTable grpid_dt = db.QueryBySQLCode("SELECT grp_id,d_code FROM rssys.x08 WHERE uid = '" + GlobalClass.username + "'");
                if (grpid_dt.Rows.Count > 0)
                {
                    String grp_id = grpid_dt.Rows[0]["grp_id"].ToString();
                    String grp_desc = grpid_dt.Rows[0]["d_code"].ToString();
                    /*
                    "001";"ADMINISTRATORS"
                    "002";"ACCOUNTING"
                    "003";"HOTEL"
                    "004";"FINANCE"
                    "005";"PURCHASING"
                    */

                    if (grp_id == "001")
                    {

                    }
                    else if (grp_id == "002")
                    {


                    }
                    else if (grp_id == "003")
                    {
                        fmi_m100.Visible = false;
                        fmi_m400.Visible = false;
                        fmi_a000.Visible = false;
                        fmi_i000.Visible = false;
                        fmi_r_a000.Visible = false;
                        fmi_r_i000.Visible = false;
                    }
                    else if (grp_id == "004")
                    {

                    }
                    else if (grp_id == "005")
                    {
                        btn_7.Enabled = false;
                        btn_1.Enabled = false;
                        btn_2.Enabled = false;
                        btn_4.Enabled = false;
                        btn_3.Enabled = false;

                        btn_5.Enabled = false;
                        btn_6.Enabled = false;
                        btn_8.Enabled = false;
                        btn_9.Enabled = false;


                        fmi_m200.Visible = false;
                        fmi_pms000.Visible = false;
                    }
                }
            }
            catch { }

        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            z_Notification jsi = new z_Notification();



            jsi.MdiParent = this;

            lbl_modname.Text = btn_7.Text;

            btn_color_reset();

            btn_7.BackColor = panel2.BackColor;

            jsi.Show();

            try
            {
                //TRAVERSE MENU             
                if (GlobalClass.username.ToUpper() != "ADMIN")
                {
                    this.Visible = false;
                    //traverse_menu_rights();
                    this.Visible = true;
                }

                // if (bgWorker.IsBusy == false)
                //   bgWorker.RunWorkerAsync();

            }
            catch (Exception) { }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (CloseCancel() == false)
            {
                e.Cancel = true;
            };
        }

        public static bool CloseCancel()
        {
            const string message = "Are you sure that you would like to close this application?";
            const string caption = "Close Right Apps";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        public String get_modname()
        {
            return lbl_modname.Text;

        }

        public void set_modname(String name)
        {
            lbl_modname.Text = name;
        }


        private void traverse_menu_rights()
        {
            String lvl1 = "1";
            String lvl2 = "2";
            String lvl3 = "3";
            Boolean hide = false;
            // GET THE LOGIN USER GROUP SETTINGS grp_id
            DataTable grpid_dt = db.QueryBySQLCode("SELECT grp_id FROM rssys.x08 WHERE uid = '" + GlobalClass.username + "'");
            String grp_id = grpid_dt.Rows[0]["grp_id"].ToString();

            foreach (ToolStripMenuItem menu in menuStrip1.Items)
            {
                hide = check_module_level_rights(menu.Text, lvl1);
                if (hide == true)
                {
                    menu.Visible = false;
                }
                else
                {
                    foreach (ToolStripMenuItem submenu in menu.DropDownItems)
                    {
                        hide = check_module_level_rights(submenu.Text, lvl2);
                        if (hide == true)
                        {
                            //s
                            submenu.Visible = false;
                        }
                        else
                        {
                            foreach (ToolStripMenuItem level3 in submenu.DropDownItems)
                            {
                                hide = check_module_level_rights(level3.Text, lvl3);
                                if (hide == true)
                                {
                                    level3.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private Boolean check_module_level_rights(String mod_name, String level)
        {
            Boolean ok = false;
            String res = "";
            DataTable dt = db.QueryBySQLCode("SELECT a.*, b.grp_desc,b.level FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id WHERE b.grp_desc ='" + mod_name + "'  and b.level ='" + level + "'");
            if (dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    res = dt.Rows[r]["restrict"].ToString();
                    if (res == "n")
                    {
                        ok = true;
                    }
                }
            }
            return ok;
        }

        void btn_1_Click(object sender, EventArgs e)
        {
            closechild();

            Hotel_System.RoomStatus rs = new Hotel_System.RoomStatus();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = "UNIT STATUS";

            btn_color_reset();

            btn_1.BackColor = panel2.BackColor;


        }

        void btn_2_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "UNIT AVAILABILITY";
            Hotel_System.RoomAvailability igh = new Hotel_System.RoomAvailability();
            igh.MdiParent = this;
            igh.Show();
            btn_color_reset();

            btn_2.BackColor = panel2.BackColor;
        }

        void btn_3_Click(object sender, EventArgs e)
        {
            //closechild();
            //Hotel_System.newReservation fm = new Hotel_System.newReservation();
            //fm.MdiParent = this;
            //fm.Show();
            lbl_modname.Text = "RESERVATION";
            closechild();
            Hotel_System.newReservation r = new Hotel_System.newReservation();
            r.cur_load = true;
            r.MdiParent = this;
            r.disp_cur();
            r.Show();
            btn_color_reset();

            btn_3.BackColor = panel2.BackColor;
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "CONTRACT ENTRY / CHECK IN";
            //Hotel_System.newArrivalWalkin fm = new Hotel_System.newArrivalWalkin();
            //fm.MdiParent = this;
            //fm.Show();
            Hotel_System.newReservation r = new Hotel_System.newReservation();
            r.cur_load = false;
            r.MdiParent = this;
            r.disp_cur();
            r.Show();

            btn_color_reset();

            btn_4.BackColor = panel2.BackColor;
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "CONTRACT UPDATE / IN-HOUSE UPDATE";
            Hotel_System.newInhouse fm = new Hotel_System.newInhouse();
            fm.MdiParent = this;
            fm.Show();
            btn_color_reset();

            btn_5.BackColor = panel2.BackColor;
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "Activity and Payment Records";
            Hotel_System.newGuestBilling gbill = new Hotel_System.newGuestBilling();
            gbill.forView = false;
            gbill.MdiParent = this;

            gbill.Show();
            btn_color_reset();

            btn_6.BackColor = panel2.BackColor;
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            closechild();

            z_Notification jsi = new z_Notification();

            jsi.MdiParent = this;

            lbl_modname.Text = btn_7.Text;

            btn_color_reset();

            btn_7.BackColor = panel2.BackColor;

            jsi.Show();
        }

        void btn_8_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "UTILITIES";
            Hotel_System.Hk_UtilitiesReading fc = new Hotel_System.Hk_UtilitiesReading();

            fc.MdiParent = this;

            fc.Show();
            btn_color_reset();

            btn_8.BackColor = panel2.BackColor;

        }

        void btn_9_Click(object sender, EventArgs e)
        {
            closechild();
            //lbl_modname.Text = "UPDATE UNIT STATUS";
            //Hotel_System.UpdateRoomStatus urs = new Hotel_System.UpdateRoomStatus();
            //urs.MdiParent = this;
            //urs.Show();

            lbl_modname.Text = "Activity and Payment Records";
            Hotel_System.newGuestBilling gbill = new Hotel_System.newGuestBilling();
            gbill.forView = true;
            gbill.forVv();
            gbill.MdiParent = this;

            gbill.Show();

            btn_color_reset();

            btn_9.BackColor = panel2.BackColor;
        }

        private void btn_10_Click(object sender, EventArgs e)
        {
            z_ItemSearch frm = new z_ItemSearch(1);

            frm.Show();
        }

        private void btn_11_Click(object sender, EventArgs e)
        {
            z_ItemSearch frm = new z_ItemSearch(2);

            frm.Show();
        }

        private void btn_12_Click(object sender, EventArgs e)
        {
            z_ItemSearch frm = new z_ItemSearch(0);

            frm.Show();
        }

        private void closechild()
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
        }

        private void btn_color_reset()
        {
            btn_1.BackColor = menuStrip1.BackColor;
            btn_2.BackColor = menuStrip1.BackColor;
            btn_3.BackColor = menuStrip1.BackColor;
            btn_4.BackColor = menuStrip1.BackColor;
            btn_5.BackColor = menuStrip1.BackColor;
            btn_6.BackColor = menuStrip1.BackColor;
            btn_7.BackColor = menuStrip1.BackColor;
            btn_8.BackColor = menuStrip1.BackColor;
            btn_9.BackColor = menuStrip1.BackColor;
            btn_10.BackColor = menuStrip1.BackColor;
            btn_11.BackColor = menuStrip1.BackColor;
            btn_12.BackColor = menuStrip1.BackColor;
            btn_13.BackColor = menuStrip1.BackColor;
            btn_14.BackColor = menuStrip1.BackColor;
        }


        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        public void auto_email_send()
        {
            #region AutomaticEmail
            /*
            try
            {
                this.Invoke(new Action(() =>
                {
                    EmailAutomationClass.Class1 t = new EmailAutomationClass.Class1();
                    externalDatabase.ga_hotelEntities all = new externalDatabase.ga_hotelEntities(thisDatabase.local);
                    var temp = all.emails.FirstOrDefault();

                    if (temp != null)
                    {
                        try
                        {

                            //if (DateTime.Now.TimeOfDay.Hours == 14 && DateTime.Now.Minute == 15)
                            //{
                            //    Generate report here
                            //}
                            var savedTime = temp.mailingTimeSchedule;

                            if (DateTime.Now.ToString("HH:mm") == savedTime)
                            {
                                if (t.sendEmailPreparation(DateTime.Now.ToString("MM-dd-yyyy"),
                                    temp.sender,
                                    temp.password,
                                    temp.receiverEmail1,
                                    temp.smtp,
                                    temp.email_provider,
                                    temp.port_number.Value,
                                    temp.content,
                                    temp.subject))
                                {
                                    MessageBox.Show("Sent!");
                                }

                                else
                                {
                                    if (t.sendEmailPreparation(DateTime.Now.ToString("MM-dd-yyyy"),
                                    temp.sender,
                                    temp.password,
                                    temp.receiverEmail2,
                                    temp.smtp,
                                    temp.email_provider,
                                    temp.port_number.Value,
                                    temp.content,
                                    temp.subject))
                                    {
                                        MessageBox.Show("Sent!");
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }));
            }
            catch(Exception er)
            {
                MessageBox.Show("Error on Automatic Email. \n" + er.Message);
            } */
            #endregion
        }

        void staffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_clerk frm = new m_clerk();

            frm.ShowDialog();
        }

        private void journalEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            a_journalentry rs = new a_journalentry();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = "Journal Entry";

            btn_color_reset();

            btn_1.BackColor = panel2.BackColor;
        }

        private void statementOfAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            a_statementofaccount rs = new a_statementofaccount();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = btn_2.Text;

            btn_color_reset();
        }

        private void postingToGeneralLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            a_CollectionEntry rs = new a_CollectionEntry();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = btn_3.Text;

            btn_color_reset();
        }

        private void accountActivityByIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rpt_Global rpt = new rpt_Global("101");
            rpt.Show();
        }

        private void journalizeSaleInvoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            z_Jrnlz_SalesInvoices jsi = new z_Jrnlz_SalesInvoices();

            jsi.MdiParent = this;

            lbl_modname.Text = "Journalize Sale Invoices";

            btn_color_reset();

            jsi.Show();
        }

        private void journalizeCostOfSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            z_Jrnlz_CostOfSales jsi = new z_Jrnlz_CostOfSales();

            jsi.MdiParent = this;

            lbl_modname.Text = "Journalize Cost of Sales";

            btn_color_reset();

            jsi.Show();
        }

        private void journalizeStockTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            z_Jrnlz_StockTransaction jsi = new z_Jrnlz_StockTransaction();

            jsi.MdiParent = this;

            lbl_modname.Text = "Journalize Stock Transactions";

            btn_color_reset();

            jsi.Show();
        }

        private void journalizeDirectPurchasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            z_Jrnlz_DirectPurchase jsi = new z_Jrnlz_DirectPurchase();

            jsi.MdiParent = this;

            lbl_modname.Text = "Journalize Direct Purchases";

            btn_color_reset();

            jsi.Show();
        }

        private void journalizePurchaseReturnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            z_Jrnlz_PurchaseReturns jsi = new z_Jrnlz_PurchaseReturns();

            jsi.MdiParent = this;

            lbl_modname.Text = "Journalize Purchase Returns";

            btn_color_reset();

            jsi.Show();
        }

        private void uploadStockToOtherSitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            z_Jrnlz_UploadStockToOtherSites jsi = new z_Jrnlz_UploadStockToOtherSites();

            jsi.MdiParent = this;

            lbl_modname.Text = "Upload Stocks to other Sites";

            btn_color_reset();

            jsi.Show();
        }

        private void exportFileToUploadStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            z_Jrnlz_ExportFileToUploadStocks jsi = new z_Jrnlz_ExportFileToUploadStocks();

            jsi.MdiParent = this;

            lbl_modname.Text = "Export File to Upload Stocks";

            btn_color_reset();

            jsi.Show();
        }

        private void importFileToUploadStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            z_Jrnlz_ImportFileToUploadStocks jsi = new z_Jrnlz_ImportFileToUploadStocks();

            jsi.MdiParent = this;

            lbl_modname.Text = "Export File to Upload Stocks";

            btn_color_reset();


            jsi.Show();
        }

        private void statementOfAccountToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            closechild();

            a_statementofaccount jsi = new a_statementofaccount();

            jsi.MdiParent = this;

            lbl_modname.Text = "Statement of Account";

            btn_color_reset();


            jsi.Show();
        }

        private void mainAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_mainaccounts frm = new m_mainaccounts();

            frm.ShowDialog();
        }

        private void subAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_subaccounts frm = new m_subaccounts();

            frm.ShowDialog();
        }

        private void accountTittleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_accounttitles frm = new m_accounttitles();

            frm.ShowDialog();
        }

        private void journalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_journal frm = new m_journal();

            frm.ShowDialog();
        }

        private void customersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_customers frm = new m_customers();

            frm.ShowDialog();
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_suppliers frm = new m_suppliers();

            frm.ShowDialog();
        }

        private void costCentersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_costcenters frm = new m_costcenters();

            frm.ShowDialog();
        }

        private void accountingPeriodsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_accountingperiods frm = new m_accountingperiods();

            frm.ShowDialog();
        }

        private void collectionEntryhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a_CollectionEntry frm = new a_CollectionEntry();

            closechild();

            lbl_modname.Text = frm.Text;

            btn_color_reset();

            frm.MdiParent = this;

            frm.Show();
        }

        private void userConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z_user_settings frm = new z_user_settings();

            frm.ShowDialog();
        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_PO frm = new i_PO();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void directPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_DirectPurchase frm = new i_DirectPurchase();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void purchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_Purch_Orders frm = new i_Purch_Orders();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void stockIssuanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_StockIssuance frm = new i_StockIssuance();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void stockTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_StockTransfer frm = new i_StockTransfer();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void receivingStockTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_StockTransfer frm = new i_StockTransfer(true);

            lbl_modname.Text = "Receiving " + frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void stockAdjustmenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_StockAdjustment frm = new i_StockAdjustment();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void itemGroupsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_itemgroup frm = new m_itemgroup();

            frm.ShowDialog();
        }

        private void brandNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_brand frm = new m_brand();

            frm.ShowDialog();
        }

        private void itemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_item frm = new m_item();

            frm.ShowDialog();
        }

        private void stockLocationsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_stocklocation frm = new m_stocklocation();

            frm.ShowDialog();
        }

        private void modeOfPaymenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_modeofpayment frm = new m_modeofpayment();

            frm.ShowDialog();
        }

        private void vATCodesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_vat frm = new m_vat();

            frm.ShowDialog();
        }

        private void salesEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            s_Sales_Auto frm = new s_Sales_Auto();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void saleReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z_ItemSearch frm = new z_ItemSearch();

            frm.ShowDialog();
        }

        private void salesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            s_SalesReturn frm = new s_SalesReturn();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void journalizeSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            journalize frm = new journalize("S");
            try { frm.ShowDialog(); }
            catch { }
        }

        private void aboutAccountingApplicationSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 frm = new AboutBox1();

            frm.ShowDialog();
        }

        private void hotelParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z_accounting_system_settings frm = new z_accounting_system_settings();

            frm.ShowDialog();
        }

        private void collectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_collector frm = new m_collector();

            frm.ShowDialog();
        }

        private void accountGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_accountgroup frm = new m_accountgroup();

            frm.ShowDialog();
        }

        private void recievingPurchaseOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_ReceivingPurchase frm = new i_ReceivingPurchase();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void userRightsSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z_user_rights frm = new z_user_rights();

            frm.Show();
        }



        private void prchaseRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_purchaseRequest frm = new i_purchaseRequest();

            frm.MdiParent = this;

            lbl_modname.Text = "Purchase Request";

            frm.Show();
        }

        private void stockTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_stkcrd_frm frm = new i_stkcrd_frm();

            frm.MdiParent = this;

            lbl_modname.Text = "Stock Transactions";

            frm.Show();
        }

        private void journalizedDirectPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            journalize frm = new journalize("P");
            try { frm.ShowDialog(); }
            catch { }
        }

        private void journalizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            journalize frm = new journalize("ST");
            try { frm.ShowDialog(); }
            catch { }
        }

        private void journalEntryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            closechild();

            a_journalentry rs = new a_journalentry();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = "Journal Entry";

            btn_color_reset();
        }

        private void statementOfAccountToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            closechild();

            a_statementofaccount rs = new a_statementofaccount();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = btn_2.Text;

            btn_color_reset();
        }

        private void collectionEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            a_CollectionEntry rs = new a_CollectionEntry();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = btn_3.Text;
        }

        private void dailyCollectionReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A001");
            rpt.Show();
        }

        private void byNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I011");
            rpt.Show();
        }

        private void byNumberToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I012");
            rpt.Show();
        }

        private void byProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I014");
            rpt.Show();
        }

        private void byItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I022");
            rpt.Show();
        }

        private void journalEntryToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();

            a_journalentry frm = new a_journalentry();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void postToLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();

            a_postingtoledger frm = new a_postingtoledger();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void statementOfAccountToolStripMenuItem_Click_3(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();

            a_statementofaccount frm = new a_statementofaccount();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void outletToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_outlet mout = new m_outlet();
            mout.Show();
        }

        private void discountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_discount frm = new m_discount();
            frm.Show();
        }

        private void accountActivityByIDToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A101");
            rpt.Show();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A104");
            rpt.Show();
        }

        private void journalizeHotelTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            journalize frm = new journalize("H");
            try { frm.ShowDialog(); }
            catch { }
        }

        private void byDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I021");
            rpt.Show();
        }

        private void byDateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I031");
            rpt.Show();
        }

        private void byRRNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I032");
            rpt.Show();
        }

        private void customersLedgerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            closechild();

            rpt_customerledger rs = new rpt_customerledger();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = "Customer's Ledger";

            btn_color_reset();
        }

        private void balancesFromCustomerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A402");

            rpt.Show();
        }

        private void suppliersLedgerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            closechild();

            rpt_supplierledger rs = new rpt_supplierledger();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = "Supplier's Ledger";

            btn_color_reset();

        }

        private void balancesToSupplierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A405");
            rpt.Show();
        }

        private void itemTransactionCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            i_stkcrd_frm frm = new i_stkcrd_frm();

            frm.MdiParent = this;

            lbl_modname.Text = "Stock Transactions";

            frm.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void byItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I013");
            rpt.Show();
        }

        private void bySupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I023");
            rpt.Show();
        }

        private void byItemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I024");
            rpt.Show();
        }

        private void byItemsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I033");
            rpt.Show();
        }

        private void viewByNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I041");
            rpt.Show();
        }

        private void byPurchaseNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I042");
            rpt.Show();
        }

        private void viewBySupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I043");
            rpt.Show();
        }

        private void viewByItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I044");
            rpt.Show();
        }

        private void byDateToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I071");
            rpt.Show();
        }

        private void byAdjNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I072");
            rpt.Show();
        }

        private void byItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I073");
            rpt.Show();
        }

        private void byDateToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I051");
            rpt.Show();
        }

        private void byIssNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I052");
            rpt.Show();
        }

        private void byItemToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I053");
            rpt.Show();
        }

        private void byProjectsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I054");
            rpt.Show();
        }

        private void byDateToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I061");
            rpt.Show();
        }

        private void byTransNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I062");
            rpt.Show();
        }

        private void byItemToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I063");
            rpt.Show();
        }

        private void inventorySummaryByDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I01");
            rpt.Show();
        }

        private void inventoryValuationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I02");
            rpt.Show();
        }

        private void reorderReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("I03");
            rpt.Show();
        }

        private void outletToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("S011");
            rpt.Show();
        }

        private void outletSalesReportByItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("S012");
            rpt.Show();
        }

        private void salesReportByItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("S013");
            rpt.Show();
        }

        private void salesReportByStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("S014");
            rpt.Show();
        }

        private void salesReportSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("S015");
            rpt.Show();
        }

        private void summaryOfItemSoldMaterialUsageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("S016");
            rpt.Show();
        }

        private void viewGLByJournalidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A302");
            rpt.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A102");
            rpt.Show();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A103");
            rpt.Show();
        }

        private void byEntryDateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A201");
            rpt.Show();
        }

        private void byCheckDateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A202");
            rpt.Show();
        }

        private void byCheckNumberReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A203");
            rpt.Show();
        }


        private void generalJournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A300");
            rpt.Show();
        }


        private void viewGLActivityByAccountIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A301");
            rpt.Show();
        }

        private void gLSummaryByAccountIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A303");
            rpt.Show();
        }

        private void customersAgingReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A403S");
            rpt.Show();
        }

        private void agingReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A406S");
            rpt.Show();
        }

        private void trialBalanceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A501");
            rpt.Show();
        }
        private void adjustedBalanceSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A502a");
            rpt.Show();
        }
        private void balanceSheetListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A502l");
            rpt.Show();
        }
        private void balanceSheetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A502");
            rpt.Show();
        }

        private void comparativeIncomeStatementPerCostCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A502m3");
            rpt.Show();
        }

        private void comparativeMonthlyBalanceSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A502y");
            rpt.Show();
        }


        private void incomeStatementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A503");
            rpt.Show();
        }

        private void incomeStatementListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A503l");
            rpt.Show();
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_suppliers frm = new m_suppliers();

            frm.ShowDialog();
        }

        private void enginnerStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*m_clerk frm = new m_clerk();

            frm.ShowDialog();*/
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_costcenters frm = new m_costcenters();

            frm.Text = "Project Category";

            frm.ShowDialog();
        }

        private void itemSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z_ItemSearch frm = new z_ItemSearch();

            frm.ShowDialog();
        }

        private void itemUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_itemunit frm = new m_itemunit();

            frm.ShowDialog();
        }

        private void stockIssuanceToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            //btn_1.BackColor = panel2.BackColor;

            if (GlobalClass.projcompany == "1")
            {
                i_StockIssuance frm = new i_StockIssuance();


                lbl_modname.Text = frm.Text;

                frm.MdiParent = this;

                lbl_modname.Text = frm.Text;

                frm.MdiParent = this;

                frm.Show();
            }
        }

        private void byDateToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("P101");
            rpt.Show();
        }

        private void projectNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("P102");
            rpt.Show();
        }

        private void byItemsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("P103");
            rpt.Show();
        }

        private void summaryOfProjectCostingPerItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("P201");
            rpt.Show();
        }

        private void byItemGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("P104");
            rpt.Show();
        }

        private void byProjectsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("P105");
            rpt.Show();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //m_customers frm = new m_customers();

            m_auto_customer frm = new m_auto_customer();
            frm.ShowDialog();
        }

        private void costCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_costcenters frm = new m_costcenters();

            frm.ShowDialog();
        }

        private void subCostCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_subcostcenters frm = new m_subcostcenters();

            frm.ShowDialog();
        }

        private void printBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting_Application_System.MD.PrintBarcode frm = new MD.PrintBarcode();

            frm.ShowDialog();
        }

        private void printBarcode2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rpt_PrintBarcode rpt = new rpt_PrintBarcode(1);

            rpt.Show();
        }

        private void textBlastToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_vehiclec_info frm = new m_vehiclec_info();
            frm.ShowDialog();
        }

        private void servexMembershipToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_membership frm = new m_membership();
            frm.ShowDialog();
        }

        private void warrantyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void brandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_brand frm = new m_brand();

            frm.ShowDialog();
        }

        private void typeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_type frm = new m_type();

            frm.ShowDialog();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_color frm = new m_color();

            frm.ShowDialog();
        }

        private void salesAgentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_salesagent frm = new m_salesagent();
            frm.ShowDialog();
        }

        private void fmi_m300_Click(object sender, EventArgs e)
        {

        }

        private void warehouseServicingDispatchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void carDealerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_company frm = new m_company();
            frm.ShowDialog();
        }

        private void assembledItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_assembleditem frm = new m_assembleditem();

            frm.ShowDialog();
        }

        private void repairOrderEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void repairOrderEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            btn_5.BackColor = panel2.BackColor;
            lbl_modname.Text = btn_5.Text;

            s_RepairOrder frm = new s_RepairOrder(false);
            frm.MdiParent = this;
            frm.Show();
        }

        private void performMaintenanceRepairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();

            s_ServicesDispatch frm = new s_ServicesDispatch();
            lbl_modname.Text = frm.Text;
            frm.MdiParent = this;
            frm.Show();
        }

        private void cashierToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salesInvoiceEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            btn_2.BackColor = panel2.BackColor;
            lbl_modname.Text = btn_2.Text;

            s_Sales frm = new s_Sales(false, false);
            frm.MdiParent = this;
            frm.Show();
        }

        private void salesCashierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            btn_3.BackColor = panel2.BackColor;
            lbl_modname.Text = btn_3.Text;

            s_Sales frm = new s_Sales(false, true);
            frm.MdiParent = this;
            frm.Show();
        }

        private void updateAutoLoanStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void autoLoanStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            btn_1.BackColor = panel2.BackColor;
            lbl_modname.Text = btn_1.Text;

            s_AutoLoanStatus frm = new s_AutoLoanStatus();
            frm.MdiParent = this;
            frm.Show();
        }

        private void vehicleSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void repairOrderStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            btn_4.BackColor = panel2.BackColor;
            lbl_modname.Text = btn_4.Text;

            s_ROStatus frm = new s_ROStatus();
            frm.MdiParent = this;
            frm.Show();
        }

        private void serviceBillingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            btn_6.BackColor = panel2.BackColor;
            lbl_modname.Text = btn_6.Text;

            s_RepairOrder frm = new s_RepairOrder(true);
            frm.MdiParent = this;
            frm.Show();
        }

        private void textBlastToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            closechild();

            crm_TextBlast_Scheduling rs = new crm_TextBlast_Scheduling();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = rs.Text;

            btn_color_reset();
        }

        private void textMessageTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBlastTemplate tbt = new TextBlastTemplate();
            tbt.ShowDialog();

        }

        private void emailBlastToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void emailBlastTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void prospectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();

            m_auto_customer frm = new m_auto_customer();
            frm.ShowDialog();
        }

        private void calendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            //
            z_Calendar2 frm = new z_Calendar2(GlobalClass.branch, "ALL");
            frm.MdiParent = this;
            frm.Show();
            //frm.ShowDialog();
        }

        private void textBlastTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TextBlastTemplate tbt = new TextBlastTemplate();
            tbt.ShowDialog();
        }

        private void textBlastHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void emailCampaignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            EmailBlastSchedule rs = new EmailBlastSchedule();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = rs.Text;

            btn_color_reset();
        }

        private void emailCampaignTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void emailCampaignHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void calListActivityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void genericToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_generic frm = new m_generic();

            frm.ShowDialog();
        }

        private void vehicleSearchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            z_ItemSearch frm = new z_ItemSearch(1);

            frm.Show();
        }

        private void loanApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_auto_customer frm = new m_auto_customer();

            frm.Show();
        }

        private void autoLoanApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            lbl_modname.Text = "Auto Loan Application";

            auto_loanapplication frm = new auto_loanapplication();
            frm.MdiParent = this;
            frm.Show();
        }

        private void addressBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();

            m_auto_customer frm = new m_auto_customer();
            frm.ShowDialog();
        }

        private void taxParamaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_vat frm = new m_vat();
            frm.ShowDialog();
        }

        private void servicePerformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_workperform frm = new m_workperform();
            frm.ShowDialog();
        }

        private void technicianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_technician frm = new m_technician();
            frm.ShowDialog();
        }

        private void repairOrderStatusToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_ro_status frm = new m_ro_status();
            frm.ShowDialog();
        }

        private void disbursementEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            a_disbursement rs = new a_disbursement();

            rs.MdiParent = this;

            rs.Show();

            lbl_modname.Text = rs.Text;

            btn_color_reset();
        }

        private void requirementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_requirements frm = new m_requirements();
            frm.ShowDialog();
        }

        private void conditionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_cond_approval frm = new m_cond_approval();
            frm.ShowDialog();
        }

        private void loanItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            auto_loan_items frm = new auto_loan_items();
            frm.ShowDialog();
        }
        private void autoSalesCashierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            s_Sales_Auto frm = new s_Sales_Auto(false, true);

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void carReleaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            s_Sales_Auto frm = new s_Sales_Auto(true, false);

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void autoSalesCashierToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            s_Sales_Auto frm = new s_Sales_Auto(false, true);

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void carReleaseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            s_Release_Deliver_Unit frm = new s_Release_Deliver_Unit(true, false);

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void categoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tbCategory tc = new tbCategory();
            tc.ShowDialog();
        }

        private void categoryToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            tbCategory tc = new tbCategory();
            tc.ShowDialog();
        }

        private void categoryToolStripMenuItem1_Click_2(object sender, EventArgs e)
        {
            tbCategory tc = new tbCategory();
            tc.ShowDialog();
        }

        private void fmi_c000_Click(object sender, EventArgs e)
        {

        }

        private void carSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void concernsToDosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //to_do frm = new to_do();
            //frm.ShowDialog();
            closechild();

            btn_color_reset();

            to_do frm = new to_do();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void serviceHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            s_ServiceHistory jsi = new s_ServiceHistory();

            jsi.MdiParent = this;

            lbl_modname.Text = btn_9.Text;

            btn_color_reset();

            jsi.Show();
        }

        private void menuSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z_ItemSearch frm = new z_ItemSearch(0);

            frm.Show();
        }

        private void callHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            closechild();

            btn_color_reset();

            call_history frm = new call_history();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void cashiersReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("S011B");
            rpt.Show();
        }

        private void calendarAutoSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            //
            z_Calendar2 frm = new z_Calendar2(GlobalClass.branch, "AUTO SALES");
            frm.MdiParent = this;
            frm.Show();
        }

        private void calendarAutoServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            //
            z_Calendar2 frm = new z_Calendar2(GlobalClass.branch, "SERVICE STATUS");
            frm.MdiParent = this;
            frm.Show();
        }

        private void calendarOverTheCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            z_Calendar2 frm = new z_Calendar2(GlobalClass.branch, "OVER THE COUNTER");
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_13_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            SalesServiceLoan frm = new SalesServiceLoan();
            lbl_modname.Text = frm.Text;
            frm.MdiParent = this;
            btn_13.BackColor = panel2.BackColor;
            frm.Show();
        }

        private void btn_14_Click(object sender, EventArgs e)
        {
            closechild();
            btn_color_reset();
            z_Calendar2 frm = new z_Calendar2(GlobalClass.branch, "ALL");
            frm.MdiParent = this;
            lbl_modname.Text = frm.Text;
            btn_14.BackColor = panel2.BackColor;
            frm.Show();
        }

        private void gPComputationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            s_GP_Computation frm = new s_GP_Computation();
            frm.MdiParent = this;
            lbl_modname.Text = btn_7.Text;
            btn_color_reset();
            btn_7.BackColor = panel2.BackColor;
            frm.Show();
        }

        private void jobQuotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            s_Job_Quotation frm = new s_Job_Quotation();
            frm.MdiParent = this;
            lbl_modname.Text = btn_7.Text;
            btn_color_reset();
            btn_7.BackColor = panel2.BackColor;
            frm.Show();
        }

        private void workShoploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            s_Work_Shopload frm = new s_Work_Shopload();
            frm.MdiParent = this;
            lbl_modname.Text = btn_7.Text;
            btn_color_reset();
            btn_7.BackColor = panel2.BackColor;
            frm.Show();
        }

        private void callRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();

            btn_color_reset();

            call_history frm = new call_history();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void updateROStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void itemClassificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_itemclassification frm = new m_itemclassification();
            frm.ShowDialog();
        }

        private void arrivalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void roomAvailabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {

            closechild();
            lbl_modname.Text = "UNIT STATUS";
            Hotel_System.RoomStatus rs = new Hotel_System.RoomStatus();

            lbl_modname.Text = rs.Name;

            rs.MdiParent = this;

            rs.Show();
        }

        private void roomStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "UNIT AVAILABILITY";
            Hotel_System.RoomAvailability igh = new Hotel_System.RoomAvailability();

            lbl_modname.Text = igh.Name;

            igh.MdiParent = this;

            igh.Show();
        }

        private void reservationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "RESERVATION";
            Hotel_System.newReservation fm = new Hotel_System.newReservation();
            fm.MdiParent = this;
            fm.Show();
        }

        private void fmi_s000_Click(object sender, EventArgs e)
        {

        }

        private void fmi_pms4001_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "CHECK IN";
            Hotel_System.newArrivalWalkin fm = new Hotel_System.newArrivalWalkin();
            fm.MdiParent = this;
            fm.Show();
        }

        private void fmi_pms4002_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "IN-HOUSE GUEST";
            Hotel_System.newInhouse fm = new Hotel_System.newInhouse();
            fm.MdiParent = this;
            fm.Show();

        }

        private void fmi_pms005_Click(object sender, EventArgs e)
        {
            lbl_modname.Text = "TENNANT BILLING";
            Hotel_System.newGuestBilling gbill = new Hotel_System.newGuestBilling();
            gbill.MdiParent = this;
            gbill.Show();
        }

        private void fmi_pms6001_Click(object sender, EventArgs e)
        {
            lbl_modname.Text = "UTILITIES READING";
            Hotel_System.Hk_UtilitiesReading fc = new Hotel_System.Hk_UtilitiesReading();

            fc.MdiParent = this;

            fc.Show();

        }

        private void fmi_pms6002_Click(object sender, EventArgs e)
        {
            lbl_modname.Text = "UPDATE UNIT STATUS";
            Hotel_System.UpdateRoomStatus urs = new Hotel_System.UpdateRoomStatus();
            urs.MdiParent = this;
            urs.Show();
        }

        private void fmi_pms007_Click(object sender, EventArgs e)
        {
            closechild();
            lbl_modname.Text = "UNIT POSTING";
            Hotel_System.GenerateRoomCharge grc = new Hotel_System.GenerateRoomCharge();
            grc.MdiParent = this;
            grc.Show();
        }

        private void userConfigurationToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void propertySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.z_HotelSettings zh = new Hotel_System.z_HotelSettings();

            zh.ShowDialog();
        }

        private void systemDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.z_SystemDataUpdate frm = new Hotel_System.z_SystemDataUpdate();
            frm.ShowDialog();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void guestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.Guest g = new Hotel_System.Guest();

            //g.MdiParent = this;

            g.ShowDialog();
        }

        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RoomNumber rn = new Hotel_System.RoomNumber();

            //rn.MdiParent = this;

            rn.ShowDialog();
        }

        private void roomTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.m_RoomType rr = new Hotel_System.m_RoomType();

            //rr.MdiParent = this;

            rr.ShowDialog();
        }

        private void roomRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hotel_System.RoomRate dt = new Hotel_System.RoomRate();
            Hotel_System.RoomRates dt = new Hotel_System.RoomRates();
            //dt.MdiParent = this;

            dt.ShowDialog();
        }

        private void hotelChargesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_charge frm = new m_charge();
            frm.ShowDialog();
        }

        private void otherMasterListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.OtherMasterList oml = new Hotel_System.OtherMasterList();

            //oml.MdiParent = this;

            oml.ShowDialog();
        }

        private void houseKeepingItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.Housekeeping h = new Hotel_System.Housekeeping();

            //h.MdiParent = this;

            h.Show();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_company frm = new m_company();
            frm.ShowDialog();
        }

        private void stockTransferToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            closechild();
            Hotel_System.newReservation fm = new Hotel_System.newReservation();
            fm.MdiParent = this;
            fm.Show();
        }

        private void newArrivalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            Hotel_System.newArrivalWalkin fm = new Hotel_System.newArrivalWalkin();
            fm.MdiParent = this;
            fm.Show();
        }

        private void newInhouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closechild();
            Hotel_System.newInhouse fm = new Hotel_System.newInhouse();
            fm.MdiParent = this;
            fm.Show();
        }

        private void summaryOfInputTaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A106");
            rpt.Show();
        }

        private void summaryOfStatementOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A105");
            rpt.Show();
        }

        private void customersAgingReportDetailedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A403D");
            rpt.Show();
        }

        private void customersAgingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A403S");
            rpt.Show();

        }

        private void suppliersAgingReportDetailedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A406D");
            rpt.Show();
        }

        private void roomRateTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ratetypes frm = new Ratetypes();
            frm.ShowDialog();
        }

        private void comparativeIncomeStatementPerCostCenterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A503cc");
            rpt.Show();
        }
        private void comparativeMonthIncomeStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A503y");
            rpt.Show();
        }

        private void budgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_budget frm = new m_budget();
            frm.Show();
        }

        private void marToolStripMenuItem_Click(object sender, EventArgs e)
        {
            market_segment frm = new market_segment();
            frm.ShowDialog();
        }

        private void reservationReportByDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("101");
            rpt.Show();
        }

        private void blockedReservationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("102");
            rpt.Show();
        }

        private void cancelReservationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("103");
            rpt.Show();
        }

        private void noShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("104");
            rpt.Show();
        }

        private void expectedArrivalReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("201");
            rpt.Show();
        }

        private void actualArrivalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("202");
            rpt.Show();
        }

        private void actualDepartureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("203");
            rpt.Show();
        }

        private void actualDepartureReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("204");
            rpt.Show();
        }

        private void inhouseGuestReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("205");
            rpt.Show();
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("205B");
            rpt.Show();
        }

        private void roomTransferReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("206");
            rpt.Show();
        }

        private void creditCardReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.rpt_DSR dsr = new Hotel_System.rpt_DSR();

            dsr.MdiParent = this;

            dsr.Show();
        }

        private void dailyRemittanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("314");
            rpt.Show();
        }

        private void monthlySalesReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("306_ga");
            rpt.Show();
        }

        private void dailySalesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("305");
            rpt.Show();
        }

        private void dailySalesReportCheckOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("305B");
            rpt.Show();
        }

        private void summaryOfChargesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("307");
            rpt.Show();
        }

        private void monthlyChargesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("308");
            rpt.Show();
        }

        private void summaryOfMiscellaneousReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("309");
            rpt.Show();
        }

        private void chargesBalanceTransferedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("310");
            rpt.Show();
        }

        private void dSRSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("311");
            rpt.Show();
        }

        private void extraItemReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("312");
            rpt.Show();
        }

        private void reservationAdvancePaymentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("313");
            rpt.Show();

        }

        private void viewGuestHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.GuestHistory gh = new Hotel_System.GuestHistory();

            gh.MdiParent = this;
            gh.Show();
        }

        private void guestFolioByAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("207");
            rpt.Show();
        }

        private void guestBalanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.rpt_GuestHighBalance gh = new Hotel_System.rpt_GuestHighBalance();

            gh.MdiParent = this;
            gh.Show();
        }

        private void summaryOfGuestBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("403");
            rpt.Show();
        }

        private void summaryOfGuestBalanceCheckOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("404");
            rpt.Show();
        }

        private void sOAPeriodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.m_soaperiods frm = new Hotel_System.m_soaperiods();
            frm.Show();
        }

        private void comparativeIncomeStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A503m3");
            rpt.Show();
        }

        private void monthlySalesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("306");
            rpt.Show();
        }

        private void shiftDetailReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("315");
            rpt.Show();

        }

        private void miscelleneousAddOnsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotel_System.RPT_RES_entry rpt = new Hotel_System.RPT_RES_entry("309");
            rpt.Show();

        }

        private void marketSegmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_outlet frm = new m_outlet();
            frm.ShowDialog();
        }

        private void releaseChecksToolStripMenuItem_Click(object sender, EventArgs e)
        {

            closechild();
            btn_color_reset();

            a_releasecheck frm = new a_releasecheck();

            lbl_modname.Text = frm.Text;

            frm.MdiParent = this;

            frm.Show();
        }

        private void journalizeHotelTransactionsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            journalize frm = new journalize("H");
            try { frm.ShowDialog(); }
            catch { }
        }

        private void dailyCollectionReportToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A601");
            rpt.Show();
        }

        private void dailyFieldCollectionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A601");
            rpt.Show();
        }

        private void accountActivityMovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A107");
            rpt.Show();
        }

        private void journalizeCollectionEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closingPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            periodClosing frm = new periodClosing();
            frm.ShowDialog();
        }

        private void journalizeStatementOfAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            journalize frm = new journalize("A", "date");
            try { frm.ShowDialog(); }
            catch { }
        }

        private void journalizeSOABySOAPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            journalize frm = new journalize("A","period");
            try { frm.ShowDialog(); }
            catch { }
        }

        private void chargeClassificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_chargeClassification frm = new m_chargeClassification();
            frm.ShowDialog();
        }

        private void sOASummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A701");
            rpt.Show();
        }

        private void postedJournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z_clerkpassword clrk = new z_clerkpassword(true);
            clrk.ShowDialog();
        }

        public void verifiedClerk(Boolean isProceed)
        {
            if (isProceed == true)
            {
                ViewController.Sys.PostLedgerUndo plu = new ViewController.Sys.PostLedgerUndo();
                plu.ShowDialog();
            }
        }

        private void closingIncomeFinancialYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            incomeClosing ic = new incomeClosing();
            ic.ShowDialog();
        }
    }
}