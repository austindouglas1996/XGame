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
    public abstract class Projectile : SpaceGameObject
    {
        protected Projectile(XGame game, Texture2D texture, Vector2 position, Vector2 velocity, Color color) 
            : base(game, texture, position, color)
        {
            base.Velocity = velocity;
        }

        protected Projectile(XGame game, Texture2D texture, Vector2 position, Vector2 velocity, Color color, float rotation, float scale, SpriteEffects effects, float depth) 
            : base(game, texture, position, color, rotation, scale, effects, depth)
        {
            base.Velocity = velocity;
        }

        public TimeSpan LifeSpan = TimeSpan.Zero;

        public override void Update(GameTime gameTime)
        {
            // Reduce life.
            LifeSpan -= gameTime.ElapsedGameTime;

            // No more projectile.
            if (LifeSpan.TotalMilliseconds == 0)
                this.IsActive = false;

            base.Update(gameTime);
        }
    }
}
