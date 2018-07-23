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
    public partial class m_accountingperiods : Form
    {
        dbAcctg db;
        Boolean seltbp = false;
        Boolean isnew = false;

        public m_accountingperiods()
        {
            db = new dbAcctg();
            GlobalClass gc = new GlobalClass();

            InitializeComponent();

            try
            {
                gc.load_accountingperiod(cbo_month);

                cbo_viewas.SelectedIndex = 1;

                txt_fy.Text = db.get_m99fy();

                disp_dgvlist();
            }
            catch (Exception) { }
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
            String table = "x03";
            try
            {
                int r = dgv_list.CurrentRow.Index;
                String fy = dgv_list["fy", r].Value.ToString();
                String mo = dgv_list["mo", r].Value.ToString();

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to close this period?", "Confirm", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    if (db.UpdateOnTable(table, "closed='Y'", "fy='" + fy + "' AND mo='" + mo + "'"))
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
            String fy, month_desc, mo, dtfrm, dtto;
            String col = "", val = "", add_col = "", add_val = "";
            String table = "x03";
            Boolean success = false;
            int _mo = 0;

            if (String.IsNullOrEmpty(txt_fy.Text) == true || cbo_month.SelectedIndex == -1)
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                fy = txt_fy.Text.ToString();
                try { _mo = Convert.ToInt32(cbo_month.SelectedValue.ToString()); }
                catch { }
                dtfrm = dtp_frm.Value.ToString("yyyy-MM-dd");
                dtto = dtp_to.Value.ToString("yyyy-MM-dd");
                month_desc = cbo_month.Text.ToString();
                mo = (_mo == 0 ? _mo : (_mo == 10 ? 12 : (_mo + 2) % 12)).ToString();

                if (isnew)
                {
                    col = "fy, mo, \"from\", \"to\", month_desc";
                    val = "'" + fy + "', '" + mo + "', '" + dtfrm + "', '" + dtto + "', '" + month_desc + "'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        //db.set_pkm99("c_code", db.get_nextincrementlimitchar(code, 6));
                        success = true;
                    }
                    else
                    {
                        db.DeleteOnTable(table, "fy='" + fy + "' AND mo='" + mo + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "fy='" + fy + "', mo='" + mo + "', \"from\"='" + from + "', \"to\"='" + to + "', month_desc='" + month_desc + "'";

                    if (db.UpdateOnTable(table, col, "fy='" + fy + "' AND mo='" + mo + "'"))
                    {
                        //success
                        success = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
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
            txt_fy.Text = "";
            txt_mo.Text = "";
            cbo_month.SelectedIndex = -1;
            dtp_frm.Value = DateTime.Today;
            dtp_to.Value = DateTime.Today;
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
            txt_fy.Enabled = flag;
            txt_mo.Enabled = flag;
            cbo_month.Enabled = flag;

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

            DataTable dt = db.get_accountingperiodlist(get_statusdisplay());

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["fy"].Value = dt.Rows[r]["fy"].ToString();
                    row.Cells["month_desc"].Value = dt.Rows[r]["month_desc"].ToString();
                    row.Cells["closed"].Value = dt.Rows[r]["closed"].ToString();
                    row.Cells["from"].Value = dt.Rows[r]["from"].ToString();
                    row.Cells["to"].Value = dt.Rows[r]["to"].ToString();
                    row.Cells["mo"].Value = dt.Rows[r]["mo"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;
                String fy = dgv_list["fy", r].Value.ToString();
                String mo = dgv_list["mo", r].Value.ToString();
                String dtfrm = dgv_list["from", r].Value.ToString();
                String dtto = dgv_list["to", r].Value.ToString();

                txt_fy.Text = fy; //(Convert.ToInt32(mo) - 2 + 12) % 12;
                cbo_month.Text = dgv_list["month_desc", r].Value.ToString();
                if (cbo_month.SelectedIndex == -1)
                    cbo_month.SelectedValue = 0;
                dtp_frm.Value = Convert.ToDateTime(dtfrm);
                dtp_to.Value = Convert.ToDateTime(dtto);
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void cbo_viewas_SelectedValueChanged(object sender, EventArgs e)
        {
            disp_dgvlist();
        }

        private void cbo_monthperiod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_month.SelectedIndex != -1)
                {
                    if (isnew == true && String.IsNullOrEmpty(txt_fy.Text))
                    {
                        MessageBox.Show("Please type the YEAR first.");
                    }
                    else
                    {
                        int fy = Convert.ToInt32(txt_fy.Text);
                        int mo = Convert.ToInt32(cbo_month.SelectedValue.ToString());
                        txt_mo.Text = "0";

                        dtp_frm.Value = Convert.ToDateTime(fy.ToString() + "-" + mo.ToString("00") + "-01");
                        dtp_to.Value = Convert.ToDateTime(fy.ToString() + "-" + mo.ToString("00") + "-" + DateTime.DaysInMonth(fy, mo).ToString("00"));
                        txt_mo.Text = (mo == 10 ? 12 : (mo + 2) % 12).ToString();

                    }
                }
            }
            catch (Exception) { }
        }

        private void cbo_viewas_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgvlist();
        }

        private String get_statusdisplay()
        {
            if (cbo_viewas.SelectedIndex == 1)
            {
                return "O";
            }
            else if (cbo_viewas.SelectedIndex == 2)
            {
                return "C";
            }
            else
            {
                return "";
            }
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

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "month_desc";
            
            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "fy";
                }
                else
                {
                    typname = "month_desc";
                }

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
