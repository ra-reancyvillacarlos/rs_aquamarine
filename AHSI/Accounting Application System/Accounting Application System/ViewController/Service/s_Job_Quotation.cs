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
    public partial class s_Job_Quotation : Form
    {
        private z_enter_item_simple _frm_ipr;
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        dbInv db = new dbInv();
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        int lnno_last = 1;

        public s_Job_Quotation()
        {
            InitializeComponent();
            dgv_delitem.Hide();

            gc.load_crm(cbo_customer);
            gc.load_vehicle_info(cbo_vehicle);

            DateTime temp_dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtp_frm.Value = temp_dt;

            dtp_frm.ValueChanged += dtp_frm_ValueChanged;
            dtp_to.ValueChanged += dtp_to_ValueChanged;
            disp_list();
        }

        void s_Job_Quotation_Load(object sender, EventArgs e)
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

                DataTable dt = db.QueryBySQLCode("SELECT to_char(t_date,'yyyy-MM-dd') AS t_date, to_char(jq_date,'yyyy-MM-dd') as jq_date, *  FROM rssys.jqhdr WHERE jq_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "' ORDER BY jq_code");
                int i = 0;

                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["dgvl_jq_code"].Value = dt.Rows[r]["jq_code"].ToString();
                        row.Cells["dgvl_customer"].Value = dt.Rows[r]["customer"].ToString();
                        row.Cells["dgvl_vehicle"].Value = dt.Rows[r]["vehicle"].ToString();
                        row.Cells["dgvl_t_date"].Value = dt.Rows[r]["t_date"].ToString();
                        row.Cells["dgvl_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                        row.Cells["dgvl_jqdate"].Value = dt.Rows[r]["jq_date"].ToString();
                        row.Cells["dgvl_tln_amnt"].Value = dt.Rows[r]["total_ln_amnt"].ToString();
                        row.Cells["dgvl_vin_no"].Value = dt.Rows[r]["vin_no"].ToString();
                        row.Cells["dgvl_cust_code"].Value = dt.Rows[r]["cust_code"].ToString();
                        row.Cells["dgvl_plate_no"].Value = dt.Rows[r]["vplate_no"].ToString();
                        row.Cells["dgvl_cancel"].Value = dt.Rows[r]["cancel"].ToString();
                        row.Cells["dgvl_est_dayscomp"].Value = dt.Rows[r]["est_dayscomp"].ToString();
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
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
            m_auto_customer frm = new m_auto_customer(this, true);
            frm.ShowDialog(); 
        }

        private void frm_clear()
        {
            try
            {
                txt_invoiceno.Text = "";
                cbo_customer.SelectedIndex = -1;
                rtxt_address.Text = "";
                txt_contact.Text = "";
                cbo_vehicle.SelectedIndex = -1;
                txt_vin.Text = "";
                txt_plateno.Text = "";
                txt_est_dayscomp.Text = "";

                dtp_jqDate.Value = Convert.ToDateTime(db.get_systemdate(""));
                lbl_invoice_total.Text = "0.00";
                
                dgv_itemlist.Rows.Clear();
                dgv_delitem.Rows.Clear();
            }
            catch (Exception) { }

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

                code = dgv_list["dgvl_jq_code", r].Value.ToString();

                if (canc == "Y")
                {
                    MessageBox.Show("JQ# " + code + " is already cancelled. Can not be updated.");
                }
                else
                {
                    try
                    {
                        txt_invoiceno.Text = code;
                        cbo_customer.SelectedValue = dgv_list["dgvl_cust_code", r].Value.ToString();
                        cbo_vehicle.SelectedValue = dgv_list["dgvl_vin_no", r].Value.ToString();
                        txt_est_dayscomp.Text = dgv_list["dgvl_est_dayscomp", r].Value.ToString();
                        dtp_jqDate.Value = Convert.ToDateTime(dgv_list["dgvl_jqdate", r].Value.ToString());
                        lbl_invoice_total.Text = dgv_list["dgvl_tln_amnt", r].Value.ToString();
                    }
                    catch (Exception er) { MessageBox.Show("No invoice selected"); }
                  
                    disp_itemlist(code);
                    disp_total();
                    goto_win2();
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void disp_itemlist(string code)
        {
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT jql.*, u.* FROM rssys.jqhdr jq LEFT JOIN rssys.jqlne jql ON jql.jq_code=jq.jq_code LEFT JOIN rssys.itmunit u ON u.unit_id=jql.purc_unit WHERE jq.jq_code='" + code + "'");
               
                for(int i=0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["quantity"].ToString();
                    dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[i]["unit_shortcode"].ToString();
                    dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[i]["purc_unit"].ToString();
                    dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[i]["cost_price"].ToString();
                    dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[i]["price"].ToString();
                    dgv_itemlist["dgvi_notes", i].Value = dt.Rows[i]["addl_desc"].ToString();
                    if (isnew == false)
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["quantity"].ToString();
                    }
                    else
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = "0.00";
                    }
                }
            }
            catch { }
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
                    code = dgv_list["dgvl_jq_code", r].Value.ToString();

                    dialogResult = MessageBox.Show("Are you sure you want to cancel this J.Q.# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        if (db.UpdateOnTable("jqhdr", "customer='CANCELLED' || '-' ||customer, cancel='Y'", "jq_code='" + code + "'"))
                        {
                            //db.DeleteOnTable("prlne", "pr_code='" + code + "'");
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
                /*
                code = dgv_list["dgvl_jq_code", r].Value.ToString();
                t_date = dgv_list["dgvl_t_date", r].Value.ToString();
                date_needed = dgv_list["dgvl_jqdate", r].Value.ToString();
                project = dgv_list["dgvl_subCostcentre", r].Value.ToString();
                requestedby = db.get_salesrep_name(dgv_list["dgvl_requestedby", r].Value.ToString());

                rpt.print_purchaserequest(code, project, requestedby, date_needed, t_date);

                rpt.Show();
                 */
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btn_itemadd_Click(object sender, EventArgs e)
        {
          
            int lastrow = 0;
            isnew_item = true;

            try
            {
                if (isnew == false)
                {
                    lastrow = dgv_itemlist.Rows.Count - 2;
                    lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                    inc_lnno();
                }

                else
                {
                    if (dgv_itemlist.Rows.Count == 1)
                    {
                        lnno_last = 0;
                        inc_lnno();
                    }
                    else
                    {
                        lastrow = dgv_itemlist.Rows.Count - 2;
                        lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                        inc_lnno();
                    }
                }

            }
            catch { }

            _frm_ipr = new z_enter_item_simple(this, isnew_item, lnno_last);
            _frm_ipr.ShowDialog();
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

            lbl_invoice_total.Text = gm.toAccountingFormat(total);
        }

        public String get_dgvi_lnno(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnno", currow].Value.ToString();

            return val;
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {

            if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the customer field.");
                m_auto_customer frm = new m_auto_customer(this, true);
                frm.ShowDialog(); 
            }
            else if (cbo_vehicle.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the sub cost center field.");
                m_vehiclec_info frm = new m_vehiclec_info(this, true);
                frm.ShowDialog();
            }
            else if (String.IsNullOrEmpty(txt_est_dayscomp.Text))
            {
                MessageBox.Show("Please type an estimated days.");
            }
            else if(dgv_itemlist.Rows.Count <= 1)
            {
                MessageBox.Show("Please enter item(s) to request.");
            }
            else
            {
                z_Notification notify = new z_Notification();
                String notificationText = "has added: ";
                Boolean success = false;
                String col = "", val = "";
                String notifyadd = "";
                String table = "jqhdr";
                String tableln = "jqlne";

                String code, t_date, t_time, customer, cust_code, vehicle, vin_no, vplate_no, est_dayscomp, total_ln_amnt, jq_date;

                code = txt_invoiceno.Text;
                customer = cbo_customer.Text;
                cust_code = cbo_customer.SelectedValue.ToString();
                vehicle = cbo_vehicle.Text;
                vin_no = txt_vin.Text;
                vplate_no = txt_plateno.Text;
                jq_date = dtp_jqDate.Value.ToString("yyyy-MM-dd");
                t_date = db.get_systemdate("");
                t_time = db.get_systemtime();
                est_dayscomp = txt_est_dayscomp.Text;
                total_ln_amnt = gm.toDoubleStr(lbl_invoice_total.Text);
                
                if (isnew)
                {
                    code = db.get_pk("jq_code");

                    col = "jq_code, jq_date, t_date, t_time, customer, cust_code, vehicle, vin_no, vplate_no, est_dayscomp, total_ln_amnt";
                    val = "'" + code + "','" + jq_date + "', '" + t_date + "', '" + t_time + "', $$" + customer + "$$, $$" + cust_code + "$$, $$" + vehicle + "$$, $$" + vin_no + "$$, $$" + vplate_no + "$$, $$" + est_dayscomp + "$$, $$" + total_ln_amnt + "$$";

                    if (db.InsertOnTable(table, col, val))
                    {
                        notifyadd = add_items(tableln, code, t_date);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "PURCHASE REQUEST");

                            code = db.get_nextincrementlimitchar(code, 8);

                            db.set_pkm99("jq_code", code);
                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "jq_code='" + code + "'");
                            db.DeleteOnTable(tableln, "jq_code='" + code + "'");
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }   
                    }
                    else
                    {
                        db.DeleteOnTable(table, "jq_code='" + code + "'");
                        db.DeleteOnTable(tableln, "jq_code='" + code + "'");

                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    notificationText = "has updated: ";

                    col = "jq_date='" + jq_date + "',t_date='" + t_date + "', t_time='" + t_time + "', customer=$$" + customer + "$$, cust_code=$$" + cust_code + "$$, vehicle=$$" + vehicle + "$$, vin_no=$$" + vin_no + "$$, vplate_no=$$" + vplate_no + "$$, est_dayscomp=$$" + est_dayscomp + "$$, total_ln_amnt=$$" + total_ln_amnt + "$$";


                    if (db.UpdateOnTable(table, col, "jq_code='" + code + "'"))
                    {
                        notifyadd = add_items(tableln, code, t_date);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "PURCHASE REQUEST");
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
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }

                if (success)
                {
                    disp_list();
                    goto_win1();
                    frm_clear();
                }
            }
        }

        private String add_items(String tableln, String code, String i_date_need)
        {
            String notificationText = null;
            try
            {
                String i_lnno = "", i_itemcode = "", i_itemdesc = "", i_qty = "", i_costunitid = "", i_costprice = "", i_lnamnt = "", i_notes, i_part_no = "";
                String val2 = "";
                String col2 = "jq_code, ln_num, item_code, item_desc, purc_unit, quantity, cost_price, price, date_need, addl_desc, part_no";

                //db.DeleteOnTable(tableln, "jq_code='" + code + "'");
                db.UpdateOnTable(tableln, "cancel='Y'", "jq_code='" + code + "'");
                for (int r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
                {
                    i_lnno = dgv_itemlist["dgvi_lnno", r].Value.ToString();
                    i_part_no = dgv_itemlist["dgvi_part_no", r].Value.ToString();
                    i_itemcode = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                    i_itemdesc = dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
                    i_qty = gm.toDoubleStr(dgv_itemlist["dgvi_qty", r].Value.ToString());
                    i_costprice = gm.toDoubleStr(dgv_itemlist["dgvi_costprice", r].Value.ToString());
                    i_lnamnt = gm.toDoubleStr(dgv_itemlist["dgvi_lnamnt", r].Value.ToString());
                    i_costunitid = dgv_itemlist["dgvi_costunitid", r].Value.ToString();
                    i_notes = dgv_itemlist["dgvi_notes", r].Value.ToString();
                    //cost_price
                    val2 = "'" + code + "', '" + i_lnno + "', '" + i_itemcode + "', " + db.str_E(i_itemdesc) + ",  '" + i_costunitid + "', '" + i_qty + "', '" + i_costprice + "','" + i_lnamnt + "', '" + i_date_need + "', " + db.str_E(i_notes) + ",'" + i_part_no + "'";

                    if (db.InsertOnTable(tableln, col2, val2))
                    {
                        notificationText += Environment.NewLine + i_itemdesc;
                    }
                    else
                    {
                        notificationText = null;
                    }
                }
                db.DeleteOnTable(tableln, "jq_code='" + code + "' AND cancel='Y'");
            }
            catch {
                db.DeleteOnTable(tableln, "jq_code='" + code + "' AND cancel<>'Y'");
                db.UpdateOnTable(tableln, "cancel=''", "jq_code='" + code + "'");
            }
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
            int r = 0;
            isnew_item = false;

            try
            {
                if (dgv_itemlist.Rows.Count >= 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    _frm_ipr = new z_enter_item_simple(this, isnew_item, r);
                    _frm_ipr.ShowDialog();
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

                    if (isnew == false)
                    {
                        code = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                        qty = dgv_itemlist["dgvi_oldqty", r].Value.ToString();
                        dgv_delitem.Rows.Add(code, qty);
                    }

                    dgv_itemlist.Rows.RemoveAt(r);

                    disp_total();
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

        private void btn_customer_Click(object sender, EventArgs e)
        {

            m_auto_customer frm = new m_auto_customer(this, true);
            frm.ShowDialog(); 
        }

        private void btn_vehicle_Click(object sender, EventArgs e)
        {

            m_vehiclec_info frm = new m_vehiclec_info(this, true);
            frm.ShowDialog();
        }


        public void set_custvalue_frm(String custcode)
        {
            try { cbo_customer.Items.Clear(); }
            catch { }

            //dsdadsa
            gc.load_crm(cbo_customer);
            cbo_customer.SelectedValue = custcode;
        }

        private void cbo_customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            String val = "";
            DataTable dt = null;
            try
            {
                dt = db.QueryBySQLCode("SELECT d_addr2,d_cntc_no,d_email FROM rssys.m06 WHERE d_code='" + cbo_customer.SelectedValue.ToString() + "'");
            }
            catch { }
            //MessageBox.Show(dt.Rows.Count.ToString());
            try
            {
                if (dt.Rows.Count != 0)
                {
                    rtxt_address.Text = dt.Rows[0]["d_addr2"].ToString();
                    txt_contact.Text = dt.Rows[0]["d_cntc_no"].ToString();
                }
            }
            catch { }
        }

        public void set_vehicle_frm(String custcode, String custname)
        {

            try { cbo_vehicle.Items.Clear(); }
            catch { }

            gc.load_vehicle_info(cbo_vehicle);
            //gc.load_customer(cbo_carunit);
            cbo_vehicle.SelectedValue = custcode;
        }

        private void cbo_vehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            vehicle_info_entry();
        }


        private void vehicle_info_entry()
        {
            DataTable dt = null;
            try
            {
                dt = db.QueryBySQLCode("SELECT vin_no, plate_no FROM rssys.vehicle_info WHERE vin_no='" + cbo_vehicle.SelectedValue.ToString() + "'");
            }
            catch { }
            //MessageBox.Show(dt.Rows.Count.ToString());
            try
            {
                if (dt.Rows.Count != 0)
                {
                    txt_vin.Text = dt.Rows[0]["vin_no"].ToString();
                    txt_plateno.Text = dt.Rows[0]["plate_no"].ToString();
                }
            }
            catch { }
        }
    }
}
