using MelonLoader;
using MoonriseV2Mod.API;
using MoonriseV2Mod.AvatarFunctions;
using MoonriseV2Mod.BaseFunctions;
using MoonriseV2Mod.Settings;
using MoonriseV2Mod.SocialInterractions;
using RubyButtonAPI;
using System;
using System.Collections;
using UshioUI;
using VRC.Core;

namespace MoonriseV2Mod
{
    public class VFc5dmJuSnBjMlU9 : MelonMod
    {

        internal TVJVc2Vy user;
        internal static bool debug = false;

        internal static event Action<QMNestedButton, QMNestedButton, TVJVc2Vy> loadMenu;
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
            Q29uZmln.Initialize();
            MoonriseAssetBundles.InitializeAssetBundle();
            while (!MoonriseAssetBundles.isInitialized) yield return null;

            VFc5a1NXNW1idz09.Initialize();
            VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09.Initialize();
            TW9vbnJpc2VCYXNlRnVuY3Rpb25z.Initialize();
            VmxSSk5XRnRSbGhTYms1VVZucFZkMWRzYUV0bFZteFlWR3BDYUZaNmJERlpla0pMWVVkTmVWWlVNRDA9.Initialize();
            QXZhdGFyRnVuY3Rpb25z.Initialize();
            VTJWMGRHbHVaM05HZFc1amRHbHZibk09.Initialize();

            while (APIUser.CurrentUser == null) yield return null;

            if (Q29uZmln.config.moonriseKey != "FreeUser")
                user = TVJVc2Vy.UjJWMFZYTmxjZz09(Q29uZmln.config.moonriseKey);

            if (!isInitialized)
            {
                QMNestedButton functions = new QMNestedButton("ShortcutMenu", 0, -2, "", "");
                QMNestedButton socialInterractions = new QMNestedButton("UserInteractMenu", 4, -2, "<color=cyan>MMM</color>\nPlayer\nFunctions", "MMM options for selected player");
                UshioMenuApi.SetMenu();
                loadMenu?.Invoke(functions, socialInterractions, user);
                isInitialized = true;
            }
        }
    }
}
