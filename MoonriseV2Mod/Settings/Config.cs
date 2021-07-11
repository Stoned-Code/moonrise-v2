using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MoonriseV2Mod.Settings
{
    public class Config
    {
        [JsonIgnore] public static Config config;
        [JsonProperty] public string moonriseKey { get; set; } = "FreeUser";
        [JsonProperty] public Dictionary<string, string> ignoreList { get; set; } = new Dictionary<string, string>();
        [JsonProperty] public int avatarHiderState { get; set; } = 1;
        [JsonProperty] public bool avatarsShowing { get; set; }
        [JsonProperty] public float avatarHiderDistance { get; set; } = 7f;
        [JsonProperty] public bool addBlocker { get; set; } = true;
        [JsonIgnore] public static string rootDirectory = Environment.CurrentDirectory;
        public static void Initialize()
        {


            // rootDirectory += @"\..\";

            string moonriseDirectory = Path.Combine(rootDirectory, "Moonrise");
            string configPath = Path.Combine(moonriseDirectory, "config.json");

            string json = "";

            if (File.Exists(configPath))
                using (StreamReader sr = new StreamReader(configPath))
                {
                    json = sr.ReadToEnd();
                }

            config = JsonConvert.DeserializeObject<Config>(json) ?? new Config();

            if (!Directory.Exists(moonriseDirectory))
                Directory.CreateDirectory(moonriseDirectory);
            if (!File.Exists(configPath))
            {
                config.WriteConfig();
            }

            /////////////////
            //  Key Check  //
            /////////////////
            
            string keyPath = Path.Combine(rootDirectory, "MoonriseKey.txt");
            if (File.Exists(keyPath))
            {
                config.moonriseKey = File.ReadAllLines(keyPath)[0];
                File.Delete(keyPath);
            }

            config.WriteConfig();
        }

        public void WriteConfig()
        {
            string rootDirectory = Environment.CurrentDirectory;

            string moonriseDirectory = Path.Combine(rootDirectory, "Moonrise");
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(Path.Combine(moonriseDirectory, "config.json")))
            {
                sw.Write(json);
            }
        }
    }
}
