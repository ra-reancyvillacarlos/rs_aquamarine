namespace Hotel_System
{
    partial class Housekeeping
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_hicancel = new System.Windows.Forms.Button();
            this.btn_hisave = new System.Windows.Forms.Button();
            this.btn_hiedit = new System.Windows.Forms.Button();
            this.btn_hinew = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.dgv_hilist = new System.Windows.Forms.DataGridView();
            this.hkitem_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hkitem_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_hidesc = new System.Windows.Forms.TextBox();
            this.txt_hicode = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_racancel = new System.Windows.Forms.Button();
            this.btn_rasave = new System.Windows.Forms.Button();
            this.btn_raedit = new System.Windows.Forms.Button();
            this.btn_ranew = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dgv_ralist = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_raname = new System.Windows.Forms.TextBox();
            this.txt_racode = new System.Windows.Forms.TextBox();
            this.ra_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ra_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_hilist)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ralist)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Info;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1004, 708);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.DarkKhaki;
            this.groupBox5.Controls.Add(this.btn_hicancel);
            this.groupBox5.Controls.Add(this.btn_hisave);
            this.groupBox5.Controls.Add(this.btn_hiedit);
            this.groupBox5.Controls.Add(this.btn_hinew);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.dgv_hilist);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txt_hidesc);
            this.groupBox5.Controls.Add(this.txt_hicode);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(500, 708);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Housekeeping Item";
            // 
            // btn_hicancel
            // 
            this.btn_hicancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_hicancel.Location = new System.Drawing.Point(339, 103);
            this.btn_hicancel.Name = "btn_hicancel";
            this.btn_hicancel.Size = new System.Drawing.Size(95, 27);
            this.btn_hicancel.TabIndex = 19;
            this.btn_hicancel.Text = "Cancel";
            this.btn_hicancel.UseVisualStyleBackColor = true;
            this.btn_hicancel.Click += new System.EventHandler(this.btn_hicancel_Click);
            // 
            // btn_hisave
            // 
            this.btn_hisave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_hisave.Location = new System.Drawing.Point(339, 73);
            this.btn_hisave.Name = "btn_hisave";
            this.btn_hisave.Size = new System.Drawing.Size(95, 27);
            this.btn_hisave.TabIndex = 18;
            this.btn_hisave.Text = "Save";
            this.btn_hisave.UseVisualStyleBackColor = true;
            this.btn_hisave.Click += new System.EventHandler(this.btn_hisave_Click);
            // 
            // btn_hiedit
            // 
            this.btn_hiedit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_hiedit.Location = new System.Drawing.Point(339, 44);
            this.btn_hiedit.Name = "btn_hiedit";
            this.btn_hiedit.Size = new System.Drawing.Size(95, 27);
            this.btn_hiedit.TabIndex = 17;
            this.btn_hiedit.Text = "Edit";
            this.btn_hiedit.UseVisualStyleBackColor = true;
            this.btn_hiedit.Click += new System.EventHandler(this.btn_hiedit_Click);
            // 
            // btn_hinew
            // 
            this.btn_hinew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_hinew.Location = new System.Drawing.Point(339, 15);
            this.btn_hinew.Name = "btn_hinew";
            this.btn_hinew.Size = new System.Drawing.Size(95, 27);
            this.btn_hinew.TabIndex = 16;
            this.btn_hinew.Text = "New";
            this.btn_hinew.UseVisualStyleBackColor = true;
            this.btn_hinew.Click += new System.EventHandler(this.btn_hinew_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 125);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(118, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Housekeeping Item List";
            // 
            // dgv_hilist
            // 
            this.dgv_hilist.AllowUserToAddRows = false;
            this.dgv_hilist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_hilist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hkitem_code,
            this.hkitem_desc});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_hilist.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_hilist.Location = new System.Drawing.Point(11, 141);
            this.dgv_hilist.Name = "dgv_hilist";
            this.dgv_hilist.ReadOnly = true;
            this.dgv_hilist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_hilist.Size = new System.Drawing.Size(423, 359);
            this.dgv_hilist.TabIndex = 10;
            this.dgv_hilist.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_hilist_CellDoubleClick);
            this.dgv_hilist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_hilist_CellPainting);
            // 
            // hkitem_code
            // 
            this.hkitem_code.DataPropertyName = "hkitem_code";
            this.hkitem_code.HeaderText = "HKeeping Item Code";
            this.hkitem_code.Name = "hkitem_code";
            this.hkitem_code.ReadOnly = true;
            // 
            // hkitem_desc
            // 
            this.hkitem_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.hkitem_desc.DataPropertyName = "hkitem_desc";
            this.hkitem_desc.HeaderText = "HKeeping Description";
            this.hkitem_desc.Name = "hkitem_desc";
            this.hkitem_desc.ReadOnly = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Description";
            // 
            // txt_hidesc
            // 
            this.txt_hidesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_hidesc.Location = new System.Drawing.Point(88, 26);
            this.txt_hidesc.Name = "txt_hidesc";
            this.txt_hidesc.Size = new System.Drawing.Size(197, 20);
            this.txt_hidesc.TabIndex = 2;
            // 
            // txt_hicode
            // 
            this.txt_hicode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_hicode.Location = new System.Drawing.Point(88, 52);
            this.txt_hicode.Name = "txt_hicode";
            this.txt_hicode.Size = new System.Drawing.Size(71, 20);
            this.txt_hicode.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_racancel);
            this.groupBox3.Controls.Add(this.btn_rasave);
            this.groupBox3.Controls.Add(this.btn_raedit);
            this.groupBox3.Controls.Add(this.btn_ranew);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.dgv_ralist);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txt_raname);
            this.groupBox3.Controls.Add(this.txt_racode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(500, 708);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Unit Attendant ";
            // 
            // btn_racancel
            // 
            this.btn_racancel.Location = new System.Drawing.Point(339, 103);
            this.btn_racancel.Name = "btn_racancel";
            this.btn_racancel.Size = new System.Drawing.Size(95, 27);
            this.btn_racancel.TabIndex = 19;
            this.btn_racancel.Text = "Cancel";
            this.btn_racancel.UseVisualStyleBackColor = true;
            this.btn_racancel.Click += new System.EventHandler(this.btn_racancel_Click);
            // 
            // btn_rasave
            // 
            this.btn_rasave.Location = new System.Drawing.Point(339, 73);
            this.btn_rasave.Name = "btn_rasave";
            this.btn_rasave.Size = new System.Drawing.Size(95, 27);
            this.btn_rasave.TabIndex = 18;
            this.btn_rasave.Text = "Save";
            this.btn_rasave.UseVisualStyleBackColor = true;
            this.btn_rasave.Click += new System.EventHandler(this.btn_rasave_Click);
            // 
            // btn_raedit
            // 
            this.btn_raedit.Location = new System.Drawing.Point(339, 44);
            this.btn_raedit.Name = "btn_raedit";
            this.btn_raedit.Size = new System.Drawing.Size(95, 27);
            this.btn_raedit.TabIndex = 17;
            this.btn_raedit.Text = "Edit";
            this.btn_raedit.UseVisualStyleBackColor = true;
            this.btn_raedit.Click += new System.EventHandler(this.btn_raedit_Click);
            // 
            // btn_ranew
            // 
            this.btn_ranew.Location = new System.Drawing.Point(339, 15);
            this.btn_ranew.Name = "btn_ranew";
            this.btn_ranew.Size = new System.Drawing.Size(95, 27);
            this.btn_ranew.TabIndex = 16;
            this.btn_ranew.Text = "New";
            this.btn_ranew.UseVisualStyleBackColor = true;
            this.btn_ranew.Click += new System.EventHandler(this.btn_ranew_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Unit Attendant List";
            // 
            // dgv_ralist
            // 
            this.dgv_ralist.AllowUserToAddRows = false;
            this.dgv_ralist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ralist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ra_code,
            this.ra_name});
            this.dgv_ralist.Location = new System.Drawing.Point(11, 141);
            this.dgv_ralist.Name = "dgv_ralist";
            this.dgv_ralist.ReadOnly = true;
            this.dgv_ralist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ralist.Size = new System.Drawing.Size(423, 359);
            this.dgv_ralist.TabIndex = 10;
            this.dgv_ralist.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ralist_CellDoubleClick);
            this.dgv_ralist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_ralist_CellPainting);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Name";
            // 
            // txt_raname
            // 
            this.txt_raname.Location = new System.Drawing.Point(88, 26);
            this.txt_raname.Name = "txt_raname";
            this.txt_raname.Size = new System.Drawing.Size(197, 20);
            this.txt_raname.TabIndex = 2;
            // 
            // txt_racode
            // 
            this.txt_racode.Location = new System.Drawing.Point(88, 52);
            this.txt_racode.Name = "txt_racode";
            this.txt_racode.Size = new System.Drawing.Size(197, 20);
            this.txt_racode.TabIndex = 0;
            // 
            // ra_code
            // 
            this.ra_code.DataPropertyName = "ra_code";
            this.ra_code.HeaderText = "Unit Attendnt #";
            this.ra_code.Name = "ra_code";
            this.ra_code.ReadOnly = true;
            // 
            // ra_name
            // 
            this.ra_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ra_name.DataPropertyName = "ra_name";
            this.ra_name.HeaderText = "Unit Attendnt Name";
            this.ra_name.Name = "ra_name";
            this.ra_name.ReadOnly = true;
            // 
            // Housekeeping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 708);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Housekeeping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Housekeeping";
            this.Load += new System.EventHandler(this.Housekeeping_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_hilist)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ralist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_hicancel;
        private System.Windows.Forms.Button btn_hisave;
        private System.Windows.Forms.Button btn_hiedit;
        private System.Windows.Forms.Button btn_hinew;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView dgv_hilist;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_hidesc;
        private System.Windows.Forms.TextBox txt_hicode;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_racancel;
        private System.Windows.Forms.Button btn_rasave;
        private System.Windows.Forms.Button btn_raedit;
        private System.Windows.Forms.Button btn_ranew;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgv_ralist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_raname;
        private System.Windows.Forms.TextBox txt_racode;
        private System.Windows.Forms.DataGridViewTextBoxColumn hkitem_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn hkitem_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ra_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn ra_name;
    }
}