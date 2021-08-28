using MoonriseV2Mod.API;
using MoonriseV2Mod.BaseFunctions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace MoonriseV2Mod.Settings
{
    internal class ModInfo
    {
        public static ModInfo modInfo;
        public const string modName = "Moonrise";
        public const string modVersion = "2.0.0";
        public const string modAuthor = "Stoned Code";
        public const string modDownload = "N/A";
        public const string buildNumber = "13";

        [JsonProperty] public string downloadLink { get; set; }
        [JsonProperty] public string pluginLink { get; set; }
        [JsonProperty] public bool updatePlugin { get; set; }
        [JsonProperty] public int modBuild = Int32.Parse(buildNumber);
        [JsonProperty] public string[] modChanges = new string[0];

        public static bool isUpdated
        {
            get
            {
                string url = TVJVc2Vy.WorkingUrl;
                if (VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09(url) == "N/A") return true;
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09(url) + "/slkefgdga9e3d");
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
                            MoonriseConsole.Log("Moonrise is up to date!");
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

        private static bool isUpdating = false;

        public static string infoPath => Path.Combine(Environment.CurrentDirectory, "Moonrise", "modInfo.json");

        public string ChangesToString()
        {
            string changeString = "";
            changeString += $"<color=cyan>Changes:</color>\n";
            for (int i = 0; i < modChanges.Length; i++)
            {
                string change = modChanges[i];
                change = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09(change);

                if (change != "")
                    changeString += $"- {change}\n";
            }
            changeString += "\n";
            return changeString;
        }

        public static void Initialize()
        {
            modInfo = new ModInfo();
            string pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins", "MoonriseUpdater.dll");

            if (!File.Exists(pluginPath))
            {
                using (WebClient webClient = new WebClient())
                {
                    modInfo.pluginLink = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09("aHR0cHM6Ly9naXRodWIuY29tL1N0b25lZC1Db2RlL01vb25yaXNlL3JlbGVhc2VzL2Rvd25sb2FkLzEyL01vb25yaXNlVXBkYXRlci5kbGw=");

                    webClient.DownloadFile(modInfo.pluginLink, pluginPath);
                }
            }
        }

        public static void CheckUpdate()
        {
            if (isUpdated) return;
            if (isUpdating) return;
            isUpdating = true;
            string modDirectory = Path.Combine(Environment.CurrentDirectory, "Mods", "MoonriseV2.dll");
            string pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins", "MoonriseUpdater.dll");

            DownloadUpdate(modDirectory);

            if (modInfo.updatePlugin)
                DownloadUpdater(pluginPath);

            isUpdating = false;
            MoonriseBaseFunctions.baseFunctions.menuTab.SetBadgeActive(true, "Update!", Color.blue);
            UshioUI.UshioMenuApi.PopupUI("Moonrise Updated!\nRestart for update to take affect");
            MoonriseConsole.Log("Moonrise has updated! Restart for update to take affect.");
        }

        public static void DownloadUpdate(string modDirectory)
        {
            byte[] data;
            File.Delete(modDirectory);
            using (WebClient webClient = new WebClient())
            {
                modInfo.downloadLink = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09(modInfo.downloadLink);

                data = webClient.DownloadData(modInfo.downloadLink);
            }

            File.WriteAllBytes(modDirectory, data);
        }

        public static void DownloadUpdater(string pluginPath)
        {
            byte[] data;
            File.Delete(pluginPath);
            using (WebClient client = new WebClient())
            {
                modInfo.pluginLink = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09(modInfo.pluginLink);

                data = client.DownloadData(modInfo.pluginLink);
            }

            File.WriteAllBytes(pluginPath, data);
            
        }
    }
}
