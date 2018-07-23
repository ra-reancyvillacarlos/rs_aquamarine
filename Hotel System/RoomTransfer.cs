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
    public partial class RoomTransfer : Form
    {
        private InHouseGuest IHG;
        private newInhouse newIHG;
        private int frm_typ = 1;

        public RoomTransfer(InHouseGuest i)
        {
            InitializeComponent();
            IHG = i;
        }
        public RoomTransfer(newInhouse i)
        {
            InitializeComponent();
            newIHG = i;
        }
        private void RoomTransfer_Load(object sender, EventArgs e)
        {
            chk_sametyp.Checked = true;

            lbl_occ.Hide();
            lbl_govt_transf.Hide();
            lbl_netrate_transf.Hide();
            lbl_sc_transf.Hide();
            lbl_rtcode.Hide();
            lbl_disc_amt.Hide();
            lbl_disc_code.Hide();
        }

        public void set_data(int formtype, String gfno, String guest, String rom, String arr_dt, String dep_dt, String rmtype, String rate, String rt_code, String disc_code, String occ)
        {
            thisDatabase db = new thisDatabase();
            frm_typ = formtype;

            lbl_gfno.Text = gfno;
            lbl_gfname.Text = guest;
            lbl_rom.Text = rom;
            lbl_arr_dt.Text = arr_dt;
            lbl_dep_dt.Text = dep_dt;
            lbl_rmtyp.Text = rmtype;
            lbl_rate.Text = rate;
            lbl_rtcode.Text = rt_code;
            lbl_disc_code.Text = disc_code;
            lbl_occ.Text = occ;

            if (frm_typ == 1)
            {
                load_romtransfhist(gfno);
                load_rom(lbl_rom.Text, db.get_romtyp_code(lbl_rmtyp.Text), lbl_arr_dt.Text, lbl_dep_dt.Text);
            }
            else if (frm_typ == 2)
            {
                grp_availablelist.Text = "Room Rate List";
                grp_historylist.Text = "Upgrade/Downgrade Guest History";
                lbl_description.Text = "Upgrade/Downgrade to";
                btn_submit.Text = "Upgrade/Downgrade";
                chk_sametyp.Text = "Same Published Rate Type";
                lbl_type.Text = "Published Rate Type";

                load_rmrate();
            }
        }

        private void load_romtransfhist(String reg_num)
        {
            thisDatabase db = new thisDatabase();

            dgv_rmhistory.DataSource = db.QueryOnTableWithParams("rmtransfer", "rtrf_num AS \"Transfer No.\", fr_room AS \"From\", to_room AS \"To\", reason AS \"Reason To Transfer\", Remarks AS \"Remarks\", user_id AS \"Transfered By\", t_date AS \"Date\", t_time AS \"Time\"", "reg_num='" + reg_num + "'", "ORDER BY rtrf_num");
        }

        private void load_rmrate()
        {
            thisDatabase db = new thisDatabase();

            dgv_availablerooms.DataSource = db.get_romrate("");
        }
        //rom_except and typ_code can be blank string.
        private void load_rom(String rom_except, String typ_code, String arr_dt, String dep_dt)
        {
            DataTable dt_allrooms = new DataTable();
            DataTable dt_reserved = new DataTable();
            DataTable dt_occupied = new DataTable();
            thisDatabase db = new thisDatabase();

            //changed by: Reancy 05-09-2018 //updated again in 06 02 2018
            dt_allrooms = db.get_allroomExpZ(typ_code, "VC", null);
            dt_reserved = db.get_reservedroomExptZ(DateTime.Parse(arr_dt).ToString("yyyy-MM-dd"), DateTime.Parse(dep_dt).ToString("yyyy-MM-dd"), typ_code);
            dt_occupied = db.get_occupancyExptZ("rom_code, res_code, full_name, arr_date, dep_date", DateTime.Parse(arr_dt).ToString("yyyy-MM-dd"), DateTime.Parse(dep_dt).ToString("yyyy-MM-dd"), typ_code);

            DataRow[] drr, drrr;

            // removed by: Reancy (incorrect) 05-09-2018
            //remove reserved
            //if (dt_reserved.Rows.Count > 0)
            //{
            //    try
            //    {
            //        for (int r = 0; r < dt_reserved.Rows.Count; r++)
            //        {
            //            if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) <= 0 && Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) >= 0 && String.IsNullOrEmpty(dt_reserved.Rows[r]["arrived"].ToString()) == true)
            //            {
            //                //MessageBox.Show(dt_reserved.Rows[r]["arrived"].ToString() + " is not arrived.");
            //                //if equal nothing to do.
            //            }
            //            else if (Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).Equals(Convert.ToDateTime(arr_dt)))
            //            {
            //                //if equal nothing to do.
            //            }
            //            else if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).Equals(Convert.ToDateTime(dep_dt)))
            //            {
            //                //if equal nothing to do.
            //            }
            //            else
            //            {
            //                drr = dt_allrooms.Select("rom_code='" + dt_reserved.Rows[r]["rom_code"].ToString() + "'");

            //                if (drr.Length > 0)
            //                {
            //                    drr[0].Delete();
            //                    dt_allrooms.AcceptChanges();
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception) { }
            //}

            // remove reserved 2.0 by: Reancy 05-09-2018
            for (int r = 0; r < dt_reserved.Rows.Count; r++)
            {
                drr = dt_allrooms.Select("rom_code='" + dt_reserved.Rows[r]["rom_code"].ToString() + "'");

                if (drr.Length > 0)
                {
                    drr[0].Delete();
                    dt_allrooms.AcceptChanges();
                }
            }

            // removed by: Reancy (incorrect) 05-09-2018
            //remove occuppied
            //if (dt_occupied.Rows.Count > 0)
            //{
            //    try
            //    {
            //        //dgv_occ.DataSource = dt_occupied;

            //        for (int o = 0; o < dt_occupied.Rows.Count; o++)
            //        {
            //            if (Convert.ToDateTime(dt_occupied.Rows[o]["dep_date"].ToString()).Equals(Convert.ToDateTime(arr_dt)))
            //            {
            //                //if equal nothing to do. means to be available
            //            }
            //            else
            //            {
            //                drr = dt_allrooms.Select("rom_code='" + dt_occupied.Rows[o]["rom_code"].ToString() + "' ");

            //                if (drr.Length > 0)
            //                {
            //                    drr[0].Delete();
            //                    dt_allrooms.AcceptChanges();
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception) { }
            //}
            // remove reserved 2.0 by: Reancy 05-09-2018
            for (int r = 0; r < dt_occupied.Rows.Count; r++)
            {
                drrr = dt_allrooms.Select("rom_code='" + dt_occupied.Rows[r]["rom_code"].ToString() + "'");

                if (drrr.Length > 0)
                {
                    drrr[0].Delete();
                    dt_allrooms.AcceptChanges();
                }
            }


            dgv_availablerooms.DataSource = dt_allrooms;
        }

        private void chk_sametyp_CheckedChanged(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            if (frm_typ == 1)
            {
                if (chk_sametyp.Checked == true)
                {
                    load_rom(lbl_rom.Text, db.get_romtyp_code(lbl_rmtyp.Text), lbl_arr_dt.Text, lbl_dep_dt.Text);
                }
                else
                {
                    load_rom("", "", lbl_arr_dt.Text, lbl_dep_dt.Text);
                }
            }
            else if (frm_typ == 2)
            {
                db.get_romrate("");
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (IHG != null)
            {
                IHG.reset_modname();
                this.Close();
            }
            else if (newIHG != null)
            {
                newIHG.reset_modname();
                this.Close();
            }
        }

        private void dgv_availablerooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            int occ = 1;

            try
            {
                //modified by: Reancy 06 02 2018
                if (dgv_availablerooms[3, e.RowIndex].Value.ToString() == "OCC")
                {
                    MessageBox.Show("Room is Occupied");
                }
                else if (dgv_availablerooms[3, e.RowIndex].Value.ToString() == "OOO")
                {
                    MessageBox.Show("Room is Out of Order");
                }
                else if (dgv_availablerooms[3, e.RowIndex].Value.ToString() == "VD")
                {
                    MessageBox.Show("Room is Vacant Dirty");
                }
                else
                {
                    if (frm_typ == 2)
                    {
                        String rmrt = dgv_availablerooms[4, e.RowIndex].Value.ToString();

                        if (lbl_occ.Text == "Double")
                        {
                            rmrt = dgv_availablerooms[5, e.RowIndex].Value.ToString();
                            occ = 2;
                        }
                        else if (lbl_occ.Text == "Triple")
                        {
                            rmrt = dgv_availablerooms[6, e.RowIndex].Value.ToString();
                            occ = 3;
                        }
                        else if (lbl_occ.Text == "Quad")
                        {
                            rmrt = dgv_availablerooms[7, e.RowIndex].Value.ToString();
                            occ = 4;
                        }

                        lbl_rmtransfer.Text = dgv_availablerooms[0, e.RowIndex].Value.ToString();

                        //get room rate

                        lbl_rmrate_transfer.Text = rmrt; //db.get_roomrateamt(lbl_rtcode.Text, db.get_romtyp_code(dgv_availablerooms[2, e.RowIndex].Value.ToString()), occ).ToString("0.00");
                        lbl_rntyp_transfer.Text = dgv_availablerooms[2, e.RowIndex].Value.ToString();
                    }
                    else // frm_typ = 1
                    {
                        if (lbl_occ.Text == "Double")
                        {
                            occ = 2;
                        }
                        else if (lbl_occ.Text == "Triple")
                        {
                            occ = 3;
                        }
                        else if (lbl_occ.Text == "Quad")
                        {
                            occ = 4;
                        }

                        lbl_rmtransfer.Text = dgv_availablerooms[0, e.RowIndex].Value.ToString();
                        lbl_rmrate_transfer.Text = db.get_roomrateamt(lbl_rtcode.Text, db.get_romtyp_code(dgv_availablerooms[2, e.RowIndex].Value.ToString()), occ).ToString("0.00");
                        lbl_rntyp_transfer.Text = dgv_availablerooms[2, e.RowIndex].Value.ToString();
                    }
                }
            }
            catch (Exception) { }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String rtrf_num = db.get_pk("rtrf_num");
            String chg_num = db.get_pk("chg_num");
            String rm_typ_code = db.get_romtyp_code(lbl_rmtyp.Text);
            String rm_typ_code_transfer = db.get_romtyp_code(lbl_rntyp_transfer.Text);

            try
            {
                String col = "'" + rtrf_num + "', '" + lbl_gfno.Text + "', '" + lbl_rom.Text + "', '" + lbl_rmtransfer.Text + "', '" + rtxt_reason.Text + "', '" + rtxt_remark.Text + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "'";

                if (rtxt_reason.Text == "")
                {
                    MessageBox.Show("Pls specify the reason to transfer.");
                }
                else if (lbl_rmtransfer.Text == "")
                {
                    MessageBox.Show("Pls select room to transfer.");
                }
                else if (db.is_roomreserved(lbl_rmtransfer.Text))
                {
                    MessageBox.Show("Room already reserved.");
                }
                else if (db.get_romstatus(lbl_rmtransfer.Text) == "OCC")
                {
                    MessageBox.Show("Room still occupied");
                }
                else
                {
                    disp_computed_bill();

                    btn_submit.Enabled = false;

                    if (frm_typ == 2)
                    {
                        col = "'" + rtrf_num + "', '" + lbl_gfno.Text + "', '" + lbl_rom.Text + "', '" + lbl_rom.Text + "', '" + rtxt_reason.Text + "', '" + rtxt_remark.Text + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', 'TRUE'";

                        if (db.InsertOnTable("rmtransfer", "rtrf_num, reg_num, fr_room, to_room, reason, remarks, user_id, t_date, t_time, is_chgrate", col))
                        {
                            db.set_pkm99("rtrf_num", db.get_nextincrementlimitchar(rtrf_num, rtrf_num.Length));

                            col = "'" + chg_num + "', '" + lbl_gfno.Text + "', '" + lbl_gfname.Text + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + lbl_arr_dt.Text + "', '" + lbl_dep_dt.Text + "', '" + lbl_rom.Text + "', '" + rm_typ_code + "', '" + lbl_rate.Text + "', '" + lbl_rmtransfer.Text + "', '" + rm_typ_code_transfer + "', '" + lbl_rmrate_transfer.Text + "', '" + rtxt_remark.Text + "', '" + GlobalClass.username + "'";

                            db.InsertOnTable("gfchange", "chg_num, reg_num, full_name, t_date, t_time, arr_date, dep_date, f_rom, f_type, f_rate, t_rom, t_type, t_rate, remarks, user_id", col);

                            db.set_pkm99("chg_num", db.get_nextincrementlimitchar(chg_num, chg_num.Length));

                            db.UpdateOnTable("gfolio", "rom_code='" + lbl_rom.Text + "', typ_code='" + lbl_rmtransfer.Text + "', rate_code='" + lbl_rntyp_transfer.Text + "', rom_rate='" + lbl_netrate_transf.Text + "', govt_tax='" + lbl_govt_transf.Text + "', serv_chg='" + lbl_sc_transf.Text + "', disc_code='" + lbl_disc_code.Text + "', disc_pct='" + lbl_disc_amt.Text + "'", "reg_num='" + lbl_gfno.Text + "'");

                            UpdateRoomStatus urs = new UpdateRoomStatus();

                            urs.set_rmstatus("VD", lbl_rom.Text);
                            urs.set_rmstatus("OCC", lbl_rmtransfer.Text);

                            Report rpt = new Report("", "");

                            rpt.printprev_romtransferform(lbl_gfno.Text, chg_num, lbl_gfname.Text, lbl_rom.Text, lbl_rate.Text, lbl_rmtyp.Text, lbl_rom.Text, lbl_rmrate_transfer.Text, lbl_rmtransfer.Text, lbl_arr_dt.Text, lbl_dep_dt.Text, rtxt_reason.Text + " | " + rtxt_remark.Text, GlobalClass.user_fullname, "", "");

                            rpt.Show();
                            if (IHG != null)
                            {
                                IHG.reset_modname();
                                btn_submit.Enabled = false;
                                //this.Close();
                            }
                            else if (newIHG != null)
                            {
                                newIHG.reset_modname();
                                btn_submit.Enabled = false;
                                //this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error on Upgrading/Downgrading Room. ");
                        }
                    }
                    else
                    {
                        if (db.InsertOnTable("rmtransfer", "rtrf_num, reg_num, fr_room, to_room, reason, remarks, user_id, t_date, t_time", col))
                        {
                            db.set_pkm99("rtrf_num", db.get_nextincrementlimitchar(rtrf_num, rtrf_num.Length));

                            col = "'" + chg_num + "', '" + lbl_gfno.Text + "', '" + lbl_gfname.Text + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "', '" + lbl_arr_dt.Text + "', '" + lbl_dep_dt.Text + "', '" + lbl_rom.Text + "', '" + rm_typ_code + "', '" + lbl_rate.Text + "', '" + lbl_rmtransfer.Text + "', '" + rm_typ_code_transfer + "', '" + lbl_rmrate_transfer.Text + "', '" + rtxt_remark.Text + "', '" + GlobalClass.username + "'";

                            db.InsertOnTable("gfchange", "chg_num, reg_num, full_name, t_date, t_time, arr_date, dep_date, f_rom, f_type, f_rate, t_rom, t_type, t_rate, remarks, user_id", col);

                            db.set_pkm99("chg_num", db.get_nextincrementlimitchar(chg_num, chg_num.Length));

                            db.UpdateOnTable("gfolio", "rom_code='" + lbl_rmtransfer.Text + "', typ_code='" + rm_typ_code_transfer + "', rate_code='" + lbl_rtcode.Text + "', rom_rate='" + lbl_netrate_transf.Text + "', govt_tax='" + lbl_govt_transf.Text + "', serv_chg='" + lbl_sc_transf.Text + "', disc_code='" + lbl_disc_code.Text + "', disc_pct='" + lbl_disc_amt.Text + "'", "reg_num='" + lbl_gfno.Text + "'");

                            UpdateRoomStatus urs = new UpdateRoomStatus();

                            urs.set_rmstatus("VD", lbl_rom.Text);
                            urs.set_rmstatus("OCC", lbl_rmtransfer.Text);

                            Report rpt = new Report("", "");

                            rpt.printprev_romtransferform(lbl_gfno.Text, chg_num, lbl_gfname.Text, lbl_rom.Text, lbl_rate.Text, lbl_rmtyp.Text, lbl_rmtransfer.Text, lbl_rmrate_transfer.Text, lbl_rntyp_transfer.Text, lbl_arr_dt.Text, lbl_dep_dt.Text, rtxt_reason.Text + " | " + rtxt_remark.Text, GlobalClass.user_fullname, "", "");

                            rpt.Show();

                            if (IHG != null)
                            {
                                IHG.reset_modname();
                                btn_submit.Enabled = false;
                                //this.Close();
                            }
                            else if (newIHG != null)
                            {
                                newIHG.reset_modname();
                                btn_submit.Enabled = false;
                                // this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error on Transfering Room. ");
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Error on Transfering Room. " + er.Message);
            }

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
                if (lbl_occ.Text == "Double")
                {
                    occ = 2;
                }
                else if (lbl_occ.Text == "Triple")
                {
                    occ = 3;
                }
                else if (lbl_occ.Text == "Quad")
                {
                    occ = 4;
                }

                if (lbl_disc_code.Text != "")
                {
                    lessdiscount = db.get_discount(lbl_disc_code.Text);
                    issenior_disc = db.issenior_disc(lbl_disc_code.Text);
                    lbl_disc_amt.Text = db.get_discount(lbl_disc_code.Text).ToString("0.00");
                }

                grossamt = db.get_roomrateamt(lbl_rtcode.Text, db.get_romtyp_code(lbl_rntyp_transfer.Text), occ);

                lbl_netrate_transf.Text = db.get_netrate(grossamt, lessdiscount, lessdisc_amt).ToString("0.00");
                lbl_govt_transf.Text = db.get_tax(grossamt, lessdiscount, lessdisc_amt).ToString("0.00");
                lbl_sc_transf.Text = db.get_svccharge(grossamt, lessdiscount, lessdisc_amt).ToString("0.00");

                if (issenior_disc == true)
                {
                    Double db_rmrate = Convert.ToDouble(lbl_netrate_transf.Text) + Convert.ToDouble(lbl_sc_transf.Text);

                    lbl_govt_transf.Text = "0.00";
                }
            }
            catch (Exception)
            { }
        }

        private void dgv_rmhistory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_availablerooms_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
