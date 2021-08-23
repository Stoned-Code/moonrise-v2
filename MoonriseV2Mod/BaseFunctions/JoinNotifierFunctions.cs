using MoonriseV2Mod.API;
using MoonriseV2Mod.Settings;
using System;
using UshioUI;
using VRC;
using VRC.Core;
using static MoonriseV2Mod.API.MoonriseMultiCast;

namespace MoonriseV2Mod.BaseFunctions
{
    public class JoinNotifierFunctions
    {
        public static int lastLevelLoad;
        public static bool observedLocalPlayerJoin;
        public static bool observervedFirstPlayerLeave;

        internal static OnPlayerMultiCast PlayerJoined;
        internal static OnPlayerMultiCast PlayerLeft;

        public static void Initialize()
        {
            PlayerJoined += PlayerJoin;
            PlayerLeft += PlayerLeave;
        }

        internal static void OnPlayerJoin(Player player)
        {
            if (!MRConfiguration.config.joinNotifier) return;

            var apiUser = player.prop_APIUser_0;

            if (apiUser == null) return;

            if (APIUser.CurrentUser.id == apiUser.id)
            {
                observedLocalPlayerJoin = true;
                lastLevelLoad = Environment.TickCount;
            }

            if (!observedLocalPlayerJoin || Environment.TickCount - lastLevelLoad < 5_000) return;
            if (PlayerCheck.IsMe(apiUser.id)) return;
                var isFriendsWith = APIUser.IsFriendsWith(apiUser.id);
            var playerName = apiUser.displayName ?? "!null!";
            PlayerJoined?.Invoke(player, isFriendsWith, MRConfiguration.config.allUsersJn);
        }

        internal static void OnPlayerLeave(Player player)
        {
            if (!MRConfiguration.config.joinNotifier) return;

            var apiUser = player.prop_APIUser_0;

            if (apiUser == null) return;

            if (Environment.TickCount - lastLevelLoad < 5_000) return;

            if (!observervedFirstPlayerLeave)
            {
                observervedFirstPlayerLeave = true;
                return;
            }
            if (PlayerCheck.IsMe(apiUser.id)) return;
            var isFriendsWith = APIUser.IsFriendsWith(apiUser.id);
            var playerName = apiUser.displayName ?? "!null!";

            PlayerLeft?.Invoke(player, isFriendsWith, MRConfiguration.config.allUsersJn);
        }

        private static void PlayerJoin(Player player, bool isFriend, bool allJn)
        {
            APIUser apiUser = player.field_Private_APIUser_0;
            string playerName = apiUser.displayName ?? "!null!";

            if (allJn)
            {
                MoonriseConsole.Log($"{(isFriend ? "Friend" : "Player")} Joined: {playerName}");
                UshioMenuApi.PopupUI(playerName, $"{(isFriend ? "Friend" : "Player")} Joined");
            }

            else
            {
                if (!isFriend) return;
                MoonriseConsole.Log($"Friend Joined: {playerName}");
                UshioMenuApi.PopupUI(playerName, "Friend Joined");
            }
        }

        private static void PlayerLeave(Player player, bool isFriend, bool allJn)
        {
            APIUser apiUser = player.field_Private_APIUser_0;
            string playerName = apiUser.displayName ?? "!null!";

            if (allJn)
            {
                MoonriseConsole.Log($"{(isFriend ? "Friend" : "Player")} Left: {playerName}");
                UshioMenuApi.PopupUI(playerName, $"{(isFriend ? "Friend" : "Player")} Left");
            }

            else
            {
                if (!isFriend) return;
                MoonriseConsole.Log($"Friend Left: {playerName}");
                UshioMenuApi.PopupUI(playerName, "Friend Left");
            }
        }
    }
}
