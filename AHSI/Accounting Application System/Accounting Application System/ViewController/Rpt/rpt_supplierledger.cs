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
    public partial class rpt_supplierledger : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        public rpt_supplierledger()
        {
            InitializeComponent();
        }

        private void rpt_supplierledger_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            //gc.load_account_for_sup_ledger(cbo_accttitle);
            gc.load_account_for_sup_ledger(cbo_accttitle);
            Enable_MainOption(false);
            Enable_OtherOption(false);

            cbo_accttitle.Enabled = true;
        }

        private void cbo_accttitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_accttitle.SelectedIndex > -1)
            {
                //gc.load_subsidiaryname(cbo_subsidiary, cbo_accttitle.SelectedValue.ToString());
                Boolean isSupplier = false;
                if (!String.IsNullOrEmpty(db.get_colval("m07", "c_code", " at_code='" + cbo_accttitle.SelectedValue.ToString() + "'")))
                {//Supplier
                    isSupplier = true;
                }
                gc.load_subsidiaryname(cbo_subsidiary, (!isSupplier ? "00000" : cbo_accttitle.SelectedValue).ToString());
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
            RPT_RES_entry rpt = new RPT_RES_entry("A404", cbo_accttitle.SelectedValue.ToString(), (cbo_subsidiary.SelectedValue ?? "").ToString(), (cbo_subsidiary.SelectedValue??"").ToString(), dtp_trnxdateasof.Value.ToString("yyyy-MM-dd"), "");
            rpt.Show();
            rpt.print();
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
        }

        private void load_list()
        {
            DataTable dt = new DataTable();
            String sl_code = "", invoice = "";
            DateTime asofDate;
            Double tcr = 0.00, tdr = 0.00;

            if (cbo_subsidiary.SelectedIndex > -1)
            {
                sl_code = cbo_subsidiary.SelectedValue.ToString();
                asofDate = dtp_trnxdateasof.Value;
                invoice = txt_invoice.Text;

                Enable_OtherOption(false);

                dgv_list.Rows.Clear();
                //dt = db.ledger_supplier(sl_code, asofDate, invoice); 
                //JOIN (" + db.jrnlNotInTypeStr("'Sales','Collection'") + ") m05 ON m05.j_code=t2.j_code
                dt = db.QueryBySQLCode("SELECT to_char(t1.t_date,'MM/dd/yyyy') AS t_date, t2.invoice, t1.j_code, t1.j_num, t1.t_desc, SUM(t2.debit), SUM(t2.credit), t2.seq_desc, t1.fy, t1.mo, t1.t_desc FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1 AND c_code='" + (cbo_subsidiary.SelectedValue ?? "").ToString() + "')  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code ORDER BY c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code)   WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date<='" + asofDate.ToString("yyyy-MM-dd") + "' AND t2.at_code='" + (cbo_accttitle.SelectedValue ?? "").ToString() + "' GROUP BY t_date, t2.invoice, t1.j_code, t1.j_num, t1.t_desc, t2.seq_desc, t1.fy, t1.mo, t1.t_desc ORDER BY t1.t_date ASC");

                // intended for including closed transactions: Reancy 05 31 2018
                //dt = db.QueryBySQLCode("(SELECT to_char(t1.t_date,'MM/dd/yyyy') AS t_date, t2.invoice, t1.j_code, t1.j_num, t1.t_desc, SUM(t2.debit), SUM(t2.credit), t2.seq_desc, t1.fy, t1.mo, t1.t_desc FROM (SELECT m7.c_name AS sl_name, m7.c_code AS sl_code, m7.c_codes AS sl_codes, t2.invoice, t2.at_code, t2.debit, t2.credit, t2.j_num, t2.j_code, t2.seq_num, t2.seq_desc FROM rssys.tr02 t2 JOIN (SELECT m7.*, cm7.c_codes FROM rssys.m07 m7 JOIN (SELECT DISTINCT om7.link AS c_code,  string_agg(m7.c_code,',') AS c_codes FROM rssys.m07 m7 JOIN (SELECT c_code, CASE WHEN COALESCE(c_oldcode,'')='' THEN c_code ELSE c_oldcode END AS link FROM rssys.m07 m7 WHERE 1=1 AND c_code='" + (cbo_subsidiary.SelectedValue ?? "").ToString() + "')  om7 ON (om7.link=(CASE WHEN COALESCE(m7.c_oldcode,'')='' THEN m7.c_code ELSE m7.c_oldcode END)) GROUP BY om7.link) cm7 ON cm7.c_code=m7.c_code ORDER BY c_code) m7 ON (m7.c_codes LIKE '%'||COALESCE(t2.sl_code,'')||'%' AND COALESCE(t2.sl_code,'')<>'')) t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code) WHERE (t1.cancel is null OR t1.cancel != 'Y') AND t1.t_date<='" + asofDate.ToString("yyyy-MM-dd") + "' AND t2.at_code='" + (cbo_accttitle.SelectedValue ?? "").ToString() + "' GROUP BY t_date, t2.invoice, t1.j_code, t1.j_num, t1.t_desc, t2.seq_desc, t1.fy, t1.mo, t1.t_desc) UNION (SELECT to_char(t4.t_date,'MM/dd/yyyy') AS t_date, t4.invoice, t4.j_code, t4.j_num, t4.t_desc, SUM(t4.debit), SUM(t4.credit), t4.seq_desc, t4.fy, t4.mo, t4.t_desc FROM rssys.tr04 t4 WHERE t4.t_date<='" + asofDate.ToString("yyyy-MM-dd") + "' AND t4.at_code='" + (cbo_accttitle.SelectedValue ?? "").ToString() + "' AND t4.sl_code = '" + (cbo_subsidiary.SelectedValue ?? "").ToString() + "' GROUP BY t4.t_date, t4.invoice, t4.j_code, t4.j_num, t4.t_desc, t4.seq_desc, t4.fy, t4.mo, t4.t_desc)");
             
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv_list.Rows[0].Clone();

                    row.Cells[0].Value = Convert.ToDateTime(dt.Rows[i][0].ToString()).ToString("MM/dd/yyyy");
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

                    row.Cells[7].Value = gm.toAccountingFormat(tcr - tdr);//string.Format("{0:#,##0.##}", tcr - tdr); //(tdr - tcr).ToString("0,000.00");
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
                    MessageBox.Show("Generated Supplier's Ledger is Empty");                
                }

            }

            txt_total_debit.Text = gm.toAccountingFormat(tdr);//string.Format("{0:#,##0.##}", );
            txt_total_credit.Text = gm.toAccountingFormat(tcr); //string.Format("{0:#,##0.##}", ); //tcr.ToString("0,000.00");
            txt_balance.Text = gm.toAccountingFormat(tcr - tdr); //string.Format("{0:#,##0.##}", tdr - tcr); //(tdr - tcr).ToString("0,000.00");
        }

        private void cbo_subsidiary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_subsidiary.SelectedIndex != -1 && cbo_subsidiary.SelectedValue != null)
            {
                Enable_MainOption(true);
            }
        }
    }
}
