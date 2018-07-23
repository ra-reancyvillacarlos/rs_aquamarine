using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class m_soaperiods : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        Boolean seltbp = false;
        Boolean isnew = false;

        public m_soaperiods()
        {

            InitializeComponent();
            disp_dgvlist();
        }

        private void m_accountingperiods_Load(object sender, EventArgs e)
        {

        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
        }

        private void btn_viewitem_Click(object sender, EventArgs e)
        {
            isnew = false;
            tpg_info_enable(false);
            btn_save.Enabled = false;
            frm_clear();
            disp_info();
            goto_tbcntrl_info();
        }

        private void btn_closeitem_Click(object sender, EventArgs e)
        {
            String table = "soa_period";
            try
            {
                int r = dgv_list.CurrentRow.Index;
                String dtfrm = dgv_list["from", r].Value.ToString();
                String dtto = dgv_list["to", r].Value.ToString();

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to close this period?", "Confirm", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    if (db.UpdateOnTable(table, "closed='Y'", "soafrom='" + dtfrm + "' AND soato='" + dtto + "'"))
                    {
                        //success
                        disp_dgvlist();
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
                    }
                }
            }
            catch (Exception) { }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String soa_desc, dtfrm, dtto;
            String col = "", val = "";
            String table = "soa_period";
            String closed = "";
            Boolean success = false, cnew = false;

            dtfrm = dtp_frm.Value.ToString("yyyy-MM-dd");
            dtto = dtp_to.Value.ToString("yyyy-MM-dd");


            closed = db.get_colval(table, "closed", "soafrom='" + dtfrm + "' AND soato='" + dtto + "'");
            if (String.IsNullOrEmpty(closed))
            {
                cnew = true;
            }
            else if (closed == "Y")
            {
                if (DialogResult.Yes == MessageBox.Show("SOA Period is closed.\n Are you sure you want to open again.", "Confirm", MessageBoxButtons.YesNo))
                {
                    cnew = true;
                }
            }
            else
            {
                MessageBox.Show("Selected Date already Exist.");
            }


            if (cnew)
            {
                soa_desc = txt_desc.Text;

                if (isnew)
                {
                    col = "soa_desc, soafrom, soato, closed";
                    val = "'" + soa_desc + "','" + dtfrm + "', '" + dtto + "', 'N'";
                    if (closed == "Y")
                    {
                        db.DeleteOnTable(table, "soafrom='" + dtfrm + "' AND soato='" + dtto + "'");
                    }
                    if (db.InsertOnTable(table, col, val))
                    {
                        success = true;
                    }
                }

                if (success)
                {
                    disp_dgvlist();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                    frm_clear();
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            btn_save.Enabled = true;
            frm_clear();
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            dtp_to.Value = DateTime.Today;
            dtp_frm.Value = dtp_to.Value.AddMonths(-1);
            txt_desc.Text = dtp_frm.Value.ToString("MMMM dd,yyyy") + " - " + dtp_to.Value.ToString("MMMM dd,yyyy");
        }

        private void goto_tbcntrl_info()
        {
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_info;
            tbcntrl_option.SelectedTab = tpg_opt_2;

            tpg_info.Show();
            tpg_opt_2.Show();
            seltbp = false;
        }

        private void goto_tbcntrl_list()
        {
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_list;
            tbcntrl_option.SelectedTab = tpg_opt_1;

            tpg_list.Show();
            tpg_opt_1.Show();
            seltbp = false;
        }

        private void tpg_info_enable(Boolean flag)
        {
            dtp_frm.Enabled = flag;
            dtp_to.Enabled = flag;
        }

        private void clear_dgv()
        {
            try
            {
                dgv_list.Rows.Clear();
            }
            catch (Exception)
            { }
        }

        private void disp_dgvlist()
        {
            thisDatabase db = new thisDatabase();

            DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.soa_period WHERE COALESCE(closed,'')<>'Y' ORDER BY soafrom, soato");

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["soa_desc"].Value = dt.Rows[r]["soa_desc"].ToString();
                    row.Cells["closed"].Value = dt.Rows[r]["closed"].ToString();
                    row.Cells["from"].Value = DateTime.Parse(dt.Rows[r]["soafrom"].ToString()).ToString("yyyy-MM-dd");
                    row.Cells["to"].Value = DateTime.Parse(dt.Rows[r]["soato"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;
                String dtfrm = dgv_list["from", r].Value.ToString();
                String dtto = dgv_list["to", r].Value.ToString();

                txt_desc.Text = dgv_list["soa_desc", r].Value.ToString();
                dtp_frm.Value = Convert.ToDateTime(dtfrm);
                dtp_to.Value = Convert.ToDateTime(dtto);
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void cbo_viewas_SelectedValueChanged(object sender, EventArgs e)
        {
            disp_dgvlist();
        }

        private void cbo_viewas_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgvlist();
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
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

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            txt_desc.Text = dtp_frm.Value.ToString("MMMM dd,yyyy") + " - " + dtp_to.Value.ToString("MMMM dd,yyyy");
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            txt_desc.Text = dtp_frm.Value.ToString("MMMM dd,yyyy") + " - " + dtp_to.Value.ToString("MMMM dd,yyyy");
        }


        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "soa_desc";

            try
            {
                searchValue = searchValue.ToUpper();


                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                        .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                        .First();
                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;

                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[1];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[0];

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
        }
    }
}
