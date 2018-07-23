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
    public partial class s_AutoLoanStatus : Form
    {
        private dbSales db;
        private GlobalClass gc;
        private GlobalMethod gm;
        
        public s_AutoLoanStatus()
        {
            InitializeComponent();
            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            disp_list();
        }

        private void s_AutoLoanStatus_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }
        }

        public void disp_list()
        {
            Double total_amnt = 0.00, paid = 0.00;
            String car_item_code = "";

            String WHERE = "";
            try
            {
                try { dgv_list.Rows.Clear(); }
                catch { }

                if (cbo_status.SelectedIndex != -1)
                {
                    WHERE += " AND af.status='" + cbo_status.Text + "' ";
                }
                //if(false){
                //    WHERE += (WHERE.Length != 0? " AND ": "") + " status='" + cbo_status.SelectedValue + "' ";
                //}
                if (txt_customer.Text.Length > 0)
                {
                    WHERE += " AND af.cust_name=$$%" + txt_customer + "%$$ ";
                }


                DataTable dt = db.QueryBySQLCode("SELECT af.financer_code,af.app_no,af.m06_finance_code,af.cust_name,af.status,af.m06_finance_name,a.trnx_date FROM rssys.autoloanfinancer af LEFT JOIN rssys.autoloandhr a ON a.app_no=af.app_no WHERE (a.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + (WHERE.Length != 0 ?  WHERE : "") + " ORDER BY financer_code ASC");

                dgv_list.DataSource = dt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            //AdjustColumnOrder();

        }
        public void set_value(String app_no, String cust_no, String credit_des, String finance_code, String financer) {
            String val = "",col = "",col2="";
            col = "credit_des='" + credit_des + "'";
            col2 = "status='" + credit_des + "'";

            if (credit_des.ToLower() == "approved"){
                col += ",credit_advice='"+db.get_systemdate("")+"'";
            }

            if (db.UpdateOnTable("autoloandhr", col, "app_no='" + app_no + "' AND cust_no='" + cust_no + "'") && db.UpdateOnTable("autoloanfinancer", col2, "financer_code='" + finance_code + "'"))
            {
                MessageBox.Show("Record Successfully Updated.");

            }
            else 
            {
                MessageBox.Show("Saving Failed");
            }
            int i = dgv_list.CurrentRow.Index;
            
            disp_list();
            dgv_list.ClearSelection();
            dgv_list.Rows[i].Selected = true;
        }
        private void dgv_list_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("Wewada");
           
            
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dgv_list.CurrentRow.Index.ToString());
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            if (dgv_list.CurrentRow != null)
            {
                int i = dgv_list.CurrentRow.Index;
                String financer = "";
                String app_no = "";
                String cust_no = "";
                String cust_name = "";
                String credit_des = "";
                if (dgv_list.Rows.Count > 0 && String.IsNullOrEmpty(dgv_list["dgvl_appno", i].Value.ToString()) == false)
                {
                    financer = dgv_list["dgvl_finance_code", i].Value.ToString();
                    app_no = dgv_list["dgvl_appno", i].Value.ToString();
                    cust_no = dgv_list["dgvl_custno", i].Value.ToString();
                    cust_name = dgv_list["dgvl_custname", i].Value.ToString();
                    credit_des = dgv_list["dgvl_status", i].Value.ToString();
                    String m06_cust = dgv_list["dgvl_financer", i].Value.ToString();
                    auto_status frm = new auto_status(this, financer, app_no, cust_no, cust_name, credit_des, m06_cust);
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("No rows selected");
                }
            }
            else {
                MessageBox.Show("No rows selected");
            }


        }

        private void dgv_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void btn_new_Click(object sender, EventArgs e)
        {
            disp_list();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

        }
    }
}
