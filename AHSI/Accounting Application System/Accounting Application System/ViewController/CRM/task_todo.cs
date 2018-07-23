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
    public partial class to_do : Form
    {
        private z_enter_item_simple _frm_ipr;
        thisDatabase db = null;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        Boolean isUpd = false;
        int lnno_last = 1;

        public to_do()
        {
            InitializeComponent();
           

            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new thisDatabase();
  
            gc.load_priority(cbo_priority);
            gc.load_customer(cbo_customer);
           
            disp_list();
        }

        void task_todo_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void disp_list()
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
                String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");

                DataTable dt = db.QueryBySQLCode("SELECT taskid, task_desc, date_task, date_to_remind, time_to_remind, user_id, t_date, t_time, client_id, client_name, priority_no, date_to_remind_to,repeat_reminder FROM rssys.taskhdr WHERE (t_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "')");
                int i = 0;

                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["dgv_taskid"].Value = dt.Rows[r]["taskid"].ToString();
                        row.Cells["dgv_task_desc"].Value = dt.Rows[r]["task_desc"].ToString();
                        row.Cells["dgv_date_task"].Value = gm.toDateString(dt.Rows[r]["date_task"].ToString(), "");
                        row.Cells["dgv_repeat_reminder"].Value = dt.Rows[r]["repeat_reminder"].ToString();
                        row.Cells["dgv_date_to_remind"].Value = gm.toDateString(dt.Rows[r]["date_to_remind"].ToString(), "");
                        row.Cells["dgv_date_to_remind_to"].Value = gm.toDateString(dt.Rows[r]["date_to_remind_to"].ToString(), "");
                        row.Cells["dgv_time_to_remind"].Value = dt.Rows[r]["time_to_remind"].ToString();
                        row.Cells["dgv_user_id"].Value = dt.Rows[r]["user_id"].ToString();
                        row.Cells["dgv_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(),"");
                        row.Cells["dgv_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                        row.Cells["dgv_client_id"].Value = dt.Rows[r]["client_id"].ToString();
                        row.Cells["dgv_client_name"].Value = dt.Rows[r]["client_name"].ToString();
                        row.Cells["dgv_priority_no"].Value = dt.Rows[r]["priority_no"].ToString();
                     
                        i++;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        void cbo_costCenter_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        public void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_to.Value <= dtp_frm.Value)
            {
                MessageBox.Show("'TO' date must not be lesser or equal to 'FROM' date");
            }
            else
                disp_list();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            frm_clear();
            goto_win2();
        }

        private void frm_clear()
        {
            try
            {
                txt_task_id.Text = "";
                txt_desc.Text = "";
                cbo_priority.SelectedIndex = -1;
                dtp_date_remind.Value = Convert.ToDateTime(db.get_systemdate(""));
                dtp_date_remind_to.Value = Convert.ToDateTime(db.get_systemdate(""));
                //dtp_time_remind.Value = Convert.ToDateTime(db.get_systemtime(""));
                cbo_customer.SelectedIndex = -1;

                dgv_itemlist.Rows.Clear();
               
            }
            catch (Exception) { }

            try
            {
                
            }
            catch { }
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
            isnew = false;
            isUpd = true;
            int r = dgv_list.CurrentRow.Index;
            String code = dgv_list["dgv_taskid", r].Value.ToString();

            DataTable dt = db.QueryBySQLCode("SELECT taskid, task_desc, date_task, date_to_remind, time_to_remind, user_id, t_date, t_time, client_id, client_name, priority_no,repeat_reminder,date_to_remind_to FROM rssys.taskhdr WHERE taskid ='" + code + "'");
            txt_task_id.Text = code;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txt_desc.Text = dt.Rows[i]["task_desc"].ToString();
                    cbo_customer.SelectedValue = dt.Rows[i]["client_id"].ToString();
                    cbo_priority.SelectedValue = dt.Rows[i]["priority_no"].ToString();
                    dtp_time_remind.Value = Convert.ToDateTime(dt.Rows[i]["time_to_remind"].ToString());
                    dtp_date_remind.Value = gm.toDateValue(dt.Rows[i]["date_to_remind"].ToString());
                    dtp_date_remind_to.Value = gm.toDateValue(dt.Rows[i]["date_to_remind_to"].ToString());

                    cbo_repeat.Text = dt.Rows[i]["repeat_reminder"].ToString();
                    //
                    //dtp.Value = gm.toDateValue(dt.Rows[i]["trnx_date"].ToString());

                    //txt_amtdue.Text = dt.Rows[i]["ord_amnt"].ToString();
                    //txt_totaltax.Text = dt.Rows[i]["disc_amnt"].ToString();
                    //txt_payment.Text = dt.Rows[i]["payment"].ToString();

                    //dtp_ord_date.Value = gm.toDateValue(dt.Rows[i]["ord_date"].ToString());
                    //cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                    //cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                    //cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                }
            }
            goto_win2();
            disp_itemlist(code);
            isUpd = false;
        }
           

        private void disp_itemlist(string code)
        {
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT task_id, task_line_desc, ln_num FROM rssys.tasklne WHERE task_id='" + code + "'");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvi_desc", i].Value = dt.Rows[i]["task_line_desc"].ToString();
                   
                    
                    //if (isnew == false)
                    //{
                    //    dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["quantity"].ToString();
                    //}
                    //else
                    //{
                    //    dgv_itemlist["dgvi_oldqty", i].Value = "0.00";
                    //}
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            DataTable dt;
            String code = "";
            int r = -1;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                if (dgv_list["dgvl_cancel", r].Value.ToString().Equals("Y"))
                {
                    MessageBox.Show("Invoice already cancelled.");
                }
                else
                {
                    code = dgv_list["dgvl_pr_code", r].Value.ToString();

                    dialogResult = MessageBox.Show("Are you sure you want to cancel this P.I.# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        if (db.UpdateOnTable("prhdr", "reference='CANCELLED' || '-' ||reference, cancel='Y'", "pr_code='" + code + "'"))
                        {
                            db.DeleteOnTable("prlne", "pr_code='" + code + "'");
                        }

                        disp_list();
                    }
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            String code, t_date, date_needed, project, requestedby;
            Report rpt = new Report();

            try
            {
                int r = dgv_list.CurrentRow.Index;

                code = dgv_list["dgvl_pr_code", r].Value.ToString();
                t_date = dgv_list["dgvl_t_date", r].Value.ToString();
                date_needed = dgv_list["dgvl_prdate", r].Value.ToString();
                project = dgv_list["dgvl_subCostcentre", r].Value.ToString();
                requestedby = db.get_salesrep_name(dgv_list["dgvl_requestedby", r].Value.ToString());

                rpt.print_purchaserequest(code, project, requestedby, date_needed, t_date);

                rpt.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btn_itemadd_Click(object sender, EventArgs e)
        {
            int line = 0;
            
            if(dgv_itemlist.Rows.Count == 0)
            {
                line = 0;
            }
            else{

                line = int.Parse(dgv_itemlist["dgvi_lnno", dgv_itemlist.Rows.Count-1].Value.ToString());
            }
            task_item frm = new task_item(this, true, (line+1).ToString());
            frm.ShowDialog();
        }

        private void inc_lnno()
        {
            lnno_last++;
        }

        public void set_dgv_itemlist(DataTable dt)
        {
            int i = 0;

            try
            {
                if (isnew_item)
                {
                    i = dgv_itemlist.Rows.Add();
                }
                else
                {
                    i = dgv_itemlist.CurrentRow.Index;
                }
                dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[0]["dgvi_part_no"].ToString();
                dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[0]["dgvi_ln"].ToString();
                dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[0]["dgvi_itemcode"].ToString();
                dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[0]["dgvi_itemdesc"].ToString();
                dgv_itemlist["dgvi_qty", i].Value = dt.Rows[0]["dgvi_qty"].ToString();
                dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[0]["dgvi_unitdesc"].ToString();
                dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[0]["dgvi_unitid"].ToString();
                dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[0]["dgvi_price"].ToString();
                dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[0]["dgvi_lnamt"].ToString();
                dgv_itemlist["dgvi_notes", i].Value = dt.Rows[0]["dgvi_notes"].ToString();
                dgv_itemlist["dgvi_price", i].Value = dt.Rows[0]["dgvi_price"].ToString();
                if (isnew_item)
                {
                    inc_lnno();
                }
            }
            catch (Exception) { }

            disp_total();
        }

        public void disp_total()
        {
            Double total = 0.00;

            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count - 1; i++)
                {
                    //total += gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnvat", i].Value.ToString());
                    total += gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnamnt", i].Value.ToString());
                }
            }
            catch (Exception) { }

            //lbl_invoice_total.Text = gm.toAccountingFormat(total);
        }

        public String get_dgvi_lnno(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnno", currow].Value.ToString();

            return val;
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            String notificationText = "has added: ";
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String txtid, task_desc, date_to_remind, time_to_remind, user_id, t_date, t_time, client_id, client_name, priority_no, date_task;

            String col = "", val = "";
            String notifyadd = "";
            String table = "taskhdr";
            String tableln = "tasklne";

            if (cbo_priority.SelectedIndex == -1)
            {
                MessageBox.Show("Please select priority level.");
                cbo_priority.DroppedDown = true;
            }
            else if(cbo_customer.SelectedIndex==-1)
            {
                MessageBox.Show("Please Select Client");
                m_customers frm = new m_customers(this, true);
                frm.ShowDialog();
            }
            else if(txt_desc.Text == "")
            {
                MessageBox.Show("Please input task description header");
                txt_desc.Focus();
            }
            //else if (cbo_subcostcenter.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please select the sub cost center field.");
            //}
            //else if (String.IsNullOrEmpty(txt_reference.Text))
            //{
            //    MessageBox.Show("Please type a reference.");
            //}
            //else if (dgv_itemlist.Rows.Count <= 1)
            //{
            //    MessageBox.Show("Please enter item(s) to request.");
            //}
            else
            {
                txtid = txt_task_id.Text;
                task_desc = txt_desc.Text;
                date_to_remind = dtp_date_remind.Value.ToString("yyyy-MM-dd");
                time_to_remind = dtp_time_remind.Value.ToString("HH:mm");
                user_id = GlobalClass.username;
                t_date = dtp_current_date.Value.ToString("yyyy-MM-dd");
                t_time = dtp_current_time.Value.ToString("HH:mm");
                client_id = cbo_customer.SelectedValue.ToString();
                client_name = cbo_customer.Text;
                priority_no = cbo_priority.SelectedValue.ToString();
                date_task = DateTime.Now.ToString("yyyy-MM-dd");
                String date_to_remind_to = dtp_date_remind_to.Value.ToString("yyyy-MM-dd");
                String repeat = cbo_repeat.Text;
                
                if (isnew)
                {
                    txtid = db.get_pk("taskid");

                    col = "taskid, task_desc, date_to_remind,date_to_remind_to, time_to_remind, user_id, t_date, t_time, client_id, client_name, priority_no, date_task,repeat_reminder";
                    val = "'" + txtid + "', " + db.str_E(task_desc) + ", '" + date_to_remind + "','" + date_to_remind_to + "','" + time_to_remind + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + client_id + "', '" + client_name + "', '" + priority_no + "', '" + date_task + "'," + db.str_E(repeat) + "";

                    if (db.InsertOnTable("taskhdr", col, val))
                    {
                        add_items(tableln, txtid);
                                               
                            txtid = db.get_nextincrementlimitchar(txtid, 8);

                            db.set_pkm99("taskid", txtid);
                            success = true;
                        
                       
                    }
                    else
                    {
                        db.DeleteOnTable(table, "taskid='" + txtid + "'");
                        db.DeleteOnTable(tableln, "taskid='" + txtid + "'");

                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    
                    notificationText = "has updated: ";
                    col = "taskid='" + txtid + "', task_desc=" + db.str_E(task_desc) + ", date_to_remind='" + date_to_remind + "',date_to_remind_to='" + date_to_remind_to + "', time_to_remind='" + time_to_remind + "', user_id='" + user_id + "', t_date='" + t_date + "', t_time='" + t_time + "', client_id='" + client_id + "', client_name='" + client_name + "', priority_no='" + priority_no + "',repeat_reminder=" + db.str_E(repeat) + "";

                    if (db.UpdateOnTable("taskhdr", col, "taskid='" + txtid + "'"))
                    {
                        db.DeleteOnTable("tasklne", "taskid='" + txtid + "'");

                        add_items(tableln, txtid);

                            success = true;
                      
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
                frm_clear();
            }
        }

        private String add_items(String tableln, String task_id)
        {
            String notificationText = null;
            String  ln_num = "", task_line_desc = "";
            String val2 = "";
            String col2 = "task_id, ln_num, task_line_desc";
            try {
                DataTable dtcheck = db.QueryBySQLCode("SELECT * FROM rssys.tasklne WHERE task_id='" + task_id + "'");
                if (dtcheck.Rows.Count > 0)
                {
                    db.DeleteOnTable(tableln, "task_id='" + task_id + "'");
                }
            for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
            {
                ln_num = dgv_itemlist["dgvi_lnno", r].Value.ToString();
                task_line_desc = dgv_itemlist["dgvi_desc", r].Value.ToString();

                val2 = "'" + task_id + "', '" + ln_num + "', " + db.str_E(task_line_desc) + "";
                
                if (db.InsertOnTable(tableln, col2, val2))
                {
                    //MessageBox.Show("Inserted");
                }
                else
                {
                    notificationText = null;
                }
            }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            return notificationText;
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            if (dgv_itemlist.Rows.Count > 1)
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

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            String r = "0";
            isnew_item = false;

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index.ToString();

                    task_item frm = new task_item(this, false, r);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            String qty = "";

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;
            

                    dgv_itemlist.Rows.RemoveAt(r);

                   
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        public String get_dgvi_ln(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnno", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_part_no(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_part_no", currow].Value.ToString();

            return val;
        }
        public String get_dgvi_itemcode(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_itemcode", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_itemdesc(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_itemdesc", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_unitid(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_costunitid", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_qty(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_qty", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_price(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_costprice", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_lnamt(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnamnt", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_notes(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_notes", currow].Value.ToString();

            return val;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_left_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void btn_customer_Click(object sender, EventArgs e)
        {
            m_customers frm = new m_customers(this, true);
            frm.ShowDialog();
        }
        public void set_custvalue_frm(String custcode)
        {
            try { cbo_customer.Items.Clear(); }
            catch { }

            gc.load_customer(cbo_customer);
            cbo_customer.SelectedValue = custcode;
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

        private void cbo_repeat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpd == false)
            {
                String repeat = cbo_repeat.Text;
                if (repeat == "Daily")
                {
                    dtp_date_remind_to.Value = dtp_date_remind.Value.AddDays(1);
                }
                else if (repeat == "Weekly")
                {
                    dtp_date_remind_to.Value = dtp_date_remind.Value.AddDays(7);
                }
                else if (repeat == "Monthly")
                {
                    dtp_date_remind_to.Value = dtp_date_remind.Value.AddMonths(1);
                }
            }
        }

        private void dgv_itemlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
