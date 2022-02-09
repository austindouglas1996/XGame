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
    /// Represents a UIElement with text capabilities.
    /// </summary>
    public abstract class UITextObject : UIObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UITextObject"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        public UITextObject(XGame game)
            : base(game)
        {
            // Set default values.
            this.font = game.EngineResource.Load<SpriteFont>("defaultFont");

            // Create a new list of effects.
            this.TextEffects = new List<TextEffect>();
        }

        /// <summary>
        /// Raised when the <see cref="Text"/> changes.
        /// </summary>
        public event EventHandler TextChanged;

        /// <summary>
        /// Gets or sets the text foreground.
        /// </summary>
        public Color Foreground
        {
            get { return this.foreground; }
            set { this.foreground = value; }
        }
        private Color foreground = new Color(0, 0, 0);

        /// <summary>
        /// Gets the height of the actual text based on font.
        /// </summary>
        public override int ActualHeight
        {
            get { return (int)(this.font.MeasureString(this.text).Y * this.Scale); }
        }

        /// <summary>
        /// Gets the width of the actual text based on font.
        /// </summary>
        public override int ActualWidth
        {
            get { return (int)(this.font.MeasureString(this.text).X * this.Scale); }
        }

        /// <summary>
        /// Contains a list of text effects for text processing.
        /// </summary>
        public List<TextEffect> TextEffects
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        public virtual SpriteFont Font
        {
            get { return this.font; }
            set { this.font = value; }
        }
        private SpriteFont font;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public virtual string Text
        {
            get { return this.text; }
            set 
            { 
                this.text = value; 
                if (this.TextChanged != null)
                    this.TextChanged(this, EventArgs.Empty);
            }
        }
        private string text = string.Empty;

        /// <summary>
        /// Draw the text.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            base.Draw(sprite, gameTime);

            if (base.Visible == Visibility.Visible)
            {
                Vector2 origin = new Vector2(this.ActualWidth / 2, this.ActualHeight / 2);

                base.Game.WorldRender.SpriteBatch.DrawString
                    (this.font, this.text, this.ScreenPosition, this.foreground, this.Rotation,
                    new Vector2(-3,0), this.Scale, SpriteEffects.None, 1f);

                foreach (var effect in this.TextEffects)
                    effect.Draw(sprite, gameTime);
            }
        }

        /// <summary>
        /// Update the base controls.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (var effect in this.TextEffects)
                effect?.Update(gameTime);

            base.Update(gameTime);
        }
    }
}
