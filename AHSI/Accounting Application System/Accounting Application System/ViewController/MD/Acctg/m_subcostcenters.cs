﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    public partial class m_subcostcenters : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;

        public m_subcostcenters()
        {
            GlobalClass gc = new GlobalClass();

            InitializeComponent();

            gc.load_costcenter(cbo_cc_code);
            thisDatabase db = new thisDatabase();
            String grp_id = "";
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0111' ORDER BY b.pla, b.mod_id");

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

        private void m_subcostcenters_Load(object sender, EventArgs e)
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
            rpt.print_mdata(4011);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String code, scc_desc, cc_code;
            String col = "", val = "", add_col = "", add_val = "";
            String table = "subctr";
            Boolean success = false;

            if (String.IsNullOrEmpty(txt_code.Text) || String.IsNullOrEmpty(txt_scc_desc.Text) || cbo_cc_code.SelectedIndex == -1)
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else if(txt_code.Text.Length > 4)
            {
                MessageBox.Show("Code must 4 characters.");
            }
            else
            {
                code = txt_code.Text;
                scc_desc = txt_scc_desc.Text;
                cc_code = cbo_cc_code.SelectedValue.ToString();

                if (isnew)
                {
                    //code = db.get_pk("c_code");
                    col = "scc_code, scc_desc, cc_code";
                    val = "'" + code + "', '" + scc_desc + "', '" + cc_code + "'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        //db.set_pkm99("c_code", db.get_nextincrementlimitchar(code, 6));
                        success = true;
                    }
                    else
                    {
                        db.DeleteOnTable(table, "c_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "scc_code='" + code + "', scc_desc='" + scc_desc + "', cc_code='" + cc_code + "'";

                    if (db.UpdateOnTable(table, col, "scc_code='" + code + "'"))
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
            frm_clear();
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_scc_desc.Text = "";

            cbo_cc_code.SelectedIndex = -1;
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
            txt_scc_desc.Enabled = flag;

            cbo_cc_code.Enabled = flag;
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
            DataTable dt = db.get_subcostcenterlist();

            clear_dgv();
            
            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["scc_code"].Value = dt.Rows[r]["scc_code"].ToString();
                    row.Cells["scc_desc"].Value = dt.Rows[r]["scc_desc"].ToString();
                    row.Cells["cc_code"].Value = dt.Rows[r]["cc_code"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); } 
        }

        private void disp_info()
        {
            
            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_code.Text = dgv_list["scc_code", r].Value.ToString();
                txt_scc_desc.Text = dgv_list["scc_desc", r].Value.ToString();
                cbo_cc_code.SelectedValue = dgv_list["cc_code", r].Value.ToString();
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
            String typname = "scc_desc";
            



            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "scc_code";
                }
                else if (cbo_searchby.SelectedIndex == 2)
                {
                    typname = "cc_code";
                }
                else
                {
                    typname = "scc_desc";
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
