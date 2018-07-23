namespace Accounting_Application_System
{
    partial class Remarks
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtxt_remark = new System.Windows.Forms.RichTextBox();
            this.rtxt_tech_notes = new System.Windows.Forms.RichTextBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbo_clerk = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbo_drivetest = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cbo_approvedby = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbo_checkedby = new System.Windows.Forms.ComboBox();
            this.dtp_promisetime = new System.Windows.Forms.DateTimePicker();
            this.dtp_promise_date = new System.Windows.Forms.DateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "RO Remark";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Technician Notes";
            // 
            // rtxt_remark
            // 
            this.rtxt_remark.BackColor = System.Drawing.SystemColors.Info;
            this.rtxt_remark.Location = new System.Drawing.Point(50, 118);
            this.rtxt_remark.Name = "rtxt_remark";
            this.rtxt_remark.ReadOnly = true;
            this.rtxt_remark.Size = new System.Drawing.Size(331, 204);
            this.rtxt_remark.TabIndex = 2;
            this.rtxt_remark.Text = "";
            // 
            // rtxt_tech_notes
            // 
            this.rtxt_tech_notes.BackColor = System.Drawing.SystemColors.Info;
            this.rtxt_tech_notes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxt_tech_notes.Location = new System.Drawing.Point(32, 172);
            this.rtxt_tech_notes.Name = "rtxt_tech_notes";
            this.rtxt_tech_notes.ReadOnly = true;
            this.rtxt_tech_notes.Size = new System.Drawing.Size(379, 150);
            this.rtxt_tech_notes.TabIndex = 3;
            this.rtxt_tech_notes.Text = "";
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.Maroon;
            this.btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_exit.Image = global::Accounting_Application_System.Properties.Resources._1343892237_DeleteRed;
            this.btn_exit.Location = new System.Drawing.Point(386, 387);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(151, 47);
            this.btn_exit.TabIndex = 4;
            this.btn_exit.Text = "Close";
            this.btn_exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbo_clerk);
            this.groupBox1.Controls.Add(this.label43);
            this.groupBox1.Controls.Add(this.rtxt_remark);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(32, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 353);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RO Details";
            // 
            // cbo_clerk
            // 
            this.cbo_clerk.AllowDrop = true;
            this.cbo_clerk.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_clerk.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_clerk.BackColor = System.Drawing.SystemColors.Info;
            this.cbo_clerk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbo_clerk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_clerk.FormattingEnabled = true;
            this.cbo_clerk.Location = new System.Drawing.Point(102, 50);
            this.cbo_clerk.Name = "cbo_clerk";
            this.cbo_clerk.Size = new System.Drawing.Size(264, 26);
            this.cbo_clerk.TabIndex = 9;
            this.cbo_clerk.SelectedIndexChanged += new System.EventHandler(this.cbo_clerk_SelectedIndexChanged);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label43.Location = new System.Drawing.Point(7, 53);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(89, 15);
            this.label43.TabIndex = 8;
            this.label43.Text = "Service Advisor";
            this.label43.Click += new System.EventHandler(this.label43_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbo_drivetest);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.cbo_approvedby);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.cbo_checkedby);
            this.groupBox2.Controls.Add(this.dtp_promisetime);
            this.groupBox2.Controls.Add(this.dtp_promise_date);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.rtxt_tech_notes);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(476, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(433, 353);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Maintenance Details";
            // 
            // cbo_drivetest
            // 
            this.cbo_drivetest.BackColor = System.Drawing.SystemColors.Info;
            this.cbo_drivetest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbo_drivetest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_drivetest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_drivetest.FormattingEnabled = true;
            this.cbo_drivetest.Location = new System.Drawing.Point(114, 110);
            this.cbo_drivetest.Name = "cbo_drivetest";
            this.cbo_drivetest.Size = new System.Drawing.Size(277, 23);
            this.cbo_drivetest.TabIndex = 175;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(17, 118);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 15);
            this.label14.TabIndex = 176;
            this.label14.Text = "Drive Tested By";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(17, 61);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 15);
            this.label22.TabIndex = 174;
            this.label22.Text = "Checked By";
            // 
            // cbo_approvedby
            // 
            this.cbo_approvedby.BackColor = System.Drawing.SystemColors.Info;
            this.cbo_approvedby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbo_approvedby.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_approvedby.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_approvedby.FormattingEnabled = true;
            this.cbo_approvedby.Location = new System.Drawing.Point(114, 81);
            this.cbo_approvedby.Name = "cbo_approvedby";
            this.cbo_approvedby.Size = new System.Drawing.Size(277, 23);
            this.cbo_approvedby.TabIndex = 172;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(17, 89);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(93, 15);
            this.label20.TabIndex = 173;
            this.label20.Text = "QA Approved By";
            // 
            // cbo_checkedby
            // 
            this.cbo_checkedby.BackColor = System.Drawing.SystemColors.Info;
            this.cbo_checkedby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbo_checkedby.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_checkedby.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_checkedby.FormattingEnabled = true;
            this.cbo_checkedby.Location = new System.Drawing.Point(114, 53);
            this.cbo_checkedby.Name = "cbo_checkedby";
            this.cbo_checkedby.Size = new System.Drawing.Size(277, 23);
            this.cbo_checkedby.TabIndex = 171;
            // 
            // dtp_promisetime
            // 
            this.dtp_promisetime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_promisetime.Location = new System.Drawing.Point(246, 25);
            this.dtp_promisetime.Name = "dtp_promisetime";
            this.dtp_promisetime.ShowUpDown = true;
            this.dtp_promisetime.Size = new System.Drawing.Size(107, 22);
            this.dtp_promisetime.TabIndex = 170;
            // 
            // dtp_promise_date
            // 
            this.dtp_promise_date.CustomFormat = "yyyy-MM-dd";
            this.dtp_promise_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_promise_date.Location = new System.Drawing.Point(135, 25);
            this.dtp_promise_date.Name = "dtp_promise_date";
            this.dtp_promise_date.Size = new System.Drawing.Size(102, 22);
            this.dtp_promise_date.TabIndex = 168;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(6, 28);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(120, 15);
            this.label38.TabIndex = 169;
            this.label38.Text = "Date/Time Promised";
            // 
            // Remarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(938, 441);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_exit);
            this.Name = "Remarks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remarks";
            this.Load += new System.EventHandler(this.Remarks_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtxt_remark;
        private System.Windows.Forms.RichTextBox rtxt_tech_notes;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtp_promisetime;
        private System.Windows.Forms.DateTimePicker dtp_promise_date;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox cbo_drivetest;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cbo_approvedby;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbo_checkedby;
        private System.Windows.Forms.ComboBox cbo_clerk;
        private System.Windows.Forms.Label label43;
    }
}