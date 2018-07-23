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
    public partial class m_auto_customer : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalMethod gm = new GlobalMethod();
        GlobalClass gc = new GlobalClass();

        auto_loanapplication _frm_auto;
        s_Sales_Auto _frm_sales_auto;
        s_Release_Deliver_Unit _frm_reldel_unit;
        s_GP_Computation frm_gp_compute;
        s_Job_Quotation frm_jq;

        Boolean seltbp = false;
        Boolean isnew = false;

        int lastpage = 0;

        public m_auto_customer()
        {
            InitializeComponent();

            gc.load_branch(cbo_branch);

            load_cbo_worker.RunWorkerAsync();
            btn_vpage.Text = "1";
            thisDatabase db = new thisDatabase();
            String grp_id = "";
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0402' ORDER BY b.pla, b.mod_id");

            if (dt2.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt2.Rows[0]["add"].ToString();
                update = dt2.Rows[0]["upd"].ToString();
                delete = dt2.Rows[0]["cancel"].ToString();
                print = dt2.Rows[0]["print"].ToString();

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
            disp_list();
        }
        public m_auto_customer(auto_loanapplication auto, Boolean iscallback) : this()
        {
            _frm_auto = auto;
        }
        public m_auto_customer(s_Sales_Auto sauto, Boolean iscallback) : this()
        {
            _frm_sales_auto = sauto;
        }
        public m_auto_customer(s_Job_Quotation frm, Boolean iscallback): this()
        {
            frm_jq = frm;
        }

        public m_auto_customer(s_GP_Computation frm, Boolean iscallback): this()
        {
            frm_gp_compute = frm;
        }
        public m_auto_customer(s_Release_Deliver_Unit frm, Boolean iscallback) : this()
        {
            _frm_reldel_unit = frm;
        }

        private void m_auto_customer_Load(object sender, EventArgs e){}

        private void load_civi_status()
        {
            DataTable dt = new DataTable();
            dt = db.QueryBySQLCode("SELECT * FROM rssys.hr_civil_status");
            cbo_civil_status.DataSource = dt;
            cbo_civil_status.DisplayMember = "description";
            cbo_civil_status.ValueMember = "code";
            cbo_civil_status.SelectedIndex = -1;
        }
        private void load_country()
        {
            DataTable dt = new DataTable();
            dt = db.QueryBySQLCode("SELECT * FROM rssys.country");
            cbo_country.DataSource = dt;
            cbo_country.DisplayMember = "cntry_desc";
            cbo_country.ValueMember = "cntry_code";
            cbo_country.SelectedIndex = -1;
        }
        private void load_nationality()
        {
            DataTable dt = new DataTable();
            dt = db.QueryBySQLCode("SELECT * FROM rssys.nationality");
            cbo_nationality.DataSource = dt;
            cbo_nationality.DisplayMember = "nat_desc";
            cbo_nationality.ValueMember = "nat_code";
            cbo_nationality.SelectedIndex = -1;
        }
        //private void load_city()
        //{
        //    DataTable dt = new DataTable();
        //    dt = db.QueryBySQLCode("SELECT * FROM rssys.city");
        //    cbo_city.DataSource = dt;
        //    cbo_city.DisplayMember = "city_name";
        //    cbo_city.ValueMember = "city_code";
        //    cbo_city.SelectedIndex = -1;
        //}
        private void auto_completemode()
        {
            txt_lname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_lname.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void disp_list()
        {

            DataTable dt = get_customer_pagination();
            if (dt != null)
            {
                try
                {
                    dgv_list.Rows.Clear();
                }
                catch (Exception)
                { }

                try
                {
                    //dt = db.QueryBySQLCode("SELECT d_name,d_code,lastname,firstname,d_addr2,mname,d_cntc_no,d_tel,fax,d_email,d_tin,remarks FROM rssys.m06");

                    for (int r = 0; dt.Rows.Count > r; r++)
                    {
                        int i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["code"].Value = dt.Rows[r]["d_code"].ToString();
                        row.Cells["cust_name"].Value = dt.Rows[r]["firstname"].ToString() + " " + dt.Rows[r]["mname"].ToString() + " " + dt.Rows[r]["lastname"].ToString();
                        if (String.IsNullOrEmpty(dt.Rows[r]["firstname"].ToString()))
                        {
                            row.Cells["cust_name"].Value = dt.Rows[r]["d_name"].ToString();
                        }
                        row.Cells["address"].Value = dt.Rows[r]["d_addr2"].ToString();
                        row.Cells["mobile"].Value = dt.Rows[r]["d_cntc_no"].ToString();
                        row.Cells["phone"].Value = dt.Rows[r]["d_tel"].ToString();
                        row.Cells["fax"].Value = dt.Rows[r]["fax"].ToString();
                        row.Cells["email"].Value = dt.Rows[r]["d_email"].ToString();
                        row.Cells["tin"].Value = dt.Rows[r]["d_tin"].ToString();
                        row.Cells["remarks"].Value = dt.Rows[r]["remarks"].ToString();
                        i++;
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
        }
        private void goto_win2()
        {
            seltbp = true;
            tgp_main.SelectedTab = tgp_info;
            tbcntrl_left.SelectedTab = tpg_option_2;

            tgp_info.Show();
            tpg_option_2.Show();
            seltbp = false;
        }

        private void goto_win1()
        {
            seltbp = true;
            tgp_main.SelectedTab = tgp_list;
            tbcntrl_left.SelectedTab = tpg_option;

            tgp_list.Show();
            tpg_option.Show();
            seltbp = false;
        }

        private void tgp_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
            {
                e.Cancel = true;
            }
        }

        private void tbcntrl_left_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
            {
                e.Cancel = true;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            clear_form();

            goto_win2();
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            goto_win1();
        }

        private void txt_lname_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {


            String col = "", val = "", add_col = "", add_val = "";
            String code = "", title = "", lastname = "", firstname = "", mname = "", bdate = "", sex = "", civil_status = "", home_address = "", resi_owned = "", city = "", zip = "", country = "", length_stay_years = "", emp_busines_name = "", business_nature = "", emp_busi_email = "", type = "", remarks = "", tin = "", sss = "", landline = "", mobile1 = "", mobile2 = "", email = "", facebook = "", is_textblast = "", is_email = "", years_w_company = "", gross_m_income = "", position_title = "", work_landline = "", fax = "", work_mobile = "", co_maker_name = "", co_m_relation = "", co_m_home_add = "", com_m_bdate = "", co_m_sex = "", co_email = "", co_mobile = "", co_tin = "", co_sss = "", co_emp_business_name = "", co_business_add = "", co_nature_of_buss = "", co_business_emp_email = "", co_years_n_comp = "", co_gross_m_income = "", co_pos_title = "", co_landline = "", co_work_mobile = "", work_business_work_address = "", nationality = "", d_branch = "";
            String d_name = "";

            String table = "m06";
            Boolean success = false;


            if (dtp_dob.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                MessageBox.Show("Date of birth is equal today.");
                return;
            }
            if (txt_lname.Text == "")
            {
                MessageBox.Show("Pls enter the required fields.");
                return;
            }
            if (txt_fname.Text == "")
            {
                MessageBox.Show("Pls enter the required fields.");
                return;
            }
            if (rtxt_home_address.Text == "")
            {
                MessageBox.Show("Pls enter the required fields.");
                return;
            }
            if (cbo_gender.SelectedIndex == -1)
            {
                MessageBox.Show("Pls enter the required fields.");
                return;
            }
            if (txt_mobile1.Text == "")
            {
                MessageBox.Show("Pls enter the required fields.");
                return;
            }
            if (txt_email.Text == "")
            {
                MessageBox.Show("Pls enter the required fields.");
                return;
            }
            code = txt_code.Text;
            if (cbo_title.SelectedIndex != -1)
            {
                title = cbo_title.SelectedItem.ToString();
            }

            lastname = txt_lname.Text.Trim();
            firstname = txt_fname.Text.Trim();
            mname = txt_mname.Text.Trim();

            if (!String.IsNullOrEmpty(firstname) && !String.IsNullOrEmpty(lastname)) d_name = lastname + ", " + firstname;
            else if (!String.IsNullOrEmpty(firstname)) d_name = firstname;
            else if (!String.IsNullOrEmpty(lastname)) d_name = lastname;
            if (!String.IsNullOrEmpty(mname))
                d_name += " " + mname;

            d_branch = (cbo_branch.SelectedValue??"").ToString();

            bdate = gm.toDateString(dtp_dob.Value.ToShortDateString(), "");
            if (cbo_gender.SelectedIndex != -1)
            {
                sex = cbo_gender.SelectedItem.ToString();
            }

            if (cbo_civil_status.SelectedIndex != -1)
            {
                civil_status = cbo_civil_status.SelectedItem.ToString();
            }

            home_address = rtxt_home_address.Text;

            if (chk_is_owned.Checked)
            {
                resi_owned = "1";
            }
            //if (cbo_city.SelectedIndex != -1)
            //{
            //    city = cbo_city.Text;
            //}
            //else
            //{
            //    city = cbo_city.Text;
            //}
            city = txt_city.Text;
            zip = txt_zip.Text;
            if (cbo_country.SelectedIndex != -1)
            {
                country = cbo_country.SelectedValue.ToString();
            }
            if (cbo_nationality.SelectedIndex != -1)
            {
                nationality = cbo_nationality.SelectedValue.ToString();
            }

            length_stay_years = txt_length_of_stay_years.Text;
            emp_busines_name = txt_emp_busines_name.Text;
            work_business_work_address = txt_work_business_work_address.Text;
            business_nature = txt_w_business.Text;
            emp_busi_email = txt_emp_busi_email.Text;
            if (cbo_type.SelectedIndex != -1)
            {
                type = cbo_type.Text;
            }

            remarks = rtxt_remarks.Text;
            tin = txt_tin.Text;
            sss = txt_sss.Text;
            landline = txt_landline.Text;
            mobile1 = txt_mobile1.Text;
            mobile2 = txt_mobile2.Text;
            email = txt_email.Text;
            facebook = txt_facebook.Text;

            if (chk_is_textblast.Checked)
            {
                is_textblast = "1";
            }
            else
            {
                is_textblast = "0";
            }
            if (chk_is_emailblast.Checked)
            {
                is_email = "1";
            }
            else
            {
                is_email = "0";
            }
            years_w_company = txt_years_w_company.Text;
            gross_m_income = gm.toNormalDoubleFormat(txt_gross_m_income.Text).ToString();

            position_title = txt_w_position_title.Text;


            work_landline = txt_worklandline.Text;
            fax = txt_fax.Text;
            work_mobile = txt_work_mobile.Text;
            co_maker_name = txt_co_maker_name.Text;

            co_m_relation = txt_relation.Text;


            co_m_home_add = rtxt_cm_address.Text;
            com_m_bdate = gm.toDateString(dtp_cm_bdate.Value.ToShortDateString(), "");
            co_m_sex = cbo_cm_status.Text;
            if (cbo_cm_status.SelectedIndex != -1)
            {
                co_m_sex = cbo_cm_status.SelectedItem.ToString();
            }

            co_email = txt_co_email.Text;
            co_mobile = txt_co_mobile.Text;
            co_tin = txt_co_tin.Text;
            co_sss = txt_cm_sss.Text;
            co_emp_business_name = txt_cm_bus_name.Text;
            co_business_add = rtxt_bus_address.Text;

            co_nature_of_buss = txt_co_nature_business.Text;


            co_business_emp_email = txt_co_business_emp_email.Text;
            co_years_n_comp = txt_co_years_n_comp.Text;
            co_gross_m_income = gm.toNormalDoubleFormat(txt_co_gross_m_income.Text).ToString();
            if (cbo_co_pos_title.SelectedIndex != -1)
            {
                co_pos_title = cbo_co_pos_title.SelectedItem.ToString();
            }
            co_landline = txt_co_landline.Text;
            co_work_mobile = txt_co_work_mobile.Text;

            if (isnew)
            {

                col = "d_name, d_code, title, lastname, firstname, mname, bdate, sex,civil_status, d_addr2, resi_owned , city, area_code, country, length_stay_years, emp_busines_name, business_nature, emp_busi_email , type, remarks, d_tin, sss, d_tel , d_cntc_no, mobile2 , d_email, facebook, is_textblast, is_email, years_w_company , gross_m_income , position_title , work_landline, fax , work_mobile, co_maker_name , co_m_relation , co_m_home_add , com_m_bdate , co_m_sex , co_email , co_mobile , co_tin , co_sss, co_emp_business_name, co_business_add , co_nature_of_buss , co_business_emp_email , co_years_n_comp, co_gross_m_income, co_pos_title, co_landline, co_work_mobile,work_business_work_address,nationality,d_branch";

                code = db.get_pk("d_code");

                val = "" + db.str_E(d_name) + ",'" + code + "'," + db.str_E(title) + "," + db.str_E(lastname) + "," + db.str_E(firstname) + "," + db.str_E(mname) + "," + db.str_E(bdate) + "," + db.str_E(sex) + "," + db.str_E(civil_status) + "," + db.str_E(home_address) + "," + resi_owned + "," + db.str_E(city) + "," + db.str_E(zip) + "," + db.str_E(country) + "," + db.str_E(length_stay_years) + "," + db.str_E(emp_busines_name) + "," + db.str_E(business_nature) + "," + db.str_E(emp_busi_email) + "," + db.str_E(type) + "," + db.str_E(remarks) + "," + db.str_E(tin) + "," + db.str_E(sss) + "," + db.str_E(landline) + "," + db.str_E(mobile1) + "," + db.str_E(mobile2) + "," + db.str_E(email) + "," + db.str_E(facebook) + "," + db.str_E(is_textblast) + "," + db.str_E(is_email) + "," + db.str_E(years_w_company) + "," + db.str_E(gross_m_income) + "," + db.str_E(position_title) + "," + db.str_E(work_landline) + "," + db.str_E(fax) + "," + db.str_E(work_mobile) + "," + db.str_E(co_maker_name) + "," + db.str_E(co_m_relation) + "," + db.str_E(co_m_home_add) + ",'" + com_m_bdate + "'," + db.str_E(co_m_sex) + "," + db.str_E(co_email) + "," + db.str_E(co_mobile) + "," + db.str_E(co_tin) + "," + db.str_E(co_sss) + "," + db.str_E(co_emp_business_name) + "," + db.str_E(co_business_add) + "," + db.str_E(co_nature_of_buss) + "," + db.str_E(co_business_emp_email) + "," + db.str_E(co_years_n_comp) + "," + db.str_E(co_gross_m_income) + "," + db.str_E(co_pos_title) + "," + db.str_E(co_landline) + "," + db.str_E(co_work_mobile) + "," + db.str_E(work_business_work_address) + "," + db.str_E(nationality) + ",'" + d_branch + "'";

                if (db.InsertOnTable(table, col, val))
                {
                    db.set_pkm99("d_code", db.get_nextincrementlimitchar(code, 6));
                    db.set_pkm99("acct_no", db.get_nextincrementlimitchar(code, 6));
                    //db.set_pkm99("addressbook_code", db.get_nextincrementlimitchar(code, 6));

                    String value = "'" + code + "'," + db.str_E(title) + "," + db.str_E(lastname) + "," + db.str_E(firstname) + "," + db.str_E(mname) + ", " + db.str_E(d_name) + "," + db.str_E(sex.Substring(0, 1)) + "," + db.str_E(bdate) + "," + db.str_E(home_address) + "," + db.str_E(landline) + "," + db.str_E(txt_email.Text) + ",'','','1950-01-01','1950-01-01','" + (cbo_country.SelectedValue ?? "").ToString() + "','101', '', '" + db.get_systemdate("") + "','" + DateTime.Now.ToString("HH:mm") + "','" + GlobalClass.username + "'";
                    db.InsertOnTable("guest", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, mp_code, escaper, t_date, t_time, user_id", value);

                    //db.InsertOnTable("city", "city_name", "'" + city + "'");
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
                col = "d_name=" + db.str_E(d_name) + ",title=" + db.str_E(title) + ",lastname=" + db.str_E(lastname) + ",firstname=" + db.str_E(firstname) + ",mname=" + db.str_E(mname) + ",bdate=" + db.str_E(bdate) + ",sex=" + db.str_E(sex) + ",civil_status=" + db.str_E(civil_status) + ",d_addr2=" + db.str_E(home_address) + ",resi_owned=" + resi_owned + ",city=" + db.str_E(city) + ",area_code=" + db.str_E(zip) + ",country=" + db.str_E(country) + ",length_stay_years=" + db.str_E(length_stay_years) + ",emp_busines_name=" + db.str_E(emp_busines_name) + ",business_nature=" + db.str_E(business_nature) + ",emp_busi_email=" + db.str_E(emp_busi_email) + ",type=" + db.str_E(type) + ",remarks=" + db.str_E(remarks) + ",d_tin=" + db.str_E(tin) + ",sss=" + db.str_E(sss) + ",d_tel=" + db.str_E(landline) + ",d_cntc_no=" + db.str_E(mobile1) + ",mobile2=" + db.str_E(mobile2) + ",d_email=" + db.str_E(email) + ",facebook=" + db.str_E(facebook) + ",is_textblast=" + is_textblast + ",is_email=" + db.str_E(is_email) + ",years_w_company=" + db.str_E(years_w_company) + ",gross_m_income=" + db.str_E(gross_m_income) + ",position_title=" + db.str_E(position_title) + ",work_landline=" + db.str_E(work_landline) + ",fax=" + db.str_E(fax) + ",work_mobile=" + db.str_E(work_mobile) + ",co_maker_name=" + db.str_E(co_maker_name) + ",co_m_relation=" + db.str_E(co_m_relation) + ",co_m_home_add=" + db.str_E(co_m_home_add) + ",com_m_bdate='" + com_m_bdate + "',co_m_sex=" + db.str_E(co_m_sex) + ",co_email=" + db.str_E(co_email) + ",co_mobile=" + db.str_E(co_mobile) + ",co_tin=" + db.str_E(co_tin) + ",co_sss=" + db.str_E(co_sss) + ",co_emp_business_name=" + db.str_E(co_emp_business_name) + ",co_business_add=" + db.str_E(co_business_add) + ",co_nature_of_buss=" + db.str_E(co_nature_of_buss) + ",co_business_emp_email=" + db.str_E(co_business_emp_email) + ",co_years_n_comp=" + db.str_E(co_years_n_comp) + ",co_gross_m_income='" + co_gross_m_income + "',co_pos_title=" + db.str_E(co_pos_title) + ",co_landline=" + db.str_E(co_landline) + ",co_work_mobile=" + db.str_E(co_work_mobile) + ",work_business_work_address=" + db.str_E(work_business_work_address) + ",nationality=" + db.str_E(nationality) + ",d_branch='" + d_branch + "'";

                if (db.UpdateOnTable(table, col, "d_code='" + code + "'"))
                {


                    db.UpdateOnTable("guest", "title=" + db.str_E(title) + ", last_name=" + db.str_E(lastname) + ", first_name=" + db.str_E(firstname) + ", mid_name=" + db.str_E(mname) + ", full_name=" + db.str_E(d_name) + ", gender=" + db.str_E(sex.Substring(0,1)) + ", birth_date=" + db.str_E(bdate) + ", address1=" + db.str_E(home_address) + ", tel_num=" + db.str_E(landline) + ", email=" + db.str_E(txt_email.Text) + ", comp_code='', passport_no='', passport_issued='1950-01-01', passport_expiry='1950-01-01', cntry_code='" + (cbo_country.SelectedValue ?? "").ToString() + "', mp_code='101', escaper='', t_date='" + db.get_systemdate("") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "', user_id='" + GlobalClass.username + "'", "acct_no='" + code + "'");

                    success = true;
                }
                else
                {
                    MessageBox.Show("Failed on saving.");
                }
            }

            if (success)
            {
                disp_list();
                goto_win1();
                clear_form();
            }
        }



        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = -1;
            String code = "", canc = "";
            isnew = false;
            DataTable dt;
            if (dgv_list.Rows.Count > 1)
            {
                //cbo_city.Invoke(new Action(() =>
                //{
                //    load_city();
                //}));

                r = dgv_list.CurrentRow.Index;

                try
                {
                    code = dgv_list["code", r].Value.ToString();
                    dt = db.QueryBySQLCode("SELECT * FROM rssys.m06 WHERE d_code = '" + code + "'");
                    if (dt.Rows.Count > 0)
                    {
                        txt_code.Text = dt.Rows[0]["d_code"].ToString();
                        cbo_title.SelectedItem = dt.Rows[0]["title"].ToString();
                        if (!String.IsNullOrEmpty(dt.Rows[0]["firstname"].ToString()))
                        {
                            txt_lname.Text = dt.Rows[0]["lastname"].ToString();
                            txt_fname.Text = dt.Rows[0]["firstname"].ToString();
                            txt_mname.Text = dt.Rows[0]["mname"].ToString();
                        }
                        else
                        {
                            txt_fname.Text = dt.Rows[0]["d_name"].ToString();
                        }

                        if (!String.IsNullOrEmpty(dt.Rows[0]["bdate"].ToString()))
                        {
                            dtp_dob.Value = Convert.ToDateTime(dt.Rows[0]["bdate"].ToString());
                        }
                        cbo_gender.SelectedItem = dt.Rows[0]["sex"].ToString();
                        cbo_civil_status.SelectedItem = dt.Rows[0]["civil_status"].ToString();


                        cbo_branch.SelectedValue = GlobalClass.branch;
                        if (!String.IsNullOrEmpty(dt.Rows[0]["d_branch"].ToString()))
                        {
                            cbo_branch.SelectedValue = dt.Rows[0]["d_branch"].ToString();
                        }

                        if (dt.Rows[0]["civil_status"].ToString() == "1")
                        {
                            chk_is_owned.Checked = true;
                        }
                        cbo_nationality.SelectedValue = dt.Rows[0]["nationality"].ToString();
                        rtxt_home_address.Text = dt.Rows[0]["d_addr2"].ToString();

                        txt_city.Text = dt.Rows[0]["city"].ToString();
                        //DataTable dc = db.QueryBySQLCode("SELECT city_name FROM rssys.city WHERE city_name = '" + city + "'");
                        //if(dc.Rows.Count > 0)
                        //{
                        //    cbo_city.SelectedItem = dc.Rows[0]["city_name"].ToString();
                        //}
                        //else
                        //{
                        //    cbo_city.SelectedIndex = -1;
                        //}


                        txt_zip.Text = dt.Rows[0]["area_code"].ToString();
                        cbo_country.SelectedValue = dt.Rows[0]["country"].ToString();
                        txt_length_of_stay_years.Text = dt.Rows[0]["length_stay_years"].ToString();
                        txt_emp_busines_name.Text = dt.Rows[0]["emp_busines_name"].ToString();
                        txt_work_business_work_address.Text = dt.Rows[0]["work_business_work_address"].ToString();
                        txt_w_business.Text = dt.Rows[0]["business_nature"].ToString();
                        txt_emp_busi_email.Text = dt.Rows[0]["emp_busi_email"].ToString();
                        cbo_type.Text = dt.Rows[0]["type"].ToString();
                        rtxt_remarks.Text = dt.Rows[0]["remarks"].ToString();
                        txt_tin.Text = dt.Rows[0]["d_tin"].ToString();
                        txt_sss.Text = dt.Rows[0]["sss"].ToString();
                        txt_landline.Text = dt.Rows[0]["d_tel"].ToString();
                        txt_mobile1.Text = dt.Rows[0]["d_cntc_no"].ToString();
                        txt_mobile2.Text = dt.Rows[0]["mobile2"].ToString();
                        txt_email.Text = dt.Rows[0]["d_email"].ToString();
                        txt_facebook.Text = dt.Rows[0]["facebook"].ToString();
                        if (dt.Rows[0]["is_textblast"].ToString() == "1")
                        {
                            chk_is_textblast.Checked = true;
                        }

                        if (dt.Rows[0]["is_email"].ToString() == "1")
                        {
                            chk_is_emailblast.Checked = true;
                        }



                        txt_years_w_company.Text = dt.Rows[0]["years_w_company"].ToString();
                        txt_gross_m_income.Text = dt.Rows[0]["gross_m_income"].ToString();
                        txt_w_position_title.Text = dt.Rows[0]["position_title"].ToString();
                        txt_worklandline.Text = dt.Rows[0]["work_landline"].ToString();
                        txt_fax.Text = dt.Rows[0]["fax"].ToString();
                        txt_work_mobile.Text = dt.Rows[0]["work_mobile"].ToString();
                        txt_co_maker_name.Text = dt.Rows[0]["co_maker_name"].ToString();
                        txt_relation.Text = dt.Rows[0]["co_m_relation"].ToString();
                        rtxt_cm_address.Text = dt.Rows[0]["co_m_home_add"].ToString();
                        if (!String.IsNullOrEmpty(dt.Rows[0]["com_m_bdate"].ToString()))
                        {
                            dtp_cm_bdate.Value = Convert.ToDateTime(dt.Rows[0]["com_m_bdate"].ToString());
                        }
                        cbo_cm_status.SelectedItem = dt.Rows[0]["co_m_sex"].ToString();
                        txt_co_email.Text = dt.Rows[0]["co_email"].ToString();
                        txt_co_mobile.Text = dt.Rows[0]["co_mobile"].ToString();
                        txt_co_tin.Text = dt.Rows[0]["co_tin"].ToString();
                        txt_cm_sss.Text = dt.Rows[0]["co_sss"].ToString();
                        txt_cm_bus_name.Text = dt.Rows[0]["co_emp_business_name"].ToString();
                        rtxt_bus_address.Text = dt.Rows[0]["co_business_add"].ToString();
                        txt_co_nature_business.Text = dt.Rows[0]["co_nature_of_buss"].ToString();
                        txt_co_business_emp_email.Text = dt.Rows[0]["co_business_emp_email"].ToString();
                        txt_co_years_n_comp.Text = dt.Rows[0]["co_years_n_comp"].ToString();
                        txt_co_gross_m_income.Text = dt.Rows[0]["co_gross_m_income"].ToString();
                        cbo_co_pos_title.Text = dt.Rows[0]["co_pos_title"].ToString();
                        txt_co_landline.Text = dt.Rows[0]["co_landline"].ToString();
                        txt_co_work_mobile.Text = dt.Rows[0]["co_work_mobile"].ToString();

                        dtp_fto.Value = DateTime.Now;
                        dtp_ffrm.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01"));

                        disp_activity_history();

                    }
                    goto_win2();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No customer selected.");
                }
            }
            else
            {
                MessageBox.Show("No customer selected.");
            }

        }

        public void disp_activity_history()
        {
            try{ dgv_history.Rows.Clear(); }
            catch { }
            String str = "";
            if (cbo_type.Text == "Customer")
            {
                dgv_history.DataSource = db.QueryBySQLCode(str = String.Format("SELECT * FROM (SELECT ord_code AS \"TRANS#\", to_char(ord_date,'YYYY-MM-DD') AS \"TransDate\", (SELECT o.out_desc FROM rssys.outlet o WHERE o.out_code=oh.out_code) AS \"Description\", reference AS \"Reference/Remark\", to_char(t_date,'YYYY-MM-DD') AS \"Date\", to_char(to_timestamp(t_time,'hh24:mi:ss'),'hh24:mi:ss') AS \"Time\", branch AS \"Branch\", COALESCE(cancel,'N') AS \"IsCancel\", user_id AS \"User\" FROM rssys.orhdr oh WHERE debt_code='{0}' AND t_date BETWEEN '{1}' AND '{2}' UNION ALL SELECT call_history_number, to_char(date_to_call,'YYYY-MM-DD'), status || '/Contact#' || contact_no, remark, to_char(date_to_call,'YYYY-MM-DD'), to_char(to_timestamp(time_to_call,'hh24:mi:ss'),'hh24:mi:ss'), '', '', userid FROM rssys.call_history WHERE client_number='{0}' AND date_to_call BETWEEN '{1}' AND '{2}' ) history ORDER BY history.\"TransDate\" DESC, history.\"Time\" DESC ", txt_code.Text, dtp_ffrm.Value.ToString("yyyy-MM-dd"), dtp_fto.Value.ToString("yyyy-MM-dd")));

            }
            else if (cbo_type.Text == "Supplier")
            {
                dgv_history.DataSource = db.QueryBySQLCode(str = String.Format("SELECT * FROM (SELECT inv_num AS \"Trans#\", to_char(inv_date,'YYYY-MM-DD') AS \"TransDate\" ,  'Whs:'||COALESCE((SELECT w.whs_desc FROM rssys.whouse w WHERE ph.whs_code=w.whs_code),'') ||'\\Term:'|| COALESCE((SELECT p.mp_desc FROM rssys.m10 p WHERE ph.pay_code=p.mp_code),'') AS \"Description\", reference AS \"Reference\", to_char(t_date,'YYYY-MM-DD') AS \"Date\",to_char(to_timestamp(t_time,'hh24:mi:ss'),'hh24:mi:ss') AS \"Time\", branch AS \"Branch\", cancel AS \"IsCancel\", userid AS \"User/Recipient\" FROM rssys.pinvhd ph WHERE inv_num='{0}' AND t_date BETWEEN '{1}' AND '{2}' UNION ALL SELECT  purc_ord, to_char(rels_date,'YYYY-MM-DD'), 'DeliveryDate:'||to_char(delv_date,'YYYY-MM-DD')||'\\To:'||notes || '\\Term:' ||COALESCE((SELECT p.mp_desc FROM rssys.m10 p WHERE ph.pay_code=p.mp_code),'--'), reference, to_char(t_date,'YYYY-MM-DD'), to_char(to_timestamp(t_time,'hh24:mi:ss'),'hh24:mi:ss'),  '',  cancel, request_by FROM rssys.purhdr as ph WHERE purc_ord='{0}' AND t_date BETWEEN '{1}' AND '{2}' UNION ALL SELECT rec_num, to_char(trnx_date,'YYYY-MM-DD'), 'PR:'||purc_ord || '\\Whs:'||COALESCE((SELECT w.whs_desc FROM rssys.whouse w WHERE rh.whs_code=w.whs_code),'') ||'\\Term:'|| COALESCE((SELECT p.mp_desc FROM rssys.m10 p WHERE rh.payment_term=p.mp_code),''), _reference, to_char(t_date,'YYYY-MM-DD'), to_char(to_timestamp(t_time,'hh24:mi:ss'),'hh24:mi:ss'), branch, cancel, recipient FROM rssys.rechdr rh  WHERE rec_num='{0}' AND t_date BETWEEN '{1}' AND '{2}' ) history  ORDER BY history.\"Date\" DESC, history.\"Time\" DESC", txt_code.Text, dtp_ffrm.Value.ToString("yyyy-MM-dd"), dtp_fto.Value.ToString("yyyy-MM-dd")));
            }



        
        }


        private void load_cbo_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                cbo_nationality.Invoke(new Action(() =>
                {
                    load_nationality();
                }));

                cbo_country.Invoke(new Action(() =>
                {
                    load_country();
                }));
                //cbo_city.Invoke(new Action(() =>
                //{
                //    load_city();
                //}));

                dgv_list.Invoke(new Action(() =>
                {
                    //disp_list();
                }));
            }
            catch (Exception ex)
            {

            }
        }
        private void clear_form()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                Boolean success = false;
                foreach (Control control in controls)
                {
                    success = false;
                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }
                    else
                    {
                        func(control.Controls);
                    }
                    if (control is ComboBox)
                    {
                        if ((control as ComboBox) == cbo_perpage)success = true;

                        if(!success)(control as ComboBox).SelectedIndex = -1;
                    }
                    else
                    {
                        func(control.Controls);
                    }
                    if (control is RichTextBox)
                    {
                        (control as RichTextBox).Clear();
                    }
                    else
                    {
                        func(control.Controls);
                    }
                    if (control is CheckBox)
                    {
                        (control as CheckBox).Checked = true;
                    }
                    else
                    {
                        func(control.Controls);
                    }
                }

            };

            func(Controls);
        }

        private void tgp_main_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbcntrl_left_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pnl_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tgp_list_Click(object sender, EventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            disp_list();
        }

        private void tgp_info_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox12_Enter(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void label56_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void txt_work_mobile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void txt_fax_TextChanged(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void txt_worklandline_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbo_position_title_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void txt_gross_m_income_TextChanged(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void txt_years_w_company_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_emp_busi_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbo_business_nature_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_work_business_work_address_TextChanged(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void txt_emp_busines_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox16_Enter(object sender, EventArgs e)
        {

        }

        private void cbo_co_pos_title_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void txt_co_gross_m_income_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txt_co_work_mobile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void txt_co_landline_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void txt_co_years_n_comp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_co_business_emp_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbo_co_nature_of_buss_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rtxt_bus_address_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void txt_cm_bus_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void txt_co_maker_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void label62_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void txt_cm_sss_TextChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void txt_co_tin_TextChanged(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void txt_co_mobile_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_co_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbo_cm_status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtp_cm_bdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rtxt_cm_address_TextChanged(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void cbo_cm_relation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txt_facebook_TextChanged(object sender, EventArgs e)
        {

        }

        private void chk_is_textblast_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chk_is_emailblast_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void txt_mobile2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void txt_mobile1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_landline_TextChanged(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void txt_sss_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txt_tin_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void rtxt_remarks_TextChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void cbo_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pnl_guestinfo1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbo_city_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_lname_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void label57_Click(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txt_length_of_stay_years_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbo_nationality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cbo_country_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_zip_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void rtxt_home_address_TextChanged(object sender, EventArgs e)
        {

        }

        private void chk_is_owned_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void cbo_civil_status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbo_gender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dtp_dob_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_mname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_fname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt_code_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbo_title_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pnl_sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tpg_option_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btn_print_Click(object sender, EventArgs e)
        {

        }

        private void tpg_option_2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void clear_form_worker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void dgv_list_DoubleClick(object sender, EventArgs e)
        {
            int r = dgv_list.CurrentRow.Index;
            String cust = "";
            if (_frm_auto != null)
            {
                cust = dgv_list["code", r].Value.ToString();
                _frm_auto.set_custvalue_frm(cust);
            }
            if (_frm_sales_auto != null)
            {
                cust = dgv_list["code", r].Value.ToString();
                _frm_sales_auto.set_custvalue_frm(cust);
            }
            if (frm_jq != null)
            {
                cust = dgv_list["code", r].Value.ToString();
                frm_jq.set_custvalue_frm(cust);
            }
            if (frm_gp_compute != null)
            {
                cust = dgv_list["code", r].Value.ToString();
                frm_gp_compute.set_custvalue_frm(cust);
            }
            if (_frm_reldel_unit != null)
            {
                cust = dgv_list["code", r].Value.ToString();
                _frm_reldel_unit.set_custvalue_frm(cust);
            }
            this.Close();

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

        private void dtp_fto_ValueChanged(object sender, EventArgs e)
        {
            disp_activity_history();
        }

        private void dtp_ffrm_ValueChanged(object sender, EventArgs e)
        {
            disp_activity_history();
        }


        private DataTable get_customer_pagination()
        {
            try
            {
                Double pagelength = 0, perpage = Convert.ToInt32(gm.toNormalDoubleFormat(cbo_perpage.Text));
                int page = Convert.ToInt32(gm.toNormalDoubleFormat(btn_vpage.Text));
                String WHERE = "";
                if (!String.IsNullOrEmpty(txt_search.Text))
                {
                    WHERE = String.Format("WHERE d_name LIKE '%{0}%' OR d_code LIKE '%{0}%' OR lastname LIKE '%{0}%' OR firstname LIKE '%{0}%' OR d_addr2 LIKE '%{0}%' OR mname LIKE '%{0}%' OR d_cntc_no LIKE '%{0}%' OR d_tel LIKE '%{0}%' OR fax LIKE '%{0}%' OR d_email LIKE '%{0}%' OR d_tin LIKE '%{0}%' OR remarks LIKE '%{0}%'", txt_search.Text);
                }

                DataTable dt = db.QueryBySQLCode("SELECT count(d_code) AS cnt FROM rssys.m06 " + WHERE + "");
                pagelength = Convert.ToInt32(gm.toNormalDoubleFormat(dt.Rows[0]["cnt"].ToString()));

                lastpage = Convert.ToInt32(pagelength / perpage);

                int offset = Convert.ToInt32(perpage) * page;
                if (offset >= pagelength)
                {
                    page = lastpage;
                }
                else if (offset <= 0)
                {
                    page = 1;
                }
                offset = page * Convert.ToInt32(perpage);
                offset -= (pagelength % perpage) != 0 && page == lastpage ? Convert.ToInt32(pagelength % perpage) : Convert.ToInt32(perpage);

                btn_vpage.Text = page.ToString();

                return db.QueryBySQLCode(String.Format("SELECT d_name, d_code, lastname, firstname, d_addr2, mname, d_cntc_no, d_tel, fax, d_email, d_tin, remarks FROM rssys.m06 " + WHERE + " ORDER BY d_code ASC OFFSET {0} LIMIT {1} ", offset.ToString(), perpage.ToString()));
            }
            catch { }
            return null;
        }

        private void btn_pagefirst_Click(object sender, EventArgs e)
        {
            btn_vpage.Text = "1";
            btn_pageprev.Enabled = false;
            btn_pagefirst.Enabled = false;
            btn_pagenext.Enabled = true;
            btn_pagelast.Enabled = true;
            disp_list();
        }
        private void btn_pagelast_Click(object sender, EventArgs e)
        {
            btn_vpage.Text = lastpage.ToString();
            btn_pagenext.Enabled = false;
            btn_pagelast.Enabled = false;
            btn_pageprev.Enabled = true;
            btn_pagefirst.Enabled = true;
            disp_list();
        }
        private void btn_pagenext_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(gm.toNormalDoubleFormat(btn_vpage.Text)) + 1;
            if (page >= lastpage)
            {
                page = lastpage;
                btn_pagenext.Enabled = false;
                btn_pagelast.Enabled = false;
            }
            else {
                btn_pagenext.Enabled = true;
                btn_pagelast.Enabled = true;
                btn_pageprev.Enabled = true;
                btn_pagefirst.Enabled = true;
            }
            btn_vpage.Text = page.ToString();
            disp_list();
        }
        private void btn_pageprev_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(gm.toNormalDoubleFormat(btn_vpage.Text)) - 1;
            if (page <= 1)
            {
                page = 1;
                btn_pageprev.Enabled = false;
                btn_pagefirst.Enabled = false;
            }
            else
            {
                btn_pagenext.Enabled = true;
                btn_pagelast.Enabled = true;
                btn_pageprev.Enabled = true;
                btn_pagefirst.Enabled = true;
            }
            btn_vpage.Text = page.ToString();
            disp_list();
        }

        private void cbo_perpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_list();
            int page = Convert.ToInt32(gm.toNormalDoubleFormat(btn_vpage.Text));
            if (page <= 1)
            {
                page = 1;
                btn_pageprev.Enabled = false;
                btn_pagefirst.Enabled = false;
            }
            else if (page >= lastpage)
            {
                page = lastpage;
                btn_pagenext.Enabled = false;
                btn_pagelast.Enabled = false;
            }
            else
            {
                btn_pagenext.Enabled = true;
                btn_pagelast.Enabled = true;
                btn_pageprev.Enabled = true;
                btn_pagefirst.Enabled = true;
            }
        }

        private void cbo_branch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbo_status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
