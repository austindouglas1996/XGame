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
        private static float MinRotationSpeed = 0.024f;
        private static float MaxRotationSpeed = 0.027f;

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

            this.Rotation += this.RotationSpeed;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            base.Draw(sprite, gameTime);
        }

        /// <summary>
        /// Initialize the UFO. Create the proper properties.
        /// </summary>
        public override void Initialize()
        {
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
            RotationSpeed = Math.Clamp(RotationSpeed, MinRotationSpeed, MaxRotationSpeed);

            base.Initialize();
        }
    }
}
