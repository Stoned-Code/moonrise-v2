using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;
using static MoonriseV2Mod.API.MoonriseMultiCast;

namespace MoonriseV2Mod.WorldFunctions
{
    internal class PickupFunctions
    {
        public PickupFunctions(GameObject obj)
        {
            this.pickupObject = obj;

            OnPickupToggle += TogglePickupable;
            OnPickupObjectToggle += TogglePickupObject;
        }

        public GameObject pickupObject;
        public VRC_Pickup pickup => pickupObject.GetComponent<VRC_Pickup>();
        private static List<PickupFunctions> m_pickups = new List<PickupFunctions>();

        public static BoolMultiCast OnPickupToggle;
        public static BoolMultiCast OnPickupObjectToggle;
        public void TogglePickupable(bool state)
        {
            if (pickup != null)
                pickup.pickupable = state;
        }

        public void TogglePickupObject(bool state)
        {

            pickupObject.SetActive(state);
        }

        public static void AddPickup(GameObject pickup)
        {
            if (pickup == PortableMirror.mirror) return;
            m_pickups.Add(new PickupFunctions(pickup.gameObject));
        }

        public static void ClearPickups() => m_pickups.Clear();



        ~PickupFunctions()
        {
            OnPickupToggle -= TogglePickupable;
            OnPickupObjectToggle -= TogglePickupObject;
        }
    }
}
