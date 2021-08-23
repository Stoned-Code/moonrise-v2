using HarmonyLib;
using MoonriseV2Mod.WorldFunctions;
using VRC.SDKBase;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(VRC_Pickup), "Awake")]
    class PickupAwake
    {
        static void Postfix(VRC_Pickup __instance)
        {
            // PickupFunctions.AddPickup(__instance.gameObject);
        }
    }
}
