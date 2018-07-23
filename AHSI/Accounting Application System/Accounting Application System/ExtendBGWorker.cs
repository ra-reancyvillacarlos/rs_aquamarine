using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Accounting_Application_System
{
    public static class ExtendBGWorker
    {
        //Text Box
        public static String getText(this TextBox txtbox)
        {
            String str = "";
            try
            {
                txtbox.Invoke(new Action(() =>
                {
                    str = txtbox.Text;
                }));
            }
            catch { str = txtbox.Text; }
            return str;
        }
        //End of Text Box

        //Combo Box
        public static String getSelectedValue(this ComboBox cbo)
        {
            String str = "";
            try
            {
                cbo.Invoke(new Action(() =>
                {
                    str = cbo.SelectedValue.ToString();
                }));
            }
            catch { str = cbo.SelectedValue.ToString(); }
            return str;
        }
        public static String getSelectedText(this ComboBox cbo)
        {
            String str = "";
            try
            {
                cbo.Invoke(new Action(() =>
                {
                    str = cbo.SelectedText.ToString();
                }));
            }
            catch { str = cbo.SelectedText.ToString(); }
            return str;
        }
        public static String getText(this ComboBox cbo)
        {
            String str = "";
            try
            {
                cbo.Invoke(new Action(() =>
                {
                    str = cbo.Text;
                }));
            }
            catch { str = cbo.Text; }
            return str;
        }
        public static int getSelectedIndex(this ComboBox cbo)
        {
            int indx = -1;
            try
            {
                cbo.Invoke(new Action(() =>
                {
                    indx = cbo.SelectedIndex;
                }));
            }
            catch { indx = cbo.SelectedIndex; }
            return indx;
        }
        //End of Combo Box

        // DateTimePicker
        public static String getStringDate(this DateTimePicker dtp, String format = null)
        {
            String str = "";

            try
            {
                dtp.Invoke(new Action(() =>
                {
                    if (format == null)
                    {
                        format = "yyyy-MM-dd";
                    }
                    try
                    {
                        str = dtp.Value.ToString(format);
                    }
                    catch { str = DateTime.Now.ToString("yyyy-MM-dd"); }
                }));
            }
            catch
            {
                if (format == null)
                {
                    format = "yyyy-MM-dd";
                }
                try
                {
                    str = dtp.Value.ToString(format);
                }
                catch { str = DateTime.Now.ToString("yyyy-MM-dd"); }
            }
            return str;
        }
        public static String getStringTime(this DateTimePicker dtp, String format = null)
        {
            String str = "";
            try
            {
                dtp.Invoke(new Action(() =>
                {
                    if (format == null)
                    {
                        format = "HH:mm";
                    }
                    try
                    {
                        str = dtp.Value.ToString(format);
                    }
                    catch { str = DateTime.Now.ToString("HH:mm"); }
                }));
            }
            catch
            {
                if (format == null)
                {
                    format = "HH:mm";
                }
                try
                {
                    str = dtp.Value.ToString(format);
                }
                catch { str = DateTime.Now.ToString("HH:mm"); }
            }
            return str;
        }
        //End of DateTime Picker
    }
}
