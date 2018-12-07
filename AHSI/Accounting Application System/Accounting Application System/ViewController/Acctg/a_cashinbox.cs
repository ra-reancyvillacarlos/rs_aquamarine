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
        string[] WHERE = new string[] { "userid='" + GlobalClass.username + "'" };
        Boolean isNew = true; String notNew = "";
        public a_cashinbox()
        {
            InitializeComponent();
            label8.Text = GlobalClass.username;
            gc.load_accounttitle_payment_only(comboBox1);
            try
            {
                comboBox1.SelectedIndex = -1;
            }
            catch { }
        }

        private void a_cashinbox_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = load_cb("cb.OID AS id, t_date, description, amount, m04.at_desc");
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String id = dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString();
                notNew = dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString();
                WHERE = pushString(WHERE, "cb.OID = '" + id + "'");
                DataTable dt_cur = load_cb("cb.*");
                if (dt_cur.Rows.Count > 0)
                {
                    textBox1.Text = dt_cur.Rows[0]["description"].ToString();
                    textBox2.Text = dt_cur.Rows[0]["amount"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dt_cur.Rows[0]["t_date"].ToString());
                    dateTimePicker2.Value = Convert.ToDateTime(dt_cur.Rows[0]["t_time"].ToString());
                    comboBox1.SelectedValue = dt_cur.Rows[0]["acct_id"].ToString();
                    checkBox1.Checked = Convert.ToBoolean(dt_cur.Rows[0]["dbrsmntid"].ToString());
                }
                WHERE = removeIndex(WHERE, WHERE.Length - 1);
            }
            catch
            {
                if (WHERE.Length > 1)
                {
                    for (int i = 0; i < (WHERE.Length - 1); i++)
                    {
                        WHERE = removeIndex(WHERE, WHERE.Length - 1);
                    }
                }
            }
        }

        private DataTable load_cb(String col)
        {
            String _WHERE = "";
            if (WHERE.Length > 0)
            {
                _WHERE = " WHERE";
                for (int i = 0; i < WHERE.Length; i++)
                {
                    _WHERE += (((i == 0) ? " " : " AND ") + WHERE[i]);
                }
            }
            DataTable dt_dg = db.QueryBySQLCode("SELECT "+col+" FROM rssys.cashinbox cb LEFT JOIN (SELECT at_code, at_desc FROM rssys.m04 WHERE payment='Y') m04 ON m04.at_code = cb.acct_id" + _WHERE + " ORDER BY t_date DESC");
            return dt_dg;
        }

        private string[] pushString(string[] array, string pushString)
        {
            int nLength = array.Length + 1;
            string[] returnResult = new string[nLength];
            for (int i = 0; i < array.Length; i++)
            {
                returnResult[i] = array[i];
            }
            returnResult[nLength - 1] = pushString;
            return returnResult;
        }

        private string[] removeIndex(string[] array, int cIndex)
        {
            if (array.Length < 1)
            {
                return array;
            }
            string[] returnResult = new string[array.Length - 1];
            for (int i = 0; i < array.Length; i++)
            {
                if (i == cIndex)
                {
                    continue;
                }
                returnResult[i] = array[i];
            }
            return returnResult;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isNew = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            groupBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            groupBox1.Enabled = true;
            isNew = true; notNew = "";
            clear_fields();
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            if (checkValues())
            {
                Double text2 = 0.00;
                String text1 = textBox1.Text, combo1 = (comboBox1.SelectedValue ?? "").ToString();
                DateTime dt1 = dateTimePicker1.Value, dt2 = dateTimePicker2.Value;
                Boolean chk1 = checkBox1.Checked;
                try{
                    text2 = Convert.ToDouble(textBox2.Text);
                } catch{}
                String col = "description, amount, userid, dbrsmntid, t_date, t_time, acct_id", val = "'" + text1 + "', '" + text2 + "', '" + GlobalClass.username + "', '" + chk1 + "', '" + dt1.ToString("yyyy-MM-dd") + "', '" + dt2.ToString("HH:mm") + "', '" + combo1 + "'", col_up = "description = '" + text1 + "', amount = '" + text2 + "', userid = '" + GlobalClass.username + "', dbrsmntid = '" + chk1 + "', t_date = '" + dt1.ToString("yyyy-MM-dd") + "', t_time = '" + dt2.ToString("HH:mm") + "', acct_id = '" + combo1 + "'", cond = "cashinbox.OID = '" + notNew + "'";
                try
                {
                    Boolean sQuery = ((isNew) ? db.InsertOnTable("cashinbox", col, val) : db.UpdateOnTable("cashinbox", col_up, cond));
                    String sMessage = ((isNew) ? "added" : "updated");
                    if (sQuery)
                    {
                        MessageBox.Show("Successfully " + sMessage + " Cash in Box"); 
                        dataGridView1.DataSource = load_cb("cb.OID AS id, t_date, description, amount, m04.at_desc");
                        button4.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Error on adding Cash in Box record");
                    }
                }
                catch { }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            groupBox1.Enabled = false;
            clear_fields();
        }

        private void clear_fields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            checkBox1.Checked = false;
        }

        Boolean checkValues()
        {
            try
            {
                if (String.IsNullOrEmpty(textBox1.Text) && String.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Input Description or Amount");
                    return false;
                }
                if (comboBox1.SelectedIndex < 0)
                {
                    MessageBox.Show("Select Accounting Title");
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Report rpt = new Report();
                DateTime dt3 = dateTimePicker3.Value;
                rpt.print_dailysalesexpenses(dt3.ToString("yyyy-MM-dd"));
                rpt.Show();
            }
            catch { }
        }
    }
}
