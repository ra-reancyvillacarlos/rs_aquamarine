namespace Accounting_Application_System
{
    partial class auto_status
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_app_no = new System.Windows.Forms.TextBox();
            this.txt_custno = new System.Windows.Forms.TextBox();
            this.txt_custname = new System.Windows.Forms.TextBox();
            this.cbo_decision = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txt_finance_code = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_financer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_srp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_dp_amt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_amt_finance = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtp_date_credit = new System.Windows.Forms.DateTimePicker();
            this.txt_date_credit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Auto Loan No.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(46, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Customer No.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Customer Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(47, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Status*";
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.SystemColors.Info;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_close.Location = new System.Drawing.Point(263, 331);
            this.btn_close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(96, 42);
            this.btn_close.TabIndex = 79;
            this.btn_close.Text = "Close";
            this.btn_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.SystemColors.Info;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btn_save.Location = new System.Drawing.Point(378, 331);
            this.btn_save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(96, 42);
            this.btn_save.TabIndex = 80;
            this.btn_save.Text = "Save";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_app_no
            // 
            this.txt_app_no.BackColor = System.Drawing.SystemColors.Info;
            this.txt_app_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_app_no.Location = new System.Drawing.Point(160, 54);
            this.txt_app_no.Name = "txt_app_no";
            this.txt_app_no.ReadOnly = true;
            this.txt_app_no.Size = new System.Drawing.Size(170, 21);
            this.txt_app_no.TabIndex = 81;
            // 
            // txt_custno
            // 
            this.txt_custno.BackColor = System.Drawing.SystemColors.Info;
            this.txt_custno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_custno.Location = new System.Drawing.Point(160, 81);
            this.txt_custno.Name = "txt_custno";
            this.txt_custno.ReadOnly = true;
            this.txt_custno.Size = new System.Drawing.Size(170, 21);
            this.txt_custno.TabIndex = 82;
            // 
            // txt_custname
            // 
            this.txt_custname.BackColor = System.Drawing.SystemColors.Info;
            this.txt_custname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_custname.Location = new System.Drawing.Point(160, 108);
            this.txt_custname.Name = "txt_custname";
            this.txt_custname.ReadOnly = true;
            this.txt_custname.Size = new System.Drawing.Size(278, 21);
            this.txt_custname.TabIndex = 83;
            // 
            // cbo_decision
            // 
            this.cbo_decision.BackColor = System.Drawing.SystemColors.Info;
            this.cbo_decision.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbo_decision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_decision.FormattingEnabled = true;
            this.cbo_decision.Location = new System.Drawing.Point(161, 164);
            this.cbo_decision.Name = "cbo_decision";
            this.cbo_decision.Size = new System.Drawing.Size(169, 23);
            this.cbo_decision.TabIndex = 84;
            this.cbo_decision.SelectedIndexChanged += new System.EventHandler(this.cbo_decision_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(215, 383);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 15);
            this.label6.TabIndex = 87;
            // 
            // txt_finance_code
            // 
            this.txt_finance_code.BackColor = System.Drawing.SystemColors.Info;
            this.txt_finance_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_finance_code.Location = new System.Drawing.Point(160, 27);
            this.txt_finance_code.Name = "txt_finance_code";
            this.txt_finance_code.ReadOnly = true;
            this.txt_finance_code.Size = new System.Drawing.Size(170, 21);
            this.txt_finance_code.TabIndex = 89;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(46, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 88;
            this.label5.Text = "Finance Code";
            // 
            // txt_financer
            // 
            this.txt_financer.BackColor = System.Drawing.SystemColors.Info;
            this.txt_financer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_financer.Location = new System.Drawing.Point(160, 135);
            this.txt_financer.Name = "txt_financer";
            this.txt_financer.ReadOnly = true;
            this.txt_financer.Size = new System.Drawing.Size(277, 21);
            this.txt_financer.TabIndex = 91;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(46, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 90;
            this.label7.Text = "Financer";
            // 
            // txt_srp
            // 
            this.txt_srp.BackColor = System.Drawing.SystemColors.Info;
            this.txt_srp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_srp.Location = new System.Drawing.Point(160, 226);
            this.txt_srp.Name = "txt_srp";
            this.txt_srp.ReadOnly = true;
            this.txt_srp.Size = new System.Drawing.Size(169, 21);
            this.txt_srp.TabIndex = 93;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(46, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 15);
            this.label8.TabIndex = 92;
            this.label8.Text = "SRP";
            // 
            // txt_dp_amt
            // 
            this.txt_dp_amt.BackColor = System.Drawing.SystemColors.Info;
            this.txt_dp_amt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_dp_amt.Location = new System.Drawing.Point(160, 253);
            this.txt_dp_amt.Name = "txt_dp_amt";
            this.txt_dp_amt.ReadOnly = true;
            this.txt_dp_amt.Size = new System.Drawing.Size(169, 21);
            this.txt_dp_amt.TabIndex = 95;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(46, 256);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 15);
            this.label9.TabIndex = 94;
            this.label9.Text = "DP Amount";
            // 
            // txt_amt_finance
            // 
            this.txt_amt_finance.BackColor = System.Drawing.SystemColors.Info;
            this.txt_amt_finance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_amt_finance.Location = new System.Drawing.Point(160, 280);
            this.txt_amt_finance.Name = "txt_amt_finance";
            this.txt_amt_finance.ReadOnly = true;
            this.txt_amt_finance.Size = new System.Drawing.Size(169, 21);
            this.txt_amt_finance.TabIndex = 97;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(46, 283);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 15);
            this.label10.TabIndex = 96;
            this.label10.Text = "Amount Finance";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(45, 197);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 15);
            this.label12.TabIndex = 99;
            this.label12.Text = "Date Credit Advice";
            // 
            // dtp_date_credit
            // 
            this.dtp_date_credit.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date_credit.Location = new System.Drawing.Point(160, 195);
            this.dtp_date_credit.Name = "dtp_date_credit";
            this.dtp_date_credit.Size = new System.Drawing.Size(170, 20);
            this.dtp_date_credit.TabIndex = 100;
            this.dtp_date_credit.ValueChanged += new System.EventHandler(this.dtp_date_credit_ValueChanged);
            // 
            // txt_date_credit
            // 
            this.txt_date_credit.BackColor = System.Drawing.SystemColors.Info;
            this.txt_date_credit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_date_credit.Location = new System.Drawing.Point(160, 195);
            this.txt_date_credit.Name = "txt_date_credit";
            this.txt_date_credit.ReadOnly = true;
            this.txt_date_credit.Size = new System.Drawing.Size(170, 21);
            this.txt_date_credit.TabIndex = 101;
            // 
            // auto_status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(502, 399);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txt_amt_finance);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_dp_amt);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_srp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_financer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_finance_code);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbo_decision);
            this.Controls.Add(this.txt_custname);
            this.Controls.Add(this.txt_custno);
            this.Controls.Add(this.txt_app_no);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_date_credit);
            this.Controls.Add(this.dtp_date_credit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "auto_status";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "auto_status";
            this.Load += new System.EventHandler(this.auto_status_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_app_no;
        private System.Windows.Forms.TextBox txt_custno;
        private System.Windows.Forms.TextBox txt_custname;
        private System.Windows.Forms.ComboBox cbo_decision;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_finance_code;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_financer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_srp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_dp_amt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_amt_finance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtp_date_credit;
        private System.Windows.Forms.TextBox txt_date_credit;
    }
}