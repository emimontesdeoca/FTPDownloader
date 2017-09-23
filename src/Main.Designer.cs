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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btn_testconnection = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textbox_password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textbox_selectedpath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_selectpath = new System.Windows.Forms.Button();
            this.btn_download = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combobox_ftpserver = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_testconnection
            // 
            this.btn_testconnection.Location = new System.Drawing.Point(133, 145);
            this.btn_testconnection.Name = "btn_testconnection";
            this.btn_testconnection.Size = new System.Drawing.Size(107, 21);
            this.btn_testconnection.TabIndex = 0;
            this.btn_testconnection.Text = "Test connection";
            this.btn_testconnection.UseVisualStyleBackColor = true;
            this.btn_testconnection.Click += new System.EventHandler(this.btn_testconnection_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server path";
            // 
            // textbox_username
            // 
            this.textbox_username.Location = new System.Drawing.Point(9, 76);
            this.textbox_username.Name = "textbox_username";
            this.textbox_username.Size = new System.Drawing.Size(235, 20);
            this.textbox_username.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username";
            // 
            // textbox_password
            // 
            this.textbox_password.Location = new System.Drawing.Point(9, 119);
            this.textbox_password.Name = "textbox_password";
            this.textbox_password.Size = new System.Drawing.Size(235, 20);
            this.textbox_password.TabIndex = 6;
            this.textbox_password.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 262);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(250, 20);
            this.progressBar1.TabIndex = 7;
            // 
            // textbox_selectedpath
            // 
            this.textbox_selectedpath.Enabled = false;
            this.textbox_selectedpath.Location = new System.Drawing.Point(6, 33);
            this.textbox_selectedpath.Name = "textbox_selectedpath";
            this.textbox_selectedpath.Size = new System.Drawing.Size(199, 20);
            this.textbox_selectedpath.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Path";
            // 
            // btn_selectpath
            // 
            this.btn_selectpath.Enabled = false;
            this.btn_selectpath.Location = new System.Drawing.Point(214, 30);
            this.btn_selectpath.Name = "btn_selectpath";
            this.btn_selectpath.Size = new System.Drawing.Size(30, 24);
            this.btn_selectpath.TabIndex = 10;
            this.btn_selectpath.UseVisualStyleBackColor = true;
            this.btn_selectpath.Click += new System.EventHandler(this.btn_selectpath_Click);
            // 
            // btn_download
            // 
            this.btn_download.Enabled = false;
            this.btn_download.Location = new System.Drawing.Point(126, 320);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(65, 20);
            this.btn_download.TabIndex = 11;
            this.btn_download.Text = "Download";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.combobox_ftpserver);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btn_testconnection);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textbox_username);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textbox_password);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 175);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credentials";
            // 
            // combobox_ftpserver
            // 
            this.combobox_ftpserver.FormattingEnabled = true;
            this.combobox_ftpserver.Location = new System.Drawing.Point(9, 33);
            this.combobox_ftpserver.Name = "combobox_ftpserver";
            this.combobox_ftpserver.Size = new System.Drawing.Size(235, 21);
            this.combobox_ftpserver.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 21);
            this.button1.TabIndex = 7;
            this.button1.Text = "Save credentials";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 291);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 20);
            this.textBox1.TabIndex = 13;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textbox_selectedpath);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btn_selectpath);
            this.groupBox2.Location = new System.Drawing.Point(12, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 64);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(197, 320);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 20);
            this.button2.TabIndex = 15;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Emiliano Montesdeoca";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(224, 347);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(38, 13);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Github";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(274, 369);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_download);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FtpDownloader";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_testconnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textbox_username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textbox_password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textbox_selectedpath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_selectpath;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox combobox_ftpserver;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

