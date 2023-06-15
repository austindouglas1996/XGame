using Example01.Graphics;
using Example01.Logic;
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
using XGameEngine.Logic;
using XGameEngine.Logic.Input;

namespace Example01.Screens
{
    public class PlayScreen : GameScreen
    {
        private PlayerLivesDisplay LivesDisplay;
        private PlayerScoreDisplay ScoreDisplay;

        public PlayScreen(XGame game, ShipResource resource)
            : base(game)
        {
            this.PlayerResources = resource;
        }

        public int TotalScore = 0;

        public int RemainingLives = 4;

        public int MinimumMeteor = 5;
        public int MaximumMeteor = 20;

        public PlayerShip Player;
        public ShipResource PlayerResources;
        public Vector2 StartPos = Vector2.Zero;

        public override void Initialize()
        {
            // Setup the player lives display.
            LivesDisplay = new PlayerLivesDisplay(this);
            LivesDisplay.Initialize();
            LivesDisplay.Position = new Vector2(50, Game.GraphicsDevice.Viewport.Height - LivesDisplay.ActualHeight - 50);
            this.Children.Add(LivesDisplay);

            // Setup the player scores display.
            ScoreDisplay = new PlayerScoreDisplay(this);
            ScoreDisplay.Initialize();
            ScoreDisplay.Position = new Vector2((Game.GraphicsDevice.Viewport.Width - ScoreDisplay.ActualWidth) / 2, 50);
            this.Children.Add(ScoreDisplay);

            // Reset the scene.
            this.Reset();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputState.KeyPressed(Microsoft.Xna.Framework.Input.Keys.J, PlayerIndex.One, StateOptions.CurrentFavor))
            {
                Game1.World.AddUFORandom();
            }

            if (!Player.IsActive && this.RemainingLives > 0)
            {
                Reset(); 
            }
            else if (!Player.IsActive && this.RemainingLives <= 0)
            {
                base.Game.Screens.Push(new GameOverScreen(this));
            }

            base.Update(gameTime);
        }
        protected override void UpdateInput(GameTime gameTime)
        {
            // Kill the player and reset the scene? (Debug)
            if (InputState.KeyPressed(Keys.K, PlayerIndex.One, StateOptions.CurrentFavor))
            {
                this.Player.IsActive = false;
                this.Reset();
            }

            // M stands for meteor.
            if (InputState.KeyPressed(Keys.M, PlayerIndex.One, StateOptions.CurrentFavor))
            {
                for (int i = 0; i < 100; i++)
                    Game1.World.AddMeteorRandom();
            }

            // P stands for part.
            if (InputState.KeyPressed(Keys.P, PlayerIndex.One, StateOptions.CurrentFavor))
            {
                for (int i = 0; i < 100; i++)
                    Game1.World.AddShipPartRandom();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void Reset()
        {
            if (RemainingLives == 0)
            {
                this.ScoreDisplay.IsActive = false;
                this.LivesDisplay.IsActive = false;

                // The game is over.
                base.Game.Screens.Push(new GameOverScreen(this));

                return;
            }

            // Take away one of the lives before proceeding.
            this.RemainingLives -= 1;

            if (this.Player != null)
            {
                // Add to total score.
                this.TotalScore += this.Player.Points;
            }

            Game1.World.ResetWorld();
            this.CreatePlayer();
        }

        private void CreatePlayer()
        {
            // Default projectile.
            ProjectileResource model = new ProjectileResource(Resources.Projectiles[2].Texture);
            Player = new PlayerShip(this.Game, PlayerResources.Texture, model, Vector2.Zero, Color.White);

            // Adjust player position.
            StartPos = new Vector2(
                ((Game1.World.WorldWidth - Player.ActualWidth) / 2) + 50,
                (Game1.World.WorldHeight - Player.ActualHeight) / 2);

            // Set player position.
            Player.Position = StartPos;

            // Add the player,
            Game1.World.AddPlayer(Player);

            // Set scoreboard.
            this.ScoreDisplay.Player = Player;
        }
    }
}
