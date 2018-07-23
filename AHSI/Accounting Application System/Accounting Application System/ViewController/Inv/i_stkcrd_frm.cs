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
    public partial class i_stkcrd_frm : Form
    {
        dbInv db;
        GlobalClass gc;

        public i_stkcrd_frm()
        {
            InitializeComponent();

            db = new dbInv();
            gc = new GlobalClass();

        }

        void i_stkcrd_frm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        public void getList()
        {
            dgv_list.Refresh();
            DataTable dt;
                        
            try
            {
                var code = txt_itemcode.Text;

                if (String.IsNullOrEmpty(code) == false)
                {
                    dt = get_stkinv_item_by_item_code(code);
               
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["item_code"].Value = dt.Rows[r]["item_code"].ToString();
                        row.Cells["part_no"].Value = dt.Rows[r]["part_no"].ToString();
                        row.Cells["item_desc"].Value = dt.Rows[r]["item_desc"].ToString();
                        row.Cells["unit"].Value = dt.Rows[r]["unit"].ToString();
                        row.Cells["trnx_date"].Value = dt.Rows[r]["trnx_date"].ToString();
                        row.Cells["reference"].Value = dt.Rows[r]["reference"].ToString();
                        row.Cells["qty_in"].Value =  dt.Rows[r]["qty_in"].ToString();
                        row.Cells["qty_out"].Value = dt.Rows[r]["qty_out"].ToString();
                        row.Cells["fcp"].Value = dt.Rows[r]["fcp"].ToString();
                        row.Cells["price"].Value = dt.Rows[r]["price"].ToString();
                        row.Cells["whs_code"].Value = dt.Rows[r]["whs_code"].ToString();
                        row.Cells["supl_code"].Value = dt.Rows[r]["supl_code"].ToString();
                        row.Cells["supl_name"].Value = dt.Rows[r]["supl_name"].ToString();
                        row.Cells["trn_type"].Value = dt.Rows[r]["trn_type"].ToString();
                        row.Cells["cht_code"].Value = dt.Rows[r]["cht_code"].ToString();
                        row.Cells["cnt_code"].Value = dt.Rows[r]["cnt_code"].ToString();
                        row.Cells["unit_shortcode"].Value = dt.Rows[r]["unit_shortcode"].ToString();
                    }                   
                }
            }
            catch (Exception) {  }
        }

        public DataTable get_stkinv_item_by_item_code(String itemCode) //for stocks alone
        {
            return db.QueryBySQLCode("SELECT it.part_no,s.item_code, s.item_desc, s.unit, TO_CHAR(s.trnx_date, 'MM/DD/YYYY') AS trnx_date, s.reference, s.po_so, s.qty_in, s.qty_out, s.fcp, price, s.whs_code, s.supl_code, s.supl_name, s.trn_type, s.cht_code, s.cnt_code, i.unit_shortcode, w.whs_desc FROM rssys.stkcrd s LEFT JOIN rssys.itmunit i ON s.unit=i.unit_id LEFT JOIN rssys.whouse w ON w.whs_code=s.whs_code LEFT JOIN rssys.items it ON it.item_code = s.item_code WHERE s.item_code='" + itemCode + "' ORDER BY s.trnx_date, s.trn_type;");
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            z_ItemSearch frm = new z_ItemSearch(this, txt_itemdesc.Text, "D");
            frm.ShowDialog();
        }

        public void set_itemselected(String code, String partno, String tdesc)
        {
            decimal currQTY1 = 0, currQTY2 = 0, QTYtotal = 0;
            dgv_list.Rows.Clear();

            txt_itemcode.Text = code;
            txt_partno.Text = partno;
            txt_itemdesc.Text = tdesc;

            getList();

            foreach (DataGridViewRow d in dgv_list.Rows)
            {
                currQTY1 += Convert.ToDecimal(d.Cells["qty_in"].Value);
                currQTY2 += Convert.ToDecimal(d.Cells["qty_out"].Value);
            }

            QTYtotal = currQTY1 - currQTY2;
            txt_totalqty.Text = QTYtotal.ToString();
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
