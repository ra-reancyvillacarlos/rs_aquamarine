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
    public partial class rpt_customerledger : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalMethod gm = new GlobalMethod();
        GlobalClass gc = new GlobalClass();
        public rpt_customerledger()
        {
            InitializeComponent();
        }

        private void rpt_customerledger_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            //gc.load_account_for_cust_ledger(cbo_accttitle);
            gc.load_account_for_cust_ledger(cbo_accttitle);
            Enable_MainOption(false);
            Enable_OtherOption(false);

            cbo_accttitle.Enabled = true;
        }

        private void cbo_acct_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbo_accttitle.SelectedIndex > -1)
            {
                //gc.load_subsidiaryname(cbo_subsidiary, cbo_accttitle.SelectedValue.ToString());
                Boolean isCustomer = false;
                if (!String.IsNullOrEmpty(db.get_colval("m06", "d_code", " at_code='" + cbo_accttitle.SelectedValue.ToString() + "'")))
                {//Customer
                    isCustomer = true;
                }
                gc.load_subsidiaryname(cbo_subsidiary, (!isCustomer ? "00000" : cbo_accttitle.SelectedValue).ToString());
                /*if (cbo_subsidiary.Items.Count != 0)
                {
                    cbo_subsidiary.DroppedDown = true;
                }*/
                //Enable_MainOption(true);
            }
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            load_list();
        }

        private void btn_view_Click(object sender, EventArgs e)
        {

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            RPT_RES_entry rpt = new RPT_RES_entry("A401", cbo_accttitle.SelectedValue.ToString(), (cbo_subsidiary.SelectedValue ?? "").ToString(), (cbo_subsidiary.SelectedValue??"").ToString(), dtp_trnxdateasof.Value.ToString("yyyy-MM-dd"), "");
            rpt.Show();
            rpt.print();
        }

        private void btn_pdc_Click(object sender, EventArgs e)
        {

        }

        private void Enable_MainOption(Boolean flag)
        {
            cbo_accttitle.Enabled = flag;
            cbo_subsidiary.Enabled = flag;
            dtp_trnxdateasof.Enabled = flag;
            //ck_uposted.Enabled = flag;
            btn_generate.Enabled = flag;

            txt_invoice.Enabled = flag;
        }

        private void Enable_OtherOption(Boolean flag)
        {
            btn_view.Enabled = flag;
            btn_print.Enabled = flag;
            btn_pdc.Enabled = flag;
        }

        private void load_list()
        {
            DataTable dt = new DataTable();
            String sl_code = "", invoice = "";
            DateTime asofDate;
            Double tcr=0.00, tdr=0.00;

            if (cbo_subsidiary.SelectedIndex > -1)
            {
                sl_code = cbo_subsidiary.SelectedValue.ToString();
                asofDate = dtp_trnxdateasof.Value;
                invoice = txt_invoice.Text;

                Enable_OtherOption(false);

                dgv_list.Rows.Clear();
                //dt = db.ledger_customer(sl_code, asofDate, invoice);
                //JOIN (" + db.jrnlNotInTypeStr("'Purchase','Disbursement'") + ") m05 ON m05.j_code=t2.j_code 
                dt = db.QueryBySQLCode("SELECT to_char(t1.t_date,'MM/dd/yyyy') AS t_date, t2.invoice, t1.j_code, t1.j_num, t1.t_desc, SUM(t2.debit), SUM(t2.credit), t2.seq_desc, t1.fy, t1.mo, t1.t_desc FROM (SELECT m6.d_name AS sl_name, m6.d_code AS sl_code, m6.d_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m6.*, cm6.d_codes FROM rssys.m06 m6 JOIN (SELECT DISTINCT om6.link AS d_code,  string_agg(m6.d_code,',') AS d_codes FROM rssys.m06 m6 JOIN (SELECT d_code, CASE WHEN COALESCE(d_oldcode,'')='' THEN d_code ELSE d_oldcode END AS link FROM rssys.m06 m6 WHERE 1=1 AND d_code='" + (cbo_subsidiary.SelectedValue ?? "").ToString() + "')  om6 ON (om6.link=(CASE WHEN COALESCE(m6.d_oldcode,'')='' THEN m6.d_code ELSE m6.d_oldcode END)) GROUP BY om6.link) cm6 ON cm6.d_code=m6.d_code ORDER BY d_code) m6 ON (m6.d_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code)  WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date<='" + asofDate.ToString("yyyy-MM-dd") + "' AND t2.at_code='" + (cbo_accttitle.SelectedValue ?? "").ToString() + "' GROUP BY t_date, t2.invoice, t1.j_code, t1.j_num, t1.t_desc, t2.seq_desc, t1.fy, t1.mo, t1.t_desc ORDER BY t1.t_date ASC");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv_list.Rows[0].Clone();

                    row.Cells[0].Value = gm.toDateString(dt.Rows[i][0].ToString(), "");
                    row.Cells[1].Value = dt.Rows[i][1].ToString();
                    row.Cells[2].Value = dt.Rows[i][2].ToString();
                    row.Cells[3].Value = dt.Rows[i][3].ToString();
                    row.Cells[4].Value = dt.Rows[i][4].ToString();

                    if (String.IsNullOrEmpty(dt.Rows[i][5].ToString()) == false)
                    {
                        row.Cells[5].Value = gm.toAccountingFormat(dt.Rows[i][5].ToString());
                        tdr += Convert.ToDouble(dt.Rows[i][5].ToString());
                    }

                    if (String.IsNullOrEmpty(dt.Rows[i][6].ToString()) == false)
                    {
                        row.Cells[6].Value = gm.toAccountingFormat(dt.Rows[i][6].ToString());
                        tcr += Convert.ToDouble(dt.Rows[i][6].ToString());
                    }

                    row.Cells[7].Value = gm.toAccountingFormat(tdr - tcr);//string.Format("{0:#,##0.##}", tcr - tdr); //(tdr - tcr).ToString("0,000.00");
                    row.Cells[8].Value = dt.Rows[i][7].ToString();
                    row.Cells[9].Value = dt.Rows[i][8].ToString();
                    row.Cells[10].Value = dt.Rows[i][9].ToString();

                    dgv_list.Rows.Add(row);
                }
                if (dt.Rows.Count != 0)
                {
                    Enable_OtherOption(true);
                }
                else
                {
                    MessageBox.Show("Generated Customer's Ledger is Empty");
                }
            }
            txt_total_debit.Text = gm.toAccountingFormat(tdr);//string.Format("{0:#,##0.##}", );
            txt_total_credit.Text = gm.toAccountingFormat(tcr); //string.Format("{0:#,##0.##}", ); //tcr.ToString("0,000.00");

            txt_balance.Text = gm.toAccountingFormat(tdr - tcr); //string.Format("{0:#,##0.##}", tdr - tcr); //(tdr - tcr).ToString("0,000.00");
        }

        private void cbo_subsidiary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_subsidiary.SelectedIndex != -1)
            {
                Enable_MainOption(true);
            }
        }
    }
}
