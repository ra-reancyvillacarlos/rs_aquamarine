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
    public partial class UpdateRoomStatus : Form
    {
        public UpdateRoomStatus()
        {
            InitializeComponent();

        }

        private void UpdateRoomStatus_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            disp_dgv_rmstatuslist();
            set_roomstatuslist();

            cbo_rmtyp.SelectedIndex = -1;
            cbo_rmstatus.SelectedIndex = -1;
        }

        private void set_roomstatuslist()
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
        }

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
        }

        private void cbo_rmstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgv_rmstatuslist();
        }

        private void cbo_rmtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cbo_rmtyp.SelectedIndex.ToString()+" "+cbo_rmtyp.SelectedValue.ToString());
            disp_dgv_rmstatuslist();
        }

        private void btn_vc_Click(object sender, EventArgs e)
        {
            set_rmstatus("VC", lbl_rm.Text);
        }

        private void btn_vd_Click(object sender, EventArgs e)
        {
            set_rmstatus("VD", lbl_rm.Text);
        }

        private void btn_occ_Click(object sender, EventArgs e)
        {
            set_rmstatus("OCC", lbl_rm.Text);
        }

        private void btn_ooo_Click(object sender, EventArgs e)
        {
            set_rmstatus("OOO", lbl_rm.Text);
        }

        private void btn_fun_Click(object sender, EventArgs e)
        {
            set_rmstatus("FUN", lbl_rm.Text);
        }

        public void set_rmstatus(String rmstatus, String rm_code)
        {
            thisDatabase db = new thisDatabase();
            Boolean success = false;
            String reason = "";

            if (rmstatus == "OOO")
            {
                reason = Prompt.ShowDialog("Pls enter the Reason why this room is set to Out Of Order(OOO):", "Reason");
            }
            if ((rmstatus == "OOO" && reason != "") || rmstatus != "OOO")
            {
                if (db.UpdateOnTable("rooms", "stat_code='" + rmstatus + "'", "rom_code='"+rm_code+"'"))
                {
                    success = true;

                    if (rmstatus == "OOO")
                    {
                        if (db.InsertOnTable("rmoorder", "trnx_date, rom_code, reason, t_date, t_time", "'" + db.get_systemdate("") + "', '" + rm_code + "', '" + reason + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "'"))
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }                    
                    }
                }

                if (success == false)
                {
                    MessageBox.Show("Error on updating status.");
                }
                //else
                //{
                    disp_dgv_rmstatuslist();
                //}
            }
            else
            {
                MessageBox.Show("Room cannot be updated. Pls specify the reason.");
            }
        }

        private void dgv_romstatus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            try
            {
                if (dgv_romstatus.Rows.Count > 0)
                {
                    lbl_rm.Text = dgv_romstatus[0, e.RowIndex].Value.ToString();
                    lbl_gfno.Text = db.get_guestregnum(lbl_rm.Text);
                    lbl_gfname.Text = db.get_guestname(lbl_rm.Text);
                    lbl_arrdate.Text = Convert.ToDateTime(db.get_guestarrdate(lbl_rm.Text)).ToString("MM/dd/yyyy");
                    lbl_depdate.Text = Convert.ToDateTime(db.get_guestdepdate(lbl_rm.Text)).ToString("MM/dd/yyyy");
                    dgv_ooohistory.DataSource = db.get_guest_ooohistory(lbl_rm.Text);
                }
            }
            catch (Exception) { }
        }

        private void dgv_romstatus_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_ooohistory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            set_rmstatus("OFC", lbl_rm.Text);
        }

        private void dgv_romstatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
