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
    public partial class a_releasecheck : Form
    {
        private String jrnl = "";
        private String prd = "";
        private String j_num = "";
        private DateTime systemdate;

        List<int> list = new List<int>();
        Boolean isReady = false;

        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        public a_releasecheck()
        {
            InitializeComponent();

            try
            {
                systemdate = Convert.ToDateTime(db.get_systemdate("").ToString());

                gc.load_journal(cbo_journal);
                gc.load_branch(cbo_branch);
                gc.load_openperiod(cbo_period, true);

                cbo_branch.SelectedValue = GlobalClass.branch;

                int fy = Convert.ToInt32(DateTime.Now.ToString("yyyy")), mo = Convert.ToInt32(DateTime.Now.ToString("MM"));
                if (mo > 10)
                {
                    mo = mo % 10;
                    fy++;
                }
                else mo += 2;

                cbo_period.SelectedValue = fy.ToString() + "-" + mo.ToString();
                if (cbo_period.SelectedValue == null)
                {
                    cbo_period.SelectedValue = fy.ToString() + "-0";
                }



                //cbo_period.DroppedDown = true;
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            isReady = true;
        }

        private void a_postingtoledger_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void cbo_journal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;
            try { bgworker.CancelAsync(); }catch { }
            
            bgworker.RunWorkerAsync();
        }

        private void disp_list_fromdb()
        {
            if (!isReady) return;
            String WHERE = "";
            DataTable dt = new DataTable();
            String prd = cbo_period.SelectedValue.ToString();

            String[] p = prd.Split('-');
            String mo = p.GetValue(1).ToString();
            String fy = p.GetValue(0).ToString();



            try { list.Clear(); } catch { }
            try { dgv_list.Rows.Clear(); } catch { }

            if (cbo_branch.SelectedIndex != -1)
            {
                WHERE = " AND branch='" + cbo_branch.SelectedValue + "' "; 
            }
            WHERE += " AND mo<=" + mo + " AND fy<=" + fy + " ";

            dt = db.QueryBySQLCode("SELECT j_num AS \"Ref_Num\", t_date AS \"Date\", t_desc AS \"Description\", payee AS \"Paid To\", ck_num AS \"Check Number\", ck_date AS \"Check Date\", cancel AS \"Cancel\", user_id AS \"User ID\", branch AS \"Branch ID\", 'cr' AS dr_cr, CASE WHEN debit<0 THEN credit+ABS(debit) ELSE credit END AS Amount, seq_num FROM ( SELECT * FROM (SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel,isreleased  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num)) t4 JOIN ( SELECT * FROM rssys.m04 WHERE COALESCE(cib_acct,'N')='Y') m4 ON m4.at_code=t4.at_code WHERE t4.t_desc not like ('%CANCELLED-%') AND t4.credit>0) t4 WHERE j_code='" + (cbo_journal.SelectedValue ?? "").ToString() + "' AND COALESCE(isreleased,'')<>'Y' " + WHERE + ""); 
                

            //SELECT *, 'dr' AS dr_cr, CASE WHEN credit<0 THEN debit+ABS(credit) ELSE debit END AS amount FROM ( SELECT * FROM (SELECT fy, mo, t1.j_code, t1.j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, '' AS at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, cancel  FROM rssys.tr01 t1 LEFT JOIN rssys.tr02 t2 ON(t2.j_code=t1.j_code AND t2.j_num=t1.j_num)) t4 JOIN ( SELECT * FROM rssys.m04 WHERE COALESCE(cib_acct,'N')='Y') m4 ON m4.at_code=t4.at_code WHERE t4.t_desc not like ('%CANCELLED-%') AND t4.debit>0) t4 UNION ALL 
 
            if(dt != null)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!bgworker.IsBusy) return;

                    DataGridViewRow row = (DataGridViewRow)dgv_list.Rows[0].Clone();

                    row.Cells[0].Value = ck_selectall.Checked;

                    row.Cells[1].Value = dt.Rows[i][0].ToString();
                    row.Cells[2].Value = Convert.ToDateTime(dt.Rows[i][1].ToString()).ToString("yyyy-MM-dd");
                    row.Cells[3].Value = dt.Rows[i][2].ToString();
                    row.Cells[4].Value = dt.Rows[i][3].ToString();
                    row.Cells[5].Value = gm.toAccountingFormat(dt.Rows[i][10].ToString());

                    row.Cells[6].Value = dt.Rows[i][4].ToString();
                    if (String.IsNullOrEmpty(row.Cells[6].Value.ToString()))
                        row.Cells[7].Value = "";
                    else
                        row.Cells[7].Value = Convert.ToDateTime(dt.Rows[i][5].ToString()).ToString("yyyy-MM-dd");

                    //dt.Rows[i][6] is cancel field and is not included here

                    row.Cells[8].Value = dt.Rows[i][7].ToString();
                    row.Cells[9].Value = db.get_colval("branch", "name", "code='" + dt.Rows[i][8].ToString() + "'");

                    row.Cells[10].Value = dt.Rows[i][11].ToString();

                    dgv_list.Rows.Add(row);
                }
            }
        }

        private void ck_selectall_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_list.Rows.Count; i++)
            {
                DataGridViewRow row = dgv_list.Rows[i];
                //MessageBox.Show(" " + row.Cells[1].Value.ToString());
                if (!String.IsNullOrEmpty((row.Cells[1].Value ?? "").ToString()))
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = ck_selectall.Checked;
                    if (ck_selectall.Checked)
                    {
                        list.Add(i);
                    }
                    else
                    {
                        list.Remove(i);
                    }
                }
            }
        }

        private void dgv_ckAll(Boolean isck)
        {
            
        }

        private void btn_post_Click(object sender, EventArgs e)
        {
            if (cbo_journal.SelectedIndex == -1)
            {
                MessageBox.Show("Please select journal.");
                cbo_journal.DroppedDown = true;
            }
            else if (list.Count == 0)
            {
                MessageBox.Show("No item selected.");
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to release this item?", "", MessageBoxButtons.YesNo))
                {
                    btn_post.Enabled = false;
                    int r = -1;
                    String j_num, seq_num, j_code = cbo_journal.SelectedValue.ToString();
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        seq_num = dgv_list["seq_num", list[i]].Value.ToString();
                        j_num = dgv_list["ref_num", list[i]].Value.ToString();
                        if (!String.IsNullOrEmpty(seq_num))
                        {
                            db.UpdateOnTable("tr02", "isreleased='Y'", "j_code='" + j_code + "' AND j_num='" + j_num + "' AND seq_num='" + seq_num + "'");

                            dgv_list.Rows.RemoveAt(list[i]);
                        }
                    }

                    list.Clear();
                    btn_post.Enabled = true;
                }
            }
                
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cbo_branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;
            try { bgworker.CancelAsync(); } catch { }
            bgworker.RunWorkerAsync();
        }


        private void dgv_list_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 0)
                {
                    int r = e.RowIndex;
                    dgv_list["ck_select", r].Value = (Boolean)dgv_list["ck_select", r].Value ? false : true;

                    if ((Boolean)dgv_list["ck_select", r].Value)
                    {
                        list.Add(r);
                    }
                    else
                    {
                        list.Remove(r);
                    }
                }
            }
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 0)
                {
                    int r = e.RowIndex;

                    dgv_list["ck_select", r].Value = (Boolean)dgv_list["ck_select", r].Value ? false : true;

                    if ((Boolean)dgv_list["ck_select", r].Value)
                    {
                        list.Add(r);
                    }
                    else
                    {
                        list.Remove(r);
                    }

                }
            }
        }
        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            dgv_list.Invoke(new Action(() => {
                disp_list_fromdb();
            }));

        }

        private void cbo_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;
            if (cbo_period.SelectedIndex != -1)
            {
                cbo_journal.DroppedDown = true;
            }
        }
    }
}
