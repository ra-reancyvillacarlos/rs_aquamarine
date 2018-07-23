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
    public partial class GenerateRoomCharge : Form
    {
        thisDatabase db = new thisDatabase();
        public String double_posting = "";
        public GenerateRoomCharge()
        {
            InitializeComponent();
           
        }

        private void GenerateRoomCharge_Load(object sender, EventArgs e)
        {
           

            disp_log();
            
            lbl_olddate.Text = Convert.ToDateTime(db.get_systemdate("")).ToString("yyyy-MM-dd");
            lbl_newdate.Text = Convert.ToDateTime(db.get_systemdate("")).AddDays(Convert.ToDouble(1)).ToString("yyyy-MM-dd");
            lbl_sched_day.Text = db.get_pk("day_of_posting");
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            frm_enable(false);
            String current  = DateTime.Now.ToString("yyyy-MM-dd") ;
            String dbdate = db.get_systemdate("");
            if (current == dbdate)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to perform DOUBLE POSTING?", "Posting Notification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.double_posting = "double";
                    backgroundWorker1.RunWorkerAsync();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
               
            }
            
        }

        private void disp_log()
        {
            thisDatabase db = new thisDatabase();

            dgv_history.DataSource = db.QueryOnTableWithParams("romcharge_log", "*", "", " ORDER BY dt_old");
        }

        private void frm_enable(Boolean flag)
        {
            btn_generate.Invoke(new Action(() =>
            {
                btn_generate.Enabled = flag;
            }));
        }

        private void inc_pbar(int i)
        {
            try
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value += i;
                }));
            }
            catch (Exception)
            {
                reset_pbar();
            }
        }

        private void reset_pbar()
        {
            try
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = 0;
                }));
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            DataTable dt = new DataTable();
            String double_new = "",double_old="",trnx="";
            String trnxdate = db.get_systemdate("");
            int nchg = 0;
            int nnochg = 0;
            int i = 1;
               String newdate = lbl_newdate.Text;
            //if(double_posting=="double")
            //{
            //    double_new = DateTime.Parse(lbl_newdate.Text).AddDays(-1).ToString("yyyy-MM-dd");
            //    double_old = DateTime.Parse(lbl_olddate.Text).AddDays(-1).ToString("yyyy-MM-dd");
            //    trnx = DateTime.Parse(trnxdate).AddDays(-1).ToString("yyyy-MM-dd");
            //    db.InsertOnTable("romcharge_log", "dt_old, dt_new, user_id, t_date, t_time", "'" + double_old + "', '" + double_new + "', '" + GlobalClass.username + "', '" + trnx + "', '" + DateTime.Now.ToString("HH:mm") + "'");
            //}
            //else { 
            db.InsertOnTable("romcharge_log", "dt_old, dt_new, user_id, t_date, t_time", "'" + lbl_olddate.Text + "', '" + lbl_newdate.Text + "', '" + GlobalClass.username + "', '" + trnxdate + "', '" + DateTime.Now.ToString("HH:mm") + "'");
            //}
            dt = db.get_guestforcharge();

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Maximum += dt.Rows.Count;
            }));
            //i = dt.Rows.Count/100;

            foreach (DataRow row in dt.Rows)
            {
                inc_pbar(i);
                Boolean isNotM = false;

                if (row["rmrttyp"].ToString() != "M")
                {
                   /* if (DateTime.Parse(row["arr_date"].ToString()).CompareTo(DateTime.Parse(db.get_systemdate(""))) <= 0 && DateTime.Parse(db.get_systemdate("")).CompareTo(DateTime.Parse(row["dep_date"].ToString())) < 0)
                    { */
                        if (db.roomcharge_reg(row["reg_num"].ToString(), "RNT", row["rom_code"].ToString(), "ROOM CHARGE", trnxdate, 0.00, row["rmrttyp"].ToString(), Convert.ToDateTime(row["arr_date"].ToString()), row["sen_disc"].ToString(), double_posting))
                        {
                            isNotM = true;
                        }
                  //  } 
                }

                if (isNotM)
                {
                    nchg++;
                }
                else
                {
                    nnochg++;
                }
            }
            //if(double_posting!="double")
            //{ 
            db.set_pkm99("trnx_date", Convert.ToDateTime(newdate).ToString("yyyy-MM-dd"));
            //}
            reset_pbar();

            frm_enable(true);

            MessageBox.Show("Successfully Generated.\nNo of Rooms charged :" + nchg.ToString() + "\n No of Rooms not charged:" + nnochg.ToString());

            dgv_history.Invoke(new Action(() =>
            {
                dgv_history.DataSource = db.QueryAllOnTable("romcharge_log");
            }));

            lbl_olddate.Invoke(new Action(() =>
            {
                lbl_olddate.Text = db.get_systemdate("");
            }));
            lbl_sched_day.Invoke(new Action(() =>
            {
                lbl_sched_day.Text = db.get_pk("day_of_posting");
            }));

            lbl_newdate.Invoke(new Action(() =>
            {
                lbl_newdate.Text = Convert.ToDateTime(lbl_olddate.Text).AddDays(1).ToString("yyyy-MM-dd");
            }));

            
        }

        private void dgv_history_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pnl_left_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}