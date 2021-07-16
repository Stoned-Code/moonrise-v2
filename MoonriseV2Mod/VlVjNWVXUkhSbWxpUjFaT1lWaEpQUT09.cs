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
    internal class VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09 : VkZjNWRtSnVTbkJqTWxaUVdXMXZQUT09
    {
        public static VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09 portableMirror;
        public VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09()
        {
            VFc5dmJuSnBjMlU9.loadMenu += LoadMenu;
        }
        public QMNestedButton portableMirrorNested;
        public static GameObject mirror;
        public static VRC_Pickup pickup => mirror.GetComponent<VRC_Pickup>();
        public static VRC_MirrorReflection mirrorReflection => mirror.GetComponent<VRC_MirrorReflection>();
        public static bool isPickupable = true;

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            portableMirrorNested = new QMNestedButton(functions, 3, 2, "Portable\nMirror", "Functions for a portable mirror");

            var portableMirrorToggle = new QMToggleButton(portableMirrorNested, 1, 0, "Mirror", delegate
            {
                VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09.SpawnMirror();
            }, "Disabled", delegate
            {
                VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09.DespawnMirror();
            }, "Toggles portable mirror\nHotkey: LShift + 1");

            var mirrorResetButton = new QMSingleButton(portableMirrorNested, 2, 0, "Reset\nMirror\nPosition", delegate
            {
                VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09.ResetMirrorPosition();
            }, "Resets position of portable mirror");

            var portableMirrorPickupToggle = new QMToggleButton(portableMirrorNested, 3, 0, "Pickupable", delegate
            {
                VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09.ToggleMirrorPickup(true);
            }, "Disabled", delegate
            {
                VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09.ToggleMirrorPickup(false);
            }, "Toggles ability to pickup mirror", null, null, false, VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09.isPickupable);

            var mirrorWidthSlider = new UshioMenuSlider(UshioMenuSlider.SliderPosition.CenterLeft, "Mirror Width:", portableMirrorNested, 1f, 5f, Q29uZmln.config.portableMirrorWidth, delegate (float value)
            {
                try
                {
                    VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09.SetWidth(value);
                }

                catch { }
            });

            var mirrorHeightSlider = new UshioMenuSlider(UshioMenuSlider.SliderPosition.CenterRight, "Mirror Height:", portableMirrorNested, 1f, 5f, Q29uZmln.config.portableMirrorHeight, delegate (float value)
            {
                try
                {
                    VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09.SetHeight(value);
                }

                catch { }
            });
        }

        public static void Initialize()
        {
            portableMirror = new VlVjNWVXUkhSbWxpUjFaT1lWaEpQUT09();
            portableMirror.isInitialized = true;
        }
        public static void SpawnMirror()
        {
            if (mirror != null) return;
            var obj = MoonriseAssetBundles.MoonriseAssetBundle.LoadAsset("PortableMirror").Cast<GameObject>();
            mirror = GameObject.Instantiate(obj, VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.LocalVRCPlayer.transform.GetParent(), worldPositionStays: true);
            mirror.transform.localScale = new Vector3(Q29uZmln.config.portableMirrorWidth, Q29uZmln.config.portableMirrorHeight, 0f);
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
            Q29uZmln.config.portableMirrorWidth = value;
            Q29uZmln.config.WriteConfig();
            if (portableMirror == null) return;
            mirror.transform.localScale = new Vector3(Q29uZmln.config.portableMirrorWidth, Q29uZmln.config.portableMirrorHeight, 1f);
        }

        public static void SetHeight(float value)
        {
            Q29uZmln.config.portableMirrorHeight = value;
            Q29uZmln.config.WriteConfig();
            if (mirror == null) return;
            mirror.transform.localScale = new Vector3(Q29uZmln.config.portableMirrorWidth, Q29uZmln.config.portableMirrorHeight, 1f);
        }

        public static void ResetMirrorPosition()
        {
            if (portableMirror == null) return;
            Vector3 pos = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.LocalVRCPlayer.transform.position - mirror.transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            Quaternion trueRotation = Quaternion.Euler(0f, rotation.eulerAngles.y + 180f, 0f);
            mirror.transform.SetPositionAndRotation(VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.LocalVRCPlayer.transform.position + VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.LocalVRCPlayer.transform.forward + new Vector3(0f, Q29uZmln.config.portableMirrorHeight * 0.5f, 0f), trueRotation);

            Vector3 pos2 = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.LocalVRCPlayer.transform.position - mirror.transform.position;
            Quaternion rotation2 = Quaternion.LookRotation(pos2);
            Quaternion trueRotation2 = Quaternion.Euler(0f, rotation2.eulerAngles.y + 180f, 0f);
            mirror.transform.SetPositionAndRotation(VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.LocalVRCPlayer.transform.position + VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.LocalVRCPlayer.transform.forward + new Vector3(0f, Q29uZmln.config.portableMirrorHeight * 0.5f, 0f), trueRotation2);
        }

        public static void ToggleMirrorPickup(bool active)
        {
            isPickupable = active;
            if (pickup == null) return;
            pickup.pickupable = active;
        }
    }
}
