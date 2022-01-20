using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Common
{
    /// <summary>
    /// Executes an event after a certain amount of time has passed.
    /// </summary>
    public class TimedEvent
    {
        /// <summary>
        /// Restart the timer after each interval?
        /// </summary>
        private bool loopEvent = true;

        /// <summary>
        /// The amount of time to wait each time before executing the event.
        /// </summary>
        private TimeSpan time = TimeSpan.Zero;

        /// <summary>
        /// Holds the amount of time remaining before executing the event.
        /// </summary>
        private TimeSpan remainingTime = TimeSpan.Zero;

        /// <summary>
        /// Raised when the time has ran out.
        /// </summary>
        public event EventHandler interval;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedEvent"/> class.
        /// </summary>
        /// <param name="timeInterval">The time between each event call.</param>
        /// <param name="interval">The event to be raised at the end of each interval.</param>
        public TimedEvent(TimeSpan timeInterval, EventHandler interval)
        {
            this.time = timeInterval;
            this.interval += interval;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to reset the timer after each interval.
        /// </summary>
        public bool LoopEvent
        {
            get { return this.loopEvent; }
            set { this.loopEvent = value; }
        }

        /// <summary>
        /// Gets or sets the time between each event call.
        /// </summary>
        public TimeSpan Time
        {
            get { return this.time; }
            set { this.time = value; }
        }

        /// <summary>
        /// Gets the remaining time until the event is called.
        /// </summary>
        public TimeSpan RemainingTime
        {
            get { return this.remainingTime; }
        }

        /// <summary>
        /// Update the event timer.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            this.remainingTime -= gameTime.ElapsedGameTime;

            if (this.remainingTime < TimeSpan.Zero)
            {
                this.OnInterval(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raised when the time has reached zero.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnInterval(EventArgs e)
        {
            if (this.interval != null)
            {
                this.interval(this, e);
                this.remainingTime = this.time;
            }
        }
    }
}
