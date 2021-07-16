/***************************************************
 * Moonrise Quick Menu Tabs Api
 * By: UshioHiko/UHModz
 * Comments: Feel free to make changes. Might not be 
 *           the best way to do it, but it works.
 * Credits: DubyaDude - Ruby Button Api
 ***************************************************/

using MelonLoader;
using MoonriseV2Mod;
using RubyButtonAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MoonriseTabApi.MenuTab;

namespace MoonriseTabApi
{
    internal static class TabApi
    {
        // Default color for the tab icon when the tab's menu isn't open
        public static Color DisabledIconColor
        {
            get
            {
                return new Color(0.471f, 0.471f, 0.471f, 1f);
            }
        }

        // Default color for the tab icon when the tab's menu is open
        public static Color EnabledIconColor
        {
            get
            {
                return new Color(0.941f, 0.941f, 0.941f, 1f);
            }
        }

        // Defualt color block for the tab background for when the tab's menu is open
        public static ColorBlock EnabledColorBlock
        {
            get
            {
                return new ColorBlock
                {
                    colorMultiplier = 1,
                    disabledColor = new Color(0.784f, 0.784f, 0.784f, 0.502f),
                    highlightedColor = new Color(0.055f, 0.651f, 0.678f, 1f),
                    normalColor = new Color(0.03137255f, 0.3764706f, 0.3921569f, 1f),
                    pressedColor = new Color(0.784f, 0.784f, 0.784f, 1f),
                    fadeDuration = 0.1f
                };
            }
        }

        // Defualt color block for the tab background for when the tab's menu isn't open
        public static ColorBlock DisabledColorBlock
        {
            get
            {
                return new ColorBlock
                {
                    colorMultiplier = 1,
                    disabledColor = new Color(0.784f, 0.784f, 0.784f, 0.502f),
                    highlightedColor = new Color(0.055f, 0.651f, 0.678f, 0.7058824f),
                    normalColor = new Color(0f, 0f, 0f, 0.7058824f),
                    pressedColor = new Color(0.784f, 0.784f, 0.784f, 0.7058824f),
                    fadeDuration = 0.1f
                };
            }
        }

        // Vrc's home tab
        public static Button VRCHomeTab
        {
            get
            {
                return GameObject.Find("UserInterface/QuickMenu/QuickModeTabs/HomeTab").GetComponent<Button>();
            }
        }

        // Checks if the tabs are enabled
        public static bool TabsOpen
        {
            get
            {
                return GameObject.Find("UserInterface/QuickMenu/QuickModeTabs").activeInHierarchy;
            }
        }

        public static bool MenuTabsInitialized = false;

        /// <summary>
        /// Makes a tab menu for vrchat
        /// </summary>
        /// <param name="nestedButton">The nested button you want to turn into a tab menu</param>
        /// <param name="iconSprite">The sprite for the tab's icon</param>
        /// <param name="menuSide">The side of the menu you want it to be on</param>
        /// <param name="x">The x value for the buttons position (1 = right of the notifications tab)</param>
        /// <param name="badgeText">The text you want the badge to have on it</param>
        /// <param name="badgeColor">The color you want the badge to be</param>
        /// <returns></returns>
        public static MenuTab MakeTabButton(QMNestedButton nestedButton, Sprite iconSprite, MenuSide menuSide, int offset, string badgeText, Color badgeColor)
        {
            var mainButton = nestedButton.getMainButton();

            mainButton.getGameObject().transform.SetParent(VRCHomeTab.gameObject.transform.GetParent());
            mainButton.getGameObject().GetComponent<RectTransform>().sizeDelta = VRCHomeTab.GetComponent<RectTransform>().sizeDelta;
            mainButton.getGameObject().transform.localPosition = VRCHomeTab.transform.localPosition;

            mainButton.getGameObject().GetComponent<Image>().sprite = VRCHomeTab.GetComponent<Image>().sprite;
            mainButton.getGameObject().GetComponentInChildren<Text>().enabled = false;

            var icon = GameObject.Instantiate(VRCHomeTab.transform.FindChild("Icon"), mainButton.getGameObject().transform, worldPositionStays: true).GetComponent<Image>();
            icon.sprite = iconSprite;
            icon.color = new Color(0.471f, 0.471f, 0.471f, 1f);

            var badge = GameObject.Instantiate(GameObject.Find("UserInterface/QuickMenu/QuickModeTabs/NotificationsTab/Badge"), mainButton.getGameObject().transform, worldPositionStays: true).GetComponent<RawImage>();
            badge.color = badgeColor;
            badge.rectTransform.localPosition = new Vector3(0, badge.rectTransform.localPosition.y, badge.rectTransform.localPosition.z);

            Vector2 position = new Vector2(VRCHomeTab.GetComponent<RectTransform>().sizeDelta.x + (VRCHomeTab.GetComponent<RectTransform>().sizeDelta.x * offset), 0f);
            mainButton.getGameObject().GetComponent<RectTransform>().anchoredPosition += position;

            mainButton.getGameObject().GetComponent<Button>().colors = DisabledColorBlock;
            var menuTab = new MenuTab(nestedButton, icon, badge, badgeText);

            AddNestedAction(nestedButton, delegate
            {
                if (menuTab.BadgeEnabled)
                    MelonCoroutines.Start(menuTab.BadgeCountDown(5f));
            });

            return menuTab;
        }

