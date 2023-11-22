using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics;

namespace MazeGame.Graphics
{
    public class Square : Sprite
    {
        public Square(XGame game) 
            : base(game)
        {
            base.Texture = game.EngineResource.Dummy;
            base.Scale = 1f;
        }

        public bool Activated { get; set; } = false;
        public bool Disabled { get; set; } = false;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
