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
        /// Manages the stars we see in the world.
        /// </summary>
        private StarManager _Stars;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWorld"/> class.
        /// </summary>
        /// <param name="game"></param>
        public GameWorld(XGame game) 
            : base(game, Vector2.Zero)
        {
            WorldWidth = 3000;
            WorldHeight = 4000;
        }

        public int WorldWidth { get; set; }
        public int WorldHeight { get; set; }

        /// <summary>
        /// Represents the player.
        /// </summary>
        public PlayerShip Player { get; set; }

        /// <summary>
        /// Add a player to the world.
        /// </summary>
        /// <param name="ship"></param>
        public void AddPlayer(PlayerShip ship)
        {
            Player = ship;
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
        /// Add a ship part to the world.
        /// </summary>
        /// <param name="part"></param>
        public void AddShipPart(ShipPart part)
        {
            this.Children.Add(part);
        }

        /// <summary>
        /// Add a UFO to the world.
        /// </summary>
        /// <param name="ship"></param>
        public void AddUFO(UFOShip ship)
        {
            this.Children.Add(ship);
        }

        /// <summary>
        /// Add a random UFO.
        /// </summary>
        public void AddUFORandom()
        {
            int resourceIndex = RandomNum.Next(0, Resources.UFOs.Count());
            UFOResource resource = Resources.UFOs[resourceIndex];

            UFOShip ufo = new UFOShip(this.Game, resource, Color.White);
            this.AddUFO(ufo);
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

        public void AddShipPartRandom()
        {
            int Max = Resources.ShipPartsTextures.Count;
            Texture2D resource = Resources.ShipPartsTextures[RandomNum.Next(0, Max)].Resource;

            ShipPart part = new ShipPart(this.Game, resource, RandomPositionOutsideBounds(), Color.White);

            AddShipPart(part);
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
            _Stars = new StarManager(base.Game, this, base.Game.Camera);
            _Stars.AddStars(1500);

            base.Initialize();
        }

        /// <summary>
        /// Update all entities within the world.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (var child in this.Children.GetAll())
            {
                var spaceObj = child as SpaceGameObject;
                if (spaceObj == null)
                {
                    //continue;
                }

                child.Position = CheckAndResetChildBounds((SpaceGameObject)child);
            }

            HandleCollisons(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws world elements.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _Stars.Draw(gameTime);
            base.Draw(gameTime);
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
                    minX = this.WorldWidth;
                    maxX = minX + 100;
                    break;
                case Direction.Top:
                    minY = -100;
                    maxY = 0;
                    break;
                case Direction.Bottom:
                    minY = this.WorldHeight;
                    maxY = minY + 100;
                    break;
            }

            if (spawnSide == Direction.Right || spawnSide == Direction.Left)
            {
                // Choose random Y.
                minY = -100;
                maxY = WorldHeight;
            }
            else
            {
                // Choose random X.
                minX = 0;
                minX = WorldWidth;
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
            float x = child.Position.X;
            float y = child.Position.Y;

            // Holds the Min/Max values for X.
            float minX = child.ActualWidth - difference;
            float maxX = WorldWidth + child.ActualWidth + difference;

            // Holds the Min/Max values for Y.
            float minY = child.ActualHeight - difference;
            float maxY = WorldHeight + child.ActualHeight + difference;

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
