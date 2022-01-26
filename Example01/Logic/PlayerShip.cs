
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics;
using XGameEngine.Logic.Input;

namespace Example01.Logic
{
    public class PlayerShip : ShipGameObject
    {
        private TimeSpan TimeBetweenShots = TimeSpan.FromSeconds(0.2);
        private TimeSpan LastProjectileShot = TimeSpan.Zero;

        public PlayerShip(XGame game, Texture2D texture, ProjectileResource projectile, Vector2 position, Color color) 
            : base(game, texture, position, color)
        {
            this.Projectile = projectile;
            this.Collision += PlayerShip_Collision;
        }

        public ProjectileResource Projectile
        {
            get { return _Projectile; }
            set { _Projectile = value; }
        }
        private ProjectileResource _Projectile;

        public float RotationSpeed = 0.04f;

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Go up?
            if (InputState.KeyPressed(GlobalKeys.RaiseKey, PlayerIndex.One, StateOptions.Current))
                base.Accelerate();

            // Go down?
            if (InputState.KeyPressed(GlobalKeys.LowerKey, PlayerIndex.One, StateOptions.Current))
                base.Decelerate();

            // Go Right?
            if (InputState.KeyPressed(GlobalKeys.RightSideKey, PlayerIndex.One, StateOptions.Current))
                this.Rotation += RotationSpeed;

            // Go left?
            if (InputState.KeyPressed(GlobalKeys.LeftSideKey, PlayerIndex.One, StateOptions.Current))
                this.Rotation -= RotationSpeed;

            // Shoot?
            if (InputState.KeyPressed(GlobalKeys.ShootKey, PlayerIndex.One, StateOptions.Current))
            {
                if (gameTime.TotalGameTime.TotalSeconds - LastProjectileShot.TotalSeconds > TimeBetweenShots.TotalSeconds)
                {
                    this.ShootProjectile();
                    LastProjectileShot = TimeSpan.FromSeconds(gameTime.TotalGameTime.TotalSeconds);
                }
            }

            this.Game.Camera.Position = this.Position - new Vector2(800,800);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            base.Draw(sprite, gameTime);
        }

        private void ShootProjectile()
        {
            MediaPlayer.Play(Resources.ProjectileSounds[0]);

            ShipProjectile project = new ShipProjectile
                (this.Game, this, this.Projectile, this.Position, this.Velocity, Color.White);
            project.Initialize();
            project.PositionType = PositionType.Absolute;

            Game1.World.AddProjectile(project);
        }

        private void PlayerShip_Collision(SpaceGameObjectEventArgs e)
        {
            if (e.Sender.GetType() == typeof(Meteor))
            {
                this.IsActive = false;
                e.Sender.IsActive = false;
            }
        }
    }
}
