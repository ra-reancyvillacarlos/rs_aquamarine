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
    public partial class s_Sales_Auto : Form
    {

        //main
        private dbSales db;
        private GlobalClass gc;
        private GlobalMethod gm;
        //form
        private m_customers frm_cust;
        private z_clerkpassword frm_pclerkpass;
        private z_enter_sales_item frm_si;
        private z_payment frm_payment;
        private z_so_list frm_solist;
        private int ln = 1;

        private Boolean isverified = false, isWin2Active = false;
        Boolean seltbp = false;
        private Boolean isnew = false;
        private Boolean isnew_item = true;

        String status = "";
        String stk_trns_type = "SO";//SI or RO
        Boolean isRepair = false, isCashier = false;

        int lnno_last = 1;
        int lnno_lastpay = 1;

        // Release Status 
        private String rocode_ready = "00006"; // READY TO RELEASE
        private String rocode_released = "00007";// RELEASE

        Boolean direct_sales = false;
        Boolean loan_application = false;



        public s_Sales_Auto()
        {
            InitializeComponent();
            init_load();            
        }

        public s_Sales_Auto(Boolean repair, Boolean cashier)
        {
            InitializeComponent();
            
            isRepair = repair;
            isCashier = cashier;
            sub_item_list();
            init_load();   
        }

        private void init_load()
        {
            db = new dbSales();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            frm_pclerkpass = new z_clerkpassword(this);

            gc.load_whouse(cbo_whsname);
            gc.load_outlet_carsale(cbo_outlet, true);
            gc.load_outlet_carsale(cbo_outlet_olist, true);

            gc.load_repair_orderstatus(cbo_rorder_status);
          
            //gc.load_customer(cbo_customer);
            gc.load_salesagent(cbo_agent);
            gc.load_vehicle_info(cbo_carunit);

            gc.load_salesclerk(cbo_clerk);
            gc.load_salesclerk(cbo_cashier);
            gc.load_marketsegment(cbo_marketsegment);
            gc.load_downpayment(cbo_dp_pct);
            gc.load_decision(cbo_status);
            gc.load_crm(cbo_customer);
            gc.load_insurance(cbo_insurance);
            //
            isnew = true;
            frm_reload();

            //default value for outlets
            try
            {
                cbo_outlet.SelectedIndex = 0;
                cbo_outlet_olist.SelectedIndex = 0;
            }
            catch { }

            if (isRepair)
            {
                cbo_cashier.Enabled = false;
                cbo_rorder_status.Enabled = false;
                btn_new.Visible = false;
                btn_print_agreement.Visible = false;
                btn_cancel.Enabled = false;

                frm_enable(false);

                //groupBox11.Visible = false;
                btn_saveorder.Enabled = true;

                frm_readOnly();

                btn_upd.Text = "Release Unit";
                btn_saveorder.Text = "Release";
            }
            else {
                btn_new.Enabled = true;

                if (isCashier) {
                    cbo_cashier.Enabled = false;
                    btn_new.Enabled = false;
                    btn_upd.Enabled = true;
                }

            }

            dgv_list_Cell_isClick(dgv_list.CurrentRow == null ? 0 : dgv_list.CurrentRow.Index);
        }

        private void s_Sales_Auto_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }
        }

        private void disp_list()
        {   
            DataTable dt = get_solist(dtp_frm.Value, dtp_to.Value, chk_pendingonly.Checked);
             Double total_amnt = 0.00, paid = 0.00;
            String car_item_code = "";

            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                dgv_list.DataSource = dt;
                AdjustColumnOrder();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        public DataTable get_solist(DateTime frm, DateTime to, Boolean pendingOnly)
        {
            String WHERE = "";
            String out_code = "";

            if (pendingOnly)
            {
                WHERE = " pending='Y' AND ";
            }
            if (cbo_outlet_olist.SelectedIndex > -1)
            {
                out_code = cbo_outlet_olist.SelectedValue.ToString();
                WHERE = WHERE + " out_code='" + out_code + "' AND ";
            }
            if (isRepair) {
                //WHERE = WHERE + " o.rorder_status='" + ro_stat_code + "' AND ";
                WHERE = WHERE + " ( o.rorder_status='" + rocode_ready + "' OR o.rorder_status='" + rocode_released + "' ) AND ";
            }
            //o.trnx_date AS t_date
            return db.QueryBySQLCode("SELECT ro.ro_stat_desc,o.out_code,o.ord_code,o.customer,o.payment,o.cancel,o.net_amnt,o.ord_date,o.t_date,o.tax_amnt,o.pending,o.amnt_due,o.disc_amnt, o.user_id,o.loc,o.user_id2,o.rep_code, o.cashier,o.gross_amnt,o.total_amnt AS ord_amnt,o.pay_code FROM " + db.schema + ".orhdr o LEFT JOIN  " + db.schema + ".whouse w ON w.whs_code=o.loc LEFT JOIN " + db.schema + ".ro_status ro ON ro.ro_stat_code=o.rorder_status WHERE " + WHERE + " (o.trnx_date BETWEEN '" + frm.ToString("yyyy-MM-dd") + "' AND '" + to.ToString("yyyy-MM-dd") + "') ORDER BY o.ord_code");
        }

        public DataTable get_soitemlist(String ord_code)
        {
            DataTable dt = null;

            try
            {
                dt = db.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id  WHERE ol.ord_code='" + ord_code + "' AND COALESCE(ln_type,'')='' ORDER BY " + db.castToInteger("ol.ln_num") + " ASC");
            }
            catch { }

            return dt;
        }

        public DataTable get_soitemlistpending()
        {
            DataTable dt = null;

            try
            {
                dt = db.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id  WHERE ol.pending='N' ORDER BY " + db.castToInteger("ol.ln_num") + " ASC");
            }
            catch { }

            return dt;
        }
        private void disp_itemlist(String code)
        {
            try
            {
                DataTable dt = get_soitemlist(code);

                try { dgv_itemlist.Rows.Clear(); }
                catch (Exception er) { MessageBox.Show(er.Message); }

                //Me.Show(dt.Rows.Count.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();
                    dgv_itemlist["dgvli_lnno", i].Value = dt.Rows[i]["ln_num"].ToString();
                    //dgv_itemlist["dgvli_unitid", i].Value = dt.Rows[i]["dgvli_unitid"].ToString();
                    dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[i]["part_no"].ToString(); //1
                    dgv_itemlist["dgvli_itemcode", i].Value = dt.Rows[i]["item_code"].ToString();//1
                    dgv_itemlist["dgvli_itemdesc", i].Value = dt.Rows[i]["item_desc"].ToString();//1
                    //dgv_itemlist["dgvli_unit", i].Value = dt.Rows[i]["unit_desc"].ToString();
                    dgv_itemlist["dgvli_qty", i].Value = dt.Rows[i]["ord_qty"].ToString(); //1
                    dgv_itemlist["dgvli_regprice", i].Value = dt.Rows[i]["price"].ToString(); //1
                    dgv_itemlist["dgvli_sellprice", i].Value = dt.Rows[i]["price"].ToString(); // 1 alalalal
                    dgv_itemlist["dgvli_lnamt", i].Value = dt.Rows[i]["ln_amnt"].ToString(); //1
                    dgv_itemlist["dgvli_taxamt", i].Value = dt.Rows[i]["ln_tax"].ToString(); //1

                    dgv_itemlist["dgvli_discamt", i].Value = dt.Rows[i]["disc_amt"].ToString(); //1
                    dgv_itemlist["dgvli_disccode", i].Value = dt.Rows[i]["disc_code"].ToString(); //1
                    dgv_itemlist["dgvli_discuser", i].Value = dt.Rows[i]["disc_user"].ToString(); //1
                    dgv_itemlist["dgvli_discreason", i].Value = dt.Rows[i]["disc_reason"].ToString(); //1

                    dgv_itemlist["dgvli_unit", i].Value = dt.Rows[i]["unit"].ToString(); //1
                    dgv_itemlist["dgvli_unitid", i].Value = dt.Rows[i]["unitid"].ToString(); //1
                    dgv_itemlist["dgvli_remarks", i].Value = dt.Rows[i]["reference"].ToString(); //1

                    lnno_last = int.Parse(dt.Rows[i]["ln_num"].ToString());
                }
                total_itemamount();
                inc_lnno();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            try {
                DataTable itemdt4 = db.QueryBySQLCode("SELECT o.ln_num,o.ln_amnt,o.rep_code,o.reference,o.pay_code,m.mp_desc,o.ln_type, o.chargeto FROM rssys.orlne o LEFT JOIN rssys.m10 m ON o.pay_code=m.mp_code WHERE o.ord_code='" + code + "' AND ln_type='P' ORDER BY ln_num ASC");
                try { dgv_payment.Rows.Clear(); }
                catch (Exception) { }
                for (int i = 0; i < itemdt4.Rows.Count; i++)
                {
                    dgv_payment.Rows.Add();

                    dgv_payment["dgvlp_ln_num", i].Value = itemdt4.Rows[i]["ln_num"].ToString();
                    dgv_payment["dgvlp_pay_code", i].Value = itemdt4.Rows[i]["pay_code"].ToString();
                    dgv_payment["dgvlp_pay_desc", i].Value = itemdt4.Rows[i]["mp_desc"].ToString();
                    dgv_payment["dgvlp_reference", i].Value = itemdt4.Rows[i]["reference"].ToString();
                    dgv_payment["dgvlp_ln_amnt", i].Value = gm.toAccountingFormat(itemdt4.Rows[i]["ln_amnt"].ToString());
                    dgv_payment["dgvlp_rep_code", i].Value = itemdt4.Rows[i]["rep_code"].ToString();
                    dgv_payment["dgvlp_ln_type", i].Value = itemdt4.Rows[i]["ln_type"].ToString();
                    dgv_payment["dgvlp_chargeto", i].Value = itemdt4.Rows[i]["chargeto"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

        }


        public void frm_reload()
        {
            frm_reset();
            frm_clear();

            frm_enable(true);
        }

        public void frm_enable(Boolean flag)
        {
            btn_additem.Enabled = flag;
            btn_upditem.Enabled = flag;
            btn_delitem.Enabled = flag;
            btn_saveorder.Enabled = flag;

            txt_servex.Enabled = flag;
            cbo_agent.Enabled = flag;
            cbo_marketsegment.Enabled = flag;
            //cbo_customer.Enabled = flag;
            cbo_agent.Enabled = flag;
        }

        private void frm_reset()
        {
            isnew = true;
            isverified = false;

            direct_sales = false;
            loan_application = false;
        }

        private void frm_clear()
        {
            try
            {
                cbo_customer.SelectedIndex = -1;
                lbl_custcode.Text = cbo_customer.SelectedValue.ToString();
            }
            catch (Exception) { }

            cbo_rorder_status.SelectedIndex = (cbo_rorder_status.SelectedValue == null ? -1 : 0);
         
            txt_ordcode.Text = "";
            txt_appcode.Text = "";
            cbo_customer.SelectedIndex = -1;
            rtxt_address.Text = "";
            txt_contact.Text = "";
            txt_email.Text = "";

            cbo_insurance.SelectedIndex = -1;
            txt_servex.Text = "";
            cbo_loaner.Text = "";
            txt_loaner.Text = "";
            cbo_status.SelectedIndex = -1;
            cbo_marketsegment.SelectedIndex = -1;
            cbo_agent.SelectedIndex = -1;
            cbo_clerk.SelectedIndex = -1;
            cbo_cashier.SelectedIndex = -1;
            rtxt_remark.Text = "";
            txt_salescontrol.Text = "";

            cbo_itemcode.Text = "";
            cbo_carunit.SelectedIndex = -1;
            cbo_carunit.Text = "";
            txt_vin.Text = "";
            txt_engine.Text = "";
            txt_condno.Text = "";
            try { cbo_dp_pct.SelectedIndex = 0; }
            catch { }
            txt_srpprice.Text = "0.00";
            txt_dpamnt.Text = "0.00";
            txt_amnt_financed.Text = "0.00";
            cbo_terms.Text = "60";
            txt_add_on_rate.Text = "0.00";
            txt_clientDP.Text = "0.00";
            txt_totaldiscount.Text = "0.00";
            txt_monthly_amort.Text = "0.00";
            txt_totalpayment.Text = "0.00";

            txt_vat_table.Text = "0.00";
            txt_vat_exemp.Text = "0.00";
            txt_zero_rated.Text = "0.00";
            txt_vat_amnt.Text = "0.00";
            txt_vat_less.Text = "0.00";
            txt_srp_netvat.Text = "0.00";
            txt_totalitemamnt.Text = "0.00";

            try { dgv_itemlist.Rows.Clear(); }
            catch { }
            try { dgv_payment.Rows.Clear(); }
            catch { }
        }

        private void frm_readOnly()
        {
            
            txt_ordcode.ReadOnly = true;
            txt_appcode.ReadOnly = true;
            cbo_customer.Enabled = false;
            rtxt_address.ReadOnly = true;
            txt_contact.ReadOnly = true;
            txt_email.ReadOnly = true;

            txt_servex.ReadOnly = true;
            cbo_loaner.Enabled = false;
            cbo_insurance.Enabled = false;
            cbo_status.Enabled = false;
            cbo_marketsegment.Enabled = false;
            cbo_agent.Enabled = false;
            cbo_clerk.Enabled = false;
            //cbo_clerk.Enabled = false;
            cbo_cashier.Enabled = false;
            rtxt_remark.ReadOnly = true;
            txt_salescontrol.ReadOnly = true;

            cbo_carunit.Enabled = false;
            txt_vin.ReadOnly = true;
            txt_engine.ReadOnly = true;
            txt_srpprice.ReadOnly = true;
            txt_condno.ReadOnly = true;
            cbo_dp_pct.Enabled = false;
            txt_dpamnt.ReadOnly = true;
            //txt_pn_amnt.ReadOnly = true;
            txt_amnt_financed.ReadOnly = true;
            //txt_reg_chg.ReadOnly = true;
            cbo_terms.Enabled = false;
            //txt_doc_stamp.ReadOnly = true;
            txt_add_on_rate.ReadOnly = true;
            //txt_processing_fee.ReadOnly = true;
            txt_monthly_amort.ReadOnly = true;
            //txt_max_sc.ReadOnly = true;
            //txt_reference.ReadOnly = true;
            //txt_paid_amnt.ReadOnly = true;
            txt_clientDP.ReadOnly = true;

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

            if (!isRepair)
            {
                frm_enable(true);
            }
        }

        private void inc_lnno()
        {
            lnno_last = lnno_last + 1;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            isnew_item = true;
            lnno_last = 1;
            if (isverified == false)
            {
                frm_reload();
                direct_sales = true;
               //frm_clear();
                frm_pclerkpass.ShowDialog();
            }
            else
            {
                MessageBox.Show("Already accessed to sales order.");
                goto_win2();
            }
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            String upd = "";
            String code = "", canc = "";
            int r = -1;
            int lastrow = 0;

            isnew = false;

            frm_clear();

            if (dgv_list.Rows.Count > 0)
            {

                try
                {
                    r = dgv_list.CurrentRow.Index;

                    try
                    {
                        canc = dgv_list["dgvl_cancel", r].Value.ToString();
                        upd = dgv_list["dgvl_pending", r].Value.ToString();
                    }
                    catch /*(Exception err)*/{ /*MessageBox.Show(canc.ToString());*/ }

                    code = dgv_list["dgvl_code", r].Value.ToString();

                    if (canc == "Y")
                    {
                        MessageBox.Show("SO# " + code + " is already cancelled. Can not be updated.");
                    }
                    else if (upd == "N")
                    {
                        MessageBox.Show("SO# " + code + " is already done. Can not be updated.");
                    }
                    else
                    {
                        if (isverified == false)
                        {
                            frm_pclerkpass.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Already accessed to sales order.");
                            goto_win2();
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No invoice selected.:P");
                }
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
            String upd = "";
            String canc = "";
            int r = -1;

            if (dgv_list.Rows.Count == 0)
            {
                MessageBox.Show("No Rows selected,");
            }
            else
            {
                r = dgv_list.CurrentRow.Index;

                try
                {
                    canc = dgv_list["dgvl_cancel", r].Value.ToString();
                    upd = dgv_list["dgvl_pending", r].Value.ToString();

                }
                catch /*(Exception err) */{ /*MessageBox.Show(canc.ToString()); */}

                if (canc == "Y")
                {
                    MessageBox.Show("SO# " + code + " is already cancelled. Can not be updated.");
                }
                else if (upd == "N")
                {
                    MessageBox.Show("SO# " + code + " is already done. Can not be updated.");
                }
                else
                {

                    code = dgv_list["dgvl_code", r].Value.ToString();

                    dialogResult = MessageBox.Show("Are you sure you want to cancel this SO# " + code + "?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        if (db.set_cancel("orhdr", "ord_code='" + code + "'"))
                        {
                            try
                            {
                                dt = get_soitemlist(code);

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    itemcode = dt.Rows[i]["item_code"].ToString();
                                    itemqty = dt.Rows[i]["ord_qty"].ToString();

                                    db.upd_item_quantity_onhand(itemcode, gm.toNormalDoubleFormat(itemqty), stk_trns_type);
                                }
                            }
                            catch (Exception) { }

                            db.DeleteOnTable("orlne", "ord_code='" + code + "'");
                            db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");
                        }

                        disp_list();
                    }
                }
            }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            add_item("");
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            upd_item();
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            if (tbpg_items.SelectedIndex == 1)
            {
                 try
                {
                    if (dgv_payment.Rows.Count == 0)
                    {
                        MessageBox.Show("No payment item selected.");
                    }
                    else
                    {
                        int i;
                        DialogResult dialogResult = MessageBox.Show("Confirm", "Are you sure you want to void this payment line?", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            i = dgv_payment.CurrentRow.Index;

                            dgv_payment.Rows.RemoveAt(i);
                            total_amountdue();
                        }
                    }
                }
                catch (Exception) { MessageBox.Show("No item selected."); }
            }
            else{
                del_item();
            }
        }

        private void btn_saveorder_Click(object sender, EventArgs e)
        {
            mainsave(false, "", true, "");
        }

        //payment or order
        public void mainsave(Boolean isPayment, String pay_code, Boolean pending_status, String amt_tendered)
        {
            Boolean success = false;
            String notificationText = "";
            z_Notification notify = new z_Notification();
            String ord_code, out_code, debt_code, customer, ord_date, ord_qty, ord_amnt, disc_amnt, reference, mcardid, tax_amnt, amnt_due, user_id, t_date, t_time, loc, user_id2, t_date2, t_time2, trnx_date, disc_code, rep_code = "", agentid, amount_paid;
            String branch = GlobalClass.branch, transferred="", stk_ref = "", stk_po_so = "", market_segment_id = "";
            String status, remark, loaner, vehicle, terms, dp_pct, condno, pn_amnt, amnt_financed, reg_chg, doc_stamp, add_on_rate, processing_fee, monthly_amort, max_sc, paid_amnt, dp_payment, dp_reference, dpamnt,rorder_status;
            String cashier = "";
            String pending = "Y";
            String col = "", val = "", col2 = "", val2 = "", col3="",val3="";
            String notifyadd = null;
            String table = "orhdr";
            String tableln = "orlne";

            if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the customer field.");
                m_auto_customer frm = new m_auto_customer(this, true);
                frm.ShowDialog();
            }
            else if (cbo_carunit.SelectedIndex == -1 && String.IsNullOrEmpty(cbo_carunit.Text))
            {
                MessageBox.Show("Please select the vehicle field.");
                z_ItemSearch frm = new z_ItemSearch(this, 1);
                frm.Show();
            }
            else if (String.IsNullOrEmpty(txt_vin.Text))
            {
                MessageBox.Show("Please select the vin no field.");
            }
            else if (String.IsNullOrEmpty(txt_engine.Text))
            {
                MessageBox.Show("Please select the engine no field.");
            }
            else if (String.IsNullOrEmpty(txt_condno.Text))
            {
                MessageBox.Show("Please select the conduction no field.");
            }
            else if (cbo_status.SelectedIndex == -1 && String.IsNullOrEmpty(cbo_status.Text) && !String.IsNullOrEmpty(txt_appcode.Text))
            {
                MessageBox.Show("Please select credit decision field.");
                cbo_status.DroppedDown = true;
            }
            else if (cbo_marketsegment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select market segment field.");
                cbo_marketsegment.DroppedDown = true;
            }
            else if (cbo_agent.SelectedIndex == -1)
            {
                MessageBox.Show("Please select agent field.");
                cbo_agent.DroppedDown = true;
            }
            /*else if (isCashier == true && cbo_cashier.SelectedIndex == -1)
            {
                MessageBox.Show("Please select cashier field.");
                cbo_cashier.DroppedDown = true;
            }*/
            else if (cbo_dp_pct.SelectedIndex == -1)
            {
                MessageBox.Show("Please select DP % field.");
                cbo_dp_pct.DroppedDown = true;
            }
            else if (cbo_rorder_status.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Repair Order Status");
                cbo_rorder_status.DroppedDown = true;
            }
            /*else if (dgv_itemlist.Rows.Count < 1)
            {
                 MessageBox.Show("No entry of Sales Item(s). Please add item(s).");
            }*/
            else
            {

                if (pending_status == false)
                {
                    pending = "N";
                }
                
                
                ord_code = txt_ordcode.Text;
                out_code = cbo_outlet.SelectedValue.ToString();
                debt_code = cbo_customer.SelectedValue.ToString();
                customer = cbo_customer.Text;
                mcardid = txt_servex.Text;
                loc = cbo_whsname.SelectedValue.ToString();

                agentid = cbo_agent.SelectedValue.ToString();
                
                stk_ref = stk_trns_type + "#" + ord_code;
                stk_po_so = ord_code;
                //txt_reference.Text = stk_ref;
                //reference = txt_reference.Text;
                //*ord_amnt = gm.toNormalDoubleFormat(txt_srpprice.Text).ToString("0.00");
                //*disc_amnt = gm.toNormalDoubleFormat(txt_vat.Text).ToString("0.00");
                //*amnt_tendered = gm.toNormalDoubleFormat(txt_csh_amttendered.Text).ToString("0.00");

                user_id = GlobalClass.username;
                t_date = db.get_systemdate("yyyy-MM-dd");
                t_time = db.get_systemtime();
                market_segment_id = cbo_marketsegment.SelectedValue.ToString();
                //amount_paid = gm.toNormalDoubleFormat(amt_tendered).ToString("0.00");

                trnx_date = dtp_trnx_date.Value.ToString("yyyy-MM-dd");
                ord_date = trnx_date;

                String insurance = (cbo_insurance.SelectedValue??"").ToString();

                loaner = cbo_loaner.Text;
                String loaner_id = txt_loaner.Text;
                status = cbo_status.Text;

                rorder_status = cbo_rorder_status.SelectedValue.ToString();
                dp_reference = txt_salescontrol.Text;
                remark = rtxt_remark.Text;

                
                String vehicle_desc = cbo_carunit.Text;
                String item_code = cbo_itemcode.Text;

                String engine_no = txt_engine.Text;
                String vin_no = txt_vin.Text;
                String cond_no = txt_condno.Text;

                reg_chg = gm.toNormalDoubleFormat(txt_srpprice.Text).ToString("0.00");
                String dp = cbo_dp_pct.SelectedValue.ToString();
                String dp_amt = gm.toNormalDoubleFormat(txt_dpamnt.Text).ToString("0.00");
                terms = cbo_terms.Text;
                String add_rate = gm.toNormalDoubleFormat(txt_add_on_rate.Text).ToString("0.00");
                String monthly_amor = gm.toNormalDoubleFormat(txt_monthly_amort.Text).ToString("0.00");
                String pn_amt = gm.toNormalDoubleFormat(txt_clientDP.Text).ToString("0.00");

                String totaldiscount = gm.toNormalDoubleFormat(txt_totaldiscount.Text).ToString("0.00");
                String totalpayment = gm.toNormalDoubleFormat(txt_totalpayment.Text).ToString("0.00");
                String totalitemamnt = gm.toNormalDoubleFormat(txt_totalitemamnt.Text).ToString("0.00");

                tax_amnt = txt_vat_exemp.Text;

                //ord_amnt = gm.toNormalDoubleFormat(totaldiscount) - gm.toNormalDoubleFormat(totaldiscount);

                //amnt_due = gm.toNormalDoubleFormat(totalpayment)
                //String tax_amnt = 
                //tax_amnt
                String amt_finance = gm.toNormalDoubleFormat(txt_amnt_financed.Text).ToString("0.00");
                String credit_advice = dtp_credit_advice.Value.ToString("yyyy-MM-dd");

                String vat_table = gm.toNormalDoubleFormat(txt_vat_table.Text).ToString("0.00");
                String vat_exemp = gm.toNormalDoubleFormat(txt_vat_exemp.Text).ToString("0.00");
                String zero_rated = gm.toNormalDoubleFormat(txt_zero_rated.Text).ToString("0.00");
                String vat_amnt = gm.toNormalDoubleFormat(txt_vat_amnt.Text).ToString("0.00");
                String vat_less = gm.toNormalDoubleFormat(txt_vat_less.Text).ToString("0.00");
                String srp_netvat = gm.toNormalDoubleFormat(txt_srp_netvat.Text).ToString("0.00");

                String total_amnt = (gm.toNormalDoubleFormat(srp_netvat) - gm.toNormalDoubleFormat(totaldiscount)).ToString("0.00");
                String net_amnt = (gm.toNormalDoubleFormat(total_amnt) / (((db.get_outlet_govt_pct(out_code)) / 100) + 1)).ToString("0.00");
                tax_amnt = (gm.toNormalDoubleFormat(total_amnt) - gm.toNormalDoubleFormat(net_amnt)).ToString("0.00");
                amnt_due = (gm.toNormalDoubleFormat(total_amnt) - (gm.toNormalDoubleFormat(totalpayment) * -1)).ToString("0.00");


                String app_no = txt_appcode.Text;
                //db.get_outlet_govt_pct(out_code)
                //amnt_due = gm.toNormalDoubleFormat(txt_srpprice.Text).ToString("0.00");
                //tax_amnt = (gm.toNormalDoubleFormat(amnt_due) * db.get_outlet_govt_pct(out_code) / 100).ToString("0.00");

                if (cbo_clerk.SelectedIndex != -1)
                {
                    rep_code = cbo_clerk.SelectedValue.ToString();
                }

                if (cbo_cashier.SelectedIndex != -1)
                {
                    cashier = cbo_cashier.SelectedValue.ToString();
                }

                user_id2 = GlobalClass.username;
                t_date2 = db.get_systemdate("yyyy-MM-dd");
                t_time2 = db.get_systemtime();
                
               //col2 = ", user_id2, t_date2, t_time2, tax_amnt, amnt_due, pay_code, cashier, amnt_tendered";
                //val2 = ", '" + user_id2 + "', '" + t_date2 + "', '" + t_time2 + "', '" + tax_amnt + "', '" + amnt_due + "', '" + pay_code + "', '" + cashier + "', '" + amount_paid + "'";
                

                //col3 = ",status,remark, loaner, vehicle, terms, dp_pct, condno, pn_amnt, amnt_financed, reg_chg, doc_stamp, add_on_rate, processing_fee, monthly_amort, max_sc, paid_amnt, dp_payment, dp_reference, dpamnt,rorder_status";


                //col = "ord_code, out_code, debt_code, customer, ord_date, ord_amnt, trnx_date, disc_amnt, reference, mcardid, user_id, t_date, t_time, loc,rep_code, agentid, branch,market_segment_id, pending" + col2 + col3;


                /*val3 = ",'" + status + "'," + remark + ",'" + loaner + "','" + vehicle + "','" + terms + "','" + dp_pct + "','" + condno + "','" + pn_amnt + "','" + amnt_financed + "','" + reg_chg + "','" + doc_stamp + "','" + add_on_rate + "','" + processing_fee + "','" + monthly_amort + "','" + max_sc + "','" + paid_amnt + "','" + dp_payment + "','" + dp_reference + "','" + dpamnt + "','" + rorder_status + "'";*/

                if (isnew == false)
                {
                    //col = ", tax_amnt='" + tax_amnt + "', amnt_due='" + amnt_due + "', user_id2='" + user_id2 + "', t_date2='" + t_date2 + "', t_time2='" + t_time2 + "', pay_code='" + pay_code + "', cashier='" + cashier + "', amnt_tendered='" + amount_paid + "'";
                }
                
                if (isnew)
                {

                    if (rorder_status == rocode_released)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to Release this Unit ?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            rorder_status = rocode_released;
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                    }
                    else
                    {
                        success = true;
                    }

                    if (success) {

                        notificationText = "has added: ";
                        stk_ref = stk_trns_type + "#" + ord_code;
                        stk_po_so = ord_code;
                        /*txt_reference.Text = stk_ref;
                        reference = txt_reference.Text;*/
                        ord_code = db.get_ord_code(out_code);

                        col = "app_no,ord_code, out_code, debt_code, customer, mcardid, loc, agentid, ord_amnt, user_id, t_date, t_time, market_segment_id, trnx_date, ord_date, loaner,loaner_id,insurance, status, rorder_status, dp_reference, remark, car_vin_desc, car_item_code, car_engine, car_vin_num, car_plate, reg_chg, dp_pct, dpamnt, terms, add_on_rate, monthly_amort, pn_amnt, disc_amnt, payment, t_itemamnt, amnt_financed, credit_advice, vat_table, vat_exemp, zero_rated, vat_amnt, vat_less, srp_netvat, cashier, rep_code, user_id2, t_date2, t_time2,tax_amnt,net_amnt,total_amnt,amnt_due";

                        val = "'" + app_no + "','" + ord_code + "', '" + out_code + "', '" + debt_code + "', " + db.str_E(customer) + ", '" + mcardid + "', '" + loc + "', '" + agentid + "', '" + reg_chg + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + market_segment_id + "', '" + trnx_date + "', '" + ord_date + "', " + db.str_E(loaner) + ",'" + loaner_id + "','" + insurance + "', '" + status + "', '" + rorder_status + "', " + db.str_E(dp_reference) + ", " + db.str_E(remark) + ", " + db.str_E(vehicle_desc) + ", '" + item_code + "', '" + engine_no + "', '" + vin_no + "', '" + cond_no + "', '" + reg_chg + "', '" + dp + "', '" + dp_amt + "', '" + terms + "', '" + add_rate + "', '" + monthly_amor + "', '" + pn_amt + "', '" + totaldiscount + "', '" + totalpayment + "', '" + totalitemamnt + "', '" + amt_finance + "', '" + credit_advice + "', '" + vat_table + "', '" + vat_exemp + "', '" + zero_rated + "', '" + vat_amnt + "', '" + vat_less + "', '" + srp_netvat + "', '" + cashier + "', '" + rep_code + "', '" + user_id2 + "', '" + t_date2 + "', '" + t_time2 + "','" + tax_amnt + "','" + net_amnt + "','" + total_amnt + "','" + amnt_due + "'";

                        //col = "ord_code, out_code, debt_code, customer, ord_date, ord_amnt, trnx_date, disc_amnt, reference, mcardid, user_id, t_date, t_time, loc,rep_code, agentid, branch,market_segment_id, pending" + col2 + col3;

                        //val = "'" + ord_code + "', '" + out_code + "', '" + debt_code + "', " + db.str_E(customer) + ", '" + ord_date + "', '" + ord_amnt + "', '" + trnx_date + "', '" + ""/*disc_amnt*/ + "', " + db.str_E(/*reference*/"") + ", '" + mcardid + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + loc + "', '" + rep_code + "', '" + agentid + "', '" + branch + "','" + market_segment_id + "', '" + pending + "'" + val2 + val3;

                        if (db.InsertOnTable(table, col, val))
                        {
                            try
                            {
                                stk_ref = stk_trns_type + "#" + ord_code;
                                notifyadd = add_items(tableln, ord_code, trnx_date, stk_ref, stk_po_so, loc);
                                add_payment_items(ord_code);
                                //if (String.IsNullOrEmpty(notifyadd) == false)
                                //{
                                notificationText += String.IsNullOrEmpty(notifyadd) ? "" : notifyadd;
                                notificationText += Environment.NewLine + " with SO#" + ord_code;
                                notify.saveNotification(notificationText, "Sales Outlet");
                                db.set_ord_code_nextno(ord_code, out_code);
                                success = true;
                                //}
                                //else
                                //{
                                //    success = false;
                                //}
                            }
                            catch (Exception er){
                                success = false;
                                MessageBox.Show(er.ToString());
                            }
                        }
                        if (success == false)
                        {
                            MessageBox.Show("Failed on saving. on New");
                        }
                    }
                }
                else
                {

                    if (isRepair)
                    { 
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to Release this Unit ?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            rorder_status = rocode_released;
                            success = true;
                        }
                        else {
                            success = false;
                        }
                    }
                    else {
                        success = true;
                    }

                    if (success) {

                        notificationText = "has updated: ";
                        /*
                        col = "ord_code='" + ord_code + "', out_code='" + out_code + "', debt_code='" + debt_code + "', customer='" + customer + "', ord_date='" + ord_date + "', ord_amnt='" + ord_amnt + "', disc_amnt='" + disc_amnt + "', reference='" + reference + "', mcardid='" + mcardid + "', user_id='" + user_id + "', t_date='" + t_date + "', t_time='" + t_time + "', loc='" + loc + "', trnx_date='" + trnx_date + "', pay_code='" + pay_code + "', cashier='" + cashier + "', rep_code='" + rep_code + "', agentid='" + agentid + "', branch='" + branch + "', pending='" + pending + "' ,payment='" + amount_paid + "',loaner='" + loaner + "', vehicle='" + vehicle + "', terms='" + terms + "', dp_pct='" + dp_pct + "', condno='" + condno + "', pn_amnt='" + pn_amnt + "', amnt_financed='" + amnt_financed + "', reg_chg='" + reg_chg + "', doc_stamp='" + doc_stamp + "', add_on_rate='" + add_on_rate + "', processing_fee='" + processing_fee + "', monthly_amort='" + monthly_amort + "', max_sc='" + max_sc + "', paid_amnt='" + paid_amnt + "', dp_payment='" + dp_payment + "', dp_reference='" + dp_reference + "', dpamnt='" + dpamnt + "',remark=" + remark + ",status='" + status + "',rorder_status='" + rorder_status + "'";
                        */

                        col = "app_no='" + app_no + "', out_code='" + out_code + "', debt_code='" + debt_code + "', customer=" + db.str_E(customer) + ", mcardid='" + mcardid + "', loc='" + loc + "', agentid='" + agentid + "', ord_amnt='" + reg_chg + "', user_id='" + user_id + "', t_date='" + t_date + "', t_time='" + t_time + "', market_segment_id='" + market_segment_id + "', trnx_date='" + trnx_date + "', ord_date='" + ord_date + "', loaner=" + db.str_E(loaner) + ",loaner_id='" + loaner_id + "',insurance='" + insurance + "', status='" + status + "', rorder_status='" + rorder_status + "', dp_reference=" + db.str_E(dp_reference) + ", remark=" + db.str_E(remark) + ", car_vin_desc=" + db.str_E(vehicle_desc) + ", car_item_code='" + item_code + "', car_engine='" + engine_no + "', car_vin_num='" + vin_no + "', car_plate='" + cond_no + "', reg_chg='" + reg_chg + "', dp_pct='" + dp + "', dpamnt='" + dp_amt + "', terms='" + terms + "', add_on_rate='" + add_rate + "', monthly_amort='" + monthly_amor + "', pn_amnt='" + pn_amt + "', disc_amnt='" + totaldiscount + "', payment='" + totalpayment + "', t_itemamnt='" + totalitemamnt + "', amnt_financed='" + amt_finance + "', credit_advice='" + credit_advice + "', vat_table='" + vat_table + "', vat_exemp='" + vat_exemp + "', zero_rated='" + zero_rated + "', vat_amnt='" + vat_amnt + "', vat_less='" + vat_less + "', srp_netvat='" + srp_netvat + "', cashier='" + cashier + "', rep_code='" + rep_code + "', user_id2='" + user_id2 + "', t_date2='" + t_date2 + "', t_time2='" + t_time2 + "',tax_amnt='" + tax_amnt + "',net_amnt='" + net_amnt + "',total_amnt='" + total_amnt + "',amnt_due='" + amnt_due + "'";

                        try
                        {
                            if (db.UpdateOnTable(table, col, "ord_code='" + ord_code + "'"))
                            {
                                db.DeleteOnTable(tableln, "ord_code='" + ord_code + "'");
                                db.DeleteOnTable("stkcrd", "po_so='" + ord_code + "' AND trn_type='" + stk_trns_type + "'");
                                try
                                {

                                    stk_ref = stk_trns_type + "#" + ord_code;
                                    notifyadd = add_items(tableln, ord_code, trnx_date, stk_ref, stk_po_so, loc);
                                    add_payment_items(ord_code);
                                    //MessageBox.Show(notifyadd.ToString());
                                    //if (String.IsNullOrEmpty(notifyadd) == false) 
                                    //{
                                    notificationText += String.IsNullOrEmpty(notifyadd) ? "" : notifyadd;
                                    notificationText += Environment.NewLine + " with SO#" + ord_code;
                                    notify.saveNotification(notificationText, "Sales Outlet");
                                    success = true;
                                    //}
                                    //else
                                    //{
                                    //    success = false;
                                    //}
                                }
                                catch (Exception er){
                                    success = false;
                                    MessageBox.Show(er.ToString());
                                }
                            }

                        }catch (Exception er){
                            success = false;
                        }
                        if (success == false)
                        {
                            MessageBox.Show("Failed on saving. on update");
                        }
                    }
                }
                if (success)
                {
                    if (isPayment)
                    {
                        //frm_payment.Close();
                    }
                    disp_list();
                    goto_win1();
                    if (!isRepair) {
                        frm_reload();
                    }
                }
            }
        }
        public void disp_loaner(String code)
        {
            DataTable itemdt3 = db.QueryBySQLCode("SELECT app_no,cust_no,cust_name,status,line FROM rssys.autoloanfinancer WHERE m06_finance_code='" + code + "'");

            for (int i = 0; i < itemdt3.Rows.Count; i++)
            {
                /*dgv_loaner_list.Rows.Add();
                dgv_loaner_list["loaner_line", i].Value = itemdt3.Rows[i]["line"].ToString();
                dgv_loaner_list["loaner_no", i].Value = itemdt3.Rows[i]["cust_no"].ToString();
                dgv_loaner_list["loaner_name", i].Value = itemdt3.Rows[i]["cust_name"].ToString();
                dgv_loaner_list["loaner_status", i].Value = itemdt3.Rows[i]["status"].ToString();*/
            }
        
        
        }
        private String add_items(String tableln, String ord_code, String dt_trnx, String stk_ref, String stk_po_so, String loc)
        {
            String notificationText = null;
            String ln_num, ord_line, item_code, item_desc, unit, unitid, ord_qty, price, ln_amnt, ln_tax, rep_code, reference, trnx_date, t_time, fcp = "0.00", part_no, disc_code, disc_amt, disc_reason, disc_user;
            String val2 = "";
            String col2 = "ord_code, ln_num, item_code, item_desc, unit, ord_qty, price, ln_amnt, ln_tax, rep_code, reference, trnx_date, t_time, fcp, part_no, disc_code, disc_amt, disc_reason, disc_user, unitid";
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";

            try
            {
                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    ln_num = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                    item_code = dgv_itemlist["dgvli_itemcode", r].Value.ToString();
                    item_desc = dgv_itemlist["dgvli_itemdesc", r].Value.ToString();
                    ord_qty = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_qty", r].Value.ToString()).ToString("0.00");
                    unit = dgv_itemlist["dgvli_unit", r].Value.ToString();
                    unitid = dgv_itemlist["dgvli_unitid", r].Value.ToString();
                    price = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_sellprice", r].Value.ToString()).ToString("0.00");
                    ln_amnt = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", r].Value.ToString()).ToString("0.00");
                    ln_tax = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_taxamt", r].Value.ToString()).ToString("0.00");
                    //reference = txt_reference.Text;
                    trnx_date = dt_trnx;
                    t_time = db.get_systemtime();
                    fcp = db.get_item_fcp(item_code).ToString("0.00");
                    part_no = dgv_itemlist["dgvi_part_no", r].Value.ToString();
                    disc_code = dgv_itemlist["dgvli_disccode", r].Value.ToString();
                    disc_amt = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_discamt", r].Value.ToString()).ToString("0.00");
                    disc_reason = dgv_itemlist["dgvli_discreason", r].Value.ToString();
                    disc_user = dgv_itemlist["dgvli_discuser", r].Value.ToString();

                    /*if (cbo_clerk.SelectedIndex != -1)
                    {
                        rep_code = cbo_clerk.SelectedValue.ToString();
                    }
                    else
                    {
                        rep_code = "";
                    }*/

                    val2 = "'" + ord_code + "', '" + ln_num + "', '" + item_code + "', " + db.str_E(item_desc) + ", '" + unit + "', '" + ord_qty + "', '" + price + "', '" + ln_amnt + "', '" + ln_tax + "', '" + /*rep_code*/"" + "', '" + /*reference*/"" +"', '" + trnx_date + "', '" + t_time + "', '" + fcp + "', " + db.str_E(part_no) + ", '" + disc_code + "', '" + disc_amt + "', '" + disc_reason + "', '" + disc_user + "','" + unitid + "'";

                    if (db.InsertOnTable(tableln, col2, val2))
                    {
                        stk_qty_in = "0.00";
                        stk_qty_out = "0.00";

                        if (gm.toNormalDoubleFormat(ord_qty) < 0)
                            stk_qty_in = Math.Abs(gm.toNormalDoubleFormat(ord_qty)).ToString();
                        else
                            stk_qty_out = ord_qty;

                        db.save_to_stkcard(item_code, item_desc, unit, dt_trnx, stk_ref, stk_po_so, stk_qty_in, stk_qty_out, fcp, price, loc, "", "", stk_trns_type, "", "", "");

                        db.upd_item_quantity_onhand(item_code, Convert.ToDouble(ord_qty), stk_trns_type);
                        notificationText += Environment.NewLine + ord_qty + " - " + item_desc;
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

        public void add_payment_items(String code)
        {

            String notificationText = null;
            String ln_num, ln_amnt, rep_code, reference, paycode, ln_type, ordcode, chargeto;
            String val2 = "";
            String col2 = "ord_code, ln_num, ln_amnt, pay_code, rep_code, reference, ln_type,chargeto";
            DataTable dt;
            try
            {
                db.DeleteOnTable("orlne", "ln_type='P' AND ord_code='" + code + "'");
                for (int r = 0; r < dgv_payment.Rows.Count; r++)
                {
                    ordcode = code;
                    ln_num = dgv_payment["dgvlp_ln_num", r].Value.ToString();
                    ln_amnt = gm.toNormalDoubleFormat(dgv_payment["dgvlp_ln_amnt", r].Value.ToString()).ToString("0.00");
                    rep_code = dgv_payment["dgvlp_rep_code", r].Value.ToString();
                    reference = dgv_payment["dgvlp_reference", r].Value.ToString();
                    paycode = dgv_payment["dgvlp_pay_code", r].Value.ToString();
                    ln_type = dgv_payment["dgvlp_ln_type", r].Value.ToString();
                    chargeto = dgv_payment["dgvlp_chargeto", r].Value.ToString();


                    dt = db.QueryBySQLCode("SELECT * FROM rssys.orlne WHERE ord_code='" + ordcode + "' AND ln_num='" + ln_num + "'");

                    val2 = "'" + ordcode + "', '" + ln_num + "', '" + ln_amnt + "', '" + paycode + "', '" + rep_code + "', '" + reference + "','" + ln_type + "','" + chargeto + "'";

                    try
                    {
                        if (db.InsertOnTable("orlne", col2, val2))
                        {

                            // MessageBox.Show("Payment Saved");
                        }
                        else
                        {
                            MessageBox.Show("Payment Not Saved"); ;
                        }
                    }
                    catch (Exception er) { MessageBox.Show(er.Message); }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            frm_reload();
            goto_win1();
            disp_list();
            isverified = false;
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

        private void add_item(String typ)
        {
            frm_si = new z_enter_sales_item(this, true, "", typ, lnno_last);
            frm_si.ShowDialog();
        }

        public void upd_item()
        {
            int r = 0;

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {

                    r = dgv_itemlist.CurrentRow.Index;

                    frm_si = new z_enter_sales_item(this, false, "", "", r);

                    frm_si.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void del_item()
        {
            try
            {
                if (dgv_itemlist.Rows.Count < 1)
                {
                    MessageBox.Show("No item selected.");
                }
                else
                {
                    int i;
                    DialogResult dialogResult = MessageBox.Show("Confirm", "Are you sure you want to remove this order?", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        i = dgv_itemlist.CurrentRow.Index;
                        dgv_itemlist.Rows.RemoveAt(i);
                        total_itemamount();
                    }
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void btn_customer_refresh_Click(object sender, EventArgs e)
        {
            //m_customers frm = new m_customers(this, true);
            //frm.ShowDialog();
            m_auto_customer frm = new m_auto_customer(this, true);
            frm.ShowDialog(); 
        }

        private void btn_vehicle_Click(object sender, EventArgs e)
        {
            //**m_vehiclec_info frm = new m_vehiclec_info(this, true);
            //**frm.Show();
            z_ItemSearch frm = new z_ItemSearch(this, 1);
            frm.Show();
        }
        public void enter_item(String item)
        {
            DataTable dt = db.get_item_details(item.Trim());

            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > 0; i++)
                    {
                        cbo_itemcode.Text = dt.Rows[i]["item_code"].ToString();
                        cbo_carunit.Text = dt.Rows[i]["item_desc"].ToString();
                        txt_srpprice.Text = gm.toAccountingFormat(dt.Rows[i]["sell_pric"].ToString());
                        txt_condno.Text = dt.Rows[i]["part_no"].ToString();
                        cbo_terms.Text = "60";
                        compute_monthlyAmort();
                    }
                }
            }
            catch (Exception) { }
        }

        private void cbo_terms_TextChanged(object sender, EventArgs e)
        {
            compute_monthlyAmort();
        }
        private void cbo_dp_pct_SelectedIndexChanged(object sender, EventArgs e)
        {
            compute_monthlyAmort();
        }
        private void txt_clientDP_TextChanged(object sender, EventArgs e)
        {
            compute_monthlyAmort();
        }

        public void compute_monthlyAmort()
        {
            Double INTperc = 10.0;

            Double termM = 60;
            try { termM = gm.toNormalDoubleFormat(cbo_terms.Text); }
            catch { }

            Double DPperc = 0;
            try { DPperc = gm.toNormalDoubleFormat(cbo_dp_pct.Text.Substring(0, 2)); }
            catch { }

            Double clientDP = 0;
            try { clientDP = gm.toNormalDoubleFormat(txt_clientDP.Text); }
            catch { }

            Double interest = ((INTperc / 100.0) * (termM / 12.0)) + 1;

            Double srpprice = gm.toNormalDoubleFormat(txt_srpprice.Text);

            txt_dpamnt.Text = gm.toAccountingFormat((DPperc / 100.0) * srpprice);

            txt_monthly_amort.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_dpamnt.Text) * interest) / termM);

            txt_totaldiscount.Text = gm.toAccountingFormat(gm.toNormalDoubleFormat(txt_dpamnt.Text) - clientDP);

            compute_srp_netvat();
        }

        private void txt_compute_srp_netvat_TextChanged(object sender, EventArgs e)
        {
            compute_monthlyAmort();
        }
        public void compute_srp_netvat()
        {
            Double srp = 0.00;
            try { srp = gm.toNormalDoubleFormat(txt_srpprice.Text) - gm.toNormalDoubleFormat(txt_totaldiscount.Text); }
            catch { }
            txt_vat_table.Text = gm.toAccountingFormat(srp);
            Double tax = 12;
            try { tax = db.get_outlet_govt_pct(cbo_outlet.SelectedValue.ToString()); }
            catch { }


            txt_vat_exemp.Text = gm.toAccountingFormat((srp * ((tax / 100.0))).ToString("0.00"));

            Double less = gm.toNormalDoubleFormat(txt_vat_exemp.Text) + gm.toNormalDoubleFormat(txt_zero_rated.Text) + gm.toNormalDoubleFormat(txt_vat_amnt.Text) + gm.toNormalDoubleFormat(txt_vat_less.Text);

            txt_srp_netvat.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_vat_table.Text) - less));

        }
        
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




        public void verifiedClerk(String cid, String cname)
        {
            DataTable dt;
            String outcode = "";
            isverified = true;

            if (isCashier == true && isnew == true)
            {
                //cbo_clerk.SelectedValue = cid;
                cbo_cashier.SelectedValue = cid;
            }
            else if (isCashier == true && isnew == false)
            {
                cbo_cashier.SelectedValue = cid;
            }
            else
            {
                cbo_clerk.SelectedValue = cid;
            }

            if (isnew)
            {
                try { outcode = cbo_outlet_olist.SelectedValue.ToString(); }
                catch { }

                if (isverified)
                {
                    cbo_rorder_status.SelectedIndex = 0;
                    if (direct_sales)
                    {
                        m_auto_customer frm = new m_auto_customer(this, true);
                        frm.ShowDialog();
                        gb_finance.Visible = false;
                        goto_win2();
                    }
                    else if (loan_application)
                    {
                        isverified = false;
                        s_Auto_Sales_Loan_Status frm = new s_Auto_Sales_Loan_Status(this);
                        frm.ShowDialog(); 
                    }
                }
            }
            else if (isnew == false)
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["dgvl_code", r].Value.ToString();

                dt = db.QueryBySQLCode("SELECT o.* FROM rssys.orhdr o WHERE ord_code ='" + code + "'");
                txt_ordcode.Text = code;

                if (dt.Rows.Count > 0) 
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //txt_reference.Text = dt.Rows[i]["reference"].ToString();
                        txt_appcode.Text = dt.Rows[i]["app_no"].ToString();

                        if (!String.IsNullOrEmpty(txt_appcode.Text)){
                            gb_finance.Visible = true;
                        }
                        else{
                            gb_finance.Visible = false;
                        }



                        cbo_customer.Text = dt.Rows[i]["customer"].ToString();
                        txt_servex.Text = dt.Rows[i]["mcardid"].ToString();
                        dtp_trnx_date.Value = gm.toDateValue(dt.Rows[i]["trnx_date"].ToString());

                        txt_srpprice.Text = dt.Rows[i]["ord_amnt"].ToString();
                        //txt_vat.Text = dt.Rows[i]["disc_amnt"].ToString();
                        //txt_total_dp.Text = dt.Rows[i]["payment"].ToString();

                        dtp_appdate.Value = gm.toDateValue(dt.Rows[i]["ord_date"].ToString());
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                        //cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();

                        //update

                        cbo_rorder_status.SelectedValue = dt.Rows[i]["rorder_status"].ToString();

                        cbo_insurance.SelectedValue = dt.Rows[i]["insurance"].ToString();//
                        cbo_loaner.Text = dt.Rows[i]["loaner"].ToString();//
                        txt_loaner.Text = dt.Rows[i]["loaner_id"].ToString();//
                        cbo_status.Text = dt.Rows[i]["status"].ToString();
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();

                        if (isCashier == true)
                        {
                            cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                        }
                        else {
                            cbo_cashier.SelectedValue = dt.Rows[i]["cashier"].ToString();
                        }

                        //

                        //if (!String.IsNullOrEmpty(dt.Rows[i]["cashier"].ToString()))
                        //{
                            //
                        //}

                        rtxt_remark.Text = dt.Rows[i]["remark"].ToString();

                        cbo_itemcode.Text = dt.Rows[i]["car_item_code"].ToString();
                        cbo_carunit.Text = dt.Rows[i]["car_vin_desc"].ToString();
                        txt_vin.Text = dt.Rows[i]["car_vin_num"].ToString();
                        txt_engine.Text = dt.Rows[i]["car_engine"].ToString();
                        txt_condno.Text = dt.Rows[i]["car_plate"].ToString();


                        txt_srpprice.Text = gm.toAccountingFormat(dt.Rows[i]["reg_chg"].ToString());
                        txt_dpamnt.Text = gm.toAccountingFormat(dt.Rows[i]["dpamnt"].ToString());
                        txt_amnt_financed.Text = gm.toAccountingFormat(dt.Rows[i]["amnt_financed"].ToString());
                        cbo_terms.Text = dt.Rows[i]["terms"].ToString();
                        txt_add_on_rate.Text = gm.toAccountingFormat(dt.Rows[i]["add_on_rate"].ToString());
                        txt_clientDP.Text = gm.toAccountingFormat(dt.Rows[i]["pn_amnt"].ToString());
                        txt_totaldiscount.Text = gm.toAccountingFormat(dt.Rows[i]["disc_amnt"].ToString());
                        txt_monthly_amort.Text = gm.toAccountingFormat(dt.Rows[i]["monthly_amort"].ToString());
                        txt_totalpayment.Text = gm.toAccountingFormat(dt.Rows[i]["payment"].ToString());
                        txt_totalitemamnt.Text = gm.toAccountingFormat(dt.Rows[i]["t_itemamnt"].ToString());

                        txt_vat_table.Text = gm.toAccountingFormat(dt.Rows[i]["vat_table"].ToString());
                        txt_vat_exemp.Text = gm.toAccountingFormat(dt.Rows[i]["vat_exemp"].ToString());
                        txt_zero_rated.Text = gm.toAccountingFormat(dt.Rows[i]["zero_rated"].ToString());
                        txt_vat_amnt.Text = gm.toAccountingFormat(dt.Rows[i]["vat_amnt"].ToString());
                        txt_vat_less.Text = gm.toAccountingFormat(dt.Rows[i]["vat_less"].ToString());
                        txt_srp_netvat.Text = gm.toAccountingFormat(dt.Rows[i]["srp_netvat"].ToString());

                    }
                }
                vehicle_info_entry();
                disp_itemlist(code);
                goto_win2();
            }

            //frm_enable(true);

        }

        public void dgv_salesitem(DataTable dt, Boolean isnewitem)
        {
            int i = 0;

            ////try
            // {
            if (isnewitem)
            {
                i = dgv_itemlist.Rows.Add();
            }
            else
            {
                i = dgv_itemlist.CurrentRow.Index;
            }

            dgv_itemlist["dgvli_lnno", i].Value = dt.Rows[0]["dgvli_lnno"].ToString();
            dgv_itemlist["dgvli_qty", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_qty"].ToString());

            dgv_itemlist["dgvli_unit", i].Value = dt.Rows[0]["dgvli_unit"].ToString();
            dgv_itemlist["dgvli_unitid", i].Value = dt.Rows[0]["dgvli_unitid"].ToString();

            dgv_itemlist["dgvli_itemcode", i].Value = dt.Rows[0]["dgvli_itemcode"].ToString();
            dgv_itemlist["dgvi_part_no", i].Value = dt.Rows[0]["dgvi_part_no"].ToString();
            dgv_itemlist["dgvli_itemdesc", i].Value = dt.Rows[0]["dgvli_itemdesc"].ToString();

            dgv_itemlist["dgvli_regprice", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_regprice"].ToString());
            dgv_itemlist["dgvli_discamt", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_discamt"].ToString());
            dgv_itemlist["dgvli_taxamt", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_taxamt"].ToString()); //1
            dgv_itemlist["dgvli_sellprice", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_sellprice"].ToString());

            dgv_itemlist["dgvli_lnamt", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_lnamt"].ToString());
            dgv_itemlist["dgvli_remarks", i].Value = dt.Rows[0]["dgvli_remarks"].ToString();

            dgv_itemlist["dgvli_discuser", i].Value = dt.Rows[0]["dgvli_discuser"].ToString();
            dgv_itemlist["dgvli_discreason", i].Value = dt.Rows[0]["dgvli_discreason"].ToString();
            dgv_itemlist["dgvli_disccode", i].Value = dt.Rows[0]["dgvli_disccode"].ToString();

            dgv_itemlist["dgvli_t_date", i].Value = db.get_systemdate("");
            dgv_itemlist["dgvli_t_time", i].Value = db.get_systemtime();
            dgv_itemlist["dgvli_clerkid", i].Value = cbo_clerk.SelectedValue.ToString();
            dgv_itemlist["dgvli_clerk", i].Value = cbo_clerk.Text;

            if (isnew_item)
            {
                inc_lnno();
            }
            total_itemamount();
            //}
            //catch (Exception) { }
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
                    txt_email.Text = dt.Rows[0]["d_email"].ToString();

                }
            }
            catch { }
            //dgv_loaner_list.Rows.Clear();
            disp_loaner(cbo_customer.SelectedValue.ToString());
        }

        public void set_vehicle_frm(String custcode, String custname)
        {

            try { cbo_carunit.Items.Clear(); }
            catch { }

            gc.load_vehicle_info(cbo_carunit);
            //gc.load_customer(cbo_carunit);
            cbo_carunit.SelectedValue = custcode;
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
                dt = db.QueryBySQLCode("SELECT vin_no, engine_no FROM rssys.vehicle_info WHERE vin_no='" + cbo_carunit.SelectedValue.ToString() + "'");
            }
            catch { }
            //MessageBox.Show(dt.Rows.Count.ToString());
            try
            {
                if (dt.Rows.Count != 0)
                {
                    txt_vin.Text = dt.Rows[0]["vin_no"].ToString();
                    txt_engine.Text = dt.Rows[0]["engine_no"].ToString();
                }
            }
            catch { }
        }




        private void cbo_outlet_olist_SelectedIndexChanged(object sender, EventArgs e)
        {
            int r = 0;
            String outcode = "";
            String whs_code = "";

            try
            {
                if (cbo_outlet_olist.SelectedIndex != 1)
                {

                    outcode = cbo_outlet_olist.SelectedValue.ToString();
                    whs_code = get_outlet_whs(outcode);

                    if (String.IsNullOrEmpty(whs_code) == false)
                    {
                        cbo_whsname.SelectedValue = whs_code;
                    }

                    if (String.IsNullOrEmpty(outcode) == false)
                    {
                        cbo_outlet.SelectedValue = outcode;
                    }

                    disp_list();
                    goto_win1();
                }
            }
            catch { }
        }

        private String get_outlet_whs(String outcode)
        {
            DataTable dt = db.QueryBySQLCode("SELECT whs_code FROM rssys.outlet WHERE out_code='" + outcode + "'");
            String whschode = "";

            try
            {
                if (dt.Rows.Count > 0)
                {
                    whschode = dt.Rows[0][0].ToString();
                }
            }
            catch { }

            return whschode;
        }

        private void chk_pendingonly_CheckedChanged(object sender, EventArgs e)
        {
            disp_list();
            dgv_list_Cell_isClick(dgv_list.CurrentRow == null ? 0 : dgv_list.CurrentRow.Index);
        }

        private void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
            dgv_list_Cell_isClick(dgv_list.CurrentRow == null ? 0 : dgv_list.CurrentRow.Index);
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
            dgv_list_Cell_isClick(dgv_list.CurrentRow == null ? 0 : dgv_list.CurrentRow.Index);
        }
        private void AdjustColumnOrder()
        {
            dgv_list.AutoGenerateColumns = false;
            dgv_list.Columns["dgvl_code"].DisplayIndex = 0;//rorder_status
            dgv_list.Columns["dgvl_rorder_status"].DisplayIndex = 1;
            dgv_list.Columns["dgvl_customer"].DisplayIndex = 2;
            dgv_list.Columns["dgvl_ord_date"].DisplayIndex = 3;
            dgv_list.Columns["dgvl_trnx_date"].DisplayIndex = 4;
            dgv_list.Columns["dgvl_ord_amnt"].DisplayIndex = 5;
            dgv_list.Columns["dgvl_netamnt"].DisplayIndex = 6;
            dgv_list.Columns["dgvl_disc_amnt"].DisplayIndex = 7;
            dgv_list.Columns["dgvl_tax_amnt"].DisplayIndex = 8;
            dgv_list.Columns["dgvl_gross_amnt"].DisplayIndex = 9;
            dgv_list.Columns["dgvl_payment"].DisplayIndex = 10;
            dgv_list.Columns["dgvl_bal"].DisplayIndex = 11;
            dgv_list.Columns["dgvl_pending"].DisplayIndex = 12;
            dgv_list.Columns["dgvl_assistedby"].DisplayIndex = 13;
            dgv_list.Columns["dgvl_cashier"].DisplayIndex = 14;
            dgv_list.Columns["dgvl_jrnlz"].DisplayIndex = 15;
            dgv_list.Columns["dgvl_cancel"].DisplayIndex = 16;
            dgv_list.Columns["dgvl_canc_user"].DisplayIndex = 17;
            dgv_list.Columns["dgvl_canc_date"].DisplayIndex = 18;
            dgv_list.Columns["dgvl_canc_time"].DisplayIndex = 19;
            dgv_list.Columns["dgvl_out_code"].DisplayIndex = 20;

        }

        private void dgv_itemlist_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Menu list ");


        }
        private void sub_item_list()
        {
            int r = -1;
            String itemcode = "";
            String ord_code = "";
            String ln = "1";
            DataTable dt = null;

            if (dgv_itemlist.Rows.Count > 0)
            {
                try
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    ord_code = txt_ordcode.Text;
                    ln = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                    itemcode = dgv_itemlist["dgvli_itemcode", r].Value.ToString();

                    //s_sub_items frm_subitem = new s_sub_items(this, itemcode, ord_code, ln);

                    //frm_subitem.ShowDialog();

                    //MessageBox.Show(code);

                }
                catch (Exception er) { MessageBox.Show(er.Message); }
            }


        }

        private void dgv_itemlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sub_item_list();
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_netcashprice_TextChanged(object sender, EventArgs e)
        {

        }


        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = 0;

            try
            {
                dgv_list_Cell_isClick(dgv_list.CurrentRow.Index);
            }
            catch (Exception) { }
        }
        private void dgv_list_Cell_isClick(int r)
        {
            String code = "";
            try { code = dgv_list["dgvl_code", r].Value.ToString(); }
            catch { code = ""; }

            DataTable dt = db.QueryBySQLCode("SELECT rorder_status FROM rssys.orhdr WHERE ord_code ='" + code + "'");
            if (dt.Rows.Count == 1)
            {
                btn_upd.Enabled = true;
                if (dt.Rows[0]["rorder_status"].ToString().Equals(rocode_released)) //Released
                {
                    btn_upd.Enabled = false;
                }
            }

        }

        private void dgv_loaner_list_Click(object sender, EventArgs e)
        {
            try
            {
                //int r = dgv_loaner_list.CurrentRow.Index;

                //cbo_loaner.Text = dgv_loaner_list["loaner_name", r].Value.ToString();
                //cbo_status.Text = dgv_loaner_list["loaner_status", r].Value.ToString();
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

        private void dgv_loaner_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void btn_print_invoice_Click(object sender, EventArgs e)
        {
            print_auto_sales(sender);
        }

        private void btn_print_agreement_Click(object sender, EventArgs e)
        {
            print_auto_sales(sender);
        }

        private void print_auto_sales(object sender)
        {
            try
            {
                int indx = cbo_print_opt.SelectedIndex;
                if (indx == -1) {
                    MessageBox.Show("Select print option first.");
                    cbo_print_opt.DroppedDown = true;
                    return;
                }

                if (dgv_list.Rows.Count > 0)
                {
                    int r = dgv_list.CurrentRow.Index;
                    String ord_code = dgv_list["dgvl_code", r].Value.ToString();
                    if (!String.IsNullOrEmpty(ord_code))
                    {
                        DataTable dt = dt = db.QueryBySQLCode("SELECT oh.*, i.*, b.*, c.color_desc FROM rssys.orhdr oh LEFT JOIN rssys.items i ON i.item_code=oh.car_item_code LEFT JOIN rssys.brand b ON b.brd_code=i.brd_code LEFT JOIN rssys.color c ON c.id=i.color_id  WHERE ord_code ='" + ord_code + "' LIMIT 1");
                        //car_item_code = //items..item_code item_desc brd_code
                        String vat_sales = "", vat_exsales = "", zero_sales = "", vat_amnt = "", vat_less = "", net_vat = "", total_sales = "", amnt_due = "", vat_add = "", total_amnt_due = ""
                            , sagent = "", terms = ""
                            , vin_no = dt.Rows[0]["car_vin_num"].ToString() // chassis no
                            , vehicle = dt.Rows[0]["car_vin_desc"].ToString()
                            , plate_no = dt.Rows[0]["car_plate"].ToString() // cs no
                            , engine_no = dt.Rows[0]["car_engine"].ToString()
                            , year_model = "" //dt.Rows[0]["year_model"].ToString()
                            , color = dt.Rows[0]["color_desc"].ToString()
                            , brand = dt.Rows[0]["brd_name"].ToString()
                            , dt_trans = gm.toDateString(dt.Rows[0]["trnx_date"].ToString(), "");
                      

                        cbo_agent.SelectedValue = dt.Rows[0]["agentid"].ToString();
                        sagent = cbo_agent.Text;

                        cbo_terms.Text = dt.Rows[0]["terms"].ToString();//
                        terms = cbo_terms.Text;

                        try
                        {
                            double vatS = gm.toNormalDoubleFormat(dt.Rows[0]["ord_amnt"].ToString()) // or total_amnt(maybe due)
                                , vatLess, vatAmt;
                            vatAmt = vatS * (12.0 / 100); // just put '.0' to read as DOUBLE
                            vatLess = vatS - vatAmt;

                            vat_sales = gm.toAccountingFormat(vatS);
                            vat_less = gm.toAccountingFormat(vatLess);
                            vat_amnt = gm.toAccountingFormat(vatAmt);

                            total_sales = gm.toAccountingFormat(vatS);
                            amnt_due = gm.toAccountingFormat(vatLess);
                            vat_add = gm.toAccountingFormat(vatAmt);
                            total_amnt_due = gm.toAccountingFormat(vatS);

                        }
                        catch { }


                        Report rpt = new Report();
                        if (indx == 0) { // DELIVERY NOTE
                            rpt.print_vehicle_delivery_note_2(dt.Rows[0]["customer"].ToString());
                        }
                        else if (indx == 1) { // Vehicle Sales Agreement
                            rpt.print_as_agreement(ord_code, terms, vin_no, vehicle, plate_no, engine_no, year_model, color, brand
                                , sagent, vat_sales, vat_exsales, zero_sales, vat_amnt, vat_less, net_vat, total_sales, amnt_due, vat_add, total_amnt_due, dt_trans);
                        }
                        else if (indx == 2) { // Sales Invoice
                            rpt.print_as_invoice(ord_code, terms, vin_no, vehicle, plate_no, engine_no, year_model, color, brand
                                , sagent, vat_sales, vat_exsales, zero_sales, vat_amnt, vat_less, net_vat, total_sales, amnt_due, vat_add, total_amnt_due, dt_trans);
                        }
                        
                        rpt.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No invoice selected.");
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

        private void btn_new_loan_Click(object sender, EventArgs e)
        {
            isnew = true;
            isnew_item = true;
            lnno_last = 1;

            if (isverified == false)
            {
                frm_reload();
                loan_application = true;
                //frm_clear();
                frm_pclerkpass.ShowDialog();
            }
            else
            {
                MessageBox.Show("Already accessed to sales order.");
                goto_win2();
            }
        }

        public void setInfo_fromAutoLoan(String code)
        {
            try
            {
                isnew = true;
                isverified = true;

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.autoloandhr WHERE app_no ='" + code + "'");
                
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    { //
                        //txt_reference.Text = dt.Rows[i]["reference"].ToString();
                        txt_appcode.Text = dt.Rows[i]["app_no"].ToString();
                        cbo_customer.SelectedValue = dt.Rows[i]["cust_no"].ToString();
                        cbo_customer.Text = dt.Rows[i]["cust_name"].ToString();
                        cbo_loaner.Text = dt.Rows[i]["financer"].ToString();
                        txt_loaner.Text = dt.Rows[i]["financer_id"].ToString();
                        cbo_status.Text = dt.Rows[i]["credit_des"].ToString();
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["mrtk_segment"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["salesman"].ToString();
                        rtxt_remark.Text = dt.Rows[i]["remark"].ToString();
                        txt_srpprice.Text = dt.Rows[i]["net_cash"].ToString();
                        cbo_dp_pct.SelectedValue = dt.Rows[i]["dp"].ToString();
                       
                        cbo_itemcode.Text = dt.Rows[i]["car_item_code"].ToString();
                        cbo_carunit.Text = dt.Rows[i]["car_vin_desc"].ToString();
                        txt_vin.Text = dt.Rows[i]["car_vin_num"].ToString();
                        txt_engine.Text = dt.Rows[i]["car_engine"].ToString();
                        txt_condno.Text = dt.Rows[i]["car_plate"].ToString();

                        txt_srpprice.Text = gm.toAccountingFormat(dt.Rows[i]["reg_charges"].ToString());
                        txt_dpamnt.Text = gm.toAccountingFormat(dt.Rows[i]["dp_amt"].ToString());
                        txt_amnt_financed.Text = gm.toAccountingFormat(dt.Rows[i]["amt_finance"].ToString());
                        cbo_terms.Text = dt.Rows[i]["terms"].ToString();
                        txt_add_on_rate.Text = gm.toAccountingFormat(dt.Rows[i]["add_rate"].ToString());
                        txt_clientDP.Text = gm.toAccountingFormat(dt.Rows[i]["pn_amt"].ToString());
                        txt_totaldiscount.Text = gm.toAccountingFormat(dt.Rows[i]["totaldiscount"].ToString());
                        txt_monthly_amort.Text = gm.toAccountingFormat(dt.Rows[i]["monthly_amor"].ToString());
                        txt_totalpayment.Text = gm.toAccountingFormat(dt.Rows[i]["totalpayment"].ToString());

                        compute_monthlyAmort();
                    }


                    DataTable itemdt4 = db.QueryBySQLCode("SELECT o.ln_num,o.ln_amnt,o.rep_code,o.reference,o.pay_code,m.mp_desc,o.ln_type,o.chargeto FROM rssys.orlne o LEFT JOIN rssys.m10 m ON o.pay_code=m.mp_code WHERE o.ord_code='" + code + "' AND ln_type='P' ORDER BY ln_num ASC");
                    try { dgv_payment.Rows.Clear(); }
                    catch (Exception) { }
                    for (int i = 0; i < itemdt4.Rows.Count; i++)
                    {
                        dgv_payment.Rows.Add();

                        dgv_payment["dgvlp_ln_num", i].Value = itemdt4.Rows[i]["ln_num"].ToString();
                        dgv_payment["dgvlp_pay_code", i].Value = itemdt4.Rows[i]["pay_code"].ToString();
                        dgv_payment["dgvlp_pay_desc", i].Value = itemdt4.Rows[i]["mp_desc"].ToString();
                        dgv_payment["dgvlp_reference", i].Value = itemdt4.Rows[i]["reference"].ToString();
                        dgv_payment["dgvlp_ln_amnt", i].Value = gm.toAccountingFormat(itemdt4.Rows[i]["ln_amnt"].ToString());
                        dgv_payment["dgvlp_rep_code", i].Value = itemdt4.Rows[i]["rep_code"].ToString();
                        dgv_payment["dgvlp_ln_type", i].Value = itemdt4.Rows[i]["ln_type"].ToString();
                        dgv_payment["dgvlp_chargeto", i].Value = itemdt4.Rows[i]["chargeto"].ToString();
                    }

                    //dgv_loaner_list.Rows.Clear();
                    DataTable itemdt3 = db.QueryBySQLCode("SELECT app_no,cust_no,cust_name,status,line FROM rssys.autoloanfinancer WHERE app_no='" + code + "'");

                    for (int i = 0; i < itemdt3.Rows.Count; i++)
                    {
                        //dgv_loaner_list.Rows.Add();
                        //dgv_loaner_list["loaner_line", i].Value = itemdt3.Rows[i]["line"].ToString();
                       // dgv_loaner_list["loaner_no", i].Value = itemdt3.Rows[i]["cust_no"].ToString();
                        //dgv_loaner_list["loaner_name", i].Value = itemdt3.Rows[i]["cust_name"].ToString();
                        //dgv_loaner_list["loaner_status", i].Value = itemdt3.Rows[i]["status"].ToString();
                    }

                    // default / disable
                    gb_finance.Visible = true;

                    goto_win2();
                }
                    
            }
            catch { }
        
        }



        private void btn_addpay_Click(object sender, EventArgs e)
        {
            isnew_item = true;
            enter_payment((++lnno_lastpay).ToString(), (String.IsNullOrEmpty(txt_loaner.Text)?"101":""), "", "", txt_loaner.Text);
        }
        private void enter_payment(String lnno, String mode, String refe, String amnt,String chargeto)
        {

            if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer first.");
                m_auto_customer frm = new m_auto_customer(this, true);
                frm.ShowDialog();
            }
            else
            {
                int ln = 1;

                if (dgv_payment.Rows.Count > 0)
                {
                    int n = 0;
                    if (isnew_item)
                    {
                        n = (int)double.Parse(dgv_payment["dgvlp_ln_num", dgv_payment.Rows.Count - 1].Value.ToString());
                    }
                    else
                    {
                        n = (int)double.Parse(dgv_payment["dgvlp_ln_num", dgv_payment.CurrentRow.Index].Value.ToString());
                    }

                    ln = Math.Abs(n) + (isnew_item ? 1 : 0);
                }

                frm_payment = new z_payment(this, isnew_item, chargeto, cbo_customer.SelectedValue.ToString(), gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_srp_netvat.Text) - (gm.toNormalDoubleFormat(txt_totalpayment.Text) * -1)).ToString("0.00")), txt_totaldiscount.Text, "", true, ln.ToString(), mode, refe, amnt);

                frm_payment.ShowDialog();

                this.Refresh();
            }
        }
        private void btn_updpay_Click(object sender, EventArgs e)
        {
            isnew_item = false;
            try
            {
                if (dgv_payment.Rows.Count > 0)
                {
                    int r = dgv_payment.CurrentRow.Index;
                    String line_no = dgv_payment["dgvlp_ln_num", r].Value.ToString();
                    String mode = dgv_payment["dgvlp_pay_code", r].Value.ToString();
                    String referen = dgv_payment["dgvlp_reference", r].Value.ToString();
                    String amount = dgv_payment["dgvlp_ln_amnt", r].Value.ToString();
                    String chargeto = dgv_payment["dgvlp_chargeto", r].Value.ToString();
                    //MessageBox.Show(line_no.ToString());
                    enter_payment((gm.toNormalDoubleFormat(line_no) * -1).ToString(), mode, referen, amount, chargeto);
                }
                else
                {
                    MessageBox.Show("No payment item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No payment item selected catch."); }

        }
        public void total_amountdue()
        {
            try
            {
                Double paid = 0.00;
                for (int p = 0; p < dgv_payment.Rows.Count; p++)
                {
                    paid += gm.toNormalDoubleFormat(dgv_payment["dgvlp_ln_amnt", p].Value.ToString());
                }
                txt_totalpayment.Text = gm.toAccountingFormat(paid);
            }
            catch { }
        }
        public void total_itemamount()
        {
            try
            {
                Double amount = 0.00;
                for (int p = 0; p < dgv_itemlist.Rows.Count; p++)
                {
                    amount += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", p].Value.ToString());
                }
                txt_totalitemamnt.Text = gm.toAccountingFormat(amount);
            }
            catch { }
        }

        private void tbpg_items_Selecting(object sender, TabControlCancelEventArgs e)
        { }




    }
}
