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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Accounting_Application_System.thisDatabase db1 = new Accounting_Application_System.thisDatabase();
            Hotel_System.thisDatabase db2 = new Hotel_System.thisDatabase();
            String str = "";
            DataTable dt = db1.QueryBySQLCode("SELECT res.table_name, string_agg(res.column_name, ', ') as column_name FROM (SELECT table_name, column_name FROM information_schema.columns WHERE table_schema = 'rssys' order by table_name, column_name) res GROUP BY  res.table_name");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str = textBox1.Text ?? "";
                textBox1.Text = str + "\n" + dt.Rows[i]["table_name"].ToString() + "::" + dt.Rows[i]["column_name"].ToString();
            }


            dt = db2.QueryBySQLCode("SELECT res.table_name, string_agg(res.column_name, ', ') as column_name FROM (SELECT table_name, column_name FROM information_schema.columns WHERE table_schema = 'rssys' order by table_name, column_name) res GROUP BY  res.table_name");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str = textBox2.Text ?? "";
                textBox2.Text = str + "\n" + dt.Rows[i]["table_name"].ToString() + "::" + dt.Rows[i]["column_name"].ToString();
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.A))
            {
                (sender as TextBox).SelectAll();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
