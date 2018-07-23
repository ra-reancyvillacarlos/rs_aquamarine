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
    public partial class z_add_item_from_PO : Form
    {
        List<enterFromPOtoReceiving> fPOtoReceiving = new List<enterFromPOtoReceiving>();
        int i = 0;
        i_ReceivingPurchase _from_receiving;
        int lnNum = 0;
        String _purc_ord = null;

        public z_add_item_from_PO(i_ReceivingPurchase receiving, int ln_num)
        {
            InitializeComponent();

            GlobalClass gc = new GlobalClass();

            gc.load_po_number(cbo_code);

            lnNum = ln_num;
            _from_receiving = receiving;
            dgv_list.AutoGenerateColumns = false;
            btn_search.Click += btn_search_Click;
        }

        void z_add_item_from_PO_Load(object sender, EventArgs e)
        {

        }

        void btn_save_Click(object sender, EventArgs e)
        {
            _from_receiving.set_dgv_fromPO(_purc_ord);
            _from_receiving.disp_total();
            this.Close();
        }

        void btn_search_Click(object sender, EventArgs e)
        {
            getItemList();
        }

        private void getItemList()
        {
            thisDatabase db = new thisDatabase();

            try 
            { 
                if (cbo_code.SelectedIndex > -1) 
                { 
                    _purc_ord = cbo_code.SelectedValue.ToString(); 
                } 
            }
            catch (Exception) { }

            try
            {
                if (String.IsNullOrEmpty(_purc_ord) == false)
                {
                    DataTable dt = db.get_po_items(_purc_ord);
                    dgv_list.DataSource = dt;
                }
            }
            catch (Exception) { }
        }

        private void btn_search_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_search_Click_2(object sender, EventArgs e)
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
