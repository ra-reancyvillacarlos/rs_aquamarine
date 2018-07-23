namespace Accounting_Application_System
{
    partial class z_SystemDataUpdate
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpg_send = new System.Windows.Forms.TabPage();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_ping = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.btn_testconect = new System.Windows.Forms.Button();
            this.btn_close1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_server = new System.Windows.Forms.TextBox();
            this.tpg_upload = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt2_fileupload = new System.Windows.Forms.TextBox();
            this.btn2_searchfile = new System.Windows.Forms.Button();
            this.btn2_upload = new System.Windows.Forms.Button();
            this.btn2_close = new System.Windows.Forms.Button();
            this.tpg_download = new System.Windows.Forms.TabPage();
            this.btn3_close = new System.Windows.Forms.Button();
            this.btn3_openfolder = new System.Windows.Forms.Button();
            this.btn3_download = new System.Windows.Forms.Button();
            this.bgworker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tspbar = new System.Windows.Forms.ToolStripProgressBar();
            this.tslbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tpg_send.SuspendLayout();
            this.tpg_upload.SuspendLayout();
            this.tpg_download.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpg_send);
            this.tabControl1.Controls.Add(this.tpg_upload);
            this.tabControl1.Controls.Add(this.tpg_download);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(478, 368);
            this.tabControl1.TabIndex = 0;
            // 
            // tpg_send
            // 
            this.tpg_send.BackColor = System.Drawing.SystemColors.Info;
            this.tpg_send.Controls.Add(this.btn_send);
            this.tpg_send.Controls.Add(this.btn_ping);
            this.tpg_send.Controls.Add(this.label10);
            this.tpg_send.Controls.Add(this.txt_password);
            this.tpg_send.Controls.Add(this.label9);
            this.tpg_send.Controls.Add(this.txt_user);
            this.tpg_send.Controls.Add(this.btn_testconect);
            this.tpg_send.Controls.Add(this.btn_close1);
            this.tpg_send.Controls.Add(this.label8);
            this.tpg_send.Controls.Add(this.txt_server);
            this.tpg_send.Location = new System.Drawing.Point(4, 24);
            this.tpg_send.Name = "tpg_send";
            this.tpg_send.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_send.Size = new System.Drawing.Size(470, 340);
            this.tpg_send.TabIndex = 1;
            this.tpg_send.Text = "Send Data via Internet";
            this.tpg_send.UseVisualStyleBackColor = true;
            // 
            // btn_send
            // 
            this.btn_send.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_send.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_send.Location = new System.Drawing.Point(351, 260);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(111, 49);
            this.btn_send.TabIndex = 22;
            this.btn_send.Text = "Send Data";
            this.btn_send.UseVisualStyleBackColor = false;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_ping
            // 
            this.btn_ping.BackColor = System.Drawing.Color.Peru;
            this.btn_ping.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ping.Location = new System.Drawing.Point(326, 26);
            this.btn_ping.Name = "btn_ping";
            this.btn_ping.Size = new System.Drawing.Size(87, 32);
            this.btn_ping.TabIndex = 21;
            this.btn_ping.Text = "Ping Test";
            this.btn_ping.UseVisualStyleBackColor = false;
            this.btn_ping.Click += new System.EventHandler(this.btn_ping_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 15);
            this.label10.TabIndex = 20;
            this.label10.Text = "Password";
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.Location = new System.Drawing.Point(119, 118);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(162, 22);
            this.txt_password.TabIndex = 19;
            this.txt_password.Text = "Rightech777";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 15);
            this.label9.TabIndex = 18;
            this.label9.Text = "DB User";
            // 
            // txt_user
            // 
            this.txt_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_user.Location = new System.Drawing.Point(119, 90);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(162, 22);
            this.txt_user.TabIndex = 17;
            this.txt_user.Text = "postgres";
            // 
            // btn_testconect
            // 
            this.btn_testconect.BackColor = System.Drawing.Color.Peru;
            this.btn_testconect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_testconect.Location = new System.Drawing.Point(302, 111);
            this.btn_testconect.Name = "btn_testconect";
            this.btn_testconect.Size = new System.Drawing.Size(111, 30);
            this.btn_testconect.TabIndex = 15;
            this.btn_testconect.Text = "Test Connection";
            this.btn_testconect.UseVisualStyleBackColor = false;
            this.btn_testconect.Click += new System.EventHandler(this.btn_testconect_Click);
            // 
            // btn_close1
            // 
            this.btn_close1.BackColor = System.Drawing.Color.Peru;
            this.btn_close1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close1.Location = new System.Drawing.Point(256, 260);
            this.btn_close1.Name = "btn_close1";
            this.btn_close1.Size = new System.Drawing.Size(89, 49);
            this.btn_close1.TabIndex = 16;
            this.btn_close1.Text = "Close";
            this.btn_close1.UseVisualStyleBackColor = false;
            this.btn_close1.Click += new System.EventHandler(this.btn_close1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Intenet IP Address";
            // 
            // txt_server
            // 
            this.txt_server.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_server.Location = new System.Drawing.Point(119, 31);
            this.txt_server.Name = "txt_server";
            this.txt_server.Size = new System.Drawing.Size(162, 22);
            this.txt_server.TabIndex = 13;
            // 
            // tpg_upload
            // 
            this.tpg_upload.BackColor = System.Drawing.SystemColors.Info;
            this.tpg_upload.Controls.Add(this.label5);
            this.tpg_upload.Controls.Add(this.label6);
            this.tpg_upload.Controls.Add(this.label7);
            this.tpg_upload.Controls.Add(this.label4);
            this.tpg_upload.Controls.Add(this.label3);
            this.tpg_upload.Controls.Add(this.label2);
            this.tpg_upload.Controls.Add(this.label1);
            this.tpg_upload.Controls.Add(this.txt2_fileupload);
            this.tpg_upload.Controls.Add(this.btn2_searchfile);
            this.tpg_upload.Controls.Add(this.btn2_upload);
            this.tpg_upload.Controls.Add(this.btn2_close);
            this.tpg_upload.Location = new System.Drawing.Point(4, 24);
            this.tpg_upload.Name = "tpg_upload";
            this.tpg_upload.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_upload.Size = new System.Drawing.Size(470, 340);
            this.tpg_upload.TabIndex = 0;
            this.tpg_upload.Text = "Upload Data From other Locations";
            this.tpg_upload.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(78, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "You cannot longer undo it.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(78, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(350, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "Once you upload the data and successfully save it to the server, ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(23, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Warning";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(62, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "to another locations.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(359, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Select file to upload to update of data that send from one location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Note";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "File Upload";
            // 
            // txt2_fileupload
            // 
            this.txt2_fileupload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt2_fileupload.Location = new System.Drawing.Point(99, 28);
            this.txt2_fileupload.Name = "txt2_fileupload";
            this.txt2_fileupload.Size = new System.Drawing.Size(265, 22);
            this.txt2_fileupload.TabIndex = 11;
            // 
            // btn2_searchfile
            // 
            this.btn2_searchfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2_searchfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2_searchfile.Location = new System.Drawing.Point(370, 28);
            this.btn2_searchfile.Name = "btn2_searchfile";
            this.btn2_searchfile.Size = new System.Drawing.Size(37, 24);
            this.btn2_searchfile.TabIndex = 10;
            this.btn2_searchfile.UseVisualStyleBackColor = true;
            this.btn2_searchfile.Click += new System.EventHandler(this.btn2_searchfile_Click);
            // 
            // btn2_upload
            // 
            this.btn2_upload.BackColor = System.Drawing.Color.Peru;
            this.btn2_upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2_upload.Location = new System.Drawing.Point(332, 249);
            this.btn2_upload.Name = "btn2_upload";
            this.btn2_upload.Size = new System.Drawing.Size(89, 49);
            this.btn2_upload.TabIndex = 8;
            this.btn2_upload.Text = "Upload";
            this.btn2_upload.UseVisualStyleBackColor = false;
            this.btn2_upload.Click += new System.EventHandler(this.btn2_upload_Click);
            // 
            // btn2_close
            // 
            this.btn2_close.BackColor = System.Drawing.Color.Peru;
            this.btn2_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2_close.Location = new System.Drawing.Point(237, 249);
            this.btn2_close.Name = "btn2_close";
            this.btn2_close.Size = new System.Drawing.Size(89, 49);
            this.btn2_close.TabIndex = 9;
            this.btn2_close.Text = "Close";
            this.btn2_close.UseVisualStyleBackColor = false;
            this.btn2_close.Click += new System.EventHandler(this.btn2_close_Click);
            // 
            // tpg_download
            // 
            this.tpg_download.BackColor = System.Drawing.SystemColors.Info;
            this.tpg_download.Controls.Add(this.btn3_close);
            this.tpg_download.Controls.Add(this.btn3_openfolder);
            this.tpg_download.Controls.Add(this.btn3_download);
            this.tpg_download.Location = new System.Drawing.Point(4, 24);
            this.tpg_download.Name = "tpg_download";
            this.tpg_download.Size = new System.Drawing.Size(470, 340);
            this.tpg_download.TabIndex = 2;
            this.tpg_download.Text = "Download System Data";
            // 
            // btn3_close
            // 
            this.btn3_close.BackColor = System.Drawing.Color.Peru;
            this.btn3_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3_close.Location = new System.Drawing.Point(142, 175);
            this.btn3_close.Name = "btn3_close";
            this.btn3_close.Size = new System.Drawing.Size(89, 49);
            this.btn3_close.TabIndex = 22;
            this.btn3_close.Text = "Close";
            this.btn3_close.UseVisualStyleBackColor = false;
            this.btn3_close.Click += new System.EventHandler(this.btn3_close_Click);
            // 
            // btn3_openfolder
            // 
            this.btn3_openfolder.BackColor = System.Drawing.Color.Peru;
            this.btn3_openfolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3_openfolder.Location = new System.Drawing.Point(243, 175);
            this.btn3_openfolder.Name = "btn3_openfolder";
            this.btn3_openfolder.Size = new System.Drawing.Size(89, 49);
            this.btn3_openfolder.TabIndex = 21;
            this.btn3_openfolder.Text = "Open Folder";
            this.btn3_openfolder.UseVisualStyleBackColor = false;
            this.btn3_openfolder.Click += new System.EventHandler(this.btn3_openfolder_Click);
            // 
            // btn3_download
            // 
            this.btn3_download.BackColor = System.Drawing.Color.Peru;
            this.btn3_download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3_download.Location = new System.Drawing.Point(142, 106);
            this.btn3_download.Name = "btn3_download";
            this.btn3_download.Size = new System.Drawing.Size(190, 49);
            this.btn3_download.TabIndex = 9;
            this.btn3_download.Text = "Dowload Data to Send";
            this.btn3_download.UseVisualStyleBackColor = false;
            this.btn3_download.Click += new System.EventHandler(this.btn3_download_Click);
            // 
            // bgworker
            // 
            this.bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworker_DoWork);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbar,
            this.tslbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(478, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tspbar
            // 
            this.tspbar.Name = "tspbar";
            this.tspbar.Size = new System.Drawing.Size(100, 16);
            // 
            // tslbl
            // 
            this.tslbl.Name = "tslbl";
            this.tslbl.Size = new System.Drawing.Size(39, 17);
            this.tslbl.Text = "Ready";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // z_SystemDataUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(478, 368);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "z_SystemDataUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Data Update";
            this.Load += new System.EventHandler(this.z_SystemDataUpdate_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpg_send.ResumeLayout(false);
            this.tpg_send.PerformLayout();
            this.tpg_upload.ResumeLayout(false);
            this.tpg_upload.PerformLayout();
            this.tpg_download.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpg_upload;
        private System.Windows.Forms.Button btn2_upload;
        private System.Windows.Forms.Button btn2_close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt2_fileupload;
        private System.Windows.Forms.Button btn2_searchfile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.ComponentModel.BackgroundWorker bgworker;
        private System.Windows.Forms.TabPage tpg_send;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Button btn_testconect;
        private System.Windows.Forms.Button btn_close1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_server;
        private System.Windows.Forms.Button btn_ping;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TabPage tpg_download;
        private System.Windows.Forms.Button btn3_download;
        private System.Windows.Forms.Button btn3_close;
        private System.Windows.Forms.Button btn3_openfolder;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tspbar;
        private System.Windows.Forms.ToolStripStatusLabel tslbl;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}