using HarmonyLib;
using MoonriseV2Mod.MonoBehaviourScripts;
using System;
using UnityEngine;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(VRCAvatarManager), "Start")]
    class VRCAvatarManagerStartPatch
    {
        public static bool m_IgnoreFriends = true;
        public static bool m_HideAvatars = true;
        // static Player GetPlayer(VRCAvatarManager avatarManager) => avatarManager.transform.GetParent().GetComponent<Player>();
        
        static void Postfix(VRCAvatarManager __instance)
        {
            if (__instance.gameObject.GetComponent<MRAvatarController>() != null) return;

            __instance.gameObject.AddComponent<MRAvatarController>();
            // MoonriseConsole.Log("Loaded avatar manager.");
        }
    }

    [HarmonyPatch(typeof(VRCAvatarManager), "Method_Private_Boolean_GameObject_String_Single_String_0")]
    internal class VRCAvatarManagerLoadedPatch
    {
        internal static event Action<VRCAvatarManager, MRAvatarController, GameObject, string> OnAvatarLoaded;
        static bool Prefix(VRCAvatarManager __instance, out MRAvatarController __state, GameObject param_1, string param_2, float param_3, string param_4)
        {
            __state = __instance.GetComponent<MRAvatarController>();
            if (__state == null) return true;
            if (param_2 == "loading") return true;
            if (__instance == null || __instance.prop_ApiAvatar_0 == null || param_1 == null || param_4 == "") return true;

            return true;
        }

        static void Postfix(VRCAvatarManager __instance, MRAvatarController __state, GameObject param_1, string param_2, float param_3, string param_4)
        {
            if (param_2 == "loading") return;
            if (__instance == null || __instance.prop_ApiAvatar_0 == null || param_1 == null || param_4 == "") return;

            OnAvatarLoaded?.Invoke(__instance, __state, param_1, param_4);
        }
    }
}
