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
    public class Moonrise : MelonMod
    {

        internal MRUser user;

        internal static event Action<QMNestedButton, QMNestedButton, MRUser> loadMenu;
        internal static event Action modUpdate;

        internal bool isInitialized = false;

        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(ModStart());
        }

        public override void OnLevelWasLoaded(int level)
        {
            if (level == -1)
            {
                ModInfo.CheckUpdate();
            }
        }

        public override void OnUpdate()
        {

            modUpdate?.Invoke();
        }

        public IEnumerator ModStart()
        {
            Config.Initialize();
            MoonriseAssetBundles.InitializeAssetBundle();
            while (!MoonriseAssetBundles.isInitialized) yield return null;

            ModInfo.Initialize();
            PortableMirror.Initialize();
            MoonriseBaseFunctions.Initialize();
            SocialInterractionsBase.Initialize();
            AvatarFunctionsBase.Initialize();
            SettingsFunctions.Initialize();

            while (APIUser.CurrentUser == null) yield return null;

            if (Config.config.moonriseKey != "FreeUser")
                user = MRUser.GetUser(Config.config.moonriseKey);

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
