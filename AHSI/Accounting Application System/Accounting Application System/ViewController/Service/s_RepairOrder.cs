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
    public partial class s_RepairOrder : Form
    {
        private z_clerkpassword frm_pclerkpass;
        private z_payment frm_payment;
        private z_enter_sales_item frm_si;
        private z_so_list frm_solist;
        private m_customers frm_cust;
        private m_company frm_dealer;
        private int ln = 1;
        private Boolean isverified = false, isWin2Active = false;
        Boolean seltbp = false;
        private Boolean isnew = false;
        private Boolean isnew_item = true;
        String paycode="", paydesc="", ln_amnt="", type="P", reference="", rep_code="";
        private dbSales db;
        private GlobalClass gc;
        String sub_col2 = "",sub_val2="",sub_ord_code2="",sub_item_code2="",sub_item_code="";
        private GlobalMethod gm;
        private int count;
        int lnno_last = 1;
        String stk_trns_type = "RO";
        Boolean isRepair = false, isCashier = false;
        private int p;
        Boolean isRelease = false;

        public s_RepairOrder()
        {
            InitializeComponent();
            init_load();
            
        }

        public s_RepairOrder(Boolean cashier)
        {
            InitializeComponent();
            isCashier = cashier;
            init_load();            
        }

        private void init_load()
        {
            String sMonth = DateTime.Now.ToString("MM");
            db = new dbSales();
            frm_pclerkpass = new z_clerkpassword(this);
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_technician(cbo_technician);
            gc.load_whouse(cbo_whsname);
            gc.load_outlet_repair(cbo_outlet, true);
            gc.load_outlet_repair(cbo_outlet_olist, true);
            gc.load_customer(cbo_customer);
            gc.load_salesagent(cbo_agent);
            gc.load_salesclerk(cbo_clerk);
            gc.load_salesclerk(cbo_cashier);
            gc.load_company_acct(cbo_dealer);
            gc.load_vehicle_info(cbo_vehicle);
            gc.load_marketsegment(cbo_marketsegment);
            gc.load_servicetype(cbo_servicetype);
            gc.load_repair_orderstatus(cbo_status);
            gc.load_customer(cbo_insurance);
            gc.load_brand(cbo_brand);
            cbo_insurance.Enabled = false;
            isnew = true;
            frm_enable(false);
            frm_reload();
            isempty(count);
            
            dtp_frm.Value = gm.toDateValue(db.get_systemdate("yyyy-" + sMonth + "-01"));

            try
            {
                cbo_outlet_olist.SelectedIndex = 0;
            }
            catch { }

            if(isCashier)
            {
                btn_additem.Text = "Add Payment";
                btn_upditem.Text = "Update Payment";
                btn_delitem.Text = "Void Payment";

                tbpg_items.SelectedTab = tpgi_payments;
                tpgi_payments.Show();
                btn_new.Enabled = false;
            }
            else
            {
                groupBox3.Visible = true;
                btn_release.Hide();
            }
            disp_list();
        }

        public void frm_enable_main_opt(Boolean flag)
        {
            if(isCashier == false)
            {
                btn_new.Enabled = flag;
            }

            btn_upd.Enabled = flag;
            btn_cancel.Enabled = flag;
            btn_print.Enabled = flag;
        }

        private void s_RepairOrder_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
                //this.ActiveControl = txt_searchcatch
            }
            catch (Exception) { }
        }

        private void goto_win_outlet()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option;
            tpg_option.Show();

            seltbp = false;

            frm_enable_main_opt(false);
        }

        private void btn_chg_oulet_Click(object sender, EventArgs e)
        {
            goto_win_outlet();
        }
        
        private void btn_vehicle_Click(object sender, EventArgs e)
        {
            m_vehiclec_info frm = new m_vehiclec_info(this, true);
            frm.ShowDialog();
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

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            AdjustColumnOrder();
        }

        private void disp_payment_list(String ord_code)
        {
            DataTable dt = this.get_paymentlist(ord_code);

            try { dgv_payment.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_payment.Rows.Add();

                    dgv_payment["dgvlp_ln_num", i].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_payment["dgvlp_pay_code", i].Value = dt.Rows[i]["pay_code"].ToString();
                    dgv_payment["dgvlp_pay_desc", i].Value = dt.Rows[i]["mp_desc"].ToString();
                    dgv_payment["dgvlp_reference", i].Value = dt.Rows[i]["reference"].ToString();
                    dgv_payment["dgvlp_ln_amnt", i].Value = dt.Rows[i]["ln_amnt"].ToString();
                    dgv_payment["dgvlp_rep_code", i].Value = dt.Rows[i]["rep_code"].ToString();
                    dgv_payment["dgvlp_ln_type", i].Value = dt.Rows[i]["ln_type"].ToString();
                    dgv_payment["dgvlp_chargeto", i].Value = dt.Rows[i]["chargeto"].ToString();

                    dgv_payment["dgvlp_t_date", i].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                    dgv_payment["dgvlp_t_time", i].Value = dt.Rows[i]["t_time"].ToString();
                    //dgv_itemlist["dgvlp_clerk", i].Value = db.get_colval("repmst", "rep_name", "rep_code='" + dt.Rows[i]["rep_code"].ToString() + "'");
                    //dgv_itemlist["dgvlp_clerkid", i].Value = dt.Rows[i]["rep_code"].ToString();
                }
            }
            catch (Exception)
            { }
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

            return db.QueryBySQLCode("SELECT o.out_code,o.ord_code,CASE WHEN COALESCE(o.cancel,'')='Y' THEN 'CANCELLED-'||m.d_name ELSE m.d_name END AS customer,o.payment,o.cancel,o.net_amnt,o.ord_date,o.trnx_date AS t_date,to_char(o.t_date,'MM/DD/YYYY')||' '||o.t_time as \"SysDate\",o.t_date2,o.tax_amnt,o.pending,o.amnt_due,o.disc_amnt, o.user_id,o.loc,o.user_id2,o.rep_code, o.promise_date, o.promise_time, o.cashier,o.gross_amnt,o.ord_amnt,o.pay_code,o.debt_code as cust_no FROM " + db.schema + ".orhdr o LEFT JOIN  " + db.schema + ".whouse w ON w.whs_code=o.loc LEFT JOIN  " + db.schema + ".m06 m ON m.d_code=o.debt_code WHERE " + WHERE + " trnx_date BETWEEN '" + frm.ToString("yyyy-MM-dd") + "' AND '" + to.ToString("yyyy-MM-dd") + "' ORDER BY o.ord_date,o.t_date,o.t_time");
            //m06
        }
        
        public DataTable get_soitemlist(String ord_code)
        {
            DataTable dt = null;

            try
            {
                dt = db.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit_desc FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id  WHERE ol.ord_code='" + ord_code + "' AND ol.ln_num > '0' ORDER BY " + db.castToInteger("ol.ln_num") + " ASC");
            }
            catch { }

            return dt;
        }

        public DataTable get_paymentlist(String ord_code)
        {
            DataTable dt = null;

            try
            {
                dt = db.QueryBySQLCode("SELECT o.ln_num,o.ln_amnt,o.rep_code,o.reference,o.pay_code,m.mp_desc,o.ln_type, o.chargeto,  o.trnx_date, o.t_time FROM rssys.orlne o LEFT JOIN rssys.m10 m ON o.pay_code=m.mp_code WHERE o.ord_code='" + ord_code + "' AND ln_type='P' ORDER BY ln_num ASC");
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

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
                    //dgv_itemlist["dgvli_regprice", i].Value = dt.Rows[i]["price"].ToString(); //1
                    dgv_itemlist["dgvli_sellprice", i].Value = gm.toAccountingFormat(dt.Rows[i]["price"].ToString()); // 1
                    dgv_itemlist["dgvli_lnamt", i].Value = gm.toAccountingFormat(dt.Rows[i]["ln_amnt"].ToString()); //1
                    dgv_itemlist["dgvli_taxamt", i].Value = gm.toAccountingFormat(dt.Rows[i]["ln_tax"].ToString()); //1
                    //dgvli_unit
                    dgv_itemlist["dgvli_net", i].Value = gm.toAccountingFormat(dt.Rows[i]["net_amount"].ToString());
                    dgv_itemlist["dgvli_unit", i].Value = dt.Rows[i]["unit_desc"].ToString();
                    dgv_itemlist["dgvli_discamt", i].Value = gm.toAccountingFormat(dt.Rows[i]["disc_amt"].ToString()); //1
                    dgv_itemlist["dgvli_disccode", i].Value = dt.Rows[i]["disc_code"].ToString(); //1
                    dgv_itemlist["dgvli_discuser", i].Value = dt.Rows[i]["disc_user"].ToString(); //1
                    dgv_itemlist["dgvli_discreason", i].Value = dt.Rows[i]["disc_reason"].ToString(); //1

                    dgv_itemlist["dgvli_unitid", i].Value = dt.Rows[i]["unit"].ToString(); //1
                    dgv_itemlist["dgvli_remarks", i].Value = dt.Rows[i]["reference"].ToString(); //1

                    dgv_itemlist["dgvli_t_date", i].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "");
                    dgv_itemlist["dgvli_t_time", i].Value = dt.Rows[i]["t_time"].ToString();
                    dgv_itemlist["dgvli_clerk", i].Value = db.get_colval("repmst", "rep_name", "rep_code='" + dt.Rows[i]["rep_code"].ToString() + "'");
                    dgv_itemlist["dgvli_clerkid", i].Value = dt.Rows[i]["rep_code"].ToString();
                    //", "*", "", "ORDER BY rep_code ASC


                    DataTable checkdt = db.QueryBySQLCode("SELECT i2.*, or2.* FROM rssys.orlne2 or2 LEFT JOIN rssys.items2 i2 ON or2.item_code_ol = i2.item_code WHERE or2.ord_code='" + code + "' AND i2.item_code='" + dgv_itemlist["dgvli_itemcode", i].Value .ToString()+ "'");
                    if (checkdt.Rows.Count > 0)
                    {
                        disp_sub(code, dgv_itemlist["dgvli_itemcode", i].Value.ToString(), true, dgv_itemlist["dgvli_lnno", i].Value.ToString());

                    }
                    else
                    {
                        disp_sub(code, dgv_itemlist["dgvli_itemcode", i].Value.ToString(), false, dgv_itemlist["dgvli_lnno", i].Value.ToString());                 
                    }
                    
                    lnno_last = int.Parse(dt.Rows[i]["ln_num"].ToString());
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            inc_lnno();
        }

        public void get_payment_data(String paycode , String paydesc, String ln_amnt, String type, String reference)
        {

            this.paycode = paycode;
            this.paydesc = paydesc;
            this.ln_amnt = ln_amnt;
            this.type = type;
            this.reference = reference;
            this.rep_code = cbo_cashier.SelectedValue.ToString();
                
        }


        public void frm_reload()
        {
            frm_reset();
            frm_clear();
            total_amountdue();
            frm_enable(true);
        }

        public void frm_enable(Boolean flag)
        {
            /*
            btn_additem.Enabled = flag;
            btn_upditem.Enabled = flag;
            btn_delitem.Enabled = flag;
            btn_saveorder.Enabled = flag;

            txt_servex.Enabled = flag;
            txt_search.Enabled = flag;
            cbo_agent.Enabled = flag;
            cbo_marketsegment.Enabled = flag;
            //cbo_customer.Enabled = flag;
            cbo_agent.Enabled = flag;*/

            btn_additem.Enabled = flag;
            btn_upditem.Enabled = flag;
            btn_delitem.Enabled = flag;
            btn_saveorder.Enabled = flag;

            txt_servex.Enabled = flag;
            cbo_agent.Enabled = flag;
            cbo_marketsegment.Enabled = flag;
            //cbo_customer.Enabled = flag;
            cbo_agent.Enabled = flag;

            //if (isCashier == false)
           // {
             //  btn_payadd.Enabled = false;

          //  }

        }

        private void frm_reset()
        {
            isnew = true;
            isverified = false;
        }

        private void frm_clear()
        {
            txt_totaltax.Text = "0.00";
            cbo_servicetype.SelectedIndex = -1;
            cbo_technician.SelectedIndex = -1;
            cbo_clerk.SelectedIndex = -1;
            txt_ordcode.Text = "";

            cbo_vehicle.SelectedIndex = -1;
            txt_plate.Text = "";
            txt_vin.Text = "";
            txt_engine.Text = "";
            txt_model.Text = "";
            txt_speednometer.Text = "";
            //dtp_released.Text = "";

            txt_reference.Text = "";
            txt_billing.Text = "";

            cbo_brand.SelectedIndex = -1;
            cbo_dealer.SelectedIndex = -1;
            cbo_status.SelectedIndex = 0;

            cbo_marketsegment.SelectedIndex = -1;
            try { cbo_agent.SelectedIndex = 0; }
            catch { }
           
            rtxt_remark.Text = "";
            rtxt_warranty_notes.Text = "";

            txt_grossamount.Text = "0.00";
            txt_totaldiscamt.Text = "0.00";
            txt_totalamount.Text = "0.00";
            txt_payment.Text = "0.00";
            txt_balance.Text = "0.00";
            txt_netamt.Text = "0.00";
            txt_totaltax.Text = "0.00";
            cbo_insurance.SelectedIndex = -1;
            cbo_clerk.SelectedIndex = -1;
            cbo_cashier.SelectedIndex = -1;
            cbo_fa_time.SelectedIndex = -1;

            dtp_promise_date.ResetText();
            dtp_promisetime.ResetText();
            dtp_released.ResetText();
            dtp_trnx_date.ResetText();
            dtp_ord_date.ResetText();
            
            try
            {
                cbo_customer.SelectedIndex = -1;
                lbl_custcode.Text = cbo_customer.SelectedValue.ToString();
            }
            catch (Exception) { }


            try
            {
                dgv_itemlist.Rows.Clear();
                dgv_subitem.Rows.Clear();
                dgv_payment.Rows.Clear();
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

            frm_enable(true);
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

            try
            {
                if (cbo_outlet_olist.SelectedIndex > -1)
                {
                    cbo_outlet.SelectedIndex = cbo_outlet_olist.SelectedIndex;

                    cbo_whsname.SelectedValue = db.get_outlet_whs_code(cbo_outlet_olist.SelectedValue.ToString());
                }
            }
            catch { }

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

        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = -1;
            String upd = "";
            String code = "", canc = "";
            isnew = false;
            int lastrow = 0;
            DataTable dt = null;

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
                    catch (Exception err) { }

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
                        try
                        {
                            if (cbo_outlet_olist.SelectedIndex > -1)
                            {
                                cbo_outlet.SelectedIndex = cbo_outlet_olist.SelectedIndex;

                                cbo_whsname.SelectedValue = db.get_outlet_whs_code(cbo_outlet_olist.SelectedValue.ToString());
                            }
                        }
                        catch { }

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
                    MessageBox.Show("No invoice selected.");
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
            DataTable dtp;
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
                catch (Exception err) {  }


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

        private void btn_print_Click(object sender, EventArgs e)
        {

            if (dgv_list.Rows.Count > 0)
            {
                if (comboBox2.SelectedIndex != -1)
                {
                    int r = dgv_list.CurrentRow.Index;
                    int i = comboBox2.SelectedIndex;
                    String or_num = dgv_list["dgvl_code", r].Value.ToString();

                    if (!String.IsNullOrEmpty(or_num))
                    {
                        Report rpt = new Report();

                        if (i == 0)
                           rpt.print_ro_invoice(or_num);
                        else if (i == 1)
                            rpt.print_purchase_requestion(or_num);
                        else if (i == 2)
                            rpt.print_invoice_billing(or_num);
                        else if (i == 3)
                            rpt.print_job_order(or_num);
                        else if (i == 4)
                            rpt.print_ziebart_billing(or_num);
                        else if (i == 5)
                            rpt.print_ziebart_warranty(or_num);

                        if (i < comboBox2.Items.Count)
                            rpt.ShowDialog();
                    }
                    //RO Invoice, PRIS, Invoice Billing
                }
                else
                {
                    MessageBox.Show("Please select printing option");
                    comboBox2.DroppedDown = true;
                }
            }
            else
            {
                MessageBox.Show("No RO item selected.");
            }

        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            String line_no = "";
            if(isCashier)
            {
                isnew_item = true;
                this.isCashier = true;
                int lastrow = 0;

                //try
                //{
                //    if (isnew == false)
                //    {
                //        lastrow = dgv_payment.Rows.Count;

                //        lnno_last = int.Parse(dgv_payment["dgvlp_ln_num", lastrow].Value.ToString());
                //        inc_lnno();
                //    }
                //    else
                //    {
                //        if (dgv_payment.Rows.Count == 0)
                //        {
                //            lnno_last = 0;
                //            inc_lnno();
                //        }
                //        else
                //        {
                //            lastrow = dgv_payment.Rows.Count;
                //            lnno_last = int.Parse(dgv_payment["dgvlp_ln_num", lastrow].Value.ToString());
                //            inc_lnno();
                //        }
                //    }
                //}
                //catch { }

                enter_payment((++lnno_last).ToString(),"","","","");
            }
            else
            {
                add_item("");
            }
        }

        private void btn_addtxtitem_Click(object sender, EventArgs e)
        {
            int r = 0;
            try {
                r = dgv_itemlist.Rows.Count - 1;
                z_enter_item_simple frm = new z_enter_item_simple(this, true, (int)double.Parse(dgv_itemlist["dgvli_lnno", r].Value.ToString()));
                frm.ShowDialog();
            }
            catch { }
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            String line_no = "", mode="",referen = "",amount ="";
            if (isCashier)
            {
                isnew_item = false;
                
                int r = 0;

                try
                {
                    if (dgv_payment.Rows.Count > 0)
                    {
                        r = dgv_payment.CurrentRow.Index;
                        line_no = dgv_payment["dgvlp_ln_num", r].Value.ToString();
                        mode = dgv_payment["dgvlp_pay_code", r].Value.ToString();
                        referen = dgv_payment["dgvlp_reference", r].Value.ToString();
                        amount = dgv_payment["dgvlp_ln_amnt", r].Value.ToString();
                        String chargeTo = dgv_payment["dgvlp_chargeto", r].Value.ToString();
                        //MessageBox.Show(line_no.ToString());
                        enter_payment((gm.toNormalDoubleFormat(line_no) * -1).ToString(), mode, referen, amount, chargeTo);
                    }
                    else
                    {
                        MessageBox.Show("No payment item selected.");
                    }
                }
                catch (Exception) { MessageBox.Show("No payment item selected catch."); }

            }
            else
            {
                int r = dgv_itemlist.CurrentRow.Index;

                if (dgv_itemlist["dgvli_itemcode", r].Value.ToString().ToLower().Equals("text-item"))
                {
                    z_enter_item_simple frm = new z_enter_item_simple(this, false, (int)double.Parse(dgv_itemlist["dgvli_lnno", r].Value.ToString()));
                    frm.ShowDialog();
                }
                else
                {
                    upd_item();
                }
            }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            if (isCashier)
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
            else
            {
                del_item();
            }

            //total_amountdue();
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
            String ord_code, out_code, debt_code, customer, ord_date, ord_qty, ord_amnt, promise_date, promise_time, disc_amnt, reference, mcardid, tax_amnt, amnt_due, user_id, t_date, t_time, loc, user_id2, t_date2, t_time2, trnx_date, disc_code, rep_code = "", agentid, amount_paid, car_item_code = "", car_insurance = "", car_plate, car_vin_num, car_engine, servicetype, car_model, last_km_reading, dealer_id, car_brand_id, remark, status = "", technician = ""; ;
            String branch = GlobalClass.branch, transferred, stk_ref = "", stk_po_so = "", market_segment_id = "", car_date_release = "", fa_time = "", billing_no = "", warranty_notes;
            String cashier = "";
            String pending = "Y";
            String col = "", val = "", col2 = "", val2 = "";
            String notifyadd = null;
            String table = "orhdr";
            String tableln = "orlne";
            promise_date = dtp_promise_date.Value.ToString("yyyy-MM-dd");
            promise_time = dtp_promisetime.Value.ToString("hh:mm");
            if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the customer field.");
                m_customers frm = new m_customers(this, true);
                frm.ShowDialog();
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
            else if (cbo_status.SelectedIndex == -1)
            {
                MessageBox.Show("Please select status field.");
                cbo_status.DroppedDown = true;
            }
            else if (cbo_servicetype.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Service Type field.");
                cbo_servicetype.DroppedDown = true;
            }
            else if (dgv_itemlist.Rows.Count < 1)
            {
                MessageBox.Show("No entry of Sales Item(s). Please add item(s).");
            }
            else
            {
                if (cbo_insurance.SelectedIndex == -1)
                {
                    car_insurance = "";
                }
                else
                {
                    car_insurance = cbo_insurance.SelectedValue.ToString();
                }
                if (pending_status == false)
                {
                    pending = "N";
                }

                out_code = (cbo_outlet.SelectedValue ?? "").ToString();
                debt_code = (cbo_customer.SelectedValue ?? "").ToString();

                ord_code = txt_ordcode.Text;
                customer = cbo_customer.Text;
                mcardid = txt_servex.Text;
                loc = (cbo_whsname.SelectedValue ?? "").ToString();
                agentid = (cbo_agent.SelectedValue ?? "").ToString();
                technician = (cbo_technician.SelectedValue ?? "").ToString();

                stk_ref = stk_trns_type + "#" + ord_code;
                stk_po_so = ord_code;
                //txt_reference.Text = stk_ref;
                reference = txt_reference.Text;
                billing_no = txt_billing.Text;
                ord_date = dtp_ord_date.Value.ToString("yyyy-MM-dd");
                //amnt_tendered = gm.toNormalDoubleFormat(txt_csh_amttendered.Text).ToString("0.00");
                user_id = GlobalClass.username;
                t_date = db.get_systemdate("yyyy-MM-dd");
                t_time = db.get_systemtime();
                market_segment_id = (cbo_marketsegment.SelectedValue ?? "").ToString();

                //
                //amount_paid = gm.toNormalDoubleFormat(amt_tendered);

                //car details
                car_item_code = cbo_vehicle.Text;
                car_plate = txt_plate.Text;
                car_engine = txt_engine.Text;
                car_model = txt_model.Text;
                last_km_reading = txt_speednometer.Text;
                car_vin_num = txt_vin.Text;
                remark = rtxt_remark.Text;
                warranty_notes = rtxt_warranty_notes.Text;
                trnx_date = dtp_trnx_date.Value.ToString("yyyy-MM-dd");
                car_date_release = dtp_released.Value.ToString("yyyy-MM-dd");

                servicetype = (cbo_servicetype.SelectedValue ?? "").ToString();
                dealer_id = (cbo_dealer.SelectedValue ?? "").ToString();
                car_brand_id = (cbo_brand.SelectedValue ?? "").ToString();
                status = (cbo_status.SelectedValue ?? "").ToString();
                fa_time = cbo_fa_time.Text;

                if (cbo_clerk.SelectedIndex != -1)
                {
                    rep_code = cbo_clerk.SelectedValue.ToString();
                }
                if (isCashier)
                {
                    
                }

                ord_amnt = gm.toNormalDoubleFormat(txt_grossamount.Text).ToString("0.00");
                String total_amnt = gm.toNormalDoubleFormat(txt_totalamount.Text).ToString("0.00");
                disc_amnt = gm.toNormalDoubleFormat(txt_totaldiscamt.Text).ToString("0.00");
                amount_paid = gm.toNormalDoubleFormat(txt_payment.Text).ToString("0.00");
                amnt_due = gm.toNormalDoubleFormat(txt_balance.Text).ToString("0.00");
                String net_amnt = gm.toNormalDoubleFormat(txt_netamt.Text).ToString("0.00");
                tax_amnt = gm.toNormalDoubleFormat(txt_totaltax.Text).ToString("0.00");
 
                if (isPayment)
                {
                    /*
                    if (cbo_cashier.SelectedIndex != -1)
                    {
                        cashier = cbo_cashier.SelectedValue.ToString();
                    }

                    user_id2 = GlobalClass.username;
                    t_date2 = db.get_systemdate("yyyy-MM-dd");
                    t_time2 = db.get_systemtime();

                    amount_paid = (gm.toNormalDoubleFormat(txt_payment.Text) * -1).ToString("0.00");
                    amnt_due = gm.toNormalDoubleFormat(txt_balance.Text).ToString("0.00");
                    tax_amnt = (gm.toNormalDoubleFormat(amnt_due) * ((db.get_outlet_govt_pct(out_code) / 100)+ 1 )).ToString("0.00");

                    col2 = ", user_id2, t_date2, t_time2, tax_amnt, amnt_due,pay_code, cashier, amnt_tendered";
                    val2 = ", '" + user_id2 + "', '" + t_date2 + "', '" + t_time2 + "', '" + tax_amnt + "', '" + amnt_due + "', '" + pay_code + "', '" + cashier + "', '" + amount_paid + "', '" + car_item_code + "', '" + car_plate + "', '" + car_vin_num + "', '" + car_engine + "', '" + servicetype + "', '" + car_model + "', '" + last_km_reading + "', '" + dealer_id + "', '" + car_brand_id + "', '" + remark + "'";

                    if (isnew == false)
                    {
                        col = ", tax_amnt='" + tax_amnt + "', amnt_due='" + amnt_due + "', user_id2='" + user_id2 + "', t_date2='" + t_date2 + "', t_time2='" + t_time2 + "', pay_code='" + pay_code + "', cashier='" + cashier + "', amnt_tendered='" + amount_paid + "', promise_date='" + promise_date + "', promise_time='" + promise_time + "', car_item_code='" + car_item_code + "', car_plate='" + car_plate + "', car_vin_num='" + car_vin_num + "', car_engine='" + car_engine + "', servicetype='" + servicetype + "', car_model='" + car_model + "', last_km_reading='" + last_km_reading + "', dealer_id='" + dealer_id + "', car_brand_id='" + car_brand_id + "', remark='" + remark + "', technician='" + technician + "', car_insurance='" + car_insurance + "'";
                    }
                     */
                }

                if (isnew)
                {
                    notificationText = "has added: ";
                    stk_ref = stk_trns_type + "#" + ord_code;
                    stk_po_so = ord_code;
                    //txt_reference.Text = stk_ref;
                    ord_code = db.get_ord_code(out_code);
                    col = "ord_code, out_code, debt_code, customer, ord_date, ord_amnt, trnx_date, disc_amnt, reference, mcardid, user_id, t_date, t_time, loc,rep_code, agentid, branch,market_segment_id, pending,promise_date,promise_time,car_item_code,car_plate ,car_vin_num ,car_engine ,servicetype ,car_model ,last_km_reading ,dealer_id ,car_brand_id ,remark,warranty_notes,status,technician,car_insurance,car_date_release, total_amnt, payment, amnt_due, net_amnt, tax_amnt,fa_time,billing_no " + col2;

                    val = "'" + ord_code + "', '" + out_code + "', '" + debt_code + "', " + db.str_E(customer) + ", '" + ord_date + "', '" + ord_amnt + "', '" + trnx_date + "', '" + disc_amnt + "', " + db.str_E(reference) + ", '" + mcardid + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + loc + "', '" + rep_code + "', '" + agentid + "', '" + branch + "','" + market_segment_id + "', '" + pending + "','" + promise_date + "','" + promise_time + "','" + car_item_code + "','" + car_plate + "','" + car_vin_num + "','" + car_engine + "','" + servicetype + "','" + car_model + "','" + last_km_reading + "','" + dealer_id + "','" + car_brand_id + "'," + db.str_E(remark) + "," + db.str_E(warranty_notes) + ",'" + status + "','" + technician + "','" + car_insurance + "','" + car_date_release + "','" + total_amnt + "','" + amount_paid + "','" + amnt_due + "','" + net_amnt + "','" + tax_amnt + "','" + fa_time + "'," + db.str_E(billing_no) + " " + val2; 

                    if (db.InsertOnTable(table, col, val))
                    {
                        try
                        {

                            stk_ref = stk_trns_type + "#" + ord_code;
                            notifyadd = add_items(tableln, ord_code, trnx_date, stk_ref, stk_po_so, loc);

                            if (String.IsNullOrEmpty(notifyadd) == false)
                            {
                                notificationText += notifyadd;
                                notificationText += Environment.NewLine + " with SO#" + ord_code;
                                notify.saveNotification(notificationText, "Sales Outlet");
                                db.set_ord_code_nextno(ord_code, out_code);
                                success = true;
                            }
                            else
                            {
                                success = false;
                            }
                        }
                        catch (Exception er) { MessageBox.Show(er.ToString()); }
                    }

                    if (success == false)
                    {
                        //db.DeleteOnTable(table, "ord_code='" + ord_code + "'");
                        //db.DeleteOnTable(tableln, "ord_code='" + ord_code + "'");
                        //db.DeleteOnTable("stkcrd", "po_so='" + ord_code + "' AND trn_type='" + stk_trns_type + "'");

                        MessageBox.Show("Failed on saving. on New");
                    }
                }
                else
                {
                    if (isPayment)
                    {

                    }

                    notificationText = "has updated: ";

                    if (isCashier)
                    {

                        if (cbo_cashier.SelectedIndex != -1)
                        {
                            cashier = cbo_cashier.SelectedValue.ToString();
                        }

                        user_id2 = GlobalClass.username;
                        t_date2 = db.get_systemdate("yyyy-MM-dd");
                        t_time2 = db.get_systemtime();

                        //ord_amnt, disc_amnt, total_amnt, payment, amnt_due, net_amnt,tax_amnt
                        //ord_amnt, disc_amnt, total_amnt, amount_paid, amnt_due, net_amnt, tax_amnt

                        col = " user_id2='" + user_id2 + "', t_date2='" + t_date2 + "', t_time2='" + t_time2 + "', pay_code='" + pay_code + "', cashier='" + cashier + "', payment='" + amount_paid + "', promise_date='" + promise_date + "', promise_time='" + promise_time + "', car_item_code='" + car_item_code + "', car_plate='" + car_plate + "', car_vin_num='" + car_vin_num + "', car_engine='" + car_engine + "', servicetype='" + servicetype + "', car_model='" + car_model + "', last_km_reading='" + last_km_reading + "', dealer_id='" + dealer_id + "', car_brand_id='" + car_brand_id + "', remark=" + db.str_E(remark) + ", warranty_notes=" + db.str_E(warranty_notes) + ", technician='" + technician + "', car_insurance='" + car_insurance + "',ord_amnt='" + ord_amnt + "',disc_amnt='" + disc_amnt + "', total_amnt='" + total_amnt + "', amnt_due='" + amnt_due + "', net_amnt='" + net_amnt + "', tax_amnt='" + tax_amnt + "',fa_time='" + fa_time + "',billing_no=" + db.str_E(billing_no) + "";
                        
                    }
                    else {

                        //total_amnt, amnt_due, net_amnt, tax_amnt
                        //ord_amnt, disc_amnt, total_amnt, payment, amnt_due, net_amnt,tax_amnt
                        //ord_amnt, disc_amnt, total_amnt, amount_paid, amnt_due, net_amnt, tax_amnt



                        col = "ord_code='" + ord_code + "', out_code='" + out_code + "', debt_code='" + debt_code + "', customer='" + customer + "', ord_date='" + ord_date + "', ord_amnt='" + ord_amnt + "', disc_amnt='" + disc_amnt + "', reference = " + db.str_E(reference) + ", mcardid='" + mcardid + "', user_id='" + user_id + "', t_date='" + t_date + "', t_time='" + t_time + "', loc='" + loc + "', trnx_date='" + trnx_date + "', pay_code='" + pay_code + "', cashier='" + cashier + "', rep_code='" + rep_code + "', agentid='" + agentid + "', branch='" + branch + "', pending='" + pending + "' ,payment='" + amount_paid + "' ,promise_date='" + promise_date + "' ,promise_time='" + promise_time + "' ,car_item_code='" + car_item_code + "' ,car_plate ='" + car_plate + "' ,car_vin_num ='" + car_vin_num + "' ,car_engine='" + car_engine + "', servicetype='" + servicetype + "',car_model ='" + car_model + "' ,last_km_reading ='" + last_km_reading + "' ,dealer_id  ='" + dealer_id + "' ,car_brand_id  ='" + car_brand_id + "' ,remark =" + db.str_E(remark) + ", warranty_notes=" + db.str_E(warranty_notes) + " ,status ='" + status + "',technician ='" + technician + "', car_insurance='" + car_insurance + "',car_date_release='" + car_date_release + "',total_amnt='" + total_amnt + "', amnt_due='" + amnt_due + "', net_amnt='" + net_amnt + "', tax_amnt='" + tax_amnt + "',fa_time='" + fa_time + "',billing_no=" + db.str_E(billing_no) + "";
                    }

                    if (db.UpdateOnTable(table, col, "ord_code='" + ord_code + "'"))
                    {
                        db.DeleteOnTable(tableln, "ord_code='" + ord_code + "'");
                        db.DeleteOnTable("stkcrd", "po_so='" + ord_code + "' AND trn_type='" + stk_trns_type + "'");
                        try
                        {

                            stk_ref = stk_trns_type + "#" + ord_code;
                            notifyadd = add_items(tableln, ord_code, trnx_date, stk_ref, stk_po_so, loc);
                            //MessageBox.Show(notifyadd.ToString());
                            if (String.IsNullOrEmpty(notifyadd) == false)
                            {
                                notificationText += notifyadd;
                                notificationText += Environment.NewLine + " with SO#" + ord_code;
                                notify.saveNotification(notificationText, "Sales Outlet");
                                success = true;
                            }
                            else
                            {
                                success = false;
                            }
                        }
                        catch (Exception er) { MessageBox.Show(er.ToString()); }
                    }

                    if (success == false)
                    {
                        MessageBox.Show("Failed on saving. on update");
                    }
                }
                add_payment_items(txt_ordcode.Text);
                if (success)
                {
                    if (isPayment)
                    {
                        frm_payment.Close();
                    }

                    if(isRelease)
                    {
                        db.UpdateOnTable("orhdr", "pending='N', status='00007'", "ord_code='" + txt_ordcode.Text + "'");
                    }

                    disp_list();
                    frm_reload();
                    goto_win1();
                }
            }
        }

        private String add_items(String tableln, String ord_code, String dt_trnx, String stk_ref, String stk_po_so, String loc)
        {
            String notificationText = null;
            String ln_num, ord_line, item_code, item_desc, unit, ord_qty, price, ln_amnt, ln_tax, rep_code, reference, trnx_date, t_time, fcp = "0.00", part_no, disc_code, disc_amt, disc_reason, disc_user, net_amount;
            String val2 = "";
            String col2 = "ord_code, ln_num, item_code, item_desc, unit, ord_qty, price, ln_amnt, ln_tax, rep_code, reference, trnx_date, t_time, fcp, part_no, disc_code, disc_amt, disc_reason, disc_user, net_amount";
            String stk_qty_in = "0.00";
            String stk_qty_out = "0.00";

            try
            {
                db.DeleteOnTable("orlne2", "ord_code='" + ord_code + "'");

                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    ln_num = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                    item_code = dgv_itemlist["dgvli_itemcode", r].Value.ToString();
                    item_desc = dgv_itemlist["dgvli_itemdesc", r].Value.ToString();
                    trnx_date = dgv_itemlist["dgvli_t_date", r].Value.ToString();
                    t_time = dgv_itemlist["dgvli_t_time", r].Value.ToString();
                    rep_code = dgv_itemlist["dgvli_clerkid", r].Value.ToString();
                    ord_qty = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_qty", r].Value.ToString()).ToString("0.00");
                    unit = dgv_itemlist["dgvli_unitid", r].Value.ToString();
                    price = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_sellprice", r].Value.ToString()).ToString("0.00");
                    ln_amnt = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", r].Value.ToString()).ToString("0.00");
                    reference = stk_ref;
                    // = dt_trnx;
                    //t_time = db.get_systemtime();
                    fcp = db.get_item_fcp(item_code).ToString("0.00");
                    ln_tax = gm.toNormalDoubleFormat((dgv_itemlist["dgvli_taxamt", r].Value ?? "0").ToString()).ToString("0.00");
                    disc_amt = gm.toNormalDoubleFormat((dgv_itemlist["dgvli_discamt", r].Value ?? "0").ToString()).ToString("0.00");
                    part_no = (dgv_itemlist["dgvi_part_no", r].Value ?? "").ToString();
                    disc_code = (dgv_itemlist["dgvli_disccode", r].Value ?? "").ToString();
                    disc_reason = (dgv_itemlist["dgvli_discreason", r].Value ?? "").ToString();
                    disc_user = (dgv_itemlist["dgvli_discuser", r].Value ?? "").ToString();
                    net_amount = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_net", r].Value.ToString()).ToString("0.00");
                    //if (cbo_clerk.SelectedIndex != -1)
                    //{
                    //    rep_code = cbo_clerk.SelectedValue.ToString();
                    //}
                    //else
                    //{
                    //    rep_code = "";
                    //}

                    val2 = "'" + ord_code + "', '" + ln_num + "', '" + item_code + "', " + db.str_E(item_desc) + ", '" + unit + "', '" + ord_qty + "', '" + price + "', '" + ln_amnt + "', '" + ln_tax + "', '" + rep_code + "', '" + reference + "', '" + trnx_date + "', '" + t_time + "', '" + fcp + "', " + db.str_E(part_no) + ", '" + disc_code + "', '" + disc_amt + "', '" + disc_reason + "', '" + disc_user + "', '" + net_amount + "'";

                    if (db.InsertOnTable(tableln, col2, val2))
                    {
                        stk_qty_in = "0.00";
                        stk_qty_out = "0.00";

                        if (dgv_itemlist["dgvli_unit", r].Value.ToString().ToLower().Equals("set"))
                        {
                            add_sub_item(ord_code, item_code, ln_num);
                        }


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

            total_amountdue();

            return notificationText;
        }
        public void add_sub_item(String ord_code, String item_code_ol, String lnnum)
        {
            String ln_num = "", item_code = "", item_desc = "", part_no = "", unitid = "", qty = "", cost_pric = "", lnamnt = "", disc_amt = "", sellprice = "", in_tax = "", disc_code = "", disc_user = "", disc_reason = "", notes = "", net_amount="";
            String val2 = "";
            String col2 = "ord_code, item_code_ol, item_code, item_desc, part_no, unitid, qty, cost_pric, lnamnt, ln_num, disc_amt,sellprice,in_tax,disc_code,disc_user,disc_reason,notes,net_amount";
            try
            {
                for (int r = 0; r < dgv_subitem.Rows.Count; r++)
                {
                    ln_num = dgv_subitem["dgv_sub_item_ln", r].Value.ToString();
                    if (ln_num == lnnum) {

                        item_code = dgv_subitem["dgv_sub_item_itemcode", r].Value.ToString();
                        item_desc = db.str_E(dgv_subitem["dgv_sub_item_itemdesc", r].Value.ToString());
                        part_no = db.str_E(dgv_subitem["dgv_sub_item_partno", r].Value.ToString());
                        unitid = dgv_subitem["dgv_sub_item_unitid", r].Value.ToString();
                        qty = dgv_subitem["dgv_sub_item_qty", r].Value.ToString();
                        cost_pric = gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_costprice", r].Value ?? "0").ToString()).ToString("0.00");
                        lnamnt = gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_lnamnt", r].Value ?? "0").ToString()).ToString("0.00");
                        net_amount = gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_net", r].Value ?? "0").ToString()).ToString("0.00");
                        disc_amt = gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_disc_amt", r].Value ?? "0").ToString()).ToString("0.00");
                        sellprice = gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_sellprice", r].Value ?? "0").ToString()).ToString("0.00");
                        in_tax = gm.toNormalDoubleFormat((dgv_subitem["dgv_sub_item_in_tax", r].Value ?? "0").ToString()).ToString("0.00");
                        disc_code = (dgv_subitem["dgv_sub_item_disc_code", r].Value ?? "").ToString();
                        disc_user = (dgv_subitem["dgv_sub_item_disc_user", r].Value ?? "").ToString();
                        disc_reason = (dgv_subitem["dgv_sub_item_disc_reason", r].Value ?? "").ToString();
                        notes = (dgv_subitem["dgv_sub_item_notes", r].Value ?? "").ToString();
                        //ord_code, item_code_ol, item_code, part_no, item_desc, unitid, qty, cost_pric, lnamnt, ln_num, disc_amt,sellprice,in_tax,disc_code,disc_user,disc_reason,notes

                        val2 = "'" + ord_code + "', '" + item_code_ol + "', '" + item_code + "', " + item_desc + ", " + part_no + ", '" + unitid + "', '" + qty + "', '" + cost_pric + "', '" + lnamnt + "', '" + ln_num + "', '" + disc_amt + "', '" + sellprice + "', '" + in_tax + "', '" + disc_code + "', '" + disc_user + "', '" + disc_reason + "', '" + notes + "', '" + net_amount + "'";

                        //DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.orlne2 WHERE ord_code='"+ord_code+"' AND item_code_ol='"+item_code_ol+"'");

                        //if (dt.Rows.Count > 0)
                        //{
                        //db.DeleteOnTable("orlne2", "ord_code='" + ord_code + "' AND item_code_ol='" + item_code_ol  + "' AND item_code='"+item_code+"'");
                        //}
                        if (db.InsertOnTable("orlne2", col2, val2))
                        {
                            //MessageBox.Show("Record Successfully Saved.");
                        }
                        else
                        {
                            MessageBox.Show("Saving Sub item Failed.");
                        }

                    }
                }

            }
            catch { }
        
        }

        public void add_payment_items(String code) 
        {

            String notificationText = null;
            String ln_num, ln_amnt, rep_code, reference, paycode, ln_type, ordcode, chargeto, t_date, t_time;
            String val2 = "";
            String col2 = "ord_code, ln_num, ln_amnt, pay_code, rep_code, reference, ln_type, chargeto,trnx_date,t_time";
            String paytemp = "";
            DataTable dt;
            try
            {
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
                    t_date = dgv_payment["dgvlp_t_date", r].Value.ToString();
                    t_time = dgv_payment["dgvlp_t_time", r].Value.ToString();
                  
                    //dt = db.QueryBySQLCode("SELECT * FROM rssys.orlne WHERE ");

                    paytemp = db.get_colval("orlne", "ord_code", "ord_code='" + ordcode + "' AND ln_num='" + ln_num + "'");
                    if (!String.IsNullOrEmpty(paytemp))
                    {
                        db.DeleteOnTable("orlne", "ord_code='" + ordcode + "' AND ln_num='" + ln_num + "'");
                    }

                    val2 = "'" + ordcode + "', '" + ln_num + "', '" + ln_amnt + "', '" + paycode + "', '" + rep_code + "', '" + reference + "','" + ln_type + "','" + chargeto + "','" + t_date + "','" + t_time + "'";

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

            total_amountdue();         
        }


        private void btn_exit_Click(object sender, EventArgs e)
        {
            frm_reload();
            goto_win1();
            disp_list();
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
            //int lastrow = 0;

            //try
            //{
            //    if (isnew == false)
            //    {
            //        lastrow = dgv_itemlist.Rows.Count;
            //        lnno_last = int.Parse(dgv_itemlist["dgvli_lnno", lastrow].Value.ToString());
            //        inc_lnno();
            //    }
            //    else
            //    {
            //        if (dgv_itemlist.Rows.Count == 0)
            //        {
            //            lnno_last = 0;
            //            inc_lnno();
            //        }
            //        else
            //        {
            //            lastrow = dgv_itemlist.Rows.Count;
            //            lnno_last = int.Parse(dgv_itemlist["dgvli_lnno", lastrow].Value.ToString());
            //            inc_lnno();
            //        }
            //    }
            //}
            //catch {  }

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
                if (dgv_itemlist.Rows.Count == 0)
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
                        String _lnno = dgv_itemlist["dgvli_lnno", i].Value.ToString();

                        for (int r = dgv_subitem.Rows.Count - 1; r >= 0; r--)
                        {
                            if (dgv_subitem["dgv_sub_item_ln", r].Value.ToString() == _lnno)
                            {
                                dgv_subitem.Rows.RemoveAt(r);
                            }
                        }

                        dgv_itemlist.Rows.RemoveAt(i);
                        total_amountdue();
                    }
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        public void total_amountdue()
        {
            Double amt = 0.00;
            Double damt = 0.00;
            Double tax = 0.00;
            Double paid = 0.00;
            Double net = 0.00;
            Double lnamt = 0.00;
            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count; i++)
                {
                    lnamt += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", i].Value.ToString());
                    damt += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_discamt", i].Value.ToString());
                    tax += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_taxamt", i].Value.ToString());
                    net += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_net", i].Value.ToString());
                }
            }
            catch { }

            try
            {
                for (int p = 0; p < dgv_payment.Rows.Count; p++)
                 {
                     paid += gm.toNormalDoubleFormat(dgv_payment["dgvlp_ln_amnt", p].Value.ToString());
                 }
            }
            catch { }

            txt_grossamount.Text = gm.toAccountingFormat((lnamt + damt).ToString());
            txt_totaldiscamt.Text = gm.toAccountingFormat(damt.ToString());
            txt_totalamount.Text = gm.toAccountingFormat(lnamt.ToString());
            txt_payment.Text = gm.toAccountingFormat(paid);
            txt_balance.Text = gm.toAccountingFormat((lnamt+ paid).ToString());

            txt_netamt.Text = gm.toAccountingFormat(net);
            txt_totaltax.Text = gm.toAccountingFormat(tax.ToString());

            if(gm.toNormalDoubleFormat(txt_balance.Text) <= 0.00 && isCashier == true)
            {
                btn_release.Enabled = true;
            }
            else
            {
                btn_release.Enabled = false;
            }
        }

        private void enter_payment(String lnno, String mode, String refe , String amnt,String chargeTo)
        {
          
            if (dgv_itemlist.Rows.Count < 0)
            {
                MessageBox.Show("Pls add the sales order item(s).");
            }
            else if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer first.");
            }
            else
            {
                if (!isCashier)
                {

                    frm_payment = new z_payment(this, isnew, chargeTo, cbo_customer.SelectedValue.ToString(), txt_balance.Text, txt_totaldiscamt.Text, cbo_outlet.SelectedValue.ToString(), false, lnno, mode, refe, amnt);
                }
                else {
                    int ln = 1;

                    if (dgv_payment.Rows.Count > 0)
                    {
                        int n = 0;
                        if (isnew_item)
                        {
                            n = (int)double.Parse(dgv_payment["dgvlp_ln_num", dgv_payment.Rows.Count - 1].Value.ToString());
                        }
                        else {
                            n = (int)double.Parse(dgv_payment["dgvlp_ln_num", dgv_payment.CurrentRow.Index].Value.ToString());
                        }

                        ln = Math.Abs(n) + ( isnew_item ? 1:0 );
                    }

                    frm_payment = new z_payment(this, isnew_item, chargeTo, cbo_customer.SelectedValue.ToString(), txt_balance.Text, txt_totaldiscamt.Text, cbo_outlet.SelectedValue.ToString(), true, ln.ToString(), mode, refe, amnt);
                }
                frm_payment.ShowDialog();

                this.Refresh();
            }
        }

        private void btn_customer_refresh_Click(object sender, EventArgs e)
        {
            m_customers frm = new m_customers(this, true);
            frm.ShowDialog();
        }

        private void btn_crd_clear_Click(object sender, EventArgs e)
        {
            m_membership frm = new m_membership(this,true);
            frm.ShowDialog();
        }

        /*protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                frm_pclerkpass.ShowDialog();

                return true;
            }
            else if (keyData == Keys.F2)
            {
                mod_customer();

                return true;
            }
            else if (keyData == Keys.F3)
            {
                //no stock location
                return true;
            }
            else if (keyData == Keys.F5)
            {
                add_item("");

                return true;
            }
            else if (keyData == Keys.F6)
            {
                upd_item();

                return true;
            }
            else if (keyData == Keys.F7)
            {
                this.ActiveControl = cbo_customer;

                return true;
            }
            else if (keyData == Keys.F8)
            {
                if (ck_senior.Checked)
                {
                    ck_senior.Checked = false;
                }
                else
                {
                    ck_senior.Checked = true;
                }

                return true;
            }
            else if (keyData == Keys.F10)
            {
                enter_payment();

                return true;
            }
            else if (keyData == Keys.F11)
            {
                disp_order_list();

                return true;
            }
            else if (keyData == Keys.F12)
            {
                enter_return();

                return true;
            }
            else if (keyData == Keys.Delete)
            {
                del_item();

                return true;
            }
            else if (keyData == Keys.Home)
            {
                this.ActiveControl = txt_search;

                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        } */
                
        public void verifiedClerk(String cid, String cname)
        {
            DataTable dt;
            String outcode = "";
            isverified = true;

            frm_clear();

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
                try { outcode = cbo_outlet_olist.SelectedValue.ToString(); }
                catch { }


                if (isverified)
                {
                    m_customers frm = new m_customers(this, true);
                    frm.ShowDialog();
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

                        try { dtp_trnx_date.Value = gm.toDateValue(dt.Rows[i]["trnx_date"].ToString()); }
                        catch { dtp_trnx_date.ResetText(); }
                        try { dtp_released.Value = gm.toDateValue(dt.Rows[i]["car_date_release"].ToString()); }
                        catch { dtp_released.ResetText(); }
                        try { dtp_promise_date.Value = gm.toDateValue(dt.Rows[i]["promise_date"].ToString()); }
                        catch { dtp_promise_date.ResetText(); }
                        try { dtp_promisetime.Value = DateTime.Parse(dt.Rows[i]["promise_time"].ToString()); }
                        catch { dtp_promisetime.ResetText(); }

                        txt_reference.Text = dt.Rows[i]["reference"].ToString();
                        txt_billing.Text = dt.Rows[i]["billing_no"].ToString();
                        cbo_customer.SelectedValue = dt.Rows[i]["debt_code"].ToString();
                        txt_servex.Text = dt.Rows[i]["mcardid"].ToString();

                        txt_balance.Text = dt.Rows[i]["ord_amnt"].ToString();
                        txt_totaltax.Text = dt.Rows[i]["disc_amnt"].ToString();
                        txt_payment.Text = dt.Rows[i]["payment"].ToString();
                        cbo_vehicle.Text = dt.Rows[i]["car_item_code"].ToString();
                        txt_plate.Text = dt.Rows[i]["car_plate"].ToString();
                        txt_vin.Text = dt.Rows[i]["car_vin_num"].ToString();
                        cbo_servicetype.SelectedValue = dt.Rows[i]["servicetype"].ToString();
                        txt_engine.Text = dt.Rows[i]["car_engine"].ToString();
                        txt_model.Text = dt.Rows[i]["car_model"].ToString();
                        cbo_insurance.Text = dt.Rows[i]["car_insurance"].ToString();
                        txt_speednometer.Text = dt.Rows[i]["last_km_reading"].ToString();
                        cbo_dealer.SelectedValue = dt.Rows[i]["dealer_id"].ToString();
                        cbo_brand.SelectedValue = dt.Rows[i]["car_brand_id"].ToString();
                        rtxt_remark.Text = dt.Rows[i]["remark"].ToString();
                        rtxt_warranty_notes.Text = dt.Rows[i]["warranty_notes"].ToString();
                        dtp_ord_date.Value = gm.toDateValue(dt.Rows[i]["ord_date"].ToString());
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                        if (isCashier == true)
                        {
                            cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                        }
                        else
                        {
                            cbo_cashier.SelectedValue = dt.Rows[i]["cashier"].ToString();
                        }
                        
                        cbo_status.SelectedValue = dt.Rows[i]["status"].ToString();
                        cbo_technician.SelectedValue = dt.Rows[i]["technician"].ToString();
                        cbo_fa_time.Text = (dt.Rows[i]["fa_time"]??"").ToString();

                    }
                    disp_itemlist(code);
                    disp_sub_item_list(true);
                    disp_payment_list(code);
                    total_amountdue();
                }
            }

            goto_win2();
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

            //dgv_itemlist["dgvli_regprice", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_regprice"].ToString());
            dgv_itemlist["dgvli_discamt", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_discamt"].ToString());
            dgv_itemlist["dgvli_sellprice", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_sellprice"].ToString());
            
            dgv_itemlist["dgvli_lnamt", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_lnamt"].ToString());
            dgv_itemlist["dgvli_net", i].Value = gm.toAccountingFormat((gm.toNormalDoubleFormat(dt.Rows[0]["dgvli_lnamt"].ToString()) / (((db.get_outlet_govt_pct(cbo_outlet.SelectedValue.ToString())) / 100) + 1)));
            dgv_itemlist["dgvli_taxamt", i].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", i].Value.ToString()) - gm.toNormalDoubleFormat(dgv_itemlist["dgvli_net", i].Value.ToString()));

            dgv_itemlist["dgvli_remarks", i].Value = dt.Rows[0]["dgvli_remarks"].ToString();
            dgv_itemlist["dgvli_discuser", i].Value = dt.Rows[0]["dgvli_discuser"].ToString();
            dgv_itemlist["dgvli_discreason", i].Value = dt.Rows[0]["dgvli_discreason"].ToString();
            dgv_itemlist["dgvli_disccode", i].Value = dt.Rows[0]["dgvli_disccode"].ToString();

            dgv_itemlist["dgvli_t_date", i].Value = db.get_systemdate("");
            dgv_itemlist["dgvli_t_time", i].Value = db.get_systemtime();
            dgv_itemlist["dgvli_clerk", i].Value = cbo_clerk.Text;
            dgv_itemlist["dgvli_clerkid", i].Value = cbo_clerk.SelectedValue.ToString();

            if (isnew_item)
            {
                inc_lnno();
            }
            //}
            //catch (Exception) { }
            disp_sub_item_list(false);
            total_amountdue();
        }
        
        public void dgv_sales_subitem(DataTable dt, String lnno, Boolean clearPreviousLineData)
        {
            int i = 0;
            
            if(dt.Rows.Count > 0)
            {
                try
                {
                    if (clearPreviousLineData)
                    {
                        for (int l = dgv_subitem.Rows.Count - 1; l >= 0; l--)
                        {
                            if (dgv_subitem["dgv_sub_item_ln", l].Value.ToString() == lnno)
                            {
                                dgv_subitem.Rows.RemoveAt(l);
                            }
                        }
                    }
                }
                catch { }

                try
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        i = dgv_subitem.Rows.Add();

                        dgv_subitem["dgv_sub_item_ln", i].Value = lnno;

                        dgv_subitem["dgv_sub_item_qty", i].Value = dt.Rows[r]["dgv_sub_item_qty"].ToString();
                        dgv_subitem["dgv_sub_item_unitid", i].Value = dt.Rows[r]["dgv_sub_item_unitid"].ToString();
                        dgv_subitem["dgv_sub_item_unitdesc", i].Value = dt.Rows[r]["dgv_sub_item_unitdesc"].ToString();
                        dgv_subitem["dgv_sub_item_itemdesc", i].Value = dt.Rows[r]["dgv_sub_item_itemdesc"].ToString();
                        dgv_subitem["dgv_sub_item_partno", i].Value = dt.Rows[r]["dgv_sub_item_partno"].ToString();
                        dgv_subitem["dgv_sub_item_itemcode", i].Value = dt.Rows[r]["dgv_sub_item_itemcode"].ToString();

                        //dgv_subitem["dgv_sub_item_costprice", i].Value = dt.Rows[r]["dgv_sub_item_costprice"].ToString();
                        dgv_subitem["dgv_sub_item_lnamnt", i].Value = gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_lnamnt"].ToString());
                        dgv_subitem["dgv_sub_item_net", i].Value = gm.toAccountingFormat((gm.toNormalDoubleFormat(gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_lnamnt"].ToString())) / (((db.get_outlet_govt_pct(cbo_outlet.SelectedValue.ToString())) / 100) + 1)).ToString("0.00"));
                        dgv_subitem["dgv_sub_item_disc_amt", i].Value = gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_disc_amt"].ToString());
                        dgv_subitem["dgv_sub_item_sellprice", i].Value = gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_sellprice"].ToString());
                        dgv_subitem["dgv_sub_item_in_tax", i].Value = gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_in_tax"].ToString());
                        dgv_subitem["dgv_sub_item_disc_code", i].Value = dt.Rows[r]["dgv_sub_item_disc_code"].ToString();
                        dgv_subitem["dgv_sub_item_disc_user", i].Value = dt.Rows[r]["dgv_sub_item_disc_user"].ToString();
                        dgv_subitem["dgv_sub_item_disc_reason", i].Value = dt.Rows[r]["dgv_sub_item_disc_reason"].ToString();
                        dgv_subitem["dgv_sub_item_notes", i].Value = dt.Rows[r]["dgv_sub_item_notes"].ToString();

                    }
                }
                catch (Exception er) { MessageBox.Show(er.Message); }
            }            
        }

        public void disp_sub(String ord_code , String menu_code, Boolean issub, String line)
        {

            dgv_subitem.Rows.Clear();

            if (issub)
            {
                int i = 0;
                DataTable dt = db.QueryBySQLCode("SELECT item_code_ol, item_code, part_no, item_desc, unitid, qty, cost_pric, lnamnt,  ln_num, net_amount FROM rssys.orlne2 WHERE ord_code='" + ord_code + "' AND item_code_ol='" + menu_code + "'");

                try
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        i = dgv_subitem.Rows.Add();

                        dgv_subitem["dgv_sub_item_ln", i].Value = gm.toAccountingFormat(dt.Rows[r]["ln_num"].ToString());
                        dgv_subitem["dgv_sub_item_itemdesc", i].Value = dt.Rows[r]["item_desc"].ToString();
                        dgv_subitem["dgv_sub_item_partno", i].Value = dt.Rows[r]["part_no"].ToString();
                        dgv_subitem["dgv_sub_item_qty", i].Value = dt.Rows[r]["qty"].ToString();
                        dgv_subitem["dgv_sub_item_unitdesc", i].Value = dt.Rows[r]["unitid"].ToString();
                        dgv_subitem["dgv_sub_item_costprice", i].Value = gm.toAccountingFormat(dt.Rows[r]["cost_pric"].ToString());
                        dgv_subitem["dgv_sub_item_lnamnt", i].Value = gm.toAccountingFormat(dt.Rows[r]["lnamnt"].ToString());
                        dgv_subitem["dgv_sub_item_unitid", i].Value = dt.Rows[r]["unitid"].ToString();
                        dgv_subitem["dgv_sub_item_itemcode", i].Value = dt.Rows[r]["item_code"].ToString();
                        dgv_subitem["dgv_sub_item_net", i].Value = gm.toAccountingFormat(dt.Rows[r]["net_amount"].ToString());
                    }
                }
                catch (Exception er) { MessageBox.Show(er.Message); }
            }
            else {
                
                DataTable dt = db.QueryBySQLCode("SELECT i2.item_code2 AS dgv_sub_item_itemcode, i.item_desc AS dgv_sub_item_itemdesc, i2.part_no AS dgv_sub_item_partno,i2.qty AS dgv_sub_item_qty,i2.sales_unit_id AS dgv_sub_item_unitid, u.unit_shortcode AS dgv_sub_item_unitdesc, i.unit_cost AS dgv_sub_item_costprice,  i2.qty*i.unit_cost AS dgv_sub_item_lnamnt FROM rssys.items2 i2 LEFT JOIN  rssys.items i ON i.item_code=i2.item_code2 LEFT JOIN rssys.itmunit u ON u.unit_id=i2.sales_unit_id WHERE i2.item_code='" + menu_code + "'");
                int i = 0;
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        for (int r = 0; r < dt.Rows.Count; r++)
                        {
                            i = dgv_subitem.Rows.Add();

                            dgv_subitem["dgv_sub_item_ln", i].Value = line;
                            dgv_subitem["dgv_sub_item_itemdesc", i].Value = dt.Rows[r]["dgv_sub_item_itemdesc"].ToString();
                            dgv_subitem["dgv_sub_item_partno", i].Value = dt.Rows[r]["dgv_sub_item_partno"].ToString();
                            dgv_subitem["dgv_sub_item_qty", i].Value = dt.Rows[r]["dgv_sub_item_qty"].ToString();
                            dgv_subitem["dgv_sub_item_unitdesc", i].Value = dt.Rows[r]["dgv_sub_item_unitdesc"].ToString();
                            dgv_subitem["dgv_sub_item_costprice", i].Value = gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_costprice"].ToString());
                            dgv_subitem["dgv_sub_item_lnamnt", i].Value = gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_lnamnt"].ToString());
                            dgv_subitem["dgv_sub_item_unitid", i].Value = dt.Rows[r]["dgv_sub_item_unitid"].ToString();
                            dgv_subitem["dgv_sub_item_itemcode", i].Value = dt.Rows[r]["dgv_sub_item_itemcode"].ToString();
                            dgv_subitem["dgv_sub_item_net", i].Value = gm.toAccountingFormat((gm.toNormalDoubleFormat(gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_lnamnt"].ToString())) / (((db.get_outlet_govt_pct(cbo_outlet.SelectedValue.ToString())) / 100) + 1)).ToString("0.00"));

                        }
                    }
                    catch (Exception er) { MessageBox.Show(er.Message); }                
                }
            }
        }

        public void set_custvalue_frm(String custcode)
        {
            try { cbo_customer.Items.Clear(); }
            catch { }

            gc.load_customer(cbo_customer);
            cbo_customer.SelectedValue = custcode;
        }
        
        public void set_dealer_frm(String dealercode,String dealername)
        {
            try { cbo_dealer.Items.Clear(); }
            catch { }
            gc.load_company_acct(cbo_dealer);
            cbo_dealer.SelectedValue = dealercode;
        }
        
        public void set_vehicle_frm(String vin_no, String vin_name)
        {
            try { cbo_vehicle.Items.Clear(); }
            catch { }
            gc.load_vehicle_info(cbo_vehicle);
            //cbo_dealer.Text = dealername.ToString();
            cbo_vehicle.SelectedValue = vin_no;
           // MessageBox.Show(dealercode.ToString()+" : "+dealername.ToString());
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
                dt = db.QueryBySQLCode("SELECT * FROM rssys.vehicle_info WHERE vin_no='" + cbo_vehicle.SelectedValue.ToString() + "'");
            }
            catch { }
            //MessageBox.Show(dt.Rows.Count.ToString());
            try
            {
                if (dt.Rows.Count != 0)
                {
                    txt_vin.Text = dt.Rows[0]["vin_no"].ToString();
                    txt_engine.Text = dt.Rows[0]["engine_no"].ToString();
                    txt_plate.Text = dt.Rows[0]["plate_no"].ToString();
                    txt_model.Text = dt.Rows[0]["year_model"].ToString();
                    cbo_insurance.SelectedValue = dt.Rows[0]["insurance"].ToString();
                    dtp_released.Value = gm.toDateValue(dt.Rows[0]["date_release"].ToString());

                    cbo_dealer.SelectedValue = dt.Rows[0]["dealer"].ToString();
                    cbo_brand.SelectedValue = dt.Rows[0]["brand"].ToString();
                    //txt_speednometer.Text = dt.Rows[0]["engine_no"].ToString();
                }
            }
            catch { }
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
            dgv_list.Columns["dgvl_code"].DisplayIndex = 0;
            dgv_list.Columns["dgvl_pending"].DisplayIndex = 1;
            dgv_list.Columns["dgvl_customer"].DisplayIndex = 2;
            dgv_list.Columns["dgvl_ord_date"].DisplayIndex = 3;
            dgv_list.Columns["dgvl_ord_amnt"].DisplayIndex = 4;
            dgv_list.Columns["dgvl_trnx_date"].DisplayIndex = 5;
            dgv_list.Columns["dgvl_amnt_due"].DisplayIndex = 6;
            dgv_list.Columns["dgvl_tax_amnt"].DisplayIndex = 7;
            dgv_list.Columns["dgvl_senior"].DisplayIndex = 8;
            dgv_list.Columns["dgvl_whs_desc"].DisplayIndex = 9;
            dgv_list.Columns["dgvl_user_id2"].DisplayIndex = 10;
            dgv_list.Columns["dgvl_t_date2"].DisplayIndex = 11;
            dgv_list.Columns["dgvl_t_time2"].DisplayIndex = 12;
            dgv_list.Columns["dgvl_jrnlz"].DisplayIndex = 13;
            dgv_list.Columns["dgvl_cancel"].DisplayIndex = 14;
            dgv_list.Columns["dgvl_canc_user"].DisplayIndex = 15;
            dgv_list.Columns["dgvl_canc_date"].DisplayIndex = 16;
            dgv_list.Columns["dgvl_canc_time"].DisplayIndex = 17;
            dgv_list.Columns["dgvl_manual_sc"].DisplayIndex = 18;
            dgv_list.Columns["dgvl_out_code"].DisplayIndex = 19;
            dgv_list.Columns["dgvl_pay_code"].DisplayIndex = 20;
            dgv_list.Columns["dgvl_whs_code"].DisplayIndex = 21;
            dgv_list.Columns["dgvl_debt_code"].DisplayIndex = 22;
            dgv_list.Columns["dgvl_ord_shift"].DisplayIndex = 23;
            dgv_list.Columns["dgvl_pay_shift"].DisplayIndex = 24;
            dgv_list.Columns["dgvl_user_id"].DisplayIndex = 25;
        }

        public void isempty(int count)
        {
            this.count = count;
        }

        private void sub_item_list()
        {
            int r = -1;
            String itemcode = "";
            String ord_code = "";
            String ln = "1";
            DataTable dts, dtc;

            if (dgv_itemlist.Rows.Count > 0)
            {
                try
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    ord_code = txt_ordcode.Text;
                    ln = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                    itemcode = dgv_itemlist["dgvli_itemcode", r].Value.ToString();
                    dts = db.QueryBySQLCode("SELECT * FROM rssys.items2 WHERE item_code='"+itemcode+"'");
                    dtc = db.QueryBySQLCode("SELECT * FROM rssys.orlne2 WHERE ord_code='"+ord_code+"' AND item_col='"+itemcode+"'");
                    
                    if(dts.Rows.Count > 0 || dtc.Rows.Count > 0)
                    {
                        s_sub_items frm_subitem = new s_sub_items(this, itemcode, ord_code, ln, isnew, (cbo_outlet.SelectedValue??"").ToString());
                    }
                    //frm_subitem.ShowDialog();

                }
                catch {  }
            }
        }

        public void disp_sub_item_list(Boolean _is_new)
        { 

            DataTable dtl = null;

            if (_is_new)
            {

                String ord_code = txt_ordcode.Text;
                String ln = "", itemcode = "";

                dgv_subitem.Rows.Clear();

                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    if (!dgv_itemlist["dgvli_unit", r].Value.ToString().ToLower().Equals("set"))
                        continue;

                    Boolean is_new = true;
                    ln = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                    itemcode = dgv_itemlist["dgvli_itemcode", r].Value.ToString();

                    dtl = db.QueryBySQLCode("SELECT DISTINCT o.ord_code, o.item_code_ol, o.item_code, o.part_no, o.item_desc, o.unitid, o.qty, o.cost_pric, o.lnamnt AS lnamnt, o.ln_num ,o.disc_amt, o.sellprice, o.in_tax, o.disc_code, o.disc_user, o.disc_reason, o.notes ,ui.unit_shortcode FROM rssys.orlne2 o LEFT JOIN rssys.itmunit ui ON ui.unit_id=o.unitid WHERE o.ord_code='" + ord_code + "' AND o.item_code_ol='" + itemcode + "' AND o.ln_num='" + ln + "'");

                    if (dtl.Rows.Count == 0)
                    {
                        is_new = false;
                        dtl = db.QueryBySQLCode("SELECT DISTINCT i2.item_code2, i.item_desc, i2.part_no,i2.qty,i2.sales_unit_id, u.unit_shortcode, i.unit_cost,  i2.qty*i.sell_pric AS lnamt, i.sell_pric FROM rssys.items2 i2 LEFT JOIN  rssys.items i ON i.item_code=i2.item_code2 LEFT JOIN rssys.itmunit u ON u.unit_id=i2.sales_unit_id WHERE i2.item_code='" + itemcode + "'");
                    }


                    for (int r2 = 0; dtl.Rows.Count > r2; r2++)
                    {
                        int i = dgv_subitem.Rows.Add();
                        DataGridViewRow row = dgv_subitem.Rows[i];

                        row.Cells["dgv_sub_item_ln"].Value = ln;

                        row.Cells["dgv_sub_item_partno"].Value = dtl.Rows[r2]["part_no"].ToString();
                        row.Cells["dgv_sub_item_itemdesc"].Value = dtl.Rows[r2]["item_desc"].ToString();
                        row.Cells["dgv_sub_item_qty"].Value = dtl.Rows[r2]["qty"].ToString();
                        row.Cells["dgv_sub_item_unitdesc"].Value = dtl.Rows[r2]["unit_shortcode"].ToString();

                        if (is_new)
                        {
                            //row.Cells["dgv_sub_item_costprice"].Value = dtl.Rows[r2]["cost_pric"].ToString();
                            row.Cells["dgv_sub_item_lnamnt"].Value = gm.toAccountingFormat(dtl.Rows[r2]["lnamnt"].ToString());
                            row.Cells["dgv_sub_item_unitid"].Value = dtl.Rows[r2]["unitid"].ToString();
                            row.Cells["dgv_sub_item_itemcode"].Value = dtl.Rows[r2]["item_code"].ToString();
                            row.Cells["dgv_sub_item_net"].Value = gm.toAccountingFormat((gm.toNormalDoubleFormat(gm.toAccountingFormat(dtl.Rows[r2]["lnamnt"].ToString())) / (((db.get_outlet_govt_pct(cbo_outlet.SelectedValue.ToString())) / 100) + 1)).ToString("0.00"));

                            row.Cells["dgv_sub_item_disc_amt"].Value = gm.toAccountingFormat(dtl.Rows[r2]["disc_amt"].ToString());
                            row.Cells["dgv_sub_item_sellprice"].Value = gm.toAccountingFormat(dtl.Rows[r2]["sellprice"].ToString());
                            row.Cells["dgv_sub_item_in_tax"].Value = gm.toAccountingFormat((gm.toNormalDoubleFormat(dtl.Rows[r2]["lnamnt"].ToString()) - gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_net"].Value.ToString())).ToString("0.00"));

                            row.Cells["dgv_sub_item_disc_code"].Value = dtl.Rows[r2]["disc_code"].ToString();
                            row.Cells["dgv_sub_item_disc_user"].Value = dtl.Rows[r2]["disc_user"].ToString();
                            row.Cells["dgv_sub_item_disc_reason"].Value = dtl.Rows[r2]["disc_reason"].ToString();
                            row.Cells["dgv_sub_item_notes"].Value = dtl.Rows[r2]["notes"].ToString();

                        }
                        else
                        {
                            //row.Cells["dgv_sub_item_costprice"].Value = dtl.Rows[r2]["unit_cost"].ToString();
                            row.Cells["dgv_sub_item_lnamnt"].Value = gm.toAccountingFormat(dtl.Rows[r2]["lnamt"].ToString());
                            row.Cells["dgv_sub_item_unitid"].Value = dtl.Rows[r2]["sales_unit_id"].ToString();
                            row.Cells["dgv_sub_item_itemcode"].Value = dtl.Rows[r2]["item_code2"].ToString();
                            row.Cells["dgv_sub_item_net"].Value = gm.toAccountingFormat((gm.toNormalDoubleFormat(gm.toAccountingFormat(dtl.Rows[r2]["lnamnt"].ToString())) / (((db.get_outlet_govt_pct(cbo_outlet.SelectedValue.ToString())) / 100) + 1)).ToString("0.00"));
                            row.Cells["dgv_sub_item_disc_amt"].Value = "0.00";
                            row.Cells["dgv_sub_item_sellprice"].Value = gm.toAccountingFormat(dtl.Rows[r2]["sell_pric"].ToString());
                            row.Cells["dgv_sub_item_in_tax"].Value = gm.toAccountingFormat((gm.toNormalDoubleFormat(dtl.Rows[r2]["lnamt"].ToString()) - gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_net"].Value.ToString())).ToString("0.00"));

                        }

                    }

                }
            }
            else {

                String ord_code = txt_ordcode.Text;

                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    if (!dgv_itemlist["dgvli_unit", r].Value.ToString().ToLower().Equals("set"))
                        continue;

                    Boolean isExist = false;
                    String _lnn_il = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                    String itemcode = dgv_itemlist["dgvli_itemcode", r].Value.ToString();
                    for (int r2 = 0; dgv_subitem.Rows.Count > r2; r2++)
                    {
                        String _lnn_si = dgv_subitem["dgv_sub_item_ln", r2].Value.ToString();
                        if (_lnn_il.Equals(_lnn_si))
                        {
                            isExist = true;
                        }
                    }

                    if (!isExist) {

                        dtl = db.QueryBySQLCode("SELECT DISTINCT i2.item_code2, i.item_desc, i2.part_no,i2.qty,i2.sales_unit_id, u.unit_shortcode, i.unit_cost,  i2.qty*i.sell_pric AS lnamt, i.sell_pric FROM rssys.items2 i2 LEFT JOIN  rssys.items i ON i.item_code=i2.item_code2 LEFT JOIN rssys.itmunit u ON u.unit_id=i2.sales_unit_id WHERE i2.item_code='" + itemcode + "'");

                        for (int r2 = 0; dtl.Rows.Count > r2; r2++)
                        {
                            int i = dgv_subitem.Rows.Add();
                            DataGridViewRow row = dgv_subitem.Rows[i];

                            row.Cells["dgv_sub_item_ln"].Value = _lnn_il;

                            row.Cells["dgv_sub_item_partno"].Value = dtl.Rows[r2]["part_no"].ToString();
                            row.Cells["dgv_sub_item_itemdesc"].Value = dtl.Rows[r2]["item_desc"].ToString();
                            row.Cells["dgv_sub_item_qty"].Value = dtl.Rows[r2]["qty"].ToString();
                            row.Cells["dgv_sub_item_unitdesc"].Value = dtl.Rows[r2]["unit_shortcode"].ToString();

                            //row.Cells["dgv_sub_item_costprice"].Value = dtl.Rows[r2]["unit_cost"].ToString();
                            row.Cells["dgv_sub_item_lnamnt"].Value = dtl.Rows[r2]["lnamt"].ToString();
                            row.Cells["dgv_sub_item_unitid"].Value = dtl.Rows[r2]["sales_unit_id"].ToString();
                            row.Cells["dgv_sub_item_itemcode"].Value = dtl.Rows[r2]["item_code2"].ToString();
                           
                            row.Cells["dgv_sub_item_disc_amt"].Value = "0";
                            row.Cells["dgv_sub_item_sellprice"].Value = dtl.Rows[r2]["sell_pric"].ToString();

                            row.Cells["dgv_sub_item_net"].Value = (gm.toNormalDoubleFormat(dtl.Rows[r2]["lnamt"].ToString()) / (((db.get_outlet_govt_pct(cbo_outlet.SelectedValue.ToString())) / 100) + 1)).ToString("0.00");

                            row.Cells["dgv_sub_item_in_tax"].Value = gm.toNormalDoubleFormat(dtl.Rows[r2]["lnamt"].ToString()) - gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_net"].Value.ToString());

                        }
                    
                    }
                }
            }

        
        }



        private void btn_saveorder_Click_1(object sender, EventArgs e)
        {
            mainsave(false, "", true, "");
        }

        private void dgv_itemlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sub_item_list();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_reload();
            goto_win1();
            disp_list();
        }

        private void btn_exit_Click_1(object sender, EventArgs e)
        {
            frm_reload();
            goto_win1();
            disp_list();
        }

        private void btn_dealer_Click(object sender, EventArgs e)
        {
            m_company frm = new m_company(this, true);
            frm.ShowDialog();
        }

        private void cbo_servicetype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void add_sub_items(String col2,String val2,String ord_code2, String item_code2,String code)
        {
            this.sub_col2 = col2;
            this.sub_val2 = val2;
            this.sub_ord_code2 = ord_code2;
            this.sub_item_code2 = item_code2;
            this.sub_item_code = code;
        }

        private void btn_release_Click(object sender, EventArgs e)
        {
            String status = "";

            if(cbo_status.SelectedIndex != -1)
            {
                status = cbo_status.SelectedValue.ToString();
            }

            //status is ready for release
            if(gm.toNormalDoubleFormat(txt_balance.Text) == 0 && status == "00006")
            {
                isRelease = true;
                mainsave(false, "", false, "");
            }
            else
            {
                isRelease = false;
                MessageBox.Show("Action denied. Balance must be zero and Repair Status must be Ready for Release.");
            }
        }

        private String getComboBoxString(ComboBox cbo) {
            String str = cbo.SelectedIndex != -1 ? "" : cbo.SelectedValue.ToString();
            if (!str.Equals(cbo.Text)) {
                str = cbo.Text;
            }
            return str;
        }

        public void set_last_lnno(int lnno) {
            lnno_last = lnno + 1;
        }

        private void cbo_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String status = cbo_status.SelectedValue.ToString();

                if(status == "00007")
                {
                    lbl_released.Show();
                    dtp_released.ResetText();
                    dtp_released.Show();
                }
                else
                {
                    lbl_released.Hide();
                    dtp_released.Hide();
                }
            }
            catch { }
        }

        private void txt_servex_TextChanged(object sender, EventArgs e)
        {
            if (txt_servex.Text != "")
            {
                chk_disc.Checked = true;
                txt_servex_disc_pct.Text = "10";
                
            }
        }

        private void btn_remark_Click(object sender, EventArgs e)
        {
            Remarks frm = new Remarks(this,txt_ordcode.Text);
            frm.Show();
        }

        private void cbo_outlet_olist_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_list();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //Customer Name
            if(cbo_searchtype.SelectedIndex == 1)
            {

            }
                //Service Service No.
            else if (cbo_searchtype.SelectedIndex == 2)
            {

            }
                //Plate No.
            else if (cbo_searchtype.SelectedIndex == 3)
            {

            }
                //default: Trans. No.
            else 
            {

            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }


        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "d_name";

            try
            {
                dgv_list.ClearSelection();
                searchValue = searchValue.ToUpper();
                //Trans.# dgvl_code
                //Customer Name dgvl_customer
                //Service Reference No.
                //Plate No.
                if (cbo_searchtype.SelectedIndex == 0)
                {
                    typname = "dgvl_code";
                }
                else
                {
                    typname = "dgvl_customer";
                }

                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                        .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                        .First();

                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;

                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[0];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[10];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[11];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[12];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[1];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[2];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[3];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[4];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[5];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[6];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[7];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[8];
                //dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[9];

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
        }

        private void cbo_searchtype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}