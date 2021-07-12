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
    public class CrasherReport
    {
        public CrasherReport(string displayName, string uid, string aid, string[] aa, string au, string du)
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
            string[] aa = new string[2];
            aa[0] = EncodingApi.Encoder(avatarApi.authorName);
            aa[1] = EncodingApi.Encoder(avatarApi.authorId);
            CrasherReport report = new CrasherReport(EncodingApi.Encoder(crasherName), EncodingApi.Encoder(crasherId), EncodingApi.Encoder(avatarApi.id), aa, EncodingApi.Encoder(avatarApi.imageUrl), EncodingApi.Encoder(avatarApi.assetUrl));

            string json = JsonConvert.SerializeObject(report, Formatting.Indented);
            MoonriseConsole.Log(json);
            string requestUrl = MRUser.WorkingUrl;
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
    }
}
