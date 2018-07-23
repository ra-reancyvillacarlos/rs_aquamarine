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
    public partial class z_add_recip : Form
    {
        private dbCRM db;
        private crm_TextBlast_Scheduling crm;
        private EmailBlastSchedule ebs;
        public z_add_recip(crm_TextBlast_Scheduling crm)
        {
            this.crm = crm;
            InitializeComponent();
            db = new dbCRM();
            disp_list();
        }
        public z_add_recip(EmailBlastSchedule ebs)
        {
            this.ebs = ebs;
            InitializeComponent();
            db = new dbCRM();
           
        }
        private void z_add_recip_Load(object sender, EventArgs e)
        {
            disp_list();
        }
        private void disp_list()
        {            
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                String searchname = txt_search.Text;

                DataTable dt = db.get_customer_contact_list(searchname);
                int i = 0;

                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["dgvl_code"].Value = dt.Rows[r]["d_code"].ToString();
                        if (String.IsNullOrEmpty(dt.Rows[r]["firstname"].ToString()))
                        {
                            row.Cells["dgvl_name"].Value = dt.Rows[r]["d_name"].ToString();
                        }
                        else
                        {
                            row.Cells["dgvl_name"].Value = dt.Rows[r]["firstname"].ToString() + " " + dt.Rows[r]["lastname"].ToString() + dt.Rows[r]["mname"].ToString();
                        }

                        row.Cells["dgvl_mobile1"].Value = dt.Rows[r]["d_cntc_no"].ToString();
                        row.Cells["dgvl_mobile2"].Value = dt.Rows[r]["d_tel"].ToString();
                        row.Cells["dgvl_email"].Value = dt.Rows[r]["d_email"].ToString();
                        
                        i++;
                    }              
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            int r = 0;
            string check = null;
            foreach (DataGridViewRow row in dgv_list.Rows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("code");
                dt.Columns.Add("name");
                dt.Columns.Add("mobile1");
                dt.Columns.Add("mobile2");
                dt.Columns.Add("email");
                try
                {
                    DataRow newRow = dt.NewRow();
                    if(String.IsNullOrEmpty(dgv_list["dgvl_ckbox", r].Value.ToString()) == false)
                    {
                        newRow["code"] = dgv_list["dgvl_code", r].Value.ToString();
                        newRow["name"] = dgv_list["dgvl_name", r].Value.ToString();
                        newRow["mobile1"] = dgv_list["dgvl_mobile1", r].Value.ToString();
                        newRow["mobile2"] = dgv_list["dgvl_mobile2", r].Value.ToString();
                        newRow["email"] = dgv_list["dgvl_email", r].Value.ToString();

                        dt.Rows.Add(newRow);
                        if(this.crm != null)
                        {
                            this.crm.set_selected_recip(dt);
                        }
                        if(this.ebs != null)
                        {
                            this.ebs.set_selected_recip(dt);
                        }
                    }
                }
                catch (Exception ex)
                { }
                r++;
               
            }
            this.Close();
        }

        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            int rowIndex = -1;
            String typname = "dgvl_name";
            String searchValue = txt_search.Text;

            try
            {

                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                            .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                            .FirstOrDefault();

                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;
            }
            catch (Exception) { }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_list();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
    }
}
