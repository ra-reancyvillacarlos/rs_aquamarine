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
    public partial class i_DirectPurchase : Form
    {   
        private z_enter_item _frm_ei;
        DateTime dt_to;
        DateTime dt_frm;
        dbInv db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        int lnno_last = 1;
        String stk_trns_type = "P";
        a_CollectionEntry pa;

        Boolean isReady = false;
        Boolean bgworker_cancel = false;

        public i_DirectPurchase()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbInv();

            gc.load_supplier(cbo_suppliername);
            gc.load_modeofpayment(cbo_paymentterms);
            gc.load_stocklocation(cbo_stocklocation);

            dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));

            disp_list();
            isReady = true;     
        }

        private void i_DirectPurchase_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }


        private void frm_clear()
        {
            try
            {
                txt_invoiceno.Text = "";
                txt_reference.Text = "";
                cbo_suppliername.SelectedIndex = -1;
                cbo_stocklocation.SelectedIndex = -1;
                cbo_paymentterms.SelectedIndex = -1;
                dtp_invoicedt.Value = Convert.ToDateTime(db.get_systemdate(""));
                lbl_invoice_total.Text = "0.00";

                dgv_itemlist.Rows.Clear();
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
            String code = "";
            isnew = false;

            try
            {
                r = dgv_list.CurrentRow.Index;
                code = (dgv_list["dgvl_inv_num", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    txt_invoiceno.Text = code;
                    cbo_suppliername.SelectedValue = dgv_list["dgvl_supl_code", r].Value.ToString();
                    txt_reference.Text = dgv_list["dgvl_reference", r].Value.ToString();
                    cbo_stocklocation.SelectedValue = dgv_list["dgvl_whs_code", r].Value.ToString();
                    cbo_paymentterms.SelectedValue = dgv_list["dgvl_pay_code", r].Value.ToString();

                    dtp_invoicedt.Value = Convert.ToDateTime(dgv_list["dgvl_inv_date", r].Value.ToString());

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
                MessageBox.Show("No invoice selected.");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
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
                        code = dgv_list["dgvl_inv_num", r].Value.ToString();

                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this P.I.# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            if (db.UpdateOnTable("pinvhd", "supl_name='CANCELLED' || '-' ||supl_name, cancel='Y'", "inv_num='" + code + "'"))
                            {
                                db.DeleteOnTable("pinvln", "inv_num='" + code + "'");
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

            if (dgv_list.Rows.Count > 1)
            {
                int r = dgv_list.CurrentRow.Index;
                String inv_num = dgv_list["dgvl_inv_num", r].Value.ToString();
                if (!String.IsNullOrEmpty(inv_num))
                {
                    Report rpt = new Report();
                    rpt.print_direct_purchase(inv_num);
                    rpt.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _frm_ei = new z_enter_item(this, isnew_item, lnno_last);
            _frm_ei.ShowDialog();
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            int r = 0;
            int cur_index = 0;
            isnew_item = false;
            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;                    

                    cur_index = int.Parse(dgv_itemlist[0, r].Value.ToString());

                    _frm_ei = new z_enter_item(this, false, cur_index, r);
                    _frm_ei.ShowDialog();
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

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String notifyadd = "";
            String code, supp_id, supp_name, terms, loc, dt_inv, reference, stk_ref = "", stk_po_so = "";
            String i_oldqty = "";
            String di_code = "", di_qty = "0.00";
            String col = "", val = "";
            String trns_type = this.stk_trns_type;
            String notificationText = "";
            String table = "pinvhd";
            String tableln = "pinvln";

            if (cbo_suppliername.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the supplier field.");
            }
            else if (cbo_paymentterms.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the term of payment field.");
            }
            else if (cbo_stocklocation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select stock location.");
            }
            else if (String.IsNullOrEmpty(txt_reference.Text))
            {
                MessageBox.Show("Please type a reference.");
            }
            else
            {
                code = txt_invoiceno.Text;
                supp_id = cbo_suppliername.SelectedValue.ToString();
                supp_name = cbo_suppliername.Text.ToString();
                terms = cbo_paymentterms.SelectedValue.ToString();
                loc = cbo_stocklocation.SelectedValue.ToString();
                dt_inv = dtp_invoicedt.Value.ToString("yyyy-MM-dd");
                reference = txt_reference.Text;
                stk_ref = "PI#" + code;
                notificationText = "has added: ";

                if (isnew)
                {
                    code = db.get_pk("pi_num");
                    col = "inv_num, supl_code, supl_name, reference, inv_date, whs_code, pay_code, trn_type, userid, t_date, t_time, cancel";
                    val = "'" + code + "', '" + supp_id + "', '" + supp_name + "', " + db.str_E(reference) + ", '" + dt_inv + "', '" + loc + "', '" + terms + "', '" + trns_type + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + db.get_systemtime() + "', 'N'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        notifyadd = add_items(tableln, code, dt_inv, stk_ref, stk_po_so, loc, supp_id, supp_name);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "DIRECT PURCHASE");
                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "inv_num='" + code + "'");
                            db.DeleteOnTable(tableln, "inv_num='" + code + "'");
                            db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");
                            success = false;
                        }

                        notificationText += Environment.NewLine + " with #" + code;
                        notify.saveNotification(notificationText, "DIRECT PURCHASE");
                        db.set_pkm99("pi_num", db.get_nextincrementlimitchar(code, 8));
                        success = true;
                    }
                    else
                    {
                        db.DeleteOnTable(table, "inv_num='" + code + "'");
                        db.DeleteOnTable(tableln, "inv_num='" + code + "'");
                        db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "supl_code='" + supp_id + "', supl_name='" + supp_name + "', reference='" + reference + "', inv_date='" + dt_inv + "', whs_code='" + loc + "', pay_code='" + terms + "', trn_type='" + trns_type + "', userid='" + GlobalClass.username + "', t_date='" + db.get_systemdate("") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "'";

                    if (db.UpdateOnTable(table, col, "inv_num='" + code + "'"))
                    {
                        db.DeleteOnTable(tableln, "inv_num='" + code + "'");
                        db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                        notifyadd = add_items(tableln, code, dt_inv, stk_ref, stk_po_so, loc, supp_id, supp_name);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
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

        private String add_items(String tableln, String code, String dt_inv, String stk_ref, String stk_po_so, String loc, String supp_id, String supp_name)
        {
            String notificationText = null;
            String i_lnno = "", i_itemcode = "", i_itemdesc = "", i_vatcode = "", i_qty = "", i_costunitid = "", i_costprice = "", i_discpct = "", i_discamnt = "", i_netprice = "", i_lnamnt = "", i_lnvat = "", i_newregsellprice = "", i_lotnum = "", i_expiry = "", i_cccode = "", i_fcp = "0.00", i_scc_code = "", i_part_no = "" ;
            String val2 = "";
            String col2 = "inv_num, ln_num, item_code, item_desc, unit, inv_qty, price, ln_amnt, ln_vat, lot_no, expiry, disc_pct, disc_amt, netprice, newprice, cc_code, scc_code, vat_code, cancel,part_no";
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";

            for (int r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
            {
                i_lnno = dgv_itemlist["dgvi_lnno", r].Value.ToString();
                i_part_no = dgv_itemlist["dgvi_part_no", r].Value.ToString();
                i_itemcode = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                i_itemdesc = dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
                i_vatcode = dgv_itemlist["dgvi_vatcode", r].Value.ToString();
                i_qty = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_qty", r].Value.ToString()).ToString("0.00");
                i_costunitid = dgv_itemlist["dgvi_costunitid", r].Value.ToString();
                i_costprice = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_costprice", r].Value.ToString()).ToString("0.00");
                i_discpct = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_discpct", r].Value.ToString()).ToString("0.00");
                i_discamnt = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_discamnt", r].Value.ToString()).ToString("0.00");
                i_lnamnt = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnamnt", r].Value.ToString()).ToString("0.00");
                i_lnvat = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnvat", r].Value.ToString()).ToString("0.00");
                i_netprice = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_netprice", r].Value.ToString()).ToString("0.00");
                i_newregsellprice = gc.toNormalDoubleFormat(dgv_itemlist["dgvi_newregsellprice", r].Value.ToString()).ToString();
                i_lotnum = dgv_itemlist["dgvi_lotnum", r].Value.ToString();
                i_expiry = dgv_itemlist["dgvi_expiry", r].Value.ToString();
                i_cccode = dgv_itemlist["dgvi_cccode", r].Value.ToString();
                i_scc_code = dgv_itemlist["dgvi_scc_code", r].Value.ToString();
                i_fcp = gm.toNormalDoubleFormat(db.get_item_fcp(i_itemcode).ToString("0.00")).ToString("0.00");

                if (i_newregsellprice == "")
                {
                    i_newregsellprice = "0.00";
                }

                val2 = "'" + code + "','" 
                    + i_lnno + "', " 
                    + db.str_E(i_itemcode) + "," 
                    + db.str_E(i_itemdesc) + ", '" 
                    + i_costunitid + "', " 
                    + i_qty + ", " 
                    + i_costprice + ", " 
                    + i_lnamnt + ", " 
                    + i_lnvat + ", " 
                    + db.str_E(i_lotnum) + ", '" 
                    + i_expiry + "', " 
                    + i_discpct + "," 
                    + i_discamnt + "," 
                    + i_netprice + "," 
                    + i_newregsellprice + ", '" 
                    + i_cccode + "', '" 
                    + i_scc_code + "', '" 
                    + i_vatcode + "'," 
                    + "'N','" + i_part_no + "'";

                if (db.InsertOnTable(tableln, col2, val2))
                {
                    stk_qty_in = "0.00";
                    stk_qty_out = "0.00";

                    if (gm.toNormalDoubleFormat(i_qty) < 0)
                        stk_qty_out = Math.Abs(gm.toNormalDoubleFormat(i_qty)).ToString();
                    else
                        stk_qty_in = i_qty;

                    db.save_to_stkcard(i_itemcode, i_itemdesc, i_costunitid, dt_inv, stk_ref, stk_po_so, stk_qty_in, stk_qty_out, i_fcp, i_costprice, loc, supp_id, supp_name, stk_trns_type, i_cccode, i_cccode, "");
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

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_list();
            /*
            DataTable dt;

            try
            {
                dgv_list.Rows.Clear();
                thisDatabase db = new thisDatabase();

                String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
                String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");
                if (comboBox1.Text == string.Empty)
                {
                    String ss = "SELECT * FROM rssys.pinvhd  WHERE inv_num LIKE '" + textBox15.Text + "%' OR supl_code LIKE '" + textBox15.Text + "%' OR supl_name LIKE '" + textBox15.Text + "%' OR reference LIKE '" + textBox15.Text + "%' OR recipient LIKE '" + textBox15.Text + "%' OR userid LIKE '" + textBox15.Text + "%' OR prec_num LIKE '" + textBox15.Text + "%'  AND coalesce(cancel,'')<>'N' AND t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY inv_num ASC";
                    dt = db.QueryBySQLCode("SELECT * FROM rssys.pinvhd  WHERE inv_num LIKE '" + textBox15.Text + "%' OR supl_code LIKE '" + textBox15.Text + "%' OR supl_name LIKE '" + textBox15.Text + "%' OR reference LIKE '" + textBox15.Text + "%' OR recipient LIKE '" + textBox15.Text + "%' OR userid LIKE '" + textBox15.Text + "%' OR prec_num LIKE '" + textBox15.Text + "%'  AND coalesce(cancel,'')<>'N' AND t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY inv_num ASC");
                }
                else if(comboBox1.Text =="clear filter")
                {
                    dt = db.QueryOnTableWithParams("pinvhd", "*", "coalesce(cancel,'')<>'N' AND t_date between " + "'" + dateFrom + "' and '" + dateTo + "'", "ORDER BY inv_num ASC");
                
                }
                else{
                    String ss = "SELECT * FROM rssys.pinvhd WHERE " + comboBox1.Text + " LIKE '" + textBox15.Text + "%' AND coalesce(cancel,'')<>'N' AND t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY inv_num ASC";
                    dt = db.QueryBySQLCode("SELECT * FROM rssys.pinvhd WHERE " + comboBox1.Text + " LIKE '" + textBox15.Text + "%' AND coalesce(cancel,'')<>'N' AND t_date between " + "'" + dateFrom + "' and '" + dateTo + "' ORDER BY inv_num ASC");

                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int r = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[r];

                    row.Cells["dgvl_inv_num"].Value = dt.Rows[i]["inv_num"].ToString();
                    row.Cells["dgvl_supl_code"].Value = dt.Rows[i]["supl_code"].ToString();
                    row.Cells["dgvl_supl_name"].Value = dt.Rows[i]["supl_name"].ToString();
                    row.Cells["dgvl_reference"].Value = dt.Rows[i]["reference"].ToString();
                    row.Cells["dgvl_inv_date"].Value = gm.toDateString(dt.Rows[i]["inv_date"].ToString(), "");
                    //row.Cells["dgvl_locname"].Value = dt.Rows[i]["whs_code"].ToString();
                    row.Cells["dgvl_whs_code"].Value = dt.Rows[i]["whs_code"].ToString();
                    cbo_paymentterms.SelectedValue = dt.Rows[i]["pay_code"].ToString();
                    row.Cells["dgvl_pay_desc"].Value = cbo_paymentterms.Text;
                    row.Cells["dgvl_pay_code"].Value = dt.Rows[i]["pay_code"].ToString();
                    row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                    row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                    row.Cells["dgvl_userid"].Value = dt.Rows[i]["userid"].ToString();
                    row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                    row.Cells["dgvl_t_time"].Value = dt.Rows[i]["t_time"].ToString();
                    row.Cells["dgvl_final"].Value = dt.Rows[i]["final"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Display list : " + ex.Message);
            }*/
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

            /*DataTable dt;
            
            try
            {
                dgv_list.Rows.Clear();
                thisDatabase db = new thisDatabase();

                String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
                String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");

                dt = db.QueryOnTableWithParams("pinvhd", "*", "coalesce(cancel,'N')<>'' AND t_date between " + "'" + dateFrom + "' and '" + dateTo + "'", "ORDER BY inv_num DESC");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int r = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[r];

                    row.Cells["dgvl_inv_num"].Value = dt.Rows[i]["inv_num"].ToString();
                    row.Cells["dgvl_supl_code"].Value = dt.Rows[i]["supl_code"].ToString();
                    row.Cells["dgvl_supl_name"].Value = dt.Rows[i]["supl_name"].ToString();
                    row.Cells["dgvl_reference"].Value = dt.Rows[i]["reference"].ToString();
                    row.Cells["dgvl_inv_date"].Value = gm.toDateString(dt.Rows[i]["inv_date"].ToString(),"");
                    //row.Cells["dgvl_locname"].Value = dt.Rows[i]["whs_code"].ToString();
                    row.Cells["dgvl_whs_code"].Value = dt.Rows[i]["whs_code"].ToString();
                    cbo_paymentterms.SelectedValue = dt.Rows[i]["pay_code"].ToString();
                    row.Cells["dgvl_pay_desc"].Value = cbo_paymentterms.Text;
                    row.Cells["dgvl_pay_code"].Value = dt.Rows[i]["pay_code"].ToString();
                    row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                    row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                    row.Cells["dgvl_userid"].Value = dt.Rows[i]["userid"].ToString();
                    row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(),"");
                    row.Cells["dgvl_t_time"].Value = dt.Rows[i]["t_time"].ToString();
                    row.Cells["dgvl_final"].Value = dt.Rows[i]["final"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Display list : " + ex.Message);
            }*/
        }

        private void disp_itemlist(String inv_num)
        {
            DataTable dt;
            this.dgv_itemlist.Sort(this.dgv_itemlist.Columns[0], ListSortDirection.Ascending);

            try
            {
                dt = db.get_po_item_list(inv_num);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["dgvi_lnno", i].Value =  dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    dgv_itemlist["dgvi_vatcode", i].Value =  dt.Rows[i]["vat_code"].ToString();

                    switch (dt.Rows[i]["vat_code"].ToString())
                    {
                        case "I":
                            dgv_itemlist["dgvi_vattype", i].Value = "INCLUSIVE";
                            break;
                        case "E":
                            dgv_itemlist["dgvi_vattype", i].Value = "EXCLUSIVE";
                            break;
                        case "N":
                            dgv_itemlist["dgvi_vattype", i].Value = "NO VAT";
                            break;
                    }
                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["inv_qty"].ToString();
                    dgv_itemlist["dgvi_costunit", i].Value = db.get_item_unit_desc(dt.Rows[i]["unit"].ToString()); // get unit name later
                   
                    dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[i]["unit"].ToString();
                    dgv_itemlist["dgvi_costprice", i].Value =  dt.Rows[i]["price"].ToString();
                    //dgv_itemlist["dgvi_price", i].Value = dt.Rows[i]["price"].ToString();
                    dgv_itemlist["dgvi_discpct", i].Value = dt.Rows[i]["disc_pct"].ToString();
                    dgv_itemlist["dgvi_discamnt", i].Value = dt.Rows[i]["disc_amt"].ToString();
                    dgv_itemlist["dgvi_netprice", i].Value = dt.Rows[i]["netprice"].ToString();
                    dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[i]["ln_amnt"].ToString();
                    dgv_itemlist["dgvi_lnvat", i].Value = dt.Rows[i]["ln_vat"].ToString();
                    dgv_itemlist["dgvi_newregsellprice", i].Value = dt.Rows[i]["newprice"].ToString();
                    dgv_itemlist["dgvi_lotnum", i].Value = dt.Rows[i]["lot_no"].ToString();
                    dgv_itemlist["dgvi_expiry", i].Value = dt.Rows[i]["expiry"].ToString();
                    dgv_itemlist["dgvi_ccdesc", i].Value = db.get_costcenter_desc(dt.Rows[i]["cc_code"].ToString()); //get cost center name later
                    dgv_itemlist["dgvi_cccode", i].Value =  dt.Rows[i]["cc_code"].ToString();
                    dgv_itemlist["dgvi_scc_code", i].Value = dt.Rows[i]["scc_code"].ToString();
                    //dgv_itemlist["dgvi_vatcode", i].Value = dt.Rows[i]["cc_code"].ToString();
                                        
                    if (isnew == false)
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["inv_qty"].ToString();
                    }
                    else
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = "0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Data : " + ex.Message);
            }
        }

        public void disp_total()
        {
            Double total = 0.00;

            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count - 1; i++)
                {
                    total += gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnvat", i].Value.ToString());
                    total += gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnamnt", i].Value.ToString());
                }
            }
            catch (Exception) { }

            lbl_invoice_total.Text = gm.toAccountingFormat(total);
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

                dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[0]["ln_num"].ToString();
                dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[0]["part_no"].ToString();
                dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[0]["item_code"].ToString();
                dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[0]["item_desc"].ToString();
                dgv_itemlist["dgvi_vatcode", i].Value = dt.Rows[0]["vat_code"].ToString();
                //dgv_itemlist["dgvi_vattype", i].Value = dt.Rows[0]["vat_code"].ToString();
                string temp = dgv_itemlist["dgvi_vatcode", i].Value.ToString();

                switch (temp)
                {
                    case "I":
                        dgv_itemlist["dgvi_vattype", i].Value = "INCLUSIVE";
                        break;
                    case "E":
                        dgv_itemlist["dgvi_vattype", i].Value = "EXCLUSIVE";
                        break;
                    case "N":
                        dgv_itemlist["dgvi_vattype", i].Value = "NO VAT";
                        break;
                    case "D":
                        dgv_itemlist["dgvi_vattype", i].Value = "WRONG VALUE";
                        break;
                }
                dgv_itemlist["dgvi_qty", i].Value = dt.Rows[0]["inv_qty"].ToString();
                dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[0]["unit"].ToString();
                dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[0]["unit_id"].ToString();
                dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[0]["price"].ToString();
                //dgv_itemlist["dgvi_price", i].Value = dt.Rows[i]["price"].ToString();
                dgv_itemlist["dgvi_discpct", i].Value = dt.Rows[0]["disc_pct"].ToString();
                dgv_itemlist["dgvi_discamnt", i].Value = dt.Rows[0]["disc_amt"].ToString();
                dgv_itemlist["dgvi_netprice", i].Value = dt.Rows[0]["netprice"].ToString();
                dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[0]["ln_amnt"].ToString();
                dgv_itemlist["dgvi_lnvat", i].Value = dt.Rows[0]["ln_vat"].ToString();
                dgv_itemlist["dgvi_newregsellprice", i].Value = dt.Rows[0]["newprice"].ToString();
                dgv_itemlist["dgvi_lotnum", i].Value = dt.Rows[0]["lot_no"].ToString();
                dgv_itemlist["dgvi_expiry", i].Value = dt.Rows[0]["expiry"].ToString();
                //dgv_itemlist["dgvi_ccdesc", i].Value = dt.Rows[0]["dgvi_ccdesc"].ToString();
                dgv_itemlist["dgvi_cccode", i].Value = dt.Rows[0]["cc_code"].ToString();
                dgv_itemlist["dgvi_scc_code", i].Value = dt.Rows[0]["scc_code"].ToString();

                //dgv_itemlist["dgvi_lnno", i].Value = dt.Rows[0]["dgvi_lnno"].ToString();
                //dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[0]["dgvi_itemcode"].ToString();
                //dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[0]["dgvi_itemdesc"].ToString();
                //dgv_itemlist["dgvi_vatcode", i].Value = dt.Rows[0]["dgvi_vatcode"].ToString();
                //dgv_itemlist["dgvi_vattype", i].Value = dt.Rows[0]["dgvi_vattype"].ToString();
                //dgv_itemlist["dgvi_qty", i].Value = dt.Rows[0]["dgvi_qty"].ToString();
                //dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[0]["dgvi_costunit"].ToString();
                //dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[0]["dgvi_costunitid"].ToString();
                ////dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[0]["dgvi_costprice"].ToString();
                //dgv_itemlist["dgvi_price", i].Value = dt.Rows[i]["price"].ToString();
                //dgv_itemlist["dgvi_discpct", i].Value = dt.Rows[0]["dgvi_discpct"].ToString();
                //dgv_itemlist["dgvi_discamnt", i].Value = dt.Rows[0]["dgvi_discamnt"].ToString();
                //dgv_itemlist["dgvi_netprice", i].Value = dt.Rows[0]["dgvi_netprice"].ToString();
                //dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[0]["dgvi_lnamnt"].ToString();
                //dgv_itemlist["dgvi_lnvat", i].Value = dt.Rows[0]["dgvi_lnvat"].ToString();
                //dgv_itemlist["dgvi_newregsellprice", i].Value = dt.Rows[0]["dgvi_newregsellprice"].ToString();
                //dgv_itemlist["dgvi_lotnum", i].Value = dt.Rows[0]["dgvi_lotnum"].ToString();
                //dgv_itemlist["dgvi_expiry", i].Value = dt.Rows[0]["dgvi_expiry"].ToString();
                //dgv_itemlist["dgvi_ccdesc", i].Value = dt.Rows[0]["dgvi_ccdesc"].ToString();
                //dgv_itemlist["dgvi_cccode", i].Value = dt.Rows[0]["dgvi_cccode"].ToString();

                if (isnew_item)
                {
                    inc_lnno();
                }
            }
            catch (Exception) { }

            disp_total();
        }

        public String get_dgvi_lnno(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnno", currow].Value.ToString();

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

        public String get_dgvi_qty(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_qty", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_costunitid(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_costunitid", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_costprice(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_costprice", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_discpct(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_discpct", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_discamnt(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_discamnt", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_netprice(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_netprice", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_lnamnt(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnamnt", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_vatcode(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_vatcode", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_lnvat(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lnvat", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_newregsellprice(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_newregsellprice", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_lotnum(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_lotnum", currow].Value.ToString();

            return val;
        }

        public DateTime get_dgvi_expiry(int currow)
        {
            DateTime val;

            val = Convert.ToDateTime(dgv_itemlist["dgvi_expiry", currow].Value.ToString());

            return val;
        }

        public String get_dgvi_cccode(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_cccode", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_partno(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_part_no", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_scc_code(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_scc_code", currow].Value.ToString();

            return val;
        }

        private void cbo_supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_suppliername.SelectedIndex > -1)
                {
                    String c_code = cbo_suppliername.SelectedValue.ToString();

                    cbo_paymentterms.SelectedValue = db.get_mp_code_of_supplier(c_code);
                }
            }
            catch (Exception) { }
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

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbo_suppliername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_suppliername.SelectedIndex > -1)
                {
                    String c_code = cbo_suppliername.SelectedValue.ToString();

                    cbo_paymentterms.SelectedValue = db.get_mp_code_of_supplier(c_code);
                }
            }
            catch (Exception) { }
        }

        private void dgv_itemlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            String typname = "dgvl_supl_name";

            try
            {
                String searchValue = textBox15.Text.ToUpper();

                if (comboBox1.SelectedIndex == 0)
                {
                    typname = "dgvl_inv_num";
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    typname = "dgvl_supl_name";
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
                        row.Cells["dgvl_inv_num"].Value = dt.Rows[i]["inv_num"].ToString();
                        row.Cells["dgvl_supl_code"].Value = dt.Rows[i]["supl_code"].ToString();
                        row.Cells["dgvl_supl_name"].Value = dt.Rows[i]["supl_name"].ToString();
                        row.Cells["dgvl_reference"].Value = dt.Rows[i]["reference"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_inv_date"].Value = gm.toDateString(dt.Rows[i]["inv_date"].ToString(), "");
                        //row.Cells["dgvl_locname"].Value = dt.Rows[i]["whs_code"].ToString();
                        row.Cells["dgvl_whs_code"].Value = dt.Rows[i]["whs_code"].ToString();
                        cbo_paymentterms.SelectedValue = dt.Rows[i]["pay_code"].ToString();
                        row.Cells["dgvl_pay_desc"].Value = cbo_paymentterms.Text;
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_pay_code"].Value = dt.Rows[i]["pay_code"].ToString();
                        row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                        row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                        row.Cells["dgvl_userid"].Value = dt.Rows[i]["userid"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                        row.Cells["dgvl_t_time"].Value = dt.Rows[i]["t_time"].ToString();
                        row.Cells["dgvl_final"].Value = dt.Rows[i]["final"].ToString();
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
            String ORDERBY = "ORDER BY inv_num DESC";

            String search_text = textBox15.getText();
            int selected_index = comboBox1.getSelectedIndex();


            if (selected_index == -1 && !String.IsNullOrEmpty(search_text))
            {
                WHERE = " AND (supl_name LIKE '%" + search_text + "%' OR inv_num LIKE '%" + search_text + "%')";
                if (gm.toNormalDoubleFormat(search_text) != 0)
                {
                    ORDERBY = "ORDER BY supl_code, inv_num DESC";
                }
                else
                {
                    ORDERBY = "ORDER BY inv_num DESC";
                }
            }
            else if (selected_index == 0)
            {
                WHERE = " AND inv_num LIKE '%" + search_text + "%'";
            }
            else if (selected_index == 1)
            {
                WHERE = " AND (supl_name LIKE '%" + search_text + "%' OR supl_code LIKE '%" + search_text + "%')";
                ORDERBY = "ORDER BY supl_name, inv_num DESC";
            }
            else if (selected_index == 2)
            {
                WHERE = " AND reference LIKE '%" + search_text + "%'";
                ORDERBY = "ORDER BY reference, inv_num DESC";
            }

            dt = db.QueryOnTableWithParams("pinvhd", "*", "coalesce(cancel,'N')<>'' AND t_date between " + "'" + dtp_frm.getStringDate() + "' and '" + dtp_to.getStringDate() + "' " + WHERE, ORDERBY);

            return dt;
        }
    }
}
