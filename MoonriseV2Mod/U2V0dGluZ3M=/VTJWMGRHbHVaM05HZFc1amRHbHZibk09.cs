using MoonriseV2Mod.API;
using RubyButtonAPI;
using System;

namespace MoonriseV2Mod.Settings
{
    internal sealed class VTJWMGRHbHVaM05HZFc1amRHbHZibk09 : VkZjNWRtSnVTbkJqTWxaUVdXMXZQUT09
    {
        public static VTJWMGRHbHVaM05HZFc1amRHbHZibk09 settingsFunctions;

        public VTJWMGRHbHVaM05HZFc1amRHbHZibk09()
        {
            VFc5dmJuSnBjMlU9.loadMenu += VEc5aFpFMWxiblU9;
            VFc5dmJuSnBjMlU9.modUpdate += OnSettingsUpdate;
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
                TW9vbnJpc2VDb25zb2xl.ErrorLog(ex.Message);
            }
        }

        public QMNestedButton settingsMenu;
        public override void VEc5aFpFMWxiblU9(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            settingsMenu = new QMNestedButton(functions, 5, 0, "Settings", "Settings for moonrise.");

            var addBlocker = new QMToggleButton(settingsMenu, 1, 0, "Add\nBlocker", delegate
            {
                Q29uZmln.config.addBlocker = true;
                Q29uZmln.config.WriteConfig();
            }, "Disabled", delegate
            {
                Q29uZmln.config.addBlocker = false;
                Q29uZmln.config.WriteConfig();
            }, "Disables VRChat promotions.", null, null, false, Q29uZmln.config.addBlocker);
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
