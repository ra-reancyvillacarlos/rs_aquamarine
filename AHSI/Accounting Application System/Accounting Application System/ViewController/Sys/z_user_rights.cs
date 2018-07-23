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
    public partial class z_user_rights : Form
    {
        Boolean seltbp = false;
        Boolean isnew = true;
        dbSettings db = null;
        public z_user_rights()
        {
            db = new dbSettings();
            InitializeComponent();
            this.Load += z_user_rights_Load;
        }

        void z_user_rights_Load(object sender, EventArgs e)
        {
            disp_list();
        }

        private void loadData()
        {
            thisDatabase db = new thisDatabase();
            
            dgv_list.DataSource = db.get_grouprights();
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            
            add_update();
            isnew = true;
            txt_name.Text = "";
            txt_code.Text = "";

          
        }

        private void add_update()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_2;
            tpg_opt_2.Show();
            disp_x05();
            tbcntrl_main.SelectedTab = tpg_info;
            tpg_info.Show();
            seltbp = false;
            
        }
        public void goto_win1()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_1;
            tpg_opt_1.Show();

            tbcntrl_main.SelectedTab = tpg_list;
            tpg_list.Show();
            seltbp = false;
        }
        public void disp_list()
        {
          
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {

                DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x07");
                int i = 0;
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["dgvl_code"].Value = dt.Rows[r]["grp_id"].ToString();
                    row.Cells["dgvl_fullname"].Value = dt.Rows[r]["grp_desc"].ToString();
                   
                    i++;
                }
            }

            catch (Exception er)
            { MessageBox.Show(er.Message); }
        }
        public void disp_x05()
        {
            int i = 0;
            int level = 3;
            String space = "";
            try { dgv_list_x05.Rows.Clear(); }
            catch { }
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x05 ORDER BY pla, mod_id");
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {

                        if (dt.Rows[r]["level"].ToString() == "2")
                        {
                            space = "\t\t - ";
                        }
                        else if (dt.Rows[r]["level"].ToString() == "3")
                        {
                            space = "\t\t\t\t * ";
                        }

                        i = dgv_list_x05.Rows.Add();

                        DataGridViewRow row = dgv_list_x05.Rows[i];
                        row.Cells["dgvi_mod_id"].Value = dt.Rows[r]["mod_id"].ToString();
                        row.Cells["dgvi_mdesc"].Value = space + dt.Rows[r]["grp_desc"].ToString();
                      
                        if (row.Cells["dgvi_allowed"].Value == "" || row.Cells["dgvi_allowed"].Value == null)
                        {
                            row.Cells["dgvi_allowed"].Value = "y";
                        }
                        if (row.Cells["dgvi_create"].Value == "" || row.Cells["dgvi_create"].Value == null)
                        {
                            row.Cells["dgvi_create"].Value = "y";
                        }
                        if (row.Cells["dgvi_update"].Value == "" || row.Cells["dgvi_update"].Value == null)
                        {
                            row.Cells["dgvi_update"].Value = "y";
                        }
                        if (row.Cells["dgvi_cancel"].Value == "" || row.Cells["dgvi_cancel"].Value == null)
                        {
                            row.Cells["dgvi_cancel"].Value = "y";
                        }
                        if (row.Cells["dgvi_view"].Value == "" || row.Cells["dgvi_view"].Value == null)
                        {
                            row.Cells["dgvi_view"].Value = "y";
                        }
                        if (row.Cells["dgvi_print"].Value == "" || row.Cells["dgvi_print"].Value == null)
                        {
                            row.Cells["dgvi_print"].Value = "y";
                        }
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
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listing : " + ex.Message);
            }
        }
        private void mainMenu()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_1;
            tpg_opt_1.Show();

            tbcntrl_main.SelectedTab = tpg_list;
            tpg_list.Show();
            seltbp = false;
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            String code = "";
            int r = -1;
            isnew = false;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                code = dgv_list["dgvl_code", r].Value.ToString();
                try
                {
                    txt_code.Text = code;
                    txt_name.Text = dgv_list["dgvl_fullname", r].Value.ToString();
                }
                catch (Exception er) { MessageBox.Show("No group rights selected."); }
                add_update();
                disp_itemlist(code);
                
                //disp_itemlist2(code);
                //disp_x05();
               
               
            }
            else
            {
                MessageBox.Show("No group rights selected.");
            }
        }

        public void disp_itemlist(String code)
        {
            String space = "";
            int i = 0;
            String level = "";
            String yes = "";
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
                     
                    dgv_list_x05["dgvi_mod_id", r].Value = dt.Rows[r]["mod_id"].ToString();
                    dgv_list_x05["dgvi_mdesc", r].Value = space + dt.Rows[r]["grp_desc"].ToString();
                    
                    if (dt.Rows[r]["restrict"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_allowed", r].Value =  dt.Rows[r]["restrict"].ToString();
                    }
                   
                    if (dt.Rows[r]["add"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_create", r].Value =  dt.Rows[r]["add"].ToString();
                    }
                    if (dt.Rows[r]["upd"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_update", r].Value = dt.Rows[r]["upd"].ToString();
                    }
                    if (dt.Rows[r]["cancel"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_cancel", r].Value =  dt.Rows[r]["cancel"].ToString();
                    }
                    if (dt.Rows[r]["print"].ToString() == "y")
                    {
                        dgv_list_x05["dgvi_print", r].Value =  dt.Rows[r]["print"].ToString();
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
        public void disp_itemlist2(String code)
        {
            String space = "";
            int i = 0;
            String level = "";
            String yes = "";
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

                    dgv_list_x05["dgvi_mod_id", r].Value = dt.Rows[r]["mod_id"].ToString();
                    dgv_list_x05["dgvi_mdesc", r].Value = space + dt.Rows[r]["grp_desc"].ToString();

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
        private void btn_delitem_Click(object sender, EventArgs e)
        {
          
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String desc = "";
            String code = "";

            String col = "", val = "";
            String notificationText = "", notifyadd = "";
            String table = "x07";
            String tableln = "x06";
            int r;

            if (String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Rights ID is requird.");
            }
            else if (String.IsNullOrEmpty(txt_name.Text))
            {
                MessageBox.Show("Rights description is required");
            }
            else
            {
                code = txt_code.Text;
                desc = txt_name.Text;
                
            

                if (isnew)
                {
                   
                    notificationText = "Has added";
                    col = "grp_id,grp_desc";
                    val = "'" + code + "','" + desc + "'";
                    if (db.InsertOnTable(table, col, val))
                    {
                        notifyadd = add_rights(tableln, code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "GROUP RIGHTS");

                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "grp_id='" + code + "'");
                            db.DeleteOnTable(tableln, "grp_id='" + code + "'");
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        db.DeleteOnTable(table, "grp_id='" + code + "'");
                        db.DeleteOnTable(tableln, "grp_id='" + code + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    notificationText = "has updated: ";
                    col = "grp_id='" + code + "',grp_desc='" + desc + "'";

                    if (db.UpdateOnTable(table, col, "grp_id='" + code + "'"))
                    {
                        db.DeleteOnTable(tableln, "grp_id='" + code + "'");

                        notifyadd = add_rights(tableln, code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "GROUP RIGHTS");
                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "grp_id='" + code + "'");
                            db.DeleteOnTable(tableln, "grp_id='" + code + "'");
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
            }

            if (success)
            {
                frm_clear();
                disp_list();
                goto_win1();
            }
        }
        public String add_rights(String tableln, String code)
        {
            String notificationText = null;
            String col = "", val = "";
            String grp_id = "", mod_id = "", restrict = "", add = "", upd = "", cancel = "", print = "";
            col = "grp_id,mod_id,restrict,add,upd,cancel,print";
     
            for (int r = 0; r < dgv_list_x05.Rows.Count; r++)
            {

                mod_id = (dgv_list_x05["dgvi_mod_id", r].Value??"n").ToString();
                restrict = (dgv_list_x05["dgvi_allowed", r].Value??"n").ToString();
                add = (dgv_list_x05["dgvi_create", r].Value??"n").ToString();
                upd = (dgv_list_x05["dgvi_update", r].Value??"n").ToString();
                cancel = (dgv_list_x05["dgvi_cancel", r].Value??"n").ToString();
                print = (dgv_list_x05["dgvi_print", r].Value??"n").ToString();

                val = "'" + code + "',"
                    + "'" + mod_id + "',"
                    + "'" + restrict + "',"
                    + "'" + add + "',"
                    + "'" + upd + "',"
                    + "'" + cancel + "',"
                    + "'" + print + "'";
                if (db.InsertOnTable(tableln, col, val))
                {
                    notificationText += Environment.NewLine + code;
                }
                else
                {
                    notificationText = null;
                }
            }

            return notificationText;
        }
        private void frm_clear()
        {
            try
            {
                txt_code.Text = "";
                txt_name.Text = "";
                txt_search.Text = "";
            }
            catch (Exception) { }
        }
        private void btn_back_Click(object sender, EventArgs e)
        {
            mainMenu();

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

      
    }
}
