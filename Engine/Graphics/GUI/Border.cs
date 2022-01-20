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
    /// Represents an extending border around a element.
    /// </summary>
    public class Border : UIObject
    {
        /// <summary>
        /// The thickness of the child.
        /// </summary>
        private Thickness thickness;

        /// <summary>
        /// The border child.
        /// </summary>
        private UIObject child;

        /// <summary>
        /// Initializes a new instance of the <see cref="Border"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        public Border(XGame game)
            : base(game)
        {
            this.thickness = new Thickness(0, 0, 0, 0);
        }

        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        public Thickness Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        /// <summary>
        /// Gets or sets the child to render around.
        /// </summary>
        public UIObject Child
        {
            get { return this.child; }
            set 
            { 
                this.child = value;
                this.SetChild(value);
            }
        }

        /// <summary>
        /// Draw the border element.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            // Draw the base first.
            base.Draw(sprite, gameTime);

            List<Rectangle> borders = new List<Rectangle>();

            int difference = (int)this.thickness.Spacing;

            Rectangle right, bottom, left, top;

            // Create borders.
            right = new Rectangle
                ((int)this.child.Position.X + (int)this.child.ActualWidth + difference,
                (int)this.child.Position.Y - difference,
                (int)this.thickness.Right, (int)this.child.ActualHeight + (difference * 2));

            bottom = new Rectangle
                ((int)this.child.Position.X - (int)this.thickness.Left - difference,
                (int)this.child.Position.Y  + (int)this.child.ActualHeight + difference,
                (int)this.thickness.Left + (int)this.child.ActualWidth + (int)this.thickness.Right + (difference * 2), (int)this.thickness.Bottom);

            left = new Rectangle
                ((int)this.child.Position.X - (int)this.thickness.Left - difference,
                (int)this.child.Position.Y - difference,
                (int)this.thickness.Left, (int)this.child.ActualHeight + (difference * 2));

            top = new Rectangle
                ((int)this.child.Position.X - (int)this.thickness.Left - difference,
                (int)this.child.Position.Y - (int)this.thickness.Top - difference,
                (int)this.thickness.Left + (int)this.child.ActualWidth + (int)this.thickness.Right + (difference * 2), (int)this.thickness.Top);







            borders.Add(right);
            borders.Add(bottom);
            borders.Add(left);
            borders.Add(top);

            for (int i = 0; i < borders.Count; i++)
            {
                base.Game.Render.SpriteBatch.Draw
                    (base.Game.EngineResource.Dummy, borders[i], this.Background);
            }
        }

        /// <summary>
        /// Set the element child.
        /// </summary>
        /// <param name="child">Child.</param>
        protected void SetChild(UIObject child)
        {
            // Set the parent.
            child.Parent = this;
        }
    }
}
