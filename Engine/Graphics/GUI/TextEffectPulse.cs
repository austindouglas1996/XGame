using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Creates a simple pulsing effect with a <see cref="UITextObject"/> to increase and lower the scale.
    /// </summary>
    public class TextEffectPulse : TextEffect
    {
        /// <summary>
        /// The amount of time between each change.
        /// </summary>
        private TimeSpan Difference = TimeSpan.Zero;

        /// <summary>
        /// The last game change.
        /// </summary>
        private TimeSpan LastChange = TimeSpan.Zero;

        /// <summary>
        /// The lowest value the scale be lowered to.
        /// </summary>
        private float _Min = 0f;

        /// <summary>
        /// The maximum value the can be scaled to.
        /// </summary>
        private float _Max = 0f;

        /// <summary>
        /// How much to increase the scale each change.
        /// </summary>
        private float _Rate = 0f;

        /// <summary>
        /// The original scale.
        /// </summary>
        private float _Original = 0;

        /// <summary>
        /// Internal: Increasing the scale or decreasing the scale.
        /// </summary>
        private bool Increasing = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEffectPulse"/> class.
        /// </summary>
        /// <param name="text">Object to modify.</param>
        /// <param name="scaleMin">The lowest scale value we can go.</param>
        /// <param name="scaleMax">The highest scale value we can go.</param>
        /// <param name="rate">The amount to change per pulse.</param>
        /// <param name="diff">The amount of time to wait between each pulse.</param>
        public TextEffectPulse(UITextObject text, float scaleMin, float scaleMax, float rate, TimeSpan diff)
            : base(text)
        {
            _Original = text.Scale; 
            _Min = text.Scale - scaleMin;
            _Max = text.Scale + scaleMax;
            _Rate = rate;
            Difference = diff;
        }

        /// <summary>
        /// No drawing done here.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
        }

        /// <summary>
        /// Update the text element.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // First time the update is called.
            if (LastChange == TimeSpan.Zero)
                LastChange = gameTime.ElapsedGameTime;

            if (Text.Scale > _Max)
                Increasing = false;

            else if (Text.Scale < _Min)
                Increasing = true;

            if (Increasing)
            {
                Text.Scale += (float)(gameTime.ElapsedGameTime.TotalMilliseconds) * _Rate;
            }
            else
            {
                Text.Scale -= (float)(gameTime.ElapsedGameTime.TotalMilliseconds) * _Rate;
            }
        }
    }
}
