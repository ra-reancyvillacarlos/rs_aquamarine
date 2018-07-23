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
    public partial class z_soaline_update : Form
    {
        a_statementofaccount _frm_soa = null;

        public z_soaline_update()
        {
            InitializeComponent();
        }

        public z_soaline_update(a_statementofaccount frm, String refdesc)
        {
            InitializeComponent();

            _frm_soa = frm;
            txt_reference.Text = refdesc;
        }

        private void z_soaline_update_Load(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
           // _frm_soa.upd_soaln_reference(txt_reference.Text);
            this.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
