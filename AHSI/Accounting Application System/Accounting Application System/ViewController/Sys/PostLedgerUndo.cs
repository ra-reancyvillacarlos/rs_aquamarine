using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace Accounting_Application_System.ViewController.Sys
{
    public partial class PostLedgerUndo : Form
    {
        thisDatabase db = new thisDatabase();
        DataTable dt_tr = new DataTable();
        DataTable dt_lbl = new DataTable();
        DataTable dt_up = new DataTable();
        DataTable dt_del = new DataTable();
        NpgsqlConnection conn;
        String msg = "";
        String cbo_f;
        Boolean tr1_err, tr2_err;
        Boolean done_next_row = false;

        public PostLedgerUndo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PostLedgerUndo_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection("Server=" + db.serv + ";Port=5432;User Id=postgres;Password=" + db.pwd + ";Database=" + db.l_db + ";");
            get_fy();
            get_al_fy();
            get_tr_post();
            timer1.Start();
            try
            {
                comboBox2.SelectedIndex = 0;
            }
            catch { }
        }

        public void get_fy()
        {
            DataTable dt_fy = new DataTable();
            dt_fy = db.QueryBySQLCode("SELECT DISTINCT x3.fy FROM rssys.x03 x3 WHERE x3.fy NOT IN (SELECT t4.fy FROM rssys.tr04 t4)");

            comboBox1.DataSource = dt_fy;
            comboBox1.ValueMember = "fy";
            comboBox1.DisplayMember = "fy";

            try
            {
                comboBox1.SelectedIndex = -1;
            }
            catch { }
        }

        public void get_al_fy()
        {
            DataTable dt_fy = new DataTable();
            dt_fy = db.QueryBySQLCode("SELECT DISTINCT x3.fy FROM rssys.x03 x3 WHERE x3.fy IN (SELECT t4.fy FROM rssys.tr04 t4)");

            comboBox3.DataSource = dt_fy;
            comboBox3.ValueMember = "fy";
            comboBox3.DisplayMember = "fy";

            try
            {
                comboBox3.SelectedIndex = -1;
            }
            catch { }
        }

        public void get_tr_post()
        {
            //at_code. at_desc. sl_code sl_name debit, credit, invoice
            dt_tr = db.QueryBySQLCode("SELECT t4.oid, t4.fy AS \"Year\", t4.mo AS \"Month\", t4.t_date AS \"Date\", t4.t_desc AS \"Description\", t4.at_code AS \"Account Code\", t4.at_desc AS \"Account Name\", t4.sl_code AS \"Subsidiary No.\", t4.sl_name AS \"Subsidiary Name\", t4.debit AS \"Debit\", t4.credit AS \"Credit\", t4.invoice AS \"Invoice\" FROM rssys.tr04 t4 WHERE t4.t_date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'");
            dt_lbl = db.QueryBySQLCode("SELECT COUNT(*) FROM rssys.tr04 t4 WHERE t4.t_date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'");
            lbl_rows_count.Text = dt_lbl.Rows[0][0].ToString();
            try
            {
                if (bgworker.IsBusy)
                {

                }
                else
                {
                    bgworker.RunWorkerAsync();
                }
            }
            catch { }
        }

        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                dgv_list.Invoke(new Action(() =>
                {
                    dgv_list.DataSource = dt_tr;
                    dgv_list.Columns[0].ReadOnly = false;
                    dgv_list.Columns[1].Visible = false;
                }));
            }
            catch { }
            rdonly();
        }

        private void rdonly()
        {
            for (int i = 1; i < 12; i++)
            {
                dgv_list.Columns[i].ReadOnly = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv_list.Rows)
            {
                if (!String.IsNullOrEmpty((row.Cells[1].Value ?? "").ToString()))
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = checkBox1.Checked;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Choose Year to change selected rows.");
                comboBox1.DroppedDown = true;
            }
            else
            {
                //MessageBox.Show(dgv_list[1, dgv_list.CurrentRow.Index].Value.ToString());
                //Boolean del_dt = db.DeleteOnTable();
                if (MessageBox.Show("Are you sure? You cant undo this action.", "Undo Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        //bgworker.CancelAsync();
                        if (bgworker_update.IsBusy)
                        {

                        }
                        else
                        {
                            bgworker_update.RunWorkerAsync();
                        }
                    }
                    catch { }
                }
            }
        }

        private Boolean str_qry_code(String qry)
        {
            Boolean flag = false;

            try
            {
                conn.Close();
                try
                {
                    conn.Open();
                }
                catch { }

                NpgsqlCommand command = new NpgsqlCommand(qry, conn);

                Int32 rowsaffected = command.ExecuteNonQuery();

                try
                {
                    conn.Close();
                }
                catch { }

                flag = true;
            }
            catch (Exception er)
            {

            }

            return flag;
        }

        private void upworker_DoWork(object sender, DoWorkEventArgs e)
        {
            msg = "";

            dgv_list.Invoke(new Action(() =>
            {
                for (int i = 0; i < dgv_list.Rows.Count; i++)
                {

                    reset_pbar();
                    try
                    {
                        String dgvi_ck = dgv_list[0, i].Value.ToString();
                        String next_c_row;
                        Boolean next_r_r = false;
                        try
                        {
                            next_c_row = dgv_list[0, i + 1].Value.ToString();
                            next_r_r = Convert.ToBoolean(next_c_row); 
                        }
                        catch { next_c_row = "false"; next_r_r = false; }
                        String dgvi = dgv_list[1, i].Value.ToString();
                        Boolean stat = false;
                        Boolean statt = false;
                        Boolean stat1 = false;
                        Boolean stat2 = false;
                        Boolean stat3 = false;
                        Boolean stat4 = false;

                        tr1_err = false;
                        tr2_err = false;
                        
                        try { stat = Convert.ToBoolean(dgvi_ck); }
                        catch { stat = false; }
                        try
                        {
                            inc_pbar(10);
                            if (stat == true)
                            {
                                inc_pbar(10);
                                statt = str_qry_code("INSERT INTO rssys.tr01 (fy, mo, j_code, j_num, t_date, t_desc, payee, ck_date, user_id, systime, cancel, relsd, rec_num, asci_code, jo_code, pr_code, purc_ord, inv_num, dr_code, vp_num, \"sysdate\", ck_num, branch, collectorid) SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_date, user_id, systime, COALESCE('', NULL), relsd, COALESCE('', NULL), COALESCE('', NULL), jo_code, pr_code, purc_ord, inv_num, dr_code, vp_num, \"sysdate\", ck_num, branch, COALESCE('', NULL) FROM rssys.tr04 WHERE oid = '" + dgvi + "'");
                                stat1 = str_qry_code("INSERT INTO rssys.tr02 (j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, prj_code, debit, credit, invoice, seq_desc, item_code, item_desc, unit, recv_qty, clrd, price, rep_code, pay_code, or_code, or_lne, scc_code, whs_code, chg_code, chg_desc, chg_num, isreleased) SELECT j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, prj_code, debit, credit, invoice, seq_desc, item_code, item_desc, unit, recv_qty, clrd, price, rep_code, pay_code, or_code, CAST(or_lne AS INTEGER), scc_code, COALESCE('', NULL), chg_code, chg_desc, chg_num, isreleased FROM rssys.tr04 WHERE oid = '" + dgvi + "'");
                                //backup
                                //AND (j_code NOT IN (SELECT j_code FROM rssys.tr01) AND j_num NOT IN (SELECT j_num FROM rssys.tr01))
                                //backup
                                //AND (j_code NOT IN (SELECT j_code FROM rssys.tr02) AND j_num NOT IN (SELECT j_num FROM rssys.tr02) AND seq_num NOT IN (SELECT seq_num FROM rssys.tr02))
                                if (statt == true)
                                {
                                    tr1_err = true;

                                    write_log(DateTime.Now.ToString() + ": Successfully added Table: tr01, OID: " + dgvi + "\n");

                                    stat3 = db.UpdateOnTable("tr01", " fy = '" + cbo_f + "'", "j_code = (SELECT j_code FROM rssys.tr04 WHERE oid = '" + dgvi + "') AND j_num = (SELECT j_num FROM rssys.tr04 WHERE oid = '" + dgvi + "')");
                                    if (stat3 == true)
                                    {
                                        write_log(DateTime.Now.ToString() + ": Successfully updated Table: tr01, Year: " + cbo_f + "\n");
                                    }
                                    else
                                    {
                                        write_log(DateTime.Now.ToString() + ": Skip on updating Table: tr01, Year: " + cbo_f + "\n");
                                    }
                                }
                                else
                                {
                                    write_log(DateTime.Now.ToString() + ": Skip on adding Table: tr01, OID: " + dgvi + " Cause: j_code or j_num FROM tr01 already in duplicate keys\n");
                                }
                                inc_pbar(20);

                                if (stat1 == true)
                                {
                                    tr2_err = true;
                                    write_log(DateTime.Now.ToString() + ": Successfully added Table: tr02, OID: " + dgvi + "\n");
                                }
                                else
                                {
                                    write_log(DateTime.Now.ToString() + ": Skip on adding Table: tr02, OID: " + dgvi + "\n Cause: j_code, j_num or seq_num FROM tr02 already in duplicate keys\n");
                                }
                                inc_pbar(10);

                                //if (tr1_err == true && tr2_err == true)
                                //{
                                    stat4 = perf_up(dgvi, next_r_r);
                                    if (next_r_r == false || stat4 == true || done_next_row == true)
                                    {
                                        stat2 = str_qry_code("DELETE FROM rssys.tr04 WHERE oid = '" + dgvi + "'");
                                        if (stat2 == true)
                                        {
                                            write_log(DateTime.Now.ToString() + ": Successfully deleted Table: tr04, OID: " + dgvi + "\n");
                                        }
                                        else
                                        {
                                            write_log(DateTime.Now.ToString() + ": Skip on deleting Table: tr04, OID: " + dgvi + "\n");
                                        }
                                    }
                                    else
                                    {
                                        write_log(DateTime.Now.ToString() + ": Skip on deleting Tables: tr02 AND tr01 using beg j_code and j_num \n");
                                    }
                                //}
                                inc_pbar(10);
                            }
                        }
                        catch { }
                    }
                    catch { }

                    inc_pbar(40);
                }
            }));
        }

        private Boolean perf_up(String oid, Boolean next_row)
        {
            DataTable dt_f_del = new DataTable();
            DataTable dt_r_tdel = new DataTable();
            DataTable dt_ch_tdel = new DataTable();
            Boolean stat = false;
            Boolean s_c = false, s_t = false;
            //try
            //{
            //2017
                dt_f_del = db.QueryBySQLCode("SELECT DISTINCT fy FROM rssys.x03 WHERE closed = 'Y'");
                if (dt_f_del.Rows.Count < 1)
                {
                    stat = true;
                }
                else
                {
                    foreach (DataRow dt_r_del in dt_f_del.Rows)
                    {
                        //try
                        //{
                        s_c = db.QueryBySQLCode_bool("DELETE FROM rssys.tr02 WHERE j_code = (SELECT DISTINCT beg_j_code FROM rssys.tr04 WHERE oid = '" + oid + "') AND j_num = (SELECT DISTINCT beg_j_num FROM rssys.tr04 WHERE oid = '" + oid + "') AND seq_num = (SELECT DISTINCT seq_num FROM rssys.tr04 WHERE oid = '" + oid + "')");

                        if (s_c == true)
                        {
                            stat = true;
                            write_log(DateTime.Now.ToString() + ": Successfully deleted Tables: tr02, j_code: " + dt_ch_tdel.Rows[0][1].ToString() + ", j_num: " + dt_ch_tdel.Rows[0][0].ToString() + ", seq_num: " + dt_ch_tdel.Rows[2][0].ToString() + "\n");

                            dt_r_tdel = db.QueryBySQLCode("SELECT DISTINCT fy FROM rssys.tr04 WHERE fy = '" + dt_r_del[0].ToString() + "'");
                            if (dt_r_tdel.Rows.Count > 1 && next_row == false)
                            {
                                stat = true;
                            }
                            else if (dt_r_tdel.Rows.Count < 2 && next_row == true)
                            {
                                done_next_row = true;
                                dt_ch_tdel = db.QueryBySQLCode("SELECT DISTINCT beg_j_num, beg_j_code, beg_seq_num FROM rssys.tr04 WHERE oid = '" + oid + "'");
                                //try
                                //{
                                s_t = db.QueryBySQLCode_bool("DELETE FROM rssys.tr01 WHERE j_code = (SELECT DISTINCT beg_j_code FROM rssys.tr04 WHERE oid = '" + oid + "') AND j_num = (SELECT DISTINCT beg_j_num FROM rssys.tr04 WHERE oid = '" + oid + "')");
                                //}
                                //catch { }

                                if (s_t == true)
                                {
                                    write_log(DateTime.Now.ToString() + ": Successfully deleted Tables: tr01, j_code: " + dt_ch_tdel.Rows[0][1].ToString() + ", j_num: " + dt_ch_tdel.Rows[0][0].ToString() + "\n");
                                    stat = true;
                                }
                                else
                                {
                                    write_log(DateTime.Now.ToString() + ": Error on deleting Tables: tr02 AND tr01, j_code: " + dt_ch_tdel.Rows[0][1].ToString() + ", j_num: " + dt_ch_tdel.Rows[0][0].ToString() + "\n");
                                    stat = false;
                                }
                            }
                            else
                            {
                                stat = true;
                            }
                        }
                        else
                        {
                            write_log(DateTime.Now.ToString() + ": Error on deleting Tables: tr02, j_code: " + dt_ch_tdel.Rows[0][1].ToString() + ", j_num: " + dt_ch_tdel.Rows[0][0].ToString() + ", seq_num: " + dt_ch_tdel.Rows[2][0].ToString() + "\n");
                            stat = true;
                        }
                        //}
                        //catch { }
                    }
                }
            //}
            //catch { }
            return stat;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_f = comboBox1.SelectedValue.ToString();
        }

        private void inc_pbar(int i)
        {
            try
            {
                if (pbar.Value + i <= 100)
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value += i;
                    }));
                }
                else
                {
                    reset_pbar();
                }

            }
            catch (Exception)
            {
                reset_pbar();
            }
        }

        private void reset_pbar()
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    try { pbar.Value = 0; }
                    catch { }
                }));
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label3.ForeColor == Color.Black)
            {
                label3.ForeColor = Color.Red;
            }
            else
            {
                label3.ForeColor = Color.Black;
            }
        }

        private void bgworker_update_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            get_tr_post();
            reset_pbar();
            String loc_db = db.get_system_loc();
            String cur_user = GlobalClass.username;
            MessageBox.Show("View logs at " + loc_db + "\\Logs\\" + cur_user + "-Logs_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt");
        }

        private void write_log(String messg)
        {
            String loc_db = db.get_system_loc();

            String path = @""+loc_db+"Logs";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    proc_exe(path, messg);
                }
                else
                {
                    Directory.CreateDirectory(path);
                    proc_exe(path, messg);
                }
            }
            catch (Exception e) { Console.WriteLine("The process failed: {0}", e.ToString()); } 
        }

        private void proc_exe(String path, String messg)
        {
            String cur_user = GlobalClass.username;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"" + path + "\\" + cur_user + "-Logs_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt", true))
            {
                file.WriteLine(messg);
            }
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    tabControl1.SelectedTab = tabPage1;
                    tabPage1.Show();
                    tabControl1.TabStop = false;
                    break;

                case 1:
                    tabControl1.SelectedTab = tabPage2;
                    tabPage2.Show();
                    tabControl1.TabStop = false;
                    break;

                default:
                    break;
            }
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            get_tr_post();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            get_tr_post();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt_tr = db.QueryBySQLCode("SELECT t4.oid, t4.fy AS \"Year\", t4.mo AS \"Month\", t4.t_date AS \"Date\", t4.t_desc AS \"Description\", t4.at_code AS \"Account Code\", t4.at_desc AS \"Account Name\", t4.sl_code AS \"Subsidiary No.\", t4.sl_name AS \"Subsidiary Name\", t4.debit AS \"Debit\", t4.credit AS \"Credit\", t4.invoice AS \"Invoice\" FROM rssys.tr04 t4 WHERE t4.fy = '"+comboBox3.SelectedValue.ToString()+"'");
            dt_lbl = db.QueryBySQLCode("SELECT COUNT(*) FROM rssys.tr04 t4 WHERE t4.fy = '" + comboBox3.SelectedValue.ToString() + "'");
            lbl_rows_count.Text = dt_lbl.Rows[0][0].ToString();
            try
            {
                if (bgworker.IsBusy)
                {

                }
                else
                {
                    bgworker.RunWorkerAsync();
                }
            }
            catch { }
        }
    }
}
