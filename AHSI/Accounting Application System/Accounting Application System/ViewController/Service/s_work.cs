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
    public partial class s_work : Form
    {


        GlobalClass gc;
        thisDatabase db;
        GlobalMethod gm;
        s_ServicesDispatch _frm_sd;
        Boolean isnew = false;
        int upd = 0;
        public s_work()
        {
           
            InitializeComponent();
            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            gc.load_technician(cbo_technician);
            gc.load_work(cbo_work);
            txt_itemcode.Text = "0";
        }

        private void s_work_Load(object sender, EventArgs e)
        {

        }
        public s_work(s_ServicesDispatch frm_sd, Boolean is_new)
        {
               int index =0;
              
               int inval =0;
            InitializeComponent();

            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            gc.load_technician(cbo_technician);
            gc.load_work(cbo_work);
            txt_itemcode.Text = "0";
            _frm_sd = frm_sd;
            this.isnew = is_new;
            if (_frm_sd.dgv_service.Rows.Count > 0)
            {
                index = _frm_sd.dgv_service.Rows.Count;
                inval = Int32.Parse(_frm_sd.dgv_service["dgvwp_ln", index - 1].Value.ToString()) + 1;
                txt_lnno.Text = inval.ToString();
            }
            if (is_new == false)
            {
                
                upd = _frm_sd.dgv_service.CurrentRow.Index;
                //MessageBox.Show(upd.ToString()+ "\n" + is_new.ToString());
                txt_lnno.Text = _frm_sd.dgv_service["dgvwp_ln", upd].Value.ToString();
                cbo_work.SelectedValue = _frm_sd.dgv_service["dgvwp_code", upd].Value.ToString();
                cbo_technician.SelectedValue = _frm_sd.dgv_service["dgvwp_tech", upd].Value.ToString();
                cbo_status.Text = _frm_sd.dgv_service["dgvwp_status", upd].Value.ToString();
                if (String.IsNullOrEmpty(cbo_status.Text)) cbo_status.SelectedIndex = 0;

                dtp_date.Value = gm.toDateValue(_frm_sd.dgv_service["dgvwp_date", upd].Value.ToString());
                dtp_time.Value = gm.toDateValue(_frm_sd.dgv_service["dgvwp_time", upd].Value.ToString());
                
            }
            
        }
        private void cbo_work_SelectedIndexChanged(object sender, EventArgs e)
        {
            //db = new thisDatabase();
            //DataTable dt = db.QueryBySQLCode("SELECT srv_code FROM service WHERE srv_code='"+cbo_work.selecte+"'");
            txt_itemcode.Text = cbo_work.SelectedValue.ToString();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            int index = 0;
            int inval = 0;
            int i = -1;

            if (cbo_work.SelectedIndex == -1)
            {
                MessageBox.Show("Please select work field.");
                cbo_work.DroppedDown = true;
            }
            else if (cbo_technician.SelectedIndex == -1)
            {
                MessageBox.Show("Please select technician field.");
                cbo_technician.DroppedDown = true;
            }
            else if (cbo_status.SelectedIndex == -1)
            {
                MessageBox.Show("Please select status field.");
                cbo_status.DroppedDown = true;
            }
            else
            {
                if (isnew)
                {
                    _frm_sd.dgv_service.Rows.Add();
                }

                if (_frm_sd != null)
                {
                    if (isnew)
                    {
                        i = _frm_sd.dgv_service.Rows.Count - 1;
                        _frm_sd.dgv_service["dgvwp_ln", i].Value = txt_lnno.Text;
                        _frm_sd.dgv_service["dgvwp_name", i].Value = cbo_work.Text;
                        _frm_sd.dgv_service["dgvwp_code", i].Value = cbo_work.SelectedValue.ToString();
                        _frm_sd.dgv_service["dgvwp_status", i].Value = cbo_status.Text;
                        _frm_sd.dgv_service["dgvwp_date", i].Value = dtp_date.Value.ToString("yyyy-MM-dd");
                        _frm_sd.dgv_service["dgvwp_time", i].Value = dtp_time.Value.ToString("hh:mm tt");
                        _frm_sd.dgv_service["dgvwp_tech", i].Value = cbo_technician.SelectedValue.ToString();
                    }
                    else
                    {
                        _frm_sd.dgv_service["dgvwp_ln", upd].Value = txt_lnno.Text;
                        _frm_sd.dgv_service["dgvwp_name", upd].Value = cbo_work.Text;
                        _frm_sd.dgv_service["dgvwp_code", upd].Value = cbo_work.SelectedValue.ToString();
                        _frm_sd.dgv_service["dgvwp_status", upd].Value = cbo_status.Text;
                        _frm_sd.dgv_service["dgvwp_date", upd].Value = dtp_date.Value.ToString("yyyy-MM-dd");
                        _frm_sd.dgv_service["dgvwp_time", upd].Value = dtp_time.Value.ToString("hh:mm tt");
                        _frm_sd.dgv_service["dgvwp_tech", upd].Value = cbo_technician.SelectedValue.ToString();
                    }
                }
                this.Close();
            }
        }
    }
}
