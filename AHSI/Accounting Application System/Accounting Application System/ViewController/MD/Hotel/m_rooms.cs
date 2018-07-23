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
    public partial class m_rooms : Form
    {
        Boolean isnew = false;

        public m_rooms()
        {
            InitializeComponent();
            dgv_list.SelectionChanged += dgv_list_SelectionChanged;
            disp_dgvlist();
        }

        void dgv_list_SelectionChanged(object sender, EventArgs e)
        {

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
            txt_code.Text = "";
            txt_desc.Text = "";
            //txt_branch.Text = "";
            txtStatCode.Text = "";
            //txt_gov.Text = "";
            //txt_ord.Text = "";
            //txt_scc.Text = "";
            txt_search.Text = "";
            txtRemarks.Text = "";
            //txt_transferred.Text = "";
            txtTypeCode.Text = "";
        }

        private void goto_tbcntrl_info()
        {
            tbcntrl_main.SelectedTab = tpg_info;
            tbcntrl_option.SelectedTab = tpg_opt_2;

            tpg_info.Show();
            tpg_opt_2.Show();
        }

        private void goto_tbcntrl_list()
        {
            tbcntrl_main.SelectedTab = tpg_list;
            tbcntrl_option.SelectedTab = tpg_opt_1;

            tpg_list.Show();
            tpg_opt_1.Show();
        }

        private void tpg_info_enable(Boolean flag)
        {
            txt_code.Enabled = flag;
            txt_desc.Enabled = flag;
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
            MessageBox.Show("This section has been disabled");
            //thisDatabase db = new thisDatabase();
            //int r;

            //if (dgv_list.Rows.Count > 1)
            //{
            //    r = dgv_list.CurrentRow.Index;

            //    if (db.UpdateOnTable("brand", "cancel='Y'", "brd_code='" + dgv_list["ID", r].Value.ToString() + "'"))
            //    {
            //        disp_dgvlist();
            //        goto_tbcntrl_list();
            //        tpg_info_enable(false);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Failed on deleting.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No rows selected.");
            //}
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(2004);
            rpt.Show();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            Boolean success = false;
            String code, desc, typ_code, stat_code, remarks;
            String col = "", val = "", add_col = "", add_val = "";
            String table = "rooms";

            if (String.IsNullOrEmpty(txt_desc.Text) && String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                code = txt_code.Text;
                desc = txt_desc.Text;
                typ_code = txtTypeCode.Text;
                stat_code = txtStatCode.Text;
                remarks = txtRemarks.Text;

                if (isnew)
                {
                    code = txt_code.Text;
                    col = "rom_code, rom_desc, typ_code, stat_code, remarks";
                    val = "'" + code + "', '" + desc + "', '" + typ_code + "', '" + stat_code + "', '" + remarks + "'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable(table, "rom_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "rom_desc='" + desc + "', typ_code='" + typ_code + "', stat_code='" + stat_code + "', remarks='" + remarks + "'";

                    if (db.UpdateOnTable(table, col, "rom_code='" + rom_code + "'"))
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
            dbHotel dbh = new dbHotel();

            DataTable temp = dbh.get_rooms();

            if (temp.Rows.Count > 0)
            {
                dgv_list.AutoGenerateColumns = false;
                dgv_list.DataSource = temp;
            }
        }

        private void disp_info()
        {
            int r = dgv_list.CurrentRow.Index;

            txt_code.Text = dgv_list["rom_code", r].Value.ToString();
            txt_desc.Text = dgv_list["rom_desc", r].Value.ToString();
            txtTypeCode.Text = (dgv_list["typ_code", r].Value != null) ? dgv_list["typ_code", r].Value.ToString() : "";
            txtStatCode.Text = (dgv_list["stat_code", r].Value != null) ? dgv_list["stat_code", r].Value.ToString() : "";
            txtRemarks.Text = (dgv_list["remarks", r].Value != null) ? dgv_list["remarks", r].Value.ToString() : "";

        }
    }
}
