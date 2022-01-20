using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Core;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Contains an array of UIElements.
    /// </summary>
    public class Container : UIObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        public Container(XGame game)
            : base(game)
        {
            // Set the background.
            base.Background = new Color(0, 0, 0, 50);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="position">Position</param>
        public Container(XGame game, int width, int height, Vector2 position)
            : this(game)
        {
            this.Width = width;
            this.Height = height;
            this.Position = position;
        }
    }
}
