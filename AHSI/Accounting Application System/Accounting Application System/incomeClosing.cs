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
    public partial class incomeClosing : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        public incomeClosing()
        {
            InitializeComponent();
            gc.load_branch(comboBox5);
            comboBox5.SelectedValue = GlobalClass.branch;
            gc.load_openperiod(comboBox2);
            gc.load_openperiod(comboBox3);
            gc.load_account_title(comboBox1);
            
            comboBox4.Items.Add("Posted Entries Only");
            comboBox4.Items.Add("Unposted Entries Only");
            comboBox4.Items.Add("Posted and Unposted Entries");
            comboBox4.SelectedIndex = 0;
        }

        private void proc_up()
        {
            String WHERE = "";
            if (get_cbo_value(comboBox2) == "")
            {
                MessageBox.Show("Please select a period from.");
                set_cbo_droppeddown(comboBox2, true);
                return;
            }
            if (get_cbo_value(comboBox3) == "")
            {
                MessageBox.Show("Please select a period to.");
                set_cbo_droppeddown(comboBox3, true);
                return;
            }
            if (get_cbo_index(comboBox4) == -1)
            {
                MessageBox.Show("Please select a view.");
                set_cbo_droppeddown(comboBox4, true);
                return;
            }

            if (get_cbo_index(comboBox5) != -1)
            {
                WHERE = " AND branch='" + get_cbo_value(comboBox5) + "' ";
            }
            String fy = ((String)get_cbo_value(comboBox2).Split('-').GetValue(0)).Trim();
            String pQry = "", p2Qry = "";
            String trQry = "", dtQry = "";
            if (get_cbo_index(comboBox4) == 0)
            {
                trQry = "(SELECT DISTINCT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";
            }
            else if (get_cbo_index(comboBox4) == 1)
            {
                trQry = "(SELECT DISTINCT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
            }
            else if (get_cbo_index(comboBox4) == 2)
            {
                trQry = "(SELECT DISTINCT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04 UNION ALL SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num))";
            }


            dtQry = "(SELECT DISTINCT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from,sx3._to  FROM ( SELECT DISTINCT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(comboBox2) + "' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(comboBox3) + "' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(comboBox2) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + get_cbo_value(comboBox3) + "' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";


            pQry = "(SELECT DISTINCT t4.* FROM " + trQry + " t4 JOIN (SELECT DISTINCT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to) AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

            // modified again by: Reancy 06 26 2018
            // modified by: Reancy 06 06 2018
            DataTable dt = db.QueryBySQLCode("SELECT DISTINCT false AS bool_check, m4.code AS j_num, m4.name AS t_desc, m4.cmp_code AS item_code, initcap(m4.cmp_desc) AS item_desc, m4.at_code, m4.at_desc, m4.pos, m4.m4dr_cr, SUM(CASE WHEN m4.m4dr_cr='C' THEN credit - debit ELSE debit - credit END) as amount, COALESCE((SELECT SUM(budget)::numeric(20,2) FROM rssys.budget WHERE at_code = m4.at_code AND (fy='" + fy + "')), 0.00) AS bal_begin FROM    " + pQry + "     t4 LEFT JOIN (SELECT DISTINCT m4.at_code, m4.at_desc, m3.dr_cr as m3dr_cr, m4.dr_cr as m4dr_cr, m3.name, m3.code, m3.cmp_desc, m3.cmp_code2 as cmp_code, m3.pos  FROM rssys.m04 m4 LEFT JOIN (SELECT DISTINCT m3.*, m2.*, m0.name, m0.code, m2.cmp_code AS cmp_code2, m0.pos FROM rssys.m03 m3 LEFT JOIN rssys.m02 m2 ON m2.cmp_code=m3.cmp_code LEFT JOIN rssys.m01 m1 ON m1.mag_code=m2.mag_code LEFT JOIN (SELECT DISTINCT *,(CASE WHEN name LIKE '%INCOME%' THEN 0 ELSE 1 END) AS pos FROM rssys.m00 WHERE (name in ('INCOME','COST OF SALES','EXPENSES') OR name like('%INCOME%')) ) m0 ON m0.code=m1.accttype_code WHERE m0.code is not null ORDER BY m0.pos, m0.name, m3.acc_code) m3 ON m3.acc_code=m4.acc_code) m4 ON m4.at_code=t4.at_code WHERE 1=1 AND COALESCE(m4.name,'')<>'' GROUP BY m4.code,m4.cmp_code, m4.name, m4.cmp_desc, m4.at_code, m4.at_desc, m4.pos, m4.m4dr_cr HAVING SUM(CASE WHEN m4.m4dr_cr='C' THEN credit - debit ELSE debit - credit END)<>0 ORDER BY m4.code, m4.cmp_code, m4.at_code");

            dataGridView1.DataSource = dt;
        }

        private String get_cbo_value(ComboBox cbo)
        {
            String value = "";
            try
            {
                cbo.Invoke(new Action(() =>
                {
                    try { value = cbo.SelectedValue.ToString(); }
                    catch { }
                }));
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
            return value;
        }
        private void set_cbo_droppeddown(ComboBox cbo, Boolean isdrop)
        {
            cbo.Invoke(new Action(() =>
            {
                cbo.DroppedDown = isdrop;
            }));
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            proc_up();
        }

        private int get_cbo_index(ComboBox cbo)
        {
            int i = -1;

            cbo.Invoke(new Action(() =>
            {
                try { i = cbo.SelectedIndex; }
                catch { }
            }));

            return i;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (backgroundWorker1.IsBusy)
            //    {

            //    }
            //    else
            //    {
            //        backgroundWorker1.RunWorkerAsync();
            //    }
            //}
            //catch { }
            proc_up();
        }

        private void incomeClosing_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String notif = "Message:\n";
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Select Account Title");
            }
            else
            {
                String fy = ((String)get_cbo_value(comboBox2).Split('-').GetValue(0)).Trim();
                String mo = ((String)get_cbo_value(comboBox2).Split('-').GetValue(1)).Trim();
                DataTable dt_cur = db.QueryBySQLCode("SELECT j_num FROM rssys.m05 WHERE j_code = 'GJ' AND j_type='G'");
                String cur_jnum = dt_cur.Rows[0][0].ToString();
                String col = "fy, mo, j_code, j_num, t_date, t_desc, ck_date, user_id, systime, \"sysdate\", branch";
                String val = "'" + fy + "', '" + mo + "', 'GJ', '" + cur_jnum + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + textBox1.Text.ToString() + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + GlobalClass.username + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + (comboBox5.SelectedValue ?? "").ToString() + "'";
                if (db.InsertOnTable("tr01", col, val))
                {
                    notif = notif + "Successfully inserted to tr01\n";
                    int j = 1;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        Boolean bol = false;
                        try { bol = Convert.ToBoolean(dataGridView1["bool_check", i].Value.ToString()); }
                        catch { bol = false; }
                        try
                        {
                            if (bol == true)
                            {
                                String col1 = "j_code, j_num, seq_num, at_code, debit, credit";
                                String val1 = "'GJ', '" + cur_jnum + "', '" + j + "', '" + (comboBox1.SelectedValue ?? "").ToString() + "', '" + ((dataGridView1["m4dr_cr", i].Value.ToString() == "D") ? 0.00 : Convert.ToDouble(dataGridView1["amount", i].Value.ToString())) + "', '" + ((dataGridView1["m4dr_cr", i].Value.ToString() == "C") ? 0.00 : Convert.ToDouble(dataGridView1["amount", i].Value.ToString())) + "'";
                                if (db.InsertOnTable("tr02", col1, val1))
                                {
                                    notif = notif + "Successfully inserted to tr02\n";
                                }
                                else
                                {
                                    notif = notif + "Error on inserting to tr02\n";
                                }

                                j++;
                            }
                        }
                        catch { }
                    }
                    if (db.UpdateOnTable("m05", "j_num = '" + db.get_nextincrement(cur_jnum) + "'", "j_code = 'GJ' AND j_type='G'"))
                    {
                        notif = notif + "Successfully updated m05\n";
                    }
                    else
                    {
                        notif = notif + "Error on updating m05\n";
                    }
                    MessageBox.Show(notif);
                }
                else
                {
                    MessageBox.Show("Error on inserting to tr01");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!String.IsNullOrEmpty((row.Cells[1].Value ?? "").ToString()))
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = checkBox1.Checked;
                }
            }
        }
    }
}
