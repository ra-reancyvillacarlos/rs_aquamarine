namespace Hotel_System
{
    partial class RoomRate
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_rrrt = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_rrcancel = new System.Windows.Forms.Button();
            this.btn_rrsave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dgv_rrlist = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rrlist)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Unit Types";
            // 
            // cbo_rrrt
            // 
            this.cbo_rrrt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_rrrt.FormattingEnabled = true;
            this.cbo_rrrt.Location = new System.Drawing.Point(86, 15);
            this.cbo_rrrt.Name = "cbo_rrrt";
            this.cbo_rrrt.Size = new System.Drawing.Size(318, 21);
            this.cbo_rrrt.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_rrcancel);
            this.groupBox2.Controls.Add(this.btn_rrsave);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.dgv_rrlist);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbo_rrrt);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(416, 512);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unit Rates Table";
            // 
            // btn_rrcancel
            // 
            this.btn_rrcancel.Image = global::Hotel_System.Properties.Resources._1343907586_Cancel;
            this.btn_rrcancel.Location = new System.Drawing.Point(184, 56);
            this.btn_rrcancel.Name = "btn_rrcancel";
            this.btn_rrcancel.Size = new System.Drawing.Size(107, 51);
            this.btn_rrcancel.TabIndex = 47;
            this.btn_rrcancel.Text = "Cancel";
            this.btn_rrcancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_rrcancel.UseVisualStyleBackColor = true;
            // 
            // btn_rrsave
            // 
            this.btn_rrsave.Image = global::Hotel_System.Properties.Resources._1343908142_database_save;
            this.btn_rrsave.Location = new System.Drawing.Point(297, 56);
            this.btn_rrsave.Name = "btn_rrsave";
            this.btn_rrsave.Size = new System.Drawing.Size(107, 51);
            this.btn_rrsave.TabIndex = 46;
            this.btn_rrsave.Text = "Save";
            this.btn_rrsave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_rrsave.UseVisualStyleBackColor = true;
            this.btn_rrsave.Click += new System.EventHandler(this.btn_rrsave_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Unit Number  List";
            // 
            // dgv_rrlist
            // 
            this.dgv_rrlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rrlist.Location = new System.Drawing.Point(16, 127);
            this.dgv_rrlist.Name = "dgv_rrlist";
            this.dgv_rrlist.Size = new System.Drawing.Size(388, 373);
            this.dgv_rrlist.TabIndex = 8;
            this.dgv_rrlist.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_rrlist_CellPainting);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Info;
            this.splitContainer1.Size = new System.Drawing.Size(884, 512);
            this.splitContainer1.SplitterDistance = 436;
            this.splitContainer1.TabIndex = 1;
            // 
            // RoomRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 512);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoomRate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unit Rate";
            this.Load += new System.EventHandler(this.RoomRate_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rrlist)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_rrrt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgv_rrlist;
        private System.Windows.Forms.Button btn_rrcancel;
        private System.Windows.Forms.Button btn_rrsave;
    }
}