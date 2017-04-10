using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FtpDownloader.Business
{
    public class Local
    {
        /// <summary>
        /// Method that creates a directory in a certain path.
        /// </summary>
        /// <param name="filepath">Path to create the folder.</param>
        /// <param name="directoryname">Directory name.</param>
        public void CreateDirectory(string filepath, string directoryname)
        {
            Directory.CreateDirectory(filepath + directoryname);
        }
    }
}
