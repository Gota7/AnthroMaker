using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiUtils {

    /// <summary>
    /// Window manager.
    /// </summary>
    public static class WindowManager {

        /// <summary>
        /// Renderer.
        /// </summary>
        public static ImGuiRenderer Renderer;

        /// <summary>
        /// Windows.
        /// </summary>
        private static List<Window> Windows = new List<Window>();

        /// <summary>
        /// Windows to add.
        /// </summary>
        private static List<Window> WindowsToAdd = new List<Window>();

        /// <summary>
        /// Windows to destroy.
        /// </summary>
        private static List<Window> WindowsToDestroy = new List<Window>();

        /// <summary>
        /// Add a window.
        /// </summary>
        /// <param name="w">Window to add.</param>
        public static void AddWindow(Window w) {
            WindowsToAdd.Add(w);
        }

        /// <summary>
        /// Remove a window.
        /// </summary>
        /// <param name="w">Window to remove.</param>
        public static void RemoveWindow(Window w) {
            WindowsToDestroy.Add(w);
        }

        /// <summary>
        /// Draw the windows.
        /// </summary>
        public static void Draw() {
            foreach (var w in Windows) {
                w.Draw(Renderer);
            }
        }

        /// <summary>
        /// Update the windows.
        /// </summary>
        public static void Update() {
            foreach (var w in WindowsToAdd) {
                Windows.Add(w);
            }
            WindowsToAdd.Clear();
            foreach (var w in WindowsToDestroy) {
                Windows.Remove(w);
            }
            WindowsToDestroy.Clear();
            foreach (var w in Windows) {
                w.Update();
            }
        }

    }

}
