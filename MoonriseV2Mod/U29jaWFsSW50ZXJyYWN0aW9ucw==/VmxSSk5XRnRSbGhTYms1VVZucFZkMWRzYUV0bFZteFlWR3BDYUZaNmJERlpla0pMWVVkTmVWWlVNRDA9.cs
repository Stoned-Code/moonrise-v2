using MelonLoader;
using MoonriseV2Mod.API;
using MoonriseV2Mod.AvatarFunctions;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;

namespace MoonriseV2Mod.SocialInterractions
{
    public class VmxSSk5XRnRSbGhTYms1VVZucFZkMWRzYUV0bFZteFlWR3BDYUZaNmJERlpla0pMWVVkTmVWWlVNRDA9 : VkZjNWRtSnVTbkJqTWxaUVdXMXZQUT09
    {
        public static VmxSSk5XRnRSbGhTYms1VVZucFZkMWRzYUV0bFZteFlWR3BDYUZaNmJERlpla0pMWVVkTmVWWlVNRDA9 siBase;
        public VmxSSk5XRnRSbGhTYms1VVZucFZkMWRzYUV0bFZteFlWR3BDYUZaNmJERlpla0pMWVVkTmVWWlVNRDA9()
        {
            VFc5dmJuSnBjMlU9.loadMenu += LoadMenu;
        }

        public static void Initialize()
        {
            siBase = new VmxSSk5XRnRSbGhTYms1VVZucFZkMWRzYUV0bFZteFlWR3BDYUZaNmJERlpla0pMWVVkTmVWWlVNRDA9();
            siBase.isInitialized = true;
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            var clearIgnoreListButton = new QMSingleButton(socialInterractions, 0, 0, "Clear\nIgnore\nList", delegate
            {
                Q29uZmln.config.ignoreList.Clear();

                MelonCoroutines.Start(VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.ResetHideDistantAvatars());
            }, "Clears the distant avatar ignore list");

            var addToIgnoreListButton = new QMSingleButton(socialInterractions, 0, 1, "Add To\nIgnore List", delegate
            {
                var apiUser = QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0;

                string displayName;

                if (!Q29uZmln.config.ignoreList.TryGetValue(apiUser.id, out displayName))
                {
                    Q29uZmln.config.ignoreList.Add(apiUser.id, apiUser.displayName);
                    Q29uZmln.config.WriteConfig();
                }

                else TW9vbnJpc2VDb25zb2xl.ErrorLog($"{displayName} is already ignored...");

            }, "Adds selected user to distant avatar hider ignore list");
            UshioRubyModifiers.SetHalfButton(addToIgnoreListButton, UshioRubyModifiers.HalfPosition.Top, UshioRubyModifiers.Rotation.Horizontal);

            var removeFromIgnoreListButton = new QMSingleButton(socialInterractions, 0, 1, "Remove From\nIgnore List", delegate
            {
                var selectedUser = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.GetSelectedPlayer(QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0.id);
                var selectedId = selectedUser.prop_APIUser_0.id;

                string displayName;

                if (Q29uZmln.config.ignoreList.TryGetValue(selectedId, out displayName))
                {
                    Q29uZmln.config.ignoreList.Remove(selectedId);
                    Q29uZmln.config.WriteConfig();
                }

                MelonCoroutines.Start(VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.ResetHideDistantAvatars());
            }, "Removes selected user from distant avatar hider ignore list");
            UshioRubyModifiers.SetHalfButton(removeFromIgnoreListButton, UshioRubyModifiers.HalfPosition.Bottom, UshioRubyModifiers.Rotation.Horizontal);

            var playerTeleport = new QMSingleButton(socialInterractions, 1, 0, "Teleport\nTo", delegate
            {
                var foundPlayer = QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0;

                VUd4aGVXVnlWRkE9.TeleportTo(foundPlayer.id);
            }, "Teleports you to the selected player.");
            if (user == null) return;
            if (user.Premium)
            {
                var reportCrasher = new QMSingleButton(socialInterractions, 5, 0, "Report as\nCrasher", delegate
                {
                    var foundPlayer = QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0;
                    var foundVrcPlayer = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.GetSelectedPlayer(foundPlayer.id);
                    VVROS2FHTXlhR3hqYkVwc1kwRTlQUT09.ReportCrasher(foundPlayer.displayName, foundPlayer.id, foundVrcPlayer.prop_ApiAvatar_0);
                }, "Sends a report to the Moonrise database of the selected player as a potential crasher");
            }
        }
    }
}
