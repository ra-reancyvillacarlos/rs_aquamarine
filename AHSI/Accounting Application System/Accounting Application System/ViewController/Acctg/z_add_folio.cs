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
    public partial class z_add_folio : Form
    {
        a_statementofaccount _frm_soa = null;
        String gfolio = "";
        GlobalClass gc;
        GlobalMethod gm;
        String gtype = "";
        Boolean newitem = false;
        String custid = "";
        thisDatabase db = new thisDatabase();
        Boolean isCustomer = true;

        public z_add_folio(a_statementofaccount frm,Boolean isnew, Boolean _isCustomer)
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            _frm_soa = frm;
            this.newitem = isnew;

            //txt_gfolio.Text = frm.cbo_customer.Text;
            isCustomer = _isCustomer;
            disp_dgv_list_folio();
        }

        private void z_add_folio_Load(object sender, EventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (newitem == false)
            {
                _frm_soa.dgv_itemlist.Rows.Clear();
            }
            Boolean status = false;
            DataTable dt = new DataTable();
            
            dt.Columns.Add("dgvl1_lnnum");
            dt.Columns.Add("dgvl1_invoice");
            dt.Columns.Add("dgvl1_date");
            dt.Columns.Add("dgvl1_invoice_balance");
            dt.Columns.Add("dgvl1_paid");
            dt.Columns.Add("dgvl1_customer");
            dt.Columns.Add("dgvl1_description");
            dt.Columns.Add("dgvl1_chg_desc");
            dt.Columns.Add("dgvl1_chg_code");
            dt.Columns.Add("dgvl1_chg_num");
            dt.Columns.Add("dgvl1_chg_date");
            dt.Columns.Add("dgvl1_chg_time");
            dt.Columns.Add("dgvl1_gfolio");
            
            String rw = "";
            Boolean hasPaid = false, hasInvoice = false;
            for (int r = 0; r < dgv_list_folio.Rows.Count -1 ; r++)
            {
                try { status = Convert.ToBoolean(dgv_list_folio["dgvl1_chk", r].Value); }
                catch 
                {
                    status = false;
                }
                if (status)
                {
                    if (dgv_list_folio["dgvl1_invoice_balance", r].Value.ToString() == dgv_list_folio["dgvl1_paid", r].Value.ToString())
                    {
                        status = false;
                        if (!hasPaid)
                        {
                            hasPaid = true;
                            MessageBox.Show("Paid invoice cannot add to list.");
                        }
                    }
                }
                if (status)
                {
                    if (!String.IsNullOrEmpty(dgv_list_folio["dgvl1_invoice", r].Value.ToString()))
                    {
                        status = false;
                        if (!hasInvoice)
                        {
                            hasInvoice = true;
                            MessageBox.Show("Has invoice line cannot add to list.");
                        }
                    }
                }


                try
                {
                    if (status)
                    {
                        dt.AcceptChanges();

                        DataRow newRow = dt.NewRow();
                        newRow["dgvl1_lnnum"] = (r + 1).ToString();
                        newRow["dgvl1_invoice"] = dgv_list_folio["dgvl1_invoice", r].Value.ToString();
                        newRow["dgvl1_invoice_balance"] = dgv_list_folio["dgvl1_invoice_balance", r].Value.ToString();
                        newRow["dgvl1_customer"] = dgv_list_folio["dgvl1_customer", r].Value.ToString();
                        newRow["dgvl1_description"] = dgv_list_folio["dgvl1_description", r].Value.ToString();


                        newRow["dgvl1_chg_desc"] = dgv_list_folio["dgvl1_chg_desc", r].Value.ToString();
                        newRow["dgvl1_chg_code"] = dgv_list_folio["dgvl1_chg_code", r].Value.ToString();
                        newRow["dgvl1_chg_num"] = dgv_list_folio["dgvl1_chg_num", r].Value.ToString();
                        newRow["dgvl1_chg_date"] = dgv_list_folio["dgvl1_date", r].Value.ToString();
                        newRow["dgvl1_chg_time"] = dgv_list_folio["dgvl1_time", r].Value.ToString();
                        newRow["dgvl1_gfolio"] = dgv_list_folio["dgvl1_gfolio", r].Value.ToString();

                        dt.Rows.Add(newRow);
                    }
                }
                catch{}

            }

            if (dt.Rows.Count != 0)
            {
                if (_frm_soa != null)
                {
                    int index = 1;
                    if (_frm_soa.dgv_itemlist.Rows.Count != 0)
                    {
                        try
                        {
                            int i = _frm_soa.dgv_itemlist.Rows.Count;
                            index = Convert.ToInt32(_frm_soa.dgv_itemlist["dgvl2_lnno", i - 1].Value.ToString()) + 1;
                        }
                        catch { }
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        int r = _frm_soa.dgv_itemlist.Rows.Add();

                        _frm_soa.dgv_itemlist["dgvl2_lnno", r].Value = (index++).ToString();
                        _frm_soa.dgv_itemlist["dgvl2_invoice", r].Value = dt.Rows[i]["dgvl1_invoice"].ToString();
                        _frm_soa.dgv_itemlist["dgvl2_desc", r].Value = dt.Rows[i]["dgvl1_description"].ToString();
                        _frm_soa.dgv_itemlist["dgvl2_amount", r].Value = gm.toAccountingFormat(dt.Rows[i]["dgvl1_invoice_balance"].ToString());
                        _frm_soa.dgv_itemlist["dgvl2_charge_desc", r].Value =  dt.Rows[i]["dgvl1_chg_desc"].ToString();
                        _frm_soa.dgv_itemlist["dgvl2_chg_code", r].Value = dt.Rows[i]["dgvl1_chg_code"].ToString();
                        _frm_soa.dgv_itemlist["dgvl2_chg_num", r].Value = dt.Rows[i]["dgvl1_chg_num"].ToString();
                        _frm_soa.dgv_itemlist["dgvl2_chg_date", r].Value = dt.Rows[i]["dgvl1_chg_date"].ToString();
                        _frm_soa.dgv_itemlist["dgvl2_chg_time", r].Value = dt.Rows[i]["dgvl1_chg_time"].ToString();
                        _frm_soa.dgv_itemlist["dgvl2_gfolio", r].Value = dt.Rows[i]["dgvl1_gfolio"].ToString();


                        if (_frm_soa.dgv_itemlist.Rows.Count == 1)
                        {
                            String rmrttyp = db.get_colval("gfolio", "rmrttyp", "reg_num='" + dt.Rows[i]["dgvl1_gfolio"].ToString() + "'");
                            if (String.IsNullOrEmpty(rmrttyp))
                            {
                                rmrttyp = db.get_colval("gfhist", "rmrttyp", "reg_num='" + dt.Rows[i]["dgvl1_gfolio"].ToString() + "'");
                            }

                            if (!String.IsNullOrEmpty(rmrttyp))
                            {
                                try { _frm_soa.change_rmrttyp(rmrttyp, DateTime.Parse(db.get_colval("gfolio", "arr_date", "reg_num='" + dt.Rows[i]["dgvl1_gfolio"].ToString() + "'")).ToString("yyyy-MM-dd")); }
                                catch { }
                            }
                        }
                    }

                    _frm_soa.total_amountdue();


                }
            }
            else
            {
                MessageBox.Show("Nothing to add list.");
            }

            this.Close();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            disp_dgv_list_folio();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void disp_dgv_list_folio()
        {
            GlobalMethod gm = new GlobalMethod();
            String custcode = _frm_soa.get_custcode_frm();
            DataTable dt = null;
            
            String searchtxt = txt_gfolio.Text;
            Double debit = 0, credit = 0;
            Boolean isExistItem = false;

            //dt = db.get_hotelguestfolioWithBalances(gfolio, gtype, includeWarehouse);
            //dt = db.QueryBySQLCode("SELECT o.ord_code, o.customer, o.ord_amnt, o.user_id, o.reference, ol.out_desc, ol.out_code FROM rssys.orhdr o LEFT JOIN rssys.outlet ol ON ol.out_code=o.out_code  WHERE (o.customer LIKE '%" + gfolio + "%' OR o.ord_code  LIKE '%" + gfolio + "%' OR o.reference LIKE '%" + gfolio + "%') AND o.debt_code LIKE '%" + custid + "%'");
            //dt = db.QueryBySQLCode("SELECT DISTINCT tr.*,o.trnx_date AS date, o.total_amnt ,o.out_code,ot.out_desc FROM rssys.tr02 tr LEFT JOIN rssys.orhdr o ON tr.invoice=o.ord_code LEFT JOIN rssys.outlet ot ON o.out_code=ot.out_code WHERE tr.sl_code ='" + _frm_soa.cbo_customer.SelectedValue.ToString() + "'");
            //t2.invoice=cf.soa_code
            if (isCustomer == false)
            {
                dt = db.QueryBySQLCode("SELECT cf.*, c.chg_desc, t2.*, gf.acct_no, gf.full_name FROM rssys.chgfil cf JOIN rssys.gfolio gf ON gf.reg_num=cf.reg_num LEFT JOIN rssys.tr02 t2 ON (cf.chg_code=t2.chg_code AND cf.chg_num=t2.chg_num) LEFT JOIN rssys.charge c ON c.chg_code=cf.chg_code WHERE c.isdeposit=FALSE AND c.chg_type='C' AND ((gf.acct_no LIKE '%" + searchtxt + "%' OR gf.full_name LIKE '%" + searchtxt + "%' OR cf.soa_code LIKE '%" + searchtxt + "%' OR gf.reg_num LIKE '%" + searchtxt + "%' OR c.chg_desc LIKE '%" + searchtxt + "%') AND gf.acct_no='" + (_frm_soa.cbo_customer.SelectedValue ?? "").ToString() + "') ORDER BY chg_date desc,t_time  desc");

                dgv_list_folio.Columns["dgvl1_gfolio"].Visible = true; dgv_list_folio.Columns["acct_no"].Visible = true; dgv_list_folio.Columns["full_name"].Visible = true; dgv_list_folio.Columns["name"].Visible = true; dgv_list_folio.Columns["chg_code"].Visible = true; dgv_list_folio.Columns["chg_desc"].Visible = true; dgv_list_folio.Columns["ttlpax"].Visible = true;
                dgv_list_folio.Columns["dgvl1_chg_desc"].Visible = false; dgv_list_folio.Columns["dgvl1_invoice"].Visible = false; dgv_list_folio.Columns["dgvl1_customer"].Visible = false; dgv_list_folio.Columns["dgvl1_chg_code"].Visible = false; dgv_list_folio.Columns["dgvl1_chg_num"].Visible = false; dgv_list_folio.Columns["dgvl1_out_code1"].Visible = false;
            }
            else
            {
                dt = db.QueryBySQLCode("SELECT cf.*, c.chg_desc, t2.*, gf.acct_no, gf.full_name FROM rssys.chgfil cf JOIN rssys.gfolio gf ON gf.reg_num=cf.reg_num LEFT JOIN rssys.tr02 t2 ON (cf.chg_code=t2.chg_code AND cf.chg_num=t2.chg_num) LEFT JOIN rssys.charge c ON c.chg_code=cf.chg_code WHERE c.isdeposit=FALSE AND c.chg_type='C' AND ((gf.acct_no LIKE '%" + searchtxt + "%' OR gf.full_name LIKE '%" + searchtxt + "%' OR cf.soa_code LIKE '%" + searchtxt + "%' OR gf.reg_num LIKE '%" + searchtxt + "%' OR c.chg_desc LIKE '%" + searchtxt + "%') AND gf.acct_no='" + (_frm_soa.cbo_customer.SelectedValue ?? "").ToString() + "') ORDER BY chg_date desc,t_time  desc");

                dgv_list_folio.Columns["dgvl1_gfolio"].Visible = false; dgv_list_folio.Columns["acct_no"].Visible = false; dgv_list_folio.Columns["full_name"].Visible = false; dgv_list_folio.Columns["name"].Visible = false; dgv_list_folio.Columns["chg_code"].Visible = false; dgv_list_folio.Columns["chg_desc"].Visible = false; dgv_list_folio.Columns["ttlpax"].Visible = false;
                dgv_list_folio.Columns["dgvl1_chg_desc"].Visible = true; dgv_list_folio.Columns["dgvl1_invoice"].Visible = true; dgv_list_folio.Columns["dgvl1_customer"].Visible = true; dgv_list_folio.Columns["dgvl1_chg_code"].Visible = true; dgv_list_folio.Columns["dgvl1_chg_num"].Visible = true; dgv_list_folio.Columns["dgvl1_out_code1"].Visible = true;
            }

            try
            {
                try{ dgv_list_folio.Rows.Clear(); }
                catch { }

                if (dt != null)
                {

                    for (int r = 0; dt.Rows.Count > r; r++)
                    {
                        isExistItem = false;
                        for (int j = 0; j < _frm_soa.dgv_itemlist.Rows.Count; j++)
                        {
                            if (isCustomer == true)
                            {
                                if (dt.Rows[r]["reg_num"].ToString() == _frm_soa.dgv_itemlist["dgvl2_gfolio", j].Value.ToString() && dt.Rows[r]["chg_num"].ToString() == _frm_soa.dgv_itemlist["dgvl2_chg_num", j].Value.ToString() && dt.Rows[r]["chg_code"].ToString() == _frm_soa.dgv_itemlist["dgvl2_chg_code", j].Value.ToString())
                                {
                                    isExistItem = true;
                                }
                            }
                            else
                            {
                                if (dt.Rows[r]["reg_num"].ToString() == _frm_soa.dgv_itemlist["dgvl2_gfolio", j].Value.ToString())
                                {
                                    isExistItem = true;
                                }
                            }
                        }

                        if (!isExistItem)
                        {
                            int i = dgv_list_folio.Rows.Add();
                            DataGridViewRow row = dgv_list_folio.Rows[i];

                            row.Cells["dgvl1_lnnum"].Value = (r + 1).ToString();
                            row.Cells["dgvl1_invoice"].Value = dt.Rows[r]["soa_code"].ToString();
                            row.Cells["dgvl1_date"].Value = gm.toDateString(dt.Rows[r]["chg_date"].ToString(), "");
                            row.Cells["dgvl1_time"].Value = dt.Rows[r]["t_time"].ToString();
                            row.Cells["dgvl1_invoice_balance"].Value = dt.Rows[r]["amount"].ToString();

                            row.Cells["dgvl1_gfolio"].Value = dt.Rows[r]["reg_num"].ToString();
                            row.Cells["dgvl1_chg_desc"].Value = dt.Rows[r]["chg_desc"].ToString();
                            row.Cells["dgvl1_chg_code"].Value = dt.Rows[r]["chg_code"].ToString();
                            row.Cells["dgvl1_chg_num"].Value = dt.Rows[r]["chg_num"].ToString();

                            try { debit = gm.toNormalDoubleFormat(dt.Rows[r]["debit"].ToString()); }
                            catch { }
                            try { credit = gm.toNormalDoubleFormat(dt.Rows[r]["credit"].ToString()); }
                            catch { }

                            row.Cells["dgvl1_paid"].Value = "0.00";
                            if (String.IsNullOrEmpty(dt.Rows[r]["invoice"].ToString()))
                            {
                                if (debit == 0 && credit != 0)
                                {
                                    row.Cells["dgvl1_paid"].Value = credit.ToString("0.00");
                                }
                                else if (debit != 0 && credit == 0)
                                {
                                    row.Cells["dgvl1_paid"].Value = debit.ToString("0.00");
                                }
                                else
                                {
                                    row.Cells["dgvl1_paid"].Value = credit.ToString("0.00");
                                }
                            }

                            row.Cells["dgvl1_customer"].Value = dt.Rows[r]["full_name"].ToString();
                            row.Cells["dgvl1_description"].Value = dt.Rows[r]["reference"].ToString();

                            row.Cells["dgvl1_description"].Value = dt.Rows[r]["reference"].ToString();
                            row.Cells["dgvl1_description"].Value = dt.Rows[r]["reference"].ToString();
                            row.Cells["dgvl1_description"].Value = dt.Rows[r]["reference"].ToString();
                            row.Cells["dgvl1_description"].Value = dt.Rows[r]["reference"].ToString();
                            row.Cells["dgvl1_description"].Value = dt.Rows[r]["reference"].ToString();
                            row.Cells["dgvl1_description"].Value = dt.Rows[r]["reference"].ToString();
                        }
                    }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }


        private void dgv_list_folio_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv_list_folio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_list_folio_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void txt_gfolio_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            Invoke(new Action(() => {
                disp_dgv_list_folio();
            }));
        }
    }
}
