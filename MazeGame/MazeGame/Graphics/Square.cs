using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.Graphics
{
    public class Square
    {
        private static Texture2D _BlankTexture;

        public Square(GraphicsDevice device) 
        {
            if (_BlankTexture == null)
            {
                _BlankTexture = new Texture2D(device, 1, 1);
                _BlankTexture.SetData(new Color[] { Color.White });
            }
        }

        public Color Color { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public bool Activated { get; set; } = false;
        public bool Disabled { get; set; } = false;

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            sprite.Draw(_BlankTexture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color);
        }
    }
}
