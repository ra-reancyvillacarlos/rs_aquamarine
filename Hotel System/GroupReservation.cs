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
    public partial class GroupReservation : Form
    {
        addGuestinGroup aggrp;
        String schema = "";
        String t_date = "";
        DataTable lcl_dt;
        Boolean isnew = true;
        //DataGridViewCheckBoxColumn dgv_chkbox;

        public GroupReservation()
        {
            InitializeComponent();

            aggrp = new addGuestinGroup(this);
        }

        private void GroupReservation_Load(object sender, EventArgs e)
        {
            lcl_dt = new DataTable();
            thisDatabase db = new thisDatabase();

            //dgv_chkbox = new DataGridViewCheckBoxColumn();
            lcl_dt.Columns.Add("Arrival", typeof(String));
            lcl_dt.Columns.Add("Departure", typeof(String));
            lcl_dt.Columns.Add("Room", typeof(String));
            lcl_dt.Columns.Add("Description", typeof(String));
            lcl_dt.Columns.Add("Type", typeof(String));
            lcl_dt.Columns.Add("Guest No", typeof(String));
            lcl_dt.Columns.Add("Guest(s)", typeof(String));
            lcl_dt.Columns.Add("Occupancy", typeof(String));
            lcl_dt.Columns.Add("Rate Type", typeof(String));
            lcl_dt.Columns.Add("Disc", typeof(String));
            lcl_dt.Columns.Add("Room Rate", typeof(String));
            lcl_dt.Columns.Add("Room Rate Total", typeof(String));
            lcl_dt.Columns.Add("Free B-Fast", typeof(String));
            lcl_dt.Columns.Add("Blocked", typeof(String));
                        
            schema = db.get_schema();
            t_date = db.get_systemdate("");
            dtp_checkin.Value = DateTime.Today;
            dtp_checkout.Value = DateTime.Today;

            set_roomstatuslist();

            cbo_type.SelectedIndex = -1;

            txt_grp_code.Hide();
            load_company();
            load_mkt();
            cbo_srchcomp.SelectedIndex = -1;

            cbo_mkt.SelectedValue = "002";
            disp_resgrp();
        }

        private void disp_resgrp()
        {
            thisDatabase db = new thisDatabase();

            dgv_reslist.DataSource = db.get_grpresevationlist("");
        }

        private void load_company()
        {
            DataTable dt = new DataTable();
            thisDatabase db = new thisDatabase();

            dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_name ASC;");

            cbo_srchcomp.DataSource = dt;
            cbo_srchcomp.DisplayMember = "comp_name";
            cbo_srchcomp.ValueMember = "comp_code";
        }

        private void load_mkt()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("market", "mkt_code, mkt_desc", "", "ORDER BY mkt_code ASC;");

                cbo_mkt.DataSource = dt;
                cbo_mkt.DisplayMember = "mkt_desc";
                cbo_mkt.ValueMember = "mkt_code";

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void set_roomstatuslist()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                dt.Rows.Add();
                dt.AcceptChanges();

                dt = db.get_roomtypExptZ();

                DataRow newRow = dt.NewRow();

                newRow["typ_code"] = "*";
                newRow["typ_desc"] = "";

                dt.Rows.Add(newRow);

                cbo_type.DataSource = dt;
                cbo_type.DisplayMember = "typ_desc";
                cbo_type.ValueMember = "typ_code";
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txt_noofnights.Text) < 0)
            {
                MessageBox.Show("No. of Nights must not less than zero.");
            }
            else
            {
                dtp_checkin.Enabled = false;
                dtp_checkout.Enabled = false;
                txt_noofnights.Enabled = false;

                lbl_arrdt.Text = dtp_checkin.Value.ToString("MM/dd/yyyy");
                lbl_depdt.Text = dtp_checkout.Value.ToString("MM/dd/yyyy");
                lbl_noofnight.Text = txt_noofnights.Text;

                load_rom_available();
            }
        }

        private void load_rom_available()
        {
            /*String WHERE = "";
            DataTable dt_allrooms = new DataTable();
            DataTable dt_reserved = new DataTable();
            DataTable dt_occupied = new DataTable();

            if (String.IsNullOrEmpty(cbo_type.Text) == false)
            {
                WHERE = " AND r.typ_code = '" + cbo_type.SelectedValue.ToString() + "'";
            }

            thisDatabase db = new thisDatabase();
            String SQL_allrooms = "SELECT r.rom_code, r.rom_desc, rt.typ_desc, r.stat_code FROM " + schema + ".rooms r INNER JOIN " + schema + ".rtype rt ON rt.typ_code=r.typ_code  WHERE r.typ_code != 'Z'" + WHERE + " ORDER BY r.rom_code ASC";
            
            dt_allrooms = db.QueryBySQLCode(SQL_allrooms);
            dt_reserved = db.get_reservedroomExptZ(dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), cbo_type.SelectedValue.ToString());
            dt_occupied = db.get_occupancyExptZ("rom_code, res_code, full_name, arr_date, dep_date", dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), cbo_type.SelectedValue.ToString());

            DataRow[] drr;

            //DataGridViewCheckBoxColumn dgv_chkbox = new DataGridViewCheckBoxColumn();
            //dgv_chkbox.ReadOnly = false;

            //remove reserved
            if (dt_reserved.Rows.Count > 0)
            {
                try
                {
                    for (int r = 0; r < dt_reserved.Rows.Count; r++)
                    {
                        drr = dt_allrooms.Select("rom_code='" + dt_reserved.Rows[r]["rom_code"].ToString() + "'");

                        if (drr.Length > 0)
                        {
                            drr[0].Delete();
                            dt_allrooms.AcceptChanges();
                        }
                    }
                }
                catch (Exception) { }
            }

            //remove occuppied
            if (dt_occupied.Rows.Count > 0)
            {
                try
                {
                    for (int o = 0; o < dt_occupied.Rows.Count; o++)
                    {
                        drr = dt_allrooms.Select("rom_code='" + dt_occupied.Rows[o]["rom_code"].ToString() + "' ");

                        if (drr.Length > 0)
                        {
                            drr[0].Delete();
                            dt_allrooms.AcceptChanges();
                        }
                    }
                }
                catch (Exception) { }
            }

            dgv_rom_available.DataSource = dt_allrooms; */

            String typ_code = "";
            DataTable dt_allrooms = new DataTable();
            DataTable dt_reserved = new DataTable();
            DataTable dt_occupied = new DataTable();

            if (cbo_type.SelectedIndex == -1)
            {
                typ_code = "";
            }
            else
            {
                typ_code = cbo_type.SelectedValue.ToString();
            }

            thisDatabase db = new thisDatabase();

            dt_allrooms = db.get_allroomExpZ(typ_code, null, null);
            dt_reserved = db.get_reservedroomExptZ(dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), typ_code);
            dt_occupied = db.get_occupancyExptZ("rom_code, res_code, full_name, arr_date, dep_date", dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), typ_code);

            DataRow[] drr;

            lbl_rom.Text = dt_allrooms.Rows.Count.ToString();
            lbl_res.Text = dt_reserved.Rows.Count.ToString();
            lbl_occ.Text = dt_occupied.Rows.Count.ToString();

            //remove reserved
            if (dt_reserved.Rows.Count > 0)
            {
                try
                {
                    for (int r = 0; r < dt_reserved.Rows.Count; r++)
                    {
                        //MessageBox.Show(dt_reserved.Rows[r]["dep_date"].ToString() + " - " + dtp_checkout.Value.ToString("yyyy-MM-dd") + " == " + Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).Equals(dtp_checkout.Value).ToString());
                        //MessageBox.Show(dt_reserved.Rows[r]["rom_code"].ToString() + ":: " +db.get_systemdate() + " => " + Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate())).ToString() + " and " + Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate())).ToString() + " and " + String.IsNullOrEmpty(dt_reserved.Rows[r]["arrived"].ToString()).ToString());
                        if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) <= 0 && Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) >= 0 && String.IsNullOrEmpty(dt_reserved.Rows[r]["arrived"].ToString()) == true)
                        {
                            //MessageBox.Show(dt_reserved.Rows[r]["arrived"].ToString() + " is not arrived.");
                            //if equal nothing to do.
                        }
                        else if (Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).Equals(dtp_checkin.Value))
                        {
                            //if equal nothing to do.
                        }
                        else if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).Equals(dtp_checkout.Value))
                        {
                            //if equal nothing to do.
                        }
                        else
                        {
                            drr = dt_allrooms.Select("rom_code='" + dt_reserved.Rows[r]["rom_code"].ToString() + "'");

                            if (drr.Length > 0)
                            {
                                drr[0].Delete();
                                dt_allrooms.AcceptChanges();
                            }
                        }
                    }
                }
                catch (Exception) { }
            }

            //remove occuppied
            if (dt_occupied.Rows.Count > 0)
            {
                try
                {
                    for (int o = 0; o < dt_occupied.Rows.Count; o++)
                    {
                        //MessageBox.Show(dt_occupied.Rows[o]["rom_code"].ToString() + ": " + dt_occupied.Rows[o]["dep_date"].ToString() + " - " + dtp_checkin.Value.ToString("yyyy-MM-dd") + " == " + Convert.ToDateTime(dt_occupied.Rows[o]["dep_date"].ToString()).Equals(dtp_checkin.Value).ToString());
                        if (Convert.ToDateTime(dt_occupied.Rows[o]["dep_date"].ToString()).Equals(dtp_checkin.Value))
                        {
                            //if equal nothing to do. means to be available
                        }
                        else
                        {
                            drr = dt_allrooms.Select("rom_code='" + dt_occupied.Rows[o]["rom_code"].ToString() + "' ");

                            if (drr.Length > 0)
                            {
                                drr[0].Delete();
                                dt_allrooms.AcceptChanges();
                            }
                        }
                    }
                }
                catch (Exception) { }
            }

            lbl_row.Text = dt_allrooms.Rows.Count.ToString();
            dgv_rom_available.DataSource = dt_allrooms;

            if (dt_occupied.Columns.Count > 1)
            {
                try
                {
                    for (int r = 1; r < dgv_rom_available.Columns.Count; r++)
                    {
                        dgv_rom_available.Columns[r].ReadOnly = true;
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
            //ColumnList.Columns.Add("Column Fields", gettype(CheckBox));
        }

        private void txt_noofnights_TextChanged(object sender, EventArgs e)
        {
            if (e.Equals(""))
            {
                txt_noofnights.Text = "1";
            }
            else
            {
                dtp_checkout.Value = dtp_checkin.Value.AddDays(Convert.ToInt32(txt_noofnights.Text));
            }
        }

        private void dtp_checkout_ValueChanged(object sender, EventArgs e)
        {
            double val = (dtp_checkout.Value - dtp_checkin.Value).TotalDays;

            txt_noofnights.Text = val.ToString();

            if (val == 0)
            {
                txt_noofnights.Text = "1";
            }
        }

        private void btn_grpselect_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_grpselect_Click(object sender, EventArgs e)
        {
            addGroup agrp = new addGroup();

            agrp.Show();
        }

        private void btn_romupd_Click(object sender, EventArgs e)
        {
            //aggrp = new addGuestinGroup();
            DataTable dt = new DataTable();
            String arr, dep, rm, rmtyp, rttyp, disc, occ, guestno, rmrate, total;

            try
            {
                if (dgv_rom.Rows.Count > 0)
                {
                    dt.Columns.Add("arr", typeof(String));
                    dt.Columns.Add("dep", typeof(String));
                    dt.Columns.Add("rm", typeof(String));
                    dt.Columns.Add("rmtyp", typeof(String));
                    dt.Columns.Add("guestno", typeof(String));
                    dt.Columns.Add("occ", typeof(String));
                    dt.Columns.Add("rttyp", typeof(String));
                    dt.Columns.Add("disc", typeof(String));
                    dt.Columns.Add("rmrate", typeof(String));
                    dt.Columns.Add("total", typeof(String));

                    foreach (DataGridViewRow dgv_r in dgv_rom.SelectedRows)
                    {
                        arr = dgv_r.Cells[0].Value.ToString();
                        dep = dgv_r.Cells[1].Value.ToString();
                        rm = dgv_r.Cells[2].Value.ToString();
                        rmtyp = dgv_r.Cells[4].Value.ToString();
                        guestno = dgv_r.Cells[5].Value.ToString();
                        occ = dgv_r.Cells[7].Value.ToString();
                        rttyp = dgv_r.Cells[8].Value.ToString();
                        disc = dgv_r.Cells[9].Value.ToString();
                        rmrate = dgv_r.Cells[10].Value.ToString();
                        total = dgv_r.Cells[11].Value.ToString();

                        dt.Rows.Add(arr, dep, rm, rmtyp, guestno, occ, rttyp, disc, rmrate, total);
                    }

                    aggrp.set_passdatatable(dt);
                    aggrp.Show();
                }
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
            }
        }

        private void clr_field()
        {
            try
            {
                lbl_resno.Text = "";
                lbl_arrdt.Text = "";
                lbl_depdt.Text = "";
                lbl_noofnight.Text = "0";

                //dtp_resdt.Value = DateTime.Now.ToString("MM/dd/yyyy");
                cbo_mkt.SelectedIndex = -1;

                lbl_grpname.Text = "";
                lbl_contact.Text = "";
                lbl_company.Text = "";
                lbl_contactno.Text = "";
                txt_grp_code.Text = "";

                rtxt_remark.Text = "";

                lbl_userid.Text = "";
            }
            catch (Exception er) {}
        }

        private void clr_dgv_rom()
        {
            try
            {
                for (int i = 0; i < dgv_rom.Rows.Count; i++)
                {
                    dgv_rom.Rows.RemoveAt(i);
                }

                lcl_dt.Rows.Clear();
            }
            catch(Exception){}
        }
        
        private void add_to_rom()
        {
            DataTable temp_dt = lcl_dt;

            dgv_rom.DataSource = temp_dt;

            tbcntrl_grpres.SelectedTab = tpg_info;

            tpg_info.Show();

            temp_dt = null;
        }

        private void btn_add_res_Click(object sender, EventArgs e)
        {
            DataTable temp_dt = lcl_dt;

            dgv_rom.DataSource = temp_dt;

            tbcntrl_grpres.SelectedTab = tpg_info;

            tpg_info.Show();

            temp_dt = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = GlobalClass.gdt;

                foreach (DataRow rows in dt.Rows)
                {
                    txt_grp_code.Text = rows[0].ToString();
                    lbl_grpname.Text = rows[1].ToString();
                    lbl_contact.Text = rows[2].ToString();
                    lbl_company.Text = rows[3].ToString();
                    lbl_contactno.Text = rows[4].ToString();
                }
            }
            catch (Exception) { }
        }

        private int get_occ_number(String occ)
        {
            int num = 1;

            try
            {
            if (occ == "Double")
                {
                    num = 2;
                }
                else if (occ == "Triple")
                {
                    num = 3;
                }
                else if (occ == "Quad")
                {
                    num = 4;
                }
            }
            catch(Exception) {}

            return num;
        }
            
        public void upd_selrom(String guestno, String guest, String occ, String rtcode, String rtcode_name, String disc, String disc_name, String disc_amount, String rmrate, String rmtotal, String blocked, String bfast)
        {
            Double total = 0.00;
            try
            {
                foreach (DataGridViewRow dgv_r in dgv_rom.SelectedRows)
                {
                    dgv_r.Cells[5].Value = guestno;
                    dgv_r.Cells[6].Value = guest;
                    dgv_r.Cells[7].Value = get_occ_number(occ).ToString();
                    dgv_r.Cells[8].Value = rtcode + "::" + rtcode_name;
                    dgv_r.Cells[9].Value = disc + "::" + disc_name + "::" + disc_amount;
                    dgv_r.Cells[10].Value = rmrate;
                    dgv_r.Cells[11].Value = rmtotal;
                    dgv_r.Cells[12].Value = bfast;
                    dgv_r.Cells[13].Value = blocked;

                    total += Convert.ToDouble(rmtotal);
                }

                txt_total_amt.Text = total.ToString();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void btn_romdel_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dgv_rom.CurrentCell.RowIndex;

                dgv_rom.Rows.RemoveAt(row);

                //lbl_noofguest.Text = (dgv_guestlist.Rows.Count - 1).ToString();
            }
            catch (Exception) { }
        }

        private void btn_ressave_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String rgrp_code = db.get_pk("rgrp_code");
            String[] gacctno_arr;
            String[] gfullname_arr;
            String[] disc_arr;
            Boolean issuccess = false;
            String[] rmrate_arr;
            int i = 1;

            if (txt_grp_code.Text == "")
            {
                MessageBox.Show("Group Information is required.");
            }
            else
            {
                try
                {
                    if (isnew == true)
                    {
                        String col = "rgrp_code, grp_code, \"group\", contact, start, \"end\", user_id, t_date, t_time, res_date, res_type, mkt_code, trv_code, rm_features, bill_info, remarks";
                        String val = "'" + rgrp_code + "', '" + txt_grp_code.Text + "', '" + lbl_grpname.Text + "','" + lbl_contact.Text + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + dtp_resdt.Value.ToString("yyyy-MM-dd") + "', 'D', '" + cbo_mkt.SelectedValue.ToString() + "', '000001','','','" + rtxt_remark.Text + "'";

                        if (db.InsertOnTable("resvngrp", col, val))
                        {
                            //increment rescode to m99
                            db.set_pkm99("rgrp_code", db.get_nextincrementlimitchar(rgrp_code, 8));

                            //insert to resfil
                            try
                            {
                                String arr_dt, dep_dt, rm, rmdesc, typ, guestno, guest, occ, rttyp, disc, disc_amt, rmrate, totalamt = "0.00", fbfast = "0", blocked = "", isblocked = "N";
                                dgv_rom.AllowUserToAddRows = false;

                                foreach (DataGridViewRow row in dgv_rom.Rows)
                                {
                                    arr_dt = row.Cells[0].Value.ToString();
                                    dep_dt = row.Cells[1].Value.ToString();
                                    rm = row.Cells[2].Value.ToString();
                                    rmdesc = row.Cells[3].Value.ToString();
                                    typ = db.get_romtyp_code(row.Cells[4].Value.ToString());
                                    guestno = row.Cells[5].Value.ToString();
                                    guest = row.Cells[6].Value.ToString();
                                    occ = row.Cells[7].Value.ToString();
                                    rttyp = row.Cells[8].Value.ToString();
                                    disc = row.Cells[9].Value.ToString();
                                    rmrate = row.Cells[10].Value.ToString();
                                    fbfast = row.Cells[12].Value.ToString();
                                    blocked = row.Cells[13].Value.ToString();

                                    gacctno_arr = guestno.Split(new String[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                                    gfullname_arr = guest.Split(new String[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                                    disc_arr = disc.Split(new String[] { "::" }, StringSplitOptions.None);
                                    disc = disc_arr.GetValue(0).ToString();
                                    disc_amt = disc_arr.GetValue(2).ToString();

                                    rmrate_arr = rttyp.Split(new String[] { "::" }, StringSplitOptions.None);
                                    rttyp = rmrate_arr.GetValue(0).ToString();

                                    if (disc_amt == "")
                                    {
                                        disc_amt = "0.00";
                                    }
                                    if (blocked != "")
                                    {
                                        isblocked = "Y";
                                    }

                                    //// copy from reservation ////////////                                
                                    try
                                    {
                                        String res_code = db.get_pk("res_code");
                                        String dacct_no = gacctno_arr.GetValue(0).ToString().TrimStart(new char[] { '/' });
                                        String dfull_name = gfullname_arr.GetValue(0).ToString().TrimStart(new char[] { '/' });

                                        //String col = "res_code, acct_no, full_name, res_date, arr_date, arr_time, dep_date, dep_time, typ_code, rate_code, mkt_code, src_code, trv_code, rm_features, bill_info, remarks, reserv_by,  reserv_thru, reserv_tel, cancel, user_id, t_date, t_time, arrived, occ_type, reason, turnaway, canc_user, canc_date, canc_time, canc_remarks, rgrp_code, rgrp_code, rgrp_ln, discount, fctr_code, rom_rev, food, misc, approv, free_bfast, blockresv, blockby, disc_code, disc_pct";
                                        String col2 = "res_code, acct_no, full_name, res_date, arr_date, arr_time, dep_date, dep_time, typ_code, rate_code, rom_code, rom_rate, res_type, mkt_code, src_code, trv_code, rm_features, bill_info, remarks, reserv_by,  reserv_thru, reserv_tel, user_id, t_date, t_time, occ_type, rgrp_code, rgrp_ln, fctr_code, free_bfast, blockresv, blockby, disc_code, disc_pct";
                                        String val2 = "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "','" + dtp_resdt.Value.ToString("yyyy-dd-MM") + "', '" + arr_dt + "', '" + dtp_arrtime.Value.ToString("HH:mm") + "', '" + dep_dt + "', '" + dtp_deptime.Value.ToString("HH:mm") + "', '" + typ + "', '" + rttyp + "', '" + rm + "', '" + rmrate + "', 'D', '" + cbo_mkt.SelectedValue.ToString() + "', '', '000001','','','" + rtxt_remark.Text + "'";
                                        val2 = val2 + ", '', '','','" + GlobalClass.username + "','" + db.get_systemdate("") + "','" + DateTime.Now.ToString("HH:mm") + "','" + occ + "', '" + rgrp_code + "', '" + i.ToString() + "', '','" + fbfast + "','" + isblocked + "','" + blocked + "','" + disc + "','" + disc_amt + "'";

                                        i++;

                                        if (db.InsertOnTable("resfil", col2, val2))
                                        {
                                            //insert other guests
                                            for (int r = 1; gacctno_arr.Length > r; r++)
                                            {
                                                dacct_no = gacctno_arr.GetValue(r).ToString().TrimStart(new char[] { '/' });
                                                dfull_name = gfullname_arr.GetValue(r).ToString().TrimStart(new char[] { '/' });

                                                db.InsertOnTable("resguest", "res_code, acct_no, full_name, arr_date, dep_date", "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "'");
                                            }
                                            //increment rescode to m99
                                            db.set_pkm99("res_code", db.get_nextincrementlimitchar(res_code, 8));
                                            issuccess = true;
                                        }
                                        else
                                        {
                                            issuccess = false;
                                            break;
                                        }
                                    }
                                    catch (Exception er)
                                    {
                                        issuccess = false;
                                        MessageBox.Show(er.Message);
                                    }
                                }
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show(er.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Problem on Adding New Record.");
                        }

                        if (issuccess == true)
                        {
                            clr_field();
                            clr_dgv_rom();
                            disp_resgrp();
                            MessageBox.Show("Successfully Added new Record");
                        }
                        else
                        {
                            MessageBox.Show("Problem on Adding New Record.");
                        }
                    }
                    //update reservation
                    else
                    {
                        String col = "rgrp_code, grp_code, \"group\", contact, start, \"end\", user_id, t_date, t_time, res_date, res_type, mkt_code, trv_code, rm_features, bill_info, remarks";
                        String val = "'" + rgrp_code + "', '" + txt_grp_code.Text + "', '" + lbl_grpname.Text + "','" + lbl_contact.Text + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + dtp_resdt.Value.ToString("yyyy-MM-dd") + "', 'D', '" + cbo_mkt.SelectedValue.ToString() + "', '000001','','','" + rtxt_remark.Text + "'";

                        if (db.InsertOnTable("resvngrp", col, val))
                        {
                            //increment rescode to m99
                            db.set_pkm99("rgrp_code", db.get_nextincrementlimitchar(rgrp_code, 8));

                            //insert to resfil
                            try
                            {
                                String arr_dt, dep_dt, rm, rmdesc, typ, guestno, guest, occ, rttyp, disc, disc_amt, rmrate, totalamt = "0.00", fbfast = "0", blocked = "", isblocked = "N";
                                dgv_rom.AllowUserToAddRows = false;

                                foreach (DataGridViewRow row in dgv_rom.Rows)
                                {
                                    arr_dt = row.Cells[0].Value.ToString();
                                    dep_dt = row.Cells[1].Value.ToString();
                                    rm = row.Cells[2].Value.ToString();
                                    rmdesc = row.Cells[3].Value.ToString();
                                    typ = db.get_romtyp_code(row.Cells[4].Value.ToString());
                                    guestno = row.Cells[5].Value.ToString();
                                    guest = row.Cells[6].Value.ToString();
                                    occ = row.Cells[7].Value.ToString();
                                    rttyp = row.Cells[8].Value.ToString();
                                    disc = row.Cells[9].Value.ToString();
                                    rmrate = row.Cells[10].Value.ToString();
                                    fbfast = row.Cells[12].Value.ToString();
                                    blocked = row.Cells[13].Value.ToString();

                                    gacctno_arr = guestno.Split(new String[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                                    gfullname_arr = guest.Split(new String[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                                    disc_arr = disc.Split(new String[] { "::" }, StringSplitOptions.None);
                                    disc = disc_arr.GetValue(0).ToString();
                                    disc_amt = disc_arr.GetValue(2).ToString();

                                    rmrate_arr = rttyp.Split(new String[] { "::" }, StringSplitOptions.None);
                                    rttyp = rmrate_arr.GetValue(0).ToString();

                                    if (disc_amt == "")
                                    {
                                        disc_amt = "0.00";
                                    }
                                    if (blocked != "")
                                    {
                                        isblocked = "Y";
                                    }

                                    //// copy from reservation ////////////                                
                                    try
                                    {
                                        String res_code = db.get_pk("res_code");
                                        String dacct_no = gacctno_arr.GetValue(0).ToString().TrimStart(new char[] { '/' });
                                        String dfull_name = gfullname_arr.GetValue(0).ToString().TrimStart(new char[] { '/' });

                                        //String col = "res_code, acct_no, full_name, res_date, arr_date, arr_time, dep_date, dep_time, typ_code, rate_code, mkt_code, src_code, trv_code, rm_features, bill_info, remarks, reserv_by,  reserv_thru, reserv_tel, cancel, user_id, t_date, t_time, arrived, occ_type, reason, turnaway, canc_user, canc_date, canc_time, canc_remarks, rgrp_code, rgrp_code, rgrp_ln, discount, fctr_code, rom_rev, food, misc, approv, free_bfast, blockresv, blockby, disc_code, disc_pct";
                                        String col2 = "res_code, acct_no, full_name, res_date, arr_date, arr_time, dep_date, dep_time, typ_code, rate_code, rom_code, rom_rate, res_type, mkt_code, src_code, trv_code, rm_features, bill_info, remarks, reserv_by,  reserv_thru, reserv_tel, user_id, t_date, t_time, occ_type, rgrp_code, rgrp_ln, fctr_code, free_bfast, blockresv, blockby, disc_code, disc_pct";
                                        String val2 = "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "','" + dtp_resdt.Value.ToString("yyyy-dd-MM") + "', '" + arr_dt + "', '" + dtp_arrtime.Value.ToString("HH:mm") + "', '" + dep_dt + "', '" + dtp_deptime.Value.ToString("HH:mm") + "', '" + typ + "', '" + rttyp + "', '" + rm + "', '" + rmrate + "', 'D', '" + cbo_mkt.SelectedValue.ToString() + "', '', '000001','','','" + rtxt_remark.Text + "'";
                                        val2 = val2 + ", '', '','','" + GlobalClass.username + "','" + db.get_systemdate("") + "','" + DateTime.Now.ToString("HH:mm") + "','" + occ + "', '" + rgrp_code + "', '" + i.ToString() + "', '','" + fbfast + "','" + isblocked + "','" + blocked + "','" + disc + "','" + disc_amt + "'";

                                        i++;

                                        if (db.InsertOnTable("resfil", col2, val2))
                                        {
                                            //insert other guests
                                            for (int r = 1; gacctno_arr.Length > r; r++)
                                            {
                                                dacct_no = gacctno_arr.GetValue(r).ToString().TrimStart(new char[] { '/' });
                                                dfull_name = gfullname_arr.GetValue(r).ToString().TrimStart(new char[] { '/' });

                                                db.InsertOnTable("resguest", "res_code, acct_no, full_name, arr_date, dep_date", "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "', '" + lbl_arrdt.Text + "', '" + lbl_depdt.Text + "'");
                                            }
                                            //increment rescode to m99
                                            db.set_pkm99("res_code", db.get_nextincrementlimitchar(res_code, 8));
                                            issuccess = true;
                                        }
                                        else
                                        {
                                            issuccess = false;
                                            break;
                                        }
                                    }
                                    catch (Exception er)
                                    {
                                        issuccess = false;
                                        MessageBox.Show(er.Message);
                                    }
                                }
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show(er.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Problem on Adding New Record.");
                        }

                        if (issuccess == true)
                        {
                            clr_field();
                            clr_dgv_rom();
                            disp_resgrp();
                            MessageBox.Show("Successfully Added new Record");
                        }
                        else
                        {
                            MessageBox.Show("Problem on Adding New Record.");
                        }
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }               
            }

            dgv_rom.AllowUserToAddRows = true;
        }

        private void btn_rescancel_Click(object sender, EventArgs e)
        {
            clr_field();
            tbcntrl_grpres.SelectedTab = tpg_list;
            tpg_list.Show();
        }        

        private void dgv_rom_available_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            String arr_dt = dtp_checkin.Value.ToString("MM/dd/yyyy");
            String dep_dt = dtp_checkout.Value.ToString("MM/dd/yyyy");
            String rom = dgv_rom_available.Rows[e.RowIndex].Cells[1].Value.ToString();
            String desc = dgv_rom_available.Rows[e.RowIndex].Cells[2].Value.ToString();
            String typ = dgv_rom_available.Rows[e.RowIndex].Cells[3].Value.ToString();

            //if ((MessageBox.Show("Do U Want To Create New Record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
           // {
            //    btn_ressave.Text = "Create Reservation";
            //}
            //else
           // {
           //     btn_ressave.Text = "Update Reservation";
           // }
            clr_field();

            lbl_arrdt.Text = Convert.ToDateTime(arr_dt).ToString("MM/dd/yyyy");
            lbl_depdt.Text = Convert.ToDateTime(dep_dt).ToString("MM/dd/yyyy");
            
            lcl_dt.Rows.Add(arr_dt, dep_dt, rom, desc, typ, null, null, null, null, null, null, null, null);
            add_to_rom();
        }

        private void dgv_rom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv_reslist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_reslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            String arr = "";
            String dep = "";
            String romdesc = "";
            String other_acctno = "";
            String other_fullname = "";
            
            try
            {
                if (dgv_reslist.SelectedRows.Count > 0)
                {
                    String rgrp_code = dgv_reslist[0, e.RowIndex].Value.ToString();
                    String grp_code = dgv_reslist[1, e.RowIndex].Value.ToString();
                    String group = dgv_reslist[1, e.RowIndex].Value.ToString();
                    String contact = dgv_reslist[1, e.RowIndex].Value.ToString();
                    String start = dgv_reslist[1, e.RowIndex].Value.ToString();
                    String end = dgv_reslist[1, e.RowIndex].Value.ToString();
                    String user_id = dgv_reslist[1, e.RowIndex].Value.ToString();

                    isnew = false;

                    dt = db.get_grpreservation_info(rgrp_code);
                    dt2 = db.get_grpreservation_resfil(rgrp_code);

                    clr_field();

                    foreach (DataRow row in dt.Rows)
                    {
                        arr = Convert.ToDateTime(row["start"].ToString()).ToString("MM/dd/yyyy");
                        dep = Convert.ToDateTime(row["end"].ToString()).ToString("MM/dd/yyyy");
                        lbl_resno.Text = row["rgrp_code"].ToString();
                        lbl_arrdt.Text = arr;
                        lbl_depdt.Text = dep;
                        lbl_grpname.Text = row["group"].ToString();
                        lbl_contact.Text = row["contact"].ToString();
                        lbl_company.Text = row["rgrp_code"].ToString();
                        lbl_contactno.Text = row["rgrp_code"].ToString();
                        lbl_userid.Text = row["user_id"].ToString() + " " + Convert.ToDateTime(row["t_date"].ToString()).ToString("MM/dd/yyyy") + " " + row["t_time"].ToString();
                        rtxt_remark.Text = row["rm_features"].ToString() + " " + row["bill_info"].ToString() + " " + row["remarks"].ToString();
                        cbo_mkt.SelectedValue = row["mkt_code"].ToString();
                        txt_grp_code.Text = row["grp_code"].ToString();
                    }

                    foreach (DataRow row2 in dt2.Rows)
                    {
                        romdesc = db.get_romdesc(row2["rom_code"].ToString());

                        dt3 = db.get_resguest(row2["res_code"].ToString());

                        foreach (DataRow row3 in dt3.Rows)
                        {
                            other_acctno = other_acctno + "/" + row3["acct_no"].ToString();
                            other_fullname = other_fullname + "/" + row3["full_name"].ToString();
                        }

                        lcl_dt.Rows.Add(Convert.ToDateTime(row2["arr_date"].ToString()).ToString("MM/dd/yyyy"), Convert.ToDateTime(row2["dep_date"].ToString()).ToString("MM/dd/yyyy"), row2["rom_code"].ToString(), romdesc, db.get_romtyp_desc(row2["typ_code"].ToString()), "/" + row2["acct_no"].ToString() + other_acctno, "/" + row2["full_name"].ToString() + other_fullname, row2["occ_type"].ToString(), row2["rate_code"].ToString() + "::" + db.get_ratetype_desc(row2["rate_code"].ToString()), row2["disc_code"].ToString() + "::" + db.get_disc_desc(row2["disc_code"].ToString()) + "::" + row2["disc_pct"].ToString(), row2["rom_rate"].ToString(), "total", row2["free_bfast"].ToString(), row2["blockby"].ToString());
                        
                        add_to_rom();
                    }

                    tbcntrl_grpres.SelectedTab = tpg_info;
                    tpg_info.Show();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }   
        }

        private void btn_srchreservation_Click(object sender, EventArgs e)
        {
            disp_resgrp();
        }

        private void dgv_rom_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
