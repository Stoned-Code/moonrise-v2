using HarmonyLib;
using MelonLoader;
using MoonriseApi;
using MoonriseV2Mod.API;
using RubyButtonAPI;
using System.Collections;
using UshioUI;
using VRC.Core;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(QuickMenu), "Start")]
    public class QuickMenuPatches
    {
        internal delegate void LoadMenuMultiCast(QMNestedButton nestedButton, QMNestedButton socialFunctions, TVJVc2Vy user);
        internal static LoadMenuMultiCast loadMenu;
        internal static LoadMenuMultiCast OnAddonsLoaded;
        static bool initializing = false;

        static void Postfix()
        {
            if (initializing) return;
            initializing = true;
            MelonCoroutines.Start(LoadMenu());
        }

        static IEnumerator LoadMenu()
        {
            // MoonriseConsole.Log("Started Loading Menu.");
            while (Moonrise.moonrise == null) yield return null;
            while (APIUser.CurrentUser == null) yield return null;
            // MoonriseConsole.Log("Still Loading Menu...");
            if (!Moonrise.moonrise.isInitialized)
            {
                UshioMenuApi.SetMenu();
                QMNestedButton functions = new QMNestedButton("ShortcutMenu", 0, -2, "", "");
                QMNestedButton socialInterractions = new QMNestedButton("UserInteractMenu", 4, -2, "<color=cyan>MMM</color>\nPlayer\nFunctions", "MMM options for selected player");

                QXNzZXRCdW5kbGVz.InitializeSpecial(Moonrise.moonrise.user ?? null);

                while (!QXNzZXRCdW5kbGVz.specialInitialized) yield return null;

                loadMenu?.Invoke(functions, socialInterractions, Moonrise.moonrise.user ?? null);
                Moonrise.moonrise.isInitialized = true;
                while (!AddonMods.isInitialized) yield return null;
                OnAddonsLoaded?.Invoke(functions, socialInterractions, Moonrise.moonrise.user ?? null);
            }
        }
    }
}
