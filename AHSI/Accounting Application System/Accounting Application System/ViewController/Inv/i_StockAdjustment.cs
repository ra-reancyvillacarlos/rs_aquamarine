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
    public partial class i_StockAdjustment : Form
    {
        private z_enter_item_simple _frm_eis;
        DateTime dt_to;
        DateTime dt_frm;
        dbInv db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        int lnno_last = 1;
        String stk_trns_type = "A";
        String codepri = "";
        String trns_type = "A";
        int lastpage = 0;
        int limit_val = 99999;
        int offset = 0;
        
        Boolean isReady = false;
        Boolean bgworker_cancel = false;

        public i_StockAdjustment()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbInv();
            // btn_vpage.Text = "1";
            gc.load_account_title(cbo_contraacct);
            gc.load_costcenter(cbo_costcenter);
            gc.load_stocklocation(cbo_stocklocation);

            //cbo_search.Items.Add("DESCRIPTION");
            //cbo_search.Items.Add("ADJUSTMENT NUMBER");

            dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));

            disp_list();
            isReady = true; 
        }


        private void i_StockAdjustment_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void frm_clear()
        {
            try
            {
                txt_code.Text = "";
                txt_desc.Text = "";
                cbo_contraacct.SelectedIndex = -1;
                cbo_stocklocation.SelectedIndex = -1;
                cbo_costcenter.SelectedIndex = -1;
                dtp_trnxdt.Value = Convert.ToDateTime(db.get_systemdate(""));
                lbl_total.Text = "0.00";
                // cbo_perpage.SelectedItem = "25";
                dgv_itemlist.Rows.Clear();
                lbl_progress.Visible = false;
                progressBar1.Value = 0;
                // dgv_itemlist.Rows.Clear();
            }
            catch (Exception) { }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            goto_win2();
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "", whscode = "", cccode = "", atcode = "";
            isnew = false;

            try
            {
                r = dgv_list.CurrentRow.Index;
                code = (dgv_list["dgvl_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    txt_code.Text = code;

                    if (dgv_list["dgvl_description", r].Value != null)
                    {
                        txt_desc.Text = dgv_list["dgvl_description", r].Value.ToString();
                    }

                    if (dgv_list["dgvl_whscode", r].Value != null)
                    {
                        whscode = dgv_list["dgvl_whscode", r].Value.ToString();
                        cbo_stocklocation.SelectedValue = whscode;
                    }

                    if (dgv_list["dgvl_cccode", r].Value != null)
                    {
                        cccode = dgv_list["dgvl_cccode", r].Value.ToString();
                        cbo_costcenter.SelectedValue = cccode;
                    }

                    //cbo_contraacct.SelectedValue = dgv_list["", r].Value.ToString();

                    dtp_trnxdt.Value = Convert.ToDateTime(dgv_list["dgvl_trnxdate", r].Value.ToString());
                    this.codepri = code;
                    disp_itemlist(code);
                    disp_total();
                    goto_win2();

                }
                else
                {
                    MessageBox.Show("No invoice selected.");
                }
            }
            catch
            {

            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DataTable dt;
            String code = "", itemcode = "", itemqty = "0.00";
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
                    else if ((dgv_list["dgvl_jrnlz", r].Value ?? "").ToString() == "Y")
                    {
                        MessageBox.Show("Selected transaction already journalize.\nCannot be cancelled.");
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            code = dgv_list["dgvl_code", r].Value.ToString();

                            if (db.UpdateOnTable("rechdr", "_reference='CANCELLED' || '-' ||_reference, cancel='Y'", "rec_num='" + code + "'"))
                            {
                                db.DeleteOnTable("reclne", "rec_num='" + code + "'");
                                db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

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
            if (dgv_list.Rows.Count > 0)
            {
                int r = dgv_list.CurrentRow.Index;
                String adjust_no = (dgv_list["dgvl_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(adjust_no))
                {
                    DataTable dt = db.QueryBySQLCode("SELECT r.*, i.unit_desc FROM " + db.schema + ".reclne r LEFT JOIN " + db.schema + ".itmunit i ON r.unit=i.unit_id WHERE rec_num='" + adjust_no + "' ORDER BY " + db.castToInteger("ln_num") + " LIMIT 1");

                    String adjust_desc = dgv_list["dgvl_description", r].Value.ToString()
                          , t_date = dgv_list["dgvl_systemdate", r].Value.ToString()
                          , t_time = dgv_list["dgvl_systemtime", r].Value.ToString()
                          , stk_loc = dgv_list["dgvl_location", r].Value.ToString()
                          , caccnt = ""
                          , ccenter = "";

                    if (String.IsNullOrEmpty(dt.Rows[0]["cht_code"].ToString()) == false)
                    {
                        cbo_contraacct.SelectedValue = dt.Rows[0]["cht_code"].ToString();
                        caccnt = cbo_contraacct.Text;
                    }
                    if (String.IsNullOrEmpty(dt.Rows[0]["cnt_code"].ToString()) == false)
                    {
                        cbo_costcenter.SelectedValue = dt.Rows[0]["cnt_code"].ToString();
                        ccenter = cbo_costcenter.Text;
                    }

                    Report rpt = new Report();
                    rpt.print_stock_adjustment(adjust_no, adjust_desc, stk_loc, caccnt, ccenter, gm.toDateString(t_date, ""), t_time);
                    rpt.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No Trans item selected.");
            }
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
            thisDatabase db = new thisDatabase();

            String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
            String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");

            DataTable dt;

            dgv_list.Rows.Clear();
            // dt = db.QueryOnTableWithParams("rechdr", "*", "cancel='N' AND trn_type = 'A'", "ORDER BY rec_num ASC");
            dt = db.QueryBySQLCode("SELECT r.*, w.* FROM rssys.rechdr as r LEFT JOIN rssys.whouse w ON w.whs_code = r.whs_code WHERE (COALESCE(r.cancel,'')='N' OR COALESCE(r.cancel,'')='') AND r.trn_type= 'A' AND r.t_date between " + "'" + dateFrom + "' AND '" + dateTo + "' ORDER BY r.rec_num DESC");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int r = dgv_list.Rows.Add();
                DataGridViewRow row = dgv_list.Rows[r];

                row.Cells["dgvl_code"].Value = dt.Rows[i]["rec_num"].ToString();
                row.Cells["dgvl_description"].Value = dt.Rows[i]["_reference"].ToString();
                row.Cells["dgvl_trnxdate"].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                row.Cells["dgvl_location"].Value = dt.Rows[i]["whs_desc"].ToString();
                row.Cells["dgvl_whscode"].Value = dt.Rows[i]["whs_code"].ToString();
                row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                row.Cells["dgvl_userid"].Value = dt.Rows[i]["printed"].ToString();
                row.Cells["dgvl_systemdate"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                row.Cells["dgvl_systemtime"].Value = dt.Rows[i]["t_time"].ToString();
                row.Cells["dgvl_trn_type"].Value = dt.Rows[i]["trn_type"].ToString();
                //row.Cells["dgvl_locationTo"].Value = dt.Rows[i]["locationTo"].ToString();
                //dgvl_code dgvl_description dgvl_location dgvl_systemdate dgvl_systemtime ,dgvl_ccname
            }
            /*DataTable dt = db.get_stkinvlist(dt_frm, dt_to, trns_type);
            try
            {
                dgv_list.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_list.Rows.Add();

                    dgv_list["dgvl_code", i].Value = dt.Rows[i]["rec_num"].ToString();
                    dgv_list["dgvl_description", i].Value = dt.Rows[i]["_reference"].ToString();
                    dgv_list["dgvl_trnxdate", i].Value = dt.Rows[i]["trnx_date"].ToString();
                    dgv_list["dgvl_location", i].Value = dt.Rows[i]["whs_code"].ToString();
                    dgv_list["dgvl_whscode", i].Value = dt.Rows[i]["whs_code"].ToString();
                    dgv_list["dgvl_jrnlz", i].Value = dt.Rows[i]["jrnlz"].ToString();
                    dgv_list["dgvl_cancel", i].Value = dt.Rows[i]["cancel"].ToString();
                    dgv_list["dgvl_userid", i].Value = dt.Rows[i]["printed"].ToString();
                    dgv_list["dgvl_systemdate", i].Value = dt.Rows[i]["t_date"].ToString();
                    dgv_list["dgvl_systemtime", i].Value = dt.Rows[i]["t_time"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }*/
        }

        private void disp_itemlist(String inv_num)
        {
            //Double pagelength = 0, perpage = Convert.ToInt32(gm.toNormalDoubleFormat(cbo_perpage.Text));
            //int page = Convert.ToInt32(gm.toNormalDoubleFormat(btn_vpage.Text));
            DataTable dt;

            try
            {
                dt = db.get_stkinv_item_list(inv_num);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["dgvi_ln", i].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    dgv_itemlist["dgvi_unitid", i].Value = dt.Rows[i]["unit"].ToString();
                    dgv_itemlist["dgvi_unitdesc", i].Value = dt.Rows[i]["unit_desc"].ToString();
                    dgv_itemlist["dgvi_actual_qty", i].Value = gm.toAccountingFormat(dt.Rows[i]["actual_qty"].ToString());
                    dgv_itemlist["dgvi_adjusted_qty", i].Value = gm.toAccountingFormat(dt.Rows[i]["actual_qty"].ToString());

                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["recv_qty"].ToString();
                    dgv_itemlist["dgvi_price", i].Value = dt.Rows[i]["price"].ToString();
                    dgv_itemlist["dgvi_lnamt", i].Value = dt.Rows[i]["ln_amnt"].ToString();
                    dgv_itemlist["dgvi_notes", i].Value = dt.Rows[i]["notes"].ToString();

                    if (dt.Rows[i]["active"].ToString() == "T")
                    {
                        dgv_itemlist["dgvi_active", i].Value = true;
                    }
                    else
                    {
                        dgv_itemlist["dgvi_active", i].Value = false;
                    }

                    if (isnew == false)
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["org_qty"].ToString();
                    }
                    else
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = "0.00";
                    }

                    if (String.IsNullOrEmpty(dt.Rows[i]["cht_code"].ToString()) == false)
                    {
                        cbo_contraacct.SelectedValue = dt.Rows[i]["cht_code"].ToString();
                    }
                    if (String.IsNullOrEmpty(dt.Rows[i]["cnt_code"].ToString()) == false)
                    {
                        cbo_costcenter.SelectedValue = dt.Rows[i]["cnt_code"].ToString();
                    }

                }

            }
            catch (Exception er) { MessageBox.Show(er.Message); }



        }

        public void disp_total()
        {
            Double total = 0.00;

            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count - 1; i++)
                {
                    total += gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnamt", i].Value.ToString());
                }
            }
            catch (Exception) { }

            lbl_total.Text = gm.toAccountingFormat(total);
        }

        private void btn_itemadd_Click(object sender, EventArgs e)
        {
            isnew_item = true;
            int lastrow = 0;

            try
            {
                if (isnew == false)
                {
                    lastrow = dgv_itemlist.Rows.Count - 2;
                    lnno_last = int.Parse(dgv_itemlist["dgvi_ln", lastrow].Value.ToString());
                    inc_lnno();
                }
                if (dgv_itemlist.Rows.Count <= 1)
                {
                    lnno_last = 1;
                }
            }
            catch { }

            _frm_eis = new z_enter_item_simple(this, isnew_item, lnno_last);
            _frm_eis.ShowDialog();
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            int r = 0;
            isnew_item = false;

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    _frm_eis = new z_enter_item_simple(this, isnew_item, r);
                    _frm_eis.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch { MessageBox.Show("No item selected."); }
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

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove?", "Cancel", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        if (isnew == false)
                        {
                            code = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                            qty = dgv_itemlist["dgvi_oldqty", r].Value.ToString();
                            dgv_itemlist.Rows.Add(code, qty);
                        }

                        dgv_itemlist.Rows.RemoveAt(r);
                        disp_total();
                    }
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
            thisDatabase db = new thisDatabase();
            Boolean success = false;
            String notificationText = "";
            z_Notification notify = new z_Notification();
            String code, supp_id, supp_name, terms, loc, dt_trnx, reference, vat_code, purc_ord, at_code, cc_code, debt_code, stk_ref = "", stk_po_so = "", active = "";

            String col = "", val = "";
            String notifyadd = "";
            String table = "rechdr";
            String tableln = "reclne";

            try
            {
                if (String.IsNullOrEmpty(txt_desc.Text))
                {
                    MessageBox.Show("Please select the description field.");
                }
                else if (cbo_contraacct.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select the contra account field.");
                }
                else if (cbo_stocklocation.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select stock location field.");
                }
                else if (cbo_costcenter.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select cost center field.");
                }
                else
                {


                    code = txt_code.Text;
                    at_code = cbo_contraacct.SelectedValue.ToString();
                    loc = cbo_stocklocation.SelectedValue.ToString();
                    cc_code = cbo_costcenter.SelectedValue.ToString();
                    dt_trnx = dtp_trnxdt.Value.ToString("yyyy-MM-dd");
                    reference = txt_desc.Text;
                    stk_ref = stk_trns_type + "#" + code;

                    if (isnew)
                    {
                        notificationText = "has added: ";

                        code = db.get_pk("adj_num");
                        col = "rec_num, _reference, trnx_date, recipient, t_date, t_time, trn_type,whs_code, cancel";
                        val = "'" + code + "', '"
                            + reference + "', '"
                            + dt_trnx + "', '"
                            + GlobalClass.username + "', '"
                            + db.get_systemdate("") + "', '"
                            + db.get_systemtime() + "', '"
                            + trns_type + "', '"
                            + loc + "', 'N'";

                        if (db.InsertOnTable(table, col, val))
                        {
                            notifyadd = add_items(tableln, code, dt_trnx, stk_ref, stk_po_so, loc, at_code, cc_code);
                            lbl_progress.Visible = true;
                            if (String.IsNullOrEmpty(notifyadd) == false)
                            {
                                notificationText += notifyadd;
                                notificationText += Environment.NewLine + " with #" + code;
                                notify.saveNotification(notificationText, "STOCK ADJUSTMENT");
                                db.set_pkm99("adj_num", db.get_nextincrementlimitchar(code, 8));
                                success = true;
                            }
                            else
                            {
                                success = false;
                            }
                        }

                        if (success == false)
                        {
                            db.DeleteOnTable(table, "rec_num='" + code + "'");
                            db.DeleteOnTable(tableln, "rec_num='" + code + "'");
                            db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        notificationText = "has updated: ";
                        col = "rec_num='" + code + "', _reference='" + reference + "', trnx_date='" + dt_trnx + "', recipient='" + GlobalClass.username + "', t_date='" + db.get_systemdate("") + "', t_time='" + db.get_systemtime() + "', whs_code='" + loc + "', trn_type='" + trns_type + "', cancel='N'";

                        if (db.UpdateOnTable(table, col, "rec_num='" + code + "'"))
                        {
                            lbl_progress.Visible = true;
                            db.DeleteOnTable(tableln, "rec_num='" + code + "'");
                            db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                            notifyadd = add_items(tableln, code, dt_trnx, stk_ref, stk_po_so, loc, at_code, cc_code);

                            if (String.IsNullOrEmpty(notifyadd) == false)
                            {
                                notificationText += notifyadd;
                                notificationText += Environment.NewLine + " with #" + code;
                                notify.saveNotification(notificationText, "STOCK ADJUSTMENT");
                                success = true;
                            }
                            else
                            {
                                success = false;
                            }
                        }

                        if (success == false)
                        {
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
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private String add_items(String tableln, String code, String dt_trnx, String stk_ref, String stk_po_so, String loc, String at_code, String cc_code)
        {
            lbl_progress.Visible = true;
            String max = dgv_itemlist.RowCount.ToString();
            String notificationText = null;
            String i_lnno = "", i_itemcode = "", i_itemdesc = "", i_qty = "", i_unitid = "", i_price = "", i_lnamnt = "", i_lnvat = "", i_expiry = "", i_lotnum = "", i_fcp = "0.00", i_part_no = "", i_notes = "", active = "";
            String val2 = "";
            String col2 = "rec_num, ln_num, item_code, item_desc, unit, recv_qty, price, ln_amnt, cht_code, cnt_code,part_no,notes,active,actual_qty";
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";
            Double actual_qty = 0.00, adjusted_qty = 0.00;
            String total_item = "";
            int current_saved = 0;
            try
            {
                progressBar1.Maximum = dgv_itemlist.Rows.Count - 1;
                progressBar1.Minimum = 0;
                //lbl_progress.Text = (dgv_itemlist.Rows.Count -1).ToString();
                for (int r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
                {
                    i_lnno = dgv_itemlist["dgvi_ln", r].Value.ToString();
                    i_part_no = dgv_itemlist["dgvi_part_no", r].Value.ToString();
                    i_itemcode = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                    i_itemdesc = dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
                    i_notes = (dgv_itemlist["dgvi_notes", r].Value ?? "").ToString();

                    actual_qty = gm.toNormalDoubleFormat((dgv_itemlist["dgvi_actual_qty", r].Value ?? 0.00).ToString());
                    adjusted_qty = gm.toNormalDoubleFormat((dgv_itemlist["dgvi_adjusted_qty", r].Value ?? 0.00).ToString());
                    if (actual_qty != adjusted_qty)
                    {
                        i_qty = (adjusted_qty - actual_qty).ToString("0.00");
                    }
                    else
                    {
                        i_qty = gm.toNormalDoubleFormat((dgv_itemlist["dgvi_qty", r].Value ?? 0.00).ToString()).ToString("0.00");
                    }


                    //MessageBox.Show(i_qty);

                    i_unitid = dgv_itemlist["dgvi_unitid", r].Value.ToString();
                    i_price = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_price", r].Value.ToString()).ToString("0.00");
                    i_lnamnt = gm.toNormalDoubleFormat((dgv_itemlist["dgvi_lnamt", r].Value ?? "0.00").ToString()).ToString("0.00");
                    i_fcp = db.get_item_fcp(i_itemcode).ToString("0.00");
                    if (dgv_itemlist["dgvi_active", r].Value.ToString() == "True")
                    {
                        active = "T";
                    }
                    else
                    {
                        active = "F";
                    }
               

                    val2 = "'" + code + "', '" + i_lnno + "', '" + i_itemcode + "', " + db.str_E(i_itemdesc) + ", '" + i_unitid + "','" + i_qty + "', '" + i_price + "', '" + i_lnamnt + "', '" + at_code + "', '" + cc_code + "','" + i_part_no + "','" + i_notes + "','" + active + "','" + actual_qty + "'";

                    if (db.InsertOnTable(tableln, col2, val2))
                    {
                        stk_qty_in = "0.00";
                        stk_qty_out = "0.00";
                        progressBar1.Value = r;
                        //lbl_current.Text = r.ToString() ;
                        if (gm.toNormalDoubleFormat(i_qty) < 0)
                            stk_qty_out = Math.Abs(gm.toNormalDoubleFormat(i_qty)).ToString();
                        else
                            stk_qty_in = i_qty;

                        db.save_to_stkcard(i_itemcode, i_itemdesc, i_unitid, dt_trnx, stk_ref, stk_po_so, stk_qty_in, stk_qty_out, i_fcp, i_price, loc, "", "", stk_trns_type, at_code, cc_code, "");
                        //lbl_current.Text = r.ToString();
                        //lbl_max.Text = max;
                        notificationText += Environment.NewLine + i_itemdesc;

                    }

                    //else
                    //{
                    //    notificationText = null;
                    //}
                    total_item = (dgv_itemlist.Rows.Count - 1).ToString();
                }
                notificationText = total_item + " total items";
            }
            catch /*(Exception er)*/ { /*MessageBox.Show(er.Message);*/ }

            return notificationText;
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            goto_win1();
            frm_clear();
        }

        private void btn_itemsave_Click(object sender, EventArgs e)
        {
            goto_win2();
        }

        private void btn_itemclose_Click(object sender, EventArgs e)
        {
            goto_win2();
        }

        private void goto_win1()
        {
            seltbp = true;
            tbcntrl_left2.SelectedTab = tpg_option_1;
            tpg_option_1.Show();

            tbcntrl_main.SelectedTab = tpg_right_list;
            tpg_right_list.Show();
            seltbp = false;
        }

        private void goto_win2()
        {
            seltbp = true;
            tbcntrl_left2.SelectedTab = tpg_option_2;
            tpg_option_2.Show();

            tbcntrl_main.SelectedTab = tpg_right_entry;
            tpg_right_entry.Show();
            seltbp = false;
        }

        private void inc_lnno()
        {
            lnno_last = lnno_last + 1;
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

                dgv_itemlist["dgvi_ln", i].Value = dt.Rows[0]["dgvi_ln"].ToString();
                dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[0]["dgvi_part_no"].ToString();
                dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[0]["dgvi_itemcode"].ToString();
                dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[0]["dgvi_itemdesc"].ToString();
                dgv_itemlist["dgvi_unitdesc", i].Value = dt.Rows[0]["dgvi_unitdesc"].ToString();
                dgv_itemlist["dgvi_unitid", i].Value = dt.Rows[0]["dgvi_unitid"].ToString();
                dgv_itemlist["dgvi_qty", i].Value = dt.Rows[0]["dgvi_qty"].ToString();
                dgv_itemlist["dgvi_price", i].Value = dt.Rows[0]["dgvi_price"].ToString();
                dgv_itemlist["dgvi_lnamt", i].Value = dt.Rows[0]["dgvi_lnamt"].ToString();
                dgv_itemlist["dgvi_notes", i].Value = dt.Rows[0]["dgvi_notes"].ToString();
                dgv_itemlist["dgvi_active", i].Value = true;

                if (isnew_item)
                {
                    inc_lnno();
                }
            }
            catch (Exception) { }

            disp_total();
        }

        public String get_dgvi_ln(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_ln", currow].Value.ToString();

            return val;
        }

        public String get_partno(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_part_no", currow].Value.ToString();

            return val;
        }
        public String get_dgvi_remark(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_notes", currow].Value.ToString();

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

            val = dgv_itemlist["dgvi_unitid", currow].Value.ToString();

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

            val = dgv_itemlist["dgvi_price", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_lnamt(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnamt", currow].Value.ToString();

            return val;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_list();
            /*
            thisDatabase db = new thisDatabase();

            String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
            String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");

            DataTable dt;

            dgv_list.Rows.Clear();
            // dt = db.QueryOnTableWithParams("rechdr", "*", "cancel='N' AND trn_type = 'A'", "ORDER BY rec_num ASC");
            if (cbo_search.Text == string.Empty)
            {
                dt = db.QueryBySQLCode("SELECT r.*, w.* FROM rssys.rechdr as r LEFT JOIN rssys.whouse w ON w.whs_code = r.whs_code WHERE rec_num LIKE '" + txt_search.Text + "%' OR _reference LIKE '" + txt_search.Text + "%' AND coalesce(r.cancel,'')<>'N' AND r.trn_type= 'A' AND r.t_date between " + "'" + dateFrom + "' AND '" + dateTo + "' ORDER BY r.rec_num ASC");

            }
            else if (cbo_search.Text == "clear filter")
            {
                dt = db.QueryBySQLCode("SELECT r.*, w.* FROM rssys.rechdr as r LEFT JOIN rssys.whouse w ON w.whs_code = r.whs_code WHERE coalesce(r.cancel,'')<>'N' AND r.trn_type= 'A' AND r.t_date between " + "'" + dateFrom + "' AND '" + dateTo + "' ORDER BY r.rec_num ASC");

            }
            else
            {
                String ss = "SELECT r.*, w.* FROM rssys.rechdr as r LEFT JOIN rssys.whouse w ON w.whs_code = r.whs_code WHERE " + cbo_search.Text + " LIKE '" + txt_search.Text + "%' AND coalesce(r.cancel,'')<>'N'' AND r.trn_type= 'A' AND r.t_date between " + "'" + dateFrom + "' AND '" + dateTo + "' ORDER BY r.rec_num ASC";
                dt = db.QueryBySQLCode("SELECT r.*, w.* FROM rssys.rechdr as r LEFT JOIN rssys.whouse w ON w.whs_code = r.whs_code WHERE " + cbo_search.Text + " LIKE '" + txt_search.Text + "%' AND coalesce(r.cancel,'')<>'N' AND r.trn_type= 'A' AND r.t_date between " + "'" + dateFrom + "' AND '" + dateTo + "' ORDER BY r.rec_num ASC");

            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int r = dgv_list.Rows.Add();
                DataGridViewRow row = dgv_list.Rows[r];

                row.Cells["dgvl_code"].Value = dt.Rows[i]["rec_num"].ToString();
                row.Cells["dgvl_description"].Value = dt.Rows[i]["_reference"].ToString();
                row.Cells["dgvl_trnxdate"].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                row.Cells["dgvl_location"].Value = dt.Rows[i]["whs_desc"].ToString();
                row.Cells["dgvl_whscode"].Value = dt.Rows[i]["whs_code"].ToString();
                row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                row.Cells["dgvl_userid"].Value = dt.Rows[i]["printed"].ToString();
                row.Cells["dgvl_systemdate"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                row.Cells["dgvl_systemtime"].Value = dt.Rows[i]["t_time"].ToString();
                row.Cells["dgvl_trn_type"].Value = dt.Rows[i]["trn_type"].ToString();
                //row.Cells["dgvl_locationTo"].Value = dt.Rows[i]["locationTo"].ToString();
                //dgvl_code dgvl_description dgvl_location dgvl_systemdate dgvl_systemtime ,dgvl_ccname
            }*/
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_left2_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void cbo_stocklocation_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void txt_searchtxt_TextChanged(object sender, EventArgs e)
        {
            //disp_dgv_search(txt_searchtxt.Text);
        }
        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            // disp_dgv_search(txt_searchtxt.Text);
        }

        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "dgvl_description";

            try
            {
                searchValue = txt_search.Text.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "dgvl_code";
                }
                else
                {
                    typname = "dgvl_description";
                }
                dgv_list.ClearSelection();
                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue)).First();

                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
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
            String r = dgv_itemlist.RowCount.ToString();
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

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void btn_pagefirst_Click(object sender, EventArgs e)
        {
            //btn_vpage.Text = "1";
            //btn_pageprev.Enabled = false;
            //btn_pagefirst.Enabled = false;
            //btn_pagenext.Enabled = true;
            //btn_pagelast.Enabled = true;
            //if(codepri!=string.Empty)
            //{ 
            ////disp_itemlist(codepri);
            //}
            ////disp_list();
        }

        private void btn_pageprev_Click(object sender, EventArgs e)
        {
            // int page = Convert.ToInt32(gm.toNormalDoubleFormat(btn_vpage.Text)) - 1;
            // if (page <= 1)
            // {
            //     page = 1;
            //     btn_pageprev.Enabled = false;
            //     btn_pagefirst.Enabled = false;
            // }
            // else
            // {
            //     btn_pagenext.Enabled = true;
            //     btn_pagelast.Enabled = true;
            //     btn_pageprev.Enabled = true;
            //     btn_pagefirst.Enabled = true;
            // }
            // btn_vpage.Text = page.ToString();
            // if (codepri != string.Empty)
            // {
            //    // disp_itemlist(codepri);
            // }
            //// disp_list();
        }

        private void btn_vpage_Click(object sender, EventArgs e)
        {

        }

        private void btn_pagenext_Click(object sender, EventArgs e)
        {
            //int page = Convert.ToInt32(gm.toNormalDoubleFormat(btn_vpage.Text)) + 1;
            //if (page >= lastpage)
            //{
            //    page = lastpage;
            //    btn_pagenext.Enabled = false;
            //    btn_pagelast.Enabled = false;
            //}
            //else
            //{
            //    btn_pagenext.Enabled = true;
            //    btn_pagelast.Enabled = true;
            //    btn_pageprev.Enabled = true;
            //    btn_pagefirst.Enabled = true;
            //}
            //btn_vpage.Text = page.ToString();
            //if (codepri != string.Empty)
            //{
            //    //disp_itemlist(codepri);
            //}

            // disp_list();
        }

        private void btn_pagelast_Click(object sender, EventArgs e)
        {
            //{
            //    btn_vpage.Text = lastpage.ToString();
            //    btn_pagenext.Enabled = false;
            //    btn_pagelast.Enabled = false;
            //    btn_pageprev.Enabled = true;
            //    btn_pagefirst.Enabled = true;
            //    if (codepri != string.Empty)
            //    {
            //       // disp_itemlist(codepri);
            //    }
            //disp_list();
        }
        private DataTable get_customer_pagination(String codes)
        {
            try
            {
                // //Double pagelength = 0, perpage = Convert.ToInt32(gm.toNormalDoubleFormat(cbo_perpage.Text));
                // //int page = Convert.ToInt32(gm.toNormalDoubleFormat(btn_vpage.Text));
                // //String WHERE = "";
                // //if (!String.IsNullOrEmpty(txt_search.Text))
                // //{
                // //    WHERE = String.Format("WHERE d_name LIKE '%{0}%' OR d_code LIKE '%{0}%' OR lastname LIKE '%{0}%' OR firstname LIKE '%{0}%' OR d_addr2 LIKE '%{0}%' OR mname LIKE '%{0}%' OR d_cntc_no LIKE '%{0}%' OR d_tel LIKE '%{0}%' OR fax LIKE '%{0}%' OR d_email LIKE '%{0}%' OR d_tin LIKE '%{0}%' OR remarks LIKE '%{0}%'", txt_search.Text);
                // //}

                // DataTable dt = db.QueryBySQLCode("SELECT  r.rec_num,( SELECT count(rec_num) from rssys.reclne )as cnt, i.unit_desc FROM rssys.reclne r LEFT JOIN rssys.itmunit i ON r.unit=i.unit_id WHERE rec_num='"+codes+"' LIMIT 1");
                // pagelength = Convert.ToInt32(gm.toNormalDoubleFormat(dt.Rows[0]["cnt"].ToString()));

                // lastpage = Convert.ToInt32(pagelength / perpage);

                // int offset = Convert.ToInt32(perpage) * page;
                // if (offset >= pagelength)
                // {
                //     page = lastpage;
                // }
                // else if (offset <= 0)
                // {
                //     page = 1;
                // }
                // offset = page * Convert.ToInt32(perpage);
                // offset -= (pagelength % perpage) != 0 && page == lastpage ? Convert.ToInt32(pagelength % perpage) : Convert.ToInt32(perpage);

                //// btn_vpage.Text = page.ToString();
                // if (pagelength < 100 && offset != 0 && lastpage == 1)
                // {
                //     offset = 0;
                //     perpage = pagelength;
                // }
                // //return db.QueryBySQLCode(String.Format("SELECT d_name, d_code, lastname, firstname, d_addr2, mname, d_cntc_no, d_tel, fax, d_email, d_tin, remarks FROM rssys.m06 " + WHERE + " ORDER BY d_code ASC OFFSET {0} LIMIT {1} ", offset.ToString(), perpage.ToString()));
                // String r = String.Format("SELECT  r.*, i.unit_desc FROM rssys.reclne r LEFT JOIN rssys.itmunit i ON r.unit=i.unit_id WHERE rec_num='" + codes + "' ORDER BY " + db.castToInteger("ln_num") + "  OFFSET {0} LIMIT {1} ", offset.ToString(), perpage.ToString());

                // return db.QueryBySQLCode(String.Format("SELECT  r.*, i.unit_desc FROM rssys.reclne r LEFT JOIN rssys.itmunit i ON r.unit=i.unit_id WHERE rec_num='" + codes + "' ORDER BY " + db.castToInteger("ln_num") + "  OFFSET {0} LIMIT {1} ", offset.ToString(), perpage.ToString()));
            }
            catch { }
            return null;
        }
        private void cbo_perpage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_loaditems_Click(object sender, EventArgs e)
        {
            //limit_val = 19000;
            //offset = 0;
            disp_itemslist(txt_search.Text);
        }

        private void disp_itemslist(String srch)
        {
            Double disc_pct = 0;
            String _typeOfGroupCat = "";
            String category = "";
            String typ = "";
            String sortby1 = "";
            String branch = GlobalClass.branch;
            int ctr = 0;

            try
            {
                DataTable dt = new DataTable();

                dgv_itemlist.Rows.Clear();

                dt = db.get_itemlist2(srch, _typeOfGroupCat, branch, category, typ, sortby1, limit_val.ToString(), offset.ToString());


                if (dt != null)
                {
                    for (int r = 0; dt.Rows.Count > r; r++)
                    {
                        dgv_itemlist.Rows.Add();
                        ctr = r;
                        dgv_itemlist["dgvi_ln", r].Value = ctr + 1; //
                        //dgv_itemlist["dgvi_qty", r].Value = gm.toAccountingFormat(dt.Rows[r]["qty_onhand_su"].ToString()); //

                        dgv_itemlist["dgvi_qty", r].Value = "0.00"; //



                        dgv_itemlist["dgvi_actual_qty", r].Value = gm.toAccountingFormat(dt.Rows[r]["qty_onhand_su"].ToString()); //

                        dgv_itemlist["dgvi_adjusted_qty", r].Value = gm.toAccountingFormat(dt.Rows[r]["qty_onhand_su"].ToString()); //




                        dgv_itemlist["dgvi_part_no", r].Value = dt.Rows[r]["part_no"].ToString();//
                        dgv_itemlist["dgvi_itemdesc", r].Value = dt.Rows[r]["item_desc"].ToString();//
                        dgv_itemlist["dgvi_unitdesc", r].Value = dt.Rows[r]["sale_unit"].ToString();//
                        //dgv_itemlist["dgvi_cost", r].Value = gm.toAccountingFormat(dt.Rows[r]["unit_cost"].ToString());
                        //dgv_itemlist["dgvi_itemgrp", r].Value = dt.Rows[r]["grp_desc"].ToString();
                        dgv_itemlist["dgvi_price", r].Value = gm.toAccountingFormat(dt.Rows[r]["unit_cost"].ToString()); //
                        //dgv_itemlist["dgvi_senior", r].Value = dt.Rows[r]["senior"].ToString();
                        //dgv_itemlist["dgvi_rack", r].Value = dt.Rows[r]["bin_loc"].ToString();
                        dgv_itemlist["dgvi_unitid", r].Value = dt.Rows[r]["sales_unit_id"].ToString();//
                        dgv_itemlist["dgvi_itemcode", r].Value = dt.Rows[r]["item_code"].ToString(); //
                        //dgv_itemlist["dgvi_brd_code", r].Value = dt.Rows[r]["brd_code"].ToString();
                        //dgv_itemlist["dgvi_brd_desc", r].Value = dt.Rows[r]["brd_name"].ToString();
                        //dgv_itemlist["dgvi_whs_desc", r].Value = dt.Rows[r]["whs_desc"].ToString();
                        //dgv_itemlist["dgvi_whs_code", r].Value = dt.Rows[r]["whs_code"].ToString();
                        //dgv_itemlist["dgvi_branch", r].Value = dt.Rows[r]["branchcode"].ToString();
                        //dgv_itemlist["dgvi_branchname", r].Value = dt.Rows[r]["branchname"].ToString();
                        //dgv_itemlist["dgvi_itemgrpid", r].Value = dt.Rows[r]["item_grp"].ToString();
                        dgv_itemlist["dgvi_active", r].Value = true;//

                    }
                }
            }
            catch /*(Exception er)*/ { /*MessageBox.Show(er.Message);*/ }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

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
                        row.Cells["dgvl_code"].Value = dt.Rows[i]["rec_num"].ToString();
                        row.Cells["dgvl_description"].Value = dt.Rows[i]["_reference"].ToString();
                        row.Cells["dgvl_trnxdate"].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_location"].Value = dt.Rows[i]["whs_desc"].ToString();
                        row.Cells["dgvl_whscode"].Value = dt.Rows[i]["whs_code"].ToString();
                        row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                        row.Cells["dgvl_userid"].Value = dt.Rows[i]["printed"].ToString();
                        row.Cells["dgvl_systemdate"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_systemtime"].Value = dt.Rows[i]["t_time"].ToString();
                        row.Cells["dgvl_trn_type"].Value = dt.Rows[i]["trn_type"].ToString();
                        //row.Cells["dgvl_locationTo"].Value = dt.Rows[i]["locationTo"].ToString();
                        //dgvl_code dgvl_description dgvl_location dgvl_systemdate dgvl_systemtime ,dgvl_ccname
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
            String ORDERBY = "ORDER BY rec_num DESC";

            String search_text = txt_search.getText();
            int selected_index = cbo_search.getSelectedIndex();

            if (selected_index == -1 && !String.IsNullOrEmpty(search_text))
            {
                WHERE = " AND (rec_num LIKE '%" + search_text + "%' OR _reference LIKE '%" + search_text + "%') ";
                if (gm.toNormalDoubleFormat(search_text) != 0)
                {
                    ORDERBY = "ORDER BY _reference, rec_num  DESC";
                }
                else
                {
                    ORDERBY = "ORDER BY rec_num DESC";
                }
            }
            else if (selected_index == 0)
            {
                WHERE = " AND rec_num LIKE '%" + search_text + "%'";
            }
            else if (selected_index == 1)
            {
                WHERE = " AND _reference LIKE '%" + search_text + "%'";
                ORDERBY = "ORDER BY _reference, rec_num  DESC";
            }

            dt = db.QueryBySQLCode("SELECT r.*, w.* FROM rssys.rechdr as r LEFT JOIN rssys.whouse w ON w.whs_code = r.whs_code WHERE (COALESCE(r.cancel,'')='N' OR COALESCE(r.cancel,'')='') AND r.trn_type= 'A' AND r.t_date between " + "'" + dtp_frm.getStringDate() + "' AND '" + dtp_to.getStringDate() + "' " + WHERE + " " + ORDERBY);


            return dt;
        }
    }
}
