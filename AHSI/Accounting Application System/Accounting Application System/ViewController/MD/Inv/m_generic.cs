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
    public partial class m_generic : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;

        public m_generic()
        {
            InitializeComponent();
            thisDatabase db2 = new thisDatabase();
            String grp_id = "";
            DataTable dt = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0304' ORDER BY b.pla, b.mod_id");

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
            disp_list();
        }

        private void m_generic_Load(object sender, EventArgs e)
        {
            tpg_info_enable(false);
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_name.Text = "";
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
            txt_name.Enabled = flag;
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
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_generic_list();

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["id"].Value = dt.Rows[r]["gen_code"].ToString();
                    row.Cells["name"].Value = dt.Rows[r]["gen_name"].ToString();
                }
            }
            catch (Exception) { }
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_code.Text = dgv_list["id", r].Value.ToString();
                txt_name.Text = dgv_list["name", r].Value.ToString();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
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
            if (dgv_list.Rows.Count > 0 && String.IsNullOrEmpty(dgv_list["ID", dgv_list.CurrentRow.Index].Value.ToString()) == false)
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

                if (db.UpdateOnTable("brand", "cancel='Y'", "brd_code='" + dgv_list["ID", r].Value.ToString() + "'"))
                {
                    disp_list();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                }
                else
                {
                    MessageBox.Show("Failed on deleting.");
                }
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4013);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            Boolean success = false;
            String code, desc;
            String col = "", val = "", add_col = "", add_val = "";

            if (String.IsNullOrEmpty(txt_name.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else if (txt_code.Text.Length > 8)
            {
                MessageBox.Show("Code must be at least 8 characters.");
            }
            else
            {
                code = txt_code.Text;
                desc = txt_name.Text;

                if (isnew)
                {
                    code = txt_code.Text;//db.get_pk("brd_code");
                    col = "gen_code, gen_name";
                    val = "'" + code + "', '" + desc + "'";

                    if (db.InsertOnTable("generic", col, val))
                    {
                        success = true;
                        //db.set_pkm99("brd_code", db.get_nextincrementlimitchar(code, 3));
                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable("generic", "gen_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "gen_code='" + code + "', gen_name='" + desc + "'";

                    if (db.UpdateOnTable("brand", col, "gen_code='" + code + "'"))
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }

                if (success)
                {
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
    }
}
