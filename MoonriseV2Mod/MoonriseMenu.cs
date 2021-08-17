using MoonriseV2Mod.API;
using MoonriseV2Mod.Patches;
using RubyButtonAPI;
using System;

namespace MoonriseV2Mod
{
    public class MoonriseMenu
    {
        public MoonriseMenu()
        {
            QuickMenuPatches.loadMenu += LoadMenu;
            QuickMenuPatches.OnAddonsLoaded += LoadAddonMenu;
            NetworkManagerJoinRoom.OnJoinedRoom += OnJoinedRoom;
            NetworkManagerLeftRoom.OnLeftRoom += OnLeftRoom;
            InitializeMenu();
        }
        public bool isInitialized = false;
        public virtual void InitializeMenu() { }
        public virtual void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user) { }
        public virtual void LoadAddonMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user) { }
        public virtual void OnJoinedRoom() { }
        public virtual void OnLeftRoom() { }
    }
}
