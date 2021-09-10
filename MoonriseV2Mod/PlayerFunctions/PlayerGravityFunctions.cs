using MoonriseV2Mod.API;
using MoonriseV2Mod.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoonriseV2Mod.PlayerFunctions
{
    internal class PlayerGravityFunctions
    {
        public static void SetGravity(float gravity)
        {
            PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetGravityStrength(gravity);
        }

        public static void ResetGravity() => PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetGravityStrength(VRCPlayerApiJumpImpulse.m_defaultGravity);
    }
}
