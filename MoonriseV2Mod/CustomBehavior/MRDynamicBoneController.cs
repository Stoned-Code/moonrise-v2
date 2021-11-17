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
        private MRAvatarController avatarController;
        [HideFromIl2Cpp] 
        public MRAvatarController m_avatarController => m_avatarController;
        [HideFromIl2Cpp]
        public DynamicBone[] m_dynamicBones { get; private set; }
        [HideFromIl2Cpp]
        public DynamicBoneCollider[] m_dynamicBoneColliders { get; private set; }

        /// <summary>
        /// Called when dynamic bones and their colliders are loaded on the selected user.
        /// </summary>
        public static event Action<MRDynamicBoneController> OnDynamicBonesLoaded;

        private void Start()
        {
            avatarController = GetComponent<MRAvatarController>();
        }
        [HideFromIl2Cpp]
        public void GetDynamicBoneAndColliders()
        {
            if (avatarController.apiUser == null) return;

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
