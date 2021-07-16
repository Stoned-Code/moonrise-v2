using MoonriseV2Mod.API;
using MoonriseV2Mod.Settings;
using System;
using System.Collections;
using UnityEngine;
using VRC;
using VRC.Core;

namespace MoonriseV2Mod.AvatarFunctions
{
    public class VVZoYWFHUkhSbmxUUjJ4cldsaEpQUT09
    {
        public static bool m_IgnoreFriends = true;
        public static bool m_HideAvatars = true;

        public static GameObject GetAvatarObject(VRC.Player p) => p.prop_VRCPlayer_0.prop_VRCAvatarManager_0.prop_GameObject_0;

        public static void UnhideAvatars()
        {
            m_HideAvatars = false;
            m_IgnoreFriends = false;

            try
            {
                UnhollowerBaseLib.Il2CppReferenceArray<Player> list = PlayerManager.Method_Public_Static_ArrayOf_Player_0();
                for (int i = 0; i < list.Count; i++)
                {
                    Player player = list[i];
                    if (player == null || VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.IsMe(player.prop_APIUser_0.id)) continue;

                    GameObject avtrObject = GetAvatarObject(player);
                    if (avtrObject == null || avtrObject.active) continue;

                    avtrObject.SetActive(true);
                }
            }

            catch (Exception ex)
            {
                TW9vbnJpc2VDb25zb2xl.ErrorLog($"Failed to unhide avatar...\n{ex}");
            }
        }

        public static IEnumerator ResetHideDistantAvatars()
        {
            var tempDistantAvatarState = Q29uZmln.config.avatarHiderState;
            m_HideAvatars = false;
            m_IgnoreFriends = false;
            Q29uZmln.config.avatarHiderState = 0;
            yield return new WaitForSeconds(0.25f);
            Q29uZmln.config.avatarHiderState = tempDistantAvatarState;
            yield return new WaitForSeconds(0.25f);

            if (Q29uZmln.config.avatarHiderState == 1)
            {
                m_HideAvatars = true;
                m_IgnoreFriends = true;
                //MelonCoroutines.Start(HideAvatars());
            }

            else if (Q29uZmln.config.avatarHiderState == 2)
            {
                m_HideAvatars = true;
                m_IgnoreFriends = false;
                //MelonCoroutines.Start(HideAvatars());
            }

            yield break;
        }

        public static void AvatarHiderUpdate()
        {
            try
            {
                if (Q29uZmln.config.avatarHiderState == 1 || Q29uZmln.config.avatarHiderState == 2)
                {
                    foreach (VRC.Player player in PlayerManager.field_Private_Static_PlayerManager_0.prop_ArrayOf_Player_0)
                    {
                        try
                        {
                            if (player == null || VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.IsMe(player.prop_APIUser_0.id)) continue;

                            APIUser apiUser = player.prop_APIUser_0;
                            string displayName;
                            bool isIgnored = Q29uZmln.config.ignoreList.TryGetValue(apiUser.id, out displayName);
                            GameObject avtrObject = GetAvatarObject(player);
                            DynamicBoneController dynamicBoneController = VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.GetDynamicBoneController(player);
                            if (isIgnored)
                            {
                                if (!avtrObject.activeInHierarchy)
                                    avtrObject.SetActive(true);

                                if (dynamicBoneController != null)
                                {
                                    for (int dbIndex = 0; dbIndex < dynamicBoneController.field_Private_List_1_DynamicBone_0.Count; dbIndex++)
                                    {
                                        var db = dynamicBoneController.field_Private_List_1_DynamicBone_0.ToArray()[dbIndex];
                                        if (db.enabled) continue;
                                        db.enabled = true;
                                    }
                                }
                                continue;
                            }

                            if (avtrObject == null) continue;
                            if (VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.IsFriendsWith(apiUser.id) && m_IgnoreFriends) continue;
                            float dist = Vector3.Distance(VlVkNGFHVlhWbmxSTW1oc1dUSnpQUT09.LocalVRCPlayer.transform.position, avtrObject.transform.position);

                            if (dist > Q29uZmln.config.avatarHiderDistance && m_HideAvatars)
                            {
                                if (dynamicBoneController != null)
                                {
                                    for (int dbIndex = 0; dbIndex < dynamicBoneController.field_Private_List_1_DynamicBone_0.Count; dbIndex++)
                                    {
                                        var db = dynamicBoneController.field_Private_List_1_DynamicBone_0.ToArray()[dbIndex];
                                        if (!db.enabled) continue;
                                        db.enabled = false;
                                    }
                                }

                                avtrObject.SetActive(false);
                            }

                            else if (dist <= Q29uZmln.config.avatarHiderDistance && m_HideAvatars)
                            {
                                avtrObject.SetActive(true);

                                if (dynamicBoneController != null)
                                {
                                    for (int dbIndex = 0; dbIndex < dynamicBoneController.field_Private_List_1_DynamicBone_0.Count; dbIndex++)
                                    {
                                        var db = dynamicBoneController.field_Private_List_1_DynamicBone_0.ToArray()[dbIndex];
                                        if (db.enabled) continue;
                                        db.enabled = true;
                                    }
                                }
                            }

                            else if (!m_HideAvatars)
                            {
                                avtrObject.SetActive(true);

                                if (dynamicBoneController != null)
                                {
                                    for (int dbIndex = 0; dbIndex < dynamicBoneController.field_Private_List_1_DynamicBone_0.Count; dbIndex++)
                                    {
                                        var db = dynamicBoneController.field_Private_List_1_DynamicBone_0.ToArray()[dbIndex];
                                        if (db.enabled) continue;
                                        db.enabled = true;
                                    }
                                }
                            }
                        }

                        catch (Exception)
                        {
                            TW9vbnJpc2VDb25zb2xl.Log($"Failed to scan avatar: {player.prop_APIUser_0.displayName}");
                        }
                    }
                }

                else
                {
                    if (!Q29uZmln.config.avatarsShowing) UnhideAvatars();
                }
            }

            catch
            {
            }
        }
    }
}
