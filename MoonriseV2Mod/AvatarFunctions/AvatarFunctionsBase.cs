using MoonriseV2Mod.API;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoonriseV2Mod.AvatarFunctions
{
    internal sealed class AvatarFunctionsBase : MoonriseObject
    {
        public static AvatarFunctionsBase avatarFunctionsBase;

        public AvatarFunctionsBase()
        {
            Moonrise.loadMenu += LoadMenu;
            Moonrise.modUpdate += OnUpdate;
        }

        public QMSingleButton avatarHiderSwitch;
        public QMNestedButton avatarFunctions;
        public static void Initialize()
        {
            avatarFunctionsBase = new AvatarFunctionsBase();

            avatarFunctionsBase.isInitialized = true;
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, MRUser user)
        {
            avatarFunctions = new QMNestedButton(functions, 2, 2, "Avatar\nFunctions", "Functions for avatars.");
            //Avatar Distance Switch
            if (Config.config.avatarHiderState == 0)
            {
                AvatarHider.m_HideAvatars = false;
                AvatarHider.m_IgnoreFriends = false;
            }

            else if (Config.config.avatarHiderState == 1)
            {
                AvatarHider.m_HideAvatars = true;
                AvatarHider.m_IgnoreFriends = true;
                Config.config.avatarsShowing = false;
            }

            else if (Config.config.avatarHiderState == 2)
            {
                AvatarHider.m_HideAvatars = true;
                AvatarHider.m_IgnoreFriends = false;
                Config.config.avatarsShowing = false;
            }

            avatarHiderSwitch = new QMSingleButton(avatarFunctions, 2, 0, GetAvatarHiderState(), delegate
            {
                if (Config.config.avatarHiderState == 2)
                {
                    AvatarHider.m_HideAvatars = false;
                    AvatarHider.m_IgnoreFriends = false;
                    Config.config.avatarHiderState = 0;

                    Config.config.avatarHiderState = Config.config.avatarHiderState;

                    avatarHiderSwitch.setButtonText("Avatar Hider:\nDisabled");

                    AvatarHider.UnhideAvatars();
                }

                else
                {
                    Config.config.avatarHiderState++;

                    if (Config.config.avatarHiderState == 1)
                    {
                        AvatarHider.m_HideAvatars = true;
                        AvatarHider.m_IgnoreFriends = true;
                        //MelonCoroutines.Start(mainFunctions.HideAvatars());
                        avatarHiderSwitch.setButtonText("Avatar Hider:\nIgnore\nFriends");
                        Config.config.avatarsShowing = false;
                    }

                    else if (Config.config.avatarHiderState == 2)
                    {
                        AvatarHider.m_HideAvatars = true;
                        AvatarHider.m_IgnoreFriends = false;
                        avatarHiderSwitch.setButtonText("Avatar Hider:\nHide\nEveryone");
                        Config.config.avatarsShowing = false;
                    }
                }

                Config.config.WriteConfig();
            }, "Switches the distant avatar hider settings (If distant avatars are still rendering after loading into a new world, reset the avatar hider)");

            var distanceStatus = new QMSingleButton(avatarFunctions, 1, 1, $"Hide\nDistance:\n{Config.config.avatarHiderDistance}", delegate { }, "", null, Color.yellow);
            UshioRubyModifiers.MakeTextOnly(distanceStatus);

            var increaseAvatarHideDistanceButtonOne = new QMSingleButton(avatarFunctions, 1, 0, "+1", delegate
            {
                Config.config.avatarHiderDistance++;
                Config.config.WriteConfig();
                distanceStatus.setButtonText($"Hide\nDistance:\n{Config.config.avatarHiderDistance}");
            }, "Increases avatar hide distance by 1");
            UshioRubyModifiers.SetHalfButton(increaseAvatarHideDistanceButtonOne, UshioRubyModifiers.HalfPosition.Bottom, UshioRubyModifiers.Rotation.Horizontal);

            var increaseAvatarHideDistanceButtonTwo = new QMSingleButton(avatarFunctions, 1, 0, "+2", delegate
            {
                Config.config.avatarHiderDistance += 2; 
                Config.config.WriteConfig();
                distanceStatus.setButtonText($"Hide\nDistance:\n{Config.config.avatarHiderDistance}");
            }, "Increases avatar hide distance by 2");
            UshioRubyModifiers.SetHalfButton(increaseAvatarHideDistanceButtonTwo, UshioRubyModifiers.HalfPosition.Top, UshioRubyModifiers.Rotation.Horizontal);

            var decreaseAvatarHideDistanceButtonOne = new QMSingleButton(avatarFunctions, 1, 2, "-1", delegate
            {
                if (Config.config.avatarHiderDistance <= 1)
                {
                    MoonriseConsole.Log("You can't set your distance below 1...", ConsoleColor.Yellow);
                }

                else
                {
                    Config.config.avatarHiderDistance--;
                    Config.config.WriteConfig();
                }

                distanceStatus.setButtonText($"Hide\nDistance:\n{Config.config.avatarHiderDistance}");
            }, "Decreases avatar hide distance by 1");
            UshioRubyModifiers.SetHalfButton(decreaseAvatarHideDistanceButtonOne, UshioRubyModifiers.HalfPosition.Top, UshioRubyModifiers.Rotation.Horizontal);

            var decreaseAvatarHideDistanceButtonTwo = new QMSingleButton(avatarFunctions, 1, 2, "-2", delegate
            {
                if (Config.config.avatarHiderDistance <= 2)
                {
                    MoonriseConsole.Log("You can't set your distance below 1...", ConsoleColor.Yellow);
                }

                else
                {
                    Config.config.avatarHiderDistance -= 2;
                    Config.config.WriteConfig();
                }

                distanceStatus.setButtonText($"Hide\nDistance:\n{Config.config.avatarHiderDistance}");
            }, "Decreases avatar hide distance by 2");
            UshioRubyModifiers.SetHalfButton(decreaseAvatarHideDistanceButtonTwo, UshioRubyModifiers.HalfPosition.Bottom, UshioRubyModifiers.Rotation.Horizontal);

        }

        public void OnUpdate()
        {
            AvatarHider.AvatarHiderUpdate();
        }

        public string GetAvatarHiderState()
        {
            if (Config.config.avatarHiderState == 0) return "Avatar Hider:\nDisabled";
            else if (Config.config.avatarHiderState == 1) return "Avatar Hider:\nIgnore\nFriends";
            else if (Config.config.avatarHiderState == 2) return "Avatar Hider:\nHide\nEveryone";
            return null;
        }
    }
}
