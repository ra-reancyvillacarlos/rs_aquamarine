﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    public partial class i_StockTransfer : Form
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
        String stk_trns_type = "T";
        String trns_type = "T";
        Boolean is_receiving = false;

        
        Boolean isReady = false;
        Boolean bgworker_cancel = false;

        public i_StockTransfer()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbInv();
            disp_list();
            //gc.load_account_title(cbo_contraacct);
            //gc.load_costcenter(cbo_costcenter);
            gc.load_stocklocation(cbo_stocklocation);
            gc.load_stocklocation(cbo_stkLocTo);
            this.Load += i_StockTransfer_Load;

            dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));

            disp_list();
            isReady = true; 
        }

        public i_StockTransfer(Boolean is_receive)
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbInv();
            disp_list();
            this.is_receiving = is_receive;
            //gc.load_account_title(cbo_contraacct);
            //gc.load_costcenter(cbo_costcenter);
            gc.load_stocklocation(cbo_stocklocation);
            gc.load_stocklocation(cbo_stkLocTo);
            this.Load += i_StockTransfer_Load;

            if (is_receive) {
                btn_new.Enabled = false;
            }

            dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));

            disp_list();
            isReady = true; 
        }

        void i_StockTransfer_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            if(this.is_receiving == true)
            {
                btn_itemadd.Enabled = false;
                btn_new.Enabled = false;
                txt_code.Enabled = false;
                cbo_stocklocation.Enabled = false;
                cbo_stkLocTo.Enabled = false;
                txt_desc.Enabled = false;
                dtp_trnxdt.Enabled = false;
                F.Columns[5].Visible = true;
            }
            else
            {
                F.Columns[5].Visible = false;
            }
        }


        private void frm_clear()
        {
            try
            {
                txt_code.Text = "";
                txt_desc.Text = "";
                //cbo_contraacct.SelectedIndex = -1;
                cbo_stocklocation.SelectedIndex = -1;
                cbo_stkLocTo.SelectedIndex = -1;
                //cbo_costcentSelectedIndex = -1;er.SelectedIndex = -1;
                dtp_trnxdt.Value = Convert.ToDateTime(db.get_systemdate(""));
                lbl_total.Text = "0.00";

                F.Rows.Clear();
            }
            catch (Exception) { }
        }

        public void frm_disabled(Boolean disabled)
        {
            cbo_stkLocTo.Enabled = disabled;
            cbo_stocklocation.Enabled = disabled;
            txt_code.ReadOnly = !disabled;
            txt_desc.ReadOnly = !disabled;

            btn_itemadd.Enabled = disabled;
            btn_itemupd.Enabled = disabled;
            btn_itemremove.Enabled = disabled;
            btn_mainsave.Enabled = disabled;

        }


        private void btn_new_Click(object sender, EventArgs e)
        {
            frm_disabled(true);

            isnew = true;
            goto_win2();
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "", whscode = "", cccode = "", atcode = "", from ="", to="";
            isnew = false;

            try
            {
                r = dgv_list.CurrentRow.Index;
                code = dgv_list["dgvl_code", r].Value.ToString();

                if (!String.IsNullOrEmpty(code))
                {

                    txt_code.Text = code;

                    if (dgv_list["dgvl_description", r].Value != null)
                    {
                        txt_desc.Text = dgv_list["dgvl_description", r].Value.ToString();
                    }

                    if (dgv_list["dgvl_location", r].Value != null)
                    {
                        from = dgv_list["dgvl_location", r].Value.ToString();
                        cbo_stocklocation.SelectedValue = from;
                    }

                    if (dgv_list["dgvl_locationTo", r].Value != null)
                    {
                        to = dgv_list["dgvl_locationTo", r].Value.ToString();
                        cbo_stkLocTo.SelectedValue = to;
                    }


                    if (dgv_list["dgvl_cccode", r].Value != null)
                    {
                        cccode = dgv_list["dgvl_cccode", r].Value.ToString();
                        //cbo_costcenter.SelectedValue = cccode;
                    }

                    //cbo_contraacct.SelectedValue = dgv_list["", r].Value.ToString();

                    dtp_trnxdt.Value = Convert.ToDateTime(dgv_list["dgvl_trnxdate", r].Value.ToString());

                    disp_itemlist(code);
                    disp_total();

                    //dgvl_is_receive
                    if (dgv_list["dgvl_is_receive", r].Value.ToString().ToLower() == "yes" && is_receiving == false)
                    {
                        frm_disabled(false);
                    }
                    goto_win2();

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

                    if ((dgv_list["dgvl_cancel", r].Value??"").ToString().Equals("Y"))
                    {
                        MessageBox.Show("Invoice already cancelled."); //PO already cancelled.
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
                String trans_no = (dgv_list["dgvl_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(trans_no))
                {
                    String locfrom = dgv_list["dgvl_locfromdesc", r].Value.ToString()
                          , locto = dgv_list["dgvl_loctodesc", r].Value.ToString()
                          , trans_desc = dgv_list["dgvl_description", r].Value.ToString()
                          , t_date = dgv_list["dgvl_systemdate", r].Value.ToString()
                          , t_time = dgv_list["dgvl_systemtime", r].Value.ToString();
                 
                    Report rpt = new Report();
                    rpt.print_stock_transfer(trans_no, trans_desc, locfrom, locto, gm.toDateString(t_date,""), t_time);
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
            String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
            String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");

            DataTable dt;
            
            dgv_list.Rows.Clear();
            dt = db.QueryOnTableWithParams("rechdr", "*", "(COALESCE(cancel,'N')='N' OR COALESCE(cancel,'')='') AND trn_type = 'T' AND t_date between " + "'" + dateFrom + "' and '" + dateTo + "'", "ORDER BY rec_num DESC");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int r = dgv_list.Rows.Add();
                DataGridViewRow row = dgv_list.Rows[r];
                    
                row.Cells["dgvl_code"].Value = dt.Rows[i]["rec_num"].ToString();
                row.Cells["dgvl_description"].Value = dt.Rows[i]["_reference"].ToString();
                row.Cells["dgvl_trnxdate"].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(),"");
                row.Cells["dgvl_whscode"].Value = dt.Rows[i]["whs_code"].ToString();
                row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                row.Cells["dgvl_userid"].Value = dt.Rows[i]["printed"].ToString();
                row.Cells["dgvl_systemdate"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                row.Cells["dgvl_systemtime"].Value = dt.Rows[i]["t_time"].ToString();
                row.Cells["dgvl_locationTo"].Value = dt.Rows[i]["locationTo"].ToString();
                row.Cells["dgvl_location"].Value = dt.Rows[i]["locationFrom"].ToString();
                 
                cbo_stocklocation.SelectedValue = dt.Rows[i]["locationTo"].ToString();
                row.Cells["dgvl_loctodesc"].Value = cbo_stocklocation.Text;
                cbo_stocklocation.SelectedValue = dt.Rows[i]["locationFrom"].ToString();
                row.Cells["dgvl_locfromdesc"].Value = cbo_stocklocation.Text;
                
                row.Cells["dgvl_trn_type"].Value = dt.Rows[i]["trn_type"].ToString();
                if (dt.Rows[i]["is_receiving"].ToString() == "1")
                {
                    row.Cells["dgvl_is_receive"].Value = "YES";
                }
                else
                {
                    row.Cells["dgvl_is_receive"].Value = "NO";
                }
               
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
                    dgv_list["dgvl_location", i].Value = dt.Rows[i]["locationFrom"].ToString();
                    dgv_list["dgvl_whscode", i].Value = dt.Rows[i]["whs_code"].ToString();
                    dgv_list["dgvl_jrnlz", i].Value = dt.Rows[i]["jrnlz"].ToString();
                    dgv_list["dgvl_cancel", i].Value = dt.Rows[i]["cancel"].ToString();
                    dgv_list["dgvl_userid", i].Value = dt.Rows[i]["printed"].ToString();
                    dgv_list["dgvl_systemdate", i].Value = dt.Rows[i]["t_date"].ToString();
                    dgv_list["dgvl_systemtime", i].Value = dt.Rows[i]["t_time"].ToString();
                    dgv_list["dgvl_locationTo", i].Value = dt.Rows[i]["locationTo"].ToString();
//                    dgv_list["dgvl_locationTo", i].Value = dt.Rows[i]["locationTo"].ToString();

                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); } */
        }

        private void disp_itemlist(String inv_num)
        {
            DataTable dt;

            try
            {
                dt = db.get_stkinv_item_list(inv_num);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    F.Rows.Add();

                    //dgv_itemlist["dgvi_ln", i].Value = dt.Rows[i]["ln_num"].ToString();
                    //dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                    //dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    //dgv_itemlist["dgvi_unitid", i].Value = dt.Rows[i]["unit_id"].ToString(); // get unit name later
                    //dgv_itemlist["dgvi_unitdesc", i].Value = dt.Rows[i]["unit_id"].ToString();
                    //dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["recv_qty"].ToString();
                    //dgv_itemlist["dgvi_lnamt", i].Value = dt.Rows[i]["price"].ToString(); // get unit name later
                    //dgv_itemlist["dgvi_lnamt", i].Value = dt.Rows[i]["ln_amnt"].ToString();

                    F["dgvi_ln", i].Value = dt.Rows[i]["ln_num"].ToString();
                    F["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                    F["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    F["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                    F["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    F["dgvi_unitid", i].Value = dt.Rows[i]["unit"].ToString();
                    F["dgvi_unitdesc", i].Value = dt.Rows[i]["unit_desc"].ToString();
                    F["dgvi_qty", i].Value = dt.Rows[i]["recv_qty"].ToString();
                    F["dgvi_price", i].Value = dt.Rows[i]["price"].ToString();
                    F["dgvi_lnamt", i].Value = dt.Rows[i]["ln_amnt"].ToString();
                    F["dgvi_notes", i].Value = dt.Rows[i]["notes"].ToString();
                    F["dgvi_transpo", i].Value = dt.Rows[i]["transpo"].ToString();

                    
                    if (dt.Rows[i]["is_receiving"].ToString() == "0")
                    {
                        F["dgvi_is_received", i].Value = "NO";
                    }
                    else
                    {
                        F["dgvi_is_received", i].Value = "YES";
                    }

                    if(is_receiving)
                    {
                        F["dgvi_received_qty", i].Value = dt.Rows[i]["receiving_qty"].ToString();
                    }
                    //dgv_itemlist["dgvi_locationFrom", i].Value = dt.Rows[i]["locationFrom"].ToString();
                    //dgv_itemlist["dgvi_locationTo", i].Value = dt.Rows[i]["locationTo"].ToString();

                    if (isnew == false)
                    {
                        F["dgvi_oldqty", i].Value = dt.Rows[i]["org_qty"].ToString();
                    }
                    else
                    {
                        F["dgvi_oldqty", i].Value = "0.00";
                    }


                    //if (String.IsNullOrEmpty(dt.Rows[i]["cht_code"].ToString()) == false)
                    //{
                    //    cbo_contraacct.SelectedValue = dt.Rows[i]["cht_code"].ToString();
                    //}
                    //if (String.IsNullOrEmpty(dt.Rows[i]["cnt_code"].ToString()) == false)
                    //{
                    //    cbo_costcenter.SelectedValue = dt.Rows[i]["cnt_code"].ToString();
                    //}
                }
            }
            catch (Exception) { }
        }

        public void disp_total()
        {
            Double total = 0.00;

            try
            {
                for (int i = 0; i < F.Rows.Count - 1; i++)
                {
                    total += gm.toNormalDoubleFormat(F["dgvi_lnamt", i].Value.ToString());
                }
            }
            catch (Exception) { }

            lbl_total.Text = gm.toAccountingFormat(total);
        }

        private void btn_itemadd_Click(object sender, EventArgs e)
        {
            int lastrow = 0;
            isnew_item = true;

            try
            {
                if (isnew == false)
                {
                    lastrow = F.Rows.Count - 2;
                    this.lnno_last = int.Parse(F["dgvi_ln", lastrow].Value.ToString());
                    inc_lnno();
                } else
                {
                    if (F.Rows.Count == 1)
                    {
                        lnno_last = 0;
                        inc_lnno();
                    }
                    else
                    {
                        lastrow = F.Rows.Count - 2;
                        lnno_last = int.Parse(F["dgvi_lnno", lastrow].Value.ToString());
                        inc_lnno();
                    }
                }
            }
            catch (Exception) { }

            _frm_eis = new z_enter_item_simple(this, isnew_item, lnno_last);
            _frm_eis.ShowDialog();
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            int r = 0;
            isnew_item = false;
            
            try
            {
                if (F.Rows.Count > 0)
                {
                    r = F.CurrentRow.Index;

                    if(is_receiving)
                    {
                        _frm_eis = new z_enter_item_simple(this, isnew_item, r,is_receiving);
                    }
                    else
                    {
                        _frm_eis = new z_enter_item_simple(this, isnew_item, r);
                    }
                  
                    _frm_eis.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch { MessageBox.Show("No item selected.");  }            
        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            String qty = "";

            try
            {
                if (F.Rows.Count > 1)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove?", "Cancel", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        r = F.CurrentRow.Index;

                        if (isnew == false)
                        {
                            //code = F["dgvi_itemcode", r].Value.ToString();
                            //qty = F["dgvi_oldqty", r].Value.ToString();
                            //dgv_delitem.Rows.Add(code, qty);
                        }

                        F.Rows.RemoveAt(r);
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
            Boolean success = false;
            String notificationText = "has added: ";
            z_Notification notify = new z_Notification();
            String code,  loc, dt_trnx, reference, stk_ref = "", stk_po_so = "", locfrm = "", locto = "";

            String col = "", val = "";
            String notifyadd = null;
            String table = "rechdr";
            String tableln = "reclne";
            int r;

            

            if (String.IsNullOrEmpty(txt_desc.Text))
            {
                MessageBox.Show("Please select the description field.");
            }
            else if (cbo_stocklocation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select stock location field.");
            }
            else if (cbo_stkLocTo.SelectedValue.ToString().Equals(cbo_stocklocation.SelectedValue.ToString()))
            {
                MessageBox.Show("Same Location, Can not save!");
            }
            else
            {
                code = txt_code.Text;
                loc = cbo_stocklocation.SelectedValue.ToString();
                dt_trnx = dtp_trnxdt.Value.ToString("yyyy-MM-dd");
                reference = txt_desc.Text;
                stk_ref = stk_trns_type + "#" + code;
                locfrm = cbo_stocklocation.SelectedValue.ToString();
                locto = cbo_stkLocTo.SelectedValue.ToString();

                String msg = is_receiving ? "Receiving Stock Transaction" : "Stock Transaction";

                DialogResult dr = MessageBox.Show("Are you sure you want to save this " + msg + "?", "Saving", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {

                    if (isnew)
                    {
                        notificationText = "has added: ";

                        code = db.get_pk("trf_num");
                        col = "rec_num, _reference, trnx_date, recipient, t_date, t_time, whs_code, trn_type,  \"locationFrom\", \"locationTo\", cancel";
                        val = "'" + code + "', " + db.str_E(reference) + ", '" + dt_trnx + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + db.get_systemtime() + "', '" + loc + "', '" + trns_type + "', '" + locfrm + "', '" + locto + "', 'N'";

                        if (db.InsertOnTable(table, col, val))
                        {
                            notifyadd = add_items(tableln, code, dt_trnx, stk_ref, stk_po_so, locfrm, locto);

                            if (String.IsNullOrEmpty(notifyadd) == false)
                            {
                                notificationText += notifyadd;
                                notificationText += Environment.NewLine + " with #" + code;
                                notify.saveNotification(notificationText, "STOCK TRANSFER");
                                db.set_pkm99("trf_num", db.get_nextincrementlimitchar(code, 8));
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

                        #region OLD CODES
                        //code = db.get_pk("trf_num");
                        //col = "rec_num, reference, trnx_date, whs_code, trn_type, recipient, t_date, t_time";

                        //val = "'" + code + "', " + db.str_E(reference) + ", '" + dt_trnx + "', " + loc + ", '" + trns_type + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "'";

                        //if (db.InsertOnTable("rechdr", col, val))
                        //{
                        //    db.set_pkm99("trf_num", db.get_nextincrementlimitchar(code, 8));

                        //    col2 = "rec_num, ln_num, item_code, item_desc, unit_id, recv_qty, price, ln_amnt, cht_code, cnt_code";
                        //    col3 = "item_code, item_desc, unit, trnx_date, reference, po_so, qty_in, price, whs_code, trn_type";

                        //    for (r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
                        //    {
                        //        i_lnno = dgv_itemlist["dgvi_ln", r].Value.ToString();
                        //        i_itemcode = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                        //        i_itemdesc = dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
                        //        i_qty = dgv_itemlist["dgvi_qty", r].Value.ToString();
                        //        i_unitid = dgv_itemlist["dgvi_unitid", r].Value.ToString();
                        //        i_price = dgv_itemlist["dgvi_price", r].Value.ToString();
                        //        i_lnamnt = dgv_itemlist["dgvi_lnamt", r].Value.ToString();

                        //        val2 = "'" + code + "', '" + i_lnno + "', " + db.str_E(i_itemcode) + ", " + db.str_E(i_itemdesc) + ", '" + i_unitid + "', " + db.castToInteger(i_qty) + ", " + db.castToInteger(i_price) + ", " + db.castToInteger(i_lnamnt) + ", '" + at_code + "', '" + cc_code + "'";
                        //        val3 = db.str_E(i_itemcode) + ", " + db.str_E(i_itemdesc) + ", '" + i_unitid + "', '" + dt_trnx + "', " + db.str_E(reference) + ", '" + code + "', " + i_qty + ", " + i_price + ", '" + loc + "', '" + stk_trns_type + "'";

                        //        db.InsertOnTable("reclne", col2, val2);
                        //        db.InsertOnTable("stkcrd", col3, val3);
                        //        db.upd_item_quantity_onhand(i_itemcode, Convert.ToDouble(i_qty), stk_trns_type);
                        //    }

                        //    success = true;
                        //}
                        //else
                        //{
                        //    db.DeleteOnTable("rechdr", "rec_num='" + code + "'");
                        //    db.DeleteOnTable("reclne", "rec_num='" + code + "'");
                        //    db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");
                        //    db.set_pkm99("trf_num", code); // return back to old code

                        //    success = false;
                        //    MessageBox.Show("Failed on saving.");
                        //}

                        #endregion
                    }
                    else
                    {
                        #region OLD CODE
                        //col = "reference=" + db.str_E(reference) + ", trnx_date='" + dt_trnx + "', whs_code='" + loc + "', trn_type='" + trns_type + "', recipient='" + GlobalClass.username + "', t_date='" + db.get_systemdate("") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "'";

                        //if (db.UpdateOnTable("rechdr", col, "rec_num='" + code + "'"))
                        //{
                        //    db.DeleteOnTable("reclne", "rec_num='" + code + "'");
                        //    db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                        //    col2 = "rec_num, ln_num, item_code, item_desc, unit_id, recv_qty, price, ln_amnt, cht_code, cnt_code";
                        //    col3 = "item_code, item_desc, unit, trnx_date, reference, po_so, qty_in, price, whs_code, trn_type";

                        //    for (r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
                        //    {
                        //        i_lnno = dgv_itemlist["dgvi_ln", r].Value.ToString();
                        //        i_itemcode = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                        //        i_itemdesc = dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
                        //        i_qty = dgv_itemlist["dgvi_qty", r].Value.ToString();
                        //        i_unitid = dgv_itemlist["dgvi_unitid", r].Value.ToString();
                        //        i_price = dgv_itemlist["dgvi_price", r].Value.ToString();
                        //        i_lnamnt = dgv_itemlist["dgvi_lnamt", r].Value.ToString();
                        //        i_oldqty = dgv_itemlist["dgvi_oldqty", r].Value.ToString();

                        //        val2 = "'" + code + "', '" + i_lnno + "', " + db.str_E(i_itemcode) + ", " + db.str_E(i_itemdesc) + ", '" + i_unitid + "', " + db.castToInteger(i_qty) + ", " + db.castToInteger(i_price) + ", " + db.castToInteger(i_lnamnt) + ", " + at_code + ", '" + cc_code + "'";
                        //        val3 = db.str_E(i_itemcode) + ", " + db.str_E(i_itemdesc) + ", '" + i_unitid + "', '" + dt_trnx + "', " + db.str_E(reference) + ", '" + code + "', " + i_qty + ", " + i_price + ", '" + loc + "', '" + stk_trns_type + "'";

                        //        db.InsertOnTable("reclne", col2, val2);
                        //        db.InsertOnTable("stkcrd", col3, val3);
                        //        db.upd_item_quantity_onhand(i_itemcode, gm.toNormalDoubleFormat(i_qty) - gm.toNormalDoubleFormat(i_oldqty), stk_trns_type);
                        //    }

                        //    //adjust the item quantity on hand from deleted item
                        //    for (int di = 0; di < dgv_delitem.Rows.Count - 1; di++)
                        //    {
                        //        di_code = dgv_delitem["dgvdi_code", di].Value.ToString();
                        //        di_qty = dgv_delitem["dgvdi_qty", di].Value.ToString();

                        //        db.upd_item_quantity_onhand(di_code, gm.toNormalDoubleFormat(di_qty) * -1, stk_trns_type);
                        //    }

                        //    success = true;
                        //}
                        //else
                        //{
                        //    success = false;
                        //    MessageBox.Show("Failed on saving.");
                        //}

                        #endregion

                        if (!is_receiving)
                        {
                            notificationText = "has updated: ";
                            col = "rec_num='" + code + "', _reference='" + reference + "', trnx_date='" + dt_trnx + "', recipient='" + GlobalClass.username + "', t_date='" + db.get_systemdate("") + "', t_time='" + db.get_systemtime() + "', whs_code='" + loc + "', trn_type='" + trns_type + "', cancel='N'";

                            if (db.UpdateOnTable(table, col, "rec_num='" + code + "'"))
                            {
                                db.DeleteOnTable(tableln, "rec_num='" + code + "'");
                                db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                                notifyadd = add_items(tableln, code, dt_trnx, stk_ref, stk_po_so, locfrm, locto);

                                if (String.IsNullOrEmpty(notifyadd) == false)
                                {
                                    notificationText += notifyadd;
                                    notificationText += Environment.NewLine + " with #" + code;
                                    notify.saveNotification(notificationText, "STOCK TRANSFER");
                                    success = true;
                                }
                                else
                                {
                                    success = false;
                                }
                            }
                        }
                        else
                        {
                            col = "is_receiving='1'";
                            if (db.UpdateOnTable(table, col, "rec_num='" + code + "'"))
                            {
                                db.DeleteOnTable(tableln, "rec_num='" + code + "'");
                                db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                                notifyadd = add_items(tableln, code, dt_trnx, stk_ref, stk_po_so, locfrm, locto);

                                if (String.IsNullOrEmpty(notifyadd) == false)
                                {
                                    notificationText += notifyadd;
                                    notificationText += Environment.NewLine + " with #" + code;
                                    notify.saveNotification(notificationText, "STOCK TRANSFER");
                                    success = true;
                                }
                                else
                                {
                                    success = false;
                                }
                            }

                        }

                        if (success == false)
                        {
                            MessageBox.Show("Failed on saving.");
                        }
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

        private String add_items(String tableln, String code, String dt_trnx, String stk_ref, String stk_po_so, String locfrm, String locto)
        {
            String notificationText = null;
            String i_lnno = "", i_itemcode = "", i_itemdesc = "", i_qty = "", i_unitid = "", i_price = "", i_lnamnt = "", i_lnvat = "", i_expiry = "", i_lotnum = "", i_fcp = "0.00", i_part_no = "", i_received_qty = "0.00", i_transpo = "0.00", i_is_receiving = "0", i_notes = "";
            String val2 = "";
            String col2 = "rec_num, ln_num, item_code, item_desc, unit, recv_qty, price, ln_amnt,part_no,is_receiving,receiving_qty,notes,transpo";
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";
            
            for (int r = 0; r < F.Rows.Count - 1; r++)
            {
                i_lnno = F["dgvi_ln", r].Value.ToString();
                i_part_no = F["dgvi_part_no", r].Value.ToString();
                i_itemcode = F["dgvi_itemcode", r].Value.ToString();
                i_itemdesc = F["dgvi_itemdesc", r].Value.ToString();
                i_qty = gm.toDoubleStr(F["dgvi_qty", r].Value.ToString());
                i_unitid = F["dgvi_unitid", r].Value.ToString();
                i_price = gm.toDoubleStr(F["dgvi_price", r].Value.ToString());
                i_lnamnt = gm.toDoubleStr(F["dgvi_lnamt", r].Value.ToString());
                i_fcp = db.get_item_fcp(i_itemcode).ToString("0.00");
                i_notes = F["dgvi_notes", r].Value.ToString();
                i_transpo = gm.toDoubleStr(F["dgvi_transpo", r].Value.ToString());
              
                if(is_receiving)
                {
                    i_is_receiving = "1";
                }
                
                if(is_receiving)
                {
                    try
                    {
                        i_received_qty = F["dgvi_received_qty", r].Value.ToString();
                    }catch(Exception ex)
                    {
                        i_received_qty = "0.00";
                    }
                }
                val2 = "'" + code + "', '" + i_lnno + "', '" + i_itemcode + "', " + db.str_E(i_itemdesc) + ", '" + i_unitid + "', '" + i_qty + "', '" + i_price + "', '" + i_lnamnt + "','" + i_part_no + "','" + i_is_receiving + "','" + i_received_qty + "','" + i_notes + "','" + i_transpo + "'";
                

                if (db.InsertOnTable(tableln, col2, val2))
                {
                    stk_qty_in = "0.00";
                    stk_qty_out = "0.00";

                    //Transfer From Negative = Stock In
                    if (gm.toNormalDoubleFormat(i_qty) < 0)
                        stk_qty_in = Math.Abs(gm.toNormalDoubleFormat(i_qty)).ToString();
                    //Transfer From Positive = Stock Out
                    else
                        stk_qty_out = i_qty;

                    //Stock Transfer From is qty out
                    db.save_to_stkcard(i_itemcode, i_itemdesc, i_unitid, dt_trnx, stk_ref, stk_po_so, stk_qty_in, stk_qty_out, i_fcp, i_price, locfrm, "", "", stk_trns_type, "", "", "");

                    //Stock Transfer To is qty in
                    db.save_to_stkcard(i_itemcode, i_itemdesc, i_unitid, dt_trnx, stk_ref, stk_po_so, stk_qty_out, stk_qty_in, i_fcp, i_price, locto, "", "", stk_trns_type, "", "", "");
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
                    i = F.Rows.Add();
                }
                else
                {
                    i = F.CurrentRow.Index; 
                }

                F["dgvi_ln", i].Value = dt.Rows[0]["dgvi_ln"].ToString();
                F["dgvi_part_no", i].Value = dt.Rows[0]["dgvi_part_no"].ToString();
                F["dgvi_itemcode", i].Value = dt.Rows[0]["dgvi_itemcode"].ToString();
                F["dgvi_itemdesc", i].Value = dt.Rows[0]["dgvi_itemdesc"].ToString();
                F["dgvi_unitdesc", i].Value = dt.Rows[0]["dgvi_unitdesc"].ToString();
                F["dgvi_unitid", i].Value = dt.Rows[0]["dgvi_unitid"].ToString();
                F["dgvi_qty", i].Value = dt.Rows[0]["dgvi_qty"].ToString();
                F["dgvi_price", i].Value = dt.Rows[0]["dgvi_price"].ToString();
                F["dgvi_lnamt", i].Value = dt.Rows[0]["dgvi_lnamt"].ToString();
                F["dgvi_notes", i].Value = dt.Rows[0]["dgvi_notes"].ToString();
                F["dgvi_transpo", i].Value = dt.Rows[0]["dgvi_transpo"].ToString();

                
                if(is_receiving)
                {
                    F["dgvi_received_qty", i].Value = dt.Rows[0]["dgvi_received_qty"].ToString();
                }
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

            val = F["dgvi_ln", currow].Value.ToString();

            return val;
        }

        public String get_partno(int currow)
        {
            String val = "";

            val = F["dgvi_part_no", currow].Value.ToString();

            return val;
        }
        public String get_dgvi_itemcode(int currow)
        {
            String val = "";

            val = F["dgvi_itemcode", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_itemdesc(int currow)
        {
            String val = "";

            val = F["dgvi_itemdesc", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_unitid(int currow)
        {
            String val = "";

            val = F["dgvi_unitid", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_qty(int currow)
        {
            String val = "";

            val = F["dgvi_qty", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_recqty(int currow)
        {
            String val = "";

            val = F["dgvi_received_qty", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_price(int currow)
        {
            String val = "";

            val = F["dgvi_price", currow].Value.ToString();

            return val;
        }

        public String get_dgvi_lnamt(int currow)
        {
            String val = "";

            val = F["dgvi_lnamt", currow].Value.ToString();

            return val;
        }
        public String get_dgvi_notes(int currow)
        {
            String val = "";

            val = F["dgvi_notes", currow].Value.ToString();

            return val;
        }
        public String get_dgvi_transpo(int currow)
        {
            String val = "";

            val = F["dgvi_transpo", currow].Value.ToString();

            return val;
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

        private void groupBox5_Enter(object sender, EventArgs e)
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

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_list();
            /*
            String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
            String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");

            DataTable dt;

            dgv_list.Rows.Clear();
            if (comboBox1.Text == string.Empty)
            {
                dt = db.QueryBySQLCode("SELECT * FROM rssys.rechdr WHERE rec_num LIKE '" + textBox15.Text + "%' OR supl_code LIKE '" + textBox15.Text + "%' OR supl_name LIKE '" + textBox15.Text + "%' OR _reference LIKE '" + textBox15.Text + "%' OR recipient LIKE '" + textBox15.Text + "%' AND trn_type = 'T' AND trnx_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "' ORDER BY rec_num ASC");
            }
            else if (comboBox1.Text == "clear filter")
            {
                dt = db.QueryOnTableWithParams("rechdr", "*", "trn_type = 'T' AND trnx_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "'", "ORDER BY rec_num ASC");

            }
            else
            {
                dt = db.QueryBySQLCode("SELECT * FROM rssys.rechdr WHERE " + comboBox1.Text + " LIKE '" + textBox15.Text + "%' AND trn_type = 'T' AND trnx_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "' ORDER BY rec_num ASC");

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int r = dgv_list.Rows.Add();
                DataGridViewRow row = dgv_list.Rows[r];

                row.Cells["dgvl_code"].Value = dt.Rows[i]["rec_num"].ToString();
                row.Cells["dgvl_description"].Value = dt.Rows[i]["_reference"].ToString();
                row.Cells["dgvl_trnxdate"].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                row.Cells["dgvl_whscode"].Value = dt.Rows[i]["whs_code"].ToString();
                row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                row.Cells["dgvl_userid"].Value = dt.Rows[i]["printed"].ToString();
                row.Cells["dgvl_systemdate"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                row.Cells["dgvl_systemtime"].Value = dt.Rows[i]["t_time"].ToString();
                row.Cells["dgvl_locationTo"].Value = dt.Rows[i]["locationTo"].ToString();
                row.Cells["dgvl_location"].Value = dt.Rows[i]["locationFrom"].ToString();

                cbo_stocklocation.SelectedValue = dt.Rows[i]["locationTo"].ToString();
                row.Cells["dgvl_loctodesc"].Value = cbo_stocklocation.Text;
                cbo_stocklocation.SelectedValue = dt.Rows[i]["locationFrom"].ToString();
                row.Cells["dgvl_locfromdesc"].Value = cbo_stocklocation.Text;

                row.Cells["dgvl_trn_type"].Value = dt.Rows[i]["trn_type"].ToString();
                if (dt.Rows[i]["is_receiving"].ToString() == "1")
                {
                    row.Cells["dgvl_is_receive"].Value = "YES";
                }
                else
                {
                    row.Cells["dgvl_is_receive"].Value = "NO";
                }

            }*/
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

            int rowIndex = -1;
            String typname = "dgvl_description";

            try
            {
                String searchValue = textBox15.Text.ToUpper();

                if (comboBox1.SelectedIndex == 0)
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
                        row.Cells["dgvl_whscode"].Value = dt.Rows[i]["whs_code"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                        row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                        row.Cells["dgvl_userid"].Value = dt.Rows[i]["printed"].ToString();
                        row.Cells["dgvl_systemdate"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_systemtime"].Value = dt.Rows[i]["t_time"].ToString();
                        row.Cells["dgvl_locationTo"].Value = dt.Rows[i]["locationTo"].ToString();
                        row.Cells["dgvl_location"].Value = dt.Rows[i]["locationFrom"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        cbo_stocklocation.SelectedValue = dt.Rows[i]["locationTo"].ToString();
                        row.Cells["dgvl_loctodesc"].Value = cbo_stocklocation.Text;
                        cbo_stocklocation.SelectedValue = dt.Rows[i]["locationFrom"].ToString();
                        row.Cells["dgvl_locfromdesc"].Value = cbo_stocklocation.Text;
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_trn_type"].Value = dt.Rows[i]["trn_type"].ToString();
                        if (dt.Rows[i]["is_receiving"].ToString() == "1")
                        {
                            row.Cells["dgvl_is_receive"].Value = "YES";
                        }
                        else
                        {
                            row.Cells["dgvl_is_receive"].Value = "NO";
                        }
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

            dt = db.QueryOnTableWithParams("rechdr", "*", "(COALESCE(cancel,'N')='N' OR COALESCE(cancel,'')='') AND trn_type = 'T' AND t_date between " + "'" + dtp_frm.getStringDate() + "' and '" + dtp_to.getStringDate() + "'  " + WHERE, ORDERBY);

            return dt;
        }
    }
}