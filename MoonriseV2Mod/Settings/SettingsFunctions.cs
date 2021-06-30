using MoonriseV2Mod.API;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonriseV2Mod.Settings
{
    internal sealed class SettingsFunctions : MoonriseObject
    {
        public static SettingsFunctions settingsFunctions;

        public SettingsFunctions()
        {
            Moonrise.loadMenu += LoadMenu;
            Moonrise.modUpdate += OnSettingsUpdate;
        }

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
        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, MRUser user)
        {
            settingsMenu = new QMNestedButton(functions, 3, 0, "Settings", "Settings for moonrise.");

            var addBlocker = new QMToggleButton(settingsMenu, 1, 0, "Add\nBlocker", delegate
            {
                Config.config.addBlocker = true;
                Config.config.WriteConfig();
            }, "Disabled", delegate
            {
                Config.config.addBlocker = false;
                Config.config.WriteConfig();
            }, "Disables VRChat promotions.", null, null, false, Config.config.addBlocker);
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
