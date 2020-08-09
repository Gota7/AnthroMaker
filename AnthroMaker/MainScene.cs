using ImGuiNET;
using ImGuiUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        /// Font.
        /// </summary>
        ImFontPtr Font;

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
            Font = ImGui.GetIO().Fonts.AddFontFromFileTTF("Res/Delfino.ttf", 14.0f);
            ImGuiRenderer.RebuildFontAtlas();
            Helper.ImGuiRenderer = ImGuiRenderer;

            //Set up style.
            var style = ImGui.GetStyle();
            style.WindowRounding = 5.3f;
            style.FrameRounding = 2.3f;
            style.ScrollbarRounding = 0;

            //Style colors.
            style.Colors[(int)ImGuiCol.Text] = new Num.Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Num.Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Num.Vector4(0.06f, 0.06f, 0.06f, 0.94f);
            style.Colors[(int)ImGuiCol.ChildBg] = new Num.Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Num.Vector4(0.08f, 0.08f, 0.08f, 0.94f);
            style.Colors[(int)ImGuiCol.Border] = new Num.Vector4(0.43f, 0.43f, 0.50f, 0.50f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Num.Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Num.Vector4(0.00f, 0.42f, 0.15f, 0.54f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Num.Vector4(0.40f, 0.90f, 0.00f, 0.40f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Num.Vector4(0.00f, 0.55f, 0.23f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Num.Vector4(0.04f, 0.28f, 0.13f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Num.Vector4(0.10f, 0.41f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Num.Vector4(0.00f, 0.00f, 0.00f, 0.51f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Num.Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Num.Vector4(0.02f, 0.02f, 0.02f, 0.53f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Num.Vector4(0.31f, 0.31f, 0.31f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Num.Vector4(0.31f, 0.31f, 0.31f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Num.Vector4(0.51f, 0.51f, 0.51f, 1.00f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Num.Vector4(0.12f, 0.85f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Num.Vector4(0.19f, 0.78f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Num.Vector4(0.57f, 1.00f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.Button] = new Num.Vector4(0.01f, 0.65f, 0.00f, 0.40f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Num.Vector4(0.53f, 0.86f, 0.01f, 0.74f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Num.Vector4(0.53f, 0.86f, 0.01f, 0.74f);
            style.Colors[(int)ImGuiCol.Header] = new Num.Vector4(0.00f, 0.57f, 0.07f, 0.58f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Num.Vector4(0.49f, 0.86f, 0.00f, 0.82f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Num.Vector4(0.22f, 0.79f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.Separator] = new Num.Vector4(0.43f, 0.43f, 0.50f, 0.50f);
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Num.Vector4(0.16f, 0.78f, 0.00f, 0.70f);
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Num.Vector4(0.02f, 0.41f, 0.14f, 1.00f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Num.Vector4(0.02f, 0.58f, 0.09f, 0.58f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Num.Vector4(0.57f, 1.00f, 0.00f, 0.69f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Num.Vector4(0.41f, 0.86f, 0.01f, 1.00f);
            style.Colors[(int)ImGuiCol.Tab] = new Num.Vector4(0.03f, 0.41f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Num.Vector4(0.43f, 0.76f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TabActive] = new Num.Vector4(0.18f, 0.67f, 0.05f, 1.00f);
            style.Colors[(int)ImGuiCol.TabUnfocused] = new Num.Vector4(0.00f, 0.20f, 0.05f, 0.54f);
            style.Colors[(int)ImGuiCol.TabUnfocusedActive] = new Num.Vector4(0.00f, 0.31f, 0.06f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotLines] = new Num.Vector4(0.61f, 0.61f, 0.61f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Num.Vector4(1.00f, 0.43f, 0.35f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Num.Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Num.Vector4(1.00f, 0.60f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Num.Vector4(0.17f, 0.71f, 0.00f, 0.35f);
            style.Colors[(int)ImGuiCol.DragDropTarget] = new Num.Vector4(1.00f, 1.00f, 0.00f, 0.90f);
            style.Colors[(int)ImGuiCol.NavHighlight] = new Num.Vector4(0.46f, 0.87f, 0.03f, 1.00f);
            style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Num.Vector4(1.00f, 1.00f, 1.00f, 0.70f);
            style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Num.Vector4(0.80f, 0.80f, 0.80f, 0.20f);
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Num.Vector4(0.80f, 0.80f, 0.80f, 0.20f);

            //Set up the window manager.
            WindowManager.Renderer = ImGuiRenderer;
            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;

        }
        
        /// <summary>
        /// Draw the scene.
        /// </summary>
        public override void Draw() {

            //Wallpapers.
            Wallpapers.Draw();
            Helper.SpriteBatch.End();
            Helper.SpriteBatch.Begin();

            //Run data before layout.
            ImGuiRenderer.BeforeLayout(Helper.Time);

            //Set up font.
            ImGui.PushFont(Font);

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
