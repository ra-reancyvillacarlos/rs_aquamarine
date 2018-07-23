using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

// Database
using Npgsql;

namespace Accounting_Application_System
{
    public partial class z_SystemDataUpdate : Form
    {
        public z_SystemDataUpdate()
        {
            InitializeComponent();
        }

        private void z_SystemDataUpdate_Load(object sender, EventArgs e)
        {

        }

        private void btn_ping_Click(object sender, EventArgs e)
        {

        }

        private void btn_testconect_Click(object sender, EventArgs e)
        {
            String server = txt_server.Text;
            String user = txt_user.Text;
            String svr_pass = txt_password.Text;
            String lcl_db = "inv_headway";
            String schema = "rssys";

            frm_enable(false);

            conn = new NpgsqlConnection("Server=" + server + ";Port=5432;User Id=postgres;Password=" + svr_pass + ";Database=" + lcl_db + ";");

            OpenConn(conn);

            frm_enable(true);
        }

        private void btn_close1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                externalDatabase.ga_hotelEntities all = new externalDatabase.ga_hotelEntities(thisDatabase.local);
               // externalDatabase.hms_testEntities1 stkcrdAll = new externalDatabase.hms_testEntities1();
                var date = DateTime.Now.Date;

                var items = all.items.Where(p => p.branch != "001" && p.transferred == null).ToList();
                var itmchg = all.itmchgs.Where(p => p.branch != "001" && p.transferred == null).ToList();
                var itmfifo = all.itmfifoes.Where(p => p.trnx_date == date).ToList();
                var itmwhs = all.itmwhs.ToList();
                var rechdr = all.rechdrs.Where(p => p.branch != "001" && p.transferred == null).ToList();
                var reclne = all.reclnes.ToList();
                var stkcrd = all.stkcrds.Where(p => p.branch != "001" && p.transferred == null).ToList();

                int counter = 0;

                if (items != null)
                {
                    counter += items.Count;
                }
                if (itmchg != null)
                {
                    counter += itmchg.Count;
                }
                if (itmfifo != null)
                {
                    counter += itmfifo.Count;
                }
                if (itmwhs != null)
                {
                    counter += itmwhs.Count;
                }
                if (rechdr != null)
                {
                    counter += rechdr.Count;
                }
                //if (reclne != null)
                //{
                //    counter += reclne.Count;
                //}
                if (stkcrd != null)
                {
                    counter += stkcrd.Count;
                }


                conn = new NpgsqlConnection("Server=" + txt_server.Text + ";Port=5432;User Id=postgres;Password=" + txt_password.Text + ";Database=inv_headway;");
                string col = "", val = "";

