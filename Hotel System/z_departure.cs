using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class z_departure : Form
    {
        public newReservation frm = null;
        public newArrivalWalkin frmarrival = null;
        public newInhouse frmhouse = null;
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();


        public z_departure()
        {
            InitializeComponent();
        }
         public z_departure(newInhouse frm,String arr_date , String dep_date)
        {
            InitializeComponent();
            this.frmhouse = frm;
            dtp_arr_date.Value = Convert.ToDateTime(arr_date);
            dtp_dep_date.Value = Convert.ToDateTime(dep_date);
        }
        private void z_departure_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //int days = 0;
            //double das = 0.00;
            //TimeSpan span = dtp_dep_date.Value.Subtract(dtp_arr_date.Value);
            //das = Math.Round(span.TotalDays, 1);
            //days = (int)das;
            //lbl_nights.Text = days.ToString();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            try
            {
                if (frmhouse != null)
                {
                    frmhouse.lbl_arrdt.Text = Convert.ToDateTime(dtp_arr_date.Value).ToString("MM/dd/yyyy");
                    frmhouse.lbl_depdt.Text = Convert.ToDateTime(dtp_dep_date.Value).ToString("MM/dd/yyyy");
                    success = true;
                }
                if (success)
                {
                    MessageBox.Show("Departure Date Successfully Set.");
                }
                else
                {
                    MessageBox.Show("Problem Occurs.");
                }
            }
            catch { }
            this.Close();
            
        }

        private void dtp_dep_date_ValueChanged(object sender, EventArgs e)
        {
            //int days = 0;
            //double das = 0.00;
            //TimeSpan span = dtp_dep_date.Value.Subtract(dtp_arr_date.Value);
            //das = Math.Round(span.TotalDays, 1);
            //days = (int)das;
            //lbl_nights.Text = days.ToString();
        }
    }
}
