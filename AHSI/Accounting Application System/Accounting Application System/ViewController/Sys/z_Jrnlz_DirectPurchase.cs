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
    public partial class z_Jrnlz_DirectPurchase : Form
    {
        thisDatabase db;
        GlobalClass gc;
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        String j_code = "PJ";
        int typ = 31; //Purchase Return


        public z_Jrnlz_DirectPurchase()
        {
            InitializeComponent();
            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();

            db = new thisDatabase();
            gc = new GlobalClass();

            gc.load_poinvoice_nonjrnlz(cbo_inv_frm);
            gc.load_poinvoice_nonjrnlz(cbo_inv_to);

            gc.load_journal(cbo_jrnl_entry);

            cbo_jrnl_entry.SelectedValue = j_code;

            //dtp_jrnldt_frm.Value = Convert.ToDateTime(DateTime.Today.Month.ToString("m") + "/01/" + DateTime.Today.Month.ToString("yyyy"));
               
        }

        private void z_Jrnlz_DirectPurchase_Load(object sender, EventArgs e)
        {
            DateTime datefrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtp_jrnldt_frm.Value = datefrom;

            displist(); 

            pbar_panl_hide();  
        }

        private void btn_journalize_Click(object sender, EventArgs e)
        {
            if (cbo_inv_frm.SelectedIndex > -1 && cbo_inv_to.SelectedIndex > -1)
            {
                input_enable(false);
                bgworker.RunWorkerAsync();
            }
        }

        private void dtp_jrnldt_to_ValueChanged(object sender, EventArgs e)
        {
            displist();
        }

        private void displist()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt;
            String dt_frm = dtp_jrnldt_frm.Value.ToString("yyyy-MM-dd");
            String dt_to = dtp_jrnldt_to.Value.ToString("yyyy-MM-dd");
            int i;

            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            try
            {
                dt = db.get_z_jrnllist(dt_frm, dt_to, typ);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv_list.Rows[0].Clone();

                    row.Cells[0].Value = dt.Rows[i]["inv_frm"].ToString();
                    row.Cells[1].Value = dt.Rows[i]["inv_to"].ToString();
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
            catch (Exception er) {  }    
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
            cbo_inv_frm.Invoke(new Action(() =>
            {
                cbo_inv_frm.Enabled = bol;
            }));

            cbo_inv_to.Invoke(new Action(() =>
            {
                cbo_inv_to.Enabled = bol;
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
            String fy = "", inv_date = "", code = "", mo = "", j_num = "", t_date = "", t_desc, supl_code = "", supl_name = "",
                    pay_code="", amt = "0.00", invoice = "",at_code = "", j_num_start = "", j_num_to = "";
            String user_id = GlobalClass.username, sysdate = db.get_systemdate(""), systime=DateTime.Now.ToString("HH:mm");
            String ck_report = "N";
            String inv_frm = "", inv_to = "";
            int i, j, noOfjrnl = 0, dtrows = 0, inc = 0;
            Double total_amt = 0.00;
            DataTable dt, dt2;
            Boolean success = false;
            Item_Array item_arr = new Item_Array();

            pbar_panl_show();

            cbo_inv_frm.Invoke(new Action(() =>
            {
                inv_frm = cbo_inv_frm.SelectedValue.ToString();
            }));

            cbo_inv_to.Invoke(new Action(() =>
            {
                inv_to = cbo_inv_to.SelectedValue.ToString();
            }));

            if (ischkbox_checked(chk_rpt))
                ck_report = "Y";

            dt = db.QueryOnTableWithParams("pinvhd", "*", "inv_num between '" + inv_frm + "' and '" + inv_to + "' AND jrnlz != 'Y' AND cancel='N'", "");

            dtrows = dt.Rows.Count;
            
            if (dtrows > 0)
            {
                inc = ((1 / dtrows) * 100);

                if (inc <= 0) {  inc = 1;  }
            }

            for (i = 0; i < dtrows; i++)
            {
                inv_date = Convert.ToDateTime(dt.Rows[i]["inv_date"].ToString()).ToString("yyyy-MM-dd");
                code = dt.Rows[i]["inv_num"].ToString();
                fy = Convert.ToDateTime(inv_date).ToString("yyyy");
                mo = Convert.ToDateTime(inv_date).ToString("MM");

                supl_code = dt.Rows[i]["supl_code"].ToString();
                supl_name = dt.Rows[i]["supl_name"].ToString();

                j_num = db.get_nextincrementlimitchar(db.get_colval("tr01", "j_num", "j_code='"+j_code+"'"), 8);
                t_desc = dt.Rows[i]["reference"].ToString();
                invoice = code;

                at_code = db.get_colval("m07", "at_code", "c_code='" + supl_code + "'");
                pay_code = dt.Rows[i]["pay_code"].ToString();

                if (i == 0)
                    j_num_start = j_num;

                j_num_to = j_num;

                if (db.add_jrnlz_jrnl(fy, mo, j_code, j_num, t_desc, inv_date, sysdate, systime))
                {
                    dt2 = db.QueryOnTableWithParams("pinvln", "*", "inv_num='" + code + "'", "");
                    
                    for (j = 0; j < dt2.Rows.Count; j++)
                    {
                        item_arr.item_code = dt2.Rows[j]["item_code"].ToString();
                        item_arr.item_desc = dt2.Rows[j]["item_desc"].ToString();
                        item_arr.price = db.get_item_fcp(item_arr.item_code).ToString("0.0000");
                        item_arr.recv_qty = dt2.Rows[j]["inv_qty"].ToString();
                        item_arr.unit = dt2.Rows[j]["unit"].ToString();

                        amt = dt2.Rows[j]["ln_amnt"].ToString();
                        total_amt += Convert.ToDouble(amt);

                        inc_pbar(inc);

                        db.add_jrnlz_jrnl_entry(j_code, j_num, at_code, supl_code, supl_name, amt, invoice, pay_code, item_arr);   
                    }

                    if (pay_code != "COD")
                        db.InsertOnTable("tr06", "c_code, invoice, t_date, j_code, j_num, seq_desc, credit",
                            "'" + supl_code + "', '" + invoice + "', '" + inv_date + "', '" + j_code + "', '" + j_num + "', " + db.str_E(t_desc) + ", " + total_amt.ToString("0.00"));
                    
                    noOfjrnl++;

                    inc_pbar(inc);

                    db.UpdateOnTable("pinvhd", "jrnlz='Y'", "inv_num='" + code + "'");

                    success = true;
                }

                inc_pbar(inc);
            }

            if (success)
                db.InsertOnTable("z_jrnl", "j_code, inv_frm, inv_to, report, user_id, t_date, t_time, j_num_start, j_num_to, noofjrnl, typ",
                                        "'" + j_code + "', '" + inv_frm + "', '" +  inv_to +"', '" + ck_report + "', '" + user_id + "', '" + sysdate + "', '" + systime + "', '" + j_num_start + "', '" + j_num_to + "', '" + noOfjrnl + "', " + typ);
            else
                MessageBox.Show("No PO to journalize. Please try again.");

            displist();

            pbar_panl_hide();
            input_enable(true);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
