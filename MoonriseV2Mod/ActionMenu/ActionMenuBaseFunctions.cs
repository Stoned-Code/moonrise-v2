﻿using ActionMenuAPI;
using MoonriseV2Mod.API;
using MoonriseV2Mod.BaseFunctions;
using RubyButtonAPI;
using UnhollowerRuntimeLib;
using UnityEngine;
using UshioUI;

namespace MoonriseV2Mod.ActionMenu
{
    internal sealed class ActionMenuBaseFunctions : MoonriseMenu
    {
        private static ActionMenuBaseFunctions menuFunctions;
        public static ActionMenuBaseFunctions m_menuFunctions => menuFunctions;
        public ActionMenuApi actionMenu;

        public static void Initialize()
        {
            menuFunctions = new ActionMenuBaseFunctions();
            GrappleFunctions.Initialize();
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            actionMenu = new ActionMenuApi();

            Sprite grappleLeftIcon = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/ActionMenu/GrappleLeft.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            Sprite grappleRightIcon = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/ActionMenu/GrappleRight.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            Sprite grappleCancelIcon = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/ActionMenu/GrappleCancel.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();

            grappleLeftIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            grappleRightIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            grappleCancelIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            PedalOption slackPedal;
            actionMenu.AddPedalToExistingMenu(ActionMenuApi.ActionMenuPageType.Main, () =>
            {
                actionMenu.CreateSubMenu(() =>
                {
                    actionMenu.AddPedalToCustomMenu(() =>
                    {
                        GrappleFunctions.LaunchAnchor(GrappleFunctions.LaunchSide.Right);
                    }, "", grappleRightIcon.texture);

                    actionMenu.AddPedalToCustomMenu(() =>
                    {
                        GrappleFunctions.CancelGrapple();
                    }, "", grappleCancelIcon.texture);

                    actionMenu.AddPedalToCustomMenu(() =>
                    {
                        GrappleFunctions.LaunchAnchor(GrappleFunctions.LaunchSide.Left);
                    }, "", grappleLeftIcon.texture);
                });

            }, "Moonrise", MoonriseBaseFunctions.baseFunctions.m_moonriseLogo.texture);

            Sprite moonriseOptions = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/ActionMenu/MoonriseOptionButton.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            Sprite respawnIcon = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/ActionMenu/RespawnButton.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();
            Sprite goHomeIcon = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset_Internal("Assets/Moonrise/ActionMenu/GoHomeButton.png", Il2CppType.Of<Sprite>()).Cast<Sprite>();

            moonriseOptions.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            respawnIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            goHomeIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            actionMenu.AddPedalToExistingMenu(ActionMenuApi.ActionMenuPageType.Options, () =>
            {
                actionMenu.CreateSubMenu(() =>
                {
                    actionMenu.AddPedalToCustomMenu(() =>
                    {
                        UshioMenuApi.RespawnButton.onClick?.Invoke();
                    }, "Respawn", respawnIcon.texture);

                    actionMenu.AddPedalToCustomMenu(() =>
                    {
                        UshioMenuApi.GoHomeButton.onClick?.Invoke();
                    }, "Go Home", goHomeIcon.texture);
                });
            }, "Moonrise Options", moonriseOptions.texture);
        }
    }
}
