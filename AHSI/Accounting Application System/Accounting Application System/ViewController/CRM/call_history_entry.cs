using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class call_history_entry : Form
    {
        call_history _frm_call = null;
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        thisDatabase db = new thisDatabase();
        Boolean isnew = false;
        String code = "";

        DateTime dt_to_call = DateTime.Now;

        public call_history_entry()
        {
            InitializeComponent();
            //gc.load_payroll_period(cbo_period_code);
            //gc.load_dept(cbo_department_frm);
            //gc.load_dept(cbo_department_until);
            //gc.load_employee(cbo_employee);
            gc.load_contact(cbo_client);
            //gc.load_salesclerk(cbo_clerk);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public call_history_entry(call_history frm, String pk, Boolean _isnew)
        {
            InitializeComponent();
            //gc.load_payroll_period(cbo_period_code);
            //gc.load_dept(cbo_department_frm);
            //gc.load_dept(cbo_department_until);
            //gc.load_employee(cbo_employee);
            gc.load_contact(cbo_client);
            //gc.load_salesclerk(cbo_clerk);

            _frm_call = frm;
            code = pk;
            isnew = _isnew;
            if (isnew == false)
            {
                init_load(code);
            }
        }
        public void init_load(String pk)
        {
            DataTable dt = null;
            dt = db.QueryBySQLCode("SELECT * FROM rssys.call_history WHERE call_history_number='" + pk + "'");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt_to_call = DateTime.Parse(gm.toDateString(dt.Rows[i]["date_to_call"].ToString(), "") + " " + dt.Rows[i]["time_to_call"].ToString());

                    txt_code.Text = dt.Rows[i]["call_history_number"].ToString();
                    cbo_client.SelectedValue = dt.Rows[i]["client_number"].ToString();
                    dtp_time_to_call.Value = dtp_date_to_call.Value = dt_to_call;
                    //dtp_date_time_last_called.Value = gm.toDateValue(dt.Rows[i]["date_time_last_called"].ToString());
                    //cbo_clerk.SelectedValue = dt.Rows[i]["last_clerk_called"].ToString();
                    cbo_status.Text = dt.Rows[i]["status"].ToString();
                    rtxt_remark.Text = dt.Rows[i]["remark"].ToString();
                    //cbo_department_frm.SelectedValue = dt.Rows[i]["dept_frm"].ToString();
                    //cbo_department_until.SelectedValue = dt.Rows[i]["dept_until"].ToString();
                    //cbo_employee.SelectedValue = dt.Rows[i]["employee"].ToString();

                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {

            String call_history_number, date_to_call, time_to_call, date_time_last_called, last_clerk_called, status, remark, client_number, client_name,contact_no;
            String col = "", val = "";
            Boolean success = false;
            String table = "call_history";

            if (cbo_client.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Client");
                cbo_client.DroppedDown = true;
            }
            /*else if(cbo_clerk.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Clerk");
                cbo_clerk.DroppedDown = true;
            }*/
            else if(cbo_status.SelectedIndex==-1)
            {
                MessageBox.Show("Please Select Contact Status.");
                cbo_status.DroppedDown = true;
            }
            else
            {
                call_history_number = txt_code.Text;
                date_to_call = dtp_date_to_call.Value.ToString("yyyy-MM-dd");
                time_to_call = dtp_time_to_call.Value.ToString("h:mm:ss tt");
                //date_time_last_called = dtp_date_time_last_called.Value.ToString("yyyy-MM-dd h:mm:ss tt");
                //last_clerk_called = cbo_clerk.SelectedValue.ToString();
                status = cbo_status.Text;
                remark = rtxt_remark.Text;
                client_number = cbo_client.SelectedValue.ToString();
                client_name = cbo_client.Text;
                contact_no = txt_contact_no.Text;

                if (isnew)
                {
                    call_history_number = db.get_pk("call_history_number");
                    col = "call_history_number, date_to_call, time_to_call, date_time_last_called, userid, status, remark, client_number, client_name,contact_no";
                    val = "'" + call_history_number + "', '" + date_to_call + "', '" + time_to_call + "', '" + /*date_time_last_called*/"" + "', '" + GlobalClass.username +"', '" + status + "', '" + remark + "', '" + client_number + "', '" + client_name + "', '" + contact_no + "'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        db.set_pkm99("call_history_number", db.get_nextincrementlimitchar(call_history_number, 8));
                        success = true;
                        //add_items(loan_code);
                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable(table, "call_history_number='" + call_history_number + "'");
                        MessageBox.Show("Failed on saving.");
                    }


                }
                else
                {

                    col = "call_history_number='" + call_history_number + "', date_to_call='" + date_to_call + "', time_to_call='" + time_to_call + "', date_time_last_called='" + /*date_time_last_called*/"" + "', userid='" + GlobalClass.username + "', status='" + status + "', remark='" + remark + "', client_number='" + client_number + "', client_name='" + client_name + "', contact_no='" + contact_no + "'";

                    if (db.UpdateOnTable(table, col, "call_history_number='" + call_history_number + "'"))
                    {
                        //db.DeleteOnTable("soalne", "loan_code='" + loan_code + "'");
                        //add_items(code);

                        success = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
                        success = false;
                    }


                }
                if (success)
                {

                    this.Close();
                    frm_clear();

                }
            }
        }
        public void frm_clear()
        {
            cbo_client.SelectedIndex = -1;
            //cbo_clerk.SelectedIndex = -1;
            //cbo_employee.SelectedIndex = -1;
            //cbo_department_frm.SelectedIndex = -1;
            //cbo_department_until.SelectedIndex = -1;
            txt_code.Text = "";

        }

        private void call_history_entry_Load(object sender, System.EventArgs e)
        {

        }

        private void label2_Click(object sender, System.EventArgs e)
        {

        }

        private void cbo_client_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (cbo_client.SelectedIndex >= 0)
                {
                    String client = cbo_client.SelectedValue.ToString();
                    DataTable dt = new DataTable();
                    dt = db.QueryBySQLCode("SELECT * FROM rssys.m06 WHERE d_code='" + client + "'");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                            txt_contact_no.Text = dt.Rows[i]["d_tel"].ToString();
                    }

                    dt = db.QueryBySQLCode("SELECT * FROM rssys.call_history WHERE client_number='" + client + "' AND call_history_number<>'" + txt_code.Text + "' ORDER BY call_history_number DESC LIMIT 5");
                    
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        
                        int i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];
                        
                        row.Cells["dgv_call_history_number"].Value = dt.Rows[r]["call_history_number"].ToString();
                        row.Cells["dgv_date_to_call"].Value = gm.toDateString(dt.Rows[r]["date_to_call"].ToString(), "");
                        row.Cells["dgv_time_to_call"].Value = dt.Rows[r]["time_to_call"].ToString();
                        //row.Cells["dgv_date_time_last_called"].Value = dt.Rows[r]["date_time_last_called"].ToString();
                        row.Cells["dgv_userid"].Value = dt.Rows[r]["userid"].ToString();
                        row.Cells["dgv_status"].Value = dt.Rows[r]["status"].ToString();
                        row.Cells["dgv_remark"].Value = dt.Rows[r]["remark"].ToString();
                        row.Cells["dgv_contact_no"].Value = dt.Rows[r]["contact_no"].ToString();
                    }



                }
            }
            catch { }
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private void btn_customer_Click(object sender, System.EventArgs e)
        {
            m_customers frm = new m_customers(this, true);
            frm.ShowDialog();
        }
        public void set_custvalue_frm(String custcode)
        {
            String prevClient = (cbo_client.SelectedValue??"").ToString();

            cbo_client.SelectedValue = custcode;

            if (cbo_client.SelectedValue == null)
            {
                cbo_client.SelectedValue = prevClient;
                MessageBox.Show("Invalid! Your selected customer is not available.");
            }
        }

        private void dtp_date_to_call_ValueChanged(object sender, System.EventArgs e)
        {
            dtp_date_to_call.Value = dt_to_call;
        }

        private void dtp_time_to_call_ValueChanged(object sender, System.EventArgs e)
        {
            dtp_time_to_call.Value = dt_to_call;
        }
    }
}
