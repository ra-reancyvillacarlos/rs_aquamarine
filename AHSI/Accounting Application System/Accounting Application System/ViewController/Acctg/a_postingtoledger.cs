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
    public partial class a_postingtoledger : Form
    {
        private String jrnl = "";
        private String prd = "";
        private String j_num = "";
        private DateTime systemdate;
        Boolean isReady = false;

        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        public a_postingtoledger()
        {
            InitializeComponent();
        }

        private void a_postingtoledger_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            lbl_journal_id.Hide();
            
            try
            {
                gc.load_openperiod(cbo_period, true);
                gc.load_journal(cbo_journal);
                gc.load_branch(cbo_branch);

                cbo_branch.SelectedValue = GlobalClass.branch;


                int fy = Convert.ToInt32(DateTime.Now.ToString("yyyy")), mo = Convert.ToInt32(DateTime.Now.ToString("MM"));
                if (mo > 10)
                {
                    mo = mo % 10;
                    fy++;
                } else mo += 2;

                cbo_period.SelectedValue = fy.ToString() + "-" + mo.ToString();
                if (cbo_period.SelectedValue == null)
                {
                    cbo_period.SelectedValue = fy.ToString() + "-0";
                }

            }
            catch (Exception er) { MessageBox.Show(er.Message); }
            isReady = true;
        }

        private void cbo_journal_SelectedIndexChanged(object sender, EventArgs e)
        {
            String mo = "";
            String fy = "";
            String[] p;
            

            try
            {
                if (cbo_journal.SelectedIndex > -1 && cbo_period.SelectedIndex < 0)
                {
                    //MessageBox.Show("Please select period.");
                }
                else
                {
                    prd = cbo_period.SelectedValue.ToString();
                    jrnl = cbo_journal.SelectedValue.ToString();

                    p = prd.Split('-');
                    mo = p.GetValue(1).ToString();
                    fy = p.GetValue(0).ToString();

                    disp_list_fromdb(fy, mo, jrnl);

                    lbl_period.Text = cbo_period.Text.ToString();
                    lbl_journal.Text = cbo_journal.Text.ToString();
                    lbl_journal_id.Text = jrnl;

                    btn_post.Enabled = true;
                    btn_print.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void disp_list_fromdb(String fy, String mo, String jrnl)
        {
             
            DataTable dt = new DataTable();
            int i;

            //dt = db.get_journalentrylist(fy, mo, jrnl, false);
            dt = db.get_journalentrylist(fy, mo, jrnl, (cbo_branch.SelectedValue??"").ToString(), false);
            if(dt != null)
            {
                try { dgv_list.Rows.Clear(); }catch { }

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv_list.Rows[0].Clone();

                    row.Cells[0].Value = ck_selectall.Checked;

                    row.Cells[1].Value = dt.Rows[i][0].ToString();
                    row.Cells[2].Value = Convert.ToDateTime(dt.Rows[i][1].ToString()).ToString("yyyy-MM-dd");
                    row.Cells[3].Value = dt.Rows[i][2].ToString();
                    row.Cells[4].Value = dt.Rows[i][3].ToString();
                    row.Cells[5].Value = dt.Rows[i][4].ToString();

                    if (String.IsNullOrEmpty(row.Cells[5].Value.ToString()))
                        row.Cells[6].Value = "";
                    else
                        row.Cells[6].Value = Convert.ToDateTime(dt.Rows[i][5].ToString()).ToString("yyyy-MM-dd");

                    //dt.Rows[i][6] is cancel field and is not included here

                    row.Cells[7].Value = dt.Rows[i][7].ToString();
                    row.Cells[8].Value = db.get_colval("branch", "name", "code='" + dt.Rows[i][8].ToString() + "'");

                    dgv_list.Rows.Add(row);
                }
            }
        }

        private void ck_selectall_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv_list.Rows)
            {
                //MessageBox.Show(" " + row.Cells[1].Value.ToString());
                if (!String.IsNullOrEmpty((row.Cells[1].Value??"").ToString()))
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = ck_selectall.Checked;
                }
            }
        }

        private void dgv_ckAll(Boolean isck)
        {
            
        }

        private void btn_post_Click(object sender, EventArgs e)
        {
            Boolean status = false;
            DataTable dt = new DataTable();
            String
                fy="",
                mo="",
            t_date="",
            t_desc="",
           user_id="",
           ck_date="",
            ck_num="",
           sysdate = "",
           systime = "",
            j_code="",
             j_num="",
           seq_num = "",
           seq_desc = "",
           at_code="",
           sl_code="",
           sl_name="",
           cc_code="",
             debit="",
            credit="",
           invoice="",
         item_code="",
         item_desc = "",
         isreleased = "",
         jo_code = "",
         purc_ord = "",
         inv_num = "",
         pr_code = "",
         dr_code = "",
         branch = "",
         chg_code = "",
         chg_desc = "",
         chg_num = "";

                // fy, mo,t_date,t_desc,user_id ,ck_date,ck_num,sysdate,j_code,j_num,seq_num,at_code,sl_code,sl_name,cc_code,debit,credit,invoice,item_code,item_desc,
            String rw = "";
            String col = "", val = "";
            int index = 0;
            for (int r = 0; r < dgv_list.Rows.Count - 1; r++)
            {
                try { status = Convert.ToBoolean(dgv_list["ck_select", r].Value.ToString()); }
                catch
                {
                    status = false;
                }

                try
                {
                    if (status)
                    {
                        status = false;

                        dt = db.QueryBySQLCode("SELECT t1.fy,t1.mo,t1.t_date,t1.t_desc,t1.user_id,t1.ck_date,t1.ck_num,t1.sysdate,t1.systime,t1.branch,t1.jo_code, t1.purc_ord, t1.inv_num, t1.pr_code, dr_code, t2.* FROM rssys.tr02 t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code) WHERE t2.j_num='" + dgv_list["ref_num", r].Value.ToString() + "' AND t2.j_code='" + cbo_journal.SelectedValue.ToString() + "'");
                       
                        if(dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count;i++ )
                            {
                                fy = dt.Rows[i]["fy"].ToString();
                                mo = dt.Rows[i]["mo"].ToString();
                                t_date = gm.toDateString(dt.Rows[i]["t_date"].ToString(),"");
                                t_desc = dt.Rows[i]["t_desc"].ToString();
                                user_id = dt.Rows[i]["user_id"].ToString();

                                ck_date = gm.toDateString(dt.Rows[i]["ck_date"].ToString(),"");
                                ck_num = dt.Rows[i]["ck_num"].ToString();
                                sysdate = gm.toDateString(dt.Rows[i]["sysdate"].ToString(),"");
                                systime = dt.Rows[i]["systime"].ToString();
                                j_code = dt.Rows[i]["j_code"].ToString();
                                j_num = dt.Rows[i]["j_num"].ToString();
                                seq_num = dt.Rows[i]["seq_num"].ToString();
                                seq_desc = dt.Rows[i]["seq_desc"].ToString();
                                at_code = dt.Rows[i]["at_code"].ToString();
                                sl_code = dt.Rows[i]["sl_code"].ToString();
                                sl_name = dt.Rows[i]["sl_name"].ToString();
                                cc_code = dt.Rows[i]["cc_code"].ToString();
                                debit = dt.Rows[i]["debit"].ToString();
                                credit = dt.Rows[i]["credit"].ToString();
                                invoice = dt.Rows[i]["invoice"].ToString();
                                item_code = dt.Rows[i]["item_code"].ToString();
                                item_desc = dt.Rows[i]["item_desc"].ToString();
                                branch = dt.Rows[i]["branch"].ToString();
                                isreleased = dt.Rows[i]["isreleased"].ToString();

                                chg_code = dt.Rows[i]["chg_code"].ToString();
                                chg_desc = dt.Rows[i]["chg_desc"].ToString();
                                chg_num = dt.Rows[i]["chg_num"].ToString();

                                jo_code = dt.Rows[i]["jo_code"].ToString();
                                purc_ord = dt.Rows[i]["purc_ord"].ToString();
                                inv_num = dt.Rows[i]["inv_num"].ToString();
                                pr_code = dt.Rows[i]["pr_code"].ToString();
                                dr_code = dt.Rows[i]["dr_code"].ToString();


                                //  ,  , , , ,,,,,,,,,,,,,,,,
                                if (ck_num != "")
                                {
                                    col = "fy,mo,t_date,t_desc,user_id ,ck_date,ck_num,sysdate,systime,j_code,j_num,seq_num,at_code,sl_code,sl_name,cc_code,debit,credit,invoice,item_code,item_desc,branch,isreleased,jo_code,purc_ord,inv_num,pr_code,dr_code,seq_desc, chg_code,chg_desc,chg_num";
                                    val = "'" + fy + "','" + mo + "','" + t_date + "'," + db.str_E(t_desc) + ",'" + user_id + "','" + ck_date + "','" + ck_num + "','" + sysdate + "','" + systime + "','" + j_code + "','" + j_num + "','" + seq_num + "','" + at_code + "','" + sl_code + "'," + db.str_E(sl_name) + ",'" + cc_code + "','" + debit + "','" + credit + "','" + invoice + "','" + item_code + "'," + db.str_E(item_desc) + ",'" + branch + "','" + isreleased + "','" + jo_code + "','" + purc_ord + "','" + inv_num + "','" + pr_code + "','" + dr_code + "'," + db.str_E(seq_desc) + ",'" + chg_code + "'," + db.str_E(chg_desc) + ",'" + chg_num + "'";

                                } 
                                else {
                                    col = "fy,mo,t_date,t_desc,user_id ,sysdate,systime,j_code,j_num,seq_num,at_code,sl_code,sl_name,cc_code,debit,credit,invoice,item_code,item_desc,branch, isreleased,jo_code,purc_ord,inv_num,pr_code,dr_code,seq_desc, chg_code,chg_desc,chg_num";
                                    val = "'" + fy + "','" + mo + "','" + t_date + "'," + db.str_E(t_desc) + ",'" + user_id + "','" + sysdate + "','" + systime + "','" + j_code + "','" + j_num + "','" + seq_num + "','" + at_code + "','" + sl_code + "'," + db.str_E(sl_name) + ",'" + cc_code + "','" + debit + "','" + credit + "','" + invoice + "','" + item_code + "'," + db.str_E(item_desc) + ",'" + branch + "','" + isreleased + "','" + jo_code + "','" + purc_ord + "','" + inv_num + "','" + pr_code + "','" + dr_code + "'," + db.str_E(seq_desc) + ",'" + chg_code + "'," + db.str_E(chg_desc) + ",'" + chg_num + "'";
                                }
                                if (db.InsertOnTable("tr04", col, val))
                                {
                                    status = true;
                                }
                            }
                        }
                        if (status)
                        {
                            db.DeleteOnTable("tr01", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                            db.DeleteOnTable("tr02", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                        }
                    }
                }
                catch (Exception er) { MessageBox.Show(er.Message); }

            }

            if (status)
            {
                MessageBox.Show("Successfully post selected entry.");
            }
            clear();
 
        }
        public void clear()
        {
             
            dgv_list.Rows.Clear();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cbo_branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            String mo = "";
            String fy = "";
            String[] p;


            try
            {
                if (cbo_journal.SelectedIndex > -1 && cbo_period.SelectedIndex < 0)
                {
                    //MessageBox.Show("Please select period.");
                }
                else
                {
                    prd = cbo_period.SelectedValue.ToString();
                    jrnl = cbo_journal.SelectedValue.ToString();

                    p = prd.Split('-');
                    mo = p.GetValue(1).ToString();
                    fy = p.GetValue(0).ToString();

                    disp_list_fromdb(fy, mo, jrnl);

                    lbl_period.Text = cbo_period.Text.ToString();
                    lbl_journal.Text = cbo_journal.Text.ToString();
                    lbl_journal_id.Text = jrnl;

                    btn_post.Enabled = true;
                    btn_print.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cbo_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_period.SelectedIndex != -1 && isReady)
            {
                cbo_journal.DroppedDown = true;
            }
        }
    }
}
