using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics;

namespace Example01.Logic
{
    public class SpaceGameObject : Sprite
    {
        public SpaceGameObject(XGame game, Texture2D texture, Vector2 position, Color color) 
            : base(game, texture, position, color)
        {
            Points = 0;
        }

        public SpaceGameObject(XGame game, Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects effects, float depth) 
            : base(game, texture, position, color, rotation, scale, effects, depth)
        {
            Points = 0;
        }

        /// <summary>
        /// Event invoked on collision from another object.
        /// </summary>
        public event SpaceGameObjectHandler Collision;

        /// <summary>
        /// Total velocity of the space object is moving at. 
        /// </summary>
        public Vector2 Velocity = Vector2.Zero;

        /// <summary>
        /// Total amount of points this object is worth on destruction.
        /// </summary>
        public virtual int Points { get; set; }

        public void InvokeCollision(SpaceGameObject collider)
        {
            if (this.Collision != null)
                this.Collision(new SpaceGameObjectEventArgs(collider));
        }

        public override void Update(GameTime gameTime)
        {
            this.Position += this.Velocity;

            base.Update(gameTime);
        }
    }
}
