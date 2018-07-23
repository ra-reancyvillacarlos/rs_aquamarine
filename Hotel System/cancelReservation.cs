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
    public partial class cancelReservation : Form
    {
        newReservation Res;

        public cancelReservation(newReservation R)
        {
            InitializeComponent();

            Res = R;
        }
        private void cancelReservation_Load(object sender, EventArgs e)
        {
            
        }


        private void disp_cancellednowshohis()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = new DataTable();
            dt.Columns.Add("Arrival", typeof(String));
            dt.Columns.Add("Departure", typeof(String));
            dt.Columns.Add("Room", typeof(String));
            dt.Columns.Add("Description", typeof(String));
            dt.Columns.Add("Type", typeof(String));
            dt.Columns.Add("Guest No", typeof(String));
            dt.Columns.Add("Guest(s)", typeof(String));
            dt.Columns.Add("Occupancy", typeof(String));
            dt.Columns.Add("Rate Type", typeof(String));
            dt.Columns.Add("Disc", typeof(String));
            dt.Columns.Add("Room Rate", typeof(String));
            dt.Columns.Add("Room Rate Total", typeof(String));
            dt.Columns.Add("Free B-Fast", typeof(String));
            dt.Columns.Add("Blocked", typeof(String));
            try
            {
                foreach (DataGridViewRow row in dgv_guests.Rows)
                {
                    dt = db.get_cancelandnoshowreshist(row.Cells[0].Value.ToString());

                    if (dt.Rows.Count > 0)
                    {
                        dgv_cancellednoshowhist.DataSource = dt;
                    }
                }
            }
            catch (Exception) { }
        }

        public void reload_guest()
        {
            DataGridViewRow row = new DataGridViewRow();

            if (GlobalClass.gdgv != null)
            {
                for (int i = 0; i < GlobalClass.gdgv.Rows.Count - 1; i++)
                {
                    row = (DataGridViewRow)GlobalClass.gdgv.Rows[i].Clone();

                    int intColIndex = 0;

                    foreach (DataGridViewCell cell in GlobalClass.gdgv.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_guests.Rows.Add(row);
                }
            }

            lbl_noofguest.Text = (dgv_guests.Rows.Count - 1).ToString();

            GlobalClass.gdgv = null;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Res.reset_modname();
            clr_thisfield();
            this.Hide();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String reason = "", turnaway = "", remark = "";

            try
            {
                if (cbo_reason.Text != "" || cbo_turnaway.Text != "" || rtxt_remark.Text != "")
                {
                    if (MessageBox.Show("Are you sure you want to CANCEL the Reservation?", "Confirm Cancel", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (cbo_reason.Text != "")
                        {
                            reason = cbo_reason.Text;
                        }
                        if (cbo_turnaway.Text != "")
                        {
                            turnaway = cbo_turnaway.Text;
                        }

                        remark = rtxt_remark.Text;

                        if (db.cancel_reservation(txt_code.Text, reason, lbl_user.Text, remark, turnaway, false))
                        {
                            Res.reset_modname();
                            Res.gotofront();
                            Res.set_reslist(Res.active_res_where);
                            Res.clr_field();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pls enter any of the fields.");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public void set_data(String code)
        {
            thisDatabase db = new thisDatabase();
            
            txt_code.Text = code;
            lbl_user.Text = GlobalClass.username;
            lbl_date.Text = db.get_systemdate("");
            lbl_time.Text = DateTime.Now.ToString("HH:mm");

            reload_guest();
            disp_cancellednowshohis();
        }

        private void clr_thisfield()
        {
            txt_code.Text = "";
            cbo_reason.SelectedIndex = -1;
            cbo_turnaway.SelectedIndex = -1;
            rtxt_remark.Text = "";
            lbl_user.Text = "";
            lbl_date.Text = "";
            lbl_time.Text = "";
        }

        private void dgv_cancellednoshowhist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_guests_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
