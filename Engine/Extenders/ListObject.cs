using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XGameEngine.Logic;

namespace XGameEngine.Extenders
{
    /// <summary>
    /// Extends the base of <see cref="List"/> with basic update and render capabilites.
    /// </summary>
    /// <typeparam name="T">Value</typeparam>
    public class ListObject<T> : List<T>
        where T : GameObject
    {
        /// <summary>
        /// ALlows us to automatically delete inactive values.
        /// </summary>
        private bool autoDelete = true;

        /// <summary>
        /// Gets or sets a value indicating whether to delete inactive values.
        /// </summary>
        public bool AutoDelete
        {
            get { return this.autoDelete; }
            set { this.autoDelete = value; }
        }

        /// <summary>
        /// Gets a value indicating whether a child element is valid.
        /// </summary>
        /// <param name="element">Element to be checked.</param>
        /// <returns>bool</returns>
        private bool ChildIsValid(T element)
        {
            if (!element.IsActive)
            {
                if (this.autoDelete)
                    this.Remove(element);

                return false;
            }

            if (element.Visible == Visibility.Hidden)
                return false;

            return true;
        }

        /// <summary>
        /// Draw the list of elements.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime)
        {
            for (int i = 0; i < base.Count; i++)
            {
                // Hold the element to execute certain methods.
                dynamic element = base[i];

                // Make sure the element is OK to draw.
                if (ChildIsValid(base[i]))
                {
                    // Draw.
                    element.Draw(gameTime);
                }
            }
        }

        /// <summary>
        /// Update the list of elements.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < base.Count; i++)
            {
                // Hold the element to execute certain methods.
                T element = base[i];

                if (element.IsInitialized == false)
                    element.Initialize();

                // Make sure the element is OK to update.
                if (ChildIsValid(base[i]))
                {
                    // Draw.
                    element.Update(gameTime);
                }
            }
        }
    }
}
