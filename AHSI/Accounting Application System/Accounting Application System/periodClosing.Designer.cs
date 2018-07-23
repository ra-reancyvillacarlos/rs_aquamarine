namespace Accounting_Application_System
{
    partial class periodClosing
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
            this.lbl_typ = new System.Windows.Forms.Label();
            this.cbo_fy = new System.Windows.Forms.ComboBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_proceed = new System.Windows.Forms.Button();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_typ
            // 
            this.lbl_typ.AutoSize = true;
            this.lbl_typ.Location = new System.Drawing.Point(12, 40);
            this.lbl_typ.Name = "lbl_typ";
            this.lbl_typ.Size = new System.Drawing.Size(85, 15);
            this.lbl_typ.TabIndex = 18;
            this.lbl_typ.Text = "Financial Year";
            // 
            // cbo_fy
            // 
            this.cbo_fy.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_fy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_fy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_fy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_fy.FormattingEnabled = true;
            this.cbo_fy.Location = new System.Drawing.Point(120, 37);
            this.cbo_fy.Name = "cbo_fy";
            this.cbo_fy.Size = new System.Drawing.Size(317, 24);
            this.cbo_fy.TabIndex = 17;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Peru;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_close.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_close.Location = new System.Drawing.Point(146, 74);
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
            this.btn_proceed.Location = new System.Drawing.Point(234, 74);
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
            this.pbar.Location = new System.Drawing.Point(3, 149);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(482, 34);
            this.pbar.TabIndex = 8;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_typ);
            this.groupBox1.Controls.Add(this.pbar);
            this.groupBox1.Controls.Add(this.cbo_fy);
            this.groupBox1.Controls.Add(this.btn_proceed);
            this.groupBox1.Controls.Add(this.btn_close);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 186);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Period Closing";
            // 
            // periodClosing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(488, 186);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "periodClosing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Closing Period";
            this.Load += new System.EventHandler(this.j_hotel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_proceed;
        private System.Windows.Forms.ProgressBar pbar;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label lbl_typ;
        private System.Windows.Forms.ComboBox cbo_fy;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}