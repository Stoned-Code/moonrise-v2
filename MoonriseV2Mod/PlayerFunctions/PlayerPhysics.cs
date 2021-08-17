using MelonLoader;
using MoonriseV2Mod.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;

namespace MoonriseV2Mod.PlayerFunctions
{
    public class PlayerPhysics
    {
        public static float defaultWalkSpeed;
        public static float defaultRunSpeed;
        public static float defaultStrafeSpeed;
        public static float defaultJumpImpulse;
        public static float defaultGravityStrength;

        public static void ResetAllPhysics()
        {
            PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetRunSpeed(defaultRunSpeed);
            PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetWalkSpeed(defaultWalkSpeed);
            PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetStrafeSpeed(defaultStrafeSpeed);
            PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetJumpImpulse(defaultJumpImpulse);
            PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetGravityStrength(defaultGravityStrength);
        }

        public static void GetDefaultPhysics() => MelonCoroutines.Start(GetPhysics());
        private static IEnumerator GetPhysics()
        {
            while (VRCPlayer.field_Internal_Static_VRCPlayer_0 == null) yield return null;
            while (Player.prop_Player_0.prop_APIUser_0 == null) yield return null;
            while (RoomManager.field_Private_Static_RoomManager_0 == null) yield return null;
            while (PlayerCheck.LocalVRCPlayer.field_Private_VRCPlayerApi_0 == null) yield return null;

            try
            {
                defaultRunSpeed = PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.GetRunSpeed();
                defaultWalkSpeed = PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.GetWalkSpeed();
                defaultStrafeSpeed = PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.GetStrafeSpeed();
                defaultJumpImpulse = PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.GetJumpImpulse();
                defaultGravityStrength = PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.GetGravityStrength();

                string physicsInfo = "Successfully Retrieved default physics values!";
                physicsInfo += $"\nWalk Speed: {defaultWalkSpeed}";
                physicsInfo += $"\nRun Speed: {defaultRunSpeed}";
                physicsInfo += $"\nStrafe Speed: {defaultStrafeSpeed}";
                physicsInfo += $"\nJump Impulse: {defaultJumpImpulse}";
                physicsInfo += $"\nGravity Strength: {defaultGravityStrength}";
                MoonriseConsole.Log(physicsInfo);
                
                yield break;
            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog($"Failed to get default physics values...\n{ex}");
            }
        }
    }
}
