using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Accounting_Application_System
{
    public partial class m_suppliers : Form
    {
        Boolean seltbp = false;
        Boolean isnew = false;
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();

        //form & other
        i_PO frm_po = null;
        Boolean iscallbackfrm = false;

        public m_suppliers()
        {
            InitializeComponent();

            gc.load_mop(cbo_mop);
            gc.load_account_for_sup_ledger(cbo_subledger);

            init_load();
        }

        private void m_suppliers_Load(object sender, EventArgs e)
        {

        }

        public m_suppliers(i_PO po, Boolean iscallback)
        {

            InitializeComponent();
            frm_po = po;
            iscallbackfrm = iscallback;

            txt_search.Text = frm_po.cbo_suppliername.Text;

            init_load();
            txt_search.Select();
        }

        private void init_load()
        {
            String grp_id = "";
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");

            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0109' ORDER BY b.pla, b.mod_id");

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
            String grp_id2 = "";
            DataTable dt24 = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt24.Rows.Count > 0)
            {
                grp_id2 = dt24.Rows[0]["grp_id"].ToString();
            }
            DataTable dt23 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id2 + "' AND a.mod_id='M0508' ORDER BY b.pla, b.mod_id");

            if (dt23.Rows.Count > 0)
            {
                String add = "", update = "", delete = "", print = "";
                add = dt23.Rows[0]["add"].ToString();
                update = dt23.Rows[0]["upd"].ToString();
                delete = dt23.Rows[0]["cancel"].ToString();
                print = dt23.Rows[0]["print"].ToString();

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


            disp_dgvlist(txt_search.Text);
            disp_dgv_search(txt_search.Text);
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            tpg_info_enable(true);
            frm_clear();
            goto_tbcntrl_info();
            btn_save.Enabled = true;
            btnImport.Enabled = false;
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            isnew = false;
            tpg_info_enable(true);
            txt_code.Enabled = false;
            frm_clear();
            disp_info();
            goto_tbcntrl_info();
            btn_save.Enabled = true;
            btnImport.Enabled = false;
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently Disabled");

            /*thisDatabase db = new thisDatabase();
            int r;

            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;

                //if (db.UpdateOnTable("m07", "cancel='Y'", "c_code='" + dgv_list["ID", r].Value.ToString() + "'"))
                //{
                    disp_dgvlist();
                    goto_tbcntrl_list();
                    tpg_info_enable(false);
                //}
                //else
                //{
                 //   MessageBox.Show("Failed on deleting.");
                //}
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }*/
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.print_mdata(4009);
            rpt.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_dgvlist(txt_search.Text);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String code, c_name, c_addr2, c_tel, c_fax, c_tin, c_cntc, at_code, mp_code, c_oldcode = "";

            String col = "", val = "", add_col = "", add_val = "";
            String table = "m07";

            if (String.IsNullOrEmpty(txt_supplier.Text) || cbo_mop.SelectedIndex == -1 || cbo_subledger.SelectedIndex == -1)
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                code = txt_code.Text;
                c_name = txt_supplier.Text;
                c_addr2 = rtxt_address.Text;
                c_tel = txt_phone.Text;
                c_fax = txt_fax.Text;
                c_tin = txt_tin.Text;
                c_cntc = txt_contactperson.Text;
                mp_code = cbo_mop.SelectedValue.ToString();
                at_code = cbo_subledger.SelectedValue.ToString();
                c_oldcode = (cbo_oldaccount.SelectedValue??"").ToString();
                if (isnew)
                {
                    code = db.get_pk("c_code");
                    col = "c_code, c_oldcode, c_name, c_addr2, c_tel, c_fax, c_tin, c_cntc, at_code, mp_code";
                    val = "'" + code + "','" + c_oldcode + "', '" + c_name + "', '" + c_addr2 + "', '" + c_tel + "', '" + c_fax + "', '" + c_tin + "', '" + c_cntc + "', '" + at_code + "', '" + mp_code + "'";

                    if (db.InsertOnTable(table, col, val))
                    {
                        db.set_pkm99("c_code", db.get_nextincrementlimitchar(code, 6));
                    }
                    else
                    {
                        db.DeleteOnTable(table, "c_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "c_code='" + code + "',c_oldcode='" + c_oldcode + "', c_name='" + c_name + "', c_addr2='" + c_addr2 + "', c_tel='" + c_tel + "', c_fax='" + c_fax + "', c_tin='" + c_tin + "', c_cntc='" + c_cntc + "', at_code='" + at_code + "', mp_code='" + mp_code + "'";

                    if (db.UpdateOnTable(table, col, "c_code='" + code + "'"))
                    {
                        //success
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
                    }
                }
                disp_dgvlist(txt_search.Text);
                goto_tbcntrl_list();
                tpg_info_enable(false);
                frm_clear();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void frm_reset()
        {

        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_contactperson.Text = "";
            txt_supplier.Text = "";
            txt_fax.Text = "";
            txt_phone.Text = "";
            txt_tin.Text = "";

            rtxt_address.Text = "";

            cbo_mop.SelectedIndex = -1;
            cbo_subledger.SelectedIndex = -1;
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
            txt_supplier.Enabled = flag;
            txt_fax.Enabled = flag;
            txt_phone.Enabled = flag;
            txt_tin.Enabled = flag;
            txt_contactperson.Enabled = flag;
            rtxt_address.Enabled = flag;
            cbo_mop.Enabled = flag;
            cbo_subledger.Enabled = flag;
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

        private void disp_dgvlist(String searchtext)
        {
            String WHERE = "";
            clear_dgv();

            if (String.IsNullOrEmpty(searchtext) == false)
            {
                if (cbo_searchby.Text == "Customer ID")
                {
                    WHERE = "c_code LIKE $$%" + searchtext + "%$$";
                }
                else //Customer Name
                {
                    WHERE = "c_name LIKE  $$%" + searchtext + "%$$";
                }
            }

            try
            {
                DataTable dt = db.QueryOnTableWithParams("m07", "*", WHERE, " ORDER BY c_name ASC");

                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["c_code"].Value = dt.Rows[r]["c_code"].ToString();
                    row.Cells["c_name"].Value = dt.Rows[r]["c_name"].ToString();
                    row.Cells["c_addr2"].Value = dt.Rows[r]["c_addr2"].ToString();
                    row.Cells["c_tel"].Value = dt.Rows[r]["c_tel"].ToString();
                    row.Cells["c_fax"].Value = dt.Rows[r]["c_fax"].ToString();
                    row.Cells["c_tin"].Value = dt.Rows[r]["c_tin"].ToString();
                    row.Cells["c_cntc"].Value = dt.Rows[r]["c_cntc"].ToString();
                    row.Cells["at_code"].Value = dt.Rows[r]["at_code"].ToString();
                    row.Cells["mp_code"].Value = dt.Rows[r]["mp_code"].ToString();
                    row.Cells["c_oldcode"].Value = dt.Rows[r]["c_oldcode"].ToString();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;

                txt_code.Text = dgv_list["c_code", r].Value.ToString();
                txt_supplier.Text = dgv_list["c_name", r].Value.ToString();
                rtxt_address.Text = dgv_list["c_addr2", r].Value.ToString();
                txt_phone.Text = dgv_list["c_tel", r].Value.ToString();
                txt_fax.Text = dgv_list["c_fax", r].Value.ToString();
                txt_tin.Text = dgv_list["c_tin", r].Value.ToString();
                txt_contactperson.Text = dgv_list["c_cntc", r].Value.ToString();
                cbo_mop.SelectedValue = dgv_list["mp_code", r].Value.ToString();
                cbo_subledger.SelectedValue = dgv_list["at_code", r].Value.ToString();
                cbo_oldaccount.SelectedValue = dgv_list["c_oldcode", r].Value.ToString();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
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

        private void btn_import_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btnImport.Enabled = true;
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_import;
            tbcntrl_option.SelectedTab = tpg_opt_2;

            tpg_import.Show();
            tpg_opt_2.Show();
            seltbp = false;

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_filename.Text = openFile.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(openFile.FileName.ToString()))
            {
                MessageBox.Show("Please select and excel file to import.");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to import this excel?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    importBgWorker.RunWorkerAsync();
                }
            }
        }

        private void importBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            Boolean success = false;
            String str = "";
            int rCnt = 0;
            int cCnt = 0;
            int rw = 0;
            int cl = 0;
            String filename = "";
            int i = 0;
            int count = 0;
            String code, c_name, c_addr2, c_tel, c_fax, c_tin, c_cntc, at_code, mp_code, at_desc;

            String col = "", val = "", add_col = "", add_val = "";
            String table = "m07";
            

            try
            {
                filename = openFile.FileName;
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(@filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;
                if (rw > 0)
                {
                    btnBrowse.Invoke(new Action(() =>
                    {
                        btnBrowse.Enabled = false;
                    }));
                    btnImport.Invoke(new Action(() =>
                    {
                        btnImport.Enabled = false;
                    }));
                    lbl_max.Invoke(new Action(() =>
                    {
                        lbl_max.Text = (rw - 1).ToString();
                    }));
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Maximum = rw;
                    }));
                    lbl_min.Invoke(new Action(() =>
                    {
                        lbl_min.Text = pbar.Minimum.ToString();
                    }));
                }
                for(rCnt = 1; rCnt <= rw; rCnt++)
                {
                    if (rCnt != 1)
                    {
                        code = db.get_pk("c_code");
                        c_name = getString(range, rCnt, 2);
                        at_desc = getString(range, rCnt, 3); 
                        c_addr2 = getString(range, rCnt, 4); 
                        c_tel = getString(range, rCnt, 5);
                        c_fax = getString(range, rCnt, 6);

                        at_code = db.get_colval("m04", "at_code", "UPPER(at_desc)=" + db.str_E(at_desc) + "");
                        col = "c_code, c_name,c_addr2,c_tel,c_fax,at_code";
                        val = "'" + code + "'," + db.str_E(c_name) + "," + db.str_E(c_addr2) + ",'" + c_tel + "','" + c_fax + "','" + at_code + "'"; 

                        if(db.InsertOnTable(table,col,val))
                        {
                            count++;
                            db.set_pkm99("c_code", db.get_nextincrementlimitchar(code, 6));
                            inc_pbar(count, rw);
                            lbl_min.Invoke(new Action(() => 
                            {
                                lbl_min.Text = count.ToString();
                            }));
                        }
                    }
                }
                MessageBox.Show("Number of rows inserted : " + (count));
                btn_import.Invoke(new Action(() =>
                {
                    btn_import.Enabled = true;
                }));
                pbar.Invoke(new Action(() =>
                {
                    pbar.Value = 0;
                }));
                lbl_min.Invoke(new Action(() =>
                {
                    lbl_min.Text = "0";
                }));
                lbl_max.Invoke(new Action(() =>
                {
                    lbl_max.Text = "0";
                }));
                btnBrowse.Invoke(new Action(() =>
                {
                    btnBrowse.Enabled = true;
                }));
                dgv_list.Invoke(new Action(() => {
                    disp_dgvlist("");
                }));
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        public String getString(Excel.Range range, int row, int col)
        {
            String str = "";
            if (range != null)
            {
                try
                {
                    str = Convert.ToString((range.Cells[row, col] as Excel.Range).Value2 ?? "");
                }
                catch { }
            }
            return str;
        }
        private void inc_pbar(int i, int rw)
        {
            try
            {

                if (pbar.Value <= rw)
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = i;
                    }));

                }
                else
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = rw;
                    }));

                    btnBrowse.Invoke(new Action(() =>
                    {
                        btnBrowse.Enabled = true;
                    }));

                }

            }
            catch (Exception)
            {

            }
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

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }


        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "c_name";

            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "c_code";
                }
                else
                {
                    typname = "c_name";
                }

                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                        .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                        .First();

                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;


                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[9];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[8];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[7];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[6];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[5];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[4];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[3];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[2];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[1];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[0];

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
        }

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (iscallbackfrm)
            {
                int i = dgv_list.CurrentRow.Index;
                if (frm_po != null)
                {
                    frm_po.refresh_customerlist();

                    frm_po.cbo_suppliername.Text = dgv_list["c_name", i].Value.ToString();
                    frm_po.txt_supplierid.Text = dgv_list["c_code", i].Value.ToString();
                    this.Close();
                }
            }
        }
    }
}
