using HarmonyLib;
using MoonriseV2Mod.WorldFunctions;
using VRC.SDKBase;
using static VRC.SDKBase.VRC_Pickup;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(VRC_Pickup), "Awake")]
    class Pickup2Awake
    {
        static void Postfix(VRC_Pickup __instance)
        {
            //PickupFunctions.AddPickup(__instance);
        }
    }

    //[HarmonyPatch(typeof(VRC_Pickup))]

    //[HarmonyPatch(typeof(VRCSDK2.VRC_Pickup), "Awake")]
    //class Pickup2Awake
    //{
    //    static void Postfix(VRCSDK2.VRC_Pickup __instance)
    //    {

    //    }
    //}
}
