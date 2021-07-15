using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoonriseV2Mod.BaseFunctions
{
    internal static class EmojiSpam
    {
        public static bool maxTime = true;
        public static float spamInterval => maxTime ? 10f : 5f;

        public static bool emojiSpam = false;
        public static int emoji = 17;
        public static EmojiMenu emojiMenu => GameObject.Find("UserInterface/QuickMenu/EmojiMenu").GetComponent<EmojiMenu>();

        public static IEnumerator SpamEmojis()
        {
            while (emojiSpam)
            {
                try
                {
                    if (emojiMenu != null)
                    {
                        emojiMenu.TriggerEmoji(emoji);
                    }
                }

                catch { }
                yield return new WaitForSeconds(spamInterval);
                yield return new WaitForEndOfFrame();
            }
        }

        public enum Emoji
        {
            Smile = 0,
            ThumbsUp = 1,
            Heart = 2,
            Frown = 3,
            ThumbsDown = 4,
            Exclamation = 5,
            Laugh = 6,
            Wow = 7,
            Question = 8,
            Kiss = 9,
            InLove = 10,
            Crying = 11,
            Stoic = 12,
            TongueOut = 13,
            Blushing = 14,
            Angry = 15,
            Fire = 16,
            Money = 17,
            BrokenHeart = 18,
            Gift = 19,
            Beer = 20,
            Tomato = 21,
            Zzz = 22,
            Thinking = 23,
            Pizza = 24,
            Sunglasses = 25,
            MusicNote = 26,
            Go = 27,
            HandWave = 28,
            Stop = 29,
            Cloud = 30,
            JackOLantern = 31,
            SpookyGhost = 32,
            Skull = 33,
            Candy = 34,
            CandyCorn = 35,
            Boo = 36,
            Bats = 37,
            Web = 38,
            Mistletoe = 40,
            Snowball = 41,
            SnowFall = 42,
            Coal = 43,
            CandyCane = 44,
            Gingerbread = 45,
            Confetti = 46,
            Champagne = 47,
            Gifts = 48,
            Beachball = 49,
            CoconutDrink = 50,
            HangTen = 51,
            IceCream = 52,
            LifeRing = 53,
            NeonShades = 54,
            Pineapple = 55,
            Splash = 56,
            SunScreen = 57
        }
    }
}
