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
    public partial class z_unpaid_invoices : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();

        private a_journalentry _frm_jrnl;
        private z_journalentry _frm_je;

        private a_disbursement _frm_disb;
        private z_add_disburse_item _frm_de;

        private String _j_code = "";
        private String _at_code = "";
        private String _at_desc = "";
        private String _sl_code = "";
        private String _sl_name = "";
        private String _cc_code = "";
        private String _cc_name = "";

        public z_unpaid_invoices(a_journalentry frm_jrnl, z_journalentry frm_je, String j_code, String at_code, String at_desc, String sl_code, String sl_name, String cc_code, String cc_name)
        {
            InitializeComponent();

            _frm_jrnl = frm_jrnl;
            _frm_je = frm_je;
            _j_code = j_code;
            _at_code = at_code;
            _at_desc = at_desc;
            _sl_code = sl_code;
            _sl_name = sl_name;
            _cc_code = cc_code;
            _cc_name = cc_name;
        }
        public z_unpaid_invoices(a_disbursement frm_disb, z_add_disburse_item frm_de, String j_code, String at_code, String at_desc, String sl_code, String sl_name,String cc_code, String cc_name)
        {
            InitializeComponent();

            _frm_disb = frm_disb;
            _frm_de = frm_de;
            _j_code = j_code;
            _at_code = at_code;
            _at_desc = at_desc;
            _sl_code = sl_code;
            _sl_name = sl_name;
            _cc_code = cc_code;
            _cc_name = cc_name;
        }

        private void prompt_unpaid_invoices_Load(object sender, EventArgs e)
        {
            form_load();
        }

        private void form_load()
        {
            
            String j_code, inv = "", tdate1 = "", desc1 = "", tdate2 = "", desc2 = "",j_num = "";
            Double cr = 0.00, dr = 0.00;
            //MessageBox.Show(_at_code + " " + _sl_code); 
            DataTable dt = db.get_unpaid_invoices(_j_code, _at_code, _sl_code);

            lbl_acct_title.Text = _at_code + " - " + _at_desc;
            lbl_subdry.Text = _sl_code + " - " + _sl_name;

            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (String.IsNullOrEmpty(dt.Rows[i]["invoice"].ToString()))
                            continue;

                        DataGridViewRow row = (DataGridViewRow)dgv_invoicelist.Rows[0].Clone();

                        j_code = dt.Rows[i]["j_code"].ToString();
                        j_num = dt.Rows[i]["j_num"].ToString();
                        // added by: Reancy 05/15/2018
                        desc1 = dt.Rows[i]["t_desc"].ToString();
                        
                        if (String.IsNullOrEmpty(inv) == false && String.Compare(inv, dt.Rows[i]["invoice"].ToString()) != 0)
                        {
                            if (String.IsNullOrEmpty(tdate1) && String.IsNullOrEmpty(desc1))
                            {
                                tdate1 = tdate2;
                                tdate1 = gm.toDateString(tdate1, "MM/dd/yyyy");
                                desc1 = desc2;
                            }

                            //insert to datagridview
                            dgv_insert(j_code, j_num, inv, tdate1, desc1, dr, cr);
                            cr = 0.00; dr = 0.00;
                            tdate1 = ""; desc1 = ""; tdate2 = ""; desc2 = "";
                        }
                        
                        inv = dt.Rows[i]["invoice"].ToString();


                        if (String.IsNullOrEmpty(dt.Rows[i]["cr"].ToString()) == false)
                        {
                            cr += Convert.ToDouble(dt.Rows[i]["cr"].ToString()); //balance
                        }
                        if (String.IsNullOrEmpty(dt.Rows[i]["dr"].ToString()) == false)
                        {
                            dr += Convert.ToDouble(dt.Rows[i]["dr"].ToString()); //paid
                        }

                        if (dt.Rows[i]["j_code"].ToString() == "PJ" || dt.Rows[i]["j_code"].ToString() == "JV" || dt.Rows[i]["j_code"].ToString() == "SJ")
                        {
                            tdate1 = dt.Rows[i]["t_date"].ToString();
                            tdate1 = gm.toDateString(tdate1, "MM/dd/yyyy");
                            desc1 = dt.Rows[i]["t_desc"].ToString();
                        }
                        if (dt.Rows[i]["j_code"].ToString() == "CDJ" || dt.Rows[i]["j_code"].ToString() == "CV" || dt.Rows[i]["j_code"].ToString() == "CV1" || dt.Rows[i]["j_code"].ToString() == "CRJ")
                        {
                            tdate2 = dt.Rows[i]["t_date"].ToString();
                            tdate2 = gm.toDateString(tdate2, "MM/dd/yyyy");
                            desc2 = dt.Rows[i]["t_desc"].ToString();
                        }
                        if (i + 1 == dt.Rows.Count)
                        {
                            if (String.IsNullOrEmpty(tdate1) && String.IsNullOrEmpty(desc1))
                            {
                                tdate1 = tdate2;
                                desc1 = desc2;
                            }

                            //insert to datagridview
                            dgv_insert(j_code, j_num, inv, tdate1, desc1, dr, cr);
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void dgv_insert(String j_code, String j_num, String inv, String tdate, String desc, Double dr, Double cr)
        {
            DataGridViewRow row = (DataGridViewRow)dgv_invoicelist.Rows[0].Clone();
            DataTable dt = new DataTable();
            if( _frm_jrnl != null){
                dt = _frm_jrnl.get_je_dgv(_at_code, _sl_code);
            }
            if(_frm_disb != null){
                dt = _frm_disb.get_je_dgv(_at_code, _sl_code);
            }
            int r;
            Double dt_dr = 0.00, dt_cr = 0.00;
            
            for (r = 0; r < dt.Rows.Count; r++)
            {
                if (dt.Rows[r]["invoice"].ToString() == inv)
                {
                    dt_dr = Convert.ToDouble(dt.Rows[r]["debit"].ToString());
                    dt_cr = Convert.ToDouble(dt.Rows[r]["credit"].ToString());

                    break;
                }
            }

            row.Cells[1].Value = j_code;
            row.Cells[2].Value = inv;
            row.Cells[3].Value = tdate;
            row.Cells[6].Value = desc;

            String jtype_name = db.get_jtypename(_j_code);

            if (jtype_name == "Disbursement")//_j_code == "CDJ" || _j_code == "CV" || _j_code == "CV1"
            {
                dr = dr - dt_dr;
                cr = cr - dt_dr;
                //row.Cells[3].Value = ((cr - dr) - dt_dr + dt_cr).ToString("0.00"); //total balance
                //row.Cells[4].Value = (dr + dt_dr - dt_cr).ToString("0.00"); //total paid

                if (dr - cr == 0)
                {
                    row.Cells[4].Value = dt_dr.ToString("0.00");
                    row.Cells[5].Value = dt_dr.ToString("0.00");
                }
                else
                {
                    row.Cells[4].Value = (cr - dr).ToString("0.00"); //total balance
                    row.Cells[5].Value = dr.ToString("0.00"); //total paid
                }
            }
            else if (jtype_name == "Purchase")//_j_code == "PJ" || _j_code == "JV"
            {
                dr = dr - dt_dr;
                cr = cr - dt_dr;

                if (dr - cr == 0)
                {
                    row.Cells[4].Value = dt_dr.ToString("0.00");
                    row.Cells[5].Value = dt_dr.ToString("0.00");
                }
                else
                {
                    row.Cells[4].Value = (dr - cr).ToString("0.00"); //total balance
                    row.Cells[5].Value = cr.ToString("0.00"); //total paid
                }
            }
            else if (jtype_name == "Collection")//_j_code == "CRJ"
            {
                row.Cells[4].Value = ((dr - cr) - dt_cr + dt_dr).ToString("0.00"); //total balance
                row.Cells[5].Value = (cr - dt_cr + dt_dr).ToString("0.00"); //total paid
            }
            else if (jtype_name == "Sales")//_j_code == "SJ"
            {
                row.Cells[4].Value = ((cr - dr) - dt_dr + dt_cr).ToString("0.00"); //total balance
                row.Cells[5].Value = (dr - dt_dr + dt_cr).ToString("0.00"); //total paid
            }
            else if (jtype_name == "General")
            {

            }

            dgv_invoicelist.Rows.Add(row);
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String debit, credit, invoice, sl_name, cc_desc, pay_code, rep_code, seq_desc, at_code, cc_code;
            String amt = "", temp = "";
            int ln = 0;
            Boolean success = false;
            Boolean hasCheck = false;
            try
            {
                dt.Columns.Add("at_desc", typeof(String));
                dt.Columns.Add("debit", typeof(String));
                dt.Columns.Add("credit", typeof(String));
                dt.Columns.Add("invoice", typeof(String));
                dt.Columns.Add("sl_name", typeof(String));
                dt.Columns.Add("cc_desc", typeof(String));
                dt.Columns.Add("pay_code", typeof(String));
                dt.Columns.Add("rep_code", typeof(String));
                dt.Columns.Add("seq_desc", typeof(String));
                dt.Columns.Add("at_code", typeof(String));
                dt.Columns.Add("sl_code", typeof(String));
                dt.Columns.Add("cc_code", typeof(String));

                foreach (DataGridViewRow row in dgv_invoicelist.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        hasCheck = true;
                        invoice = row.Cells["invoice_no"].Value.ToString();
                        debit = "0.00";
                        credit = "0.00";
                        cc_code = _cc_code;
                        cc_desc = _cc_name;
                        seq_desc = "";
                        pay_code = null;
                        rep_code = null;

                        amt = row.Cells["inv_bal"].Value.ToString(); //invoice balance

                        if (Convert.ToDouble(amt) == 0.00)
                        {
                            MessageBox.Show("Invoice No. "+ invoice +" has a balance of ZERO amount.\nThis invoice CAN NOT BE ADDED to the journal entry.");
                            success = false;
                        }
                        else
                        {
                            if (_j_code == "CDJ" || _j_code == "CV" || _j_code == "CV1")
                            {
                                debit = amt;
                                credit = row.Cells["inv_paid"].Value.ToString();
                            }
                            else
                            {
                                debit = row.Cells["inv_paid"].Value.ToString();
                                credit = amt;
                            }

                            if (Convert.ToDouble(debit) < 0.00)
                            {
                                temp = credit;
                                credit = (Convert.ToDouble(debit) * -1).ToString("0.00");
                                debit = "0.00";
                            }

                            if (Convert.ToDouble(credit) < 0.00)
                            {
                                temp = debit;
                                debit = (Convert.ToDouble(credit) * -1).ToString("0.00");
                                credit = "0.00";
                            }
                            //if(Convert.ToDouble(amt) > 0.00)
                            dt.Rows.Add(_at_desc, debit, credit, invoice, _sl_name, cc_desc, pay_code, rep_code, seq_desc, _at_code, _sl_code, cc_code);

                            success = true;
                        }

                        ln++;
                    }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            if(!hasCheck)
            {
                MessageBox.Show("No selected invoice.");
            }
            if (success)
            {
                if (_frm_jrnl != null)
                {
                    _frm_jrnl.set_item(dt);
                    _frm_jrnl.set_uiload(true);
                    _frm_je.Close();
                }
                if (_frm_disb != null)
                {
                    _frm_disb.set_item(dt);
                    //_frm_jrnl.set_uiload(true);
                    _frm_de.Close();
                }

                this.Close();
            }
        }

        private void btn_addnew_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgv_invoicelist.CurrentRow.Index;
                String invoice = dgv_invoicelist["invoice_no", r].Value.ToString();
                String bal = dgv_invoicelist["inv_bal", r].Value.ToString();
                if (!String.IsNullOrEmpty(invoice))
                {
                    if (_frm_je != null)
                    {
                        _frm_je.addnew_invoice();
                    }
                    if (_frm_de != null)
                    {
                        _frm_de.set_txt_invoice(invoice, bal);
                    }
                }
            }
            catch { }
            this.Close();
        }

        private void dgv_invoicelist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
    }
}
