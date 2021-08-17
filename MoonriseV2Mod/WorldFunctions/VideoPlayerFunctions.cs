using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoonriseV2Mod.Patches;
using UshioUI;

namespace MoonriseV2Mod.WorldFunctions
{
    internal static class VideoPlayerFunctions
    {
        public static string currentVideoUrl;

        public static void CopyVideoUrl()
        {
            if (currentVideoUrl == "") return;
            System.Windows.Forms.Clipboard.SetText(currentVideoUrl);
            UshioMenuApi.PopupUI("Copied Video URL to Clipboard", "World Functions");
        }
        public static void OpenVideoUrlInBrowser()
        {
            if (currentVideoUrl == "") return;
            System.Diagnostics.Process.Start(currentVideoUrl);
            UshioMenuApi.PopupUI("Opened URL in Browser", "World Functions");
        }

        public static string SetVideoURL(string url) => currentVideoUrl = url;
    }
}
