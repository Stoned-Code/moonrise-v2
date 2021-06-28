using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using VRC.Core;

namespace MoonriseV2Mod.API
{
    public sealed class MRUser
    {
        [JsonProperty] public string DisplayName { get; set; }
        [JsonProperty] public string UserId { get; set; }
        [JsonProperty] public bool Premium { get; set; }
        [JsonProperty] public bool Lewd { get; set; }
        [JsonProperty] public string MoonriseKey { get; set; }
        [JsonProperty] public bool isMoonriseUser { get; set; }

        [JsonIgnore] internal static string URL = "https://moonrise-sc.loca.lt/moonriseuser";

        public static MRUser GetUser(string key)
        {
            try
            {
                MRUser user = new MRUser();
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(URL);
                wr.Accept = "application/json";
                wr.ContentType = "application/json";
                wr.Method = "POST";

                user.MoonriseKey = key;
                user.UserId = APIUser.CurrentUser.id;
                string content = JsonConvert.SerializeObject(user);
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(content);
                
                string json = "";
                using (Stream stream = wr.GetRequestStream())
                {

                    stream.Write(bytes, 0, bytes.Length);
                    var response = wr.GetResponse();

                    var str = response.GetResponseStream();
                    using (StreamReader sr = new StreamReader(str))
                    {
                        json = sr.ReadToEnd();
                    }
                }

                json = json.Replace("[", "");
                json = json.Replace("]", "");
                if (json.Contains("\"{"))
                    json = json.Replace("\"{", "{");
                if (json.Contains("}\""))
                    json = json.Replace("}\"", "}");
                if (json.Contains("\\"))
                    json = json.Replace("\\", "");
                MoonriseConsole.Log(json);
                user = JsonConvert.DeserializeObject<MRUser>(json) ?? null;
                if (!user.isMoonriseUser || user == null) return null;

                return user;
            }

            catch (Exception ex)
            {
                // MoonriseConsole.ErrorLog($"Error Getting MRUser...\n{ex}");
                return null;
            }
        }

    }
}
