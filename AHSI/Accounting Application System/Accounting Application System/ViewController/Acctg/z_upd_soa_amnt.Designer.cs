namespace Accounting_Application_System
{
    partial class z_upd_soa_amnt
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
            this.btn_itemclose = new System.Windows.Forms.Button();
            this.btn_itemupd = new System.Windows.Forms.Button();
            this.txt_amount = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_itemclose
            // 
            this.btn_itemclose.BackColor = System.Drawing.Color.Peru;
            this.btn_itemclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_itemclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_itemclose.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_itemclose.Image = global::Accounting_Application_System.Properties.Resources._1343907460_go_back;
            this.btn_itemclose.Location = new System.Drawing.Point(64, 71);
            this.btn_itemclose.Margin = new System.Windows.Forms.Padding(2);
            this.btn_itemclose.Name = "btn_itemclose";
            this.btn_itemclose.Size = new System.Drawing.Size(106, 49);
            this.btn_itemclose.TabIndex = 26;
            this.btn_itemclose.Text = "Close";
            this.btn_itemclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_itemclose.UseVisualStyleBackColor = false;
            this.btn_itemclose.Click += new System.EventHandler(this.btn_itemclose_Click);
            // 
            // btn_itemupd
            // 
            this.btn_itemupd.BackColor = System.Drawing.Color.Peru;
            this.btn_itemupd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_itemupd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_itemupd.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_itemupd.Location = new System.Drawing.Point(174, 71);
            this.btn_itemupd.Margin = new System.Windows.Forms.Padding(2);
            this.btn_itemupd.Name = "btn_itemupd";
            this.btn_itemupd.Size = new System.Drawing.Size(107, 49);
            this.btn_itemupd.TabIndex = 27;
            this.btn_itemupd.Text = "Update";
            this.btn_itemupd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_itemupd.UseVisualStyleBackColor = false;
            this.btn_itemupd.Click += new System.EventHandler(this.btn_itemupd_Click);
            // 
            // txt_amount
            // 
            this.txt_amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_amount.Location = new System.Drawing.Point(125, 25);
            this.txt_amount.Margin = new System.Windows.Forms.Padding(2);
            this.txt_amount.Name = "txt_amount";
            this.txt_amount.Size = new System.Drawing.Size(180, 24);
            this.txt_amount.TabIndex = 29;
            this.txt_amount.Text = "0.00";
            this.txt_amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(33, 31);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 16);
            this.label31.TabIndex = 28;
            this.label31.Text = "Amount";
            // 
            // z_upd_soa_amnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(348, 139);
            this.Controls.Add(this.txt_amount);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.btn_itemclose);
            this.Controls.Add(this.btn_itemupd);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "z_upd_soa_amnt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update SOA Amount";
            this.Load += new System.EventHandler(this.z_upd_soa_amnt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_itemclose;
        private System.Windows.Forms.Button btn_itemupd;
        private System.Windows.Forms.TextBox txt_amount;
        private System.Windows.Forms.Label label31;
    }
}