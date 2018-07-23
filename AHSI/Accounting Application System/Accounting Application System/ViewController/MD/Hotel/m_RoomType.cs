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
    public partial class m_RoomType : Form
    {
        Boolean isnew = false;

        public m_RoomType()
        {
            InitializeComponent();
            disp_dgvlist();
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
            txt_name.Text = "";
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
            isnew = false;
            tpg_info_enable(true);
            frm_clear();
            disp_info();
            goto_tbcntrl_info();
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This section has been disabled.");
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
            else
            {
                code = txt_code.Text;
                desc = txt_name.Text;

                if (isnew)
                {
                    code = txt_code.Text;//db.get_pk("brd_code");
                    col = "typ_code, typ_desc";
                    val = "'" + code + "', '" + desc + "'";

                    if (db.InsertOnTable("rtype", col, val))
                    {
                        success = true;
                        //db.set_pkm99("brd_code", db.get_nextincrementlimitchar(code, 3));
                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable("rtype", "typ_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "typ_code='" + code + "', typ_desc='" + desc + "'";

                    if (db.UpdateOnTable("rtype", col, "typ_code='" + code + "'"))
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
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_rType_list();

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["id"].Value = dt.Rows[r]["typ_code"].ToString();
                    row.Cells["name"].Value = dt.Rows[r]["typ_desc"].ToString();
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
    }
}
