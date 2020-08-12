using AnthroMaker;
using ImGuiNET;
using ImGuiUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Num = System.Numerics;

namespace ImGuiUtils {

    /// <summary>
    /// A render area.
    /// </summary>
    public abstract class RenderArea : Scene {

        /// <summary>
        /// X position in the window.
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Y position in the window.
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// Mouse X camera coordinate.
        /// </summary>
        public float MouseX { get; private set; }

        /// <summary>
        /// Mouse Y camera coordinate.
        /// </summary>
        public float MouseY { get; private set; }

        /// <summary>
        /// Create a new render area.
        /// </summary>
        /// <param name="width">Render area width.</param>
        /// <param name="height">Render area height.</param>
        public RenderArea(int width, int height) {
            RenderTarget = new RenderTarget2D(Helper.Graphics, width, height);
            Initialize();
        }

        /// <summary>
        /// Resize the render area.
        /// </summary>
        /// <param name="width">Render area width.</param>
        /// <param name="height">Render area height.</param>
        public void Resize(int width, int height) {
            RenderTarget.Dispose();
            RenderTarget = new RenderTarget2D(Helper.Graphics, width, height);
        }

        /// <summary>
        /// Draw in ImGui.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public void ImGuiDraw(ImGuiRenderer renderer) {

            //Get X and Y.
            X = ImGui.GetCursorPosX();
            Y = ImGui.GetCursorPosY();

            //Get the mouse location.
            MouseX = ImGui.GetMousePos().X - X - ImGui.GetWindowPos().X;
            MouseY = ImGui.GetMousePos().Y - Y - ImGui.GetWindowPos().Y;
            MouseX += ImGui.GetScrollX();
            MouseY += ImGui.GetScrollY();

            //Add camera.
            //MouseX += Helper.Camera.X;
            //MouseY += Helper.Camera.Y;

            //Draw image.
            AdjustTrueSize();
            WindowManager.QueueRenderArea(this);
            ImGui.Image(renderer.BindTexture(RenderTarget), new Num.Vector2(RenderTarget.Width, RenderTarget.Height));

        }

        /// <summary>
        /// Maximize the render area to fill up the remaining space in the window.
        /// </summary>
        /// <param name="window">The window.</param>
        public void Maximize(Window window) {

            //Get width and height, and set it.
            int width = (int)Math.Max(1, window.Width - X - ImGui.GetStyle().WindowPadding.X + ImGui.GetScrollX());
            int height = (int)Math.Max(1, window.Height - Y - ImGui.GetStyle().WindowPadding.Y + ImGui.GetScrollY());
            if (width != RenderTarget.Width || height != RenderTarget.Height) {
                Resize(width, height);
            }

            //Set values.
            Width = width;
            Height = height;

        }

        /// <summary>
        /// Adjust true size if needed.
        /// </summary>
        public void AdjustTrueSize() {
            Width = RenderTarget.Width;
            Height = RenderTarget.Height;
        }

    }

}
