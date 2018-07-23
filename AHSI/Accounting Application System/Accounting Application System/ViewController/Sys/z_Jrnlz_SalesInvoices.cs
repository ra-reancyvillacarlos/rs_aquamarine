using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Accounting_Application_System
{
    public partial class z_Jrnlz_SalesInvoices : Form
    {
        thisDatabase db;
        GlobalClass gc;
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        String j_code = "SJ";
        int typ = 1;

        public z_Jrnlz_SalesInvoices()
        {
            InitializeComponent();
            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();

            db = new thisDatabase();
            gc = new GlobalClass();

            gc.load_journal(cbo_jrnl_entry);

            cbo_jrnl_entry.SelectedValue = "SJ";

                       
        }

        private void z_Jrnlz_SalesInvoices_Load(object sender, EventArgs e)
        {
            dtp_jrnldt_frm.Value = Convert.ToDateTime("04/01/2015");

            displist(); 

            pbar_panl_hide();  
        }

        private void btn_journalize_Click(object sender, EventArgs e)
        {
            input_enable(false);
            bgworker.RunWorkerAsync();            
        }

        private void dtp_jrnldt_to_ValueChanged(object sender, EventArgs e)
        {
            displist();
        }

        private void displist()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            String dt_frm = dtp_jrnldt_frm.Value.ToString("yyyy-MM-dd");
            String dt_to = dtp_jrnldt_to.Value.ToString("yyyy-MM-dd");
            int i;

            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                dt = db.get_z_jrnllist(dt_frm, dt_to, typ);

                for (i = 0; i < dt.Rows.Count-1 ; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv_list.Rows[0].Clone();

                    row.Cells[0].Value = dt.Rows[i]["dt_frm"].ToString();
                    row.Cells[1].Value = dt.Rows[i]["dt_to"].ToString(); 
                    row.Cells[2].Value = dt.Rows[i]["j_code"].ToString(); 
                    row.Cells[3].Value = dt.Rows[i]["j_num_start"].ToString(); 
                    row.Cells[4].Value = dt.Rows[i]["j_num_to"].ToString(); 
                    row.Cells[5].Value = dt.Rows[i]["noofjrnl"].ToString(); 
                    row.Cells[6].Value = dt.Rows[i]["report"].ToString();
                    row.Cells[7].Value = dt.Rows[i]["user_id"].ToString();
                    row.Cells[8].Value = dt.Rows[i]["t_date"].ToString();
                    row.Cells[9].Value = dt.Rows[i]["t_time"].ToString();

                    dgv_list.Rows.Add(row);
                }
            }
            catch (Exception er) { }    
        }
       
        private Boolean ischkbox_checked(CheckBox chk)
        {
            Boolean ischk = false;

            chk.Invoke(new Action(() =>
            {
                ischk = chk.Checked;
            }));

            return ischk;
        }

        private void clr_param()
        {
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
        }

        private void input_enable(Boolean bol)
        {
            dtp_frm.Invoke(new Action(() =>
            {
                dtp_frm.Enabled = bol;
            }));

            dtp_to.Invoke(new Action(() =>
            {
                dtp_to.Enabled = bol;
            }));

            cbo_jrnl_entry.Invoke(new Action(() =>
            {
                cbo_jrnl_entry.Enabled = bol;
            }));

            chk_rpt.Invoke(new Action(() =>
            {
                chk_rpt.Enabled = bol;
            }));

            btn_journalize.Invoke(new Action(() =>
            {
                btn_journalize.Enabled = bol;
            }));
        }

        private void inc_pbar(int i)
        {
            try
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value += i;
                }));
            }
            catch (Exception) { reset_pbar(); }
        }

        private void reset_pbar()
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));
        }

        private void pbar_panl_hide()
        {
            pnl_pbar.Invoke(new Action(() =>
            {
                pnl_pbar.Hide();
            }));
        }

        private void pbar_panl_show()
        {
            pnl_pbar.Invoke(new Action(() =>
            {
                pnl_pbar.Show();
            }));
        }

        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            ReportDocument myReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            ParameterFieldDefinitions crParameterFieldDefinitions;
            String fy="", ord_date = "", ord_code="", mo="", j_num = "", t_date = "", t_desc, debt_code="", debt_name="",
                    pay_code="", amt = "0.00", invoice = "",at_code = "", j_num_start = "", j_num_to = "";
            String user_id = GlobalClass.username, sysdate = db.get_systemdate(""), systime=DateTime.Now.ToString("HH:mm");
            String ck_report = "N", dt_frm = "", dt_to = "";
            DataTable dt;
            Double amnt_tend = 0.00, change = 0.00;
            int i, noOfjrnl = 0, dtrows = 0, inc = 0;
            Boolean success = false;

            pbar_panl_show();

            dt_frm = dtp_frm.Value.ToString("yyyy-MM-dd"); dt_to = dtp_to.Value.ToString("yyyy-MM-dd");

            if (ischkbox_checked(chk_rpt))
                ck_report = "Y";

            dt = db.QueryOnTableWithParams("orhdr", "*", "ord_date between '" + dt_frm + "' and '" + dt_to + "' AND jrnlz is null", "");
            
            dtrows = dt.Rows.Count;

            if (dtrows > 0)
            {
                inc = ((1 / dtrows) * 100);

                if (inc <= 0)
                {
                    inc = 1;
                }
            }

            for (i = 0; i < dtrows; i++)
            {
                amnt_tend = gc.toNormalDoubleFormat(dt.Rows[i]["amnt_tend"].ToString());
                change = gc.toNormalDoubleFormat(dt.Rows[i]["change"].ToString());

                ord_date = Convert.ToDateTime(dt.Rows[i]["ord_date"].ToString()).ToString("yyyy-MM-dd");
                ord_code = dt.Rows[i]["ord_code"].ToString();
                fy = Convert.ToDateTime(ord_date).ToString("yyyy");
                mo = Convert.ToDateTime(ord_date).ToString("MM");

                debt_code = dt.Rows[i]["debt_code"].ToString();
                debt_name = dt.Rows[i]["debt_name"].ToString();

                j_num = db.get_nextincrementlimitchar(db.get_colval("tr01", "j_num", "j_code='"+j_code+"'"), 8);
                t_desc = ord_code + "-" + dt.Rows[i]["debt_name"].ToString();
                invoice = ord_code;

                at_code = db.get_colval("m06", "at_code", "d_code='" + debt_code + "'");
                amt = (gc.toNormalDoubleFormat(dt.Rows[i]["amnt_tend"].ToString()) - gc.toNormalDoubleFormat(dt.Rows[i]["change"].ToString())).ToString("0.00");

                pay_code = dt.Rows[i]["pay_code"].ToString();

                if (i == 0)
                    j_num_start = j_num;

                j_num_to = j_num;
                
                if (db.add_jrnlz_jrnl(fy, mo, j_code, j_num, t_desc, ord_date, sysdate, systime))
                {
                    inc_pbar(inc);
                    
                    db.add_jrnlz_jrnl_entry(j_code, j_num, at_code, debt_code, debt_name, amt, invoice, pay_code, null);
                    inc_pbar(inc);
                    
                    if (pay_code != "COD")
                    {
                        db.InsertOnTable("tr05", "d_code, invoice, t_date, j_code, j_num, seq_desc, debit",
                            "'" + debt_code + "', '" + invoice + "', '" + ord_date + "', '" + j_code + "', '" + j_num + "', " + db.str_E(t_desc) + ", " + amt);
                        
                    }
                    noOfjrnl++;
                   
                    db.UpdateOnTable("orhdr", "jrnlz='Y'", "ord_code='" + ord_code + "'");

                    success = true;
                }
                
                inc_pbar(inc);
            }

            if (success)
            {
                db.InsertOnTable("z_jrnl", "dt_frm, dt_to, j_code, report, user_id, t_date, t_time, j_num_start, j_num_to, noofjrnl, typ",
                                          "'" + dt_frm + "', '" + dt_to + "', '" + j_code + "', '" + ck_report + "', '" + user_id + "', '" + sysdate + "', '" + systime + "', '" + j_num_start + "', '" + j_num_to + "', '" + noOfjrnl + "', " + typ);
                MessageBox.Show("7");
            }
            else
                MessageBox.Show("No SO to journalize. Please try again.");

            displist();

            pbar_panl_hide();
            input_enable(true);
        }
    }
}
