using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;
using MoonriseV2Mod.API;
using RubyButtonAPI;
using UshioUI;
using VRC.Core;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(QuickMenu), "Start")]
    class QuickMenuPatches
    {

        public static event Action<QMNestedButton, QMNestedButton, TVJVc2Vy> loadMenu;
        static bool initializing = false;
        [HarmonyPostfix]
        static void StartPatch()
        {
            if (initializing) return;
            initializing = true;
            MelonCoroutines.Start(LoadMenu());
        }

        internal static IEnumerator LoadMenu()
        {
            MoonriseConsole.Log("Started Loading Menu.");
            while (Moonrise.moonrise == null) yield return null;
            while (APIUser.CurrentUser == null) yield return null;
            MoonriseConsole.Log("Still Loading Menu...");
            if (!Moonrise.moonrise.isInitialized)
            {
                QMNestedButton functions = new QMNestedButton("ShortcutMenu", 0, -2, "", "");
                QMNestedButton socialInterractions = new QMNestedButton("UserInteractMenu", 4, -2, "<color=cyan>MMM</color>\nPlayer\nFunctions", "MMM options for selected player");
                UshioMenuApi.SetMenu();
                QXNzZXRCdW5kbGVz.InitializeSpecial(Moonrise.moonrise.user);

                while (!QXNzZXRCdW5kbGVz.specialInitialized) yield return null;

                loadMenu?.Invoke(functions, socialInterractions, Moonrise.moonrise.user);
                Moonrise.moonrise.isInitialized = true;
            }
        }
    }
}
