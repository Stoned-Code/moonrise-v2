using MoonriseV2Mod.API;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.SDKBase;

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

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            playerFunctionsMenu = new QMNestedButton(functions, 3, 1, "Player\nFunctions", "Useful player functions.");

            var enableJump = new QMSingleButton(playerFunctionsMenu, 1, 0, "Enable\nJump", delegate
            {
                var jumpEnabled = PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.GetJumpImpulse() > 0;
                if (!jumpEnabled)
                    PlayerCheck.LocalVRCPlayer.field_Private_VRCPlayerApi_0.SetJumpImpulse(5f);
            }, "Enables jumping if the world doesn't have jumping enabled for some dumb reason.");

            //var getPhysics = new QMSingleButton(playerFunctionsMenu, 2, 0, "Get\nPhysics", PlayerPhysics.GetDefaultPhysics, "Get's the player's current physics.");
        }
    }
}
