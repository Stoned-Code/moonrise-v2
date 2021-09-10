using MoonriseV2Mod.API;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoonriseV2Mod.Patches;

namespace MoonriseV2Mod.WorldFunctions
{
    internal sealed class WorldFunctionsBase : MoonriseMenu
    {
        public static WorldFunctionsBase worldFunctions;

        public static void Initialize()
        {
            worldFunctions = new WorldFunctionsBase();
        }

        public QMNestedButton worldFunctionsMenu;
        //private QMToggleButton pickupsPickupable;
        //private QMToggleButton pickupsActive;

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            worldFunctionsMenu = new QMNestedButton(functions, 4, 1, "World\nFunctions", "Useful world functions.");

            //pickupsPickupable = new QMToggleButton(worldFunctionsMenu, 1, 0, "Grabable\nPickups", delegate
            //{
            //    PickupFunctions.InvokePickupEnable(true);
            //    //PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.EnablePickups(true);
            //}, "Disabled", delegate
            //{
            //    PickupFunctions.InvokePickupEnable(false);
            //    //PlayerCheck.LocalVRCPlayer.prop_VRCPlayerApi_0.EnablePickups(false);
            //}, "Toggles if pickups are grabbable.", null, null, false, true);

            //pickupsActive = new QMToggleButton(worldFunctionsMenu, 2, 0, "Pickups\nActive", delegate
            //{
            //    PickupFunctions.InvokePickupObjectToggle(true);
            //}, "Disabled", delegate
            //{
            //    PickupFunctions.InvokePickupObjectToggle(false);
            //}, "Sets pickups active.", null, null, false, true);

            var copyVideoUrl = new QMSingleButton(worldFunctionsMenu, 1, 0, "Copy\nVideo\nUrl", VideoPlayerFunctions.CopyVideoUrl, "Copies the URL of the video that's playing.");

            var openUrl = new QMSingleButton(worldFunctionsMenu, 2, 0, "Open\nURL In\nBrowser", VideoPlayerFunctions.OpenVideoUrlInBrowser, "Opens the video url in your default browser.");
        }

        public override void OnJoinedRoom()
        {
            //pickupsPickupable.setToggleState(true, false);
            //pickupsActive.setToggleState(true, false);
            //PickupFunctions.ClearPickups();
        }

        public override void OnLeftRoom()
        {
        }
    }
}
