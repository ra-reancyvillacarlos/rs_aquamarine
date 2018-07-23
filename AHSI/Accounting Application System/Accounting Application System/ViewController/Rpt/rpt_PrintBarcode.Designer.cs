namespace Accounting_Application_System
{
    partial class rpt_PrintBarcode
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
            this.label3 = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_noofcopies = new System.Windows.Forms.TextBox();
            this.cbo_source = new System.Windows.Forms.ComboBox();
            this.cbo_itemcode = new System.Windows.Forms.ComboBox();
            this.crptviewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.pnl_pbar = new System.Windows.Forms.Panel();
            this.lbl_status = new System.Windows.Forms.Label();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.pnl_pbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btn_submit);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_noofcopies);
            this.panel1.Controls.Add(this.cbo_source);
            this.panel1.Controls.Add(this.cbo_itemcode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 735);
            this.panel1.TabIndex = 3;
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.Peru;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_clear.Location = new System.Drawing.Point(8, 22);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(107, 37);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "Clear";
            this.btn_clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Source";
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.Color.Peru;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_submit.Location = new System.Drawing.Point(121, 22);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(106, 37);
            this.btn_submit.TabIndex = 1;
            this.btn_submit.Text = "Preview";
            this.btn_submit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Item Code / Line Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Number of Copies";
            // 
            // txt_noofcopies
            // 
            this.txt_noofcopies.Location = new System.Drawing.Point(127, 195);
            this.txt_noofcopies.Name = "txt_noofcopies";
            this.txt_noofcopies.Size = new System.Drawing.Size(100, 22);
            this.txt_noofcopies.TabIndex = 10;
            this.txt_noofcopies.Text = "1";
            // 
            // cbo_source
            // 
            this.cbo_source.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_source.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_source.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_source.FormattingEnabled = true;
            this.cbo_source.Items.AddRange(new object[] {
            "Master File",
            "Direct Purchase",
            "Receiving PO",
            "Stock Transfer",
            "Stock Adjustment"});
            this.cbo_source.Location = new System.Drawing.Point(8, 106);
            this.cbo_source.Name = "cbo_source";
            this.cbo_source.Size = new System.Drawing.Size(219, 24);
            this.cbo_source.TabIndex = 9;
            // 
            // cbo_itemcode
            // 
            this.cbo_itemcode.FormattingEnabled = true;
            this.cbo_itemcode.Location = new System.Drawing.Point(8, 153);
            this.cbo_itemcode.Name = "cbo_itemcode";
            this.cbo_itemcode.Size = new System.Drawing.Size(219, 24);
            this.cbo_itemcode.TabIndex = 8;
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
            this.crptviewer.ShowLogo = false;
            this.crptviewer.Size = new System.Drawing.Size(716, 735);
            this.crptviewer.TabIndex = 4;
            this.crptviewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crptviewer.ViewTimeSelectionFormula = "";
            // 
            // pnl_pbar
            // 
            this.pnl_pbar.Controls.Add(this.lbl_status);
            this.pnl_pbar.Controls.Add(this.pbar);
            this.pnl_pbar.Location = new System.Drawing.Point(622, 300);
            this.pnl_pbar.Name = "pnl_pbar";
            this.pnl_pbar.Size = new System.Drawing.Size(233, 107);
            this.pnl_pbar.TabIndex = 8;
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
            // pbar
            // 
            this.pbar.Location = new System.Drawing.Point(7, 50);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(219, 23);
            this.pbar.TabIndex = 8;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // rpt_PrintBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(949, 735);
            this.Controls.Add(this.pnl_pbar);
            this.Controls.Add(this.crptviewer);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeBox = false;
            this.Name = "rpt_PrintBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Barcode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rpt_PrintBarcode_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_pbar.ResumeLayout(false);
            this.pnl_pbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_submit;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crptviewer;
        private System.Windows.Forms.Panel pnl_pbar;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.ComboBox cbo_itemcode;
        private System.Windows.Forms.ComboBox cbo_source;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_noofcopies;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}