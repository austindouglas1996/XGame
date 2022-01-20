using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine.Graphics.GUI;
using XGameEngine.Logic;

namespace XGameEngine.Logic
{
    public class MenuGameScreen : GameScreen
    {        
       /// <summary>
       /// The current selected child.
       /// </summary>
       private int selectedIndex = -1;
        
       /// <summary>
       /// Holds the style options.
       /// </summary>
       private TextMenuOptions styleOptions;

        public MenuGameScreen(XGame game) : base(game)
        {
        }

        protected override void UpdateInput(GameTime gameTime)
        {
        }
    }
}
