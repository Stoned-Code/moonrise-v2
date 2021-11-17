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
    internal struct ModInfo
    {
        public const string modName = "Moonrise";
        public const string modAuthor = "Stoned Code";
        public const string modVersion = "2.0.0";
        public const string buildNumber = "21";
        public const string modDownload = "https://github.com/Stoned-Code/Moonrise/releases/download/" + buildNumber + "/MoonriseV2.dll";
    }
}
