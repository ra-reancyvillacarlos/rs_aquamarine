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

        public enterDeposit(Arrivals _frmArr)
        {
            InitializeComponent();

            db = new thisDatabase();
            gm = new GlobalMethod();
            frmArrivals = _frmArr;

            gm.load_charge_paymentsonly(cbo_appaymentform);
            gm.load_charge_depositonly(cbo_dpaymentform);
            gm.load_currency_code(cbo_dcurrency);
        }
        public enterDeposit(newArrivalWalkin _frmArr, String gfolio , String action)
        {
            InitializeComponent();

            db = new thisDatabase();
            gm = new GlobalMethod();
            newfrmArrivals = _frmArr;
            gm.load_charge_paymentsonly(cbo_appaymentform);
            gm.load_charge_depositonly(cbo_dpaymentform);
            gm.load_currency_code(cbo_dcurrency);
            if(gfolio != string.Empty)
            {

                disp_payment(gfolio);
                this.gfolio = gfolio;
                this.action = action;

            }
           
        }
        private void enterDeposit_Load(object sender, EventArgs e)
        {
            if (cbo_dcurrency.SelectedIndex == -1)
            {
                cbo_dcurrency.SelectedValue = "PHP";
            }

            //cbo_appaymentform.SelectedIndex = -1;
            //cbo_dpaymentform.SelectedIndex = -1;
        }

        void disp_payment(String gfolio)
        {
            DataTable dt,dt1;
            dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE reg_num='"+gfolio+"'");
            if (dt.Rows.Count > 0)
            {
                String pay = "";
                dt1 = db.QueryBySQLCode("SELECT chg_code from rssys.charge WHERE chg_desc='" + dt.Rows[0]["ap_paymentform"].ToString() + "'");
                if(dt1.Rows.Count !=0)
                {
                    pay = dt1.Rows[0]["chg_code"].ToString();
                }
                cbo_appaymentform.SelectedValue = pay;
                txt_apreference.Text = dt.Rows[0]["ap_doc_ref"].ToString();
                txt_apamount.Text = dt.Rows[0]["ap_dep_amnt"].ToString();
                //txt_apcardnumber.Text = dt.Rows[0][""].ToString();
                rtxt_apreason.Text = dt.Rows[0]["ap_nodeposit_rmrk"].ToString();

                cbo_dpaymentform.SelectedValue = db.get_colval("charge", "chg_code", "chg_desc='" + dt.Rows[0]["paymentform"].ToString() + "'");
                txt_dreference.Text = dt.Rows[0]["doc_ref"].ToString();
                txt_damount.Text = dt.Rows[0]["dep_amnt"].ToString();
                rtxt_dreason.Text = dt.Rows[0]["nodeposit_rmrk"].ToString();
                if (cbo_dcurrency.SelectedIndex == -1)
                {
                    cbo_dcurrency.SelectedValue = "PHP";
                }
            }
        
        }
        private void btn_checkin_Click(object sender, EventArgs e)
        {
            String ap_amt = txt_apamount.Text;
            String ap_reference = txt_apreference.Text;
            String ap_ccard = txt_apcardnumber.Text;
            String ap_traceno = txt_aptracenumber.Text;
            String ap_reason = rtxt_apreason.Text;
            String ap_paymentform = "";

            String d_amt = txt_damount.Text;
            String d_reference = txt_dreference.Text;
            String d_ccard = txt_dcardnumber.Text;
            String d_traceno = txt_dtracenumber.Text;
            String d_reason = rtxt_dreason.Text;
            String d_paymentform = "";
            String rom = "";
            if (frmArrivals!=null)
            { 
             rom = frmArrivals.get_room();
            }
            else if (newfrmArrivals != null)
            {
                rom = newfrmArrivals.get_room();
            }
            Boolean success = false;

            if (rom == "Z01")
            {
                success = true;
            }
            else
            {
                
                if (String.IsNullOrEmpty(rtxt_apreason.Text) == true)
                {
                    success = true;

                    //advance payment
                    if (cbo_appaymentform.SelectedIndex == -1)
                    {
                        success = false;
                        MessageBox.Show("Please select payment form for advance payment.");
                    }
                    else
                    {
                        ap_paymentform = cbo_appaymentform.SelectedValue.ToString();
                        success = true;

                        if (String.IsNullOrEmpty(ap_reference))
                        {
                            success = false;
                            MessageBox.Show("Please enter the reference for advance payment.");
                        }

                        if (db.iscardpayment(ap_paymentform))
                        {
                            if (String.IsNullOrEmpty(ap_ccard))
                            {
                                success = false;
                                MessageBox.Show("Card number is required for this payment form of advance payment.");
                            }
                            else if (String.IsNullOrEmpty(ap_traceno))
                            {
                                success = false;
                                MessageBox.Show("Trace number is required for this payment form of advance payment.");
                            }
                        }
                    }
                }
                else if (String.IsNullOrEmpty(ap_reason) == false && cbo_appaymentform.SelectedIndex > -1)
                {
                    success = false;
                    MessageBox.Show("Entries not allowed. Do not type the reason if you select the payment form.");
                }
                else if (String.IsNullOrEmpty(ap_reason) == false)
                {
                    success = true;
                }

                //deposit payment
                if (String.IsNullOrEmpty(d_reason))
                {
                    success = true;

                    if (cbo_dpaymentform.SelectedIndex == -1)
                    {
                        success = false;
                        MessageBox.Show("Please select payment form for deposit payment.");
                    }
                    else
                    {
                        success = true;
                        d_paymentform = cbo_dpaymentform.SelectedValue.ToString();

                        if (String.IsNullOrEmpty(d_reference))
                        {
                            success = false;
                            MessageBox.Show("Please enter the reference for advance payment.");
                        }

                        if (db.iscardpayment(d_paymentform))
                        {
                            if (String.IsNullOrEmpty(d_ccard))
                            {
                                success = false;
                                MessageBox.Show("Card number is required for this payment form of advance payment.");
                            }
                            else if (String.IsNullOrEmpty(d_traceno))
                            {
                                success = false;
                                MessageBox.Show("Trace number is required for this payment form of advance payment.");
                            }
                        }
                    }
                }
                else if (String.IsNullOrEmpty(d_reason) == false && cbo_dpaymentform.SelectedIndex > -1)
                {
                    success = false;
                    MessageBox.Show("Entries not allowed. Do not type the reason if you select the payment form.");
                }
                else if(String.IsNullOrEmpty(d_reason) == false)
                {
                    success = true;
                }
            }
            if (frmArrivals!=null)
            { 
            if (success)
            {
                this.frmArrivals.guest_checkInSave();
                this.Close();
            }
            }
            else if (newfrmArrivals != null)
            {
                if (success && gfolio==string.Empty)
                {
                    this.newfrmArrivals.guest_checkInSave();
                    this.Close();
                }
                if (gfolio != string.Empty)
                {

                    String col = "ap_paymentform='" + cbo_appaymentform.Text + "',ap_doc_ref='" + txt_apreference.Text + "',ap_dep_amnt='" + txt_apamount.Text + "',ap_nodeposit_rmrk='" + rtxt_apreason.Text + "', paymentform='" + cbo_dpaymentform.Text + "',doc_ref='" + txt_dreference.Text + "',dep_amnt='" + txt_damount.Text + "',nodeposit_rmrk='" + rtxt_dreason.Text+ "'";
                    if (db.UpdateOnTable("gfolio", col, "reg_num='" + gfolio + "'"))
                    {
                        MessageBox.Show("Payment/Deposit Record Updated.");
                        this.Close();
                        
                    }
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public String get_ap_paymentform_code()
        {
            try
            {
                return cbo_appaymentform.SelectedValue.ToString();
            }
            catch (Exception) { }

            return null;
        }

        public String get_ap_paymentform_name()
        {
            try
            {
                return cbo_appaymentform.Text;
            }
            catch (Exception) { }

            return null;
        }

        public String get_ap_reference()
        {
            return txt_apreference.Text;
        }

        public String get_ap_amount()
        {
            return txt_apamount.Text;
        }

        public String get_ap_ccnumber()
        {
            return txt_apcardnumber.Text;
        }

        public String get_ap_tracenumber()
        {
            return txt_aptracenumber.Text;
        }

        public String get_ap_reasonForNoPayment()
        {
            return rtxt_apreason.Text;
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
            if (cbo_dcurrency.SelectedIndex > -1)
                return cbo_dcurrency.SelectedValue.ToString();

            return null;
        }

        public String get_d_amount()
        {
            return txt_damount.Text;
        }

        public String get_d_ccnumber()
        {
            return txt_dcardnumber.Text;
        }

        public String get_d_tracenumber()
        {
            return txt_dtracenumber.Text;
        }

        public String get_d_reasonForNoPayment()
        {
            return rtxt_dreason.Text;
        }
    }
}
