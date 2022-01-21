using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Core;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Represents a basic text element.
    /// </summary>
    public class TextBlock : UITextObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class.
        /// </summary>
        /// <param name="obj">Parent element.</param>
        /// <param name="text">Text to be displayed.</param>
        /// <param name="pos">Position of the element.</param>
        public TextBlock(UIObject obj, string text, Vector2 pos)
            : this(obj.Game, text, pos)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class.
        /// </summary>
        /// <param name="game"></param>
        public TextBlock(XGame game, string text, Vector2 pos)
            : base(game)
        {
            base.Text = text;
            base.Position = pos;
        }
    }
}
