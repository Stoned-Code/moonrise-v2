using MelonLoader;
using MoonriseV2Mod.API;
using MoonriseV2Mod.AvatarFunctions;
using MoonriseV2Mod.BaseFunctions;
using MoonriseV2Mod.Settings;
using MoonriseV2Mod.SocialInterractions;
using RubyButtonAPI;
using System;
using System.Collections;
using System.IO;
using System.Net;
using UshioUI;
using VRC.Core;

namespace MoonriseV2Mod
{
    public class Moonrise : MelonMod
    {
        internal class ModInfo
        {
            public const string modName = "Moonrise";
            public const string modVersion = "2.0.0";
            public const string modAuthor = "Stoned Code";
            public const string modDownload = "N/A";
        }

        internal MRUser user;

        internal static event Action<QMNestedButton, QMNestedButton, MRUser> loadMenu;
        internal static event Action modUpdate;

        internal bool isInitialized = false;

        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(ModStart());
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

            MoonriseBaseFunctions.Initialize();
            SocialInterractionsBase.Initialize();
            AvatarFunctionsBase.Initialize();
            SettingsFunctions.Initialize();

            while (APIUser.CurrentUser == null) yield return null;

            MoonriseConsole.Log(APIUser.CurrentUser.ToString());

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
