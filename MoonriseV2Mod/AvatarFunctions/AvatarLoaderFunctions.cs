using MoonriseV2Mod.MonoBehaviourScripts;
using MoonriseV2Mod.Patches;
using UnityEngine;

namespace MoonriseV2Mod.AvatarFunctions
{
    public class AvatarLoaderFunctions
    {
        public static void Initialize()
        {
            VRCAvatarManagerLoadedPatch.OnAvatarLoaded += OnAvatarLoaded;
        }

        private static void OnAvatarLoaded(VRCAvatarManager avatarManager, MRAvatarController hider, GameObject avatarObject, string avatarId)
        {
            // MoonriseConsole.Log(avatarManager.prop_ApiAvatar_0.name);
            hider.dynamicBoneController.GetDynamicBoneAndColliders();
        }
    }
}
