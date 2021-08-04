using MelonLoader;
using MoonriseV2Mod.API;
using MoonriseV2Mod.AvatarFunctions;
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
    public class MRAvatarHider : MonoBehaviour
    {
        public MRAvatarHider(IntPtr ptr) : base(ptr) { }

        Player player;

        [HideFromIl2Cpp]
        APIUser apiUser
        {
            [HideFromIl2Cpp]
            get 
            {
                if (player == null) return null;
                return player.field_Private_APIUser_0; 
            }
        }

        void Start()
        {
            player = transform.GetParent().GetComponent<Player>();
        }

        void Update()
        {
            try
            {
                if (player == null)
                {
                    player = transform.GetParent().GetComponent<Player>() ?? null;
                    return;
                }

                if (MRConfiguration.config.avatarHiderState == 0) return;
                if (PlayerCheck.IsMe(transform.GetParent().GetComponent<Player>().field_Private_APIUser_0.id)) return;

                bool isIgnored = MRConfiguration.config.ignoreList.TryGetValue(apiUser.id, out string displayName);
                GameObject avtrObject = GetAvatarObject(player);
                DynamicBoneController dynamicBoneController = PlayerCheck.GetDynamicBoneController(player);

                if (isIgnored)
                {
                    UnhideIgnored(avtrObject, dynamicBoneController);
                    return;
                }

                if (PlayerCheck.IsFriendsWith(apiUser.id) && VRCAvatarManagerPatches.m_IgnoreFriends)
                {
                    UnhideFriends(avtrObject, dynamicBoneController);
                    return;
                }

                float dist = Vector3.Distance(PlayerCheck.LocalVRCPlayer.transform.position, GetAvatarObject(player).transform.position);

                if (dist > MRConfiguration.config.avatarHiderDistance && VRCAvatarManagerPatches.m_HideAvatars)
                    HideDistantAvatar(avtrObject, dynamicBoneController);

                else if (dist <= MRConfiguration.config.avatarHiderDistance && VRCAvatarManagerPatches.m_HideAvatars)
                    ShowNearbyAvatar(avtrObject, dynamicBoneController);

                else if (!VRCAvatarManagerPatches.m_HideAvatars)
                    ShowAvatar(avtrObject, dynamicBoneController);
            }

            catch // (Exception ex)
            {
                // TW9vbnJpc2VDb25zb2xl.Log("Something Fucked Up...\n" + ex);
            }
        }

        [HideFromIl2Cpp]
        static void UnhideIgnored(GameObject avtrObject, DynamicBoneController dynamicBoneController)
        {
            if (!avtrObject.activeInHierarchy)
                avtrObject.SetActive(true);

            if (dynamicBoneController == null) return;
            
            for (int dbIndex = 0; dbIndex < dynamicBoneController.field_Private_List_1_DynamicBone_0.Count; dbIndex++)
            {
                var db = dynamicBoneController.field_Private_List_1_DynamicBone_0.ToArray()[dbIndex];
                if (db.enabled) continue;
                db.enabled = true;
            }
        }

        [HideFromIl2Cpp]
        static void UnhideFriends(GameObject avtrObject, DynamicBoneController dynamicBoneController)
        {
            if (!avtrObject.activeInHierarchy)
                avtrObject.SetActive(true);

            if (dynamicBoneController == null) return;
            
            for (int dbIndex = 0; dbIndex < dynamicBoneController.field_Private_List_1_DynamicBone_0.Count; dbIndex++)
            {
                var db = dynamicBoneController.field_Private_List_1_DynamicBone_0.ToArray()[dbIndex];
                if (db.enabled) continue;
                db.enabled = true;
            }
        }

        [HideFromIl2Cpp]
        static void HideDistantAvatar(GameObject avtrObject, DynamicBoneController dynamicBoneController)
        {
            if (dynamicBoneController != null)
                for (int dbIndex = 0; dbIndex < dynamicBoneController.field_Private_List_1_DynamicBone_0.Count; dbIndex++)
                {
                    var db = dynamicBoneController.field_Private_List_1_DynamicBone_0.ToArray()[dbIndex];
                    if (!db.enabled) continue;
                    db.enabled = false;
                }
            
            avtrObject.SetActive(false);
        }

        [HideFromIl2Cpp]
        static void ShowNearbyAvatar(GameObject avtrObject, DynamicBoneController dynamicBoneController)
        {
            avtrObject.SetActive(true);

            if (dynamicBoneController == null) return;
            
            for (int dbIndex = 0; dbIndex < dynamicBoneController.field_Private_List_1_DynamicBone_0.Count; dbIndex++)
            {
                var db = dynamicBoneController.field_Private_List_1_DynamicBone_0.ToArray()[dbIndex];
                if (db.enabled) continue;
                db.enabled = true;
            }
        }

        [HideFromIl2Cpp]
        static void ShowAvatar(GameObject avtrObject, DynamicBoneController dynamicBoneController)
        {
            avtrObject.SetActive(true);

            if (dynamicBoneController == null) return;
            for (int dbIndex = 0; dbIndex < dynamicBoneController.field_Private_List_1_DynamicBone_0.Count; dbIndex++)
            {
                var db = dynamicBoneController.field_Private_List_1_DynamicBone_0.ToArray()[dbIndex];
                if (db.enabled) continue;
                db.enabled = true;
            }
        }

        [HideFromIl2Cpp]
        static Player GetPlayer(VRCAvatarManager avatarManager) => avatarManager.transform.GetParent().GetComponent<Player>();
        
        [HideFromIl2Cpp]
        public static GameObject GetAvatarObject(VRC.Player p) => p.prop_VRCPlayer_0.prop_VRCAvatarManager_0.prop_GameObject_0;
    }
}
