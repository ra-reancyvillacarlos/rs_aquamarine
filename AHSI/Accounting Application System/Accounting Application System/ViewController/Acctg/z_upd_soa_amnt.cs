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
    public partial class z_upd_soa_amnt : Form
    {
        a_CollectionEntry _frm_collection;

        public z_upd_soa_amnt(a_CollectionEntry frm, String amt)
        {
            InitializeComponent();

            GlobalMethod gm = new GlobalMethod();

            _frm_collection = frm;

            txt_amount.Text = gm.toAccountingFormat(gm.toNormalDoubleFormat(amt));
        }

        private void z_upd_soa_amnt_Load(object sender, EventArgs e)
        {

        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            send_update();
        }

        private void btn_itemclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                send_update();
            }
        }

        private void send_update()
        {
            GlobalMethod gm = new GlobalMethod();

            if (String.IsNullOrEmpty(txt_amount.Text) == true && gm.toNormalDoubleFormat(txt_amount.Text) <= 0)
            {
                MessageBox.Show("Invalid Amount");
            }
            else
            {
                //_frm_collection.upd_soa_amount(txt_amount.Text);
                this.Close();
            }
        }
    }
}
