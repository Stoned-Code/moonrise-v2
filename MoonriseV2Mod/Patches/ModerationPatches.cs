using HarmonyLib;
using VRC.Core;
using VRC.Management;
using static VRC.Core.ApiPlayerModeration;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(ModerationManager), "Method_Private_Void_String_ModerationType_Action_1_ApiPlayerModeration_Action_1_String_0")]
    class PlayerModerationSend
    {
        static bool Prefix(ModerationManager __instance, string param_1, ApiPlayerModeration.ModerationType param_2, Il2CppSystem.Action<ApiPlayerModeration> param_3, Il2CppSystem.Action<string> param_4)
        {
            return true;
        }

        static void Postfix(ModerationManager __instance, string param_1, ApiPlayerModeration.ModerationType param_2, Il2CppSystem.Action<ApiPlayerModeration> param_3, Il2CppSystem.Action<string> param_4)
        {
            if (param_1 == null) return;
            //MoonriseConsole.Log("Player Moderation Send: " + param_2 + " - " + param_1);
            return;
        }
    }
    [HarmonyPatch(typeof(ModerationManager), "Method_Private_Void_String_ModerationType_0")]
    class PlayerModerationRemove
    {
        static bool Prefix(ModerationManager __instance, string param_1, ApiPlayerModeration.ModerationType param_2)
        {
            return true;
        }

        static void Postfix(ModerationManager __instance, string param_1, ApiPlayerModeration.ModerationType param_2)
        {
            if (param_1 == null) return;
            //MoonriseConsole.Log("Player Moderation Remove: " + param_2 + " - "+ param_1);
            return;
        }
    }

    [HarmonyPatch(typeof(ApiPlayerModeration), "SendModeration")]
    class ApiPlayerModerationSend
    {
        static bool Prefix(string targetUserId, ModerationType mType)
        {
            //MoonriseConsole.Log($"Api Player Moderation: {targetUserId} - {mType}");
            return true;
        }
    }
}
