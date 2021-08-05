using MoonriseV2Mod.API;
using MoonriseV2Mod.MonoBehaviourScripts;
using MoonriseV2Mod.Patches;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;
using System;
using UnityEngine;

namespace MoonriseV2Mod.AvatarFunctions
{
    internal sealed class QXZhdGFyRnVuY3Rpb25z : MoonriseObject
    {
        public static QXZhdGFyRnVuY3Rpb25z avatarFunctionsBase;

        public QXZhdGFyRnVuY3Rpb25z()
        {
            QuickMenuPatches.loadMenu += LoadMenu;
            Moonrise.modUpdate += OnUpdate;
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
            if (MRConfiguration.config.avatarHiderState == 0)
            {
                VRCAvatarManagerStartPatch.m_HideAvatars = false;
                VRCAvatarManagerStartPatch.m_IgnoreFriends = false;
            }

            else if (MRConfiguration.config.avatarHiderState == 1)
            {
                VRCAvatarManagerStartPatch.m_HideAvatars = true;
                VRCAvatarManagerStartPatch.m_IgnoreFriends = true;
                MRConfiguration.config.avatarsShowing = false;
            }

            else if (MRConfiguration.config.avatarHiderState == 2)
            {
                VRCAvatarManagerStartPatch.m_HideAvatars = true;
                VRCAvatarManagerStartPatch.m_IgnoreFriends = false;
                MRConfiguration.config.avatarsShowing = false;
            }

            avatarHiderSwitch = new QMSingleButton(avatarFunctions, 2, 0, GetAvatarHiderState(), delegate
            {
                if (MRConfiguration.config.avatarHiderState == 2)
                {
                    VRCAvatarManagerStartPatch.m_HideAvatars = false;
                    VRCAvatarManagerStartPatch.m_IgnoreFriends = false;
                    MRConfiguration.config.avatarHiderState = 0;

                    MRConfiguration.config.avatarHiderState = MRConfiguration.config.avatarHiderState;

                    avatarHiderSwitch.setButtonText("Avatar Hider:\nDisabled");

                    // VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.UnhideAvatars();
                }

                else
                {
                    MRConfiguration.config.avatarHiderState++;

                    if (MRConfiguration.config.avatarHiderState == 1)
                    {
                        VRCAvatarManagerStartPatch.m_HideAvatars = true;
                        VRCAvatarManagerStartPatch.m_IgnoreFriends = true;

                        avatarHiderSwitch.setButtonText("Avatar Hider:\nIgnore\nFriends");
                        MRConfiguration.config.avatarsShowing = false;
                    }

                    else if (MRConfiguration.config.avatarHiderState == 2)
                    {
                        VRCAvatarManagerStartPatch.m_HideAvatars = true;
                        VRCAvatarManagerStartPatch.m_IgnoreFriends = false;
                        avatarHiderSwitch.setButtonText("Avatar Hider:\nHide\nEveryone");
                        MRConfiguration.config.avatarsShowing = false;
                    }
                }

                MRConfiguration.config.WriteConfig();
            }, "Switches the avatar hider settings (If distant avatars are still rendering after loading into a new world, reset the avatar hider)");

            var distanceStatus = new QMSingleButton(avatarFunctions, 1, 1, $"Hide\nDistance:\n{MRConfiguration.config.avatarHiderDistance}", delegate { }, "", null, Color.yellow);
            VXNoaW9SdWJ5TW9kaWZpZXJz.MakeTextOnly(distanceStatus);

            var increaseAvatarHideDistanceButtonOne = new QMSingleButton(avatarFunctions, 1, 0, "+1", delegate
            {
                MRConfiguration.config.avatarHiderDistance++;
                MRConfiguration.config.WriteConfig();
                distanceStatus.setButtonText($"Hide\nDistance:\n{MRConfiguration.config.avatarHiderDistance}");
            }, "Increases avatar hide distance by 1");
            VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(increaseAvatarHideDistanceButtonOne, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Bottom, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Horizontal);

            var increaseAvatarHideDistanceButtonTwo = new QMSingleButton(avatarFunctions, 1, 0, "+2", delegate
            {
                MRConfiguration.config.avatarHiderDistance += 2; 
                MRConfiguration.config.WriteConfig();
                distanceStatus.setButtonText($"Hide\nDistance:\n{MRConfiguration.config.avatarHiderDistance}");
            }, "Increases avatar hide distance by 2");
            VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(increaseAvatarHideDistanceButtonTwo, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Top, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Horizontal);

            var decreaseAvatarHideDistanceButtonOne = new QMSingleButton(avatarFunctions, 1, 2, "-1", delegate
            {
                if (MRConfiguration.config.avatarHiderDistance <= 1)
                {
                    MoonriseConsole.Log("You can't set your distance below 1...", ConsoleColor.Yellow);
                }

                else
                {
                    MRConfiguration.config.avatarHiderDistance--;
                    MRConfiguration.config.WriteConfig();
                }

                distanceStatus.setButtonText($"Hide\nDistance:\n{MRConfiguration.config.avatarHiderDistance}");
            }, "Decreases avatar hide distance by 1");
            VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(decreaseAvatarHideDistanceButtonOne, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Top, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Horizontal);

            var decreaseAvatarHideDistanceButtonTwo = new QMSingleButton(avatarFunctions, 1, 2, "-2", delegate
            {
                if (MRConfiguration.config.avatarHiderDistance <= 2)
                {
                    MoonriseConsole.Log("You can't set your distance below 1...", ConsoleColor.Yellow);
                }

                else
                {
                    MRConfiguration.config.avatarHiderDistance -= 2;
                    MRConfiguration.config.WriteConfig();
                }

                distanceStatus.setButtonText($"Hide\nDistance:\n{MRConfiguration.config.avatarHiderDistance}");
            }, "Decreases avatar hide distance by 2");
            VXNoaW9SdWJ5TW9kaWZpZXJz.SetHalfButton(decreaseAvatarHideDistanceButtonTwo, VXNoaW9SdWJ5TW9kaWZpZXJz.HalfPosition.Bottom, VXNoaW9SdWJ5TW9kaWZpZXJz.Rotation.Horizontal);

        }

        public void OnUpdate()
        {
            // VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09.AvatarHiderUpdate();
        }

        public string GetAvatarHiderState()
        {
            if (MRConfiguration.config.avatarHiderState == 0) return "Avatar Hider:\nDisabled";
            else if (MRConfiguration.config.avatarHiderState == 1) return "Avatar Hider:\nIgnore\nFriends";
            else if (MRConfiguration.config.avatarHiderState == 2) return "Avatar Hider:\nHide\nEveryone";
            return null;
        }
    }
}
