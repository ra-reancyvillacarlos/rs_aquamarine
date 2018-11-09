using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Accounting_Application_System
{
    public partial class a_disbursement : Form
    {
        String db_code = "";
        String atcode = "";
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        z_add_disburse_item _frm_di;
        DateTime dt_to;
        DateTime dt_frm;
        Boolean seltbp = false;
        private Boolean isnew = false, ischeck = false;
        Boolean isnew_item = true;
        private int lnno_last = 1;
        String code = "", ln_no = "", at_code = "", at_desc = "", amount="", notes = "";
        String invoicee = "";
        Boolean is_new = false;

        String j_code = "", jrnl_name = "";
        String jtype_name = "Disbursement";

        public a_disbursement()
        {
            InitializeComponent();
            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            //
            gc.load_account_title_payment(cbo_modeofpayment);
            gc.load_modeofpayment(cbo_terms);
            gc.load_salesclerk(cbo_cashier);
            gc.load_payee(cbo_payee);

            String j_type = db.get_colval("m05type", "code", "lower(name)='" + jtype_name.ToLower() + "'");
            gc.load_journal(cbo_dtype, j_type);
            gc.load_journal(cbo_codetype, j_type);
            gc.load_branch(cbo_branch);

            dtp_frm.Value = DateTime.Parse(dtp_frm.Value.ToString("yyyy-MM-01"));

            if(cbo_codetype.SelectedIndex != -1)
            {
                disp_list();
            }
        }

        private void a_disbursement_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            if (cbo_codetype.SelectedIndex != -1)
            {
                disp_list();
            }
        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_desc.Text = "";
            cbo_modeofpayment.SelectedIndex = -1;
            cbo_terms.SelectedIndex = -1;
            txt_ckno.Text = "";
            rtxt_tech_remarks.Text = "";
            txt_pay_amnt.Text = gm.toAccountingFormat("0.00");
            txt_payment.Text = gm.toAccountingFormat("0.00");
            txt_totalamount.Text = gm.toAccountingFormat("0.00");
            txt_amtdue.Text = gm.toAccountingFormat("0.00");
            cbo_payee.Text = "";
            cbo_payee.SelectedIndex = -1;
            cbo_codetype.SelectedIndex = -1;

            dgv_itemlist.Rows.Clear();

            cbo_branch.SelectedValue = GlobalClass.branch;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            if (cbo_dtype.SelectedIndex != -1)
            {
                isnew = true;
                frm_clear();
                //m_customers frm = new m_customers(this, true);
                //frm.ShowDialog();
                lnno_last = 1;
                txt_code.Text = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                cbo_codetype.SelectedValue = cbo_dtype.SelectedValue.ToString();

                goto_win2();
            }
            else
            {
                MessageBox.Show("Select Journal Type first.");
                cbo_dtype.DroppedDown = true;
            }
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
           
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

                code = dgv_list["dgvl_inv_num", r].Value.ToString();

                dialogResult = MessageBox.Show("Are you sure you want to cancel this P.I.# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    if (db.UpdateOnTable("pinvhd", "supl_name='CANCELLED' || '-' ||supl_name, cancel='Y'", "inv_num='" + code + "'"))
                    {
                        try
                        {

                        }
                        catch (Exception) { }

                    }

                    disp_list();
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            /*if (dgv_list.Rows.Count > 0)
            {
                int r = dgv_list.CurrentRow.Index;
                String dis_no = (dgv_list["dgvl_or_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(dis_no))
                {
                    String debt_name = dgv_list["dgvl_debt_code", r].Value.ToString() // this is name not code
                        , chk_no = dgv_list["dgvl_check_no", r].Value.ToString()
                        , chk_date = gm.toDateString(dgv_list["dgvl_check_date", r].Value.ToString(), "")
                        , t_date = gm.toDateString(dgv_list["dgvl_trans_date", r].Value.ToString(), "")
                        , notes = dgv_list["dgvl_explaination", r].Value.ToString()
                        , acc_link = "", pay_thru = "";

                    //cbo_acct_link.SelectedValue = dgv_list["dgvl_c_code", r].Value.ToString();
                    //acc_link = cbo_acct_link.Text;
                    cbo_modeofpayment.SelectedValue = dgv_list["dgvl_paycode", r].Value.ToString();
                    pay_thru = cbo_modeofpayment.Text;

                    Report rpt = new Report();
                    rpt.print_disbursement("", dis_no, debt_name, acc_link, t_date, notes, pay_thru, chk_no, chk_date);

                    rpt.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No Disbursement item selected.");
            }*/
            try
            {
                int rowno = dgv_list.CurrentRow.Index;
                String dis_no = dgv_list["dgvl_or_code", rowno].Value.ToString();
                if (!String.IsNullOrEmpty(dis_no))
                {
                    Report rpt = new Report();
                    DataTable dt;
                    String refno = "", ref_desc = "", yr = "", mo = "", t_date = "", paid_to = "", ck_no = "", ck_date = "", explanation, j_code = "", jrnl_name = "" ;

                    DateTime dt_tmp = DateTime.Parse(dgv_list["dgvl_trans_date", rowno].Value.ToString());
                    mo = dt_tmp.ToString("MM");
                    yr = dt_tmp.ToString("yyyy");


                    refno = dis_no;
                    t_date = dgv_list["dgvl_trans_date", rowno].Value.ToString();
                    ref_desc = dgv_list["dgvl_desc", rowno].Value.ToString();
                    paid_to = dgv_list["dgvl_payee", rowno].Value.ToString();
                    ck_no = dgv_list["dgvl_check_no", rowno].Value.ToString();
                    ck_date = dgv_list["dgvl_check_date", rowno].Value.ToString();
                    explanation = dgv_list["dgvl_explaination", rowno].Value.ToString();

                    if (cbo_dtype.SelectedIndex != -1)
                    {
                        j_code = this.j_code;
                        jrnl_name = this.jrnl_name;
                    }
                    else
                    {
                        j_code = dgv_list["dgvl_dtype", rowno].Value.ToString();
                        jrnl_name = db.get_colval("m05", "j_desc", "j_code='" + j_code + "'");
                    }

                    dt = db.get_journalentry_info_accntlist(j_code, refno);

                    rpt.print_voucher(j_code, jrnl_name, refno, ref_desc, yr, mo, t_date, paid_to, ck_date, ck_no, explanation, dt);
                    rpt.Show();
                }
                else 
                {
                    MessageBox.Show("No Disbursement item selected.");
                }


                if (dgv_list.Rows.Count <= 1)
                {
                    MessageBox.Show("No journal entry selected");
                }
                else
                {
                    
                }
            }
            catch (Exception) { MessageBox.Show("No journal entry selected."); }

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

            //_frm_di = new z_add_disburse_item(this, true, "", lnno_last, (cbo_creditors.SelectedValue??"").ToString());
            //_frm_di.ShowDialog();
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            int r = 0;
            int cur_index = 0;

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    cur_index = int.Parse(dgv_itemlist[0, r].Value.ToString());

                    //_frm_di = new z_add_disburse_item(this, false, "", lnno_last, "");
                    //_frm_di.ShowDialog();
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
            //z_Notification notify = new z_Notification();
            //Boolean success = false;
            //String notifyadd = "";
            //String code, supp_id, supp_name, terms, loc, dt_inv, reference, stk_ref = "", stk_po_so = "";
            //String i_oldqty = "";
            //String di_code = "", di_qty = "0.00";
            //String col = "", val = "";
            //String creditors = "", explaination = "", check_no = "", mp_code = "", user_id="", c_code="" ,remark="";
            //String notificationText = "";
            //String table = "disbursementhdr";
            //String tableln = "disbursementhdrlne";

            //if (cbo_creditors.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please select Creditor.");
            //}
            //else if (cbo_modeofpayment.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please select Mode Of Payment.");
            //}
            //else
            //{
            //    code = txt_code.Text;

            //    notificationText = "has added: ";

            //    if (isnew)
            //    {
            //        code = "";//db.get_pk("pi_num");
            //        col = "db_code, creditors, explaination, check_no, mp_code, user_id, c_code, reference";
            //        val = "'" + code + "','" + creditors + "','" + explaination + "','" + check_no + "','" + mp_code + "','" + user_id + "','" + c_code + "','" + remark + "'";
                    
            //        if (db.InsertOnTable(table, col, val))
            //        {
            //            notifyadd = add_items(tableln, code);

            //            if (String.IsNullOrEmpty(notifyadd) == false)
            //            {
            //                notificationText += notifyadd;
            //                notificationText += Environment.NewLine + " with #" + code;
            //                notify.saveNotification(notificationText, "Disbursement Entry");
            //                db.set_pkm99("db_code", db.get_nextincrementlimitchar(code, 8));
            //                success = true;
            //            }
            //            else
            //            {
            //                db.DeleteOnTable(table, "db_code='" + code + "'");
            //                db.DeleteOnTable(tableln, "db_code='" + code + "'");
            //                success = false;
            //            }
            //        }
            //        else
            //        {
            //            success = false;
            //            MessageBox.Show("Failed on saving.");
            //        }
            //    }
            //    else
            //    {
            //        col = "db_code=''";

            //        if (db.UpdateOnTable(table, col, "db_code='" + code + "'"))
            //        {
            //            db.DeleteOnTable(tableln, "db_code='" + code + "'");

            //            notifyadd = add_items(tableln, code);

            //            if (String.IsNullOrEmpty(notifyadd) == false)
            //            {
            //                success = true;
            //            }
            //            else
            //            {
            //                success = false;
            //                MessageBox.Show("Failed on saving.");
            //            }
            //        }
            //        else
            //        {
            //            success = false;
            //            MessageBox.Show("Failed on saving.");
            //        }
            //    }
            //}

            //if (success)
            //{
            //    disp_list();
            //    goto_win1();
            //    frm_clear();
            //}
        }

        private String add_items(String tableln, String code)
        {
            String notificationText = null;
            String i_lnno = "", i_atcode = "", i_atdesc = "", i_amt = "", i_invoice = "";
            String col2 = "db_code, ln_num, at_code, at_desc, amount,invoice";
            String val2 = "";

            try
            {
                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    i_lnno = dgv_itemlist["seqno", r].Value.ToString();
                    i_atcode = dgv_itemlist["dgv_atcode", r].Value.ToString();
                    i_atdesc = dgv_itemlist["accttitle", r].Value.ToString();
                    i_amt = gm.toNormalDoubleFormat((dgv_itemlist["credit", r].Value ?? "0.00").ToString()).ToString("0.00");
                    i_invoice = dgv_itemlist["invoice", r].Value.ToString();
                    //price = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_regprice", r].Value.ToString()).ToString("0.00");
                    //ln_amnt = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", r].Value.ToString()).ToString("0.00");


                    //if (cbo_clerk.SelectedIndex != -1)
                    //{
                    //    rep_code = cbo_clerk.SelectedValue.ToString();
                    //}
                    //else
                    //{
                    //    rep_code = "";
                    //}

                    val2 = "'" + code + "','" + i_lnno + "','" + i_atcode + "'," + db.str_E(i_atdesc) + ",'" + i_amt + "','" + i_invoice + "'";

                    if (db.InsertOnTable(tableln, col2, val2))
                    {

                        //db.upd_item_quantity_onhand(item_code, Convert.ToDouble(ord_qty), stk_trns_type);
                        notificationText += Environment.NewLine + code + " - " + i_atcode;
                    }
                    else
                    {
                        notificationText = null;
                    }
                }
            }
            catch { }
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

        private void goto_win3()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_3;
            tpg_option_3.Show();

            tbcntrl_main.SelectedTab = tpg_right_import;
            tpg_right_import.Show();
            seltbp = false;
        }
        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void disp_list()
        {
            var dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
            var dateTo = dtp_to.Value.ToString("yyyy-MM-dd");
            DataTable dt;

            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                String WHERE = "";
                if (cbo_dtype.SelectedIndex != -1)
                {
                    WHERE = " t1.j_code='" + j_code + "' AND ";
                }
                else
                {
                    String j_type = db.get_colval("m05type", "code", "lower(name)='" + jtype_name.ToLower() + "'");
                    WHERE = " t1.j_code IN (SELECT m5.j_code FROM rssys.m05 m5 WHERE m5.j_type='" + j_type + "') AND ";
                }

                dt = db.QueryBySQLCode("SELECT t1.*, tc2.*, t3.j_memo, m10.mp_desc,m04.at_desc FROM rssys.tr01 t1 LEFT JOIN (SELECT DISTINCT j_code, j_num, pay_code, at_code, credit FROM rssys.tr02 WHERE COALESCE(credit,0)<>0  AND (SELECT COUNT(tr2.j_num) FROM rssys.tr02 tr2 WHERE COALESCE(credit,0)<>0 AND tr2.j_num=tr02.j_num AND tr2.j_code=tr02.j_code)=1) tc2 ON tc2.j_num=t1.j_num AND tc2.j_code=t1.j_code LEFT JOIN rssys.tr03 t3 ON t3.j_num=t1.j_num AND t3.j_code=t1.j_code LEFT JOIN rssys.m10 m10 ON m10.mp_code=tc2.pay_code LEFT JOIN rssys.m04 m04 ON m04.at_code=tc2.at_code WHERE " + WHERE + " t1.t_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "' AND tc2.j_num<>'' AND (t1.cancel IS NULL OR t1.cancel = '') ORDER BY t1.j_num DESC");
                //AND COALESCE(pay_code,'')<>''

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int r = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[r];
                   
                    row.Cells["dgvl_or_code"].Value = dt.Rows[i]["j_num"].ToString();// dt.Rows[i]["db_code"].ToString();
                    //row.Cells["dgvl_atname"].Value = dt.Rows[i]["at_desc"].ToString();
                    //row.Cells["dgvl_c_code"].Value = dt.Rows[i]["at_code"].ToString();//dt.Rows[i]["c_code"].ToString();
                    //row.Cells["dgvl_debt_name"].Value = dt.Rows[i]["sl_name"].ToString();// dt.Rows[i]["creditors"].ToString();
                    //row.Cells["dgvl_debt_code"].Value = dt.Rows[i]["sl_code"].ToString();// dt.Rows[i]["creditors_name"].ToString();
                    row.Cells["dgvl_desc"].Value = dt.Rows[i]["t_desc"].ToString();

                    row.Cells["dgvl_payment"].Value = dt.Rows[i]["at_desc"].ToString();
                    row.Cells["dgvl_c_code"].Value = dt.Rows[i]["at_code"].ToString();

                    row.Cells["dgvl_terms"].Value = dt.Rows[i]["mp_desc"].ToString();
                    row.Cells["dgvl_paycode"].Value = dt.Rows[i]["pay_code"].ToString();

                    //
                    row.Cells["dgvl_payamnt"].Value = dt.Rows[i]["credit"].ToString();
                    row.Cells["dgvl_explaination"].Value = dt.Rows[i]["j_memo"].ToString();// dt.Rows[i]["explaination"].ToString();
                    row.Cells["dgvl_check_no"].Value = dt.Rows[i]["ck_num"].ToString();// dt.Rows[i]["check_no"].ToString();
                    //row.Cells["dgvl_mp_code"].Value = dt.Rows[i]["db_code"].ToString();// dt.Rows[i]["mp_code"].ToString();
                    row.Cells["dgvl_check_date"].Value = gm.toDateString(dt.Rows[i]["ck_date"].ToString(), "");// dt.Rows[i]["check_date"].ToString();
                    row.Cells["dgvl_user_id"].Value = dt.Rows[i]["user_id"].ToString();//dt.Rows[i]["user_id"].ToString();
                    row.Cells["dgvl_trans_date"].Value = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");//dt.Rows[i]["trans_date"].ToString();

                    row.Cells["dgvl_payee"].Value = dt.Rows[i]["payee"].ToString();
                    //row.Cells["dgvl_reference"].Value = dt.Rows[i]["db_code"].ToString();//dt.Rows[i]["explaination"].ToString();
                    //
                    row.Cells["dgvl_dtype"].Value = dt.Rows[i]["j_code"].ToString();
                    cbo_codetype.SelectedValue = dt.Rows[i]["j_code"].ToString();
                    row.Cells["dgvl_dtype_desc"].Value = cbo_codetype.Text.ToUpper();
                    
                    row.Cells["dgv_branchid"].Value = dt.Rows[r]["branch"].ToString();
                    cbo_branch.SelectedValue = dt.Rows[r]["branch"].ToString();
                    row.Cells["dgv_branch"].Value = cbo_branch.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Display list : " + ex.Message);
            }
        }

        private void disp_itemlist(String inv_num)
        {

            lnno_last = 1;
            DataTable dt;

            try
            {
                dt = db.QueryBySQLCode("SELECT t2.*, m4.at_desc, m8.cc_desc FROM rssys.tr02 t2 LEFT JOIN rssys.m04 m4 ON m4.at_code=t2.at_code LEFT JOIN rssys.m08 m8 ON m8.cc_code=t2.cc_code  WHERE j_num='" + inv_num + "' AND COALESCE(debit,'0')>0 ORDER BY seq_num ASC");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();

                    dgv_itemlist["seqno", i].Value = dt.Rows[i]["seq_num"].ToString();
                    dgv_itemlist["accttitle", i].Value = dt.Rows[i]["at_desc"].ToString();
                    dgv_itemlist["dgv_ccname", i].Value = dt.Rows[i]["cc_desc"].ToString();
                    dgv_itemlist["debit", i].Value = gm.toAccountingFormat(dt.Rows[i]["debit"].ToString());
                    dgv_itemlist["invoice", i].Value = dt.Rows[i]["invoice"].ToString();
                    dgv_itemlist["dgv_atcode", i].Value = dt.Rows[i]["at_code"].ToString();
                    dgv_itemlist["dgv_cccode", i].Value = dt.Rows[i]["cc_code"].ToString();
                    dgv_itemlist["dgv_crdtrname", i].Value = dt.Rows[i]["sl_name"].ToString();
                    dgv_itemlist["dgv_crdtrcode", i].Value = dt.Rows[i]["sl_code"].ToString();
                    dgv_itemlist["dgv_notes", i].Value = dt.Rows[i]["seq_desc"].ToString();
                    
                    lnno_last = int.Parse(dt.Rows[i]["seq_num"].ToString());
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
            Double payment = 0.00;
            try { payment = gm.toNormalDoubleFormat(txt_pay_amnt.Text); }
            catch { }

            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count; i++)
                {
                    try{ total += gm.toNormalDoubleFormat(dgv_itemlist["debit", i].Value.ToString()); }
                    catch { }
                }
            }
            catch (Exception) { }

            //txttoa.Text = gm.toAccountingFormat(total);
            txt_totalamount.Text = gm.toAccountingFormat(total);
            txt_payment.Text = gm.toAccountingFormat(payment);
            txt_amtdue.Text = gm.toAccountingFormat(total - payment);
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

        private void inc_lnno()
        {
            lnno_last = lnno_last + 1;
        }

       

        private void btn_additem_Click(object sender, EventArgs e)
        {
            // a_disbursement frm,Boolean isnew, String code, int ln_no, String at_code
            int r = 0;
            if (dgv_itemlist.Rows.Count < 1)
            {
                lnno_last = 1;
                z_add_disburse_item frm = new z_add_disburse_item(this, true, "", lnno_last);
                //inc_lnno();
                frm.ShowDialog();
            }
            else
            {
                r = dgv_itemlist.Rows.Count - 1;
                lnno_last = Convert.ToInt32(dgv_itemlist["seqno", r].Value.ToString());
                //MessageBox.Show("R="+r + "\n Lnno_last="+lnno_last);
                z_add_disburse_item frm = new z_add_disburse_item(this, true, "", lnno_last + 1);
                //inc_lnno();
                frm.ShowDialog();
            }
            
        }
        
        public DataTable get_je_dgv(String at_code, String sl_code)
        {
            DataTable dt = new DataTable();
            String l_invoice = "", l_sl_code = "", l_at_code = "", l_debit = "0.00", l_credit = "0.00";
            int i;
            Double debit = 0.0, credit = 0.0;
            try
            { // credit invoice dgv_atcode
                dt.Columns.Add("invoice", typeof(String));
                dt.Columns.Add("debit", typeof(String));
                dt.Columns.Add("credit", typeof(String));

                //l_sl_code = (cbo_creditors.SelectedValue??"").ToString();

                for (i = 0; i < dgv_itemlist.Rows.Count; i++)
                {
                    try { debit += gm.toNormalDoubleFormat(dgv_itemlist["debit", i].Value.ToString()); }catch{}
                }
                credit = gm.toNormalDoubleFormat(txt_pay_amnt.Text);

                for (i = 0; i < dgv_itemlist.Rows.Count; i++)
                {
                    l_invoice = dgv_itemlist["invoice", i].Value.ToString();
                    l_at_code = dgv_itemlist["dgv_atcode", i].Value.ToString();
                    l_sl_code = dgv_itemlist["dgv_crdtrcode", i].Value.ToString();

                    if (String.IsNullOrEmpty(l_invoice) == false && at_code == l_at_code && sl_code == l_sl_code)
                    {
                        if (String.IsNullOrEmpty(dgv_itemlist["debit", i].Value.ToString()) == false)
                        {
                            l_debit = dgv_itemlist["debit", i].Value.ToString();
                            Double dr = gm.toNormalDoubleFormat(l_debit);
                            Double cr = (credit / debit) * dr;
                            if (cr >= dr) {
                                cr = dr;
                            }
                            l_credit = gm.toAccountingFormat(cr);
                        }
                        dt.Rows.Add(l_invoice, l_debit, l_credit);
                    }

                    l_invoice = ""; l_sl_code = ""; l_at_code = ""; l_debit = "0.00"; l_credit = "0.00";
                }
                //dt.Rows.Add(txt_code.Text, "0.00", gm.toNormalDoubleFormat(txt_pay_amnt.Text).ToString("0.00"));
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            return dt;
        }

        public void set_item(DataTable dt)
        {
            int seq_num = 1; 
            String j_code = "", sl_code = "", invoice = "", debit = "", credit = "";

            try
            {
                try
                {
                    lnno_last = Convert.ToInt32(dgv_itemlist["seqno", (dgv_itemlist.Rows.Count - 1)].Value.ToString());
                    seq_num = lnno_last + 1;
                }
                catch { }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int indx = dgv_itemlist.Rows.Add();

                    DataGridViewRow row = dgv_itemlist.Rows[indx];
                    row.Cells[0].Value = seq_num; //dt.Rows[i][0].ToString(); //seq_num
                    /*row.Cells[1].Value = dt.Rows[i]["at_desc"].ToString(); //at_desc
                    row.Cells[2].Value = dt.Rows[i]["debit"].ToString(); //debit
                    row.Cells[3].Value = dt.Rows[i]["credit"].ToString(); //credit
                    row.Cells[4].Value = dt.Rows[i]["invoice"].ToString(); //invoice
                    row.Cells[5].Value = dt.Rows[i]["sl_name"].ToString(); //sl_name
                    row.Cells[6].Value = dt.Rows[i]["cc_desc"].ToString(); //cc_desc
                    row.Cells[7].Value = dt.Rows[i]["pay_code"].ToString(); //pay_code
                    row.Cells[8].Value = dt.Rows[i]["rep_code"].ToString(); //rep_code
                    row.Cells[9].Value = dt.Rows[i]["seq_desc"].ToString(); //seq_desc
                    row.Cells[10].Value = dt.Rows[i]["at_code"].ToString(); //at_code
                    row.Cells[11].Value = dt.Rows[i]["sl_code"].ToString(); //sl_code
                    row.Cells[12].Value = dt.Rows[i]["cc_code"].ToString(); //cc_code*/
                    
                    //
                    // 

                    debit = dt.Rows[i]["credit"].ToString();
                    credit = dt.Rows[i]["debit"].ToString();

                    row.Cells[1].Value = dt.Rows[i]["at_desc"].ToString();
                    row.Cells[2].Value = dt.Rows[i]["sl_name"].ToString();
                    row.Cells[3].Value = dt.Rows[i]["cc_desc"].ToString();
                    row.Cells[4].Value = debit; //dt.Rows[i]["debit"].ToString();
                    row.Cells[5].Value = dt.Rows[i]["invoice"].ToString();
                    row.Cells[6].Value = "Unpaid Invoice with Invoice#" + dt.Rows[i]["invoice"].ToString();
                    row.Cells[7].Value = dt.Rows[i]["at_code"].ToString();
                    row.Cells[8].Value = dt.Rows[i]["cc_code"].ToString();
                    row.Cells[9].Value = dt.Rows[i]["sl_code"].ToString();
                    
                    //sl_name
                    j_code = this.j_code;
                    sl_code = dt.Rows[i]["sl_code"].ToString();
                    invoice = dt.Rows[i]["invoice"].ToString();

                    //set_nextln(nxt_ln);
                    //this.inc_lnno();
                    
                    seq_num++;
                }

                if (String.IsNullOrEmpty(invoice) == false)
                {
                    db.upd_unpaid_invoices(j_code, sl_code, invoice, Convert.ToDouble(debit), Convert.ToDouble(credit));
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }


            //disp_total_drcr_bal();
            disp_total();
            //goto_win2_option();
        }



        public void set_disp(String code, Boolean is_new, String ln_no, String at_code, String at_desc, String invoice, String amount, String notes)
        {

            this.code = code;
            this.is_new = is_new;
            this.ln_no = ln_no;
            this.atcode = at_code;
            this.at_desc = at_desc;
            this.invoicee = invoice;
            this.amount = amount;
            this.notes = notes;

            MessageBox.Show("Line number" + ln_no.ToString() + " Code:" + code + " At_code=" + at_code.ToString() + " At_desc=" + at_desc.ToString() + " Invoice=" + invoice.ToString() + " amount=" + amount.ToString() + " notes=" + notes.ToString());
            
            int i = 0;
          
        }
        public void mainsave( Boolean isnew, String line_no)
        {   

        
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            m_customers frm = new m_customers(this, true);
            frm.ShowDialog();
        }
        public void set_custvalue_frm(String custcode)
        {
            //try { cbo_creditors.Items.Clear(); }
            //catch { }

            //gc.load_customer(cbo_creditors);
            //cbo_creditors.SelectedValue = custcode;
        }
        private void btn_saveorder_Click(object sender, EventArgs e)
        {
            //mainsave(is_new,"");
            z_Notification notify = new z_Notification();
            Boolean success = false;
            String notifyadd = "";
            String code, supp_id, supp_name, terms, loc, dt_inv, reference, stk_ref = "", stk_po_so = "";
            String i_oldqty = "";
            String di_code = "", di_qty = "0.00";
            String col = "", val = "";
            String creditors = "", explaination = "", check_no = "", mp_code = "", user_id = "", c_code = "", remark = "",check_date="",cashier="",acct_lnk="",trans_date="",creditors_name="", fy = "", mo = "", fymo = "", tranx_date = "";
            String notificationText = "";
            String table = "disbursementhdr";
            String tableln = "disbursementlne";


            /*if (cbo_creditors.Enabled && cbo_creditors.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Creditor.");
                cbo_creditors.DroppedDown = true;
            }
            else*/
            if (String.IsNullOrEmpty(txt_desc.Text))
            {
                MessageBox.Show("Please enter the Description.");
            }
            else if (cbo_modeofpayment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Mode of Payment");
                cbo_modeofpayment.DroppedDown = true;
            }
            else if (gm.toNormalDoubleFormat(txt_pay_amnt.Text) <=0)
            {
                MessageBox.Show("Must be the payment amount less than zero(0).");
            }
            else if (txt_ckno.Enabled && txt_ckno.Text == "")
            {
                MessageBox.Show("Please Input Check Number");
            }
            else if (dgv_itemlist.Rows.Count == 0)
            {
                MessageBox.Show("Empty Account Link");
            }
            //if(cbo_cashier.SelectedIndex == -1)
            //{
            //    MessageBox.Show("No Cashier found!");
            //}   
            else
            {
                //DateTime valdate;

                //valdate = dtp_ckdt.Value.ToString("yyyy-MM-dd");
                code = txt_code.Text;
                //creditors_name = cbo_creditors.Text;
                explaination = rtxt_tech_remarks.Text;
                check_no = txt_ckno.Text;
                check_date = dtp_ckdt.Value.ToString("yyyy-MM-dd");
                //String at_code = cbo_modeofpayment.SelectedValue.ToString();
                //mp_code = (cbo_terms.SelectedValue??"").ToString();
                //user_id = cbo_cashier.SelectedValue.ToString();
                //c_code = (cbo_acct_link.SelectedValue ?? "").ToString();
                remark = rtxt_tech_remarks.Text;
                notificationText = "has added: ";


                //creditors = (cbo_creditors.SelectedValue ?? "").ToString();
                //acct_lnk = (cbo_acct_link.SelectedValue ?? "").ToString();

                trans_date = db.get_systemdate("");
                DateTime dt_t = DateTime.Parse(trans_date);

                String j_num = txt_code.Text;
                String t_desc = txt_desc.Text;
                /* t_desc = cbo_acct_link.Text + " as of Transaction Date "+ trans_date;
                if(!String.IsNullOrEmpty(creditors))
                {
                    t_desc = "Payment For "+cbo_creditors.Text+" as of Transaction Date "+ trans_date;
                }*/
                tranx_date = dtp_dt.Value.ToString("yyyy-MM-dd");
                fymo = db.get_fy_period(tranx_date);
                fy = fymo.Split('-').GetValue(0).ToString();
                mo = fymo.Split('-').GetValue(1).ToString();

                String seq_num = "0", sl_code, sl_name, cc_code, debit, credit, invoice = "", seq_desc, rep_code, pay_code;

                if (isnew)
                {
                    //
                    if (db.add_jrnl(fy, mo, j_code, j_num, t_desc, cbo_payee.Text, txt_ckno.Text, null, tranx_date, dtp_ckdt.Value.ToString("yyyy-MM-dd")))
                    {
                        db.UpdateOnTable("tr01", "branch='" + (cbo_branch.SelectedValue ?? "").ToString() + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                        
                        if (String.IsNullOrEmpty(rtxt_tech_remarks.Text) == false)
                        {
                            db.add_jrnl_explanation(j_code, j_num, rtxt_tech_remarks.Text);
                        }

                        try
                        {

                            for (int i = 0; i < dgv_itemlist.Rows.Count; i++)
                            {
                                seq_num = dgv_itemlist["seqno", i].Value.ToString();
                                at_code = dgv_itemlist["dgv_atcode", i].Value.ToString();
                                sl_code = dgv_itemlist["dgv_crdtrcode", i].Value.ToString();
                                sl_name = dgv_itemlist["dgv_crdtrname", i].Value.ToString();
                                cc_code = dgv_itemlist["dgv_cccode", i].Value.ToString();
                                credit = "0.00";
                                
                                debit = dgv_itemlist["debit", i].Value.ToString();
                                invoice = dgv_itemlist["invoice", i].Value.ToString();

                                seq_desc = dgv_itemlist["dgv_notes", i].Value.ToString();
                                if (String.IsNullOrEmpty(seq_desc))
                                {
                                    seq_desc = "Disbursement : Account Link#" + at_code + "" + (sl_code == "" ? "" : ", Subsidiary#" + sl_code);
                                }

                                rep_code = "";
                                pay_code = "";

                                if (String.IsNullOrEmpty(invoice) == false)
                                {
                                    db.upd_unpaid_invoices(j_code, sl_code, invoice, Convert.ToDouble(debit), Convert.ToDouble(credit));
                                }

                                db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit, credit, invoice, seq_desc, rep_code, pay_code, null, null);
                            }


                            //for credit
                            seq_num = (int.Parse(seq_num) + 1).ToString();
                            sl_code = ""; sl_name = "";cc_code = "";
                            credit = gm.toNormalDoubleFormat(txt_pay_amnt.Text).ToString("0.00");

                            debit = "0.00";invoice = "";

                            rep_code = "";
                            pay_code = (cbo_terms.SelectedValue??"").ToString();
                            at_code = cbo_modeofpayment.SelectedValue.ToString();
                            //pay_code = cbo_modeofpayment.SelectedValue.ToString();
                            //at_code = db.get_colval("m10", "at_code", "mp_code='" + pay_code + "'");

                            //seq_desc = "Disbursement : Account Link#" + at_code + "" + (sl_code == "" ? "" : ", Creditor#" + sl_code);
                            seq_desc = "Disbursement: Account Link#" + at_code + "" + (pay_code == "" ? "" : ", Payment#" + pay_code);

                            if (String.IsNullOrEmpty(invoice) == false)
                            {
                                db.upd_unpaid_invoices(j_code, sl_code, invoice, Convert.ToDouble(debit), Convert.ToDouble(credit));
                            }
                            db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit, credit, invoice, seq_desc, rep_code, pay_code, null, null);

                            success = true;
                        }
                        catch (Exception) { }

                        db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");

                        MessageBox.Show("Journal inserted successfully.");
                    }

                    /*
                    code = db.get_pk("db_code");
                    col = "db_code, creditors, explaination, check_no, mp_code, user_id, c_code, reference, check_date, trans_date,creditors_name";
                    val = "'" + code + "','" + creditors + "','" + explaination + "','" + check_no + "','" + mp_code + "','" + user_id + "','" + c_code + "','" + remark + "','" + check_date + "','" + trans_date + "','" + creditors_name + "'";
                    try
                    {
                        if (db.InsertOnTable(table, col, val))
                        {
                            notifyadd = add_items(tableln, code);

                            if (String.IsNullOrEmpty(notifyadd) == false)
                            {
                                notificationText += db.str_E(notifyadd);
                                notificationText += Environment.NewLine + " with #" + code;
                                notify.saveNotification(db.str_E(notificationText), "Disbursement Entry");
                                db.set_pkm99("db_code", db.get_nextincrementlimitchar(code, 8));
                                success = true;
                            }
                            else
                            {
                                db.DeleteOnTable(table, "db_code='" + code + "'");
                                db.DeleteOnTable(tableln, "db_code='" + code + "'");
                                success = false;
                            }
                        }

                        else
                        {
                            success = false;
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    catch (Exception er) { MessageBox.Show(er.Message + "addadd"); }*/
                }
                else
                {
                    if (db.upd_jrnl(fy, mo, j_code, j_num, t_desc, cbo_payee.Text, txt_ckno.Text, null, dtp_dt.Value.ToString("yyyy-MM-dd"), dtp_ckdt.Value.ToString("yyyy-MM-dd")))
                    {
                        db.UpdateOnTable("tr01", "branch='" + (cbo_branch.SelectedValue ?? "").ToString() + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");

                        if (db.isExists("tr03", "j_code = '" + j_code + "' AND j_num = '" + j_num + "'"))
                        {
                            db.upd_jrnl_explanation(j_code, j_num, rtxt_tech_remarks.Text);
                        }
                        else
                        {
                            db.add_jrnl_explanation(j_code, j_num, rtxt_tech_remarks.Text);
                        }

                        //add all journal entries and
                        try
                        {
                            db.del_jrnl_entry(j_code, j_num);

                            for (int i = 0; i < dgv_itemlist.Rows.Count; i++)
                            {
                                seq_num = dgv_itemlist["seqno", i].Value.ToString();
                                at_code = dgv_itemlist["dgv_atcode", i].Value.ToString();
                                sl_code = dgv_itemlist["dgv_crdtrcode", i].Value.ToString();
                                sl_name = dgv_itemlist["dgv_crdtrname", i].Value.ToString();
                                cc_code = dgv_itemlist["dgv_cccode", i].Value.ToString();
                                credit = "0.00";

                                debit = dgv_itemlist["debit", i].Value.ToString();
                                invoice = dgv_itemlist["invoice", i].Value.ToString();

                                seq_desc = dgv_itemlist["dgv_notes", i].Value.ToString();
                                if (String.IsNullOrEmpty(seq_desc))
                                {
                                    seq_desc = "Disbursement : Account Link#" + at_code + "" + (sl_code == "" ? "" : ", Subsidiary#" + sl_code);
                                }
                                rep_code = "";
                                pay_code = "";

                                if (String.IsNullOrEmpty(invoice) == false)
                                {
                                    db.upd_unpaid_invoices(j_code, sl_code, invoice, Convert.ToDouble(debit), Convert.ToDouble(credit));
                                }

                                db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit, credit, invoice, seq_desc, rep_code, pay_code, null, null);
                            }

                            //for credit
                            seq_num = (int.Parse(seq_num) + 1).ToString();
                            sl_code = ""; sl_name = ""; cc_code = "";
                            credit = gm.toNormalDoubleFormat(txt_pay_amnt.Text).ToString("0.00");

                            debit = "0.00"; invoice = "";


                            rep_code = "";
                            pay_code = (cbo_terms.SelectedValue ?? "").ToString();
                            at_code = cbo_modeofpayment.SelectedValue.ToString();
                            //pay_code = cbo_modeofpayment.SelectedValue.ToString();
                            //at_code = db.get_colval("m10", "at_code", "mp_code='" + pay_code + "'");

                            //seq_desc = "Disbursement : Account Link#" + at_code + "" + (sl_code == "" ? "" : ", Creditor#" + sl_code);
                            seq_desc = "Disbursement: Account Link#" + at_code + "" + (pay_code == "" ? "" : ", Payment#" + pay_code);

                            if (String.IsNullOrEmpty(invoice) == false)
                            {
                                db.upd_unpaid_invoices(j_code, sl_code, invoice, Convert.ToDouble(debit), Convert.ToDouble(credit));
                            }
                            db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit, credit, invoice, seq_desc, rep_code, pay_code, null, null);

                            success = true;
                        }
                        catch (Exception) { }

                        MessageBox.Show("Journal updated successfully.");
                    }
                   /* //code = txt_code.Text;
                    if (cbo_cashier.SelectedIndex == -1)
                    {
                        cashier = "";
                    }
                    else
                    {

                        cashier = cbo_cashier.SelectedValue.ToString();
                    }
                    col = "db_code='" + code + "', creditors='" + creditors + "', explaination='" + remark + "', check_no='" + check_no + "', mp_code='" + mp_code + "', user_id='" + cashier + "', c_code='" + acct_lnk + "', reference='" + remark +"', trans_date='" + trans_date+ "', creditors_name='"+creditors_name+"'";
                    if (db.UpdateOnTable(table, col, "db_code='" + code + "'"))
                    {
                        db.DeleteOnTable(tableln, "db_code='" + code + "'");

                        notifyadd = add_items(tableln, code);

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
                    }*/
                }
            }

            if (success)
            {
                disp_list();
                goto_win1();
                frm_clear();
            }
        }

        private void btn_upd_Click_1(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            isnew = false;
            try
            {
                r = dgv_list.CurrentRow.Index;
                code = dgv_list["dgvl_or_code", r].Value.ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    txt_code.Text = code;
                    //cbo_acct_link.SelectedValue = dgv_list["dgvl_c_code", r].Value.ToString();
                    //cbo_creditors.SelectedValue = dgv_list["dgvl_debt_code", r].Value.ToString();
                    txt_desc.Text = dgv_list["dgvl_desc", r].Value.ToString();
                    txt_ckno.Text = dgv_list["dgvl_check_no", r].Value.ToString();
                    //dtp_invoicedt.Value = Convert.ToDateTime(dgv_list["dgvl_inv_date", r].Value.ToString());
                    //dtp_ckdt.Value = Convert.ToDateTime(dgv_list["dgvl_check_date", r].Value.ToString());
                    dtp_dt.Value = gm.toDateValue(dgv_list["dgvl_trans_date", r].Value.ToString());
                    rtxt_tech_remarks.Text = dgv_list["dgvl_explaination", r].Value.ToString();
                    dtp_ckdt.Value = gm.toDateValue(dgv_list["dgvl_check_date", r].Value.ToString());
                    cbo_modeofpayment.SelectedValue = (dgv_list["dgvl_c_code", r].Value ?? "").ToString();
                    cbo_terms.SelectedValue = (dgv_list["dgvl_paycode", r].Value ?? "").ToString();
                    txt_pay_amnt.Text = gm.toAccountingFormat(dgv_list["dgvl_payamnt", r].Value.ToString());
                    cbo_payee.Text = dgv_list["dgvl_payee", r].Value.ToString();

                    cbo_codetype.SelectedValue = dgv_list["dgvl_dtype", r].Value.ToString();
                    cbo_branch.SelectedValue = dgv_list["dgv_branchid", r].Value.ToString();

                    dgv_itemlist.Rows.Clear();
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

        private void btn_exit_Click(object sender, EventArgs e)
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

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            String qty = "";

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;


                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to Remove this account?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        //if (isnew == false)
                        //{
                        code = dgv_itemlist["seqno", r].Value.ToString();
                        //qty = dgv_itemlist["dgvi_oldqty", r].Value.ToString();
                        //dgv_delitem.Rows.Add(code, qty);
                        //}
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

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            int r = 0;
            int cur_index = 0;

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    cur_index = int.Parse(dgv_itemlist[0, r].Value.ToString());

                    z_add_disburse_item frm = new z_add_disburse_item(this, false, txt_code.Text, r);
                    //inc_lnno();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        /*private void cbo_creditors_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Value:"+cbo_creditors.SelectedValue.ToString()+ "\n  Desc:"+cbo_creditors.Text);
            String val = "";
            DataTable dt = null;
            try
            {
                dt = db.QueryBySQLCode("SELECT at_code FROM rssys.m06 WHERE d_code='" + cbo_creditors.SelectedValue.ToString() + "'");
            }
            catch { }
                //MessageBox.Show(dt.Rows.Count.ToString());
            try { 
            if (dt.Rows.Count != 0)
            { 
                cbo_acct_link.SelectedValue = dt.Rows[0]["at_code"].ToString();      
            
            }
                }
            catch{}
        }
        public void accnt_link(String cust)
        {
            String val = "";
            DataTable dt = null;
            dt = db.QueryBySQLCode("SELECT at_code FROM rssys.m06 WHERE d_code='"+cust+"'");
            if(dt.Rows.Count > 0)
            {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                val = dt.Rows[i]["at_code"].ToString();
                
            }
            cbo_acct_link.SelectedValue = val;
           }
        }

        private void cbo_acct_link_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cbo_creditors_SelectedValueChanged(object sender, EventArgs e)
        {

        }*/

        private void txt_payment_TextChanged(object sender, EventArgs e)
        {
            
            double payment = 0.00;
            double totalamt = 0.00;
            double diff = 0.00;
            try
            {
                if (txt_payment.Text == "")
                {

                    payment = 0.00;
                    txt_payment.Text = "0.00";
                }
                payment = Double.Parse(txt_payment.Text);
                totalamt = Double.Parse(txt_totalamount.Text);
                diff = totalamt - payment;
                txt_amtdue.Text = gm.toAccountingFormat(diff.ToString());
            }
            catch { }
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

        private void dtp_frm_ValueChanged_1(object sender, EventArgs e)
        {
            disp_list();
        }

        private void dtp_to_ValueChanged_1(object sender, EventArgs e)
        {
            disp_list();
        }

        /*private void cbo_acct_link_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_acct_link.SelectedIndex != -1)
                {
                    cbo_creditors.Enabled = false;
                    String at_code = cbo_acct_link.SelectedValue.ToString();

                    if (db.has_subledger(at_code))
                    {
                        gc.load_subsidiaryname(cbo_creditors, at_code);
                        cbo_creditors.Enabled = (((DataTable)cbo_creditors.DataSource).Rows.Count != 0);
                    }
                    
                }
            }
            catch (Exception) { }
        }*/

        private void txt_EnterTextNumber(object sender, System.EventArgs e)
        {
            TextBox txt_num = (TextBox)sender;
            if (txt_num.Text != String.Empty)
            {
                txt_num.Text = gm.toNormalDoubleFormat(txt_num.Text).ToString("0.00");
                txt_num.Select(txt_num.Text.Length, 0);
            }
        }

        private void txt_LeaveTextNumber(object sender, System.EventArgs e)
        {
            TextBox txt_num = (TextBox)sender;
            if (txt_num.Text != String.Empty)
            {
                txt_num.Text = gm.toAccountingFormat(txt_num.Text);
                txt_num.Select(0, 0);
            }
        }

        private void txt_pay_amnt_TextChanged(object sender, EventArgs e)
        {
            disp_total();
        }

        private void cbo_terms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_terms.Text.ToLower().Equals("check"))
            {
                ischeck = true;
            }
            else
            {
                ischeck = false;
                txt_ckno.Text = "";
            }

            txt_ckno.Enabled = ischeck;
            dtp_ckdt.Enabled = ischeck;
        }
        private void cbo_modeofpayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_modeofpayment.SelectedIndex != -1)
            {
                String term = db.get_colval("m10", "mp_code", "at_code='" + cbo_modeofpayment.SelectedValue.ToString() + "'");
                if (!String.IsNullOrEmpty(term))
                {
                    cbo_terms.SelectedValue = term;
                }
                else
                {
                    cbo_terms.SelectedIndex = -1;
                }
            }
        }

        private void cbo_dtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_dtype.SelectedIndex != -1)
            {
                j_code = cbo_dtype.SelectedValue.ToString();
                jrnl_name = db.get_colval("m05", "j_desc", "j_code='" + j_code + "'");
                disp_list();
            }
        }

        private void cbo_codetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_codetype.SelectedIndex != -1)
            {
                j_code = cbo_codetype.SelectedValue.ToString();
                jrnl_name = db.get_colval("m05", "j_desc", "j_code='" + j_code + "'");
            }
        }


        //Import Function
        private void btn_goimport_Click(object sender, EventArgs e)
        {
            if (cbo_dtype.SelectedIndex != -1)
            {
                openFile.FileName = "";
                btn_import.Enabled = false;
                txt_filename.Text = ".";
                goto_win3();
            }
            else
            {
                MessageBox.Show("Select Journal Type first.");
                cbo_dtype.DroppedDown = true;
            }
        }
        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_win1();
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            if (txt_filename.Text == "." && txt_filename.Text != openFile.FileName)
            {
                MessageBox.Show("Please select and excel file to import.");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to import this excel?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    importBgWorker.RunWorkerAsync();
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_filename.Text = openFile.FileName;
                btn_import.Enabled = true;
            }
            else
            {
                btn_import.Enabled = false;
            }
        }

        private void importBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            Boolean success = false;
            String str = "";
            int rCnt = 0;
            int cCnt = 0;
            int rw = 0, trw = 0;
            int cl = 0;
            String filename = "";
            int i = 0;
            int count = 0;

            //String code = "", acc_desc = "", acc_code = "", cmp_code = "", cmp_desc = "", mag_code = "", mag_desc = "", accttype_code = "", at_code = "", at_desc = "", bs_pl = "", dr_cr = "", sl = "", payment = "";
            //String col = "", val = "", add_col = "", add_val = "";
            //String table = "m06";
            String code = "", col = "", val = "";
            String last_name = "", first_name = "", mid_name = "", birth_date = "", mobile = "", email = "", address1 = "", dt_strval = "", acct_no = "", reg_date = "", arr_time = "", dep_date = "", dep_time = "", rmrttyp = "", rom_code = "", typ_code = "", rom_rate = "", govt_tax = "", serv_chg = "", discount = "", reg_num = "", chg_desc = "", chg_code = "", chg_date = "", chg_desc2 = "", chg_code2 = "", chg_date2 = "", reference = "", amount = "", total_room_rate = "";
            String tacct_no = "", treg_num = "", trom_code = "", tchg_code = "";

            DateTime sysdt = DateTime.Parse(db.get_systemdate("") + " " + DateTime.Now.ToString("HH:mm tt"));
            String refno = "", period = "", paidto = "", refdesc = "", refdate = "", chkno = "", chkdate = "", branch = "";
            List<dynamic> list = new List<dynamic>();
            DateTime tmpdt;
            String j_code = "";
            cbo_dtype.Invoke(new Action(() =>{
                j_code = cbo_dtype.SelectedValue.ToString();
            }));
            String j_num = "";
            
            try
            {

                filename = openFile.FileName;
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(@filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;

                if (rw > 0)
                {
                    btnBrowse.Invoke(new Action(() =>
                    {
                        btnBrowse.Enabled = false;
                    }));
                    btn_import.Invoke(new Action(() =>
                    {
                        btn_import.Enabled = false;
                    }));
                    lbl_max.Invoke(new Action(() =>
                    {
                        lbl_max.Text = (rw - 1).ToString();
                    }));
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Maximum = rw;
                    }));
                    lbl_min.Invoke(new Action(() =>
                    {
                        lbl_min.Text = pbar.Minimum.ToString();
                    }));

                    trw = rw - 1;
                }
                for (rCnt = 1; rCnt <= rw; rCnt++)
                {
                    Boolean hasval = false;
                    for (int j = 1; j < 25 && !hasval; j++)
                        if (!String.IsNullOrEmpty(getString(range, rCnt, j)))
                            hasval = true;

                    if (hasval)
                    {
                        String option = getString(range, rCnt, 1).ToLower();

                        if (String.IsNullOrEmpty(option) || option.IndexOf("acct id") >= 0 || option.IndexOf("acct") >= 0)
                        {
                            success = false;
                        }
                        else
                        {
                            success = true;
                        }

                        if (success)
                        {
                            if (option.IndexOf("reference") >= 0)
                            {
                                 refno = getString(range, rCnt, 4);
                                 period = getString(range, rCnt, 6);
                                 paidto = getString(range, rCnt, 14);
                            } 
                            else if (option.IndexOf("description") >= 0)
                            {
                                 refdesc = getString(range, rCnt, 4);
                                 refdate = getDateString(range, rCnt, 6);
                                 chkno = getString(range, rCnt, 14);
                                 chkdate = getDateString(range, rCnt, 22);
                            }
                            else
                            {
                                list.Add(new {
                                    acctid = getString(range, rCnt, 1),
                                    acctdesc = getString(range, rCnt, 3),
                                    sl_code = getString(range, rCnt, 5),
                                    sl_name = getString(range, rCnt, 7),
                                    debit = gm.toNormalDoubleFormat(getString(range, rCnt, 13)).ToString("0.00"),
                                    credit = gm.toNormalDoubleFormat(getString(range, rCnt, 18)).ToString("0.00"),
                                    invoice = getString(range, rCnt, 19),
                                    cc_code = db.get_colval("m08", "cc_code", "UPPER(cc_desc)=" + db.str_E(getString(range, rCnt, 21).ToUpper()) + ""),
                                });
                            }
                        }
                    }

                    if (!hasval)
                    {
                        if (!String.IsNullOrEmpty(refno) && !String.IsNullOrEmpty(period) && !String.IsNullOrEmpty(refdesc) && !String.IsNullOrEmpty(refdate))
                        {
                            try
                            {
                                tmpdt = DateTime.Parse(refdate);

                                j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                                if(gm.toNormalDoubleFormat(j_num)<gm.toNormalDoubleFormat(refno))    
                                    j_num = refno;

                                if (db.add_jrnl(tmpdt.ToString("yyyy"), tmpdt.ToString("MM"), j_code, j_num, refdesc, paidto, chkno, null, sysdt.ToString("yyyy-MM-dd"), chkdate))
                                {

                                    db.UpdateOnTable("tr01", "branch='" + (GlobalClass.branch) + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                    db.add_jrnl_explanation(j_code, j_num, refdesc);
                                    
                                    for (int j = 0; j < list.Count; j++)
                                    {
                                        var line = list[j];

                                        if (String.IsNullOrEmpty(line.invoice) == false)
                                        {
                                            db.upd_unpaid_invoices(j_code, line.sl_code, line.invoice, Convert.ToDouble(line.debit), Convert.ToDouble(line.credit));
                                        }
                                        //acctid ,acctdesc, sl_code ,sl_name ,debit ,credit ,invoice 
                                        if (db.add_jrnl_entry(j_code, j_num, (j + 1).ToString(), line.acctid, line.sl_code, line.sl_name, line.cc_code, null, line.debit, line.credit, line.invoice, null, null, null, null, null)) {
                                            count++;
                                        }
                                    }
                                }

                                if (db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'"))
                                {
                                    count++;
                                }
                            }catch{}
                        }
                        list.Clear();
                        refno = ""; period = ""; paidto = ""; refdesc = ""; refdate = ""; chkno = ""; chkdate = "";
                    }

                    if (success)
                    {
                        inc_pbar(count, rw);
                        lbl_min.Invoke(new Action(() =>
                        {
                            lbl_min.Text = count.ToString();
                        }));
                    }
                }
                MessageBox.Show("Number of rows inserted : " + (count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nOr some problem of your data.");
            }

            btn_import.Invoke(new Action(() =>
            {
                btn_import.Enabled = true;
            }));
            pbar.Invoke(new Action(() =>
            {
                pbar.Value = 0;
            }));
            lbl_min.Invoke(new Action(() =>
            {
                lbl_min.Text = "0";
            }));
            lbl_max.Invoke(new Action(() =>
            {
                lbl_max.Text = "0";
            }));
            btnBrowse.Invoke(new Action(() =>
            {
                btnBrowse.Enabled = true;
                btn_import.Enabled = false;
            }));
        }

        public String getString(Excel.Range range, int row, int col)
        {
            String str = "";
            if (range != null)
            {
                try
                {
                    str = Convert.ToString((range.Cells[row, col] as Excel.Range).Value2 ?? "");
                }
                catch { }
            }
            return str;
        }
        public String getDateString(Excel.Range range, int row, int col)
        {
            DateTime dt = DateTime.Now;
            String dtstr = "";
            if (range != null)
            {
                try
                {
                    dtstr = getString(range, row, col);
                    try { dt = DateTime.Parse(dtstr); }
                    catch { dt = DateTime.FromOADate(Double.Parse(dtstr)); }
                }
                catch { }
            }
            return dt.ToString("yyyy-MM-dd");
        }
        public String getTimeString(Excel.Range range, int row, int col)
        {
            DateTime dt = DateTime.Now;
            String dtstr = "";
            if (range != null)
            {
                try
                {
                    dtstr = getString(range, row, col);
                    try { dt = DateTime.Parse(dt.ToString("yyyy-MM-dd ") + dtstr); }
                    catch { dt = DateTime.FromOADate(Double.Parse(dtstr)); }
                }
                catch { }
            }
            return dt.ToString("HH:mm");
        }
        private void inc_pbar(int i, int rw)
        {
            try
            {

                if (pbar.Value <= rw)
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = i;
                    }));

                }
                else
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = rw;
                    }));

                    btnBrowse.Invoke(new Action(() =>
                    {
                        btnBrowse.Enabled = true;
                    }));

                }

            }
            catch (Exception)
            {

            }
        }

        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            String _curCode = "", _curNum = j_code;
            try
            {
                _curCode = dgv_list["dgvl_or_code", dgv_list.CurrentRow.Index].Value.ToString();
            }
            catch { }

            if (MessageBox.Show("Are you sure to cancel this entry?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_curCode != "" || !String.IsNullOrEmpty(_curCode))
                {
                    if (db.QueryBySQLCode_bool("UPDATE rssys.tr01 SET cancel = 'Y' WHERE j_num = '" + _curCode + "' AND j_code = '" + j_code + "'"))
                    {
                        disp_list();
                        MessageBox.Show("Successfully cancelled entry.");
                    }
                    else
                    {
                        MessageBox.Show("Error.");
                    }
                }
            }
            else
            {

            }
        }
    }
}