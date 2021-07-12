using MelonLoader;
using MoonriseV2Mod.API;
using MoonriseV2Mod.AvatarFunctions;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;

namespace MoonriseV2Mod.SocialInterractions
{
    public class SocialInterractionsBase : MoonriseObject
    {
        public static SocialInterractionsBase siBase;
        public SocialInterractionsBase()
        {
            Moonrise.loadMenu += LoadMenu;
        }

        public static void Initialize()
        {
            siBase = new SocialInterractionsBase();
            siBase.isInitialized = true;
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, MRUser user)
        {
            var clearIgnoreListButton = new QMSingleButton(socialInterractions, 0, 0, "Clear\nIgnore\nList", delegate
            {
                Config.config.ignoreList.Clear();

                MelonCoroutines.Start(AvatarHider.ResetHideDistantAvatars());
            }, "Clears the distant avatar ignore list");

            var addToIgnoreListButton = new QMSingleButton(socialInterractions, 0, 1, "Add To\nIgnore List", delegate
            {
                var apiUser = QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0;

                string displayName;

                if (!Config.config.ignoreList.TryGetValue(apiUser.id, out displayName))
                {
                    Config.config.ignoreList.Add(apiUser.id, apiUser.displayName);
                    Config.config.WriteConfig();
                }

                else MoonriseConsole.ErrorLog($"{displayName} is already ignored...");

            }, "Adds selected user to distant avatar hider ignore list");
            UshioRubyModifiers.SetHalfButton(addToIgnoreListButton, UshioRubyModifiers.HalfPosition.Top, UshioRubyModifiers.Rotation.Horizontal);

            var removeFromIgnoreListButton = new QMSingleButton(socialInterractions, 0, 1, "Remove From\nIgnore List", delegate
            {
                var selectedUser = PlayerCheckApi.GetSelectedPlayer(QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0.id);
                var selectedId = selectedUser.prop_APIUser_0.id;

                string displayName;

                if (Config.config.ignoreList.TryGetValue(selectedId, out displayName))
                {
                    Config.config.ignoreList.Remove(selectedId);
                    Config.config.WriteConfig();
                }

                MelonCoroutines.Start(AvatarHider.ResetHideDistantAvatars());
            }, "Removes selected user from distant avatar hider ignore list");
            UshioRubyModifiers.SetHalfButton(removeFromIgnoreListButton, UshioRubyModifiers.HalfPosition.Bottom, UshioRubyModifiers.Rotation.Horizontal);

            var playerTeleport = new QMSingleButton(socialInterractions, 1, 0, "Teleport\nTo", delegate
            {
                var foundPlayer = QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0;

                PlayerTeleport.TeleportTo(foundPlayer.id);
            }, "Teleports you to the selected player.");
            if (user == null) return;
            if (user.Premium)
            {
                var reportCrasher = new QMSingleButton(socialInterractions, 5, 0, "Report as\nCrasher", delegate
                {
                    var foundPlayer = QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0;
                    var foundVrcPlayer = PlayerCheckApi.GetSelectedPlayer(foundPlayer.id);
                    CrasherReport.ReportCrasher(foundPlayer.displayName, foundPlayer.id, foundVrcPlayer.prop_ApiAvatar_0);
                }, "Sends a report to the Moonrise database of the selected player as a potential crasher");
            }

        }
    }
}
