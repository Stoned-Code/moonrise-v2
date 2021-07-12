using UnityEngine;
using UnityEngine.UI;

namespace MoonriseV2Mod.Settings
{
    internal static class AddBlocker
    {
        static GameObject vrcpBanner => GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/HeaderContainer/VRCPlusBanner");
        static GameObject vrcpMiniBanner => GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/VRCPlusMiniBanner");
        static GameObject vrcpThankYou => GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/VRCPlusThankYou");
        static GameObject vrcpSocialSupporter => GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/User Panel/Supporter");
        static GameObject vrcpAvatarPromotion => GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Vertical Scroll View/Viewport/Content/Favorite Avatar List/GetMoreFavorites");
        private static void Toggle(GameObject obj)
        {
            if (obj == null) return;
            obj.GetComponentInChildren<Image>().enabled = !Config.config.addBlocker;
            obj.GetComponentInChildren<Button>().enabled = !Config.config.addBlocker;
        }
        public static void ToggleVRCPlusPromotions()
        {
            if (vrcpBanner != null && vrcpBanner.GetComponentInChildren<Image>().enabled == Config.config.addBlocker)
            {
                Toggle(vrcpBanner);
            }
            if (vrcpMiniBanner != null && vrcpMiniBanner.GetComponentInChildren<Image>().enabled == Config.config.addBlocker)
            {
                Toggle(vrcpMiniBanner);
            }
            if (vrcpThankYou != null && vrcpThankYou.GetComponentInChildren<Image>().enabled == Config.config.addBlocker)
            {
                Toggle(vrcpThankYou);
            }
            if (vrcpSocialSupporter != null && vrcpSocialSupporter.GetComponentInChildren<Image>().enabled == Config.config.addBlocker)
            {
                Toggle(vrcpSocialSupporter);
            }
            if (vrcpAvatarPromotion != null && vrcpAvatarPromotion.GetComponentInChildren<Image>().enabled == Config.config.addBlocker)
            {
                Toggle(vrcpAvatarPromotion);
            }
        }
    }
}
