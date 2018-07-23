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
    public partial class EmailBlastSchedule : Form
    {
        private z_enter_item _frm_ipr;
        DateTime dt_to;
        DateTime dt_frm;
        dbCRM db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        public EmailBlastSchedule()
        {
            InitializeComponent();
            
            DateTime temp_dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtp_frm.Value = temp_dt;
            dt_frm = dtp_frm.Value;
            dt_to = dtp_to.Value;
            dtp_frm.ValueChanged += dtp_frm_ValueChanged;
            dtp_to.ValueChanged += dtp_to_ValueChanged;

            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbCRM();
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

        private void goto_win1()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option;
            tpg_option.Show();

            tbcntrl_main.SelectedTab = tpg_right_list;
            tpg_right_list.Show();
            seltbp = false;
            

        }
        
        private void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void EmailBlastSchedule_Load(object sender, EventArgs e)
        {
            disp_list();
        }

        private void disp_list()
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
                String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");
                //DataTable dt = db.QueryOnTableWithParams("tb_hdr", "*", "t_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "'", " ORDER BY purc_ord ASC");


                DataTable dt = db.QueryBySQLCode("SELECT t.*, t.tb_emailid , to_char(send_date,'MMDD/yyyy') AS date_send, (SELECT COUNT(r.d_code) FROM rssys.tb_email_recip r WHERE r.tb_emailid=t.tb_emailid) AS RECIP, (SELECT COUNT(r.d_code) FROM rssys.tb_email_recip r WHERE r.tb_emailid=t.tb_emailid AND sent_stat='Y') AS SEND,(SELECT COUNT(r.d_code) FROM rssys.tb_email_recip r WHERE r.tb_emailid = t.tb_emailid AND sent_stat = 'N') AS FAIL FROM rssys.tb_emailhdr t ORDER BY t.tb_emailid DESC");
                int i = 0;

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];


                    row.Cells["dgvl_code"].Value = dt.Rows[r]["tb_emailid"].ToString();
                    row.Cells["dgv_sms_desc"].Value = dt.Rows[r]["message"].ToString();
                    row.Cells["dgvl_send_date"].Value = gm.toDateString(dt.Rows[r]["send_date"].ToString(),"");
                    row.Cells["dgvl_userid"].Value = dt.Rows[r]["userid"].ToString();
                    row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(), "");
                    row.Cells["dgvl_time"].Value = dt.Rows[r]["t_time_"].ToString();
                    row.Cells["file_path"].Value = dt.Rows[r]["file_path"].ToString();
                    row.Cells["sender_email"].Value = dt.Rows[r]["sender_email"].ToString();
                    row.Cells["password"].Value = dt.Rows[r]["sender_pass"].ToString();
                    if (dt.Rows[r]["cancel"].ToString() == "N")
                    {
                        row.Cells["dgvl_cancel"].Value = "NO";
                    }
                    else
                    {
                        row.Cells["dgvl_cancel"].Value = "YES";
                    }

                    i++;
                }
            }

            catch (Exception er)
            { MessageBox.Show(er.Message + "Line : " + er.StackTrace); }
        }
        public void set_selected_recip(DataTable dt)
        {


            int last_row = dgv_recip_list.Rows.Add();
            DataGridViewRow row = dgv_recip_list.Rows[last_row];
            row.Cells["recipient_id"].Value = dt.Rows[0]["code"].ToString();
            row.Cells["recipient_name"].Value = dt.Rows[0]["name"].ToString();
            row.Cells["email"].Value = dt.Rows[0]["email"].ToString();
            row.Cells["dgvi_sentstat"].Value = "";
            row.Cells["dgvi_date_sent"].Value = "";
            row.Cells["dgvi_time_sent"].Value = "";

        }

        private void btn_addRecipients_Click(object sender, EventArgs e)
        {
            z_add_recip rp = new z_add_recip(this);
            rp.ShowDialog();
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(seltbp == false)
            {
                e.Cancel = true;
            }
        }

        private void tbcntrl_left_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(seltbp == false)
            {
                e.Cancel = true;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_recip_list.Rows.Clear();
            }
            catch { }
            txt_message.Text = "";
            txt_tbid.Text = "";
            txt_file_path.Text = "";
            txt_email_add.Text = "";
            txt_password.Text = "";
            isnew = true;
            goto_win2();
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            goto_win1();
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
          
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String send_date = "", time_sent = "", userid = "", t_date = "", t_time_ = "", message = "", cancel = "N", branch = "", file_path = "", sender_email = "", sender_pass="";
            String col = "", val = "", query = "";
            String notificationText = "", notifyadd = "", temp_id = "";
            String table = "tb_emailhdr";   
            String tableln = "tb_email_recip";
            int r;
            String code = "";
            int row_count = dgv_recip_list.Rows.Count - 1;
           
            if (txt_message.Text == "" || txt_message.Text == null)
            {
                MessageBox.Show("Please enter a message.");
            }
            else if (row_count == 0)
            {
                MessageBox.Show("Please choose your recipients");
            }
            else if( txt_email_add.Text == "" || String.IsNullOrEmpty(txt_email_add.Text))
            {
                MessageBox.Show("Please enter sender's email address.");
            }
            else
            {
                String update_code = txt_tbid.Text;
                send_date = gm.toDateString(dtp_send.Value.ToString("yyyy-MM-dd"), "yyyy-MM-dd");
               
                t_date = db.get_systemdate("yyyy-MM-dd");
                t_time_ = db.get_systemtime();
                message = txt_message.Text;
                file_path = txt_file_path.Text;
                sender_email = txt_email_add.Text;
                userid = GlobalClass.username;
                sender_pass = txt_password.Text;
                if (isnew)
                {
                    notificationText = "has added: ";
                    code = db.get_pk("tb_emailid");
                    col = "tb_emailid,send_date,time_sent,userid,t_date,t_time_,message,cancel,  branch,file_path,sender_email,sender_pass";
                    val = "'" + code + "','" + send_date + "','" + time_sent + "','" + userid + "','" + t_date + "','" + t_time_ + "'," + db.str_E(message) + ",'" + cancel + "','" + branch + "'," + db.str_E(file_path) + "," + db.str_E(sender_email) + "," + db.str_E(sender_pass) + "";


                    if (db.InsertOnTable(table, col, val))
                    {
                        notifyadd = add_items(tableln, code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "TEXTBLAST SCHEDULING");
                            db.set_pkm99("tb_emailid", db.get_nextincrementlimitchar(code, 8));
                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "tb_emailid='" + code + "'");
                            db.DeleteOnTable(tableln, "tb_emailid='" + code + "'");
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        db.DeleteOnTable(table, "tb_emailid='" + code + "'");
                        db.DeleteOnTable(tableln, "tb_emailid='" + code + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    //temporaray

                    col = "send_date='" + send_date + "',time_sent = '" + time_sent + "',userid='" + userid + "',t_date='" + t_date + "',t_time_='" + t_time_ + "',message=" + db.str_E(message) + ",cancel='" + cancel + "',branch='" + branch + "',file_path=" + db.str_E(file_path) + ",sender_email=" + db.str_E(sender_email) + ",sender_pass=" + db.str_E(sender_pass) + "";

                    notificationText = "has updated: ";


                    if (db.UpdateOnTable(table, col, "tb_emailid='" + update_code + "'"))
                    {
                        db.DeleteOnTable(tableln, "tb_emailid='" + update_code + "'");

                        notifyadd = add_items(tableln, update_code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + update_code;
                            notify.saveNotification(notificationText, "TEXTBLAST SCHEDULING");
                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "tb_emailid='" + update_code + "'");
                            db.DeleteOnTable(tableln, "tb_emailid'" + update_code + "'");
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
                disp_list();
                goto_win1();
               
            }
        }

        private String add_items(String tableln, String code)
        {
            String notificationText = null;
            String tb_emailid = "", d_code = "", email = "", date_sent = "", time_sent = "", sent_stat = "";
            String val2 = "";
            String col2 = "tb_emailid, d_code, email,sent_stat";

            val2 = "";


            for (int r = 0; r < dgv_recip_list.Rows.Count - 1; r++)
            {
                d_code = dgv_recip_list["recipient_id", r].Value.ToString();
                email = dgv_recip_list["email", r].Value.ToString();
                sent_stat = gm.toStr(dgv_recip_list["dgvi_sentstat", r].Value.ToString());
                date_sent = gm.toDateString(dgv_recip_list["dgvi_date_sent", r].Value.ToString(), "yyyy-MM-dd");
                time_sent = gm.toTime24hr(dgv_recip_list["dgvi_time_sent", r].Value.ToString());

                val2 = "'" + code + "', '" + d_code + "', '" + email + "','N'";

                if (String.IsNullOrEmpty(date_sent) == false)
                {
                    col2 = col2 + ", date_sent, time_sent";
                    val2 = val2 + ", '" + date_sent + "', '" + time_sent + "'";
                }

                if (db.InsertOnTable(tableln, col2, val2))
                {
                    notificationText += Environment.NewLine + code + " " + email;
                }
                else
                {
                    notificationText = null;
                }
            }

            return notificationText;
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = browseFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_file_path.Text = browseFile.FileName;
            }
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            char[] dilim = { ' ', ':', '-' };
            string[] times = null;
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
                txt_tbid.Text = code;
                
                if (canc == "YES")
                {
                    MessageBox.Show("TextBlast# :" + code + " is already cancelled. Can not be updated.");

                }
                else
                {
                    try
                    {
                        String message = dgv_list["dgv_sms_desc", r].Value.ToString();
                        txt_file_path.Text = dgv_list["file_path", r].Value.ToString();
                        dtp_send.Value = gm.toDateValue(dgv_list["dgvl_send_date", r].Value.ToString());
                        txt_email_add.Text = dgv_list["sender_email", r].Value.ToString();
                        txt_message.Text = message;
                        txt_tbid.Text = code;
                        txt_password.Text = dgv_list["password", r].Value.ToString();

                    }
                    catch (Exception er) { MessageBox.Show(er.Message); }

                    disp_itemlist(code);

                    goto_win2();
                }
            }
            else
            {
                MessageBox.Show("No TexBlast Transaction is selected.");
            }
        }
        private void disp_itemlist(string code)
        {
            try
            {
                dgv_recip_list.Rows.Clear();
            }
            catch { }
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT r.*, d.* FROM rssys.tb_email_recip r LEFT JOIN rssys.m06 d ON d.d_code = r.d_code WHERE  r.tb_emailid = '" + code + "'");
                DataTable recip = new DataTable();
                recip.Columns.Add("code");
                recip.Columns.Add("name");
                recip.Columns.Add("email");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow newRow = recip.NewRow();

                    if (String.IsNullOrEmpty(dt.Rows[i]["firstname"].ToString()))
                    {
                        newRow["name"] = dt.Rows[i]["d_name"].ToString();
                    }
                    else
                    {
                        newRow["name"] = dt.Rows[i]["firstname"].ToString() + " " + dt.Rows[i]["mname"].ToString() + " " + dt.Rows[i]["lastname"].ToString();
                    }

                    newRow["email"] = dt.Rows[i]["d_email"].ToString();
                    newRow["code"] = dt.Rows[i]["d_code"].ToString();
                    recip.Rows.Add(newRow);
                    set_update_recip(recip, i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void set_update_recip(DataTable dt, int i)
        {
            int last_row = dgv_recip_list.Rows.Add();
            DataGridViewRow row = dgv_recip_list.Rows[last_row];
            row.Cells["recipient_name"].Value = dt.Rows[i]["name"].ToString();
            row.Cells["email"].Value = dt.Rows[i]["email"].ToString();
            row.Cells["recipient_id"].Value = dt.Rows[i]["code"].ToString();
            row.Cells["dgvi_sentstat"].Value = "";
            row.Cells["dgvi_date_sent"].Value = "";
            row.Cells["dgvi_time_sent"].Value = "";
        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            if (dgv_recip_list.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Nothing to remove.");
                return;
            }
            if (MessageBox.Show(
                "Remove selected recipients?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1
                ) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            foreach (DataGridViewRow item in dgv_recip_list.SelectedRows)
            {
                try
                {
                    dgv_recip_list.Rows.RemoveAt(item.Index);
                }
                catch
                {
                    MessageBox.Show("Nothing to remove.");
                }
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
