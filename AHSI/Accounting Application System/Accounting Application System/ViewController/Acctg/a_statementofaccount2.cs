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
    public partial class a_statementofaccount2 : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        String _reg_num = "";
        String res_line = "";
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        int _lnno=1;

        public a_statementofaccount2()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new thisDatabase();
            gc.load_customer(cbo_customer);
            disp_dgvlist();
            total_amountdue();
            gc.load_salesclerk(cbo_cashier);
            
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
        }

        private void a_statementofaccount_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            thisDatabase db = new thisDatabase();

            dtp_dt.Value = Convert.ToDateTime(db.get_systemdate(""));
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            tpg_info_enable(true);
            frm_clear();

            //m_customers frm_cust = new m_customers(this, true);

            //frm_cust.ShowDialog();
            
            goto_tbcntrl_info();
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            isnew = false;

            if (dgv_list.Rows.Count > 0)
            {
                try
                {
                    r = dgv_list.CurrentRow.Index;
                    code = dgv_list["dgvl_soa_code", r].Value.ToString();
                    txt_code.Text = code;
                    cbo_customer.SelectedValue = dgv_list["dgvl_debt_code", r].Value.ToString();
                    cbo_cashier.Text = dgv_list["dgvl_user_id", r].Value.ToString();
                    dtp_dt.Value = gm.toDateValue(dgv_list["dgvl_soa_date",r].Value.ToString());
                    dtp_dt_due.Value = gm.toDateValue(dgv_list["dgvl_due_date", r].Value.ToString());
                    //cbo_modeofpayment.SelectedValue = dgv_list["dgvl_mp_code", r].Value.ToString();
                    //txt_ckno.Text = dgv_list["dgvl_check_no", r].Value.ToString();
                    //dtp_invoicedt.Value = Convert.ToDateTime(dgv_list["dgvl_inv_date", r].Value.ToString());
                    //dtp_ckdt.Value = Convert.ToDateTime(dgv_list["dgvl_check_date", r].Value.ToString());

                    rtxt_comment.Text = dgv_list["dgvl_comments", r].Value.ToString();
                    //dtp_ckdt.Value = gm.toDateValue(dgv_list["dgvl_check_date", r].Value.ToString());

                }
                catch { }
                dgv_itemlist.Rows.Clear();
                disp_itemlist(code);
                total_amountdue();
                goto_tbcntrl_info();
                
                //goto_win2();
            }
        }

        private void disp_itemlist(String code)
        {
            DataTable dt = db.QueryBySQLCode("SELECT soa_code, ln_num, invoice, reference, amount, out_code, ord_code,out_desc FROM rssys.soalne WHERE soa_code='" + code + "'");

            try { dgv_itemlist.Rows.Clear(); }
            catch (Exception er) { MessageBox.Show(er.Message); }

            try
            {
               //Me.Show(dt.Rows.Count.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv_itemlist.Rows.Add();
                    dgv_itemlist["dgvl2_lnno", i].Value = dt.Rows[i]["ln_num"].ToString();
                   //dgv_itemlist["dgvli_unitid", i].Value = dt.Rows[i]["dgvli_unitid"].ToString();
                    dgv_itemlist["dgvl_outcode", i].Value = dt.Rows[i]["out_code"].ToString(); //1
                    dgv_itemlist["dgvl_outdesc", i].Value = dt.Rows[i]["out_desc"].ToString(); //1
                    dgv_itemlist["dgvl2_invoice", i].Value = dt.Rows[i]["invoice"].ToString();//1
                    dgv_itemlist["dgvl2_desc", i].Value = dt.Rows[i]["reference"].ToString();//1
                    dgv_itemlist["dgvl2_amount", i].Value = dt.Rows[i]["amount"].ToString(); //1
                    dgv_itemlist["dgvl2_ordcode", i].Value = dt.Rows[i]["ord_code"].ToString(); // 1
                  
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

            if (dgv_list.Rows.Count > 0)
            {
                int r = dgv_list.CurrentRow.Index;
                String soa_code = (dgv_list["dgvl_soa_code", r].Value ?? "").ToString();
                if (!String.IsNullOrEmpty(soa_code))
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
            if(cbo_customer.SelectedIndex != -1)
            {
                //z_add_folio frm_addfolio = new z_add_folio(this, true);

                //frm_addfolio.ShowDialog();
            }
        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "";
            String qty = "";

            try
            {
                if (dgv_itemlist.Rows.Count > 0)
                {
                    r = dgv_itemlist.CurrentRow.Index;

                    //if (isnew == false)
                    //{
                    //res_line = dgv_itemlist["dgvl2_lnno", r].Value.ToString();
                    //qty = dgv_itemlist["dgvi_oldqty", r].Value.ToString();
                    //dgv_delitem.Rows.Add(code, qty);
                    //}
                    //MessageBox.Show(code.ToString());
                    dgv_itemlist.Rows.RemoveAt(r);
                   
                    //disp_total();
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
            String code="", customerid="",customer_name="", comments="", soa_date="", user_id="", due_date, t_date, t_time;
            
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
            else
            {

                code = txt_code.Text;
                customerid = cbo_customer.SelectedValue.ToString();
                customer_name = cbo_customer.Text;
                soa_date = dtp_dt.Value.ToString("yyyy-MM-dd");
                user_id = cbo_cashier.Text;
                due_date = dtp_dt_due.Value.ToString("yyyy-MM-dd");
                user_id = GlobalClass.username;
                comments = rtxt_comment.Text;
                t_date = db.get_systemdate("");
                t_time = db.get_systemtime();

                if (isnew)
                {
                    try
                    {
                        code = db.get_pk("soa_code");
                        col = "soa_code, debt_code, debt_name, soa_date, user_id, comments, due_date, t_date, t_time";
                        val = "'" + code + "', '" + customerid + "', '" + customer_name + "', '" + soa_date + "', '" + user_id + "', '" + comments + "', '" + due_date + "', '" + t_date + "', '" + t_time + "'";

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
                    col = "soa_code='" + code + "', debt_code='" + customerid + "', debt_name='" + customer_name + "', soa_date='" + soa_date + "', user_id='" + user_id + "', due_date='" + due_date + "', comments='" + comments + "', t_date='" + t_date + "', t_time='" + t_time + "'";

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
            String ln_num, ord_code, invoice, reference, balance, outlet, out_desc;
            String val2 = "";
            String col2 = "soa_code, ln_num, ord_code, invoice, reference, amount, out_code, out_desc";
           
            try
            {
                for (int r = 0; r < dgv_itemlist.Rows.Count; r++)
                {
                    ln_num = dgv_itemlist["dgvl2_lnno", r].Value.ToString();
                    ord_code = dgv_itemlist["dgvl2_ordcode", r].Value.ToString();
                    invoice = dgv_itemlist["dgvl2_invoice", r].Value.ToString();
                    reference = dgv_itemlist["dgvl2_desc", r].Value.ToString();
                    outlet = dgv_itemlist["dgvl_outcode", r].Value.ToString();
                    out_desc = dgv_itemlist["dgvl_outdesc", r].Value.ToString();
                    balance = gm.toNormalDoubleFormat(dgv_itemlist["dgvl2_amount", r].Value.ToString()).ToString("0.00");


                    val2 = "'" + soa_code + "', '" + ln_num + "', '" + ord_code + "', '" + invoice + "', '" + reference + "', '" + balance + "', '" + outlet + "', '" + out_desc + "'";
                    DataTable dtcheck = db.QueryBySQLCode("SELECT * FROM rssys.soalne WHERE soa_code='"+soa_code+"'");
                    if (dtcheck.Rows.Count > 0)
                    {
                        db.DeleteOnTable("soalne", "soa_code='" + code + "'");
                    }
                 
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
            dtp_dt.Value = Convert.ToDateTime(db.get_systemdate(""));
           
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
                dtp_dt.Value = Convert.ToDateTime(dgv_list["dgvl_soa_date", r].Value.ToString());


            }
            catch (Exception) { }
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            //m_customers frm_cust = new m_customers(this, true);

            //frm_cust.ShowDialog();
        }

        public void set_custvalue_frm(String custcode, String custname)
        {
            cbo_customer.SelectedValue = custcode;
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
            String searchtype = "";

            if(cbo_searchby.SelectedIndex == 0)
            {
                searchtype = "soa_code";
            }
            else if(cbo_searchby.SelectedIndex == 1)
            {
                searchtype = "debt_name";
            }

            dgv_list.Rows.Clear();

            dt = db.get_soalist1(search, searchtype);

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                int i = dgv_list.Rows.Add();
                DataGridViewRow row = dgv_list.Rows[i];

                row.Cells["dgvl_soa_code"].Value = dt.Rows[r]["soa_code"].ToString();
                row.Cells["dgvl_debt_code"].Value = dt.Rows[r]["debt_code"].ToString();
                row.Cells["dgvl_debt_name"].Value = dt.Rows[r]["debt_name"].ToString();
                row.Cells["dgvl_soa_date"].Value = gm.toDateString(dt.Rows[r]["soa_date"].ToString(), "MM/dd/yyyy");
                row.Cells["dgvl_due_date"].Value = gm.toDateString(dt.Rows[r]["due_date"].ToString(), "MM/dd/yyyy");
                row.Cells["dgvl_user_id"].Value = dt.Rows[r]["user_id"].ToString();
                row.Cells["dgvl_t_date"].Value = gm.toDateString(dt.Rows[r]["t_date"].ToString(), "MM/dd/yyyy");
                row.Cells["dgvl_t_time"].Value = dt.Rows[r]["t_time"].ToString();
                row.Cells["dgvl_comments"].Value = dt.Rows[r]["comments"].ToString();

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

                //z_soaline_update frm = new z_soaline_update(this, upd_ref);

                //frm.Show();
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
            //z_add_folio frm_addfolio = new z_add_folio(this, false);
            //frm_addfolio.Show();
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

       
    }
}
