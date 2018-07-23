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
    public partial class z_payment : Form
    {
        private s_Sales _frm_sales;
        private s_Sales_Auto _frm_salesauto;
        private s_RepairOrder _frm_repairorder;
        private auto_loanapplication _frm_autoloan;
        private Boolean isnew = false;
        private Boolean isback = false;
        Boolean isCashier = false;
        int row_index = 0;
        Receipt rcpt;
        String orcodes = "";
        String lnno = "1";

        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();

        Boolean isFullpay = true;

        public z_payment(s_Sales frm_sales, Boolean isnewTransaction, String chargeto, String custid, String amtdue, String total_discamt, String orcode)
        {
            InitializeComponent();

            
            rcpt = new Receipt();
            this.orcodes = orcode;           

            _frm_sales = frm_sales;
            txt_amtdue.Text = amtdue;
            txt_csh_amttendered.Text = amtdue;
            txt_discamt.Text = total_discamt;
            isnew = isnewTransaction;
            gc.load_terms(cbo_mop);
            gc.load_customer(cbo_charge);

            cbo_charge.SelectedValue = chargeto;
            disp_change();
        }
        public z_payment(auto_loanapplication frm, Boolean isnewTransaction, String chargeto, String custid, String amtdue, String total_discamt, String orcode, Boolean isCashier, String ln, String modeofpayment, String reference, String amnt)
        {
            InitializeComponent();
            double amt = 0.00;
            lbl_balance.Hide();
            txt_csh_change.Hide();

            rcpt = new Receipt();
            this.orcodes = orcode;
            this.isCashier = isCashier;
            _frm_autoloan = frm;
            txt_amtdue.Text = amtdue;
            txt_discamt.Text = total_discamt;
            isnew = isnewTransaction;
            lnno = ln;

            gc.load_terms(cbo_mop);
            gc.load_customer(cbo_charge);
            disp_change();

            isFullpay = false;

            //display for update
            if (isnew == false)
            {
                if (!String.IsNullOrEmpty(amnt))
                {
                    try { amt = double.Parse(amnt); }
                    catch { amt = -1 * double.Parse(amnt.Substring(1, amnt.Length - 2)); }
                }
                amt = amt * -1;
                txt_csh_amttendered.Text = amt.ToString();
            }

            txt_terms_ref.Text = reference;
            cbo_mop.SelectedValue = modeofpayment;
            cbo_charge.SelectedValue = chargeto;
        }

        public z_payment(s_Sales_Auto frm, Boolean isnewTransaction, String chargeto, String custid, String amtdue, String total_discamt, String orcode, Boolean isCashier, String ln, String modeofpayment, String reference, String amnt)
        {
            InitializeComponent();
            double amt = 0.00;
            lbl_balance.Hide();
            txt_csh_change.Hide();

            rcpt = new Receipt();
            this.orcodes = orcode;
            this.isCashier = isCashier;
            _frm_salesauto = frm;
            if (!isnewTransaction)
            {
                amtdue = gm.toAccountingFormat(gm.toNormalDoubleFormat(amtdue) + (-1 * gm.toNormalDoubleFormat(amnt)));
            }
            txt_amtdue.Text = amtdue;
            txt_discamt.Text = total_discamt;
            isnew = isnewTransaction;
            lnno = ln;

            gc.load_terms(cbo_mop);
            gc.load_customer(cbo_charge);
            disp_change();

            //display for update
            if (isnew == false)
            {
                if (!String.IsNullOrEmpty(amnt))
                {
                    try { amt = double.Parse(amnt); }
                    catch { amt = -1 * double.Parse(amnt.Substring(1, amnt.Length - 2)); }
                }

                amt = amt * -1;
                txt_csh_amttendered.Text = amt.ToString();
            }
            cbo_mop.SelectedValue = modeofpayment;
            cbo_charge.SelectedValue = chargeto;
            txt_terms_ref.Text = reference;
        }
        public z_payment(s_RepairOrder frm_repairorder, Boolean isnewTransaction, String chargeto, String custid, String amtdue, String total_discamt, String orcode,Boolean isCashier, String ln, String modeofpayment, String reference , String amnt)
        {
            InitializeComponent();
            double amt = 0.00;
            lbl_balance.Hide();
            txt_csh_change.Hide();

            rcpt = new Receipt();
            this.orcodes = orcode;
            this.isCashier = isCashier;
            _frm_repairorder = frm_repairorder;
            txt_amtdue.Text = amtdue;
            txt_discamt.Text = total_discamt;
            isnew = isnewTransaction;
            lnno = ln;

            gc.load_terms(cbo_mop);
            gc.load_customer(cbo_charge);
            disp_change();

            //display for update
            if(isnew == false)
            {

                try { amt = double.Parse(amnt); }
                catch { amt = -1 * double.Parse(amnt.Substring(1, amnt.Length - 2)); }

                amt = amt * -1;
                txt_csh_amttendered.Text = amt.ToString() ;
            }
            cbo_mop.SelectedValue = modeofpayment;
            cbo_charge.SelectedValue = chargeto;
            txt_terms_ref.Text = reference;
        }
        
        public z_payment(s_Sales_Auto frm_salesauto, Boolean isnewPaymentItem, String custid, String amtdue, String total_discamt, String orcode)
        {
            InitializeComponent();


            rcpt = new Receipt();
            this.orcodes = orcode;

            _frm_salesauto = frm_salesauto;
            txt_amtdue.Text = amtdue;
            txt_discamt.Text = total_discamt;
            isnew = isnewPaymentItem;
            gc.load_terms(cbo_mop);
            gc.load_customer(cbo_charge);
            disp_change();
        }

        private void z_payment_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txt_csh_amttendered;
        }

        private void disp_change()
        {
            Double change = 0.00;
            try
            {
                change = gm.toNormalDoubleFormat(txt_amtdue.Text) - gm.toNormalDoubleFormat(txt_csh_amttendered.Text);
                txt_csh_change.Text = gm.toAccountingFormat(change);
            }
            catch (Exception ex) 
            {
               MessageBox.Show("Invalid entry.");
            }           
        }

        private void txt_amttendered_TextChanged(object sender, EventArgs e)
        {
            /*
            if (System.Text.RegularExpressions.Regex.IsMatch(txt_csh_amttendered.Text, "[0-9.]+$"))
            {
                MessageBox.Show("Please enter only numbers.");
                txt_csh_amttendered.Text = txt_csh_amttendered.Text.Remove(txt_csh_amttendered.Text.Length - 1);
            }
            else
            {
                disp_change();
            }
             */
            disp_change();
        }

        private void txt_amttendered_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (String.IsNullOrEmpty(txt_csh_amttendered.Text) || txt_csh_amttendered.Text == "0.00")
            {
                txt_csh_amttendered.Text = "";
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Double amt = 0.00;
            Double damt = 0.00;
            Double tax = 0.00;
            Double paid = 0.00;
            double due_amnt = gm.toNormalDoubleFormat(txt_amtdue.Text);
            double amt_tendered = gm.toNormalDoubleFormat(txt_csh_amttendered.Text);
            Boolean pending = false;
            int i = 0;
            

            /*if (due_amnt > amt_tendered)
            {
                MessageBox.Show("Total amount due is higher than total amount tendered.");
                pending = true;
            }
            else */
            if (0 >= amt_tendered && isFullpay)
            {
                MessageBox.Show("Total amount tendered is higher than zero(0).");
                pending = true;
            }
            else if (cbo_mop.SelectedIndex == -1)
            {
                MessageBox.Show("Select mode of payment.");
                cbo_mop.DroppedDown = true;
            }
            else
            {
                if (_frm_sales != null)
                {
                    _frm_sales.mainsave(true, cbo_mop.SelectedValue.ToString(), pending, txt_csh_amttendered.Text);
                }
                else if (_frm_repairorder != null)
                {
                    if (!isCashier)
                    { 
                        _frm_repairorder.mainsave(true, cbo_mop.SelectedValue.ToString(), pending, txt_csh_amttendered.Text);
                    }
                    else
                    {
                        if (cbo_mop.SelectedIndex == -1)
                        {
                        }
                        else
                        {
                            try
                            {
                                thisDatabase db = new thisDatabase();
                                if (isnew)
                                {
                                    i = _frm_repairorder.dgv_payment.Rows.Add();
                                }
                                else
                                {
                                    i = _frm_repairorder.dgv_payment.CurrentRow.Index;
                                }
                                String cashier = "";

                                if(_frm_repairorder.cbo_cashier.SelectedIndex != -1)
                                {
                                    cashier = _frm_repairorder.cbo_cashier.SelectedValue.ToString();
                                }

                                _frm_repairorder.dgv_payment["dgvlp_ln_num", i].Value = (gm.toNormalDoubleFormat(lnno)* -1).ToString();
                                _frm_repairorder.dgv_payment["dgvlp_pay_code", i].Value = cbo_mop.SelectedValue.ToString();
                                _frm_repairorder.dgv_payment["dgvlp_pay_desc", i].Value = cbo_mop.Text;
                                _frm_repairorder.dgv_payment["dgvlp_reference", i].Value = txt_terms_ref.Text;
                                _frm_repairorder.dgv_payment["dgvlp_ln_amnt", i].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(txt_csh_amttendered.Text) * -1);
                                _frm_repairorder.dgv_payment["dgvlp_rep_code", i].Value = cashier;
                                _frm_repairorder.dgv_payment["dgvlp_ln_type", i].Value = "P";
                                _frm_repairorder.dgv_payment["dgvlp_chargeto", i].Value = (cbo_charge.SelectedValue ?? "").ToString();

                                _frm_repairorder.dgv_payment["dgvlp_t_date", i].Value = db.get_systemdate("");
                                _frm_repairorder.dgv_payment["dgvlp_t_date", i].Value = db.get_systemtime();

                                _frm_repairorder.total_amountdue();
                            }
                            catch (Exception er)
                            { MessageBox.Show(er.Message); }
                        }
                    }
                }
                else if (_frm_autoloan != null)
                {
                    if (cbo_mop.SelectedIndex == -1)
                    {
                    }
                    else
                    {
                        try
                        {
                            if (isnew)
                            {
                                i = _frm_autoloan.dgv_payment.Rows.Add();
                            }
                            else
                            {
                                i = _frm_autoloan.dgv_payment.CurrentRow.Index;
                            }
                            String cashier = "";

                            if (_frm_autoloan.cbo_clientname.SelectedIndex != -1)
                            {
                                cashier = _frm_autoloan.cbo_clientname.SelectedValue.ToString();
                            }

                            _frm_autoloan.dgv_payment["dgvlp_ln_num", i].Value = (gm.toNormalDoubleFormat(lnno) * -1).ToString();
                            _frm_autoloan.dgv_payment["dgvlp_pay_code", i].Value = cbo_mop.SelectedValue.ToString();
                            _frm_autoloan.dgv_payment["dgvlp_pay_desc", i].Value = cbo_mop.Text;
                            _frm_autoloan.dgv_payment["dgvlp_reference", i].Value = txt_terms_ref.Text;
                            _frm_autoloan.dgv_payment["dgvlp_ln_amnt", i].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(txt_csh_amttendered.Text) * -1);
                            _frm_autoloan.dgv_payment["dgvlp_rep_code", i].Value = cashier;
                            _frm_autoloan.dgv_payment["dgvlp_ln_type", i].Value = "P";
                            _frm_autoloan.dgv_payment["dgvlp_chargeto", i].Value = (cbo_charge.SelectedValue ?? "").ToString();

                            _frm_autoloan.total_amountdue();
                        }
                        catch (Exception er)
                        { MessageBox.Show(er.Message); }
                        
                    }
                }

                else if (_frm_salesauto != null)
                {
                    //if (cbo_mop.SelectedIndex == -1)
                    if (amt_tendered != due_amnt && isnew)
                    {
                        MessageBox.Show("Total amount tendered must be full paid.");
                        return;
                    }
                    else
                    {
                        try
                        {
                            if (isnew)
                            {
                                i = _frm_salesauto.dgv_payment.Rows.Add();
                            }
                            else
                            {
                                i = _frm_salesauto.dgv_payment.CurrentRow.Index;
                            }
                            String cashier = "";

                            if (_frm_salesauto.cbo_customer.SelectedIndex != -1)
                            {
                                cashier = _frm_salesauto.cbo_customer.SelectedValue.ToString();
                            }

                            _frm_salesauto.dgv_payment["dgvlp_ln_num", i].Value = (gm.toNormalDoubleFormat(lnno) * -1).ToString();
                            _frm_salesauto.dgv_payment["dgvlp_pay_code", i].Value = cbo_mop.SelectedValue.ToString();
                            _frm_salesauto.dgv_payment["dgvlp_pay_desc", i].Value = cbo_mop.Text;
                            _frm_salesauto.dgv_payment["dgvlp_reference", i].Value = txt_terms_ref.Text;
                            _frm_salesauto.dgv_payment["dgvlp_ln_amnt", i].Value = gm.toAccountingFormat(gm.toNormalDoubleFormat(txt_csh_amttendered.Text) * -1);
                            _frm_salesauto.dgv_payment["dgvlp_rep_code", i].Value = cashier;
                            _frm_salesauto.dgv_payment["dgvlp_ln_type", i].Value = "P";
                            _frm_salesauto.dgv_payment["dgvlp_chargeto", i].Value = (cbo_charge.SelectedValue ?? "").ToString();

                            _frm_salesauto.total_amountdue();
                        }
                        catch (Exception er)
                        { MessageBox.Show(er.Message); }

                    }
                }
                else if (_frm_salesauto != null) { 
                    _frm_salesauto.mainsave(true, cbo_mop.SelectedValue.ToString(), false, txt_amtdue.Text);
                }

               
                //_frm_repairorder.get_payment_data(cbo_mop.SelectedValue.ToString(), cbo_mop.Text, txt_csh_amttendered.Text, "P", txt_terms_ref.Text);
                this.Close();
            }
            
           // save();
        }

        private void save()
        {
              
        }

        private void print_bill()
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();


            dt.Columns.Add("qty", typeof(String));
            dt.Columns.Add("desc", typeof(String));
            dt.Columns.Add("price", typeof(String));
            dt.Columns.Add("total", typeof(String));
            /*
            try
            {
                foreach (DataGridViewRow row in dgv_orddtl.Rows)
                {
                    dt.Rows.Add(row.Cells[4].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString());
                }
            }
            catch (Exception) { }

            rcpt._dt = dt;
            rcpt._checkNo = txt_gchkno.Text;
            rcpt._clerk = GlobalClass.username;
            rcpt._cust_name = txt_custname.Text;
            rcpt._dtype = btn_dinetyp.Text;
            rcpt._rmno = txt_custcode.Text;
            rcpt._mcoupon = txt_mealcoupon.Text;
            rcpt._pax = cbo_pax.Text;
            rcpt._senior = cbo_senior.Text;
            rcpt._t_date = dtp_ordr_dt.Value;
            rcpt._t_time = DateTime.Now.ToString("HH:mm");
            rcpt._tbl_no = txt_tblno.Text;

            rcpt._disc_name = cbo_disctyp.Text;
            rcpt._disc_amnt = Convert.ToDouble(txt_disc_amt.Text);

            rcpt._meal = Convert.ToDouble(txt_meal.Text) + Convert.ToDouble(txt_meal_sc.Text);
            rcpt._sc = Convert.ToDouble(txt_sc.Text) + Convert.ToDouble(txt_sc_sc.Text);
            rcpt._vat_nonsc = Convert.ToDouble(txt_vat.Text);
            rcpt._vat_sc = Convert.ToDouble(txt_vat_sc.Text);

            rcpt._subtotal = Convert.ToDouble(txt_gross.Text);
            rcpt._grand_amnt = Convert.ToDouble(txt_net_amt.Text);

            if (cbo_disctyp.SelectedIndex > -1)
            {
                rcpt._has_disc = true;
            }

            if (cbo_senior.Text != "" || cbo_senior.Text != "0")
            {
                rcpt._has_sc = true;
            }
            */


            rcpt.print();

            //db.UpdateOnTable("ordprt", "printed='Y', t_date='" + db.get_systemdate() + "', t_time='" + DateTime.Now.ToString("HH:mm") + "'", "ord_code='" + txt_gchkno.Text + "'");
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            isback = true;
            this.Close();
        }



        private void z_payment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isback == false)
            {
                this.Hide();

                //if(_frm_sales != null)
                //{
               //     _frm_sales.frm_reload();
               // }
              //  else if (_frm_salesauto != null)
              //  {
              //      _frm_salesauto.frm_reload();
              //  }
            }
        }

        private void cbo_terms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_csh_amttendered_Leave(object sender, EventArgs e)
        {
            txt_csh_amttendered.Text = gm.toAccountingFormat(txt_csh_amttendered.Text);
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

        private void txt_csh_amttendered_TextChanged(object sender, EventArgs e)
        {
            txt_csh_change.Text = gm.toAccountingFormat(gm.toNormalDoubleFormat(txt_amtdue.Text) - gm.toNormalDoubleFormat(txt_csh_amttendered.Text));
        }

        private void cbo_charge_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbo_mop.SelectedIndex = -1;
        }

        private void btn_clr_dealer_Click(object sender, EventArgs e)
        {
            cbo_charge.SelectedIndex = -1;
        }
    }
}
