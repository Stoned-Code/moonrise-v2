using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine.Events;
using RubyButtonAPI;
using MoonriseTabApi;
using MoonriseV2Mod.API;
using VRC.Core;
using UnityEngine;

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

        internal static event Action<QMNestedButton> menuLoaded;
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

        public void LoadMenu()
        {
            if (isInitialized) return;
            QMNestedButton functions = new QMNestedButton("ShortcutMenu", 0, -2, "", "");
            menuLoaded?.Invoke(functions);
            isInitialized = true;
        }

        public IEnumerator ModStart()
        {
            MoonriseAssetBundles.InitializeAssetBundle();
            MoonriseMainFunctions.Initialize();

            while (APIUser.CurrentUser == null) yield return null;

            user = MRUser.GetUser("wh9vmJdJ9vg4zfh5wmun0n9nfeu7qsJ5kayua2z9");
            LoadMenu();
        }
    }
}
