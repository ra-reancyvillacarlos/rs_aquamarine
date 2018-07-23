using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Accounting_Application_System
{
    public partial class a_journalentry : Form
    {
        private String jrnl = "";
        private String prd = "";
        private DateTime systemdate;
        Boolean seltbp = false;
        private Boolean isnew = false;
        private Boolean is_ui_load = false; // true if unpaid invoices form load
        private int nxt_ln = 1;

        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();

        public a_journalentry()
        {
            InitializeComponent();

        }

        private void a_journalentry_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            //tbcntrl_extaattached.Hide(); // mot included in drs choice
            try
            {
                systemdate = Convert.ToDateTime(db.get_systemdate("").ToString());
                gc.load_openperiod(cbo_period,true);
                gc.load_journal(cbo_journal);
                gc.load_payee(cbo_payee);
                gc.load_branch(cbo_branch);
                gc.load_branch(cbo_jbranch);

                cbo_jbranch.SelectedValue = GlobalClass.branch;
                cbo_search.SelectedIndex = 0;

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

                enable_mainopt(false);
                //set_currentperiod();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            lbl_journal_id.Hide();
        }

        private void enable_mainopt(Boolean flag)
        {
            btn_new.Enabled = flag;
            btn_upd.Enabled = flag;
            btn_cancel.Enabled = flag;
            btn_print.Enabled = flag;

            cbo_form.Enabled = flag;
        }

        private void set_currentperiod()
        {
            String match = "";
            try
            {
                if (cbo_period.Items.Count > 0)
                {
                    for(int i = 0; cbo_period.Items.Count > 0; i++)
                    {
                        cbo_period.SelectedIndex = i;
                        match = cbo_period.SelectedValue.ToString();

                        if(match.Contains(DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString()))
                        {
                            cbo_period.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void disp_jrnl()
        {
            
        }

        private void goto_win1()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option;
            tpg_option.Show();

            tbcntrl_main.SelectedTab = tpg_right_list;
            tpg_right_list.Show();
            seltbp = false;
        }

        private void goto_win2()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_2;
            tpg_option_2.Show();

            tbcntrl_main.SelectedTab = tpg_right_entry;
            tpg_right_entry.Show();
            seltbp = false;
        }

        private void goto_win2_option()
        {
            tbcntrl_left.SelectedTab = tpg_option_2;
            tpg_option_2.Show();
        }

        private void goto_additem()
        {
            tbcntrl_main.SelectedTab = tpg_right_entry;
            tpg_right_entry.Show();
        }

        //display general journal, cash receipt journal, sales journal
        private void disp_gj()
        {
            pnl_chk_disbursement.Hide();
            //tbcntrl_extaattached.Hide();
        }

        //display check disbursement journal
        private void disp_cdj()
        {
            pnl_chk_disbursement.Show();
            //tbcntrl_extaattached.Show();

            //tbcntrl_extaattached.SelectedIndex = 1;
        }

        //display purchase journal
        private void disp_pj()
        {
            pnl_chk_disbursement.Show();
            //tbcntrl_extaattached.Show();

            //tbcntrl_extaattached.SelectedIndex = 0;
        }

        private void clear_form()
        {
            txt_refno.Text = "";
            dtp_dt.Value = systemdate;
            txt_desc.Text = "";
            rtxt_explanation.Text = "";

            cbo_payee.SelectedIndex = -1;
            cbo_payee.Text = "";
            txt_ckno.Text = "";
            dtp_ckdt.Value = systemdate;

            txt_jonum.Text = "";
            txt_prnum.Text = "";
            txt_ponum.Text = "";
            txt_sinum.Text = "";
            txt_drnum.Text = "";
            cbo_vocherpayable.SelectedIndex = -1;

            dgv_itemlist.Rows.Clear();

            txt_total_debit.Text = "0.00";
            txt_total_credit.Text = "0.00";
            txt_balance.Text = "0.00";

            cbo_branch.SelectedValue = GlobalClass.branch;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            String j_num = "", j_code = lbl_journal_id.Text;
            isnew = true;
            enable_mainopt(false);
            clear_form();

            nxt_ln = 1;

            j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");

            if (String.IsNullOrEmpty(j_num))
            {
                MessageBox.Show("The next journal number of " + j_code + " is invalid. Please recheck the journal number on the Master File.");
            }
            else
            {
                txt_refno.Text = j_num;

                disp_extaattached();
                goto_win2();
                enable_mainopt(true);
            }
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            String j_code = jrnl;
            String j_num;
            int rowno;

            try
            {
                enable_mainopt(false);
                clear_form();
                isnew = false;
                rowno = dgv_list.CurrentRow.Index;

                if (dgv_list[6, rowno].Value.ToString() != "Y")
                {
                    j_num = dgv_list[0, rowno].Value.ToString();

                    txt_refno.Text = j_num;
                    dtp_dt.Value = Convert.ToDateTime(dgv_list[1, rowno].Value.ToString());
                    txt_desc.Text = dgv_list[2, rowno].Value.ToString();

                    cbo_payee.SelectedValue = dgv_list[3, rowno].Value.ToString();
                    txt_ckno.Text = dgv_list[4, rowno].Value.ToString();

                    cbo_branch.SelectedValue = dgv_list[9, rowno].Value.ToString();

                    if (String.IsNullOrEmpty(dgv_list[5, rowno].Value.ToString()))
                    {
                        dtp_ckdt.Value = dtp_ckdt.MinDate;
                    }
                    else
                    {
                        dtp_ckdt.Value = Convert.ToDateTime(dgv_list[5, rowno].Value.ToString());
                    }
                    rtxt_explanation.Text = db.get_explanation(j_code, j_num);


                    txt_jonum.Text = dgv_list[10, rowno].Value.ToString();
                    txt_ponum.Text = dgv_list[11, rowno].Value.ToString();
                    txt_sinum.Text = dgv_list[12, rowno].Value.ToString();
                    txt_prnum.Text = dgv_list[13, rowno].Value.ToString();
                    txt_drnum.Text = dgv_list[14, rowno].Value.ToString();


                    disp_item_fromdb(j_code, j_num);


                    disp_extaattached();
                    disp_total_drcr_bal();
                    goto_win2();
                    
                    //nxt_ln = db.get_jrnl_lastseq_num(jrnl, txt_refno.Text);
                    try
                    {
                        nxt_ln = Int32.Parse(dgv_itemlist[0, dgv_itemlist.Rows.Count - 2].Value.ToString());
                        if (isnew == false)
                        {
                            nxt_ln += 1;
                        }
                    }
                    catch { }

                }
            }
           catch (Exception er){ MessageBox.Show(er.Message + "\nNo journal entry selected"); }
           enable_mainopt(true);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            String j_code, j_num, t_desc;

            try
            {
                j_code = jrnl;
                j_num = dgv_list[0, dgv_list.CurrentRow.Index].Value.ToString();
                t_desc = dgv_list[2, dgv_list.CurrentRow.Index].Value.ToString();

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel Ref. No " + j_num + "?", "Confirmation", MessageBoxButtons.YesNo);
                
                if (dialogResult == DialogResult.Yes)
                {
                    if (db.del_jrnl(j_code, j_num, t_desc))
                    {
                        db.del_jrnl_explanation(j_code, j_code);
                        db.del_jrnl_entry(j_code, j_num);

                        dgv_list.Rows.Clear();
                        disp_list_fromdb();
                    }
                    else
                    {
                        MessageBox.Show("No Journal Entry selected");
                    }
                }               
            }
            catch (Exception) { MessageBox.Show("No Journal Entry selected."); }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            DataTable dt = new DataTable();
            String j_code = "", jrnl_name = "", refno = "", ref_desc = "", yr = "", mo = "", t_date = "", paid_to = "", ck_no = "", ck_date = "", explanation;
            String[] p;
            int rowno;

            try
            {

                rowno = dgv_list.CurrentRow.Index;
                if (cbo_form.SelectedIndex == -1)
                {
                    MessageBox.Show("Select print form first.");
                }
                if (dgv_list.Rows.Count <= 1)
                {
                    MessageBox.Show("No journal entry selected");
                }
                else if (dgv_list[6, rowno].Value.ToString() == "Y")
                {
                    MessageBox.Show("Journal selected already cancelled. Invalid action.");
                }
                else
                {
                    jrnl_name = lbl_journal.Text;
                    j_code = jrnl;
                    p = cbo_period.SelectedValue.ToString().Split('-');
                    mo = p.GetValue(1).ToString();
                    yr = p.GetValue(0).ToString();

                    refno = dgv_list[0, rowno].Value.ToString();
                    t_date = dgv_list[1, rowno].Value.ToString();
                    ref_desc = dgv_list[2, rowno].Value.ToString();
                    paid_to = dgv_list[3, rowno].Value.ToString();
                    ck_no = dgv_list[4, rowno].Value.ToString();
                    ck_date = dgv_list[5, rowno].Value.ToString();
                    explanation = db.get_explanation(jrnl, refno);

                    //voucher and check
                    if (cbo_form.SelectedIndex == 0)
                    {
                        Report rpt_chk = new Report();

                        rpt_chk.print_check(j_code, refno, paid_to, ck_date);
                        
                        dt = db.get_journalentry_info_accntlist(jrnl, refno);
                        rpt.print_voucher(j_code, jrnl_name, refno, ref_desc, yr, mo, t_date, paid_to, ck_date, ck_no, explanation, dt);
                    
                        rpt.Show();
                        rpt_chk.Show();
                    }
                    //voucher only
                    else if (cbo_form.SelectedIndex == 1)
                    {
                        dt = db.get_journalentry_info_accntlist(jrnl, refno);
                        rpt.print_voucher(j_code, jrnl_name, refno, ref_desc, yr, mo, t_date, paid_to, ck_date, ck_no, explanation, dt);
                        rpt.Show();
                    }
                    //check only
                    else if (cbo_form.SelectedIndex == 2)
                    {
                        //Double amt = db.get_paidtoamount(j_code, refno);
                        //Print_Check pchk = new Print_Check(paid_to, amt, gm.toDateValue(ck_date));
                        Report rpt_chk = new Report();

                        rpt_chk.print_check(j_code, refno, paid_to, ck_date);
                        rpt_chk.Show();
                    }
                }
            }
            catch (Exception) { MessageBox.Show("No journal entry selected."); }
        }

        public void set_nextln(int ln)
        {
            ln = ln+1;
            nxt_ln = ln;
        }

        private void btn_itemadd_Click(object sender, EventArgs e)
        {
            z_journalentry prmpt = new z_journalentry(this, true, jrnl, txt_refno.Text, gm.toAccountingFormat(txt_balance.Text), nxt_ln);
            prmpt.ShowDialog();
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            String bal = "0.00";

            z_journalentry prmpt = new z_journalentry(this, false, jrnl, txt_refno.Text, gm.toAccountingFormat(txt_balance.Text), 0);
            int r;

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    prmpt.set_txt_line(dgv_itemlist[0, r].Value.ToString()); //seq_num
                    prmpt.set_cbo_accttitle_value(dgv_itemlist[10, r].Value.ToString()); //at_code
                    prmpt.set_cbo_subdiaryname_value(dgv_itemlist[11, r].Value.ToString(), dgv_itemlist[10, r].Value.ToString()); //sl_code
                    prmpt.set_txt_debit(gc.toNormalDoubleFormat(dgv_itemlist[2, r].Value.ToString())); //debit
                    prmpt.set_txt_credit(gc.toNormalDoubleFormat(dgv_itemlist[3, r].Value.ToString())); //credit
                    prmpt.set_txt_invoice_no(dgv_itemlist[4, r].Value.ToString()); //invoice
                    prmpt.set_cbo_costcenter(dgv_itemlist[12, r].Value.ToString());  //cc_code
                    prmpt.set_cbo_terms(dgv_itemlist[7, r].Value.ToString()); //pay_code
                    prmpt.set_cbo_salesperson(dgv_itemlist[8, r].Value.ToString()); //rep_code
                    prmpt.set_rtxt_notes(dgv_itemlist[9, r].Value.ToString()); //seq_desc 

                    prmpt.ShowDialog();
                }
            }
            catch (Exception) { MessageBox.Show("No account title selected."); }
        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dgv_itemlist.CurrentRow.Index;

                if (isnew)
                {
                    dgv_itemlist.Rows.RemoveAt(row);
                }
                else
                {
                    dgv_itemlist.Rows.RemoveAt(row);
                }
                disp_total_drcr_bal();
            }
            catch (Exception) { MessageBox.Show("No account title selected."); }
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String prd = cbo_period.SelectedValue.ToString();
            String fy, mo;
            String[] p;
            String j_code, j_num, jtype_name, seq_num, at_code, sl_code, sl_name, cc_code, debit, credit, invoice, seq_desc, rep_code, pay_code, or_code, or_lne, jo_num, purc_ord,  inv_num, pr_code, dr_code, branch;
            String invoicedb = "";
            Boolean success = false;

            p = prd.Split('-');
            mo = p.GetValue(1).ToString();
            fy = p.GetValue(0).ToString();

            j_code = jrnl;
            j_num = txt_refno.Text;
            jtype_name = db.get_jtypename(jrnl);

            if (gc.toNormalDoubleFormat(txt_balance.Text) != 0.00)
            {
                MessageBox.Show("Balance should be zero.");
            }
            else if(String.IsNullOrEmpty(txt_refno.Text))
            {
                MessageBox.Show("Reference Number is required.");
            }
            else if (String.IsNullOrEmpty(txt_desc.Text))
            {
                MessageBox.Show("Description is required.");
            }
            else if (dgv_itemlist.Rows.Count <= 0)
            {
                MessageBox.Show("Journal entries must be entered.");
            }
            else if ((jtype_name == "Disbursement") /* && (j_code == "CDJ" || j_code == "CV" || j_code == "CV1") */ && String.IsNullOrEmpty(cbo_payee.Text) == true)
            {
                MessageBox.Show("\"Paid To\" field is required.");
                cbo_payee.DroppedDown = true;
            }
            else if (dgv_itemlist.Rows.Count <= 1)
            {
                MessageBox.Show("Invalid journal entries.");
            }
            else
            {
                //jo_num OR, purc_ord UR,  inv_num, pr_code AR, dr_code DR,
                //txt_jonum txt_ponum txt_sinum txt_prnum txt_drnum

                branch = (cbo_branch.SelectedValue ?? "").ToString();
                jo_num = txt_jonum.Text;
                purc_ord = txt_ponum.Text;
                inv_num = txt_sinum.Text;
                pr_code = txt_prnum.Text;
                dr_code = txt_drnum.Text;
                //code here
                if (isnew)
                {
                    if (db.add_jrnl(fy, mo, j_code, j_num, txt_desc.Text, cbo_payee.Text, txt_ckno.Text, null, dtp_dt.Value.ToString("yyyy-MM-dd"), dtp_ckdt.Value.ToString("yyyy-MM-dd")))
                    {
                        db.UpdateOnTable("tr01", "branch='" + branch + "', jo_code='" + jo_num + "', purc_ord='" + purc_ord + "',  inv_num='" + inv_num + "', pr_code='" + pr_code + "', dr_code='" + dr_code + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                        if (String.IsNullOrEmpty(rtxt_explanation.Text) == false)
                        {
                            db.add_jrnl_explanation(jrnl, txt_refno.Text, rtxt_explanation.Text);
                        }

                        try
                        {
                            for (int i = 0; i < dgv_itemlist.Rows.Count; i++)
                            {
                                seq_num = dgv_itemlist[0, i].Value.ToString();
                                at_code = dgv_itemlist[10, i].Value.ToString();   
                                sl_code = dgv_itemlist[11, i].Value.ToString();
                                sl_name = dgv_itemlist[5, i].Value.ToString();
                                cc_code = dgv_itemlist[12, i].Value.ToString();
                                debit = dgv_itemlist[2, i].Value.ToString();
                                credit = dgv_itemlist[3, i].Value.ToString();
                                invoice = dgv_itemlist[4, i].Value.ToString();
                                seq_desc = dgv_itemlist[9, i].Value.ToString();
                                rep_code = dgv_itemlist[8, i].Value.ToString();
                                pay_code = dgv_itemlist[7, i].Value.ToString();

                                if (String.IsNullOrEmpty(invoice) == false)
                                {
                                    db.upd_unpaid_invoices(j_code, sl_code, invoice, Convert.ToDouble(debit), Convert.ToDouble(credit));
                                }

                                db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit, credit, invoice, seq_desc, rep_code, pay_code, null, null);
                            }

                        }
                        catch (Exception) { }

                        db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");

                        MessageBox.Show("Journal inserted successfully.");
                    }
                }
                else
                {
                    if (db.upd_jrnl(fy, mo, j_code, j_num, txt_desc.Text, cbo_payee.Text, txt_ckno.Text, null, dtp_dt.Value.ToString("yyyy-MM-dd"), dtp_ckdt.Value.ToString("yyyy-MM-dd")))
                    {
                        db.UpdateOnTable("tr01", "branch='" + branch + "', jo_code='" + jo_num + "', purc_ord='" + purc_ord + "',  inv_num='" + inv_num + "', pr_code='" + pr_code + "', dr_code='" + dr_code + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");

                        if (db.isExists("tr03", "j_code = '" + j_code + "' AND j_num = '" + j_num + "'"))
                        {
                            db.upd_jrnl_explanation(j_code, j_num, rtxt_explanation.Text);
                        }
                        else
                        {
                            db.add_jrnl_explanation(jrnl, txt_refno.Text, rtxt_explanation.Text);
                        }

                        //add all journal entries and
                        try
                        {
                            db.del_jrnl_entry(j_code, j_num);

                            for (int i = 0; i < dgv_itemlist.Rows.Count; i++)
                            {
                                seq_num = dgv_itemlist[0, i].Value.ToString();
                                at_code = dgv_itemlist[10, i].Value.ToString();
                                sl_code = dgv_itemlist[11, i].Value.ToString();
                                sl_name = dgv_itemlist[5, i].Value.ToString();
                                cc_code = dgv_itemlist[12, i].Value.ToString();
                                debit = dgv_itemlist[2, i].Value.ToString();
                                credit = dgv_itemlist[3, i].Value.ToString();
                                invoice = dgv_itemlist[4, i].Value.ToString();
                                seq_desc = dgv_itemlist[9, i].Value.ToString();
                                rep_code = dgv_itemlist[8, i].Value.ToString();
                                pay_code = dgv_itemlist[7, i].Value.ToString();

                                if (String.IsNullOrEmpty(invoice) == false)
                                {
                                    if ((jtype_name == "Disbursement") /*&& (j_code == "CDJ" || j_code == "CV" || j_code == "CV1")*/ && String.IsNullOrEmpty(invoice) == false)
                                    {
                                        db.upd_unpaid_invoices(j_code, sl_code, invoice, Convert.ToDouble(debit), Convert.ToDouble(credit));
                                    }
                                    else if ((jtype_name == "Collection") && /* j_code == "CRJ" && */ String.IsNullOrEmpty(invoice) == false)
                                    {
                                        db.upd_unpaid_invoices(j_code, sl_code, invoice, Convert.ToDouble(debit), Convert.ToDouble(credit));
                                    }
                                }

                                db.add_jrnl_entry(j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, null, debit, credit, invoice, seq_desc, rep_code, pay_code, null, null);
                            }

                            success = true;
                        }
                        catch (Exception) { }

                        MessageBox.Show("Journal updated successfully.");
                    }
                }
                disp_list_fromdb();
                goto_win1();
                isnew = false;
            }
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {

            goto_win1();
            clear_form();
            //code here
            isnew = false;
        }

        private void disp_total_drcr_bal()
        {
            Double total_dr = 0.00;
            Double total_cr = 0.00;

            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count; i++)
                {
                    total_dr += gc.toNormalDoubleFormat(dgv_itemlist[2, i].Value.ToString());
                    total_cr += gc.toNormalDoubleFormat(dgv_itemlist[3, i].Value.ToString());
                }
            }
            catch (Exception) {  }

            txt_total_debit.Text = gc.toAccountingFormat(total_dr);
            txt_total_credit.Text = gc.toAccountingFormat(total_cr);
            txt_balance.Text = gc.toAccountingFormat((total_dr - total_cr));
        }

        private void bck_option_search_Click(object sender, EventArgs e)
        {
            goto_win2_option();
        }

        private void cbo_journal_SelectedIndexChanged(object sender, EventArgs e)
        {
            String mo = "";
            String fy = "";
            String[] p;

            dgv_list.Rows.Clear();
            
            try
            {
                lbl_period.Text = cbo_period.Text.ToString();
                lbl_journal.Text = cbo_journal.Text.ToString();
                lbl_journal_id.Text = cbo_journal.SelectedValue.ToString();
                jrnl = lbl_journal_id.Text;

                disp_list_fromdb();
                enable_mainopt(true);
            }
            catch (Exception) { }
        }

        private void disp_list_fromdb()
        {
            DataTable dt = new DataTable();
            int i;

            try
            {
                dgv_list.Rows.Clear();
            }
            catch (Exception) { }

            try
            {
                if (cbo_period.SelectedIndex > -1)
                {
                    String prd = cbo_period.SelectedValue.ToString();

                    String[] p = prd.Split('-');
                    String mo = p.GetValue(1).ToString();
                    String fy = p.GetValue(0).ToString();


                    if (cbo_search.SelectedIndex == 0)
                    {
                        dt = db.get_journalentrylist(fy, mo, jrnl, (cbo_jbranch.SelectedValue ?? "").ToString(), true);
                    }
                    else if (cbo_search.SelectedIndex == 1)
                    {
                        dt = db.get_journalentrylist_bysubsidiary(fy, mo, jrnl, txt_search_jnum.Text, true);
                    }

                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        DataGridViewRow row = (DataGridViewRow)dgv_list.Rows[0].Clone();

                        row.Cells[0].Value = dt.Rows[i][0].ToString();
                        row.Cells[1].Value = Convert.ToDateTime(dt.Rows[i][1].ToString()).ToString("yyyy-MM-dd");
                        row.Cells[2].Value = dt.Rows[i][2].ToString();
                        row.Cells[3].Value = dt.Rows[i][3].ToString();
                        row.Cells[4].Value = dt.Rows[i][4].ToString();

                        if (String.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                            row.Cells[5].Value = "";
                        else
                            row.Cells[5].Value = Convert.ToDateTime(dt.Rows[i][5].ToString()).ToString("yyyy-MM-dd");

                        row.Cells[6].Value = dt.Rows[i][6].ToString();
                        row.Cells[7].Value = dt.Rows[i][7].ToString();


                        row.Cells[9].Value = dt.Rows[i][8].ToString();
                        cbo_branch.SelectedValue = dt.Rows[i][8].ToString();
                        row.Cells[8].Value = cbo_branch.Text;

                        row.Cells[10].Value = dt.Rows[i][9].ToString();
                        row.Cells[11].Value = dt.Rows[i][10].ToString();
                        row.Cells[12].Value = dt.Rows[i][11].ToString();
                        row.Cells[13].Value = dt.Rows[i][12].ToString();
                        row.Cells[14].Value = dt.Rows[i][13].ToString();

                        dgv_list.Rows.Add(row);
                    }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }        
        }

        private void disp_item_fromdb(String j_code, String j_num)
        {
            DataTable dt = new DataTable();
            int i;

            dt = db.get_journalentry_info_accntlist(j_code, j_num);
            
            for(i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dgv_itemlist.Rows[0].Clone();
                row.Cells[0].Value = dt.Rows[i]["seq_num"].ToString();
                row.Cells[1].Value = dt.Rows[i]["at_desc"].ToString();
                
                if (String.IsNullOrEmpty(dt.Rows[i]["debit"].ToString()) == false)
                {
                    row.Cells[2].Value = gc.toAccountingFormat(gc.toNormalDoubleFormat(dt.Rows[i]["debit"].ToString()));
                }
                else
                {
                    row.Cells[2].Value = "0.00";
                }
                if (String.IsNullOrEmpty(dt.Rows[i]["credit"].ToString()) == false)
                {
                    row.Cells[3].Value = gc.toAccountingFormat(gc.toNormalDoubleFormat(dt.Rows[i]["credit"].ToString()));
                }
                else
                {
                    row.Cells[3].Value = "0.00";
                }
                row.Cells[4].Value = dt.Rows[i]["invoice"].ToString();
                row.Cells[5].Value = dt.Rows[i]["sl_name"].ToString();
                row.Cells[6].Value = dt.Rows[i]["cc_desc"].ToString();
                row.Cells[7].Value = dt.Rows[i]["pay_code"].ToString();
                row.Cells[8].Value = dt.Rows[i]["rep_code"].ToString();
                row.Cells[9].Value = dt.Rows[i]["seq_desc"].ToString();
                row.Cells[10].Value = dt.Rows[i]["at_code"].ToString();
                row.Cells[11].Value = dt.Rows[i]["sl_code"].ToString();
                row.Cells[12].Value = dt.Rows[i]["cc_code"].ToString();

                dgv_itemlist.Rows.Add(row);
            }            
        }

        public void set_item(DataTable dt)
        {
            int seq_num = dgv_itemlist.Rows.Count;
            String j_code = "", sl_code = "", invoice = "", debit = "", credit = ""; 

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv_itemlist.Rows[0].Clone();
                    row.Cells[0].Value = seq_num; //dt.Rows[i][0].ToString(); //seq_num
                    row.Cells[1].Value = dt.Rows[i]["at_desc"].ToString(); //at_desc
                    row.Cells[2].Value = dt.Rows[i]["debit"].ToString(); //debit
                    row.Cells[3].Value = dt.Rows[i]["credit"].ToString(); //credit
                    row.Cells[4].Value = dt.Rows[i]["invoice"].ToString(); //invoice
                    row.Cells[5].Value = dt.Rows[i]["sl_name"].ToString(); //sl_name
                    row.Cells[6].Value = dt.Rows[i]["cc_desc"].ToString(); //cc_desc
                    row.Cells[7].Value = dt.Rows[i]["pay_code"].ToString(); //pay_code
                    row.Cells[8].Value = dt.Rows[i]["rep_code"].ToString(); //rep_code
                    row.Cells[9].Value = dt.Rows[i]["seq_desc"].ToString(); //seq_desc
                    row.Cells[10].Value = dt.Rows[i]["at_code"].ToString(); //at_code
                    row.Cells[11].Value = dt.Rows[i]["sl_code"].ToString(); //sl_code
                    row.Cells[12].Value = dt.Rows[i]["cc_code"].ToString(); //cc_code

                    j_code = jrnl;
                    sl_code = dt.Rows[i]["sl_code"].ToString();
                    invoice = dt.Rows[i]["invoice"].ToString();
                    debit = dt.Rows[i]["debit"].ToString();
                    credit = dt.Rows[i]["credit"].ToString();

                    dgv_itemlist.Rows.Add(row);
                    set_nextln(nxt_ln);

                    seq_num++;
                }

                if (String.IsNullOrEmpty(invoice) == false)
                {
                    db.upd_unpaid_invoices(j_code, sl_code, invoice, Convert.ToDouble(debit), Convert.ToDouble(credit));
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }


            disp_total_drcr_bal();
            goto_win2_option();
        }

        public DataTable get_je_dgv(String at_code, String sl_code)
        {
            DataTable dt = new DataTable();
            String l_invoice = "", l_sl_code = "", l_at_code = "", l_debit = "0.00", l_credit = "0.00";
            int i;

            try
            {                
                dt.Columns.Add("invoice", typeof(String));
                dt.Columns.Add("debit", typeof(String));
                dt.Columns.Add("credit", typeof(String));

                for(i=0; i < dgv_itemlist.Rows.Count - 1; i++)
                {
                    l_invoice = dgv_itemlist["invoice", i].Value.ToString();
                    l_at_code = dgv_itemlist["at_code", i].Value.ToString();
                    l_sl_code = dgv_itemlist["sl_code", i].Value.ToString();

                    if (String.IsNullOrEmpty(l_invoice) == false && at_code == l_at_code && sl_code == l_sl_code)
                    {
                        if (String.IsNullOrEmpty(dgv_itemlist["debit", i].Value.ToString()) == false)
                        {
                            l_debit = dgv_itemlist["debit", i].Value.ToString();
                        }
                        if (String.IsNullOrEmpty(dgv_itemlist["credit", i].Value.ToString()) == false)
                        {
                            l_credit = dgv_itemlist["credit", i].Value.ToString();
                        }

                        dt.Rows.Add(l_invoice, l_debit, l_credit);
                    }

                    l_invoice = ""; l_sl_code = ""; l_at_code = ""; l_debit = "0.00"; l_credit = "0.00";
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            return dt;
        }

        public void add_je(String ln, String attitle, String dr, String cr, String inv, String slname, String ccname, String paycode,
                            String clerk, String notes, String at_code, String sl_code, String cc_code)
        {
            DataGridViewRow row = (DataGridViewRow)dgv_itemlist.Rows[0].Clone();
                       
            row.Cells[0].Value = ln;
            row.Cells[1].Value = attitle;
            row.Cells[2].Value = dr;
            row.Cells[3].Value = cr;
            row.Cells[4].Value = inv;
            row.Cells[5].Value = slname; // subsidiary
            row.Cells[6].Value = ccname; //cost center
            row.Cells[7].Value = paycode; //payment
            row.Cells[8].Value = clerk; // sales clerk
            row.Cells[9].Value = notes;
            row.Cells[10].Value = at_code;
            row.Cells[11].Value = sl_code;
            row.Cells[12].Value = cc_code; // cc_code
            
            dgv_itemlist.Rows.Add(row);

            disp_total_drcr_bal();
        }

        public void upd_je(String ln, String attitle, String dr, String cr, String inv, String slname, String ccname, String paycode,
                            String clerk, String notes, String at_code, String sl_code, String cc_code)
        {
            int dgv_RowNo = -1;
            
            try
            {
                dgv_RowNo = dgv_itemlist.CurrentRow.Index;

                dgv_itemlist[0, dgv_RowNo].Value = ln;
                dgv_itemlist[1, dgv_RowNo].Value = attitle;
                dgv_itemlist[2, dgv_RowNo].Value = dr;
                dgv_itemlist[3, dgv_RowNo].Value = cr;
                dgv_itemlist[4, dgv_RowNo].Value = inv;
                dgv_itemlist[5, dgv_RowNo].Value = slname; // subsidiary
                dgv_itemlist[6, dgv_RowNo].Value = ccname; //cost center
                dgv_itemlist[7, dgv_RowNo].Value = paycode; //payment
                dgv_itemlist[8, dgv_RowNo].Value = clerk; // sales clerk
                dgv_itemlist[9, dgv_RowNo].Value = notes;
                dgv_itemlist[10, dgv_RowNo].Value = at_code;
                dgv_itemlist[11, dgv_RowNo].Value = sl_code;
                dgv_itemlist[12, dgv_RowNo].Value = cc_code; // cc_code
            }
            catch (Exception) { }

            disp_total_drcr_bal();
        }        

        private void cbo_period_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        public void set_uiload(Boolean flag)
        {
            is_ui_load = true;
        }

        private void txt_search_jnum_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_left_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
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

        private void dgv_itemlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void cbo_jbranch_SelectedIndexChanged(object sender, EventArgs e)
        {

            String mo = "";
            String fy = "";
            String[] p;

            dgv_list.Rows.Clear();

            try
            {
                lbl_period.Text = cbo_period.Text.ToString();
                lbl_journal.Text = cbo_journal.Text.ToString();
                lbl_journal_id.Text = cbo_journal.SelectedValue.ToString();
                jrnl = lbl_journal_id.Text;

                disp_list_fromdb();
                enable_mainopt(true);
            }
            catch (Exception) { }
        }

        private void cbo_period_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbo_period.SelectedIndex != -1)
            {
                cbo_journal.DroppedDown = true;
            }
        }
        public void disp_extaattached()
        {
            tbcntrl_extaattached.Show();
            /*
            String jtype_name = db.get_jtypename(jrnl);
            if (jtype_name == "Disbursement")
            {
                tbcntrl_extaattached.Visible = true;
                tbcntrl_extaattached.SelectedTab = tpb_chk;
                tpb_chk.Show();
            }
            else if (jtype_name == "Purchase" || jtype_name == "Collection")
            {
                tbcntrl_extaattached.Visible = true;
                tbcntrl_extaattached.SelectedTab = tpg_ref;
                tpg_ref.Show();
            }*/
        }
        private void tbcntrl_extaattached_Selecting(object sender, TabControlCancelEventArgs e)
        {
            /*
            String jtype_name = db.get_jtypename(jrnl);
            if (jtype_name == "Disbursement")
            {
                if (tbcntrl_extaattached.SelectedTab != tpb_chk && tbcntrl_extaattached.SelectedTab != tpg_voucher)
                {
                    e.Cancel = true;
                }
            }
            else if (jtype_name == "Purchase" || jtype_name == "Collection")
            {
                if (tbcntrl_extaattached.SelectedTab != tpg_ref)
                {
                    e.Cancel = true;
                }
            }*/
        }

        // journal import excel
        private void goto_win3()
        {
            seltbp = true;
            tbcntrl_left.SelectedTab = tpg_option_3;
            tpg_option_3.Show();

            tbcntrl_main.SelectedTab = tpg_right_import;
            tpg_right_import.Show();
            seltbp = false;
        }
        private void btn_back_Click(object sender, EventArgs e)
        {
            tpg_right_import.Text = "Import Excel";
            goto_win1();
        }

        private void btn_goimport_Click(object sender, EventArgs e)
        {

            if (cbo_journal.SelectedIndex != -1)
            {
                openFile.FileName = "";
                btn_import.Enabled = false;
                txt_filename.Text = ".";

                gc.load_journal(cbo_import_journal);
                cbo_import_journal.SelectedValue = cbo_journal.SelectedValue;
                lbl_upl_desc.Text = cbo_import_journal.Text + " to Upload : ";
                tpg_right_import.Text = "Import Excel - " + cbo_import_journal.Text;
                gc.load_openperiod(cbo_import_period);
                cbo_import_period.Enabled = false;
                cbo_import.SelectedIndex = -1;
                goto_win3();
            }
            else
            {
                MessageBox.Show("Select Journal Type first.");
                cbo_journal.DroppedDown = true;
            }
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            if (cbo_import.SelectedIndex == -1)
            {
                MessageBox.Show("Please select import option.");
                cbo_import.DroppedDown = true;
            }
            else if ((db.get_jtypename(cbo_import_journal.SelectedValue.ToString()) == "General" && cbo_import.SelectedIndex != 0) || (db.get_jtypename(cbo_import_journal.SelectedValue.ToString()) != "General" && cbo_import.SelectedIndex == 0))
            {
                MessageBox.Show("Beginning of Balance for General Journal Only.");
                cbo_import_journal.DroppedDown = true;
            }
            else if (cbo_import.SelectedIndex == 0 && cbo_import_period.SelectedIndex == -1)
            {
                MessageBox.Show("Please select period.");
                cbo_import_period.DroppedDown = true;
            }
            else if (txt_filename.Text == "." && txt_filename.Text != openFile.FileName)
            {
                MessageBox.Show("Please select and excel file to import.");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to import this excel to " + cbo_import.Text + "?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    importBgWorker.RunWorkerAsync();
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_filename.Text = openFile.FileName;
                btn_import.Enabled = true;
            }
            else
            {
                btn_import.Enabled = false;
            }
        }

        private void importBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            Boolean success = false,chk = false;
            String str = "";
            int rCnt = 0;
            int cCnt = 0;
            int rw = 0, trw = 0;
            int cl = 0;
            String filename = "";
            int i = 0;
            int count = 0;

            DateTime sysdt = DateTime.Parse(db.get_systemdate("") + " " + DateTime.Now.ToString("HH:mm tt"));
            String refno = "", period = "", paidto = "", refdesc = "", refdate = "", chkno = "", chkdate = "";
            List<dynamic> list = new List<dynamic>();
            DateTime tmpdt = DateTime.Now;

            String credit = "", debit = "", code = "", acctid = "", acctdesc = "", dr_cr = "";
            Double balance, dbldebit, dblcredit;

            String j_code = get_cbo_value(cbo_import_journal);
            String j_num = "";
            int j_num_len = 0;
            int import_selected = get_cbo_index(cbo_import);
            int mo = 0, fy = 0;
            String _mo = "",_fy = "";
            if (import_selected == 0)
            {
                String[] splt = get_cbo_value(cbo_import_period).Split('-');
                _fy = splt.GetValue(0).ToString().Trim();
                _mo = splt.GetValue(1).ToString().Trim();
                DataTable dts = db.QueryBySQLCode("SELECT x03.to FROM rssys.x03 WHERE fy='" + _fy + "' AND mo='" + _mo + "'");
                tmpdt = DateTime.Parse(dts.Rows[i]["to"].ToString());
            }

            try
            {

                filename = openFile.FileName;
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(@filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;

                if (rw > 0)
                {
                    btnBrowse.Invoke(new Action(() =>
                    {
                        btnBrowse.Enabled = false;
                    }));
                    btn_import.Invoke(new Action(() =>
                    {
                        btn_import.Enabled = false;
                    }));
                    lbl_max.Invoke(new Action(() =>
                    {
                        lbl_max.Text = (rw - 1).ToString();
                    }));
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Maximum = rw;
                    }));
                    lbl_min.Invoke(new Action(() =>
                    {
                        lbl_min.Text = pbar.Minimum.ToString();
                    }));

                    cbo_import.Invoke(new Action(() => {
                        cbo_import.Enabled = false;
                        cbo_import_journal.Enabled = false;
                        cbo_import_period.Enabled = false;
                    }));



                    trw = rw - 1;
                }
                /*if (import_selected == 0)
                {
                    for (rCnt = 1; rCnt <= rw; rCnt++)
                    {
                        Boolean hasval = false;
                        for (int j = 1; j < 4 && !hasval; j++)
                            if (!String.IsNullOrEmpty(getString(range, rCnt, j)))
                                hasval = true;

                        if (hasval)
                        {
                            String option = getString(range, rCnt, 0).ToLower();
                            String option2 = getString(range, rCnt, 1).ToLower();
                            if (option.IndexOf("acct id") >= 0 || option.IndexOf("acct") >= 0 || option.IndexOf("account") >= 0 || option.IndexOf("account no") >= 0 || option.IndexOf("account code") >= 0 || option.IndexOf("no") >= 0 || option.IndexOf("code") >= 0 || option2.IndexOf("acct id") >= 0 || option2.IndexOf("acct") >= 0 || option2.IndexOf("account") >= 0 || option2.IndexOf("account no") >= 0 || option2.IndexOf("account code") >= 0 || option2.IndexOf("no") >= 0 || option2.IndexOf("code") >= 0)
                            {
                                success = false;
                            }
                            else
                            {
                                success = true;
                            }

                            if (success)
                            {
                                acctid = getString(range, rCnt, 1);
                                acctdesc = getString(range, rCnt, 2);
                                balance = gm.toNormalDoubleFormat(getString(range, rCnt, 3));
                                if (balance != 0)
                                {
                                    dr_cr = "";
                                    if (String.IsNullOrEmpty(acctid))
                                    {
                                        acctid = db.get_colval("m04", "at_code", "UPPER(at_desc)=" + db.str_E(acctdesc.ToUpper()) + "");
                                    }
                                    dr_cr = db.get_colval("m04", "dr_cr", "at_code='" + acctid + "'");

                                    if (!String.IsNullOrEmpty(dr_cr))
                                    {
                                        if (dr_cr == "D" && balance < 0)
                                        {
                                            balance = (-1 * balance);
                                            dr_cr = "C";
                                        }
                                        else if (dr_cr == "C" && balance < 0)
                                        {
                                            balance = (-1 * balance);
                                            dr_cr = "D";
                                        }

                                        credit = "0.00"; debit = "0.00";
                                        if (dr_cr == "D")
                                        {
                                            debit = balance.ToString("0.00");
                                        }
                                        else if (dr_cr == "C")
                                        {
                                            credit = balance.ToString("0.00");
                                        }   
                                    }
                                    else
                                    { 
                                        acctid = "LINE" + rCnt;
                                        chk = false;
                                        do{
                                            if (String.IsNullOrEmpty(db.get_colval("m04", "at_code", "at_code='" + acctid + "'"))) chk = true;
                                            else acctid = getInc(acctid);
                                        } while (!chk);

                                        credit = "0.00";
                                        debit = "0.00";
                                        if (balance < 0)
                                        {
                                            dr_cr = "C";
                                            credit = balance.ToString("0.00");
                                        }
                                        else 
                                        {
                                            dr_cr = "D";
                                            debit = balance.ToString("0.00");
                                        } 
                                        String col = "at_code,at_desc,dr_cr,bs_pl,sl,cib_acct,acc_code,payment";
                                        String val = "'" + acctid + "'," + db.str_E(acctdesc) + ",'" + dr_cr + "', 'B','N','','','N'";
                                        db.InsertOnTable("m04", col, val);
                                    }

                                    try
                                    {
                                        if (String.IsNullOrEmpty(j_num))
                                        {
                                            j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                                        }

                                        code = db.get_colval("tr01", "j_num", "j_code='" + j_code + "' AND j_num='" + j_num + "'");
                                        if (String.IsNullOrEmpty(code))
                                        {
                                            tmpdt = DateTime.Parse(get_cbo_value(cbo_import_period) + "-01");
                                            if (db.add_jrnl(tmpdt.ToString("yyyy"), tmpdt.ToString("MM"), j_code, j_num, System.IO.Path.GetFileName(filename), "", "", null, sysdt.ToString("yyyy-MM-dd"), sysdt.ToString("yyyy-MM-dd")))
                                            {
                                                db.UpdateOnTable("tr01", "branch='" + (GlobalClass.branch) + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                                db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");
                                            }
                                        }

                                        if (db.add_jrnl_entry(j_code, j_num, (count + 1).ToString(), acctid, "", "", "", null, debit, credit, "", null, null, null, null, null))
                                        {
                                            count++;
                                            if (success)
                                            {
                                                inc_pbar(count, rw);
                                                lbl_min.Invoke(new Action(() =>
                                                {
                                                    lbl_min.Text = count.ToString();
                                                }));
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }*/
                if (import_selected == 0)
                {
                    for (rCnt = 1; rCnt <= rw; rCnt++)
                    {
                        Boolean hasval = false;
                        for (int j = 1; j < 4 && !hasval; j++)
                            if (!String.IsNullOrEmpty(getString(range, rCnt, j)))
                                hasval = true;

                        if (hasval)
                        {
                            String option = getString(range, rCnt, 0).ToLower();
                            String option2 = getString(range, rCnt, 1).ToLower();
                            if (option.IndexOf("acct id") >= 0 || option.IndexOf("acct") >= 0 || option.IndexOf("account") >= 0 || option.IndexOf("account no") >= 0 || option.IndexOf("account code") >= 0 || option.IndexOf("no") >= 0 || option.IndexOf("code") >= 0 || option2.IndexOf("acct id") >= 0 || option2.IndexOf("acct") >= 0 || option2.IndexOf("account") >= 0 || option2.IndexOf("account no") >= 0 || option2.IndexOf("account code") >= 0 || option2.IndexOf("no") >= 0 || option2.IndexOf("code") >= 0)
                            {
                                success = false;
                            }
                            else
                            {
                                success = true;
                            }

                            if (success)
                            {
                                acctid = getString(range, rCnt, 1);
                                acctdesc = getString(range, rCnt, 2);
                                dbldebit = gm.toNormalDoubleFormat(getString(range, rCnt, 3));
                                dblcredit = gm.toNormalDoubleFormat(getString(range, rCnt, 4));
                                if (dbldebit != 0 || dblcredit != 0)
                                {
                                    dr_cr = "";
                                    if (String.IsNullOrEmpty(acctid))
                                    {
                                        acctid = db.get_colval("m04", "at_code", "UPPER(at_desc)=" + db.str_E(acctdesc.ToUpper()) + "");
                                    }
                                    dr_cr = db.get_colval("m04", "dr_cr", "at_code='" + acctid + "'");

                                    if (!String.IsNullOrEmpty(dr_cr))
                                    {
                                        debit = dbldebit.ToString("0.00");
                                        credit = dblcredit.ToString("0.00");
                                    }
                                    else
                                    {
                                        acctid = "LINE" + rCnt;
                                        chk = false;
                                        do
                                        {
                                            if (String.IsNullOrEmpty(db.get_colval("m04", "at_code", "at_code='" + acctid + "'"))) chk = true;
                                            else acctid = getInc(acctid);
                                        } while (!chk);

                                        if ((dbldebit - dblcredit) < 0)
                                        {
                                            dr_cr = "C";
                                        }
                                        else
                                        {
                                            dr_cr = "D";
                                        }

                                        credit = dblcredit.ToString("0.00");
                                        debit = dbldebit.ToString("0.00");

                                        String col = "at_code,at_desc,dr_cr,bs_pl,sl,cib_acct,acc_code,payment";
                                        String val = "'" + acctid + "'," + db.str_E(acctdesc) + ",'" + dr_cr + "', 'B','N','','','N'";
                                        db.InsertOnTable("m04", col, val);
                                    }

                                    try
                                    {
                                        if (String.IsNullOrEmpty(j_num))
                                        {
                                            j_num = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                                        }

                                        code = db.get_colval("tr01", "j_num", "j_code='" + j_code + "' AND j_num='" + j_num + "'");
                                        if (String.IsNullOrEmpty(code))
                                        {

                                            if (db.add_jrnl(_fy, _mo, j_code, j_num, System.IO.Path.GetFileName(filename), "", "", null, tmpdt.ToString("yyyy-MM-dd"), tmpdt.ToString("yyyy-MM-dd")))
                                            {
                                                db.UpdateOnTable("tr01", "branch='" + (GlobalClass.branch) + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                                db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");
                                            }
                                        }

                                        if (db.add_jrnl_entry(j_code, j_num, (count + 1).ToString(), acctid, "", "", "", null, debit, credit, "", null, null, null, null, null))
                                        {
                                            count++;
                                            if (success)
                                            {
                                                inc_pbar(count, rw);
                                                lbl_min.Invoke(new Action(() =>
                                                {
                                                    lbl_min.Text = count.ToString();
                                                }));
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
                else if (import_selected == 1)
                {
                    for (rCnt = 1; rCnt <= rw; rCnt++)
                    {

                        Boolean hasval = false;
                        for (int j = 1; j < 25 && !hasval; j++)
                            if (!String.IsNullOrEmpty(getString(range, rCnt, j)))
                                hasval = true;
                        
                        if (hasval)
                        {
                            String option = getString(range, rCnt, 1).ToLower();

                            if (String.IsNullOrEmpty(option) || option.IndexOf("acct id") >= 0 || option.IndexOf("acct") >= 0)
                            {
                                success = false;
                            }
                            else
                            {
                                success = true;
                            }

                            if (success)
                            {
                                if (option.IndexOf("reference") >= 0)
                                {
                                    list.Clear();
                                    refno = getString(range, rCnt, 4);
                                    period = getString(range, rCnt, 6);
                                    paidto = getString(range, rCnt, 14);
                                }
                                else if (option.IndexOf("description") >= 0)
                                {
                                    refdesc = getString(range, rCnt, 4);
                                    refdate = getDateString(range, rCnt, 6);
                                    chkno = getString(range, rCnt, 14);
                                    chkdate = getDateString(range, rCnt, 22);
                                }
                                else if (!(refdesc.IndexOf("CANCELLED-") >= 0))
                                {
                                    list.Add(new
                                    {
                                        acctid = getString(range, rCnt, 1),
                                        acctdesc = getString(range, rCnt, 3),
                                        sl_code = getString(range, rCnt, 5).PadLeft(6,'0'),
                                        sl_name = getString(range, rCnt, 7),
                                        debit = gm.toNormalDoubleFormat(getString(range, rCnt, 13)).ToString("0.00"),
                                        credit = gm.toNormalDoubleFormat(getString(range, rCnt, 18)).ToString("0.00"),
                                        invoice = getString(range, rCnt, 19),
                                        cc_code = db.get_colval("m08", "cc_code", "UPPER(cc_desc)=" + db.str_E(getString(range, rCnt, 21).ToUpper()) + ""),
                                    });
                                }
                            }
                        }

                        if (rCnt == rw) hasval = false;

                        if (!hasval)
                        {
                            if (!String.IsNullOrEmpty(refno) && !String.IsNullOrEmpty(period) && !String.IsNullOrEmpty(refdesc) && !String.IsNullOrEmpty(refdate))
                            {

                                try { tmpdt = DateTime.Parse(refdate); }
                                catch { tmpdt = DateTime.Now; }

                                code = db.get_colval("m05", "j_num", "j_code='" + j_code + "'");
                                j_num_len = code.Length;
                                if (gm.toNormalDoubleFormat(code) < gm.toNormalDoubleFormat(refno) || !String.IsNullOrEmpty(refno))
                                {
                                    j_num = refno.PadLeft(j_num_len, '0');
                                }
                                else
                                {
                                    j_num = code;
                                }

                                if (period.ToLower().IndexOf("beg bal") >= 0 || (period.ToLower().IndexOf("beg") >= 0 && period.ToLower().IndexOf("bal") >= 0))
                                {
                                    mo = 0;
                                }
                                else
                                {
                                    mo = Convert.ToInt32(tmpdt.ToString("MM"));
                                    mo = (mo == 10 ? 12 : (mo + 2) % 12);
                                }

                                try { fy = Convert.ToInt32(db.get_colval("x03 x3", "fy", "('" + tmpdt.ToString("yyyy-MM-dd") + "')::date BETWEEN x3.from AND x3.to AND x3.mo='" + mo + "'")); }
                                catch { fy = Convert.ToInt32(tmpdt.ToString("yyyy")); }

                                do{
                                    try
                                    {
                                        if (db.add_jrnl(fy.ToString(), mo.ToString(), j_code, j_num, refdesc.ToUpper(), paidto, chkno, null, tmpdt.ToString("yyyy-MM-dd"), chkdate))
                                        {

                                            db.UpdateOnTable("tr01", "branch='" + (GlobalClass.branch) + "'", "j_num='" + j_num + "' AND j_code='" + j_code + "'");
                                            //db.add_jrnl_explanation(j_code, j_num, refdesc);

                                            for (int j = 0; j < list.Count; j++)
                                            {
                                                var line = list[j];

                                                if (String.IsNullOrEmpty(line.invoice) == false)
                                                {
                                                    db.upd_unpaid_invoices(j_code, line.sl_code, line.invoice, Convert.ToDouble(line.debit), Convert.ToDouble(line.credit));
                                                }
                                                //acctid ,acctdesc, sl_code ,sl_name ,debit ,credit ,invoice 
                                                if (db.add_jrnl_entry(j_code, j_num, (j + 1).ToString(), line.acctid, line.sl_code, line.sl_name, line.cc_code, null, line.debit, line.credit, line.invoice, null, null, null, null, null))
                                                {
                                                    count++;
                                                }
                                            }

                                            success = true;
                                            count++;
                                        }
                                        else
                                        {
                                            success = false;
                                            j_num = code;
                                        }
                                        if (code == j_num || gm.toNormalDoubleFormat(code) < gm.toNormalDoubleFormat(refno))
                                        {
                                            db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");
                                        }
                                    }
                                    catch
                                    {
                                        success = false;
                                        j_num = code;
                                    }
                                }while(!success);
                               
                            }
                            refno = ""; period = ""; paidto = ""; refdesc = ""; refdate = ""; chkno = ""; chkdate = "";

                        }

                        if (success)
                        {
                            inc_pbar(count, rw);
                            lbl_min.Invoke(new Action(() =>
                            {
                                lbl_min.Text = count.ToString();
                            }));
                        }
                    }
                }





                MessageBox.Show("Number of rows inserted : " + (count));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nOr some problem of your data.");
            }

            btn_import.Invoke(new Action(() =>
            {
                btn_import.Enabled = true;
            }));
            pbar.Invoke(new Action(() =>
            {
                pbar.Value = 0;
            }));
            lbl_min.Invoke(new Action(() =>
            {
                lbl_min.Text = "0";
            }));
            lbl_max.Invoke(new Action(() =>
            {
                lbl_max.Text = "0";
            }));
            btnBrowse.Invoke(new Action(() =>
            {
                btnBrowse.Enabled = true;
                btn_import.Enabled = false;
            }));

            btn_import.Invoke(new Action(() => {
                disp_list_fromdb();
            }));

            cbo_import.Invoke(new Action(() =>
            {
                cbo_import.Enabled = true;
                cbo_import_journal.Enabled = true;
                cbo_import_period.Enabled = true;
            }));
        }
        public int get_cbo_index(ComboBox cbo)
        {
            int index = -1;
            cbo.Invoke(new Action(() =>{
                try { index = cbo.SelectedIndex; }
                catch { }
            }));
            return index;
        }
        public String get_cbo_value(ComboBox cbo)
        {
            String str = "";
            cbo.Invoke(new Action(() => {
                try { str = cbo.SelectedValue.ToString(); } catch { }
            }));
            return str;
        }
        public String get_cbo_text(ComboBox cbo)
        {
            String str = "";
            cbo.Invoke(new Action(() => {
                try { str = cbo.Text; }
                catch { }
            }));
            return str;
        }
        public String getString(Excel.Range range, int row, int col)
        {
            String str = "";
            if (range != null)
            {
                try
                {
                    str = Convert.ToString((range.Cells[row, col] as Excel.Range).Value2 ?? "");
                }
                catch { }
            }
            return str;
        }
        public String getDateString(Excel.Range range, int row, int col)
        {
            DateTime dt = DateTime.Now;
            String dtstr = "";
            if (range != null)
            {
                try
                {
                    dtstr = getString(range, row, col);
                    try { dt = DateTime.Parse(dtstr); }
                    catch { dt = DateTime.FromOADate(Double.Parse(dtstr)); }
                }
                catch { }
            }
            return dt.ToString("yyyy-MM-dd");
        }
        public String getTimeString(Excel.Range range, int row, int col)
        {
            DateTime dt = DateTime.Now;
            String dtstr = "";
            if (range != null)
            {
                try
                {
                    dtstr = getString(range, row, col);
                    try { dt = DateTime.Parse(dt.ToString("yyyy-MM-dd ") + dtstr); }
                    catch { dt = DateTime.FromOADate(Double.Parse(dtstr)); }
                }
                catch { }
            }
            return dt.ToString("HH:mm");
        }
        public String getInc(String code)
        {
            String[] str = code.Split('-');
            double inc = 1;
            try
            {
                if (str.Length >= 2)
                {
                    inc = Convert.ToInt32(str[str.Length - 1]);
                    inc++;
                }
                code = (str.Length < 2 ? code + "-" : code.Replace(str[str.Length - 1], "")) + inc.ToString().PadLeft(3, '0');
            }
            catch { }
            return code;
        }
        private void inc_pbar(int i, int rw)
        {
            try
            {
                if (pbar.Value <= rw)
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = i;
                    }));
                }
                else
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = rw;
                    }));
                    btnBrowse.Invoke(new Action(() =>
                    {
                        btnBrowse.Enabled = true;
                    }));
                }
            }
            catch (Exception)
            {

            }
        }
        String tmptxtimp = "";
        Boolean chk1 = false;
        private void cbo_import_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean ok = true;
            if (cbo_import.SelectedIndex != -1)
            {
                if (db.get_jtypename(cbo_import_journal.SelectedValue.ToString()) == "General")
                {
                    if(cbo_import.SelectedIndex != 0)
                    {
                        MessageBox.Show("Beginning of Balance for General Journal Only.");
                        cbo_import_journal.DroppedDown = true;
                        ok = false;
                    }
                }
                else if (cbo_import.SelectedIndex == 0)
                {
                    if (db.get_jtypename(cbo_import_journal.SelectedValue.ToString()) != "General")
                    {
                        MessageBox.Show("Beginning of Balance for General Journal Only.");
                        cbo_import_journal.DroppedDown = true;
                        ok = false;
                    }
                
                }
                if(ok)
                {
                    if (cbo_import.SelectedIndex == 0)
                    {
                        cbo_import_period.Enabled = true;
                        cbo_import_period.DroppedDown = true;
                    }
                    else
                    {
                        cbo_import_period.Enabled = false;
                        cbo_import_period.SelectedIndex = -1;
                    }
                }
            }

        }

        private void cbo_import_journal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_import_journal.SelectedIndex != -1 && tpg_right_import.Text != "Import Excel")
            {
                lbl_upl_desc.Text = cbo_import_journal.Text + " to Upload : ";
                tpg_right_import.Text = "Import Excel - " + cbo_import_journal.Text;
                cbo_import.DroppedDown = true;
            }
        }

        private void cbo_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_search.SelectedIndex == 0)
            {
                cbo_period.Enabled = true;
            }
            else if (cbo_search.SelectedIndex == 1)
            {
                cbo_period.Enabled = false;
            }
        }

        private void btn_search_Click_1(object sender, EventArgs e)
        {
            disp_list_fromdb();
        }

        private void txt_search_jnum_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
