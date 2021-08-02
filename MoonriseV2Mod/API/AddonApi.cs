using MelonLoader;
using MoonriseTabApi;
using RubyButtonAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MoonriseV2Mod.BaseFunctions;
using UnhollowerRuntimeLib;
using MoonriseV2Mod.API;
using MoonriseV2Mod;

namespace MoonriseApi
{
    public static class AddonMods
    {
        public static bool isInitialized = false;

        static UshioMultiPageNested addonPage;
        static List<AddonMod> addOnMods;
        static QMNestedButton otherMods;

        static bool firstButtonAdded = true;
        static int highestPageNumber = 1;
        static int currentButtonPos = 0;

        static QMSingleButton addonModButton
        {
            get
            {
                return otherMods.getMainButton();
            }
        }

        public static void PushBadge(string text, Color color)
        {
            TW9vbnJpc2VCYXNlRnVuY3Rpb25z.baseFunctions.menuTab.SetBadgeActive(true, text, color);
        }

        static bool addonButtonActive
        {
            get
            {
                return addonModButton.getGameObject().active;
            }
        }

        [InitMethod]
        internal static void Initialize()
        {
            MelonCoroutines.Start(InitializeAddons());
        }

        private static IEnumerator InitializeAddons()
        {
            while (MenuTab.MenuTabs == null) yield return null;

            otherMods = new QMNestedButton(TW9vbnJpc2VCYXNlRnVuY3Rpb25z.baseFunctions.menuTab.NestedButton, 4, 2, "Add-ons", "Various other mods.");
            addonPage = new UshioMultiPageNested(otherMods, highestPageNumber);
            addOnMods = new List<AddonMod>();

            //TabApi.MakeTabButton(otherMods, addonIcon, MenuTab.MenuSide.Bottom, 2, "", Color.blue);

            otherMods.getMainButton().setActive(false);

            isInitialized = true;
        }

        public static QMNestedButton MakeAddonNestedButton(string buttonText, string buttonTooltips, Color? btnBackgroundColor = null, Color? btnTextColor = null, Color? backBtnBackgroundColor = null, Color? backBtnTextColor = null)
        {
            if (!addonButtonActive)
            {
                otherMods.getMainButton().setActive(true);
            }

            QMNestedButton nestedButton;
            AddonMod tempAddon;
            Vector2 buttonPos = AddonMod.GetButtonPosition(currentButtonPos);
            int xPos = (int)buttonPos.x;
            int yPos = (int)buttonPos.y;
            nestedButton = new QMNestedButton(otherMods, xPos, yPos, buttonText, buttonTooltips, btnBackgroundColor, btnTextColor, backBtnBackgroundColor, backBtnTextColor);
            tempAddon = new AddonMod(nestedButton, highestPageNumber, currentButtonPos);
            addOnMods.Add(tempAddon);
            addonPage.AddElementToMenu(nestedButton, highestPageNumber);

            if (currentButtonPos == 0)
            {
                if (!firstButtonAdded)
                {
                    addonPage.IncreaseNumberOfPages();
                }

                else
                {
                    firstButtonAdded = false;
                }
            }

            if (currentButtonPos == 8)
            {
                currentButtonPos = 0;
                highestPageNumber++;
            }

            else
            {
                currentButtonPos++;
            }

            return nestedButton;
        }

        public static MenuTab MakeAddonTab(QMNestedButton nestedButton, Sprite sprite, MenuTab.MenuSide menuSide, int offset, string badgeText, Color badgeColor)
        {
            var tempTab = TabApi.MakeTabButton(nestedButton, sprite, menuSide, offset, badgeText, badgeColor);
            return tempTab;
        }

        public enum TargetMenu
        {
            Main,
            Interaction
        }
    }

    public class AddonMod
    {
        public AddonMod(QMNestedButton nestedButton, int pageNumber, int buttonPosition)
        {
            this.ModNestedButton = nestedButton;
            this.PageNumber = pageNumber;
            this.ButtonPosition = (ButtonPos)buttonPosition;
        }
        public QMNestedButton ModNestedButton { get; set; }
        public int PageNumber { get; set; }
        public ButtonPos ButtonPosition { get; set; }
        internal static Vector2 GetButtonPosition(int buttonPosition)
        {
            Vector2 vec2 = Vector2.zero;
            switch (buttonPosition)
            {
                case (int)ButtonPos.TopLeft:
                    vec2 = new Vector2(1, 0);
                    break;

                case (int)ButtonPos.TopCenter:
                    vec2 = new Vector2(2, 0);
                    break;

                case (int)ButtonPos.TopRight:
                    vec2 = new Vector2(3, 0);
                    break;

                case (int)ButtonPos.CenterLeft:
                    vec2 = new Vector2(1, 1);
                    break;

                case (int)ButtonPos.Center:
                    vec2 = new Vector2(2, 1);
                    break;

                case (int)ButtonPos.CenterRight:
                    vec2 = new Vector2(3, 1);
                    break;

                case (int)ButtonPos.BottomLeft:
                    vec2 = new Vector2(1, 2);
                    break;

                case (int)ButtonPos.BottomCenter:
                    vec2 = new Vector2(2, 2);
                    break;

                case (int)ButtonPos.BottomRight:
                    vec2 = new Vector2(3, 2);
                    break;

                default:

                    break;
            }

            return vec2;
        }

        public enum ButtonPos
        {
            TopLeft = 0,
            TopCenter = 1,
            TopRight = 2,
            CenterLeft = 3,
            Center = 4,
            CenterRight = 5,
            BottomLeft = 6,
            BottomCenter = 7,
            BottomRight = 8
        }
    }
}
