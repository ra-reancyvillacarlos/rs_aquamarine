using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Microsoft;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Accounting_Application_System
{
    public partial class rpt_PrintBarcode : Form
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        thisDatabase db;
        GlobalMethod gm;
        int action = 0;
        String fileloc_acctg = "";
        String fileloc_hotel = "";
        String fileloc_inv = "";
        String fileloc_md = "";

        ReportDocument myReportDocument;
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        ParameterFieldDefinitions crParameterFieldDefinitions;

        DataTable dt, newdt;
        String comp_name, comp_addr;
        String l_itemcode = "", l_itemdesc = "", l_source = "";
        int l_noofcopies = 1;

        public rpt_PrintBarcode(int actionno)
        {
            InitializeComponent(); 
            db = new thisDatabase();
            gm = new GlobalMethod();
            GlobalClass gc = new GlobalClass();
            myReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();

            String system_loc = db.get_system_loc();

            fileloc_acctg = system_loc + "\\Reports\\Accounting\\";
            fileloc_hotel = system_loc + "\\Reports\\Hotel\\";
            fileloc_inv = system_loc + "\\Reports\\Inventory\\";
            fileloc_md = system_loc + "\\Reports\\MD\\";

            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();
            dt = new DataTable();
            newdt = new DataTable();
            l_noofcopies = 1;
            action = actionno;

            try
            {
                //Master Data
                if (action == 1)
                {
                    this.Text = "Print Barcode";
                    cbo_source.SelectedIndex = 0;
                    l_source = "0";
                    gc.load_item(cbo_itemcode);
                }
            }
            catch { }             
        }

        private void rpt_PrintBarcode_Load(object sender, EventArgs e)
        {
            comp_name = db.get_m99comp_name();
            comp_addr = db.get_m99comp_addr();

            pbar_panl_hide();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            action = 1;
            
            if(get_cbo_index(cbo_itemcode) > -1)
            {
                l_itemcode = get_cbo_value(cbo_itemcode);                
            }
            l_noofcopies = gm.toInt(txt_noofcopies.Text);
            bgWorker.RunWorkerAsync();
        }

        private void add_fieldparam(String col, String val)
        {
            crParameterDiscreteValue.Value = val;
            crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions[col];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            clr_param();
            inc_pbar(10);
        }

        private void clr_param()
        {
            try
            {
                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
            }
            catch (Exception) { }
        }

        private int get_cbo_index(ComboBox cbo)
        {
            int i = -1;

            cbo.Invoke(new Action(() =>
            {
                i = cbo.SelectedIndex;
            }));

            return i;
        }

        private String get_cbo_value(ComboBox cbo)
        {
            String value = "";

            cbo.Invoke(new Action(() =>
            {
                value = cbo.SelectedValue.ToString();
            }));

            return value;
        }

        private String get_cbo_text(ComboBox cbo)
        {
            String txt = "";

            cbo.Invoke(new Action(() =>
            {
                txt = cbo.Text.ToString();
            }));

            return txt;
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

        private void disp_reportviewer(ReportDocument myReportDocument)
        {
            try
            {
                crptviewer.Invoke(new Action(() =>
                {
                    crptviewer.ReportSource = myReportDocument;
                }));

                crptviewer.Invoke(new Action(() =>
                {
                    crptviewer.Refresh();
                }));
            }
            catch (Exception) { }
        }

        private void inc_pbar(int i)
        {
            try
            {
                if (pbar.Value + i <= 100)
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value += i;
                    }));
                }
                else
                {
                    reset_pbar();
                }

            }
            catch (Exception)
            {
                reset_pbar();
            }
        }

        private void reset_pbar()
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    pbar.Value = 0;
                }));
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
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

        private void input_enable(Boolean bol)
        {
            cbo_source.Invoke(new Action(() =>
            {
                cbo_source.Enabled = bol;
            }));

            cbo_itemcode.Invoke(new Action(() =>
            {
                cbo_itemcode.Enabled = bol;
            }));
            
            txt_noofcopies.Invoke(new Action(() =>
            {
                txt_noofcopies.Enabled = bol;
            }));

            btn_clear.Invoke(new Action(() =>
            {
                btn_clear.Enabled = bol;
            }));

            btn_submit.Invoke(new Action(() =>
            {
                btn_submit.Enabled = bol;
            }));
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            NumberToEnglish_orig amtinwords = new NumberToEnglish_orig();
            String _schema = db.get_schema();
            String WHERE = "";

            pbar_panl_show();
            input_enable(false);

            if (action == 1)
            {
                inc_pbar(15);

                dt = db.get_item_details_multiple(l_itemcode, l_noofcopies);

                inc_pbar(25);
                myReportDocument.Load(fileloc_md + "Barcode.rpt");

                inc_pbar(25);
                myReportDocument.Database.Tables[0].SetDataSource(dt);
                inc_pbar(10);
            }

            inc_pbar(5);
            disp_reportviewer(myReportDocument);

            reset_pbar();
            input_enable(true);
            pbar_panl_hide();
        }
    }
}
