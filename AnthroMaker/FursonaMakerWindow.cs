using ImGuiNET;
using ImGuiUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnthroMaker {
    
    /// <summary>
    /// Fursona making window.
    /// </summary>
    public class FursonaMakerWindow : Window {

        /// <summary>
        /// Make a new fursona maker.
        /// </summary>
        public FursonaMakerWindow() : base("Fursona Maker", x: 100, y: 100) { }
        
        /// <summary>
        /// Draw the layout.
        /// </summary>
        /// <param name="renderer">Renderer.</param>
        public override void DrawLayout(ImGuiRenderer renderer) {
            ImGui.Text("Hello World!");
        }

    }

}
