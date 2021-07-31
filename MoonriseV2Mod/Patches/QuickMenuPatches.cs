using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;
using MoonriseV2Mod.API;
using RubyButtonAPI;
using UshioUI;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(QuickMenu), "Start")]
    class QuickMenuPatches
    {
        [HarmonyPostfix]
        static void StartPatch()
        {
            MelonCoroutines.Start(Moonrise.LoadMenu());
        }
    }
}
