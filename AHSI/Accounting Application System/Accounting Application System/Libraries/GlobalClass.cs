using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace Accounting_Application_System
{
    class GlobalClass
    {
        private static string l_user = null;
        private static string l_userfullname = null;
        private static string l_branch = null;
        private static string l_schema = null;
        private static DataTable l_gdt = null;
        private static DataRow l_gdr = null;
        private static DataGridView l_gdgv = null;
        private static DataGridViewRow l_gdgvRow = null;
        private static string l_projcomp = null;
        private static string l_server_ip = null;
        private static Boolean l_DontSendToMain = false;
        
        public static Boolean DontSendToMain
        {
            get { return l_DontSendToMain; }
            set { l_DontSendToMain = value; }
        }

        public static string projcompany
        {
            get { return l_projcomp; }
            set { l_projcomp = value; }
        }

        public static string username
        {
            get { return l_user; }
            set { l_user = value; }
        }

        public static string user_fullname
        {
            get { return l_userfullname; }
            set { l_userfullname = value; }
        }

        public static string branch
        {
            get { return l_branch; }
            set { l_branch = value; }
        }
        public static string server_ip
        {
            get { return l_server_ip; }
            set { l_server_ip = value; }
        }

        public static string schema
        {
            get { return l_schema; }
            set { l_schema = value; }
        }

        public static DataTable gdt
        {
            get { return l_gdt; }
            set { l_gdt = value; }
        }

        public static DataRow gdr
        {
            get { return l_gdr; }
            set { l_gdr = value; }
        }

        public static DataGridView gdgv
        {
            get { return l_gdgv; }
            set { l_gdgv = value; }
        }

        public static DataGridViewRow gdgvRow
        {
            get { return l_gdgvRow; }
            set { l_gdgvRow = value; }
        }

        public DataGridView CopyDataGridView(DataGridView dgv_org)
        {
            DataGridView dgv_copy = new DataGridView();
            try
            {
                if (dgv_copy.Columns.Count == 0)
                {
                    foreach (DataGridViewColumn dgvc in dgv_org.Columns)
                    {
                        dgv_copy.Columns.Add(dgvc.Clone() as DataGridViewColumn);
                    }
                }

                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < dgv_org.Rows.Count; i++)
                {
                    row = (DataGridViewRow)dgv_org.Rows[i].Clone();
                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in dgv_org.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_copy.Rows.Add(row);
                }
                dgv_copy.AllowUserToAddRows = false;
                dgv_copy.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//cf.ShowExceptionErrorMsg("Copy DataGridViw", ex);
            }

            return dgv_copy;
        }

        public void set_cbo_selectedvalue(ComboBox cbo, String selectedvalue)
        {
            //try
           // {
                if (String.IsNullOrEmpty(selectedvalue) == false)
                {
                    cbo.SelectedValue = selectedvalue;
                }
                else
                {
                    cbo.SelectedIndex = -1;
                }
            //}
            //catch (Exception) { cbo.SelectedIndex = -1; }
        }

        public void load_branch(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("branch", "code, name", "", " ORDER BY name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "name";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_room_type(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("rtype", "typ_code, typ_desc", "", " ORDER BY typ_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "typ_desc";
                cbo.ValueMember = "typ_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_rooms(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("rooms", "rom_code, rom_code ||' - '|| rom_desc AS rom_desc", "", " ORDER BY rom_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "rom_desc";
                cbo.ValueMember = "rom_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_journaltype(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m05type", "code, name", "", " ORDER BY name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "name";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_journal(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m05", "j_code, j_desc", "", " ORDER BY j_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "j_desc";
                cbo.ValueMember = "j_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_journal(ComboBox cbo, String type)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                String WHERE = "";
                if (!String.IsNullOrEmpty(type))
                {
                    WHERE = "j_type='" + type + "'";
                }

                dt = db.QueryOnTableWithParams("m05", "j_code, j_desc", WHERE, " ORDER BY j_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "j_desc";
                cbo.ValueMember = "j_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_journal_code_asc(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m05", "j_code, j_desc", "", " ORDER BY j_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "j_desc";
                cbo.ValueMember = "j_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_openperiod(ComboBox cbo, Boolean isOpen = false)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                String WHERE = "";
                if (isOpen)
                {
                    WHERE += " COALESCE(closed,'')<>'Y'";
                }

                dt = db.QueryOnTableWithParams("x03", "fy ||'-'|| mo AS mo, fy || ' - ' ||month_desc AS month_desc", WHERE, " ORDER BY \"from\" ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "month_desc";
                cbo.ValueMember = "mo";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_userid(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("x08", "uid", "", " ORDER BY uid ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "uid";
                cbo.ValueMember = "uid";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_userfullname(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("x08", "uid, opr_name", "", " ORDER BY uid ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "opr_name";
                cbo.ValueMember = "uid";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_customer(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.get_customer_list();
                cbo.DataSource = dt;
                cbo.DisplayMember = "d_name";
                cbo.ValueMember = "d_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_insurance(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m06", "d_code, d_name", "type='Insurance'", "ORDER BY d_name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "d_name";
                cbo.ValueMember = "d_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_financer(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m06", "d_code, d_name", "type='Financer'", "ORDER BY d_name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "d_name";
                cbo.ValueMember = "d_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_m00(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m00", "code, name", "", " ORDER BY code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "name";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_m01(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m01", "mag_code, mag_desc", "", " ORDER BY mag_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "mag_desc";
                cbo.ValueMember = "mag_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_m02(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m02", "cmp_code, cmp_desc", "", " ORDER BY cmp_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "cmp_desc";
                cbo.ValueMember = "cmp_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_subaccount(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m03", "acc_code, acc_desc", "", " ORDER BY acc_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "acc_desc";
                cbo.ValueMember = "acc_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_accounttitle(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m04", "at_code, at_desc", "", " ORDER BY at_desc ASC");

                db.CloseConn();
                cbo.DataSource = dt;
                cbo.DisplayMember = "at_desc";
                cbo.ValueMember = "at_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_accounttitle_sl_only(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m04", "at_code, at_desc", "sl='Y'", " ORDER BY at_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "at_desc";
                cbo.ValueMember = "at_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_accounttitle_payment_only(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m04", "at_code, at_desc", "payment='Y'", " ORDER BY at_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "at_desc";
                cbo.ValueMember = "at_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_mop(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m10", "mp_code, mp_desc", "", " ORDER BY mp_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "mp_desc";
                cbo.ValueMember = "mp_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_collector(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("collector", "coll_code, coll_name", "", " ORDER BY coll_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "coll_name";
                cbo.ValueMember = "coll_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_accountingperiod(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("x04", "mo, month_desc", "", " ORDER BY mo ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "month_desc";
                cbo.ValueMember = "mo";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        //for customer ledger 
        public void load_account_for_cust_ledger(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m04", "at_code, at_desc", "dr_cr='D' AND sl='Y'", " ORDER BY at_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "at_desc";
                cbo.ValueMember = "at_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_soa(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("soahdr", "soa_code", "", " ORDER BY soa_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "soa_code";
                cbo.ValueMember = "soa_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        //for supplier ledger
        public void load_account_for_sup_ledger(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m04", "at_code, at_desc", "dr_cr='C' AND sl='Y'", " ORDER BY at_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "at_desc";
                cbo.ValueMember = "at_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_costcenter(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m08", "cc_code, cc_desc", "", " ORDER BY cc_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "cc_desc";
                cbo.ValueMember = "cc_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_subsidiaryname(ComboBox cbo, String accttitle_code)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                String WHERE = "";
                Boolean ispayable = false;

                ispayable = db.is_liabilities(accttitle_code);

                if (ispayable == true)
                {
                    if (String.IsNullOrEmpty(accttitle_code) == false)
                    {
                        WHERE = " at_code='" + accttitle_code + "'";
                    }

                    dt = db.QueryOnTableWithParams("m07", "c_code, c_name", WHERE, " ORDER BY c_name ASC");
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "c_name";
                    cbo.ValueMember = "c_code";
                    cbo.SelectedIndex = -1;
                }
                else
                {
                    if (String.IsNullOrEmpty(accttitle_code) == false)
                    {
                        WHERE = " at_code='" + accttitle_code + "'";
                    }

                    dt = db.QueryOnTableWithParams("m06", "d_code, d_name", WHERE, " ORDER BY d_name ASC");
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "d_name";
                    cbo.ValueMember = "d_code";
                    cbo.SelectedIndex = -1;
                }
            }
            catch (Exception) { }
        }
        public void load_subsidiarycode(ComboBox cbo, String accttitle_code)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                String WHERE = "";
                Boolean ispayable = false;

                ispayable = db.is_liabilities(accttitle_code);

                if (ispayable == true)
                {
                    if (String.IsNullOrEmpty(accttitle_code) == false)
                    {
                        WHERE = " at_code='" + accttitle_code + "'";
                    }

                    dt = db.QueryOnTableWithParams("m07", "c_code, c_name", WHERE, " ORDER BY c_code ASC");
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "c_name";
                    cbo.ValueMember = "c_code";
                    cbo.SelectedIndex = -1;
                }
                else
                {
                    if (String.IsNullOrEmpty(accttitle_code) == false)
                    {
                        WHERE = " at_code='" + accttitle_code + "'";
                    }

                    dt = db.QueryOnTableWithParams("m06", "d_code, d_name", WHERE, " ORDER BY d_code ASC");
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "d_name";
                    cbo.ValueMember = "d_code";
                    cbo.SelectedIndex = -1;
                }
            }
            catch (Exception) { }
        }

        public void load_subcostcenter(ComboBox cbo, String costcenter_code)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                String WHERE = "";

                if (String.IsNullOrEmpty(costcenter_code) == false)
                {
                    WHERE = " cc_code='" + costcenter_code + "'";
                }

                dt = db.QueryOnTableWithParams("subctr", "scc_code, scc_desc", WHERE, " ORDER BY scc_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "scc_desc";
                cbo.ValueMember = "scc_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_payee(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("payee", "payee", "", " ORDER BY payee ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "payee";
                cbo.ValueMember = "payee";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_hkcharge(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("charge", "chg_code, chg_desc", "chg_type='C' AND (UPPER(utility)='ELECTRICITY' OR UPPER(utility)='WATER')", " ORDER BY chg_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "chg_desc";
                cbo.ValueMember = "chg_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_terms(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m10", "mp_code, mp_desc", "", "");
                cbo.DataSource = dt;
                cbo.DisplayMember = "mp_desc";
                cbo.ValueMember = "mp_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_whouse(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("whouse", "whs_code, whs_desc", "", "");
                cbo.DataSource = dt;
                cbo.DisplayMember = "whs_desc";
                cbo.ValueMember = "whs_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_poinvoice_nonjrnlz(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("pinvhd", "inv_num, inv_num || ' - ' || supl_name || ' - ' || reference AS po_desc", "jrnlz!='Y' AND COALESCE(cancel,'')='N'", "ORDER BY inv_num ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "po_desc";
                cbo.ValueMember = "inv_num";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_poinvoice_return_nonjrnlz(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("prethdr", "pret_num, pret_num || ' - ' || reference || ' - ' || supl_name  AS pret_desc", "(jrnlz='N' OR jrnlz is null) AND COALESCE(cancel,'')<>'' ", "ORDER BY pret_num ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "pret_desc";
                cbo.ValueMember = "pret_num";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_stktransfer_invoice(ComboBox cbo, String whs_code)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("rechdr", "rec_num, rec_num || ' - ' || trnx_date || ' - ' || reference  AS rec_desc", "whs_code='" + whs_code + "' AND trn_type='T' AND COALESCE(cancel,'')<>'Y'", "ORDER BY rec_num ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "rec_desc";
                cbo.ValueMember = "rec_num";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        
        public int ToInt(String month)
        {
            try
            {
                return DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            return 0;
        }

        public String toAccountingFormat(Double amt)
        {
            try
            {
                return amt.ToString("#,##0.00;(#,##0.00);0.00");
            }
            catch (Exception er) { MessageBox.Show(er.Message);  }

            return "";
        }

        public Double toNormalDoubleFormat(String acct_amt)
        {
            Double amt = 0.00;

            try
            {
                if(String.IsNullOrEmpty(acct_amt))
                {
                    return 0.00;
                }
                else if (acct_amt.Contains("(") && acct_amt.Contains(")"))
                {
                    amt = Double.Parse(acct_amt, NumberStyles.AllowParentheses |
                                      NumberStyles.AllowThousands |
                                      NumberStyles.AllowDecimalPoint);
                }
                else
                    amt = Convert.ToDouble(acct_amt);
            }
            catch (Exception er) 
            { 
                //MessageBox.Show(er.Message); 
                amt = 0.00;
            }
            
            return amt;
        }


        //inventory
        public void load_vat(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("vat", "vat_code, vat_desc", "", "");
                cbo.DataSource = dt;
                cbo.DisplayMember = "vat_desc";
                cbo.ValueMember = "vat_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_saleunit(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("itmunit", "unit_id, unit_desc", "", " ORDER BY unit_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "unit_desc";
                cbo.ValueMember = "unit_id";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_unit_with_desc(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("itmunit", "unit_id, unit_shortcode ||' - '|| unit_desc AS unit_desc", "", " ORDER BY unit_shortcode ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "unit_desc";
                cbo.ValueMember = "unit_id";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_account_title(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m04", "at_code, at_desc", "", " ORDER BY at_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "at_desc";
                cbo.ValueMember = "at_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_account_title_code_asc(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m04", "at_code, at_desc", "", " ORDER BY at_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "at_desc";
                cbo.ValueMember = "at_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_stocklocation(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.get_location_list();
                cbo.DataSource = dt;
                cbo.DisplayMember = "whs_desc";
                cbo.ValueMember = "whs_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_supplier(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m07", "c_code, c_name", "", " ORDER BY c_name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "c_name";
                cbo.ValueMember = "c_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_modeofpayment(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m10", "mp_code, mp_desc", "", " ORDER BY mp_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "mp_desc";
                cbo.ValueMember = "mp_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_category_asc_desc(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("itmgrp", "item_grp, grp_desc", "", " ORDER BY grp_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "grp_desc";
                cbo.ValueMember = "item_grp";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_category(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("itmgrp", "item_grp, grp_desc", "", " ORDER BY item_grp ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "grp_desc";
                cbo.ValueMember = "item_grp";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_itemclass_discription(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("itmclass", "code, description", "", " ORDER BY code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "description";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_itemclass_shortcode(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("itmclass", "code, shortcode", "", " ORDER BY code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "shortcode";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_generic(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("generic", "gen_code, gen_name", "COALESCE(cancel,'')<>'Y'", " ORDER BY gen_name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "gen_name";
                cbo.ValueMember = "gen_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_brand(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("brand", "brd_code, brd_name", "COALESCE(cancel,'')<>'Y'", " ORDER BY brd_name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "brd_name";
                cbo.ValueMember = "brd_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        /*

        public void load_terms(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m10", "mp_code, mp_desc", "isterms='Y' AND (cancel IS NULL OR cancel!='Y')", " ORDER BY mp_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "mp_desc";
                cbo.ValueMember = "mp_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_vat(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("vat", "vat_code, vat_desc", "cancel IS NULL OR cancel!='Y'", "");
                cbo.DataSource = dt;
                cbo.DisplayMember = "vat_desc";
                cbo.ValueMember = "vat_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }*/
        public void load_salesclerk(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.get_salesrep_list_with_canc();
                cbo.DataSource = dt;
                cbo.DisplayMember = "rep_name";
                cbo.ValueMember = "rep_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_salesagent(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.get_salesagent_list();
                cbo.DataSource = dt;
                cbo.DisplayMember = "name";
                cbo.ValueMember = "id";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        /*
        public static int ToInt(String month)
        {
            try
            {
                return DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            return 0;
        } */

        public void load_vehicle_item(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("items", "item_code, item_desc", "item_grp='00002'", " ORDER BY item_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "item_desc";
                cbo.ValueMember = "item_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_vehicle_info(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("vehicle_info", "vin_desc,vin_no", "", " ORDER BY vin_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "vin_desc";
                cbo.ValueMember = "vin_no";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }

        }
        public void load_item_asc_desc(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("items", "item_code, item_desc", "", " ORDER BY item_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "item_desc";
                cbo.ValueMember = "item_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_item(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("items", "item_code, item_desc", "", " ORDER BY item_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "item_desc";
                cbo.ValueMember = "item_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_charge(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("charge", "chg_code, chg_desc", "", " ORDER BY chg_type ASC, chg_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "chg_desc";
                cbo.ValueMember = "chg_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_paymenttype(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m10", "mp_code, mp_desc", "", " ORDER BY mp_code ASC, mp_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "mp_desc";
                cbo.ValueMember = "mp_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_outlet(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryBySQLCode("SELECT b.name || ' - ' || o.out_desc AS out_desc, o.out_code FROM rssys.outlet o LEFT JOIN rssys.branch b ON o.branch=b.code ORDER BY out_code;");
                cbo.DataSource = dt;
                cbo.DisplayMember = "out_desc";
                cbo.ValueMember = "out_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_outlet_pos(ComboBox cbo, Boolean isCurrentBranchOnly)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                String add_cond = "";

                if (isCurrentBranchOnly)
                {
                    add_cond = " AND branch='" + GlobalClass.branch + "'";
                }
                dt = db.QueryBySQLCode("SELECT b.name || ' - ' || o.out_desc AS out_desc, o.out_code FROM rssys.outlet o LEFT JOIN rssys.branch b ON o.branch=b.code WHERE ottyp='POS'" + add_cond +" ORDER BY out_code;");

                cbo.DataSource = dt;
                cbo.DisplayMember = "out_desc";
                cbo.ValueMember = "out_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_outlet_carsale(ComboBox cbo, Boolean isCurrentBranchOnly)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                String add_cond = "";

                if (isCurrentBranchOnly)
                {
                    add_cond = " AND branch='" + GlobalClass.branch + "'";
                }
                dt = db.QueryBySQLCode("SELECT b.name || ' - ' || o.out_desc AS out_desc, o.out_code FROM rssys.outlet o LEFT JOIN rssys.branch b ON o.branch=b.code WHERE ottyp='CS'" + add_cond + " ORDER BY out_code;");
                
                cbo.DataSource = dt;
                cbo.DisplayMember = "out_desc";
                cbo.ValueMember = "out_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_outlet_repair(ComboBox cbo, Boolean isCurrentBranchOnly)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                String add_cond = "";

                if (isCurrentBranchOnly)
                {
                    add_cond = " AND branch='" + GlobalClass.branch + "'";
                }
                dt = db.QueryBySQLCode("SELECT b.name || ' - ' || o.out_desc AS out_desc, o.out_code FROM rssys.outlet o LEFT JOIN rssys.branch b ON o.branch=b.code WHERE ottyp='R'" + add_cond + " ORDER BY out_code;");
                
                cbo.DataSource = dt;
                cbo.DisplayMember = "out_desc";
                cbo.ValueMember = "out_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_orhdr_inv_not_jrnlz(ComboBox cbo, String out_code)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("orhdr", "ord_code", " jrnlz IS NULL OR jrnlz!='Y' ", " ORDER BY ord_code ASC, out_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "ord_code";
                cbo.ValueMember = "ord_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_pinvhd_inv_not_jrnlz(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("pinvhd", "inv_num", " jrnlz IS NULL OR jrnlz!='Y' ", " ORDER BY inv_num ASC, out_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "inv_num";
                cbo.ValueMember = "inv_num";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_pr_code(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("prhdr", "pr_code AS pr_code, pr_code ||' - '||reference ||' - '|| pr_date AS pr_desc", " COALESCE(cancel,'')!='Y' ", " ORDER BY pr_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "pr_desc";
                cbo.ValueMember = "pr_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_po_number(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("purhdr", "purc_ord AS pcode, purc_ord ||' - '|| supl_name AS cname", "", " ORDER BY purc_ord ASC");
                
                //dt = db.QueryBySQLCode("SELECT p.purc_ord AS pcode, p.purc_ord ||' - '|| m7.c_name AS cname FROM " + db.get_schema() + ".purhdr p LEFT JOIN " + db.get_schema() + ".m07 m7 ON p.supl_name=m7.c_code ORDER BY p.purc_ord");
                cbo.DataSource = dt;
                cbo.DisplayMember = "cname";
                cbo.ValueMember = "pcode";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_rr_number(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("rechdr", "rec_num AS rec_num, rec_num ||' - '||_reference AS _reference", "trn_type='P' AND COALESCE(cancel,'') !='Y'", "ORDER BY rec_num"); 
                cbo.DataSource = dt;
                cbo.DisplayMember = "_reference";
                cbo.ValueMember = "rec_num";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_dr_number(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("pinvhd", "inv_num, reference", "COALESCE(cancel,'') != 'Y'", " ORDER BY inv_num ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "reference";
                cbo.ValueMember = "inv_num";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_si_number(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("rechdr", "rec_num AS rec_num, _reference", "trn_type='I' AND COALESCE(cancel,'') !='Y'", "ORDER BY rec_num");
                cbo.DataSource = dt;
                cbo.DisplayMember = "_reference";
                cbo.ValueMember = "rec_num";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_tra_number(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("rechdr", "rec_num AS rec_num, _reference", "trn_type='T' AND COALESCE(cancel,'') !='Y'", "ORDER BY rec_num");
                cbo.DataSource = dt;
                cbo.DisplayMember = "_reference";
                cbo.ValueMember = "rec_num";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_adj_number(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("rechdr", "rec_num AS rec_num, _reference", "trn_type='A' AND COALESCE(cancel,'') !='Y'", "ORDER BY rec_num");
                cbo.DataSource = dt;
                cbo.DisplayMember = "_reference";
                cbo.ValueMember = "rec_num";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_color(ComboBox cbo)
        { 
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("color", "id, color_desc", "", "ORDER BY color_desc");
                cbo.DataSource = dt;
                cbo.DisplayMember = "color_desc";
                cbo.ValueMember = "id";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_cartype(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("cartype", "id, ctyp_desc", "", "ORDER BY ctyp_desc");
                cbo.DataSource = dt;
                cbo.DisplayMember = "ctyp_desc";
                cbo.ValueMember = "id";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_marketsegment(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("market", "mkt_code, mkt_desc", "", "ORDER BY mkt_code");
                cbo.DataSource = dt;
                cbo.DisplayMember = "mkt_desc";
                cbo.ValueMember = "mkt_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_discount(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("disctbl", "disc_code, disc_desc", "", "ORDER BY disc_code");
                cbo.DataSource = dt;
                cbo.DisplayMember = "disc_desc";
                cbo.ValueMember = "disc_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_company_acct(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_code");
                cbo.DataSource = dt;
                cbo.DisplayMember = "comp_name";
                cbo.ValueMember = "comp_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_downpayment(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("downpayment", "dpcode, dpdesc", "", "ORDER BY dpcode");
                cbo.DataSource = dt;
                cbo.DisplayMember = "dpdesc";
                cbo.ValueMember = "dpcode";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_bank(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("bank", "b_code, b_name", "", "ORDER BY b_name");
                cbo.DataSource = dt;
                cbo.DisplayMember = "b_name";
                cbo.ValueMember = "b_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_servicetype(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("servicetype", "st_code, st_desc", "", "");
                cbo.DataSource = dt;
                cbo.DisplayMember = "st_desc";
                cbo.ValueMember = "st_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_repair_orderstatus(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("ro_status", "ro_stat_code, ro_stat_desc", "", " ORDER BY ro_stat_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "ro_stat_desc";
                cbo.ValueMember = "ro_stat_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_technician(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("technician", "tech_code, tech_name", "", " ORDER BY tech_name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "tech_name";
                cbo.ValueMember = "tech_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_service(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("service", "srv_code, srv_name", "", " ORDER BY srv_name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "srv_name";
                cbo.ValueMember = "srv_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_outlet_type(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("outlettyp", "ottyp, ot_desc", "", " ORDER BY ot_desc ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "ot_desc";
                cbo.ValueMember = "ottyp";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_work(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("service", "srv_code,srv_name", "", " ORDER BY srv_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "srv_name";
                cbo.ValueMember = "srv_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_decision(ComboBox cbo)
        {

            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("decision", "decision_code, decision_name", "", " ORDER BY decision_name ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "decision_name";
                cbo.ValueMember = "decision_code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }

        }
        public void load_registered_customer(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m06", "d_code as code, d_name as name", "", " ORDER BY d_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "name";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }
        public void load_crm(ComboBox cbo)
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("m06", "d_code as code, d_name AS name", "", " ORDER BY d_code ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "name";
                cbo.ValueMember = "code";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public void load_membership(ComboBox cbo)
        {

            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("ecard", "cardno, cardtype", "", " ORDER BY cardtype ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "cardtype";
                cbo.ValueMember = "cardno";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }

        }
            public void load_priority(ComboBox cbo)
        {

            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("priority", "priority_no, priority_desc", "", " ORDER BY priority_no ASC");
                cbo.DataSource = dt;
                cbo.DisplayMember = "priority_desc";
                cbo.ValueMember = "priority_no";
                cbo.SelectedIndex = -1;
            }
            catch (Exception) { }

        }
            public void load_contact(ComboBox cbo)
            {

                try
                {
                    DataTable dt = new DataTable();
                    thisDatabase db = new thisDatabase();

                    //dt = db.QueryOnTableWithParams("m0", "priority_no, priority_desc", "", " ORDER BY priority_no ASC");
                    dt = db.QueryBySQLCode("select distinct m.* from rssys.m06 m left join rssys.orhdr o on m.d_code=o.debt_code WHERE m.d_code=o.debt_code ORDER by m.d_code ASC");
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "d_name";
                    cbo.ValueMember = "d_code";
                    cbo.SelectedIndex = -1;
                }
                catch (Exception) { }

            }

            public void load_account_title_payment(ComboBox cbo)
            {
                try
                {
                    DataTable dt = new DataTable();
                    thisDatabase db = new thisDatabase();

                    dt = db.QueryOnTableWithParams("m04", "at_code, at_desc", "COALESCE(payment,'N')='Y'", " ORDER BY at_code ASC");
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "at_desc";
                    cbo.ValueMember = "at_code";
                    cbo.SelectedIndex = -1;
                }
                catch (Exception) { }
            }
            public void load_account_title_check_writting(ComboBox cbo)
            {
                try
                {
                    DataTable dt = new DataTable();
                    thisDatabase db = new thisDatabase();

                    dt = db.QueryOnTableWithParams("m04", "at_code, at_desc", "COALESCE(cib_acct,'N')='Y'", " ORDER BY at_code ASC");
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "at_desc";
                    cbo.ValueMember = "at_code";
                    cbo.SelectedIndex = -1;
                }
                catch (Exception) { }
            }

            // by Paul for SOA SUMMARY
            public void load_soa_period(ComboBox cbo)
            {
                try
                {
                    DataTable dt = new DataTable();
                    thisDatabase db = new thisDatabase();

                    dt = db.QueryOnTableWithParams("soahdr", "DISTINCT soa_period", "", " ORDER BY soa_period ");
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "soa_period";
                    cbo.ValueMember = "soa_period";
                    cbo.SelectedIndex = -1;
                }
                catch (Exception) { }
            }
    }
}
