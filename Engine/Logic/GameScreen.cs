using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Core;

namespace XGameEngine.Logic
{
    /// <summary>
    /// Represents the base of a game screen.
    /// </summary>
    public abstract class GameScreen : GameObject
    {
        /// <summary>
        /// Does this screen render the entire screen or can it be rendered with other screens?
        /// </summary>
        private bool isTranslucent = false;

        /// <summary>
        /// Gets the amount of time the screen has been active.
        /// </summary>
        private TimeSpan lifeSpan = TimeSpan.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="Screen"/> class.
        /// </summary>
        public GameScreen(XGame game)
            : base(game, new Vector2(1,1))
        {

        }

        /// <summary>
        /// Gets a value indicating whether the screen can render with other screens.
        /// </summary>
        public bool IsTranslucent
        {
            get { return this.isTranslucent; }
            protected set { this.isTranslucent = value; }
        }

        /// <summary>
        /// Gets the time the screen has been active.
        /// </summary>
        public TimeSpan LifeSpan
        {
            get { return this.lifeSpan; }
        }

        /// <summary>
        /// Set the screen as active.
        /// </summary>
        public virtual void Activated()
        {
            this.IsActive = true;
        }

        /// <summary>
        /// Set the screen as inactive.
        /// </summary>
        public virtual void Deactived()
        {
            this.IsActive = false;
        }

        /// <summary>
        /// Dispose of screen resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// Draw the game screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            base.Draw(sprite, gameTime);
        }

        /// <summary>
        /// Update the game screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Update lifeTime.
            this.lifeSpan += gameTime.ElapsedGameTime;

            // Update input.
            this.UpdateInput(gameTime);
        }

        /// <summary>
        /// Update the game input.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected abstract void UpdateInput(GameTime gameTime);
    }
}
