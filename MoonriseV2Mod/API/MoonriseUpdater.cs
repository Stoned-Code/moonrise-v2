using Mono.Cecil;
using MoonriseV2Mod.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoonriseV2Mod.API
{
    class MoonriseUpdater
    {
        private static string pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins", "MoonriseUpdater.dll");
        private static string modPath = Path.Combine(Environment.CurrentDirectory, "Mods", "MoonriseV2.dll");

        public static void CheckUpdate()
        {
            string currentVersion = ModInfo.buildNumber;
            string latestVersion = GetLatestVersion();

            if (!File.Exists(modPath))
            {
                DownloadMoonrise(latestVersion);
            }

            if (currentVersion != latestVersion)
            {
                DownloadMoonrise(latestVersion);
                DownloadPlugin(latestVersion);
            }

            else
            {
                MoonriseConsole.Log("Moonrise is Up to date!");
            }
        }

        private static void DownloadMoonrise(string version)
        {
            MoonriseConsole.Log("Downloading MoonriseV2...");

            byte[] data;
            using (WebClient wc = new WebClient())
            {
                data = wc.DownloadData($"https://github.com/Stoned-Code/Moonrise/releases/download/{version}/MoonriseV2.dll");
            }

            File.WriteAllBytes(modPath, data);

            MoonriseConsole.Log("Finished downloading MoonriseV2!");
        }

        private static void DownloadPlugin(string version)
        {
            MoonriseConsole.Log("Downloading Updater Plugin...");

            byte[] data;
            using (WebClient wc = new WebClient())
            {
                data = wc.DownloadData($"https://github.com/Stoned-Code/Moonrise/releases/download/{version}/MoonriseUpdater.dll");
            }

            File.WriteAllBytes(pluginPath, data);

            MoonriseConsole.Log("Finished downloading Updater Plugin!");
        }

        private static string GetLatestVersion()
        {
            string githubResponse;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/Stoned-Code/Moonrise/releases/latest");
                request.Method = "GET";
                request.KeepAlive = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = $"Moonrise/{ModInfo.buildNumber}";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader requestReader = new StreamReader(response.GetResponseStream()))
                {
                    githubResponse = requestReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MoonriseConsole.ErrorLog("Failed to fetch latest plugin version info from github:\n" + e);
                return ModInfo.buildNumber;
            }

            JObject obj = JsonConvert.DeserializeObject(githubResponse) as JObject;

            return obj.GetValue("tag_name")?.ToString();
        }
    }
}
