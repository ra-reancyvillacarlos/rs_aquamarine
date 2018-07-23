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
    public partial class s_Sales : Form
    {
        private z_clerkpassword frm_pclerkpass;
        private z_payment frm_payment;
        private z_enter_sales_item frm_si;
        private z_so_list frm_solist;
        private m_customers frm_cust;
        private int ln = 1;
        private Boolean isverified = false, isWin2Active = false;
        Boolean seltbp = false;
        private Boolean isnew = false;
        private Boolean isnew_item = true;
        private dbSales db;
        private GlobalClass gc;
        
        private GlobalMethod gm;
        String status = "";
        int lnno_last = 1;
        String stk_trns_type = "SO";//SI or RO
        Boolean isRepair = false, isCashier = false;

        public s_Sales()
        {
            InitializeComponent();
            init_load();
            thisDatabase db2 = new thisDatabase();
            String grp_id2 = "";
            DataTable dt24 = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt24.Rows.Count > 0)
            {
                grp_id2 = dt24.Rows[0]["grp_id"].ToString();
            }
            DataTable dt23 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id2 + "' AND a.mod_id='S1000' ORDER BY b.pla, b.mod_id");

            if (dt23.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt23.Rows[0]["add"].ToString();
                update = dt23.Rows[0]["upd"].ToString();
                delete = dt23.Rows[0]["cancel"].ToString();
                print = dt23.Rows[0]["print"].ToString();

                if (add == "n")
                {
                    btn_additem.Enabled = false;
                }
                if (update == "n")
                {
                    btn_upditem.Enabled = false;
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

        //trns_type=SI, or RO
        public s_Sales(Boolean repair, Boolean cashier)
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
            gc.load_outlet_pos(cbo_outlet, true);
            gc.load_outlet_pos(cbo_outlet_olist, true);
            gc.load_customer(cbo_customer);
            gc.load_salesagent(cbo_agent);

            gc.load_salesclerk(cbo_clerk);
            gc.load_salesclerk(cbo_cashier);
            gc.load_marketsegment(cbo_marketsegment);

            isnew = true;
            frm_reload();
            
            //default value for outlets
            try {
                cbo_outlet.SelectedIndex = 0;
                cbo_outlet_olist.SelectedIndex = 0;
            }
            catch { }

            if (isCashier == true)
            {
                btn_new.Enabled = false;
            }
        }
        
        private void s_Sales_Load(object sender, EventArgs e)
        {            
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }

            frm_enable(true);
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
                /*
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_list.Rows.Add();

                    dgv_list["dgvl_out_code", i].Value = dt.Rows[i]["out_code"].ToString();
                    dgv_list["dgvl_code", i].Value = dt.Rows[i]["ord_code"].ToString();
                    dgv_list["dgvl_customer", i].Value = dt.Rows[i]["customer"].ToString();
                    dgv_list["dgvl_debt_code", i].Value = dt.Rows[i]["debt_code"].ToString();
                    dgv_list["dgvl_mcardid", i].Value = dt.Rows[i]["mcardid"].ToString();
                    dgv_list["dgvl_mkt_code", i].Value = dt.Rows[i]["dgvl_mkt_code"].ToString();
                    dgv_list["dgvl_rep_code", i].Value = dt.Rows[i]["rep_code"].ToString();
                    dgv_list["dgvl_user_id", i].Value = dt.Rows[i]["user_id"].ToString();
                    dgv_list["dgvl_user_id2", i].Value = dt.Rows[i]["user_id2"].ToString();
                    dgv_list["dgvl_ord_date", i].Value = gm.toDateString(dt.Rows[i]["ord_date"].ToString(), "yyyy-MM-dd");
                    dgv_list["dgvl_trnx_date", i].Value = gm.toDateString(dt.Rows[i]["trnx_date"].ToString(), "yyyy-MM-dd");

                    //Monetary
                    dgv_list["dgvl_gross_amnt", i].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[i]["gross_amnt"].ToString()));
                    dgv_list["dgvl_disc_amnt", i].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[i]["disc_amnt"].ToString()));
                    dgv_list["dgvl_netamnt", i].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[i]["net_amnt"].ToString()));
                    dgv_list["dgvl_tax_amnt", i].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(dt.Rows[i]["tax_amnt"].ToString()));
                    total_amnt = gm.toNormalDoubleFormat(dt.Rows[i]["total_amnt"].ToString());
                    paid = gm.toNormalDoubleFormat(dt.Rows[i]["payment"].ToString());
                    dgv_list["dgvl_ord_amnt", i].Value = gm.toAccountingFormat(total_amnt);
                    dgv_list["dgvl_payment", i].Value = gm.toAccountingFormat(paid);
                    dgv_list["dgvl_bal", i].Value = gm.toAccountingFormat(total_amnt - paid);

                    //Car Info    
                    car_item_code = dt.Rows[i]["car_item_code"].ToString();
                    dgv_list["dgvl_car_item_code", i].Value = car_item_code;

                    if (String.IsNullOrEmpty(car_item_code) == false)
                    {
                        dgv_list["dgvl_car_vin_num", i].Value = dt.Rows[i]["car_vin_num"].ToString();
                        dgv_list["dgvl_car_engine", i].Value = dt.Rows[i]["car_engine"].ToString();
                        dgv_list["dgvl_car_plate", i].Value = dt.Rows[i]["car_plate"].ToString();
                        dgv_list["dgvl_car_model", i].Value = dt.Rows[i]["car_model"].ToString();
                        dgv_list["dgvl_last_km_reading", i].Value = dt.Rows[i]["last_km_reading"].ToString();
                        dgv_list["dgvl_dealer_id", i].Value = dt.Rows[i]["dealer_id"].ToString();
                        dgv_list["dgvl_car_brand_id", i].Value = dt.Rows[i]["car_brand_id"].ToString();
                        dgv_list["dgvl_car_color_id", i].Value = dt.Rows[i]["car_color_id"].ToString();
                        dgv_list["dgvl_car_date_release", i].Value = gm.toDateString(dt.Rows[i]["car_date_release"].ToString(), "yyyy-MM-dd");
                        dgv_list["dgvl_warrantyto", i].Value = gm.toDateString(dt.Rows[i]["warrantyto"].ToString(), "yyyy-MM-dd");
                    }
                    
                    //service or for RO    
                    if(isRepair == true)
                    {
                        dgv_list["dgvl_servicetype", i].Value = dt.Rows[i]["servicetype"].ToString();
                        dgv_list["dgvl_promise_date", i].Value = dt.Rows[i]["promise_date"].ToString();
                        dgv_list["dgvl_promise_time", i].Value = dt.Rows[i]["promise_time"].ToString();  
                    }                 
                    
                    //Cancel user
                    dgv_list["dgvl_cancel", i].Value = dt.Rows[i]["cancel"].ToString();
                    dgv_list["dgvl_canc_user", i].Value = dt.Rows[i]["canc_user"].ToString();
                    dgv_list["dgvl_canc_date", i].Value = gm.toDateString(dt.Rows[i]["canc_date"].ToString(), "yyyy-MM-dd"); ;
                    dgv_list["dgvl_canc_time", i].Value = dt.Rows[i]["canc_time"].ToString();

                    dgv_list["dgvl_whs_desc", i].Value = dt.Rows[i]["whs_desc"].ToString(); //whsdesc
                    dgv_list["dgvl_whs_code", i].Value = dt.Rows[i]["loc"].ToString(); //whs_code
                    dgv_list["dgvl_jrnlz", i].Value = dt.Rows[i]["jrnlz"].ToString();
                }
                 * */
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
            if(cbo_outlet_olist.SelectedIndex > -1)
            {
                out_code = cbo_outlet_olist.SelectedValue.ToString();
                WHERE = WHERE + " out_code='"+out_code+"' AND ";
            }
            return db.QueryBySQLCode("SELECT w.whs_code, w.whs_desc, o.out_code,o.ord_code,o.customer,o.payment,o.jrnlz, o.cancel,o.net_amnt,o.ord_date,o.t_date,o.tax_amnt,o.total_amnt AS ord_amnt,o.pending,o.amnt_due,o.disc_amnt, o.user_id,o.loc,o.user_id2,o.rep_code, o.cashier,o.ord_amnt AS gross_amnt,o.pay_code, o.reference, o.agentid, o.market_segment_id, o.mcardid FROM " + db.schema + ".orhdr o LEFT JOIN  " + db.schema + ".whouse w ON w.whs_code=o.loc WHERE " + WHERE + " trnx_date BETWEEN '" + frm.ToString("yyyy-MM-dd") + "' AND '" + to.ToString("yyyy-MM-dd") + "' ORDER BY o.ord_code");
        }
        /*
 Trans Date
Balance
Date CancId
Time CanId
Paid Amount
Jrlndz
Cancelled
Cashier

scc_code
use
Cancel1*/

        public DataTable get_soitemlist(String ord_code)
        {
            DataTable dt = null;

            try
            {
                //dt = this.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit, c.comp_name AS dealer_name, b.brd_name AS car_brand_desc, ct.ctyp_desc AS car_type_desc, clr.color_desc AS car_color_desc FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id LEFT JOIN rssys.company c ON c.comp_code=ol.dealer_id LEFT JOIN rssys.brand b ON b.brd_code=ol.car_brand_id LEFT JOIN rssys.cartype ct ON ct.id=ol.car_type_id LEFT JOIN rssys.color clr ON clr.id=ol.car_color_id WHERE ol.ord_code='" + ord_code + "'");
                dt = db.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit_desc FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id  WHERE ol.ord_code='" + ord_code + "' ORDER BY " + db.castToInteger("ol.ln_num") + " ASC");
            }
            catch { }

            return dt;
        }
       
        public DataTable get_soitemlistpending()
        {
            DataTable dt = null;

            try
            {
                //dt = this.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit, c.comp_name AS dealer_name, b.brd_name AS car_brand_desc, ct.ctyp_desc AS car_type_desc, clr.color_desc AS car_color_desc FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id LEFT JOIN rssys.company c ON c.comp_code=ol.dealer_id LEFT JOIN rssys.brand b ON b.brd_code=ol.car_brand_id LEFT JOIN rssys.cartype ct ON ct.id=ol.car_type_id LEFT JOIN rssys.color clr ON clr.id=ol.car_color_id WHERE ol.ord_code='" + ord_code + "'");
                dt = db.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id  WHERE ol.pending='N' ORDER BY " + db.castToInteger("ol.ln_num") + " ASC");
            }
            catch { }

            return dt;
        }
        private void disp_itemlist(String code)
        {
            string yea = "";
            DataTable dt = get_soitemlist(code);

            try { dgv_itemlist.Rows.Clear(); }
            catch (Exception er) { MessageBox.Show(er.Message); }
            yea = "SELECT ol.*, u.unit_shortcode AS unit FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id  WHERE ol.ord_code='" + code + "' ORDER BY " + db.castToInteger("ol.ln_num") + " ASC";
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
                    dgv_itemlist["dgvli_unit", i].Value = dt.Rows[i]["unit_desc"].ToString();
                    dgv_itemlist["dgvli_qty", i].Value = dt.Rows[i]["ord_qty"].ToString(); //1
                    dgv_itemlist["dgvli_regprice", i].Value = dt.Rows[i]["price"].ToString(); //1
                    dgv_itemlist["dgvli_sellprice", i].Value = dt.Rows[i]["price"].ToString(); // 1
                    dgv_itemlist["dgvli_lnamt", i].Value = dt.Rows[i]["ln_amnt"].ToString(); //1
                   //1
                    dgv_itemlist["dgvli_net", i].Value = (gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", i].Value.ToString()) / 1.12).ToString("0.00");
                    dgv_itemlist["dgvli_taxamt", i].Value = (gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", i].Value.ToString()) - gm.toNormalDoubleFormat(dgv_itemlist["dgvli_net", i].Value.ToString())).ToString("0.00"); 
                    dgv_itemlist["dgvli_discamt", i].Value = dt.Rows[i]["disc_amt"].ToString(); //1
                    dgv_itemlist["dgvli_disccode", i].Value = dt.Rows[i]["disc_code"].ToString(); //1
                    dgv_itemlist["dgvli_discuser", i].Value = dt.Rows[i]["disc_user"].ToString(); //1
                    dgv_itemlist["dgvli_discreason", i].Value = dt.Rows[i]["disc_reason"].ToString(); //1

                    dgv_itemlist["dgvli_unitid", i].Value = dt.Rows[i]["unit"].ToString(); //1
                    dgv_itemlist["dgvli_remarks", i].Value = dt.Rows[i]["reference"].ToString(); //1

                    lnno_last = int.Parse(dt.Rows[i]["ln_num"].ToString());
                }
            }
            catch {  }

            inc_lnno();
        }
        

        public void frm_reload()
        {
            this.ActiveControl = txt_search;

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
            txt_search.Enabled = flag;
            cbo_agent.Enabled = flag;
            cbo_marketsegment.Enabled = flag;
            //cbo_customer.Enabled = flag;
            cbo_agent.Enabled = flag;

            if (isCashier == false) {
                btn_payadd.Enabled = false;
               
            }
           
        }

        private void frm_reset()
        {
            isnew = true;
            isverified = false;
        }

        private void frm_clear()
        {
            txt_totaltax.Text = "0.00";
            txt_search.Text = "";
            txt_ordcode.Text = "";
            cbo_clerk.SelectedIndex = -1;

            try
            {
                cbo_customer.SelectedIndex = -1;
                lbl_custcode.Text = cbo_customer.SelectedValue.ToString();
            }
            catch (Exception) { }


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
            String code = "", canc = "", jrnlz = "";
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
                        jrnlz = dgv_list["dgvl_jrnlz", r].Value.ToString();

                    }
                    catch (Exception err) { MessageBox.Show(canc.ToString()); }

                    code = dgv_list["dgvl_code", r].Value.ToString();


                    if (canc == "Y")
                    {
                        MessageBox.Show("SO# " + code + " is already cancelled. Can not be updated.");
                    }
                    else if (upd == "N")
                    {
                        MessageBox.Show("SO# " + code + " is already done. Can not be updated.");
                        
                    }
                    else if (jrnlz == "Y")
                    {
                        MessageBox.Show("SO# " + code + " is already journalize. Can not be updated.");
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
            String upd = "", canc = "", jrnlz = "";
            int r = -1;


            if (dgv_list.Rows.Count == 0)
            {
                MessageBox.Show("No Rows selected,");
            }
            else
            {
                r = dgv_list.CurrentRow.Index;
                code = dgv_list["dgvl_code", r].Value.ToString();

                try
                {
                    canc = dgv_list["dgvl_cancel", r].Value.ToString();
                    upd = dgv_list["dgvl_pending", r].Value.ToString();
                    jrnlz = dgv_list["dgvl_jrnlz", r].Value.ToString();

                }
                catch (Exception err) { MessageBox.Show(canc.ToString()); }


                if (canc == "Y")
                {
                    MessageBox.Show("SO# " + code + " is already cancelled. Can not be updated.");
                }
                else if (upd == "N")
                {
                    MessageBox.Show("SO# " + code + " is already done. Can not be updated.");
                }
                else if (jrnlz == "Y")
                {
                    MessageBox.Show("SO# " + code + " is already journalize. Can not be updated.");
                }
                else
                {
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
                int r = dgv_list.CurrentRow.Index;
                String trans_no = (dgv_list["dgvl_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(trans_no))
                {
                    String cust_name = dgv_list["dgvl_customer", r].Value.ToString()
                          , stck_loc = dgv_list["dgvl_whs_desc", r].Value.ToString()
                          , t_date = dgv_list["dgvl_trnx_date", r].Value.ToString()
                          , reference = dgv_list["dgvl_reference", r].Value.ToString()
                          , mcardid = dgv_list["dgvl_mcardid", r].Value.ToString()
                          , agent = "", mrk_sgmnt = ""
                          , outlet = cbo_outlet_olist.Text;

                    cbo_agent.SelectedValue = dgv_list["dgvl_agentid", r].Value.ToString();
                    cbo_marketsegment.SelectedValue = dgv_list["dgvl_market_segment_id", r].Value.ToString();
                    agent = cbo_agent.Text;
                    mrk_sgmnt = cbo_marketsegment.Text;

                    Double payment = double.Parse((dgv_list["dgvl_payment", r].Value ?? "0.00").ToString());
                    Double total_amt = double.Parse((dgv_list["dgvl_ord_amnt", r].Value ?? "0.00").ToString());
                    String blnce_due = gm.toAccountingFormat(total_amt - payment);

                    Report rpt = new Report();
                    rpt.print_sales(trans_no, cust_name, stck_loc, reference, mcardid, agent, mrk_sgmnt, outlet, gm.toDateString(t_date, ""), blnce_due, gm.toAccountingFormat(payment));

                    rpt.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No Trans item selected.");
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
            del_item();
        }

        private void btn_saveorder_Click(object sender, EventArgs e)
        {
            mainsave(false, "",true,"");
        }

        //payment or order
        public void mainsave(Boolean isPayment, String pay_code, Boolean pending_status, String amt_tendered)
        {
            Boolean success = false;
            String notificationText = "";
            z_Notification notify = new z_Notification();
            String ord_code, out_code, debt_code, customer, ord_date,ord_qty , ord_amnt, disc_amnt, reference, mcardid, tax_amnt, amnt_due, user_id, t_date, t_time, loc, user_id2, t_date2, t_time2, trnx_date, disc_code, rep_code ="", agentid, amount_paid;
            String branch = GlobalClass.branch, transferred, stk_ref = "", stk_po_so = "",market_segment_id="";
            String cashier = "";
            String pending = "Y";
            String col = "", val = "", col2="", val2="";
            String notifyadd = null;
            String table = "orhdr";
            String tableln = "orlne";

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
            else if (dgv_itemlist.Rows.Count < 1)
            {
                MessageBox.Show("No entry of Sales Item(s). Please add item(s).");
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
                txt_reference.Text = stk_ref;
                reference = txt_reference.Text;
                ord_date = dtp_ord_date.Value.ToString("yyyy-MM-dd");
                //ord_amnt = gm.toNormalDoubleFormat(txt_amtdue.Text).ToString("0.00");
                //disc_amnt = gm.toNormalDoubleFormat(txt_totaltax.Text).ToString("0.00");
                //amnt_tendered = gm.toNormalDoubleFormat(txt_csh_amttendered.Text).ToString("0.00");
                user_id = GlobalClass.username;
                t_date = db.get_systemdate("yyyy-MM-dd");
                t_time = db.get_systemtime();
                market_segment_id = cbo_marketsegment.SelectedValue.ToString();
                amount_paid = gm.toNormalDoubleFormat(amt_tendered).ToString("0.00");

                trnx_date = dtp_trnx_date.Value.ToString("yyyy-MM-dd");


                ord_amnt = gm.toNormalDoubleFormat(txt_grossamount.Text).ToString("0.00");
                String total_amnt = gm.toNormalDoubleFormat(txt_totalamount.Text).ToString("0.00");
                disc_amnt = gm.toNormalDoubleFormat(txt_totaldiscamt.Text).ToString("0.00");
                amount_paid = ((gm.toNormalDoubleFormat(amount_paid) + gm.toNormalDoubleFormat(txt_payment.Text)) * -1 ).ToString("0.00");
                amnt_due = (gm.toNormalDoubleFormat(txt_amtdue.Text) + gm.toNormalDoubleFormat(amount_paid)).ToString("0.00");
                String net_amnt = gm.toNormalDoubleFormat(txt_netamt.Text).ToString("0.00");
                tax_amnt = gm.toNormalDoubleFormat(txt_totaltax.Text).ToString("0.00");
                if(cbo_clerk.SelectedIndex != -1)
                {
                    rep_code = cbo_clerk.SelectedValue.ToString();
                }

                if (isPayment)
                {
                    if(cbo_cashier.SelectedIndex != -1)
                    {
                        cashier = cbo_cashier.SelectedValue.ToString();
                    }
                    /*
                    user_id2 = GlobalClass.username;
                    t_date2 = db.get_systemdate("yyyy-MM-dd");
                    t_time2 = db.get_systemtime();
                    amnt_due = gm.toNormalDoubleFormat(txt_amtdue.Text).ToString("0.00");
                    tax_amnt = (gm.toNormalDoubleFormat(amnt_due) * db.get_outlet_govt_pct(out_code)/100).ToString("0.00");
                    
                    col2 = ", user_id2, t_date2, t_time2, tax_amnt, amnt_due, pay_code, cashier, amnt_tendered";
                    val2 = ", '" + user_id2 + "', '" + t_date2 + "', '" + t_time2 + "', '" + tax_amnt + "', '" + amnt_due + "', '" + pay_code + "', '" + cashier + "', '" + amount_paid + "'";

                    if(isnew == false)
                    {
                        col = ", tax_amnt='" + tax_amnt + "', amnt_due='" + amnt_due + "', user_id2='" + user_id2 + "', t_date2='" + t_date2 + "', t_time2='" + t_time2 + "', pay_code='" + pay_code + "', cashier='" + cashier + "', amnt_tendered='" + amount_paid + "'";
                    }*/
                }

                if (isnew)
                {
                    notificationText = "has added: ";
                    stk_ref = stk_trns_type + "#" + ord_code;
                    stk_po_so = ord_code;
                    txt_reference.Text = stk_ref;
                    reference = txt_reference.Text;
                    ord_code = db.get_ord_code(out_code);

                    col = "ord_code, out_code, debt_code, customer, ord_date, ord_amnt, disc_amnt, payment, amnt_due, net_amnt, tax_amnt, total_amnt, trnx_date, reference, mcardid, user_id, t_date, t_time, loc,rep_code, agentid, branch,market_segment_id, pending" + col2;
                    val = "'" + ord_code + "', '" + out_code + "', '" + debt_code + "', " + db.str_E(customer) + ", '" + ord_date + "', '" + ord_amnt + "', '" + disc_amnt + "', '" + amount_paid + "', '" + amnt_due + "', '" + net_amnt + "', '" + tax_amnt + "', '" + total_amnt + "', '" + trnx_date + "', " + db.str_E(reference) + ", '" + mcardid + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + loc + "', '" + rep_code + "', '" + agentid + "', '" + branch + "','" + market_segment_id + "', '" + pending + "'" + val2;

                    if (db.InsertOnTable(table, col, val))
                    {
                        try {

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
                    if(isPayment)
                    {
                        
                    }
                    //ord_amnt, disc_amnt, amount_paid, amnt_due, net_amnt, tax_amnt, total_amnt
                    notificationText = "has updated: ";
                    col = "ord_code='" + ord_code + "', out_code='" + out_code + "', debt_code='" + debt_code + "', customer='" + customer + "', ord_date='" + ord_date + "', ord_amnt='" + ord_amnt + "', disc_amnt='" + disc_amnt + "',payment='" + amount_paid + "', amnt_due='" + amnt_due + "', net_amnt='" + net_amnt + "', tax_amnt='" + tax_amnt + "', total_amnt='" + total_amnt + "', reference='" + reference + "', mcardid='" + mcardid + "', user_id='" + user_id + "', t_date='" + t_date + "', t_time='" + t_time + "', loc='" + loc + "', trnx_date='" + trnx_date + "', pay_code='" + pay_code + "', cashier='" + cashier + "', rep_code='" + rep_code + "', agentid='" + agentid + "', branch='" + branch + "', pending='" + pending + "' ";

                    if (db.UpdateOnTable(table, col, "ord_code='" + ord_code + "'"))
                    {
                        db.DeleteOnTable(tableln, "ord_code='" + ord_code + "'");
                        db.DeleteOnTable("stkcrd", "po_so='" + ord_code + "' AND trn_type='" + stk_trns_type + "'");
                        try{

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
                if (success)
                {
                    if (isPayment)
                    {
                        frm_payment.Close();
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
                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    ln_num = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                    item_code = dgv_itemlist["dgvli_itemcode", r].Value.ToString();
                    item_desc = dgv_itemlist["dgvli_itemdesc", r].Value.ToString();
                    ord_qty = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_qty", r].Value.ToString()).ToString("0.00");
                    unit = dgv_itemlist["dgvli_unitid", r].Value.ToString();
                    price = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_regprice", r].Value.ToString()).ToString("0.00");
                    ln_amnt = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_lnamt", r].Value.ToString()).ToString("0.00");
                    ln_tax = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_taxamt", r].Value.ToString()).ToString("0.00");
                    net_amount = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_net", r].Value.ToString()).ToString("0.00");
                    reference = txt_reference.Text;
                    trnx_date = dt_trnx;
                    t_time = db.get_systemtime();
                    fcp = db.get_item_fcp(item_code).ToString("0.00");
                    part_no = dgv_itemlist["dgvi_part_no", r].Value.ToString();
                    disc_code = dgv_itemlist["dgvli_disccode", r].Value.ToString();
                    disc_amt = gm.toNormalDoubleFormat(dgv_itemlist["dgvli_discamt", r].Value.ToString()).ToString("0.00");
                    disc_reason = dgv_itemlist["dgvli_discreason", r].Value.ToString();
                    disc_user = dgv_itemlist["dgvli_discuser", r].Value.ToString();

                    if(cbo_clerk.SelectedIndex != -1)
                    {
                        rep_code = cbo_clerk.SelectedValue.ToString();
                    }
                    else
                    {
                        rep_code = "";
                    }

                    val2 = "'" + ord_code + "', '" + ln_num + "', '" + item_code + "', " + db.str_E(item_desc) + ", '" + unit + "', '" + ord_qty + "', '" + price + "', '" + ln_amnt + "', '" + ln_tax + "', '" + rep_code + "', '" + reference + "', '" + trnx_date + "', '" + t_time + "', '" + fcp + "', " + db.str_E(part_no) + ", '" + disc_code + "', '" + disc_amt + "', '" + disc_reason + "', '" + disc_user + "', '" + net_amount + "'";
                    
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
                 
           total_amountdue();

            return notificationText;
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
         
        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            String typ = "D";

            if (isverified == false)
            {
                frm_pclerkpass.ShowDialog();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (db.is_itemcode(txt_search.Text))
                {
                    typ = "C";
                }

                add_item(typ);

                txt_search.Text = "";
            }
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
                        dgv_itemlist.Rows.RemoveAt(i);
                        total_amountdue();
                    }
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }
        
        private void total_amountdue()
        {
            Double amt = 0.00;
            Double damt = 0.00;
            Double tax = 0.00;
            Double paid = 0.00;
            Double net = 0.00;
            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count; i++)
                {
                    amt += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_regprice", i].Value.ToString()) * gm.toNormalDoubleFormat(dgv_itemlist["dgvli_qty", i].Value.ToString());

                    damt += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_discamt", i].Value.ToString());
                    tax += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_taxamt", i].Value.ToString());
                    net += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_net", i].Value.ToString());
                }
            }
            catch {}

            txt_grossamount.Text = gm.toAccountingFormat(amt.ToString());
            txt_totaldiscamt.Text = gm.toAccountingFormat(damt.ToString());
            txt_totalamount.Text = gm.toAccountingFormat((amt - damt).ToString());
            //txt_payment.Text = gm.toAccountingFormat(paid);
            paid = gm.toNormalDoubleFormat(txt_payment.Text);
            txt_amtdue.Text = gm.toAccountingFormat(((amt - damt) + paid).ToString());
            
            txt_netamt.Text = gm.toAccountingFormat(net);
            txt_totaltax.Text = gm.toAccountingFormat(tax.ToString());
        }

        private void enter_payment()
        {
            if (dgv_itemlist.Rows.Count < 0)
            {
                MessageBox.Show("Pls add the sales order item(s).");
            }
            else if (cbo_customer.SelectedIndex == -1) {
                MessageBox.Show("Customer is Null!");
            }
            else
            {
                frm_payment = new z_payment(this, isnew, "", cbo_customer.SelectedValue.ToString(), txt_amtdue.Text, txt_totaldiscamt.Text, cbo_outlet.SelectedValue.ToString());
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
            m_membership frm = new m_membership(this, true);
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

        private void cbo_custname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    lbl_custcode.Text = cbo_customer.SelectedValue.ToString();
                }
            }
            catch (Exception) { MessageBox.Show("Customer Name is not registered."); }
        }

        private void cbo_customer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_vehicle_Click(object sender, EventArgs e)
        {
            m_vehiclec_info frm = new m_vehiclec_info();
            frm.ShowDialog();
        }

        private void cbo_vehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch { }
        }

        public void verifiedClerk(String cid, String cname)
        {
            DataTable dt;
            String outcode = "";
            isverified = true;

            if(isCashier == true && isnew == true)
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

            if(isnew)
            {
                try { outcode = cbo_outlet_olist.SelectedValue.ToString(); }
                catch { }


                if (isverified)
                {
                    m_customers frm = new m_customers(this, true);
                    frm.ShowDialog();
                }
            }
            else if(isnew == false)
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["dgvl_code", r].Value.ToString();

                dt = db.QueryBySQLCode("SELECT * FROM rssys.orhdr WHERE ord_code ='" + code + "'");
                txt_ordcode.Text = code;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        txt_reference.Text = dt.Rows[i]["reference"].ToString();
                        cbo_customer.Text = dt.Rows[i]["customer"].ToString();
                        txt_servex.Text = dt.Rows[i]["mcardid"].ToString();
                        dtp_trnx_date.Value = gm.toDateValue(dt.Rows[i]["trnx_date"].ToString());

                        txt_amtdue.Text = gm.toAccountingFormat(dt.Rows[i]["amnt_due"].ToString());
                        txt_totaltax.Text = gm.toAccountingFormat(dt.Rows[i]["tax_amnt"].ToString());
                        txt_payment.Text = gm.toAccountingFormat(dt.Rows[i]["payment"].ToString());


                        txt_grossamount.Text = gm.toAccountingFormat(dt.Rows[i]["ord_amnt"].ToString());
                        txt_totalamount.Text = gm.toAccountingFormat(dt.Rows[i]["total_amnt"].ToString());
                        txt_netamt.Text = gm.toAccountingFormat(dt.Rows[i]["net_amnt"].ToString());
                        txt_totaldiscamt.Text = gm.toAccountingFormat(dt.Rows[i]["disc_amnt"].ToString());

                        dtp_ord_date.Value = gm.toDateValue(dt.Rows[i]["ord_date"].ToString());
                        cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                        cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                        cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();

                    }
                }

                disp_itemlist(code);
                total_amountdue();
            }

            //frm_enable(true);

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

            dgv_itemlist["dgvli_regprice", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_regprice"].ToString());
            dgv_itemlist["dgvli_discamt", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_discamt"].ToString());
            //1
            dgv_itemlist["dgvli_sellprice", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_sellprice"].ToString());
            
            dgv_itemlist["dgvli_lnamt", i].Value = gm.toAccountingFormat(dt.Rows[0]["dgvli_lnamt"].ToString());
            dgv_itemlist["dgvli_remarks", i].Value = dt.Rows[0]["dgvli_remarks"].ToString();

            dgv_itemlist["dgvli_net", i].Value = (gm.toNormalDoubleFormat(gm.toAccountingFormat(dt.Rows[0]["dgvli_lnamt"].ToString())) / gm.toNormalDoubleFormat("1.12")).ToString("0.00");


            dgv_itemlist["dgvli_taxamt", i].Value = (gm.toNormalDoubleFormat(gm.toAccountingFormat(dt.Rows[0]["dgvli_lnamt"].ToString())) - gm.toNormalDoubleFormat(dgv_itemlist["dgvli_net", i].Value.ToString())).ToString("0.00");

            dgv_itemlist["dgvli_discuser", i].Value = dt.Rows[0]["dgvli_discuser"].ToString();
            dgv_itemlist["dgvli_discreason", i].Value = dt.Rows[0]["dgvli_discreason"].ToString();
            dgv_itemlist["dgvli_disccode", i].Value = dt.Rows[0]["dgvli_disccode"].ToString();

            if (isnew_item)
            {
                inc_lnno();
            }
            //}
            //catch (Exception) { }

            total_amountdue();
        }

        public void set_custvalue_frm(String custcode)
        {
            try { cbo_customer.Items.Clear(); }
            catch { }

            gc.load_customer(cbo_customer);
            cbo_customer.SelectedValue = custcode;
        }

        private void btn_payadd_Click(object sender, EventArgs e)
        {

            enter_payment();

            /*frm_payment = new z_payment(this, isnew, cbo_customer.SelectedValue.ToString(), txt_amtdue.Text, txt_totaltax.Text, cbo_outlet.SelectedValue.ToString());
            frm_payment.ShowDialog();
            this.Refresh();*/
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

                    if(String.IsNullOrEmpty(whs_code) == false)
                    {
                        cbo_whsname.SelectedValue = whs_code;
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
            dgv_list.Columns["dgvl_code"].DisplayIndex = 0;
            dgv_list.Columns["dgvl_customer"].DisplayIndex = 1;
            dgv_list.Columns["dgvl_ord_date"].DisplayIndex = 2;
            dgv_list.Columns["dgvl_trnx_date"].DisplayIndex = 3;
            dgv_list.Columns["dgvl_gross_amnt"].DisplayIndex = 4;
            dgv_list.Columns["dgvl_disc_amnt"].DisplayIndex = 5;
            dgv_list.Columns["dgvl_ord_amnt"].DisplayIndex = 6;
            dgv_list.Columns["dgvl_netamnt"].DisplayIndex = 7;
            dgv_list.Columns["dgvl_tax_amnt"].DisplayIndex = 8;
            dgv_list.Columns["dgvl_payment"].DisplayIndex = 9;
            dgv_list.Columns["dgvl_bal"].DisplayIndex = 10;
            dgv_list.Columns["dgvl_pending"].DisplayIndex = 11;
            dgv_list.Columns["dgvl_user_id"].DisplayIndex = 12;
            dgv_list.Columns["dgvl_user_id2"].DisplayIndex = 13;
            dgv_list.Columns["dgvl_jrnlz"].DisplayIndex = 14;
            dgv_list.Columns["dgvl_cancel"].DisplayIndex = 15;
            dgv_list.Columns["dgvl_canc_user"].DisplayIndex = 16;
            dgv_list.Columns["dgvl_canc_date"].DisplayIndex = 17;
            dgv_list.Columns["dgvl_canc_time"].DisplayIndex = 18;
            dgv_list.Columns["dgvl_out_code"].DisplayIndex = 19;
            
        }

        private void dgv_itemlist_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Menu list ");

           
        }
        private void sub_item_list() {
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

                    s_sub_items frm_subitem = new s_sub_items(this,itemcode, ord_code, ln);

                    //frm_subitem.ShowDialog();


                    //MessageBox.Show(code);

                }
                catch  {  }
            }
        
        
        }

        private void dgv_itemlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sub_item_list();
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

        private void txt_servex_TextChanged(object sender, EventArgs e)
        {
            if (txt_servex.Text != "")
            {
                chk_disc.Checked = true;
                txt_servex_disc_pct.Text = "10";

            }
        }     
    }
}