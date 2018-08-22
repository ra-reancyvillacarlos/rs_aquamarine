using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Accounting_Application_System
{
    public partial class m_customers : Form
    {
        a_statementofaccount _frm_soa = null;
        s_Sales _frm_sales = null;
        s_Sales_Auto _frm_salesauto = null;
        s_RepairOrder _frm_ro = null;
        call_history_entry _frm_call = null;
        auto_loanapplication _frm_auto = null;
        to_do _frm_todo = null;
        a_CollectionEntry _frm_co = null;
        a_disbursement _frm_d = null;

        Boolean iscallbackfrm = false;
        Boolean seltbp = false;
        Boolean isnew = false;
        dbSales db;
        GlobalMethod gm;
        Boolean isSOA = false;

        public m_customers()
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();
            cbo_type.SelectedIndex = 0;
            gc.load_mop(cbo_mop);
            gc.load_account_for_cust_ledger(cbo_subledger);
            thisDatabase db2 = new thisDatabase();
            String grp_id = "";
            DataTable dt = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0108' ORDER BY b.pla, b.mod_id");

            if (dt2.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt2.Rows[0]["add"].ToString();
                update = dt2.Rows[0]["upd"].ToString();
                delete = dt2.Rows[0]["cancel"].ToString();
                print = dt2.Rows[0]["print"].ToString();

                if (add == "n")
                {
                    btn_additem.Enabled = false;
                }
                if (update == "n")
                {
                    btn_upditem.Enabled = false;
                }
                if (delete == "n")
                {
                    btn_delitem.Enabled = false;
                }
                if (print == "n")
                {
                    btn_print.Enabled = false;
                }

            }
            disp_list("");

        }

        public m_customers(a_statementofaccount frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();

            _frm_soa = frm;
            iscallbackfrm = iscallback;

            gc.load_mop(cbo_mop);
            gc.load_accounttitle_sl_only(cbo_subledger);

            isSOA = true;
            disp_list("");
        }

        public m_customers(s_Sales frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();

            _frm_sales = frm;
            iscallbackfrm = iscallback;

            gc.load_mop(cbo_mop);
            gc.load_accounttitle_sl_only(cbo_subledger);

            disp_list("");
        }
        public m_customers(to_do frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();

            _frm_todo = frm;
            iscallbackfrm = iscallback;

            gc.load_mop(cbo_mop);
            gc.load_accounttitle_sl_only(cbo_subledger);

            disp_list("");
        }

        public m_customers(s_Sales_Auto frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();

            _frm_salesauto = frm;
            iscallbackfrm = iscallback;

            gc.load_mop(cbo_mop);
            gc.load_accounttitle_sl_only(cbo_subledger);

            disp_list("");
        }

        public m_customers(auto_loanapplication frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();

            _frm_auto = frm;
            iscallbackfrm = iscallback;

            gc.load_mop(cbo_mop);
            gc.load_accounttitle_sl_only(cbo_subledger);

            disp_list("");
        }

        public m_customers(s_RepairOrder frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();

            _frm_ro = frm;
            iscallbackfrm = iscallback;

            gc.load_mop(cbo_mop);
            gc.load_accounttitle_sl_only(cbo_subledger);

            disp_list("");
        }
        public m_customers(call_history_entry frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();

            _frm_call = frm;
            iscallbackfrm = iscallback;

            gc.load_mop(cbo_mop);
            gc.load_accounttitle_sl_only(cbo_subledger);

            disp_list("");
        }

        public m_customers(a_disbursement frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();

            _frm_d = frm;
            iscallbackfrm = iscallback;

            gc.load_mop(cbo_mop);
            gc.load_accounttitle_sl_only(cbo_subledger);

            disp_list("");
        }

        public m_customers(a_CollectionEntry frm, Boolean iscallback)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();
            db = new dbSales();
            gm = new GlobalMethod();

            _frm_co = frm;
            iscallbackfrm = iscallback;

            gc.load_mop(cbo_mop);
            gc.load_accounttitle_sl_only(cbo_subledger);

            disp_list("");
        }

        private void m_customers_Load(object sender, EventArgs e)
        {
            if (iscallbackfrm)
            {
                dgv_list.Dock = DockStyle.Bottom;
                dgv_list.Height = 482; //
                //dgv_list.Location.Y = 45; // 
                dgv_list.Location = new Point(this.dgv_list.Location.X, 45);
                lbl_notification.Show();
            }
            else
            {
                dgv_list.Dock = DockStyle.Fill;
                lbl_notification.Hide();
            }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
            btn_save.Enabled = true;
            btn_import.Enabled = false;
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            if (dgv_list.Rows.Count > 0 && String.IsNullOrEmpty(dgv_list["d_code", dgv_list.CurrentRow.Index].Value.ToString()) == false)
            {
                isnew = false;
                tpg_info_enable(true);
                txt_code.Enabled = false;
                frm_clear();
                disp_info();
                goto_tbcntrl_info();

                btn_save.Enabled = true;
                btn_import.Enabled = false;
            }
            else
            {
                MessageBox.Show("No rows selected");
            }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently Disabled");

            /*
            int r;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                //if (db.UpdateOnTable("m07", "cancel='Y'", "c_code='" + dgv_list["ID", r].Value.ToString() + "'"))
                //{
                    disp_list();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                //}
                //else
                //{
                 //   MessageBox.Show("Failed on deleting.");
                //}
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }*/
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4008);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_list(txt_search.Text);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String code, d_name, d_addr2, d_tel, d_fax, d_email, d_tin, d_cntc, limit, at_code, mp_code, remarks, d_cntc_no, type = "", d_oldcode = "";
            
            String col = "", val = "", add_col = "", add_val = "";
            String table = "m06";
            Boolean success = true;

            if (String.IsNullOrEmpty(txt_customer.Text) || cbo_mop.SelectedIndex == -1 || cbo_subledger.SelectedIndex == -1)
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else if (cbo_type.SelectedIndex == -1)
            {
                MessageBox.Show("Please select type.");
                cbo_type.DroppedDown = true;
            }
            else
            {
                
                code = txt_code.Text;
                d_name = txt_customer.Text.Trim();
                d_addr2 = rtxt_address.Text;
                d_tel = txt_phone.Text; 
                d_fax = txt_fax.Text;
                d_email = txt_email.Text;
                d_tin = txt_tin.Text;
                d_cntc = txt_contactperson.Text;
                d_cntc_no = txt_cntc_mobile.Text;
                limit = gm.toNormalDoubleFormat(txt_limit.Text).ToString("0.00");
                mp_code = cbo_mop.SelectedValue.ToString();
                at_code = cbo_subledger.SelectedValue.ToString();
                remarks = rtxt_remark.Text;
                type = cbo_type.Text;
                d_oldcode = (cbo_oldaccount.SelectedValue??"").ToString();
                if (isnew)
                {
                    code = db.get_pk("d_code");
                    col = "d_code, d_oldcode, d_name, d_addr2, d_tel, d_fax, d_email, d_tin, d_cntc, d_cntc_no, \"limit\", at_code, mp_code, remarks,type";
                    val = "'" + code + "','" + d_oldcode + "', " + db.str_E(d_name) + ", " + db.str_E(d_addr2) + ", '" + d_tel + "', '" + d_fax + "', '" + d_email + "', '" + d_tin + "', '" + d_cntc + "', '" + d_cntc_no + "', " + limit + ", '" + at_code + "', '" + mp_code + "', '" + remarks + "', '" + type + "'";

                    if (db.InsertOnTable(table, col, val))
                    {

                        String value = "'" + code + "',''," + db.str_E(d_name) + ",'','', " + db.str_E(d_name) + ",'M','1950-01-01'," + db.str_E(d_addr2) + "," + db.str_E(d_tel) + "," + db.str_E(d_email) + ",'','','1950-01-01','1950-01-01','','101', '', '" + db.get_systemdate("") + "','" + DateTime.Now.ToString("HH:mm") + "','" + GlobalClass.username + "'";
                        db.InsertOnTable("guest", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, mp_code, escaper, t_date, t_time, user_id", value);

                        db.set_pkm99("d_code", db.get_nextincrementlimitchar(code, 6));
                        db.set_pkm99("acct_no", db.get_nextincrementlimitchar(code, 6));
                        success = true;
                    }
                    else
                    {
                        db.DeleteOnTable(table, "d_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "d_code='" + code + "',d_oldcode='" + d_oldcode + "', d_name=" + db.str_E(d_name) + ", d_addr2=" + db.str_E(d_addr2) + ", d_tel='" + d_tel + "', d_fax='" + d_fax + "', d_email='" + d_email + "', d_tin='" + d_tin + "', d_cntc='" + d_cntc + "', d_cntc_no='" + d_cntc_no + "', \"limit\"=" + limit + ", at_code='" + at_code + "', mp_code='" + mp_code + "'" + ", remarks='" + remarks + "',type='" + type + "'";


                    if (db.UpdateOnTable(table, col, "d_code='" + code + "'"))
                    {
                        db.UpdateOnTable("guest", "full_name=" + db.str_E(d_name) + ", address1=" + db.str_E(d_addr2) + ", tel_num=" + db.str_E(d_tel) + ", email=" + db.str_E(d_email) + ", t_date='" + db.get_systemdate("") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "', user_id='" + GlobalClass.username + "' ", "acct_no='" + code + "'");
                        success = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
                    }
                }


                if (success)
                {
                    if (iscallbackfrm)
                    {
                        setValuefromFrm(code, d_name, "");
                    }
                    else
                    {
                        disp_list("");
                        goto_tbcntrl_list();
                        tpg_info_enable(false);
                        frm_clear();
                    }
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_customer.Text = "";
            txt_cntc_mobile.Text = "";
            rtxt_address.Text = "";
            txt_phone.Text = "";
            txt_fax.Text = "";
            txt_email.Text = "";
            txt_tin.Text = "";
            txt_contactperson.Text = "";
            txt_limit.Text = "0.00";
            cbo_mop.SelectedIndex = -1;
            cbo_subledger.SelectedIndex = -1;
            cbo_oldaccount.SelectedIndex = -1;
            
            rtxt_remark.Text = "";
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
            txt_customer.Enabled = flag;
            txt_contactperson.Enabled = flag;
            txt_email.Enabled = flag;
            txt_fax.Enabled = flag;
            txt_limit.Enabled = flag;
            txt_phone.Enabled = flag;
            txt_tin.Enabled = flag;

            rtxt_address.Enabled = flag;

            cbo_mop.Enabled = flag;
            cbo_subledger.Enabled = flag;
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

        private void disp_list(String searchtext)
        {
            String WHERE = "";
            clear_dgv();

            if(String.IsNullOrEmpty(searchtext) == false)
            {
                if (cbo_searchby.Text == "Customer ID") 
                {
                    WHERE = "d_code LIKE $$%" + searchtext + "%$$";
                }
                else //Customer Name
                {
                    WHERE = "d_name LIKE  $$%" + searchtext + "%$$";
                }
            }
                   
            try
            {
                DataTable dt = new DataTable();
                if (isSOA == true)
                {
                    dt = db.QueryBySQLCode("SELECT trv_code AS d_code, trv_name AS d_name, '' AS d_addr2, '' AS d_cntc_no, '' AS d_tel, '' AS d_fax, '' AS d_email, '' AS d_tin, '' AS d_cntc, 0 AS limit, '' AS at_code, '' AS mp_code, '' AS remarks, '' AS type, '' AS d_oldcode, 'Agency' AS soatype FROM rssys.travagnt UNION ALL SELECT d_code, d_name, d_addr2, d_cntc_no, d_tel, d_fax, d_email, d_tin, d_cntc, m06.limit, at_code, mp_code, remarks, type, d_oldcode, 'Customer' AS soatype FROM rssys.m06" + ((WHERE != "") ? " WHERE " + WHERE + "" : "") + " ORDER BY d_name ASC");
                    dgv_list.Columns["d_addr2"].Visible = false; dgv_list.Columns["d_cntc_no"].Visible = false; dgv_list.Columns["d_tel"].Visible = false; dgv_list.Columns["d_fax"].Visible = false; dgv_list.Columns["d_email"].Visible = false; dgv_list.Columns["d_tin"].Visible = false; dgv_list.Columns["d_cntc"].Visible = false; dgv_list.Columns["limit"].Visible = false; dgv_list.Columns["at_code"].Visible = false; dgv_list.Columns["mp_code"].Visible = false; dgv_list.Columns["remarks"].Visible = false; dgv_list.Columns["type"].Visible = false; dgv_list.Columns["d_oldcode"].Visible = false; dgv_list.Columns["soatype"].Visible = true;
                }
                else
                {
                    dt = db.QueryOnTableWithParams("m06", "*, '' AS soatype", WHERE, " ORDER BY d_name ASC");
                    dgv_list.Columns["d_addr2"].Visible = true; dgv_list.Columns["d_cntc_no"].Visible = true; dgv_list.Columns["d_tel"].Visible = true; dgv_list.Columns["d_fax"].Visible = true; dgv_list.Columns["d_email"].Visible = true; dgv_list.Columns["d_tin"].Visible = true; dgv_list.Columns["d_cntc"].Visible = true; dgv_list.Columns["limit"].Visible = true; dgv_list.Columns["at_code"].Visible = true; dgv_list.Columns["mp_code"].Visible = true; dgv_list.Columns["remarks"].Visible = true; dgv_list.Columns["type"].Visible = true; dgv_list.Columns["d_oldcode"].Visible = true; dgv_list.Columns["soatype"].Visible = false;
                }

                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["d_code"].Value = dt.Rows[r]["d_code"].ToString();
                    row.Cells["d_name"].Value = dt.Rows[r]["d_name"].ToString();
                    row.Cells["d_addr2"].Value = dt.Rows[r]["d_addr2"].ToString();
                    row.Cells["d_cntc_no"].Value = dt.Rows[r]["d_cntc_no"].ToString();
                    row.Cells["d_tel"].Value = dt.Rows[r]["d_tel"].ToString();
                    row.Cells["d_fax"].Value = dt.Rows[r]["d_fax"].ToString();
                    row.Cells["d_email"].Value = dt.Rows[r]["d_email"].ToString();
                    row.Cells["d_tin"].Value = dt.Rows[r]["d_tin"].ToString();
                    row.Cells["d_cntc"].Value = dt.Rows[r]["d_cntc"].ToString();
                    row.Cells["limit"].Value = dt.Rows[r]["limit"].ToString();
                    row.Cells["at_code"].Value = dt.Rows[r]["at_code"].ToString();
                    row.Cells["mp_code"].Value = dt.Rows[r]["mp_code"].ToString();
                    row.Cells["remarks"].Value = dt.Rows[r]["remarks"].ToString();
                    row.Cells["type"].Value = dt.Rows[r]["type"].ToString();
                    row.Cells["d_oldcode"].Value = dt.Rows[r]["d_oldcode"].ToString();
                    row.Cells["soatype"].Value = dt.Rows[r]["soatype"].ToString();
                }
            }
            catch (Exception er) 
            { 
                //MessageBox.Show(er.Message); 
            }
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;
                
                txt_code.Text = dgv_list["d_code", r].Value.ToString();
                txt_customer.Text = dgv_list["d_name", r].Value.ToString();
                rtxt_address.Text = dgv_list["d_addr2", r].Value.ToString();
                txt_phone.Text = dgv_list["d_tel", r].Value.ToString();
                txt_fax.Text = dgv_list["d_fax", r].Value.ToString();
                txt_email.Text = dgv_list["d_email", r].Value.ToString();
                txt_tin.Text = dgv_list["d_tin", r].Value.ToString();
                txt_contactperson.Text = dgv_list["d_cntc", r].Value.ToString();
                txt_cntc_mobile.Text = dgv_list["d_cntc_no", r].Value.ToString();
                txt_limit.Text = dgv_list["limit", r].Value.ToString();
                cbo_mop.SelectedValue = dgv_list["mp_code", r].Value.ToString();
                cbo_subledger.SelectedValue = dgv_list["at_code", r].Value.ToString();
                rtxt_remark.Text = dgv_list["remarks", r].Value.ToString();
                cbo_type.Text = dgv_list["type", r].Value.ToString();
                cbo_oldaccount.SelectedValue = dgv_list["d_oldcode", r].Value.ToString();

            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        
        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "d_name";

            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "d_code";
                }
                else
                {
                    typname = "d_name";
                }

                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                        .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                        .First();

                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;


                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[10];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[11];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[12];
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

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void dgv_list_DoubleClick(object sender, EventArgs e)
        {
            int r = 0;
            String custcode = "", custname = "", soatype = "";

            try
            {
                if (iscallbackfrm)
                {
                    r = dgv_list.CurrentRow.Index;

                    custcode = dgv_list[0, r].Value.ToString();
                    custname = dgv_list[1, r].Value.ToString();
                    if (isSOA == true)
                    {
                        soatype = dgv_list["soatype", r].Value.ToString();

                        setValuefromFrm(custcode, custname, soatype);
                    }
                    else
                    {
                        setValuefromFrm(custcode, custname, "");
                    }
                }
            }
            catch (Exception) { }
        }

        private void setValuefromFrm(String custcode, String custname, String soatype)
        {
            if (_frm_soa != null)
            {
                _frm_soa.set_custvalue_frm(custcode, custname, soatype);
                this.Close();
            }
            if (_frm_sales != null)
            {
                _frm_sales.set_custvalue_frm(custcode);
                this.Close();
            }
            if (_frm_salesauto != null)
            {
                _frm_salesauto.set_custvalue_frm(custcode);
                this.Close();
            }
            if (_frm_ro != null)
            {
                _frm_ro.set_custvalue_frm(custcode);
                this.Close();
            }
            if (_frm_call != null)
            {
                _frm_call.set_custvalue_frm(custcode);
                this.Close();
            }
            if (_frm_todo != null)
            {
                _frm_todo.set_custvalue_frm(custcode);
                this.Close();
            }
            if (_frm_d != null)
            {
                _frm_d.set_custvalue_frm(custcode);
                this.Close();
            }
            if (_frm_co != null)
            {
                _frm_co.set_custvalue_frm(custcode);
                this.Close();
            }
            if (_frm_auto != null)
            {
                _frm_auto.set_custvalue_frm(custcode);
                this.Close();
            }
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_filename.Text = openFile.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_import.Enabled = true;
            cbo_import.SelectedIndex = 0;
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_import;
            tbcntrl_option.SelectedTab = tpg_opt_2;

            tpg_import.Show();
            tpg_opt_2.Show();
            seltbp = false;
        }

        private void importBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            Boolean success = false;
            String str = "";
            int rCnt = 0;
            int cCnt = 0;
            int rw = 0;
            int cl = 0;
            String filename = "";
            int i = 0;
            int count = 0;
            int r = 0;

            Double dt_double = 0;
            String dt_strval = "";
            DateTime date_release, dt_temp = Convert.ToDateTime(db.get_systemdate(""));
            String d_name = "", d_code = "", d_addr2 = "", d_cntc_no = "", d_tel = "", d_fax = "", d_email = "", d_cntc = "", at_code = "", at_desc = "", acct_no = "", limit = "", vin_desc = "", plate_no = "", vin_no = "", engine_no = "", strf = "", dt_release = "", year_model = "";
            String col = "", val = "", add_col = "", add_val = "",code="";
            String table = "m06";
            
            String import_selected = "";
            if(get_cbo_index(cbo_import) != -1)
            {
                import_selected = get_cbo_text(cbo_import).ToLower();
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
                    cbo_import.Invoke(new Action(() =>
                    {
                        cbo_import.Enabled = false;
                    }));
                }
                if(import_selected.IndexOf("hotel")>=0)
                {
                    for (rCnt = 1; rCnt <= rw; rCnt++)
                    {
                        Boolean hasval = false;

                        for (int j = 1; j < 4 && !hasval; j++)
                            if (!String.IsNullOrEmpty(getString(range, rCnt, j)))
                                hasval = true;
                        if (hasval)
                        {
                            String option = getString(range, rCnt, 1).ToLower();
                            if (String.IsNullOrEmpty(option) || option.IndexOf("acct") >= 0 || option.IndexOf("account") >= 0)
                            {
                                success = false;
                            }
                            else
                            {
                                success = true;
                            }

                            if (success)
                            {
                                if (rCnt != 1)
                                {
                                    d_code = db.get_pk("d_code");
                                    acct_no = db.get_pk("acct_no");

                                    d_name = getString(range, rCnt, 2);
                                    d_addr2 = getString(range, rCnt, 3);
                                    d_tel = getString(range, rCnt, 4);
                                    d_fax = getString(range, rCnt, 5);
                                    d_email = getString(range, rCnt, 6);
                                    d_cntc = getString(range, rCnt, 7);
                                    at_desc = getString(range, rCnt, 8);
                                    at_code = db.get_colval("m04", "at_code", "UPPER(at_desc)=" + db.str_E(at_desc) + "");

                                    col = "d_code, d_name, d_addr2, d_tel, d_fax, d_email, type, d_cntc, lastname,firstname, mname,at_code";
                                    val = "'" + d_code + "', " + db.str_E(d_name) + ", " + db.str_E(d_addr2) + ", '" + d_tel + "', '" + d_fax + "', '" + d_email + "', 'Customer'," + db.str_E(d_cntc) + ", '','', '','" + at_code + "'";
                                    if (db.InsertOnTable("m06", col, val))
                                    {
                                        
                                        col = "acct_no, last_name, first_name, mid_name, full_name, address1, tel_num, fax_num, email, at_code, t_date, t_time";
                                        val = "'" + acct_no + "', '', '', '', " + db.str_E(d_name) + ", " + db.str_E(d_addr2) + ", '" + d_tel + "', '" + d_fax + "', '" + d_email + "','" + at_code + "','" + dt_temp.ToString("yyyy-MM-dd") + "','" + dt_temp.ToString("HH:mm") + "'";
                                        db.InsertOnTable("guest", col, val);
                                        db.set_pkm99("d_code", db.get_nextincrementlimitchar(d_code, 6));
                                        db.set_pkm99("acct_no", db.get_nextincrementlimitchar(acct_no, 6));
                                        count++;
                                        inc_pbar(count, rw);
                                        lbl_min.Invoke(new Action(() =>
                                        {
                                            lbl_min.Text = count.ToString();
                                        }));
                                    }
                                }
                            }
                        }
                    }
                }
                else if (import_selected.IndexOf("vehicle") >= 0)
                {

                    for (rCnt = 1; rCnt <= rw; rCnt++)
                    {
                        success = false;
                        d_name = Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2).ToLower();

                        if (!(d_name.IndexOf("customer") >= 0 || d_name.IndexOf("d_name") >= 0 || d_name.IndexOf("m06") >= 0))
                        {
                            d_name = Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2).ToUpper().Trim('\'');
                            d_addr2 = Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2 ?? "").Trim('\'');
                            d_cntc_no = Convert.ToString((range.Cells[rCnt, 3] as Excel.Range).Value2 ?? "").Trim('\'');
                            d_tel = Convert.ToString((range.Cells[rCnt, 4] as Excel.Range).Value2 ?? "").Trim('\'');
                            d_fax = Convert.ToString((range.Cells[rCnt, 5] as Excel.Range).Value2 ?? "").Trim('\'');
                            limit = Convert.ToString((range.Cells[rCnt, 6] as Excel.Range).Value2 ?? "").Trim('\'');
                            vin_desc = Convert.ToString((range.Cells[rCnt, 7] as Excel.Range).Value2 ?? "").Trim('\'');
                            plate_no = Convert.ToString((range.Cells[rCnt, 8] as Excel.Range).Value2 ?? "").Trim('\'');
                            vin_no = Convert.ToString((range.Cells[rCnt, 9] as Excel.Range).Value2 ?? "").Trim('\'');
                            engine_no = Convert.ToString((range.Cells[rCnt, 10] as Excel.Range).Value2 ?? "").Trim('\'');
                            dt_strval = Convert.ToString((range.Cells[rCnt, 10] as Excel.Range).Value2 ?? "");
                            dt_release = ""; year_model = "";

                            date_release = dt_temp;
                            try { if (String.IsNullOrEmpty(dt_strval) == false) { date_release = gm.toDateValue(dt_strval); } }
                            catch { }
                            try { if (String.IsNullOrEmpty(limit) || Convert.ToDouble(limit) == 0) limit = "0"; }
                            catch { limit = "0"; }

                            d_code = db.get_colval("m06", "d_code", String.Format("lower(d_name)={0} OR lower(firstname||' '||lastname)={0} OR lower(lastname||' '||firstname)={0}", db.str_E(d_name.ToLower())));

                            if (String.IsNullOrEmpty(d_code))
                            {
                                d_code = db.get_pk("d_code");
                                col = "d_code,d_name,d_addr2,d_tel,d_fax,d_cntc_no, \"limit\", type";
                                val = "'" + d_code + "'," + db.str_E(d_name) + "," + db.str_E(d_addr2) + ",'" + d_tel + "','" + d_fax + "','" + d_cntc_no + "','" + limit + "', 'Customer'";
                                if (db.InsertOnTable("m06", col, val))
                                {
                                    success = true;
                                    db.set_pkm99("d_code", db.get_nextincrementlimitchar(d_code, 6));
                                }
                            }

                            if (!String.IsNullOrEmpty(vin_no) || !String.IsNullOrEmpty(vin_desc))
                            {
                                if (String.IsNullOrEmpty(vin_no))
                                {
                                    strf = "{0}-{1}-{2}";
                                    if (String.IsNullOrEmpty(plate_no))
                                        strf = "{0}-{2}";
                                    vin_no = String.Format(strf, vin_desc, plate_no, d_code);
                                }
                                if (String.IsNullOrEmpty(vin_desc))
                                {
                                    strf = "{0}-{1}-{2}";
                                    if (String.IsNullOrEmpty(plate_no))
                                        strf = "{0}-{2}";
                                    vin_desc = String.Format(strf, vin_no, plate_no, d_code);
                                }

                                year_model = date_release.ToString("yyyy");
                                dt_release = date_release.ToString("yyyy-MM-dd");

                                do
                                { // Increment the last data like 'data_code-2' to 'data_code-3'
                                    str = db.get_colval("vehicle_info", "vin_no", "vin_no=" + db.str_E(vin_no) + "");

                                    if (!String.IsNullOrEmpty(str))
                                    {
                                        String[] splited = str.Split('-');
                                        String cnt = "2";
                                        try
                                        {
                                            r = Convert.ToInt32(splited[splited.Length - 1]);
                                            cnt = (r <= 100 ? r + 1 : r).ToString();
                                        }
                                        catch { cnt = "2"; }
                                        vin_no = String.Format("{0}-{1}", String.Join("-", splited.Take(splited.Length - 1)), cnt);
                                    }
                                } while (!String.IsNullOrEmpty(str));

                                col = "vin_desc,year_model,vin_no,engine_no,plate_no,date_release,owner";
                                val = "" + db.str_E(vin_desc) + ",'" + year_model.Trim('\'') + "'," + db.str_E(vin_no) + ",'" + engine_no + "','" + plate_no.Trim('\'') + "','" + dt_release + "','" + d_code + "'";

                                if (db.InsertOnTable("vehicle_info", col, val))
                                {
                                    success = true;
                                }
                            }

                            if (success)
                            {
                                count++;
                                inc_pbar(count, rw);
                                lbl_min.Invoke(new Action(() =>
                                {
                                    lbl_min.Text = count.ToString();
                                }));

                                lbl_customer.Invoke(new Action(() =>
                                {
                                    lbl_customer.Text = d_name + " :: " + val;
                                }));
                            }
                        }
                    }
                }

                MessageBox.Show("Number of rows inserted : " + (count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            }));
            cbo_import.Invoke(new Action(() =>
            {
                cbo_import.Enabled = true;
            }));
            dgv_list.Invoke(new Action(() => {
                disp_list(txt_search.Text);
            }));
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(openFile.FileName.ToString()))
            {
                MessageBox.Show("Please select and excel file to import.");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to import this excel?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    importBgWorker.RunWorkerAsync();
                }
            }
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
        public int get_cbo_index(ComboBox cbo)
        {
            int index = -1;
            cbo.Invoke(new Action(() =>
            {
                try { index = cbo.SelectedIndex; }
                catch { }
            }));
            return index;
        }
        public String get_cbo_value(ComboBox cbo)
        {
            String str = "";
            cbo.Invoke(new Action(() =>
            {
                try { str = cbo.SelectedValue.ToString(); }
                catch { }
            }));
            return str;
        }
        public String get_cbo_text(ComboBox cbo)
        {
            String str = "";
            cbo.Invoke(new Action(() =>
            {
                try { str = cbo.Text; }
                catch { }
            }));
            return str;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_auto_customer frm = new m_auto_customer();
            frm.ShowDialog();   
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

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void lbl_min_Click(object sender, EventArgs e)
        {

        }

        private void txt_filename_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void lbl_max_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void pbar_Click(object sender, EventArgs e)
        {

        }

        private void openFile_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                disp_list(txt_search.Text);
            }
        }

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void cbo_oldaccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
