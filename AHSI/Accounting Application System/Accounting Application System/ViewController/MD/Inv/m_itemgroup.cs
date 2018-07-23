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
    public partial class m_itemgroup : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;

        private String itmgrp_code = "";

        public m_itemgroup()
        {
            GlobalClass gc = new GlobalClass();

            InitializeComponent();

            gc.load_account_title(cbo_stks);
            gc.load_account_title(cbo_cost);
            gc.load_account_title(cbo_sales);
            gc.load_outlet_type(cbo_grptype);
            thisDatabase db2 = new thisDatabase();
            String grp_id = "";
            DataTable dt = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0502' ORDER BY b.pla, b.mod_id");

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

        private void m_itemgroup_Load(object sender, EventArgs e)
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
            cbo_sales.SelectedIndex = -1;
            cbo_stks.SelectedIndex = -1;
            cbo_cost.SelectedIndex = -1;
            cbo_grptype.SelectedIndex = -1;

            itmgrp_code = "";
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
            cbo_sales.Enabled = flag;
            cbo_stks.Enabled = flag;
            cbo_cost.Enabled = flag;
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
            try
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
            catch
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

                if (db.UpdateOnTable("itmgrp", "cancel='Y'", "item_grp='" + dgv_list["ID", r].Value.ToString() + "'"))
                {
                    disp_dgvlist();
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
            rpt.print_mdata(4022);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            Boolean success = false;
            String code, name, next_num, grptype="", asales = null, acost = null, astocks = null;
            String l_subleger = null;
            String col = "", val = "", add_col = "", add_val = "";

            if (String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else if (String.IsNullOrEmpty(txt_name.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else if (cbo_grptype.SelectedIndex == -1)
            {
                MessageBox.Show("Group Type is required");
            }
            else
            {
                code = txt_code.Text;
                name = txt_name.Text;
                next_num = txt_next_num.Text;               

                if(cbo_grptype.SelectedIndex != -1)
                {
                    grptype = cbo_grptype.SelectedValue.ToString();
                }
                if (cbo_sales.SelectedIndex > -1)
                {
                    asales = cbo_sales.SelectedValue.ToString();
                }
                if (cbo_cost.SelectedIndex > -1)
                {
                    acost = cbo_cost.SelectedValue.ToString();
                }
                if (cbo_stks.SelectedIndex > -1)
                {
                    astocks = cbo_stks.SelectedValue.ToString();
                }

                if (isnew)
                {
                    //code = db.get_pk("item_grp");
                    col = "item_grp, grp_desc, next_num, grptype, acct_stks, acct_sales, acct_cost";
                    val = "'" + code + "', " + db.str_E(name) + ", '" + next_num + "', '" + grptype+ "', '" + astocks + "', '" + asales + "', '" + acost + "'";

                    if (db.InsertOnTable("itmgrp", col, val))
                    {
                        success = true;
                       // db.set_pkm99("item_grp", db.get_nextincrementlimitchar(code, 5));
                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable("itmgrp", "item_grp='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(db.get_colval("itmgrp", "item_grp", "item_grp='" + code + "'")) || code == itmgrp_code)
                    {

                        col = "item_grp='" + code + "',grp_desc=" + db.str_E(name) + ", next_num='" + next_num + "', grptype='" + grptype + "', acct_stks='" + astocks + "', acct_sales='" + asales + "', acct_cost='" + acost + "'";

                        if (db.UpdateOnTable("itmgrp", col, "item_grp='" + itmgrp_code + "'"))
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
                        success = false;
                        MessageBox.Show("Item Group Code already exist.");
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

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tpg_info_enable(true);
            goto_tbcntrl_list();
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
            DataTable dt = db.get_itemgrp_withdesc_list();

            clear_dgv();

            try
            {
                dgv_list.DataSource = dt;

            }
            catch (Exception) { }
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;
                itmgrp_code = dgv_list["id", r].Value.ToString();

                txt_code.Text = itmgrp_code;
                txt_name.Text = dgv_list["name", r].Value.ToString();
                txt_next_num.Text = dgv_list["next_num", r].Value.ToString();
                String grptype = dgv_list["grptype", r].Value.ToString();

                if (String.IsNullOrEmpty(grptype))
                    cbo_grptype.SelectedIndex = -1;
                else
                    cbo_grptype.SelectedValue = grptype;

                if (String.IsNullOrEmpty(dgv_list["acct_stks", r].Value.ToString()) == false)
                    cbo_stks.SelectedValue = dgv_list["acct_stks", r].Value.ToString();
                else
                    cbo_stks.SelectedIndex = -1;
                
                if (String.IsNullOrEmpty(dgv_list["acct_sales", r].Value.ToString()) == false)
                    cbo_sales.SelectedValue = dgv_list["acct_sales", r].Value.ToString();
                else
                    cbo_sales.SelectedIndex = -1;

                if (String.IsNullOrEmpty(dgv_list["acct_cost", r].Value.ToString()) == false)
                    cbo_cost.SelectedValue = dgv_list["acct_cost", r].Value.ToString();
                else
                    cbo_cost.SelectedIndex = -1;
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
    }
}
