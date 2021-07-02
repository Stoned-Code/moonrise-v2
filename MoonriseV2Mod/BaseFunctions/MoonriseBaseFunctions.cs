using MoonriseTabApi;
using MoonriseV2Mod.API;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib;
using UnityEngine;
using Harmony;
using HarmonyLib;

namespace MoonriseV2Mod.BaseFunctions
{
    internal sealed class MoonriseBaseFunctions : MoonriseObject
    {
        public static MoonriseBaseFunctions baseFunctions;

        public MoonriseBaseFunctions()
        {
            Moonrise.loadMenu += LoadMenu;
        }

        private Sprite MoonriseIcon;
        public MenuTab menuTab;

        public static void Initialize()
        {
            PlayerMute.Initialize();
            baseFunctions = new MoonriseBaseFunctions();

            baseFunctions.MoonriseIcon = MoonriseAssetBundles.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/Sprites/MoonriseIcon.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            baseFunctions.MoonriseIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;


            baseFunctions.isInitialized = true;
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socielInterraction, MRUser user)
        {
            var quickMenuCollider = GameObject.Find("UserInterface/QuickMenu").GetComponent<BoxCollider>();
            quickMenuCollider.size = new Vector3(2517.34f, 2511.213f, 1);
            menuTab = TabApi.MakeTabButton(functions, MoonriseIcon, MenuTab.MenuSide.Bottom, 1, "Update!", Color.blue);

            //Mute All
            var muteAllButton = new QMToggleButton(functions, 1, 0, "Mute All", delegate
            {
                PlayerMute.muteAll = true;
            }, "Disabled", delegate
            {
                PlayerMute.muteAll = false;
                PlayerMute.UnmutePlayers();
            }, "Mutes all players in the world except friends", null, null, false, false);

            //Mute Friends
            var muteFriendsTransform = new QMToggleButton(functions, 2, 0, "Mute Friends", delegate
            {
                PlayerMute.muteFriends = true;
            }, "Disabled", delegate
            {
                PlayerMute.muteFriends = false;
                PlayerMute.UnmutePlayers();
            }, "Mutes all friends in the world", null, null, false, false);
        }
    }
}
