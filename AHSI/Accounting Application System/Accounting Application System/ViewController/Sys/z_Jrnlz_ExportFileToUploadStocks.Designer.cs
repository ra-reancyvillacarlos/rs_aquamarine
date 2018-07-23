namespace Accounting_Application_System
{
    partial class z_Jrnlz_ExportFileToUploadStocks
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbo_rec_num_to = new System.Windows.Forms.ComboBox();
            this.cbo_rec_num_frm = new System.Windows.Forms.ComboBox();
            this.cbo_warehouse = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.pnl_pbar = new System.Windows.Forms.Panel();
            this.lbl_status = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_jrnldt_frm = new System.Windows.Forms.DateTimePicker();
            this.dtp_jrnldt_to = new System.Windows.Forms.DateTimePicker();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.dgvi_whs_code_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_whs_name_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_tra_frm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_tra_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgworker = new System.ComponentModel.BackgroundWorker();
            this.pnl_mainside.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnl_main.SuspendLayout();
            this.pnl_pbar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
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
            this.pnl_mainside.Size = new System.Drawing.Size(271, 637);
            this.pnl_mainside.TabIndex = 12;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbo_rec_num_to);
            this.groupBox3.Controls.Add(this.cbo_rec_num_frm);
            this.groupBox3.Controls.Add(this.cbo_warehouse);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btn_submit);
            this.groupBox3.Location = new System.Drawing.Point(12, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(247, 295);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export File From Other Sites";
            // 
            // cbo_rec_num_to
            // 
            this.cbo_rec_num_to.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rec_num_to.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_rec_num_to.FormattingEnabled = true;
            this.cbo_rec_num_to.Location = new System.Drawing.Point(21, 170);
            this.cbo_rec_num_to.Name = "cbo_rec_num_to";
            this.cbo_rec_num_to.Size = new System.Drawing.Size(207, 24);
            this.cbo_rec_num_to.TabIndex = 18;
            // 
            // cbo_rec_num_frm
            // 
            this.cbo_rec_num_frm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rec_num_frm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_rec_num_frm.FormattingEnabled = true;
            this.cbo_rec_num_frm.Location = new System.Drawing.Point(21, 107);
            this.cbo_rec_num_frm.Name = "cbo_rec_num_frm";
            this.cbo_rec_num_frm.Size = new System.Drawing.Size(207, 24);
            this.cbo_rec_num_frm.TabIndex = 17;
            // 
            // cbo_warehouse
            // 
            this.cbo_warehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_warehouse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_warehouse.FormattingEnabled = true;
            this.cbo_warehouse.Location = new System.Drawing.Point(21, 44);
            this.cbo_warehouse.Name = "cbo_warehouse";
            this.cbo_warehouse.Size = new System.Drawing.Size(207, 24);
            this.cbo_warehouse.TabIndex = 16;
            this.cbo_warehouse.SelectedIndexChanged += new System.EventHandler(this.cbo_warehouse_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Stock Transfer To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Target Warehouse";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 88);
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
            this.btn_submit.Location = new System.Drawing.Point(21, 229);
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
            // pnl_main
            // 
            this.pnl_main.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_main.Controls.Add(this.pnl_pbar);
            this.pnl_main.Controls.Add(this.groupBox2);
            this.pnl_main.Controls.Add(this.dgv_list);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_main.Location = new System.Drawing.Point(271, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(827, 637);
            this.pnl_main.TabIndex = 16;
            // 
            // pnl_pbar
            // 
            this.pnl_pbar.Controls.Add(this.lbl_status);
            this.pnl_pbar.Controls.Add(this.progressBar1);
            this.pnl_pbar.Location = new System.Drawing.Point(297, 265);
            this.pnl_pbar.Name = "pnl_pbar";
            this.pnl_pbar.Size = new System.Drawing.Size(233, 107);
            this.pnl_pbar.TabIndex = 15;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lbl_status.Location = new System.Drawing.Point(64, 21);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(114, 16);
            this.lbl_status.TabIndex = 9;
            this.lbl_status.Text = "PROCESSING . . .";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 50);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(219, 23);
            this.progressBar1.TabIndex = 8;
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
            this.groupBox2.Text = "Date Exported";
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
            this.dtp_jrnldt_to.ValueChanged += new System.EventHandler(this.dtp_jrnldt_to_ValueChanged);
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvi_whs_code_to,
            this.dgvi_whs_name_to,
            this.dgvi_tra_frm,
            this.dgvi_tra_to,
            this.dgvi_user,
            this.dgvi_dt,
            this.dgvi_time});
            this.dgv_list.Location = new System.Drawing.Point(0, 76);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowHeadersVisible = false;
            this.dgv_list.Size = new System.Drawing.Size(748, 656);
            this.dgv_list.TabIndex = 0;
            // 
            // dgvi_whs_code_to
            // 
            this.dgvi_whs_code_to.FillWeight = 150F;
            this.dgvi_whs_code_to.HeaderText = "TARGET";
            this.dgvi_whs_code_to.Name = "dgvi_whs_code_to";
            this.dgvi_whs_code_to.ReadOnly = true;
            this.dgvi_whs_code_to.Width = 75;
            // 
            // dgvi_whs_name_to
            // 
            this.dgvi_whs_name_to.HeaderText = "TARGET WHOUSE NAME";
            this.dgvi_whs_name_to.Name = "dgvi_whs_name_to";
            this.dgvi_whs_name_to.Width = 180;
            // 
            // dgvi_tra_frm
            // 
            this.dgvi_tra_frm.HeaderText = "TRA FROM";
            this.dgvi_tra_frm.Name = "dgvi_tra_frm";
            this.dgvi_tra_frm.ReadOnly = true;
            // 
            // dgvi_tra_to
            // 
            this.dgvi_tra_to.HeaderText = "TRA TO";
            this.dgvi_tra_to.Name = "dgvi_tra_to";
            this.dgvi_tra_to.ReadOnly = true;
            // 
            // dgvi_user
            // 
            this.dgvi_user.HeaderText = "USER ID";
            this.dgvi_user.Name = "dgvi_user";
            this.dgvi_user.ReadOnly = true;
            // 
            // dgvi_dt
            // 
            this.dgvi_dt.HeaderText = "DATE";
            this.dgvi_dt.Name = "dgvi_dt";
            this.dgvi_dt.ReadOnly = true;
            // 
            // dgvi_time
            // 
            this.dgvi_time.HeaderText = "TIME";
            this.dgvi_time.Name = "dgvi_time";
            this.dgvi_time.ReadOnly = true;
            // 
            // bgworker
            // 
            this.bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworker_DoWork);
            // 
            // z_Jrnlz_ExportFileToUploadStocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1036, 637);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.pnl_mainside);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "z_Jrnlz_ExportFileToUploadStocks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "z_Jrnlz_ExportFileToUploadStocks";
            this.Load += new System.EventHandler(this.z_Jrnlz_ExportFileToUploadStocks_Load);
            this.pnl_mainside.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.pnl_main.ResumeLayout(false);
            this.pnl_pbar.ResumeLayout(false);
            this.pnl_pbar.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_mainside;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_jrnldt_frm;
        private System.Windows.Forms.DateTimePicker dtp_jrnldt_to;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.ComboBox cbo_rec_num_to;
        private System.Windows.Forms.ComboBox cbo_rec_num_frm;
        private System.Windows.Forms.ComboBox cbo_warehouse;
        private System.ComponentModel.BackgroundWorker bgworker;
        private System.Windows.Forms.Panel pnl_pbar;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_whs_code_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_whs_name_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_tra_frm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_tra_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_time;
    }
}