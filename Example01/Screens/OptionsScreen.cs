using Microsoft.Xna.Framework;
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
    public class OptionsScreen : GameScreen
    {
        ProgressBar p;

        public OptionsScreen(XGame game) : base(game)
        {
            p = new ProgressBar(game);
            p.Width = 400;
            p.Height = 70;
            p.Background = new Color(0, 0, 0, 170);
            p.Position = new Vector2(200, 100);

            Border b = new Border(p);
            b.Thickness = new Thickness(20);
            b.Background = new Color(0, 0, 0, 170);


            this.Children.Add(b);
            this.Children.Add(p);
        }

        protected override void UpdateInput(GameTime gameTime)
        {
            if (InputState.KeyPressed(Microsoft.Xna.Framework.Input.Keys.F, PlayerIndex.One, StateOptions.CurrentFavor))
                p.CurrentProgress += 10;

            if (InputState.KeyPressed(Microsoft.Xna.Framework.Input.Keys.H, PlayerIndex.One, StateOptions.CurrentFavor))
                p.CurrentProgress -= 10;
        }
    }
}
