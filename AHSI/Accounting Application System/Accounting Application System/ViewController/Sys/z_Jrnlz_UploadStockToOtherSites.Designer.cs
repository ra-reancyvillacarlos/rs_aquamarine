namespace Accounting_Application_System
{
    partial class z_Jrnlz_UploadStockToOtherSites
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
            this.pnl_main = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_jrnldt_frm = new System.Windows.Forms.DateTimePicker();
            this.dtp_jrnldt_to = new System.Windows.Forms.DateTimePicker();
            this.dgvi_list = new System.Windows.Forms.DataGridView();
            this.dgvi_dtfrm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_dtto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_jrnl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_print = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbo_rec_num_to = new System.Windows.Forms.ComboBox();
            this.cbo_rec_num_frm = new System.Windows.Forms.ComboBox();
            this.cbo_whs_to = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.btn_test = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_mainside.SuspendLayout();
            this.pnl_main.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvi_list)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_mainside
            // 
            this.pnl_mainside.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnl_mainside.Controls.Add(this.groupBox3);
            this.pnl_mainside.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_mainside.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_mainside.Location = new System.Drawing.Point(0, 0);
            this.pnl_mainside.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_mainside.Name = "pnl_mainside";
            this.pnl_mainside.Size = new System.Drawing.Size(271, 768);
            this.pnl_mainside.TabIndex = 13;
            // 
            // pnl_main
            // 
            this.pnl_main.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_main.Controls.Add(this.groupBox2);
            this.pnl_main.Controls.Add(this.dgvi_list);
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
            this.dgvi_print,
            this.dgvi_user,
            this.dgvi_date,
            this.dgvi_time});
            this.dgvi_list.Location = new System.Drawing.Point(0, 76);
            this.dgvi_list.Name = "dgvi_list";
            this.dgvi_list.RowHeadersVisible = false;
            this.dgvi_list.Size = new System.Drawing.Size(748, 656);
            this.dgvi_list.TabIndex = 0;
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
            // 
            // dgvi_print
            // 
            this.dgvi_print.HeaderText = "REPORT";
            this.dgvi_print.Name = "dgvi_print";
            this.dgvi_print.ReadOnly = true;
            // 
            // dgvi_user
            // 
            this.dgvi_user.HeaderText = "USER ID";
            this.dgvi_user.Name = "dgvi_user";
            this.dgvi_user.ReadOnly = true;
            // 
            // dgvi_date
            // 
            this.dgvi_date.HeaderText = "DATE JORNALIZED";
            this.dgvi_date.Name = "dgvi_date";
            this.dgvi_date.ReadOnly = true;
            this.dgvi_date.Width = 150;
            // 
            // dgvi_time
            // 
            this.dgvi_time.HeaderText = "TIME";
            this.dgvi_time.Name = "dgvi_time";
            this.dgvi_time.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btn_test);
            this.groupBox3.Controls.Add(this.txt_ip);
            this.groupBox3.Controls.Add(this.cbo_rec_num_to);
            this.groupBox3.Controls.Add(this.cbo_rec_num_frm);
            this.groupBox3.Controls.Add(this.cbo_whs_to);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btn_submit);
            this.groupBox3.Location = new System.Drawing.Point(12, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(247, 372);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export File Stock Transfer";
            // 
            // cbo_rec_num_to
            // 
            this.cbo_rec_num_to.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rec_num_to.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_rec_num_to.FormattingEnabled = true;
            this.cbo_rec_num_to.Location = new System.Drawing.Point(21, 262);
            this.cbo_rec_num_to.Name = "cbo_rec_num_to";
            this.cbo_rec_num_to.Size = new System.Drawing.Size(207, 24);
            this.cbo_rec_num_to.TabIndex = 18;
            // 
            // cbo_rec_num_frm
            // 
            this.cbo_rec_num_frm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rec_num_frm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_rec_num_frm.FormattingEnabled = true;
            this.cbo_rec_num_frm.Location = new System.Drawing.Point(21, 199);
            this.cbo_rec_num_frm.Name = "cbo_rec_num_frm";
            this.cbo_rec_num_frm.Size = new System.Drawing.Size(207, 24);
            this.cbo_rec_num_frm.TabIndex = 17;
            // 
            // cbo_whs_to
            // 
            this.cbo_whs_to.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_whs_to.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_whs_to.FormattingEnabled = true;
            this.cbo_whs_to.Location = new System.Drawing.Point(21, 136);
            this.cbo_whs_to.Name = "cbo_whs_to";
            this.cbo_whs_to.Size = new System.Drawing.Size(207, 24);
            this.cbo_whs_to.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Stock Transfer To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Target Warehouse";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Stock Transfer From";
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_submit.Image = global::Accounting_Application_System.Properties.Resources.play;
            this.btn_submit.Location = new System.Drawing.Point(21, 309);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(2);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(207, 41);
            this.btn_submit.TabIndex = 4;
            this.btn_submit.Text = "Export File";
            this.btn_submit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_submit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(21, 48);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(207, 22);
            this.txt_ip.TabIndex = 19;
            // 
            // btn_test
            // 
            this.btn_test.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_test.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_test.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_test.Location = new System.Drawing.Point(120, 75);
            this.btn_test.Margin = new System.Windows.Forms.Padding(2);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(108, 25);
            this.btn_test.TabIndex = 21;
            this.btn_test.Text = "Test Connection";
            this.btn_test.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_test.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_test.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Target IP Address";
            // 
            // z_Jrnlz_UploadStockToOtherSites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1036, 768);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.pnl_mainside);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "z_Jrnlz_UploadStockToOtherSites";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "z_Jrnlz_UploadStockToOtherSites";
            this.Load += new System.EventHandler(this.z_Jrnlz_UploadStockToOtherSites_Load);
            this.pnl_mainside.ResumeLayout(false);
            this.pnl_main.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvi_list)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_mainside;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_jrnldt_frm;
        private System.Windows.Forms.DateTimePicker dtp_jrnldt_to;
        private System.Windows.Forms.DataGridView dgvi_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_dtfrm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_dtto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_jrnl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_print;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_time;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.ComboBox cbo_rec_num_to;
        private System.Windows.Forms.ComboBox cbo_rec_num_frm;
        private System.Windows.Forms.ComboBox cbo_whs_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.Label label1;
    }
}