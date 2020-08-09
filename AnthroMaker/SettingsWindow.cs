using ImGuiNET;
using ImGuiUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnthroMaker {
    
    /// <summary>
    /// Settings window.
    /// </summary>
    public class SettingsWindow : Window {

        /// <summary>
        /// Make a new settings window.
        /// </summary>
        public SettingsWindow() : base("Settings:", ImGuiWindowFlags.NoResize, x: 100, y: 100) { }

        /// <summary>
        /// Draw the layout.
        /// </summary>
        /// <param name="renderer">Renderer.</param>
        public override void DrawLayout(ImGuiRenderer renderer) {
            ImGui.Checkbox("Allow NSFW Content", ref Helper.Settings.NSFW);
            ImGui.Checkbox("Enable Wallpapers", ref Helper.Settings.EnableWallpapers);
            ImGui.Checkbox("Force Wallpapers To Be SFW", ref Helper.Settings.ForceWallpaperSFW);
            ImGui.Separator();
            ImGui.Text("Milliseconds Per Wallpaper");
            ImGui.InputInt("##1", ref Helper.Settings.WallpaperTime, 100, 1000);
            if (Helper.Settings.WallpaperTime < 2000) {
                Helper.Settings.WallpaperTime = 2000;
            }
            ImGui.Text("Wallpaper Tags");
            ImGui.InputText("##2", ref Helper.Settings.WallpaperTags, 10000);
            ImGui.Separator();
            if (ImGui.Button("Reload Last Settings")) {
                Helper.Settings.LoadSettings();
            }
            ImGui.SameLine();
            if (ImGui.Button("Save Settings")) {
                Helper.Settings.SaveSettings();
            }
        }

    }

}
