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
    public partial class m_vat : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        GlobalClass gc = new GlobalClass();

        public m_vat()
        {
            InitializeComponent();
            thisDatabase db2 = new thisDatabase();
            String grp_id2 = "";
            DataTable dt24 = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt24.Rows.Count > 0)
            {
                grp_id2 = dt24.Rows[0]["grp_id"].ToString();
            }
            DataTable dt23 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id2 + "' AND a.mod_id='M0511' ORDER BY b.pla, b.mod_id");

            if (dt23.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt23.Rows[0]["add"].ToString();
                update = dt23.Rows[0]["upd"].ToString();
                delete = dt23.Rows[0]["cancel"].ToString();
                print = dt23.Rows[0]["print"].ToString();

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

        private void m_vat_Load(object sender, EventArgs e)
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
            cbo_type.SelectedIndex = -1;
            cbo_salesvat.SelectedIndex = -1;
            cbo_purvat.SelectedIndex = -1;
            txt_pct.Text = "";
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
            cbo_type.Enabled = flag;
            cbo_salesvat.Enabled = flag;
            cbo_purvat.Enabled = flag;
            txt_pct.Enabled = flag;
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
            thisDatabase db = new thisDatabase();
            int r;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                if (db.UpdateOnTable("vat", "cancel='Y'", "vat_code='" + dgv_list["ID", r].Value.ToString() + "'"))
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
            rpt.print_mdata(4025);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            Boolean success = false;
            String code, desc, pct = "0.00", vsales = "", vpurc = "", vtype = "";
            String col = "", val = "", add_col = "", add_val = "";

            try
            {
                if (String.IsNullOrEmpty(txt_pct.Text))
                {
                    MessageBox.Show("Pls enter the required fields.");
                }
                else if (String.IsNullOrEmpty(txt_name.Text) || Convert.ToDouble(txt_pct.Text) <= 0.00 || cbo_type.SelectedIndex == -1)
                {
                    MessageBox.Show("Pls enter the required fields.");
                }
                else
                {
                    code = txt_code.Text;
                    desc = txt_name.Text;
                    pct = Convert.ToDouble(txt_pct.Text).ToString("0.00");

                    if (!string.IsNullOrEmpty(cbo_type.Text))
                    {
                        vtype = cbo_type.Text.ToString().Substring(0,1);

                        switch(vtype)
                        {
                            case "N":
                                vtype = "X";
                                break;
                            case "E":
                                vtype = "E";
                                break;
                            case "I":
                                vtype = "I";
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(cbo_salesvat.Text))
                    {
                        vsales = cbo_salesvat.SelectedValue.ToString();
                    }
                    if (!string.IsNullOrEmpty(cbo_purvat.Text))
                    {
                        vpurc = cbo_purvat.SelectedValue.ToString();
                    }

                    if (isnew)
                    {
                        //code = db.get_pk("vat_code");
                        col = "vat_code, vat_desc, vat_type, vat_sales, vat_purch, vat_pct";
                        val = "'" + code + "', '" + desc + "', '" + vtype + "', '" + vsales + "', '" + vpurc + "', '" + pct + "'";

                        if (db.InsertOnTable("vat", col, val))
                        {
                            success = true;
                            //db.set_pkm99("vat_code", db.get_nextincrementlimitchar(code, 3));
                        }
                        else
                        {
                            success = false;
                            db.DeleteOnTable("vat", "vat_code='" + code + "'");
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        col = "vat_code='" + code + "', vat_desc='" + desc + "', vat_type='" + vtype + "', vat_sales='" + vsales + "', vat_purch='" + vpurc + "', vat_pct='" + pct + "'";

                        if (db.UpdateOnTable("vat", col, "vat_code='" + code + "'"))
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
                        disp_dgvlist();
                        goto_tbcntrl_list();
                        tpg_info_enable(false);
                        frm_clear();
                    }
                }
            }
            catch (Exception er) { MessageBox.Show("Error on saving." + er.Message); }
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
            DataTable dt = db.get_vat_list();

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["id"].Value = dt.Rows[r]["vat_code"].ToString();
                    row.Cells["name"].Value = dt.Rows[r]["vat_desc"].ToString();
                    row.Cells["type"].Value = dt.Rows[r]["vat_type"].ToString();
                    row.Cells["vat_sales"].Value = dt.Rows[r]["vat_sales"].ToString();
                    row.Cells["vat_purchase"].Value = dt.Rows[r]["vat_purch"].ToString();
                    row.Cells["vat_pct"].Value = dt.Rows[r]["vat_pct"].ToString();
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
                cbo_type.SelectedValue = dgv_list["type", r].Value.ToString();
                cbo_salesvat.SelectedValue = dgv_list["vat_sales", r].Value.ToString();
                cbo_purvat.SelectedValue = dgv_list["vat_purchase", r].Value.ToString();
                txt_pct.Text = dgv_list["vat_pct", r].Value.ToString();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
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
