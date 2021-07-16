using MoonriseV2Mod.API;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;
using System;
using UnityEngine;

namespace MoonriseV2Mod.AvatarFunctions
{
    internal sealed class QXZhdGFyRnVuY3Rpb25z : VkZjNWRtSnVTbkJqTWxaUVdXMXZQUT09
    {
        public static QXZhdGFyRnVuY3Rpb25z avatarFunctionsBase;

        public QXZhdGFyRnVuY3Rpb25z()
        {
            VFc5dmJuSnBjMlU9.loadMenu += LoadMenu;
            VFc5dmJuSnBjMlU9.modUpdate += OnUpdate;
        }

        public QMSingleButton avatarHiderSwitch;
        public QMNestedButton avatarFunctions;
        public static void Initialize()
        {
            avatarFunctionsBase = new QXZhdGFyRnVuY3Rpb25z();

            avatarFunctionsBase.isInitialized = true;
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            avatarFunctions = new QMNestedButton(functions, 3, 0, "Avatar\nFunctions", "Functions for avatars.");

            //Avatar Distance Switch
            if (Q29uZmln.config.avatarHiderState == 0)
            {
                VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_HideAvatars = false;
                VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_IgnoreFriends = false;
            }

            else if (Q29uZmln.config.avatarHiderState == 1)
            {
                VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_HideAvatars = true;
                VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_IgnoreFriends = true;
                Q29uZmln.config.avatarsShowing = false;
            }

            else if (Q29uZmln.config.avatarHiderState == 2)
            {
                VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_HideAvatars = true;
                VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_IgnoreFriends = false;
                Q29uZmln.config.avatarsShowing = false;
            }

            avatarHiderSwitch = new QMSingleButton(avatarFunctions, 2, 0, GetAvatarHiderState(), delegate
            {
                if (Q29uZmln.config.avatarHiderState == 2)
                {
                    VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_HideAvatars = false;
                    VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_IgnoreFriends = false;
                    Q29uZmln.config.avatarHiderState = 0;

                    Q29uZmln.config.avatarHiderState = Q29uZmln.config.avatarHiderState;

                    avatarHiderSwitch.setButtonText("Avatar Hider:\nDisabled");

                    VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.UnhideAvatars();
                }

                else
                {
                    Q29uZmln.config.avatarHiderState++;

                    if (Q29uZmln.config.avatarHiderState == 1)
                    {
                        VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_HideAvatars = true;
                        VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_IgnoreFriends = true;

                        avatarHiderSwitch.setButtonText("Avatar Hider:\nIgnore\nFriends");
                        Q29uZmln.config.avatarsShowing = false;
                    }

                    else if (Q29uZmln.config.avatarHiderState == 2)
                    {
                        VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_HideAvatars = true;
                        VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.m_IgnoreFriends = false;
                        avatarHiderSwitch.setButtonText("Avatar Hider:\nHide\nEveryone");
                        Q29uZmln.config.avatarsShowing = false;
                    }
                }

                Q29uZmln.config.WriteConfig();
            }, "Switches the avatar hider settings (If distant avatars are still rendering after loading into a new world, reset the avatar hider)");

            var distanceStatus = new QMSingleButton(avatarFunctions, 1, 1, $"Hide\nDistance:\n{Q29uZmln.config.avatarHiderDistance}", delegate { }, "", null, Color.yellow);
            UshioRubyModifiers.MakeTextOnly(distanceStatus);

            var increaseAvatarHideDistanceButtonOne = new QMSingleButton(avatarFunctions, 1, 0, "+1", delegate
            {
                Q29uZmln.config.avatarHiderDistance++;
                Q29uZmln.config.WriteConfig();
                distanceStatus.setButtonText($"Hide\nDistance:\n{Q29uZmln.config.avatarHiderDistance}");
            }, "Increases avatar hide distance by 1");
            UshioRubyModifiers.SetHalfButton(increaseAvatarHideDistanceButtonOne, UshioRubyModifiers.HalfPosition.Bottom, UshioRubyModifiers.Rotation.Horizontal);

            var increaseAvatarHideDistanceButtonTwo = new QMSingleButton(avatarFunctions, 1, 0, "+2", delegate
            {
                Q29uZmln.config.avatarHiderDistance += 2; 
                Q29uZmln.config.WriteConfig();
                distanceStatus.setButtonText($"Hide\nDistance:\n{Q29uZmln.config.avatarHiderDistance}");
            }, "Increases avatar hide distance by 2");
            UshioRubyModifiers.SetHalfButton(increaseAvatarHideDistanceButtonTwo, UshioRubyModifiers.HalfPosition.Top, UshioRubyModifiers.Rotation.Horizontal);

            var decreaseAvatarHideDistanceButtonOne = new QMSingleButton(avatarFunctions, 1, 2, "-1", delegate
            {
                if (Q29uZmln.config.avatarHiderDistance <= 1)
                {
                    TW9vbnJpc2VDb25zb2xl.Log("You can't set your distance below 1...", ConsoleColor.Yellow);
                }

                else
                {
                    Q29uZmln.config.avatarHiderDistance--;
                    Q29uZmln.config.WriteConfig();
                }

                distanceStatus.setButtonText($"Hide\nDistance:\n{Q29uZmln.config.avatarHiderDistance}");
            }, "Decreases avatar hide distance by 1");
            UshioRubyModifiers.SetHalfButton(decreaseAvatarHideDistanceButtonOne, UshioRubyModifiers.HalfPosition.Top, UshioRubyModifiers.Rotation.Horizontal);

            var decreaseAvatarHideDistanceButtonTwo = new QMSingleButton(avatarFunctions, 1, 2, "-2", delegate
            {
                if (Q29uZmln.config.avatarHiderDistance <= 2)
                {
                    TW9vbnJpc2VDb25zb2xl.Log("You can't set your distance below 1...", ConsoleColor.Yellow);
                }

                else
                {
                    Q29uZmln.config.avatarHiderDistance -= 2;
                    Q29uZmln.config.WriteConfig();
                }

                distanceStatus.setButtonText($"Hide\nDistance:\n{Q29uZmln.config.avatarHiderDistance}");
            }, "Decreases avatar hide distance by 2");
            UshioRubyModifiers.SetHalfButton(decreaseAvatarHideDistanceButtonTwo, UshioRubyModifiers.HalfPosition.Bottom, UshioRubyModifiers.Rotation.Horizontal);

        }

        public void OnUpdate()
        {
            VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.AvatarHiderUpdate();
        }

        public string GetAvatarHiderState()
        {
            if (Q29uZmln.config.avatarHiderState == 0) return "Avatar Hider:\nDisabled";
            else if (Q29uZmln.config.avatarHiderState == 1) return "Avatar Hider:\nIgnore\nFriends";
            else if (Q29uZmln.config.avatarHiderState == 2) return "Avatar Hider:\nHide\nEveryone";
            return null;
        }
    }
}
