using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Represents the base of a text effect that can affect <see cref="TextBlock"/> controls.
    /// </summary>
    public abstract class TextEffect : IGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextEffect"/> class.
        /// </summary>
        /// <param name="text"></param>
        public TextEffect(UITextObject text)
        {
            this.Text = text;
        }

        /// <summary>
        /// ID of the control.
        /// </summary>
        public int ID { get; protected set; }

        /// <summary>
        /// Whether this control is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The control to modify.
        /// </summary>
        public UITextObject Text { get; set; }

        /// <summary>
        /// Draw the control.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Draw(SpriteBatch sprite, GameTime gameTime);

        /// <summary>
        /// Update the control.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);
    }
}
