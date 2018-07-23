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
    public partial class rpt_z_jrnl : Form
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptdoc;
        String rpt_no = "";
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        thisDatabase db;

        public rpt_z_jrnl()
        {
            InitializeComponent();
            db = new thisDatabase();

            crParameterValues = new ParameterValues();
            crParameterDiscreteValue = new ParameterDiscreteValue();
        }

        private void rpt_z_jrnl_Load(object sender, EventArgs e)
        {
            if (rpt_no == "101")
            {

            }
        }
    }
}
