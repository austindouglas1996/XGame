using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Common;
using XGameEngine.Core;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Represents a series of text for an allocated amount of time.
    /// </summary>
    public class TextTimer : TextBlock
    {
        /// <summary>
        /// Holds the lifetime of this element.
        /// </summary>
        private TimedEvent interval;

        /// <summary>
        /// Initializes an ew instance of the <see cref="TextTimer"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <param name="lifeSpan">The lifespan of this element.</param>
        public TextTimer(XGame game, TimeSpan lifeSpan)
            : base(game, "", Vector2.Zero)
        {
            // Initialize.
            this.interval = new TimedEvent(lifeSpan, this.OnEnd);
        }

        /// <summary>
        /// Gets the remaining lifespan of this timer.
        /// </summary>
        public TimeSpan RemainingTime
        {
            get { return this.interval.RemainingTime; }
        }

        /// <summary>
        /// Update the element.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            this.interval.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Raised once interval has ended.
        /// </summary>
        /// <param name="sender">The calling object.</param>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnEnd(object sender, EventArgs e)
        {
            this.IsActive = false;
        }
    }
}
