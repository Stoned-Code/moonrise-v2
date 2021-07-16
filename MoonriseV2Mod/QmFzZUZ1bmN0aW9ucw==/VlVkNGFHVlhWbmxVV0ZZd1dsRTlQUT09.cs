using MoonriseV2Mod.API;
using MoonriseV2Mod.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;

namespace MoonriseV2Mod.BaseFunctions
{
    internal static class VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09
    {
        public static bool isInitialize = false;
        public static bool muteFriends = false;
        public static bool muteAll = false;
        public static void Initialize()
        {
            VFc5dmJuSnBjMlU9.modUpdate += AudioUpdate;
            isInitialize = true;
        }

        public static void MutePlayers()
        {
            if (PlayerManager.field_Private_Static_PlayerManager_0 != null)
            {
                var playerManager = PlayerManager.field_Private_Static_PlayerManager_0.prop_ArrayOf_Player_0;

                for (int i = 0; i < playerManager.Length; i++)
                {
                    var player = playerManager[i];
                    var apiUser = player.prop_APIUser_0;
                    var speaker = player.prop_USpeaker_0;
                    string displayName;

                    bool isFriend = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.IsFriendsWith(apiUser.id);
                    bool isIgnored = Q29uZmln.config.ignoreList.TryGetValue(apiUser.id, out displayName);

                    if (isFriend && !muteFriends)
                    {
                        if (speaker.field_Private_AudioSource_0.mute)
                            speaker.field_Private_AudioSource_0.mute = false;
                        continue;
                    }

                    if (isIgnored)
                    {
                        if (speaker.field_Private_AudioSource_0.mute)
                            speaker.field_Private_AudioSource_0.mute = false;
                        continue;
                    }

                    if (speaker.field_Private_AudioSource_0.mute) continue;

                    speaker.field_Private_AudioSource_0.mute = true;
                }
            }
        }

        public static void UnmutePlayers()
        {
            if (PlayerManager.field_Private_Static_PlayerManager_0 != null)
            {
                var playerManager = PlayerManager.field_Private_Static_PlayerManager_0.prop_ArrayOf_Player_0;

                for (int i = 0; i < playerManager.Length; i++)
                {
                    var player = playerManager[i];
                    var apiUser = player.prop_APIUser_0;
                    var speaker = player.prop_USpeaker_0;

                    bool isFriend = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.IsFriendsWith(apiUser.id);

                    if (isFriend && muteFriends) continue;
                    if (muteAll) continue;
                    if (!speaker.field_Private_AudioSource_0.mute) continue;

                    speaker.field_Private_AudioSource_0.mute = false;
                }
            }
        }

        public static void AudioUpdate()
        {
            if (muteAll || muteFriends) MutePlayers();
        }
    }
}
