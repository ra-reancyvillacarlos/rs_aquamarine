using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft;
using System.Drawing.Printing;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Hotel_System
{
    public partial class RPT_RES_entry : Form
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        String fol_num;
        String rpt_no = "";
        String _schema = "";
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        String fileloc_hotel = @"\\RIGHTAPPS\RightApps\Eastland\Reports\Hotel\";
        String resvation_folio = "";
        String room_spc = "";

        /*
         * RPT NO
         * 101 - reservation report by date   - 101_reservationrpt.rpt
         * 102 - blocked reservation          - 102_blockedreservation.rpt
         * 103 - cancelled reservation        - 103_cancelledreservation.rpt 
         */

        public RPT_RES_entry(String rnum)
        {
            InitializeComponent();
            rpt_no = rnum;
            thisDatabase db = new thisDatabase();

            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();

            String system_loc = db.get_system_loc();

            fileloc_hotel = system_loc + "\\Reports\\Hotel\\";
        }

        private void RPT_RES_entry_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            DateTime this_t_date = Convert.ToDateTime(db.get_systemdate(""));
            GlobalMethod gm = new GlobalMethod();

            resvation_folio = db.get_colval("m99", "resv_fol", "");
            room_spc = db.get_colval("m99", "rom_spc", "");

            _schema = db.get_schema();

            dtp_frm.Value = this_t_date;
            dtp_to.Value = this_t_date;

            try
            {
                pbar_panl_hide();

                if (rpt_no == "101")
                {
                    this.Text = "Reservation Report By Date";
                    grp_bydate.Text = "Reservation Dates";
                    grp_bydate.Show();
                    grp_options.Show();

                    lbl_cbo_1.Text = "Company";
                    lbl_cbo_2.Text = "Market Segment";

                    load_company(cbo_1);
                    load_marketsegment(cbo_2);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "102")
                {
                    this.Text = "Blocked Reservation Report";
                    grp_bydate.Show();

                    lbl_cbo_1.Text = "Room Number";
                    load_romnumber(cbo_1);
                    chk_1.Top = chk_1.Top - 50;
                    chk_1.Text = "Include Arrived Guest";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "103")
                {
                    this.Text = "Cancelled Reservation Report";
                    grp_bydate.Show();
                    grp_options.Hide();

                    grp_bydate.Text = "Cancellation Dates";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "104")
                {
                    this.Text = "No Show Report";
                    grp_bydate.Show();
                    grp_options.Hide();

                    grp_bydate.Text = "Expected Arrival Dates";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "201")
                {
                    this.Text = "Expected Arrival Report";
                    grp_bydate.Text = "Arrival Dates";
                    lbl_cbo_1.Hide();
                    lbl_cbo_2.Hide();
                    cbo_1.Hide();
                    cbo_2.Hide();
                    chk_1.Hide();

                    chk_2.Top = chk_2.Top - 100;
                    chk_3.Top = chk_3.Top - 100;
                    //chk_1.Text = "Print Registration Cards";                    
                    chk_2.Text = "Preview Registration Cards";
                    chk_3.Text = "Exclude Arrived Guest";
                    chk_3.Checked = true;

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "202")
                {
                    this.Text = "Actual Arrival Report";
                    grp_bydate.Text = "Arrival Dates";
                    lbl_cbo_1.Text = "Company";
                    lbl_cbo_2.Text = "Group";

                    load_company(cbo_1);
                    load_group(cbo_2);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "203")
                {
                    this.Text = "Expected Departure Report";
                    grp_bydate.Text = "Departure Dates";
                    lbl_cbo_1.Text = "Company";
                    lbl_cbo_2.Text = "Group";

                    load_company(cbo_1);
                    load_group(cbo_2);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "204")
                {
                    this.Text = "Actual Departure Report";
                    grp_bydate.Text = "Departure Dates";
                    lbl_cbo_1.Text = "Company";
                    //lbl_cbo_2.Text = "Group";

                    load_company(cbo_1);
                    //load_group(cbo_2);

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "205")
                {
                    this.Text = "In House Guest Report";
                    grp_bydate.Hide();

                    grp_options.Top = grp_options.Top - 100;
                    lbl_cbo_1.Text = "Company";
                    lbl_cbo_2.Text = "Group";
                    lbl_cbo_3.Text = "Rate Type";
                    load_company(cbo_1);
                    load_group(cbo_2);
                    load_ratetype(cbo_3);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();
                }
                else if (rpt_no == "205B")
                {
                    this.Text = "In House Guest By Nationality Report  ";
                    grp_bydate.Hide();

                    grp_options.Top = grp_options.Top - 100;
                    lbl_cbo_1.Text = "Company";
                    lbl_cbo_2.Text = "Group";
                    lbl_cbo_3.Text = "Rate Type";
                    load_company(cbo_1);
                    load_group(cbo_2);
                    load_ratetype(cbo_3);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();
                }
                else if (rpt_no == "206")
                {
                    this.Text = "Room Transfer Report";
                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Room";

                    load_romnumber(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "207")
                {
                    this.Text = "Guest Folio By Address";
                    grp_bydate.Text = "Transaction Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Hide();
                    cbo_1.Hide();
                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "301")
                {
                    this.Text = "Cashier's Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific Cashier / UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "302")
                {
                    this.Text = "Cash Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific Cashier / UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "303")
                {
                    this.Text = "Cash Credit Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific Cashier / UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "304")
                {
                    this.Text = "Credit Card Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific Cashier / UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "305")
                {
                    this.Text = "Daily Sales Report";

                    grp_bydate.Text = "Transaction Date";
                    lbl_cbo_1.Text = "Specific User ID";
                    this.load_userid(cbo_1);

                    lbl_dt_frm.Text = "Date";
                    dtp_frm.ResetText();

                    lbl_dt_to.Hide();
                    dtp_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "305B")
                {
                    this.Text = "Daily Sales Report";

                    grp_bydate.Text = "Transaction Date";
                    lbl_cbo_1.Text = "Specific User ID";
                    this.load_userid(cbo_1);

                    lbl_dt_frm.Text = "Date";

                    lbl_dt_to.Hide();
                    dtp_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "306")
                {
                    this.Text = "Monthly Sales Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific User ID";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "306_ga")
                {
                    this.Text = "Monthly Sales Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific User ID";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "307")
                {
                    this.Text = "Summary of Charges Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific User ID";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "307B")
                {
                    this.Text = "Summary of Charges Report(Other Charges)";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific User ID";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "308")
                {
                    this.Text = "Monthly Charges Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "309")
                {
                    this.Text = "Miscelleneous Detail Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "310")
                {
                    this.Text = "Charged Balances Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "311")
                {
                    this.Text = "DSR Summary Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "312")
                {
                    this.Text = "Extra Item Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "313")
                {
                    this.Text = "Reservation Advance Payment Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "314")
                {
                    this.Text = "Daily Remittance Report";

                    grp_bydate.Text = "Transaction Dates";
                    lbl_cbo_1.Text = "Specific UserId";
                    this.load_userid(cbo_1);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "315")
                {
                    this.Text = "Shift Detail Report";

                    grp_bydate.Text = "Transaction Dates";

                    lbl_cbo_1.Text = "Specific UserId";
                    this.load_userid(cbo_1);

                    lbl_cbo_2.Text = "Charge Description";
                    gm.load_charge_all(cbo_2);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "403")
                {
                    this.Text = "Summary of Guest Balances (In-House)";

                    grp_bydate.Text = "Arrival Dates";
                    //lbl_cbo_1.Text = "Specific UserId";
                    //this.load_userid(cbo_1);

                    cbo_1.Hide();
                    lbl_cbo_1.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "404")
                {
                    this.Text = "Summary of Guest Balances (Check Out)";

                    grp_bydate.Text = "Check Out Dates";
                    //lbl_cbo_1.Text = "Specific UserId";
                    //this.load_userid(cbo_1);

                    cbo_1.Hide();
                    lbl_cbo_1.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void load_userid(ComboBox cbo)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_alluserid();
            cbo.DataSource = dt;
            cbo.DisplayMember = "uid";
            cbo.ValueMember = "uid";
            cbo.SelectedIndex = -1;
        }

        private void load_paymentcharge(ComboBox cbo)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_chargedesc_bytype("P");
            cbo.DataSource = dt;
            cbo.DisplayMember = "chg_desc";
            cbo.ValueMember = "chg_code";
            cbo.SelectedIndex = -1;
        }

        private void load_company(ComboBox cbo)
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_name ASC;");

            cbo.DataSource = dt;
            cbo.DisplayMember = "comp_name";
            cbo.ValueMember = "comp_code";
            cbo.SelectedIndex = -1;
        }

        private void load_marketsegment(ComboBox cbo)
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("market", "mkt_code, mkt_desc", "", "ORDER BY mkt_code ASC;");

            cbo.DataSource = dt;
            cbo.DisplayMember = "mkt_desc";
            cbo.ValueMember = "mkt_code";
            cbo.SelectedIndex = -1;
        }

        private void load_romnumber(ComboBox cbo)
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("rooms", "rom_code, rom_desc", "", "ORDER BY rom_code ASC;");

            cbo.DataSource = dt;
            cbo.DisplayMember = "rom_desc";
            cbo.ValueMember = "rom_code";
            cbo.SelectedIndex = -1;
        }

        private void load_group(ComboBox cbo)
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("resgrp", "grp_code, \"group\"", "", "ORDER BY grp_code ASC;");

            cbo.DataSource = dt;
            cbo.DisplayMember = "group";
            cbo.ValueMember = "grp_code";
            cbo.SelectedIndex = -1;
        }

        private void load_ratetype(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("ratetype", "rate_code, rate_desc", "", "ORDER BY rate_code ASC;");

                cbo.DataSource = dt;
                cbo.DisplayMember = "rate_desc";
                cbo.ValueMember = "rate_code";

                cbo.SelectedIndex = -1;

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            input_enable(false);
            bgworker.RunWorkerAsync();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            cbo_1.SelectedIndex = -1;
            cbo_2.SelectedIndex = -1;
            cbo_3.SelectedIndex = -1;

            chk_1.Checked = false;
            chk_2.Checked = false;
            chk_3.Checked = false;
        }

        private int get_cbo_index(ComboBox cbo)
        {
            int i = -1;

            cbo.Invoke(new Action(() =>
            {
                i = cbo.SelectedIndex;
            }));

            return i;
        }

        private String get_cbo_value(ComboBox cbo)
        {
            String value = "";

            cbo.Invoke(new Action(() =>
            {
                value = cbo.SelectedValue.ToString();
            }));

            return value;
        }

        private String get_cbo_text(ComboBox cbo)
        {
            String txt = "";

            cbo.Invoke(new Action(() =>
            {
                txt = cbo.Text.ToString();
            }));

            return txt;
        }

        private Boolean ischkbox_checked(CheckBox chk)
        {
            Boolean ischk = false;

            chk.Invoke(new Action(() =>
            {
                ischk = chk.Checked;
            }));

            return ischk;
        }

        private void disp_reportviewer(ReportDocument myReportDocument)
        {
            crptviewer.Invoke(new Action(() =>
            {
                crptviewer.ReportSource = myReportDocument;
            }));

            crptviewer.Invoke(new Action(() =>
            {
                crptviewer.Refresh();
            }));
        }

        private void clr_param()
        {
            try
            {
                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
            }
            catch(Exception er)
            {
                
            }
        }

        private void input_enable(Boolean bol)
        {
            cbo_1.Invoke(new Action(() =>
            {
                cbo_1.Enabled = bol;
            }));

            cbo_2.Invoke(new Action(() =>
            {
                cbo_2.Enabled = bol;
            }));

            cbo_3.Invoke(new Action(() =>
            {
                cbo_3.Enabled = bol;
            }));

            dtp_frm.Invoke(new Action(() =>
            {
                dtp_frm.Enabled = bol;
            }));

            dtp_to.Invoke(new Action(() =>
            {
                dtp_to.Enabled = bol;
            }));

            btn_clear.Invoke(new Action(() =>
            {
                btn_clear.Enabled = bol;
            }));

            btn_submit.Invoke(new Action(() =>
            {
                btn_submit.Enabled = bol;
            }));
        }

        private void inc_pbar(int i)
        {
            try
            {
                progressBar1.Invoke(new Action(() =>
                {
                    if (progressBar1.Value + i <= progressBar1.Maximum)
                    {
                        progressBar1.Value += i;
                    }
                }));
            }
            catch (Exception er ) {
                MessageBox.Show(er.Message);
                reset_pbar(); }
        }

        private void reset_pbar()
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));
        }

        private void pbar_panl_hide()
        {
            pnl_pbar.Invoke(new Action(() =>
            {
                pnl_pbar.Hide();
            }));
        }

        private void pbar_panl_show()
        {
            pnl_pbar.Invoke(new Action(() =>
            {
                pnl_pbar.Show();
            }));
        }

        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            ReportDocument myReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            ParameterFieldDefinitions crParameterFieldDefinitions;
            String dtpicker_frm = dtp_frm.Value.ToString("yyyy/MM/dd");
            String dtpicker_to = dtp_to.Value.ToString("yyyy/MM/dd");

            pbar_panl_show();

            // try
            //{
            if (rpt_no == "101")
            {
                inc_pbar(5);
                DataTable dt_resfil = db.QueryBySQLCode("Select rf.*, m.mkt_desc AS market_name, rt.rate_desc AS rate_code_name, rmtp.typ_desc AS room_type_desc, c.comp_name AS company FROM " + _schema + ".resfil rf RIGHT JOIN " + _schema + ".market m ON rf.mkt_code=m.mkt_code RIGHT JOIN " + _schema + ".ratetype rt ON rt.rate_code=rf.rate_code RIGHT JOIN " + _schema + ".rtype rmtp ON rmtp.typ_code=rf.typ_code RIGHT JOIN " + _schema + ".guest g ON g.acct_no=rf.acct_no RIGHT JOIN " + _schema + ".company c ON c.comp_code=g.comp_code  WHERE rf.res_date >= '" + dtpicker_frm + "' AND rf.res_date <= '" + dtpicker_to + "' AND (rf.cancel IS NULL OR cancel='') ORDER BY rf.arr_date ASC");

                inc_pbar(50);

                myReportDocument.Load(fileloc_hotel + "101_reservationrpt.rpt");

                inc_pbar(5);

                myReportDocument.Database.Tables[0].SetDataSource(dt_resfil);

                inc_pbar(5);

                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //no of rooms
                crParameterDiscreteValue.Value = dt_resfil.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRooms"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "102")
            {
                String WHERE = ""; // cbo_1 = rooms chk_1 = include arrived guest

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    WHERE = WHERE + " AND rf.rom_code='" + get_cbo_value(cbo_1).ToString() + "'";
                }
                if (ischkbox_checked(chk_1))
                {
                    WHERE = WHERE + " AND rf.arrived='Y'";
                }

                DataTable dt_resfil = db.QueryBySQLCode("Select rf.*, m.mkt_desc AS market_name, rt.rate_desc AS rate_code_name, rmtp.typ_desc AS room_type_desc, c.comp_name AS company FROM " + _schema + ".resfil rf RIGHT JOIN " + _schema + ".market m ON rf.mkt_code=m.mkt_code RIGHT JOIN " + _schema + ".ratetype rt ON rt.rate_code=rf.rate_code RIGHT JOIN " + _schema + ".rtype rmtp ON rmtp.typ_code=rf.typ_code RIGHT JOIN " + _schema + ".guest g ON g.acct_no=rf.acct_no RIGHT JOIN " + _schema + ".company c ON c.comp_code=g.comp_code  WHERE rf.res_date >= '" + dtpicker_frm + "' AND rf.res_date <= '" + dtpicker_to + "' AND (rf.cancel IS NULL OR cancel='') AND rf.blockresv='Y'" + WHERE + " ORDER BY rf.arr_date ASC");

                inc_pbar(50);

                myReportDocument.Load(fileloc_hotel + "102_blockedreservation.rpt");

                inc_pbar(5);

                myReportDocument.Database.Tables[0].SetDataSource(dt_resfil);

                inc_pbar(5);

                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                 
            }
            else if (rpt_no == "103")
            {
                inc_pbar(5);

                DataTable dt_resfil = db.QueryBySQLCode("Select rf.*, m.mkt_desc AS market_name, rt.rate_desc AS rate_code_name, rmtp.typ_desc AS room_type_desc, c.comp_name AS company FROM " + _schema + ".resfil rf RIGHT JOIN " + _schema + ".market m ON rf.mkt_code=m.mkt_code RIGHT JOIN " + _schema + ".ratetype rt ON rt.rate_code=rf.rate_code RIGHT JOIN " + _schema + ".rtype rmtp ON rmtp.typ_code=rf.typ_code RIGHT JOIN " + _schema + ".guest g ON g.acct_no=rf.acct_no RIGHT JOIN " + _schema + ".company c ON c.comp_code=g.comp_code  WHERE rf.canc_date >= '" + dtpicker_frm + "' AND rf.canc_date <= '" + dtpicker_to + "' AND rf.cancel='Y' ORDER BY rf.arr_date ASC");

                inc_pbar(50);

                myReportDocument.Load(fileloc_hotel + "103_cancelledreservation.rpt");

                inc_pbar(5);

                myReportDocument.Database.Tables[0].SetDataSource(dt_resfil);

                inc_pbar(5);

                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //no of rooms
                crParameterDiscreteValue.Value = dt_resfil.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRooms"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                 
            }

            else if (rpt_no == "104")
            {
                inc_pbar(5);
                DataTable dt_resfil = db.QueryBySQLCode("Select rf.*, m.mkt_desc AS market_name, rt.rate_desc AS rate_code_name, rmtp.typ_desc AS room_type_desc, c.comp_name AS company FROM " + _schema + ".resfil rf RIGHT JOIN " + _schema + ".market m ON rf.mkt_code=m.mkt_code RIGHT JOIN " + _schema + ".ratetype rt ON rt.rate_code=rf.rate_code RIGHT JOIN " + _schema + ".rtype rmtp ON rmtp.typ_code=rf.typ_code RIGHT JOIN " + _schema + ".guest g ON g.acct_no=rf.acct_no RIGHT JOIN " + _schema + ".company c ON c.comp_code=g.comp_code  WHERE rf.arr_date >= '" + dtpicker_frm + "' AND rf.arr_date <= '" + dtpicker_to + "' AND rf.arr_date <= '" + db.get_systemdate("") + "' AND (rf.arrived IS NULL OR rf.arrived != 'Y')AND (rf.cancel IS NULL OR rf.cancel != 'Y')  ORDER BY rf.arr_date ASC");

                inc_pbar(50);

                myReportDocument.Load(fileloc_hotel + "104_noshowreport.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt_resfil);

                inc_pbar(5);
                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //no of rooms
                crParameterDiscreteValue.Value = dt_resfil.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRooms"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "201")
            {
                //Expected Arrival Report
                String WHERE = "";
                Boolean print_regc = false;
                Report rpt = new Report("", ""); // report for rc.

                inc_pbar(5);

                if (ischkbox_checked(chk_2))
                {
                    print_regc = true;
                }
                if (ischkbox_checked(chk_3))
                {
                    WHERE = WHERE + " AND (rf.arrived IS NULL OR rf.arrived != 'Y')";
                }

                DataTable dt_resfil = db.QueryBySQLCode("Select rf.*, m.mkt_desc AS market_name, rt.rate_desc AS rate_code_name, rmtp.typ_desc AS room_type_desc, c.comp_name AS company FROM " + _schema + ".resfil rf RIGHT JOIN " + _schema + ".market m ON rf.mkt_code=m.mkt_code RIGHT JOIN " + _schema + ".ratetype rt ON rt.rate_code=rf.rate_code RIGHT JOIN " + _schema + ".rtype rmtp ON rmtp.typ_code=rf.typ_code RIGHT JOIN " + _schema + ".guest g ON g.acct_no=rf.acct_no RIGHT JOIN " + _schema + ".company c ON c.comp_code=g.comp_code  WHERE rf.arr_date >= '" + dtpicker_frm + "' AND rf.arr_date <= '" + dtpicker_to + "' AND rf.arr_date >= '" + db.get_systemdate("") + "'" + WHERE + " AND (rf.cancel IS NULL OR rf.cancel != 'Y')  ORDER BY rf.rom_code ASC");
                inc_pbar(50);
                myReportDocument.Load(fileloc_hotel + "201B_expectedarrival.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt_resfil);
                inc_pbar(5);

                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //no of rooms
                crParameterDiscreteValue.Value = dt_resfil.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRooms"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 

                //rpt.printprev_regform();
                //rpt.Show();
            }
            else if (rpt_no == "202")
            {
                inc_pbar(5);
                //Actual Arrival Report
                DataTable dt_resfil = db.QueryBySQLCode("Select rf.*, m.mkt_desc AS market_name, rt.rate_desc AS rate_code_name, rmtp.typ_desc AS room_type_desc, c.comp_name AS company FROM " + _schema + ".resfil rf RIGHT JOIN " + _schema + ".market m ON rf.mkt_code=m.mkt_code RIGHT JOIN " + _schema + ".ratetype rt ON rt.rate_code=rf.rate_code RIGHT JOIN " + _schema + ".rtype rmtp ON rmtp.typ_code=rf.typ_code RIGHT JOIN " + _schema + ".guest g ON g.acct_no=rf.acct_no RIGHT JOIN " + _schema + ".company c ON c.comp_code=g.comp_code  WHERE rf.arr_date >= '" + dtpicker_frm + "' AND rf.arr_date <= '" + dtpicker_to + "' AND rf.arrived = 'Y' AND (rf.cancel IS NULL OR rf.cancel != 'Y')  ORDER BY rf.rom_code ASC");

                inc_pbar(30);
                myReportDocument.Load(fileloc_hotel + "202_actualarrival.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt_resfil);
                inc_pbar(5);
                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //no of rooms
                crParameterDiscreteValue.Value = dt_resfil.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRooms"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "203")
            {
                //company
                String comp = "All";
                String grp = "All";
                String WHERE = "";

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    comp = get_cbo_text(cbo_1);

                    WHERE = " AND g.comp_code='" + get_cbo_value(cbo_1).ToString() + "'";
                }
                if (get_cbo_index(cbo_2) > -1)
                {
                    grp = get_cbo_text(cbo_2);

                    WHERE = WHERE + " AND gf.rgrp_code='" + get_cbo_value(cbo_2).ToString() + "'";
                }

                DataTable dt_gfolio = db.QueryBySQLCode("SELECT gf.*, rt.rate_desc AS rate_code_name, c.comp_name AS company, m.mkt_desc AS market FROM " + _schema + ".gfolio gf LEFT JOIN " + _schema + ".guest g ON gf.acct_no=g.acct_no LEFT JOIN " + _schema + ".company c ON c.comp_code=g.comp_code LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON gf.rate_code=rt.rate_desc WHERE gf.typ_code != 'Z' AND gf.dep_date >= '" + dtpicker_frm + "' AND gf.dep_date <= '" + dtpicker_to + "'" + WHERE + " ORDER BY gf.rom_code ASC");
                inc_pbar(35);

                myReportDocument.Load(fileloc_hotel + "203_expecteddeparture.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt_gfolio);
                inc_pbar(5);

                //
                crParameterDiscreteValue.Value = comp;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["company"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //grp
                crParameterDiscreteValue.Value = grp;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["group"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //no of rooms
                crParameterDiscreteValue.Value = dt_gfolio.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRows"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 

            }
            else if (rpt_no == "204")
            {
                //company
                String comp = "All";
                String WHERE = "";

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    comp = get_cbo_text(cbo_1);

                    WHERE = " AND g.comp_code='" + get_cbo_value(cbo_1) + "'";
                }

                //MessageBox.Show("SELECT gf.reg_num, gf.acct_no, gf.full_name, gf.arr_date, gf.arr_time, gf.dep_date, gf.dep_time, gf.rom_code, gf.rom_rate, (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf INNER JOIN " + db.get_schema() + ".charge c ON c.chg_code=cf.chg_code WHERE gf.reg_num=cf.reg_num AND c.chg_type='C') AS totalcharge, (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf INNER JOIN " + db.get_schema() + ".charge c ON c.chg_code=cf.chg_code WHERE gf.reg_num=cf.reg_num AND c.chg_type='P') AS totalpayment FROM " + db.get_schema() + ".gfhist gf LEFT JOIN " + db.get_schema() + ".guest g ON g.acct_no=gf.acct_no LEFT JOIN " + db.get_schema() + ".company c ON c.comp_code=g.comp_code LEFT JOIN " + db.get_schema() + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + db.get_schema() + ".ratetype rt ON gf.rate_code=rt.rate_desc WHERE gf.typ_code != 'Z' AND gf.dep_date >= '" + dtpicker_frm + "' AND gf.dep_date <= '" + dtpicker_to + "' ORDER BY gf.rom_code ASC" + WHERE + " ORDER gf.rom_code ASC");
                DataTable dt_gfolio = db.QueryBySQLCode("SELECT gf.reg_num, gf.acct_no, gf.full_name, gf.arr_date, gf.arr_time, gf.dep_date, gf.dep_time, gf.rom_code, gf.rom_rate, (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf INNER JOIN " + db.get_schema() + ".charge c ON c.chg_code=cf.chg_code WHERE gf.reg_num=cf.reg_num AND c.chg_type='C') AS totalcharge, (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf INNER JOIN " + db.get_schema() + ".charge c ON c.chg_code=cf.chg_code WHERE gf.reg_num=cf.reg_num AND c.chg_type='P') AS totalpayment FROM " + db.get_schema() + ".gfhist gf LEFT JOIN " + db.get_schema() + ".guest g ON g.acct_no=gf.acct_no LEFT JOIN " + db.get_schema() + ".company c ON c.comp_code=g.comp_code LEFT JOIN " + db.get_schema() + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + db.get_schema() + ".ratetype rt ON gf.rate_code=rt.rate_desc WHERE gf.typ_code != 'Z' AND gf.dep_date >= '" + dtpicker_frm + "' AND gf.dep_date <= '" + dtpicker_to + "' " + WHERE + " ORDER BY gf.rom_code ASC ");

                inc_pbar(35);

                myReportDocument.Load(fileloc_hotel + "204_actualdeparture.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt_gfolio);
                inc_pbar(5);
                //
                crParameterDiscreteValue.Value = comp;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["company"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //no of rooms
                crParameterDiscreteValue.Value = dt_gfolio.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRows"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "205")
            {
                //company
                String comp = "All";
                String grp = "All";
                String rm_rate = "All";
                String WHERE = "";

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    comp = get_cbo_text(cbo_1);

                    WHERE = " AND g.comp_code='" + get_cbo_value(cbo_1) + "'";
                }
                if (get_cbo_index(cbo_2) > -1)
                {
                    grp = get_cbo_text(cbo_2);

                    WHERE = WHERE + " AND gf.rgrp_code='" + get_cbo_value(cbo_2) + "'";
                }
                if (get_cbo_index(cbo_3) > -1)
                {
                    rm_rate = get_cbo_text(cbo_3);

                    WHERE = WHERE + " AND g.rate_code='" + get_cbo_value(cbo_3) + "'";
                }

                //Actual Arrival Report
                DataTable dt_resfil = db.QueryBySQLCode("SELECT gf.*, rt.rate_desc AS rate_code_name, c.comp_name AS company, m.mkt_desc AS market from " + _schema + ".gfolio gf LEFT JOIN " + _schema + ".guest g ON gf.acct_no=g.acct_no LEFT JOIN " + _schema + ".company c ON c.comp_code=g.comp_code LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON gf.rate_code=rt.rate_code WHERE gf.typ_code != 'Z'" + WHERE + " ORDER BY gf.rom_code ASC");

                inc_pbar(35);

                myReportDocument.Load(fileloc_hotel + "205_inhouseguest.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt_resfil);

                inc_pbar(5);
                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                crParameterDiscreteValue.Value = comp;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["company"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtto
                crParameterDiscreteValue.Value = grp;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["group"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                crParameterDiscreteValue.Value = rm_rate;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["ratetype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                crParameterDiscreteValue.Value = dt_resfil.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRooms"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "205B")
            {
                //company
                String comp = "All";
                String grp = "All";
                String rm_rate = "All";
                String WHERE = "";

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    comp = get_cbo_text(cbo_1);

                    WHERE = " AND g.comp_code='" + get_cbo_value(cbo_1) + "'";
                }
                if (get_cbo_index(cbo_2) > -1)
                {
                    grp = get_cbo_text(cbo_2);

                    WHERE = WHERE + " AND gf.rgrp_code='" + get_cbo_value(cbo_2) + "'";
                }
                if (get_cbo_index(cbo_3) > -1)
                {
                    rm_rate = get_cbo_text(cbo_3);

                    WHERE = WHERE + " AND g.rate_code='" + get_cbo_value(cbo_3) + "'";
                }

                //Actual Arrival Report
                DataTable dt_resfil = db.QueryBySQLCode("SELECT gf.*, rt.rate_desc AS rate_code_name, c.comp_name AS company, m.mkt_desc AS market from " + _schema + ".gfolio gf LEFT JOIN " + _schema + ".guest g ON gf.acct_no=g.acct_no LEFT JOIN " + _schema + ".company c ON c.comp_code=g.comp_code LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON gf.rate_code=rt.rate_code WHERE gf.typ_code != 'Z'" + WHERE + " ORDER BY gf.rom_code ASC");

                inc_pbar(35);

                myReportDocument.Load(fileloc_hotel + "205B_inhouseguest.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt_resfil);

                inc_pbar(5);
                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                crParameterDiscreteValue.Value = comp;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["company"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtto
                crParameterDiscreteValue.Value = grp;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["group"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                crParameterDiscreteValue.Value = rm_rate;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["ratetype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                crParameterDiscreteValue.Value = dt_resfil.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRooms"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "206")
            {
                //company
                String rom_code = "All";
                String WHERE = "";

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    rom_code = get_cbo_text(cbo_1);

                    WHERE = " AND (rmt.fr_room='" + get_cbo_value(cbo_1) + "' OR rmt.to_room='" + get_cbo_value(cbo_1) + "')";
                }

                DataTable dt_gfolio = db.QueryBySQLCode("SELECT rmt.rtrf_num, rmt.reg_num, rmt.fr_room, rmt.to_room, rmt.reason ||' | '|| rmt.remarks AS reason, rmt.user_id, rmt.t_date, rmt.t_time, gf.full_name FROM " + db.get_schema() + ".rmtransfer rmt LEFT JOIN " + db.get_schema() + ".gfolio gf ON gf.reg_num=rmt.reg_num WHERE gf.typ_code != 'Z' AND rmt.t_date >= '" + dtpicker_frm + "' AND rmt.t_date <= '" + dtpicker_to + "'" + WHERE);
                inc_pbar(40);
                myReportDocument.Load(fileloc_hotel + "206_roomtransfer.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt_gfolio);

                inc_pbar(5);

                //room code
                crParameterDiscreteValue.Value = rom_code;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["rom_code"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //no of rooms
                crParameterDiscreteValue.Value = dt_gfolio.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofRows"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "207")
            {
                //company
                String WHERE = "";

                inc_pbar(5);

                DataTable dt_gfolio = db.QueryBySQLCode("SELECT gh.reg_num as reg_num, gh.acct_no as acct_no, g.full_name as full_name, to_char(gh.arr_date, 'MM/DD/yyyy') as arr_date, to_char(gh.dep_date, 'MM/DD/yyyy') as dep_date, gh.rom_code as rom_code, gh.typ_code as typ_code, g.address1 ||' / '|| g.address2 as company_name, 'CHECK OUT' as remarks"
                    + " FROM " + _schema + ".guest g, " + _schema + ".gfhist gh"
                    + " WHERE gh.typ_code != 'Z' AND gh.acct_no=g.acct_no AND gh.arr_date >= '" + dtp_frm.Value.ToString("yyyy/MM/dd") + "' AND gh.dep_date <= '" + dtp_to.Value.ToString("yyyy/MM/dd") + "'"
                    + " UNION"
                    + " SELECT gf.reg_num as reg_num, gf.acct_no as acct_no, g.full_name as full_name, to_char(gf.arr_date, 'MM/DD/yyyy') as arr_date, to_char(gf.dep_date, 'MM/DD/yyyy') as dep_date, gf.rom_code as rom_code, gf.typ_code as typ_code, g.address1 ||' / '|| g.address2 as company_name, 'CHECK IN' as remarks"
                    + " FROM " + _schema + ".guest g, " + _schema + ".gfolio gf"
                    + " WHERE gf.typ_code != 'Z' AND gf.acct_no=g.acct_no AND gf.arr_date >= '" + dtp_frm.Value.ToString("yyyy/MM/dd") + "' AND gf.dep_date <= '" + dtp_to.Value.ToString("yyyy/MM/dd") + "'"
                    + " ORDER BY" + " arr_date ASC");

                inc_pbar(40);
                myReportDocument.Load(fileloc_hotel + "207_checkinoutguestbyaddress.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt_gfolio);

                inc_pbar(5);

                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //no of rooms
                crParameterDiscreteValue.Value = dt_gfolio.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofFolio"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "301")
            {
                String clerk = "All";
                String WHERE = "";

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }
                DataTable dt = db.QueryBySQLCode("SELECT cf.chg_code, c.chg_desc, cf.reg_num, gf.full_name, cf.rom_code, cf.reference, (SELECT r.full_name FROM " + _schema + ".resfil r WHERE r.res_code=cf.res_code) as \"respaymentof\",cf.amount, cf.user_id, cf.chg_date, cf.t_time, 'Checked-In' AS status FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".gfolio gf ON gf.reg_num=cf.reg_num LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE c.chg_type='P' AND cf.t_date >= '" + dtpicker_frm + "' AND cf.t_date <= '" + dtpicker_to + "'" + WHERE + " ORDER BY cf.chg_code ASC, cf.chg_date ASC, cf.t_time ASC");

                inc_pbar(15);

                DataTable dt2 = db.QueryBySQLCode("SELECT cf.chg_code, c.chg_desc, cf.reg_num, gf.full_name, cf.rom_code, cf.reference, (SELECT r.full_name FROM " + _schema + ".resfil r WHERE r.res_code=cf.res_code) as \"respaymentof\",cf.amount, cf.user_id, cf.chg_date, cf.t_time, 'Checked-Out' AS status FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".gfhist gf ON gf.reg_num=cf.reg_num LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE c.chg_type='P' AND cf.t_date >= '" + dtpicker_frm + "' AND cf.t_date <= '" + dtpicker_to + "'" + WHERE + " ORDER BY cf.chg_code ASC, cf.chg_date ASC, cf.t_time ASC");

                inc_pbar(15);

                myReportDocument.Load(fileloc_hotel + "301_cashierreport.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt);
                inc_pbar(5);

                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //cashier / clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "302")
            {
                String clerk = "All";
                String WHERE = "";

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }
                //DataTable dt = db.QueryBySQLCode("SELECT cf.*, cf.chg_code, c.chg_desc, cf.reg_num, gf.full_name, cf.rom_code, cf.reference, (SELECT r.full_name FROM " + _schema + ".resfil r WHERE r.res_code=cf.res_code) as \"respaymentof\",cf.amount, cf.user_id, cf.chg_date, cf.t_time, 'Checked-In' AS status FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".gfolio gf ON gf.reg_num=cf.reg_num LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE c.chg_type='P' AND cf.t_date >= '" + dtpicker_frm + "' AND cf.t_date <= '" + dtpicker_to + "'" + WHERE + " ORDER BY cf.chg_code ASC, cf.chg_date ASC, cf.t_time ASC");
                //DataTable dt = db.QueryBySQLCode("SELECT cf.* FROM " + _schema + ".chghist cf WHERE cf.chg_code='101 AND cf.t_date >= '" + dtpicker_frm + "' AND cf.t_date <= '" + dtpicker_to + "'" + WHERE);
                inc_pbar(15);

                //db.transtotemp_chghist(GlobalClass.username, rpt_no, false, "SELECT cf.* FROM " + _schema + ".chghist cf WHERE cf.chg_code='101 AND cf.chg_date >= '" + dtpicker_frm + "' AND cf.chg_date <= '" + dtpicker_to + "'" + WHERE);

                inc_pbar(20);

                DataTable dt = db.QueryBySQLCode("Select cf.*, gf.full_name FROM " + _schema + ".chghist cf JOIN rssys.charge c ON c.chg_code=cf.chg_code  LEFT JOIN " + _schema + ".gfolio gf ON gf.reg_num=cf.reg_num WHERE c.chg_class='CASH' AND cf.chg_date>='" + dtpicker_frm + "' AND cf.chg_date<='" + dtpicker_to + "'" + WHERE);

                inc_pbar(5);

                myReportDocument.Load(fileloc_hotel + "302_cashreport.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt);
                inc_pbar(5);
                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtfrm
                crParameterDiscreteValue.Value = dtpicker_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //cashier / clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "303")
            {
                String clerk = "All";
                String WHERE = "";

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }
                inc_pbar(5);
                db.clear_temp_chghist(rpt_no);
                inc_pbar(10);
                db.transtotemp_chghist(GlobalClass.username, rpt_no, false, "SELECT cf.chg_code, c.chg_desc, cf.reg_num, gf.full_name, cf.rom_code, cf.reference, (SELECT r.full_name FROM " + _schema + ".resfil r WHERE r.res_code=cf.res_code) as \"respaymentof\",cf.amount, cf.user_id, cf.chg_date, cf.t_time, cf.doc_type, 'Checked-In' AS status FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".gfolio gf ON gf.reg_num=cf.reg_num LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE c.chg_type='P' AND cf.t_date >= '" + dtpicker_frm + "' AND cf.t_date <= '" + dtpicker_to + "'" + WHERE);
                inc_pbar(10);
                DataTable dt = db.QueryBySQLCode("SELECT cf.chg_code, c.chg_desc, cf.reg_num, gf.full_name, cf.rom_code, cf.reference, (SELECT r.full_name FROM " + _schema + ".resfil r WHERE r.res_code=cf.res_code) as \"respaymentof\",cf.amount, cf.user_id, cf.chg_date, cf.t_time, cf.doc_type, 'Checked-In' AS status FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".gfolio gf ON gf.reg_num=cf.reg_num LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE c.chg_type='P' AND cf.t_date >= '" + dtpicker_frm + "' AND cf.t_date <= '" + dtpicker_to + "'" + WHERE + " ORDER BY cf.chg_code ASC, cf.chg_date ASC, cf.t_time ASC");

                inc_pbar(5);
                myReportDocument.Load(fileloc_hotel + "303_cashcreditreport.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt);
                inc_pbar(5);

                //user
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtfrm
                crParameterDiscreteValue.Value = dtp_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //dtto
                crParameterDiscreteValue.Value = dtpicker_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //cashier / clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "304")
            {

            }
            else if (rpt_no == "305")
            {
                String clerk = "All";
                String WHERE = "", WHERE2 = "";
                String trnx_dt = DateTime.Parse(dtpicker_frm).ToString("yyyy-MM-dd");

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND gf.co_user='" + get_cbo_value(cbo_1) + "'";
                    WHERE2 = WHERE2 + " AND gf.user_id='" + get_cbo_value(cbo_1) + "'";
                }

                DataTable dt = db.QueryBySQLCode("(SELECT DISTINCT gf.reg_num, gf.full_name, gf.rom_code, gf.arr_date, gf.co_date AS dep_date, cf.reference as cc_no, SUM((CASE WHEN cf.chg_class='CASHC' THEN cf.amount ELSE 0.00 END) * -1)  as cashcredit,SUM((CASE WHEN cf.chg_class='CASH' THEN cf.amount ELSE 0.00 END) * -1)  as cash,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1)  as creditcard,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1) as check,SUM((CASE WHEN cf.chg_class='POTHR' THEN cf.amount ELSE 0.00 END) * -1) as charge,SUM(cf.amount * -1) AS total_charge, CASE WHEN gf.co_date::date=cf.chg_date  THEN 'CHECK OUT' ELSE 'CHECK IN' END AS status FROM rssys.gfhist gf LEFT JOIN (SELECT cf.*, CASE WHEN doc_type='CC' THEN 'CASHC' WHEN COALESCE(c.chg_class,'')='' THEN 'CASH' ELSE c.chg_class END chg_class FROM rssys.chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND cf.amount<0) cf ON gf.reg_num=cf.reg_num WHERE COALESCE(cf.reference,'')<>'' AND gf.co_date::date='" + trnx_dt + "' " + WHERE + " GROUP BY gf.reg_num, gf.full_name, gf.rom_code, gf.arr_date, gf.co_date, cf.reference, cf.chg_date UNION ALL SELECT DISTINCT gf.reg_num, gf.full_name, gf.rom_code, gf.arr_date, gf.dep_date, cf.reference as cc_no, SUM((CASE WHEN cf.chg_class='CASHC' THEN cf.amount ELSE 0.00 END) * -1)  as cashcredit,SUM((CASE WHEN cf.chg_class='CASH' THEN cf.amount ELSE 0.00 END) * -1)  as cash,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1)  as creditcard,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1) as check,SUM((CASE WHEN cf.chg_class='POTHR' THEN cf.amount ELSE 0.00 END) * -1) as charge, SUM(cf.amount * -1) AS total_charge, 'CHECK IN' AS status FROM rssys.gfolio gf LEFT JOIN (SELECT cf.*, CASE WHEN doc_type='CC' THEN 'CASHC' WHEN COALESCE(c.chg_class,'')='' THEN 'CASH' ELSE c.chg_class END chg_class FROM rssys.chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND cf.amount<0) cf ON gf.reg_num=cf.reg_num WHERE COALESCE(cf.reference,'')<>'' AND gf.dep_date::date='" + trnx_dt + "' " + WHERE2 + " GROUP BY gf.reg_num, gf.full_name, gf.rom_code, gf.arr_date, gf.dep_date, cf.reference) ORDER BY rom_code, reg_num");

                inc_pbar(5);
                myReportDocument.Load(fileloc_hotel + "305_dailysalesreport.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt);
                inc_pbar(5);

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //trnx date
                crParameterDiscreteValue.Value = trnx_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_trnx"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                //user_name
                crParameterDiscreteValue.Value = GlobalClass.user_fullname;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["preparedby"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "305B")
            {
                String clerk = "All";
                String WHERE = "";
                String trnx_dt = DateTime.Parse(dtpicker_frm).ToString("yyyy-MM-dd");

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND gf.co_user='" + get_cbo_value(cbo_1) + "'";
                }

                DataTable dt = db.QueryBySQLCode("SELECT gf.reg_num, gf.full_name, gf.rom_code, gf.arr_date, gf.co_date AS dep_date, SUM((CASE WHEN cf.chg_class='CASHC' THEN cf.amount ELSE 0.00 END) * -1)  as cashcredit,SUM((CASE WHEN cf.chg_class='CASH' THEN cf.amount ELSE 0.00 END) * -1)  as cash,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1)  as creditcard,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1) as check,SUM((CASE WHEN cf.chg_class='POTHR' THEN cf.amount ELSE 0.00 END) * -1) as total_charge FROM rssys.gfhist gf LEFT JOIN (SELECT cf.*, CASE WHEN doc_type='CC' THEN 'CASHC' WHEN COALESCE(c.chg_class,'')='' THEN 'CASH' ELSE c.chg_class END AS chg_class FROM rssys.chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND cf.amount<0) cf ON gf.reg_num=cf.reg_num WHERE COALESCE(cf.reference,'')<>'' AND gf.co_date::date='" + trnx_dt + "' " + WHERE + " GROUP BY gf.reg_num, gf.full_name, gf.rom_code, gf.arr_date, gf.co_date ORDER BY gf.reg_num");
                inc_pbar(5);
                myReportDocument.Load(fileloc_hotel + "305_dailysalesreport_chkout.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt);
                inc_pbar(5);

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //trnx date
                crParameterDiscreteValue.Value = trnx_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_trnx"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }
            else if (rpt_no == "306")
            {
                String clerk = "All";
                String WHERE = "", WHERE2 = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND gf.co_user='" + get_cbo_value(cbo_1) + "'";
                    WHERE2 = WHERE2 + " AND gf.user_id='" + get_cbo_value(cbo_1) + "'";
                }

                inc_pbar(15);
                String was = "SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, gf.arr_time, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.dep_time, gf.rom_code ||'-'|| gf.rmrttyp as rom_code, gf.rom_rate as rom_rate, rt.rate_desc  AS rate_code,  m.mkt_desc AS market_name, 'CHECKED IN' AS status, SUM((CASE WHEN cf.chg_class='CASHC' THEN cf.amount ELSE 0.00 END) * -1)  as cashcredit, SUM((CASE WHEN cf.chg_class='CASH' THEN cf.amount ELSE 0.00 END) * -1)  as cash, SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1)  as creditcard, SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1) as check, SUM((CASE WHEN cf.chg_class='POTHR' THEN cf.amount ELSE 0.00 END) * -1) as charge FROM rssys.gfolio gf LEFT JOIN (SELECT cf.*, CASE WHEN doc_type='CC' THEN 'CASHC' WHEN COALESCE(c.chg_class,'')='' THEN 'CASH' ELSE c.chg_class END AS chg_class FROM rssys.chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND cf.amount<0) cf ON gf.reg_num=cf.reg_num LEFT JOIN rssys.market m ON m.mkt_code=gf.mkt_code LEFT JOIN rssys.ratetype rt ON rt.rate_code=gf.rate_code WHERE COALESCE(cf.amount,0)<>0 AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') ) " + WHERE2 + "  GROUP BY gf.reg_num, gf.acct_no, gf.full_name, gf.arr_date, arr_date, gf.arr_time, gf.dep_date, gf.dep_time, gf.rom_code, gf.rmrttyp, gf.rom_rate, rt.rate_desc, m.mkt_desc UNION ALL  SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, gf.arr_time, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.dep_time, gf.rom_code as rom_code, gf.rom_rate + gf.govt_tax as rom_rate, rt.rate_desc  AS rate_code, m.mkt_desc AS market_name, 'CHECKED OUT' AS status,  SUM((CASE WHEN cf.chg_class='CASHC' THEN cf.amount ELSE 0.00 END) * -1)  as cashcredit,SUM((CASE WHEN cf.chg_class='CASH' THEN cf.amount ELSE 0.00 END) * -1)  as cash,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1)  as creditcard,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1) as check,SUM((CASE WHEN cf.chg_class='POTHR' THEN cf.amount ELSE 0.00 END) * -1) as charge FROM rssys.gfhist gf LEFT JOIN (SELECT cf.*, CASE WHEN doc_type='CC' THEN 'CASHC' WHEN COALESCE(c.chg_class,'')='' THEN 'CASH' ELSE c.chg_class END AS chg_class FROM rssys.chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND cf.amount<0) cf ON gf.reg_num=cf.reg_num LEFT JOIN rssys.market m ON m.mkt_code=gf.mkt_code LEFT JOIN rssys.ratetype rt ON rt.rate_code=gf.rate_code  WHERE COALESCE(cf.amount,0)<>0	 AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') ) " + WHERE + " GROUP BY gf.reg_num, gf.acct_no, gf.full_name, gf.arr_date, arr_date, gf.arr_time, gf.dep_date, gf.dep_time, gf.rom_code, gf.rmrttyp, gf.rom_rate, gf.govt_tax, rt.rate_desc, m.mkt_desc ORDER BY arr_date, reg_num";
                DataTable dt = db.QueryBySQLCode("SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, gf.arr_time, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.dep_time, gf.rom_code ||'-'|| gf.rmrttyp as rom_code, gf.rom_rate as rom_rate, rt.rate_desc  AS rate_code,  m.mkt_desc AS market_name, 'CHECKED IN' AS status, SUM((CASE WHEN cf.chg_class='CASHC' THEN cf.amount ELSE 0.00 END) * -1)  as cashcredit, SUM((CASE WHEN cf.chg_class='CASH' THEN cf.amount ELSE 0.00 END) * -1)  as cash, SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1)  as creditcard, SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1) as check, SUM((CASE WHEN cf.chg_class='POTHR' THEN cf.amount ELSE 0.00 END) * -1) as charge FROM rssys.gfolio gf LEFT JOIN (SELECT cf.*, CASE WHEN doc_type='CC' THEN 'CASHC' WHEN COALESCE(c.chg_class,'')='' THEN 'CASH' ELSE c.chg_class END AS chg_class FROM rssys.chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND cf.amount<0) cf ON gf.reg_num=cf.reg_num LEFT JOIN rssys.market m ON m.mkt_code=gf.mkt_code LEFT JOIN rssys.ratetype rt ON rt.rate_code=gf.rate_code WHERE COALESCE(cf.amount,0)<>0 AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') ) " + WHERE2 + "  GROUP BY gf.reg_num, gf.acct_no, gf.full_name, gf.arr_date, arr_date, gf.arr_time, gf.dep_date, gf.dep_time, gf.rom_code, gf.rmrttyp, gf.rom_rate, rt.rate_desc, m.mkt_desc UNION ALL  SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, gf.arr_time, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.dep_time, gf.rom_code as rom_code, gf.rom_rate + gf.govt_tax as rom_rate, rt.rate_desc  AS rate_code, m.mkt_desc AS market_name, 'CHECKED OUT' AS status,  SUM((CASE WHEN cf.chg_class='CASHC' THEN cf.amount ELSE 0.00 END) * -1)  as cashcredit,SUM((CASE WHEN cf.chg_class='CASH' THEN cf.amount ELSE 0.00 END) * -1)  as cash,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1)  as creditcard,SUM((CASE WHEN cf.chg_class='CCARD' THEN cf.amount ELSE 0.00 END) * -1) as check,SUM((CASE WHEN cf.chg_class='POTHR' THEN cf.amount ELSE 0.00 END) * -1) as charge FROM rssys.gfhist gf LEFT JOIN (SELECT cf.*, CASE WHEN doc_type='CC' THEN 'CASHC' WHEN COALESCE(c.chg_class,'')='' THEN 'CASH' ELSE c.chg_class END AS chg_class FROM rssys.chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND cf.amount<0) cf ON gf.reg_num=cf.reg_num LEFT JOIN rssys.market m ON m.mkt_code=gf.mkt_code LEFT JOIN rssys.ratetype rt ON rt.rate_code=gf.rate_code  WHERE COALESCE(cf.amount,0)<>0	 AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') ) " + WHERE + " GROUP BY gf.reg_num, gf.acct_no, gf.full_name, gf.arr_date, arr_date, gf.arr_time, gf.dep_date, gf.dep_time, gf.rom_code, gf.rmrttyp, gf.rom_rate, gf.govt_tax, rt.rate_desc, m.mkt_desc ORDER BY arr_date, reg_num");

                inc_pbar(25);
                myReportDocument.Load(fileloc_hotel + "306_monthlysalesreport_2.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt);
                inc_pbar(5);
                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                 
            }
            else if (rpt_no == "306_ga")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;
                String cnt = "0";

                inc_pbar(5);

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }

                inc_pbar(15);

                DataTable dt = db.QueryBySQLCode("SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, gf.arr_time, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.dep_time, gf.rom_code ||'-'|| gf.rmrttyp as rom_code, rom_rate as rom_rate,rt. rate_desc  AS rate_code,  m.mkt_desc AS market_name, 'CHECKED IN' AS status,  COALESCE((SELECT (SUM(cf.amount)*-1) FROM " + _schema + ".chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class='CASH'  WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "'),0.00) AS cash,  COALESCE((SELECT (SUM(cf.amount)*-1)  FROM " + _schema + ".chgfil cf  JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class='CCARD' WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "'),0.00) AS creditcard,  COALESCE((SELECT SUM(CASE WHEN cf.chg_code IN (SELECT chg_code FROM " + _schema + ".charge WHERE chg_type='P') THEN cf.amount ELSE '0.00' END)*-1  FROM " + _schema + ".chgfil cf  JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class not in ('CASH','CCARD','CHECK')  WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "'),0.00) AS other  FROM " + _schema + ".gfolio gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code  WHERE gf.rom_code!='" + room_spc + "' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' )   OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') )  UNION ALL  SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, gf.arr_time, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.dep_time, gf.rom_code as rom_code, gf.rom_rate + gf.govt_tax as rom_rate, rt.rate_desc  AS rate_code, m.mkt_desc AS market_name, 'CHECKED OUT' AS status,  COALESCE((SELECT (SUM(cf.amount)*-1) FROM " + _schema + ".chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class='CASH'  WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "'),0.00) AS cash,  COALESCE((SELECT (SUM(cf.amount)*-1)  FROM " + _schema + ".chgfil cf  JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class='CCARD' WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "'),0.00) AS creditcard,  COALESCE((SELECT SUM(CASE WHEN cf.chg_code IN (SELECT chg_code FROM " + _schema + ".charge WHERE chg_type='P') THEN cf.amount ELSE '0.00' END)*-1  FROM " + _schema + ".chgfil cf  JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class not in ('CASH','CCARD','CHECK')  WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "'),0.00) AS other  FROM " + _schema + ".gfhist gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code  WHERE gf.rom_code!='" + room_spc + "' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' )  OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') )  ORDER BY arr_date ");

                /*
                 
                 * 
                 * 
                 "SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, gf.arr_time, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.dep_time, gf.rom_code ||'-'|| gf.rmrttyp as rom_code, rom_rate as rom_rate,rt. rate_desc  AS rate_code,  m.mkt_desc AS market_name, 'CHECKED IN' AS status, "
                                                 + " (SELECT (SUM(CASE WHEN cf.chg_code='101' THEN cf.amount else '0.00' END)*-1) - SUM(CASE WHEN cf.chg_code='007' THEN cf.amount else '0.00' END) FROM " + _schema + ".chgfil cf WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS cash, "
                                                 + " (SELECT (SUM(CASE WHEN cf.chg_code='102' OR cf.chg_code='103' OR cf.chg_code='104' OR cf.chg_code='105' THEN cf.amount else '0.00' END)*-1)  FROM " + _schema + ".chgfil cf WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS creditcard, "
                                                 + " (SELECT SUM(CASE WHEN cf.chg_code IN (SELECT chg_code FROM " + _schema + ".charge WHERE chg_type='P' AND chg_code!='101' AND chg_code!='102' AND chg_code!='103' AND chg_code!='104' AND chg_code!='105' AND chg_code!='108' AND chg_code!='109' AND chg_code!='112') THEN cf.amount ELSE '0.00' END)*-1  FROM " + _schema + ".chgfil cf WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS other "
                                                 + " FROM " + _schema + ".gfolio gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code "
                                                 + " WHERE gf.rom_code!='Z01' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' )  "
                                                 + " OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') ) "
                                                 + " UNION ALL "
                                                 + " SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, gf.arr_time, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.dep_time, gf.rom_code as rom_code, gf.rom_rate + gf.govt_tax as rom_rate, rt.rate_desc  AS rate_code, m.mkt_desc AS market_name, 'CHECKED OUT' AS status, "
                                                 + " (SELECT (SUM(CASE WHEN cf.chg_code='101' THEN cf.amount else '0.00' END)*-1) - SUM(CASE WHEN cf.chg_code='007' THEN cf.amount else '0.00' END) FROM " + _schema + ".chghist cf WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS cash, "
                                                 + " (SELECT (SUM(CASE WHEN cf.chg_code='102' OR cf.chg_code='103' OR cf.chg_code='104' OR cf.chg_code='105' THEN cf.amount else '0.00' END)*-1)  FROM " + _schema + ".chghist cf WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS creditcard, "
                                                 + " (SELECT SUM(CASE WHEN cf.chg_code IN (SELECT chg_code FROM " + _schema + ".charge WHERE chg_type='P' AND chg_code!='101' AND chg_code!='102' AND chg_code!='103' AND chg_code!='104' AND chg_code!='105' AND chg_code!='108' AND chg_code!='109' AND chg_code!='112') THEN cf.amount ELSE '0.00' END)*-1  FROM " + _schema + ".chghist cf WHERE cf.reg_num=gf.reg_num AND cf.chg_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS other "
                                                 + " FROM " + _schema + ".gfhist gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code "
                                                 + " WHERE gf.rom_code!='Z01' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' ) "
                                                 + " OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') ) "
                                                 + " ORDER BY arr_date "
                 */



                inc_pbar(25);
                myReportDocument.Load(fileloc_hotel + "306_monthlysalesreport2.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt);
                inc_pbar(5);

                if (dt.Rows.Count > 0)
                {
                    cnt = dt.Rows.Count.ToString();
                }

                //clerk
                crParameterDiscreteValue.Value = cnt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noOfRows"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);

                 
            }
            else if (rpt_no == "307")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;
                String prev_mo = Convert.ToDateTime(dtpicker_frm).AddMonths(-1).ToString("MM");

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }

                DataTable dt = db.QueryBySQLCode("SELECT DISTINCT gf.arr_date AS arr_dt, gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.rom_code ||'-'|| gf.rmrttyp as rom_code,rt.rate_desc  AS rate_code,  m.mkt_desc AS market_name, 'CHECKED IN' AS status,  rom_rate + govt_tax as rom_rate, gf.rmrttyp, SUM(CASE WHEN c.chg_class in ('URENT') THEN cf.amount ELSE 0 END) AS total_charges, SUM(CASE WHEN c.chg_class in ('WATER','ELEC') THEN cf.amount ELSE 0 END) AS addons, SUM(CASE WHEN c.chg_class not in ('URENT','WATER','ELEC') THEN cf.amount ELSE 0 END) AS balance FROM rssys.gfolio gf LEFT JOIN rssys.market m ON m.mkt_code=gf.mkt_code LEFT JOIN rssys.ratetype rt ON rt.rate_code=gf.rate_code LEFT JOIN rssys.chgfil cf ON cf.reg_num=gf.reg_num JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_type='C' WHERE gf.rom_code!='" + room_spc + "' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' ) OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "'))  AND cf.chg_date BETWEEN  '" + dt_frm + "' AND '" + dt_to + "' GROUP BY gf.reg_num, gf.acct_no, gf.full_name, gf.arr_date, gf.dep_date, gf.rom_code, gf.rmrttyp ,rt.rate_desc,  m.mkt_desc,  rom_rate, gf.rmrttyp UNION ALL SELECT DISTINCT gf.arr_date AS arr_dt, gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.rom_code ||'-'|| gf.rmrttyp as rom_code,rt.rate_desc  AS rate_code,  m.mkt_desc AS market_name, 'CHECKED OUT' AS status,  rom_rate + govt_tax as rom_rate, gf.rmrttyp, SUM(CASE WHEN c.chg_class in ('URENT') THEN cf.amount ELSE 0 END) AS total_charges, SUM(CASE WHEN c.chg_class in ('WATER','ELEC') THEN cf.amount ELSE 0 END) AS addons, SUM(CASE WHEN c.chg_class not in ('URENT','WATER','ELEC') THEN cf.amount ELSE 0 END) AS balance FROM rssys.gfhist gf  LEFT JOIN rssys.market m ON m.mkt_code=gf.mkt_code LEFT JOIN rssys.ratetype rt ON rt.rate_code=gf.rate_code LEFT JOIN rssys.chghist cf ON cf.reg_num=gf.reg_num JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_type='C' WHERE gf.rom_code!='" + room_spc + "' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' ) OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "')) AND cf.chg_date BETWEEN  '" + dt_frm + "' AND '" + dt_to + "'  GROUP BY gf.reg_num, gf.acct_no, gf.full_name, gf.arr_date, gf.dep_date, gf.rom_code, gf.rmrttyp ,rt.rate_desc,  m.mkt_desc, rom_rate, gf.rmrttyp ORDER BY arr_dt");

                /* 
                 * 
                 * SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.rom_code ||'-'|| gf.rmrttyp as rom_code,rt.rate_desc  AS rate_code,  m.mkt_desc AS market_name, 'CHECKED IN' AS status,  rom_rate as rom_rate, gf.rmrttyp, "
                + " CASE WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND gf.dep_date > '" + dt_to + " 00:00:00'::timestamp THEN gf.rom_rate  "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30  THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/30) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30 THEN DATE_PART('day', gf.dep_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/30) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30 THEN DATE_PART('day', gf.dep_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/30) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31  THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/(DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31 THEN DATE_PART('day', gf.dep_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/(DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31 THEN DATE_PART('day', gf.dep_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/(DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                + " WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND gf.dep_date > '" + dt_to + " 00:00:00'::timestamp THEN (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class in ('URENT') WHERE cf.reg_num=gf.reg_num) "
                + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date > '" + dt_to + " 00:00:00'::timestamp THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/7) "
                    + " WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp THEN DATE_PART('day', gf.dep_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/7) "
                + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp THEN DATE_PART('day', gf.dep_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/7) "
                + " WHEN gf.rmrttyp='D' THEN (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class in ('URENT') WHERE cf.reg_num=gf.reg_num) "
                + " END AS total_charges, " //room charge applied
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num=gf.reg_num AND c.chg_type='C' AND (c.isaddons='FALSE' OR c.isaddons IS NULL) AND c.chg_class not in ('URENT')  AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS balance, " //othercharges
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num=gf.reg_num AND c.isaddons='TRUE' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS addons "
                + " FROM " + _schema + ".gfolio gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code "
                + " WHERE gf.rom_code!='" + room_spc + "' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' ) OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "')) "
                + " UNION ALL "
                + " SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, to_char(gf.co_date, 'MM/dd/yyyy') AS dep_date, gf.rom_code ||'-'|| gf.rmrttyp as rom_code, rt.rate_desc  AS rate_code, m.mkt_desc AS market_name, 'CHECKED OUT' AS status, gf.rom_rate as rom_rate, gf.rmrttyp, "
                + " CASE WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND gf.co_date > '" + dt_to + " 00:00:00'::timestamp THEN (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class in ('URENT') WHERE cf.reg_num=gf.reg_num) "
                + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) > 7 THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/7) "
                + " WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) > 7 THEN DATE_PART('day', gf.co_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/7) "
                + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) > 7 THEN DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/7) "
                + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) < 8 THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/( DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp))) "
                + " WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) < 8 THEN DATE_PART('day', gf.co_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/( DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp))) "
                + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) < 8 THEN DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/( DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp))) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND gf.co_date > '" + dt_to + " 00:00:00'::timestamp THEN gf.rom_rate  "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30  THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/30) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30 THEN DATE_PART('day', gf.co_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/30) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30 THEN DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/30) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31  THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/(DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31 THEN DATE_PART('day', gf.co_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/(DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31 THEN DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/(DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                + " WHEN gf.rmrttyp='D' THEN (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf JOIN rssys.charge c ON c.chg_code=cf.chg_code AND c.chg_class in ('URENT') WHERE cf.reg_num=gf.reg_num) "
                + " END AS total_charges, " //room charge applied
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code  WHERE cf.reg_num=gf.reg_num AND c.chg_type='C' AND (c.isaddons='FALSE' OR c.isaddons IS NULL)  AND c.chg_class not in ('URENT') AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS balance, "//othercharges
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num=gf.reg_num AND c.isaddons='TRUE' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS addons "
                + " FROM " + _schema + ".gfhist gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code "
                + " WHERE gf.rom_code!='" + room_spc + "' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' ) OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') ) "
                + " ORDER BY arr_date
                 * 
                 * 
                 "SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.rom_code ||'-'|| gf.rmrttyp as rom_code,rt.rate_desc  AS rate_code,  m.mkt_desc AS market_name, 'CHECKED IN' AS status,  rom_rate as rom_rate, gf.rmrttyp, "
                                    + " CASE WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND gf.dep_date > '" + dt_to + " 00:00:00'::timestamp THEN gf.rom_rate  "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30  THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/30) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30 THEN DATE_PART('day', gf.dep_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/30) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30 THEN DATE_PART('day', gf.dep_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/30) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31  THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/(DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31 THEN DATE_PART('day', gf.dep_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/(DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31 THEN DATE_PART('day', gf.dep_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/(DATE_PART('day', gf.dep_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                                    + " WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND gf.dep_date > '" + dt_to + " 00:00:00'::timestamp THEN (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf WHERE cf.reg_num=gf.reg_num AND (cf.chg_code='001' OR cf.chg_code='005' OR cf.chg_code='006')) "
                                    + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date > '" + dt_to + " 00:00:00'::timestamp THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/7) "
                                     + " WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp THEN DATE_PART('day', gf.dep_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/7) "
                                    + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND dep_date <= '" + dt_to + " 00:00:00'::timestamp THEN DATE_PART('day', gf.dep_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/7) "
                                    + " WHEN gf.rmrttyp='D' THEN (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf WHERE cf.reg_num=gf.reg_num AND (cf.chg_code='001' OR cf.chg_code='005' OR cf.chg_code='006')) "
                                    + " END AS total_charges, " //room charge applied
                                    + " (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num=gf.reg_num AND c.chg_type='C' AND (c.isaddons='FALSE' OR c.isaddons IS NULL) AND c.chg_code!='001' AND c.chg_code!='005' AND c.chg_code!='006'  AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS balance, " //othercharges
                                    + " (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num=gf.reg_num AND c.isaddons='TRUE' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS addons "
                                    + " FROM " + _schema + ".gfolio gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code "
                                    + " WHERE gf.rom_code!='Z01' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' ) OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "')) "
                                    + " UNION ALL "
                                    + " SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, to_char(gf.co_date, 'MM/dd/yyyy') AS dep_date, gf.rom_code ||'-'|| gf.rmrttyp as rom_code, rt.rate_desc  AS rate_code, m.mkt_desc AS market_name, 'CHECKED OUT' AS status, gf.rom_rate as rom_rate, gf.rmrttyp, "
                                    + " CASE WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND gf.co_date > '" + dt_to + " 00:00:00'::timestamp THEN (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf WHERE cf.reg_num=gf.reg_num AND (cf.chg_code='001' OR cf.chg_code='005' OR cf.chg_code='006')) "
                                    + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) > 7 THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/7) "
                                    + " WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) > 7 THEN DATE_PART('day', gf.co_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/7) "
                                    + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) > 7 THEN DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/7) "
                                    + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) < 8 THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/( DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp))) "
                                    + " WHEN gf.rmrttyp='W' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) < 8 THEN DATE_PART('day', gf.co_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/( DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp))) "
                                    + " WHEN gf.rmrttyp='W' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp) < 8 THEN DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/( DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp))) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND gf.co_date > '" + dt_to + " 00:00:00'::timestamp THEN gf.rom_rate  "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30  THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/30) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30 THEN DATE_PART('day', gf.co_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/30) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) > 30 THEN DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/30) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date > '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31  THEN DATE_PART('day', '" + dt_to + " 00:00:00'::timestamp - gf.arr_date) * (gf.rom_rate/(DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date < '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31 THEN DATE_PART('day', gf.co_date - '" + dt_frm + " 00:00:00'::timestamp ) * (gf.rom_rate/(DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                                    + " WHEN gf.rmrttyp='M' AND gf.arr_date >= '" + dt_frm + " 00:00:00'::timestamp AND co_date <= '" + dt_to + " 00:00:00'::timestamp AND DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')) < 31 THEN DATE_PART('day', gf.co_date::timestamp - gf.arr_date::timestamp ) * (gf.rom_rate/(DATE_PART('day', gf.co_date::timestamp - to_timestamp(DATE_PART('year', gf.arr_date::timestamp)||'-" + prev_mo + "-'||DATE_PART('day', gf.arr_date::timestamp), 'yyyy-MM-dd')))) "
                                    + " WHEN gf.rmrttyp='D' THEN (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf WHERE cf.reg_num=gf.reg_num AND (cf.chg_code='001' OR cf.chg_code='005' OR cf.chg_code='006')) "
                                    + " END AS total_charges, " //room charge applied
                                    + " (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num=gf.reg_num AND c.chg_type='C' AND (c.isaddons='FALSE' OR c.isaddons IS NULL)  AND c.chg_code!='001' AND c.chg_code!='005' AND c.chg_code!='006' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS balance, "//othercharges
                                    + " (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num=gf.reg_num AND c.isaddons='TRUE' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS addons "
                                    + " FROM " + _schema + ".gfhist gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code "
                                    + " WHERE gf.rom_code!='Z01' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' ) OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') ) "
                                    + " ORDER BY arr_date" 
                  
                 */


                myReportDocument.Load(fileloc_hotel + "307_summaryofcharges.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //noOfRows
                crParameterDiscreteValue.Value = dt.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noOfRows"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "307B")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;
                String prev_mo = Convert.ToDateTime(dtpicker_frm).AddMonths(-1).ToString("MM");

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }

                DataTable dt = db.QueryBySQLCode("SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, to_char(gf.dep_date, 'MM/dd/yyyy') AS dep_date, gf.rom_code ||'-'|| gf.rmrttyp as rom_code,rt.rate_desc  AS rate_code,  m.mkt_desc AS market_name, 'CHECKED IN' AS status,  rom_rate as rom_rate, gf.rmrttyp, "
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf WHERE cf.reg_num=gf.reg_num AND (cf.chg_code='032' OR cf.chg_code='003') AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS laundry, "
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf WHERE cf.reg_num=gf.reg_num AND cf.chg_code='011' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS food, "
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf WHERE cf.reg_num=gf.reg_num AND cf.chg_code='314' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS purifiedwater, "
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num=gf.reg_num AND c.chg_type='C' AND (c.isaddons='FALSE' OR c.isaddons IS NULL) AND c.chg_code!='032' AND c.chg_code!='003' AND c.chg_code!='011' AND c.chg_code!='314' AND c.chg_code!='001' AND c.chg_code!='005' AND c.chg_code!='006' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS others "//othercharges
                + " FROM " + _schema + ".gfolio gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code "
                + " WHERE gf.rom_code!='" + room_spc + "' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' ) OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "')) "
                + " UNION ALL "
                + " SELECT DISTINCT gf.reg_num, gf.acct_no, gf.full_name, to_char(gf.arr_date, 'MM/dd/yyyy') AS arr_date, to_char(gf.co_date, 'MM/dd/yyyy') AS dep_date, gf.rom_code ||'-'|| gf.rmrttyp as rom_code, rt.rate_desc  AS rate_code, m.mkt_desc AS market_name, 'CHECKED OUT' AS status, gf.rom_rate as rom_rate, gf.rmrttyp, "
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf WHERE cf.reg_num=gf.reg_num AND (cf.chg_code='032' OR cf.chg_code='003') AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS laundry, "
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf WHERE cf.reg_num=gf.reg_num AND cf.chg_code='011' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS food, "
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf WHERE cf.reg_num=gf.reg_num AND cf.chg_code='314' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS purifiedwater, "
                + " (SELECT SUM(cf.amount) FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num=gf.reg_num AND c.chg_type='C' AND (c.isaddons='FALSE' OR c.isaddons IS NULL) AND c.chg_code!='032' AND c.chg_code!='003' AND c.chg_code!='011' AND c.chg_code!='314' AND c.chg_code!='001' AND c.chg_code!='005' AND c.chg_code!='006' AND cf.t_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') AS others "//othercharges
                + " FROM " + _schema + ".gfhist gf  LEFT JOIN " + _schema + ".market m ON m.mkt_code=gf.mkt_code LEFT JOIN " + _schema + ".ratetype rt ON rt.rate_code=gf.rate_code "
                + " WHERE gf.rom_code!='" + room_spc + "' AND ((arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "' ) OR (dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') ) "
                + " ORDER BY arr_date");

                myReportDocument.Load(fileloc_hotel + "307_summaryofcharges2.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //noOfRows
                crParameterDiscreteValue.Value = dt.Rows.Count.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noOfRows"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "308")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }
                //MessageBox.Show("pass");
                //db.clear_temp_chghist(rpt_no);// MessageBox.Show("pass");
                //db.transtotemp_chghist(GlobalClass.username, rpt_no, true, "SELECT cf.*, gf.full_name, gf.arr_date, gf.dep_date, gf.co_date, gf.disc_code FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".gfhist gf ON cf.reg_num=gf.reg_num WHERE cf.chg_date>='" + dtpicker_frm + "' AND cf.chg_date<='" + dtpicker_to + "'" + WHERE);
                //MessageBox.Show("pass");
                //db.transtotemp_chghist(GlobalClass.username, rpt_no, false, "SELECT cf.*, gf.full_name, gf.arr_date, gf.dep_date, gf.disc_code FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".gfolio gf ON cf.reg_num=gf.reg_num WHERE cf.chg_date>='" + dtpicker_frm + "' AND cf.chg_date<='" + dtpicker_to + "'" + WHERE);
                //MessageBox.Show("pass");  AND c.ismisc='TRUE'
                DataTable dt = db.QueryBySQLCode("SELECT to_char(cf.chg_date,'YYYY/MM/DD') AS date, SUM(CASE WHEN c.chg_class in ('URENT') THEN cf.amount ELSE 0.00 END) AS net_rate, SUM(CASE WHEN c.chg_class in ('URENT') THEN cf.sc_amnt ELSE 0.00 END) AS serv_chg, SUM(CASE WHEN c.chg_class in ('URENT') THEN cf.vat_amnt ELSE 0.00 END) AS govt_tax, SUM(CASE WHEN COALESCE(c.chg_class,'') not in ('URENT','F&B') THEN cf.amount ELSE 0.00 END) AS misc_netrate, SUM(CASE WHEN COALESCE(c.chg_class,'') not in ('URENT','F&B') THEN cf.sc_amnt ELSE 0.00 END) AS misc_serv_chg, SUM(CASE WHEN COALESCE(c.chg_class,'') not in ('URENT','F&B') THEN cf.vat_amnt ELSE 0.00 END) AS misc_vat, SUM(CASE WHEN c.chg_class in ('F&B') THEN cf.amount ELSE 0.00 END) AS fb_netrate, SUM(CASE WHEN c.chg_class in ('F&B') THEN cf.sc_amnt ELSE 0.00 END) AS fb_serv_chg, SUM(CASE WHEN c.chg_class in ('F&B')   THEN cf.vat_amnt ELSE 0.00 END) AS fb_vat FROM  (SELECT cf.chg_code, cf.chg_num, cf.amount, cf.vat_amnt, cf.sc_amnt, cf.chg_date, cf.user_id  FROM rssys.chgfil cf JOIN rssys.gfolio gf ON gf.reg_num=cf.reg_num UNION ALL SELECT cf.chg_code, cf.chg_num, cf.amount, cf.vat_amnt, cf.sc_amnt, cf.chg_date, cf.user_id FROM rssys.chghist cf JOIN rssys.gfhist gf ON gf.reg_num=cf.reg_num ) cf JOIN rssys.charge c ON c.chg_code=cf.chg_code WHERE COALESCE(c.chg_type,'')='C' AND cf.chg_date >= '" + dt_frm + "' AND cf.chg_date <= '" + dt_to + "' " + WHERE + " GROUP BY cf.chg_date ORDER BY cf.chg_date ASC");

                /*
                 * SELECT cf.chg_date AS date, cf.disc_code, SUM(CASE WHEN c.chg_class in ('URENT') THEN cf.amount ELSE 0.00 END) AS net_rate, SUM(CASE WHEN c.chg_class in ('URENT') THEN cf.sc_amnt ELSE 0.00 END) AS serv_chg, SUM(CASE WHEN c.chg_class in ('URENT') THEN cf.vat_amnt ELSE 0.00 END) AS govt_tax, SUM(CASE WHEN COALESCE(c.chg_class,'') not in ('URENT','F&B') THEN cf.amount ELSE 0.00 END) AS misc_netrate, SUM(CASE WHEN COALESCE(c.chg_class,'') not in ('URENT','F&B') THEN cf.sc_amnt ELSE 0.00 END) AS misc_serv_chg, SUM(CASE WHEN COALESCE(c.chg_class,'') not in ('URENT','F&B') THEN cf.vat_amnt ELSE 0.00 END) AS misc_vat, SUM(CASE WHEN c.chg_class in ('F&B') THEN cf.amount ELSE 0.00 END) AS fb_netrate, SUM(CASE WHEN c.chg_class in ('F&B') THEN cf.sc_amnt ELSE 0.00 END) AS fb_serv_chg, SUM(CASE WHEN c.chg_class in ('F&B') THEN cf.vat_amnt ELSE 0.00 END) AS fb_vat FROM " + _schema + ".temp_chghist cf JOIN rssys.charge c ON c.chg_code=cf.chg_code WHERE  cf.chg_date >= '" + dt_frm + "' AND cf.chg_date <= '" + dt_to + "' AND user_id2='" + GlobalClass.username + "' AND temp_id='" + rpt_no + "' GROUP BY cf.chg_date, cf.disc_code ORDER BY cf.chg_date ASC
                 * 
                 * SELECT cf.chg_date AS date, cf.disc_code, (SELECT COALESCE(SUM(cf1.amount),0.00) FROM rssys.temp_chghist cf1 JOIN rssys.charge c ON c.chg_code=cf1.chg_code AND c.chg_class in ('URENT')  WHERE cf1.chg_date=cf.chg_date) AS net_rate, (SELECT COALESCE(SUM(cf2.sc_amnt),0.00) FROM rssys.temp_chghist cf2  JOIN rssys.charge c ON cf2.chg_code=c.chg_code AND c.chg_class in ('URENT') WHERE cf2.chg_date=cf.chg_date) AS serv_chg, (SELECT COALESCE(SUM(cf3.vat_amnt),0.00) FROM rssys.temp_chghist cf3 JOIN rssys.charge c ON cf3.chg_code=c.chg_code AND c.chg_class in ('URENT') WHERE cf3.chg_date=cf.chg_date) AS govt_tax, (Select COALESCE(SUM(cf4.amount),0.00) FROM rssys.temp_chghist cf4 LEFT JOIN rssys.charge c4 ON cf4.chg_code=c4.chg_code AND c4.chg_class not in ('URENT','F&B') WHERE cf4.chg_date=cf.chg_date) AS misc_netrate, (Select COALESCE(SUM(cf4.sc_amnt),0.00) FROM rssys.temp_chghist cf4 LEFT JOIN rssys.charge c4 ON cf4.chg_code=c4.chg_code AND c4.chg_class not in ('URENT','F&B') WHERE cf4.chg_date=cf.chg_date) AS misc_serv_chg, (Select COALESCE(SUM(cf4.vat_amnt),0.00) FROM rssys.temp_chghist cf4 LEFT JOIN rssys.charge c4 ON cf4.chg_code=c4.chg_code AND c4.chg_class not in ('URENT','F&B') WHERE cf4.chg_date=cf.chg_date) AS misc_vat, (SELECT COALESCE(SUM(cf3.amount),0.00) FROM rssys.temp_chghist cf3 JOIN rssys.charge c ON c.chg_code=cf3.chg_code AND c.chg_class in ('F&B') WHERE cf3.chg_date=cf.chg_date) AS fb_netrate, (SELECT COALESCE(SUM(cf3.sc_amnt),0.00) FROM rssys.temp_chghist cf3 JOIN rssys.charge c ON c.chg_code=cf3.chg_code AND c.chg_class in ('F&B') WHERE cf3.chg_date=cf.chg_date) AS fb_serv_chg, (SELECT COALESCE(SUM(cf3.vat_amnt),0.00) FROM rssys.temp_chghist cf3 JOIN rssys.charge c ON c.chg_code=cf3.chg_code AND c.chg_class in ('F&B') WHERE cf3.chg_date=cf.chg_date) AS fb_vat FROM " + _schema + ".temp_chghist cf WHERE cf.chg_date >= '" + dt_frm + "' AND cf.chg_date <= '" + dt_to + "' AND user_id2='" + GlobalClass.username + "' AND temp_id='" + rpt_no + "' GROUP BY cf.chg_date, cf.disc_code ORDER BY cf.chg_date ASC
                 * 
                 "SELECT cf.chg_date AS date, cf.disc_code, (SELECT SUM(cf1.amount) FROM " + _schema + ".temp_chghist cf1 JOIN rssys.charge c ON c.chg_code=cf1.chg_code AND c.chg_class in ('URENT')  WHERE cf1.chg_date=cf.chg_date) AS net_rate, (SELECT SUM(cf2.sc_amnt) FROM " + _schema + ".temp_chghist cf2  JOIN " + _schema + ".charge c ON cf2.chg_code=c.chg_code AND c.ismisc='FALSE'  WHERE cf2.chg_date=cf.chg_date) AS serv_chg, (SELECT SUM(cf3.vat_amnt) FROM " + _schema + ".temp_chghist cf3 JOIN " + _schema + ".charge c ON cf3.chg_code=c.chg_code AND c.ismisc='FALSE' WHERE cf3.chg_date=cf.chg_date) AS govt_tax, (Select SUM(cf4.vat_amnt) FROM " + _schema + ".temp_chghist cf4 LEFT JOIN " + _schema + ".charge c4 ON cf4.chg_code=c4.chg_code WHERE c4.ismisc='TRUE' AND cf4.chg_date=cf.chg_date) AS misc_netrate, (Select SUM(cf4.sc_amnt) FROM " + _schema + ".temp_chghist cf4 LEFT JOIN " + _schema + ".charge c4 ON cf4.chg_code=c4.chg_code WHERE c4.ismisc='TRUE' AND cf4.chg_date=cf.chg_date) AS misc_serv_chg, (Select SUM(cf4.vat_amnt) FROM " + _schema + ".temp_chghist cf4 LEFT JOIN " + _schema + ".charge c4 ON cf4.chg_code=c4.chg_code WHERE c4.ismisc='TRUE' AND cf4.chg_date=cf.chg_date) AS misc_vat, (SELECT SUM(cf3.amount) FROM " + _schema + ".temp_chghist cf3 JOIN rssys.charge c ON c.chg_code=cf3.chg_code AND c.chg_class in ('F&B') WHERE cf3.chg_date=cf.chg_date) AS fb_netrate, (SELECT SUM(cf3.sc_amnt) FROM " + _schema + ".temp_chghist cf3 JOIN rssys.charge c ON c.chg_code=cf3.chg_code AND c.chg_class in ('F&B') WHERE cf3.chg_date=cf.chg_date) AS fb_serv_chg, (SELECT SUM(cf3.vat_amnt) FROM " + _schema + ".temp_chghist cf3 JOIN rssys.charge c ON c.chg_code=cf3.chg_code AND c.chg_class in ('F&B') WHERE cf3.chg_date=cf.chg_date) AS fb_vat FROM " + _schema + ".temp_chghist cf WHERE cf.chg_date >= '" + dt_frm + "' AND cf.chg_date <= '" + dt_to + "' AND user_id2='" + GlobalClass.username + "' AND temp_id='" + rpt_no + "' GROUP BY cf.chg_date, cf.disc_code ORDER BY cf.chg_date ASC"
                 */

                myReportDocument.Load(fileloc_hotel + "308_MonthlyChargesReport.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "309")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }
                DataTable dt = db.QueryBySQLCode("SELECT DISTINCT cf.chg_code || ' - ' || c.chg_desc AS chg_desc, cf.chg_date AS t_date, cf.t_time, cf.rom_code,  "
                    + " cf.reg_num || ' - ' || gf.full_name AS full_name, cf.reference, cf.amount, cf.user_id "
                    + " FROM " + _schema + ".chgfil cf "
                    + " LEFT JOIN  " + _schema + ".gfolio gf ON gf.reg_num=cf.reg_num "
                    + " LEFT JOIN " + _schema + ".charge c ON cf.chg_code=c.chg_code "
                    + " WHERE ((gf.arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') OR (gf.dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "'))  " + WHERE
                    + " AND c.chg_type='C' AND COALESCE(c.chg_class,'') not in ('URENT','F&B') "
                    + " UNION ALL "
                    + " SELECT DISTINCT cf.chg_code || ' - ' || c.chg_desc AS chg_desc, cf.chg_date AS t_date, cf.t_time, cf.rom_code,  "
                    + " cf.reg_num || ' - ' || gf.full_name AS full_name, cf.reference, cf.amount, cf.user_id "
                    + " FROM " + _schema + ".chghist cf "
                    + " LEFT JOIN  " + _schema + ".gfhist gf ON gf.reg_num=cf.reg_num "
                    + " LEFT JOIN " + _schema + ".charge c ON cf.chg_code=c.chg_code "
                    + " WHERE ((gf.arr_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "') OR (gf.dep_date BETWEEN '" + dt_frm + "' AND '" + dt_to + "'))  " + WHERE
                    + " AND c.chg_type='C' AND COALESCE(c.chg_class,'') not in ('URENT','F&B') "
                    + " ORDER BY chg_desc, t_date, t_time ");

                myReportDocument.Load(fileloc_hotel + "309_miscelleneousdetailreport.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "310")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }

                DataTable dt = db.QueryBySQLCode("SELECT gf.full_name || ' - ' || gf.reg_num AS reg_num, cf.chg_date AS t_date, cf.t_time, cf.reference, cf.amount, cf.vat_amnt, cf.sc_amnt, cf.tofr_fol, cf.user_id " +
                    "FROM " + _schema + ".gfolio gf " +
                    "LEFT JOIN " + _schema + ".chgfil cf ON gf.reg_num=cf.reg_num " +
                    "WHERE cf.chg_date >= '" + dt_frm + "' AND cf.chg_date <= '" + dt_to + "' AND gf.rom_code='" + room_spc + "' AND gf.reg_num !='" + resvation_folio + "' " + 
                    "ORDER BY gf.full_name");

                myReportDocument.Load(fileloc_hotel + "310_chargebalancesreport.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "311")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cashier='" + get_cbo_value(cbo_1) + "'";
                }

                DataTable dt = db.QueryBySQLCode("SELECT dsr_dt, cash_amnt, crd_amnt, cci_amnt, cca_amnt, extra, sales, fb_sales FROM " + _schema + ".dsr WHERE dsr_dt>='" + dt_frm + "' AND dsr_dt<='" + dt_to + "'" + WHERE);

                myReportDocument.Load(fileloc_hotel + "311_dsr_summary.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "312")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                }

                DataTable dt = db.QueryBySQLCode("SELECT cf.chg_code || ' - ' || c.chg_desc AS chg_code, cf.chg_date AS t_date, cf.t_time, cf.reference, cf.amount, cf.vat_amnt, cf.sc_amnt, cf.tofr_fol, cf.user_id " +
                "FROM " + _schema + ".gfolio gf " +
                "LEFT JOIN " + _schema + ".chgfil cf ON gf.reg_num=cf.reg_num " +
                "LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code " +
                "WHERE cf.chg_date >= '" + dt_frm + "' AND cf.chg_date <= '" + dt_to + "'  AND gf.rom_code='" + room_spc + "'  AND gf.reg_num='" + resvation_folio + "' " + 
                "ORDER BY cf.chg_code, gf.t_date, gf.t_time");

                myReportDocument.Load(fileloc_hotel + "312_extraitemrpt.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "313")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE = WHERE + " AND gf.acct_no='" + get_cbo_value(cbo_1) + "'";
                }

                DataTable dt = db.QueryBySQLCode("SELECT to_char(t_date,'YYYY/MM/DD') AS t_date, t_time, res_code, full_name, arr_date, dep_date, rom_code, typ_code, SUM(ap_dep_amnt) AS total_payment FROM " + _schema + ".gfolio gf WHERE gf.t_date >= '" + dt_frm + "' AND gf.t_date <= '" + dt_to + "' AND COALESCE(res_code,'')<>'' GROUP BY t_date, t_time, res_code, full_name, arr_date, dep_date, rom_code, typ_code ORDER BY gf.t_date, t_time");
                /*
                 SELECT cf.chg_code || ' - ' || c.chg_desc AS chg_code, cf.chg_date AS t_date, cf.t_time, cf.reference, cf.amount, cf.vat_amnt, cf.sc_amnt, cf.tofr_fol, cf.user_id " +
                    "FROM " + _schema + ".gfolio gf " +
                    "LEFT JOIN " + _schema + ".chgfil cf ON gf.reg_num=cf.reg_num " +
                    "LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code " +
                    "WHERE cf.chg_date >= '" + dt_frm + "' AND cf.chg_date <= '" + dt_to + "' AND gf.reg_num='" + resvation_folio + "' AND gf.rom_code='" + room_spc + "'  " + // 
                    "ORDER BY cf.chg_code, gf.t_date, gf.t_time
                 
                 */


                myReportDocument.Load(fileloc_hotel + "313_reservationadvancepayment.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "314")
            {
                String clerk = "All";
                String WHERE1 = "", WHERE2 = ""; ;
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE1 = WHERE1 + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                    WHERE2 = WHERE2 + " AND cf2.user_id='" + get_cbo_value(cbo_1) + "'";
                }
                DataTable dt = db.QueryBySQLCode("SELECT cf.chg_desc AS chg_desc, cf.chg_date AS t_date, cf.t_time, cf.rom_code, cf.reg_num, gf.full_name, cf.reference, " +
                    "cf.amount*-1 AS amount, cf.user_id, 'CHECK OUT' AS status " +
                    "FROM " + _schema + ".gfhist gf " +
                    "LEFT JOIN (SELECT cf.*, c.* FROM " + _schema + ".chghist cf INNER JOIN " + _schema + ".charge c ON cf.chg_code=c.chg_code WHERE c.chg_type='P' AND COALESCE(c.chg_class,'')<>'') cf ON gf.reg_num=cf.reg_num " +
                    "WHERE cf.t_date>='" + dt_frm + "' AND cf.t_date<='" + dt_to + "' " + WHERE1 +
                    " UNION ALL " +
                    "SELECT cf2.chg_desc AS chg_desc, cf2.chg_date AS t_date, cf2.t_time, cf2.rom_code, cf2.reg_num, gf2.full_name, cf2.reference, " +
                    "cf2.amount*-1 AS amount, cf2.user_id, 'CHECK IN' AS status " +
                    "FROM " + _schema + ".gfolio gf2 " +
                    "LEFT JOIN (SELECT cf2.*, c2.* FROM " + _schema + ".chgfil cf2 INNER JOIN " + _schema + ".charge c2 ON cf2.chg_code=c2.chg_code WHERE c2.chg_type='P' AND (soa != 'Y' OR soa IS NULL) AND COALESCE(c2.chg_class,'')<>'') cf2 ON gf2.reg_num=cf2.reg_num " +
                    "" +
                    "WHERE cf2.t_date>='" + dt_frm + "' AND cf2.t_date<='" + dt_to + "' " + WHERE2 +
                    " ORDER BY chg_desc, t_date, t_time");
                myReportDocument.Load(fileloc_hotel + "314_dailyremittance.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //preparedby
                crParameterDiscreteValue.Value = GlobalClass.user_fullname;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["preparedby"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "315")
            {
                String clerk = "All";
                String WHERE1 = "", WHERE2 = ""; ;
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;
                String chg_code = "", chg_desc = "";

                if (get_cbo_index(cbo_1) > -1)
                {
                    clerk = get_cbo_text(cbo_1);

                    WHERE1 = WHERE1 + " AND cf.user_id='" + get_cbo_value(cbo_1) + "'";
                    WHERE2 = WHERE2 + " AND cf2.user_id='" + get_cbo_value(cbo_1) + "'";
                }
                if (get_cbo_index(cbo_2) > -1)
                {
                    chg_code = get_cbo_value(cbo_2);
                    chg_desc = " For " + get_cbo_text(cbo_2).ToUpper() + " Only";

                    WHERE1 = WHERE1 + " AND cf.chg_code='" + chg_code + "'";
                    WHERE2 = WHERE2 + " AND cf2.chg_code='" + chg_code + "'";
                }

                DataTable dt = db.QueryBySQLCode("SELECT c.chg_code ||' - '|| c.chg_desc AS chg_desc, cf.chg_date AS t_date, cf.t_time, cf.chg_num AS chg_code, cf.rom_code, cf.reg_num ||' - '|| gf.full_name AS full_name, cf.reference, " +
                "cf.amount*-1 AS amount, cf.user_id, 'OUT' AS status " +
                "FROM " + _schema + ".gfhist gf " +
                "LEFT JOIN " + _schema + ".chghist cf ON gf.reg_num=cf.reg_num " +
                "LEFT JOIN " + _schema + ".charge c ON cf.chg_code=c.chg_code " +
                "WHERE cf.chg_date>='" + dt_frm + "' AND cf.chg_date<='" + dt_to + "' " + WHERE1 +
                " UNION " +
                "SELECT c2.chg_code ||' - '|| c2.chg_desc AS chg_desc, cf2.chg_date AS t_date, cf2.t_time, cf2.chg_num AS chg_code, cf2.rom_code, cf2.reg_num ||' - '|| gf2.full_name AS full_name, cf2.reference, " +
                "cf2.amount*-1 AS amount, cf2.user_id, 'IN' AS status " +
                "FROM " + _schema + ".gfolio gf2 " +
                "LEFT JOIN " + _schema + ".chgfil cf2 ON gf2.reg_num=cf2.reg_num " +
                "LEFT JOIN " + _schema + ".charge c2 ON cf2.chg_code=c2.chg_code " +
                "WHERE cf2.chg_date>='" + dt_frm + "' AND cf2.chg_date<='" + dt_to + "' " + WHERE2 +
                " ORDER BY chg_desc, t_date, t_time");

                myReportDocument.Load(fileloc_hotel + "315_shiftdetailreport.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                //preparedby
                crParameterDiscreteValue.Value = chg_code;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["addtitle"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //preparedby
                crParameterDiscreteValue.Value = GlobalClass.user_fullname;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["preparedby"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //clerk
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();

                 
            }
            else if (rpt_no == "403")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;

                inc_pbar(5);

                DataTable dt = db.QueryBySQLCode("SELECT gf.reg_num, gf.full_name, gf.arr_date, gf.dep_date, gf.rom_code, gf.typ_code, gf.rom_rate, gf.disc_pct, (SELECT SUM(cf1.amount) FROM " + _schema + ".chgfil cf1 LEFT JOIN " + _schema + ".charge c1 ON c1.chg_code=cf1.chg_code WHERE cf1.reg_num=gf.reg_num AND c1.chg_type='P') AS total_payment, (SELECT SUM(cf1.amount) FROM " + _schema + ".chgfil cf1 LEFT JOIN " + _schema + ".charge c1 ON c1.chg_code=cf1.chg_code WHERE cf1.reg_num=gf.reg_num AND c1.chg_type='C') AS total_charges, sum(cf.amount)AS balance, 'CHECK-IN' AS status FROM " + _schema + ".gfolio gf LEFT JOIN " + _schema + ".chgfil cf ON cf.reg_num=gf.reg_num WHERE gf.rom_code!='" + room_spc + "' AND gf.arr_date>='" + dt_frm + "' AND gf.arr_date<='" + dt_to + "' GROUP BY gf.reg_num, gf.full_name, gf.arr_date, gf.dep_date, gf.rom_code, gf.typ_code, gf.rom_rate, gf.disc_pct");
                inc_pbar(25);
                myReportDocument.Load(fileloc_hotel + "403_SummaryOfGuestBalances.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                inc_pbar(5);
                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                
            }
            else if (rpt_no == "404")
            {
                String clerk = "All";
                String WHERE = "";
                String dt_frm = dtpicker_frm;
                String dt_to = dtpicker_to;
                inc_pbar(5);
                DataTable dt = db.QueryBySQLCode("SELECT gf.reg_num, gf.full_name, gf.arr_date, gf.dep_date, gf.rom_code, gf.typ_code, gf.rom_rate, gf.disc_pct, (SELECT SUM(cf1.amount) FROM " + _schema + ".chghist cf1 LEFT JOIN " + _schema + ".charge c1 ON c1.chg_code=cf1.chg_code WHERE cf1.reg_num=gf.reg_num AND c1.chg_type='P') AS total_payment, (SELECT SUM(cf1.amount) FROM " + _schema + ".chghist cf1 LEFT JOIN " + _schema + ".charge c1 ON c1.chg_code=cf1.chg_code WHERE cf1.reg_num=gf.reg_num AND c1.chg_type='C') AS total_charges, sum(cf.amount)AS balance, 'CHECK-IN' AS status FROM " + _schema + ".gfhist gf LEFT JOIN " + _schema + ".chghist cf ON cf.reg_num=gf.reg_num WHERE gf.rom_code!='" + room_spc + "' AND gf.dep_date>='" + dt_frm + "' AND gf.dep_date<='" + dt_to + "' GROUP BY gf.reg_num, gf.full_name, gf.arr_date, gf.dep_date, gf.rom_code, gf.typ_code, gf.rom_rate, gf.disc_pct");
                inc_pbar(25);
                myReportDocument.Load(fileloc_hotel + "404_summaryofguestbalances_checkout.rpt");
                inc_pbar(5);
                myReportDocument.Database.Tables[0].SetDataSource(dt);
                inc_pbar(5);
                //from date
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //to date
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                clr_param();
                inc_pbar(5);
                 
            }

            if (!String.IsNullOrEmpty(rpt_no))
            {
                inc_pbar(30);
                crParameterDiscreteValue.Value = db.get_pk("comp_name");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["m99company"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(30);
                crParameterDiscreteValue.Value = db.get_pk("comp_addr");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["m99address"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                //inc_pbar(40);
                inc_pbar(30);
                crParameterDiscreteValue.Value = db.get_pk("comp_tel");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["m99tel_no"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();


                //user_id
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                disp_reportviewer(myReportDocument);
            }


            db.clear_temp_chghist(rpt_no);
            // }
            //catch (Exception er)
            //{
            //   MessageBox.Show(er.Message);
            // }

            reset_pbar();

            input_enable(true);

            pbar_panl_hide();
        }

        private void RPT_RES_entry_FormClosing(object sender, FormClosingEventArgs e)
        {
            //bgworker.CancelAsync();
        }
    }
}
