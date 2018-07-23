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
    public partial class z_add_soa : Form
    {
        a_CollectionEntry _frm_col = null;
        String gfolio = "";
        String gtype = "";

        public z_add_soa(a_CollectionEntry frm)
        {
            InitializeComponent();

            _frm_col = frm;
            txt_gfolio.Text = "";//_frm_col.get_billed_to_no();
            cbo_viewby.SelectedIndex = 0;

            disp_dgv_list_folio();
        }

        private void z_add_soa_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            Boolean status = false;
            DataTable dt = new DataTable();
            int r;

            dt.Columns.Add("soa");
            dt.Columns.Add("clientcode");
            dt.Columns.Add("clientname");
            dt.Columns.Add("soadate");
            dt.Columns.Add("balance");

            try
            {
                r = dgv_list_folio.CurrentRow.Index;

                DataRow newRow = dt.NewRow();
                /*
                newRow["dgvl1_reg_num"] = dgv_list_folio["dgvl1_reg_num", r].Value.ToString();
                newRow["dgvl1_rmrttyp"] = dgv_list_folio["dgvl1_rmrttyp", r].Value.ToString();
                newRow["dgvl1_full_name"] = dgv_list_folio["dgvl1_full_name", r].Value.ToString();
                */
                newRow["soa"] = dgv_list_folio["dgvl1_soa", r].Value.ToString();
                newRow["clientcode"] = dgv_list_folio["dgvl1_clientcode", r].Value.ToString();
                newRow["clientname"] = dgv_list_folio["dgvl1_billedclient", r].Value.ToString();
                newRow["soadate"] = dgv_list_folio["dgvl_soadate", r].Value.ToString();
                newRow["balance"] = dgv_list_folio["dgvl1_bal", r].Value.ToString();

                dt.Rows.Add(newRow);

            }
            catch (Exception) { }        

            //_frm_col.add_soa_folio(dt);

            this.Close();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_dgv_list_folio();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void disp_dgv_list_folio()
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            DataTable dt;

            gfolio = txt_gfolio.Text;
            dt = db.get_soalistWithBalance(txt_soa.Text, txt_gfolio.Text, cbo_viewby.SelectedIndex);

            clear_dgv_list_folio();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list_folio.Rows.Add();
                    DataGridViewRow row = dgv_list_folio.Rows[i];
                    
                    row.Cells["dgvl1_soa"].Value = dt.Rows[r]["soa_code"].ToString();
                    row.Cells["dgvl1_billedclient"].Value = dt.Rows[r]["debt_name"].ToString();
                    row.Cells["dgvl1_rom_code"].Value = dt.Rows[r]["rom_code"].ToString();
                    row.Cells["dgvl_soadate"].Value = gm.toDateString(dt.Rows[r]["soa_date"].ToString(), "MM/dd/yyyy");
                    row.Cells["dgvl1_chg_dtfrm"].Value = gm.toDateString(dt.Rows[r]["chg_dtfrm"].ToString(), "MM/dd/yyyy");
                    row.Cells["dgvl1_chg_dtto"].Value = gm.toDateString(dt.Rows[r]["chg_dtto"].ToString(), "MM/dd/yyyy");
                    row.Cells["dgvl1_bal"].Value = dt.Rows[r]["amount"].ToString();
                    row.Cells["dgvl1_userid"].Value = dt.Rows[r]["user_id"].ToString();
                    row.Cells["dgvl1_clientcode"].Value = dt.Rows[r]["debt_code"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void clear_dgv_list_folio()
        {
            try
            {
                dgv_list_folio.Rows.Clear();
            }
            catch (Exception)
            { }
        }

        private void dgv_list_folio_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            String reg_num = "";
            int r;
            try
            {
                r = dgv_list_folio.CurrentRow.Index;

                reg_num = dgv_list_folio[2, r].Value.ToString();

                disp_guestinfo(reg_num);
                disp_chgfil(reg_num);
            }
            catch (Exception er) { MessageBox.Show(er.Message); } */
        }

        private void cbo_viewby_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgv_list_folio();
        }

        private void dgv_list_folio_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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