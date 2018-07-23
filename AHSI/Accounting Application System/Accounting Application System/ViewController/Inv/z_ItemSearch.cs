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
    public partial class z_ItemSearch : Form
    {
        z_enter_sales_item frm_esitem = null;
        z_enter_item frm_eitem = null;
        z_enter_item_simple frm_eisimple = null;
        z_enter_ret_pur_item frm_retpi = null;
        z_enter_ret_sale_item frm_retsi = null;
        m_assembleditem _frm_ai = null;
        i_stkcrd_frm _frm_stkcrd = null;
        auto_loanapplication _frm_autoloan = null;
        s_Sales_Auto _frm_autosales = null;
        
        m_item frm_m_item = null;
        String _tsrch;
        String _ttype; //D = description; C = code, Q = quantity
        Boolean itemsel_enable = false;
        Boolean issearch_ready = true;


        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        int limit_val = 30;
        int offset = 0;
        String _typeOfCategory = "";

        Boolean iscopyAssembledItem = false;

        public z_ItemSearch()
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            cbo_searchby.SelectedIndex = 0;
            cbo_sort1.SelectedIndex = 0;
            
            disp_list(txt_search.Text);
        }

        public z_ItemSearch(i_stkcrd_frm frm, String tsrch, String ttype)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            _frm_stkcrd = frm;
            _tsrch = tsrch;
            _ttype = ttype;
            txt_search.Text = _tsrch;
            disp_list(txt_search.Text);
            disp_dgv_search(_tsrch);
        }

        //typeOfSearch = 0 = Menu
        //1 = Cars
        //2 = Items
        public z_ItemSearch(int typeOfSearch) 
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);

            issearch_ready = false;
            //_typeOfGroupCat = typeOfSearch;

            cbo_itmgrp.SelectedIndex = -1;
            cbo_searchby.SelectedIndex = 0;
            cbo_sort1.SelectedIndex = 0;

            //disp_list(txt_search.Text);

            issearch_ready = true;
        }

        public z_ItemSearch(m_assembleditem frm_ai, String tsrch, String ttype, int typeOfSearch, Boolean iscopy)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            //_typeOfGroupCat = typeOfSearch;

            _frm_ai = frm_ai;
            iscopyAssembledItem = iscopy;
            _tsrch = tsrch;
            _ttype = ttype;
            txt_search.Text = _tsrch;
        }

        public z_ItemSearch(z_enter_item frm_ei, String tsrch, String ttype)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            frm_eitem = frm_ei;
            _tsrch = tsrch;
            _ttype = ttype;
            txt_search.Text = _tsrch;
            disp_list(txt_search.Text);
            disp_dgv_search(_tsrch);
        }

        public z_ItemSearch(auto_loanapplication frm, int typeOfSearch)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            _frm_autoloan = frm;
            //_typeOfGroupCat = typeOfSearch;

            cbo_searchby.SelectedIndex = 0;
            cbo_sort1.SelectedIndex = 0;

           // cbo_itmgrp.Enabled = false;
            //disp_list(txt_search.Text);
        }

        public z_ItemSearch(s_Sales_Auto frm, int typeOfSearch)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            _frm_autosales = frm;
            //_typeOfGroupCat = typeOfSearch;

            cbo_searchby.SelectedIndex = 0;
            cbo_sort1.SelectedIndex = 0;

            //cbo_itmgrp.Enabled = false;
            //disp_list(txt_search.Text);
        }
        
        public z_ItemSearch(z_enter_sales_item frm_esi, String tsrch, String ttype)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            frm_esitem = frm_esi;
            _tsrch = tsrch;
            _ttype = ttype;
            txt_search.Text = _tsrch;
            disp_list(txt_search.Text);
            disp_dgv_search(_tsrch);
        }

        public z_ItemSearch(z_enter_item_simple frm_eis, String tsrch, String ttype)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            frm_eisimple = frm_eis;
            _tsrch = tsrch;
            _ttype = ttype;
            txt_search.Text = _tsrch;
            disp_list(txt_search.Text);
            disp_dgv_search(_tsrch);
        }

        public z_ItemSearch(z_enter_ret_pur_item frm_rpi, String tsrch, String ttype)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            this.frm_retpi = frm_rpi;
            _tsrch = tsrch;
            _ttype = ttype;
            txt_search.Text = _tsrch;
            disp_list(txt_search.Text);
            disp_dgv_search(_tsrch);
        }

        public z_ItemSearch(z_enter_ret_sale_item frm_rsi, String tsrch, String ttype)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            frm_retsi = frm_rsi;
            _tsrch = tsrch;
            _ttype = ttype;
            txt_search.Text = _tsrch;
            disp_list(txt_search.Text);
            disp_dgv_search(_tsrch); 
        }
              
        public z_ItemSearch(m_assembleditem frm_ai, String tsrch, String ttype)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            _frm_ai = frm_ai;
            _tsrch = tsrch;
            _ttype = ttype;
            txt_search.Text = _tsrch;
            disp_list(txt_search.Text);
            disp_dgv_search(_tsrch); 
        }

        public z_ItemSearch(m_assembleditem frm_ai, String tsrch, String ttype, int typeOfSearch)
        {
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_branch(cbo_branch);
            cbo_branch.SelectedValue = GlobalClass.branch;
            gc.load_category(cbo_itmgrp);
            itemsel_enable = true;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            _frm_ai = frm_ai;
            _tsrch = tsrch;
            _ttype = ttype;
            txt_search.Text = _tsrch;
            //_typeOfGroupCat = typeOfSearch;
            disp_list(txt_search.Text);
            disp_dgv_search(_tsrch);
        }

        private void z_ItemSearch_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txt_search;
            dgv_list.CellMouseDoubleClick += dgv_list_CellMouseDoubleClick;

            dgv_category.DataSource = db.QueryOnTableWithParams("itmgrp", "item_grp, grp_desc", "", " ORDER BY item_grp ASC");
        }

        private void z_ItemSearch_Shown(object sender, EventArgs e)
        {

        }

        public void set_frm_m_item(m_item i)
        {
            frm_m_item = i;
        }

        private void disp_catlist()
        {
            try
            {
                dgv_category.DataSource = db.QueryAllOnTable("itmgrp");
            }
            catch { }
        }

        private String get_TypeOfCategory()
        {
            return "";
        }


        private void disp_list(String srch)
        {
            Double disc_pct = 0;
            String _typeOfGroupCat = "";
            String category = "";
            String typ = get_cbo_srch_typ();
            String sortby1 = get_cbo_sort1();
            String branch = "";

            try
            {
                if (cbo_branch.SelectedIndex > -1)
                {
                    branch = cbo_branch.SelectedValue.ToString();
                }
            }
            catch { }
            

            try { if(cbo_itmgrp.SelectedIndex > -1) category = cbo_itmgrp.SelectedValue.ToString(); }
            catch { }

            try
            {
                DataTable dt = new DataTable();
                //dt = db.QueryBySQLCode("SELECT i.item_code, COALESCE(SUM(st.qty_in),0.00) - COALESCE(SUM(st.qty_out),0.00) AS qty_onhand_su, i.part_no, i.cartype, i.item_desc, iu.unit_shortcode AS sale_unit, i.sales_unit_id, b.brd_name, i.brd_code, i.sell_pric AS regular, i.sc_price AS senior, i.bin_loc, i.unit_cost, ig.grp_desc, i.item_grp, st.whs_code, w.whs_desc, st.branch AS branchcode, branch.name AS branchname FROM rssys.items  i LEFT JOIN rssys.itmunit iu ON i.sales_unit_id=iu.unit_id LEFT JOIN rssys.brand b ON i.brd_code=b.brd_code LEFT JOIN rssys.itmgrp ig ON ig.item_grp=i.item_grp LEFT JOIN rssys.stkcrd st ON st.item_code=i.item_code LEFT JOIN rssys.whouse w ON w.whs_code=st.whs_code LEFT JOIN rssys.branch ON w.branch=branch.code GROUP BY  i.item_code, i.part_no, i.cartype, i.item_desc,iu.unit_shortcode, i.sales_unit_id, b.brd_name, i.brd_code, i.sell_pric, i.sc_price, i.bin_loc, i.unit_cost, ig.grp_desc, i.item_grp, st.whs_code, w.whs_desc, branchcode, branchname    ORDER BY i.item_code ASC limit 70 offset "+offset+""); //

                try
                {
                    //_typeOfCategory = cbo_itmgrp.SelectedValue.ToString();
                }
                catch { _typeOfCategory = ""; }

                //Menu
                if (_typeOfGroupCat == "001")
                {
                    dgv_list.Columns["dgvi_qty"].Visible = true;

                    dgv_list.Columns["dgvi_downpayment"].Visible = false;
                    dgv_list.Columns["dgvi_1years"].Visible = false;
                    dgv_list.Columns["dgvi_2years"].Visible = false;
                    dgv_list.Columns["dgvi_3years"].Visible = false;
                    dgv_list.Columns["dgvi_4yrs"].Visible = false;
                    dgv_list.Columns["dgvi_5yrs"].Visible = false;

                    dgv_list.Columns["dgvi_part_no"].Visible = false;
                    dgv_list.Columns["dgvi_rack"].Visible = false;
                }

                //Cars
                else if (_typeOfGroupCat == "002")
                {
                    dgv_list.Columns["dgvi_qty"].Visible = true;

                    dgv_list.Columns["dgvi_downpayment"].Visible = true;
                    dgv_list.Columns["dgvi_1years"].Visible = true;
                    dgv_list.Columns["dgvi_2years"].Visible = true;
                    dgv_list.Columns["dgvi_3years"].Visible = true;
                    dgv_list.Columns["dgvi_4yrs"].Visible = true;
                    dgv_list.Columns["dgvi_5yrs"].Visible = true;

                    dgv_list.Columns["dgvi_part_no"].Visible = false;
                    dgv_list.Columns["dgvi_rack"].Visible = false;
                }
                //Parts and Service
                else if (_typeOfGroupCat == "003")
                {
                    dgv_list.Columns["dgvi_qty"].Visible = true;

                    dgv_list.Columns["dgvi_downpayment"].Visible = false;
                    dgv_list.Columns["dgvi_1years"].Visible = false;
                    dgv_list.Columns["dgvi_2years"].Visible = false;
                    dgv_list.Columns["dgvi_3years"].Visible = false;
                    dgv_list.Columns["dgvi_4yrs"].Visible = false;
                    dgv_list.Columns["dgvi_5yrs"].Visible = false;

                    dgv_list.Columns["dgvi_part_no"].Visible = true;
                    dgv_list.Columns["dgvi_rack"].Visible = true;
                }

                dt = db.get_itemlist(srch, _typeOfGroupCat, branch, category, typ, sortby1, limit_val.ToString(), offset.ToString());

                lbl_rows.Text = "No.Of Rows: " + dt.Rows.Count.ToString();

                dgv_list.Rows.Clear();

                if (dt != null)
                {
                    for (int r = 0; dt.Rows.Count > r; r++)
                    {
                        dgv_list.Rows.Add();

                        dgv_list["dgvi_qty", r].Value = gm.toAccountingFormat(dt.Rows[r]["qty_onhand_su"].ToString());
                        dgv_list["dgvi_part_no", r].Value = dt.Rows[r]["part_no"].ToString();
                        dgv_list["dgvi_tdesc", r].Value = dt.Rows[r]["item_desc"].ToString();
                        dgv_list["dgvi_unitdesc", r].Value = dt.Rows[r]["sale_unit"].ToString();
                        dgv_list["dgvi_cost", r].Value = gm.toAccountingFormat(dt.Rows[r]["unit_cost"].ToString());
                        dgv_list["dgvi_itemgrp", r].Value = dt.Rows[r]["grp_desc"].ToString();
                        dgv_list["dgvi_reg_price", r].Value = gm.toAccountingFormat(dt.Rows[r]["regular"].ToString());
                        dgv_list["dgvi_senior", r].Value = dt.Rows[r]["senior"].ToString();
                        dgv_list["dgvi_rack", r].Value = dt.Rows[r]["bin_loc"].ToString();
                        dgv_list["dgvi_unit", r].Value = dt.Rows[r]["sales_unit_id"].ToString();
                        dgv_list["dgvi_itemcode", r].Value = dt.Rows[r]["item_code"].ToString();
                        dgv_list["dgvi_brd_code", r].Value = dt.Rows[r]["brd_code"].ToString();
                        dgv_list["dgvi_brd_desc", r].Value = dt.Rows[r]["brd_name"].ToString();
                        dgv_list["dgvi_whs_desc", r].Value = dt.Rows[r]["whs_desc"].ToString();
                        dgv_list["dgvi_whs_code", r].Value = dt.Rows[r]["whs_code"].ToString();
                        dgv_list["dgvi_branch", r].Value = dt.Rows[r]["branchcode"].ToString();
                        dgv_list["dgvi_branchname", r].Value = dt.Rows[r]["branchname"].ToString();
                        dgv_list["dgvi_itemgrpid", r].Value = dt.Rows[r]["item_grp"].ToString();

                        if (_typeOfGroupCat == "002")
                        {
                            double interest = double.Parse(db.get_colval("items", "interestpct", "item_code='" + dgv_list["dgvi_itemcode", r].Value.ToString() + "'"));
                            double srp = gm.toNormalDoubleFormat(dt.Rows[r]["regular"].ToString());

                            Double dpamt = (disc_pct * srp);

                            //dgv_amortlist["dgvdp_dpdesc", r].Value = dt.Rows[i]["dpdesc"].ToString();
                            //dgv_amortlist["dgvdp_dpamt", r].Value = gm.toAccountingFormat(dpamt);

                            Double rdpamt = (srp - dpamt);

                            Double _interest = 0;

                            dgv_list["dgvi_downpayment", r].Value = "0.00";

                            _interest = ((interest / 100.0) * 1) + 1;
                            dgv_list["dgvi_1years", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 12));
                            _interest = ((interest / 100.0) * 2) + 1;
                            dgv_list["dgvi_2years", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 24));
                            _interest = ((interest / 100.0) * 3) + 1;
                            dgv_list["dgvi_3years", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 36));
                            _interest = ((interest / 100.0) * 4) + 1;
                            dgv_list["dgvi_4yrs", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 48));
                            _interest = ((interest / 100.0) * 5) + 1;
                            dgv_list["dgvi_5yrs", r].Value = gm.toAccountingFormat(((rdpamt * _interest) / 60));
                        }
                    }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); lbl_rows.Text = "No.Of Rows: 0"; }
        }

        private void disp_dgv_search(String searchValue)
        {
            int rowIndex = -1;
            String typname = "dgvi_tdesc";

            _ttype = get_cbo_srch_typ();

            try
            {
                if (_ttype == "C")
                {
                    typname = "dgvi_itemcode";
                }
                else if (_ttype == "Q")
                {
                    typname = "dgvi_qty";
                }
                else if (_ttype == "B")
                {
                    typname = "dgvi_brd_code";
                }
                else if (_ttype == "C")
                {
                    typname = "dgvi_itemgrp";
                }
                else
                {
                    searchValue = searchValue.ToUpper();
                }

                DataGridViewRow row = dgv_list.Rows.Cast<DataGridViewRow>()
                                        .Where(r => r.Cells[typname].Value.ToString().ToUpper().StartsWith(searchValue))
                                        .FirstOrDefault();

                rowIndex = row.Index;

                dgv_list.Rows[rowIndex].Selected = true;
                /*
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[1];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[2];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[3];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[4];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[5];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[6];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[7];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[8];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[9];
                dgv_list.CurrentCell = dgv_list.Rows[rowIndex].Cells[0];
                */
                //dgv_list.FirstDisplayedScrollingRowIndex = rowIndex;

                //dgv_list.Rows[rowIndex].Selected = true;
            }
            catch (Exception) { }
        }

        private String get_cbo_srch_typ()
        {
            try
            {
                if (cbo_searchby.SelectedIndex == 0) //"QTY"
                {
                    return "D";
                }
                else if (cbo_searchby.SelectedIndex == 1) //"PART NUMBER"
                {
                    return "P";
                }
                else if (cbo_searchby.SelectedIndex == 2) //"ITEM CODE"
                {
                    return "C";
                }
                else if (cbo_searchby.SelectedIndex == 3) //"BRAND NAME"
                {
                    return "B";
                }
                else if (cbo_searchby.SelectedIndex == 4) // CATEGORY or ITEM GROUP
                {
                    return "G";
                }
                else if (cbo_searchby.SelectedIndex == 5) // CATEGORY or ITEM GROUP
                {
                    return "Q";
                }
            }
            catch (Exception) { }

            return "D"; //DESCRIPTION
        }

        private String get_cbo_sort1()
        {
            try
            {
                if (cbo_sort1.SelectedIndex == 0) //Category, Item Description
                {
                    return "GD";
                }
                else if (cbo_sort1.SelectedIndex == 1) //Item Code, Item Description
                {
                    return "CD";
                }
                else if (cbo_sort1.SelectedIndex == 2) //Brand Name, Item Description
                {
                    return "BD";
                }
                else if (cbo_sort1.SelectedIndex == 3) // Qty, Category, Item Desc
                {
                    return "QGD";
                }
            }
            catch (Exception) { }

            return "CD"; //DESCRIPTION
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            limit_val = 30;
            offset = 0;
            cbo_itmgrp.SelectedIndex = -1;
            disp_list(txt_search.Text);
        }
        
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                this.ActiveControl = dgv_list;
            }

            if (e.KeyCode == Keys.Enter)
            {
                limit_val = 30;
                offset = 0;
                disp_list(txt_search.Text);
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            disp_dgv_search(txt_search.Text);
            //disp_list(txt_search.Text, get_cbo_srch_typ(), get_cbo_sort1(), get_cbo_sort2());

        }

        private void itemselected()
        {
            Boolean proceed = true;
            Double lqty = 0.00;
            String code = "", partno = "", tdesc = "", unitid = "", reg_price = "0.00", ave_cost = "0.00", itm_grpid = "", brd_id="",condno="";

            //try
            //{
                int r = dgv_list.CurrentRow.Index;

                lqty = gm.toNormalDoubleFormat(dgv_list["dgvi_qty", r].Value.ToString());

                if ((frm_esitem != null || frm_retpi != null) && lqty <= 0.00)
                {
                    proceed = false;

                    DialogResult result = MessageBox.Show("Insufficient item quantity. Are you sure you want to continue?", "Confirmation", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Yes)
                    {
                        proceed = true;
                    }
                }

                if(proceed)
                {
                    code = dgv_list["dgvi_itemcode", r].Value.ToString();
                    
                    tdesc = dgv_list["dgvi_tdesc", r].Value.ToString();
                    unitid = dgv_list["dgvi_unit", r].Value.ToString();
                    reg_price = dgv_list["dgvi_reg_price", r].Value.ToString();
                    itm_grpid = dgv_list["dgvi_itemgrpid", r].Value.ToString();
                    partno = dgv_list["dgvi_part_no", r].Value.ToString();
                    brd_id = dgv_list["dgvi_brd_code", r].Value.ToString();
                    brd_id = dgv_list["dgvi_brd_code", r].Value.ToString();

                    if (_frm_autoloan != null)
                    {
                        _frm_autoloan.enter_item(code);
                    }
                    if (_frm_autosales != null)
                    {
                        _frm_autosales.enter_item(code);
                    }
                    if (frm_esitem != null)
                    {
                        frm_esitem.enter_salesitem(code, tdesc, unitid, reg_price, partno, itm_grpid, brd_id);
                    }
                    if (frm_m_item != null)
                    {
                        frm_m_item.enter_item(code);
                    }
                    else if (frm_eitem != null)
                    {
                        frm_eitem.enter_item(code, tdesc);
                    }
                    else if (frm_eisimple != null)
                    {
                        frm_eisimple.enter_item(code, tdesc);
                    }
                    else if (frm_retsi != null)
                    {
                        frm_retsi.enter_item(code, tdesc, partno);
                        frm_retsi.set_unit(unitid);
                        frm_retsi.set_reg_price(reg_price);
                    }
                    else if (frm_retpi != null)
                    {
                        frm_retpi.enter_item(code, tdesc);
                    }
                    else if (_frm_ai != null)
                    {
                        if (iscopyAssembledItem == false)
                            _frm_ai.set_info(code, tdesc);
                        else
                            _frm_ai.set_info(code, tdesc);
                    }

                    else if (_frm_stkcrd != null)
                    {
                        _frm_stkcrd.set_itemselected(code, partno, tdesc);
                    }

                    this.Close();
                }
            //}
            //catch (Exception) { }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

        }

        private void dgv_list_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(itemsel_enable)
            {
                itemselected();
                itemsel_enable = false;
            }
        }

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_sort_Click(object sender, EventArgs e)
        {

        }

        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_prev_Click(object sender, EventArgs e)
        {

            try
            {
               
                if (offset >=0)
                {
                    offset = offset - limit_val;
                    disp_list(txt_search.Text);

                    /*
                    DataTable dt = new DataTable();
                    dt = db.get_itemlist(srch, typ, sortby1, sortby2, limit_val.ToString(), offset.ToString());
                   
                    //dt = db.QueryBySQLCode("SELECT i.item_code, COALESCE(SUM(st.qty_in),0.00) - COALESCE(SUM(st.qty_out),0.00) AS qty_onhand_su, i.part_no, i.cartype, i.item_desc, iu.unit_shortcode AS sale_unit, i.sales_unit_id, b.brd_name, i.brd_code, i.sell_pric AS regular, i.sc_price AS senior, i.bin_loc, i.unit_cost, ig.grp_desc, i.item_grp, st.whs_code, w.whs_desc, st.branch AS branchcode, branch.name AS branchname FROM rssys.items  i LEFT JOIN rssys.itmunit iu ON i.sales_unit_id=iu.unit_id LEFT JOIN rssys.brand b ON i.brd_code=b.brd_code LEFT JOIN rssys.itmgrp ig ON ig.item_grp=i.item_grp LEFT JOIN rssys.stkcrd st ON st.item_code=i.item_code LEFT JOIN rssys.whouse w ON w.whs_code=st.whs_code LEFT JOIN rssys.branch ON w.branch=branch.code GROUP BY  i.item_code, i.part_no, i.cartype, i.item_desc,iu.unit_shortcode, i.sales_unit_id, b.brd_name, i.brd_code, i.sell_pric, i.sc_price, i.bin_loc, i.unit_cost, ig.grp_desc, i.item_grp, st.whs_code, w.whs_desc, branchcode, branchname    ORDER BY i.item_code ASC limit " + limit_val.ToString() + " offset " + offset + ""); //db.get_itemlist(srch, typ, sortby1, sortby2);
                    
                    lbl_rows.Text = "No.Of Rows: " + dt.Rows.Count.ToString();

                    if (dt.Rows.Count > 0)
                    {
                        dgv_list.Rows.Clear();

                        for (int r = 0; dgv_list.Rows.Count > r; r++)
                        {

                            dgv_list.Rows.Add();


                            dgv_list["dgvi_qty", r].Value = gm.toAccountingFormat(dt.Rows[r]["qty_onhand_su"].ToString());
                            dgv_list["dgvi_part_no", r].Value = dt.Rows[r]["part_no"].ToString();
                            dgv_list["dgvi_tdesc", r].Value = dt.Rows[r]["item_desc"].ToString();
                            dgv_list["dgvi_unitdesc", r].Value = dt.Rows[r]["sale_unit"].ToString();
                            dgv_list["dgvi_cost", r].Value = gm.toAccountingFormat(dt.Rows[r]["unit_cost"].ToString());
                            dgv_list["dgvi_itemgrp", r].Value = dt.Rows[r]["grp_desc"].ToString();
                            dgv_list["dgvi_reg_price", r].Value = gm.toAccountingFormat(dt.Rows[r]["regular"].ToString());
                            dgv_list["dgvi_senior", r].Value = dt.Rows[r]["senior"].ToString();
                            dgv_list["dgvi_rack", r].Value = dt.Rows[r]["bin_loc"].ToString();
                            dgv_list["dgvi_unit", r].Value = dt.Rows[r]["sales_unit_id"].ToString();
                            dgv_list["dgvi_itemcode", r].Value = dt.Rows[r]["item_code"].ToString();
                            dgv_list["dgvi_brd_code", r].Value = dt.Rows[r]["brd_code"].ToString();
                            dgv_list["dgvi_brd_desc", r].Value = dt.Rows[r]["brd_name"].ToString();
                            dgv_list["dgvi_whs_desc", r].Value = dt.Rows[r]["whs_desc"].ToString();
                            dgv_list["dgvi_whs_code", r].Value = dt.Rows[r]["whs_code"].ToString();
                            dgv_list["dgvi_branch", r].Value = dt.Rows[r]["branchcode"].ToString();
                            dgv_list["dgvi_branchname", r].Value = dt.Rows[r]["branchname"].ToString();
                            dgv_list["dgvi_itemgrpid", r].Value = dt.Rows[r]["item_grp"].ToString();

                        }
                    }*/

                }
            }
            catch (Exception err) {  }

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
           try
            {
                offset = offset + limit_val;
                disp_list(txt_search.Text);
               /*
                DataTable dt = new DataTable();

                dt = db.get_itemlist(srch, typ, sortby1, sortby2, limit_val.ToString(), offset.ToString());

               // dt = db.QueryBySQLCode("SELECT i.item_code, COALESCE(SUM(st.qty_in),0.00) - COALESCE(SUM(st.qty_out),0.00) AS qty_onhand_su, i.part_no, i.cartype, i.item_desc, iu.unit_shortcode AS sale_unit, i.sales_unit_id, b.brd_name, i.brd_code, i.sell_pric AS regular, i.sc_price AS senior, i.bin_loc, i.unit_cost, ig.grp_desc, i.item_grp, st.whs_code, w.whs_desc, st.branch AS branchcode, branch.name AS branchname FROM rssys.items  i LEFT JOIN rssys.itmunit iu ON i.sales_unit_id=iu.unit_id LEFT JOIN rssys.brand b ON i.brd_code=b.brd_code LEFT JOIN rssys.itmgrp ig ON ig.item_grp=i.item_grp LEFT JOIN rssys.stkcrd st ON st.item_code=i.item_code LEFT JOIN rssys.whouse w ON w.whs_code=st.whs_code LEFT JOIN rssys.branch ON w.branch=branch.code GROUP BY  i.item_code, i.part_no, i.cartype, i.item_desc,iu.unit_shortcode, i.sales_unit_id, b.brd_name, i.brd_code, i.sell_pric, i.sc_price, i.bin_loc, i.unit_cost, ig.grp_desc, i.item_grp, st.whs_code, w.whs_desc, branchcode, branchname    ORDER BY i.item_code ASC limit " + limit_val.ToString() + " offset " + offset + ""); //db.get_itemlist(srch, typ, sortby1, sortby2);

                if (dt != null)
                {

                    dgv_list.Rows.Clear();

                    for (int r = 0; dgv_list.Rows.Count> r; r++)
                    {

                        dgv_list.Rows.Add();
                        

                        dgv_list["dgvi_qty", r].Value = gm.toAccountingFormat(dt.Rows[r]["qty_onhand_su"].ToString());
                        dgv_list["dgvi_part_no", r].Value = dt.Rows[r]["part_no"].ToString();
                        dgv_list["dgvi_tdesc", r].Value = dt.Rows[r]["item_desc"].ToString();
                        dgv_list["dgvi_unitdesc", r].Value = dt.Rows[r]["sale_unit"].ToString();
                        dgv_list["dgvi_cost", r].Value = gm.toAccountingFormat(dt.Rows[r]["unit_cost"].ToString());
                        dgv_list["dgvi_itemgrp", r].Value = dt.Rows[r]["grp_desc"].ToString();
                        dgv_list["dgvi_reg_price", r].Value = gm.toAccountingFormat(dt.Rows[r]["regular"].ToString());
                        dgv_list["dgvi_senior", r].Value = dt.Rows[r]["senior"].ToString();
                        dgv_list["dgvi_rack", r].Value = dt.Rows[r]["bin_loc"].ToString();
                        dgv_list["dgvi_unit", r].Value = dt.Rows[r]["sales_unit_id"].ToString();
                        dgv_list["dgvi_itemcode", r].Value = dt.Rows[r]["item_code"].ToString();
                        dgv_list["dgvi_brd_code", r].Value = dt.Rows[r]["brd_code"].ToString();
                        dgv_list["dgvi_brd_desc", r].Value = dt.Rows[r]["brd_name"].ToString();
                        dgv_list["dgvi_whs_desc", r].Value = dt.Rows[r]["whs_desc"].ToString();
                        dgv_list["dgvi_whs_code", r].Value = dt.Rows[r]["whs_code"].ToString();
                        dgv_list["dgvi_branch", r].Value = dt.Rows[r]["branchcode"].ToString();
                        dgv_list["dgvi_branchname", r].Value = dt.Rows[r]["branchname"].ToString();
                        dgv_list["dgvi_itemgrpid", r].Value = dt.Rows[r]["item_grp"].ToString();
                       
                    }
                }*/
               
            }
            catch (Exception err) { }
        }

        private void cbo_itmgrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (issearch_ready)
            {
                disp_list(txt_search.Text);
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

        private void goto_win1()
        {
            tbcntrl_list.SelectedTab = tpg_category;
            tpg_category.Show();
        }

        private void goto_win2()
        {
            tbcntrl_list.SelectedTab = tpg_item;
            tpg_item.Show();
        }

        private void dgv_category_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_category.Rows.Count > 0)
                {
                    int r = dgv_category.CurrentRow.Index;
                    cbo_itmgrp.SelectedValue = dgv_category["item_grp", r].Value.ToString();

                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }

            goto_win2();
        }
    }
}
