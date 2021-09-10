using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MoonriseV2Mod.API;
using MoonriseV2Mod.MonoBehaviourScripts;
using MoonriseV2Mod.PlayerFunctions;
using Newtonsoft.Json;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;

namespace MoonriseV2Mod.Patches
{
    [HarmonyPatch(typeof(VRCPlayer), "Start")]
    class VRCPlayerAwake
    {
        //static void Postfix(VRCPlayer __instance)
        //{
        //    if (__instance == null) return;
        //    if (__instance.prop_Player_0.field_Private_APIUser_0.id != APIUser.CurrentUser.id) return;

        //    PlayerPhysics.GetDefaultPhysics();
        //}
    }

    [HarmonyPatch(typeof(VRCPlayerApi), "SetGravityStrength")]
    internal class VRCPlayerApiGravityStrength
    {
        public static bool retrievedGravity = false;
        private static float defaultGravity;
        public static float m_defaultGravity
        {
            get => defaultGravity;
        }

        static void Postfix(VRCPlayerApi __instance, float strength)
        {
            if (!__instance.isLocal) return;
            if (retrievedGravity) return;

            defaultGravity = strength;
            retrievedGravity = true;
            //MoonriseConsole.Log("Retrieved gravity strength: " + strength);
        }
    }

    [HarmonyPatch(typeof(VRCPlayerApi), "SetWalkSpeed")]
    internal class VRCPlayerApiWalkSpeed
    {
        public static bool retrievedSpeed = false;
        private static float defaulWalkSpeed;
        public static float m_defaultWalkSpeed
        {
            get => defaulWalkSpeed;
        }

        static void Postfix(VRCPlayerApi __instance, float speed)
        {
            if (!__instance.isLocal) return;
            if (retrievedSpeed) return;

            defaulWalkSpeed = speed;
            retrievedSpeed = true;
            //MoonriseConsole.Log("Retrieved walk speed: " + speed);
        }
    }

    [HarmonyPatch(typeof(VRCPlayerApi), "SetRunSpeed")]
    internal class VRCPlayerApiRunSpeed
    {
        public static bool retrievedSpeed = false;
        private static float defaultRunSpeed;
        public static float m_defaultRunSpeed
        {
            get => defaultRunSpeed;
        }

        static void Postfix(VRCPlayerApi __instance, float speed)
        {
            if (!__instance.isLocal) return;
            if (retrievedSpeed) return;


            defaultRunSpeed = speed;
            retrievedSpeed = true;
            //MoonriseConsole.Log("Retrieved run speed: " + speed);
        }
    }

    [HarmonyPatch(typeof(VRCPlayerApi), "SetJumpImpulse")]
    internal class VRCPlayerApiJumpImpulse
    {
        public static bool retrievedImpulse = false;
        private static float defaultJumpImpulse;
        private static float defaultGravity;
        public static float m_defaultGravity
        {
            get => defaultGravity;
        }

        public static float m_defaultJumpImpulse
        {
            get => defaultJumpImpulse;
        }

        static void Postfix(VRCPlayerApi __instance, float impulse)
        {
            if (!__instance.isLocal) return;
            if (retrievedImpulse) return;

            defaultJumpImpulse = impulse;
            defaultGravity = __instance.GetGravityStrength();
            retrievedImpulse = true;
            //MoonriseConsole.Log("Retrieved jump impulse: " + impulse);
        }
    }

    [HarmonyPatch(typeof(VRCPlayerApi), "SetStrafeSpeed")]
    internal class VRCPlayerApiStrafeSpeed
    {
        public static bool retrievedSpeed = false;
        private static float defaultStrafeSpeed;
        public static float m_defaultStafeSpeed
        {
            get => defaultStrafeSpeed;
        }

        static void Postfix(VRCPlayerApi __instance, float speed)
        {
            if (!__instance.isLocal) return;
            if (retrievedSpeed) return;

            defaultStrafeSpeed = speed;
            retrievedSpeed = true;
            //MoonriseConsole.Log("Retrieved strafe speed: " + speed);
        }
    }

    internal class VRCPlayerMovement
    {
        public static void LeaveWorld()
        {
            VRCPlayerApiWalkSpeed.retrievedSpeed = false;
            VRCPlayerApiRunSpeed.retrievedSpeed = false;
            VRCPlayerApiStrafeSpeed.retrievedSpeed = false;
            VRCPlayerApiGravityStrength.retrievedGravity = false;
            VRCPlayerApiJumpImpulse.retrievedImpulse = false;
        }
    }




}
