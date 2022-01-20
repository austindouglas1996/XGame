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
    public abstract class MenuGameScreen : GameScreen
    {
        private int selectedIndex = -1;
        private TextMenuOptions styleOptions;
        public MenuGameScreen(XGame game) : base(game)
        {

        }
    }
}
