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
    /// <summary>
    /// Represents a deadly lock in outerspace.
    /// </summary>
    public class Meteor : SpaceGameObject
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
        /// Initializes a new instance of the <see cref="Meteor"/> class.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="resource"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public Meteor(XGame game, MeteorResource resource, Vector2 position, Color color) 
            : base(game, resource.Texture, position, color)
        {
            if (resource == null)
                throw new ArgumentNullException("Resources is null.");
            this.Resource = resource;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meteor"/> class.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="resource"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        /// <param name="effects"></param>
        /// <param name="depth"></param>
        public Meteor(XGame game, MeteorResource resource, Vector2 position, Color color, float rotation, float scale, SpriteEffects effects, float depth) 
            : base(game, resource.Texture, position, color, rotation, scale, effects, depth)
        {
            if (resource == null)
                throw new ArgumentNullException("Resources is null.");
            this.Resource = resource;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meteor"/> class with that of a parent.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="parent"></param>
        internal Meteor(XGame game, Meteor parent)
            : base(game, parent.Texture, parent.Position, parent.Color)
        {
            // Increase or decrease the velocity of the meteor.
            this.Velocity += parent.Velocity * Game1.Rand.Next(-6, 6);

            // Decrease the size of the meteor by at least 1 (If possible).
            bool sizeChanged = false;

            MeteorSize newSize = parent.Resource.Size;
            MeteorColour newColour = parent.Resource.Colour;

            do
            {
                // Determine the new size.
                int nextSize = Game1.Rand.Next(0, (int)parent.Resource.Size - 1);

                newSize = (MeteorSize)nextSize;

                // Make sure the size is acceptable.
                if ((int)newSize > 0)
                {
                    // Size is acceptable.
                    sizeChanged = true;
                }
            }
            while (!sizeChanged && (int)newSize == -1);

            // New color?
            if (Game1.Rand.Next(0, 100) < 35)
            {
                if (parent.Resource.Colour == MeteorColour.Brown)
                    parent.Resource.Colour = MeteorColour.Gray;
                else
                    parent.Resource.Colour = MeteorColour.Brown;
            }

            this.Resource = Resources.GetMeteorResourceFromProp(newSize, newColour);
            this.Texture = this.Resource.Texture;
        }

        /// <summary>
        /// Returns the amount of points this meteor is worth.
        /// </summary>
        public override int Points
        {
            get
            {
                switch(this.Resource.Size)
                {
                    case MeteorSize.Tiny:
                        return 100;
                    case MeteorSize.Small:
                        return 250;
                    case MeteorSize.Medium:
                        return 500;
                    case MeteorSize.Big:
                        return 750;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// Gets the resources that were used to make this resource.
        /// </summary>
        public MeteorResource Resource
        {
            get => _Resource;
            private set => _Resource = value;
        }
        private MeteorResource _Resource;

        /// <summary>
        /// Initialize the meteor.
        /// </summary>
        public override void Initialize()
        {
            this.GenerateProps();
            this.ActiveChange += Meteor_ActiveChange;
            base.Initialize();
        }

        /// <summary>
        /// Update the meteor.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            this.Rotation += RotationSpeed;
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

        /// <summary>
        /// Create child meteors on death.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Meteor_ActiveChange(object sender, EventArgs e)
        {
            // Check if the meteor has died.
            if (!this.IsActive)
            {
                // The minimum and maximum amount of smaller
                // asteroids to spawn.
                int max = 0;
                int min = 0;

                switch (this.Resource.Size)
                {
                    case MeteorSize.Big:
                        max = 4;
                        min = 3;
                        break;
                    case MeteorSize.Medium:
                        max = 3;
                        min = 2;
                        break;
                    case MeteorSize.Small:
                        max = 2;
                        min = 0;
                        break;
                    case MeteorSize.Tiny:
                        max = 0;
                        min = 0;
                        break;
                }

                // Get random amount.
                int amount = Game1.Rand.Next(min, max);

                for (int i = 0; i < amount; i++)
                {
                    Meteor newMeteor = new Meteor(this.Game, this);
                    Game1.World.AddMeteor(newMeteor);
                }
            }

        }
    }
}
