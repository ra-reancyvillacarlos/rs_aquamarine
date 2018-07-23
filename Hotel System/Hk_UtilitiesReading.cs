﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class Hk_UtilitiesReading : Form
    {
        String schema = "";
        String t_date = "";
        int prev_rm = 0;
        thisDatabase db = new thisDatabase();
        public Hk_UtilitiesReading()
        {
            InitializeComponent();
        }

        private void Hk_UtilitiesReading_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            int flr = 2;
            thisDatabase db = new thisDatabase();

            t_date = db.get_systemdate("");
            schema = db.get_schema();

            lbl_flr.Text = flr.ToString();

            load_chargecbo();

            cbo_charge.SelectedIndex = -1;

            load_rooms(flr);

            try
            {
                lbl_misc_rm.Text = "";
                lbl_gfolio.Text = "";
                lbl_gname.Text = "";
                cbo_charge.SelectedIndex = -1;
                txt_chargeamt.Text = "0.00";
                txt_prev_reading.Text = "";
                txt_cur_reading.Text = "";
                txt_prev_reading.Text = "";

                cbo_charge.Enabled = false;
                txt_prev_reading.Enabled = false;
                txt_cur_reading.Enabled = false;
                txt_prev_reading.Enabled = false;
                btn_save.Enabled = false;
                btn_cancel.Enabled = false;

                dgv_mischist.Rows.Clear();
            }
            catch (Exception) { }
        }

        private void load_rooms(int pg)
        {
            String drtyp = "", rmstatus="";

            //current status of the room
            try
            {
                tv_reset_form();

                /////////////////////////////////////////////////////////////////
                if (rmstatus == "ALL VACANTS")
                {
                    rmstatus = " AND (r.stat_code='VC' OR r.stat_code='VD')";
                }
                else if (rmstatus == "VACANT CLEAN")
                {
                    rmstatus = " AND r.stat_code='VC'";
                }
                else if (rmstatus == "VACANT DIRTY")
                {
                    rmstatus = " AND r.stat_code='VD'";
                }
                else if (rmstatus == "OCCUPIED")
                {
                    rmstatus = " AND r.stat_code='OCC'";
                }
                else if (rmstatus == "OUT OF ORDER")
                {
                    rmstatus = " AND r.stat_code='OOO'";
                }
                else if (rmstatus == "RESERVED")
                {
                    rmstatus = "";
                }
                ////////////////////////////////////////////////////////////
                 


                String WHERE = "";

                if (pg > 0)
                {
                   // WHERE = " WHERE r.rom_code LIKE '" + pg + "%' AND r.stat_code='OCC' AND g.typ_code != 'Z'";
                    WHERE = " WHERE r.rom_code LIKE '" + pg + "%'";

                }

                thisDatabase db = new thisDatabase();
                String SQL_room = "SELECT r.rom_code, r.rom_desc, r.typ_code, r.stat_code, g.reg_num FROM "+schema+".rooms r LEFT JOIN "+schema+".gfolio g ON r.rom_code=g.rom_code" + WHERE + " ORDER BY r.rom_code ASC";

                //String SQL_room = "SELECT r.rom_code, r.rom_desc, r.typ_code, r.stat_code FROM " + schema + ".rooms r" + WHERE + " ORDER BY r.rom_code ASC";

                DataTable dt = db.QueryBySQLCode(SQL_room);

                int i = 1, x = 10;

                foreach (DataRow row in dt.Rows)
                {
                    set_panel(i, row["rom_code"].ToString(), row["rom_desc"].ToString() + "\n" + row["typ_code"].ToString(), row["stat_code"].ToString(), row["reg_num"].ToString());

                    if (x - i == 3)
                    {
                        i = i + 4;
                        x = x + 10;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void load_chargecbo()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                dt.Rows.Add();
                dt.AcceptChanges();

                dt = db.QueryOnTableWithParams("charge  ", "chg_code, chg_desc", "chg_type='C' AND (utility='ELECTRICITY' OR utility='WATER')", "ORDER BY chg_code ASC;");

                cbo_charge.DataSource = dt;
                cbo_charge.DisplayMember = "chg_desc";
                cbo_charge.ValueMember = "chg_code";
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void tv_reset_form()
        {
            pnl_rm01.Hide();
            lbl_rmres01.Hide();
            pnl_rm02.Hide();
            lbl_rmres02.Hide();
            pnl_rm03.Hide();
            lbl_rmres03.Hide();
            pnl_rm04.Hide();
            lbl_rmres04.Hide();
            pnl_rm05.Hide();
            lbl_rmres05.Hide();
            pnl_rm06.Hide();
            lbl_rmres06.Hide();
            pnl_rm07.Hide();
            lbl_rmres07.Hide();

            pnl_rm11.Hide();
            lbl_rmres11.Hide();
            pnl_rm12.Hide();
            lbl_rmres12.Hide();
            pnl_rm13.Hide();
            lbl_rmres13.Hide();
            pnl_rm14.Hide();
            lbl_rmres14.Hide();
            pnl_rm15.Hide();
            lbl_rmres15.Hide();
            pnl_rm16.Hide();
            lbl_rmres16.Hide();
            pnl_rm17.Hide();
            lbl_rmres17.Hide();

            pnl_rm21.Hide();
            lbl_rmres21.Hide();
            pnl_rm22.Hide();
            lbl_rmres22.Hide();
            pnl_rm23.Hide();
            lbl_rmres23.Hide();
            pnl_rm24.Hide();
            lbl_rmres24.Hide();
            pnl_rm25.Hide();
            lbl_rmres25.Hide();
            pnl_rm26.Hide();
            lbl_rmres26.Hide();
            pnl_rm27.Hide();
            lbl_rmres27.Hide();

            pnl_rm31.Hide();
            lbl_rmres31.Hide();
            pnl_rm32.Hide();
            lbl_rmres32.Hide();
            pnl_rm33.Hide();
            lbl_rmres33.Hide();
            pnl_rm34.Hide();
            lbl_rmres34.Hide();
            pnl_rm35.Hide();
            lbl_rmres35.Hide();
            pnl_rm36.Hide();
            lbl_rmres36.Hide();
            pnl_rm37.Hide();
            lbl_rmres37.Hide();
        }

        private void set_panel(int no, String rm, String rtyp, String status, String gfolio)
        {
            Color stat = Color.Blue;

            ///panels or rooms
            if (no == 1)
            {
                pnl_rm01.Show();
                pictureBox1.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='"+rm+"'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox1.Show();
                }
                lbl_rm01.Text = rm;
                lbl_rmstatus01.Text = rtyp;
                pnl_rm01.BackColor = stat;
                lbl_rmres01.Text = gfolio;
            }
            else if (no == 2)
            {
                pnl_rm02.Show();
                pictureBox2.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox2.Show();
                }
                lbl_rm02.Text = rm;
                lbl_rmstatus02.Text = rtyp;
                pnl_rm02.BackColor = stat;
                lbl_rmres02.Text = gfolio;
            }
            else if (no == 3)
            {
                pnl_rm03.Show();
                pictureBox3.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox3.Show();
                }
                lbl_rm03.Text = rm;
                lbl_rmstatus03.Text = rtyp;
                pnl_rm03.BackColor = stat;
                lbl_rmres03.Text = gfolio;
            }
            else if (no == 4)
            {
                pnl_rm04.Show();
                pictureBox4.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox4.Show();
                }
                lbl_rm04.Text = rm;
                lbl_rmstatus04.Text = rtyp;
                pnl_rm04.BackColor = stat;
                lbl_rmres04.Text = gfolio;
            }
            else if (no == 5)
            {
                pnl_rm05.Show();
                pictureBox5.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox5.Show();
                }
                lbl_rm05.Text = rm;
                lbl_rmstatus05.Text = rtyp;
                pnl_rm05.BackColor = stat;
                lbl_rmres05.Text = gfolio;
            }
            else if (no == 6)
            {
                pnl_rm06.Show();
                pictureBox6.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox6.Show();
                }
                lbl_rm06.Text = rm;
                lbl_rmstatus06.Text = rtyp;
                pnl_rm06.BackColor = stat;
                lbl_rmres06.Text = gfolio;
            }
            else if (no == 7)
            {
                pnl_rm07.Show();
                pictureBox7.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox7.Show();
                }
                lbl_rm07.Text = rm;
                lbl_rmstatus07.Text = rtyp;
                pnl_rm07.BackColor = stat;
                lbl_rmres07.Text = gfolio;
            }

            else if (no == 11)
            {
                pnl_rm11.Show();
                pictureBox8.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox8.Show();
                }
                lbl_rm11.Text = rm;
                lbl_rmstatus11.Text = rtyp;
                pnl_rm11.BackColor = stat;
                lbl_rmres11.Text = gfolio;
            }
            else if (no == 12)
            {
                pnl_rm12.Show();
                pictureBox9.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox9.Show();
                }
                lbl_rm12.Text = rm;
                lbl_rmstatus12.Text = rtyp;
                pnl_rm12.BackColor = stat;
                lbl_rmres12.Text = gfolio;
            }
            else if (no == 13)
            {
                pnl_rm13.Show();
                pictureBox10.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox10.Show();
                }
                lbl_rm13.Text = rm;
                lbl_rmstatus13.Text = rtyp;
                pnl_rm13.BackColor = stat;
                lbl_rmres13.Text = gfolio;
            }
            else if (no == 14)
            {
                pnl_rm14.Show();
                pictureBox11.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox11.Show();
                }
                lbl_rm14.Text = rm;
                lbl_rmstatus14.Text = rtyp;
                pnl_rm14.BackColor = stat;
                lbl_rmres14.Text = gfolio;
            }
            else if (no == 15)
            {
                pnl_rm15.Show();
                pictureBox12.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox12.Show();
                }
                lbl_rm15.Text = rm;
                lbl_rmstatus15.Text = rtyp;
                pnl_rm15.BackColor = stat;
                lbl_rmres15.Text = gfolio;

            }
            else if (no == 16)
            {
                pnl_rm16.Show();
                pictureBox13.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox13.Show();
                }
                lbl_rm16.Text = rm;
                lbl_rmstatus16.Text = rtyp;
                pnl_rm16.BackColor = stat;
                lbl_rmres16.Text = gfolio;
            }
            else if (no == 17)
            {
                pnl_rm17.Show();
                pictureBox14.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox14.Show();
                }
                lbl_rm17.Text = rm;
                lbl_rmstatus17.Text = rtyp;
                pnl_rm17.BackColor = stat;
                lbl_rmres17.Text = gfolio;
            }

            else if (no == 21)
            {
                pnl_rm21.Show();
                pictureBox15.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox15.Show();
                }
                lbl_rm21.Text = rm;
                lbl_rmstatus21.Text = rtyp;
                pnl_rm21.BackColor = stat;
                lbl_rmres21.Text = gfolio;
            }
            else if (no == 22)
            {
                pnl_rm22.Show();
                pictureBox16.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox16.Show();
                }
                lbl_rm22.Text = rm;
                lbl_rmstatus22.Text = rtyp;
                pnl_rm22.BackColor = stat;
                lbl_rmres22.Text = gfolio;
            }
            else if (no == 23)
            {
                pnl_rm23.Show();
                pictureBox17.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox17.Show();
                }
                lbl_rm23.Text = rm;
                lbl_rmstatus23.Text = rtyp;
                pnl_rm23.BackColor = stat;
                lbl_rmres23.Text = gfolio;
            }
            else if (no == 24)
            {
                pnl_rm24.Show();
                pictureBox18.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox18.Show();
                }
                lbl_rm24.Text = rm;
                lbl_rmstatus24.Text = rtyp;
                pnl_rm24.BackColor = stat;
                lbl_rmres24.Text = gfolio;
            }
            else if (no == 25)
            {
                pnl_rm25.Show();
                pictureBox19.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox19.Show();
                }
                lbl_rm25.Text = rm;
                lbl_rmstatus25.Text = rtyp;
                pnl_rm25.BackColor = stat;
                lbl_rmres25.Text = gfolio;
            }
            else if (no == 26)
            {
                pnl_rm26.Show();
                pictureBox20.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox20.Show();
                }
                lbl_rm26.Text = rm;
                lbl_rmstatus26.Text = rtyp;
                pnl_rm26.BackColor = stat;
                lbl_rmres26.Text = gfolio;
            }
            else if (no == 27)
            {
                pnl_rm27.Show();
                pictureBox21.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox21.Show();
                }
                lbl_rm27.Text = rm;
                lbl_rmstatus27.Text = rtyp;
                pnl_rm27.BackColor = stat;
                lbl_rmres27.Text = gfolio;
            }

            else if (no == 31)
            {
                pnl_rm31.Show();
                pictureBox22.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox22.Show();
                }
                lbl_rm31.Text = rm;
                lbl_rmstatus31.Text = rtyp;
                pnl_rm31.BackColor = stat;
                lbl_rmres31.Text = gfolio;
            }
            else if (no == 32)
            {
                pnl_rm32.Show();
                pictureBox23.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox23.Show();
                }
                lbl_rm32.Text = rm;
                lbl_rmstatus32.Text = rtyp;
                pnl_rm32.BackColor = stat;
                lbl_rmres32.Text = gfolio;
            }
            else if (no == 33)
            {
                pnl_rm33.Show();
                pictureBox24.Hide();

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox24.Show();
                }
                lbl_rm33.Text = rm;
                lbl_rmstatus33.Text = rtyp;
                pnl_rm33.BackColor = stat;
                lbl_rmres33.Text = gfolio;
            }
            else if (no == 34)
            {
                pnl_rm34.Show();
                pictureBox25.Hide();
                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox25.Show();
                }
                lbl_rm34.Text = rm;
                lbl_rmstatus34.Text = rtyp;
                pnl_rm34.BackColor = stat;
                lbl_rmres34.Text = gfolio;
            }
            else if (no == 35)
            {
                pnl_rm35.Show();
                pictureBox26.Hide();
                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox26.Show();
                }
                lbl_rm35.Text = rm;
                lbl_rmstatus35.Text = rtyp;
                pnl_rm35.BackColor = stat;
                lbl_rmres35.Text = gfolio;
            }
            else if (no == 36)
            {
                pnl_rm36.Show();
                pictureBox27.Hide();
                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox27.Show();
                }
                lbl_rm36.Text = rm;
                lbl_rmstatus36.Text = rtyp;
                pnl_rm36.BackColor = stat;
                lbl_rmres36.Text = gfolio;
            }
            else if (no == 37)
            {
                pnl_rm37.Show();
                pictureBox28.Hide();
                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.gfolio WHERE rom_code='" + rm + "'");
                if (dt.Rows.Count > 0)
                {
                    pictureBox28.Show();
                }
                lbl_rm37.Text = rm;
                lbl_rmstatus37.Text = rtyp;
                pnl_rm37.BackColor = stat;
                lbl_rmres37.Text = gfolio;
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(lbl_flr.Text);

            i++;

            lbl_flr.Text = i.ToString();

            load_rooms(i);
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(lbl_flr.Text);

            i--;

            if (i > 0)
            {
                lbl_flr.Text = i.ToString();
                load_rooms(i);
            }
        }

        private void set_defaultcolor(int no)
        {
            Color stat = Color.Blue;

            ///panels or rooms
            if (no == 1)
            {
                pnl_rm01.BackColor = stat;
            }
            else if (no == 2)
            {
                pnl_rm02.BackColor = stat;
            }
            else if (no == 3)
            {
                pnl_rm03.BackColor = stat;
            }
            else if (no == 4)
            {
                pnl_rm04.BackColor = stat;
            }
            else if (no == 5)
            {
                pnl_rm05.BackColor = stat;
            }
            else if (no == 6)
            {
                pnl_rm06.BackColor = stat;
            }
            else if (no == 7)
            {
                pnl_rm07.BackColor = stat;
            }

            else if (no == 11)
            {
                pnl_rm11.BackColor = stat;
            }
            else if (no == 12)
            {
                pnl_rm12.BackColor = stat;
            }
            else if (no == 13)
            {
                pnl_rm13.BackColor = stat;
            }
            else if (no == 14)
            {
                pnl_rm14.BackColor = stat;
            }
            else if (no == 15)
            {
                pnl_rm15.BackColor = stat;

            }
            else if (no == 16)
            {
                pnl_rm16.BackColor = stat;
            }
            else if (no == 17)
            {
                pnl_rm17.BackColor = stat;
            }

            else if (no == 21)
            {
                pnl_rm21.BackColor = stat;
            }
            else if (no == 22)
            {
                pnl_rm22.BackColor = stat;
            }
            else if (no == 23)
            {
                pnl_rm23.BackColor = stat;
            }
            else if (no == 24)
            {
                pnl_rm24.BackColor = stat;
            }
            else if (no == 25)
            {
                pnl_rm25.BackColor = stat;
            }
            else if (no == 26)
            {
                pnl_rm26.BackColor = stat;
            }
            else if (no == 27)
            {
                pnl_rm27.BackColor = stat;
            }

            else if (no == 31)
            {
                pnl_rm31.BackColor = stat;
            }
            else if (no == 32)
            {
                pnl_rm32.BackColor = stat;
            }
            else if (no == 33)
            {
                pnl_rm33.BackColor = stat;
            }
            else if (no == 34)
            {
                pnl_rm34.BackColor = stat;
            }
            else if (no == 35)
            {
                pnl_rm35.BackColor = stat;
            }
            else if (no == 36)
            {
                pnl_rm36.BackColor = stat;
            }
            else if (no == 37)
            {
                pnl_rm37.BackColor = stat;
            }
        }

        private void click_room(String rm, String gfolio)
        {
            thisDatabase db = new thisDatabase();

            lbl_misc_rm.Text = rm;
            lbl_gfolio.Text = gfolio;
            lbl_gname.Text = get_gname(gfolio);

            cbo_charge.Enabled = true;
            txt_cur_reading.Enabled = true;
            txt_prev_reading.Enabled = true;
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;

            cbo_charge.SelectedIndex = -1;
            txt_chargeamt.Text = "0.00";
            txt_prev_reading.Text = "";

            dtp_chgdate.ResetText();

            dgv_mischist.DataSource = db.get_chargehistory(lbl_gfolio.Text);
        }

        private void load_prev_charges(String rm, String gfolio)
        {
           // thisDatabase db = new thisDatabase();

            //dgv_precharges.DataSource = db.QueryBySQLCode("");
        }

        private String get_gname(String gfolio)
        {
            thisDatabase db = new thisDatabase();

            DataTable dt = db.QueryOnTableWithParams("gfolio","full_name","reg_num='"+gfolio+"'","");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception er)
            {
                //MessageBox.Show("get_noofromByStatus: " + er.Message);
            }

            return null;
        }

        private String get_price()
        {
            String c_code = cbo_charge.SelectedValue.ToString();
            thisDatabase db = new thisDatabase();
            DataTable dt = db.QueryOnTableWithParams("charge", "price", "chg_code='" + c_code + "'", "");

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception er)
            {
                //MessageBox.Show("get_noofromByStatus: " + er.Message);
            }


            return "0.00";

        }

        //click room panels
        private void pnl_rm01_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm01.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 1;
            click_room(lbl_rm01.Text, lbl_rmres01.Text);
        }       

        private void pnl_rm02_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm02.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 2;

            click_room(lbl_rm02.Text, lbl_rmres02.Text);
        }

        private void pnl_rm03_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm03.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 3;

            click_room(lbl_rm03.Text, lbl_rmres03.Text);
        }

        private void pnl_rm04_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm04.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 4;

            click_room(lbl_rm04.Text, lbl_rmres04.Text);
        }

        private void pnl_rm05_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm05.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 5;

            click_room(lbl_rm05.Text, lbl_rmres05.Text);
        }

        private void pnl_rm06_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm06.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 6;

            click_room(lbl_rm06.Text, lbl_rmres06.Text);
        }

        private void pnl_rm07_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm07.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 7;

            click_room(lbl_rm07.Text, lbl_rmres07.Text);
        }

        private void pnl_rm11_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm11.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 11;

            click_room(lbl_rm11.Text, lbl_rmres11.Text);
        }

        private void pnl_rm12_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm12.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 12;

            click_room(lbl_rm12.Text, lbl_rmres12.Text);
        }
        
        private void pnl_rm13_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pnl_rm13.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 13;

            click_room(lbl_rm13.Text, lbl_rmres13.Text);
        }

        private void pnl_rm14_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm14.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 14;

            click_room(lbl_rm14.Text, lbl_rmres14.Text);
        }

        private void pnl_rm15_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm15.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 15;

            click_room(lbl_rm15.Text, lbl_rmres15.Text);
        }

        private void pnl_rm16_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm16.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 16;

            click_room(lbl_rm16.Text, lbl_rmres16.Text);
        }

        private void pnl_rm17_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm17.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 17;

            click_room(lbl_rm17.Text, lbl_rmres17.Text);
        }

        private void pnl_rm21_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm21.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 21;

            click_room(lbl_rm21.Text, lbl_rmres21.Text);
        }

        private void pnl_rm22_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm22.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 22;

            click_room(lbl_rm22.Text, lbl_rmres22.Text);
        }
        
        private void pnl_rm23_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm23.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 23;

            click_room(lbl_rm23.Text, lbl_rmres23.Text);
        }

        private void pnl_rm24_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm24.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 24;

            click_room(lbl_rm24.Text, lbl_rmres24.Text);
        }

        private void pnl_rm25_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm25.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 25;

            click_room(lbl_rm25.Text, lbl_rmres25.Text);
        }

        private void pnl_rm26_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm26.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 26;

            click_room(lbl_rm26.Text, lbl_rmres26.Text);
        }

        private void pnl_rm27_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm27.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 27;

            click_room(lbl_rm27.Text, lbl_rmres27.Text);
        }

        private void pnl_rm31_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm31.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 31;

            click_room(lbl_rm31.Text, lbl_rmres31.Text);
        }

        private void pnl_rm32_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm32.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 32;

            click_room(lbl_rm32.Text, lbl_rmres32.Text);
        }

        private void pnl_rm33_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm33.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 33;

            click_room(lbl_rm33.Text, lbl_rmres33.Text);
        }

        private void pnl_rm34_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm34.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 34;

            click_room(lbl_rm34.Text, lbl_rmres34.Text);
        }

        private void pnl_rm35_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm35.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 35;

            click_room(lbl_rm35.Text, lbl_rmres35.Text);
        }

        private void pnl_rm36_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm36.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 36;

            click_room(lbl_rm36.Text, lbl_rmres36.Text);
        }

        private void pnl_rm37_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_rm37.BackColor = Color.Green;

            set_defaultcolor(prev_rm);
            prev_rm = 37;

            click_room(lbl_rm37.Text, lbl_rmres37.Text);
        }

        private void cbo_charge_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_previous_reading();
            compute_reading();
        }

        private void set_previous_reading()
        {
            if (cbo_charge.SelectedIndex != -1)
            {
                
            
            
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();

            try
            {

                if (cbo_charge.SelectedValue.ToString() == "ELTRC" || cbo_charge.SelectedValue.ToString() == "WTR")
                {
                    String rom = lbl_misc_rm.Text;
                    String gfolio = lbl_gfolio.Text;
                    String gname = lbl_gname.Text;
                    String chg_amt_total = gm.toNormalDoubleFormat(txt_chargeamt.Text).ToString("0.00"); ;
                    String c_code = cbo_charge.SelectedValue.ToString();
                    String refno = "";
                    String chg_num = "";
                    String bill_amnt = "0.00", tax = "0.00";
                    Double diff = 0.00;
                    Double prev = 0.00, curr = 0.00;
                    String chg_date = dtp_chgdate.Value.ToString("yyyy-MM-dd");//db.get_systemdate("");
                    
                    prev = gm.toNormalDoubleFormat(txt_prev_reading.Text);
                    curr = gm.toNormalDoubleFormat(txt_cur_reading.Text);
                    diff = Math.Round(curr - prev, 0, MidpointRounding.AwayFromZero);
                    bill_amnt = gm.toNormalDoubleFormat(txt_bill.Text).ToString();
                    tax = gm.toNormalDoubleFormat(txt_tax.Text).ToString();
                    if (cbo_charge.SelectedValue.ToString() == "ELTRC" || cbo_charge.SelectedValue.ToString() == "WTR")
                    {
                        refno = "Prev: " + prev.ToString("0.00") + "Kwh less Cur:" + curr.ToString("0.00") + "Kwh = " + diff.ToString("0.00") + "Kwh ";
                    }
                    else
                        refno = "Prev: " + prev.ToString("0.00") + "cu less Curr:" + curr.ToString("0.00") + "cu = " + diff.ToString("0.00") + "cu ";

                    DataTable dtnextnum = db.QueryOnTableWithParams("charge", "chg_num", "chg_code='" + c_code + "'", "");

                    chg_num = dtnextnum.Rows[0][0].ToString();

                    db.InsertOnTable("chgfil", "reg_num, chg_code, chg_num, rom_code, reference, amount, user_id, t_date, t_time, fol_num, chg_date, bill_amnt, tax",
                        "'" + gfolio + "', '" + c_code + "', '" + chg_num + "', '" + rom + "', '" + refno + "', '" + chg_amt_total + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', 1,'" + chg_date + "', '" + bill_amnt + "', '" + tax + "'");

                    db.UpdateOnTable("charge", "chg_num='" + (Convert.ToInt32(chg_num) + 1).ToString("00000000") + "'", "chg_code='" + c_code + "'");

                    MessageBox.Show("Charge to RM." + lbl_misc_rm.Text + " succesfully.");


                    txt_prev_reading.Text = "";
                    txt_cur_reading.Text = "";
                    txt_prev_reading.Text = "";
                    txt_chargeamt.Text = "0.00";
                    txt_bill.Text = "0.00";
                    txt_tax.Text = "0.00";
                    try { cbo_charge.SelectedIndex = -1; } catch { }
                    dtp_chgdate.ResetText();
                    dgv_mischist.DataSource = db.get_chargehistory(lbl_gfolio.Text);

                }
                else
                {
                    MessageBox.Show("Invalid selection. Please select entry for electricity or water consumption only.");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_misc_rm.Text = "";
                lbl_gfolio.Text = "";
                lbl_gname.Text = "";
                cbo_charge.SelectedIndex = -1;
                txt_chargeamt.Text = "0.00";
                txt_prev_reading.Text = "";
                txt_cur_reading.Text = "";
                txt_prev_reading.Text = "";

                btn_save.Enabled = false;
                btn_cancel.Enabled = false;

                dtp_chgdate.ResetText();
                dgv_mischist.Rows.Clear();
            }
            catch (Exception) { }
        }

        private void txt_prev_reading_TextChanged(object sender, EventArgs e)
        {
            compute_reading();
        }

        private void txt_cur_reading_TextChanged(object sender, EventArgs e)
        {
            compute_reading();
        }

        private void compute_reading()
        {
            GlobalMethod gm = new GlobalMethod();


            if (String.IsNullOrEmpty(txt_prev_reading.Text) == false && String.IsNullOrEmpty(txt_cur_reading.Text) == false)
            {
                Double pre = gm.toNormalDoubleFormat(txt_prev_reading.Text);
                Double cur = gm.toNormalDoubleFormat(txt_cur_reading.Text);
                Double bill = 0.00;
                DateTime datetime = DateTime.Now;
                //String nowmonth = datetime.ToString("MMMM").ToLower();
                String nowmonth = dtp_chgdate.Value.ToString("MMMM").ToLower();/*datetime.ToString("MMMM").ToLower();*/;
                String chg_class = db.get_colval("charge", "chg_class", "chg_code='" + (cbo_charge.SelectedValue ?? "").ToString() + "'");
                String monthly = db.get_colval("charge","e_"+nowmonth,"chg_code='"+(cbo_charge.SelectedValue??"").ToString()+"'");
                Double month_charge = gm.toNormalDoubleFormat((monthly));
                Double total_bill = 0.00;
                Double tax_amnt = 0.00;
                Double tax = gm.toNormalDoubleFormat(db.get_colval("charge", "franchise_tax", "chg_code='" + (cbo_charge.SelectedValue ?? "").ToString() + "'"));
                
                try
                {
                    if (chg_class == "ELEC")
                    {
                        Double get_min = gm.toNormalDoubleFormat(db.get_colval("charge", "electricity_min", "chg_code='" + (cbo_charge.SelectedValue ?? "").ToString() + "'"));
                        Double get_min_charge = gm.toNormalDoubleFormat(db.get_colval("charge", "electricity_min_charge", "chg_code='" + (cbo_charge.SelectedValue ?? "").ToString() + "'")); ;
                        Double get_excess_charge = gm.toNormalDoubleFormat(db.get_colval("charge", "electricity_excess_charge", "chg_code='" + (cbo_charge.SelectedValue ?? "").ToString() + "'")); ;

                        bill = gm.get_ElectricityReadingRate(pre, cur, get_min, get_min_charge, get_excess_charge, month_charge);
                        tax_amnt = bill * tax;
                        total_bill = bill + tax_amnt;

                        txt_bill.Text = gm.toAccountingFormat(bill);
                        txt_tax.Text = gm.toAccountingFormat(tax_amnt);
                        txt_chargeamt.Text = gm.toAccountingFormat(total_bill);
                    }
                    else if (chg_class == "WATER")
                    {
                        Double get_min = gm.toNormalDoubleFormat(db.get_colval("charge", "water_min", "chg_code='" + (cbo_charge.SelectedValue ?? "").ToString() + "'"));
                        Double get_min_charge = gm.toNormalDoubleFormat(db.get_colval("charge", "water_min_charge", "chg_code='" + (cbo_charge.SelectedValue ?? "").ToString() + "'")); ;
                        Double get_excess_charge = gm.toNormalDoubleFormat(db.get_colval("charge", "water_excess_charge", "chg_code='" + (cbo_charge.SelectedValue ?? "").ToString() + "'")); ;
                        tax = 0.00;
                        bill = gm.get_WaterReadingRate(pre, cur, get_min, get_min_charge, get_excess_charge);
                        

                        txt_bill.Text = gm.toAccountingFormat(bill);
                        txt_tax.Text = gm.toAccountingFormat(tax);
                        txt_chargeamt.Text = gm.toAccountingFormat(bill);
                    }

                    
                }
                catch { }
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_mischist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void pictureBox26_Click(object sender, EventArgs e)
        {

        }

        private void txt_tax_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnl_rm01_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnl_rm02_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtp_chgdate_ValueChanged(object sender, EventArgs e)
        {
            compute_reading();
        }

        private void txt_bill_TextChanged(object sender, EventArgs e)
        {

        }
    }
}