using MoonriseV2Mod.API;
using MoonriseV2Mod.SocialInterractions;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using VRC;
using VRC.Core;

namespace MoonriseV2Mod.BaseFunctions
{
    public class PlayerTeleportMenu
    {
        public static bool isInitialized = false;
        public static QMNestedButton playerTeleportMenu;
        private static List<PlayerTP> teleportButtons;
        private static UshioMultiPageNested teleportButtonsMenu;
        public static void Initialize(QMNestedButton functions)
        {
            playerTeleportMenu = new QMNestedButton(functions, 3, 2, "Player\nTeleport", "Opens a menu with buttons that teleport you to players.");

            VXNoaW9SdWJ5TW9kaWZpZXJz.AddNestedAction(playerTeleportMenu, PlayerTeleportMenu.BuildMenu);
            teleportButtons = new List<PlayerTP>();
            teleportButtonsMenu = new UshioMultiPageNested(playerTeleportMenu, 1);
            isInitialized = true;
        }

        public static void BuildMenu()
        {
            try
            {
                if (teleportButtonsMenu != null) teleportButtonsMenu.ClearElements();

                teleportButtons.Clear();

                var players = PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0;

                int buttonPos = 0;
                int meshPageNumber = 1;

                for (int i = 0; i < players.Length; i++)
                {
                    var player = players[i];
                    var apiUser = player.prop_APIUser_0;
                    string displayName = apiUser.displayName ?? "";
                    string userId = apiUser.id ?? "";

                    if (userId == APIUser.CurrentUser.id) continue;

                    if (buttonPos == 0)
                    {

                        var playerButton = new QMSingleButton(playerTeleportMenu, 1, 0, displayName, delegate
                        {
                            try
                            {
                                PlayerTeleport.TeleportTo(userId);
                            }
                            catch (Exception ex)
                            {
                                MoonriseConsole.Log($"Failed to teleport...\n{ex}");
                            }
                        }, $"Teleport to {displayName}");

                        teleportButtons.Add(new PlayerTP(playerButton, meshPageNumber, apiUser.id));
                        buttonPos++;
                    }

                    else if (buttonPos == 1)
                    {
                        var playerButton = new QMSingleButton(playerTeleportMenu, 2, 0, displayName, delegate
                        {
                            try
                            {
                                PlayerTeleport.TeleportTo(userId);
                            }
                            catch (Exception ex)
                            {
                                MoonriseConsole.Log($"Failed to teleport...\n{ex}");
                            }
                        }, $"Teleport to {displayName}");

                        teleportButtons.Add(new PlayerTP(playerButton, meshPageNumber, apiUser.id));
                        
                        buttonPos++;
                    }

                    else if (buttonPos == 2)
                    {
                        var playerButton = new QMSingleButton(playerTeleportMenu, 3, 0, displayName, delegate
                        {
                            try
                            {
                                PlayerTeleport.TeleportTo(userId);
                            }
                            catch (Exception ex)
                            {
                                MoonriseConsole.Log($"Failed to teleport...\n{ex}");
                            }
                        }, $"Teleport to {displayName}");

                        teleportButtons.Add(new PlayerTP(playerButton, meshPageNumber, apiUser.id));
                        
                        buttonPos++;
                    }

                    else if (buttonPos == 3)
                    {
                        var playerButton = new QMSingleButton(playerTeleportMenu, 1, 1, displayName, delegate
                        {
                            try
                            {
                                PlayerTeleport.TeleportTo(userId);
                            }
                            catch (Exception ex)
                            {
                                MoonriseConsole.Log($"Failed to teleport...\n{ex}");
                            }
                        }, $"Teleport to {displayName}");

                        teleportButtons.Add(new PlayerTP(playerButton, meshPageNumber, apiUser.id));
                        
                        buttonPos++;
                    }

                    else if (buttonPos == 4)
                    {
                        var playerButton = new QMSingleButton(playerTeleportMenu, 2, 1, displayName, delegate
                        {
                            try
                            {
                                PlayerTeleport.TeleportTo(userId);
                            }
                            catch (Exception ex)
                            {
                                MoonriseConsole.Log($"Failed to teleport...\n{ex}");
                            }
                        }, $"Teleport to {displayName}");

                        teleportButtons.Add(new PlayerTP(playerButton, meshPageNumber, apiUser.id));
                        
                        buttonPos++;
                    }

                    else if (buttonPos == 5)
                    {
                        var playerButton = new QMSingleButton(playerTeleportMenu, 3, 1, displayName, delegate
                        {
                            try
                            {
                                PlayerTeleport.TeleportTo(userId);
                            }
                            catch (Exception ex)
                            {
                                MoonriseConsole.Log($"Failed to teleport...\n{ex}");
                            }
                        }, $"Teleport to {displayName}");

                        teleportButtons.Add(new PlayerTP(playerButton, meshPageNumber, apiUser.id));
                        
                        buttonPos++;
                    }

                    else if (buttonPos == 6)
                    {
                        var playerButton = new QMSingleButton(playerTeleportMenu, 1, 2, displayName, delegate
                        {
                            try
                            {
                                PlayerTeleport.TeleportTo(userId);
                            }
                            catch (Exception ex)
                            {
                                MoonriseConsole.Log($"Failed to teleport...\n{ex}");
                            }
                        }, $"Teleport to {displayName}");

                        teleportButtons.Add(new PlayerTP(playerButton, meshPageNumber, apiUser.id));
                        
                        buttonPos++;
                    }

                    else if (buttonPos == 7)
                    {
                        var playerButton = new QMSingleButton(playerTeleportMenu, 2, 2, displayName, delegate
                        {
                            try
                            {
                                PlayerTeleport.TeleportTo(userId);
                            }
                            catch (Exception ex)
                            {
                                MoonriseConsole.Log($"Failed to teleport...\n{ex}");
                            }
                        }, $"Teleport to {displayName}");

                        teleportButtons.Add(new PlayerTP(playerButton, meshPageNumber, apiUser.id));
                        
                        buttonPos++;
                    }

                    else if (buttonPos == 8)
                    {
                        var playerButton = new QMSingleButton(playerTeleportMenu, 3, 2, displayName, delegate
                        {
                            try
                            {
                                PlayerTeleport.TeleportTo(userId);
                            }
                            catch (Exception ex)
                            {
                                MoonriseConsole.Log($"Failed to teleport...\n{ex}");
                            }
                        }, $"Teleport to {displayName}");

                        teleportButtons.Add(new PlayerTP(playerButton, meshPageNumber, apiUser.id));
                        
                        buttonPos = 0;
                        meshPageNumber++;
                    }
                }

                if (teleportButtonsMenu == null) teleportButtonsMenu = new UshioMultiPageNested(playerTeleportMenu, meshPageNumber);

                else
                {
                    teleportButtonsMenu.SetPageAmount(meshPageNumber);
                }

                for (int i = 0; i < teleportButtons.Count; i++)
                {
                    var tpButton = teleportButtons[i];
                    teleportButtonsMenu.AddElementToMenu(tpButton.TeleportButton, tpButton.PageNumber);
                }

                teleportButtonsMenu.SetPage();
            }

            catch (Exception ex)
            {
                MoonriseConsole.ErrorLog($"Failed to load Player Teleport Menu...\n{ex}");
            }
        }
    }

    public class PlayerTP
    {
        public PlayerTP(QMSingleButton tpButton,int pageNumber, string userId)
        {
            TeleportButton = tpButton;
            UserId = userId;
            PageNumber = pageNumber;
        }
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public QMSingleButton TeleportButton { get; set; }
    }
}
