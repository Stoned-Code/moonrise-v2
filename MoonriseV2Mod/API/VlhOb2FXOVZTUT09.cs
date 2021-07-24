using MelonLoader;
using MoonriseV2Mod;
using MoonriseV2Mod.API;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;
using System;
using System.Collections;
using System.Linq;
using UnhollowerBaseLib.Attributes;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using VRC;

namespace UshioUI
{
    public static class UshioMenuApi
    {
        public const string ModName = "Moonrise"; // Replace this with your mod's name

        public static Color DefaultPopupColor = Color.white;

        public static ConsoleColor modConsoleColor = ConsoleColor.Cyan; // Replace with the color you want your logs to be in your console
        public static ConsoleColor modErrorConsoleColor = ConsoleColor.Blue; // Replace with the color you want your error logs to be in your console

        public static bool ShortcutMenuInitialized
        {
            get
            {
                return ShortcutMenu != null;
            }
        }
        #region Variables
        //////////////////
        //  Transforms  //
        //////////////////
        
        public static Transform ShortcutMenu
        {
            get
            {
                return QuickMenu.prop_QuickMenu_0.transform.Find("ShortcutMenu");
            }
        }

        public static Transform UserInteractMenu
        {
            get
            {
                return QuickMenu.prop_QuickMenu_0.transform.Find("UserInterface/QuickMenu/UserInteractMenu");
            }

        }

        public static Transform CameraMenu
        {
            get
            {
                return QuickMenu.prop_QuickMenu_0.transform.Find("CameraMenu");
            }
        }

        public static Transform NotificationMenu
        {
            get
            {
                return QuickMenu.prop_QuickMenu_0.transform.Find("NotificationInteractMenu");
            }
        }

        public static Transform UiElementsMenu
        {
            get
            {
                return QuickMenu.prop_QuickMenu_0.transform.Find("UIElementsMenu");
            }
        }

        public static GameObject OnlineFriendsMenu
        {
            get
            {
                return GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/User Panel/OnlineFriend");
            }
        }

        public static Button InviteButton
        {
            get
            {
                return GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/User Panel/OnlineFriend/Actions/Invite").GetComponent<Button>();
            }
        }
        public static Button RespawnButton
        {
            get
            {
                return GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/RespawnButton").GetComponent<Button>();
            }
        }

        public static Button GoHomeButton
        {
            get
            {
                return GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/GoHomeButton").GetComponent<Button>();
            }
        }

        public static Button ToggleSeatedPlay => GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/SitButton").GetComponent<Button>();

        ////////////////////
        //  Game Objects  //
        ////////////////////


        #endregion Variables

        #region Ui Modifications

        public static void AddButtonAction(Button button, System.Action listener)
        {
            button.onClick.AddListener(listener);
        }

        #endregion Ui Modifications

        #region Popup Methods

        /// <summary>
        /// Creates a simple double button popup UI.
        /// </summary>
        /// <param name="Title">The title that shows at the top of the popup. (Supports Richtext)</param>
        /// <param name="Content">The text content of the popup. (Supports Richtext)</param>
        /// <param name="FirstButtonText">The text that's read on the first button. (Supports Richtext)</param>
        /// <param name="FirstButtonListener">Actions taken when the first button is clicked. (Use "delegate")</param>
        /// <param name="SecondButtonText">The text that's read on the second button. (Supports Richtext)</param>
        /// <param name="SecondButtonListener">Actions taken when the second button is clicked. (Use "delegate")</param>
        /// <param name="PopupAction">Actions taken when the popup opens.</param>
        /// <param name="ContentAlignment">Alignment of the popups text content.</param>
        public static void CreateDoubleButtonPopup(string Title, string Content, string FirstButtonText, Action FirstButtonListener, string SecondButtonText, Action SecondButtonListener, Action<VRCUiPopup> PopupAction = null, TextAnchor ContentAlignment = TextAnchor.MiddleCenter)
        {
            var twoChoicePopup = typeof(VRCUiPopupManager).GetMethods().ToList().First(x =>
            {
                if (!x.Name.Contains("Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_"))
                    return false;
                try
                {
                    if (UnhollowerRuntimeLib.XrefScans.XrefScanner.XrefScan(x).Any(z => z.Type == UnhollowerRuntimeLib.XrefScans.XrefType.Global && z.ReadAsObject() != null && z.ReadAsObject().ToString() == "UserInterface/MenuContent/Popups/StandardPopupV2"))
                        return true;
                }
                catch { }
                return false;
            });

            Il2CppSystem.Action firstAction = FirstButtonListener + UshioMenuApi.ClosePopup;
            Il2CppSystem.Action secondAction = SecondButtonListener + UshioMenuApi.ClosePopup;
            Il2CppSystem.Action<VRCUiPopup> openAction = PopupAction;
            twoChoicePopup.Invoke(VRCUiPopupManager.prop_VRCUiPopupManager_0, new System.Object[] { Title, Content, FirstButtonText, firstAction, SecondButtonText, secondAction, openAction });

            var textComponents = GameObject.FindObjectsOfType<Text>();
            foreach (Text text in textComponents)
            {
                text.supportRichText = true;
                if (text.text == Title)
                {
                    text.color = Color.white;
                }

                if (text.text == Content)
                {
                    text.alignment = ContentAlignment;
                }

                if (text.text == SecondButtonText)
                {
                    text.color = Color.white;
                }
            }
        }

