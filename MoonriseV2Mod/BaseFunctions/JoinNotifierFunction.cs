using JoinNotifier;
using MoonriseV2Mod.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UshioUI;
using VRC;
using VRC.Core;

namespace MoonriseV2Mod.BaseFunctions
{
    internal class JoinNotifierFunction
    {
        public static int lastLevelLoad;
        public static bool observedLocalPlayerJoin;
        public static bool observervedFirstPlayerLeave;

        public static void Initialize()
        {
            NetworkManagerHooks.OnJoin += PlayerJoined;
            NetworkManagerHooks.OnLeave += PlayerLeft;
        }

        private static void PlayerLeft(Player player)
        {
            if (!MRConfiguration.config.joinNotifier) return;

            var userApi = player.prop_APIUser_0;

            if (Environment.TickCount - lastLevelLoad < 5_000) return;

            if (!observervedFirstPlayerLeave)
            {
                observervedFirstPlayerLeave = true;
                return;
            }

            var isFriendsWith = APIUser.IsFriendsWith(userApi.id);
            var playerName = userApi.displayName ?? "!null!";


            if (isFriendsWith)
            {
                UshioMenuApi.PopupUI($"Friend Left:\n{playerName}", Color.green);
                MoonriseConsole.Log($"Friend Left: {playerName}");
            }
        }

        private static void PlayerJoined(Player player)
        {
            if (!MRConfiguration.config.joinNotifier) return;

            var userApi = player.prop_APIUser_0;

            if (APIUser.CurrentUser.id == userApi.id)
            {
                observedLocalPlayerJoin = true;
                lastLevelLoad = Environment.TickCount;

                //MoonriseConsole.Log("Finished loading players!");
            }

            if (!observedLocalPlayerJoin || Environment.TickCount - lastLevelLoad < 5_000) return;

            var isFriendsWith = APIUser.IsFriendsWith(userApi.id);
            var playerName = userApi.displayName ?? "!null!";

            if (isFriendsWith)
            {
                UshioMenuApi.PopupUI($"Friend Joined:\n{playerName}", Color.green);
                MoonriseConsole.Log($"Friend Joined: {playerName}");
            }
        }
    }
}
