namespace Hotel_System
{
    partial class or_entry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(or_entry));
            this.pnl_side = new System.Windows.Forms.Panel();
            this.grpbx_res = new System.Windows.Forms.GroupBox();
            this.dgv_orlist = new System.Windows.Forms.DataGridView();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_tdate = new System.Windows.Forms.DateTimePicker();
            this.lbl_reference = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_ref = new System.Windows.Forms.TextBox();
            this.txt_amt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_serial = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_deposit = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lbl_gfolionum = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lbl_guestname = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_balance = new System.Windows.Forms.Label();
            this.Room = new System.Windows.Forms.Label();
            this.lbl_rom_code = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnl_side.SuspendLayout();
            this.grpbx_res.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_orlist)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_side
            // 
            this.pnl_side.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnl_side.Controls.Add(this.grpbx_res);
            this.pnl_side.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_side.Location = new System.Drawing.Point(0, 0);
            this.pnl_side.Name = "pnl_side";
            this.pnl_side.Size = new System.Drawing.Size(528, 733);
            this.pnl_side.TabIndex = 12;
            // 
            // grpbx_res
            // 
            this.grpbx_res.Controls.Add(this.dgv_orlist);
            this.grpbx_res.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpbx_res.Location = new System.Drawing.Point(12, 12);
            this.grpbx_res.Name = "grpbx_res";
            this.grpbx_res.Size = new System.Drawing.Size(502, 536);
            this.grpbx_res.TabIndex = 0;
            this.grpbx_res.TabStop = false;
            this.grpbx_res.Text = "OR Summary";
            // 
            // dgv_orlist
            // 
            this.dgv_orlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_orlist.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_orlist.Location = new System.Drawing.Point(12, 32);
            this.dgv_orlist.MultiSelect = false;
            this.dgv_orlist.Name = "dgv_orlist";
            this.dgv_orlist.ReadOnly = true;
            this.dgv_orlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_orlist.Size = new System.Drawing.Size(476, 484);
            this.dgv_orlist.TabIndex = 4;
            this.dgv_orlist.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_orlist_CellClick);
            this.dgv_orlist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_orlist_CellPainting);
            // 
            // btn_close
            // 
            this.btn_close.Image = ((System.Drawing.Image)(resources.GetObject("btn_close.Image")));
            this.btn_close.Location = new System.Drawing.Point(645, 423);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(129, 66);
            this.btn_close.TabIndex = 23;
            this.btn_close.Text = "Close";
            this.btn_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_save
            // 
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.Location = new System.Drawing.Point(780, 423);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(129, 66);
            this.btn_save.TabIndex = 22;
            this.btn_save.Text = "Save";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction Date";
            // 
            // dtp_tdate
            // 
            this.dtp_tdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_tdate.Location = new System.Drawing.Point(154, 35);
            this.dtp_tdate.Margin = new System.Windows.Forms.Padding(4);
            this.dtp_tdate.Name = "dtp_tdate";
            this.dtp_tdate.Size = new System.Drawing.Size(129, 22);
            this.dtp_tdate.TabIndex = 1;
            // 
            // lbl_reference
            // 
            this.lbl_reference.AutoSize = true;
            this.lbl_reference.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl_reference.Location = new System.Drawing.Point(13, 71);
            this.lbl_reference.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_reference.Name = "lbl_reference";
            this.lbl_reference.Size = new System.Drawing.Size(85, 15);
            this.lbl_reference.TabIndex = 5;
            this.lbl_reference.Text = "OR Reference";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(13, 107);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Amount";
            // 
            // txt_ref
            // 
            this.txt_ref.Location = new System.Drawing.Point(151, 68);
            this.txt_ref.Name = "txt_ref";
            this.txt_ref.Size = new System.Drawing.Size(276, 22);
            this.txt_ref.TabIndex = 12;
            // 
            // txt_amt
            // 
            this.txt_amt.Location = new System.Drawing.Point(154, 104);
            this.txt_amt.Name = "txt_amt";
            this.txt_amt.Size = new System.Drawing.Size(101, 22);
            this.txt_amt.TabIndex = 13;
            this.txt_amt.Text = "0.00";
            this.txt_amt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_serial);
            this.groupBox1.Controls.Add(this.txt_amt);
            this.groupBox1.Controls.Add(this.txt_ref);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbl_reference);
            this.groupBox1.Controls.Add(this.dtp_tdate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(535, 242);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(464, 148);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Official Receipt Detail Entry";
            // 
            // lbl_serial
            // 
            this.lbl_serial.AutoSize = true;
            this.lbl_serial.Location = new System.Drawing.Point(390, 19);
            this.lbl_serial.Name = "lbl_serial";
            this.lbl_serial.Size = new System.Drawing.Size(0, 16);
            this.lbl_serial.TabIndex = 30;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.richTextBox1);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.lbl_gfolionum);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.lbl_guestname);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.Room);
            this.panel4.Controls.Add(this.lbl_rom_code);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(528, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(492, 235);
            this.panel4.TabIndex = 32;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(178, 130);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(202, 69);
            this.richTextBox1.TabIndex = 31;
            this.richTextBox1.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label9.Location = new System.Drawing.Point(18, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "Folio Number";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Navy;
            this.panel3.Controls.Add(this.lbl_deposit);
            this.panel3.Location = new System.Drawing.Point(282, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(100, 20);
            this.panel3.TabIndex = 30;
            // 
            // lbl_deposit
            // 
            this.lbl_deposit.AutoSize = true;
            this.lbl_deposit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_deposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_deposit.ForeColor = System.Drawing.SystemColors.Info;
            this.lbl_deposit.Location = new System.Drawing.Point(56, 0);
            this.lbl_deposit.Name = "lbl_deposit";
            this.lbl_deposit.Size = new System.Drawing.Size(44, 20);
            this.lbl_deposit.TabIndex = 13;
            this.lbl_deposit.Text = "0.00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label10.Location = new System.Drawing.Point(18, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "Name";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label24.ForeColor = System.Drawing.Color.DarkBlue;
            this.label24.Location = new System.Drawing.Point(181, 35);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 15);
            this.label24.TabIndex = 28;
            this.label24.Text = "Deposit (PHP)";
            // 
            // lbl_gfolionum
            // 
            this.lbl_gfolionum.AutoSize = true;
            this.lbl_gfolionum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_gfolionum.Location = new System.Drawing.Point(116, 8);
            this.lbl_gfolionum.Name = "lbl_gfolionum";
            this.lbl_gfolionum.Size = new System.Drawing.Size(0, 16);
            this.lbl_gfolionum.TabIndex = 16;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(175, 12);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(88, 15);
            this.label25.TabIndex = 27;
            this.label25.Text = "Balance (PHP)";
            // 
            // lbl_guestname
            // 
            this.lbl_guestname.AutoSize = true;
            this.lbl_guestname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_guestname.Location = new System.Drawing.Point(116, 34);
            this.lbl_guestname.Name = "lbl_guestname";
            this.lbl_guestname.Size = new System.Drawing.Size(0, 16);
            this.lbl_guestname.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Controls.Add(this.lbl_balance);
            this.panel2.Location = new System.Drawing.Point(284, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(98, 20);
            this.panel2.TabIndex = 29;
            // 
            // lbl_balance
            // 
            this.lbl_balance.AutoSize = true;
            this.lbl_balance.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_balance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_balance.ForeColor = System.Drawing.SystemColors.Info;
            this.lbl_balance.Location = new System.Drawing.Point(54, 0);
            this.lbl_balance.Name = "lbl_balance";
            this.lbl_balance.Size = new System.Drawing.Size(44, 20);
            this.lbl_balance.TabIndex = 5;
            this.lbl_balance.Text = "0.00";
            // 
            // Room
            // 
            this.Room.AutoSize = true;
            this.Room.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Room.Location = new System.Drawing.Point(18, 59);
            this.Room.Name = "Room";
            this.Room.Size = new System.Drawing.Size(60, 15);
            this.Room.TabIndex = 18;
            this.Room.Text = "Room No";
            // 
            // lbl_rom_code
            // 
            this.lbl_rom_code.AutoSize = true;
            this.lbl_rom_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rom_code.Location = new System.Drawing.Point(116, 59);
            this.lbl_rom_code.Name = "lbl_rom_code";
            this.lbl_rom_code.Size = new System.Drawing.Size(0, 16);
            this.lbl_rom_code.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(177, 111);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 16);
            this.label15.TabIndex = 25;
            this.label15.Text = "Remark";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label11.Location = new System.Drawing.Point(18, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 15);
            this.label11.TabIndex = 20;
            this.label11.Text = "Type";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label12.Location = new System.Drawing.Point(18, 111);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 15);
            this.label12.TabIndex = 21;
            this.label12.Text = "Room Rate";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label13.Location = new System.Drawing.Point(18, 167);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 15);
            this.label13.TabIndex = 23;
            this.label13.Text = "Departure";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label14.Location = new System.Drawing.Point(18, 140);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 15);
            this.label14.TabIndex = 22;
            this.label14.Text = "Arrival";
            // 
            // or_entry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1020, 733);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnl_side);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "or_entry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OFFICIAL RECEIPT ENTRY";
            this.Load += new System.EventHandler(this.or_entry_Load);
            this.pnl_side.ResumeLayout(false);
            this.grpbx_res.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_orlist)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_side;
        private System.Windows.Forms.GroupBox grpbx_res;
        private System.Windows.Forms.DataGridView dgv_orlist;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_tdate;
        private System.Windows.Forms.Label lbl_reference;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_ref;
        private System.Windows.Forms.TextBox txt_amt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_serial;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_deposit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lbl_gfolionum;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lbl_guestname;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_balance;
        private System.Windows.Forms.Label Room;
        private System.Windows.Forms.Label lbl_rom_code;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}