        /// <summary>
        /// Creates a simple single button UI.
        /// </summary>
        /// <param name="Title">The title that shows at the top of the popup. (Supports Richtext)</param>
        /// <param name="Content">The text content of the popup. (Supports Richtext)</param>
        /// <param name="ButtonText">The text that's read on the button. (Supports Richtext)</param>
        /// <param name="ButtonAction">Actions taken when the button is clicked. (Use "delegate")</param>
        /// <param name="PopupAction">Actions taken when the popup opens.</param>
        /// <param name="ContentAlignment">Alignment of the popups text content.</param>
        public static void CreateSingleButtonPopup(string Title, string Content, string ButtonText, Action ButtonAction, Action<VRCUiPopup> PopupAction = null, TextAnchor ContentAlignment = TextAnchor.MiddleCenter)
        {
            var singleChoicePopup = typeof(VRCUiPopupManager).GetMethods().ToList().First(x =>
            {
                if (!x.Name.Contains("Method_Public_Void_String_String_String_Action_Action_1_VRCUiPopup_"))
                    return false;
                try
                {
                    if (UnhollowerRuntimeLib.XrefScans.XrefScanner.XrefScan(x).Any(z => z.Type == UnhollowerRuntimeLib.XrefScans.XrefType.Global && z.ReadAsObject() != null && z.ReadAsObject().ToString() == "UserInterface/MenuContent/Popups/StandardPopupV2"))
                        return true;
                }
                catch { }
                return false;
            });

            Il2CppSystem.Action action = ButtonAction + ClosePopup;
            Il2CppSystem.Action<VRCUiPopup> openAction = PopupAction;
            singleChoicePopup.Invoke(VRCUiPopupManager.prop_VRCUiPopupManager_0, new object[] { Title, Content, ButtonText, action, openAction });

            var textComponents = GameObject.FindObjectsOfType<Text>();
            foreach (Text text in textComponents)
            {
                text.supportRichText = true;
                if (text.text == Title)
                {
                    text.color = Color.white;
                }

                if (text.text == Content)
                {
                    text.alignment = ContentAlignment;
                }
            }
        }

        public static void ClosePopup()
        {
            var button = GameObject.Find("ExitButton");

            button.GetComponent<Button>().Press();
        }

        public static void PopupUI(string Message, Color color)
        {
            if (!VRCUiManager.prop_VRCUiManager_0.field_Public_Text_0.supportRichText)
            {
                VRCUiManager.prop_VRCUiManager_0.field_Public_Text_0.supportRichText = true;
            }

            VRCUiManager.prop_VRCUiManager_0.field_Public_Text_0.color = color;
            VRCUiManager.prop_VRCUiManager_0.field_Private_List_1_String_0.Add(Message);
        }

        public static void PopupUI(string Message)
        {
            if (!VRCUiManager.prop_VRCUiManager_0.field_Public_Text_0.supportRichText)
            {
                VRCUiManager.prop_VRCUiManager_0.field_Public_Text_0.supportRichText = true;
            }

            VRCUiManager.prop_VRCUiManager_0.field_Public_Text_0.color = DefaultPopupColor;
            VRCUiManager.prop_VRCUiManager_0.field_Private_List_1_String_0.Add(Message);
        }
        public static void SetMenu()
        {
            MelonCoroutines.Start(SetMenuColliders());
        }
        private static IEnumerator SetMenuColliders()
        {
            while (GameObject.Find("UserInterface/QuickMenu/UserInteractMenu").GetComponent<BoxCollider>() == null) yield return null;

            GameObject.Find("UserInterface/QuickMenu/UserInteractMenu").GetComponent<BoxCollider>().center = new Vector3(0f, 1450f, 0f);
        }

        #endregion Popup Methods
    }

    public class UshioColorApi
    {
        //Default Colors
        public static Color DefaultBackgroundColor = Color.magenta; // Used for nameplate color
        public static Color DefaultTextColor = Color.white; // Used for nameplate text and PopupUi

        //Custom Colors
        public static Color NavyBlue = new Color(0f, 0f, 0.3f);
        public static Color LightBlue = new Color(0.68f, 0.85f, 0.9f);
        public static Color LightPink = new Color(1f, 0.71f, 0.76f);
        public static Color Orange = new Color(1f, 0.65f, 0f);
        public static Color Pink = new Color(1f, 0.75f, 0.79f);
        public static Color Purple = new Color(0.57f, 0.44f, 0.86f);
        public static Color Ruby = new Color(0.61f, 0.07f, 0.12f);
        public static Color Teal = new Color(0f, 0.5f, 0.5f);

        public static Color StringToColor(string colorText, ObjectType objectType)
        {
            var mainText = colorText.ToLower();

            switch (mainText)
            {
                case "black":
                    return Color.black;

                case "blue":
                    return Color.blue;

                case "clear":
                    return Color.clear;

                case "cyan":
                    return Color.cyan;

                case "gray":
                    return Color.gray;

                case "green":
                    return Color.green;

                case "grey":
                    return Color.grey;

                case "magenta":
                    return Color.magenta;

                case "red":
                    return Color.red;

                case "white":
                    return Color.white;

                case "yellow":
                    return Color.yellow;

                case "lightblue":
                    return UshioColorApi.LightBlue;

                case "lightpink":
                    return UshioColorApi.LightPink;

                case "navyblue":
                    return NavyBlue;

                case "orange":
                    return UshioColorApi.Orange;

                case "pink":
                    return UshioColorApi.Pink;

                case "purple":
                    return UshioColorApi.Purple;

                case "ruby":
                    return UshioColorApi.Ruby;

                case "teal":
                    return UshioColorApi.Teal;

                default:
                    if (objectType == ObjectType.Plate) return UshioColorApi.DefaultBackgroundColor;
                    return UshioColorApi.DefaultTextColor;
            }
        }

        public enum ObjectType
        {
            Plate,
            Text
        }
    }

    #region Menu Buttons

    public class UshioMainSocialButton
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HorizontalPosition">Position of the button from left to right.</param>
        /// <param name="VerticlePosition">Position of the button from top to bottom.</param>
        /// <param name="ButtonText">Text that's displayed on the button.</param>
        /// <param name="TextSize">Amount to increase or decrease the text size. (Start at zero then adjust after testing)</param>
        /// <param name="ButtonColor">Color of the button itself.</param>
        /// <param name="Listener">The method your button will perform. (Use delegate)</param>
        public UshioMainSocialButton(float HorizontalPosition, float VerticlePosition, string ButtonText, int TextSize, Color ButtonColor, System.Action Listener)
        {
            InitializeMainSocialButton(HorizontalPosition, VerticlePosition, ButtonText, TextSize, ButtonColor, Listener);
        }

        public Button MenuButton { get; set; }
        public Text ButtonTextComponent { get; set; }
        public Image ButtonImage { get; set; }

