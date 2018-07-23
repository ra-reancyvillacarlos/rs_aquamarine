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
    public partial class auto_loan_items : Form
    {
        auto_loanapplication _frm_auto = null;
        String gfolio = "";
        GlobalClass gc;
        GlobalMethod gm;
        String gtype = "";
        Boolean newitem = false;
        public auto_loan_items()
        {
            InitializeComponent();
        }
         public auto_loan_items(auto_loanapplication frm)
        {
            InitializeComponent();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            _frm_auto = frm;
          
           disp_items();
        }
        private void auto_loan_items_Load(object sender, EventArgs e)
        {

        }
        public void disp_items()
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            DataTable dtca = null;
            DataTable dtr = null;

            dtca = db.QueryBySQLCode("SELECT cndpvl_code,cndpvl_desc FROM rssys.cond_approvals");
            dtr = db.QueryBySQLCode("SELECT rqrt_code, rqrt_desc FROM rssys.requirements");
           // clear_dgv_list_folio();

            try
            {
                if (_frm_auto != null)
                {

                    for (int r = 0; dtca.Rows.Count > r; r++)
                    {
                        int i = dgv_list_ca.Rows.Add();
                        DataGridViewRow row = dgv_list_ca.Rows[i];

                        row.Cells["dgvlca2_line"].Value = (r + 1).ToString();
                        row.Cells["dgvlca2_code"].Value = dtca.Rows[r]["cndpvl_code"].ToString();
                        row.Cells["dgvlca2_desc"].Value = dtca.Rows[r]["cndpvl_desc"].ToString();

                        for (int r2 = 0; r2 < _frm_auto.dgv_conditions.Rows.Count; r2++)
                        {
                            String _frm_auto_dgvlca_code = _frm_auto.dgv_conditions["dgvlca_code", r2].Value.ToString();
                            if (_frm_auto_dgvlca_code.Equals(dtca.Rows[r]["cndpvl_code"].ToString()))
                            { 
                                row.Cells["dgvlca2_chk"].Value = true;
                            }
                        }
                    }

                    for (int r = 0; dtr.Rows.Count > r; r++)
                    {
                        int i = dgv_list_r.Rows.Add();
                        DataGridViewRow row = dgv_list_r.Rows[i];

                        row.Cells["dgvlr2_line"].Value = (r + 1).ToString();
                        row.Cells["dgvlr2_code"].Value = dtr.Rows[r]["rqrt_code"].ToString();
                        row.Cells["dgvlr2_desc"].Value = dtr.Rows[r]["rqrt_desc"].ToString();

                        for (int r2 = 0; r2 < _frm_auto.dgv_docreq.Rows.Count; r2++)
                        {
                            String _frm_auto_dgvlr_code = _frm_auto.dgv_docreq["dgvlr_code", r2].Value.ToString();
                            if (_frm_auto_dgvlr_code.Equals(dtr.Rows[r]["rqrt_code"].ToString()))
                            {
                                row.Cells["dgvlr2_chk"].Value = true;
                            }
                        }
                    }
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_addtolist_Click(object sender, EventArgs e)
        {
            Boolean status = false;
            _frm_auto.dgv_conditions.Rows.Clear();
            _frm_auto.dgv_docreq.Rows.Clear();

            int i = 0;
            for (int r = 0; r < dgv_list_ca.Rows.Count - 1; r++)
            {


                try { status = Convert.ToBoolean(dgv_list_ca["dgvlca2_chk", r].Value.ToString()); }
                catch
                {
                    status = false;
                }

                try
                {
                    if (status)
                    {
                        i = _frm_auto.dgv_conditions.Rows.Count;
                        _frm_auto.dgv_conditions.Rows.Add();

                        _frm_auto.dgv_conditions["dgvlca_line", i].Value = dgv_list_ca["dgvlca2_line", r].Value.ToString();
                        _frm_auto.dgv_conditions["dgvlca_code", i].Value = dgv_list_ca["dgvlca2_code", r].Value.ToString();
                        _frm_auto.dgv_conditions["dgvlca_description", i].Value = dgv_list_ca["dgvlca2_desc", r].Value.ToString();

                    }
                }
                catch (Exception er) { MessageBox.Show(er.Message); }

            }

            i = 0;
            for (int r = 0; r < dgv_list_r.Rows.Count - 1; r++)
            {
                try { status = Convert.ToBoolean(dgv_list_r["dgvlr2_chk", r].Value.ToString()); }
                catch
                {
                    status = false;
                }

                try
                {
                    if (status)
                    {
                        i = _frm_auto.dgv_docreq.Rows.Count;

                        _frm_auto.dgv_docreq.Rows.Add();

                        _frm_auto.dgv_docreq["dgvlr_line", i].Value = dgv_list_r["dgvlr2_line", r].Value.ToString();
                        _frm_auto.dgv_docreq["dgvlr_code", i].Value = dgv_list_r["dgvlr2_code", r].Value.ToString();
                        _frm_auto.dgv_docreq["dgvlr_desc", i].Value = dgv_list_r["dgvlr2_desc", r].Value.ToString();

                    }
                }
                catch (Exception er) { MessageBox.Show(er.Message); }

            }

            this.Close();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        
    }
}
