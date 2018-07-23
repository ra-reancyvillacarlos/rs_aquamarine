using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Accounting_Application_System
{
    public partial class z_Jrnlz_ExportFileToUploadStocks : Form
    {
        thisDatabase db;
        GlobalClass gc;
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        String fileOut = "";
        Encoding encodingCSV;
        String typ = "E"; //E-Export, I-Import, U-Upload

        public z_Jrnlz_ExportFileToUploadStocks()
        {
            InitializeComponent();
            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();

            db = new thisDatabase();
            gc = new GlobalClass();

            encodingCSV = Encoding.Unicode;

            gc.load_whouse(cbo_warehouse);
        }

        private void z_Jrnlz_ExportFileToUploadStocks_Load(object sender, EventArgs e)
        {
            DateTime datefrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtp_jrnldt_frm.Value = datefrom;

            displist();

            pbar_panl_hide();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialogCSV = new SaveFileDialog();
            saveFileDialogCSV.InitialDirectory = Application.ExecutablePath.ToString();

            saveFileDialogCSV.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialogCSV.FilterIndex = 0;
            saveFileDialogCSV.RestoreDirectory = true;
            

            if (cbo_rec_num_frm.SelectedIndex == -1 || cbo_rec_num_to.SelectedIndex == -1)
            {
                MessageBox.Show("Please select transactions for stock transfers");
            }
            else if (saveFileDialogCSV.ShowDialog() == DialogResult.OK)
            {
                fileOut = saveFileDialogCSV.FileName.ToString();
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

                dt = db.get_z_upload_stklist(dt_frm, dt_to, typ);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv_list.Rows[0].Clone();

                    row.Cells[0].Value = dt.Rows[i]["whs_code_tgt"].ToString();
                    row.Cells[1].Value = dt.Rows[i]["whs_code_tgt"].ToString();
                    row.Cells[2].Value = dt.Rows[i]["rec_num_frm"].ToString();
                    row.Cells[3].Value = dt.Rows[i]["rec_num_to"].ToString();
                    row.Cells[4].Value = dt.Rows[i]["userid"].ToString();
                    row.Cells[5].Value = dt.Rows[i]["t_date"].ToString();
                    row.Cells[6].Value = dt.Rows[i]["t_time"].ToString();

                    dgv_list.Rows.Add(row);
                }
            }
            catch (Exception er) { }
        }

        private void clr_param()
        {
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
        }

        private void input_enable(Boolean bol)
        {
            cbo_warehouse.Invoke(new Action(() =>
            {
                cbo_warehouse.Enabled = bol;
            }));

            cbo_rec_num_frm.Invoke(new Action(() =>
            {
                cbo_rec_num_frm.Enabled = bol;
            }));

            cbo_rec_num_to.Invoke(new Action(() =>
            {
                cbo_rec_num_to.Enabled = bol;
            }));

            btn_submit.Invoke(new Action(() =>
            {
                btn_submit.Enabled = bol;
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
           
            String user_id = GlobalClass.username, sysdate = db.get_systemdate(""), systime = DateTime.Now.ToString("HH:mm");
            String target = "", inv_frm = "", inv_to = "", code="";
            int dtrows = 0;
            DataTable dt, dt2;
            string strRow = "";
            String separator = "\t";

            cbo_warehouse.Invoke(new Action(() =>
            {
                target = cbo_warehouse.SelectedValue.ToString();
            }));

            cbo_rec_num_frm.Invoke(new Action(() =>
            {
                inv_frm = cbo_rec_num_frm.SelectedValue.ToString();
            }));

            cbo_rec_num_to.Invoke(new Action(() =>
            {
                inv_to = cbo_rec_num_to.SelectedValue.ToString();
            }));

            pbar_panl_show();

            inc_pbar(15);

            dt = db.QueryBySQLCode("SELECT r.rec_num, r.supl_code, r.supl_name, r.reference, r.trnx_date, r.whs_code, r.recipient, r.t_date, r.t_time, "
                                            + "rl.ln_num, rl.item_code, rl.item_desc, rl.unit, rl.recv_qty, rl.price, rl.ln_amnt, rl.discount, "
                                            + "rl.cht_code, rl.cnt_code, rl.proj_code, rl.ln_vat, rl.po_line, rl.lot_no, rl.expiry "
                                            + "FROM rapco.rechdr r RIGHT JOIN rapco.reclne rl ON r.rec_num=rl.rec_num "
                                            + "WHERE r.rec_num between '" + inv_frm + "' AND '" + inv_to + "' AND r.whs_code='" + target + "' "
                                            + "ORDER BY r.rec_num ASC, " + db.castToInteger("rl.ln_num") + " ASC ");

            dtrows = dt.Rows.Count;

            if (dtrows > 0)
            {
                // Creates the CSV file as a stream, using the given encoding.
                StreamWriter sw = new StreamWriter(fileOut, false, this.encodingCSV);
                
                sw.WriteLine(columnNames(dt, separator));
                sw.Flush();
                
                for(int i=0; i < dtrows; i++)
                {
                    if (String.Compare(code, dt.Rows[i][0].ToString()) == 0)
                    {
                        strRow = "";
                    }
                    else
                    {
                        code = dt.Rows[i][0].ToString();
                        strRow = Environment.NewLine;                        
                    }

                    for (int y = 0; y < dt.Columns.Count; y++)
                    {
                        strRow += dt.Rows[i][y].ToString() + separator;

                        inc_pbar(5);
                    }

                    sw.WriteLine(strRow);
                    sw.Flush();
                }

                sw.Close();

                inc_pbar(2);

                db.InsertOnTable("z_upload_stk", "whs_code_tgt, rec_num_frm, rec_num_to, userid, t_date, t_time, typ",
                                       "'" + target + "', '" + inv_frm + "', '" + inv_to + "', '" + user_id + "', '" + sysdate + "', '" + systime + "', '" + typ + "'");
                MessageBox.Show("File successfully exported.");
            }
            else
                MessageBox.Show("No Transaction to export. Please try again.");

            inc_pbar(5);

            displist();

            pbar_panl_hide();
            input_enable(true); 
        }

        private string columnNames(DataTable dtSchemaTable, string delimiter)
        {
            string strOut = "";

            foreach (DataColumn column in dtSchemaTable.Columns)
            {
                strOut += column.ColumnName;
                strOut += delimiter;
            }

            strOut += Environment.NewLine;

            return strOut;
        }

        private void cbo_warehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            String whs_code = cbo_warehouse.SelectedValue.ToString();

            gc.load_stktransfer_invoice(cbo_rec_num_frm, whs_code);
            gc.load_stktransfer_invoice(cbo_rec_num_to, whs_code);
        }
    }
}
