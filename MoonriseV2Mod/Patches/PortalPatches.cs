using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MoonriseV2Mod.Settings;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(PortalTrigger), "OnTriggerEnter")]
    class PortalOnTriggerEnter
    {
        [HarmonyPrefix]
        static bool PortalTriggerEnter()
        {
            if (MRConfiguration.config.antiPortal) return false;
            return true;
        }
    }
}
