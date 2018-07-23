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
    
    public partial class z_add_disburse_item : Form
    {
        GlobalClass gc = new GlobalClass();
        thisDatabase db = new thisDatabase();
        GlobalMethod gm = new GlobalMethod();
        a_disbursement _frmd;
        private String db_code;
        private int nxt_ln;
        String linenum = "";
        String code = "", _invoice;
        Boolean isnew = false;

        public z_add_disburse_item(a_disbursement frm, Boolean isnew, String code, int ln_no)
        {
            InitializeComponent();
            //gc.load_accounttitle_sl_only(cbo_accttitle);
            gc.load_accounttitle(cbo_accttitle);
            gc.load_costcenter(cbo_costcenter);
            this.isnew = isnew;
            this.code = code;
            txt_accttitle_code.Text = "";
            linenum = ln_no.ToString();
            this.nxt_ln = ln_no;
            txt_line.Text = linenum.ToString();
           // cbo_accttitle.SelectedValue = frm.cbo_acct_link.SelectedValue.ToString();
            _frmd = frm;
            init_load();
        }


        public void init_load()
        {
            if (!isnew)
            {
                txt_line.Text = _frmd.dgv_itemlist["seqno", nxt_ln].Value.ToString();
                txt_accttitle_code.Text = _frmd.dgv_itemlist["accttitle", nxt_ln].Value.ToString();
                cbo_accttitle.SelectedValue = _frmd.dgv_itemlist["dgv_atcode", nxt_ln].Value.ToString();
                txt_invoice_no.Text = _frmd.dgv_itemlist["invoice", nxt_ln].Value.ToString();
                txt_credit.Text = _frmd.dgv_itemlist["debit", nxt_ln].Value.ToString();
                cbo_costcenter.SelectedValue = (_frmd.dgv_itemlist["dgv_cccode", nxt_ln].Value ?? "").ToString();
                rtxt_notes.Text = _frmd.dgv_itemlist["dgv_notes", nxt_ln].Value.ToString();
            }
            /*else 
            {
                cbo_accttitle.SelectedValue = (_frmd.cbo_acct_link.SelectedValue ?? "").ToString();
            }*/
        }
        private void z_add_disburse_item_Load(object sender, EventArgs e)
        {

        }

        private void btn_itemsave_Click(object sender, EventArgs e)
        {
             
            String ln_no = "", code = "", at_code = "", at_desc = "", invoice = "", amount = "0.00", notes = "";
            try
            {
                ln_no = txt_line.Text;
                code = txt_accttitle_code.Text;
                
                if (cbo_accttitle.SelectedIndex != -1)
                {
                    at_code = cbo_accttitle.SelectedValue.ToString();
                    at_desc = cbo_accttitle.Text;
                }

                invoice = txt_invoice_no.Text;
                amount = txt_credit.Text;
                notes = rtxt_notes.Text;

                int i = 0;
                if (isnew)
                {
                    i = _frmd.dgv_itemlist.Rows.Add();
                }
                else
                {
                    i = _frmd.dgv_itemlist.CurrentRow.Index;
                }
                _frmd.dgv_itemlist["seqno", i].Value = ln_no.ToString();
                _frmd.dgv_itemlist["accttitle", i].Value = at_desc.ToString();
                _frmd.dgv_itemlist["dgv_ccname", i].Value = cbo_costcenter.Text;

                _frmd.dgv_itemlist["debit", i].Value = amount.ToString();
                _frmd.dgv_itemlist["invoice", i].Value = invoice.ToString();
                _frmd.dgv_itemlist["dgv_notes", i].Value = notes;
                _frmd.dgv_itemlist["dgv_atcode", i].Value = at_code.ToString();
                _frmd.dgv_itemlist["dgv_cccode", i].Value = (cbo_costcenter.SelectedValue ?? "").ToString();
                _frmd.dgv_itemlist["dgv_crdtrname", i].Value = cbo_subdiaryname.Text;
                _frmd.dgv_itemlist["dgv_crdtrcode", i].Value = (cbo_subdiaryname.SelectedValue ?? "").ToString();

                _frmd.disp_total();

                //_frmd.set_disp(code, isnew, txt_line.Text, at_code, at_desc, invoice, amount, notes);
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
            this.Close();
        }
        private void cbo_accttitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_accttitle.SelectedIndex != -1)
                {
                    cbo_subdiaryname.Enabled = false;
                    txt_accttitle_code.Text = cbo_accttitle.SelectedValue.ToString();

                    if (db.has_subledger(txt_accttitle_code.Text))
                    {
                        gc.load_subsidiaryname(cbo_subdiaryname, txt_accttitle_code.Text);
                        cbo_subdiaryname.Enabled = (((DataTable)cbo_subdiaryname.DataSource).Rows.Count != 0);
                        try {
                            if (!isnew)
                            {
                                cbo_subdiaryname.SelectedValue = (_frmd.dgv_itemlist["dgv_crdtrcode", nxt_ln].Value ?? "").ToString();
                            }
                            /*else{
                                cbo_subdiaryname.SelectedValue = (_frmd.cbo_creditors.SelectedValue ?? "").ToString();
                            }*/
                        }
                        catch { }

                        if (!cbo_subdiaryname.Enabled)
                        {
                            cbo_subdiaryname.SelectedIndex = -1;
                        }

                        cbo_costcenter.SelectedIndex = -1;
                        cbo_costcenter.Enabled = true;
                        cbo_subcostcenter.SelectedIndex = -1;
                        cbo_subcostcenter.Enabled = false;
                    }
                    //check for the cash payment from the inputted subledger account
                    else if (db.is_expenseORincome(txt_accttitle_code.Text))
                    {
                        cbo_costcenter.Enabled = true;
                        cbo_subcostcenter.Select();
                        cbo_costcenter.DroppedDown = true;
                    }
                    else
                    {
                        cbo_costcenter.SelectedIndex = -1;
                        cbo_costcenter.Enabled = false;
                        cbo_subcostcenter.SelectedIndex = -1;
                        cbo_subcostcenter.Enabled = false;
                    }
                }
            }
            catch (Exception) { }
        }

        private void cbo_costcenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalClass gc = new GlobalClass();

            cbo_subcostcenter.Enabled = false;

            if (String.IsNullOrEmpty(cbo_costcenter.SelectedValue.ToString()) == false)
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



        private void cbo_accttitle_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            try
            {
                if (cbo_accttitle.SelectedIndex != -1)
                {
                    cbo_subdiaryname.Enabled = false;
                    txt_accttitle_code.Text = cbo_accttitle.SelectedValue.ToString();

                    if (db.has_subledger(txt_accttitle_code.Text))
                    {
                        gc.load_subsidiaryname(cbo_subdiaryname, txt_accttitle_code.Text);

                    }
                    //check for the cash payment from the inputted subledger account
                    else if (db.is_payment_acct(txt_accttitle_code.Text))
                    {

                    }
                    else if (db.is_expenseORincome(txt_accttitle_code.Text))
                    {

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception) { }
        }

        public void set_cbo_accttitle_value(String str)
        {
            if (String.IsNullOrEmpty(str) == false)
            {
                cbo_accttitle.SelectedValue = str;
                txt_accttitle_code.Text = str;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_getinvoice_Click(object sender, EventArgs e)
        {
            z_unpaid_invoices pui;

            try
            {
                if (cbo_subdiaryname.SelectedValue.ToString() != "System.Data.DataRowView")//&& do_subledger_load == true)
                {
                    pui = new z_unpaid_invoices(_frmd, this, "CDJ", cbo_accttitle.SelectedValue.ToString(), cbo_accttitle.Text, cbo_subdiaryname.SelectedValue.ToString(), cbo_subdiaryname.Text, (cbo_costcenter.SelectedValue??"").ToString(),cbo_costcenter.Text);
                   pui.ShowDialog();
                }
            }
            catch { }
        }
        public void set_txt_invoice(String inv,String bal)
        {
            txt_invoice_no.Text = inv;
            txt_credit.Text = (Double.Parse(bal) * -1).ToString("0.00");
        }

        private void cbo_subcostcenter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_itemclose_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
