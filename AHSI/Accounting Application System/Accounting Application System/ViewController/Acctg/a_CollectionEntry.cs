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
    public partial class a_CollectionEntry : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        thisDatabase db = new thisDatabase();
        GlobalMethod gm = new GlobalMethod();
        GlobalClass gc = new GlobalClass();
        public Boolean check = false;

        String j_code = "", jrnl_name = "";
        String jtype_name = "Collection";

        public a_CollectionEntry()
        {
            InitializeComponent();


            gc.load_branch(cbo_branch);
            gc.load_customer(cbo_customer);
            gc.load_collector(cbo_collector);

            dtp_trnxdate.Value = Convert.ToDateTime(db.get_systemdate(""));
            cbo_cashier.Text = GlobalClass.username;

            String j_type = db.get_colval("m05type", "code", "lower(name)='" + jtype_name.ToLower() + "'");
            gc.load_journal(cbo_ctype, j_type);
            gc.load_journal(cbo_codetype, j_type);
            thisDatabase db2 = new thisDatabase();
            String grp_id2 = "";
            DataTable dt24 = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt24.Rows.Count > 0)
            {
                grp_id2 = dt24.Rows[0]["grp_id"].ToString();
            }
            DataTable dt23 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id2 + "' AND a.mod_id='A4000' ORDER BY b.pla, b.mod_id");

            if (dt23.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt23.Rows[0]["add"].ToString();
                update = dt23.Rows[0]["upd"].ToString();
                delete = dt23.Rows[0]["cancel"].ToString();
                print = dt23.Rows[0]["print"].ToString();

                if (add == "n")
                {
                    btn_new.Enabled = false;
                }
                if (update == "n")
                {
                    btn_upd.Enabled = false;
                }
                if (delete == "n")
                {
                    btn_cancel.Enabled = false;
                }
                if (print == "n")
                {
                    btn_print.Enabled = false;
                }

            }
            disp_dgvlist();
        }

        private void a_CollectionEntry_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            if (cbo_ctype.SelectedIndex != -1)
            {
                isnew = true;
                tpg_info_enable(true);
                frm_clear();
                goto_tbcntrl_info();

                txt_amtdue.Text = "0.00";
                cbo_codetype.SelectedValue = cbo_ctype.SelectedValue.ToString();
                txt_code.Text = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");

                m_customers frm = new m_customers(this, true);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select Journal Type first.");
                cbo_ctype.DroppedDown = true;
            }
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            int i;

            try
            {
                if (dgv_list.Rows.Count > 0 && String.IsNullOrEmpty(dgv_list["dgv_j_num", dgv_list.CurrentRow.Index].Value.ToString()) == false)
                {
                    isnew = false;
                    tpg_info_enable(true);
                    frm_clear();
                    disp_info();
                    goto_tbcntrl_info();
                }

                else
                {
                    MessageBox.Show("No rows selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No rows selected."); }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            int r;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                if (db.UpdateOnTable("soahdr2", "cancel='Y'", "soa_code='" + dgv_list["dgvl_soa_code", r].Value.ToString() + "'"))
                {
                    disp_dgvlist();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                }
                else
                {
                    MessageBox.Show("Failed on deleting.");
                }
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

            try
            {
                if (dgv_list.Rows.Count > 0)
                {
                    int r = dgv_list.CurrentRow.Index;

                    String fy, mo, j_code, j_num, jrnl_name, t_date;
                    String refno, ref_desc, paid_to, ck_no, ck_date, explanation;
                    DateTime dt = gm.toDateValue(dgv_list["dgv_t_date", r].Value.ToString());


                    if (cbo_ctype.SelectedIndex != -1)
                    {
                        j_code = this.j_code;
                        jrnl_name = this.jrnl_name;
                    }
                    else
                    {
                        j_code = dgv_list["dgv_j_code", r].Value.ToString();
                        jrnl_name = db.get_colval("m05", "j_desc", "j_code='" + j_code + "'");
                    }


                    fy = dgv_list["dgv_fy", r].Value.ToString();
                    //j_code = dgv_list["dgv_j_code", r].Value.ToString();
                    //jrnl_name = db.get_colval("m05","j_desc","j_code='"+j_code+"'");
                    j_num = dgv_list["dgv_j_num", r].Value.ToString();
                    mo = Convert.ToInt32(dt.ToString("MM")).ToString("0");

                    DataTable dtl = db.get_journalentry_info(fy ,mo, j_code, j_num);

                    refno = j_num;
                    t_date = gm.toDateString(dtl.Rows[0]["t_date"].ToString(),"");
                    ref_desc = dtl.Rows[0]["t_desc"].ToString();
                    paid_to = dtl.Rows[0]["payee"].ToString();
                    ck_no = dtl.Rows[0]["ck_num"].ToString();
                    ck_date = gm.toDateString(dtl.Rows[0]["ck_date"].ToString(),"");
                    explanation = db.get_explanation(j_code, refno);

                    dtl = db.get_journalentry_info_accntlist(j_code, refno);

                    Report rpt = new Report();
                    rpt.print_voucher(j_code, jrnl_name, refno, ref_desc, fy, mo, t_date, paid_to, ck_date, ck_no, explanation, dtl);
                    rpt.Show();
                }
                else
                {
                    MessageBox.Show("No Collection item selected.");
                }
            }
            catch { MessageBox.Show("No Collection item selected."); }


        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            String code, debt_code, debt_name, trnx_date, user_id, t_date, t_time, coll_code, or_type, or_ref, soa;
            String ln, rmttyp, ln_num, reg_num, full_name, lne_desc, amount, chg_code, chg_num, chg_desc, fol_code, seq, acct_no, sourcetype = "A";
            String rom_code, arr_date, dep_date;
            String col = "", val = "", add_col = "", add_val = "";
            String table = "colhdr";
            String tbl_ln1 = "collne";
            String tbl_ln2 = "collne2";
            Boolean success = false;
            DateTime mydate = DateTime.Parse(db.get_systemdate("yyyy-MM-dd"));
            String ord_code = "", ord_date = "", customer = "", jrnlz = "", out_code = "", fy = "", j_code = "", j_num = "", at_code = "", total_amnt = "", dr_cr = "", t_desc = "", collector = "";

            //if(cbo_collector.SelectedIndex == -1)
            //{
            //    //MessageBox.Show("Please select a collector.");
            //    //cbo_collector.DroppedDown = true;
            //}
            if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
                cbo_customer.DroppedDown = true;
            }
            //else if (String.IsNullOrEmpty(txt_reference.Text) == true)
            //{
            //    //MessageBox.Show("Please type the document/OR reference.");
            //}
            else if (dgv_collection.Rows.Count <= 1)
            {
                MessageBox.Show("No Collection Entry. Please re-enter.");
            }
            else
            {
                j_num = txt_code.Text; 
                j_code = this.j_code;
                debt_code = cbo_customer.SelectedValue.ToString();
                debt_name = cbo_customer.Text;
                trnx_date = dtp_trnxdate.Value.ToString("yyyy-MM-dd");
                user_id = GlobalClass.username;
                t_date = db.get_systemdate("yyyy-MM-dd");
                t_time = DateTime.Now.ToString("HH:mm");
                soa = "";
                or_type = "NA";
                or_ref = txt_reference.Text;
                coll_code = (cbo_collector.SelectedValue ?? "").ToString();
                DataTable dtfy = new DataTable();
                dtfy = db.QueryBySQLCode("SELECT fy from rssys.m99");
                if (dtfy.Rows.Count > 0)
                {
                    fy = dtfy.Rows[0]["fy"].ToString();
                }
                t_desc = txt_reference.Text;
                if (String.IsNullOrEmpty(t_desc))
                {
                    t_desc = db.get_colval("m05", "j_desc", "j_code='" + j_code + "'");
                }

                if (cbo_collector.SelectedIndex != -1)
                {
                    collector = cbo_collector.SelectedValue.ToString();
                }
                
                if (isnew)
                {  //db.get_jtypename(j_code)
                    //code = db.get_pk("col_code");
                    // or_ref = "REF#:" + code;
                    // col = "col_code, soa_code, debt_code, debt_name, trnx_date, or_type, or_ref, coll_code, user_id, t_date, t_time";
                    col = "fy,j_code,j_num,t_date,t_desc,user_id,sysdate, systime,mo,collectorid";
                    val = "'" + fy + "', '" + j_code + "', '" + j_num + "', '" + t_date + "', '" + t_desc + "', '" + user_id + "', '" + t_date + "', '" + t_time + "', '" + mydate.ToString("MM") + "','" + collector + "'";

                    if (db.InsertOnTable("tr01", col, val))
                    {
                        //db.set_pkm99("col_code", db.get_nextincrementlimitchar(code, 8));
                        db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");
                        db.UpdateOnTable("tr01", "branch='" + (cbo_branch.SelectedValue ?? "").ToString() + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                        save_collection_data(j_code, j_num);
                        success = true;

                    }
                    else
                    {
                        db.DeleteOnTable("tr01", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "fy='" + fy + "', j_code='" + j_code + "', j_num='" + j_num + "', t_date='" + t_date + "', t_desc='" + t_desc + "', user_id='" + user_id + "', sysdate='" + t_date + "', systime='" + t_time + "', mo='" + mydate.ToString("MM") + "', collectorid='" + collector + "'";

                    if (db.UpdateOnTable("tr01", col, "j_num='" + j_num + "' AND j_code='" + j_code + "'"))
                    {
                        db.UpdateOnTable("tr01", "branch='" + (cbo_branch.SelectedValue ?? "").ToString() + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                        save_collection_data(j_code, j_num);
                        success = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
                    }
                }

                if (success)
                {
                    disp_dgvlist();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                    frm_clear();
                }
            }
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void btn_addsoa_Click(object sender, EventArgs e)
        {
            z_add_soa frm = new z_add_soa(this);

            frm.ShowDialog();
        }

        private void btn_remove1_Click(object sender, EventArgs e)
        {

        }

        private void btn_addcollection_Click(object sender, EventArgs e)
        {
            z_add_collection frm = new z_add_collection(this, true);

            frm.ShowDialog();
        }

        private void btn_remove2_Click(object sender, EventArgs e)
        {
            if (dgv_collection.Rows.Count > 0)
            {
                int r = dgv_collection.CurrentRow.Index;

                dgv_collection.Rows.RemoveAt(r);

                disp_col_amnt();
            }
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {

            txt_code.Text = "";
            txt_reference.Text = "";
            cbo_customer.SelectedIndex = -1;
            cbo_collector.SelectedIndex = -1;
            dgv_collection.Rows.Clear();
            dtp_trnxdate.Value = Convert.ToDateTime(db.get_systemdate(""));

            cbo_branch.SelectedValue = GlobalClass.branch;

        }

        private void goto_tbcntrl_info()
        {
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_info;
            tbcntrl_option.SelectedTab = tpg_opt_2;

            tpg_info.Show();
            tpg_opt_2.Show();

            seltbp = false;
        }

        private void goto_tbcntrl_list()
        {
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_list;
            tbcntrl_option.SelectedTab = tpg_opt_1;

            tpg_list.Show();
            tpg_opt_1.Show();

            seltbp = false;
        }

        private void tpg_info_enable(Boolean flag)
        {
            txt_code.Enabled = flag;
        }

        private void clear_dgv()
        {
            try
            {
                dgv_list.Rows.Clear();
            }
            catch (Exception)
            { }
        }

        private void disp_info()
        {
            String code = "";
            int r = 0;
            DataTable dt, dt2;

            //try
            // {
            r = dgv_list.CurrentRow.Index;
            code = dgv_list["dgv_j_num", r].Value.ToString();
            txt_code.Text = code;
            dtp_trnxdate.Value = Convert.ToDateTime(dgv_list["dgv_t_date", r].Value.ToString());
            cbo_customer.SelectedValue = dgv_list["dgv_sl_code", r].Value.ToString();
            cbo_codetype.SelectedValue = dgv_list["dgv_j_code", r].Value.ToString();
            txt_reference.Text = dgv_list["dgv_t_desc", r].Value.ToString();
            cbo_collector.SelectedValue = dgv_list["dgv_collectorid", r].Value.ToString();
            cbo_branch.SelectedValue = dgv_list["dgv_branchid", r].Value.ToString();
            
            // cbo_collector.SelectedValue = dgv_list["dgv_coll_code", r].Value.ToString();

            // txt_reference.Text = dgv_list["dgvl_or_ref", r].Value.ToString();

            // dt = db.get_collne_list(code);

            try
            {
                //dt2 = db.get_collne2_list(code);
                dt2 = db.QueryBySQLCode("SELECT tr2.*,tr1.ck_num,tr1.ck_date FROM rssys.tr02 tr2 LEFT JOIN rssys.tr01 tr1 ON (tr2.j_num = tr1.j_num  AND tr2.j_code = tr1.j_code) WHERE tr2.j_num='" + code + "' AND tr2.j_code='" + this.j_code + "'");

                //SELECT DISTINCT tr1.*, tr2.sl_code, tr2.sl_name  FROM rssys.tr01 tr1 LEFT JOIN rssys.tr02 tr2 ON tr1.j_num=tr2.j_num WHERE  COALESCE(sl_code,'')<>'' AND tr1.sysdate BETWEEN '" + dateFrom + "' AND '" + dateTo + "' " + WHERE + " ORDER BY j_num ASC


                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    int d = dgv_collection.Rows.Add();
                    DataGridViewRow row = dgv_collection.Rows[d];

                    row.Cells["dgvl2_ln"].Value = dt2.Rows[j]["seq_num"].ToString();
                    row.Cells["dgvl2_paymentcode"].Value = dt2.Rows[j]["item_code"].ToString();
                    row.Cells["dgvl2_paymentdesc"].Value = dt2.Rows[j]["item_desc"].ToString();
                    if (dt2.Rows[j]["credit"].ToString() != "0.00")
                    {
                        row.Cells["dgvl2_amount"].Value = dt2.Rows[j]["credit"].ToString();
                    }
                    else
                    {
                        row.Cells["dgvl2_amount"].Value = dt2.Rows[j]["debit"].ToString();
                    }
                    row.Cells["dgvl_invoice"].Value = dt2.Rows[j]["invoice"].ToString();
                    if (dt2.Rows[j]["item_desc"].ToString() == "CHECK")
                    { 
                        row.Cells["dgvl2_chknumber"].Value = dt2.Rows[j]["ck_num"].ToString();
                        row.Cells["dgvl2_chkdate"].Value = gm.toDateString(dt2.Rows[j]["ck_date"].ToString(), "");
                    }

                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            disp_col_amnt();
            verifyCheck();
            // }
            // catch (Exception) { } 
        }

        private void disp_dgvlist()
        {
            DataTable dt;
            String search = txt_search.Text;
            var dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
            var dateTo = dtp_to.Value.ToString("yyyy-MM-dd");
            String searchtype = "";

            /*
            if (cbo_searchby.SelectedIndex == 0)
            {
                searchtype = "col_code";
            }
            else if (cbo_searchby.SelectedIndex == 1)
            {
                searchtype = "debt_name";
            }*/

            try { dgv_list.Rows.Clear(); }
            catch { }

            try
            {
                String WHERE = "";

                if (cbo_ctype.SelectedIndex != -1)
                {
                    WHERE = " AND tr1.j_code='" + j_code + "' ";
                }
                else
                {
                    String j_type = db.get_colval("m05type", "code", "lower(name)='" + jtype_name.ToLower() + "'");
                    WHERE = " AND tr1.j_code IN (SELECT m5.j_code FROM rssys.m05 m5 WHERE m5.j_type='" + j_type + "') ";
                }

                dt = db.QueryBySQLCode("SELECT DISTINCT tr1.*, tr2.sl_code, tr2.sl_name  FROM rssys.tr01 tr1 LEFT JOIN rssys.tr02 tr2 ON tr1.j_num=tr2.j_num WHERE  COALESCE(tr2.sl_code,'')<>'' AND tr1.sysdate BETWEEN '" + dateFrom + "' AND '" + dateTo + "' " + WHERE + " ORDER BY j_num ASC");

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["dgv_fy"].Value = dt.Rows[r]["fy"].ToString();
                    row.Cells["dgv_j_num"].Value = dt.Rows[r]["j_num"].ToString();
                    row.Cells["dgv_j_code"].Value = dt.Rows[r]["j_code"].ToString();
                    row.Cells["dgv_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(), "MM/dd/yyyy");
                    row.Cells["dgv_t_desc"].Value = dt.Rows[r]["t_desc"].ToString();
                    row.Cells["dgv_user_id"].Value = dt.Rows[r]["user_id"].ToString();
                    row.Cells["dgv_sl_code"].Value = dt.Rows[r]["sl_code"].ToString();
                    row.Cells["dgv_sl_name"].Value = dt.Rows[r]["sl_name"].ToString();
                    cbo_codetype.SelectedValue = dt.Rows[r]["j_code"].ToString();
                    row.Cells["dgv_ctype_desc"].Value = cbo_codetype.Text;

                    row.Cells["dgv_collectorid"].Value = dt.Rows[r]["collectorid"].ToString();
                    cbo_collector.SelectedValue = dt.Rows[r]["collectorid"].ToString();
                    row.Cells["dgv_collector"].Value = cbo_collector.Text;
                    row.Cells["dgv_branchid"].Value = dt.Rows[r]["branch"].ToString();
                    cbo_branch.SelectedValue = dt.Rows[r]["branch"].ToString();
                    row.Cells["dgv_branch"].Value = cbo_branch.Text;
                    //
                }

            }
            catch { }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "";

            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "dgvl_ctr_code";
                }
                else
                {
                    typname = "dgvl_or_code";
                }

                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                        .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                        .First();

                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;

                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[10];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[1];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[2];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[3];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[4];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[5];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[6];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[7];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[8];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[9];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[0];

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
        }

        public void add_collection(String ln, String paymentdesc, Double amt, String paymentcode, String chkdate, String chknumber, Boolean ischeck, Boolean isnewline, String soa_code, String payment_desc)
        {
            DataTable dt = null;

            try
            {
                if (isnewline)
                {
                    int i = dgv_collection.Rows.Add();
                    DataGridViewRow row = dgv_collection.Rows[i];

                    row.Cells["dgvl2_ln"].Value = ln;
                    row.Cells["dgvl2_paymentdesc"].Value = paymentdesc;
                    row.Cells["dgvl2_amount"].Value = gm.toAccountingFormat(amt.ToString("0.00"));
                    row.Cells["dgvl2_paymentcode"].Value = paymentcode;
                    row.Cells["dgvl2_paymentdesc"].Value = payment_desc;
                    row.Cells["dgvl_invoice"].Value = soa_code;

                    //if (ischeck)
                    //{
                    if (chknumber != "")
                    {
                        row.Cells["dgvl2_chkdate"].Value = gm.toDateString(chkdate, "");
                    }
                    else
                    {
                        row.Cells["dgvl2_chkdate"].Value = gm.toDateString("", "");
                    }
                    row.Cells["dgvl2_chknumber"].Value = chknumber;
                    //}
                    //else
                    //{
                    //    row.Cells["dgvl2_chkdate"].Value = "";
                    //    row.Cells["dgvl2_chknumber"].Value = "";
                    //}
                }
                else
                {
                    int r = dgv_collection.CurrentRow.Index;

                    dgv_collection["dgvl2_ln", r].Value = ln;
                    dgv_collection["dgvl2_paymentdesc", r].Value = payment_desc;
                    dgv_collection["dgvl2_amount", r].Value = gm.toAccountingFormat(amt.ToString("0.00"));
                    dgv_collection["dgvl2_paymentcode", r].Value = paymentcode;
                    dgv_collection["dgvl_invoice", r].Value = soa_code;

                    //if (ischeck)
                    //{
                    if (chknumber != "")
                    {
                        dgv_collection["dgvl2_chkdate", r].Value = gm.toDateString(chkdate, "");
                    }
                    else
                    {
                        dgv_collection["dgvl2_chkdate", r].Value = gm.toDateString("", "");
                    }
                    dgv_collection["dgvl2_chknumber", r].Value = chknumber;
                    //}
                    //else
                    //{
                    //    dgv_collection["dgvl2_chkdate", r].Value = "";
                    //    dgv_collection["dgvl2_chknumber", r].Value = "";
                    //}
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            disp_col_amnt();
            verifyCheck();
        }
        public void verifyCheck()
        {
            this.check = false;
            for (int i = 0; i < dgv_collection.Rows.Count - 1; i++)
            {
                if (dgv_collection["dgvl2_paymentdesc", i].Value.ToString() == "CHECK")
                {
                    this.check = true;
                    break;
                }
            }
        }
        private void disp_col_amnt()
        {
            Double bal = 0.00, run_bal = 0.00;

            try
            {
                for (int i = 0; i < dgv_collection.Rows.Count - 1; i++)
                {
                    bal = gm.toNormalDoubleFormat(dgv_collection["dgvl2_amount", i].Value.ToString());
                    run_bal += bal;
                }
                txt_amtdue.Text = gm.toAccountingFormat(run_bal.ToString());
            }
            catch (Exception) { }

            // lbl_colamnt.Text = gm.toAccountingFormat(run_bal);
        }

        private void btn_updcollection_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_collection.Rows.Count > 0)
                {
                    int r = dgv_collection.CurrentRow.Index;

                    z_add_collection frm = new z_add_collection(this, false);

                    frm.set_data(dgv_collection["dgvl2_ln", r].Value.ToString(), dgv_collection["dgvl2_paymentcode", r].Value.ToString(), dgv_collection["dgvl2_amount", r].Value.ToString(), "", "", dgv_collection["dgvl_invoice", r].Value.ToString());

                    frm.ShowDialog();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void save_collection_data(String j_code, String j_num)
        {
            String ln_num, invoice, t_desc, rep_code, amnt_paid, fol_code, reg_num, full_name, rmrttyyp, soa_code = "", payment_desc = "", at_code = "", subledger = "", dr_cr = "";
            String type, chk_num = "", chk_date = "", amount;
            String rom_code, arr_date, dep_date;
            String col = "", val = "", add_col = "", add_val = "";
            String tbl_ln1 = "collne";
            String tbl_ln2 = "collne2";

            Boolean success = false;
            String chg_dtfrm = "", chg_dtto = "";

            if (isnew == false)
            {
                //db.DeleteOnTable(tbl_ln1, "or_code='" + code + "'");
                db.DeleteOnTable("tr02", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
            }


            //col = "or_code, ln_num, type, amount, soa_code, payment_desc";
            col = "j_code ,j_num ,seq_num ,at_code ,sl_code ,sl_name  , debit, credit , invoice, item_code,item_desc, pay_code";

            try
            {
                for (int j = 0; j < dgv_collection.Rows.Count - 1; j++)
                {
                    ln_num = dgv_collection["dgvl2_ln", j].Value.ToString();
                    type = dgv_collection["dgvl2_paymentcode", j].Value.ToString();
                    soa_code = dgv_collection["dgvl_invoice", j].Value.ToString();
                    payment_desc = dgv_collection["dgvl2_paymentdesc", j].Value.ToString();
                    amount = gm.toNormalDoubleFormat(dgv_collection["dgvl2_amount", j].Value.ToString()).ToString();
                    DataTable atdt, dt = new DataTable();
                    atdt = db.QueryBySQLCode("SELECT at_code FROM rssys.m06 WHERE d_code='" + cbo_customer.SelectedValue.ToString() + "'");

                    chk_num = (dgv_collection["dgvl2_chknumber", j].Value ?? "").ToString();
                    chk_date = db.get_systemdate("");
                    try { chk_date = DateTime.Parse((dgv_collection["dgvl2_chkdate", j].Value ?? "").ToString()).ToString("yyyy-MM-dd"); }
                    catch { }
                    if (!String.IsNullOrEmpty(chk_num))
                    {
                        db.UpdateOnTable("tr01", "ck_date='" + Convert.ToDateTime(chk_date).ToString("yyyy-MM-dd") + "', ck_num='" + chk_num + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                    }


                    if (atdt.Rows.Count > 0)
                    {
                        at_code = atdt.Rows[0]["at_code"].ToString();
                    }
                    else
                    {
                        at_code = "";
                    }

                    dt = db.QueryBySQLCode("SELECT dr_cr from rssys.m04 WHERE at_code='" + at_code + "'");
                    if (dt.Rows.Count > 0)
                    {
                        dr_cr = dt.Rows[0]["dr_cr"].ToString();
                    }

                    String credit = "0.00";
                    String debit = "0.00";
                    if (!string.IsNullOrEmpty(amount))
                    {
                        if (dr_cr == "D")
                        {

                            debit = amount;
                        }
                        else
                        {

                            credit = amount;
                        }
                    }

                    //if (String.IsNullOrEmpty(dgv_collection["dgvl2_chknumber", j].Value.ToString()) == false)
                    //{
                    //    chk_num = dgv_collection["dgvl2_chknumber", j].Value.ToString();
                    //    chk_date = gm.toDateString(dgv_collection["dgvl2_chkdate", j].Value.ToString(), "");
                    //    add_col = ", chk_num, chk_date";
                    //    add_val = ", '" + chk_num + "', '" + chk_date + "'";
                    // } 
                    val = "'" + j_code + "', '" + j_num + "', '" + ln_num + "', '" + at_code + "', '" + cbo_customer.SelectedValue.ToString() + "','" + cbo_customer.Text + "','" + debit + "','" + credit + "','" + soa_code + "','" + type + "','" + payment_desc + "', '" + type + "'";

                    if (db.InsertOnTable("tr02", col, val))
                    {
                        // MessageBox.Show("Record Succesfully In tr02");
                        // db.UpdateOnTable("tr01", "ck_num='"+chk_num+"' AND ck_date='"+chk_date+"'", "j_num='" + j_num + "'");
                        //db.InsertOnTable(tbl_ln2, col + "" + add_col, val + "" + add_val);
                    }

                    //else {
                    //    val = "'" + j_code + "', '" + j_num + "', '" + ln_num + "', '" + at_code + "', '" + cbo_customer.SelectedValue.ToString() + "','" + cbo_customer.Text + "','" + debit + "','" + credit + "','" + soa_code + "','" + type + "','" + payment_desc + "'";

                    //    if (db.InsertOnTable("tr02", col, val))
                    //    {
                    //        //db.UpdateOnTable("tr01", "ck_num='' AND ck_date=''", "j_num='" + j_num + "'");
                    //        MessageBox.Show("Record Succesfully In tr02");
                    //        //db.InsertOnTable(tbl_ln2, col + "" + add_col, val + "" + add_val);
                    //    }
                    //}

                }
                //if (chk_num != "" && chk_date != "")
                //{
                    //db.UpdateOnTable("tr01", "ck_num='" + chk_num + "' , ck_date='" + chk_date + "'", "j_num='" + j_num + "'");
                //}
            }
            catch (Exception er) { MessageBox.Show(er.Message + "Item Error"); }
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            m_customers frm = new m_customers(this, true);
            frm.ShowDialog();
        }
        public void set_custvalue_frm(String custcode)
        {
            try { cbo_customer.Items.Clear(); }
            catch { }

            gc.load_customer(cbo_customer);
            cbo_customer.SelectedValue = custcode;

        }
        public void accnt_link(String cust)
        {
            String val = "";
            DataTable dt = null;
            dt = db.QueryBySQLCode("SELECT at_code FROM rssys.m06 WHERE d_code='" + cust + "'");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    val = dt.Rows[i]["at_code"].ToString();

                }

            }
        }



        private void cbo_customer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbo_customer_SelectedValueChanged(object sender, EventArgs e)
        {
            //String val = "";
            //DataTable dt = null;

            //try
            //{
            //    dt = db.QueryBySQLCode("SELECT at_code FROM rssys.m06 WHERE d_code='" + cbo_customer.SelectedValue.ToString() + "'");
            //    MessageBox.Show(cbo_customer.Text);
            //}
            //catch { }
            ////MessageBox.Show(dt.Rows.Count.ToString());
            //try
            //{
            //    if (dt.Rows.Count != 0)
            //    {
            //        cbo_at_code_sl.SelectedValue = dt.Rows[0]["at_code"].ToString();

            //    }
            //}
            //catch { }
        }

        private void cbo_customer_TextChanged(object sender, EventArgs e)
        {
            String val = "";
            DataTable dt = null;

            try
            {
                dt = db.QueryBySQLCode("SELECT at_code FROM rssys.m06 WHERE d_code='" + cbo_customer.SelectedValue.ToString() + "'");

            }
            catch { }
            //MessageBox.Show(dt.Rows.Count.ToString());
            try
            {
                if (dt.Rows.Count != 0)
                {

                }
            }
            catch { }
        }

        private void btn_removecollection_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            String qty = "";

            try
            {
                if (dgv_collection.Rows.Count > 0)
                {
                    r = dgv_collection.CurrentRow.Index;

                    if (isnew == false)
                    {

                    }

                    dgv_collection.Rows.RemoveAt(r);

                    disp_col_amnt();
                    verifyCheck();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void dgv_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.CellStyle.SelectionForeColor = Color.Brown;
            e.CellStyle.SelectionBackColor = Color.GreenYellow;

            if (e.RowIndex == -1)
            {
                SolidBrush br = new SolidBrush(Color.Gray);
                e.Graphics.FillRectangle(br, e.CellBounds);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
            else
            {
                if (e.RowIndex % 2 == 0)
                {
                    SolidBrush br = new SolidBrush(Color.Gainsboro);

                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
                else
                {
                    SolidBrush br = new SolidBrush(Color.White);
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
            }
        }

        private void dgv_collection_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.CellStyle.SelectionForeColor = Color.Brown;
            e.CellStyle.SelectionBackColor = Color.GreenYellow;

            if (e.RowIndex == -1)
            {
                SolidBrush br = new SolidBrush(Color.Gray);
                e.Graphics.FillRectangle(br, e.CellBounds);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
            else
            {
                if (e.RowIndex % 2 == 0)
                {
                    SolidBrush br = new SolidBrush(Color.Gainsboro);

                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
                else
                {
                    SolidBrush br = new SolidBrush(Color.White);
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
            }
        }

        private void cbo_codetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_codetype.SelectedIndex != -1)
            {
                j_code = cbo_codetype.SelectedValue.ToString();
                jrnl_name = db.get_colval("m05", "j_desc", "j_code='" + j_code + "'");
            }
        }

        private void cbo_ctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_ctype.SelectedIndex != -1)
            {
                j_code = cbo_ctype.SelectedValue.ToString();
                jrnl_name = db.get_colval("m05", "j_desc", "j_code='" + j_code + "'");
                disp_dgvlist();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            disp_dgvlist();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            disp_dgvlist();
        }


    }
}

