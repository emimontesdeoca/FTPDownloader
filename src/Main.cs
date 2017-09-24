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
        public static bool isFinishedDownload { get; set; }
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;

            object o = Properties.Resources.ResourceManager.GetObject("folderjpg");
            btn_selectpath.Image = (Image)o;
            btn_selectpath.ImageAlign = ContentAlignment.MiddleCenter;
            textBox_status.Text = "Waiting for credentials...";

            textbox_serverpath.Text = "files.000webhost.com";
            textbox_username.Text = "remontesdeoc";
            textbox_password.Text = "pelotudo1337";
        }

        #region BUTTON CONTROL

        /// <summary>
        /// Button that test the connection of the parameters introduced in the textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_testconnection_Click(object sender, EventArgs e)
        {
            await doTestConnection();

        }

        private async Task doTestConnection()
        {
            /// Try to connect to the FTP.
            try
            {
                btn_testconnection.Enabled = false;

                /// Save the textboxes so it doesnt change after the test button is pushed.
                textbox_serverpath.Enabled = false;
                textbox_password.Enabled = false;
                textbox_username.Enabled = false;

                textBox_status.Text = "Connecting...";

                /// Corrects the input for ftpserver.
                if (textbox_serverpath.Text.ToString().Substring(textbox_serverpath.Text.ToString().Count() - 1, 1) != "/")
                {
                    textbox_serverpath.Text += "/";
                }
                /// Corrects the input for ftpserver.
                if (textbox_serverpath.Text.ToString().Substring(0, 6) != "ftp://")
                {
                    string newstring = "ftp://" + textbox_serverpath.Text.ToString();
                    textbox_serverpath.Text = newstring;
                }

                /// If successfully show some messagebox and enable buttons for further steps.
                await new Business.FTP().TestConnection(textbox_serverpath.Text.ToString(), textbox_username.Text, textbox_password.Text, true);

                textBox_status.Text = "Connection successfully!";
                btn_selectpath.Enabled = true;

                /// Disable this button.
                btn_testconnection.Enabled = false;
            }
            catch (Exception)
            {
                textBox_status.Text = "Error connecting to FTP!";
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
        private async void btn_download_Click(object sender, EventArgs e)
        {
            while (!token.IsCancellationRequested || isFinishedDownload)
            {
                await DoDownload();
                token.Cancel();
                isFinishedDownload = true;
                break;
            }
        }

        private async Task DoDownload()
        {
            /// Set ProgrssBar values and maximum.
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            /// Do the download stuff.
            try
            {

                string newdirectory = "";
                /// Corrects the input for ftpserver.
                if (textbox_serverpath.Text.ToString().Substring(textbox_serverpath.Text.ToString().Count() - 1, 1) == "/")
                {
                    newdirectory = textbox_serverpath.Text.ToString().Remove(textbox_serverpath.Text.ToString().Length - 1, 1);
                }
                /// Corrects the input for ftpserver.
                if (textbox_serverpath.Text.ToString().Substring(0, 6) == "ftp://")
                {
                    newdirectory = newdirectory.Remove(0, 6);
                }
                localfilepath = textbox_selectedpath.Text + newdirectory + "\\";
                await new Business.Local().CreateDirectory(textbox_selectedpath.Text, newdirectory);
                await DownloadInside(textbox_serverpath.Text.ToString(), textbox_selectedpath.Text + newdirectory + "\\");

                /// End progress bar.
                progressBar1.Value = progressBar1.Maximum;

                /// If done, show something bro.
                MessageBox.Show("Download completed!");
            }
            catch (Exception)
            {
                progressBar1.Value = 0;
                MessageBox.Show("FTP error, retry again!");
            }
        }

        #endregion

        #region LOGIC

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
            textbox_serverpath.Enabled = false;
            textbox_password.Enabled = false;
            textbox_username.Enabled = false;


            /// Create new list for filename string .
            List<string> directoryfiles = new List<string>();
            /// Get the struct list of files with its type ("d" for directory || "-" for file).
            /// This is going to download the files first since it is ordered to do it!
            List<Business.FTP.Ftplist> ftpdirectory = await new Business.FTP().GetFileList(path, textbox_username.Text, textbox_password.Text);
            ftpdirectory.OrderBy(o => o.Type).ToList();
            /// Since we know how many files there are, lets increment the ProgressBar.
            progressBar1.Maximum = progressBar1.Maximum + ftpdirectory.Count;

            foreach (Business.FTP.Ftplist item in ftpdirectory)
            {
                /// If the file/directory is actually a directory and it does NOT exist in our download PATH.
                if (item.Type == "d" && !Directory.Exists(localpath + item.Filename + "\\"))
                {
                    /// If there is a directory, lets create it.
                    await new Business.Local().CreateDirectory(localpath, item.Filename);

                    /// Now lets get the filetree of the directory that we just created (filetree in the FTP not in the PATH).
                    /// This is going to download the files first since it is ordered to do it!
                    List<Business.FTP.Ftplist> newftpdirectory = await new Business.FTP().GetFileList(path + item.Filename + "/", textbox_username.Text, textbox_password.Text);
                    newftpdirectory.OrderBy(o => o.Type).ToList();

                    /// Since we know the filetree now, lets download the files and create the folders.
                    foreach (Business.FTP.Ftplist newitem in newftpdirectory)
                    {
                        /// If the file/directory is a directory, and it does not exist in our PATH.
                        if (newitem.Type == "d" && !Directory.Exists(localpath + item.Filename + "\\" + newitem.Filename + "\\"))
                        {
                            /// It calls this same method to do it again but giving it this current FTP path and LOCAL path.
                            await DownloadInside(path + item.Filename + "/", localpath + item.Filename + "\\");
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
                            await new Business.FTP().DownloadFile(path + item.Filename + "/", textbox_username.Text, textbox_password.Text, localpath + item.Filename + "\\", UrlEncodedFilename, newitem.Filename, localfilepath);
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
                    await new Business.FTP().DownloadFile(path, textbox_username.Text, textbox_password.Text, localpath, UrlEncodedFilename, item.Filename, localfilepath);
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
            textbox_serverpath.Enabled = true;
            textbox_password.Enabled = true;
            textbox_username.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/emimontesdeoca/FTPDownloader");
        }
    }

    #endregion

}

