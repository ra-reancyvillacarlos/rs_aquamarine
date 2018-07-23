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
    public partial class or_entry : Form
    {
        Boolean isnew = true;

        public or_entry()
        {
            InitializeComponent();
        }

        private void or_entry_Load(object sender, EventArgs e)
        {
            disp_or();
        }

        public void setdata(String reg_num, String full_name, String rom_code)
        {
            lbl_gfolionum.Text = reg_num;
            lbl_guestname.Text = full_name;
            lbl_rom_code.Text = rom_code;
        }

        private void disp_or()
        {
            thisDatabase db = new thisDatabase();

            dgv_orlist.DataSource = db.get_orlist(lbl_gfolionum.Text);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            if (txt_ref.Text == "")
            {
                MessageBox.Show("OR Reference should be inputted.");
            }
            else if (Convert.ToDouble(txt_amt.Text) <= 0)
            {
                MessageBox.Show("Invalid amount.");
            }
            else
            {
                if (isnew)
                {
                    if (db.InsertOnTable("or_folio_ref", "reg_num, or_no, or_amnt, user_id, t_date, t_time", "'" + lbl_gfolionum.Text + "', '" + txt_ref.Text + "', '" + txt_amt.Text + "', '" + GlobalClass.username + "', '" + db.get_systemdate("") + "', '" + DateTime.Now.ToString("HH:mm") + "'"))
                    {
                        MessageBox.Show("OR entry successfully saved.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error on saving.");
                    }
                }
                else
                {
                    if (db.UpdateOnTable("or_folio_ref", "reg_num='" + lbl_gfolionum.Text + "', or_no='" + txt_ref.Text + "', or_amnt='" + txt_amt.Text + "', user_id='" + GlobalClass.username + "', t_date='" + db.get_systemdate("") + "', t_time='" + DateTime.Now.ToString("HH:mm") + "'", "id='" + lbl_serial.Text + "'"))
                    {
                        MessageBox.Show("Successfully updated");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error on saving.");
                    }                    
                }
                disp_or();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_orlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_orlist.CurrentRow.Selected)
            {
                int row = dgv_orlist.CurrentRow.Index;

                txt_ref.Text = dgv_orlist.Rows[row].Cells[0].Value.ToString();
                txt_amt.Text = dgv_orlist.Rows[row].Cells[1].Value.ToString();
                try
                {
                    dtp_tdate.Value = Convert.ToDateTime(dgv_orlist.Rows[row].Cells[3].Value.ToString());
                }
                catch (Exception) { }
                lbl_serial.Text = dgv_orlist.Rows[row].Cells[5].Value.ToString();

                isnew = false;
            }
                
        }

        private void dgv_orlist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
