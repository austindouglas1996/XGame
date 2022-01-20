using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;

namespace Example01.Logic
{
    public abstract class ShipGameObject : SpaceGameObject
    {
        protected ShipGameObject(XGame game, Texture2D texture, Vector2 position, Color color) 
            : base(game, texture, position, color)
        {
        }

        protected ShipGameObject(XGame game, Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects effects, float depth) 
            : base(game, texture, position, color, rotation, scale, effects, depth)
        {
        }

        /// <summary>
        /// Accelerate the ship from the current position.
        /// </summary>
        public virtual void Accelerate()
        {
            // Update the ship velocity.
            this.Velocity += new Vector2
                ((float)(Math.Cos(this.Rotation - MathHelper.PiOver2) * 1.0f),
                (float)((Math.Sin(this.Rotation - MathHelper.PiOver2) * 1.0f)));

            // Represents the max and minimum value.
            float maxValue = 2.0f;
            float minValue = -2.0f;

            // Change the ship velocity depending on value.
            if (this.Velocity.X > maxValue)
            {
                this.Velocity = new Vector2(maxValue, this.Velocity.Y);
            }

            if (this.Velocity.X < minValue)
            {
                this.Velocity = new Vector2(minValue, this.Velocity.Y);
            }

            if (this.Velocity.Y > maxValue)
            {
                this.Velocity = new Vector2(this.Velocity.X, maxValue);
            }

            if (this.Velocity.Y < minValue)
            {
                this.Velocity = new Vector2(this.Velocity.X, minValue);
            }
        }

        /// <summary>
        /// Decelerate the ship from the current position.
        /// </summary>
        public virtual void Decelerate()
        {
            // Change the ship velocity depending on value.
            if (this.Velocity.X < 0)
            {
                this.Velocity = new Vector2(this.Velocity.X + 0.02f, this.Velocity.Y);
            }

            if (this.Velocity.X > 0)
            {
                this.Velocity = new Vector2(this.Velocity.X - 0.02f, this.Velocity.Y);
            }

            if (this.Velocity.Y < 0)
            {
                this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y + 0.02f);
            }

            if (this.Velocity.Y > 0)
            {
                this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - 0.02f);
            }
        }
    }
}
