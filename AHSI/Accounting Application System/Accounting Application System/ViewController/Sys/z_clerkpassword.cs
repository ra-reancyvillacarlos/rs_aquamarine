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
    public partial class z_clerkpassword : Form
    {
        //private const int CP_NOCLOSE_BUTTON = 0x200;
        //forms
        private s_Sales frm_sales = null;
        private s_Sales_Auto frm_salesauto = null;
        private auto_loanapplication frm_autoloan = null;
        private s_RepairOrder frm_ro = null;
        private z_enter_sales_item frm_salesitem;
        private s_ServicesDispatch frm_serdisp = null;
        private s_Release_Deliver_Unit frm_reldel_unit = null;
        private s_GP_Computation frm_gp_compute;
        private z_ItemSearch frm_itemsearch = null;

        //added by Reancy 05 31 2018
        private Main mn;
        String loan = "";
        //other
        private Boolean _isAdmin = false;
        private Boolean _passwordConfrim = false;
        GlobalClass gc;
        GlobalMethod gm;

        public z_clerkpassword(s_Sales sale)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_salesclerk(cbo_id);
            cbo_id.AllowDrop = true;
            this.ActiveControl = cbo_id;

            frm_sales = sale;
        }

        public z_clerkpassword(z_enter_sales_item salesitem)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_userfullname(cbo_id);
            cbo_id.AllowDrop = true;

            frm_salesitem = salesitem;
        }

        public z_clerkpassword(z_ItemSearch itemsearch)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_userfullname(cbo_id);
            cbo_id.AllowDrop = true;
            this.ActiveControl = cbo_id;

            frm_itemsearch = itemsearch;
        }


        public z_clerkpassword(s_RepairOrder salesitem)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_salesclerk(cbo_id);
            cbo_id.AllowDrop = true;
            this.ActiveControl = cbo_id;
            
            frm_ro = salesitem;
        }
        public z_clerkpassword(s_RepairOrder salesitem, String isloan)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_salesclerk(cbo_id);
            cbo_id.AllowDrop = true;
            this.ActiveControl = cbo_id;
            this.loan = isloan;
            frm_ro = salesitem;
        }
         
        public z_clerkpassword(auto_loanapplication autoloan)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_userfullname(cbo_id);
            cbo_id.AllowDrop = true;
            this.ActiveControl = cbo_id;

            frm_autoloan = autoloan;
        }

        public z_clerkpassword(s_ServicesDispatch servdisp)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_userfullname(cbo_id);
            cbo_id.AllowDrop = true;
            this.ActiveControl = cbo_id;

            frm_serdisp = servdisp;
        }

        public z_clerkpassword(s_Sales_Auto salesauto)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_salesclerk(cbo_id);
            cbo_id.AllowDrop = true;
            this.ActiveControl = cbo_id;

            frm_salesauto = salesauto;
        }
        public z_clerkpassword(s_GP_Computation frm)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_salesclerk(cbo_id);
            cbo_id.AllowDrop = true;
            this.ActiveControl = cbo_id;

            frm_gp_compute = frm;
        }
        public z_clerkpassword(s_Release_Deliver_Unit frm)
        {
            InitializeComponent();

            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_salesclerk(cbo_id);
            cbo_id.AllowDrop = true;
            this.ActiveControl = cbo_id;

            frm_reldel_unit = frm;
        }
         

        public z_clerkpassword(Boolean isPasswordForAdmin)
        {
            InitializeComponent();

            _isAdmin = isPasswordForAdmin;
        }

        private void cbo_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enter_verification();
            }
        }

        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {           
            if (e.KeyCode == Keys.Enter)
            {
                enter_verification();
            }
        }

        private void enter_verification()
        {
            thisDatabase db = new thisDatabase();
            String[] clerk;
            String userid;

            try
            {
                if (_isAdmin == true)
                {
                    DataTable dt = db.QueryOnTableWithParams("x08", "uid", "uid='" + cbo_id.Text.ToString().ToUpper() + "' AND pwd=$$" + txt_password.Text.ToString() + "$$ AND grp_id='001'", "");

                    if (dt.Rows.Count > 0)
                    {
                        mn = new Main();
                        mn.verifiedClerk(true);
                        _passwordConfrim = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect User ID or Password");
                    }
                }
                else if (frm_sales != null)
                {
                    clerk = db.verifyClerk(txt_password.Text);

                    if (clerk.Length > 0)
                    {
                        frm_sales.verifiedClerk(clerk[0].ToString(), clerk[1].ToString());

                        txt_password.Text = "";
                        this.Close();
                        //frm_sales.Show();
                    }
                    else
                    {
                        MessageBox.Show("Password not registered.");
                        txt_password.Text = "";
                    }
                }
                else if (frm_reldel_unit != null)
                {
                    clerk = db.verifyClerk(txt_password.Text);

                    if (clerk.Length > 0)
                    {
                        frm_reldel_unit.verifiedClerk(clerk[0].ToString(), clerk[1].ToString());

                        txt_password.Text = "";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password not registered.");
                        txt_password.Text = "";
                    }
                }
                else if (frm_salesauto != null)
                {
                    clerk = db.verifyClerk(txt_password.Text);

                    if (clerk.Length > 0)
                    {
                        frm_salesauto.verifiedClerk(clerk[0].ToString(), clerk[1].ToString());

                        txt_password.Text = "";
                        this.Close();
                        //frm_salesauto.Show();
                    }
                    else
                    {
                        MessageBox.Show("Password not registered.");
                        txt_password.Text = "";
                    }
                }
                else if (frm_gp_compute != null)
                {
                    clerk = db.verifyClerk(txt_password.Text);

                    if (clerk.Length > 0)
                    {
                        frm_gp_compute.verifiedClerk(clerk[0].ToString(), clerk[1].ToString());
                        txt_password.Text = "";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password not registered.");
                        txt_password.Text = "";
                    }
                }
                else if (frm_autoloan != null)
                {
                    clerk = db.verifyClerk(txt_password.Text);

                    if (clerk.Length > 0)
                    {
                        frm_autoloan.verifiedClerk(clerk[0].ToString(), clerk[1].ToString());

                        txt_password.Text = "";
                        this.Close();
                        //frm_salesauto.Show();
                    }
                    else
                    {
                        MessageBox.Show("Password not registered.");
                        txt_password.Text = "";
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Password not registered.\n" + er.Message);
                txt_password.Text = "";
            }
        }

        private void z_clerkpassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            //frm_sales.
        }

        private void z_clerkpassword_Load(object sender, EventArgs e)
        {

        }

        public Boolean get_adminpasswordconfrimation()
        {
            return _passwordConfrim;
        }

        /*
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }*/
    }
}
