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
    public partial class m_modeofpayment : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        GlobalClass gc;
        GlobalMethod gm;
        thisDatabase db;

        public m_modeofpayment()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new thisDatabase();

            gc.load_account_title(cbo_at_code);
            thisDatabase db2 = new thisDatabase();
            String grp_id = "";
            DataTable dt = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0507' ORDER BY b.pla, b.mod_id");

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

        private void m_modeofpayment_Load(object sender, EventArgs e)
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

        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String code, mp_desc, mp_days, at_code;
            String col = "", val = "", add_col = "", add_val = "", supname = "", address = "", phone = "", mobile = "", fax = "", email = "", tin = "", cntc_name = "", cntc_position = "", cntc_number = "", mop = "", l_subleger = "", iscredit = "";
            String table = "m10";
            Boolean success = false;

            if (String.IsNullOrEmpty(txt_code.Text) || String.IsNullOrEmpty(txt_code.Text) || String.IsNullOrEmpty(txt_mop_days.Text) )
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                code = txt_code.Text;
                mp_desc = txt_name.Text;
                mp_days = txt_mop_days.Text;
                at_code = (cbo_at_code.SelectedValue??"").ToString();
                iscredit = chk_iscredit.Checked ? "Y" : "N";

                if (isnew)
                {
                    code = txt_code.Text;
                    col = "mp_code, mp_desc, mp_days, at_code, iscredit";
                    val = "'" + code + "', '" + mp_desc + "', '" + mp_days + "', '" + at_code + "', '" + iscredit + "'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        success = true;
                    }
                    else
                    {
                        //db.DeleteOnTable("m07", "c_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "mp_code='" + code + "', mp_desc=" + db.str_E(mp_desc) + ", mp_days='" + mp_days + "', at_code='" + at_code + "', iscredit='" + iscredit + "'";

                    if (db.UpdateOnTable(table, col, "mp_code='" + code + "'"))
                    {
                        success = true;
                        //success
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
                    }
                }
                if(success)
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
            chk_iscredit.Checked = false;
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
            DataTable dt = db.get_moplist();

            clear_dgv();
            
            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["mp_code"].Value = dt.Rows[r]["mp_code"].ToString();
                    row.Cells["mp_desc"].Value = dt.Rows[r]["mp_desc"].ToString();
                    row.Cells["mp_days"].Value = dt.Rows[r]["mp_days"].ToString();
                    row.Cells["at_code"].Value = dt.Rows[r]["at_code"].ToString();

                    row.Cells["iscredit"].Value = (dt.Rows[r]["iscredit"].ToString() == "N" ? "N" : "Y");
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); } 
        }

        private void disp_info()
        {
            
            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_code.Text = dgv_list["mp_code", r].Value.ToString();
                txt_name.Text = dgv_list["mp_desc", r].Value.ToString();
                txt_mop_days.Text = dgv_list["mp_days", r].Value.ToString();

                chk_iscredit.Checked = (dgv_list["iscredit", r].Value.ToString() != "N");

                cbo_at_code.SelectedValue = dgv_list["at_code", r].Value.ToString();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

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
            String typname = "mp_desc";

            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "mp_code";
                }
                else
                {
                    typname = "mp_desc";
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
