using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiUtils {
    
    /// <summary>
    /// A popup with buttons options.
    /// </summary>
    public class ButtonsPopup : Window {

        /// <summary>
        /// Get the selected option name.
        /// </summary>
        public string OptionName;

        /// <summary>
        /// Message.
        /// </summary>
        public string Message;

        /// <summary>
        /// Options.
        /// </summary>
        private List<string> Options;

        /// <summary>
        /// Create a new message box.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        public ButtonsPopup(string title, List<string> options, string message = "") : base(title, ImGuiWindowFlags.Popup | ImGuiWindowFlags.NoResize, modalMode: true) {
            Message = message;
            Options = options;
        }

        /// <summary>
        /// Draw the layout.
        /// </summary>
        /// <param name="renderer">Renderer.</param>
        public override void DrawLayout(ImGuiRenderer renderer) {
            if (Message != "") {
                ImGui.Text(Message);
            }
            foreach (var o in Options) {
                if (ImGui.Button(o)) {
                    OptionName = o;
                    Open = false;
                }
                if (o != Options.Last()) {
                    ImGui.SameLine();
                }
            }
        }

    }

}
