using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FtpDownloader.Business
{
    /// <summary>
    /// FTP class where it contais connection methods and file management.
    /// </summary>
    class FTP
    {
        #region VARIABLES

        /// <summary>
        /// Struct for every file or directory.
        /// </summary>
        public struct ftplist
        {
            /// <summary>
            /// Type, it can be either "-" for file or "d" for directory.
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// Filename.
            /// </summary>
            public string filename { get; set; }
        }

        #endregion

        #region CONNECTION STUFF

        /// <summary>
        /// Function that creates teh FTPWebRequest.
        /// </summary>
        /// <param name="FTPDirectoryPath">FTP path.</param>
        /// <param name="username">FTP Username.</param>
        /// <param name="password">FTP Password.</param>
        /// <param name="keepAlive">Bool to close the connection after done, true by default.</param>
        /// <returns></returns>
        public FtpWebRequest CreateFtpWebRequest(string FTPDirectoryPath, string username, string password, bool keepAlive = false)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(FTPDirectoryPath));

            request.Proxy = null;

            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = keepAlive;

            request.Credentials = new NetworkCredential(username, password);

            return request;
        }

        /// <summary>
        /// Function that try to connect to the FTP.
        /// </summary>
        /// <param name="s">Settings </param>
        /// <param name="keepAlive">Request value, keep it TRUE.</param>
        public void TestConnection(string FtpFolderPath, string FtpUsername, string FtpPassword, bool keepAlive = false)
        {
            try
            {
                /// Creates FtpWebRequest.
                FtpWebRequest request = CreateFtpWebRequest(FtpFolderPath, FtpUsername, FtpPassword, true);

                /// Method is set to ListDirectoryDetails.
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                /// Make the call to the FTP.
                request.GetResponse();
            }
            catch
            {
                throw new Exception();
            }

        }

        /// <summary>
        /// Method that gets the directory list.
        /// </summary>
        /// <param name="s">Settings.</param>
        /// <param name="keepAlive">Request value, keep it TRUE.</param>
        /// <returns></returns>
        public FtpWebResponse GetDirectoryList(string FtpFolderPath, string FtpUsername, string FtpPassword, bool keepAlive = false)
        {
            /// Creates FtpWebRequest.
            FtpWebRequest request = CreateFtpWebRequest(FtpFolderPath, FtpUsername, FtpPassword, true);

            /// Method is set to ListDirectoryDetails.
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            try
            {
                /// Return the response.
                return (FtpWebResponse)request.GetResponse();

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        #endregion

        #region FTP FILE MANAGEMENT

        /// <summary>
        /// Method that downloads a file from a FTP.
        /// </summary>
        /// <param name="FtpFolderPath">Path to file/directory in FTP.</param>
        /// <param name="FtpUsername">Ftp username.</param>
        /// <param name="FtpPassword">Ftp password.</param>
        /// <param name="DownloadFolderPath">Local download path.</param>
        /// <param name="UrlEncodedTorrent">Encoded URL, necessary to download.</param>
        /// <param name="filename">Filename for creating the file with same name(not encoded).</param>
        public void DownloadFile(string FtpFolderPath, string FtpUsername, string FtpPassword, string DownloadFolderPath, string UrlEncodedTorrent, string filename)
        {
            /// Filestream necessary values.
            int bytesRead = 0;
            byte[] buffer = new byte[2048];

            /// Creates request and assigns it to the request.
            FtpWebRequest request = CreateFtpWebRequest(FtpFolderPath + UrlEncodedTorrent, FtpUsername, FtpPassword, true);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            try
            {
                /// Gets response from the server.
                Stream reader = request.GetResponse().GetResponseStream();

                /// Generates a file in the destination with the certain filename.
                FileStream fileStream = new FileStream(DownloadFolderPath + filename, FileMode.Create);

                /// While the file has bytes, it keeps writing on the file.
                while (true)
                {
                    bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                /// Close the file.
                fileStream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CreateDirectory(string FtpFolderPath, string FtpUsername, string FtpPassword, string DirectoryName)
        {

            /// Creates request and assigns it to the request.
            FtpWebRequest request = CreateFtpWebRequest(FtpFolderPath + DirectoryName, FtpUsername, FtpPassword, true);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.GetResponse();
        }

        /// <summary>
        /// Method that deletes file on FTP server.
        /// </summary>
        /// <param name="r">FtpWebRequest, it contais entire data like path and filename.</param>
        public void DeleteFileOnFtpServer(FtpWebRequest r)
        {
            /// Make the request.
            FtpWebRequest request = r;

            /// Set the method.
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            try
            {
                /// Try to do the request on the FTP.
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region GET FTP FILE LIST

        /// <summary>
        /// Reads entire directory for .torrents.
        /// </summary>
        /// <param name="s">Settings.</param>
        /// <returns></returns>
        public List<ftplist> GetFileList(string FtpFolderPath, string FtpUsername, string FtpPassword)
        {
            /// Set the sourceFileList to return, is a list of ftplist, the struct generated at the beginning.
            List<ftplist> sourceFileList = new List<ftplist>();
            string line = "";
            try
            {
                /// Build the response and return it to this method.
                FtpWebResponse sourceResponse = GetDirectoryList(FtpFolderPath, FtpUsername, FtpPassword, true);

                /// Do the call to the FTP.
                using (Stream responseStream = sourceResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        /// Reads the filename details.
                        line = reader.ReadLine();
                        /// While the line is not null (no more lines).
                        while (line != null)
                        {
                            try
                            {
                                /// Split it by spaces.
                                string[] newfilename = line.Split(' ');
                                /// Split it by ":", gives 2 string array.
                                string[] fullfilename = line.Split(':');
                                /// Delete 3 first characters because of yes.
                                string finalfilename = fullfilename[1].Remove(0, 3).ToString();
                                try
                                {
                                    /// If last is not torrent, does nothing
                                    if (newfilename.Last() == "." || newfilename.Last() == "..")
                                    {
                                    }
                                    else /// Add it to the list
                                    {
                                        ftplist newitem = new ftplist()
                                        {
                                            type = line.Substring(0, 1),
                                            filename = finalfilename
                                        };
                                        sourceFileList.Add(newitem);
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            catch (Exception)
                            {
                            }
                            line = reader.ReadLine();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            /// Return the list of ftlist struct.
            return sourceFileList;
        }

        #endregion
    }
}
