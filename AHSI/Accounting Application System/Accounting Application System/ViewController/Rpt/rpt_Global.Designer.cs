namespace Accounting_Application_System
{
    partial class rpt_Global
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_clear = new System.Windows.Forms.Button();
            this.grp_options = new System.Windows.Forms.GroupBox();
            this.cbo_3 = new System.Windows.Forms.ComboBox();
            this.lbl_cbo_3 = new System.Windows.Forms.Label();
            this.cbo_2 = new System.Windows.Forms.ComboBox();
            this.lbl_cbo_2 = new System.Windows.Forms.Label();
            this.chk_3 = new System.Windows.Forms.CheckBox();
            this.chk_2 = new System.Windows.Forms.CheckBox();
            this.chk_1 = new System.Windows.Forms.CheckBox();
            this.cbo_1 = new System.Windows.Forms.ComboBox();
            this.lbl_cbo_1 = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.grp_bydate = new System.Windows.Forms.GroupBox();
            this.lbl_dt_to = new System.Windows.Forms.Label();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.lbl_dt_frm = new System.Windows.Forms.Label();
            this.dtp_frm = new System.Windows.Forms.DateTimePicker();
            this.crptviewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.pnl_pbar = new System.Windows.Forms.Panel();
            this.lbl_status = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bgworker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.grp_options.SuspendLayout();
            this.grp_bydate.SuspendLayout();
            this.pnl_pbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.grp_options);
            this.panel1.Controls.Add(this.btn_submit);
            this.panel1.Controls.Add(this.grp_bydate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 515);
            this.panel1.TabIndex = 2;
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.Peru;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_clear.Location = new System.Drawing.Point(7, 24);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(107, 37);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "Clear";
            this.btn_clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // grp_options
            // 
            this.grp_options.Controls.Add(this.cbo_3);
            this.grp_options.Controls.Add(this.lbl_cbo_3);
            this.grp_options.Controls.Add(this.cbo_2);
            this.grp_options.Controls.Add(this.lbl_cbo_2);
            this.grp_options.Controls.Add(this.chk_3);
            this.grp_options.Controls.Add(this.chk_2);
            this.grp_options.Controls.Add(this.chk_1);
            this.grp_options.Controls.Add(this.cbo_1);
            this.grp_options.Controls.Add(this.lbl_cbo_1);
            this.grp_options.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grp_options.Location = new System.Drawing.Point(7, 177);
            this.grp_options.Name = "grp_options";
            this.grp_options.Size = new System.Drawing.Size(220, 224);
            this.grp_options.TabIndex = 4;
            this.grp_options.TabStop = false;
            this.grp_options.Text = "Other Options";
            // 
            // cbo_3
            // 
            this.cbo_3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_3.FormattingEnabled = true;
            this.cbo_3.Location = new System.Drawing.Point(9, 148);
            this.cbo_3.Name = "cbo_3";
            this.cbo_3.Size = new System.Drawing.Size(200, 24);
            this.cbo_3.TabIndex = 8;
            // 
            // lbl_cbo_3
            // 
            this.lbl_cbo_3.AutoSize = true;
            this.lbl_cbo_3.Location = new System.Drawing.Point(6, 129);
            this.lbl_cbo_3.Name = "lbl_cbo_3";
            this.lbl_cbo_3.Size = new System.Drawing.Size(53, 16);
            this.lbl_cbo_3.TabIndex = 7;
            this.lbl_cbo_3.Text = "User ID";
            // 
            // cbo_2
            // 
            this.cbo_2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_2.FormattingEnabled = true;
            this.cbo_2.Location = new System.Drawing.Point(9, 97);
            this.cbo_2.Name = "cbo_2";
            this.cbo_2.Size = new System.Drawing.Size(200, 24);
            this.cbo_2.TabIndex = 6;
            // 
            // lbl_cbo_2
            // 
            this.lbl_cbo_2.AutoSize = true;
            this.lbl_cbo_2.Location = new System.Drawing.Point(6, 78);
            this.lbl_cbo_2.Name = "lbl_cbo_2";
            this.lbl_cbo_2.Size = new System.Drawing.Size(53, 16);
            this.lbl_cbo_2.TabIndex = 5;
            this.lbl_cbo_2.Text = "User ID";
            // 
            // chk_3
            // 
            this.chk_3.AutoSize = true;
            this.chk_3.Location = new System.Drawing.Point(16, 189);
            this.chk_3.Name = "chk_3";
            this.chk_3.Size = new System.Drawing.Size(92, 20);
            this.chk_3.TabIndex = 4;
            this.chk_3.Text = "checkbox3";
            this.chk_3.UseVisualStyleBackColor = true;
            // 
            // chk_2
            // 
            this.chk_2.AutoSize = true;
            this.chk_2.Location = new System.Drawing.Point(16, 163);
            this.chk_2.Name = "chk_2";
            this.chk_2.Size = new System.Drawing.Size(92, 20);
            this.chk_2.TabIndex = 3;
            this.chk_2.Text = "checkbox2";
            this.chk_2.UseVisualStyleBackColor = true;
            // 
            // chk_1
            // 
            this.chk_1.AutoSize = true;
            this.chk_1.Location = new System.Drawing.Point(16, 137);
            this.chk_1.Name = "chk_1";
            this.chk_1.Size = new System.Drawing.Size(92, 20);
            this.chk_1.TabIndex = 2;
            this.chk_1.Text = "checkbox1";
            this.chk_1.UseVisualStyleBackColor = true;
            // 
            // cbo_1
            // 
            this.cbo_1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_1.FormattingEnabled = true;
            this.cbo_1.Location = new System.Drawing.Point(9, 48);
            this.cbo_1.Name = "cbo_1";
            this.cbo_1.Size = new System.Drawing.Size(200, 24);
            this.cbo_1.TabIndex = 1;
            // 
            // lbl_cbo_1
            // 
            this.lbl_cbo_1.AutoSize = true;
            this.lbl_cbo_1.Location = new System.Drawing.Point(6, 29);
            this.lbl_cbo_1.Name = "lbl_cbo_1";
            this.lbl_cbo_1.Size = new System.Drawing.Size(53, 16);
            this.lbl_cbo_1.TabIndex = 1;
            this.lbl_cbo_1.Text = "User ID";
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.Color.Peru;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_submit.Location = new System.Drawing.Point(120, 24);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(106, 37);
            this.btn_submit.TabIndex = 1;
            this.btn_submit.Text = "Preview";
            this.btn_submit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // grp_bydate
            // 
            this.grp_bydate.Controls.Add(this.lbl_dt_to);
            this.grp_bydate.Controls.Add(this.dtp_to);
            this.grp_bydate.Controls.Add(this.lbl_dt_frm);
            this.grp_bydate.Controls.Add(this.dtp_frm);
            this.grp_bydate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grp_bydate.Location = new System.Drawing.Point(7, 67);
            this.grp_bydate.Name = "grp_bydate";
            this.grp_bydate.Size = new System.Drawing.Size(220, 104);
            this.grp_bydate.TabIndex = 0;
            this.grp_bydate.TabStop = false;
            this.grp_bydate.Text = "By Date";
            // 
            // lbl_dt_to
            // 
            this.lbl_dt_to.AutoSize = true;
            this.lbl_dt_to.Location = new System.Drawing.Point(6, 65);
            this.lbl_dt_to.Name = "lbl_dt_to";
            this.lbl_dt_to.Size = new System.Drawing.Size(25, 16);
            this.lbl_dt_to.TabIndex = 3;
            this.lbl_dt_to.Text = "To";
            // 
            // dtp_to
            // 
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(51, 60);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(96, 22);
            this.dtp_to.TabIndex = 2;
            // 
            // lbl_dt_frm
            // 
            this.lbl_dt_frm.AutoSize = true;
            this.lbl_dt_frm.Location = new System.Drawing.Point(6, 26);
            this.lbl_dt_frm.Name = "lbl_dt_frm";
            this.lbl_dt_frm.Size = new System.Drawing.Size(39, 16);
            this.lbl_dt_frm.TabIndex = 1;
            this.lbl_dt_frm.Text = "From";
            // 
            // dtp_frm
            // 
            this.dtp_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_frm.Location = new System.Drawing.Point(51, 21);
            this.dtp_frm.Name = "dtp_frm";
            this.dtp_frm.Size = new System.Drawing.Size(96, 22);
            this.dtp_frm.TabIndex = 0;
            // 
            // crptviewer
            // 
            this.crptviewer.ActiveViewIndex = -1;
            this.crptviewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crptviewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crptviewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crptviewer.Location = new System.Drawing.Point(233, 0);
            this.crptviewer.Name = "crptviewer";
            this.crptviewer.SelectionFormula = "";
            this.crptviewer.Size = new System.Drawing.Size(623, 515);
            this.crptviewer.TabIndex = 3;
            this.crptviewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crptviewer.ViewTimeSelectionFormula = "";
            // 
            // pnl_pbar
            // 
            this.pnl_pbar.Controls.Add(this.lbl_status);
            this.pnl_pbar.Controls.Add(this.progressBar1);
            this.pnl_pbar.Location = new System.Drawing.Point(483, 215);
            this.pnl_pbar.Name = "pnl_pbar";
            this.pnl_pbar.Size = new System.Drawing.Size(233, 107);
            this.pnl_pbar.TabIndex = 7;
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
            // bgworker
            // 
            this.bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworker_DoWork);
            // 
            // rpt_Global
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(856, 515);
            this.Controls.Add(this.pnl_pbar);
            this.Controls.Add(this.crptviewer);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "rpt_Global";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "rpt_Global";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rpt_Global_Load);
            this.panel1.ResumeLayout(false);
            this.grp_options.ResumeLayout(false);
            this.grp_options.PerformLayout();
            this.grp_bydate.ResumeLayout(false);
            this.grp_bydate.PerformLayout();
            this.pnl_pbar.ResumeLayout(false);
            this.pnl_pbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.GroupBox grp_options;
        private System.Windows.Forms.ComboBox cbo_3;
        private System.Windows.Forms.Label lbl_cbo_3;
        private System.Windows.Forms.ComboBox cbo_2;
        private System.Windows.Forms.Label lbl_cbo_2;
        private System.Windows.Forms.CheckBox chk_3;
        private System.Windows.Forms.CheckBox chk_2;
        private System.Windows.Forms.CheckBox chk_1;
        private System.Windows.Forms.ComboBox cbo_1;
        private System.Windows.Forms.Label lbl_cbo_1;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.GroupBox grp_bydate;
        private System.Windows.Forms.Label lbl_dt_to;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label lbl_dt_frm;
        private System.Windows.Forms.DateTimePicker dtp_frm;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crptviewer;
        private System.Windows.Forms.Panel pnl_pbar;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bgworker;

    }
}