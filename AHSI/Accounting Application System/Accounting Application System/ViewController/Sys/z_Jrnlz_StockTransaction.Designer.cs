namespace Accounting_Application_System
{
    partial class z_Jrnlz_StockTransaction
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
            this.pnl_mainside = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_rpt = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_jrnl_entry = new System.Windows.Forms.ComboBox();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_frm = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_journalize = new System.Windows.Forms.Button();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_jrnldt_frm = new System.Windows.Forms.DateTimePicker();
            this.dtp_jrnldt_to = new System.Windows.Forms.DateTimePicker();
            this.dgvi_list = new System.Windows.Forms.DataGridView();
            this.dgvi_dtfrm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_dtto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_jrnl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_j_num_start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_j_num_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_noofjrnl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_print = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_mainside.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnl_main.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvi_list)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_mainside
            // 
            this.pnl_mainside.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnl_mainside.Controls.Add(this.groupBox1);
            this.pnl_mainside.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_mainside.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_mainside.Location = new System.Drawing.Point(0, 0);
            this.pnl_mainside.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_mainside.Name = "pnl_mainside";
            this.pnl_mainside.Size = new System.Drawing.Size(271, 768);
            this.pnl_mainside.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_rpt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbo_jrnl_entry);
            this.groupBox1.Controls.Add(this.dtp_to);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtp_frm);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_journalize);
            this.groupBox1.Location = new System.Drawing.Point(11, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 272);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Journalize Sales Invoices";
            // 
            // chk_rpt
            // 
            this.chk_rpt.AutoSize = true;
            this.chk_rpt.Location = new System.Drawing.Point(21, 172);
            this.chk_rpt.Name = "chk_rpt";
            this.chk_rpt.Size = new System.Drawing.Size(114, 20);
            this.chk_rpt.TabIndex = 11;
            this.chk_rpt.Text = "Include Report";
            this.chk_rpt.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Journal Entry";
            // 
            // cbo_jrnl_entry
            // 
            this.cbo_jrnl_entry.FormattingEnabled = true;
            this.cbo_jrnl_entry.Location = new System.Drawing.Point(21, 128);
            this.cbo_jrnl_entry.Name = "cbo_jrnl_entry";
            this.cbo_jrnl_entry.Size = new System.Drawing.Size(207, 24);
            this.cbo_jrnl_entry.TabIndex = 9;
            // 
            // dtp_to
            // 
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(125, 61);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(103, 22);
            this.dtp_to.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Date From";
            // 
            // dtp_frm
            // 
            this.dtp_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_frm.Location = new System.Drawing.Point(125, 25);
            this.dtp_frm.Name = "dtp_frm";
            this.dtp_frm.Size = new System.Drawing.Size(103, 22);
            this.dtp_frm.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Date From";
            // 
            // btn_journalize
            // 
            this.btn_journalize.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_journalize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_journalize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_journalize.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_journalize.Image = global::Accounting_Application_System.Properties.Resources.play;
            this.btn_journalize.Location = new System.Drawing.Point(21, 214);
            this.btn_journalize.Margin = new System.Windows.Forms.Padding(2);
            this.btn_journalize.Name = "btn_journalize";
            this.btn_journalize.Size = new System.Drawing.Size(207, 41);
            this.btn_journalize.TabIndex = 4;
            this.btn_journalize.Text = " Journalize";
            this.btn_journalize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_journalize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_journalize.UseVisualStyleBackColor = false;
            // 
            // pnl_main
            // 
            this.pnl_main.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_main.Controls.Add(this.dgvi_list);
            this.pnl_main.Controls.Add(this.groupBox2);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_main.Location = new System.Drawing.Point(271, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(827, 768);
            this.pnl_main.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dtp_jrnldt_frm);
            this.groupBox2.Controls.Add(this.dtp_jrnldt_to);
            this.groupBox2.Location = new System.Drawing.Point(5, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(743, 55);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Journalized Dates";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(370, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "To";
            // 
            // dtp_jrnldt_frm
            // 
            this.dtp_jrnldt_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_jrnldt_frm.Location = new System.Drawing.Point(261, 19);
            this.dtp_jrnldt_frm.Name = "dtp_jrnldt_frm";
            this.dtp_jrnldt_frm.Size = new System.Drawing.Size(103, 20);
            this.dtp_jrnldt_frm.TabIndex = 12;
            // 
            // dtp_jrnldt_to
            // 
            this.dtp_jrnldt_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_jrnldt_to.Location = new System.Drawing.Point(406, 19);
            this.dtp_jrnldt_to.Name = "dtp_jrnldt_to";
            this.dtp_jrnldt_to.Size = new System.Drawing.Size(103, 20);
            this.dtp_jrnldt_to.TabIndex = 13;
            // 
            // dgvi_list
            // 
            this.dgvi_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvi_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvi_dtfrm,
            this.dgvi_dtto,
            this.dgvi_jrnl,
            this.dgvi_j_num_start,
            this.dgvi_j_num_to,
            this.dgvi_noofjrnl,
            this.dgvi_print,
            this.dgvi_user,
            this.dgvi_date,
            this.dgvi_time});
            this.dgvi_list.Location = new System.Drawing.Point(0, 76);
            this.dgvi_list.Name = "dgvi_list";
            this.dgvi_list.RowHeadersVisible = false;
            this.dgvi_list.Size = new System.Drawing.Size(748, 656);
            this.dgvi_list.TabIndex = 16;
            // 
            // dgvi_dtfrm
            // 
            this.dgvi_dtfrm.HeaderText = "DATE FROM";
            this.dgvi_dtfrm.Name = "dgvi_dtfrm";
            this.dgvi_dtfrm.ReadOnly = true;
            // 
            // dgvi_dtto
            // 
            this.dgvi_dtto.HeaderText = "DATE TO";
            this.dgvi_dtto.Name = "dgvi_dtto";
            this.dgvi_dtto.ReadOnly = true;
            // 
            // dgvi_jrnl
            // 
            this.dgvi_jrnl.HeaderText = "JOURNAL";
            this.dgvi_jrnl.Name = "dgvi_jrnl";
            this.dgvi_jrnl.ReadOnly = true;
            this.dgvi_jrnl.Width = 70;
            // 
            // dgvi_j_num_start
            // 
            this.dgvi_j_num_start.HeaderText = "JRNL NO FROM";
            this.dgvi_j_num_start.Name = "dgvi_j_num_start";
            this.dgvi_j_num_start.ReadOnly = true;
            this.dgvi_j_num_start.Width = 110;
            // 
            // dgvi_j_num_to
            // 
            this.dgvi_j_num_to.HeaderText = "JRNL NO TO";
            this.dgvi_j_num_to.Name = "dgvi_j_num_to";
            this.dgvi_j_num_to.ReadOnly = true;
            // 
            // dgvi_noofjrnl
            // 
            this.dgvi_noofjrnl.HeaderText = "NO OF JRNL";
            this.dgvi_noofjrnl.Name = "dgvi_noofjrnl";
            this.dgvi_noofjrnl.ReadOnly = true;
            // 
            // dgvi_print
            // 
            this.dgvi_print.HeaderText = "REPORT";
            this.dgvi_print.Name = "dgvi_print";
            this.dgvi_print.ReadOnly = true;
            this.dgvi_print.Width = 80;
            // 
            // dgvi_user
            // 
            this.dgvi_user.HeaderText = "USER ID";
            this.dgvi_user.Name = "dgvi_user";
            this.dgvi_user.ReadOnly = true;
            // 
            // dgvi_date
            // 
            this.dgvi_date.HeaderText = "DATE JRNLZ";
            this.dgvi_date.Name = "dgvi_date";
            this.dgvi_date.ReadOnly = true;
            // 
            // dgvi_time
            // 
            this.dgvi_time.HeaderText = "TIME";
            this.dgvi_time.Name = "dgvi_time";
            this.dgvi_time.ReadOnly = true;
            // 
            // z_Jrnlz_StockTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1036, 768);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.pnl_mainside);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "z_Jrnlz_StockTransaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "z_Jrnlz_StockTransaction";
            this.pnl_mainside.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnl_main.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvi_list)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_mainside;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk_rpt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_jrnl_entry;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_frm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_journalize;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_jrnldt_frm;
        private System.Windows.Forms.DateTimePicker dtp_jrnldt_to;
        private System.Windows.Forms.DataGridView dgvi_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_dtfrm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_dtto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_jrnl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_j_num_start;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_j_num_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_noofjrnl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_print;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_time;
    }
}