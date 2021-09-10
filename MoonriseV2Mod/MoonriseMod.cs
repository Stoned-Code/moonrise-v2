using MelonLoader;
using MoonriseApi;
using MoonriseV2Mod.API;
using MoonriseV2Mod.BaseFunctions;
using MoonriseV2Mod.Patches;
using RubyButtonAPI;
using System.Linq;
using System.Reflection;
using VRC;

namespace MoonriseV2Mod
{
    /// <summary>
    /// Used for making addon mods
    /// </summary>
    public abstract class MoonriseMod : MelonMod
    {
        public MoonriseMod() : base() { }
        private void Initialize()
        {
            JoinNotifierFunctions.PlayerJoined += OnPlayerJoin;
            JoinNotifierFunctions.PlayerLeft += OnPlayerLeave;

            QuickMenuPatches.OnAddonsLoaded += OnAddonMenuLoaded;
            NetworkManagerJoinRoom.OnJoinedRoom += OnJoinRoom;
            NetworkManagerLeftRoom.OnLeftRoom += OnLeaveRoom;
        }

        private QMNestedButton addonButton;
        private HarmonyLib.Harmony m_harmonyInstance;

        private string AddonName;
        private string AddonAuthor;
        private string AddonDescription;

        public QMNestedButton addonNested => addonButton;

        public sealed override void OnApplicationStart()
        {
            CustomAttributeData moonriseAttribute = Assembly.CustomAttributes.First(a => a.AttributeType.Name == "MoonriseAddonAttribute");
            if (moonriseAttribute == null)
            {
                MoonriseConsole.ErrorLog("Missing moonrise addon attribute...");
                return;
            }
            Initialize();

            string addonName = moonriseAttribute.ConstructorArguments[0].Value as string;
            string author = moonriseAttribute.ConstructorArguments[2].Value as string;

            addonName = addonName.Replace(" ", "");
            author = author.Replace(" ", "");
            m_harmonyInstance = new HarmonyLib.Harmony($"com.{author}.{addonName}");
            OnVRCStart(m_harmonyInstance);
        }

        /// <summary>
        /// Makes sure to set addon name with "SetAddonInfo()".
        /// <param name="harmonyInstance">The harmony instance created when the mod loaded.</param>
        /// </summary>
        public virtual void OnVRCStart(HarmonyLib.Harmony harmonyInstance) { }

        /// <summary>
        /// Called when the local player joins the room.
        /// </summary>
        public virtual void OnJoinRoom() { }

        /// <summary>
        /// Called when the local player leaves the room.
        /// </summary>
        public virtual void OnLeaveRoom() { }

        /// <summary>
        /// Called when a players joins the world.
        /// Only called when Moonrise's join notifier is enabled.
        /// </summary>
        /// <param name="player">The person that joined the lobby.</param>
        /// <param name="isFriend">If the player that joined is a friend.</param>
        /// <param name="allJN"></param>
        public virtual void OnPlayerJoin(Player player, bool isFriend, bool allJN) { }

        /// <summary>
        /// Called when a player leaves the world.
        /// Only called when Moonrise's join notifier is enabled.
        /// </summary>
        /// <param name="player">The person that left the lobby.</param>
        /// <param name="isFriend">If the player that left is a friend.</param>
        /// <param name="allJN"></param>
        public virtual void OnPlayerLeave(Player player, bool isFriend, bool allJN) { }

        /// <summary>
        /// Called when The addon menu is finished initializing.
        /// </summary>
        /// <param name="addonNested">The nested button in the addon menu.</param>
        public virtual void OnMenuLoaded(QMNestedButton addonNested) { }

        /// <summary>
        /// Sets the name and tooltip in the addon menu. (Use in OnVRCStart() Method)
        /// </summary>
        /// <param name="buttonName">The name to be displayed on the button in the moonrise addons menu.</param>
        /// <param name="addonAuthor">The name of addon author</param>
        /// <param name="addonDescription">The discription shown in the tooltips of the addon button.</param>
        public void SetAddonInfo(string buttonName, string addonAuthor, string addonDescription)
        {
            addonAuthor = addonAuthor.Replace(" ", "");
            AddonName = buttonName;
            AddonAuthor = addonAuthor;
            AddonDescription = addonDescription;
        }

        private void OnAddonMenuLoaded(QMNestedButton functions, QMNestedButton socialFunctions, TVJVc2Vy user)
        {
            addonButton = AddonMods.MakeAddonNestedButton(AddonName, AddonDescription);
            OnMenuLoaded(addonButton);
        }
    }
}
