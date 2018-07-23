using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Hotel_System
{
    public partial class addGuestinGroup : Form
    {
        GroupReservation GRPRES;
        DataTable lcl_dt;
        Boolean isnew = false;
        String displist = "last_name, first_name, mid_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, escaper, notes, acct_no, title";
        
        public addGuestinGroup(GroupReservation G)
        {
            InitializeComponent();

            GRPRES = G;
        }

        private void addGuestinGroup_Load(object sender, EventArgs e)
        {
            lcl_dt = new DataTable();
            form_reset();
            load_country();
            load_company();

            set_rtcbo();
            set_disccbo();
            compute_noofnight();

            cbo_srh_country.SelectedIndex = -1;
            cbo_country.SelectedItem = -1;
            cbo_srh_company.SelectedIndex = -1;
            cbo_rtcode.SelectedIndex = -1;
            cbo_disc.SelectedIndex = -1;
            cbo_company.SelectedValue = "000002";
            cbo_occtyp.Text = "Double";
            txt_acctno.Hide();
        }

        private void form_reload()
        {
            int i = 0;
            lcl_dt.Clear();

            dgv_search.DataSource = lcl_dt;
            dgv_gfoliohistory.DataSource = lcl_dt;      

            if (dgv_guests.Rows.Count > 0)
            {
                dgv_guests.Rows.Clear();
            }

            form_clear();
            form_reset();
            
            lbl_noofnight_billing.Text = "0";
            cbo_srh_country.SelectedIndex = -1;
            cbo_country.SelectedItem = -1;
            cbo_srh_company.SelectedIndex = -1;
            cbo_rtcode.SelectedIndex = -1;
            cbo_disc.SelectedIndex = -1;
            cbo_company.SelectedValue = "000002";
            txt_govtrt.Text = "0.00";
            txt_netrt.Text = "0.00";
            txt_srvccharge.Text = "0.00";
            txt_rmrate.Text = "0.00";
            txt_total_amt.Text = "";
        }

        public void set_passdatatable(DataTable dt)
        {
            DataTable this_dt = new DataTable();
            String cin = null;
            String cout = null;
            String guestno = "";
            String occ = "";
            String[] guestno_arr;

            foreach (DataRow dr in dt.Rows)
            {
                lbl_arrdt.Text = dr["arr"].ToString();
                lbl_depdt.Text = dr["dep"].ToString();
                lbl_rm.Text = dr["rm"].ToString();
                lbl_rmtyp.Text = dr["rmtyp"].ToString();
                guestno = dr["guestno"].ToString();
                occ = dr["occ"].ToString();
                cbo_rtcode.SelectedValue = get_strincode(dr["rttyp"].ToString());
                cbo_disc.SelectedValue = get_strincode(dr["disc"].ToString());
                cin = lbl_arrdt.Text;
                cout = lbl_depdt.Text;
            }

            double val = (Convert.ToDateTime(cin) - Convert.ToDateTime(cin)).TotalDays;

            lbl_noofnight_billing.Text = val.ToString();

            if (val == 0)
            {
                lbl_noofnight_billing.Text = "1";
            }

            guestno_arr = Regex.Split(guestno, "/");

            foreach (String gno in guestno_arr )
            {
                this_dt = get_guest_info(gno);

                foreach (DataRow drow in this_dt.Rows)
                {
                    add_guestlist(drow["acct_no"].ToString(), drow["full_name"].ToString(), drow["gender"].ToString(), drow["address1"].ToString(), drow["tel_num"].ToString(), drow["email"].ToString(), drow["cntry_code"].ToString());
                }

                dt.Rows.Clear();
            }
        }

        private DataTable get_guest_info(String gno)
        {
            thisDatabase db = new thisDatabase();

            return db.QueryOnTableWithParams("guest", "acct_no, full_name, gender, address1, tel_num, email, cntry_code", "acct_no='"+ gno +"'", "");
        }

        private String get_strincode(String val)
        {
            try
            {
                String[] lines = Regex.Split(val, "::");

                return lines[0].ToString();
            }
            catch (Exception) { }

            return "";
            
        }

        private void load_country()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                dt.Rows.Add();
                dt.AcceptChanges();

                dt = db.QueryOnTableWithParams("country", "cntry_code, cntry_desc", "", "ORDER BY cntry_desc ASC;");

                cbo_srh_country.DataSource = dt;
                cbo_srh_country.DisplayMember = "cntry_desc";
                cbo_srh_country.ValueMember = "cntry_code";

                cbo_country.DataSource = dt;
                cbo_country.DisplayMember = "cntry_desc";
                cbo_country.ValueMember = "cntry_code";

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void load_company()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_name ASC;");

                cbo_srh_company.DataSource = dt;
                cbo_srh_company.DisplayMember = "comp_name";
                cbo_srh_company.ValueMember = "comp_code";

                cbo_company.DataSource = dt;
                cbo_company.DisplayMember = "comp_name";
                cbo_company.ValueMember = "comp_code";

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        

        private void set_rtcbo()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("ratetype", "rate_code, rate_desc", "", "ORDER BY rate_code ASC;");

                cbo_rtcode.DataSource = dt;
                cbo_rtcode.DisplayMember = "rate_desc";
                cbo_rtcode.ValueMember = "rate_code";

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void set_disccbo()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("disctbl", "disc_code, disc_desc", "", "ORDER BY disc_code ASC;");

                cbo_disc.DataSource = dt;
                cbo_disc.DisplayMember = "disc_desc";
                cbo_disc.ValueMember = "disc_code";

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void compute_noofnight()
        {
            DateTime cin = Convert.ToDateTime(lbl_arrdt.Text);
            DateTime cout = Convert.ToDateTime(lbl_depdt.Text);

            lbl_noofnight_billing.Text = (cout - cin).TotalDays.ToString();
            //String noofnight =
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            String cond = "";
            String join = "";

            if (txt_srh_lname.Text != "")
            {
                cond = "last_name LIKE '%" + txt_srh_lname.Text + "%'";
                join = " AND ";
            }
            if (txt_srh_fname.Text != "")
            {
                cond = cond + "" + join + "first_name LIKE '%" + txt_srh_fname.Text + "%'";
                join = " AND ";
            }
            if (cbo_srh_company.Text != "")
            {
                cond = cond + "" + join + "comp_code='" + cbo_srh_company.SelectedValue.ToString() + "'";
                join = " AND ";
            }
            if (cbo_srh_country.Text != "")
            {
                cond = cond + "" + join + "cntry_code='" + cbo_srh_country.SelectedValue.ToString() + "'";
            }

            dgv_search.DataSource = db.QueryOnTableWithParams("guest", this.displist, cond, "ORDER BY full_name ASC");
        }

        private void dgv_search_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_search.SelectedRows.Count > 0)
                {
                    int row = dgv_search.CurrentCell.RowIndex;

                    String last_name = dgv_search.Rows[row].Cells[0].Value.ToString().Trim();
                    String first_name = dgv_search.Rows[row].Cells[1].Value.ToString().Trim();
                    String mid_name = dgv_search.Rows[row].Cells[2].Value.ToString().Trim();
                    String gender = dgv_search.Rows[row].Cells[3].Value.ToString().Trim();
                    DateTime birth_date = Convert.ToDateTime(dgv_search.Rows[row].Cells[4].Value.ToString().Trim());
                    String address1 = dgv_search.Rows[row].Cells[5].Value.ToString().Trim();
                    String tel_num = dgv_search.Rows[row].Cells[6].Value.ToString().Trim();
                    String email = dgv_search.Rows[row].Cells[7].Value.ToString().Trim();
                    String comp_code = dgv_search.Rows[row].Cells[8].Value.ToString().Trim();
                    String passport_no = dgv_search.Rows[row].Cells[9].Value.ToString().Trim();
                    DateTime passport_issued = Convert.ToDateTime(dgv_search.Rows[row].Cells[10].Value.ToString().Trim());
                    DateTime passport_expiry = Convert.ToDateTime(dgv_search.Rows[row].Cells[11].Value.ToString().Trim());
                    String cntry_code = dgv_search.Rows[row].Cells[12].Value.ToString().Trim();
                    String escaper = dgv_search.Rows[row].Cells[13].Value.ToString().Trim();
                    String notes = dgv_search.Rows[row].Cells[14].Value.ToString().Trim();
                    String acct_no = dgv_search.Rows[row].Cells[15].Value.ToString().Trim();
                    String title = dgv_search.Rows[row].Cells[16].Value.ToString().Trim();

                    txt_acctno.Text = acct_no;
                    cbo_title.Text = title;
                    txt_lname.Text = last_name;
                    txt_fname.Text = first_name;
                    txt_mname.Text = mid_name;
                    cbo_gender.SelectedItem = gender;
                    dtp_dob.Value = birth_date;
                    txt_addr.Text = address1;
                    txt_contact.Text = tel_num;
                    txt_email.Text = email;
                    txt_passport.Text = passport_no;
                    dtp_issuedon.Value = passport_issued;
                    dtp_expired.Value = passport_expiry;
                    cbo_country.SelectedItem = cntry_code;
                    rtxt_remarks.Text = notes;

                    disp_guesthist(acct_no);

                    form_toedit();
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
        }

        private void disp_guesthist(String acctno)
        {
            try
            {
                thisDatabase db = new thisDatabase();

                dgv_gfoliohistory.DataSource = db.QueryOnTableWithParams("gfhist", "reg_num AS folio_no, rom_code AS room, arr_date AS Arrival_Date, dep_date AS Departure_Date, remarks AS Remarks", "acct_no='" + acctno + "'", "ORDER BY reg_num ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void btn_addtolist_Click(object sender, EventArgs e)
        {
            String gender = "M";

            if (cbo_gender.Text == "Female")
            {
                gender = "F";
            }

            add_guestlist(txt_acctno.Text, txt_lname.Text + ", " + txt_fname.Text + " " + txt_mname.Text, gender, txt_addr.Text, txt_contact.Text, txt_email.Text, cbo_country.SelectedValue.ToString());
        }

        private void add_guestlist(String acct_no, String full_name, String gender, String addr, String contact, String email, String country)
        {
            DataGridViewRow row = (DataGridViewRow)dgv_guests.Rows[0].Clone();
            row.Cells[0].Value = acct_no;
            row.Cells[1].Value = full_name;
            row.Cells[2].Value = gender;
            row.Cells[3].Value = addr;
            row.Cells[4].Value = contact;
            row.Cells[5].Value = email;
            row.Cells[6].Value = country;

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
            this.Close();
        }

        private void btn_save_all_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String guestno = "", guest = "";
            String occ = cbo_occtyp.Text;
            String rtcode = "";
            String rtcode_name = "";
            String disc = "";
            String disc_name = "";
            String disc_amt = "";
            String rmrate = txt_rmrate.Text;
            String total = txt_total_amt.Text;
            String blocked = "";
            String bfast = txt_fbkfast.Text;

            if (cbo_rtcode.SelectedIndex == -1)
            {
                MessageBox.Show("Pls select Room Rate.");
            }
            else
            {
                rtcode = cbo_rtcode.SelectedValue.ToString();
                rtcode_name = cbo_rtcode.Text;

                if (cbo_disc.SelectedIndex != -1)
                {
                    disc = cbo_disc.SelectedValue.ToString();
                    disc_name = cbo_disc.Text;
                }

                try
                {
                    foreach (DataGridViewRow dgv_r in dgv_guests.Rows)
                    {
                        guestno = guestno + "/" + dgv_r.Cells[0].Value.ToString();
                        guest = guest + "/" + dgv_r.Cells[1].Value.ToString();

                        //dt.Rows.Add(guestno, guest);
                    }
                }
                catch (Exception) { }

                if (String.IsNullOrEmpty(disc) == false)
                {
                    disc_amt = db.get_discount(disc).ToString();
                }
                if (chk_blockedby.Checked == true)
                {
                    blocked = lbl_blockedbyuser.Text;
                }
                
                GRPRES.upd_selrom(guestno, guest, occ, rtcode, rtcode_name, disc, disc_name, disc_amt, rmrate, total, blocked, bfast);
                form_reload();
                this.Hide();
            }
        }

        private DataGridView CopyDataGridView(DataGridView dgv_org)
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String gender = "M";
            String isescaper = "N";

            if (cbo_gender.Text == "Female")
            {
                gender = "F";
            }

            try
            {
                String pk = db.get_pk("acct_no");

                String value = "'" + pk + "','" + cbo_title.Text + "','" + txt_lname.Text + "','" + txt_fname.Text + "','" + txt_mname.Text + "','" + txt_lname.Text + ", " + txt_fname.Text + " " + txt_mname.Text + "','" + gender + "','" + dtp_dob.Value.ToString("yyyy-MM-dd") + "','" + txt_addr.Text + "','" + txt_contact.Text + "','" + txt_email.Text + "','" + cbo_company.SelectedValue.ToString() + "','" + txt_passport.Text + "','" + dtp_issuedon.Value.ToString("yyyy-MM-dd") + "','" + dtp_expired.Value.ToString("yyyy-MM-dd") + "','" + cbo_country.SelectedValue.ToString() + "','101', '" + isescaper + "', '" +db.get_systemdate("") + "','" + DateTime.Now.ToString("HH:mm") + "','" + GlobalClass.username + "'";
                MessageBox.Show(value);
                if (isnew == true)
                {

                    if (db.InsertOnTable("guest", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, mp_code, escaper, t_date, t_time, user_id", value))
                    {
                        String msg = "New record added successfully.";

                        if (db.set_pkincrement("acct_no", pk) == false)
                        {
                            msg = msg + " But, theres a problem in pk.";
                        }
                        db.InsertOnTable("guest2", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, mp_code, escaper, t_date, t_time, user_id", value);

                        MessageBox.Show(msg);

                        //ADD TO LIST
                        txt_acctno.Text = pk;

                        add_guestlist(txt_acctno.Text, txt_lname.Text + ", " + txt_fname.Text + " " + txt_mname.Text, gender, txt_addr.Text, txt_contact.Text, txt_email.Text, cbo_country.SelectedValue.ToString());
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
                    String colupd = "title='" + cbo_title.Text + "', last_name='" + txt_lname.Text + "', first_name='" + txt_fname.Text + "', mid_name='" + txt_mname.Text + "', full_name='" + txt_lname.Text + ", " + txt_fname.Text + " " + txt_mname.Text + "', gender='" + gender + "', birth_date='" + dtp_dob.Value.ToString("yyyy-MM-dd") + "', address1='" + txt_addr.Text + "', tel_num='" + txt_contact.Text + "', email='" + txt_email.Text + "', comp_code='" + cbo_company.SelectedValue.ToString() + "', passport_no='" + txt_passport.Text + "', passport_issued='" + dtp_issuedon.Value.ToString("yyyy-MM-dd") + "', passport_expiry='" + dtp_expired.Value.ToString("yyyy-MM-dd") + "', cntry_code='" + cbo_country.SelectedValue.ToString() + "', mp_code='101', escaper='" + isescaper + "', t_date='" + db.get_systemdate("") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "',user_id='" + GlobalClass.username + "'";

                    if (db.UpdateOnTable("guest", colupd, "acct_no='" + txt_acctno.Text + "'"))
                    {
                        MessageBox.Show("Record updated successfully.");
                        form_clear();
                        form_reset();
                        db.InsertOnTable("guest2", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, mp_code, escaper, t_date, t_time, user_id", value);
                    }
                    else
                    {
                        MessageBox.Show("Record cannot update.");
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void form_reset()
        {

            pnl_guestinfo1.Enabled = false;
            pnl_guestinfo2.Enabled = false;

            btn_new.Enabled = true;

            btn_edit.Enabled = false;

            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void form_new()
        {
            form_clear();
            pnl_guestinfo1.Enabled = true;
            pnl_guestinfo2.Enabled = true;

            btn_new.Enabled = false;

            btn_edit.Enabled = false;
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
        }

        private void form_toedit()
        {
            form_reset();
            btn_new.Enabled = true;
            btn_edit.Enabled = true;
            pnl_guestinfo1.Enabled = true;
            pnl_guestinfo2.Enabled = true;

            form_setreadonly(false);
        }

        private void form_edit()
        {
            //form_new();

            //form new
            pnl_guestinfo1.Enabled = true;
            pnl_guestinfo2.Enabled = true;

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

            txt_acctno.Text = "";
            txt_lname.Text = "";
            txt_fname.Text = "";
            txt_mname.Text = "";
            txt_email.Text = "";
            txt_contact.Text = "";
            txt_addr.Text = "";
            txt_passport.Text = "";
            rtxt_remarks.Text = "";
            cbo_company.Text = "";
            cbo_country.Text = "";
            cbo_country.Text = "";
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

                dtp_dob.Enabled = false;
                dtp_expired.Enabled = false;
                dtp_issuedon.Enabled = false;
            }
            else
            {
                cbo_title.Enabled = true;
                cbo_company.Enabled = true;
                cbo_country.Enabled = true;
                cbo_gender.Enabled = true;

                dtp_dob.Enabled = true;
                dtp_expired.Enabled = true;
                dtp_issuedon.Enabled = true;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            isnew = true;
            form_new();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            isnew = false;
            form_edit();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            isnew = false;
            form_clear();
            form_reset();
        }

        private void cbo_rtcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_computed_bill();
        }

        private void cbo_disc_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_computed_bill();
        }

        private void disp_computed_bill()
        {
            thisDatabase db = new thisDatabase();
            Double amt = 0.00;
            Double lessdiscount = 0.00, lessdisc_amt = 0.00;
            Double grossamt = 0.00;
            int occ = 1;
            Boolean issenior_disc = false;

            try
            {
                if (cbo_occtyp.Text == "Double")
                {
                    occ = 2;
                }
                else if (cbo_occtyp.Text == "Triple")
                {
                    occ = 3;
                }
                else if (cbo_occtyp.Text == "Quad")
                {
                    occ = 4;
                }

                if (cbo_disc.SelectedIndex >= 0)
                {
                    lessdiscount = db.get_discount(cbo_disc.SelectedValue.ToString());
                    issenior_disc = db.issenior_disc(cbo_disc.SelectedValue.ToString());
                }

                if (db.has_brkfast(cbo_rtcode.SelectedValue.ToString()))
                {
                    txt_fbkfast.Text = occ.ToString();
                }
                else
                {
                    txt_fbkfast.Text = "0";
                }

                grossamt = db.get_roomrateamt(cbo_rtcode.SelectedValue.ToString(), db.get_romtyp_code(lbl_rmtyp.Text), occ);
                txt_grossrt.Text = grossamt.ToString("0.00");

                txt_netrt.Text = db.get_netrate(grossamt, lessdiscount, lessdisc_amt).ToString("0.00");
                txt_govtrt.Text = db.get_tax(grossamt, lessdiscount, lessdisc_amt).ToString("0.00");
                txt_srvccharge.Text = db.get_svccharge(grossamt, lessdiscount, lessdisc_amt).ToString("0.00");

                //regular rate w/ or w/out discount
                if (issenior_disc == false)
                {
                    txt_rmrate.Text = (grossamt - (grossamt * (lessdiscount / 100))).ToString("0.00");
                }
                //senior citizen discount no tax
                else if (issenior_disc == true)
                {
                    Double db_rmrate = Convert.ToDouble(txt_netrt.Text) + Convert.ToDouble(txt_srvccharge.Text);

                    Decimal roundoff_amt = Math.Round(Convert.ToDecimal(db_rmrate), 1);

                    txt_rmrate.Text = roundoff_amt.ToString("0.00");
                    txt_govtrt.Text = "0.00";
                }

                txt_total_amt.Text = (Convert.ToDouble(txt_rmrate.Text) * Convert.ToDouble(lbl_noofnight_billing.Text)).ToString("0.00");
            }
            catch (Exception)
            { }
        }

        private void cbo_occtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            int occ = 1;

            try
            {
                if (cbo_occtyp.Text == "Double")
                {
                    occ = 2;
                }
                else if (cbo_occtyp.Text == "Triple")
                {
                    occ = 3;
                }
                else if (cbo_occtyp.Text == "Quad")
                {
                    occ = 4;
                }

                if (db.has_brkfast(cbo_rtcode.SelectedValue.ToString()))
                {
                    txt_fbkfast.Text = occ.ToString();
                }
            }
            catch (Exception) { }
        }

        private void addGuestinGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }

        private void chk_blockedby_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_blockedby.Checked == true)
            {
                lbl_blockedbyuser.Text = GlobalClass.username;
            }
        }

        private void dgv_guests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv_search_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
