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
    public partial class Remarks : Form
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        s_RepairOrder frm_ro = null;
        public Remarks()
        {
            InitializeComponent();
        }
        public Remarks(s_RepairOrder frm , String code)
        {
            InitializeComponent();
            frm_ro = frm;
            gc.load_technician(cbo_checkedby);
            gc.load_technician(cbo_approvedby);
            gc.load_technician(cbo_drivetest);
            gc.load_salesclerk(cbo_clerk);
            init_load(code);

        }
        void init_load(String code )
        {
            DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.orhdr WHERE ord_code='"+code+"'");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++ )
                {
                    rtxt_remark.Text = dt.Rows[i]["remark"].ToString();
                    rtxt_tech_notes.Text = dt.Rows[i]["tech_remark"].ToString();
                    cbo_approvedby.SelectedValue = dt.Rows[i]["approvedby_id"].ToString();
                    cbo_checkedby.SelectedValue = dt.Rows[i]["checkedby_id"].ToString();
                    cbo_drivetest.SelectedValue = dt.Rows[i]["drivetester_id"].ToString();
                    cbo_clerk.SelectedValue = dt.Rows[i]["rep_code"].ToString();
                    dtp_promise_date.Value = gm.toDateValue(dt.Rows[i]["promise_date"].ToString());
                    dtp_promisetime.Value = gm.toDateValue(dt.Rows[i]["promise_time"].ToString());


                }

            }
        }
        private void Remarks_Load(object sender, EventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void cbo_clerk_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
