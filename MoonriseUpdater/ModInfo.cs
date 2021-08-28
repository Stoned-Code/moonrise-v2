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

namespace MoonriseV2Mod.Settings
{
    internal class ModInfo
    {
        public static ModInfo modInfo;
        public ModInfo()
        {
            this.downloadLink = "";
            this.modBuild = 13;
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
            modInfo = new ModInfo();

            string assemblyVersion = null;
            string modDirectory = Path.Combine(Environment.CurrentDirectory, "Mods", "MoonriseV2.dll");

            if (File.Exists(modDirectory))
            {
                using (AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(modDirectory, new ReaderParameters { ReadWrite = true }))
                {
                    CustomAttribute melonInfoAttribute = assembly.CustomAttributes.First(a => a.AttributeType.Name == "AssemblyFileVersionAttribute");
                    assemblyVersion = melonInfoAttribute.ConstructorArguments[0].Value as string;
                    modInfo.modBuild = Int32.Parse(assemblyVersion);
                }
            }

            else
            {
                DownloadMoonrise(modDirectory);

                using (AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(modDirectory, new ReaderParameters { ReadWrite = true }))
                {
                    CustomAttribute melonInfoAttribute = assembly.CustomAttributes.First(a => a.AttributeType.Name == "AssemblyFileVersionAttribute");
                    assemblyVersion = melonInfoAttribute.ConstructorArguments[0].Value as string;
                    modInfo.modBuild = Int32.Parse(assemblyVersion);
                }
            }

            if (isUpdated) return;

            UpdateMod(modDirectory);

            if (modInfo.updatePlugin)
                UpdatePlugin();

            MoonriseLoader.Log("Moonrise has updated!");
        }

        public static void DownloadMoonrise(string modDirectory)
        {
            string pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins", "MoonriseUpdater.dll");
            MoonriseLoader.Log("Moonrise not in folder.");
            var tempUrl = MoonriseLoader.WorkingUrl;
            if (BaseEncoding.Decoder(tempUrl) == "N/A")
                return;

            WebRequest wr = WebRequest.Create(tempUrl + "/owselsdfkolfglkag");
            wr.Timeout = 1500;
            wr.Method = "GET";
            wr.Proxy = null;

            string json = "";

            try
            {
                WebResponse res = wr.GetResponse();

                using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                    json = sr.ReadToEnd();
            }

            catch
            {
                return;
            }

            JObject jobj = JsonConvert.DeserializeObject(json) as JObject;

            byte[] data;
            using (WebClient client = new WebClient())
            {
                data = client.DownloadData(BaseEncoding.Decoder(jobj.GetValue("downloadLink")?.ToString()));
            }

            File.WriteAllBytes(modDirectory, data);

            using (WebClient client = new WebClient())
            {
                data = client.DownloadData(BaseEncoding.Decoder(jobj.GetValue("pluginLink")?.ToString()));
            }

            File.WriteAllBytes(pluginPath, data);
        }

        public static void UpdateMod(string modDirectory)
        {
            byte[] data;
            File.Delete(modDirectory);
            using (WebClient webClient = new WebClient())
            {

                modInfo.downloadLink = BaseEncoding.Decoder(modInfo.downloadLink);
                MoonriseLoader.Log(modInfo.downloadLink);
                data = webClient.DownloadData(modInfo.downloadLink);
            }
            File.WriteAllBytes(modDirectory, data);
        }

        public static void UpdatePlugin()
        {
            string pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins", "MoonriseUpdater.dll");
            byte[] data;
            File.Delete(pluginPath);
            using (WebClient client = new WebClient())
            {
                modInfo.pluginLink = BaseEncoding.Decoder(modInfo.pluginLink);
                MoonriseLoader.Log(modInfo.pluginLink);
                data = client.DownloadData(modInfo.pluginLink);
            }

            File.WriteAllBytes(pluginPath, data);
        }
    }

    internal class PingResponse
    {
        [JsonProperty] public bool foundBackend { get; set; }
    }
}