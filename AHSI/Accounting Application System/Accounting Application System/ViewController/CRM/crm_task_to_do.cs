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
    public partial class crm_task_to_do : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalMethod gm = new GlobalMethod();
        GlobalClass gc = new GlobalClass();
        Boolean seltbp = false;
        Boolean isnew = false;
        public crm_task_to_do()
        {
            InitializeComponent();
        }

        private void crm_task_to_do_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }

            dtp_time_last_call.CustomFormat = "hh:mm tt";
            dtp_time_last_call.ShowUpDown = true;
            dtp_time_last_call.CustomFormat = "hh:mm tt";
            dtp_time_last_call.ShowUpDown = true;

            dtp_next_call_time.CustomFormat = "hh:mm tt";
            dtp_next_call_time.ShowUpDown = true;
            dtp_next_call_time.CustomFormat = "hh:mm tt";
            dtp_next_call_time.ShowUpDown = true;
            disp_list();
        }

        private void goto_win2()
        {
            seltbp = true;
            tgp_main.SelectedTab = tgp_info;
            tbcntrl_left.SelectedTab = tpg_option_2;

            tgp_info.Show();
            tpg_option_2.Show();
            seltbp = false;
        }

        private void goto_win1()
        {
            seltbp = true;
            tgp_main.SelectedTab = tgp_list;
            tbcntrl_left.SelectedTab = tpg_option;

            tgp_list.Show();
            tpg_option.Show();
            seltbp = false;
        }

        private void tgp_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(seltbp == false)
            {
                e.Cancel = true;
            }
        }

        private void tbcntrl_left_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(seltbp == false)
            {
                e.Cancel = true;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            clear_form();
            goto_win2();
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            String code = "", d_code = "", through = "", last_called_date = "", last_called_time = "", concerns = "", remarks = "", department = "", status = "", next_call_date = "", next_call_time = "", description="";
            String table = "crm_task_to_do";
            String col = "";
            String val = "";
            Boolean success = false;

            if(cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
                return;
            }
            if (txt_desc.Text == "")
            {
                MessageBox.Show("Please enter a description");
                return;
            }
            if(txt_desc.Text == "")
            {
                MessageBox.Show("Please enter a description.");
                return;
            }
            if(txt_concerns.Text == "")
            {
                MessageBox.Show("Please enter a concern.");
                return;
            }

            code = txt_code.Text;
            d_code = cbo_customer.SelectedValue.ToString();
            if(cbo_through.SelectedIndex != -1)
            {
                through = cbo_through.SelectedItem.ToString();
            }
            description = txt_desc.Text;
            last_called_date = dtp_last_called_date.Value.ToShortDateString();
            last_called_time = dtp_time_last_call.Value.ToShortTimeString();
            concerns = txt_concerns.Text;
            remarks = txt_remarks.Text;
            if(cbo_department.SelectedIndex != -1)
            {
                department = cbo_department.SelectedItem.ToString();
            }
            if(cbo_status.SelectedIndex != -1)
            {
                status = cbo_status.SelectedItem.ToString();
            }
            next_call_date = dtp_date_next_call.Value.ToShortDateString();
            next_call_time = dtp_next_call_time.Value.ToShortTimeString();

            if(isnew)
            {
                col = "d_code, through, last_called_date, last_called_time, concerns, remarks , department, status,next_call_date,next_call_time,description";
                val = "'" + d_code + "','" + through + "','" + last_called_date + "','" + last_called_time + "'," + db.str_E(concerns) + "," + db.str_E(remarks) + ",'" + department + "','" + status + "','" + next_call_date + "','" + next_call_time + "'," + db.str_E(description) ;
                if (db.InsertOnTable(table, col, val))
                {
                    success = true;
                }
            }
            else
            {
                col = "d_code='" + d_code + "',through='" + through + "',last_called_date='" + last_called_date + "',last_called_time='" + last_called_time + "',concerns=" + db.str_E(concerns) + ",remarks=" + db.str_E(remarks) + ",department='" + department + "',status='" + status + "',next_call_date='" + next_call_date + "',next_call_time='" + next_call_time + "',description=" + db.str_E(description) ;
                if (db.UpdateOnTable(table, col, "code='" + code + "'"))
                {
                    success = true;
                }
                else
                {
                    MessageBox.Show("Failed on saving.");
                }
            }
            if(success)
            {
                goto_win1();
                clear_form();
                disp_list();
            }
        }
        private void disp_list()
        {

            try
            {
                dgv_list.Rows.Clear();
            }
            catch (Exception)
            { }
            DataTable dt, dt2;

            try
            {
                dt = db.QueryBySQLCode("SELECT d.last_called_date,d.code,c.d_code,c.d_name,d.description FROM rssys.m06 c LEFT JOIN rssys.crm_task_to_do d ON c.d_code = d.d_code WHERE c.d_code = d.d_code ORDER BY c.d_name");

                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["d_code"].Value = dt.Rows[r]["d_code"].ToString();
                    row.Cells["desc"].Value = dt.Rows[r]["description"].ToString();
                    row.Cells["customer"].Value = dt.Rows[r]["d_name"].ToString();

                    row.Cells["date_called"].Value = Convert.ToDateTime(dt.Rows[r]["last_called_date"].ToString()).ToString("dddd., MMM dd yyyy");
                    row.Cells["id"].Value = dt.Rows[r]["code"].ToString();
                    i++;
                }
            }
            catch (Exception er)
            {
                
            }
        }
        private void clear_form()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }
                    else
                    {
                        func(control.Controls);
                    }
                    if (control is ComboBox)
                    {
                        (control as ComboBox).SelectedIndex = -1;
                    }
                    else
                    {
                        func(control.Controls);
                    }
                    if (control is RichTextBox)
                    {
                        (control as RichTextBox).Clear();
                    }
                    else
                    {
                        func(control.Controls);
                    }
                    if (control is CheckBox)
                    {
                        (control as CheckBox).Checked = true;
                    }
                    else
                    {
                        func(control.Controls);
                    }
                }

            };

            func(Controls);
        }
        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            goto_win1();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            m_customers mc = new m_customers(this,true);
            mc.ShowDialog();
        }
        public void set_custvalue_frm(String code)
        {
            try { cbo_customer.Items.Clear(); }
            catch { }

            gc.load_customer(cbo_customer);
            cbo_customer.SelectedValue = code;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            isnew = false;
            int r = -1;
            String code = "", canc = "";
            isnew = false;
            DataTable dt;
            if (dgv_list.Rows.Count > 1)
            {


                r = dgv_list.CurrentRow.Index;

                try
                {
                    code = dgv_list["id", r].Value.ToString();
                    dt = db.QueryBySQLCode("SELECT d.code,c.d_code,c.d_name,d.* FROM rssys.m06 c LEFT JOIN rssys.crm_task_to_do d ON c.d_code = d.d_code WHERE d.code = '" + code + "'");
                    if (dt.Rows.Count > 0)
                    {
                        txt_code.Text = dt.Rows[0]["code"].ToString();
                        set_custvalue_frm(dt.Rows[0]["d_code"].ToString());
                        txt_desc.Text = dt.Rows[0]["description"].ToString();
                        cbo_through.SelectedItem = dt.Rows[0]["through"].ToString();
                        dtp_last_called_date.Value = Convert.ToDateTime(dt.Rows[0]["last_called_date"].ToString());
                        dtp_time_last_call.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["last_called_time"].ToString());
                        txt_concerns.Text = dt.Rows[0]["concerns"].ToString();
                        dtp_date_next_call.Value = Convert.ToDateTime(dt.Rows[0]["next_call_date"].ToString());
                        dtp_next_call_time.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["next_call_time"].ToString());
                        txt_remarks.Text = dt.Rows[0]["remarks"].ToString();
                        cbo_department.SelectedItem = dt.Rows[0]["department"].ToString();
                        cbo_status.SelectedItem = dt.Rows[0]["status"].ToString();
                       

                    }
                    goto_win2();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No customer selected.");
                }
            }
            else
            {
                MessageBox.Show("No customer selected.");
            }
            goto_win2();
        }

        private void btn_search_Click_1(object sender, EventArgs e)
        {
            m_customers mc = new m_customers(this, true);
            mc.ShowDialog();
        }
    }
}
