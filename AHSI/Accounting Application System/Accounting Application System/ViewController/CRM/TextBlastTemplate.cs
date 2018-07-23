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
    public partial class TextBlastTemplate : Form
    { 
       
        DateTime dt_to;
        DateTime dt_frm;
        dbCRM db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
      
     
        public TextBlastTemplate()
        {
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbCRM();
           
            InitializeComponent();
        }

        private void TextBlastTemplate_Load(object sender, EventArgs e)
        {
            clear_frm();
            disp_list();
            load_cboCat();
        }

        public void load_cboCat()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = db.QueryBySQLCode("SELECT * FROM rssys.tb_category");
                cbo_cat.DataSource = dt;
                cbo_cat.DisplayMember = "cat_desc";
                cbo_cat.ValueMember = "tb_cat_id";
                cbo_cat.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            clear_frm();
            goto_win2();
            
        }
        public void goto_win2()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_2;
            tpg_option_2.Show();

            tbcntrl_main.SelectedTab = tpg_right_entry;
            tpg_right_entry.Show();
            seltbp = false;
        }
        public void goto_win1()
        { 
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option;
            tpg_option.Show();

            tbcntrl_main.SelectedTab = tpg_right_list;
            tpg_right_list.Show();
            seltbp = false;
           
        }
        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
            
        }

        private void tbcntrl_left_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(seltbp == false)
            {
                e.Cancel = true;
            }
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            goto_win1();
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String col = "", val = "", query = "";
            String notificationText = "", notifyadd = "";
            String table = "tb_temp";
            String code = "", mesage = "", title = "", cat_id = "";
            if(String.IsNullOrEmpty(txt_message.Text))
            {
                MessageBox.Show("Please enter your message template.");
            }
            else if (String.IsNullOrEmpty(txt_title.Text))
            {
                MessageBox.Show("Please enter template title");
            }
            else if(cbo_cat.Text == "")
            {
                MessageBox.Show("Please enter a category.");
            }
            else
            {
                String update_code = txt_tbid.Text;
                mesage = txt_message.Text;
                title = txt_title.Text;
                cat_id = cbo_cat.SelectedValue.ToString();
                if (isnew)
                {
                    notificationText = "has added: ";
                    code = db.get_pk("tbtemp_id");

                    col = "tbtemp_id,temp_body,cancel,title,tb_cat_id";
                    val = "'" + code + "','" + mesage + "','N','" + title + "','" + cat_id + "'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        success = true;
                        notificationText += notifyadd;
                        notificationText += Environment.NewLine + " with #" + code;
                        notify.saveNotification(notificationText, "TEXTBLAST TEMPLATE");
                        db.set_pkm99("tbtemp_id", db.get_nextincrementlimitchar(code, 8));
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
                    col = "temp_body='" + mesage + "',title='" + title + "',tb_cat_id='" + cat_id + "'";

                    if (db.UpdateOnTable(table, col, "tbtemp_id='" + update_code + "'"))
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
                String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
                String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");
                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.tb_temp a LEFT JOIN rssys.tb_category b ON a.tb_cat_id = b.tb_cat_id  ORDER BY tbtemp_id ASC");
              
                int i = 0;

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];


                    row.Cells["dgvl_code"].Value = dt.Rows[r]["tbtemp_id"].ToString();
                    row.Cells["dgvl_title"].Value = dt.Rows[r]["title"].ToString();
                    row.Cells["dgvl_body_temp"].Value = dt.Rows[r]["temp_body"].ToString();
                    row.Cells["dgvl_cancel"].Value = dt.Rows[r]["cancel"].ToString();
                    row.Cells["dgvl_cat_id"].Value = dt.Rows[r]["tb_cat_id"].ToString();
                    row.Cells["dgvl_category"].Value = dt.Rows[r]["cat_desc"].ToString();
                
                    i++;
                }
            }

            catch (Exception er)
            { MessageBox.Show(er.Message + "Line : " + er.StackTrace); }

        }

        private void btn_upd_Click(object sender, EventArgs e)
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
                    canc = dgv_list["dgvl_cancel", r].Value.ToString();
                }
                catch { }

                code = dgv_list["dgvl_code", r].Value.ToString();

                if (canc == "Y")
                {
                    MessageBox.Show("TextBlast Template # :" + code + " is already cancelled. Can not be updated.");

                }
                else
                {
                    try
                    {
                        txt_message.Text = dgv_list["dgvl_body_temp", r].Value.ToString();
                        txt_title.Text = dgv_list["dgvl_title", r].Value.ToString();
                        txt_tbid.Text = code;
                        cbo_cat.SelectedValue = dgv_list["dgvl_cat_id", r].Value.ToString();

                    }
                    catch (Exception er) { MessageBox.Show(er.Message); }

                   
                    goto_win2();
                }
            }
            else
            {
                MessageBox.Show("No TexBlast Template is selected.");
            }
        }
        public void clear_frm()
        {
            txt_tbid.Text = "";
            txt_message.Text = "";
            cbo_cat.SelectedIndex = -1;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            Boolean success = false;
            String msg = "";
            String tbid = "";
            int r = -1;
            if (dgv_list.SelectedRows.Count <= 0 || dgv_list.Rows.Count <= 0)
            {
                MessageBox.Show("Nothing to cancel.");
                return;
            }
            if (MessageBox.Show(
                "Are you sure you want to cancel selected transactions?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1
                ) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            r = dgv_list.CurrentRow.Index;
            try
            {
                tbid = dgv_list.Rows[r].Cells["dgvl_code"].Value.ToString();
                if (db.set_cancel("tb_hdr", "tbid='" + tbid + "'"))
                {
                    db.DeleteOnTable("tb_recip", "tbid='" + tbid + "'");
                    success = true;
                    msg += tbid + "\n";
                }
            }
            catch
            {
                MessageBox.Show("Nothing to cancel.");
            }

            if (success)
            {
                MessageBox.Show("TexBlast transactions with transaction ID : \n " + msg + " is successfully cancelled.");
                disp_list();
            }
            return;
        }

        private void btn_addCategory_Click(object sender, EventArgs e)
        {
            tbCategory tbc = new tbCategory();
            tbc.ShowDialog();
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
