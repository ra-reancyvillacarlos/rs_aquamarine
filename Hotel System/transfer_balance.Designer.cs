namespace Hotel_System
{
    partial class transfer_balance
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbo_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_option = new System.Windows.Forms.ComboBox();
            this.lbl_gfno_to = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_rm_to = new System.Windows.Forms.Label();
            this.lbl_guest_to = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_bal_amt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_transfer = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.rtxt_notes = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_isescaper = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_search = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbl_gfno_frm = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_rm_frm = new System.Windows.Forms.Label();
            this.lbl_guest_frm = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_search)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbo_type);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbo_option);
            this.groupBox1.Controls.Add(this.lbl_gfno_to);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbl_rm_to);
            this.groupBox1.Controls.Add(this.lbl_guest_to);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_bal_amt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(394, 90);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(440, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Balance/Payment Transfer To";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Charge Type";
            // 
            // cbo_type
            // 
            this.cbo_type.BackColor = System.Drawing.SystemColors.Info;
            this.cbo_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbo_type.Enabled = false;
            this.cbo_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_type.FormattingEnabled = true;
            this.cbo_type.Items.AddRange(new object[] {
            "Balance",
            "Payment",
            "-----------"});
            this.cbo_type.Location = new System.Drawing.Point(122, 126);
            this.cbo_type.Name = "cbo_type";
            this.cbo_type.Size = new System.Drawing.Size(133, 23);
            this.cbo_type.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Transfer Option";
            // 
            // cbo_option
            // 
            this.cbo_option.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_option.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_option.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_option.FormattingEnabled = true;
            this.cbo_option.Items.AddRange(new object[] {
            "Transfer All balance",
            "Split balance of selected charge",
            "Input balance manually"});
            this.cbo_option.Location = new System.Drawing.Point(122, 98);
            this.cbo_option.Name = "cbo_option";
            this.cbo_option.Size = new System.Drawing.Size(306, 23);
            this.cbo_option.TabIndex = 10;
            this.cbo_option.SelectedIndexChanged += new System.EventHandler(this.cbo_option_SelectedIndexChanged);
            // 
            // lbl_gfno_to
            // 
            this.lbl_gfno_to.AutoSize = true;
            this.lbl_gfno_to.Location = new System.Drawing.Point(205, 24);
            this.lbl_gfno_to.Name = "lbl_gfno_to";
            this.lbl_gfno_to.Size = new System.Drawing.Size(82, 15);
            this.lbl_gfno_to.TabIndex = 9;
            this.lbl_gfno_to.Text = "Folio Number";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Transfer to Contract No / Folio No";
            // 
            // lbl_rm_to
            // 
            this.lbl_rm_to.AutoSize = true;
            this.lbl_rm_to.Location = new System.Drawing.Point(121, 72);
            this.lbl_rm_to.Name = "lbl_rm_to";
            this.lbl_rm_to.Size = new System.Drawing.Size(0, 15);
            this.lbl_rm_to.TabIndex = 7;
            // 
            // lbl_guest_to
            // 
            this.lbl_guest_to.AutoSize = true;
            this.lbl_guest_to.Location = new System.Drawing.Point(121, 48);
            this.lbl_guest_to.Name = "lbl_guest_to";
            this.lbl_guest_to.Size = new System.Drawing.Size(0, 15);
            this.lbl_guest_to.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Transfer to Unit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Transfer to Tennant";
            // 
            // txt_bal_amt
            // 
            this.txt_bal_amt.Location = new System.Drawing.Point(122, 154);
            this.txt_bal_amt.Name = "txt_bal_amt";
            this.txt_bal_amt.Size = new System.Drawing.Size(133, 21);
            this.txt_bal_amt.TabIndex = 1;
            this.txt_bal_amt.Text = "0.00";
            this.txt_bal_amt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Amount To Transfer";
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.SystemColors.Info;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Image = global::Hotel_System.Properties.Resources._1343907586_Cancel;
            this.btn_close.Location = new System.Drawing.Point(489, 437);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(113, 57);
            this.btn_close.TabIndex = 6;
            this.btn_close.Text = "Close";
            this.btn_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_transfer
            // 
            this.btn_transfer.BackColor = System.Drawing.SystemColors.Info;
            this.btn_transfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_transfer.Image = global::Hotel_System.Properties.Resources.transfer;
            this.btn_transfer.Location = new System.Drawing.Point(608, 437);
            this.btn_transfer.Name = "btn_transfer";
            this.btn_transfer.Size = new System.Drawing.Size(113, 57);
            this.btn_transfer.TabIndex = 5;
            this.btn_transfer.Text = "Transfer";
            this.btn_transfer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_transfer.UseVisualStyleBackColor = false;
            this.btn_transfer.Click += new System.EventHandler(this.btn_transfer_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Notes";
            // 
            // rtxt_notes
            // 
            this.rtxt_notes.Location = new System.Drawing.Point(95, 26);
            this.rtxt_notes.Name = "rtxt_notes";
            this.rtxt_notes.Size = new System.Drawing.Size(339, 91);
            this.rtxt_notes.TabIndex = 9;
            this.rtxt_notes.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_isescaper);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.rtxt_notes);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(394, 274);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 148);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tennant Notes";
            // 
            // chk_isescaper
            // 
            this.chk_isescaper.AutoSize = true;
            this.chk_isescaper.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_isescaper.ForeColor = System.Drawing.Color.DarkRed;
            this.chk_isescaper.Location = new System.Drawing.Point(95, 123);
            this.chk_isescaper.Name = "chk_isescaper";
            this.chk_isescaper.Size = new System.Drawing.Size(160, 20);
            this.chk_isescaper.TabIndex = 10;
            this.chk_isescaper.Text = "Tennant is escaper";
            this.chk_isescaper.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel1.Controls.Add(this.dgv_search);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 506);
            this.panel1.TabIndex = 11;
            // 
            // dgv_search
            // 
            this.dgv_search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_search.Location = new System.Drawing.Point(0, 75);
            this.dgv_search.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_search.MultiSelect = false;
            this.dgv_search.Name = "dgv_search";
            this.dgv_search.ReadOnly = true;
            this.dgv_search.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_search.Size = new System.Drawing.Size(394, 431);
            this.dgv_search.TabIndex = 0;
            this.dgv_search.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_search_CellDoubleClick);
            this.dgv_search.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_search_CellContentClick);
            this.dgv_search.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_search_CellPainting);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.DarkKhaki;
            this.groupBox3.Controls.Add(this.btn_search);
            this.groupBox3.Controls.Add(this.txt_search);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(394, 75);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Tennant";
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.SystemColors.Info;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_search.Image = global::Hotel_System.Properties.Resources.search_32x32;
            this.btn_search.Location = new System.Drawing.Point(253, 15);
            this.btn_search.Margin = new System.Windows.Forms.Padding(4);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(131, 52);
            this.btn_search.TabIndex = 9;
            this.btn_search.Text = "Search";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(7, 41);
            this.txt_search.Margin = new System.Windows.Forms.Padding(4);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(238, 21);
            this.txt_search.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(7, 19);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "Contract No or Tennant Name";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbl_gfno_frm);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.lbl_rm_frm);
            this.groupBox4.Controls.Add(this.lbl_guest_frm);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(394, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(440, 90);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Balance From";
            // 
            // lbl_gfno_frm
            // 
            this.lbl_gfno_frm.AutoSize = true;
            this.lbl_gfno_frm.Location = new System.Drawing.Point(173, 23);
            this.lbl_gfno_frm.Name = "lbl_gfno_frm";
            this.lbl_gfno_frm.Size = new System.Drawing.Size(82, 15);
            this.lbl_gfno_frm.TabIndex = 9;
            this.lbl_gfno_frm.Text = "Folio Number";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(155, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "From Contract No./Folio No";
            // 
            // lbl_rm_frm
            // 
            this.lbl_rm_frm.AutoSize = true;
            this.lbl_rm_frm.Location = new System.Drawing.Point(119, 66);
            this.lbl_rm_frm.Name = "lbl_rm_frm";
            this.lbl_rm_frm.Size = new System.Drawing.Size(0, 15);
            this.lbl_rm_frm.TabIndex = 7;
            // 
            // lbl_guest_frm
            // 
            this.lbl_guest_frm.AutoSize = true;
            this.lbl_guest_frm.Location = new System.Drawing.Point(119, 44);
            this.lbl_guest_frm.Name = "lbl_guest_frm";
            this.lbl_guest_frm.Size = new System.Drawing.Size(0, 15);
            this.lbl_guest_frm.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 15);
            this.label12.TabIndex = 5;
            this.label12.Text = "From Room";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 15);
            this.label13.TabIndex = 4;
            this.label13.Text = "From Tennant";
            // 
            // transfer_balance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(834, 506);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_transfer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "transfer_balance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Balance";
            this.Load += new System.EventHandler(this.transfer_balance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_search)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_transfer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_rm_to;
        private System.Windows.Forms.Label lbl_guest_to;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_bal_amt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox rtxt_notes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk_isescaper;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgv_search;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbl_gfno_frm;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_rm_frm;
        private System.Windows.Forms.Label lbl_guest_frm;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbl_gfno_to;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_option;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbo_type;
    }
}