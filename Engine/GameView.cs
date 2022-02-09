using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using XGameEngine.Graphics;
using XGameEngine.Settings;

namespace XGameEngine
{
    /// <summary>
    /// Provides the rendering portion of the game.
    /// </summary>
    public class GameView
    {
        /// <summary>
        /// Holds the options of the game.
        /// </summary>
        private DisplayOptions options;

        /// <summary>
        /// Manages the GraphicDevice.
        /// </summary>
        private GraphicsDeviceManager graphics;

        /// <summary>
        /// The current game session/
        /// </summary>
        private XGame game;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameView"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        public GameView(XGame game)
        {
            this.game = game;

            // Initialize.
            this.graphics = new GraphicsDeviceManager(game);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameView"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <param name="bounds">Sets the bounds of the current window.</param>
        public GameView(XGame game, Vector2 bounds)
            : this(game)
        {
            this.options = new DisplayOptions();
            this.Width = (int)bounds.X;
            this.Height = (int)bounds.Y;
            this.ApplyChanges();
        }

        /// <summary>
        /// Gets display options for the view of the game.
        /// </summary>
        public DisplayOptions Options
        {
            get { return this.options; }
            internal set { this.options = value; }
        }

        /// <summary>
        /// Gets the primary graphicDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get 
            { 
                return this.game.GraphicsDevice; 
            }
        }

        /// <summary>
        /// Gets the configuration and management for the graphicsDevice.
        /// </summary>
        public GraphicsDeviceManager Graphics
        {
            get 
            {
                return this.graphics;
            }
        }

        /// <summary>
        /// Gets or sets the window height.
        /// </summary>
        public int Height
        {
            get { return this.options.WindowHeight; }
            set { this.options.WindowHeight = value; }
        }

        /// <summary>
        /// Gets or sets the window width.
        /// </summary>
        public int Width
        {
            get { return this.options.WindowWidth; }
            set { this.options.WindowWidth = value; }
        }

        /// <summary>
        /// Gets or sets the portion of the game rendering target.
        /// </summary>
        public Viewport Viewport
        {
            get { return this.GraphicsDevice.Viewport; }
            set { this.GraphicsDevice.Viewport = value; }
        }

        /// <summary>
        /// Apply the appropiate graphic changes.
        /// </summary>
        public void ApplyChanges()
        {
            // Set the options.
            this.Graphics.IsFullScreen = this.options.PreferFullScreen;
            this.Graphics.PreferredBackBufferHeight = this.options.WindowHeight;
            this.Graphics.PreferredBackBufferWidth = this.options.WindowWidth;
            this.Graphics.PreferMultiSampling = this.options.PreferMultiSampling;

            // Make sure the game has been initialized.
            if (this.GraphicsDevice != null)
            {
                this.GraphicsDevice.PresentationParameters.MultiSampleCount = this.options.MultiSampleCount;
            }

            // Apply.
            this.Graphics.ApplyChanges();
        }
    }
}
