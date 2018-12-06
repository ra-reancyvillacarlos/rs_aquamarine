using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System.ViewController.Acctg
{
    public partial class a_cashinbox : Form
    {
        GlobalMethod gm = new GlobalMethod();
        GlobalClass gc = new GlobalClass();
        thisDatabase db = new thisDatabase();
        public a_cashinbox()
        {
            InitializeComponent();
            label8.Text = GlobalClass.username;
            gc.load_accounttitle_payment_only(comboBox1);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void a_cashinbox_Load(object sender, EventArgs e)
        {
        }
    }
}
