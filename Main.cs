using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace FtpDownloader
{
    public partial class Main : Form
    {
        public string localfilepath { get; set; }
        public CancellationTokenSource token = new CancellationTokenSource();
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        #region BUTTON CONTROL

        /// <summary>
        /// Button that test the connection of the parameters introduced in the textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_testconnection_Click(object sender, EventArgs e)
        {
            /// Try to connect to the FTP.
            try
            {
                /// Corrects the input for ftpserver.
                if (textbox_ftpserver.Text.Substring(textbox_ftpserver.Text.Count() - 1, 1) != "/")
                {
                    textbox_ftpserver.Text += "/";
                }
                /// Corrects the input for ftpserver.
                if (textbox_ftpserver.Text.Substring(0, 6) != "ftp://")
                {
                    string newstring = "ftp://" + textbox_ftpserver.Text;
                    textbox_ftpserver.Text = newstring;
                }

                /// If successfully show some messagebox and enable buttons for further steps.
                new Business.FTP().TestConnection(textbox_ftpserver.Text, textbox_username.Text, textbox_password.Text, true);
                MessageBox.Show("Connection successfully.");
                btn_selectpath.Enabled = true;

                /// Save the textboxes so it doesnt change after the test button is pushed.
                textbox_ftpserver.Enabled = false;
                textbox_password.Enabled = false;
                textbox_username.Enabled = false;

                /// Disable this button.
                btn_testconnection.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to FTP.");
            }
        }

        /// <summary>
        /// Button that opens a FolderBrowserDialog asking for a path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_selectpath_Click(object sender, EventArgs e)
        {
            /// Instance a FolderBrowserDialog.
            FolderBrowserDialog fbd = new FolderBrowserDialog()
            {
                Description = "In other to make sure that the download has no problems, the folder has to have writing and reading rights for everyone."
            };
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                /// Set the textbox with the selectedpath.
                textbox_selectedpath.Text = fbd.SelectedPath + "\\";
                /// Enable the download button since we have a path.
                btn_download.Enabled = true;
                /// Set the text to download, this is because we change the text in next steps.
                btn_download.Text = "Download";
            }
        }

        /// <summary>
        /// Button that calls the DownloadInside() method for the introduced ftp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_download_Click(object sender, EventArgs e)
        {
            /// Set ProgrssBar values and maximum.
            progressBar1.Maximum = 500;
            progressBar1.Value = 0;

            /// Do the download stuff.
            try
            {

                string newdirectory = "";
                /// Corrects the input for ftpserver.
                if (textbox_ftpserver.Text.Substring(textbox_ftpserver.Text.Count() - 1, 1) == "/")
                {
                    newdirectory = textbox_ftpserver.Text.Remove(textbox_ftpserver.Text.Length - 1, 1);
                }
                /// Corrects the input for ftpserver.
                if (textbox_ftpserver.Text.Substring(0, 6) == "ftp://")
                {
                    newdirectory = newdirectory.Remove(0, 6);
                }
                localfilepath = textbox_selectedpath.Text + newdirectory + "\\";
                new Business.Local().CreateDirectory(textbox_selectedpath.Text, newdirectory);
                var a = DownloadInside(textbox_ftpserver.Text, textbox_selectedpath.Text + newdirectory + "\\");
                /// End progress bar.
                progressBar1.Value = progressBar1.Maximum;

                /// If done, show something bro.
                MessageBox.Show("Download completed!");
            }
            catch (Exception)
            {
                progressBar1.Value = 0;
                MessageBox.Show("FTP timeout, retry again!");
            }
        }

        #endregion

        #region LOGIC

        public async Task AsyncBackcraft(int interval)
        {
            while (!token.IsCancellationRequested)
            {

                /// Set ProgrssBar values and maximum.
                progressBar1.Maximum = 500;
                progressBar1.Value = 0;

                /// Do the download stuff.
                try
                {
                    string newdirectory = "";

                    /// Corrects the input for ftpserver.
                    if (textbox_ftpserver.Text.Substring(textbox_ftpserver.Text.Count() - 1, 1) == "/")
                    {
                        newdirectory = textbox_ftpserver.Text.Remove(textbox_ftpserver.Text.Length - 1, 1);
                    }
                    /// Corrects the input for ftpserver.
                    if (textbox_ftpserver.Text.Substring(0, 6) == "ftp://")
                    {
                        newdirectory = newdirectory.Remove(0, 6);
                    }

                    localfilepath = textbox_selectedpath.Text + newdirectory + "\\";
                    /// Create dir
                    new Business.Local().CreateDirectory(textbox_selectedpath.Text, newdirectory);

                    /// Aync task
                    var a = DownloadInside(textbox_ftpserver.Text, textbox_selectedpath.Text + newdirectory + "\\");

                    /// End progress bar.
                    progressBar1.Value = progressBar1.Maximum;

                    /// If done, show something bro.
                    MessageBox.Show("Download completed!");
                }
                catch (Exception)
                {
                    progressBar1.Value = 0;
                    MessageBox.Show("FTP timeout, retry again!");
                }
            }
        }


        /// <summary>
        /// Method that downloads from FTP.
        /// </summary>
        /// <param name="path">Path to FTP.</param>
        /// <param name="localpath">Path to local.</param>
        private async Task DownloadInside(string path, string localpath)
        {
            /// Disable forms again and some styling.
            btn_download.Enabled = false;

            /// Enable FTP buttons
            textbox_ftpserver.Enabled = false;
            textbox_password.Enabled = false;
            textbox_username.Enabled = false;


            /// Create new list for filename string .
            List<string> directoryfiles = new List<string>();
            /// Get the struct list of files with its type ("d" for directory || "-" for file).
            /// This is going to download the files first since it is ordered to do it!
            List<Business.FTP.Ftplist> ftpdirectory = new Business.FTP().GetFileList(path, textbox_username.Text, textbox_password.Text).OrderBy(o => o.Type).ToList();
            /// Since we know how many files there are, lets increment the ProgressBar.
            progressBar1.Increment(ftpdirectory.Count);

            foreach (Business.FTP.Ftplist item in ftpdirectory)
            {
                /// If the file/directory is actually a directory and it does NOT exist in our download PATH.
                if (item.Type == "d" && !Directory.Exists(localpath + item.Filename + "\\"))
                {
                    /// If there is a directory, lets create it.
                    new Business.Local().CreateDirectory(localpath, item.Filename);
                    new Business.Log().WriteLog(path, localpath + item.Filename, localfilepath);

                    /// Now lets get the filetree of the directory that we just created (filetree in the FTP not in the PATH).
                    /// This is going to download the files first since it is ordered to do it!
                    List<Business.FTP.Ftplist> newftpdirectory = new Business.FTP().GetFileList(path + item.Filename + "/", textbox_username.Text, textbox_password.Text).OrderBy(o => o.Type).ToList();

                    /// Since we know the filetree now, lets download the files and create the folders.
                    foreach (Business.FTP.Ftplist newitem in newftpdirectory)
                    {
                        /// If the file/directory is a directory, and it does not exist in our PATH.
                        if (newitem.Type == "d" && !Directory.Exists(localpath + item.Filename + "\\" + newitem.Filename + "\\"))
                        {
                            /// It calls this same method to do it again but giving it this current FTP path and LOCAL path.
                            DownloadInside(path + item.Filename + "/", localpath + item.Filename + "\\");
                        }
                        /// If it is a file and already exist
                        else if (File.Exists(localpath + item.Filename + "\\" + newitem.Filename))
                        {
                            /// Do nothing.
                        }
                        /// And if it is not a file that exist and it is not a directory that existe, then it is a file that we have to download
                        else if (!Directory.Exists(localpath + item.Filename + "\\" + newitem.Filename + "\\") && !File.Exists(localpath + item.Filename + "\\" + newitem.Filename))
                        {
                            /// Encode it to URL for the FTP download.
                            string UrlEncodedFilename = System.Net.WebUtility.UrlEncode(newitem.Filename).Replace("+", "%20");

                            /// Download it.
                            new Business.FTP().DownloadFile(path + item.Filename + "/", textbox_username.Text, textbox_password.Text, localpath + item.Filename + "\\", UrlEncodedFilename, newitem.Filename, localfilepath);
                        }
                        /// Add 1 to progress bar.
                        try
                        {
                            progressBar1.Increment(1);
                        }
                        catch (Exception)
                        {
                            /// If the increment is greater thant the maximun, just keep it 99%.
                            progressBar1.Value = progressBar1.Maximum - 1;
                        }
                    }
                }
                /// I the file/directory already exist
                else if (File.Exists(localpath + item.Filename))
                {
                    /// Obviously it does nothing.
                }
                /// It does not exist and it is not a directory, so is a file -> download it.
                else
                {
                    /// Encode it to URL for the FTP download.
                    string UrlEncodedFilename = System.Net.WebUtility.UrlEncode(item.Filename).Replace("+", "%20");
                    /// Download it.
                    new Business.FTP().DownloadFile(path, textbox_username.Text, textbox_password.Text, localpath, UrlEncodedFilename, item.Filename, localfilepath);
                }
                /// Add 1 to progress bar.
                try
                {
                    progressBar1.Increment(1);
                }
                catch (Exception)
                {
                    /// If the increment is greater thant the maximun, just keep it 99%.
                    progressBar1.Value = progressBar1.Maximum - 1;
                }
            }

            /// Disable forms again and some styling.
            btn_download.Enabled = true;

            /// Enable FTP buttons
            textbox_ftpserver.Enabled = true;
            textbox_password.Enabled = true;
            textbox_username.Enabled = true;
        }

    }

    #endregion

}

