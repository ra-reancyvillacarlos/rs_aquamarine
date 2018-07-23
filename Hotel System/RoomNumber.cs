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
    public partial class RoomNumber : Form
    {
        Boolean isrtnew = false, isrnnew = false;
        String displist = "last_name, first_name, mid_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, escaper, notes, acct_no, title";
        Int32 qlistlimit = 12;
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
       

        public RoomNumber()
        {
            InitializeComponent();
            disp_dgvlist();
            disp_dgvlist2();
            gm.load_romtype(cbo_rt);
            thisDatabase db = new thisDatabase();
            String grp_id = "";
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0202' ORDER BY b.pla, b.mod_id");

            if (dt2.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt2.Rows[0]["add"].ToString();
                update = dt2.Rows[0]["upd"].ToString();
                delete = dt2.Rows[0]["cancel"].ToString();
                print = dt2.Rows[0]["print"].ToString();

                if (add == "n")
                {
                    btn_rnnew.Enabled = false;
                }
                if (update == "n")
                {
                    btn_rnedit.Enabled = false;
                }
                if (delete == "n")
                {
                    //btn_delitem.Enabled = false;
                }
                if (print == "n")
                {
                    //btn_print.Enabled = false;
                }

            }
        }

        private void RoomNumber_Load(object sender, EventArgs e)
        {
            form_rtreset();
            form_rnreset();
        }

        private void form_rtreset()
        {
            txt_rtid.Enabled = false;
            rtxt_rtdesc.Enabled = false;

            btn_rtnew.Enabled = true;
            btn_rtedit.Enabled = true;
            btn_rtsave.Enabled = false;
            btn_rtcancel.Enabled = false;
        }

        private void form_rtnew()
        {
            txt_rtid.Enabled = true;
            rtxt_rtdesc.Enabled = true;

            btn_rtnew.Enabled = false;
            btn_rtedit.Enabled = false;
            btn_rtsave.Enabled = true;
            btn_rtcancel.Enabled = true;
        }

        private void form_rttoedit()
        {
            form_rtreset();
            btn_rtnew.Enabled = false;
            btn_rtedit.Enabled = true;

            form_rtsetreadonly(true);            
        }

        private void form_rtedit()
        {
            form_rtnew();
            form_rtsetreadonly(false);
        }

        private void form_rtclear()
        {
            txt_rtid.Text = "";
            rtxt_rtdesc.Text = "";
        }

        private void form_rtsetreadonly(Boolean flag)
        {
            txt_rtid.ReadOnly = flag;
            rtxt_rtdesc.ReadOnly = flag;
        }

        private DataTable get_rtlist(int offset)
        {
            try
            {
                thisDatabase db = new thisDatabase();

                String offset_code = "";

                if (offset != 0)
                {
                    offset_code = " OFFSET " + offset.ToString();
                }

                return db.QueryOnTableWithParams("guest", this.displist, "", "ORDER BY full_name ASC LIMIT " + qlistlimit.ToString() + "" + offset_code);
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }

        private void putdata_allcbort()
        {
            try
            {
                DataTable dt = new DataTable();
                thisDatabase db = new thisDatabase();
                /*
                dt = db.QueryOnTableWithParams("company", "comp_code, comp_name", "", "ORDER BY comp_name ASC;");
                
                cbo_searchcompany.DataSource = dt;
                cbo_searchcompany.DisplayMember = "comp_name";
                cbo_searchcompany.ValueMember = "comp_code";
                
                cbo_company.DataSource = dt;
                cbo_company.DisplayMember = "comp_name";
                cbo_company.ValueMember = "comp_code";*/

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void btn_rtnew_Click(object sender, EventArgs e)
        {
            isrtnew = true;
            form_rtnew();
            btn_rtedit.Enabled = false;
        }

        private void btn_rtedit_Click(object sender, EventArgs e)
        {
           
            form_rtedit();

            btn_rtnew.Enabled = false;
            isrtnew = false;
            int r = dgv_rtlist.CurrentRow.Index;
            try
            {
                if (r >= 0)
                {
                    txt_rtid.Text = dgv_rtlist["dgv_list_room_type_id", r].Value.ToString();
                    rtxt_rtdesc.Text = dgv_rtlist["dgv_list_room_desc", r].Value.ToString();
                    txt_rtid.Enabled = true;
                    rtxt_rtdesc.Enabled = true;
                    btn_rtsave.Enabled = true;
                }
                else
                {
                    txt_rtid.Enabled = false;
                    rtxt_rtdesc.Enabled = false;
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void btn_rtsave_Click(object sender, EventArgs e)
        {
            String room_type_id = "", room_type_desc = "", col = "", val = "";
            if (txt_rtid.Text == "")
            {
                MessageBox.Show("Please Input Room Type ID.");
                txt_rtid.Focus();
            }
            else if (rtxt_rtdesc.Text == "")
            {
                MessageBox.Show("Please Input Room Type Description.");
                rtxt_rtdesc.Focus();
            }
            else
            {
                try
                {
                    if (isrtnew)
                    {
                        room_type_id = txt_rtid.Text;
                        room_type_desc = rtxt_rtdesc.Text;
                        col = "typ_code,typ_desc";
                        val = "'" + room_type_id + "','" + room_type_desc + "'";
                        if (db.InsertOnTable("rtype", col, val))
                        {

                        }
                        else
                        {
                            MessageBox.Show("Saving Failed.");
                        }
                    }
                    else
                    {
                        room_type_id = txt_rtid.Text;
                        room_type_desc = rtxt_rtdesc.Text;
                        col = "typ_code='" + room_type_id + "', typ_desc='" + room_type_desc + "'";
                        if (db.UpdateOnTable("rtype", col, "typ_code='" + room_type_id + "'"))
                        {

                        }
                        else
                        {
                            MessageBox.Show("Updating Record Failed.");
                        }

                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
            disp_dgvlist();
            clear();
        }
        private void disp_dgvlist()
        {
            DataTable dt = db.QueryBySQLCode("SELECT typ_code,typ_desc from rssys.rtype ORDER BY typ_desc");
            try
            {
                dgv_rtlist.DataSource = dt;
            }
            catch (Exception) { }


        }
        private void disp_dgvlist2()
        {
            DataTable dt = db.QueryBySQLCode("SELECT r.rom_code,r.rom_desc,r.typ_code,rt.typ_desc from rssys.rooms r LEFT JOIN rssys.rtype rt ON r.typ_code=rt.typ_code ORDER BY rom_code");
            try
            {
                dgv_rnlist.DataSource = dt;
            }
            catch (Exception) { }


        }
        void clear2()
        {
            txt_rnid.Text = "";
            rtxt_rndesc.Text = "";
            cbo_rt.SelectedValue = -1;
            txt_rnid.Enabled = false;
            rtxt_rndesc.Enabled = false;
            btn_rnsave.Enabled = false;
            btn_rnnew.Enabled = true;
            btn_rnedit.Enabled = true;
            btn_rncancel.Enabled = false;
        
        }
        void clear()
        {
            txt_rtid.Text = "";
            rtxt_rtdesc.Text = "";
            txt_rtid.Enabled = false;
            rtxt_rtdesc.Enabled = false;
            btn_rtsave.Enabled = true;
        }
        private void btn_rtcancel_Click(object sender, EventArgs e)
        {
            isrtnew = false;
            form_rtclear();
            form_rtreset();
        }

        private void btn_rtprev_Click(object sender, EventArgs e)
        {

        }

        private void btn_rtnext_Click(object sender, EventArgs e)
        {

        }

        //////////////////////// room number /////////////////

        private void form_rnreset()
        {
            txt_rnid.Enabled = false;
            rtxt_rndesc.Enabled = false;
            cbo_rt.Enabled = false;

            btn_rnnew.Enabled = true;
            btn_rnedit.Enabled = true;
            btn_rnsave.Enabled = false;
            btn_rncancel.Enabled = false;
        }

        private void form_rnnew()
        {
            txt_rnid.Enabled = true;
            rtxt_rndesc.Enabled = true;
            cbo_rt.Enabled = true;

            btn_rnnew.Enabled = false;
            btn_rnedit.Enabled = false;
            btn_rnsave.Enabled = true;
            btn_rncancel.Enabled = true;
        }

        private void form_rntoedit()
        {
            form_rnreset();
            btn_rnnew.Enabled = false;
            btn_rnedit.Enabled = true;

            form_rnsetreadonly(true);
        }

        private void form_rnedit()
        {
            form_rnnew();
            form_rnsetreadonly(false);
        }

        private void form_rnclear()
        {
            txt_rnid.Text = "";
            rtxt_rndesc.Text = "";
        }

        private void form_rnsetreadonly(Boolean flag)
        {
            txt_rnid.ReadOnly = flag;
            rtxt_rndesc.ReadOnly = flag;

            if (flag == false)
            {
                cbo_rt.Enabled = true;
            }
            else
            {
                cbo_rt.Enabled = false;
            }
        }

        private DataTable get_rnlist(int offset)
        {
            try
            {
                thisDatabase db = new thisDatabase();

                String offset_code = "";

                if (offset != 0)
                {
                    offset_code = " OFFSET " + offset.ToString();
                }

                return db.QueryOnTableWithParams("guest", this.displist, "", "ORDER BY full_name ASC LIMIT " + qlistlimit.ToString() + "" + offset_code);
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }

        private void btn_rnnew_Click(object sender, EventArgs e)
        {
            isrnnew = true;
            form_rnnew();
            btn_rnedit.Enabled = false;
        }

        private void btn_rnedit_Click(object sender, EventArgs e)
        {
            isrnnew = false;
            form_rnedit();

            btn_rnnew.Enabled = false;
           
            int r = dgv_rnlist.CurrentRow.Index;
            try
            {
                if (r >= 0)
                {
                    txt_rnid.Text = dgv_rnlist["rom_code", r].Value.ToString();
                    rtxt_rndesc.Text = dgv_rnlist["rom_desc", r].Value.ToString();
                    cbo_rt.SelectedValue = dgv_rnlist["dgv_type_code", r].Value.ToString();
                    txt_rtid.Enabled = true;
                    rtxt_rtdesc.Enabled = true;
                    btn_rtsave.Enabled = true;
                }
                else
                {
                    txt_rnid.Enabled = false;
                    rtxt_rndesc.Enabled = false;
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void btn_rnsave_Click(object sender, EventArgs e)
        {
            String rom_code = "", rom_desc = "",type_code="", col = "", val = "";
            if (txt_rnid.Text == "")
            {
                MessageBox.Show("Please Input Room  ID.");
                txt_rnid.Focus();
            }
            else if (rtxt_rndesc.Text == "")
            {
                MessageBox.Show("Please Input Room Description.");
                rtxt_rndesc.Focus();
            }
            else if(cbo_rt.SelectedIndex == -1)
            {
                MessageBox.Show("Please select room type.");
                cbo_rt.DroppedDown = true;
            }

            else
            {
                try
                {
                    if (isrnnew)
                    {
                        rom_code = txt_rnid.Text;
                        rom_desc = rtxt_rndesc.Text;
                        type_code = cbo_rt.SelectedValue.ToString();
                        col = "rom_code,rom_desc,typ_code";
                        val = "'" + rom_code + "','" + rom_desc + "','" +type_code+"'";
                        if (db.InsertOnTable("rooms", col, val))
                        {

                        }
                        else
                        {
                            MessageBox.Show("Saving Failed.");
                        }
                    }
                    else
                    {
                        rom_code = txt_rnid.Text;
                        rom_desc = rtxt_rndesc.Text;
                        type_code = cbo_rt.SelectedValue.ToString();

                        col = "rom_code='" + rom_code + "', rom_desc='" + rom_desc + "' ,typ_code='" + type_code+"'";
                        if (db.UpdateOnTable("rooms", col, "rom_code='" + rom_code + "'"))
                        {

                        }
                        else
                        {
                            MessageBox.Show("Updating Record Failed.");
                        }

                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
            disp_dgvlist2();
            clear2();
        }
        

         
        private void btn_rncancel_Click(object sender, EventArgs e)
        {
            isrnnew = false;
            form_rnclear();
            form_rnreset();
        }

        private void btn_rnprev_Click(object sender, EventArgs e)
        {

        }

        private void btn_rnnext_Click(object sender, EventArgs e)
        {

        }

        private void btn_rncancel_Click_1(object sender, EventArgs e)
        {
            isrnnew = false;
            form_rtclear();
            form_rtreset();
        }

        private void dgv_rtlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_rnlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
