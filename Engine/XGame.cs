using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using XGameEngine.Logic.Camera;
using XGameEngine.Logic.Input;
using XGameEngine.Logic.Utility;
using XGameEngine.Managers;
using XGameEngine.Rendering;

namespace XGameEngine
{
    /// <summary>
    /// Represents the basic logic needed for rendering and update a game.
    /// </summary>
    public class XGame : Game
    {
        /// <summary>
        /// The relative path to the content directory.
        /// </summary>
        private readonly string contentPath = "Content";

        /// <summary>
        /// Retrieves the amount of frames per second.
        /// </summary>
        private FPSCounter frameCounter;

        /// <summary>
        /// Manages the active game screen.
        /// </summary>
        private ScreenManager screens;

        /// <summary>
        /// Helps with taking in-game screenshots.
        /// </summary>
        private ScreenShot screenShot;

        /// <summary>
        /// Initializes a new instance of the <see cref="XGame"/> class.
        /// </summary>
        /// <param name="width">Width of the game.</param>
        /// <param name="height">Heght of the game.</param>
        /// <param name="name">Name of the game window.</param>
        public XGame(int width, int height, string name)
        {
            graphics = new GraphicsDeviceManager(this);
        }

        /// <summary>
        /// Gets the average amount of frames.
        /// </summary>
        public float AverageFPS
        {
            get { return this.frameCounter.AverageFPS; }
        }

        /// <summary>
        /// Gets the current amount of frames.
        /// </summary>
        public float CurrentFPS
        {
            get { return this.frameCounter.CurrentFPS; }
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
        private GraphicsDeviceManager graphics;


        /// <summary>
        /// Gets or sets the camera used when rendering the game.
        /// </summary>
        public GameCamera Camera
        {
            get { return this.camera; }
            set { this.camera = value; }
        }
        private GameCamera camera;

        /// <summary>
        /// Helps with rendering game sprites.
        /// </summary>
        public IGameRenderer WorldRender
        {
            get { return this.worldrender; }
        }
        private IGameRenderer worldrender;

        /// <summary>
        /// Gets the engines content within the assembly.
        /// </summary>
        public ResourceLoader EngineResource
        {
            get { return this.engineResource; }
        }
        private ResourceLoader engineResource;

        /// <summary>
        /// Gets the manager for the game screens.
        /// </summary>
        public ScreenManager Screens
        {
            get { return this.screens; }
        }

        /// <summary>
        /// Gets the path of the current execution.
        /// </summary>
        public string? ExecutionPath => Path.GetDirectoryName
                    (Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Draw the game.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Update the frame count.
            this.frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            this.worldrender.Begin(0);
            this.worldrender.Begin(1);
            this.worldrender.Draw(1, gameTime);
            this.worldrender.End(1);
            this.worldrender.End(0);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Initialize the game content.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();

            // Initialize render.
            this.worldrender = new GameRenderDefault(this);

            // Initialize render.
            this.worldrender.Initialize();

            // Initialize the screens.
            this.screens = new ScreenManager(this);
            this.screens.Initialize();
            this.worldrender.Entities.Add(this.screens);

            // Setup the screenshot.
            this.screenShot = new ScreenShot
                (this, new Action(delegate { this.Draw(new GameTime()); }),
                string.Format("{0}/{1}/ScreenShots", ExecutionPath, contentPath));

            // Initialize frameCounter
            this.frameCounter = new FPSCounter();

            base.Initialize();
        }

        protected override void LoadContent()
        {            
            // Initialize engine resources.
            this.engineResource = new ResourceLoader(this, this.contentPath);
            this.engineResource.Initialize();

            base.LoadContent();

            // Create a basic camera.
            this.camera = new SimpleCamera(this.GraphicsDevice.Viewport);
            this.worldrender.SpriteOptions[1].Camera = this.camera;
        }

        /// <summary>
        /// Update game content.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update the input of the game.
            InputState.Update(gameTime);
            this.worldrender.Update(gameTime);


            if (InputState.KeyPressed(Keys.F12, PlayerIndex.One, StateOptions.CurrentFavor))
            {
                this.screenShot.CreateScreenShot();
            }

            base.Update(gameTime);
        }
    }
}
