using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using MoonriseUpdater;
using MelonLoader;
using Mono.Cecil;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MoonriseV2Mod.Settings
{
    internal class ModUpdater
    {
        internal const string VERSION = "21";

        private static string pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins", "MoonriseUpdater.dll");
        private static string modPath = Path.Combine(Environment.CurrentDirectory, "Mods", "MoonriseV2.dll");

        public static void CheckUpdate()
        {
            if (File.Exists(modPath))
            {
                File.Delete(modPath);
            }

            if (File.Exists(pluginPath))
            {
                File.Delete(pluginPath);
            }

            //string currentVersion;
            //string latestVersion = GetLatestVersion();

            //if (!File.Exists(modPath))
            //{
            //    DownloadMoonrise(latestVersion);
            //}

            //using (AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(modPath, new ReaderParameters { ReadWrite = true }))
            //{
            //    CustomAttribute moonriseInfoAttribute = assembly.CustomAttributes.First(a => a.AttributeType.Name == "MoonriseInfoAttribute");
            //    currentVersion = moonriseInfoAttribute.ConstructorArguments[0].Value as string;
            //}

            //if (currentVersion != latestVersion)
            //{
            //    DownloadMoonrise(latestVersion);
            //    DownloadPlugin(latestVersion);
            //}

            //else
            //{
            //    MoonriseLoader.Log("Moonrise is Up to date!");
            //}
        }

        private static void DownloadMoonrise(string version)
        {
            MoonriseLoader.Log("Downloading MoonriseV2...");

            byte[] data;
            using (WebClient wc = new WebClient())
            {
                data = wc.DownloadData($"https://github.com/Stoned-Code/Moonrise/releases/download/{version}/MoonriseV2.dll");
            }

            File.WriteAllBytes(modPath, data);

            MoonriseLoader.Log("Finished downloading MoonriseV2!");
        }

        private static void DownloadPlugin(string version)
        {
            MoonriseLoader.Log("Downloading Updater Plugin...");

            byte[] data;
            using (WebClient wc = new WebClient())
            {
                data = wc.DownloadData($"https://github.com/Stoned-Code/Moonrise/releases/download/{version}/MoonriseUpdater.dll");
            }

            File.WriteAllBytes(pluginPath, data);

            MoonriseLoader.Log("Finished downloading Updater Plugin!");
        }

        private static string GetLatestVersion()
        {
            string githubResponse;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(MoonriseLoader.gitApi);
                request.Method = "GET";
                request.KeepAlive = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = $"Moonrise/{VERSION}";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader requestReader = new StreamReader(response.GetResponseStream()))
                {
                    githubResponse = requestReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MelonLogger.Error("Failed to fetch latest plugin version info from github:\n" + e);
                return VERSION;
            }

            JObject obj = JsonConvert.DeserializeObject(githubResponse) as JObject;

            return obj.GetValue("tag_name")?.ToString();
        }
    }
}