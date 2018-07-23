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
    public partial class s_ROStatus : Form
    {
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;

        public s_ROStatus()
        {
            InitializeComponent();
            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);

            cbo_branch.SelectedValue = GlobalClass.branch;
            cbo_searchby.SelectedIndex = 2;
        }

        private void s_ROStatus_Load(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception) { }

            disp_list();
        }

        private void disp_list()
        {
            DataTable dt;
            String dateFrom = dtp_frm.Value.ToString("yyyy-MM-dd");
            String dateTo = dtp_to.Value.ToString("yyyy-MM-dd");
            String branch = GlobalClass.branch;
            String searchtext = txt_search.Text.Trim();
            String searchtype = "oh.customer";

            if(cbo_branch.SelectedIndex != -1)
            {
                branch = cbo_branch.SelectedValue.ToString();
            }

            if(cbo_searchby.SelectedIndex == 0)
            {
                searchtype = "";
            }
            else if (cbo_searchby.SelectedIndex == 0)
            {
                searchtype = "";
            }
            else if (cbo_searchby.SelectedIndex == 0)
            {
                searchtype = "";
            }
            else if (cbo_searchby.SelectedIndex == 0)
            {
                searchtype = "";
            }
                        
            dgv_list.Rows.Clear();

            try
            {
                String query = "SELECT o.out_desc , oh.ord_code , oh.customer , oh.ord_date , oh.promise_date , oh.promise_time , oh.status , ro.ro_stat_desc,oh.loc, oh.car_plate , oh.car_engine ,  oh.car_vin_num , oh.debt_code , oh.out_code  FROM rssys.orhdr oh LEFT JOIN rssys.outlet o ON oh.out_code=o.out_code LEFT JOIN rssys.ro_status ro ON ro.ro_stat_code = oh.status WHERE oh.pending='Y' AND o.ottyp='R' AND " + searchtype + " LIKE '%" + searchtext + "%' AND oh.branch='" + branch + "' AND oh.ord_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "' ORDER BY oh.ord_date DESC";

                dt = db.QueryBySQLCode("SELECT o.out_desc , oh.ord_code , oh.customer , oh.ord_date , oh.promise_date , oh.promise_time , oh.status , ro.ro_stat_desc,oh.loc, oh.car_plate , oh.car_engine ,  oh.car_vin_num , oh.debt_code , oh.out_code  FROM rssys.orhdr oh LEFT JOIN rssys.outlet o ON oh.out_code=o.out_code LEFT JOIN rssys.ro_status ro ON ro.ro_stat_code = oh.status WHERE oh.pending='Y' AND o.ottyp='R' AND " + searchtype + " LIKE '%" + searchtext + "%' AND oh.branch='" + branch + "' AND oh.ord_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "' ORDER BY oh.ord_date DESC");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int r = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[r];

                    row.Cells["dgvi_outlet"].Value = dt.Rows[i]["out_desc"].ToString();
                    row.Cells["dgvi_code"].Value = dt.Rows[i]["out_code"].ToString();
                    row.Cells["dgvi_customer"].Value = dt.Rows[i]["customer"].ToString();
                    row.Cells["dgvi_date_promise"].Value = dt.Rows[i]["promise_time"].ToString();
                    row.Cells["dgvi_status"].Value = dt.Rows[i]["ro_stat_desc"].ToString();
                    row.Cells["dgvi_status_no"].Value = dt.Rows[i]["status"].ToString();
                    row.Cells["dgvi_whs_code"].Value = dt.Rows[i]["loc"].ToString();
                    row.Cells["dgvli_car_plate"].Value = dt.Rows[i]["car_plate"].ToString();
                    row.Cells["dgvi_ro_date"].Value = dt.Rows[i]["ord_date"].ToString();
                    row.Cells["dgvli_car_plate_type"].Value = dt.Rows[i]["car_plate"].ToString();
                    row.Cells["dgvli_car_engine"].Value = dt.Rows[i]["car_engine"].ToString();
                    row.Cells["dgvli_car_vin_num"].Value = dt.Rows[i]["car_vin_num"].ToString();
                    row.Cells["dgvi_custcode"].Value = dt.Rows[i]["debt_code"].ToString();
                    row.Cells["dgvi_ord_code"].Value = dt.Rows[i]["ord_code"].ToString();
                }
            }
            catch { }            
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_list();
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
