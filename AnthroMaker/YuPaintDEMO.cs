using ImGuiUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using YuPaint;

namespace AnthroMaker {

    /// <summary>
    /// A demo showcasing the YuPaint painting library by Gota7.
    /// </summary>
    public class YuPaintDEMO : Window {

        /// <summary>
        /// View.
        /// </summary>
        public YuPaintDEMOView View;

        /// <summary>
        /// YuPaint DEMO.
        /// </summary>
        public YuPaintDEMO() : base("YuPaint DEMO", x: 100, y: 100, flags: ImGuiNET.ImGuiWindowFlags.NoResize) {
            View = new YuPaintDEMOView(800, 600);
        }

        /// <summary>
        /// Draw layout.
        /// </summary>
        /// <param name="renderer">Renderer.</param>
        public override void DrawLayout(ImGuiRenderer renderer) {
            View.ImGuiDraw(renderer);
        }

        /// <summary>
        /// Update.
        /// </summary>
        public override void Update() {
            if (View.MouseX >= 0 && View.MouseX < View.Width && View.MouseY >= 0 && View.MouseY < View.Height) {
                Flags |= ImGuiNET.ImGuiWindowFlags.NoMove;
            } else {
                Flags = ImGuiNET.ImGuiWindowFlags.NoResize;
            }
            View.Update();
        }

    }

    /// <summary>
    /// Demo view.
    /// </summary>
    public class YuPaintDEMOView : RenderArea {

        /// <summary>
        /// Draw canvas.
        /// </summary>
        public Canvas Canvas;

        /// <summary>
        /// New DEMO view.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public YuPaintDEMOView(int width, int height) : base(width, height) {
            Canvas = new Canvas(RenderTarget);
            Canvas.ActiveLayer.Clear(Color.White);
        }

        /// <summary>
        /// Do render code.
        /// </summary>
        public override void Draw() {
            Canvas.Render();
        }

        /// <summary>
        /// Update view.
        /// </summary>
        public override void Update() {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                if (MouseX >= 0 && MouseX < Width && MouseY >= 0 && MouseY < Height) {
                    Canvas.Layers[0].SetPixel((int)MouseX, (int)MouseY, new Microsoft.Xna.Framework.Color(0, 0, 0));
                }
            }
        }

    }

}
