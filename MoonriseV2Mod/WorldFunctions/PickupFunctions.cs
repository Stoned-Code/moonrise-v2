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
        private static List<MRPickup> m_pickups = new List<MRPickup>();
        public static BoolMultiCast OnEnablePickups;
        public static BoolMultiCast OnToggleObjectPickups;
        public static void GetPickups()
        {
            if (m_pickups != null)
                m_pickups.Clear();

            var tempPickups = GameObject.FindObjectsOfType<VRC_Pickup>();

            for (int i = 0; i < tempPickups.Length; i++)
            {
                m_pickups.Add(new MRPickup(tempPickups[i].gameObject));
            }

            MoonriseConsole.Log("Retrieved pickups.");
        }

        public static void InvokePickupEnable(bool toggle) => OnEnablePickups?.Invoke(toggle);
        public static void InvokePickupObjectToggle(bool toggle) => OnToggleObjectPickups?.Invoke(toggle);
    }

    internal class MRPickup
    {
        public MRPickup(GameObject obj)
        {
            this.pickupObject = obj;

            PickupFunctions.OnEnablePickups += OnPickupEnabled;
            PickupFunctions.OnToggleObjectPickups += OnPickupObjectToggled;
        }

        public GameObject pickupObject { get; set; }
        public VRC_Pickup pickup => pickupObject.GetComponent<VRC_Pickup>();

        public void OnPickupEnabled(bool toggle)
        {
            pickup.pickupable = toggle;
        }

        public void OnPickupObjectToggled(bool toggle)
        {
            pickupObject.SetActive(toggle);
        }

        ~MRPickup()
        {
            PickupFunctions.OnEnablePickups -= OnPickupEnabled;
            PickupFunctions.OnToggleObjectPickups -= OnPickupObjectToggled;
        }
    }
}
