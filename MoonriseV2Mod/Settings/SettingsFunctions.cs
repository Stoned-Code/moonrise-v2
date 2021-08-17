using MoonriseV2Mod.API;
using MoonriseV2Mod.BaseFunctions;
using MoonriseV2Mod.Patches;
using RubyButtonAPI;
using System;

namespace MoonriseV2Mod.Settings
{
    internal sealed class SettingsFunctions : MoonriseMenu
    {
        public static SettingsFunctions settingsFunctions;

        public static void Initialize()
        {
            try
            {
                settingsFunctions = new SettingsFunctions();
                settingsFunctions.isInitialized = true;
            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog(ex.Message);
            }
        }

        public QMNestedButton settingsMenu;
        public override void InitializeMenu()
        {
            Moonrise.modUpdate += OnSettingsUpdate;
        }
        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            settingsMenu = new QMNestedButton(functions, 5, -1, "Settings", "Settings for moonrise.");

            var addBlocker = new QMToggleButton(settingsMenu, 1, 0, "Ad\nBlocker", delegate
            {
                MRConfiguration.config.addBlocker = true;
                //MRConfiguration.config.WriteConfig();
            }, "Disabled", delegate
            {
                MRConfiguration.config.addBlocker = false;
                //MRConfiguration.config.WriteConfig();
            }, "Disables VRChat promotions.", null, null, false, MRConfiguration.config.addBlocker);

            var toggleUiDisplay = new QMToggleButton(settingsMenu, 2, 0, "Shortcut Ui", delegate
            {
                MRConfiguration.config.UiDisplayEnabled = true;
                MoonriseBaseFunctions.baseFunctions.display.SetDisplayActive(true);
                //MRConfiguration.config.WriteConfig();
            }, "Disabled", delegate
            {
                MRConfiguration.config.UiDisplayEnabled = false;
                MoonriseBaseFunctions.baseFunctions.display.SetDisplayActive(false);
                //MRConfiguration.config.WriteConfig();
            }, "Toggles the Moonrise display in the shortcut menu", null, null, false, MRConfiguration.config.UiDisplayEnabled);
        }

        public void OnSettingsUpdate()
        {
            try
            {
                AddBlocker.ToggleVRCPlusPromotions();
            }

            catch { }
        }
    }
}
