using Example01.Logic;
using Example01.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics.GUI;

namespace Example01.Graphics
{
    public class PlayerScoreDisplay : UIObject
    {
        private TextBlock Score;
        private PlayScreen Screen;

        public PlayerScoreDisplay(PlayScreen screen) : base(screen.Game)
        {
            this.Screen = screen;
            this.Height = 120;
            this.Width = 300;
            this.Background = new Color(0, 0, 0, 70);
        }

        public PlayerShip Player
        {
            get; set;
        }

        public override void Initialize()
        {     
            // Create the default text label.
            TextBlock label = new TextBlock(this.Game, "SCORE", Vector2.Zero);
            label.Foreground = Color.White;
            label.Scale = Resources.MenuItemScale;
            label.Position = new Vector2
                ((this.ActualWidth - label.ActualWidth) /2, 20);
            this.Children.Add(label);

            Score = new TextBlock(this.Game, "0", Vector2.Zero);
            Score.Foreground = Color.White;
            Score.Scale = Resources.MenuItemScale;
            Score.TextChanged += (obj, e) =>
            {
                Score.Position = new Vector2
                 ((this.ActualWidth - Score.ActualWidth) / 2, this.ActualHeight - Score.ActualHeight - 20);
            };
            Score.Text = "0";
            
            this.Children.Add(label);
            this.Children.Add(Score);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Player != null)
            {
                // Update the text, but only if required.
                if (Player.Points.ToString() != Screen.TotalScore.ToString())
                    Score.Text = Player.Points.ToString();
            }

            base.Update(gameTime);
        }
    }
}
