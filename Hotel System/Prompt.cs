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
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 150;
            prompt.WindowState = FormWindowState.Normal;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.FormBorderStyle = FormBorderStyle.FixedSingle;
            prompt.MaximizeBox = false;
            prompt.Font = new Font("Microsoft Sans Serif", (float)10.0);
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 10, Text = text, Width = 420 };
            TextBox textBox = new TextBox() { Left = 50, Top = 40, Width = 400, Height = 50 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Height = 40, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.ShowDialog();
            return textBox.Text;
        }

        public static string ShowPasswordDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 150;
            prompt.WindowState = FormWindowState.Normal;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.FormBorderStyle = FormBorderStyle.FixedSingle;
            prompt.MaximizeBox = false;
            prompt.Font = new Font("Microsoft Sans Serif", (float)10.0);
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 10, Text = text, Width = 420 };
            TextBox textBox = new TextBox() { Left = 50, Top = 40, Width = 400, Height = 50, PasswordChar = '*' };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Height = 40, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.ShowDialog();
            return textBox.Text;
        }

        public static string[] ShowDialogDeposit(DataTable chg_dt, Color BColor)
        {
            Boolean isconfirm = false;
            Boolean blHasDot = false;
            string[] arr_data = new string[8];
            Form prompt = new Form();
            prompt.Width = 400;
            prompt.Height = 580;
            prompt.WindowState = FormWindowState.Normal;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.FormBorderStyle = FormBorderStyle.FixedSingle;
            prompt.BackColor = BColor;
            prompt.MaximizeBox = false;
            prompt.Font = new Font("Microsoft Sans Serif", (float)10.0);
            prompt.Text = "Deposit Entry";

            Font txt_amt = new Font("Microsoft Sans Serif", (float)14.0); 
            Label lbl_chg = new Label() { Left = 50, Top = 10, Text = "Payment Type", Width = 420 };
            ComboBox cbo_chg = new ComboBox() { Left = 50, Top = 40, Width = 300 };

            Label lbl_doc = new Label() { Left = 50, Top = 70, Text = "Document Type", Width = 420 };
            ComboBox cbo_doc = new ComboBox() { Left = 50, Top = 100, Width = 300 };

            Label lbl_ref = new Label() { Left = 50, Top = 130, Text = "Reference", Width = 420 };
            TextBox text_ref = new TextBox() { Left = 50, Top = 160, Width = 200, Height = 100, Text = "" };

            Label textLabel = new Label() { Left = 50, Top = 190, Text = "Deposit Amount", Width = 420 };
            TextBox textBox = new TextBox() { Left = 50, Top = 220, Width = 200, Height = 100, Font = txt_amt, Text = "0.00" };

            Label textLabel_crdno = new Label() { Left = 50, Top = 255, Text = "Card Number", Width = 420 };
            TextBox txt_crdno = new TextBox() { Left = 50, Top = 280, Width = 200, Height = 100, Text = "" };

            Label textLabel_traceno = new Label() { Left = 50, Top = 315, Text = "Trace Number", Width = 420 };
            TextBox txt_traceno = new TextBox() { Left = 50, Top = 340, Width = 200, Height = 100, Text="" };

            Label lbl_sep = new Label() { Left = 50, Top = 370, Text = "----------------------------------------------------------", Width = 420};

            Label textLabel_reason = new Label() { Left = 50, Top = 390, Text = "If no deposit, pls type here the reason.", Width = 420, Font = new Font("Microsoft Sans Serif", (float)10.0, FontStyle.Italic)};
            RichTextBox txt_reason = new RichTextBox() { Left = 50, Top = 415, Width = 300, Height = 70, Text = "" };

            Button confirmation = new Button() { Text = "Ok", Left = 250, Width = 100, Height = 50, Top = 490 };
            Button close = new Button() { Text = "Close", Left = 120, Width = 100, Height = 50, Top = 490 };

            cbo_chg.DataSource = chg_dt;
            cbo_chg.DisplayMember = "chg_desc";
            cbo_chg.ValueMember = "chg_code";

            cbo_chg.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbo_chg.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbo_doc.Items.Add("Cash Credit");
            cbo_doc.Items.Add("No O.R.");
            cbo_doc.DropDownStyle = ComboBoxStyle.DropDownList;

            textBox.TextAlign = HorizontalAlignment.Right;

            cbo_doc.SelectedIndexChanged += (sender, e) =>
            {
                if (cbo_doc.Text.ToString() == "Cash Credit")
                {
                    thisDatabase db = new thisDatabase();

                    String ccno = db.get_pk("next_cc");
                    text_ref.Text = ccno;

                    text_ref.ReadOnly = true;
                }
                else
                {
                    text_ref.Text = "NO O.R.";
                    text_ref.ReadOnly = false;
                }
            };

            textBox.KeyPress += (sender, e) => {
                if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '-')
                {
                    // Allow Digits and BackSpace char
                    if (e.KeyChar == '\b')
                    {
                        if (textBox.Text.Contains(".") == false)
                        {
                            blHasDot = false;
                        }
                    }
                }
                else if (e.KeyChar == '.' && !blHasDot)
                {
                    //Allows only one Dot Char
                    blHasDot = true;
                }
                else
                {
                    e.Handled = true;
                }            
            };

            confirmation.Click += (sender, e) => {
                Boolean flag = true;

                if(txt_reason.Text.Trim() != "")
                {
                    flag = false;

                    if (MessageBox.Show("You have been typed the reason box for no deposit.\nDo you really want to Check in this guest WITHOUT deposit?", "Confirmation Box", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        flag = true;
                    }
                }
                else if (cbo_doc.SelectedIndex == -1)
                {
                    flag = false;
                    MessageBox.Show("Pls select the document type.");
                }
                else if (text_ref.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Pls type the reference.");
                }
                else if (textBox.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Invalid amount.");
                }
                else if (Convert.ToDouble(textBox.Text) == 0.00)
                {
                    flag = false;
                    MessageBox.Show("Invalid amount.");
                }
                else if (cbo_chg.SelectedValue.ToString() == "102" || cbo_chg.SelectedValue.ToString() == "103" || cbo_chg.SelectedValue.ToString() == "104" || cbo_chg.SelectedValue.ToString() == "105")
                {
                    if(txt_crdno.Text == "")
                    {
                        flag = false;
                        MessageBox.Show("Card Number should be inputted.");
                    }
                    else if(txt_traceno.Text == "")
                    {
                        flag = false;
                        MessageBox.Show("Trace Number should be inputted.");
                    }
                }
                
                if(flag)
                {
                    isconfirm = true;
                    prompt.Close();
                }
            };

            close.Click += (sender, e) =>
            {
                prompt.Close();
            };

            prompt.ControlBox = false;
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(close);
            prompt.Controls.Add(lbl_chg);
            prompt.Controls.Add(cbo_chg); //charge description / payment
            prompt.Controls.Add(lbl_doc);
            prompt.Controls.Add(cbo_doc); // document type
            prompt.Controls.Add(text_ref); // reference
            prompt.Controls.Add(lbl_ref); 
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox); // amount
            prompt.Controls.Add(textLabel_crdno);
            prompt.Controls.Add(txt_crdno);
            prompt.Controls.Add(textLabel_traceno);
            prompt.Controls.Add(txt_traceno);

            prompt.Controls.Add(lbl_sep);
            prompt.Controls.Add(textLabel_reason);
            prompt.Controls.Add(txt_reason);
            prompt.ShowDialog();

            if (cbo_chg.SelectedIndex == -1)
            {
                arr_data.SetValue("", 0);
                arr_data.SetValue("", 4);
            }
            else if (cbo_chg.SelectedIndex != -1)
            {
                arr_data.SetValue(cbo_chg.SelectedValue.ToString(), 0);
                arr_data.SetValue(cbo_chg.Text, 4);
            }
            if (cbo_doc.SelectedIndex == -1)
            {
                arr_data.SetValue("", 1);
            }
            else if (cbo_doc.SelectedIndex != -1)
            {
                arr_data.SetValue(cbo_doc.Text.ToString(), 1);
            }

            arr_data.SetValue(text_ref.Text, 2);
            arr_data.SetValue(textBox.Text, 3);
            arr_data.SetValue(txt_crdno.Text, 5);
            arr_data.SetValue(txt_traceno.Text, 6);
            arr_data.SetValue(txt_reason.Text, 7);

            if (isconfirm == true)
            {
                return arr_data;
            }

            return null;
        }
    }
}
