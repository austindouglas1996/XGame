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
        /// Holds the camera which can be used to edit the rendering portion.
        /// </summary>
        private GameCamera camera;

        /// <summary>
        /// Helps render game content.
        /// </summary>
        private IGameRenderer render;

        /// <summary>
        /// Reads or writes the game settings.
        /// </summary>
        private GameSettings settings;

        /// <summary>
        /// Represents the view of the game.
        /// </summary>
        private GameView view;

        /// <summary>
        /// Helps get files within the engines assembly.
        /// </summary>
        private ResourceLoader engineResource;

        /// <summary>
        /// Manages the active game screen.
        /// </summary>
        private ScreenManager screens;

        /// <summary>
        /// Helps with taking in-game screenshots.
        /// </summary>
        private ScreenShot screenShot;

        /// <summary>
        /// Name of the current game.
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="XGame"/> class.
        /// </summary>
        /// <param name="width">Width of the game.</param>
        /// <param name="height">Heght of the game.</param>
        /// <param name="name">Name of the game window.</param>
        public XGame(int width, int height, string name)
        {
            // Set the name of the game.
            this.name = name;

            // Initialize settings.
            this.settings = new GameSettings(this);
            this.settings.Load();

            // Initialize view.
            this.view = new GameView(this, new Vector2(width, height));
            this.settings.Display.Width = width;
            this.settings.Display.Height = height;

            // Initialize render.
            this.render = new GameRenderDefault(this);

            // Initialize engine resources.
            this.engineResource = new ResourceLoader(this, this.contentPath);
            this.engineResource.Initialize();

            WriteVersionToFile();
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
        /// Gets or sets the camera used when rendering the game.
        /// </summary>
        public GameCamera Camera
        {
            get { return this.camera; }
            set { this.camera = value; }
        }

        /// <summary>
        /// Helps with rendering game sprites.
        /// </summary>
        public IGameRenderer Render
        {
            get { return this.render; }
        }

        /// <summary>
        /// Gets the primary game settings.
        /// </summary>
        public GameSettings Settings
        {
            get { return this.settings; }
        }

        /// <summary>
        /// Gets the rendering portion of the game.
        /// </summary>
        public GameView View
        {
            get { return this.view; }
        }

        /// <summary>
        /// Gets the engines content within the assembly.
        /// </summary>
        public ResourceLoader EngineResource
        {
            get { return this.engineResource; }
        }

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
        /// Gets the name of the current game.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Draw the game.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Update the frame count.
            this.frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            this.render.Begin();
            this.render.Draw(gameTime);
            this.render.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Initialize the game content.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize render.
            this.render.Initialize();

            // Initialize the screens.
            this.screens = new ScreenManager(this);
            this.screens.Initialize();
            this.render.Entities.Add(this.screens);

            // Setup the screenshot.
            this.screenShot = new ScreenShot
                (this, new Action(delegate{this.Draw(new GameTime());}), 
                string.Format("{0}/{1}/ScreenShots", ExecutionPath, contentPath));

            // Initialize frameCounter
            this.frameCounter = new FPSCounter();

            // Create a basic camera.
            this.camera = new SimpleCamera(this.view.Viewport);
            this.render.SpriteOptions.Camera = this.camera;

            // Apply graphics.
            this.view.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// Update game content.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update the input of the game.
            InputState.Update(gameTime);
            this.render.Update(gameTime);


            if (InputState.KeyPressed(Keys.F12, PlayerIndex.One, StateOptions.CurrentFavor))
            {
                this.screenShot.CreateScreenShot();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Writes the current version to file.
        /// </summary>
        private static void WriteVersionToFile()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            string savePath = Path.GetDirectoryName(path) + "\\Content\\Version";

            // Get info on the current assembly.
            FileVersionInfo versionInfo = 
                FileVersionInfo.GetVersionInfo(path);

            // Get version.
            string? version = versionInfo.FileVersion;

            // Write to file if file does not exist already.
            if (!File.Exists(savePath))
            {
                using (StreamWriter stream = new StreamWriter(savePath))
                {
                    stream.WriteLine
                        (string.Format("{0} Version: {1}", Path.GetFileName(versionInfo.FileName), version));
                }
            }

        }
    }
}
