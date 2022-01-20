using Example01.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics;
using XGameEngine.Helpers;

namespace Example01
{
    public class GameWorld : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameWorld"/> class.
        /// </summary>
        /// <param name="game"></param>
        public GameWorld(XGame game) 
            : base(game, Vector2.Zero)
        {
        }

        /// <summary>
        /// Handls rendering the background stars.
        /// </summary>
        private PrimitiveBatch Primitive;

        /// <summary>
        /// List of initialized stars.
        /// </summary>
        private List<Vector2> Stars = new List<Vector2>();

        /// <summary>
        /// List of star colors.
        /// </summary>
        private List<Color> StarsColors = new List<Color>();

        /// <summary>
        /// Add a player to the world.
        /// </summary>
        /// <param name="ship"></param>
        public void AddPlayer(PlayerShip ship)
        {
            this.Children.Add(ship);
        }

        /// <summary>
        /// Add a projectile to the world.
        /// </summary>
        /// <param name="projec"></param>
        public void AddProjectile(Projectile projec)
        {
            this.Children.Add(projec);
        }

        /// <summary>
        /// Add a meteor to the world.
        /// </summary>
        /// <param name="meteor"></param>
        public void AddMeteor(Meteor meteor)
        {
            this.Children.Add(meteor);
        }

        /// <summary>
        /// Add a new random meteor given random properties.
        /// </summary>
        public void AddMeteorRandom()
        {
            int Max = Resources.Meteors.Count;

            MeteorResource resource = Resources.Meteors[Game1.Rand.Next(0, Max)];
            Meteor m = new Meteor(this.Game, resource, RandomPositionOutsideBounds(), Color.White);

            AddMeteor(m);
        }

        /// <summary>
        /// Remove only the players, but keep the asteroids.
        /// </summary>
        public void RemovePlayer()
        {
            var players = this.Children.Find(c => c.GetType() == typeof(PlayerShip));
            foreach (var player in players)
                player.IsActive = false;
        }

        /// <summary>
        /// Delete all children from the world.
        /// </summary>
        public void ResetWorld()
        {
            this.Children.RemoveAll();
        }

        /// <summary>
        /// Initializes world elements.
        /// </summary>
        public override void Initialize()
        {
            Random rand = new Random();
            this.Primitive = new PrimitiveBatch(base.Game.GraphicsDevice);

            for (int i = 0; i < 700; i++)
            {
                // Width and height of the game.
                int width = Game.View.Width;
                int height = Game.View.Height;

                // Size of the stars.
                int size = rand.Next(1, 2);

                // Positions of the star.
                int x = rand.Next(0, width);
                int y = rand.Next(0, height);

                // Color of the star.
                int r = rand.Next(0, 255);
                int g = rand.Next(0, 255);
                int b = rand.Next(0, 255);
                int a = rand.Next(0, 255);

                // The stars are made of triangles so add 3.
                Stars.Add(new Vector2(x, y));
                Stars.Add(new Vector2(x + size, y + size));
                Stars.Add(new Vector2(x - size, y + size));

                // Add 3 colors.
                StarsColors.Add(new Color(a, a, a));
                StarsColors.Add(new Color(a, a, a));
                StarsColors.Add(new Color(a, a, b));
            }

            base.Initialize();
        }

        /// <summary>
        /// Update all entities within the world.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (var child in this.Children.GetAll())
                child.Position = CheckAndResetChildBounds((SpaceGameObject)child);

            HandleCollisons(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws world elements.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="gameTime"></param>
        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            this.Primitive.Begin(PrimitiveType.TriangleList);
            for (int i = 0; i < this.Stars.Count; i++)
                this.Primitive.AddVertex(Stars[i], StarsColors[i]);
            this.Primitive.End();

            base.Draw(sprite, gameTime);
        }

        /// <summary>
        /// Handle collisions between entities. Distribute points to players for destructions of asteroids.
        /// </summary>
        /// <param name="gameTime"></param>
        private void HandleCollisons(GameTime gameTime)
        {
            var projectiles = this.Children.Find(c => c.GetType() == typeof(ShipProjectile));
            var meteors = this.Children.Find(m => m.GetType() == typeof(Meteor));
            var players = this.Children.Find(p => p.GetType() == typeof(PlayerShip));

            foreach (PlayerShip player in players)
            {
                // Modify the player bounds slightly 
                // so asteroids won't directly destroy the player.
                Rectangle playerBounds = new Rectangle(
                    player.Bounds.X,
                    player.Bounds.Y,
                    player.ActualWidth / 2,
                    player.ActualHeight / 2);

                foreach (Meteor m in meteors)
                {
                    // Did the meteor hit the player?
                    if (m.Bounds.Intersects(playerBounds))
                    {
                        player.InvokeCollision(m);
                    }

                    // Make sure the meteor is still alive.
                    if (!m.IsActive)
                        continue;

                    foreach (Projectile projectile in projectiles)
                    {
                        if (m.Bounds.Intersects(projectile.Bounds))
                        {
                            projectile.InvokeCollision(m);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Chooses a position outside of the current player position. 
        /// </summary>
        /// <returns></returns>
        private Vector2 RandomPositionOutsideBounds()
        {
            float x = 0;
            float y = 0;

            // Min/Max for X.
            float minX = 0;
            float maxX = 0;

            // Min/Max for Y.
            float minY = 0;
            float maxY = 0;

            // Screen side to spawn into.
            int directionLength = Enum.GetNames(typeof(Direction)).Length;
            Direction spawnSide = (Direction)RandomNum.Next(0, directionLength);

            switch (spawnSide)
            {
                case Direction.Left:
                    minX = -100;
                    maxX = 100;
                    break;
                case Direction.Right:
                    minX = this.Game.View.Width;
                    maxX = minX + 100;
                    break;
                case Direction.Top:
                    minY = -100;
                    maxY = 0;
                    break;
                case Direction.Bottom:
                    minY = this.Game.View.Height;
                    maxY = minY + 100;
                    break;
            }

            if (spawnSide == Direction.Right || spawnSide == Direction.Left)
            {
                // Choose random Y.
                minY = -100;
                maxY = this.Game.View.Height;
            }
            else
            {
                // Choose random X.
                minX = 0;
                minX = this.Game.View.Width;
            }

            x = RandomNum.GetRandomFloat(minX, maxX);
            y = RandomNum.GetRandomFloat(minY, maxY);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Check a space game objects position and reset it to be within the worlds bounds if needed.
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        private Vector2 CheckAndResetChildBounds(SpaceGameObject child)
        {
            // The difference from Minimum/Maximum before declaring
            // the object out of game bounds.
            // (Meteor default difference is 100f, so don't go to low.)
            float difference = 25.0f;

            // Bounds of the object.
            float x = this.Game.Camera.Position.X + child.Position.X;
            float y = this.Game.Camera.Position.Y + child.Position.Y;

            // Holds the Min/Max values for X.
            float minX = this.Game.Camera.Position.X - child.ActualWidth - difference;
            float maxX = this.Game.Camera.Position.X + this.Game.View.Width + child.ActualWidth + difference;

            // Holds the Min/Max values for Y.
            float minY = this.Game.Camera.Position.Y - child.ActualHeight - difference;
            float maxY = this.Game.Camera.Position.Y + this.Game.View.Height + child.ActualHeight + difference;

            if (x < minX)
                x = maxX;
            else if (x > maxX)
                x = minX;

            if (y < minY)
                y = maxY;
            else if (y > maxY)
                y = minY;

            return new Vector2(x, y);
        }
    }
}
