namespace FtpDownloader
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_testconnection = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_ftpserver = new System.Windows.Forms.TextBox();
            this.textbox_username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textbox_password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textbox_selectedpath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_selectpath = new System.Windows.Forms.Button();
            this.btn_download = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_testconnection
            // 
            this.btn_testconnection.Location = new System.Drawing.Point(343, 10);
            this.btn_testconnection.Name = "btn_testconnection";
            this.btn_testconnection.Size = new System.Drawing.Size(96, 74);
            this.btn_testconnection.TabIndex = 0;
            this.btn_testconnection.Text = "Test connection";
            this.btn_testconnection.UseVisualStyleBackColor = true;
            this.btn_testconnection.Click += new System.EventHandler(this.btn_testconnection_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ftp server path";
            // 
            // textbox_ftpserver
            // 
            this.textbox_ftpserver.Location = new System.Drawing.Point(101, 12);
            this.textbox_ftpserver.Name = "textbox_ftpserver";
            this.textbox_ftpserver.Size = new System.Drawing.Size(236, 20);
            this.textbox_ftpserver.TabIndex = 2;
            // 
            // textbox_username
            // 
            this.textbox_username.Location = new System.Drawing.Point(101, 38);
            this.textbox_username.Name = "textbox_username";
            this.textbox_username.Size = new System.Drawing.Size(236, 20);
            this.textbox_username.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ftp username";
            // 
            // textbox_password
            // 
            this.textbox_password.Location = new System.Drawing.Point(101, 64);
            this.textbox_password.Name = "textbox_password";
            this.textbox_password.Size = new System.Drawing.Size(236, 20);
            this.textbox_password.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ftp password";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 117);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(317, 37);
            this.progressBar1.TabIndex = 7;
            // 
            // textbox_selectedpath
            // 
            this.textbox_selectedpath.Enabled = false;
            this.textbox_selectedpath.Location = new System.Drawing.Point(101, 90);
            this.textbox_selectedpath.Name = "textbox_selectedpath";
            this.textbox_selectedpath.Size = new System.Drawing.Size(236, 20);
            this.textbox_selectedpath.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Download path";
            // 
            // btn_selectpath
            // 
            this.btn_selectpath.Enabled = false;
            this.btn_selectpath.Location = new System.Drawing.Point(343, 88);
            this.btn_selectpath.Name = "btn_selectpath";
            this.btn_selectpath.Size = new System.Drawing.Size(96, 23);
            this.btn_selectpath.TabIndex = 10;
            this.btn_selectpath.Text = "Select path";
            this.btn_selectpath.UseVisualStyleBackColor = true;
            this.btn_selectpath.Click += new System.EventHandler(this.btn_selectpath_Click);
            // 
            // btn_download
            // 
            this.btn_download.Enabled = false;
            this.btn_download.Location = new System.Drawing.Point(343, 117);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(96, 37);
            this.btn_download.TabIndex = 11;
            this.btn_download.Text = "Download";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 167);
            this.Controls.Add(this.btn_download);
            this.Controls.Add(this.btn_selectpath);
            this.Controls.Add(this.textbox_selectedpath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textbox_password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textbox_username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textbox_ftpserver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_testconnection);
            this.Name = "Main";
            this.Text = "FtpDownloader";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_testconnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textbox_ftpserver;
        private System.Windows.Forms.TextBox textbox_username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textbox_password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textbox_selectedpath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_selectpath;
        private System.Windows.Forms.Button btn_download;
    }
}

