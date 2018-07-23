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
    public partial class Housekeeping : Form
    {
        Boolean hiisnew = false, raisnew = false;

        public Housekeeping()
        {
            InitializeComponent();
        }

        private void Housekeeping_Load(object sender, EventArgs e)
        {
            form_hireset();
            form_rareset();
            txt_hicode.Hide();
            txt_racode.Hide();
            dgv_hilist.DataSource = get_hilist();
            dgv_ralist.DataSource = get_ralist();
        }

        //////////////// housekeeping item ///////////////////////

        private void form_hireset()
        {
            txt_hicode.Enabled = false;
            txt_hidesc.Enabled = false;

            btn_hinew.Enabled = true;
            btn_hiedit.Enabled = false;
            btn_hisave.Enabled = false;
            btn_hicancel.Enabled = false;
        }

        private void form_hinew()
        {
            txt_hicode.Enabled = true;
            txt_hidesc.Enabled = true;

            btn_hinew.Enabled = false;
            btn_hiedit.Enabled = false;
            btn_hisave.Enabled = true;
            btn_hicancel.Enabled = true;
        }

        private void form_hitoedit()
        {
            form_hireset();
            btn_hinew.Enabled = false;
            btn_hiedit.Enabled = true;

            form_hisetreadonly(true);
        }

        private void form_hiedit()
        {
            form_hinew();
            form_hisetreadonly(false);
        }

        private void form_hiclear()
        {
            txt_hicode.Text = "";
            txt_hidesc.Text = "";
        }

        private void form_hisetreadonly(Boolean flag)
        {
            txt_hicode.ReadOnly = flag;
            txt_hidesc.ReadOnly = flag;
        }

        private DataTable get_hilist()
        {
            try
            {
                thisDatabase db = new thisDatabase();

                return db.QueryOnTableWithParams("hskitem", "hkitem_code, hkitem_desc", "", "ORDER BY hkitem_code ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }

        private void btn_hinew_Click(object sender, EventArgs e)
        {
            hiisnew = true;
            form_hinew();
        }

        private void btn_hiedit_Click(object sender, EventArgs e)
        {
            hiisnew = false;
             
            form_hiedit();
        }

        private void btn_hisave_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            if (hiisnew == true)
            {

                String pk = db.get_pk("hkitem_code");

                if (db.InsertOnTable("hskitem", "hkitem_code, hkitem_desc", "'" + pk + "','" + txt_hidesc.Text + "'"))
                {
                    MessageBox.Show("New record added successfully.");
                    form_hiclear();
                    form_hireset();
                    db.set_pkm99("hkitem_code", db.get_nextincrementlimitchar(pk, 6));
                }
                else
                {
                    MessageBox.Show("Duplicate entry.");
                }
            }
            else
            {
                if (db.UpdateOnTable("hskitem", "hkitem_desc='" + txt_hidesc.Text + "'", "hkitem_code='" + txt_hicode.Text + "'"))
                {
                    MessageBox.Show("Record updated successfully.");
                    form_hiclear();
                    form_hireset();
                }
                else
                {
                    MessageBox.Show("Record cannot update.");
                }
            }

            dgv_hilist.DataSource = get_hilist();
        }

        private void btn_hicancel_Click(object sender, EventArgs e)
        {
            hiisnew = false;
            form_hiclear();
            form_hireset();
        }

        private void dgv_hilist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            form_hitoedit();

            int row = dgv_hilist.CurrentCell.RowIndex;

            String code = dgv_hilist.Rows[row].Cells[0].Value.ToString().Trim();
            String desc = dgv_hilist.Rows[row].Cells[1].Value.ToString().Trim();

            txt_hicode.Text = code;
            txt_hidesc.Text = desc;
        }

        //////////////// room attendant ///////////////////////

        private void form_rareset()
        {
            txt_racode.Enabled = false;
            txt_raname.Enabled = false;

            btn_ranew.Enabled = true;
            btn_raedit.Enabled = false;
            btn_rasave.Enabled = false;
            btn_racancel.Enabled = false;
        }

        private void form_ranew()
        {
            txt_racode.Enabled = true;
            txt_raname.Enabled = true;

            btn_ranew.Enabled = false;
            btn_raedit.Enabled = false;
            btn_rasave.Enabled = true;
            btn_racancel.Enabled = true;
        }

        private void form_ratoedit()
        {
            form_rareset();
            btn_ranew.Enabled = false;
            btn_raedit.Enabled = true;

            form_rasetreadonly(true);
        }

        private void form_raedit()
        {
            form_ranew();
            form_rasetreadonly(false);
        }

        private void form_raclear()
        {
            txt_racode.Text = "";
            txt_raname.Text = "";
        }

        private void form_rasetreadonly(Boolean flag)
        {
            txt_racode.ReadOnly = flag;
            txt_raname.ReadOnly = flag;
        }

        private DataTable get_ralist()
        {
            try
            {
                thisDatabase db = new thisDatabase();

                return db.QueryOnTableWithParams("ramastr", "ra_code, ra_name", "", "ORDER BY ra_code ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }

        private void btn_ranew_Click(object sender, EventArgs e)
        {
            raisnew = true;
            form_ranew();
        }

        private void btn_raedit_Click(object sender, EventArgs e)
        {
            raisnew = false;
            
           
            form_raedit();
        }

        private void btn_rasave_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            if (raisnew == true)
            {
                int pkval = db.get_cntrow("ramastr", "ra_code", "");
                
                String pk = db.get_nextincrementlimitchar(pkval.ToString(), 3);

                if (db.InsertOnTable("ramastr", "ra_code, ra_name", "'" + pk + "','" + txt_raname.Text + "'"))
                {
                    MessageBox.Show("New record added successfully.");
                    form_raclear();
                    form_rareset();
                }
                else
                {
                    MessageBox.Show("Duplicate entry.");
                }
            }
            else
            {
                if (db.UpdateOnTable("ramastr", "ra_name='" + txt_raname.Text + "'", "ra_code='" + txt_racode.Text + "'"))
                {
                    MessageBox.Show("Record updated successfully.");
                    form_raclear();
                    form_rareset();
                }
                else
                {
                    MessageBox.Show("Record cannot update.");
                }
            }

            dgv_ralist.DataSource = get_ralist();
        }

        private void btn_racancel_Click(object sender, EventArgs e)
        {
            raisnew = false;
            form_raclear();
            form_rareset();
        }

        private void dgv_ralist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            form_ratoedit();

            int row = dgv_ralist.CurrentCell.RowIndex;

            String code = dgv_ralist.Rows[row].Cells[0].Value.ToString().Trim();
            String desc = dgv_ralist.Rows[row].Cells[1].Value.ToString().Trim();

            txt_racode.Text = code;
            txt_raname.Text = desc;
        }

        private void dgv_hilist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_ralist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
