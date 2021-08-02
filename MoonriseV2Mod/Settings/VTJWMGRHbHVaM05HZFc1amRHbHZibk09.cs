using MoonriseV2Mod.API;
using MoonriseV2Mod.BaseFunctions;
using RubyButtonAPI;
using System;

namespace MoonriseV2Mod.Settings
{
    internal sealed class VTJWMGRHbHVaM05HZFc1amRHbHZibk09 : MoonriseObject
    {
        public static VTJWMGRHbHVaM05HZFc1amRHbHZibk09 settingsFunctions;

        public VTJWMGRHbHVaM05HZFc1amRHbHZibk09()
        {
            Moonrise.loadMenu += LoadMenu;
            Moonrise.modUpdate += OnSettingsUpdate;
        }

        public static void Initialize()
        {
            try
            {
                settingsFunctions = new VTJWMGRHbHVaM05HZFc1amRHbHZibk09();
                settingsFunctions.isInitialized = true;
            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog(ex.Message);
            }
        }

        public QMNestedButton settingsMenu;
        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            settingsMenu = new QMNestedButton(functions, 5, -1, "Settings", "Settings for moonrise.");

            var addBlocker = new QMToggleButton(settingsMenu, 1, 0, "Add\nBlocker", delegate
            {
                MRConfiguration.config.addBlocker = true;
                MRConfiguration.config.WriteConfig();
            }, "Disabled", delegate
            {
                MRConfiguration.config.addBlocker = false;
                MRConfiguration.config.WriteConfig();
            }, "Disables VRChat promotions.", null, null, false, MRConfiguration.config.addBlocker);

            var toggleUiDisplay = new QMToggleButton(settingsMenu, 2, 0, "Shortcut Ui", delegate
            {
                MRConfiguration.config.UiDisplayEnabled = true;
                TW9vbnJpc2VCYXNlRnVuY3Rpb25z.baseFunctions.display.SetDisplayActive(true);
                MRConfiguration.config.WriteConfig();
            }, "Disabled", delegate
            {
                MRConfiguration.config.UiDisplayEnabled = false;
                TW9vbnJpc2VCYXNlRnVuY3Rpb25z.baseFunctions.display.SetDisplayActive(false);
                MRConfiguration.config.WriteConfig();
            }, "Toggles the Moonrise display in the shortcut menu", null, null, false, MRConfiguration.config.UiDisplayEnabled);
        }

        public void OnSettingsUpdate()
        {
            try
            {
                VVZkU2ExRnRlSFpaTW5Sc1kyYzlQUT09.ToggleVRCPlusPromotions();
            }

            catch { }
        }
    }
}
