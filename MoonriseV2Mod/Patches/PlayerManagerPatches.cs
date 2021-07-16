using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UshioUI;
using VRC;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(PlayerManager), "Method_Private_Void_PDM_0")]
    class Method1
    {
        [HarmonyPostfix]
        static void ModerationPatch()
        {
            if (!VFc5dmJuSnBjMlU9.debug) return;
            UshioMenuApi.PopupUI("Invoked Method_Private_void_PDM_0 in PlayerManager");
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "Method_Private_Void_PDM_1")]
    class Method2
    {
        [HarmonyPostfix]
        static void PlayerManagerMethod2()
        {
            if (!VFc5dmJuSnBjMlU9.debug) return;
            UshioMenuApi.PopupUI("Invoked Method_Private_void_PDM_1 in PlayerManager");
        }
    }
}
