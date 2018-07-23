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
    public partial class z_Jrnlz_ImportFileToUploadStocks : Form
    {
        thisDatabase db;
        GlobalClass gc;
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        String fileOut = "";
        Encoding encodingCSV;
        String typ = "I"; //E-Export, I-Import, U-Upload
        Boolean isenable = true;

        public z_Jrnlz_ImportFileToUploadStocks()
        {
            InitializeComponent();
            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();

            db = new thisDatabase();
            gc = new GlobalClass();

            encodingCSV = Encoding.Unicode;
        }

        private void z_Jrnlz_ImportFileToUploadStocks_Load(object sender, EventArgs e)
        {
            DateTime datefrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtp_jrnldt_frm.Value = datefrom;

            displist();

            pbar_panl_hide();
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtFileToImport.Text) == false && isenable == true)
            {
                fileOut = txtFileToImport.Text;
                input_enable(false);
                bgworker.RunWorkerAsync();
            }
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogCSV = new OpenFileDialog();

            openFileDialogCSV.InitialDirectory = Application.ExecutablePath.ToString();
            openFileDialogCSV.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialogCSV.FilterIndex = 1;
            openFileDialogCSV.RestoreDirectory = true;

            if (openFileDialogCSV.ShowDialog() == DialogResult.OK)
            {
                this.txtFileToImport.Text = openFileDialogCSV.FileName.ToString();
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

                    row.Cells[0].Value = dt.Rows[i]["t_date"].ToString();
                    row.Cells[1].Value = dt.Rows[i]["rec_num_frm"].ToString();
                    row.Cells[2].Value = dt.Rows[i]["rec_num_to"].ToString();
                    row.Cells[3].Value = dt.Rows[i]["userid"].ToString();
                    row.Cells[4].Value = dt.Rows[i]["t_time"].ToString();

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
            isenable = bol;
            
            btn_import.Invoke(new Action(() =>
            {
                btn_import.Enabled = bol;
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
            String target = "", inv_frm = "", inv_to = "", code = "";
            int dtrows = 0;
            DataTable dt = new DataTable();
            DataRow row;
            string strRow = "";
            String separator = "\t";

            pbar_panl_show();

            try
            {

                StreamReader sr = new StreamReader(fileOut, this.encodingCSV, false);
                strRow = sr.ReadLine();
                String[] value = strRow.Split('\t');

                foreach (string dc in value)
                {
                    dt.Columns.Add(new DataColumn(dc));

                    inc_pbar(5);
                }

                while (!sr.EndOfStream)
                {
                    value = sr.ReadLine().Split('\t');

                    if (value.Length == dt.Columns.Count)
                    {
                        row = dt.NewRow();
                        row.ItemArray = value;
                        dt.Rows.Add(row);
                    }

                    inc_pbar(5);
                }

                sr.Close();

                inc_pbar(1);
            }
            catch (Exception er)
            {
                MessageBox.Show("File is currently open by another person. Please close file before importing.");
            }
            
            if (db.import_stktra(dt))
            {
                inc_pbar(15);

                dtrows = dt.Rows.Count;

                inv_frm = dt.Rows[0][0].ToString();
                inv_to = dt.Rows[dtrows - 1][0].ToString();

                db.InsertOnTable("z_upload_stk", "rec_num_frm, rec_num_to, userid, t_date, t_time, typ",
                                           "'" + inv_frm + "', '" + inv_to + "', '" + user_id + "', '" + sysdate + "', '" + systime + "', '" + typ + "'");
                
                MessageBox.Show("File successfully imported to database.");
            }
            else
                MessageBox.Show("Error on importing file. Please check the file and try again.");

            displist();

            pbar_panl_hide();
            input_enable(true); 
        }
    }
}
