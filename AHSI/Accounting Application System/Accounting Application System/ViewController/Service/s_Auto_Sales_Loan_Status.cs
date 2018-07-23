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
    public partial class s_Auto_Sales_Loan_Status : Form
    {
        private dbSales db = new dbSales();
        private GlobalClass gc = new GlobalClass();
        private GlobalMethod gm = new GlobalMethod();
        //FORMS
        s_Sales_Auto frm_sales_auto;
        s_Release_Deliver_Unit frm_reldel_unit;
        //OTHERS

        private void s_Auto_Sales_Loan_Status_Load(object sender, EventArgs e) { }

        public s_Auto_Sales_Loan_Status()
        {
            InitializeComponent();
            init_load();
        }
        public s_Auto_Sales_Loan_Status(s_Sales_Auto frm) : this() // gikan sa GOOGLE
        {
            frm_sales_auto = frm;
        }
        public s_Auto_Sales_Loan_Status(s_Release_Deliver_Unit frm) : this() // gikan sa GOOGLE
        {
            frm_reldel_unit = frm;
        }
        public void init_load()
        {
            gc.load_decision(cbo_status);

            frm_clear();
            disp_list();
        }
        public void frm_clear()
        {
            cbo_status.Text = "APPROVED";
            txt_customer.Text = "";
        }
        public void disp_list()
        {
            String WHERE = "";
            try
            {
                try { dgv_list.Rows.Clear(); }
                catch { }

                if (cbo_status.SelectedIndex != -1) {
                    WHERE += " AND af.status='" + cbo_status.Text + "' ";
                }
                //if(false){
                //    WHERE += (WHERE.Length != 0? " AND ": "") + " status='" + cbo_status.SelectedValue + "' ";
                //}
                if (txt_customer.Text.Length > 0)
                {
                    WHERE += " AND af.cust_name=$$%" + txt_customer + "%$$ ";
                }


                DataTable dt = db.QueryBySQLCode("SELECT af.financer_code,af.app_no,af.m06_finance_code,af.cust_name,af.status,af.m06_finance_name,a.trnx_date FROM rssys.autoloanfinancer af LEFT JOIN rssys.autoloandhr a ON a.app_no=af.app_no WHERE (a.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + (WHERE.Length != 0 ? WHERE : "") + " AND af.app_no NOT IN (SELECT app_no FROM  rssys.orhdr WHERE COALESCE(app_no,'')<>'') ORDER BY financer_code ASC");

                dgv_list.DataSource = dt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_list.Rows.Count > 0)
                {
                    int r = dgv_list.CurrentRow.Index;
                    String code = dgv_list["dgvl_appno", r].Value.ToString();
                    if (!String.IsNullOrEmpty(code))
                    {
                        if (frm_sales_auto != null)
                        {
                            frm_sales_auto.setInfo_fromAutoLoan(code);
                        }
                        if (frm_reldel_unit != null)
                        {
                            String financer = dgv_list["dgvl_financer", r].Value.ToString();
                            String decision = dgv_list["dgvl_status", r].Value.ToString();
                            frm_reldel_unit.setInfo_fromAutoLoan(code, financer, decision);
                        }
                        this.Close();
                    }
                }
            }catch{}
        }


        private void btn_filter_Click(object sender, EventArgs e)
        {
            disp_list();
        }


    }
}
