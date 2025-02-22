﻿using MoonriseV2Mod.API;
using MoonriseV2Mod.Patches;
using MoonriseV2Mod.Settings;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UshioUI;
using VRC.SDKBase;

namespace MoonriseV2Mod
{
    internal class PortableMirror : MoonriseMenu
    {
        public static PortableMirror portableMirror;

        public QMNestedButton portableMirrorNested;
        public static GameObject mirror;
        public static int defaultLayer = 8676887;
        public static int optimizedLayer = 263680;
        public static VRC_Pickup pickup
        {
            get
            {
                if (mirror == null) return null;
                return mirror.GetComponent<VRC_Pickup>();
            }
        }
        public static VRC_MirrorReflection mirrorReflection => mirror.GetComponent<VRC_MirrorReflection>();
        public static bool isPickupable = true;
        public static bool useOptimized = true;

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            portableMirrorNested = new QMNestedButton(functions, 4, 0, "Portable\nMirror", "Functions for a portable mirror");

            var portableMirrorToggle = new QMToggleButton(portableMirrorNested, 1, 0, "Mirror", delegate
            {
                PortableMirror.SpawnMirror();
            }, "Disabled", delegate
            {
                PortableMirror.DespawnMirror();
            }, "Toggles portable mirror\nHotkey: LShift + 1");

            var mirrorResetButton = new QMSingleButton(portableMirrorNested, 2, 0, "Reset\nMirror\nPosition", delegate
            {
                PortableMirror.ResetMirrorPosition();
            }, "Resets position of portable mirror");

            var portableMirrorPickupToggle = new QMToggleButton(portableMirrorNested, 3, 0, "Pickupable", delegate
            {
                PortableMirror.ToggleMirrorPickup(true);
            }, "Disabled", delegate
            {
                PortableMirror.ToggleMirrorPickup(false);
            }, "Toggles ability to pickup mirror", null, null, false, PortableMirror.isPickupable);

            //var debug = new QMSingleButton(portableMirrorNested, 4, 0, "Get\nLayers", () =>
            //{
            //    VRC_MirrorReflection[] mirror = GameObject.FindObjectsOfType<VRC_MirrorReflection>();
            //    foreach (VRC_MirrorReflection reflection in mirror)
            //    {
            //        MoonriseConsole.Log($"{reflection.name} Layer: {reflection.m_ReflectLayers.value}");
            //    }
            //}, "Gets mirror layers.");

            var portableMirrorOptimized = new QMToggleButton(portableMirrorNested, 4, 0, "Optimized", () =>
            {
                PortableMirror.SetOptimized(true);
                useOptimized = true;
            }, "Disabled", () =>
            {
                PortableMirror.SetOptimized(false);
                useOptimized = false;
            }, "Optimized portable mirror.", null, null, false, useOptimized);

            var mirrorWidthSlider = new UshioMenuSlider(UshioMenuSlider.SliderPosition.CenterLeft, "Mirror Width:", portableMirrorNested, 1f, 5f, MRConfiguration.config.portableMirrorWidth, delegate (float value)
            {
                try
                {
                    PortableMirror.SetWidth(value);
                }

                catch { }
            });

            var mirrorHeightSlider = new UshioMenuSlider(UshioMenuSlider.SliderPosition.CenterRight, "Mirror Height:", portableMirrorNested, 1f, 5f, MRConfiguration.config.portableMirrorHeight, delegate (float value)
            {
                try
                {
                    PortableMirror.SetHeight(value);
                }

                catch { }
            });
        }

        public static void Initialize()
        {
            portableMirror = new PortableMirror();
            portableMirror.isInitialized = true;
        }
        public static void SpawnMirror()
        {
            if (mirror != null) return;
            var obj = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset("PortableMirror").Cast<GameObject>();
            mirror = GameObject.Instantiate(obj, PlayerCheck.LocalVRCPlayer.transform.GetParent(), worldPositionStays: true);
            mirror.transform.localScale = new Vector3(MRConfiguration.config.portableMirrorWidth, MRConfiguration.config.portableMirrorHeight, 0f);
            pickup.pickupable = isPickupable;
            mirrorReflection.m_ReflectLayers = useOptimized ? optimizedLayer : defaultLayer;
            ResetMirrorPosition();
        }

        public static void DespawnMirror()
        {
            if (mirror == null) return;
            GameObject.Destroy(mirror);
        }

        public static void SetWidth(float value)
        {
            MRConfiguration.config.portableMirrorWidth = value;
            //MRConfiguration.config.WriteConfig();
            if (portableMirror == null) return;
            mirror.transform.localScale = new Vector3(MRConfiguration.config.portableMirrorWidth, MRConfiguration.config.portableMirrorHeight, 1f);
        }

        public static void SetHeight(float value)
        {
            MRConfiguration.config.portableMirrorHeight = value;
            //MRConfiguration.config.WriteConfig();
            if (mirror == null) return;
            mirror.transform.localScale = new Vector3(MRConfiguration.config.portableMirrorWidth, MRConfiguration.config.portableMirrorHeight, 1f);
        }

        public static void ResetMirrorPosition()
        {
            if (portableMirror == null) return;
            Vector3 pos = PlayerCheck.LocalVRCPlayer.transform.position - mirror.transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            Quaternion trueRotation = Quaternion.Euler(0f, rotation.eulerAngles.y + 180f, 0f);
            mirror.transform.SetPositionAndRotation(PlayerCheck.LocalVRCPlayer.transform.position + PlayerCheck.LocalVRCPlayer.transform.forward + new Vector3(0f, MRConfiguration.config.portableMirrorHeight * 0.5f, 0f), trueRotation);

            Vector3 pos2 = PlayerCheck.LocalVRCPlayer.transform.position - mirror.transform.position;
            Quaternion rotation2 = Quaternion.LookRotation(pos2);
            Quaternion trueRotation2 = Quaternion.Euler(0f, rotation2.eulerAngles.y + 180f, 0f);
            mirror.transform.SetPositionAndRotation(PlayerCheck.LocalVRCPlayer.transform.position + PlayerCheck.LocalVRCPlayer.transform.forward + new Vector3(0f, MRConfiguration.config.portableMirrorHeight * 0.5f, 0f), trueRotation2);
        }

        public static void ToggleMirrorPickup(bool active)
        {
            isPickupable = active;
            if (pickup == null) return;
            pickup.pickupable = active;
        }

        public static void SetOptimized(bool optimized)
        {
            mirrorReflection.m_ReflectLayers = optimized ? optimizedLayer : defaultLayer;
        }
    }
}
