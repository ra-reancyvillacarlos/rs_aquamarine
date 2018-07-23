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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enter_login();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lbl_server.Text = thisDatabase.servers;
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enter_login();
            }
        }

        private void txt_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enter_login();
            }
        }

        private void enter_login()
        {
            thisDatabase db = new thisDatabase();

            String user = db.validate_login(txt_user.Text, txt_pass.Text);

            if (user != null)
            {
                GlobalClass.username = user;
                GlobalClass.user_fullname = db.get_user_fullname(user);

                DialogResult = DialogResult.OK;
            }
            else
            {
                txt_pass.Text = "";
                MessageBox.Show("Invalid username and password. Pls try again.");
            }
        }
    }
}
