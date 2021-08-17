using MoonriseV2Mod.CustomBehavior;

namespace MoonriseV2Mod.AvatarFunctions
{
    public class DynamicBoneFunctions
    {
        public static void Initialize() => MRDynamicBoneController.OnDynamicBonesLoaded += OnDynamicBonesLoaded;

        /// <summary>
        /// Called when a player's dynamic bones are finished loading.
        /// </summary>
        /// <param name="controller">The dynamic bone controller of the player that loaded.</param>
        public static void OnDynamicBonesLoaded(MRDynamicBoneController controller)
        {

        }
    }
}
