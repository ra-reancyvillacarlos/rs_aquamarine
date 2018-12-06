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
    public partial class newReservation : Form
    {
        cancelReservation cRes;

        Boolean seltbp = false;
        Boolean isnew = false;
        public String active_res_where = "";
        public Boolean cur_load = true;
        Boolean saveClickProc = false;
        Boolean forGfolio = false;
        String forWalkIn = "";
        public String cur_walkIn = "";

        Double l_price = 0.00;
        Double c_price = 0.00;
        String l_code = "";
        String c_code = "";

        public newReservation(Main m)
        {
            InitializeComponent();

            cRes = new cancelReservation(this);

        }
        public newReservation()
        {
            InitializeComponent();
            cRes = new cancelReservation(this);
            thisDatabase db2 = new thisDatabase();
            String grp_id2 = "";
            DataTable dt24 = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt24.Rows.Count > 0)
            {
                grp_id2 = dt24.Rows[0]["grp_id"].ToString();
            }
            DataTable dt23 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id2 + "' AND a.mod_id='H3000' ORDER BY b.pla, b.mod_id");

            if (dt23.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt23.Rows[0]["add"].ToString();
                update = dt23.Rows[0]["upd"].ToString();
                delete = dt23.Rows[0]["cancel"].ToString();
                print = dt23.Rows[0]["print"].ToString();

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
                    btn_cancel.Enabled = false;
                }
                if (print == "n")
                {
                    btn_print.Enabled = false;
                }

            }
            // disp_list();
        }

        private void m_brand_Load(object sender, EventArgs e)
        {
            tpg_info_enable(false);
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            //txt_code.Text = "";
            //txt_name.Text = "";

            lbl_resno.Text = "";
            label6.Text = "0";
            label7.Text = "0";
            label8.Text = "0";
            lbl_noofguest.Text = "0";
            lbl_rm.Text = "";
            txt_resby.Text = "";
            txt_contact.Text = "";
            txt_discamt.Text = "0.00";

            cbo_agency.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            cbo_mktsegment.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            cbo_disc.SelectedIndex = -1;
            comboBox5.Text = "";

            txt_total_amt.Text = "0.00";

            rtb_remarks.Text = "";
        }

        private void goto_tbcntrl_info()
        {
            seltbp = true;
            tbcntrl_res.SelectedTab = tpg_reg;
            tbcntrl_option.SelectedTab = tpg_opt_2;

            tpg_reg.Show();
            tpg_opt_2.Show();
            seltbp = false;
        }

        private void goto_tbcntrl_list()
        {
            seltbp = true;
            tbcntrl_res.SelectedTab = tpg_list;
            tbcntrl_option.SelectedTab = tpg_opt_1;

            tpg_list.Show();
            tpg_opt_1.Show();
            seltbp = false;
        }

        private void tpg_info_enable(Boolean flag)
        {
            //txt_code.Enabled = flag;
            //txt_name.Enabled = flag;
        }
        public void set_reslist(String search_code)
        {
            try
            {
                search_code = ((search_code != "") ? search_code : ((cur_load == true) ? " WHERE (rf.arrived IS NULL OR rf.arrived !='Y') AND (rf.cancel IS NULL OR rf.cancel !='Y')" : " WHERE (rf.arrived IS NULL OR rf.arrived !='Y') AND (rf.cancel IS NULL OR rf.cancel !='Y')"));
                thisDatabase db = new thisDatabase();
                dgv_reslist.DataSource = db.get_reservationlist(search_code);
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }
        private void clear_dgv()
        {
            try
            {
                dgv_reslist.Rows.Clear();
            }
            catch (Exception)
            { }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            get_all();
        }

        private void get_all()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            DataTable dt_guest = new DataTable();
            DataTable dt_otherguestid = new DataTable();
            String guestno = "", res_code = "";
            String rom_rate = "", rom_rate_typ = "Monthly";
            String occ_code = "";
            int cnt = 1;
            
            try
            {
                if (dgv_reslist.SelectedRows.Count > 0)
                {
                    int row = dgv_reslist.CurrentRow.Index;

                    isnew = false;
                    //pnl_leftfirst.Hide();

                    //get reservation info and pass res_code;
                    dt = db.get_res_info(dgv_reslist["res_code", row].Value.ToString());
                   // DataTable dt_lc = db.QueryBySQLCode("SELECT l_count, c_bool FROM (SELECT COUNT(rg.chg_code) AS l_count FROM rssys.res_gfil rg WHERE rg.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ADTL%' AND UPPER(chg_desc) LIKE 'LUNCH%') AND rg_code = '" + dgv_reslist["res_code", row].Value.ToString() + "' LIMIT 1) l_b INNER JOIN (SELECT COUNT(rg.chg_code) AS c_bool FROM rssys.res_gfil rg WHERE rg.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ADTL%' AND UPPER(chg_desc) LIKE 'CAMERA%') AND rg_code = '" + dgv_reslist["res_code", row].Value.ToString() + "' LIMIT 1) c_b ON 1=1");

                    DataTable dt_lc = db.QueryBySQLCode("SELECT l_count, c_bool FROM (SELECT COUNT(rg.chg_code) AS l_count FROM rssys.res_gfil rg WHERE rg.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ADTL%' AND UPPER(chg_desc) LIKE 'LUNCH%') AND rg_code = '" + dgv_reslist["res_code", row].Value.ToString() + "' LIMIT 1) l_b INNER JOIN (SELECT COUNT(rg.chg_code) AS c_bool FROM rssys.res_gfil rg WHERE rg.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ADTL%' AND UPPER(chg_desc) LIKE 'CAMERA%') AND rg_code = '" + dgv_reslist["res_code", row].Value.ToString() + "' LIMIT 1) c_b ON 1=1");

                    an_frm_load(dgv_reslist["res_code", row].Value.ToString());
                    foreach (DataRow r in dt.Rows)
                    {
                        res_code = r["res_code"].ToString();
                        GlobalMethod gm = new GlobalMethod();

                        gm.load_seller(comboBox5, "trv_code = '"+r["trv_code"].ToString()+"'");
                        //cbo_disc.Selected

                        dtp_resdt.Value = Convert.ToDateTime(r["res_date"].ToString());
                        dtp_arrtime.Value = Convert.ToDateTime(r["arr_time"].ToString());
                        //dtp_deptime.Value = Convert.ToDateTime(r["dep_time"].ToString());
                        lbl_resno.Text = res_code;
                        lbl_arrdt.Value = Convert.ToDateTime(r["arr_date"].ToString());
                        //lbl_depdt.Text = Convert.ToDateTime(r["dep_date"].ToString()).ToString("MM/dd/yyyy");
                        lbl_rm.Text = r["rom_code"].ToString();
                        comboBox3.SelectedValue = r["code"].ToString();
                        label6.Text = ((r["adult"].ToString() == "") ? "0" : r["adult"].ToString().ToUpper().Replace(" ADULT", ""));
                        label7.Text = ((r["kid"].ToString() == "") ? "0" : r["kid"].ToString().ToUpper().Replace(" KID", ""));
                        label8.Text = ((r["inf"].ToString() == "") ? "0" : r["inf"].ToString().ToUpper().Replace(" INFANT", ""));

                        cbo_mktsegment.SelectedValue = r["mkt_code"].ToString();
                        cbo_disc.SelectedValue = r["disc_code"].ToString();
                        cbo_agency.SelectedValue = r["trv_code"].ToString();
                        txt_discamt.Text = r["discount"].ToString();
                        comboBox4.SelectedValue = r["p_typ"].ToString();

                        rtb_remarks.Text = r["remarks"].ToString();
                        txt_resby.Text = r["reserv_by"].ToString();

                        guestno = r["acct_no"].ToString();
                        lbl_noofguest.Text = ((r["ttlpax"].ToString() == "") ? (Convert.ToDouble(r["adult"].ToString().ToUpper().Replace(" ADULT", "") ?? "0") + Convert.ToDouble(r["kid"].ToString().ToUpper().Replace(" KID", "") ?? "0") + Convert.ToDouble(r["inf"].ToString().ToUpper().Replace(" INFANT", "") ?? "0")).ToString() : r["ttlpax"].ToString().ToUpper().Replace(" ALL", ""));

                        textBox1.Text = dt_lc.Rows[0]["l_count"].ToString();
                        int g_g = 0;
                        try { g_g = Convert.ToInt32(dt_lc.Rows[0]["c_bool"].ToString()); }
                        catch { }
                        comboBox5.SelectedValue = r["seller"].ToString();
                        checkBox1.Checked = ((g_g > 0) ? true : false);

                    }
                    //dt_guest = db.get_guest_info(guestno);

                    //dgv_guestlist.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString(), dt_guest.Rows[0]["g_typ"].ToString());

                    dt_otherguestid = db.get_resguest_id(res_code);
                    dt_guest.Rows.Clear();

                    foreach (DataRow rowid in dt_otherguestid.Rows)
                    {
                        dt_guest = db.get_guest_info(rowid["acct_no"].ToString());

                        dgv_guestlist.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString(), dt_guest.Rows[0]["g_typ"].ToString());

                        dt_guest.Rows.Clear();

                        cnt++;
                    }


                    //disp_computed_bill();
                    gchk_rows();
                    //get_aki();

                    tbcntrl_res.SelectedTab = tpg_reg;
                    tbcntrl_option.SelectedTab = tpg_opt_2;
                    tpg_reg.Show();
                }
                else
                {
                    MessageBox.Show("Pls select the room.");
                }
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
            }
        }
        private void btn_delitem_Click(object sender, EventArgs e)
        {
            //    thisDatabase db = new thisDatabase();
            //    int r;

            //    if (dgv_reslist.Rows.Count > 1)
            //    {
            //        r = dgv_reslist.CurrentRow.Index;

            //        if (db.UpdateOnTable("brand", "cancel='Y'", "brd_code='" + dgv_reslist["ID", r].Value.ToString() + "'"))
            //        {
            //            disp_list();
            //            goto_tbcntrl_list();
            //            tpg_info_enable(false);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Failed on deleting.");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No rows selected.");
            //    }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
            try
            {
                dgv_guestlist.Rows.Clear();
                dataGridView1.Rows.Clear();
            }
            catch { }

            set_reslist(active_res_where);
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tpg_info_enable(true);
            goto_tbcntrl_list();
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

        private void dgv_reslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            get_all();
        }
        private void disp_computed_bill()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count < 1)
                {
                    MessageBox.Show("Please add 1 or more of the service(s).");
                }
                else if (dgv_guestlist.Rows.Count < 1)
                {
                    MessageBox.Show("Please select 1 or more  of the guest(s).");
                }
                else
                {
                    thisDatabase db = new thisDatabase();
                    String c_all = "";
                    Boolean new_stat = true;
                    String res_code = ((isnew == true) ? db.get_pk("res_code") : lbl_resno.Text.ToString());

                    if (isnew == true) { } else { db.DeleteOnTable("res_gfil", "rg_code ='" + res_code + "'"); }
                    //db.DeleteOnTable("res_gfil", "rg_code = '" + res_code + "' AND chg_code LIKE 'ADTL%'");
                    if (checkBox1.Checked)
                    {
                        vald_save(c_price, "1 ADTL", c_code);
                    }
                    int g_f = 0;
                    try { g_f = Convert.ToInt32(textBox1.Text.ToString()); }
                    catch { MessageBox.Show("Input a valid number for lunch"); }
                    if (g_f > 0)
                    {
                        vald_save((l_price * g_f) , "" + textBox1.Text.ToString() + " ADTL", l_code);
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            if (Convert.ToBoolean(dataGridView1["dgvl_chk", i].Value.ToString()) == true)
                            {
                                if (dataGridView1["dgvl_sdesc", i].Value.ToString().ToUpper().Contains("PACKAGE"))
                                {
                                    Double val_dat = 0.00;
                                    String val_typ = "";
                                    try
                                    {
                                        val_dat = ((dataGridView1["dgvl_sdesc", i].Value.ToString().ToUpper().Contains("ADULT")) ? (Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) * Convert.ToDouble(dataGridView1["dgvl_price", i].Value.ToString())) : (Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) * Convert.ToDouble(dataGridView1["dgvl_price", i].Value.ToString())));
                                        val_typ = ((dataGridView1["dgvl_sdesc", i].Value.ToString().ToUpper().Contains("ADULT")) ? "" + dataGridView1["pax", i].Value.ToString() + " ADULT" : "" + dataGridView1["pax", i].Value.ToString() + " KID");

                                        if ((dataGridView1["dgvl_sdesc", i].Value.ToString().ToUpper().Contains("ADULT")))
                                        {
                                            vald_save(val_dat, val_typ, dataGridView1["dgvl_code", i].Value.ToString());
                                        }
                                        if ((dataGridView1["dgvl_sdesc", i].Value.ToString().ToUpper().Contains("KID")))
                                        {
                                            vald_save(val_dat, val_typ, dataGridView1["dgvl_code", i].Value.ToString());
                                        }
                                    }
                                    catch { }
                                }
                                else
                                {
                                    Double val_dat = 0.00;
                                    try
                                    {
                                        val_dat = ((Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) * Convert.ToDouble(dataGridView1["dgvl_price", i].Value.ToString())));

                                        vald_save(val_dat, "" + dataGridView1["pax", i].Value.ToString() + " All", dataGridView1["dgvl_code", i].Value.ToString());

                                    }
                                    catch { }
                                }

                                if (dataGridView1["ifree", i].Value.ToString().ToUpper() == "TRUE" && label8.Text.ToString() != "0" && (Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) == Convert.ToDouble(lbl_noofguest.Text.ToString())))
                                {
                                    vald_save(((Convert.ToDouble(label8.Text.ToString()) * Convert.ToDouble(dataGridView1["dgvl_price", i].Value.ToString())) * -1), ("" + label8.Text.ToString() + " INFANT"), dataGridView1["dgvl_code", i].Value.ToString());
                                }

                                c_all = ((String.IsNullOrEmpty(c_all)) ? c_all + "" + dataGridView1["dgvl_code", i].Value.ToString() + "" : c_all + ", " + dataGridView1["dgvl_code", i].Value.ToString() + "");
                            }
                        }
                        catch { }
                    }
                    if (new_stat == true)
                    {
                        proc_save(Convert.ToDouble(txt_total_amt.Text.ToString()), "" + lbl_noofguest.Text.ToString() + " All", c_all);
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
            catch(Exception er)
            {
                //MessageBox.Show(er.Message);
            }
        }

        private void proc_save(Double price, String occ_g, String c_code)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String curdate = DateTime.Today.ToString("yyyy-MM-dd");
            String curtime = DateTime.Now.ToString("HH:mm");
            String res_code = ((isnew == true) ? db.get_pk("res_code") : lbl_resno.Text.ToString());

            String col_ins = "res_code, acct_no, full_name, res_date, arr_date, arr_time, dep_date, hotel_code, rom_code, mkt_code, remarks, reserv_by, reserv_tel, user_id, t_date, t_time, occ_type, chg_code, price, disc_code, discount, p_typ, trv_code, seller";
            String val_ins = "'" + res_code + "', '" + dgv_guestlist["acct_no", 0].Value.ToString() + "', '" + dgv_guestlist["full_name", 0].Value.ToString() + "', '" + dtp_resdt.Value.ToString("yyyy-MM-dd") + "', '" + lbl_arrdt.Value.ToString("yyyy-MM-dd") + "', '" + dtp_arrtime.Value.ToString("HH:mm") + "', '" + curdate + "', '" + (comboBox3.SelectedValue ?? "").ToString() + "', '" + lbl_rm.Text.ToString() + "', '" + (cbo_mktsegment.SelectedValue ?? "").ToString() + "', '" + rtb_remarks.Text.ToString() + "', '" + txt_resby.Text.ToString() + "', '" + txt_contact.Text.ToString() + "', '" + GlobalClass.username + "', '" + curdate + "', '" + curtime + "', '" + (label6.Text.ToString() + " , " + label7.Text.ToString() + ", " + label8.Text.ToString() + ", " + lbl_noofguest.Text.ToString()) + "', '" + c_code + "', '" + price + "', '" + (cbo_disc.SelectedValue ?? "").ToString() + "', '" + txt_discamt.Text.ToString() + "', '" + (comboBox4.SelectedValue ?? "").ToString() + "', '" + (cbo_agency.SelectedValue ?? "").ToString() + "', '" + (comboBox5.SelectedValue ?? comboBox5.Text).ToString() + "'";
            String val_cond = "res_code = '" + res_code + "'", val_upd = "acct_no = '" + dgv_guestlist["acct_no", 0].Value.ToString() + "', full_name = '" + dgv_guestlist["full_name", 0].Value.ToString() + "', res_date = '" + dtp_resdt.Value.ToString("yyyy-MM-dd") + "', arr_date = '" + lbl_arrdt.Value.ToString("yyyy-MM-dd") + "', arr_time = '" + dtp_arrtime.Value.ToString("HH:mm") + "', dep_date = '" + curdate + "', hotel_code = '" + (comboBox3.SelectedValue ?? "").ToString() + "', rom_code = '" + lbl_rm.Text.ToString() + "', mkt_code = '" + (cbo_mktsegment.SelectedValue ?? "").ToString() + "', remarks = '" + rtb_remarks.Text.ToString() + "', reserv_by = '" + txt_resby.Text.ToString() + "', reserv_tel = '" + txt_contact.Text.ToString() + "', user_id = '" + GlobalClass.username + "', t_date = '" + curdate + "', t_time = '" + curtime + "', occ_type = '" + (label6.Text.ToString() + " , " + label7.Text.ToString() + ", " + label8.Text.ToString() + ", " + lbl_noofguest.Text.ToString()) + "', chg_code = '" + c_code + "', price = '" + price + "', disc_code = '" + (cbo_disc.SelectedValue ?? "").ToString() + "', discount = '" + txt_discamt.Text.ToString() + "', p_typ = '" + (comboBox4.SelectedValue ?? "").ToString() + "', trv_code = '" + (cbo_agency.SelectedValue ?? "").ToString() + "', seller = '" + (comboBox5.SelectedValue ?? comboBox5.Text).ToString() + "'";
            Boolean stat = ((isnew == true) ? db.InsertOnTable("resfil", col_ins, val_ins) : db.UpdateOnTable("resfil", val_upd, val_cond));
            if (stat)
            {
                if (isnew == true) { } else { db.DeleteOnTable("resguest", "res_code='" + res_code + "'"); }
                for (int j = 0; j < dgv_guestlist.Rows.Count; j++)
                {
                    try
                    {
                        db.InsertOnTable("resguest", "res_code, acct_no, full_name, arr_date, dep_date, g_typ", "'" + res_code + "', '" + dgv_guestlist["acct_no", j].Value.ToString() + "', '" + dgv_guestlist["full_name", j].Value.ToString() + "', '" + lbl_arrdt.Value.ToString("yyyy-MM-dd") + "', '" + curdate + "', '" + dgv_guestlist["g_typ", j].Value.ToString() + "'");
                    }
                    catch {  }
                }
                forWalkIn = ((cur_load == true) ? "" : res_code);
                Boolean ch_pk = ((isnew == true) ? db.set_pkm99("res_code", db.get_nextincrementlimitchar(res_code, 8)) : false);
                if (ch_pk) { } else if (ch_pk == false && isnew == false) { } else { MessageBox.Show("Increment Error"); }
                String msg_notif = ((ch_pk == true && isnew == true) ? "Successfully reserved entry." : ((ch_pk == false && isnew == false) ? "Successfully saved entry" : "Erron on saving entry."));
                if (cur_load == true) { MessageBox.Show(msg_notif); } else { saveClickProc = true; vor_get(); }
                btn_back.PerformClick();
            }
            else
            {
                db.DeleteOnTable("res_gfil", "res_code='" + res_code + "'");
                db.DeleteOnTable("resguest", "res_code='" + res_code + "'");
                MessageBox.Show("Error on saving");
            }

        }
        private void vald_save(Double price, String occ_g, String c_code)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String curdate = DateTime.Today.ToString("yyyy-MM-dd");
            String curtime = DateTime.Now.ToString("HH:mm");
            String res_code = ((isnew == true) ? db.get_pk("res_code") : lbl_resno.Text.ToString());

            String col_ins = "rg_code, acct_no, occ_type, chg_code, price";
            String val_ins = "'" + res_code + "', '" + dgv_guestlist["acct_no", 0].Value.ToString() + "', '" + occ_g + "', '" + c_code + "', '" + price + "'";
            Boolean stat = db.InsertOnTable("res_gfil", col_ins, val_ins);
            if (stat)
            {

            }
            else
            {
                MessageBox.Show("Error on saving");
            }
        }
        public void clr_field()
        {
            lbl_resno.Text = "";
            lbl_arrdt.Text = "";
            lbl_noofnight.Text = "0";
            lbl_rm.Text = "";
            //lbl_rmtyp.Text = "";
            comboBox3.SelectedIndex = -1;
            lbl_noofnight_billing.Text = "0";
            lbl_noofguest.Text = "0";
            lbl_depdt.Text = "";
            lbl_blockedby.Text = "";

            cbo_disc.SelectedIndex = -1;
            cbo_mktsegment.SelectedIndex = -1;
            //cbo_type.SelectedIndex = -1;

            txt_contact.Text = "";
            txt_discamt.Text = "0";
            txt_govtrt.Text = "0.00";
            txt_netrt.Text = "0.00";
            txt_rmrate.Text = "0.00";
            txt_total_amt.Text = "0.00";
            txt_resby.Text = "";
            rtb_remarks.Text = "";

            if (dgv_guestlist.Rows.Count > 1)
            {
                for (int i = 0; dgv_guestlist.Rows.Count > i + 1; i++)
                {
                    dgv_guestlist.Rows.RemoveAt(i);
                }
            }

            // dgv_rom_available.DataSource = null;

            btn_cancel.Enabled = false;
            btn_save.Enabled = false;
            //btn_selectcust.Enabled = false;

            //grp_roominfo.Enabled = false;
            //grp_guest.Enabled = false;
            //grpbx_billing.Enabled = false;
        }

        private void newReservation_Load(object sender, EventArgs e)
        {
            frm_load();
        }

        private void an_frm_load(String rc)
        {
            thisDatabase dbs = new thisDatabase();
            DataTable dt_cur = new DataTable();
            DataTable dt_cur_adtl = new DataTable();
            String WHERE = ((rc == "" || String.IsNullOrEmpty(rc)) ? "SELECT false AS bool_check, '0'::text AS pax, 'NORC'::text AS chg_code" : "SELECT true AS bool_check, COALESCE(SPLIT_PART(occ_type, ' ', 1), occ_type, '0') AS pax, chg_code FROM rssys.res_gfil WHERE rg_code = '" + rc + "' GROUP BY chg_code, occ_type ORDER BY chg_code ASC");
            dt_cur = dbs.QueryBySQLCode("SELECT (CASE WHEN bool_check = true THEN bool_check ELSE false END) AS bool_check, COALESCE(pax, '0') AS pax, charge.chg_code, chg_desc, price, ifree FROM rssys.charge LEFT JOIN (" + WHERE + ") rs ON rs.chg_code = charge.chg_code WHERE UPPER(charge.chg_code) NOT LIKE 'TRNS%' AND UPPER(charge.chg_code) NOT LIKE 'ADTL%' AND chg_type = 'C' ORDER BY charge.chg_code ASC");
            dt_cur_adtl = dbs.QueryBySQLCode("SELECT l_code, l_desc, l_price, c_code, c_desc, c_price FROM (SELECT chg_code AS l_code, chg_desc AS l_desc, price AS l_price FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ADTL%' AND UPPER(chg_desc) = 'LUNCH' LIMIT 1) l_b LEFT JOIN (SELECT chg_code AS c_code, chg_desc AS c_desc, price AS c_price FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ADTL%' AND UPPER(chg_desc) = 'CAMERA' LIMIT 1) c_b ON 1=1");

            if (dt_cur_adtl.Rows.Count > 0)
            {
                l_code = dt_cur_adtl.Rows[0]["l_code"].ToString();
                c_code = dt_cur_adtl.Rows[0]["c_code"].ToString();
                try
                {
                    l_price = Convert.ToDouble(dt_cur_adtl.Rows[0]["l_price"].ToString());
                    c_price = Convert.ToDouble(dt_cur_adtl.Rows[0]["c_price"].ToString());
                }
                catch { }
            }
            dataGridView1.DataSource = dt_cur;
        }
        public void frm_load()
        {
            this.WindowState = FormWindowState.Maximized;
            Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            Hotel_System.GlobalMethod gm = new Hotel_System.GlobalMethod();
            //active_res_where = " AND r.arr_date >= '" + db.get_systemdate("") + "' AND (r.arrived IS NULL OR r.arrived !='Y') AND (r.cancel IS NULL OR r.cancel !='Y')";
            active_res_where = "";
            set_reslist(active_res_where);

            //set_compcbo();
            //set_rtcbo();
            //set_mscbo();
            //set_disccbo();
            //set_rmtypecbo();
            gm.load_market(cbo_mktsegment);
            gm.load_hotel(comboBox3);
            //set_rmtypecbo();
            gm.load_disctbl(cbo_disc);
            gm.load_agency(cbo_agency);
            gm.load_charge_paymentsonly(comboBox4);
            //set_roomstatuslist();

            //cbo_type.SelectedIndex = -1;
            clr_field();
            lbl_clerk.Text = GlobalClass.username;
            //disp_res();
        }

        private void btn_additem_Click_1(object sender, EventArgs e)
        {
            //tbcntrl_res.SelectedTab = tpg_reg;
            //tpg_reg.Show();

            tbcntrl_res.SelectedTab = tpg_reg;
            tpg_reg.Show();
            isnew = true;
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
            an_frm_load("");
            //z_Research frm = new z_Research(this);
            //frm.ShowDialog();

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //Report rpt = new Report();
            //rpt.print_mdata(4013);
            //rpt.ShowDialog();
            try
            {
                GlobalMethod gmethod = new GlobalMethod();
                GlobalClass.gdgv = gmethod.CopyDataGridView(dgv_guestlist);
                //mainform.set_modname(modname + " > " + cRes.Text);
                cRes.MdiParent = this.MdiParent;
                cRes.set_data(lbl_resno.Text);
                cRes.Show();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }
        public void reload_guest()
        {
            int ad = 0;
            int kd = 0;
            int inf = 0;
            DataGridViewRow row = new DataGridViewRow();

            if (GlobalClass.gdgv != null)
            {
                dgv_guestlist.Rows.Clear();

                for (int i = 0; i < GlobalClass.gdgv.Rows.Count - 1; i++)
                {
                    row = (DataGridViewRow)GlobalClass.gdgv.Rows[i].Clone();

                    int intColIndex = 0;

                    foreach (DataGridViewCell cell in GlobalClass.gdgv.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    ad = ((GlobalClass.gdgv.Rows[i].Cells[7].Value.ToString() == "A") ? ad + 1 : ad);
                    kd = ((GlobalClass.gdgv.Rows[i].Cells[7].Value.ToString() == "K") ? kd + 1 : kd);
                    inf = ((GlobalClass.gdgv.Rows[i].Cells[7].Value.ToString() == "I") ? inf + 1 : inf);
                    dgv_guestlist.Rows.Add(row);
                }
            }

            label6.Text = ad.ToString();
            label7.Text = kd.ToString();
            label8.Text = inf.ToString();
            lbl_noofguest.Text = (dgv_guestlist.Rows.Count - 1).ToString();

            reset_modname();
            GlobalClass.gdgv = null;
        }
        public void reset_modname()
        {
            // mainform.set_modname(modname);
        }
        private void btn_selectcust_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalMethod gmethod = new GlobalMethod();
                addGuest AG = new addGuest(this, null, null);
                GlobalClass.gdgv = gmethod.CopyDataGridView(dgv_guestlist);

                //mainform.set_modname(modname + " > " + "Select Guest");

                //AG.MdiParent = this.MdiParent;
                AG.reload_guest();
                AG.ShowDialog();

            }
            catch (Exception) { }
        }

        private void dtp_srch_dt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_srchreservation_Click(object sender, EventArgs e)
        {

        }

        private void dgv_reslist_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_reslist_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void dgv_reslist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void btn_srchreservation_Click_1(object sender, EventArgs e)
        {

        }

        private void cbo_disc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_disc.SelectedIndex > -1)
            {
                if (cbo_disc.SelectedValue.ToString() == "014")
                {
                    txt_discamt.Enabled = true;
                    txt_discamt.Text = "0.00";
                }
                else
                {
                    txt_discamt.Enabled = false;
                    txt_discamt.Text = "0.00";
                }
            }

            gchk_rows();
        }

        private void btn_back_Click_1(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void btn_removefrmguestlist_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dgv_guestlist.CurrentRow.Index;

                dgv_guestlist.Rows.RemoveAt(row);

                lbl_noofguest.Text = (dgv_guestlist.Rows.Count - 1).ToString();
            }
            catch (Exception er) { MessageBox.Show("Pls select/highlight the guest to remove."); }
        }
        public void disp_res()
        {

            thisDatabase db = new thisDatabase();
            String WHERE = "";
            String lname = txt_srchlname.Text;
            String fname = txt_srchfname.Text;

            if (String.IsNullOrEmpty(lname) == false)
            {
                WHERE = WHERE + " AND r.full_name LIKE '%" + lname + "%'";
            }
            if (String.IsNullOrEmpty(fname) == false)
            {
                WHERE = WHERE + " AND r.full_name LIKE '%" + fname + "%'";
            }
            if (chk_active.Checked)
            {
                WHERE = WHERE + " AND r.arr_date <= '" + db.get_systemdate("") + "' AND (r.arrived IS NULL OR r.arrived !='Y') AND (r.cancel IS NULL OR r.cancel !='Y')";
            }

            set_reslist(WHERE);
        }
        public void btn_srchreservation_Click_2(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String WHERE = "";
            String lname = txt_srchlname.Text;
            String fname = txt_srchfname.Text;

            WHERE = WHERE + ((cur_load == true) ? " WHERE (rf.arrived IS NULL OR rf.arrived !='Y')" : " WHERE (rf.arrived IS NULL OR rf.arrived !='Y')");

            if (String.IsNullOrEmpty(lname) == false)
            {
                WHERE = WHERE + " AND rf.full_name LIKE '%" + lname + "%'";
            }
            if (String.IsNullOrEmpty(fname) == false)
            {
                WHERE = WHERE + " AND rf.full_name LIKE '%" + fname + "%'";
            }

            WHERE = WHERE + ((chk_active.Checked) ? " AND (rf.cancel IS NULL OR rf.cancel !='Y')" : "");

            set_reslist(WHERE);
        }

        public void gotofront()
        {
            tbcntrl_res.SelectedTab = tpg_list;
            tbcntrl_option.SelectedTab = tpg_opt_1;
            tpg_list.Show();
            tpg_opt_1.Show();
        }

        private void lbl_rm_Click(object sender, EventArgs e)
        {
            z_Research frm = new z_Research(this);
            frm.Show();
        }

        private void grp_roominfo_Enter(object sender, EventArgs e)
        {

        }

        private void panel90_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_delitem_Click_1(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            try
            {
                // added by: Reancy 05-17-2018
                int cur_row = dgv_reslist.CurrentRow.Index;
                DialogResult result = MessageBox.Show("Do you want to delete the reservation of " + dgv_reslist[1, cur_row].Value.ToString() + "?", "Delete Confirmation", MessageBoxButtons.YesNo);
                switch (result)
                {
                    case DialogResult.Yes:
                        if(db.DeleteOnTable("resfil", "res_code='" + dgv_reslist[0, cur_row].Value.ToString() + "'"))
                        {
                            MessageBox.Show("Delete Successful", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbcntrl_res.SelectedTab = tpg_list;
                            tpg_list.Show();
                            tbcntrl_option.SelectedTab = tpg_opt_1;
                        }
                        break;

                    default:
                        break;
                }
                frm_load();
            }
            catch (Exception er) { }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }

        private void chk_blockres_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rtb_remarks_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            gchk_rows();
        }

        private void trash_bin()
        {
            //savebutton

            //occ = "" + label6.Text.ToString() + "A, " + label7.Text.ToString() + "K, " + label8.Text.ToString() + "I";
            ////if (cbo_occtyp.Text == "Double")
            ////{
            ////    occ = 2;
            ////}
            ////else if (cbo_occtyp.Text == "Triple")
            ////{
            ////    occ = 3;
            ////}
            ////else if (cbo_occtyp.Text == "Quad")
            ////{
            ////    occ = 4;
            ////}

            //if (chk_blockres.Checked)
            //{
            //    isblock = "Y";
            //}
            //if (cbo_disc.SelectedIndex > -1)
            //{
            //    disc = cbo_disc.SelectedValue.ToString();
            //    disc_amt = db.get_discount(cbo_disc.SelectedValue.ToString()).ToString("0.00");
            //}

            //if (cbo_rmrttyp.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Pls select Room Rate Type.");
            //}
            //else if (dgv_guestlist.Rows.Count <= 1)
            //{
            //    MessageBox.Show("Pls select guest(s) at Guest list.");
            //}
            //else if (cbo_rtcode.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Pls select the type of rate code.");
            //}
            //else if (cbo_mktsegment.SelectedIndex == -1 && cbo_mktsegment.Text == "")
            //{
            //    MessageBox.Show("Pls select the type of market segment.");
            //}
            //else if (lbl_rm.Text == "")
            //{
            //    MessageBox.Show("Pls select the room the you are going to reserve.");
            //}
            //else if (db.is_roomreserved(lbl_rm.Text) && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
            //{
            //    MessageBox.Show("Room already reserved.");
            //}
            //// Start Modify By: Roldan 04/20/18 check if date reserved  
            //// updated:: Added convert on lbl_arrdt and lbl_depdt by: Reancy
            //// removed by: Reancy 06 22 2018
            ////else if (db.is_arr_roomreserved(lbl_rm.Text, Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd")) || db.is_dep_roomreserved(lbl_rm.Text, Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd")))
            //else if (db.is_roomreservedBythisRescode(lbl_rm.Text, lbl_resno.Text, Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd")))
            //{
            //    MessageBox.Show("Room already reserved.");
            //}
            //// End Modify By: Roldan 04/20/18 check if date reserved
            //else if (cbo_agency.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Pls select travel agency.");
            //}
            //else
            //{
            //    if (db.get_romstatus(lbl_rm.Text) == "OCC" && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
            //    {
            //        if (MessageBox.Show("Room is currently occupied, are you sure you want to continue?", "Room is occupied", MessageBoxButtons.YesNo) == DialogResult.No)
            //        {
            //            cont = false;
            //        }
            //    }
            //    else if (db.get_romstatus(lbl_rm.Text) == "VD" && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
            //    {
            //        if (MessageBox.Show("Room is currently vacant dirty, are you sure you want to continue?", "Room is occupied", MessageBoxButtons.YesNo) == DialogResult.No)
            //        {
            //            cont = false;
            //        }
            //    }
            //    else if (db.get_romstatus(lbl_rm.Text) == "OOO" && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
            //    {
            //        if (MessageBox.Show("Room is currently out of order, are you sure you want to continue?", "Room is occupied", MessageBoxButtons.YesNo) == DialogResult.No)
            //        {
            //            cont = false;
            //        }
            //    }

            //    if (cont == true)
            //    {
            //        try
            //        {
            //            dacct_no = dgv_guestlist.Rows[0].Cells[0].Value.ToString();
            //            dfull_name = dgv_guestlist.Rows[0].Cells[1].Value.ToString();

            //            if (cbo_rmrttyp.SelectedIndex > -1)
            //            {
            //                rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
            //            }

            //            if (isnew == true)
            //            {
            //                // MessageBox.Show("res_date:" + curdate + "\n" + "arr_date:" + dtp_arrtime.Value.ToString("yyyy-MM-dd") + "\n" + "dep_date:" + dtp_deptime.Value.ToString("yyyy-MM-dd"));
            //                String col = "res_code, acct_no, full_name, rmrttyp, res_date, arr_date, arr_time, dep_date, dep_time, rom_code, rom_rate, typ_code, rate_code, mkt_code, src_code, trv_code, rm_features, bill_info, remarks, reserv_by,  reserv_thru, reserv_tel, user_id, t_date, t_time, occ_type, fctr_code, free_bfast, blockresv, blockby, disc_code, disc_pct";

            //                // updated:: Added convert on lbl_arrdt and lbl_depdt by: Reancy
            //                String val = "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "','" + rmrttyp + "','" + curdate + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_arrtime.Value.ToString("HH:mm") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_deptime.Value.ToString("HH:mm") + "', '" + lbl_rm.Text + "', '" + gm.toNormalDoubleFormat(txt_rmrate.Text) + "', '" + rmtyp + "', '" + cbo_rtcode.SelectedValue.ToString() + "', '" + cbo_mktsegment.SelectedValue.ToString() + "', '', '" + cbo_agency.SelectedValue.ToString() + "','','','" + rtb_remarks.Text + "'";
            //                val = val + ", '" + txt_resby.Text + "', '','" + txt_contact.Text + "','" + GlobalClass.username + "','" + db.get_systemdate("") + "','" + curtime + "','" + occ.ToString() + "','','" + txt_discamt.Text + "','" + isblock + "','" + lbl_blockedby.Text + "','" + disc + "','" + disc_amt + "'";
            //                //MessageBox.Show(res_code);
            //                if (db.InsertOnTable("resfil", col, val))
            //                {
            //                    //insert other guests
            //                    for (int r = 1; dgv_guestlist.Rows.Count - 1 > r; r++)
            //                    {
            //                        dacct_no = dgv_guestlist.Rows[r].Cells[0].Value.ToString();
            //                        dfull_name = dgv_guestlist.Rows[r].Cells[1].Value.ToString();

            //                        db.InsertOnTable("resguest", "res_code, acct_no, full_name, arr_date, dep_date", "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "'");
            //                    }
            //                    //increment rescode to m99
            //                    db.set_pkm99("res_code", db.get_nextincrementlimitchar(res_code, 8));

            //                    MessageBox.Show("New Record(s) successfully added.");
            //                    tbcntrl_res.SelectedTab = tpg_list;
            //                    tpg_list.Show();
            //                    //pnl_leftfirst.Show();
            //                    //clr_field();
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Problem on Adding New Record.");
            //                }
            //            }
            //            else
            //            {
            //                // updated:: Added convert on lbl_arrdt and lbl_depdt by: Reancy
            //                res_code = lbl_resno.Text;
            //                if (db.UpdateOnTable("resfil", "acct_no='" + dacct_no + "', rom_code='" + lbl_rm.Text + "', full_name='" + dfull_name + "', res_date='" + curdate + "', arr_date='" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', arr_time='" + dtp_arrtime.Value.ToString("HH:mm") + "', dep_date='" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', dep_time='" + dtp_deptime.Value.ToString("HH:mm") + "', typ_code='" + rmtyp + "', rate_code='" + cbo_rtcode.SelectedValue.ToString() + "', mkt_code='" + cbo_mktsegment.SelectedValue.ToString() + "', src_code='', trv_code='000001', rm_features='', bill_info='', remarks='" + rtb_remarks.Text + "', reserv_by='" + txt_resby.Text + "',  reserv_thru='', reserv_tel='" + txt_contact.Text + "', user_id='" + GlobalClass.username + "', t_date='" + db.get_systemdate("") + "', t_time='" + curtime + "', occ_type='" + occ.ToString() + "', fctr_code='', free_bfast='" + txt_discamt.Text + "', blockresv='" + isblock + "', blockby='" + lbl_blockedby.Text + "', disc_code='" + disc + "', disc_pct='" + disc_amt + "'", "res_code='" + res_code + "'"))
            //                {
            //                    db.DeleteOnTable("resguest", "res_code='" + res_code + "'");

            //                    //insert other guests
            //                    for (int r = 1; dgv_guestlist.Rows.Count - 1 > r; r++)
            //                    {
            //                        dacct_no = dgv_guestlist.Rows[r].Cells[0].Value.ToString();
            //                        dfull_name = dgv_guestlist.Rows[r].Cells[1].Value.ToString();

            //                        db.InsertOnTable("resguest", "res_code, acct_no, full_name, arr_date, dep_date", "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "'");
            //                    }

            //                    MessageBox.Show("Record updated successfully.");
            //                    tbcntrl_res.SelectedTab = tpg_list;
            //                    tpg_list.Show();
            //                    tbcntrl_option.SelectedTab = tpg_opt_1;

            //                    //pnl_leftfirst.Show();
            //                    clr_field();
            //                }
            //            }
            //            set_reslist(active_res_where);
            //            //disp_list(); 
            //            //tbcntrl_res.SelectedTab = tpg_reg;
            //            tbcntrl_option.SelectedTab = tpg_opt_1;
            //            tpg_opt_1.Show();
            //        }
            //        catch (Exception er)
            //        {
            //            MessageBox.Show(er.Message);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Transaction cannot be saved.");
            //    }
            //}
            //end savebutton
        }

        private void get_tl()
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            Double lessdiscount = 0.00;
            Boolean issenior_disc = false;
            Double pck = 0.00;
            Double n_pck = 0.00;
            Double adtl = 0.00;

            try
            {
                if (cbo_disc.SelectedIndex >= 0)
                {
                    lessdiscount = (db.get_discount((cbo_disc.SelectedValue ?? "0.00").ToString()) * 0.01);
                    //issenior_disc = db.issenior_disc(cbo_disc.SelectedValue.ToString());
                }

                if (checkBox1.Checked)
                {
                    adtl = adtl + c_price;
                }
                try
                {
                    adtl = adtl + (l_price * Convert.ToDouble(textBox1.Text.ToString()));
                }
                catch {  }

                Double ad = Convert.ToInt32(label6.Text.ToString());
                Double kd = Convert.ToInt32(label7.Text.ToString());
                Double inf = Convert.ToInt32(label8.Text.ToString());
                Double all = Convert.ToInt32(lbl_noofguest.Text.ToString());

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToBoolean(dataGridView1["dgvl_chk", i].Value.ToString()) == true)
                        {
                            //if ((dataGridView1["dgvl_sdesc", i].Value.ToString()).ToUpper().Contains("PACKAGE")) { if ((dataGridView1["dgvl_sdesc", i].Value.ToString()).ToUpper().Contains("ADULT")) { Double val_en = 0; try { val_en = Convert.ToDouble(dataGridView1["dgvl_price", i].Value.ToString()); } catch { } pck += val_en * ad; } if ((dataGridView1["dgvl_sdesc", i].Value.ToString()).ToUpper().Contains("KID")) { Double val_en = 0; try { val_en = Convert.ToDouble(dataGridView1["dgvl_price", i].Value.ToString()); } catch { } pck += val_en * kd; } } else { Double val_en = 0; try { val_en = Convert.ToDouble(dataGridView1["dgvl_price", i].Value.ToString()); } catch { } n_pck += val_en * all; } if ((dataGridView1["ifree", i].Value.ToString()).ToUpper() == "TRUE" && !dataGridView1["dgvl_code", i].Value.ToString().ToUpper().Contains("PCK")) { Double val_en = 0; try { val_en = Convert.ToDouble(dataGridView1["dgvl_price", i].Value.ToString()); } catch { } n_pck += (val_en * inf) * -1; }
                            Double val_en = 0, totl = 0;
                            try { val_en = Convert.ToDouble(dataGridView1["dgvl_price", i].Value.ToString()); }
                            catch { }
                            try { totl = Convert.ToDouble(dataGridView1["pax", i].Value.ToString()); }
                            catch { }
                            pck += totl * val_en;
                        }
                    }
                    catch { }
                }
                //lessdiscount = ((pck + n_pck) * lessdiscount);
                //try { lessdiscount += Convert.ToDouble(txt_discamt.Text.ToString()); }
                //catch { }
                txt_total_amt.Text = gm.toAccountingFormat(((pck + n_pck) - lessdiscount) + adtl);
            }
            catch (Exception)
            { }
        }

        private void get_aki()
        {
            int ad = 0, kid = 0, inf = 0;
            try
            {
                if (dgv_guestlist.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_guestlist.Rows.Count; i++)
                    {
                        ad = ((dgv_guestlist["g_typ", i].Value.ToString() == "A") ? ad + 1 : ad);
                        kid = ((dgv_guestlist["g_typ", i].Value.ToString() == "K") ? kid + 1 : kid);
                        inf = ((dgv_guestlist["g_typ", i].Value.ToString() == "I") ? inf + 1 : inf);
                    }
                }
            }
            catch { }
            label6.Text = ad.ToString();
            label7.Text = kid.ToString();
            label8.Text = inf.ToString();
            lbl_noofguest.Text = (ad + kid + inf).ToString();
        }
        private void newReservation_MouseClick(object sender, MouseEventArgs e)
        {
            gchk_rows();
        }

        private void newReservation_MouseMove(object sender, MouseEventArgs e)
        {
            gchk_rows();
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            gchk_rows();
        }

        private void lbl_noofguest_TextChanged(object sender, EventArgs e)
        {
            ent_kc(lbl_noofguest);
        }

        private void chk_active_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_active.Checked == true)
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btn_cancel.Enabled = false;
                active_res_where = (!String.IsNullOrEmpty(active_res_where)) ? ((active_res_where.Contains("(rf.arrived IS NULL OR rf.arrived !='Y')")) ? (active_res_where.Replace("(rf.arrived IS NULL OR rf.arrived !='Y')", "(rf.arrived IS NOT NULL OR rf.arrived ='Y')")) : active_res_where + " AND (rf.arrived IS NOT NULL OR rf.arrived ='Y')") : " WHERE (rf.arrived IS NOT NULL OR rf.arrived ='Y')";
            }
            else
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btn_cancel.Enabled = true;
                active_res_where = (!String.IsNullOrEmpty(active_res_where)) ? (active_res_where.Replace("(rf.arrived IS NOT NULL OR rf.arrived ='Y')", "(rf.arrived IS NULL OR rf.arrived !='Y')")) : " WHERE (rf.arrived IS NULL OR rf.arrived !='Y')";
            }
            set_reslist(active_res_where);
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        public void disp_cur()
        {
            if (forGfolio == true)
            {
                btn_additem.Visible = false;
                btn_additem.Enabled = false;

                btn_upditem.Text = "Update Entry";
            }
            else
            {
                btn_additem.Visible = true;
                btn_additem.Enabled = true;

                if (cur_load == true)
                {
                    btn_additem.Text = "Add New";
                    button2.Enabled = false;
                    panel8.Height = 0;
                    button1.Text = "Save";
                }
                else
                {
                    btn_additem.Text = "Walk-in";
                    btn_additem.Image = Hotel_System.Properties.Resources._1365167226_user_walk_copy;
                    button2.Enabled = true;
                    panel8.Height = 68;
                    button1.Text = "Save and Mark as Arrived";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                vor_get();
        }

        private void vor_get()
        {
            try
            {
                thisDatabase db = new thisDatabase();
                GlobalMethod gm = new GlobalMethod();
                String curdate = DateTime.Today.ToString("yyyy-MM-dd");
                String curtime = DateTime.Now.ToString("HH:mm");

                String reg_num = db.get_pk("reg_num");
            
                String res_code1 = ((forWalkIn == "") ? ((lbl_resno.Text.ToString() == "") ? dgv_reslist["res_code", dgv_reslist.CurrentRow.Index].Value.ToString() : lbl_resno.Text.ToString()) : forWalkIn);
                String full_name1 = ((forWalkIn == "") ? ((lbl_resno.Text.ToString() == "") ? dgv_reslist["name", dgv_reslist.CurrentRow.Index].Value.ToString() : db.QueryBySQLCodeRetStr("SELECT full_name FROM rssys.resfil WHERE res_code = '" + lbl_resno.Text.ToString() + "'")) : db.QueryBySQLCodeRetStr("SELECT full_name FROM rssys.resfil WHERE res_code = '" + res_code1 + "'"));
            
                //MessageBox.Show(res_code);
                //(dgv_reslist.SelectedRows.Count > 0 || dgv_reslist.SelectedRows.Count < 1) && cur_load == false
                if (res_code1 != "")
                {
                    if (MessageBox.Show("Continue on setting guest " + full_name1 + " as arrived?", "Confirmation dialog", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cur_walkIn = res_code1;
                        enterDeposit ed = new enterDeposit(this, res_code1);
                        ed.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Please select 1 record");
                }

            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        public Boolean afterPayment()
        {
            try
            {
                thisDatabase db = new thisDatabase();
                GlobalMethod gm = new GlobalMethod();
                String curdate = DateTime.Today.ToString("yyyy-MM-dd");
                String curtime = DateTime.Now.ToString("HH:mm");
                String reg_num = db.get_pk("reg_num");

                String res_code1 = cur_walkIn;
                String full_name1 = ((forWalkIn == "") ? ((lbl_resno.Text.ToString() == "") ? dgv_reslist["name", dgv_reslist.CurrentRow.Index].Value.ToString() : db.QueryBySQLCodeRetStr("SELECT full_name FROM rssys.resfil WHERE res_code = '" + lbl_resno.Text.ToString() + "'")) : db.QueryBySQLCodeRetStr("SELECT full_name FROM rssys.resfil WHERE res_code = '" + res_code1 + "'"));

                String col1 = "reg_num, res_code, acct_no, full_name, arr_date, arr_time, dep_date, hotel_code, rom_code, mkt_code, remarks, user_id, t_date, t_time, occ_type, chg_code, price, disc_code, discount, p_typ, trv_code, reg_date, seller";
                String col2 = "'" + reg_num + "' AS reg_num, res_code, acct_no, full_name, arr_date, arr_time, dep_date, hotel_code, rom_code, mkt_code, remarks, user_id, t_date, t_time, occ_type, chg_code, price, disc_code, discount, p_typ, trv_code, '" + curdate + "', seller";
                String cond = "res_code = '" + res_code1 + "'";
                try
                {
                    if (db.InsertSelect("gfolio", col1, "resfil", col2, cond))
                    {
                        if (db.UpdateOnTable("resfil", "arrived ='Y'", "res_code = '" + res_code1 + "'"))
                        {
                            db.set_pkm99("reg_num", db.get_nextincrementlimitchar(reg_num, 8));
                            set_reslist(active_res_where); btn_back.PerformClick();
                            MessageBox.Show("Successfully updated guest " + full_name1 + " to arrived.");
                            return true;
                            //ed.load_items(res_code);
                            //db.DeleteOnTable("resfil", cond);
                        }
                        else
                        {
                            //MessageBox.Show("Error on updating status");
                            return false;
                        }
                    }
                    else 
                    { 
                        //MessageBox.Show("Error on updating"); 
                        return false;
                    }
                }
                catch (Exception er)
                {
                    return false;
                    //MessageBox.Show(er.Message); 
                }
            }
            catch 
            {
                return false;
            }
        }
        private void label6_TextChanged(object sender, EventArgs e)
        {
            //get_tlpc();
            //get_tl();
            ent_kc(label6);
        }

        private void label7_TextChanged(object sender, EventArgs e)
        {
            //get_tlpc();
            //get_tl();
            ent_kc(label7);
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            //get_tlpc();
            //get_tl();
            ent_kc(label8);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gchk_rows();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            gchk_rows();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            gchk_rows();
        }

        private void cbo_agency_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Text = "";
            GlobalMethod gm = new GlobalMethod();

            gm.load_seller(comboBox5, "trv_code = '" + (cbo_agency.SelectedValue ?? "").ToString() + "'");
        }

        private void dgv_reslist_MouseClick(object sender, MouseEventArgs e)
        {
            clr_field();
            btn_cancel.Enabled = true;
            btn_save.Enabled = true;
            btn_selectcust.Enabled = true;
            grp_roominfo.Enabled = true;
            grp_guest.Enabled = true;
            grpbx_billing.Enabled = true;
        }

        private void get_tlpc()
        {
            int ad = 0, kd = 0, inf = 0;

            try
            {
                ad = Convert.ToInt32(label6.Text);
            }
            catch { }
            try
            {
                kd = Convert.ToInt32(label7.Text);
            }
            catch { }
            try
            {
                inf = Convert.ToInt32(label8.Text);
            }
            catch { }

            int tl = ad + kd + inf;
            lbl_noofguest.Text = tl.ToString();
        }

        private void ent_kc(TextBox txtbox)
        {
            int newTxt = 0;
            String newStr = txtbox.Text.ToString().Replace(" ", "");
            try
            {
                newTxt = Convert.ToInt32(newStr);
                gchk_rows();
            }
            catch
            {
                newTxt = 0;
                //MessageBox.Show("Please input a numeric value.");
                txtbox.Focus();
            }
            txtbox.Text = ((newTxt == 0) ? "" : newTxt.ToString());
        }
        private String ent_kc_st(String txtbox)
        {
            int newTxt = 0;
            String newStr = txtbox.ToString().Replace(" ", ""), newestString = "";
            try
            {
                newTxt = Convert.ToInt32(newStr);
            }
            catch
            {
                newTxt = 0;

            }
            newestString = ((newTxt == 0) ? "0" : newTxt.ToString());
            return newestString;
        }

        private void gchk_rows()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToBoolean(dataGridView1["dgvl_chk", i].Value.ToString()) == true)
                    {
                        Double val_en = 0;

                        if ((dataGridView1["dgvl_sdesc", i].Value.ToString()).ToUpper().Contains("PACKAGE"))
                        {
                            if ((dataGridView1["dgvl_sdesc", i].Value.ToString()).ToUpper().Contains("ADULT"))
                            {
                                try
                                {
                                    val_en = Convert.ToDouble(label6.Text.ToString());
                                }
                                catch { }
                            }
                            if ((dataGridView1["dgvl_sdesc", i].Value.ToString()).ToUpper().Contains("KID"))
                            {
                                try
                                {
                                    val_en = Convert.ToDouble(label7.Text.ToString());
                                }
                                catch { }
                            }
                            else
                            {
                                try
                                {
                                    val_en = Convert.ToDouble(label8.Text.ToString());
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            try
                            {
                                val_en = Convert.ToDouble(lbl_noofguest.Text.ToString());
                            }
                            catch { }
                        }
                        dataGridView1["pax", i].ReadOnly = false;
                        dataGridView1["pax", i].Value = ent_kc_st(((dataGridView1["pax", i].Value.ToString() == "") ? val_en.ToString() : dataGridView1["pax", i].Value.ToString()));
                    }
                    else
                    {
                        dataGridView1["pax", i].ReadOnly = true;
                        dataGridView1["pax", i].Value = ent_kc_st("0");
                    }
                }
                catch {  }
            }
            get_tl();
        }

        private void btn_print_Click_1(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Month.");
                comboBox6.DroppedDown = true;
            }
            else
            {

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                active_res_where = (!String.IsNullOrEmpty(active_res_where)) ? ((active_res_where.Contains("1=1")) ? (active_res_where.Replace("1=1", "arr_date = current_date")) : (active_res_where + " AND arr_date = current_date")) : " WHERE arr_date = current_date";
            }
            else
            {
                active_res_where = (!String.IsNullOrEmpty(active_res_where)) ? (active_res_where.Replace("arr_date = current_date", "1=1")) : " WHERE 1=1";
            }
            //MessageBox.Show(active_res_where);
            set_reslist(active_res_where);
        }
    }
}
