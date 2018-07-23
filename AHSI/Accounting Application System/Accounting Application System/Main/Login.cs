using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    public partial class Login : Form
    {
        Main frm;
        thisDatabase db;
        GlobalClass gc;        
        public string ok = "ok";
        string cipher = "", mac_add = "", decipher = "";
        public Login()
        {
            InitializeComponent();
            GetMACAddress();
            frm = new Main();

            try
            {
                db = new thisDatabase();
                gc = new GlobalClass();

                lbl_server.Text = thisDatabase.servers;

                if(ApplicationDeployment.IsNetworkDeployed)
                {
                    lbl_version.Text = String.Format("Version {0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
                }

            }
            catch (Exception er)
            {
                MessageBox.Show("Wrong Database information. \n" + er.Message);
            }
        }

        private void btn_access_Click(object sender, EventArgs e)
        {
            auto_cipher();
            if (ok == "ok")
            {
                enter_login();
            }
           
        }
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        void auto_cipher()
        {
            if (mac_add != string.Empty)
            {

                if (mac_add  != string.Empty)
                {
                    //Here key is of 128 bit  
                    //Key should be either of 128 bit or of 192 bit  
                    cipher = Login.Encrypt(mac_add, "sblw-3hn8-sqoy19");
                }
                if (cipher != string.Empty)
                {
                    //Key shpuld be same for encryption and decryption  
                   decipher = Login.Decrypt(cipher, "sblw-3hn8-sqoy19");
                }
                DataTable dt = new DataTable();
                dt = db.QueryBySQLCode("SELECT * from rssys.x09 WHERE licensed_pc='"+cipher+"'");
                try
                {
                    if (dt.Rows.Count <= 0)
                    {
                        activation_form ac = new activation_form(this, mac_add, cipher);
                        ac.ShowDialog();
                    }
                    else
                    {
                        ok = "ok";
                    }
                }
                catch 
                {
                    activation_form ac = new activation_form(this, mac_add, cipher);
                    ac.ShowDialog();
                }
            }
        }
        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            this.mac_add = sMacAddress;
            return ﻿sMacAddress;
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                GlobalClass gm = new GlobalClass();

                gm.load_branch(cbo_branch);
                gc.load_branch(cbo_branch);

                //cbo_branch.SelectedValue = db.get_m99branch();
            }
            catch { }
        }

        private void txt_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                auto_cipher();
                if (ok == "ok")
                {
                    enter_login();
                }
            }
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                auto_cipher();
                if (ok == "ok")
                {
                    enter_login();
                }
            }
        }

        private void enter_login()
        {
            try
            {
                if (cbo_db.SelectedIndex == -1)
                {
                    MessageBox.Show("Please the database.");
                    cbo_db.DroppedDown = true;
                }
                else if (cbo_branch.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a branch.");
                    cbo_branch.DroppedDown = true;
                }
                else
                {
                    String branch = (cbo_branch.SelectedValue ?? "").ToString(); //db.get_m99branch();
                    String comp = db.get_m99comp_name();
                    String user = db.validate_login(txt_user.Text, txt_pass.Text, comp);

                    if (String.IsNullOrEmpty(user) == false)
                    {
                        GlobalClass.username = user;
                        GlobalClass.branch = branch;
                        GlobalClass.server_ip = lbl_server.Text;
                        Hotel_System.GlobalClass.username = user;
                        Hotel_System.GlobalClass.branch = branch;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Invalid username and password. Pls try again.");
                        txt_pass.Text = "";
                    }
                }
            }
            catch { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("https://web.facebook.com/RightAppsSolutions/");

            MessageBox.Show("For information, contact 0915-806-0792 / 0942-734-7599.\nAlso like us on facebook \n https://web.facebook.com/RightAppsOfficial/");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cbo_db_SelectedIndexChanged(object sender, EventArgs e)
        {
            after_change_cbo();
        }

        private void after_change_cbo()
        {

            String branch = "";
            Boolean isMain = false;

            try
            {
                if (cbo_db.SelectedIndex == 0)
                {
                    Accounting_Application_System.thisDatabase.db_name = "aquamarine"; //tsc residences;
                    Hotel_System.thisDatabase.db_name = "aquamarine"; //pms_eastland_050318 pms_eastland pms_eastland_after_closing pms_eastland_before_closing pms_eastland_compare_backup reancy_eastland pms_eastland_060618 curr_eastland pms_curr
                    gc.load_branch(cbo_branch);

                    pbox_db.Image = Properties.Resources.aquamarine;
                    pbox_db.Visible = true;
                    lbl_test_side.Visible = false;
                    

                }
                else
                {
                    thisDatabase.db_name = "inv_test";
                    Hotel_System.thisDatabase.db_name = "inv_test";

                    gc.load_branch(cbo_branch);

                    pbox_db.Visible = false;
                    lbl_test_side.Visible = true;
                }
            }
            catch { }

            try
            {
                thisDatabase ldb = new thisDatabase();

                branch = ldb.get_m99branch();
                isMain = ldb.get_isMainBranch();

                cbo_branch.SelectedValue = branch;

                if (isMain)
                {
                    cbo_branch.Enabled = true;
                }
                else
                {
                    cbo_branch.Enabled = false;
                }
            }
            catch { }    
        }

        private void cbo_db_ValueMemberChanged(object sender, EventArgs e)
        {
            after_change_cbo();
        }
    }
}
