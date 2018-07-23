using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    public partial class z_add_item_from_PR : Form
    {
        i_PO _frm_po;
        int lnNum = 0;
        String _pr_code = null;

        public z_add_item_from_PR(i_PO po, int ln_num)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();

            gc.load_pr_code(cbo_code);

            _frm_po = po;
            lnNum = ln_num;

            dgv_list.AutoGenerateColumns = false;
            this.Load += z_add_item_from_PR_Load;
            btn_search.Click += btn_search_Click;
        }

        void z_add_item_from_PR_Load(object sender, EventArgs e)
        {
        }

        void btn_search_Click(object sender, EventArgs e)
        {
            getItemList();
        }

        private void getItemList()
        {
            thisDatabase db = new thisDatabase();

            try { if (cbo_code.SelectedIndex > -1) { _pr_code = cbo_code.SelectedValue.ToString(); } }
            catch (Exception) { }

            try
            {
                if (String.IsNullOrEmpty(_pr_code) == false)
                {
                    DataTable dt = db.get_purhace_request_items(_pr_code);
                    dgv_list.DataSource = dt;
                }
            }
            catch (Exception) { }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            _frm_po.set_dgv_fromPO(_pr_code);
            this.Close();
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
