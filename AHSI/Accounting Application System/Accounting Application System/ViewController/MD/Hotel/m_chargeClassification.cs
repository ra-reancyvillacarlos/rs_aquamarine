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
    public partial class m_chargeClassification : Form
    {
        Boolean isnew = false;
        thisDatabase db = new thisDatabase();

        public m_chargeClassification()
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
            cbo_type.Text = "Charge";
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
            cbo_type.Enabled = flag;
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
            //MessageBox.Show("This section has been disabled.");

            if (dgv_list.Rows.Count > 1)
            {
                int r = dgv_list.CurrentRow.Index;

                if ((dgv_list["isdefault", r].Value??"").ToString() != "Y")
                {
                    if (db.UpdateOnTable("chgclass", "cancel='Y'", "cc_code='" + dgv_list["id", r].Value.ToString() + "'"))
                    {
                        disp_dgvlist();
                        goto_tbcntrl_list();
                        tpg_info_enable(false);
                        MessageBox.Show("Successfully deleted.");
                    }
                    else
                    {
                        MessageBox.Show("Failed on deleting.");
                    }
                }
                else
                {
                    MessageBox.Show("Cannot deleted default classification");
                }
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Boolean success = false, isdef;
            String code, desc, type;
            String col = "", val = "";

            if (String.IsNullOrEmpty(txt_name.Text) || String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                code = txt_code.Text.ToUpper();
                desc = txt_name.Text.ToUpper();
                type = cbo_type.Text;

                isdef = (db.get_colval("chgclass", "isdefault", "cc_code='" + code + "'") == "Y");

                if (isnew && !isdef)
                {
                    code = txt_code.Text;//db.get_pk("brd_code");
                    col = "cc_code, cc_desc, cc_type, isdefault";
                    val = "'" + code + "', '" + desc + "', '" + type + "','N'";

                    if (db.InsertOnTable("chgclass", col, val))
                    {
                        success = true;
                        //db.set_pkm99("brd_code", db.get_nextincrementlimitchar(code, 3));
                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable("chgclass", "cc_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                        
                    }
                }
                else if (!isdef)
                {
                    col = "cc_code='" + code + "', cc_desc='" + desc + "', cc_type='" + type + "'";
                    if (db.UpdateOnTable("chgclass", col, "cc_code='" + code + "'"))
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
                    MessageBox.Show("Your save entry are default charge classification.");
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
            DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.chgclass WHERE COALESCE(cancel,'')<>'Y' ORDER BY cc_desc");

            clear_dgv();
            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["id"].Value = dt.Rows[r]["cc_code"].ToString();
                    row.Cells["name"].Value = dt.Rows[r]["cc_desc"].ToString();
                    row.Cells["type"].Value = dt.Rows[r]["cc_type"].ToString();

                    row.Cells["isdefault"].Value = (dt.Rows[r]["isdefault"] ?? "").ToString();
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
                cbo_type.Text = dgv_list["type", r].Value.ToString();

                btn_save.Enabled = ((dgv_list["isdefault", r].Value ?? "").ToString() != "Y");
                tpg_info_enable(btn_save.Enabled);
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
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
            String typname = "name";

            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "id";
                }
                else
                {
                    typname = "name";
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
