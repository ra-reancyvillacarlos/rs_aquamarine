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
    public partial class m_requirements : Form
    {
        Boolean seltbp = false; // use for tabbing
        Boolean isnew = false;
        String pKey; // use for updating

        thisDatabase db;

        public m_requirements()
        {
            InitializeComponent();

            db = new thisDatabase();

            disp_list();
        }


        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_desc.Text = "";
        }

        private void tpg_info_enable(Boolean flag)
        {
            txt_code.Enabled = flag;
            txt_desc.Enabled = flag;
        }

        //tabbing

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
        //end of tabbing



        // actions

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            pKey = null; 
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {

            if (dgv_list.Rows.Count > 1 && String.IsNullOrEmpty(dgv_list["id", dgv_list.CurrentRow.Index].Value.ToString()) == false)
            {
                isnew = false;
                pKey = dgv_list["id", dgv_list.CurrentRow.Index].Value.ToString(); 
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
            int r;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                if (db.UpdateOnTable("requirements", "cancel='Y'", "rqrt_code='" + dgv_list["id", r].Value.ToString() + "'"))
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


        private void btn_save_Click(object sender, EventArgs e)
        {
            Boolean success = false;

            String code, desc;
            String col = "", val = "";
            String table = "requirements";

            int r = -1;

            if (String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                code = txt_code.Text;
                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys." + table + " WHERE rqrt_code='" + code + "'");
                if (dt.Rows.Count != 0 && (isnew || pKey != code))
                {
                    if (dt.Rows[0]["cancel"].ToString() != "Y")
                    {
                        success = false;
                        MessageBox.Show("Saving Failed: Code Already existed.");
                    }
                    else
                    {
                        db.DeleteOnTable(table, "rqrt_code='" + code + "'");
                        success = true;
                    }
                }
                else
                {
                    success = true;
                }

                if (success)
                {

                    desc = db.str_E(txt_desc.Text);

                    if (isnew)
                    {
                        col = "rqrt_code,rqrt_desc,cancel";
                        val = "'"+code+"',"+desc+",''";

                        if (db.InsertOnTable(table, col, val))
                        {
                            success = true;
                        }
                    }
                    else
                    {
                        r = dgv_list.CurrentRow.Index;

                        col = "rqrt_code='" + code + "',rqrt_desc=" + desc + "";
                        try
                        {
                            if (db.UpdateOnTable(table, col, "rqrt_code='" + pKey + "'"))
                            {
                                success = true;
                            }
                            else
                            {
                                success = false;
                                MessageBox.Show("Failed on saving.");
                            }
                        }
                        catch (Exception err) { MessageBox.Show(err.Message); }

                    }
                }

                if (success)
                {
                    disp_list();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                    frm_clear();
                    if (r != -1) // para sa update rani
                    {
                        dgv_list.ClearSelection();
                        dgv_list.Rows[r].Selected = true;
                    }
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        //end of actions


        //display

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

            DataTable dt = db.QueryBySQLCode("SELECT r.* FROM rssys.requirements r WHERE r.cancel<>'Y' ORDER BY r.rqrt_code ASC");

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["id"].Value = dt.Rows[r]["rqrt_code"].ToString();
                    row.Cells["desc"].Value = dt.Rows[r]["rqrt_desc"].ToString();
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
                txt_desc.Text = dgv_list["desc", r].Value.ToString();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4031);
            rpt.ShowDialog();
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
