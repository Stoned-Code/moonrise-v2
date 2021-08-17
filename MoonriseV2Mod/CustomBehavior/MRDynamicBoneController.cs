using MelonLoader;
using MoonriseV2Mod.MonoBehaviourScripts;
using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace MoonriseV2Mod.CustomBehavior
{
    [RegisterTypeInIl2Cpp]
    public class MRDynamicBoneController : MonoBehaviour
    {
        public MRDynamicBoneController(IntPtr ptr) : base(ptr) { }
        private MRAvatarController m_avatarController;
        [HideFromIl2Cpp] 
        public MRAvatarController avatarController => m_avatarController;
        public DynamicBone[] m_dynamicBones;
        public DynamicBoneCollider[] m_dynamicBoneColliders;

        /// <summary>
        /// Called when dynamic bones and their colliders are loaded on the selected user.
        /// </summary>
        public static event Action<MRDynamicBoneController> OnDynamicBonesLoaded;

        private void Start()
        {
            m_avatarController = GetComponent<MRAvatarController>();
        }
        [HideFromIl2Cpp]
        public void GetDynamicBoneAndColliders()
        {
            if (m_avatarController.apiUser == null) return;

            m_dynamicBones = GetComponentsInChildren<DynamicBone>();
            m_dynamicBoneColliders = GetComponentsInChildren<DynamicBoneCollider>();

            OnDynamicBonesLoaded?.Invoke(this);
        }

        [HideFromIl2Cpp]
        public void SetDynamicBoneAndColliders(DynamicBone[] dynamicBones, DynamicBoneCollider[] dynamicBoneColliders)
        {

        }
    }
}
