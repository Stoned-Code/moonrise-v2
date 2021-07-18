using MoonriseV2Mod.API;
using MoonriseV2Mod.Settings;
using System;
using System.Collections;
using UnityEngine;
using VRC;
using VRC.Core;
using HarmonyLib;
using MoonriseV2Mod.MonoBehaviourScripts;

namespace MoonriseV2Mod.AvatarFunctions
{
    [HarmonyPatch(typeof(VRCAvatarManager), "Start")]
    public class VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09
    {
        public static bool m_IgnoreFriends = true;
        public static bool m_HideAvatars = true;
        // static Player GetPlayer(VRCAvatarManager avatarManager) => avatarManager.transform.GetParent().GetComponent<Player>();
        
        [HarmonyPostfix]
        static void AvatarManagerStart(VRCAvatarManager __instance)
        {
            if (__instance.gameObject.GetComponent<MRAvatarHider>() == null)
                __instance.gameObject.AddComponent<MRAvatarHider>();
        }
    }
}
