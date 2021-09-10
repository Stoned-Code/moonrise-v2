using MelonLoader;
using MoonriseTabApi;
using MoonriseV2Mod.API;
using MoonriseV2Mod.Patches;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;
using UnhollowerRuntimeLib;
using UnityEngine;
using UshioUI;
using static MoonriseV2Mod.BaseFunctions.EmojiSpam;

namespace MoonriseV2Mod.BaseFunctions
{
    internal sealed class MoonriseBaseFunctions : MoonriseMenu
    {
        public static MoonriseBaseFunctions baseFunctions;

        private Sprite MoonriseIcon;
        private Sprite MoonriseLogo;
        private Sprite DisplayUiBanner;

        public MenuTab menuTab;
        public UshioDisplayUi display;

        public Sprite m_moonriseLogo => MoonriseLogo;

        public static void Initialize()
        {
            baseFunctions = new MoonriseBaseFunctions();

            //JoinNotifierFunctions.Initialize();

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
                PlayerMutePatch.muteAll = true;
            }, "Disabled", delegate
            {
                PlayerMutePatch.muteAll = false;
                //VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09.UnmutePlayers();
            }, "Mutes all players in the world except friends", null, null, false, false);

            //Mute Friends
            var muteFriendsTransform = new QMToggleButton(functions, 2, 0, "Mute Friends", delegate
            {
                PlayerMutePatch.muteFriends = true;
            }, "Disabled", delegate
            {
                PlayerMutePatch.muteFriends = false;
                //VlVkNGFHVlhWbmxVV0ZZd1dsRTlQUT09.UnmutePlayers();
            }, "Mutes all friends in the world", null, null, false, false);

            var walkthrough = new QMToggleButton(functions, 2, 2, "Walk Through", delegate
            {
                MRConfiguration.config.walkThrough = true;
                //MRConfiguration.config.WriteConfig();
            }, "Confirmation", delegate
            {
                MRConfiguration.config.walkThrough = false;
                //MRConfiguration.config.WriteConfig();
            }, "Toggles whether a confirmation pops up.", null, null, false, MRConfiguration.config.walkThrough);
            walkthrough.setActive(MRConfiguration.config.antiPortal);

            // Anti-Portal
            var antiPortal = new QMToggleButton(functions, 1, 2, "Anti-Portal", delegate
            {
                MRConfiguration.config.antiPortal = true;
                walkthrough.setActive(true);
                //MRConfiguration.config.WriteConfig();
            }, "Disabled", delegate
            {
                MRConfiguration.config.antiPortal = false;
                walkthrough.setActive(false);
                //MRConfiguration.config.WriteConfig();
            }, "Toggles whether you go through a portal.", null, null, false, MRConfiguration.config.antiPortal);


            // Friends only toggle for "Join Notifier"
            var friendsOnlyToggle = new QMToggleButton(functions, 2, 1, "All", delegate
            {
                MRConfiguration.config.allUsersJn = true;
                //MRConfiguration.config.WriteConfig();
            }, "Friends Only", delegate
            {
                MRConfiguration.config.allUsersJn = false;
                //MRConfiguration.config.WriteConfig();
            }, "Toggles whether \"Join Notifier\" shows only friends or all users.", null, null, false, MRConfiguration.config.allUsersJn);
            friendsOnlyToggle.setActive(MRConfiguration.config.joinNotifier);

            // Join Notifier
            var joinNotifier = new QMToggleButton(functions, 1, 1, "Join Notifier", delegate
            {
                MRConfiguration.config.joinNotifier = true;
                friendsOnlyToggle.setActive(true);
                //MRConfiguration.config.WriteConfig();
            }, "Disabled", delegate
            {
                MRConfiguration.config.joinNotifier = false;
                friendsOnlyToggle.setActive(false);
                //MRConfiguration.config.WriteConfig();
            }, "Notifies you when friends join the world.", null, null, false, MRConfiguration.config.joinNotifier);

            PlayerTeleportMenu.Initialize(functions);

            if (user == null) return;

            if (user.Premium)
            {
                EmojiSpam.emoji = 17;
                EmojiSpam.maxTime = true;

                var emojiSpam = new QMToggleButton(functions, 5, 1, $"Emoji Spam\n{(Emoji)emoji}", delegate
                {
                    EmojiSpam.emojiSpam = true;
                    MelonCoroutines.Start(EmojiSpam.SpamEmojis());
                }, "Disabled", delegate
                {
                    EmojiSpam.emojiSpam = false;
                }, $"Spams {(Emoji)emoji} emoji every {spamInterval} seconds");
                VXNoaW9SdWJ5TW9kaWZpZXJz.MoveHalf(emojiSpam, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Bottom);

                var timeSpanToggle = new QMToggleButton(functions, 5, 0, $"10 Seconds", delegate
                {
                    EmojiSpam.maxTime = true;
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every {EmojiSpam.spamInterval} seconds");
                }, "5 Seconds", delegate
                {
                    EmojiSpam.maxTime = false;
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every {EmojiSpam.spamInterval} seconds");
                }, "Emoji spam interval", null, null, false, EmojiSpam.maxTime);

                var cycleEmojisUp = new QMSingleButton(functions, 5, 1, "", delegate
                {
                    if (EmojiSpam.emoji >= 57)
                    {
                        EmojiSpam.emoji = 0;
                    }

                    else
                    {
                        EmojiSpam.emoji++;
                    }
                    if (EmojiSpam.emoji == 39) EmojiSpam.emoji++;
                    emojiSpam.setOnText($"Emoji Spam\n{(Emoji)emoji}");
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every {EmojiSpam.spamInterval} seconds");
                }, "Cycles through emojis");
                VXNoaW9SdWJ5TW9kaWZpZXJz.MakeArrowButton(cycleEmojisUp, VXNoaW9SdWJ5TW9kaWZpZXJz.ArrowDirection.Up);
                VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(cycleEmojisUp, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Top, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Verticle);

                var cycleEmojisDown = new QMSingleButton(functions, 5, 2, "", delegate
                {
                    if (EmojiSpam.emoji <= 0)
                    {
                        EmojiSpam.emoji = 57;
                    }

                    else
                    {
                        EmojiSpam.emoji--;
                    }
                    if (EmojiSpam.emoji == 39) EmojiSpam.emoji--;
                    emojiSpam.setOnText($"Emoji Spam\n{(Emoji)emoji}");
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every 5 seconds");
                }, "Cycles through emojis");
                VXNoaW9SdWJ5TW9kaWZpZXJz.MakeArrowButton(cycleEmojisDown, VXNoaW9SdWJ5TW9kaWZpZXJz.ArrowDirection.Down);
                VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(cycleEmojisDown, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Bottom, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Verticle);            
            }
        }
    }
}
