using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Accounting_Application_System
{
    public partial class z_Notification : Form
    {
        thisDatabase db = new thisDatabase();

        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        String thisNotification = "";
        String thisFromWhatForm = "";
        String tt_date = "";
        DateTime date_in;
        DateTime date_out;
        String schema = "";
        DataTable dt_reserved;
        DataTable dt_occupied;
        Boolean isbtnclick = false;
        DataView v_glist;

        public z_Notification()
        {
            InitializeComponent();
            //gc.load_branch(cbo_nbranch);
            Hotel_System.GlobalMethod gm = new Hotel_System.GlobalMethod();
            Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            thisDatabase dbh = new thisDatabase();
            String startdate = Convert.ToDateTime(db.get_systemdate("")).Month.ToString() + "/1/" + Convert.ToDateTime(db.get_systemdate("")).Year.ToString();
            
            tt_date = dbh.get_systemdate("");
            schema = dbh.get_schema();
            v_glist = new DataView();

            //cbo_disfolio.SelectedIndex = 0;
            //cbo_balances.SelectedIndex = 0;

            //dis_dgvguest("");

            ////Start By: Roldan
            //dtp_sdate.Value = Convert.ToDateTime(startdate);
            //dtp_edate.Value = Convert.ToDateTime(db.get_systemdate(""));
            //dtp_travagnt_sdate.Value = Convert.ToDateTime(startdate);
            //dtp_travagnt_edate.Value = Convert.ToDateTime(db.get_systemdate(""));

            //gm.load_year(cbo_collectionfilteryear);
            //gm.load_year(cbo_occupancyfilteryear);


            tbcntrl_res.SelectedTab = tabPage3;
            tabPage3.Show();
            // End of by Roldan
            set_reslist(" WHERE res_date = '"+DateTime.Now.ToString("yyyy-MM-dd")+"'");
            this.Load += z_Notification_Load;
        }
        /*
        void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            toDisplay();
        }

        void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            toDisplay();
        }
        */

        private void z_Notification_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            //Start of By Roldan
            disp_dgvcollection();
            disp_dgv_occupancy();
            disp_dgv_market();
            disp_dgv_travagnt();

            disp_chart_collection();
            disp_chart_occupancy();
            disp_chart_market();
            disp_chart_travagnt();

            

            
            //End By: Roldan
            
            isbtnclick = false;
            load_listview();
        }
        //private void dis_dgvguest()
        //{
        //    Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();

        //    dgv_guestlist.DataSource = db.get_guest_currentlycheckin("");
        //}
        private void load_roomtype()
        {
            try
            {
                Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
                DataTable dt = new DataTable();

                dt = db.get_roomtypExptZ();

                //cbo_rmtype.DataSource = dt;
                //cbo_rmtype.DisplayMember = "typ_desc";
                //cbo_rmtype.ValueMember = "typ_code";

                DataRow newRow = dt.NewRow();

                newRow["typ_code"] = "*";
                newRow["typ_desc"] = "";

                dt.Rows.Add(newRow);
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }
        /*
        private void toDisplay()
        {
            DateTime currentDate = dtp_nfrm.Value.Date;
            DateTime nextDate = dtp_nto.Value.Date;
            int r = 0;
            DataTable dt = db.get_notification(currentDate, nextDate);

            try
            {
                dgv_list.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[r];

                    row.Cells["t_date"].Value = dt.Rows[i]["t_date"].ToString();
                    row.Cells["t_time"].Value = dt.Rows[i]["t_time"].ToString();
                    row.Cells["username"].Value = dt.Rows[i]["username"].ToString();
                    row.Cells["module"].Value = dt.Rows[i]["module"].ToString();
                    row.Cells["notification_text"].Value = dt.Rows[i]["notification_text"].ToString();
                }
            }
            catch
            {

            }
        }
        */
        public void saveNotification(String notification, String form)
        {
            Boolean success = false;
            this.thisNotification = notification;
            this.thisFromWhatForm = form;

            try
            {
                success = db.InsertOnTable("notification", "notification_text, username, module, t_date, t_time", "'" + thisNotification + "', '" + GlobalClass.username + "', '" + thisFromWhatForm + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + DateTime.Now.ToString("HH:mm") + "'");
            }
            catch { }
        }

        private void dtp_frm_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {

        }
        private void load_listview()
        {
            try
            {
                //date_in = dtp_chkin.Value;
                //date_out = dtp_chkout.Value;

                Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
                DataTable dt = new DataTable();
                String datenamefrm = date_in.ToString("yyyy-MM-dd");
                String datenamefrm_text = date_in.ToString("MM/dd/yy");
                String rmtyp = "";
                int diff = 0;
                String cname = "";

                //try
                //{
                //    for (int x = 3; x < dgv_listview.Columns.Count - 1; )
                //    {
                //        cname = dgv_listview.Columns[x].Name;
                //        dgv_listview.Columns.Remove(cname);
                //    }
                //}
                //catch (Exception) { }

                load_room();

                dt_reserved = db.get_reservedroomExptZ(date_in.ToString("yyyy-MM-dd"), date_out.ToString("yyyy-MM-dd"), rmtyp);
                dt_occupied = db.get_occupancyExptZ("rom_code, res_code, full_name, arr_date, dep_date", date_in.ToString("yyyy-MM-dd"), date_out.ToString("yyyy-MM-dd"), rmtyp);

                if (isbtnclick == false)
                {
                    diff = (date_out.AddDays(30) - date_in).Days;
                }
                else
                {
                    diff = (date_out.AddDays(1) - date_in.Date).Days;
                    isbtnclick = false;
                }

                //for (int i = 1; i <= diff; i++)
                //{
                //    DataGridViewColumn dgv_col = (DataGridViewColumn)dgv_listview.Columns[3].Clone();

                //    if (i == 1)
                //    {
                //        datenamefrm_text = date_in.ToString("MM/dd/yy");
                //        dgv_listview.Columns[3].Name = datenamefrm;
                //        dgv_listview.Columns[3].HeaderText = datenamefrm_text;
                //    }
                //    else
                //    {
                //        dgv_col.Name = datenamefrm;
                //        dgv_col.HeaderText = datenamefrm_text;
                //        dgv_col.Width = 70;
                //        dgv_col.ReadOnly = true;
                //        dgv_listview.Columns.Add(dgv_col);
                //    }

                //    datenamefrm = date_in.AddDays(i).ToString("yyyy-MM-dd");
                //    datenamefrm_text = date_in.AddDays(i).ToString("MM/dd/yy");
                //}

                load_stat_availability();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private void load_room()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            String WHERE = "";

            //if (cbo_rmtype.SelectedIndex > -1)
            //{
            //    WHERE = " AND r.typ_code='" + cbo_rmtype.SelectedValue.ToString() + "'";
            //}

            //try
            //{
            //    String SQL = "SELECT r.rom_desc, rt.typ_desc, r.stat_code FROM " + schema + ".rooms r INNER JOIN " + schema + ".rtype rt ON rt.typ_code=r.typ_code WHERE rt.typ_code!='Z'" + WHERE + " ORDER BY r.rom_code ASC";
            //    dt = db.QueryBySQLCode(SQL);
            //    dgv_listview.Rows.Clear();

            //    foreach (DataRow row in dt.Rows)
            //    {
            //        DataGridViewRow dgv_row = (DataGridViewRow)dgv_listview.Rows[0].Clone();

            //        dgv_row.Cells[0].Value = row[0].ToString();
            //        dgv_row.Cells[1].Value = row[1].ToString();
            //        dgv_row.Cells[2].Value = row[2].ToString();

            //        dgv_listview.Rows.Add(dgv_row);
            //    }
            //}
            //catch (Exception) { }
        }

        private void load_stat_availability()
        {
            Color clr_reserved = Color.Yellow;
            Color clr_occupied = Color.Blue;

            load_color(dt_reserved, clr_reserved);
            load_color(dt_occupied, clr_occupied);
        }

        private void load_color(DataTable dt, Color clr)
        {
            try
            {
                DataRow[] dt2_chkrom;
                String dcur_room = "";
                DateTime dcur_date = new DateTime();
                int c = 3;

                //foreach (DataGridViewRow dgv_row in dgv_listview.Rows)
                //{
                //    dcur_room = dgv_row.Cells[0].Value.ToString().Substring(0, 3);
                //    dcur_date = Convert.ToDateTime(dgv_listview.Columns[c].Name);
                //    //dt2_chkrom = this.dt_reserved.Select("rom_code='" + dcur_room + "' AND arr_date <= '" + dcur_date.ToString("yyyy-MM-dd") + "' AND dep_date >= '" + dcur_date.ToString("yyyy-MM-dd") + "'");
                //    dt2_chkrom = dt.Select("rom_code='" + dcur_room + "'");

                //    try
                //    {
                //        foreach (DataRow drow in dt2_chkrom)
                //        {
                //            DateTime guest_arr = Convert.ToDateTime(drow["arr_date"].ToString());
                //            DateTime guest_dep = Convert.ToDateTime(drow["dep_date"].ToString());

                //            c = 3;
                //            dcur_date = Convert.ToDateTime(dgv_listview.Columns[c].Name);

                //            if (dcur_date >= guest_arr || dcur_date <= guest_dep)
                //            {
                //                while (dcur_date.CompareTo(guest_dep) <= 0)
                //                {
                //                    //MessageBox.Show(dcur_room + dcur_date + " >= " + guest_arr);
                //                    if (dcur_date == guest_dep)
                //                    {
                //                        //MessageBox.Show(dcur_room + dcur_date + " >= " + guest_dep);
                //                    }
                //                    else if (dcur_date >= guest_arr)
                //                    {
                //                        dgv_row.Cells[c].Style.BackColor = clr;
                //                    }
                //                    c++;
                //                    dcur_date = Convert.ToDateTime(dgv_listview.Columns[c].Name);
                //                }
                //            }
                //        }
                //    }
                //    catch (Exception er) { }
                //}
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
            }
        }
        //Auto Loan Status
        public void disp_autoloanstatuslist()
        {
            //DataTable dt = db.QueryBySQLCode("SELECT financer_code,app_no,m06_finance_code,cust_name,status,m06_finance_name FROM rssys.autoloanfinancer ORDER BY financer_code ASC");

            //try { dgv_autoloanstatus.Rows.Clear(); }
            //catch (Exception) { }

            //try
            //{
            //    dgv_autoloanstatus.DataSource = dt;

            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message);
            //}
        }

        //Repair Order Status


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

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_autoloanstatus_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_rostatus_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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



        private void btn_nfilter_Click(object sender, EventArgs e)
        {

        }

        private void btn_new_Click(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void dgv_listview_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(txt_noofnights.Text) < 0)
            //{
            //    MessageBox.Show("No. of Nights must not less than zero.");
            //}
            //else
            //{
            //    load_rom_available();
            //}
        }

        private void load_rom_available()
        {
           // String typ_code = "";
           // DataTable dt_allrooms = new DataTable();
           // DataTable dt_reserved = new DataTable();
           // DataTable dt_occupied = new DataTable();

           // if (cbo_type.SelectedIndex == -1)
           // {
           //     typ_code = "";
           // }
           // else
           // {
           //     typ_code = cbo_type.SelectedValue.ToString();
           // }

           // Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();

           // dt_allrooms = db.get_allroomWithZ(typ_code, null, "");
           // dt_reserved = db.get_reservedroomExptZ(dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), typ_code);
           //// dt_occupied = db.get_occupancyExptZandgfolioRoom("rom_code, res_code, full_name, arr_date, dep_date", dtp_checkin.Value.ToString("yyyy-MM-dd"), dtp_checkout.Value.ToString("yyyy-MM-dd"), typ_code, lbl_rm.Text);

           // DataRow[] drr;

           // lbl_rom.Text = dt_allrooms.Rows.Count.ToString();
           // lbl_res.Text = dt_reserved.Rows.Count.ToString();
           // lbl_occ.Text = dt_occupied.Rows.Count.ToString();

           // //remove reserved
           // if (dt_reserved.Rows.Count > 0)
           // {
           //     try
           //     {
           //         for (int r = 0; r < dt_reserved.Rows.Count; r++)
           //         {
           //             //MessageBox.Show(dt_reserved.Rows[r]["dep_date"].ToString() + " - " + dtp_checkout.Value.ToString("yyyy-MM-dd") + " == " + Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).Equals(dtp_checkout.Value).ToString());
           //             //MessageBox.Show(dt_reserved.Rows[r]["rom_code"].ToString() + ":: " +db.get_systemdate() + " => " + Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate())).ToString() + " and " + Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate())).ToString() + " and " + String.IsNullOrEmpty(dt_reserved.Rows[r]["arrived"].ToString()).ToString());
           //             if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) <= 0 && Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).CompareTo(Convert.ToDateTime(db.get_systemdate(""))) >= 0 && String.IsNullOrEmpty(dt_reserved.Rows[r]["arrived"].ToString()) == true)
           //             {
           //                 //MessageBox.Show(dt_reserved.Rows[r]["arrived"].ToString() + " is not arrived.");
           //                 //if equal nothing to do.
           //             }
           //             else if (Convert.ToDateTime(dt_reserved.Rows[r]["dep_date"].ToString()).Equals(dtp_checkin.Value))
           //             {
           //                 //if equal nothing to do.
           //             }
           //             else if (Convert.ToDateTime(dt_reserved.Rows[r]["arr_date"].ToString()).Equals(dtp_checkout.Value))
           //             {
           //                 //if equal nothing to do.
           //             }
           //             else
           //             {
           //                 drr = dt_allrooms.Select("rom_code='" + dt_reserved.Rows[r]["rom_code"].ToString() + "'");

           //                 if (drr.Length > 0)
           //                 {
           //                     drr[0].Delete();
           //                     dt_allrooms.AcceptChanges();
           //                 }
           //             }
           //         }
           //     }
           //     catch (Exception) { }
            //}
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void dis_dgvguest(String cond)
        {
            //Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            //DataTable dt = new DataTable();

            //try
            //{
            //    DateTime curdate = Convert.ToDateTime(db.get_systemdate(""));

            //    dt = db.get_guest_currentlycheckin(cond);

            //    dgv_guestlist.Rows.Clear();

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataGridViewRow row = (DataGridViewRow)dgv_guestlist.Rows[0].Clone();

            //        row.Cells[0].Value = dt.Rows[i][0].ToString();
            //        row.Cells[1].Value = dt.Rows[i][1].ToString();
            //        row.Cells[2].Value = dt.Rows[i][2].ToString();
            //        row.Cells[3].Value = Convert.ToDateTime(dt.Rows[i][3].ToString()).ToString("MM/dd/yyyy");
            //        row.Cells[4].Value = Convert.ToDateTime(dt.Rows[i][4].ToString()).ToString("MM/dd/yyyy");
            //        row.Cells[5].Value = dt.Rows[i][5].ToString();
            //        row.Cells[6].Value = dt.Rows[i][6].ToString();
            //        row.Cells[7].Value = Convert.ToDateTime(dt.Rows[i][7].ToString()).ToString("MM/dd/yyyy");
            //        row.Cells[8].Value = Convert.ToDateTime(dt.Rows[i][8].ToString()).ToString("HH:mm");

            //        if (curdate >= Convert.ToDateTime(dt.Rows[i][4].ToString()))
            //        {
            //            //no color if empty block by column
            //            row.Cells[0].Style.ForeColor = Color.Red;
            //            row.Cells[1].Style.ForeColor = Color.Red;
            //            row.Cells[2].Style.ForeColor = Color.Red;
            //            row.Cells[3].Style.ForeColor = Color.Red;
            //            row.Cells[4].Style.ForeColor = Color.Red;
            //            row.Cells[5].Style.ForeColor = Color.Red;
            //            row.Cells[6].Style.ForeColor = Color.Red;
            //            row.Cells[7].Style.ForeColor = Color.Red;
            //            row.Cells[8].Style.ForeColor = Color.Red;
            //            dgv_guestlist.Rows.Add(row);
            //        }
            //        else
            //        {
            //            row.Cells[0].Style.ForeColor = Color.Black;
            //            row.Cells[1].Style.ForeColor = Color.Black;
            //            row.Cells[2].Style.ForeColor = Color.Black;
            //            row.Cells[3].Style.ForeColor = Color.Black;
            //            row.Cells[4].Style.ForeColor = Color.Black;
            //            row.Cells[5].Style.ForeColor = Color.Black;
            //            row.Cells[6].Style.ForeColor = Color.Black;
            //            row.Cells[7].Style.ForeColor = Color.Black;
            //            row.Cells[8].Style.ForeColor = Color.Black;
            //            dgv_guestlist.Rows.Add(row);
            //            //color if block by column is not empty
            //            //dgv_reslist.Rows[i] = (DataRowCollection)dt.Rows[i];                      
            //        }
            //    }
            //}
            //catch (Exception) { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String cond = "";

             
            //if (cbo_disfolio.Text == "In-house")
            //{

            //}
            //if (cbo_disfolio.Text == "Expected Departures")
            //{
            //    cond = cond + " AND gf.dep_date='" + db.get_systemdate("") + "'";
            //}
            //if (cbo_disfolio.Text == "Arrivals Today")
            //{
            //    cond = cond + " AND gf.arr_date='" + db.get_systemdate("") + "'";
            //}

            //if (cbo_balances.Text == "Zero Balances")
            //{
            //    cond = cond + " AND (SELECT SUM(cf1.amount) FROM " + db.get_schema() + ".chgfil cf1 WHERE cf1.reg_num=gf.reg_num)=0";
            //}
            //if (cbo_balances.Text == "Open Balances")
            //{
            //    cond = cond + " AND (SELECT SUM(cf1.amount) FROM " + db.get_schema() + ".chgfil cf1 WHERE cf1.reg_num=gf.reg_num)!=0";
            //}

            //if (cbo_contract.Text == "Within A Month")
            //{
            //    cond = cond + " AND (gf.dep_date - gf.t_date) >=" + 1 + " AND (gf.dep_date - gf.t_date) != 0 AND (gf.dep_date - gf.t_date) <= 60 AND gf.rmrttyp='M'";
            //}
            //if (cbo_contract.Text == "2 Months")
            //{
            //    cond = cond + " AND (gf.dep_date - gf.t_date) >=" + 60 + " AND (gf.dep_date - gf.t_date) != 30 AND (gf.dep_date - gf.t_date) <= 90 AND gf.rmrttyp='M'";
            //}
            //if (cbo_contract.Text == "3 Months")
            //{
            //    cond = cond + " AND (gf.dep_date - gf.t_date) >=" + 90 + " AND (gf.dep_date - gf.t_date) != 60 AND (gf.dep_date - gf.t_date) <= 120 AND gf.rmrttyp='M'";
            //}
            //if (cbo_contract.Text == "4 Months")
            //{
            //    cond = cond + " AND (gf.dep_date - gf.t_date) >=" + 120 + " AND (gf.dep_date - gf.t_date) != 90 AND (gf.dep_date - gf.t_date) <= 150 AND gf.rmrttyp='M'";
            //}
            //if (cbo_contract.Text == "5 Months")
            //{
            //    cond = cond + " AND (gf.dep_date - gf.t_date) >=" + 150 + " AND (gf.dep_date - gf.t_date) != 120 AND (gf.dep_date - gf.t_date) <= 180 AND gf.rmrttyp='M'";
            //}
            //if (cbo_contract.Text == "6 Months")
            //{
            //    cond = cond + " AND (gf.dep_date - gf.t_date) >=" + 180 + " AND (gf.dep_date - gf.t_date) != 150 AND (gf.dep_date - gf.t_date) <=210 AND gf.rmrttyp='M'";
            //}
            //dis_dgvguest(cond);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex = -1;
            //cbo_contract.SelectedIndex = -1;
            //dis_dgvguest("");
        }

        // UpdateRoomList

        /*private void set_roomstatuslist()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();

                dt = db.get_roomtypExptZ();

                cbo_rmtyp.DataSource = db.get_roomtypExptZ();

                cbo_rmtyp.DisplayMember = "typ_desc";
                cbo_rmtyp.ValueMember = "typ_code";
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }*/

        /*
        private void disp_dgv_rmstatuslist()
        {
            thisDatabase db = new thisDatabase();
            String rmstat = "";
            String rmtyp = "";

            if (cbo_rmstatus.SelectedIndex > -1)
            {
                if (cbo_rmstatus.Text == "Vacant Clean")
                {
                    rmstat = "VC";
                }
                else if (cbo_rmstatus.Text == "Vacant Dirty")
                {
                    rmstat = "VD";
                }
                else if (cbo_rmstatus.Text == "Occupied")
                {
                    rmstat = "OCC";
                }
                else if (cbo_rmstatus.Text == "Out Of Order")
                {
                    rmstat = "OOO";
                }
                else if (cbo_rmstatus.Text == "Function Room")
                {
                    rmstat = "FUN";
                }
                else if (cbo_rmstatus.Text == "Office")
                {
                    rmstat = "OFC";
                }
            }

            if (cbo_rmtyp.SelectedIndex > -1)
            {
                rmtyp = cbo_rmtyp.SelectedValue.ToString();
            }

            dgv_romstatus.DataSource = db.get_rmstatuslist(rmstat, rmtyp);
        }*/
        
        /*
        private void cbo_rmstatus_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            disp_dgv_rmstatuslist();
        }*/

        /*
        private void cbo_rmtyp_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            disp_dgv_rmstatuslist();
        }
         */

        private void dgv_guestlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Collection Table

        private void disp_dgvcollection()
        {
            /*
            Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();

            try
            {
                String collection_year = cbo_collectionfilteryear.SelectedValue.ToString();


                if (collection_year == "2017")
                {
                    DataTable dt = new DataTable();
                    dt = db.QueryBySQLCode("SELECT extract(year from to_date(sl.chg_date, 'YYYY-MM-DD')) as year, extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(t2.credit)) END as \"collection\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((SUM(t2.credit)/sum(amount)) * 100) END as \"collectionpct\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(amount)-sum(t2.credit)) END as \"uncollected\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((sum(amount)-sum(t2.credit))/SUM(sl.amount)*100) END as \"uncollectedpct\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_date BETWEEN '2016-11-01' AND '2017-10-31' GROUP BY year, month ORDER BY year, month");

                    string[] monthNames = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }; // month names

                    dgv_collection.Rows.Clear();

                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_collection.Rows.Add();
                        DataGridViewRow row = dgv_collection.Rows[i];

                        int m = Int32.Parse(dt.Rows[r]["month"].ToString());

                        DataTable dt_rent = new DataTable();
                        dt_rent = db.QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_code='RNT' AND extract(month from to_date(sl.chg_date, 'YYYY-MM-DD'))=" + m + " AND sl.chg_date BETWEEN '2016-11-01' AND '2017-10-31' GROUP BY month ORDER BY month");
                        DataTable dt_utilities = new DataTable();
                        dt_utilities = db.QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_code!='RNT' AND extract(month from to_date(sl.chg_date, 'YYYY-MM-DD'))=" + m + " AND sl.chg_date BETWEEN '2016-11-01' AND '2017-10-31' GROUP BY month ORDER BY month");


                        row.Cells["dgvl_year"].Value = dt.Rows[r]["year"].ToString();
                        row.Cells["dgvl_month"].Value = monthNames[m];
                        try { row.Cells["dgvl_rental"].Value = gm.toAccountingFormat(dt_rent.Rows[0]["total"].ToString()); }
                        catch (Exception) { }
                        try { row.Cells["dgvl_utilities"].Value = gm.toAccountingFormat(dt_utilities.Rows[0]["total"].ToString()); }
                        catch (Exception) { }
                        row.Cells["dgvl_billedtotal"].Value = gm.toAccountingFormat(dt.Rows[r]["total"].ToString());
                        row.Cells["dgvl_collected"].Value = gm.toAccountingFormat(dt.Rows[r]["collection"].ToString());
                        row.Cells["dgvl_pct"].Value = String.Format("{0}%", dt.Rows[r]["collectionpct"]);
                        row.Cells["dgvl_notcollected"].Value = gm.toAccountingFormat(dt.Rows[r]["uncollected"].ToString());
                        row.Cells["dgvl_notcollected_pct"].Value = String.Format("{0}%", dt.Rows[r]["uncollectedpct"]);

                        row.Cells["dgvl_notcollected"].Style.ForeColor = Color.Red;
                        row.Cells["dgvl_notcollected_pct"].Style.ForeColor = Color.Red;
                    }

                }

                else if (collection_year == "2018")
                {
                    DataTable dt = new DataTable();
                    dt = db.QueryBySQLCode("SELECT extract(year from to_date(sl.chg_date, 'YYYY-MM-DD')) as year, extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(t2.credit)) END as \"collection\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((SUM(t2.credit)/sum(amount)) * 100) END as \"collectionpct\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(amount)-sum(t2.credit)) END as \"uncollected\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((sum(amount)-sum(t2.credit))/SUM(sl.amount)*100) END as \"uncollectedpct\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_date BETWEEN '2017-11-01' AND '2018-10-31' GROUP BY year, month ORDER BY year, month");

                    string[] monthNames = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }; // month names

                    dgv_collection.Rows.Clear();

                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_collection.Rows.Add();
                        DataGridViewRow row = dgv_collection.Rows[i];

                        int m = Int32.Parse(dt.Rows[r]["month"].ToString());

                        DataTable dt_rent = new DataTable();
                        dt_rent = db.QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_code='RNT' AND extract(month from to_date(sl.chg_date, 'YYYY-MM-DD'))=" + m + " AND sl.chg_date BETWEEN '2017-11-01' AND '2018-10-31' GROUP BY month ORDER BY month");
                        DataTable dt_utilities = new DataTable();
                        dt_utilities = db.QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_code!='RNT' AND extract(month from to_date(sl.chg_date, 'YYYY-MM-DD'))=" + m + " AND sl.chg_date BETWEEN '2017-11-01' AND '2018-10-31' GROUP BY month ORDER BY month");


                        row.Cells["dgvl_year"].Value = dt.Rows[r]["year"].ToString();
                        row.Cells["dgvl_month"].Value = monthNames[m];
                        try { row.Cells["dgvl_rental"].Value = gm.toAccountingFormat(dt_rent.Rows[0]["total"].ToString()); }
                        catch (Exception) { }
                        try { row.Cells["dgvl_utilities"].Value = gm.toAccountingFormat(dt_utilities.Rows[0]["total"].ToString()); }
                        catch (Exception) { }
                        row.Cells["dgvl_billedtotal"].Value = gm.toAccountingFormat(dt.Rows[r]["total"].ToString());
                        row.Cells["dgvl_collected"].Value = gm.toAccountingFormat(dt.Rows[r]["collection"].ToString());
                        row.Cells["dgvl_pct"].Value = String.Format("{0}%", dt.Rows[r]["collectionpct"]);
                        row.Cells["dgvl_notcollected"].Value = gm.toAccountingFormat(dt.Rows[r]["uncollected"].ToString());
                        row.Cells["dgvl_notcollected_pct"].Value = String.Format("{0}%", dt.Rows[r]["uncollectedpct"]);

                        row.Cells["dgvl_notcollected"].Style.ForeColor = Color.Red;
                        row.Cells["dgvl_notcollected_pct"].Style.ForeColor = Color.Red;
                    }

                }

                else if (collection_year == "2019")
                {
                    DataTable dt = new DataTable();
                    dt = db.QueryBySQLCode("SELECT extract(year from to_date(sl.chg_date, 'YYYY-MM-DD')) as year, extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(t2.credit)) END as \"collection\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((SUM(t2.credit)/sum(amount)) * 100) END as \"collectionpct\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(amount)-sum(t2.credit)) END as \"uncollected\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((sum(amount)-sum(t2.credit))/SUM(sl.amount)*100) END as \"uncollectedpct\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_date BETWEEN '2018-11-01' AND '2019-10-31' GROUP BY year, month ORDER BY year, month");

                    string[] monthNames = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }; // month names

                    dgv_collection.Rows.Clear();

                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_collection.Rows.Add();
                        DataGridViewRow row = dgv_collection.Rows[i];

                        int m = Int32.Parse(dt.Rows[r]["month"].ToString());

                        DataTable dt_rent = new DataTable();
                        dt_rent = db.QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_code='RNT' AND extract(month from to_date(sl.chg_date, 'YYYY-MM-DD'))=" + m + " AND sl.chg_date BETWEEN '2018-11-01' AND '2019-10-31' GROUP BY month ORDER BY month");
                        DataTable dt_utilities = new DataTable();
                        dt_utilities = db.QueryBySQLCode("SELECT extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as \"month\", ROUND(SUM(sl.amount)) as \"total\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_code!='RNT' AND extract(month from to_date(sl.chg_date, 'YYYY-MM-DD'))=" + m + " AND sl.chg_date BETWEEN '2018-11-01' AND '2019-10-31' GROUP BY month ORDER BY month");


                        row.Cells["dgvl_year"].Value = dt.Rows[r]["year"].ToString();
                        row.Cells["dgvl_month"].Value = monthNames[m];
                        try { row.Cells["dgvl_rental"].Value = gm.toAccountingFormat(dt_rent.Rows[0]["total"].ToString()); }
                        catch (Exception) { }
                        try { row.Cells["dgvl_utilities"].Value = gm.toAccountingFormat(dt_utilities.Rows[0]["total"].ToString()); }
                        catch (Exception) { }
                        row.Cells["dgvl_billedtotal"].Value = gm.toAccountingFormat(dt.Rows[r]["total"].ToString());
                        row.Cells["dgvl_collected"].Value = gm.toAccountingFormat(dt.Rows[r]["collection"].ToString());
                        row.Cells["dgvl_pct"].Value = String.Format("{0}%", dt.Rows[r]["collectionpct"]);
                        row.Cells["dgvl_notcollected"].Value = gm.toAccountingFormat(dt.Rows[r]["uncollected"].ToString());
                        row.Cells["dgvl_notcollected_pct"].Value = String.Format("{0}%", dt.Rows[r]["uncollectedpct"]);

                        row.Cells["dgvl_notcollected"].Style.ForeColor = Color.Red;
                        row.Cells["dgvl_notcollected_pct"].Style.ForeColor = Color.Red;
                    }

                }

                
            }
            catch (Exception)
            { }
             * */

            Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();

            //try
            //{
            //    String collection_year = cbo_collectionfilteryear.SelectedValue.ToString();


            //    if (collection_year == "2017")
            //    {
            //        DataTable dt = new DataTable();
            //        dt = db.QueryBySQLCode("SELECT cs.fy as \"year\", cs.mo as \"month\", to_char(to_date(cs.date, 'YYYY-MM-DD'), 'Mon') as monthname, cs.rental as \"rental\", cs.utilities as \"utilities\", cs.collection as collection, cs.date as \"date\" FROM " + schema + ".collection_stat cs WHERE date BETWEEN '2016-11-01' AND '2017-10-31' ORDER BY year, month");

            //        dgv_collection.Rows.Clear();

            //        for (int r = 0; r < dt.Rows.Count; r++)
            //        {
            //            int i = dgv_collection.Rows.Add();
            //            DataGridViewRow row = dgv_collection.Rows[i];

            //            Double billedtotal = gm.toNormalDoubleFormat(dt.Rows[r]["rental"].ToString()) + gm.toNormalDoubleFormat(dt.Rows[r]["utilities"].ToString());
            //            Double collectionpct = (gm.toNormalDoubleFormat(dt.Rows[r]["collection"].ToString()) / billedtotal) * 100;
            //            Double notcollected = gm.toNormalDoubleFormat(dt.Rows[r]["collection"].ToString()) - billedtotal;
            //            Double notcollectedpct = (notcollected / billedtotal) * 100;

            //            row.Cells["dgvl_year"].Value = dt.Rows[r]["year"].ToString();
            //            row.Cells["dgvl_month"].Value = dt.Rows[r]["monthname"].ToString();
            //            row.Cells["dgvl_m"].Value = dt.Rows[r]["month"].ToString();
            //            row.Cells["dgvl_rental"].Value = gm.toAccountingFormat(dt.Rows[r]["rental"].ToString());
            //            row.Cells["dgvl_utilities"].Value = gm.toAccountingFormat(dt.Rows[r]["utilities"].ToString());
            //            row.Cells["dgvl_billedtotal"].Value = gm.toAccountingFormat(billedtotal);
            //            row.Cells["dgvl_collected"].Value = gm.toAccountingFormat(dt.Rows[r]["collection"].ToString());
            //            row.Cells["dgvl_pct"].Value = String.Format("{0}%", collectionpct);
            //            row.Cells["dgvl_notcollected"].Value = gm.toAccountingFormat(notcollected);
            //            row.Cells["dgvl_notcollected_pct"].Value = String.Format("{0}%", notcollectedpct);

            //            row.Cells["dgvl_notcollected"].Style.ForeColor = Color.Red;
            //            row.Cells["dgvl_notcollected_pct"].Style.ForeColor = Color.Red;
            //        }

            //    }
            //    else if (collection_year == "2018")
            //    {
            //        DataTable dt = new DataTable();
            //        dt = db.QueryBySQLCode("SELECT cs.fy as \"year\", cs.mo as \"month\", to_char(to_date(cs.date, 'YYYY-MM-DD'), 'Mon') as monthname, cs.rental as \"rental\", cs.utilities as \"utilities\", cs.collection as collection, cs.date as \"date\" FROM " + schema + ".collection_stat cs WHERE date BETWEEN '2017-11-01' AND '2018-10-31' ORDER BY year, month");

            //        dgv_collection.Rows.Clear();

            //        for (int r = 0; r < dt.Rows.Count; r++)
            //        {
            //            int i = dgv_collection.Rows.Add();
            //            DataGridViewRow row = dgv_collection.Rows[i];

            //            Double billedtotal = gm.toNormalDoubleFormat(dt.Rows[r]["rental"].ToString()) + gm.toNormalDoubleFormat(dt.Rows[r]["utilities"].ToString());
            //            Double collectionpct = (gm.toNormalDoubleFormat(dt.Rows[r]["collection"].ToString()) / billedtotal) * 100;
            //            Double notcollected = gm.toNormalDoubleFormat(dt.Rows[r]["collection"].ToString()) - billedtotal;
            //            Double notcollectedpct = (notcollected / billedtotal) * 100;

            //            row.Cells["dgvl_year"].Value = dt.Rows[r]["year"].ToString();
            //            row.Cells["dgvl_month"].Value = dt.Rows[r]["monthname"].ToString();
            //            row.Cells["dgvl_m"].Value = dt.Rows[r]["month"].ToString();
            //            row.Cells["dgvl_rental"].Value = gm.toAccountingFormat(dt.Rows[r]["rental"].ToString());
            //            row.Cells["dgvl_utilities"].Value = gm.toAccountingFormat(dt.Rows[r]["utilities"].ToString());
            //            row.Cells["dgvl_billedtotal"].Value = gm.toAccountingFormat(billedtotal);
            //            row.Cells["dgvl_collected"].Value = gm.toAccountingFormat(dt.Rows[r]["collection"].ToString());
            //            row.Cells["dgvl_pct"].Value = String.Format("{0}%", collectionpct);
            //            row.Cells["dgvl_notcollected"].Value = gm.toAccountingFormat(notcollected);
            //            row.Cells["dgvl_notcollected_pct"].Value = String.Format("{0}%", notcollectedpct);

            //            row.Cells["dgvl_notcollected"].Style.ForeColor = Color.Red;
            //            row.Cells["dgvl_notcollected_pct"].Style.ForeColor = Color.Red;
            //        }

            //    }
                
            //}
            //catch(Exception)
            //{ }
        }

        private void disp_chart_collection()
        {
            /*
            try
            {
                foreach (var seriess in chart_collection.Series)
                {
                    seriess.Points.Clear();
                }

                String collection_year = cbo_collectionfilteryear.SelectedValue.ToString();

                if(collection_year == "2017")
                {
                    DataTable dt = new DataTable();


                    dt = db.QueryBySQLCode("SELECT extract(year from to_date(sl.chg_date, 'YYYY-MM-DD')) as year, extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as m, to_char(to_date(sl.chg_date, 'YYYY-MM-DD'), 'Mon') as \"month\", ROUND(SUM(sl.amount)) as \"total\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(t2.credit)) END as \"collection\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((SUM(t2.credit)/sum(amount)) * 100) END as \"collectionpct\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(amount)-sum(t2.credit)) END as \"uncollected\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((sum(amount)-sum(t2.credit))/SUM(sl.amount)*100) END as \"uncollectedpct\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_date BETWEEN '2016-11-01' AND '2017-10-31' GROUP BY year, month, m ORDER BY year, m");

                    chart_collection.DataSource = dt;
                    chart_collection.Series["Total Billing"].XValueMember = "month";
                    chart_collection.Series["Actual Collection"].XValueMember = "month";
                    chart_collection.Series["Total Billing"].YValueMembers = "total";
                    chart_collection.Series["Actual Collection"].YValueMembers = "collection";
                    chart_collection.Series["Total Billing"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    chart_collection.Series["Actual Collection"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                }
                else if(collection_year == "2018")
                {
                    DataTable dt = new DataTable();


                    dt = db.QueryBySQLCode("SELECT extract(year from to_date(sl.chg_date, 'YYYY-MM-DD')) as year, extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as m, to_char(to_date(sl.chg_date, 'YYYY-MM-DD'), 'Mon') as \"month\", ROUND(SUM(sl.amount)) as \"total\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(t2.credit)) END as \"collection\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((SUM(t2.credit)/sum(amount)) * 100) END as \"collectionpct\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(amount)-sum(t2.credit)) END as \"uncollected\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((sum(amount)-sum(t2.credit))/SUM(sl.amount)*100) END as \"uncollectedpct\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_date BETWEEN '2017-11-01' AND '2018-10-31' GROUP BY year, month, m ORDER BY year, m");

                    chart_collection.DataSource = dt;
                    chart_collection.Series["Total Billing"].XValueMember = "month";
                    chart_collection.Series["Actual Collection"].XValueMember = "month";
                    chart_collection.Series["Total Billing"].YValueMembers = "total";
                    chart_collection.Series["Actual Collection"].YValueMembers = "collection";
                    chart_collection.Series["Total Billing"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    chart_collection.Series["Actual Collection"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                }

                else if (collection_year == "2019")
                {
                    DataTable dt = new DataTable();


                    dt = db.QueryBySQLCode("SELECT extract(year from to_date(sl.chg_date, 'YYYY-MM-DD')) as year, extract(month from to_date(sl.chg_date, 'YYYY-MM-DD')) as m, to_char(to_date(sl.chg_date, 'YYYY-MM-DD'), 'Mon') as \"month\", ROUND(SUM(sl.amount)) as \"total\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(t2.credit)) END as \"collection\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((SUM(t2.credit)/sum(amount)) * 100) END as \"collectionpct\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND(SUM(amount)-sum(t2.credit)) END as \"uncollected\", CASE WHEN ROUND(SUM(t2.credit)) is NULL THEN '0' ELSE ROUND((sum(amount)-sum(t2.credit))/SUM(sl.amount)*100) END as \"uncollectedpct\" FROM " + schema + ".soahdr s LEFT JOIN " + schema + ".soalne sl ON s.soa_code=sl.soa_code LEFT JOIN " + schema + ".tr02 t2 ON sl.soa_code=t2.invoice WHERE sl.chg_date BETWEEN '2018-11-01' AND '2019-10-31' GROUP BY year, month, m ORDER BY year, m");

                    chart_collection.DataSource = dt;
                    chart_collection.Series["Total Billing"].XValueMember = "month";
                    chart_collection.Series["Actual Collection"].XValueMember = "month";
                    chart_collection.Series["Total Billing"].YValueMembers = "total";
                    chart_collection.Series["Actual Collection"].YValueMembers = "collection";
                    chart_collection.Series["Total Billing"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    chart_collection.Series["Actual Collection"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                }

                
            }
            catch (Exception)
            { }
             * */

            try
            {
                //foreach (var seriess in chart_collection.Series)
                //{
                //    seriess.Points.Clear();
                //}

                //String collection_year = cbo_collectionfilteryear.SelectedValue.ToString();

                //if (collection_year == "2017")
                //{
                //    DataTable dt = new DataTable();


                //    dt = db.QueryBySQLCode("SELECT cs.fy as \"year\", cs.mo as \"month\", to_char(to_date(cs.date, 'YYYY-MM-DD'), 'Mon') as monthname, cs.rental+cs.utilities as \"total\", cs.collection as \"collection\" FROM " + schema + ".collection_stat cs WHERE date BETWEEN '2016-11-01' AND '2017-10-31' ORDER BY year, month");

                //    chart_collection.DataSource = dt;
                //    chart_collection.Series["Total Billing"].XValueMember = "monthname";
                //    chart_collection.Series["Actual Collection"].XValueMember = "monthname";
                //    chart_collection.Series["Total Billing"].YValueMembers = "total";
                //    chart_collection.Series["Actual Collection"].YValueMembers = "collection";
                //    chart_collection.Series["Total Billing"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //    chart_collection.Series["Actual Collection"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //}
                //else if (collection_year == "2018")
                //{
                //    DataTable dt = new DataTable();


                //    dt = db.QueryBySQLCode("SELECT cs.fy as \"year\", cs.mo as \"month\", to_char(to_date(cs.date, 'YYYY-MM-DD'), 'Mon') as monthname, cs.rental+cs.utilities as \"total\", cs.collection as \"collection\" FROM " + schema + ".collection_stat cs WHERE date BETWEEN '2017-11-01' AND '2018-10-31' ORDER BY year, month");

                //    chart_collection.DataSource = dt;
                //    chart_collection.Series["Total Billing"].XValueMember = "monthname";
                //    chart_collection.Series["Actual Collection"].XValueMember = "monthname";
                //    chart_collection.Series["Total Billing"].YValueMembers = "total";
                //    chart_collection.Series["Actual Collection"].YValueMembers = "collection";
                //    chart_collection.Series["Total Billing"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //    chart_collection.Series["Actual Collection"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //}
            }
            catch(Exception)
            { }
        }

        // Occupancy DataGrid by: Roldan
        private void disp_dgv_occupancy()
        {/*
            Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();

            try
            {
               String occupancy_year = cbo_occupancyfilteryear.SelectedValue.ToString();

                if(occupancy_year == "2017")
                {
                    DataTable dt = new DataTable();
                    dt = db.QueryBySQLCode("SELECT extract(month from c.chg_date) as \"month\", extract(year from c.chg_date) as \"year\", ROUND(SUM(g.rom_rate)) as \"total\", CASE WHEN ROUND(oss.series1) IS NULL THEN '0' ELSE ROUND(oss.series1) END as \"series1\" FROM " + schema + ".chgfil c LEFT JOIN " + schema + ".gfolio g ON c.rom_code=g.rom_code LEFT JOIN " + schema + ".occupanc_series_stat oss ON oss.fy=extract(year from c.chg_date) AND oss.mo=extract(month from c.chg_date) WHERE c.chg_date BETWEEN '2016-11-01' AND '2017-10-31' GROUP BY month, year, series1 ORDER BY year, month");

                    string[] monthNames = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }; // month names

                    dgv_occupancy.Rows.Clear();

                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_occupancy.Rows.Add();
                        DataGridViewRow row = dgv_occupancy.Rows[i];

                        int m = Int32.Parse(dt.Rows[r]["month"].ToString());

                        Double variance = gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString()) - gm.toNormalDoubleFormat(dt.Rows[r]["total"].ToString());


                        Double variancepct = (variance / gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString())) * 100;

                        row.Cells["dgvl_occ_year"].Value = dt.Rows[r]["year"].ToString();
                        row.Cells["dgvl_occ_month"].Value = monthNames[m];
                        row.Cells["dgvl_occ_series1"].Value = gm.toAccountingFormat(dt.Rows[r]["series1"].ToString());
                        row.Cells["dgvl_occ_series2"].Value = gm.toAccountingFormat(dt.Rows[r]["Total"].ToString());
                        row.Cells["dgvl_occ_var_amt"].Value = gm.toAccountingFormat(variance);
                        row.Cells["dgvl_occ_var_pct"].Value = String.Format("{0}%", variancepct.ToString()); //gm.toNormalDoubleFormat(variancepct.ToString()); 
                        row.Cells["dgvl_occ_m"].Value = dt.Rows[r]["month"].ToString();

                        row.Cells["dgvl_occ_var_amt"].Style.ForeColor = Color.Red;
                        row.Cells["dgvl_occ_var_pct"].Style.ForeColor = Color.Red;
                    }
                }
                else if (occupancy_year == "2018")
                {
                    DataTable dt = new DataTable();
                    dt = db.QueryBySQLCode("SELECT extract(month from c.chg_date) as \"month\", extract(year from c.chg_date) as \"year\", ROUND(SUM(g.rom_rate)) as \"total\", CASE WHEN ROUND(oss.series1) IS NULL THEN '0' ELSE ROUND(oss.series1) END as \"series1\" FROM " + schema + ".chgfil c LEFT JOIN " + schema + ".gfolio g ON c.rom_code=g.rom_code LEFT JOIN " + schema + ".occupanc_series_stat oss ON oss.fy=extract(year from c.chg_date) AND oss.mo=extract(month from c.chg_date) WHERE c.chg_date BETWEEN '2017-11-01' AND '2018-10-31' GROUP BY month, year, series1 ORDER BY year, month");

                    string[] monthNames = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }; // month names

                    dgv_occupancy.Rows.Clear();

                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_occupancy.Rows.Add();
                        DataGridViewRow row = dgv_occupancy.Rows[i];

                        int m = Int32.Parse(dt.Rows[r]["month"].ToString());

                        Double variance = gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString()) - gm.toNormalDoubleFormat(dt.Rows[r]["total"].ToString());


                        Double variancepct = (variance / gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString())) * 100;

                        row.Cells["dgvl_occ_year"].Value = dt.Rows[r]["year"].ToString();
                        row.Cells["dgvl_occ_month"].Value = monthNames[m];
                        row.Cells["dgvl_occ_series1"].Value = gm.toAccountingFormat(dt.Rows[r]["series1"].ToString());
                        row.Cells["dgvl_occ_series2"].Value = gm.toAccountingFormat(dt.Rows[r]["Total"].ToString());
                        row.Cells["dgvl_occ_var_amt"].Value = gm.toAccountingFormat(variance);
                        row.Cells["dgvl_occ_var_pct"].Value = String.Format("{0}%", variancepct.ToString()); //gm.toNormalDoubleFormat(variancepct.ToString()); 
                        row.Cells["dgvl_occ_m"].Value = dt.Rows[r]["month"].ToString();

                        row.Cells["dgvl_occ_var_amt"].Style.ForeColor = Color.Red;
                        row.Cells["dgvl_occ_var_pct"].Style.ForeColor = Color.Red;
                    }
                }
                else if (occupancy_year == "2019")
                {
                    DataTable dt = new DataTable();
                    dt = db.QueryBySQLCode("SELECT extract(month from c.chg_date) as \"month\", extract(year from c.chg_date) as \"year\", ROUND(SUM(g.rom_rate)) as \"total\", CASE WHEN ROUND(oss.series1) IS NULL THEN '0' ELSE ROUND(oss.series1) END as \"series1\" FROM " + schema + ".chgfil c LEFT JOIN " + schema + ".gfolio g ON c.rom_code=g.rom_code LEFT JOIN " + schema + ".occupanc_series_stat oss ON oss.fy=extract(year from c.chg_date) AND oss.mo=extract(month from c.chg_date) WHERE c.chg_date BETWEEN '2018-11-01' AND '2019-10-31' GROUP BY month, year, series1 ORDER BY year, month");

                    string[] monthNames = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }; // month names

                    dgv_occupancy.Rows.Clear();

                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_occupancy.Rows.Add();
                        DataGridViewRow row = dgv_occupancy.Rows[i];

                        int m = Int32.Parse(dt.Rows[r]["month"].ToString());

                        Double variance = gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString()) - gm.toNormalDoubleFormat(dt.Rows[r]["total"].ToString());


                        Double variancepct = (variance / gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString())) * 100;

                        row.Cells["dgvl_occ_year"].Value = dt.Rows[r]["year"].ToString();
                        row.Cells["dgvl_occ_month"].Value = monthNames[m];
                        row.Cells["dgvl_occ_series1"].Value = gm.toAccountingFormat(dt.Rows[r]["series1"].ToString());
                        row.Cells["dgvl_occ_series2"].Value = gm.toAccountingFormat(dt.Rows[r]["Total"].ToString());
                        row.Cells["dgvl_occ_var_amt"].Value = gm.toAccountingFormat(variance);
                        row.Cells["dgvl_occ_var_pct"].Value = String.Format("{0}%", variancepct.ToString()); //gm.toNormalDoubleFormat(variancepct.ToString()); 
                        row.Cells["dgvl_occ_m"].Value = dt.Rows[r]["month"].ToString();

                        row.Cells["dgvl_occ_var_amt"].Style.ForeColor = Color.Red;
                        row.Cells["dgvl_occ_var_pct"].Style.ForeColor = Color.Red;
                    }
                }

               
            }
            catch (Exception)
            { }
          * */
            Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();

            try
            {
                //String occupancy_year = cbo_occupancyfilteryear.SelectedValue.ToString();

                //if (occupancy_year == "2017")
                //{
                //    DataTable dt = new DataTable();
                //    dt = db.QueryBySQLCode("SELECT o.fy as \"year\", o.mo as \"month\", to_char(to_date(o.date, 'YYYY-MM-DD'), 'Mon') as monthname, o.series1 as \"series1\", o.series2 as \"series2\", o.date as \"date\" FROM " + schema + ".occupanc_series_stat o WHERE date BETWEEN '2016-11-01' AND '2017-10-31' ORDER BY year, month");

                //    dgv_occupancy.Rows.Clear();

                //    for (int r = 0; r < dt.Rows.Count; r++)
                //    {
                //        int i = dgv_occupancy.Rows.Add();
                //        DataGridViewRow row = dgv_occupancy.Rows[i];

                //        Double variance = gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString()) - gm.toNormalDoubleFormat(dt.Rows[r]["series2"].ToString());


                //        Double variancepct = (variance / gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString())) * 100;

                //        row.Cells["dgvl_occ_year"].Value = dt.Rows[r]["year"].ToString();
                //        row.Cells["dgvl_occ_month"].Value = dt.Rows[r]["monthname"].ToString();
                //        row.Cells["dgvl_occ_series1"].Value = gm.toAccountingFormat(dt.Rows[r]["series1"].ToString());
                //        row.Cells["dgvl_occ_series2"].Value = gm.toAccountingFormat(dt.Rows[r]["series2"].ToString());
                //        row.Cells["dgvl_occ_var_amt"].Value = gm.toAccountingFormat(variance);
                //        row.Cells["dgvl_occ_var_pct"].Value = String.Format("{0}%", variancepct.ToString()); //gm.toNormalDoubleFormat(variancepct.ToString()); 
                //        row.Cells["dgvl_occ_m"].Value = dt.Rows[r]["month"].ToString();

                //        row.Cells["dgvl_occ_var_amt"].Style.ForeColor = Color.Red;
                //        row.Cells["dgvl_occ_var_pct"].Style.ForeColor = Color.Red;
                //    }
                //}
                //else if (occupancy_year == "2018")
                //{
                //    DataTable dt = new DataTable();
                //    dt = db.QueryBySQLCode("SELECT o.fy as \"year\", o.mo as \"month\", to_char(to_date(o.date, 'YYYY-MM-DD'), 'Mon') as monthname, o.series1 as \"series1\", o.series2 as \"series2\", o.date as \"date\" FROM " + schema + ".occupanc_series_stat o WHERE date BETWEEN '2017-11-01' AND '2018-10-31' ORDER BY year, month");

                //    dgv_occupancy.Rows.Clear();

                //    for (int r = 0; r < dt.Rows.Count; r++)
                //    {
                //        int i = dgv_occupancy.Rows.Add();
                //        DataGridViewRow row = dgv_occupancy.Rows[i];

                //        Double variance = gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString()) - gm.toNormalDoubleFormat(dt.Rows[r]["series2"].ToString());


                //        Double variancepct = (variance / gm.toNormalDoubleFormat(dt.Rows[r]["series1"].ToString())) * 100;

                //        row.Cells["dgvl_occ_year"].Value = dt.Rows[r]["year"].ToString();
                //        row.Cells["dgvl_occ_month"].Value = dt.Rows[r]["monthname"].ToString();
                //        row.Cells["dgvl_occ_series1"].Value = gm.toAccountingFormat(dt.Rows[r]["series1"].ToString());
                //        row.Cells["dgvl_occ_series2"].Value = gm.toAccountingFormat(dt.Rows[r]["series2"].ToString());
                //        row.Cells["dgvl_occ_var_amt"].Value = gm.toAccountingFormat(variance);
                //        row.Cells["dgvl_occ_var_pct"].Value = String.Format("{0}%", variancepct.ToString()); //gm.toNormalDoubleFormat(variancepct.ToString()); 
                //        row.Cells["dgvl_occ_m"].Value = dt.Rows[r]["month"].ToString();

                //        row.Cells["dgvl_occ_var_amt"].Style.ForeColor = Color.Red;
                //        row.Cells["dgvl_occ_var_pct"].Style.ForeColor = Color.Red;
                //    }
                //}

            }
            catch(Exception)
            { }

        }

        // Occupancy Chart by: Roldan
        private void disp_chart_occupancy()
        {
            /*
            foreach (var seriess in chart_occupancy.Series)
            {
                seriess.Points.Clear();
            }

            String occupancy_year = cbo_occupancyfilteryear.SelectedValue.ToString();

            if(occupancy_year == "2017")
            {
                DataTable dt = new DataTable();

                dt = db.QueryBySQLCode("SELECT to_char(c.chg_date, 'Mon') as \"month\", extract(year from c.chg_date) as \"year\", extract(month from c.chg_date) as \"m\", ROUND(SUM(g.rom_rate)) as \"total\", CASE WHEN ROUND(oss.series1) IS NULL THEN '0' ELSE ROUND(oss.series1) END as \"series1\" FROM " + schema + ".chgfil c LEFT JOIN " + schema + ".gfolio g ON c.rom_code=g.rom_code LEFT JOIN " + schema + ".occupanc_series_stat oss ON oss.fy=extract(year from c.chg_date) AND oss.mo=extract(month from c.chg_date) WHERE c.chg_date BETWEEN '2016-11-01' AND '2017-10-31' GROUP BY month, year, series1, m ORDER BY year, m");

                chart_occupancy.DataSource = dt;
                chart_occupancy.Series["Projected Occupancy"].XValueMember = "month";
                chart_occupancy.Series["Actual Occupancy"].XValueMember = "month";
                chart_occupancy.Series["Projected Occupancy"].YValueMembers = "series1";
                chart_occupancy.Series["Actual Occupancy"].YValueMembers = "total";
                chart_occupancy.Series["Projected Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                chart_occupancy.Series["Actual Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            }
            else if (occupancy_year == "2018")
            {
                DataTable dt = new DataTable();

                dt = db.QueryBySQLCode("SELECT to_char(c.chg_date, 'Mon') as \"month\", extract(year from c.chg_date) as \"year\", extract(month from c.chg_date) as \"m\", ROUND(SUM(g.rom_rate)) as \"total\", CASE WHEN ROUND(oss.series1) IS NULL THEN '0' ELSE ROUND(oss.series1) END as \"series1\" FROM " + schema + ".chgfil c LEFT JOIN " + schema + ".gfolio g ON c.rom_code=g.rom_code LEFT JOIN " + schema + ".occupanc_series_stat oss ON oss.fy=extract(year from c.chg_date) AND oss.mo=extract(month from c.chg_date) WHERE c.chg_date BETWEEN '2017-11-01' AND '2018-10-31' GROUP BY month, year, series1, m ORDER BY year, m");

                chart_occupancy.DataSource = dt;
                chart_occupancy.Series["Projected Occupancy"].XValueMember = "month";
                chart_occupancy.Series["Actual Occupancy"].XValueMember = "month";
                chart_occupancy.Series["Projected Occupancy"].YValueMembers = "series1";
                chart_occupancy.Series["Actual Occupancy"].YValueMembers = "total";
                chart_occupancy.Series["Projected Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                chart_occupancy.Series["Actual Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            }
            else if (occupancy_year == "2019")
            {
                DataTable dt = new DataTable();

                dt = db.QueryBySQLCode("SELECT to_char(c.chg_date, 'Mon') as \"month\", extract(year from c.chg_date) as \"year\", extract(month from c.chg_date) as \"m\", ROUND(SUM(g.rom_rate)) as \"total\", CASE WHEN ROUND(oss.series1) IS NULL THEN '0' ELSE ROUND(oss.series1) END as \"series1\" FROM " + schema + ".chgfil c LEFT JOIN " + schema + ".gfolio g ON c.rom_code=g.rom_code LEFT JOIN " + schema + ".occupanc_series_stat oss ON oss.fy=extract(year from c.chg_date) AND oss.mo=extract(month from c.chg_date) WHERE c.chg_date BETWEEN '2018-11-01' AND '2019-10-31' GROUP BY month, year, series1, m ORDER BY year, m");

                chart_occupancy.DataSource = dt;
                chart_occupancy.Series["Projected Occupancy"].XValueMember = "month";
                chart_occupancy.Series["Actual Occupancy"].XValueMember = "month";
                chart_occupancy.Series["Projected Occupancy"].YValueMembers = "series1";
                chart_occupancy.Series["Actual Occupancy"].YValueMembers = "total";
                chart_occupancy.Series["Projected Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                chart_occupancy.Series["Actual Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            }
             * */
            try
            {
                //foreach (var seriess in chart_occupancy.Series)
                //{
                //    seriess.Points.Clear();
                //}

                //String occupancy_year = cbo_occupancyfilteryear.SelectedValue.ToString();

                //if (occupancy_year == "2017")
                //{
                //    DataTable dt = new DataTable();

                //    dt = db.QueryBySQLCode("SELECT o.fy as \"year\", o.mo as \"month\", to_char(to_date(o.date, 'YYYY-MM-DD'), 'Mon') as monthname, o.series1 as \"series1\", o.series2 as \"series2\", o.date as \"date\" FROM " + schema + ".occupanc_series_stat o WHERE date BETWEEN '2016-11-01' AND '2017-10-31' ORDER BY year, month");

                //    chart_occupancy.DataSource = dt;
                //    chart_occupancy.Series["Projected Occupancy"].XValueMember = "monthname";
                //    chart_occupancy.Series["Actual Occupancy"].XValueMember = "monthname";
                //    chart_occupancy.Series["Projected Occupancy"].YValueMembers = "series1";
                //    chart_occupancy.Series["Actual Occupancy"].YValueMembers = "series2";
                //    chart_occupancy.Series["Projected Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //    chart_occupancy.Series["Actual Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //}
                //else if (occupancy_year == "2018")
                //{
                //    DataTable dt = new DataTable();

                //    dt = db.QueryBySQLCode("SELECT o.fy as \"year\", o.mo as \"month\", to_char(to_date(o.date, 'YYYY-MM-DD'), 'Mon') as monthname, o.series1 as \"series1\", o.series2 as \"series2\", o.date as \"date\" FROM " + schema + ".occupanc_series_stat o WHERE date BETWEEN '2017-11-01' AND '2018-10-31' ORDER BY year, month");

                //    chart_occupancy.DataSource = dt;
                //    chart_occupancy.Series["Projected Occupancy"].XValueMember = "monthname";
                //    chart_occupancy.Series["Actual Occupancy"].XValueMember = "monthname";
                //    chart_occupancy.Series["Projected Occupancy"].YValueMembers = "series1";
                //    chart_occupancy.Series["Actual Occupancy"].YValueMembers = "series2";
                //    chart_occupancy.Series["Projected Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //    chart_occupancy.Series["Actual Occupancy"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //}
            }
            catch(Exception)
            { }
        }

        private void disp_calc_collection()
        {
            //int r = dgv_collection.CurrentRow.Index;

            //string rental = dgv_collection["dgvl_rental", r].Value.ToString();
            //rental = rental.Replace(",", "");
            //rental = rental.Replace(".00", "");

            //string utilities = dgv_collection["dgvl_utilities", r].Value.ToString();
            //utilities = utilities.Replace(",", "");
            //utilities = utilities.Replace(".00", "");

            //string collection = dgv_collection["dgvl_collected", r].Value.ToString();
            //collection = collection.Replace(",", "");
            //collection = collection.Replace(".00", "");

            //txt_cyear.Text = dgv_collection["dgvl_year", r].Value.ToString();
            //txt_cmonth.Text = dgv_collection["dgvl_month", r].Value.ToString();
            //txt_crental.Text = rental;
            //txt_cutilities.Text = utilities;
            //txt_ccollected.Text = collection;

            //tbcntrl_collectionstat.SelectTab(tpg_cupdate);
        }

        private void disp_calc_occupancy()
        {
            //txt_series1.Text = string.Empty;
            //txt_variance.Text = "0";
            //txt_variancepct.Text = "0";

            //int r = dgv_occupancy.CurrentRow.Index;
            //string series2 = dgv_occupancy["dgvl_occ_series2", r].Value.ToString();
            //series2 = series2.Replace(",", "");
            //series2 = series2.Replace(".00", "");

            //string series1 = dgv_occupancy["dgvl_occ_series1", r].Value.ToString();
            //series1 = series1.Replace(",", "");
            //series1 = series1.Replace(".00", "");

            //txt_series1.Text = series1;
            //txt_year.Text = dgv_occupancy["dgvl_occ_year", r].Value.ToString();
            //txt_month.Text = dgv_occupancy["dgvl_occ_month", r].Value.ToString();
            //txt_series2.Text = series2;

            //txt_year.Enabled = false;
            //txt_month.Enabled = false;
            //txt_variance.Enabled = false;
            //txt_variancepct.Enabled = false;

            //tbcntrl_main.SelectTab(tabPage2);
        }

        //Market by: Roldan 04/04/18
        public void disp_dgv_market()
        {
            /*
            Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            DataTable dt = db.get_market();

            clear_dgvmarket();

            try
            {
                for(int r=0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_market.Rows.Add();
                    DataGridViewRow row = dgv_market.Rows[i];

                    row.Cells["dgvmarket_date"].Value = dt.Rows[r]["date"].ToString();
                    row.Cells["dgvmarket_market"].Value = dt.Rows[r]["market"].ToString();
                    row.Cells["dgvmarket_amount"].Value = dt.Rows[r]["amount"].ToString();
                }
            }
            catch(Exception)
            { }*/

            //DateTime s_date = dtp_sdate.Value;
            //DateTime e_date = dtp_edate.Value;

            //String start_date = s_date.ToString("MM/dd/yyyy");
            //String end_date = e_date.ToString("MM/dd/yyyy");

            //Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            //DataTable dt = db.QueryBySQLCode("SELECT gf.mkt_code as \"marketcode\", m.mkt_desc as \"market\", SUM(gf.rom_rate) as \"amount\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".market m ON gf.mkt_code = m.mkt_code WHERE to_char(gf.arr_date, 'MM/dd/yyyy')>='" + start_date + "' AND to_char(gf.arr_date, 'MM/dd/yyyy')<='" + end_date + "' GROUP BY marketcode, market ORDER BY marketcode");

            //clear_dgvmarket();

            //try
            //{
            //    for (int r = 0; dt.Rows.Count > r; r++)
            //    {
            //        int i = dgv_market.Rows.Add();
            //        DataGridViewRow row = dgv_market.Rows[i];

            //        row.Cells["dgvmarket_market"].Value = dt.Rows[r]["market"].ToString();
            //        row.Cells["dgvmarket_amount"].Value = gm.toAccountingFormat(dt.Rows[r]["amount"].ToString());
            //    }
            //}
            //catch (Exception)
            //{ }


        }

        public void disp_dgv_travagnt()
        {
            //DateTime travagnt_s_date = dtp_travagnt_sdate.Value;
            //DateTime travagnt_e_date = dtp_travagnt_edate.Value;

            //String travagnt_start_date = travagnt_s_date.ToString("MM/dd/yyyy");
            //String travagnt_end_date = travagnt_e_date.ToString("MM/dd/yyyy");

            //Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            //DataTable dt_travagnt = db.QueryBySQLCode("SELECT gf.trv_code as \"travelcode\", t.trv_name as \"travel\", SUM(gf.rom_rate) as \"amount\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".travagnt t ON gf.trv_code = t.trv_code WHERE to_char(gf.arr_date, 'MM/dd/yyyy')>='" + travagnt_start_date + "' AND to_char(gf.arr_date, 'MM/dd/yyyy')<='" + travagnt_end_date + "' GROUP BY travelcode, travel ORDER BY travelcode");

            //clear_dgvtravagnt();

            //try
            //{
            //    for (int r = 0; dt_travagnt.Rows.Count > r; r++)
            //    {
            //        int i = dgv_travagnt.Rows.Add();
            //        DataGridViewRow row = dgv_travagnt.Rows[i];

            //        row.Cells["dgvtravagnt_travagnt"].Value = dt_travagnt.Rows[r]["travel"].ToString();
            //        row.Cells["dgvtravagnt_amount"].Value = gm.toAccountingFormat(dt_travagnt.Rows[r]["amount"].ToString());
            //    }
            //}
            //catch (Exception)
            //{ }


        }

        public void disp_chart_market()
        {
            //DateTime s_date = dtp_sdate.Value;
            //DateTime e_date = dtp_edate.Value;

            //String start_date = s_date.ToString("MM/dd/yyyy");
            //String end_date = e_date.ToString("MM/dd/yyyy");

            //Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            /*
            DataTable dt = db.QueryBySQLCode("SELECT gf.mkt_code as \"marketcode\", m.mkt_desc as \"market\", to_char(gf.arr_date, 'MM/dd/yyyy') as \"date\", SUM(gf.rom_rate) as \"amount\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".market m ON gf.mkt_code = m.mkt_code WHERE to_char(gf.arr_date, 'MM/dd/yyyy')='08/01/2017' GROUP BY marketcode, market, gf.arr_date ORDER BY gf.arr_date");*/
            //DataTable dt_marketseries = db.QueryBySQLCode("SELECT m.mkt_code as \"marketcode\", m.mkt_desc as \"market\" FROM " + schema + ".market m ORDER BY m.mkt_code");

            //DataTable dt_marketseries = db.QueryBySQLCode("SELECT gf.mkt_code as \"marketcode\", m.mkt_desc as \"market\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".market m ON gf.mkt_code = m.mkt_code WHERE m.mkt_desc != 'RENTPAD' AND to_char(gf.arr_date, 'MM/dd/yyyy')>='" + start_date + "' AND to_char(gf.arr_date, 'MM/dd/yyyy')<='" + end_date + "'  GROUP BY marketcode, market ORDER BY marketcode");

            /*
            chart_market.DataSource = dt;
            chart_market.Series["Market"].XValueMember = "market";
            chart_market.Series["Market"].YValueMembers = "amount";
            chart_market.Series["Market"].YValueType =System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;*/

            /*for (int r=0; dt_marketseries.Rows.Count > r; r++)
            {
                try
                {
                    string seriesname = dt_marketseries.Rows[r]["market"].ToString();
                    string mktcode = dt_marketseries.Rows[r]["marketcode"].ToString();

                    DataTable dt_seriesvalue = db.QueryBySQLCode("SELECT gf.mkt_code as \"marketcode\", m.mkt_desc as \"market\", SUM(gf.rom_rate) as \"amount\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".market m ON gf.mkt_code = m.mkt_code WHERE gf.mkt_code = '"+ mktcode +"' GROUP BY marketcode, market");

                    chart_market.Series.Add(seriesname);
                    chart_market.Series[seriesname].ChartType = SeriesChartType.Column;
                    chart_market.DataSource = dt_seriesvalue;
                    chart_market.Series[seriesname].XValueMember = "market";
                    chart_market.Series[seriesname].YValueMembers = "amount";
                    chart_market.Series[seriesname].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                }
                catch(Exception)
                { }
            }*/
            //chart_market.Series.Clear();

            //for (int r = 0; dt_marketseries.Rows.Count > r; r++)
            //{
            //    try
            //    {
            //        string seriesname = dt_marketseries.Rows[r]["market"].ToString();
            //        string mktcode = dt_marketseries.Rows[r]["marketcode"].ToString();

            //        DataTable dt_seriesvalue = db.QueryBySQLCode("SELECT gf.mkt_code as \"marketcode\", m.mkt_desc as \"market\", SUM(gf.rom_rate) as \"amount\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".market m ON gf.mkt_code = m.mkt_code WHERE gf.mkt_code = '" + mktcode + "' AND to_char(gf.arr_date, 'MM/dd/yyyy')>='" + start_date + "' AND to_char(gf.arr_date, 'MM/dd/yyyy')<='" + end_date + "' GROUP BY marketcode, market");
                    

            //        chart_market.Series.Add(seriesname);
            //        chart_market.Series[seriesname].ChartType = SeriesChartType.Column;
            //        chart_market.Series[seriesname].Points.AddXY("Market", dt_seriesvalue.Rows[0]["amount"].ToString());
            //        chart_market.Series[seriesname].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            //        //chart_market.Size = new Size(1000, 1000);
            //    }
            //    catch (Exception)
            //    { }
            //}

            
        }

        public void disp_chart_travagnt()
        {
            //DateTime travagnt_s_date = dtp_travagnt_sdate.Value;
            //DateTime travagnt_e_date = dtp_travagnt_edate.Value;

            //String travagnt_start_date = travagnt_s_date.ToString("MM/dd/yyyy");
            //String travagnt_end_date = travagnt_e_date.ToString("MM/dd/yyyy");

            //Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();


            //DataTable dt_travagnt = db.QueryBySQLCode("SELECT gf.trv_code as \"travelcode\", t.trv_name as \"travel\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".travagnt t ON gf.trv_code = t.trv_code WHERE to_char(gf.arr_date, 'MM/dd/yyyy')>='" + travagnt_start_date + "' AND to_char(gf.arr_date, 'MM/dd/yyyy')<='" + travagnt_end_date + "' GROUP BY travelcode, travel ORDER BY travelcode");

           
            //chart_travagnt.Series.Clear();

            //for (int r = 0; dt_travagnt.Rows.Count > r; r++)
            //{
            //    try
            //    {
            //        string seriesname = dt_travagnt.Rows[r]["travel"].ToString();
            //        string trvcode = dt_travagnt.Rows[r]["travelcode"].ToString();

            //        DataTable dt_seriesvalue = db.QueryBySQLCode("SELECT gf.trv_code as \"travelcode\", t.trv_name as \"travel\", SUM(gf.rom_rate) as \"amount\" FROM " + schema + ".gfolio gf LEFT JOIN " + schema + ".travagnt t ON gf.mkt_code = t.trv_code WHERE gf.trv_code = '"+ trvcode +"' AND to_char(gf.arr_date, 'MM/dd/yyyy')>='" + travagnt_start_date + "' AND to_char(gf.arr_date, 'MM/dd/yyyy')<='" + travagnt_end_date + "' GROUP BY travelcode, travel ORDER BY travelcode");


            //        chart_travagnt.Series.Add(seriesname);
            //        chart_travagnt.Series[seriesname].ChartType = SeriesChartType.Column;
            //        chart_travagnt.Series[seriesname].Points.AddXY("Travel Agency", dt_seriesvalue.Rows[0]["amount"].ToString());
            //        chart_travagnt.Series[seriesname].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            //        //chart_market.Size = new Size(1000, 1000);
            //    }
            //    catch (Exception)
            //    { }
            //}


        }

        public void clear_dgvmarket()
        {
            //try
            //{
            //    dgv_market.Rows.Clear();
            //}
            //catch(Exception)
            //{ }
        }

        public void clear_dgvtravagnt()
        {
            //try
            //{
            //    dgv_travagnt.Rows.Clear();
            //}
            //catch (Exception)
            //{ }
        }

        private void dgv_occupancy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btn_calculate_Click(object sender, EventArgs e)
        {
            //disp_calc();
            
        }

        private void txt_variance_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_series1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txt_series1.Text) && !string.IsNullOrEmpty(txt_series2.Text))
                //{
                //    Double var = gm.toNormalDoubleFormat(txt_series1.Text) - gm.toNormalDoubleFormat(txt_series2.Text);
                //    Double varpct = (var / gm.toNormalDoubleFormat(txt_series1.Text)) * 100;

                //    txt_variance.Text = (var).ToString();
                //    txt_variancepct.Text = (varpct).ToString();//(var).ToString(); 
                //}
            }
            catch(Exception)
            { }
            
            
        }

        private void txt_series2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txt_series1.Text) && !string.IsNullOrEmpty(txt_series2.Text))
                //{
                //    Double var = gm.toNormalDoubleFormat(txt_series1.Text) - gm.toNormalDoubleFormat(txt_series2.Text);
                //    Double varpct = (var / gm.toNormalDoubleFormat(txt_series1.Text)) * 100;

                //    txt_variance.Text = (var).ToString();
                //    txt_variancepct.Text = (varpct).ToString();//(var).ToString(); 
                //}
            }
            catch(Exception)
            { }
        }

        private void btn_occ_back_Click(object sender, EventArgs e)
        {
            //txt_series1.Text = string.Empty;
            //txt_variance.Text = "0";
            //txt_variancepct.Text = "0";

            //tbcntrl_main.SelectTab(tabPage1);
        }

        private void btn_occ_save_Click(object sender, EventArgs e)
        {
            //Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            //Boolean success = false;

            //int r = dgv_occupancy.CurrentRow.Index;

            //int month = Int32.Parse(dgv_occupancy["dgvl_occ_m", r].Value.ToString());
            //int year = Int32.Parse(txt_year.Text);
            //Double series1 = gm.toNormalDoubleFormat(txt_series1.Text);
            //Double series2 = gm.toNormalDoubleFormat(txt_series2.Text);

            //DataTable dt_check = db.QueryBySQLCode("SELECT os.oid FROM " + schema + ".occupanc_series_stat os WHERE os.fy='"+ year +"' AND os.mo='"+ month +"'");

            //String col = "", val = "";

            //if (String.IsNullOrEmpty(txt_series1.Text) && String.IsNullOrEmpty(txt_series2.Text))
            //{
            //    MessageBox.Show("Please input the required fields.");
            //}
            //else if (dt_check.Rows.Count > 0)
            //{
            //    col = "series1='" + series1 + "', series2='" + series2 + "'";

            //    if (db.UpdateOnTable("occupanc_series_stat", col, "oid='" + dt_check.Rows[0]["oid"].ToString() + "'"))
            //    {
            //        success = true;
            //        disp_dgv_occupancy();
            //        disp_chart_occupancy();
            //        tbcntrl_main.SelectTab(tabPage1);
            //    }
            //    else
            //    {
            //        success = false;
            //        MessageBox.Show("Failed on saving.");
            //    }
            //}
            //else
            //{

            //    col = "fy, mo, series1, series2";
            //    val = "'" + year + "','" + month + "','" + series1 + "','" + series2 + "'";

            //    if(db.InsertOnTable("occupanc_series_stat", col, val))
            //    {
            //        success = true;
            //        disp_dgv_occupancy();
            //        disp_chart_occupancy();
            //        tbcntrl_main.SelectTab(tabPage1);
            //    }
            //    else
            //    {
            //        success = false;
            //        MessageBox.Show("Failed on saving.");
            //    }

            //}

        }

        private void dgv_occupancy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            disp_calc_occupancy();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_daterange_Click(object sender, EventArgs e)
        {
            disp_chart_market();
            disp_dgv_market();
        }

        private void btn_travagnt_daterange_Click(object sender, EventArgs e)
        {
            disp_chart_travagnt();
            disp_dgv_travagnt();
        }

        private void cbo_collectionfilteryear_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void dgv_collection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { }

        private void cbo_collectionfilteryear_SelectedValueChanged(object sender, EventArgs e)
        {
            disp_chart_collection();
            disp_dgvcollection();
            
        }

        private void cbo_occupancyfilteryear_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_chart_occupancy();
            disp_dgv_occupancy();
        }

        private void btn_collectionyear_Click(object sender, EventArgs e)
        {
            //disp_chart_collection();
            //disp_dgvcollection();
        }

        private void btn_occupancyyear_Click(object sender, EventArgs e)
        {
            //disp_chart_occupancy();
            //disp_dgv_occupancy();
        }

        private void pnl_occratestat_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtp_sdate_ValueChanged(object sender, EventArgs e)
        {
            disp_chart_market();
            disp_dgv_market();
        }

        private void dtp_edate_ValueChanged(object sender, EventArgs e)
        {
            disp_chart_market();
            disp_dgv_market();
        }

        private void dtp_travagnt_sdate_ValueChanged(object sender, EventArgs e)
        {
            disp_chart_travagnt();
            disp_dgv_travagnt();
        }

        private void dtp_travagnt_edate_ValueChanged(object sender, EventArgs e)
        {
            disp_chart_travagnt();
            disp_dgv_travagnt();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void dgv_collection_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            disp_calc_collection();
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void tpg_cupdate_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //txt_cyear.Text = string.Empty;
            //txt_cmonth.Text = string.Empty;
            //txt_crental.Text = string.Empty;
            //txt_cutilities.Text = string.Empty;
            //txt_ccollected.Text = string.Empty;

            //tbcntrl_collectionstat.SelectTab(tpg_cmain);
        }

        private void txt_ctotalbill_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txt_ctotalbill.Text) && !string.IsNullOrEmpty(txt_ccollected.Text))
                //{
                //    Double uncollected = gm.toNormalDoubleFormat(txt_ccollected.Text) - gm.toNormalDoubleFormat(txt_ctotalbill.Text);
                //    Double pctcollected = (gm.toNormalDoubleFormat(txt_ccollected.Text) / gm.toNormalDoubleFormat(txt_ctotalbill.Text)) * 100;
                //    Double pctuncollected = (uncollected / gm.toNormalDoubleFormat(txt_ctotalbill.Text)) * 100;

                //    txt_cuncollected.Text = (uncollected).ToString();
                //    txt_cpctcollected.Text = (pctcollected).ToString();
                //    txt_cpctuncollected.Text = (pctuncollected).ToString();
                //}
            }
            catch (Exception)
            { }
        }

        private void txt_crental_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txt_crental.Text) && !string.IsNullOrEmpty(txt_cutilities.Text))
                //{
                //    Double totalbill = gm.toNormalDoubleFormat(txt_crental.Text) + gm.toNormalDoubleFormat(txt_cutilities.Text);

                //    txt_ctotalbill.Text = (totalbill).ToString();
                //}
            }
            catch (Exception)
            { }
        }

        private void txt_cutilities_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txt_crental.Text) && !string.IsNullOrEmpty(txt_cutilities.Text))
                //{
                //    Double totalbill = gm.toNormalDoubleFormat(txt_crental.Text) + gm.toNormalDoubleFormat(txt_cutilities.Text);

                //    txt_ctotalbill.Text = (totalbill).ToString();
                //}
            }
            catch (Exception)
            { }
        }

        private void txt_ccollected_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txt_ctotalbill.Text) && !string.IsNullOrEmpty(txt_ccollected.Text))
                //{
                //    Double uncollected = gm.toNormalDoubleFormat(txt_ccollected.Text) - gm.toNormalDoubleFormat(txt_ctotalbill.Text);
                //    Double pctcollected = (gm.toNormalDoubleFormat(txt_ccollected.Text) / gm.toNormalDoubleFormat(txt_ctotalbill.Text)) * 100;
                //    Double pctuncollected = (uncollected / gm.toNormalDoubleFormat(txt_ctotalbill.Text)) * 100;

                //    txt_cuncollected.Text = (uncollected).ToString();
                //    txt_cpctcollected.Text = (pctcollected).ToString();
                //    txt_cpctuncollected.Text = (pctuncollected).ToString();
                //}
            }
            catch (Exception)
            { }
        }

        private void txt_cuncollected_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_collect_save_Click(object sender, EventArgs e)
        {

            //Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            //Boolean success = false;

            //int r = dgv_collection.CurrentRow.Index;

            //int month = Int32.Parse(dgv_collection["dgvl_m", r].Value.ToString());
            //int year = Int32.Parse(txt_cyear.Text);
            //Double rental = gm.toNormalDoubleFormat(txt_crental.Text);
            //Double utilities = gm.toNormalDoubleFormat(txt_cutilities.Text);
            //Double collected = gm.toNormalDoubleFormat(txt_ccollected.Text);

            //DataTable dt_getid = db.QueryBySQLCode("SELECT c.oid FROM " + schema + ".collection_stat c WHERE c.fy='" + year + "' AND c.mo='" + month + "'");

            //String col = "", val = "";

            //    col = "rental='" + rental + "', utilities='" + utilities + "', collection='" + collected + "'";

            //    if (db.UpdateOnTable("collection_stat", col, "oid='" + dt_getid.Rows[0]["oid"].ToString() + "'"))
            //    {
            //        success = true;
            //        disp_dgvcollection();
            //        disp_chart_collection();
            //        tbcntrl_collectionstat.SelectTab(tpg_cmain);
            //    }
            //    else
            //    {
            //        success = false;
            //        MessageBox.Show("Failed on saving.");
            //    }
        }


        public void set_reslist(String search_code)
        {
            Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
            try
            {
                DataTable dt_curr = db.QueryBySQLCode("SELECT res_code, res_date, arr_time, full_name, pp.p_name, hl.name, COALESCE(SPLIT_PART(rf.occ_type, ', ', 4)) AS ttlpax, pck.package, pck1.activities, (CASE WHEN c_bool > 0 THEN true ELSE false END) AS cd, l_count AS lunch, res_date, reserv_by, remarks, arrived FROM rssys.resfil rf LEFT JOIN (SELECT name, code FROM rssys.hotel) hl ON hl.code = rf.hotel_code LEFT JOIN (SELECT string_agg(chg_desc, ', ') AS package, COALESCE(adult.occ_type, '0 ADULT') AS adult, COALESCE(kid.occ_type, '0 KID') AS kid, COALESCE(inf.occ_type, '0 INFANT') AS inf, res_gfil.rg_code FROM rssys.res_gfil LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE UPPER(occ_type) LIKE '%ADULT') adult ON adult.rg_code = res_gfil.rg_code LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE UPPER(occ_type) LIKE '%KID') kid ON kid.rg_code = res_gfil.rg_code LEFT JOIN (SELECT occ_type, rg_code FROM rssys.res_gfil WHERE UPPER(occ_type) LIKE '%INFANT') inf ON inf.rg_code = res_gfil.rg_code LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code WHERE res_gfil.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'PCK%') GROUP BY res_gfil.rg_code, adult.occ_type, kid.occ_type, inf.occ_type) pck ON pck.rg_code = rf.res_code LEFT JOIN (SELECT string_agg(chg_desc, ', ') AS activities, (occ_type) AS ttlpax, rg_code FROM rssys.res_gfil LEFT JOIN (SELECT chg_code, chg_desc FROM rssys.charge) cgh ON res_gfil.chg_code = cgh.chg_code WHERE res_gfil.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ACT%') AND UPPER(occ_type) LIKE '%ALL' GROUP BY rg_code, occ_type) pck1 ON pck1.rg_code = rf.res_code  LEFT JOIN (SELECT chg_desc AS p_name, chg_code FROM rssys.charge WHERE chg_type = 'P') pp ON pp.chg_code = p_typ LEFT JOIN (SELECT COUNT(rg.chg_code) AS l_count, rg_code FROM rssys.res_gfil rg WHERE rg.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ADTL%' AND UPPER(chg_desc) LIKE 'LUNCH%') GROUP BY rg_code LIMIT 1) l_b ON l_b.rg_code = rf.res_code LEFT JOIN (SELECT COUNT(rg.chg_code) AS c_bool, rg_code FROM rssys.res_gfil rg WHERE rg.chg_code IN (SELECT chg_code FROM rssys.charge WHERE UPPER(chg_code) LIKE 'ADTL%' AND UPPER(chg_desc) LIKE 'CAMERA%') GROUP BY rg_code LIMIT 1) c_b ON c_b.rg_code = rf.res_code" + search_code + "");
                DataTable dt_ld = db.QueryBySQLCode("SELECT COALESCE(SUM(COALESCE(SPLIT_PART(rf.occ_type, ', ', 1)::numeric(15,0), 0)), 0) AS adult, COALESCE(SUM(COALESCE(SPLIT_PART(rf.occ_type, ', ', 2)::numeric(15,0), 0)), 0) AS kid, COALESCE(SUM(COALESCE(SPLIT_PART(rf.occ_type, ', ', 3)::numeric(15,0), 0)), 0) AS inf, COALESCE(SUM(COALESCE(SPLIT_PART(rf.occ_type, ', ', 4)::numeric(15,0), 0)), 0) AS all FROM rssys.resfil rf");

                dgv_reslist.DataSource = dt_curr;

                label2.Text = dt_curr.Rows.Count.ToString();
                label3.Text = dt_ld.Rows[0]["adult"].ToString();
                label5.Text = dt_ld.Rows[0]["kid"].ToString();
                label7.Text = dt_ld.Rows[0]["inf"].ToString();
                label9.Text = ((dt_ld.Rows[0]["all"].ToString() == "0") ? (Convert.ToDouble(dt_ld.Rows[0]["adult"].ToString()) + Convert.ToDouble(dt_ld.Rows[0]["kid"].ToString()) + Convert.ToDouble(dt_ld.Rows[0]["inf"].ToString())).ToString() : dt_ld.Rows[0]["all"].ToString());

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void dgv_reslist_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            try
            {
                if (dgv_reslist["arrived", e.RowIndex].Value.ToString() == "Y")
                {
                    CellStyle.BackColor = Color.LimeGreen;
                    CellStyle.ForeColor = Color.Black;
                }
                else
                {
                    CellStyle.BackColor = Color.Red;
                    CellStyle.ForeColor = Color.White;
                }

                dgv_reslist.Rows[e.RowIndex].Cells["res_code"].Style = CellStyle;
            }
            catch { }
        }

       
    }
}
