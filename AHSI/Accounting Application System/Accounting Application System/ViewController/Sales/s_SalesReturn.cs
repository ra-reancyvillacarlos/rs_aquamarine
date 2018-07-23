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
    public partial class s_SalesReturn : Form
    {
        private z_enter_ret_sale_item _frm_rsi;
        DateTime dt_to;
        DateTime dt_frm;
        dbSales db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        int lnno_last = 1;
        String stk_trns_type = "SR";
        String trns_type = "SR";

        public s_SalesReturn()
        {
            InitializeComponent();
            dgv_delitem.Hide();

            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbSales();

            gc.load_customer(cbo_customer);
            gc.load_stocklocation(cbo_stocklocation);

            thisDatabase db2 = new thisDatabase();
            String grp_id2 = "";
            DataTable dt24 = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt24.Rows.Count > 0)
            {
                grp_id2 = dt24.Rows[0]["grp_id"].ToString();
            }
            DataTable dt23 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id2 + "' AND a.mod_id='S2000' ORDER BY b.pla, b.mod_id");

            if (dt23.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt23.Rows[0]["add"].ToString();
                update = dt23.Rows[0]["upd"].ToString();
                delete = dt23.Rows[0]["cancel"].ToString();
                print = dt23.Rows[0]["print"].ToString();

                if (add == "n")
                {
                    btn_new.Enabled = false;
                }
                if (update == "n")
                {
                    btn_upd.Enabled = false;
                }
                if (delete == "n")
                {
                    btn_cancel.Enabled = false;
                }
                if (print == "n")
                {
                    btn_print.Enabled = false;
                }

            }
        }

        private void s_SalesReturn_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            DateTime temp_dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtp_frm.Value = temp_dt;

            dt_frm = dtp_frm.Value;
            dt_to = dtp_to.Value;
            dgv_delitem.Hide();

            dtp_frm.ValueChanged += dtp_frm_ValueChanged;
            dtp_to.ValueChanged += dtp_to_ValueChanged;

            disp_list();
        }

        void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
        }

        void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
        }

        private void frm_clear()
        {
            try
            {
                txt_code.Text = "";
                cbo_customer.SelectedIndex = -1;
                cbo_stocklocation.SelectedIndex = -1;
                dtp_trnxdt.Value = Convert.ToDateTime(db.get_systemdate(""));
                lbl_total.Text = "0.00";
                chk_refund.Checked = false;
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
            String code = "", customer = "", whscode = "";
            isnew = false;

            if (dgv_list.Rows.Count > 1)
            {
                try
                {
                    r = dgv_list.CurrentRow.Index;
                    code = dgv_list["dgvl_code", r].Value.ToString();
                    txt_code.Text = code;

                    if (dgv_list["dgvl_jrnlz", r].Value.ToString() == "Y")
                    {
                        MessageBox.Show("SRET# " + code + " is already journalize. Can not be updated.");
                        return;
                    }
                    if (dgv_list["dgvl_cancel", r].Value.ToString() == "Y")
                    {
                        MessageBox.Show("SRET# " + code + " is already cancelled. Can not be updated.");
                        return;
                    }
                    
                    if (String.IsNullOrEmpty(dgv_list["dgvl_whs_code", r].Value.ToString()) == false)
                    {
                        whscode = dgv_list["dgvl_whs_code", r].Value.ToString();
                        cbo_stocklocation.SelectedValue = whscode;
                    }

                    if (String.IsNullOrEmpty(dgv_list["dgvl_debt_code", r].Value.ToString()) == false)
                    {
                        customer = dgv_list["dgvl_debt_name", r].Value.ToString();
                        cbo_customer.Text = customer;
                    }

                    if (String.IsNullOrEmpty(dgv_list["dgvl_ret_date", r].Value.ToString()) == false)
                    {
                        dtp_trnxdt.Value = Convert.ToDateTime(dgv_list["dgvl_ret_date", r].Value.ToString());
                    }

                    if (dgv_list["dgvl_refund", r].Value.ToString() == "Y")
                    {
                        chk_refund.Checked = true;

                        lbl_csh_dttime.Text = "on " + dgv_list["dgvl_csh_date", r].Value.ToString() + " " + dgv_list["dgvl_csh_time", r].Value.ToString();
                    }

                }
                catch (Exception er) { MessageBox.Show(er.Message); }

                disp_itemlist(code);
                disp_total();
                goto_win2();
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            DataTable dt;
            String code = "", itemcode = "", itemqty = "0.00";
            int r = -1;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                code = dgv_list["dgvl_code", r].Value.ToString();

                if (dgv_list["dgvl_jrnlz", r].Value.ToString() == "Y")
                {
                    MessageBox.Show("SRET# " + code + " is already journalize. Can not be updated.");
                    return;
                }
                if (dgv_list["dgvl_cancel", r].Value.ToString() == "Y")
                {
                    MessageBox.Show("SRET# " + code + " is already cancelled. Can not be updated.");
                    return;
                }


                dialogResult = MessageBox.Show("Are you sure you want to cancel this SRET# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    if (db.set_cancel("rethdr", "ret_num='" + code + "'"))
                    {
                        try
                        {
                            dt = db.get_po_item_list(code);

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                itemcode = dt.Rows[i]["item_code"].ToString();
                                itemqty = dt.Rows[i]["inv_qty"].ToString();

                                db.upd_item_quantity_onhand(itemcode, gm.toNormalDoubleFormat(itemqty) * -1, stk_trns_type);
                            }
                        }
                        catch (Exception) { }

                        db.DeleteOnTable("retlne", "ret_num='" + code + "'");
                        db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

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

            if (dgv_list.Rows.Count > 0)
            {
                int r = dgv_list.CurrentRow.Index;
                String trans_no = (dgv_list["dgvl_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(trans_no))
                {
                    String cust_name = dgv_list["dgvl_debt_name", r].Value.ToString()
                          , stck_loc = dgv_list["dgvl_whs_desc", r].Value.ToString()
                          , t_date = dgv_list["dgvl_systemdate", r].Value.ToString()
                          , t_time = dgv_list["dgvl_systemtime", r].Value.ToString();

                    Report rpt = new Report();
                    rpt.print_sales_return(trans_no, cust_name, stck_loc, gm.toDateString(t_date, ""), t_time);
                    rpt.ShowDialog();
                } //dgvl_debt_name dgvl_whs_desc dgvl_code dgvl_systemdate dgvl_systemtime
            }
            else
            {
                MessageBox.Show("No Trans item selected.");
            }
        }

        private void disp_list()
        {
            DataTable dt = db.get_sales_return(dt_frm, dt_to);
            
            try
            {
                dgv_list.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_list.Rows.Add();
                    
                    dgv_list["dgvl_code", i].Value = dt.Rows[i]["ret_num"].ToString();
                    dgv_list["dgvl_debt_name", i].Value = dt.Rows[i]["debt_name"].ToString();
                    dgv_list["dgvl_ret_date", i].Value = gm.toDateString(dt.Rows[i]["ret_date"].ToString(), "");
                    dgv_list["dgvl_whs_code", i].Value = dt.Rows[i]["whs_code"].ToString();
                    cbo_stocklocation.SelectedValue = dt.Rows[i]["whs_code"].ToString();
                    dgv_list["dgvl_whs_desc", i].Value = cbo_stocklocation.Text;
                    dgv_list["dgvl_refund", i].Value = dt.Rows[i]["refund"].ToString();
                    dgv_list["dgvl_jrnlz", i].Value = dt.Rows[i]["jrnlz"].ToString();
                    dgv_list["dgvl_cancel", i].Value = dt.Rows[i]["cancel"].ToString();
                    dgv_list["dgvl_userid", i].Value = dt.Rows[i]["user_id"].ToString();
                    dgv_list["dgvl_systemdate", i].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                    dgv_list["dgvl_systemtime", i].Value = dt.Rows[i]["t_time"].ToString();

                    dgv_list["dgvl_csh_id", i].Value = dt.Rows[i]["csh_id"].ToString();
                    dgv_list["dgvl_csh_date", i].Value = gm.toDateString(dt.Rows[i]["csh_date"].ToString(), "");
                    dgv_list["dgvl_csh_time", i].Value = dt.Rows[i]["csh_time"].ToString();
                    dgv_list["dgvl_debt_code", i].Value = dt.Rows[i]["debt_code"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void disp_itemlist(String inv_num)
        {
            DataTable dt;

            try
            {
                dt = db.get_soret_item_list(inv_num);
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["dgvi_ln", i].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvi_docno", i].Value = dt.Rows[i]["doc_code"].ToString();
                    dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();
                    dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();
                    dgv_itemlist["dgvi_unitdesc", i].Value = dt.Rows[i]["unit_shortcode"].ToString();
                    dgv_itemlist["dgvi_qty", i].Value = dt.Rows[i]["ret_qty"].ToString();
                    dgv_itemlist["dgvi_price", i].Value = dt.Rows[i]["price"].ToString();
                    dgv_itemlist["dgvi_lnamt", i].Value = dt.Rows[i]["ln_amnt"].ToString();
                    dgv_itemlist["dgvi_repcode", i].Value = dt.Rows[i]["rep_code"].ToString();
                    dgv_itemlist["dgvi_inv_num", i].Value = dt.Rows[i]["inv_num"].ToString();
                    dgv_itemlist["dgvi_inv_lne", i].Value = dt.Rows[i]["inv_lne"].ToString();
                    dgv_itemlist["dgvi_prc_user", i].Value = dt.Rows[i]["prc_user"].ToString();
                    dgv_itemlist["dgvi_prc_reason", i].Value = dt.Rows[i]["prc_reason"].ToString();
                    dgv_itemlist["dgvi_unitid", i].Value = dt.Rows[i]["unit"].ToString();
                    dgv_itemlist["dgvi_fcp", i].Value = dt.Rows[i]["fcp"].ToString();
                    dgv_itemlist["dgvi_oldqty", i].Value = dt.Rows[i]["ret_qty"].ToString();
                }
            }
            catch (Exception) { }
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
            _frm_rsi = new z_enter_ret_sale_item(this, isnew_item, lnno_last);
            _frm_rsi.ShowDialog();
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            int r = 0;
            int cur_index = 0;

            isnew_item = false;

            if (dgv_itemlist.Rows.Count > 1)
            {
                r = dgv_itemlist.CurrentRow.Index;

                cur_index = int.Parse(dgv_itemlist[0, r].Value.ToString());

                _frm_rsi = new z_enter_ret_sale_item(this, isnew_item, r);
                _frm_rsi.ShowDialog();
            }
            else
            {
                MessageBox.Show("No item selected.");
            }
        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            String qty = "";

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

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String notificationText = "", notifyadd = null;

            String code, debt_code, debt_name, ret_date, whs_code, user_id, t_date, t_time, refund = "", csh_id, csh_date, csh_time, stk_po_so = "", reference = "";
            String col = "", val = "";
            String table = "rethdr";
            String tableln = "retlne";

            if (cbo_customer.SelectedIndex == -1)
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
                debt_code = cbo_customer.SelectedValue.ToString();
                debt_name = cbo_customer.Text;
                ret_date = dtp_trnxdt.Value.ToString("yyyy-MM-dd");
                whs_code = cbo_stocklocation.SelectedValue.ToString();
                user_id = GlobalClass.username;
                t_date = db.get_systemdate("yyyy-MM-MM");
                t_time = db.get_systemtime();
                refund = "";
                csh_id = "";
                csh_date = "";
                csh_time = "";
                reference ="Sales Return/Refund:"+code;

                if (isnew)
                {
                    code = db.get_pk("ret_num");

                    if (chk_refund.Checked)
                    {
                        refund = "Y";
                        csh_id = "101"; //m10 cash
                        csh_date = t_date;
                        csh_time = t_time;
                    }

                    col = "ret_num, debt_code, debt_name, ret_date, whs_code, user_id, t_date, t_time, refund, csh_id, csh_date, csh_time, cancel";
                    val = "'" + code + "', '" + debt_code + "', '" + debt_name + "', '" + ret_date + "', '" + whs_code + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + refund + "', '" + csh_id + "', '" + csh_date + "', '" + csh_time + "', 'N'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        notifyadd = add_items(tableln, code, ret_date, whs_code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "SALES RETURN");
                            db.set_pkm99("ret_num", db.get_nextincrementlimitchar(code, 8));
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                    }

                    if(success == false)
                    {
                        db.DeleteOnTable(table, "ret_num='" + code + "'");
                        db.DeleteOnTable(tableln, "ret_num='" + code + "'");
                        db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    if (chk_refund.Checked && String.IsNullOrEmpty(lbl_csh_dttime.Text))
                    {
                        refund = "Y";
                        csh_id = "101"; //m10 cash
                        csh_date = t_date;
                        csh_time = t_time;
                    }
                    
                    col = "ret_num='" + code + "', debt_code='" + debt_code + "', debt_name=" + db.str_E(debt_name) + ", ret_date='" + ret_date + "', whs_code='" + whs_code + "', user_id='" + user_id + "', t_date='" + t_date + "', t_time='" + t_time + "', refund='" + refund + "', csh_id='" + csh_id + "', csh_date='" + csh_date + "', csh_time='" + csh_time + "'";

                    if (db.UpdateOnTable(table, col, "ret_num='" + code + "'"))
                    {
                        db.DeleteOnTable(tableln, "ret_num='" + code + "'");
                        db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                        notifyadd = notifyadd = add_items(tableln, code, ret_date, whs_code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "SALES RETURN");
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                    }

                    if(success == false)
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

        private String add_items(String tableln, String code, String dt_trnx, String loc)
        {
            String notificationText = null;
            String i_lnno = "", i_itemcode = "", i_itemdesc = "", i_qty = "", i_lnamnt = "", i_unitid = "", i_price = "", i_doc = "", i_fcp = "", i_prc_user = "", i_prc_reason = "", i_rep_code = "", i_inv_num = "", i_inv_lne = ""; 
            String val2 = "";
            String col2 = "ret_num, ln_num, item_code, item_desc, unit, ret_qty, price, ln_amnt, prc_user, prc_reason, doc_code, rep_code, inv_num, inv_lne, fcp";
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";

            for (int r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
            {
                i_lnno = dgv_itemlist["dgvi_ln", r].Value.ToString();
                i_itemcode = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                i_itemdesc = dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
                i_unitid = dgv_itemlist["dgvi_unitid", r].Value.ToString();
                i_qty = dgv_itemlist["dgvi_qty", r].Value.ToString();                
                i_price = dgv_itemlist["dgvi_price", r].Value.ToString();
                i_lnamnt = dgv_itemlist["dgvi_lnamt", r].Value.ToString();
                i_prc_user = dgv_itemlist["dgvi_prc_user", r].Value.ToString();
                i_prc_reason = dgv_itemlist["dgvi_prc_reason", r].Value.ToString();
                i_doc = dgv_itemlist["dgvi_docno", r].Value.ToString();         //reference from so       
                i_rep_code = dgv_itemlist["dgvi_repcode", r].Value.ToString();
                i_inv_num = dgv_itemlist["dgvi_inv_num", r].Value.ToString();
                i_inv_lne = dgv_itemlist["dgvi_inv_lne", r].Value.ToString();
                i_fcp = db.get_item_fcp(i_itemcode).ToString("0.00");

                val2 = "'" + code + "', '" + i_lnno + "', '" + i_itemcode + "', " + db.str_E(i_itemdesc) + ", '" + i_unitid + "', '" + i_qty + "', '" + i_lnamnt + "', '" + i_prc_user + "', " + db.str_E(i_prc_reason) + ", '" + i_doc + "', '" + i_rep_code + "', '" + i_inv_num + "', '" + i_inv_lne + "', '" + i_fcp +"'";

                if (db.InsertOnTable(tableln, col2, val2))
                {
                    stk_qty_in = "0.00";
                    stk_qty_out = "0.00";

                    if (gm.toNormalDoubleFormat(i_qty) < 0)
                        stk_qty_out = Math.Abs(gm.toNormalDoubleFormat(i_qty)).ToString();
                    else
                        stk_qty_in = i_qty;

                    db.save_to_stkcard(i_itemcode, i_itemdesc, i_unitid, dt_trnx, i_prc_reason, i_doc, stk_qty_in, stk_qty_out, i_fcp, i_price, loc, "", "", stk_trns_type, "", "", "");

                    db.upd_item_quantity_onhand(i_itemcode, Convert.ToDouble(i_qty), stk_trns_type);
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
                    inc_lnno();
                }
                else
                {
                    i = dgv_itemlist.CurrentRow.Index;
                }

                dgv_itemlist["dgvi_ln", i].Value = dt.Rows[0]["dgvi_ln"].ToString();
                dgv_itemlist["dgvi_docno", i].Value = dt.Rows[0]["dgvi_docno"].ToString();
                dgv_itemlist["dgvi_itemcode", i].Value = dt.Rows[0]["dgvi_itemcode"].ToString();
                dgv_itemlist["dgvi_itemdesc", i].Value = dt.Rows[0]["dgvi_itemdesc"].ToString();
                dgv_itemlist["dgvi_unitdesc", i].Value = dt.Rows[0]["dgvi_unitdesc"].ToString();
                dgv_itemlist["dgvi_unitid", i].Value = dt.Rows[0]["dgvi_unitid"].ToString();
                dgv_itemlist["dgvi_qty", i].Value = dt.Rows[0]["dgvi_qty"].ToString();
                dgv_itemlist["dgvi_price", i].Value = dt.Rows[0]["dgvi_price"].ToString();
                dgv_itemlist["dgvi_lnamt", i].Value = dt.Rows[0]["dgvi_lnamt"].ToString();

                dgv_itemlist["dgvi_inv_num", i].Value = dt.Rows[0]["dgvi_inv_num"].ToString();
                dgv_itemlist["dgvi_inv_lne", i].Value = dt.Rows[0]["dgvi_inv_lne"].ToString();
                dgv_itemlist["dgvi_prc_user", i].Value = dt.Rows[0]["dgvi_prc_user"].ToString();
                dgv_itemlist["dgvi_prc_reason", i].Value = dt.Rows[0]["dgvi_prc_reason"].ToString();
                dgv_itemlist["dgvi_repcode", i].Value = dt.Rows[0]["dgvi_repcode"].ToString();
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

        private void btn_itemadd_Click_1(object sender, EventArgs e)
        {
            int lastrow = 0;
            isnew_item = true;

            try
            {
                if (isnew == false)
                {
                    lastrow = dgv_itemlist.Rows.Count - 2;
                    lnno_last = int.Parse(dgv_itemlist["dgvi_ln", lastrow].Value.ToString());
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

            _frm_rsi = new z_enter_ret_sale_item(this, isnew_item, lnno_last);
            _frm_rsi.ShowDialog();
        }

        private void btn_mainexit_Click_1(object sender, EventArgs e)
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

        private void btn_mainsave_Click_1(object sender, EventArgs e)
        {
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String code, debt_code, debt_name, ret_date, whs_code, user_id, t_date, t_time, refund, csh_id, csh_date, csh_time, jrnlz, cancel, jrnlz2, branch, transferred;

            String col = "", val = "";
            String notificationText = "", notifyadd = "";
            String table = "rethdr";
            String tableln = "retlne";
            
            int r;

            if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else if (cbo_stocklocation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a stock location.");
            }
            else if (dgv_itemlist.Rows.Count <= 1)
            {
                MessageBox.Show("There no items to purchase. Please entry items.");
            }
            else
            {
                code = txt_code.Text;
                debt_code = cbo_customer.SelectedValue.ToString();
                debt_name = cbo_customer.Text.ToString();
                whs_code = cbo_stocklocation.SelectedValue.ToString();
                t_date = dtp_trnxdt.Value.ToString("yyyy-MM-dd");
                ret_date = dtp_trnxdt.Value.ToString("yyyy-MM-dd");
                user_id = GlobalClass.username;
                t_time =  db.get_systemtime();
                if (chk_refund.Checked)
                    refund = "Y";
                else
                    refund = "N";

                if (isnew)
                {
                    notificationText = "has added: ";
                    code = db.get_pk("ret_num");
                    col = "ret_num, debt_code, debt_name, ret_date, whs_code, user_id, t_date, t_time, refund,cancel,transferred";

                    val = "'" + code + "',"
                        + "'" + debt_code + "',"
                        + "'" + debt_name + "',"
                        + "'" + ret_date + "',"
                        + "'" + whs_code + "',"
                        + db.str_E(user_id) + ","
                        + "'" + t_date + "',"
                        + "'" + t_time + "',"
                        + "'" + refund + "',"
                        + "'N',"
                        + "'N'";
                    
 
                    //val = "'" + code + "', " + db.str_E(supp_id) + ", " + db.str_E(supp_name) + ", " + db.str_E(reference) + ", '" + rels_date + "', '" + delv_date + "', '" + terms + "', '" + db.get_systemdate("") + "', '" + db.get_systemtime() + "', '" + request_by + "', " + db.str_E(notes) + ",'N'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        notifyadd = add_items(tableln, code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "SALES RETURN");
                            db.set_pkm99("ret_num", db.get_nextincrementlimitchar(code, 8));
                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "ret_num='" + code + "'");
                            db.DeleteOnTable(tableln, "ret_num='" + code + "'");
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    else
                    {
                        db.DeleteOnTable(table, "ret_num='" + code + "'");
                        db.DeleteOnTable(tableln, "ret_num='" + code + "'");
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    notificationText = "has updated: ";
                    col = "ret_num=" + "'" + code + "',"
                       + "debt_code=" + "'" + debt_code + "',"
                       + "debt_name=" + "'" + debt_name + "',"
                       + "ret_date='" + ret_date + "',"
                       + "whs_code=" + "'" + whs_code + "',"
                       + "user_id=" + db.str_E(user_id) + ","
                       + "t_date=" + "'" + t_date + "',"
                       + "t_time=" + "'" + t_time + "',"
                       + "refund=" + "'" + refund + "',"
                       + "cancel='N'" +","
                       + "transferred='N'";

                    if (db.UpdateOnTable(table, col, "ret_num='" + code + "'"))
                    {
                        db.DeleteOnTable(tableln, "ret_num='" + code + "'");

                        notifyadd = add_items(tableln, code);

                        if (String.IsNullOrEmpty(notifyadd) == false)
                        {
                            notificationText += notifyadd;
                            notificationText += Environment.NewLine + " with #" + code;
                            notify.saveNotification(notificationText, "SALES RETURN");
                            success = true;
                        }
                        else
                        {
                            db.DeleteOnTable(table, "ret_num='" + code + "'");
                            db.DeleteOnTable(tableln, "ret_num='" + code + "'");
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
        private String add_items(String tableln, String code)
        {
            String notificationText = null;
            String i_ln_num = "", i_inv_num = "", i_inv_lne = "", i_item_code = "", i_item_desc = "", i_unit = "", i_ret_qty = "", i_price = "", i_ln_amnt = "", i_prc_user = "", i_prc_reason = "", i_doc_code = "", i_rep_code = "", i_fcp = "";
            String val2 = "";
            String col2 = "";
            Double i_total = 0.00;
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";
           
                
            for (int r = 0; r < dgv_itemlist.Rows.Count - 1; r++)
            {
                i_ln_num = dgv_itemlist["dgvi_ln", r].Value.ToString();
                i_item_code = dgv_itemlist["dgvi_itemcode", r].Value.ToString();
                i_item_desc = dgv_itemlist["dgvi_itemdesc", r].Value.ToString();
               // i_qty = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_qty", r].Value.ToString()).ToString();
                i_unit = dgv_itemlist["dgvi_unitid", r].Value.ToString();
                i_ret_qty = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_qty", r].Value.ToString()).ToString("0.00");
                i_price = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_price", r].Value.ToString()).ToString("0.00");
                i_total = ( Convert.ToDouble(i_ret_qty) * Convert.ToDouble(i_price) );
                i_ln_amnt = gm.toNormalDoubleFormat(i_total.ToString()).ToString("0.00");
                i_prc_user = GlobalClass.username;//dgv_itemlist["dgvi_scc_code", r].Value.ToString();

                i_prc_reason = "N/A";//dgv_itemlist["dgvi_vatcode", r].Value.ToString();
                i_doc_code = "N/A";//dgv_itemlist["dgvi_costunitid", r].Value.ToString();
                i_rep_code = "N/A"; //dgv_itemlist["dgvi_newregsellprice", r].Value.ToString();
                //fcp = gm.toNormalDoubleFormat(dgv_itemlist["dgvi_fcp", r].Value.ToString()).ToString("0.00");
                i_fcp = gm.toNormalDoubleFormat("0.00").ToString("0.00");
                i_inv_lne = gm.toNormalDoubleFormat("0.00").ToString("0.00");
                col2 = "ret_num,ln_num, inv_num , inv_lne, item_code, item_desc,unit,ret_qty, price, ln_amnt, prc_user, prc_reason, doc_code, rep_code, fcp";

                val2 = "'" + code + "',"
                       + "'" + i_ln_num + "',"
                       + "'" + i_inv_num + "',"
                       + "'" + i_inv_lne + "',"
                       + "'" + i_item_code + "',"
                       + "'" + i_item_desc + "',"
                       + "'" + i_unit + "',"
                       + "'" + i_ret_qty + "',"
                       + "'" + i_price + "',"
                       + "'" + i_ln_amnt + "',"
                       + "'" + i_prc_user + "',"
                       + "'" + i_prc_reason + "',"
                       + "'" + i_doc_code + "',"
                       + "'" + i_rep_code + "',"
                       + "'" + i_fcp + "'";
                if (db.InsertOnTable(tableln, col2, val2))
                {
                    stk_qty_in = "0.00";
                    stk_qty_out = "0.00";

                    if (gm.toNormalDoubleFormat(i_ret_qty) < 0)
                        stk_qty_out = Math.Abs(gm.toNormalDoubleFormat(i_ret_qty)).ToString();
                    else
                        stk_qty_in = i_ret_qty;

                    //db.save_to_stkcard(i_item_code, i_item_desc, i_unit, dt_inv, stk_ref, stk_po_so, stk_qty_in, stk_qty_out, i_fcp, i_costprice, loc, supp_id, supp_name, stk_trns_type, "", i_cccode, i_scc_code);

                    notificationText += Environment.NewLine + i_item_desc;
                }
                else
                {
                    notificationText = null;
                }
            }

            return notificationText;
        }

        private void btn_itemremove_Click_1(object sender, EventArgs e)
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
            catch (Exception er) { MessageBox.Show("No item selected"); }
        }

        private void btn_itemupd_Click_1(object sender, EventArgs e)
        {

            int r = 0;
            int cur_index = 0;
            isnew_item = false;
            string item_unit = "";

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    cur_index = int.Parse(dgv_itemlist[0, r].Value.ToString());
                    _frm_rsi = new z_enter_ret_sale_item(this, isnew_item, cur_index -1);
                    _frm_rsi.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
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
