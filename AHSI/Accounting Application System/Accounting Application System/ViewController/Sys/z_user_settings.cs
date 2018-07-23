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
    public partial class z_user_settings : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        dbSettings db = null;
        public z_user_settings()
        {
            InitializeComponent();
            db = new dbSettings();
            disp_dgvlist();
        }

        private void z_user_settings_Load(object sender, EventArgs e)
        {
            cbo_rights.SelectedValueChanged += cbo_rights_SelectedValueChanged;
            loadData();
        }

        private void loadData()
        {

        }

        void cbo_rights_SelectedValueChanged(object sender, EventArgs e)
        {
            selection();
        }

        private void selection()
        {

            
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            btn_save.Text = "Save";
            frm_clear();            
            goto_tbcntrl_info();
            isnew = true;
            loadCbo();
            selection();

        }

        private void loadCbo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = db.QueryBySQLCode("SELECT * FROM rssys.x07");
                cbo_rights.DataSource = dt;
                cbo_rights.DisplayMember = "grp_desc";
                cbo_rights.ValueMember = "grp_id";
                cbo_rights.SelectedIndex = -1;
             
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void btn_upditem_Click(object sender, EventArgs e)
        {
            btn_save.Text = "Update";
           
            loadCbo();
            //disp_info();
            //goto_tbcntrl_info();
            String code = "";
            int r = -1;
            isnew = false;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                try
                {
                    code = dgv_list["dgvl_userid", r].Value.ToString();
                    txt_code.Text = code;
                    txt_name.Text = dgv_list["dgvl_fullname", r].Value.ToString();
                    txtPass1.Text = dgv_list["dgvl_pass", r].Value.ToString();
                    txtPass2.Text = dgv_list["dgvl_pass", r].Value.ToString();
                    txt_name.Text = dgv_list["dgvl_fullname", r].Value.ToString();
                    cbo_rights.SelectedValue = dgv_list["dgvl_rightid", r].Value.ToString();
                    load_rights(dgv_list["dgvl_rightid", r].Value.ToString());
                }
                catch (Exception er) { MessageBox.Show("No group rights selected."); }
                goto_tbcntrl_info();
              
            }
            else
            {
                MessageBox.Show("No group rights selected.");
            }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently Disabled");

            #region old
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
            #endregion
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String uid = "", opr_name = "", pwd = "", grp_id = "", d_code = "";

            String col = "", val = "", code = "";
            String notificationText = "", notifyadd = "";
            String table = "x08";
            String tableln = "";
            String approve_disc = "";
            int r;

            if (String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("User ID is required.");
            }
            else if (String.IsNullOrEmpty(txtPass1.Text))
            {
                MessageBox.Show("Password is required.");
            }
            else if (String.IsNullOrEmpty(txt_name.Text))
            {
                MessageBox.Show("Fullname is required.");
            }
            else if (!txtPass1.Text.Equals(txtPass2.Text))
            {
                MessageBox.Show("Password confirmation did not match.");
            }
            else if (cbo_rights.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a group rights.");
            }
            else
            {
                code = txt_code.Text;
                opr_name = txt_name.Text;
                pwd = txtPass2.Text;
                grp_id = cbo_rights.SelectedValue.ToString();
                d_code = cbo_rights.Text;
                if (appr_disc.Checked)
                    approve_disc = "y";
                else
                    approve_disc = "n";
                if (isnew)
                {

                    notificationText = "Has added";
                    col = "uid,opr_name,pwd,grp_id,d_code,approve_disc";
                    val = "'" + code + "','" + opr_name + "','" + pwd + "','" + grp_id + "','" + d_code + "','" + approve_disc + "'";
                    if (db.InsertOnTable(table, col, val))
                    {
                        notificationText += notifyadd;
                        notificationText += Environment.NewLine + " with #" + code;
                        notify.saveNotification(notificationText, "GROUP RIGHTS");
                        success = true;
                    }
                    else
                    {
                        db.DeleteOnTable(table, "uid='" + code + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    notificationText = "has updated: ";
                    col = "uid='" + code + "',opr_name='" + opr_name + "',pwd='" + pwd + "',grp_id='" + grp_id + "',d_code='" + d_code + "',approve_disc='" + approve_disc + "'";

                    if (db.UpdateOnTable(table, col, "uid='" + code + "'"))
                    {
                        notificationText += notifyadd;
                        notificationText += Environment.NewLine + " with #" + code;
                        notify.saveNotification(notificationText, "GROUP RIGHTS");
                        success = true;
                    }
                    else
                    {
                        db.DeleteOnTable(table, "uid='" + code + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
            }

            if (success)
            {
                frm_clear();
                goto_tbcntrl_list();
                disp_dgvlist();
                //disp_list();
                //goto_win1();
            }
        }

        private Boolean checkIfUserExists(String username)
        {
            return false;
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
            txt_name.Text = "";
            txtPass1.Text = "";
            txtPass2.Text = "";
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

            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {

                DataTable dt = db.QueryBySQLCode("SELECT a.*,b.* FROM rssys.x08 a LEFT JOIN rssys.x07 b ON a.grp_id = b.grp_id");
                int i = 0;
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["dgvl_userid"].Value = dt.Rows[r]["uid"].ToString();
                    row.Cells["dgvl_fullname"].Value = dt.Rows[r]["opr_name"].ToString();
                    row.Cells["dgvl_rightdesc"].Value = dt.Rows[r]["grp_desc"].ToString();
                    row.Cells["dgvl_rightid"].Value = dt.Rows[r]["grp_id"].ToString();
                    row.Cells["dgvl_pass"].Value = dt.Rows[r]["pwd"].ToString();
                    i++;
                }
            }

            catch (Exception er)
            { MessageBox.Show(er.Message); }
            
        }

        private void disp_info()
        {
            #region old
            String source = "";
            /*
            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_code.Text = dgv_list["d_code", r].Value.ToString();
                txt_customer.Text = dgv_list["d_name", r].Value.ToString();
                rtxt_address.Text = dgv_list["d_addr2", r].Value.ToString();
                txt_phone.Text = dgv_list["d_tel", r].Value.ToString();
                txt_fax.Text = dgv_list["d_fax", r].Value.ToString();
                txt_email.Text = dgv_list["d_email", r].Value.ToString();
                txt_tin.Text = dgv_list["d_tin", r].Value.ToString();
                txt_contactperson.Text = dgv_list["d_cntc", r].Value.ToString();
                txt_limit.Text = dgv_list["limit", r].Value.ToString();
                cbo_mop.SelectedValue = dgv_list["at_code", r].Value.ToString();
                cbo_subledger.SelectedValue = dgv_list["mp_code", r].Value.ToString();
                rtxt_remark.Text = dgv_list["remarks", r].Value.ToString();

                source = dgv_list["source", r].Value.ToString();

                if (source == "H")
                {
                    cbo_source.SelectedIndex = 1;
                }
                else
                {
                    cbo_source.SelectedIndex = 0;
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); } */

            #endregion


        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = 0;
            String custcode = "", custname = "";
            /*
            try
            {
                if (iscallbackfrm)
                {
                    if (_frm_soa != null)
                    {
                        r = dgv_list.CurrentRow.Index;

                        custcode = dgv_list[0, r].Value.ToString();
                        custname = dgv_list[1, r].Value.ToString();

                        _frm_soa.set_custvalue_frm(custcode, custname);
                        this.Close();
                    }
                }
            }
            catch (Exception) { } */
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
        public void load_rights(String code)
        {
            String space = "";
            int i = 0;
            String level = "";
            try
            {
                dgv_list_x05.Rows.Clear();
            }
            catch (Exception ex) { }
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id WHERE a.grp_id = '" + code + "' ORDER BY b.pla, b.mod_id");

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    level = dt.Rows[r]["level"].ToString();
                    if (level == "2")
                    {
                        space = "\t\t - ";
                    }
                    else if (level == "3")
                    {
                        space = "\t\t\t\t * ";
                    }

                    //dgv_list_x05.Rows.Add();
                    i = dgv_list_x05.Rows.Add();

                    DataGridViewRow row = dgv_list_x05.Rows[i];

                    dgv_list_x05["dgvi_mod_id", r].Value = dt.Rows[r]["grp_id"].ToString();
                    dgv_list_x05["dgvi_mdesc", r].Value =  space + dt.Rows[r]["grp_desc"].ToString();

                    if (dt.Rows[r]["restrict"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_allowed", r].Value = dt.Rows[r]["restrict"].ToString();
                    }
                    if (dt.Rows[r]["add"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_create", r].Value = dt.Rows[r]["add"].ToString();
                    }
                    if (dt.Rows[r]["upd"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_update", r].Value = dt.Rows[r]["upd"].ToString();
                    }
                    if (dt.Rows[r]["cancel"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_cancel", r].Value = dt.Rows[r]["cancel"].ToString();
                    }
                    if (dt.Rows[r]["print"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_print", r].Value = dt.Rows[r]["print"].ToString();
                    }
                    dgv_list_x05["dgvi_view", r].Value = true;

                    i++;
                    space = "";
                    if (dt.Rows[r]["level"].ToString() == "1")
                    {
                        row.DefaultCellStyle.BackColor = Color.DimGray;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    if (dt.Rows[r]["level"].ToString() == "2")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error disp_itemlist : " + ex.Message);
            }
        }
        private void cbo_rights_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
            if (cbo_rights.SelectedValue.ToString() != "-1")
            {
                String code = cbo_rights.SelectedValue.ToString();
                load_rights(code);
            }
           
        }
    }
}
