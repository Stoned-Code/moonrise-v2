using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MoonriseV2Mod.API;
using MoonriseV2Mod.Settings;
using VRC;
using VRC.Core;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(USpeaker), "Update")]
    class PlayerMutePatch
    {
        public static bool muteFriends = false;
        public static bool muteAll = false;

        [HarmonyPostfix]
        static void PlayerMuteUpdate(USpeaker __instance)
        {
            var player = __instance.field_Private_VRCPlayer_0.prop_Player_0;
            if (player == null) return;
            APIUser apiUser = player.field_Private_APIUser_0;
            if (apiUser == null) return;

            bool isFriend = PlayerCheck.IsFriendsWith(apiUser.id);
            string displayName;
            bool isIgnored = MRConfiguration.config.ignoreList.TryGetValue(apiUser.id, out displayName);

            if (isFriend)
            {
                if (isIgnored)
                {
                    if (__instance.field_Private_AudioSource_0.mute)
                        __instance.field_Private_AudioSource_0.mute = false;
                    return;
                }

                if (__instance.field_Private_AudioSource_0.mute != muteFriends)
                    __instance.field_Private_AudioSource_0.mute = muteFriends;

            }

            else
            {
                if (isIgnored)
                {
                    if (__instance.field_Private_AudioSource_0.mute)
                        __instance.field_Private_AudioSource_0.mute = false;
                    return;
                }

                if (__instance.field_Private_AudioSource_0.mute != muteAll)
                    __instance.field_Private_AudioSource_0.mute = muteAll;
            }
        }
    }
}
