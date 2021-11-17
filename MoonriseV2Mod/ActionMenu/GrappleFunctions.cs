using MelonLoader;
using MoonriseV2Mod.API;
using MoonriseV2Mod.CustomBehavior;
using MoonriseV2Mod.MonoBehaviourScripts;
using MoonriseV2Mod.Patches;
using System;
using System.Collections;
using UnityEngine;

namespace MoonriseV2Mod.ActionMenu
{
    internal class GrappleFunctions
    {
        private static bool grappling = false;
        private static bool cancelGrapple = false;
        private static bool isHumanoid = false;

        private static Transform rightArm;
        private static Transform leftArm;
        private static Transform rightHand;
        private static Transform leftHand;

        public static bool m_grappling => grappling;

        public static void Initialize()
        {
            VRCAvatarManagerLoadedPatch.OnAvatarLoaded += AvatarLoaded;
        }

        private static void AvatarLoaded(VRCAvatarManager avatarManager, MRAvatarController avatarController, GameObject avatarObject, string avatarId)
        {
            var grasper1 = VRCTrackingManager.field_Private_Static_VRCTrackingManager_0.field_Private_IkController_0.field_Private_VRCHandGrasper_0;
            if (!avatarController.isLocalPlayer) return;
            
            Transform right = avatarObject.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.RightLowerArm);
            Transform left = avatarObject.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.LeftLowerArm);
            Transform rightH = avatarObject.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
            Transform leftH = avatarObject.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.LeftHand);

            isHumanoid = avatarObject.GetComponentInChildren<Animator>().isHuman;

            rightArm = right;
            leftArm = left;
            rightHand = rightH;
            leftHand = leftH;
        }

        public static void LaunchAnchor(LaunchSide side, Action afterAction)
        {
            if (!isHumanoid)
            {
                UshioUI.UshioMenuApi.PopupUI("Avatar is not humanoid", "Grapple Functions");
                return;
            }

            Transform launchPos;
            Transform launchHand;

            if (side == LaunchSide.Left)
            {
                launchPos = leftArm;
                launchHand = leftHand;
            }

            else
            {
                launchPos = rightArm;
                launchHand = rightHand;
            }
            Vector3 direction = (launchPos.position - launchHand.position).normalized;
            RaycastHit hit;
            int layerMask = 1 << 10;
            bool inVr = PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.IsUserInVR();

            if (Physics.Raycast(inVr ? launchHand.position : PlayerCheck.DesktopCamera.transform.position, inVr ? launchHand.up * 2 : PlayerCheck.DesktopCamera.transform.forward, out hit, float.MaxValue, ~layerMask))
            {
                MelonCoroutines.Start(Launching(hit.m_Point, side, inVr, hit.collider.transform, afterAction));
            }
        }

        public static void CancelGrapple()
        {
            if (!grappling) return;
            cancelGrapple = true;
        }

        private static IEnumerator Launching(Vector3 hit, LaunchSide side, bool inVR, Transform parent, Action afterAction)
        {
            if (grappling) yield break;
            grappling = true;
            GameObject tempObject = QXNzZXRCdW5kbGVz.MoonriseAssetBundle.LoadAsset<GameObject>("Anchor.prefab");


            bool rightSide = side == LaunchSide.Right;

            tempObject = GameObject.Instantiate(tempObject);
            var rot = rightSide ? rightArm.rotation : leftArm.rotation;
            rot = inVR ? rot : PlayerCheck.DesktopCamera.transform.rotation;
            tempObject.transform.position = hit;
            tempObject.transform.rotation = rot;
            tempObject.transform.SetParent(parent);
            GrappleAnchor anchor = tempObject.AddComponent<GrappleAnchor>();
            anchor.Initialize(rightSide ? rightHand : leftHand);

            while (tempObject != null)
            {
                try
                {
                    if (PlayerCheck.LocalVRCPlayer == null) break;

                    if (cancelGrapple)
                    {
                        anchor.RemoveAnchor();
                        cancelGrapple = false;
                    }
                }

                catch { }

                yield return new WaitForEndOfFrame();
            }

            grappling = false;
            afterAction?.Invoke();
        }

        public enum LaunchSide
        {
            Right = 0,
            Left = 1
        }
    }
}
