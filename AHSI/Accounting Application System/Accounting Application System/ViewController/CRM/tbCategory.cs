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
    public partial class tbCategory : Form
    {
        DateTime dt_to;
        DateTime dt_frm;
        dbCRM db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        public tbCategory()
        {
            db = new dbCRM();
            InitializeComponent();
        }

        private void tbCategory_Load(object sender, EventArgs e)
        {
            txt_code.Enabled = false;
            disp_list();
            for(int i = 0; i <= 59; i++)
            {
                cbo_time_delay.Items.Add(i);
            }
            for(int i = 0;i <= 31; i++)
            {
                cbo_day_delay.Items.Add(i);
            }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            clear_frm();
            goto_win2();
        }
        public void goto_win2()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_2;
            tpg_opt_2.Show();

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

        private void btn_back_Click(object sender, EventArgs e)
        {
            clear_frm();
            goto_win1();
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String col = "", val = "", query = "";
            String notificationText = "", notifyadd = "";
            String table = "tb_category";
            String code = "", category = "", title = "", time_delay = "", day_delay = "" ;
           
            if (String.IsNullOrEmpty(txt_category.Text))
            {
                MessageBox.Show("Please enter textblast category.");
            }
            else
            {
                if (cbo_time_delay.SelectedIndex == -1)
                    time_delay = "0";
                else
                    time_delay = cbo_time_delay.Text.ToString();
                if (cbo_day_delay.SelectedIndex == -1)
                    day_delay = "0";
                else
                    day_delay = cbo_day_delay.Text.ToString();
                String update_code = txt_code.Text;
                category = txt_category.Text;               
                if (isnew)
                {
                    notificationText = "has added: ";
                    code = db.get_pk("tb_cat");

                    col = "tb_cat_id,cat_desc,cat_time_delay,cat_day_delay";
                    val = "'" + code + "','" + category + "','" + time_delay + "','" + day_delay + "'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        success = true;
                        notificationText += notifyadd;
                        notificationText += Environment.NewLine + " with #" + code;
                        notify.saveNotification(notificationText, "TEXTBLAST CATEGORY");
                        db.set_pkm99("tb_cat", db.get_nextincrementlimitchar(code, 8));
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    //temporaray



                    notificationText = "has updated: ";
                    col = "cat_desc='" + category + "',cat_time_delay='" + time_delay +"',cat_day_delay='" + day_delay + "'";

                    if (db.UpdateOnTable(table, col, "tb_cat_id='" + update_code + "'"))
                    {
                        success = true;
                        notificationText += notifyadd;
                        notificationText += Environment.NewLine + " with #" + code;
                        notify.saveNotification(notificationText, "TEXTBLAST TEMPLATE");
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
                disp_list();
                goto_win1();
                clear_frm();
            }
        }
        public void disp_list()
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.tb_category ORDER BY tb_cat_id ");

                int i = 0;

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];


                    row.Cells["ID"].Value = dt.Rows[r]["tb_cat_id"].ToString();
                    row.Cells["dgvl_category"].Value = dt.Rows[r]["cat_desc"].ToString();
                    row.Cells["dgvl_tim_del"].Value = dt.Rows[r]["cat_time_delay"].ToString();
                    row.Cells["dgvl_day_del"].Value = dt.Rows[r]["cat_day_delay"].ToString();

                    i++;
                }
            }

            catch (Exception er)
            { MessageBox.Show(er.Message + "Line : " + er.StackTrace); }
        }
        public void clear_frm()
        {
            txt_code.Text = "";
            txt_category.Text = "";
            cbo_day_delay.SelectedIndex = -1;
            cbo_time_delay.SelectedIndex = -1;
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            int r = -1;
            String code = "", canc = "";
            isnew = false;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;


                try
                {
                    //canc = dgv_list["dgvl_cancel", r].Value.ToString();
                }
                catch { }

                try
                {
                    code = dgv_list["ID", r].Value.ToString();
                    txt_code.Text = dgv_list["ID", r].Value.ToString();
                    txt_category.Text = dgv_list["dgvl_category", r].Value.ToString();
                    cbo_time_delay.Text = dgv_list["dgvl_tim_del", r].Value.ToString();
                    cbo_day_delay.Text = dgv_list["dgvl_day_del", r].Value.ToString();
                    goto_win2();

                }
                catch (Exception er) { MessageBox.Show("No category selected."); }

            }
            else
            {
                MessageBox.Show("No TexBlast Template is selected.");
            }
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
