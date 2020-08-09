using ImGuiNET;
using ImGuiUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnthroMaker {

    /// <summary>
    /// About window.
    /// </summary>
    public class AboutWindow : Window {

        /// <summary>
        /// Make a new about window.
        /// </summary>
        public AboutWindow() : base("About Anthro Maker:", x: 100, y: 100) {}

        /// <summary>
        /// Draw the layout.
        /// </summary>
        /// <param name="renderer">Renderer.</param>
        public override void DrawLayout(ImGuiRenderer renderer) {
            ImGui.Text("Hello World!");
        }

    }

}
