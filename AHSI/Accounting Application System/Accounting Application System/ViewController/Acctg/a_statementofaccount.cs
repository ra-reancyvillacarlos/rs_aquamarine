using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    public partial class a_statementofaccount : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        Boolean isperiod = false;
        Boolean isReady = false;
        String _reg_num = "";
        String res_line = "";
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        int _lnno=1;
        Boolean isCustomer = false;

        Boolean updateHasJrnlz = false;

        public a_statementofaccount()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new thisDatabase();
            load_customer_agency(cbo_customer);
            gc.load_salesclerk(cbo_cashier);

            load_soaperiod(cbo_soaperiod);
            load_soaperiod(cbo_soaperiods);
            gc.load_agency(comboBox2);
            
            cbo_cashier.Text = GlobalClass.username;
            
            thisDatabase db2 = new thisDatabase();
            String grp_id2 = "";
            DataTable dt24 = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt24.Rows.Count > 0)
            {
                grp_id2 = dt24.Rows[0]["grp_id"].ToString();
            }
            DataTable dt23 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id2 + "' AND a.mod_id='A3000' ORDER BY b.pla, b.mod_id");

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

            dtp_from.Value = DateTime.Parse(dtp_to.Value.ToString("yyyy-MM-01"));
            cbo_print.SelectedIndex = 0;


            disp_dgvlist();
            isReady = true;
        }
        public void load_customer_agency(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryBySQLCode("SELECT trv_code AS d_code, trv_name AS d_name, '' AS d_addr2, '' AS d_cntc_no, '' AS d_tel, '' AS d_fax, '' AS d_email, '' AS d_tin, '' AS d_cntc, 0 AS limit, '' AS at_code, '' AS mp_code, '' AS remarks, '' AS type, '' AS d_oldcode, 'Agency' AS soatype FROM rssys.travagnt UNION ALL SELECT d_code, d_name, d_addr2, d_cntc_no, d_tel, d_fax, d_email, d_tin, d_cntc, m06.limit, at_code, mp_code, remarks, type, d_oldcode, 'Customer' AS soatype FROM rssys.m06 ORDER BY d_name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "d_name";
                cbo.ValueMember = "d_code";
                cbo.SelectedIndex = -1;
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
        private void a_statementofaccount_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            comboBox1.SelectedIndex = 0;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            try
            {
                isnew = true;
                tpg_info_enable(true);
                frm_clear();

                m_customers frm_cust = new m_customers(this, true);
                frm_cust.ShowDialog();

                viewOnly(false);
                goto_tbcntrl_info();
            }
            catch { }
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            isnew = false;
            
            try
            {
                if (dgv_list.Rows.Count > 0)
                {
                    r = dgv_list.CurrentRow.Index;

                    //dgvl_cancel
                    if ((dgv_list["dgvl_cancel", r].Value ?? "").ToString() == "Y")
                    {
                        MessageBox.Show("Selected SAO already cancel.\nCannot be updated.");
                    }
                    else
                    {
                        updateHasJrnlz = (dgv_list["dgvl_jrnl", r].Value.ToString() == "Y");
                    
                        code = dgv_list["dgvl_soa_code", r].Value.ToString();
                        txt_code.Text = code;
                        cbo_customer.SelectedValue = dgv_list["dgvl_debt_code", r].Value.ToString();
                        cbo_cashier.Text = dgv_list["dgvl_user_id", r].Value.ToString();
                        comboBox4.SelectedIndex = ((dgv_list["dgvl_user_id", r].Value.ToString() == "Agency") ? 1 : 0);

                        dtp_dt_due.Value = gm.toDateValue(dgv_list["dgvl_due_date", r].Value.ToString());

                        rtxt_comment.Text = dgv_list["dgvl_comments", r].Value.ToString();
                        
                        dgv_itemlist.Rows.Clear();
                        disp_itemlist(code);
                        total_amountdue();

                        if ((dgv_list["dgvl_rmrttyp", r].Value ?? "").ToString() == "M")
                        {
                            cbo_soaperiod.SelectedValue = dgv_list["dgvl_soa_period", r].Value.ToString();
                            if (cbo_soaperiod.SelectedValue == null)
                            {
                                try { cbo_soaperiod.Text = db.get_colval("soa_period", "soa_desc", "'" + DateTime.Parse(dgv_list["dgvl_soa_period", r].Value.ToString()).ToString("yyyy-MM-dd") + "'::date BETWEEN soafrom AND soato"); }
                                catch { }
                            }
                        }
                        else
                        {
                            try { dtp_soa.Value = DateTime.Parse(dgv_list["dgvl_soa_period", r].Value.ToString()); }
                            catch { }
                        }

                        change_rmrttyp((dgv_list["dgvl_rmrttyp", r].Value ?? "").ToString(), "");
                        
                        viewOnly(updateHasJrnlz);
                        goto_tbcntrl_info();

                        if (updateHasJrnlz)
                        {
                            MessageBox.Show("Selected SAO already journalize.\nThis SOA is view only.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch { MessageBox.Show("No item selected."); }
        }

        private void disp_itemlist(String code)
        {
            DataTable dt = db.QueryBySQLCode("SELECT sl.*, gf.acct_no, gf.full_name, hf.name, CONCAT_WS(', ', pck.package1, pck1.activities1) AS chg_code1, CONCAT_WS(', ', pck.package, pck1.activities) AS chg_desc1, COALESCE(SPLIT_PART(gf.occ_type, ', ', 4), '0') AS ttlpax, c.chg_desc FROM rssys.soalne sl LEFT JOIN rssys.charge c ON c.chg_code=sl.chg_code LEFT JOIN rssys.gfolio gf ON gf.reg_num = sl.gfolio LEFT JOIN rssys.hotel hf ON hf.code = gf.hotel_code LEFT JOIN (SELECT cf.reg_num, cf.res_code AS rg_code, STRING_AGG(ch.chg_desc, ', ') AS package, STRING_AGG(ch.chg_code, ', ') AS package1 FROM rssys.chgfil cf LEFT JOIN rssys.charge ch ON cf.chg_code = ch.chg_code WHERE UPPER(cf.chg_code) LIKE 'PCK%' GROUP BY cf.reg_num, cf.res_code) pck ON pck.rg_code = gf.res_code LEFT JOIN (SELECT cf.reg_num, cf.res_code AS rg_code, STRING_AGG(ch.chg_desc, ', ') AS activities, STRING_AGG(ch.chg_code, ', ') AS activities1 FROM rssys.chgfil cf LEFT JOIN rssys.charge ch ON cf.chg_code = ch.chg_code WHERE UPPER(cf.chg_code) LIKE 'ACT%' GROUP BY cf.reg_num, cf.res_code) pck1 ON pck1.rg_code = gf.res_code WHERE soa_code='" + code + "' ORDER BY ln_num");
            if (comboBox4.SelectedIndex == 1)
            {
                dgv_itemlist.Columns["dgvl2_gfolio"].Visible = true; dgv_itemlist.Columns["acct_no"].Visible = true; dgv_itemlist.Columns["full_name"].Visible = true; dgv_itemlist.Columns["name"].Visible = true; dgv_itemlist.Columns["chg_code"].Visible = true; dgv_itemlist.Columns["chg_desc"].Visible = true; dgv_itemlist.Columns["ttlpax"].Visible = true; dgv_itemlist.Columns["dgvl2_charge_desc"].Visible = false; dgv_itemlist.Columns["dgvl2_chg_date"].Visible = false; dgv_itemlist.Columns["dgvl2_desc"].Visible = false; dgv_itemlist.Columns["dgvl2_chg_code"].Visible = false; dgv_itemlist.Columns["dgvl2_chg_num"].Visible = false; dgv_itemlist.Columns["dgvl2_istransferred"].Visible = false;
            }
            else
            {
                dgv_itemlist.Columns["dgvl2_gfolio"].Visible = false; dgv_itemlist.Columns["acct_no"].Visible = false; dgv_itemlist.Columns["full_name"].Visible = false; dgv_itemlist.Columns["name"].Visible = false; dgv_itemlist.Columns["chg_code"].Visible = false; dgv_itemlist.Columns["chg_desc"].Visible = false; dgv_itemlist.Columns["ttlpax"].Visible = false; dgv_itemlist.Columns["dgvl2_charge_desc"].Visible = true; dgv_itemlist.Columns["dgvl2_chg_date"].Visible = true; dgv_itemlist.Columns["dgvl2_desc"].Visible = true; dgv_itemlist.Columns["dgvl2_chg_code"].Visible = true; dgv_itemlist.Columns["dgvl2_chg_num"].Visible = true; dgv_itemlist.Columns["dgvl2_istransferred"].Visible = true;
            }

            try { dgv_itemlist.Rows.Clear(); }
            catch (Exception er) { MessageBox.Show(er.Message); }

            try
            {
               //Me.Show(dt.Rows.Count.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();
                    dgv_itemlist["dgvl2_lnno", i].Value = dt.Rows[i]["ln_num"].ToString();
                    dgv_itemlist["dgvl2_gfolio", i].Value = dt.Rows[i]["gfolio"].ToString();
                    dgv_itemlist["dgvl2_charge_desc", i].Value = dt.Rows[i]["chg_desc"].ToString(); 
                    dgv_itemlist["dgvl2_invoice", i].Value = dt.Rows[i]["invoice"].ToString();
                    dgv_itemlist["dgvl2_desc", i].Value = dt.Rows[i]["reference"].ToString();
                    dgv_itemlist["dgvl2_amount", i].Value = dt.Rows[i]["amount"].ToString();
                    dgv_itemlist["dgvl2_chg_date", i].Value = dt.Rows[i]["chg_date"].ToString();
                    dgv_itemlist["dgvl2_chg_time", i].Value = dt.Rows[i]["chg_time"].ToString();
                    dgv_itemlist["dgvl2_chg_code", i].Value = dt.Rows[i]["chg_code"].ToString();
                    dgv_itemlist["dgvl2_chg_num", i].Value = dt.Rows[i]["chg_num"].ToString();
                    dgv_itemlist["dgvl2_istransferred", i].Value = dt.Rows[i]["istransferred"].ToString();

                    dgv_itemlist["acct_no", i].Value = dt.Rows[i]["acct_no"].ToString();
                    dgv_itemlist["full_name", i].Value = dt.Rows[i]["full_name"].ToString();
                    dgv_itemlist["name", i].Value = dt.Rows[i]["name"].ToString();
                    dgv_itemlist["chg_code", i].Value = dt.Rows[i]["chg_code1"].ToString();
                    dgv_itemlist["chg_desc", i].Value = dt.Rows[i]["chg_desc1"].ToString();
                    dgv_itemlist["ttlpax", i].Value = dt.Rows[i]["ttlpax"].ToString();
                }
            }
            catch(Exception er) { MessageBox.Show(er.Message); }

        }
        public void total_amountdue()
        {
            Double amt = 0.00;
            Double damt = 0.00;
            Double tax = 0.00;
            Double paid = 0.00;

            try
            {
                for (int i = 0; i < dgv_itemlist.Rows.Count; i++)
                {
                    amt += gm.toNormalDoubleFormat(dgv_itemlist["dgvl2_amount", i].Value.ToString());
                }
            }
            catch { }

         
            txt_amtdue.Text = gm.toAccountingFormat(amt.ToString());


          
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            int r;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                if ((dgv_list["dgvl_cancel", r].Value ?? "").ToString() == "Y")
                {
                    MessageBox.Show("Selected SAO already cancel.");
                }
                else if ((dgv_list["dgvl_jrnl", r].Value ?? "").ToString() == "Y")
                {
                    MessageBox.Show("Selected SAO already journalize.");
                }
                else 
                {
                    String soa_code = dgv_list["dgvl_soa_code", r].Value.ToString();

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel SOA No. " + soa_code + "?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (db.UpdateOnTable("soahdr", "cancel='Y'", "soa_code='" + soa_code + "'"))
                        {
                            DataTable dt = db.QueryBySQLCode("SELECT COALESCE(SUM(amount),0.00) AS sum FROM rssys.soalne WHERE soa_code='" + soa_code + "'");
                            if (dt != null)
                            {
                                Hotel_System.thisDatabase dbhotel = new Hotel_System.thisDatabase();

                                String amount = dt.Rows[0]["sum"].ToString();
                                String chg_code = db.get_colval("m99", "fnlz_chg", "");
                                String gfolio = db.get_colval("soalne", "gfolio", "soa_code='" + soa_code + "'");
                                String tenant_name = db.get_colval("gfolio", "full_name", "reg_num='" + gfolio + "'");
                                String rom_code = db.get_colval("gfolio", "rom_code", "reg_num='" + gfolio + "'");
                                String tenant_no = db.get_colval("gfolio", "acct_no", "reg_num='" + gfolio + "'");
                                String soa_desc = "The SOA No. " + soa_code + " has been cancelled.\nAll linked charges has been removed.";

                                dbhotel.insert_charges(gfolio, tenant_name, chg_code, rom_code, soa_desc, (gm.toNormalDoubleFormat(amount) * -1), db.get_systemdate(""), "", "", "", "", "", 0.00, 0.00, 0.00, "", "", "", true);

                                db.UpdateOnTable("chgfil", "soa_code='', soa=''", "soa_code='" + soa_code + "' AND chg_code<>'" + chg_code + "'");
                                db.UpdateOnTable("chgfil", "soa_code='" + soa_code + "'", "COALESCE(soa_code,'')='' AND chg_code='" + chg_code + "'");

                                db.DeleteOnTable("soalne", "soa_code='" + soa_code + "' AND ln_num<>'1'");
                            }

                            disp_dgvlist();
                            goto_tbcntrl_list();
                            tpg_info_enable(false);
                        }
                        else
                        {
                            MessageBox.Show("Failed on deleting.");
                        }
                    }  
                }
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            int r = dgv_list.CurrentRow.Index;
            String soa_code = (dgv_list["dgvl_soa_code", r].Value ?? "").ToString();
            if (comboBox1.SelectedIndex == 0)
            {
                if (dgv_list.Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(soa_code))
                    {
                        if ((dgv_list["dgvl_cancel", r].Value ?? "").ToString() == "Y")
                        {
                            MessageBox.Show("Selected SAO already cancel.");
                        }
                        else
                        {
                            String debt_code = dgv_list["dgvl_debt_code", r].Value.ToString()
                                , debt_name = dgv_list["dgvl_debt_name", r].Value.ToString()
                                , sao_date = dgv_list["dgvl_soa_date", r].Value.ToString()
                                , due_date = dgv_list["dgvl_due_date", r].Value.ToString()
                                , t_date = dgv_list["dgvl_t_date", r].Value.ToString()
                                , t_time = dgv_list["dgvl_t_time", r].Value.ToString()
                                , comments = dgv_list["dgvl_comments", r].Value.ToString();

                            Report rpt = new Report();
                            rpt.print_statementofaccount(soa_code, debt_code, debt_name, sao_date, due_date, gm.toDateString(t_date, ""), t_time, comments);
                            rpt.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No SOA item selected.");
                    }
                }
                else
                {
                    MessageBox.Show("No SOA item selected.");
                }
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                if (!String.IsNullOrEmpty(soa_code))
                {
                        
                    Report rpt = new Report();
                    rpt.print_statementofaccount_agency(dgv_list["dgvl_debt_code", r].Value.ToString(), soa_code, dgv_list["dgvl_t_date", r].Value.ToString());
                    rpt.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No SOA item selected.");
                }
            }
        }

        private void btn_printsoa_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String code = "", client = "", ttdate = "", duedate = "", chg_dtfrm = "", chg_dtto = "";
            String reg_num = "";
            int r = 0;

            if (dgv_list.Rows.Count > 0)
            {
                Report rpt = new Report();

                r = dgv_list.CurrentRow.Index;

                code = dgv_list["dgvl_soa_code", r].Value.ToString();
                client = dgv_list["dgvl_debt_name", r].Value.ToString();
                ttdate = dgv_list["dgvl_soa_date", r].Value.ToString();
                chg_dtfrm = (dgv_list["dgvl_chg_dtfrm", r].Value ?? "").ToString();
                chg_dtto = (dgv_list["dgvl_chg_dtto", r].Value ?? "").ToString();
                reg_num = db.get_soa_reg_num(code);

                if (cbo_print.SelectedIndex == 0)
                {
                     rpt.print_soa(code, client, ttdate, duedate, chg_dtfrm, chg_dtto, reg_num);
                     
                    rpt.Show();
                }
                else if (cbo_print.SelectedIndex == 1)
                {
                    rpt.print_soa_warehouse(code, client, ttdate, chg_dtfrm, chg_dtto, reg_num);

                    rpt.Show();
                }
                else
                {
                    MessageBox.Show("Please select the print options.");
                    cbo_print.DroppedDown = true;
                }
            }
            
        }
        public void dgv_salesitem(DataTable dt, Boolean isnewitem)
        {
            String trial = "1";
            int i = 0;

            ////try
            // {
            if (isnewitem)
            {
                i = dgv_itemlist.Rows.Add();
            }
            else
            {
                i = dgv_itemlist.CurrentRow.Index;
            }



            //dgv_itemlist["dgvl2_lnno", i].Value = dt.Rows[0]["dgvl1_lnnum"].ToString();
            dgv_itemlist["dgvl2_lnno", i].Value = trial.ToString() ;
            dgv_itemlist["dgvl_outdesc", i].Value = dt.Rows[0]["dgvl1_outlet"].ToString();

            dgv_itemlist["dgvl2_chg_code", i].Value = dt.Rows[0]["dgvl1_ref"].ToString();
            dgv_itemlist["dgvl2_desc", i].Value = dt.Rows[0]["dgvl1_ref"].ToString();

            dgv_itemlist["dgvl2_amount", i].Value = dt.Rows[0]["dgvl1_balance"].ToString();

            //dgv_itemlist["dgvl2_chg_num", i].Value = dt.Rows[0]["dgvl1_user_id"].ToString();

            //if (isnew_item)
            //{
            //    inc_lnno();
            //}
            //}
            //catch (Exception) { }

            //total_amountdue();
        }
        private void btn_itemadd_Click(object sender, EventArgs e)
        {
            if (cbo_customer.SelectedIndex != -1)
            {
                z_add_folio frm_addfolio = new z_add_folio(this, true, isCustomer);

                frm_addfolio.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select tenant first.");
                btn_customer.PerformClick();
            }
        }

        private void viewOnly(Boolean enabled)
        {
            btn_itemadd.Enabled = !enabled;
            btn_itemupd.Enabled = !enabled;
            btn_itemremove.Enabled = !enabled;
            btn_mainsave.Enabled = !enabled;

            btn_customer.Enabled = !enabled;
            rtxt_comment.ReadOnly = enabled;
          
        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            int r = -1;
            String invoice = "", istransferred = "";

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;
                    invoice = (dgv_itemlist["dgvl2_invoice", r].Value ?? "").ToString();
                    istransferred = (dgv_itemlist["dgvl2_istransferred", r].Value ?? "").ToString();
                    if (String.IsNullOrEmpty(invoice) || String.IsNullOrEmpty(istransferred))
                    {
                        dgv_itemlist.Rows.RemoveAt(r);
                    }
                    else
                    {
                        MessageBox.Show("Cannot remove old soa lines.");
                    }
                    total_amountdue();
                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }
            catch (Exception) { MessageBox.Show("No item selected."); }
        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String code="", customerid="", customer_name="", comments="", soa_date="", user_id="", due_date, t_date, t_time, soa_period;
            
            String col = "", val = "", add_col = "", add_val = "";
            String table = "soahdr";
            String tbl_ln1 = "soalne2";
            String tbl_ln2 = "soalne3";
            Boolean success = false;
            String chg_dtto = "", chg_dtfrm = "";

            if (cbo_customer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a client to bill.");
            }
            else if (dgv_itemlist.Rows.Count == 0)
            {
                MessageBox.Show("Empty line list.");
            }
            else
            {

                code = txt_code.Text;
                customerid = cbo_customer.SelectedValue.ToString();
                customer_name = cbo_customer.Text;
                
                //user_id = cbo_cashier.Text;
                due_date = dtp_dt_due.Value.ToString("yyyy-MM-dd");
                user_id = GlobalClass.username;
                comments = rtxt_comment.Text;
                t_date = db.get_systemdate("");
                t_time = db.get_systemtime();

                
                if (cbo_soaperiod.Visible)
                {
                    soa_period = cbo_soaperiod.SelectedValue.ToString();
                }
                else
                {
                    soa_period = dtp_soa.Value.ToString("yyyy/MM/dd");
                }



                if (isnew)
                {
                    try
                    {
                        code = db.get_pk("soa_code");
                        col = "soa_code, debt_code, debt_name, soa_period, user_id, comments, due_date, t_date, t_time, branch, soatype";
                        val = "'" + code + "', '" + customerid + "', '" + customer_name + "', '" + soa_period + "', '" + user_id + "', '" + comments + "', '" + due_date + "', '" + t_date + "', '" + t_time + "', '" + GlobalClass.branch + "', '" + comboBox4.Text.ToString() + "'";

                        if (db.InsertOnTable(table, col, val))
                        {
                            db.set_pkm99("soa_code", db.get_nextincrementlimitchar(code, 8));
                            success = true;
                            add_items(code);
                        }
                        else
                        {
                            db.DeleteOnTable(table, "soa_code='" + code + "'");
                            MessageBox.Show("Failed on saving.");
                        }
                    }
                    catch  {  }
                }
                else
                {
                    col = "soa_code='" + code + "', debt_code='" + customerid + "', debt_name='" + customer_name + "', soa_period='" + soa_period + "', user_id='" + user_id + "', due_date='" + due_date + "', comments='" + comments + "', t_date='" + t_date + "', t_time='" + t_time + "', soatype='" + comboBox4.Text.ToString() + "'";

                    if (db.UpdateOnTable(table, col, "soa_code='" + code + "'"))
                    {
                        db.DeleteOnTable("soalne", "soa_code='" + code + "'");
                        add_items(code);
                        
                        success = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
                    }
                }

                if (success)
                {
                    MessageBox.Show("Successfully save.");
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

        private String add_items(String soa_code)
        {
            String notificationText = null;
            String ln_num, ord_code, invoice, reference, balance, outlet, out_desc, gfolio, amount, chg_date, chg_time, chg_code, chg_num, istransferred, soa_desc, tenant_no, rom_code, tenant_name;
            String val2 = "", _chg_code = "";
            String col2 = "soa_code, ln_num, invoice, reference, amount, gfolio, chg_date, chg_time, chg_code, chg_num,istransferred";
            Double total_charges = 0.00, tbalance;

            try
            {
                db.DeleteOnTable("soalne", "soa_code='" + soa_code + "'");
                _chg_code = db.get_colval("m99", "fnlz_chg", "");

                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    ln_num = dgv_itemlist["dgvl2_lnno", r].Value.ToString();
                    invoice = dgv_itemlist["dgvl2_invoice", r].Value.ToString();
                    reference = dgv_itemlist["dgvl2_desc", r].Value.ToString();

                    gfolio = dgv_itemlist["dgvl2_gfolio", r].Value.ToString();
                    chg_date = dgv_itemlist["dgvl2_chg_date", r].Value.ToString();
                    chg_time = dgv_itemlist["dgvl2_chg_time", r].Value.ToString();
                    chg_code = dgv_itemlist["dgvl2_chg_code", r].Value.ToString();
                    chg_num = dgv_itemlist["dgvl2_chg_num", r].Value.ToString();
                    istransferred = (dgv_itemlist["dgvl2_istransferred", r].Value??"").ToString();

                    balance = gm.toNormalDoubleFormat(dgv_itemlist["dgvl2_amount", r].Value.ToString()).ToString("0.00");

                    // check if unit billing has SOA 
                    if (String.IsNullOrEmpty(db.get_colval("chgfil", "soa_code", "soa_code='" + soa_code + "' AND chg_code='" + _chg_code + "' AND reg_num='" + gfolio + "'")))
                    {   // hasnot SOA, create new SOA
 
                        Hotel_System.thisDatabase dbhotel = new Hotel_System.thisDatabase();

                        tenant_name = db.get_colval("gfolio", "full_name", "reg_num='" + gfolio + "'");
                        rom_code = db.get_colval("gfolio", "rom_code", "reg_num='" + gfolio + "'");
                        tenant_no = db.get_colval("gfolio", "acct_no", "reg_num='" + gfolio + "'");

                        if (cbo_soaperiod.Visible)
                        {
                            soa_desc = "The SOA period of " + cbo_soaperiod.SelectedValue.ToString() + " has been picked up.\nThe generated SOA number is " + soa_code + ".";
                        }
                        else
                        {
                            soa_desc = "The SOA date of " + dtp_soa.Value.ToString("yyyy/MM/dd") + " has been picked up.\nThe generated SOA number is " + soa_code + ".";
                        }


                        dbhotel.insert_charges(gfolio, tenant_name, _chg_code, rom_code, soa_desc, total_charges, db.get_systemdate(""), "", "", "", "", "", 0.00, 0.00, 0.00, "", "", "", true);
                        db.UpdateOnTable("chgfil", "soa='Y', soa_code='" + soa_code + "'", "COALESCE(soa_code,'')='' AND chg_code='" + _chg_code + "' AND reg_num='" + gfolio + "'");

                        db.UpdateOnTable("soahdr", "comments='" + soa_desc + "'", "soa_code='" + soa_code + "'");
                        rtxt_comment.Text = soa_desc;
                    }


                    if (String.IsNullOrEmpty(invoice) || invoice == soa_code)
                    {
                        if (String.IsNullOrEmpty(db.get_colval("chgfil", "soa_code", "soa_code='" + invoice + "' AND chg_num='" + chg_num + "' AND chg_code='" + chg_code + "' AND reg_num='" + gfolio + "'")))
                        {
                            invoice = soa_code;
                            istransferred = "Y";
                            db.UpdateOnTable("chgfil", "soa='Y', soa_code='" + invoice + "'", "chg_num='" + chg_num + "' AND chg_code='" + chg_code + "' AND reg_num='" + gfolio + "' ");

                            try
                            {
                                tbalance = gm.toNormalDoubleFormat(db.get_colval("chgfil", "amount", "chg_code='" + _chg_code + "' AND reg_num='" + gfolio + "' AND soa_code='" + invoice + "'"));
                                tbalance += (-1 * gm.toNormalDoubleFormat(balance));
                                /*if (tbalance < 0) tbalance += (-1 * gm.toNormalDoubleFormat(balance));
                                else tbalance += gm.toNormalDoubleFormat(balance);*/

                                db.UpdateOnTable("chgfil", "amount='" + tbalance.ToString("0.00") + "'", "soa_code='" + invoice + "' AND chg_code='" + _chg_code + "' AND reg_num='" + gfolio + "'");

                            }
                            catch { }
                        }

                        total_charges += gm.toNormalDoubleFormat(balance);
                    }

                    val2 = "'" + soa_code + "', '" + ln_num + "', '" + invoice + "', " + db.str_E(reference) + ", '" + balance + "', '" + gfolio + "', '" + chg_date + "', '" + chg_time + "', '" + chg_code + "', '" + chg_num + "', '" + istransferred + "'";

                    if (db.InsertOnTable("soalne", col2, val2))
                    {
                        
                    }
                    else
                    {
                        notificationText = null;
                    }
                }

            }
            catch  {  }

            total_amountdue();

            return notificationText;
        }

        private void btn_ln_remove_Click(object sender, EventArgs e)
        {
            Boolean status = false;
            String reg_num = "", d_chg_code = "", d_chg_num = "", d_t_date = "";

            if (dgv_itemlist.Rows.Count > 1)
            {

                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    try { status = Convert.ToBoolean(dgv_itemlist["dgvl2_chk", r].Value.ToString()); }
                    catch (Exception) { status = false; }

                    if (status)
                    {
                        d_chg_code = dgv_itemlist["dgvl2_chg_code", r].Value.ToString();
                        d_chg_num = dgv_itemlist["dgvl2_chg_num", r].Value.ToString();
                        d_t_date = dgv_itemlist["dgvl2_t_date", r].Value.ToString();

                        dgv_itemlist.Rows.RemoveAt(r);

                        r--;  
                    }
                }
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            thisDatabase db = new thisDatabase();
            txt_code.Text = "";
            rtxt_comment.Text = "";
            dgv_itemlist.Rows.Clear();
        }
        private void goto_win2()
        {
           
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
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String code = "";
            int r = 0;
            DataTable dt = new DataTable();

            try
            {
                r = dgv_list.CurrentRow.Index;
                code = dgv_list["dgvl_soa_code", r].Value.ToString();
                txt_code.Text = code;
               cbo_customer.SelectedValue = dgv_list["dgvl_debt_code", r].Value.ToString();
                


            }
            catch (Exception) { }
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            m_customers frm_cust = new m_customers(this, true);

            frm_cust.ShowDialog();
        }

        public void set_custvalue_frm(String custcode, String custname, String soatype)
        {
            cbo_customer.SelectedValue = custcode;

            if (soatype == "Agency")
            {
                comboBox4.SelectedIndex = 1;
            }
            else
            {
                comboBox4.SelectedIndex = 0;
            }
        }

        public String get_custcode_frm()
        {
            String custcode = "";

            if(cbo_customer.SelectedIndex != -1)
            {
                custcode = cbo_customer.SelectedValue.ToString();
            }

            return custcode;
        }

        private void disp_dgvlist()
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            DataTable dt;
            String search = txt_search.Text;
            String searchtype = "", WHERE = "";

            if(cbo_searchby.SelectedIndex == 0)
            {
                searchtype = "soa_code";
            }
            else if(cbo_searchby.SelectedIndex == 1)
            {
                searchtype = "debt_name";
            }

            dgv_list.Rows.Clear();


            if (isperiod)
            {
                String[]dt_ = (cbo_soaperiods.SelectedValue??"").ToString().Split('-');
                String dt_frm = dt_.GetValue(0).ToString(), dt_to = dt_.GetValue(1).ToString();

                WHERE += " AND (sh.soa_period='" + cbo_soaperiods.SelectedValue.ToString() + "' OR sh.soa_period BETWEEN '" + dt_frm + "' AND '" + dt_to + "'  )";
            }
            else
            {
                WHERE += " AND t_date BETWEEN '" + dtp_from.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp_to.Value.ToString("yyyy-MM-dd") + "' ";
            }
            if (comboBox1.SelectedIndex > -1)
            {
                WHERE += " AND sh.soatype = '" + comboBox1.Text + "'";
            }

            //dt = db.get_soalist1(search, searchtype);

            dt = db.QueryBySQLCode("SELECT sh.*, sp.*, sl.rom_code, sl.rmrttyp FROM rssys.soahdr sh LEFT JOIN rssys.soa_period sp ON to_Char(sp.soafrom,'yyyy/MM/dd')||'-'||to_Char(sp.soato,'yyyy/MM/dd')=sh.soa_period LEFT JOIN (SELECT DISTINCT sl.soa_code, CASE WHEN COALESCE(gf.rom_code,'')<>'' THEN gf.rom_code ELSE COALESCE(gfh.rom_code,'') END rom_code, CASE WHEN COALESCE(gf.rmrttyp,'')<>'' THEN gf.rmrttyp ELSE COALESCE(gfh.rmrttyp,'') END rmrttyp FROM rssys.soalne sl LEFT JOIN rssys.gfolio gf ON sl.gfolio=gf.reg_num LEFT JOIN rssys.gfhist gfh ON sl.gfolio=gfh.reg_num WHERE sl.ln_num='1') sl ON (sl.soa_code=sh.soa_code)  WHERE 1=1 " + WHERE + " ORDER BY soa_code DESC"); //COALESCE(cancel,'')<>'Y'

            if (dt != null)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["dgvl_soa_code"].Value = dt.Rows[r]["soa_code"].ToString();
                    row.Cells["dgvl_debt_code"].Value = dt.Rows[r]["debt_code"].ToString();
                    row.Cells["dgvl_debt_name"].Value = dt.Rows[r]["debt_name"].ToString();

                    row.Cells["dgvl_due_date"].Value = gm.toDateString(dt.Rows[r]["due_date"].ToString(), "MM/dd/yyyy");
                    row.Cells["dgvl_user_id"].Value = dt.Rows[r]["user_id"].ToString();
                    row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(), "MM/dd/yyyy");
                    row.Cells["dgvl_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                    row.Cells["dgvl_comments"].Value = dt.Rows[r]["comments"].ToString();
                    row.Cells["dgvl_jrnl"].Value = dt.Rows[r]["jrnlz"].ToString();

                    row.Cells["dgvl_cancel"].Value = (dt.Rows[r]["cancel"] ?? "").ToString();

                    row.Cells["dgvl_roomno"].Value = dt.Rows[r]["rom_code"].ToString();
                    row.Cells["dgvl_rmrttyp"].Value = dt.Rows[r]["rmrttyp"].ToString();
                    row.Cells["dgvl_soa_period"].Value = dt.Rows[r]["soa_period"].ToString();


                    if (!String.IsNullOrEmpty((dt.Rows[r]["soa_desc"] ?? "").ToString()))
                    {
                        row.Cells["dgvl_soa_date"].Value = dt.Rows[r]["soa_desc"].ToString();
                    }
                    else
                    {
                        row.Cells["dgvl_soa_date"].Value = DateTime.Parse(dt.Rows[r]["soa_period"].ToString()).ToString("MMMM dd,yyyy");
                    }
                    row.Cells["dgvl_soa_period"].Value = dt.Rows[r]["soa_period"].ToString();
                    row.Cells["soatype"].Value = dt.Rows[r]["soatype"].ToString();

                    //dgvl_rmrttyp
                }
            }
        }

        public void change_rmrttyp(String mrttyp, String soa_dt)
        {
            if (mrttyp == "M")
            {
                label26.Show();
                cbo_soaperiod.Show();
                label9.Hide();
                dtp_soa.Hide();
                if (!String.IsNullOrEmpty(soa_dt))
                {
                    try { cbo_soaperiod.Text = db.get_colval("soa_period", "soa_desc", "'" + soa_dt + "'::date BETWEEN soafrom AND soato"); }
                    catch { }
                }
            }
            else
            {
                label26.Hide();
                cbo_soaperiod.Hide();
                label9.Show();
                dtp_soa.Show();
                if (!String.IsNullOrEmpty(soa_dt))
                {
                    try { dtp_soa.Value = DateTime.Parse(soa_dt); }
                    catch { }
                }
            }
        }


        
        private void dtp_chgto_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtp_chgfrom_ValueChanged(object sender, EventArgs e)
        {

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
                    typname = "dgvl_soa_code";
                }
                else
                {
                    typname = "dgvl_debt_name";
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

        private void btn_updsoa_Click(object sender, EventArgs e)
        {
            try
            {
                String upd_ref = dgv_itemlist["dgvl2_reference", dgv_itemlist.CurrentRow.Index].Value.ToString();

                z_soaline_update frm = new z_soaline_update(this, upd_ref);

                frm.Show();
            }
            catch(Exception){}
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

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            z_add_folio frm_addfolio = new z_add_folio(this, false, isCustomer);
            frm_addfolio.Show();
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

        private void dtp_from_ValueChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            isperiod = false;
            disp_dgvlist();
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            isperiod = false;
            disp_dgvlist();
        }

        private void cbo_soaperiods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            isperiod = true;
            disp_dgvlist();
        }

        private void btn_printlist_Click(object sender, EventArgs e)
        {
            try
            {
                RPT_RES_entry rpt = null;
                if (cbo_print.SelectedIndex == 0)
                {
                    rpt = new RPT_RES_entry("AS01P");
                }
                else
                {
                    rpt = new RPT_RES_entry("AS01D");
                }
                rpt.ShowDialog();
            }
            catch { }
        }

        private void cbo_customer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgvlist();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 1)
            {
                isCustomer = false;
                dgv_itemlist.Columns["dgvl2_gfolio"].Visible = true; dgv_itemlist.Columns["acct_no"].Visible = true; dgv_itemlist.Columns["full_name"].Visible = true; dgv_itemlist.Columns["name"].Visible = true; dgv_itemlist.Columns["chg_code"].Visible = true; dgv_itemlist.Columns["chg_desc"].Visible = true; dgv_itemlist.Columns["ttlpax"].Visible = true; dgv_itemlist.Columns["dgvl2_charge_desc"].Visible = false; dgv_itemlist.Columns["dgvl2_chg_date"].Visible = false; dgv_itemlist.Columns["dgvl2_desc"].Visible = false; dgv_itemlist.Columns["dgvl2_chg_code"].Visible = false; dgv_itemlist.Columns["dgvl2_chg_num"].Visible = false; dgv_itemlist.Columns["dgvl2_istransferred"].Visible = false;
            }
            else
            {
                isCustomer = true;
                dgv_itemlist.Columns["dgvl2_gfolio"].Visible = false; dgv_itemlist.Columns["acct_no"].Visible = false; dgv_itemlist.Columns["full_name"].Visible = false; dgv_itemlist.Columns["name"].Visible = false; dgv_itemlist.Columns["chg_code"].Visible = false; dgv_itemlist.Columns["chg_desc"].Visible = false; dgv_itemlist.Columns["ttlpax"].Visible = false; dgv_itemlist.Columns["dgvl2_charge_desc"].Visible = true; dgv_itemlist.Columns["dgvl2_chg_date"].Visible = true; dgv_itemlist.Columns["dgvl2_desc"].Visible = true; dgv_itemlist.Columns["dgvl2_chg_code"].Visible = true; dgv_itemlist.Columns["dgvl2_chg_num"].Visible = true; dgv_itemlist.Columns["dgvl2_istransferred"].Visible = true;
            }
        }
    }
}
