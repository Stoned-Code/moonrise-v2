﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MoonriseV2Mod.Settings
{
    public class MRConfiguration
    {
        [JsonIgnore] public static MRConfiguration config;
        [JsonProperty] public string moonriseKey { get; internal set; } = "FreeUser";
        [JsonProperty] public Dictionary<string, string> ignoreList { get; internal set; } = new Dictionary<string, string>();
        [JsonProperty] public int avatarHiderState { get; internal set; } = 1;
        [JsonProperty] public bool avatarsShowing { get; internal set; }
        [JsonProperty] public float avatarHiderDistance { get; internal set; } = 7f;
        [JsonProperty] public bool addBlocker { get; internal set; } = true;
        [JsonProperty] public float portableMirrorWidth { get; internal set; } = 1;
        [JsonProperty] public float portableMirrorHeight { get; internal set; } = 2;
        [JsonProperty] public bool UiDisplayEnabled { get; internal set; } = true;
        [JsonProperty] public bool enlargeEbookOnGrab { get; internal set; } = true;
        [JsonProperty] public bool antiPortal { get; internal set; } = false;
        [JsonProperty] public bool walkThrough { get; internal set; } = true;
        [JsonProperty] public bool joinNotifier { get; internal set; } = false;
        [JsonProperty] public bool allUsersJn { get; internal set; } = false;
        [JsonProperty] public bool hideCrashers { get; internal set; } = true;


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

            config = JsonConvert.DeserializeObject<MRConfiguration>(json) ?? new MRConfiguration();

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
