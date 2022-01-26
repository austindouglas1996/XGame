using Example01;
using Example01.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using XGameEngine;
using XGameEngine.Core;

namespace Example01
{
    public class Game1 : XGame
    {
        private SpriteFont _font;

        public static GameWorld World
        {
            get => _world;
            private set => _world = value;
        }
        private static GameWorld _world;

        public static Random Rand
        {
            get => _Rand;
        }
        private static Random _Rand = new Random();


        public Game1()
            : base(1200,1000,"Testing")
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = base.EngineResource.Load<SpriteFont>("defaultfont");

            Resources.LoadContent(base.EngineResource);


            World = new GameWorld(this);
            World.Initialize();
            base.Render.Entities.Add(World);

            this.Screens.Push(new MainMenu(this));



            base.LoadContent();

            //this.Camera.Zoom = 0.8f;
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
