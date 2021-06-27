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

namespace MoonriseV2Mod
{
    internal class MoonriseMainFunctions
    {
        private static Sprite MoonriseIcon;
        public static MenuTab menuTab;
        public static bool isInitialized = false;

        public static void Initialize()
        {
            MoonriseIcon = MoonriseAssetBundles.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/Sprites/MoonriseIcon.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            MoonriseIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            Moonrise.menuLoaded += LoadMainFunctions;

            isInitialized = true;
        }

        public static void LoadMainFunctions(QMNestedButton functions)
        {
            var quickMenuCollider = GameObject.Find("UserInterface/QuickMenu").GetComponent<BoxCollider>();
            quickMenuCollider.size = new Vector3(2517.34f, 2511.213f, 1);
            menuTab = TabApi.MakeTabButton(functions, MoonriseIcon, MenuTab.MenuSide.Bottom, 1, "Update!", Color.blue);
        }
    }
}
