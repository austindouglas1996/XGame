using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Helpers;

namespace Example01.Logic
{
    public class ShipPart : SpaceGameObject
    { 
        /// <summary> 
        /// Minimum rotation speed for the meteor.
        /// </summary>
        private static readonly float RotationSpeedMin = -0.04f;

        /// <summary>
        /// Maximum rotation speed for the meteor.
        /// </summary>
        private static readonly float RotationSpeedMax = 0.03f;

        /// <summary>
        /// The speed to rotate the meteor.
        /// </summary>
        private float RotationSpeed = 0f;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipPart"/> class.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public ShipPart(XGame game, Texture2D texture, Vector2 position, Color color) 
            : base(game, texture, position, color)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipPart"/> class.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        /// <param name="effects"></param>
        /// <param name="depth"></param>
        public ShipPart(XGame game, Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects effects, float depth) 
            : base(game, texture, position, color, rotation, scale, effects, depth)
        {
        }

        public override void Initialize()
        {
            this.GenerateProps();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            this.Rotation += this.RotationSpeed;
            base.Update(gameTime);
        }

        /// <summary>
        /// Randomly generates some of the properties for the meteor. 
        /// </summary>
        private void GenerateProps()
        {
            // Set the rotation speed and then make sure it does not go past our limit.
            this.RotationSpeed = RandomNum.GetRandomFloat(RotationSpeedMin, RotationSpeedMax);
            this.RotationSpeed = Math.Clamp(this.RotationSpeed, RotationSpeedMin, RotationSpeedMax);

            // Set random velocity speed.
            this.Velocity = new Vector2(
                (float)RandomNum.GetNextDouble() * ((float)RandomNum.GetNextDouble(-2f, 2f) - 1),
                (float)RandomNum.GetNextDouble() * ((float)RandomNum.GetNextDouble(-2f, 2f) - 1));

            // Set random rotation and rotation speed.
            this.Rotation = (float)RandomNum.GetNextDouble()
                * MathHelper.Pi
                * (RandomNum.Next(1, 8)
                - (MathHelper.Pi * 2));
        }
    }
}
