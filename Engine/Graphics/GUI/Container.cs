using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Core;
using XGameEngine.Extenders;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Contains an array of UIElements.
    /// </summary>
    public class Container : UIObject
    {
        /// <summary>
        /// Holds the window elements.
        /// </summary>
        private ListObject<UIObject> children = new ListObject<UIObject>();

        /// <summary>
        /// Holds the window children relative position.
        /// </summary>
        private List<Vector2> childrenPosition = new List<Vector2>();

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

        /// <summary>
        /// Add a new element to the container.
        /// </summary>
        /// <param name="child">Child to be added.</param>
        public void Add(UIObject child)
        {
            // Add child.
            children.Add(child);

            // (Important: Add child position before modifying position.)
            childrenPosition.Add(child.Position);

            // Set children position to be relative with the container.
            child.Position = this.Position + child.Position;
        }

        /// <summary>
        /// Remove all child elements.
        /// </summary>
        public void Clear()
        {
            this.children.Clear();
        }

        /// <summary>
        /// Draw the container elements.
        /// </summary>
        /// <param name="gametime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch sprite, GameTime gametime)
        {
            if (this.Visible == Visibility.Visible)
            {
                // Draw the container first.
                base.Draw(sprite, gametime);

                // Draw.
                this.children.Draw(gametime);
            }
        }

        /// <summary>
        /// Find an existing child by name.
        /// </summary>
        /// <param name="name">Name of the child to be found.</param>
        /// <returns>The first child found with the matching name.</returns>
        public UIObject Find(string name)
        {
            for (int i = 0; i < this.children.Count; i++)
            {
                if (this.children[i].Name == name)
                {
                    return this.children[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Find an existing child by parent.
        /// </summary>
        /// <param name="parent">Parent of the child to be found.</param>
        /// <returns>The first child found with the matching name.</returns>
        public UIObject Find(UIObject parent)
        {
            // Make sure parent is not null.
            if (parent == null)
            {
                throw new ArgumentNullException
                    ("Parent cannot be null.");
            }

            for (int i = 0; i < this.children.Count; i++)
            {
                if (this.children[i].Parent == parent)
                {
                    return this.children[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Find an existing child position.
        /// </summary>
        /// <param name="child">Child to be found.</param>
        /// <returns>The position of the child.</returns>
        public int FindIndex(UIObject child)
        {
            for (int i = 0; i < this.children.Count; i++)
            {
                // Check for a match.
                if (this.children[i] == child)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Remove an existing child from the container.
        /// </summary>
        /// <param name="child">Child to be removed.</param>
        public void Remove(UIObject child)
        {
            // Make sure the child exists.
            if (!this.children.Contains(child))
            {
                throw new ArgumentNullException
                    ("The container does not contain this child.");
            }

            // Remove.
            this.children.Remove(child);
            this.childrenPosition.RemoveAt(this.FindIndex(child));
        }

        /// <summary>
        /// Remove an existing child from the container by position.
        /// </summary>
        /// <param name="index">Position of the child.</param>
        public void RemoveAt(int index)
        {
            // Make sure it does not go under or above the count.
            if (index > this.children.Count ||
                index < 0)
            {
                throw new IndexOutOfRangeException
                    ("Index cannot be smaller or larger than container collection.");
            }

            // Remove.
            this.children.RemoveAt(index);
            this.childrenPosition.RemoveAt(index);
        }

        /// <summary>
        /// Update the container elements.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update the children.
            this.children.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Update the window position.
        /// </summary>
        protected void UpdatePosition()
        {
            // Update elements relative to the window.
            for (int i = 0; i < this.children.Count; i++)
            {
                // Set new position.
                this.children[i].Position = 
                    this.Position + this.childrenPosition[i];
            }
        }
    }
}
