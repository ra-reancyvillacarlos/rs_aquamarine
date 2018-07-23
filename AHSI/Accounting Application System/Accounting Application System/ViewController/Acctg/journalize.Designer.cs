namespace Accounting_Application_System
{
    partial class journalize
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_period = new System.Windows.Forms.ComboBox();
            this.lbl_typ = new System.Windows.Forms.Label();
            this.cbo_typ = new System.Windows.Forms.ComboBox();
            this.lbl_inv = new System.Windows.Forms.Label();
            this.lbl_inv_to = new System.Windows.Forms.Label();
            this.cbo_inv_to = new System.Windows.Forms.ComboBox();
            this.cbo_inv_frm = new System.Windows.Forms.ComboBox();
            this.lbl_dt_to = new System.Windows.Forms.Label();
            this.lbl_dt = new System.Windows.Forms.Label();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.dtp_frm = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_journal = new System.Windows.Forms.ComboBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_proceed = new System.Windows.Forms.Button();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbo_period);
            this.groupBox1.Controls.Add(this.lbl_typ);
            this.groupBox1.Controls.Add(this.cbo_typ);
            this.groupBox1.Controls.Add(this.lbl_inv);
            this.groupBox1.Controls.Add(this.lbl_inv_to);
            this.groupBox1.Controls.Add(this.cbo_inv_to);
            this.groupBox1.Controls.Add(this.cbo_inv_frm);
            this.groupBox1.Controls.Add(this.lbl_dt_to);
            this.groupBox1.Controls.Add(this.lbl_dt);
            this.groupBox1.Controls.Add(this.dtp_to);
            this.groupBox1.Controls.Add(this.dtp_frm);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbo_journal);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 175);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Journalize Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Period";
            // 
            // cbo_period
            // 
            this.cbo_period.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_period.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_period.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_period.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_period.FormattingEnabled = true;
            this.cbo_period.Location = new System.Drawing.Point(133, 138);
            this.cbo_period.Name = "cbo_period";
            this.cbo_period.Size = new System.Drawing.Size(317, 24);
            this.cbo_period.TabIndex = 19;
            // 
            // lbl_typ
            // 
            this.lbl_typ.AutoSize = true;
            this.lbl_typ.Location = new System.Drawing.Point(10, 25);
            this.lbl_typ.Name = "lbl_typ";
            this.lbl_typ.Size = new System.Drawing.Size(33, 15);
            this.lbl_typ.TabIndex = 18;
            this.lbl_typ.Text = "Type";
            // 
            // cbo_typ
            // 
            this.cbo_typ.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_typ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_typ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_typ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_typ.FormattingEnabled = true;
            this.cbo_typ.Location = new System.Drawing.Point(133, 22);
            this.cbo_typ.Name = "cbo_typ";
            this.cbo_typ.Size = new System.Drawing.Size(317, 24);
            this.cbo_typ.TabIndex = 17;
            this.cbo_typ.SelectedIndexChanged += new System.EventHandler(this.cbo_typ_SelectedIndexChanged);
            // 
            // lbl_inv
            // 
            this.lbl_inv.AutoSize = true;
            this.lbl_inv.Location = new System.Drawing.Point(10, 55);
            this.lbl_inv.Name = "lbl_inv";
            this.lbl_inv.Size = new System.Drawing.Size(51, 15);
            this.lbl_inv.TabIndex = 16;
            this.lbl_inv.Text = "Invoices";
            // 
            // lbl_inv_to
            // 
            this.lbl_inv_to.AutoSize = true;
            this.lbl_inv_to.Location = new System.Drawing.Point(281, 55);
            this.lbl_inv_to.Name = "lbl_inv_to";
            this.lbl_inv_to.Size = new System.Drawing.Size(21, 15);
            this.lbl_inv_to.TabIndex = 15;
            this.lbl_inv_to.Text = "To";
            // 
            // cbo_inv_to
            // 
            this.cbo_inv_to.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_inv_to.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_inv_to.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_inv_to.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_inv_to.FormattingEnabled = true;
            this.cbo_inv_to.Location = new System.Drawing.Point(308, 52);
            this.cbo_inv_to.Name = "cbo_inv_to";
            this.cbo_inv_to.Size = new System.Drawing.Size(142, 24);
            this.cbo_inv_to.TabIndex = 14;
            // 
            // cbo_inv_frm
            // 
            this.cbo_inv_frm.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_inv_frm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_inv_frm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_inv_frm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_inv_frm.FormattingEnabled = true;
            this.cbo_inv_frm.Location = new System.Drawing.Point(133, 52);
            this.cbo_inv_frm.Name = "cbo_inv_frm";
            this.cbo_inv_frm.Size = new System.Drawing.Size(142, 24);
            this.cbo_inv_frm.TabIndex = 13;
            this.cbo_inv_frm.SelectedIndexChanged += new System.EventHandler(this.cbo_inv_frm_SelectedIndexChanged);
            // 
            // lbl_dt_to
            // 
            this.lbl_dt_to.AutoSize = true;
            this.lbl_dt_to.Location = new System.Drawing.Point(254, 85);
            this.lbl_dt_to.Name = "lbl_dt_to";
            this.lbl_dt_to.Size = new System.Drawing.Size(21, 15);
            this.lbl_dt_to.TabIndex = 12;
            this.lbl_dt_to.Text = "To";
            // 
            // lbl_dt
            // 
            this.lbl_dt.AutoSize = true;
            this.lbl_dt.Location = new System.Drawing.Point(10, 82);
            this.lbl_dt.Name = "lbl_dt";
            this.lbl_dt.Size = new System.Drawing.Size(106, 15);
            this.lbl_dt.TabIndex = 11;
            this.lbl_dt.Text = "Transaction Dates";
            // 
            // dtp_to
            // 
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(292, 82);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(106, 21);
            this.dtp_to.TabIndex = 10;
            this.dtp_to.ValueChanged += new System.EventHandler(this.dtp_to_ValueChanged);
            // 
            // dtp_frm
            // 
            this.dtp_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_frm.Location = new System.Drawing.Point(133, 82);
            this.dtp_frm.Name = "dtp_frm";
            this.dtp_frm.Size = new System.Drawing.Size(106, 21);
            this.dtp_frm.TabIndex = 9;
            this.dtp_frm.ValueChanged += new System.EventHandler(this.dtp_frm_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Journal Entry";
            // 
            // cbo_journal
            // 
            this.cbo_journal.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_journal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_journal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_journal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_journal.FormattingEnabled = true;
            this.cbo_journal.Location = new System.Drawing.Point(133, 109);
            this.cbo_journal.Name = "cbo_journal";
            this.cbo_journal.Size = new System.Drawing.Size(317, 24);
            this.cbo_journal.TabIndex = 7;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Peru;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_close.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_close.Location = new System.Drawing.Point(157, 188);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(80, 54);
            this.btn_close.TabIndex = 6;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_proceed
            // 
            this.btn_proceed.BackColor = System.Drawing.Color.Peru;
            this.btn_proceed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_proceed.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_proceed.Location = new System.Drawing.Point(243, 188);
            this.btn_proceed.Name = "btn_proceed";
            this.btn_proceed.Size = new System.Drawing.Size(80, 54);
            this.btn_proceed.TabIndex = 7;
            this.btn_proceed.Text = "Proceed";
            this.btn_proceed.UseVisualStyleBackColor = false;
            this.btn_proceed.Click += new System.EventHandler(this.btn_proceed_Click);
            // 
            // pbar
            // 
            this.pbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbar.Location = new System.Drawing.Point(0, 252);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(488, 23);
            this.pbar.TabIndex = 8;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // journalize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(488, 275);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.btn_proceed);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "journalize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Journalize Transactions";
            this.Load += new System.EventHandler(this.j_hotel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_dt_to;
        private System.Windows.Forms.Label lbl_dt;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.DateTimePicker dtp_frm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_journal;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_proceed;
        private System.Windows.Forms.ProgressBar pbar;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label lbl_inv;
        private System.Windows.Forms.Label lbl_inv_to;
        private System.Windows.Forms.ComboBox cbo_inv_to;
        private System.Windows.Forms.ComboBox cbo_inv_frm;
        private System.Windows.Forms.Label lbl_typ;
        private System.Windows.Forms.ComboBox cbo_typ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_period;

    }
}