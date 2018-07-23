using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hotel_System
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
           // Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());

            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            DialogResult result;

            using (var loginForm = new Login())

                result = loginForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                // login was successful
                Application.Run(new Main());
            }
        }
    }
}
