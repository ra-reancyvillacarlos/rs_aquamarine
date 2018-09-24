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

namespace Accounting_Application_System
{
    public partial class Report : Form
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        int action = 0;
        String fileloc_acctg = "";
        String fileloc_hotel = "";
        String fileloc_inv = "";
        String fileloc_md = "";
        String fileloc_srvc = "";
        String fileloc_sales = "";
        String soa_code = "";
        String trv_code = "";
        Int32 __month__ = 0;

        ReportDocument myReportDocument;
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        ParameterFieldDefinitions crParameterFieldDefinitions;

        DataTable dt, newdt;
        NumberToEnglish_orig nte = new NumberToEnglish_orig();

        String comp_name, comp_addr, comp_number;
        String j_code = "", j_title, jtype_name;
        String vtitle, refno, refdesc, yr, mo, t_date, paidto, ckdt, ckno, particular, reg_num = "", reference = "";
        String code = "", acct_no = "", client = "", duedate = "", _chg_dtfrm = "", _chg_dtto = "";
        String e_mult = "16.00", e_min = "100.00", w_mult = "75.00", w_min = "100.00";
        String project = "", date_needed = "", requestedby = "", payment = "", blnce_due = "", supplier = "", delv_date = "", notes = "", suppl_code = "";

        String contraacc = "", cc = "", scc = "";
        String trans_no = "", trans_desc = "", locfrom = "", locto = "", t_time = "", stckloc = "";
        String outlet = "", mcardid = "", agent = "", mrk_sgmnt = "";
        String soa_date = "", due_date = "", coll_date = "", collector = "", acc_link = "", pay_thru = "", chk_no = "", chk_date = "";
        String aor = "", terms = "", dp_pct = "", cashprice = "",  sagent = "";
        String vin_no = "",unit = "", brand = "", plate_no = "",engine_no = "", year_model = "", color = "",last_km_reading = "";
        String vat_sales = "", vat_exsales = "", zero_sales = "", vat_amnt = "", vat_less = "", net_vat = "", total_sales = "", amnt_due = "", vat_add = "", total_amnt_due = "";
        String form = "";
        String item_code = "", item_desc = "", purch_unit = "", sales_unit = "", reg_price = "", unit_price = "";
        String branch = "";
        String pr_no="";
        String vsi_no = "", guardby = "", pickupby = "", unitdelby = "", releasedby = "", t_unitdel = "", t_release = "", d_unitdel = "", d_release = "", d_receive = "", remarks = "";
        String preparedby = "", finalizedby = "", approvedby = "";
        String _WHERE = "";
        DataTable dt_trv = null;
        //
        // 
        public Report()
        {
            InitializeComponent();

            try
            {
                thisDatabase db = new thisDatabase();
                myReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                crParameterValues = new ParameterValues();
                crParameterDiscreteValue = new ParameterDiscreteValue();

                String system_loc = db.get_system_loc();

                fileloc_acctg = system_loc + "\\Reports\\Accounting\\";
                fileloc_hotel = system_loc + "\\Reports\\Hotel\\";
                fileloc_inv = system_loc + "\\Reports\\Inventory\\";
                fileloc_md = system_loc + "\\Reports\\MD\\";
                fileloc_srvc = system_loc + "\\Reports\\Service\\";
                fileloc_sales = system_loc + "\\Reports\\Sale\\";

                //fileloc_acctg = "../..\\Reports/Accounting/";
                //fileloc_hotel = "../..\\Reports/Hotel/";
                //fileloc_inv = "../..\\Reports/Inventory/";
                //fileloc_md = "../..\\Reports/MD/";

                crParameterValues = new ParameterValues();
                crParameterDiscreteValue = new ParameterDiscreteValue();
                dt = new DataTable();
                newdt = new DataTable();
            }
            catch (Exception er) { MessageBox.Show("Report Form Error Message:" + er.Message); }
        }
   
