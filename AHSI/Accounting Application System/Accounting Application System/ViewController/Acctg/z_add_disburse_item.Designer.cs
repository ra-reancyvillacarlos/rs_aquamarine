﻿namespace Accounting_Application_System
{
    partial class z_add_disburse_item
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
            this.btn_chg_cbo_subdiaryname = new System.Windows.Forms.Button();
            this.btn_chg_cbo_accttitle = new System.Windows.Forms.Button();
            this.rtxt_notes = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_subdiaryname = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbo_costcenter = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cbo_subcostcenter = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_getinvoice = new System.Windows.Forms.Button();
            this.cbo_accttitle = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_line = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_credit = new System.Windows.Forms.TextBox();
            this.txt_invoice_no = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txt_accttitle_code = new System.Windows.Forms.TextBox();
            this.btn_itemclose = new System.Windows.Forms.Button();
            this.btn_itemsave = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_chg_cbo_subdiaryname
            // 
            this.btn_chg_cbo_subdiaryname.BackColor = System.Drawing.Color.White;
            this.btn_chg_cbo_subdiaryname.Location = new System.Drawing.Point(520, 98);
            this.btn_chg_cbo_subdiaryname.Name = "btn_chg_cbo_subdiaryname";
            this.btn_chg_cbo_subdiaryname.Size = new System.Drawing.Size(24, 24);
            this.btn_chg_cbo_subdiaryname.TabIndex = 15;
            this.btn_chg_cbo_subdiaryname.Text = "^";
            this.btn_chg_cbo_subdiaryname.UseVisualStyleBackColor = false;
            // 
            // btn_chg_cbo_accttitle
            // 
            this.btn_chg_cbo_accttitle.BackColor = System.Drawing.Color.White;
            this.btn_chg_cbo_accttitle.Location = new System.Drawing.Point(520, 69);
            this.btn_chg_cbo_accttitle.Name = "btn_chg_cbo_accttitle";
            this.btn_chg_cbo_accttitle.Size = new System.Drawing.Size(24, 24);
            this.btn_chg_cbo_accttitle.TabIndex = 14;
            this.btn_chg_cbo_accttitle.Text = "^";
            this.btn_chg_cbo_accttitle.UseVisualStyleBackColor = false;
            // 
            // rtxt_notes
            // 
            this.rtxt_notes.Location = new System.Drawing.Point(122, 240);
            this.rtxt_notes.Name = "rtxt_notes";
            this.rtxt_notes.Size = new System.Drawing.Size(393, 43);
            this.rtxt_notes.TabIndex = 20;
            this.rtxt_notes.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(10, 240);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 54;
            this.label1.Text = "Notes";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbo_subdiaryname
            // 
            this.cbo_subdiaryname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_subdiaryname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_subdiaryname.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_subdiaryname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_subdiaryname.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_subdiaryname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_subdiaryname.FormattingEnabled = true;
            this.cbo_subdiaryname.Location = new System.Drawing.Point(122, 99);
            this.cbo_subdiaryname.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_subdiaryname.Name = "cbo_subdiaryname";
            this.cbo_subdiaryname.Size = new System.Drawing.Size(393, 24);
            this.cbo_subdiaryname.TabIndex = 12;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label27.Location = new System.Drawing.Point(10, 215);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(49, 15);
            this.label27.TabIndex = 29;
            this.label27.Text = "Amount";
            this.label27.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbo_costcenter);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.cbo_subcostcenter);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.btn_getinvoice);
            this.groupBox1.Controls.Add(this.btn_chg_cbo_subdiaryname);
            this.groupBox1.Controls.Add(this.btn_chg_cbo_accttitle);
            this.groupBox1.Controls.Add(this.rtxt_notes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbo_subdiaryname);
            this.groupBox1.Controls.Add(this.cbo_accttitle);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txt_line);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txt_credit);
            this.groupBox1.Controls.Add(this.txt_invoice_no);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.txt_accttitle_code);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(578, 293);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Journal Entry Line";
            // 
            // cbo_costcenter
            // 
            this.cbo_costcenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_costcenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_costcenter.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_costcenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_costcenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_costcenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_costcenter.FormattingEnabled = true;
            this.cbo_costcenter.Location = new System.Drawing.Point(122, 153);
            this.cbo_costcenter.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_costcenter.Name = "cbo_costcenter";
            this.cbo_costcenter.Size = new System.Drawing.Size(393, 24);
            this.cbo_costcenter.TabIndex = 403;
            this.cbo_costcenter.SelectedIndexChanged += new System.EventHandler(this.cbo_costcenter_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label22.Location = new System.Drawing.Point(10, 189);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(95, 15);
            this.label22.TabIndex = 405;
            this.label22.Text = "Sub Cost Center";
            // 
            // cbo_subcostcenter
            // 
            this.cbo_subcostcenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_subcostcenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_subcostcenter.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_subcostcenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_subcostcenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_subcostcenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_subcostcenter.FormattingEnabled = true;
            this.cbo_subcostcenter.Location = new System.Drawing.Point(122, 181);
            this.cbo_subcostcenter.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_subcostcenter.Name = "cbo_subcostcenter";
            this.cbo_subcostcenter.Size = new System.Drawing.Size(393, 24);
            this.cbo_subcostcenter.TabIndex = 404;
            this.cbo_subcostcenter.SelectedIndexChanged += new System.EventHandler(this.cbo_subcostcenter_SelectedIndexChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label33.Location = new System.Drawing.Point(10, 161);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(70, 15);
            this.label33.TabIndex = 402;
            this.label33.Text = "Cost Center";
            this.label33.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_getinvoice
            // 
            this.btn_getinvoice.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btn_getinvoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_getinvoice.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_getinvoice.Location = new System.Drawing.Point(269, 126);
            this.btn_getinvoice.Name = "btn_getinvoice";
            this.btn_getinvoice.Size = new System.Drawing.Size(164, 24);
            this.btn_getinvoice.TabIndex = 401;
            this.btn_getinvoice.Text = "Get Invoice Balances >>";
            this.btn_getinvoice.UseVisualStyleBackColor = false;
            this.btn_getinvoice.Click += new System.EventHandler(this.btn_getinvoice_Click);
            // 
            // cbo_accttitle
            // 
            this.cbo_accttitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbo_accttitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbo_accttitle.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_accttitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_accttitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_accttitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_accttitle.FormattingEnabled = true;
            this.cbo_accttitle.Location = new System.Drawing.Point(122, 71);
            this.cbo_accttitle.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_accttitle.Name = "cbo_accttitle";
            this.cbo_accttitle.Size = new System.Drawing.Size(393, 24);
            this.cbo_accttitle.TabIndex = 11;
            this.cbo_accttitle.SelectedIndexChanged += new System.EventHandler(this.cbo_accttitle_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label21.Location = new System.Drawing.Point(10, 133);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(64, 15);
            this.label21.TabIndex = 49;
            this.label21.Text = "Invoice No";
            // 
            // txt_line
            // 
            this.txt_line.AutoSize = true;
            this.txt_line.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_line.Location = new System.Drawing.Point(119, 23);
            this.txt_line.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_line.Name = "txt_line";
            this.txt_line.Size = new System.Drawing.Size(15, 16);
            this.txt_line.TabIndex = 48;
            this.txt_line.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label19.Location = new System.Drawing.Point(10, 23);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(31, 15);
            this.label19.TabIndex = 47;
            this.label19.Text = "Line";
            // 
            // txt_credit
            // 
            this.txt_credit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_credit.Location = new System.Drawing.Point(122, 209);
            this.txt_credit.Margin = new System.Windows.Forms.Padding(2);
            this.txt_credit.Name = "txt_credit";
            this.txt_credit.Size = new System.Drawing.Size(140, 22);
            this.txt_credit.TabIndex = 19;
            this.txt_credit.Text = "0.00";
            this.txt_credit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_credit.Enter += new System.EventHandler(this.txt_EnterTextNumber);
            this.txt_credit.Leave += new System.EventHandler(this.txt_LeaveTextNumber);
            // 
            // txt_invoice_no
            // 
            this.txt_invoice_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_invoice_no.Location = new System.Drawing.Point(122, 127);
            this.txt_invoice_no.Margin = new System.Windows.Forms.Padding(2);
            this.txt_invoice_no.Name = "txt_invoice_no";
            this.txt_invoice_no.Size = new System.Drawing.Size(140, 22);
            this.txt_invoice_no.TabIndex = 13;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label30.Location = new System.Drawing.Point(10, 107);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(101, 15);
            this.label30.TabIndex = 16;
            this.label30.Text = "Subsidiary Name";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label34.Location = new System.Drawing.Point(10, 81);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(76, 15);
            this.label34.TabIndex = 2;
            this.label34.Text = "Account Title";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label35.Location = new System.Drawing.Point(11, 51);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(36, 15);
            this.label35.TabIndex = 1;
            this.label35.Text = "Code";
            // 
            // txt_accttitle_code
            // 
            this.txt_accttitle_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_accttitle_code.Location = new System.Drawing.Point(122, 45);
            this.txt_accttitle_code.Margin = new System.Windows.Forms.Padding(2);
            this.txt_accttitle_code.Name = "txt_accttitle_code";
            this.txt_accttitle_code.ReadOnly = true;
            this.txt_accttitle_code.Size = new System.Drawing.Size(140, 22);
            this.txt_accttitle_code.TabIndex = 400;
            // 
            // btn_itemclose
            // 
            this.btn_itemclose.BackColor = System.Drawing.Color.Peru;
            this.btn_itemclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_itemclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_itemclose.Image = global::Accounting_Application_System.Properties.Resources._1343907460_go_back;
            this.btn_itemclose.Location = new System.Drawing.Point(180, 305);
            this.btn_itemclose.Margin = new System.Windows.Forms.Padding(2);
            this.btn_itemclose.Name = "btn_itemclose";
            this.btn_itemclose.Size = new System.Drawing.Size(106, 49);
            this.btn_itemclose.TabIndex = 13;
            this.btn_itemclose.Text = "Close";
            this.btn_itemclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_itemclose.UseVisualStyleBackColor = false;
            this.btn_itemclose.Click += new System.EventHandler(this.btn_itemclose_Click);
            // 
            // btn_itemsave
            // 
            this.btn_itemsave.BackColor = System.Drawing.Color.Peru;
            this.btn_itemsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_itemsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_itemsave.Image = global::Accounting_Application_System.Properties.Resources._1343908142_database_save;
            this.btn_itemsave.Location = new System.Drawing.Point(291, 305);
            this.btn_itemsave.Margin = new System.Windows.Forms.Padding(2);
            this.btn_itemsave.Name = "btn_itemsave";
            this.btn_itemsave.Size = new System.Drawing.Size(106, 49);
            this.btn_itemsave.TabIndex = 21;
            this.btn_itemsave.Text = "Save";
            this.btn_itemsave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_itemsave.UseVisualStyleBackColor = false;
            this.btn_itemsave.Click += new System.EventHandler(this.btn_itemsave_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Info;
            this.panel4.Controls.Add(this.btn_itemclose);
            this.panel4.Controls.Add(this.btn_itemsave);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(578, 365);
            this.panel4.TabIndex = 2;
            // 
            // z_add_disburse_item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(578, 365);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "z_add_disburse_item";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "z_add_disburse_item";
            this.Load += new System.EventHandler(this.z_add_disburse_item_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_chg_cbo_subdiaryname;
        private System.Windows.Forms.Button btn_chg_cbo_accttitle;
        private System.Windows.Forms.RichTextBox rtxt_notes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_subdiaryname;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbo_accttitle;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label txt_line;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_credit;
        private System.Windows.Forms.TextBox txt_invoice_no;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txt_accttitle_code;
        private System.Windows.Forms.Button btn_itemclose;
        private System.Windows.Forms.Button btn_itemsave;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_getinvoice;
        private System.Windows.Forms.ComboBox cbo_costcenter;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cbo_subcostcenter;
        private System.Windows.Forms.Label label33;
    }
}