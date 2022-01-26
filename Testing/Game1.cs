using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Testing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        Sprite _arrow;
        Sprite _arrow1;

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D arrowTexture = Texture2D.FromStream(GraphicsDevice, new FileStream("Content/2.png", FileMode.Open));
            _arrow = new Sprite(arrowTexture)
            {
                Position = new Vector2(500, 200),
                Color = Color.White,
                Rotation = 0f,
                Scale = 1f,
                Effect = SpriteEffects.None,
                Origin = new Vector2(arrowTexture.Bounds.Center.X, arrowTexture.Bounds.Center.Y)
            };

            _arrow1 = new Sprite(arrowTexture)
            {
                Position = new Vector2(200, 400),
                Color = Color.White,
                Rotation = 0f,
                Scale = 1f,
                Effect = SpriteEffects.None,
                Origin = new Vector2(arrowTexture.Bounds.Center.X, arrowTexture.Bounds.Center.Y)
            };

            // TODO: use this.Content to load your game content here
        }

        public Texture2D Dummy
        {
            get
            {
                // Due to a bug when the game is being closed and somewhere
                // Dummy is called GraphicsDevice will be null so we will only
                // create the dummy texture if needed.
                if (dummy == null & this.GraphicsDevice != null)
                {
                    // Create texture.
                    dummy = new Texture2D(this.GraphicsDevice, 1, 1);
                    dummy.SetData<Color>(new Color[] { Color.White });
                }

                return dummy;
            }
        }
        private static Texture2D dummy;
        private static SpriteEffects flip = SpriteEffects.None;


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);


            _arrow.Rotation = TurnToFace(_arrow.Position, _arrow1.Position, _arrow.Rotation, 0.2f);

            _arrow.Position += new Vector2
                ((float)(Math.Cos(_arrow.Rotation - MathHelper.PiOver2) * 1.0f),
                (float)((Math.Sin(_arrow.Rotation - MathHelper.PiOver2) * 1.0f)));

            _arrow1.Position = mousePosition;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); 
            


            _spriteBatch.Begin();
            _arrow.Draw(_spriteBatch, gameTime);
            _arrow1.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();


            base.Draw(gameTime);
        }

        /// 

        /// Calculates the angle that an object should face, given its position, its
        /// target's position, its current angle, and its maximum turning speed.
        /// 

        private static float TurnToFace(Vector2 position, Vector2 faceThis,
            float currentAngle, float turnSpeed)
        {
            // consider this diagram:
            //         C 
            //        /|
            //      /  |
            //    /    | y
            //  / o    |
            // S--------
            //     x
            // 
            // where S is the position of the spot light, C is the position of the player,
            // and "o" is the angle that the spot light should be facing in order to 
            // point at the player. we need to know what o is. using trig, we know that
            //      tan(theta)       = opposite / adjacent
            //      tan(o)           = y / x
            // if we take the arctan of both sides of this equation...
            //      arctan( tan(o) ) = arctan( y / x )
            //      o                = arctan( y / x )
            // so, we can use x and y to find o, our "desiredAngle."
            // x and y are just the differences in position between the two objects.
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;

            // we'll use the Atan2 function. Atan will calculates the arc tangent of 
            // y / x for us, and has the added benefit that it will use the signs of x
            // and y to determine what cartesian quadrant to put the result in.
            // http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
            float desiredAngle = (float)Math.Atan2(y, x) * (float)(180 / Math.PI); ; 
            //float desiredAngle = (float)Math.Atan2(y, x) + MathHelper.PiOver2 + MathHelper.Pi;

            // so now we know where we WANT to be facing, and where we ARE facing...
            // if we weren't constrained by turnSpeed, this would be easy: we'd just 
            // return desiredAngle.
            // instead, we have to calculate how much we WANT to turn, and then make
            // sure that's not more than turnSpeed.

            // first, figure out how much we want to turn, using WrapAngle to get our
            // result from -Pi to Pi ( -180 degrees to 180 degrees )
            float difference = WrapAngle(desiredAngle - currentAngle);

            // clamp that between -turnSpeed and turnSpeed.
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

            // so, the closest we can get to our target is currentAngle + difference.
            // return that, using WrapAngle again.
            return WrapAngle(currentAngle + difference);
        }

        /// 

        /// Returns the angle expressed in radians between -Pi and Pi.
        /// 

        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

    }
}
