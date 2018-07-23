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
        public String active_res_where;

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

        private void disp_list()
        {
            try
            {
                thisDatabase db = new thisDatabase();

                dgv_reslist.DataSource = db.QueryBySQLCode("SELECT r.res_code AS \"Code\", r.full_name AS \"Name\", r.rmrttyp AS \"Rm Rate Type\", r.rom_code AS \"Room\", r.typ_code AS \"Type Code\", r.arr_date AS \"Arrival\", r.dep_date AS \"Departure\", c.comp_name AS \"Company\", r.user_id AS \"User ID\", r.t_date AS \"Trnx Date\", r.t_time AS \"Tnx Time\" FROM rssys.resfil r INNER JOIN rssys.guest g ON r.acct_no=g.acct_no LEFT JOIN rssys.company c ON c.comp_code=g.comp_code WHERE (r.cancel IS NULL OR cancel != 'Y')  ORDER BY r.res_code ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void disp_info()
        {

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

                    clr_field();
                    btn_cancel.Enabled = true;
                    btn_save.Enabled = true;
                    btn_selectcust.Enabled = true;
                    grp_roominfo.Enabled = true;
                    grp_guest.Enabled = true;
                    grpbx_billing.Enabled = true;

                    //get reservation info and pass res_code;
                    dt = db.get_res_info(dgv_reslist[0, row].Value.ToString());

                    foreach (DataRow r in dt.Rows)
                    {
                        res_code = r["res_code"].ToString();
                        rom_rate = r["rom_rate"].ToString();

                        dtp_resdt.Value = Convert.ToDateTime(r["res_date"].ToString());
                        dtp_arrtime.Value = Convert.ToDateTime(r["arr_time"].ToString());
                        dtp_deptime.Value = Convert.ToDateTime(r["dep_time"].ToString());

                        lbl_resno.Text = res_code;
                        lbl_arrdt.Text = Convert.ToDateTime(r["arr_date"].ToString()).ToString("MM/dd/yyyy");
                        lbl_depdt.Text = Convert.ToDateTime(r["dep_date"].ToString()).ToString("MM/dd/yyyy");
                        lbl_rm.Text = r["rom_code"].ToString();
                        lbl_rmtyp.Text = db.get_romtyp_desc(r["typ_code"].ToString());
                        lbl_clerk.Text = r["user_id"].ToString();
                        lbl_blockedby.Text = r["blockby"].ToString();

                        rom_rate_typ = r["rmrttyp"].ToString();

                        cbo_rtcode.SelectedValue = r["rate_code"].ToString();

                        occ_code = r["occ_type"].ToString();
                        cbo_mktsegment.SelectedValue = r["mkt_code"].ToString();
                        cbo_disc.SelectedValue = r["disc_code"].ToString();

                        rtb_remarks.Text = r["remarks"].ToString() + " " + r["rm_features"].ToString() + " " + r["bill_info"].ToString();
                        txt_resby.Text = r["reserv_by"].ToString();
                        txt_discamt.Text = r["free_bfast"].ToString();

                        if (r["blockresv"].ToString() == "Y")
                        {
                            chk_blockres.Checked = true;
                        }

                        guestno = r["acct_no"].ToString();
                    }

                    double val = (Convert.ToDateTime(lbl_depdt.Text) - Convert.ToDateTime(lbl_arrdt.Text)).TotalDays;

                    lbl_noofnight.Text = val.ToString();
                    lbl_noofnight_billing.Text = val.ToString();

                    if (val == 0)
                    {
                        lbl_noofnight.Text = "1";
                        lbl_noofnight_billing.Text = "1";
                    }

                    if (occ_code == "1")
                    {
                        cbo_occtyp.Text = "Single";
                    }
                    else if (occ_code == "2")
                    {
                        cbo_occtyp.Text = "Double";
                    }
                    else if (occ_code == "3")
                    {
                        cbo_occtyp.Text = "Triple";
                    }
                    else if (occ_code == "4")
                    {
                        cbo_occtyp.Text = "Quad";
                    }

                    dt_guest = db.get_guest_info(guestno);

                    dgv_guestlist.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());

                    dt_otherguestid = db.get_resguest_id(res_code);
                    //dt_otherguestid = db.QueryBySQLCode("SELECT * FROM rssys.gfguest WHERE reg_num='" + res_code + "'");
                    dt_guest.Rows.Clear();

                    foreach (DataRow rowid in dt_otherguestid.Rows)
                    {
                        dt_guest = db.get_guest_info(rowid["acct_no"].ToString());

                        dgv_guestlist.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());

                        dt_guest.Rows.Clear();

                        cnt++;
                    }

                    lbl_noofguest.Text = cnt.ToString();

                    disp_computed_bill();

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
                MessageBox.Show(er.Message);
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
                    int row = dgv_reslist.CurrentCell.RowIndex;

                    isnew = false;
                    //pnl_leftfirst.Hide();

                    clr_field();
                    btn_cancel.Enabled = true;
                    btn_save.Enabled = true;
                    btn_selectcust.Enabled = true;
                    grp_roominfo.Enabled = true;
                    grp_guest.Enabled = true;
                    grpbx_billing.Enabled = true;

                    //get reservation info and pass res_code;
                    dt = db.get_res_info(dgv_reslist[0, e.RowIndex].Value.ToString());

                    foreach (DataRow r in dt.Rows)
                    {
                        res_code = r["res_code"].ToString();
                        rom_rate = r["rom_rate"].ToString();

                        dtp_resdt.Value = Convert.ToDateTime(r["res_date"].ToString());
                        dtp_arrtime.Value = Convert.ToDateTime(r["arr_time"].ToString());
                        dtp_deptime.Value = Convert.ToDateTime(r["dep_time"].ToString());

                        lbl_resno.Text = res_code;
                        lbl_arrdt.Text = Convert.ToDateTime(r["arr_date"].ToString()).ToString("MM/dd/yyyy");
                        lbl_depdt.Text = Convert.ToDateTime(r["dep_date"].ToString()).ToString("MM/dd/yyyy");
                        lbl_rm.Text = r["rom_code"].ToString();
                        lbl_rmtyp.Text = db.get_romtyp_desc(r["typ_code"].ToString());
                        lbl_clerk.Text = r["user_id"].ToString();
                        lbl_blockedby.Text = r["blockby"].ToString();

                        rom_rate_typ = r["rmrttyp"].ToString();

                        cbo_rtcode.SelectedValue = r["rate_code"].ToString();

                        occ_code = r["occ_type"].ToString();
                        cbo_mktsegment.SelectedValue = r["mkt_code"].ToString();
                        cbo_disc.SelectedValue = r["disc_code"].ToString();

                        rtb_remarks.Text = r["remarks"].ToString() + " " + r["rm_features"].ToString() + " " + r["bill_info"].ToString();
                        txt_resby.Text = r["reserv_by"].ToString();
                        txt_discamt.Text = r["free_bfast"].ToString();

                        if (r["blockresv"].ToString() == "Y")
                        {
                            chk_blockres.Checked = true;
                        }

                        guestno = r["acct_no"].ToString();
                    }

                    double val = (Convert.ToDateTime(lbl_depdt.Text) - Convert.ToDateTime(lbl_arrdt.Text)).TotalDays;

                    lbl_noofnight.Text = val.ToString();
                    lbl_noofnight_billing.Text = val.ToString();

                    if (val == 0)
                    {
                        lbl_noofnight.Text = "1";
                        lbl_noofnight_billing.Text = "1";
                    }

                    if (occ_code == "1")
                    {
                        cbo_occtyp.Text = "Single";
                    }
                    else if (occ_code == "2")
                    {
                        cbo_occtyp.Text = "Double";
                    }
                    else if (occ_code == "3")
                    {
                        cbo_occtyp.Text = "Triple";
                    }
                    else if (occ_code == "4")
                    {
                        cbo_occtyp.Text = "Quad";
                    }

                    dt_guest = db.get_guest_info(guestno);

                    dgv_guestlist.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());

                    dt_otherguestid = db.get_resguest_id(res_code);

                    dt_guest.Rows.Clear();

                    foreach (DataRow rowid in dt_otherguestid.Rows)
                    {
                        dt_guest = db.get_guest_info(rowid["acct_no"].ToString());

                        dgv_guestlist.Rows.Add(dt_guest.Rows[0]["acct_no"].ToString(), dt_guest.Rows[0]["full_name"].ToString(), dt_guest.Rows[0]["gender"].ToString(), dt_guest.Rows[0]["address1"].ToString(), dt_guest.Rows[0]["tel_num"].ToString(), dt_guest.Rows[0]["email"].ToString(), dt_guest.Rows[0]["cntry_code"].ToString());

                        dt_guest.Rows.Clear();

                        cnt++;
                    }

                    lbl_noofguest.Text = cnt.ToString();

                    disp_computed_bill();

                    tbcntrl_res.SelectedTab = tpg_reg;

                    tpg_reg.Show();
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
        private void disp_computed_bill()
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
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
                    txt_discamt.Text = occ.ToString();
                }
                else
                {
                    txt_discamt.Text = "0";
                }

                lessdisc_amt = gm.toNormalDoubleFormat(txt_discamt.Text);

                grossamt = db.get_roomrateamt(cbo_rtcode.SelectedValue.ToString(), db.get_romtyp_code(lbl_rmtyp.Text), occ);
                txt_grossrt.Text = gm.toAccountingFormat(grossamt);

                txt_netrt.Text = gm.toAccountingFormat(db.get_netrate(grossamt, lessdiscount, lessdisc_amt));
                txt_govtrt.Text = gm.toAccountingFormat(db.get_tax(grossamt, lessdiscount, lessdisc_amt));

                //regular rate w/ or w/out discount
                if (issenior_disc == false)
                {
                    txt_rmrate.Text = gm.toAccountingFormat(grossamt - (grossamt * (lessdiscount / 100)) - lessdisc_amt);
                }
                //senior citizen discount no tax
                else if (issenior_disc == true)
                {
                    Double db_rmrate = Convert.ToDouble(txt_netrt.Text);

                    Decimal roundoff_amt = Math.Round(Convert.ToDecimal(db_rmrate), 1);

                    txt_rmrate.Text = gm.toAccountingFormat(Convert.ToDouble(roundoff_amt));
                    txt_govtrt.Text = "0.00";
                }

                txt_total_amt.Text = gm.toAccountingFormat(Convert.ToDouble(txt_rmrate.Text) * Convert.ToDouble(lbl_noofnight_billing.Text));
            }
            catch (Exception)
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String curdate = DateTime.Today.ToString("yyyy-MM-dd");
            String curtime = DateTime.Now.ToString("HH:mm");
            String res_code = db.get_pk("res_code");
            String dacct_no = "";
            String dfull_name = "";
            String rmtyp = db.get_romtyp_code(lbl_rmtyp.Text);
            String rmrttyp = "";
            int occ = 1;
            String isblock = "N";
            String disc = "";
            String disc_amt = "0.00";
            Boolean cont = true;

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

            if (chk_blockres.Checked)
            {
                isblock = "Y";
            }
            if (cbo_disc.SelectedIndex > -1)
            {
                disc = cbo_disc.SelectedValue.ToString();
                disc_amt = db.get_discount(cbo_disc.SelectedValue.ToString()).ToString("0.00");
            }

            if (cbo_rmrttyp.SelectedIndex == -1)
            {
                MessageBox.Show("Pls select Room Rate Type.");
            }
            else if (dgv_guestlist.Rows.Count <= 1)
            {
                MessageBox.Show("Pls select guest(s) at Guest list.");
            }
            else if (cbo_rtcode.SelectedIndex == -1)
            {
                MessageBox.Show("Pls select the type of rate code.");
            }
            else if (cbo_mktsegment.SelectedIndex == -1 && cbo_mktsegment.Text == "")
            {
                MessageBox.Show("Pls select the type of market segment.");
            }
            else if (lbl_rm.Text == "")
            {
                MessageBox.Show("Pls select the room the you are going to reserve.");
            }
            else if (db.is_roomreserved(lbl_rm.Text) && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
            {
                MessageBox.Show("Room already reserved.");
            }
            // Start Modify By: Roldan 04/20/18 check if date reserved  
            // updated:: Added convert on lbl_arrdt and lbl_depdt by: Reancy
            // removed by: Reancy 06 22 2018
            //else if (db.is_arr_roomreserved(lbl_rm.Text, Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd")) || db.is_dep_roomreserved(lbl_rm.Text, Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd")))
            else if (db.is_roomreservedBythisRescode(lbl_rm.Text, lbl_resno.Text, Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd")))
            {
                MessageBox.Show("Room already reserved.");
            }
            // End Modify By: Roldan 04/20/18 check if date reserved
            else if (cbo_agency.SelectedIndex == -1)
            {
                MessageBox.Show("Pls select travel agency.");
            }
            else
            {
                if (db.get_romstatus(lbl_rm.Text) == "OCC" && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
                {
                    if (MessageBox.Show("Room is currently occupied, are you sure you want to continue?", "Room is occupied", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        cont = false;
                    }
                }
                else if (db.get_romstatus(lbl_rm.Text) == "VD" && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
                {
                    if (MessageBox.Show("Room is currently vacant dirty, are you sure you want to continue?", "Room is occupied", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        cont = false;
                    }
                }
                else if (db.get_romstatus(lbl_rm.Text) == "OOO" && Convert.ToDateTime(lbl_arrdt.Text).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) == 0)
                {
                    if (MessageBox.Show("Room is currently out of order, are you sure you want to continue?", "Room is occupied", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        cont = false;
                    }
                }

                if (cont == true)
                {
                    try
                    {
                        dacct_no = dgv_guestlist.Rows[0].Cells[0].Value.ToString();
                        dfull_name = dgv_guestlist.Rows[0].Cells[1].Value.ToString();

                        if (cbo_rmrttyp.SelectedIndex > -1)
                        {
                            rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
                        }

                        if (isnew == true)
                        {
                            // MessageBox.Show("res_date:" + curdate + "\n" + "arr_date:" + dtp_arrtime.Value.ToString("yyyy-MM-dd") + "\n" + "dep_date:" + dtp_deptime.Value.ToString("yyyy-MM-dd"));
                            String col = "res_code, acct_no, full_name, rmrttyp, res_date, arr_date, arr_time, dep_date, dep_time, rom_code, rom_rate, typ_code, rate_code, mkt_code, src_code, trv_code, rm_features, bill_info, remarks, reserv_by,  reserv_thru, reserv_tel, user_id, t_date, t_time, occ_type, fctr_code, free_bfast, blockresv, blockby, disc_code, disc_pct";

                            // updated:: Added convert on lbl_arrdt and lbl_depdt by: Reancy
                            String val = "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "','" + rmrttyp + "','" + curdate + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_arrtime.Value.ToString("HH:mm") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', '" + dtp_deptime.Value.ToString("HH:mm") + "', '" + lbl_rm.Text + "', '" + gm.toNormalDoubleFormat(txt_rmrate.Text) + "', '" + rmtyp + "', '" + cbo_rtcode.SelectedValue.ToString() + "', '" + cbo_mktsegment.SelectedValue.ToString() + "', '', '" + cbo_agency.SelectedValue.ToString() + "','','','" + rtb_remarks.Text + "'";
                            val = val + ", '" + txt_resby.Text + "', '','" + txt_contact.Text + "','" + GlobalClass.username + "','" + db.get_systemdate("") + "','" + curtime + "','" + occ.ToString() + "','','" + txt_discamt.Text + "','" + isblock + "','" + lbl_blockedby.Text + "','" + disc + "','" + disc_amt + "'";
                            //MessageBox.Show(res_code);
                            if (db.InsertOnTable("resfil", col, val))
                            {
                                //insert other guests
                                for (int r = 1; dgv_guestlist.Rows.Count - 1 > r; r++)
                                {
                                    dacct_no = dgv_guestlist.Rows[r].Cells[0].Value.ToString();
                                    dfull_name = dgv_guestlist.Rows[r].Cells[1].Value.ToString();

                                    db.InsertOnTable("resguest", "res_code, acct_no, full_name, arr_date, dep_date", "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "'");
                                }
                                //increment rescode to m99
                                db.set_pkm99("res_code", db.get_nextincrementlimitchar(res_code, 8));

                                MessageBox.Show("New Record(s) successfully added.");
                                tbcntrl_res.SelectedTab = tpg_list;
                                tpg_list.Show();
                                //pnl_leftfirst.Show();
                                //clr_field();
                            }
                            else
                            {
                                MessageBox.Show("Problem on Adding New Record.");
                            }
                        }
                        else
                        {
                            // updated:: Added convert on lbl_arrdt and lbl_depdt by: Reancy
                            res_code = lbl_resno.Text;
                            if (db.UpdateOnTable("resfil", "acct_no='" + dacct_no + "', rom_code='" + lbl_rm.Text + "', full_name='" + dfull_name + "', res_date='" + curdate + "', arr_date='" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', arr_time='" + dtp_arrtime.Value.ToString("HH:mm") + "', dep_date='" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "', dep_time='" + dtp_deptime.Value.ToString("HH:mm") + "', typ_code='" + rmtyp + "', rate_code='" + cbo_rtcode.SelectedValue.ToString() + "', mkt_code='" + cbo_mktsegment.SelectedValue.ToString() + "', src_code='', trv_code='000001', rm_features='', bill_info='', remarks='" + rtb_remarks.Text + "', reserv_by='" + txt_resby.Text + "',  reserv_thru='', reserv_tel='" + txt_contact.Text + "', user_id='" + GlobalClass.username + "', t_date='" + db.get_systemdate("") + "', t_time='" + curtime + "', occ_type='" + occ.ToString() + "', fctr_code='', free_bfast='" + txt_discamt.Text + "', blockresv='" + isblock + "', blockby='" + lbl_blockedby.Text + "', disc_code='" + disc + "', disc_pct='" + disc_amt + "'", "res_code='" + res_code + "'"))
                            {
                                db.DeleteOnTable("resguest", "res_code='" + res_code + "'");

                                //insert other guests
                                for (int r = 1; dgv_guestlist.Rows.Count - 1 > r; r++)
                                {
                                    dacct_no = dgv_guestlist.Rows[r].Cells[0].Value.ToString();
                                    dfull_name = dgv_guestlist.Rows[r].Cells[1].Value.ToString();

                                    db.InsertOnTable("resguest", "res_code, acct_no, full_name, arr_date, dep_date", "'" + res_code + "', '" + dacct_no + "', '" + dfull_name + "', '" + Convert.ToDateTime(lbl_arrdt.Text).ToString("yyyy-MM-dd") + "', '" + Convert.ToDateTime(lbl_depdt.Text).ToString("yyyy-MM-dd") + "'");
                                }

                                MessageBox.Show("Record updated successfully.");
                                tbcntrl_res.SelectedTab = tpg_list;
                                tpg_list.Show();
                                tbcntrl_option.SelectedTab = tpg_opt_1;

                                //pnl_leftfirst.Show();
                                clr_field();
                            }
                        }
                        set_reslist(active_res_where);
                        //disp_list(); 
                        //tbcntrl_res.SelectedTab = tpg_reg;
                        tbcntrl_option.SelectedTab = tpg_opt_1;
                        tpg_opt_1.Show();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Transaction cannot be saved.");
                }
            }

        }
        public void clr_field()
        {
            lbl_resno.Text = "";
            lbl_arrdt.Text = "";
            lbl_noofnight.Text = "0";
            lbl_rm.Text = "";
            lbl_rmtyp.Text = "";
            lbl_noofnight_billing.Text = "0";
            lbl_noofguest.Text = "0";
            lbl_depdt.Text = "";
            lbl_blockedby.Text = "";

            chk_blockres.Checked = false;

            cbo_srchcomp.SelectedIndex = -1;
            cbo_disc.SelectedIndex = -1;
            cbo_mktsegment.SelectedIndex = -1;
            cbo_rtcode.SelectedIndex = -1;
            cbo_occtyp.SelectedIndex = -1;
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
            btn_selectcust.Enabled = false;

            grp_roominfo.Enabled = false;
            grp_guest.Enabled = false;
            grpbx_billing.Enabled = false;
        }

        private void newReservation_Load(object sender, EventArgs e)
        {
            frm_load();
        }

        private void frm_load()
        {
            this.WindowState = FormWindowState.Maximized;
            Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            Hotel_System.GlobalMethod gm = new Hotel_System.GlobalMethod();
            //active_res_where = " AND r.arr_date >= '" + db.get_systemdate("") + "' AND (r.arrived IS NULL OR r.arrived !='Y') AND (r.cancel IS NULL OR r.cancel !='Y')";
            active_res_where = " AND r.arr_date >= '" + db.get_systemdate("") + "' AND (r.arrived IS NULL OR r.arrived !='Y') AND (r.cancel IS NULL OR r.cancel !='Y')";
            set_reslist(active_res_where);

            //set_compcbo();
            //set_rtcbo();
            //set_mscbo();
            //set_disccbo();
            //set_rmtypecbo();

            gm.load_company(cbo_srchcomp);
            gm.load_ratetype(cbo_rtcode);
            gm.load_market(cbo_mktsegment);
            //set_rmtypecbo();
            gm.load_disctbl(cbo_disc);
            gm.load_romratetype(cbo_rmrttyp);
            gm.load_agency(cbo_agency);
            //set_roomstatuslist();

            //cbo_type.SelectedIndex = -1;
            clr_field();
            lbl_clerk.Text = GlobalClass.username;
            //disp_res();
        }

        private void cbo_rtcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String rmrttyp;
            Double night = 0, mo = 0, wk = 0, val = 0;
            String room_rate_code = "";
            try
            {
                if (cbo_rtcode.SelectedIndex > -1)
                {
                    DataTable dt = db.QueryBySQLCode("SELECT rmrttyp from rssys.ratetype WHERE rate_code='" + cbo_rtcode.SelectedValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {

                        room_rate_code = dt.Rows[0]["rmrttyp"].ToString();
                    }
                    cbo_rmrttyp.SelectedValue = db.get_romratetype_code(room_rate_code);
                    rmrttyp = cbo_rmrttyp.SelectedValue.ToString();
                    if (night % 30 > 0) { mo = mo + 1.00; }
                    if (rmrttyp == "M")
                    {

                        lbl_noofnight_title_top.Text = "No. of Month(s)";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        night = Convert.ToDouble(lbl_noofnight.Text);

                        if (night > 30)
                        {
                            mo = night / 30;


                        }

                        else
                        {
                            mo = 1.00;
                        }

                        lbl_noofnight.Text = mo.ToString("0");
                        lbl_noofnight_billing.Text = lbl_noofnight.Text;
                    }
                    else if (rmrttyp == "W")
                    {
                        lbl_noofnight_title_top.Text = "No. of Week(s)";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        night = Convert.ToDouble(lbl_noofnight.Text);
                        if (night % 7 > 0) { wk = wk + 1.00; }
                        if (night > 7)
                        {
                            wk = night / 7;


                        }

                        else
                        {
                            wk = 1.00;
                        }

                        lbl_noofnight.Text = wk.ToString("0");
                        lbl_noofnight_billing.Text = lbl_noofnight.Text;
                    }
                    else
                    {
                        val = (Convert.ToDateTime(lbl_depdt.Text) - Convert.ToDateTime(lbl_arrdt.Text)).TotalDays;

                        lbl_noofnight_title_top.Text = "No. of Nights";
                        lbl_noofnight_title.Text = lbl_noofnight_title_top.Text;

                        lbl_noofnight.Text = val.ToString("0");
                        lbl_noofnight_billing.Text = lbl_noofnight.Text;
                    }
                }
                else
                {
                    cbo_rmrttyp.SelectedIndex = -1;
                }
            }
            catch (Exception) { }

            disp_computed_bill();
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
            z_Research frm = new z_Research(this);
            frm.ShowDialog();

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
            catch { }
        }
        public void reload_guest()
        {
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
                    dgv_guestlist.Rows.Add(row);
                }
            }

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

                AG.MdiParent = this.MdiParent;
                AG.reload_guest();
                AG.Show();

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

        private void dgv_reslist_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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

            disp_computed_bill();
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
            if (cbo_srchcomp.SelectedIndex != -1)
            {
                WHERE = WHERE + " AND g.comp_code='" + cbo_srchcomp.SelectedValue.ToString() + "'";
            }
            if (chk_active.Checked)
            {
                WHERE = WHERE + " AND r.arr_date <= '" + db.get_systemdate("") + "' AND (r.arrived IS NULL OR r.arrived !='Y') AND (r.cancel IS NULL OR r.cancel !='Y')";
            }

            set_reslist(WHERE);
        }
        private void btn_srchreservation_Click_2(object sender, EventArgs e)
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
            if (cbo_srchcomp.SelectedIndex != -1)
            {
                WHERE = WHERE + " AND g.comp_code='" + cbo_srchcomp.SelectedValue.ToString() + "'";
            }
            if (chk_active.Checked)
            {
                WHERE = WHERE + " AND r.arr_date >= '" + db.get_systemdate("") + "' AND (r.arrived IS NULL OR r.arrived !='Y') AND (r.cancel IS NULL OR r.cancel !='Y')";
            }

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

    }
}
