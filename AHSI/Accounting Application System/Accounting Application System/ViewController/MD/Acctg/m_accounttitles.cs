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
    public partial class m_accounttitles : Form
    {
        private thisDatabase db = new thisDatabase();
        private GlobalClass gc = new GlobalClass();
        Boolean seltbp = false;
        Boolean isnew = false;

        public m_accounttitles()
        {

            InitializeComponent();

            gc.load_subaccount(cbo_mag_code);
            thisDatabase db = new thisDatabase();
            String grp_id = "";
            DataTable dt = db.QueryBySQLCode("SELECT * from rssys.x08 WHERE uid='" + GlobalClass.username + "'");
            if (dt.Rows.Count > 0)
            {
                grp_id = dt.Rows[0]["grp_id"].ToString();
            }
            DataTable dt2 = db.QueryBySQLCode("SELECT a.*, b.* FROM rssys.x06 a LEFT JOIN rssys.x05 b ON a.mod_id = b.mod_id  WHERE a.grp_id = '" + grp_id + "' AND a.mod_id='M0105' ORDER BY b.pla, b.mod_id");

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
            disp_dgvlist();
        }

        private void m_accounttitles_Load(object sender, EventArgs e)
        {

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
            txt_code.Enabled = false;
            frm_clear();
            disp_info();
            goto_tbcntrl_info();
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently disabled.");
            /*
            thisDatabase db = new thisDatabase();
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
            rpt.print_mdata(1004);
            rpt.Show();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String code, at_desc, bs_pl, dr_cr, sl = "N", cib_acct = "N", payment = "N", acc_code;
            String col = "", val = "", add_col = "", add_val = "";
            String table = "m04";
            Boolean success = false;


            if (String.IsNullOrEmpty(txt_code.Text) || String.IsNullOrEmpty(txt_at_desc.Text) || cbo_mag_code.SelectedIndex == -1)
            {
                MessageBox.Show("Pls enter the required fields.");
            }
            else
            {
                code = txt_code.Text;
                at_desc = txt_at_desc.Text.ToUpper();
                acc_code = cbo_mag_code.SelectedValue.ToString();
                bs_pl = db.get_bs_pl(acc_code);
                dr_cr = db.get_dr_cr_typ(acc_code);                

                if (chk_cib_acct.Checked == true)
                {
                    cib_acct = "Y";
                }

                if (chk_sl.Checked == true)
                {
                    sl = "Y";
                }
                if (chk_payment.Checked == true)
                {
                    payment = "Y";
                }

                if (isnew)
                {
                    //code = db.get_pk("c_code");
                    col = "at_code, at_desc, bs_pl, dr_cr, sl, cib_acct, acc_code, payment";
                    val = "'" + code + "', " + db.str_E(at_desc) + ", '" + bs_pl + "', '" + dr_cr + "', '" + sl + "', '" + cib_acct + "', '" + acc_code + "','" + payment + "'";
                    
                    if (db.InsertOnTable(table, col, val))
                    {
                        //db.set_pkm99("at_code", db.get_nextincrementlimitchar(code, 6));
                        success = true;
                    }
                    else
                    {
                        //db.DeleteOnTable(table, "at_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }
                }
                else
                {
                    col = "at_code='" + code + "', at_desc=" + db.str_E(at_desc) + ", bs_pl='" + bs_pl + "', dr_cr='" + dr_cr + "', sl='" + sl + "', cib_acct='" + cib_acct + "', acc_code='" + acc_code + "',payment='" + payment + "'";

                    if (db.UpdateOnTable(table, col, "at_code='" + code + "'"))
                    {
                        success = true;
                        //success
                    }
                    else
                    {
                        MessageBox.Show("Failed on saving.");
                    }
                }

                disp_dgvlist();
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
            txt_at_desc.Text = "";

            cbo_mag_code.SelectedIndex = -1;
            chk_sl.Checked = false;
            chk_cib_acct.Checked = false;
            chk_payment.Checked = false;
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
            txt_at_desc.Enabled = flag;
            chk_sl.Enabled = flag;
            chk_cib_acct.Enabled = flag;
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

        private void disp_dgvlist()
        {
            DataTable dt = db.get_accounttitlelist();

            clear_dgv();
            
            try
            {
                for (int r = 0; dt.Rows.Count > r; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["at_code"].Value = dt.Rows[r]["at_code"].ToString();
                    row.Cells["at_desc"].Value = dt.Rows[r]["at_desc"].ToString();
                    row.Cells["bs_pl"].Value = dt.Rows[r]["bs_pl"].ToString();
                    row.Cells["dr_cr"].Value = dt.Rows[r]["dr_cr"].ToString();
                    row.Cells["sl"].Value = dt.Rows[r]["sl"].ToString();
                    row.Cells["cib_acct"].Value = dt.Rows[r]["cib_acct"].ToString();
                    row.Cells["acc_code"].Value = dt.Rows[r]["acc_code"].ToString();
                    row.Cells["payment"].Value = dt.Rows[r]["payment"].ToString();

                    
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); } 
        }

        private void disp_info()
        {
            try
            {
                int r = dgv_list.CurrentRow.Index;
                String cib_acct = "N", sl = "N", acc_code = "", payment = "N";

                txt_code.Text = dgv_list["at_code", r].Value.ToString();
                txt_at_desc.Text = dgv_list["at_desc", r].Value.ToString();
                
                cib_acct =  dgv_list["cib_acct", r].Value.ToString();
                sl =  dgv_list["sl", r].Value.ToString();
                acc_code = dgv_list["acc_code", r].Value.ToString();
                payment = dgv_list["payment", r].Value.ToString();
                if (cib_acct == "Y")
                {
                    chk_cib_acct.Checked = true;
                }
                if(sl == "Y")
                {
                    chk_sl.Checked = true;
                }
                if (payment == "Y")
                {
                    chk_payment.Checked = true;
                }

                if(String.IsNullOrEmpty(acc_code) == false)
                {
                    cbo_mag_code.SelectedValue = acc_code;
                }                
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_filename.Text = openFile.FileName;
                btn_import.Enabled = true;
            }
            else
            {
                btn_import.Enabled = false;
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

            String code = "", acc_desc = "", acc_code = "", cmp_code = "", cmp_desc = "", mag_code = "", mag_desc = "", accttype_code = "", at_code = "", at_desc = "", bs_pl = "", dr_cr = "", sl = "", payment = "";
            String col = "", val = "", add_col = "", add_val = "";
            String table = "m06";

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
                    btn_import.Invoke(new Action(() =>
                    {
                        btn_import.Enabled = false;
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
                db.QueryBySQLCode("TRUNCATE TABLE rssys.m04;TRUNCATE TABLE rssys.m03;TRUNCATE TABLE rssys.m03;TRUNCATE TABLE rssys.m02;TRUNCATE TABLE rssys.m01;");
                for (rCnt = 1; rCnt <= rw; rCnt++)
                {
                    at_code = Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2);
                    if (!String.IsNullOrEmpty(at_code))
                    {
                        if (at_code.Length <= 10)
                        {
                            acc_code = Convert.ToString((range.Cells[rCnt, 3] as Excel.Range).Value2);
                            acc_desc = Convert.ToString((range.Cells[rCnt, 4] as Excel.Range).Value2).ToUpper();
                            cmp_code = Convert.ToString((range.Cells[rCnt, 5] as Excel.Range).Value2);
                            cmp_desc = Convert.ToString((range.Cells[rCnt, 6] as Excel.Range).Value2).ToUpper();
                            mag_code = Convert.ToString((range.Cells[rCnt, 7] as Excel.Range).Value2);
                            mag_desc = Convert.ToString((range.Cells[rCnt, 8] as Excel.Range).Value2).ToUpper();
                            accttype_code = "";
                            //at_code = Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2);
                            at_desc = Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2).ToUpper();
                            bs_pl = Convert.ToString((range.Cells[rCnt, 9] as Excel.Range).Value2 ?? "");
                            dr_cr = Convert.ToString((range.Cells[rCnt, 10] as Excel.Range).Value2 ?? "");
                            sl = Convert.ToString((range.Cells[rCnt, 11] as Excel.Range).Value2 ?? "N");
                            payment = Convert.ToString((range.Cells[rCnt, 12] as Excel.Range).Value2 ?? "N");

                            code = db.get_colval("m01", "mag_code", "mag_code='" + mag_code + "'");
                            if (String.IsNullOrEmpty(code))
                            {
                                accttype_code = db.get_colval("m00", "code", "" + db.str_E(mag_desc) + " LIKE '%'|| name || '%'");
                                col = "accttype_code,mag_code,mag_desc";
                                val = "'" + accttype_code + "','" + mag_code + "'," + db.str_E(mag_desc) + "";
                                db.InsertOnTable("m01", col, val);
                            }

                            code = db.get_colval("m02", "cmp_code", "cmp_code='" + cmp_code + "'");
                            if (String.IsNullOrEmpty(code))
                            {
                                col = "mag_code,cmp_code,cmp_desc";
                                val = "'" + mag_code + "','" + cmp_code + "'," + db.str_E(cmp_desc) + "";
                                db.InsertOnTable("m02", col, val);
                            }

                            code = db.get_colval("m03", "acc_code", "acc_code='" + acc_code + "'");
                            if (String.IsNullOrEmpty(code))
                            {
                                col = "cmp_code,acc_code,acc_desc,dr_cr";
                                val = "'" + cmp_code + "','" + acc_code + "'," + db.str_E(acc_desc) + ",'" + dr_cr + "'";
                                db.InsertOnTable("m03", col, val);
                            }

                            col = "acc_code,at_code,at_desc,bs_pl,dr_cr,sl,payment,cib_acct";
                            val = "'" + acc_code + "','" + at_code + "'," + db.str_E(at_desc) + ",'" + bs_pl + "','" + dr_cr + "','" + sl + "','" + payment + "',''";

                            if (db.InsertOnTable("m04", col, val))
                            {
                                count++;
                                //db.set_pkm99("d_code", db.get_nextincrementlimitchar(code, 6));
                                inc_pbar(count, rw);
                                lbl_min.Invoke(new Action(() =>
                                {
                                    lbl_min.Text = count.ToString();
                                }));
                            }
                        }
                     }
                    /*
		                m03.acc_code[3],m03.acc_desc[4]
                        m02.cmp_code[5],m02.cmp_desc[6]
		                m01.mag_code[7],m01.mag_code[8]
		                m04.at_code[1],m04.at_desc[2], m04.bs_pl[9], m04.dr_cr[10],m04.sl[11],m04.payment[12]
                     */
                }
                MessageBox.Show("Number of rows inserted : " + (count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"\nOr some problem of your data.");
            }

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

        private void btn_import_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(openFile.FileName.ToString()))
            {
                MessageBox.Show("Please select and excel file to import.");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to import this excel?\nAccount Title will be reset base on imported Account Title.", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    importBgWorker.RunWorkerAsync();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_import.Enabled = true;
            seltbp = true;
            tbcntrl_main.SelectedTab = tpg_import;
            tbcntrl_option.SelectedTab = tpg_opt_3;

            tpg_import.Show();
            tpg_opt_3.Show();
            seltbp = false;

            btn_import.Enabled = false;
            txt_filename.Text = ".";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            goto_tbcntrl_list();
            tpg_info_enable(false);
            frm_clear();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
        }

        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "at_desc";

            try
            {
                searchValue = searchValue.ToUpper();

                if (cbo_searchby.SelectedIndex == 0)
                {
                    typname = "at_code";
                }
                else
                {
                    typname = "at_desc";
                }
                
 
                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                        .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                        .First();
                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;

                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[2];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[1];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[0];

                dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            catch (Exception) { }
        }
    }
}
