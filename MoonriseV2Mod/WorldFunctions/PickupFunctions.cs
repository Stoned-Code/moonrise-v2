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

        public static void ClearPickups()
        {
            for (int i = m_pickups.Count - 1; i > -1; i--)
            {
                if (m_pickups[i].pickup == null)
                    m_pickups.RemoveAt(i);
            }
        }

        public static void AddPickup(VRC_Pickup pickup)
        {
            for (int i = 0; i < m_pickups.Count; i++)
            {
                if (pickup == m_pickups[i].pickup) return;
            }

            m_pickups.Add(new MRPickup(pickup.gameObject));


        }

        public static void InvokePickupEnable(bool toggle) => OnEnablePickups?.Invoke(toggle);
        public static void InvokePickupObjectToggle(bool toggle) => OnToggleObjectPickups?.Invoke(toggle);
    }

    internal class MRPickup
    {
        public MRPickup(GameObject obj)
        {
            this.pickupObject = obj;
            this.pickup = pickupObject.GetComponent<VRC_Pickup>();

            PickupFunctions.OnEnablePickups += OnPickupEnabled;
            PickupFunctions.OnToggleObjectPickups += OnPickupObjectToggled;

            MoonriseConsole.Log("Added " + pickupObject.name + " to pickup list.");
        }

        public GameObject pickupObject { get; set; }
        public VRC_Pickup pickup { get; set; }

        public void OnPickupEnabled(bool toggle)
        {
            pickup.pickupable = toggle;
        }

        public void OnPickupObjectToggled(bool toggle)
        {
            pickupObject.SetActive(toggle);
        }

        //~MRPickup()
        //{
        //    PickupFunctions.OnEnablePickups -= OnPickupEnabled;
        //    PickupFunctions.OnToggleObjectPickups -= OnPickupObjectToggled;
        //}
    }
}
