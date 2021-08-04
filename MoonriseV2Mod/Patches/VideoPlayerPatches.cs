using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine.Video;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(VideoPlayer), "url", MethodType.Setter)]
    class VideoPlayerPlayPatch
    {
        public static string currentVideoLink { get; private set; }
        [HarmonyPostfix]
        static void URL(string value)
        {
            if (currentVideoLink != value)
            {
                currentVideoLink = value;
                MoonriseConsole.Log(value);
            }

        }
    }
}
