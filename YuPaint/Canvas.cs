using LilyPath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YuPaint {

    /// <summary>
    /// Canvas.
    /// </summary>
    public class Canvas {

        /// <summary>
        /// Target.
        /// </summary>
        RenderTarget2D Target;

        /// <summary>
        /// Width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Layers.
        /// </summary>
        public List<Layer> Layers;

        /// <summary>
        /// Color cache.
        /// </summary>
        private Color[] ColorCache;

        /// <summary>
        /// Undos.
        /// </summary>
        private Stack<ReversibleAction> Undos = new Stack<ReversibleAction>();

        /// <summary>
        /// Foreground or active color.
        /// </summary>
        public Color ForegroundColor = new Color(0, 0, 0);

        /// <summary>
        /// Background color.
        /// </summary>
        public Color BackgroundColor = new Color(255, 255, 255);

        /// <summary>
        /// Current layer.
        /// </summary>
        public int CurrentLayer = 0;

        /// <summary>
        /// Active layer.
        /// </summary>
        public Layer ActiveLayer => Layers[CurrentLayer];

        /// <summary>
        /// Drawbatch.
        /// </summary>
        public DrawBatch DrawBatch;

        /// <summary>
        /// Current tool.
        /// </summary>
        public string CurrentTool = "Dropper";

        /// <summary>
        /// Active tool.
        /// </summary>
        public Tool ActiveTool => Tools[CurrentTool];

        /// <summary>
        /// Tools.
        /// </summary>
        public Dictionary<string, Tool> Tools = new Dictionary<string, Tool>();

        /// <summary>
        /// Make a new canvas.
        /// </summary>
        /// <param name="target">Target.</param>
        public Canvas(RenderTarget2D target) {

            //General setup.
            Target = target;
            DrawBatch = new DrawBatch(Target.GraphicsDevice);
            Layers = new List<Layer>();
            Width = target.Width;
            Height = target.Height;
            ColorCache = new Color[target.Width * target.Height];
            new Layer(this);

            //Tools.
            Tools.Add("Dropper", new Dropper(this));

        }

        /// <summary>
        /// Clamp a color.
        /// </summary>
        /// <param name="val">Value to clamp.</param>
        /// <returns>Clamped color.</returns>
        byte ClampColor(float val) {
            if (val < 0) {
                return 0;
            } else if (val > 255) {
                return 255;
            } else {
                return (byte)val;
            }
        }

        /// <summary>
        /// Blending mode. TODO! Finish and do blending stuff.
        /// </summary>
        /// <param name="f">Foreground color.</param>
        /// <param name="b">Background color.</param>
        /// <param name="mode">Blend mode.</param>
        /// <returns>Blended color.</returns>
        public Color Blend(Color f, Color b, BlendMode mode) { 
            switch (mode) {

                //Addition.
                case BlendMode.Addition:
                    return new Color(
                        ClampColor(f.R + b.R),
                        ClampColor(f.G + b.G),
                        ClampColor(f.B + b.B),
                        Math.Max(f.A, b.A)
                    );

                //Normal.
                default:
                    return new Color(
                        ClampColor(f.R * f.A / 255f + b.R * (1 - f.A / 255f)),
                        ClampColor(f.G * f.A / 255f + b.G * (1 - f.A / 255f)),
                        ClampColor(f.B * f.A / 255f + b.B * (1 - f.A / 255f)),
                        ClampColor(f.A * f.A / 255f + b.A * (1 - f.A / 255f))
                    );

            }
        }

        /// <summary>
        /// Refresh pixel.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        public void RefreshPixel(int x, int y) {
            for (int l = Layers.Count - 1; l >= 0; l--) {
                int ptr = x + y * Width;
                var f = (255 - Layers[l].AlphaMask[x, y]) / 255f * Layers[l].Data[x, y] * Layers[l].Opacity;
                ColorCache[ptr] = Blend(f, ColorCache[ptr], Layers[l].BlendMode);
            }
        }

        /// <summary>
        /// Get a color at a position.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <returns>Color at the position.</returns>
        public Color GetColor(int x, int y) {
            return ColorCache[x + y * Width];
        }

        /// <summary>
        /// Render the drawing onto the target.
        /// </summary>
        public void Render() {

            //Set data.
            Target.SetData(ColorCache);

            //Start draw batch.
            DrawBatch.Begin(DrawSortMode.Deferred);

            Vector2 origin = new Vector2(200, 200);
            float startAngle = (float)(Math.PI / 16) * 25; // 11:20
            float arcLength = (float)(Math.PI / 16) * 30;

            DrawBatch.FillCircle(new SolidColorBrush(Color.SkyBlue), origin, 175);
            DrawBatch.FillArc(new SolidColorBrush(Color.LimeGreen), origin, 150,
                startAngle, arcLength, ArcType.Sector);
            DrawBatch.DrawClosedArc(new Pen(Color.Green, 15), origin, 150,
                startAngle, arcLength, ArcType.Sector);

            //Draw active tool.
            ActiveTool.Draw();

            //End draw batch.
            DrawBatch.End();

        }

        /// <summary>
        /// Undo the last reversible action.
        /// </summary>
        public void Undo() { 
        
        }

        /// <summary>
        /// Redo the last reversible action.
        /// </summary>
        public void Redo() { 
        
        }

    }

}
