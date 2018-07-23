namespace Accounting_Application_System
{
    partial class z_payment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_csh_change = new System.Windows.Forms.TextBox();
            this.txt_csh_amttendered = new System.Windows.Forms.TextBox();
            this.lbl_balance = new System.Windows.Forms.Label();
            this.lbl_amnttendered = new System.Windows.Forms.Label();
            this.cbo_mop = new System.Windows.Forms.ComboBox();
            this.txt_terms_ref = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.txt_discamt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_amtdue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_charge = new System.Windows.Forms.ComboBox();
            this.btn_clr_dealer = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_csh_change
            // 
            this.txt_csh_change.BackColor = System.Drawing.SystemColors.Info;
            this.txt_csh_change.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_csh_change.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_csh_change.ForeColor = System.Drawing.Color.Red;
            this.txt_csh_change.Location = new System.Drawing.Point(149, 205);
            this.txt_csh_change.Name = "txt_csh_change";
            this.txt_csh_change.ReadOnly = true;
            this.txt_csh_change.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_csh_change.Size = new System.Drawing.Size(240, 19);
            this.txt_csh_change.TabIndex = 13;
            this.txt_csh_change.Text = "0.00";
            // 
            // txt_csh_amttendered
            // 
            this.txt_csh_amttendered.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_csh_amttendered.Location = new System.Drawing.Point(149, 168);
            this.txt_csh_amttendered.Name = "txt_csh_amttendered";
            this.txt_csh_amttendered.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_csh_amttendered.Size = new System.Drawing.Size(240, 26);
            this.txt_csh_amttendered.TabIndex = 12;
            this.txt_csh_amttendered.Text = "0.00";
            this.txt_csh_amttendered.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_csh_amttendered.TextChanged += new System.EventHandler(this.txt_csh_amttendered_TextChanged);
            this.txt_csh_amttendered.Enter += new System.EventHandler(this.txt_EnterTextNumber);
            this.txt_csh_amttendered.Leave += new System.EventHandler(this.txt_LeaveTextNumber);
            // 
            // lbl_balance
            // 
            this.lbl_balance.AutoSize = true;
            this.lbl_balance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_balance.ForeColor = System.Drawing.Color.Red;
            this.lbl_balance.Location = new System.Drawing.Point(16, 208);
            this.lbl_balance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_balance.Name = "lbl_balance";
            this.lbl_balance.Size = new System.Drawing.Size(58, 16);
            this.lbl_balance.TabIndex = 6;
            this.lbl_balance.Text = "Balance";
            // 
            // lbl_amnttendered
            // 
            this.lbl_amnttendered.AutoSize = true;
            this.lbl_amnttendered.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_amnttendered.Location = new System.Drawing.Point(16, 178);
            this.lbl_amnttendered.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_amnttendered.Name = "lbl_amnttendered";
            this.lbl_amnttendered.Size = new System.Drawing.Size(84, 16);
            this.lbl_amnttendered.TabIndex = 5;
            this.lbl_amnttendered.Text = "Amount Paid";
            // 
            // cbo_mop
            // 
            this.cbo_mop.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_mop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_mop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_mop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_mop.FormattingEnabled = true;
            this.cbo_mop.Location = new System.Drawing.Point(149, 72);
            this.cbo_mop.Name = "cbo_mop";
            this.cbo_mop.Size = new System.Drawing.Size(240, 24);
            this.cbo_mop.TabIndex = 9;
            this.cbo_mop.SelectedIndexChanged += new System.EventHandler(this.cbo_terms_SelectedIndexChanged);
            // 
            // txt_terms_ref
            // 
            this.txt_terms_ref.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_terms_ref.Location = new System.Drawing.Point(149, 132);
            this.txt_terms_ref.Name = "txt_terms_ref";
            this.txt_terms_ref.Size = new System.Drawing.Size(240, 26);
            this.txt_terms_ref.TabIndex = 11;
            // 
            // txt_discamt
            // 
            this.txt_discamt.BackColor = System.Drawing.SystemColors.Info;
            this.txt_discamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_discamt.Location = new System.Drawing.Point(149, 40);
            this.txt_discamt.Name = "txt_discamt";
            this.txt_discamt.ReadOnly = true;
            this.txt_discamt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_discamt.Size = new System.Drawing.Size(240, 26);
            this.txt_discamt.TabIndex = 8;
            this.txt_discamt.Text = "0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total Discount";
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.Peru;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_back.Image = global::Accounting_Application_System.Properties.Resources._1343907460_go_back;
            this.btn_back.Location = new System.Drawing.Point(68, 266);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(140, 50);
            this.btn_back.TabIndex = 21;
            this.btn_back.Text = "Back";
            this.btn_back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Peru;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_save.Image = global::Accounting_Application_System.Properties.Resources.play_1_;
            this.btn_save.Location = new System.Drawing.Point(217, 266);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(137, 50);
            this.btn_save.TabIndex = 20;
            this.btn_save.Text = "Submit (Enter)";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_amtdue
            // 
            this.txt_amtdue.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txt_amtdue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_amtdue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_amtdue.Location = new System.Drawing.Point(149, 12);
            this.txt_amtdue.Name = "txt_amtdue";
            this.txt_amtdue.ReadOnly = true;
            this.txt_amtdue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_amtdue.Size = new System.Drawing.Size(240, 22);
            this.txt_amtdue.TabIndex = 7;
            this.txt_amtdue.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Due Amount";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 142);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Reference";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Mode Of Payment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Charge To";
            // 
            // cbo_charge
            // 
            this.cbo_charge.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_charge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_charge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_charge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_charge.FormattingEnabled = true;
            this.cbo_charge.Location = new System.Drawing.Point(149, 102);
            this.cbo_charge.Name = "cbo_charge";
            this.cbo_charge.Size = new System.Drawing.Size(240, 24);
            this.cbo_charge.TabIndex = 10;
            this.cbo_charge.SelectedIndexChanged += new System.EventHandler(this.cbo_charge_SelectedIndexChanged);
            // 
            // btn_clr_dealer
            // 
            this.btn_clr_dealer.BackColor = System.Drawing.SystemColors.Info;
            this.btn_clr_dealer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_clr_dealer.Location = new System.Drawing.Point(392, 103);
            this.btn_clr_dealer.Name = "btn_clr_dealer";
            this.btn_clr_dealer.Size = new System.Drawing.Size(28, 23);
            this.btn_clr_dealer.TabIndex = 314;
            this.btn_clr_dealer.Text = "...";
            this.btn_clr_dealer.UseVisualStyleBackColor = false;
            this.btn_clr_dealer.Click += new System.EventHandler(this.btn_clr_dealer_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Info;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(392, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 23);
            this.button1.TabIndex = 315;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // z_payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(432, 327);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_clr_dealer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbo_charge);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_csh_change);
            this.Controls.Add(this.txt_terms_ref);
            this.Controls.Add(this.txt_csh_amttendered);
            this.Controls.Add(this.lbl_balance);
            this.Controls.Add(this.cbo_mop);
            this.Controls.Add(this.lbl_amnttendered);
            this.Controls.Add(this.txt_discamt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_amtdue);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "z_payment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.z_payment_FormClosing);
            this.Load += new System.EventHandler(this.z_payment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_csh_change;
        private System.Windows.Forms.TextBox txt_csh_amttendered;
        private System.Windows.Forms.Label lbl_balance;
        private System.Windows.Forms.Label lbl_amnttendered;
        private System.Windows.Forms.ComboBox cbo_mop;
        private System.Windows.Forms.TextBox txt_terms_ref;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.TextBox txt_discamt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_amtdue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_charge;
        private System.Windows.Forms.Button btn_clr_dealer;
        private System.Windows.Forms.Button button1;
    }
}