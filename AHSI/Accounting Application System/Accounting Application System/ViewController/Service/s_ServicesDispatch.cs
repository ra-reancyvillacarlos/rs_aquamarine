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
    public partial class s_ServicesDispatch : Form
    {
        private z_enter_item_simple _frm_eis;
        private z_clerkpassword frm_pclerkpass;
        private Boolean isverified = false, isWin2Active = false;
        DateTime dt_to;
        DateTime dt_frm;
        Boolean isRepair = false, isCashier = false;
        dbSales db;
        GlobalClass gc;
        GlobalMethod gm;
        Boolean seltbp = false;
        Boolean isnew = true;
        Boolean isnew_item = true;
        Boolean isReady = false;
        int lnno_last = 1;
        String stk_trns_type = "I";
        String trns_type = "I";
        
        public s_ServicesDispatch()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbSales();
            //disp_list_perform();
            init_load();
            disp_list();
            isReady = true;
        }
        private void init_load()
        {
            db = new dbSales();
            frm_pclerkpass = new z_clerkpassword(this);
            gc = new GlobalClass();
            gm = new GlobalMethod();

            //gc.load_whouse(cbo_whsname);
            //gc.load_outlet(cbo_outlet);
            //gc.load_outlet(cbo_outlet_olist);
            gc.load_customer(cbo_customer);
            //gc.load_salesagent(cbo_agent);
            //gc.load_salesclerk(cbo_clerk);
            gc.load_technician(cbo_checkedby);
            gc.load_technician(cbo_approvedby);
            gc.load_technician(cbo_drivetest);
           // gc.load_salesclerk(cbo_cashier);
            gc.load_company_acct(cbo_dealer);
            gc.load_repair_orderstatus(cbo_status);
            //gc.load_vehicle_item(cbo_vehicle);
           // gc.load_marketsegment(cbo_marketsegment);
            gc.load_servicetype(cbo_servicetype);
            gc.load_brand(cbo_brand);
            gc.load_branch(cbo_branch);

            //isnew = true;

            cbo_branch.SelectedValue = GlobalClass.branch;
        }

       
        private void s_ServicesDispatch_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            DateTime temp_dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtp_frm.Value = temp_dt;

            dt_frm = dtp_frm.Value;
            dt_to = dtp_to.Value;
            frm_clear();
            disp_list();
        }
        private void frm_clear()
        {
            try
            {
                dgv_itemlist.Rows.Clear();
            }
            catch (Exception) { }

            cbo_vehicle.SelectedIndex = -1;
            cbo_dealer.SelectedIndex = -1;
            cbo_brand.SelectedIndex = -1;
            cbo_servicetype.SelectedIndex = -1;

            txt_plate.Text = "";
            txt_vin.Text = "";
            txt_engine.Text = "";
            txt_model.Text = "";
            txt_speednometer.Text = "";
            rtxt_remark.Text = "";
            rtxt_tech_remarks.Text = "";
            rtxt_remark.Text = "";
            cbo_fa_time.SelectedIndex = -1;

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            lnno_last = 1;
            goto_win2();
        }
        public void verifiedClerk(String cid, String cname)
        {
            DataTable dt;
            String outcode = "";
            isverified = true;

            // MessageBox.Show(isCashier.ToString() + " :: " + isnew.ToString() + " :: " + cid);
            /*
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
            */
            
            if (isnew)
            {
                /*
                try { outcode = cbo_outlet_olist.SelectedValue.ToString(); }
                catch { }


                if (isverified)
                {
                    m_customers frm = new m_customers(this, true);
                    frm.ShowDialog();
                }
                 * */
            }
            else if (isnew == false)
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["dgvi_ord_code", r].Value.ToString();

                dt = db.QueryBySQLCode("SELECT * FROM rssys.orhdr WHERE ord_code ='" + code + "'");
                txt_ordcode.Text = code;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //txt_reference.Text = dt.Rows[i]["reference"].ToString();
                        cbo_customer.Text = dt.Rows[i]["customer"].ToString();
                        dtp_trnx_date.Value = gm.toDateValue(dt.Rows[i]["trnx_date"].ToString());

                        cbo_vehicle.SelectedValue = dt.Rows[i]["car_item_code"].ToString();
                        txt_plate.Text = dt.Rows[i]["car_plate"].ToString();
                        txt_vin.Text = dt.Rows[i]["car_vin_num"].ToString();
                        cbo_servicetype.SelectedValue = dt.Rows[i]["servicetype"].ToString();
                        txt_engine.Text = dt.Rows[i]["car_engine"].ToString();
                        txt_model.Text = dt.Rows[i]["car_model"].ToString();
                        txt_speednometer.Text = dt.Rows[i]["last_km_reading"].ToString();
                        cbo_dealer.SelectedValue = dt.Rows[i]["dealer_id"].ToString();
                        cbo_brand.SelectedValue = dt.Rows[i]["car_brand_id"].ToString();
                        rtxt_remark.Text = dt.Rows[i]["remark"].ToString();
                        rtxt_tech_remarks.Text = dt.Rows[i]["tech_remark"].ToString();
                      
                    }
                }
                
                
               // disp_payment_list(code);
                //total_amountdue();
               
            }

            //frm_enable(true);

            goto_win2();
        }
        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = -1;
            String upd = "";
            String code = "", canc = "";
            isnew = false;
            int lastrow = 0;
            DataTable dt = null;


            frm_clear();

            if (dgv_list.Rows.Count > 0)
            {

                try
                {

                    r = dgv_list.CurrentRow.Index;
                    code = dgv_list["dgvi_ord_code", r].Value.ToString();

                    dt = db.QueryBySQLCode("SELECT * FROM rssys.orhdr WHERE ord_code ='" + code + "'");
                    txt_ordcode.Text = code;

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //txt_reference.Text = dt.Rows[i]["reference"].ToString();
                            cbo_customer.Text = dt.Rows[i]["customer"].ToString();
                            dtp_trnx_date.Value = gm.toDateValue(dt.Rows[i]["trnx_date"].ToString());

                            //txt_amtdue.Text = dt.Rows[i]["ord_amnt"].ToString();
                            //txt_totaltax.Text = dt.Rows[i]["disc_amnt"].ToString();
                            //txt_payment.Text = dt.Rows[i]["payment"].ToString();
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
                            cbo_status.SelectedValue = dt.Rows[i]["status"].ToString();
                            cbo_checkedby.SelectedValue = dt.Rows[i]["checkedby_id"].ToString();
                            cbo_approvedby.SelectedValue = dt.Rows[i]["approvedby_id"].ToString();
                            cbo_drivetest.SelectedValue = dt.Rows[i]["drivetester_id"].ToString();
                            rtxt_tech_remarks.Text = dt.Rows[i]["tech_remark"].ToString();
                            cbo_fa_time.Text = (dt.Rows[i]["fa_time"]??"").ToString();
                            
                            //dtp_ord_date.Value = gm.toDateValue(dt.Rows[i]["ord_date"].ToString());
                            //cbo_marketsegment.SelectedValue = dt.Rows[i]["market_segment_id"].ToString();
                            //cbo_agent.SelectedValue = dt.Rows[i]["agentid"].ToString();
                            //cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                        }

                        disp_list_perform(code);
                        disp_itemlist(code);
                        goto_win2();
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No invoice selected."+ r.ToString());
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DataTable dt;
            String code = "", itemcode = "", itemqty = "0.00", cancel = "Y";
            int r = -1;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                try
                {
                    if (dgv_list["dgvl_cancel", r].Value != null)
                    {
                        cancel = dgv_list["dgvl_cancel", r].Value.ToString();
                    }
                }
                catch (Exception) { }

                if (cancel == "Y")
                {
                    MessageBox.Show("Invalid action. This transaction is already cancelled.");
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        r = dgv_list.CurrentRow.Index;

                        code = dgv_list["dgvl_code", r].Value.ToString();

                        if (db.set_cancel("rechdr", "rec_num='" + code + "'"))
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

                            db.DeleteOnTable("reclne", "rec_num='" + code + "'");
                            db.DeleteOnTable("stkcrd", "po_so='" + code + "' AND trn_type='" + stk_trns_type + "'");

                            disp_list();
                        }
                        else
                        {
                            MessageBox.Show("Cannot be cancelled.");
                        }
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

        }

        private void disp_list()
        {
            var dateFrom = dtp_frm.Value;
            var dateTo = dtp_to.Value;

            DataTable dt;
            String WHERE = "";
            try { dgv_list.Rows.Clear(); } catch { }

            if (cbo_branch.SelectedIndex != -1)
            {
                WHERE += " AND o.branch='" + cbo_branch.SelectedValue.ToString() + "' ";
            }
            if(chk_pending.Checked)
            {
                WHERE += " AND oh.pending='Y' ";
            }

            dt = db.QueryBySQLCode("SELECT o.out_desc , oh.ord_code , oh.customer , oh.ord_date , oh.promise_date , oh.promise_time , oh.status ,	oh.loc, oh.car_plate , oh.car_engine ,  oh.car_vin_num , oh.debt_code , oh.out_code  FROM rssys.orhdr oh LEFT JOIN rssys.outlet o ON oh.out_code=o.out_code WHERE 1=1 AND  trnx_date BETWEEN '" + dateFrom.ToString("yyyy-MM-dd") + "' AND '" + dateTo.ToString("yyyy-MM-dd") + "'  " + WHERE + " ORDER BY oh.ord_date DESC");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int r = dgv_list.Rows.Add();
                DataGridViewRow row = dgv_list.Rows[r];

                row.Cells["dgvi_outlet"].Value = dt.Rows[i]["out_desc"].ToString();
                row.Cells["dgvi_code"].Value = dt.Rows[i]["out_code"].ToString();
                row.Cells["dgvi_customer"].Value = dt.Rows[i]["customer"].ToString();
                row.Cells["dgvi_date_promise"].Value = dt.Rows[i]["promise_time"].ToString();
                cbo_status.SelectedValue = dt.Rows[i]["status"].ToString();
                row.Cells["dgvi_status"].Value = cbo_status.Text;
                row.Cells["dgvi_whs_code"].Value = dt.Rows[i]["loc"].ToString();
                row.Cells["dgvli_car_plate"].Value = dt.Rows[i]["car_plate"].ToString();
                row.Cells["dgvi_ro_date"].Value = dt.Rows[i]["ord_date"].ToString();
                row.Cells["dgvli_car_plate_type"].Value = dt.Rows[i]["car_plate"].ToString();
                row.Cells["dgvli_car_engine"].Value = dt.Rows[i]["car_engine"].ToString();
                row.Cells["dgvli_car_vin_num"].Value = dt.Rows[i]["car_vin_num"].ToString();
                row.Cells["dgvi_custcode"].Value = dt.Rows[i]["debt_code"].ToString();
                row.Cells["dgvi_ord_code"].Value = dt.Rows[i]["ord_code"].ToString();

            }
        }
        private void disp_list_perform(String code)
        {

            DataTable dt;

            dgv_service.Rows.Clear();

            dt = db.QueryBySQLCode("SELECT * FROM rssys.ord_perform WHERE ord_code='" + code + "' ORDER BY srv_line ASC");

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //ord_code srv_code isperform date_perform time_perform tech_code srv_name srv_line
                    dgv_service.Rows.Add();

                    dgv_service["dgvwp_ln", i].Value = dt.Rows[i]["srv_line"].ToString();
                    dgv_service["dgvwp_name", i].Value = dt.Rows[i]["srv_name"].ToString();
                    dgv_service["dgvwp_code", i].Value = dt.Rows[i]["srv_code"].ToString();
                    dgv_service["dgvwp_date", i].Value = gm.toDateString(dt.Rows[i]["date_perform"].ToString(),"");
                    dgv_service["dgvwp_time", i].Value = dt.Rows[i]["time_perform"].ToString();
                    dgv_service["dgvwp_tech", i].Value = dt.Rows[i]["tech_code"].ToString();
                    dgv_service["dgvwp_status", i].Value = dt.Rows[i]["srv_status"].ToString();
                }
            }



            /*
             var dateFrom = dtp_frm.Value.Date;
            var dateTo = dtp_to.Value.Date;
            int lnno = 0;
            int x = 0;
            DataTable dt;

            dgv_service.Rows.Clear();

            dt = db.QueryBySQLCode("SELECT *  FROM rssys.service  ORDER BY srv_name ASC");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int r = dgv_service.Rows.Add();
                x=i;
                DataGridViewRow row = dgv_service.Rows[r];
                row.Cells["dgvwp_name"].Value = dt.Rows[i]["srv_name"].ToString();
                lnno = x+1;
                row.Cells["dgvwp_ln"].Value = lnno;
                
            }
             
             
             */
        }   
        public DataTable get_soitemlist(String ord_code)
        {
            DataTable dt = null;

            try
            {
                //dt = this.QueryBySQLCode("SELECT ol.*, u.unit_shortcode AS unit, c.comp_name AS dealer_name, b.brd_name AS car_brand_desc, ct.ctyp_desc AS car_type_desc, clr.color_desc AS car_color_desc FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id LEFT JOIN rssys.company c ON c.comp_code=ol.dealer_id LEFT JOIN rssys.brand b ON b.brd_code=ol.car_brand_id LEFT JOIN rssys.cartype ct ON ct.id=ol.car_type_id LEFT JOIN rssys.color clr ON clr.id=ol.car_color_id WHERE ol.ord_code='" + ord_code + "'");
                //dt = db.QueryBySQLCode("SELECT ol.*, u.unit_shortcode  FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id  WHERE ol.ord_code='" + ord_code + "' AND ol.item_code<>'' ORDER BY " + db.castToInteger("ol.ln_num") + " ASC");

                dt = db.QueryBySQLCode("SELECT ol.*, u.unit_shortcode FROM rssys.orlne ol LEFT JOIN rssys.itmunit u ON ol.unit=u.unit_id  WHERE ol.ord_code='" + ord_code + "' AND ol.ln_num > '0' ORDER BY " + db.castToInteger("ol.ln_num") + " ASC");

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
                int r = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    dgv_itemlist.Rows.Add();
                    dgv_itemlist["dgvli_lnno", r].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvli_unitid", r].Value = dt.Rows[i]["unit"].ToString();
                    dgv_itemlist["dgvli_unit", r].Value = dt.Rows[i]["unit_shortcode"].ToString();
                    dgv_itemlist["dgvi_part_no", r].Value = dt.Rows[i]["part_no"].ToString(); //1
                    dgv_itemlist["dgvli_itemcode", r].Value = dt.Rows[i]["item_code"].ToString();//1
                    dgv_itemlist["dgvli_itemdesc", r].Value = dt.Rows[i]["item_desc"].ToString();//1
                    //dgv_itemlist["dgvli_unit", i].Value = dt.Rows[i]["unit_desc"].ToString();
                    dgv_itemlist["dgvli_qty", r].Value = dt.Rows[i]["ord_qty"].ToString(); //1     
                    dgv_itemlist["dgvli_remarks", r].Value = dt.Rows[i]["reference"].ToString(); //1

                    r++; // for bases to increament ROW in 'dgv_itemlist'

                    if (!dt.Rows[i]["unit_shortcode"].ToString().ToLower().Equals("set"))
                        continue;

                    DataTable dts = db.QueryBySQLCode("SELECT DISTINCT o.ord_code, o.item_code_ol, o.item_code, o.part_no, o.item_desc, o.unitid, o.qty, o.cost_pric, o.lnamnt AS lnamnt, o.ln_num ,ui.unit_shortcode FROM rssys.orlne2 o LEFT JOIN rssys.itmunit ui ON ui.unit_id=o.unitid WHERE o.ord_code='" + code + "' AND o.item_code_ol='" + dt.Rows[i]["item_code"].ToString() + "'");
                    Boolean is_new = true;
                    if (dts.Rows.Count < 1)
                    {
                        is_new = false;
                        dts = db.QueryBySQLCode("SELECT DISTINCT i2.item_code2, i.item_desc, i2.part_no,i2.qty,i2.sales_unit_id, u.unit_shortcode, i.unit_cost,  i2.qty*i.unit_cost AS lnamt FROM rssys.items2 i2 LEFT JOIN  rssys.items i ON i.item_code=i2.item_code2 LEFT JOIN rssys.itmunit u ON u.unit_id=i2.sales_unit_id WHERE i2.item_code='" + dt.Rows[i]["item_code"].ToString() + "'");
                    }

                    for (int r2 = 0; dts.Rows.Count > r2; r2++)
                    {
                        dgv_itemlist.Rows.Add();

                        dgv_itemlist["dgvli_lnno", r].Value = "*";
                        dgv_itemlist["dgvi_part_no", r].Value = dts.Rows[r2]["part_no"].ToString(); //1
                        dgv_itemlist["dgvli_itemdesc", r].Value = dts.Rows[r2]["item_desc"].ToString();//1
                        dgv_itemlist["dgvli_qty", r].Value = dts.Rows[r2]["qty"].ToString(); //1  
                        dgv_itemlist["dgvli_unit", r].Value = dts.Rows[r2]["unit_shortcode"].ToString();  

                        if (is_new)
                        {
                            dgv_itemlist["dgvli_unitid", r].Value = dts.Rows[r2]["unitid"].ToString();
                            dgv_itemlist["dgvli_itemcode", r].Value = dts.Rows[r2]["item_code"].ToString();
                        }
                        else
                        {
                            dgv_itemlist["dgvli_unitid", r].Value = dts.Rows[r2]["sales_unit_id"].ToString();
                            dgv_itemlist["dgvli_itemcode", r].Value = dts.Rows[r2]["item_code2"].ToString();
                        }

                        r++; // for bases to increament ROW in 'dgv_itemlist'

                    }
                }

                dgv_itemlist.Columns["dgvli_unitid"].DefaultCellStyle.BackColor = Color.LightGray;
                dgv_itemlist.Columns["dgvli_unit"].DefaultCellStyle.BackColor = Color.LightGray;
                dgv_itemlist.Columns["dgvi_part_no"].DefaultCellStyle.BackColor = Color.LightGray;
                dgv_itemlist.Columns["dgvli_itemcode"].DefaultCellStyle.BackColor = Color.LightGray;
                dgv_itemlist.Columns["dgvli_qty"].DefaultCellStyle.BackColor = Color.LightGray;
                dgv_itemlist.Columns["dgvli_itemdesc"].DefaultCellStyle.BackColor = Color.LightGray;

                dgv_itemlist.Columns["dgvli_remarks"].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);

                dgv_itemlist.Columns["dgvli_unitid"].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
                dgv_itemlist.Columns["dgvli_unit"].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
                dgv_itemlist.Columns["dgvi_part_no"].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
                dgv_itemlist.Columns["dgvli_itemcode"].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
                dgv_itemlist.Columns["dgvli_qty"].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
                dgv_itemlist.Columns["dgvli_itemdesc"].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
                dgv_itemlist.Columns["dgvli_remarks"].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);

            }
            catch (Exception er) { MessageBox.Show(er.Message); }
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
                    lnno_last = int.Parse(dgv_itemlist["dgvi_ln", lastrow].Value.ToString());
                    inc_lnno();
                }
            }
            catch (Exception) { }

            //_frm_eis = new z_enter_item_simple(this, isnew_item, lnno_last);
            //_frm_eis.ShowDialog();
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            int r = 0;
            isnew_item = false;

            try
            {
                if (dgv_itemlist.Rows.Count >= 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    //_frm_eis = new z_enter_item_simple(this, isnew_item, r);
                    //_frm_eis.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch
            {
                MessageBox.Show("No item selected.");
            }

        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            String qty = "";

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove?", "Cancel", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    dgv_itemlist.Rows.RemoveAt(r);
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            String notificationText = "has added: ";
            z_Notification notify = new z_Notification();
            Boolean isnew = false;
            String p_ln_num = "", p_name = "" ,p_date="" ,p_time="" , p_tech="";
            Boolean ischecked = false;
            String col = "", val = "";
            String notifyadd = null;
            String table = "rechdr";
            String tableln = "reclne";
            String status = "", checkedby = "", approvedby = "", drivetest = "", tech_remark = "", fa_time="";
            String code = "";
            code = txt_ordcode.Text;
            if (cbo_status.SelectedIndex != -1)
            {
                status = cbo_status.SelectedValue.ToString();
            } 
            if(cbo_checkedby.SelectedIndex!=-1)
            {
                checkedby = cbo_checkedby.SelectedValue.ToString();
            }
            if(cbo_approvedby.SelectedIndex!=-1)
            {
                approvedby = cbo_approvedby.SelectedValue.ToString();
            }
            if (cbo_drivetest.SelectedIndex != -1)
            {
                drivetest = cbo_drivetest.SelectedValue.ToString();
            }
            if (cbo_fa_time.SelectedIndex != -1)
            {
                fa_time = cbo_fa_time.Text;
            }

            //
            tech_remark = rtxt_tech_remarks.Text;

            col = "status='" + status + "', tech_remark='" + tech_remark + "', checkedby_id='" + checkedby + "', approvedby_id='" + approvedby + "', drivetester_id='" + drivetest + "',fa_time='" + fa_time + "'";
            if (db.UpdateOnTable("orhdr", col, "ord_code='" + code + "'"))
            {
                success = true;

                db.DeleteOnTable("ord_perform", "ord_code='" + code + "'");
                add_work(code);

                MessageBox.Show("Record Update Successfully");
                if (success)
                {
                    disp_list();
                    goto_win1();
                    frm_clear();
                } 
            }



          
          
            if (success)
            {
                disp_list();
                goto_win1();
                frm_clear();
            } 
        }
        public void add_work(String code)
        {

            String notificationText = null;

            String srv_line = "", srv_code = "", srv_name = "", date_perform = "", time_perform = "", tech_code = "", srv_status = "";
            String val2 = "";
            String col2 = "ord_code, srv_line ,srv_code ,srv_name ,date_perform, time_perform, tech_code, srv_status";
            

            try
            {
                for (int r = 0; r < dgv_service.Rows.Count; r++)
                {
                   

                    srv_line = dgv_service["dgvwp_ln", r].Value.ToString();
                    srv_code = dgv_service["dgvwp_code", r].Value.ToString();
                    srv_name = dgv_service["dgvwp_name", r].Value.ToString();
                    date_perform = dgv_service["dgvwp_date", r].Value.ToString();
                    time_perform = dgv_service["dgvwp_time", r].Value.ToString();
                    tech_code = dgv_service["dgvwp_tech", r].Value.ToString();
                    srv_status = dgv_service["dgvwp_status", r].Value.ToString();


                    val2 = "'" + code + "', '" + srv_line + "', '" + srv_code + "','" + srv_name + "', '" + date_perform + "', '" + time_perform + "', '" + tech_code + "','" + srv_status + "'";


                    if (db.InsertOnTable("ord_perform", col2, val2))
                    {

                        //MessageBox.Show("Successfully Added");
                    }
                    else
                    {
                        //notificationText = null;
                        MessageBox.Show("Failed Adding Record");
                    }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            //total_amountdue();

            //return notificationText;


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

        private void cbo_costcenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalClass gc = new GlobalClass();

        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
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

        private void btn_additem_Click(object sender, EventArgs e)
        {
            s_work frm = new s_work(this,true);
            frm.ShowDialog();
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_service.Rows.Count == 0)
                {
                    MessageBox.Show("No item selected.");
                }
                else
                {
                    int i;
                    DialogResult dialogResult = MessageBox.Show("Confirm", "Are you sure you want to remove this item?", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        i = dgv_service.CurrentRow.Index;
                        dgv_service.Rows.RemoveAt(i);
                        
                    }
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            try
            {
                s_work frm = new s_work(this, false);
                frm.ShowDialog();
            }
            catch { MessageBox.Show("No selected item."); }
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

        private void dgv_service_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void cbo_branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_branch.SelectedIndex != -1 && isReady)
            {
                disp_list();
            }
        }

        private void chk_pending_CheckedChanged(object sender, EventArgs e)
        {
            disp_list();
        }

        private void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            disp_list();
        }
    }
}
