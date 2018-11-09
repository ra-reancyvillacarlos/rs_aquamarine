using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class enterDeposit : Form
    {
        thisDatabase db;
        GlobalMethod gm;
        Arrivals frmArrivals;
        String action = "";
        String gfolio = "";
        newArrivalWalkin newfrmArrivals;
        newReservation newReserv;
        String cur_res_code = "";
        String cur_c_c = "";
        String cur_p_c = "";
        String cur_ent_c = "";
        int cur_i = 0;
        public enterDeposit(Arrivals _frmArr)
        {
            InitializeComponent();

            db = new thisDatabase();
            gm = new GlobalMethod();
            frmArrivals = _frmArr;

            //gm.load_charge_paymentsonly(cbo_appaymentform);
            //gm.load_charge_depositonly(cbo_dpaymentform);
            //gm.load_currency_code(cbo_dcurrency);
            gm.load_hotel(cbo_dpaymentform);
            gm.load_trns(comboBox1);
            gm.load_charge_paymentsonly(comboBox2);
        }
        public enterDeposit(newArrivalWalkin _frmArr, String gfolio, String action)
        {
            InitializeComponent();

            db = new thisDatabase();
            gm = new GlobalMethod();
            ld_ent();

            gm.load_hotel(cbo_dpaymentform);
            gm.load_trns(comboBox1);
            gm.load_charge_paymentsonly(comboBox2);

        }
        public enterDeposit(newReservation _frmArr)
        {
            InitializeComponent();

            db = new thisDatabase();
            gm = new GlobalMethod();
            ld_ent();

            gm.load_hotel(cbo_dpaymentform);
            gm.load_trns(comboBox1);
            gm.load_charge_paymentsonly(comboBox2);

        }
        private void enterDeposit_Load(object sender, EventArgs e)
        {
            if (cur_res_code == "") { } else { load_items(cur_res_code); }
        }

        private void ld_ent()
        {
            Double cur_ent = 0.00;

            DataTable dt_ent = db.QueryBySQLCode("SELECT chg_code, chg_desc, price FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ADTL%' AND UPPER(chg_desc) LIKE 'ENTRANCE%'");
            cur_ent_c = dt_ent.Rows[0]["chg_code"].ToString();
            try
            {
                cur_ent = Convert.ToDouble(dt_ent.Rows[0]["price"].ToString());
            }
            catch {  }

            label27.Text = gm.toAccountingFormat(cur_ent);
        }

        void disp_payment(String gfolio)
        {
        
        }
        private void btn_checkin_Click(object sender, EventArgs e)
        {
            DataTable dt_upch = db.QueryBySQLCode("SELECT chg.chg_code, chg_num FROM rssys.res_gfil rf LEFT JOIN rssys.charge chg ON chg.chg_code = rf.chg_code LEFT JOIN rssys.gfolio gf ON  gf.res_code = rf.rg_code WHERE rg_code = '" + cur_res_code + "'");

            DataTable dt_upfg = db.QueryBySQLCode("SELECT ch.chg_code, chg_num FROM rssys.gfolio gf LEFT JOIN rssys.charge ch ON ch.chg_code = gf.chg_code WHERE res_code = '" + cur_res_code + "'");

            if (textBox1.Text.Length > 0)
            {
                Double price = 0.00;
                Double amnt = 0.00;
                Double total = 0.00;
                try { price = Convert.ToDouble(label9.Text.ToString()); }
                catch { }
                try { amnt = Convert.ToDouble(textBox1.Text.ToString()); }
                catch { MessageBox.Show("Input a valid number."); }
                total = amnt - price;
                if (total < 0)
                {
                    MessageBox.Show("Amount must be equal or larger to price.");
                }
                else
                {
                    if (db.UpdateOnTable("gfolio", "amnt = '" + amnt + "'", "res_code = '" + cur_res_code + "'"))
                    {
                        if (db.QueryBySQLCode_bool("INSERT INTO rssys.chgfil (reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, chg_date, res_code, food, misc, vat_amnt, sc_amnt, tax, bill_amnt) SELECT gf.reg_num, chg.chg_code, chg_num, gf.rom_code, MIN(CONCAT(SPLIT_PART(rf.occ_type, ' ', 1), ' ', chg.chg_desc, ' (', gf.full_name, ')')) AS reference, SUM(rf.price) AS amount, gf.user_id, current_date AS t_date, '" + DateTime.Now.ToString("HH:mm") + "' AS t_time, current_date AS chg_date, rf.rg_code, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 FROM rssys.res_gfil rf LEFT JOIN rssys.charge chg ON chg.chg_code = rf.chg_code LEFT JOIN rssys.gfolio gf ON  gf.res_code = rf.rg_code WHERE rg_code = '" + cur_res_code + "' GROUP BY gf.reg_num, chg.chg_code, chg_num, gf.rom_code, gf.user_id, rf.rg_code"))
                        {
                            if(dt_upch.Rows.Count > 0)
                            {
                                for(int i = 0; i < dt_upch.Rows.Count; i++)
                                {
                                    try
                                    {
                                        db.set_all_pk("charge", "chg_num", dt_upch.Rows[i]["chg_num"].ToString(), "chg_code='" + dt_upch.Rows[i]["chg_code"].ToString() + "'", dt_upch.Rows[i]["chg_num"].ToString().Length);
                                    }
                                    catch { }
                                }
                            }
                            if (db.QueryBySQLCode_bool("INSERT INTO rssys.chgfil (reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, chg_date, res_code, food, misc, vat_amnt, sc_amnt, tax, bill_amnt) SELECT reg_num, ch.chg_code, chg_num, rom_code, CONCAT('PAYMENT (', gf.full_name, ')') AS reference, (amnt * -1) AS amount, user_id, current_date AS t_date, '" + DateTime.Now.ToString("HH:mm") + "' AS t_time, current_date AS chg_date, res_code, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 FROM rssys.gfolio gf LEFT JOIN rssys.charge ch ON ch.chg_code = gf.p_typ WHERE res_code = '" + cur_res_code + "'"))
                            {
                                if (dt_upfg.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt_upfg.Rows.Count; i++)
                                    {
                                        try
                                        {
                                            db.set_all_pk("charge", "chg_num", dt_upfg.Rows[i]["chg_num"].ToString(), "chg_code='" + dt_upfg.Rows[i]["chg_code"].ToString() + "'", dt_upfg.Rows[i]["chg_num"].ToString().Length);
                                        }
                                        catch { }
                                    }
                                }
                                //db.UpdateOnTable("resfil", "remarks = 'PAID'", "res_code = '" + cur_res_code + "'");
                                MessageBox.Show("Successfully updated amount.");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Error on adding payment to chgfil");
                            }
                        }
                        else
                        {
                            db.UpdateOnTable("gfolio", "amnt = '0.00'", "res_code = '" + cur_res_code + "'");
                            MessageBox.Show("Error on updating amount to chgfil");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error on updating amount");
                    }
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Double price = 0.00;
            Double amnt = 0.00;
            Double total = 0.00;
            try { price = Convert.ToDouble(label9.Text.ToString()); }
            catch { }
            try { amnt = Convert.ToDouble(textBox1.Text.ToString()); }
            catch { }
            total = amnt - price;
            if (total < 0)
            {
                MessageBox.Show("Amount must be equal to price.");
            }
            else
            {
                this.Close();
            }
        }

        public String get_ap_paymentform_code()
        {
            return "";
        }

        public String get_ap_paymentform_name()
        {
            return "";
        }

        public String get_ap_reference()
        {
            return txt_apreference.Text;
        }

        public String get_ap_amount()
        {
            return "";
        }

        public String get_ap_ccnumber()
        {
            return "";
        }

        public String get_ap_tracenumber()
        {
            return "";
        }

        public String get_ap_reasonForNoPayment()
        {
            return "";
        }

        //Deposit Get Data
        public String get_d_paymentform_code()
        {
            try
            {
                return cbo_dpaymentform.SelectedValue.ToString();
            }
            catch (Exception) { }

            return null;
        }

        public String get_d_paymentform_name()
        {
            try
            {
                return cbo_dpaymentform.Text;
            }
            catch (Exception) { }

            return null;
        }

        public String get_d_reference()
        {
            return txt_dreference.Text;
        }

        public String get_currency_code()
        {
            return "";
        }

        public String get_d_amount()
        {
            return "";
        }

        public String get_d_ccnumber()
        {
            return "";
        }

        public String get_d_tracenumber()
        {
            return "";
        }

        public String get_d_reasonForNoPayment()
        {
            return "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void load_items(String res_code)
        {
            DataTable dt_pck = new DataTable();
            DataTable dt_act = new DataTable();
            DataTable dt_cur = new DataTable();

            cur_res_code = ((res_code == "") ? cur_res_code : res_code);
            dt_pck = db.QueryBySQLCode("SELECT (CASE WHEN bool_check = true THEN bool_check ELSE false END) AS bool_check, chg_desc FROM rssys.charge ch LEFT JOIN (SELECT DISTINCT true AS bool_check, chg_code FROM rssys.res_gfil WHERE rg_code = '" + cur_res_code + "') rg ON ch.chg_code = rg.chg_code WHERE ch.chg_code LIKE 'PCK%'");
            dt_act = db.QueryBySQLCode("SELECT (CASE WHEN bool_check = true THEN bool_check ELSE false END) AS bool_check, chg_desc FROM rssys.charge ch LEFT JOIN (SELECT DISTINCT true AS bool_check, chg_code FROM rssys.res_gfil WHERE rg_code = '"+cur_res_code+"') rg ON ch.chg_code = rg.chg_code WHERE (ch.chg_code LIKE 'ACT%')");
            dt_cur = db.QueryBySQLCode("SELECT rf.full_name, tr.trns, hl.code, pck.adult, pck.kid, pck.inf, pck1.ttlpax, rf.p_typ, ttl AS price FROM rssys.gfolio rf LEFT JOIN (SELECT name, code FROM rssys.hotel) hl ON hl.code = rf.hotel_code LEFT JOIN (SELECT string_agg(chg_desc, ', ') AS package, COALESCE(adult.occ_type, '0 ADULT') AS adult, COALESCE(kid.occ_type, '0 KID') AS kid, COALESCE(inf.occ_type, '0 INFANT') AS inf, res_gfil.rg_code FROM rssys.res_gfil LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE occ_type LIKE '%ADULT') adult ON adult.rg_code = res_gfil.rg_code LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE occ_type LIKE '%KID') kid ON kid.rg_code = res_gfil.rg_code LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE occ_type LIKE '%INFANT') inf ON inf.rg_code = res_gfil.rg_code LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code GROUP BY res_gfil.rg_code, adult.occ_type, kid.occ_type, inf.occ_type) pck ON pck.rg_code = rf.res_code LEFT JOIN (SELECT string_agg(chg_desc, ', ') AS activities, (CASE WHEN occ_type LIKE '%All' THEN occ_type END) AS ttlpax, rg_code FROM rssys.res_gfil LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code WHERE UPPER(occ_type) LIKE '%ALL' GROUP BY rg_code, occ_type) pck1 ON pck1.rg_code = rf.res_code LEFT JOIN (SELECT chg_desc AS p_name, chg_code FROM rssys.charge WHERE chg_type = 'P') pp ON pp.chg_code = p_typ LEFT JOIN (SELECT chg_code AS trns, rg_code FROM rssys.res_gfil WHERE UPPER(chg_code) LIKE 'TRNS%') tr ON tr.rg_code = rf.res_code LEFT JOIN (SELECT SUM(price) AS ttl, rg_code FROM rssys.res_gfil GROUP BY rg_code) trs ON rf.res_code = trs.rg_code WHERE res_code = '" + cur_res_code + "' LIMIT 1");
            if (dt_pck.Rows.Count > 0)
            {
                checkedListBox2.Items.Clear();
                for (int i = 0; i < dt_pck.Rows.Count; i++)
                {
                    Boolean ischecked = false;
                    try 
                    {
                        ischecked = Convert.ToBoolean(dt_pck.Rows[i]["bool_check"].ToString());
                    } catch { }
                    checkedListBox2.Items.Add(dt_pck.Rows[i]["chg_desc"].ToString(), ischecked);
                }
            }
            if (dt_act.Rows.Count > 0)
            {
                checkedListBox1.Items.Clear();
                for (int i = 0; i < dt_act.Rows.Count; i++)
                {
                    Boolean ischecked = false;
                    try
                    {
                        ischecked = Convert.ToBoolean(dt_act.Rows[i]["bool_check"].ToString());
                    }
                    catch { }
                    checkedListBox1.Items.Add(dt_act.Rows[i]["chg_desc"].ToString(), ischecked);
                }
            }
            if (dt_cur.Rows.Count > 0)
            {
                try
                {
                    txt_dreference.Text = dt_cur.Rows[0]["full_name"].ToString();

                    cbo_dpaymentform.SelectedValue = dt_cur.Rows[0]["code"].ToString();
                    comboBox1.SelectedValue = dt_cur.Rows[0]["trns"].ToString();
                    comboBox2.SelectedValue = dt_cur.Rows[0]["p_typ"].ToString();

                    cur_c_c = dt_cur.Rows[0]["trns"].ToString();
                    cur_p_c = dt_cur.Rows[0]["p_typ"].ToString();

                    label9.Text = gm.toAccountingFormat(Convert.ToDouble((dt_cur.Rows[0]["price"] ?? "0.00").ToString()));

                    label20.Text = ((dt_cur.Rows[0]["adult"].ToString() == "") ? "0" : dt_cur.Rows[0]["adult"].ToString().ToUpper().Replace(" ADULT", ""));
                    label21.Text = ((dt_cur.Rows[0]["kid"].ToString() == "") ? "0" : dt_cur.Rows[0]["kid"].ToString().ToUpper().Replace(" KID", ""));
                    label23.Text = ((dt_cur.Rows[0]["inf"].ToString() == "") ? "0" : dt_cur.Rows[0]["inf"].ToString().ToUpper().Replace(" INFANT", ""));
                    label5.Text = ((dt_cur.Rows[0]["ttlpax"].ToString() == "") ? "0" : dt_cur.Rows[0]["ttlpax"].ToString().ToUpper().Replace(" ALL", ""));
                }
                catch { }
            }

            //if (cur_i == 0)
            //{
            //    cur_i = 1;
            //    up_gt();
            //}
            
            //button1.Visible = false;
        }

        private void up_gt()
        {
            try
            {
                if (db.DeleteOnTable("res_gfil", "chg_code = '" + cur_ent_c + "'"))
                {
                    if (checkBox1.Checked == true)
                    {
                        Double dd_c = 0.00;
                        dd_c = ((checkBox1.Checked == true) ? Convert.ToDouble(label27.Text.ToString()) * ((label5.Text.ToString() == "0") ? (Convert.ToDouble(label20.Text.ToString()) + Convert.ToDouble(label21.Text.ToString()) + Convert.ToDouble(label23.Text.ToString())) : Convert.ToDouble(label5.Text.ToString())) : 0.00);
                        if (db.InsertOnTable("res_gfil", "rg_code, acct_no, occ_type, chg_code, price", "'" + cur_res_code + "', '" + db.QueryBySQLCodeRetStr("SELECT acct_no FROM rssys.resfil WHERE res_code = '" + cur_res_code + "'") + "', '" + ((label5.Text.ToString() == "0") ? (Convert.ToDouble(label20.Text.ToString()) + Convert.ToDouble(label21.Text.ToString()) + Convert.ToDouble(label23.Text.ToString())).ToString() : label5.Text.ToString()) + "', '" + cur_ent_c + "', '" + dd_c + "'"))
                        {
                            Double tl_g = (Convert.ToDouble(db.QueryBySQLCodeRetStr("SELECT SUM(ttl) FROM (SELECT REPLACE(occ_type, occ_type, (CASE WHEN occ_type LIKE '%ADULT' THEN 'ADULT' WHEN occ_type LIKE '%KID' THEN 'KID' WHEN occ_type LIKE '%INFANT' THEN 'INFANT' WHEN UPPER(occ_type) LIKE '%ALL' THEN 'All' ELSE 'ADTL' END)) AS pck_typ, SPLIT_PART(occ_type, ' ', 1) AS count, rf.price, chg.price AS chg_price, ((SPLIT_PART(occ_type, ' ', 1))::numeric(20,2) * chg.price::numeric(20,2))::numeric(20,2) AS ttl FROM rssys.res_gfil rf LEFT JOIN rssys.charge chg ON chg.chg_code = rf.chg_code WHERE rf.chg_code NOT LIKE 'TRNS%' AND rg_code = '" + cur_res_code + "') _all") ?? "0.00"));

                            if (db.UpdateOnTable("gfolio", "price = '" + tl_g + "'", "res_code = '" + cur_res_code + "'"))
                            {
                                load_items(cur_res_code);
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        load_items(cur_res_code);
                    }
                }
                else
                {

                }
            }
            catch { MessageBox.Show("Error on updating price"); }
        }

        private void trashbin()
        {

            //try
            //{
            //    return cbo_appaymentform.Text;
            //}
            //catch (Exception) { }

            //return null;

            //try
            //{
            //    return cbo_appaymentform.SelectedValue.ToString();
            //}
            //catch (Exception) { }

            //return null;   //String ap_amt = txt_apamount.Text;
            //String ap_reference = txt_apreference.Text;
            //String ap_ccard = txt_apcardnumber.Text;
            //String ap_traceno = txt_aptracenumber.Text;
            //String ap_reason = rtxt_apreason.Text;
            //String ap_paymentform = "";

            //String d_amt = txt_damount.Text;
            //String d_reference = txt_dreference.Text;
            //String d_ccard = txt_dcardnumber.Text;
            //String d_traceno = txt_dtracenumber.Text;
            //String d_reason = rtxt_dreason.Text;
            //String d_paymentform = "";
            //String rom = "";
            //if (frmArrivals!=null)
            //{ 
            // rom = frmArrivals.get_room();
            //}
            //else if (newfrmArrivals != null)
            //{
            //    rom = newfrmArrivals.get_room();
            //}
            //Boolean success = false;

            //if (rom == "Z01")
            //{
            //    success = true;
            //}
            //else
            //{

            //    if (String.IsNullOrEmpty(rtxt_apreason.Text) == true)
            //    {
            //        success = true;

            //        //advance payment
            //        if (cbo_appaymentform.SelectedIndex == -1)
            //        {
            //            success = false;
            //            MessageBox.Show("Please select payment form for advance payment.");
            //        }
            //        else
            //        {
            //            ap_paymentform = cbo_appaymentform.SelectedValue.ToString();
            //            success = true;

            //            if (String.IsNullOrEmpty(ap_reference))
            //            {
            //                success = false;
            //                MessageBox.Show("Please enter the reference for advance payment.");
            //            }

            //            if (db.iscardpayment(ap_paymentform))
            //            {
            //                if (String.IsNullOrEmpty(ap_ccard))
            //                {
            //                    success = false;
            //                    MessageBox.Show("Card number is required for this payment form of advance payment.");
            //                }
            //                else if (String.IsNullOrEmpty(ap_traceno))
            //                {
            //                    success = false;
            //                    MessageBox.Show("Trace number is required for this payment form of advance payment.");
            //                }
            //            }
            //        }
            //    }
            //    else if (String.IsNullOrEmpty(ap_reason) == false && cbo_appaymentform.SelectedIndex > -1)
            //    {
            //        success = false;
            //        MessageBox.Show("Entries not allowed. Do not type the reason if you select the payment form.");
            //    }
            //    else if (String.IsNullOrEmpty(ap_reason) == false)
            //    {
            //        success = true;
            //    }

            //    //deposit payment
            //    if (String.IsNullOrEmpty(d_reason))
            //    {
            //        success = true;

            //        if (cbo_dpaymentform.SelectedIndex == -1)
            //        {
            //            success = false;
            //            MessageBox.Show("Please select payment form for deposit payment.");
            //        }
            //        else
            //        {
            //            success = true;
            //            d_paymentform = cbo_dpaymentform.SelectedValue.ToString();

            //            if (String.IsNullOrEmpty(d_reference))
            //            {
            //                success = false;
            //                MessageBox.Show("Please enter the reference for advance payment.");
            //            }

            //            if (db.iscardpayment(d_paymentform))
            //            {
            //                if (String.IsNullOrEmpty(d_ccard))
            //                {
            //                    success = false;
            //                    MessageBox.Show("Card number is required for this payment form of advance payment.");
            //                }
            //                else if (String.IsNullOrEmpty(d_traceno))
            //                {
            //                    success = false;
            //                    MessageBox.Show("Trace number is required for this payment form of advance payment.");
            //                }
            //            }
            //        }
            //    }
            //    else if (String.IsNullOrEmpty(d_reason) == false && cbo_dpaymentform.SelectedIndex > -1)
            //    {
            //        success = false;
            //        MessageBox.Show("Entries not allowed. Do not type the reason if you select the payment form.");
            //    }
            //    else if(String.IsNullOrEmpty(d_reason) == false)
            //    {
            //        success = true;
            //    }
            //}
            //if (frmArrivals!=null)
            //{ 
            //if (success)
            //{
            //    this.frmArrivals.guest_checkInSave();
            //    this.Close();
            //}
            //}
            //else if (newfrmArrivals != null)
            //{
            //    if (success && gfolio==string.Empty)
            //    {
            //        this.newfrmArrivals.guest_checkInSave();
            //        this.Close();
            //    }
            //    if (gfolio != string.Empty)
            //    {

            //        String col = "ap_paymentform='" + cbo_appaymentform.Text + "',ap_doc_ref='" + txt_apreference.Text + "',ap_dep_amnt='" + txt_apamount.Text + "',ap_nodeposit_rmrk='" + rtxt_apreason.Text + "', paymentform='" + cbo_dpaymentform.Text + "',doc_ref='" + txt_dreference.Text + "',dep_amnt='" + txt_damount.Text + "',nodeposit_rmrk='" + rtxt_dreason.Text+ "'";
            //        if (db.UpdateOnTable("gfolio", col, "reg_num='" + gfolio + "'"))
            //        {
            //            MessageBox.Show("Payment/Deposit Record Updated.");
            //            this.Close();

            //        }
            //    }
            //}
            //newfrmArrivals = _frmArr;
            //gm.load_charge_paymentsonly(cbo_appaymentform);
            //gm.load_charge_depositonly(cbo_dpaymentform);
            //gm.load_currency_code(cbo_dcurrency);
            //if(gfolio != string.Empty)
            //{

            //    disp_payment(gfolio);
            //    this.gfolio = gfolio;
            //    this.action = action;

            //}
            //if (cbo_dcurrency.SelectedIndex == -1)
            //{
            //    cbo_dcurrency.SelectedValue = "PHP";
            //}

            //cbo_appaymentform.SelectedIndex = -1;
            //cbo_dpaymentform.SelectedIndex = -1;            //DataTable dt,dt1;
            //dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE reg_num='"+gfolio+"'");
            //if (dt.Rows.Count > 0)
            //{
            //    String pay = "";
            //    dt1 = db.QueryBySQLCode("SELECT chg_code from rssys.charge WHERE chg_desc='" + dt.Rows[0]["ap_paymentform"].ToString() + "'");
            //    if(dt1.Rows.Count !=0)
            //    {
            //        pay = dt1.Rows[0]["chg_code"].ToString();
            //    }
            //    cbo_appaymentform.SelectedValue = pay;
            //    txt_apreference.Text = dt.Rows[0]["ap_doc_ref"].ToString();
            //    txt_apamount.Text = dt.Rows[0]["ap_dep_amnt"].ToString();
            //    //txt_apcardnumber.Text = dt.Rows[0][""].ToString();
            //    rtxt_apreason.Text = dt.Rows[0]["ap_nodeposit_rmrk"].ToString();

            //    cbo_dpaymentform.SelectedValue = db.get_colval("charge", "chg_code", "chg_desc='" + dt.Rows[0]["paymentform"].ToString() + "'");
            //    txt_dreference.Text = dt.Rows[0]["doc_ref"].ToString();
            //    txt_damount.Text = dt.Rows[0]["dep_amnt"].ToString();
            //    rtxt_dreason.Text = dt.Rows[0]["nodeposit_rmrk"].ToString();
            //    if (cbo_dcurrency.SelectedIndex == -1)
            //    {
            //        cbo_dcurrency.SelectedValue = "PHP";
            //    }
            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedValue ?? "").ToString() == cur_c_c)
            {
                //button1.Visible = false;
            }
            else
            {
                //button1.Visible = true;
                upd_ptgo();
            }
        }

        private void cbo_dpaymentform_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox2.SelectedValue ?? "").ToString() == cur_p_c)
            {
                //button1.Visible = false;
            }
            else
            {
                //button1.Visible = true;
                upd_ptgo();
            }
        }

        private void upd_ptgo()
        {
            try
            {
                if (comboBox2.SelectedIndex > -1)
                {
                    if (db.UpdateOnTable("gfolio", "p_typ = '" + (comboBox2.SelectedValue ?? "").ToString() + "'", "res_code = '" + cur_res_code + "'"))
                    {
                        //MessageBox.Show("Successfully updated payment type.");
                    }
                    else
                    {
                        MessageBox.Show("Error on updating payment type");
                    }
                }

                if (comboBox1.SelectedIndex > -1)
                {
                    db.DeleteOnTable("res_gfil", "rg_code = '" + cur_res_code + "' AND  chg_code LIKE 'TRNS%'");
                    if (db.InsertOnTable("res_gfil", "rg_code, acct_no, occ_type, price, chg_code", "'" + cur_res_code + "', '" + db.QueryBySQLCodeRetStr("SELECT acct_no FROM rssys.resfil WHERE res_code = '" + cur_res_code + "' LIMIT 1") + "', '1', '" + db.QueryBySQLCodeRetStr("SELECT price FROM rssys.charge WHERE chg_code = '" + (comboBox1.SelectedValue ?? "").ToString() + "'") + "', '" + (comboBox1.SelectedValue ?? "").ToString() + "'"))
                    {
                        if (db.UpdateOnTable("gfolio", "price = '" + (Convert.ToDouble(db.QueryBySQLCodeRetStr("SELECT SUM(ttl) FROM (SELECT REPLACE(occ_type, occ_type, (CASE WHEN occ_type LIKE '%ADULT' THEN 'ADULT' WHEN occ_type LIKE '%KID' THEN 'KID' WHEN occ_type LIKE '%INFANT' THEN 'INFANT' WHEN UPPER(occ_type) LIKE '%ALL' THEN 'All' ELSE 'ADTL' END)) AS pck_typ, SPLIT_PART(occ_type, ' ', 1) AS count, rf.price, chg.price AS chg_price, ((SPLIT_PART(occ_type, ' ', 1))::numeric(20,2) * chg.price::numeric(20,2))::numeric(20,2) AS ttl FROM rssys.res_gfil rf LEFT JOIN rssys.charge chg ON chg.chg_code = rf.chg_code WHERE rf.chg_code NOT LIKE 'TRNS%' AND rg_code = '" + cur_res_code + "') _all") ?? "0.00") + Convert.ToDouble(db.QueryBySQLCodeRetStr("SELECT price FROM rssys.charge WHERE chg_code = '" + (comboBox1.SelectedValue ?? "").ToString() + "'") ?? "0.00")) + "'", "res_code = '" + cur_res_code + "'"))
                        {
                            DataTable dt_ggc = db.QueryBySQLCode("SELECT REPLACE(occ_type, occ_type, (CASE WHEN occ_type LIKE '%ADULT' THEN 'ADULT' WHEN occ_type LIKE '%KID' THEN 'KID' WHEN occ_type LIKE '%INFANT' THEN 'INFANT' WHEN UPPER(occ_type) LIKE '%ALL' THEN 'All' ELSE 'ADTL' END)) AS pck_typ, SPLIT_PART(occ_type, ' ', 1) AS count, rf.price, chg.price AS chg_price, ((SPLIT_PART(occ_type, ' ', 1))::numeric(20,2) * chg.price::numeric(20,2))::numeric(20,2) AS ttl FROM rssys.res_gfil rf LEFT JOIN rssys.charge chg ON chg.chg_code = rf.chg_code WHERE rf.chg_code NOT LIKE 'TRNS%' AND rg_code = '" + cur_res_code + "'");

                            //db.insert_charges()
                            //MessageBox.Show("Successfully updated price and transportation");
                            load_items(cur_res_code);
                        }
                        else
                        {
                            MessageBox.Show("Error on updating price but successfully updated transportation");
                            load_items(cur_res_code);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error on updating price and transportation");
                    }
                }


                load_items(cur_res_code);
            }
            catch {  }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            upd_ptgo();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            up_gt();
        }

        private void label27_TextChanged(object sender, EventArgs e)
        {
            Double _curAmount = 0.00;

            try
            {
                _curAmount = Convert.ToDouble(label27.Text.ToString());
            }
            catch { }
            label27.Text = gm.toAccountingFormat(_curAmount); up_gt();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Double _curAmount = 0.00;

            try
            {
                _curAmount = Convert.ToDouble(textBox1.Text.ToString());
            }
            catch { }
            textBox1.Text = gm.toAccountingFormat(_curAmount);
        }
    }
}
