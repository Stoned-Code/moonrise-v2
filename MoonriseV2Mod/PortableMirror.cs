using MoonriseV2Mod.API;
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
    internal class PortableMirror : MoonriseObject
    {
        public static PortableMirror portableMirror;
        public PortableMirror()
        {
            Moonrise.loadMenu += LoadMenu;
        }
        public QMNestedButton portableMirrorNested;
        public static GameObject mirror;
        public static VRC_Pickup pickup => mirror.GetComponent<VRC_Pickup>();
        public static VRC_MirrorReflection mirrorReflection => mirror.GetComponent<VRC_MirrorReflection>();
        public static bool isPickupable = true;

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, MRUser user)
        {
            portableMirrorNested = new QMNestedButton(functions, 3, 2, "Portable\nMirror", "Functions for a portable mirror");

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

            var mirrorWidthSlider = new UshioMenuSlider(UshioMenuSlider.SliderPosition.CenterLeft, "Mirror Width:", portableMirrorNested, 1f, 5f, Config.config.portableMirrorWidth, delegate (float value)
            {
                try
                {
                    PortableMirror.SetWidth(value);
                }

                catch { }
            });

            var mirrorHeightSlider = new UshioMenuSlider(UshioMenuSlider.SliderPosition.CenterRight, "Mirror Height:", portableMirrorNested, 1f, 5f, Config.config.portableMirrorHeight, delegate (float value)
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
            var obj = MoonriseAssetBundles.MoonriseAssetBundle.LoadAsset("PortableMirror").Cast<GameObject>();
            mirror = GameObject.Instantiate(obj, PlayerCheckApi.LocalVRCPlayer.transform.GetParent(), worldPositionStays: true);
            mirror.transform.localScale = new Vector3(Config.config.portableMirrorWidth, Config.config.portableMirrorHeight, 0f);
            pickup.pickupable = isPickupable;

            ResetMirrorPosition();
        }

        public static void DespawnMirror()
        {
            if (mirror == null) return;
            GameObject.Destroy(mirror);
        }

        public static void SetWidth(float value)
        {
            Config.config.portableMirrorWidth = value;
            Config.config.WriteConfig();
            if (portableMirror == null) return;
            mirror.transform.localScale = new Vector3(Config.config.portableMirrorWidth, Config.config.portableMirrorHeight, 1f);
        }

        public static void SetHeight(float value)
        {
            Config.config.portableMirrorHeight = value;
            Config.config.WriteConfig();
            if (mirror == null) return;
            mirror.transform.localScale = new Vector3(Config.config.portableMirrorWidth, Config.config.portableMirrorHeight, 1f);
        }

        public static void ResetMirrorPosition()
        {
            if (portableMirror == null) return;
            Vector3 pos = PlayerCheckApi.LocalVRCPlayer.transform.position - mirror.transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            Quaternion trueRotation = Quaternion.Euler(0f, rotation.eulerAngles.y + 180f, 0f);
            mirror.transform.SetPositionAndRotation(PlayerCheckApi.LocalVRCPlayer.transform.position + PlayerCheckApi.LocalVRCPlayer.transform.forward + new Vector3(0f, Config.config.portableMirrorHeight * 0.5f, 0f), trueRotation);

            Vector3 pos2 = PlayerCheckApi.LocalVRCPlayer.transform.position - mirror.transform.position;
            Quaternion rotation2 = Quaternion.LookRotation(pos2);
            Quaternion trueRotation2 = Quaternion.Euler(0f, rotation2.eulerAngles.y + 180f, 0f);
            mirror.transform.SetPositionAndRotation(PlayerCheckApi.LocalVRCPlayer.transform.position + PlayerCheckApi.LocalVRCPlayer.transform.forward + new Vector3(0f, Config.config.portableMirrorHeight * 0.5f, 0f), trueRotation2);
        }

        public static void ToggleMirrorPickup(bool active)
        {
            isPickupable = active;
            if (pickup == null) return;
            pickup.pickupable = active;
        }
    }
}
