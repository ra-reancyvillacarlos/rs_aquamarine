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
    public partial class z_HotelSettings : Form
    {
        //private externalDatabase.hms_testEntities all = new externalDatabase.hms_testEntities();

        public z_HotelSettings()
        {
            InitializeComponent();
            this.Load +=z_HotelSettings_Load;
            cbo_emailList.SelectedValueChanged += cbo_emailList_SelectedValueChanged;
            cbo_emailList.SelectedIndexChanged += cbo_emailList_SelectedIndexChanged;
        }

        void cbo_emailList_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPortNumber();
        }

        void cbo_emailList_SelectedValueChanged(object sender, EventArgs e)
        {
            setPortNumber();
        }

        private void setPortNumber()
        {
            var email = cbo_emailList.Text;
            //var port = all.email.Where(p => p.email_provider.Contains(email)).FirstOrDefault();

            //txtPortNumber.Text = port.port_number.ToString();
        }

        private void z_HotelSettings_Load(object sender, EventArgs e)
        {
            //var provider = all.email.Select(p => p.email_provider).ToList();

            //cbo_emailList.DataSource = provider;
            //cbo_emailList.DisplayMember = "email_provider";

            ////var companies = all.company.Select(p => p.comp_name).ToList();

            //cbo_companyEmail.DataSource = companies;
            //cbo_companyEmail.DisplayMember = "comp_name";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string username = "", password = "", recipient = "", smtp = "", provider="";
            int port = 0;

            provider = cbo_emailList.Text;
            username = txtUsernameEmail.Text;
            password = txtPasswordEmail.Text;
            recipient = txtRecipientEmail.Text;

            //smtp = all.email.Where(p => p.email_provider == provider).Select(a => a.smtp).FirstOrDefault();

           // port = Convert.ToInt32(all.email.Where(p => p.email_provider == provider).Select(a => a.port_number).FirstOrDefault());

            //EmailAutomationClass.Class1 email = new EmailAutomationClass.Class1();

            //if(email.sendEmailPreparation(DateTime.Now.ToString("MM-dd-yyyy"), username, password, recipient, smtp, provider, port))
            //{
            //    MessageBox.Show("Message sent!");
            //}
            
        }

        private void btn_clearall_Click(object sender, EventArgs e)
        {

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_email_save_Click(object sender, EventArgs e)
        {
            //save to m99
            
        }
    }
}
