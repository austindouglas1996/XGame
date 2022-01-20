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
        /// <param name="game"></param>
        public TextBlock(XGame game, string text, Vector2 pos)
            : base(game)
        {
            base.Text = text;
            base.Position = pos;
        }
    }
}
