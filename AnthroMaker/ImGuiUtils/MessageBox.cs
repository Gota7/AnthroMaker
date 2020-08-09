using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiUtils {

    /// <summary>
    /// Message box.
    /// </summary>
    public class MessageBox : Window {
        
        /// <summary>
        /// Message.
        /// </summary>
        public string Message;

        /// <summary>
        /// Create a new message box.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        public MessageBox(string title, string message) : base(title, ImGuiWindowFlags.Popup | ImGuiWindowFlags.NoResize, modalMode: true) {
            Message = message;
            var titleWidth = ImGui.CalcTextSize(title).X;
            if (titleWidth > ImGui.CalcTextSize(message).X) {
                Width += titleWidth + 22;
            }
        }

        /// <summary>
        /// Draw the layout.
        /// </summary>
        /// <param name="renderer">Renderer.</param>
        public override void DrawLayout(ImGuiRenderer renderer) {
            ImGui.Text(Message);
        }

        /// <summary>
        /// Show a message box.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        public static void Show(string title, string message) {
            new MessageBox(title, message);
        }

    }

}
