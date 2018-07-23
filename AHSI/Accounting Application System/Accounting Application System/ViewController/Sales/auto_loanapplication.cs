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
    public partial class auto_loanapplication : Form
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
        private dbSales db = new dbSales();
        private GlobalClass gc = new GlobalClass();
        private GlobalMethod gm = new GlobalMethod();
        Boolean update = false;
        int lnno_last = 1;
        int lnno_lastpay = 1;
        String stk_trns_type = "SO";//SI or RO
        Boolean isRepair = false, isCashier = false;
        public auto_loanapplication()
        {
            InitializeComponent();
            init_load();   
        }
        private void init_load()
        {
            frm_pclerkpass = new z_clerkpassword(this);
           // gc.load_customer(cbo_clientname);
            gc.load_vehicle_info(cbo_carunit);
            gc.load_marketsegment(cbo_marketsegment);
            //gc.load_userid(cbo_userid);
            gc.load_salesagent(cbo_salesman);
            gc.load_downpayment(cbo_dp_pct);
            gc.load_decision(cbo_status);
           // gc.load_registered_customer(cbo_clientname);
            gc.load_crm(cbo_clientname);
            gc.load_financer(cbo_loaner);
            //load for credit decission

            disp_list();
        }
        public void disp_requrements()
        {

            
        
        
        }
        public void disp_dgv_condition()
        {
           
        
        
        
        }
        private void auto_loanapplication_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }
        }
        public void set_vehicle_frm(String custcode, String custname)
        {

            try { cbo_carunit.Items.Clear(); }
            catch { }

            gc.load_vehicle_info(cbo_carunit);
            //gc.load_customer(cbo_carunit);
            cbo_carunit.SelectedValue = custcode;
        
        }

        private void btn_client_Click(object sender, EventArgs e)
        {
            //m_auto_customer frm = new m_auto_customer();
            //frm.ShowDialog();
            m_auto_customer frm = new m_auto_customer(this, true);
            frm.ShowDialog(); 
        }
        //private void goto_win_outlet()
        //{
        //    seltbp = true;
        //    tbcntrl_left.SelectedTab = tpg_option;
        //    tpg_option.Show();

        //    seltbp = false;

        //    //frm_enable_main_opt(false);
        //}

        private void goto_win1()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option;
            tpg_option.Show();

            tbcntrl_main.SelectedTab = tpg_right_list;
            tpg_right_list.Show();
            seltbp = false;
        }

        public void verifiedClerk(String cid, String cname)
        {
            String outcode = "";
            //cbo_sa.SelectedValue = cid;
            isverified = true;

            //frm_enable(true);

            //try { outcode = cbo_outlet_olist.SelectedValue.ToString(); }
            //catch { }
            
            goto_win2();

            if (isverified)
            {
                m_auto_customer frm = new m_auto_customer(this, true);
                frm.ShowDialog();
            }
        }

        private void goto_win2()
        {
            disp_dgv_condition();
            disp_requrements();
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_2;
            tpg_option_2.Show();

            tbcntrl_main.SelectedTab = tpg_right_entry;
            tpg_right_entry.Show();
            seltbp = false;
        }
        public void set_custvalue_frm(String custcode)
        {
            try { cbo_clientname.Items.Clear(); }
            catch { }

            gc.load_registered_customer(cbo_clientname);
            cbo_clientname.SelectedValue = custcode;
        }
        private void btn_new_Click(object sender, EventArgs e)
        {
            frm_clear();

            isnew = true;
            isnew_item = true;
            lnno_last = 1;
            //cbo_userid.SelectedValue = GlobalClass.username;
            if (isverified == false)
            {
                frm_pclerkpass.ShowDialog();
            }
            else
            {
                MessageBox.Show("Already accessed to sales order.");
                frm_clear();
               
                goto_win2();
                //frm.Show();
            }
        }

        private void cbo_clientname_SelectedIndexChanged(object sender, EventArgs e)
        {
            String val = "";
            DataTable dt = null;
            try
            {
                dt = db.QueryBySQLCode("SELECT d_addr2,d_cntc_no,d_email FROM rssys.m06 WHERE d_code='" + cbo_clientname.SelectedValue.ToString() + "'");
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
        }

        private void btn_vehicle_Click(object sender, EventArgs e)
        {
            //**m_vehiclec_info frm = new m_vehiclec_info(this, true);
            //**frm.Show();
            z_ItemSearch frm = new z_ItemSearch(this,1);
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
            try{ termM = gm.toNormalDoubleFormat(cbo_terms.Text); }
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

            txt_amnt_financed.Text = gm.toAccountingFormat(gm.toNormalDoubleFormat(txt_srpprice.Text) - gm.toNormalDoubleFormat(txt_dpamnt.Text));

        }




        private void cbo_carunit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      
        private void btn_mainsave_Click(object sender, EventArgs e)
        {

            String app_no = "", cust_no = "", cust_name = "", financer = "",financer_value="", credit_des = "", mrtk_segment = "", salesman = "", remark = "", vehicle_code = "", net_cash = "", cond_no = "", dp = "", dp_amt = "", amt_finance = "", terms = "", add_rate = "", monthly_amor = "", pn_amt = "", reg_charges = "", doc_stamps = "", process_fee = "", maximum_sc = "", paid_amt = "", balance = "", reference = "",user_id="",trnx_date="";
            //String ln, rmttyp, ln_num, reg_num, full_name, lne_desc, amount, chg_code, chg_num, chg_desc, fol_code, seq, acct_no, sourcetype = "A";
            String rom_code, arr_date, dep_date;
            String col = "", val = "", add_col = "", add_val = "";
            String table = "autoloandhr";
            String tbl_ln1 = "autoloanlne";
            String tbl_ln2 = "collne2";
            Boolean success = false;
            
            if (cbo_carunit.SelectedIndex == -1 && String.IsNullOrEmpty(cbo_carunit.Text))
            {
                MessageBox.Show("Please select a vehicle.");
                z_ItemSearch frm = new z_ItemSearch(this, 1);
                frm.ShowDialog();
            }
            else if (cbo_loaner.SelectedIndex == -1 && String.IsNullOrEmpty(cbo_loaner.Text))
            {
                MessageBox.Show("Please select financer from the top side datagridview.");  
            }
            else if (cbo_status.SelectedIndex == -1 || cbo_status.Text == "")
            {
                MessageBox.Show("Please select status");
                cbo_status.DroppedDown = true;
            }
            else if (cbo_salesman.SelectedIndex == -1)
            {
                MessageBox.Show("Please select salesman");
                cbo_salesman.DroppedDown = true;
            }
            else if (cbo_marketsegment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select market segment");
                cbo_marketsegment.DroppedDown = true;
            }
            /*else if (cbo_userid.SelectedIndex == -1)
            {
                MessageBox.Show("Please select user");
                cbo_userid.DroppedDown = true;
            }*/
            else if (cbo_dp_pct.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select DP.");
                cbo_dp_pct.DroppedDown = true;
                
            }
            else if (dgv_loaner_list.Rows.Count == 0)
            {
                MessageBox.Show("Please Select Loaner");
                auto_loaner frm = new auto_loaner(this);
                frm.Show();
            }
            else
            {
                app_no = txt_code.Text;
                cust_no = cbo_clientname.SelectedValue.ToString();
                cust_name = cbo_clientname.Text;
                financer = cbo_loaner.Text;
                financer_value = cbo_clientname.SelectedValue.ToString();
                credit_des = cbo_status.Text;
                mrtk_segment = cbo_marketsegment.SelectedValue.ToString();
                salesman = cbo_salesman.SelectedValue.ToString();
                remark = rtxt_remark.Text;
                String financer_id = txt_loaner.Text;
                //financer_id 
                 
                String vehicle_desc = cbo_carunit.Text;
                String item_code = cbo_itemcode.Text;

                String engine_no = txt_engine.Text;
                String vin_no = txt_vin.Text;
                cond_no = txt_condno.Text;

                String reg_chg = gm.toNormalDoubleFormat(txt_srpprice.Text).ToString("0.00");
                dp = cbo_dp_pct.SelectedValue.ToString();
                dp_amt = gm.toNormalDoubleFormat(txt_dpamnt.Text).ToString("0.00");
                amt_finance = gm.toNormalDoubleFormat(txt_amnt_financed.Text).ToString("0.00");
                terms = cbo_terms.Text;
                add_rate = gm.toNormalDoubleFormat(txt_add_on_rate.Text).ToString("0.00");
                monthly_amor = gm.toNormalDoubleFormat(txt_monthly_amort.Text).ToString("0.00");
                pn_amt = gm.toNormalDoubleFormat(txt_clientDP.Text).ToString("0.00");

                String totaldiscount = gm.toNormalDoubleFormat(txt_totaldiscount.Text).ToString("0.00");
                String totalpayment = gm.toNormalDoubleFormat(txt_totalpayment.Text).ToString("0.00");

                trnx_date = dtp_trnxdate.Value.ToString("yyyy-MM-dd");

                if (isnew)
                {
                    app_no = db.get_pk("app_no");
                    // or_ref = "REF#:" + code;
                    //financer_value
                    //app_no, cust_no, cust_name, financer, credit_des, mrtk_segment, salesman, remark, vehicle_desc, car_item_code, engine_no, vin_no, cond_no, dp, dp_amt, amt_finance, terms, add_rate, monthly_amor, pn_amt, totaldiscount, totalpayment, reg_chg

                    //car_engine, car_vin_num, car_plate, car_vin_desc
                    // 

                    col = "app_no, cust_no, cust_name, financer,financer_id , credit_des, mrtk_segment, salesman, remark, car_vin_desc, car_item_code, car_engine, car_vin_num, car_plate, dp, dp_amt, amt_finance, terms, add_rate, monthly_amor, pn_amt, totaldiscount, totalpayment, reg_charges,trnx_date";

                    val = "'" + app_no + "', '" + cust_no + "', '" + cust_name + "', '" + financer + "', '" + financer_id + "', '" + credit_des + "', '" + mrtk_segment + "', '" + salesman + "', " + db.str_E(remark) + ", " + db.str_E(vehicle_desc) + ", '" + item_code + "', " + db.str_E(engine_no) + ", " + db.str_E(vin_no) + ", " + db.str_E(cond_no) + ", '" + dp + "', '" + dp_amt + "', '" + amt_finance + "', '" + terms + "', '" + add_rate + "', '" + monthly_amor + "', '" + pn_amt + "', '" + totaldiscount + "', '" + totalpayment + "', '" + reg_chg + "','" + trnx_date + "'";

                    //col = "app_no, cust_no, cust_name, financer, credit_des, mrtk_segment, salesman, remark, vehicle_code, net_cash, cond_no, dp, dp_amt, amt_finance, terms, add_rate, monthly_amor, pn_amt, reg_charges, doc_stamps, process_fee, maximum_sc, paid_amt, balance, reference, user_id,trnx_date";
                    //val = "'" + app_no + "', '" + cust_no + "', '" + cust_name + "', '" + financer + "', '" + credit_des + "', '" + mrtk_segment + "', '" + salesman + "', '" + remark + "', '" + vehicle_code + "', '" + net_cash + "', '" + cond_no + "', '" + dp + "', '" + dp_amt + "', '" + amt_finance + "', '" + terms + "', '" + add_rate + "', '" + monthly_amor + "', '" + pn_amt + "', '" + reg_charges + "', '" + doc_stamps + "', '" + process_fee + "', '" + maximum_sc + "', '" + paid_amt + "', '" + balance + "', '" + reference + "', '" + user_id + "','" + trnx_date + "'";
                    try
                    {
                        if (db.InsertOnTable(table, col, val))
                        {
                            db.set_pkm99("app_no", db.get_nextincrementlimitchar(app_no, 8));
                            save_approval_data(app_no, true);
                            save_requirements_data(app_no, true);
                            add_financer(app_no, financer, financer_value, cust_no, cust_name);
                            add_payment_items(app_no);
                            success = true;
                            MessageBox.Show("Successfully Saved");
                        }
                        else
                        {
                            db.DeleteOnTable(table, "app_no='" + app_no + "'");
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    catch (Exception er) { MessageBox.Show(er.Message + "\n Insert NEW record Error"); }
                }
                else
                {
                    try
                    {
                        col = "cust_no='" + cust_no + "', cust_name='" + cust_name + "', financer='" + financer + "',financer_id='" + financer_id + "', credit_des='" + credit_des + "', mrtk_segment='" + mrtk_segment + "', salesman='" + salesman + "', remark=" + db.str_E(remark) + ", car_vin_desc=" + db.str_E(vehicle_desc) + ", car_item_code='" + item_code + "', car_engine=" + db.str_E(engine_no) + ", car_vin_num=" + db.str_E(vin_no) + ", car_plate=" + db.str_E(cond_no) + ", dp='" + dp + "', dp_amt='" + dp_amt + "', amt_finance='" + amt_finance + "', terms='" + terms + "', add_rate='" + add_rate + "', monthly_amor='" + monthly_amor + "', pn_amt='" + pn_amt + "', totaldiscount='" + totaldiscount + "', totalpayment='" + totalpayment + "', reg_charges='" + reg_chg + "',trnx_date='" + trnx_date + "'";

                        //col = "app_no='" + app_no + "', cust_no='" + cust_no + "', cust_name='" + cust_name + "', financer='" + financer + "', credit_des='" + credit_des + "', mrtk_segment='" + mrtk_segment + "', salesman='" + salesman + "', remark='" + remark + "', vehicle_code='" + vehicle_code + "', net_cash='" + net_cash + "', cond_no='" + cond_no + "', dp='" + dp + "', dp_amt='" + dp_amt + "', amt_finance='" + amt_finance + "', terms='" + terms + "', add_rate='" + add_rate + "', monthly_amor='" + monthly_amor + "', pn_amt='" + pn_amt + "', reg_charges='" + reg_charges + "', doc_stamps='" + doc_stamps + "', process_fee='" + process_fee + "', maximum_sc='" + maximum_sc + "', paid_amt='" + paid_amt + "', balance='" + balance + "', reference='" + reference + "', user_id='" + user_id + "',trnx_date='" + trnx_date + "'";

                        if (db.UpdateOnTable(table, col, "app_no='" + app_no + "'"))
                        {
                            db.DeleteOnTable("autoloanlne", "app_no='" + app_no + "'");
                            save_approval_data(app_no, false);
                            save_requirements_data(app_no, false);
                            add_financer(app_no, financer, financer_value, cust_no, cust_name);
                            add_payment_items(app_no);
                            success = true;
                        }
                        else
                        {
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    catch (Exception er) { MessageBox.Show(er.Message + "\n Update Error"); }
                }

                if (success)
                {

                    frm_clear();
                    disp_list();
                    goto_win1();
                }
            }
        }



        public void add_financer(String app_no, String m06_finance_name,String m06_code, String custno, String custname)
        {
            String financer_code = "", status = "", line = "",loaner_no="",loaner_name="";
            String val = "", col = "";
            Boolean bol = false;
            col = "financer_code,app_no,m06_finance_code,m06_finance_name,cust_no,cust_name,status,line";

            db.DeleteOnTable("autoloanfinancer", "app_no='" + app_no + "'");
            for (int r = 0; r < dgv_loaner_list.Rows.Count; r++)
            {
                financer_code = db.get_pk("financer_code");
                //app_no = dgv_itemlist["dgvl2_lnno", r].Value.ToString();
                line = dgv_loaner_list["loaner_line", r].Value.ToString();
                loaner_no = dgv_loaner_list["loaner_no", r].Value.ToString();
                loaner_name = dgv_loaner_list["loaner_name", r].Value.ToString();
                status = dgv_loaner_list["loaner_status", r].Value.ToString();

                //try
                //{

                //    bol = Convert.ToBoolean(dgv_loaner_list["dgvlr_chkbox", r].Value.ToString());

                //    status = bol.ToString();

                //}
                //catch (Exception)
                //{
                //    status = bol.ToString(); ;
                //}
                val = "'" + financer_code + "', '" + app_no + "', '" + custno + "', '" + custname + "', '" + loaner_no + "', '" + loaner_name + "','" + status + "','" + line + "'";

                DataTable dtcheck = db.QueryBySQLCode("SELECT * FROM rssys.autoloanfinancer WHERE app_no='" + app_no + "' AND cust_no='"+loaner_no+"' AND line='"+line+"'");
                if(dtcheck.Rows.Count > 0)
                {
                         
                    //db.DeleteOnTable("autoloanlne", "app_no='" + code + "' AND item_code='" + item_code + "' AND type='" + type + "' AND line_no='" + line_no + "'");
                }

                if (db.InsertOnTable("autoloanfinancer", col, val))
                {
                    db.set_pkm99("financer_code", db.get_nextincrementlimitchar(financer_code, 8));
                    //MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Saving Loaner FAILED. ");
                    //notificationText = null;
                }   
            }
        }

        public void add_payment_items(String code)
        {

            String notificationText = null;
            String ln_num, ln_amnt, rep_code, reference, paycode, ln_type, ordcode, chargeto;
            String val2 = "";
            String col2 = "ord_code, ln_num, ln_amnt, pay_code, rep_code, reference, ln_type, chargeto";
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

        public void save_requirements_data(String code, Boolean isnew)
        {
            String item_code = "", item_desc = "", type = "", status = "",line_no="";
            String col = "", val = "";
            Boolean bol = false;
            col = "app_no, item_code, item_desc, type, status, line_no";
            try
            {
                for (int r = 0; r < dgv_docreq.Rows.Count; r++)
                {
                    //app_no = dgv_itemlist["dgvl2_lnno", r].Value.ToString();
                    item_code = dgv_docreq["dgvlr_code", r].Value.ToString();
                    item_desc = dgv_docreq["dgvlr_desc", r].Value.ToString();
                    line_no = dgv_docreq["dgvlr_line", r].Value.ToString();
                    type = "REQ";
                    try
                    {

                        bol = Convert.ToBoolean(dgv_docreq["dgvlr_chkbox", r].Value.ToString());

                        status = bol.ToString();           
                        
                    }
                    catch(Exception)
                    {
                        status = bol.ToString(); ;
                    }


                    val = "'" + code + "', '" + item_code + "', " + db.str_E(item_desc) + ", '" + type + "', '" + status + "','" + line_no + "'";
                    DataTable dtcheck = db.QueryBySQLCode("SELECT * FROM rssys.autoloanlne WHERE app_no='" + code + "' AND item_code='" + item_code + "' AND type='" + type + "' AND line_no='" + line_no + "'");
                    if (dtcheck.Rows.Count > 0)
                    {
                        //db.DeleteOnTable("autoloanlne", "app_no='" + code + "' AND item_code='" + item_code + "' AND type='" + type + "' AND line_no='" + line_no + "'");
                    }

                    if (db.InsertOnTable("autoloanlne", col, val))
                    {
                        //MessageBox.Show("Records of Approval Items Added.");
                    }
                    else
                    {
                        MessageBox.Show("Saving Item of Requirements! FAILED. ");
                        //notificationText = null;
                    }

                }
            }
            catch { }

            //total_amountdue();

            //return notificationText;
        
        }
        public void save_approval_data(String code , Boolean isnew)
        {
            Boolean bol = false;
            String item_code="", item_desc="", type="",status="",line_no="";
            String col = "", val = "";
            col = "app_no, item_code, item_desc, type, status, line_no";
            try
            {
                for (int r = 0; r < dgv_conditions.Rows.Count; r++)
                {
                    //app_no = dgv_itemlist["dgvl2_lnno", r].Value.ToString();
                    item_code = dgv_conditions["dgvlca_code", r].Value.ToString();
                    item_desc = dgv_conditions["dgvlca_description", r].Value.ToString();
                    line_no = dgv_conditions["dgvlca_line", r].Value.ToString();
                    type = "CA";
                    try
                    {

                        bol = Convert.ToBoolean(dgv_conditions["dgvlca_chk", r].Value.ToString());

                        status = bol.ToString();

                    }
                    catch (Exception)
                    {
                        status = bol.ToString(); 
                    }

                    try { 
                        val = "'" + code + "', '" + item_code + "', " + db.str_E(item_desc) + ", '" + type + "', '" + status + "','" + line_no + "'";
                        DataTable dtcheck = db.QueryBySQLCode("SELECT * FROM rssys.autoloanlne WHERE app_no='" + code + "' AND item_code='"+item_code+"' AND type='"+type+"' AND line_no='"+line_no+"'");

                        if (dtcheck.Rows.Count > 0)
                        {
                            //db.DeleteOnTable("autoloanlne", "app_no='" + code + "' AND item_code='"+ item_code+ "' AND type='"+type+"'");
                        }

                        if (db.InsertOnTable("autoloanlne", col, val))
                        {
                            //MessageBox.Show("Records of Approval Items Added.");
                        }
                        else
                        {
                            MessageBox.Show("Saving Item of Approval! FAILED. ");
                            //notificationText = null;
                        }
                    }catch(Exception er){MessageBox.Show(er.Message);}
                }
            }catch { }

            //total_amountdue();

            //return notificationText;
        }
        public void disp_info()
        {

            int r = -1;
            String upd = "";
            String code = "", canc = "";
            isnew = false;
            int lastrow = 0;
            DataTable dt = null;

            if (dgv_list.Rows.Count > 1)
            {

                try
                {

                    r = dgv_list.CurrentRow.Index;
                    code = dgv_list["dgvl_appno", r].Value.ToString();

                    dt = db.QueryBySQLCode("SELECT * FROM rssys.autoloandhr WHERE app_no ='" + code + "'");
                    txt_code.Text = code;

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {    
                            //txt_reference.Text = dt.Rows[i]["reference"].ToString();
                            cbo_clientname.SelectedValue = dt.Rows[i]["cust_no"].ToString();
                            cbo_clientname.Text = dt.Rows[i]["cust_name"].ToString();
                            cbo_loaner.Text = dt.Rows[i]["financer"].ToString();
                            txt_loaner.Text = dt.Rows[i]["financer_id"].ToString();
                            cbo_status.Text = dt.Rows[i]["credit_des"].ToString();
                            cbo_marketsegment.SelectedValue = dt.Rows[i]["mrtk_segment"].ToString();
                            cbo_salesman.SelectedValue = dt.Rows[i]["salesman"].ToString();
                            rtxt_remark.Text = dt.Rows[i]["remark"].ToString();
                            /*txt_doc_stamp.Text = dt.Rows[i]["doc_stamps"].ToString();
                            txt_processing_fee.Text = dt.Rows[i]["process_fee"].ToString();
                            txt_max_sc.Text = dt.Rows[i]["maximum_sc"].ToString();
                            txt_paid_amnt.Text = dt.Rows[i]["paid_amt"].ToString();
                            txt_balance.Text = dt.Rows[i]["balance"].ToString();
                            txt_reference.Text = dt.Rows[i]["reference"].ToString();
                            cbo_userid.SelectedValue = dt.Rows[i]["user_id"].ToString();*/

                            cbo_itemcode.Text = dt.Rows[i]["car_item_code"].ToString();
                            cbo_carunit.Text = dt.Rows[i]["car_vin_desc"].ToString();
                            txt_vin.Text = dt.Rows[i]["car_vin_num"].ToString();
                            txt_engine.Text = dt.Rows[i]["car_engine"].ToString();
                            txt_condno.Text = dt.Rows[i]["car_plate"].ToString();
                            cbo_terms.Text = dt.Rows[i]["terms"].ToString();
                            txt_srpprice.Text = gm.toAccountingFormat(dt.Rows[i]["reg_charges"].ToString());
                            cbo_dp_pct.SelectedValue = dt.Rows[i]["dp"].ToString();
                            txt_dpamnt.Text = gm.toAccountingFormat(dt.Rows[i]["dp_amt"].ToString());
                            txt_amnt_financed.Text = gm.toAccountingFormat(dt.Rows[i]["amt_finance"].ToString());
                            txt_add_on_rate.Text = gm.toAccountingFormat(dt.Rows[i]["add_rate"].ToString());
                            txt_clientDP.Text = gm.toAccountingFormat(dt.Rows[i]["pn_amt"].ToString());
                            txt_totaldiscount.Text = gm.toAccountingFormat(dt.Rows[i]["totaldiscount"].ToString());
                            txt_monthly_amort.Text = gm.toAccountingFormat(dt.Rows[i]["monthly_amor"].ToString());
                            txt_totalpayment.Text = gm.toAccountingFormat(dt.Rows[i]["totalpayment"].ToString());
                           
                        }

                        goto_win2();
                        disp_itemlist(code);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No invoice selected." + code.ToString());
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        
        }
        public void disp_itemlist(String code)
        {

            DataTable itemdt = db.QueryBySQLCode("SELECT * FROM rssys.autoloanlne WHERE app_no='" + code + "' AND type='CA' ORDER by line_no ASC");

            try { dgv_conditions.Rows.Clear(); }
            catch (Exception) { }
            for (int i = 0; i < itemdt.Rows.Count; i++)
            {
                dgv_conditions.Rows.Add();
                dgv_conditions["dgvlca_line", i].Value = itemdt.Rows[i]["line_no"].ToString();
                dgv_conditions["dgvlca_code", i].Value = itemdt.Rows[i]["item_code"].ToString();
                dgv_conditions["dgvlca_description", i].Value = itemdt.Rows[i]["item_desc"].ToString();
                dgv_conditions["dgvlca_type", i].Value = itemdt.Rows[i]["type"].ToString();
            }

            DataTable itemdt2 = db.QueryBySQLCode("SELECT * FROM rssys.autoloanlne WHERE app_no='" + code + "' AND type='REQ'  ORDER by line_no ASC");

            try { dgv_docreq.Rows.Clear(); }
            catch (Exception) { }
            for (int i = 0; i < itemdt2.Rows.Count; i++)
            {
                dgv_docreq.Rows.Add();
                dgv_docreq["dgvlr_line", i].Value = itemdt2.Rows[i]["line_no"].ToString();
                dgv_docreq["dgvlr_code", i].Value = itemdt2.Rows[i]["item_code"].ToString();
                dgv_docreq["dgvlr_desc", i].Value = itemdt2.Rows[i]["item_desc"].ToString();
                dgv_docreq["dgvlr_type", i].Value = itemdt2.Rows[i]["type"].ToString();
            }

            DataTable itemdt3 = db.QueryBySQLCode("SELECT app_no,cust_no,cust_name,status,line FROM rssys.autoloanfinancer WHERE app_no='" + code + "'");
            try { dgv_loaner_list.Rows.Clear(); }
            catch (Exception) { }
            for (int i = 0; i < itemdt3.Rows.Count; i++)
            {
                dgv_loaner_list.Rows.Add();
                dgv_loaner_list["loaner_line", i].Value = itemdt3.Rows[i]["line"].ToString();
                dgv_loaner_list["loaner_no", i].Value = itemdt3.Rows[i]["cust_no"].ToString();
                dgv_loaner_list["loaner_name", i].Value = itemdt3.Rows[i]["cust_name"].ToString();
                dgv_loaner_list["loaner_status", i].Value = itemdt3.Rows[i]["status"].ToString();
            }


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
        
        public void frm_clear()
        {

            txt_code.Text = "";
            cbo_clientname.SelectedIndex = -1;
            cbo_clientname.Text = "";
            cbo_loaner.Text = "";
            cbo_status.Text = "";
            cbo_marketsegment.Text = "";
            cbo_salesman.Text = "";
            rtxt_remark.Text = "";
            cbo_itemcode.Text = "";
            cbo_carunit.SelectedIndex = -1;
            cbo_carunit.Text = "";
            txt_vin.Text = "";
            txt_engine.Text = "";
            txt_condno.Text = "";
            try{ cbo_dp_pct.SelectedIndex = 0; }catch { }
            txt_srpprice.Text = "0.00";
            txt_dpamnt.Text = "0.00";
            txt_amnt_financed.Text = "0.00";
            cbo_terms.Text = "60";
            txt_add_on_rate.Text = "0.00";
            txt_clientDP.Text = "0.00";
            txt_totaldiscount.Text = "0.00";
            txt_monthly_amort.Text = "0.00";
            txt_totalpayment.Text = "0.00";

            /*
            txt_doc_stamp.Text = "";
            txt_processing_fee.Text = "";
            txt_max_sc.Text = "";
            txt_paid_amnt.Text = "";
            txt_balance.Text = "";
            txt_reference.Text = "";
            cbo_userid.SelectedIndex = -1;*/

            try{dgv_conditions.Rows.Clear();}
            catch { }
            try { dgv_docreq.Rows.Clear(); }
            catch { }
            try { dgv_loaner_list.Rows.Clear(); }
            catch { }
            try { dgv_payment.Rows.Clear(); }
            catch { }
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            goto_win1();
            disp_list();
            isverified = false;
        }
        public void disp_list() {

            String search = textBox23.Text;

            DataTable dt = db.QueryBySQLCode("SELECT app_no, cust_no, cust_name, credit_des, mrtk_segment, salesman, remark, vehicle_code, reference, financer, trnx_date FROM rssys.autoloandhr WHERE (trnx_date BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') AND (cust_name like $$%" + search + "%$$ OR financer like $$%" + search + "%$$ OR cust_no like $$%" + search + "%$$ OR app_no like $$%" + search + "%$$ ) ");
           
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
            //AdjustColumnOrder();
        
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            isnew = false;
            isnew_item = true;
            lnno_last = 1;
            this.update = true;
            disp_info();
        }

        private void dgv_docreq_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_addcollection_Click(object sender, EventArgs e)
        {
            auto_loan_items frm = new auto_loan_items(this);
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            auto_loaner frm = new auto_loaner(this);
            frm.Show();
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

        private void dgv_conditions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_docreq_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (dgv_list.Rows.Count > 1)
            {
                int r = dgv_list.CurrentRow.Index;
                String app_no = dgv_list["dgvl_appno", r].Value.ToString();
                if (!String.IsNullOrEmpty(app_no))
                {
                    DataTable dt = db.QueryBySQLCode("SELECT au.*, i.item_desc as _vehicle, b.brd_name as _brand  FROM rssys.autoloandhr au LEFT JOIN rssys.items i ON i.item_code=au.car_item_code LEFT JOIN rssys.brand b ON b.brd_code=i.brd_code WHERE app_no ='" + app_no + "' LIMIT 1");
                    
                    String dp_pct = "", terms = "", sagent = ""
                        , unit = dt.Rows[0]["_vehicle"].ToString()
                        , aor = dt.Rows[0]["add_rate"].ToString()
                        , cashprice = dt.Rows[0]["reg_charges"].ToString()
                        , brand = dt.Rows[0]["_brand"].ToString();

                    cbo_dp_pct.SelectedValue = dt.Rows[0]["dp"].ToString();
                    dp_pct = cbo_dp_pct.Text;
                    cbo_terms.Text = dt.Rows[0]["terms"].ToString();
                    terms = cbo_terms.Text;
                    cbo_salesman.SelectedValue = dt.Rows[0]["salesman"].ToString();
                    sagent = cbo_salesman.Text;

                    Report rpt = new Report();
                    rpt.print_loan_app(app_no, unit, cashprice, dp_pct, terms, aor, sagent, brand);
                    rpt.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No invoice selected.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            disp_list();
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

        private void btn_addpay_Click(object sender, EventArgs e)
        {
            isnew_item = true;
            enter_payment((++lnno_lastpay).ToString(), "", "", "", txt_loaner.Text);
        }
        private void enter_payment(String lnno, String mode, String refe, String amnt,String chargeto)
        {

            if (cbo_clientname.SelectedIndex == -1)
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

                frm_payment = new z_payment(this, isnew_item,chargeto, cbo_clientname.SelectedValue.ToString(), txt_srpprice.Text, txt_totaldiscount.Text, "", true, ln.ToString(), mode, refe, amnt);
                
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

        private void btn_delpay_Click(object sender, EventArgs e)
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
        public void total_amountdue()
        {
            try
            {
                Double paid = 0.00;
                for (int p = 0; p < dgv_payment.Rows.Count; p++){
                    paid += gm.toNormalDoubleFormat(dgv_payment["dgvlp_ln_amnt", p].Value.ToString());
                }
                txt_totalpayment.Text = gm.toAccountingFormat(paid);
            }
            catch { }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_comaker_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void dgv_loaner_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgv_loaner_list.CurrentRow.Index;
                cbo_loaner.Text = dgv_loaner_list["loaner_name", r].Value.ToString();
                cbo_status.Text = dgv_loaner_list["loaner_status", r].Value.ToString();
                txt_loaner.Text = dgv_loaner_list["loaner_no", r].Value.ToString();
            }
            catch { }
        }





    }
}
