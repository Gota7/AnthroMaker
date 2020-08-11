using ImGuiNET;
using ImGuiUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Num = System.Numerics;

namespace AnthroMaker {

    /// <summary>
    /// Main scene.
    /// </summary>
    public class MainScene : Scene {

        /// <summary>
        /// ImGui renderer.
        /// </summary>
        private ImGuiRenderer ImGuiRenderer;

        /// <summary>
        /// Wallpapapers.
        /// </summary>
        Wallpapers Wallpapers;

        /// <summary>
        /// Initialize the scene.
        /// </summary>
        public override void Initialize() {

            //Wallpapers.
            Wallpapers = new Wallpapers();

            //Allow resizing.
            Helper.Game.Window.AllowUserResizing = true;
            Helper.Game.IsMouseVisible = true;

            //Initialize renderer.
            ImGuiRenderer = new ImGuiRenderer(Helper.Game);
            Helper.ImGuiRenderer = ImGuiRenderer;

            //Set up style.
            if (File.Exists("Res/Themes/" + Helper.Settings.Theme + ".ini")) {
                Style.LoadFromFile(Helper.Settings.Theme);
            } else {
                Style.LoadFromFile(Style.ThemeList[0]);
            }

            //Set up the window manager.
            WindowManager.Renderer = ImGuiRenderer;
            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;

        }
        
        /// <summary>
        /// Draw the scene.
        /// </summary>
        public override void Draw() {

            //Rebuild fonts.
            if (Style.FontRebuildNeeded) {
                Helper.ImGuiRenderer.RebuildFontAtlas();
                Style.FontRebuildNeeded = false;
            }

            //Wallpapers.
            Wallpapers.Draw();
            Helper.SpriteBatch.End();
            Helper.SpriteBatch.Begin();

            //Run data before layout.
            ImGuiRenderer.BeforeLayout(Helper.Time);

            //Set up font.
            ImGui.PushFont(Style.FontPtr);

            //Debug.
            if (Helper.DebugMode) {
                ImGui.ShowDemoWindow();
            }

            //Main menu.
            DrawMainMenu();

            //Draw windows.
            WindowManager.Draw();

            //Run data after layout.
            ImGuiRenderer.AfterLayout();

        }
        
        /// <summary>
        /// Update the scene.
        /// </summary>
        public override void Update() {

            //Update wallpapers.
            Wallpapers.Update();

            //Update windows.
            WindowManager.Update();

        }

        /// <summary>
        /// Draw the main menu.
        /// </summary>
        public void DrawMainMenu() {

            //Begin menu bar.
            if (ImGui.BeginMainMenuBar()) {

                //Tools.
                if (ImGui.MenuItem("Fursona Maker")) {
                    new FursonaMakerWindow();
                }
                if (ImGui.MenuItem("Coloring Book")) { 
                
                }
                if (ImGui.MenuItem("Part Editor")) {

                }
                if (ImGui.MenuItem("Settings")) {
                    new SettingsWindow();
                }
                if (ImGui.MenuItem("About")) {
                    new AboutWindow();
                }

                //Fullscreen.
                if (ImGui.Button("Fullscreen")) {
                    ToggleFullscreen();
                }

                //Wallpaper stuff.
                if (Helper.Settings.EnableWallpapers && ImGui.Button("Next Wallpaper")) {
                    Wallpapers.FetchNextWallpaper();
                }
                if (Helper.Settings.EnableWallpapers && ImGui.Button("Save Wallpaper")) {
                    Wallpapers.SaveWallpaper();
                }
                string wallpaperPauseText = Wallpapers.Paused ? "Unpause Wallpapers" : "Pause Wallpapers";
                if (Helper.Settings.EnableWallpapers && ImGui.Button(wallpaperPauseText)) {
                    Wallpapers.Paused = !Wallpapers.Paused;
                }

                //End menu bar.
                ImGui.EndMenuBar();

            }

        }

        /// <summary>
        /// Toggle fullscreen.
        /// </summary>
        public void ToggleFullscreen() {

            //Graphics.
            var graphics = Helper.GraphicsDeviceManager;

            //Disable fullscreen if already in fullscreen mode.
            if (graphics.IsFullScreen) {

                //Disable fullscreen, set size to target resolution, and apply changes.
                graphics.IsFullScreen = false;
                Width = Helper.GraphicsDeviceManager.PreferredBackBufferWidth = AnthroMaker.DISPLAY_WIDTH;
                Height = Helper.GraphicsDeviceManager.PreferredBackBufferHeight = AnthroMaker.DISPLAY_HEIGHT;
                graphics.ApplyChanges();

            //Enable fullscreen if not already in fullscreen mode.
            } else {

                //Set fullscreen, set the resolution to the screen, and apply changes.
                graphics.IsFullScreen = true;
                Width = Helper.GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                Height = Helper.GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.ApplyChanges();

            }

        }

    }

}
