using System.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.UI;

namespace MoonriseV2Mod.API
{
    internal static class VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09
    {
        public static DynamicBoneController GetDynamicBoneController(Player player) => player.GetComponentInChildren<DynamicBoneController>();
        public static APIUser user => Object.FindObjectOfType<PageUserInfo>()?.field_Public_APIUser_0;
        public static bool IsFriendsWith(string id) => APIUser.CurrentUser.friendIDs.Contains(id);

        public static bool IsMe(string userId) => userId == APIUser.CurrentUser.id;

        public static bool UserStateCheck(string State)
        {
            bool state;

            if (State.ToLower() == "true")
            {
                state = true;
            }

            else
            {

                state = false;
            }

            return state;
        }

        /// <summary>
        /// Get's the selected player for the interaction menu
        /// </summary>
        /// <param name="UserID">The user ID the method uses to retrieve the player</param>
        /// <returns></returns>
        public static Player GetSelectedPlayer(string UserID)
        {
            Player selectedPlayer = null;
            var players = PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0;

            foreach (var player in players)
            {
                if (player.prop_APIUser_0.id == UserID)
                {
                    selectedPlayer = player;
                    break;
                }
            }

            return selectedPlayer;
        }

        //public static Player LocalPlayer
        //{
        //    get
        //    {
        //        return Player.prop_Player_0;
        //    }
        //}

        public static VRCPlayer LocalVRCPlayer
        {
            get
            {
                return VRCPlayer.field_Internal_Static_VRCPlayer_0;
            }

        }

        //public static Player[] FriendsInRoom
        //{
        //    get
        //    {
        //        var playerList = new List<Player>();

        //        for (int i = 0; i < PlayerManager.field_Private_Static_PlayerManager_0.prop_ArrayOf_Player_0.Length; i++)
        //        {
        //            var player = PlayerManager.field_Private_Static_PlayerManager_0.prop_ArrayOf_Player_0[i];
        //            var apiUser = player.prop_APIUser_0;

        //            var isFriend = IsFriendsWith(apiUser.id);

        //            if (!isFriend) continue;

        //            playerList.Add(player);
        //        }

        //        playerList.Sort();

        //        return playerList.ToArray();
        //    }
        //}

        //public static bool MasterCheck(string UserID)
        //{
        //    bool isMaster = false;
        //    foreach (Player player in PlayerManager.Method_Public_Static_ArrayOf_Player_0())
        //    {
        //        var playerApi = player.field_Private_VRCPlayerApi_0;
        //        if (playerApi.isMaster)
        //        {
        //            if (player.prop_APIUser_0.id != UserID)
        //            {
        //                isMaster = false;
        //                break;
        //            }

        //            else
        //            {
        //                isMaster = true;
        //                break;
        //            }
        //        }

        //        else continue;
        //    }

        //    return isMaster;
        //}

        //public static bool SuperCheck(string UserID)
        //{
        //    bool isSuper = false;
        //    foreach (Player player in PlayerManager.Method_Public_Static_ArrayOf_Player_0())
        //    {
        //        var playerApi = player.field_Private_VRCPlayerApi_0;
        //        if (playerApi.isSuper)
        //        {
        //            if (player.prop_APIUser_0.id != UserID)
        //            {
        //                isSuper = false;
        //                break;
        //            }

        //            else
        //            {
        //                isSuper = true;
        //                break;
        //            }
        //        }

        //        else continue;
        //    }

        //    return isSuper;
        //}

        //public static bool FriendsWithMaster()
        //{
        //    var playerManager = PlayerManager.field_Private_Static_PlayerManager_0.prop_ArrayOf_Player_0;

        //    for (int i = 0; i < playerManager.Length; i++)
        //    {
        //        var player = playerManager[i];
        //        var apiUser = player.prop_APIUser_0;
        //        var isFriends = IsFriendsWith(apiUser.id);

        //        if (!player.field_Private_VRCPlayerApi_0.isMaster) continue;
        //        if (isFriends) return true;
        //    }

        //    return false;
        //}

        //public static Player GetPlayer(string userId)
        //{
        //    return PlayerManager.Method_Public_Static_Player_String_0(userId);
        //}
    }
}
