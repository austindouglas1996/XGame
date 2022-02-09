using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Core;
using XGameEngine.Graphics.GUI;
using XGameEngine.Logic;

namespace Example01.Screens
{
    public class MainMenu : GameScreen
    {
        public MainMenu(XGame game)
            :base(game)
        {
            
        }

        public override void Initialize()
        {
            // Create the title block. Middle of the screen.
            TextBlock title = new TextBlock(base.Game, "2D Asteroids", Vector2.Zero);
            title.Font = Resources.TitleFont;
            title.Foreground = Color.White;
            title.Scale = Resources.TitleScale;
            title.Position = new Vector2((Game.GraphicsDevice.Viewport.Width - title.ActualWidth) / 2, 150); 
            title.Background = new Color(0, 0, 0, 100);
            this.Children.Add(title);

            // Create a border and place the title as the child to be encased.
            Border b = new Border(title);
            b.Background = new Color(0,0,0, 100);
            b.Thickness = new Thickness(30,30,30,30,0);
            this.Children.Add(b);

            // Create the options for the sub menu items.
            TextMenuOptions options = new TextMenuOptions();
            options.Foreground = Color.White;
            options.SelectedForeground = Resources.MenuSelectedForecolor;

            // Create the menu items and add to the window.
            TextMenu menuItems = new TextMenu(base.Game);
            menuItems.Scale = 0.5f;
            menuItems.Options = options;
            menuItems.Add("Play", new Vector2(title.Position.X, 310), Resources.MenuItemScale, PlayGame);
            menuItems.Add("Options", new Vector2(title.Position.X, 385), Resources.MenuItemScale, Options);
            menuItems.Add("Leaderboard", new Vector2(title.Position.X, 450), Resources.MenuItemScale);
            menuItems.Add("Exit", new Vector2(title.Position.X, 515), Resources.MenuItemScale);
            this.Children.Add(menuItems);

            // PulseEffect: TextBlock, MinimumLoss, MaximumLoss, ChangeRate, DifferenceInChange.
            menuItems.Options.SelectedEffect = 
                new TextEffectPulse(menuItems.Items[0], 0.01f, 0.2f, 0.0002f, TimeSpan.FromSeconds(0.4));

            // Add some asteroids.
            for (int i = 0; i < 50; i++)
                Game1.World.AddMeteorRandom();

            base.Initialize();
        }

        protected override void UpdateInput(GameTime gameTime)
        {
        }

        private void PlayGame(object sender, EventArgs e)
        {
            base.Game.Screens.Push(new ShipSelectionScreen(this.Game));
        }

        private void Options(object sender, EventArgs e)
        {
            base.Game.Screens.Push(new OptionsScreen(this.Game));
        }
    }
}
