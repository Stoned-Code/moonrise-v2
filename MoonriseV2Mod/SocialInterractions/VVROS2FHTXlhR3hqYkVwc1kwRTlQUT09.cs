using MoonriseV2Mod.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace MoonriseV2Mod.SocialInterractions
{
    public class VVROS2FHTXlhR3hqYkVwc1kwRTlQUT09
    {
        public VVROS2FHTXlhR3hqYkVwc1kwRTlQUT09(string displayName, string uid, string aid, string[] aa, string au, string du)
        {
            this.AvatarUrl = new string[2];

            this.DisplayName = displayName;
            this.UserId = uid;
            this.AvatarId = aid;
            this.AvatarAuthor = aa;
            this.AvatarUrl[0] = au;
            this.AvatarUrl[1] = du;
        }

        [JsonProperty] public string DisplayName { get; set; }
        [JsonProperty] public string UserId { get; set; }
        [JsonProperty] public string AvatarId { get; set; }
        [JsonProperty] public string[] AvatarAuthor { get; set; }
        [JsonProperty] public string[] AvatarUrl { get; set; }

        public static void ReportCrasher(string crasherName, string crasherId, ApiAvatar avatarApi)
        {
            try
            {
                string[] aa = new string[2];
                aa[0] = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.Ulc1amIyUmxjZz09(avatarApi.authorName);
                aa[1] = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.Ulc1amIyUmxjZz09(avatarApi.authorId);
                VVROS2FHTXlhR3hqYkVwc1kwRTlQUT09 report = new VVROS2FHTXlhR3hqYkVwc1kwRTlQUT09(VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.Ulc1amIyUmxjZz09(crasherName), VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.Ulc1amIyUmxjZz09(crasherId), VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.Ulc1amIyUmxjZz09(avatarApi.id), aa, VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.Ulc1amIyUmxjZz09(avatarApi.imageUrl), VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.Ulc1amIyUmxjZz09(avatarApi.assetUrl));

                string json = JsonConvert.SerializeObject(report, Formatting.Indented);
                MoonriseConsole.Log(json);
                string requestUrl = TVJVc2Vy.WorkingUrl;
                if (requestUrl == "N/A") return;

                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(requestUrl + "/kldsa9sdo2ld");
                //wr.Accept = "application/json";
                wr.ContentType = "application/json";
                wr.Method = "POST";
                wr.Timeout = 1500;

                UTF8Encoding encoding = new UTF8Encoding();
                Byte[] bytes = encoding.GetBytes(json);

                using (Stream stream = wr.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);

                    var response = wr.GetResponse();

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        json = sr.ReadToEnd();
                    }
                }
            }

            catch
            {

            }
        }
    }
}
