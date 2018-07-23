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
        private newGuestBilling rGuestBill;
        private Boolean isnew = true;
        bool blHasDot = false;
        String chgtype = "";
        String currency_code = "PHP"; //default currency

        //public add_charge(rGuestBilling rgbilling)
        //{
        //    InitializeComponent();

        //    rGuestBill = rgbilling;
        //}
        public add_charge(newGuestBilling rgbilling)
        {
            rGuestBill = rgbilling;

            InitializeComponent();
            load_chargecbo();
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
            lbl_or_amt.Hide();
            txt_or_amnt.Hide();

            pnl_side.BackColor = Color.DarkKhaki;
        }

        private void load_chargecbo()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.QueryOnTableWithParams("charge", "chg_code, chg_desc", "", "ORDER BY chg_code ASC;");

                cbo_chg.DataSource = dt;
                cbo_chg.DisplayMember = "chg_desc";
                cbo_chg.ValueMember = "chg_code";
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
            Double vat_included = 0.00;
            Double sc_included = 0.00;

            String doctyp = "";
            String tofr_fol = "";
            String res_code = "";

            Report rpt = new Report("", "");

            if (chgtype == "P" && cbo_doctyp.Text != "")
            {
                if (cbo_doctyp.Text == "Cash Credit")
                {
                    doctyp = "CC";
                }
                else if (cbo_doctyp.Text == "No O.R.")
                {
                    doctyp = "NA";
                }
            }

            if (gm.is_Doublevalid(txt_amt.Text) == false)
            {
                MessageBox.Show("Invalid Amout.");
            }
            else if (chgtype == "P" && cbo_doctyp.Text == "")
            {
                MessageBox.Show("Pls enter the document type for payment charges");
            }
            else if (txt_ref.Text == "")
            {
                MessageBox.Show("Pls enter the reference textfield.");
            }
            else if (Convert.ToDouble(txt_amt.Text) == 0.00)
            {
                MessageBox.Show("Invalid Amount.");
            }
            else if (cbo_chg.SelectedIndex == -1)
            {
                MessageBox.Show("Pls select the valid charge.");
            }
            else if (db.iscardpayment(cbo_chg.SelectedValue.ToString()) && txt_ccard_no.Text == "")
            {
                MessageBox.Show("Card Number should be inputted.");
            }
            else if (db.iscardpayment(cbo_chg.SelectedValue.ToString()) && txt_traceno.Text == "")
            {
                MessageBox.Show("Trace Number should be inputted.");
            }
            else
            {
                String chg_code = cbo_chg.SelectedValue.ToString();

                Double netamt = db.get_netrate(Convert.ToDouble(txt_amt.Text), 0.00, 0.00);

                if (db.has_vat(chg_code))
                {
                    vat_included = db.get_tax(Convert.ToDouble(txt_amt.Text), 0.00, 0.00);
                }
                if (db.has_sc(chg_code))
                {
                    sc_included = db.get_svccharge(Convert.ToDouble(txt_amt.Text), 0.00, 0.00);
                }

                if (isnew)
                {
                    if (db.is_roomcharge(chg_code))
                    {
                        lbl_gfolionum.Text = rGuestBill.lbl_gfolio.Text;
                        DataTable dt;
                        dt = db.QueryBySQLCode("SELECT g.*, c.* from rssys.chgfil c LEFT JOIN rssys.gfolio g ON g.reg_num=c.reg_num WHERE g.reg_num='" + lbl_gfolionum.Text + "'");
                        if (dt.Rows.Count != 0)
                        {
                            lbl_arrdate.Text = dt.Rows[0]["arr_date"].ToString();
                        }

                        if (db.roomcharge_reg(lbl_gfolionum.Text, chg_code, rGuestBill.rom_code, txt_ref.Text, dtp_tdate.Value.ToString("yyyy-MM-dd"), Convert.ToDouble(txt_amt.Text), "D", Convert.ToDateTime(dtp_tdate.Value.ToString("yyyy-MM-dd")), null, ""))
                        {
                            rGuestBill.disp_chgfil(lbl_gfolionum.Text);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error occured on saving charge.");
                        }                        
                    }
                    else if (db.is_roomcharge_senior(chg_code))
                    {
                        /*
                        if (db.romcharge_senior(lbl_gfolionum.Text, chg_code, rGuestBill.rom_code, txt_ref.Text, dtp_tdate.Value.ToString("yyyy-MM-dd"), Convert.ToDouble(txt_amt.Text)))
                        {
                            rGuestBill.disp_chgfil(lbl_gfolionum.Text);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error occured on saving charge.");
                        }*/
                    }
                    else
                    {
                        db.insert_charges(lbl_gfolionum.Text, lbl_guestname.Text, chg_code, lbl_rom_code.Text, txt_ref.Text, Convert.ToDouble(txt_amt.Text), dtp_tdate.Value.ToString("yyyy-MM-dd"), "", "", res_code, tofr_fol, doctyp, vat_included, sc_included, Convert.ToDouble(txt_or_amnt.Text), txt_traceno.Text, txt_ccard_no.Text, currency_code, true);

                        rGuestBill.disp_chgfil(lbl_gfolionum.Text);
                        this.Close();
                    }
                }
                //UPDATE CHARGES
                else
                {
                    db.update_charges(lbl_gfolionum.Text, lbl_guestname.Text, chg_code, lbl_rom_code.Text, txt_ref.Text, Convert.ToDouble(txt_amt.Text), dtp_tdate.Value.ToString("yyyy-MM-dd"), "", "", res_code, tofr_fol, doctyp, vat_included, sc_included, txt_chg_num.Text, Convert.ToDouble(txt_amt.Text), txt_traceno.Text, txt_ccard_no.Text, currency_code, true);
                    rGuestBill.disp_chgfil(lbl_gfolionum.Text);
                    this.Close();
                }
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
            thisDatabase db = new thisDatabase();

            if (cbo_chg.SelectedIndex != -1)
            {
                String chg_code = cbo_chg.SelectedValue.ToString();

                chgtype = db.get_chg_type(chg_code);

                lbl_card_no_name.Enabled = false;
                lbl_trace_no_name.Enabled = false;
                txt_ccard_no.Enabled = false;
                txt_traceno.Enabled = false;

                //MessageBox.Show(chg_code);
                if (chgtype == "")
                {
                    btn_save.Enabled = false;
                }
                else if (chgtype == "P")
                {
                    cbo_doctyp.Enabled = true;
                    btn_save.Enabled = true;
                    txt_amt.Enabled = true;

                    if (chg_code == "102" || chg_code == "103" || chg_code == "104" || chg_code == "105")
                    {
                        lbl_card_no_name.Enabled = true;
                        lbl_trace_no_name.Enabled = true;
                        txt_ccard_no.Enabled = true;
                        txt_traceno.Enabled = true;
                    }
                }
                else if (chgtype == "C")
                {
                    if (db.is_roomcharge(chg_code) || db.is_roomcharge_senior(chg_code))
                    {
                        txt_ref.Text = "ROOM CHARGE";
                        txt_amt.Text = rGuestBill.rom_rate;
                        //txt_amt.Enabled = false;
                    }
                    cbo_doctyp.Enabled = false;
                    btn_save.Enabled = true;
                }
            }
            else
            {
                btn_save.Enabled = false;
            }
        }

        private void cbo_chg_TextChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            if (cbo_chg.SelectedIndex != -1)
            {
                String chg_code = cbo_chg.SelectedValue.ToString();

                chgtype = db.get_chg_type(chg_code);
                //MessageBox.Show(chg_code);
                if (chgtype == "")
                {
                    btn_save.Enabled = false;
                }
                else if (chgtype == "P")
                {
                    cbo_doctyp.Enabled = true;
                    btn_save.Enabled = true;
                    txt_amt.Enabled = true;
                }
                else if (chgtype == "C")
                {
                    if (db.is_roomcharge(chg_code) || db.is_roomcharge_senior(chg_code))
                    {
                        txt_ref.Text = "ROOM CHARGE";
                        txt_amt.Text = rGuestBill.rom_rate;
                        //txt_amt.Enabled = false;
                    }
                    cbo_doctyp.Enabled = false;
                    btn_save.Enabled = true;
                }
            }
            if (chgtype == "")
            {
                btn_save.Enabled = false;
            }
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
    }
}
