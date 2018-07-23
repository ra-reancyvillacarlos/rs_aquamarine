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
    public partial class i_ReceivingPurchase : Form
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

      
        Boolean isReady = false;
        Boolean bgworker_cancel = false;

        public i_ReceivingPurchase()
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

        void i_ReceivingPurchase_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void frm_clear()
        {
            try
            {
                txt_invoiceno.Text = "";
                txt_reference.Text = "";
                txt_po.Text = "";
                cbo_suppliername.SelectedIndex = -1;
                cbo_stocklocation.SelectedIndex = -1;
                cbo_paymentterms.SelectedIndex = -1;
                dtp_invoicedt.Value = Convert.ToDateTime(db.get_systemdate(""));
                lbl_invoice_total.Text = "0.00";

                dgv_itemlist.Rows.Clear();
                dgv_delitem.Rows.Clear();
            }
            catch (Exception) { }
        }


        void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            disp_list();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {

             lnno_last = 1;
            cbo_suppliername.Enabled = true;
            isnew = true;
            goto_win2();
            cbo_suppliername.DroppedDown = true;
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
            String code = "";
            isnew = false;

            try
            {

                r = dgv_list.CurrentRow.Index;
                code = (dgv_list["dgvl_inv_num", r].Value??"").ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    if (dgv_list["dgvl_cancel", r].Value.ToString().Equals("Y"))
                    {
                        MessageBox.Show("Rec PO# " + code + " is already cancelled. Can not be updated.");
                    }
                    else
                    {
                        try
                        {
                            txt_invoiceno.Text = code;
                            cbo_suppliername.SelectedValue = dgv_list["dgvl_supl_code", r].Value.ToString();
                            txt_reference.Text = dgv_list["dgvl_reference", r].Value.ToString();
                            txt_po.Text = dgv_list["dgvl_po_num", r].Value.ToString();
                            cbo_stocklocation.SelectedValue = dgv_list["dgvl_whs_code", r].Value.ToString();
                            if (!String.IsNullOrEmpty(dgv_list["dgvl_pay_desc", r].Value.ToString()))
                            {
                                cbo_paymentterms.SelectedValue = dgv_list["dgvl_pay_desc", r].Value.ToString();
                            }
                            else
                            {
                                cbo_paymentterms.SelectedIndex = -1;
                            }
                            //

                            cbo_suppliername.Enabled = false;
                            dtp_invoicedt.Value = Convert.ToDateTime(dgv_list["dgvl_inv_date", r].Value.ToString());
                        }
                        catch (Exception er) { MessageBox.Show(er.Message); }

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
            catch{
                MessageBox.Show("No invoice selected.");
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
                        code = dgv_list["dgvl_code", r].Value.ToString();

                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this P.I.# " + code + "?", "Confirm", MessageBoxButtons.YesNo); ;

                        if (dialogResult == DialogResult.Yes)
                        {

                            if (db.UpdateOnTable("rechdr", "supl_name='CANCELLED' || '-' ||supl_name, cancel='Y'", "rec_num='" + code + "'"))
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
              
            }
            catch (Exception) { }

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

                    _frm_ei = new z_enter_item(this, isnew_item, cur_index, r);
                    _frm_ei.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("Excption : No item selected."); }
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

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            try
            {
                z_Notification notify = new z_Notification();
                String notificationText = "has added: ";
                String notifyadd = "";
                Boolean success = false;

                String code, po_num, supp_id, supp_name, terms, loc, dt_inv, reference, stk_ref = "", stk_po_so = "";

                String i_oldqty = "";
                String di_code = "", di_qty = "0.00";
                String col = "", val = "";

                String trns_type = this.stk_trns_type;
                String table = "rechdr";
                String tableln = "reclne";
                int r;

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
                    po_num = txt_po.Text;
                    supp_id = cbo_suppliername.SelectedValue.ToString();
                    supp_name = cbo_suppliername.Text.ToString();
                    terms = cbo_paymentterms.SelectedValue.ToString();
                    loc = cbo_stocklocation.SelectedValue.ToString();
                    dt_inv = dtp_invoicedt.Value.ToString("yyyy-MM-dd");
                    reference = txt_reference.Text;
                    stk_ref = stk_trns_type + "#" + code;
                    stk_po_so = po_num;

                    if (isnew)
                    {
                        code = db.get_pk("rec_num");

                        col = "rec_num, supl_code, supl_name, _reference, purc_ord, trnx_date, whs_code, trn_type, recipient, t_date, t_time, payment_term";
                        val = "'" + code + "', '" + supp_id + "', " + db.str_E(supp_name) + ", " + db.str_E(reference) + ", '" + po_num + "', '" + dt_inv + "', '" + loc + "', '" + stk_trns_type + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "','" + terms + "'";


                        if (db.InsertOnTable(table, col, val))
                        {
                            notifyadd = add_items(tableln, code, dt_inv, stk_ref, stk_po_so, loc, supp_id, supp_name);

                            if (String.IsNullOrEmpty(notifyadd) == false)
                            {
                                notificationText += notifyadd;
                                notificationText += Environment.NewLine + " with #" + code;
                                notify.saveNotification(notificationText, "RECEIVING PURCHASE");
                                db.set_pkm99("rec_num", db.get_nextincrementlimitchar(code, 8));
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
                            db.DeleteOnTable("stkcrd", "po_so='" + po_num + "' AND trn_type='" + stk_trns_type + "'");
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        col = "rec_num='" + code +
                            "', supl_code='" + supp_id +
                            "', supl_name=" + db.str_E(supp_name) +
                            ", _reference=" + db.str_E(reference) +
                            ", purc_ord='" + po_num +
                            "', trnx_date='" + dt_inv +
                            "', whs_code='" + loc +
                            "', trn_type='" + trns_type +
                            "', recipient='" + GlobalClass.username +
                            "', t_date='" + db.get_systemdate("") +
                            "', t_time='" + db.get_systemtime() + 
                            "', payment_term='" + terms + "'";

                        if (db.UpdateOnTable(table, col, "rec_num='" + code + "'"))
                        {
                            db.DeleteOnTable(tableln, "rec_num='" + code + "'");
                            db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                            notifyadd = add_items(tableln, code, dt_inv, stk_ref, stk_po_so, loc, supp_id, supp_name);

                            if (String.IsNullOrEmpty(notifyadd) == false)
                            {
                                notificationText += notifyadd;
                                notificationText += Environment.NewLine + " with #" + code;
                                notify.saveNotification(notificationText, "RECEIVING PURCHASE");
                                success = true;
                            }
                            else
                            {
                                success = false;
                            }
                        }

                        if (success == false)
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
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private String add_items(String tableln, String code, String dt_inv, String stk_ref, String stk_po_so, String loc, String supp_id, String supp_name)
        {
            String notificationText = null;
            String i_lnno = "", i_itemcode = "", i_itemdesc = "", i_vatcode = "", i_qty = "", i_costunitid = "", i_costprice = "", i_discpct = "", i_discamnt = "", i_netprice = "", i_lnamnt = "",i_cht_code="", i_lnvat = "", i_newregsellprice = "", i_lotnum = "", i_expiry = "", i_cccode = "", i_scc_code = "", i_poline="0", i_fcp = "",i_part_no=""; //i_ccdesc = "",
            String val2 = "",purc_ord ="";
            String col2 = "rec_num, ln_num, item_code, item_desc, unit, recv_qty, price, ln_amnt, discount, cnt_code, ln_vat, scc_code, non_vat, po_line,purc_ord,part_no, lot_no, newprice,cht_code";
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";

           // try
            //{

                for (int r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
                {
                    i_lnno = (dgv_itemlist["dgvi_lnno", r].Value??"").ToString();
                    i_part_no = (dgv_itemlist["dgvi_part_no", r].Value??"").ToString();
                    i_itemcode = (dgv_itemlist["dgvi_itemcode", r].Value??"").ToString();
                    i_itemdesc = (dgv_itemlist["dgvi_itemdesc", r].Value??"").ToString();
                    i_vatcode = (dgv_itemlist["dgvi_vatcode", r].Value??"").ToString();
                    i_cht_code = (dgv_itemlist["dgvi_account_link", r].Value??"").ToString();
                    i_qty = gc.toNormalDoubleFormat((dgv_itemlist["dgvi_qty", r].Value??"").ToString()).ToString("0.00");
                    i_costunitid = dgv_itemlist["dgvi_costunitid", r].Value.ToString();
                   
                    i_costprice = gc.toNormalDoubleFormat((dgv_itemlist["dgvi_costprice", r].Value??"").ToString()).ToString("0.00");
                    //i_costprice = dgv_itemlist["dgvi_price", r].Value.ToString();
                    //i_discpct = gc.toNormalDoubleFormat(dgv_itemlist["dgvi_discpct", r].Value.ToString()).ToString("0.00");
                    i_discamnt = gc.toNormalDoubleFormat((dgv_itemlist["dgvi_discamnt", r].Value??"").ToString()).ToString("0.00");
                    i_lnamnt = gc.toNormalDoubleFormat((dgv_itemlist["dgvi_lnamnt", r].Value??"").ToString()).ToString("0.00");

                    i_lnvat = gc.toNormalDoubleFormat((dgv_itemlist["dgvi_lnvat", r].Value??"").ToString()).ToString("0.00");
                    
                   // i_netprice = gc.toNormalDoubleFormat(dgv_itemlist["dgvi_netprice", r].Value.ToString()).ToString("0.00");

                    //i_newregsellprice = "0.00";// dgv_itemlist["dgvi_newregsellprice", r].Value.ToString();
                    i_newregsellprice = gm.toNormalDoubleFormat((dgv_itemlist["dgvi_newregsellprice", r].Value ?? "").ToString()).ToString("0.00");

                    i_lotnum = (dgv_itemlist["dgvi_lotnum", r].Value??"").ToString();
                    i_expiry = "";// dgv_itemlist["dgvi_expiry", r].Value.ToString();
                    i_cccode = (dgv_itemlist["dgvi_cccode", r].Value??"").ToString();
                    i_scc_code = (dgv_itemlist["dgvi_scc_code", r].Value??"").ToString(); //scc_code
                    try
                    {
                        i_poline = dgv_itemlist["dgvi_poline", r].Value.ToString();
                        if (i_poline == "" || i_poline == null)
                        {
                            i_poline = "0.00";
                        }
                        purc_ord = dgv_itemlist["dgvi_ponum", r].Value.ToString();
                    }catch{}
                  
                    i_fcp = db.get_item_fcp(i_itemcode).ToString("0.00");



                    val2 = "'" + code + "', '" + i_lnno + "', '" + i_itemcode + "', " + db.str_E(i_itemdesc) + ", '" + i_costunitid + "', '" + i_qty + "', '" + i_costprice + "', '" + i_lnamnt + "', '" + i_discamnt + "', '" + i_cccode + "', '" + i_lnvat + "', '" + i_scc_code + "', '" + i_vatcode + "', '" + i_poline + "'," + "'" + purc_ord + "','" + i_part_no + "','" + i_lotnum + "','" + i_newregsellprice + "','" + i_cht_code + "'";

                    if (db.InsertOnTable(tableln, col2, val2))
                    {
                        stk_qty_in = "0.00";
                        stk_qty_out = "0.00";

                        if (gm.toNormalDoubleFormat(i_qty) < 0)
                            stk_qty_out = Math.Abs(gm.toNormalDoubleFormat(i_qty)).ToString();
                        else
                            stk_qty_in = i_qty;

                        db.save_to_stkcard(i_itemcode, i_itemdesc, i_costunitid, dt_inv, stk_ref, stk_po_so, stk_qty_in, stk_qty_out, i_fcp, i_costprice, loc, supp_id, supp_name, stk_trns_type, "", i_cccode, i_scc_code);

                        notificationText += Environment.NewLine + i_itemdesc;
                    }
                    else
                    {
                        notificationText = null;
                    }
                }
          //  }
          //  catch (Exception er) { MessageBox.Show(er.Message);  notificationText = null; }

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
            //DataTable dt = db.get_polist(dt_frm, dt_to, "E");
            DataTable dt = db.get_stkinvlist(dtp_frm.Value, dtp_to.Value, "P");

            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_list.Rows.Add();

                    dgv_list["dgvl_inv_num", i].Value = dt.Rows[i]["rec_num"].ToString();
                    dgv_list["dgvl_supl_code", i].Value = dt.Rows[i]["supl_code"].ToString();
                    dgv_list["dgvl_supl_name", i].Value = dt.Rows[i]["supl_name"].ToString();
                    dgv_list["dgvl_reference", i].Value = dt.Rows[i]["_reference"].ToString();

                    dgv_list["dgvl_inv_date", i].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                    dgv_list["dgvl_locname", i].Value = dt.Rows[i]["whs_desc"].ToString();
                    dgv_list["dgvl_whs_code", i].Value = dt.Rows[i]["whs_code"].ToString();
                   // dgv_list["dgvl_pay_desc", i].Value = dt.Rows[i]["mp_desc"].ToString();
                   // dgv_list["dgvl_pay_code", i].Value = dt.Rows[i]["mp_code"].ToString();
                    dgv_list["dgvl_jrnlz", i].Value = dt.Rows[i]["jrnlz"].ToString();
                    dgv_list["dgvl_cancel", i].Value = dt.Rows[i]["cancel"].ToString();
                    dgv_list["dgvl_userid", i].Value = dt.Rows[i]["recipient"].ToString();
                    dgv_list["dgvl_t_date", i].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(),"");
                    dgv_list["dgvl_t_time", i].Value = dt.Rows[i]["t_time"].ToString();
                    dgv_list["dgvl_po_num", i].Value = dt.Rows[i]["purc_ord"].ToString();
                    cbo_paymentterms.SelectedValue = dt.Rows[i]["payment_term"].ToString();
                    dgv_list["dgvl_trm_desc", i].Value = cbo_paymentterms.Text;
                    dgv_list["dgvl_pay_desc", i].Value = dt.Rows[i]["payment_term"].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }*/
        }

        private void disp_itemlist(String inv_num)
        {
            DataTable dt;
            double netprice = 00.00;

            try
            {
                dt = db.get_rr_items(inv_num);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["dgvi_lnno", i].Value =  dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value =  dt.Rows[i]["item_code"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value =dt.Rows[i]["item_desc"].ToString();
                    dgv_itemlist["dgvi_vatcode", i].Value = dt.Rows[i]["non_vat"].ToString();
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                    String cost_price = dt.Rows[i]["price"].ToString();
                    //i_scc_code = dgv_itemlist["dgvi_scc_code", r].Value.ToString(); //scc_code
                    if (cost_price != "0.0000")
                    {
                        netprice = gc.toNormalDoubleFormat(dt.Rows[i]["recv_qty"].ToString()) / gc.toNormalDoubleFormat(dt.Rows[i]["price"].ToString());
                        dgv_itemlist["dgvi_netprice", i].Value = gc.toNormalDoubleFormat(netprice.ToString()).ToString("0.00");
                    }
                    else
                    {
                        dgv_itemlist["dgvi_netprice", i].Value = "0.000";
                    }
                  
                    switch (dt.Rows[i]["non_vat"].ToString())
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

                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["recv_qty"].ToString();
                    dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[i]["unit_desc"].ToString();
                    dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[i]["price"].ToString();
                    //dgv_itemlist["dgvi_discpct", i].Value =  dt.Rows[i]["disc_pct"].ToString();
                    dgv_itemlist["dgvi_discamnt", i].Value = dt.Rows[i]["discount"].ToString();
                    //dgv_itemlist["dgvi_netprice", i].Value = dt.Rows[i]["netprice"].ToString();
                    dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[i]["ln_amnt"].ToString();
                    dgv_itemlist["dgvi_lnvat", i].Value =  dt.Rows[i]["ln_vat"].ToString();
                    dgv_itemlist["dgvi_cccode", i].Value = dt.Rows[i]["cnt_code"].ToString();
                    dgv_itemlist["dgvi_scc_code", i].Value = dt.Rows[i]["scc_code"].ToString();

                    dgv_itemlist["dgvi_newregsellprice", i].Value = dt.Rows[i]["newprice"].ToString();

                    dgv_itemlist["dgvi_account_link", i].Value = dt.Rows[i]["cht_code"].ToString();

                    dgv_itemlist["dgvi_ponum", i].Value = dt.Rows[i]["purc_ord"].ToString();
                    dgv_itemlist["dgvi_poline", i].Value = dt.Rows[i]["po_line"].ToString();
                    dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[i]["unit_id"].ToString();
                    dgv_itemlist["dgvi_lotnum", i].Value = dt.Rows[i]["lot_no"].ToString();
                    if (isnew == false)
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["recv_qty"].ToString();
                    }
                    else
                    {
                        dgv_itemlist["dgvi_oldqty", i].Value = "0.00";
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Disp err : " + ex.Message);
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
                //
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
                dgv_itemlist["dgvi_account_link", i].Value = dt.Rows[0]["acct_code"].ToString();
                
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
            try
            {
                val = dgv_itemlist["dgvi_lnno", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        public String get_dgvi_itemcode(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_itemcode", currow].Value.ToString();

            }
            catch { }


            return val;
        }

        public String get_dgvi_itemdesc(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_itemdesc", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        public String get_dgvi_qty(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_qty", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        public String get_dgvi_costunitid(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_costunitid", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        public String get_dgvi_costprice(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_costprice", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        public String get_dgvi_discpct(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_discpct", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        public String get_dgvi_discamnt(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_discamnt", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        public String get_dgvi_netprice(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_netprice", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        public String get_dgvi_lnamnt(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_lnamnt", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        public String get_dgvi_vatcode(int currow)
        {
            String val = "";
            try {
                val = dgv_itemlist["dgvi_vatcode", currow].Value.ToString();
            }
            catch { }
            

            return val;
        }

        public String get_dgvi_lnvat(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_lnvat", currow].Value.ToString();


            }
            catch { }

            return val;
        }

        public String get_dgvi_newregsellprice(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_newregsellprice", currow].Value.ToString();


            }
            catch { }

            return val;
        }

        public String get_dgvi_lotnum(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_lotnum", currow].Value.ToString();

            }
            catch { }
            

            return val;
        }

        public DateTime get_dgvi_expiry(int currow)
        {
            DateTime val = DateTime.Now;
            try
            {
                val = Convert.ToDateTime(dgv_itemlist["dgvi_expiry", currow].Value.ToString());

            }
            catch { }
            

            return val;
        }

        public String get_dgvi_cccode(int currow)
        {
            String val = "";
              try
            {
                val = dgv_itemlist["dgvi_cccode", currow].Value.ToString();

            }
            catch { }

            return val;
        }
        public String get_dgvi_partno(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_part_no", currow].Value.ToString();

            }
            catch { }

            return val;
        }
        public String get_dgvi_scc_code(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_scc_code", currow].Value.ToString();

            }
            catch { }

            return val;
        }
        public String get_dgvi_acct_code(int currow)
        {
            String val = "";
            try
            {
                val = dgv_itemlist["dgvi_account_link", currow].Value.ToString();

            }
            catch { }

            return val;
        }

        private void btnFromPO_Click(object sender, EventArgs e)
        {
            
            int lastrow = 0;

            try
            {
                if (isnew == false)
                {
                    lastrow = dgv_itemlist.Rows.Count - 2;
                    lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                    //inc_lnno();
                }
                else
                {
                    if (dgv_itemlist.Rows.Count == 1)
                    {
                        lnno_last = 0;
                        //inc_lnno();
                    }
                    else
                    {
                        lastrow = dgv_itemlist.Rows.Count - 2;
                        lnno_last = int.Parse(dgv_itemlist["dgvi_lnno", lastrow].Value.ToString());
                        //inc_lnno();
                    }
                }
            }
            catch (Exception) { }

            z_add_item_from_PO po = new z_add_item_from_PO(this, lnno_last);

            po.ShowDialog();
        }

        public void set_dgv_fromPO(String purc_ord)
        {
            DataTable dt = null;
            int i = 0;
            String referencetext = "";
            double i_netpirce = 00.00;
            try
            {
                if (dgv_itemlist.Rows.Count > 1)
                {
                    MessageBox.Show("Invalid action. One RECIEVING must only have one PO.");
                    return;
                }
                dt = db.get_po_items(purc_ord);
                for (i = 0; i < dt.Rows.Count; i++ )
                {
                    
                    i = dgv_itemlist.Rows.Add();

                    inc_lnno();

                    dgv_itemlist["dgvi_lnno", i].Value = this.lnno_last;
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["delv_qty"].ToString();
                    dgv_itemlist["dgvi_costunit", i].Value = dt.Rows[i]["unit_desc"].ToString();
                    dgv_itemlist["dgvi_costprice", i].Value = dt.Rows[i]["price"].ToString();
                    dgv_itemlist["dgvi_lnamnt", i].Value = dt.Rows[i]["ln_amnt"].ToString();
                    //dgv_itemlist["dgvi_discpct", i].Value = dt.Rows[i][""].ToString();
                    dgv_itemlist["dgvi_discamnt", i].Value = dt.Rows[i]["discount"].ToString();
                    dgv_itemlist["dgvi_lnvat", i].Value = dt.Rows[i]["ln_vat"].ToString();
                    i_netpirce = gm.toNormalDoubleFormat(dt.Rows[i]["price"].ToString()) / gm.toNormalDoubleFormat(dt.Rows[i]["delv_qty"].ToString());
                    dgv_itemlist["dgvi_netprice", i].Value = i_netpirce.ToString("0.00");
                    //lacking field
                   // dgv_itemlist["dgvi_netprice", i].Value = dt.Rows[i][""].ToString();
                    //dgv_itemlist["dgvi_newregsellprice", i].Value = dt.Rows[i][""].ToString();
                    //dgv_itemlist["dgvi_lotnum", i].Value = dt.Rows[i][""].ToString();
                    //dgv_itemlist["dgvi_expiry", i].Value = dt.Rows[i][""].ToString();
                    dgv_itemlist["dgvi_account_link", i].Value = dt.Rows[i]["cht_code"].ToString();
                    dgv_itemlist["dgvi_cccode", i].Value = dt.Rows[i]["cnt_code"].ToString();
                    //dgv_itemlist["dgvi_vatcode", i].Value = dt.Rows[i][""].ToString();
                    dgv_itemlist["dgvi_costunitid", i].Value = dt.Rows[i]["unit_id"].ToString();
                    dgv_itemlist["dgvi_scc_code", i].Value = dt.Rows[i]["scc_code"].ToString();
                    dgv_itemlist["dgvi_ponum", i].Value = dt.Rows[i]["pr_code"].ToString();
                    dgv_itemlist["dgvi_poline", i].Value = dt.Rows[i]["pr_ln"].ToString();
                    dgv_itemlist["dgvi_vatcode", i].Value = dt.Rows[i]["debt_code"].ToString();
                    referencetext = dt.Rows[i]["purc_ord"].ToString();
                    cbo_paymentterms.SelectedValue = dt.Rows[i]["pay_code"].ToString();
                    cbo_suppliername.SelectedValue = dt.Rows[i]["supl_code"].ToString();
                    // cbo_suppliername.Enabled = false;
                }


                if (String.IsNullOrEmpty(txt_reference.Text))
                {
                    txt_reference.Text = "";
                    txt_po.Text = referencetext;
                }
                else
                {
                    txt_reference.Text = txt_reference.Text + " / " + referencetext;
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Set Items error : " + ex.Message + " Line : " + ex.StackTrace.ToString());
            }
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

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFromPO_Click_1(object sender, EventArgs e)
        {

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
                    rpt.print_receving_po(inv_num);
                    rpt.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_list();

            /*
            DataTable dt = new DataTable();
            string schema = "rssys";
            if (comboBox1.Text == string.Empty)
            {
                String ss = "SELECT p.payment_term, p.\"locationFrom\", p.\"locationTo\", p.rec_num, p.supl_name,p.supl_code, p._reference, TO_CHAR(p.trnx_date, 'MM/DD/YYYY') AS trnx_date, p.whs_code, p.trn_type, p.jrnlz, p.cancel, p.recipient, TO_CHAR(p.t_date, 'MM/DD/YYYY') AS t_date, p.t_time, p.vat_code, w.whs_desc, p.purc_ord, p.printed FROM " + schema + ".rechdr p LEFT JOIN " + schema + ".whouse w ON w.whs_code=p.whs_code WHERE rec_num LIKE '" + textBox15.Text + "%' OR supl_code LIKE '" + textBox15.Text + "%' OR supl_name LIKE '" + textBox15.Text + "%' OR _reference LIKE '" + textBox15.Text + "%' OR whs_desc LIKE '" + textBox15.Text + "%' OR whs_code LIKE '" + textBox15.Text + "%'  AND p.t_date between " + "'" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY rec_num";
                dt = db.QueryBySQLCode("SELECT p.payment_term, p.\"locationFrom\", p.\"locationTo\", p.rec_num, p.supl_name,p.supl_code, p._reference, TO_CHAR(p.trnx_date, 'MM/DD/YYYY') AS trnx_date, p.whs_code, p.trn_type, p.jrnlz, p.cancel, p.recipient, TO_CHAR(p.t_date, 'MM/DD/YYYY') AS t_date, p.t_time, p.vat_code, w.whs_desc, p.purc_ord, p.printed FROM " + schema + ".rechdr p LEFT JOIN " + schema + ".whouse w ON w.whs_code=p.whs_code WHERE rec_num LIKE '" + textBox15.Text + "%' OR supl_code LIKE '" + textBox15.Text + "%' OR supl_name LIKE '" + textBox15.Text + "%' OR _reference LIKE '" + textBox15.Text + "%' OR whs_desc LIKE '" + textBox15.Text + "%'  AND p.t_date between " + "'" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY rec_num");
            }
            else if (comboBox1.Text == "clear filter")
            {
                dt = db.get_stkinvlist(dtp_frm.Value, dtp_to.Value, "P");
            }
            else {
                dt = db.QueryBySQLCode("SELECT p.payment_term, p.\"locationFrom\", p.\"locationTo\", p.rec_num, p.supl_name,p.supl_code, p._reference, TO_CHAR(p.trnx_date, 'MM/DD/YYYY') AS trnx_date, p.whs_code, p.trn_type, p.jrnlz, p.cancel, p.recipient, TO_CHAR(p.t_date, 'MM/DD/YYYY') AS t_date, p.t_time, p.vat_code, w.whs_desc, p.purc_ord, p.printed FROM " + schema + ".rechdr p LEFT JOIN " + schema + ".whouse w ON w.whs_code=p.whs_code WHERE "+comboBox1.Text+" LIKE '"+textBox15.Text+"%' AND p.t_date between " + "'" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY rec_num");

            }
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_list.Rows.Add();

                    dgv_list["dgvl_inv_num", i].Value = dt.Rows[i]["rec_num"].ToString();
                    dgv_list["dgvl_supl_code", i].Value = dt.Rows[i]["supl_code"].ToString();
                    dgv_list["dgvl_supl_name", i].Value = dt.Rows[i]["supl_name"].ToString();
                    dgv_list["dgvl_reference", i].Value = dt.Rows[i]["_reference"].ToString();

                    dgv_list["dgvl_inv_date", i].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                    dgv_list["dgvl_locname", i].Value = dt.Rows[i]["whs_desc"].ToString();
                    dgv_list["dgvl_whs_code", i].Value = dt.Rows[i]["whs_code"].ToString();
                    // dgv_list["dgvl_pay_desc", i].Value = dt.Rows[i]["mp_desc"].ToString();
                    // dgv_list["dgvl_pay_code", i].Value = dt.Rows[i]["mp_code"].ToString();
                    dgv_list["dgvl_jrnlz", i].Value = dt.Rows[i]["jrnlz"].ToString();
                    dgv_list["dgvl_cancel", i].Value = dt.Rows[i]["cancel"].ToString();
                    dgv_list["dgvl_userid", i].Value = dt.Rows[i]["recipient"].ToString();
                    dgv_list["dgvl_t_date", i].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                    dgv_list["dgvl_t_time", i].Value = dt.Rows[i]["t_time"].ToString();
                    dgv_list["dgvl_po_num", i].Value = dt.Rows[i]["purc_ord"].ToString();
                    cbo_paymentterms.SelectedValue = dt.Rows[i]["payment_term"].ToString();
                    dgv_list["dgvl_trm_desc", i].Value = cbo_paymentterms.Text;
                    dgv_list["dgvl_pay_desc", i].Value = dt.Rows[i]["payment_term"].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }*/
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

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
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
            String typname = "dgvl_inv_num";

            try
            {
                String searchValue = textBox15.Text.ToUpper();

                if (comboBox1.SelectedIndex == 0)
                {
                    typname = "dgvl_inv_num";
                }
                else if(comboBox1.SelectedIndex == 1)
                {
                    typname = "dgvl_supl_name";
                }
                else if(comboBox1.SelectedIndex == 2)
                {
                    typname = "dgvl_po_num";
                }
                else if(comboBox1.SelectedIndex == 3)
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
                        row.Cells["dgvl_inv_num"].Value = dt.Rows[i]["rec_num"].ToString();
                        row.Cells["dgvl_supl_code"].Value = dt.Rows[i]["supl_code"].ToString();
                        row.Cells["dgvl_supl_name"].Value = dt.Rows[i]["supl_name"].ToString();
                        row.Cells["dgvl_reference"].Value = dt.Rows[i]["_reference"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_inv_date"].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                        row.Cells["dgvl_locname"].Value = dt.Rows[i]["whs_desc"].ToString();
                        row.Cells["dgvl_whs_code"].Value = dt.Rows[i]["whs_code"].ToString();
                        // dgv_list["dgvl_pay_desc", i].Value = dt.Rows[i]["mp_desc"].ToString();
                        // dgv_list["dgvl_pay_code", i].Value = dt.Rows[i]["mp_code"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                        row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                        row.Cells["dgvl_userid"].Value = dt.Rows[i]["recipient"].ToString();
                        row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_t_time"].Value = dt.Rows[i]["t_time"].ToString();
                        row.Cells["dgvl_po_num"].Value = dt.Rows[i]["purc_ord"].ToString();
                        cbo_paymentterms.SelectedValue = dt.Rows[i]["payment_term"].ToString();
                        row.Cells["dgvl_trm_desc"].Value = cbo_paymentterms.Text;
                        row.Cells["dgvl_pay_desc"].Value = dt.Rows[i]["payment_term"].ToString();
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

            String search_text = textBox15.getText();
            int selected_index = comboBox1.getSelectedIndex();

            if (selected_index == -1 && !String.IsNullOrEmpty(search_text))
            {
                WHERE = " AND (supl_name LIKE '%" + search_text + "%' OR rec_num LIKE '%" + search_text + "%') ";
                if (gm.toNormalDoubleFormat(search_text) != 0)
                {
                    ORDERBY = "ORDER BY supl_name, rec_num  DESC";
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
                WHERE = " AND (supl_name LIKE '%" + search_text + "%' OR supl_code LIKE '%" + search_text + "%')";
                ORDERBY = "ORDER BY supl_name, rec_num DESC";
            }
            else if (selected_index == 2)
            {
                WHERE = " AND purc_ord LIKE '%" + search_text + "%'";
                ORDERBY = "ORDER BY purc_ord, rec_num DESC";
            }
            else if (selected_index == 3)
            {
                WHERE = " AND _reference LIKE '%" + search_text + "%'";
                ORDERBY = "ORDER BY _reference, rec_num  DESC";
            }


            dt = db.QueryBySQLCode("SELECT p.payment_term, p.\"locationFrom\", p.\"locationTo\", p.rec_num, p.supl_name,p.supl_code, p._reference, TO_CHAR(p.trnx_date, 'MM/DD/YYYY') AS trnx_date, p.whs_code, p.trn_type, p.jrnlz, p.cancel, p.recipient, TO_CHAR(p.t_date, 'MM/DD/YYYY') AS t_date, p.t_time, p.vat_code, w.whs_desc, p.purc_ord, p.printed FROM rssys.rechdr p LEFT JOIN rssys.whouse w ON w.whs_code=p.whs_code WHERE p.t_date between " + "'" + dtp_frm.getStringDate() + "' AND '" + dtp_to.getStringDate() + "' AND p.trn_type='P'  " + WHERE + "   " + ORDERBY);

            return dt;
        }
    }
}
