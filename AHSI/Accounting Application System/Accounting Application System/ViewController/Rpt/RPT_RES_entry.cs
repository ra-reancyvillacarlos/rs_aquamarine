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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Accounting_Application_System
{
    public partial class RPT_RES_entry : Form
    {

        String rpt_no = "";
        String _schema = "";
        thisDatabase db;
        GlobalMethod gm;
        GlobalClass gc;

        ReportDocument myReportDocument;
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        ParameterFieldDefinitions crParameterFieldDefinitions;

        String fileloc_acctg = "";
        String fileloc_hotel = "";
        String fileloc_inv = "";
        String fileloc_md = "";
        String fileloc_sale = "";
        String fileloc_proj = "";
        String fileloc_srvc = "";
        String comp_name, comp_addr;
        Boolean ishide_opt = true;

        Boolean hasBranch = false;

        Boolean isReady = false;

        String _fy = "";

        public RPT_RES_entry(String rnum)
        {
            InitializeComponent();
            db = new thisDatabase();
            gm = new GlobalMethod();
            gc = new GlobalClass();
            myReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();

            DateTime this_t_date = Convert.ToDateTime(db.get_systemdate(""));
            String system_loc = db.get_system_loc();

            rpt_no = rnum;
            fileloc_proj = system_loc + "\\Reports\\Project\\";

            fileloc_acctg = system_loc + "\\Reports\\Accounting\\";
            fileloc_hotel = system_loc + "\\Reports\\Hotel\\";
            fileloc_inv = system_loc + "\\Reports\\Inventory\\";
            fileloc_md = system_loc + "\\Reports\\MD\\";
            fileloc_srvc = system_loc + "\\Reports\\Service\\";
            fileloc_sale = system_loc + "\\Reports\\Sale\\";
            _fy = db.get_m99fy();
            dtp_frm.Value = this_t_date;
            dtp_to.Value = this_t_date;



            try
            {
                if (rpt_no[0] == 'A' && rpt_no[1] != '0' && Array.IndexOf(new String[] { "A106" }, rpt_no) < 0)
                {
                    hasBranch = true;
                }

                //Master Data
                if (rpt_no == "M001")
                {
                    this.Text = "Master File - Items Report";
                    grp_bydate.Text = "";
                    grp_bydate.Hide();
                    grp_options.Text = "Filter By";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();

                    lbl_cbo_1.Text = "Category";
                    lbl_cbo_2.Text = "Brand";
                    gc.load_category(cbo_1);
                    gc.load_brand(cbo_2);

                }
                //Accounting
                else if (rpt_no == "A001")
                {
                    this.Text = "Daily Collection Report";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Hide();

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
                else if (rpt_no == "A101")
                {
                    this.Text = "Account Activity By ID";
                    grp_bydate.Text = "Entry Dates";
                    /*
                    grp_bydate.Text = "";
                    lbl_dt_frm.Text = "As Of";
                    dtp_to.Hide();
                    lbl_dt_to.Hide();
                     */

                    chk_1.Text = "Unposted Entries";
                    chk_1.Checked = true;
                    chk_2.Text = "Posted Entries";
                    chk_2.Checked = true;
                    chk_3.Hide();

                    gc.load_account_title(cbo_1);
                    gc.load_journal_code_asc(cbo_2);
                    gc.load_journal_code_asc(cbo_3);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Journal From";
                    lbl_cbo_3.Text = "Journal To";

                }
                else if (rpt_no == "A102")
                {
                    this.Text = "List of Unposted Entries";
                    grp_bydate.Text = "Entry Dates";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Journal";
                    gc.load_journal_code_asc(cbo_1);
                    lbl_cbo_2.Text = "Period From";
                    gc.load_openperiod(cbo_2);
                    lbl_cbo_3.Text = "To";
                    gc.load_openperiod(cbo_3);
                }
                else if (rpt_no == "A103")
                {
                    this.Text = "Cash Status Report";
                    grp_bydate.Text = "Entry Dates";

                    chk_1.Text = "Unposted Entries";
                    chk_1.Checked = true;
                    chk_2.Text = "Posted Entries";
                    chk_2.Checked = true;

                    chk_3.Text = "Include Previous";
                    chk_3.Checked = false;


                    gc.load_account_title_check_writting(cbo_1);
                    gc.load_account_title_check_writting(cbo_2);

                    cbo_3.DataSource = db.QueryBySQLCode("SELECT DISTINCT fy FROM rssys.x03 ORDER BY fy DESC");
                    cbo_3.DisplayMember = "fy";
                    cbo_3.ValueMember = "fy";
                    cbo_3.SelectedIndex = -1;
                    cbo_3.SelectedValue = DateTime.Now.ToString("yyyy");

                    lbl_cbo_1.Text = "Account From";
                    lbl_cbo_2.Text = "Account To";
                    lbl_cbo_3.Text = "Financial Year";

                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
                }
                else if (rpt_no == "A104")
                {
                    this.Text = "Unposted Entries";
                    grp_bydate.Text = "Entry Dates";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Journal";
                    gc.load_journal_code_asc(cbo_1);
                    lbl_cbo_2.Text = "Period From";
                    gc.load_openperiod(cbo_2);
                    lbl_cbo_3.Text = "To";
                    gc.load_openperiod(cbo_3);
                }
                else if (rpt_no == "A105")
                {
                    this.Text = "Summary of Input Tax";
                    grp_bydate.Text = "Entry Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    grp_options.Hide();

                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
                }
                else if (rpt_no == "A106")
                {
                    this.Text = "Summary of Statement of Account";
                    grp_bydate.Text = "Entry Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    grp_options.Hide();
                    grp_bybranch.Hide();

                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
                }
                else if (rpt_no == "A107")
                {
                    this.Text = "Account Activity Movement";
                    grp_bydate.Text = "Entry Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_journal_code_asc(cbo_1);
                    gc.load_journal_code_asc(cbo_2);
                    cbo_3.Hide();

                    lbl_cbo_1.Text = "Journal From";
                    lbl_cbo_2.Text = "Journal To";
                    lbl_cbo_3.Hide();

                }
                else if (rpt_no == "A201")
                {
                    this.Text = "Check By Entry Date";
                    grp_bydate.Text = "Entry Dates";
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_title_code_asc(cbo_1);
                    gc.load_account_title_code_asc(cbo_2);

                    lbl_cbo_1.Text = "Account From";
                    lbl_cbo_2.Text = "Account To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();

                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
                }
                else if (rpt_no == "A202")
                {
                    this.Text = "Check By Check Date";
                    grp_bydate.Text = "Check Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_title_code_asc(cbo_1);
                    gc.load_account_title_code_asc(cbo_2);

                    lbl_cbo_1.Text = "Account From";
                    lbl_cbo_2.Text = "Account To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();

                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
                }
                else if (rpt_no == "A203")
                {
                    this.Text = "Check By Check No";
                    grp_bydate.Text = "Entry Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_title_code_asc(cbo_1);
                    gc.load_account_title_code_asc(cbo_2);

                    lbl_cbo_1.Text = "Account From";
                    lbl_cbo_2.Text = "Account To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();

                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
                }
                else if (rpt_no == "A300")
                {
                    this.Text = "General Journal";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Period";
                    gc.load_openperiod(cbo_1);

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "A301")
                {
                    this.Text = "General Ledger Activity by Account ID";
                    grp_bydate.Text = "Entry Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_title_code_asc(cbo_1);
                    gc.load_account_title_code_asc(cbo_2);
                    gc.load_journal_code_asc(cbo_3);

                    lbl_cbo_1.Text = "Account From";
                    lbl_cbo_2.Text = "Account To";
                    lbl_cbo_3.Text = "Journal";

                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
                }
                else if (rpt_no == "A302")
                {
                    this.Text = "General Ledger by Journal ID";
                    grp_bydate.Text = "Entry Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_journal_code_asc(cbo_1);
                    gc.load_journal_code_asc(cbo_2);

                    lbl_cbo_1.Text = "Journal From";
                    lbl_cbo_2.Text = "Journal To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();

                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
                }
                else if (rpt_no == "A303")
                {
                    this.Text = "General Ledger Summary by Account ID";
                    grp_bydate.Text = "Entry Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_title_code_asc(cbo_1);
                    gc.load_account_title_code_asc(cbo_2);
                    gc.load_journal_code_asc(cbo_3);

                    lbl_cbo_1.Text = "Account From";
                    lbl_cbo_2.Text = "Account To";
                    lbl_cbo_3.Text = "Journal ";

                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
                }
                else if (rpt_no == "A401")
                {
                    this.Text = "Balances from Customer";
                    grp_bydate.Text = "";
                    lbl_dt_frm.Text = "As Of";
                    dtp_to.Hide();
                    lbl_dt_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_for_cust_ledger(cbo_1);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Customer From";
                    lbl_cbo_3.Text = "To";

                }
                else if (rpt_no == "A402")
                {
                    this.Text = "Balances from Customer";
                    grp_bydate.Text = "";
                    lbl_dt_frm.Text = "As Of";
                    dtp_to.Hide();
                    lbl_dt_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_for_cust_ledger(cbo_1);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Customer From";
                    lbl_cbo_3.Text = "To";
                }
                else if (rpt_no == "A403D")
                {
                    this.Text = "Customer Aging Report - Detailed";
                    grp_bydate.Text = "";
                    lbl_dt_frm.Text = "As Of";
                    dtp_to.Hide();
                    lbl_dt_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_for_cust_ledger(cbo_1);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Customer From";
                    lbl_cbo_3.Text = "To";

                }
                else if (rpt_no == "A403S")
                {
                    this.Text = "Customer Aging Report - Summary";
                    grp_bydate.Text = "";
                    lbl_dt_frm.Text = "As Of";
                    dtp_to.Hide();
                    lbl_dt_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_for_cust_ledger(cbo_1);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Customer From";
                    lbl_cbo_3.Text = "To";

                }
                else if (rpt_no == "A404")
                {
                    this.Text = "Balances to Supplier";
                    grp_bydate.Text = "";
                    lbl_dt_frm.Text = "As Of";
                    dtp_to.Hide();
                    lbl_dt_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_for_sup_ledger(cbo_1);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Supplier From";
                    lbl_cbo_3.Text = "To";

                }
                else if (rpt_no == "A405")
                {
                    this.Text = "Balances to Supplier";
                    grp_bydate.Text = "";
                    lbl_dt_frm.Text = "As Of";
                    dtp_to.Hide();
                    lbl_dt_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_for_sup_ledger(cbo_1);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Supplier From";
                    lbl_cbo_3.Text = "To";

                }
                else if (rpt_no == "A406D")
                {
                    this.Text = "Supplier Aging Report - Detailed";
                    grp_bydate.Text = "";
                    lbl_dt_frm.Text = "As Of";
                    dtp_to.Hide();
                    lbl_dt_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_for_sup_ledger(cbo_1);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Supplier From";
                    lbl_cbo_3.Text = "To";

                }
                else if (rpt_no == "A406S")
                {
                    this.Text = "Supplier Aging Report - Summary";
                    grp_bydate.Text = "";
                    lbl_dt_frm.Text = "As Of";
                    dtp_to.Hide();
                    lbl_dt_to.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_account_for_sup_ledger(cbo_1);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Supplier From";
                    lbl_cbo_3.Text = "To";

                }
                else if (rpt_no == "A501")
                {
                    this.Text = "Trial Balance";
                    //grp_bydate.Text = "Entry Dates";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Period From";
                    gc.load_openperiod(cbo_1);
                    lbl_cbo_2.Text = "To";
                    gc.load_openperiod(cbo_2);
                    lbl_cbo_3.Text = "View As";
                    cbo_3.Items.Add("Posted Entries Only");
                    cbo_3.Items.Add("Unposted Entries Only");
                    cbo_3.Items.Add("Posted and Unposted Entries");
                    cbo_3.SelectedIndex = 0;
                }
                else if (rpt_no == "A502a")
                {
                    this.Text = "Adjusted Balance Sheet";
                    //grp_bydate.Text = "Entry Dates";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Period From";
                    gc.load_openperiod(cbo_1);
                    lbl_cbo_2.Text = "To";
                    gc.load_openperiod(cbo_2);
                    lbl_cbo_3.Text = "View As";
                    cbo_3.Items.Add("Posted Entries Only");
                    cbo_3.Items.Add("Unposted Entries Only");
                    cbo_3.Items.Add("Posted and Unposted Entries");
                    cbo_3.SelectedIndex = 0;
                }
                else if (rpt_no == "A502l")
                {
                    this.Text = "Balance Sheet - List";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Period From";
                    gc.load_openperiod(cbo_1);
                    lbl_cbo_2.Text = "To";
                    gc.load_openperiod(cbo_2);
                    lbl_cbo_3.Text = "View As";
                    cbo_3.Items.Add("Posted Entries Only");
                    cbo_3.Items.Add("Unposted Entries Only");
                    cbo_3.Items.Add("Posted and Unposted Entries");
                    cbo_3.SelectedIndex = 0;
                }
                else if (rpt_no == "A502")
                {
                    this.Text = "Balance Summary Sheet";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Period From";
                    gc.load_openperiod(cbo_1);
                    lbl_cbo_2.Text = "To";
                    gc.load_openperiod(cbo_2);
                    lbl_cbo_3.Text = "View As";
                    cbo_3.Items.Add("Posted Entries Only");
                    cbo_3.Items.Add("Unposted Entries Only");
                    cbo_3.Items.Add("Posted and Unposted Entries");
                    cbo_3.SelectedIndex = 0;
                }
                else if (rpt_no == "A502m3")
                {
                    this.Text = "Comparative Balance Sheet";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Period From";
                    gc.load_openperiod(cbo_1);
                    lbl_cbo_2.Text = "To";
                    gc.load_openperiod(cbo_2);
                    cbo_2.Enabled = false;
                    //lbl_cbo_1.Hide();
                    //cbo_1.Hide();
                    //lbl_cbo_2.Hide();
                    //cbo_2.Hide();
                    lbl_cbo_3.Text = "View As";
                    cbo_3.Items.Add("Posted Entries Only");
                    cbo_3.Items.Add("Unposted Entries Only");
                    cbo_3.Items.Add("Posted and Unposted Entries");
                    cbo_3.SelectedIndex = 0;
                }
                else if (rpt_no == "A502y")
                {
                    this.Text = "Comparative Monthly Balance Sheet";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Financial Year";
                    cbo_1.DataSource = db.QueryBySQLCode("SELECT DISTINCT fy FROM rssys.x03 ORDER BY fy DESC");
                    cbo_1.DisplayMember = "fy";
                    cbo_1.ValueMember = "fy";
                    cbo_1.SelectedIndex = -1;
                    cbo_1.SelectedValue = DateTime.Now.ToString("yyyy");


                    //lbl_cbo_2.Hide();
                    //cbo_2.Hide();
                    lbl_cbo_2.Text = "View As";
                    cbo_2.Items.Add("Posted Entries Only");
                    cbo_2.Items.Add("Unposted Entries Only");
                    cbo_2.Items.Add("Posted and Unposted Entries");
                    cbo_2.SelectedIndex = 0;
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "A503l")
                {
                    this.Text = "Income Statement Report - List";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    //chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();
                    chk_1.Text = "Compare against budget";

                    lbl_cbo_1.Text = "Period From";
                    gc.load_openperiod(cbo_1);
                    lbl_cbo_2.Text = "To";
                    gc.load_openperiod(cbo_2);
                    lbl_cbo_3.Text = "View As";
                    cbo_3.Items.Add("Posted Entries Only");
                    cbo_3.Items.Add("Unposted Entries Only");
                    cbo_3.Items.Add("Posted and Unposted Entries");
                    cbo_3.SelectedIndex = 0;
                }
                else if (rpt_no == "A503")
                {
                    this.Text = "Income Statement Report";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Period From";
                    gc.load_openperiod(cbo_1);
                    lbl_cbo_2.Text = "To";
                    gc.load_openperiod(cbo_2);
                    lbl_cbo_3.Text = "View As";
                    cbo_3.Items.Add("Posted Entries Only");
                    cbo_3.Items.Add("Unposted Entries Only");
                    cbo_3.Items.Add("Posted and Unposted Entries");
                    cbo_3.SelectedIndex = 0;
                }
                else if (rpt_no == "A503m3")
                {
                    this.Text = "Comparative Income Statement";
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Period From";
                    gc.load_openperiod(cbo_1);
                    lbl_cbo_2.Text = "To";
                    gc.load_openperiod(cbo_2); 
                    cbo_2.Enabled = false;

                    //lbl_cbo_1.Hide();
                    //cbo_1.Hide();
                    //lbl_cbo_2.Hide();
                    //cbo_2.Hide();
                    lbl_cbo_3.Text = "View As";
                    cbo_3.Items.Add("Posted Entries Only");
                    cbo_3.Items.Add("Unposted Entries Only");
                    cbo_3.Items.Add("Posted and Unposted Entries");
                    cbo_3.SelectedIndex = 0;
                }
                else if (rpt_no == "A503y" || rpt_no == "A503cc")
                {
                    this.Text = "Comparative Monthly Income Statement" + (rpt_no == "A503cc" ? " per Cost Center" : "");
                    grp_options.Text = "Select Period";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_1.Text = "Financial Year";
                    cbo_1.DataSource = db.QueryBySQLCode("SELECT DISTINCT fy FROM rssys.x03 ORDER BY fy DESC");
                    cbo_1.DisplayMember = "fy";
                    cbo_1.ValueMember = "fy";
                    cbo_1.SelectedIndex = -1;
                    cbo_1.SelectedValue = DateTime.Now.ToString("yyyy");

                    //lbl_cbo_2.Hide();
                    //cbo_2.Hide();
                    lbl_cbo_2.Text = "View As";
                    cbo_2.Items.Add("Posted Entries Only");
                    cbo_2.Items.Add("Unposted Entries Only");
                    cbo_2.Items.Add("Posted and Unposted Entries");
                    cbo_2.SelectedIndex = 0;
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "A601")
                {
                    this.Text = "Daily Field Collection Report";
                    grp_bydate.Text = "Entry Dates";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    cbo_2.Hide();
                    cbo_3.Hide();

                    lbl_cbo_2.Hide();
                    lbl_cbo_3.Hide();

                    lbl_cbo_1.Text = "View As";
                    cbo_1.Items.Add("Posted Entries Only");
                    cbo_1.Items.Add("Unposted Entries Only");
                    cbo_1.Items.Add("Posted and Unposted Entries");
                    cbo_1.SelectedIndex = 0;

                    /*
                    gc.load_account_title(cbo_1);
                    gc.load_journal_code_asc(cbo_2);
                    gc.load_journal_code_asc(cbo_3);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Journal From";
                    lbl_cbo_3.Text = "Journal To";*/
                }
                else if (rpt_no == "A701")
                {
                    this.Text = "SOA Summary Report";
                    grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    cbo_2.Hide();
                    cbo_3.Hide();

                    lbl_cbo_2.Hide();
                    lbl_cbo_3.Hide();

                    lbl_cbo_1.Text = "SOA Period";
                    gc.load_soa_period(cbo_1);

                    try { cbo_1.SelectedIndex = 0; }
                    catch { }
                    /*
                    gc.load_account_title(cbo_1);
                    gc.load_journal_code_asc(cbo_2);
                    gc.load_journal_code_asc(cbo_3);

                    lbl_cbo_1.Text = "Account ID";
                    lbl_cbo_2.Text = "Journal From";
                    lbl_cbo_3.Text = "Journal To";*/
                }
                //Inventory
                else if (rpt_no == "I011")
                {
                    this.Text = "Purchase Request Reports By Date";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Hide();

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
                else if (rpt_no == "I012")
                {
                    this.Text = "Purchase Request Reports By PR Number";
                    grp_options.Text = "Purchase Request Number";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_pr_code(cbo_1);
                    gc.load_pr_code(cbo_2);

                    lbl_cbo_1.Text = "From";
                    lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "I013")
                {
                    this.Text = "Purchase Request Reports By Item";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Text = "Items";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_item(cbo_1);
                    lbl_cbo_1.Text = "Items";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "I014")
                {
                    this.Text = "Purchase Request Reports By Sub Cost Center";
                    grp_options.Text = "Select Options";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_costcenter(cbo_1);

                    lbl_cbo_1.Text = "Cost Center";
                    lbl_cbo_2.Text = "From";
                    lbl_cbo_3.Text = "To";
                }
                //Purchase Order
                else if (rpt_no == "I021")
                {
                    this.Text = "Purchase Order Report By Date";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Hide();

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
                else if (rpt_no == "I022")
                {
                    this.Text = "Purchase Order Report By PO Number";
                    grp_options.Text = "Purchase Order Number";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_po_number(cbo_1);
                    gc.load_po_number(cbo_2);
                    lbl_cbo_1.Text = "From";
                    lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "I023")
                {
                    this.Text = "Purchase Order Report By Supplier";
                    grp_options.Text = "Select Supplier";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_supplier(cbo_1);

                    lbl_cbo_1.Text = "Supplier";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "I024")
                {
                    this.Text = "Purchase Order Report By Item";
                    grp_options.Text = "Select Items";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_item(cbo_1);
                    lbl_cbo_1.Text = "Item";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                //Receiving Purchase
                else if (rpt_no == "I031")
                {
                    this.Text = "Receiving Purchase Report By Date";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Hide();

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
                else if (rpt_no == "I032")
                {
                    this.Text = "Purchase Report By RR Number";
                    grp_options.Text = "RR Number";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_rr_number(cbo_1);
                    gc.load_rr_number(cbo_2);
                    lbl_cbo_1.Text = "From";
                    lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "I033")
                {
                    this.Text = "Receiving Purchase Report By Item";
                    grp_options.Text = "Select Item";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_item(cbo_1);
                    lbl_cbo_1.Text = "Item";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                //Purchase Order
                else if (rpt_no == "I041")
                {
                    this.Text = "Direct Purchase Report By Date";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Hide();

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
                else if (rpt_no == "I042")
                {
                    this.Text = "Direct Purchase Report By Purchase Number";
                    grp_options.Text = "Direct Purchase Number";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_dr_number(cbo_1);
                    gc.load_dr_number(cbo_2);
                    lbl_cbo_1.Text = "From";
                    lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "I044")
                {
                    this.Text = "Direct Purchase Report By Item";
                    grp_options.Text = "Items";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_item(cbo_1);
                    lbl_cbo_1.Text = "Item";
                    //gc.load_item(cbo_2);
                    //lbl_cbo_1.Text = "From";
                    //lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                }
                else if (rpt_no == "I043")
                {
                    this.Text = "Direct Purchase Report By Supplier";
                    grp_options.Text = "Select Options";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_supplier(cbo_1);
                    lbl_cbo_1.Text = "Supplier";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                }
                //Stock Adjustment
                else if (rpt_no == "I051")
                {
                    this.Text = "Stock Issuance Report By Date";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Hide();

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
                else if (rpt_no == "I052")
                {
                    this.Text = "Stock Issuance Report By Issuance Number";
                    grp_options.Text = "Issuance Number";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_si_number(cbo_1);
                    gc.load_si_number(cbo_2);
                    lbl_cbo_1.Text = "From";
                    lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "I053")
                {
                    this.Text = "Stock Issuance Report By Item";
                    grp_options.Text = "Items";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_item(cbo_1);
                    lbl_cbo_1.Text = "Item";
                    //gc.load_item(cbo_2);
                    //lbl_cbo_1.Text = "From";
                    //lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                }
                else if (rpt_no == "I054")
                {
                    this.Text = "Stock Issuance Report By Cost Center";
                    grp_options.Text = "Select Options";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_costcenter(cbo_1);

                    lbl_cbo_1.Text = "Cost Center";
                    lbl_cbo_2.Text = "From";
                    lbl_cbo_3.Text = "To";
                }
                //Stock Transfer
                else if (rpt_no == "I061")
                {
                    this.Text = "Stock Transfer Report By Date";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Hide();

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
                else if (rpt_no == "I062")
                {
                    this.Text = "Stock Transfer Report By Issuance Number";
                    grp_options.Text = "Transfer Number";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_tra_number(cbo_1);
                    gc.load_tra_number(cbo_2);
                    lbl_cbo_1.Text = "From";
                    lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "I063")
                {
                    this.Text = "Stock Transfer Report By Item";
                    grp_options.Text = "Items";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_item(cbo_1);
                    lbl_cbo_1.Text = "Item";
                    //gc.load_item(cbo_2);
                    //lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                }
                //Stock Transfer
                else if (rpt_no == "I071")
                {
                    this.Text = "Stock Adjustment Report By Date";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Hide();

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
                else if (rpt_no == "I072")
                {
                    this.Text = "Stock Adjustment Report By Issuance Number";
                    grp_options.Text = "Adjustment Number";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_adj_number(cbo_1);
                    gc.load_adj_number(cbo_2);
                    lbl_cbo_1.Text = "From";
                    lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "I073")
                {
                    this.Text = "Stock Adjustment Report By Item";
                    grp_options.Text = "Items";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_item(cbo_1);
                    lbl_cbo_1.Text = "Item";
                    //gc.load_item(cbo_2);
                    //lbl_cbo_1.Text = "From";
                    //lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                }
                //Inventory Summary By Date
                else if (rpt_no == "I01")
                {
                    this.Text = "Inventory Summary By Date";
                    grp_options.Text = "Select Options";
                    grp_options.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_category(cbo_1);
                    gc.load_whouse(cbo_2);

                    lbl_cbo_1.Text = "Item Group";
                    lbl_cbo_2.Text = "Warehouse";
                    //lbl_cbo_3.Text = "View Unit";
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                //Inventory Valuation
                else if (rpt_no == "I02")
                {
                    this.Text = "Inventory Valuation";
                    grp_options.Text = "Select Options";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_category(cbo_1);
                    gc.load_whouse(cbo_2);

                    lbl_cbo_1.Text = "Item Group";
                    lbl_cbo_2.Text = "Warehouse";
                    //lbl_cbo_3.Text = "Unit By";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                //Reorder Level Report
                else if (rpt_no == "I03")
                {
                    this.Text = "Reorder Level Report";
                    grp_options.Text = "Select Options";
                    //grp_bydate.Hide();

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_category(cbo_1);

                    lbl_cbo_1.Text = "Item Group";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                //Project Costing
                else if (rpt_no == "P101")
                {
                    this.Text = "Project Costing Report By Date";
                    grp_bydate.Text = "Transaction Dates";
                    grp_options.Hide();

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
                else if (rpt_no == "P102")
                {
                    this.Text = "Project Costing Report By Number";
                    grp_options.Text = "Number";
                    grp_bydate.Hide();
                    grp_bybranch.Dock = System.Windows.Forms.DockStyle.Top;

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_si_number(cbo_1);
                    gc.load_si_number(cbo_2);
                    lbl_cbo_1.Text = "From";
                    lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "P103")
                {
                    this.Text = "Project Costing Report By Item";
                    grp_options.Text = "Items";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_item(cbo_1);
                    //gc.load_item(cbo_2);
                    lbl_cbo_1.Text = "Item";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "P104")
                {
                    this.Text = "Project Costing Report By Item Group";
                    grp_options.Text = "Item Groups";
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_category(cbo_1);
                    gc.load_category(cbo_2);
                    lbl_cbo_1.Text = "From";
                    lbl_cbo_2.Text = "To";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "P105")
                {
                    this.Text = "Project Costing Report By Project Name";
                    grp_options.Text = "Project Names";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();
                    gc.load_costcenter(cbo_1);
                    // gc.load_subcostcenter(cbo_1, "001");
                    // gc.load_subcostcenter(cbo_2, "001");
                    lbl_cbo_1.Text = "Type";
                    lbl_cbo_2.Text = "From";
                    lbl_cbo_3.Text = "To";
                }
                else if (rpt_no == "P201")
                {
                    this.Text = "Summary of Project Cost per Item";
                    grp_options.Text = "Select Options";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_costcenter(cbo_1);

                    lbl_cbo_1.Text = "Cost Center Name";
                    lbl_cbo_2.Text = "Project Name";
                    //lbl_cbo_3.Text = "Project Name To";
                }
                //Sales
                else if (rpt_no == "S011")
                {
                    this.Text = "Outlet Sales Report";
                    grp_options.Text = "Select Options";
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_branch(cbo_1);
                    lbl_cbo_1.Text = "Branch";

                    gc.load_outlet(cbo_2);
                    lbl_cbo_2.Text = "Outlet";

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "S011B")
                {
                    this.Text = "Cashier's Report";
                    grp_options.Text = "Select Options";
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_outlet(cbo_1);
                    lbl_cbo_1.Text = "Outlet";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "S012")
                {
                    this.Text = "Outlet Sales Report By Item";
                    grp_options.Text = "Select Options";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_outlet(cbo_1);
                    gc.load_category(cbo_2);
                    gc.load_category(cbo_3);
                    lbl_cbo_1.Text = "Outlet";
                    lbl_cbo_2.Text = "Item Group From";
                    lbl_cbo_3.Text = "Item Group To";
                }
                else if (rpt_no == "S013")
                {
                    this.Text = "Sales Report By Item";
                    grp_options.Text = "Select Options";
                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_outlet(cbo_1);
                    lbl_cbo_1.Text = "Outlet";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "S014")
                {
                    this.Text = "Sales Report By Staff";
                    grp_options.Text = "Select Options";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_outlet(cbo_1);
                    gc.load_salesclerk(cbo_2);
                    gc.load_salesclerk(cbo_3);

                    lbl_cbo_1.Text = "Outlet";
                    lbl_cbo_2.Text = "Staff From";
                    lbl_cbo_3.Text = "Staff To";
                }
                else if (rpt_no == "S015")
                {
                    this.Text = "Sales Report Summary";
                    grp_options.Text = "Select Options";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_outlet(cbo_1); ;

                    lbl_cbo_1.Text = "Outlet";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "S016")
                {
                    this.Text = "Summary Of Item Sold & Material Report";
                    grp_options.Text = "Select Options";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    gc.load_outlet(cbo_1); ;

                    lbl_cbo_1.Text = "Outlet";

                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }

                // SOA
                else if (rpt_no == "AS01P")
                {
                    this.Text = "SOA List by Period";

                    grp_bybranch.Dock = DockStyle.Fill;
                    grp_options.Text = "Select Options";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    cbo_1.DataSource = db.QueryBySQLCode("SELECT soa_desc, to_Char(soafrom,'yyyy/MM/dd')||'-'||to_Char(soato,'yyyy/MM/dd') AS soa_dt FROM rssys.soa_period WHERE COALESCE(closed,'')<>'Y' ORDER BY soafrom desc,soato desc");
                    cbo_1.DisplayMember = "soa_desc";
                    cbo_1.ValueMember = "soa_dt";
                    try { cbo_1.SelectedIndex = 0; }
                    catch { }

                    lbl_cbo_1.Text = "SOA Period";
                    gc.load_customer(cbo_2);
                    lbl_cbo_2.Text = "Specific Tenant";
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
                else if (rpt_no == "AS01D")
                {
                    this.Text = "SOA List by Date";

                    grp_options.Text = "Select Options";

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    dtp_frm.Value = DateTime.Parse(dtp_frm.Value.ToString("yyyy-MM-01"));

                    gc.load_customer(cbo_1);
                    lbl_cbo_1.Text = "Specific Tenant";
                    lbl_cbo_2.Hide();
                    cbo_2.Hide();
                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }

                

                if (hasBranch)
                {
                    gc.load_branch(cbo_4);
                    cbo_4.SelectedValue = GlobalClass.branch;
                    grp_bybranch.Text = "Select Branch";
                    grp_bybranch.Show();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            this.Text = this.Text + " | " + rpt_no;
            isReady = true;
        }
        public RPT_RES_entry(String rnum, String cbo_val1, String cbo_val2, String cbo_val3, String dt_frm, String dt_to)
            : this(rnum)
        {
            cbo_1.SelectedValue = cbo_val1;
            if (!String.IsNullOrEmpty(dt_to)) dtp_to.Value = DateTime.Parse(dt_to);
            if (!String.IsNullOrEmpty(dt_frm)) dtp_frm.Value = DateTime.Parse(dt_frm);
            cbo_2.SelectedValue = cbo_val2;
            cbo_3.SelectedValue = cbo_val3;
        }
        private void RPT_RES_entry_Load(object sender, EventArgs e)
        {
            pbar_panl_hide();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            this.print();
        }
        public void print()
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

            cbo_4.Text = "ALL BRANCH"; /*GlobalClass.branch*/

        }

        private int get_cbo_index(ComboBox cbo)
        {
            int i = -1;

            cbo.Invoke(new Action(() =>
            {
                try { i = cbo.SelectedIndex; }
                catch { }
            }));

            return i;
        }

        private String get_cbo_value(ComboBox cbo)
        {
            String value = "";
            try
            {
                cbo.Invoke(new Action(() =>
                {
                    try { value = cbo.SelectedValue.ToString(); }
                    catch { }
                }));
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
            return value;
        }

        private String get_cbo_text(ComboBox cbo)
        {
            String txt = "";

            cbo.Invoke(new Action(() =>
            {
                try { txt = cbo.Text.ToString(); }
                catch { }
            }));

            return txt;
        }
        private void set_cbo_droppeddown(ComboBox cbo, Boolean isdrop)
        {
            cbo.Invoke(new Action(() =>
            {
                cbo.DroppedDown = isdrop;
            }));
        }
        private void reset()
        {
            reset_pbar();
            input_enable(true);
            pbar_panl_hide();
        }
        private Boolean ischkbox_checked(CheckBox chk)
        {
            Boolean ischk = false;

            chk.Invoke(new Action(() =>
            {
                try { ischk = chk.Checked; }
                catch { }
            }));

            return ischk;
        }
        private void chkbox_check(CheckBox chk, Boolean ischk)
        {
            chk.Invoke(new Action(() =>
            {
                try { chk.Checked = ischk; }
                catch { }
            }));
        }
        private String chkbox_text(CheckBox chk)
        {
            String txt = "";
            chk.Invoke(new Action(() =>
            {
                try { txt = chk.Text.ToString(); }
                catch { }
            }));
            return txt;
        }
        private void add_fieldparam(String col, String val)
        {
            try
            {
                crParameterDiscreteValue.Value = val;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions[col];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();
                inc_pbar(10);
            }
            catch (Exception error) { MessageBox.Show(error.Message); }
        }

        private void disp_reportviewer(ReportDocument myReportDocument)
        {
            crptviewer.Invoke(new Action(() =>
            {
                try { crptviewer.ReportSource = myReportDocument; }
                catch { }
            }));

            crptviewer.Invoke(new Action(() =>
            {
                crptviewer.Refresh();
            }));
        }

        private void clr_param()
        {
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
        }

        private void input_enable(Boolean bol)
        {
            cbo_1.Invoke(new Action(() =>
            {
                try { cbo_1.Enabled = bol; }
                catch { }
            }));

            cbo_2.Invoke(new Action(() =>
            {
                try { cbo_2.Enabled = bol; }
                catch { }
            }));

            cbo_3.Invoke(new Action(() =>
            {
                try { cbo_3.Enabled = bol; }
                catch { }
            }));

            dtp_frm.Invoke(new Action(() =>
            {
                try { dtp_frm.Enabled = bol; }
                catch { }
            }));

            dtp_to.Invoke(new Action(() =>
            {
                try { dtp_to.Enabled = bol; }
                catch { }
            }));

            btn_clear.Invoke(new Action(() =>
            {
                try { btn_clear.Enabled = bol; }
                catch { }
            }));

            btn_submit.Invoke(new Action(() =>
            {
                try { btn_submit.Enabled = bol; }
                catch { }
            }));

            cbo_4.Invoke(new Action(() =>
            {
                try { cbo_4.Enabled = bol; }
                catch { }
            }));

        }

        private void inc_pbar(int i)
        {
            try
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value += i;
                }));
            }
            catch (Exception) { reset_pbar(); }
        }

        private void reset_pbar()
        {
            progressBar1.Invoke(new Action(() =>
            {
                try { progressBar1.Value = 0; }
                catch { }
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
            String dtpicker_frm = dtp_frm.Value.ToString("MM-dd-yyyy");
            String dtpicker_to = dtp_to.Value.ToString("MM-dd-yyyy");
            String branch_cbo = "";
            String WHERE = "";

            _schema = db.get_schema();
            comp_name = db.get_m99comp_name();
            comp_addr = db.get_m99comp_addr();

            pbar_panl_show();

            try
            {
                if (rpt_no == "M001")
                {
                    String cat = "";
                    String brd = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        cat = get_cbo_value(cbo_1);
                    }

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        brd = get_cbo_value(cbo_2);
                    }

                    if (String.IsNullOrEmpty(cat) == false)
                    {
                        WHERE = WHERE + "item_grp='" + cat + "'";
                    }
                    if (String.IsNullOrEmpty(brd) == false)
                    {
                        if (String.IsNullOrEmpty(WHERE) == false)
                        {
                            WHERE = WHERE + " AND ";
                        }

                        WHERE = WHERE + "brd_code='" + brd + "'";
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_md + "m_items.rpt");

                    DataTable dt = null;

                    dt = db.QueryOnTableWithParams("items", "*", WHERE, " ORDER BY item_desc ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A001")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_acctg + "a_dailycollection.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT c.or_code, c.debt_code, c.debt_name, to_char(c.trnx_date, 'MM/dd/yyyy') AS trnx_date, c.or_type, c.or_ref, c.coll_code, c.soa, m10.mp_desc AS type, cl.amount  " + " FROM " + db.get_schema() + ".colhdr c LEFT JOIN " + db.get_schema() + ".collne2 cl ON c.or_code=cl.or_code LEFT JOIN " + db.get_schema() + ".m10 ON m10.mp_code=cl.type " + " WHERE c.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY c.trnx_date");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    //user
                    crParameterDiscreteValue.Value = GlobalClass.user_fullname;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["user_fullname"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    clr_param();
                    inc_pbar(10);

                    //dtfrm
                    crParameterDiscreteValue.Value = dtpicker_frm + " to " + dtpicker_to;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["t_date"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;


                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }

                else if (rpt_no == "A101")
                {
                    //this.Text = "Account Activity By ID";
                    inc_pbar(10);
                    String at_code = get_cbo_value(cbo_1);
                    String j_code_frm = get_cbo_value(cbo_2);
                    String j_code_to = get_cbo_value(cbo_3);
                    String fy = dtp_frm.Value.ToString("yyyy"); //"2016";
                    String entries = "";

                    int _fy = Convert.ToInt32(dtp_frm.Value.ToString("yyyy"));
                    if (Convert.ToInt32(dtp_frm.Value.ToString("MM")) > 10) _fy++;
                    fy = _fy.ToString("0");

                    DataTable dt_begBal = db.QueryBySQLCode("SELECT * FROM rssys.x03 WHERE mo='0' AND fy='" + fy + "'");
                    String begBal_dt = dtp_frm.Value.ToString("yyyy-10-31");
                    try { begBal_dt = DateTime.Parse(dt_begBal.Rows[0]["to"].ToString()).ToString("yyyy-MM-dd"); }
                    catch { }


                    String acct = "All";
                    String jrnl = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND t4.j_code >= '" + get_cbo_value(cbo_2) + "'";
                        jrnl = j_code_frm + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE = " AND t4.j_code <= '" + get_cbo_value(cbo_3) + "'";
                        jrnl = "First To " + j_code_to;
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE = " AND (t4.j_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        jrnl = j_code_frm + " To " + j_code_to;
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            jrnl = j_code_frm;
                        }
                    }

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND t4.at_code='" + at_code + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t4.branch='" + get_cbo_value(cbo_4) + "'";
                    }


                    String afterfrm = dtp_frm.Value.AddDays(-1).ToString("yyyy-MM-dd");
                    String dtQry = "";
                    String pQry = "";
                    String trQry = "";
                    if (ischkbox_checked(chk_1))
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                        entries = "(" + chkbox_text(chk_1) + ")";
                    }
                    if (ischkbox_checked(chk_2))
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                        entries = "(" + chkbox_text(chk_2) + ")";
                    }
                    if (ischkbox_checked(chk_1) && ischkbox_checked(chk_2))
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                        entries = "(Posted and Unposted Entries)";
                    }

                    String dt_frm = "", dt_to = "";
                    {
                        int frm_fy, to_fy, frm_mo, to_mo;
                        frm_fy = Convert.ToInt32(dtp_frm.Value.ToString("yyyy"));
                        to_fy = Convert.ToInt32(dtp_to.Value.ToString("yyyy"));
                        frm_mo = Convert.ToInt32(dtp_frm.Value.ToString("MM"));
                        to_mo = Convert.ToInt32(dtp_to.Value.ToString("MM"));

                        if (frm_mo > 10)
                        {
                            frm_mo = frm_mo % 10;
                            frm_fy++;
                        }
                        else
                        {
                            frm_mo += 2;
                        }

                        if (to_mo > 10)
                        {
                            to_mo = to_mo % 10;
                            to_fy++;
                        }
                        else
                        {
                            to_mo += 2;
                        }
                        dt_frm = frm_fy.ToString("0") + "-" + frm_mo.ToString("0");
                        dt_to = to_fy.ToString("0") +"-" + to_mo.ToString("0");
                    }

                    //removed by Reancy 06 30 2018
                    //pQry = "(SELECT * FROM " + trQry + " t4 WHERE t4.t_desc not like ('%CANCELLED-%') AND COALESCE(t4.cancel,'')<>'Y' " + WHERE + " AND (t4.t_date <= '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' AND t4.t_date>='" + begBal_dt + "'))";
                    pQry = "(SELECT * FROM " + trQry + " t4 WHERE t4.t_desc not like ('%CANCELLED-%') AND COALESCE(t4.cancel,'')<>'Y' " + WHERE + ")";


                    //dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from,sx3._to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + dt_frm + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + dt_to + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + dt_frm + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + dt_to + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";
                    //JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)  AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo)
                    //pQry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)  AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo) WHERE t4.t_desc not like ('%CANCELLED-%') AND COALESCE(t4.cancel,'')<>'Y' " + WHERE + " AND (t4.t_date <= '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' AND t4.t_date>='" + begBal_dt + "'))";


                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "101_acct_activity_by_id.rpt");
                    //remove by: Reancy 06 30 2018
                    //DataTable dt = db.QueryBySQLCode("SELECT DISTINCT j_num, j_code, t_date, t_date2, t_desc,  SUM(CASE WHEN debit>credit THEN debit-credit ELSE 0 END) AS debit, SUM(CASE WHEN debit<credit THEN credit-debit ELSE 0 END) AS credit FROM (SELECT DISTINCT j_num, j_code, to_char(t_date, 'MM/dd/yyyy') AS t_date, t_date AS t_date2, t_desc,  SUM(CASE WHEN debit>credit THEN debit-credit ELSE 0 END) AS debit, SUM(CASE WHEN debit<credit THEN credit-debit ELSE 0 END) AS credit  FROM(SELECT CASE WHEN t4.t_date<='" + afterfrm + "' THEN '' ELSE t4.j_code END j_code, CASE WHEN t4.t_date<='" + afterfrm + "' THEN '' ELSE t4.j_num END j_num, CASE WHEN t4.t_date<='" + afterfrm + "' THEN '" + afterfrm + "'::date ELSE t4.t_date END t_date, CASE WHEN t4.t_date<='" + afterfrm + "' THEN 'Balance Forwarded' ELSE t4.t_desc END t_desc, t4.credit,t4.debit FROM " + pQry + " t4 LEFT JOIN rssys.tr03 t3 ON (t4.j_code=t3.j_code AND t4.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code UNION ALL SELECT ''::text, ''::text, '" + afterfrm + "'::date, 'Balance Forwarded'::text, 0, 0) res WHERE debit<>0 OR credit<>0 OR j_code='' GROUP BY j_num, j_code, t_date, t_date2, t_desc) res GROUP BY j_num, j_code, t_date, t_date2, t_desc HAVING SUM(CASE WHEN debit<credit THEN credit-debit ELSE 0 END)<>0 OR SUM(CASE WHEN debit>credit THEN debit-credit ELSE 0 END)<>0 OR j_code='' ORDER BY  t_date2, j_code, j_num");
                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT j_num, j_code, t_date, t_date2, t_desc, (CASE WHEN t_desc='Balance Forwarded' THEN SUM(CASE WHEN bdebit>bcredit THEN bdebit-bcredit ELSE 0 END) ELSE SUM(CASE WHEN debit>credit THEN debit-credit ELSE 0 END) END) AS debit, SUM(CASE WHEN debit<credit THEN credit-debit ELSE 0 END) AS credit FROM (SELECT DISTINCT j_num, j_code, to_char(t_date, 'MM/dd/yyyy') AS t_date, t_date AS t_date2, t_desc,  SUM(CASE WHEN debit>credit THEN debit-credit ELSE 0 END) AS debit, SUM(CASE WHEN debit<credit THEN credit-debit ELSE 0 END) AS credit, SUM(CASE WHEN bdebit>bcredit THEN bdebit-bcredit ELSE 0 END) AS bdebit, SUM(CASE WHEN bdebit<bcredit THEN bcredit-bdebit ELSE 0 END) AS bcredit  FROM (SELECT CASE WHEN t4.t_date<='" + afterfrm + "' THEN '' ELSE t4.j_code END j_code, CASE WHEN t4.t_date<='" + afterfrm + "' THEN '' ELSE t4.j_num END j_num, CASE WHEN t4.t_date<='" + afterfrm + "' THEN '" + afterfrm + "'::date ELSE t4.t_date END t_date, CASE WHEN t4.t_date<='" + afterfrm + "' THEN 'Balance Forwarded' ELSE t4.t_desc END t_desc, CASE WHEN t4.t_date<='" + afterfrm + "' THEN 0.00 ELSE t4.credit END credit,CASE WHEN t4.t_date<='" + afterfrm + "' THEN 0.00 ELSE t4.debit END debit, CASE WHEN t4.t_date<='" + afterfrm + "' THEN t4.credit ELSE 0.00 END bcredit,CASE WHEN t4.t_date<='" + afterfrm + "' THEN t4.debit ELSE 0.00 END bdebit FROM" + pQry + " t4 LEFT JOIN rssys.tr03 t3 ON (t4.j_code=t3.j_code AND t4.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code UNION ALL SELECT ''::text, ''::text, '" + afterfrm + "'::date, 'Balance Forwarded'::text, 0, 0, 0, 0) res WHERE debit<>0 OR credit<>0 OR j_code='' GROUP BY j_num, j_code, t_date, t_date2, t_desc) res GROUP BY j_num, j_code, t_date, t_date2, t_desc HAVING SUM(CASE WHEN debit<credit THEN credit-debit ELSE 0 END)<>0 OR SUM(CASE WHEN debit>credit THEN debit-credit ELSE 0 END)<>0 OR j_code='' ORDER BY  t_date2, j_code, j_num");
                    rtxt_test.Text = "SELECT DISTINCT j_num, j_code, t_date, t_date2, t_desc, (CASE WHEN t_desc='Balance Forwarded' THEN SUM(CASE WHEN bdebit>bcredit THEN bdebit-bcredit ELSE 0 END) ELSE SUM(CASE WHEN debit>credit THEN debit-credit ELSE 0 END) END) AS debit, SUM(CASE WHEN debit<credit THEN credit-debit ELSE 0 END) AS credit FROM (SELECT DISTINCT j_num, j_code, to_char(t_date, 'MM/dd/yyyy') AS t_date, t_date AS t_date2, t_desc,  SUM(CASE WHEN debit>credit THEN debit-credit ELSE 0 END) AS debit, SUM(CASE WHEN debit<credit THEN credit-debit ELSE 0 END) AS credit, SUM(CASE WHEN bdebit>bcredit THEN bdebit-bcredit ELSE 0 END) AS bdebit, SUM(CASE WHEN bdebit<bcredit THEN bcredit-bdebit ELSE 0 END) AS bcredit  FROM (SELECT CASE WHEN t4.t_date<='" + afterfrm + "' THEN '' ELSE t4.j_code END j_code, CASE WHEN t4.t_date<='" + afterfrm + "' THEN '' ELSE t4.j_num END j_num, CASE WHEN t4.t_date<='" + afterfrm + "' THEN '" + afterfrm + "'::date ELSE t4.t_date END t_date, CASE WHEN t4.t_date<='" + afterfrm + "' THEN 'Balance Forwarded' ELSE t4.t_desc END t_desc, CASE WHEN t4.t_date<='" + afterfrm + "' THEN 0.00 ELSE t4.credit END credit,CASE WHEN t4.t_date<='" + afterfrm + "' THEN 0.00 ELSE t4.debit END debit, CASE WHEN t4.t_date<='" + afterfrm + "' THEN t4.credit ELSE 0.00 END bcredit,CASE WHEN t4.t_date<='" + afterfrm + "' THEN t4.debit ELSE 0.00 END bdebit FROM" + pQry + " t4 LEFT JOIN rssys.tr03 t3 ON (t4.j_code=t3.j_code AND t4.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code UNION ALL SELECT ''::text, ''::text, '" + afterfrm + "'::date, 'Balance Forwarded'::text, 0, 0, 0, 0) res WHERE debit<>0 OR credit<>0 OR j_code='' GROUP BY j_num, j_code, t_date, t_date2, t_desc) res GROUP BY j_num, j_code, t_date, t_date2, t_desc HAVING SUM(CASE WHEN debit<credit THEN credit-debit ELSE 0 END)<>0 OR SUM(CASE WHEN debit>credit THEN debit-credit ELSE 0 END)<>0 OR j_code='' ORDER BY  t_date2, j_code, j_num";

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    add_fieldparam("t_date", dtp_frm.Value.ToString("yyyy-MM-dd") + " TO " + dtp_to.Value.ToString("yyyy-MM-dd"));
                    add_fieldparam("fy", fy + entries);
                    add_fieldparam("jrnl", jrnl);
                    add_fieldparam("acct_id", acct);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A102")
                {
                    //this.Text = "List of Unposted Entries";
                    if (get_cbo_index(cbo_2) == -1)
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_3, true);
                        reset();
                        return;
                    }

                    String fy = ((String)get_cbo_value(cbo_2).Split('-').GetValue(0)).Trim();

                    String jrnl = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t1.j_code='" + get_cbo_value(cbo_1) + "'";
                        jrnl = get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    String dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_3) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to FROM rssys.x03 x3) sx3)";

                    String trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num) WHERE 1=1 " + WHERE + ")";

                    String pQry = "(SELECT t4.* FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo) WHERE t4.t_desc not like ('%CANCELLED-%'))";


                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "102_list_of_unposted_entries.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT t4.j_num, t4.j_code, t4.mo, to_char(t4.t_date, 'MM/dd/yyyy') AS t_date, t4.payee, t4.t_desc, t4.ck_num, CASE WHEN COALESCE(t4.ck_num,'')<>'' THEN to_char(t4.ck_date, 'MM/dd/yyyy') ELSE '' END AS ck_date, t3.j_memo, t4.at_code, m4.at_desc, t4.sl_code, t4.sl_name, t4.debit, t4.credit, t4.invoice, t4.cc_code FROM " + pQry + " t4 LEFT JOIN " + db.get_schema() + ".tr03 t3 ON (t4.j_code=t3.j_code AND t4.j_num=t3.j_num) LEFT JOIN " + db.get_schema() + ".m04 m4 ON m4.at_code=t4.at_code ORDER BY t4.j_num ASC, t4.j_code;");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    add_fieldparam("t_date", (get_cbo_text(cbo_2).Split('-')[1] ?? "") + "-" + (get_cbo_text(cbo_3).Split('-')[1] ?? ""));
                    add_fieldparam("fy", fy);
                    add_fieldparam("jrnl", jrnl);

                    disp_reportviewer(myReportDocument);
                }

                else if (rpt_no == "A103")
                {
                    //this.Text = "Cash Status Report";
                    inc_pbar(10);
                    String at_code_frm = get_cbo_value(cbo_1);
                    String at_code_to = get_cbo_value(cbo_2);
                    String fy = dtp_frm.Value.ToString("yyyy"); //"2016";
                    String entries = "";

                    String acct = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t4.at_code >= '" + get_cbo_value(cbo_1) + "'";
                        acct = get_cbo_text(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND t4.at_code <= '" + get_cbo_value(cbo_2) + "'";
                        acct = "First To " + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND (t4.at_code BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        acct = get_cbo_text(cbo_1) + " To " + get_cbo_text(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            acct = get_cbo_text(cbo_1);
                        }
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t4.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    String pQry = "";
                    String trQry = "";
                    if (ischkbox_checked(chk_1))
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel, isreleased  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                        entries = "(" + chkbox_text(chk_1) + ")";
                    }
                    if (ischkbox_checked(chk_2))
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel, isreleased FROM rssys.tr04)";
                        entries = "(" + chkbox_text(chk_2) + ")";
                    }
                    if (ischkbox_checked(chk_1) && ischkbox_checked(chk_2))
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel, isreleased FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel, isreleased  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                        entries = "(Posted and Unposted Entries)";
                    }


                    trQry = trQry + " t4 JOIN (SELECT ('" + dtp_frm.Value.ToString("yyyy-MM-dd") + "')::date AS dt_frm, ('" + dtp_to.Value.ToString("yyyy-MM-dd") + "')::date AS dt_to ) dt ON (t_date<=dt_to) WHERE t4.t_desc not like ('%CANCELLED-%') AND fy='" + get_cbo_value(cbo_3) + "'  " + WHERE + "";

                    pQry = "((SELECT *, 'dr' AS dr_cr, CASE WHEN credit<0 THEN debit+ABS(credit) ELSE debit END AS amount FROM " + trQry + " AND t4.debit>0) UNION ALL (SELECT *, 'cr' AS dr_cr, CASE WHEN debit<0 THEN credit+ABS(debit) ELSE credit END AS amount FROM " + trQry + " AND t4.credit>0))";



                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "103_cash_reposition.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT j_code, j_num, t_desc, t_date, payee, ck_num, at_desc, seq_desc, or_lne, SUM(price) AS price, dr_cr AS or_code FROM ( SELECT CASE WHEN t_date< dt_frm THEN '' ELSE j_code END  AS j_code, CASE WHEN t_date< dt_frm THEN '' ELSE j_num END AS j_num, CASE WHEN t_date< dt_frm THEN 'BALANCE FORWARDED' ELSE t_desc END  AS t_desc, CASE WHEN t_date< dt_frm THEN to_char((dt_frm::timestamp - ('1 day')::interval),'MM/DD/YYYY') ELSE to_char(t_date,'MM/DD/YYYY') END AS t_date, CASE WHEN t_date< dt_frm THEN '' ELSE payee END  AS payee, CASE WHEN t_date< dt_frm THEN '' ELSE ck_num END AS ck_num, CASE WHEN t_date< dt_frm THEN '' ELSE m4.at_desc END AS at_desc, CASE WHEN t_date< dt_frm THEN CASE WHEN t4.dr_cr='dr' THEN amount ELSE -1 * amount END ELSE amount END AS price, CASE WHEN t_date< dt_frm THEN 'dr' ELSE t4.dr_cr END  AS dr_cr, CASE WHEN t_date< dt_frm THEN ' Balance forwarded' ELSE CASE WHEN t4.dr_cr='dr' THEN 'ADD: Collection/Deposits' ELSE 'LESS: Disbursement' END END AS seq_desc, 1 AS or_lne FROM             " + pQry + "            t4 JOIN (SELECT * FROM rssys.m04 WHERE COALESCE(cib_acct,'N')='Y') m4 ON m4.at_code=t4.at_code UNION ALL SELECT '','','BALANCE FORWARDED',to_char(('" + dtp_frm.Value.ToString("yyyy-MM-dd") + "'::timestamp - ('1 day')::interval),'MM/DD/YYYY'),'','','',0.00,'dr',' Balance forwarded','1' ) t4 GROUP BY j_code, j_num, t_desc, t_date, payee, ck_num, at_desc, seq_desc, or_lne, dr_cr          UNION ALL           SELECT j_code, j_num, t_desc, to_char(t_date,'MM/DD/YYYY') AS t_date, payee, ck_num, m4.at_desc, CASE WHEN t4.dr_cr='dr' THEN 'Deposit in Transit' ELSE 'Outstanding Checks/' || (CASE WHEN COALESCE(isreleased,'')='Y' THEN 'Released' ELSE 'Unreleased' END) END AS seq_desc, 2 as or_lne, amount AS price, t4.dr_cr AS or_code  FROM              " + pQry + "           t4 JOIN (SELECT * FROM rssys.m04 WHERE COALESCE(cib_acct,'N')='Y') m4  ON m4.at_code=t4.at_code " + (!ischkbox_checked(chk_3) ? " WHERE  ('" + dtp_frm.Value.ToString("yyyy-MM-dd") + "')::date<=(t_date)::date " : "") + " ORDER BY or_lne ASC, seq_desc ASC, j_code, j_num");

                    //"SELECT j_code, j_num, t_desc, to_char(t_date,'MM/DD/YYYY') AS t_date, t_date AS t_date2, payee, ck_num, m4.at_desc, m4.cib_acct,(CASE WHEN debit<>0 THEN 'Y' ELSE 'N' END) AS or_code, (CASE WHEN debit<>0 THEN 'ADD: Collection/Deposits' ELSE 'LESS: Disbursement' END) AS seq_desc, 1 AS or_lne, (CASE WHEN debit<>0 THEN debit ELSE credit END) AS price FROM       " + pQry + "         t4 JOIN (SELECT * FROM rssys.m04 WHERE COALESCE(cib_acct,'N')='Y') m4 ON m4.at_code=t4.at_code               UNION ALL            SELECT j_code, j_num, t_desc, to_char(t_date,'MM/DD/YYYY') AS t_date, t_date AS t_date2, payee, ck_num, m4.at_desc, m4.cib_acct, (CASE WHEN debit<>0 THEN 'Y' ELSE 'N' END) AS or_code,(CASE WHEN debit<>0 THEN 'Deposit in Transit' ELSE 'Outstanding Checks/' || (CASE WHEN COALESCE(m4.cib_acct,'')<>'Y' THEN ' Released' ELSE 'Unreleased' END)END) AS seq_desc, 2 AS or_lne,(CASE WHEN debit<>0 THEN debit ELSE credit END) AS price FROM          " + pQry + "                t4 JOIN (SELECT * FROM rssys.m04 WHERE COALESCE(cib_acct,'N')='Y') m4 ON m4.at_code=t4.at_code  ORDER BY or_lne, or_code DESC, seq_desc, j_code, j_num"


                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("acct_id", acct);
                    add_fieldparam("fy", fy + entries);
                    add_fieldparam("t_date", dtp_frm.Value.ToString("MM/dd/yy") + " To " + dtp_to.Value.ToString("MM/dd/yy"));

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A104")
                {
                    //this.Text = "Unposted Entries";
                    inc_pbar(10);
                    if (get_cbo_index(cbo_2) == -1)
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_3, true);
                        reset();
                        return;
                    }

                    String fy = ((String)get_cbo_value(cbo_2).Split('-').GetValue(0)).Trim();

                    String jrnl = "All";
                    String acct = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t1.j_code='" + get_cbo_value(cbo_1) + "'";
                        jrnl = get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    String dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_3) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to FROM rssys.x03 x3) sx3)";

                    String trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num) WHERE 1=1 " + WHERE + ")";

                    String pQry = "(SELECT t4.* FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo) WHERE t4.t_desc not like ('%CANCELLED-%'))";

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "104_unposted_entries_summary.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT t4.mo, t4.j_num, t4.j_code, to_char(t4.t_date, 'MM/dd/yyyy') AS t_date, t4.t_desc, SUM(t4.debit) AS debit, SUM(t4.credit) AS credit, t4.payee, t4.ck_num, t4.ck_date  FROM " + pQry + " t4 LEFT JOIN rssys.tr03 t3 ON (t4.j_code=t3.j_code AND t4.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code GROUP BY t4.mo, t4.j_num, t4.j_code, t4.t_desc,  t4.t_date, t4.payee, t4.ck_num, t4.ck_date  ORDER BY t_date");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    add_fieldparam("t_date", (get_cbo_text(cbo_2).Split('-')[1] ?? "") + "-" + (get_cbo_text(cbo_3).Split('-')[1] ?? ""));
                    add_fieldparam("fy", fy);
                    add_fieldparam("jrnl", jrnl);
                    //add_fieldparam("acct_id", acct);

                    disp_reportviewer(myReportDocument);
                }

                else if (rpt_no == "A105")
                {
                    //this.Text = "Summary of Input Tax";
                    inc_pbar(10);
                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    WHERE = "";

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "105_input_tax_summary.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT t1.*, COALESCE(c_addr1,c_addr2,'') AS addr, c_tin AS tin FROM (SELECT t1.t_desc, t1.payee, t1.j_num, t1.j_code, to_char(t1.t_date,'MM/DD/YYYY') AS t_date, SUM(t2.debit) AS amount, (SELECT sl_code FROM rssys.tr02 t2t WHERE t2t.j_num=t1.j_num AND t2t.j_code=t1.j_code LIMIT 1) AS sl_code FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON (t2.j_num=t1.j_num AND t2.j_code=t1.j_code) WHERE COALESCE(payee,'')<>'' AND (t1.cancel is null OR t1.cancel != 'Y') " + WHERE + " AND (t1.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') GROUP BY t1.t_desc, t1.payee, t1.j_num, t1.j_code, t1.t_date) t1 LEFT JOIN rssys.m07 m7 ON (t1.sl_code=m7.c_code) ORDER BY j_code, j_num");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    add_fieldparam("t_date", t_date);
                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A106")
                {
                    //this.Text = "Summary of Statement of Account";
                    inc_pbar(10);
                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");

                    //if (get_cbo_index(cbo_4) != -1)
                    //{
                    //    WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    //}

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "106_soa_summary.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT sh.soa_code as soa_num, to_char(sh.soa_date,'MM/DD/YYYY') AS soa_date, to_char(sh.due_date,'MM/DD/YYYY') AS due_date, sh.debt_code, sh.debt_name, sh.comments, (SELECT SUM(sl.amount) FROM rssys.soalne sl WHERE sl.soa_code=sh.soa_code) AS amount FROM rssys.soahdr sh WHERE (sh.soa_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY sh.soa_code");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    //add_fieldparam("branch", "");

                    add_fieldparam("t_date", t_date);
                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A107")
                {
                    //this.Text = "Account movement summary";
                    inc_pbar(10);
                    String j_code_frm = get_cbo_value(cbo_1);
                    String j_code_to = get_cbo_value(cbo_2);
                    String entries = "";

                    String jrnl = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t4.j_code >= '" + get_cbo_value(cbo_1) + "'";
                        jrnl = get_cbo_text(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND t4.j_code <= '" + get_cbo_value(cbo_2) + "'";
                        jrnl = "First To " + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND (t4.j_code BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        jrnl = j_code_frm + " To " + j_code_to;
                        if (get_cbo_text(cbo_1) == get_cbo_text(cbo_2))
                        {
                            jrnl = get_cbo_text(cbo_1);
                        }
                    }


                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t4.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    String pQry = "";
                    String trQry = "";

                    trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel, 'post' AS entry FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel, 'unpost' AS entry  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";


                    pQry = "(SELECT * FROM " + trQry + " t4 WHERE t4.t_desc not like ('%CANCELLED-%') AND COALESCE(t4.cancel,'')<>'Y' " + WHERE + " AND (t4.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "'))";


                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "107_acct_activity_movement.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT t4.at_code AS id1, m4.at_desc AS desc1, SUM(CASE WHEN entry='post' THEN t4.debit ELSE 0 END) AS amount1, SUM(CASE WHEN entry='post' THEN t4.credit ELSE 0 END) AS amount2, SUM(CASE WHEN entry='unpost' THEN t4.debit ELSE 0 END) AS amount3, SUM(CASE WHEN entry='unpost' THEN t4.credit ELSE 0 END) AS amount4 FROM " + pQry + "  t4 LEFT JOIN rssys.tr03 t3 ON (t4.j_code=t3.j_code AND t4.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code GROUP BY  t4.at_code, m4.at_desc ORDER BY t4.at_code, m4.at_desc");
                    

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    add_fieldparam("t_date", dtp_frm.Value.ToString("yyyy-MM-dd") + " - " + dtp_to.Value.ToString("yyyy-MM-dd"));
                    add_fieldparam("jrnl", jrnl);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A201")
                {
                    //this.Text = "Check By Entry Date";
                    inc_pbar(10);
                    String at_code_frm = get_cbo_value(cbo_1);
                    String at_code_to = get_cbo_value(cbo_2);

                    String acct = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t2.at_code >= '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND t2.at_code <= '" + get_cbo_value(cbo_2) + "'";
                        acct = "First To (" + get_cbo_value(cbo_2) + ")" + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND (t2.at_code BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1) + " To (" + get_cbo_value(cbo_2) + ")" + get_cbo_text(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                        }
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "201_check_by_entry_date.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT t1.payee, to_char(t1.ck_date, 'MM/dd/yyyy') AS ck_date ,t1.ck_num, t1.j_num, t1.j_code, to_char(t1.t_date, 'MM/dd/yyyy') AS t_date, t1.t_desc, t2.at_code, m4.at_desc, (t2.debit - t2.credit) AS price FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) LEFT JOIN rssys.tr03 t3 ON (t1.j_code=t3.j_code AND t1.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t2.at_code WHERE (t1.cancel is null OR t1.cancel != 'Y') AND (t1.ck_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "')  AND COALESCE(t1.ck_num,'')<>'' " + WHERE + " AND t1.t_desc not like ('%CANCELLED-%') ORDER BY t1.t_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    add_fieldparam("acct_id", acct);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }

                else if (rpt_no == "A202")
                {
                    //this.Text = "Check By Check Date";
                    inc_pbar(10);
                    String at_code_frm = get_cbo_value(cbo_1);
                    String at_code_to = get_cbo_value(cbo_2);

                    String acct = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t2.at_code >= '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND t2.at_code <= '" + get_cbo_value(cbo_2) + "'";
                        acct = "First To (" + get_cbo_value(cbo_2) + ")" + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND (t2.at_code BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1) + " To (" + get_cbo_value(cbo_2) + ")" + get_cbo_text(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                        }
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "202_check_by_chkdate.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT t1.payee, t1.j_num, t1.j_code, to_char(t1.t_date, 'MM/dd/yyyy') AS t_date, to_char(t1.ck_date, 'MM/dd/yyyy') AS ck_date ,t1.ck_num, t1.t_desc, t2.at_code, m4.at_desc, (t2.debit - t2.credit) AS price FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) LEFT JOIN rssys.tr03 t3 ON (t1.j_code=t3.j_code AND t1.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t2.at_code WHERE (t1.cancel is null OR t1.cancel != 'Y') AND (t1.ck_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') AND COALESCE(t1.ck_num,'')<>'' " + WHERE + " AND t1.t_desc not like ('%CANCELLED-%') ORDER BY t1.ck_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    add_fieldparam("acct_id", acct);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A203")
                {
                    //this.Text = "Check By Check No";
                    inc_pbar(10);
                    String ck_num_frm = "";
                    String ck_num_to = "";

                    String at_code_frm = get_cbo_value(cbo_1);
                    String at_code_to = get_cbo_value(cbo_2);

                    String acct = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t2.at_code >= '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND t2.at_code <= '" + get_cbo_value(cbo_2) + "'";
                        acct = "First To (" + get_cbo_value(cbo_2) + ")" + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND (t2.at_code BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1) + " To (" + get_cbo_value(cbo_2) + ")" + get_cbo_text(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                        }
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "203_check_by_ckno.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT t1.payee, t1.j_num, t1.j_code, to_char(t1.t_date, 'MM/dd/yyyy') AS t_date, to_char(t1.ck_date, 'MM/dd/yyyy') AS ck_date ,t1.ck_num, t1.t_desc, t2.at_code, m4.at_desc, (t2.debit - t2.credit) AS price FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) LEFT JOIN rssys.tr03 t3 ON (t1.j_code=t3.j_code AND t1.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t2.at_code WHERE (t1.cancel is null OR t1.cancel != 'Y') AND (t1.ck_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') AND COALESCE(t1.ck_num,'')<>''  " + WHERE + " AND t1.t_desc not like ('%CANCELLED-%') ORDER BY t1.ck_date, t1.ck_num ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("item", "All");
                    add_fieldparam("acct_id", acct);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }

                else if (rpt_no == "A300")
                {

                    if (get_cbo_index(cbo_1) == -1)
                    {
                        MessageBox.Show("Please select a period.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    String fy = ((String)get_cbo_value(cbo_1).Split('-').GetValue(0)).Trim();
                    //DateTime dt_t = DateTime.Parse(get_cbo_value(cbo_1) + "-01");

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t4.branch='" + get_cbo_value(cbo_4) + "' ";
                    }

                    String pQry = "";
                    String trQry = "", dtQry = "";

                    trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";

                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from,sx3._to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";

                    pQry = "(SELECT t4.* FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to) AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo) WHERE 1=1 " + WHERE + " )";

                    myReportDocument.Load(fileloc_acctg + "300_general_journal.rpt");

                    DataTable dt = db.QueryBySQLCode("(SELECT to_char(t_date,'Month') AS unit, to_char(t_date,'DD') AS mo,  t4.j_code, t4.j_num, t4.at_code, CASE WHEN COALESCE(debit,0.0)<> 0 THEN m4.at_desc ELSE '' END AS at_desc, CASE WHEN COALESCE(debit,0.0)<> 0 THEN '' ELSE m4.at_desc END AS chg_desc, t3.j_memo as item_desc, debit, credit, systime FROM " + pQry + " t4 LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code LEFT JOIN rssys.tr03 t3 ON (t3.j_num=t4.j_num AND t3.j_code=t4.j_code)) ORDER BY mo,systime, j_code, j_num,  debit desc, credit desc");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy);
                    add_fieldparam("t_date", get_cbo_text(cbo_1));

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A301")
                {
                    inc_pbar(10);
                    String mo = dtp_frm.Value.ToString("MMMM") + " to " + dtp_to.Value.ToString("MMMM");
                    String fy = dtp_to.Value.ToString("yyyy");
                    if (dtp_frm.Value.ToString("MMMM") == dtp_to.Value.ToString("MMMM"))
                        mo = dtp_frm.Value.ToString("MMMM");

                    String acct = "All";
                    String jrnl = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t4.at_code >= '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1) + " To End";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND t4.at_code <= '" + get_cbo_value(cbo_2) + "'";
                        acct = "Start To (" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND (t4.at_code BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        acct = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1) + " To (" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            jrnl = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                        }
                    }

                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE += " AND t4.j_code='" + get_cbo_value(cbo_3) + "'";
                        jrnl = "(" + get_cbo_value(cbo_3) + ") " + get_cbo_text(cbo_3);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t4.branch='" + get_cbo_value(cbo_4) + "' ";
                    }

                    String prQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_acctg + "301_general_ledger_activity_account_id.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT t4.payee, t4.ck_num, CASE WHEN COALESCE(t4.ck_num,'')<>'' THEN to_char(t4.ck_date, 'MM/dd/yyyy') ELSE '' END, t4.sl_code, t4.sl_name,t4.at_code, t4.j_num, t4.j_code, to_char(t4.t_date, 'Month') as mo, to_char(t4.t_date, 'MM/dd/yyyy') AS t_date, t4.t_desc, t4.at_code, m4.at_desc, t4.debit, t4.credit FROM " + prQry + " t4 LEFT JOIN rssys.tr03 t3 ON (t4.j_code=t3.j_code AND t4.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code WHERE (t4.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + WHERE + " ORDER BY t4.at_code, t4.j_code, t4.t_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    add_fieldparam("acct_id", acct);
                    add_fieldparam("jrnl", jrnl);
                    add_fieldparam("t_date", fy + " " + mo);
                    //add_fieldparam("costcenter", costcenter);

                    disp_reportviewer(myReportDocument);
                }

                else if (rpt_no == "A302")
                {
                    inc_pbar(10);
                    String mo = dtp_frm.Value.ToString("MMMM") + " to " + dtp_to.Value.ToString("MMMM");
                    String fy = dtp_to.Value.ToString("yyyy");
                    if (dtp_frm.Value.ToString("MMMM") == dtp_to.Value.ToString("MMMM"))
                        mo = dtp_frm.Value.ToString("MMMM");

                    String acct = "All";
                    String jrnl = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t4.j_code >= '" + get_cbo_value(cbo_1) + "'";
                        jrnl = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1) + " To End";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND t4.j_code <= '" + get_cbo_value(cbo_2) + "'";
                        jrnl = "Start To (" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND (t4.j_code BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        jrnl = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1) + " To (" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            jrnl = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                        }
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t4.branch='" + get_cbo_value(cbo_4) + "' ";
                    }

                    String prQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";

                    myReportDocument.Load(fileloc_acctg + "302_general_ledger_by_jrnlid.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT t4.payee, t4.ck_num, CASE WHEN COALESCE(t4.ck_num,'')<>'' THEN to_char(t4.ck_date, 'MM/dd/yyyy') ELSE '' END, t4.sl_code, t4.sl_name,t4.at_code, t4.j_num, t4.j_code, to_char(t4.t_date, 'Month') as mo, to_char(t4.t_date, 'MM/dd/yyyy') AS t_date, t4.t_desc, t4.at_code, m4.at_desc, t4.debit, t4.credit FROM " + prQry + " t4 LEFT JOIN rssys.tr03 t3 ON (t4.j_code=t3.j_code AND t4.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code WHERE (t4.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + WHERE + " ORDER BY  t4.j_code, t4.t_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    //add_fieldparam("acct_id", "(" + acct_id_frm + ") " + get_cbo_text(cbo_1) + " To (" + acct_id_to + ") " + get_cbo_text(cbo_2));
                    add_fieldparam("jrnl", jrnl);
                    add_fieldparam("t_date", fy + " " + mo);
                    //add_fieldparam("costcenter", costcenter);

                    disp_reportviewer(myReportDocument);
                }

                else if (rpt_no == "A303")
                {
                    //this.Text = "General Ledger Summary by Account ID";
                    inc_pbar(10);
                    String mo = dtp_frm.Value.ToString("MMMM") + " to " + dtp_to.Value.ToString("MMMM");
                    String fy = dtp_to.Value.ToString("yyyy");
                    if (dtp_frm.Value.ToString("MMMM") == dtp_to.Value.ToString("MMMM"))
                        mo = dtp_frm.Value.ToString("MMMM");

                    String acct = "All";
                    String jrnl = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " AND t4.at_code >= '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1) + " To End";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND t4.at_code <= '" + get_cbo_value(cbo_2) + "'";
                        acct = "Start To (" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = " AND (t4.at_code BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        acct = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1) + " To " + "(" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_1))
                        {
                            acct = "(" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2);
                        }
                    }

                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE += " AND t4.j_code='" + get_cbo_value(cbo_3) + "'";
                        jrnl = "(" + get_cbo_value(cbo_3) + ") " + get_cbo_text(cbo_3);
                    }
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t4.branch='" + get_cbo_value(cbo_4) + "' ";
                    }

                    String prQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_acctg + "303_general_ledger_summary_by_acctid.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT t4.j_code, m5.j_desc AS t_desc, t4.at_code, m4.at_desc, SUM(t4.debit) AS debit, SUM(t4.credit) AS credit FROM " + prQry + " t4 LEFT JOIN rssys.tr03 t3 ON (t4.j_code=t3.j_code AND t4.j_num=t3.j_num) LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code JOIN rssys.m05 m5 ON m5.j_code=t4.j_code WHERE (t4.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + WHERE + " GROUP BY  t4.j_code, t4.at_code, m4.at_desc, j_desc HAVING (SUM(t4.debit)<>0 OR SUM(t4.credit)<>0) ORDER BY  t4.j_code, t4.at_code");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));

                    add_fieldparam("acct_id", acct);
                    add_fieldparam("jrnl", jrnl);
                    add_fieldparam("t_date", fy + " " + mo);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A401")
                {
                    //this.Text = "Customer Aging Report";
                    inc_pbar(10);
                    String asOf = dtp_frm.Value.ToString("MM/dd/yyyy");
                    
                    String acct = "All";
                    String customer = "All";
                    WHERE = "";
                    String WHERE2 = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = " AND d_code >= '" + get_cbo_value(cbo_2) + "'";
                        customer = get_cbo_value(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND d_code <= '" + get_cbo_value(cbo_3) + "'";
                        customer = "First To " + get_cbo_value(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND (d_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        customer = get_cbo_value(cbo_2) + " To " + get_cbo_value(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            customer = get_cbo_value(cbo_2);
                        }
                    }

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND t2.at_code = '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                    }
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }
                    //
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "401_customers_ledger.rpt");
                    //JOIN (" + db.jrnlNotInTypeStr("'Purchase','Disbursement'") + ") m05 ON m05.j_code=t2.j_code
                    DataTable dt = db.QueryBySQLCode("SELECT t2.*, t1.t_desc, to_char(t1.t_date,'MM/dd/yyyy') AS t_date, t1.t_date AS t_date2 FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  " + WHERE2 + ")  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code)  WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date <= '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%') " + WHERE + " ORDER BY sl_code, t_date2");
                    

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("acct_id", acct);
                    add_fieldparam("customer", customer);
                    add_fieldparam("t_date", asOf);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A402")
                {
                    //this.Text = "Customer Aging Report";
                    inc_pbar(10);
                    String asOf = dtp_frm.Value.ToString("MM/dd/yyyy");

                    String acct = "All";
                    String customer = "All";
                    WHERE = "";
                    String WHERE2 = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = " AND d_code >= '" + get_cbo_value(cbo_2) + "'";
                        customer = get_cbo_value(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND d_code <= '" + get_cbo_value(cbo_3) + "'";
                        customer = "First To " + get_cbo_value(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND (d_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        customer = get_cbo_value(cbo_2) + " To " + get_cbo_value(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            customer = "(" + get_cbo_value(cbo_2) + ")" + get_cbo_text(cbo_2);
                        }
                    }

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND t2.at_code = '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "402_balancesfrom_customer.rpt");
                    //JOIN (" + db.jrnlNotInTypeStr("'Purchase','Disbursement'") + ") m05 ON m05.j_code=t2.j_code

                    DataTable dt = db.QueryBySQLCode("SELECT t2.*, t1.t_desc, to_char(t1.t_date,'MM/dd/yyyy') AS t_date FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  " + WHERE2 + ")  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code)  WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date <= '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "'  AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%') " + WHERE + " ORDER BY sl_code, t1.t_date ASC");


                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("acct_id", acct);
                    add_fieldparam("customer", customer);
                    add_fieldparam("t_date", asOf);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A403D")
                {
                    //this.Text = "Customer Aging Report";
                    inc_pbar(10);
                    String asOf = dtp_frm.Value.ToString("MM/dd/yyyy");
                    String _asOf = dtp_frm.Value.ToString("yyyy-MM-dd");

                    String acct = "All";
                    String customer = "All";
                    WHERE = "";
                    String WHERE2 = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = " AND d_code >= '" + get_cbo_value(cbo_2) + "'";
                        customer = get_cbo_value(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND d_code <= '" + get_cbo_value(cbo_3) + "'";
                        customer = "First To " + get_cbo_value(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND (d_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        customer = get_cbo_value(cbo_2) + " To " + get_cbo_value(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            customer = get_cbo_value(cbo_2);
                        }
                    }
                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND t2.at_code = '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "403_customer_aging_report_detailed.rpt");
                    //JOIN (" + db.jrnlNotInTypeStr("'Purchase','Disbursement'") + ") m05 ON m05.j_code=t2.j_code 
                    /*
                     (SELECT t1.t_desc, to_char(t1.t_date,'MM/dd/yyyy') AS t_date, (fdt.dt_to - t1.t_date) AS mo, debit + (credit * -1) AS chg_num, t2.* FROM (SELECT t2.* FROM rssys.tr02 t2 JOIN rssys.m06 m6 ON (t2.sl_code=m6.d_code AND t2.at_code=m6.at_code) WHERE COALESCE(sl_code,'')<>'') t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code) JOIN (SELECT ('" + dtp_frm.Value.ToString("yyyy-MM-dd") + "')::date AS dt_to) fdt ON (1=1) WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date <= fdt.dt_to " + WHERE + "  ORDER BY sl_code, t_date)
                     */

                    DataTable dt = db.QueryBySQLCode("SELECT res.*, (('" + _asOf + "')::date - t_date::date) AS mo, t_date::date AS t_date2 FROM (SELECT invoice, sl_code, sl_name, at_code, max(TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')) AS t_date, max(CASE WHEN TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')<>'' THEN t_desc ELSE '' END) AS t_desc, SUM(credit) AS credit, SUM(debit) AS debit, SUM(COALESCE(debit,0.00)) + SUM(COALESCE(credit,0.00) * -1)  AS chg_num FROM (SELECT inv.t_date AS it_date, t2.*  FROM (SELECT * FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  " + WHERE2 + ")  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')   ORDER BY sl_code, invoice) t2 JOIN rssys.tr01 t1 ON t1.j_code=t2.j_code AND t1.j_num=t2.j_num WHERE 1=1   " + WHERE + "  AND t1.t_date<='" + _asOf + "'            ) t2 LEFT JOIN (SELECT max(t1.t_date) AS t_date, max(t1.systime) AS systime, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) WHERE COALESCE(invoice,'')<>'' AND t_date<='" + _asOf + "'  GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND  TO_CHAR(t2.t_date,'YYYY-MM-DD')=TO_CHAR(inv.t_date,'YYYY-MM-DD') AND t2.systime=inv.systime)) res GROUP BY invoice, sl_code, sl_name, at_code) res WHERE COALESCE(chg_num,0)<>0 AND (lower(sl_name) not like '%beg%' OR lower(sl_name) not like '%bal%') ORDER BY sl_code, t_date::date");
                    //DataTable dt = db.QueryBySQLCode("SELECT res.* FROM (SELECT t1.t_desc, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN to_char(inv.t_date,'MM/dd/yyyy')  ELSE to_char(t1.t_date,'MM/dd/yyyy')  END AS t_date, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END AS t_date2, (('" + _asOf + "')::date - (CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END)) AS mo, COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00) AS chg_num, t2.sl_name, t2.sl_code, t2.invoice FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  " + WHERE2 + ")  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "') LEFT JOIN (SELECT t2.invoice, t2.sl_code, SUM(COALESCE(t2.debit,0.00) + ( COALESCE(t2.credit,0.00) * -1)) / (CASE WHEN COALESCE(drt2.cnt,0)=0 THEN 1 ELSE drt2.cnt END) AS amnt, CASE WHEN COALESCE(drt2.invoice,'')<>'' THEN 'Y' ELSE 'N' END AS is_invoice, drt2.cnt FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  " + WHERE2 + ")  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "')  LEFT JOIN (SELECT DISTINCT sl_code, sl_name, invoice, SUM(CASE WHEN credit=0 THEN 0 ELSE 1 END) AS cnt, SUM(credit) AS credit, SUM(debit) AS debit FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t1.t_date<='" + _asOf + "') WHERE COALESCE(sl_code,'')<>'' GROUP BY sl_code,sl_name, invoice ) drt2 ON (drt2.invoice=t2.invoice AND drt2.sl_code=t2.sl_code AND drt2.debit<>0)   WHERE t2.credit<>0 AND COALESCE(t2.invoice,'')<>''     " + WHERE + "              GROUP BY t2.invoice, t2.sl_code, drt2.invoice, drt2.cnt) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code AND crt2.is_invoice='Y')    LEFT JOIN (SELECT max(t1.t_date) AS t_date, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND inv.t_date<='" + _asOf + "')     WHERE (t2.debit<>0 OR COALESCE(crt2.amnt,0)=0)     " + WHERE + "           ) res WHERE COALESCE(chg_num,0)<>0 AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%') ORDER BY t_date2"); //AND (COALESCE(t2.invoice,'')<>'' OR mo=0) 
                    /*
                     SELECT res.* FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM (SELECT to_char(t1.t_date,'MM/dd/yyyy') AS t_date, (('2017-11-15')::date - t1.t_date) AS mo, COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00) AS amnt, t2.sl_code, t2.sl_name, t2.invoice FROM rssys.tr02 t2 JOIN rssys.m07 m7 ON (t2.sl_code=m7.c_code AND t2.at_code=m7.at_code) LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='2017-11-15') LEFT JOIN (SELECT invoice, sl_code, SUM(COALESCE(debit,0.00) + ( COALESCE(credit,0.00) * -1)) AS amnt FROM rssys.tr02 t2 JOIN rssys.m07 m7 ON (t2.sl_code=m7.c_code AND t2.at_code=m7.at_code) LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='2017-11-15') WHERE credit<>0 AND COALESCE(invoice,'')<>''     AND t2.at_code = '1111' AND t1.branch='001'           GROUP BY invoice, sl_code ORDER BY sl_code, invoice) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code) WHERE debit<>0        AND t2.at_code = '1111' AND t1.branch='001'           ) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0

                     
                     */
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("acct_id", acct);
                    add_fieldparam("customer", customer);
                    add_fieldparam("t_date", asOf);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A403S")
                {
                    //this.Text = "Customer Aging Report";
                    inc_pbar(10);
                    String asOf = dtp_frm.Value.ToString("MM/dd/yyyy");
                    String _asOf = dtp_frm.Value.ToString("yyyy-MM-dd");

                    String acct = "All";
                    String customer = "All";
                    WHERE = "";
                    String WHERE2 = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = " AND d_code >= '" + get_cbo_value(cbo_2) + "'";
                        customer = get_cbo_value(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND d_code <= '" + get_cbo_value(cbo_3) + "'";
                        customer = "First To " + get_cbo_value(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND (d_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        customer = get_cbo_value(cbo_2) + " To " + get_cbo_value(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            customer = get_cbo_value(cbo_2);
                        }
                    }
                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND t2.at_code = '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "403_customer_aging_report_summary.rpt");
                    //JOIN (" + db.jrnlNotInTypeStr("'Purchase','Disbursement'") + ") m05 ON m05.j_code=t2.j_code
                    /*
                     "SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM (SELECT t1.t_desc, to_char(t1.t_date,'MM/dd/yyyy') AS t_date, (fdt.dt_to - t1.t_date) AS mo, debit + (credit * -1) AS amnt, t2.* FROM (SELECT t2.* FROM rssys.tr02 t2 JOIN rssys.m06 m6 ON (t2.sl_code=m6.d_code AND t2.at_code=m6.at_code)  WHERE COALESCE(sl_code,'')<>'') t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code) JOIN (SELECT ('" + dtp_frm.Value.ToString("yyyy-MM-dd") + "')::date AS dt_to) fdt ON (1=1) WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date <= fdt.dt_to " + WHERE + " ORDER BY t2.sl_code, t1.t_date) res GROUP BY sl_code, sl_name ORDER BY sl_code"
                     */
                    //String iss = "        SELECT t2.invoice, t2.sl_code, SUM(COALESCE(t2.debit,0.00) + ( COALESCE(t2.credit,0.00) * -1)) AS amnt, CASE WHEN COALESCE(drt2.invoice,'')<>'' THEN 'Y' ELSE 'N' END AS is_invoice FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  " + WHERE2 + ")  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "')  LEFT JOIN rssys.tr02 drt2 ON (drt2.invoice=t2.invoice AND drt2.sl_code=t2.sl_code AND drt2.debit<>0)   WHERE t2.credit<>0 AND COALESCE(t2.invoice,'')<>''    " + WHERE + "           GROUP BY t2.invoice, t2.sl_code, drt2.invoice ORDER BY t2.sl_code, t2.invoice    ";

                    DataTable dt = db.QueryBySQLCode("SELECT res.* FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM ( SELECT res.*, (('" + _asOf + "')::date - t_date::date) AS mo FROM (SELECT invoice, sl_code, sl_name, at_code, max(TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')) AS t_date, max(CASE WHEN TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')<>'' THEN t_desc ELSE '' END) AS t_desc, SUM(credit) AS credit, SUM(debit) AS debit, SUM(COALESCE(debit,0.00)) + SUM(COALESCE(credit,0.00) * -1)  AS amnt FROM (SELECT inv.t_date AS it_date, t2.*  FROM (SELECT * FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  " + WHERE2 + ")  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')   ORDER BY sl_code, invoice) t2 JOIN rssys.tr01 t1 ON t1.j_code=t2.j_code AND t1.j_num=t2.j_num WHERE 1=1   " + WHERE + "  AND t1.t_date<='" + _asOf + "'            ) t2 LEFT JOIN (SELECT max(t1.t_date) AS t_date, max(t1.systime) AS systime, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) WHERE COALESCE(invoice,'')<>'' AND t_date<='" + _asOf + "'  GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND  TO_CHAR(t2.t_date,'YYYY-MM-DD')=TO_CHAR(inv.t_date,'YYYY-MM-DD') AND t2.systime=inv.systime)) res GROUP BY invoice, sl_code, sl_name, at_code) res WHERE COALESCE(amnt,0)<>0 ORDER BY sl_code, t_date::date) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE (COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0) AND ((COALESCE(_cur30days,0) + COALESCE(_3160days,0) + COALESCE(_6190days,0) + COALESCE(_91120days,0) + COALESCE(_over120days,0)))<>0 AND (lower(sl_name) not like '%beg%' OR lower(sl_name) not like '%bal%') ORDER BY sl_code");
                    //DataTable dt = db.QueryBySQLCode("SELECT res.* FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM (SELECT t1.t_desc, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN to_char(inv.t_date,'MM/dd/yyyy')  ELSE to_char(t1.t_date,'MM/dd/yyyy')  END AS t_date, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END AS t_date2, (('" + _asOf + "')::date - (CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END)) AS mo, COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00) AS amnt, t2.sl_name, t2.sl_code, t2.invoice FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  " + WHERE2 + ")  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "') LEFT JOIN (SELECT t2.invoice, t2.sl_code, SUM(COALESCE(t2.debit,0.00) + ( COALESCE(t2.credit,0.00) * -1)) / (CASE WHEN COALESCE(drt2.cnt,0)=0 THEN 1 ELSE drt2.cnt END) AS amnt, CASE WHEN COALESCE(drt2.invoice,'')<>'' THEN 'Y' ELSE 'N' END AS is_invoice, drt2.cnt FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  " + WHERE2 + ")  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "')  LEFT JOIN (SELECT DISTINCT sl_code, sl_name, invoice, SUM(CASE WHEN credit=0 THEN 0 ELSE 1 END) AS cnt, SUM(credit) AS credit, SUM(debit) AS debit FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t1.t_date<='" + _asOf + "') WHERE COALESCE(sl_code,'')<>'' GROUP BY sl_code,sl_name, invoice) drt2 ON (drt2.invoice=t2.invoice AND drt2.sl_code=t2.sl_code AND drt2.debit<>0)   WHERE t2.credit<>0 AND COALESCE(t2.invoice,'')<>''     " + WHERE + "              GROUP BY t2.invoice, t2.sl_code, drt2.invoice, drt2.cnt ) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code AND crt2.is_invoice='Y')    LEFT JOIN (SELECT max(t1.t_date) AS t_date, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND inv.t_date<='" + _asOf + "')     WHERE (t2.debit<>0 OR COALESCE(crt2.amnt,0)=0)     " + WHERE + "            ) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE (COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0) AND ((COALESCE(_cur30days,0) + COALESCE(_3160days,0) + COALESCE(_6190days,0) + COALESCE(_91120days,0) + COALESCE(_over120days,0)))<>0 AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%')"); //AND (COALESCE(t2.invoice,'')<>'' OR mo=0)

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("acct_id", acct);
                    add_fieldparam("customer", customer);
                    add_fieldparam("t_date", asOf);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A404")
                {
                    inc_pbar(10);
                    String asOf = dtp_frm.Value.ToString("MM/dd/yyyy");

                    String acct = "All";
                    String supplier = "All";
                    WHERE = "";
                    String WHERE2 = "";
                    
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = " AND c_code >= '" + get_cbo_value(cbo_2) + "'";
                        supplier = get_cbo_value(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND c_code <= '" + get_cbo_value(cbo_3) + "'";
                        supplier = "First To " + get_cbo_value(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND (c_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        supplier = get_cbo_value(cbo_2) + " To " + get_cbo_value(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            supplier = "(" + get_cbo_value(cbo_2) + ")" + get_cbo_text(cbo_2);
                        }
                    }
                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND t2.at_code = '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }
                    
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "404_suppliers_ledger.rpt");
                    //JOIN (" + db.jrnlNotInTypeStr("'Sales','Collection'") + ") m05 ON m05.j_code=t2.j_code
                    DataTable dt = db.QueryBySQLCode("SELECT t2.*, t1.t_desc, to_char(t1.t_date,'MM/dd/yyyy') AS t_date, t1.t_date AS t_date2 FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1  " + WHERE2 + ")  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code ORDER BY c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code)  WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date <= '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%') " + WHERE + " ORDER BY sl_code, t_date2");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("acct_id", acct);
                    add_fieldparam("supplier", supplier);
                    add_fieldparam("t_date", asOf);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A405")
                {
                    inc_pbar(10);
                    String asOf = dtp_frm.Value.ToString("MM/dd/yyyy");

                    String acct = "All";
                    String supplier = "All";
                    WHERE = "";
                    String WHERE2 = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = " AND c_code >= '" + get_cbo_value(cbo_2) + "'";
                        supplier = get_cbo_value(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND c_code <= '" + get_cbo_value(cbo_3) + "'";
                        supplier = "First To " + get_cbo_value(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND (c_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        supplier = get_cbo_value(cbo_2) + " To " + get_cbo_value(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            supplier = get_cbo_value(cbo_2);
                        }
                    }

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND t2.at_code = '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "405_balancesto_supplier.rpt");
                    //JOIN (" + db.jrnlNotInTypeStr("'Sales','Collection'") + ") m05 ON m05.j_code=t2.j_code
                    DataTable dt = db.QueryBySQLCode("SELECT t2.*, t1.t_desc, to_char(t1.t_date,'MM/dd/yyyy') AS t_date FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1  " + WHERE2 + ")  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code ORDER BY c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code)   WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date <= '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%') " + WHERE + " ORDER BY sl_code, t_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("acct_id", acct);
                    add_fieldparam("supplier", supplier);
                    add_fieldparam("t_date", asOf);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A406D")
                {
                    //this.Text = "Supplier Aging Report";
                    inc_pbar(10);
                    String acct_id = get_cbo_value(cbo_1);
                    String sl_name = get_cbo_value(cbo_2);

                    String asOf = dtp_frm.Value.ToString("MM/dd/yyyy");
                    String _asOf = dtp_frm.Value.ToString("yyyy-MM-dd");

                    String acct = "All";
                    String supplier = "All";
                    WHERE = "";
                    String WHERE2 = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = " AND c_code >= '" + get_cbo_value(cbo_2) + "'";
                        supplier = get_cbo_value(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND c_code <= '" + get_cbo_value(cbo_3) + "'";
                        supplier = "First To " + get_cbo_value(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND (c_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        supplier = get_cbo_value(cbo_2) + " To " + get_cbo_value(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            supplier = get_cbo_value(cbo_2);
                        }
                    }
                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND t2.at_code = '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "406_supplier_aging_report_detailed.rpt");
                    //JOIN (" + db.jrnlNotInTypeStr("'Sales','Collection'") + ") m05 ON m05.j_code=t2.j_code
                    //DataTable dt = db.QueryBySQLCode("(SELECT t1.t_desc, to_char(t1.t_date,'MM/dd/yyyy') AS t_date, (fdt.dt_to - t1.t_date) AS mo, debit + (credit * -1) AS chg_num, t2.* FROM (SELECT t2.* FROM rssys.tr02 t2 JOIN rssys.m07 m7 ON (t2.sl_code=m7.c_code AND t2.at_code=m7.at_code)   WHERE COALESCE(sl_code,'')<>'') t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code) JOIN (SELECT ('" + dtp_frm.Value.ToString("yyyy-MM-dd") + "')::date AS dt_to) fdt ON (1=1) WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date <= fdt.dt_to " + WHERE + "  ORDER BY sl_code, t_date)");
                    //DataTable dt = db.QueryBySQLCode("SELECT res.* FROM (SELECT t1.t_desc, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN to_char(inv.t_date,'MM/dd/yyyy')  ELSE to_char(t1.t_date,'MM/dd/yyyy')  END AS t_date, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END AS t_date2, (('" + _asOf + "')::date - (CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END)) AS mo, COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00) AS chg_num, t2.sl_name, t2.sl_code, t2.invoice FROM rssys.tr02 t2 JOIN rssys.m07 m7 ON (t2.sl_code=m7.c_code AND t2.at_code=m7.at_code) LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "') LEFT JOIN (SELECT invoice, sl_code, SUM(COALESCE(debit,0.00) + ( COALESCE(credit,0.00) * -1)) AS amnt FROM rssys.tr02 t2 JOIN rssys.m07 m7 ON (t2.sl_code=m7.c_code AND t2.at_code=m7.at_code) LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "') WHERE credit<>0 AND COALESCE(invoice,'')<>''    " + WHERE + "           GROUP BY invoice, sl_code ORDER BY sl_code, invoice) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code)    LEFT JOIN (SELECT max(t1.t_date) AS t_date, invoice FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) GROUP BY invoice) inv ON (t2.invoice=inv.invoice AND inv.t_date<='" + _asOf + "')     WHERE debit<>0      " + WHERE + "           ) res WHERE COALESCE(chg_num,0)<>0 AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%') ORDER BY t_date2");
                    DataTable dt = db.QueryBySQLCode("SELECT res.*, (('" + _asOf + "')::date - t_date::date) AS mo, t_date::date AS t_date2 FROM (SELECT invoice, sl_code, sl_name, at_code, max(TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')) AS t_date, max(CASE WHEN TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')<>'' THEN t_desc ELSE '' END) AS t_desc, SUM(credit) AS credit, SUM(debit) AS debit, (SUM(COALESCE(debit,0.00)) + SUM(COALESCE(credit,0.00) * -1)) * -1  AS chg_num FROM (SELECT inv.t_date AS it_date, t2.*  FROM (SELECT * FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1  " + WHERE2 + ")  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code ORDER BY c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')   ORDER BY sl_code, invoice) t2 JOIN rssys.tr01 t1 ON t1.j_code=t2.j_code AND t1.j_num=t2.j_num WHERE 1=1   " + WHERE + "  AND t1.t_date<='" + _asOf + "') t2 LEFT JOIN (SELECT max(t1.t_date) AS t_date, max(t1.systime) AS systime, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) WHERE COALESCE(invoice,'')<>'' AND t_date<='" + _asOf + "'  GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND  TO_CHAR(t2.t_date,'YYYY-MM-DD')=TO_CHAR(inv.t_date,'YYYY-MM-DD') AND t2.systime=inv.systime)) res GROUP BY invoice, sl_code, sl_name, at_code) res WHERE COALESCE(chg_num,0)<>0 AND (lower(sl_name) not like '%beg%' OR lower(sl_name) not like '%bal%') ORDER BY sl_code, t_date::date");
                    //DataTable dt = db.QueryBySQLCode("SELECT res.* FROM (SELECT t1.t_desc, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN to_char(inv.t_date,'MM/dd/yyyy')  ELSE to_char(t1.t_date,'MM/dd/yyyy')  END AS t_date, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END AS t_date2, (('" + _asOf + "')::date - (CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END)) AS mo, (COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00)) * -1 AS chg_num, t2.sl_name, t2.sl_code, t2.invoice FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1  " + WHERE2 + ")  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code ORDER BY c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "') LEFT JOIN ( SELECT t2.invoice, t2.sl_code, SUM(COALESCE(t2.debit,0.00) + ( COALESCE(t2.credit,0.00) * -1)) / (CASE WHEN COALESCE(drt2.cnt,0)=0 THEN 1 ELSE drt2.cnt END) AS amnt, CASE WHEN COALESCE(drt2.invoice,'')<>'' THEN 'Y' ELSE 'N' END AS is_invoice,  drt2.cnt    FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1   " + WHERE2 + "     )  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "')  LEFT JOIN (SELECT DISTINCT sl_code, sl_name, invoice, SUM(CASE WHEN credit=0 THEN 0 ELSE 1 END) AS cnt, SUM(credit) AS credit, SUM(debit) AS debit FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t1.t_date<='" + _asOf + "') WHERE COALESCE(sl_code,'')<>'' GROUP BY sl_code,sl_name, invoice ) drt2 ON (drt2.invoice=t2.invoice AND drt2.sl_code=t2.sl_code AND drt2.credit<>0)   WHERE t2.debit<>0 AND COALESCE(t2.invoice,'')<>''     " + WHERE + "            GROUP BY t2.invoice, t2.sl_code, drt2.invoice, drt2.cnt  ) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code AND crt2.is_invoice='Y')    LEFT JOIN (SELECT max(t1.t_date) AS t_date, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND inv.t_date<='" + _asOf + "')     WHERE (t2.credit<>0 OR COALESCE(crt2.amnt,0)=0)     " + WHERE + "           ) res WHERE COALESCE(chg_num,0)<>0 AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%') ORDER BY t_date2");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("acct_id", acct);
                    add_fieldparam("supplier", supplier);
                    add_fieldparam("t_date", asOf);

                    disp_reportviewer(myReportDocument);

                }
                else if (rpt_no == "A406S")
                {
                    //this.Text = "Supplier Aging Report";
                    inc_pbar(10);
                    String acct_id = get_cbo_value(cbo_1);
                    String sl_name = get_cbo_value(cbo_2);

                    String asOf = dtp_frm.Value.ToString("MM/dd/yyyy");
                    String _asOf = dtp_frm.Value.ToString("yyyy-MM-dd");

                    String acct = "All";
                    String supplier = "All";
                    WHERE = "";
                    String WHERE2 = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = " AND c_code >= '" + get_cbo_value(cbo_2) + "'";
                        supplier = get_cbo_value(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND c_code <= '" + get_cbo_value(cbo_3) + "'";
                        supplier = "First To " + get_cbo_value(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = " AND (c_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        supplier = get_cbo_value(cbo_2) + " To " + get_cbo_value(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            supplier = get_cbo_value(cbo_2);
                        }
                    }
                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND t2.at_code = '" + get_cbo_value(cbo_1) + "'";
                        acct = "(" + get_cbo_value(cbo_1) + ")" + get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE += " AND t1.branch='" + get_cbo_value(cbo_4) + "'";
                    }

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "406_supplier_aging_report_summary.rpt");
                    //JOIN (" + db.jrnlNotInTypeStr("'Sales','Collection'") + ") m05 ON m05.j_code=t2.j_code
                    //DataTable dt = db.QueryBySQLCode("SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM (SELECT t1.t_desc, to_char(t1.t_date,'MM/dd/yyyy') AS t_date, (fdt.dt_to - t1.t_date) AS mo, debit + (credit * -1) AS amnt, t2.* FROM (SELECT t2.* FROM rssys.tr02 t2 JOIN rssys.m07 m7 ON (t2.sl_code=m7.c_code AND t2.at_code=m7.at_code)  WHERE COALESCE(sl_code,'')<>'') t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code) JOIN (SELECT ('" + dtp_frm.Value.ToString("yyyy-MM-dd") + "')::date AS dt_to) fdt ON (1=1) WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date <= fdt.dt_to " + WHERE + " ORDER BY t2.sl_code, t1.t_date) res GROUP BY sl_code, sl_name ORDER BY sl_code");

                    //DataTable dt = db.QueryBySQLCode("SELECT res.* FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM (SELECT CASE WHEN COALESCE(inv.invoice,'')<>'' THEN to_char(inv.t_date,'MM/dd/yyyy')  ELSE to_char(t1.t_date,'MM/dd/yyyy')  END AS t_date, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END AS t_date2, (('" + _asOf + "')::date - (CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END)) AS mo, COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00) AS amnt, t2.sl_code, t2.sl_name, t2.invoice FROM rssys.tr02 t2 JOIN rssys.m07 m7 ON (t2.sl_code=m7.c_code AND t2.at_code=m7.at_code) LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "') LEFT JOIN (SELECT invoice, sl_code, SUM(COALESCE(debit,0.00) + ( COALESCE(credit,0.00) * -1)) AS amnt FROM rssys.tr02 t2 JOIN rssys.m07 m7 ON (t2.sl_code=m7.c_code AND t2.at_code=m7.at_code) LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "') WHERE credit<>0 AND COALESCE(invoice,'')<>''    " + WHERE + "           GROUP BY invoice, sl_code ORDER BY sl_code, invoice) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code)    LEFT JOIN (SELECT max(t1.t_date) AS t_date, invoice FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) GROUP BY invoice) inv ON (t2.invoice=inv.invoice AND inv.t_date<='" + _asOf + "')  WHERE debit<>0       " + WHERE + "           ) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE (COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0) AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%')"); 






                    //String ss = "SELECT t2.invoice, t2.sl_code, SUM(COALESCE(t2.debit,0.00) + ( COALESCE(t2.credit,0.00) * -1)) AS amnt, CASE WHEN COALESCE(drt2.invoice,'')<>'' THEN 'Y' ELSE 'N' END AS is_invoice FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1  " + WHERE2 + ")  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code ORDER BY c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "')  LEFT JOIN rssys.tr02 drt2 ON (drt2.invoice=t2.invoice AND drt2.sl_code=t2.sl_code AND drt2.credit<>0)   WHERE t2.debit<>0 AND COALESCE(t2.invoice,'')<>''    " + WHERE + "           GROUP BY t2.invoice, t2.sl_code, drt2.invoice ORDER BY t2.sl_code, t2.invoice";


                    DataTable dt = db.QueryBySQLCode("SELECT res.* FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM ( SELECT res.*, (('" + _asOf + "')::date - t_date::date) AS mo FROM (SELECT invoice, sl_code, sl_name, at_code, max(TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')) AS t_date, max(CASE WHEN TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')<>'' THEN t_desc ELSE '' END) AS t_desc, SUM(credit) AS credit, SUM(debit) AS debit, (SUM(COALESCE(debit,0.00)) + SUM(COALESCE(credit,0.00) * -1)) * -1  AS amnt FROM (SELECT inv.t_date AS it_date, t2.*  FROM (SELECT * FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1  " + WHERE2 + ")  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code ORDER BY c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')   ORDER BY sl_code, invoice) t2 JOIN rssys.tr01 t1 ON t1.j_code=t2.j_code AND t1.j_num=t2.j_num WHERE 1=1   " + WHERE + "  AND t1.t_date<='" + _asOf + "'            ) t2 LEFT JOIN (SELECT max(t1.t_date) AS t_date, max(t1.systime) AS systime, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) WHERE COALESCE(invoice,'')<>'' AND t_date<='" + _asOf + "'  GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND  TO_CHAR(t2.t_date,'YYYY-MM-DD')=TO_CHAR(inv.t_date,'YYYY-MM-DD') AND t2.systime=inv.systime)) res GROUP BY invoice, sl_code, sl_name, at_code) res WHERE COALESCE(amnt,0)<>0 ORDER BY sl_code, t_date::date) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE (COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0) AND ((COALESCE(_cur30days,0) + COALESCE(_3160days,0) + COALESCE(_6190days,0) + COALESCE(_91120days,0) + COALESCE(_over120days,0)))<>0 AND (lower(sl_name) not like '%beg%' OR lower(sl_name) not like '%bal%') ORDER BY sl_code");

                    //DataTable dt = db.QueryBySQLCode("SELECT res.* FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM (SELECT t1.t_desc, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN to_char(inv.t_date,'MM/dd/yyyy')  ELSE to_char(t1.t_date,'MM/dd/yyyy')  END AS t_date, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END AS t_date2, (('" + _asOf + "')::date - (CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END)) AS mo, (COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00)) * -1 AS amnt, t2.sl_name, t2.sl_code, t2.invoice FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1  " + WHERE2 + ")  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code ORDER BY c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "') LEFT JOIN ( SELECT t2.invoice, t2.sl_code, SUM(COALESCE(t2.debit,0.00) + ( COALESCE(t2.credit,0.00) * -1)) / (CASE WHEN COALESCE(drt2.cnt,0)=0 THEN 1 ELSE drt2.cnt END) AS amnt, CASE WHEN COALESCE(drt2.invoice,'')<>'' THEN 'Y' ELSE 'N' END AS is_invoice,  drt2.cnt    FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1   " + WHERE2 + "     )  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _asOf + "')  LEFT JOIN (SELECT DISTINCT sl_code, sl_name, invoice, SUM(CASE WHEN credit=0 THEN 0 ELSE 1 END) AS cnt, SUM(credit) AS credit, SUM(debit) AS debit FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t1.t_date<='" + _asOf + "') WHERE COALESCE(sl_code,'')<>'' GROUP BY sl_code,sl_name, invoice ) drt2 ON (drt2.invoice=t2.invoice AND drt2.sl_code=t2.sl_code AND drt2.credit<>0)   WHERE t2.debit<>0 AND COALESCE(t2.invoice,'')<>''     " + WHERE + "            GROUP BY t2.invoice, t2.sl_code, drt2.invoice, drt2.cnt  ) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code AND crt2.is_invoice='Y')    LEFT JOIN (SELECT max(t1.t_date) AS t_date, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND inv.t_date<='" + _asOf + "')     WHERE (t2.credit<>0 OR COALESCE(crt2.amnt,0)=0)     " + WHERE + "            ) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE (COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0) AND ((COALESCE(_cur30days,0) + COALESCE(_3160days,0) + COALESCE(_6190days,0) + COALESCE(_91120days,0) + COALESCE(_over120days,0)))<>0 AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%')"); 
                    
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("acct_id", acct);
                    add_fieldparam("supplier", supplier);
                    add_fieldparam("t_date", asOf);

                    disp_reportviewer(myReportDocument);

                }
                else if (rpt_no == "A501")
                {
                    //Trail Balance
                    if (get_cbo_index(cbo_1) == -1)
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_2) == -1)
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_3, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }

                    String fy = ((String)get_cbo_value(cbo_1).Split('-').GetValue(0)).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "";
                    if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from, sx3._to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";

                    pQry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to) AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";
                    p2Qry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (sx3.begin<=x3.from AND x3.from<sx3.from) AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    myReportDocument.Load(fileloc_acctg + "501_trial_balance.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT m4.at_code, m4.at_desc, COALESCE(t4.debit,0.00) AS debit, COALESCE(t4.credit,0.00) AS credit, COALESCE(bt4.bal_begin,0.0) AS bal_begin, COALESCE((COALESCE(t4.debit,0.00) + (-1*COALESCE(t4.credit,0.00)) + COALESCE(bt4.bal_begin,0.0)),0.00) AS bal_end FROM rssys.m04 m4 LEFT JOIN (SELECT DISTINCT at_code, SUM(debit) AS debit, SUM(credit) AS credit FROM " + pQry + " t4 WHERE 1=1 " + WHERE + "  GROUP BY at_code) AS t4 ON t4.at_code=m4.at_code LEFT JOIN (SELECT DISTINCT t4.at_code, SUM(CASE WHEN m4.dr_cr='D' THEN debit-credit WHEN m4.dr_cr='C' THEN -1 * (credit-debit) ELSE 0.0 END) AS bal_begin FROM " + p2Qry + " t4 LEFT JOIN rssys.m04 m4 ON (t4.at_code=m4.at_code) WHERE 1=1 " + WHERE + " GROUP BY t4.at_code, m4.dr_cr) as bt4 ON bt4.at_code=m4.at_code WHERE (lower(m4.at_desc) not like '%net income (loss)%') Order By m4.at_code");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_3) + ")");
                    add_fieldparam("t_date", (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_1).Split('-')[1] ?? "")) + "-" + (get_cbo_text(cbo_2).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_2).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_2).Split('-')[1] ?? "")) + " " + (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? fy : ""));

                    disp_reportviewer(myReportDocument);

                }
                else if (rpt_no == "A502a")
                {
                    //Adjust Balance Sheet
                    if (get_cbo_index(cbo_1) == -1)
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_2) == -1)
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_3, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }

                    String fy = ((String)get_cbo_value(cbo_1).Split('-').GetValue(0)).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "";
                    if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from, sx3._to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";

                    pQry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to) AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    myReportDocument.Load(fileloc_acctg + "502_adjusted_balance_sheet.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT m4.at_code, m4.at_desc, t4.debit, t4.credit FROM rssys.m04 m4 LEFT JOIN (SELECT DISTINCT at_code, SUM(debit) AS debit, SUM(credit) AS credit FROM " + pQry + " t4 WHERE 1=1 " + WHERE + " GROUP BY at_code) AS t4 ON t4.at_code=m4.at_code  WHERE t4.credit is not null AND (lower(m4.at_desc) not like '%net income (loss)%') ORDER BY m4.at_code");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_3) + ")");
                    add_fieldparam("t_date", (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_1).Split('-')[1] ?? "")) + "-" + (get_cbo_text(cbo_2).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_2).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_2).Split('-')[1] ?? "")) + " " + (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? fy : ""));

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A502l")
                {
                    //this.Text = "Balance Sheet";
                    if (get_cbo_index(cbo_1) == -1)
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_2) == -1)
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_3, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }
                    String fy = ((String)get_cbo_value(cbo_1).Split('-').GetValue(0)).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "";

                    if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from,sx3._to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";

                    pQry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)  AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    myReportDocument.Load(fileloc_acctg + "502l_balance_sheet.rpt");
                    // modified by: Reancy 06 06 2018
                    // t4 - tr01, tr02 UNION ALL tr04
                    // x3, sx3 - x03
                    // m0 - m00
                    // m1 - m01
                    // m2 - m02
                    // m3 - m03
                    // m4 - m04
                    // m5 - m05
                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT j_num, t_desc, item_code, item_desc, at_code, at_desc, pos, m3dr_cr, SUM(amount) AS amount FROM( SELECT  COALESCE(res.item_code, m4.item_code) AS item_code, COALESCE(res.j_num, m4.j_num) AS j_num, COALESCE(res.t_desc,m4.t_desc) AS t_desc, COALESCE(res.item_desc,m4.item_desc) AS item_desc, COALESCE(res.at_code,m4.at_code) AS at_code, COALESCE(res.at_desc,m4.at_desc) AS at_desc,  COALESCE(res.m3dr_cr,m4.m4dr_cr) AS m3dr_cr, res.pos, res.amount AS amount FROM (SELECT  CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.item_code END AS item_code, CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.j_num END AS j_num, CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.t_desc END AS t_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.item_desc END AS item_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.at_code END AS at_code,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.at_desc END AS at_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.m3dr_cr END AS m3dr_cr,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN 3 ELSE res.pos END AS pos,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN CASE WHEN res.m3dr_cr='D' THEN -1 * res.amount  WHEN res.m3dr_cr='C' THEN  res.amount ELSE 0 END ELSE res.amount END AS amount FROM (       SELECT DISTINCT m4.name AS t_desc, initcap(m4.cmp_desc) AS item_desc, m4.at_code, m4.at_desc, m4.pos, m4.m3dr_cr, m4.j_num, m4.item_code, SUM(CASE WHEN m4.m4dr_cr='D' THEN  (debit-credit) WHEN m4.m4dr_cr='C' THEN (credit-debit) ELSE 0 END) as amount FROM " + pQry + " t4 LEFT JOIN (SELECT DISTINCT m4.at_code, m4.at_desc, m3.*, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr, m3.code AS j_num, m3.cmp_code2 AS item_code   FROM rssys.m04 m4 LEFT JOIN (SELECT DISTINCT m3.*, m2.*, m0.*, m3.cmp_code AS cmp_code2 FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%'))    OR (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%'))     ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE COALESCE(m4.name,'')<>'' " + WHERE + "     AND lower(m4.at_desc) not like ('%net income (loss)%')       GROUP BY m4.name, m4.cmp_desc, m4.at_code, m4.at_desc, m4.pos, m4.m3dr_cr, m4.j_num, m4.item_code         ) res ) res  LEFT JOIN               (SELECT m3.code AS j_num, m3.cmp_code2 AS item_code, m3.name AS t_desc, m3.cmp_desc AS item_desc, m4.at_code, m4.at_desc, 1, m4.dr_cr AS m4dr_cr  FROM rssys.m04 m4 LEFT JOIN (SELECT DISTINCT m3.*, m2.*, m0.*, m2.cmp_code AS cmp_code2 FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT DISTINCT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code WHERE lower(at_desc) like '%net income (loss)%')      m4 ON (COALESCE(res.t_desc,'')='')) res GROUP BY j_num, item_code, t_desc, item_desc, at_code, at_desc, m3dr_cr, pos HAVING SUM(amount)<>0 ORDER BY j_num, item_code, at_code");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_3) + ")");
                    add_fieldparam("t_date", (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_1).Split('-')[1] ?? "")) + "-" + (get_cbo_text(cbo_2).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_2).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_2).Split('-')[1] ?? "")) + " " + (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? fy : ""));

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A502")
                {
                    //this.Text = "Balance Sheet";
                    if (get_cbo_index(cbo_1) == -1)
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_2) == -1)
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_3, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }
                    String fy = ((String)get_cbo_value(cbo_1).Split('-').GetValue(0)).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "";
                    if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to FROM rssys.x03 x3) sx3)";

                    pQry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";


                    myReportDocument.Load(fileloc_acctg + "502_balance_sheet.rpt");


                    DataTable dt = db.QueryBySQLCode("SELECT SUM(CASE WHEN t_desc='ASSETS' THEN amount  ELSE 0 END) as  recv_qty, SUM(CASE WHEN t_desc='LIABILITIES' THEN amount  ELSE 0 END) as  debit, SUM(CASE WHEN t_desc like ('%EQUITY%') THEN amount  ELSE 0 END) as credit FROM(   SELECT  COALESCE(res.item_code, m4.item_code) AS item_code, COALESCE(res.j_num, m4.j_num) AS j_num, COALESCE(res.t_desc,m4.t_desc) AS t_desc, COALESCE(res.item_desc,m4.item_desc) AS item_desc, COALESCE(res.at_code,m4.at_code) AS at_code, COALESCE(res.at_desc,m4.at_desc) AS at_desc,  COALESCE(res.m3dr_cr,m4.m4dr_cr) AS m3dr_cr, res.pos, res.amount AS amount FROM (SELECT  CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.item_code END AS item_code, CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.j_num END AS j_num, CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.t_desc END AS t_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.item_desc END AS item_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.at_code END AS at_code,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.at_desc END AS at_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.m3dr_cr END AS m3dr_cr,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN 3 ELSE res.pos END AS pos,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN CASE WHEN res.m3dr_cr='D' THEN -1 * res.amount  WHEN res.m3dr_cr='C' THEN  res.amount ELSE 0 END ELSE res.amount END AS amount FROM (       SELECT DISTINCT m4.name AS t_desc, initcap(m4.cmp_desc) AS item_desc, m4.at_code, m4.at_desc, m4.pos, m4.m3dr_cr, m4.j_num, m4.item_code, SUM(CASE WHEN m4.m4dr_cr='D' THEN  (debit-credit) WHEN m4.m4dr_cr='C' THEN (credit-debit) ELSE 0 END) as amount FROM " + pQry + " t4 LEFT JOIN (SELECT m4.at_code, m4.at_desc, m3.*, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr, m3.code AS j_num, m3.cmp_code2 AS item_code   FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.*, m3.cmp_code AS cmp_code2 FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%'))    OR (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%'))     ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE COALESCE(m4.name,'')<>'' " + WHERE + "     AND lower(m4.at_desc) not like ('%net income (loss)%')       GROUP BY m4.name, m4.cmp_desc, m4.at_code, m4.at_desc, m4.pos, m4.m3dr_cr, m4.j_num, m4.item_code         ) res ) res  LEFT JOIN               (SELECT m3.code AS j_num, m3.cmp_code2 AS item_code, m3.name AS t_desc, m3.cmp_desc AS item_desc, m4.at_code, m4.at_desc, 1, m4.dr_cr AS m4dr_cr  FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.*, m2.cmp_code AS cmp_code2 FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code WHERE lower(at_desc) like '%net income (loss)%')      m4 ON (COALESCE(res.t_desc,'')='')   ) res");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_3) + ")");
                    add_fieldparam("t_date", (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_1).Split('-')[1] ?? "")) + "-" + (get_cbo_text(cbo_2).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_2).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_2).Split('-')[1] ?? "")) + " " + (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? fy : ""));

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A502m3")
                {
                    //this.Text = "Balance Sheet";
                    if (get_cbo_value(cbo_1) == "")
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_value(cbo_2) == "")
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }

                    String[] splt = get_cbo_value(cbo_1).Split('-');
                    String fy = ((String)splt.GetValue(0)).Trim();
                    String mo = ((String)splt.GetValue(1)).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "", dtftQry = "";

                    String mon2 = "0", fyn2;
                    int _mon2 = Convert.ToInt32(mo) + 1;
                    mon2 = (_mon2 == 13 ? mon2 : _mon2.ToString("0"));
                    fyn2 = (Convert.ToInt32(fy) + (_mon2 == 13 ? 1 : 0)).ToString("0");
	   

                    /*if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    dtftQry = "(SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, mos._mos, mos.mo2, mos.mo3, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to  FROM rssys.x03 x3 LEFT JOIN   (SELECT '" + mo + "'::text AS _mos, (CASE WHEN '" + mo + "'='0' THEN '0' ELSE '1' END)||' month' AS mo2,(CASE WHEN '" + mo + "'='0' THEN '1' ELSE '2' END)||' month' AS mo3) mos  ON (1=1) GROUP BY mos._mos, mos.mo2, mos.mo3)";


                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from,sx3._to FROM " + dtftQry + " sx3)";

                    pQry = "(SELECT *, x3.fy AS _fy, x3.mo AS _mo, x3.month_desc AS _moname FROM " + trQry + " t4 JOIN (SELECT x3.* FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)    AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to     ) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";
*/
                    if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT fy||'-'||LPAD(mo::text,3,'0') AS fymo, fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT fy||'-'||LPAD(mo::text,3,'0') AS fymo, fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT fy||'-'||LPAD(mo::text,3,'0') AS fymo, fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy||'-'||LPAD(mo::text,3,'0') AS fymo, fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    String begbalYr = "2017-0"; //" + fy + "-0  get_cbo_value(cbo_1) get_cbo_value(cbo_1)
                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from,sx3._to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + begbalYr + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + begbalYr + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + begbalYr + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";

                    pQry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)  AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    myReportDocument.Load(fileloc_acctg + "502_balance_sheet_comp.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT  DISTINCT j_num AS id1, t_desc AS name1, item_code AS id2, item_desc AS name2, at_code AS id3, at_desc AS name3, pos, m3dr_cr, SUM(CASE WHEN fymo<=_mo1 THEN amount ELSE 0 END) AS amount1, SUM(CASE WHEN fymo<=_mo2 THEN amount ELSE 0 END) AS amount2, SUM(CASE WHEN fymo<=_mo3 THEN amount ELSE 0 END) AS amount3, mo1 AS month1, mo2 AS month2, mo3 AS month3 FROM (                                                               SELECT DISTINCT j_num, t_desc, item_code, item_desc, at_code, at_desc, pos, m3dr_cr, SUM(amount) AS amount, fymo FROM( SELECT  COALESCE(res.item_code, m4.item_code) AS item_code, COALESCE(res.j_num, m4.j_num) AS j_num, COALESCE(res.t_desc,m4.t_desc) AS t_desc, COALESCE(res.item_desc,m4.item_desc) AS item_desc, COALESCE(res.at_code,m4.at_code) AS at_code, COALESCE(res.at_desc,m4.at_desc) AS at_desc,  COALESCE(res.m3dr_cr,m4.m4dr_cr) AS m3dr_cr, res.pos, res.amount AS amount, fymo FROM (SELECT  CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.item_code END AS item_code, CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.j_num END AS j_num, CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.t_desc END AS t_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.item_desc END AS item_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.at_code END AS at_code,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.at_desc END AS at_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.m3dr_cr END AS m3dr_cr,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN 3 ELSE res.pos END AS pos,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN CASE WHEN res.m3dr_cr='D' THEN -1 * res.amount  WHEN res.m3dr_cr='C' THEN  res.amount ELSE 0 END ELSE res.amount END AS amount, fymo FROM (       SELECT DISTINCT m4.name AS t_desc, initcap(m4.cmp_desc) AS item_desc, m4.at_code, m4.at_desc, m4.pos, m4.m3dr_cr, m4.j_num, m4.item_code, SUM(CASE WHEN m4.m4dr_cr='D' THEN  (debit-credit) WHEN m4.m4dr_cr='C' THEN (credit-debit) ELSE 0 END) as amount, fymo FROM " + pQry + " t4 LEFT JOIN (SELECT m4.at_code, m4.at_desc, m3.*, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr, m3.code AS j_num, m3.cmp_code2 AS item_code   FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.*, m3.cmp_code AS cmp_code2 FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%'))    OR (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%'))     ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE COALESCE(m4.name,'')<>'' " + WHERE + "     AND lower(m4.at_desc) not like ('%net income (loss)%')       GROUP BY m4.name, m4.cmp_desc, m4.at_code, m4.at_desc, m4.pos, m4.m3dr_cr, m4.j_num, m4.item_code, fymo         ) res ) res  LEFT JOIN               (SELECT m3.code AS j_num, m3.cmp_code2 AS item_code, m3.name AS t_desc, m3.cmp_desc AS item_desc, m4.at_code, m4.at_desc, 1, m4.dr_cr AS m4dr_cr  FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.*, m2.cmp_code AS cmp_code2 FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code WHERE lower(at_desc) like '%net income (loss)%')      m4 ON (COALESCE(res.t_desc,'')='')) res GROUP BY j_num, item_code, t_desc, item_desc, at_code, at_desc, m3dr_cr, pos, fymo HAVING SUM(amount)<>0 ORDER BY j_num, item_code, at_code                                  ) res JOIN (SELECT _mo1,_mo2,_mo3, mo1, CASE WHEN COALESCE(_mo2,'')='' THEN TO_CHAR(mo+'1 month'::interval,'Month') ELSE mo2 END AS mo2, CASE WHEN COALESCE(_mo3,'')='' THEN TO_CHAR(mo+'2 month'::interval,'Month') ELSE mo3 END AS mo3 FROM (SELECT x3.mo, _mo1, COALESCE(fy||'-'||LPAD(sx3.mo::text,3,'0'),'') AS _mo2,  CASE WHEN _mo1=_mo3 THEN '' ELSE _mo3 END AS _mo3, mo1, month_desc AS mo2, mo3 FROM (SELECT DISTINCT sx3.mo2, min(fy||'-'||LPAD(mo::text,3,'0')) AS _mo1, max(fy||'-'||LPAD(mo::text,3,'0')) AS _mo3, min(x3.from) AS mo, string_agg(CASE WHEN mo1=x3.fy||'-'||x3.mo THEN month_desc ELSE '' END,'') AS mo1, string_agg(CASE WHEN mo3=x3.fy||'-'||x3.mo THEN month_desc ELSE '' END,'') AS mo3 FROM rssys.x03 x3 JOIN (SELECT '" + get_cbo_value(cbo_1) + "'::text AS mo1, '" + fyn2 + "-" + mon2 + "'::text AS mo2, '" + get_cbo_value(cbo_2) + "'::text AS mo3) sx3 ON (x3.fy||'-'||x3.mo IN (sx3.mo1, sx3.mo3)) GROUP BY sx3.mo1, sx3.mo2, sx3.mo3) x3 LEFT JOIN rssys.x03 sx3 ON (x3.mo2=sx3.fy||'-'||sx3.mo)) x3) x3 ON (1=1) GROUP BY j_num, t_desc, item_code, item_desc, at_code, at_desc, pos, m3dr_cr,mo1,mo2,mo3");


                    

                    //DataTable dt = db.QueryBySQLCode("SELECT * FROM (SELECT COALESCE(split_part(amount,',', (CASE WHEN split_part(mos,',', 1)=trim(month1) THEN 1 WHEN split_part(mos,',', 2)=trim(month1) THEN 2 WHEN split_part(mos,',', 3)=trim(month1) THEN 3 ELSE 9 END)),'0.00') AS amount1,COALESCE(split_part(amount,',', (CASE WHEN split_part(mos,',', 1)=trim(month2) THEN 1 WHEN split_part(mos,',', 2)=trim(month2) THEN 2 WHEN split_part(mos,',', 3)=trim(month2) THEN 3 ELSE 9 END)),'0.00') AS amount2, COALESCE(split_part(amount,',', (CASE WHEN split_part(mos,',', 1)=trim(month3) THEN 1 WHEN split_part(mos,',', 2)=trim(month3) THEN 2 WHEN split_part(mos,',', 3)=trim(month3) THEN 3 ELSE 9 END)),'0.00') AS amount3, * FROM (SELECT string_agg(moname,',') AS mos, string_agg((amount)::text,',') AS amount, j_num AS id1, t_desc AS name1, item_code AS id2, item_desc AS name2, at_code AS id3, at_desc AS name3, pos, m3dr_cr FROM (SELECT DISTINCT moname, j_num, t_desc, item_code, item_desc, at_code, at_desc, pos, m3dr_cr, SUM(amount) AS amount FROM( SELECT res.moname, COALESCE(res.item_code, m4.item_code) AS item_code, COALESCE(res.j_num, m4.j_num) AS j_num, COALESCE(res.t_desc,m4.t_desc) AS t_desc, COALESCE(res.item_desc,m4.item_desc) AS item_desc, COALESCE(res.at_code,m4.at_code) AS at_code, COALESCE(res.at_desc,m4.at_desc) AS at_desc,  COALESCE(res.m3dr_cr,m4.m4dr_cr) AS m3dr_cr, res.pos, res.amount AS amount FROM (SELECT  res.moname, CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.item_code END AS item_code, CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.j_num END AS j_num, CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.t_desc END AS t_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.item_desc END AS item_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.at_code END AS at_code,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.at_desc END AS at_desc,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN null ELSE res.m3dr_cr END AS m3dr_cr,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN 3 ELSE res.pos END AS pos,CASE WHEN (res.t_desc in ('INCOME','COST OF SALES','EXPENSES') OR res.t_desc like('%INCOME%')) THEN CASE WHEN res.m3dr_cr='D' THEN -1 * res.amount  WHEN res.m3dr_cr='C' THEN  res.amount ELSE 0 END ELSE res.amount END AS amount FROM (      	SELECT DISTINCT  t4._fy AS fy, t4._mo AS mo,t4._moname AS moname,  m4.name AS t_desc, initcap(m4.cmp_desc) AS item_desc, m4.at_code, m4.at_desc, m4.pos, m4.m3dr_cr, m4.j_num, m4.item_code, SUM(CASE WHEN m4.m4dr_cr='D' THEN  (debit-credit) WHEN m4.m4dr_cr='C' THEN (credit-debit) ELSE 0 END) as amount FROM (          " + pQry + "          ) t4 LEFT JOIN (SELECT m4.at_code, m4.at_desc, m3.*, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr, m3.code AS j_num, m3.cmp_code2 AS item_code   FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.*, m3.cmp_code AS cmp_code2 FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%'))    OR (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%'))     ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE COALESCE(m4.name,'')<>''  AND branch='001'      AND lower(m4.at_desc) not like ('%net income (loss)%')       GROUP BY m4.name, m4.cmp_desc, m4.at_code, m4.at_desc, m4.pos, m4.m3dr_cr, m4.j_num, m4.item_code, t4._fy, t4._mo,t4._moname ) res  ) res  LEFT JOIN                  (SELECT m3.code AS j_num, m3.cmp_code2 AS item_code, m3.name AS t_desc, m3.cmp_desc AS item_desc, m4.at_code, m4.at_desc, 1, m4.dr_cr AS m4dr_cr  FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.*, m2.cmp_code AS cmp_code2 FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code WHERE lower(at_desc) like '%net income (loss)%')      m4 ON (COALESCE(res.t_desc,'')='')) res GROUP BY moname,  j_num, item_code, t_desc, item_desc, at_code, at_desc, m3dr_cr, pos ORDER BY j_num, item_code, at_code )res GROUP BY j_num, t_desc, item_code, item_desc, at_code, at_desc, pos, m3dr_cr) res LEFT JOIN (SELECT (tdt.from)::date AS dt_begin, (tdt.to)::date AS dt_end, CASE WHEN _mos='0' THEN 'Beginning Balance' ELSE to_char((tdt.from)::date,'Month') END as month1, to_char(((tdt.from)::date + (tdt.mo2)::interval),'Month') as month2, to_char(((tdt.from)::date + (tdt.mo3)::interval),'Month') as month3 FROM " + dtftQry + " tdt) dt ON (1=1) ORDER BY id1, id2, id3) res WHERE ((COALESCE(amount1,'')<>'' AND amount1<>'0.00') OR (COALESCE(amount2,'')<>'' AND amount2<>'0.00') OR  (COALESCE(amount3,'')<>'' AND amount3<>'0.00')) ");


                    

                    //SELECT  m0.name AS name1, res.month1, res.month2, res.month3, COALESCE(res.amount1,0.00) AS amount1, COALESCE(res.amount2,0.00) AS amount2, COALESCE(res.amount3,0.00) AS amount3 FROM rssys.m00 m0 LEFT JOIN           (SELECT DISTINCT dt.month1, dt.month2, dt.month3, t4.name AS m2name, SUM((CASE WHEN t4.m4dr_cr='D' AND to_char(t4.sysdate2,'Month')=dt.month1 THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND to_char(t4.sysdate2,'Month')=dt.month1 THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount1, SUM((CASE WHEN t4.m4dr_cr='D' AND to_char(t4.sysdate2,'Month')=dt.month2 THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND to_char(t4.sysdate2,'Month')=dt.month2 THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount2, SUM((CASE WHEN t4.m4dr_cr='D' AND to_char(t4.sysdate2,'Month')=dt.month3 THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND to_char(t4.sysdate2,'Month')=dt.month3 THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount3 FROM(SELECT res.branch, res.from, sysdate2, res.m4dr_cr, res.amount, COALESCE(res.name,m4.t_desc) as name, CASE WHEN COALESCE(res.name,'')='' THEN 0 ELSE 1 END AS pos  FROM (SELECT  res.branch,res.from, sysdate2, res.m4dr_cr, CASE WHEN (res.name in ('INCOME','COST OF SALES','EXPENSES') OR res.name like('%INCOME%')) THEN CASE WHEN res.m4dr_cr='D' THEN res.amount  WHEN res.m4dr_cr='C' THEN -1 * res.amount ELSE 0 END ELSE res.amount END AS amount, CASE WHEN (res.name in ('INCOME','COST OF SALES','EXPENSES') OR res.name like('%INCOME%')) THEN null ELSE res.name END AS name FROM (         SELECT t4.from AS sysdate2, t4.*, m4.name, m4.m4dr_cr, CASE WHEN m4.m4dr_cr='D' THEN  (debit-credit) WHEN m4.m4dr_cr='C' THEN (credit-debit) ELSE 0 END as amount FROM " + pQry + " t4 LEFT JOIN (SELECT m4.at_code, m4.at_desc, m3.*, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr  FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.* FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT * FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%'))       OR (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%'))           ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE COALESCE(m4.name,'')<>''     AND lower(m4.at_desc) not like ('%net income (loss)%')     ) res ) res LEFT JOIN (SELECT m3.name AS t_desc, m3.cmp_desc AS item_desc, m4.at_code, m4.at_desc, 1, m4.dr_cr AS m4dr_cr FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.* FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code WHERE lower(at_desc) like '%net income (loss)%') m4 ON (COALESCE(res.name,'')='')            ) t4 LEFT JOIN(SELECT (tdt.from)::date AS dt_begin, (tdt.to)::date AS dt_end, to_char((tdt.from)::date,'Month') as month1, to_char(((tdt.from)::date + ('1 month')::interval),'Month') as month2, to_char(((tdt.from)::date + ('2 month')::interval),'Month') as month3 FROM " + dtftQry + " tdt ) dt ON (t4.from BETWEEN dt.dt_begin AND dt.dt_end) WHERE 1=1 " + WHERE + " GROUP BY dt.month1, dt.month2, dt.month3, t4.name) res ON res.m2name=m0.name WHERE  (m0.name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%')) Order By name1
                    //

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_3) + ")");
                    add_fieldparam("t_date", (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_1).Split('-')[1] ?? "")) + "-" + (get_cbo_text(cbo_2).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_2).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_2).Split('-')[1] ?? "")) + " " + (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? fy : ""));

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A502y")
                {
                    if (get_cbo_value(cbo_1) == "")
                    {
                        MessageBox.Show("Please select a fy.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_2) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }
                    String fy = get_cbo_value(cbo_1).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "", dtftQry = "";
                    if (get_cbo_index(cbo_2) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_2) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_2) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    pQry = "(SELECT t4.*, x3.from FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 WHERE fy='" + fy + "') x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    myReportDocument.Load(fileloc_acctg + "502_balance_sheet_compy.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT  m0.name AS name1, COALESCE(amnt_begin,0.00) AS amnt_begin,COALESCE(amount1,0.00) AS amount1,COALESCE(amount2,0.00) AS amount2,COALESCE(amount3,0.00) AS amount3,COALESCE(amount4,0.00) AS amount4,COALESCE(amount5,0.00) AS amount5,COALESCE(amount6,0.00) AS amount6,COALESCE(amount7,0.00) AS amount7,COALESCE(amount8,0.00) AS amount8,COALESCE(amount9,0.00) AS amount9,COALESCE(amount10,0.00) AS amount10,COALESCE(amount11,0.00) AS amount11,COALESCE(amount12,0.00) AS amount12 FROM rssys.m00 m0 LEFT JOIN (SELECT DISTINCT t4.name AS m2name,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo=0 THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo=0 THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amnt_begin,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='January' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='January' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount1,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='February' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='February' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount2,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='March' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='March' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount3,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='April' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='April' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount4,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='May' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='May' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount5,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='June' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='June' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount6,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='July' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='July' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount7,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='August' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='August' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount8,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='September' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='September' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount9,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='October' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='October' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount10,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='November' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='November' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount11,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='December' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='December' THEN CASE WHEN t4.pos=1 THEN t4.amount ELSE -1*t4.amount END ELSE 0.00 END)) AS amount12 FROM (         (SELECT res.mo, res.branch, res.from, sysdate2, res.m4dr_cr, res.amount, COALESCE(res.name,m4.t_desc) as name, CASE WHEN COALESCE(res.name,'')='' THEN 0 ELSE 1 END AS pos  FROM (SELECT res.mo2 AS mo, res.branch,res.from, sysdate2, res.m4dr_cr, CASE WHEN (res.name in ('INCOME','COST OF SALES','EXPENSES') OR res.name like('%INCOME%')) THEN CASE WHEN res.m4dr_cr='D' THEN res.amount  WHEN res.m4dr_cr='C' THEN -1 * res.amount ELSE 0 END ELSE res.amount END AS amount, CASE WHEN (res.name in ('INCOME','COST OF SALES','EXPENSES') OR res.name like('%INCOME%')) THEN null ELSE res.name END AS name FROM (         SELECT  t4.mo AS mo2, t4.from AS sysdate2, t4.*, m4.name, m4.m4dr_cr, CASE WHEN m4.m4dr_cr='D' THEN  (debit-credit) WHEN m4.m4dr_cr='C' THEN (credit-debit) ELSE 0 END as amount FROM " + pQry + " t4 LEFT JOIN (SELECT m4.at_code, m4.at_desc, m3.*, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr  FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.* FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT * FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%'))       OR (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%'))           ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE COALESCE(m4.name,'')<>''     AND lower(m4.at_desc) not like ('%net income (loss)%')     ) res ) res LEFT JOIN (SELECT m3.name AS t_desc, m3.cmp_desc AS item_desc, m4.at_code, m4.at_desc, 1, m4.dr_cr AS m4dr_cr FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.* FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name='ASSETS' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code WHERE lower(at_desc) like '%net income (loss)%') m4 ON (COALESCE(res.name,'')='')            )        ) t4 WHERE  COALESCE(t4.name,'')<>''  AND t4.branch='001'  GROUP BY t4.name) res ON res.m2name=m0.name WHERE (m0.name in ('ASSETS','LIABILITIES','EQUITY') OR name like ('%EQUITY%')) Order By m0.name");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_2) + ")");
                    add_fieldparam("t_date", "January - December");

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A503l")
                {
                    if (get_cbo_value(cbo_1) == "")
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_value(cbo_2) == "")
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }
                    String fy = ((String)get_cbo_value(cbo_1).Split('-').GetValue(0)).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "";
                    if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT DISTINCT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT DISTINCT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT DISTINCT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }


                    dtQry = "(SELECT DISTINCT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from,sx3._to  FROM ( SELECT DISTINCT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";


                    pQry = "(SELECT DISTINCT t4.* FROM " + trQry + " t4 JOIN (SELECT DISTINCT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to) AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    String file = fileloc_acctg + "503l_incomestatement.rpt";
                    if (ischkbox_checked(chk_1))
                    {
                        file = fileloc_acctg + "503l_incomestatement_budget.rpt";
                    }

                    myReportDocument.Load(file);
                    // modified again by: Reancy 06 26 2018
                    // modified by: Reancy 06 06 2018
                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT m4.code AS j_num, m4.name AS t_desc, m4.cmp_code AS item_code, initcap(m4.cmp_desc) AS item_desc, m4.at_code, m4.at_desc, m4.pos, m4.m4dr_cr, SUM(CASE WHEN m4.m4dr_cr='C' THEN credit - debit ELSE debit - credit END) as amount, COALESCE((SELECT SUM(budget)::numeric(20,2) FROM rssys.budget WHERE at_code = m4.at_code AND (fy='" + fy + "')), 0.00) AS bal_begin FROM    " + pQry + "     t4 LEFT JOIN (SELECT DISTINCT m4.at_code, m4.at_desc, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr, m3.name, m3.code, m3.cmp_desc, m3.cmp_code2 as cmp_code, m3.pos  FROM rssys.m04 m4 LEFT JOIN (SELECT DISTINCT m3.*, m2.*, m0.name, m0.code, m2.cmp_code AS cmp_code2, m0.pos FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT DISTINCT *,(CASE WHEN name LIKE '%INCOME%' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE 1=1 AND COALESCE(m4.name,'')<>'' GROUP BY m4.code,m4.cmp_code, m4.name, m4.cmp_desc, m4.at_code, m4.at_desc, m4.pos, m4.m4dr_cr HAVING SUM(CASE WHEN m4.m4dr_cr='C' THEN credit - debit ELSE debit - credit END)<>0 ORDER BY m4.code, m4.cmp_code, m4.at_code");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_3) + ")");
                    add_fieldparam("t_date", (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_1).Split('-')[1] ?? "")) + "-" + (get_cbo_text(cbo_2).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_2).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_2).Split('-')[1] ?? "")) + " " + (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? fy : ""));

                    disp_reportviewer(myReportDocument);
                }

                else if (rpt_no == "A503m3")
                {
                    //this.Text = "Balance Sheet";
                    if (get_cbo_value(cbo_1) == "")
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_value(cbo_2) == "")
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }
                    String[]splt = get_cbo_value(cbo_1).Split('-');
                    String fy = ((String)splt.GetValue(0)).Trim();
                    String mo = ((String)splt.GetValue(1)).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "", dtftQry = "";
                    String mon2 = "0", fyn2;
                    int _mon2 = Convert.ToInt32(mo) + 1;
                    mon2 = (_mon2 == 13 ? mon2 : _mon2.ToString("0"));
                    fyn2 = (Convert.ToInt32(fy) + (_mon2 == 13 ? 1 : 0)).ToString("0");

                    if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    dtftQry = "(SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, mos._mos, mos.mo2, mos.mo3, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to  FROM rssys.x03 x3 LEFT JOIN   (SELECT '" + mo + "'::text AS _mos, (CASE WHEN '" + mo + "'='0' THEN '0' ELSE '1' END)||' month' AS mo2,(CASE WHEN '" + mo + "'='0' THEN '1' ELSE '2' END)||' month' AS mo3) mos  ON (1=1) GROUP BY mos._mos, mos.mo2, mos.mo3)";

                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from,sx3._to FROM " + dtftQry + " sx3)";

                    pQry = "(SELECT *, x3.fy AS _fy, x3.mo AS _mo, x3.month_desc AS moname FROM " + trQry + " t4 JOIN (SELECT x3.* FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)    AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to     ) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    myReportDocument.Load(fileloc_acctg + "503_incomestatement_comp.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM (SELECT COALESCE(split_part(amount,',', (CASE WHEN split_part(mos,',', 1)=trim(month1) THEN 1 WHEN split_part(mos,',', 2)=trim(month1) THEN 2 WHEN split_part(mos,',', 3)=trim(month1) THEN 3 ELSE 9 END)),'0.00') AS amount1,COALESCE(split_part(amount,',', (CASE WHEN split_part(mos,',', 1)=trim(month2) THEN 1 WHEN split_part(mos,',', 2)=trim(month2) THEN 2 WHEN split_part(mos,',', 3)=trim(month2) THEN 3 ELSE 9 END)),'0.00') AS amount2, COALESCE(split_part(amount,',', (CASE WHEN split_part(mos,',', 1)=trim(month3) THEN 1 WHEN split_part(mos,',', 2)=trim(month3) THEN 2 WHEN split_part(mos,',', 3)=trim(month3) THEN 3 ELSE 9 END)),'0.00') AS amount3, * FROM (SELECT string_agg(moname,',') AS mos, string_agg((amount)::text,',') AS amount, j_num AS id1, t_desc AS name1, item_code AS id2, item_desc AS name2, at_code AS id3, at_desc AS name3, pos, m4dr_cr FROM ( SELECT DISTINCT moname, m4.code AS j_num, m4.name AS t_desc, m4.cmp_code AS item_code, initcap(m4.cmp_desc) AS item_desc, m4.at_code, m4.at_desc, m4.pos, m4.m4dr_cr, SUM(CASE WHEN m4.m4dr_cr='C' THEN credit - debit ELSE debit - credit END) as amount, SUM(COALESCE(b.budget,0.00)) AS bal_begin FROM   " + pQry + "    t4 LEFT JOIN (SELECT m4.at_code, m4.at_desc, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr, m3.name, m3.code, m3.cmp_desc, m3.cmp_code2 as cmp_code, m3.pos  FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.name, m0.code, m2.cmp_code AS cmp_code2, m0.pos FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name LIKE '%INCOME%' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code LEFT JOIN rssys.budget b ON (b.fy=t4._fy AND b.mo=t4._mo AND b.at_code=t4.at_code) WHERE 1=1 AND COALESCE(m4.name,'')<>'' GROUP BY moname, m4.code,m4.cmp_code, m4.name, m4.cmp_desc, m4.at_code, m4.at_desc, m4.pos, m4.m4dr_cr ORDER BY m4.code, m4.cmp_code, m4.at_code )res GROUP BY j_num, t_desc, item_code, item_desc, at_code, at_desc, pos, m4dr_cr ) res LEFT JOIN (SELECT (tdt.from)::date AS dt_begin, (tdt.to)::date AS dt_end, CASE WHEN _mos='0' THEN 'Beginning Balance' ELSE to_char((tdt.from)::date,'Month') END as month1, to_char(((tdt.from)::date + (tdt.mo2)::interval),'Month') as month2, to_char(((tdt.from)::date + (tdt.mo3)::interval),'Month') as month3 FROM " + dtftQry + " tdt) dt ON (1=1)  ORDER BY id1, id2, id3) res WHERE ((COALESCE(amount1,'')<>'' AND amount1<>'0.00') OR (COALESCE(amount2,'')<>'' AND amount2<>'0.00') OR  (COALESCE(amount3,'')<>'' AND amount3<>'0.00')) ");
                    /*
                    if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT fy||'-'||LPAD(mo::text,3,'0') AS fymo, fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT fy||'-'||LPAD(mo::text,3,'0') AS fymo, fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT fy||'-'||LPAD(mo::text,3,'0') AS fymo, fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy||'-'||LPAD(mo::text,3,'0') AS fymo, fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    String begbalYr = "2017-0"; // " + fy + "-0 " + get_cbo_value(cbo_1) + " " + get_cbo_value(cbo_1) + "
                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from,sx3._to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + begbalYr + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + begbalYr + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + begbalYr + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";


                    pQry = "(SELECT t4.* FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to) AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    myReportDocument.Load(fileloc_acctg + "503_incomestatement_comp.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT  DISTINCT j_num AS id1, t_desc AS name1, item_code AS id2, item_desc AS name2, at_code AS id3, at_desc AS name3, pos, m4dr_cr, SUM(CASE WHEN fymo<=_mo1 THEN amount ELSE 0 END) AS amount1, SUM(CASE WHEN fymo<=_mo2 THEN amount ELSE 0 END) AS amount2, SUM(CASE WHEN fymo<=_mo3 THEN amount ELSE 0 END) AS amount3, mo1 AS month1, mo2 AS month2, mo3 AS month3 FROM (  SELECT DISTINCT m4.code AS j_num, m4.name AS t_desc, m4.cmp_code AS item_code, initcap(m4.cmp_desc) AS item_desc, m4.at_code, m4.at_desc, m4.pos, m4.m4dr_cr, SUM(CASE WHEN m4.m4dr_cr='C' THEN credit - debit ELSE debit - credit END) as amount, SUM(COALESCE(b.budget,0.00)) AS bal_begin, fymo FROM    " + pQry + "     t4 LEFT JOIN (SELECT m4.at_code, m4.at_desc, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr, m3.name, m3.code, m3.cmp_desc, m3.cmp_code2 as cmp_code, m3.pos  FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.name, m0.code, m2.cmp_code AS cmp_code2, m0.pos FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name LIKE '%INCOME%' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code LEFT JOIN rssys.budget b ON (b.fy=t4.fy AND b.mo=t4.mo AND b.at_code=t4.at_code) WHERE 1=1 AND COALESCE(m4.name,'')<>'' GROUP BY m4.code,m4.cmp_code, m4.name, m4.cmp_desc, m4.at_code, m4.at_desc, m4.pos, m4.m4dr_cr, fymo HAVING SUM(CASE WHEN m4.m4dr_cr='C' THEN credit - debit ELSE debit - credit END)<>0 ORDER BY m4.code, m4.cmp_code, m4.at_code) res JOIN (SELECT _mo1,_mo2,_mo3, mo1, CASE WHEN COALESCE(_mo2,'')='' THEN TO_CHAR(mo+'1 month'::interval,'Month') ELSE mo2 END AS mo2, CASE WHEN COALESCE(_mo3,'')='' THEN TO_CHAR(mo+'2 month'::interval,'Month') ELSE mo3 END AS mo3 FROM (SELECT x3.mo, _mo1, COALESCE(fy||'-'||LPAD(sx3.mo::text,3,'0'),'') AS _mo2,  CASE WHEN _mo1=_mo3 THEN '' ELSE _mo3 END AS _mo3, mo1, month_desc AS mo2, mo3 FROM (SELECT DISTINCT sx3.mo2, min(fy||'-'||LPAD(mo::text,3,'0')) AS _mo1, max(fy||'-'||LPAD(mo::text,3,'0')) AS _mo3, min(x3.from) AS mo, string_agg(CASE WHEN mo1=x3.fy||'-'||x3.mo THEN month_desc ELSE '' END,'') AS mo1, string_agg(CASE WHEN mo3=x3.fy||'-'||x3.mo THEN month_desc ELSE '' END,'') AS mo3 FROM rssys.x03 x3 JOIN (SELECT '" + get_cbo_value(cbo_1) + "'::text AS mo1, '" + fyn2 + "-" + mon2 + "'::text AS mo2, '" + get_cbo_value(cbo_2) + "'::text AS mo3) sx3 ON (x3.fy||'-'||x3.mo IN (sx3.mo1, sx3.mo3)) GROUP BY sx3.mo1, sx3.mo2, sx3.mo3) x3 LEFT JOIN rssys.x03 sx3 ON (x3.mo2=sx3.fy||'-'||sx3.mo)) x3) x3 ON (1=1) GROUP BY j_num, t_desc, item_code, item_desc, at_code, at_desc, pos, m4dr_cr, mo1, mo2, mo3");

                    */
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_3) + ")");
                    add_fieldparam("t_date", (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_1).Split('-')[1] ?? "")) + "-" + (get_cbo_text(cbo_2).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_2).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_2).Split('-')[1] ?? "")) + " " + (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? fy : ""));

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A503")
                {
                    //this.Text = "Income Statement Report";
                    if (get_cbo_index(cbo_1) == -1)
                    {
                        MessageBox.Show("Please select a period from.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_2) == -1)
                    {
                        MessageBox.Show("Please select a period to.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_3) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_3, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }
                    String fy = ((String)get_cbo_value(cbo_1).Split('-').GetValue(0)).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "";
                    if (get_cbo_index(cbo_3) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_3) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_3) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_1) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(cbo_2) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to FROM rssys.x03 x3) sx3)";

                    pQry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to)) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    myReportDocument.Load(fileloc_acctg + "503_incomestatement.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT COALESCE(SUM(CASE WHEN m4.name LIKE '%INCOME%' AND m4.m4dr_cr='D' THEN debit - credit WHEN m4.name LIKE '%INCOME%' AND m4.m4dr_cr='C' THEN credit - debit ELSE 0.00 END),0.00) as  credit, COALESCE(SUM(CASE WHEN m4.name IN ('EXPENSES','COST OF SALES') AND m4.m4dr_cr='D' THEN debit - credit WHEN m4.name IN ('EXPENSES','COST OF SALES') AND m4.m4dr_cr='C' THEN credit - debit ELSE 0.00 END),0.00) as  debit FROM " + pQry + " t4 LEFT JOIN (SELECT m4.at_code, m4.at_desc, m3.*, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr  FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.name, m0.code, m0.pos FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT *,(CASE WHEN name LIKE '%INCOME%' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%'))) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE COALESCE(m4.name,'')<>'' " + WHERE + " ");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_2) + ")");
                    add_fieldparam("t_date", (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_1).Split('-')[1] ?? "")) + "-" + (get_cbo_text(cbo_2).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_2).ToLower().IndexOf("bal") >= 0 ? "Beg Bal" : (get_cbo_text(cbo_2).Split('-')[1] ?? "")) + " " + (get_cbo_text(cbo_1).ToLower().IndexOf("beg") >= 0 && get_cbo_text(cbo_1).ToLower().IndexOf("bal") >= 0 ? fy : ""));

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A503y" || rpt_no == "A503cc")
                {
                    if (get_cbo_value(cbo_1) == "")
                    {
                        MessageBox.Show("Please select a fy.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }
                    if (get_cbo_index(cbo_2) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_2, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);
                    if (get_cbo_index(cbo_4) != -1)
                    {
                        WHERE = " AND branch='" + get_cbo_value(cbo_4) + "' ";
                    }
                    String fy = get_cbo_value(cbo_1).Trim();
                    String pQry = "", p2Qry = "";
                    String trQry = "", dtQry = "", dtftQry = "";
                    if (get_cbo_index(cbo_2) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_2) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }
                    else if (get_cbo_index(cbo_2) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
                    }

                    pQry = "(SELECT t4.*, x3.from FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 WHERE fy='" + fy + "') x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    String fname = "503_incomestatement_compy.rpt";
                    if (rpt_no == "A503cc")
                    {
                        fname = "503_incomestatement_compccy.rpt";
                    }
                    myReportDocument.Load(fileloc_acctg + fname);
                    DataTable dt = db.QueryBySQLCode("SELECT name AS name1, m8.cc_desc AS name2, at_code AS id1, t_desc AS desc1,COALESCE(amount1,0.00) AS amount1,COALESCE(amount2,0.00) AS amount2,COALESCE(amount3,0.00) AS amount3,COALESCE(amount4,0.00) AS amount4,COALESCE(amount5,0.00) AS amount5,COALESCE(amount6,0.00) AS amount6,COALESCE(amount7,0.00) AS amount7,COALESCE(amount8,0.00) AS amount8,COALESCE(amount9,0.00) AS amount9,COALESCE(amount10,0.00) AS amount10,COALESCE(amount11,0.00) AS amount11,COALESCE(amount12,0.00) AS amount12 FROM (SELECT DISTINCT t4.name, t4.at_code, t4.t_desc, t4.cc_code,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='January' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='January' THEN credit-debit ELSE 0.00 END)) AS amount1,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='February' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='February' THEN credit-debit ELSE 0.00 END)) AS amount2,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='March' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='March' THEN credit-debit ELSE 0.00 END)) AS amount3,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='April' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='April' THEN credit-debit ELSE 0.00 END)) AS amount4,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='May' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='May' THEN credit-debit ELSE 0.00 END)) AS amount5,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='June' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='June' THEN credit-debit ELSE 0.00 END)) AS amount6,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='July' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='July' THEN credit-debit ELSE 0.00 END)) AS amount7,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='August' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='August' THEN credit-debit ELSE 0.00 END)) AS amount8,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='September' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='September' THEN credit-debit ELSE 0.00 END)) AS amount9,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='October' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='October' THEN credit-debit ELSE 0.00 END)) AS amount10,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='November' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='November' THEN credit-debit ELSE 0.00 END)) AS amount11,SUM((CASE WHEN t4.m4dr_cr='D' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='December' THEN debit-credit WHEN t4.m4dr_cr='C' AND t4.mo<>0 AND trim(to_char(t4.sysdate2,'Month'))='December' THEN credit-debit ELSE 0.00 END)) AS amount12 FROM (SELECT t4.from AS sysdate2, t4.*, m4.name, m4.m4dr_cr FROM " + pQry + " t4 LEFT JOIN (SELECT m4.at_code, m4.at_desc, m3.*, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr FROM rssys.m04 m4 LEFT JOIN (SELECT m3.*, m2.*, m0.* FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT * FROM rssys.m00 WHERE (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE  COALESCE(m4.name,'')<>''  AND t4.branch='" + get_cbo_value(cbo_4) + "' ) t4 GROUP BY t4.name, t4.at_code, t4.t_desc, t4.cc_code) res LEFT JOIN rssys.m08 m8 ON m8.cc_code=res.cc_code  ORDER BY " + (rpt_no == "A503y" ? "name," : "cc_desc,") + " at_code");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("fy", fy + "(" + get_cbo_text(cbo_2) + ")");
                    add_fieldparam("t_date", "January - December");

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "A601")
                {

                    if (get_cbo_index(cbo_1) == -1)
                    {
                        MessageBox.Show("Please select a view.");
                        set_cbo_droppeddown(cbo_1, true);
                        reset();
                        return;
                    }

                    inc_pbar(10);

                    String dtfrm = dtp_frm.Value.ToString("yyyy-MM-dd"), dtto = dtp_to.Value.ToString("yyyy-MM-dd");
                    String trQry = "", trsubQry = "";

                    if (get_cbo_index(cbo_1) == 0)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";

                        trsubQry = "(SELECT DISTINCT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, jo_code, purc_ord, pr_code, dr_code FROM rssys.tr04)";
                    }
                    else if (get_cbo_index(cbo_1) == 1)
                    {
                        trQry = "(SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";

                        trsubQry = "rssys.tr01";
                    }
                    else if (get_cbo_index(cbo_1) == 2)
                    {
                        trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";

                        trsubQry = "(SELECT DISTINCT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, jo_code, purc_ord, pr_code, dr_code FROM rssys.tr01 UNION ALL SELECT DISTINCT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, jo_code, purc_ord, pr_code, dr_code FROM rssys.tr04 ORDER BY fy, mo)";
                    }

                    myReportDocument.Load(fileloc_acctg+ "601_daily_collection_report.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT res.*, CASE WHEN COALESCE(t2.sl_code,'')<>'' THEN t2.sl_name ELSE res._tenants END AS tenants, CASE WHEN POSITION(';' IN _invoices)=1 THEN SUBSTRING(_invoices,2) ELSE _invoices END AS invoices  FROM (SELECT jo_code AS po_num, purc_ord AS ur_num, pr_code AS ar_num, dr_code AS dr_num, t_date, ck_num, CASE WHEN COALESCE(ck_num,'')<>'' THEN TO_CHAR(ck_date,'MM/DD/YYYY') ELSE '' END AS ck_date, t4.j_code, t4.j_num, string_agg(tenant,'; ') AS _tenants, string_agg(COALESCE(rom_code,''),CASE WHEN COALESCE(rom_code,'')<>'' THEN '; ' ELSE '' END) AS rom_code, string_agg(COALESCE(chg_desc,''),CASE WHEN COALESCE(chg_desc,'')<>'' THEN '; ' ELSE '' END) AS particular, string_agg(COALESCE(invoices,''),CASE WHEN COALESCE(invoices,'')<>'' THEN '; ' ELSE '' END) AS _invoices, SUM(soa_amnt) AS soa_amnt, SUM(nosoa_amnt) AS nosoa_amnt, SUM(other_amnt) AS other_amnt  FROM (SELECT t4.j_code,t4.j_num, CASE WHEN COALESCE(t2x.rom_code,'')<>'' THEN t4.sl_name ELSE t4.at_desc END tenant, t2x.rom_code, t2x.chg_desc, string_agg(COALESCE(t4.invoice,''),CASE WHEN COALESCE(t4.invoice,'')<>'' THEN '; ' ELSE '' END) AS invoices, SUM(CASE WHEN COALESCE(t2x.invoice,'')<>'' THEN t4.credit ELSE 0 END) AS soa_amnt, SUM(CASE WHEN COALESCE(t2x.invoice,'')='' AND COALESCE(t4.invoice,'')<>'' THEN t4.credit ELSE 0 END) AS nosoa_amnt, SUM(CASE WHEN COALESCE(t4.invoice,'')='' THEN t4.credit ELSE 0 END) AS other_amnt FROM (SELECT t4.*, m4.at_desc FROM " + trQry + " t4 JOIN (SELECT j_code FROM rssys.m05 m5 JOIN rssys.m05type m5t ON m5t.code=m5.j_type WHERE name='Collection') m5 ON m5.j_code=t4.j_code LEFT JOIN rssys.m04 m4 ON m4.at_code=t4.at_code ORDER BY j_code, j_num DESC, invoice DESC) t4 LEFT JOIN rssys.tr02_ext t2x ON t2x.invoice=t4.invoice WHERE t4.credit>0 GROUP BY t4.j_code,t4.j_num, t2x.rom_code, t2x.chg_desc, t4.sl_name, t4.at_desc  ORDER BY rom_code , invoices, j_code DESC, j_num DESC) t4 JOIN " + trsubQry + " t1 ON (t1.j_code=t4.j_code AND t1.j_num=t4.j_num AND (t1.t_date BETWEEN '" + dtfrm + "' AND '" + dtto + "')) GROUP BY jo_code, purc_ord, pr_code, dr_code, t_date, ck_num, ck_date, t4.j_code, t4.j_num) res       LEFT JOIN (SELECT DISTINCT j_num, j_code, sl_code, sl_name FROM rssys.tr02 WHERE COALESCE(sl_code,'')<>'' ORDER BY sl_code, sl_name) t2 ON (t2.j_code=res.j_code AND t2.j_num=res.j_num)          ORDER BY t_date, j_code, j_num");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("t_date", dtfrm + " to "+ dtto);

                    disp_reportviewer(myReportDocument);
                }

                    ////// soa summary

                else if (rpt_no == "A701")
                {
                    String soa_period = "";

                    if(get_cbo_index( cbo_1) != -1)
                    {
                        soa_period = get_cbo_value(cbo_1);
                    }
                    
                    inc_pbar(10);                

                    myReportDocument.Load(fileloc_acctg + "soa_summary.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT s.soa_code AS soa_num, s.debt_name, s.soa_period, s.due_date, SUM(sl.amount) AS amount, s.user_id FROM rssys.soahdr s LEFT JOIN rssys.soalne sl ON s.soa_code=sl.soa_code WHERE s.soa_period='" + soa_period + "' GROUP BY s.soa_code, s.debt_name, s.soa_period, s.due_date, s.user_id ORDER BY s.soa_code;");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("m99company", comp_name);
                    add_fieldparam("m99address", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("branch", get_cbo_text(cbo_4));
                    add_fieldparam("soa_period", soa_period);

                    disp_reportviewer(myReportDocument);
                }


                    ////

                else if (rpt_no == "I011")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "101_pr_by_date.rpt");
                    DataTable dt = db.QueryBySQLCode("SELECT to_char(p.pr_date, 'MM/dd/yyyy') AS pr_date, p.reference, p.request_by, pl.item_code, pl.item_desc, u.unit_shortcode, pl.quantity  FROM " + db.get_schema() + ".prhdr p LEFT JOIN " + db.get_schema() + ".prlne pl ON p.pr_code=pl.pr_code LEFT JOIN " + db.get_schema() + ".itmunit u ON u.unit_id=pl.purc_unit  WHERE p.pr_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY p.pr_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("dt_frm", dtpicker_frm);
                    add_fieldparam("dt_to", dtpicker_to);

                    disp_reportviewer(myReportDocument);
                }

                //purchase request
                else if (rpt_no == "I011")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "101_pr_by_date.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT to_char(p.pr_date, 'MM/dd/yyyy') AS pr_date, p.reference, p.request_by, pl.item_code, pl.item_desc, u.unit_shortcode, pl.quantity  FROM " + db.get_schema() + ".prhdr p LEFT JOIN " + db.get_schema() + ".prlne pl ON p.pr_code=pl.pr_code LEFT JOIN " + db.get_schema() + ".itmunit u ON u.unit_id=pl.purc_unit  WHERE p.pr_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY p.pr_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("dt_frm", dtpicker_frm);
                    add_fieldparam("dt_to", dtpicker_to);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I012")
                {

                    String code1 = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND p.pr_code >= '" + get_cbo_value(cbo_1) + "'";
                        code1 = get_cbo_value(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND p.pr_code <= '" + get_cbo_value(cbo_2) + "'";
                        code1 = "First To " + get_cbo_value(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND p.pr_code BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "' ";
                        code1 = get_cbo_value(cbo_1) + " To " + get_cbo_value(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            code1 = get_cbo_value(cbo_1);
                        }
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "102_pr_by_number.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT p.*, pl.*, cc.cc_desc AS c_name, scc.scc_desc AS scc_desc FROM " + db.get_schema() + ".prhdr p LEFT JOIN " + db.get_schema() + ".prlne pl ON p.pr_code=pl.pr_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=p.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=p.scc_code WHERE p.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY p.t_date");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("code1", code1);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I013")
                {
                    String item = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND pl.item_code='" + get_cbo_value(cbo_1) + "'";
                        item = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "103_pr_by_item.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT p.*, pl.* FROM " + db.get_schema() + ".prhdr p LEFT JOIN " + db.get_schema() + ".prlne pl ON p.pr_code=pl.pr_code  WHERE p.pr_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY p.pr_date");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("item", item);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);

                }
                else if (rpt_no == "I014")
                {

                    String costcenter = "All";
                    String scc_frmto = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND p.scc_code >= '" + get_cbo_value(cbo_2) + "'";
                        scc_frmto = get_cbo_text(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE = "AND p.scc_code <= '" + get_cbo_value(cbo_3) + "'";
                        scc_frmto = "First To " + get_cbo_text(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE = "AND (p.scc_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        scc_frmto = get_cbo_text(cbo_2) + " To " + get_cbo_text(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            scc_frmto = get_cbo_text(cbo_2);
                        }
                    }

                    String WHERE2 = "";
                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE2 = " AND p.cnt_code='" + get_cbo_value(cbo_1) + "'";
                        costcenter = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }


                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "104_pr_by_scc.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT p.pr_code, p.reference, to_char(p.pr_date, 'MM/dd/yyyy') AS pr_date, pl.item_code, pl.item_desc, pl.quantity, u.unit_shortcode AS purc_unit, s.scc_desc AS scc_desc FROM " + db.get_schema() + ".prhdr p LEFT JOIN " + db.get_schema() + ".prlne pl ON p.pr_code=pl.pr_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=p.cnt_code LEFT JOIN " + db.get_schema() + ".subctr s ON s.scc_code=p.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON u.unit_id=pl.purc_unit WHERE p.pr_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' " + (WHERE == "WHERE" ? "" : WHERE) + WHERE2 + " ORDER BY p.pr_date, p.scc_code, pr_code");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("cc_desc1", costcenter);
                    add_fieldparam("cc_desc2", scc_frmto);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }

                //Purchase Order
                else if (rpt_no == "I021")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "201_po_by_date.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT p.purc_ord, m7.c_name, p.reference, to_char(p.rels_date, 'MM/dd/yyyy') AS t_date, to_char(p.delv_date, 'MM/dd/yyyy') AS delv_date, m10.mp_desc, SUM(pl.ln_amnt) AS po_amnt FROM " + db.get_schema() + ".purhdr p LEFT JOIN " + db.get_schema() + ".purlne pl ON p.purc_ord=pl.purc_ord LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=p.supl_code LEFT JOIN " + db.get_schema() + ".m10 m10 ON  m10.mp_code=p.pay_code WHERE p.rels_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' GROUP BY p.purc_ord, m7.c_name, p.reference, p.rels_date, p.delv_date, m10.mp_desc ORDER BY p.rels_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("dt_frm", dtpicker_frm);
                    add_fieldparam("dt_to", dtpicker_to);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I022")
                {

                    String code1 = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND p.purc_ord  >= '" + get_cbo_value(cbo_1) + "'";
                        code1 = get_cbo_text(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND p.purc_ord  <= '" + get_cbo_value(cbo_2) + "'";
                        code1 = "First To " + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND p.purc_ord BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "'";
                        code1 = get_cbo_text(cbo_1) + " To " + get_cbo_text(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            code1 = get_cbo_text(cbo_1);
                        }
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "202_po_by_number.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT p.purc_ord, p.reference, to_char(p.rels_date, 'MM/dd/yyyy') AS t_date, m7.c_name AS supl_name, m10.mp_desc AS pay_code, pl.ln_num, pl.item_code, pl.item_desc, pl.price, u.unit_shortcode AS purc_unit, pl.discount, pl.ln_amnt FROM " + db.get_schema() + ".purhdr p LEFT JOIN " + db.get_schema() + ".purlne pl ON p.purc_ord=pl.purc_ord LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=p.supl_code LEFT JOIN " + db.get_schema() + ".m10 m10 ON m10.mp_code=p.pay_code LEFT JOIN " + db.get_schema() + ".itmunit u ON pl.purc_unit=u.unit_id WHERE p.rels_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "'  " + (WHERE == "WHERE" ? "" : WHERE) + "  ORDER BY  p.rels_date, p.purc_ord ASC) res WHERE ln_amnt<>0");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("code1", code1);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);
                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I023")
                {

                    String supplier = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "p.supl_code='" + get_cbo_value(cbo_1) + "' AND";
                        supplier = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "203_po_by_supplier.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT p.purc_ord, p.reference, to_char(p.rels_date, 'MM/dd/yyyy') AS t_date, m7.c_name AS supl_name, m10.mp_desc, pl.ln_num, pl.item_code, pl.item_desc, pl.price, u.unit_shortcode AS purc_unit, pl.delv_qty as rels_qty,pl.discount, pl.price, pl.ln_amnt FROM " + db.get_schema() + ".purhdr p LEFT JOIN " + db.get_schema() + ".purlne pl ON p.purc_ord=pl.purc_ord LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=p.supl_name LEFT JOIN " + db.get_schema() + ".m10 m10 ON m10.mp_code=p.pay_code LEFT JOIN " + db.get_schema() + ".itmunit u ON pl.purc_unit=u.unit_id WHERE " + (WHERE == "WHERE" ? "" : WHERE) + " p.rels_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY p.rels_date, p.purc_ord ASC) res WHERE ln_amnt<>0");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("supplier", supplier);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);

                }
                else if (rpt_no == "I024")
                {

                    String item = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "pl.item_code='" + get_cbo_value(cbo_1) + "' AND";
                        item = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "204_po_by_item.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT p.purc_ord, p.reference, to_char(p.rels_date, 'MM/dd/yyyy') AS t_date, m7.c_name , m10.mp_desc AS pay_code, pl.ln_num, pl.item_code, pl.item_desc, pl.price, u.unit_shortcode AS purc_unit, pl.discount, pl.price, pl.ln_amnt,pl.delv_qty as rels_qty FROM " + db.get_schema() + ".purhdr p LEFT JOIN " + db.get_schema() + ".purlne pl ON p.purc_ord=pl.purc_ord LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=p.supl_code LEFT JOIN " + db.get_schema() + ".m10 m10 ON m10.mp_code=p.pay_code LEFT JOIN " + db.get_schema() + ".itmunit u ON pl.purc_unit=u.unit_id WHERE " + (WHERE == "WHERE" ? "" : WHERE) + " p.rels_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY p.rels_date,p.purc_ord ASC) res WHERE ln_amnt<>0");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("item", item);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);

                }

                //Receiving Purchase
                else if (rpt_no == "I031")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "301_rr_by_date.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, m7.c_name , w.whs_desc AS locationTo,  SUM(rl.ln_amnt) as ln_amnt FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code WHERE r.trn_type='P' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') GROUP BY r.rec_num, r.purc_ord, r._reference, r.trnx_date, m7.c_name, w.whs_desc ORDER BY r.trnx_date ASC) res WHERE ln_amnt<>0");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I032")
                {

                    String code1 = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND r.rec_num  >= '" + get_cbo_value(cbo_1) + "')";
                        code1 = get_cbo_value(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND r.rec_num  <= '" + get_cbo_value(cbo_2) + "'";
                        code1 = "First To " + get_cbo_value(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND (r.rec_num BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        code1 = get_cbo_value(cbo_1) + " To " + get_cbo_value(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            code1 = get_cbo_value(cbo_1);
                        }
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "302_rr_by_number.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date,'MM/dd/yyyy') AS trnx_date, m7.c_name, w.whs_desc AS locationTo, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='P' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date , r.rec_num, rl.ln_num) res WHERE ln_amnt<>0");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("code1", code1);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I033")
                {

                    String supplier = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND rl.item_code='" + get_cbo_value(cbo_1) + "'";
                        supplier = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "303_rr_by_supplier.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, m7.c_name AS supl_name, w.whs_desc AS locationTo, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='P' " + (WHERE == "WHERE" ? "" : WHERE) + " AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY trnx_date , r.rec_num, rl.ln_num) res WHERE ln_amnt<>0");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("item", supplier);
                    //add_fieldparam("t_date", dtp_frm.Value.ToString("yyyy-MM-dd") + " To " + dtp_to.Value.ToString("yyyy-MM-dd"));
                    add_fieldparam("dt_frm", dtp_frm.Value.ToString("yyyy-MM-dd"));
                    add_fieldparam("dt_to", dtp_to.Value.ToString("yyyy-MM-dd"));

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);

                }
                else if (rpt_no == "I034")
                {
                    if (get_cbo_index(cbo_1) == -1)
                    {
                        MessageBox.Show("Please select Item.");
                    }
                    else
                    {
                        String item = get_cbo_text(cbo_1);
                        inc_pbar(10);

                        myReportDocument.Load(fileloc_inv + "304_rr_by_item.rpt");

                        DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, m7.c_name AS supl_name, w.whs_desc AS locationTo, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='P' AND r.item_code='" + get_cbo_value(cbo_1) + "' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY trnx_date,r.rec_num, rl.ln_num) res WHERE ln_amnt<>0");

                        myReportDocument.Database.Tables[0].SetDataSource(dt);
                        inc_pbar(10);

                        add_fieldparam("comp_name", comp_name);
                        add_fieldparam("comp_addr", comp_addr);
                        add_fieldparam("userid", GlobalClass.username);
                        add_fieldparam("item", "(" + get_cbo_value(cbo_1) + ") " + item);

                        String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                        if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                            t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                        add_fieldparam("t_date", t_date);

                        clr_param();
                        inc_pbar(10);
                        disp_reportviewer(myReportDocument);
                    }
                }

                //Direct Purchase
                else if (rpt_no == "I041")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "401_dr_by_date.rpt");


                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT p.inv_num, p.reference, to_char(p.t_date, 'MM/dd/yyyy') AS t_date , m7.c_name , w.whs_desc, SUM(COALESCE(pl.ln_amnt,0.00)) as ln_amnt FROM rssys.pinvhd p LEFT JOIN rssys.pinvln pl ON pl.inv_num=p.inv_num LEFT JOIN rssys.m07 m7 ON m7.c_code=p.supl_code LEFT JOIN rssys.m10 m10 ON m10.mp_code=p.pay_code LEFT JOIN rssys.whouse w ON w.whs_code=p.whs_code WHERE p.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' GROUP BY p.inv_num, p.reference, p.t_date, m7.c_name, w.whs_desc ORDER BY p.t_date ASC) res WHERE ln_amnt<>0");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I042")
                {

                    String code1 = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND p.inv_num  >= '" + get_cbo_value(cbo_1) + "')";
                        code1 = get_cbo_value(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND p.inv_num  <= '" + get_cbo_value(cbo_2) + "'";
                        code1 = "First To " + get_cbo_value(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND (p.inv_num BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        code1 = get_cbo_value(cbo_1) + " To " + get_cbo_value(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            code1 = get_cbo_value(cbo_1);
                        }
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "402_dr_by_number.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT p.inv_num, p.reference, to_char(p.t_date,'MM/dd/yyyy') AS t_date, m7.c_name AS supl_name, w.whs_desc, m10.mp_desc, pl.ln_num, pl.item_code, pl.item_desc, pl.price, pl.inv_qty, u.unit_shortcode AS unit, pl.disc_amt, pl.ln_amnt, pl.ln_vat FROM rssys.pinvhd p LEFT JOIN rssys.pinvln pl ON pl.inv_num=p.inv_num LEFT JOIN rssys.m07 m7 ON m7.c_code=p.supl_code LEFT JOIN rssys.m10 m10 ON m10.mp_code=p.pay_code LEFT JOIN rssys.whouse w ON w.whs_code=p.whs_code  LEFT JOIN rssys.itmunit u ON pl.unit=u.unit_id WHERE p.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' " + WHERE + " ORDER BY p.t_date, p.inv_num, pl.ln_num ASC) res WHERE ln_amnt<>0");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("code1", code1);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I043")
                {
                    String supplier = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND p.supl_code='" + get_cbo_value(cbo_1) + "' ";
                        supplier = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "403_dr_by_supplier.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT DISTINCT p.inv_num, p.reference, to_char(p.t_date, 'MM/dd/yyyy') AS trnx_date, m7.c_name AS supl_name, w.whs_desc, pl.ln_num, pl.item_code, pl.item_desc, pl.price, pl.inv_qty, u.unit_shortcode AS unit, pl.disc_amt, pl.ln_amnt, pl.ln_vat FROM rssys.pinvhd p LEFT JOIN rssys.pinvln pl ON pl.inv_num=p.inv_num LEFT JOIN rssys.m07 m7 ON m7.c_code=p.supl_code LEFT JOIN rssys.m10 m10 ON m10.mp_code=p.pay_code LEFT JOIN rssys.whouse w ON w.whs_code=p.whs_code  LEFT JOIN rssys.itmunit u ON pl.unit=u.unit_id WHERE p.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' " + WHERE + " ORDER BY trnx_date, p.inv_num, pl.ln_num ASC) res WHERE ln_amnt<>0");


                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("supplier", supplier);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I044")
                {

                    String item = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND pl.item_code='" + get_cbo_value(cbo_1) + "' ";
                        item = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "404_dr_by_item.rpt");


                    DataTable dt = db.QueryBySQLCode("SELECT * FROM(SELECT DISTINCT p.inv_num, p.reference, to_char(p.t_date, 'MM/dd/yyyy') AS t_date , m7.c_name AS supl_name, w.whs_desc, pl.ln_num, pl.item_code, pl.item_desc, pl.price, pl.inv_qty, u.unit_shortcode AS unit, pl.disc_amt, pl.ln_amnt, pl.ln_vat FROM rssys.pinvhd p LEFT JOIN rssys.pinvln pl ON pl.inv_num=p.inv_num LEFT JOIN rssys.m07 m7 ON m7.c_code=p.supl_code LEFT JOIN rssys.m10 m10 ON m10.mp_code=p.pay_code LEFT JOIN rssys.whouse w ON w.whs_code=p.whs_code LEFT JOIN rssys.itmunit u ON pl.unit=u.unit_id WHERE p.t_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' " + WHERE + " ORDER BY t_date, p.inv_num, pl.ln_num ASC) res WHERE ln_amnt<>0");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("item", item);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }



                //Stock Issuance
                else if (rpt_no == "I051")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "501_si_by_date.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='I' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY trnx_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I052")
                {

                    String code1 = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND r.rec_num  >= '" + get_cbo_value(cbo_1) + "')";
                        code1 = get_cbo_value(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND r.rec_num  <= '" + get_cbo_value(cbo_2) + "'";
                        code1 = "First To " + get_cbo_value(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND (r.rec_num BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        code1 = get_cbo_value(cbo_1) + " To " + get_cbo_value(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            code1 = get_cbo_value(cbo_1);
                        }
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "502_si_by_number.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='I' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "')  " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date, r.rec_num ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("code_fromto", code1);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I053")
                {

                    String item = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND rl.item_code='" + get_cbo_value(cbo_1) + "'";
                        item = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "503_si_by_item.rpt");


                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='I' " + (WHERE == "WHERE" ? "" : WHERE) + " AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY trnx_date, r.rec_num ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("item", item);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);


                }
                else if (rpt_no == "I054")
                {

                    String costcenter = "All";
                    String scc_frmto = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND rl.scc_code >= '" + get_cbo_value(cbo_2) + "'";
                        scc_frmto = get_cbo_text(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE = "AND rl.scc_code <= '" + get_cbo_value(cbo_3) + "'";
                        scc_frmto = "First To " + get_cbo_text(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE = "AND (rl.scc_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        scc_frmto = get_cbo_text(cbo_2) + " To " + get_cbo_text(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            scc_frmto = get_cbo_value(cbo_2);
                        }
                    }

                    String WHERE2 = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE2 = "AND rl.cnt_code='" + get_cbo_value(cbo_1) + "' ";
                        costcenter = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }


                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "504_si_by_subcostcenter.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='I' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "')  " + WHERE2 + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date, r.rec_num, rl.ln_num");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("cc_frmto", costcenter);
                    add_fieldparam("scc_frmto", scc_frmto);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);

                }


                //Stock Transfer
                else if (rpt_no == "I061")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "601_st_by_date.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, r.purc_ord, r._reference,to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='T' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY trnx_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I062")
                {
                    String code1 = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND r.rec_num  >= '" + get_cbo_value(cbo_1) + "')";
                        code1 = get_cbo_value(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND r.rec_num  <= '" + get_cbo_value(cbo_2) + "'";
                        code1 = "First To " + get_cbo_value(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND (r.rec_num BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        code1 = get_cbo_value(cbo_1) + " To " + get_cbo_value(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            code1 = get_cbo_value(cbo_1);
                        }
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "602_st_by_number.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='T' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date, r.rec_num ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("code_fromto", code1);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I063")
                {

                    String item = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND rl.item_code='" + get_cbo_value(cbo_1) + "'";
                        item = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }


                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "603_st_by_item.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.\"locationFrom\", r.\"locationTo\", r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='T' " + (WHERE == "WHERE" ? "" : WHERE) + " AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY trnx_date, r.rec_num ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("item", item);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }


                //Stock Adjustment
                else if (rpt_no == "I071")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "701_sa_by_date.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, w.whs_desc as whs_code rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='A' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY trnx_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I072")
                {
                    String code1 = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND r.rec_num  >= '" + get_cbo_value(cbo_1) + "')";
                        code1 = get_cbo_value(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND r.rec_num  <= '" + get_cbo_value(cbo_2) + "'";
                        code1 = "First To " + get_cbo_value(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND (r.rec_num BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        code1 = get_cbo_value(cbo_1) + " To " + get_cbo_value(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            code1 = get_cbo_value(cbo_1);
                        }
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "702_sa_by_number.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='A' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "')  " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date, r.rec_num ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("code_fromto", code1);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "I073")
                {

                    String item = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND rl.item_code='" + get_cbo_value(cbo_1) + "'";
                        item = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "703_sa_by_item.rpt");


                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, r.purc_ord, r._reference, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, cc.cc_desc, scc.scc_desc, rl.ln_num, rl.item_code, rl.item_desc, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat, w.whs_desc as whs_code FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='A' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "')  " + (WHERE == "WHERE" ? "" : WHERE) + "  ORDER BY trnx_date, r.rec_num ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("item", item);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }
                //Inventory Summary By Date
                else if (rpt_no == "I01")
                {
                    String itemgrp_frmto = "All", whouse_frmto = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " i.item_grp = '" + get_cbo_value(cbo_1) + "'";
                        itemgrp_frmto = get_cbo_text(cbo_1);
                    }
                    if (get_cbo_index(cbo_2) != -1) //problem, no Warehouse
                    {
                        WHERE += (WHERE.Equals("WHERE") ? " " : " AND") + " condition = '" + get_cbo_value(cbo_2) + "'";
                        whouse_frmto = get_cbo_text(cbo_2);
                    }


                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "i_inventory_summary_by_date.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT i.* FROM rssys.items i LEFT JOIN rssys.itmgrp ig ON ig.item_grp=i.item_grp " + (WHERE.Equals("WHERE") ? "" : WHERE) + " ORDER BY i.item_desc ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("itemgrp_frmto", itemgrp_frmto);
                    add_fieldparam("whouse_frmto", whouse_frmto);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }

                //Inventory Valuation
                else if (rpt_no == "I02")
                {
                    String itemgrp_frmto = "All", whouse_frmto = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " i.item_grp = '" + get_cbo_value(cbo_1) + "'";
                        itemgrp_frmto = get_cbo_text(cbo_1);
                    }
                    if (get_cbo_index(cbo_2) != -1) //problem, no Warehouse
                    {
                        WHERE += (WHERE.Equals("WHERE") ? " " : " AND") + " i.item_grp = '" + get_cbo_value(cbo_2) + "'";
                        whouse_frmto = get_cbo_text(cbo_2);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "i_inventory_valuation.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT i.*,ig.* FROM rssys.items i LEFT JOIN rssys.itmgrp ig ON ig.item_grp=i.item_grp " + (WHERE.Equals("WHERE") ? "" : WHERE) + " ORDER BY i.item_desc ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("itemgrp_frmto", itemgrp_frmto);
                    add_fieldparam("whouse_frmto", whouse_frmto);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }

                //Reorder Level Report
                else if (rpt_no == "I03")//problem, no QUERY
                {

                    String itemgrp = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " i.item_grp = '" + get_cbo_value(cbo_1) + "'";
                        itemgrp = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "i_reorder.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT i.* FROM rssys.items i LEFT JOIN rssys.itmgrp ig ON ig.item_grp=i.item_grp " + (WHERE.Equals("WHERE") ? "" : WHERE) + " ORDER BY i.item_desc ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("itemgrp", itemgrp);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);
                    disp_reportviewer(myReportDocument);
                }

                //Outlet Sales Report
                else if (rpt_no == "S011")
                {

                    String branch = "All";
                    String outlet = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE += " AND ot.branch='" + get_cbo_value(cbo_1) + "'";
                        branch = get_cbo_text(cbo_1);
                    }

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE += " AND o.out_code='" + get_cbo_value(cbo_1) + "'";
                        outlet = "(" + get_cbo_value(cbo_2) + ")" + get_cbo_text(cbo_2);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_sale + "101_outlet_sales.rpt");

                    //o.rom_code,                 disc_amnt, net_amnt, amnt_due, tax_amnt
                    DataTable dt = db.QueryBySQLCode("SELECT ot.out_code||'-'||ot.out_desc AS out_code, o.ord_code, to_char(o.ord_date, 'MM/dd/yyyy') AS ord_date, o.customer AS customer,  SUM(COALESCE(ol.disc_amt,0.00)) AS disc_amnt,  SUM(COALESCE(ol.ln_tax,0.00)) AS tax_amnt,  SUM(COALESCE(ol.net_amount,0.00)) AS net_amnt, CASE WHEN o.pay_code='CSH' THEN SUM(ol.ln_amnt) ELSE '0.00'  END AS cash, CASE WHEN o.pay_code='CRD' THEN SUM(ol.ln_amnt) ELSE '0.00' END AS card, CASE WHEN o.pay_code='DCD' THEN SUM(ol.ln_amnt) ELSE '0.00' END AS dcard, CASE WHEN o.pay_code='CHG' THEN SUM(ol.ln_amnt) ELSE '0.00' END AS charge, SUM(COALESCE(ol.ln_amnt,0.00)) AS amnt_due FROM " + db.get_schema() + ".orhdr o LEFT JOIN " + db.get_schema() + ".orlne ol ON o.ord_code=ol.ord_code LEFT JOIN " + db.get_schema() + ".outlet ot ON o.out_code=ot.out_code WHERE o.ord_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' AND COALESCE(ol.item_code,'')<>'' " + WHERE + " GROUP BY ot.out_code||'-'||ot.out_desc, o.ord_code, o.ord_date, customer");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.user_fullname);
                    add_fieldparam("branch", branch);
                    add_fieldparam("outlet", outlet);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                // CASHIER's REPORT
                else if (rpt_no == "S011B")
                {
                    String outlet = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " o.out_code='" + get_cbo_value(cbo_1) + "' AND ";
                        outlet = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_sale + "101B_cashier.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT o.ord_code, o.out_code, to_char(o.ord_date, 'MM/dd/yyyy') AS ord_date, o.customer, o.reference, CASE WHEN ol.pay_code='101' THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS cash, CASE WHEN ol.pay_code='102' THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS dcard, CASE WHEN ol.pay_code='103' THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS card, CASE WHEN ol.pay_code='114' THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS price, CASE WHEN ol.pay_code NOT IN('101','102','103','114') THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS charge FROM rssys.orhdr o LEFT JOIN rssys.orlne ol ON o.ord_code=ol.ord_code WHERE " + WHERE + " o.ord_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' AND COALESCE(ol.item_code,'')='' AND COALESCE(ol.ln_amnt,0.00)<0 ORDER BY ord_date");


                    //SELECT o.ord_code, o.out_code, to_char(o.ord_date, 'MM/dd/yyyy') AS ord_date, o.customer, o.reference, CASE WHEN ol.pay_code='101' THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS cash, CASE WHEN ol.pay_code='102' THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS dcard, CASE WHEN ol.pay_code='103' THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS card, CASE WHEN ol.pay_code='114' THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS price, CASE WHEN ol.pay_code NOT IN('101','102','103','114') THEN (-1 * ol.ln_amnt) ELSE 0.00 END AS charge FROM rssys.orhdr o LEFT JOIN rssys.orlne ol ON o.ord_code=ol.ord_code WHERE " + WHERE + " o.ord_date BETWEEN 2017-01-01 AND 2017-06-01 AND COALESCE(ol.item_code,'')='' AND COALESCE(ol.ln_amnt,0.00)<0 ORDER BY ord_date
                    //
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("outlet", outlet);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                //Outlet Sales Report
                else if (rpt_no == "S012")//problem, no WHERE CLUASE
                {
                    String outlet = "All";
                    String itemgrp = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " o.out_code='" + get_cbo_value(cbo_1) + "' AND ";
                        outlet = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    String WHERE2 = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = WHERE + " i.item_grp >= '" + get_cbo_value(cbo_2) + "' AND ";
                        itemgrp = "(" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = WHERE + " i.item_grp  <= '" + get_cbo_value(cbo_3) + "' AND ";
                        itemgrp = "First To " + "(" + get_cbo_value(cbo_3) + ") " + get_cbo_text(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = WHERE + " (i.item_grp BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "') AND ";// need toReplace
                        itemgrp = "(" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2) + " To " + "(" + get_cbo_value(cbo_3) + ") " + get_cbo_text(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            itemgrp = get_cbo_value(cbo_2);
                        }
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_sale + "102_outlet_sales_by_item.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT o.ord_code, o.out_code,  to_char(o.ord_date, 'MM/dd/yyyy') AS ord_date, o.customer AS customer, ol.item_code ||'-'|| ol.item_desc AS item_code, iu.unit_shortcode AS unit, ol.ord_qty, ol.price, ol.disc_amt AS disc_amnt, SUM(ol.ln_amnt) AS ln_amnt FROM " + db.get_schema() + ".orhdr o LEFT JOIN " + db.get_schema() + ".orlne ol ON o.ord_code=ol.ord_code LEFT JOIN " + db.get_schema() + ".itmunit iu ON iu.unit_id=ol.unit LEFT JOIN " + db.get_schema() + ".items i ON i.item_code=ol.item_code WHERE " + WHERE2 + " o.ord_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' AND COALESCE(ol.item_code,'')<>'' GROUP BY o.ord_code,  o.out_code, o.ord_date, customer, ol.item_desc, ol.unit, ol.ord_qty, ol.price, ol.disc_amt, ol.item_code ||'-'|| ol.item_desc, iu.unit_shortcode ORDER BY o.ord_date");

                    //dt = db.QueryOnTableWithParams("itmgrp", "item_grp, grp_desc", "", " ORDER BY item_grp ASC");
                    //
                    //SELECT o.ord_code,  o.out_code, o.ord_date, o.customer AS customer, ol.item_code ||'-'|| ol.item_desc AS item_code, iu.unit_shortcode AS unit, ol.ord_qty, ol.price, ol.disc_amt, SUM(ol.ln_amnt) AS ord_amnt FROM rssys.orhdr o LEFT JOIN rssys.orlne ol ON o.ord_code=ol.ord_code LEFT JOIN rssys.itmunit iu ON iu.unit_id=ol.unit WHERE  o.ord_date BETWEEN '2017-01-01' AND '2017-05-24' AND COALESCE(ol.item_code,ol.item_code,'')<>'' GROUP BY o.ord_code,  o.out_code, o.ord_date, customer, item_code, ol.item_desc, iu.unit_shortcode, ol.ord_qty, ol.price, ol.disc_amt


                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("outlet", outlet);
                    add_fieldparam("itemgrp", itemgrp);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }

                //Sales Report By Item
                else if (rpt_no == "S013")
                {
                    String outlet = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " o.out_code='" + get_cbo_value(cbo_1) + "' AND ";
                        outlet = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_sale + "103_sales_by_item.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT ig.item_grp ||'-'|| ig.grp_desc AS itmgrp, ol.item_code, ol.item_desc, u.unit_shortcode AS unit, ol.price, SUM(ol.ord_qty) AS ord_qty,  SUM(ol.ln_amnt) AS ln_amnt FROM " + db.get_schema() + ".orhdr o LEFT JOIN " + db.get_schema() + ".orlne ol ON o.ord_code=ol.ord_code LEFT JOIN " + db.get_schema() + ".items i ON i.item_code=ol.item_code LEFT JOIN " + db.get_schema() + ".itmgrp ig ON ig.item_grp=i.item_grp LEFT JOIN " + db.get_schema() + ".itmunit u ON u.unit_id=ol.unit WHERE " + WHERE + " o.ord_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' AND COALESCE(ol.item_code,'')<>'' GROUP BY o.ord_date, o.out_code, ig.item_grp, ig.item_grp, ol.item_code, ol.item_desc, u.unit_shortcode, ol.price ORDER BY o.ord_date, itmgrp");


                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("outlet", outlet);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }

            //Sales Report By Staff
                else if (rpt_no == "S014")
                {

                    String outlet = "All";
                    String staff_frmto = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " o.out_code='" + get_cbo_value(cbo_1) + "' AND ";
                        outlet = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    String WHERE2 = "";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE2 = WHERE + " o.rep_code >= '" + get_cbo_value(cbo_2) + "' AND ";
                        staff_frmto = "(" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = WHERE + " o.rep_code  <= '" + get_cbo_value(cbo_3) + "' AND ";
                        staff_frmto = "First To " + "(" + get_cbo_value(cbo_3) + ") " + get_cbo_text(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE2 = WHERE + " (o.rep_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "') AND ";
                        staff_frmto = "(" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2) + " To " + "(" + get_cbo_value(cbo_3) + ") " + get_cbo_text(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            staff_frmto = get_cbo_value(cbo_2);
                        }
                    }


                    inc_pbar(10);

                    myReportDocument.Load(fileloc_sale + "104_sales_by_staff.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT rp.rep_code ||'-'|| rp.rep_name AS rep_code, ol.ord_code, ol.item_code, ol.item_desc, u.unit_shortcode AS unit, ol.price, ol.ord_qty AS ord_qty,  ol.ln_amnt AS ln_amnt FROM " + db.get_schema() + ".orhdr o LEFT JOIN " + db.get_schema() + ".orlne ol ON o.ord_code=ol.ord_code LEFT JOIN " + db.get_schema() + ".items i ON i.item_code=ol.item_code LEFT JOIN " + db.get_schema() + ".repmst rp ON rp.rep_code=o.rep_code LEFT JOIN " + db.get_schema() + ".itmunit u ON u.unit_id=ol.unit WHERE  " + WHERE2 + " (o.ord_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') AND COALESCE(ol.item_code,'')<>'' ORDER BY o.ord_date, rep_code");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("outlet", outlet);
                    add_fieldparam("staff_frmto", staff_frmto);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }

            //Sales Report Summary
                else if (rpt_no == "S015")
                {
                    String outlet = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " o.out_code='" + get_cbo_value(cbo_1) + "' AND ";
                        outlet = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_sale + "105_sales_report_summary.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT to_char(o.ord_date, 'MM/dd/yyyy') AS ord_date, SUM(COALESCE(ol.ln_tax,0.00)) AS tax_amnt, SUM(COALESCE(ol.disc_amt,0.00)) as disc_amnt, SUM(COALESCE(ol.disc_amt,0.00)) AS disc_amt, CASE WHEN o.pay_code='CSH' THEN SUM(ol.ln_amnt) ELSE '0.00'  END AS cash, CASE WHEN o.pay_code='CRD' THEN SUM(ol.ln_amnt) ELSE '0.00' END AS card, CASE WHEN o.pay_code='CDC' THEN SUM(ol.ln_amnt) ELSE '0.00' END AS dcard, CASE WHEN o.pay_code='CHG' THEN SUM(ol.ln_amnt) ELSE '0.00' END AS charge, SUM(COALESCE(ol.ln_amnt,0.00)) AS amnt_due FROM " + db.get_schema() + ".orhdr o LEFT JOIN " + db.get_schema() + ".orlne ol ON o.ord_code=ol.ord_code WHERE " + WHERE + " o.ord_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' AND COALESCE(ol.item_code,'')<>''  GROUP BY o.ord_date, o.pay_code, ol.ln_tax, ol.disc_amt ORDER BY ord_date");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("outlet", outlet);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }

            //Summary of Item Sold and Material Usage
                else if (rpt_no == "S016")
                {
                    String outlet = "All";
                    WHERE = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = " o.out_code='" + get_cbo_value(cbo_1) + "' AND ";
                        outlet = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_sale + "106_summary_of_item_sold_manterial_usage.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT ig.item_grp ||'-'|| ig.grp_desc AS itmgrp, ol.item_code, ol.item_desc, u.unit_shortcode AS unit, ol.fcp, SUM(ol.ord_qty) AS ord_qty, (ol.fcp*SUM(ol.ord_qty)) AS ln_amnt  FROM " + db.get_schema() + ".orhdr o LEFT JOIN " + db.get_schema() + ".orlne ol ON o.ord_code=ol.ord_code LEFT JOIN " + db.get_schema() + ".items i ON i.item_code=ol.item_code LEFT JOIN " + db.get_schema() + ".itmgrp ig ON ig.item_grp=i.item_grp LEFT JOIN " + db.get_schema() + ".itmunit u ON u.unit_id=ol.unit WHERE " + WHERE + " o.ord_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' AND COALESCE(ol.item_code,'')<>''  GROUP BY o.ord_date, ig.item_grp, ig.item_grp,  ol.item_code, ol.item_desc, u.unit_shortcode, ol.price, ol.fcp ORDER BY o.ord_date , ig.item_grp");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("outlet", outlet);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }


                //Project
                else if (rpt_no == "P101")
                {
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_proj + "101_psi_by_date.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, rl.scc_code ||'-'||scc.scc_desc AS scc_code, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='I' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY trnx_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "P102")
                {

                    String code1 = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND r.rec_num  >= '" + get_cbo_value(cbo_1) + "')";
                        code1 = get_cbo_value(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND r.rec_num  <= '" + get_cbo_value(cbo_2) + "'";
                        code1 = "First To " + get_cbo_value(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND (r.rec_num BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        code1 = get_cbo_value(cbo_1) + " To " + get_cbo_value(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            code1 = get_cbo_value(cbo_1);
                        }
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_proj + "102_psi_by_number.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, r._reference, rl.scc_code ||'-'||scc.scc_desc AS scc_desc, rl.cnt_code ||' - '|| cc.cc_desc AS c_name, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='I' " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date, r.rec_num ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("code_fromto", code1);

                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "P103")
                {

                    String item = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND rl.item_code='" + get_cbo_value(cbo_1) + "'";
                        item = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }


                    inc_pbar(10);

                    myReportDocument.Load(fileloc_proj + "103_psi_by_item.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, r._reference, rl.scc_code ||'-'||scc.scc_desc AS scc_desc, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='I' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("item", item);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);

                }
                else if (rpt_no == "P104")
                {
                    String code1 = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE = "AND ig.item_grp >= '" + get_cbo_value(cbo_1) + "'";
                        code1 = get_cbo_text(cbo_1) + " To Last";
                    }
                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND ig.item_grp <= '" + get_cbo_value(cbo_2) + "'";
                        code1 = "First To " + get_cbo_text(cbo_2);
                    }
                    if (get_cbo_index(cbo_1) != -1 && get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND (ig.item_grp BETWEEN '" + get_cbo_value(cbo_1) + "' AND '" + get_cbo_value(cbo_2) + "')";
                        code1 = get_cbo_text(cbo_1) + " To " + get_cbo_text(cbo_2);
                        if (get_cbo_index(cbo_1) == get_cbo_index(cbo_2))
                        {
                            code1 = get_cbo_text(cbo_1);
                        }
                    }

                    String costcenter = get_cbo_text(cbo_1);
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_proj + "104_psi_by_itemgroups.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, ig.item_grp ||'-'|| grp_desc AS item_grp, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.ln_amnt FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id LEFT JOIN " + db.get_schema() + ".items i ON i.item_code=rl.item_code LEFT JOIN " + db.get_schema() + ".itmgrp ig ON ig.item_grp=i.item_grp WHERE r.trn_type='I' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date, ig.item_grp, r.rec_num ASC");


                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("itmgrp", code1);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "P105")
                {

                    String costcenter = "All";
                    String scc_frmto = "All";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND rl.scc_code >= '" + get_cbo_value(cbo_2) + "'";
                        scc_frmto = get_cbo_text(cbo_2) + " To Last";
                    }
                    if (get_cbo_index(cbo_3) != -1)
                    {
                        WHERE = "AND rl.scc_code <= '" + get_cbo_value(cbo_3) + "'";
                        scc_frmto = "First To " + get_cbo_text(cbo_3);
                    }
                    if (get_cbo_index(cbo_2) != -1 && get_cbo_index(cbo_3) != -1)
                    {
                        WHERE = "AND (rl.scc_code BETWEEN '" + get_cbo_value(cbo_2) + "' AND '" + get_cbo_value(cbo_3) + "')";
                        scc_frmto = get_cbo_text(cbo_2) + " To " + get_cbo_text(cbo_3);
                        if (get_cbo_index(cbo_2) == get_cbo_index(cbo_3))
                        {
                            scc_frmto = get_cbo_text(cbo_2);
                        }
                    }

                    String WHERE2 = "";

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        WHERE2 = "AND r.supl_code='" + get_cbo_value(cbo_1) + "'";
                        costcenter = "(" + get_cbo_value(cbo_1) + ") " + get_cbo_text(cbo_1);
                    }

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_proj + "105_psi_by_project.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, r._reference, rl.scc_code ||'-'||scc.scc_desc AS scc_desc, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, rl.discount, rl.ln_amnt, rl.ln_vat FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id WHERE r.trn_type='I' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date, rl.scc_code, r.rec_num ASC");

                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("cc_frmto", costcenter);
                    add_fieldparam("scc_frmto", scc_frmto);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }
                else if (rpt_no == "P201")
                {
                    String subcostcenter = "All";
                    String bars = "00001";
                    String cement = "00002";
                    String sg = "00003";
                    WHERE = "WHERE";

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        WHERE = "AND rl.scc_code='" + get_cbo_value(cbo_2) + "'";
                        subcostcenter = "(" + get_cbo_value(cbo_2) + ") " + get_cbo_text(cbo_2);
                    }
                    inc_pbar(10);

                    myReportDocument.Load(fileloc_proj + "201_summary_of_project_costing_per_item.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT r.rec_num, to_char(r.trnx_date, 'MM/dd/yyyy') AS trnx_date, rl.scc_code, rl.item_code, rl.item_desc, rl.price, rl.recv_qty, u.unit_shortcode AS unit, CASE WHEN ig.item_grp = '" + bars + "' THEN rl.ln_amnt ELSE '0.00' END AS bars, CASE WHEN ig.item_grp = '" + cement + "' THEN rl.ln_amnt ELSE '0.00' END AS cement, CASE WHEN ig.item_grp = '" + sg + "' THEN rl.ln_amnt ELSE '0.00' END AS sg, CASE WHEN ig.item_grp != '" + bars + "' AND ig.item_grp != '" + cement + "' AND ig.item_grp != '" + sg + "'  THEN rl.ln_amnt ELSE '0.00' END AS others FROM " + db.get_schema() + ".rechdr r LEFT JOIN " + db.get_schema() + ".reclne rl ON rl.rec_num=r.rec_num LEFT JOIN " + db.get_schema() + ".m07 m7 ON m7.c_code=r.supl_code LEFT JOIN " + db.get_schema() + ".whouse w ON w.whs_code=r.whs_code LEFT JOIN " + db.get_schema() + ".m08 cc ON cc.cc_code=rl.cnt_code LEFT JOIN " + db.get_schema() + ".subctr scc ON scc.scc_code=rl.scc_code LEFT JOIN " + db.get_schema() + ".itmunit u ON rl.unit=u.unit_id LEFT JOIN " + db.get_schema() + ".items i ON i.item_code=rl.item_code LEFT JOIN " + db.get_schema() + ".itmgrp ig ON ig.item_grp=i.item_grp WHERE r.trn_type='I' AND (r.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "')  " + (WHERE == "WHERE" ? "" : WHERE) + " ORDER BY trnx_date ASC");


                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("scc_frmto", subcostcenter);

                    String t_date = dtp_frm.Value.ToString("MM/dd/yyyy");
                    if (t_date != dtp_to.Value.ToString("MM/dd/yyyy"))
                        t_date += " - " + dtp_to.Value.ToString("MM/dd/yyyy");
                    add_fieldparam("t_date", t_date);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }



                else if (rpt_no == "AS01P")
                {
                    String soa_period = get_cbo_value(cbo_1);
                    String soa_period_desc = get_cbo_text(cbo_1);

                    if (!String.IsNullOrEmpty(soa_period))
                    {
                        String[] dt_ = soa_period.ToString().Split('-');
                        String dt_frm = dt_.GetValue(0).ToString(), dt_to = dt_.GetValue(1).ToString();

                        WHERE += " AND (sh.soa_period='" + soa_period + "' OR sh.soa_period BETWEEN '" + dt_frm + "' AND '" + dt_to + "'  )";
                    }

                    if (get_cbo_index(cbo_2) != -1)
                    {
                        soa_period_desc += "       Tenant : " + get_cbo_text(cbo_2);
                        WHERE += " AND sh.debt_code='" + get_cbo_value(cbo_2) + "'";
                    }

                    WHERE += " AND sh.branch='" + get_cbo_value(cbo_4) + "' ";

                    myReportDocument.Load(fileloc_acctg + "a_statement_accountlist.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT sh.soa_code, sh.rom_code AS roomno, debt_name AS tenant, CASE WHEN soafrom is null THEN to_char(soa_period::date,'MM/DD/YY') ELSE to_char(soafrom,'MM/DD/YY')||'-'||to_char(soato,'MM/DD/YY') END  AS soa_period, SUM(CASE WHEN sl.chg_class='URENT' THEN amount ELSE 0 END) AS rent, SUM(CASE WHEN sl.chg_class='WATER' THEN amount ELSE 0 END) AS water, SUM(CASE WHEN sl.chg_class='ELEC' THEN amount ELSE 0 END) AS electric, SUM(CASE WHEN sl.chg_class='INET' THEN amount ELSE 0 END) AS internet, SUM(CASE WHEN sl.chg_class not in ('URENT','ELEC','WATER','INET') THEN amount ELSE 0 END) AS others FROM (SELECT sh.*, sp.*, sl.rom_code, sl.rmrttyp FROM rssys.soahdr sh LEFT JOIN rssys.soa_period sp ON to_Char(sp.soafrom,'yyyy/MM/dd')||'-'||to_Char(sp.soato,'yyyy/MM/dd')=sh.soa_period LEFT JOIN (SELECT DISTINCT sl.soa_code, CASE WHEN COALESCE(gf.rom_code,'')<>'' THEN gf.rom_code ELSE COALESCE(gfh.rom_code,'') END rom_code, CASE WHEN COALESCE(gf.rmrttyp,'')<>'' THEN gf.rmrttyp ELSE COALESCE(gfh.rmrttyp,'') END rmrttyp FROM rssys.soalne sl LEFT JOIN rssys.gfolio gf ON sl.gfolio=gf.reg_num LEFT JOIN rssys.gfhist gfh ON sl.gfolio=gfh.reg_num WHERE sl.ln_num='1') sl ON (sl.soa_code=sh.soa_code)  WHERE 1=1 ORDER BY soa_code DESC) sh LEFT JOIN (SELECT sl.*, c.chg_class FROM rssys.soalne sl LEFT JOIN rssys.charge c ON c.chg_code=sl.chg_code) sl ON sl.soa_code=sh.soa_code WHERE 1=1 " + WHERE + " GROUP BY  sh.soa_code, sh.debt_code, sh.branch, rom_code, debt_name, soafrom, soato, soa_period ORDER BY rom_code, sh.debt_code, sh.soa_period");


                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("soa_period_desc", "SOA Period : ");
                    add_fieldparam("soa_period", soa_period_desc);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    
                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }

                else if (rpt_no == "AS01D")
                {
                    String soa_period = get_cbo_value(cbo_1);
                    String soa_period_desc = get_cbo_text(cbo_1);
                    WHERE = "";

                    soa_period_desc = dtp_frm.Value.ToString("MM/dd/yy") + " - " + dtp_to.Value.ToString("MM/dd/yy");

                    if (get_cbo_index(cbo_1) != -1)
                    {
                        soa_period_desc += "       Tenant : " + get_cbo_text(cbo_1);
                        WHERE += " AND sh.debt_code='" + get_cbo_value(cbo_1) + "'";
                    }

                    WHERE += " AND ((sh.soafrom BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' OR sh.soato BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') OR sh.soa_period BETWEEN '" + dtp_frm.Value.ToString("yyyy/MM/dd") + "' AND '" + dtp_to.Value.ToString("yyyy/MM/dd") + "')";

                    WHERE += " AND sh.branch='" + get_cbo_value(cbo_4) + "' ";

                    myReportDocument.Load(fileloc_acctg + "a_statement_accountlist.rpt");

                    DataTable dt = db.QueryBySQLCode("SELECT sh.soa_code, sh.rom_code AS roomno, debt_name AS tenant, CASE WHEN soafrom is null THEN to_char(soa_period::date,'MM/DD/YY') ELSE to_char(soafrom,'MM/DD/YY')||'-'||to_char(soato,'MM/DD/YY') END  AS soa_period, SUM(CASE WHEN sl.chg_class='URENT' THEN amount ELSE 0 END) AS rent, SUM(CASE WHEN sl.chg_class='WATER' THEN amount ELSE 0 END) AS water, SUM(CASE WHEN sl.chg_class='ELEC' THEN amount ELSE 0 END) AS electric, SUM(CASE WHEN sl.chg_class='INET' THEN amount ELSE 0 END) AS internet, SUM(CASE WHEN sl.chg_class not in ('URENT','ELEC','WATER','INET') THEN amount ELSE 0 END) AS others FROM (SELECT sh.*, sp.*, sl.rom_code, sl.rmrttyp FROM rssys.soahdr sh LEFT JOIN rssys.soa_period sp ON to_Char(sp.soafrom,'yyyy/MM/dd')||'-'||to_Char(sp.soato,'yyyy/MM/dd')=sh.soa_period LEFT JOIN (SELECT DISTINCT sl.soa_code, CASE WHEN COALESCE(gf.rom_code,'')<>'' THEN gf.rom_code ELSE COALESCE(gfh.rom_code,'') END rom_code, CASE WHEN COALESCE(gf.rmrttyp,'')<>'' THEN gf.rmrttyp ELSE COALESCE(gfh.rmrttyp,'') END rmrttyp FROM rssys.soalne sl LEFT JOIN rssys.gfolio gf ON sl.gfolio=gf.reg_num LEFT JOIN rssys.gfhist gfh ON sl.gfolio=gfh.reg_num WHERE sl.ln_num='1') sl ON (sl.soa_code=sh.soa_code)  WHERE 1=1 ORDER BY soa_code DESC) sh LEFT JOIN (SELECT sl.*, c.chg_class FROM rssys.soalne sl LEFT JOIN rssys.charge c ON c.chg_code=sl.chg_code) sl ON sl.soa_code=sh.soa_code WHERE 1=1 " + WHERE + " GROUP BY  sh.soa_code, sh.debt_code, sh.branch, rom_code, debt_name, soafrom, soato, soa_period ORDER BY rom_code, sh.debt_code, sh.soa_period");


                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("soa_period_desc", "SOA Date : ");
                    add_fieldparam("soa_period", soa_period_desc);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);

                    clr_param();
                    inc_pbar(10);
                    disp_reportviewer(myReportDocument);
                }



            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            reset();
        }

        private void RPT_RES_entry_FormClosing(object sender, FormClosingEventArgs e)
        {
            //bgworker.CancelAsync();
        }

        private void cbo_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReady)
            {
                if (cbo_1.SelectedIndex > -1)
                {
                    if (rpt_no == "I014" || rpt_no == "P105")
                    {
                        String costcenter = cbo_1.SelectedValue.ToString();

                        gc.load_subcostcenter(cbo_2, costcenter);
                        gc.load_subcostcenter(cbo_3, costcenter);
                    }
                    else if (rpt_no.IndexOf("A40") >= 0)
                    {
                        Boolean isSL = false;
                        if (!String.IsNullOrEmpty(db.get_colval("m06", "d_code", " at_code='" + cbo_1.SelectedValue.ToString() + "'")) && (rpt_no == "A401" || rpt_no == "A402" || rpt_no.IndexOf("A403") >= 0))
                        {//Customer
                            isSL = true;
                        }
                        else if (!String.IsNullOrEmpty(db.get_colval("m07", "c_code", " at_code='" + cbo_1.SelectedValue.ToString() + "'")) && (rpt_no == "A404" || rpt_no == "A405" || rpt_no.IndexOf("A406") >= 0))
                        {//Supplier
                            isSL = true;
                        }

                        gc.load_subsidiarycode(cbo_2, (!isSL ? "00000" : cbo_1.SelectedValue).ToString());
                        gc.load_subsidiarycode(cbo_3, (!isSL ? "00000" : cbo_1.SelectedValue).ToString());
                    }
                    else if (rpt_no == "A502m3" || rpt_no == "A503m3")
                    {
                        try
                        { 
                            DateTime dt = DateTime.Parse(cbo_1.Text + " - 01");
                            if (Convert.ToInt32(dt.ToString("MM")) > 10) dt = dt.AddYears(-1);
                            cbo_2.Text = dt.AddMonths(2).ToString("yyyy - MMMM");
                        } catch { cbo_2.Text = DateTime.Parse(cbo_1.Text.Split('-')[0] + "-12-01").ToString("yyyy - MMMM"); }
                    }
                    else if (rpt_no.IndexOf("A50") >= 0 && lbl_cbo_1.Text.IndexOf("Period") >= 0)
                    {
                        cbo_2.DroppedDown = true;
                        cbo_2.SelectedValue = cbo_1.SelectedValue;
                    }
                    else if (rpt_no == "P201")
                    {
                        String cc_code = "";

                        if (cbo_1.SelectedIndex != -1)
                        {
                            cc_code = cbo_1.SelectedValue.ToString();
                        }
                        gc.load_subcostcenter(cbo_2, cc_code);
                        //gc.load_subcostcenter(cbo_3, cc_code);
                    }
                }

            }
        }

        private void crptviewer_Load(object sender, EventArgs e)
        {

        }

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            if (ishide_opt)
            {
                pnl_rpt_option.Hide();
                pnl_rpt_option_header.Height = 30;
                btn_minimize.Width = 150;

                ishide_opt = false;
            }
            else
            {
                pnl_rpt_option.Show();
                pnl_rpt_option_header.Height = 114;
                btn_minimize.Width = 64;

                ishide_opt = true;
            }
        }

        private void grp_options_Enter(object sender, EventArgs e)
        {

        }

        private void dtp_frm_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbo_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReady)
            {
                if (cbo_2.SelectedIndex > -1)
                {
                    if (rpt_no == "A102")
                    {
                        cbo_3.DroppedDown = true;
                        cbo_3.SelectedValue = cbo_2.SelectedValue;
                    }
                }

            }
        }

        private void cbo_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReady)
            {

            }
        }

        private void chk_1_CheckedChanged(object sender, EventArgs e)
        {
            if (isReady)
            {
                if (ischkbox_checked(chk_1))
                {
                }
            }
        }
        private void chk_2_CheckedChanged(object sender, EventArgs e)
        {
            if (isReady)
            {
                if (ischkbox_checked(chk_2))
                {

                }
            }
        }

        private void chk_3_CheckedChanged(object sender, EventArgs e)
        {
            if (isReady)
            {
                if (ischkbox_checked(chk_3))
                {

                }
            }
        }

        private void cbo_4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
