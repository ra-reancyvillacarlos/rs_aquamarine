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
    public partial class m_itemunit : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;

        public m_itemunit()
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();

            gc.load_unit_with_desc(cbo_eq1);
            gc.load_unit_with_desc(cbo_eq2);
            gc.load_unit_with_desc(cbo_eq3);
            gc.load_unit_with_desc(cbo_eq4);
            gc.load_unit_with_desc(cbo_eq5);
            thisDatabase db2 = new thisDatabase();
            String grp_id = "";
            DataTable dt = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0503' ORDER BY b.pla, b.mod_id");

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

        private void m_itemunit_Load(object sender, EventArgs e)
        {

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
                isnew = false;
                tpg_info_enable(true);
                frm_clear();
                disp_info();
                goto_tbcntrl_info();
            }
            catch(Exception)
            {
                MessageBox.Show("No rows selected");
            }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            int r;

            MessageBox.Show("This action is disabled.");
            /*
            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                if (db.UpdateOnTable("itmunit", "cancel='Y'", "unit_id='" + dgv_list["ID", r].Value.ToString() + "'"))
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
            } */
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4023);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            Boolean success = false;
            String code, desc, unit_desc, eunit1 = null, eunit2 = null, eunit3 = null, eunit4 = null, eunit5 = null;
            Double eval1 = 0.00, eval2 = 0.00, eval3 = 0.00, eval4 = 0.00, eval5 = 0.00;
            String col = "", val = "", add_col = "", add_val = "";

            if (String.IsNullOrEmpty(txt_name.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                code = txt_code.Text;
                desc = txt_name.Text;
                unit_desc = txt_unit_desc.Text;
                eval1 = gm.toNormalDoubleFormat(txt_eq1.Text);
                eval2 = gm.toNormalDoubleFormat(txt_eq2.Text);
                eval3 = gm.toNormalDoubleFormat(txt_eq3.Text);
                eval4 = gm.toNormalDoubleFormat(txt_eq4.Text);
                eval5 = gm.toNormalDoubleFormat(txt_eq5.Text);                

                if (isnew)
                {
                    //code = txt_code.Text;//db.get_pk("brd_code");
                    code = db.get_pk("unit_id");
                    col = "unit_id, unit_shortcode, unit_desc";
                    val = "'" + code + "', '" + desc + "', '" + unit_desc + "'";

                    if (db.InsertOnTable("itmunit", col, val))
                    {
                        success = true;
                        db.set_pkm99("unit_id", db.get_nextincrementlimitchar(code, code.Length));
                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable("itmunit", "unit_id='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "unit_id='" + code + "', unit_shortcode='" + desc + "', unit_desc='" + unit_desc + "'";

                    if (db.UpdateOnTable("itmunit", col, "unit_id='" + code + "'"))
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }

                //delete all equivalents
                db.DeleteOnTable("itmunitcnvrsn", "unit_id1='" + code + "'");

                if (cbo_eq1.SelectedIndex != -1)
                {
                    eunit1 = cbo_eq1.SelectedValue.ToString();

                    InsertUpdate_equivalent(code, eunit1, eval1, 1);
                }
                if (cbo_eq2.SelectedIndex != -1)
                {
                    eunit2 = cbo_eq2.SelectedValue.ToString();

                    InsertUpdate_equivalent(code, eunit2, eval2, 2);
                }
                if (cbo_eq3.SelectedIndex != -1)
                {
                    eunit3 = cbo_eq3.SelectedValue.ToString();

                    InsertUpdate_equivalent(code, eunit3, eval3, 3);
                }
                if (cbo_eq4.SelectedIndex != -1)
                {
                    eunit4 = cbo_eq4.SelectedValue.ToString();

                    InsertUpdate_equivalent(code, eunit4, eval4, 4);
                }
                if (cbo_eq5.SelectedIndex != -1)
                {
                    eunit5 = cbo_eq5.SelectedValue.ToString();

                    InsertUpdate_equivalent(code, eunit5, eval5, 5);
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

        private void InsertUpdate_equivalent(String unit_id1, String unit_id2, Double equivalent, int lnno)
        {
            thisDatabase db = new thisDatabase();
            String table = "itmunitcnvrsn";

            if(db.isExists(table, "unit_id1='" + unit_id1 + "' AND unit_id2='" + unit_id2 + "'"))
            {
                db.UpdateOnTable(table, "lnno='" + lnno + "', equivalent='" + equivalent + "'", "unit_id1='" + unit_id1 + "' AND unit_id2='" + unit_id2 + "'");
            }
            else
            {
                db.InsertOnTable(table, "unit_id1, unit_id2, lnno, equivalent", "'" + unit_id1 + "', '" + unit_id2 + "', '" + lnno + "', '" + equivalent + "'");
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

        private void disp_list()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_itemunitlist();

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["id"].Value = dt.Rows[r]["unit_id"].ToString();
                    row.Cells["name"].Value = dt.Rows[r]["unit_shortcode"].ToString();
                    row.Cells["unit_desc"].Value = dt.Rows[r]["unit_desc"].ToString();
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
                txt_unit_desc.Text = dgv_list["unit_desc", r].Value.ToString();
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
