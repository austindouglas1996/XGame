using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics;
using XGameEngine.Graphics.GUI;

namespace Example01.Screens
{
    /// <summary>
    /// Displays the amount of lives the player has left.
    /// </summary>
    public class PlayerLivesDisplay : UIObject
    {
        /// <summary>
        /// The last known amount of lives. Do we need to update?
        /// </summary>
        private int LastKnownLives = 0;

        /// <summary>
        /// The screen that owns this control.
        /// </summary>
        private PlayScreen Screen;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerLivesDisplay"/> class.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="player"></param>
        public PlayerLivesDisplay(PlayScreen screen) 
            : base(screen.Game)
        {
            this.Screen = screen;

            this.Height = 100;
            this.Width = 500;
            this.Background = new Color(0, 0, 0, 70);

            LastKnownLives = screen.RemainingLives;

            base.Layer = 0;
        }

        /// <summary>
        /// Initialize the content.
        /// </summary>
        public override void Initialize()
        {
            this.Reset();
            base.Initialize();
        }

        /// <summary>
        /// Update the control.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Update the known amount of lives if required.
            if (this.Screen.RemainingLives != LastKnownLives)
            {
                LastKnownLives = this.Screen.RemainingLives;
                Reset();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!this.Screen.Player.IsActive)
                return;

            base.Draw(gameTime);
        }

        /// <summary>
        /// The known lives has changed so we'll reset the controls.
        /// </summary>
        public void Reset()
        {
            this.Children.RemoveAll();

            // Create the default text label.
            TextBlock label = new TextBlock(this.Game, "Lives:", Vector2.Zero);
            label.Scale = Resources.MenuItemScale;
            label.Foreground = Color.White;
            label.Position = new Vector2(50, (this.Height - label.ActualHeight) / 2);
            this.Children.Add(label);

            // The position to place each icon.
            Vector2 pos = new Vector2(label.ActualWidth + 20, 50);

            for (int i = 0; i < LastKnownLives; i++)
            {
                Sprite s = new Sprite(this.Game, Screen.PlayerResources.LifeIcon, Vector2.Zero, Color.White);
                s.Layer = 0;

                // Increase the position
                pos = pos + new Vector2(s.ActualWidth + 20, 0);

                s.Position = pos;
                this.Children.Add(s);
            }
        }
    }
}
