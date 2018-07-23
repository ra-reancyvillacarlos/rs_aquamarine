namespace Hotel_System
{
    partial class RoomRates
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_rrcancel = new System.Windows.Forms.Button();
            this.btn_rrupdate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dgv_rrlist = new System.Windows.Forms.DataGridView();
            this.typ_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typ_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.single_occ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.double_occ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.triple_occ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quad_occ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_rrrt = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rrlist)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Controls.Add(this.btn_rrcancel);
            this.groupBox2.Controls.Add(this.btn_rrupdate);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.dgv_rrlist);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbo_rrrt);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(568, 525);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unit Rates Table";
            // 
            // btn_rrcancel
            // 
            this.btn_rrcancel.Image = global::Hotel_System.Properties.Resources._1343907586_Cancel;
            this.btn_rrcancel.Location = new System.Drawing.Point(335, 462);
            this.btn_rrcancel.Name = "btn_rrcancel";
            this.btn_rrcancel.Size = new System.Drawing.Size(107, 51);
            this.btn_rrcancel.TabIndex = 47;
            this.btn_rrcancel.Text = "Close";
            this.btn_rrcancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_rrcancel.UseVisualStyleBackColor = true;
            // 
            // btn_rrupdate
            // 
            this.btn_rrupdate.Image = global::Hotel_System.Properties.Resources._1343908142_database_save;
            this.btn_rrupdate.Location = new System.Drawing.Point(448, 462);
            this.btn_rrupdate.Name = "btn_rrupdate";
            this.btn_rrupdate.Size = new System.Drawing.Size(107, 51);
            this.btn_rrupdate.TabIndex = 46;
            this.btn_rrupdate.Text = "Update";
            this.btn_rrupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_rrupdate.UseVisualStyleBackColor = true;
            this.btn_rrupdate.Click += new System.EventHandler(this.btn_rrsave_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Unit Types";
            // 
            // dgv_rrlist
            // 
            this.dgv_rrlist.AllowUserToAddRows = false;
            this.dgv_rrlist.AllowUserToDeleteRows = false;
            this.dgv_rrlist.AllowUserToOrderColumns = true;
            this.dgv_rrlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rrlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typ_code,
            this.typ_desc,
            this.single_occ,
            this.double_occ,
            this.triple_occ,
            this.quad_occ});
            this.dgv_rrlist.Location = new System.Drawing.Point(16, 85);
            this.dgv_rrlist.Name = "dgv_rrlist";
            this.dgv_rrlist.ReadOnly = true;
            this.dgv_rrlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_rrlist.Size = new System.Drawing.Size(540, 360);
            this.dgv_rrlist.TabIndex = 8;
            this.dgv_rrlist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_rrlist_CellPainting);
            // 
            // typ_code
            // 
            this.typ_code.DataPropertyName = "typ_code";
            this.typ_code.HeaderText = "Unit Type Code";
            this.typ_code.Name = "typ_code";
            this.typ_code.ReadOnly = true;
            this.typ_code.Width = 110;
            // 
            // typ_desc
            // 
            this.typ_desc.DataPropertyName = "typ_desc";
            this.typ_desc.HeaderText = "Unit Type Desc";
            this.typ_desc.Name = "typ_desc";
            this.typ_desc.ReadOnly = true;
            this.typ_desc.Width = 125;
            // 
            // single_occ
            // 
            this.single_occ.DataPropertyName = "single";
            this.single_occ.HeaderText = "Single";
            this.single_occ.Name = "single_occ";
            this.single_occ.ReadOnly = true;
            // 
            // double_occ
            // 
            this.double_occ.DataPropertyName = "double";
            this.double_occ.HeaderText = "Double";
            this.double_occ.Name = "double_occ";
            this.double_occ.ReadOnly = true;
            // 
            // triple_occ
            // 
            this.triple_occ.DataPropertyName = "triple";
            this.triple_occ.HeaderText = "Triple";
            this.triple_occ.Name = "triple_occ";
            this.triple_occ.ReadOnly = true;
            // 
            // quad_occ
            // 
            this.quad_occ.DataPropertyName = "quad";
            this.quad_occ.HeaderText = "Quad";
            this.quad_occ.Name = "quad_occ";
            this.quad_occ.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Rate Types";
            // 
            // cbo_rrrt
            // 
            this.cbo_rrrt.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_rrrt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rrrt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_rrrt.FormattingEnabled = true;
            this.cbo_rrrt.Location = new System.Drawing.Point(87, 30);
            this.cbo_rrrt.Name = "cbo_rrrt";
            this.cbo_rrrt.Size = new System.Drawing.Size(318, 21);
            this.cbo_rrrt.TabIndex = 4;
            this.cbo_rrrt.SelectedIndexChanged += new System.EventHandler(this.cbo_rrrt_SelectedIndexChanged);
            // 
            // RoomRates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkKhaki;
            this.ClientSize = new System.Drawing.Size(568, 525);
            this.Controls.Add(this.groupBox2);
            this.Name = "RoomRates";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unit Rates";
            this.Load += new System.EventHandler(this.RoomRates_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rrlist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_rrcancel;
        private System.Windows.Forms.Button btn_rrupdate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_rrrt;
        public System.Windows.Forms.DataGridView dgv_rrlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn typ_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn typ_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn single_occ;
        private System.Windows.Forms.DataGridViewTextBoxColumn double_occ;
        private System.Windows.Forms.DataGridViewTextBoxColumn triple_occ;
        private System.Windows.Forms.DataGridViewTextBoxColumn quad_occ;

    }
}