namespace Hotel_System
{
    partial class m_RoomType
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rtlist)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_cancel);
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
            this.groupBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 512);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unit Types";
            // 
            // btn_cancel
            // 
            this.btn_cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_cancel.Location = new System.Drawing.Point(422, 95);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(107, 24);
            this.btn_cancel.TabIndex = 48;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // lbl_rtpgno
            // 
            this.lbl_rtpgno.AutoSize = true;
            this.lbl_rtpgno.Location = new System.Drawing.Point(479, 149);
            this.lbl_rtpgno.Name = "lbl_rtpgno";
            this.lbl_rtpgno.Size = new System.Drawing.Size(15, 16);
            this.lbl_rtpgno.TabIndex = 47;
            this.lbl_rtpgno.Text = "0";
            // 
            // btn_rtnext
            // 
            this.btn_rtnext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtnext.Location = new System.Drawing.Point(498, 141);
            this.btn_rtnext.Name = "btn_rtnext";
            this.btn_rtnext.Size = new System.Drawing.Size(31, 25);
            this.btn_rtnext.TabIndex = 46;
            this.btn_rtnext.Text = ">";
            this.btn_rtnext.UseVisualStyleBackColor = true;
            // 
            // btn_rtprev
            // 
            this.btn_rtprev.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtprev.Location = new System.Drawing.Point(442, 141);
            this.btn_rtprev.Name = "btn_rtprev";
            this.btn_rtprev.Size = new System.Drawing.Size(31, 25);
            this.btn_rtprev.TabIndex = 45;
            this.btn_rtprev.Text = "<";
            this.btn_rtprev.UseVisualStyleBackColor = true;
            // 
            // rtxt_rtdesc
            // 
            this.rtxt_rtdesc.AcceptsTab = true;
            this.rtxt_rtdesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rtxt_rtdesc.Location = new System.Drawing.Point(139, 42);
            this.rtxt_rtdesc.Name = "rtxt_rtdesc";
            this.rtxt_rtdesc.Size = new System.Drawing.Size(237, 74);
            this.rtxt_rtdesc.TabIndex = 44;
            this.rtxt_rtdesc.Text = "";
            // 
            // btn_rtcancel
            // 
            this.btn_rtcancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtcancel.Location = new System.Drawing.Point(1261, 121);
            this.btn_rtcancel.Name = "btn_rtcancel";
            this.btn_rtcancel.Size = new System.Drawing.Size(107, 24);
            this.btn_rtcancel.TabIndex = 43;
            this.btn_rtcancel.Text = "Cancel";
            this.btn_rtcancel.UseVisualStyleBackColor = true;
            this.btn_rtcancel.Click += new System.EventHandler(this.btn_rtcancel_Click_1);
            // 
            // btn_rtsave
            // 
            this.btn_rtsave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtsave.Location = new System.Drawing.Point(422, 65);
            this.btn_rtsave.Name = "btn_rtsave";
            this.btn_rtsave.Size = new System.Drawing.Size(107, 24);
            this.btn_rtsave.TabIndex = 42;
            this.btn_rtsave.Text = "Save";
            this.btn_rtsave.UseVisualStyleBackColor = true;
            this.btn_rtsave.Click += new System.EventHandler(this.btn_rtsave_Click_1);
            // 
            // btn_rtedit
            // 
            this.btn_rtedit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtedit.Location = new System.Drawing.Point(422, 39);
            this.btn_rtedit.Name = "btn_rtedit";
            this.btn_rtedit.Size = new System.Drawing.Size(107, 24);
            this.btn_rtedit.TabIndex = 41;
            this.btn_rtedit.Text = "Edit";
            this.btn_rtedit.UseVisualStyleBackColor = true;
            this.btn_rtedit.Click += new System.EventHandler(this.btn_rtedit_Click_1);
            // 
            // btn_rtnew
            // 
            this.btn_rtnew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_rtnew.Location = new System.Drawing.Point(422, 14);
            this.btn_rtnew.Name = "btn_rtnew";
            this.btn_rtnew.Size = new System.Drawing.Size(107, 24);
            this.btn_rtnew.TabIndex = 40;
            this.btn_rtnew.Text = "New";
            this.btn_rtnew.UseVisualStyleBackColor = true;
            this.btn_rtnew.Click += new System.EventHandler(this.btn_rtnew_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label6.Location = new System.Drawing.Point(10, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Room Type List";
            // 
            // dgv_rtlist
            // 
            this.dgv_rtlist.AllowUserToAddRows = false;
            this.dgv_rtlist.AllowUserToDeleteRows = false;
            this.dgv_rtlist.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_rtlist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_rtlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rtlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_list_room_type_id,
            this.dgv_list_room_desc});
            this.dgv_rtlist.Location = new System.Drawing.Point(13, 178);
            this.dgv_rtlist.Name = "dgv_rtlist";
            this.dgv_rtlist.ReadOnly = true;
            this.dgv_rtlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_rtlist.Size = new System.Drawing.Size(516, 328);
            this.dgv_rtlist.TabIndex = 8;
            this.dgv_rtlist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_rtlist_CellPainting);
            // 
            // dgv_list_room_type_id
            // 
            this.dgv_list_room_type_id.DataPropertyName = "typ_code";
            this.dgv_list_room_type_id.HeaderText = "Unit Type ID";
            this.dgv_list_room_type_id.MinimumWidth = 78;
            this.dgv_list_room_type_id.Name = "dgv_list_room_type_id";
            this.dgv_list_room_type_id.ReadOnly = true;
            this.dgv_list_room_type_id.Width = 120;
            // 
            // dgv_list_room_desc
            // 
            this.dgv_list_room_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_list_room_desc.DataPropertyName = "typ_desc";
            this.dgv_list_room_desc.HeaderText = "Description";
            this.dgv_list_room_desc.Name = "dgv_list_room_desc";
            this.dgv_list_room_desc.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(10, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label5.Location = new System.Drawing.Point(10, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Unit Type ID";
            // 
            // txt_rtid
            // 
            this.txt_rtid.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_rtid.Location = new System.Drawing.Point(139, 16);
            this.txt_rtid.Name = "txt_rtid";
            this.txt_rtid.Size = new System.Drawing.Size(237, 22);
            this.txt_rtid.TabIndex = 4;
            // 
            // m_RoomType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(594, 537);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "m_RoomType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unit Type";
            this.Load += new System.EventHandler(this.m_RoomType_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rtlist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_rtpgno;
        private System.Windows.Forms.Button btn_rtnext;
        private System.Windows.Forms.Button btn_rtprev;
        private System.Windows.Forms.RichTextBox rtxt_rtdesc;
        private System.Windows.Forms.Button btn_rtcancel;
        private System.Windows.Forms.Button btn_rtsave;
        private System.Windows.Forms.Button btn_rtedit;
        private System.Windows.Forms.Button btn_rtnew;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgv_rtlist;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_rtid;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_list_room_type_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_list_room_desc;
    }
}