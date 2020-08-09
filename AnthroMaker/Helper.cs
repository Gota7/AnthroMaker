using ImGuiUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthroMaker {
    
    /// <summary>
    /// Class to provide useful shortcuts.
    /// </summary>
    public static class Helper {

        /// <summary>
        /// Game.
        /// </summary>
        public static Game Game;

        /// <summary>
        /// Time.
        /// </summary>
        public static GameTime Time;

        /// <summary>
        /// Debugging features.
        /// </summary>
        public static bool DebugMode;

        /// <summary>
        /// Current scene.
        /// </summary>
        public static Scene CurrentScene;

        /// <summary>
        /// Graphics.
        /// </summary>
        public static GraphicsDevice Graphics;

        /// <summary>
        /// Graphics device manager.
        /// </summary>
        public static GraphicsDeviceManager GraphicsDeviceManager;

        /// <summary>
        /// Sprite batch.
        /// </summary>
        public static SpriteBatch SpriteBatch;

        /// <summary>
        /// ImGui renderer.
        /// </summary>
        public static ImGuiRenderer ImGuiRenderer;

        /// <summary>
        /// Settings.
        /// </summary>
        public static Settings Settings = new Settings();

    }

}