        public override string ToString()
        {
            return $"Main Social Button Info:\n" +
                $"Button: {MenuButton.name}\n" +
                $"Button Text: {ButtonTextComponent.text}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HorizontalPosition">Position of the button from left to right.</param>
        /// <param name="VerticlePosition">Position of the button from top to bottom.</param>
        /// <param name="ButtonText">Text that's displayed on the button.</param>
        /// <param name="TextSize">Amount to increase or decrease the text size. (Start at zero then adjust after testing)</param>
        /// <param name="ButtonColor">Color of the button itself.</param>
        /// <param name="Listener">The method your button will perform. (Use delegate)</param>
        /// <returns></returns>
        private void InitializeMainSocialButton(float HorizontalPosition, float VerticlePosition, string ButtonText, int TextSize, Color ButtonColor, System.Action Listener)
        {
            Transform transform = GameObject.Find("Screens").transform.Find("Social");
            GameObject gameObject = GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/User Panel/Moderator/Actions/Warn").gameObject;
            Button component = UnityEngine.Object.Instantiate(gameObject, gameObject.transform, worldPositionStays: true).GetComponent<Button>();
            component.transform.localPosition = new Vector3(component.transform.localPosition.x - 275f + HorizontalPosition, component.transform.localPosition.y - 50f + VerticlePosition, component.transform.localPosition.z);
            component.GetComponentInChildren<Text>().text = ButtonText;
            component.GetComponentInChildren<Text>().fontSize = 25 + TextSize;
            component.GetComponentInChildren<Text>().lineSpacing = 0.69f;
            component.GetComponentInChildren<Text>().supportRichText = true;
            component.onClick = new Button.ButtonClickedEvent();
            component.enabled = true;
            component.gameObject.SetActive(value: true);
            component.GetComponentInChildren<Image>().color = Color.magenta;
            component.GetComponent<RectTransform>().sizeDelta += new Vector2(25f, 0f);
            component.GetComponent<RectTransform>().sizeDelta -= new Vector2(0f, 0f);
            component.onClick.AddListener(Listener);
            component.transform.SetParent(transform.transform);

            component.gameObject.name = $"UshioUi|{UshioMenuApi.ModName}SocialButton";

            component.colors = new ColorBlock
            {
                colorMultiplier = 1f,
                disabledColor = Color.grey,
                highlightedColor = ButtonColor * 1.5f,
                normalColor = ButtonColor / 1.5f,
                pressedColor = Color.grey * 1.5f
            };

            MenuButton = component;
            ButtonTextComponent = component.GetComponentInChildren<Text>();
            ButtonImage = component.GetComponentInChildren<Image>();
        }
    }

    public class UshioSocialButton
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HorizontalPosition">Position of the button from left to right.</param>
        /// <param name="VerticalPosition">Position of the button from top to bottom</param>
        /// <param name="ButtonText">Text that's displayed on the button.</param>
        /// <param name="TextSize">Amount to increase or decrease the text size. (Start at zero then adjust after testing)</param>
        /// <param name="ButtonColor">Color of the button itself</param>
        /// <param name="Listener">The method your button will perform. (Use delegate)</param>
        /// <param name="isActive">If the button is active when you first load the social menu. (Set to true)</param>
        public UshioSocialButton(float HorizontalPosition, float VerticalPosition, string ButtonText, int TextSize, Color ButtonColor, System.Action Listener, bool isActive = true)
        {
            InitializeSocialButton(HorizontalPosition, VerticalPosition, ButtonText, TextSize, ButtonColor, Listener, isActive);
        }
        public Button MenuButton { get; set; }
        public Text ButtonTextComponent { get; set; }
        public Image ButtonImage { get; set; }

        public override string ToString()
        {
            return $"Social Button Info:\n" +
                $"Button: {MenuButton.name}\n" +
                $"Button Text: {ButtonTextComponent.text}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HorizontalPosition">Position of the button from left to right.</param>
        /// <param name="VerticalPostition">Position of the button from top to bottom</param>
        /// <param name="ButtonText">Text that's displayed on the button.</param>
        /// <param name="TextSize">Amount to increase or decrease the text size. (Start at zero then adjust after testing)</param>
        /// <param name="ButtonColor">Color of the button itself</param>
        /// <param name="Listener">The method your button will perform. (Use delegate)</param>
        /// <param name="isActive">If the button is active when you first load the social menu. (Set to true)</param>
        /// <returns></returns>
        private void InitializeSocialButton(float HorizontalPosition, float VerticalPostition, string ButtonText, int TextSize, Color ButtonColor, System.Action Listener, bool isActive = false)
        {
            Transform transform = GameObject.Find("Screens").transform.Find("UserInfo");
            GameObject gameObject = GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/User Panel/Moderator/Actions/Warn").gameObject;
            Button component = UnityEngine.Object.Instantiate(gameObject, gameObject.transform, worldPositionStays: true).GetComponent<Button>();
            component.transform.localPosition = new Vector3(component.transform.localPosition.x - 275f + HorizontalPosition, component.transform.localPosition.y - 50f + VerticalPostition, component.transform.localPosition.z);
            component.GetComponentInChildren<Text>().text = ButtonText;
            component.GetComponentInChildren<Text>().fontSize = 25 + TextSize;
            component.GetComponentInChildren<Text>().lineSpacing = 0.69f;
            component.GetComponentInChildren<Text>().supportRichText = true;
            component.onClick = new Button.ButtonClickedEvent();
            component.enabled = true;
            component.gameObject.SetActive(value: true);
            component.GetComponentInChildren<Image>().color = Color.white;
            component.GetComponent<RectTransform>().sizeDelta += new Vector2(25f, 0f);
            component.GetComponent<RectTransform>().sizeDelta -= new Vector2(0f, 0f);
            component.onClick.AddListener(Listener);

            component.GetComponentInChildren<Button>().interactable = isActive;
            component.GetComponentInChildren<Text>().enabled = isActive;
            component.GetComponentInChildren<Image>().enabled = isActive;
            component.transform.SetParent(transform.transform);

            component.gameObject.name = $"UshioUi|{UshioMenuApi.ModName}SocialButton";

            component.colors = new ColorBlock
            {
                colorMultiplier = 1f,
                disabledColor = Color.grey,
                highlightedColor = ButtonColor * 1.5f,
                normalColor = ButtonColor / 1.5f,
                pressedColor = Color.grey * 1.5f
            };

            MenuButton = component;
            ButtonTextComponent = component.GetComponentInChildren<Text>();
            ButtonImage = component.GetComponentInChildren<Image>();
        }

        public void ToggleButton(bool isEnabled)
        {
            MenuButton.enabled = isEnabled;
            ButtonTextComponent.enabled = isEnabled;
            ButtonImage.enabled = isEnabled;
        }
    }

