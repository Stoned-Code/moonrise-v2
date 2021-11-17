using MelonLoader;
using MoonriseV2Mod.API;
using MoonriseV2Mod.CustomBehavior;
using MoonriseV2Mod.Patches;
using MoonriseV2Mod.Settings;
using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;
using VRC;
using VRC.Core;

namespace MoonriseV2Mod.MonoBehaviourScripts
{
    [RegisterTypeInIl2Cpp]
    public class MRAvatarController : MonoBehaviour
    {
        [HideFromIl2Cpp]
        public static MRAvatarController localAvatarController { get; private set; }
        public MRAvatarController(IntPtr ptr) : base(ptr) { }
        public MRDynamicBoneController dynamicBoneController;
        private Player player;
        private VRCAvatarManager avatarManager;

        private string currentAvatarId;

        //private bool m_isLocalPlayer;
        public bool isLocalPlayer
        {
            [HideFromIl2Cpp]
            get
            {
                if (apiUser == null) return false;
                return apiUser.id == APIUser.CurrentUser.id;
            }
        }

        public APIUser apiUser
        {
            [HideFromIl2Cpp]
            get 
            {
                if (player == null) return null;
                return player.field_Private_APIUser_0; 
            }
        }

        private GameObject AvatarObject
        {
            [HideFromIl2Cpp]
            get
            {
                return avatarManager.prop_GameObject_0 ?? null;
            }
        }

        private void Start()
        {
            try
            {
                player = transform.GetParent().GetComponentInChildren<Player>();
                avatarManager = GetComponent<VRCAvatarManager>();
                dynamicBoneController = gameObject.AddComponent<MRDynamicBoneController>();

                if (isLocalPlayer)
                {
                   
                }

                if (apiUser != null)
                {
                    if (isLocalPlayer)
                    {
                        localAvatarController = this;
                    }
                }

            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog("Something fucked up with the avatar controller behaviour...\n" + ex.Message);
            }
        }

        private void Update()
        {
            if (player == null)
            {
                player = transform.GetParent().GetComponent<Player>() ?? null;
                return;
            }

            if (avatarManager == null)
            {
                avatarManager = GetComponent<VRCAvatarManager>();
                return;
            }

            if (!isLocalPlayer) HiderUpdate();
        }

        [HideFromIl2Cpp]
        public bool IsSameAvatar(string id)
        {
            if (currentAvatarId == id) return true;
            
            currentAvatarId = id;
            return false;
        }

        [HideFromIl2Cpp]
        private void HiderUpdate()
        {
            try
            {
                if (MRConfiguration.config.avatarHiderState == 0)
                {
                    ShowAvatar();
                    return;
                }

                bool isIgnored = MRConfiguration.config.ignoreList.TryGetValue(apiUser.id, out string displayName);
                // DynamicBoneController dynamicBoneController = avatarManager.GetComponentInChildren<DynamicBoneController>();

                if (avatarManager == null) return;
                if (AvatarObject == null) return;

                if (isIgnored)
                {
                    ShowAvatar();
                    return;
                }

                if (PlayerCheck.IsFriendsWith(apiUser.id) && VRCAvatarManagerStartPatch.m_IgnoreFriends)
                {
                    ShowAvatar();
                    return;
                }

                float dist = Vector3.Distance(PlayerCheck.LocalVRCPlayer.transform.position, AvatarObject.transform.position);

                if (dist > MRConfiguration.config.avatarHiderDistance && VRCAvatarManagerStartPatch.m_HideAvatars)
                    HideDistantAvatar();

                else if (dist <= MRConfiguration.config.avatarHiderDistance && VRCAvatarManagerStartPatch.m_HideAvatars)
                    ShowAvatar();

                else if (!VRCAvatarManagerStartPatch.m_HideAvatars)
                    ShowAvatar();
            }

            catch //(Exception ex)
            {
                //MoonriseConsole.Log("Something Fucked Up...\n" + ex);
            }
        }

        [HideFromIl2Cpp]
        void ShowAvatar()
        {
            if (!AvatarObject.activeInHierarchy)
                AvatarObject.SetActive(true);
            try
            {
                if (dynamicBoneController != null)
                    for (int dbIndex = 0; dbIndex < dynamicBoneController.m_dynamicBones.Length; dbIndex++)
                    {
                        var db = dynamicBoneController.m_dynamicBones[dbIndex];
                        if (db.enabled) continue;
                        db.enabled = true;
                    }
            }

            catch { }
        }
        [HideFromIl2Cpp]
        private void HideDistantAvatar()
        {
            try
            {
                if (dynamicBoneController != null)
                    for (int dbIndex = 0; dbIndex < dynamicBoneController.m_dynamicBones.Length; dbIndex++)
                    {
                        var db = dynamicBoneController.m_dynamicBones[dbIndex];
                        if (db == null) continue;
                        if (!db.enabled) continue;
                        db.enabled = false;
                    }
            }

            catch { }

            AvatarObject.SetActive(false);
        }
    }
}
