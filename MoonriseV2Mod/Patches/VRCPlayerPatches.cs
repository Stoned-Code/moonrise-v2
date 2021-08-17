using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MoonriseV2Mod.API;
using MoonriseV2Mod.MonoBehaviourScripts;
using MoonriseV2Mod.PlayerFunctions;
using Newtonsoft.Json;
using VRC.Core;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(VRCPlayer), "Start")]
    class VRCPlayerAwake
    {
        //static void Postfix(VRCPlayer __instance)
        //{
        //    if (__instance == null) return;
        //    if (__instance.prop_Player_0.field_Private_APIUser_0.id != APIUser.CurrentUser.id) return;

        //    PlayerPhysics.GetDefaultPhysics();
        //}
    }
}
