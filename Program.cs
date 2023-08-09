using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using Newtonsoft.Json;
using System.Reflection;

namespace MapDownloader
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //string appPath = Assembly.GetEntryAssembly().Location;

            //if (Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\MapDownloaderURL\shell\open\command").GetValue(null).ToString() != "\"" + appPath + "\" " + "\"" + "%1" + "\"")
            //{
            //    Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\MapDownloaderURL\shell\open\command").SetValue(null, "\"" + appPath + "\" " + "\"" + "%1" + "\"");
            //}

            if (RegistryManagement.IsAdministrator())
            {
                RegistryManagement.CreateRegistry();
                MessageBox.Show("To use this program, set your default browser as MapDownloader.exe and choose your default browser's executable after clicking OK.");
                BrowserManagement.SetBrowser();
                MessageBox.Show("And on this step, choose your osu! folder.");
                DbReader.SetOsuPath();
                return;
            }

            if (args.Length == 0 || RegistryManagement.IsAdministrator() || !BrowserManagement.IsSet())
            {
                MessageBox.Show("Run the program as administrator for the initial setup.");
                return;
            }

            foreach (string arg in args)
            {

                if (!LinkManagement.IsMapLink(arg))
                {
                    Process.Start(DbReader.OsuPathKey.GetValue("BrowserPath").ToString(), arg);
                    return;

                }

                if (LinkManagement.GetSetId(arg) == null)
                {
                    MessageBox.Show("The map couldn't be found on chimu.moe.");
                    Process.Start(DbReader.OsuPathKey.GetValue("BrowserPath").ToString(), arg);
                    return;
                }

                if (!DbReader.IsDownloaded(arg))
                {
                    DownloadManagement.DownloadMap(arg);

                    Process.Start(@"C:\Windows\Temp\" + LinkManagement.GetFileName(arg));

                    return;
                }
                Process.Start(DbReader.OsuPathKey.GetValue("BrowserPath").ToString(), arg);
            }
        }
    }
}