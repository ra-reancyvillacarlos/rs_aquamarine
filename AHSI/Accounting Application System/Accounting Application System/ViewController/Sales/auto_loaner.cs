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
    public partial class auto_loaner : Form
    {
        private GlobalClass gc = new GlobalClass();
        private GlobalMethod gm = new GlobalMethod();
        private thisDatabase db = new thisDatabase();

        private auto_loanapplication _frm_auto = null;

        private String gfolio = "";
        private String gtype = "";
        private Boolean newitem = false;

        private List<int> list = new List<int>();
        public auto_loaner()
        {
            InitializeComponent();
            init_load();
        }
        public auto_loaner(auto_loanapplication frm) : this()
        {
            _frm_auto = frm;
            disp_items();
        }

        private void init_load() 
        {

        }

        public void disp_items()
        {
            DataTable dtca = db.QueryBySQLCode("SELECT d_code,d_name FROM rssys.m06 WHERE type='Financer'");
           
            try
            {
                for (int r = 0; dtca.Rows.Count > r; r++)
                {
                    int i = dgv_list_ca.Rows.Add();
                    DataGridViewRow row = dgv_list_ca.Rows[i];

                    row.Cells["dgvlf_line"].Value = (r + 1).ToString();
                    row.Cells["dgvlf_code"].Value = dtca.Rows[r]["d_code"].ToString();
                    row.Cells["dgvlf_name"].Value = dtca.Rows[r]["d_name"].ToString();
                    row.Cells["dgvlf_status"].Value = "PENDING";

                    if (_frm_auto != null)
                    {
                        for (int r2 = 0; r2 < _frm_auto.dgv_loaner_list.Rows.Count; r2++)
                        {
                            String _frm_auto_dgvlca_code = _frm_auto.dgv_loaner_list["loaner_no", r2].Value.ToString();
                            if (_frm_auto_dgvlca_code.Equals(dtca.Rows[r]["d_code"].ToString()))
                            {
                                row.Cells["dgvlca2_chk"].Value = true;
                                list.Add(r);
                            }
                        }
                    }
                }


            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }
        private void auto_loaner_Load(object sender, EventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            list.Sort();
            _frm_auto.dgv_loaner_list.Rows.Clear();

            try
            {

                for (int r = 0; r < list.Count; r++)
                {
                    int indx = (int)list[r];

                    int i = _frm_auto.dgv_loaner_list.Rows.Count;
                    _frm_auto.dgv_loaner_list.Rows.Add();

                    _frm_auto.dgv_loaner_list["loaner_line", i].Value = dgv_list_ca["dgvlf_line", indx].Value.ToString();
                    _frm_auto.dgv_loaner_list["loaner_no", i].Value = dgv_list_ca["dgvlf_code", indx].Value.ToString();
                    _frm_auto.dgv_loaner_list["loaner_name", i].Value = dgv_list_ca["dgvlf_name", indx].Value.ToString();
                    _frm_auto.dgv_loaner_list["loaner_status", i].Value = dgv_list_ca["dgvlf_status", indx].Value.ToString();
                }
            }
            catch { }
            this.Close();
        }

        private void dgv_list_ca_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                int r = dgv_list_ca.CurrentRow.Index;
                String code = (dgv_list_ca["dgvlf_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    String strbool = (dgv_list_ca["dgvlca2_chk", r].Value ?? "false").ToString();
                    if (!Convert.ToBoolean(strbool))
                    {
                        if (list.IndexOf(r) == -1)
                        {
                            list.Add(r);
                        }
                    }
                    else
                    {
                        list.Remove(r);
                    }
                }
                else {
                    dgv_list_ca.Rows.RemoveAt(r);
                }

            }
            catch {}
        }
    }
}
