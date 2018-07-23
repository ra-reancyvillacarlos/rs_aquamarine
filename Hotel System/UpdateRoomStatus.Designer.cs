namespace Hotel_System
{
    partial class UpdateRoomStatus
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_arrdate = new System.Windows.Forms.Label();
            this.lbl_depdate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv_ooohistory = new System.Windows.Forms.DataGridView();
            this.lbl_gfname = new System.Windows.Forms.Label();
            this.lbl_gfno = new System.Windows.Forms.Label();
            this.btn_fun = new System.Windows.Forms.Button();
            this.btn_ooo = new System.Windows.Forms.Button();
            this.btn_vc = new System.Windows.Forms.Button();
            this.btn_vd = new System.Windows.Forms.Button();
            this.btn_occ = new System.Windows.Forms.Button();
            this.lbl_gfolio = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_rm = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_romstatus = new System.Windows.Forms.DataGridView();
            this.panel64 = new System.Windows.Forms.Panel();
            this.cbo_rmtyp = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_rmstatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ooohistory)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_romstatus)).BeginInit();
            this.panel64.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 742);
            this.panel1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbl_arrdate);
            this.groupBox2.Controls.Add(this.lbl_depdate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dgv_ooohistory);
            this.groupBox2.Controls.Add(this.lbl_gfname);
            this.groupBox2.Controls.Add(this.lbl_gfno);
            this.groupBox2.Controls.Add(this.btn_fun);
            this.groupBox2.Controls.Add(this.btn_ooo);
            this.groupBox2.Controls.Add(this.btn_vc);
            this.groupBox2.Controls.Add(this.btn_vd);
            this.groupBox2.Controls.Add(this.btn_occ);
            this.groupBox2.Controls.Add(this.lbl_gfolio);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox2.Location = new System.Drawing.Point(12, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(457, 533);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unit Status Options";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Info;
            this.button1.Location = new System.Drawing.Point(29, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 49);
            this.button1.TabIndex = 39;
            this.button1.Text = "Office (OFC)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 16);
            this.label7.TabIndex = 38;
            this.label7.Text = "End Date :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 16);
            this.label6.TabIndex = 37;
            this.label6.Text = "Start Date :";
            // 
            // lbl_arrdate
            // 
            this.lbl_arrdate.AutoSize = true;
            this.lbl_arrdate.Location = new System.Drawing.Point(134, 95);
            this.lbl_arrdate.Name = "lbl_arrdate";
            this.lbl_arrdate.Size = new System.Drawing.Size(0, 16);
            this.lbl_arrdate.TabIndex = 36;
            // 
            // lbl_depdate
            // 
            this.lbl_depdate.AutoSize = true;
            this.lbl_depdate.Location = new System.Drawing.Point(132, 121);
            this.lbl_depdate.Name = "lbl_depdate";
            this.lbl_depdate.Size = new System.Drawing.Size(0, 16);
            this.lbl_depdate.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 16);
            this.label3.TabIndex = 32;
            this.label3.Tag = " ";
            this.label3.Text = "OOO History Display";
            // 
            // dgv_ooohistory
            // 
            this.dgv_ooohistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ooohistory.Location = new System.Drawing.Point(19, 338);
            this.dgv_ooohistory.Name = "dgv_ooohistory";
            this.dgv_ooohistory.Size = new System.Drawing.Size(418, 182);
            this.dgv_ooohistory.TabIndex = 31;
            this.dgv_ooohistory.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_ooohistory_CellPainting);
            // 
            // lbl_gfname
            // 
            this.lbl_gfname.AutoSize = true;
            this.lbl_gfname.Location = new System.Drawing.Point(26, 71);
            this.lbl_gfname.Name = "lbl_gfname";
            this.lbl_gfname.Size = new System.Drawing.Size(0, 16);
            this.lbl_gfname.TabIndex = 30;
            // 
            // lbl_gfno
            // 
            this.lbl_gfno.AutoSize = true;
            this.lbl_gfno.Location = new System.Drawing.Point(26, 44);
            this.lbl_gfno.Name = "lbl_gfno";
            this.lbl_gfno.Size = new System.Drawing.Size(0, 16);
            this.lbl_gfno.TabIndex = 28;
            // 
            // btn_fun
            // 
            this.btn_fun.BackColor = System.Drawing.Color.DimGray;
            this.btn_fun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fun.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_fun.Location = new System.Drawing.Point(237, 269);
            this.btn_fun.Name = "btn_fun";
            this.btn_fun.Size = new System.Drawing.Size(200, 49);
            this.btn_fun.TabIndex = 25;
            this.btn_fun.Text = "Function Room (FUN)";
            this.btn_fun.UseVisualStyleBackColor = false;
            this.btn_fun.Click += new System.EventHandler(this.btn_fun_Click);
            // 
            // btn_ooo
            // 
            this.btn_ooo.BackColor = System.Drawing.Color.Purple;
            this.btn_ooo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ooo.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_ooo.Location = new System.Drawing.Point(237, 215);
            this.btn_ooo.Name = "btn_ooo";
            this.btn_ooo.Size = new System.Drawing.Size(200, 49);
            this.btn_ooo.TabIndex = 24;
            this.btn_ooo.Text = "Out of Order (OOO)";
            this.btn_ooo.UseVisualStyleBackColor = false;
            this.btn_ooo.Click += new System.EventHandler(this.btn_ooo_Click);
            // 
            // btn_vc
            // 
            this.btn_vc.BackColor = System.Drawing.Color.Green;
            this.btn_vc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_vc.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_vc.Location = new System.Drawing.Point(29, 160);
            this.btn_vc.Name = "btn_vc";
            this.btn_vc.Size = new System.Drawing.Size(200, 49);
            this.btn_vc.TabIndex = 23;
            this.btn_vc.Text = "Vacant Clean (VC)";
            this.btn_vc.UseVisualStyleBackColor = false;
            this.btn_vc.Click += new System.EventHandler(this.btn_vc_Click);
            // 
            // btn_vd
            // 
            this.btn_vd.BackColor = System.Drawing.Color.Red;
            this.btn_vd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_vd.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_vd.Location = new System.Drawing.Point(237, 160);
            this.btn_vd.Name = "btn_vd";
            this.btn_vd.Size = new System.Drawing.Size(200, 49);
            this.btn_vd.TabIndex = 22;
            this.btn_vd.Text = "Vacant Dirty (VD)";
            this.btn_vd.UseVisualStyleBackColor = false;
            this.btn_vd.Click += new System.EventHandler(this.btn_vd_Click);
            // 
            // btn_occ
            // 
            this.btn_occ.BackColor = System.Drawing.Color.DarkBlue;
            this.btn_occ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_occ.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_occ.Location = new System.Drawing.Point(29, 215);
            this.btn_occ.Name = "btn_occ";
            this.btn_occ.Size = new System.Drawing.Size(200, 49);
            this.btn_occ.TabIndex = 21;
            this.btn_occ.Text = "Occupied (OCC)";
            this.btn_occ.UseVisualStyleBackColor = false;
            this.btn_occ.Click += new System.EventHandler(this.btn_occ_Click);
            // 
            // lbl_gfolio
            // 
            this.lbl_gfolio.AutoSize = true;
            this.lbl_gfolio.Location = new System.Drawing.Point(26, 44);
            this.lbl_gfolio.Name = "lbl_gfolio";
            this.lbl_gfolio.Size = new System.Drawing.Size(0, 16);
            this.lbl_gfolio.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tennant Folio :";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.lbl_rm);
            this.panel2.ForeColor = System.Drawing.Color.SeaGreen;
            this.panel2.Location = new System.Drawing.Point(209, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 75);
            this.panel2.TabIndex = 20;
            // 
            // lbl_rm
            // 
            this.lbl_rm.AutoSize = true;
            this.lbl_rm.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_rm.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rm.ForeColor = System.Drawing.Color.Green;
            this.lbl_rm.Location = new System.Drawing.Point(242, 0);
            this.lbl_rm.Name = "lbl_rm";
            this.lbl_rm.Size = new System.Drawing.Size(0, 73);
            this.lbl_rm.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_romstatus);
            this.groupBox1.Controls.Add(this.panel64);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(483, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 742);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unit Status";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dgv_romstatus
            // 
            this.dgv_romstatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_romstatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_romstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_romstatus.Location = new System.Drawing.Point(3, 84);
            this.dgv_romstatus.MultiSelect = false;
            this.dgv_romstatus.Name = "dgv_romstatus";
            this.dgv_romstatus.ReadOnly = true;
            this.dgv_romstatus.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_romstatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_romstatus.Size = new System.Drawing.Size(539, 655);
            this.dgv_romstatus.TabIndex = 19;
            this.dgv_romstatus.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_romstatus_CellDoubleClick);
            this.dgv_romstatus.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_romstatus_CellContentClick);
            this.dgv_romstatus.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_romstatus_CellPainting);
            // 
            // panel64
            // 
            this.panel64.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel64.Controls.Add(this.cbo_rmtyp);
            this.panel64.Controls.Add(this.label2);
            this.panel64.Controls.Add(this.cbo_rmstatus);
            this.panel64.Controls.Add(this.label1);
            this.panel64.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel64.Location = new System.Drawing.Point(3, 18);
            this.panel64.Name = "panel64";
            this.panel64.Size = new System.Drawing.Size(539, 66);
            this.panel64.TabIndex = 18;
            // 
            // cbo_rmtyp
            // 
            this.cbo_rmtyp.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_rmtyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rmtyp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_rmtyp.FormattingEnabled = true;
            this.cbo_rmtyp.Location = new System.Drawing.Point(312, 21);
            this.cbo_rmtyp.Name = "cbo_rmtyp";
            this.cbo_rmtyp.Size = new System.Drawing.Size(218, 24);
            this.cbo_rmtyp.TabIndex = 3;
            this.cbo_rmtyp.SelectedIndexChanged += new System.EventHandler(this.cbo_rmtyp_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(240, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Unit Type";
            // 
            // cbo_rmstatus
            // 
            this.cbo_rmstatus.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_rmstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rmstatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_rmstatus.FormattingEnabled = true;
            this.cbo_rmstatus.Items.AddRange(new object[] {
            "Vacant Clean",
            "Vacant Dirty",
            "Occupied",
            "Out Of Order",
            "Function Room",
            "Office"});
            this.cbo_rmstatus.Location = new System.Drawing.Point(92, 21);
            this.cbo_rmstatus.Name = "cbo_rmstatus";
            this.cbo_rmstatus.Size = new System.Drawing.Size(125, 24);
            this.cbo_rmstatus.TabIndex = 1;
            this.cbo_rmstatus.SelectedIndexChanged += new System.EventHandler(this.cbo_rmstatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Unit Status";
            // 
            // UpdateRoomStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1028, 742);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateRoomStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateRoomStatus";
            this.Load += new System.EventHandler(this.UpdateRoomStatus_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ooohistory)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_romstatus)).EndInit();
            this.panel64.ResumeLayout(false);
            this.panel64.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_gfolio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_rm;
        private System.Windows.Forms.Button btn_fun;
        private System.Windows.Forms.Button btn_ooo;
        private System.Windows.Forms.Button btn_vc;
        private System.Windows.Forms.Button btn_vd;
        private System.Windows.Forms.Button btn_occ;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_romstatus;
        private System.Windows.Forms.ComboBox cbo_rmtyp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_rmstatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_gfname;
        private System.Windows.Forms.Label lbl_gfno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgv_ooohistory;
        private System.Windows.Forms.Label lbl_depdate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_arrdate;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel64;
        private System.Windows.Forms.Button button1;
    }
}