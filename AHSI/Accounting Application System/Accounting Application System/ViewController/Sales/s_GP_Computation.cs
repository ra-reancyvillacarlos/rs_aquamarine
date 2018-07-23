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
    public partial class s_GP_Computation : Form
    {
        GlobalMethod gm = new GlobalMethod();
        GlobalClass gc = new GlobalClass();
        thisDatabase db = new thisDatabase();
        z_clerkpassword frm_pclerkpass;

        private int lnno_last = 1;
        private Boolean isnew_item = true;
        private Boolean isnew = true;
        private Boolean seltbp = false;
        private Boolean isverified = false;
        private Boolean isCashier = false;

        public s_GP_Computation()
        {
            InitializeComponent();
            init_load();
            disp_list();
        }
        private void s_GP_Computation_Load(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Maximized;
        }
        private void init_load()
        {
            frm_pclerkpass = new z_clerkpassword(this);
            gc.load_whouse(cbo_whsname);
            gc.load_outlet_carsale(cbo_outlet, true);
            gc.load_outlet_carsale(cbo_outlet_olist, true);

            gc.load_repair_orderstatus(cbo_rorder_status);

            gc.load_customer(cbo_customer);
            gc.load_salesagent(cbo_agent);
            gc.load_vehicle_info(cbo_vehicle);

            gc.load_salesclerk(cbo_clerk);
            gc.load_salesclerk(cbo_cashier);
            gc.load_marketsegment(cbo_marketsegment);
            gc.load_downpayment(cbo_dp_perc); //cbo_dp_pct
            gc.load_decision(cbo_status);
            gc.load_crm(cbo_customer);

            //default value for outlets
            try
            {
                cbo_outlet.SelectedIndex = 0;
                cbo_outlet_olist.SelectedIndex = 0;
            }
            catch { }

            isnew = true;
            frm_clear();
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


            cbo_dp_perc.SelectedIndex = -1;
            cbo_sales_exe.SelectedIndex = -1;
            txt_net_profit.Text = "0.00";
            txt_payback.Text = "0.00";
            txt_profit_b4_pay.Text = "0.00";
            txt_market_exp.Text = "0.00";
            txt_override.Text = "0.00";
            txt_fuel_trans_cost.Text = "0.00";
            txt_finance_cost.Text = "0.00";
            txt_total_dir_exp.Text = "0.00";
            txt_unit_comm.Text = "0.00";
            txt_referal.Text = "0.00";
            txt_sales_inc.Text = "0.00";
            txt_gross_profit.Text = "0.00";
            txt_total_cos_pf.Text = "0.00";
            txt_gp_cos_pf.Text = "0.00";
            txt_other_dir_exp.Text = "0.00";
            txt_dealer_margin.Text = "0.00";
            txt_add_dealer_inc.Text = "0.00";
            txt_less_sales_disc.Text = "0.00";
            txt_dp_amnt.Text = "0.00";
            txt_adi_perc.Text = "0.00";
            txt_total_breakdown.Text = "0.00";
            txt_unit_others.Text = "0.00";
            txt_unit_subsidy.Text = "0.00";
            txt_unit_margin.Text = "0.00";
            txt_delear_cnet.Text = "0.00";
            txt_srpnet_amnt.Text = "0.00";
            txt_actual_dp.Text = "0.00";
            txt_srp_amnt.Text = "0.00";
            
            try
            {
                dgv_itemlist.Rows.Clear();
            }
            catch (Exception) { }
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

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            isnew_item = true;
            lnno_last = 1;
            if (isverified == false)
            {
                frm_pclerkpass.ShowDialog();
            }
            else
            {
                MessageBox.Show("Already accessed to GP info.");
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
            String code = "";
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
                                /*dt = get_soitemlist(code);

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    itemcode = dt.Rows[i]["item_code"].ToString();
                                    itemqty = dt.Rows[i]["ord_qty"].ToString();

                                }*/
                            }
                            catch (Exception) { }

                            //db.DeleteOnTable("orlne", "ord_code='" + code + "'");
                            //db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");
                        }

                       disp_list();
                    }
                }
            }
        }
        private void btn_print_invoice_Click(object sender, EventArgs e)
        {
            //forPrint
            try
            {
                int indx = cbo_print_opt.SelectedIndex;
                if (indx == -1)
                {
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
                        /*

                        DataTable dt = dt = db.QueryBySQLCode("SELECT oh.*, v.*, b.* FROM rssys.orhdr oh LEFT JOIN rssys.vehicle_info v ON v.vin_no=oh.vehicle LEFT JOIN rssys.brand b ON b.brd_code=v.brand WHERE ord_code ='" + ord_code + "' LIMIT 1");

                        String vat_sales = "", vat_exsales = "", zero_sales = "", vat_amnt = "", vat_less = "", net_vat = "", total_sales = "", amnt_due = "", vat_add = "", total_amnt_due = ""
                            , sagent = "", terms = ""
                            , vin_no = dt.Rows[0]["vin_no"].ToString() // chassis no
                            , vehicle = dt.Rows[0]["vin_desc"].ToString()
                            , plate_no = dt.Rows[0]["plate_no"].ToString() // cs no
                            , engine_no = dt.Rows[0]["engine_no"].ToString()
                            , year_model = dt.Rows[0]["year_model"].ToString()
                            , color = dt.Rows[0]["color"].ToString()
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
                        if (indx == 0)
                        { // DELIVERY NOTE
                            rpt.print_vehicle_delivery_note_2(dt.Rows[0]["customer"].ToString());
                        }
                        else if (indx == 1)
                        { // Vehicle Sales Agreement
                            rpt.print_as_agreement(ord_code, terms, vin_no, vehicle, plate_no, engine_no, year_model, color, brand
                                , sagent, vat_sales, vat_exsales, zero_sales, vat_amnt, vat_less, net_vat, total_sales, amnt_due, vat_add, total_amnt_due, dt_trans);
                        }
                        else if (indx == 2)
                        { // Sales Invoice
                            rpt.print_as_invoice(ord_code, terms, vin_no, vehicle, plate_no, engine_no, year_model, color, brand
                                , sagent, vat_sales, vat_exsales, zero_sales, vat_amnt, vat_less, net_vat, total_sales, amnt_due, vat_add, total_amnt_due, dt_trans);
                        }

                        rpt.ShowDialog();*/
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

        private void btn_saveorder_Click(object sender, EventArgs e)
        {
            mainsave(false, "", true, "");
        }
        public void mainsave(Boolean isPayment, String pay_code, Boolean pending_status, String amt_tendered)
        {
            Boolean success = false;
            String notificationText = "";
            z_Notification notify = new z_Notification();
            String ord_code, out_code, debt_code, customer, ord_date, ord_qty, ord_amnt, disc_amnt, reference, mcardid, tax_amnt, amnt_due, user_id, t_date, t_time, loc, user_id2, t_date2, t_time2, trnx_date, disc_code, rep_code = "", agentid, amount_paid;
            String branch = GlobalClass.branch, transferred = "", stk_ref = "", stk_po_so = "", market_segment_id = "";
            String status, remark, loaner, vehicle, terms, dp_pct, condno, pn_amnt, amnt_financed, reg_chg, doc_stamp, add_on_rate, processing_fee, monthly_amort, max_sc, paid_amnt, dp_payment, dp_reference, dpamnt, rorder_status;
            String cashier = "";
            String pending = "Y";
            String col = "", val = "", col2 = "", val2 = "", col3 = "", val3 = "";
            String notifyadd = null;
            String table = "orhdr";
            String tableln = "orlne";

            if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the customer field.");
                m_auto_customer frm = new m_auto_customer(this, true);
                frm.ShowDialog();
            }
            else if (cbo_vehicle.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the vehicle field.");
                m_vehiclec_info frm = new m_vehiclec_info(this, true);
                frm.ShowDialog();
            }
            else if (cbo_loaner.Text == "")
            {
                MessageBox.Show("Please enter financer or select from the left side datagridview.");
                //m_vehiclec_info frm = new m_vehiclec_info(this, true);
                //frm.ShowDialog();
            }
            else if (cbo_status.SelectedIndex == -1 || cbo_status.Text == "")
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
            else if (cbo_dp_perc.SelectedIndex == -1)
            {
                MessageBox.Show("Please select DP % field.");
                cbo_dp_perc.DroppedDown = true;
            }
            else if (cbo_rorder_status.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Repair Order Status");
                cbo_rorder_status.DroppedDown = true;
            }
            //else if (dgv_itemlist.Rows.Count < 1)
            //{
            //     MessageBox.Show("No entry of Sales Item(s). Please add item(s).");
            //}
            else
            {
                return;

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

                //*stk_ref = stk_trns_type + "#" + ord_code;
                //*stk_po_so = ord_code;
                //*txt_reference.Text = stk_ref;
                //*reference = txt_reference.Text;
                //*ord_amnt = gm.toNormalDoubleFormat(txt_netcashprice.Text).ToString("0.00");
                //*disc_amnt = gm.toNormalDoubleFormat(txt_vat.Text).ToString("0.00");
                //amnt_tendered = gm.toNormalDoubleFormat(txt_csh_amttendered.Text).ToString("0.00");
                user_id = GlobalClass.username;
                t_date = db.get_systemdate("yyyy-MM-dd");
                t_time = db.get_systemtime();
                market_segment_id = cbo_marketsegment.SelectedValue.ToString();
                amount_paid = gm.toNormalDoubleFormat(amt_tendered).ToString("0.00");

                trnx_date = dtp_trnx_date.Value.ToString("yyyy-MM-dd");
                ord_date = trnx_date;

                loaner = cbo_loaner.Text;
                vehicle = cbo_vehicle.SelectedValue.ToString();
                terms = cbo_vehicle.SelectedValue.ToString();
                dp_pct = cbo_dp_perc.SelectedValue.ToString();
                status = cbo_status.Text;


                rorder_status = cbo_rorder_status.SelectedValue.ToString();
                //*
                //*dp_reference = txt_dp_reference.Text;
                //*condno = txt_condno.Text;

                //*doc_stamp = txt_doc_stamp.Text;
                remark = db.str_E(rtxt_remark.Text);

                //*pn_amnt = gm.toNormalDoubleFormat(txt_pn_amnt.Text).ToString("0.00");
                //*amnt_financed = gm.toNormalDoubleFormat(txt_amnt_financed.Text).ToString("0.00");
                //*reg_chg = gm.toNormalDoubleFormat(txt_reg_chg.Text).ToString("0.00");
                add_on_rate = gm.toNormalDoubleFormat(txt_add_on_rate.Text).ToString("0.00");
                //*processing_fee = gm.toNormalDoubleFormat(txt_processing_fee.Text).ToString("0.00");
                //*monthly_amort = gm.toNormalDoubleFormat(txt_monthly_amort.Text).ToString("0.00");
                //*max_sc = gm.toNormalDoubleFormat(txt_max_sc.Text).ToString("0.00");
                //*paid_amnt = gm.toNormalDoubleFormat(txt_paid_amnt.Text).ToString("0.00");
                //*dp_payment = gm.toNormalDoubleFormat(txt_dp_payment.Text).ToString("0.00");
                //*dpamnt = gm.toNormalDoubleFormat(txt_dpamnt.Text).ToString("0.00");


                String dp_perc, sales_exe, net_profit, payback, profit_b4_pay, _override, fuel_trans_cost, market_exp, finance_cost, total_dir_exp, unit_comm, referal, sales_inc, gross_profit, total_cos_pf, gp_cos_pf, other_dir_exp, dealer_margin, add_dealer_inc, less_sales_disc, dp_amnt, adi_perc, total_breakdown, unit_others, unit_subsidy, unit_margin, delear_cnet, srpnet_amnt, actual_dp, srp_amnt;

                dp_perc = (cbo_dp_perc.SelectedValue ?? "").ToString();
                sales_exe = (cbo_sales_exe.SelectedValue ?? "").ToString();
                net_profit = gm.toNormalDoubleFormat(txt_net_profit.Text).ToString("0.00");
                payback = gm.toNormalDoubleFormat(txt_payback.Text).ToString("0.00"); //gm.toNormalDoubleFormat().ToString("0.00")
                profit_b4_pay = gm.toNormalDoubleFormat(txt_profit_b4_pay.Text).ToString("0.00");
                market_exp = gm.toNormalDoubleFormat(txt_market_exp.Text).ToString("0.00");
                _override = gm.toNormalDoubleFormat(txt_override.Text).ToString("0.00");
                fuel_trans_cost = gm.toNormalDoubleFormat(txt_fuel_trans_cost.Text).ToString("0.00");
                finance_cost = gm.toNormalDoubleFormat(txt_finance_cost.Text).ToString("0.00");
                total_dir_exp = gm.toNormalDoubleFormat(txt_total_dir_exp.Text).ToString("0.00");
                unit_comm = gm.toNormalDoubleFormat(txt_unit_comm.Text).ToString("0.00");
                referal = gm.toNormalDoubleFormat(txt_referal.Text).ToString("0.00");
                sales_inc = gm.toNormalDoubleFormat(txt_sales_inc.Text).ToString("0.00");
                gross_profit =  gm.toNormalDoubleFormat(txt_gross_profit.Text).ToString("0.00");
                total_cos_pf = gm.toNormalDoubleFormat(txt_total_cos_pf.Text).ToString("0.00");
                gp_cos_pf = gm.toNormalDoubleFormat(txt_gp_cos_pf.Text).ToString("0.00");
                other_dir_exp = gm.toNormalDoubleFormat(txt_other_dir_exp.Text).ToString("0.00");
                dealer_margin = gm.toNormalDoubleFormat(txt_dealer_margin.Text).ToString("0.00");
                add_dealer_inc = gm.toNormalDoubleFormat(txt_add_dealer_inc.Text).ToString("0.00");
                less_sales_disc = gm.toNormalDoubleFormat(txt_less_sales_disc.Text).ToString("0.00");
                dp_amnt = gm.toNormalDoubleFormat(txt_dp_amnt.Text).ToString("0.00");
                adi_perc = gm.toNormalDoubleFormat(txt_adi_perc.Text).ToString("0.00");
                total_breakdown = gm.toNormalDoubleFormat(txt_total_breakdown.Text).ToString("0.00");
                unit_others = gm.toNormalDoubleFormat(txt_unit_others.Text).ToString("0.00");
                unit_subsidy = gm.toNormalDoubleFormat(txt_unit_subsidy.Text).ToString("0.00");
                unit_margin = gm.toNormalDoubleFormat(txt_unit_margin.Text).ToString("0.00");
                delear_cnet = gm.toNormalDoubleFormat(txt_delear_cnet.Text).ToString("0.00");
                srpnet_amnt = gm.toNormalDoubleFormat(txt_srpnet_amnt.Text).ToString("0.00");
                actual_dp = gm.toNormalDoubleFormat(txt_actual_dp.Text).ToString("0.00");
                srp_amnt = gm.toNormalDoubleFormat(txt_srp_amnt.Text).ToString("0.00");


                //col = "dp_perc, sales_exe, net_profit, payback, profit_b4_pay, _override, fuel_trans_cost, market_exp, finance_cost, total_dir_exp, unit_comm, referal, sales_inc, gross_profit, total_cos_pf, gp_cos_pf, other_dir_exp, dealer_margin, add_dealer_inc, less_sales_disc, dp_amnt, adi_perc, total_breakdown, unit_others, unit_subsidy, unit_margin, delear_cnet, srpnet_amnt, actual_dp, srp_amnt";
                //val = "'"+dp_perc+"', '"+sales_exe+"', '"+net_profit+"', '"+payback+"', '"+profit_b4_pay+"', '"+_override+"', '"+fuel_trans_cost+"', '"+market_exp+"', '"+finance_cost+"', '"+total_dir_exp+"', '"+unit_comm+"', '"+referal+"', '"+sales_inc+"', '"+gross_profit+"', '"+total_cos_pf+"', '"+gp_cos_pf+"', '"+other_dir_exp+"', '"+dealer_margin+"', '"+add_dealer_inc+"', '"+less_sales_disc+"', '"+dp_amnt+"', '"+adi_perc+"', '"+total_breakdown+"', '"+unit_others+"', '"+unit_subsidy+"', '"+unit_margin+"', '"+delear_cnet+"', '"+srpnet_amnt+"', '"+actual_dp+"', '"+srp_amnt+"'";

                //col = "dp_perc='" + dp_perc + "', sales_exe='" + sales_exe + "', net_profit='" + net_profit + "', payback='" + payback + "', profit_b4_pay='" + profit_b4_pay + "', _override='" + _override + "', fuel_trans_cost='" + fuel_trans_cost + "', market_exp='" + market_exp + "', finance_cost='" + finance_cost + "', total_dir_exp='" + total_dir_exp + "', unit_comm='" + unit_comm + "', referal='" + referal + "', sales_inc='" + sales_inc + "', gross_profit='" + gross_profit + "', total_cos_pf='" + total_cos_pf + "', gp_cos_pf='" + gp_cos_pf + "', other_dir_exp='" + other_dir_exp + "', dealer_margin='" + dealer_margin + "', add_dealer_inc='" + add_dealer_inc + "', less_sales_disc='" + less_sales_disc + "', dp_amnt='" + dp_amnt + "', adi_perc='" + adi_perc + "', total_breakdown='" + total_breakdown + "', unit_others='" + unit_others + "', unit_subsidy='" + unit_subsidy + "', unit_margin='" + unit_margin + "', delear_cnet='" + delear_cnet + "', srpnet_amnt='" + srpnet_amnt + "', actual_dp='" + actual_dp + "', srp_amnt='" + srp_amnt + "'";


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
                //*amnt_due = gm.toNormalDoubleFormat(txt_netcashprice.Text).ToString("0.00");
                //*tax_amnt = (gm.toNormalDoubleFormat(amnt_due) * db.get_outlet_govt_pct(out_code) / 100).ToString("0.00");

                col2 = ", user_id2, t_date2, t_time2, tax_amnt, amnt_due, pay_code, cashier, amnt_tendered";
                val2 = ", '" + user_id2 + "', '" + t_date2 + "', '" + t_time2 + "', '" + "tax_amnt" + "', '" + "amnt_due" + "', '" +
                    pay_code + "', '" + cashier + "', '" + amount_paid + "'";


                col3 = ",status,remark, loaner, vehicle, terms, dp_pct, condno, pn_amnt, amnt_financed, reg_chg, doc_stamp, add_on_rate, processing_fee, monthly_amort, max_sc, paid_amnt, dp_payment, dp_reference, dpamnt,rorder_status";
                val3 = ",'" + status + "'," + remark + ",'" + loaner + "','" + vehicle + "','" + terms + "','" + dp_pct + "','" + "condno" + "','" + "pn_amnt" + "','" + "amnt_financed" + "','" + "reg_chg" + "','" + "doc_stamp" + "','" + add_on_rate + "','" + "processing_fee" + "','" + "monthly_amort" + "','" + "max_sc" + "','" + "paid_amnt" + "','" + "dp_payment" + "','" + "dp_reference" + "','" + "dpamnt" + "','" + rorder_status + "'";

                if (isnew == false)
                {
                    col = ", tax_amnt='" + "tax_amnt" + "', amnt_due='" + "amnt_due" + "', user_id2='" + user_id2 + "', t_date2='" + t_date2 + "', t_time2='" + t_time2 + "', pay_code='" + pay_code + "', cashier='" + cashier + "', amnt_tendered='" + amount_paid + "'";
                }

                if (isnew)
                {

                    if (rorder_status == /*rocode_released*/ "sese")
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to Release this Unit ?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            //*rorder_status = rocode_released;
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

                        notificationText = "has added: ";
                        //*stk_ref = stk_trns_type + "#" + ord_code;
                        stk_po_so = ord_code;
                        //*txt_reference.Text = stk_ref;
                        //*reference = txt_reference.Text;
                        //*ord_code = db.get_ord_code(out_code);

                        col = "ord_code, out_code, debt_code, customer, ord_date, ord_amnt, trnx_date, disc_amnt, reference, mcardid, user_id, t_date, t_time, loc,rep_code, agentid, branch,market_segment_id, pending" + col2 + col3;
                        val = "'" + ord_code + "', '" + out_code + "', '" + debt_code + "', " + db.str_E(customer) + ", '" + ord_date + "', '" + "ord_amnt" + "', '" + trnx_date + "', '" + "disc_amnt" + "', " + db.str_E("reference") + ", '" + mcardid + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + loc + "', '" + rep_code + "', '" + agentid + "', '" + branch + "','" + market_segment_id + "', '" + pending + "'" + val2 + val3;

                        if (db.InsertOnTable(table, col, val))
                        {
                            try
                            {
                                //stk_ref = stk_trns_type + "#" + ord_code;
                                //notifyadd = add_items(tableln, ord_code, trnx_date, stk_ref, stk_po_so, loc);

                                //if (String.IsNullOrEmpty(notifyadd) == false)
                                //{
                                notificationText += String.IsNullOrEmpty(notifyadd) ? "" : notifyadd;
                                notificationText += Environment.NewLine + " with SO#" + ord_code;
                                notify.saveNotification(notificationText, "Sales Outlet");
                                //db.set_ord_code_nextno(ord_code, out_code);
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
                }
                else
                {

                    if (/*isRepair*/false)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to Release this Unit ?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            //rorder_status = rocode_released;
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

                        notificationText = "has updated: ";
                        col = "ord_code='" + ord_code + "', out_code='" + out_code + "', debt_code='" + debt_code + "', customer='" + customer + "', ord_date='" + ord_date + "', ord_amnt='" + "ord_amnt" + "', disc_amnt='" + "disc_amnt" + "', reference='" + "reference" + "', mcardid='" + mcardid + "', user_id='" + user_id + "', t_date='" + t_date + "', t_time='" + t_time + "', loc='" + loc + "', trnx_date='" + trnx_date + "', pay_code='" + pay_code + "', cashier='" + cashier + "', rep_code='" + rep_code + "', agentid='" + agentid + "', branch='" + branch + "', pending='" + pending + "' ,payment='" + amount_paid + "',loaner='" + loaner + "', vehicle='" + vehicle + "', terms='" + terms + "', dp_pct='" + dp_pct + "', condno='" + "condno" + "', pn_amnt='" + "pn_amnt" + "', amnt_financed='" + "amnt_financed" + "', reg_chg='" + "reg_chg" + "', doc_stamp='" + "doc_stamp" + "', add_on_rate='" + add_on_rate + "', processing_fee='" + "processing_fee" + "', monthly_amort='" + "monthly_amort" + "', max_sc='" + "max_sc" + "', paid_amnt='" + "paid_amnt" + "', dp_payment='" + "dp_payment" + "', dp_reference='" + "dp_reference" + "', dpamnt='" + "dpamnt" + "',remark=" + remark + ",status='" + status + "',rorder_status='" + rorder_status + "'";


                        if (db.UpdateOnTable(table, col, "ord_code='" + ord_code + "'"))
                        {
                            db.DeleteOnTable(tableln, "ord_code='" + ord_code + "'");
                            db.DeleteOnTable("stkcrd", "po_so='" + ord_code + "' AND trn_type='" + ""/*stk_trns_type*/ + "'");
                            try
                            {

                                //stk_ref = stk_trns_type + "#" + ord_code;
                                //notifyadd = add_items(tableln, ord_code, trnx_date, stk_ref, stk_po_so, loc);

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
                    disp_list();
                    goto_win1();
                }
            }
        }


        private void btn_exit_Click(object sender, EventArgs e)
        {
            goto_win1();
            disp_list();
            isverified = false;
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
            /*if (isRepair)
            {
                //WHERE = WHERE + " o.rorder_status='" + ro_stat_code + "' AND ";
                WHERE = WHERE + " ( o.rorder_status='" + rocode_ready + "' OR o.rorder_status='" + rocode_released + "' ) AND ";
            }*/
            //o.trnx_date AS t_date
            return db.QueryBySQLCode("SELECT ro.ro_stat_desc,o.out_code,o.ord_code,o.customer,o.payment,o.cancel,o.net_amnt,o.ord_date,o.t_date,o.tax_amnt,o.pending,o.amnt_due,o.disc_amnt, o.user_id,o.loc,o.user_id2,o.rep_code, o.cashier,o.gross_amnt,o.ord_amnt,o.pay_code FROM " + db.schema + ".orhdr o LEFT JOIN  " + db.schema + ".whouse w ON w.whs_code=o.loc LEFT JOIN " + db.schema + ".ro_status ro ON ro.ro_stat_code=o.rorder_status WHERE " + WHERE + " (o.t_date BETWEEN '" + frm.ToString("yyyy-MM-dd") + "' AND '" + to.ToString("yyyy-MM-dd") + "') ORDER BY o.ord_code");
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
        private void AdjustColumnOrder()
        {
            dgv_list.AutoGenerateColumns = false;
            dgv_list.Columns["dgvl_code"].DisplayIndex = 0;//rorder_status
            dgv_list.Columns["dgvl_rorder_status"].DisplayIndex = 1;
            dgv_list.Columns["dgvl_customer"].DisplayIndex = 2;
            dgv_list.Columns["dgvl_ord_date"].DisplayIndex = 3;
            dgv_list.Columns["dgvl_trnx_date"].DisplayIndex = 4;
            dgv_list.Columns["dgvl_netamnt"].DisplayIndex = 5;
            dgv_list.Columns["dgvl_tax_amnt"].DisplayIndex = 6;
            dgv_list.Columns["dgvl_gross_amnt"].DisplayIndex = 7;
            dgv_list.Columns["dgvl_disc_amnt"].DisplayIndex = 8;
            dgv_list.Columns["dgvl_ord_amnt"].DisplayIndex = 9;
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

            if (isnew)
            {
                frm_clear();

                try { outcode = cbo_outlet_olist.SelectedValue.ToString(); }
                catch { }

                if (isverified)
                {
                    /*
                    if (direct_sales)
                    {
                        m_auto_customer frm = new m_auto_customer(this, true);
                        frm.ShowDialog();
                        goto_win2();
                    }
                    else if (loan_application)
                    {
                        isverified = false;
                        s_Auto_Sales_Loan_Status frm = new s_Auto_Sales_Loan_Status(this);
                        frm.ShowDialog();
                    }*/
                    m_auto_customer frm = new m_auto_customer(this, true);
                    frm.ShowDialog();
                    goto_win2();
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
                    /*
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        txt_reference.Text = dt.Rows[i]["reference"].ToString();
                        cbo_customer.Text = dt.Rows[i]["customer"].ToString();
                        txt_servex.Text = dt.Rows[i]["mcardid"].ToString();
                        dtp_trnx_date.Value = gm.toDateValue(dt.Rows[i]["trnx_date"].ToString());

                        txt_netcashprice.Text = dt.Rows[i]["ord_amnt"].ToString();
                        txt_vat.Text = dt.Rows[i]["disc_amnt"].ToString();
                        txt_total_dp.Text = dt.Rows[i]["payment"].ToString();

                        dtp_appdate.Value = gm.toDateValue(dt.Rows[i]["ord_date"].ToString());
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                        cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();

                        //update

                        cbo_rorder_status.SelectedValue = dt.Rows[i]["rorder_status"].ToString();

                        cbo_loaner.Text = dt.Rows[i]["loaner"].ToString();//
                        cbo_status.Text = dt.Rows[i]["status"].ToString();
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                        cbo_sa.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                        cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                        cbo_cashier.SelectedValue = dt.Rows[i]["cashier"].ToString();
                        rtxt_remark.Text = dt.Rows[i]["remark"].ToString();

                        cbo_vehicle.SelectedValue = dt.Rows[i]["vehicle"].ToString();//

                        //txt_netcashprice.Text = dt.Rows[i]["net_amnt"].ToString();
                        txt_condno.Text = dt.Rows[i]["condno"].ToString();//
                        cbo_dp_pct.SelectedValue = dt.Rows[i]["dp_pct"].ToString();//
                        txt_dpamnt.Text = dt.Rows[i]["dpamnt"].ToString();//
                        txt_pn_amnt.Text = dt.Rows[i]["pn_amnt"].ToString();//
                        txt_amnt_financed.Text = dt.Rows[i]["amnt_financed"].ToString();//
                        txt_reg_chg.Text = dt.Rows[i]["reg_chg"].ToString();//
                        cbo_terms.Text = dt.Rows[i]["terms"].ToString();//
                        txt_doc_stamp.Text = dt.Rows[i]["doc_stamp"].ToString();//
                        txt_add_on_rate.Text = dt.Rows[i]["add_on_rate"].ToString();//
                        txt_processing_fee.Text = dt.Rows[i]["processing_fee"].ToString();//
                        txt_monthly_amort.Text = dt.Rows[i]["monthly_amort"].ToString();//
                        txt_max_sc.Text = dt.Rows[i]["max_sc"].ToString();//
                        txt_reference.Text = dt.Rows[i]["reference"].ToString();
                        txt_paid_amnt.Text = dt.Rows[i]["paid_amnt"].ToString();//
                        txt_dp_payment.Text = dt.Rows[i]["dp_payment"].ToString();//
                        txt_dp_reference.Text = dt.Rows[i]["dp_reference"].ToString();//
                     
                  
           
                     
                     
                     
            cbo_dp_perc.SelectedValue = dt.Rows[i]["dp_perc"].ToString();
            cbo_sales_exe.SelectedValue = dt.Rows[i]["sales_exe"].ToString();
            txt_net_profit.Text = dt.Rows[i]["net_profit"].ToString();
            txt_payback.Text = dt.Rows[i]["payback"].ToString();
            txt_profit_b4_pay.Text = dt.Rows[i]["profit_b4_pay"].ToString();
            txt_market_exp.Text = dt.Rows[i]["market_exp"].ToString();
            txt_override.Text = dt.Rows[i]["_override"].ToString();
            txt_fuel_trans_cost.Text = dt.Rows[i]["fuel_trans_cost"].ToString();
            txt_finance_cost.Text = dt.Rows[i]["finance_cost"].ToString();
            txt_total_dir_exp.Text = dt.Rows[i]["total_dir_exp"].ToString();
            txt_unit_comm.Text = dt.Rows[i]["unit_comm"].ToString();
            txt_referal.Text = dt.Rows[i]["referal"].ToString();
            txt_sales_inc.Text = dt.Rows[i]["sales_inc"].ToString();
            txt_gross_profit.Text = dt.Rows[i]["gross_profit"].ToString();
            txt_total_cos_pf.Text = dt.Rows[i]["total_cos_pf"].ToString();
            txt_gp_cos_pf.Text = dt.Rows[i]["gp_cos_pf"].ToString();
            txt_other_dir_exp.Text = dt.Rows[i]["other_dir_exp"].ToString();
            txt_dealer_margin.Text = dt.Rows[i]["dealer_margin"].ToString();
            txt_add_dealer_inc.Text = dt.Rows[i]["add_dealer_inc"].ToString();
            txt_less_sales_disc.Text = dt.Rows[i]["less_sales_disc"].ToString();
            txt_dp_amnt.Text = dt.Rows[i]["dp_amnt"].ToString();
            txt_adi_perc.Text = dt.Rows[i]["adi_perc"].ToString();
            txt_total_breakdown.Text = dt.Rows[i]["total_breakdown"].ToString();
            txt_unit_others.Text = dt.Rows[i]["unit_others"].ToString();
            txt_unit_subsidy.Text = dt.Rows[i]["unit_subsidy"].ToString();
            txt_unit_margin.Text = dt.Rows[i]["unit_margin"].ToString();
            txt_delear_cnet.Text = dt.Rows[i]["delear_cnet"].ToString();
            txt_srpnet_amnt.Text = dt.Rows[i]["srpnet_amnt"].ToString();
            txt_actual_dp.Text = dt.Rows[i]["actual_dp"].ToString();
            txt_srp_amnt.Text = dt.Rows[i]["srp_amnt"].ToString();
                     
                    }
                     * */




                }
                vehicle_info_entry();
                //disp_itemlist(code);
                goto_win2();
            }

            //frm_enable(true);
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
        private void txt_TextChanged_Compute_net_profit(object sender, EventArgs e)
        {
            compute_all_to_netprofit();
        }
        private void cbo_dp_perc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean error = false;
            try{
                Double.Parse(cbo_dp_perc.Text.Substring(0, 2));
            }
            catch { error = true; }
            try{
                if (error){
                    Double.Parse(cbo_dp_perc.Text.Substring(0, 3));
                }
            }
            catch { error = true; }

            if (!error)
            {
                compute_all_to_netprofit();
            }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            z_enter_sales_item frm_si = new z_enter_sales_item(this, true, "", "", lnno_last);
            frm_si.ShowDialog();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            int r = 0;

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {

                    r = dgv_itemlist.CurrentRow.Index;

                    z_enter_sales_item frm_si = new z_enter_sales_item(this, false, "", "", r);

                    frm_si.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
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


        public void dgv_salesitem(DataTable dt, Boolean isnewitem)
        {
            int i = 0;

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

            if (isnew_item)
            {
                inc_lnno();
            }

            compute_all_to_netprofit();
        }

        private void compute_all_to_netprofit()
        { 
            try
            {
                double conts_num = 620000.00;
                double perc_num = 1.12;

                double srp_amt = 0.0;
                try { srp_amt = gm.toNormalDoubleFormat(txt_srp_amnt.Text); }
                catch { }
                double actual_dp = 0.0;
                try { actual_dp = gm.toNormalDoubleFormat(txt_actual_dp.Text); }
                catch { }

                double srp_amt_perc80 = (srp_amt * (80.0 / 100));

                double dp_perc = 0.0;
                try { dp_perc = Double.Parse(cbo_dp_perc.Text.Substring(0, 2)); }
                catch { }
                try {
                    if (dp_perc == 0){
                        dp_perc = Double.Parse(cbo_dp_perc.Text.Substring(0, 3));
                    }
                }catch { }


                txt_dp_amnt.Text = gm.toAccountingFormat((srp_amt * (dp_perc / 100)).ToString("0.00"));

                txt_srpnet_amnt.Text = gm.toAccountingFormat((srp_amt / perc_num).ToString("0.00"));
                txt_delear_cnet.Text = gm.toAccountingFormat((conts_num / perc_num).ToString("0.00"));

                txt_dealer_margin.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_srpnet_amnt.Text) - gm.toNormalDoubleFormat(txt_delear_cnet.Text)).ToString("0.00"));

                try { txt_dealer_margin.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_srpnet_amnt.Text) - gm.toNormalDoubleFormat(txt_delear_cnet.Text)).ToString("0.00")); }
                catch { txt_dealer_margin.Text = "0.00"; }

                double unitm = 0.0;
                try { unitm = gm.toNormalDoubleFormat(txt_unit_margin.Text); }
                catch { }
                double units = 0.0;
                try { units = gm.toNormalDoubleFormat(txt_unit_subsidy.Text); }
                catch { }
                double unito = 0.0;
                try { unito = gm.toNormalDoubleFormat(txt_unit_others.Text); }
                catch { }

                txt_total_breakdown.Text = gm.toAccountingFormat((unitm + units + unito).ToString("0.00"));

                try { txt_vat_exe.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_total_breakdown.Text) / perc_num).ToString("0.00")); }
                catch { txt_vat_exe.Text = "0.00"; }

                double adi_perc = 0.0;
                try { adi_perc = gm.toNormalDoubleFormat(txt_adi_perc.Text); }
                catch { }

                txt_add_dealer_inc.Text = gm.toAccountingFormat((srp_amt_perc80 * (adi_perc / 100)).ToString("0.00"));

                try { txt_less_sales_disc.Text = gm.toAccountingFormat(((gm.toNormalDoubleFormat(txt_dp_amnt.Text) - actual_dp) / perc_num).ToString("0.00")); }
                catch { txt_less_sales_disc.Text = "0.00"; }

                try { txt_gross_profit.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_dealer_margin.Text) + gm.toNormalDoubleFormat(txt_add_dealer_inc.Text) - gm.toNormalDoubleFormat(txt_less_sales_disc.Text)).ToString("0.00")); }
                catch { txt_gross_profit.Text = "0.00"; }

                double accessories = 0.00; 
                for (int i = 0; i < dgv_itemlist.Rows.Count; i++) {
                    try {
                        accessories += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", i].ToString());
                    }catch{}
                }

                try { txt_total_cos_pf.Text = gm.toAccountingFormat((accessories + gm.toNormalDoubleFormat(txt_lto_reg_amnt.Text) + gm.toNormalDoubleFormat(txt_tpl_amnt.Text) + gm.toNormalDoubleFormat(txt_compre_ins.Text) + gm.toNormalDoubleFormat(txt_chat_mortgage.Text)).ToString("0.00")); }
                catch { txt_total_cos_pf.Text = "0.00"; }

                try { txt_gp_cos_pf.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_gross_profit.Text) - gm.toNormalDoubleFormat(txt_total_cos_pf.Text)).ToString("0.00")); }
                catch { txt_gp_cos_pf.Text = "0.00"; }

                try { txt_override.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_unit_comm.Text) + gm.toNormalDoubleFormat(txt_sales_inc.Text) + gm.toNormalDoubleFormat(txt_referal.Text)).ToString("0.00")); }
                catch { txt_override.Text = "0.00"; }
                //gm.toAccountingFormat() 

                try { txt_total_dir_exp.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_override.Text) + gm.toNormalDoubleFormat(txt_fuel_trans_cost.Text) + gm.toNormalDoubleFormat(txt_market_exp.Text) + gm.toNormalDoubleFormat(txt_finance_cost.Text) + gm.toNormalDoubleFormat(txt_other_dir_exp.Text)).ToString("0.00")); }
                catch { txt_total_dir_exp.Text = "0.00"; }

                try { txt_profit_b4_pay.Text = gm.toAccountingFormat((gm.toNormalDoubleFormat(txt_gp_cos_pf.Text) - gm.toNormalDoubleFormat(txt_total_dir_exp.Text)).ToString("0.00")); }
                catch { txt_profit_b4_pay.Text = "0.00"; }

                double payback = gm.toNormalDoubleFormat(txt_payback.Text);

                try { txt_net_profit.Text = gm.toAccountingFormat((payback + gm.toNormalDoubleFormat(txt_profit_b4_pay.Text)).ToString("0.00")); }
                catch { txt_net_profit.Text = "0.00"; }

            } 
            catch { }
        }
        private void inc_lnno()
        {
            lnno_last = lnno_last + 1;
        }

        private void btn_customer_refresh_Click(object sender, EventArgs e)
        {
            m_auto_customer frm = new m_auto_customer(this, true);
            frm.ShowDialog();
        }

        private void btn_vehicle_Click(object sender, EventArgs e)
        {
            m_vehiclec_info frm = new m_vehiclec_info(this, true);
            frm.ShowDialog();
        }
        public void set_vehicle_frm(String custcode, String custname)
        {

            try { cbo_vehicle.Items.Clear(); }
            catch { }

            gc.load_vehicle_info(cbo_vehicle);
            cbo_vehicle.SelectedValue = custcode;

            cbo_sales_exe.Text = cbo_vehicle.Text;
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
            //dgv_loaner_list.Rows.Clear();
            //disp_loaner(cbo_customer.SelectedValue.ToString());
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



    }
}
