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
    public partial class Guest : Form
    {
        Boolean isnew = false;
        String displist = "CASE WHEN COALESCE(last_name,'')='' AND COALESCE(first_name,'')=''  THEN full_name ELSE last_name END AS last_name, CASE WHEN COALESCE(last_name,'')='' AND COALESCE(first_name,'')=''  THEN '' ELSE first_name END AS first_name, CASE WHEN COALESCE(last_name,'')='' AND COALESCE(first_name,'')=''  THEN '' ELSE mid_name END AS mid_name , gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, escaper, notes, acct_no, title";
        Int32 qlistlimit = 12;
        //String querydisp = "";
        Int32 pg = 1;

        public Guest()
        {
            InitializeComponent();
            thisDatabase db = new thisDatabase();
            String grp_id = "";
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0201' ORDER BY b.pla, b.mod_id");

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
                    btn_edit.Enabled = false;
                }
                if (delete == "n")
                {
                    //btn_delitem.Enabled = false;
                }
                if (print == "n")
                {
                   // btn_print.Enabled = false;
                }

            }
        }

        private void Guest_Load(object sender, EventArgs e)
        {
            form_reset();
            lbl_pgno.Text = pg.ToString();
            txt_acctno.Hide();

            int doffset = (qlistlimit * (pg - 1));
            dgv_guestlist.DataSource = get_guestlist(doffset, "");
            putdata_allcbocountry();
            putdata_allcbocompany();            
        }

        private void form_reset()
        {
            cbo_title.SelectedIndex = 0;
            cbo_gender.SelectedIndex = 0;
            
            pnl_guestinfo1.Enabled = false;
            pnl_guestinfo2.Enabled = false;
            chk_escaper.Enabled = false;

            btn_new.Enabled = true;
            
            btn_edit.Enabled = false;
            
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void form_new()
        {
            pnl_guestinfo1.Enabled = true;
            pnl_guestinfo2.Enabled = true;
            chk_escaper.Enabled = true;

            btn_new.Enabled = false;

            btn_edit.Enabled = false;
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
            dgv_guestfolliohist.Enabled = true;
        }

        private void form_toedit()
        {
            form_reset();
            btn_new.Enabled = false;
            btn_edit.Enabled = true;
            pnl_guestinfo1.Enabled = true;
            pnl_guestinfo2.Enabled = true;

            form_setreadonly(true);            
        }

        private void form_edit()
        {
            form_new();
            form_setreadonly(false);
        }

        private void form_clear()
        {
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
            chk_escaper.Checked = false;
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

        private DataTable get_guestlist(int offset,String WHERE)
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

        private void putdata_allcbocompany()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_name ASC;");
                
                cbo_searchcompany.DataSource = dt;
                cbo_searchcompany.DisplayMember = "comp_name";
                cbo_searchcompany.ValueMember = "comp_code";
                
                cbo_company.DataSource = dt;
                cbo_company.DisplayMember = "comp_name";
                cbo_company.ValueMember = "comp_code";

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void putdata_allcbocountry()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                dt.Rows.Add();
                dt.AcceptChanges();

                dt = db.QueryOnTableWithParams("country", "cntry_code, cntry_desc", "", "ORDER BY cntry_desc ASC;");
                
                cbo_searchcountry.DataSource = dt;
                cbo_searchcountry.DisplayMember = "cntry_desc";
                cbo_searchcountry.ValueMember = "cntry_code";

                cbo_country.DataSource = dt;
                cbo_country.DisplayMember = "cntry_desc";
                cbo_country.ValueMember = "cntry_code";

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String gender = "M";
            String isescaper = "N";
            
            if (cbo_gender.Text == "Female")
            {
                gender = "F";
            }

            if (chk_escaper.Checked == true)
            {
                isescaper = "Y";
            }

            String pk = db.get_pk("acct_no");

            String value = "'" + pk + "','" + cbo_title.Text + "','" + txt_lname.Text + "','" + txt_fname.Text + "','" + txt_mname.Text + "','" + txt_lname.Text + ", " + txt_fname.Text + " " + txt_mname.Text + "','" + gender + "','" + dtp_dob.Value.ToString("yyyy-MM-dd") + "','" + txt_addr.Text + "','" + txt_contact.Text + "','" + txt_email.Text + "','" + (cbo_company.SelectedValue ?? "").ToString() + "','" + txt_passport.Text + "','" + dtp_issuedon.Value.ToString("yyyy-MM-dd") + "','" + dtp_expired.Value.ToString("yyyy-MM-dd") + "','" + (cbo_country.SelectedValue ?? "").ToString() + "','101', '" + isescaper + "', '" + db.get_systemdate("") + "','" + DateTime.Now.ToString("HH:mm") + "','" + GlobalClass.username + "'";
              
            if (isnew == true)
            {
                 
                if (db.InsertOnTable("guest", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, mp_code, escaper, t_date, t_time, user_id", value))
                {
                    db.InsertOnTable("m06", "d_code, d_name, d_addr2, d_tel, d_fax, d_email, type, d_cntc, lastname,firstname, mname,at_code, bdate, sex, country", "'" + pk + "', '" + txt_lname.Text + ", " + txt_fname.Text + " " + txt_mname.Text + "', '" + txt_addr.Text + "', '" + txt_contact.Text + "', '" + ""/*d_fax*/ + "', '" + txt_email.Text + "', 'Customer'," + "'"/*db.str_E(d_cntc)*/ + "', '" + txt_lname.Text + "','" + txt_fname.Text + "', '" + txt_mname.Text + "','" + ""/*at_code*/ + "','" + dtp_dob.Value.ToString("yyyy-MM-dd") + "','" + gender + "','" + (cbo_country.SelectedValue??"").ToString() + "'");

                    String msg = "New record added successfully.";

                    if (db.set_pkincrement("acct_no", pk) == false)
                    {
                        msg = msg + " But, theres a problem in pk.";
                    }
                    else
                    {
                        db.set_pkincrement("d_code", pk);
                    }
                    db.InsertOnTable("guest2", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, mp_code, escaper, t_date, t_time, user_id", value);
                    
                    MessageBox.Show(msg);
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
                String colupd = "title='" + cbo_title.Text + "', last_name='" + txt_lname.Text + "', first_name='" + txt_fname.Text + "', mid_name='" + txt_mname.Text + "', full_name='" + txt_lname.Text + ", " + txt_fname.Text + " " + txt_mname.Text + "', gender='" + gender + "', birth_date='" + dtp_dob.Value.ToString("yyyy-MM-dd") + "', address1='" + txt_addr.Text + "', tel_num='" + txt_contact.Text + "', email='" + txt_email.Text + "', comp_code='" + (cbo_company.SelectedValue ?? "").ToString() + "', passport_no='" + txt_passport.Text + "', passport_issued='" + dtp_issuedon.Value.ToString("yyyy-MM-dd") + "', passport_expiry='" + dtp_expired.Value.ToString("yyyy-MM-dd") + "', cntry_code='" + (cbo_country.SelectedValue??"").ToString() + "', mp_code='101', escaper='" + isescaper + "', t_date='" + db.get_systemdate("") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "',user_id='" + GlobalClass.username + "'";
                
                if (db.UpdateOnTable("guest", colupd, "acct_no='" + txt_acctno.Text + "'"))
                {
                    db.UpdateOnTable("m06", "d_name='" + txt_lname.Text + ", " + txt_fname.Text + " " + txt_mname.Text + "', d_addr2='" + txt_addr.Text + "', d_tel='" + txt_contact.Text + "', d_email='" + txt_email.Text + "', type='" + "Customer" + "', lastname='" + txt_lname.Text + "',firstname='" + txt_fname.Text + "', mname='" + txt_mname.Text + "'", "d_code='" + txt_acctno.Text + "'");

                    db.InsertOnTable("guest2", "acct_no, title, last_name, first_name, mid_name, full_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, mp_code, escaper, t_date, t_time, user_id", value);
                    MessageBox.Show("Record updated successfully.");
                    form_clear();
                    form_reset();
                    btn_search_Click(btn_search, e);
                }
                else
                {
                    MessageBox.Show("Record cannot update.");
                }
            }
        }
        
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            isnew = false;
            form_clear();
            form_reset();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            String cond = "";
            String join = "";

            if (txt_searchlname.Text != "")
            {
                cond = "full_name LIKE '%" + txt_searchlname.Text + "%'";
                join = " AND ";
            }
            if (txt_searchfname.Text != "")
            {
                cond = cond + "" + join + "full_name LIKE '%" + txt_searchfname.Text + "%'";
                join = " AND ";
            }
            //if (cbo_searchcompany.SelectedIndex != -1)
            //{
            //    cond = cond + "" + join + "comp_code='" + cbo_searchcompany.SelectedValue.ToString() + "'";
            //    join = " AND ";
            //}
            //if (cbo_searchcountry.SelectedIndex != -1)
            //{
            //    cond = cond + "" + join + "cntry_code='" + cbo_searchcountry.SelectedValue.ToString() + "'";
            //}

            pg = 1;
            int doffset = (qlistlimit * (pg - 1));
            dgv_guestlist.DataSource = get_guestlist(doffset, cond);
            lbl_pgno.Text = pg.ToString();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            pg--;
            if (pg < 1) pg = 1;

            int doffset = (qlistlimit * (pg - 1));
            dgv_guestlist.DataSource = get_guestlist(doffset,"");
            lbl_pgno.Text = pg.ToString();
            
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            pg++;
            int doffset = (qlistlimit * pg - 1);
            dgv_guestlist.DataSource = get_guestlist(doffset,"");
            lbl_pgno.Text = pg.ToString();
        }

        private void dgv_guestlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            form_toedit();

            try
            {
                int row = dgv_guestlist.CurrentCell.RowIndex;

                String last_name = (dgv_guestlist.Rows[row].Cells[0].Value??"").ToString().Trim();
                String first_name = (dgv_guestlist.Rows[row].Cells[1].Value ?? "").ToString().Trim();
                String mid_name = (dgv_guestlist.Rows[row].Cells[2].Value ?? "").ToString().Trim();
                String gender = (dgv_guestlist.Rows[row].Cells[3].Value ?? "").ToString().Trim();
                DateTime birth_date = DateTime.Now;
                try { birth_date = Convert.ToDateTime(dgv_guestlist.Rows[row].Cells[4].Value ?? "".ToString().Trim()); }
                catch { }
                String address1 = (dgv_guestlist.Rows[row].Cells[5].Value ?? "").ToString().Trim();
                String tel_num = (dgv_guestlist.Rows[row].Cells[6].Value ?? "").ToString().Trim();
                String email = (dgv_guestlist.Rows[row].Cells[7].Value ?? "").ToString().Trim();
                String comp_code = (dgv_guestlist.Rows[row].Cells[8].Value ?? "").ToString().Trim();
                String passport_no = (dgv_guestlist.Rows[row].Cells[9].Value ?? "").ToString().Trim();
                DateTime passport_issued = DateTime.Now;
                try { passport_issued = Convert.ToDateTime((dgv_guestlist.Rows[row].Cells[10].Value ?? "").ToString().Trim()); } catch { }
                DateTime passport_expiry = DateTime.Now;
                try { passport_expiry = Convert.ToDateTime((dgv_guestlist.Rows[row].Cells[11].Value ?? "").ToString().Trim()); }catch { }
                String cntry_code = (dgv_guestlist.Rows[row].Cells[12].Value ?? "").ToString().Trim();
                String escaper = (dgv_guestlist.Rows[row].Cells[13].Value ?? "").ToString().Trim();
                String notes = (dgv_guestlist.Rows[row].Cells[14].Value ?? "").ToString().Trim();
                String acct_no = (dgv_guestlist.Rows[row].Cells[15].Value ?? "").ToString().Trim();
                String title = (dgv_guestlist.Rows[row].Cells[16].Value ?? "").ToString().Trim();
                
                txt_acctno.Text = acct_no;
                cbo_title.Text = title;
                txt_lname.Text = last_name;
                txt_fname.Text = first_name;
                txt_mname.Text = mid_name;
                cbo_gender.Text = (gender == "F" ? "Female" : "Male");
                txt_addr.Text = address1;
                txt_contact.Text = tel_num;
                txt_email.Text = email;

                cbo_searchcompany.SelectedValue = comp_code;
                cbo_searchcountry.SelectedValue = cntry_code;
                cbo_company.Text = cbo_searchcompany.Text;
                cbo_country.Text = cbo_searchcountry.Text;

                txt_passport.Text = passport_no;

                dtp_dob.Value = birth_date;
                dtp_issuedon.Value = passport_issued;
                dtp_expired.Value = passport_expiry;

                rtxt_remarks.Text = notes;

                if (escaper == "Y")
                    chk_escaper.Checked = true;
                else
                    chk_escaper.Checked = false;

                disp_guesthist(acct_no);
            }
            catch (Exception) { }
        }

        private void disp_guesthist(String acctno)
        {
            try
            {
                thisDatabase db = new thisDatabase();

                dgv_guestfolliohist.DataSource = db.QueryOnTableWithParams("gfhist", "reg_num AS folio_no, rom_code AS room, arr_date AS Arrival_Date, dep_date AS Departure_Date, remarks AS Remarks", "acct_no='"+ acctno +"'", "ORDER BY reg_num ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void dgv_guestlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
