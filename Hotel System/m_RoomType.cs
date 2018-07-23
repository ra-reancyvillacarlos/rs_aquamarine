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
    public partial class m_RoomType : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        Boolean isnew = false;
        public m_RoomType()
        {
            InitializeComponent();
            thisDatabase db = new thisDatabase();
            String grp_id = "";
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0203' ORDER BY b.pla, b.mod_id");

            if (dt2.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt2.Rows[0]["add"].ToString();
                update = dt2.Rows[0]["upd"].ToString();
                delete = dt2.Rows[0]["cancel"].ToString();
                print = dt2.Rows[0]["print"].ToString();

                if (add == "n")
                {
                    btn_rtnew.Enabled = false;
                }
                if (update == "n")
                {
                    btn_rtedit.Enabled = false;
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
            disp_dgvlist();
        }

        private void m_RoomType_Load(object sender, EventArgs e)
        {
            form_rtreset();
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

        private void form_rttoedit()
        {
            form_rtreset();
            btn_rtnew.Enabled = false;
            btn_rtedit.Enabled = true;

            form_rtsetreadonly(true);
        }

        private void form_rtedit()
        {

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



        private void putdata_allcbort()
        {
            try
            {

            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }
        }

        private void btn_rtnew_Click(object sender, EventArgs e)
        {

        }

        private void btn_rtedit_Click(object sender, EventArgs e)
        {

        }

        private void btn_rtsave_Click(object sender, EventArgs e)
        {

        }

        private void btn_rtcancel_Click(object sender, EventArgs e)
        {

        }

        private void btn_rtnew_Click_1(object sender, EventArgs e)
        {
            txt_rtid.Text = "";
            rtxt_rtdesc.Text = "";
            txt_rtid.Enabled = true;
            rtxt_rtdesc.Enabled = true;
            isnew = true;
            btn_rtsave.Enabled = true;
            btn_rtedit.Enabled = false;
        }

        private void btn_rtedit_Click_1(object sender, EventArgs e)
        {
            btn_rtnew.Enabled = true;
            isnew = false;
            int r = dgv_rtlist.CurrentRow.Index;
            try { 
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

        private void btn_rtsave_Click_1(object sender, EventArgs e)
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
                    if (isnew)
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
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.rtype ORDER BY typ_desc");
            try
            {
                dgv_rtlist.DataSource = dt;
            }
            catch (Exception) { }


        }
        void clear()
        {
            txt_rtid.Text = "";
            rtxt_rtdesc.Text = "";
            txt_rtid.Enabled = false;
            rtxt_rtdesc.Enabled = false;
            btn_rtsave.Enabled = true;
        }
        private void btn_rtcancel_Click_1(object sender, EventArgs e)
        {
            form_rtreset();
            clear();
        }

        private void dgv_rtlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_rtlist_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            form_rtreset();
            clear();
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
    }
}
