using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpDownloader.Business
{
    public class Log
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Log() { }

        /// <summary>
        /// Write downloaded file in log.txt
        /// </summary>
        /// <param name="s"></param>
        public void WriteLog(string FtpPath, string DownloadFolderPath, string rootDownloadFolderPath)
        {
            string log = ("Date: " + DateTime.Now.ToShortDateString() + " - Time: " + DateTime.Now.ToShortTimeString() + " - FileSource: " + FtpPath + " - Destination Folder: " + DownloadFolderPath);
            System.IO.File.AppendAllText(rootDownloadFolderPath + "download_log.txt", log + Environment.NewLine);
        }


    }
}