    public class UshioMainWorldsButton
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HorizontalPosition">Position of button from left to right.</param>
        /// <param name="VerticalPostition">Position of button from top to bottom.</param>
        /// <param name="ButtonText">Text that's displayed on the button.</param>
        /// <param name="ButtonWidth">Width of the button. (Start at 0 then adjust after testing)</param>
        /// <param name="ButtonHeight">Height of the button. (Start at 0 then adjust after testing)</param>
        /// <param name="TextSize">Amount to increase or decrease the text size. (Start at zero then adjust after testing)</param>
        /// <param name="ButtonColor">Color of the button itself.</param>
        /// <param name="Listener">The method your button will perform. (Use delegate)</param>
        /// <param name="isActive">If the button is active when you first load the Worlds Button. (Set to true)</param>
        public UshioMainWorldsButton(float HorizontalPosition, float VerticalPostition, string ButtonText, float ButtonWidth, float ButtonHeight, int TextSize, Color ButtonColor, System.Action Listener, bool isActive = true)
        {
            InitializeMainWorldsButton(HorizontalPosition, VerticalPostition, ButtonText, ButtonWidth, ButtonHeight, TextSize, ButtonColor, Listener, isActive);
        }
        public Button MenuButton { get; set; }
        public Text ButtonTextComponent { get; set; }
        public Image ButtonImage { get; set; }

        public override string ToString()
        {
            return $"Main Worlds Button Info:\n" +
                $"Button: {MenuButton.name}\n" +
                $"Button Text: {ButtonTextComponent.text}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HorizontalPosition">Position of button from left to right.</param>
        /// <param name="VerticalPostition">Position of button from top to bottom.</param>
        /// <param name="ButtonText">Text that's displayed on the button.</param>
        /// <param name="ButtonWidth">Width of the button. (Start at 0 then adjust after testing)</param>
        /// <param name="ButtonHeight">Height of the button. (Start at 0 then adjust after testing)</param>
        /// <param name="TextSize">Amount to increase or decrease the text size. (Start at zero then adjust after testing)</param>
        /// <param name="ButtonColor">Color of the button itself.</param>
        /// <param name="Listener">The method your button will perform. (Use delegate)</param>
        /// <param name="isActive">If the button is active when you first load the Worlds Button. (Set to true)</param>
        /// <returns></returns>
        private void InitializeMainWorldsButton(float HorizontalPosition, float VerticalPostition, string ButtonText, float ButtonWidth, float ButtonHeight, int TextSize, Color ButtonColor, System.Action Listener, bool isActive = false)
        {
            Transform transform = GameObject.Find("Screens").transform.Find("Worlds");
            GameObject gameObject = GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/User Panel/Moderator/Actions/Warn").gameObject;
            Button component = UnityEngine.Object.Instantiate(gameObject, gameObject.transform, worldPositionStays: true).GetComponent<Button>();
            component.transform.localPosition = new Vector3(component.transform.localPosition.x - 275f + HorizontalPosition, component.transform.localPosition.y - 50f + VerticalPostition, component.transform.localPosition.z);
            component.GetComponentInChildren<Text>().text = ButtonText;
            component.GetComponentInChildren<Text>().fontSize = 25 + TextSize;
            component.GetComponentInChildren<Text>().lineSpacing = 0.69f;
            component.GetComponentInChildren<Text>().supportRichText = true;
            component.onClick = new Button.ButtonClickedEvent();
            component.enabled = true;
            component.gameObject.SetActive(value: true);
            component.GetComponentInChildren<Image>().color = Color.white;
            component.GetComponent<RectTransform>().sizeDelta += new Vector2(25f + ButtonWidth, 0f);
            component.GetComponent<RectTransform>().sizeDelta -= new Vector2(0f, 10f + ButtonHeight);
            component.onClick.AddListener(Listener);
            component.GetComponentInChildren<Text>().enabled = isActive;
            component.GetComponentInChildren<Image>().enabled = isActive;
            component.GetComponentInChildren<Button>().interactable = isActive;
            component.transform.SetParent(transform.transform);

            component.gameObject.name = $"UshioUi|{UshioMenuApi.ModName}WorldButton";

            component.colors = new ColorBlock
            {
                colorMultiplier = 1f,
                disabledColor = Color.grey,
                highlightedColor = ButtonColor * 1.5f,
                normalColor = ButtonColor / 1.5f,
                pressedColor = Color.grey * 1.5f
            };

            MenuButton = component;
            ButtonTextComponent = component.GetComponentInChildren<Text>();
            ButtonImage = component.GetComponentInChildren<Image>();
        }
    }

    public class UshioMainAvatarButton
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HorizontalPosition">Position of the button from left to right.</param>
        /// <param name="VerticalPostition">Position of the button from top to bottom.</param>
        /// <param name="ButtonText">Text that's displayed on the button.</param>
        /// <param name="TextSize">Amount to increase or decrease the text size. (Start at zero then adjust after testing)</param>
        /// <param name="ButtonColor">Color of the button itself.</param>
        /// <param name="Listener">The method your button will perform. (Use delegate)</param>
        public UshioMainAvatarButton(float HorizontalPosition, float VerticalPostition, string ButtonText, int TextSize, Color ButtonColor, System.Action Listener)
        {
            InitializeMainAvatarsButton(HorizontalPosition, VerticalPostition, ButtonText, TextSize, ButtonColor, Listener);
        }
        public Button MenuButton { get; set; }
        public Text ButtonTextComponent { get; set; }
        public Image ButtonImage { get; set; }

