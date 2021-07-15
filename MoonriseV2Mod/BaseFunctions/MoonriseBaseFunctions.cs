using MelonLoader;
using MoonriseTabApi;
using MoonriseV2Mod.API;
using RubyButtonAPI;
using UnhollowerRuntimeLib;
using UnityEngine;
using static MoonriseV2Mod.BaseFunctions.EmojiSpam;

namespace MoonriseV2Mod.BaseFunctions
{
    internal sealed class MoonriseBaseFunctions : MoonriseObject
    {
        public static MoonriseBaseFunctions baseFunctions;

        public MoonriseBaseFunctions()
        {
            Moonrise.loadMenu += LoadMenu;
        }

        private Sprite MoonriseIcon;
        public MenuTab menuTab;

        public static void Initialize()
        {
            PlayerMute.Initialize();
            baseFunctions = new MoonriseBaseFunctions();

            baseFunctions.MoonriseIcon = MoonriseAssetBundles.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/Sprites/MoonriseIcon.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            baseFunctions.MoonriseIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;


            baseFunctions.isInitialized = true;
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socielInterraction, MRUser user)
        {
            var quickMenuCollider = GameObject.Find("UserInterface/QuickMenu").GetComponent<BoxCollider>();
            quickMenuCollider.size = new Vector3(2517.34f, 2511.213f, 1);
            menuTab = TabApi.MakeTabButton(functions, MoonriseIcon, MenuTab.MenuSide.Bottom, 1, "Update!", Color.blue);

            //Mute All
            var muteAllButton = new QMToggleButton(functions, 1, 0, "Mute All", delegate
            {
                PlayerMute.muteAll = true;
            }, "Disabled", delegate
            {
                PlayerMute.muteAll = false;
                PlayerMute.UnmutePlayers();
            }, "Mutes all players in the world except friends", null, null, false, false);

            //Mute Friends
            var muteFriendsTransform = new QMToggleButton(functions, 2, 0, "Mute Friends", delegate
            {
                PlayerMute.muteFriends = true;
            }, "Disabled", delegate
            {
                PlayerMute.muteFriends = false;
                PlayerMute.UnmutePlayers();
            }, "Mutes all friends in the world", null, null, false, false);

            if (user == null) return;

            if (user.Premium)
            {
                EmojiSpam.emoji = 17;
                EmojiSpam.maxTime = true;

                var emojiSpam = new QMToggleButton(functions, 4, 1, $"Emoji Spam\n{(Emoji)emoji}", delegate
                {
                    EmojiSpam.emojiSpam = true;
                    MelonCoroutines.Start(EmojiSpam.SpamEmojis());
                }, "Disabled", delegate
                {
                    EmojiSpam.emojiSpam = false;
                }, $"Spams {(Emoji)emoji} emoji every {spamInterval} seconds");
                UshioRubyModifiers.MoveHalf(emojiSpam, UshioRubyModifiers.HalfPosition.Bottom);

                var timeSpanToggle = new QMToggleButton(functions, 4, 0, $"10 Seconds", delegate
                {
                    EmojiSpam.maxTime = true;
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every {EmojiSpam.spamInterval} seconds");
                }, "5 Seconds", delegate
                {
                    EmojiSpam.maxTime = false;
                    emojiSpam.setToolTip($"Spams {(Emoji)emoji} emoji every {EmojiSpam.spamInterval} seconds");
                }, "Emoji spam interval", null, null, false, EmojiSpam.maxTime);

                var cycleEmojisUp = new QMSingleButton(functions, 4, 1, "", delegate
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
                UshioRubyModifiers.MakeArrowButton(cycleEmojisUp, UshioRubyModifiers.ArrowDirection.Up);
                UshioRubyModifiers.SetHalfButton(cycleEmojisUp, UshioRubyModifiers.HalfPosition.Top, UshioRubyModifiers.Rotation.Verticle);

                var cycleEmojisDown = new QMSingleButton(functions, 4, 2, "", delegate
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
                UshioRubyModifiers.MakeArrowButton(cycleEmojisDown, UshioRubyModifiers.ArrowDirection.Down);
                UshioRubyModifiers.SetHalfButton(cycleEmojisDown, UshioRubyModifiers.HalfPosition.Bottom, UshioRubyModifiers.Rotation.Verticle);

            }
        }
    }
}
