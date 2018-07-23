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
    public partial class crm_TextBlast_Scheduling : Form
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
        int lnno_last = 1;
        String stk_trns_type = "PO";

        public crm_TextBlast_Scheduling()
        {
            InitializeComponent();
            

            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbCRM();
           
        }

        private void crm_TextBlast_Scheduling_Load(object sender, EventArgs e)
        {
             WindowState = FormWindowState.Maximized;

            DateTime temp_dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtp_frm.Value = temp_dt;
            dt_frm = dtp_frm.Value;
            dt_to = dtp_to.Value;
            dtp_frm.ValueChanged += dtp_frm_ValueChanged;
            dtp_to.ValueChanged += dtp_to_ValueChanged;
            disp_list();
            load_tb_templates();
         
        }

        public void load_tb_templates()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = db.QueryBySQLCode("SELECT * FROM rssys.tb_temp WHERE cancel ='N' ORDER BY tbtemp_id ASC");
                cbo_templates.DataSource = dt;
                cbo_templates.DisplayMember = "title";
                cbo_templates.ValueMember = "tbtemp_id";
                cbo_templates.SelectedIndex = -1;
            }
            catch (Exception) { }
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
                DataTable dt = db.get_tblist_withstat();
                int i = 0;
               
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];


                    row.Cells["dgvl_code"].Value = dt.Rows[r]["tbid"].ToString();
                    row.Cells["dgv_sms_desc"].Value = dt.Rows[r]["message"].ToString();
                    row.Cells["dgvl_send_date"].Value = dt.Rows[r]["date_send"].ToString();
                    row.Cells["dgvl_send_time"].Value = dt.Rows[r]["send_time"].ToString();
                    row.Cells["dgvl_noOfRec"].Value = dt.Rows[r]["recip"].ToString();
                    row.Cells["dgvl_sent_msg"].Value = dt.Rows[r]["send"].ToString();
                    row.Cells["dgvl_failed_msg"].Value = dt.Rows[r]["fail"].ToString();
                    row.Cells["dgvl_userid"].Value = dt.Rows[r]["userid"].ToString();
                    row.Cells["dgvl_t_date"].Value = dt.Rows[r]["t_date"].ToString();
                    //row.Cells["dgvl_pr_no"].Value = dt.Rows[r]["pr_no"].ToString();
                    row.Cells["dgvl_time"].Value = dt.Rows[r]["t_time"].ToString();
                   
                    if (dt.Rows[r]["cancel"].ToString() == "N")
                    {
                        row.Cells["dgvl_cancel"].Value = "NO";
                    }
                    else
                    {
                        row.Cells["dgvl_cancel"].Value = "YES";
                    }
                    row.Cells["dgvl_resend_date"].Value = dt.Rows[r]["resend_date"].ToString();
                    row.Cells["dgvl_resend_time"].Value = dt.Rows[r]["resend_time"].ToString();
                    row.Cells["dgvl_resend_userid"].Value = dt.Rows[r]["resend_userid"].ToString();
                   
                    i++;
                }
            }

            catch (Exception er)
            { MessageBox.Show(er.Message +"Line : " + er.StackTrace); }
        }
        
        void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            //disp_list();
        }

        void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            //disp_list();
        }

        void cbo_costCenter_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
           // dtp_poDate.Enabled = true;
            try
            {
                dgv_recip_list.Rows.Clear();
            }
            catch { }
            txt_message.Text = "";
            txt_tbid.Text = "";
            isnew = true;
            cbo_templates.SelectedIndex = -1;
            initialize_time_cbo();
            goto_win2();
        }


        public void initialize_time_cbo()
        {
            cbo_am_pm.SelectedIndex = 0;

            for (int i = 1; i <= 12; i++)
            {
                if (i <= 9)
                    cbo_hour.Items.Add("0" + i);
                else
                    cbo_hour.Items.Add(i);

            }
            cbo_hour.SelectedIndex = 0;
            for (int i = 0; i <= 59; i++)
            {
                if (i <= 9)
                {
                    cbo_min.Items.Add("0" + i);
                }
                else
                    cbo_min.Items.Add(i);

            }
            cbo_min.SelectedIndex = 0;
        }
        private void goto_win2()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_2;
            tpg_option_2.Show();

            tbcntrl_main.SelectedTab = tpg_right_entry;
            tpg_right_entry.Show();
            seltbp = false;
           
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
                    code = dgv_list["dgvl_code", r].Value.ToString();
                }
                catch { }

                

                if (canc == "YES")
                {
                    MessageBox.Show("TextBlast# :" + code + " is already cancelled. Can not be updated.");
                    
                }
                else
                {
                    try
                    {
                        String message = dgv_list["dgv_sms_desc", r].Value.ToString();
                        dtp_send.Value = gm.toDateValue(dgv_list["dgvl_send_date", r].Value.ToString());
                        //cbo_templates.SelectedValue = "";
                        txt_message.Text = message;
                        String amPmDesignator = "AM";
                        txt_tbid.Text = code;
                        times = dgv_list["dgvl_send_time", r].Value.ToString().Split(dilim);
                        cbo_hour.Items.Clear();
                        int hour = Convert.ToInt32(times[0]);
                        int min = Convert.ToInt32(times[1]);

                        if (hour == 12)
                            hour = 12;
                        else if (hour == 12)
                            amPmDesignator = "PM";
                        else if (hour > 12)
                        {
                            hour -= 12;
                            amPmDesignator = "PM";
                        }
                        cbo_min.Items.Clear();
                        initialize_time_cbo();
                        if(hour <= 9)
                            cbo_hour.Text = "0" + hour;
                        else
                            cbo_hour.Text = "" + hour;

                        if (min <= 9)
                            cbo_min.Text = "0" + min;
                        else
                            cbo_min.Text = "" + min;     
                        cbo_am_pm.Text = amPmDesignator;
                                          
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
                DataTable dt = db.QueryBySQLCode("SELECT r.*, d.* FROM rssys.tb_recip r LEFT JOIN rssys.m06 d ON d.d_code = r.d_code WHERE  r.tbid = '" + code + "'");
                DataTable recip = new DataTable();
                recip.Columns.Add("code");
                recip.Columns.Add("name");
                recip.Columns.Add("mobile1");
                recip.Columns.Add("mobile2");
                recip.Columns.Add("send_stat");
                for (int i = 0; i < dt.Rows.Count; i++ )
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

                    newRow["mobile1"] = dt.Rows[i]["d_cntc_no"].ToString();
                    newRow["mobile2"] = dt.Rows[i]["d_tel"].ToString();
                    newRow["code"] = dt.Rows[i]["d_code"].ToString();
                    newRow["send_stat"] = dt.Rows[i]["send_stat"].ToString();
                    recip.Rows.Add(newRow);
                    set_update_recip(recip, i);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            /*
            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                tbid = dgv_list["dgvl_purc_ord", r].Value.ToString();

                dialogResult = MessageBox.Show("Are you sure you want to cancel this P.O.# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    if (db.set_cancel("purhdr", "purc_ord='" + code + "'"))
                    {
                        db.DeleteOnTable("purlne", "purc_ord='" + code + "'");
                    }
                }
            }
            else
            {
                MessageBox.Show("No transactions selected.");
            }*/
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            String code, t_date;
            Report rpt = new Report();

            try
            {
                //int r = dgv_list.CurrentRow.Index;
                //
                //code = this.get_dgv_list_value("dgvl_purc_ord", r); //
                //String supl = db.get_supplier_name(this.get_dgv_list_value("dgvl_supl_name", r));
                //String supl_code = this.get_dgv_list_value("dgvl_supl_code", r);
                //String payment = this.get_dgv_list_value("dgvl_payment", r);
                //String ddate = this.get_dgv_list_value("dgvl_delv_date", r);
                //String notes = this.get_dgv_list_value("dgvl_notes", r);
                //String reference = this.get_dgv_list_value("dgvl_reference", r);
                //String prno = this.get_dgv_list_value("dgvl_pr_no", r);
                //t_date = this.get_dgv_list_value("dgvl_t_date", r);
                //
                //rpt.print_purchaseorder(code, prno, supl, payment, ddate, notes, t_date, supl_code, reference);
                //
                //rpt.Show();
                MessageBox.Show("This function is currently not available.");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private String get_dgv_list_value(String col, int row)
        {
            String val = "";

            try
            {
                val = dgv_list[col, row].Value.ToString();
            }
            catch (Exception er)
            {

            }

            return val;
        }

        

       

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            int r = 0;
            int cur_index = 0;
            isnew_item = false;

            try
            {
                if (dgv_recip_list.Rows.Count > 0)
                {
                    r = dgv_recip_list.CurrentRow.Index;

                    cur_index = int.Parse(dgv_recip_list[0, r].Value.ToString());

                   // _frm_ipr = new z_enter_item(this, isnew_item, lnno_last, cur_index);
                   // _frm_ipr.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String send_date = null, send_time = null, userid = null, t_date = null, t_time = null, message = null, resend_date = null, resend_time = null,resend_userid = null, cancel = "N", branch = null, transffered = null;
            String col = "", val = "", query = "";
            String notificationText = "", notifyadd = "",temp_id="";
            String table = "tb_hdr";
            String tableln = "tb_recip";
            int r;
            String code = "";
            int row_count = dgv_recip_list.Rows.Count -1;
            if (cbo_hour.Text == "" || cbo_hour.Text == null)
            {
                MessageBox.Show("Please enter sending hour.");
                return;
            }
            if (cbo_min.Text == "" || cbo_min.Text == null)
            {
                MessageBox.Show("Please enter sending minutes.");
                return;
            }
            if (txt_message.Text == "" || txt_message.Text == null)
            {
                MessageBox.Show("Please enter a message.");
            }
            if (row_count == 0)
            {
                MessageBox.Show("Please choose your recipients");
            }
            else if(cbo_templates.SelectedIndex == -1) 
            {
                MessageBox.Show("Please select a text blast templates.");
            }
            else
            {
                String update_code = txt_tbid.Text;
                send_date = gm.toDateString(dtp_send.Value.ToString("yyyy-MM-dd"), "yyyy-MM-dd");
                send_time = gm.toTime24hr(cbo_hour.Text + ":" + cbo_min.Text + " " + cbo_am_pm.Text);
                t_date = db.get_systemdate("yyyy-MM-dd");
                t_time = db.get_systemtime();
                message = txt_message.Text;
                temp_id = cbo_templates.SelectedValue.ToString();
                userid = GlobalClass.username;

                if (isnew)
                {
                    notificationText = "has added: ";
                    code = db.get_pk("tb_id");
                    col = "tbid,send_date,send_time,userid,t_date,t_time,message,cancel,branch,transffered,tbtemp_id";
                    val = "'" + code + "','" +
                        send_date + "','" +
                        send_time + "','" +
                        userid + "','" +
                        t_date + "','" +
                        t_time + "','" +
                        message + "','" +
                        cancel + "','" +
                        branch + "','" +
                        transffered + "','" +
                        temp_id + "'";
                        


                    if (db.InsertOnTable(table, col, val))
                    {

                        notifyadd = add_items(tableln, code);
                        db.set_pkm99("tb_id", db.get_nextincrementlimitchar(code, 8));
                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "TEXTBLAST SCHEDULING");
                           
                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "tb_id='" + code + "'");
                            db.DeleteOnTable(tableln, "tb_id='" + code + "'");
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        db.DeleteOnTable(table, "tb_id='" + code + "'");
                        db.DeleteOnTable(tableln, "tb_id='" + code + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    //temporaray
                 
                    col = "send_date='" + send_date + "'," +
                          "send_time='" + send_time + "'," +
                          "t_date='" + t_date + "'," +
                          "t_time='" + t_time + "'," +
                          "message='" + message + "'," +
                          "userid='" + userid + "'," +
                          "tbtemp_id='" + temp_id + "'" ;

                    notificationText = "has updated: ";


                    if (db.UpdateOnTable(table, col, "tbid='" + update_code + "'"))
                    {
                        db.DeleteOnTable(tableln, "tbid='" + update_code + "'");

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
                            db.DeleteOnTable(table, "tbid='" + update_code + "'");
                            db.DeleteOnTable(tableln, "tbid'" + update_code + "'");
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
                //disp_list();
                goto_win1();
                frm_clear();
            }
        }


       private String add_items(String tableln, String code)
        {
            String notificationText = null;
            String i_d_code = "", i_d_cntc = "", i_send_stat = "", i_date_sent = "", i_time_sent = "",i_d_cntc2 = ""; 
            String val2 = "";
            String col2 = "tbid, d_code, d_cntc,mobile1,mobile2,send_stat";

            val2 = "";
         

            for (int r = 0; r < dgv_recip_list.Rows.Count - 1; r++)
            {
                i_d_code = dgv_recip_list["recipient_id", r].Value.ToString();
                i_d_cntc = dgv_recip_list["mobile1", r].Value.ToString();
                if (dgv_recip_list["dgvi_sentstat", r].Value.ToString() == "Y")
                {
                    i_send_stat = "Y";
                }
                else
                {
                    i_send_stat = "N";
                }
                i_date_sent = gm.toDateString(dgv_recip_list["dgvi_date_sent", r].Value.ToString(), "yyyy-MM-dd");
                i_time_sent = gm.toTime24hr(dgv_recip_list["dgvi_time_sent", r].Value.ToString());
                i_d_cntc2 = dgv_recip_list["mobile2", r].Value.ToString();

                val2 = "'" + code + "', '" + i_d_code + "', '" + i_d_cntc + "','" + i_d_cntc + "','" + i_d_cntc2 + "','" + i_send_stat + "'";

                if (String.IsNullOrEmpty(i_date_sent) == false)
                {
                    col2 = col2 + ", date_sent, time_sent, send_stat";
                    val2 = val2 + ", '" + i_date_sent + "', '" + i_time_sent + "', '" + i_send_stat + "'";                   
                }

                if (db.InsertOnTable(tableln, col2, val2))
                {
                    notificationText += Environment.NewLine + code + " " + i_d_cntc;
                }
                else
                {
                    notificationText = null;
                }               
            }

            return notificationText;
        }

        private void goto_win1()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option;
            tpg_option.Show();

            tbcntrl_main.SelectedTab = tpg_right_list;
            tpg_right_list.Show();
            seltbp = false;
            disp_list();
            
        }

        private void frm_clear()
        {
            try
            {
                //txt_invoiceno.Text = "";
               // txt_reference.Text = "";
                ///txt_notes.Text = "";
                //cbo_supplier.SelectedIndex = -1;
               // cbo_payment.SelectedIndex = -1;
                //dtp_deliveryDate.Value = Convert.ToDateTime(db.get_systemdate(""));
               // dtp_poDate.Value = Convert.ToDateTime(db.get_systemdate(""));
              //  lbl_invoice_total.Text = "0.00";

                //dgv_itemlist.Rows.Clear();
                //dgv_delitem.Rows.Clear();
            }
            catch (Exception) { }
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            if (dgv_recip_list.Rows.Count > 1)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit without saving? You may loose some data.", "Confirm", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    goto_win1();
                    frm_clear();
                }
            }
            else
            {
                goto_win1();
                frm_clear();
            }
        }

        private void btn_PRitems_Click(object sender, EventArgs e)
        {
           
            int lastrow = 0;

            try
            {
                if (isnew == false)
                {
                    lastrow = dgv_recip_list.Rows.Count - 2;
                    lnno_last = int.Parse(dgv_recip_list["dgvi_lnno", lastrow].Value.ToString());
                    //inc_lnno();
                }

                else
                {
                    if (dgv_recip_list.Rows.Count == 1)
                    {
                        lnno_last = 0;
                       // inc_lnno();
                    }
                    else
                    {
                        lastrow = dgv_recip_list.Rows.Count - 2;
                        lnno_last = int.Parse(dgv_recip_list["dgvi_lnno", lastrow].Value.ToString());
                        //inc_lnno();
                    }
                }
            }
            catch (Exception) { }

            //z_add_item_from_PR pr = new z_add_item_from_PR(this, lnno_last);

            //pr.ShowDialog();
        }
                
        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_finalized_Click(object sender, EventArgs e)
        {

        }

        private void tbcntrl_left_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void btn_resend_Click(object sender, EventArgs e)
        {

        }

        private void btn_PRitems_Click_1(object sender, EventArgs e)
        {

        }

        private void dtp_deliveryDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_mainexit_Click_1(object sender, EventArgs e)
        {
            goto_win1();
            frm_clear();
        }

        private void btn_addRecipients_Click(object sender, EventArgs e)
        {
            z_add_recip frm = new z_add_recip(this);
            frm.ShowDialog();
        }
        public void set_selected_recip(DataTable dt)
        {
          
            int last_row = dgv_recip_list.Rows.Add();
            DataGridViewRow row = dgv_recip_list.Rows[last_row];
            row.Cells["recipient_id"].Value = dt.Rows[0]["code"].ToString();
            row.Cells["recipient_name"].Value = dt.Rows[0]["name"].ToString();
            row.Cells["mobile1"].Value = dt.Rows[0]["mobile1"].ToString();
            row.Cells["mobile2"].Value = dt.Rows[0]["mobile2"].ToString();
           
            row.Cells["dgvi_sentstat"].Value = "";
            row.Cells["dgvi_date_sent"].Value = "";
            row.Cells["dgvi_time_sent"].Value = "";

        }
        public void set_update_recip(DataTable dt, int i)
        {
            int last_row = dgv_recip_list.Rows.Add();
            DataGridViewRow row = dgv_recip_list.Rows[last_row];
            row.Cells["recipient_name"].Value = dt.Rows[i]["name"].ToString();
            row.Cells["mobile1"].Value = dt.Rows[i]["mobile1"].ToString();
            row.Cells["mobile2"].Value = dt.Rows[i]["mobile2"].ToString();
            row.Cells["recipient_id"].Value = dt.Rows[i]["code"].ToString();
            if (dt.Rows[i]["send_stat"].ToString() == "Y")
            {
                row.Cells["dgvi_sentstat"].Value = "Y";
            }
            else
            {
                row.Cells["dgvi_sentstat"].Value = "N";
            }
            
            row.Cells["dgvi_date_sent"].Value = "";
            row.Cells["dgvi_time_sent"].Value = "";
        }

        private void dgv_recip_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_itemupd_Click_1(object sender, EventArgs e)
        {
            z_add_recip frm = new z_add_recip(this);
            frm.ShowDialog();
        }

        private void btn_itemremove_Click_1(object sender, EventArgs e)
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

        private void cbo_templates_SelectionChangeCommitted(object sender, EventArgs e)
        {
            String id = cbo_templates.SelectedValue.ToString();
            DataTable dt = db.QueryBySQLCode("SELECT temp_body FROM rssys.tb_temp WHERE tbtemp_id ='" + id + "'");
            if(dt.Rows.Count > 0)
            {
                int index = dt.Rows.Count;
                txt_message.Text = dt.Rows[index -1]["temp_body"].ToString();
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void tpg_right_list_Click(object sender, EventArgs e)
        {

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

        private void dgv_recip_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
