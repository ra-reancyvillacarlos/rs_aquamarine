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
    public partial class RoomRate : Form
    {
        Boolean isrtnew = false;
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        String displist = "last_name, first_name, mid_name, gender, birth_date, address1, tel_num, email, comp_code, passport_no, passport_issued, passport_expiry, cntry_code, escaper, notes, acct_no, title";
        
        public RoomRate()
        {
            InitializeComponent();
         
        }

        private void RoomRate_Load(object sender, EventArgs e)
        {
            form_rtreset();
        }

        private void form_rtreset()
        {
            //txt_rtid.Enabled = false;
            //rtxt_rtdesc.Enabled = false;
            //chk_rtfreebkfast.Enabled = false;

            //btn_rtnew.Enabled = true;
            //btn_rtedit.Enabled = false;
            //btn_rtsave.Enabled = false;
            //btn_rtcancel.Enabled = false;
        }

        private void form_rtnew()
        {
            //txt_rtid.Enabled = true;
            //rtxt_rtdesc.Enabled = true;
            //chk_rtfreebkfast.Enabled = true;

            //btn_rtnew.Enabled = false;
            //btn_rtedit.Enabled = false;
            //btn_rtsave.Enabled = true;
            //btn_rtcancel.Enabled = true;
        }

        private void form_rttoedit()
        {
            form_rtreset();
            //btn_rtnew.Enabled = false;
            //btn_rtedit.Enabled = true;

            form_rtsetreadonly(true);
        }

        private void form_rtedit()
        {
            form_rtnew();
            form_rtsetreadonly(false);
        }

        private void form_rtclear()
        {
            //txt_rtid.Text = "";
            //rtxt_rtdesc.Text = "";
            //chk_rtfreebkfast.Checked = false;
        }

        private void form_rtsetreadonly(Boolean flag)
        {
            //txt_rtid.ReadOnly = flag;
            //rtxt_rtdesc.ReadOnly = flag;
            
            //if(flag == true)
            //{
            //    chk_rtfreebkfast.Enabled = false;
            //}
            //else
            //{
            //    chk_rtfreebkfast.Enabled = true;
            //}
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

                return db.QueryOnTableWithParams("guest", this.displist, "", "ORDER BY full_name ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }

        private void btn_rtnew_Click(object sender, EventArgs e)
        {
            isrtnew = true;
            form_rtnew();
        }

        private void btn_rtedit_Click(object sender, EventArgs e)
        {
            isrtnew = false;
            form_rtedit();
        }

        private void btn_rtsave_Click(object sender, EventArgs e)
        {
            String room_type_id = "", room_type_desc = "", col = "", val = "";
            if (/*txt_rtid.Text*/ "asds" == "")
            {
                MessageBox.Show("Please Input Room Type ID.");
               // txt_rtid.Focus();
            }
            else if (/*rtxt_rtdesc.Text*/"sad" == "")
            {
                MessageBox.Show("Please Input Room Type Description.");
                //rtxt_rtdesc.Focus();
            }
            else
            {
                try
                {
                    if (isrtnew)
                    {
                        //room_type_id = txt_rtid.Text;
                        //room_type_desc = rtxt_rtdesc.Text;
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
                        //room_type_id = txt_rtid.Text;
                        //room_type_desc = rtxt_rtdesc.Text;
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
                //dgv_rtlist.DataSource = dt;
            }
            catch (Exception) { }


        }
        void clear()
        {
            //txt_rtid.Text = "";
            //rtxt_rtdesc.Text = "";
            //txt_rtid.Enabled = false;
            //rtxt_rtdesc.Enabled = false;
            //btn_rtsave.Enabled = true;
        }
        private void btn_rtcancel_Click(object sender, EventArgs e)
        {
            isrtnew = false;
            form_rtclear();
            form_rtreset();
        }

        private void btn_rrsave_Click(object sender, EventArgs e)
        {

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
