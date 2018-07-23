using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
// Database
using Npgsql;

namespace Hotel_System
{
    public partial class z_SystemDataUpdate : Form
    {
        private String delimeter = ";";
        private int action = 0; //1=send 2 = upload 3 = download
        private String filesource = "";

        public z_SystemDataUpdate()
        {
            InitializeComponent();
        }

        private void z_SystemDataUpdate_Load(object sender, EventArgs e)
        {

        }

        private void btn_testconect_Click(object sender, EventArgs e)
        {
            String server = txt_server.Text;
            String svr_port = txt_port.Text;
            String svr_user = "postgres";
            String svr_pass = txt_password.Text;
            String lcl_db = "hms_grandapartelle"; // "hms_grandapartelle"; 
            String schema = "rssys";

            frm_enable(false);

            NpgsqlConnection conn = new NpgsqlConnection("Server=" + server + ";Port=" + svr_port + ";User Id="+svr_user+";Password=" + svr_pass + ";Database=" + lcl_db + ";");

            OpenConn(conn);

            frm_enable(true);
        }

        private void btn_close1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            action = 1;

            bgworker.RunWorkerAsync();
        }

        private void btn2_searchfile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Zip Files |*.zip";

            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                String file = openFileDialog1.FileName;

                try
                {
                    txt2_fileupload.Text = openFileDialog1.FileName;

                    if (txt2_fileupload.Text != null)
                    {
                        frm_enable(true);
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
        }

        private void btn2_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn2_upload_Click(object sender, EventArgs e)
        {
            filesource = txt2_fileupload.Text;

            action = 2;

            bgworker.RunWorkerAsync();
        }

        private void btn3_download_Click(object sender, EventArgs e)
        {
            action = 3;

            bgworker.RunWorkerAsync();
        }

        private void btn3_openfolder_Click(object sender, EventArgs e)
        {

        }

        private void btn3_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frm_enable(Boolean bol)
        {
            txt_password.Invoke(new Action(() =>
            {
                txt_password.Enabled = bol;
            }));

            txt_port.Invoke(new Action(() =>
            {
                txt_port.Enabled = bol;
            }));
            
            txt_server.Invoke(new Action(() =>
            {
                txt_server.Enabled = bol;
            }));

            txt2_fileupload.Invoke(new Action(() =>
            {
                txt2_fileupload.Enabled = bol;
            }));

            dtp3_frm.Invoke(new Action(() =>
            {
                dtp3_frm.Enabled = bol;
            }));

            dtp3_to.Invoke(new Action(() =>
            {
                dtp3_to.Enabled = bol;
            }));

            btn_close1.Invoke(new Action(() =>
            {
                btn_close1.Enabled = bol;
            }));

            btn_send.Invoke(new Action(() =>
            {
                btn_send.Enabled = bol;
            }));

            btn_testconect.Invoke(new Action(() =>
            {
                btn_testconect.Enabled = bol;
            }));

            btn2_close.Invoke(new Action(() =>
            {
                btn2_close.Enabled = bol;
            }));

            btn2_searchfile.Invoke(new Action(() =>
            {
                btn2_searchfile.Enabled = bol;
            }));

            btn2_upload.Invoke(new Action(() =>
            {
                btn2_upload.Enabled = bol;
            }));

            btn3_close.Invoke(new Action(() =>
            {
                btn3_close.Enabled = bol;
            }));

            btn3_download.Invoke(new Action(() =>
            {
                btn3_download.Enabled = bol;
            }));

            btn3_openfolder.Invoke(new Action(() =>
            {
                btn3_openfolder.Enabled = bol;
            }));
        }

        private void inc_pbar(int i)
        {
            try
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value += i;
                }));
            }
            catch (Exception) { reset_pbar(); }
        }

        private void reset_pbar()
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));
        }

        private void set_status(String status)
        {
            lbl_status.Invoke(new Action(() =>
            {
                lbl_status.Text = status;
            }));
        }

        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            int ipbr = 1;
            Boolean flag = false;
            String logs = "";

                // send data via internet
            if (action == 1)
            {

            }
                //upload
            else if (action == 2)
            {
                String branchsource = "GA";
                String filedest = @"C:\\RightApps\Uploads\" + branchsource + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".zip";
                String strlne;
                String[] str_arr;

                inc_pbar(ipbr);
                set_status("Processing...");
                frm_enable(false);
                //try
                //{
                File.Copy(filesource, filedest, true);

                using (StreamReader sr = File.OpenText(filedest))
                {
                    thisDatabase db = new thisDatabase();
                    int lnno = 1, pk_cnt = 0;
                    String line, table = "", col = "", val = "", upd_val = "", upd_cond = "";
                    Boolean first = true;

                    while ((line = sr.ReadLine()) != null)
                    {
                        inc_pbar(ipbr);
                        set_status("Uploading at line " + lnno.ToString());
                        strlne = line;

                        //table
                        if (strlne.StartsWith(delimeter))
                        {
                            string[] words = strlne.TrimStart(';').Split(';');

                            table = words[0].ToString(); //table name
                            col = words[1].ToString();  // column names to insert
                            pk_cnt = Convert.ToInt32(words[2].ToString());

                            if (table == "gfolio" || table == "gfhist" || table == "chgfil" || table == "chghist")
                            {
                                //do nothing and do not delete
                            }
                            else if (pk_cnt == 0)
                            {
                                //db.DeleteOnTable(table, "");
                            }
                        }
                        //data
                        else if (String.IsNullOrEmpty(strlne) == false)
                        {
                            str_arr = strlne.Split(';');
                            first = true;
                            int i = 0;

                            for (i = 0; i < str_arr.Length - 2; i++)
                            {
                                if (first)
                                {
                                    val = str_arr[i];
                                }
                                else
                                {
                                    val = val + ", " + str_arr[i];
                                }
                                first = false;

                            }

                            upd_val = str_arr[i];
                            upd_cond = str_arr[i + 1];

                            //get the update upd_cond
                            if (table == "chgfil" || table == "chghist")
                            {
                                String[] splitcol = col.Split(',');
                                String[] splitval = strlne.Split(';');
                                String vreg_num = "", vchg_code = "", vchg_num = "";
                                String creg_num = "", cchg_code = "", cchg_num = "";

                                if (splitcol.Length > 0)
                                {
                                    creg_num = splitcol[0];
                                    cchg_code = splitcol[1];
                                    cchg_num = splitcol[2];

                                    vreg_num = splitval[0];
                                    vchg_code = splitval[1];
                                    vchg_num = splitval[2];

                                    upd_cond = creg_num + "=" + vreg_num + " AND " + cchg_code + "=" + vchg_code + " AND " + cchg_num + "=" + vchg_num + "";
                                }
                            }
                        }

                        if (String.IsNullOrEmpty(table) == false && (String.IsNullOrEmpty(val) == false || String.IsNullOrEmpty(upd_val) == false))
                        {

                            if (table == "gfhist")
                            {
                                DataTable dt = db.QueryOnTableWithParams("gfolio", "1", upd_cond, "");

                                //if exists
                                if (dt.Rows.Count > 0)
                                {
                                    if (db.DeleteOnTable("gfolio", upd_cond) == false)
                                    {
                                        add_logs(lnno.ToString() + " - " + "DELETE FROM gfolio WHERE " + upd_cond);
                                    }
                                }
                            }
                            else if (table == "chghist")
                            {
                                DataTable dt = db.QueryOnTableWithParams("chgfil", "1", upd_cond, "");

                                //if exists
                                if (dt.Rows.Count > 0)
                                {
                                    if (db.DeleteOnTable("chgfil", upd_cond) == false)
                                    {
                                        add_logs(lnno.ToString() + " - " + "DELETE FROM chgfil WHERE " + upd_cond);
                                    }
                                }
                            }


                            add_logs("--> " +lnno.ToString() + " - table=" + table + " : col=" + col + " : val=" + val + " : upd_val=" + upd_val + " : upd_cond=" + upd_cond);

                            if (db.UpSertOnTable(table, col, val, upd_val, upd_cond) == false)
                            {
                                add_logs(lnno.ToString() + " - table=" + table + " : col=" + col + " : val=" + val + " : upd_val=" + upd_val + " : upd_cond=" + upd_cond);
                            }
                        }
                        
                        val = ""; upd_val = ""; upd_cond = "";

                        lnno++;
                    }
                }
                // }
                // catch (Exception er)
                //  {
                //     MessageBox.Show(er.Message);
                // }

                frm_enable(true);
            }
                //dowload
            else if (action == 3)
            {
                String filename = @"C:\\RSS-Branch-Data_" + DateTime.Now.ToString("yyyy-MM-dd") + ".zip";

                using (StreamWriter sw = File.CreateText(filename))
                {
                    thisDatabase db = new thisDatabase();
                    DataTable dt_tableToSendNoMD = db.get_tableToSend_NoMasterdata();
                    DataTable dt_tableToSendMDonly = db.get_tableToSend_MasterdataOnly();
                    DataTable dt = new DataTable();

                    String table = "";
                    String col = "";
                    String dtfrm = dtp3_frm.Value.ToString("yyyy-MM-dd");
                    String dtto = dtp3_to.Value.ToString("yyyy-MM-dd");
                    Boolean haspartable = false;
                    Boolean success = false;

                    for (int i = 0; i < dt_tableToSendNoMD.Rows.Count; i++)
                    {
                        set_status("Downloading...");
                        inc_pbar(ipbr);
                        dt.Rows.Clear();
                        table = dt_tableToSendNoMD.Rows[i]["tablename"].ToString();


                        if (String.IsNullOrEmpty(dt_tableToSendNoMD.Rows[i]["partable"].ToString()) == false || dt_tableToSendNoMD.Rows[i]["partable"].ToString() == "false")
                        {
                            haspartable = true;
                        }
                        else
                        {
                            haspartable = false;
                        }

                        if (String.IsNullOrEmpty(table) == false)
                        {
                            DataTable dt_pk = db.get_columnkey(table);
                            Boolean nodate = false;

                            if (dt_pk.Rows.Count > 0)
                            {
                                nodate = true;
                            }

                            dt = db.get_dataToSend(table, dtfrm, dtto, haspartable, nodate);

                            if (dt.Rows.Count > 0)
                            {
                                success = true;
                                writeFile(sw, table, dt, dt_pk, filename);
                            }
                        }
                    }

                    if (success)
                    {
                        MessageBox.Show("System data dowloaded successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error on downloading data.");
                    }
                }
            }
            set_status("Ready...");
            reset_pbar();
        }

        private void OpenConn(NpgsqlConnection conn)
        {
            try
            {
                conn.Open();

                MessageBox.Show("Connection State " + conn.State.ToString());
            }
            catch (Exception er)
            {
                MessageBox.Show("Connection Exception : " + er.Message);
            }
        }

        private void CloseConn(NpgsqlConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(er.Message);
            }
        }

        //format
        //;tablename;column names; number column pks (0 if none)
        //column values by individual separated by ; columnnames=values for updates; updates condition of pk
        private void writeFile(StreamWriter sw, String tablename, DataTable table, DataTable dt_pk, String filename)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_columns(tablename);
            String colnames = "", upd_colval = "", upd_con = "NULL";
            Boolean firstCol = true;
            String val = "";
            int pbno = 1;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (firstCol)
                {
                    colnames = dt.Rows[i]["column_name"].ToString();
                }
                else
                {
                    colnames = colnames + ", " + dt.Rows[i]["column_name"].ToString();
                }

                firstCol = false;
            }

            sw.WriteLine(delimeter + tablename + delimeter + colnames + delimeter + dt_pk.Rows.Count.ToString());

            foreach (DataRow row in table.Rows)
            {
                inc_pbar(pbno);
                bool firstCol2 = true, firstCol3 = true;
                int i = 0, p=0;

                //loop for values in a line
                foreach (DataColumn col in table.Columns)
                {
                    if (db.iscol_numeric(tablename, dt.Rows[i]["column_name"].ToString()) == true && String.IsNullOrEmpty(row[col].ToString()) == true)
                    {
                        row[col] = "0";
                    }

                    if (db.iscol_date(tablename, dt.Rows[i]["column_name"].ToString()) == true && String.IsNullOrEmpty(row[col].ToString()) == true)
                    {
                        val = "NULL";
                    }
                    else if (String.IsNullOrEmpty(row[col].ToString()))
                    {
                        val = "'" + row[col].ToString().Replace('\n', ' ') + "'";
                    }
                    else
                    {
                        val = "$$" + row[col].ToString().Replace('\n', ' ') + "$$";
                    }

                    if (!firstCol2)
                    {
                        sw.Write(delimeter + " ");
                        upd_colval = upd_colval + ", " + dt.Rows[i]["column_name"].ToString() + "=" + val;
                    }
                    else
                    {
                        upd_colval = dt.Rows[i]["column_name"].ToString() + "=" + val;
                    }

                    //for update condition by pk
                    if (dt_pk.Rows.Count > p)
                    {
                        DataRow[] foundmatch = dt_pk.Select("column_name = '" + dt.Rows[i]["column_name"].ToString() + "'");
                        if (foundmatch.Length != 0)
                        {
                            if (firstCol3)
                            {
                                upd_con = dt.Rows[i]["column_name"].ToString() + "=$$" + row[col].ToString().Replace('\n', ' ') + "$$";
                            }
                            else
                            {
                                upd_con = upd_con + " AND " + dt.Rows[i]["column_name"].ToString() + "=$$" + row[col].ToString().Replace('\n', ' ') + "$$";
                            }
                            firstCol3 = false;
                        }
                    }

                    sw.Write(val);
                    firstCol2 = false;

                    i++;
                }
                //right col=val for update if existing item
                sw.Write(delimeter + " " + upd_colval + delimeter + " " + upd_con);
                sw.WriteLine();
            } 

            sw.WriteLine();
        }

        private void readFile(StreamReader sr, String line)
        {

        }

        private void add_logs(String notes)
        {
            try
            {
                rtxt_log.Text = notes + "  ;; " + rtxt_log.Text;
            }
            catch (Exception) { }
        }
    }
}
