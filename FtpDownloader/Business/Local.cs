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

        public void CreateDirectory(string filepath, string directoryname)
        {
            Directory.CreateDirectory(filepath + directoryname);

        }
    }
}
