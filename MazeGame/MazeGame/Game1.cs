using MazeGame.Graphics;
using MazeGame.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using XGameEngine;

namespace MazeGame
{
    public class Game1 : XGame
    {
        private Maze _maze;

        public Game1()
            : base(2000,1000, "MazeGame")
        {
            _maze = new Maze(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            base.WorldRender.Entities.Add(_maze);
            _maze.GenerateMaze(base.Graphics.GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(33, 30, 36));

            base.Draw(gameTime);
        }
    }
}
