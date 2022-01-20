using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics.GUI;
using XGameEngine.Logic;

namespace Example01.Screens
{
    public class GameOverScreen : MenuGameScreen
    {
        public GameOverScreen(PlayScreen screen) : base(screen.Game)
        {
            this.IsTranslucent = true;
        }

        public override void Initialize()
        {
            Container c = new Container(this.Game);
            c.Background = new Color(0, 0, 0, 100);
            c.Width = Game.View.Width;
            c.Height = Game.View.Height;
            c.Position = new Vector2(0, 0);
            this.Children.Add(c);

            // Create the title block. Middle of the screen.
            TextBlock title = new TextBlock(base.Game, "GAME OVER", Vector2.Zero);
            title.Foreground = Color.White;
            title.Scale = 2.0f;
            title.Position = new Vector2((base.Game.View.Width - title.ActualWidth) / 2, 150);
            this.Children.Add(title);

            base.Initialize();
        }

        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            base.Draw(sprite, gameTime);
        }

        protected override void UpdateInput(GameTime gameTime)
        {
        }
    }
}