                if (items != null)
                {
                    col = "item_code, item_desc, stk_item, item_class, unit_cost, sell_pric, gp, reorder, max_level, bin_loc, req_lot, brd_code, item_grp, sales_unit_id, purc_unit_id, sc_price, assembly";

                    foreach (var i in items)
                    {
                        val = "'" + i.item_code + "', '" + i.item_desc + "', '" + i.stk_item + "', '" + i.item_class + "', '" + i.unit_cost
                            + "', '" + i.sell_pric + "', '" + i.gp + "', '" + i.reorder + "', '" + i.max_level + "', '" + i.bin_loc
                            + "', '" + i.req_lot + "', '" + i.brd_code + "', '" + i.item_grp + "', '" + i.sales_unit_id + "', '" + i.purc_unit_id + "', '" + i.sc_price + "', '" + i.assembly + "'";

                        if (InsertOnTable("items", col, val))
                            i.transferred = "T";
                    }
                }
                if (itmchg != null)
                {
                    col = "item_code, item_desc, old_price, new_price, old_pric2, new_pric2, user_id, t_date, t_time, branch, transferred";

                    foreach(var i in itmchg)
                    {
                        val = "'" + i.item_code + "', '" + i.item_desc + "', '" + i.old_price + "', '" + i.new_price + "', '" + i.old_pric2
                                                    + "', '" + i.new_pric2 + "', '" + i.user_id + "', '" + i.t_date + "', '" + i.t_time + "', '" + i.branch
                                                    + "', '" + i.transferred + "'";

                        if (InsertOnTable("itmchg", col, val))
                            i.transferred = "T";
                    }
                    
                }
                if (itmfifo != null)
                {
                    col = "item_code, trn_type, rec_num, trnx_date, org_qty, qty_in, qty_out, price, unit";

                    foreach (var i in itmfifo)
                    {
                        val = "'" + i.item_code + "', '" + i.trn_type + "', '" + i.rec_num + "', '" + i.trnx_date + "', '" + i.org_qty
                            + "', '" + i.qty_in + "', '" + i.qty_in + "', '" + i.qty_out + "', '" + i.price + "', '" + i.unit+ "'";

                        InsertOnTable("itmfifo", col, val);
                    }
                }
                if (itmwhs != null)
                {
                    col = "item_code, whs_code, qty_onhand";

                    foreach (var i in itmwhs)
                    {
                        val = "'" + i.item_code + "', '" + i.whs_code + "', '" + i.qty_onhand + "'";

                        try
                        {

                        InsertOnTable("itmwhs", col, val);

                        }

                        catch
                        {
                            continue;
                        }
                    }
                }
                if (rechdr != null)
                {
                    col = "rec_num, supl_code, supl_name, _reference, trnx_date, whs_code, , jrnlz, cancel, purc_ord, printed, recipient, t_date, t_time, vat_code, act_date, actualinv, branch, transferred,\"locationFrom\",\"locationTo\"";

                    foreach (var i in rechdr)
                    {
                        val = "'" + i.rec_num + "', '" + i.supl_code + "', '" + i.supl_name + "', '" + i.C_reference + "', '" + i.trnx_date
                            + "', '" + i.whs_code + "', '" + i.jrnlz + "', '" + i.cancel + "', '" + i.purc_ord + "', '" + i.printed
                            + "', '" + i.recipient + "', '" + i.t_date + "', '" + i.t_time + "', '" + i.vat_code + "', '" + i.act_date + "', '" 
                            + i.actualinv + "', '" + i.branch + "'" + "', '" + i.transferred + "'" + "', '" + i.locationFrom + "', '" + "', '" + i.locationTo + "'";

                        InsertOnTable("rechdr", col, val);

                        String col2 = "rec_num, ln_num, item_code, unit, recv_qty, price, , ln_amnt, discount, cht_code, cnt_code, proj_code, ln_vat, po_line, debt_code, scc_code, non_vat, org_qty, transpo_cost";

                        foreach (var r in reclne)
                        {
                            if(i.rec_num == r.rec_num)
                            {
                                val = "'" + r.rec_num + "', '" + r.ln_num + "', '" + r.item_code + "', '" + r.unit + "', '" + r.recv_qty
                                + "', '" + r.price + "', '" + r.ln_amnt + "', '" + r.discount + "', '" + r.cht_code + "', '" + r.cnt_code
                                + "', '" + r.proj_code + "', '" + r.ln_vat + "', '" + r.po_line + "', '" + r.debt_code + "', '" + r.scc_code + "', '"
                                + r.non_vat + "', '" + r.org_qty + "'" + "', '" + r.transpo_cost + "'";

                                if (InsertOnTable("reclne", col2, val))
                                    i.transferred = "T";
                            }
                        }
                    }
                }
                if (stkcrd != null)
                {
                    String col2 = "item_code, item_desc, unit, trnx_date, reference, po_so, , qty_in, qty_out, fcp, price, whs_code, supl_code, supl_name, trn_type, cht_code, cnt_code, proj_code, branch, transferred";

                    foreach (var r in stkcrd)
                    {

                        val = "'" + r.item_code + "', '" + r.item_desc + "', '" + r.unit + "', '" + r.trnx_date + "', '" + r.reference
                        + "', '" + r.po_so + "', '" + r.qty_in + "', '" + r.qty_out + "', '" + r.fcp + "', '" + r.price
                        + "', '" + r.whs_code + "', '" + r.supl_code + "', '" + r.supl_name + "', '" + r.trn_type + "', '" + r.cht_code + "', '"
                        + r.cnt_code + "', '" + r.proj_code + "'" + "', '" + r.branch + "', '" + r.transferred + "'";

                        if (InsertOnTable("stkcrd", col2, val))
                            r.transferred = "T";

                    }
                }

                all.SaveChanges();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.InnerException);
            }       
            */
        }

        protected NpgsqlConnection conn = null;

        public Boolean InsertOnTable(String table, String column, String value)
        {
            Boolean flag = false;

            try
            {
                this.OpenConn();

                string SQL = "INSERT INTO rssys." + table + "(" + column + ") VALUES (" + value + ")";
                // MessageBox.Show(SQL);
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);

                Int32 rowsaffected = command.ExecuteNonQuery();

                this.CloseConn();

                flag = true;
            }
            catch (Exception er)
            {
                flag = false;
                MessageBox.Show(er.Message);
            }

            return flag;
        }

        public void OpenConn()
        {

            CloseConn();

            try
            {
                conn.Open();

                //MessageBox.Show("Connection State " + conn.State.ToString());
            }
            catch (Exception er)
            {
                MessageBox.Show("Connection Exception : " + er.Message);
            }
        }

        public void CloseConn()
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(er.Message);
            }
        }

        private void btn2_searchfile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Rar Files |*.rar";

            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                String file = openFileDialog1.FileName;

                try
                {
                    txt2_fileupload.Text = openFileDialog1.FileName;

                    if (txt2_fileupload.Text != null)
                    {
                        frm_enable(true);
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
        }

        private void btn2_upload_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);

            bgworker.RunWorkerAsync();
        }

        private void btn2_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn3_download_Click(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            db.QueryBySQLCode("pg_dump inv_headway > C:\\inv_headway.backup").ToString();

        }

        private void btn3_openfolder_Click(object sender, EventArgs e)
        {

        }

        private void btn3_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void frm_enable(Boolean flag)
        {
            btn_ping.Enabled = flag;
            btn_testconect.Enabled = flag;
            btn2_upload.Enabled = flag;
            btn_send.Enabled = flag;
            btn_close1.Enabled = flag;
            btn2_close.Enabled = flag;
        }

        private void OpenConn(NpgsqlConnection conn)
        {
            try
            {
                conn.Open();

                MessageBox.Show("Connection State " + conn.State.ToString());
            }
            catch (Exception er)
            {
                MessageBox.Show("Connection Exception : " + er.Message);
            }
        }

        private void CloseConn(NpgsqlConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(er.Message);
            }
        }
    }
}
