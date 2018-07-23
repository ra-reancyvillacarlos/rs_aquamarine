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
    public partial class RoomRates : Form
    {
        Hotel_System.thisDatabase db = new Hotel_System.thisDatabase();
        Hotel_System.GlobalClass gc = new Hotel_System.GlobalClass();
        Hotel_System.GlobalMethod gm = new Hotel_System.GlobalMethod();
        
        public RoomRates()
        {
            InitializeComponent();
            gm.load_ratetype(cbo_rrrt);
            thisDatabase db = new thisDatabase();
            String grp_id = "";
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0204' ORDER BY b.pla, b.mod_id");

            if (dt2.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt2.Rows[0]["add"].ToString();
                update = dt2.Rows[0]["upd"].ToString();
                delete = dt2.Rows[0]["cancel"].ToString();
                print = dt2.Rows[0]["print"].ToString();

                if (add == "n")
                {
                   // btn_rtnew.Enabled = false;
                }
                if (update == "n")
                {
                    btn_rrupdate.Enabled = false;
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
            disp_list();
        
        }

        private void RoomRates_Load(object sender, EventArgs e)
        {

        }
        public void disp_list()
        {
            DataTable dt = db.QueryBySQLCode("SELECT  rt.*, COALESCE(single,0.00) AS single, COALESCE(double,0.00) AS double, COALESCE(triple,0.00) AS triple, COALESCE(quad,0.00) AS quad FROM rssys.rtype rt LEFT JOIN (SELECT rr.* FROM rssys.ratetype rt LEFT JOIN rssys.romrate rr ON rr.rate_code=rt.rate_code WHERE rt.rate_code='" + (cbo_rrrt.SelectedValue ?? "").ToString() + "') rr ON rr.typ_code=rt.typ_code");
            dgv_rrlist.DataSource = dt;
        }

        private void btn_rrsave_Click(object sender, EventArgs e)
        {
            int r = dgv_rrlist.CurrentRow.Index;
            if (r != -1 && cbo_rrrt.SelectedIndex != -1)
            {
                //room_rate_info(String room_code,String room_type)
                room_rate_info frm = new room_rate_info(this, dgv_rrlist["typ_code", r].Value.ToString(), dgv_rrlist["typ_desc", r].Value.ToString(), cbo_rrrt.SelectedValue.ToString());
                frm.ShowDialog();
            }
            else {
                MessageBox.Show("Select Rate Type First.");
                cbo_rrrt.DroppedDown = true;
            }
        }

        private void cbo_rrrt_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_list();
        }

        private void dgv_rrlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
