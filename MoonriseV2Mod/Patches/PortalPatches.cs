using HarmonyLib;
using MoonriseV2Mod.API;
using MoonriseV2Mod.MonoBehaviourScripts;
using MoonriseV2Mod.Settings;
using UnityEngine;
using UshioUI;
using VRC;
using VRC.SDKBase;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(PortalTrigger), "OnTriggerEnter")]
    class PortalOnTriggerEnter
    {
        static bool Prefix(PortalTrigger __instance, Collider param_1)
        {
            MRAvatarController mrController = param_1.gameObject.GetComponentInChildren<MRAvatarController>();

            if (mrController == null) return false;
            if (!MRConfiguration.config.antiPortal) return true;
            if (MRConfiguration.config.walkThrough) return false;
            if (!mrController.isLocalPlayer)
            {
                MoonriseConsole.Log($"{mrController.apiUser.displayName} went through a portal to {__instance.field_Private_PortalInternal_0.field_Private_ApiWorld_0.name}.");
                return false;
            }

            var portalMarker = __instance.GetComponentInChildren<VRC_PortalMarker>();

            string dropperUsername = __instance.GetComponent<PortalInternal>().field_Internal_String_0 ?? "";
            string portalinfo = $"Go to \"{__instance.field_Private_PortalInternal_0.field_Private_ApiWorld_0.name}\"{(dropperUsername != "" ? "?\n[ " + dropperUsername + " ]" : "?\n[ Unkown Dropper ]")}";
            string worldId;

            worldId = __instance.field_Private_PortalInternal_0.field_Internal_String_2 + ":" + __instance.field_Private_PortalInternal_0.field_Private_String_1;
            if (worldId.EndsWith(":"))
                worldId = worldId.Replace(":", "");

            if (portalMarker != null)
                MoonriseConsole.Log(portalMarker.roomId);

            UshioMenuApi.CreateDoubleButtonPopup("Anti-Portal", portalinfo, "Yes", delegate
            {
                Networking.GoToRoom(worldId);
                UshioMenuApi.ClosePopup();
            }, "No", UshioMenuApi.ClosePopup);

            return false;
        }
    }

    [HarmonyPatch(typeof(PortalInternal), "ConfigurePortal")]
    class OnPortalDropPatch
    {
        static void Postfix(PortalInternal __instance, string param_1, string param_2, int param_3, Player param_4)
        {
            __instance.field_Internal_String_0 = param_4.field_Private_APIUser_0.displayName;
            string portalParameters = "\nPortal Dropped By " + param_4.prop_APIUser_0.displayName;
            portalParameters += "\nWorld ID: " + __instance.field_Internal_String_2;
            float distance = Vector3.Distance(__instance.transform.position, PlayerCheck.LocalVRCPlayer.transform.position);

            if (distance < 0.5f)
            {
                MoonriseConsole.Log($"{param_4.field_Private_APIUser_0.developerType} tried to drop a portal on you.");
                Networking.Destroy(__instance.gameObject);
            }
        }
    }
}
