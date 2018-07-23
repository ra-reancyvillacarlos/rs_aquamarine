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
    public partial class journalize : Form
    {
        Report rpt;
        private String j_code = "", j_text = "";
        private String dtfrm = "";
        private String dtto = "";
        private String out_code = "";
        private String j_type = "", jtype = ""; //H - Hotel Transactions, S - Sa[les Transactions, P - Purchases Transactions, ST - Stock Transactions
        private String inv_frm = "", inv_to = "", type = "", typevalue = "";
        int indx_type = -1;
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();

        Boolean isReady = false, isUse = false;

        public journalize(String journaltype, String _jtype = null)
        {
            InitializeComponent();

            rpt = new Report();
            j_type = journaltype;
            jtype = _jtype;

            dtp_frm.Value = DateTime.Parse(db.get_systemdate(""));
            dtp_to.Value = DateTime.Parse(db.get_systemdate(""));

            gc.load_journal(cbo_journal);
            gc.load_openperiod(cbo_period, true);


            int fy = Convert.ToInt32(DateTime.Now.ToString("yyyy")), mo = Convert.ToInt32(DateTime.Now.ToString("MM"));
            if(mo > 10)
            {
                mo = mo % 10;
                fy ++;
            }else mo += 2;

            cbo_period.SelectedValue = fy.ToString() + "-" + mo.ToString();
            if (cbo_period.SelectedValue == null)
            {
                cbo_period.SelectedValue = fy.ToString() + "-0";
            }


            if (journaltype == "A")
            {
                if (jtype == "period")
                {
                    lbl_dt.Hide(); lbl_dt_to.Hide();
                    dtp_frm.Hide(); dtp_to.Hide();

                    load_soaperiod(cbo_typ);
                    lbl_typ.Text = "SOA Period";
                }
                else if (jtype == "date")
                {
                    dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyy-MM-01"));

                    lbl_inv.Text = "SOA#";
                    lbl_dt.Text = "SOA Date";

                    lbl_dt.Top = 28;
                    dtp_frm.Top = 26;
                    lbl_dt_to.Top = 28;
                    dtp_to.Top = 26;
                    label1.Top = 84;
                    cbo_journal.Top = 81;
                    label2.Top = 113;
                    cbo_period.Top = 110;
                    groupBox1.Height = 148;
                    cbo_typ.Hide(); lbl_typ.Hide();

                    load_soaentry(cbo_inv_frm);
                    load_soaentry(cbo_inv_to);
                }
            }
            if (journaltype == "H")
            {
                dtp_frm.Value = DateTime.Parse(dtp_to.Value.ToString("yyy-MM-01"));

                lbl_inv.Text = "Contract#";

                lbl_dt.Top = 28;
                dtp_frm.Top = 26;
                lbl_dt_to.Top = 28;
                dtp_to.Top = 26;
                label1.Top = 84;
                cbo_journal.Top = 81;
                label2.Top = 113;
                cbo_period.Top = 110;
                groupBox1.Height = 148;
                cbo_typ.Hide(); lbl_typ.Hide();
            }
            else if (journaltype == "S")
            {
                this.Text = "Journalize Sales Outlet Transactions";

                lbl_typ.Text = "Outlet";
                cbo_inv_frm.Hide(); cbo_inv_to.Hide(); lbl_inv.Hide(); lbl_inv_to.Hide();

                gc.load_outlet(cbo_typ);
            }
            else if (journaltype == "P") //j_type
            {
                this.Text = "Journalize Purchase Transactions";

                //cbo_typ.Hide(); lbl_typ.Hide();
                lbl_dt.Hide(); lbl_dt_to.Hide(); dtp_frm.Hide(); dtp_to.Hide();

                gc.load_pinvhd_inv_not_jrnlz(cbo_inv_frm);
                gc.load_pinvhd_inv_not_jrnlz(cbo_inv_to);

                cbo_typ.Items.Add("Purchase Order");//0
                cbo_typ.Items.Add("Direct Purchases");//1
                cbo_typ.Items.Add("Purchase Returns");//2
            }
            else if (journaltype == "ST")
            {
                this.Text = "Journalize Stock Transactions";

                lbl_typ.Text = "Transaction Type";
                lbl_dt.Hide(); lbl_dt_to.Hide(); dtp_frm.Hide(); dtp_to.Hide();

                cbo_typ.Items.Add("Issuance");//0
                cbo_typ.Items.Add("Transfer");//1
                cbo_typ.Items.Add("Adjustment");//2
            }

            if (journaltype == "ST" || journaltype == "P")
            {
                cbo_journal.SelectedValue = db.get_colval("m99", "pur_jrl", ""); 
            }
            else if (journaltype == "S" || journaltype == "H" || journaltype == "A")
            {
                cbo_journal.SelectedValue = db.get_colval("m99", "sal_jrl", "");
            }


            isReady = true;
        }

        private void load_stock(ComboBox cbo, String type)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = db.QueryOnTableWithParams("rechdr", "rec_num", " (jrnlz IS NULL OR jrnlz!='Y') AND COALESCE(cancel,'N')='N' AND trn_type = '" + type + "'", " ORDER BY rec_num ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "rec_num";
                cbo.ValueMember = "rec_num";
                cbo.SelectedIndex = -1;

                input_enable(dt.Rows.Count != 0);
            }
            catch (Exception) { }
        }
        //SELECT gf.rom_code AS \"Room\", gf.typ_code AS \"Type\", gf.full_name AS \"Full Name\", gf.arr_date AS \"Arrival Date\", gf.dep_date AS \"Departure Date\", gf.reg_num AS \"Guest Folio\", gf.user_id AS User, gf.t_date AS \"Trans. Date\", gf.t_time AS \"Trans. Time\", gf.rmrttyp AS \"RT\", (gf.dep_date - gf.t_date) AS total_date FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".guest g ON g.acct_no=gf.acct_no LEFT JOIN " + schema + ".company c ON c.comp_code=g.comp_code WHERE (gf.cancel IS NULL OR gf.cancel ='')" + search + " ORDER BY gf.rom_code ASC

        private void load_purchase(ComboBox cbo, int type)
        {
            try
            {
                DataTable dt = new DataTable();

                if (type == 0)
                {    //direct purchase
                    //dt = db.QueryOnTableWithParams("purhdr", "purc_ord AS code", "(jrnlz IS NULL OR jrnlz!='Y') AND COALESCE(cancel,'N')='N'", " ORDER BY purc_ord ASC");
                    dt = db.QueryOnTableWithParams("rechdr", "rec_num AS code", " (jrnlz IS NULL OR jrnlz!='Y') AND COALESCE(cancel,'N')='N' AND trn_type = 'P'", " ORDER BY rec_num ASC");
                }
                else if (type == 1)
                {    //direct purchase
                    dt = db.QueryOnTableWithParams("pinvhd", "inv_num AS code", "(jrnlz IS NULL OR jrnlz!='Y') AND COALESCE(cancel,'N')='N'", " ORDER BY inv_num ASC");
                }
                else if (type == 2)
                {//purchase returns
                    dt = db.QueryOnTableWithParams("prethdr", "pret_num AS code", "(jrnlz IS NULL OR jrnlz!='Y') AND COALESCE(cancel,'N')='N'", " ORDER BY pret_num ASC");
                }
                cbo.DataSource = dt;
                cbo.DisplayMember = "code";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;

                input_enable(dt.Rows.Count != 0);
            }
            catch (Exception) { }
        }
        private void load_soaperiod(ComboBox cbo)
        {
            cbo.DataSource = db.QueryBySQLCode("SELECT soa_desc, to_Char(soafrom,'yyyy/MM/dd')||'-'||to_Char(soato,'yyyy/MM/dd') AS soa_dt FROM rssys.soa_period WHERE COALESCE(closed,'')<>'Y' ORDER BY soafrom desc,soato desc");
            cbo.DisplayMember = "soa_desc";
            cbo.ValueMember = "soa_dt";
            try { cbo.SelectedIndex = 0; }
            catch { }
        }

        private void load_soaentry(ComboBox cbo)
        {
            cbo.DataSource = db.QueryBySQLCode("SELECT sh.soa_code FROM rssys.soahdr sh LEFT JOIN rssys.soa_period sp ON to_Char(sp.soafrom,'yyyy/MM/dd')||'-'||to_Char(sp.soato,'yyyy/MM/dd')=sh.soa_period LEFT JOIN (SELECT sl.soa_code, CASE WHEN COALESCE(gf.rom_code,'')<>'' THEN gf.rom_code ELSE COALESCE(gfh.rom_code,'') END rom_code FROM rssys.soalne sl LEFT JOIN rssys.gfolio gf ON sl.gfolio=gf.reg_num LEFT JOIN rssys.gfhist gfh ON sl.gfolio=gfh.reg_num WHERE sl.ln_num='1') sl ON (sl.soa_code=sh.soa_code)  WHERE COALESCE(jrnlz,'')<>'Y' AND COALESCE(cancel,'')<>'Y' AND ((sp.soafrom BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' OR sp.soato BETWEEN '" + dtp_frm.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "') OR sh.soa_period BETWEEN '" + dtp_frm.Value.ToString("yyyy/MM/dd") + "' AND '" + dtp_to.Value.ToString("yyyy/MM/dd") + "')  ORDER BY soa_code ASC");
            
            cbo.DisplayMember = "soa_code";
            cbo.ValueMember = "soa_code";
            try { cbo.SelectedIndex = -1; }
            catch { }
        }
        private void j_hotel_Load(object sender, EventArgs e)
        {

        }
        public void mainsave()
        {

        }


        private void btn_proceed_Click(object sender, EventArgs e)
        {
            Boolean proceed = false;
            //mainsave();
            if (MessageBox.Show("Are you sure you want to generate this Transaction?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (cbo_journal.SelectedIndex != -1)
                {
                    if (j_type == "A")
                    {
                        if (cbo_typ.Visible)
                        {
                            if (cbo_typ.SelectedIndex == -1)
                            {
                                MessageBox.Show("Please select soa period.");
                                cbo_typ.DroppedDown = true;
                            }
                            else
                            {
                                proceed = true;
                            }
                        }
                        else
                        {
                            if (cbo_get_index(cbo_inv_frm) == -1 && cbo_get_index(cbo_inv_to) == -1)
                            {
                                MessageBox.Show("Please select soa number.");
                            }
                            else
                            {
                                proceed = true;
                            }
                        }
                    }
                    if (j_type == "H")
                    {
                        proceed = true;
                    }
                    if (j_type == "S")
                    {
                        if (cbo_typ.SelectedIndex == -1)
                        {
                            MessageBox.Show("Please select outlet.");
                            cbo_typ.DroppedDown = true;
                        }
                        else
                        {
                            proceed = true;
                            out_code = cbo_typ.SelectedValue.ToString();
                        }
                    }
                    else if (j_type == "ST")
                    {
                        if (cbo_typ.SelectedIndex == -1)
                        {
                            proceed = false;
                            MessageBox.Show("Please select transaction.");
                            cbo_typ.DroppedDown = true;
                        }
                        else
                        {
                            proceed = true;
                        }
                    }
                    else if (j_type == "P")
                    {
                        if (cbo_typ.SelectedIndex == -1)
                        {
                            proceed = false;
                            MessageBox.Show("Please select purchase.");
                            cbo_typ.DroppedDown = true;
                        }
                        if (cbo_period.SelectedIndex == -1)
                        {
                            proceed = false;
                            MessageBox.Show("Please select period.");
                            cbo_period.DroppedDown = true;
                        }
                        else
                        {
                            proceed = true;
                        }
                    }

                    if (proceed)
                    {
                        dtfrm = dtp_frm.Value.ToString("yyyy-MM-dd");
                        dtto = dtp_to.Value.ToString("yyyy-MM-dd");
                        j_code = (cbo_journal.SelectedValue ?? "").ToString();
                        j_text = (cbo_journal.Text ?? "").ToString();
                        inv_frm = (cbo_inv_frm.SelectedValue ?? "").ToString();
                        inv_to = (cbo_inv_to.SelectedValue ?? "").ToString();
                        typevalue = (cbo_typ.SelectedValue ?? "").ToString();
                        type = (cbo_typ.Text ?? "").ToString();
                        indx_type = cbo_typ.SelectedIndex;

                        bgWorker.RunWorkerAsync();

                        //
                    }
                }
                else
                {
                    MessageBox.Show("Please select journal.");
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void input_enable(Boolean bol)
        {
            dtp_frm.Invoke(new Action(() =>
            {
                dtp_frm.Enabled = bol;
            }));

            dtp_to.Invoke(new Action(() =>
            {
                dtp_to.Enabled = bol;
            }));

            cbo_journal.Invoke(new Action(() =>
            {
                cbo_journal.Enabled = bol;
            }));

            btn_close.Invoke(new Action(() =>
            {
                btn_close.Enabled = bol;
            }));

            btn_proceed.Invoke(new Action(() =>
            {
                btn_proceed.Enabled = bol;
            }));

            cbo_period.Invoke(new Action(() =>
            {
                cbo_period.Enabled = bol;
            }));

            cbo_inv_frm.Invoke(new Action(() =>
            {
                cbo_inv_frm.Enabled = bol;
            }));
            cbo_inv_to.Invoke(new Action(() =>
            {
                cbo_inv_to.Enabled = bol;
            }));
        }

        private void inc_pbar(int i)
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    pbar.Value += i;
                }));
            }
            catch (Exception) { reset_pbar(); }
        }
        private void inc_pbar(int i, int len)
        {
            //inc_pbar(Math.Round(1 / len) * 100);
            Double n = Convert.ToDouble(i);
            Double n2 = Convert.ToDouble(len);
            inc_pbar((Math.Round((n / n2) * 50.0, 1) == (int)((n / n2) * 50.0) ? 1 : 0));
        }

        private void reset_pbar()
        {
            pbar.Invoke(new Action(() =>
            {
                pbar.Value = 0;
            }));
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            input_enable(false);
            cbo_typ.Invoke(new Action(() =>
            {
                cbo_typ.Enabled = false;
            }));

            thisDatabase db = new thisDatabase();
            Boolean success = false;

            DateTime date = DateTime.Parse(db.get_systemdate("yyyy-MM-dd"));
            String fy = db.get_pk("fy");
            String mo = date.ToString("MM");

            String[] splt = cbo_get_value(cbo_period).Split('-');
            fy = splt.GetValue(0).ToString();
            mo = splt.GetValue(1).ToString();

            String WHERE = "";
            String rpt_jcodes = "''";

            try
            {
                if (j_type == "A")
                {
                    inc_pbar(50);

                    /*if (cbo_is_visible(cbo_typ))
                    {
                        WHERE += " AND sh.soa_period='" + cbo_get_value(cbo_typ) + "' ";
                    }
                    else
                    {
                        WHERE += " AND t_date BETWEEN '" + dtfrm + "' AND '" + dtto + "' ";
                    }*/

                    ////COALESCE(jrnlz,'')<>'Y' AND COALESCE(cancel,'')<>'Y'

                    if (cbo_get_index(cbo_inv_frm) != -1)
                    {
                        WHERE = " AND '" + cbo_get_value(cbo_inv_frm) + "' <= soa_code";
                    }
                    if (cbo_get_index(cbo_inv_to) != -1)
                    {
                        WHERE = " AND  soa_code <='" + cbo_get_value(cbo_inv_to) + "' ";
                    }
                    if (cbo_get_index(cbo_inv_frm) != -1 && cbo_get_index(cbo_inv_to) != -1)
                    {
                        WHERE = " AND soa_code BETWEEN '" + cbo_get_value(cbo_inv_frm) + "' AND '" + cbo_get_value(cbo_inv_to) + "'";
                    }



                    DataTable dtlst = new DataTable(), dt = db.QueryBySQLCode("SELECT * FROM rssys.soahdr sh LEFT JOIN rssys.soa_period sp ON to_Char(sp.soafrom,'yyyy/MM/dd')||'-'||to_Char(sp.soato,'yyyy/MM/dd')=sh.soa_period WHERE COALESCE(cancel,'')<>'Y' AND COALESCE(jrnlz,'')<>'Y'  " + WHERE + " ORDER BY soa_code ASC");


                    if (dt != null)
                    {
                        String j_num, t_desc;
                        String soa_code, sl_name, sl_code, reg_num, seq_num, at_code, cc_code, scc_code, seq_desc, chg_code, chg_desc, chg_num, chg_date, chg_type, dr_cr, invoice = "", rep_code = "", pay_code = "", rom_code = "", _t_date = "";
                        Double amount, credit, debit, tot_cr = 0, tot_dr = 0;
                        DateTime t_date;
                        int lnno = 1;

                        List<String> lst_chgdesc = new List<String>();
                        List<String> lst_chgcode = new List<String>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                            sl_name = dt.Rows[i]["debt_name"].ToString();
                            sl_code = dt.Rows[i]["debt_code"].ToString();
                            soa_code = dt.Rows[i]["soa_code"].ToString();

                            try 
                            {
                                if (!String.IsNullOrEmpty(dt.Rows[i]["soato"].ToString()))
                                {
                                    if (db.get_fy_period(dt.Rows[i]["soafrom"].ToString()) == cbo_get_value(cbo_period))
                                    {
                                        _t_date = dt.Rows[i]["soafrom"].ToString();
                                    }
                                    else
                                    {
                                        _t_date = dt.Rows[i]["soato"].ToString();
                                    }
                                }
                                else _t_date = dt.Rows[i]["soa_period"].ToString();
                                t_date = DateTime.Parse(_t_date);
                            }
                            catch { t_date = DateTime.Now; }

                            t_desc = "SOA#" + soa_code + "-" + sl_name + " - " + t_date.ToString("MM/dd/yy");

                            db.add_jrnl(fy, mo, j_code, j_num, t_desc, "", "", null, t_date.ToString("yyyy-MM-dd"), date.ToString("yyyy-MM-dd"));
                            rpt_jcodes += ",'" + j_num + "'";

                            try
                            {
                                tot_cr = 0; tot_dr = 0;
                                lnno = 1;

                                dtlst = db.QueryBySQLCode("SELECT sl.*, c.chg_type, c.scc_code, c.cc_code, c.at_code, c.chg_desc, m4.at_desc, m4.dr_cr, gf.rom_code FROM rssys.soalne sl LEFT JOIN rssys.charge c ON c.chg_code=sl.chg_code LEFT JOIN rssys.m04 m4 ON m4.at_code=c.at_code LEFT JOIN rssys.gfolio gf ON gf.reg_num=sl.gfolio WHERE soa_code='" + soa_code + "' ORDER BY ln_num");

                                for (int j = 0; j < dtlst.Rows.Count; j++)
                                {

                                    seq_num = lnno.ToString();
                                    //invoice = dt.Rows[i]["reg_num"].ToString();
                                    //invoice = invoice_code + invoice.Substring(3);
                                    at_code = dtlst.Rows[j]["at_code"].ToString();
                                    cc_code = dtlst.Rows[j]["cc_code"].ToString();
                                    scc_code = dtlst.Rows[j]["scc_code"].ToString();
                                    seq_desc = dtlst.Rows[j]["reference"].ToString();
                                    chg_code = dtlst.Rows[j]["chg_code"].ToString();
                                    chg_desc = db.get_colval("charge", "chg_desc", "chg_code='" + chg_code + "'");
                                    chg_num = dtlst.Rows[j]["chg_num"].ToString();
                                    chg_date = dtlst.Rows[j]["chg_date"].ToString();
                                    chg_type = dtlst.Rows[j]["chg_type"].ToString();
                                    dr_cr = dtlst.Rows[j]["dr_cr"].ToString();
                                    amount = gm.toNormalDoubleFormat(dtlst.Rows[j]["amount"].ToString());
                                    if (!String.IsNullOrEmpty(dtlst.Rows[j]["rom_code"].ToString()))
                                    {
                                        rom_code = dtlst.Rows[j]["rom_code"].ToString();
                                    }

                                    debit = 0.00;
                                    credit = 0.00;
                                    invoice = "";
                                    sl_code = "";
                                    sl_name = "";
                                    rep_code = "";
                                    pay_code = "";



                                    lst_chgcode.Add(chg_code);
                                    lst_chgdesc.Add(chg_desc + "(" + seq_desc + ")");

                                    if (chg_type == "P")
                                        amount = amount * -1;

                                    if (dr_cr == "D")
                                    {
                                        debit = amount;

                                        if (debit < 0)
                                        {
                                            credit = amount;
                                            debit = 0.00;
                                            tot_cr += credit;
                                        }
                                        else
                                        {
                                            tot_dr += debit;
                                        }
                                    }
                                    else
                                    {
                                        credit = amount;

                                        if (credit < 0)
                                        {
                                            debit = amount;
                                            credit = 0.00;
                                            tot_dr += debit;
                                        }
                                        else
                                        {
                                            tot_cr += credit;
                                        }
                                    }

                                    db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit.ToString("0.00"), credit.ToString("0.00"), invoice, seq_desc, rep_code, pay_code, null, null);
                                    //include charges
                                    db.UpdateOnTable("tr02", "chg_code='" + chg_code + "', chg_num='" + chg_num + "', chg_desc=" +  db.str_E(chg_desc) + "", "j_code='" + j_code + "' AND j_num='" + j_num + "' AND seq_num='" + seq_num + "'");

                                    lnno++;

                                }

                                seq_num = lnno.ToString();
                                sl_name = dt.Rows[i]["debt_name"].ToString();
                                sl_code = dt.Rows[i]["debt_code"].ToString();
                                at_code = db.get_colval("m06", "at_code", "d_code='" + sl_code + "'");
                                invoice = soa_code;
                                cc_code = ""; scc_code = "";
                                seq_desc = ""; chg_code = "";
                                chg_num = ""; chg_date = "";
                                debit = 0.00; credit = 0.00;

                                if (String.IsNullOrEmpty(at_code)) at_code = "1111";

                                if (tot_dr > tot_cr)
                                {
                                    credit = tot_dr - tot_cr;
                                }
                                else
                                {
                                    debit = tot_cr - tot_dr;
                                }

                                db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit.ToString("0.00"), credit.ToString("0.00"), invoice, seq_desc, rep_code, pay_code, null, null);
                                db.upd_unpaid_invoices(j_code, sl_code, invoice, debit, credit);

                                chg_code = String.Join(";", lst_chgcode.ToArray());
                                chg_desc = String.Join(";", lst_chgdesc.ToArray());

                                sl_name = db.get_colval("m06", "d_name", "d_code='" + sl_code + "'");
                                db.InsertOnTable("tr02_ext", "j_code, j_num, seq_num, chg_desc, chg_code, invoice, invoice_code, debit, credit, rom_code, sl_code, sl_name" , "'" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + chg_desc + "', '" + chg_code + "', '" + invoice + "', '" + invoice + "', '" + debit + "', '" + credit + "', '" + rom_code + "', '" + sl_code + "', '" + sl_name + "'");

                                success = true;
                            }
                            catch { success = false; }


                            if (!success)
                            {
                                db.DeleteOnTable("tr02_ext", "j_code='" + j_code + "' AND j_num='" + j_num + "'");
                                db.DeleteOnTable("tr02", "j_code='" + j_code + "' AND j_num='" + j_num + "'");
                                db.DeleteOnTable("tr01", "j_code='" + j_code + "' AND j_num='" + j_num + "'");

                            }
                            else
                            {

                                db.UpdateOnTable("soahdr", "jrnlz='Y'", "soa_code='" + soa_code + "'");
                                db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");
                                db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "', jo_code='', purc_ord='',  inv_num='', pr_code='', dr_code=''", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                            }
                        }

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Nothing to journalize soa.");
                        }
                        else
                        {
                            if (success)
                            {
                                MessageBox.Show("Hotel Transactions journalized successfully.");
                            }
                        }
                    }
                }
                else if (j_type == "H")
                {
                    inc_pbar(50);

                    if (db.jrnlz_hotel(j_code, dtfrm, dtto))
                    {
                        inc_pbar(25);

                        success = true;

                        MessageBox.Show("Hotel Transactions journalized successfully.");
                    }

                    ////////////////////
                    
                    inc_pbar(50);

                    if (cbo_get_index(cbo_inv_frm) != -1)
                    {
                        WHERE = " AND '" + cbo_get_value(cbo_inv_frm) + "' <= soa_code";
                    }
                    if (cbo_get_index(cbo_inv_to) != -1)
                    {
                        WHERE = " AND  soa_code <='" + cbo_get_value(cbo_inv_to) + "' ";
                    }
                    if (cbo_get_index(cbo_inv_frm) != -1 && cbo_get_index(cbo_inv_to) != -1)
                    {
                        WHERE = " AND soa_code BETWEEN '" + cbo_get_value(cbo_inv_frm) + "' AND '" + cbo_get_value(cbo_inv_to) + "'";
                    }

                    DataTable dtlst = new DataTable(), dt = db.QueryBySQLCode("SELECT * FROM rssys.soahdr sh LEFT JOIN rssys.soa_period sp ON to_Char(sp.soafrom,'yyyy/MM/dd')||'-'||to_Char(sp.soato,'yyyy/MM/dd')=sh.soa_period WHERE COALESCE(cancel,'')<>'Y' AND COALESCE(jrnlz,'')<>'Y'  " + WHERE + " ORDER BY soa_code ASC");

                    if (dt != null)
                    {
                        String j_num, t_desc;
                        String soa_code, sl_name, sl_code, reg_num, seq_num, at_code, cc_code, scc_code, seq_desc, chg_code, chg_desc, chg_num, chg_date, chg_type, dr_cr, invoice = "", rep_code = "", pay_code = "", rom_code = "", _t_date = "";
                        Double amount, credit, debit, tot_cr = 0, tot_dr = 0;
                        DateTime t_date;
                        int lnno = 1;

                        List<String> lst_chgdesc = new List<String>();
                        List<String> lst_chgcode = new List<String>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                            sl_name = dt.Rows[i]["debt_name"].ToString();
                            sl_code = dt.Rows[i]["debt_code"].ToString();
                            soa_code = dt.Rows[i]["soa_code"].ToString();

                            try
                            {
                                if (!String.IsNullOrEmpty(dt.Rows[i]["soato"].ToString()))
                                    _t_date = dt.Rows[i]["soato"].ToString();
                                else _t_date = dt.Rows[i]["soa_period"].ToString();
                                t_date = DateTime.Parse(_t_date);
                            }
                            catch { t_date = DateTime.Now; }

                            t_desc = "SOA#" + soa_code + "-" + sl_name + " - " + t_date.ToString("MM/dd/yy");

                            db.add_jrnl(fy, mo, j_code, j_num, t_desc, "", "", null, t_date.ToString("yyyy-MM-dd"), date.ToString("yyyy-MM-dd"));
                            rpt_jcodes += ",'" + j_num + "'";

                            try
                            {
                                tot_cr = 0; tot_dr = 0;
                                lnno = 1;

                                dtlst = db.QueryBySQLCode("SELECT sl.*, c.chg_type, c.scc_code, c.cc_code, c.at_code, c.chg_desc, m4.at_desc, m4.dr_cr, gf.rom_code FROM rssys.soalne sl LEFT JOIN rssys.charge c ON c.chg_code=sl.chg_code LEFT JOIN rssys.m04 m4 ON m4.at_code=c.at_code LEFT JOIN rssys.gfolio gf ON gf.reg_num=sl.gfolio WHERE soa_code='" + soa_code + "' ORDER BY ln_num");

                                for (int j = 0; j < dtlst.Rows.Count; j++)
                                {

                                    seq_num = lnno.ToString();
                                    //invoice = dt.Rows[i]["reg_num"].ToString();
                                    //invoice = invoice_code + invoice.Substring(3);
                                    at_code = dtlst.Rows[j]["at_code"].ToString();
                                    cc_code = dtlst.Rows[j]["cc_code"].ToString();
                                    scc_code = dtlst.Rows[j]["scc_code"].ToString();
                                    seq_desc = dtlst.Rows[j]["reference"].ToString();
                                    chg_code = dtlst.Rows[j]["chg_code"].ToString();
                                    chg_desc = db.get_colval("charge", "chg_desc", "chg_code='" + chg_code + "'");
                                    chg_num = dtlst.Rows[j]["chg_num"].ToString();
                                    chg_date = dtlst.Rows[j]["chg_date"].ToString();
                                    chg_type = dtlst.Rows[j]["chg_type"].ToString();
                                    dr_cr = dtlst.Rows[j]["dr_cr"].ToString();
                                    amount = gm.toNormalDoubleFormat(dtlst.Rows[j]["amount"].ToString());
                                    if (!String.IsNullOrEmpty(dtlst.Rows[j]["rom_code"].ToString()))
                                    {
                                        rom_code = dtlst.Rows[j]["rom_code"].ToString();
                                    }

                                    debit = 0.00;
                                    credit = 0.00;
                                    invoice = "";
                                    sl_code = "";
                                    sl_name = "";
                                    rep_code = "";
                                    pay_code = "";



                                    lst_chgcode.Add(chg_code);
                                    lst_chgdesc.Add(chg_desc + "(" + seq_desc + ")");

                                    if (chg_type == "P")
                                        amount = amount * -1;

                                    if (dr_cr == "D")
                                    {
                                        debit = amount;

                                        if (debit < 0)
                                        {
                                            credit = amount;
                                            debit = 0.00;
                                            tot_cr += credit;
                                        }
                                        else
                                        {
                                            tot_dr += debit;
                                        }
                                    }
                                    else
                                    {
                                        credit = amount;

                                        if (credit < 0)
                                        {
                                            debit = amount;
                                            credit = 0.00;
                                            tot_dr += debit;
                                        }
                                        else
                                        {
                                            tot_cr += credit;
                                        }
                                    }

                                    db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit.ToString("0.00"), credit.ToString("0.00"), invoice, seq_desc, rep_code, pay_code, null, null);
                                    //include charges
                                    db.UpdateOnTable("tr02", "chg_code='" + chg_code + "', chg_num='" + chg_num + "', chg_desc=" + db.str_E(chg_desc) + "", "j_code='" + j_code + "' AND j_num='" + j_num + "' AND seq_num='" + seq_num + "'");

                                    lnno++;

                                }

                                seq_num = lnno.ToString();
                                sl_name = dt.Rows[i]["debt_name"].ToString();
                                sl_code = dt.Rows[i]["debt_code"].ToString();
                                at_code = db.get_colval("m06", "at_code", "d_code='" + sl_code + "'");
                                invoice = soa_code;
                                cc_code = ""; scc_code = "";
                                seq_desc = ""; chg_code = "";
                                chg_num = ""; chg_date = "";
                                debit = 0.00; credit = 0.00;

                                if (String.IsNullOrEmpty(at_code)) at_code = "1111";

                                if (tot_dr > tot_cr)
                                {
                                    credit = tot_dr - tot_cr;
                                }
                                else
                                {
                                    debit = tot_cr - tot_dr;
                                }

                                db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit.ToString("0.00"), credit.ToString("0.00"), invoice, seq_desc, rep_code, pay_code, null, null);
                                db.upd_unpaid_invoices(j_code, sl_code, invoice, debit, credit);

                                chg_code = String.Join(";", lst_chgcode.ToArray());
                                chg_desc = String.Join(";", lst_chgdesc.ToArray());

                                sl_name = db.get_colval("m06", "d_name", "d_code='" + sl_code + "'");
                                db.InsertOnTable("tr02_ext", "j_code, j_num, seq_num, chg_desc, chg_code, invoice, invoice_code, debit, credit, rom_code, sl_code, sl_name", "'" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + chg_desc + "', '" + chg_code + "', '" + invoice + "', '" + invoice + "', '" + debit + "', '" + credit + "', '" + rom_code + "', '" + sl_code + "', '" + sl_name + "'");

                                success = true;
                            }
                            catch { success = false; }


                            if (!success)
                            {
                                db.DeleteOnTable("tr02_ext", "j_code='" + j_code + "' AND j_num='" + j_num + "'");
                                db.DeleteOnTable("tr02", "j_code='" + j_code + "' AND j_num='" + j_num + "'");
                                db.DeleteOnTable("tr01", "j_code='" + j_code + "' AND j_num='" + j_num + "'");

                            }
                            else
                            {

                                db.UpdateOnTable("soahdr", "jrnlz='Y'", "soa_code='" + soa_code + "'");
                                db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");
                                db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "', jo_code='', purc_ord='',  inv_num='', pr_code='', dr_code=''", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                            }
                        }

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Nothing to journalize soa.");
                        }
                        else
                        {
                            if (success)
                            {
                                MessageBox.Show("Hotel Transactions journalized successfully.");
                            }
                        }
                    }

                }
                else if (j_type == "S")
                {
                    inc_pbar(25);

                    String col = "", val = "", col2 = "", val2 = "";
                    DateTime mydate = DateTime.Parse(db.get_systemdate("yyyy-MM-dd"));
                    String ord_code = "", ord_date = "", customer = "", debt_code = "", jrnlz = "", out_code = "", j_code = "", j_num = "", at_code = "", at_codesl = "", total_amnt = "", dr_cr = "", t_desc = "";
                    String ln_num = "", item_code = "", item_desc = "", ln_amnt = "", rep_code = "", trnx_date = "", t_time = "", fcp = "", pay_code = "", ln_type = "", ln_tax = "", net_amount = "";
                    String debit = "0.00", credit = "0.00";
                    Double total_dr = 0, total_cr = 0;
                    DataTable dt1 = new DataTable();
                    DataTable ldt = new DataTable();
                    DataTable idt = new DataTable();

                    String ffs = "SELECT out_code,ord_code,ord_date,jrnlz,customer,debt_code,total_amnt from rssys.orhdr WHERE out_code='" + typevalue + "' AND jrnlz<>'Y' AND ord_date BETWEEN '" + dtfrm + "' AND '" + dtto + "' ";

                    dt1 = db.QueryBySQLCode("SELECT out_code,ord_code,ord_date,jrnlz,customer,debt_code, total_amnt,trnx_date from rssys.orhdr WHERE out_code='" + typevalue + "' AND COALESCE(jrnlz,'')<>'Y' AND COALESCE(pending,'')='N' AND ord_date BETWEEN '" + dtfrm + "' AND '" + dtto + "' ");

                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            inc_pbar(1);
                            ord_code = dt1.Rows[i]["ord_code"].ToString();
                            ord_date = dt1.Rows[i]["ord_date"].ToString();
                            customer = dt1.Rows[i]["customer"].ToString();
                            debt_code = dt1.Rows[i]["debt_code"].ToString();
                            out_code = dt1.Rows[i]["out_code"].ToString();
                            total_amnt = dt1.Rows[i]["total_amnt"].ToString();
                            j_code = db.get_colval("m05", "j_code", "j_desc='" + j_text + "'");
                            j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                            t_desc = "SO#" + ord_code + ", " + customer + " - " + gm.toDateString(dt1.Rows[i]["trnx_date"].ToString(), "") + "";

                            col = "fy,j_code,j_num,t_date,t_desc,payee,user_id,sysdate, systime,mo";
                            val = "'" + fy + "','" + j_code + "','" + j_num + "','" + gm.toDateString(dt1.Rows[i]["trnx_date"].ToString(), "") + "','" + t_desc + "','','" + GlobalClass.username.ToString() + "','" + mydate.ToString("yyyy-MM-dd") + "','" + db.get_systemtime() + "','" + mydate.ToString("MM") + "'";
                            if (db.InsertOnTable("tr01", col, val))
                            {
                                rpt_jcodes += ",'" + j_num + "'";
                                db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");
                                db.UpdateOnTable("orhdr", "jrnlz='Y'", "ord_code='" + ord_code + "'");
                            }
                            DataTable dt2 = new DataTable();
                            dt2 = db.QueryBySQLCode("SELECT ord_code, ln_num, item_code, item_desc, ln_amnt,  rep_code, trnx_date, t_time, fcp, pay_code, ln_type, ln_tax, net_amount from rssys.orlne WHERE ord_code='" + ord_code + "'");

                            at_codesl = db.get_colval("m06", "at_code", "d_code='" + debt_code + "'");

                            if (dt2.Rows.Count > 0)
                            {
                                int indx = 1;

                                total_cr = 0;
                                col2 = "j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, prj_code, debit, credit, invoice, item_code, item_desc, seq_desc";

                                for (int j = 0; j < dt2.Rows.Count; j++)
                                {
                                    inc_pbar(j, dt2.Rows.Count);
                                    item_code = dt2.Rows[j]["item_code"].ToString();
                                    //ln_num = dt2.Rows[j]["ln_num"].ToString();
                                    item_desc = dt2.Rows[j]["item_desc"].ToString();
                                    ln_amnt = dt2.Rows[j]["ln_amnt"].ToString();
                                    rep_code = dt2.Rows[j]["rep_code"].ToString();
                                    //trnx_date = dt2.Rows[j]["trnx_date"].ToString();
                                    //t_time = dt2.Rows[j]["t_time"].ToString();

                                    pay_code = dt2.Rows[j]["pay_code"].ToString();
                                    ln_type = dt2.Rows[j]["ln_type"].ToString();
                                    ln_tax = dt2.Rows[j]["ln_tax"].ToString();
                                    net_amount = dt2.Rows[j]["net_amount"].ToString();

                                    t_desc = "SO#" + ord_code + ", Line#" + dt2.Rows[j]["ln_num"].ToString();

                                    //CREDIT
                                    ln_num = (indx++).ToString();
                                    at_code = getAtCodeFromItemCode("sales", item_code);

                                    debit = "0.00";
                                    credit = ln_amnt;

                                    total_cr += gm.toNormalDoubleFormat(credit);

                                    val2 = "'" + j_code + "','" + j_num + "','" + ln_num + "','" + at_code + "','" + debt_code + "'," + db.str_E(customer) + ",'GEN','','" + debit + "','" + credit + "','','" + item_code + "', " + db.str_E(item_desc) + ", " + db.str_E(t_desc) + "";
                                    db.InsertOnTable("tr02", col2, val2);

                                }//orlne for loop

                                //DEBIT
                                ln_num = (indx++).ToString();
                                debit = total_cr.ToString("0.00");
                                credit = "0.00";
                                
                                val2 = "'" + j_code + "','" + j_num + "','" + ln_num + "','" + at_codesl + "','" + debt_code + "'," + db.str_E(customer) + ",'GEN','','" + debit + "','" + credit + "','" + ord_code + "','', '', ''";
                                db.InsertOnTable("tr02", col2, val2);
                                
                            }
                        }//orhdr for loop
                    }

                    //Sales Return

                    dt1 = db.QueryBySQLCode("SELECT o.*, w.* FROM rssys.rethdr o LEFT JOIN  rssys.whouse w ON w.whs_code=o.whs_code WHERE COALESCE(jrnlz,'')<>'Y' AND o.ret_date BETWEEN '" + dtfrm + "' AND '" + dtto + "'");
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            inc_pbar(1);
                            String sret = "", ret_date = "";
                            sret = dt1.Rows[i]["ret_num"].ToString();
                            ret_date = dt1.Rows[i]["ret_date"].ToString();
                            customer = dt1.Rows[i]["debt_name"].ToString();
                            debt_code = dt1.Rows[i]["debt_code"].ToString();
                            j_code = db.get_colval("m05", "j_code", "j_desc='" + j_text + "'");
                            j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                            t_desc = "SRET#" + sret + ", " + customer + " - " + gm.toDateString(dt1.Rows[i]["t_date"].ToString(), "") + "";

                            col = "fy,j_code,j_num,t_date,t_desc,payee,user_id,sysdate, systime,mo";
                            val = "'" + fy + "','" + j_code + "','" + j_num + "','" + gm.toDateString(dt1.Rows[i]["t_date"].ToString(), "") + "','" + t_desc + "','','" + GlobalClass.username.ToString() + "','" + mydate.ToString("yyyy-MM-dd") + "','" + db.get_systemtime() + "','" + mydate.ToString("MM") + "'";
                            if (db.InsertOnTable("tr01", col, val))
                            {
                                rpt_jcodes += ",'" + j_num + "'";
                                db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");
                                db.UpdateOnTable("rethdr", "jrnlz='Y'", "ret_num='" + sret + "'");
                            }

                            DataTable dt2 = new DataTable();
                            dt2 = db.QueryBySQLCode("SELECT r.*, u.* FROM rssys.retlne r LEFT JOIN rssys.itmunit u ON r.unit=u.unit_id WHERE r.ret_num='" + sret + "' ORDER BY r.ln_num ASC");

                            at_codesl = db.get_colval("m06", "at_code", "d_code='" + debt_code + "'");

                            if (dt2.Rows.Count > 0)
                            {
                                int indx = 1;

                                total_dr = 0;
                                col2 = "j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, prj_code, debit, credit, invoice, item_code, item_desc, seq_desc";

                                for (int j = 0; j < dt2.Rows.Count; j++)
                                {
                                    inc_pbar(j, dt2.Rows.Count);
                                    item_code = dt2.Rows[j]["item_code"].ToString();
                                    //ln_num = dt2.Rows[j]["ln_num"].ToString();
                                    item_desc = dt2.Rows[j]["item_desc"].ToString();
                                    ln_amnt = dt2.Rows[j]["ln_amnt"].ToString();
                                    rep_code = dt2.Rows[j]["rep_code"].ToString();
                                    //trnx_date = dt2.Rows[j]["trnx_date"].ToString();
                                    //t_time = dt2.Rows[j]["t_time"].ToString();

                                    //pay_code = dt2.Rows[j]["pay_code"].ToString();
                                    //ln_type = dt2.Rows[j]["ln_type"].ToString();
                                    //ln_tax = dt2.Rows[j]["ln_tax"].ToString();
                                    //net_amount = dt2.Rows[j]["net_amount"].ToString();

                                    t_desc = "SRET#" + sret + ", Line#" + dt2.Rows[j]["ln_num"].ToString();

                                    //DEBIT
                                    ln_num = (indx++).ToString();
                                    at_code = getAtCodeFromItemCode("sales", item_code);

                                    debit = ln_amnt;
                                    credit = "0.00";

                                    total_dr += gm.toNormalDoubleFormat(debit);

                                    val2 = "'" + j_code + "','" + j_num + "','" + ln_num + "','" + at_code + "','" + debt_code + "'," + db.str_E(customer) + ",'GEN','','" + debit + "','" + credit + "','','" + item_code + "', " + db.str_E(item_desc) + ", " + db.str_E(t_desc) + "";
                                    db.InsertOnTable("tr02", col2, val2);

                                }

                                //CREDIT
                                ln_num = (indx++).ToString();
                                debit = "0.00";
                                credit = total_dr.ToString("0.00");

                                val2 = "'" + j_code + "','" + j_num + "','" + ln_num + "','" + at_codesl + "','" + debt_code + "'," + db.str_E(customer) + ",'GEN','','" + debit + "','" + credit + "','" + sret + "','', '', ''";
                                db.InsertOnTable("tr02", col2, val2);

                            }
                        }
                    }


                    //if (db.jrnlz_sales(j_code, out_code,  dtfrm, dtto))
                    //{
                    inc_pbar(25);
                    success = true;

                    MessageBox.Show("Sales Transactions journalized successfully.");
                    //}
                }
                else if (j_type == "P")
                {
                    inc_pbar(25);

                    int typ = this.indx_type;
                    if (typ == 0)
                    {
                        inc_pbar(25);

                        if (!String.IsNullOrEmpty(inv_frm))
                        {
                            WHERE = "AND rec_num >= '" + inv_frm + "'";
                        }
                        if (!String.IsNullOrEmpty(inv_to))
                        {
                            WHERE = "AND rec_num <= '" + inv_to + "'";
                        }
                        if (!String.IsNullOrEmpty(inv_frm) && !String.IsNullOrEmpty(inv_to))
                        {
                            WHERE = "AND rec_num BETWEEN '" + inv_frm + "' AND '" + inv_to + "'";
                        }

                        DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.rechdr WHERE (jrnlz IS NULL OR jrnlz!='Y') AND COALESCE(cancel,'N')='N' AND  trn_type = 'P'  " + WHERE + "  ORDER BY rec_num ASC ");
                        if (dt != null)
                        {
                            DataTable idt, ldt;
                            String rec_num, whs_code, rep_code, j_code, j_num, t_date, t_desc, sysdate, systime, user_id, qty, ln_amnt, ttyp = "";
                            String seq_num, at_code, at_codesl, cc_code, scc_code, debit, credit, ln_vat, ln_num, item_code, item_desc;
                            String col, val, dr_cr;
                            int sindx = cbo_get_index(cbo_typ);
                            j_code = this.j_code;
                            sysdate = date.ToString("yyyy-MM-dd");

                            String ivat = db.get_colval("m99", "acct_ivat", "");

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                inc_pbar(1);
                                try
                                {
                                    rec_num = dt.Rows[i]["rec_num"].ToString();
                                    whs_code = dt.Rows[i]["whs_code"].ToString();
                                    rep_code = dt.Rows[i]["recipient"].ToString();

                                    t_date = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                                    //t_desc = "PO# " + rec_num + "; " + t_date + "";
                                    t_desc = dt.Rows[i]["_reference"].ToString();

                                    j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                                    systime = db.get_systemtime();
                                    user_id = GlobalClass.username;

                                    col = "fy, mo, j_code, j_num, t_date, t_desc, user_id, sysdate,systime";
                                    val = "'" + fy + "', '" + mo + "', '" + j_code + "', '" + j_num + "', '" + t_date + "', " + db.str_E(t_desc) + ", '" + user_id + "', '" + sysdate + "','" + systime + "'";

                                    if (db.InsertOnTable("tr01", col, val))
                                    {
                                        rpt_jcodes += ",'" + j_num + "'";
                                        db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                        db.UpdateOnTable("rechdr", "jrnlz='Y'", "rec_num='" + rec_num + "'");
                                        db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");

                                        idt = db.QueryBySQLCode("SELECT * FROM rssys.reclne WHERE rec_num='" + rec_num + "' ORDER BY ln_num ASC");

                                        at_codesl = db.get_colval("m10", "at_code", "mp_code='" + dt.Rows[i]["payment_term"].ToString() + "'");

                                        int indx = 1;
                                        for (int j = 0; j < idt.Rows.Count; j++)
                                        {
                                            inc_pbar(j, idt.Rows.Count);
                                            try
                                            {
                                                //invoice(recnum), seq_desc(t_desc),whs_code,rep_code
                                                //cht_code   
                                                //seq_num = idt.Rows[j]["ln_num"].ToString();
                                                ln_num = idt.Rows[j]["ln_num"].ToString();
                                                cc_code = idt.Rows[j]["cnt_code"].ToString();
                                                scc_code = idt.Rows[j]["scc_code"].ToString();
                                                item_code = idt.Rows[j]["item_code"].ToString();
                                                item_desc = idt.Rows[j]["item_desc"].ToString();
                                                qty = idt.Rows[j]["recv_qty"].ToString();
                                                ln_amnt = idt.Rows[j]["ln_amnt"].ToString();
                                                ln_vat = idt.Rows[j]["ln_vat"].ToString();
                                                at_code = idt.Rows[j]["cht_code"].ToString();
                                                t_desc = "PO#" + rec_num + ", Line#" + ln_num;
                                                

                                                col = "invoice, seq_desc, whs_code, rep_code, j_code, j_num, seq_num, at_code, cc_code, scc_code, debit, credit, item_code, item_desc, recv_qty, price";


                                                // DEBIT
                                                debit = (gm.toNormalDoubleFormat(ln_amnt) - gm.toNormalDoubleFormat(ln_vat)).ToString();
                                                credit = "0.00";
                                                seq_num = (indx++).ToString();
                                                if (item_code != "TEXT-ITEM")
                                                {
                                                    at_code = getAtCodeFromItemCode("stks", item_code);
                                                }

                                                val = "'" + rec_num + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_code + "', '" + cc_code + "', '" + scc_code + "', '" + debit + "', '" + credit + "', '" + item_code + "', " + db.str_E(item_desc) + ", '0','0.00'";
                                                if (db.InsertOnTable("tr02", col, val)) { }

                                                //CREDIT
                                                credit = ln_amnt;
                                                debit = "0.00";
                                                seq_num = (indx++).ToString();

                                                val = "'" /*+ rec_num*/ + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_codesl + "', '" + cc_code + "', '" + scc_code + "', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ", '" + qty + "', '" + ln_amnt + "'";
                                                if (db.InsertOnTable("tr02", col, val)) { }


                                                // VAT - DEBIT

                                                if (gm.toNormalDoubleFormat(ln_vat) != 0)
                                                {
                                                    debit = ln_vat;
                                                    credit = "0.00";
                                                    at_code = ivat;
                                                    seq_num = (indx++).ToString();

                                                    val = "'',  " + db.str_E(t_desc) + ", '', '', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_code + "', '', '', '" + debit + "', '" + credit + "', '" + item_code + "', " + db.str_E(item_desc) + ", '0','0.00'";
                                                    if (db.InsertOnTable("tr02", col, val)) { }
                                                }
                                            }
                                            catch { }
                                        }

                                    }
                                }
                                catch { }
                            }
                        }


                        /*
                        if (!String.IsNullOrEmpty(inv_frm))
                        {
                            WHERE = "AND purc_ord >= '" + inv_frm + "'";
                        }
                        if (!String.IsNullOrEmpty(inv_to))
                        {
                            WHERE = "AND purc_ord <= '" + inv_to + "'";
                        }
                        if (!String.IsNullOrEmpty(inv_frm) && !String.IsNullOrEmpty(inv_to))
                        {
                            WHERE = "AND purc_ord BETWEEN '" + inv_frm + "' AND '" + inv_to + "'";
                        }

                        DataTable dt = db.QueryBySQLCode("SELECT p.*, m.* FROM rssys.purhdr p left join rssys.m10 m on p.pay_code = m.mp_code WHERE (p.jrnlz IS NULL OR p.jrnlz!='Y') AND COALESCE(p.cancel,'N')='N' " + WHERE + "  ORDER BY purc_ord ASC ");

                        if (dt != null)
                        {
                            DataTable idt, ldt;
                            String inv_num, whs_code, rep_code, j_code, j_num, t_date, t_desc, sysdate, systime, user_id, sl_code, sl_name;
                            String seq_num, at_code, at_codesl, cc_code, scc_code, debit, credit, item_code, item_desc, qty, ln_amnt;
                            String col, val, dr_cr;
                            j_code = this.j_code;
                            sysdate = date.ToString("yyyy-MM-dd");
                            String ivat = db.get_colval("m99","acct_ivat","");
                            //rechdr
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                inc_pbar(1);
                                try
                                {
                                    //fy, mo
                                    inv_num = dt.Rows[i]["purc_ord"].ToString();
                                    whs_code = "";
                                    rep_code = dt.Rows[i]["recipient"].ToString();
                                    sl_code = dt.Rows[i]["supl_code"].ToString();
                                    sl_name = dt.Rows[i]["supl_name"].ToString();
                                    t_date = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                                    t_desc = "PO#" + inv_num + "; " + t_date + "";
                                    //t_desc = dt.Rows[i]["reference"].ToString();

                                    j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                                    systime = db.get_systemtime();
                                    user_id = GlobalClass.username;

                                    col = "fy, mo, j_code, j_num, t_date, t_desc, user_id, sysdate,systime";
                                    val = "'" + fy + "', '" + mo + "', '" + j_code + "', '" + j_num + "', '" + t_date + "', " + db.str_E(t_desc) + ", '" + user_id + "', '" + sysdate + "','" + systime + "'";

                                    if (db.InsertOnTable("tr01", col, val))
                                    {
                                        db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                        db.UpdateOnTable("purhdr", "jrnlz='Y'", "purc_ord='" + inv_num + "'");
                                        db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");

                                        idt = db.get_po_items(inv_num);

                                        //DEBIT
                                        //at_codesl = db.get_colval("m07", "at_code", "c_code='" + sl_code + "'");
                                        at_codesl = db.get_colval("m10", "at_code", "mp_code='" + dt.Rows[i]["pay_code"].ToString() + "'");
                                        int indx = 1;
                                        for (int j = 0; j < idt.Rows.Count; j++)
                                        {
                                            inc_pbar(j, idt.Rows.Count);
                                            try
                                            {
                                                
                                                //invoice(recnum), seq_desc(t_desc),whs_code,rep_code
                                                //cht_code   
                                                //seq_num = idt.Rows[j]["ln_num"].ToString();
                                                seq_num = (indx++).ToString();
                                                cc_code = idt.Rows[j]["cc_code"].ToString();
                                                scc_code = idt.Rows[j]["scc_code"].ToString();
                                                item_code = idt.Rows[j]["item_code"].ToString();
                                                item_desc = idt.Rows[j]["item_desc"].ToString();
                                                at_code = idt.Rows[j]["cht_code"].ToString();
                                                t_desc = "PO#" + inv_num + ", Line#" + idt.Rows[j]["ln_num"].ToString();
                                                qty = idt.Rows[j]["delv_qty"].ToString();
                                                ln_amnt = idt.Rows[j]["ln_amnt"].ToString();


                                                debit = idt.Rows[j]["ln_amnt"].ToString();
                                                credit = "0.00";

                                                col = "invoice, seq_desc, whs_code, rep_code, j_code, j_num, seq_num, at_code, cc_code, scc_code, debit, credit, item_code, item_desc,recv_qty, price,sl_code,sl_name";
                                                //debit
                                                val = "'" + inv_num + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_codesl + "', '" + cc_code + "', '" + scc_code + "', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ", '" + qty + "', '" + ln_amnt + "', '" + sl_code + "', " + db.str_E(sl_name) + "";
                                                if (db.InsertOnTable("tr02", col, val)) { }
                                                //


                                                //CREDIT
                                                seq_num = (indx++).ToString();
                                                if (item_code != "TEXT-ITEM")
                                                {
                                                    at_code = getAtCodeFromItemCode("stks", item_code);
                                                }


                                                debit = "0.00";
                                                credit = idt.Rows[j]["ln_amnt"].ToString();
                                                //credit
                                                val = "'" + inv_num + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_code + "', '" + cc_code + "', '" + scc_code + "', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ",'0','0.00','',''";
                                                if (db.InsertOnTable("tr02", col, val)) { }

                                            }
                                            catch { }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }*/

                    }
                    else if (typ == 1)
                    {
                        if (!String.IsNullOrEmpty(inv_frm))
                        {
                            WHERE = "AND inv_num >= '" + inv_frm + "'";
                        }
                        if (!String.IsNullOrEmpty(inv_to))
                        {
                            WHERE = "AND inv_num <= '" + inv_to + "'";
                        }
                        if (!String.IsNullOrEmpty(inv_frm) && !String.IsNullOrEmpty(inv_to))
                        {
                            WHERE = "AND inv_num BETWEEN '" + inv_frm + "' AND '" + inv_to + "'";
                        }

                        DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.pinvhd WHERE (jrnlz IS NULL OR jrnlz!='Y') AND COALESCE(cancel,'N')='N' " + WHERE + "  ORDER BY inv_num ASC ");
                        if (dt != null)
                        {
                            DataTable idt, ldt;
                            String inv_num, whs_code, rep_code, j_code, j_num, t_date, t_desc, sysdate, systime, user_id, sl_code, sl_name, ln_vat = "";
                            String seq_num, at_code, at_codesl, cc_code, scc_code, debit, credit, item_code, item_desc, qty, ln_amnt;
                            String col, val, dr_cr;
                            j_code = this.j_code;
                            sysdate = date.ToString("yyyy-MM-dd");

                            String ivat = db.get_colval("m99", "acct_ivat", "");

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                inc_pbar(1);
                                try
                                {
                                    //fy, mo
                                    inv_num = dt.Rows[i]["inv_num"].ToString();
                                    whs_code = dt.Rows[i]["whs_code"].ToString();
                                    rep_code = dt.Rows[i]["recipient"].ToString();
                                    sl_code = dt.Rows[i]["supl_code"].ToString();
                                    sl_name = dt.Rows[i]["supl_name"].ToString();
                                    t_date = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                                    //t_desc = "PI# " + inv_num + "; " + t_date + "";
                                    t_desc = dt.Rows[i]["reference"].ToString();

                                    j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                                    systime = db.get_systemtime();
                                    user_id = GlobalClass.username;

                                    col = "fy, mo, j_code, j_num, t_date, t_desc, user_id, sysdate,systime";
                                    val = "'" + fy + "', '" + mo + "', '" + j_code + "', '" + j_num + "', '" + t_date + "', " + db.str_E(t_desc) + ", '" + user_id + "', '" + sysdate + "','" + systime + "'";

                                    if (db.InsertOnTable("tr01", col, val))
                                    {
                                        rpt_jcodes += ",'" + j_num + "'";
                                        db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                        db.UpdateOnTable("pinvhd", "jrnlz='Y'", "inv_num='" + inv_num + "'");
                                        db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");

                                        idt = db.QueryBySQLCode("SELECT * FROM rssys.pinvln WHERE inv_num='" + inv_num + "' ORDER BY ln_num ASC");

                                       
                                        at_codesl = db.get_colval("m07", "at_code", "c_code='" + sl_code + "'");
                                        int indx = 1;
                                        for (int j = 0; j < idt.Rows.Count; j++)
                                        {
                                            inc_pbar(j, idt.Rows.Count);
                                            try
                                            {
                                                //invoice(recnum), seq_desc(t_desc),whs_code,rep_code
                                                //cht_code   
                                                //seq_num = idt.Rows[j]["ln_num"].ToString();
                                                cc_code = idt.Rows[j]["cc_code"].ToString();
                                                scc_code = idt.Rows[j]["scc_code"].ToString();
                                                item_code = idt.Rows[j]["item_code"].ToString();
                                                item_desc = idt.Rows[j]["item_desc"].ToString();
                                                //at_code = idt.Rows[j]["cht_code"].ToString();
                                                t_desc = "PI#" + inv_num + ", Line#" + idt.Rows[j]["ln_num"].ToString();
                                                qty = idt.Rows[j]["inv_qty"].ToString();
                                                ln_amnt = idt.Rows[j]["ln_amnt"].ToString();
                                                ln_vat = idt.Rows[j]["ln_vat"].ToString();


                                                col = "invoice, seq_desc, whs_code, rep_code, j_code, j_num, seq_num, at_code, cc_code, scc_code, debit, credit, item_code, item_desc, recv_qty, price,sl_code,sl_name";


                                                // DEBIT
                                                debit = (gm.toNormalDoubleFormat(ln_amnt) - gm.toNormalDoubleFormat(ln_vat)).ToString();
                                                credit = "0.00";
                                                seq_num = (indx++).ToString();
                                                at_code = getAtCodeFromItemCode("stks", item_code);

                                                val = "'" + inv_num + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_code + "', '" + cc_code + "', '" + scc_code + "', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ",'0','0.00','',''";
                                                if (db.InsertOnTable("tr02", col, val)) { }


                                                //CREDIT
                                                debit = "0.00";
                                                credit = idt.Rows[j]["ln_amnt"].ToString();
                                                seq_num = (indx++).ToString();

                                                val = "'" /*+ inv_num*/ + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_codesl + "', '" + cc_code + "', '" + scc_code + "', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ", '" + qty + "', '" + ln_amnt + "', '" + sl_code + "', " + db.str_E(sl_name) + "";
                                                if (db.InsertOnTable("tr02", col, val)) { }


                                                if (gm.toNormalDoubleFormat(ln_vat) != 0) 
                                                {
                                                    // VAT - DEBIT
                                                    debit = ln_vat;
                                                    credit = "0.00";
                                                    at_code = ivat;
                                                    seq_num = (indx++).ToString();

                                                    val = "'',  " + db.str_E(t_desc) + ", '', '', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_code + "', '', '', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ", '0', '0.00', '', ''";
                                                    if (db.InsertOnTable("tr02", col, val)) { }
                                                }
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }

                    }
                    else if (typ == 2)
                    {

                        if (!String.IsNullOrEmpty(inv_frm))
                        {
                            WHERE = "AND pret_num >= '" + inv_frm + "'";
                        }
                        if (!String.IsNullOrEmpty(inv_to))
                        {
                            WHERE = "AND pret_num <= '" + inv_to + "'";
                        }
                        if (!String.IsNullOrEmpty(inv_frm) && !String.IsNullOrEmpty(inv_to))
                        {
                            WHERE = "AND pret_num BETWEEN '" + inv_frm + "' AND '" + inv_to + "'";
                        }

                        DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.prethdr WHERE (jrnlz IS NULL OR jrnlz!='Y') AND COALESCE(cancel,'N')='N' " + WHERE + "  ORDER BY pret_num ASC ");
                        if (dt != null)
                        {
                            DataTable idt, ldt;
                            String pret_num, whs_code, rep_code, j_code, j_num, t_date, t_desc, sysdate, systime, user_id, sl_code, sl_name;
                            String seq_num, at_code, at_codesl, ln_vat = "", cc_code, scc_code, debit, credit, item_code, item_desc, qty, ln_amnt;
                            String col, val, dr_cr;
                            j_code = this.j_code;
                            sysdate = date.ToString("yyyy-MM-dd");
                            Double dprice = 0, dln_amnt;

                            String ivat = db.get_colval("m99", "acct_ivat", "");

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                inc_pbar(1);

                                try
                                {
                                    //fy, mo
                                    pret_num = dt.Rows[i]["pret_num"].ToString();
                                    whs_code = dt.Rows[i]["whs_code"].ToString();
                                    //rep_code = dt.Rows[i]["recipient"].ToString();
                                    rep_code = "";
                                    sl_code = dt.Rows[i]["supl_code"].ToString();
                                    sl_name = dt.Rows[i]["supl_name"].ToString();
                                    t_date = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");
                                    //t_desc = dt.Rows[i]["supl_name"].ToString();
                                    t_desc = dt.Rows[i]["reference"].ToString();

                                    j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                                    systime = db.get_systemtime();
                                    user_id = GlobalClass.username;

                                    col = "fy, mo, j_code, j_num, t_date, t_desc, user_id, sysdate,systime";
                                    val = "'" + fy + "', '" + mo + "', '" + j_code + "', '" + j_num + "', '" + t_date + "', " + db.str_E(t_desc) + ", '" + user_id + "', '" + sysdate + "','" + systime + "'";

                                    if (db.InsertOnTable("tr01", col, val))
                                    {
                                        rpt_jcodes += ",'" + j_num + "'";
                                        db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                        db.UpdateOnTable("prethdr", "jrnlz='Y'", "pret_num='" + pret_num + "'");
                                        db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");

                                        idt = db.QueryBySQLCode("SELECT * FROM rssys.pretlne WHERE pret_num='" + pret_num + "' ORDER BY ln_num ASC");

                                        
                                        at_codesl = db.get_colval("m07", "at_code", "c_code='" + sl_code + "'");
                                        int indx = 1;
                                        for (int j = 0; j < idt.Rows.Count; j++)
                                        {
                                            inc_pbar(j, idt.Rows.Count);
                                            try
                                            {
                                                //invoice(recnum), seq_desc(t_desc),whs_code,rep_code
                                                //cht_code   
                                                //seq_num = idt.Rows[j]["ln_num"].ToString();
                                                //cc_code = (idt.Rows[j]["cc_code"] ?? "").ToString();
                                                //scc_code = (idt.Rows[j]["scc_code"] ?? "").ToString();
                                                item_code = idt.Rows[j]["item_code"].ToString();
                                                item_desc = idt.Rows[j]["item_desc"].ToString();
                                                //at_code = idt.Rows[j]["cht_code"].ToString();
                                                t_desc = "RET#" + pret_num + ", Line#" + idt.Rows[j]["ln_num"].ToString();
                                                qty = idt.Rows[j]["ret_qty"].ToString();
                                                ln_amnt = idt.Rows[j]["ln_amnt"].ToString();

                                                dprice = gm.toNormalDoubleFormat(qty) * gm.toNormalDoubleFormat(idt.Rows[j]["price"].ToString());
                                                dln_amnt = gm.toNormalDoubleFormat(ln_amnt);

                                                col = "invoice, seq_desc, whs_code, rep_code, j_code, j_num, seq_num, at_code, cc_code, scc_code, debit, credit, item_code, item_desc,recv_qty, price,sl_code,sl_name";


                                                // CREDIT
                                                if (dprice < dln_amnt)
                                                {
                                                    credit = dprice.ToString("0");
                                                    ln_vat = (dln_amnt - dprice).ToString("0");
                                                    dln_amnt = dprice + gm.toNormalDoubleFormat(ln_vat);
                                                }
                                                else 
                                                {
                                                    credit = ln_amnt;
                                                    ln_vat = (dprice - dln_amnt).ToString("0");
                                                }
                                                debit = "0.00";
                                                seq_num = (indx++).ToString();
                                                at_code = getAtCodeFromItemCode("stks", item_code);
                                               
                                                val = "'" + pret_num + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_code + "', '" + /*cc_code*/"" + "', '" + /*scc_code*/"" + "', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ",'0','0.00','',''";
                                                if (db.InsertOnTable("tr02", col, val)) { }


                                                //DEBIT
                                                credit = "0.00";
                                                debit = dln_amnt.ToString("0");
                                                seq_num = (indx++).ToString();

                                                val = "'" /*+ pret_num*/ + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_codesl + "', '" + /*cc_code*/"" + "', '" + /*scc_code*/"" + "', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ", '" + qty + "', '" + ln_amnt + "', '" + sl_code + "', " + db.str_E(sl_name) + "";
                                                if (db.InsertOnTable("tr02", col, val)) { }


                                                if (gm.toNormalDoubleFormat(ln_vat) != 0)
                                                {
                                                    // VAT - CREDIT
                                                    credit = ln_vat;
                                                    debit = "0.00";
                                                    at_code = ivat;
                                                    seq_num = (indx++).ToString();

                                                    val = "'',  " + db.str_E(t_desc) + ", '', '', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_code + "', '', '', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ", '0', '0.00', '', ''";
                                                    if (db.InsertOnTable("tr02", col, val)) { }
                                                }
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }

                    }

                    //if (db.jrnlz_purchases(j_code, dtfrm, dtto))
                    //{
                    inc_pbar(25);
                    success = true;

                    MessageBox.Show("Purchase Transactions journalized successfully.");
                    //}
                }
                else if (j_type == "ST")
                {
                    inc_pbar(25);

                    if (!String.IsNullOrEmpty(inv_frm))
                    {
                        WHERE = "AND rec_num >= '" + inv_frm + "'";
                    }
                    if (!String.IsNullOrEmpty(inv_to))
                    {
                        WHERE = "AND rec_num <= '" + inv_to + "'";
                    }
                    if (!String.IsNullOrEmpty(inv_frm) && !String.IsNullOrEmpty(inv_to))
                    {
                        WHERE = "AND rec_num BETWEEN '" + inv_frm + "' AND '" + inv_to + "'";
                    }

                    DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.rechdr WHERE (jrnlz IS NULL OR jrnlz!='Y') AND COALESCE(cancel,'N')='N' AND  trn_type = '" + type.Substring(0, 1) + "'  " + WHERE + "  ORDER BY rec_num ASC ");
                    if (dt != null)
                    {
                        DataTable idt, ldt;
                        String rec_num, whs_code, rep_code, j_code, j_num, t_date, t_desc, sysdate, systime, user_id, qty, ln_amnt, ttyp = "";
                        String seq_num, at_code, at_codesl, cc_code, scc_code, debit, credit, item_code, item_desc;
                        String col, val, dr_cr;
                        int sindx = cbo_get_index(cbo_typ);
                        j_code = this.j_code;
                        sysdate = date.ToString("yyyy-MM-dd");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            inc_pbar(1);
                            try
                            {
                                //fy, mo
                                rec_num = dt.Rows[i]["rec_num"].ToString();
                                whs_code = dt.Rows[i]["whs_code"].ToString();
                                rep_code = dt.Rows[i]["recipient"].ToString();
                                t_date = gm.toDateString(dt.Rows[i]["t_date"].ToString(), "");

                                if (sindx == 0) ttyp = "ISS";
                                else if (sindx == 1) ttyp = "TRA";
                                else if (sindx == 2) ttyp = "ADJ";
                                //t_desc = "" + ttyp + "# " + rec_num + "; " + t_date + "";
                                t_desc = dt.Rows[i]["_reference"].ToString();
                                //Transaction Date <orhdr.trnxdate> - <orhdr.customer> of SO# <orhdr.ord_code>

                                j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                                systime = db.get_systemtime();
                                user_id = GlobalClass.username;

                                col = "fy, mo, j_code, j_num, t_date, t_desc, user_id, sysdate,systime";
                                val = "'" + fy + "', '" + mo + "', '" + j_code + "', '" + j_num + "', '" + t_date + "', " + db.str_E(t_desc) + ", '" + user_id + "', '" + sysdate + "','" + systime + "'";

                                if (db.InsertOnTable("tr01", col, val))
                                {
                                    rpt_jcodes += ",'" + j_num + "'";
                                    db.UpdateOnTable("tr01", "branch='" + GlobalClass.branch + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                    db.UpdateOnTable("rechdr", "jrnlz='Y'", "rec_num='" + rec_num + "'");
                                    db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");

                                    idt = db.QueryBySQLCode("SELECT * FROM rssys.reclne WHERE rec_num='" + rec_num + "' ORDER BY ln_num ASC");

                                    //at_codesl = db.get_colval("m07", "at_code", "c_code='" + sl_code + "'");
                                    int indx = 1;
                                    for (int j = 0; j < idt.Rows.Count; j++)
                                    {
                                        inc_pbar(j, idt.Rows.Count);
                                        try
                                        {
                                            //invoice(recnum), seq_desc(t_desc),whs_code,rep_code
                                            //cht_code   
                                            //seq_num = idt.Rows[j]["ln_num"].ToString();
                                            seq_num = (indx++).ToString();
                                            cc_code = idt.Rows[j]["cnt_code"].ToString();
                                            scc_code = idt.Rows[j]["scc_code"].ToString();
                                            item_code = idt.Rows[j]["item_code"].ToString();
                                            item_desc = idt.Rows[j]["item_desc"].ToString();
                                            qty = idt.Rows[j]["recv_qty"].ToString();
                                            ln_amnt = idt.Rows[j]["ln_amnt"].ToString();


                                            if (sindx == 0)
                                                t_desc = "ISS#" + rec_num + ", Line#" + idt.Rows[j]["ln_num"].ToString();
                                            else if (sindx == 1)
                                            {
                                                t_desc = "TRA#" + rec_num + ", Line#" + idt.Rows[j]["ln_num"].ToString() + " - Stock Transferred From: " + dt.Rows[i]["locationFrom"].ToString();
                                            }
                                            else if (sindx == 2)
                                                t_desc = "ADJ#" + rec_num + ", Line#" + idt.Rows[j]["ln_num"].ToString();

                                            col = "invoice, seq_desc, whs_code, rep_code, j_code, j_num, seq_num, at_code, cc_code, scc_code, debit, credit, item_code, item_desc, recv_qty, price";



                                            //DEBIT
                                            at_code = idt.Rows[j]["cht_code"].ToString();

                                            if (type.Substring(0, 1) == "T") // STOCK TO  // for TRANSFER 
                                            {
                                                at_code = getAtCodeFromItemCode("stks", item_code);
                                                t_desc = "TRA#" + rec_num + ", Line#" + idt.Rows[j]["ln_num"].ToString() + " - Stock Transferred To: " + dt.Rows[i]["locationTo"].ToString();
                                                credit = "0.00";
                                                debit = (gm.toNormalDoubleFormat(idt.Rows[j]["price"].ToString()) * gm.toNormalDoubleFormat(idt.Rows[j]["receiving_qty"].ToString())).ToString("0.00");
                                            }
                                            else
                                            {
                                                debit = "0.00"; credit = "0.00";
                                                if (gm.toNormalDoubleFormat(idt.Rows[j]["ln_amnt"].ToString()) < 0)
                                                {
                                                    credit = (gm.toNormalDoubleFormat(idt.Rows[j]["ln_amnt"].ToString()) * -1).ToString();
                                                }
                                                else
                                                {
                                                    debit = idt.Rows[j]["ln_amnt"].ToString();
                                                }
                                            }

                                            //debit
                                            val = "'" + rec_num + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_code + "', '" + cc_code + "', '" + scc_code + "', '" + debit + "', '" + credit + "', '" + item_code + "', " + db.str_E(item_desc) + ", '0','0.00'";

                                            if (db.InsertOnTable("tr02", col, val)) { }

                                            //CREDIT
                                            debit = "0.00"; credit = "0.00";
                                            if (gm.toNormalDoubleFormat(idt.Rows[j]["ln_amnt"].ToString()) < 0)
                                            {
                                                debit = (gm.toNormalDoubleFormat(idt.Rows[j]["ln_amnt"].ToString()) * -1).ToString();
                                            }
                                            else
                                            {
                                                credit = idt.Rows[j]["ln_amnt"].ToString();
                                            }

                                            seq_num = (indx++).ToString();

                                            val = "'" /*+ rec_num*/ + "',  " + db.str_E(t_desc) + ", '" + whs_code + "', '" + rep_code + "', '" + j_code + "', '" + j_num + "', '" + seq_num + "', '" + at_code + "', '" + cc_code + "', '" + scc_code + "', '" + debit + "', '" + credit + "', '" + item_code + "',  " + db.str_E(item_desc) + ", '" + qty + "', '" + ln_amnt + "'";
                                            
                                            if (db.InsertOnTable("tr02", col, val)) { }

                                        }
                                        catch { }
                                    }

                                }
                            }
                            catch { }
                        }
                    }



                    //if (db.jrnlz_stockTransaction(j_code, dtfrm, dtto))
                    //{
                    inc_pbar(25);
                    success = true;

                    MessageBox.Show(Text + " successfully.");
                    //}
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            inc_pbar(5);

            if (success == false)
            {
                MessageBox.Show("Problem occured on journalization.");
            }

            reset_pbar();
            view_report(" AND t1.j_num IN (" + rpt_jcodes + ")");

            input_enable(true);
            cbo_typ.Invoke(new Action(() =>
            {
                load_invioces();
                cbo_typ.Enabled = true;
            }));
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
        public Boolean cbo_is_visible(ComboBox cbo)
        {
            Boolean isEnabled = false;
            cbo.Invoke(new Action(() => { isEnabled = cbo.Visible; }));
            return isEnabled;
        }
        private String getAtCodeFromItemCode(String trans, String item_code)
        {
            String at_code = "";
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT ig.acct_stks, ig.acct_sales, ig.acct_cost FROM rssys.items i LEFT JOIN rssys.itmgrp ig ON ig.item_grp=i.item_grp WHERE i.item_code='" + item_code + "'");
                at_code = (dt.Rows[0]["acct_" + trans] ?? "").ToString();
            }
            catch { }
            return at_code;
        }

        private void view_report(String WHERE)
        {

            BeginInvoke(new Action(() =>
            {
                String fy = (cbo_period.SelectedValue??DateTime.Now.ToString("yyyy-MM")).ToString().Split('-').GetValue(0).ToString().Trim();
                
                if (j_type == "A")
                {
                    rpt.print_journalized_hotel(Text, j_code, dtfrm, dtto, cbo_period.Text, WHERE);
                    rpt.ShowDialog();
                }
                else
                {
                    rpt.print_journalized(Text , j_code, dtfrm, dtto, cbo_period.Text, WHERE);
                    rpt.ShowDialog();
                }
                /*if (j_type == "H")
                {
                    rpt.print_journalizedhotel(j_code, dtfrm, dtto);
                }*/
            }));
        }

        private void load_invioces()
        {
            isUse = true;

            if (cbo_typ.SelectedIndex != -1)
            {
                if (j_type == "S")
                {
                    String outlet = cbo_typ.SelectedValue.ToString();
                    gc.load_orhdr_inv_not_jrnlz(cbo_inv_frm, outlet);
                    gc.load_orhdr_inv_not_jrnlz(cbo_inv_to, outlet);
                }
                if (j_type == "ST")
                {
                    String type = cbo_typ.Text.Substring(0, 1);
                    this.load_stock(cbo_inv_frm, type);
                    this.load_stock(cbo_inv_to, type);
                }
                if (j_type == "P")
                {
                    int type = cbo_typ.SelectedIndex;
                    this.load_purchase(cbo_inv_frm, type);
                    this.load_purchase(cbo_inv_to, type);
                }
            }

            if (j_type == "A")
            {
                if (jtype == "date")
                {
                    this.load_soaentry(cbo_inv_frm);
                    this.load_soaentry(cbo_inv_to);
                }
            }

            isUse = false;
        }


        private void cbo_typ_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_invioces();
        }

        private void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            if (!isReady) return;
            load_invioces();
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            if (!isReady) return;
            load_invioces();
        }

        private void cbo_inv_frm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            if (!isUse)
            {
                if (j_type == "A")
                {
                    if (jtype == "date")
                    {
                        cbo_inv_to.SelectedValue = (cbo_inv_frm.SelectedValue ?? "").ToString();
                        cbo_inv_to.DroppedDown = true;
                    }
                }
                if (j_type == "P")
                {
                    cbo_inv_to.SelectedIndex = cbo_inv_frm.SelectedIndex;
                }
            }
        }
    }
}
