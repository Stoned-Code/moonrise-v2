using MoonriseV2Mod.API;
using MoonriseV2Mod.Patches;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UshioUI;
using VRC.SDKBase;
using static MoonriseV2Mod.Patches.VRCPlayerMovement;

namespace MoonriseV2Mod.PlayerFunctions
{
    internal sealed class PlayerFunctionsBase : MoonriseMenu
    {
        public static PlayerFunctionsBase playerFunctions;

        public static void Initialize()
        {
            playerFunctions = new PlayerFunctionsBase();
        }

        public QMNestedButton playerFunctionsMenu;
        public UshioMenuSlider speedSlider;

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            playerFunctionsMenu = new QMNestedButton(functions, 3, 1, "Player\nFunctions", "Useful player functions.");

            var enableJump = new QMSingleButton(playerFunctionsMenu, 1, 0, "Enable\nJump", delegate
            {
                var jumpEnabled = PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.GetJumpImpulse() > 0;
                if (!jumpEnabled)
                    PlayerCheck.LocalVRCPlayer.field_Private_VRCPlayerApi_0.SetJumpImpulse(5f);
            }, "Enables jumping if the world doesn't have jumping enabled for some dumb reason.");


            speedSlider = new UshioMenuSlider(UshioMenuSlider.SliderPosition.RightofMenu, "Speed Multiplier: ", playerFunctionsMenu, 0.1f, 10f, 1f, (value) =>
            {
                try
                {
                    PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetWalkSpeed(VRCPlayerApiWalkSpeed.m_defaultWalkSpeed * value);
                    PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetRunSpeed(VRCPlayerApiRunSpeed.m_defaultRunSpeed * value);
                    PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetStrafeSpeed(VRCPlayerApiStrafeSpeed.m_defaultStafeSpeed * value);
                }

                catch { }
            });

            var resetSpeed = new QMSingleButton(playerFunctionsMenu, 5, -1, "Reset\nSpeed", () =>
            {
                PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetWalkSpeed(VRCPlayerApiWalkSpeed.m_defaultWalkSpeed);
                PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.SetRunSpeed(VRCPlayerApiRunSpeed.m_defaultRunSpeed);
                speedSlider.GetSlider().value = 1f;
            }, "Reset's your movement speed.");
        }

        public override void OnLeftRoom()
        {
            VRCPlayerMovement.LeaveWorld();
            speedSlider.GetSlider().value = 1f;
        }
    }
}
