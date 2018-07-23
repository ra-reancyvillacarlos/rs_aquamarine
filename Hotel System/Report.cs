using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Microsoft;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Hotel_System
{
    public partial class Report : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalMethod gm = new GlobalMethod();
        NumberToEnglish_orig nte = new NumberToEnglish_orig();
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        String _schema;
        int action = 0;
        String fol_num;
        String rpt_no = "";
        String fileloc_hotel = @"\\RIGHTAPPS\RightApps\Eastland\Reports\Hotel\";
        /*
         * RPT NO
         * 001 - arr_chkin          - arvlchkin_register.rpt
         * 002 - guest_print out    - CrystalReport1.rpt
         * 003 - cc acknowledgment receipt
         * 
         * 
         */
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        DataTable dt, newdt;
        DateTime dtfrom, dtto;
        String dsr_rpt_no = "", clerk = "", dt_frm = "", dt_to = "";
        String total_or = "", total_amnt = "", total_fb = "", hotel_sales = "", fb_sales = "", extra_sales = "", cc_issued = "", cc_applied = "", total_sales = "", actual_cash = "", difference = "";
        String reg_num = "", acct_no = "", chkin_date = "", chkin_time = "", res_no = "", rom_code = "", full_name = "", romrate = "", pax = "", romtype = "", address = "", arr_dt = "", dep_dt = "", noofnights = "", remark = "", paymenttype = "", doc_reference = "", doctype = "", dep_amt = "", romratedurtype = "", dur_title = "", passportno = "", pp_valdt = "", pp_dt_place_issue = "", dt_issue = "", place_issue = "", nationality = "", contactno = "", email = "", ap_paymenttype = "", ap_doc_reference = "", ap_dep_amt = "";
        String arr_time = "", dep_time = "", totalcharges, totalpayments, chg_num = "", grossrate = "", disc_pct = "", rmrttyp = "";
        String rom_code2 = "", romrate2 = "", romtype2 = "", foclerk = "", approvedby = "", receivedby = "";
        String cc_no = "", gname = "", amt_word = "", amt = "", fo_name = "", or_no = "", paidout_vo_no = "", cc_date = "";

        public Report()
        {
            InitializeComponent();

            thisDatabase db = new thisDatabase();

            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();
            dt = new DataTable();
            newdt = new DataTable();
            _schema = db.get_schema();

            String system_loc = db.get_system_loc();

            fileloc_hotel = system_loc + "\\Reports\\Hotel\\";
        }

        public Report(String rnum, String fol_number)
        {
            InitializeComponent();

            thisDatabase db = new thisDatabase();

            fol_num = fol_number;
            rpt_no = rnum;

            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();
            dt = new DataTable();
            newdt = new DataTable();
            _schema = db.get_schema();
            
            String system_loc = db.get_system_loc();

            fileloc_hotel = system_loc + "\\Reports\\Hotel\\";
        }

        private void Report_Load(object sender, EventArgs e)
        {
            
        }

        public void print_contract(String lreg_num)
        {
            reg_num = lreg_num;

            action = 13;

            bgWorker.RunWorkerAsync();
        }

        public void print_cashsales_rpt(String ldsr_rpt_no, String lclerk, String ldt_frm, DataTable lnewdt, String ltotal_or, String ltotal_amnt, String ltotal_fb)
        {
            dsr_rpt_no = ldsr_rpt_no;
            clerk = lclerk;
            dt_frm = ldt_frm;
            newdt = lnewdt;
            total_or = ltotal_or;
            total_amnt = ltotal_amnt;
            total_fb = ltotal_fb;

            action = 1;

            bgWorker.RunWorkerAsync();
        }

        public void print_cardsales_rpt(String ldsr_rpt_no, String lclerk, String ldt_frm, DataTable lnewdt, String ltotal_or, String ltotal_amnt, String ltotal_fb)
        {
            dsr_rpt_no = ldsr_rpt_no;
            clerk = lclerk;
            dt_frm = ldt_frm;
            newdt = lnewdt;
            total_or = ltotal_or;
            total_amnt = ltotal_amnt;
            total_fb = ltotal_fb;

            action = 2;

            bgWorker.RunWorkerAsync();
        }

        public void print_ccsummary(String lclerk, String ldt_frm, String ldt_to, String lhotel_sales, String lfb_sales, String lextra_sales, String lcc_issued, String lcc_applied, String ltotal_sales, String lactual_cash, String ldifference, DataTable ldt)
        {
            clerk = lclerk;
            dt_frm = ldt_frm;
            hotel_sales = lhotel_sales;
            fb_sales = lfb_sales;
            extra_sales = lextra_sales;
            cc_issued = lcc_issued;
            cc_applied = lcc_applied;
            total_sales = ltotal_sales;
            actual_cash = lactual_cash;
            difference = ldifference;
            dt = ldt;

            action = 3;

            bgWorker.RunWorkerAsync();
        }

        public void print_extraitemsales(String lclerk, String ldt_frm, String ldt_to)
        {
            clerk = lclerk;
            dt_frm = ldt_frm;
            dt_to = ldt_to;

            action = 4;

            bgWorker.RunWorkerAsync();
        }

        //Start-Modify by: Roldan 03/27/18
        public void printnew_regform(String lregnum)
        {
            reg_num = lregnum;
            Report rpt = new Report("", "");
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            DataTable dt, dtg;
            Double noofnights = 0;
            DateTime arr_date = new DateTime();
            DateTime dep_date = new DateTime();
            String daddress = "", passport_no = "", passport_expiry = "", passport_issued = "", place_issue = "", dt_issue = "", remarks = "";


            
                dt = db.get_guestfolio_all_withaddr(reg_num);

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

                    printprev_regform(row["reg_num"].ToString(), arr_date.ToString("yyyy-MM-dd"), row["arr_time"].ToString(), row["res_code"].ToString(), row["rom_code"].ToString(), row["rmrttyp"].ToString(), row["full_name"].ToString(), (gm.toNormalDoubleFormat(row["rom_rate"].ToString()) + gm.toNormalDoubleFormat(row["govt_tax"].ToString())).ToString("0.00"), row["occ_type"].ToString(), db.get_romtyp_desc(row["typ_code"].ToString()), daddress, arr_date.ToString("yyyy-MM-dd"), row["arr_time"].ToString(), dep_date.ToString("yyyy-MM-dd"), row["dep_time"].ToString(), noofnights.ToString("0.00"), remarks, row["paymentform"].ToString(), row["doc_ref"].ToString(), row["doctype"].ToString(), row["dep_amnt"].ToString(), row["user_id"].ToString(), "", "", passport_no, passport_expiry, place_issue, dt_issue, db.get_colval("nationality", "nat_desc", "nat_code='" + dtg.Rows[0]["nat_code"].ToString() + "'"), dtg.Rows[0]["tel_num"].ToString(), dtg.Rows[0]["email"].ToString(),
                        row["ap_paymentform"].ToString(), row["ap_doc_ref"].ToString(), row["ap_dep_amnt"].ToString());
                    //+ gm.toNormalDoubleFormat(row["serv_chg"].ToString())
                }

                //rpt.Show();
            
        }
        //End-Modify by: Roldan 03/27/18

        public void printprev_regform(String lreg_num, String lchkin_date, String lchkin_time, String lres_no, String lrom_code, String lrmrttyp, String lfull_name, String lromrate, String lpax, String lromtype, String laddress, String larr_dt, String larr_time, String ldep_dt, String ldep_time, String lnoofnights, String lremark, String lpaymenttype, String ldoc_reference, String ldoctype, String ldep_amt, String lclerk, String lromratedurtype, String ldur_title, String lpassportno, String lpp_valdt, String lplace_issue, String ldt_issue, String lnationality, String lcontactno, String lemail, String lap_paymenttype, String lap_doc_reference, String lap_amt)
        {

            reg_num = lreg_num;
            chkin_date = lchkin_date;
            chkin_time = lchkin_time;
            res_no = lres_no;
            rom_code = lrom_code;
            full_name = lfull_name;
            romrate = lromrate;
            pax = lpax;
            romtype = lromtype;
            address = laddress;
            arr_dt = larr_dt;
            arr_time = larr_time;
            dep_dt = ldep_dt;
            dep_time = ldep_time;
            noofnights = lnoofnights;
            remark = lremark;
            paymenttype = lpaymenttype;
            doc_reference = ldoc_reference;
            doctype = ldoctype;
            dep_amt = ldep_amt;
            clerk = lclerk;
            romratedurtype = lromratedurtype;
            dur_title = ldur_title;
            passportno = lpassportno;
            pp_valdt = lpp_valdt;
            //pp_dt_place_issue = lpp_dt_place_issue;
            place_issue= lplace_issue;
            dt_issue = ldt_issue;
            nationality = lnationality;
            contactno = lcontactno;
            email = lemail;
            rmrttyp = lrmrttyp;
            ap_paymenttype = lap_paymenttype;
            ap_doc_reference = lap_doc_reference;
            ap_dep_amt = lap_amt;

            action = 5;

            bgWorker.RunWorkerAsync();
        }

        public void printcurr_gfolio(String lreg_num, String lacct_no, String lrom_code, String lfull_name, String lromrate, String lpax, String lromtype, String laddress, String larr_dt, String larr_time, String ldep_dt, String ldep_time, String lnoofnights, String ltotalcharges, String ltotalpayments, String ldisc_pct, String lgrossrate)
        {
            reg_num = lreg_num;
            acct_no = lacct_no;
            rom_code = lrom_code;
            full_name = lfull_name;
            romrate = lromrate;
            pax = lpax;
            romtype = lromtype;
            address = laddress;
            arr_dt = larr_dt;
            arr_time = larr_time;
            dep_dt = ldep_dt;
            dep_time = ldep_time;
            disc_pct = ldisc_pct;
            grossrate = lgrossrate;
            noofnights = lnoofnights;
            totalcharges = ltotalcharges;
            totalpayments = ltotalpayments;

            action = 6;

            bgWorker.RunWorkerAsync();
        }
        public void printprev_gfolio(String lreg_num, String lrom_code, String lfull_name, String lromrate, String lpax, String lromtype, String laddress, String larr_dt, String larr_time, String ldep_dt, String ldep_time, String lnoofnights, String ltotalcharges, String ltotalpayments, String ldisc_pct, String lgrossrate)
        {
            reg_num = lreg_num;
            rom_code = lrom_code;
            full_name = lfull_name;
            romrate = lromrate;
            pax = lpax;
            romtype = lromtype;
            address = laddress;
            arr_dt = larr_dt;
            arr_time = larr_time;
            dep_dt = ldep_dt;
            dep_time = ldep_time;
            disc_pct = ldisc_pct;
            grossrate = lgrossrate;
            noofnights = lnoofnights;
            totalcharges = ltotalcharges;
            totalpayments = ltotalpayments;
            dtfrom = DateTime.Now;
            dtto = DateTime.Now;

            action = 12;

            bgWorker.RunWorkerAsync();
        }
        public void printprev_gfolio(String lreg_num, String lacct_no, String lrom_code, String lfull_name, String lromrate, String lpax, String lromtype, String laddress, String larr_dt, String larr_time, String ldep_dt, String ldep_time, String lnoofnights, String ltotalcharges, String ltotalpayments, String ldisc_pct, String lgrossrate, String ldtfrom, String ldtto)
        {
            reg_num = lreg_num;
            acct_no = lacct_no;
            rom_code = lrom_code;
            full_name = lfull_name;
            romrate = lromrate;
            pax = lpax;
            romtype = lromtype;
            address = laddress;
            arr_dt = larr_dt;
            arr_time = larr_time;
            dep_dt = ldep_dt;
            dep_time = ldep_time;
            disc_pct = ldisc_pct;
            grossrate = lgrossrate;
            noofnights = lnoofnights;
            totalcharges = ltotalcharges;
            totalpayments = ltotalpayments;
            dtfrom = DateTime.Parse(ldtfrom);
            dtto = DateTime.Parse(ldtto);

            action = 12;

            bgWorker.RunWorkerAsync();
        }
        public void printprev_gfhist(String lreg_num, String lrom_code, String lfull_name, String lromrate, String lpax, String lromtype, String laddress, String larr_dt, String larr_time, String ldep_dt, String ldep_time, String lnoofnights, String ltotalcharges, String ltotalpayments, String ldisc_pct, String lgrossrate)
        {
            reg_num = lreg_num;
            rom_code = lrom_code;
            full_name = lfull_name;
            romrate = lromrate;
            pax = lpax;
            romtype = lromtype;
            address = laddress;
            arr_dt = larr_dt;
            arr_time = larr_time;
            dep_dt = ldep_dt;
            dep_time = ldep_time;
            disc_pct = ldisc_pct;
            grossrate = lgrossrate;
            noofnights = lnoofnights;
            totalcharges = ltotalcharges;
            totalpayments = ltotalpayments;

            action = 9;

            bgWorker.RunWorkerAsync();
        }

        public void printprev_romtransferform(String lreg_num, String lchg_num, String lfull_name, String lrom_code, String lromrate, String lromtype, String lrom_code2, String lromrate2, String lromtype2, String larr_dt, String ldep_dt, String lremark, String lfoclerk, String lapprovedby, String lreceivedby)
        {
            reg_num = lreg_num;
            chg_num = lchg_num;
            full_name = lfull_name;
            rom_code = lrom_code;
            romrate = lromrate;
            romtype = lromtype;
            rom_code2 = lrom_code2;
            romrate2 = lromrate2;
            romtype2 = lromtype2;
            arr_dt = larr_dt;
            dep_dt = ldep_dt;
            remark = lremark;
            foclerk = lfoclerk;
            approvedby = lapprovedby;
            receivedby = lreceivedby;

            action = 7;

            bgWorker.RunWorkerAsync();
        }

        public void print_summarytoor(String reg_num)
        {
            ReportDocument lmyReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            ParameterFieldDefinitions lcrParameterFieldDefinitions;
            ParameterFieldDefinition lcrParameterFieldDefinition;
            ParameterValues lcrParameterValues;
            ParameterDiscreteValue lcrParameterDiscreteValue;
            String WHERE = "";

            // try
            //{
            DataTable dt_summary_sql = db.QueryBySQLCode("SELECT cf.chg_code, c.chg_desc, SUM(cf.amount), c.sc_rep, vat_incl FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num='" + reg_num + "' GROUP BY cf.chg_code, c.chg_desc, c.sc_rep, vat_incl");
            DataTable dt_summary = new DataTable();

            dt_summary.Columns.Add("chg_code", typeof(String));
            dt_summary.Columns.Add("chg_desc", typeof(String));
            dt_summary.Columns.Add("amount", typeof(String));
            dt_summary.Columns.Add("vat", typeof(String));
            dt_summary.Columns.Add("sc", typeof(String));
            // dt_summary.Columns.Add("totalamount", typeof(String));

            foreach (DataRow row in dt_summary_sql.Rows)
            {
                dt_summary.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), "0.00", "0.00");
            }

            lmyReportDocument.Load(fileloc_hotel + "summarytoor.rpt");
            lmyReportDocument.Database.Tables[0].SetDataSource(dt_summary);

            crptviewer.Invoke(new Action(() =>
            {
                crptviewer.ReportSource = lmyReportDocument;
            }));

            crptviewer.Invoke(new Action(() =>
            {
                crptviewer.Refresh();
            }));
            // }
            //catch (Exception) { }
        }

        public void disp_cc_or(String lcc_no, String lgname, String lrom_code, String lamt_word, String lamt, String lfo_name, String lor_no, String lpaidout_vo_no, String lcc_date)
        {
            cc_no = lcc_no;
            gname = lgname;
            rom_code = lrom_code;
            amt_word = lamt_word;
            amt = lamt;
            fo_name = lfo_name;
            or_no = lor_no;
            paidout_vo_no = lpaidout_vo_no;
            cc_date = lcc_date;

            action = 8;

            bgWorker.RunWorkerAsync();
        }

        public void print_highbal_rptlist()
        {
            action = 10;

            bgWorker.RunWorkerAsync();
        }

        public void print_highbal_letter(String reg_num)
        {
            action = 11;

            bgWorker.RunWorkerAsync();
        }

        private void clr_param()
        {
            try
            {
                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
            }
            catch { }
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
            try
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
            catch (Exception) { }
        }

        private void inc_pbar(int i)
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    pbar.Value += i;
                }));
                reset_pbar();
            }
            catch (Exception)
            {
                reset_pbar();
            }
        }

        private void reset_pbar()
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    pbar.Value = 0;
                }));
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
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
            try
            {
                pnl_pbar.Invoke(new Action(() =>
                {
                    pnl_pbar.Show();
                }));
            }
            catch { }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ReportDocument myReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            ParameterFieldDefinitions crParameterFieldDefinitions;
            String WHERE = "";

            pbar_panl_show();

            if (action == 1)
            {
                inc_pbar(5);

                myReportDocument.Load(fileloc_hotel + "302_cashreport.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(newdt);
                inc_pbar(5);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = dsr_rpt_no;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dsrno"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = total_or.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["total_or"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = total_amnt.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["total_csh"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = total_fb.ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["total_food"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(20);
                //disp_reportviewer(myReportDocument);
            }
            else if (action == 2)
            {
                inc_pbar(5);

                myReportDocument.Load(fileloc_hotel + "304_creditcardreport.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(newdt);
                inc_pbar(5);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = dsr_rpt_no;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dsrno"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = total_or;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["total_or"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = total_amnt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["total_cc"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = total_fb;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["total_food"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(40);
                //disp_reportviewer(myReportDocument);
            }
            else if (action == 3)
            {
                if (clerk != "")
                {
                    WHERE = WHERE + " AND cf.user_id='" + clerk + "'";
                }

                inc_pbar(5);
                db.clear_temp_chghist(rpt_no);
                myReportDocument.Load(fileloc_hotel + "303_cashcreditreport.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                inc_pbar(5);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["trnx_dt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = hotel_sales;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["hotel_sales"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = fb_sales;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["fb_sales"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = extra_sales;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["extra_sales"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = cc_issued;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["cc_issued"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = cc_applied;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["cc_applied"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = total_sales;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["total_sales"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = actual_cash;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["actual_cash"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = difference;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["difference"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(20);
                //disp_reportviewer(myReportDocument);
            }
            else if (action == 4)
            {
                inc_pbar(5);
                dt = db.get_extraitemsales(clerk, dt_frm, dt_to);
                myReportDocument.Load(fileloc_hotel + "extraitemsales.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);

                if (clerk == "")
                {
                    clerk = "All";
                }

                inc_pbar(5);
                crParameterDiscreteValue.Value = dt_frm;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_frm"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = dt_to;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dt_to"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(5);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user_id"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(40);
                //disp_reportviewer(myReportDocument);
            }
            else if (action == 5)
            {

                inc_pbar(4);
                myReportDocument.Load(fileloc_hotel + "arvlchkin_register.rpt");

                inc_pbar(4);
                crParameterDiscreteValue.Value = reg_num;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reg_num"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                String nguest = "";
                //int npax = 1;
                //DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfguest WHERE reg_num='" + reg_num + /"'");
                //if (dt != null)
                //{
                //    npax += dt.Rows.Count;
                //}
                for (int i = 0; i < 4; i++)
                {
                    /*nguest = "- - - - - - - - - - - - - - - - - - - - - -";
                    if (dt != null && i < dt.Rows.Count)
                    {
                        nguest = dt.Rows[i]["full_name"].ToString();
                    }*/
                    inc_pbar(4);
                    crParameterDiscreteValue.Value = nguest;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["guest" + (i + 1).ToString()];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();
                }

                DateTime start_dt = Convert.ToDateTime(arr_dt), end_dt = Convert.ToDateTime(dep_dt);
                double nonight = (end_dt - start_dt).TotalDays;

                String rmrttyp = this.rmrttyp, duration_desc = "";
                if (rmrttyp == "M")
                {
                    double mo = 0, yr = 0;
                    int day = 0, fday = Convert.ToInt32(start_dt.ToString("dd")) - 1, tday = Convert.ToInt32(end_dt.ToString("dd")), lastfday = Convert.ToInt32(DateTime.Parse(start_dt.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToString("dd")), lasttday = Convert.ToInt32(DateTime.Parse(end_dt.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToString("dd"));

                    while (start_dt.CompareTo(end_dt.AddMonths(1)) < 0)
                    {
                        if (start_dt.ToString("yyyy-MM") == end_dt.ToString("yyyy-MM"))
                        {
                            if (fday > tday)
                            {
                                mo--;
                                day = (lastfday - (fday + 1)) + tday;
                            }
                            else if (lasttday == tday - fday)
                            {
                                mo++;
                            }
                            else
                            {
                                day = tday - fday;
                            }

                            break;
                        }
                        start_dt = start_dt.AddMonths(1);
                        mo++;
                    }

                    if (day != 0) mo++;

                    if (mo >= 12)
                    {
                        yr = mo / 12;
                        mo = mo % 12;

                        duration_desc = nte.changeNumericToWords(yr.ToString("0")).Trim() + "(" + yr.ToString("0") + ")Year(s)";
                        if (mo != 0)
                        {
                            duration_desc += " AND " + nte.changeNumericToWords(mo.ToString("0")).Trim() + "(" + mo.ToString("0") + ")Month(s)";
                        }
                    }
                    else
                    {
                        duration_desc = nte.changeNumericToWords(mo.ToString("0")).Trim() + "(" + mo.ToString("0") + ")Month(s)";
                    }
                }
                else if (rmrttyp == "W")
                {
                    double wk = 0, day = 0;
                    if (nonight > 7)
                    {
                        wk = nonight / 7;
                        day = nonight % 7;
                    }
                    else
                    {
                        wk = 1;
                    }

                    duration_desc = nte.changeNumericToWords(wk.ToString("0")).Trim() + "(" + wk.ToString("0") + ")Week(s)";
                    if (day != 0)
                    {
                        duration_desc += " AND " + nte.changeNumericToWords(day.ToString("0")).Trim() + "(" + day.ToString("0") + ")Day(s)";
                    }
                }
                else
                {
                    duration_desc = nte.changeNumericToWords(nonight.ToString("0")).Trim() + "(" + nonight.ToString("0") + ")Day(s)";
                }


                inc_pbar(4);
                crParameterDiscreteValue.Value = chkin_date;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["chkin_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = chkin_time;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["chkin_time"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = res_no;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["res_code"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = rom_code;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["rom_code"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = full_name;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["full_name"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(romrate));
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romrate"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = pax;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["pax"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();
                
                inc_pbar(4);
                crParameterDiscreteValue.Value = romtype;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romtype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = address;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["address"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = arr_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["arr_dt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = arr_time; 
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["arr_time"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = dep_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_dt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = dep_time;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_time"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = duration_desc;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofnights"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = clerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = remark;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["remark"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = paymenttype;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["paymenttype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = doctype;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["doctype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = doc_reference;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_reference"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(dep_amt));
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_amt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = ap_paymenttype;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["ap_paymenttype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = ap_doc_reference;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["ap_reference"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(ap_dep_amt));
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["ap_amt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = romratedurtype;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romrate_durtype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = dur_title;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dur_title"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = passportno;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["passportno"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = pp_valdt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["pp_valdt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = dt_issue;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["date_issue"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = place_issue;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["place_issue"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = nationality;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["nationality"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = contactno;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["contactno"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = email;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["email"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();


                inc_pbar(4);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();


                inc_pbar(4);
                //disp_reportviewer(myReportDocument);
            }
            else if (action == 6)
            {
                inc_pbar(10);
                String at_code = db.get_colval("m06", "at_code", "d_code='" + acct_no + "'");

                //DataTable dt_charge = db.get_guest_chargefil(reg_num);
                DataTable dt_charge = db.QueryBySQLCode("SELECT cf.chg_date AS t_date, cf.t_time, cf.user_id, c.chg_desc as description, cf.doc_type, cf.\"reference\", cf.amount as totalamount FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON cf.chg_code=c.chg_code WHERE cf.reg_num ='" + reg_num + "' ORDER BY cf.chg_date desc, c.postcharge, c.utility, cf.t_time desc");
                
                DataTable dt_prevbill = db.QueryBySQLCode("SELECT COALESCE(SUM(cf.amount),0.00) as totalamount FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON cf.chg_code=c.chg_code LEFT JOIN rssys.gfolio gf ON gf.reg_num=cf.reg_num WHERE gf.acct_no ='" + acct_no + "' AND cf.reg_num<>'" + reg_num + "' AND chg_type='C' ");

                DataTable dt_prevbill_ar = db.QueryBySQLCode("SELECT _cur30days + _3160days + _6190days + _91120days + _over120days AS total_balance FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM (SELECT t1.t_desc, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN to_char(inv.t_date,'MM/dd/yyyy')  ELSE to_char(t1.t_date,'MM/dd/yyyy')  END AS t_date, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END AS t_date2, (('" + arr_dt + "')::date - (CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END)) AS mo, COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00) AS amnt, t2.sl_name, t2.sl_code, t2.invoice FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1 AND d_code='" + acct_no + "')  om6 ON (om6.link=COALESCE(m6.d_oldcode,m6.d_code)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<('" + arr_dt + "')::date) LEFT JOIN (SELECT t2.invoice, t2.sl_code, SUM(COALESCE(t2.debit,0.00) + ( COALESCE(t2.credit,0.00) * -1)) AS amnt, CASE WHEN COALESCE(drt2.invoice,'')<>'' THEN 'Y' ELSE 'N' END AS is_invoice FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1 AND d_code='" + acct_no + "')  om6 ON (om6.link=COALESCE(m6.d_oldcode,m6.d_code)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<('" + arr_dt + "')::date)  LEFT JOIN rssys.tr02 drt2 ON (drt2.invoice=t2.invoice AND drt2.sl_code=t2.sl_code AND drt2.debit<>0)   WHERE t2.credit<>0 AND COALESCE(t2.invoice,'')<>''     AND t2.at_code = '" + at_code + "' AND t1.branch='" + GlobalClass.branch + "'            GROUP BY t2.invoice, t2.sl_code, drt2.invoice ORDER BY t2.sl_code, t2.invoice) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code AND crt2.is_invoice='Y')    LEFT JOIN (SELECT max(t1.t_date) AS t_date, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND inv.t_date<('" + arr_dt + "')::date)     WHERE (t2.debit<>0 OR COALESCE(crt2.amnt,0)=0)      AND t2.at_code = '" + at_code + "' AND t1.branch='" + GlobalClass.branch + "'            ) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE (COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0) AND ((COALESCE(_cur30days,0) + COALESCE(_3160days,0) + COALESCE(_6190days,0) + COALESCE(_91120days,0) + COALESCE(_over120days,0)))<>0 AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%')");
               

                //db.QueryBySQLCode("SELECT cf.t_date, cf.t_time, cf.user_id, c.chg_desc AS description, cf.doc_type, CASE WHEN cf.chg_code = '102' OR cf.chg_code='103' OR cf.chg_code='104' OR cf.chg_code='105' THEN cf.reference ||' '|| cf.ccrd_no ||'-'|| cf.trace_no ELSE cf.reference END, cf.amount AS totalamount, c.chg_type, CASE WHEN c.chg_type='P' THEN 'Payment' WHEN cf.chg_code='011' THEN 'Food and Beverage' WHEN cf.chg_code='007' THEN 'Paid Out' WHEN c.chg_type ='C' AND cf.chg_code != '011' AND cf.chg_code != '007' THEN 'Other Charges' END AS status FROM " + _schema + ".chgfil cf RIGHT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num='" + reg_num + "' AND cf.chg_code !='001' AND cf.chg_code != '002' AND cf.chg_code != '018' AND cf.chg_code != '005' AND cf.chg_code != '006' ORDER BY cf.t_date ASC, cf.t_time ASC");
                //DataTable dt_summary_sql = db.QueryBySQLCode("SELECT cf.chg_code, c.chg_desc, SUM(cf.amount), (SELECT SUM(cf1.amount) FROM " + _schema + ".chgfil cf1 WHERE cf1.reg_num='" + reg_num + "' AND cf1.chg_code='005' AND (cf.chg_code='001' OR cf.chg_code='018' OR cf.chg_code='002')) AS govt, (SELECT SUM(cf1.amount) FROM " + _schema + ".chgfil cf1 WHERE cf1.reg_num='" + reg_num + "' AND cf1.chg_code='006' AND (cf.chg_code='001' OR cf.chg_code='018' OR cf.chg_code='002')) AS sc, c.sc_rep, vat_incl, SUM(vat_amnt) AS vat_amnt, SUM(sc_amnt) AS sc_amnt  FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num='" + reg_num + "' AND (cf.chg_code != '005' AND cf.chg_code != '006' AND cf.chg_code != '007' AND cf.chg_code != '008') AND c.chg_type='C' GROUP BY cf.chg_code, c.chg_desc, c.sc_rep, vat_incl ORDER BY  cf.chg_code");

                Double totalpayment = 0.00, transfbal = 0.00, paidout = 0.00;
                Double balance = 0.00, grand_amnt = 0.00;
                String prevbill = "0.00";
                try { balance += gm.toNormalDoubleFormat(dt_prevbill.Rows[0]["totalamount"].ToString()); } catch { }
                try { balance += gm.toNormalDoubleFormat(dt_prevbill_ar.Rows[0]["total_balance"].ToString()); } catch { }
                prevbill = balance.ToString("0.00");

                // DataTable dt_summary = new DataTable();
                Double sc_amnt = 0.00, vat_amnt = 0.00, lessdiscount = 0.00, lessdisc_amt = 0.00;
                String status = "Balance", disc_name = "Discount";

                /*
                dt_summary.Columns.Add("chg_code", typeof(String));
                dt_summary.Columns.Add("description", typeof(String));
                dt_summary.Columns.Add("amount", typeof(double));
                dt_summary.Columns.Add("vat", typeof(double));
                dt_summary.Columns.Add("sc", typeof(double));
                dt_summary.Columns.Add("totalamount", typeof(double));
                dt_summary.Columns.Add("sumry_status", typeof(String));
                
                foreach (DataRow row in dt_summary_sql.Rows)
                {
                    inc_pbar(1);
                    String sumry_status = "1";

                    if(row[0].ToString() == "001" || row[0].ToString() == "018" || row[1].ToString() == "002")
                    {
                        try
                        {
                            vat_amnt = Convert.ToDouble(row[3].ToString());
                        }
                        catch (Exception)
                        {
                            vat_amnt = 0.00;
                        }
                        try
                        {
                            sc_amnt = Convert.ToDouble(row[4].ToString());
                        }
                        catch (Exception)
                        {
                            sc_amnt = 0.00;
                        }
                        row[2] = (Convert.ToDouble(row[2].ToString()) + vat_amnt + sc_amnt).ToString();
                    }
                    else if (row[0].ToString() == "011")
                    {
                        try
                        {
                            vat_amnt = Convert.ToDouble(row[7].ToString());
                        }
                        catch (Exception)
                        {
                            vat_amnt = 0.00;
                        }
                        try
                        {
                            sc_amnt = Convert.ToDouble(row[8].ToString());
                        }
                        catch (Exception)
                        {
                            sc_amnt = 0.00;
                        }

                        sumry_status = "2";
                    }
                    else
                    {
                        if (row[6].ToString() == "Y" && row[5].ToString() == "Y")
                        {
                            vat_amnt = db.get_tax(Convert.ToDouble(row[2].ToString()), lessdiscount, lessdisc_amt);
                            sc_amnt = db.get_svccharge(Convert.ToDouble(row[2].ToString()), lessdiscount, lessdisc_amt);
                        }
                        else
                        {
                            //vat
                            if (String.IsNullOrEmpty(row[3].ToString()) && row[6].ToString() == "Y")
                            {
                                try
                                {
                                    vat_amnt = Convert.ToDouble(row[2].ToString()) - (Convert.ToDouble(row[2].ToString()) / 1.12);
                                }
                                catch (Exception) { vat_amnt = 0.00; }
                            }
                            else
                            {
                                vat_amnt = 0.00;
                            }
                            //service charge
                            if (String.IsNullOrEmpty(row[4].ToString()) && row[5].ToString() == "Y")
                            {
                                try
                                {
                                    sc_amnt = Convert.ToDouble(row[2].ToString()) - (Convert.ToDouble(row[2].ToString()) / 1.11);
                                }
                                catch (Exception) { sc_amnt = 0.00; }
                            }
                            else
                            {
                                sc_amnt = 0.00;
                            }
                        }
                    }

                    //dt_summary.Rows.Add(row[0].ToString(), row[1].ToString(), (Convert.ToDouble(row[2].ToString()) - (vat_amnt + sc_amnt)).ToString("0.00"), vat_amnt.ToString("0.00"), sc_amnt.ToString("0.00"), row[2].ToString(), sumry_status);

                    grand_amnt += Convert.ToDouble(row[2].ToString());
                    //grand_vat += vat_amnt;
                    //grand_sc += sc_amnt;
                }

                transfbal = db.get_transfbal(reg_num);
                paidout = db.get_paidout(reg_num);
                totalpayment = db.get_guestchkin_totalpayment(reg_num);
                //totalpayment = Math.Abs(totalpayment);
                //MessageBox.Show(grand_amnt.ToString("0.00") + ", " +totalpayment.ToString("0.00"));
                
                balance = (grand_amnt + transfbal + paidout) - Math.Abs(totalpayment);

                if (balance >= 0)
                {
                    status = "Guest Balance";
                }
                else
                {
                    status = "Guest Changed";
                }

                if (Convert.ToDouble(disc_pct) != 0)
                {
                    disc_name = "Discount(%)";                    
                }
                else
                {
                    disc_name = "";
                    disc_pct = "";
                } */

                inc_pbar(5);
                myReportDocument.Load(fileloc_hotel + "CrystalReport1.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt_charge);
                // myReportDocument.Subreports[0].Database.Tables[0].SetDataSource(dt_summary);
                /*
               inc_pbar(2);
               crParameterDiscreteValue.Value = paidout.ToString("0.00");
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["paidout"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();

               inc_pbar(2);
               crParameterDiscreteValue.Value = Math.Abs(totalpayment).ToString("0.00");
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["payment"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();


               inc_pbar(2);
               crParameterDiscreteValue.Value = balance.ToString("0.00");
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["balance"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();

               inc_pbar(2);
               crParameterDiscreteValue.Value = status;
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["status"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param(); 
                 
                inc_pbar(2);
               crParameterDiscreteValue.Value = transfbal.ToString("0.00");
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["transfbal"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();
                 
                inc_pbar(2);
               crParameterDiscreteValue.Value = totalcharges;
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["charge"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();                
                */
                inc_pbar(2);
                crParameterDiscreteValue.Value = disc_pct;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["discount"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = prevbill;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["prevbill"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = disc_name;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["disc_name"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = reg_num;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reg_num"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = rom_code;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["rom_code"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = full_name;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["guest"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = grossrate;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romrate"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = pax;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["pax"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = romtype;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romtype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = address;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["address"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = arr_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["arr_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = arr_time;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["arr_time"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = dep_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = dep_time;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_time"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = noofnights;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofnights"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = romrate;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["netrate"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = GlobalClass.user_fullname;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(20);
                //disp_reportviewer(myReportDocument);
            }


            else if (action == 12)
            {

                inc_pbar(10);
                String at_code = db.get_colval("m06", "at_code", "d_code='" + acct_no + "'");

                DataTable dt_charge = db.QueryBySQLCode("SELECT cf.chg_date AS t_date, cf.t_time, cf.user_id, c.chg_desc as description, cf.doc_type, cf.\"reference\", cf.amount as totalamount FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON cf.chg_code=c.chg_code WHERE cf.reg_num ='" + reg_num + "' AND (cf.chg_date BETWEEN '" + dtfrom.ToString("yyyy-MM-dd") + "' AND '" + dtto.ToString("yyyy-MM-dd") + "') AND chg_type='C' ORDER BY cf.chg_date desc, c.postcharge, c.utility, cf.t_time desc");

                DataTable dt_prevbill = db.QueryBySQLCode("SELECT COALESCE(SUM(cf.amount),0.00) as totalamount FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON cf.chg_code=c.chg_code LEFT JOIN rssys.gfolio gf ON gf.reg_num=cf.reg_num WHERE gf.acct_no ='" + acct_no + "' AND cf.chg_date < '" + dtfrom.ToString("yyyy-MM-dd") + "'  AND c.isdeposit='false'");

                DataTable dt_prevbill_ar = db.QueryBySQLCode("SELECT _cur30days + _3160days + _6190days + _91120days + _over120days AS total_balance FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM (SELECT t1.t_desc, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN to_char(inv.t_date,'MM/dd/yyyy')  ELSE to_char(t1.t_date,'MM/dd/yyyy')  END AS t_date, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END AS t_date2, (('" + dtfrom.ToString("yyyy-MM-dd") + "')::date - (CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END)) AS mo, COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00) AS amnt, t2.sl_name, t2.sl_code, t2.invoice FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1 AND d_code='" + acct_no + "')  om6 ON (om6.link=COALESCE(m6.d_oldcode,m6.d_code)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<'" + dtfrom.ToString("yyyy-MM-dd") + "'::date) LEFT JOIN (SELECT t2.invoice, t2.sl_code, SUM(COALESCE(t2.debit,0.00) + ( COALESCE(t2.credit,0.00) * -1)) AS amnt, CASE WHEN COALESCE(drt2.invoice,'')<>'' THEN 'Y' ELSE 'N' END AS is_invoice FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1 AND d_code='" + acct_no + "')  om6 ON (om6.link=COALESCE(m6.d_oldcode,m6.d_code)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<'" + dtfrom.ToString("yyyy-MM-dd") + "'::date)  LEFT JOIN rssys.tr02 drt2 ON (drt2.invoice=t2.invoice AND drt2.sl_code=t2.sl_code AND drt2.debit<>0)   WHERE t2.credit<>0 AND COALESCE(t2.invoice,'')<>''     AND t2.at_code = '" + at_code + "' AND t1.branch='" + GlobalClass.branch + "'            GROUP BY t2.invoice, t2.sl_code, drt2.invoice ORDER BY t2.sl_code, t2.invoice) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code AND crt2.is_invoice='Y')    LEFT JOIN (SELECT max(t1.t_date) AS t_date, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND inv.t_date<'" + dtfrom.ToString("yyyy-MM-dd") + "'::date)     WHERE (t2.debit<>0 OR COALESCE(crt2.amnt,0)=0)     AND t2.at_code = '" + at_code + "' AND t1.branch='" + GlobalClass.branch + "'            ) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE (COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0) AND ((COALESCE(_cur30days,0) + COALESCE(_3160days,0) + COALESCE(_6190days,0) + COALESCE(_91120days,0) + COALESCE(_over120days,0)))<>0 AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%')");

                ///DataTable dt_summary_sql = db.QueryBySQLCode("SELECT cf.chg_code, c.chg_desc, SUM(cf.amount), (SELECT SUM(cf1.amount) FROM " + _schema + ".chgfil cf1 WHERE cf1.reg_num='" + reg_num + "' AND cf1.chg_code='005' AND (cf.chg_code='001' OR cf.chg_code='018' OR cf.chg_code='002')) AS govt, (SELECT SUM(cf1.amount) FROM " + _schema + ".chgfil cf1 WHERE cf1.reg_num='" + reg_num + "' AND cf1.chg_code='006' AND (cf.chg_code='001' OR cf.chg_code='018' OR cf.chg_code='002')) AS sc, c.sc_rep, vat_incl, SUM(vat_amnt) AS vat_amnt, SUM(sc_amnt) AS sc_amnt  FROM " + _schema + ".chgfil cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num='" + reg_num + "' AND (cf.chg_code != '005' AND cf.chg_code != '006' AND cf.chg_code != '007' AND cf.chg_code != '008') AND c.chg_type='C' GROUP BY cf.chg_code, c.chg_desc, c.sc_rep, vat_incl ORDER BY cf.chg_code");

                Double totalpayment = 0.00, transfbal = 0.00, paidout = 0.00;
                Double balance = 0.00, grand_amnt = 0.00;

                // DataTable dt_summary = new DataTable();
                Double sc_amnt = 0.00, vat_amnt = 0.00, lessdiscount = 0.00, lessdisc_amt = 0.00;
                String status = "Balance", disc_name = "Discount";
                String prevbill = "0.00", amount_word;
                try { balance += gm.toNormalDoubleFormat(dt_prevbill.Rows[0]["totalamount"].ToString()); }
                catch { }
                try { balance += gm.toNormalDoubleFormat(dt_prevbill_ar.Rows[0]["total_balance"].ToString()); } catch { }
                prevbill = balance.ToString("0.00");

                Double t_amount = balance;
                for (int i = 0; i < dt_charge.Rows.Count; i++)
                {
                    t_amount += gm.toNormalDoubleFormat(dt_charge.Rows[i]["totalamount"].ToString());
                }
                amount_word = nte.changeNumericToWords(t_amount).ToUpper() + " ONLY (Php " + gm.toAccountingFormat(t_amount) + ")";

                /*
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
                } */

                //totalamount
                inc_pbar(5);
                myReportDocument.Load(fileloc_hotel + "CrystalReport1present.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt_charge);

                inc_pbar(2);
                crParameterDiscreteValue.Value = dtto.ToString("MMMM dd,yyyy");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["periods"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = amount_word;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["amount_word"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = disc_pct;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["discount"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = disc_name;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["disc_name"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = reg_num;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reg_num"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = rom_code;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["rom_code"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = full_name;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["guest"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = grossrate;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romrate"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = pax;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["pax"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = romtype;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romtype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = address;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["address"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = arr_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["arr_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = arr_time;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["arr_time"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = dep_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = dep_time;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_time"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = noofnights;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofnights"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = romrate;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["netrate"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = GlobalClass.user_fullname;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = prevbill;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["prevbill"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(20);
            }
            else if (action == 7)
            {
                inc_pbar(4);
                myReportDocument.Load(fileloc_hotel + "RoomTransferForm.rpt");

                inc_pbar(4);
                crParameterDiscreteValue.Value = reg_num;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reg_num"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = chg_num;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["chg_num"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = full_name;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["full_name"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = rom_code;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["rom_code"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = romrate;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romrate"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = romtype;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romtype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = rom_code2;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["rom_code_2"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = romrate2;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romrate_2"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = romtype2;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romtype_2"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = arr_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["arr_dt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = dep_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_dt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = remark;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["remark"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = foclerk;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["clerk"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = approvedby;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["approvedby"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = receivedby;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["receivedby"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(10);
                //disp_reportviewer(myReportDocument);
            }
            else if (action == 8)
            {
                inc_pbar(4);
                myReportDocument.Load(fileloc_hotel + "cc_acknoledgementreport.rpt");

                inc_pbar(4);
                crParameterDiscreteValue.Value = cc_no;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["cc_no"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = gname;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["gname"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = rom_code;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["rom_code"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = amt_word;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["amt_word"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = amt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["amt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = fo_name;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["fo_name"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = or_no;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["or_no"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = paidout_vo_no;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["paidout_vo_no"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(4);
                crParameterDiscreteValue.Value = cc_date;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["cc_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();


                inc_pbar(4);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(20);
                //disp_reportviewer(myReportDocument);
            }
            else if (action == 9)
            {
                inc_pbar(10);
                DataTable dt_charge = db.QueryBySQLCode("SELECT cf.t_date, cf.t_time, cf.user_id, c.chg_desc as description, cf.doc_type, cf.\"reference\", cf.amount as totalamount FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".charge c ON cf.chg_code=c.chg_code WHERE cf.reg_num ='" + reg_num + "' ORDER BY  cf.t_date desc, c.postcharge, c.utility, cf.t_time desc");

                //db.QueryBySQLCode("SELECT cf.t_date, cf.t_time, cf.user_id, c.chg_desc AS description, cf.doc_type, CASE WHEN cf.chg_code = '102' OR cf.chg_code='103' OR cf.chg_code='104' OR cf.chg_code='105' THEN cf.reference ||' '|| cf.ccrd_no ||'-'|| cf.trace_no ELSE cf.reference END, cf.amount AS totalamount, c.chg_type, CASE WHEN c.chg_type='P' THEN 'Payment' WHEN cf.chg_code='011' THEN 'Food and Beverage' WHEN cf.chg_code='007' THEN 'Paid Out' WHEN c.chg_type ='C' AND cf.chg_code != '011' AND cf.chg_code != '007' THEN 'Other Charges' END AS status FROM " + _schema + ".chgfil cf RIGHT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num='" + reg_num + "' AND cf.chg_code !='001' AND cf.chg_code != '002' AND cf.chg_code != '018' AND cf.chg_code != '005' AND cf.chg_code != '006' ORDER BY cf.t_date ASC, cf.t_time ASC");
                //DataTable dt_summary_sql = db.QueryBySQLCode("SELECT cf.chg_code, c.chg_desc, SUM(cf.amount), (SELECT SUM(cf1.amount) FROM " + _schema + ".chghist cf1 WHERE cf1.reg_num='" + reg_num + "' AND cf1.chg_code='005' AND (cf.chg_code='001' OR cf.chg_code='018' OR cf.chg_code='002')) AS govt, (SELECT SUM(cf1.amount) FROM " + _schema + ".chghist cf1 WHERE cf1.reg_num='" + reg_num + "' AND cf1.chg_code='006' AND (cf.chg_code='001' OR cf.chg_code='018' OR cf.chg_code='002')) AS sc, c.sc_rep, vat_incl, SUM(vat_amnt) AS vat_amnt, SUM(sc_amnt) AS sc_amnt  FROM " + _schema + ".chghist cf LEFT JOIN " + _schema + ".charge c ON c.chg_code=cf.chg_code WHERE cf.reg_num='" + reg_num + "' AND (cf.chg_code != '005' AND cf.chg_code != '006' AND cf.chg_code != '007' AND cf.chg_code != '008') AND c.chg_type='C' GROUP BY cf.chg_code, c.chg_desc, c.sc_rep, vat_incl ORDER BY cf.chg_code");

                Double totalpayment = 0.00, transfbal = 0.00, paidout = 0.00;
                Double balance = 0.00, grand_amnt = 0.00;

                DataTable dt_summary = new DataTable();
                Double sc_amnt = 0.00, vat_amnt = 0.00, lessdiscount = 0.00, lessdisc_amt = 0.00;
                String status = "Balance", disc_name = "";


                /*
                dt_summary.Columns.Add("chg_code", typeof(String));
                dt_summary.Columns.Add("description", typeof(String));
                dt_summary.Columns.Add("amount", typeof(double));
                dt_summary.Columns.Add("vat", typeof(double));
                dt_summary.Columns.Add("sc", typeof(double));
                dt_summary.Columns.Add("totalamount", typeof(double));
                dt_summary.Columns.Add("sumry_status", typeof(String));
                
                foreach (DataRow row in dt_summary_sql.Rows)
                {
                    inc_pbar(1);
                    String sumry_status = "1";

                    if(row[0].ToString() == "001" || row[0].ToString() == "018" || row[1].ToString() == "002")
                    {
                        try
                        {
                            vat_amnt = Convert.ToDouble(row[3].ToString());
                        }
                        catch (Exception)
                        {
                            vat_amnt = 0.00;
                        }
                        try
                        {
                            sc_amnt = Convert.ToDouble(row[4].ToString());
                        }
                        catch (Exception)
                        {
                            sc_amnt = 0.00;
                        }
                        row[2] = (Convert.ToDouble(row[2].ToString()) + vat_amnt + sc_amnt).ToString();
                    }
                    else if (row[0].ToString() == "011")
                    {
                        try
                        {
                            vat_amnt = Convert.ToDouble(row[7].ToString());
                        }
                        catch (Exception)
                        {
                            vat_amnt = 0.00;
                        }
                        try
                        {
                            sc_amnt = Convert.ToDouble(row[8].ToString());
                        }
                        catch (Exception)
                        {
                            sc_amnt = 0.00;
                        }

                        sumry_status = "2";
                    }
                    else
                    {
                        if (row[6].ToString() == "Y" && row[5].ToString() == "Y")
                        {
                            vat_amnt = db.get_tax(Convert.ToDouble(row[2].ToString()), lessdiscount, lessdisc_amt);
                            sc_amnt = db.get_svccharge(Convert.ToDouble(row[2].ToString()), lessdiscount, lessdisc_amt);
                        }
                        else
                        {
                            //vat
                            if (String.IsNullOrEmpty(row[3].ToString()) && row[6].ToString() == "Y")
                            {
                                try
                                {
                                    vat_amnt = Convert.ToDouble(row[2].ToString()) - (Convert.ToDouble(row[2].ToString()) / 1.12);
                                }
                                catch (Exception) { vat_amnt = 0.00; }
                            }
                            else
                            {
                                vat_amnt = 0.00;
                            }
                            //service charge
                            if (String.IsNullOrEmpty(row[4].ToString()) && row[5].ToString() == "Y")
                            {
                                try
                                {
                                    sc_amnt = Convert.ToDouble(row[2].ToString()) - (Convert.ToDouble(row[2].ToString()) / 1.11);
                                }
                                catch (Exception) { sc_amnt = 0.00; }
                            }
                            else
                            {
                                sc_amnt = 0.00;
                            }
                        }
                    }

                    //dt_summary.Rows.Add(row[0].ToString(), row[1].ToString(), (Convert.ToDouble(row[2].ToString()) - (vat_amnt + sc_amnt)).ToString("0.00"), vat_amnt.ToString("0.00"), sc_amnt.ToString("0.00"), row[2].ToString(), sumry_status);

                    grand_amnt += Convert.ToDouble(row[2].ToString());
                    //grand_vat += vat_amnt;
                    //grand_sc += sc_amnt;
                }

                transfbal = db.get_transfbal(reg_num);
                paidout = db.get_paidout(reg_num);
                totalpayment = db.get_guestchkin_totalpayment(reg_num);
                //totalpayment = Math.Abs(totalpayment);
                //MessageBox.Show(grand_amnt.ToString("0.00") + ", " +totalpayment.ToString("0.00"));
                
                balance = (grand_amnt + transfbal + paidout) - Math.Abs(totalpayment);

                if (balance >= 0)
                {
                    status = "Guest Balance";
                }
                else
                {
                    status = "Guest Changed";
                }

                if (Convert.ToDouble(disc_pct) != 0)
                {
                    disc_name = "Discount(%)";                    
                }
                else
                {
                    disc_name = "";
                    disc_pct = "";
                } */

                inc_pbar(5);
                myReportDocument.Load(fileloc_hotel + "CrystalReport1.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt_charge);
                // myReportDocument.Subreports[0].Database.Tables[0].SetDataSource(dt_summary);
                /*
               inc_pbar(2);
               crParameterDiscreteValue.Value = paidout.ToString("0.00");
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["paidout"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();

               inc_pbar(2);
               crParameterDiscreteValue.Value = Math.Abs(totalpayment).ToString("0.00");
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["payment"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();


               inc_pbar(2);
               crParameterDiscreteValue.Value = balance.ToString("0.00");
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["balance"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();

               inc_pbar(2);
               crParameterDiscreteValue.Value = status;
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["status"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param(); 
                 
                inc_pbar(2);
               crParameterDiscreteValue.Value = transfbal.ToString("0.00");
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["transfbal"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();
                 
                inc_pbar(2);
               crParameterDiscreteValue.Value = totalcharges;
               crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
               crParameterFieldDefinition = crParameterFieldDefinitions["charge"];
               crParameterValues = crParameterFieldDefinition.CurrentValues;
               clr_param();                
                */
                inc_pbar(2);
                crParameterDiscreteValue.Value = disc_pct;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["discount"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = disc_name;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["disc_name"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = reg_num;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reg_num"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = rom_code;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["rom_code"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = full_name;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["guest"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = grossrate;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romrate"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = pax;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["pax"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = romtype;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["romtype"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = address;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["address"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = arr_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["arr_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = arr_time;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["arr_time"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = dep_dt;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = dep_time;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["dep_time"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = noofnights;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["noofnights"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = romrate;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["netrate"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["user"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(20);
                //disp_reportviewer(myReportDocument);
            }
            else if (action == 10)
            {
                inc_pbar(4);

                dt = db.get_hibalguestlist();
                myReportDocument.Load(fileloc_hotel + "highbalancelist.rpt");
                myReportDocument.Database.Tables[0].SetDataSource(dt);


                inc_pbar(40);
                crParameterDiscreteValue.Value = GlobalClass.username;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(20);
                //disp_reportviewer(myReportDocument);
            }
            else if (action == 13)
            {
                inc_pbar(10);
                DataTable dguest = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE reg_num='" + reg_num + "'");
                DataTable guest = db.QueryBySQLCode("SELECT * FROM rssys.guest WHERE acct_no='" + dguest.Rows[0]["acct_no"].ToString() + "'");

                String advan_rent = dguest.Rows[0]["ap_dep_amnt"].ToString();
                String sec_dep = dguest.Rows[0]["dep_amnt"].ToString();
                String total = "0.00", duration_desc = "", passport_place = "", rmrttyp = "";

                try { total = (gm.toNormalDoubleFormat(sec_dep) + gm.toNormalDoubleFormat(advan_rent)).ToString("0.00"); }
                catch { }
                try { advan_rent = gm.toNormalDoubleFormat(advan_rent).ToString("0.00"); }
                catch { advan_rent = "0.00"; }
                try { sec_dep = gm.toNormalDoubleFormat(sec_dep).ToString("0.00"); }
                catch { sec_dep = "0.00"; }

                DateTime start_dt = Convert.ToDateTime(dguest.Rows[0]["arr_date"].ToString()), end_dt = Convert.ToDateTime(dguest.Rows[0]["dep_date"].ToString());
                double nonight = (end_dt - start_dt).TotalDays;

                rmrttyp = dguest.Rows[0]["rmrttyp"].ToString();
                if (rmrttyp == "M")
                {
                    double mo = 0, yr = 0;
                    int day = 0, fday = Convert.ToInt32(start_dt.ToString("dd")) - 1, tday = Convert.ToInt32(end_dt.ToString("dd")), lastfday = Convert.ToInt32(DateTime.Parse(start_dt.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToString("dd")), lasttday = Convert.ToInt32(DateTime.Parse(end_dt.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToString("dd"));

                    while (start_dt.CompareTo(end_dt.AddMonths(1)) < 0)
                    {
                        if (start_dt.ToString("yyyy-MM") == end_dt.ToString("yyyy-MM"))
                        {
                            if (fday > tday)
                            {
                                mo--;
                                day = (lastfday - (fday + 1)) + tday;
                            }
                            else if (lasttday == tday - fday)
                            {
                                mo++;
                            }
                            else
                            {
                                day = tday - fday;
                            }

                            break;
                        }
                        start_dt = start_dt.AddMonths(1);
                        mo++;
                    }

                    if (day != 0) mo++;

                    if (mo >= 12)
                    {
                        yr = mo / 12;
                        mo = mo % 12;

                        duration_desc = nte.changeNumericToWords(yr.ToString("0")).Trim() + "(" + yr.ToString("0") + ") Year(s)";
                        if (mo != 0)
                        {
                            duration_desc += " AND " + nte.changeNumericToWords(mo.ToString("0")).Trim() + "(" + mo.ToString("0") + ") Month(s)";
                        }
                    }
                    else
                    {
                        duration_desc = nte.changeNumericToWords(mo.ToString("0")).Trim() + "(" + mo.ToString("0") + ") Month(s)";
                    }
                }
                else if (rmrttyp == "W")
                {
                    double wk = 0, day = 0;
                    if (nonight > 7)
                    {
                        wk = nonight / 7;
                        day = nonight % 7;
                    }
                    else
                    {
                        wk = 1;
                    }

                    duration_desc = nte.changeNumericToWords(wk.ToString("0")).Trim() + "(" + wk.ToString("0") + ") Week(s)";
                    if (day != 0)
                    {
                        duration_desc += " AND " + nte.changeNumericToWords(day.ToString("0")).Trim() + "(" + day.ToString("0") + ") Day(s)";
                    }
                }
                else
                {
                    duration_desc = nte.changeNumericToWords(nonight.ToString("0")).Trim() + "(" + nonight.ToString("0") + ") Day(s)";
                }

                inc_pbar(5);
                myReportDocument.Load(fileloc_hotel + "contract_lease.rpt");
                //myReportDocument.Load(fileloc_hotel + "CrystalReportGraphOccupancy.rpt");

                
                inc_pbar(2);
                crParameterDiscreteValue.Value = duration_desc;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["duration_desc"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = "";
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = reg_num;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["gfolio"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = dguest.Rows[0]["full_name"].ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["tennant"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();


                if (!String.IsNullOrEmpty(guest.Rows[0]["passport_no"].ToString()))
                {
                    passport_place = guest.Rows[0]["passport_place"].ToString() + "(" + DateTime.Parse(guest.Rows[0]["passport_issued"].ToString()).ToString("MMMM d,yyyy") + ")";
                }
                /*
                     duration_desc
                 */
                
                inc_pbar(2);
                crParameterDiscreteValue.Value = db.get_colval("country", "cntry_desc", "cntry_code='" + guest.Rows[0]["cntry_code"].ToString() + "'");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["tennant_country"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = db.get_colval("nationality", "nat_desc", "nat_code='" + guest.Rows[0]["nat_code"].ToString() + "'");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["tennant_citizen"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = guest.Rows[0]["address1"].ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["tennant_addr"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = guest.Rows[0]["passport_no"].ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["evi_id_tenn"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = passport_place;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["pd_issue_tenn"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();


                inc_pbar(2);
                crParameterDiscreteValue.Value = dguest.Rows[0]["rom_code"].ToString();
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["room_code"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = db.get_colval("rtype", "typ_desc", "typ_code='" + dguest.Rows[0]["typ_code"].ToString() + "'");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["rmtype_desc"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = DateTime.Parse(dguest.Rows[0]["t_date"].ToString()).ToString("MMMM d,yyyy");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["contract_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = DateTime.Parse(dguest.Rows[0]["arr_date"].ToString()).ToString("MMMM d,yyyy");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["start_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();
                inc_pbar(2);
                crParameterDiscreteValue.Value = DateTime.Parse(dguest.Rows[0]["dep_date"].ToString()).ToString("MMMM d,yyyy");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["end_date"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();


                inc_pbar(2); crParameterDiscreteValue.Value = "Php" + gm.toAccountingFormat(Double.Parse(advan_rent));
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["amount1"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2); crParameterDiscreteValue.Value = "Php" + gm.toAccountingFormat(Double.Parse(sec_dep));
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["amount2"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = "Php" + gm.toAccountingFormat(Double.Parse(total));
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["amount3"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = GlobalClass.user_fullname;
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["username"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();


                inc_pbar(2);
                crParameterDiscreteValue.Value = db.get_colval("m99", "comp_president", "");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["president"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = db.get_colval("m99", "pres_pd_issue", "");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["pd_issue_pres"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = db.get_colval("m99", "pres_identity", "");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["evi_id_pres"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(2);
                crParameterDiscreteValue.Value = db.get_colval("m99", "check_by", "");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["checkby"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(20);
                 
            }



            if (action != 0)
            {
                inc_pbar(40);
                crParameterDiscreteValue.Value = db.get_pk("comp_name");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["m99company"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(40);
                crParameterDiscreteValue.Value = db.get_pk("comp_addr");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["m99address"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();

                inc_pbar(40);
                crParameterDiscreteValue.Value = db.get_pk("comp_tel");
                crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["m99tel_no"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                clr_param();
                disp_reportviewer(myReportDocument);
            }


            pbar_panl_hide();
        }
    }
}
