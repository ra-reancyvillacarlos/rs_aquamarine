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
    public partial class z_set_coverage : Form
    {
        a_statementofaccount _frm_soa = null;

        public z_set_coverage()
        {
            InitializeComponent();
        }

        public z_set_coverage(a_statementofaccount frm)
        {
            InitializeComponent();

            _frm_soa = frm;
        }

        private void z_set_coverage_Load(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (_frm_soa != null)
            {

            }

            this.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
