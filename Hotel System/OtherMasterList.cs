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
    public partial class OtherMasterList : Form
    {
        Boolean msisnew = false, taisnew = false, cisnew = false, hisnew = false, sisnew= false;
        String old_code = "";
        String trv_old = "";
        String sel_old = "";

        public OtherMasterList()
        {
            InitializeComponent();

            GlobalMethod gm = new GlobalMethod();
            gm.load_agency(comboBox1);
        }

        private void OtherMasterList_Load(object sender, EventArgs e)
        {
            form_msreset();
            form_tareset();
            form_creset();

            dgv_mslist.DataSource = get_mslist();
            dgv_talist.DataSource = get_talist();
            dgv_clist.DataSource = get_clist();

            txt_tacode.Hide();
            txt_ccode.Hide();
            load_hotel();
            load_seller();
        }

        //////////// market segment /////////////

        private void form_msreset()
        {
            txt_mscode.Enabled = false;
            txt_msdesc.Enabled = false;
            txt_mscode.ReadOnly = false;

            btn_msnew.Enabled = true;
            btn_msedit.Enabled = false;
            btn_mssave.Enabled = false;
            btn_mscancel.Enabled = false;
        }

        private void form_msnew()
        {
            txt_mscode.Enabled = true;
            txt_msdesc.Enabled = true;

            txt_mscode.ReadOnly = false;

            btn_msnew.Enabled = false;
            btn_msedit.Enabled = false;
            btn_mssave.Enabled = true;
            btn_mscancel.Enabled = true;
        }

        private void form_mstoedit()
        {
            form_msreset();
            btn_msnew.Enabled = false;
            btn_msedit.Enabled = true;

            form_mssetreadonly(true);
        }

        private void form_msedit()
        {
            form_msnew();
            txt_mscode.ReadOnly = true;
            form_mssetreadonly(false);
        }

        private void form_msclear()
        {
            txt_mscode.Text = "";
            txt_msdesc.Text = "";
        }

        private void form_mssetreadonly(Boolean flag)
        {
            txt_msdesc.ReadOnly = flag;
        }

        private DataTable get_mslist()
        {
            try
            {
                thisDatabase db = new thisDatabase();

                return db.QueryOnTableWithParams("market", "*", "", "ORDER BY mkt_code ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }

        private void btn_msnew_Click(object sender, EventArgs e)
        {
            msisnew = true;
            form_msnew();
        }

        private void btn_msedit_Click(object sender, EventArgs e)
        {
            msisnew = false;
            form_msedit();
        }

        private void btn_mssave_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            if (msisnew == true)
            {
                if (db.InsertOnTable("market", "mkt_code, mkt_desc", "'" + txt_mscode.Text + "', '" + txt_msdesc.Text + "'"))
                {
                    MessageBox.Show("New record added successfully.");
                    form_msclear();
                    form_msreset();
                }
                else
                {
                    MessageBox.Show("Duplicate entry.");
                }
            }
            else
            {
                if (db.UpdateOnTable("market", "mkt_desc='" + txt_msdesc.Text + "'", "mkt_code='" + txt_mscode.Text + "'"))
                {
                    MessageBox.Show("Record updated successfully.");
                    form_msclear();
                    form_msreset();
                }
                else
                {
                    MessageBox.Show("Record cannot update.");
                }
            }

            dgv_mslist.DataSource = get_mslist();
        }

        private void btn_mscancel_Click(object sender, EventArgs e)
        {
            msisnew = false;
            form_msclear();
            form_msreset();
        }

        private void dgv_mslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            form_mstoedit();

            int row = dgv_mslist.CurrentCell.RowIndex;

            String mscode = dgv_mslist.Rows[row].Cells[0].Value.ToString().Trim();
            String msdesc = dgv_mslist.Rows[row].Cells[1].Value.ToString().Trim();

            txt_mscode.Text = mscode;
            txt_msdesc.Text = msdesc;
        }

        //////////////// travel agent ///////////////////////

        private void form_tareset()
        {
            txt_taname.Enabled = false;
            txt_tacontact.Enabled = false;

            btn_tanew.Enabled = true;
            btn_taedit.Enabled = false;
            btn_tasave.Enabled = false;
            btn_tacancel.Enabled = false;
            textBox4.Enabled = false;
        }

        private void form_tanew()
        {
            txt_taname.Enabled = true;
            txt_tacontact.Enabled = true;
            textBox4.Enabled = true;

            btn_tanew.Enabled = false;
            btn_taedit.Enabled = false;
            btn_tasave.Enabled = true;
            btn_tacancel.Enabled = true;
        }

        private void form_tatoedit()
        {
            form_tareset();
            btn_tanew.Enabled = false;
            btn_taedit.Enabled = true;

            form_tasetreadonly(true);
        }

        private void form_taedit()
        {
            form_tanew();
            form_tasetreadonly(false);
        }

        private void form_taclear()
        {
            txt_taname.Text = "";
            txt_tacontact.Text = "";
            txt_tacode.Text = "";
            textBox4.Text = "";
        }

        private void form_tasetreadonly(Boolean flag)
        {
            txt_taname.ReadOnly = flag;
            txt_tacontact.ReadOnly = flag;
        }

        private DataTable get_talist()
        {
            try
            {
                thisDatabase db = new thisDatabase();

                return db.QueryOnTableWithParams("travagnt", "trv_code, trv_name, tel_num, com", "", "ORDER BY trv_name ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }

        private void btn_tanew_Click(object sender, EventArgs e)
        {
            taisnew = true;
            form_tanew();
            form_taclear();
        }

        private void btn_taedit_Click(object sender, EventArgs e)
        {
            taisnew = false;
            form_taedit();
        }

        private void btn_tasave_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            try
            {
                Double com = 0.00;
                try { com = Convert.ToDouble(textBox4.Text.ToString()); }
                catch { MessageBox.Show("Enter a valid price."); }
                if (taisnew == true)
                {
                    String pk = db.get_nextincrement(db.get_colval("travagnt", "trv_code", ""));

                    if (db.InsertOnTable("travagnt", "trv_code, trv_name, tel_num, com", "'" + pk + "','" + txt_taname.Text + "', '" + txt_tacontact.Text + "', '" + com + "'"))
                    {
                        MessageBox.Show("New record added successfully.");
                        form_taclear();
                        form_tareset();
                    }
                    else
                    {
                        MessageBox.Show("Duplicate entry.");
                    }
                }
                else
                {
                    if (db.UpdateOnTable("travagnt", "trv_name='" + txt_taname.Text + "', tel_num='" + txt_tacontact.Text + "', com='" + com + "'", "trv_code='" + txt_tacode.Text + "'"))
                    {
                        MessageBox.Show("Record updated successfully.");
                        form_taclear();
                        form_tareset();
                    }
                    else
                    {
                        MessageBox.Show("Record cannot update.");
                    }
                }
                GlobalMethod gm = new GlobalMethod();
                gm.load_agency(comboBox1);
                dgv_talist.DataSource = get_talist();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
            
        }

        private void btn_tacancel_Click(object sender, EventArgs e)
        {
            taisnew = false;
            form_taclear();
            form_tareset();
        }

        private void dgv_talist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            form_tatoedit();

            int row = dgv_talist.CurrentCell.RowIndex;

            String tacode = dgv_talist.Rows[row].Cells[0].Value.ToString().Trim();
            String taname = dgv_talist.Rows[row].Cells[1].Value.ToString().Trim();
            String tacontact = dgv_talist.Rows[row].Cells[2].Value.ToString().Trim();

            txt_tacode.Text = tacode;
            txt_taname.Text = taname;
            txt_tacontact.Text = tacontact;
            textBox4.Text = dgv_talist.Rows[row].Cells[3].Value.ToString().Trim();
        }

        //////////////// company  ///////////////////////

        private void form_creset()
        {
            txt_cname.Enabled = false;
            txt_caddr.Enabled = false;
            txt_ctelno.Enabled = false;
            txt_ccontact.Enabled = false;
            txt_ccntperson.Enabled = false;

            btn_cnew.Enabled = true;
            btn_cedit.Enabled = false;
            btn_csave.Enabled = false;
            btn_ccancel.Enabled = false;
        }

        private void form_cnew()
        {
            txt_cname.Enabled = true;
            txt_caddr.Enabled = true;
            txt_ctelno.Enabled = true;
            txt_ccontact.Enabled = true;
            txt_ccntperson.Enabled = true;

            btn_cnew.Enabled = false;
            btn_cedit.Enabled = false;
            btn_csave.Enabled = true;
            btn_ccancel.Enabled = true;
        }

        private void form_ctoedit()
        {
            form_creset();
            btn_cnew.Enabled = false;
            btn_cedit.Enabled = true;

            form_csetreadonly(true);
        }

        private void form_cedit()
        {
            form_cnew();
            form_csetreadonly(false);
        }

        private void form_cclear()
        {
            txt_cname.Text = "";
            txt_caddr.Text = "";
            txt_ctelno.Text = "";
            txt_ccontact.Text = "";
            txt_ccntperson.Text = "";
            txt_ccode.Text = "";
        }

        private void form_csetreadonly(Boolean flag)
        {
            txt_cname.ReadOnly = flag;
            txt_caddr.ReadOnly = flag;
            txt_ctelno.ReadOnly = flag;
            txt_ccontact.ReadOnly = flag;
            txt_ccntperson.ReadOnly = flag;
        }

        private DataTable get_clist()
        {
            try
            {
                thisDatabase db = new thisDatabase();

                return db.QueryOnTableWithParams("company", "comp_code, comp_name, address1, tel_num, cntc_num1, cntc_pers1", "", "ORDER BY comp_name ASC");
            }
            catch (Exception)
            {
                MessageBox.Show("Error on SQL");
            }

            return null;
        }

        private void btn_cnew_Click(object sender, EventArgs e)
        {
            cisnew = true;
            form_cnew();
        }

        private void btn_cedit_Click(object sender, EventArgs e)
        {
            cisnew = false;
            form_cedit();
        }

        private void btn_csave_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();
            String value = "";

            if (cisnew == true)
            {
                String pk = db.get_nextincrement(db.get_colval("company", "comp_code", ""));
                value = "'" + pk + "','" + txt_cname.Text + "', '" + txt_caddr.Text + "', '" + txt_ctelno.Text + "', '" + txt_ccontact.Text + "', '" + txt_ccntperson.Text + "'";

                if (db.InsertOnTable("company", "comp_code, comp_name, address1, tel_num, cntc_num1, cntc_pers1", value ))
                {
                    MessageBox.Show("New record added successfully.");
                    form_cclear();
                    form_creset();
                }
                else
                {
                    MessageBox.Show("Duplicate entry.");
                }
            }
            else
            {
                value = "comp_name='"+ txt_cname.Text +"', address1='"+ txt_caddr.Text +"', tel_num='"+ txt_ctelno.Text +"', cntc_num1='"+ txt_ccontact.Text +"', cntc_pers1='"+ txt_ccntperson.Text +"'";

                if (db.UpdateOnTable("company", value , "comp_code='"+ txt_ccode.Text +"'"))
                {
                    MessageBox.Show("Record updated successfully.");
                    form_cclear();
                    form_creset();
                }
                else
                {
                    MessageBox.Show("Record cannot update.");
                }
            }

            dgv_clist.DataSource = get_clist();
        }

        private void btn_ccancel_Click(object sender, EventArgs e)
        {
            cisnew = false;
            form_cclear();
            form_creset();
        }

        private void dgv_clist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            form_ctoedit();

            int row = dgv_clist.CurrentCell.RowIndex;

            String ccode = dgv_clist.Rows[row].Cells[0].Value.ToString();
            String cname = dgv_clist.Rows[row].Cells[1].Value.ToString();
            String caddr = dgv_clist.Rows[row].Cells[2].Value.ToString();
            String ctel = dgv_clist.Rows[row].Cells[3].Value.ToString();
            String cctno = dgv_clist.Rows[row].Cells[4].Value.ToString();
            String cctpe = dgv_clist.Rows[row].Cells[5].Value.ToString();

            txt_ccode.Text = ccode;
            txt_cname.Text = cname;
            txt_caddr.Text = caddr;
            txt_ctelno.Text = ctel;
            txt_ccontact.Text = cctno;
            txt_ccntperson.Text = cctpe;
        }

        private void dgv_mslist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgv_clist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;

            textBox3.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Text = "";
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hisnew = true;
            button3.Enabled = true;
            button2.Enabled = true;
            button1.Enabled = true;

            textBox3.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                hisnew = false;
                textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();


                old_code = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            }
            catch
            {
                old_code = "";
                MessageBox.Show("Select 1 record.");
            }
        }

        private void load_hotel()
        {
            try
            {
                thisDatabase dbs = new thisDatabase();
                dataGridView1.DataSource = dbs.QueryBySQLCode("SELECT code, name FROM rssys.hotel ORDER BY code ASC");
            }
            catch
            {

            }
        }

        private void load_seller()
        {
            try
            {
                thisDatabase dbs = new thisDatabase();
                dataGridView2.DataSource = dbs.QueryBySQLCode("SELECT trv_code, seller FROM rssys.seller ORDER BY seller ASC");
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thisDatabase dbs = new thisDatabase();
            String col = ((hisnew) ? "code, name" : "code = '" + textBox3.Text.ToString() + "', name = '" + textBox2.Text.ToString() + "'");
            String val = ((hisnew) ? "'" + textBox3.Text.ToString() + "', '" + textBox2.Text.ToString() + "'" : "code = '" + old_code + "'");

            Boolean stat = ((hisnew) ? dbs.InsertOnTable("hotel", col, val) : dbs.UpdateOnTable("hotel", col, val));

            if (stat)
            {
                MessageBox.Show("Successfully "+((hisnew) ? "added new" : "updated")+" entry to hotel");
            }
            else
            {
                MessageBox.Show("Error on " + ((hisnew) ? "adding new" : "updating") + " entry to hotel");
            }
            load_hotel();
        }

        private void textBox3_MouseHover(object sender, EventArgs e)
        {
            old_code = textBox3.Text.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                sisnew = false;
                comboBox1.SelectedValue = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();
                textBox6.Text = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString();

                trv_old = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();
                sel_old = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString();
            }
            catch
            {
                trv_old = "";
                sel_old = "";
                MessageBox.Show("Select 1 record.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sisnew = true;
            button6.Enabled = true;
            button5.Enabled = true;

            comboBox1.Enabled = true;
            textBox6.Enabled = true;
            comboBox1.SelectedIndex = -1;
            textBox6.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            thisDatabase dbs = new thisDatabase();
            String col = ((sisnew) ? "trv_code, seller" : "trv_code = '" + (comboBox1.SelectedValue ?? "").ToString() + "', seller = '" + textBox6.Text.ToString() + "'");
            String val = ((sisnew) ? "'" + (comboBox1.SelectedValue ?? "").ToString() + "', '" + textBox6.Text.ToString() + "'" : "trv_code = '" + trv_old + "', seller = '" + sel_old + "'");

            Boolean stat = ((sisnew) ? dbs.InsertOnTable("seller", col, val) : dbs.UpdateOnTable("seller", col, val));

            if (stat)
            {
                MessageBox.Show("Successfully " + ((sisnew) ? "added new" : "updated") + " entry to seller");
            }
            else
            {
                MessageBox.Show("Error on " + ((sisnew) ? "adding new" : "updating") + " entry to seller");
            }
            load_seller();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button5.Enabled = false;

            comboBox1.Enabled = false;
            textBox6.Enabled = false;
            comboBox1.SelectedIndex = -1;
            textBox6.Text = "";
        }
    }
}
