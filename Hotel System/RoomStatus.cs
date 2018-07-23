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
    public partial class RoomStatus : Form
    {
        String t_date = "";
        DateTime date_in;
        DateTime date_out;
        String rmstatus = "";
        String schema = "";
        DataTable dt_reserved = null;
        DataTable dt_occupied = null;

        Boolean isbtnclick = false;

        public RoomStatus()
        {
            InitializeComponent();

        }

        private void RoomStatus_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            DateTime sysdate = Convert.ToDateTime(db.get_systemdate(""));
            t_date = sysdate.ToString("yyyy-MM-dd");


            //added by: Reancy 05-08-18
            int flr = 1;
            lbl_flr.Text = flr.ToString();
            lbl_top_dt_range.Text = t_date;

            schema = db.get_schema();
  
            set_topstatus();
            load_roomtype();

            cbo_rmtype.SelectedIndex = -1;
            reload();

            //added by: Reancy 05-08-18
            dtp_chkin.Value = Convert.ToDateTime(sysdate.ToString("yyyy-MM-dd"));
        }

        private void reload()
        {
            int flr = Convert.ToInt32(lbl_flr.Text);
            String drtyp = "";

            if(String.IsNullOrEmpty(cbo_rmtype.Text) == false)
                drtyp = cbo_rmtype.SelectedValue.ToString();

            load_lblstatusPerFlr();

           

            lbl_total_vc.Text = get_noofromByStatus(0, "VC", drtyp).ToString();
            lbl_total_vd.Text = get_noofromByStatus(0, "VD", drtyp).ToString();
            lbl_total_occ.Text = get_noofromByStatus(0, "OCC", drtyp).ToString();
            lbl_total_ooo.Text = get_noofromByStatus(0, "OOO", drtyp).ToString();
            lbl_total_reserved.Text = get_noofromByStatus(0, "R", drtyp).ToString();
            load_tableview(flr);
        }

        private void load_lblstatusPerFlr()
        {
            int flr = Convert.ToInt32(lbl_flr.Text);
            String drtyp = "";

            if (String.IsNullOrEmpty(cbo_rmtype.Text) == false)
                drtyp = cbo_rmtype.SelectedValue.ToString();

            lbl_flrstatus_vc.Text = get_noofromByStatus(flr, "VC", drtyp).ToString();
            lbl_flrstatus_vd.Text = get_noofromByStatus(flr, "VD", drtyp).ToString();
            lbl_flrstatus_occ.Text = get_noofromByStatus(flr, "OCC", drtyp).ToString();
            lbl_flrstatus_ooo.Text = get_noofromByStatus(flr, "OOO", drtyp).ToString();        
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
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void load_tableview(int pg)
        {
            String drtyp = "";

            //current status of the room
            try
            {
                tv_reset_form();
                
                String WHERE = "";

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

                if (String.IsNullOrEmpty(cbo_rmtype.Text) == false)
                {
                    drtyp = cbo_rmtype.SelectedValue.ToString();

                    drtyp = " AND r.typ_code='" + drtyp + "'";
                }
                 
                if (pg > 0)
                {
                    WHERE = " WHERE r.rom_code LIKE '" + pg + "%'" + drtyp + "" + rmstatus;
                }
               
                thisDatabase db = new thisDatabase();
                String SQL_room = "SELECT r.rom_code, r.rom_desc, r.typ_code, r.stat_code FROM "+schema+".rooms r"+ WHERE +" ORDER BY r.rom_code ASC";
                
                DataTable dt = db.QueryBySQLCode(SQL_room);
                int i = 1, x = 10;

                    foreach(DataRow row in dt.Rows)
                    {
                        set_panel(i, row["rom_code"].ToString(), row["rom_desc"].ToString() + "\n" + row["typ_code"].ToString(), row["stat_code"].ToString(), db.is_roomreservedByReancy(row["rom_code"].ToString(), dtp_chkin.Value.ToString("yyyy-MM-dd")));

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
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void set_topstatus()
        {
            thisDatabase db = new thisDatabase();
            DateTime sysdate = Convert.ToDateTime(db.get_systemdate(""));
            lbl_top_dt_range.Text = dtp_chkin.Value.ToString("MMMM dd, yyyy");
            
            if (cbo_rmstatus.Text == "")
                lbl_top_rm_status.Text = "All";
            else
                lbl_top_rm_status.Text = cbo_rmstatus.Text;

            if(cbo_rmtype.Text == "")
                lbl_top_rmtyp.Text = "All";
            else
                lbl_top_rmtyp.Text = cbo_rmtype.Text;
        }

        private void set_panel(int no, String rm, String rtyp, String status, Boolean isreserved)
        {
            Color stat = Color.Green;

            if (status == "VD")
            {
                stat = Color.Red;
            }
            else if (status == "OCC")
            {
                stat = Color.Blue;
                // added by: Reancy 05162018
                isreserved = false;
            }
            else if (status == "OOO")
            {
                stat = Color.Purple;
            }
            
            ///panels or rooms
            if (no == 1)
            {
                pnl_rm01.Show();

                lbl_rm01.Text = rm;
                lbl_rmstatus01.Text = rtyp;
                pnl_rm01.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres01.Show();
                }
            }
            else if (no == 2)
            {
                pnl_rm02.Show();

                lbl_rm02.Text = rm;
                lbl_rmstatus02.Text = rtyp;
                pnl_rm02.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres02.Show();
                }
            }
            else if (no == 3)
            {
                pnl_rm03.Show();

                lbl_rm03.Text = rm;
                lbl_rmstatus03.Text = rtyp;
                pnl_rm03.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres03.Show();
                }
            }
            else if (no == 4)
            {
                pnl_rm04.Show();

                lbl_rm04.Text = rm;
                lbl_rmstatus04.Text = rtyp;
                pnl_rm04.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres04.Show();
                }
            }
            else if (no == 5)
            {
                pnl_rm05.Show();

                lbl_rm05.Text = rm;
                lbl_rmstatus05.Text = rtyp;
                pnl_rm05.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres05.Show();
                }
            }
            else if (no == 6)
            {
                pnl_rm06.Show();

                lbl_rm06.Text = rm;
                lbl_rmstatus06.Text = rtyp;
                pnl_rm06.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres06.Show();
                }
            }
            else if (no == 7)
            {
                pnl_rm07.Show();

                lbl_rm07.Text = rm;
                lbl_rmstatus07.Text = rtyp;
                pnl_rm07.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres07.Show();
                }
            }
                       
            else if (no == 11)
            {
                pnl_rm11.Show();

                lbl_rm11.Text = rm;
                lbl_rmstatus11.Text = rtyp;
                pnl_rm11.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres11.Show();
                }
            }
            else if (no == 12)
            {
                pnl_rm12.Show();

                lbl_rm12.Text = rm;
                lbl_rmstatus12.Text = rtyp;
                pnl_rm12.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres12.Show();
                }
            }
            else if (no == 13)
            {
                pnl_rm13.Show();

                lbl_rm13.Text = rm;
                lbl_rmstatus13.Text = rtyp;
                pnl_rm13.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres13.Show();
                }
            }
            else if (no == 14)
            {
                pnl_rm14.Show();

                lbl_rm14.Text = rm;
                lbl_rmstatus14.Text = rtyp;
                pnl_rm14.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres14.Show();
                }
            }
            else if (no == 15)
            {
                pnl_rm15.Show();

                lbl_rm15.Text = rm;
                lbl_rmstatus15.Text = rtyp;
                pnl_rm15.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres15.Show();
                }

            }
            else if (no == 16)
            {
                pnl_rm16.Show();

                lbl_rm16.Text = rm;
                lbl_rmstatus16.Text = rtyp;
                pnl_rm16.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres16.Show();
                }
            }
            else if (no == 17)
            {
                pnl_rm17.Show();

                lbl_rm17.Text = rm;
                lbl_rmstatus17.Text = rtyp;
                pnl_rm17.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres17.Show();
                }
            }
            
            else if (no == 21)
            {
                pnl_rm21.Show();

                lbl_rm21.Text = rm;
                lbl_rmstatus21.Text = rtyp;
                pnl_rm21.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres21.Show();
                }
            }
            else if (no == 22)
            {
                pnl_rm22.Show();

                lbl_rm22.Text = rm;
                lbl_rmstatus22.Text = rtyp;
                pnl_rm22.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres22.Show();
                }
            }
            else if (no == 23)
            {
                pnl_rm23.Show();

                lbl_rm23.Text = rm;
                lbl_rmstatus23.Text = rtyp;
                pnl_rm23.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres23.Show();
                }
            }
            else if (no == 24)
            {
                pnl_rm24.Show();

                lbl_rm24.Text = rm;
                lbl_rmstatus24.Text = rtyp;
                pnl_rm24.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres24.Show();
                }
            }
            else if (no == 25)
            {
                pnl_rm25.Show();

                lbl_rm25.Text = rm;
                lbl_rmstatus25.Text = rtyp;
                pnl_rm25.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres25.Show();
                }
            }
            else if (no == 26)
            {
                pnl_rm26.Show();

                lbl_rm26.Text = rm;
                lbl_rmstatus26.Text = rtyp;
                pnl_rm26.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres26.Show();
                }
            }
            else if (no == 27)
            {
                pnl_rm27.Show();

                lbl_rm27.Text = rm;
                lbl_rmstatus27.Text = rtyp;
                pnl_rm27.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres27.Show();
                }
            }

            else if (no == 31)
            {
                pnl_rm31.Show();

                lbl_rm31.Text = rm;
                lbl_rmstatus31.Text = rtyp;
                pnl_rm31.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres31.Show();
                }
            }
            else if (no == 32)
            {
                pnl_rm32.Show();

                lbl_rm32.Text = rm;
                lbl_rmstatus32.Text = rtyp;
                pnl_rm32.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres32.Show();
                }
            }
            else if (no == 33)
            {
                pnl_rm33.Show();

                lbl_rm33.Text = rm;
                lbl_rmstatus33.Text = rtyp;
                pnl_rm33.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres33.Show();
                }
            }
            else if (no == 34)
            {
                pnl_rm34.Show();

                lbl_rm34.Text = rm;
                lbl_rmstatus34.Text = rtyp;
                pnl_rm34.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres34.Show();
                }
            }
            else if (no == 35)
            {
                pnl_rm35.Show();

                lbl_rm35.Text = rm;
                lbl_rmstatus35.Text = rtyp;
                pnl_rm35.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres35.Show();
                }
            }
            else if (no == 36)
            {
                pnl_rm36.Show();

                lbl_rm36.Text = rm;
                lbl_rmstatus36.Text = rtyp;
                pnl_rm36.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres36.Show();
                }
            }
            else if (no == 37)
            {
                pnl_rm37.Show();

                lbl_rm37.Text = rm;
                lbl_rmstatus37.Text = rtyp;
                pnl_rm37.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres37.Show();
                }
            }

            else if (no == 41)
            {
                pnl_rm41.Show();

                lbl_rm41.Text = rm;
                lbl_rmstatus41.Text = rtyp;
                pnl_rm41.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres41.Show();
                }
            }
            else if (no == 42)
            {
                pnl_rm42.Show();

                lbl_rm42.Text = rm;
                lbl_rmstatus42.Text = rtyp;
                pnl_rm42.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres42.Show();
                }
            }
            else if (no == 43)
            {
                pnl_rm43.Show();

                lbl_rm43.Text = rm;
                lbl_rmstatus43.Text = rtyp;
                pnl_rm43.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres43.Show();
                }
            }
            else if (no == 44)
            {
                pnl_rm44.Show();

                lbl_rm44.Text = rm;
                lbl_rmstatus44.Text = rtyp;
                pnl_rm44.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres44.Show();
                }
            }
            else if (no == 45)
            {
                pnl_rm45.Show();

                lbl_rm45.Text = rm;
                lbl_rmstatus45.Text = rtyp;
                pnl_rm45.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres45.Show();
                }
            }
            else if (no == 46)
            {
                pnl_rm46.Show();

                lbl_rm46.Text = rm;
                lbl_rmstatus46.Text = rtyp;
                pnl_rm46.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres46.Show();
                }
            }
            else if (no == 47)
            {
                pnl_rm47.Show();

                lbl_rm47.Text = rm;
                lbl_rmstatus47.Text = rtyp;
                pnl_rm47.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres47.Show();
                }
            }

            else if (no == 51)
            {
                pnl_rm51.Show();

                lbl_rm51.Text = rm;
                lbl_rmstatus51.Text = rtyp;
                pnl_rm51.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres51.Show();
                }
            }
            else if (no == 52)
            {
                pnl_rm52.Show();

                lbl_rm52.Text = rm;
                lbl_rmstatus52.Text = rtyp;
                pnl_rm52.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres52.Show();
                }
            }
            else if (no == 53)
            {
                pnl_rm53.Show();

                lbl_rm53.Text = rm;
                lbl_rmstatus53.Text = rtyp;
                pnl_rm53.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres53.Show();
                }
            }
            else if (no == 54)
            {
                pnl_rm54.Show();

                lbl_rm54.Text = rm;
                lbl_rmstatus54.Text = rtyp;
                pnl_rm54.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres54.Show();
                }
            }
            else if (no == 55)
            {
                pnl_rm55.Show();

                lbl_rm55.Text = rm;
                lbl_rmstatus55.Text = rtyp;
                pnl_rm55.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres55.Show();
                }
            }
            else if (no == 56)
            {
                pnl_rm56.Show();

                lbl_rm56.Text = rm;
                lbl_rmstatus56.Text = rtyp;
                pnl_rm56.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres56.Show();
                }
            }
            else if (no == 57)
            {
                pnl_rm57.Show();

                lbl_rm57.Text = rm;
                lbl_rmstatus57.Text = rtyp;
                pnl_rm57.BackColor = stat;

                if (isreserved == true)
                {
                    lbl_rmres57.Show();
                }
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

            pnl_rm41.Hide();
            lbl_rmres41.Hide();
            pnl_rm42.Hide();
            lbl_rmres42.Hide();
            pnl_rm43.Hide();
            lbl_rmres43.Hide();
            pnl_rm44.Hide();
            lbl_rmres44.Hide();
            pnl_rm45.Hide();
            lbl_rmres45.Hide();
            pnl_rm46.Hide();
            lbl_rmres46.Hide();
            pnl_rm47.Hide();
            lbl_rmres47.Hide();

            pnl_rm51.Hide();
            lbl_rmres51.Hide();
            pnl_rm52.Hide();
            lbl_rmres52.Hide();
            pnl_rm53.Hide();
            lbl_rmres53.Hide();
            pnl_rm54.Hide();
            lbl_rmres54.Hide();
            pnl_rm55.Hide();
            lbl_rmres55.Hide();
            pnl_rm56.Hide();
            lbl_rmres56.Hide();
            pnl_rm57.Hide();
            lbl_rmres57.Hide();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(lbl_flr.Text);

            i++;

            lbl_flr.Text = i.ToString();

            load_lblstatusPerFlr();

            load_tableview(i);
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(lbl_flr.Text);

            i--;

            if (i > 0)
            {

                lbl_flr.Text = i.ToString();

                load_lblstatusPerFlr();
                
                load_tableview(i);
            }
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            rmstatus = cbo_rmstatus.Text;

            isbtnclick = true;
            set_topstatus();
            reload();
        }

        private int get_noofromByflr(int pg, String curstatus, String curtyp)
        {
            String WHERE = "";
            thisDatabase db = new thisDatabase();

            if (curstatus == ".")
            {
                curstatus = "";
            }
            else  if (curstatus == "ALL VACANTS")
            {
                curstatus = " AND (r.stat_code='VC' OR r.stat_code='VD')";
            }
            else if (curstatus == "VACANT CLEAN")
            {
                curstatus = " AND r.stat_code='VC'";
            }
            else if (curstatus == "VACANT DIRTY")
            {
                curstatus = " AND r.stat_code='VD'";
            }
            else if (curstatus == "OCCUPIED")
            {
                curstatus = " AND r.stat_code='OCC'";
            }
            else if (curstatus == "OUT OF ORDER")
            {
                curstatus = " AND r.stat_code='OOO'";
            }
            else if (curstatus == "RESERVED")
            {
                curstatus = "";
            }

            if (String.IsNullOrEmpty(curtyp) == false)
            {
                curtyp = " AND r.typ_code='" + curtyp + "'";
            }

            if (pg > 0)
            {
                WHERE = " WHERE r.rom_code LIKE '" + pg + "%'" + curtyp + "" + curstatus + "";
            }

            String SQL_room = "SELECT COUNT(r.rom_code) FROM " + schema + ".rooms r" + WHERE + "";

            DataTable dt = db.QueryBySQLCode(SQL_room);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }

            return 0;
        }

        private int get_noofromByStatus(int pg, String curstatus, String curtyp)
        {
            String WHERE = "";
            String add_code = "";
            thisDatabase db = new thisDatabase();

            if (curstatus == "VC")
            {
                curstatus = " r.stat_code='VC'";
            }
            else if (curstatus == "VD")
            {
                curstatus = " r.stat_code='VD'";
            }
            else if (curstatus == "OCC")
            {
                curstatus = " r.stat_code='OCC'";
            }
            else if (curstatus == "OOO")
            {
                curstatus = " r.stat_code='OOO'";
            }
            else if (curstatus == "R")
            {
                curstatus = "";
            }

            if (String.IsNullOrEmpty(curtyp) == false)
            {
                curtyp = " AND r.typ_code='" + curtyp + "'";
            }

            //if 0 means all.
            if (pg > 0)
            {
                add_code = " AND r.rom_code LIKE '" + pg + "%'";
            }

            if (String.IsNullOrEmpty(curstatus) == false)
            {
                WHERE = " WHERE " + curstatus + "" + curtyp + "" + add_code;
            }
            else
            {
                WHERE = " WHERE r.rom_code LIKE '" + pg + "%'";
            }

            String SQL_room = "SELECT COUNT(r.stat_code) FROM " + schema + ".rooms r" + WHERE + "";

            DataTable dt = db.QueryBySQLCode(SQL_room);

            try
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("get_noofromByStatus: " + er.Message);
            }

            return 0;
        }

        private int get_noofreservation(int pg, String curtyp)
        {
            return 0;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dtp_chkin_ValueChanged(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(lbl_flr.Text);

            lbl_flr.Text = i.ToString();

            load_lblstatusPerFlr();

            load_tableview(i);

            lbl_top_dt_range.Text = dtp_chkin.Value.ToString("MMMM dd, yyyy");
        }
    }
}