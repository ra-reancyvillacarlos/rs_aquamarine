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
    public partial class i_Purch_Orders : Form
    {
        String stk_trns_type = "PR";
        private z_enter_ret_pur_item _frm_rpi;
        DateTime dt_to;
        DateTime dt_frm;
        dbInv db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        int lnno_last = 1;
        
        Boolean isReady = false;
        Boolean bgworker_cancel = false;

        public i_Purch_Orders()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbInv();

            gc.load_supplier(cbo_supplier);
            gc.load_stocklocation(cbo_stocklocation);


            dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));

            disp_list();
            isReady = true;  
        }

        private void i_PurchaseReturn_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void dtp_frm_ValueChanged(object sender, EventArgs e)
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

        private void frm_clear()
        {
            try
            {
                txt_code.Text = "";
                txt_desc.Text = "";
                cbo_supplier.SelectedIndex = -1;
                cbo_stocklocation.SelectedIndex = -1;
                dtp_trnxdt.Value = Convert.ToDateTime(db.get_systemdate(""));
                lbl_total.Text = "0.00";

                dgv_itemlist.Rows.Clear();
                dgv_delitem.Rows.Clear();
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
            String code = "", whscode = "", cccode = "", atcode = "",canc="";
            isnew = false;

            try
            {

                r = dgv_list.CurrentRow.Index;

                code = (dgv_list["dgvl_code", r].Value ?? "").ToString();

                if (!String.IsNullOrEmpty(code))
                {
                    try { canc = dgv_list["dgvl_cancel", r].Value.ToString(); }
                    catch { }

                    if (canc == "Y")
                    {
                        MessageBox.Show("RET# " + code + " is already cancelled. Can not be updated.");
                    }
                    else
                    {
                        try
                        {
                            txt_code.Text = code;

                            if (dgv_list["dgvl_reference", r].Value != null)
                            {
                                txt_desc.Text = dgv_list["dgvl_reference", r].Value.ToString();
                            }

                            if (dgv_list["dgvl_whscode", r].Value != null)
                            {
                                whscode = dgv_list["dgvl_whscode", r].Value.ToString();
                                cbo_stocklocation.SelectedValue = whscode;
                            }

                            if (dgv_list["supp_code", r].Value != null)
                            {
                                cbo_supplier.SelectedValue = dgv_list["supp_code", r].Value.ToString();
                            }

                            dtp_trnxdt.Value = Convert.ToDateTime(dgv_list["dgvl_trnxdate", r].Value.ToString());
                        }
                        catch (Exception er) { MessageBox.Show("No invoice selected."); }

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
                        code = dgv_list["dgvl_code", r].Value.ToString();

                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this P.O.# " + code + "?", "Confirm", MessageBoxButtons.YesNo); ;

                        if (dialogResult == DialogResult.Yes)
                        {

                            if (db.UpdateOnTable("prethdr", "supl_name='CANCELLED' || '-' ||supl_name, cancel='Y'", "pret_num='" + code + "'"))
                            {
                                db.DeleteOnTable("pretlne", "pret_num='" + code + "'");
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
                String inv_num = dgv_list["dgvl_code", r].Value.ToString();
                if (!String.IsNullOrEmpty(inv_num))
                {
                    Report rpt = new Report();
                    rpt.print_purchase_return(inv_num);
                    rpt.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
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
            DataTable dt = db.get_retpolist(dtp_frm.Value, dtp_to.Value);

            try
            {
                dgv_list.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_list.Rows.Add();

                    dgv_list["dgvl_code", i].Value = dt.Rows[i]["pret_num"].ToString();
                    dgv_list["dgvl_description", i].Value = dt.Rows[i]["supl_name"].ToString();
                    dgv_list["dgvl_trnxdate", i].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(),"");
                    dgv_list["supp_code", i].Value = dt.Rows[i]["supl_code"].ToString();

                    cbo_stocklocation.SelectedValue= dt.Rows[i]["whs_code"].ToString();
                    dgv_list["dgvl_location", i].Value = cbo_stocklocation.Text;
                    dgv_list["dgvl_whscode", i].Value = dt.Rows[i]["whs_code"].ToString();
                    dgv_list["dgvl_jrnlz", i].Value = dt.Rows[i]["jrnlz"].ToString();
                    dgv_list["dgvl_cancel", i].Value = dt.Rows[i]["cancel"].ToString();
                    dgv_list["dgvl_userid", i].Value = dt.Rows[i]["user_id"].ToString();
                    dgv_list["dgvl_systemdate", i].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                    dgv_list["dgvl_systemtime", i].Value = dt.Rows[i]["t_time"].ToString();
                    dgv_list["dgvl_reference", i].Value = dt.Rows[i]["reference"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }*/
        }

        private void disp_itemlist(String inv_num)
        {
            DataTable dt;

            dt = db.get_stkinv_item_list(inv_num);

            dt = db.get_retpo_item_list(inv_num);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgv_itemlist.Rows.Add();

                dgv_itemlist["dgvi_ln", i].Value = dt.Rows[i]["ln_num"].ToString();
                dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString();
                dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                dgv_itemlist["dgvi_unitid", i].Value = dt.Rows[i]["unit"].ToString(); // get unit name later
                dgv_itemlist["dgvi_unitdesc", i].Value = db.get_item_unit_desc(dt.Rows[i]["unit"].ToString());
                dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["ret_qty"].ToString();
                dgv_itemlist["dgvi_price", i].Value = dt.Rows[i]["price"].ToString(); // get unit name later
                dgv_itemlist["dgvi_lnamt", i].Value = dt.Rows[i]["ln_amnt"].ToString();
                dgv_itemlist["dgvi_vat", i].Value = dt.Rows[i]["vat_code"].ToString();
               
                if (isnew == false)
                {
                    dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["ret_qty"].ToString();
                    //                        dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["inv_qty"].ToString();
                }
                else
                {
                    dgv_itemlist["dgvi_oldqty", i].Value = "0.00";
                }

                try
                {
                    cbo_supplier.SelectedValue = dt.Rows[i]["cht_code"].ToString();
                }
                catch (Exception ex)
                {

                }
               
                
                
                   
                
            }
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
            lnno_last = dgv_itemlist.RowCount;

            isnew_item = true;

            _frm_rpi = new z_enter_ret_pur_item(this, isnew_item, lnno_last);
            _frm_rpi.ShowDialog();
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

                    _frm_rpi = new z_enter_ret_pur_item(this, isnew_item, r);
                    _frm_rpi.ShowDialog();
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
            Boolean success = false;
            String notificationText = "has added: ";
            z_Notification notify = new z_Notification();
            String code, supp_id, supp_name, terms, loc, dt_trnx, reference, vat_code, purc_ord, at_code, cc_code, debt_code, stk_ref="", stk_po_so = "";

            
            String i_oldqty = "", trns_type="";
            String di_code = "", di_qty = "0.00";
            String col = "", val = "", col2 = "", val2 = "", col3 = "", val3 = "", add_col = "", add_val = "";
            String table = "prethdr";
            String tableln = "pretlne";
            String notifyadd = null;
            int r;

            if (String.IsNullOrEmpty(txt_desc.Text))
            {
                MessageBox.Show("Please select the description field.");
            }
            else if (cbo_supplier.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the contra account field.");
            }
            else if (cbo_stocklocation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select stock location field.");
            }
            else
            {
                code = txt_code.Text;
                supp_id = cbo_supplier.SelectedValue.ToString();
                supp_name = cbo_supplier.Text;                
                dt_trnx = dtp_trnxdt.Value.ToString("yyyy-MM-dd");
                loc = cbo_stocklocation.SelectedValue.ToString();
                reference = txt_desc.Text;
                stk_ref = stk_trns_type + "#" + code;

                if (isnew)
                {
                    code = db.get_pk("pret_num"); 
                   
                    col = "pret_num, supl_code, supl_name, trnx_date, reference, user_id, t_date, t_time, whs_code";
                    val = "'" + code + "', '" + supp_id + "', '" + supp_name + "', '" + dt_trnx + "', '" + reference + "', '" + GlobalClass.username + "', '" + db.get_systemdate("yyyy-MM-dd") + "', '" + db.get_systemtime() + "', '" + loc + "'";

                    if(db.InsertOnTable(table, col, val))
                    {
                        notifyadd = add_items(tableln, code, dt_trnx, stk_ref, stk_po_so, loc, supp_id, supp_name);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            db.set_pkm99("pret_num", db.get_nextincrementlimitchar(code, 8));
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "PURCHASE RETURN");
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                    }
                    
                    if(success == false)
                    {
                        db.DeleteOnTable(table, "pret_num='" + code + "'");
                        db.DeleteOnTable(tableln, "pret_num='" + code + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "pret_num='" + code + "', supl_code='" + supp_id + "', supl_name='" + supp_name + "', trnx_date='" + dt_trnx + "', reference='" + reference + "', user_id='" + GlobalClass.username + "', t_date='" + db.get_systemdate("yyyy-MM-dd") + "', t_time='" + db.get_systemtime() + "', whs_code='" + loc + "'";

                     if (db.UpdateOnTable(table, col, "pret_num='" + code + "'"))
                     {
                        db.DeleteOnTable("pretlne", "pret_num='" + code + "'");
                        db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                        notifyadd = add_items(tableln, code, dt_trnx, stk_ref, stk_po_so, loc, supp_id, supp_name);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "PURCHASE RETURN");
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                    }
                     
                    if(success == false)
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

        private String add_items(String tableln, String code, String dt_trnx, String stk_ref, String stk_po_so, String loc, String supp_id, String supp_name)
        {
            String notificationText = null;
            String i_lnno = "", i_itemcode = "", i_itemdesc = "", i_qty = "", i_unitid = "", i_price = "", i_lnamnt = "", i_fcp ="",i_part_no = "", i_vat_code="";
            String val2 = "";
            String col2 = "pret_num, ln_num, item_code, item_desc, unit, ret_qty, price, ln_amnt,part_no, vat_code";

            for (int r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
            {
                i_lnno = dgv_itemlist["dgvi_ln", r].Value.ToString();
                i_part_no = dgv_itemlist["dgvi_part_no", r].Value.ToString();
                i_itemcode = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                i_itemdesc = dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
                i_vat_code = dgv_itemlist["dgvi_vat", r].Value.ToString();
                i_qty = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_qty", r].Value.ToString()).ToString("00.00");
                i_unitid = dgv_itemlist["dgvi_unitid", r].Value.ToString();
                i_price = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_price", r].Value.ToString()).ToString("00.00");
                i_lnamnt = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_lnamt", r].Value.ToString()).ToString("00.00");
                i_fcp = db.get_item_fcp(i_itemcode).ToString("0.00");

                val2 = "'" + code + "', '" + i_lnno + "', '" + i_itemcode + "', " + db.str_E(i_itemdesc) + ", '" + i_unitid + "', '" + i_qty + "', '" + i_price + "', '" + i_lnamnt + "','" + i_part_no + "','" + i_vat_code + "'";

                if (db.InsertOnTable(tableln, col2, val2))
                {
                    db.save_to_stkcard(i_itemcode, i_itemdesc, i_unitid, dt_trnx, stk_ref, stk_po_so, "0", i_qty, i_fcp, i_price, loc, supp_id, supp_name, stk_trns_type, "", "", "");

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
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Cancel", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
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
                dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[0]["dgvi_itemcode"].ToString();
                dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[0]["dgvi_itemdesc"].ToString();
                dgv_itemlist["dgvi_unitdesc", i].Value = dt.Rows[0]["dgvi_unitdesc"].ToString();
                dgv_itemlist["dgvi_unitid", i].Value = dt.Rows[0]["dgvi_unitid"].ToString();
                dgv_itemlist["dgvi_qty", i].Value = dt.Rows[0]["dgvi_qty"].ToString();
                dgv_itemlist["dgvi_price", i].Value = dt.Rows[0]["dgvi_price"].ToString();
                dgv_itemlist["dgvi_lnamt", i].Value = dt.Rows[0]["dgvi_lnamt"].ToString();
                dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[0]["dgvi_part_no"].ToString();
                dgv_itemlist["dgvi_vat", i].Value = dt.Rows[0]["dgvi_vat"].ToString();

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

            if (val == null)
                val = "0";

            return val;
        }
        public String get_dgvi_vat(int currow)
        {
            String val = "";

            val = dgv_itemlist["dgvi_vat", currow].Value.ToString();

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
            DataTable dt;
            String schema = "rssys";
            if (comboBox1.Text == string.Empty)
            {
                dt = db.QueryBySQLCode("SELECT p.pret_num, p.supl_code, p.supl_name, p.reference, TO_CHAR(p.trnx_date, 'MM/DD/YYYY') AS trnx_date, p.whs_code, p.jrnlz, p.cancel, p.user_id, TO_CHAR(p.t_date, 'MM/DD/YYYY') AS t_date, p.t_time FROM rssys.prethdr p LEFT JOIN " + schema + ".m07 s ON s.c_code=p.supl_code LEFT JOIN " + schema + ".whouse w ON w.whs_code=p.whs_code WHERE  pret_num LIKE '" + textBox15.Text + "%' OR supl_code LIKE '" + textBox15.Text + "' OR supl_name LIKE '" + textBox15.Text + "%' OR reference LIKE '" + textBox15.Text + "%' AND  p.t_date between " + "'" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY pret_num");            
            }
            else if(comboBox1.Text=="clear filter")
            {
                dt = db.get_retpolist(dtp_frm.Value, dtp_to.Value);
            }
            else{
                String ss = "";
                dt = db.QueryBySQLCode("SELECT p.pret_num, p.supl_code, p.supl_name, p.reference, TO_CHAR(p.trnx_date, 'MM/DD/YYYY') AS trnx_date, p.whs_code, p.jrnlz, p.cancel, p.user_id, TO_CHAR(p.t_date, 'MM/DD/YYYY') AS t_date, p.t_time FROM rssys.prethdr p LEFT JOIN " + schema + ".m07 s ON s.c_code=p.supl_code LEFT JOIN " + schema + ".whouse w ON w.whs_code=p.whs_code WHERE  "+comboBox1.Text+" LIKE '"+textBox15.Text+"%' AND  p.t_date between " + "'" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ORDER BY pret_num");
            }
            

            try
            {
                dgv_list.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_list.Rows.Add();

                    dgv_list["dgvl_code", i].Value = dt.Rows[i]["pret_num"].ToString();
                    dgv_list["dgvl_description", i].Value = dt.Rows[i]["supl_name"].ToString();
                    dgv_list["dgvl_trnxdate", i].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                    dgv_list["supp_code", i].Value = dt.Rows[i]["supl_code"].ToString();

                    cbo_stocklocation.SelectedValue = dt.Rows[i]["whs_code"].ToString();
                    dgv_list["dgvl_location", i].Value = cbo_stocklocation.Text;
                    dgv_list["dgvl_whscode", i].Value = dt.Rows[i]["whs_code"].ToString();
                    dgv_list["dgvl_jrnlz", i].Value = dt.Rows[i]["jrnlz"].ToString();
                    dgv_list["dgvl_cancel", i].Value = dt.Rows[i]["cancel"].ToString();
                    dgv_list["dgvl_userid", i].Value = dt.Rows[i]["user_id"].ToString();
                    dgv_list["dgvl_systemdate", i].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                    dgv_list["dgvl_systemtime", i].Value = dt.Rows[i]["t_time"].ToString();
                    dgv_list["dgvl_reference", i].Value = dt.Rows[i]["reference"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }*/
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
            String typname = "dgvl_description";

            try
            {
                String searchValue = textBox15.Text.ToUpper();

                if (comboBox1.SelectedIndex == 0)
                {
                    typname = "dgvl_code";
                }
                else if(comboBox1.SelectedIndex == 1)
                {
                    typname = "dgvl_description";
                }
                else if(comboBox1.SelectedIndex == 2)
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
                        row.Cells["dgvl_code"].Value = dt.Rows[i]["pret_num"].ToString();
                        row.Cells["dgvl_description"].Value = dt.Rows[i]["supl_name"].ToString();
                        row.Cells["dgvl_trnxdate"].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                        row.Cells["supp_code"].Value = dt.Rows[i]["supl_code"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        cbo_stocklocation.SelectedValue = dt.Rows[i]["whs_code"].ToString();
                        row.Cells["dgvl_location"].Value = cbo_stocklocation.Text;
                        row.Cells["dgvl_whscode"].Value = dt.Rows[i]["whs_code"].ToString();
                        row.Cells["dgvl_jrnlz"].Value = dt.Rows[i]["jrnlz"].ToString();
                        row.Cells["dgvl_cancel"].Value = dt.Rows[i]["cancel"].ToString();
                    }));
                    dgv_list.Invoke(new Action(() =>
                    {
                        row.Cells["dgvl_userid"].Value = dt.Rows[i]["user_id"].ToString();
                        row.Cells["dgvl_systemdate"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                        row.Cells["dgvl_systemtime"].Value = dt.Rows[i]["t_time"].ToString();
                        row.Cells["dgvl_reference"].Value = dt.Rows[i]["reference"].ToString();
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
            String ORDERBY = "ORDER BY pret_num DESC";

            String search_text = textBox15.getText();
            int selected_index = comboBox1.getSelectedIndex();


            if (selected_index == -1 && !String.IsNullOrEmpty(search_text))
            {
                WHERE = " AND (supl_name LIKE '%" + search_text + "%' OR pret_num LIKE '%" + search_text + "%') ";
                if (gm.toNormalDoubleFormat(search_text) != 0)
                {
                    ORDERBY = "ORDER BY supl_name, pret_num  DESC";
                }
                else
                {
                    ORDERBY = "ORDER BY pret_num DESC";
                }
            }
            else if (selected_index == 0)
            {
                WHERE = " AND pret_num LIKE '%" + search_text + "%'";
            }
            else if (selected_index == 1)
            {
                WHERE = " AND (supl_name LIKE '%" + search_text + "%' OR supl_code LIKE '%" + search_text + "%')";
                ORDERBY = "ORDER BY supl_name, pret_num DESC";
            }
            else if (selected_index == 2)
            {
                WHERE = " AND reference LIKE '%" + search_text + "%'";
                ORDERBY = "ORDER BY reference, pret_num  DESC";
            }

            dt = db.QueryBySQLCode("SELECT p.pret_num, p.supl_code, p.supl_name, p.reference, TO_CHAR(p.trnx_date, 'MM/DD/YYYY') AS trnx_date, p.whs_code, p.jrnlz, p.cancel, p.user_id, TO_CHAR(p.t_date, 'MM/DD/YYYY') AS t_date, p.t_time FROM " + db.schema + ".prethdr p LEFT JOIN " + db.schema + ".m07 s ON s.c_code=p.supl_code LEFT JOIN " + db.schema + ".whouse w ON w.whs_code=p.whs_code WHERE p.t_date between " + "'" + dtp_frm.getStringDate() + "' AND '" + dtp_to.getStringDate() + "'  " + WHERE + " " + ORDERBY);

            return dt;
        }
    }
}
