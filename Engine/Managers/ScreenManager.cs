using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Core;
using XGameEngine.Graphics;
using XGameEngine.Logic;

namespace XGameEngine.Managers
{
    /// <summary>
    /// Helps handle and render game screens.
    /// </summary>
    public class ScreenManager : GameObject, IRenderableGameObject
    {
        /// <summary>
        /// Holds a stack of game screens.
        /// </summary>
        private Stack<GameScreen> screens = new Stack<GameScreen>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenManager"/> class.
        /// </summary>
        public ScreenManager(XGame game)
            : base(game, new Vector2(1,1))
        {
        }

        /// <summary>
        /// Gets the primary screen.
        /// </summary>
        public GameScreen? ActiveScreen
        {
            get
            {
                if (this.screens.Count <= 0)
                {
                    return null;
                }

                return this.screens.Peek();
            }
        }

        /// <summary>
        /// Draw the currently active screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            // Make sure Initialize() has been called.
            if (!this.IsInitialized)
            {
                throw new ArgumentNullException
                    ("Initialize() must be called before Draw().");
            }

            // Hold the screen(s) which we will draw.
            List<GameScreen> drawable = new List<GameScreen>();

            foreach (GameScreen screen in this.screens)
            {
                // Add.
                drawable.Add(screen);

                // If this screen is not transparent we will not draw anymore.
                if (!screen.IsTranslucent)
                {
                    break;
                }
            }

            // Draw the screens.
            for (int i = 0; i < drawable.Count; i++)
            {
                // Make sure the screen is active.
                if (!drawable[i].IsActive ||
                    drawable[i].Visible == Visibility.Hidden)
                {
                    continue;
                }

                drawable[i].Draw(gameTime);
            }
        }

        /// <summary>
        /// Initialize the game screens.
        /// </summary>
        public override void Initialize()
        {
            // Initialize all screens.
            foreach (GameScreen screen in this.screens)
            {
                // Initialize.
                screen.Initialize();
            }

            base.Initialize();
        }

        /// <summary>
        /// Gets a value indicating whether a screen matches the active screen.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public bool IsActiveScreen(GameScreen screen)
        {
            // Make sure there is a active screen.
            if (this.ActiveScreen == null)
            {
                return false;
            }

            // Check if the screen matches this screen.
            if (this.ActiveScreen == screen)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the top screen of the stack.
        /// </summary>
        /// <returns>The currently active screen.</returns>
        public GameScreen? Peek()
        {
            // Make sure there is more than 1 screen.
            if (this.screens.Count < 1)
            {
                return null;
            }

            return this.screens.Peek();
        }

        /// <summary>
        /// Remove the screen at the top of the stack.
        /// </summary>
        /// <returns>The previously active screen.</returns>
        public GameScreen? Pop()
        {
            // Make sure there is more than 1 screen.
            if (this.screens.Count < 0)
            {
                return null;
            }

            // Remove the current screen.
            GameScreen previousScreen = this.screens.Pop();

            // Dispose of screen.
            previousScreen.Dispose();

            // Activate the new screen.
            if (this.ActiveScreen != null)
            {
                this.ActiveScreen.Activated();
            }

            return previousScreen;
        }

        /// <summary>
        /// Push a screen to the top of the stack.
        /// </summary>
        /// <param name="screen">Screen to be pushed to the top.</param>
        public void Push(GameScreen screen)
        {
            // Deactivate the current screen.
            if (this.ActiveScreen != null
                && !screen.IsTranslucent)
            {
                this.ActiveScreen.Deactived();
            }

            // Make sure the screen has been initialized.
            if (!screen.IsInitialized && this.IsInitialized)
            {
                screen.Initialize();
            }

            // Make sure the screen is active.
            if (!screen.IsActive)
            {
                screen.Activated();
            }

            // Start.
            this.screens.Push(screen);
        }

        /// <summary>
        /// Update the currently active screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Holds the screens which we will update.
            List<GameScreen> updatable = new List<GameScreen>();

            foreach (GameScreen screen in this.screens)
            {
                updatable.Add(screen);

                // Once we reach a screen that is not translucent
                // we want to stop adding screens.
                if (!screen.IsTranslucent)
                {
                    break;
                }
            }

            // Update the screens.
            for (int i = 0; i < updatable.Count; i++)
            {
                updatable[i].Update(gameTime);
            }

            base.Update(gameTime);
        }
    }
}
