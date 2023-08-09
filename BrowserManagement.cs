using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MapDownloader
{
    internal class BrowserManagement
    {
        public static void SetBrowser()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Exe Files (.exe)|*.exe";
                ofd.Title = "Choose your default browser's executable.";
                string BrowserPath;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    BrowserPath = ofd.FileName;
                    RegistryKey browserPathKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MapDownloader");
                    browserPathKey.SetValue("BrowserPath", BrowserPath);
                }
            }
        }
        public static bool IsSet()
        {
            return Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\MapDownloader", "BrowserPath", null) != null;
        }
    }
}
