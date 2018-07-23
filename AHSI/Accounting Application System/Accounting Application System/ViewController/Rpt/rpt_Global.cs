using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Accounting_Application_System
{
    public partial class rpt_Global : Form
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        String fol_num;
        String rpt_no = "";
        String schema = "";
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;

        public rpt_Global(String rnum)
        {
            InitializeComponent();
            rpt_no = rnum;

            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();
        }

        private void rpt_Global_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            DateTime this_t_date = Convert.ToDateTime(db.get_systemdate(""));

            schema = db.get_schema();

            dtp_frm.Value = this_t_date;
            dtp_to.Value = this_t_date;

            try
            {
                pbar_panl_hide();

                if (rpt_no == "101")
                {
                    this.Text = "Account Activity By ID";
                    grp_bydate.Text = "Transaction Dates";
                    grp_bydate.Show();
                    grp_options.Show();

                    lbl_cbo_1.Text = "Company";
                    lbl_cbo_2.Text = "Market Segment";

                    load_company(cbo_1);
                    load_marketsegment(cbo_2);

                    chk_1.Hide();
                    chk_2.Hide();
                    chk_3.Hide();

                    lbl_cbo_3.Hide();
                    cbo_3.Hide();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void load_userid(ComboBox cbo)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_alluserid();
            cbo.DataSource = dt;
            cbo.DisplayMember = "uid";
            cbo.ValueMember = "uid";
            cbo.SelectedIndex = -1;
        }

        private void load_company(ComboBox cbo)
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_name ASC;");

            cbo.DataSource = dt;
            cbo.DisplayMember = "comp_name";
            cbo.ValueMember = "comp_code";
            cbo.SelectedIndex = -1;
        }

        private void load_marketsegment(ComboBox cbo)
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("market", "mkt_code, mkt_desc", "", "ORDER BY mkt_code ASC;");

            cbo.DataSource = dt;
            cbo.DisplayMember = "mkt_desc";
            cbo.ValueMember = "mkt_code";
            cbo.SelectedIndex = -1;
        }

        private void load_romnumber(ComboBox cbo)
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("rooms", "rom_code, rom_desc", "", "ORDER BY rom_code ASC;");

            cbo.DataSource = dt;
            cbo.DisplayMember = "rom_desc";
            cbo.ValueMember = "rom_code";
            cbo.SelectedIndex = -1;
        }

        private void load_group(ComboBox cbo)
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("resgrp", "grp_code, \"group\"", "", "ORDER BY grp_code ASC;");

            cbo.DataSource = dt;
            cbo.DisplayMember = "group";
            cbo.ValueMember = "grp_code";
            cbo.SelectedIndex = -1;
        }

        private void load_ratetype(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("ratetype", "rate_code, rate_desc", "", "ORDER BY rate_code ASC;");

                cbo.DataSource = dt;
                cbo.DisplayMember = "rate_desc";
                cbo.ValueMember = "rate_code";

                cbo.SelectedIndex = -1;

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
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
            crptviewer.Invoke(new Action(() =>
            {
                crptviewer.ReportSource = myReportDocument;
            }));

            crptviewer.Invoke(new Action(() =>
            {
                crptviewer.Refresh();
            }));
        }

        private void clr_param()
        {
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
        }

        private void input_enable(Boolean bol)
        {
            cbo_1.Invoke(new Action(() =>
            {
                cbo_1.Enabled = bol;
            }));

            cbo_2.Invoke(new Action(() =>
            {
                cbo_2.Enabled = bol;
            }));

            cbo_3.Invoke(new Action(() =>
            {
                cbo_3.Enabled = bol;
            }));

            dtp_frm.Invoke(new Action(() =>
            {
                dtp_frm.Enabled = bol;
            }));

            dtp_to.Invoke(new Action(() =>
            {
                dtp_to.Enabled = bol;
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
            String dtpicker_frm = dtp_frm.Value.ToString("MM-dd-yyyy");
            String dtpicker_to = dtp_to.Value.ToString("MM-dd-yyyy");

            pbar_panl_show();

            try
            {
            }
            catch (Exception)
            {
            }

            reset_pbar();

            input_enable(true);

            pbar_panl_hide();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            input_enable(false);
            bgworker.RunWorkerAsync();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            cbo_1.SelectedIndex = -1;
            cbo_2.SelectedIndex = -1;
            cbo_3.SelectedIndex = -1;

            chk_1.Checked = false;
            chk_2.Checked = false;
            chk_3.Checked = false;
        }
    }
}
