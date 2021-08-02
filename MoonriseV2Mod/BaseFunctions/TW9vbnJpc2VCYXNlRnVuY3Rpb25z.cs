using MelonLoader;
using MoonriseTabApi;
using MoonriseV2Mod.API;
using MoonriseV2Mod.Patches;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;
using UnhollowerRuntimeLib;
using UnityEngine;
using UshioUI;
using static MoonriseV2Mod.BaseFunctions.RW1vamlTcGFt;

namespace MoonriseV2Mod.BaseFunctions
{
    internal sealed class TW9vbnJpc2VCYXNlRnVuY3Rpb25z : MoonriseObject
    {
        public static TW9vbnJpc2VCYXNlRnVuY3Rpb25z baseFunctions;

        public TW9vbnJpc2VCYXNlRnVuY3Rpb25z()
        {
            QuickMenuPatches.loadMenu += LoadMenu;
        }

        private Sprite MoonriseIcon;
        public MenuTab menuTab;
        public UshioDisplayUi display;
        private Sprite MoonriseLogo;
        private Sprite DisplayUiBanner;

        public static void Initialize()
        {
            VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09.Initialize();
            baseFunctions = new TW9vbnJpc2VCYXNlRnVuY3Rpb25z();

            baseFunctions.MoonriseIcon = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/Sprites/MoonriseIcon.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            baseFunctions.MoonriseIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            baseFunctions.MoonriseLogo = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/Sprites/MoonriseLogo.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            baseFunctions.MoonriseLogo.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            baseFunctions.DisplayUiBanner = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/Sprites/Banner.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            baseFunctions.DisplayUiBanner.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            baseFunctions.isInitialized = true;
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socielInterraction, TVJVc2Vy user)
        {
            // Moonrise Ui Display
            display = UshioDisplayUi.CreateDisplay("Moonrise", MoonriseLogo, DisplayUiBanner, "This is pretty hot!", "This should be empty", "This is empty too", new Color(0.1f, 0.1f, 0.1f, 0.8f), Color.white, UshioDisplayUi.DisplayPosition.LeftOfMenuAngled, Color.cyan, true);
            display.SetDisplayActive(MRConfiguration.config.UiDisplayEnabled);
            display.MainText.fontStyle = FontStyle.Normal;
            display.SecondaryText.fontStyle = FontStyle.Normal;
            display.MiniText.fontStyle = FontStyle.Normal;

            var quickMenuCollider = GameObject.Find("UserInterface/QuickMenu").GetComponent<BoxCollider>();
            quickMenuCollider.size = new Vector3(2517.34f, 2511.213f, 1);
            menuTab = TabApi.MakeTabButton(functions, MoonriseIcon, MenuTab.MenuSide.Bottom, 1, "Update!", Color.blue);

            //Mute All
            var muteAllButton = new QMToggleButton(functions, 1, 0, "Mute All", delegate
            {
                VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09.muteAll = true;
            }, "Disabled", delegate
            {
                VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09.muteAll = false;
                VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09.UnmutePlayers();
            }, "Mutes all players in the world except friends", null, null, false, false);

            //Mute Friends
            var muteFriendsTransform = new QMToggleButton(functions, 2, 0, "Mute Friends", delegate
            {
                VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09.muteFriends = true;
            }, "Disabled", delegate
            {
                VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09.muteFriends = false;
                VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09.UnmutePlayers();
            }, "Mutes all friends in the world", null, null, false, false);

            if (user == null) return;

            if (user.Premium)
            {
                RW1vamlTcGFt.emoji = 17;
                RW1vamlTcGFt.maxTime = true;

                var emojiSpam = new QMToggleButton(functions, 5, 1, $"Emoji Spam\n{(Emoji)emoji}", delegate
                {
                    RW1vamlTcGFt.emojiSpam = true;
                    MelonCoroutines.Start(RW1vamlTcGFt.SpamEmojis());
                }, "Disabled", delegate
                {
                    RW1vamlTcGFt.emojiSpam = false;
                }, $"Spams {(Emoji)emoji} emoji every {spamInterval} seconds");
                VXNoaW9SdWJ5TW9kaWZpZXJz.MoveHalf(emojiSpam, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Bottom);

                var timeSpanToggle = new QMToggleButton(functions, 5, 0, $"10 Seconds", delegate
                {
                    RW1vamlTcGFt.maxTime = true;
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every {RW1vamlTcGFt.spamInterval} seconds");
                }, "5 Seconds", delegate
                {
                    RW1vamlTcGFt.maxTime = false;
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every {RW1vamlTcGFt.spamInterval} seconds");
                }, "Emoji spam interval", null, null, false, RW1vamlTcGFt.maxTime);

                var cycleEmojisUp = new QMSingleButton(functions, 5, 1, "", delegate
                {
                    if (RW1vamlTcGFt.emoji >= 57)
                    {
                        RW1vamlTcGFt.emoji = 0;
                    }

                    else
                    {
                        RW1vamlTcGFt.emoji++;
                    }
                    if (RW1vamlTcGFt.emoji == 39) RW1vamlTcGFt.emoji++;
                    emojiSpam.setOnText($"Emoji Spam\n{(Emoji)emoji}");
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every {RW1vamlTcGFt.spamInterval} seconds");
                }, "Cycles through emojis");
                VXNoaW9SdWJ5TW9kaWZpZXJz.MakeArrowButton(cycleEmojisUp, VXNoaW9SdWJ5TW9kaWZpZXJz.ArrowDirection.Up);
                VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(cycleEmojisUp, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Top, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Verticle);

                var cycleEmojisDown = new QMSingleButton(functions, 5, 2, "", delegate
                {
                    if (RW1vamlTcGFt.emoji <= 0)
                    {
                        RW1vamlTcGFt.emoji = 57;
                    }

                    else
                    {
                        RW1vamlTcGFt.emoji--;
                    }
                    if (RW1vamlTcGFt.emoji == 39) RW1vamlTcGFt.emoji--;
                    emojiSpam.setOnText($"Emoji Spam\n{(Emoji)emoji}");
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every 5 seconds");
                }, "Cycles through emojis");
                VXNoaW9SdWJ5TW9kaWZpZXJz.MakeArrowButton(cycleEmojisDown, VXNoaW9SdWJ5TW9kaWZpZXJz.ArrowDirection.Down);
                VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(cycleEmojisDown, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Bottom, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Verticle);            
            }
        }
    }
}
