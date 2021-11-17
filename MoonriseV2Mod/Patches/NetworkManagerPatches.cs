using HarmonyLib;
using MoonriseV2Mod.BaseFunctions;
using System;
using VRC;
using static MoonriseV2Mod.API.MoonriseMultiCast;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(NetworkManager), "Awake")]
    internal class NetworkManagerJoinNotifier
    {
        public static bool IsInitialized = false;

        static void Postfix(NetworkManager __instance)
        {
            if (IsInitialized) return;

            Initialize();
        }
        public static void Initialize()
        {
            if (NetworkManager.field_Internal_Static_NetworkManager_0 == null) return;

            var field0 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
            var field1 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;

            field0.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>((player) =>
            {
                JoinNotifierFunctions.OnPlayerJoin(player);
            }));

            field1.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>((player) =>
            {
                JoinNotifierFunctions.OnPlayerLeave(player);
            }));
            JoinNotifierFunctions.Initialize();

            IsInitialized = true;
        }
    }

    [HarmonyPatch(typeof(NetworkManager), "OnJoinedRoom")]
    internal class NetworkManagerJoinRoom
    {
        public static SimpleMultiCast OnJoinedRoom;
        static void Postfix()
        {
            OnJoinedRoom?.Invoke();
        }
    }

    [HarmonyPatch(typeof(NetworkManager), "OnLeftRoom")]
    internal class NetworkManagerLeftRoom
    {
        public static SimpleMultiCast OnLeftRoom;
        static void Postfix()
        {
            OnLeftRoom?.Invoke();
        }
    }
}
