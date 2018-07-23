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
    public partial class i_PO_Password : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        public i_PO _frm ;
        public i_PO_Password()
        {
            InitializeComponent();
        }
        public i_PO_Password(i_PO frm)
        {
         InitializeComponent();
         this._frm = frm;
        }
        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (_frm != null)
            {
                DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE pwd='"+textBox1.Text+"' AND uid='"+GlobalClass.username+"'");
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Password.");
                }
                if(dt.Rows.Count < 0)
                {
                    MessageBox.Show("Password doesn't match to your account.");
                }
                else if (dt.Rows.Count > 0)
                {
                    _frm.ok = "ok";
                    MessageBox.Show("Process Complete.");
                    this.Close();
                }
                else {
                    MessageBox.Show("Password doesn't match to your account.");
                }
            }
        }
    }
}
