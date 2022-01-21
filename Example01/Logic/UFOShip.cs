using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics.GUI;
using XGameEngine.Helpers;
using XGameEngine.Logic.Input;

namespace Example01.Logic
{
    /// <summary>
    /// Represents a moveable ship that the player can destroy for a large amount of points.
    /// </summary>
    public class UFOShip : ShipGameObject
    {        
        // Min/Max of the rotation speed of the UFO.
        private static float MinRotationSpeed = 0.004f;
        private static float MaxRotationSpeed = 0.007f;

        /// <summary>
        /// Current rotation speed of the UFO.
        /// </summary>
        private float RotationSpeed = 0f;
        
        /// <summary>
        /// The direction the UFO will travel from left or right.
        /// </summary>
        private Direction ChosenDirectionX = Direction.Right;

        /// <summary>
        /// The direction the UFO will travel from top or bottom.
        /// </summary>
        private Direction ChosenDirectionY = Direction.Top;

        /// <summary>
        /// Initializes a new instance of the <see cref="UFOShip"/> instance.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        public UFOShip(XGame game, UFOResource resource, Color color) 
            : base(game, resource.Texture, Vector2.Zero, color)
        {
            this.Points = 5000;
            this.Resource = resource;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UFOShip"/> instance.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        /// <param name="effects"></param>
        /// <param name="depth"></param>
        public UFOShip(XGame game, UFOResource resource, Vector2 position, Color color, float rotation, float scale, SpriteEffects effects, float depth) 
            : base(game, resource.Texture, position, color, rotation, scale, effects, depth)
        {
            this.Points = 5000;
            this.Resource = resource;
        }

        /// <summary>
        /// Gets or sets the UFO resource.
        /// </summary>
        public UFOResource Resource
        {
            get { return this._Resource; }
            set { this._Resource = value; }
        }
        private UFOResource _Resource;

        TextBlock b = null;
        double g = 0;

        /// <summary>
        /// Update the UFO position.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            switch (ChosenDirectionX)
            {
                case Direction.Right:
                    this.Position -= new Vector2(1, 0);
                    break;
                case Direction.Left:
                    this.Position += new Vector2(1, 0);
                    break;
            }

            //this.Rotation += RotationSpeed;

            if (b != null)
            {
                PlayerShip p = Game1.World.Player;


                Vector2 direction = p.Position - this.Position;
                direction.Normalize();
                float rotationInRadians = (float)Math.Atan2((double)direction.Y,
                                             (double)direction.X) + MathHelper.PiOver2;

                MouseState mouseState = Mouse.GetState();
                double f = Math.Atan2((double)mouseState.Y - this.Position.Y, (double)mouseState.X - this.Position.X);
                g = f;

                float xDiff = p.Position.X - this.Position.X;
                float yDiff = p.Position.Y - this.Position.Y;


                //float xDiff = this.Position.X - p.Position.X;
                //float yDiff = this.Position.Y - p.Position.Y;
                double r = Math.Atan2(yDiff, xDiff);
                //g = (r * (180/Math.PI));

                b.Text = f.ToString();

                if (InputState.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Q, PlayerIndex.One, StateOptions.CurrentFavor))
                {
                    ShipProjectile proj = new ShipProjectile
                        (this.Game, this, this.Resource.Projectile, this.Position, this.Velocity, Color.White); 
                    proj.Initialize();
                    proj.Rotation = (float)g;


                    Game1.World.AddProjectile(proj);
                }
            }


            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            sprite.Draw(base.Game.EngineResource.Dummy, new Rectangle((int)
                this.Position.X, (int)this.Position.Y, 10, 500), null, Color.White, (float)g, new Vector2(1,1), SpriteEffects.None, 1f);
            base.Draw(sprite, gameTime);
        }

        /// <summary>
        /// Initialize the UFO. Create the proper properties.
        /// </summary>
        public override void Initialize()
        {
            b = new TextBlock(this.Game, "", new Vector2(100, 100));
            this.Children.Add(b);

            // Choose random directions for X and Y.
            // Left side of the screen or right
            // Top of the screen or the bottom.
            this.ChosenDirectionX = (Direction)RandomNum.Next(0, 2);
            this.ChosenDirectionY = (Direction)RandomNum.Next(2, 4);

            float x = 0;
            float y = 0;

            // Choose X axis.
            switch (ChosenDirectionX)
            {
                case Direction.Right:
                    x = this.Game.View.Width + this.Texture.Width;
                    break;
                case Direction.Left:
                    x = -this.Texture.Width;
                    break;
            }

            // Choose Y axis.
            switch (ChosenDirectionY)
            {
                case Direction.Top:
                    y = this.Texture.Height + 50;
                    break;
                case Direction.Bottom:
                    y = this.Game.View.Height - this.Texture.Height - 50;
                    break;
            }

            // Set position.
            this.Position = new Vector2(x, y);

            // Get a random rotation speed. Then make sure it's correct.
            RotationSpeed = RandomNum.GetRandomFloat(MinRotationSpeed, MaxRotationSpeed);
            if (RotationSpeed < MinRotationSpeed) RotationSpeed = MinRotationSpeed;
            if (RotationSpeed > MaxRotationSpeed) RotationSpeed = MaxRotationSpeed;

            base.Initialize();
        }
    }
}
