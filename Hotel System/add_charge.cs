using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Hotel_System
{
    public partial class add_charge : Form
    {
        //private rGuestBilling rGuestBill;
        GlobalMethod gm = new GlobalMethod();
        private newGuestBilling rGuestBill;
        private Boolean isnew = true;
        bool blHasDot = false;
        String chgtype = "";
        String currency_code = "PHP"; //default currency
        String rg_code = "";
        String c_num = "";
        public Boolean gisnew = false;
        String c_ref = "";

        //public add_charge(rGuestBilling rgbilling)
        //{
        //    InitializeComponent();

        //    rGuestBill = rgbilling;
        //}
        public add_charge(newGuestBilling rgbilling)
        {
            rGuestBill = rgbilling;

            InitializeComponent();
            gm.load_charge_paymentsonly(comboBox1);
            load_chargecbo();


            an_frm_load("", "", "");
        }

        private void add_charge_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            //load_chargecbo();
            //cbo_chg.SelectedIndex = -1;

            txt_chg_num.Hide();

            dtp_tdate.Value = Convert.ToDateTime(db.get_systemdate(""));
            // DateTime.Parse(rGuestBill.lbl_arrdate.Text);
            //
            //lbl_or_amt.Hide();
            //txt_or_amnt.Hide();

            pnl_side.BackColor = Color.DarkKhaki;
        }

        private void load_chargecbo()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryBySQLCode("SELECT DISTINCT chg_type, (CASE WHEN chg_type = 'P' THEN 'PAYMENT' ELSE 'CHARGE' END) AS chg_desc FROM rssys.charge");

                cbo_chg.DataSource = dt;
                cbo_chg.DisplayMember = "chg_desc";
                cbo_chg.ValueMember = "chg_type";
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void load_reslist(String search_code)
        {
            try
            {
                thisDatabase db = new thisDatabase();

                dgv_reslist.DataSource = db.get_reservationlistforBilling(search_code);
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }    

        public void set_data(String folnum, String guest, String rom_code, String rom_rate, String romtype, String remark, String arrdate, String depdate, String trnxdt, String chg, String chg_num, String doctyp, String refrnc, String amt, String resnum, String res_gname, String deposit, String balance, Boolean isthisnew)
        {
            isnew = isthisnew;
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();

            lbl_balance.Text = balance;
            lbl_deposit.Text = deposit;
             
            //lbl_romrate.Text = rom_rate;
            //lbl_rmtype.Text = romtype;
            //rtxt_remark.Text = remark;
            //lbl_arrdate.Text = arrdate;
            //lbl_depdate.Text = depdate;            

            if (isnew == false)
            {
                // added by: Reancy 05162018
                try
                {
                    DateTime dtp_dt = Convert.ToDateTime(trnxdt);
                    dtp_tdate.Value = dtp_dt;
                }
                catch (Exception er) {
                    MessageBox.Show("Incorrect format for date. Set the date manually.");
                }
                amt = gm.get_amount(amt).ToString("0.00");

                if (doctyp == "CC")
                {
                    cbo_doctyp.SelectedIndex = 0;
                }
                else if (doctyp == "NA")
                {
                    cbo_doctyp.SelectedIndex = 1;
                }

                if (db.get_roomchargetype(chg) == "P")
                {
                    amt = (Convert.ToDouble(amt) * -1).ToString("0.00");
                    cbo_doctyp.Enabled = false;
                }
                cbo_chg.SelectedValue = chg;
                cbo_chg.Enabled = false;
            }
            else
            {
                cbo_chg.Enabled = true;
                cbo_chg.SelectedIndex = -1;
                //dtp_tdate.Enabled = false;
                //lbl_gfolionum.Text = folnum;
                //lbl_guestname.Text = guest;
                //lbl_rom_code.Text = rom_code;
            }

            lbl_gfolionum.Text = folnum;
            lbl_guestname.Text = guest;
            txt_ref.Text = refrnc;
            txt_amt.Text = amt;
            lbl_rom_code.Text = rom_code;
            txt_chg_num.Text = chg_num;
            //cbo_resnumber.SelectedValue = resnum;
            //txt_guest.Text = res_gname;
            
            //dtp_tdate.Enabled = false;

            if (db.isReservationFolio(lbl_gfolionum.Text) == true)
            {
                grpbx_res.Enabled = true;
                load_reslist("");
            }
            else
            {
                grpbx_res.Enabled = false;
            }
        }

        private void dgv_reslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_reslist.Rows.Count > 0)
            {
                lbl_resnum.Text = dgv_reslist[0, e.RowIndex].Value.ToString();
                lbl_resgname.Text = dgv_reslist[1, e.RowIndex].Value.ToString();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            load_reslist(txt_search.Text);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();

            Double pck = 0.00;
            Double n_pck = 0.00;
            Double adtl = 0.00;

            Double ad = 0.00, kd = 0.00, inf = 0.00, all = 0.00;
            try
            {
                ad = Convert.ToInt32(textBox2.Text.ToString());
                kd = Convert.ToInt32(textBox3.Text.ToString());
                inf = Convert.ToInt32(textBox4.Text.ToString());
                all = Convert.ToInt32(textBox1.Text.ToString());
            }
            catch
            {
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox4.Text = "0";
                textBox1.Text = "0";
            }

            if (cbo_chg.SelectedValue.ToString() == "C")
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        Boolean bol_ = false;
                        try
                        {
                            bol_ = Convert.ToBoolean(dataGridView1["bool_check", i].Value.ToString());
                        }
                        catch { }

                        if (bol_)
                        {
                            try
                            {
                                if (Convert.ToBoolean(dataGridView1["bool_check", i].Value.ToString()) == true)
                                {
                                    if ((dataGridView1["chg_code", i].Value.ToString()).ToUpper().Contains("PCK"))
                                    {
                                        if ((dataGridView1["chg_desc", i].Value.ToString()).ToUpper().Contains("ADULT"))
                                        {
                                            Double val_en = 0;
                                            Double add_this = 0;
                                            try { ad = Convert.ToDouble(dataGridView1["pax", i].Value.ToString()); }
                                            catch { }
                                            c_ref = ad + " " + dataGridView1["chg_desc", i].Value.ToString() + "(" + lbl_resgname.Text.ToString() + ")";
                                            try { val_en = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); }
                                            catch { }

                                            if (dataGridView1["ifree", i].Value.ToString().ToUpper() == "TRUE" && textBox4.Text.ToString() != "0" && (Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) == Convert.ToDouble(textBox4.Text.ToString())))
                                            {
                                                try { add_this = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); }
                                                catch { }
                                                c_ref = (ad - inf) + " " + dataGridView1["chg_desc", i].Value.ToString() + "(" + lbl_resgname.Text.ToString() + ")";
                                                add_this = (add_this * inf) * -1;
                                            }

                                            val_en = (val_en * ad) + add_this;

                                            vald_save(val_en, dataGridView1["chg_code", i].Value.ToString());
                                        }
                                        if ((dataGridView1["chg_desc", i].Value.ToString()).ToUpper().Contains("KID"))
                                        {
                                            Double val_en = 0;
                                            Double add_this = 0;
                                            try { kd = Convert.ToDouble(dataGridView1["pax", i].Value.ToString()); }
                                            catch { }
                                            c_ref = kd + " " + dataGridView1["chg_desc", i].Value.ToString() + "(" + lbl_resgname.Text.ToString() + ")";
                                            try { val_en = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); }
                                            catch { }

                                            if (dataGridView1["ifree", i].Value.ToString().ToUpper() == "TRUE" && textBox4.Text.ToString() != "0" && (Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) == Convert.ToDouble(textBox4.Text.ToString())))
                                            {
                                                try { add_this = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); }
                                                catch { }

                                                c_ref = (kd - inf) + " " + dataGridView1["chg_desc", i].Value.ToString() + "(" + lbl_resgname.Text.ToString() + ")";
                                                add_this = (add_this * inf) * -1;
                                            }

                                            val_en = (val_en * kd) + add_this;
                                            vald_save(val_en, dataGridView1["chg_code", i].Value.ToString());
                                        }
                                    }
                                    else
                                    {
                                        Double val_en = 0;
                                        Double add_this = 0.00;
                                        c_ref = textBox1.Text.ToString() + " " + dataGridView1["chg_desc", i].Value.ToString() + "(" + lbl_resgname.Text.ToString() + ")";
                                        try { val_en = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); }
                                        catch { }

                                        if (dataGridView1["ifree", i].Value.ToString().ToUpper() == "TRUE" && textBox4.Text.ToString() != "0" && (Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) == Convert.ToDouble(textBox4.Text.ToString())))
                                        {
                                            try { add_this = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); }
                                            catch { }

                                            c_ref = (all - inf) + " " + dataGridView1["chg_desc", i].Value.ToString() + "(" + lbl_resgname.Text.ToString() + ")";
                                            add_this = (add_this * inf) * -1;
                                        }

                                        val_en = (val_en * all) + add_this;

                                        vald_save(val_en, dataGridView1["chg_code", i].Value.ToString());
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            else
            {
                Double val_en = 0;
                try { val_en = Convert.ToDouble(txt_amt.Text.ToString()); }
                catch { }
                val_en = val_en * -1;
                vald_save(val_en, (comboBox1.SelectedValue ?? "").ToString());
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '-')
            {
                // Allow Digits and BackSpace char
                if (e.KeyChar == '\b')
                {
                    if (txt_amt.Text.Contains(".") == false)
                    {
                        blHasDot = false;
                    }
                }
            }
            else if (e.KeyChar == '.' && !blHasDot)
            {
                //Allows only one Dot Char
                blHasDot = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cbo_chg_SelectedIndexChanged(object sender, EventArgs e)
        {
            on_chg();
        }

        private void on_chg()
        {
            thisDatabase db = new thisDatabase();

            if (cbo_chg.SelectedIndex > -1)
            {
                if (cbo_chg.SelectedValue.ToString() == "P")
                {
                    dataGridView1.Enabled = false;
                    comboBox1.Enabled = true;
                    comboBox1.DroppedDown = true;
                    groupBox2.Enabled = false;
                    textBox1.Enabled = false;
                    txt_amt.Enabled = true;
                    txt_ref.Enabled = true;

                    txt_amt.Text = "0.00";
                }
                else
                {
                    dataGridView1.Enabled = true;
                    comboBox1.Enabled = false;
                    //comboBox1.SelectedIndex = -1;
                    groupBox2.Enabled = true;
                    textBox1.Enabled = true;
                    txt_amt.Enabled = false;
                    txt_ref.Enabled = false;

                    txt_amt.Text = "0.00";
                }
            }
        }
        private void cbo_chg_TextChanged(object sender, EventArgs e)
        {
            //thisDatabase db = new thisDatabase();

            //if (cbo_chg.SelectedIndex != -1)
            //{
            //    String chg_code = cbo_chg.SelectedValue.ToString();

            //    chgtype = db.get_chg_type(chg_code);
            //    //MessageBox.Show(chg_code);
            //    if (chgtype == "")
            //    {
            //        btn_save.Enabled = false;
            //    }
            //    else if (chgtype == "P")
            //    {
            //        cbo_doctyp.Enabled = true;
            //        btn_save.Enabled = true;
            //        txt_amt.Enabled = true;
            //    }
            //    else if (chgtype == "C")
            //    {
            //        if (db.is_roomcharge(chg_code) || db.is_roomcharge_senior(chg_code))
            //        {
            //            txt_ref.Text = "ROOM CHARGE";
            //            txt_amt.Text = rGuestBill.rom_rate;
            //            //txt_amt.Enabled = false;
            //        }
            //        cbo_doctyp.Enabled = false;
            //        btn_save.Enabled = true;
            //    }
            //}
            //if (chgtype == "")
            //{
            //    btn_save.Enabled = false;
            //}
        }

        private void cbo_doctyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            lbl_reference.Text = "Reference";

            if (cbo_doctyp.Text.ToString() == "Cash Credit")
            {
                String ccno = db.get_pk("next_cc");
                txt_ref.Text = ccno;
                
                txt_ref.ReadOnly = true;

                lbl_or_amt.Hide();

                txt_or_amnt.Hide();
            }
            else if (cbo_doctyp.Text.ToString() == "Official Receipt")
            {
                String or_amnt = "0.00";
                txt_ref.Text = "";

                txt_ref.ReadOnly = false;

                lbl_or_amt.Show();

                txt_or_amnt.Show();

                lbl_reference.Text = "OR Reference Number";
            }
            else
            {
                txt_ref.Text = "";

                txt_ref.ReadOnly = false;

                lbl_or_amt.Hide();

                txt_or_amnt.Hide();
            }
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

        public void chg_add(String rg, String cc, String cn)
        {
            thisDatabase dbs = new thisDatabase();
            DataTable dt_gs = dbs.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE reg_num = '" + rg + "' LIMIT 1");
            DataTable dt_edt = dt_edt = dbs.QueryBySQLCode("SELECT * FROM rssys.chgfil WHERE reg_num = '" + rg + "' AND chg_code = '" + cc + "' AND chg_num = '" + cn + "'");
            //an_frm_load(rg, cc, cn);

            rg_code = rg;
            c_num = cn;

            if (gisnew)
            {

            }
            else
            {
                cbo_chg.Enabled = false;
                if (dt_edt.Rows.Count > 0)
                {
                    Double amount = 0.00;
                    try
                    {
                        amount = Convert.ToDouble(dt_edt.Rows[0]["amount"].ToString());
                    }
                    catch { }
                    if (amount < 0)
                    {   
                        cbo_chg.SelectedValue = "P";
                        comboBox1.SelectedValue = dt_edt.Rows[0]["chg_code"].ToString();
                        txt_ref.Text = dt_edt.Rows[0]["reference"].ToString();
                        txt_amt.Text = (amount * -1).ToString();
                    }
                    else
                    {
                        cbo_chg.SelectedValue = "C";
                        if(dataGridView1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (dataGridView1["chg_code", i].Value.ToString() == dt_edt.Rows[0]["chg_code"].ToString())
                                {
                                    String p_ds = dbs.QueryBySQLCodeRetStr("SELECT chg_desc FROM rssys.charge WHERE chg_code = '" + dt_edt.Rows[0]["chg_code"].ToString() + "'");
                                    string[] spt = new string[] { " " };
                                    dataGridView1["chg_code", i].Value = true;

                                    if (dt_edt.Rows[0]["chg_code"].ToString().ToUpper().Contains("PCK"))
                                    {
                                        if ((p_ds).ToString().ToUpper().Contains("ADULT"))
                                        {
                                            textBox2.Text = (dt_edt.Rows[0]["reference"].ToString()).Split(spt, StringSplitOptions.None)[0];
                                        }
                                        if ((p_ds).ToString().ToUpper().Contains("KID"))
                                        {
                                            textBox3.Text = (dt_edt.Rows[0]["reference"].ToString()).Split(spt, StringSplitOptions.None)[0];
                                        }
                                    }
                                    else
                                    {
                                        textBox1.Text = (dt_edt.Rows[0]["reference"].ToString()).Split(spt, StringSplitOptions.None)[0];
                                    }
                                }
                            }
                        }
                        else
                        {
                            chg_add(rg, cc, cn);
                        }
                        gchk_rows();
                    }
                }
            }
            if (dt_gs.Rows.Count > 0)
            {
                lbl_resnum.Text = dt_gs.Rows[0]["res_code"].ToString();
                lbl_resgname.Text = dt_gs.Rows[0]["full_name"].ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gchk_rows();
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
                Double ad = 0.00, kd = 0.00, inf = 0.00, all = 0.00;
                try
                {
                    ad = Convert.ToInt32(textBox2.Text.ToString());
                    kd = Convert.ToInt32(textBox3.Text.ToString());
                    inf = Convert.ToInt32(textBox4.Text.ToString());
                    all = Convert.ToInt32(textBox1.Text.ToString());
                }
                catch
                {
                    textBox2.Text = "0";
                    textBox3.Text = "0";
                    textBox4.Text = "0";
                    textBox1.Text = "0";
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToBoolean(dataGridView1["bool_check", i].Value.ToString()) == true)
                        {
                            //if ((dataGridView1["chg_desc", i].Value.ToString()).ToUpper().Contains("PACKAGE")) { if ((dataGridView1["chg_desc", i].Value.ToString()).ToUpper().Contains("ADULT")) { Double val_en = 0; try { val_en = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); } catch { } pck += val_en * ad; } if ((dataGridView1["chg_desc", i].Value.ToString()).ToUpper().Contains("KID")) { Double val_en = 0; try { val_en = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); } catch { } pck += val_en * kd; } } else { Double val_en = 0; try { val_en = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); } catch { } n_pck += val_en * all; } if ((dataGridView1["ifree", i].Value.ToString()).ToUpper() == "TRUE" && (dataGridView1["chg_code", i].Value.ToString().ToUpper().Contains("PCK")) == false) { Double val_en = 0; try { val_en = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); } catch { } n_pck += (val_en * inf) * -1; }
                            Double val_en = 0, totl = 0;
                            try { val_en = Convert.ToDouble(dataGridView1["price", i].Value.ToString()); }
                            catch { }
                            try { totl = Convert.ToDouble(dataGridView1["pax", i].Value.ToString()); }
                            catch { }
                            if (dataGridView1["ifree", i].Value.ToString().ToUpper() == "TRUE" && textBox4.Text.ToString() != "0" && (Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) == Convert.ToDouble(textBox4.Text.ToString())))
                            {
                                try { totl = totl - Convert.ToDouble(textBox4.Text.ToString()); }
                                catch { }
                            }
                            pck += totl * val_en;
                        }
                    }
                    catch { }
                }
                txt_or_amnt.Text = gm.toAccountingFormat(((pck + n_pck) - lessdiscount) + adtl);
            }
            catch (Exception)
            { }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            gchk_rows();
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            gchk_rows();
        }
        public void an_frm_load(String rg, String cc, String cn)
        {
            thisDatabase dbs = new thisDatabase();
            DataTable dt_cur = new DataTable();

            String WHERE = (((rg == "" || String.IsNullOrEmpty(rg)) && (cc == "" || String.IsNullOrEmpty(cc)) && (cn == "" || String.IsNullOrEmpty(cn))) ? "SELECT false AS bool_check, '0'::text AS pax, 'NORC'::text AS chg_code" : "SELECT true AS bool_check, SPLIT_PART(reference, ' ', 1) AS pax, chg_code FROM rssys.chgfil WHERE reg_num = '" + rg + "' AND chg_code = '" + cc + "' AND chg_num = '" + cn + "' GROUP BY chg_code, reference ORDER BY chg_code ASC");
            dt_cur = dbs.QueryBySQLCode("SELECT (CASE WHEN bool_check = true THEN bool_check ELSE false END) AS bool_check, COALESCE(pax, '0') AS pax, charge.chg_code, chg_desc, price, ifree FROM rssys.charge LEFT JOIN (" + WHERE + ") rs ON rs.chg_code = charge.chg_code WHERE UPPER(charge.chg_code) NOT LIKE 'TRNS%' AND chg_type = 'C' ORDER BY charge.chg_code ASC");

            dataGridView1.DataSource = dt_cur;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            gchk_rows();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            gchk_rows();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            gchk_rows();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            gchk_rows();
        }

        private void vald_save(Double price, String c_code)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            String curdate = dtp_tdate.Value.ToString("yyyy-MM-dd");
            String curtime = DateTime.Now.ToString("HH:mm");
            String reg_num = ((rg_code != "") ? rg_code : db.QueryBySQLCodeRetStr("SELECT reg_num FROM rssys.gfolio WHERE res_code = '" + lbl_resnum.Text.ToString() + "'"));
            String chg_num = ((c_num != "") ? c_num : db.QueryBySQLCodeRetStr("SELECT chg_num FROM rssys.charge WHERE chg_code = '" + c_code + "'"));
            String ref_c = (((cbo_chg.SelectedValue ?? "").ToString() == "P") ? txt_ref.Text.ToString() : c_ref);

            String col = ((gisnew) ? "INSERT INTO rssys.chgfil (reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, chg_date, res_code, food, misc, vat_amnt, sc_amnt, tax, bill_amnt) SELECT reg_num, '" + c_code + "', " + chg_num + ", rom_code, '" + ref_c + "', '" + price + "', user_id, '" + curdate + "' AS t_date, '" + curtime + "' AS t_time, current_date AS chg_date, res_code, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 FROM rssys.gfolio WHERE reg_num = '" + reg_num + "'" : "chg_code = '" + c_code + "', reference = '" + txt_ref.Text.ToString() + "', amount = '" + price + "', t_date = '" + curdate + "', t_time = '" + curtime + "'");
            String val = ((gisnew) ? "" : "reg_num = '" + reg_num + "' AND chg_num = '" + chg_num + "'");

            Boolean stat = ((gisnew) ? db.QueryBySQLCode_bool(col) : db.UpdateOnTable("chgfil", col, val));

            if (stat)
            {
                MessageBox.Show("Successfully " + ((gisnew) ? "added new" : "updated") + " entry.");
                Boolean proc_d = ((gisnew) ? db.set_all_pk("charge", "chg_num", chg_num, "chg_code='" + c_code + "'", chg_num.Length) : false );
                if (proc_d) { } else if(proc_d == false && gisnew == false) { } else { MessageBox.Show("Error on incrementing"); }
                rGuestBill.up_dt();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error on " + ((gisnew) ? "adding new" : "updating") + " entry.");
            }
        }

        private void cbo_chg_SelectedValueChanged(object sender, EventArgs e)
        {
            on_chg();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void gchk_rows()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToBoolean(dataGridView1["bool_check", i].Value.ToString()) == true)
                    {
                        Double val_en = 0;
                        if ((dataGridView1["chg_desc", i].Value.ToString()).ToUpper().Contains("PACKAGE"))
                        {
                            if ((dataGridView1["chg_desc", i].Value.ToString()).ToUpper().Contains("ADULT"))
                            {
                                try
                                {
                                    val_en = Convert.ToDouble(textBox2.Text.ToString());
                                }
                                catch { }
                            }
                            if ((dataGridView1["chg_desc", i].Value.ToString()).ToUpper().Contains("KID"))
                            {
                                try
                                {
                                    val_en = Convert.ToDouble(textBox3.Text.ToString());
                                }
                                catch { }
                            }
                            else
                            {
                                try
                                {
                                    val_en = Convert.ToDouble(textBox4.Text.ToString());
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            try
                            {
                                val_en = Convert.ToDouble(textBox1.Text.ToString());
                            }
                            catch { }
                        }
                        dataGridView1["pax", i].ReadOnly = false;
                        dataGridView1["pax", i].Value = ent_kc_st(((dataGridView1["pax", i].Value.ToString() == "") ? val_en.ToString() : ((dataGridView1["ifree", i].Value.ToString().ToUpper() == "TRUE" && textBox4.Text.ToString() != "0" && (Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) == Convert.ToDouble(textBox4.Text.ToString()))) ? (Convert.ToDouble(dataGridView1["pax", i].Value.ToString()) - Convert.ToDouble(textBox4.Text.ToString())).ToString() : dataGridView1["pax", i].Value.ToString())));
                    }
                    else
                    {
                        dataGridView1["pax", i].ReadOnly = true;
                        dataGridView1["pax", i].Value = ent_kc_st("0");
                    }
                }
                catch { }
            }
            get_tl();
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            gchk_rows();
        }
    }
}
