using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnthroMaker {
    
    /// <summary>
    /// Antromaker main.
    /// </summary>
    public class AnthroMaker : Game {

		/// <summary>
		/// Display width.
		/// </summary>
		public const int DISPLAY_WIDTH = 1280;

		/// <summary>
		/// Display height.
		/// </summary>
		public const int DISPLAY_HEIGHT = 720;

		/// <summary>
		/// Debug mode.
		/// </summary>
		public const bool DEBUG_MODE = true;

		/// <summary>
		/// Sprite batch.
		/// </summary>
		public SpriteBatch SpriteBatch;

		/// <summary>
		/// Main scene.
		/// </summary>
		public MainScene MainScene;

		/// <summary>
		/// New Anthro Maker.
		/// </summary>
		public AnthroMaker() {
			Content.RootDirectory = "Content";
			var graphics = new GraphicsDeviceManager(this) {
				PreferredBackBufferWidth = DISPLAY_WIDTH,
				PreferredBackBufferHeight = DISPLAY_HEIGHT,
			};
			graphics.PreferMultiSampling = true;
			graphics.ApplyChanges();
			Helper.GraphicsDeviceManager = graphics;
			MainScene = new MainScene();
		}

		/// <summary>
		/// Initialize stuff.
		/// </summary>
		protected override void LoadContent() {
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			Helper.SpriteBatch = SpriteBatch;
			Helper.Graphics = GraphicsDevice;
			Helper.Game = this;
			Helper.CurrentScene = MainScene;
			Helper.DebugMode = DEBUG_MODE;
			MainScene.Initialize();
			Window.Title = "Make Me A Furry!";
			base.LoadContent();
		}

		/// <summary>
		/// Draw everything.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			SpriteBatch.Begin();
			MainScene.Draw();
			SpriteBatch.End();
		}

		/// <summary>
		/// Update the stuff.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
			if (Helper.DebugMode && Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape)) {
				Exit();
			}
			Helper.Time = gameTime;
			MainScene.Update();
        }

    }

}
