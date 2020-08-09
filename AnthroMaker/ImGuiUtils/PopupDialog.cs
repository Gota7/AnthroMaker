using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiUtils {

    /// <summary>
    /// Pop-up dialog.
    /// </summary>
    public abstract class PopupDialog : Window {

        /// <summary>
        /// A popup dialog.
        /// </summary>
        public PopupDialog(string name, ImGuiWindowFlags flags = ImGuiWindowFlags.Popup, bool visible = true, float x = -1, float y = -1, float width = -1, float height = -1) : base(name, flags, visible, false, false, x, y, width, height) {
            ModalMode = true;
        }

    }

}
