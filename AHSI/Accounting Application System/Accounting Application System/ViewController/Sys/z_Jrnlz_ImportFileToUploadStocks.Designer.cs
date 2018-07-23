namespace Accounting_Application_System
{
    partial class z_Jrnlz_ImportFileToUploadStocks
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_mainside = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Browse = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFileToImport = new System.Windows.Forms.TextBox();
            this.btn_import = new System.Windows.Forms.Button();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.pnl_pbar = new System.Windows.Forms.Panel();
            this.lbl_status = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_jrnldt_frm = new System.Windows.Forms.DateTimePicker();
            this.dtp_jrnldt_to = new System.Windows.Forms.DateTimePicker();
            this.bgworker = new System.ComponentModel.BackgroundWorker();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.dgvi_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_tra_frm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_tra_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvi_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_mainside.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Browse)).BeginInit();
            this.pnl_main.SuspendLayout();
            this.pnl_pbar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
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
            this.pnl_mainside.Size = new System.Drawing.Size(271, 742);
            this.pnl_mainside.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Browse);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtFileToImport);
            this.groupBox1.Controls.Add(this.btn_import);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 194);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import File Stock Transaction";
            // 
            // Browse
            // 
            this.Browse.BackColor = System.Drawing.Color.Transparent;
            this.Browse.Image = global::Accounting_Application_System.Properties.Resources.browse;
            this.Browse.Location = new System.Drawing.Point(94, 35);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(70, 27);
            this.Browse.TabIndex = 17;
            this.Browse.TabStop = false;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Select File";
            // 
            // txtFileToImport
            // 
            this.txtFileToImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileToImport.Location = new System.Drawing.Point(21, 62);
            this.txtFileToImport.Name = "txtFileToImport";
            this.txtFileToImport.ReadOnly = true;
            this.txtFileToImport.Size = new System.Drawing.Size(207, 22);
            this.txtFileToImport.TabIndex = 15;
            // 
            // btn_import
            // 
            this.btn_import.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_import.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_import.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_import.Image = global::Accounting_Application_System.Properties.Resources.play;
            this.btn_import.Location = new System.Drawing.Point(20, 122);
            this.btn_import.Margin = new System.Windows.Forms.Padding(2);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(207, 41);
            this.btn_import.TabIndex = 4;
            this.btn_import.Text = "Import File";
            this.btn_import.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_import.UseVisualStyleBackColor = false;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // pnl_main
            // 
            this.pnl_main.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_main.Controls.Add(this.pnl_pbar);
            this.pnl_main.Controls.Add(this.groupBox2);
            this.pnl_main.Controls.Add(this.dgv_list);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_main.Location = new System.Drawing.Point(271, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(827, 742);
            this.pnl_main.TabIndex = 14;
            // 
            // pnl_pbar
            // 
            this.pnl_pbar.Controls.Add(this.lbl_status);
            this.pnl_pbar.Controls.Add(this.progressBar1);
            this.pnl_pbar.Location = new System.Drawing.Point(281, 208);
            this.pnl_pbar.Name = "pnl_pbar";
            this.pnl_pbar.Size = new System.Drawing.Size(233, 107);
            this.pnl_pbar.TabIndex = 16;
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
            this.groupBox2.Text = "Date Imported";
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
            // bgworker
            // 
            this.bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworker_DoWork);
            // 
            // dgv_list
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_list.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvi_dt,
            this.dgvi_tra_frm,
            this.dgvi_tra_to,
            this.dgvi_user,
            this.dgvi_time});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_list.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_list.Location = new System.Drawing.Point(5, 77);
            this.dgv_list.Name = "dgv_list";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_list.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_list.RowHeadersVisible = false;
            this.dgv_list.Size = new System.Drawing.Size(748, 656);
            this.dgv_list.TabIndex = 17;
            // 
            // dgvi_dt
            // 
            this.dgvi_dt.HeaderText = "DATE EXPORTED";
            this.dgvi_dt.Name = "dgvi_dt";
            this.dgvi_dt.ReadOnly = true;
            this.dgvi_dt.Width = 150;
            // 
            // dgvi_tra_frm
            // 
            this.dgvi_tra_frm.HeaderText = "RECIEVING NUMBER FROM";
            this.dgvi_tra_frm.Name = "dgvi_tra_frm";
            this.dgvi_tra_frm.ReadOnly = true;
            this.dgvi_tra_frm.Width = 180;
            // 
            // dgvi_tra_to
            // 
            this.dgvi_tra_to.HeaderText = "RECIEVING NUMBER TO";
            this.dgvi_tra_to.Name = "dgvi_tra_to";
            this.dgvi_tra_to.ReadOnly = true;
            this.dgvi_tra_to.Width = 180;
            // 
            // dgvi_user
            // 
            this.dgvi_user.HeaderText = "USER ID";
            this.dgvi_user.Name = "dgvi_user";
            this.dgvi_user.ReadOnly = true;
            this.dgvi_user.Width = 150;
            // 
            // dgvi_time
            // 
            this.dgvi_time.HeaderText = "TIME";
            this.dgvi_time.Name = "dgvi_time";
            this.dgvi_time.ReadOnly = true;
            // 
            // z_Jrnlz_ImportFileToUploadStocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1036, 742);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.pnl_mainside);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "z_Jrnlz_ImportFileToUploadStocks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "z_Jrnlz_ImportFileToUploadStocks";
            this.Load += new System.EventHandler(this.z_Jrnlz_ImportFileToUploadStocks_Load);
            this.pnl_mainside.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Browse)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.PictureBox Browse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFileToImport;
        private System.ComponentModel.BackgroundWorker bgworker;
        private System.Windows.Forms.Panel pnl_pbar;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_tra_frm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_tra_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvi_time;
    }
}