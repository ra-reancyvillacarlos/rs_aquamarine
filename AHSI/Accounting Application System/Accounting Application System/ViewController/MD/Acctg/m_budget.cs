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
    public partial class m_budget : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalMethod gm = new GlobalMethod();
        GlobalClass gc = new GlobalClass();
        Boolean seltbp = false;
        Boolean isnew = false;
        String updkey = "";

        public m_budget()
        {
            InitializeComponent();

            try
            {
                gc.load_accountingperiod(cbo_mo);
                gc.load_account_title(cbo_acct);
            }
            catch { }

            disp_list();
        }

        private void m_brand_Load(object sender, EventArgs e)
        {
            tpg_info_enable(false);
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_amount.Text = "0.00";
            txt_mo.Text = "";
            txt_fy.Text = "";
            cbo_acct.SelectedIndex = -1;
            cbo_mo.SelectedIndex = -1;
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
            txt_amount.Enabled = flag;
            txt_mo.Enabled = flag;
            txt_fy.Enabled = flag;
            cbo_acct.Enabled = flag;
            cbo_mo.Enabled = flag;
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

        private void disp_list()
        {
            int mo = 0;
            DataTable dt = db.QueryBySQLCode("SELECT b.*, m4.at_desc, x3.month_desc FROM rssys.budget b LEFT JOIN rssys.m04 m4 ON m4.at_code=b.at_code LEFT JOIN rssys.x03 x3 ON b.mo=x3.mo AND b.fy=x3.mo ORDER BY fy, mo");

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["fy"].Value = dt.Rows[r]["fy"].ToString();
                    row.Cells["mo"].Value = dt.Rows[r]["mo"].ToString();
                    row.Cells["mo_desc"].Value = dt.Rows[r]["month_desc"].ToString();
                    if (String.IsNullOrEmpty(row.Cells["mo_desc"].Value.ToString()))
                    {
                        mo = 0;
                        try
                        {
                            mo = Convert.ToInt32(row.Cells["mo"].Value.ToString()); ;
                            if (mo != 0)
                            {
                                mo = (mo - 2 + 12) % 12;
                                mo = (mo == 0 ? 12 : mo);
                            }
                            cbo_mo.SelectedValue = (mo).ToString();
                        }
                        catch { }
                        row.Cells["mo_desc"].Value = cbo_mo.Text.ToString();
                    }
                    row.Cells["acct_no"].Value = dt.Rows[r]["at_code"].ToString();
                    row.Cells["acct_desc"].Value = dt.Rows[r]["at_desc"].ToString();
                    row.Cells["amount"].Value = gm.toAccountingFormat(dt.Rows[r]["budget"].ToString());
                }
            }
            catch (Exception) { }
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_fy.Text = dgv_list["fy", r].Value.ToString();
                txt_mo.Text = dgv_list["mo", r].Value.ToString();
                try
                {
                    int mo = Convert.ToInt32(txt_mo.Text);
                    if (mo != 0)
                    {
                        mo = (mo - 2 + 12) % 12;
                        mo = (mo == 0 ? 12 : mo);
                    }
                    cbo_mo.SelectedValue = (mo).ToString();
                }
                catch { }
                cbo_acct.SelectedValue = dgv_list["acct_no", r].Value.ToString();
                txt_amount.Text = dgv_list["amount", r].Value.ToString();

                updkey = "fy='" + txt_fy.Text + "' AND mo='" + txt_mo.Text + "' AND at_code='" + cbo_acct.SelectedValue.ToString() + "'";
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            tpg_info_enable(true);
            frm_clear();

            txt_fy.Text = db.get_m99fy();
            goto_tbcntrl_info();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            if (dgv_list.Rows.Count > 0 && String.IsNullOrEmpty((dgv_list["fy", dgv_list.CurrentRow.Index].Value).ToString()) == false)
            {
                isnew = false;
                tpg_info_enable(true);
                frm_clear();
                disp_info();
                goto_tbcntrl_info();
            }
            else
            {
                MessageBox.Show("No rows selected");
            }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            int r;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;
                /*
                if (db.UpdateOnTable("buget", "cancel='Y'", "brd_code='" + dgv_list["ID", r].Value.ToString() + "'"))
                {
                    disp_list();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                }
                else
                {
                    MessageBox.Show("Failed on deleting.");
                }*/
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            //Report rpt = new Report();
            //rpt.print_mdata(4013);
            //rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            Boolean success = false;
            String fy, mo, amount, at_code;
            String col = "", val = "", add_col = "", add_val = "";

            if (String.IsNullOrEmpty(txt_amount.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else if(txt_fy.Text.Length > 8)
            {
                MessageBox.Show("Code must be at least 8 characters.");
            }
            else if (cbo_mo.SelectedIndex == -1)
            {
                MessageBox.Show("Pls select the month fields.");
                cbo_mo.DroppedDown = true;
            }
            else if (cbo_acct.SelectedIndex == -1)
            {
                MessageBox.Show("Pls select the accout title fields.");
                cbo_acct.DroppedDown = true;
            }
            else
            {
                fy = txt_fy.Text;
                mo = txt_mo.Text;
                amount = gm.toNormalDoubleFormat(txt_amount.Text).ToString("0.00");
                at_code = cbo_acct.SelectedValue.ToString();

                if (isnew)
                {
                    success = String.IsNullOrEmpty(db.get_colval("budget", "fy", "fy='" + fy + "' AND mo='" + mo + "' AND at_code='" + at_code + "'"));
                    if (success)
                    {
                        col = "fy, mo, budget, at_code";
                        val = "'" + fy + "', '" + mo + "', '" + amount + "', '" + at_code + "'";
                        if (db.InsertOnTable("budget", col, val))
                        {
                            success = true;
                            //db.set_pkm99("brd_code", db.get_nextincrementlimitchar(code, 3));
                        }
                        else
                        {
                            success = false;
                            //db.DeleteOnTable("budget", "brd_code='" + code + "'");
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Input fields are already exist.");
                    }
                }
                else
                {
                    col = "fy='" + fy + "' AND mo='" + mo + "' AND at_code='" + at_code + "'"; 
                    success = String.IsNullOrEmpty(db.get_colval("budget", "fy", col));
                    if (success || updkey == col)
                    {
                        col = "fy='" + fy + "', mo='" + mo + "', budget='" + amount + "', at_code='" + at_code + "'";

                        if (db.UpdateOnTable("budget", col, updkey))
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Input fields are already exist.");
                    }
                }

                if (success)
                {
                    MessageBox.Show("Successfully saved.");
                    disp_list();
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
            frm_clear();
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tpg_info_enable(true);
            goto_tbcntrl_list();
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

        private void txt_amount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_EnterTextNumber(object sender, System.EventArgs e)
        {
            TextBox txt_num = (TextBox)sender;
            if (txt_num.Text != String.Empty)
            {
                txt_num.Text = gm.toNormalDoubleFormat(txt_num.Text).ToString("0.00");
                txt_num.Select(txt_num.Text.Length, 0);
            }
        }
        private void txt_LeaveTextNumber(object sender, System.EventArgs e)
        {
            TextBox txt_num = (TextBox)sender;
            if (txt_num.Text != String.Empty)
            {
                txt_num.Text = gm.toAccountingFormat(txt_num.Text);
                txt_num.Select(0, 0);
            }
        }

        private void tpg_info_Click(object sender, EventArgs e)
        {

        }

        private void cbo_mo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_mo.SelectedIndex != -1)
                {
                    int mo = Convert.ToInt32(cbo_mo.SelectedValue.ToString());
                    txt_mo.Text = "0";
                    txt_mo.Text = (mo == 10 ? 12 : (mo + 2) % 12).ToString();
                }
            }
            catch (Exception) { }
        }

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "mo_desc";

            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "fy";
                }
                else
                {
                    typname = "mo_desc";
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
