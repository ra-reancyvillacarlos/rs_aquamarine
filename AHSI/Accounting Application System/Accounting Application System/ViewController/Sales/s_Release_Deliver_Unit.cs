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
    public partial class s_Release_Deliver_Unit : Form
    {
        public s_Release_Deliver_Unit()
        {
            InitializeComponent();
        }

        private void s_Release_Deliver_Unit_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }
        }

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
        Boolean isRepair = false, isCashier = false, isReleased = true;

        int lnno_last = 1;


        // Release Status 
        private String rocode_ready = "00006"; // READY TO RELEASE
        private String rocode_released = "00007";// RELEASE

        Boolean direct_sales = false;
        Boolean loan_application = false;



        public s_Release_Deliver_Unit(Boolean repair, Boolean cashier) : this()
        {
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
            gc.load_decision(cbo_status);

            gc.load_decision(cbo_mstatus);
            gc.load_crm(cbo_release);
            gc.load_salesclerk(cbo_release);
            gc.load_insurance(cbo_insurance);
            
            frm_clear();
            disp_list();
          
            gc.load_salesagent(cbo_agent);
            gc.load_vehicle_info(cbo_vehicle);

            gc.load_salesclerk(cbo_clerk);
            gc.load_salesclerk(cbo_cashier);
            gc.load_marketsegment(cbo_marketsegment);
            gc.load_decision(cbo_status);
            gc.load_crm(cbo_customer);


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
                btn_new_loan.Enabled = false;
                btn_print_agreement.Visible = false;
                btn_cancel.Enabled = false;

                frm_enable(false);

                btn_saveorder.Enabled = true;

                frm_readOnly();
                cbo_clerk.Enabled = true;
                cbo_cashier.Enabled = true;

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
                WHERE = " o.pending='Y' AND ";
            }
            if (cbo_outlet_olist.SelectedIndex > -1)
            {
                out_code = cbo_outlet_olist.SelectedValue.ToString();
                WHERE += " (o.out_code='" + out_code + "' OR COALESCE(o.out_code,o.out_code,'')='') AND ";
            }
            if (cbo_mstatus.SelectedIndex != -1)
            {
                WHERE += " af.status='" + cbo_mstatus.Text + "' AND ";
            }
            /*if (isRepair) {
                //WHERE = WHERE + " o.rorder_status='" + ro_stat_code + "' AND ";
                WHERE = WHERE + " ( o.rorder_status='" + rocode_ready + "' OR o.rorder_status='" + rocode_released + "' ) AND ";
            }*/

            //app_no financer financer_no debt_code	customer 
            /*
            DataTable dt = db.QueryBySQLCode("
            
           
            
            WHERE (a.trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') " + (WHERE.Length != 0 ? WHERE : "") + " ORDER BY financer_code ASC");
            */
            //return db.QueryBySQLCode("SELECT ro.ro_stat_desc,o.out_code,o.ord_code,o.customer,o.payment,o.cancel,o.net_amnt,o.ord_date,o.trnx_date AS t_date,o.tax_amnt,o.pending,o.amnt_due,o.disc_amnt, o.user_id,o.loc,o.user_id2,o.rep_code, o.cashier,o.gross_amnt,o.ord_amnt,o.pay_code FROM " + db.schema + ".orhdr o LEFT JOIN  " + db.schema + ".whouse w ON w.whs_code=o.loc LEFT JOIN " + db.schema + ".ro_status ro ON ro.ro_stat_code=o.rorder_status WHERE " + WHERE + " trnx_date BETWEEN '" + frm.ToString("yyyy-MM-dd") + "' AND '" + to.ToString("yyyy-MM-dd") + "' ORDER BY o.ord_code");

            return db.QueryBySQLCode("SELECT af.financer_code AS financer_no, af.app_no, af.m06_finance_code AS debt_code, af.cust_name AS financer, af.status, af.m06_finance_name AS customer, COALESCE(o.t_date,o.t_date,a.trnx_date) AS trnx_date, ro.ro_stat_desc, o.out_code, o.ord_code, o.payment, o.cancel, o.net_amnt,o.ord_date, o.tax_amnt,o.pending,o.amnt_due,o.disc_amnt, o.user_id,o.loc,o.user_id2,o.rep_code, o.cashier,o.gross_amnt,o.ord_amnt,o.pay_code FROM rssys.autoloanfinancer af LEFT JOIN rssys.autoloandhr a ON a.app_no=af.app_no LEFT JOIN rssys.orhdr o ON o.app_no=af.app_no LEFT JOIN rssys.whouse w ON w.whs_code=o.loc LEFT JOIN rssys.ro_status ro ON ro.ro_stat_code=o.rorder_status WHERE " + WHERE + " (COALESCE(o.t_date,o.t_date,a.trnx_date) BETWEEN '" + frm.ToString("yyyy-MM-dd") + "' AND '" + to.ToString("yyyy-MM-dd") + "') ORDER BY af.financer_code, o.ord_code");
        } //o.trnx_date AS t_date,

        public DataTable get_soitemlist(String ord_code)
        {
            DataTable dt = null;

            try
            {
                dt = db.QueryBySQLCode("SELECT * FROM rssys.vclne WHERE ord_code='" + ord_code + "' ORDER BY vc_code ASC");
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
            DataTable dt = get_soitemlist(code);

            try { dgv_itemlist.Rows.Clear(); }
            catch (Exception er) { MessageBox.Show(er.Message); }

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int r = dgv_itemlist.Rows.Add();
                    dgv_itemlist["dgvli_lnno", r].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvli_code", r].Value = dt.Rows[i]["vc_code"].ToString();
                    dgv_itemlist["dgvli_desc", r].Value = dt.Rows[i]["vc_desc"].ToString();

                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            inc_lnno();
        }


        public void frm_reload()
        {
            frm_reset();
            frm_clear();

            frm_enable(true);
        }

        public void frm_enable(Boolean flag)
        {
            //*btn_additem.Enabled = flag;
            //*btn_upditem.Enabled = flag;
            //*btn_delitem.Enabled = flag;
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

            txt_servex.Text = "";
            cbo_insurance.SelectedIndex = -1;
            cbo_loaner.Text = "";
            cbo_status.SelectedIndex = -1;
            cbo_marketsegment.SelectedIndex = -1;
            cbo_agent.SelectedIndex = -1;
            cbo_sa.SelectedIndex = -1;
            cbo_clerk.SelectedIndex = -1;
            cbo_cashier.SelectedIndex = -1;
            rtxt_remark.Text = "";

            cbo_vehicle.SelectedIndex = -1;
            txt_vin.Text = "";
            txt_engine.Text = "";

            dtp_receive.ResetText();
            dtp_d_release.ResetText();
            dtp_t_release.ResetText();
            dtp_d_unitdel.ResetText();
            dtp_t_unitdel.ResetText();
            txt_km_reading.Text = "" ;

            cbo_release.SelectedIndex = -1;
            cbo_unitdel.Text = "";
            cbo_guard.Text = "";
            cbo_pickup.Text = "";

            btn_vehicle.Enabled = false;
            btn_customer.Enabled = false;

            try
            {
                dgv_itemlist.Rows.Clear();
            }
            catch (Exception) { }
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
            cbo_status.Enabled = false;
            cbo_marketsegment.Enabled = false;
            cbo_agent.Enabled = false;
            cbo_sa.Enabled = false;
            cbo_clerk.Enabled = false;
            cbo_cashier.Enabled = false;
            rtxt_remark.ReadOnly = true;

            cbo_vehicle.Enabled = false;
            txt_vin.ReadOnly = true;
            txt_engine.ReadOnly = true;

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
            /*
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
            /*
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
            }*/
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            s_auto_Checklist frm = new s_auto_Checklist(this);
            frm.ShowDialog();
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgv_itemlist.CurrentRow.Index;
                String code = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                if(!String.IsNullOrEmpty(code))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this checklist?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        dgv_itemlist.Rows.RemoveAt(r);
                        MessageBox.Show("Successfully removed.");
                    }
                }
            }
            catch
            {
                MessageBox.Show("No selected checklist.");
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
            String ord_code, out_code, debt_code, customer, ord_date, mcardid, user_id, t_date, t_time, loc, user_id2, t_date2, t_time2, trnx_date, rep_code = "", agentid, amount_paid;
            String branch = GlobalClass.branch, stk_ref = "", stk_po_so = "", market_segment_id = "";

            String vsi_no, d_receive, last_km_reading, releasedby, d_release, t_release, unitdelby, d_unitdel, t_unitdel, pickupby, guardby;

            String status, remark, loaner, vehicle = "", terms = "", rorder_status;
            String cashier = "";
            String pending = "Y";
            String col = "", val = "", col2 = "", val2 = "", col3="",val3="";
            String notifyadd = null;
            String table = "orhdr";
            String tableln = "orlne";
            String tableln2 = "vclne";


            if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the customer field.");
                m_auto_customer frm = new m_auto_customer(this, true);
                frm.ShowDialog();
            }
            else if (cbo_vehicle.SelectedIndex == -1 && String.IsNullOrEmpty(cbo_vehicle.Text))
            {
                MessageBox.Show("Please select the vehicle field.");
                m_vehiclec_info frm = new m_vehiclec_info(this, true);
                frm.ShowDialog();
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
            else if (cbo_loaner.Text == "")
            {
                MessageBox.Show("Please enter financer or select from the left side datagridview.");
                //m_vehiclec_info frm = new m_vehiclec_info(this, true);
                //frm.ShowDialog();
            }
            else if (cbo_status.SelectedIndex == -1 || cbo_status.Text=="")
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
            else if (cbo_cashier.SelectedIndex == -1)
            {
                MessageBox.Show("Please select cashier field.");
                cbo_cashier.DroppedDown = true;
            }
            else if (cbo_rorder_status.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Repair Order Status");
                cbo_rorder_status.DroppedDown = true;
            }
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

                user_id = GlobalClass.username;
                t_date = db.get_systemdate("yyyy-MM-dd");
                t_time = db.get_systemtime();
                market_segment_id = cbo_marketsegment.SelectedValue.ToString();
                amount_paid = gm.toNormalDoubleFormat(amt_tendered).ToString("0.00");

                trnx_date = dtp_trnx_date.Value.ToString("yyyy-MM-dd");
                ord_date = trnx_date;

                String insurance = (cbo_insurance.SelectedValue??"").ToString();
                loaner = cbo_loaner.Text;
                //vehicle = cbo_vehicle.SelectedValue.ToString();
                //terms = cbo_vehicle.SelectedValue.ToString();
                status = cbo_status.Text;

                rorder_status = cbo_rorder_status.SelectedValue.ToString();

                remark = rtxt_remark.Text;

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

                vsi_no = txt_vsi_no.Text;
                d_receive = dtp_receive.Value.ToString("yyyy-MM-dd");
                last_km_reading = txt_km_reading.Text;
                releasedby = (cbo_release.SelectedValue ?? "").ToString();
                d_release = dtp_d_release.Value.ToString("yyyy-MM-dd");
                t_release = dtp_t_release.Value.ToString("hh:mm tt");
                unitdelby = (cbo_unitdel.Text ?? "").ToString();
                d_unitdel = dtp_d_unitdel.Value.ToString("yyyy-MM-dd");
                t_unitdel = dtp_t_unitdel.Value.ToString("hh:mm tt");
                pickupby = (cbo_pickup.Text ?? "").ToString();
                guardby = (cbo_guard.Text ?? "").ToString();

                /*
                 
                col2 = ", user_id2, t_date2, t_time2, tax_amnt, amnt_due, pay_code, cashier, amnt_tendered,d_receive,releasedby,d_release,t_release,unitdelby,d_unitdel,t_unitdel,pickupby,guardby,last_km_reading,vsi_no";

                val2 = ", '" + user_id2 + "', '" + t_date2 + "', '" + t_time2 + "', '0', '0', '" + pay_code + "', '" + cashier + "', '" + amount_paid + "','" + d_receive + "','" + releasedby + "','" + d_release + "','" + t_release + "','" + unitdelby + "','" + d_unitdel + "','" + t_unitdel + "','" + pickupby + "','" + guardby + "','" + last_km_reading + "','" + vsi_no + "'";


                col3 = ",status,remark, loaner, vehicle, terms, dp_pct, condno, pn_amnt, amnt_financed, reg_chg, doc_stamp, add_on_rate, processing_fee, monthly_amort, max_sc, paid_amnt, dp_payment, dp_reference, dpamnt";
                val3 = ",'" + status + "'," + remark + ",'" + loaner + "','" + vehicle + "','" + terms + "','0','','0','0','0','0','0','0','0','0','0','0','0','0'";

                if (isnew == false)
                {
                    col = ", tax_amnt='', amnt_due='', user_id2='" + user_id2 + "', t_date2='" + t_date2 + "', t_time2='" + t_time2 + "', pay_code='" + pay_code + "', cashier='" + cashier + "', amnt_tendered='" + amount_paid + "'";
                }
                 
                 */

                if (isnew)
                {

                    /*
                     if (isRepair)
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

                    if (success)
                    {

                        int r = dgv_list.CurrentRow.Index;
                        String app_no = dgv_list["dgvl_loanno", r].Value.ToString();

                        notificationText = "has added: ";
                        stk_ref = stk_trns_type + "#" + ord_code;
                        stk_po_so = ord_code;
                        //txt_reference.Text = stk_ref;
                        //reference = txt_reference.Text;
                        ord_code = db.get_ord_code(out_code);

                        col = "rorder_status,app_no, ord_code, out_code, debt_code, customer, ord_date, trnx_date, reference, mcardid, user_id, t_date, t_time, loc,rep_code, agentid, branch,market_segment_id, pending" + col2 + col3;
                        val = "'" + rorder_status + "','" + app_no + "','" + ord_code + "', '" + out_code + "', '" + debt_code + "', " + db.str_E(customer) + ", '" + ord_date + "',  '" + trnx_date + "', '', '" + mcardid + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + loc + "', '" + rep_code + "', '" + agentid + "', '" + branch + "','" + market_segment_id + "', '" + pending + "'" + val2 + val3;

                        if (db.InsertOnTable(table, col, val))
                        {
                            try
                            {
                                notifyadd = add_items(tableln2, ord_code);

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
                            catch (Exception er) { MessageBox.Show(er.ToString()); }
                        }

                        if (success == false)
                        {
                            MessageBox.Show("Failed on saving. on New");
                        }
                    
                    }
                     */

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
                        //col = "ord_code='" + ord_code + "', out_code='" + out_code + "', debt_code='" + debt_code + "', customer='" + customer + "', ord_date='" + ord_date + "', ord_amnt='0', disc_amnt='0', reference='', mcardid='" + mcardid + "', user_id='" + user_id + "', t_date='" + t_date + "', t_time='" + t_time + "', loc='" + loc + "', trnx_date='" + trnx_date + "', pay_code='" + pay_code + "', cashier='" + cashier + "', rep_code='" + rep_code + "', agentid='" + agentid + "', branch='" + branch + "', pending='" + pending + "' ,payment='" + amount_paid + "',loaner='" + loaner + "', vehicle='" + vehicle + "', terms='" + terms + "', dp_pct='0', condno='0', pn_amnt='0', amnt_financed='0', reg_chg='0', doc_stamp='', add_on_rate='0', processing_fee='0', monthly_amort='0', max_sc='0', paid_amnt='0', dp_payment='0', dp_reference='0', dpamnt='0',remark=" + remark + ",status='" + status + "',rorder_status='" + rorder_status + "',d_receive='" + d_receive + "',releasedby='" + releasedby + "',d_release='" + d_release + "',t_release='" + t_release + "',unitdelby='" + unitdelby + "',d_unitdel='" + d_unitdel + "',t_unitdel='" + t_unitdel + "',pickupby='" + pickupby + "',guardby='" + guardby + "',last_km_reading='" + last_km_reading + "',vsi_no='" + vsi_no + "'";

                        col = "ord_code='" + ord_code + "', out_code='" + out_code + "', debt_code='" + debt_code + "', customer=" + db.str_E(customer) + ", ord_date='" + ord_date + "', mcardid='" + mcardid + "', user_id='" + user_id + "', t_date='" + t_date + "', t_time='" + t_time + "', loc='" + loc + "', trnx_date='" + trnx_date + "', pay_code='" + pay_code + "', cashier='" + cashier + "', rep_code='" + rep_code + "', agentid='" + agentid + "', branch='" + branch + "', pending='" + pending + "', loaner='" + loaner + "', vehicle='" + vehicle + "', remark=" + db.str_E(remark) + ", status='" + status + "', rorder_status='" + rorder_status + "', d_receive='" + d_receive + "', releasedby='" + releasedby + "', d_release='" + d_release + "', t_release='" + t_release + "',unitdelby='" + unitdelby + "', d_unitdel='" + d_unitdel + "', t_unitdel='" + t_unitdel + "', pickupby='" + pickupby + "', guardby='" + guardby + "', last_km_reading='" + last_km_reading + "', vsi_no='" + vsi_no + "', insurance='" + insurance + "'"; 
                        
                        if (db.UpdateOnTable(table, col, "ord_code='" + ord_code + "'"))
                        {
                            db.DeleteOnTable("stkcrd", "po_so='" + ord_code + "' AND trn_type='" + stk_trns_type + "'");
                            try
                            {
                                notifyadd = add_items(tableln2, ord_code);

                                //MessageBox.Show(notifyadd.ToString());
                                //if (String.IsNullOrEmpty(notifyadd) == false) 
                                //{
                                notificationText += String.IsNullOrEmpty(notifyadd) ? "" : notifyadd;
                                notificationText += Environment.NewLine + " with SO#" + ord_code;
                                notify.saveNotification(notificationText, "Sales Outlet");
                                success = true;
                                add_vehicle_info(ord_code);
                                //}
                                //else
                                //{
                                //    success = false;
                                //}
                            }
                            catch (Exception er) { MessageBox.Show(er.ToString()); }
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
                        frm_payment.Close();
                    }
                    disp_list();
                    goto_win1();
                    isverified = false;
                    if (!isRepair) {
                        frm_reload();
                    }

                }
            }
        }
        private String add_items(String tableln, String ord_code)
        {
            String notificationText = null;
            String ln_num, vc_code, vc_desc;
            String val2 = "";
            String col2 = "ln_num, vc_code, vc_desc, ord_code";
            
            try
            {
                db.UpdateOnTable(tableln, "cancel='Y'", "ord_code='" + ord_code + "'");
                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    ln_num = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                    vc_code = dgv_itemlist["dgvli_code", r].Value.ToString();
                    vc_desc = dgv_itemlist["dgvli_desc", r].Value.ToString();

                    val2 = "" + db.str_E(ln_num) + ", " + db.str_E(vc_code) + ", " + db.str_E(vc_desc) + ", " + db.str_E(ord_code) + "";

                    db.DeleteOnTable(tableln, "ord_code='" + ord_code + "' AND vc_code='" + vc_code + "'");
                    db.InsertOnTable(tableln, col2, val2);


                    notificationText += Environment.NewLine + vc_code + " - " + vc_desc;
                }
                db.DeleteOnTable(tableln, "ord_code='" + ord_code + "' AND cancel='Y'");
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            return notificationText;
        }

        public void add_vehicle_info(String ord_code){

            ////txt_condno txt_engine txt_vin

            if (!isReleased) {
                String val, col = "vin_desc, color, cartype, brand, year_model, vin_no, engine_no, plate_no, date_release, dealer, owner, warranty_to, insurance, generic";

                String vin_no = txt_vin.Text;
                String vin_desc = cbo_vehicle.Text;
                String plate_no = txt_condno.Text;
                String engine_no = txt_engine.Text;
                String debt_code = cbo_customer.SelectedValue.ToString();
                String date = db.get_systemdate("");
                String cartype = "", color = "", yearmodel = "", branch = "", brand = "", generic = "";
                try
                {
                    DataTable dt = db.QueryBySQLCode("SELECT i.cartype, i.color_id, i.yearmodel, i.branch, i.brd_code, i.gen_code, COALESCE(color_desc,'') AS color_desc FROM rssys.items i LEFT JOIN rssys.color c ON c.id=i.color_id  WHERE i.item_code='" + cbo_itemcode.Text + "' LIMIT 1");

                    cartype = dt.Rows[0]["cartype"].ToString();
                    color = dt.Rows[0]["color_desc"].ToString();
                    yearmodel = dt.Rows[0]["yearmodel"].ToString();
                    branch = dt.Rows[0]["branch"].ToString();
                    brand = dt.Rows[0]["brd_code"].ToString();
                    generic = dt.Rows[0]["gen_code"].ToString();

                    db.InsertOnTable("vehicle_info", col, "" + db.str_E(vin_desc) + ",'" + color + "','" + cartype + "','" + brand + "','" + yearmodel + "','" + vin_no + "','" + engine_no + "','" + plate_no + "','" + date + "','" + /*dealer*/"" + "','" + debt_code + "','" + date + "','" + (cbo_insurance.SelectedValue ?? "").ToString() + "','" + generic + "'");
                }
                catch { }
            }
            
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
            /*
            frm_si = new z_enter_sales_item(this, true, "", typ, lnno_last);
            frm_si.ShowDialog();
            */
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
            //z_ItemSearch frm = new z_ItemSearch(1);
            //frm.ShowDialog();
            m_vehiclec_info frm = new m_vehiclec_info(this,true);
            frm.ShowDialog();
        }

        public void verifiedClerk(String cid, String cname)
        {
            DataTable dt;
            String outcode = "";
            isverified = true;

            if (isCashier == true && isnew == true)
            {
                cbo_clerk.SelectedValue = cid;
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

            //dgvl_code
            isnew = true;
            try
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["dgvl_code", r].Value.ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    isnew = false;
                }
            }
            catch { }


            if (isnew)
            {
                try { outcode = cbo_outlet_olist.SelectedValue.ToString(); }
                catch { }

                if (isverified)
                {
                    int r = dgv_list.CurrentRow.Index;
                    String app_no = dgv_list["dgvl_loanno", r].Value.ToString();
                    String financer = dgv_list["dgvl_financer", r].Value.ToString();
                    String decision = dgv_list["dgvl_status", r].Value.ToString();

                    setInfo_fromAutoLoan(app_no, financer, decision);
                }
            }
            else if (isnew == false)
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["dgvl_code", r].Value.ToString();

                dt = db.QueryBySQLCode("SELECT * FROM rssys.orhdr WHERE ord_code ='" + code + "'");
                txt_ordcode.Text = code;

                if (dt.Rows.Count > 0) 
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cbo_customer.Text = dt.Rows[i]["customer"].ToString();
                        txt_appcode.Text = dt.Rows[i]["app_no"].ToString();
                        txt_servex.Text = dt.Rows[i]["mcardid"].ToString();
                        dtp_trnx_date.Value = DateTime.Parse(dt.Rows[i]["trnx_date"].ToString());

                        dtp_appdate.Value = DateTime.Parse(dt.Rows[i]["ord_date"].ToString());
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                        cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                       
                        cbo_rorder_status.SelectedValue = dt.Rows[i]["rorder_status"].ToString();

                        isReleased = (cbo_rorder_status.Text??"").ToString().ToLower().Equals("released");

                        cbo_insurance.SelectedValue = dt.Rows[i]["insurance"].ToString();//
                        cbo_loaner.Text = dt.Rows[i]["loaner"].ToString();//
                        cbo_status.Text = dt.Rows[i]["status"].ToString();
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                        cbo_sa.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                        cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                        cbo_cashier.SelectedValue = dt.Rows[i]["cashier"].ToString();
                        rtxt_remark.Text = dt.Rows[i]["remark"].ToString();

                        cbo_itemcode.Text = dt.Rows[i]["car_item_code"].ToString();
                        cbo_vehicle.Text = dt.Rows[i]["car_vin_desc"].ToString();
                        txt_vin.Text = dt.Rows[i]["car_vin_num"].ToString();
                        txt_engine.Text = dt.Rows[i]["car_engine"].ToString();
                        txt_condno.Text = dt.Rows[i]["car_plate"].ToString();

                        dtp_receive.Value = DateTime.Now;
                        try { dtp_receive.Value = DateTime.Parse(dt.Rows[i]["d_receive"].ToString()); }
                        catch { }
                        dtp_d_release.Value = DateTime.Now;
                        try { dtp_d_release.Value = DateTime.Parse(dt.Rows[i]["d_release"].ToString()); }
                        catch { }
                        dtp_d_unitdel.Value = DateTime.Now;
                        try { dtp_d_unitdel.Value = DateTime.Parse(dt.Rows[i]["d_unitdel"].ToString()); }
                        catch { }
                        dtp_t_release.Value = DateTime.Now;
                        try { dtp_t_release.Value = DateTime.Parse(dt.Rows[i]["t_release"].ToString()); }
                        catch { }
                        dtp_t_unitdel.Value = DateTime.Now;
                        try { dtp_t_unitdel.Value = DateTime.Parse(dt.Rows[i]["t_unitdel"].ToString()); }
                        catch { }

                        txt_km_reading.Text = dt.Rows[i]["last_km_reading"].ToString();

                        //cbo_release.SelectedValue = dt.Rows[i]["releasedby"].ToString();
                        cbo_release.SelectedValue = cid;

                        cbo_unitdel.Text = dt.Rows[i]["unitdelby"].ToString();
                        if (String.IsNullOrEmpty(dt.Rows[i]["pickupby"].ToString())) {
                            cbo_pickup.Text = cbo_customer.Text;
                        }
                        else {
                            cbo_pickup.Text = dt.Rows[i]["pickupby"].ToString();
                        }
                        
                        cbo_guard.Text = dt.Rows[i]["guardby"].ToString();
                        txt_vsi_no.Text = dt.Rows[i]["vsi_no"].ToString();

                        if (isReleased) {
                            cbo_insurance.Enabled = false;
                            cbo_itemcode.Enabled = false;
                            cbo_vehicle.Enabled = false;
                            txt_vin.ReadOnly = true;
                            txt_engine.ReadOnly = true;
                            txt_condno.ReadOnly = true;
                        }

                    }
                }
                vehicle_info_entry();
                disp_itemlist(code);
                goto_win2();
            }

            //frm_enable(true);

        }

        public void set_custvalue_frm(String custcode)
        {
            try { cbo_customer.Items.Clear(); }
            catch { }

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
            //disp_loaner(cbo_customer.SelectedValue.ToString());
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
            //vehicle_info_entry();
        }


        private void vehicle_info_entry()
        {
            DataTable dt = null;
            try
            {
                dt = db.QueryBySQLCode("SELECT vin_no, engine_no FROM rssys.vehicle_info WHERE vin_no='" + cbo_vehicle.SelectedValue.ToString() + "'");
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
        }

        private void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
        }
        private void AdjustColumnOrder()
        {
            dgv_list.AutoGenerateColumns = false;
            dgv_list.Columns["dgvl_code"].DisplayIndex = 0;//rorder_status
            dgv_list.Columns["dgvl_rorder_status"].DisplayIndex = 1;
            dgv_list.Columns["dgvl_loanno"].DisplayIndex = 2;
            dgv_list.Columns["dgvl_financer"].DisplayIndex = 3;
            dgv_list.Columns["dgvl_customer"].DisplayIndex = 4;
            dgv_list.Columns["dgvl_ord_date"].DisplayIndex = 5;
            dgv_list.Columns["dgvl_trnx_date"].DisplayIndex = 6;
            dgv_list.Columns["dgvl_debt_code"].DisplayIndex = 7;
            dgv_list.Columns["dgvl_financer_no"].DisplayIndex = 8;
            dgv_list.Columns["dgvl_status"].DisplayIndex = 9;
            dgv_list.Columns["dgvl_whs_desc"].DisplayIndex = 10;
            dgv_list.Columns["dgvl_out_code"].DisplayIndex = 11;
            dgv_list.Columns["dgvl_cashier"].DisplayIndex = 12;
            dgv_list.Columns["dgvl_assistedby"].DisplayIndex = 13;
             
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


        private void btn_print_invoice_Click(object sender, EventArgs e)
        {
            print_auto_sales(sender);
        }

        private void btn_print_agreement_Click(object sender, EventArgs e)
        {
            //print_auto_sales(sender);
        }

        private void print_auto_sales(object sender)
        {
            try
            {
               /* if (comboBox2.SelectedIndex == -1) {
                    MessageBox.Show("Select print option first.");
                    comboBox2.DroppedDown = true;
                    return;
                }*/
                if (dgv_list.Rows.Count > 0)
                {
                    int r = dgv_list.CurrentRow.Index;
                    String ord_code = dgv_list["dgvl_code", r].Value.ToString();
                    if (!String.IsNullOrEmpty(ord_code))
                    {
                        DataTable dt = db.QueryBySQLCode("SELECT oh.*, i.*, b.*, c.color_desc FROM rssys.orhdr oh LEFT JOIN rssys.items i ON i.item_code=oh.car_item_code LEFT JOIN rssys.brand b ON b.brd_code=i.brd_code LEFT JOIN rssys.color c ON c.id=i.color_id  WHERE ord_code ='" + ord_code + "' LIMIT 1");

                        cbo_agent.SelectedValue = dt.Rows[0]["agentid"].ToString();

                        String sagent = cbo_agent.Text
                            , debt_code = dt.Rows[0]["debt_code"].ToString()
                            , vin_no = dt.Rows[0]["car_vin_num"].ToString() // chassis no
                            , vehicle = dt.Rows[0]["car_vin_desc"].ToString()
                            , plate_no = dt.Rows[0]["car_plate"].ToString() // cs no
                            , engine_no = dt.Rows[0]["car_engine"].ToString()
                            , year_model = "" //dt.Rows[0]["year_model"].ToString()
                            , color = dt.Rows[0]["color_desc"].ToString()
                            , brand = dt.Rows[0]["brd_name"].ToString()
                            , dt_trans = gm.toDateString(dt.Rows[0]["trnx_date"].ToString(), "")
                            , guardby = dt.Rows[0]["guardby"].ToString()
                            , pickupby = dt.Rows[0]["pickupby"].ToString()
                            , unitdelby = dt.Rows[0]["unitdelby"].ToString()
                            , releasedby = dt.Rows[0]["releasedby"].ToString()
                            , last_km_reading = dt.Rows[0]["last_km_reading"].ToString()
                            , t_unitdel = dt.Rows[0]["t_unitdel"].ToString()
                            , t_release = dt.Rows[0]["t_release"].ToString()
                            , d_unitdel = gm.toDateString(dt.Rows[0]["d_unitdel"].ToString(), "")
                            , d_release = gm.toDateString(dt.Rows[0]["d_release"].ToString(), "")
                            , d_receive = gm.toDateString(dt.Rows[0]["d_receive"].ToString(), "")
                            , remarks = dt.Rows[0]["remark"].ToString()
                            , vsi_no = dt.Rows[0]["vsi_no"].ToString();
                        
                        //
                        Report rpt = new Report();
                        if ((Button)sender == btn_print_invoice)
                        {
                            rpt.print_vehicle_delivery_note(ord_code, sagent, debt_code, vin_no, vehicle, plate_no, engine_no, year_model, color, last_km_reading, vsi_no, dt_trans, guardby, pickupby, unitdelby, releasedby, t_unitdel, t_release, d_unitdel , d_release, d_receive, remarks);
                        }
                        else if ((Button)sender == btn_print_agreement)
                        {
                            //rpt.print_vehicle_delivery_note_2(ord_code);
                        }
                        rpt.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No invoice selected. / Invoice is not process.");
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

        public void setInfo_fromAutoLoan(String code, String financer, String decision)
        {
            try
            {
                isnew = true;
                isverified = true;

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.autoloandhr WHERE app_no ='" + code + "'");
                
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //txt_reference.Text = dt.Rows[i]["reference"].ToString();
                        cbo_customer.SelectedValue = dt.Rows[i]["cust_no"].ToString();
                        cbo_customer.Text = dt.Rows[i]["cust_name"].ToString();
                        cbo_loaner.Text = dt.Rows[i]["financer"].ToString();
                        cbo_status.Text = dt.Rows[i]["credit_des"].ToString();
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["mrtk_segment"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["salesman"].ToString();
                        rtxt_remark.Text = dt.Rows[i]["remark"].ToString();
                        cbo_vehicle.SelectedValue = dt.Rows[i]["vehicle_code"].ToString();
                        cbo_loaner.Text = financer;
                        cbo_status.Text = decision;

                        cbo_pickup.Text = cbo_customer.Text;
                    }


                    cbo_rorder_status.SelectedIndex = 0;
                    btn_vehicle.Enabled = false;
                    cbo_vehicle.Enabled = false;
                    //txt_engine.Enabled = false;
                    //txt_vin.Enabled = false;
                    btn_customer.Enabled = false;
                    cbo_customer.Enabled = false;

                    goto_win2();
                }
                    
            }
            catch { }

        }

        private void btn_additem_Click_1(object sender, EventArgs e)
        {
            s_auto_Checklist frm = new s_auto_Checklist(this);
            frm.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbo_mstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_list();
        }

    }
}
