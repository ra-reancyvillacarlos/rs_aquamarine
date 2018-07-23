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
    public partial class call_history : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        public call_history()
        {
            InitializeComponent();
            disp_list();
        }

        private void call_history_Load(object sender, EventArgs e)
        {

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            call_history_entry frm = new call_history_entry(this, "", true);
            frm.ShowDialog();
            disp_list();
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = 0;
            String code;
            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;
                code = dgv_list["dgv_call_history_number", r].Value.ToString();
                call_history_entry frm = new call_history_entry(this, code, false);
                frm.ShowDialog();
                disp_list();
            }
            else
            {

                MessageBox.Show("No records to be selected.");
            }
        }
        public void disp_list()
        {

            try { dgv_list.Rows.Clear(); }catch{}
            try {
                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.call_history WHERE (date_to_call BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') ORDER BY call_history_number ASC");

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
                    row.Cells["dgv_client_number"].Value = dt.Rows[r]["client_number"].ToString();
                    row.Cells["dgv_client_name"].Value = dt.Rows[r]["client_name"].ToString();
                    row.Cells["dgv_contact_no"].Value = dt.Rows[r]["contact_no"].ToString();
                }
            
            } catch{}
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int r = 0;
                String code = "";
                r = dgv_list.CurrentRow.Index;
                code = dgv_list["dgv_client_number", r].Value.ToString();
                s_ServiceHistory frm = new s_ServiceHistory(this, code);
                frm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("No records to be selected.");
            }
        }

        private void call_history_Load_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
        }
        
    }
}
