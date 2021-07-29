using MelonLoader;
using MoonriseApi;
using MoonriseV2Mod.API;
using MoonriseV2Mod.AvatarFunctions;
using MoonriseV2Mod.BaseFunctions;
using MoonriseV2Mod.HReader;
using MoonriseV2Mod.MonoBehaviourScripts;
using MoonriseV2Mod.Settings;
using MoonriseV2Mod.SocialInterractions;
using RubyButtonAPI;
using System;
using System.Collections;
using UnhollowerRuntimeLib;
using UshioUI;
using VRC.Core;

namespace MoonriseV2Mod
{
    public class Moonrise : MelonMod
    {

        internal TVJVc2Vy user;
        // internal static bool debug = false;

        public static event Action<QMNestedButton, QMNestedButton, TVJVc2Vy> loadMenu;
        internal static event Action modUpdate;

        internal bool isInitialized = false;
        internal static HarmonyLib.Harmony harmonyInstance;
        public override void OnApplicationStart()
        {
            harmonyInstance = new HarmonyLib.Harmony("com.StonedCode.MoonriseV2");
            harmonyInstance.PatchAll();
            MelonCoroutines.Start(ModStart());
        }

        [Obsolete]
        public override void OnLevelWasLoaded(int level)
        {
            if (level == -1)
            {
                VFc5a1NXNW1idz09.CheckUpdate();
            }
        }

        public override void OnUpdate()
        {

            modUpdate?.Invoke();
        }

        public IEnumerator ModStart()
        {
            MRConfiguration.Initialize();
            QXNzZXRCdW5kbGVz.InitializeAssetBundle();

            while (!QXNzZXRCdW5kbGVz.isInitialized) yield return null;
            VFc5a1NXNW1idz09.Initialize();
            PortableMirror.Initialize();
            TW9vbnJpc2VCYXNlRnVuY3Rpb25z.Initialize();
            VmxSSk5XRnRSbGhTYms1VVZucFZkMWRzYUV0bFZteFlWR3BDYUZaNmJERlpla0pMWVVkTmVWWlVNRDA9.Initialize();
            QXZhdGFyRnVuY3Rpb25z.Initialize();
            VTJWMGRHbHVaM05HZFc1amRHbHZibk09.Initialize();
            NHentaiReader.Initialize();
            AddonMods.Initialize();

            while (APIUser.CurrentUser == null) yield return null;

            if (MRConfiguration.config.moonriseKey != "FreeUser")
                user = TVJVc2Vy.UjJWMFZYTmxjZz09(MRConfiguration.config.moonriseKey);

            if (!isInitialized)
            {
                QMNestedButton functions = new QMNestedButton("ShortcutMenu", 0, -2, "", "");
                QMNestedButton socialInterractions = new QMNestedButton("UserInteractMenu", 4, -2, "<color=cyan>MMM</color>\nPlayer\nFunctions", "MMM options for selected player");
                UshioMenuApi.SetMenu();
                QXNzZXRCdW5kbGVz.InitializeSpecial(user);

                while (!QXNzZXRCdW5kbGVz.specialInitialized) yield return null;

                loadMenu?.Invoke(functions, socialInterractions, user);
                isInitialized = true;
            }
        }
    }
}
