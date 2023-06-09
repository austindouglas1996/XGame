using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics;
using XGameEngine.Logic.Camera;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Example01.Logic
{
    /// <summary>
    /// Manages the stars seen in the world background.
    /// </summary>
    public class StarManager : GameObject
    {
        private GameWorld _World;
        private GameCamera _Camera;
        private List<Sprite> _Stars = new List<Sprite>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StarManager"/> class.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="world"></param>
        /// <param name="camera"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public StarManager(XGame game, GameWorld world, GameCamera camera) 
            : base(game, new Vector2(0,0))
        {
            if (world == null)
            {
                throw new ArgumentNullException("World is null");
            }

            _World = world;
            _Camera = camera;
        }

        /// <summary>
        /// Add new stars to the world.
        /// </summary>
        /// <param name="amount"></param>
        public void AddStars(int amount)
        {
            Random random = new Random();

            int minX = -(_World.WorldWidth / 2);
            int maxX = _World.WorldWidth + (_World.WorldWidth / 2);

            int minY = -(_World.WorldHeight /2);
            int maxY = _World.WorldHeight + (_World.WorldHeight / 2);

            for (int i = 0; i < amount; i++)
            {            
                // Color of the star.
                int r = random.Next(20, 255);
                int g = random.Next(0, 255);
                int b = random.Next(0, 255);
                int a = random.Next(20, 255);

                // Change the size but anything above 0.6f seems too big.
                float scale = 0.6f;

                // Super star?
                int big = random.Next(1, 100);
                if (big == 99)
                {
                    a = 250;
                    scale = 1.0f;
                }


                // Random position.
                Vector2 pos = new Vector2(random.Next(minX, maxX), random.Next(minY, maxY));

                // New star.
                Sprite newStar = new Sprite(base.Game, Resources.GetRandomStar(), pos, new Color(r, g, b, a));
                newStar.Scale = scale;

                // Add the star.
                this._Stars.Add(newStar);
            }
        }

        /// <summary>
        /// Remove all of the world stars.
        /// </summary>
        public void ClearStars()
        {
            this._Stars.Clear();
        }

        /// <summary>
        /// Draw the world stars.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            float minX = _Camera.Position.X;
            float maxX = _Camera.Position.X + _Camera.Width + 500;

            float minY = _Camera.Position.Y;
            float maxY = _Camera.Position.Y + _Camera.Height + 500;

            foreach (var star in this._Stars)
            {
                star.Draw(gameTime);

                /*
                 * I'm seeing visual glitches occur when rendering what's in view.
                 * in this game it does not really matter (only displaying 1500).
                if (star.Position.X > minX && star.Position.X < maxX)
                {
                    if (star.Position.Y > minX && star.Position.Y < maxX)
                    {
                        //star.Draw(gameTime);
                    }
                }    
                */
            }

            base.Draw(gameTime);
        }
    }
}
