using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using MoonriseUpdater;

namespace MoonriseV2Mod.Settings
{
    internal class ModInfo
    {
        public static ModInfo modInfo;
        public ModInfo()
        {
            this.downloadLink = "";
            this.modBuild = 11;
            this.modChanges = new string[0];
        }

        [JsonProperty] public string downloadLink { get; set; }
        [JsonProperty] public string pluginLink { get; set; }
        [JsonProperty] public bool updatePlugin { get; set; }
        [JsonProperty] public int modBuild { get; set; }
        [JsonProperty] public string[] modChanges { get; set; }

        public void UpdateInfoFile()
        {
            string json = JsonConvert.SerializeObject(this);

            using (StreamWriter writer = new StreamWriter(infoPath))
                writer.Write(json);
        }

        public string Changes()
        {
            string infoString = "";

            if (!(modChanges.Length > 0))
                return infoString;

            infoString += "\nChanges:";

            foreach (string change in modChanges)
                infoString += $"\n{change}";
            
            return infoString;
        }

        public static bool isUpdated
        {
            get
            {
                string url = MoonriseLoader.WorkingUrl;
                if (BaseEncoding.Decoder(url) == "N/A") return true;
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(BaseEncoding.Decoder(url) + "/slkefgdga9e3d");
                wr.ContentType = "application/json";
                wr.Timeout = 1500;
                wr.Method = "POST";


                string content = JsonConvert.SerializeObject(modInfo);
                UTF8Encoding encoding = new UTF8Encoding();
                Byte[] bytes = encoding.GetBytes(content);

                string json = "";
                using (Stream stream = wr.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);

                    var response = wr.GetResponse();

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        json = sr.ReadToEnd();
                        if (json == "Up to date!")
                        {
                            MoonriseLoader.Log("Moonrise is up to date!");
                            return true;
                        }
                    }
                }

                modInfo = JsonConvert.DeserializeObject<ModInfo>(json);

                return false;
            }
        }
        public static bool changesAvailable
        {
            get
            {
                return modInfo.modChanges.Length > 0;
            }
        }
        public static string infoPath => Path.Combine(Environment.CurrentDirectory, "Moonrise", "modInfo.json");

        public static void CheckUpdate()
        {
            modInfo = GetModInfo();

            if (isUpdated) return;

            var modDirectory = Path.Combine(Environment.CurrentDirectory, "Mods", "MoonriseV2.dll");

            File.Delete(modDirectory);

            using (WebClient webClient = new WebClient())
            {
                modInfo.downloadLink = BaseEncoding.Decoder(modInfo.downloadLink);
                MoonriseLoader.Log(modInfo.downloadLink);
                webClient.DownloadFile(modInfo.downloadLink, modDirectory);
            }

            if (modInfo.updatePlugin)
            {
                string pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins", "MoonriseUpdater.dll");
                using (WebClient client = new WebClient())
                {
                    modInfo.pluginLink = BaseEncoding.Decoder(modInfo.pluginLink);
                    MoonriseLoader.Log(modInfo.pluginLink);
                    client.DownloadFile(modInfo.pluginLink, pluginPath);
                }
            }

            modInfo.UpdateInfoFile();

            MoonriseLoader.Log("Moonrise has updated!");
            MoonriseLoader.Log(modInfo.Changes());
        }

        public static ModInfo GetModInfo()
        {
            string json = "";
            if (!File.Exists(infoPath))
            {
                var info = new ModInfo();
                json = JsonConvert.SerializeObject(info);
                using (StreamWriter writer = new StreamWriter(infoPath))
                {
                    writer.Write(json);

                }

                return info;
            }
            File.SetAttributes(infoPath, FileAttributes.Hidden);
            using (StreamReader reader = new StreamReader(infoPath))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<ModInfo>(json) ?? new ModInfo();
        }
    }

    internal class PingResponse
    {
        [JsonProperty] public bool foundBackend { get; set; }
    }
}