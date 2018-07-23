using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DialogResult result;

            //Application.Run(new Form1());
            //return;

            using (var loginForm = new Login())
            
                result = loginForm.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                 //login was successful
                Application.Run(new Main());
               //Application.Run(new call_history());
            } 
        }
    }
}
