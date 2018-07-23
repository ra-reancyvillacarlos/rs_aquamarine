using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    public partial class auto_status : Form
    {

        GlobalClass gc;
        GlobalMethod gm;
        thisDatabase db;
        s_AutoLoanStatus frm_auto;
        String app_no = "", cust_no = "", cust_name = "", credit_des = "",finance_code="",financer="";
        Boolean timed = false;
        public auto_status()
        {


            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new thisDatabase();
            gc.load_decision(cbo_decision);

        }

        public auto_status(s_AutoLoanStatus frm,String finance_code, String app_no, String cust_no, String cust_name, String credit_des,String financer)
        {

            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new thisDatabase();
            gc.load_decision(cbo_decision);
            this.app_no = app_no;
            this.cust_no = cust_no;
            this.cust_name = cust_name;
            this.credit_des = credit_des;
            this.finance_code = finance_code;
            this.financer = financer;
            frm_auto = frm;
            initload();
        }
        public void initload()
        {
            txt_app_no.Text = app_no.ToString();
            txt_custname.Text = cust_name.ToString();
            txt_custno.Text = cust_no.ToString();
            txt_finance_code.Text = finance_code.ToString();
            txt_financer.Text = financer.ToString();
            cbo_decision.Text = credit_des.ToString();
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT al.app_no, al.dp_amt, al.reg_charges, CASE WHEN COALESCE(o.amnt_financed,'0.00')<>'0.00' THEN o.amnt_financed ELSE al.amt_finance END AS amt_finance, COALESCE(o.credit_advice,al.credit_advice) AS credit_advice  FROM rssys.autoloandhr al LEFT JOIN rssys.orhdr o ON o.app_no=al.app_no WHERE al.app_no='" + txt_app_no.Text + "'");
                txt_date_credit.Text = dt.Rows[0]["credit_advice"].ToString();
                try {
                    if (!String.IsNullOrEmpty(txt_date_credit.Text))
                    {
                        dtp_date_credit.Value = DateTime.Parse(txt_date_credit.Text);
                        txt_date_credit.Visible = false;
                    }
                }
                catch {  }
                txt_srp.Text = gm.toAccountingFormat(dt.Rows[0]["reg_charges"].ToString());
                txt_dp_amt.Text = gm.toAccountingFormat(dt.Rows[0]["dp_amt"].ToString());
                txt_amt_finance.Text = gm.toAccountingFormat(dt.Rows[0]["amt_finance"].ToString());
            }
            catch { }

        }
        //auto_status(this,app_no,cust_no,cust_name,credit_des)
        private void auto_status_Load(object sender, EventArgs e)
        {
           
  
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void save()
        {

           
        
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (frm_auto != null)
            {
                if (cbo_decision.SelectedIndex == -1)
                { 
                    MessageBox.Show("Please Select Decision Status.");
                    cbo_decision.DroppedDown = true;
                }
                //label5.Visible = true;
                //progressBar1.Visible = true;
                //timer1.Enabled = true;
                //timer1.Start();
                //timer1.Interval = 60;
                //progressBar1.Maximum = 100;
                //timer1.Tick += new EventHandler(timer1_Tick);
                //if (progressBar1.Value == 98)
                //{
                //    save();
                
                //}
                try
                {
                    frm_auto.set_value(txt_app_no.Text, txt_custno.Text, cbo_decision.Text, txt_finance_code.Text, txt_financer.Text);

                }
                catch { }
                this.Close();
            }
        }

        private void cbo_decision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_decision.Text.ToLower() == "APPROVED".ToLower()) {
                if (String.IsNullOrEmpty(txt_date_credit.Text)){
                    txt_date_credit.Text = db.get_systemdate("");
                    txt_date_credit.Visible = false;
                }
                dtp_date_credit.Value = DateTime.Parse(txt_date_credit.Text);
            }
        }

        private void dtp_date_credit_ValueChanged(object sender, EventArgs e)
        {
            try{ dtp_date_credit.Value = DateTime.Parse(txt_date_credit.Text); }
            catch { }
        }

       
    }
}
