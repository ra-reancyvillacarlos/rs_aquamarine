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
    public partial class i_purchaseRequest : Form
    {
        private z_enter_item_simple _frm_ipr;
        dbInv db = null;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        int lnno_last = 1;
        
        Boolean isReady = false;
        Boolean bgworker_cancel = false;

        public i_purchaseRequest()
        {
            InitializeComponent();
            dgv_delitem.Hide();
            lbl_invoice_total.Hide();

            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbInv();

            gc.load_costcenter(cbo_costCenter);

            DateTime temp_dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));

            disp_list();
            isReady = true;    
        }

        void i_purchaseRequest_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void disp_list()
        {

            if (bgworker.IsBusy)
            {
                bgworker_cancel = true;
                try { bgworker.CancelAsync(); }
                catch { }
            }
            else
            {
                bgworker.RunWorkerAsync();
            }

            /*
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
                String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");

                DataTable dt = db.get_purhace_request_bydate(dateFrom, dateTo);
                int i = 0;

                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["dgvl_pr_code"].Value = dt.Rows[r]["pr_code"].ToString();
                        row.Cells["dgvl_purc_ord"].Value = dt.Rows[r]["purc_ord"].ToString();
                        row.Cells["dgvl_reference"].Value = dt.Rows[r]["reference"].ToString();
                        row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(), "");
                        row.Cells["dgvl_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                        row.Cells["dgvl_prdate"].Value = gm.toDateString(dt.Rows[r]["pr_date"].ToString(), "");
                        row.Cells["dgvl_date_need"].Value = dt.Rows[r]["date_need"].ToString();
                        row.Cells["dgvl_cost_center"].Value = dt.Rows[r]["cnt_code"].ToString();
                        row.Cells["dgvl_subCostcentre"].Value = dt.Rows[r]["scc_code"].ToString();
                        row.Cells["dgvl_cc_desc"].Value = dt.Rows[r]["cc_desc"].ToString();
                        row.Cells["dgvl_scc_desc"].Value = dt.Rows[r]["scc_desc"].ToString();
                        row.Cells["dgvl_recipient"].Value = dt.Rows[r]["recipient"].ToString();
                        row.Cells["dgvl_cancel"].Value = dt.Rows[r]["cancel"].ToString();
                        row.Cells["dgvl_requestedby"].Value = dt.Rows[r]["request_by"].ToString();

                        i++;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }*/
        }

        public void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            disp_list();
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

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
                txt_invoiceno.Text = "";
                txt_reference.Text = "";
                cbo_costCenter.SelectedIndex = -1;
                cbo_subcostcenter.SelectedIndex = -1;
                dtp_prDate.Value = Convert.ToDateTime(db.get_systemdate(""));
                dtp_dateNeeded.Value = Convert.ToDateTime(db.get_systemdate(""));
                lbl_invoice_total.Text = "0.00";

                dgv_itemlist.Rows.Clear();
                dgv_delitem.Rows.Clear();
            }
            catch (Exception) { }

            try
            {
                txt_requestedby.Text = GlobalClass.username;
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
            int r = -1;
            String code = "", canc = "";
            isnew = false;

            try
            {
                r = dgv_list.CurrentRow.Index;

                code = (dgv_list["dgvl_pr_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    try
                    {
                        canc = dgv_list["dgvl_cancel", r].Value.ToString();
                    }
                    catch { }

                    if (canc == "Y")
                    {
                        MessageBox.Show("PR# " + code + " is already cancelled. Can not be updated.");
                    }
                    else
                    {
                        try
                        {
                            txt_invoiceno.Text = code;
                            txt_reference.Text = dgv_list["dgvl_reference", r].Value.ToString();
                            dtp_prDate.Value = Convert.ToDateTime(dgv_list["dgvl_prdate", r].Value.ToString());
                            dtp_dateNeeded.Value = Convert.ToDateTime(dgv_list["dgvl_date_need", r].Value.ToString());
                            lbl_invoice_total.Text = dgv_list["dgvl_reference", r].Value.ToString();

                            var temp = dgv_list["dgvl_cost_center", r].Value.ToString();

                            if (!string.IsNullOrEmpty(temp))
                                cbo_costCenter.SelectedValue = dgv_list["dgvl_cost_center", r].Value.ToString();
                            else
                                cbo_costCenter.SelectedIndex = -1;

                            var temp2 = dgv_list["dgvl_subCostcentre", r].Value.ToString();

                            if (!string.IsNullOrEmpty(temp2))
                                cbo_subcostcenter.SelectedValue = dgv_list["dgvl_subCostcentre", r].Value.ToString();
                            else
                                cbo_subcostcenter.SelectedIndex = -1;

                            var temp3 = dgv_list["dgvl_requestedby", r].Value.ToString();

                            if (!string.IsNullOrEmpty(temp3))
                                txt_requestedby.Text = dgv_list["dgvl_requestedby", r].Value.ToString();
                            else
                                txt_requestedby.Text = "";
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
            catch
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void disp_itemlist(string code)
        {
            try
            {
                DataTable dt = db.get_purhace_request_items(code);



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["quantity"].ToString();
                    dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[i]["unit_shortcode"].ToString();
                    dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[i]["purc_unit"].ToString();
                    dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[i]["price"].ToString();
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
            String code = "";
            int r = -1;

            if (dgv_list.Rows.Count > 1)
            {
                try
                {
                    r = dgv_list.CurrentRow.Index;

                    if ((dgv_list["dgvl_cancel", r].Value ?? "").ToString().Equals("Y"))
                    {
                        MessageBox.Show("Invoice already cancelled.");
                    }
                    else
                    {
                        code = dgv_list["dgvl_pr_code", r].Value.ToString();

                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this P.I.# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            if (db.UpdateOnTable("prhdr", "reference='CANCELLED' || '-' ||reference, cancel='Y'", "pr_code='" + code + "'"))
                            {
                                db.DeleteOnTable("prlne", "pr_code='" + code + "'");
                                disp_list();
                            }
                            else
                            {
                                MessageBox.Show("No invoice selected.");
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("No invoice selected.");
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

            _frm_ipr = new z_enter_item_simple(this, isnew_item, lnno_last, false);
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
            String notificationText = "has added: ";
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String code, reference, pr_date, request_by, recipient, cnt_code, scc_code, i_date_need = "";

            String col = "", val = "";
            String notifyadd = "";
            String table = "prhdr";
            String tableln = "prlne";

            if (cbo_costCenter.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the cost center field.");
            }
            else if (cbo_subcostcenter.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the sub cost center field.");
            }
            else if (String.IsNullOrEmpty(txt_reference.Text))
            {
                MessageBox.Show("Please type a reference.");
            }
            else if (dgv_itemlist.Rows.Count <= 1)
            {
                MessageBox.Show("Please enter item(s) to request.");
            }
            else
            {
                code = txt_invoiceno.Text;
                reference = txt_reference.Text;
                pr_date = dtp_prDate.Value.ToString("yyyy-MM-dd");
                request_by = txt_requestedby.Text;
                recipient = GlobalClass.username;
                cnt_code = cbo_costCenter.SelectedValue.ToString();
                scc_code = cbo_subcostcenter.SelectedValue.ToString();
                i_date_need = dtp_dateNeeded.Value.ToString("yyyy-MM-dd");

                if (isnew)
                {
                    code = db.get_pk("pr_code");
                    col = "pr_code, pr_date, reference, cancel, request_by, recipient, t_date, t_time, cnt_code, scc_code";
                    val = "'" + code + "', '" + pr_date + "', " + db.str_E(reference) + ", 'N', '" + request_by + "', '" + recipient + "', '" + db.get_systemdate("") + "', '" + db.get_systemtime() + "', '" + cnt_code + "', '" + scc_code + "'";

                    if (db.InsertOnTable("prhdr", col, val))
                    {
                        notifyadd = add_items(tableln, code, cnt_code, i_date_need);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "PURCHASE REQUEST");

                            code = db.get_nextincrementlimitchar(code, 8);

                            db.set_pkm99("pr_code", code);
                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "pr_code='" + code + "'");
                            db.DeleteOnTable(tableln, "pr_code='" + code + "'");
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        db.DeleteOnTable(table, "pr_code='" + code + "'");
                        db.DeleteOnTable(tableln, "pr_code='" + code + "'");

                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    notificationText = "has updated: ";
                    col = "pr_code='" + code + "', pr_date='" + pr_date + "', reference=" + db.str_E(reference) + ", cancel='N', request_by='" + request_by + "', recipient='" + recipient + "', t_date='" + db.get_systemdate("") + "', t_time='" + db.get_systemtime() + "', cnt_code='" + cnt_code + "', scc_code='" + scc_code + "' ";

                    if (db.UpdateOnTable(table, col, "pr_code='" + code + "'"))
                    {
                        db.DeleteOnTable(tableln, "pr_code='" + code + "'");

                        notifyadd = add_items(tableln, code, cnt_code, i_date_need);

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
            }

            if (success)
            {
                disp_list();
                goto_win1();
                frm_clear();
            }
        }

        private String add_items(String tableln, String code, String cnt_code, String i_date_need)
        {
            String notificationText = null;
            String i_lnno = "", i_itemcode = "", i_itemdesc = "", i_qty = "", i_costunitid = "", i_costprice = "", i_lnamnt = "", i_notes, i_part_no = "";
            String val2 = "";
            String col2 = "pr_code, ln_num, item_code, item_desc, purc_unit, quantity, price, date_need, cnt_code, addl_desc,part_no";

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

                val2 = "'" + code + "', '" + i_lnno + "', '" + i_itemcode + "', " + db.str_E(i_itemdesc) + ",  '" + i_costunitid + "', '" + i_qty + "', '" + i_costprice + "', '" + i_date_need + "', '" + cnt_code + "', " + db.str_E(i_notes) + ",'" + i_part_no + "'";

                if (db.InsertOnTable(tableln, col2, val2))
                {
                    notificationText += Environment.NewLine + i_itemdesc;
                }
                else
                {
                    notificationText = null;
                }
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

                    _frm_ipr = new z_enter_item_simple(this, isnew_item, r, false);
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
            disp_list();
            /*
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
                String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");
                String schema= "rssys";
                DataTable dt = new DataTable();
                if (comboBox1.Text == string.Empty)
                {
                    dt = db.get_purchase_search(textBox15.Text, comboBox1.Text, dateFrom, dateTo);

                }
                else if (comboBox1.Text == "clear filter")
                {
                    dt = db.get_purhace_request_bydate(dateFrom, dateTo);
                
                }
                else {
                    dt = db.get_purchase_search(textBox15.Text, comboBox1.Text, dateFrom , dateTo);
               // dt = db.QueryBySQLCode("SELECT pr.*, c.*, sc.*, (SELECT DISTINCT pl.date_need FROM " + schema + ".prlne pl WHERE pl.pr_code=pr.pr_code) AS date_need, (SELECT DISTINCT pol.purc_ord FROM " + schema + ".purlne pol WHERE pol.pr_code=pr.pr_code) AS purc_ord FROM " + schema + ".prhdr pr LEFT JOIN " + schema + ".m08 c ON pr.cnt_code=c.cc_code LEFT JOIN " + schema + ".subctr sc ON sc.scc_code=pr.scc_code WHERE pr.pr_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "' ORDER BY pr.pr_code");
                }
                int i = 0;

                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["dgvl_pr_code"].Value = dt.Rows[r]["pr_code"].ToString();
                        row.Cells["dgvl_purc_ord"].Value = dt.Rows[r]["purc_ord"].ToString();
                        row.Cells["dgvl_reference"].Value = dt.Rows[r]["reference"].ToString();
                        row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(), "");
                        row.Cells["dgvl_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                        row.Cells["dgvl_prdate"].Value = gm.toDateString(dt.Rows[r]["pr_date"].ToString(), "");
                        row.Cells["dgvl_date_need"].Value = dt.Rows[r]["date_need"].ToString();
                        row.Cells["dgvl_cost_center"].Value = dt.Rows[r]["cnt_code"].ToString();
                        row.Cells["dgvl_subCostcentre"].Value = dt.Rows[r]["scc_code"].ToString();
                        row.Cells["dgvl_cc_desc"].Value = dt.Rows[r]["cc_desc"].ToString();
                        row.Cells["dgvl_scc_desc"].Value = dt.Rows[r]["scc_desc"].ToString();
                        row.Cells["dgvl_recipient"].Value = dt.Rows[r]["recipient"].ToString();
                        row.Cells["dgvl_cancel"].Value = dt.Rows[r]["cancel"].ToString();
                        row.Cells["dgvl_requestedby"].Value = dt.Rows[r]["request_by"].ToString();

                        i++;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }*/
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

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

            int rowIndex = -1;
            String typname = "dgvl_pr_code";

            try
            {
                String searchValue = textBox15.Text.ToUpper();

                if (comboBox1.SelectedIndex == 0)
                {
                    typname = "dgvl_pr_code";
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    typname = "dgvl_purc_ord";
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    typname = "dgvl_reference";
                }

                dgv_list.ClearSelection();
                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue)).First();

                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
        }

        private void cbo_costCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            String cc_code = "";

            try
            {
                if (cbo_costCenter.SelectedIndex != -1)
                {
                    cc_code = cbo_costCenter.SelectedValue.ToString();

                    gc.load_subcostcenter(cbo_subcostcenter, cc_code);
                }
            }
            catch { }
        }

        private void btn_addtextitem_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbo_costCenter.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select Cost Center first.");
                }
                else if(cbo_subcostcenter.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select Subcost Center first.");
                }
                else
                {
                    z_enter_item_simple frm_si = new z_enter_item_simple(this, true, lnno_last, true);
                    frm_si.ShowDialog();
                }
            }
            catch { }            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (bgworker.IsBusy)
            {
                bgworker_cancel = false;
                bgworker.CancelAsync();
            }
        }
        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                dgv_list.Invoke(new Action(() =>
                {
                    dgv_list.Rows.Clear();
                }));
            }
            catch { }
            try
            {
                DataTable dt = get_dgv_list();

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    if (bgworker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    int i = -1;
                    DataGridViewRow row = new DataGridViewRow();
                    dgv_list.Invoke(new Action(() =>
                    {
                        i = dgv_list.Rows.Add();
                        row = dgv_list.Rows[i];
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_pr_code"].Value = dt.Rows[r]["pr_code"].ToString();
                        row.Cells["dgvl_purc_ord"].Value = dt.Rows[r]["purc_ord"].ToString();
                        row.Cells["dgvl_reference"].Value = dt.Rows[r]["reference"].ToString();
                        row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(), "");
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                        row.Cells["dgvl_prdate"].Value = gm.toDateString(dt.Rows[r]["pr_date"].ToString(), "");
                        row.Cells["dgvl_date_need"].Value = dt.Rows[r]["date_need"].ToString();
                        row.Cells["dgvl_cost_center"].Value = dt.Rows[r]["cnt_code"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_subCostcentre"].Value = dt.Rows[r]["scc_code"].ToString();
                        row.Cells["dgvl_cc_desc"].Value = dt.Rows[r]["cc_desc"].ToString();
                        row.Cells["dgvl_scc_desc"].Value = dt.Rows[r]["scc_desc"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_recipient"].Value = dt.Rows[r]["recipient"].ToString();
                        row.Cells["dgvl_cancel"].Value = dt.Rows[r]["cancel"].ToString();
                        row.Cells["dgvl_requestedby"].Value = dt.Rows[r]["request_by"].ToString();
                    }));
                }
                try
                {
                    dgv_list.ClearSelection();
                    dgv_list.Rows[0].Selected = true;
                }
                catch { }

            }
            catch (Exception er) { }
        }

        private void bgworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (bgworker_cancel)
            {
                bgworker.RunWorkerAsync();
                bgworker_cancel = false;
            }
        }
        private DataTable get_dgv_list()
        {
            DataTable dt = new DataTable();
            String WHERE = "";
            String ORDERBY = "ORDER BY pr_code DESC";

            String search_text = textBox15.getText();
            int selected_index = comboBox1.getSelectedIndex();

            if (selected_index == -1 && !String.IsNullOrEmpty(search_text))
            {
                WHERE = " AND (reference LIKE '%" + search_text + "%' OR COALESCE(purc_ord,'') LIKE '%" + search_text + "%' OR pr_code LIKE '%" + search_text + "%') ";
                if (gm.toNormalDoubleFormat(search_text) != 0)
                {
                    ORDERBY = "ORDER BY reference, pr_code  DESC";
                }
                else
                {
                    ORDERBY = "ORDER BY pr_code DESC";
                }
            }
            else if (selected_index == 0)
            {
                WHERE = " AND pr_code LIKE '%" + search_text + "%'";
            }
            else if (selected_index == 1)
            {
                WHERE = " AND COALESCE(purc_ord,'') LIKE '%" + search_text + "%'";
                ORDERBY = "ORDER BY purc_ord, pr_code DESC";
            }
            else if (selected_index == 2)
            {
                WHERE = " AND reference LIKE '%" + search_text + "%'";
                ORDERBY = "ORDER BY reference, pr_code  DESC";
            }

            dt = db.QueryBySQLCode("SELECT * FROM (SELECT pr.*, c.*, sc.*, (SELECT DISTINCT pl.date_need FROM " + db.schema + ".prlne pl WHERE pl.pr_code=pr.pr_code) AS date_need, (SELECT DISTINCT pol.purc_ord FROM " + db.schema + ".purlne pol WHERE pol.pr_code=pr.pr_code) AS purc_ord FROM " + db.schema + ".prhdr pr LEFT JOIN " + db.schema + ".m08 c ON pr.cnt_code=c.cc_code LEFT JOIN " + db.schema + ".subctr sc ON sc.scc_code=pr.scc_code WHERE pr.pr_date BETWEEN " + "'" + dtp_frm.getStringDate() + "' AND '" + dtp_to.getStringDate() + "') res WHERE 1=1 " + WHERE + "  " + ORDERBY);

            return dt;
        }
    }
}
