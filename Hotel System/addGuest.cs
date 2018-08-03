using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class addGuest : Form
    {
        Boolean isnew = false;
        Boolean addonly = false;
        //String displist = "last_name AS \"Last Name\", first_name AS \"First Name\", mid_name AS \"Mid Name\", gender AS \"Gender\", birth_date AS \"Birthdate\", address1 AS \"Address\", tel_num ||' / '|| mobile ||' / '||fax_num AS \"Contact\", email AS \"Email\", comp_code AS \"Comp Code\", passport_no AS \"Passport No\", passport_issued AS \"Passport Issued\", passport_expiry  AS \"Passport Expiry\", cntry_code AS \"Country Code\", escaper AS \"Escaper\", notes AS \"Remarks\", acct_no AS \"Guest No\", title AS \"Title\"";
        String displist = "CASE WHEN COALESCE(last_name,'')='' AND COALESCE(first_name,'')=''  THEN full_name ELSE last_name END AS last_name, CASE WHEN COALESCE(last_name,'')='' AND COALESCE(first_name,'')=''  THEN '' ELSE first_name END AS first_name, CASE WHEN COALESCE(last_name,'')='' AND COALESCE(first_name,'')=''  THEN '' ELSE mid_name END AS mid_name , gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, escaper, notes, acct_no, title, passport_place,nat_code as nationality, g_typ";
       
        Reservation resform;
        newReservation newresform;
        Int32 qlistlimit = 12;
        //String querydisp = "";
        Int32 pg = 1;
        //Arrivals arrform;
        newArrivalWalkin arrform;
        // InHouseGuest inhouseform;
        newInhouse inhouseform;

        public addGuest(newReservation r, newArrivalWalkin a, newInhouse i)
        {
            InitializeComponent();
            newresform = r;
            arrform = a;
            inhouseform = i;
        }

        private void addGuest_Load(object sender, EventArgs e)
        {
            GlobalMethod gm = new GlobalMethod();

            form_reset();
            load_country();
            load_company();
            gm.load_nationality(cbo_nationality);

            cbo_srh_country.SelectedIndex = -1;
            cbo_country.SelectedIndex = -1;
            cbo_company.SelectedIndex = -1;
            cbo_srh_company.SelectedIndex = -1;
            cbo_nationality.SelectedIndex = -1;

            //txt_acctno.Hide();
        }

        public void reload_guest()
        {
            DataGridViewRow row = new DataGridViewRow();

            if (GlobalClass.gdgv != null)
            {
                for (int i = 0; i < GlobalClass.gdgv.Rows.Count - 1; i++)
                {
                    row = (DataGridViewRow)GlobalClass.gdgv.Rows[i].Clone();

                    int intColIndex = 0;

                    foreach (DataGridViewCell cell in GlobalClass.gdgv.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_guests.Rows.Add(row);
                }
            }

            lbl_noofguest.Text = (dgv_guests.Rows.Count - 1).ToString();

            GlobalClass.gdgv = null;
        }

        private void load_country()
        {
            GlobalMethod gm = new GlobalMethod();

            gm.load_country(cbo_country);
            gm.load_country(cbo_srh_country);
        }

        private void load_company()
        {
            GlobalMethod gm = new GlobalMethod();

            gm.load_company(cbo_company);
            gm.load_company(cbo_srh_company);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //thisDatabase db = new thisDatabase();

            //String cond = "";
            //String join = "";

            //if (txt_srh_lname.Text != "")
            //{
            //    cond = "last_name ILIKE '%" + txt_srh_lname.Text + "%'";
            //    join = " AND ";
            //}
            //if (txt_srh_fname.Text != "")
            //{
            //    cond = cond + "" + join + "first_name ILIKE '%" + txt_srh_fname.Text + "%'";
            //    join = " AND ";
            //}
            //if (cbo_srh_company.Text != "")
            //{
            //    cond = cond + "" + join + "comp_code='" + cbo_srh_company.SelectedValue.ToString() + "'";
            //    join = " AND ";
            //}
            //if (cbo_srh_country.Text != "")
            //{
            //    cond = cond + "" + join + "cntry_code='" + cbo_srh_country.SelectedValue.ToString() + "'";
            //}

            //dgv_search.DataSource = db.QueryOnTableWithParams("guest", this.displist, cond, "ORDER BY full_name ASC");
            thisDatabase db = new thisDatabase();

            String cond = "";
            String join = "";

            if (txt_srh_lname.Text != "")
            {
                cond = "full_name LIKE '%" + txt_srh_lname.Text + "%'";
                join = " AND ";
            }
            if (txt_srh_fname.Text != "")
            {
                cond = cond + "" + join + "full_name LIKE '%" + txt_srh_fname.Text + "%'";
                join = " AND ";
            }
            if (cbo_srh_company.SelectedIndex != -1)
            {
                cond = cond + "" + join + "comp_code='" + cbo_srh_company.SelectedValue.ToString() + "'";
                join = " AND ";
            }
            if (cbo_srh_country.SelectedIndex != -1)
            {
                cond = cond + "" + join + "cntry_code='" + cbo_srh_country.SelectedValue.ToString() + "'";
            }

            pg = 1;
            int doffset = (qlistlimit * (pg - 1));
            dgv_search.DataSource = get_guestlist(doffset, cond);
            //lbl_pgno.Text = pg.ToString();
        }
        private DataTable get_guestlist(int offset, String WHERE)
        {
            try
            {
                thisDatabase db = new thisDatabase();

                String offset_code = "";

                //if (offset != 0)
                //{
                offset_code = " OFFSET " + offset.ToString();
                //}

                return db.QueryOnTableWithParams("guest", this.displist, WHERE, "ORDER BY full_name ASC LIMIT " + qlistlimit.ToString() + "" + offset_code);
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }
        private void dgv_search_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          //  String birth_date = "", passport_issued = "", passport_expiry = "";
            try
            {
                if (dgv_search.SelectedRows.Count > 0)
                {
                    addonly = true;
                    int row = dgv_search.CurrentCell.RowIndex;

                    String last_name = (dgv_search.Rows[row].Cells[0].Value ?? "").ToString().Trim();
                    String first_name = (dgv_search.Rows[row].Cells[1].Value ?? "").ToString().Trim();
                    String mid_name = (dgv_search.Rows[row].Cells[2].Value ?? "").ToString().Trim();
                    String gender = (dgv_search.Rows[row].Cells[3].Value ?? "").ToString().Trim();
                    DateTime birth_date = DateTime.Now;
                    try { birth_date = Convert.ToDateTime(dgv_search.Rows[row].Cells[4].Value ?? "".ToString().Trim()); }
                    catch { }
                    String address1 = (dgv_search.Rows[row].Cells[5].Value ?? "").ToString().Trim();
                    String tel_num = (dgv_search.Rows[row].Cells[6].Value ?? "").ToString().Trim();
                    String email = (dgv_search.Rows[row].Cells[7].Value ?? "").ToString().Trim();
                    String comp_code = (dgv_search.Rows[row].Cells[8].Value ?? "").ToString().Trim();
                    String passport_no = (dgv_search.Rows[row].Cells[9].Value ?? "").ToString().Trim();
                    DateTime passport_issued = DateTime.Now;
                    try { passport_issued = Convert.ToDateTime((dgv_search.Rows[row].Cells[10].Value ?? "").ToString().Trim()); }
                    catch { }
                    DateTime passport_expiry = DateTime.Now;
                    try { passport_expiry = Convert.ToDateTime((dgv_search.Rows[row].Cells[11].Value ?? "").ToString().Trim()); }
                    catch { }
                    String cntry_code = (dgv_search.Rows[row].Cells[12].Value ?? "").ToString().Trim();
                    String escaper = (dgv_search.Rows[row].Cells[13].Value ?? "").ToString().Trim();
                    String notes = (dgv_search.Rows[row].Cells[14].Value ?? "").ToString().Trim();
                    String acct_no = (dgv_search.Rows[row].Cells[15].Value ?? "").ToString().Trim();
                    String title = (dgv_search.Rows[row].Cells[16].Value ?? "").ToString().Trim();

                    String passport_place = (dgv_search.Rows[row].Cells[17].Value ?? "").ToString().Trim();
                    
                    String nat_code = (dgv_search.Rows[row].Cells[18].Value ?? "").ToString().Trim();
                    txt_acctno.Text = acct_no;
                    cbo_title.Text = title;
                    txt_lname.Text = last_name;
                    txt_fname.Text = first_name;
                    txt_mname.Text = mid_name;
                    cbo_gender.Text = (gender == "F" ? "Female" : "Male");
                    txt_addr.Text = address1;
                    txt_contact.Text = tel_num;
                    txt_email.Text = email;
                    cbo_company.SelectedValue = comp_code;
                    txt_passport.Text = passport_no;
                    cbo_nationality.SelectedValue = nat_code;
                    dtp_dob.Value = birth_date;
                    dtp_issuedon.Value = passport_issued;
                    dtp_expired.Value = passport_expiry;
                    cbo_country.SelectedValue = cntry_code;
                    txt_issueplace.Text = passport_place;
                    rtxt_remarks.Text = notes;
                    comboBox1.Text = ((dgv_search.Rows[row].Cells[19].Value.ToString() == "A") ? "ADULT" : ((dgv_search.Rows[row].Cells[19].Value.ToString() == "K") ? "KID" : "INFANT"));

                    if (escaper == "Y")
                        chk_escaper.Checked = true;
                    else
                        chk_escaper.Checked = false;
                    disp_guesthist(acct_no);

                    form_toedit();
                    btn_new.Enabled = true;
                    btn_save.Enabled = true;
                    //form_new();
                }
                else
                {
                    MessageBox.Show("Pls select the room.");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }


            //isnew = false;
            //form_toedit();

            //try
            //{
            //    int row = dgv_search.CurrentCell.RowIndex;

            //    String last_name = (dgv_search.Rows[row].Cells[0].Value ?? "").ToString().Trim();
            //    String first_name = (dgv_search.Rows[row].Cells[1].Value ?? "").ToString().Trim();
            //    String mid_name = (dgv_search.Rows[row].Cells[2].Value ?? "").ToString().Trim();
            //    String gender = (dgv_search.Rows[row].Cells[3].Value ?? "").ToString().Trim();
            //    DateTime birth_date = DateTime.Now;
            //    try { birth_date = Convert.ToDateTime(dgv_search.Rows[row].Cells[4].Value ?? "".ToString().Trim()); }
            //    catch { }
            //    String address1 = (dgv_search.Rows[row].Cells[5].Value ?? "").ToString().Trim();
            //    String tel_num = (dgv_search.Rows[row].Cells[6].Value ?? "").ToString().Trim();
            //    String email = (dgv_search.Rows[row].Cells[7].Value ?? "").ToString().Trim();
            //    String comp_code = (dgv_search.Rows[row].Cells[8].Value ?? "").ToString().Trim();
            //    String passport_no = (dgv_search.Rows[row].Cells[9].Value ?? "").ToString().Trim();
            //    DateTime passport_issued = DateTime.Now;
            //    try { passport_issued = Convert.ToDateTime((dgv_search.Rows[row].Cells[10].Value ?? "").ToString().Trim()); }
            //    catch { }
            //    DateTime passport_expiry = DateTime.Now;
            //    try { passport_expiry = Convert.ToDateTime((dgv_search.Rows[row].Cells[11].Value ?? "").ToString().Trim()); }
            //    catch { }
            //    String cntry_code = (dgv_search.Rows[row].Cells[12].Value ?? "").ToString().Trim();
            //    String escaper = (dgv_search.Rows[row].Cells[13].Value ?? "").ToString().Trim();
            //    String notes = (dgv_search.Rows[row].Cells[14].Value ?? "").ToString().Trim();
            //    String acct_no = (dgv_search.Rows[row].Cells[15].Value ?? "").ToString().Trim();
            //    String title = (dgv_search.Rows[row].Cells[16].Value ?? "").ToString().Trim();

            //    txt_acctno.Text = acct_no;
            //    cbo_title.Text = title;
            //    txt_lname.Text = last_name;
            //    txt_fname.Text = first_name;
            //    txt_mname.Text = mid_name;
            //    cbo_gender.SelectedValue = gender;
            //    txt_addr.Text = address1;
            //    txt_contact.Text = tel_num;
            //    txt_email.Text = email;
            //    cbo_company.SelectedValue = comp_code;
            //    txt_passport.Text = passport_no;

            //    dtp_dob.Value = birth_date;
            //    dtp_issuedon.Value = passport_issued;
            //    dtp_expired.Value = passport_expiry;
            //    cbo_country.SelectedValue = cntry_code;

            //    rtxt_remarks.Text = notes;

            //    if (escaper == "Y")
            //        chk_escaper.Checked = true;
            //    else
            //        chk_escaper.Checked = false;

            //    disp_guesthist(acct_no);
                
            //}
            //catch (Exception) { }
        }

        private void disp_guesthist(String acctno)
        {
            try
            {
                thisDatabase db = new thisDatabase();

                dgv_gfoliohistory.DataSource = db.QueryOnTableWithParams("gfhist", "reg_num AS \"Folio No\", rom_code AS \"Room\", typ_code AS \"Type\", arr_date AS \"Arrival\", dep_date AS \"Departure\", remarks AS \"Remarks\", rom_rate + govt_tax + serv_chg AS \"Rate\", disc_pct AS \"Disc%\"", "acct_no='" + acctno + "'", "ORDER BY reg_num ASC");
                dgv_gfoliohistory.Columns[0].Width = 70;
                dgv_gfoliohistory.Columns[1].Width = 50;
                dgv_gfoliohistory.Columns[2].Width = 50;
                dgv_gfoliohistory.Columns[3].Width = 70;
                dgv_gfoliohistory.Columns[4].Width = 70;
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void add_guestlist(String acct_no, String full_name, String gender, String addr, String contact, String email, String country, String g_typ)
        {
            DataGridViewRow row = (DataGridViewRow)dgv_guests.Rows[0].Clone();
            row.Cells[0].Value = acct_no;
            row.Cells[1].Value = full_name;
            row.Cells[2].Value = gender;
            row.Cells[3].Value = addr;
            row.Cells[4].Value = contact;
            row.Cells[5].Value = email;
            row.Cells[6].Value = country;
            row.Cells[7].Value = g_typ;

            dgv_guests.Rows.Add(row);

            /* DataTable dt = new DataTable();
             dt.Rows.Add();

             dt.AcceptChanges();            
             DataRow newRow = dt.NewRow();

             newRow[] = "*";
             newRow[] = "";

             dt.Rows.Add(newRow);

             GlobalClass.gdt = dt;*/
            /*
            DataGridViewRow row = (DataGridViewRow) GlobalClass.gdgv.Rows[0].Clone();
            row.Cells[0].Value = acct_no;
            row.Cells[1].Value = full_name;
            GlobalClass.gdgv.Rows.Add(row);*/

        }

        private void btn_removefromlist_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dgv_guests.CurrentCell.RowIndex;

                dgv_guests.Rows.RemoveAt(row);
            }
            catch (Exception) { }
        }

        private void btn_cancel_all_Click(object sender, EventArgs e)
        {
            if (resform != null)
            {
                resform.reset_modname();
            }
            else if (arrform != null)
            {
                arrform.reset_modname();
            }
            else if (inhouseform != null)
            {
                inhouseform.reset_modname();
            }
            this.Close();
        }

        private void btn_save_all_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_guests.Rows.Count > 0)
                {
                    GlobalMethod gmethod = new GlobalMethod();

                    GlobalClass.gdgv = gmethod.CopyDataGridView(dgv_guests);
                    
                    if (newresform != null)
                    {
                        newresform.reload_guest();
                    }
                    else if (arrform != null)
                    {
                        arrform.reload_guest();
                    }
                    else if (inhouseform != null)
                    {
                        inhouseform.reload_guest();
                    }
                    dgv_guests.Rows.Clear();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Pls add guest(s) to list.");
                }  
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }
        //copy data from main window
        public void set_DGVfromMain(DataGridView dgv_frm)
        {
            DataGridViewRow row = new DataGridViewRow();

            if (dgv_frm != null)
            {
                for (int i = 0; i < dgv_frm.Rows.Count - 1; i++)
                {
                    row = (DataGridViewRow)dgv_frm.Rows[i].Clone();

                    int intColIndex = 0;

                    foreach (DataGridViewCell cell in dgv_frm.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_guests.Rows.Add(row);
                }
            }

            lbl_noofguest.Text = (dgv_guests.Rows.Count - 1).ToString();

        }        

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String gender = "M";
            String isescaper = "N";
            String nat = "", fullname = "", fname = "", lname = "", mname = "";

            if (txt_fname.Text.Trim() == "" && txt_lname.Text.Trim() == "" && txt_mname.Text.Trim() == "")
            {
                MessageBox.Show("Pls fill a name.");
            }
            else if (cbo_company.SelectedIndex == -1)
            {
                MessageBox.Show("Company should be selected.");
            }
            else if (cbo_country.SelectedIndex == -1)
            {
                MessageBox.Show("Country should be selected.");
            }
            else if (cbo_title.Text == "")
            {
                MessageBox.Show("Name Title should be selected.");
            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select type of guest");
                comboBox1.DroppedDown = true;
            }
            else
            {
                try
                {
                    if (cbo_gender.Text == "Female")
                    {
                        gender = "F";
                    }

                    String g_typ = ((comboBox1.Text == "ADULT") ? "A" : ((comboBox1.Text == "KID") ? "K" : "I"));

                    fname = txt_fname.Text.Trim();
                    lname = txt_lname.Text.Trim();
                    mname = txt_mname.Text.Trim();

                    if (!String.IsNullOrEmpty(fname) && !String.IsNullOrEmpty(lname)) fullname = lname + ", " + fname;
                    else if (!String.IsNullOrEmpty(fname)) fullname = fname;
                    else if (!String.IsNullOrEmpty(lname)) fullname = lname;
                   
                    if(!String.IsNullOrEmpty(mname))
                        fullname += " " + mname;
                    

                    if (addonly == true)
                    {
                        add_guestlist(txt_acctno.Text, fullname, gender, txt_addr.Text, txt_contact.Text, txt_email.Text, (cbo_country.SelectedValue ?? "").ToString(), g_typ);
                        //END OF ADD TO LIST

                        form_clear();
                        form_reset();
                    }
                    else
                    {
                        String pk = db.get_pk("acct_no");

                        if (chk_escaper.Checked)
                        {
                            isescaper = "Y";
                        }
                        if (cbo_nationality.SelectedIndex > -1)
                        {
                            nat = cbo_nationality.SelectedValue.ToString();
                        }

                        String value = "'" + pk + "','" + cbo_title.Text + "','" + lname + "','" + fname + "','" + mname + "','" + fullname + "','" + gender + "','" + dtp_dob.Value.ToString("yyyy-MM-dd") + "','" + txt_addr.Text + "','" + txt_contact.Text + "','" + txt_email.Text + "','" + (cbo_company.SelectedValue ?? "").ToString() + "','" + txt_passport.Text + "','" + dtp_issuedon.Value.ToString("yyyy-MM-dd") + "','" + dtp_expired.Value.ToString("yyyy-MM-dd") + "','" + (cbo_country.SelectedValue ?? "").ToString() + "', '" + isescaper + "', '" + rtxt_remarks.Text + "', '" + nat + "', '" + db.get_systemdate("") + "','" + DateTime.Now.ToString("HH:mm") + "','" + GlobalClass.username + "','" + txt_issueplace.Text + "','" + g_typ + "'";
                        //MessageBox.Show(pk);
                        if (isnew == true)
                        {
                            if (db.InsertOnTable("guest", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, escaper, notes, nat_code, t_date, t_time, user_id, passport_place, g_typ", value))
                            {
                                String msg = "New record added successfully.";

                                if (db.set_pkincrement("acct_no", pk) == false)
                                {
                                    msg = msg + " But, theres a problem in pk.";
                                }
                                else
                                {
                                    db.set_pkincrement("d_code", pk);
                                }


                                db.InsertOnTable("guest2", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, escaper,notes, nat_code, t_date, t_time, user_id,passport_place, g_typ", value);

                                //String code = "";
                                db.InsertOnTable("m06", "d_code, d_name, d_addr2, d_tel, d_fax, d_email, type, d_cntc, lastname,firstname, mname,at_code, bdate, sex, country, nationality", "'" + pk + "', '" + fullname + "', '" + txt_addr.Text + "', '" + txt_contact.Text + "', '" + ""/*d_fax*/ + "', '" + txt_email.Text + "', 'Customer'," + "'"/*db.str_E(d_cntc)*/ + "', '" + lname + "','" + fname + "', '" + mname + "','" + ""/*at_code*/ + "','" + dtp_dob.Value.ToString("yyyy-MM-dd") + "','" + gender + "','" + (cbo_country.SelectedValue ?? "").ToString() + "','" + nat + "'");
                                //ADD TO LIST
                                txt_acctno.Text = pk;

                                add_guestlist(txt_acctno.Text, txt_lname.Text + ", " + txt_fname.Text + " " + txt_mname.Text, gender, txt_addr.Text, txt_contact.Text, txt_email.Text, (cbo_country.SelectedValue ?? "").ToString(), g_typ);
                                //END OF ADD TO LIST
                                
                                form_clear();
                                form_reset();
                            }
                            else
                            {
                                MessageBox.Show("Duplicate entry.");
                            }
                        }
                        else
                        {
                            String colupd = "title='" + cbo_title.Text + "', last_name='" + lname + "', first_name='" + fname + "', mid_name='" + mname + "', full_name=$$" + fullname + "$$, gender='" + gender + "', birth_date='" + dtp_dob.Value.ToString("yyyy-MM-dd") + "', address1='" + txt_addr.Text + "', tel_num='" + txt_contact.Text + "', email='" + txt_email.Text + "', comp_code='" + (cbo_company.SelectedValue ?? "").ToString() + "', passport_no='" + txt_passport.Text + "', passport_issued='" + dtp_issuedon.Value.ToString("yyyy-MM-dd") + "', passport_expiry='" + dtp_expired.Value.ToString("yyyy-MM-dd") + "', cntry_code='" + (cbo_country.SelectedValue ?? "").ToString() + "', mp_code='101', escaper='" + isescaper + "', nat_code='" + nat + "', t_date='" + db.get_systemdate("") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "',user_id='" + GlobalClass.username + "',passport_place='" + txt_issueplace.Text + "',g_typ='" + g_typ + "'";

                            if (db.UpdateOnTable("guest", colupd, "acct_no='" + txt_acctno.Text + "'"))
                            {
                                DataTable dt;
                                dt = db.QueryBySQLCode("SELECT * FROM rssys.m06 where d_code='"+txt_acctno.Text+"'");
                                if (dt.Rows.Count < 1)
                                {
                                    String val2 = "'" + txt_acctno.Text + "',$$" + fullname + "$$,'" + txt_addr.Text + "','" + txt_email.Text + "','" + lname + "','" + fname + "','" + mname + "','" + dtp_dob.Value.ToString("yyyy-MM-dd") + "','" + gender + "','" + (cbo_country.SelectedValue ?? "").ToString() + "','" + nat + "'";
                                    db.InsertOnTable("m06", "d_code, d_name, d_addr1, d_email, lastname, firstname, mname, bdate, sex, country, nationality", val2);
                                }
                                else
                                {
                                    db.UpdateOnTable("m06", "d_code='" + txt_acctno.Text + "', d_name=$$" + fullname +"$$, d_addr1='" + txt_addr.Text + "', d_email='" + txt_email.Text + "', lastname='" + lname + "', firstname='" + fname + "', mname='" + mname + "', bdate='" + dtp_dob.Value.ToString("yyyy-MM-dd") + "', sex='" + gender + "', country='" + (cbo_country.SelectedValue ?? "").ToString() + "', nationality='" + nat + "'", "d_code='" + txt_acctno.Text + "'");
                                }
                                 
                                MessageBox.Show("Record updated successfully.");
                                db.InsertOnTable("guest2", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, escaper, notes,nat_code, t_date, t_time, user_id, passport_place", value);

                                ////ADD TO LIST
                                //txt_acctno.Text = pk;
                                add_guestlist(txt_acctno.Text, fullname, gender, txt_addr.Text, txt_contact.Text, txt_email.Text, (cbo_country.SelectedValue ?? "").ToString(), g_typ);
                                
                                form_clear();
                                form_reset();
                            }
                            else
                            {
                                MessageBox.Show("Record cannot update.");
                            }
                        }

                        dgv_search.DataSource = db.QueryOnTableWithParams("guest", this.displist, "", "ORDER BY full_name ASC");
                    }
                }
                catch 
                {
                     
                }
            }
        }

        private void form_reset()
        {
            pnl_guestinfo1.Enabled = false;

            dtp_expired.Enabled = false;
            dtp_issuedon.Enabled = false;
            txt_passport.Enabled = false;
            txt_issueplace.Enabled = false;
            txt_otherid.Enabled = false;
            txt_file1.Enabled = false;
            txt_file2.Enabled = false;
            btn_file1.Enabled = false;
            btn_file2.Enabled = false;

            //pnl_guestinfo2.Enabled = false;

            btn_new.Enabled = true;

            btn_edit.Enabled = false;

            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void form_new()
        {
            form_clear();
            pnl_guestinfo1.Enabled = true;
            //pnl_guestinfo2.Enabled = true;

            dtp_expired.Enabled = true;
            dtp_issuedon.Enabled = true;
            txt_passport.Enabled = true;
            txt_issueplace.Enabled = true;
            txt_otherid.Enabled = true;
            txt_file1.Enabled = true;
            txt_file2.Enabled = true;
            btn_file1.Enabled = true;
            btn_file2.Enabled = true;

            btn_new.Enabled = false;

            btn_edit.Enabled = false;
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
        }

        private void form_toedit()
        {
            form_reset();
            btn_new.Enabled = false;
            btn_edit.Enabled = true;
            pnl_guestinfo1.Enabled = true;
            //pnl_guestinfo2.Enabled = true;

            dtp_expired.Enabled = true;
            dtp_issuedon.Enabled = true;
            txt_passport.Enabled = true;
            txt_issueplace.Enabled = true;
            txt_otherid.Enabled = true;
            txt_file1.Enabled = true;
            txt_file2.Enabled = true;
            btn_file1.Enabled = true;
            btn_file2.Enabled = true;

            form_setreadonly(true);
        }

        private void form_edit()
        {
            //form_new();

            //form new
            pnl_guestinfo1.Enabled = true;
            //pnl_guestinfo2.Enabled = true;

            dtp_expired.Enabled = true;
            dtp_issuedon.Enabled = true;
            txt_passport.Enabled = true;
            txt_issueplace.Enabled = true;
            txt_otherid.Enabled = true;
            txt_file1.Enabled = true;
            txt_file2.Enabled = true;
            btn_file1.Enabled = true;
            btn_file2.Enabled = true;

            btn_new.Enabled = false;

            btn_edit.Enabled = false;
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
            //
            form_setreadonly(false);
        }

        private void form_clear()
        {
            cbo_title.SelectedIndex = 0;
            cbo_gender.SelectedIndex = 0;
            cbo_gender.Text = "";
            comboBox1.Text = "";

            txt_acctno.Text = "";
            txt_lname.Text = "";
            txt_fname.Text = "";
            txt_mname.Text = "";
            txt_email.Text = "";
            txt_contact.Text = "";
            txt_addr.Text = "";
            txt_passport.Text = "";
            rtxt_remarks.Text = "";
            cbo_company.SelectedIndex = -1;
            cbo_country.SelectedIndex = -1;
            cbo_nationality.SelectedIndex = -1;

            dgv_gfoliohistory.DataSource = null;

            form_setreadonly(false);
        }

        private void form_setreadonly(Boolean flag)
        {
            txt_addr.ReadOnly = flag;
            txt_contact.ReadOnly = flag;
            txt_email.ReadOnly = flag;
            txt_fname.ReadOnly = flag;
            txt_lname.ReadOnly = flag;
            txt_mname.ReadOnly = flag;
            txt_passport.ReadOnly = flag;

            rtxt_remarks.ReadOnly = flag;

            if (flag == true)
            {
                cbo_title.Enabled = false;
                cbo_company.Enabled = false;
                cbo_country.Enabled = false;
                cbo_gender.Enabled = false;
                cbo_nationality.Enabled = false;

                dtp_dob.Enabled = false;
                dtp_expired.Enabled = false;
                dtp_issuedon.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                cbo_title.Enabled = true;
                cbo_company.Enabled = true;
                cbo_country.Enabled = true;
                cbo_gender.Enabled = true;
                cbo_nationality.Enabled = true;

                dtp_dob.Enabled = true;
                dtp_expired.Enabled = true;
                dtp_issuedon.Enabled = true;
                comboBox1.Enabled = true;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            addonly = false;
            isnew = true;
            form_new();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            addonly = false;
            isnew = false;
            form_edit();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            isnew = false;
            form_clear();
            form_reset();
        }

        private void dgv_guests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();

            try
            {
                if (dgv_search.SelectedRows.Count > 0)
                {
                    int r = dgv_search.CurrentCell.RowIndex;

                    String lacct_no = dgv_guests[0, r].Value.ToString();

                    dt = db.get_guest_info(lacct_no);

                    foreach (DataRow row in dt.Rows)
                    {
                        txt_acctno.Text = row["acct_no"].ToString();
                        txt_lname.Text = row["last_name"].ToString();
                        txt_fname.Text = row["first_name"].ToString();
                        txt_mname.Text = row["mid_name"].ToString();
                        txt_passport.Text = row["last_name"].ToString();
                        txt_email.Text = row["email"].ToString();
                        txt_contact.Text = row["tel_num"].ToString();
                        txt_addr.Text = row["address1"].ToString();
                        cbo_company.SelectedValue = row["comp_code"].ToString();
                        cbo_country.SelectedValue = row["cntry_code"].ToString();
                        cbo_gender.Text = "Male";
                        cbo_title.Text = row["title"].ToString();
                        dtp_dob.Value = Convert.ToDateTime(row["birth_date"].ToString());
                        dtp_expired.Value = Convert.ToDateTime(row["passport_expiry"].ToString());
                        dtp_issuedon.Value = Convert.ToDateTime(row["passport_issued"].ToString());
                        txt_issueplace.Text = row["passport_place"].ToString();
                        rtxt_remarks.Text = "";//row["last_name"].ToString();

                        //title, last_name, first_name, mid_name, full_name,
                        //gender, birth_date, address1, tel_num, email, comp_code, passport_no, 
                        //passport_issued, passport_expiry, cntry_code, mp_code, escaper
                    }
                }
            }
            catch (Exception) { }
        }

        private void btn_file1_Click(object sender, EventArgs e)
        {

        }

        private void btn_file2_Click(object sender, EventArgs e)
        {

        }

        private void dgv_search_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        //    e.CellStyle.SelectionForeColor = Color.Brown;
        //    e.CellStyle.SelectionBackColor = Color.GreenYellow;

        //    if (e.RowIndex == -1)
        //    {
        //        SolidBrush br = new SolidBrush(Color.Gray);
        //        e.Graphics.FillRectangle(br, e.CellBounds);
        //        e.PaintContent(e.ClipBounds);
        //        e.Handled = true;
        //    }
        //    else
        //    {
        //        if (e.RowIndex % 2 == 0)
        //        {
        //            SolidBrush br = new SolidBrush(Color.Gainsboro);

        //            e.Graphics.FillRectangle(br, e.CellBounds);
        //            e.PaintContent(e.ClipBounds);
        //            e.Handled = true;
        //        }
        //        else
        //        {
        //            SolidBrush br = new SolidBrush(Color.White);
        //            e.Graphics.FillRectangle(br, e.CellBounds);
        //            e.PaintContent(e.ClipBounds);
        //            e.Handled = true;
        //        }
        //    }
        }

        private void dgv_gfoliohistory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_guests_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        //    e.CellStyle.SelectionForeColor = Color.Brown;
        //    e.CellStyle.SelectionBackColor = Color.GreenYellow;

        //    if (e.RowIndex == -1)
        //    {
        //        SolidBrush br = new SolidBrush(Color.Gray);
        //        e.Graphics.FillRectangle(br, e.CellBounds);
        //        e.PaintContent(e.ClipBounds);
        //        e.Handled = true;
        //    }
        //    else
        //    {
        //        if (e.RowIndex % 2 == 0)
        //        {
        //            SolidBrush br = new SolidBrush(Color.Gainsboro);

        //            e.Graphics.FillRectangle(br, e.CellBounds);
        //            e.PaintContent(e.ClipBounds);
        //            e.Handled = true;
        //        }
        //        else
        //        {
        //            SolidBrush br = new SolidBrush(Color.White);
        //            e.Graphics.FillRectangle(br, e.CellBounds);
        //            e.PaintContent(e.ClipBounds);
        //            e.Handled = true;
        //        }
        //    }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            pg++;
            int doffset = (qlistlimit * pg - 1);
            dgv_search.DataSource = get_guestlist(doffset, "");
            lbl_pgno.Text = pg.ToString();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {

            pg--;
            if (pg < 1) pg = 1;

            int doffset = (qlistlimit * (pg - 1));
            dgv_search.DataSource = get_guestlist(doffset, "");
            lbl_pgno.Text = pg.ToString();
            
        }

        private void dgv_search_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
