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

namespace FtpDownloader
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btn_testconnection_Click(object sender, EventArgs e)
        {
            try
            {
                new Business.FTP().TestConnection(textbox_ftpserver.Text, textbox_username.Text, textbox_password.Text, true);
                MessageBox.Show("Connection successfully.");
                btn_selectpath.Enabled = true;

                /// First create directories 


            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to FTP.");
            }
        }

        private void btn_selectpath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textbox_selectedpath.Text = fbd.SelectedPath + "\\";
                btn_download.Enabled = true;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            textbox_ftpserver.Text = "ftp://emontesdeoc.net23.net/ftpd/";
            textbox_username.Text = "a4497847";
            textbox_password.Text = "pelotudo1337";
        }

        private void btn_download_Click(object sender, EventArgs e)
        {

            DownloadInside(textbox_ftpserver.Text, "", textbox_selectedpath.Text);

        }
        private void DownloadInside(string path, string folder, string localpath)
        {

            List<string> directoryfiles = new List<string>();
            List<Business.FTP.ftplist> ftpdirectory = new Business.FTP().GetFileList(path, textbox_username.Text, textbox_password.Text);
            foreach (Business.FTP.ftplist item in ftpdirectory)
            {
                if (item.type == "d")
                {
                    new Business.Local().CreateDirectory(localpath, item.filename);
                    List<Business.FTP.ftplist> newftpdirectory = new Business.FTP().GetFileList(textbox_ftpserver.Text + item.filename + "/", textbox_username.Text, textbox_password.Text);
                    foreach (Business.FTP.ftplist newitem in newftpdirectory)
                    {
                        if (newitem.type == "d")
                        {
                            DownloadInside(textbox_ftpserver.Text + item.filename + "/", newitem.filename, textbox_selectedpath.Text + item.filename + "\\");
                        }
                        else if (File.Exists(path + newitem.filename))
                        {

                        }
                        else
                        {
                            string UrlEncodedFilename = System.Net.WebUtility.UrlEncode(newitem.filename).Replace("+", "%20");
                            new Business.FTP().DownloadFile(textbox_ftpserver.Text + item.filename + "/", textbox_username.Text, textbox_password.Text, textbox_selectedpath.Text + item.filename + "\\", UrlEncodedFilename, newitem.filename);
                        }
                    }

                }
                else if (File.Exists(localpath + item.filename))
                {

                }
                else
                {
                    string[] newpath = path.Split('/');


                    string UrlEncodedFilename = System.Net.WebUtility.UrlEncode(item.filename).Replace("+", "%20");
                    new Business.FTP().DownloadFile(path, textbox_username.Text, textbox_password.Text, textbox_selectedpath.Text + folder, UrlEncodedFilename, item.filename);
                }
            }
        }
    }
}
