using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthroMaker {

    /// <summary>
    /// A scene.
    /// </summary>
    public abstract class Scene {

        /// <summary>
        /// Width.
        /// </summary>
        public float Width;

        /// <summary>
        /// Height.
        /// </summary>
        public float Height;

        /// <summary>
        /// Render target.
        /// </summary>
        public RenderTarget2D RenderTarget;

        /// <summary>
        /// Render the scene instead of drawing it.
        /// </summary>
        public void Render() {
            var bak = Helper.CurrentScene;
            Helper.CurrentScene = this;
            Helper.Graphics.SetRenderTarget(RenderTarget);
            Draw();
            Helper.Graphics.SetRenderTarget(null);
            Helper.CurrentScene = bak;
        }

        /// <summary>
        /// Initialize the scene.
        /// </summary>
        public virtual void Initialize() {}

        /// <summary>
        /// Draw the scene.
        /// </summary>
        public virtual void Draw() {}

        /// <summary>
        /// Update the scene.
        /// </summary>
        public virtual void Update() {}

    }

    /// <summary>
    /// Default scene.
    /// </summary>
    public sealed class DefaultScene : Scene {}

}