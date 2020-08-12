using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace YuPaint {
    
    /// <summary>
    /// Tool.
    /// </summary>
    public abstract class Tool {

        /// <summary>
        /// Canvas.
        /// </summary>
        protected Canvas Canvas;

        /// <summary>
        /// An action is in progress.
        /// </summary>
        public bool ActionInProgress { get; private set; }
        
        /// <summary>
        /// Mouse state.
        /// </summary>
        public MouseState MouseState;

        /// <summary>
        /// Keyboard state.
        /// </summary>
        public KeyboardState KeyboardState;

        /// <summary>
        /// Make a new tool.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public Tool(Canvas canvas) {
            Canvas = canvas;
        }

        /// <summary>
        /// Change canvas used for the tool.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public void ChangeCanvas(Canvas canvas) {
            Canvas = canvas;
        }

        /// <summary>
        /// On left click.
        /// </summary>
        /// <param name="x">X position of the mouse.</param>
        /// <param name="y">Y position of the mouse.</param>
        public abstract void OnLeftClick(int x, int y);

        /// <summary>
        /// On right click.
        /// </summary>
        /// <param name="x">X position of the mouse.</param>
        /// <param name="y">Y position of the mouse.</param>
        public abstract void OnRightClick(int x, int y);

        /// <summary>
        /// During left click drag.
        /// </summary>
        /// <param name="x">X position of the mouse.</param>
        /// <param name="y">Y position of the mouse.</param>
        public abstract void DuringLeftDrag(int x, int y);

        /// <summary>
        /// During right drag.
        /// </summary>
        /// <param name="x">X position of the mouse.</param>
        /// <param name="y">Y position of the mouse.</param>
        public abstract void DuringRightDrag(int x, int y);

        /// <summary>
        /// Left click released.
        /// </summary>
        public abstract void LeftClickReleased();

        /// <summary>
        /// Right click released.
        /// </summary>
        public abstract void RightClickReleased();

        /// <summary>
        /// Draw the tool display.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Update hook.
        /// </summary>
        public virtual void UpdateHook() {}

        /// <summary>
        /// Start an action.
        /// </summary>
        public void StartAction() {

            //Start action.
            ActionInProgress = true;
            Canvas.ActiveLayer.StartRecordingChanges();

        }

        /// <summary>
        /// End an action.
        /// </summary>
        public void EndAction() {

            //End action.
            Canvas.ActiveLayer.EndRecordingChanges();
            ActionInProgress = false;
        
        }

        /// <summary>
        /// Universal update.
        /// </summary>
        public void Update(MouseState mouseState, KeyboardState keyboardState) {

            //Set states.
            MouseState = Mouse.GetState();
            KeyboardState = Keyboard.GetState();

            //Call hook.
            UpdateHook();

        }

    }

}
