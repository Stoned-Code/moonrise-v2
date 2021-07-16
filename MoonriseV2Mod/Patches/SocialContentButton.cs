using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(VRCUiContentButton), "Press")]
    class SocialContentButton
    {
        [HarmonyPostfix]
        static void UserInfo(VRCUiContentButton __instance)
        {
            if (!VFc5dmJuSnBjMlU9.debug) return;
            TW9vbnJpc2VDb25zb2xl.Log("Opened User " + __instance.field_Public_Text_0.text + "!");
        }
    }
}