        public override string ToString()
        {
            return $"Main Avatars Button Info:\n" +
                $"Button: {MenuButton.name}\n" +
                $"Button Text: {ButtonTextComponent.text}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HorizontalPosition">Position of the button from left to right.</param>
        /// <param name="VerticalPostition">Position of the button from top to bottom.</param>
        /// <param name="ButtonText">Text that's displayed on the button.</param>
        /// <param name="TextSize">Amount to increase or decrease the text size. (Start at zero then adjust after testing)</param>
        /// <param name="ButtonColor">Color of the button itself.</param>
        /// <param name="Listener">The method your button will perform. (Use delegate)</param>
        /// <returns></returns>
        private void InitializeMainAvatarsButton(float HorizontalPosition, float VerticalPostition, string ButtonText, int TextSize, Color ButtonColor, System.Action Listener)
        {
            Transform transform = GameObject.Find("Screens").transform.Find("Avatar");
            GameObject gameObject = GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/User Panel/Moderator/Actions/Warn").gameObject;
            Button component = UnityEngine.Object.Instantiate(gameObject, gameObject.transform, worldPositionStays: true).GetComponent<Button>();
            component.transform.localPosition = new Vector3(component.transform.localPosition.x - 275f + HorizontalPosition, component.transform.localPosition.y - 50f + VerticalPostition, component.transform.localPosition.z);
            component.GetComponentInChildren<Text>().text = ButtonText;
            component.GetComponentInChildren<Text>().fontSize = 25 + TextSize;
            component.GetComponentInChildren<Text>().lineSpacing = 0.69f;
            component.GetComponentInChildren<Text>().supportRichText = true;
            component.onClick = new Button.ButtonClickedEvent();
            component.enabled = true;
            component.gameObject.SetActive(value: true);
            component.GetComponentInChildren<Image>().color = Color.magenta;
            component.GetComponent<RectTransform>().sizeDelta += new Vector2(25f, 0f);
            component.GetComponent<RectTransform>().sizeDelta -= new Vector2(0f, 0f);
            component.onClick.AddListener(Listener);
            component.transform.SetParent(transform.transform);

            component.gameObject.name = $"{UshioMenuApi.ModName}AvatarButton";

            component.colors = new ColorBlock
            {
                colorMultiplier = 1f,
                disabledColor = Color.grey,
                highlightedColor = ButtonColor * 1.5f,
                normalColor = ButtonColor / 1.5f,
                pressedColor = Color.grey * 1.5f
            };

            MenuButton = component;
            ButtonTextComponent = component.GetComponentInChildren<Text>();
            ButtonImage = component.GetComponentInChildren<Image>();
        }
    }

    #endregion Menu Buttons

    #region Other Ui

    public class UshioMenuSlider
    {
        /// <summary>
        /// Makes a slider in your menu.
        /// </summary>
        /// <param name="sliderPosition">Slider's position in the menu.</param>
        /// <param name="text">Slider text field.</param>
        /// <param name="menu">Menu transform.</param>
        /// <param name="minValue">Slider's minimum value.</param>
        /// <param name="maxValue">Slider's maximum value.</param>
        /// <param name="defaultValue">Slider's starter value.</param>
        /// <param name="listener">Action that the slider controls. (Put try catch in listener)</param>
        public UshioMenuSlider(SliderPosition sliderPosition, string text, Transform menu, float minValue, float maxValue, float defaultValue, System.Action<float> listener)
        {
            this.MinimumValue = minValue;
            this.MaximumValue = maxValue;
            this.InitializeMenuSlider(sliderPosition, text, menu, minValue, maxValue, defaultValue, listener);
        }

        /// <summary>
        /// Makes a slider in your menu.
        /// </summary>
        /// <param name="sliderPosition">Slider's position in the menu.</param>
        /// <param name="text">Slider text field.</param>
        /// <param name="menu">Menu transform.</param>
        /// <param name="minValue">Slider's minimum value.</param>
        /// <param name="maxValue">Slider's maximum value.</param>
        /// <param name="defaultValue">Slider's starter value.</param>
        /// <param name="listener">Action that the slider controls. (Put try catch in listener)</param>
        public UshioMenuSlider(SliderPosition sliderPosition, string text, QMNestedButton menu, float minValue, float maxValue, float defaultValue, System.Action<float> listener)
        {
            this.MinimumValue = minValue;
            this.MaximumValue = maxValue;
            var menuTransform = menu.getBackButton().getGameObject().transform.GetParent();
            this.InitializeMenuSlider(sliderPosition, text, menuTransform, minValue, maxValue, defaultValue, listener);
        }

        protected Slider MenuSlider { get; set; }
        protected Text SliderText { get; set; }
        protected float MinimumValue { get; set; }
        protected float MaximumValue { get; set; }

        public override string ToString()
        {
            return $"Slider Info:\n" +
                $"Menu Slider: {MenuSlider.name}\n" +
                $"Slider Text: {SliderText.text}\n" +
                $"Minimum Value: {MinimumValue}\n" +
                $"Maximum Value: {MaximumValue}";
        }

        public void SetActive(bool isEnabled)
        {
            MenuSlider.gameObject.SetActive(isEnabled);
        }

        public Slider GetSlider() { return MenuSlider; }

        public Text GetSliderText() { return SliderText; }

        public void SetSliderActive(bool setActive)
        {
            MenuSlider.gameObject.SetActive(setActive);
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            GameObject.Destroy(SliderText.gameObject);
            GameObject.Destroy(MenuSlider.gameObject);
        }

