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
    public partial class m_outlet : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        GlobalClass gc;
        GlobalMethod gm;
        dbSales db;

        public m_outlet()
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            db = new dbSales();
            
            //For Folio Charge for Hotel.
            //gc.load_charge(cbo_chg_code);
            gc.load_whouse(cbo_whs);
            gc.load_costcenter(cbo_cc_code);
            gc.load_branch(cbo_branch);
            gc.load_outlet_type(cbo_outtype);
            thisDatabase db2 = new thisDatabase();
            String grp_id = "";
            DataTable dt = db2.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db2.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0401' ORDER BY b.pla, b.mod_id");

            if (dt2.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt2.Rows[0]["add"].ToString();
                update = dt2.Rows[0]["upd"].ToString();
                delete = dt2.Rows[0]["cancel"].ToString();
                print = dt2.Rows[0]["print"].ToString();

                if (add == "n")
                {
                    btn_additem.Enabled = false;
                }
                if (update == "n")
                {
                    btn_upditem.Enabled = false;
                }
                if (delete == "n")
                {
                    btn_delitem.Enabled = false;
                }
                if (print == "n")
                {
                    btn_print.Enabled = false;
                }

            }
            disp_list();
        }

        private void m_outlet_Load(object sender, EventArgs e)
        {
            tpg_info_enable(false);     
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            if(isnew)
                txt_ord_prefix.Text = String.Format("{0:00}", (db.get_noOfOutlet() + 1));
            
            txt_code.Text = "";
            txt_desc.Text = "";
            txt_ord.Text = "000001";
            //cbo_chg_code.SelectedIndex = -1;
            txt_gov.Text = "0.00";
            cbo_scc_code.SelectedIndex = -1;
            txt_service.Text = "0.00";
            cbo_whs.SelectedIndex = -1;
            txt_search.Text = "";
            cbo_outtype.SelectedIndex = -1;
        }

        private void goto_tbcntrl_info()
        {
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_info;
            tbcntrl_option.SelectedTab = tpg_opt_2;

            tpg_info.Show();
            tpg_opt_2.Show();
            seltbp = false;
        }

        private void goto_tbcntrl_list()
        {
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_list;
            tbcntrl_option.SelectedTab = tpg_opt_1;

            tpg_list.Show();
            tpg_opt_1.Show();
            seltbp = false;
        }

        private void tpg_info_enable(Boolean flag)
        {
            txt_code.Enabled = flag;
            txt_desc.Enabled = flag;
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            isnew = false;
            tpg_info_enable(true);
            frm_clear();
            disp_info();
            goto_tbcntrl_info();
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This section has been disabled");
            //int r;

            //if (dgv_list.Rows.Count > 1)
            //{
            //    r = dgv_list.CurrentRow.Index;

            //    if (db.UpdateOnTable("brand", "cancel='Y'", "brd_code='" + dgv_list["ID", r].Value.ToString() + "'"))
            //    {
            //        disp_list();
            //        goto_tbcntrl_list();
            //        tpg_info_enable(false);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Failed on deleting.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No rows selected.");
            //}
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4017);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            String code, desc, whs_code = "", chg_code = "001", ord_code = "", govt_pct = "", serv_pct = "", scc_code = "", branch = "", outcode = "", warranty = "false";
            String col = "", val = "", add_col = "", add_val = "";

            if (String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Outlet Code is required");
            }
            else if(String.IsNullOrEmpty(txt_desc.Text))
            {
                MessageBox.Show("Outlet Description is required");
            }
            else if(cbo_cc_code.SelectedIndex == -1)
            {
                MessageBox.Show("Cost Center is required");
            }
            else if (cbo_scc_code.SelectedIndex == -1)
            {
                MessageBox.Show("Sub Cost Center is required");
            }
            else if (cbo_whs.SelectedIndex == -1)
            {
                MessageBox.Show("Warehouse is required");
            }
            else if (cbo_branch.SelectedIndex == -1)
            {
                MessageBox.Show("Branch is required");
            }
            else if (cbo_outtype.SelectedIndex == -1)
            {
                MessageBox.Show("Outlet type is required");
            }
            else
            {
                code = txt_code.Text;
                desc = txt_desc.Text;
                ord_code = txt_ord_prefix.Text + "" + txt_ord.Text;
                whs_code = cbo_whs.SelectedValue.ToString();
                scc_code = cbo_scc_code.SelectedValue.ToString();
                govt_pct = gm.toNormalDoubleFormat(txt_gov.Text).ToString("0.00");
                serv_pct = gm.toNormalDoubleFormat(txt_service.Text).ToString("0.00");
                branch = cbo_branch.SelectedValue.ToString();
                outcode = cbo_outtype.SelectedValue.ToString();

                if(chk_warranty.Checked == true && cbo_outtype.SelectedValue.ToString() == "R")
                {
                    warranty = "true";
                }

                if (isnew)
                {
                    code = txt_code.Text;//db.get_pk("brd_code");
                    col = "out_code,  out_desc,  whs_code,  chg_code,  ord_code,  govt_pct,  serv_pct,  scc_code, branch, ottyp, warranty";
                    val = "'" + code + "', " + db.str_E(desc) + ", '" + whs_code + "', '" + chg_code + "', '" + ord_code + "', '" + govt_pct + "', '" + serv_pct + "', '" + scc_code + "', '" + branch + "', '" + outcode + "', '" + warranty + "'";
                    
                    if (db.InsertOnTable("outlet", col, val))
                    {
                        success = true;
                        //db.set_pkm99("brd_code", db.get_nextincrementlimitchar(code, 3));
                    }
                    else
                    { 
                        success = false;
                        //db.DeleteOnTable("outlet", "out_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "out_code='" + code + "', out_desc=" + db.str_E(desc) + ", whs_code='" + whs_code + "', chg_code='" + chg_code + "', ord_code='" + ord_code + "', govt_pct='" + govt_pct + "', serv_pct='" + serv_pct + "', scc_code='" + scc_code + "', branch='" + branch + "', ottyp='" + outcode + "', warranty='" + warranty + "'";

                    if (db.UpdateOnTable("outlet", col, "out_code='" + code + "'"))
                    {
                       success = true;
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Failed on saving.");
                    }
                }

                if (success)
                {
                    disp_list();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                    frm_clear();
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }
        
        private void clear_dgv()
        {
            try
            {
                dgv_list.Rows.Clear();
            }
            catch (Exception)
            { }
        }

        private void disp_list()
        {
            DataTable dt = db.get_outlet_list();

            clear_dgv();

            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];
                    
                    row.Cells["dgvi_out_code"].Value = dt.Rows[r]["out_code"].ToString();
                    row.Cells["dgvi_out_desc"].Value = dt.Rows[r]["out_desc"].ToString();
                    row.Cells["dgvi_ord_code"].Value = dt.Rows[r]["ord_code"].ToString();
                    row.Cells["dgvi_govt_pct"].Value = gm.toNormalDoubleFormat(dt.Rows[r]["govt_pct"].ToString()).ToString("0.00");
                    row.Cells["dgvi_serv_pct"].Value = gm.toNormalDoubleFormat(dt.Rows[r]["serv_pct"].ToString()).ToString("0.00");
                    //row.Cells["dgvi_chg_code"].Value = dt.Rows[r]["chg_code"].ToString();
                    row.Cells["dgvi_whs_code"].Value = dt.Rows[r]["whs_code"].ToString();
                    row.Cells["dgvi_scc_code"].Value = dt.Rows[r]["scc_code"].ToString();
                    row.Cells["dgvi_cc_code"].Value = dt.Rows[r]["cc_code"].ToString();
                    row.Cells["dgvi_branch"].Value = dt.Rows[r]["branch"].ToString();
                    row.Cells["dgvi_outtype"].Value = dt.Rows[r]["ottyp"].ToString();
                    row.Cells["dgvi_warranty"].Value = Convert.ToBoolean(dt.Rows[r]["warranty"].ToString());
                }
            }
            catch (Exception) { }
        }

        private void disp_info()
        {
            String scc_code = "", whs_code = "", cc_code = "", branch = "",ocode="";

            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_code.Text = dgv_list["dgvi_out_code", r].Value.ToString();
                txt_desc.Text = dgv_list["dgvi_out_desc", r].Value.ToString();
                txt_ord_prefix.Text = dgv_list["dgvi_ord_code", r].Value.ToString().Remove(2);
                txt_ord.Text = (dgv_list["dgvi_ord_code", r].Value != null) ? dgv_list["dgvi_ord_code", r].Value.ToString().Substring(2) : "";
                txt_gov.Text = (dgv_list["dgvi_govt_pct", r].Value != null) ? gm.toNormalDoubleFormat(dgv_list["dgvi_govt_pct", r].Value.ToString()).ToString("0.00") : "0.00";
                txt_service.Text = (dgv_list["dgvi_serv_pct", r].Value != null) ? gm.toNormalDoubleFormat(dgv_list["dgvi_serv_pct", r].Value.ToString()).ToString("0.00") : "0.00";
                
                whs_code = gm.toStr(dgv_list["dgvi_whs_code", r].Value.ToString());
                scc_code = gm.toStr(dgv_list["dgvi_scc_code", r].Value.ToString());
                cc_code = gm.toStr(dgv_list["dgvi_cc_code", r].Value.ToString());
                branch =  gm.toStr(dgv_list["dgvi_branch", r].Value.ToString());
                ocode = gm.toStr(dgv_list["dgvi_outtype",r].Value.ToString());

                if(String.IsNullOrEmpty(whs_code))
                    cbo_whs.SelectedIndex = -1;
                else
                    cbo_whs.SelectedValue = whs_code;

                if (String.IsNullOrEmpty(cc_code))
                    cbo_cc_code.SelectedIndex = -1;
                else
                    cbo_cc_code.SelectedValue = cc_code;
                if (String.IsNullOrEmpty(ocode))
                    cbo_outtype.SelectedIndex = -1;
                else
                    cbo_outtype.SelectedValue = ocode;
                if(String.IsNullOrEmpty(scc_code))
                    cbo_scc_code.SelectedIndex = -1;
                else
                    cbo_scc_code.SelectedValue = scc_code;
                
                if (String.IsNullOrEmpty(branch))
                    cbo_branch.SelectedIndex = -1;
                else
                    cbo_branch.SelectedValue = cc_code;

                chk_warranty.Checked = Convert.ToBoolean(dgv_list["dgvi_warranty", r].Value.ToString()); ;
            }
            catch { }
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void cbo_cc_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cbo_cc_code.SelectedIndex > -1)
                {
                    gc.load_subcostcenter(cbo_scc_code, cbo_cc_code.SelectedValue.ToString());
                }
            }
            catch { }
        }

        private void cbo_whs_SelectedIndexChanged(object sender, EventArgs e)
        {
            String whs_code = "";

            try
            {
                if(cbo_whs.SelectedIndex != -1)
                {
                    whs_code = cbo_whs.SelectedValue.ToString();
                    cbo_branch.SelectedValue = db.get_branchnameOfWhouse(whs_code);
                }
            }
            catch(Exception)
            {

            }
        }

        private void cbo_outtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String warnty = cbo_outtype.SelectedValue.ToString();

                chk_warranty.Checked = false;

                if(warnty == "R")
                {
                    chk_warranty.Enabled = true;
                }
                else
                {
                    chk_warranty.Enabled = false;
                }
            }
            catch { }
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
