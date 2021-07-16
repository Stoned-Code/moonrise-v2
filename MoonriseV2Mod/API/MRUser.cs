using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
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
        [JsonProperty] public string AvatarUrl { get; set; }
        [JsonIgnore] internal static string baseUrl = "loca.lt";
        [JsonIgnore] internal static bool debug = false;
        internal static string WorkingUrl
        {
            get
            {
                //string extra = debug ? "t" : "";
                string tempUrl = $"https://moonrise-sc.{baseUrl}";
                WebRequest wr = WebRequest.Create(tempUrl + "/md9fjtnj4dm");
                wr.Timeout = 1500;
                wr.Method = "GET";

                string json = "";
                // MoonriseConsole.Log($"Checking {tempUrl}");
                try
                {
                    WebResponse res = wr.GetResponse();
                    // MoonriseConsole.Log($"Received response...");
                    using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                    {
                        json = sr.ReadToEnd();
                        // MoonriseConsole.Log(json);
                    }
                }

                catch
                {

                }

                PingResponse pRes = JsonConvert.DeserializeObject<PingResponse>(json);

                if (pRes != null && pRes.foundBackend)
                    return tempUrl;

                for (int i = 1; i < 10; i++)
                {
                    try
                    {
                        tempUrl = $"https://moonrise-sc-{i}.{baseUrl}";

                        wr.Abort();
                        wr = WebRequest.Create(tempUrl + "/md9fjtnj4dm");
                        wr.Timeout = 1500;
                        MoonriseConsole.Log($"Checking {tempUrl}");
                        WebResponse res = wr.GetResponse();

                        using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                        {
                            json = sr.ReadToEnd();
                            // MoonriseConsole.Log(json);
                        }

                        pRes = JsonConvert.DeserializeObject<PingResponse>(json);

                        if (pRes.foundBackend)
                            return tempUrl;
                    }

                    catch { }
                }

                return "N/A";
            }
        }

        public static MRUser GetUser(string key)
        {
            try
            {
                string requestUrl = WorkingUrl;
                if (requestUrl == "N/A") return null;

                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(requestUrl + "/ykmhuuvlby");
                //wr.Accept = "application/json";
                wr.ContentType = "application/json";
                wr.Method = "POST";
                wr.Timeout = 1500;

                MRUser user = new MRUser();
                user.MoonriseKey = EncodingApi.Encoder(key);
                user.UserId = EncodingApi.Encoder(APIUser.CurrentUser.id);
                user.AvatarUrl = EncodingApi.Encoder(APIUser.CurrentUser.currentAvatarImageUrl);
                
                string content = JsonConvert.SerializeObject(user);
                UTF8Encoding encoding = new UTF8Encoding();
                Byte[] bytes = encoding.GetBytes(content);
                // MoonriseConsole.Log(APIUser.CurrentUser.ToString());
                string json = "";
                using (Stream stream = wr.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);

                    using (StreamReader sr = new StreamReader(wr.GetResponse().GetResponseStream(), Encoding.UTF8))
                    {
                        json = sr.ReadToEnd();
                    }
                }
                // MoonriseConsole.Log(json);
                if (json != "Denied access...")
                {
                    if (json.Contains("\"{"))
                        json = json.Replace("\"{", "{");
                    if (json.Contains("}\""))
                        json = json.Replace("}\"", "}");
                    if (json.Contains("\\"))
                        json = json.Replace("\\", "");

                    user = JsonConvert.DeserializeObject<MRUser>(json);
                    user.DisplayName = EncodingApi.Decoder(user.DisplayName);
                    user.UserId = EncodingApi.Decoder(user.UserId);
                    user.MoonriseKey = EncodingApi.Decoder(user.MoonriseKey);
                }

                if (!user.isMoonriseUser || user == null) return null;
                user.MoonriseKey = EncodingApi.Decoder(user.MoonriseKey);

                return user ?? null;
            }

            catch (Exception ex)
            {
                // MoonriseConsole.ErrorLog($"Error Getting MRUser...\n{ex}");
                return null;
            }
        }

        public override string ToString()
        {
            return $"Display Name: {DisplayName}\nUser ID: {UserId}\nPremium: {Premium}\nLewd: {Lewd}\nMoonrise Key: {MoonriseKey}";
        }
    }

    public class PingResponse
    {
        [JsonProperty] public bool foundBackend { get; set; }
    }
}