        private void InitializeMenuSlider(SliderPosition sliderPosition, string text, Transform menu, float minValue, float maxValue, float defaultValue, System.Action<float> listener)
        {
            GameObject gameObject = GameObject.Find("Screens").transform.Find("Settings/MousePanel/SensitivitySlider").gameObject;
            GameObject textGameObject = GameObject.Find("Screens").transform.Find("Settings/MousePanel/MouseSensitivityText").gameObject;

            Slider component = UnityEngine.Object.Instantiate(gameObject, menu, worldPositionStays: true).GetComponent<Slider>();

            component.name = $"UshioUi|{UshioMenuApi.ModName}|{text}";
            component.onValueChanged = new Slider.SliderEvent();
            component.onValueChanged.RemoveAllListeners();
            component.onValueChanged.AddListener(listener);
            component.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            component.minValue = minValue;
            component.maxValue = maxValue;
            component.value = defaultValue;

            component.transform.localRotation = Quaternion.EulerAngles(new Vector3(0, 0, 0));

            SliderText = UnityEngine.GameObject.Instantiate(textGameObject, component.transform, worldPositionStays: true).GetComponent<Text>();
            SliderText.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            SliderText.text = text;
            SliderText.transform.localPosition = new Vector3(20, 69, 0);
            SliderText.transform.localRotation = Quaternion.EulerAngles(new Vector3(0, 0, 0));

            switch (sliderPosition)
            {
                //Bottom Positions
                case SliderPosition.BottomLeft:
                    component.transform.localPosition = new Vector3(-410, 180, 0);
                    break;

                case SliderPosition.BottomRight:
                    component.transform.localPosition = new Vector3(410, 180, 0);
                    break;

                case SliderPosition.BottomCenter:
                    component.transform.localPosition = new Vector3(0, 180, 0);
                    break;

                //Center Positions
                case SliderPosition.CenterLeft:
                    component.transform.localPosition = new Vector3(-410, 600, 0);
                    break;

                case SliderPosition.CenterRight:
                    component.transform.localPosition = new Vector3(410, 600, 0);
                    break;

                case SliderPosition.Center:
                    component.transform.localPosition = new Vector3(0, 600, 0);
                    break;

                //Top Positions
                case SliderPosition.TopLeft:
                    component.transform.localPosition = new Vector3(-410, 1020, 0);
                    break;

                case SliderPosition.TopRight:
                    component.transform.localPosition = new Vector3(410, 1020, 0);
                    break;

                case SliderPosition.TopCenter:
                    component.transform.localPosition = new Vector3(0, 1020, 0);
                    break;

                //Side Positions
                case SliderPosition.RightofMenu:
                    component.transform.localPosition = new Vector3(1000, 850, 0);
                    component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));

                    SliderText.transform.localPosition = new Vector3(20, -69, 0);
                    SliderText.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    break;

