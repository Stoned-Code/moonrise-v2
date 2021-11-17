/**********************************************
 * 
 * Credit To DubyaDude for the button api.
 * https://github.com/DubyaDude/RubyButtonAPI
 * 
 * Credit to gompo for the action menu api.
 * 
 **********************************************/
using MelonLoader;
using MoonriseApi;
using MoonriseV2Mod.API;
using MoonriseV2Mod.BaseFunctions;
using MoonriseV2Mod.HReader;
using MoonriseV2Mod.Settings;
using MoonriseV2Mod.SocialInterractions;
using MoonriseV2Mod.AvatarFunctions;
using System;
using System.Collections;
using VRC.Core;
using MoonriseV2Mod.WorldFunctions;
using MoonriseV2Mod.PlayerFunctions;
using MoonriseV2Mod.Patches;
using MoonriseV2Mod.ActionMenu;

namespace MoonriseV2Mod
{
    public class Moonrise : MelonMod
    {

        internal TVJVc2Vy user;
        internal static Moonrise moonrise;
        // internal static bool debug = false;

        internal bool isInitialized { get; set; }
        internal static new HarmonyLib.Harmony harmonyInstance;
        internal static event Action modUpdate;

        public override void OnApplicationStart()
        {
            moonrise = this;
            harmonyInstance = new HarmonyLib.Harmony("com.StonedCode.MoonriseV2");
            harmonyInstance.PatchAll();
            MelonCoroutines.Start(ModStart());
            NetworkManagerJoinRoom.OnJoinedRoom += OnJoinedRoom;
            NetworkManagerLeftRoom.OnLeftRoom += OnLeftRoom;
        }

        private void OnJoinedRoom()
        {
            VRCUiManager.prop_VRCUiManager_0.field_Private_List_1_String_0.Clear();
        }

        private void OnLeftRoom()
        {
            VideoPlayerFunctions.SetVideoURL("");
        }

        [Obsolete]
        public override void OnLevelWasLoaded(int level)
        {
            if (level == -1)
            {
            }
        }

        public override void OnUpdate()
        {
            try
            {
                modUpdate?.Invoke();
            }

            catch { }
        }

        public IEnumerator ModStart()
        {

            MRConfiguration.Initialize();
            ActionMenuBaseFunctions.Initialize();
            QXNzZXRCdW5kbGVz.InitializeAssetBundle();

            while (!QXNzZXRCdW5kbGVz.isInitialized) yield return null;

            //ModInfo.Initialize();
            PortableMirror.Initialize();
            MoonriseBaseFunctions.Initialize();
            SocialInterractionsBase.Initialize();
            AvatarFunctionsBase.Initialize();
            SettingsFunctions.Initialize();
            NHentaiReader.Initialize();
            WorldFunctionsBase.Initialize();
            PlayerFunctionsBase.Initialize();
            AddonMods.Initialize();

            while (APIUser.CurrentUser == null) yield return null;

            if (MRConfiguration.config.moonriseKey.ToLower() != "freeuser")
                user = TVJVc2Vy.UjJWMFZYTmxjZz09(MRConfiguration.config.moonriseKey);
        }
    }
}
