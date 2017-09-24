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
        public static bool isError { get; set; }
        public static bool isCanceled { get; set; }
        public static bool folderExist { get; set; }


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


            object t = Properties.Resources.ResourceManager.GetObject("test");
            btn_testconnection.Image = (Image)t;
            btn_testconnection.ImageAlign = ContentAlignment.MiddleCenter;
            //btn_testconnection.BackgroundImage = (Image)t;


            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            btn_selectpath.ImageAlign = ContentAlignment.MiddleCenter;

            textBox_status.Text = "Waiting for credentials...";
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
                await Task.Run(() => new Business.FTP().TestConnection(textbox_serverpath.Text.ToString(), textbox_username.Text, textbox_password.Text, true));

                textBox_status.Text = "Connection successfully!";
                btn_selectpath.Enabled = true;

                /// Disable this button.
                btn_testconnection.Enabled = false;
            }
            catch (Exception e)
            {

                string a = e.Message;
                textBox_status.Text = "Error connecting to FTP!";

                btn_testconnection.Enabled = true;

                /// Save the textboxes so it doesnt change after the test button is pushed.
                textbox_serverpath.Enabled = true;
                textbox_password.Enabled = true;
                textbox_username.Enabled = true;
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
                textBox_status.Text = "Ready to download!";
            }
        }

        /// <summary>
        /// Button that calls the DownloadInside() method for the introduced ftp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_download_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => textBox_status.Text = "Starting download..."));
            this.Invoke(new Action(() => btn_cancel.Enabled = true));
            this.Invoke(new Action(() => btn_selectpath.Enabled = false));
            while (!token.IsCancellationRequested || isFinishedDownload || isCanceled)
            {
                await DoDownload();
                if (isCanceled || isError || folderExist)
                {
                    isFinishedDownload = false;
                }
                else
                {
                    isFinishedDownload = true;
                }
                btn_cancel.Enabled = false;
                token.Cancel();
                break;
            }

            /// if Is finished and without cancelation and error
            if (isFinishedDownload && token.IsCancellationRequested && !isError && !isCanceled)
            {
                await Task.Run(() => endDownload());
                /// If done, show something bro.
                this.Invoke(new Action(() => textBox_status.Text = "Download completed!"));
            }
            /// Is not finished and cancleation is requested && iscancedel true and no error
            if (!isFinishedDownload && token.IsCancellationRequested && !isError && isCanceled)
            {
                await Task.Run(() => endDownload());
                /// If done, show something bro.
                this.Invoke(new Action(() => textBox_status.Text = "Download canceled!"));
            }
            /// Not finished and not canceld but error
            if (!isFinishedDownload && !token.IsCancellationRequested && isError && !isCanceled)
            {
                await Task.Run(() => endDownload());
                /// If done, show something bro.
                this.Invoke(new Action(() => textBox_status.Text = "Error!"));
            }
            if (!isFinishedDownload && token.IsCancellationRequested && !isError && !isCanceled && folderExist)
            {
                //await Task.Run(() => endDownload());
                /// If done, show something bro.

                this.Invoke(new Action(() => btn_download.Enabled = true));
            }
        }

        public async Task endDownload()
        {
            this.Invoke(new Action(() => btn_download.Enabled = false));
            this.Invoke(new Action(() => btn_testconnection.Enabled = true));

            /// Enable FTP buttons
            this.Invoke(new Action(() => textbox_serverpath.Enabled = true));
            this.Invoke(new Action(() => textbox_password.Enabled = true));
            this.Invoke(new Action(() => textbox_username.Enabled = true));

            this.Invoke(new Action(() => textbox_serverpath.Text = ""));
            this.Invoke(new Action(() => textbox_password.Text = ""));
            this.Invoke(new Action(() => textbox_username.Text = ""));
            this.Invoke(new Action(() => textbox_selectedpath.Text = ""));

            /// End progress bar.
            this.Invoke(new Action(() => progressBar1.Value = progressBar1.Maximum));
            this.Invoke(new Action(() => btn_download.Enabled = false));
        }


        private async Task DoDownload()
        {
            /// Set ProgrssBar values and maximum.
            progressBar1.Maximum = 250;
            progressBar1.Value = 0;

            /// Do the download stuff.
            try
            {
                this.Invoke(new Action(() => textBox_status.Text = "Checking FTP path..."));
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

                folderExist = false;

                localfilepath = textbox_selectedpath.Text + newdirectory + "\\";

                if (Directory.Exists(localfilepath))
                {
                    folderExist = true;
                    this.Invoke(new Action(() => textBox_status.Text = "Folder " + newdirectory + " already exist!"));
                }
                else
                {
                    try
                    {
                        if (!token.IsCancellationRequested)
                        {
                            await DownloadInside(textbox_serverpath.Text.ToString(), textbox_selectedpath.Text + newdirectory + "\\");
                        }
                    }
                    catch (Exception e)
                    {
                        string a = e.Message.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                isError = true;
                string a = e.Message.ToString();
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
            if (!token.IsCancellationRequested)
            {


                this.Invoke(new Action(() => isFinishedDownload = false));
                this.Invoke(new Action(() => isError = false));
                /// Disable forms again and some styling.
                this.Invoke(new Action(() => btn_download.Enabled = false));

                /// Enable FTP buttons
                this.Invoke(new Action(() => textbox_serverpath.Enabled = false));
                this.Invoke(new Action(() => textbox_password.Enabled = false));
                this.Invoke(new Action(() => textbox_username.Enabled = false));


                /// Create new list for filename string .
                List<string> directoryfiles = new List<string>();
                /// Get the struct list of files with its type ("d" for directory || "-" for file).
                /// This is going to download the files first since it is ordered to do it!
                List<Business.FTP.Ftplist> ftpdirectory = await new Business.FTP().GetFileList(path, textbox_username.Text, textbox_password.Text);
                ftpdirectory.OrderBy(o => o.Type).ToList();
                /// Since we know how many files there are, lets increment the ProgressBar.
                //this.Invoke(new Action(() => this.progressBar1.Value = this.progressBar1.Maximum + ftpdirectory.Count));

                foreach (Business.FTP.Ftplist item in ftpdirectory)
                {
                    /// If the file/directory is actually a directory and it does NOT exist in our download PATH.
                    if (item.Type == "d" && !Directory.Exists(localpath + item.Filename + "\\") && !token.IsCancellationRequested)
                    {
                        /// If there is a directory, lets create it.
                        await new Business.Local().CreateDirectory(localpath, item.Filename);

                        /// Now lets get the filetree of the directory that we just created (filetree in the FTP not in the PATH).
                        /// This is going to download the files first since it is ordered to do it!
                        List<Business.FTP.Ftplist> newftpdirectory = await new Business.FTP().GetFileList(path + item.Filename + "/", textbox_username.Text, textbox_password.Text);
                        await Task.Run(() => newftpdirectory.OrderBy(o => o.Type).ToList());

                        /// Since we know the filetree now, lets download the files and create the folders.
                        foreach (Business.FTP.Ftplist newitem in newftpdirectory)
                        {
                            /// If the file/directory is a directory, and it does not exist in our PATH.
                            if (newitem.Type == "d" && !Directory.Exists(localpath + item.Filename + "\\" + newitem.Filename + "\\") && !token.IsCancellationRequested)
                            {
                                this.Invoke(new Action(() => this.textBox_status.Text = path + item.Filename));
                                /// It calls this same method to do it again but giving it this current FTP path and LOCAL path.
                                await Task.Run(() => DownloadInside(path + item.Filename + "/", localpath + item.Filename + "\\"));
                            }
                            /// If it is a file and already exist
                            else if (File.Exists(localpath + item.Filename + "\\" + newitem.Filename) && !token.IsCancellationRequested)
                            {
                                /// Do nothing.
                            }
                            /// And if it is not a file that exist and it is not a directory that existe, then it is a file that we have to download
                            else if (!Directory.Exists(localpath + item.Filename + "\\" + newitem.Filename + "\\") && !File.Exists(localpath + item.Filename + "\\" + newitem.Filename) && !token.IsCancellationRequested)
                            {
                                /// Encode it to URL for the FTP download.
                                string UrlEncodedFilename = await Task.Run(() => System.Net.WebUtility.UrlEncode(newitem.Filename).Replace("+", "%20"));

                                /// Download it.
                                await Task.Run(() => new Business.FTP().DownloadFile(path + item.Filename + "/", textbox_username.Text, textbox_password.Text, localpath + item.Filename + "\\", UrlEncodedFilename, newitem.Filename, localfilepath));

                                this.Invoke(new Action(() => this.textBox_status.Text = path + item.Filename + "/" + newitem.Filename));
                            }
                            /// Add 1 to progress bar.
                            try
                            {
                                this.Invoke(new Action(() => this.progressBar1.Increment(1)));
                            }
                            catch (Exception)
                            {
                                /// If the increment is greater thant the maximun, just keep it 99%.
                                this.Invoke(new Action(() => this.progressBar1.Value = progressBar1.Maximum - 1));
                            }
                        }
                    }
                    /// I the file/directory already exist
                    else if (File.Exists(localpath + item.Filename) && !token.IsCancellationRequested)
                    {
                        /// Obviously it does nothing.
                    }
                    /// It does not exist and it is not a directory, so is a file -> download it.
                    else
                    {
                        if (!token.IsCancellationRequested)
                        {
                            /// Encode it to URL for the FTP download.
                            string UrlEncodedFilename = await Task.Run(() => System.Net.WebUtility.UrlEncode(item.Filename).Replace("+", "%20"));
                            /// Download it.
                            try
                            {
                                await Task.Run(() => new Business.FTP().DownloadFile(path, textbox_username.Text, textbox_password.Text, localpath, UrlEncodedFilename, item.Filename, localfilepath));

                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    /// Add 1 to progress bar.
                    try
                    {
                        this.Invoke(new Action(() => this.progressBar1.Increment(1)));
                    }
                    catch (Exception)
                    {
                        /// If the increment is greater thant the maximun, just keep it 99%.
                        this.Invoke(new Action(() => this.progressBar1.Value = progressBar1.Maximum - 1));
                    }

                }
                this.Invoke(new Action(() => isFinishedDownload = true));
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private async void btn_cancel_Click(object sender, EventArgs e)
        {
            token.Cancel();

            this.Invoke(new Action(() => btn_cancel.Enabled = false));

            this.Invoke(new Action(() => isError = false));
            this.Invoke(new Action(() => isFinishedDownload = false));
            this.Invoke(new Action(() => isCanceled = true));


            this.Invoke(new Action(() => textbox_serverpath.Enabled = true));
            this.Invoke(new Action(() => textbox_password.Enabled = true));
            this.Invoke(new Action(() => textbox_username.Enabled = true));
            this.Invoke(new Action(() => btn_selectpath.Enabled = false));

            this.Invoke(new Action(() => textbox_serverpath.Text = ""));
            this.Invoke(new Action(() => textbox_password.Text = ""));
            this.Invoke(new Action(() => textbox_username.Text = ""));
            this.Invoke(new Action(() => textbox_selectedpath.Text = ""));

            //this.Invoke(new Action(() => textBox_status.Text = "Download canceled!"));
            this.Invoke(new Action(() => progressBar1.Value = progressBar1.Maximum));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => System.Diagnostics.Process.Start("https://github.com/emimontesdeoca/FTPDownloader")));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => System.Diagnostics.Process.Start("https://twitter.com/emimontesdeocaa")));
        }
    }

    #endregion

}