                case SliderPosition.LeftofMenu:
                    if (component.transform.GetParent().name == "ShortcutMenu")
                    {
                        component.transform.localPosition = new Vector3(-1000, 1250, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }

                    else
                    {
                        component.transform.localPosition = new Vector3(-1000, 850, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    break;

                //Vertical Top Positions
                case SliderPosition.VerticalTopFirst:
                    if (component.transform.GetParent().name == "ShortcutMenu")
                    {
                        component.transform.localPosition = new Vector3(-600, 1250, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }

                    else
                    {
                        component.transform.localPosition = new Vector3(-600, 850, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    break;

                case SliderPosition.VerticalTopSecond:
                    if (component.transform.GetParent().name == "ShortcutMenu")
                    {
                        component.transform.localPosition = new Vector3(-200, 1250, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }

                    else
                    {
                        component.transform.localPosition = new Vector3(-200, 850, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    break;

                case SliderPosition.VerticalTopThird:
                    if (component.transform.GetParent().name == "ShortcutMenu")
                    {
                        component.transform.localPosition = new Vector3(200, 1250, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }

                    else
                    {
                        component.transform.localPosition = new Vector3(200, 850, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    break;

                case SliderPosition.VerticalTopFourth:
                    if (component.transform.GetParent().name == "ShortcutMenu")
                    {
                        component.transform.localPosition = new Vector3(600, 1250, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }

                    else
                    {
                        component.transform.localPosition = new Vector3(600, 850, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    break;

                //Vertical Bottom Position
                case SliderPosition.VerticalBottomFirst:
                    if (component.transform.GetParent().name == "ShortcutMenu")
                    {
                        component.transform.localPosition = new Vector3(-600, 850, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }

                    else
                    {
                        component.transform.localPosition = new Vector3(-600, 450, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    break;

                case SliderPosition.VerticalBottomSecond:
                    if (component.transform.GetParent().name == "ShortcutMenu")
                    {
                        component.transform.localPosition = new Vector3(-200, 850, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }

                    else
                    {
                        component.transform.localPosition = new Vector3(-200, 450, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    break;

                case SliderPosition.VerticalBottomThird:
                    if (component.transform.GetParent().name == "ShortcutMenu")
                    {
                        component.transform.localPosition = new Vector3(200, 850, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }

                    else
                    {
                        component.transform.localPosition = new Vector3(200, 450, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    break;

                case SliderPosition.VerticalBottomFourth:
                    if (component.transform.GetParent().name == "ShortcutMenu")
                    {
                        component.transform.localPosition = new Vector3(600, 850, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }

                    else
                    {
                        component.transform.localPosition = new Vector3(600, 450, 0);
                        component.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    break;
            }

            component.GetComponent<RectTransform>().sizeDelta *= new Vector2(1.1f, 1.1f);

            MenuSlider = component;
        }

        public enum SliderPosition
        {
            BottomLeft,
            BottomRight,
            BottomCenter,
            CenterLeft,
            CenterRight,
            Center,
            TopLeft,
            TopRight,
            TopCenter,
            RightofMenu,
            LeftofMenu,
            VerticalTopFirst,
            VerticalTopSecond,
            VerticalTopThird,
            VerticalTopFourth,
            VerticalBottomFirst,
            VerticalBottomSecond,
            VerticalBottomThird,
            VerticalBottomFourth
        }
    }

    [RegisterTypeInIl2Cpp]
    public class UshioDisplayUi : MonoBehaviour
    {
        public UshioDisplayUi(IntPtr ptr) : base(ptr) { }

        private static Vector3 RightOfMenu = new Vector3(2010, 1120, 0);
        private static Vector3 RightOfMenuAngled = new Vector3(1875, 1125, -440);
        private static Vector3 LeftOfmenu = new Vector3(-2010, 1120, 0);
        private static Vector3 LeftOfMenuAngled = new Vector3(-1875, 1125, -440);
        public GameObject Display;
        public Image Background;
        public Text Clock;
        public Text Title;
        public Image Logo;
        public Image UiImage;
        public Text MiniText;
        public Text MainText;
        public Text SecondaryText;
        public static string CurrentTime
        {
            [HideFromIl2Cpp]
            get
            {
                string trueTime;
                string AMPM;

                var currentTime = DateTime.Now.TimeOfDay;

                var currentHour = currentTime.Hours;
                var currentMinutes = currentTime.Minutes;

                string trueHour;
                string trueMinute;

                if (currentHour > 12)
                {
                    trueHour = (currentHour - 12).ToString();
                    AMPM = "PM";
                }

                else if (currentHour == 0)
                {
                    trueHour = 12.ToString();
                    AMPM = "AM";
                }

                else
                {
                    trueHour = currentHour.ToString();
                    AMPM = "AM";
                }

                if (currentMinutes == 0 || currentMinutes == 1 || currentMinutes == 2 || currentMinutes == 3 || currentMinutes == 4 || currentMinutes == 5 || currentMinutes == 6 || currentMinutes == 7 || currentMinutes == 8 || currentMinutes == 9)
                {
                    trueMinute = "0" + currentMinutes.ToString();
                }

                else
                {
                    trueMinute = currentMinutes.ToString();
                }

                trueTime = trueHour + ":" + trueMinute + " " + AMPM;

                return trueTime;
            }
        }

        void Update()
        {
            var playersInRoomArray = GetPlayersInRoom();
            SetText(playersInRoomArray[0], UshioDisplayUi.TextType.MainText);
            SetText((VFc5a1NXNW1idz09.changesAvailable ? VFc5a1NXNW1idz09.modInfo.ChangesToString() : "") + playersInRoomArray[1], UshioDisplayUi.TextType.SecondaryText);
            SetText(GetWorldInfo(), UshioDisplayUi.TextType.MiniText);
            UpdateClocks();
        }

        [Obsolete]
        [HideFromIl2Cpp]
        public static UshioDisplayUi CreateDisplay(string title, Sprite logo, RuntimeAnimatorController uiImageAnimator, string miniText, string mainText, string secondaryText, Color backgroundColor, Color textColor, DisplayPosition displayPosition, Color clockColor, bool useClock = true)
        {
            var uiPrefab = QXNzZXRCdW5kbGVz.UshioUiAssetBundle.LoadAsset("Assets/UshioUi/UshioUiDisplay.prefab").Cast<GameObject>();
            var obj = GameObject.Instantiate(uiPrefab, UshioMenuApi.ShortcutMenu).AddComponent<UshioDisplayUi>();
            try
            {

                obj.Display = obj.gameObject;
                obj.Clock = obj.transform.FindChild("UshioUiClock").GetComponent<Text>();
                obj.Title = obj.transform.FindChild("UshioUiTitle").GetComponent<Text>();
                obj.Logo = obj.transform.FindChild("UshioUiLogo").GetComponent<Image>();
                obj.Background = obj.transform.FindChild("UshioUiBackground").GetComponent<Image>();
                obj.UiImage = obj.transform.FindChild("UshioUiImage").GetComponent<Image>();
                obj.MiniText = obj.transform.FindChild("UshioUiMiniText").GetComponent<Text>();
                obj.MainText = obj.transform.FindChild("UshioUiMainText").GetComponent<Text>();
                obj.SecondaryText = obj.transform.FindChild("UshioUiSecondaryText").GetComponent<Text>();
            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog($"Something fucked up setting variebles...\n{ex}");
            }

            try
            {

                obj.Title.text = title;
                obj.Logo.sprite = logo;
                obj.UiImage.gameObject.AddComponent<Animator>().runtimeAnimatorController = uiImageAnimator;
                obj.MiniText.text = miniText;
                obj.MainText.text = mainText;
                obj.SecondaryText.text = secondaryText;

                obj.Clock.color = clockColor;
                obj.Title.color = textColor;
                obj.MiniText.color = textColor;
                obj.MainText.color = textColor;
                obj.SecondaryText.color = textColor;

                obj.Background.color = backgroundColor;


                if (!useClock)
                {
                    obj.Clock.enabled = false;
                }
            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog($"Something fucked up with setting variables...\n{ex}");
            }

            obj.Display.transform.localScale = new Vector3(5.15f, 5.15f, 5.15f);

            switch (displayPosition)
            {
                case DisplayPosition.RightOfMenu:
                    obj.transform.localPosition = RightOfMenu;
                    obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case DisplayPosition.LeftOfMenu:
                    obj.transform.localPosition = LeftOfmenu;
                    obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case DisplayPosition.RightOfMenuAngled:
                    obj.transform.localPosition = RightOfMenuAngled;
                    obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 35, 0));
                    break;
                case DisplayPosition.LeftOfMenuAngled:
                    obj.transform.localPosition = LeftOfMenuAngled;
                    obj.transform.localRotation = Quaternion.Euler(new Vector3(0, -35, 0));
                    break;
            }

            return obj;
        }

        [HideFromIl2Cpp]
        public static UshioDisplayUi CreateDisplay(string title, Sprite logo, Sprite uiImage, string miniText, string mainText, string secondaryText, Color backgroundColor, Color textColor, DisplayPosition displayPosition, Color clockColor, bool useClock = true)
        {
            //var prefab = assetBundle.LoadAsset_Internal("Assets/UshioUi/UshioUiDisplay.prefab", Il2CppType.Of<GameObject>()).Cast<GameObject>();
            var uiPrefab = QXNzZXRCdW5kbGVz.UshioUiAssetBundle.LoadAsset("Assets/UshioUi/UshioUiDisplay.prefab").Cast<GameObject>();
            var obj = GameObject.Instantiate(uiPrefab, UshioMenuApi.ShortcutMenu).AddComponent<UshioDisplayUi>();

            try
            {
                obj.Display = obj.gameObject;
                obj.Clock = obj.transform.FindChild("UshioUiClock").GetComponent<Text>();
                obj.Title = obj.transform.FindChild("UshioUiTitle").GetComponent<Text>();
                obj.Logo = obj.transform.FindChild("UshioUiLogo").GetComponent<Image>();
                obj.Background = obj.transform.FindChild("UshioUiBackground").GetComponent<Image>();
                obj.UiImage = obj.transform.FindChild("UshioUiImage").GetComponent<Image>();
                obj.MiniText = obj.transform.FindChild("UshioUiMiniText").GetComponent<Text>();
                obj.MainText = obj.transform.FindChild("UshioUiMainText").GetComponent<Text>();
                obj.SecondaryText = obj.transform.FindChild("UshioUiSecondaryText").GetComponent<Text>();
            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog($"Something fucked up setting variebles...\n{ex}");
            }

            try
            {
                obj.Title.text = title;
                obj.Logo.sprite = logo;
                obj.UiImage.sprite = uiImage;
                obj.MiniText.text = miniText;
                obj.MainText.text = mainText;
                obj.SecondaryText.text = secondaryText;

                obj.Clock.color = clockColor;
                obj.Title.color = textColor;
                obj.MiniText.color = textColor;
                obj.MainText.color = textColor;
                obj.SecondaryText.color = textColor;

                obj.Background.color = backgroundColor;

                if (!useClock)
                {
                    obj.Clock.enabled = false;
                }
            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog($"Something fucked up with setting variables...\n{ex}");
            }

            obj.Display.transform.localScale = new Vector3(5.15f, 5.15f, 5.15f);

            switch (displayPosition)
            {
                case DisplayPosition.RightOfMenu:
                    obj.transform.localPosition = RightOfMenu;
                    obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case DisplayPosition.LeftOfMenu:
                    obj.transform.localPosition = LeftOfmenu;
                    obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case DisplayPosition.RightOfMenuAngled:
                    obj.transform.localPosition = RightOfMenuAngled;
                    obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 35, 0));
                    break;
                case DisplayPosition.LeftOfMenuAngled:
                    obj.transform.localPosition = LeftOfMenuAngled;
                    obj.transform.localRotation = Quaternion.Euler(new Vector3(0, -35, 0));
                    break;
            }

            return obj;
        }

        [HideFromIl2Cpp]
        public GameObject GetDisplay()
        {
            return Display;
        }

        [HideFromIl2Cpp]
        public void SetText(string text, TextType type)
        {
            if (type == TextType.MainText) this.MainText.text = text;
            else if (type == TextType.SecondaryText) this.SecondaryText.text = text;
            else if (type == TextType.MiniText) this.MiniText.text = text;
        }

        [HideFromIl2Cpp]
        public void SetText(string mainText, string secondaryText, string miniText)
        {
            this.MainText.text = mainText;
            this.SecondaryText.text = secondaryText;
            this.MiniText.text = miniText;
        }

        [HideFromIl2Cpp]
        public void SetImage(Sprite image, ImageType type)
        {
            if (type == ImageType.Logo) this.Logo.sprite = image;
            else if (type == ImageType.UiImage) this.UiImage.sprite = image;
        }

        [HideFromIl2Cpp]
        public void SetDisplayActive(bool active)
        {
            this.Background.enabled = active;
            this.Title.enabled = active;
            this.Logo.enabled = active;
            this.UiImage.enabled = active;
            this.MiniText.enabled = active;
            this.MainText.enabled = active;
            this.SecondaryText.enabled = active;
            this.Clock.enabled = active;
        }

        [HideFromIl2Cpp]
        public void UpdateClocks()
        {
            if (Clock.enabled)
            {
                Clock.text = CurrentTime;
            }
        }

        [HideFromIl2Cpp]
        public void SetDisplayPosition(Vector3 position, Vector3 rotation)
        {
            Display.transform.localPosition += position;
            Display.transform.localRotation = Quaternion.Euler(rotation);
        }

        [HideFromIl2Cpp]
        public void SetDisplayPosition(DisplayPosition position, Vector3 rotation)
        {
            if (position == DisplayPosition.RightOfMenu) Display.transform.localPosition = new Vector3(1835, 1130, 0);
            else if (position == DisplayPosition.LeftOfMenu) Display.transform.localPosition = new Vector3(-1835, 1130, 0);
            Display.transform.localRotation = Quaternion.Euler(rotation);
        }

        [HideFromIl2Cpp]
        public static string[] GetPlayersInRoom()
        {
            var playerList = PlayerManager.field_Private_Static_PlayerManager_0.prop_ArrayOf_Player_0;
            string friendsInRoom = "<color=cyan>Friends In Room:</color>\nYou have no friends...";
            string[] playersInRoom = { "<color=cyan>Friends In Room:</color>\nNone", "<color=cyan>Ignored In Room:</color>\nNone" };
            var firstFriend = true;
            var firstIgnored = true;

            for (int i = 0; i < playerList.Length; i++)
            {
                var player = playerList[i];
                var apiUser = player.prop_APIUser_0;
                bool isFriend = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.IsFriendsWith(apiUser.id);
                bool isMaster = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.MasterCheck(apiUser.id);

                if (MRConfiguration.config.ignoreList.TryGetValue(apiUser.id, out string displayName))
                {
                    if (displayName != apiUser.displayName)
                    {
                        displayName = apiUser.displayName;
                        MRConfiguration.config.WriteConfig();
                    }
                    if (firstIgnored)
                    {
                        playersInRoom[1] = "<color=cyan>Ignored In Room:</color>";
                        firstIgnored = false;
                    }

                    playersInRoom[1] += $"\n{displayName}";
                    if (isMaster) playersInRoom[1] += "[<color=red>RM</color>]";
                }

                if (isFriend)
                {
                    if (firstFriend)
                    {
                        playersInRoom[0] = "<color=cyan>Friends In Room:</color>";
                        firstFriend = false;
                    }

                    playersInRoom[0] += $"\n{apiUser.displayName}";
                    if (isMaster) friendsInRoom += " [<color=red>RM</color>]";
                }

            }

            return playersInRoom;
        }

        [HideFromIl2Cpp]
        internal static string GetWorldInfo()
        {
            try
            {
                var playerManager = PlayerManager.field_Private_Static_PlayerManager_0.prop_ArrayOf_Player_0;
                var worldApi = RoomManager.field_Internal_Static_ApiWorldInstance_0;

                string roomMaster = "";

                for (int i = 0; i < playerManager.Length; i++)
                {
                    var player = playerManager[i];
                    var apiUser = player.prop_APIUser_0;
                    bool isMaster = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.MasterCheck(apiUser.id);

                    if (!isMaster) continue;

                    roomMaster = apiUser.displayName;
                }


                string worldInfo;
                worldInfo = $"<color=cyan>Player Count:</color> {playerManager.Length}\n" +
                    $"<color=cyan>World Name:\n</color>{worldApi.world.name}\n" +
                    $"<color=cyan>Room Master:</color>\n{roomMaster}";

                return worldInfo;
            }

            catch
            {
                return "N/A";
            }
        }

        public enum DisplayPosition
        {
            RightOfMenu,
            RightOfMenuAngled,
            LeftOfMenu,
            LeftOfMenuAngled
        }

        public enum TextType
        {
            MainText,
            SecondaryText,
            MiniText
        }

        public enum ImageType
        {
            Logo,
            UiImage
        }
    }

    #endregion Other Ui
}