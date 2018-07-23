namespace Hotel_System
{
    partial class RoomNumber
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_rtpgno = new System.Windows.Forms.Label();
            this.btn_rtnext = new System.Windows.Forms.Button();
            this.btn_rtprev = new System.Windows.Forms.Button();
            this.rtxt_rtdesc = new System.Windows.Forms.RichTextBox();
            this.btn_rtcancel = new System.Windows.Forms.Button();
            this.btn_rtsave = new System.Windows.Forms.Button();
            this.btn_rtedit = new System.Windows.Forms.Button();
            this.btn_rtnew = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dgv_rtlist = new System.Windows.Forms.DataGridView();
            this.dgv_list_room_type_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_list_room_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_rtid = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_rnpgno = new System.Windows.Forms.Label();
            this.btn_rnnext = new System.Windows.Forms.Button();
            this.btn_rnprev = new System.Windows.Forms.Button();
            this.rtxt_rndesc = new System.Windows.Forms.RichTextBox();
            this.btn_rncancel = new System.Windows.Forms.Button();
            this.btn_rnsave = new System.Windows.Forms.Button();
            this.btn_rnedit = new System.Windows.Forms.Button();
            this.btn_rnnew = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dgv_rnlist = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_rt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_rnid = new System.Windows.Forms.TextBox();
            this.rom_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rom_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typ_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_type_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rtlist)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rnlist)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.SeaGreen;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Info;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(852, 512);
            this.splitContainer1.SplitterDistance = 421;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkKhaki;
            this.groupBox1.Controls.Add(this.lbl_rtpgno);
            this.groupBox1.Controls.Add(this.btn_rtnext);
            this.groupBox1.Controls.Add(this.btn_rtprev);
            this.groupBox1.Controls.Add(this.rtxt_rtdesc);
            this.groupBox1.Controls.Add(this.btn_rtcancel);
            this.groupBox1.Controls.Add(this.btn_rtsave);
            this.groupBox1.Controls.Add(this.btn_rtedit);
            this.groupBox1.Controls.Add(this.btn_rtnew);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dgv_rtlist);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_rtid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 512);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unit Types";
            // 
            // lbl_rtpgno
            // 
            this.lbl_rtpgno.AutoSize = true;
            this.lbl_rtpgno.Location = new System.Drawing.Point(355, 152);
            this.lbl_rtpgno.Name = "lbl_rtpgno";
            this.lbl_rtpgno.Size = new System.Drawing.Size(13, 13);
            this.lbl_rtpgno.TabIndex = 47;
            this.lbl_rtpgno.Text = "0";
            // 
            // btn_rtnext
            // 
            this.btn_rtnext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtnext.Location = new System.Drawing.Point(374, 144);
            this.btn_rtnext.Name = "btn_rtnext";
            this.btn_rtnext.Size = new System.Drawing.Size(31, 25);
            this.btn_rtnext.TabIndex = 46;
            this.btn_rtnext.Text = ">";
            this.btn_rtnext.UseVisualStyleBackColor = true;
            this.btn_rtnext.Click += new System.EventHandler(this.btn_rtnext_Click);
            // 
            // btn_rtprev
            // 
            this.btn_rtprev.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtprev.Location = new System.Drawing.Point(318, 144);
            this.btn_rtprev.Name = "btn_rtprev";
            this.btn_rtprev.Size = new System.Drawing.Size(31, 25);
            this.btn_rtprev.TabIndex = 45;
            this.btn_rtprev.Text = "<";
            this.btn_rtprev.UseVisualStyleBackColor = true;
            this.btn_rtprev.Click += new System.EventHandler(this.btn_rtprev_Click);
            // 
            // rtxt_rtdesc
            // 
            this.rtxt_rtdesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rtxt_rtdesc.Location = new System.Drawing.Point(91, 45);
            this.rtxt_rtdesc.Name = "rtxt_rtdesc";
            this.rtxt_rtdesc.Size = new System.Drawing.Size(201, 74);
            this.rtxt_rtdesc.TabIndex = 44;
            this.rtxt_rtdesc.Text = "";
            // 
            // btn_rtcancel
            // 
            this.btn_rtcancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtcancel.Location = new System.Drawing.Point(298, 91);
            this.btn_rtcancel.Name = "btn_rtcancel";
            this.btn_rtcancel.Size = new System.Drawing.Size(107, 24);
            this.btn_rtcancel.TabIndex = 43;
            this.btn_rtcancel.Text = "Cancel";
            this.btn_rtcancel.UseVisualStyleBackColor = true;
            this.btn_rtcancel.Click += new System.EventHandler(this.btn_rtcancel_Click);
            // 
            // btn_rtsave
            // 
            this.btn_rtsave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtsave.Location = new System.Drawing.Point(298, 64);
            this.btn_rtsave.Name = "btn_rtsave";
            this.btn_rtsave.Size = new System.Drawing.Size(107, 24);
            this.btn_rtsave.TabIndex = 42;
            this.btn_rtsave.Text = "Save";
            this.btn_rtsave.UseVisualStyleBackColor = true;
            this.btn_rtsave.Click += new System.EventHandler(this.btn_rtsave_Click);
            // 
            // btn_rtedit
            // 
            this.btn_rtedit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtedit.Location = new System.Drawing.Point(298, 38);
            this.btn_rtedit.Name = "btn_rtedit";
            this.btn_rtedit.Size = new System.Drawing.Size(107, 24);
            this.btn_rtedit.TabIndex = 41;
            this.btn_rtedit.Text = "Edit";
            this.btn_rtedit.UseVisualStyleBackColor = true;
            this.btn_rtedit.Click += new System.EventHandler(this.btn_rtedit_Click);
            // 
            // btn_rtnew
            // 
            this.btn_rtnew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtnew.Location = new System.Drawing.Point(298, 13);
            this.btn_rtnew.Name = "btn_rtnew";
            this.btn_rtnew.Size = new System.Drawing.Size(107, 24);
            this.btn_rtnew.TabIndex = 40;
            this.btn_rtnew.Text = "New";
            this.btn_rtnew.UseVisualStyleBackColor = true;
            this.btn_rtnew.Click += new System.EventHandler(this.btn_rtnew_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Unit Type List";
            // 
            // dgv_rtlist
            // 
            this.dgv_rtlist.AllowUserToAddRows = false;
            this.dgv_rtlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rtlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_list_room_type_id,
            this.dgv_list_room_desc});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_rtlist.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_rtlist.Location = new System.Drawing.Point(13, 172);
            this.dgv_rtlist.Name = "dgv_rtlist";
            this.dgv_rtlist.ReadOnly = true;
            this.dgv_rtlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_rtlist.Size = new System.Drawing.Size(392, 328);
            this.dgv_rtlist.TabIndex = 8;
            this.dgv_rtlist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_rtlist_CellPainting);
            // 
            // dgv_list_room_type_id
            // 
            this.dgv_list_room_type_id.DataPropertyName = "typ_code";
            this.dgv_list_room_type_id.HeaderText = "Unit Type #";
            this.dgv_list_room_type_id.MinimumWidth = 50;
            this.dgv_list_room_type_id.Name = "dgv_list_room_type_id";
            this.dgv_list_room_type_id.ReadOnly = true;
            this.dgv_list_room_type_id.Width = 110;
            // 
            // dgv_list_room_desc
            // 
            this.dgv_list_room_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_list_room_desc.DataPropertyName = "typ_desc";
            this.dgv_list_room_desc.HeaderText = "Unit Description";
            this.dgv_list_room_desc.Name = "dgv_list_room_desc";
            this.dgv_list_room_desc.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Unit Type ID";
            // 
            // txt_rtid
            // 
            this.txt_rtid.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_rtid.Location = new System.Drawing.Point(91, 19);
            this.txt_rtid.Name = "txt_rtid";
            this.txt_rtid.Size = new System.Drawing.Size(201, 20);
            this.txt_rtid.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_rnpgno);
            this.groupBox2.Controls.Add(this.btn_rnnext);
            this.groupBox2.Controls.Add(this.btn_rnprev);
            this.groupBox2.Controls.Add(this.rtxt_rndesc);
            this.groupBox2.Controls.Add(this.btn_rncancel);
            this.groupBox2.Controls.Add(this.btn_rnsave);
            this.groupBox2.Controls.Add(this.btn_rnedit);
            this.groupBox2.Controls.Add(this.btn_rnnew);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.dgv_rnlist);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbo_rt);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txt_rnid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 512);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unit";
            // 
            // lbl_rnpgno
            // 
            this.lbl_rnpgno.AutoSize = true;
            this.lbl_rnpgno.Location = new System.Drawing.Point(361, 180);
            this.lbl_rnpgno.Name = "lbl_rnpgno";
            this.lbl_rnpgno.Size = new System.Drawing.Size(13, 13);
            this.lbl_rnpgno.TabIndex = 51;
            this.lbl_rnpgno.Text = "0";
            // 
            // btn_rnnext
            // 
            this.btn_rnnext.Location = new System.Drawing.Point(380, 172);
            this.btn_rnnext.Name = "btn_rnnext";
            this.btn_rnnext.Size = new System.Drawing.Size(31, 25);
            this.btn_rnnext.TabIndex = 50;
            this.btn_rnnext.Text = ">";
            this.btn_rnnext.UseVisualStyleBackColor = true;
            // 
            // btn_rnprev
            // 
            this.btn_rnprev.Location = new System.Drawing.Point(324, 172);
            this.btn_rnprev.Name = "btn_rnprev";
            this.btn_rnprev.Size = new System.Drawing.Size(31, 25);
            this.btn_rnprev.TabIndex = 49;
            this.btn_rnprev.Text = "<";
            this.btn_rnprev.UseVisualStyleBackColor = true;
            // 
            // rtxt_rndesc
            // 
            this.rtxt_rndesc.Location = new System.Drawing.Point(94, 45);
            this.rtxt_rndesc.Name = "rtxt_rndesc";
            this.rtxt_rndesc.Size = new System.Drawing.Size(204, 74);
            this.rtxt_rndesc.TabIndex = 48;
            this.rtxt_rndesc.Text = "";
            // 
            // btn_rncancel
            // 
            this.btn_rncancel.Location = new System.Drawing.Point(304, 93);
            this.btn_rncancel.Name = "btn_rncancel";
            this.btn_rncancel.Size = new System.Drawing.Size(107, 24);
            this.btn_rncancel.TabIndex = 47;
            this.btn_rncancel.Text = "Cancel";
            this.btn_rncancel.UseVisualStyleBackColor = true;
            this.btn_rncancel.Click += new System.EventHandler(this.btn_rncancel_Click_1);
            // 
            // btn_rnsave
            // 
            this.btn_rnsave.Location = new System.Drawing.Point(304, 66);
            this.btn_rnsave.Name = "btn_rnsave";
            this.btn_rnsave.Size = new System.Drawing.Size(107, 24);
            this.btn_rnsave.TabIndex = 46;
            this.btn_rnsave.Text = "Save";
            this.btn_rnsave.UseVisualStyleBackColor = true;
            this.btn_rnsave.Click += new System.EventHandler(this.btn_rnsave_Click);
            // 
            // btn_rnedit
            // 
            this.btn_rnedit.Location = new System.Drawing.Point(304, 40);
            this.btn_rnedit.Name = "btn_rnedit";
            this.btn_rnedit.Size = new System.Drawing.Size(107, 24);
            this.btn_rnedit.TabIndex = 45;
            this.btn_rnedit.Text = "Edit";
            this.btn_rnedit.UseVisualStyleBackColor = true;
            this.btn_rnedit.Click += new System.EventHandler(this.btn_rnedit_Click);
            // 
            // btn_rnnew
            // 
            this.btn_rnnew.Location = new System.Drawing.Point(304, 15);
            this.btn_rnnew.Name = "btn_rnnew";
            this.btn_rnnew.Size = new System.Drawing.Size(107, 24);
            this.btn_rnnew.TabIndex = 44;
            this.btn_rnnew.Text = "New";
            this.btn_rnnew.UseVisualStyleBackColor = true;
            this.btn_rnnew.Click += new System.EventHandler(this.btn_rnnew_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Unit Number  List";
            // 
            // dgv_rnlist
            // 
            this.dgv_rnlist.AllowUserToAddRows = false;
            this.dgv_rnlist.AllowUserToDeleteRows = false;
            this.dgv_rnlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rnlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rom_code,
            this.rom_desc,
            this.typ_desc,
            this.dgv_type_code});
            this.dgv_rnlist.Location = new System.Drawing.Point(16, 204);
            this.dgv_rnlist.Name = "dgv_rnlist";
            this.dgv_rnlist.ReadOnly = true;
            this.dgv_rnlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_rnlist.Size = new System.Drawing.Size(395, 296);
            this.dgv_rnlist.TabIndex = 8;
            this.dgv_rnlist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_rnlist_CellPainting);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Unit Types";
            // 
            // cbo_rt
            // 
            this.cbo_rt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rt.FormattingEnabled = true;
            this.cbo_rt.Location = new System.Drawing.Point(94, 125);
            this.cbo_rt.Name = "cbo_rt";
            this.cbo_rt.Size = new System.Drawing.Size(204, 21);
            this.cbo_rt.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Unit Number";
            // 
            // txt_rnid
            // 
            this.txt_rnid.Location = new System.Drawing.Point(94, 19);
            this.txt_rnid.Name = "txt_rnid";
            this.txt_rnid.Size = new System.Drawing.Size(204, 20);
            this.txt_rnid.TabIndex = 0;
            // 
            // rom_code
            // 
            this.rom_code.DataPropertyName = "rom_code";
            this.rom_code.HeaderText = "Unit Code";
            this.rom_code.Name = "rom_code";
            this.rom_code.ReadOnly = true;
            // 
            // rom_desc
            // 
            this.rom_desc.DataPropertyName = "rom_desc";
            this.rom_desc.HeaderText = "Unit Description";
            this.rom_desc.Name = "rom_desc";
            this.rom_desc.ReadOnly = true;
            this.rom_desc.Width = 125;
            // 
            // typ_desc
            // 
            this.typ_desc.DataPropertyName = "typ_desc";
            this.typ_desc.HeaderText = "Type Desc";
            this.typ_desc.Name = "typ_desc";
            this.typ_desc.ReadOnly = true;
            this.typ_desc.Width = 140;
            // 
            // dgv_type_code
            // 
            this.dgv_type_code.DataPropertyName = "typ_code";
            this.dgv_type_code.HeaderText = "Type Code";
            this.dgv_type_code.Name = "dgv_type_code";
            this.dgv_type_code.ReadOnly = true;
            // 
            // RoomNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 512);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoomNumber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unit Number";
            this.Load += new System.EventHandler(this.RoomNumber_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rtlist)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rnlist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_rt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_rnid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_rtid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgv_rtlist;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgv_rnlist;
        private System.Windows.Forms.Button btn_rtcancel;
        private System.Windows.Forms.Button btn_rtsave;
        private System.Windows.Forms.Button btn_rtedit;
        private System.Windows.Forms.Button btn_rtnew;
        private System.Windows.Forms.RichTextBox rtxt_rtdesc;
        private System.Windows.Forms.RichTextBox rtxt_rndesc;
        private System.Windows.Forms.Button btn_rncancel;
        private System.Windows.Forms.Button btn_rnsave;
        private System.Windows.Forms.Button btn_rnedit;
        private System.Windows.Forms.Button btn_rnnew;
        private System.Windows.Forms.Label lbl_rtpgno;
        private System.Windows.Forms.Button btn_rtnext;
        private System.Windows.Forms.Button btn_rtprev;
        private System.Windows.Forms.Label lbl_rnpgno;
        private System.Windows.Forms.Button btn_rnnext;
        private System.Windows.Forms.Button btn_rnprev;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_list_room_type_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_list_room_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rom_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn rom_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn typ_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_type_code;
    }
}