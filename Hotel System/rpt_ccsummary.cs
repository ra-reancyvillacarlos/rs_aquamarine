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
    public partial class rpt_ccsummary : Form
    {
        private String shift_code = "";
        private String trnx_dt = "";
        private String dclerk = "";
        private int action = 0; //1=generate, 2=save, 3=print cc report, 4=finalize

        public rpt_ccsummary()
        {
            InitializeComponent();
        }

        private void rpt_ccsummary_Load(object sender, EventArgs e)
        {
            load_userid();

            btn_save.Enabled = false;
            btn_cc.Enabled = false;
            btn_finalized.Enabled = false;

            pnl_1.Hide();
            pnl_2.Hide();
        }

        private void load_userid()
        {
            thisDatabase db = new thisDatabase();
            DataTable dt = db.get_alluserid();
            cbo_cashier.DataSource = dt;
            cbo_cashier.DisplayMember = "uid";
            cbo_cashier.ValueMember = "uid";
            cbo_cashier.SelectedIndex = -1;
        }

        private String get_zero_abs_strval(String val)
        {
            if (String.IsNullOrEmpty(val))
            {
                val = "0.00";
            }
            else
            {
                val = Math.Abs(Convert.ToDouble(val)).ToString("0.00");
            }

            return val;
        }

        private int get_cbo_index(ComboBox cbo)
        {
            int i = -1;

            cbo.Invoke(new Action(() =>
            {
                i = cbo.SelectedIndex;
            }));

            return i;
        }

        private String get_cbo_value(ComboBox cbo)
        {
            String value = "";

            cbo.Invoke(new Action(() =>
            {
                value = cbo.SelectedValue.ToString();
            }));

            return value;
        }

        private String get_cbo_text(ComboBox cbo)
        {
            String txt = "";

            cbo.Invoke(new Action(() =>
            {
                txt = cbo.Text.ToString();
            }));

            return txt;
        }

        private void inc_pbar1(int i)
        {
            pbar_1.Invoke(new Action(() =>
            {
                pbar_1.Value += i;
            }));
        }

        private void reset_pbar1()
        {
            pbar_1.Invoke(new Action(() =>
            {
                pbar_1.Value = 0;
            }));
        }

        private void inc_pbar2(int i)
        {
            pbar_2.Invoke(new Action(() =>
            {
                pbar_2.Value += i;
            }));
        }

        private void reset_pbar2()
        {
            pbar_2.Invoke(new Action(() =>
            {
                pbar_2.Value = 0;
            }));
        }

        private void pbar_panl_hide()
        {
            pnl_1.Invoke(new Action(() =>
            {
                pnl_1.Hide();
            }));

            pnl_2.Invoke(new Action(() =>
            {
                pnl_2.Hide();
            }));
        }

        private void field_enable(Boolean flag)
        {
            dtp_trnxdt.Invoke(new Action(() =>
            {
                dtp_trnxdt.Enabled = flag;
            }));

            cbo_cashier.Invoke(new Action(() =>
            {
                cbo_cashier.Enabled = flag;
            }));

            cbo_shift.Invoke(new Action(() =>
            {
                cbo_shift.Enabled = flag;
            }));

            btn_generate.Invoke(new Action(() =>
            {
                btn_generate.Enabled = flag;
            }));

            btn_save.Invoke(new Action(() =>
            {
                btn_save.Enabled = flag;
            }));

            btn_cc.Invoke(new Action(() =>
            {
                btn_cc.Enabled = flag;
            }));

            btn_finalized.Invoke(new Action(() =>
            {
                btn_finalized.Enabled = flag;
            }));

            btn_extraitem.Invoke(new Action(() =>
            {
                btn_extraitem.Enabled = flag;
            }));
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            pnl_1.Show();
            pnl_2.Show();
            action = 1;
            bgWorker.RunWorkerAsync();
        }

        private void goback()
        {
            tbcntrl_disp.SelectedTab = tpg_1;
            tpg_1.Show();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }

        private void btn_finalized_Click(object sender, EventArgs e)
        {

        }

        private void btn_extraitem_Click(object sender, EventArgs e)
        {
            int r;

            tbcntrl_disp.SelectedTab = tpg_2;
            tpg_2.Show();
        }

        private void btn_extraitem_remove_Click(object sender, EventArgs e)
        {

        }

        private void btn_extraitem_add_Click(object sender, EventArgs e)
        {

        }

        private void btn_extraitem_back_Click(object sender, EventArgs e)
        {
            goback();
        }

        private void btn_extraitem_ok_Click(object sender, EventArgs e)
        {
            goback();
        }

        private void btn_cc_Click(object sender, EventArgs e)
        {

        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void dgv_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
