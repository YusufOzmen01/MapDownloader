using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace MapDownloader
{
    internal class DownloadManagement
    {
        public static void DownloadMap(string link)
        {
            var client = new WebClient();
            var setId = LinkManagement.GetSetId(link);
            var fileName = LinkManagement.GetFileName(link);

            try
            {
                client.DownloadFile(new Uri("https://chimu.moe/d/" + setId), @"C:\Windows\Temp\" + fileName);
            }
            
            catch (WebException)
            {
                Process.Start(DbReader.OsuPathKey.GetValue("BrowserPath").ToString(), link);
                Environment.Exit(0);
            }
        }
    }
}