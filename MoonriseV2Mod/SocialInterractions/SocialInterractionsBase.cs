using MelonLoader;
using MoonriseV2Mod.API;
using MoonriseV2Mod.AvatarFunctions;
using MoonriseV2Mod.CustomBehavior;
using MoonriseV2Mod.Patches;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;
using UshioUI;

namespace MoonriseV2Mod.SocialInterractions
{
    internal class SocialInterractionsBase : MoonriseMenu
    {
        public static SocialInterractionsBase siBase;

        public static void Initialize()
        {
            siBase = new SocialInterractionsBase();
            siBase.isInitialized = true;
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            var clearIgnoreListButton = new QMSingleButton(socialInterractions, 0, 0, "Clear\nIgnore\nList", delegate
            {
                MRConfiguration.config.ignoreList.Clear();
            }, "Clears the distant avatar ignore list");

            var addToIgnoreListButton = new QMSingleButton(socialInterractions, 0, 1, "Add To\nIgnore List", delegate
            {
                var apiUser = QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0;

                string displayName;

                if (!MRConfiguration.config.ignoreList.TryGetValue(apiUser.id, out displayName))
                {
                    MRConfiguration.config.ignoreList.Add(apiUser.id, apiUser.displayName);
                    //MRConfiguration.config.WriteConfig();
                }

                else MoonriseConsole.ErrorLog($"{displayName} is already ignored...");

            }, "Adds selected user to distant avatar hider ignore list");
            VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(addToIgnoreListButton, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Top, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Horizontal);

            var removeFromIgnoreListButton = new QMSingleButton(socialInterractions, 0, 1, "Remove From\nIgnore List", delegate
            {
                var selectedUser = PlayerCheck.GetSelectedPlayer(QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0.id);
                var selectedId = selectedUser.prop_APIUser_0.id;

                string displayName;

                if (MRConfiguration.config.ignoreList.TryGetValue(selectedId, out displayName))
                {
                    MRConfiguration.config.ignoreList.Remove(selectedId);
                    //MRConfiguration.config.WriteConfig();
                }

                // MelonCoroutines.Start(VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.ResetHideDistantAvatars());
            }, "Removes selected user from distant avatar hider ignore list");
            VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(removeFromIgnoreListButton, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Bottom, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Horizontal);

            var playerTeleport = new QMSingleButton(socialInterractions, 1, 0, "Teleport\nTo", delegate
            {
                var foundPlayer = QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0;

                PlayerTeleport.TeleportTo(foundPlayer.id);
            }, "Teleports you to the selected player.");

            if (user == null) return;

            if (user.Premium)
            {
                var copyUserId = new QMSingleButton(socialInterractions, 5, 0, "Copy\nUser ID", delegate
                {
                    var apiUser = QuickMenu.prop_QuickMenu_0.prop_APIUser_0;

                    System.Windows.Forms.Clipboard.SetText(apiUser.id ?? "Error Copying Link...");
                    UshioMenuApi.PopupUI("[ Moonrise ]\nCopied User ID to Clipboard");
                }, "Copies selected user's User ID.");
            }
        }

        public override void OnJoinedRoom()
        {
            // orbitToggle.setToggleState(false);
        }
    }
}
