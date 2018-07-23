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
    public partial class RoomAvailability : Form
    {
        String t_date = "";
        DateTime date_in;
        DateTime date_out;
        String schema = "";
        DataTable dt_reserved;
        DataTable dt_occupied;
        DataTable dt_outoforder;
        Boolean isbtnclick = false;

        public RoomAvailability()
        {
            InitializeComponent();
            dt_reserved = new DataTable();
            dt_occupied = new DataTable();
            dt_outoforder = new DataTable();
        }

        private void RoomAvailability_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            this.WindowState = FormWindowState.Maximized;
            t_date = db.get_systemdate("");
            schema = db.get_schema();
            

            dtp_chkin.Value = Convert.ToDateTime(db.get_systemdate(""));
            dtp_chkout.Value = Convert.ToDateTime(db.get_systemdate(""));

            date_in = dtp_chkin.Value;
            date_out = dtp_chkout.Value;

            load_roomtype();

            cbo_rmtype.SelectedIndex = -1;

            isbtnclick = false;
            load_listview();
        }

        private void load_roomtype()
        {
            try
            {
                thisDatabase db = new thisDatabase();
                DataTable dt = new DataTable();

                dt = db.get_roomtypExptZ();

                cbo_rmtype.DataSource = dt;
                cbo_rmtype.DisplayMember = "typ_desc";
                cbo_rmtype.ValueMember = "typ_code";

                DataRow newRow = dt.NewRow();

                newRow["typ_code"] = "*";
                newRow["typ_desc"] = "";

                dt.Rows.Add(newRow); 
                
                isbtnclick = false;
                load_listview();
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void load_listview()
        {
            try
            {
                date_in = dtp_chkin.Value;
                date_out = dtp_chkout.Value;

                thisDatabase db = new thisDatabase();
                DataTable dt = new DataTable();
                String datenamefrm = date_in.ToString("yyyy-MM-dd");
                String datenamefrm_text = date_in.ToString("MM/dd/yy");
                String rmtyp = "";
                int diff = 0;
                String cname = "";
                try
                {
                    for (int x=3; x < dgv_listview.Columns.Count - 1;)
                    {
                        cname = dgv_listview.Columns[x].Name;
                        dgv_listview.Columns.Remove(cname);
                    }
                }
                catch (Exception) { }

                load_room();

                dt_reserved = db.get_reservedroomExptZ(date_in.ToString("yyyy-MM-dd"), date_out.ToString("yyyy-MM-dd"), rmtyp);
                dt_occupied = db.get_occupancyExptZ("rom_code, res_code, full_name, arr_date, dep_date", date_in.ToString("yyyy-MM-dd"), date_out.ToString("yyyy-MM-dd"), rmtyp);
                dt_outoforder = db.get_statCodeRoom("OOO");

                if (isbtnclick == false)
                {
                    diff = (date_out.AddDays(30) - date_in.Date).Days;
                }
                else
                {
                    diff = (date_out.AddDays(1) - date_in.Date).Days;
                    isbtnclick = false;
                }
                // changed indexes for colums in listview by: Reancy 05-08-18
                for (int i = 1; i <= diff; i++)
                {
                    DataGridViewColumn dgv_col = (DataGridViewColumn)dgv_listview.Columns[3].Clone();
                    
                    if (i == 1)
                    {
                        datenamefrm_text = date_in.ToString("MM/dd/yy");
                        dgv_listview.Columns[3].Name = datenamefrm;
                        dgv_listview.Columns[3].HeaderText = datenamefrm_text;
                    }
                    else
                    {
                        dgv_col.Name = datenamefrm;
                        dgv_col.HeaderText = datenamefrm_text;
                        dgv_col.Width = 70;
                        dgv_col.ReadOnly = true;
                        dgv_listview.Columns.Add(dgv_col);
                    }

                    datenamefrm = date_in.AddDays(i).ToString("yyyy-MM-dd");
                    datenamefrm_text = date_in.AddDays(i).ToString("MM/dd/yy");
                }

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

            if (cbo_rmtype.SelectedIndex > -1)
            {
                WHERE = " AND r.typ_code='" + cbo_rmtype.SelectedValue.ToString() + "'";
            }

            try
            {
                String SQL = "SELECT r.rom_code, r.rom_desc, rt.typ_desc FROM " + schema + ".rooms r INNER JOIN " + schema + ".rtype rt ON rt.typ_code=r.typ_code WHERE rt.typ_code!='Z'" + WHERE + " ORDER BY r.rom_code ASC";
                dt = db.QueryBySQLCode(SQL);
                dgv_listview.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    DataGridViewRow dgv_row = (DataGridViewRow)dgv_listview.Rows[0].Clone();

                    dgv_row.Cells[0].Value = row[0].ToString();
                    dgv_row.Cells[1].Value = row[1].ToString();
                    dgv_row.Cells[2].Value = row[2].ToString();

                    dgv_listview.Rows.Add(dgv_row);
                }
            }
            catch (Exception) { }
        }

        private void load_stat_availability()
        {
            Color clr_reserved = Color.Yellow;
            Color clr_occupied = Color.Blue;
            Color clr_outOfOrder = Color.Purple;

            load_color(dt_occupied, clr_occupied);
            load_color(dt_reserved, clr_reserved);
            load_color(dt_outoforder, clr_outOfOrder);
        }

        private void load_color(DataTable dt, Color clr)
        {
            try
            {
                //DataRow[] dt2_chkrom;
                //String dcur_room = "";
                //DateTime dcur_date = new DateTime();
                //int c = 3;

                //foreach (DataGridViewRow dgv_row in dgv_listview.Rows)
                //{
                //    dcur_room = dgv_row.Cells[0].Value.ToString().Substring(0, 3);
                //    dcur_date = Convert.ToDateTime(dgv_listview.Columns[c].Name);
                //    dt2_chkrom = this.dt_occupied.Select("rom_code='" + dcur_room + "' AND arr_date <= '" + dcur_date.ToString("yyyy-MM-dd") + "' AND dep_date >= '" + dcur_date.ToString("yyyy-MM-dd") + "'");
                //    //dt2_chkrom = dt.Select("rom_code='" + dcur_room + "'");

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

        private void btn_enter_Click(object sender, EventArgs e)
        {
            isbtnclick = true;
            load_listview();
        }

        private void dgv_listview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //MessageBox.Show(this.dt_reserved.ToString());
            Color clr_reserved = Color.Yellow;
            Color clr_occupied = Color.Blue;
            Color clr_outoforder = Color.Purple;

            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.BackColor = Color.White;
            CellStyle.ForeColor = Color.Black;

            if (e.ColumnIndex == 0)
                dgv_listview.Rows[e.RowIndex].Cells[0].Style = CellStyle;
            else if (e.ColumnIndex == 1)
                dgv_listview.Rows[e.RowIndex].Cells[1].Style = CellStyle;
            else if (e.ColumnIndex == 2)
                dgv_listview.Rows[e.RowIndex].Cells[2].Style = CellStyle;

            //changed by: Reancy Villacarlos 05/05/18
            try
            {
                if (e.ColumnIndex > 2)
                {
                    String dcur_room = dgv_listview.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 3);
                    DateTime dcur_date = Convert.ToDateTime(dgv_listview.Columns[e.ColumnIndex].Name);
                    DataRow[] dt1_chkrom = this.dt_outoforder.Select("rom_code='" + dcur_room + "'");
                    DataRow[] dt2_chkrom = this.dt_reserved.Select("rom_code='" + dcur_room + "'");
                    DataRow[] dt3_chkrom = this.dt_occupied.Select("rom_code='" + dcur_room + "'");
                    //added by: Reancy Villacarlos 05/05/18
                    //out of order
                    if (dt1_chkrom.Length > 0)
                    {
                        foreach (DataRow drow in dt1_chkrom)
                        {
                            String rom_code_db = drow["rom_code"].ToString();

                            if (dgv_listview.Rows[e.RowIndex].Cells[0].Value.Equals(rom_code_db))
                                dgv_listview.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = clr_outoforder;
                        }
                    }
                    //added by: Reancy Villacarlos 05/05/18
                    //set reserved
                    if (dt2_chkrom.Length > 0)
                    {
                        foreach (DataRow drow in dt2_chkrom)
                        {
                            DateTime guest_arr = Convert.ToDateTime(drow["arr_date"].ToString());
                            DateTime guest_dep = Convert.ToDateTime(drow["dep_date"].ToString());

                            if (dcur_date >= guest_arr && dcur_date <= guest_dep)
                                dgv_listview.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = clr_reserved;
                        }
                    }
                    //added by: Reancy Villacarlos 05/05/18
                    //set occupied
                    if (dgv_listview.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == clr_reserved)
                    {

                    }
                    else
                    {
                        if (dt3_chkrom.Length > 0)
                        {
                            foreach (DataRow drow in dt3_chkrom)
                            {
                                DateTime guest_arr = Convert.ToDateTime(drow["arr_date"].ToString());
                                DateTime guest_dep = Convert.ToDateTime(drow["dep_date"].ToString());

                                if (dcur_date >= guest_arr && dcur_date <= guest_dep)
                                    dgv_listview.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = clr_occupied;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //
            }

            // removed by: Reancy 05162018
            ////set reserved
            //try
            //{
            //    if (e.ColumnIndex > 2)
            //    {
            //        String dcur_room = dgv_listview.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 3);
            //        DateTime dcur_date = Convert.ToDateTime(dgv_listview.Columns[e.ColumnIndex].Name);
            //        // Modified by: Reancy 05-05-2018
            //        //  AND " + dcur_date.ToString("yyyy-MM-dd") + " >= arr_date AND " + dcur_date.ToString("yyyy-MM-dd") + " <= dep_date
            //        DataRow[] dt2_chkrom = this.dt_reserved.Select("rom_code= '" + dcur_room + "'");

            //        //MessageBox.Show(dcur_date.ToString("yyyy-MM-dd") + " " + this.t_date);

            //        if (dt2_chkrom.Length > 0)
            //        {
            //            foreach (DataRow drow in dt2_chkrom)
            //            {
            //                DateTime guest_arr = Convert.ToDateTime(drow["arr_date"].ToString());
            //                DateTime guest_dep = Convert.ToDateTime(drow["dep_date"].ToString());

            //                if (dcur_date >= guest_arr && dcur_date <= guest_dep)
            //                    dgv_listview.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = clr_reserved;
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    // MessageBox.Show(er.Message);
            //}

            ////set occupied
            //try
            //{
            //    if (e.ColumnIndex > 2)
            //    {
            //        String dcur_room = dgv_listview.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 3);
            //        DataRow[] dt2_chkrom = this.dt_occupied.Select("rom_code='" + dcur_room + "'");

            //        if (dt2_chkrom.Length > 0)
            //        {
            //            DateTime dcur_date = Convert.ToDateTime(dgv_listview.Columns[e.ColumnIndex].Name);
            //            foreach (DataRow drow in dt2_chkrom)
            //            {
            //                DateTime guest_arr = Convert.ToDateTime(drow["arr_date"].ToString());
            //                DateTime guest_dep = Convert.ToDateTime(drow["dep_date"].ToString());

            //                if (dcur_date >= guest_arr && dcur_date <= guest_dep)
            //                    dgv_listview.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = clr_occupied;
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    //
            //}
        }

        private void dgv_listview_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {/*
            try
            {
                if (e.ColumnIndex > 2)
                {
                    dgv_listview.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Brown;
                }
            }
            catch (Exception) { }*/
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void RoomAvailability_Shown(object sender, EventArgs e)
        {

        }
    }
}
