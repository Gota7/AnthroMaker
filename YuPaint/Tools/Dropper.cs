using System;
using System.Collections.Generic;
using System.Text;

namespace YuPaint {

    /// <summary>
    /// Dropper.
    /// </summary>
    public class Dropper : Tool {

        /// <summary>
        /// Make a new dropper.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public Dropper(Canvas canvas) : base(canvas) {}

        /// <summary>
        /// On left click.
        /// </summary>
        /// <param name="x">Mouse X.</param>
        /// <param name="y">Mouse Y.</param>
        public override void OnLeftClick(int x, int y) {
            PickColorForeground(x, y);
        }

        /// <summary>
        /// On right click.
        /// </summary>
        /// <param name="x">Mouse X.</param>
        /// <param name="y">Mouse Y.</param>
        public override void OnRightClick(int x, int y) {
            PickColorBackground(x, y);
        }

        /// <summary>
        /// During left drag.
        /// </summary>
        /// <param name="x">Mouse X.</param>
        /// <param name="y">Mouse Y.</param>
        public override void DuringLeftDrag(int x, int y) {
            PickColorForeground(x, y);
        }

        /// <summary>
        /// During right drag.
        /// </summary>
        /// <param name="x">Mouse X.</param>
        /// <param name="y">Mouse Y.</param>
        public override void DuringRightDrag(int x, int y) {
            PickColorBackground(x, y);
        }

        /// <summary>
        /// Left click released.
        /// </summary>
        public override void LeftClickReleased() {}

        /// <summary>
        /// Right click released.
        /// </summary>
        public override void RightClickReleased() {}

        /// <summary>
        /// Pick foreground color.
        /// </summary>
        /// <param name="x">Mouse X.</param>
        /// <param name="y">Mouse Y.</param>
        public void PickColorForeground(int x, int y) {
            if (KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) || KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightControl)) {
                Canvas.ForegroundColor = Canvas.GetColor(x, y);
            } else {
                Canvas.ForegroundColor = Canvas.ActiveLayer.Data[x, y];
            }
        }

        /// <summary>
        /// Pick background color.
        /// </summary>
        /// <param name="x">Mouse X.</param>
        /// <param name="y">Mouse Y.</param>
        public void PickColorBackground(int x, int y) {
            if (KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) || KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightControl)) {
                Canvas.BackgroundColor = Canvas.GetColor(x, y);
            } else {
                Canvas.BackgroundColor = Canvas.ActiveLayer.Data[x, y];
            }
        }

        /// <summary>
        /// Draw the tool display.
        /// </summary>
        public override void Draw() {}

    }

}
