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

        [JsonProperty] public string downloadLink { get; set; }
        [JsonProperty] public string pluginLink { get; set; }
        [JsonProperty] public bool updatePlugin { get; set; }
        [JsonProperty] public int modBuild = 11;
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

        public void UpdateInfoFile()
        {
            string json = JsonConvert.SerializeObject(this);

            using (StreamWriter writer = new StreamWriter(infoPath))
            {
                writer.Write(json);
            }
        }

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
            modInfo = GetModInfo();
            string pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins", "MoonriseUpdater.dll");

            if (!File.Exists(pluginPath))
            {
                using (WebClient webClient = new WebClient())
                {
                    modInfo.pluginLink = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09("aHR0cHM6Ly9kcml2ZS5nb29nbGUuY29tL2ZpbGUvZC8xTmFLbUZNMlZpcXY5ejViWWlHV0NEYzJFeHFvS0xGSXI=");

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

            File.Delete(modDirectory);
            using (WebClient webClient = new WebClient())
            {
                modInfo.downloadLink = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09(modInfo.downloadLink);

                webClient.DownloadFile(modInfo.downloadLink, modDirectory);
            }

            if (modInfo.updatePlugin)
            {
                using (WebClient client = new WebClient())
                {
                    modInfo.pluginLink = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09(modInfo.pluginLink);

                    client.DownloadFile(modInfo.pluginLink, pluginPath);
                }
            }

            modInfo.UpdateInfoFile();

            isUpdating = false;
            MoonriseBaseFunctions.baseFunctions.menuTab.SetBadgeActive(true, "Update!", Color.blue);
            UshioUI.UshioMenuApi.PopupUI("Moonrise Updated!\nRestart for update to take affect");
            MoonriseConsole.Log("Moonrise has updated! Restart for update to take affect.");
        }

        public static void DownloadUpdater()
        {
            string pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins", "MoonriseUpdater.dll");
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

            using (StreamReader reader = new StreamReader(infoPath))
            {
                json = reader.ReadToEnd();
            }

            File.SetAttributes(infoPath, FileAttributes.Hidden);

            return JsonConvert.DeserializeObject<ModInfo>(json) ?? new ModInfo();
        }
    }
}
