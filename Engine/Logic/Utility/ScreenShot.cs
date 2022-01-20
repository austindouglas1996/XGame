using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XGameEngine.Core;
using XGameEngine.Logic.Input;

namespace XGameEngine.Logic.Utility
{
    /// <summary>
    /// Provides the ability to take in-game screenshots.
    /// </summary>
    public class ScreenShot : GameObject
    {
        /// <summary>
        /// The main source of the games draw.
        /// </summary>
        private Delegate drawMethod;

        /// <summary>
        /// The key to be pressed to take a screenshot.
        /// </summary>
        private Keys key = Keys.F12;

        /// <summary>
        /// The path to save the picture.
        /// </summary>
        private string saveDirectory = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenShot"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <param name="drawMethod">The primary method of game drawing.</param>
        /// <param name="saveDirectory">The directory to save screenshots.</param>
        public ScreenShot(XGame game, Delegate drawMethod, string saveDirectory)
            : base(game, new Vector2(1,1))
        {
            if (drawMethod == null)
            {
                // ToDo: Need to write exception.
            }

            // Set.
            this.drawMethod = drawMethod;

            if (!Directory.Exists(saveDirectory))
            {
                // ToDo: Need to write exception.
            }

            this.saveDirectory = saveDirectory;
        }

        /// <summary>
        /// Gets or sets the save directory of screenshots.
        /// </summary>
        public string SaveDirectory
        {
            get { return this.saveDirectory; }
            set
            {
                if (!Directory.Exists(value))
                {
                    // ToDo: Need to write exception.
                }

                this.saveDirectory = value;
            }
        }

        /// <summary>
        /// Create a new screenshot.
        /// </summary>
        public void CreateScreenShot()
        {
            // Create new renderTarget
            RenderTarget2D render = new RenderTarget2D
                (this.Game.GraphicsDevice,
                this.Game.GraphicsDevice.Viewport.Width,
                this.Game.GraphicsDevice.Viewport.Height);

            // Clear and then set target.
            this.Game.GraphicsDevice.SetRenderTarget(null);
            this.Game.GraphicsDevice.SetRenderTarget(render);

            // Force draw.
            this.drawMethod.DynamicInvoke();

            // Create a stream.
            Stream file = File.OpenWrite(this.saveDirectory 
                + string.Format("\\{0}.png", DateTime.Now.ToString("yyyy-MM-dd-HH-mm")));

            // Remove target.
            this.Game.GraphicsDevice.SetRenderTarget(null);

            // Save.
            render.SaveAsJpeg(file, render.Width, render.Height);

            // Dispose.
            file.Dispose();
            render.Dispose();
        }

        /// <summary>
        /// Check and see if the key was pressed.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (InputState.KeyPressed(this.key, PlayerIndex.One))
            {
                this.CreateScreenShot();
            }

            base.Update(gameTime);
        }
    }
}
