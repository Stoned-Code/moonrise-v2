using MelonLoader;
using RubyButtonAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UshioUI;

namespace MoonriseV2Mod.API
{
    public static class UshioRubyModifiers
    {
        /// <summary>
        /// Makes a QMSingleButton a half button.
        /// </summary>
        /// <param name="rubySingleButton">QMSingleButton you want to be a half button.</param>
        /// <param name="buttonPosition">Top or bottom button</param>
        public static void SetHalfButton(QMSingleButton rubySingleButton, HalfPosition buttonPosition, Rotation rot)
        {
            if (rot == Rotation.Horizontal) rubySingleButton.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1.0f, 2.0f);
            else rubySingleButton.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(2.0f, 1.0f);
            if (buttonPosition == HalfPosition.Top)
            {
                rubySingleButton.getGameObject().GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 105);
            }

            else
            {
                rubySingleButton.getGameObject().GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);
            }
        }

        /// <summary>
        /// Makes QMNestedButton a half button.
        /// </summary>
        /// <param name="rubyNestedButton">QMNestedButton you want to be a half button.</param>
        /// <param name="buttonPosition">Top or bottom button</param>
        public static void SetHalfButton(QMNestedButton rubyNestedButton, HalfPosition buttonPosition, Rotation rot)
        {
            if (rot == Rotation.Horizontal) rubyNestedButton.getMainButton().getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1.0f, 2.0f);
            else rubyNestedButton.getMainButton().getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(2.0f, 1.0f);

            if (buttonPosition == HalfPosition.Top)
            {
                rubyNestedButton.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 105);
            }

            else
            {
                rubyNestedButton.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);
            }
        }

        /// <summary>
        /// Makes QMNestedButton a half button.
        /// </summary>
        /// <param name="button">Button you want to be a half button.</param>
        /// <param name="buttonPosition">Top or bottom button</param>
        public static void SetHalfButton(Button button, HalfPosition buttonPosition, Rotation rot)
        {
            if (rot == Rotation.Horizontal) button.GetComponent<RectTransform>().sizeDelta /= new Vector2(1.0f, 2.0f);
            else button.GetComponent<RectTransform>().sizeDelta /= new Vector2(2.0f, 1.0f);

            if (buttonPosition == HalfPosition.Top)
            {
                button.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 105);
            }

            else
            {
                button.gameObject.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);
            }
        }

        public static void MoveHalf(QMSingleButton rubySingleButton, HalfPosition buttonPosition)
        {
            if (buttonPosition == HalfPosition.Top)
            {
                rubySingleButton.getGameObject().GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 210);
            }

            else
            {
                rubySingleButton.getGameObject().GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 210);
            }
        }

        public static void MoveHalf(QMToggleButton rubyToggleButton, HalfPosition buttonPosition)
        {
            if (buttonPosition == HalfPosition.Top)
            {
                rubyToggleButton.getGameObject().GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 210);
            }

            else
            {
                rubyToggleButton.getGameObject().GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 210);
            }
        }

        /// <summary>
        /// Makes a QMSingleButton text only. (No Button or Image)
        /// </summary>
        /// <param name="rubySingleButton">QMSingleButton you want to make text only</param>
        public static void MakeTextOnly(QMSingleButton rubySingleButton)
        {
            var buttonComponent = rubySingleButton.getGameObject().GetComponentInChildren<Button>();
            var imageComponent = rubySingleButton.getGameObject().GetComponentInChildren<Image>();

            GameObject.Destroy(buttonComponent);
            GameObject.Destroy(imageComponent);
        }

        /// <summary>
        /// Makes a QMSingleButton text only. (No Button or Image)
        /// </summary>
        /// <param name="rubySingleButton">QMSingleButton you want to make text only</param>
        /// <param name="textColor">Color you want the text to be</param>
        public static void MakeTextOnly(QMSingleButton rubySingleButton, Color textColor)
        {
            var buttonComponent = rubySingleButton.getGameObject().GetComponentInChildren<Button>();
            var imageComponent = rubySingleButton.getGameObject().GetComponentInChildren<Image>();
            var textComponent = rubySingleButton.getGameObject().GetComponentInChildren<Text>();

            textComponent.color = textColor;

            GameObject.Destroy(buttonComponent);
            GameObject.Destroy(imageComponent);
        }

        /// <summary>
        /// Makes QMSingleButton button only. (No Image or Text)
        /// </summary>
        /// <param name="rubySinglButton">QMSingleButton you want to strip of text and image</param>
        public static void MakeButtonOnly(QMSingleButton rubySinglButton)
        {
            rubySinglButton.getGameObject().GetComponentInChildren<Image>().enabled = false;
            rubySinglButton.getGameObject().GetComponentInChildren<Text>().enabled = false;
        }

        /// <summary>
        /// Makes QMNestedButton button only. (No Image or Text)
        /// </summary>
        /// <param name="rubyNestedButton">QMNestedButton yo want to strip of text and image</param>
        public static void MakeButtonOnly(QMNestedButton rubyNestedButton)
        {
            rubyNestedButton.getMainButton().getGameObject().GetComponentInChildren<Image>().enabled = false;
            rubyNestedButton.getMainButton().getGameObject().GetComponentInChildren<Text>().enabled = false;
        }

        /// <summary>
        /// Turns a QMSingleButton into an arrow button.
        /// </summary>
        /// <param name="qmSingleButton">QMSingleButton you want to turn into an arrow.</param>
        /// <param name="arrowDirection">Direction you want the arrow to point.</param>
        public static void MakeArrowButton(QMSingleButton qmSingleButton, ArrowDirection arrowDirection)
        {
            var arrowSprite = QuickMenu.prop_QuickMenu_0.transform.Find("QuickMenu_NewElements/_CONTEXT/QM_Context_User_Selected/NextArrow_Button").GetComponentInChildren<Image>().sprite;
            if (arrowDirection == ArrowDirection.Up)
            {
                qmSingleButton.getGameObject().GetComponentInChildren<Image>().sprite = arrowSprite;
                qmSingleButton.getGameObject().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                qmSingleButton.getGameObject().GetComponentInChildren<Text>().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
            }

            else if (arrowDirection == ArrowDirection.Down)
            {
                qmSingleButton.getGameObject().GetComponentInChildren<Image>().sprite = arrowSprite;
                qmSingleButton.getGameObject().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
                qmSingleButton.getGameObject().GetComponentInChildren<Text>().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }

            else if (arrowDirection == ArrowDirection.Left)
            {
                qmSingleButton.getGameObject().GetComponentInChildren<Image>().sprite = arrowSprite;
                qmSingleButton.getGameObject().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                qmSingleButton.getGameObject().GetComponentInChildren<Text>().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, -180));
            }

            else if (arrowDirection == ArrowDirection.Right)
            {
                qmSingleButton.getGameObject().GetComponentInChildren<Image>().sprite = arrowSprite;
            }
        }

        /// <summary>
        /// Turns a QMNestedButton into an arrow button.
        /// </summary>
        /// <param name="qmNestedButton">QMNestedButton you want to turn into an arrow.</param>
        /// <param name="arrowDirection">Direction you want the arrow to point.</param>
        public static void MakeArrowButton(QMNestedButton qmNestedButton, ArrowDirection arrowDirection)
        {
            var arrowSprite = QuickMenu.prop_QuickMenu_0.transform.Find("QuickMenu_NewElements/_CONTEXT/QM_Context_User_Selected/NextArrow_Button").GetComponentInChildren<Image>().sprite;
            if (arrowDirection == ArrowDirection.Up)
            {
                qmNestedButton.getMainButton().getGameObject().GetComponentInChildren<Image>().sprite = arrowSprite;
                qmNestedButton.getMainButton().getGameObject().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                qmNestedButton.getMainButton().getGameObject().GetComponentInChildren<Text>().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
            }

            else if (arrowDirection == ArrowDirection.Down)
            {
                qmNestedButton.getMainButton().getGameObject().GetComponentInChildren<Image>().sprite = arrowSprite;
                qmNestedButton.getMainButton().getGameObject().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
                qmNestedButton.getMainButton().getGameObject().GetComponentInChildren<Text>().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }

            else if (arrowDirection == ArrowDirection.Left)
            {
                qmNestedButton.getMainButton().getGameObject().GetComponentInChildren<Image>().sprite = arrowSprite;
                qmNestedButton.getMainButton().getGameObject().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                qmNestedButton.getMainButton().getGameObject().GetComponentInChildren<Text>().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, -180));
            }

            else if (arrowDirection == ArrowDirection.Right)
            {
                qmNestedButton.getMainButton().getGameObject().GetComponentInChildren<Image>().sprite = arrowSprite;
            }
        }

        /// <summary>
        /// Gets the transform for nested button menu
        /// </summary>
        /// <param name="nestedButton">QMNestedButton you want to get the menu transform of.</param>
        /// <returns></returns>
        public static Transform GetMenuTransform(QMNestedButton nestedButton)
        {
            var transform = nestedButton.getBackButton().getGameObject().transform.GetParent();
            return transform;
        }

        /// <summary>
        /// Allows you to add an extra action to the button that opens a new menu.
        /// </summary>
        /// <param name="nestedButton">QMNestedButton you want to add an action to.</param>
        /// <param name="addedAction">Action you want to add to the nested button.</param>
        public static void AddNestedAction(QMNestedButton nestedButton, System.Action addedAction)
        {
            var mainButton = nestedButton.getMainButton();

            mainButton.getGameObject().GetComponent<Button>().onClick.AddListener(addedAction);
        }
        public static void MakeTextureButton(QMNestedButton nestedButton, Sprite sprite)
        {
            var mainButton = nestedButton.getMainButton();
            mainButton.getGameObject().GetComponentInChildren<Image>().sprite = sprite;
            mainButton.getGameObject().GetComponentInChildren<Image>().color = Color.white;
        }
        public enum ArrowDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public enum HalfPosition
        {
            Top,
            Bottom
        }

        public enum Rotation
        {
            Verticle,
            Horizontal
        }
    }

    public class UshioMultiPageButtonBase
    {
        public int IndexNumber { get; set; }
        public int PageNumber { get; set; }
    }

    public class UshioMultiPageNested
    {
        #region Constructors

        /// <summary>
        /// Make a multi-page set of buttons in a nested menu(Don't user the three spots in the menu to the right)
        /// </summary>
        /// <param name="menu">Menu that you want the multi-page buttons set on.</param>
        /// <param name="pageNumbers">The amount of pages you want the button set to have.</param>
        /// <param name="extraUpArrowAction">Extra actions called when you press the up arrow.</param>
        /// <param name="extraDownArrowAction">Extra actions called when you press the down arrow.</param>
        public UshioMultiPageNested(QMNestedButton menu, int pageNumbers, System.Action extraUpArrowAction = null, System.Action extraDownArrowAction = null)
        {
            this.MenuElements = new List<UshioMultiMenuElements>();
            this.NumberOfPages = pageNumbers;
            this.CurrentPage = 1;

            this.PageUp = new QMSingleButton(menu, 4, 0, "", delegate
            {
                PreviousPage();
            }, $"Move to the previous page of \"{menu.getMainButton().getGameObject().GetComponentInChildren<Text>().text}\"");
            UshioRubyModifiers.MakeArrowButton(this.PageUp, UshioRubyModifiers.ArrowDirection.Up);

            this.PageDown = new QMSingleButton(menu, 4, 2, "", delegate
            {
                NextPage();
            }, $"Move to the next page of \"{menu.getMainButton().getGameObject().GetComponentInChildren<Text>().text}\"");
            UshioRubyModifiers.MakeArrowButton(this.PageDown, UshioRubyModifiers.ArrowDirection.Down);

            this.PageStatus = new QMSingleButton(menu, 4, 1, $"Page:\n1 of {pageNumbers}", delegate { }, "", null, Color.yellow);
            UshioRubyModifiers.MakeTextOnly(this.PageStatus, Color.yellow);

            if (extraUpArrowAction != null) this.PageUp.getGameObject().GetComponentInChildren<Button>().onClick.AddListener(extraUpArrowAction);

            if (extraDownArrowAction != null) this.PageDown.getGameObject().GetComponentInChildren<Button>().onClick.AddListener(extraDownArrowAction);
        }

        /// <summary>
        /// Make a multi-page set of buttons in a nested menu(Don't user the three spots in the menu to the right)
        /// </summary>
        /// <param name="menu">Menu that you want the multi-page buttons set on.</param>
        /// <param name="pageNumbers">The amount of pages you want the button set to have.</param>
        /// <param name="extraUpArrowAction">Extra actions called when you press the up arrow.</param>
        /// <param name="extraDownArrowAction">Extra actions called when you press the down arrow.</param>
        public UshioMultiPageNested(string menu, int pageNumbers, System.Action extraUpArrowAction = null, System.Action extraDownArrowAction = null)
        {
            this.MenuElements = new List<UshioMultiMenuElements>();
            this.NumberOfPages = pageNumbers;
            this.CurrentPage = 1;

            this.PageUp = new QMSingleButton(menu, 4, 0, "", delegate
            {

            }, $"Move to the previous page of \"{menu}\"");
            UshioRubyModifiers.MakeArrowButton(this.PageUp, UshioRubyModifiers.ArrowDirection.Up);

            this.PageDown = new QMSingleButton(menu, 4, 2, "", delegate
            {

            }, $"Move to the next page of \"{menu}\"");

            this.PageStatus = new QMSingleButton(menu, 4, 1, $"Page:\n1 of {pageNumbers}", delegate { }, "", null, Color.yellow);
            UshioRubyModifiers.MakeTextOnly(this.PageStatus, Color.yellow);

            if (extraUpArrowAction != null) this.PageUp.getGameObject().GetComponentInChildren<Button>().onClick.AddListener(extraUpArrowAction);

            if (extraDownArrowAction != null) this.PageDown.getGameObject().GetComponentInChildren<Button>().onClick.AddListener(extraDownArrowAction);
        }

        #endregion Constructors

        protected int NumberOfPages;
        protected int CurrentPage;

        protected QMSingleButton PageUp;
        protected QMSingleButton PageStatus;
        protected QMSingleButton PageDown;

        protected List<UshioMultiMenuElements> MenuElements { get; set; }

        #region Add Elements

        /// <summary>
        /// Add an element to the list of elements.
        /// </summary>
        /// <param name="element">Element you want to add the the element list.</param>
        /// <param name="pageNumber">The page you want the new element to be on</param>
        public void AddElementToMenu(QMSingleButton element, int pageNumber)
        {
            MenuElements.Add(new UshioMultiMenuElements(element, pageNumber, CurrentPage));
        }

        /// <summary>
        /// Add an element to the list of elements.
        /// </summary>
        /// <param name="element">Element you want to add the the element list.</param>
        /// <param name="pageNumber">The page you want the new element to be on.</param>
        public void AddElementToMenu(QMToggleButton element, int pageNumber)
        {
            MenuElements.Add(new UshioMultiMenuElements(element, pageNumber, CurrentPage));
        }

        /// <summary>
        /// Add an element to the list of elements.
        /// </summary>
        /// <param name="element">Element you want to add the the element list.</param>
        /// <param name="pageNumber">The page you want the new element to be on.</param>
        public void AddElementToMenu(QMNestedButton element, int pageNumber)
        {
            MenuElements.Add(new UshioMultiMenuElements(element, pageNumber, CurrentPage));
        }

        /// <summary>
        /// Add an element to the list of elements.
        /// </summary>
        /// <param name="element">Element you want to add the the element list.</param>
        /// <param name="pageNumber">The page you want the new element to be on.</param>
        public void AddElementToMenu(UshioMenuSlider element, int pageNumber)
        {
            MenuElements.Add(new UshioMultiMenuElements(element, pageNumber, CurrentPage));
        }

        #endregion Add Elements

        public int GetCurrentPage() => CurrentPage;

        public void IncreaseNumberOfPages(int interval = 1)
        {
            NumberOfPages += interval;
            PageStatus.setButtonText($"Page:\n{CurrentPage} of {NumberOfPages}");
        }

        public void SetPageAmount(int pages)
        {
            this.NumberOfPages = pages;
            this.CurrentPage = 1;
        }

        public static void MultiPageFromList(QMNestedButton menu)
        {
            //var buttonNumber = 0;
            //for (int i = 0; i < videosList.Count; i++)
            //{
            //    switch (buttonNumber)
            //    {
            //        case 0:
            //            {
            //                var vidButton = new QMSingleButton(menu, 1, 0, video.videoName, delegate
            //                {

            //                }, "Puts " + video.videoName + " on the video player", null, null);

            //                video.videoButton = vidButton;
            //                break;
            //            }

            //        case 1:
            //            {
            //                var vidButton = new QMSingleButton(menu, 2, 0, video.videoName, delegate
            //                {
            //                    if (!getVideoLinks)
            //                    {
            //                        MelonCoroutines.Start(video.PlayPersonalVideo(videoPlayerMod.videoTrollAutoplay, videoPlayerMod));
            //                    }

            //                    else
            //                    {
            //                        video.CopyVideoUrl();
            //                    }
            //                }, "Puts " + video.videoName + " on the video player", null, null);

            //                video.videoButton = vidButton;
            //                break;
            //            }

            //        case 2:
            //            {
            //                var vidButton = new QMSingleButton(menu, 3, 0, video.videoName, delegate
            //                {
            //                    if (!getVideoLinks)
            //                    {
            //                        MelonCoroutines.Start(video.PlayPersonalVideo(videoPlayerMod.videoTrollAutoplay, videoPlayerMod));
            //                    }

            //                    else
            //                    {
            //                        video.CopyVideoUrl();
            //                    }
            //                }, "Puts " + video.videoName + " on the video player", null, null);

            //                video.videoButton = vidButton;
            //                break;
            //            }

            //        case 3:
            //            {
            //                var vidButton = new QMSingleButton(menu, 1, 1, video.videoName, delegate
            //                {
            //                    if (!getVideoLinks)
            //                    {
            //                        MelonCoroutines.Start(video.PlayPersonalVideo(videoPlayerMod.videoTrollAutoplay, videoPlayerMod));
            //                    }

            //                    else
            //                    {
            //                        video.CopyVideoUrl();
            //                    }
            //                }, "Puts " + video.videoName + " on the video player", null, null);

            //                video.videoButton = vidButton;
            //                break;
            //            }

            //        case 4:
            //            {
            //                var vidButton = new QMSingleButton(menu, 2, 1, video.videoName, delegate
            //                {
            //                    if (!getVideoLinks)
            //                    {
            //                        MelonCoroutines.Start(video.PlayPersonalVideo(videoPlayerMod.videoTrollAutoplay, videoPlayerMod));
            //                    }

            //                    else
            //                    {
            //                        video.CopyVideoUrl();
            //                    }
            //                }, "Puts " + video.videoName + " on the video player", null, null);

            //                video.videoButton = vidButton;
            //                break;
            //            }

            //        case 5:
            //            {
            //                var vidButton = new QMSingleButton(menu, 3, 1, video.videoName, delegate
            //                {
            //                    if (!getVideoLinks)
            //                    {
            //                        MelonCoroutines.Start(video.PlayPersonalVideo(videoPlayerMod.videoTrollAutoplay, videoPlayerMod));
            //                    }

            //                    else
            //                    {
            //                        video.CopyVideoUrl();
            //                    }
            //                }, "Puts " + video.videoName + " on the video player", null, null);

            //                video.videoButton = vidButton;
            //                break;
            //            }

            //        case 6:
            //            {
            //                var vidButton = new QMSingleButton(menu, 1, 2, video.videoName, delegate
            //                {
            //                    if (!getVideoLinks)
            //                    {
            //                        MelonCoroutines.Start(video.PlayPersonalVideo(videoPlayerMod.videoTrollAutoplay, videoPlayerMod));
            //                    }

            //                    else
            //                    {
            //                        video.CopyVideoUrl();
            //                    }
            //                }, "Puts " + video.videoName + " on the video player", null, null);

            //                video.videoButton = vidButton;
            //                break;
            //            }

            //        case 7:
            //            {
            //                var vidButton = new QMSingleButton(menu, 2, 2, video.videoName, delegate
            //                {
            //                    if (!getVideoLinks)
            //                    {
            //                        MelonCoroutines.Start(video.PlayPersonalVideo(videoPlayerMod.videoTrollAutoplay, videoPlayerMod));
            //                    }

            //                    else
            //                    {
            //                        video.CopyVideoUrl();
            //                    }
            //                }, "Puts " + video.videoName + " on the video player", null, null);

            //                video.videoButton = vidButton;
            //                break;
            //            }

            //        case 8:
            //            {
            //                var vidButton = new QMSingleButton(menu, 3, 2, video.videoName, delegate
            //                {
            //                    if (!getVideoLinks)
            //                    {
            //                        MelonCoroutines.Start(video.PlayPersonalVideo(videoPlayerMod.videoTrollAutoplay, videoPlayerMod));
            //                    }

            //                    else
            //                    {
            //                        video.CopyVideoUrl();
            //                    }
            //                }, "Puts " + video.videoName + " on the video player", null, null);

            //                video.videoButton = vidButton;
            //                break;
            //            }
            //    }

            //    if (video.menuIndex != currentMenuIndex)
            //    {
            //        video.videoButton.setActive(false);
            //    }

            //    video.videoButton.getGameObject().GetComponentInChildren<Text>().resizeTextForBestFit = true;
            //}
        }

        public void ClearElements()
        {
            for (int i = 0; i < MenuElements.Count; i++)
            {
                var element = MenuElements[i];

                element.Destroy();
            }

            MenuElements.Clear();
        }

        public void SetPage()
        {
            for (int i = 0; i < MenuElements.Count; i++)
            {
                UshioMultiMenuElements element = MenuElements[i];
                if (element.PageNumber != CurrentPage)
                {
                    element.SetElementActive(false);
                }

                else
                {
                    element.SetElementActive(true);
                }
            }

            PageStatus.setButtonText($"Page:\n{CurrentPage} of {NumberOfPages}");
        }

        public void NextPage()
        {
            if (CurrentPage != NumberOfPages)
            {
                CurrentPage++;
            }

            for (int i = 0; i < MenuElements.Count; i++)
            {
                UshioMultiMenuElements element = MenuElements[i];
                if (element.PageNumber != CurrentPage)
                {
                    element.SetElementActive(false);
                }

                else
                {
                    element.SetElementActive(true);
                }
            }

            PageStatus.setButtonText($"Page:\n{CurrentPage} of {NumberOfPages}");
        }

        public void PreviousPage()
        {
            if (CurrentPage != 1)
            {
                CurrentPage--;
            }

            foreach (UshioMultiMenuElements element in MenuElements)
            {
                if (element.PageNumber != CurrentPage)
                {
                    element.SetElementActive(false);
                }

                else
                {
                    element.SetElementActive(true);
                }
            }

            PageStatus.setButtonText($"Page:\n{CurrentPage} of {NumberOfPages}");
        }
    }

    public class UshioMultiMenuElements
    {
        public UshioMultiMenuElements(QMSingleButton singleButton, int pageNumber, int currentPage)
        {
            this.MenuButton = singleButton;
            this.PageNumber = pageNumber;

            if (pageNumber != currentPage) singleButton.setActive(false);
        }

        public UshioMultiMenuElements(QMToggleButton toggleButton, int pageNumber, int currentPage)
        {
            this.MenuToggle = toggleButton;
            this.PageNumber = pageNumber;

            if (pageNumber != currentPage) toggleButton.setActive(false);
        }

        public UshioMultiMenuElements(QMNestedButton nestedButton, int pageNumber, int currentPage)
        {
            this.MenuNested = nestedButton;
            this.PageNumber = pageNumber;

            if (pageNumber != currentPage) nestedButton.getMainButton().setActive(false);
        }

        public UshioMultiMenuElements(UshioMenuSlider slider, int pageNumber, int currentPage)
        {
            this.MenuSlider = slider;
            this.PageNumber = pageNumber;

            if (pageNumber != currentPage) slider.SetSliderActive(false);
        }

        public QMNestedButton MenuNested;
        public QMSingleButton MenuButton;
        public QMToggleButton MenuToggle;
        public UshioMenuSlider MenuSlider;
        public int PageNumber;

        public void SetElementActive(bool setActive)
        {
            if (MenuButton != null) MenuButton.setActive(setActive);
            if (MenuNested != null) MenuNested.getMainButton().setActive(setActive);
            if (MenuSlider != null) MenuSlider.SetSliderActive(setActive);
            if (MenuToggle != null) MenuToggle.setActive(setActive);
        }

        public void Destroy()
        {
            if (MenuButton != null) MenuButton.DestroyMe();
            if (MenuNested != null) MenuNested.DestroyMe();
            if (MenuSlider != null) MenuSlider.Dispose();
            if (MenuToggle != null) MenuToggle.DestroyMe();
        }
    }
}
