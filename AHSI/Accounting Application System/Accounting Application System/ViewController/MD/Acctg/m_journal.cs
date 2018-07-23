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
    public partial class m_journal : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;

        private GlobalClass gc = new GlobalClass();

        public m_journal()
        {
            InitializeComponent();

            gc.load_journaltype(cbo_jrnltype);
            thisDatabase db = new thisDatabase();
            String grp_id = "";
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0107' ORDER BY b.pla, b.mod_id");

            if (dt2.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt2.Rows[0]["add"].ToString();
                update = dt2.Rows[0]["upd"].ToString();
                delete = dt2.Rows[0]["cancel"].ToString();
                print = dt2.Rows[0]["print"].ToString();

                if (add == "n")
                {
                    btn_additem.Enabled = false;
                }
                if (update == "n")
                {
                    btn_upditem.Enabled = false;
                }
                if (delete == "n")
                {
                    btn_delitem.Enabled = false;
                }
                if (print == "n")
                {
                    btn_print.Enabled = false;
                }

            }
            disp_dgvlist();
        }

        private void m_journal_Load(object sender, EventArgs e)
        {

        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            isnew = false;
            tpg_info_enable(true);
            txt_code.Enabled = false;
            frm_clear();
            disp_info();
            goto_tbcntrl_info();
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently Disabled");

            /*thisDatabase db = new thisDatabase();
            int r;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                //if (db.UpdateOnTable("m07", "cancel='Y'", "c_code='" + dgv_list["ID", r].Value.ToString() + "'"))
                //{
                    disp_dgvlist();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                //}
                //else
                //{
                 //   MessageBox.Show("Failed on deleting.");
                //}
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }*/
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4007);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String code, j_desc, j_num, check_by, noted_by, approv_by, j_type;
            String col = "", val = "", add_col = "", add_val = "";
            String table = "m05";


            if (String.IsNullOrEmpty(txt_code.Text) || String.IsNullOrEmpty(txt_j_desc.Text) || String.IsNullOrEmpty(txt_j_num.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else if (cbo_jrnltype.SelectedIndex == -1)
            {
                MessageBox.Show("Pls select Journal Type.");
                cbo_jrnltype.DroppedDown = true;
            }
            else if (txt_code.Text.Length > 4)
            {
                MessageBox.Show("Journal ID must be 4 characters.");
            }
            else
            {
                code = txt_code.Text;
                j_desc = txt_j_desc.Text;
                j_num = txt_j_num.Text;
                check_by = txt_check_by.Text;
                noted_by = txt_noted_by.Text;
                approv_by = txt_approv_by.Text;

                j_type = cbo_jrnltype.SelectedValue.ToString();

                if (isnew)
                {
                    //code = db.get_pk("c_code");
                    col = "j_code, j_desc, j_num, check_by, noted_by, approv_by,j_type";
                    val = "'" + code + "', '" + j_desc + "', '" + j_num + "', '" + check_by + "', '" + noted_by + "', '" + approv_by + "', '" + j_type + "'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        // db.set_pkm99("c_code", db.get_nextincrementlimitchar(code, 6));
                    }
                    else
                    {
                        db.DeleteOnTable(table, "j_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "j_code='" + code + "', j_desc='" + j_desc + "', j_num='" + j_num + "', check_by='" + check_by + "', noted_by='" + noted_by + "', approv_by='" + approv_by + "', j_type='" + j_type + "'";

                    if (db.UpdateOnTable(table, col, "j_code='" + code + "'"))
                    {
                        //success
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
                    }
                }
                disp_dgvlist();
                goto_tbcntrl_list();
                tpg_info_enable(false);
                frm_clear();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_j_desc.Text = "";
            txt_j_num.Text = "";
            txt_check_by.Text = "";
            txt_approv_by.Text = "";
            txt_noted_by.Text = "";
            cbo_jrnltype.SelectedIndex = -1;
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
            txt_code.Enabled = flag;
            txt_j_desc.Enabled = flag;
            txt_j_num.Enabled = flag;
            txt_check_by.Enabled = flag;
            txt_approv_by.Enabled = flag;
            txt_noted_by.Enabled = flag;
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
            DataTable dt = db.get_journallist();

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["j_code"].Value = dt.Rows[r]["j_code"].ToString();
                    row.Cells["j_desc"].Value = dt.Rows[r]["j_desc"].ToString();
                    row.Cells["j_num"].Value = dt.Rows[r]["j_num"].ToString();
                    row.Cells["check_by"].Value = dt.Rows[r]["check_by"].ToString();
                    row.Cells["noted_by"].Value = dt.Rows[r]["noted_by"].ToString();
                    row.Cells["approv_by"].Value = dt.Rows[r]["approv_by"].ToString();
                    row.Cells["j_type"].Value = dt.Rows[r]["j_type"].ToString();
                    cbo_jrnltype.SelectedValue = dt.Rows[r]["j_type"].ToString();
                    row.Cells["j_type_desc"].Value = cbo_jrnltype.Text.ToUpper();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); } 
        }

        private void disp_info()
        {            
            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_code.Text = dgv_list["j_code", r].Value.ToString();
                txt_j_desc.Text = dgv_list["j_desc", r].Value.ToString();
                txt_j_num.Text = dgv_list["j_num", r].Value.ToString();
                txt_check_by.Text = dgv_list["check_by", r].Value.ToString();
                txt_noted_by.Text = dgv_list["noted_by", r].Value.ToString();
                txt_approv_by.Text = dgv_list["approv_by", r].Value.ToString();
                cbo_jrnltype.SelectedValue = dgv_list["j_type", r].Value.ToString();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
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
            String typname = "j_desc";

            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "j_code";
                }
                else
                {
                    typname = "j_desc";
                }


                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                        .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                        .First();
                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;

                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[2];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[1];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[0];

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
        }
    }
}
