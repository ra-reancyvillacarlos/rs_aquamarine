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
    public partial class periodClosing : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();

        String fy;
        public periodClosing()
        {
            InitializeComponent();
            cbo_fy.Text = DateTime.Now.ToString("yyyy");
        }

        private void j_hotel_Load(object sender, EventArgs e)
        {
            get_cbo_dt();
        }
        private void get_cbo_dt()
        {
            DataTable dt_cb = new DataTable();
            dt_cb = db.QueryBySQLCode("SELECT DISTINCT fy FROM rssys.x03");

            cbo_fy.DataSource = dt_cb;
            cbo_fy.ValueMember = "fy";
            cbo_fy.DisplayMember = "fy";

            try
            {
                cbo_fy.SelectedIndex = -1;
            }
            catch { }
        }


        private void btn_proceed_Click(object sender, EventArgs e)
        {
            fy = cbo_fy.SelectedValue.ToString();

            if (String.IsNullOrEmpty(fy))
            {
                // changed by: Reancy 06 01 2018
                fy = db.get_colval("x03", "fy", "fy='" + cbo_fy.SelectedValue.ToString() + "'");
            }

            if (!String.IsNullOrEmpty(fy))
            {
                if (MessageBox.Show("Are you sure you want to close this Financial Year?\nNote:After the closing period,New Beginning Balance of next Financial Year.", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bgWorker.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show("No Journal Entry at this Financial Year.");
            }
            
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void input_enable(Boolean bol)
        {
            cbo_fy.Invoke(new Action(() => {
                cbo_fy.Enabled = bol;
            }));
            btn_close.Invoke(new Action(() =>
            {
                btn_close.Enabled = bol;
            }));
            btn_proceed.Invoke(new Action(() =>
            {
                btn_proceed.Enabled = bol;
            }));
        }

        private void inc_pbar(int i)
        {
            try
            {
                pbar.Invoke(new Action(() =>
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
                }));
            }
            catch (Exception) { reset_pbar(); }
        }
        private void inc_pbar100()
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    pbar.Value = 100;
                }));
            }
            catch (Exception) { }
        }

        private void reset_pbar()
        {
            pbar.Invoke(new Action(() =>
            {
                pbar.Value = 0;
            }));
        }

        public Boolean post_to_ledger(String fy, String j_code_m, String j_num_m)
        {
            Boolean p_to_l = db.UpdateOnTable("tr04", "beg_j_code = '" + j_code_m + "' AND beg_j_num = '" + j_num_m + "'", "fy = '" + fy + "'");
            if (p_to_l == true)
            {
                return cont_pro(fy, j_code_m, j_num_m);
            }
            else
            {
                return cont_pro(fy, j_code_m, j_num_m);
            }
        }

        private Boolean cont_pro(String fy, String j_code_m, String j_num_m)
        {
            Boolean st = false;
            Boolean stat = db.QueryBySQLCode_bool("INSERT INTO rssys.tr04 (fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, prj_code, debit, credit, invoice, item_code, item_desc, unit, recv_qty, price, user_id, \"sysdate\", systime, clrd, seq_desc, post_num, relsd, rep_code, pay_code, or_code, or_lne, scc_code, chg_code, chg_desc, chg_num, jo_code, pr_code, purc_ord, inv_num, dr_code, vp_num, branch, isreleased, beg_j_code, beg_j_num) SELECT t1.fy,t1.mo, t1.j_code, t1.j_num, t1.t_date,t1.t_desc, t1.payee, t1.ck_num,t1.ck_date, t2.seq_num, t2.at_code, m.at_desc, t2.sl_code, t2.sl_name, t2.cc_code, t2.prj_code, t2.debit, t2.credit, t2.invoice, t2.item_code, t2.item_desc, t2.unit, t2.recv_qty, t2.price, t1.user_id, t1.sysdate,t1.systime, t2.clrd, t2.seq_desc, '', t1.relsd, t2.rep_code, t2.pay_code, t2.or_code, t2.or_lne, t2.scc_code, t2.chg_code, t2.chg_num, t1.jo_code, t1.pr_code, t1.purc_ord, t1.inv_num, t1.pr_code, t1.dr_code, t1.vp_num, t1.branch, t2.isreleased, COALESCE('" + j_code_m + "', ''), COALESCE('" + j_num_m + "', '') FROM rssys.tr02 t2 LEFT JOIN rssys.tr01 t1 ON (t1.j_num=t2.j_num AND t1.j_code=t2.j_code) LEFT JOIN rssys.m04 m ON t2.at_code = m.at_code WHERE  fy='" + fy + "';");

            if (stat == true)
            {
                //incorrect query and removed by: Reancy 06 01 2018
                //if (db.isExists("tr04", "fy='" + fy + "'"))
                //{
                //db.QueryBySQLCode("DELETE FROM rssys.tr01 WHERE j_num IN (SELECT DISTINCT j_num FROM rssys.tr04 WHERE fy = '" + fy + "') AND j_code IN (SELECT DISTINCT j_code FROM rssys.tr04 WHERE fy = '" + fy + "');");
                //db.QueryBySQLCode("DELETE FROM rssys.tr02 WHERE j_num IN (SELECT DISTINCT j_num FROM rssys.tr04 WHERE fy = '" + fy + "') AND j_code IN (SELECT DISTINCT j_code FROM rssys.tr04 WHERE fy = '" + fy + "');");

                DataTable dt_an = new DataTable();
                dt_an = db.QueryBySQLCode("SELECT j_code, j_num, seq_num FROM rssys.tr04 WHERE fy = '" + fy + "'");
                foreach (DataRow row in dt_an.Rows)
                {
                    db.QueryBySQLCode("DELETE FROM rssys.tr02 WHERE j_num = '" + row["j_num"].ToString() + "' AND j_code = '" + row["j_code"].ToString() + "' AND seq_num = '" + row["seq_num"].ToString() + "'");
                    db.QueryBySQLCode("DELETE FROM rssys.tr01 WHERE j_num = '" + row["j_num"].ToString() + "' AND j_code = '" + row["j_code"].ToString() + "' AND fy = '" + fy + "'");
                }
                st = true;
                //}
            }
            return st;
        }


        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            input_enable(false);
            Boolean success = false;
            Boolean up_succ = false;
            DataTable dtl;
            String WHERE = "", j_code,j_num;
            String seq_num, at_code, sl_code, sl_name, cc_code, debit, credit, invoice, seq_desc, rep_code, pay_code;

            Double dfy = (gm.toNormalDoubleFormat(fy) + 1);
            String mo = db.get_colval("x03", "mo", "fy='" + dfy + "' AND mo='0'");
            try
            {
                inc_pbar(10);


                j_code = db.get_colval("m05", "j_code", "j_type='" + db.get_colval("m05type", "code", "name='General'") + "'");
                j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");

                Boolean st = post_to_ledger(fy, j_code, j_num);

                if (st == true)
                {
                    inc_pbar(10);

                    if (String.IsNullOrEmpty(mo))
                    {
                        mo = "0";
                        DateTime dt = DateTime.Parse(fy + "-10-01");

                        db.InsertOnTable("x03", "fy, mo, month_desc, \"from\", \"to\"", "'" + dfy.ToString("0") + "','" + mo + "','" + db.get_colval("x04", "month_desc", "mo='" + mo + "'") + "','" + dt.ToString("yyyy-MM-dd") + "','" + dt.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + "'");
                    }

                    //Close all period
                    db.UpdateOnTable("x03", "closed='Y'", "fy='" + fy + "'");

                    inc_pbar(15);

                    // INCORRECT QUERY! KASAPOT KANI DIAY ERROR HAHAHA removed by: Reancy 06 01 2018
                    //dtl = db.QueryBySQLCode("SELECT * FROM rssys.tr04 WHERE fy='" + fy + "'");
                    //if (dtl != null)
                    //{
                    //    for (int i = 0; i < dtl.Rows.Count; i++)
                    //    {
                    //        db.UpdateOnTable("tr04", "closed='Y'", "fy='" + fy + "'");
                    //        inc_pbar(1);
                    //    }
                    //}





                    String dtQry, pQry, p2Qry, trQry = "(SELECT fy, mo, j_code, j_num, t_date, t_desc, payee, ck_num, ck_date, sysdate, systime, seq_num, at_code, at_desc, sl_code, sl_name, cc_code, debit, credit, invoice, user_id, branch, ('')::text as cancel FROM rssys.tr04)";

                    dtQry = "(SELECT COALESCE((sx3.begin)::date,(sx3.from)::date, current_date) AS begin,COALESCE((sx3.from)::date,current_date) AS from, COALESCE((sx3.to)::date,current_date) AS to, sx3._from, sx3._to  FROM ( SELECT string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS begin, string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN to_Char(x3.from,'yyyy-MM-dd')  ELSE null  END,'') AS from, string_agg(CASE WHEN fy||'-'||mo='" + fy + "-12' THEN to_Char(x3.to,'yyyy-MM-dd') ELSE null END,'') AS to, string_agg(CASE WHEN fy||'-'||mo='" + fy + "-0' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _from, string_agg(CASE WHEN fy||'-'||mo='" + fy + "-12' THEN fy||'-'||LPAD(mo::text,3,'0') ELSE null END,'') AS _to FROM rssys.x03 x3) sx3)";

                    pQry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (x3.from BETWEEN sx3.from AND sx3.to) AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";
                    //modified by: Reancy 06 01 2018
                    p2Qry = "(SELECT * FROM " + trQry + " t4 JOIN (SELECT * FROM rssys.x03 x3, " + dtQry + " sx3  WHERE (sx3.begin <= x3.from AND x3.from < sx3.from) AND x3.fy||'-'||LPAD(x3.mo::text,3,'0') BETWEEN sx3._from AND sx3._to) x3 ON (t4.fy=x3.fy AND t4.mo=x3.mo))";

                    WHERE = " AND branch='" + GlobalClass.branch + "'";

                    dtl = db.QueryBySQLCode("SELECT t4.sl_code, m4.at_code, m4.at_desc, m4.dr_cr, COALESCE(t4.debit,0.00) AS debit, COALESCE(t4.credit,0.00) AS credit, COALESCE(bt4.bal_begin,0.0) AS bal_begin, COALESCE((COALESCE(t4.debit,0.00) + (-1*COALESCE(t4.credit,0.00)) + COALESCE(bt4.bal_begin,0.0)),0.00) AS bal_end FROM rssys.m04 m4 LEFT JOIN (SELECT DISTINCT sl_code, at_code, SUM(debit) AS debit, SUM(credit) AS credit FROM " + pQry + " t4 WHERE 1=1 " + WHERE + "  GROUP BY at_code, sl_code) AS t4 ON t4.at_code=m4.at_code LEFT JOIN (SELECT DISTINCT t4.at_code, t4.sl_code, SUM(CASE WHEN m4.dr_cr='D' THEN debit-credit WHEN m4.dr_cr='C' THEN -1 * (credit-debit) ELSE 0.0 END) AS bal_begin FROM " + p2Qry + " t4 LEFT JOIN rssys.m04 m4 ON (t4.at_code=m4.at_code) WHERE 1=1 " + WHERE + " GROUP BY t4.at_code, m4.dr_cr, t4.sl_code ) as bt4 ON bt4.at_code=m4.at_code WHERE (lower(m4.at_desc) not like '%net income (loss)%') AND (COALESCE((COALESCE(t4.debit,0.00) + (-1*COALESCE(t4.credit,0.00)) + COALESCE(bt4.bal_begin,0.0)),0.00)<>0) Order By m4.at_code");

                    if (dtl != null)
                    {
                        if (dtl.Rows.Count != 0)
                        {
                            //j_code = "GJ";
                            //j_code = db.get_colval("m05", "j_code", "j_type='" + db.get_colval("m05type", "code", "name='General'") + "'");
                            //j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");

                            Double amount = 0;

                            if (db.add_jrnl(dfy.ToString(), mo, j_code, j_num, "Beginning Balance of " + dfy.ToString("0"), "", "", null, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd")))
                            {
                                db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");

                                for (int i = 0; i < dtl.Rows.Count; i++)
                                {

                                    seq_num = (i + 1).ToString();
                                    at_code = dtl.Rows[i]["at_code"].ToString();
                                    sl_code = dtl.Rows[i]["sl_code"].ToString();
                                    sl_name = "";
                                    invoice = "";
                                    debit = "0.00"; credit = "0.00";
                                    //amount = gm.toNormalDoubleFormat(dtl.Rows[i]["amount"].ToString());

                                    Boolean p_to_l = db.UpdateOnTable("tr04", "beg_seq_num = '" + seq_num + "'", "at_code = '" + at_code + "' AND sl_code = '" + sl_code + "'");

                                    if (p_to_l == true)
                                    {
                                        if (!String.IsNullOrEmpty(sl_code))
                                        {
                                            sl_name = db.get_colval("m06", "d_name", "d_code='" + sl_code + "'");
                                            if (String.IsNullOrEmpty(sl_name))
                                            {
                                                sl_name = db.get_colval("m07", "c_name", "c_code='" + sl_code + "'");
                                            }
                                        }
                                        if (!String.IsNullOrEmpty(sl_name))
                                        {
                                            invoice = fy + "BEGBAL" + Convert.ToInt32(j_num).ToString("0").PadLeft(2, '0') + seq_num.PadLeft(3, '0');
                                        }
                                        else
                                        {
                                            sl_code = "";
                                        }


                                        if (dtl.Rows[i]["dr_cr"].ToString() == "D")
                                        {
                                            amount = gm.toNormalDoubleFormat(dtl.Rows[i]["debit"].ToString()) - gm.toNormalDoubleFormat(dtl.Rows[i]["credit"].ToString());

                                            if (amount < 0)
                                            {
                                                credit = (amount * -1).ToString("0.00");
                                            }
                                            else
                                            {
                                                debit = amount.ToString("0.00");
                                            }
                                        }
                                        else
                                        {
                                            amount = gm.toNormalDoubleFormat(dtl.Rows[i]["credit"].ToString()) - gm.toNormalDoubleFormat(dtl.Rows[i]["debit"].ToString());

                                            if (amount < 0)
                                            {
                                                debit = (amount * -1).ToString("0.00");
                                            }
                                            else
                                            {
                                                credit = amount.ToString("0.00");
                                            }
                                        }

                                        db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, "", null, debit, credit, invoice, "", "", "", null, null);

                                        inc_pbar(5);
                                    }

                                    db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");

                                    inc_pbar100();
                                }
                            }
                        }

                    }
                    success = true;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (success)
            {
                MessageBox.Show("All transaction successfully closed.\nCheck in Trial Balance.");
            }
            else
            {
                MessageBox.Show("Occured closing financial year");
            }


            inc_pbar(5);

            reset_pbar();

            input_enable(true);
        }

        public String cbo_get_value(ComboBox cbo)
        {
            String val = "";
            cbo.Invoke(new Action(() => { val = cbo.SelectedValue.ToString(); }));
            return val;
        }
        public int cbo_get_index(ComboBox cbo)
        {
            int indx = -1;
            cbo.Invoke(new Action(() => { indx = cbo.SelectedIndex; }));
            return indx;
        }
        
    }
}
