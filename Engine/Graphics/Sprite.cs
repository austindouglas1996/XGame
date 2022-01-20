using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine;
using XGameEngine.Core;
using XGameEngine.Logic;

namespace XGameEngine.Graphics
{
    /// <summary>
    /// Represents a Texture2D.
    /// </summary>
    public class Sprite : GameObject, ITexture
    {
        /// <summary>
        /// Texture image background color.
        /// </summary>
        private Color color = new Color(255, 255, 255);

        /// <summary>
        /// The depth that the texture should be drawn.
        /// </summary>
        private float depth = 0.0f;

        /// <summary>
        /// The rotation of the texture.
        /// </summary>
        private float rotation = 0.0f;

        /// <summary>
        /// The size of the texture.
        /// </summary>
        private float scale = 1.0f;

        /// <summary>
        /// Holds the sprite mirroring options.
        /// </summary>
        private SpriteEffects effects = SpriteEffects.None;

        /// <summary>
        /// Sprite texture.
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <param name="texture">Sprite texture.</param>
        /// <param name="position">Sprite position.</param>
        /// <param name="color">Sprite background.</param>
        public Sprite(XGame game, Texture2D texture, Vector2 position, Color color)
            : base(game, position)
        {
            this.texture = texture;
            this.color = color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <param name="texture">Sprite texture.</param>
        /// <param name="position">Sprite position.</param>
        /// <param name="color">Sprite background.</param>
        /// <param name="rotation">Sprite rotation.</param>
        /// <param name="scale">Scale of the sprite.</param>
        /// <param name="effects">Mirror effects of the sprite.</param>
        /// <param name="depth">Drawing depth of the sprite.</param>
        public Sprite(XGame game, Texture2D texture, Vector2 position, Color color, 
            float rotation, float scale, SpriteEffects effects, float depth)
            : this(game, texture, position, color)
        {
            this.rotation = rotation;
            this.scale = scale;
            this.effects = effects;
            this.depth = depth;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <remarks>Provides only the base for derived classes.</remarks>
        protected Sprite(XGame game)
            : base(game, new Vector2(1,1))
        {

        }

        /// <summary>
        /// Gets or sets the sprite color.
        /// </summary>
        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        /// <summary>
        /// Gets or sets the sprite drawing depth.
        /// </summary>
        public float Depth
        {
            get { return this.depth; }
            set { this.depth = value; }
        }

        /// <summary>
        /// Gets or sets the sprite rotation.
        /// </summary>
        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        /// <summary>
        /// Gets or sets the sprite drawing size.
        /// </summary>
        public float Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }

        /// <summary>
        /// Gets the actual texture height.
        /// </summary>
        public int ActualHeight
        {
            get 
            {
                float value = (float)this.Height * this.scale;
                return (int)value;
            }
        }

        /// <summary>
        /// Gets the actual texture width.
        /// </summary>
        public int ActualWidth
        {
            get
            {
                float value = (float)this.Width * this.scale;
                return (int)value;
            }
        }

        /// <summary>
        /// Gets the texture height.
        /// </summary>
        public int Height
        {
            get { return this.texture.Height; }
        }

        /// <summary>
        /// Gets the texture width.
        /// </summary>
        public int Width
        {
            get { return this.texture.Width; }
        }

        /// <summary>
        /// Gets the sprite bounds.
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle
                ((int)this.Position.X, (int)this.Position.Y, this.ActualWidth, this.ActualHeight); }
        }

        /// <summary>
        /// Gets or sets the sprite mirror options.
        /// </summary>
        public SpriteEffects Effects
        {
            get { return this.effects; }
            set { this.effects = value; }
        }

        /// <summary>
        /// Gets or sets the sprite.
        /// </summary>
        public Texture2D Texture
        {
            get { return this.texture; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException
                        ("Value cannot be null");
                }

                this.texture = value;
            }
        }

        /// <summary>
        /// Gets the center of the sprite.
        /// </summary>
        public Vector2 Center
        {
            get { return new Vector2(this.ActualWidth / 2, this.ActualHeight / 2); }
        }

        /// <summary>
        /// Draw the sprite.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            // Make sure the texture is not null.
            if (this.texture == null)
            {
                return;
            }

            base.Game.Render.SpriteBatch.Draw
                (this.texture, this.ScreenPosition, null, this.color,
                this.rotation, this.Center, this.scale, this.effects, this.depth);

            base.Draw(sprite, gameTime);
        }

        /// <summary>
        /// Update the game content.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
