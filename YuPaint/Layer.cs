using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace YuPaint {

    /// <summary>
    /// Blend mode.
    /// </summary>
    public enum BlendMode { 
        Normal,
        Addition,
        Subtraction,
        Difference,
        Multiply,
        Divide,
        Screen,
        Overlay,
        DarkenOnly,
        LightenOnly,
    }

    /// <summary>
    /// Layer.
    /// </summary>
    public class Layer {

        /// <summary>
        /// Color data.
        /// </summary>
        public Color[,] Data;

        /// <summary>
        /// Alpha mask.
        /// </summary>
        public byte[,] AlphaMask;

        /// <summary>
        /// Layer opacity.
        /// </summary>
        public float Opacity = 1f;

        /// <summary>
        /// Blend mode.
        /// </summary>
        public BlendMode BlendMode;

        /// <summary>
        /// Canvas.
        /// </summary>
        private Canvas Canvas;

        /// <summary>
        /// Make a new layer.
        /// </summary>
        /// <param name="canvas">Canvas to attach to.</param>
        public Layer(Canvas canvas) {
            Data = new Color[canvas.Width, canvas.Height];
            AlphaMask = new byte[canvas.Width, canvas.Height];
            Canvas = canvas;
            canvas.Layers.Add(this);
        }

        /// <summary>
        /// Set a pixel.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <param name="c">Color.</param>
        public void SetPixel(int x, int y, Color c) {
            Data[x, y] = c;
            Canvas.RefreshPixel(x, y);
        }

        /// <summary>
        /// Clear the layer with a color.
        /// </summary>
        /// <param name="c">Color.</param>
        public void Clear(Color c) {
            for (int i = 0; i < Canvas.Width; i++) {
                for (int j = 0; j < Canvas.Height; j++) {
                    SetPixel(i, j, c);
                }
            }
        }

        /// <summary>
        /// Start recording changes for undo/redo.
        /// </summary>
        public void StartRecordingChanges() { 
        
        }

        /// <summary>
        /// End recording changes for undo/redo.
        /// </summary>
        public void EndRecordingChanges() { 
        
        }
    
    }

}
