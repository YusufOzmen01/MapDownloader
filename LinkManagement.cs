using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MapDownloader
{
    internal class LinkManagement
    {

        public static bool IsMapLink(string link)
        {
            // MATCH WITH REGEX https?:\/\/osu\.ppy\.sh\/beatmaps\/\d+
            // THEN RETURN TRUE IF IT MATCHES WITH ANYTHING
        }

        public static string GetSetId(string link)
        {
            /*
                import requests
                import re 

                def get_set_id(url):
                    res = requests.head(url, allow_redirects=True)

                    if res.status_code < 200 or res.status_code >= 400:
                        return None
                    else:
                        return re.search("[0-9]+", res.request.url)[0]

                get_set_id("https://osu.ppy.sh/beatmapsets/1")
            */

            // IMPLEMENT THIS IN C#
        }

        public static string GetFileName(string link)
        {
            try
            {
                var wc = new WebClient();

                string chimuResponse = wc.DownloadString("https://api.chimu.moe/v1/set/" + GetSetId(link));

                ChimuSetJSON m = JsonConvert.DeserializeObject<ChimuSetJSON>(chimuResponse);

                return m.SetId + " " + m.Artist_Unicode + " - " + m.Title_Unicode + ".osz";
            }
            catch (WebException)
            {
                return null;
            }
        }

        public static string GetFileHash(string link)
        {
            string id = "";
            foreach (char c in link)
            {
                if (char.IsNumber(c))
                {
                    id += c;
                }
            }

            try
            {
                var wc = new WebClient();

                string chimuResponse = wc.DownloadString("https://api.chimu.moe/v1/map/" + id);

                ChimuDiffJSON m = JsonConvert.DeserializeObject<ChimuDiffJSON>(chimuResponse);

                return m.FileMD5;
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
    class ChimuDiffJSON
    {
        public string ParentSetId { get; set; }
        public string FileMD5 { get; set; }
        public string error_code { get; set; }
    }
    
    class ChimuSetJSON
    {
        public string SetId { get; set; }
        public string Title_Unicode { get; set;}
        public string Artist_Unicode { get; set;}
    }
}
