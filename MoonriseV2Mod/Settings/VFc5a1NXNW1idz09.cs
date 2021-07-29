using MoonriseV2Mod.API;
using MoonriseV2Mod.BaseFunctions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoonriseV2Mod.Settings
{
    internal class VFc5a1NXNW1idz09
    {
        public static VFc5a1NXNW1idz09 modInfo;
        public const string modName = "Moonrise";
        public const string modVersion = "2.0.0";
        public const string modAuthor = "Stoned Code";
        public const string modDownload = "N/A";
        [JsonProperty] public string downloadLink { get; set; }
        [JsonProperty] public int modBuild = 7;
        [JsonProperty] public string[] modChanges = new string[0];
        [JsonIgnore] public static bool isUpdated
        {
            get
            {
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(TVJVc2Vy.WorkingUrl + "/slkefgdga9e3d");
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

                modInfo = JsonConvert.DeserializeObject<VFc5a1NXNW1idz09>(json);

                return false;
            }
        }
        [JsonIgnore] public static bool changesAvailable
        {
            get
            {
                return modInfo.modChanges.Length > 1;
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
            VFc5a1NXNW1idz09.modInfo = new VFc5a1NXNW1idz09();
        }

        static bool isUpdating = false;
        public static void CheckUpdate()
        {
            if (isUpdated) return;
            if (isUpdating) return;
            isUpdating = true;
            var rootDirectory = Environment.CurrentDirectory;

            var subDirectory = Path.Combine(rootDirectory, "Mods");
            var modDirectory = Path.Combine(subDirectory, "MoonriseV2.dll");

            File.Delete(modDirectory);
            using (WebClient webClient = new WebClient())
            {
                modInfo.downloadLink = VWxjMWFtSXlVbkJpYldSQ1kwZHJQUT09.UkdWamIyUmxjZz09(modInfo.downloadLink);
                // MoonriseConsole.Log(modInfo.downloadLink);
                webClient.DownloadFile(modInfo.downloadLink, modDirectory);
            }

            isUpdating = false;
            TW9vbnJpc2VCYXNlRnVuY3Rpb25z.baseFunctions.menuTab.SetBadgeActive(true, "Update!", Color.blue);
            UshioUI.UshioMenuApi.PopupUI("Moonrise Updated!\nRestart for update to take affect");
            MoonriseConsole.Log("Moonrise has updated! Restart for update to take affect.");
        }
    }
}
