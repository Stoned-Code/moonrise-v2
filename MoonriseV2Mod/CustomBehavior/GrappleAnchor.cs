using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using UnhollowerBaseLib.Attributes;
using MoonriseV2Mod.ActionMenu;
using MoonriseV2Mod.API;

namespace MoonriseV2Mod.CustomBehavior
{
    [RegisterTypeInIl2Cpp]
    public class GrappleAnchor : MonoBehaviour
    {
        public GrappleAnchor(IntPtr ptr) : base(ptr) { }

        private Transform target;
        private LineRenderer line;
        private bool initialized;
        private void Start()
        {
            line = GetComponentInChildren<LineRenderer>();
        }

        private void Update()
        {
            if (!initialized) return;

            float distance = Vector3.Distance(line.transform.position, target.position);
            if (distance < 2.5f) Destroy(gameObject);

            Vector3 direction = line.transform.position - target.position;
            direction = direction.normalized;

            PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetVelocity(direction * 20);

            line.SetPosition(0, line.transform.position);
            line.SetPosition(1, target.position);
        }

        [HideFromIl2Cpp]
        public void Initialize(Transform t)
        {
            target = t;
            initialized = true;
        }

        [HideFromIl2Cpp]
        public void RemoveAnchor()
        {
            var direction = line.transform.position - target.position;
            direction = direction.normalized;

            direction = direction + Vector3.up;
            direction = direction / 2f;

            PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetVelocity(direction * 15f);

            Destroy(gameObject);
        }
    }
}