        /// <summary>
        /// Adds an action to the nested button
        /// </summary>
        /// <param name="nestedButton">The nested button you want to recieve the action</param>
        /// <param name="addedAction">The extra action you want the button to perform</param>
        public static void AddNestedAction(QMNestedButton nestedButton, System.Action addedAction)
        {
            var mainButton = nestedButton.getMainButton();

            mainButton.getGameObject().GetComponent<Button>().onClick.AddListener(addedAction);
        }
    }

    public class MenuTab
    {
        public static List<MenuTab> MenuTabs { get; set; }

        // A list of tabs in the tabs hierarchy
        public static Button[] VRCTabs
        {
            get
            {
                return GameObject.Find("UserInterface/QuickMenu/QuickModeTabs").GetComponentsInChildren<Button>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nestedButton">The nested button for the tab</param>
        /// <param name="tabIcon">The icon for the tab</param>
        /// <param name="badge">The tab's badge</param>
        /// <param name="badgeText">The text written on the badge</param>
        /// <param name="nestedButton">The nested button of the tab</param>
        public MenuTab(QMNestedButton nestedButton, Image tabIcon, RawImage badge, string badgeText)
        {
            // Change the event to you're own update event
            VFc5dmJuSnBjMlU9.modUpdate += MenuTabsUpdate;

            this.MenuSingleButton = nestedButton.getMainButton();
            this.MenuBackSingleButton = nestedButton.getBackButton();
            this.TabIcon = tabIcon;
            this.Badge = badge;
            this.BadgeText.text = badgeText;
            this.NestedButton = nestedButton;

            this.SetBadgeActive(false, "Update!", Color.blue);

            if (MenuTabs == null) MenuTabs = new List<MenuTab>();
            MenuTabs.Add(this);
        }

        QMSingleButton MenuSingleButton { get; set; }
        QMSingleButton MenuBackSingleButton { get; set; }

        // Returns the tab's button component
        public Button Tab
        {
            get
            {
                return MenuSingleButton.getGameObject().GetComponent<Button>();
            }
        }

        // Returns the tab's back button component
        public GameObject MenuBackButton
        {
            get
            {
                return MenuBackSingleButton.getGameObject();
            }
        }

        // Checks if the tab's menu is open
        public bool TabOpen
        {
            get
            {
                return MenuBackButton.activeInHierarchy;
            }
        }

        // The tab's icon
        public Image TabIcon { get; set; }

        // The tab's badge
        public RawImage Badge { get; set; }

        // The badge's text component
        public Text BadgeText
        {
            get
            {
                return this.Badge.GetComponentInChildren<Text>();
            }
        }

        // Checks if the badge is enabled
        public bool BadgeEnabled
        {
            get
            {
                return Badge.enabled == true && BadgeText.enabled == true;
            }
        }

        // Returns the original nested button
        public QMNestedButton NestedButton { get; set; }

        /// <summary>
        /// Changes the badges active state
        /// </summary>
        /// <param name="badgeActive">If you want the badge to be active or not</param>
        public void SetBadgeActive(bool badgeActive, string text, Color backgroundColor)
        {
            BadgeText.text = text;
            Badge.color = backgroundColor;
            Badge.enabled = badgeActive;
            BadgeText.enabled = badgeActive;
        }

        bool countingDown = false;
        public IEnumerator BadgeCountDown(float seconds)
        {
            if (countingDown) yield break;
            countingDown = true;
            yield return new WaitForSeconds(seconds);
            SetBadgeActive(false, "", Color.blue);
            countingDown = false;
        }

        public void MenuTabsUpdate()
        {
            if (!TabApi.TabsOpen) return;

            try
            {
                if (!this.TabOpen)
                {
                    this.Tab.colors = TabApi.DisabledColorBlock;
                    this.TabIcon.color = TabApi.DisabledIconColor;
                }

                else
                {
                    this.Tab.colors = TabApi.EnabledColorBlock;
                    this.TabIcon.color = TabApi.EnabledIconColor;

                    for (int otherTabIndex = 0; otherTabIndex < VRCTabs.Length; otherTabIndex++)
                    {
                        var otherTab = VRCTabs[otherTabIndex];
                        if (otherTab == this.Tab) continue;

                        var otherIcon = otherTab.transform.FindChild("Icon").GetComponent<Image>();
                        otherTab.colors = TabApi.DisabledColorBlock;
                        if (otherIcon.color != TabApi.DisabledIconColor)
                            otherTab.transform.FindChild("Icon").GetComponent<Image>().color = TabApi.DisabledIconColor;
                    }
                }

            }

            catch { }
        }

        public enum MenuSide
        {
            Bottom = 0,
            Right = 1
        }
    }
}
