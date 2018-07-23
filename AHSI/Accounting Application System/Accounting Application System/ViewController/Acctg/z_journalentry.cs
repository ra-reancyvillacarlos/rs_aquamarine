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
    public partial class z_journalentry : Form
    {
        private a_journalentry _frm; //require methods: add_je, updje
        private Boolean _isnew = true; //new journal item entry
        private Boolean has_invoice = false;
        private Boolean do_additem_cbo_load = false;
        private Boolean do_subledger_load = false;
        private String _j_code = "", _j_num = "";
        GlobalMethod gm = new GlobalMethod();
        String _je_bal = "0.00";
        int _ln_no = 1;
        
        //feature
        private System.Timers.Timer cbo_subdiaryname_timer;
        private Boolean cbo_subdiaryname_timer_open = false;


        public z_journalentry(a_journalentry frm, Boolean isnew, String j_code, String j_num, String bal, int ln)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            _frm = frm;
            _isnew = isnew;
            _j_code = j_code;
            _j_num = j_num;
            _je_bal = gm.toAccountingFormat(Math.Abs(gm.toNormalDoubleFormat(bal)));
            _ln_no = ln;

            gc.load_accounttitle(cbo_accttitle);
            gc.load_userid(cbo_salesperson);
            gc.load_costcenter(cbo_costcenter);
            gc.load_terms(cbo_terms);
        }

        private void prmpt_journalentry_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            cbo_terms.Enabled = false;
            cbo_salesperson.Enabled = false;

            //new journal
            if (_isnew == true)
            {
                txt_line.Text = _ln_no.ToString(); //db.get_jrnl_lastseq_num(_j_code, _j_num).ToString();
                txt_accttitle_code.Text = "";
                cbo_accttitle.DroppedDown = true;
                cbo_accttitle.Select();
            }
            else
            {
                cbo_accttitle.Enabled = false;
                cbo_subdiaryname.Enabled = true;
                //cbo_subdiaryname.Enabled = false;

                if (cbo_accttitle.SelectedValue == null)
                {
                    cbo_accttitle.Enabled = true;
                    txt_accttitle_code.Text = "";
                }
            }

            if (_j_code == "CDJ")
            {
                cbo_terms.Enabled = true;            
            }
        }

        private void _txt_Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        public void set_txt_line(String str)
        {
            txt_line.Text = str;
        }

        public void set_txt_debit(Double val)
        {
            txt_debit.Text = val.ToString("0.00");
        }

        public void set_txt_credit(Double val)
        {
            txt_credit.Text = val.ToString("0.00");
        }

        public void set_txt_invoice_no(String str)
        {
            txt_invoice_no.Text = str;
        }

        public void set_rtxt_notes(String str)
        {
            rtxt_notes.Text = str;
        }

        public void set_cbo_accttitle_value(String str)
        {
            if (String.IsNullOrEmpty(str) == false)
            {
                cbo_accttitle.SelectedValue = str;
                txt_accttitle_code.Text = str;
            }
        }

        public void set_cbo_subdiaryname_value(String str, String acct_code)
        {
            GlobalClass gc = new GlobalClass();

            if (String.IsNullOrEmpty(str) == false)
            {
                cbo_subdiaryname.Enabled = false;
                gc.load_subsidiaryname(cbo_subdiaryname, acct_code);
                cbo_subdiaryname.SelectedValue = str; // sl_code
            }
        }

        public void set_cbo_costcenter(String str)
        {
            if (String.IsNullOrEmpty(str) == false)
            {
                cbo_costcenter.Enabled = true;
                cbo_costcenter.SelectedValue = str;
            }
        }

        public void set_cbo_salesperson(String str)
        {
            if (String.IsNullOrEmpty(str) == false) 
            {
                cbo_salesperson.Enabled = true;
                cbo_salesperson.SelectedValue = str;
            }
        }

        public void set_cbo_terms(String str)
        {
            if (String.IsNullOrEmpty(str) == false)
            {
                cbo_terms.Enabled = true;
                cbo_terms.SelectedValue = str;
            }
        }

        public void addnew_invoice()
        {
            txt_invoice_no.Enabled = true;
            txt_invoice_no.ReadOnly = false;
        }

        private void cbo_accttitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalClass gc = new GlobalClass();
            thisDatabase db = new thisDatabase();

            try
            {
                if (cbo_accttitle.SelectedIndex != -1)
                {
                    cbo_subdiaryname.Enabled = false;
                    txt_accttitle_code.Text = cbo_accttitle.SelectedValue.ToString();
                    
                    if (db.has_subledger(txt_accttitle_code.Text))
                    {
                        gc.load_subsidiaryname(cbo_subdiaryname, txt_accttitle_code.Text);
                        
                        if (_isnew == true)
                        {
                            cbo_subdiaryname.Enabled = true;
                            cbo_subdiaryname.Select();
                            cbo_subdiaryname.DroppedDown = true;
                        }
                    }
                    //check for the cash payment from the inputted subledger account
                    else if (db.is_payment_acct(txt_accttitle_code.Text))
                    {
                        if (Convert.ToDouble(_je_bal) < 0.00)
                        {
                            txt_debit.Text = (Convert.ToDouble(_je_bal) * -1).ToString("0.00");
                        }
                        else
                        {
                            txt_credit.Text = _je_bal;
                        }
                    }
                    else if (db.is_expenseORincome(txt_accttitle_code.Text))
                    {
                        //cbo_costcenter.Enabled = true;
                        //cbo_subcostcenter.Select();
                        cbo_costcenter.DroppedDown = true;
                    }
                    else
                    {
                        //cbo_costcenter.SelectedIndex = 1;
                        //cbo_costcenter.Enabled = false;
                    }
                }
            }
            catch (Exception) { }
        }

        private void cbo_costcenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalClass gc = new GlobalClass();

            cbo_subcostcenter.Enabled = false;

            if (String.IsNullOrEmpty((cbo_costcenter.SelectedValue??"").ToString()) == false)
            {
                cbo_subcostcenter.DataSource = null;

                gc.load_subcostcenter(cbo_subcostcenter, cbo_costcenter.SelectedValue.ToString());

                if (cbo_subcostcenter.Items.Count > 0)
                {
                    cbo_subcostcenter.Enabled = true;
                }
            }
            else
            {
                cbo_subcostcenter.DataSource = null;
            }
        }


        private void cbo_subdiaryname_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (!cbo_subdiaryname_timer_open)
            {
                if (cbo_subdiaryname_timer != null)
                {
                    cbo_subdiaryname_timer.Stop();
                    cbo_subdiaryname_timer_open = false;
                }
                cbo_subdiaryname_timer = new System.Timers.Timer(1500);
                cbo_subdiaryname_timer.SynchronizingObject = this;
                cbo_subdiaryname_timer.AutoReset = false;
                cbo_subdiaryname_timer.Elapsed += new System.Timers.ElapsedEventHandler(cbo_subdiaryname_SelectedIndexChanged_action);
                cbo_subdiaryname_timer.Start();
            }*/
        }
        private void cbo_subdiaryname_SelectedIndexChanged_action(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!cbo_subdiaryname_timer_open)
            {
                cbo_subdiaryname_timer.Stop();
                cbo_subdiaryname_timer_open = true;

                if (cbo_subdiaryname.SelectedIndex > -1)
                {
                    if (cbo_subdiaryname.SelectedValue.ToString() != "System.Data.DataRowView" && _isnew == true)
                    {
                        z_unpaid_invoices pui = new z_unpaid_invoices(_frm, this, _j_code, cbo_accttitle.SelectedValue.ToString(), cbo_accttitle.Text, cbo_subdiaryname.SelectedValue.ToString(), cbo_subdiaryname.Text, (cbo_costcenter.SelectedValue ?? "").ToString(), cbo_costcenter.Text);
                        pui.ShowDialog();
                        cbo_subdiaryname_timer_open = false;
                    }

                }
            }
        }



        private void txt_credit_Click(object sender, EventArgs e)
        {
            /*if (has_invoice)
            {
                txt_credit.Text = lbl_bal.Text;

                has_invoice = false;
            }*/
        }

        private void txt_credit_MouseClick(object sender, MouseEventArgs e)
        {
            /*if (has_invoice)
            {
                txt_credit.Text = lbl_bal.Text;

                has_invoice = false;
            }*/
        }

        private void btn_chg_cbo_accttitle_Click(object sender, EventArgs e)
        {
            if (cbo_accttitle.DropDownStyle == ComboBoxStyle.DropDown)
            {
                cbo_accttitle.DropDownStyle = ComboBoxStyle.Simple;
            }
            else if (cbo_accttitle.DropDownStyle == ComboBoxStyle.Simple)
            {
                cbo_accttitle.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void btn_chg_cbo_subdiaryname_Click(object sender, EventArgs e)
        {
            if (cbo_subdiaryname.DropDownStyle == ComboBoxStyle.DropDown)
            {
                cbo_subdiaryname.DropDownStyle = ComboBoxStyle.Simple;
            }
            else if (cbo_subdiaryname.DropDownStyle == ComboBoxStyle.Simple)
            {
                cbo_subdiaryname.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void btn_itemsave_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalClass gc = new GlobalClass();
            String slname = "", sl_code = "", clerk = "", terms = "", ccname = "", cc_code = "";

            if (cbo_accttitle.SelectedIndex == -1 || String.IsNullOrEmpty(txt_accttitle_code.Text))
            {
                cbo_accttitle.Enabled = true;
                MessageBox.Show("Account Title does not selected.");
            }
            else if (db.is_expenseORincome(cbo_accttitle.SelectedValue.ToString()) && cbo_costcenter.SelectedIndex == -1)
            {
                cbo_costcenter.Enabled = true;
                MessageBox.Show("Cost center is required.");
            }
            else if (gc.toNormalDoubleFormat(txt_debit.Text) == 0.00 && gc.toNormalDoubleFormat(txt_credit.Text) == 0.00)
            {
                MessageBox.Show("Amount is required for Debit/Credit.");
            }
            else
            {
                if (cbo_subdiaryname.SelectedIndex != -1)
                {
                    slname =  cbo_subdiaryname.Text.ToString();
                    sl_code = cbo_subdiaryname.SelectedValue.ToString(); // sl_code
                }
                if (cbo_salesperson.SelectedIndex != -1) //sales clerk
                {
                    clerk = cbo_salesperson.SelectedValue.ToString();
                }
                if (cbo_terms.SelectedIndex != -1)
                {
                    terms = cbo_terms.SelectedValue.ToString();
                }
                if (cbo_costcenter.SelectedIndex != -1)
                {
                    ccname = cbo_costcenter.Text;
                   cc_code = cbo_costcenter.SelectedValue.ToString();
                }

                if (_isnew)
                {
                    _frm.add_je(txt_line.Text, cbo_accttitle.Text, gc.toAccountingFormat(gc.toNormalDoubleFormat(txt_debit.Text)), gc.toAccountingFormat(gc.toNormalDoubleFormat(txt_credit.Text)), txt_invoice_no.Text,
                                 slname, ccname, terms, clerk, rtxt_notes.Text, cbo_accttitle.SelectedValue.ToString(), sl_code, cc_code);
                   _frm.set_nextln(_ln_no);
                }
                else
                {
                    _frm.upd_je(txt_line.Text, cbo_accttitle.Text, gc.toAccountingFormat(gc.toNormalDoubleFormat(txt_debit.Text)), gc.toAccountingFormat(gc.toNormalDoubleFormat(txt_credit.Text)), txt_invoice_no.Text,
                                 slname, ccname, terms, clerk, rtxt_notes.Text, cbo_accttitle.SelectedValue.ToString(), sl_code, cc_code);
                }

                this.Close();
            }
        }

        private void btn_itemclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_credit_MouseClick_1(object sender, MouseEventArgs e)
        {

        }

        private void txt_credit_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_getinvoice_Click(object sender, EventArgs e)
        {
            z_unpaid_invoices pui;

            try
            {
                if (cbo_subdiaryname.SelectedValue.ToString() != "System.Data.DataRowView")//&& do_subledger_load == true)
                {
                    pui = new z_unpaid_invoices(_frm, this, _j_code, cbo_accttitle.SelectedValue.ToString(), cbo_accttitle.Text, cbo_subdiaryname.SelectedValue.ToString(), cbo_subdiaryname.Text, (cbo_costcenter.SelectedValue ?? "").ToString(), cbo_costcenter.Text);
                    pui.ShowDialog();
                }
            }
            catch  {  }
        }

        private void txt_debit_Leave(object sender, EventArgs e)
        {

        }

        private void txt_debit_TextChanged(object sender, EventArgs e)
        {
            if (txt_debit.Text.Length != 0)
            {
                if (gm.toNormalDoubleFormat(txt_credit.Text) == 0 && gm.toNormalDoubleFormat(txt_debit.Text) == 0)
                {
                    txt_debit.Text = _je_bal;
                }
            }
        }

        private void txt_credit_TextChanged(object sender, EventArgs e)
        {
            if (txt_credit.Text.Length != 0)
            {
                if (gm.toNormalDoubleFormat(txt_credit.Text) == 0 && gm.toNormalDoubleFormat(txt_debit.Text) == 0)
                {
                    txt_credit.Text = _je_bal;
                }
            }
        }

        private void txt_debit_Click(object sender, EventArgs e)
        {

        }

        private void txt_credit_Enter(object sender, EventArgs e)
        {
            if (gm.toNormalDoubleFormat(txt_credit.Text) == 0 && gm.toNormalDoubleFormat(txt_debit.Text) == 0)
            {
                txt_credit.Text = _je_bal;
            }
        }

        private void txt_debit_Enter(object sender, EventArgs e)
        {
            if (gm.toNormalDoubleFormat(txt_credit.Text) == 0 && gm.toNormalDoubleFormat(txt_debit.Text) == 0)
            {
                txt_debit.Text = _je_bal;
            }
        }
    }
}
