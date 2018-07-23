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
    public partial class z_add_collection : Form
    {
        a_CollectionEntry _frm_collection;
        Boolean isnew = true;
        int index = 0;
        int newline = 0;
        public z_add_collection(a_CollectionEntry frm, Boolean addnew)
        {
            InitializeComponent();
            GlobalClass gc = new GlobalClass();

            gc.load_paymenttype(cbo_payment);
            gc.load_soa(cbo_soacode);
            _frm_collection = frm;
            isnew = addnew;
            
            if (isnew)
            {
                if(_frm_collection.dgv_collection.Rows.Count > 1)
                { 
                index = _frm_collection.dgv_collection.Rows.Count - 1;
                newline = int.Parse(_frm_collection.dgv_collection["dgvl2_ln", index -1 ].Value.ToString());
                txt_line.Text = (newline+1).ToString();
                
                }
            }
            if (txt_chknumber.Text == "")
            {
                dtp_chk_dt.Enabled = false;

            }
            else
            {
                dtp_chk_dt.Enabled = true;
            }
            txt_amount.Text = "";//_frm_collection.get_fol_amt();
        }

        private void z_add_collection_Load(object sender, EventArgs e)
        {

        }

        private void btn_itemadd_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            Double amt = 0.00;
            Boolean isOK = true, ischeck = false;
            if (cbo_payment.Text == "CHECK")
            {
                if (_frm_collection.check == true)
                {
                    MessageBox.Show("You're only allowed to choose one 'CHECK' payment type.");
                    cbo_payment.DroppedDown = true;
                    dtp_chk_dt.Enabled = false;
                    txt_chknumber.Enabled = false;
                    isOK = false;
                }   
            }
            if (cbo_payment.SelectedIndex == -1)
            {
                isOK = false;
                MessageBox.Show("Invalid Payment Type");
            }
            
            else if (gm.toNormalDoubleFormat(txt_amount.Text) <= 0)
            {
                isOK = false;
                MessageBox.Show("Invalid Amount");
            }
            else if (cbo_soacode.SelectedIndex == -1)
            {
                isOK = false;
                MessageBox.Show("Please Select SOA.");
            }
            ischeck = db.is_check_acct(cbo_payment.SelectedValue.ToString());
            
            if(ischeck == true && String.IsNullOrEmpty(txt_chknumber.Text) == true)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to proceed without check number as reference?", "Confirm", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    isOK = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    isOK = false;
                }
            }

            if (isOK)
            {
                
                _frm_collection.add_collection(txt_line.Text, cbo_payment.Text, gm.toNormalDoubleFormat(txt_amount.Text), cbo_payment.SelectedValue.ToString(), dtp_chk_dt.Value.ToString("MM/dd/yyyy"), txt_chknumber.Text, ischeck, isnew,cbo_soacode.SelectedValue.ToString(),cbo_payment.Text);
                this.Close();
            }
        }

        private void btn_itemclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbo_payment_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            Boolean flag = false;

            if (cbo_payment.Text != "CHECK")
            {
                //flag = db.is_check_m10(cbo_payment.SelectedValue.ToString());
                dtp_chk_dt.Enabled = false;
                txt_chknumber.Enabled = false;
                txt_chknumber.Text = "";
            }
            else {

                dtp_chk_dt.Enabled = true;
                txt_chknumber.Enabled = true;
            }
        }

        public void set_data(String ln, String paymentcode, String amt, String chkdate, String chknumber,String soa_code)
        {
            GlobalMethod gm = new GlobalMethod();

            txt_line.Text = ln;
            txt_amount.Text = gm.toNormalDoubleFormat(amt).ToString("0.00");
            txt_chknumber.Text = chknumber;
            cbo_soacode.SelectedValue = soa_code;
            if (String.IsNullOrEmpty(chkdate) == false)
            {
                dtp_chk_dt.Value = gm.toDateValue(chkdate);
                dtp_chk_dt.Enabled = true;
                txt_chknumber.Enabled = true;
            }
            cbo_payment.SelectedValue = paymentcode;
        }

        private void txt_chknumber_TextChanged(object sender, EventArgs e)
        {
            
            if (txt_chknumber.Text == "")
            {
                dtp_chk_dt.Enabled = false;

            }
            else
            {
                dtp_chk_dt.Enabled = true;
            }
            
        }
    }
}
