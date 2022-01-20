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
    public class ShipProjectile : Projectile
    {
        public ShipProjectile(XGame game, ShipGameObject ship, ProjectileResource model, Vector2 position, Vector2 velocity, Color color) 
            : base(game, model.Texture, position, velocity, color)
        {
            Ship = ship;
        }

        public ShipProjectile(XGame game, ShipGameObject ship, ProjectileResource model, Vector2 position, Vector2 velocity, Color color, float rotation, float scale, SpriteEffects effects, float depth) 
            : base(game, model.Texture, position, velocity, color, rotation, scale, effects, depth)
        {
            Ship = ship;
        }

        public ProjectileResource Model
        {
            get { return _Model; }
            set { _Model = value; }
        }
        private ProjectileResource _Model;

        public override void Initialize()
        {
            // Set the velocity.
            this.Velocity = new Vector2(
                (float)Math.Cos(Ship.Rotation - (float)MathHelper.PiOver2),
                (float)Math.Sin(Ship.Rotation - (float)MathHelper.PiOver2))
                * 3f + Ship.Velocity;

            this.Rotation = Ship.Rotation;

            // Set the position.
            // (Add some distance between the projectile and the ship.)
            this.Position = Ship.Position + this.Velocity * 2f;

            this.Collision += ShipProjectile_Collision;

            base.Initialize();
        }

        private void ShipProjectile_Collision(SpaceGameObjectEventArgs e)
        {
            if (e.Sender.GetType() == typeof(Meteor))
            {
                this.Ship.Points += e.Sender.Points;
                e.Sender.IsActive = false;
                this.IsActive = false;
            }
        }

        public ShipGameObject Ship
        {
            get { return _Ship; }
            set { _Ship = value; }
        }
        private ShipGameObject _Ship;
    }
}
