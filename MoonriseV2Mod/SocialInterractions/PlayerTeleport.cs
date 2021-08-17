using MoonriseV2Mod.API;
using UshioUI;
using VRC;
using VRC.Core;

namespace MoonriseV2Mod.SocialInterractions
{
    // <summary>
    // Teleport Player to Another Player
    // </summary>
    internal class PlayerTeleport
    {
        public static void TeleportTo(string SelectedUserID)
        {
            try
            {
                VRCPlayer player = PlayerCheck.LocalVRCPlayer;
                Player foundPlayer = PlayerCheck.GetSelectedPlayer(SelectedUserID);

                if (APIUser.CurrentUser.id != foundPlayer.prop_APIUser_0.id)
                {
                    var pos = foundPlayer.prop_VRCPlayer_0.transform.position - foundPlayer.prop_VRCPlayer_0.transform.forward;
                    player.field_Private_VRCPlayerApi_0.TeleportTo(pos, foundPlayer.prop_VRCPlayer_0.transform.rotation);

                    //player.field_Internal_VRCPlayer_0.transform.rotation = foundPlayer.field_Internal_VRCPlayer_0.transform.rotation;
                }

                else
                {
                    UshioMenuApi.CreateSingleButtonPopup("Teleport Error", "You Can't Teleport To Yourself!", "OK", delegate
                    {
                        UshioMenuApi.ClosePopup();
                    });
                }
            }

            catch
            {
                MoonriseConsole.ErrorLog("Player is not in your world!");
                UshioMenuApi.CreateSingleButtonPopup("Something's Wrong Here...", "You can't teleport to a player that isn't in your world!", "OK", delegate
                {
                    UshioMenuApi.ClosePopup();
                });
            }
        }
    }
}


