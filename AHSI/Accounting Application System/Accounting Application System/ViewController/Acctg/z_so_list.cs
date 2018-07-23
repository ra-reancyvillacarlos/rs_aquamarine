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
    public partial class z_so_list : Form
    {
        public z_so_list()
        {
            InitializeComponent();
        }

        private void z_so_list_Load(object sender, EventArgs e)
        {
            dtp_frm.ValueChanged += dtp_frm_ValueChanged;
            dtp_to.ValueChanged +=dtp_to_ValueChanged;
            disp_dgvlist();
        }

        void dtp_frm_ValueChanged(object sender, EventArgs e)
        {
            disp_dgvlist();            
        }

        private void disp_dgvlist()
        {
            try
            {
                thisDatabase db = new thisDatabase();
                dgv_list.DataSource = db.get_solist(dtp_frm.Value, dtp_to.Value, false);
            }
            catch (Exception) { }
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            disp_dgvlist();
        }
    }
}
