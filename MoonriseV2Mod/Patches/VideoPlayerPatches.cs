using HarmonyLib;
using MoonriseV2Mod.WorldFunctions;
using UnityEngine.Video;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(VideoPlayer), "url", MethodType.Setter)]
    class VideoPlayerUrlPatch
    {
        static bool Prefix(string value)
        {
            if (VideoPlayerFunctions.currentVideoUrl != value)
            {
                if (value != "" )
                    VideoPlayerFunctions.SetVideoURL(value);
                //MoonriseConsole.Log(value);
            }

            return true;
        }
    }
}
