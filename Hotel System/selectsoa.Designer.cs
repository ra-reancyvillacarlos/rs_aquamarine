namespace Hotel_System
{
    partial class selectsoa
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
            this.cbo_1 = new System.Windows.Forms.ComboBox();
            this.lbl_desc = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_finalized = new System.Windows.Forms.Button();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.bgworker = new System.ComponentModel.BackgroundWorker();
            this.cbo_inv_frm = new System.Windows.Forms.ComboBox();
            this.cbo_inv_to = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbo_1
            // 
            this.cbo_1.FormattingEnabled = true;
            this.cbo_1.Location = new System.Drawing.Point(31, 29);
            this.cbo_1.Name = "cbo_1";
            this.cbo_1.Size = new System.Drawing.Size(324, 21);
            this.cbo_1.TabIndex = 12;
            // 
            // lbl_desc
            // 
            this.lbl_desc.AutoSize = true;
            this.lbl_desc.Location = new System.Drawing.Point(27, 13);
            this.lbl_desc.Name = "lbl_desc";
            this.lbl_desc.Size = new System.Drawing.Size(72, 13);
            this.lbl_desc.TabIndex = 13;
            this.lbl_desc.Text = "Select option:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::Hotel_System.Properties.Resources.search_32x32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(24, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 42);
            this.button1.TabIndex = 14;
            this.button1.Text = "View Period";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::Hotel_System.Properties.Resources._1343892237_DeleteRed;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(161, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 42);
            this.button2.TabIndex = 11;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_print
            // 
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.Image = global::Hotel_System.Properties.Resources.Print;
            this.btn_print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_print.Location = new System.Drawing.Point(259, 69);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(93, 42);
            this.btn_print.TabIndex = 10;
            this.btn_print.Text = "Print";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_finalized
            // 
            this.btn_finalized.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_finalized.Image = global::Hotel_System.Properties.Resources.note_search___32;
            this.btn_finalized.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_finalized.Location = new System.Drawing.Point(260, 68);
            this.btn_finalized.Name = "btn_finalized";
            this.btn_finalized.Size = new System.Drawing.Size(108, 42);
            this.btn_finalized.TabIndex = 15;
            this.btn_finalized.Text = "Finalized";
            this.btn_finalized.UseVisualStyleBackColor = true;
            this.btn_finalized.Visible = false;
            this.btn_finalized.Click += new System.EventHandler(this.btn_finalized_Click);
            // 
            // pbar
            // 
            this.pbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbar.Location = new System.Drawing.Point(0, 114);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(392, 23);
            this.pbar.TabIndex = 16;
            this.pbar.Visible = false;
            // 
            // bgworker
            // 
            this.bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworker_DoWork);
            // 
            // cbo_inv_frm
            // 
            this.cbo_inv_frm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_inv_frm.FormattingEnabled = true;
            this.cbo_inv_frm.Location = new System.Drawing.Point(19, 29);
            this.cbo_inv_frm.Name = "cbo_inv_frm";
            this.cbo_inv_frm.Size = new System.Drawing.Size(167, 21);
            this.cbo_inv_frm.TabIndex = 17;
            this.cbo_inv_frm.Visible = false;
            this.cbo_inv_frm.SelectedIndexChanged += new System.EventHandler(this.cbo_inv_frm_SelectedIndexChanged);
            // 
            // cbo_inv_to
            // 
            this.cbo_inv_to.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_inv_to.FormattingEnabled = true;
            this.cbo_inv_to.Location = new System.Drawing.Point(205, 29);
            this.cbo_inv_to.Name = "cbo_inv_to";
            this.cbo_inv_to.Size = new System.Drawing.Size(165, 21);
            this.cbo_inv_to.TabIndex = 18;
            this.cbo_inv_to.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "To";
            this.label1.Visible = false;
            // 
            // selectsoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(392, 137);
            this.Controls.Add(this.cbo_1);
            this.Controls.Add(this.cbo_inv_frm);
            this.Controls.Add(this.cbo_inv_to);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.btn_finalized);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_desc);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_print);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "selectsoa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "select soa";
            this.Load += new System.EventHandler(this.selectprint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbo_1;
        private System.Windows.Forms.Label lbl_desc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_finalized;
        private System.Windows.Forms.ProgressBar pbar;
        private System.ComponentModel.BackgroundWorker bgworker;
        private System.Windows.Forms.ComboBox cbo_inv_frm;
        private System.Windows.Forms.ComboBox cbo_inv_to;
        private System.Windows.Forms.Label label1;
    }
}