        private void Report_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            comp_name = db.get_m99comp_name();
            comp_addr = db.get_m99comp_addr();
            comp_number = db.get_colval("m99","comp_tel","");
        }

        public void print_journalizedhotel(String ljcode, String ldatefrm, String ldateto)
        {
            action = 777;

            j_code = ljcode;
            _chg_dtfrm = ldatefrm;
            _chg_dtto = ldateto;
            
            bgWorker.RunWorkerAsync();
        }
        public void print_journalized(String ljtitle, String ljcode, String ldatefrm, String ldateto, String fy, String WHERE)
        {
            action = 778;

            j_code = ljcode; 
            j_title = ljtitle;
            _chg_dtfrm = ldatefrm;
            _chg_dtto = ldateto;
            _WHERE = WHERE;
            yr = fy;

            bgWorker.RunWorkerAsync();
        }
        public void print_journalized_hotel(String ljtitle, String ljcode, String ldatefrm, String ldateto, String fy, String WHERE)
        {
            action = 779;

            j_code = ljcode;
            j_title = ljtitle;
            _chg_dtfrm = ldatefrm;
            _chg_dtto = ldateto;
            _WHERE = WHERE;
            yr = fy;

            bgWorker.RunWorkerAsync();
        }

        public void print_voucher(String lj_code, String lvtitle, String lrefno, String lrefdesc, String lyr, String lmo, String lt_date, String lpaidto, String lckdt, String lckno, String lparticular, DataTable ldt)
        {
            action = 1;

            j_code = lj_code;
            vtitle = lvtitle;
            refno = lrefno;
            refdesc = lrefdesc;
            yr = lyr;
            mo = lmo;
            t_date = lt_date;
            paidto = lpaidto;
            ckdt = lckdt;
            ckno = lckno;
            particular = lparticular;
            dt = ldt;

            bgWorker.RunWorkerAsync();
        }

        public void print_check(String lj_code, String lj_num, String lpaidto, String lckdt)
        {
            action = 3;

            j_code = lj_code;
            refno = lj_num;
            paidto = lpaidto;
            ckdt = lckdt;

            bgWorker.RunWorkerAsync();
        }

        public void print_soa(String soa_number, String billedclient, String t_date, String due_date, String chg_dtfrm, String chg_dtto, String reg_num)
        {
            action = 2;

            code = soa_number;

            client = billedclient;
            this.t_date = t_date;
            _chg_dtfrm = chg_dtfrm;
            this.duedate = due_date;
            _chg_dtto = chg_dtto;
            this.reg_num = reg_num;

            bgWorker.RunWorkerAsync();
        }

        public void print_soa_list(String soa_number, String billedclient, String t_date, String due_date, String chg_dtfrm, String chg_dtto, String reg_num)
        {
            action = 2;

            code = soa_number;

            client = billedclient;
            this.t_date = t_date;
            _chg_dtfrm = chg_dtfrm;
            this.duedate = due_date;
            _chg_dtto = chg_dtto;
            this.reg_num = reg_num;

            bgWorker.RunWorkerAsync();
        }

        public void print_soa_warehouse(String soa_number, String billedclient, String t_date, String chg_dtfrm, String chg_dtto, String reg_num)
        {
            action = 4;

            code = soa_number;

            client = billedclient;
            this.t_date = t_date;
            _chg_dtfrm = chg_dtfrm;
            _chg_dtto = chg_dtto;
            this.reg_num = reg_num;

            bgWorker.RunWorkerAsync();
        }
        
        public void print_purchaserequest(String lpr_number, String lproject, String lrequestedby, String ldate_needed, String lt_date)
        {
            action = 20;

            code = lpr_number;
            project = lproject;
            requestedby = lrequestedby;
            date_needed = ldate_needed;
            t_date = lt_date;

            bgWorker.RunWorkerAsync();
        }

        public void print_purchaseorder(String lpr_number, String _pr_no, String lsupplier, String lpayment, String ldelvdate, String lnotes, String lt_date, String lsupplier_code, String lreference, String lreqby)
        {
            action = 21;

            code = lpr_number;
            supplier = lsupplier;
            suppl_code = lsupplier_code;
            payment = lpayment;
            delv_date = ldelvdate;
            t_date = lt_date;
            notes = lnotes;
            reference = lreference;
            pr_no = _pr_no;
            requestedby = lreqby;

            bgWorker.RunWorkerAsync();
        }


        public void print_purchaseorder(String lpr_number, String _pr_no, String lsupplier, String lpayment, String ldelvdate, String lnotes, String lt_date, String lsupplier_code, String lreference, String lrequestedby, String lpreparedby, String lfinalizedby, String lapprovedby)
        {
            action = 21;

            code = lpr_number;
            supplier = lsupplier;
            suppl_code = lsupplier_code;
            payment = lpayment;
            delv_date = ldelvdate;
            t_date = lt_date;
            notes = lnotes;
            reference = lreference;
            pr_no = _pr_no;
            preparedby = lpreparedby;
            finalizedby = lfinalizedby;
            approvedby = lapprovedby;
            requestedby = lrequestedby;

            bgWorker.RunWorkerAsync();
        }
        //*here


        public void print_purchase_requestion(String _ro_num)
        {
            action = 4026;
            code = _ro_num;

            bgWorker.RunWorkerAsync();
        }
        public void print_invoice_billing(String _ro_num)
        {
            action = 4027;
            code = _ro_num;

            bgWorker.RunWorkerAsync();
        }
        public void print_ro_invoice(String _ro_num)
        {
            action = 4028;
            code = _ro_num;

            bgWorker.RunWorkerAsync();
        }
        public void print_job_order(String _ro_num)
        {
            action = 4029;
            code = _ro_num;

            bgWorker.RunWorkerAsync();
        }
        public void print_ziebart_billing(String _ro_num)
        {
            action = 4129; 
            code = _ro_num;

            bgWorker.RunWorkerAsync();
        }
        public void print_ziebart_warranty(String _ro_num)
        {
            action = 4229;
            code = _ro_num;

            bgWorker.RunWorkerAsync();
        }

        public void print_receving_po(String _inv_num)
        {
            action = 4031;
            code = _inv_num;

            bgWorker.RunWorkerAsync();
        }
        public void print_direct_purchase(String _inv_num)
        {
            action = 4032;
            code = _inv_num;

            bgWorker.RunWorkerAsync();
        }
        public void print_purchase_return(String _inv_num)
        {
            action = 4033;
            code = _inv_num;

            bgWorker.RunWorkerAsync();
        }
        public void print_stock_issuance(String _inv_num,String _contraacc, String _cc, String _scc)
        {
            action = 4034;
            code = _inv_num;
            contraacc = _contraacc; 
            cc = _cc;
            scc = _scc;

            bgWorker.RunWorkerAsync();
        }
        public void print_stock_transfer(String _trans_no, String _trans_desc, String _locfrom, String _locto, String _t_date, String _t_time)
        {
            action = 4035;
            trans_no = _trans_no;
            trans_desc = _trans_desc;
            locfrom = _locfrom;
            locto = _locto;
            t_date = _t_date;
            t_time = _t_time;

            bgWorker.RunWorkerAsync();
        }
        public void print_stock_adjustment(String _adjust_no, String _adjust_desc, String _stck_loc, String _caccnt, String _ccenter, String _t_date, String _t_time)
        {
            action = 4036;
            trans_no = _adjust_no;
            trans_desc = _adjust_desc;
            stckloc = _stck_loc;
            contraacc = _caccnt;
            cc = _ccenter;
            t_date = _t_date;
            t_time = _t_time;

            bgWorker.RunWorkerAsync();
        }

        
        public void print_sales_return(String _trans_no, String _cust_name, String _stck_loc, String _t_date, String _t_time)
        {
            action = 4041;
            trans_no = _trans_no;
            client = _cust_name;
            stckloc = _stck_loc;
            t_date = _t_date;
            t_time = _t_time;
          
            bgWorker.RunWorkerAsync();
        }
        public void print_sales(String _trans_no, String _cust_name, String _stck_loc, String _reference, String _mcardid, String _agent, String _mrk_sgmnt, String _outlet, String _t_date,String _blnce_due, String _payment)
        {
            action = 4042;
            trans_no = _trans_no;
            client = _cust_name;
            stckloc = _stck_loc;
            t_date = _t_date;
            outlet = _outlet;
            reference = _reference;
            mcardid = _mcardid;
            agent = _agent;
            mrk_sgmnt = _mrk_sgmnt;
            blnce_due = _blnce_due; 
            payment = _payment;
            

            bgWorker.RunWorkerAsync();
        }

        public void print_statementofaccount(String _soa_code, String _debt_code, String _debt_name, String _soa_date, String _due_date, String _t_date, String _t_time, String _comments)
        {
            action = 4051;
            trans_no = _soa_code;
            acct_no = _debt_code;
            client = _debt_name;
            notes = _comments;
            t_date = _t_date;
            t_time = _t_time;
            soa_date = _soa_date;
            due_date = _due_date;

            bgWorker.RunWorkerAsync();
        }
        public void print_statementofaccount_agency(String _trv_code, String _soa_code, String _t_dates)
        {
            action = 40522;
            thisDatabase dbsss = new thisDatabase();
            //Int32 datenoow = DateTime.DaysInMonth(DateTime.Now.Year, Int32.Parse(datedate));
            //String dateyes = "" + ((datedate == "1") ? (DateTime.Now.Year - 1) : DateTime.Now.Year) + "-" + ((datedate == "1") ? 12 : (Int32.Parse(datedate) - 1)) + "-" + DateTime.DaysInMonth(((datedate == "1") ? (DateTime.Now.Year - 1) : DateTime.Now.Year), ((datedate == "1") ? 12 : (Int32.Parse(datedate) - 1))) + "";
            //String dateone = "" + ((datedate == "1") ? (DateTime.Now.Year - 1) : DateTime.Now.Year) + "-" + ((datedate == "1") ? 12 : (Int32.Parse(datedate) - 1)) + "-01";
            //MessageBox.Show(DateTime.Parse(_t_dates).Month.ToString());
            t_date = DateTime.Now.ToString("yyyy-MM-dd");
            soa_code = _soa_code;
            trv_code = _trv_code;
            dt_trv = dbsss.QueryBySQLCode("SELECT trv_code, trv_name FROM rssys.travagnt WHERE trv_code = '" + _trv_code + "'");
            //trans_no = _soa_code;
            bgWorker.RunWorkerAsync();
        }
        public void print_commissionforagency(String _trv_code, String _soa_code, String _t_dates)
        {
            action = 40523;
            thisDatabase dbsss = new thisDatabase();

            t_date = DateTime.Now.ToString("yyyy-MM-dd");
            soa_code = _soa_code;
            trv_code = _trv_code;
            dt_trv = dbsss.QueryBySQLCode("SELECT trv_code, trv_name FROM rssys.travagnt WHERE trv_code = '" + _trv_code + "'");
            //trans_no = _soa_code;
            bgWorker.RunWorkerAsync();
        }
        public void print_monthlyreserv(String __month)
        {
            action = 40524;
            thisDatabase dbsss = new thisDatabase();
            Int32 datenoow = DateTime.DaysInMonth(DateTime.Now.Year, Int32.Parse(__month));
            __month__ = Convert.ToInt32(__month);
            t_date = DateTime.Now.Year + "-" + __month__.ToString("00") + "-" + datenoow;
            //trans_no = _soa_code;
            bgWorker.RunWorkerAsync();
        }

        public void print_collectionentry(String _form, String _coll_code, String _debt_name, String _coll_date, String _collector, String _reference, String _t_date, String _t_time)
        {
            action = 4052;
            trans_no = _coll_code;
            client = _debt_name;
            coll_date = _coll_date;
            collector = _collector;
            reference = _reference;
            t_date = _t_date;
            t_time = _t_time;
            form = _form;
           
            bgWorker.RunWorkerAsync();
        }
        public void print_disbursement(String _form, String _dis_no, String _debt_name, String _acc_link, String _t_date, String _notes, String _pay_thru, String _chk_no, String _chk_date)
        {
            action = 4053;
            trans_no = _dis_no;
            client = _debt_name;
            acc_link = _acc_link; 
            pay_thru = _pay_thru;
            chk_no = _chk_no;
            chk_date = _chk_date;
            t_date = _t_date;
            notes = _notes;
            form = _form;
            
            bgWorker.RunWorkerAsync();
        }
        public void print_loan_app(String _app_no, String _unit, String _cashprice, String _dp_pct, String _terms, String _aor,String _sagent, String _brand)
        {
            action = 4061;
            trans_no = _app_no;
            unit = _unit;
            cashprice = _cashprice;
            dp_pct = _dp_pct;
            terms = _terms;
            aor = _aor;
            brand = _brand;
            sagent = _sagent;
            
            bgWorker.RunWorkerAsync();
        }
        public void print_as_invoice(String _ord_code, String _terms, String _vin_no, String _vehicle, String _plate_no, String _engine_no, String _year_model, String _color, String _brand, String _sagent, String _vat_sales, String _vat_exsales, String _zero_sales, String _vat_amnt, String _vat_less, String _net_vat, String _total_sales, String _amnt_due, String _vat_add, String _total_amnt_due, String _dt_trans)
        {
            
            action = 4062;
            trans_no = _ord_code;
            vin_no = _vin_no;
            unit = _vehicle;
            terms = _terms;
            brand = _brand;
            plate_no = _plate_no;
            engine_no = _engine_no;
            year_model = _year_model;
            color = _color;
            vat_sales = _vat_sales;
            vat_exsales = _vat_exsales;
            zero_sales = _zero_sales;
            vat_amnt = _vat_amnt;
            vat_less = _vat_less;
            net_vat = _net_vat;
            total_sales = _total_sales;
            amnt_due = _amnt_due;
            vat_add = _vat_add;
            sagent = _sagent;
            total_amnt_due = _total_amnt_due;
            t_date = _dt_trans;
            //engine_no year_model color
            // vat_amnt vat_less net_vat total_sales amnt_due vat_add
            
            bgWorker.RunWorkerAsync();
        }
        public void print_as_agreement(String _ord_code, String _terms, String _vin_no, String _vehicle, String _plate_no, String _engine_no, String _year_model, String _color, String _brand, String _sagent, String _vat_sales, String _vat_exsales, String _zero_sales, String _vat_amnt, String _vat_less, String _net_vat, String _total_sales, String _amnt_due, String _vat_add, String _total_amnt_due, String _dt_trans)
        { 
            action = 4063;
            trans_no = _ord_code;
            vin_no = _vin_no;
            unit = _vehicle;
            terms = _terms;
            brand = _brand;
            engine_no = _engine_no;
            plate_no = _plate_no;
            year_model = _year_model;
            color = _color;
            vat_sales = _vat_sales;
            vat_exsales = _vat_exsales;
            zero_sales = _zero_sales;
            vat_amnt = _vat_amnt;
            vat_less = _vat_less;
            net_vat = _net_vat;
            total_sales = _total_sales;
            amnt_due = _amnt_due;
            vat_add = _vat_add;
            sagent = _sagent;
            total_amnt_due = _total_amnt_due;
            t_date = _dt_trans;
            //engine_no year_model color
            // vat_amnt vat_less net_vat total_sales amnt_due vat_add
            
            bgWorker.RunWorkerAsync();
        }
        
        //
        public void print_assembled_item(String _code, String _item_desc, String _purch_unit, String _sales_unit, String _reg_price, String _unit_price, String _branch)
        {
            action = 4070;
            item_code = _code;
            item_desc = _item_desc;
            purch_unit = _purch_unit;
            sales_unit = _sales_unit;
            reg_price = _reg_price;
            unit_price = _unit_price;
            branch = _branch;

            bgWorker.RunWorkerAsync();
        }
        public void print_assembled_item_list()
        {
            action = 4071;

            bgWorker.RunWorkerAsync();
        }


        public void print_vehicle_delivery_note(String _code, String _sagent, String _debt_code, String _vin_no, String _vehicle, String _plate_no, String _engine_no, String _year_model, String _color, String _last_km_reading,String _vsi_no, String _dt_trans, String _guardby, String _pickupby, String _unitdelby, String _releasedby, String _t_unitdel, String _t_release, String _d_unitdel, String _d_release, String _d_receive, String _remarks)
        {
            action = 4080;

            trans_no = _code;
            client = _debt_code;
            agent = _sagent;
            vin_no = _vin_no;
            unit = _vehicle;
            plate_no = _plate_no;
            engine_no = _engine_no;
            vsi_no = _vsi_no;
            year_model = _year_model;
            color = _color;
            last_km_reading = _last_km_reading;
            t_date = _dt_trans;
            guardby = _guardby;
            pickupby = _pickupby;
            unitdelby = _unitdelby;
            releasedby = _releasedby;
            t_unitdel = _t_unitdel;
            t_release = _t_release;
            d_unitdel = _d_unitdel;
            d_release = _d_release;
            d_receive = _d_receive;
            remarks = _remarks;
            bgWorker.RunWorkerAsync();
        }
        public void print_vehicle_delivery_note_2(String cust_name)
        {
            action = 4081;
            client = cust_name;
            bgWorker.RunWorkerAsync();
        }
        public void print_gp_computation(String _code)
        {
            action = 4090;
            trans_no = _code;
            bgWorker.RunWorkerAsync();
        }

        //();


        //

        //start on 100
        public void print_mdata(int mid)
        {
            action = mid;

            bgWorker.RunWorkerAsync();
            
        }
        
        
        private void add_fieldparam(String col, String val)
        {
            crParameterDiscreteValue.Value = val;
            crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions[col];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            clr_param();
            inc_pbar(10);
        }

        private void clr_param()
        {
            try
            {
                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
            }
            catch (Exception) { }           
        }

        private int get_cbo_index(ComboBox cbo)
        {
            int i = -1;

            cbo.Invoke(new Action(() =>
            {
                try{ i = cbo.SelectedIndex; }
                catch{ }
            }));

            return i;
        }

        private String get_cbo_value(ComboBox cbo)
        {
            String value = "";

            cbo.Invoke(new Action(() =>
            {
                try { value = cbo.SelectedValue.ToString(); }
                catch {  }
                
            }));

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

        private Boolean ischkbox_checked(CheckBox chk)
        {
            Boolean ischk = false;

            chk.Invoke(new Action(() =>
            {
                try { ischk = chk.Checked; }
                catch {  }
            }));

            return ischk;
        }

        private void disp_reportviewer(ReportDocument myReportDocument)
        {
            try
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
            catch (Exception) { }
        }

        private void inc_pbar(int i)
        {
            try
            {
                if(pbar.Value + i <= 100)
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value += i;
                    }));
                }
                else
                {
                    reset_pbar();
                }
                
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
                    try { pbar.Value = 0; }
                    catch { }
                }));
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
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
            pnl_pbar.Invoke(new Action(() =>
            {
                pnl_pbar.Show();
            }));
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            GlobalClass gc = new GlobalClass();
            NumberToEnglish_orig amtinwords = new NumberToEnglish_orig();
            String _schema = db.get_schema();
            String WHERE = "";

            comp_name = db.get_m99comp_name();
            comp_addr = db.get_m99comp_addr();

            try
            {
                //voucher from journal
                if (action == 1)
                {
                    inc_pbar(5);

                    jtype_name = db.get_jtypename(j_code);

                    if (jtype_name == "Disbursement")//j_code == "CDJ" || j_code == "CV" || j_code == "CV1"
                    {
                        Double amt = db.get_paidtoamount(j_code, refno, true);

                        myReportDocument.Load(fileloc_acctg + "j_voucher_CDJ.rpt");
                        myReportDocument.Database.Tables[0].SetDataSource(dt);

                        add_fieldparam("amount_desc", amtinwords.changeCurrencyToWordsForCheck(amt.ToString()));
                        add_fieldparam("amt",  gm.toAccountingFormat(amt));
                        add_fieldparam("checkno", ckno);
                        add_fieldparam("checkdate", gm.toDateString(ckdt, "MM/dd/yyyy"));
                        add_fieldparam("reviewedby", db.get_checkby(j_code));
                        add_fieldparam("approvedby", db.get_approvby(j_code));
                        add_fieldparam("notedby", db.get_notedby(j_code));
                        add_fieldparam("particulars", particular);
                    }
                    else
                    {
                        myReportDocument.Load(fileloc_acctg + "j_voucher.rpt");
                        myReportDocument.Database.Tables[0].SetDataSource(dt);

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = mo + " " + yr;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["year_mo"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = ckno + " / " + gm.toDateString(ckdt, "MM/dd/yyyy");
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["ck_date_no"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = particular;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["description"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = refdesc;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["ref_desc"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();
                    }

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = vtitle;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["voucher_title"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = j_code + "  " + refno;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["ref_no"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();


                    inc_pbar(5);
                    crParameterDiscreteValue.Value = gm.toDateString(t_date, "MM/dd/yyyy");
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["t_date"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = paidto;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["paid_to"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();
                }
                //soa residential
                else if (action == 2)
                {
                    //Vistana
                    if(GlobalClass.projcompany == "2")
                    {/*
                        Hotel_System.thisDatabase db_hotel = new Hotel_System.thisDatabase();

                        String room_desc = "", e_meterno = "xxxxxxx", e_meterreading = "0.00", e_amount = "0.00", e_amount_balance = "0.00", w_meterno = "xxxxxxx", w_meterreading = "0.00", w_amount = "0.00", w_amount_balance = "0.00", monthyear_period = "", rm_amt = "0.00", rm_balance = "0.00", rm_period = "";

                        room_desc = db_hotel.get_romdescbyfolio(reg_num);

                        e_meterno = db_hotel.get_e_meterno(reg_num);
                        e_meterreading = db_hotel.get_e_meterreading(reg_num);
                        e_amount = db_hotel.get_e_amount(reg_num);
                        e_amount_balance = db_hotel.get_e_amount_balance(reg_num);

                        w_meterno = db_hotel.get_w_meterno(reg_num);
                        w_meterreading = db_hotel.get_w_meterreading(reg_num);
                        w_amount = db_hotel.get_w_amount(reg_num);
                        w_amount_balance = db_hotel.get_w_amount_balance(reg_num);

                        monthyear_period = "";

                        rm_amt = "0.00";
                        rm_balance = "0.00";
                        rm_period = "";

                        inc_pbar(5);

                        myReportDocument.Load(fileloc_acctg + "a_soa_billing.rpt");

                        inc_pbar(5);
                        dt = db.get_soalne_total_summary(code);

                        myReportDocument.Database.Tables[0].SetDataSource(dt);

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = GlobalClass.username;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = comp_addr;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["comp_addr"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = code;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["soa_no"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = reg_num;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["reg_num"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = client;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["soa_billname"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = room_desc;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["room_desc"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = gm.toDateString(duedate, "MMM dd, yyyy");
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["duedate"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = gm.toDateString(duedate, "MMM dd, yyyy");
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["duedate2"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = e_meterno;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["e_meterno"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = e_mult;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["e_chargemultiplier"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = e_meterreading;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["e_meterreading"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = e_amount;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["e_amount"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = e_amount_balance;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["e_amount_balance"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = w_meterno;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["w_meterno"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = w_mult;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["w_chargemultiplier"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = w_meterreading;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["w_meterreading"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = w_amount;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["w_amount"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = w_amount_balance;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["w_amount_balance"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = monthyear_period;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["monthyear_period"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = rm_amt;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["rm_amt"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = rm_balance;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["rm_balance"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = rm_period;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["rm_period"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param(); */
                    }
                    else
                    {
                        inc_pbar(5);

                        myReportDocument.Load(fileloc_acctg + "a_soa_billing.rpt");

                        inc_pbar(5);
                        dt = db.get_soalne_folio_list_report(code);

                        myReportDocument.Database.Tables[0].SetDataSource(dt);

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = code;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["soa_no"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = reg_num;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["reg_num"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = client;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["soa_billname"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = gm.toDateString(t_date, "MM/dd/yyyy");
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["t_date"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = amtinwords.changeCurrencyToWordsForCheck(db.get_soa_amt(code).ToString("0.00"));
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["amtinwords"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = gm.toAccountingFormat(db.get_soa_amt(code));
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["amt"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();

                        inc_pbar(5);
                        crParameterDiscreteValue.Value = gm.toDateString(_chg_dtfrm, "MMM dd, yyyy") + " to " + gm.toDateString(_chg_dtto, "MMM dd, yyyy");
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["dt_range"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;
                        clr_param();
                    }
                    
                }
                //soa warehouse
                else if (action == 4)
                {
                    inc_pbar(5);

                    myReportDocument.Load(fileloc_acctg + "a_soa_warehouse.rpt");

                    inc_pbar(5);
                    dt = db.get_soalne_folio_list_report(code);

                    myReportDocument.Database.Tables[0].SetDataSource(dt);

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = code;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["soa_no"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = reg_num;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["reg_num"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = client;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["soa_billname"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = gm.toDateString(t_date, "MM/dd/yyyy");
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["t_date"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = amtinwords.changeCurrencyToWordsForCheck(db.get_soa_amt(code).ToString("0.00"));
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["amtinwords"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = gm.toAccountingFormat(db.get_soa_amt(code));
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["amt"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = gm.toDateString(_chg_dtfrm, "MMM dd, yyyy") + " to " + gm.toDateString(_chg_dtto, "MMM dd, yyyy");
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["dt_range"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();
                }
                else if (action == 3)
                {
                    inc_pbar(5);

                    Double amt = db.get_paidtoamount(j_code, refno, true);

                    myReportDocument.Load(fileloc_acctg + "j_check.rpt");

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = "***" + amtinwords.changeCurrencyToWordsForCheck(amt.ToString("0.00")) + "***";
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["amount_desc"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = "***" + gm.toAccountingFormat(amt) + "***";
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["amt"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = "***" + gm.toDateString(ckdt, "MMMM dd, yyyy") + "***";
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["t_date"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = "***" + paidto + "***";
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["paid_to"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();
                }

                else if (action == 20)
                {
                    //DataTable dt = db.QueryOnTableWithParams("prlne", "ln_num AS lnno, item_code, item_desc AS description, quantity AS qty, purc_unit AS unit, addl_desc AS purpose", "pr_code='" + code + "'", " ORDER BY " + db.castToInteger("ln_num") + " ASC");

                    DataTable dt = db.QueryBySQLCode("SELECT pl.ln_num AS lnno, pl.item_code, pl.item_desc AS description, pl.quantity AS qty, pl.purc_unit AS unit, p.reference AS purpose FROM " + db.get_schema() + ".prlne pl LEFT JOIN " + db.get_schema() + ".prhdr p ON p.pr_code=pl.pr_code WHERE pl.pr_code='" + code + "'  ORDER BY " + db.castToInteger("ln_num") + " ASC");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_inv + "i_purchase_request.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    //clerk
                    crParameterDiscreteValue.Value = code;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["prno"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();

                    //from date
                    crParameterDiscreteValue.Value = project;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["project"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();

                    //to date
                    crParameterDiscreteValue.Value = date_needed;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["date_needed"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();

                    //to date
                    crParameterDiscreteValue.Value = gm.toDateString(t_date, "MM/dd/yyyy");
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["t_date"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();

                    //to date
                    crParameterDiscreteValue.Value = requestedby;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["requestedby"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    clr_param();
                }

                else if (action == 21) 
                {
                    DataTable dt = db.QueryBySQLCode("SELECT pl.ln_num AS lnno, pl.item_code, pl.item_desc AS description, pl.delv_qty AS qty, u.unit_shortcode AS unit, pl.price AS unitprice, pl.ln_amnt AS amt FROM " + db.schema + ".purlne pl LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=pl.purc_unit WHERE pl.purc_ord='" + code + "' ORDER BY " + db.castToInteger("ln_num") + " ASC");
                    //DataTable dt = db.QueryOnTableWithParams("purlne", "ln_num AS lnno, item_code, item_desc AS description, rels_qty AS qty, purc_unit AS unit, price AS unitprice, ln_amnt AS amt", "purc_ord='" + code + "'", " ORDER BY " + db.castToInteger("ln_num") + " ASC");

                    inc_pbar(10);

                    myReportDocument.Load(fileloc_inv + "i_purchase_order.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    if (String.IsNullOrEmpty(pr_no))
                    {
                        pr_no = "N/A";
                    }

                    add_fieldparam("po_no", code);
                    add_fieldparam("prno", pr_no);
                    add_fieldparam("t_date", gm.toDateString(t_date, "MM/dd/yyyy"));
                    add_fieldparam("supplier", supplier);
                    add_fieldparam("terms", payment);
                    add_fieldparam("comp_number", comp_number); //comp_tel
                    add_fieldparam("del_date", gm.toDateString(delv_date, "MM/dd/yyyy"));
                    add_fieldparam("del_to", notes);
                    add_fieldparam("reference", reference);
                    add_fieldparam("preparedby", db.getFullName(preparedby));
                    add_fieldparam("finalizedby", db.getFullName(finalizedby));
                    add_fieldparam("approvedby", db.getFullName(approvedby));
                    add_fieldparam("request", requestedby);
                    add_fieldparam("userid", GlobalClass.username);
                }
                else if (action == 777)
                {
                    DataTable dt = db.QueryBySQLCode("SELECT t1.fy, t1.mo, t1.j_code, t1.j_num, to_char(t1.t_date, 'MM/dd/yyyy') AS t_date, t1.t_desc, t1.user_id, t2.seq_num, t2.at_code, m4.at_desc, t2.sl_code, t2.sl_name, t2.debit, t2.credit, t2.invoice, t2.seq_desc, t2.item_code, t2.item_desc, t2.unit, t2.recv_qty, t2.price, t2.scc_code FROM " + db.schema + ".tr02 t2 LEFT JOIN " + db.schema + ".tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) LEFT JOIN " + db.schema + ".m04 m4 ON m4.at_code=t2.at_code WHERE t1.j_code='" + j_code + "' AND t1.t_desc LIKE 'HOTEL TRANSACTIONS%' AND t1.t_date BETWEEN '" + _chg_dtfrm + "' AND '" + _chg_dtto + "'");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "jrnlz_transaction.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("journalize_title", "Journalize Hotel Transactions");
                    add_fieldparam("year", db.get_m99fy());
                    add_fieldparam("voucher_title", db.get_j_desc(j_code));
                }
                else if (action == 778)
                {

                    //_chg_dtto _chg_dtfrm j_title j_code

                    DataTable dt = db.QueryBySQLCode("SELECT t1.fy, t1.mo, t1.j_code, t1.j_num, to_char(t1.t_date, 'MM/dd/yyyy') AS t_date, t1.t_desc, t1.user_id, t2.seq_num, t2.at_code, m4.at_desc, t2.sl_code, t2.sl_name, t2.debit, t2.credit, t2.invoice, t2.seq_desc, t2.item_code, t2.item_desc, t2.unit, t2.recv_qty, t2.price, t2.scc_code FROM " + db.schema + ".tr02 t2 LEFT JOIN " + db.schema + ".tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) LEFT JOIN " + db.schema + ".m04 m4 ON m4.at_code=t2.at_code WHERE t1.j_code='" + j_code + "'   " + _WHERE + "         ORDER BY t1.j_code,t1.j_num,t2.seq_num");

                    //sysdate systime

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "jrnlz_transaction.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("journalize_title", j_title);
                    add_fieldparam("year", yr);
                    add_fieldparam("voucher_title", db.get_j_desc(j_code));
                }
                else if (action == 779)
                {

                    //_chg_dtto _chg_dtfrm j_title j_code

                    DataTable dt = db.QueryBySQLCode("SELECT t1.fy, t1.mo, t1.j_code, t1.j_num, to_char(t1.t_date, 'MM/dd/yyyy') AS t_date, t1.t_desc, t1.user_id, t2.seq_num, t2.at_code, m4.at_desc, t2.sl_code, t2.sl_name, t2.debit, t2.credit, t2.invoice, t2.seq_desc, t2.chg_code||' ('||t2.chg_num||')' AS item_code, t2.chg_desc AS item_desc, t2.unit, t2.recv_qty, t2.price, t2.scc_code FROM " + db.schema + ".tr02 t2 LEFT JOIN " + db.schema + ".tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) LEFT JOIN " + db.schema + ".m04 m4 ON m4.at_code=t2.at_code WHERE t1.j_code='" + j_code + "' " + _WHERE + "  ORDER BY t1.j_code,t1.j_num,t2.seq_num");

                    //sysdate systime

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "jrnlz_transaction_hotel.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("journalize_title", j_title);
                    add_fieldparam("year", yr);
                    add_fieldparam("voucher_title", db.get_j_desc(j_code));
                }

                else if (action == 1001)
                {
                    DataTable dt = db.QueryAllOnTable("m01");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_mainaccount.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                else if (action == 1002)
                {
                    DataTable dt = db.QueryAllOnTable("m02");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_subaccount.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                else if (action == 1003)
                {
                    DataTable dt = db.QueryAllOnTable("m03");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_accountgroup.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                else if (action == 1004)
                {
                    DataTable dt = db.QueryBySQLCode("SELECT DISTINCT m4.at_code, m4.at_desc, m4.bs_pl, m4.dr_cr, m4.sl, m4.cib_acct, m3.acc_code ||'-'||m3.acc_desc AS acc_code, m2.cmp_desc AS subacct FROM " + db.schema + ".m04 m4 LEFT JOIN " + db.schema + ".m03 m3 ON m4.acc_code=m3.acc_code LEFT JOIN " + db.schema + ".m02 m2 ON m2.cmp_code=m3.cmp_code ORDER BY acc_code, m4.at_code");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_accounttitle.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                else if (action == 1005)
                {
                    DataTable dt = db.QueryAllOnTable("m05");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_journal.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                else if (action == 2004)
                {
                    DataTable dt = db.QueryAllOnTable("rooms");
                    MessageBox.Show(dt.Rows.Count.ToString());

                    myReportDocument.Load(fileloc_md + "m_hotel_rooms.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                    //HOtel CHarge==
                else if (action == 2005)
                {
                    DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.charge WHERE chg_type = 'C' ORDER BY chg_code ASC");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_hotel_charge.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                else if (action == 4003)
                {
                    DataTable dt = db.QueryAllOnTable("items");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_items.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }

                //PRINT m_accountgroup
                else if (action == 4004)
                {
                    DataTable dt = db.get_accountgrouplist();
                    
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_accountgroup.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                // m_mainaccounts
                else if (action == 4005)
                {
                    DataTable dt = db.get_mainaccountlist();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_mainaccount.rpt");
                     myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();

                }
                // m_subaccounts
                else if (action == 4006)
                {
                    DataTable dt = db.get_subaccountlist();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_subaccount.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                // m_journal
                else if (action == 4007)
                {
                    DataTable dt = db.get_journallist();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_journal.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                // m_customer
                else if(action == 4008)
                {
                    DataTable dt = db.get_customer_list();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_customer.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                //m_supplier
                else if(action == 4009)
                {
                    DataTable dt = db.get_supplierlist();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_supplier.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                //m_cost_center
                 else if(action == 4010)
                {
                    DataTable dt = db.get_costcenterlist();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_cost_centers.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                //m_subcost_center
                else if (action == 4011)
                {
                    DataTable dt = db.get_subcostcenterlist();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_subcost_center.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                //m_car_dealers
                else if (action == 4012)
                {
                    DataTable dt = db.QueryAllOnTable("company");
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_car_dealers.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                } 
                //m_brand
                else if (action == 4013)
                {
                    DataTable dt = db.QueryAllOnTable("brand");
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_brand.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                //m_car_type
                else if (action == 4014)
                {
                    DataTable dt = db.get_cartypelist();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_cartype.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                //m_car_color
                else if (action == 4015)
                {
                    DataTable dt = db.get_colorlist();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_color.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                //m_vehicle_info
                else if (action == 4016)
                {
                    DataTable dt = db.QueryAllOnTable("vehicle_info");
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_vehicle_info.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                //m_sales_outlet
                else if (action == 4017)
                {
                    dbSales dbs = new dbSales();
                    DataTable dt = dbs.get_outlet_list();
                    
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_outlet.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    inc_pbar(10);
                    clr_param();
                }
                //m_service_member
                else if (action == 4018)
                {

                    DataTable dt = db.QueryAllOnTable("ecard");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_ecard.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();
                }
                    //m_discount
                else if (action == 4019)
                {

                    DataTable dt = db.QueryOnTableWithParams("disctbl", "*", "", " ORDER BY disc_code ASC");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_discount.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();
                }
                //m_clerk
                else if (action == 4020)
                {

                    DataTable dt = db.get_salesrep_list();

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_clerk.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();
                }
                //m_salesagent
                else if (action == 4021)
                {

                    DataTable dt = db.get_salesagent_list();

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_salesagent.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();
                }
                 //m_item_category
                else if (action == 4022)
                {

                    DataTable dt = db.get_itemgrp_list();

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_category.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();
                }
                 //m_item_unit
                else if (action == 4023)
                {

                    DataTable dt = db.get_itemunitlist();

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_item_unit.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();

                }
                 //m_stocklocation
                else if (action == 4024)
                {

                    DataTable dt = db.get_location_list();

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_stocklocation.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();

                }
                //m_stocklocation
                else if (action == 4025)
                {

                    DataTable dt = db.get_vat_list();

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_vatcodes.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);

                    clr_param();

                    //user_id
                    crParameterDiscreteValue.Value = GlobalClass.username;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["userid"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;

                    inc_pbar(10);
                    clr_param();

                }
                //*here
                else if (action == 4026) // purchase_requestion
                {

                    DataTable dt = db.QueryBySQLCode("SELECT o.*, sa.rep_name, st.st_desc, tec.tech_code, c.d_name, c.d_addr1, c.d_addr2 FROM " + db.schema + ".orhdr o LEFT JOIN " + db.schema + ".m06 c ON o.debt_code=c.d_code LEFT JOIN " + db.schema + ".repmst sa ON sa.rep_code=o.rep_code LEFT JOIN rssys.technician tec ON tec.tech_code=o.technician LEFT JOIN  " + db.schema + ".servicetype st ON st.st_code=o.servicetype WHERE o.ord_code='" + code + "' LIMIT 1");

                    DataTable dt2 = db.QueryBySQLCode("(SELECT ol1.ord_code, ol1.item_code, ol1.ln_num, ol1.item_desc, u.unit_desc, ol1.ord_qty, ol1.price, ol1.ln_amnt, ol1.ln_tax AS tax, ol1.net_amount AS netamnt, ol1.reference, ol1.part_no, ig.grp_desc AS itemgrp FROM " + db.schema + ".orlne ol1 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol1.unit LEFT JOIN rssys.items i ON i.item_code=ol1.item_code LEFT JOIN rssys.itmgrp ig ON ig.item_grp=i.item_grp  WHERE ol1.ord_code='" + code + "' AND ol1.ln_num>'0' AND u.unit_desc<>'SET'  AND ig.roclass_id='002' UNION ALL SELECT ol2.ord_code, ol2.item_code, ol2.ln_num, ol2.item_desc, u.unit_desc, ol2.qty, ol2.cost_pric, ol2.lnamnt, ol2.in_tax AS tax, ol2.net_amount AS netamnt, 'MENU SUB ITEM' AS reference, ol2.part_no, ig.grp_desc AS itemgrp  FROM " + db.schema + ".orlne2 ol2 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol2.unitID LEFT JOIN rssys.items i ON i.item_code=ol2.item_code LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=i.item_grp WHERE ol2.ord_code='" + code + "' AND ol2.ln_num>'0'  AND ig.roclass_id='002') ORDER BY ln_num ASC, reference desc"); //FORMAT(ol1.ord_qty,0)

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_srvc + "s_purchase_requestion_material.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("tech_no", dt.Rows[0]["tech_code"].ToString());

                    add_fieldparam("prtype", dt.Rows[0]["st_desc"].ToString());
                    add_fieldparam("prno", dt.Rows[0]["ord_code"].ToString());
                    add_fieldparam("ro_num", dt.Rows[0]["ord_code"].ToString());
                    add_fieldparam("t_date", gm.toDateString(dt.Rows[0]["t_date"].ToString(),""));
                    add_fieldparam("cust_name", dt.Rows[0]["d_name"].ToString());
                    add_fieldparam("cust_addr", dt.Rows[0]["d_addr1"].ToString());
                    add_fieldparam("dt_promised", gm.toDateString(dt.Rows[0]["promise_date"].ToString(), "") + " " + dt.Rows[0]["promise_time"].ToString());

                    add_fieldparam("disc_amnt", dt.Rows[0]["disc_amnt"].ToString());
                    add_fieldparam("tax_amnt", dt.Rows[0]["tax_amnt"].ToString());
                    add_fieldparam("ln_amnt", dt.Rows[0]["total_amnt"].ToString());
                    add_fieldparam("payment", gm.toAccountingFormat(dt.Rows[0]["payment"].ToString()));
                    add_fieldparam("net_amnt", (gm.toNormalDoubleFormat(dt.Rows[0]["total_amnt"].ToString()) - gm.toNormalDoubleFormat(dt.Rows[0]["tax_amnt"].ToString())).ToString());

                    add_fieldparam("due_amnt", gm.toAccountingFormat(dt.Rows[0]["amnt_due"].ToString()));

                    add_fieldparam("requestedby", dt.Rows[0]["rep_name"].ToString());
                    add_fieldparam("userid", GlobalClass.username);

                }
                else if (action == 4027) // ro invoice billing
                {

                    //car_engine, car_plate,car_vin_num(desc)


                    DataTable dt = db.QueryBySQLCode("SELECT o.*, vi.vin_desc ,vi.color , st.st_desc, rp.rep_name, c.d_name, c.d_addr1, c.d_addr2 FROM " + db.schema + ".orhdr o LEFT JOIN " + db.schema + ".m06 c ON o.debt_code=c.d_code LEFT JOIN " + db.schema + ".servicetype st ON st.st_code=o.servicetype LEFT JOIN " + db.schema + ".repmst rp ON rp.rep_code=o.rep_code LEFT JOIN " + db.schema + ".vehicle_info vi ON vi.vin_no=o.car_vin_num WHERE o.ord_code='" + code + "' LIMIT 1");

                    DataTable dt2 = db.QueryBySQLCode("(SELECT ol1.ord_code, ol1.item_code, ol1.ln_num, ol1.item_desc, u.unit_desc, ol1.ord_qty, ol1.price, ol1.ln_amnt, ol1.ln_tax AS tax, ol1.net_amount AS netamnt, ol1.reference, ol1.part_no, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp FROM " + db.schema + ".orlne ol1 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol1.unit LEFT JOIN " + db.schema + ".items i ON i.item_code=ol1.item_code LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=i.item_grp LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol1.ord_code='" + code + "' AND ol1.ln_num>'0' AND u.unit_desc<>'SET'  UNION ALL SELECT ol2.ord_code, ol2.item_code, ol2.ln_num, ol2.item_desc, u.unit_desc, ol2.qty, ol2.cost_pric, ol2.lnamnt, ol2.in_tax AS tax, ol2.net_amount AS netamnt, 'MENU SUB ITEM' AS reference, ol2.part_no, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp FROM " + db.schema + ".orlne2 ol2 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol2.unitID LEFT JOIN " + db.schema + ".items i ON i.item_code=ol2.item_code LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=i.item_grp LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol2.ord_code='" + code + "' AND ol2.ln_num>'0' ) ORDER BY ln_num ASC, item_grp ASC");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_srvc + "s_ro_billing_invoice.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("prno", dt.Rows[0]["ord_code"].ToString());

                    add_fieldparam("cust_refer", "");
                    add_fieldparam("ser_advisor", "");

                    add_fieldparam("cust_name", dt.Rows[0]["d_name"].ToString());
                    add_fieldparam("vin_desc", dt.Rows[0]["vin_desc"].ToString());
                    add_fieldparam("plate_no", dt.Rows[0]["car_plate"].ToString());
                    add_fieldparam("color", dt.Rows[0]["color"].ToString());
                    add_fieldparam("mileage", dt.Rows[0]["last_km_reading"].ToString());
                    add_fieldparam("t_date", gm.toDateString(dt.Rows[0]["t_date"].ToString(), "") + " " + dt.Rows[0]["t_time"].ToString());
                    add_fieldparam("engine_no", dt.Rows[0]["car_engine"].ToString());

                    add_fieldparam("jo_no", "");
                    add_fieldparam("term", ""); //??



                    add_fieldparam("approvedby", "");
                    add_fieldparam("preparedby", GlobalClass.username);

                    add_fieldparam("userid", GlobalClass.username);

                }

                else if (action == 4028) // ro invoice
                {
                    DataTable dt = db.QueryBySQLCode("SELECT o.*, vi.vin_desc ,vi.color , st.st_desc, rp.rep_name, c.d_name, c.d_addr1, c.d_addr2 FROM " + db.schema + ".orhdr o LEFT JOIN " + db.schema + ".m06 c ON o.debt_code=c.d_code LEFT JOIN " + db.schema + ".servicetype st ON st.st_code=o.servicetype LEFT JOIN " + db.schema + ".repmst rp ON rp.rep_code=o.rep_code LEFT JOIN " + db.schema + ".vehicle_info vi ON vi.vin_no=o.car_vin_num WHERE o.ord_code='" + code + "' LIMIT 1");

                    DataTable dt2 = db.QueryBySQLCode("(SELECT ol1.ord_code, ol1.item_code, ol1.ln_num, ol1.item_desc, u.unit_desc, ol1.ord_qty, ol1.price, ol1.ln_amnt, ol1.reference, ol1.part_no, ol1.ln_tax as tax, ol1.net_amount AS netamnt, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp  FROM " + db.schema + ".orlne ol1 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol1.unit LEFT JOIN " + db.schema + ".items it ON it.item_code=ol1.item_code  LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=it.item_grp  LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol1.ord_code='" + code + "' AND ol1.ln_num>'0' AND u.unit_desc<>'SET' UNION ALL  SELECT ol2.ord_code, ol2.item_code, ol2.ln_num, ol2.item_desc, u.unit_desc, ol2.qty, ol2.cost_pric, ol2.lnamnt, 'MENU SUB ITEM' AS reference, ol2.part_no, ol2.in_tax as tax, ol2.net_amount AS netamnt, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp FROM " + db.schema + ".orlne2 ol2 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol2.unitID LEFT JOIN " + db.schema + ".items it ON it.item_code=ol2.item_code  LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=it.item_grp  LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol2.ord_code='" + code + "' AND ol2.ln_num>'0' AND it.assembly!='N') ORDER BY item_grp, ln_num ASC");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_srvc + "s_ro_invoice.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("prno", dt.Rows[0]["ord_code"].ToString());

                    add_fieldparam("cust_refer", dt.Rows[0]["debt_code"].ToString());
                    add_fieldparam("ser_advisor", dt.Rows[0]["rep_name"].ToString());

                    add_fieldparam("cust_name", dt.Rows[0]["d_name"].ToString());
                    add_fieldparam("vin_desc", dt.Rows[0]["vin_desc"].ToString());
                    add_fieldparam("plate_no", dt.Rows[0]["car_plate"].ToString());
                    add_fieldparam("color", dt.Rows[0]["color"].ToString());
                    add_fieldparam("mileage", dt.Rows[0]["last_km_reading"].ToString());
                    add_fieldparam("t_date", gm.toDateString(dt.Rows[0]["t_date"].ToString(), "") + " " + dt.Rows[0]["t_time"].ToString());
                    add_fieldparam("engine_no", dt.Rows[0]["car_engine"].ToString());

                    add_fieldparam("jo_no", dt.Rows[0]["reference"].ToString());
                    add_fieldparam("term",""); //??


                    add_fieldparam("approvedby", dt.Rows[0]["approvedby_id"].ToString());
                    add_fieldparam("preparedby", GlobalClass.username);

                    add_fieldparam("userid", GlobalClass.username);

                }
                else if (action == 4029) // job order
                {
                    DataTable dt = db.QueryBySQLCode("SELECT o.*, vi.vin_desc ,vi.color , st.st_desc, rp.rep_name, c.d_name, COALESCE(c.d_addr1,c.d_addr2) AS d_addr, COALESCE(c.d_cntc_no , c.d_tel) AS d_cntc FROM " + db.schema + ".orhdr o LEFT JOIN " + db.schema + ".m06 c ON o.debt_code=c.d_code LEFT JOIN " + db.schema + ".servicetype st ON st.st_code=o.servicetype LEFT JOIN " + db.schema + ".repmst rp ON rp.rep_code=o.rep_code LEFT JOIN " + db.schema + ".vehicle_info vi ON vi.vin_no=o.car_vin_num WHERE o.ord_code='" + code + "' LIMIT 1");

                    DataTable dt2 = db.QueryBySQLCode("(SELECT ol1.ord_code, ol1.item_code, CAST(ol1.ln_num AS INTEGER) AS ln_num, ol1.item_desc, u.unit_desc, ol1.ord_qty, ol1.price, ol1.ln_amnt, ol1.reference, ol1.part_no, ol1.ln_tax as tax, ol1.net_amount AS netamnt, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp  FROM " + db.schema + ".orlne ol1 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol1.unit LEFT JOIN " + db.schema + ".items it ON it.item_code=ol1.item_code  LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=it.item_grp  LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol1.ord_code='" + code + "' AND ol1.ln_num>'0' AND u.unit_desc<>'SET' UNION ALL  SELECT ol2.ord_code, ol2.item_code, CAST(ol2.ln_num AS INTEGER) AS ln_num, ol2.item_desc, u.unit_desc, ol2.qty, ol2.cost_pric, ol2.lnamnt, 'MENU SUB ITEM' AS reference, ol2.part_no, ol2.in_tax as tax, ol2.net_amount AS netamnt, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp FROM " + db.schema + ".orlne2 ol2 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol2.unitID LEFT JOIN " + db.schema + ".items it ON it.item_code=ol2.item_code  LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=it.item_grp  LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol2.ord_code='" + code + "' AND ol2.ln_num>'0' AND it.assembly!='N') ORDER BY ln_num ASC");
                    //"SELECT *, 0.00 AS amount FROM " + db.schema + ".ord_perform WHERE ord_code='" + code + "' ORDER BY srv_line"
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_srvc + "s_job_order.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("inv_no", dt.Rows[0]["ord_code"].ToString());

                    //add_fieldparam("cust_refer", dt.Rows[0]["debt_code"].ToString());
                    //add_fieldparam("ser_advisor", dt.Rows[0]["rep_name"].ToString());

                    add_fieldparam("cust_name", dt.Rows[0]["d_name"].ToString());
                    add_fieldparam("cust_addr", dt.Rows[0]["d_addr"].ToString());
                    add_fieldparam("cust_contact", dt.Rows[0]["d_cntc"].ToString());

                    //add_fieldparam("vin_desc", dt.Rows[0]["vin_desc"].ToString());
                    add_fieldparam("plate_no", dt.Rows[0]["car_plate"].ToString());
                    add_fieldparam("color", dt.Rows[0]["color"].ToString());
                    add_fieldparam("mileage", dt.Rows[0]["last_km_reading"].ToString());
                    add_fieldparam("year_model", dt.Rows[0]["car_model"].ToString());
                    add_fieldparam("make", dt.Rows[0]["car_brand_id"].ToString());
                    add_fieldparam("model", dt.Rows[0]["vin_desc"].ToString());
                    add_fieldparam("vin_no", dt.Rows[0]["car_vin_num"].ToString());
                    add_fieldparam("body", "");
                    add_fieldparam("location", dt.Rows[0]["branch"].ToString()); //

                    add_fieldparam("start_dt", gm.toDateString(dt.Rows[0]["t_date"].ToString(), "") + " " + dt.Rows[0]["t_time"].ToString());//t_date
                    add_fieldparam("compile_dt", gm.toDateString(dt.Rows[0]["promise_date"].ToString(), "") + " " + dt.Rows[0]["promise_time"].ToString());

                    add_fieldparam("sales_personel", db.get_colval("salesagent", "name", "id='" + dt.Rows[0]["agentid"].ToString() + "'"));
                    add_fieldparam("serv_writer", db.get_colval("repmst", "rep_name", "rep_code='" + dt.Rows[0]["rep_code"].ToString() + "'"));
                    
                    //add_fieldparam("jo_no", dt.Rows[0]["reference"].ToString());
                    //add_fieldparam("term", ""); //??

                    //add_fieldparam("approvedby", dt.Rows[0]["approvedby_id"].ToString());
                    //add_fieldparam("preparedby", GlobalClass.username);

                    add_fieldparam("userid", GlobalClass.username);

                }
                else if (action == 4129) // ziebart Billing
                {
                    DataTable dt = db.QueryBySQLCode("SELECT o.*, vi.vin_desc ,vi.color , st.st_desc, rp.rep_name, c.d_name, COALESCE(c.d_addr1,c.d_addr2) AS d_addr, c.d_tin, COALESCE(c.d_cntc_no , c.d_tel) AS d_cntc, c.emp_busines_name, c.business_nature FROM " + db.schema + ".orhdr o LEFT JOIN " + db.schema + ".m06 c ON o.debt_code=c.d_code LEFT JOIN " + db.schema + ".servicetype st ON st.st_code=o.servicetype LEFT JOIN " + db.schema + ".repmst rp ON rp.rep_code=o.rep_code LEFT JOIN " + db.schema + ".vehicle_info vi ON vi.vin_no=o.car_vin_num WHERE o.ord_code='" + code + "' LIMIT 1"); 

                    DataTable dt2 = db.QueryBySQLCode("(SELECT ol1.ord_code, ol1.item_code, CAST(ol1.ln_num AS INTEGER) AS ln_num, ol1.item_desc, u.unit_desc, ol1.ord_qty, ol1.price, ol1.ln_amnt, ol1.reference, ol1.part_no, ol1.ln_tax as tax, ol1.net_amount AS netamnt, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp  FROM " + db.schema + ".orlne ol1 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol1.unit LEFT JOIN " + db.schema + ".items it ON it.item_code=ol1.item_code  LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=it.item_grp  LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol1.ord_code='" + code + "' AND ol1.ln_num>'0' AND u.unit_desc<>'SET' UNION ALL  SELECT ol2.ord_code, ol2.item_code, CAST(ol2.ln_num AS INTEGER) AS ln_num, ol2.item_desc, u.unit_desc, ol2.qty, ol2.cost_pric, ol2.lnamnt, 'MENU SUB ITEM' AS reference, ol2.part_no, ol2.in_tax as tax, ol2.net_amount AS netamnt, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp FROM " + db.schema + ".orlne2 ol2 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol2.unitID LEFT JOIN " + db.schema + ".items it ON it.item_code=ol2.item_code  LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=it.item_grp  LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol2.ord_code='" + code + "' AND ol2.ln_num>'0' AND it.assembly!='N') ORDER BY ln_num ASC");
                    
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_srvc + "s_ro_billing_invoice_ziebart.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("cust_details", dt.Rows[0]["d_name"].ToString() + "\n" + dt.Rows[0]["d_addr"].ToString() + "\n" + dt.Rows[0]["d_cntc"].ToString() + " / ");
                    add_fieldparam("vehic_details", dt.Rows[0]["vin_desc"].ToString() + "\n" + dt.Rows[0]["car_vin_num"].ToString() + " / " + dt.Rows[0]["car_plate"].ToString() + " / " + dt.Rows[0]["car_engine"].ToString() + "\n" + dt.Rows[0]["car_model"].ToString()+" / "+dt.Rows[0]["color"].ToString());

                    add_fieldparam("date_details", gm.toDateString(dt.Rows[0]["t_date"].ToString(), "") + " " + dt.Rows[0]["t_time"].ToString() + "\nPromise:" + gm.toDateString(dt.Rows[0]["promise_date"].ToString(), "") + " " + dt.Rows[0]["promise_time"].ToString());

                    add_fieldparam("tinno", dt.Rows[0]["d_tin"].ToString());
                    add_fieldparam("buss_nt", dt.Rows[0]["emp_busines_name"].ToString() + " / " + dt.Rows[0]["business_nature"].ToString());
                     
                    add_fieldparam("userid", GlobalClass.username);
                }
                else if (action == 4229) // ziebart warranty
                {
                    DataTable dt = db.QueryBySQLCode("SELECT o.*, vi.owner ||'-'|| vi.vin_desc AS vin_desc, vi.owner, vi.color, st.st_desc, rp.rep_name, c.d_name, c.d_addr1, c.d_addr2, c.area_code, CASE WHEN COALESCE(c.d_tel,'')<>'' THEN c.d_tel ELSE c.d_cntc_no END AS home_no, CASE WHEN COALESCE(c.work_landline,'')<>'' THEN c.d_tel ELSE c.work_mobile END AS work_no FROM " + db.schema + ".orhdr o LEFT JOIN " + db.schema + ".servicetype st ON st.st_code=o.servicetype LEFT JOIN " + db.schema + ".repmst rp ON rp.rep_code=o.rep_code LEFT JOIN " + db.schema + ".vehicle_info vi ON vi.vin_no=o.car_vin_num LEFT JOIN " + db.schema + ".m06 c ON vi.owner=c.d_code WHERE o.ord_code='" + code + "' LIMIT 1");
                    //
                    DataTable dt2 = db.QueryBySQLCode("(SELECT ol1.ord_code, ol1.item_code, CAST(ol1.ln_num AS INTEGER) AS ln_num, ol1.item_desc, u.unit_desc, ol1.ord_qty, ol1.price, ol1.ln_amnt, ol1.reference, ol1.part_no, ol1.ln_tax as tax, ol1.net_amount AS netamnt, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp  FROM " + db.schema + ".orlne ol1 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol1.unit LEFT JOIN " + db.schema + ".items it ON it.item_code=ol1.item_code  LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=it.item_grp  LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol1.ord_code='" + code + "' AND ol1.ln_num>'0' AND u.unit_desc<>'SET' UNION ALL  SELECT ol2.ord_code, ol2.item_code, CAST(ol2.ln_num AS INTEGER) AS ln_num, ol2.item_desc, u.unit_desc, ol2.qty, ol2.cost_pric, ol2.lnamnt, 'MENU SUB ITEM' AS reference, ol2.part_no, ol2.in_tax as tax, ol2.net_amount AS netamnt, COALESCE(roc.roclass_id ||'-'|| roc.roclass_desc,'TEXT ITEM') AS item_grp FROM " + db.schema + ".orlne2 ol2 LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=ol2.unitID LEFT JOIN " + db.schema + ".items it ON it.item_code=ol2.item_code  LEFT JOIN " + db.schema + ".itmgrp ig ON ig.item_grp=it.item_grp  LEFT JOIN " + db.schema + ".ro_classification roc ON roc.roclass_id=ig.roclass_id WHERE ol2.ord_code='" + code + "' AND ol2.ln_num>'0' AND it.assembly!='N') ORDER BY ln_num ASC");
                    
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_srvc + "s_ro_warranty_ziebert.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    DateTime dtt = DateTime.Parse(dt.Rows[0]["t_date"].ToString());

                    add_fieldparam("dt_mon_issue", dtt.ToString("MM"));
                    add_fieldparam("dt_day_issue", dtt.ToString("dd"));
                    add_fieldparam("dt_yr_issue", dtt.ToString("yyyy"));
                    add_fieldparam("place_issue", db.get_colval("branch", "name", "code='" + dt.Rows[0]["branch"].ToString() + "'"));
                    
                    add_fieldparam("vin_no", dt.Rows[0]["car_vin_num"].ToString());
                    add_fieldparam("inv_no", dt.Rows[0]["ord_code"].ToString());
                    add_fieldparam("plate_no", dt.Rows[0]["car_plate"].ToString());
                    add_fieldparam("mileage", dt.Rows[0]["last_km_reading"].ToString());
                    add_fieldparam("year_model", dt.Rows[0]["car_model"].ToString());
                    add_fieldparam("model", dt.Rows[0]["car_brand_id"].ToString() + " / " + dt.Rows[0]["vin_desc"].ToString());
                    add_fieldparam("color", dt.Rows[0]["color"].ToString());
                    add_fieldparam("body", "");

                    add_fieldparam("vin_owner", dt.Rows[0]["vin_desc"].ToString());
                    add_fieldparam("vo_addr", dt.Rows[0]["d_addr1"].ToString());
                    add_fieldparam("vo_addr2", dt.Rows[0]["d_addr2"].ToString());
                    add_fieldparam("vo_zip", dt.Rows[0]["area_code"].ToString());
                    add_fieldparam("home_no", dt.Rows[0]["home_no"].ToString());
                    add_fieldparam("work_no", dt.Rows[0]["work_no"].ToString());

                    add_fieldparam("userid", GlobalClass.username);

                }
                else if (action == 4031) // receiving po 
                {

                    DataTable dt = db.QueryBySQLCode("SELECT t.mp_desc as _payment_term, p.payment_term, p.\"locationFrom\", p.\"locationTo\", p.rec_num, p.supl_name,p.supl_code, p._reference, TO_CHAR(p.trnx_date, 'MM/dd/yyyy') AS trnx_date, p.whs_code, p.trn_type, p.jrnlz, p.cancel, p.recipient, TO_CHAR(p.t_date, 'MM/dd/yyyy') AS t_date, p.t_time, p.vat_code, w.whs_desc, p.purc_ord, p.printed FROM " + db.schema + ".rechdr p LEFT JOIN " + db.schema + ".whouse w ON w.whs_code=p.whs_code LEFT JOIN " + db.schema + ".m10 t ON t.mp_code=p.payment_term WHERE p.rec_num = '" + code + "' LIMIT 1");
                    
                    DataTable dt2 = db.QueryBySQLCode("SELECT rl.*, u.* FROM " + db.schema + ".rechdr r LEFT JOIN " + db.schema + ".reclne rl ON r.rec_num=rl.rec_num LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=rl.unit WHERE rl.rec_num='" + code + "' ORDER BY " + db.castToInteger("rl.ln_num") + ""); //FORMAT(ol1.ord_qty,0)

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_inv + "i_receiving_po.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("supplier_no", dt.Rows[0]["supl_code"].ToString());
                    add_fieldparam("supplier_name", dt.Rows[0]["supl_name"].ToString());
                    add_fieldparam("sup_address", dt.Rows[0]["locationTo"].ToString());
                    add_fieldparam("reference", dt.Rows[0]["_reference"].ToString());

                    add_fieldparam("userid", GlobalClass.username);

                }
                else if (action == 4032) // direct purchase 
                {

                    DataTable dt = db.QueryBySQLCode("SELECT ph.*, t.* FROM " + db.schema + ".pinvhd ph LEFT JOIN " + db.schema + ".m10 t ON t.mp_code=ph.pay_code WHERE ph.cancel='N' AND ph.inv_num='" + code + "' ORDER BY ph.inv_num ASC");
                    //
                    DataTable dt2 = db.QueryBySQLCode("SELECT pl.*, u.* FROM " + db.schema + ".pinvln pl LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=pl.unit WHERE pl.inv_num='" + code + "' ORDER BY " + db.castToInteger("ln_num") + "");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_inv + "i_direct_purchase.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("supplier", dt.Rows[0]["supl_name"].ToString());
                    add_fieldparam("terms", dt.Rows[0]["mp_desc"].ToString());
                    add_fieldparam("inv_no", dt.Rows[0]["inv_num"].ToString());
                    add_fieldparam("dt_trans", dt.Rows[0]["t_date"].ToString() + " " + dt.Rows[0]["t_time"].ToString());
                    add_fieldparam("reference", dt.Rows[0]["reference"].ToString());

                    add_fieldparam("userid", GlobalClass.username);


                }
                else if (action == 4033) //  purchase return 
                {

                    DataTable dt = db.QueryBySQLCode("SELECT p.pret_num, p.supl_code, p.supl_name, p.reference, TO_CHAR(p.trnx_date, 'MM/dd/yyyy') AS trnx_date, p.whs_code, p.jrnlz, p.cancel, p.user_id, TO_CHAR(p.t_date, 'MM/dd/yyyy') AS t_date, p.t_time FROM " + db.schema + ".prethdr p LEFT JOIN " + db.schema + ".m07 s ON s.c_code=p.supl_code LEFT JOIN " + db.schema + ".whouse w ON w.whs_code=p.whs_code WHERE p.pret_num='" + code + "' ORDER BY p.trnx_date,pret_num");
                    //
                    DataTable dt2 = db.QueryBySQLCode("SELECT pn.*, u.* FROM " + db.schema + ".pretlne pn LEFT JOIN " + db.schema + ".itmunit u ON u.unit_id=pn.unit WHERE pn.pret_num='" + code + "' ORDER BY " + db.castToInteger("ln_num") + "");


                    inc_pbar(10);
                    myReportDocument.Load(fileloc_inv + "i_purchase_return.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("supplier", dt.Rows[0]["supl_name"].ToString());
                    //add_fieldparam("terms", dt.Rows[0]["pret_num"].ToString());
                    add_fieldparam("inv_no", dt.Rows[0]["pret_num"].ToString());
                    add_fieldparam("dt_trans", dt.Rows[0]["t_date"].ToString() + " " + dt.Rows[0]["t_time"].ToString());
                    add_fieldparam("reference", dt.Rows[0]["reference"].ToString());

                    add_fieldparam("userid", GlobalClass.username);


                }
                else if (action == 4034) //  stock issuance
                {

                    DataTable dt = db.QueryBySQLCode("SELECT rd.*, w.whs_desc FROM " + db.schema + ".rechdr rd LEFT JOIN " + db.schema + ".m07 s ON s.c_code=rd.supl_code LEFT JOIN " + db.schema + ".whouse w ON w.whs_code=rd.whs_code WHERE rd.trn_type = 'I' AND rd.rec_num='" + code + "' ORDER BY rd.rec_num ASC");
                    
                    DataTable dt2 = db.QueryBySQLCode("SELECT r.*, i.unit_desc FROM " + db.schema + ".reclne r LEFT JOIN " + db.schema + ".itmunit i ON r.unit=i.unit_id WHERE rec_num='" + code + "' ORDER BY " + db.castToInteger("ln_num") + "");
                    
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_inv + "i_stockissuance.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt2);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("iss_no", dt.Rows[0]["rec_num"].ToString());
                    add_fieldparam("iss_desc", dt.Rows[0]["_reference"].ToString());
                    add_fieldparam("whs_desc", dt.Rows[0]["whs_desc"].ToString());
                    add_fieldparam("dt_trans", dt.Rows[0]["t_date"].ToString() + " " + dt.Rows[0]["t_time"].ToString());

                    add_fieldparam("cacct", contraacc);
                    add_fieldparam("cc", cc);
                    add_fieldparam("scc", scc);
                    

                    add_fieldparam("userid", GlobalClass.username);

                }
                else if (action == 4035) //  stock issuance
                {
                    DataTable dt = db.QueryBySQLCode("SELECT r.*, i.unit_desc FROM " + db.schema + ".reclne r LEFT JOIN " + db.schema + ".itmunit i ON r.unit=i.unit_id WHERE rec_num='" + trans_no + "' ORDER BY " + db.castToInteger("ln_num") + "");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_inv + "i_stocktransfer.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("trans_no", trans_no);
                    add_fieldparam("trans_desc", trans_desc);
                    add_fieldparam("dt_trans", t_date + " " + t_time);

                    add_fieldparam("loc_from", locfrom);
                    add_fieldparam("loc_to", locto);
                    
                    add_fieldparam("userid", GlobalClass.username);

                }
                else if (action == 4036) //  stock issuance  
                {
                    DataTable dt = db.QueryBySQLCode("SELECT r.*, i.unit_desc FROM " + db.schema + ".reclne r LEFT JOIN " + db.schema + ".itmunit i ON r.unit=i.unit_id WHERE rec_num='" + trans_no + "' ORDER BY " + db.castToInteger("ln_num") + "");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_inv + "i_stockadjustment.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("adjust_no", trans_no);
                    add_fieldparam("adjust_desc", trans_desc);
                    add_fieldparam("stck_loc", stckloc);
                    add_fieldparam("dt_trans", t_date + " " + t_time);

                    add_fieldparam("caccnt", contraacc);
                    add_fieldparam("ccenter", cc);

                    add_fieldparam("userid", GlobalClass.username);
                    
                }

                else if (action == 4041) //  sales return  
                {
                    DataTable dt = db.QueryBySQLCode("SELECT r.*, u.* FROM " + db.schema + ".retlne r LEFT JOIN " + db.schema + ".itmunit u ON r.unit=u.unit_id WHERE r.ret_num='" + trans_no + "' ORDER BY r.ln_num ASC");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_sales + "i_salesreturn.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("trans_no", trans_no);
                    add_fieldparam("cust_name", client);
                    add_fieldparam("stck_loc", stckloc);
                    add_fieldparam("dt_trans", t_date + " " + t_time);

                    add_fieldparam("userid", GlobalClass.username);

                }
                else if (action == 4042) //  sales 
                {
                    DataTable dt = db.QueryBySQLCode("SELECT ol.*, u.* AS unit FROM " + db.schema + ".orlne ol LEFT JOIN " + db.schema + ".itmunit u ON ol.unit=u.unit_id  WHERE ol.ord_code='" + trans_no + "' ORDER BY " + db.castToInteger("ol.ln_num") + " ASC");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_sales + "i_sales.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("trans_no", trans_no);
                    add_fieldparam("cust_name", client);
                    add_fieldparam("stck_loc", stckloc);
                    add_fieldparam("srvxcard", mcardid);
                    add_fieldparam("outlet", stckloc);
                    add_fieldparam("mrkseg", mrk_sgmnt);
                    add_fieldparam("salesagnt", agent);
                    add_fieldparam("reference", reference);
                    add_fieldparam("dt_trans", t_date);
                    add_fieldparam("payment", payment);
                    add_fieldparam("blnce_due", blnce_due);

                    add_fieldparam("userid", GlobalClass.username);

                }
                else if (action == 4051) //  statement of account
                {
                    DataTable dt = db.QueryBySQLCode("SELECT TO_CHAR(chg_date::date,'MM/DD/YYYY') AS chg_date, s.*, c.chg_desc FROM " + db.schema + ".soalne s LEFT JOIN " + db.schema + ".charge c ON c.chg_code=s.chg_code WHERE soa_code='" + trans_no + "' ORDER BY ln_num"), dt2 = new DataTable();

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "a_statement_account.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    String reg_num = "", rom_code = "", romtype = "", noofnights = "", arr_date = "", arr_time = "", dep_date = "", dep_time = "", amount_word = "", prevbal = "", balance = "", at_code = "", _t_date = "";
                    Double total_amount = 0, _noofnights = 0, _prevbal = 0;

                    String[] soa_dt = soa_date.Split('-');
                    String period = soa_dt.GetValue(0).ToString().Trim();
                    if (soa_dt.Length != 1)
                    {
                        period = (soa_dt.GetValue(1) ?? "").ToString().Trim();

                        try
                        {
                            String dt_chg = db.get_colval("soalne", "chg_date", "ln_num='1' AND soa_code='" + trans_no + "'");
                            if (DateTime.Parse(dt_chg).ToString("yyyy-MM") != DateTime.Parse(period).ToString("yyyy-MM"))
                            {
                                period = DateTime.Parse(dt_chg).ToString("MMMM dd,yyyy");
                            }
                        }
                        catch { }
                    }

                    if (dt.Rows.Count != 0)
                    {
                        reg_num = dt.Rows[0]["gfolio"].ToString();

                        dt2 = db.QueryBySQLCode("(SELECT rom_code, TO_CHAR(arr_date,'MM/DD/YYYY') AS arr_date, arr_time, TO_CHAR(dep_date,'MM/DD/YYYY') AS dep_date, dep_time, typ_code FROM rssys.gfolio gf WHERE reg_num='" + reg_num + "' UNION ALL SELECT rom_code, TO_CHAR(arr_date,'MM/DD/YYYY') AS arr_date, arr_time, TO_CHAR(dep_date,'MM/DD/YYYY') AS dep_date, dep_time, typ_code FROM rssys.gfhist gf WHERE reg_num='" + reg_num + "') LIMIT 1");

                        if (dt2.Rows.Count != 0)
                        {
                            rom_code = dt2.Rows[0]["rom_code"].ToString();
                            arr_date = Convert.ToDateTime(dt2.Rows[0]["arr_date"].ToString()).ToString("MM/dd/yyyy");
                            arr_time = dt2.Rows[0]["arr_time"].ToString();
                            dep_date = Convert.ToDateTime(dt2.Rows[0]["dep_date"].ToString()).ToString("MM/dd/yyyy");
                            dep_time = dt2.Rows[0]["dep_time"].ToString();

                            romtype = db.get_colval("rtype", "typ_desc", "typ_code='" + dt2.Rows[0]["typ_code"].ToString() + "'");

                            _noofnights = (Convert.ToDateTime(dep_date) - Convert.ToDateTime(arr_date)).TotalDays;
                            noofnights = (_noofnights == 0) ? "1" : _noofnights.ToString("0");
                        }

                        at_code = db.get_colval("m06", "at_code", "d_code='" + acct_no + "'");

                        //dt2 = db.QueryBySQLCode("SELECT DISTINCT t_date FROM rssys.tr01 t1 JOIN //rssys.tr02 t2 ON (t2.j_num=t1.j_num AND t2.j_code=t1.j_code AND //t2.invoice='" + trans_no + "') LIMIT 1");
                        //if (dt2.Rows.Count == 1)
                        //{
                        //    _t_date = dt2.Rows[0]["t_date"].ToString();
                        //}
                        if (String.IsNullOrEmpty(_t_date))
                        {
                            _t_date = gm.toDateString(period, "");
                        }

                        dt2 = db.QueryBySQLCode("SELECT _cur30days + _3160days + _6190days + _91120days + _over120days AS total_balance FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM ( SELECT res.*, (('" + _t_date + "')::date - t_date::date) AS mo FROM (SELECT invoice, sl_code, sl_name, at_code, max(TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')) AS t_date, max(CASE WHEN TO_CHAR(COALESCE(it_date,t_date),'YYYY-MM-DD')<>'' THEN t_desc ELSE '' END) AS t_desc, SUM(credit) AS credit, SUM(debit) AS debit, SUM(COALESCE(debit,0.00)) + SUM(COALESCE(credit,0.00) * -1)  AS amnt FROM (SELECT inv.t_date AS it_date, t2.*  FROM (SELECT * FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1  AND d_code='" + acct_no + "')  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')   ORDER BY sl_code, invoice) t2 JOIN rssys.tr01 t1 ON t1.j_code=t2.j_code AND t1.j_num=t2.j_num WHERE 1=1    AND t2.sl_code='" + acct_no + "'   AND t1.t_date<='" + _t_date + "'    AND t2.at_code = '" + at_code + "' AND t1.branch='" + GlobalClass.branch + "'  AND  t2.invoice<>'" + trans_no + "'                ) t2 LEFT JOIN (SELECT max(t1.t_date) AS t_date, max(t1.systime) AS systime, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) WHERE COALESCE(invoice,'')<>'' AND t_date<='" + _t_date + "'  GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND  TO_CHAR(t2.t_date,'YYYY-MM-DD')=TO_CHAR(inv.t_date,'YYYY-MM-DD') AND t2.systime=inv.systime)) res GROUP BY invoice, sl_code, sl_name, at_code) res WHERE COALESCE(amnt,0)<>0 ORDER BY sl_code, t_date::date) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE (COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0) AND ((COALESCE(_cur30days,0) + COALESCE(_3160days,0) + COALESCE(_6190days,0) + COALESCE(_91120days,0) + COALESCE(_over120days,0)))<>0 AND (lower(sl_name) not like '%beg%' OR lower(sl_name) not like '%bal%') ORDER BY sl_code");

                        //dt2 = db.QueryBySQLCode("SELECT _cur30days + _3160days + _6190days + _91120days + _over120days AS total_balance FROM (SELECT sl_code, sl_name, SUM(CASE WHEN mo<=30 THEN amnt ELSE 0.00 END) as _cur30days, SUM(CASE WHEN 31<=mo AND mo<=60 THEN amnt ELSE 0.00 END) as _3160days, SUM(CASE WHEN 61<=mo AND mo<=90 THEN amnt ELSE 0.00 END) as _6190days, SUM(CASE WHEN 91<=mo AND mo<=120 THEN amnt ELSE 0.00 END) as _91120days, SUM(CASE WHEN 120<mo THEN amnt ELSE 0.00 END) as _over120days FROM (SELECT t1.t_desc, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN to_char(inv.t_date,'MM/dd/yyyy')  ELSE to_char(t1.t_date,'MM/dd/yyyy')  END AS t_date, CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END AS t_date2, (('" + _t_date + "')::date - (CASE WHEN COALESCE(inv.invoice,'')<>'' THEN inv.t_date  ELSE t1.t_date END)) AS mo, COALESCE(t2.debit,0.00) + (COALESCE(t2.credit,0.00) * -1) + COALESCE(crt2.amnt,0.00) AS amnt, t2.sl_name, t2.sl_code, t2.invoice FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1 AND d_code='" + acct_no + "')  om6 ON (om6.link=COALESCE(m6.d_oldcode,m6.d_code)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _t_date + "'::date) LEFT JOIN (SELECT t2.invoice, t2.sl_code, SUM(COALESCE(t2.debit,0.00) + ( COALESCE(t2.credit,0.00) * -1)) AS amnt, CASE WHEN COALESCE(drt2.invoice,'')<>'' THEN 'Y' ELSE 'N' END AS is_invoice FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1 AND d_code='" + acct_no + "')  om6 ON (om6.link=COALESCE(m6.d_oldcode,m6.d_code)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code AND t_date<='" + _t_date + "'::date)  LEFT JOIN rssys.tr02 drt2 ON (drt2.invoice=t2.invoice AND drt2.sl_code=t2.sl_code AND drt2.debit<>0)   WHERE t2.credit<>0 AND COALESCE(t2.invoice,'')<>''     AND t2.at_code = '" + at_code + "' AND t1.branch='" + GlobalClass.branch + "'  AND  t2.invoice<>'" + trans_no + "'            GROUP BY t2.invoice, t2.sl_code, drt2.invoice ORDER BY t2.sl_code, t2.invoice) crt2 ON (crt2.invoice=t2.invoice AND crt2.sl_code=t2.sl_code AND crt2.is_invoice='Y')    LEFT JOIN (SELECT max(t1.t_date) AS t_date, invoice, sl_code FROM rssys.tr02 t2 JOIN rssys.tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) GROUP BY invoice, sl_code) inv ON (t2.invoice=inv.invoice AND inv.sl_code=t2.sl_code AND inv.t_date<='" + _t_date + "'::date)     WHERE (t2.debit<>0 OR COALESCE(crt2.amnt,0)=0)     AND t2.at_code = '" + at_code + "' AND t1.branch='" + GlobalClass.branch + "'   AND  t2.invoice<>'" + trans_no + "'         ) res GROUP BY sl_code, sl_name  ORDER BY sl_code) res WHERE (COALESCE(_cur30days,0)<>0 OR COALESCE(_3160days,0)<>0 OR COALESCE(_6190days,0)<>0 OR COALESCE(_91120days,0)<>0 OR COALESCE(_over120days,0)<>0) AND ((COALESCE(_cur30days,0) + COALESCE(_3160days,0) + COALESCE(_6190days,0) + COALESCE(_91120days,0) + COALESCE(_over120days,0)))<>0 AND (lower(sl_name) not like '%beg%' AND lower(sl_name) not like '%bal%')");

                        try { _prevbal += gm.toNormalDoubleFormat(dt2.Rows[0]["total_balance"].ToString()); }
                        catch { }

                        total_amount = _prevbal;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            total_amount += gm.toNormalDoubleFormat(dt.Rows[i]["amount"].ToString());
                        }
                    }

                    prevbal = _prevbal.ToString("0.00");
                    balance = total_amount.ToString("0.00");
                    amount_word = nte.changeNumericToWords(total_amount).ToUpper() + " ONLY (Php " + gm.toAccountingFormat(total_amount) + ")";

                    add_fieldparam("soa_no", trans_no);
                    add_fieldparam("client", client);
                    add_fieldparam("periods", period);
                    add_fieldparam("soa_date", soa_date);
                    add_fieldparam("due_date", due_date);
                    //add_fieldparam("dt_trans", t_date + " " + t_time);
                    add_fieldparam("notes", notes);

                    add_fieldparam("amount_word", amount_word);
                    add_fieldparam("noofnights", noofnights);
                    add_fieldparam("rom_code", rom_code);
                    add_fieldparam("romtype", romtype);
                    add_fieldparam("arr_date", arr_date);
                    add_fieldparam("arr_time", arr_time);
                    add_fieldparam("dep_date", dep_date);
                    add_fieldparam("dep_time", dep_time);
                    add_fieldparam("reg_num", reg_num);
                    add_fieldparam("prevbal", prevbal);
                    add_fieldparam("total_amnt", balance);

                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("comp_number", comp_number);
                }
                else if (action == 40522) //  statement of account
                {
                    //SELECT arr_date AS chg_date, acct_no AS gfolio, full_name AS tenant, hl.name AS roomno, CONCAT_WS(', ', pck.package1, pck1.activities1) AS chg_code, CONCAT_WS(', ', pck.package, pck1.activities) AS chg_desc, COALESCE(SPLIT_PART(rf.occ_type, ', ', 4), '0') AS ttlpax, prc.ttl AS amount FROM rssys.gfolio rf LEFT JOIN (SELECT name, code FROM rssys.hotel) hl ON hl.code = rf.hotel_code LEFT JOIN (SELECT cf.reg_num, cf.res_code AS rg_code, STRING_AGG(ch.chg_desc, ', ') AS package, STRING_AGG(ch.chg_code, ', ') AS package1 FROM rssys.chgfil cf LEFT JOIN rssys.charge ch ON cf.chg_code = ch.chg_code WHERE UPPER(cf.chg_code) LIKE 'PCK%' GROUP BY cf.reg_num, cf.res_code) pck ON pck.rg_code = rf.res_code LEFT JOIN (SELECT cf.reg_num, cf.res_code AS rg_code, STRING_AGG(ch.chg_desc, ', ') AS activities, STRING_AGG(ch.chg_code, ', ') AS activities1 FROM rssys.chgfil cf LEFT JOIN rssys.charge ch ON cf.chg_code = ch.chg_code WHERE UPPER(cf.chg_code) LIKE 'ACT%' GROUP BY cf.reg_num, cf.res_code) pck1 ON pck1.rg_code = rf.res_code  LEFT JOIN (SELECT SUM(COALESCE(amount, 0.00)) AS ttl, res_code AS rg_code FROM rssys.chgfil WHERE amount >= 0 AND (UPPER(chg_code) LIKE 'ACT%' OR UPPER(chg_code) LIKE 'PCK%') GROUP BY res_code) prc ON prc.rg_code = rf.res_code WHERE rf.trv_code = '" + dt_trv.Rows[0]["trv_code"].ToString() + "' AND (rf.t_date BETWEEN '" + dt_trv.Rows[0]["start"].ToString() + "' AND '" + dt_trv.Rows[0]["end"].ToString() + "')
                    DataTable dt = db.QueryBySQLCode("SELECT sl.chg_date, sl.gfolio, gf.full_name AS tenant, gf.h_name AS roomno, sl.chg_code, (SELECT STRING_AGG(ch.chg_desc, ', ') FROM rssys.charge ch, UNNEST(REGEXP_SPLIT_TO_ARRAY(sl.chg_code, ', ')) AS gh WHERE ch.chg_code IN (gh)) AS chg_desc, COALESCE(SPLIT_PART(gf.occ_type, ', ', 4), '0') AS pax, sl.amount FROM rssys.soalne sl LEFT JOIN (SELECT g.full_name, h.name AS h_name, g.occ_type, g.reg_num FROM rssys.gfolio g LEFT JOIN rssys.hotel h ON g.hotel_code = h.code) gf ON sl.gfolio = gf.reg_num WHERE sl.soa_code = '" + soa_code + "' ORDER BY sl.soa_code ASC"), dt2 = new DataTable();
                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "a_statementofaccount_angency.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();
                    String prevbal = "", balance = "", amount_word = "";
                    Double _prevbal = 0.00, total_amount = 0.00;

                    DataTable dt_prevbal = db.QueryBySQLCode("SELECT COALESCE(SUM(COAlESCE(prc.ttl, 0.00)), 0.00) AS price FROM rssys.gfolio rf LEFT JOIN (SELECT COALESCE(SUM(COALESCE(amount, 0.00)), 0.00) AS ttl, res_code AS rg_code, soa_code FROM rssys.chgfil WHERE amount >= 0 AND (UPPER(chg_code) LIKE 'ACT%' OR UPPER(chg_code) LIKE 'PCK%') GROUP BY res_code, soa_code) prc ON prc.rg_code = rf.res_code WHERE prc.soa_code::numeric(15,0) < '" + soa_code + "'::numeric(15,0) AND rf.trv_code = '" + trv_code + "' AND rf.t_date <= '" + t_date + "'");
                    dt2 = db.QueryBySQLCode("SELECT * FROM rssys.soahdr WHERE soa_code = '" + soa_code + "'");
                    try { _prevbal = Convert.ToDouble(dt_prevbal.Rows[0]["price"].ToString()); }
                    catch { }
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            try { total_amount = total_amount + Convert.ToDouble(dt.Rows[i]["amount"].ToString()); }
                            catch { }
                        }
                    }
                    prevbal = _prevbal.ToString("0.00");
                    balance = (total_amount - _prevbal).ToString("0.00");
                    amount_word = nte.changeNumericToWords(total_amount).ToUpper() + " ONLY (Php " + gm.toAccountingFormat(total_amount) + ")";

                    add_fieldparam("soa_no", soa_code);
                    add_fieldparam("client", dt_trv.Rows[0]["trv_name"].ToString());
                    add_fieldparam("soa_date", dt2.Rows[0]["soa_date"].ToString());
                    add_fieldparam("due_date", dt2.Rows[0]["due_date"].ToString());
                    //add_fieldparam("dt_trans", t_date + " " + t_time);
                    add_fieldparam("notes", dt2.Rows[0]["comments"].ToString());

                    //add_fieldparam("amount_word", amount_word);
                    add_fieldparam("prevbal", prevbal);
                    add_fieldparam("total_amnt", balance);

                    add_fieldparam("userid", db.QueryBySQLCodeRetStr("SELECT comp_president FROM rssys.m99 LIMIT 1"));
                    add_fieldparam("comp_number", comp_number);
                }
                else if (action == 40523) //  statement of account
                {
                    DataTable dt = db.QueryBySQLCode("SELECT sl.chg_date, gf.full_name AS tenant, gf.h_name AS roomno, COALESCE(SPLIT_PART(gf.occ_type, ', ', 4), '0') AS others, (SELECT STRING_AGG(ch.chg_desc, ', ') FROM rssys.charge ch, UNNEST(REGEXP_SPLIT_TO_ARRAY(sl.chg_code, ', ')) AS gh WHERE ch.chg_code IN (gh)) AS chg_desc, (SELECT SUM(((COALESCE(ch.com, 0.00) * 0.01) * COALESCE(ch.price, 0.00))::numeric(20,2)) FROM rssys.charge ch, UNNEST(REGEXP_SPLIT_TO_ARRAY(sl.chg_code, ', ')) AS gh WHERE ch.chg_code IN (gh)) AS chg_num, (SELECT (SUM(((COALESCE(ch.com, 0.00) * 0.01) * COALESCE(ch.price, 0.00))::numeric(20,2)) * COALESCE(SPLIT_PART(gf.occ_type, ', ', 4), '0')::numeric(20,2)) FROM rssys.charge ch, UNNEST(REGEXP_SPLIT_TO_ARRAY(sl.chg_code, ', ')) AS gh WHERE ch.chg_code IN (gh)) AS amount FROM rssys.soalne sl LEFT JOIN (SELECT g.full_name, h.name AS h_name, g.occ_type, g.reg_num FROM rssys.gfolio g LEFT JOIN rssys.hotel h ON g.hotel_code = h.code) gf ON sl.gfolio = gf.reg_num WHERE sl.soa_code = '" + soa_code + "' ORDER BY sl.soa_code ASC"), dt2 = new DataTable();

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "a_commissionforagency.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("client", dt_trv.Rows[0]["trv_name"].ToString());
                    add_fieldparam("due_date", t_date);
                    //add_fieldparam("dt_trans", t_date + " " + t_time);

                    //add_fieldparam("amount_word", amount_word);
                    add_fieldparam("userid", db.QueryBySQLCodeRetStr("SELECT comp_president FROM rssys.m99 LIMIT 1"));
                }
                else if (action == 40524) //  statement of account
                {
                    DataTable dt = db.QueryBySQLCode("SELECT tr.trv_name AS tenant, SUM(COALESCE(CASE WHEN (COALESCE(SPLIT_PART(gf.occ_type, ', ', 1), '0')::numeric(15,0) + COALESCE(SPLIT_PART(gf.occ_type, ', ', 2), '0')::numeric(15,0) + COALESCE(SPLIT_PART(gf.occ_type, ', ', 3), '0')::numeric(15,0)) > COALESCE(SPLIT_PART(gf.occ_type, ', ', 4), '0')::numeric(15,0) THEN (COALESCE(SPLIT_PART(gf.occ_type, ', ', 1), '0')::numeric(15,0) + COALESCE(SPLIT_PART(gf.occ_type, ', ', 2), '0')::numeric(15,0) + COALESCE(SPLIT_PART(gf.occ_type, ', ', 3), '0')::numeric(15,0)) ELSE COALESCE(SPLIT_PART(gf.occ_type, ', ', 4), '0')::numeric(15,0) END, '0')::numeric(15,0)) AS others, SUM(COALESCE(cf.amount, 0.00)) AS amount FROM rssys.travagnt tr LEFT JOIN (SELECT * FROM rssys.gfolio WHERE (t_date BETWEEN '" + DateTime.Now.Year + "-" + __month__.ToString("00") + "-01' AND '" + t_date + "')) gf ON tr.trv_code = gf.trv_code LEFT JOIN (SELECT cf.* FROM rssys.chgfil cf INNER JOIN rssys.charge ch ON ch.chg_code = cf.chg_code WHERE ch.chg_type = 'C') cf ON gf.reg_num = cf.reg_num GROUP BY tr.trv_name ORDER BY tr.trv_name ASC"), dt2 = new DataTable();

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "CrystalReport1.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                }
                /*else if (action == 4051) //  statement of account
                {
                    DataTable dt = db.QueryBySQLCode("SELECT s.*, c.chg_desc FROM " + db.schema + ".soalne s LEFT JOIN " + db.schema + ".charge c ON c.chg_code=s.chg_code WHERE soa_code='" + trans_no + "' ORDER BY ln_num"); 

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "a_statement_account.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param(); 

                    add_fieldparam("soa_no", trans_no);
                    add_fieldparam("client", client);
                    add_fieldparam("soa_date", soa_date);
                    add_fieldparam("due_date", due_date);
                    add_fieldparam("dt_trans", t_date + " " + t_time);
                    add_fieldparam("notes", notes);

                    add_fieldparam("userid", GlobalClass.username);
                }*/
                else if (action == 4052) //  collection entry
                {

                    DataTable dt = db.QueryBySQLCode("SELECT c.or_code, c.ln_num, c.type, c.chk_num, c.amount, c.deposited, c.soa_code, c.payment_desc, TO_CHAR(c.chk_date, 'MM/dd/yyyy') AS chk_date FROM " + db.schema + ".collne2 c WHERE or_code='" + trans_no + "' ORDER BY c.chk_date, or_code ASC, ln_num ASC");
                    
                    inc_pbar(10);
                    if (form == "CV")
                    {
                        myReportDocument.Load(fileloc_acctg + "a_collection_entry_cdj.rpt");
                    }
                    else if (form == "CHK")
                    {
                        myReportDocument.Load(fileloc_acctg + "a_collection_entry_chk.rpt");
                    }
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();


                    if (form == "CV")
                    {

                    }
                    else if (form == "CHK")
                    {
                        add_fieldparam("reference", reference);

                    }
                    add_fieldparam("coll_no", trans_no);
                    add_fieldparam("client", client);
                    add_fieldparam("collector", collector);
                    add_fieldparam("coll_date", coll_date);
                    add_fieldparam("dt_trans", t_date + " " + t_time);

                    add_fieldparam("userid", GlobalClass.username);
                }
                else if (action == 4053) //  disbursement
                {

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM " + db.schema + ".disbursementlne WHERE db_code='" + trans_no + "'");

                    inc_pbar(10);
                    if (form == "CV")
                    {
                        myReportDocument.Load(fileloc_acctg + "a_disbursement_cdj.rpt");
                    }
                    else if (form == "CHK")
                    {
                        myReportDocument.Load(fileloc_acctg + "a_disbursement_chk.rpt");
                    }
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();


                    if (form == "CV")
                    {
                        add_fieldparam("chk_no", chk_no);
                        add_fieldparam("chk_date", chk_date);
                    }
                    else if (form == "CHK")
                    {

                    }
                    add_fieldparam("dis_no", trans_no);
                    add_fieldparam("client", client);
                    add_fieldparam("acc_link", acc_link);
                    add_fieldparam("dt_trans", t_date);
                    add_fieldparam("pay_thru", pay_thru);
                    add_fieldparam("notes", notes);
                   
                    add_fieldparam("userid", GlobalClass.username);
                }
                else if (action == 4061) //  auto laon application
                {

                    DataTable dt = dt = db.QueryBySQLCode("SELECT au.*, c.* FROM " + db.schema + ".autoloandhr au LEFT JOIN " + db.schema + ".m06 c ON  c.d_code=au.cust_no WHERE au.app_no ='" + trans_no + "'"); 

                    inc_pbar(10);

                    if (brand.ToUpper().Equals("KIA"))
                    {
                        myReportDocument.Load(fileloc_sales + "s_auto_loan_kia.rpt");
                    }
                    else if (brand.ToUpper().Equals("MAHINDRA"))
                    {
                        myReportDocument.Load(fileloc_sales + "s_auto_loan_mahindra.rpt");
                    }

                    //myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("unit", unit);
                    add_fieldparam("cash_price", cashprice);
                    add_fieldparam("dpayment", dp_pct);
                    add_fieldparam("term", terms);
                    add_fieldparam("aor", aor);

                    add_fieldparam("prim_brwr", dt.Rows[0]["d_name"].ToString());
                    add_fieldparam("sales_consult", sagent);

                    add_fieldparam("pb_brwr_name", dt.Rows[0]["d_name"].ToString());
                    add_fieldparam("pb_home_addr", dt.Rows[0]["d_addr2"].ToString());
                    add_fieldparam("pb_res_status", dt.Rows[0]["civil_status"].ToString());
                    add_fieldparam("pb_email", dt.Rows[0]["d_email"].ToString());
                    add_fieldparam("pb_birthdate", gm.toDateString(dt.Rows[0]["bdate"].ToString(),""));
                    add_fieldparam("pb_landline", dt.Rows[0]["d_tel"].ToString());
                    add_fieldparam("pb_mobile", dt.Rows[0]["d_cntc_no"].ToString());
                    add_fieldparam("pb_civil", dt.Rows[0]["civil_status"].ToString());
                    add_fieldparam("pb_tin", dt.Rows[0]["d_tin"].ToString());
                    add_fieldparam("pb_sss", dt.Rows[0]["sss"].ToString());

                    add_fieldparam("pb_lenstay", dt.Rows[0]["length_stay_years"].ToString());
                    add_fieldparam("pb_eb_name", dt.Rows[0]["emp_busines_name"].ToString());
                    add_fieldparam("pb_eb_addr", dt.Rows[0]["work_business_work_address"].ToString());
                    add_fieldparam("pb_nob", dt.Rows[0]["business_nature"].ToString());
                    add_fieldparam("pb_eb_email", dt.Rows[0]["emp_busi_email"].ToString());
                    add_fieldparam("pb_gross", dt.Rows[0]["gross_m_income"].ToString());
                    add_fieldparam("pb_yearincom", dt.Rows[0]["years_w_company"].ToString());
                    add_fieldparam("pb_position", dt.Rows[0]["position_title"].ToString());
                    add_fieldparam("pb_eb_landline", dt.Rows[0]["work_landline"].ToString());
                    add_fieldparam("pb_eb_mobile", dt.Rows[0]["work_mobile"].ToString());

                    add_fieldparam("sc_name", dt.Rows[0]["co_maker_name"].ToString());
                    add_fieldparam("sc_home_addr", dt.Rows[0]["co_m_home_add"].ToString());
                    add_fieldparam("sc_email", dt.Rows[0]["co_email"].ToString());
                    add_fieldparam("sc_relation", dt.Rows[0]["co_m_relation"].ToString());
                    add_fieldparam("sc_birthdate", (String.IsNullOrEmpty(dt.Rows[0]["co_maker_name"].ToString()) ? "" : gm.toDateString(dt.Rows[0]["com_m_bdate"].ToString(), "")));
                    add_fieldparam("sc_landline", ""); // ??
                    add_fieldparam("sc_mobile", dt.Rows[0]["co_mobile"].ToString());
                    add_fieldparam("sc_civil", ""); // ??
                    add_fieldparam("sc_tin", dt.Rows[0]["co_tin"].ToString());
                    add_fieldparam("sc_sss", dt.Rows[0]["co_sss"].ToString());

                    add_fieldparam("sc_eb_name", dt.Rows[0]["co_emp_business_name"].ToString());
                    add_fieldparam("sc_eb_addr", dt.Rows[0]["co_business_add"].ToString());
                    add_fieldparam("sc_nob", dt.Rows[0]["co_nature_of_buss"].ToString());
                    add_fieldparam("sc_eb_email", dt.Rows[0]["co_business_emp_email"].ToString());
                    add_fieldparam("sc_gross", dt.Rows[0]["co_gross_m_income"].ToString());
                    add_fieldparam("sc_yearincom", dt.Rows[0]["co_years_n_comp"].ToString());
                    add_fieldparam("sc_position", dt.Rows[0]["co_pos_title"].ToString());
                    add_fieldparam("sc_eb_landline", dt.Rows[0]["co_landline"].ToString());
                    add_fieldparam("sc_eb_mobile", dt.Rows[0]["co_work_mobile"].ToString());

                    add_fieldparam("userid", GlobalClass.username);
                }
                else if (action == 4062) //  auto sales invoice
                {

                    DataTable dt = db.QueryBySQLCode("SELECT c.* FROM " + db.schema + ".orhdr au LEFT JOIN " + db.schema + ".m06 c ON  c.d_code=au.debt_code WHERE au.ord_code ='" + trans_no + "'");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_sales + "s_auto_sales_invoice.rpt");
                    //myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();


                    add_fieldparam("sold_to", dt.Rows[0]["d_name"].ToString());
                    add_fieldparam("tin_no", dt.Rows[0]["d_tin"].ToString());
                    add_fieldparam("si_addr", dt.Rows[0]["d_addr2"].ToString());
                    add_fieldparam("si_bstyle", ""); // ??
                    add_fieldparam("si_date", t_date);
                    add_fieldparam("si_terms", terms);
                    add_fieldparam("si_op_no", ""); // ??
                    add_fieldparam("si_cholder", ""); // ??
                    add_fieldparam("si_signature", ""); // ?? 
                    add_fieldparam("vat_sales", vat_sales); // ??
                    add_fieldparam("vat_amnt", vat_amnt); // ??
                    add_fieldparam("vat_less", vat_less); // ??
                    add_fieldparam("net_vat", net_vat); // ??
                    add_fieldparam("total_sales", total_sales); // ??
                    add_fieldparam("amnt_due", amnt_due); // ??
                    add_fieldparam("vat_add", vat_add);  // ??
                    add_fieldparam("total_amnt_due", total_amnt_due); // ??

                    add_fieldparam("motor_no", ""); // ??
                    add_fieldparam("sc_no", vin_no); // ??
                    add_fieldparam("product_no", ""); // ??

                    add_fieldparam("unit", unit);
                    add_fieldparam("color", color);
                    add_fieldparam("cs_no", plate_no); // ??
                    add_fieldparam("sales_consult", sagent);
                    add_fieldparam("year_model", year_model);
                    add_fieldparam("gvw", ""); // ?? gross vehivle weight
                    add_fieldparam("piston", ""); // ?? piston displacement
                    add_fieldparam("fuel", ""); // ?? fuel

                    add_fieldparam("userid", GlobalClass.username);
                }

                else if (action == 4063) //  auto sales agreement
                {

                    DataTable dt = db.QueryBySQLCode("SELECT c.* FROM " + db.schema + ".orhdr au LEFT JOIN " + db.schema + ".m06 c ON  c.d_code=au.debt_code WHERE au.ord_code ='" + trans_no + "'");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_sales + "s_auto_sales_agreement.rpt");
                    //myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("cust_name", dt.Rows[0]["d_name"].ToString());
                    add_fieldparam("cust_addr", dt.Rows[0]["d_addr2"].ToString()); 
                    add_fieldparam("cust_office", ""); // ??
                    add_fieldparam("birthdate", gm.toDateString(dt.Rows[0]["bdate"].ToString(),""));
                    add_fieldparam("tin_no", dt.Rows[0]["d_tin"].ToString()); 
                    add_fieldparam("rc_no", ""); // ??
                    add_fieldparam("dateplace_issue", ""); // ??
                    add_fieldparam("dt_date", t_date); // ??
                    add_fieldparam("tel1_no", ""); // ??
                    add_fieldparam("tel2_no", ""); // ??
                    add_fieldparam("mobile_no", dt.Rows[0]["d_cntc_no"].ToString());
                    add_fieldparam("fax_no", dt.Rows[0]["d_fax"].ToString()); 
                    add_fieldparam("email", dt.Rows[0]["d_email"].ToString()); 
                    add_fieldparam("se", ""); // ??
                    add_fieldparam("src_sale", ""); // ??
                    add_fieldparam("sps_name", dt.Rows[0]["co_maker_name"].ToString());
                    add_fieldparam("sps_tin_no", dt.Rows[0]["co_tin"].ToString()); 
                    add_fieldparam("sps_birthdate",  (String.IsNullOrEmpty(dt.Rows[0]["co_maker_name"].ToString()) ? "" : gm.toDateString(dt.Rows[0]["com_m_bdate"].ToString(), ""))); 
                    add_fieldparam("sps_rc_no", ""); // ??
                    add_fieldparam("sps_dateplace_issue", ""); // ??

                    add_fieldparam("unit_price", ""); // ??
                    add_fieldparam("dpayment", ""); // ??
                    add_fieldparam("insurance_prm", ""); // ??
                    add_fieldparam("lto_reg", ""); // ??
                    add_fieldparam("tpl", ""); // ??
                    add_fieldparam("less_deposit", ""); // ??
                    add_fieldparam("total_cash_outlay", ""); // ??
                    add_fieldparam("term_payment", ""); // ??
                    add_fieldparam("amnt_finance", ""); // ??
                    add_fieldparam("term_payment2", ""); // ??
                    add_fieldparam("gross_monthly", ""); // ??
                    add_fieldparam("promopt_payment", ""); // ??
                    add_fieldparam("net_monthly", ""); // ??
                    add_fieldparam("sale_executive", ""); // ??
                    add_fieldparam("accounting", ""); // ??
                    add_fieldparam("gsm", ""); // ??
                    add_fieldparam("sales_manager", ""); // ??
                    add_fieldparam("date_release", ""); // ??
                    add_fieldparam("place_release", ""); // ??
                    add_fieldparam("plate_ending", ""); // ??
                    add_fieldparam("other_arrng_agree", ""); // ??

                    add_fieldparam("model", unit); 
                    add_fieldparam("color", color); 
                    add_fieldparam("engine_no", engine_no);
                    add_fieldparam("chassis_no", vin_no); 
                    add_fieldparam("cs_no", plate_no); 

                    add_fieldparam("userid", GlobalClass.username);
                }



                else if (action == 4070) // assembled item
                {

                    DataTable dt = db.QueryBySQLCode("SELECT i2.*, i.item_desc, i.ave_cost, i2.qty*i.ave_cost AS ln_amt, u.unit_shortcode FROM rssys.items2 i2 LEFT JOIN rssys.items i ON i2.item_code2=i.item_code LEFT JOIN rssys.itmunit u ON i2.sales_unit_id=u.unit_id WHERE i2.item_code='" + item_code + "' ORDER BY item_desc ASC");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_assembled_items.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("item_code", item_code);
                    add_fieldparam("item_desc_a", item_desc);
                    add_fieldparam("purch_unit", purch_unit);
                    add_fieldparam("sales_unit", sales_unit);
                    add_fieldparam("reg_price", reg_price);
                    add_fieldparam("unit_price", unit_price);
                    add_fieldparam("branch", branch);

                    add_fieldparam("userid", GlobalClass.username);
                    
                }

                else if (action == 4071) // assembled item list
                {

                    DataTable dt = db.QueryBySQLCode("SELECT ii.item_desc as item_desc_ass, iu.unit_shortcode, i2.*, i.item_desc, i.ave_cost, i2.qty*i.ave_cost AS ln_amt, u.unit_shortcode FROM rssys.items2 i2 LEFT JOIN rssys.items i ON i2.item_code2=i.item_code LEFT JOIN rssys.itmunit u ON i2.sales_unit_id=u.unit_id LEFT JOIN rssys.items ii ON i2.item_code=ii.item_code LEFT JOIN rssys.itmunit iu  ON ii.sales_unit_id=iu.unit_id WHERE iu.unit_shortcode='SET' ORDER BY ii.item_desc ASC");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_md + "m_assembled_items_list.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("userid", GlobalClass.username);
                }

                else if (action == 4080) // Vehicle Delivery Note
                {
                    DataTable dt = db.QueryBySQLCode("SELECT m.d_name, m.d_addr2, m.d_tel, m.d_cntc_no FROM rssys.m06 m LEFT JOIN rssys.orhdr o ON m.d_code=o.debt_code WHERE o.debt_code='" + client + "' LIMIT 1");
                    DataTable dtc = db.QueryBySQLCode("SELECT vc.* ,COALESCE((SELECT vl.vc_desc FROM rssys.vclne vl WHERE vl.ord_code='" + trans_no + "' AND vl.vc_code=vc.vc_code),'') AS vc_desc2 FROM rssys.vehicle_checklist vc WHERE COALESCE(vc.cancel,'')<>'Y'");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_sales + "s_auto_sales_release_unit.rpt");
                    //myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();
                      
                    String cust_details = dt.Rows[0]["d_name"].ToString() + "\n" +
                            (dt.Rows[0]["d_tel"] ?? "").ToString() + " / " + (dt.Rows[0]["d_cntc_no"] ?? "").ToString() + "\n" +
                            (dt.Rows[0]["d_addr2"] ?? "").ToString();
                    String checklist = "";
                    String checklist2 = "";

                    for (int i = 0; i < dtc.Rows.Count; i++) {
                        if (!String.IsNullOrEmpty(dtc.Rows[i]["vc_desc2"].ToString())){
                            if (i < 16){
                                checklist += "[X] " + dtc.Rows[i]["vc_desc2"].ToString() + "\n";
                            }
                            else{
                                checklist2 += "[X] " + dtc.Rows[i]["vc_desc2"].ToString() + "\n";
                            }
                        }
                        else {
                            if (i < 16){
                                checklist += "[  ] " + dtc.Rows[i]["vc_desc"].ToString() + "\n";
                            }
                            else{
                                checklist2 += "[  ] " + dtc.Rows[i]["vc_desc"].ToString() + "\n";
                            }
                        }
                    }

                    add_fieldparam("cust_details", cust_details);
                    add_fieldparam("remarks", remarks);
                    add_fieldparam("t_date", t_date);
                    add_fieldparam("sc", "");

                    add_fieldparam("modeldesc", unit);
                    add_fieldparam("seriesyear", year_model);
                    add_fieldparam("prodyear", year_model);
                    add_fieldparam("condsticker", plate_no);
                    add_fieldparam("serial_no", vin_no); 
                    add_fieldparam("engine_no", engine_no);
                    add_fieldparam("color", color);
                    add_fieldparam("vsi_no", vsi_no);

                    add_fieldparam("checklist", checklist);
                    add_fieldparam("checklist2", checklist2);

                    add_fieldparam("preparedby", agent);
                    add_fieldparam("approvedby", "");
                    add_fieldparam("reviewedby", "");
                    add_fieldparam("operation", "");
                    add_fieldparam("opefinance", "");
                    add_fieldparam("gmanager", "");

                    add_fieldparam("releasedby", releasedby);
                    add_fieldparam("km_reading", last_km_reading);
                    add_fieldparam("cust_name", dt.Rows[0]["d_name"].ToString());
                    add_fieldparam("c_date", d_release + " " + t_release);

                    add_fieldparam("pickup", pickupby);
                    add_fieldparam("unitdelby", unitdelby);
                    add_fieldparam("d_unit", d_unitdel);
                    add_fieldparam("t_unit", t_unitdel);
                    add_fieldparam("guard", guardby);

                    add_fieldparam("cust_auth", dt.Rows[0]["d_name"].ToString());

                    add_fieldparam("userid", GlobalClass.username);
                }
                else if (action == 4081) // Terms and Conditions
                {

                    //DataTable dt = db.QueryBySQLCode("");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_sales + "s_release_delivery_unit.rpt");
                    //myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();
                    
                    add_fieldparam("cust_name", client);
                    add_fieldparam("sales_consultant", "");
                    add_fieldparam("accounting", "");
                    add_fieldparam("gsm", "");
                    add_fieldparam("sales_manager", "");
                        
                    add_fieldparam("userid", GlobalClass.username);
                }
                else if (action == 4090) // GP Computaion
                {
                    DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.orhdr WHERE ord_code='"+trans_no+"' LIMIT 1");

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_sales + "s__gp_computation.rpt");
                    //myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    clr_param();

                    add_fieldparam("inv_no", "");
                    add_fieldparam("customer", "");
                    add_fieldparam("cntc_no", "");
                    add_fieldparam("tin_no", "");
                    add_fieldparam("sales_exe", "");
                    add_fieldparam("srp", "");
                    add_fieldparam("srp", "");
                    add_fieldparam("cs_no", "");
                    add_fieldparam("term", "");
                    add_fieldparam("bank", "");
                    add_fieldparam("add_on_rate", "");
                    add_fieldparam("dp_per", "");
                    add_fieldparam("actual_dp", "");
                    add_fieldparam("dr_no", "");
                    add_fieldparam("inv_date", "");
                    add_fieldparam("model", "");
                    add_fieldparam("srp_net", "");
                    add_fieldparam("less_dealer_cnet", "");
                    add_fieldparam("dealer_margin", "");
                    add_fieldparam("unit_margin", "");
                    add_fieldparam("unit_subdiry", "");
                    add_fieldparam("unit_others", "");
                    add_fieldparam("vat_exc", "");
                    add_fieldparam("total_breakdown", "");
                    add_fieldparam("add_dealer_inc", "");
                    add_fieldparam("less_sales_disc", "");
                    add_fieldparam("gross_profit", "");
                    add_fieldparam("perc_adi", "");
                    add_fieldparam("lto_reg", "");
                    add_fieldparam("tpl", "");
                    add_fieldparam("compre_insurance", "");
                    add_fieldparam("chat_mortage", "");
                    //loop
                    String AOC_items = "";
                    String AOC_prices = "";
                    int others = 0;
                    double totalAOC = 0.00;
                    double othersTotal = 0.00;
                    DataTable dtc = db.QueryBySQLCode("SELECT * FROM rssys.orlne WHERE ord_code='" + trans_no + "' ");
                    for (int i = 0; i < dtc.Rows.Count || i < 5; i++)
                    {
                        String item = "";
                        double price = 0;

                        try 
                        {
                            item = "";
                        }
                        catch { }
                        
                        if (i < 5 || dtc.Rows.Count == 6)
                        {
                            AOC_items += (!String.IsNullOrEmpty(item) ? (item.Length < 16 ? item + " :" : item.Substring(0, 16) + "..:") : "-----") + "\n";
                            AOC_prices += gm.toAccountingFormat(price) + "\n";
                        }
                        else
                        {
                            othersTotal += price;
                            others++;
                        }
                        
                        totalAOC += price;
                    }
                    if (others != 0){
                        AOC_items += "Others(" + others + ") :\n";
                        AOC_prices += gm.toAccountingFormat(othersTotal) + "\n";
                    }


                    add_fieldparam("tablet", "");
                    add_fieldparam("tint", "");
                    add_fieldparam("visor", "");
                    add_fieldparam("seat_cover", "");
                    add_fieldparam("pdi", "");
                    add_fieldparam("fuel", "");

                    add_fieldparam("access_cost", "");
                    add_fieldparam("total_cos_pf", "");
                    add_fieldparam("gp_cos_pf", "");

                    add_fieldparam("unit_comm", "");
                    add_fieldparam("sales_inc", "");
                    add_fieldparam("referal", "");
                    add_fieldparam("override", "");
                    add_fieldparam("finance_cost", "");
                    add_fieldparam("other_dir_exp", "");
                    add_fieldparam("total_dir_exp", "");
                    add_fieldparam("profit_b4_pay", "");
                    add_fieldparam("payback", "");
                    add_fieldparam("net_profit", "");
                    add_fieldparam("fuel_trans_cost", "");
                    add_fieldparam("market_exp", "");
                    add_fieldparam("preparedby", "");
                    add_fieldparam("control_no", "");
                    add_fieldparam("month", "");
                    add_fieldparam("total_dir_exp", "");


                    add_fieldparam("userid", GlobalClass.username);
                }
                if (action > 0 && action != 3)
                {
                    inc_pbar(5);
                    crParameterDiscreteValue.Value = comp_name;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["comp_name"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    clr_param();

                    inc_pbar(5);
                    crParameterDiscreteValue.Value = comp_addr;
                    crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["comp_addr"];
                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    
                    clr_param();

                    if (Array.IndexOf(new int[] { 20, 21, 4031, 4032, 4033, 4034, 4035, 4036 }, action)>=0)
                    {
                        inc_pbar(5);
                        crParameterDiscreteValue.Value = comp_number;
                        crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["comp_number"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;

                        clr_param();
                    }
                }

                if (action > 0)
                {
                    inc_pbar(4);
                    disp_reportviewer(myReportDocument);
                }

                pbar_panl_hide();
           }
           catch (Exception er) { MessageBox.Show(":"+er.Message); }
        }
    }
}
