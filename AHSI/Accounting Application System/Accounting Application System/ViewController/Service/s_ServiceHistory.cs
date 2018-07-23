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
    public partial class s_ServiceHistory : Form
    {
        private z_clerkpassword frm_pclerkpass;
        private z_payment frm_payment;
        private z_enter_sales_item frm_si;
        private call_history frm_call;
        private z_so_list frm_solist;
        private m_customers frm_cust;
        private m_company frm_dealer;
        private int ln = 1;
        private Boolean isverified = false, isWin2Active = false;
        Boolean seltbp = false;
        private Boolean isnew = false;
        private Boolean isnew_item = true;
        String paycode = "", paydesc = "", ln_amnt = "", type = "P", reference = "", rep_code = "";
        private dbSales db;
        private GlobalClass gc;
        String sub_col2 = "", sub_val2 = "", sub_ord_code2 = "", sub_item_code2 = "", sub_item_code = "";
        private GlobalMethod gm;
        private int count;
        int lnno_last = 1;
        String stk_trns_type = "RO";
        Boolean isRepair = false, isCashier = false;
        private int p;
        Boolean isRelease = false;
        public s_ServiceHistory()
        {
            InitializeComponent();
            init_load();
        }
        public s_ServiceHistory(call_history frm, String pk)
        {
            InitializeComponent();
            init_load();
            cbo_custumer1.SelectedValue = pk;
            disp_call_info();
        }
         public s_ServiceHistory(Boolean cashier)
        {
            InitializeComponent();
            isCashier = cashier;
            init_load();            
        }
         private void init_load()
         {
             db = new dbSales();
             // frm_pclerkpass = new z_clerkpassword(this);
             gc = new GlobalClass();
             gm = new GlobalMethod();

             //gc.load_membership(cbo_servex);
             gc.load_technician(cbo_technician);
             gc.load_whouse(cbo_whsname);
             gc.load_outlet(cbo_outlet);
             //gc.load_outlet(cbo_outlet_olist);
             gc.load_customer(cbo_customer);
             gc.load_salesagent(cbo_agent);
             gc.load_salesclerk(cbo_clerk);
             gc.load_salesclerk(cbo_cashier);
             gc.load_company_acct(cbo_dealer);
             gc.load_vehicle_info(cbo_vehicle);
             gc.load_marketsegment(cbo_marketsegment);
             gc.load_servicetype(cbo_servicetype);
             gc.load_repair_orderstatus(cbo_status);
             gc.load_brand(cbo_brand);
             gc.load_customer(cbo_custumer1);
             isnew = true;
             frm_enable(false);
             //frm_enable_main_opt(false);
             //frm_reload();
             //isempty(count);
             disp_list();
             //disp_list_outlet();

             if (isCashier)
             {
                 //btn_additem.Text = "Add Payment";
                 //btn_upditem.Text = "Update Payment";
                 //btn_delitem.Text = "Void Payment";

                 //tbpg_items.SelectedTab = tpgi_payments;
                 //tpgi_payments.Show();
                 //btn_new.Enabled = false;
             }
             else
             {
                 //groupBox3.Visible = true;
                 //btn_release.Hide();
             }
         }
            public s_ServiceHistory(int count)
        {
            // TODO: Complete member initialization
            this.count= count;

        }
            public DataTable get_solist()
            {
                String WHERE = "";
                String out_code = "";

                //if (pendingOnly)
                //{
                //    WHERE = " pending='Y' AND ";
                //}
                //if (cbo_outlet_olist.SelectedIndex > -1)
                //{
                //    out_code = cbo_outlet_olist.SelectedValue.ToString();
                //    WHERE = WHERE + " out_code='" + out_code + "' AND ";
                //}

                return db.QueryBySQLCode("SELECT o.out_code,out.out_desc,o.ord_code,m.d_name as customer,o.payment,o.cancel,o.net_amnt,o.ord_date,o.reference,o.car_plate,o.car_vin_num,o.t_date,o.t_time,o.t_date2,o.tax_amnt,o.pending,o.amnt_due,o.disc_amnt, o.user_id,o.loc,o.user_id2,o.rep_code, o.promise_date,o.promise_time, o.cashier,o.gross_amnt,o.ord_amnt,o.pay_code,o.debt_code as cust_no FROM rssys.orhdr o LEFT JOIN  rssys.whouse w ON w.whs_code=o.loc LEFT JOIN  rssys.m06 m ON m.d_code=o.debt_code LEFT JOIN rssys.outlet out ON o.out_code=out.out_code ORDER BY o.ord_date");
                //m06
            }
            private void disp_list()
            {
                DataTable dt = get_solist();
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
               // AdjustColumnOrder();
            }
            private void frm_clear()
            {
                txt_totaltax.Text = "0.00";
                cbo_servicetype.SelectedIndex = -1;
                cbo_technician.SelectedIndex = -1;
                cbo_clerk.SelectedIndex = -1;
                //cbo_servex.SelectedIndex = -1;
                txt_ordcode.Text = "";

                cbo_vehicle.SelectedIndex = -1;
                txt_plate.Text = "";
                txt_vin.Text = "";
                txt_engine.Text = "";
                txt_model.Text = "";
                txt_speednometer.Text = "";
                //dtp_released.Text = "";

                txt_reference.Text = "";

                cbo_brand.SelectedIndex = -1;
                cbo_dealer.SelectedIndex = -1;
                cbo_status.SelectedIndex = 0;

                cbo_marketsegment.SelectedIndex = -1;
                cbo_agent.SelectedIndex = -1;

                rtxt_remark.Text = "";

                txt_grossamount.Text = "0.00";
                txt_totaldiscamt.Text = "0.00";
                txt_totalamount.Text = "0.00";
                txt_payment.Text = "0.00";
                txt_balance.Text = "0.00";
                txt_netamt.Text = "0.00";
                txt_totaltax.Text = "0.00";

                cbo_clerk.SelectedIndex = -1;
                cbo_cashier.SelectedIndex = -1;

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

                //btn_additem.Enabled = flag;
                //btn_upditem.Enabled = flag;
                //btn_delitem.Enabled = flag;
                //btn_saveorder.Enabled = flag;

                //cbo_servex.Enabled = flag;
                //cbo_agent.Enabled = flag;
                //cbo_marketsegment.Enabled = flag;
                ////cbo_customer.Enabled = flag;
                //cbo_agent.Enabled = flag;

                //if (isCashier == false)
                // {
                //  btn_payadd.Enabled = false;

                //  }

            }
            private void inc_lnno()
            {
                lnno_last = lnno_last + 1;
            }
        private void s_ServiceHistory_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }
        }
          public DataTable get_soitemlist(String ord_code)
        {
            DataTable dt = null;

            try
            {
                //dt = this.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit, c.comp_name AS dealer_name, b.brd_name AS car_brand_desc, ct.ctyp_desc AS car_type_desc, clr.color_desc AS car_color_desc FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id LEFT JOIN rssys.company c ON c.comp_code=ol.dealer_id LEFT JOIN rssys.brand b ON b.brd_code=ol.car_brand_id LEFT JOIN rssys.cartype ct ON ct.id=ol.car_type_id LEFT JOIN rssys.color clr ON clr.id=ol.car_color_id WHERE ol.ord_code='" + ord_code + "'");
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
                //dt = this.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit, c.comp_name AS dealer_name, b.brd_name AS car_brand_desc, ct.ctyp_desc AS car_type_desc, clr.color_desc AS car_color_desc FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id LEFT JOIN rssys.company c ON c.comp_code=ol.dealer_id LEFT JOIN rssys.brand b ON b.brd_code=ol.car_brand_id LEFT JOIN rssys.cartype ct ON ct.id=ol.car_type_id LEFT JOIN rssys.color clr ON clr.id=ol.car_color_id WHERE ol.ord_code='" + ord_code + "'");
                dt = db.QueryBySQLCode("SELECT o.ln_num,o.ln_amnt,o.rep_code,o.reference,o.pay_code,m.mp_desc,o.ln_type FROM rssys.orlne o LEFT JOIN rssys.m10 m ON o.pay_code=m.mp_code WHERE o.ord_code='" + ord_code + "' AND ln_type='P' ORDER BY ln_num ASC");
                
                //dt = db.QueryBySQLCode("SELECT o.ln_num,o.ln_amnt,o.rep_code,o.reference,o.pay_code,m.mp_desc,o.ln_type FROM rssys.orlne o LEFT JOIN rssys.m10 m ON o.pay_code=m.mp_code WHERE o.ord_code='" + ord_code + "' AND ol.ln_num < '0' ORDER BY ln_num ASC");
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

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
                    dgv_itemlist["dgvli_regprice", i].Value = dt.Rows[i]["price"].ToString(); //1
                    dgv_itemlist["dgvli_sellprice", i].Value = dt.Rows[i]["price"].ToString(); // 1
                    dgv_itemlist["dgvli_lnamt", i].Value = dt.Rows[i]["ln_amnt"].ToString(); //1
                    dgv_itemlist["dgvli_taxamt", i].Value = dt.Rows[i]["ln_tax"].ToString(); //1
                    //dgvli_unit
                    dgv_itemlist["dgvli_net", i].Value = dt.Rows[i]["net_amount"].ToString();
                    dgv_itemlist["dgvli_unit", i].Value = dt.Rows[i]["unit_desc"].ToString();
                    dgv_itemlist["dgvli_discamt", i].Value = dt.Rows[i]["disc_amt"].ToString(); //1
                    dgv_itemlist["dgvli_disccode", i].Value = dt.Rows[i]["disc_code"].ToString(); //1
                    dgv_itemlist["dgvli_discuser", i].Value = dt.Rows[i]["disc_user"].ToString(); //1
                    dgv_itemlist["dgvli_discreason", i].Value = dt.Rows[i]["disc_reason"].ToString(); //1

                    dgv_itemlist["dgvli_unitid", i].Value = dt.Rows[i]["unit"].ToString(); //1
                    dgv_itemlist["dgvli_remarks", i].Value = dt.Rows[i]["reference"].ToString(); //1


                    DataTable checkdt = db.QueryBySQLCode("SELECT i2.*, or2.* FROM rssys.orlne2 or2 LEFT JOIN rssys.items2 i2 ON or2.item_code_ol = i2.item_code WHERE or2.ord_code='" + code + "' AND i2.item_code='" + dgv_itemlist["dgvli_itemcode", i].Value .ToString()+ "'");
                    if (checkdt.Rows.Count > 0)
                    {
                        disp_sub(code, dgv_itemlist["dgvli_itemcode", i].Value.ToString(), true, dgv_itemlist["dgvli_lnno", i].Value.ToString());

                    }
                    else
                    {
                        disp_sub(code, dgv_itemlist["dgvli_itemcode", i].Value.ToString(), false, dgv_itemlist["dgvli_lnno", i].Value.ToString());                 
                    }
                    //dgv_sales_subitem(get_subitem(dt.Rows[i]["item_code"].ToString()), dt.Rows[i]["ln_num"].ToString(), true);


                    //DataTable dtcheck = db.QueryBySQLCode("SELECT i2.*, or2.* FROM rssys.items2 i2 LEFT JOIN rssys.orlne2 or2 ON i2.item_code=or2.item_code_ol WHERE i2.item_code='"+dgv_itemlist["dgvli_itemcode",i].Value.ToString()+"'");

                    //if (dtcheck.Rows.Count > 0)
                    //{
                    //    dgv_subitem.Rows.Clear();
                    //    DataTable dtret = db.QueryBySQLCode("SELECT item_code, part_no, item_desc, unitid, qty, cost_pric, lnamnt, ln_num FROM rssys.orlne2 WHERE item_code_ol='" + dgv_itemlist["dgvli_itemcode", i].Value.ToString() + "'");

                    //    if (dtret.Rows.Count > 0)
                    //    {
                    //        for (int r = 0; r < dtret.Rows.Count; r++)
                    //        {
                    //            dgv_subitem["dgv_sub_item_ln", r].Value = dt.Rows[r]["ln_num"].ToString();
                    //            dgv_subitem["dgv_sub_item_partno", r].Value = dt.Rows[r]["part_no"].ToString();
                    //            dgv_subitem["dgv_sub_item_itemdesc", r].Value = dt.Rows[r]["item_desc"].ToString();
                    //            dgv_subitem["dgv_sub_item_qty", r].Value = dt.Rows[r]["qty"].ToString();
                    //            dgv_subitem["dgv_sub_item_unitdesc", r].Value = dt.Rows[r]["unitid"].ToString();
                    //            dgv_subitem["dgv_sub_item_costprice", r].Value = dt.Rows[r]["cost_pric"].ToString();
                    //            dgv_subitem["dgv_sub_item_lnamnt", r].Value = dt.Rows[r]["lnamnt"].ToString();
                    //            dgv_subitem["dgv_sub_item_itemcode", r].Value = dt.Rows[r]["item_code"].ToString();

                    //        }


                    //    }

                    //}
                    //else 
                    //{
                    //    DataTable dtit2 = db.QueryBySQLCode("SELECT i2.item_code2, i2.qty, i.item_desc, i2.sales_unit_id, i2.branch, i2.part_no FROM rssys.items2 i2 LEFT JOIN rssys.items i i.item_code=i2.item_code2 WHERE i2.item_code='" + dgv_itemlist["dgvli_itemcode", i].Value.ToString() + "'");

                    //    if(dtit2.Rows.Count > 0)
                    //    {
                    //        for (int r = 0; r < dtit2.Rows.Count; r++)
                    //        {
                    //            dgv_subitem["dgv_sub_item_ln", r].Value = dgv_itemlist["dgvli_lnno", r].Value.ToString();
                    //            dgv_subitem["dgv_sub_item_partno", r].Value = dt.Rows[r]["part_no"].ToString();
                    //            dgv_subitem["dgv_sub_item_itemdesc", r].Value = dt.Rows[r]["item_desc"].ToString();
                    //            dgv_subitem["dgv_sub_item_qty", r].Value = dt.Rows[r]["qty"].ToString();
                    //            dgv_subitem["dgv_sub_item_unitdesc", r].Value = dt.Rows[r]["sales_unit_id"].ToString();
                    //            //dgv_subitem["dgv_sub_item_costprice", r].Value = dt.Rows[r]["cost_pric"].ToString();
                    //            //dgv_subitem["dgv_sub_item_lnamnt", r].Value = dt.Rows[r]["lnamnt"].ToString();
                    //            dgv_subitem["dgv_sub_item_itemcode", r].Value = dt.Rows[r]["item_code2"].ToString();

                    //        }

                    //    }
                    //}

                    lnno_last = int.Parse(dt.Rows[i]["ln_num"].ToString());
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            inc_lnno();
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

                        dgv_subitem["dgv_sub_item_ln", i].Value = dt.Rows[r]["ln_num"].ToString();
                        dgv_subitem["dgv_sub_item_itemdesc", i].Value = dt.Rows[r]["item_desc"].ToString();
                        dgv_subitem["dgv_sub_item_partno", i].Value = dt.Rows[r]["part_no"].ToString();
                        dgv_subitem["dgv_sub_item_qty", i].Value = dt.Rows[r]["qty"].ToString();
                        dgv_subitem["dgv_sub_item_unitdesc", i].Value = dt.Rows[r]["unitid"].ToString();
                        dgv_subitem["dgv_sub_item_costprice", i].Value = dt.Rows[r]["cost_pric"].ToString();
                        dgv_subitem["dgv_sub_item_lnamnt", i].Value = dt.Rows[r]["lnamnt"].ToString();
                        dgv_subitem["dgv_sub_item_unitid", i].Value = dt.Rows[r]["unitid"].ToString();
                        dgv_subitem["dgv_sub_item_itemcode", i].Value = dt.Rows[r]["item_code"].ToString();
                        dgv_subitem["dgv_sub_item_net", i].Value = dt.Rows[r]["net_amount"].ToString();
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
                            dgv_subitem["dgv_sub_item_costprice", i].Value = dt.Rows[r]["dgv_sub_item_costprice"].ToString();
                            dgv_subitem["dgv_sub_item_lnamnt", i].Value = dt.Rows[r]["dgv_sub_item_lnamnt"].ToString();
                            dgv_subitem["dgv_sub_item_unitid", i].Value = dt.Rows[r]["dgv_sub_item_unitid"].ToString();
                            dgv_subitem["dgv_sub_item_itemcode", i].Value = dt.Rows[r]["dgv_sub_item_itemcode"].ToString();
                            dgv_subitem["dgv_sub_item_net", i].Value = gm.toNormalDoubleFormat(gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_lnamnt"].ToString())) - gm.toNormalDoubleFormat(gm.toAccountingFormat(dt.Rows[r]["dgv_sub_item_in_tax"].ToString()));
                        }
                    }
                    catch (Exception er) { MessageBox.Show(er.Message); }
                
                }
            
            }
        }
         public void disp_sub_item_list(Boolean _is_new) { 

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
                        dtl = db.QueryBySQLCode("SELECT DISTINCT i2.item_code2, i.item_desc, i2.part_no,i2.qty,i2.sales_unit_id, u.unit_shortcode, i.unit_cost,  i2.qty*i.unit_cost AS lnamt, i.fcp FROM rssys.items2 i2 LEFT JOIN  rssys.items i ON i.item_code=i2.item_code2 LEFT JOIN rssys.itmunit u ON u.unit_id=i2.sales_unit_id WHERE i2.item_code='" + itemcode + "'");
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
                            row.Cells["dgv_sub_item_costprice"].Value = dtl.Rows[r2]["cost_pric"].ToString();
                            row.Cells["dgv_sub_item_lnamnt"].Value = dtl.Rows[r2]["lnamnt"].ToString();
                            row.Cells["dgv_sub_item_unitid"].Value = dtl.Rows[r2]["unitid"].ToString();
                            row.Cells["dgv_sub_item_itemcode"].Value = dtl.Rows[r2]["item_code"].ToString();
                            row.Cells["dgv_sub_item_net"].Value = gm.toNormalDoubleFormat(gm.toAccountingFormat(dtl.Rows[r2]["lnamnt"].ToString())) - gm.toNormalDoubleFormat(gm.toAccountingFormat(dtl.Rows[r2]["in_tax"].ToString()));
                            row.Cells["dgv_sub_item_disc_amt"].Value = dtl.Rows[r2]["disc_amt"].ToString();
                            row.Cells["dgv_sub_item_sellprice"].Value = dtl.Rows[r2]["sellprice"].ToString();
                            row.Cells["dgv_sub_item_in_tax"].Value = dtl.Rows[r2]["in_tax"].ToString();
                            row.Cells["dgv_sub_item_disc_code"].Value = dtl.Rows[r2]["disc_code"].ToString();
                            row.Cells["dgv_sub_item_disc_user"].Value = dtl.Rows[r2]["disc_user"].ToString();
                            row.Cells["dgv_sub_item_disc_reason"].Value = dtl.Rows[r2]["disc_reason"].ToString();
                            row.Cells["dgv_sub_item_notes"].Value = dtl.Rows[r2]["notes"].ToString();

                        }
                        else
                        {
                            row.Cells["dgv_sub_item_costprice"].Value = dtl.Rows[r2]["unit_cost"].ToString();
                            row.Cells["dgv_sub_item_lnamnt"].Value = dtl.Rows[r2]["lnamt"].ToString();
                            row.Cells["dgv_sub_item_unitid"].Value = dtl.Rows[r2]["sales_unit_id"].ToString();
                            row.Cells["dgv_sub_item_itemcode"].Value = dtl.Rows[r2]["item_code2"].ToString();
                            row.Cells["dgv_sub_item_net"].Value = gm.toNormalDoubleFormat(gm.toAccountingFormat(dtl.Rows[r2]["lnamnt"].ToString())) - gm.toNormalDoubleFormat(gm.toAccountingFormat(dtl.Rows[r2]["in_tax"].ToString()));
                            row.Cells["dgv_sub_item_disc_amt"].Value = "0";
                            row.Cells["dgv_sub_item_sellprice"].Value = dtl.Rows[r2]["fcp"].ToString();
                            row.Cells["dgv_sub_item_in_tax"].Value = gm.toNormalDoubleFormat(dtl.Rows[r2]["lnamt"].ToString()) * 0.12;

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

                        dtl = db.QueryBySQLCode("SELECT DISTINCT i2.item_code2, i.item_desc, i2.part_no,i2.qty,i2.sales_unit_id, u.unit_shortcode, i.unit_cost,  i2.qty*i.unit_cost AS lnamt FROM rssys.items2 i2 LEFT JOIN  rssys.items i ON i.item_code=i2.item_code2 LEFT JOIN rssys.itmunit u ON u.unit_id=i2.sales_unit_id WHERE i2.item_code='" + itemcode + "'");

                        for (int r2 = 0; dtl.Rows.Count > r2; r2++)
                        {
                            int i = dgv_subitem.Rows.Add();
                            DataGridViewRow row = dgv_subitem.Rows[i];

                            row.Cells["dgv_sub_item_ln"].Value = _lnn_il;

                            row.Cells["dgv_sub_item_partno"].Value = dtl.Rows[r2]["part_no"].ToString();
                            row.Cells["dgv_sub_item_itemdesc"].Value = dtl.Rows[r2]["item_desc"].ToString();
                            row.Cells["dgv_sub_item_qty"].Value = dtl.Rows[r2]["qty"].ToString();
                            row.Cells["dgv_sub_item_unitdesc"].Value = dtl.Rows[r2]["unit_shortcode"].ToString();

                            row.Cells["dgv_sub_item_costprice"].Value = dtl.Rows[r2]["unit_cost"].ToString();
                            row.Cells["dgv_sub_item_lnamnt"].Value = dtl.Rows[r2]["lnamt"].ToString();
                            row.Cells["dgv_sub_item_unitid"].Value = dtl.Rows[r2]["sales_unit_id"].ToString();
                            row.Cells["dgv_sub_item_itemcode"].Value = dtl.Rows[r2]["item_code2"].ToString();
                           
                            row.Cells["dgv_sub_item_disc_amt"].Value = "0";
                            row.Cells["dgv_sub_item_sellprice"].Value = dtl.Rows[r2]["unit_cost"].ToString();
                            row.Cells["dgv_sub_item_in_tax"].Value = gm.toNormalDoubleFormat(dtl.Rows[r2]["lnamt"].ToString()) * 0.12;
                            row.Cells["dgv_sub_item_net"].Value = gm.toNormalDoubleFormat(dtl.Rows[r2]["lnamt"].ToString()) - gm.toNormalDoubleFormat(row.Cells["dgv_sub_item_in_tax"].Value.ToString());

                        }
                    
                    }
                }
            }

        
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
                }
            }
            catch (Exception)
            { }
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
                    amt += gm.toNormalDoubleFormat(dgv_itemlist["dgvli_regprice", i].Value.ToString()) * gm.toNormalDoubleFormat(dgv_itemlist["dgvli_qty", i].Value.ToString());
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

            txt_grossamount.Text = gm.toAccountingFormat(lnamt.ToString());
            txt_totaldiscamt.Text = gm.toAccountingFormat(damt.ToString());
            txt_totalamount.Text = gm.toAccountingFormat((amt - damt).ToString());
            txt_payment.Text = gm.toAccountingFormat(paid);
            txt_balance.Text = gm.toAccountingFormat(((amt - damt) + paid).ToString());

            txt_netamt.Text = gm.toAccountingFormat(net);
            txt_totaltax.Text = gm.toAccountingFormat(tax.ToString());

            if(gm.toNormalDoubleFormat(txt_balance.Text) <= 0.00 && isCashier == true)
            {
                //btn_release.Enabled = true;
            }
            else
            {
                //btn_release.Enabled = false;
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
                    code = dgv_list["dgvl_code", r].Value.ToString();

                    dt = db.QueryBySQLCode("SELECT o.*,out.out_desc,w.whs_desc FROM rssys.orhdr o LEFT JOIN rssys.outlet out ON o.out_code=out.out_code LEFT JOIN rssys.whouse w ON o.loc = w.whs_code WHERE o.ord_code ='" + code + "'");
                    txt_ordcode.Text = code;

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            txt_reference.Text = dt.Rows[i]["reference"].ToString();
                            cbo_customer.SelectedValue = dt.Rows[i]["debt_code"].ToString();
                            //cbo_servex.SelectedValue = dt.Rows[i]["mcardid"].ToString();
                            dtp_trnx_date.Value = gm.toDateValue(dt.Rows[i]["trnx_date"].ToString());

                            txt_balance.Text = dt.Rows[i]["ord_amnt"].ToString();
                            txt_totaltax.Text = dt.Rows[i]["disc_amnt"].ToString();
                            txt_payment.Text = dt.Rows[i]["payment"].ToString();
                            cbo_vehicle.Text = dt.Rows[i]["car_item_code"].ToString();
                            txt_plate.Text = dt.Rows[i]["car_plate"].ToString();
                            txt_vin.Text = dt.Rows[i]["car_vin_num"].ToString();
                            cbo_servicetype.SelectedValue = dt.Rows[i]["servicetype"].ToString();
                            txt_engine.Text = dt.Rows[i]["car_engine"].ToString();
                            txt_model.Text = dt.Rows[i]["car_model"].ToString();
                            txt_speednometer.Text = dt.Rows[i]["last_km_reading"].ToString();
                            cbo_dealer.SelectedValue = dt.Rows[i]["dealer_id"].ToString();
                            cbo_brand.SelectedValue = dt.Rows[i]["car_brand_id"].ToString();
                            rtxt_remark.Text = dt.Rows[i]["remark"].ToString();
                            dtp_ord_date.Value = gm.toDateValue(dt.Rows[i]["ord_date"].ToString());
                            cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                            cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                            cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                            cbo_status.SelectedValue = dt.Rows[i]["status"].ToString();
                            cbo_technician.SelectedValue = dt.Rows[i]["technician"].ToString();
                            cbo_outlet.Text = dt.Rows[i]["out_desc"].ToString();
                            cbo_whsname.Text = dt.Rows[i]["whs_desc"].ToString();
                        }
                        disp_itemlist(code);
                        disp_sub_item_list(true);
                        disp_payment_list(code);
                        total_amountdue();
                        goto_win2();
                        dtp_released.Enabled = false;
                        btn_customer.Enabled = false;
                        btn_crd.Enabled = false;
                        btn_vehicle.Enabled = false;
                        cbo_brand.Enabled = false;
                        dtp_trnx_date.Enabled = false;
                        cbo_servicetype.Enabled = false;
                        cbo_marketsegment.Enabled = false;
                        cbo_agent.Enabled = false;
                        cbo_dealer.Enabled = false;
                        txt_speednometer.ReadOnly = true;
                        cbo_status.Enabled = false;
                        cbo_technician.Enabled = false;
                        dtp_promise_date.Enabled = false;
                        dtp_promisetime.Enabled = false;
                        txt_reference.ReadOnly = true;
                        dtp_ord_date.Enabled = false;
                        rtxt_remark.ReadOnly = true;
                        cbo_brand.Enabled = false;
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
        public void disp_call_info()
        {
            String WHERE = "";
            if (cbo_custumer1.SelectedIndex != -1)
            {
                //MessageBox.Show("Please Select Customer.");
                //cbo_custumer1.DroppedDown = true;
                WHERE = " o.debt_code='" + cbo_custumer1.SelectedValue.ToString() + "' AND ";
            }

            DataTable dt = db.QueryBySQLCode("SELECT o.out_code,out.out_desc,o.ord_code,m.d_name as customer,o.payment,o.cancel,o.net_amnt,o.ord_date,o.t_date,o.reference,o.car_plate,o.car_vin_num,o.t_time,o.t_date2,o.tax_amnt,o.pending,o.amnt_due,o.disc_amnt, o.user_id,o.loc,o.user_id2,o.rep_code, o.promise_date,o.promise_time,o.cashier,o.gross_amnt,o.ord_amnt,o.pay_code,o.debt_code as cust_no FROM rssys.orhdr o LEFT JOIN rssys.whouse w ON w.whs_code=o.loc LEFT JOIN  rssys.m06 m ON m.d_code=o.debt_code LEFT JOIN rssys.outlet out ON o.out_code=out.out_code WHERE " + WHERE + " o.ord_code LIKE '%" + txt_transcode.Text + "%' AND o.reference LIKE '%" + txt_roreference.Text + "%' AND o.car_plate LIKE '%" + txt_plateno.Text + "%' AND o.car_vin_num LIKE '%" + txt_vinno.Text + "%' ORDER BY o.ord_date");

            //MessageBox.Show(cbo_custumer1.SelectedValue.ToString());
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
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String WHERE = "";
            if (cbo_custumer1.SelectedIndex != -1)
            {
                //MessageBox.Show("Please Select Customer.");
                //cbo_custumer1.DroppedDown = true;
                WHERE = " o.debt_code='" + cbo_custumer1.SelectedValue.ToString() + "' AND ";
            }

            DataTable dt = db.QueryBySQLCode("SELECT o.out_code,out.out_desc,o.ord_code,m.d_name as customer,o.payment,o.cancel,o.net_amnt,o.ord_date,o.t_date,o.reference,o.car_plate,o.car_vin_num,o.t_time,o.t_date2,o.tax_amnt,o.pending,o.amnt_due,o.disc_amnt, o.user_id,o.loc,o.user_id2,o.rep_code, o.promise_date,o.promise_time,o.cashier,o.gross_amnt,o.ord_amnt,o.pay_code,o.debt_code as cust_no FROM rssys.orhdr o LEFT JOIN rssys.whouse w ON w.whs_code=o.loc LEFT JOIN  rssys.m06 m ON m.d_code=o.debt_code LEFT JOIN rssys.outlet out ON o.out_code=out.out_code WHERE " + WHERE + " o.ord_code LIKE '%" + txt_transcode.Text + "%' AND o.reference LIKE '%" + txt_roreference.Text + "%' AND o.car_plate LIKE '%" + txt_plateno.Text + "%' AND o.car_vin_num LIKE '%" + txt_vinno.Text + "%' ORDER BY o.ord_date");

            //MessageBox.Show(cbo_custumer1.SelectedValue.ToString());
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
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cbo_custumer1.SelectedIndex = -1;
            disp_list();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            frm_reload();
            goto_win1();
            disp_list();
        }
        
        public void frm_reload()
        {
            frm_reset();
            frm_clear();
            total_amountdue();
            frm_enable(true);
        }
        private void frm_reset()
        {
            isnew = true;
            isverified = false;
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            int r = dgv_itemlist.CurrentRow.Index;
            if (dgv_itemlist["dgvli_itemcode", r].Value.ToString().ToLower().Equals("text-item"))
            {
                //z_enter_item_simple frm = new z_enter_item_simple(this, false, (int)double.Parse(dgv_itemlist["dgvli_lnno", r].Value.ToString()));
                //frm.ShowDialog();
            }
            else
            {
                upd_item();
            }
        }
        public void upd_item()
        {
            int r = 0;

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    z_enter_sales_item frm_si = new z_enter_sales_item(this, false,"","", r);

                    frm_si.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void dgv_itemlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sub_item_list();
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
                    DataTable dts = db.QueryBySQLCode("SELECT * FROM rssys.items2 WHERE item_code='" + itemcode + "'");
                    DataTable dtc = db.QueryBySQLCode("SELECT * FROM rssys.orlne2 WHERE ord_code='" + ord_code + "' AND item_col='" + itemcode + "'");
                    if (dts.Rows.Count > 0 || dtc.Rows.Count > 0)
                    {
                        s_sub_items frm_subitem = new s_sub_items(this, itemcode, ord_code, ln, isnew);
                    }
                    //frm_subitem.ShowDialog();


                    //MessageBox.Show(code);

                }
                catch { }
            }
        }


       
    }
}
