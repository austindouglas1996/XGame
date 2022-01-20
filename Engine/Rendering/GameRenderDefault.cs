using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;

namespace XGameEngine.Rendering
{
    /// <summary>
    /// Handles the <see cref="XGame"/> rendering of objects.
    /// </summary>
    public class GameRenderDefault : GameRenderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderDefault"/> class.
        /// </summary>
        /// <param name="game"></param>
        public GameRenderDefault(XGame game)
            : base(game)
        {
        }

        /// <summary>
        /// Gets or sets the spritebatch that draws the entities.
        /// </summary>
        public override SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// Gets or sets the sprite options.
        /// </summary>
        public override SpriteBatchRenderOptions SpriteOptions { get; set; }

        /// <summary>
        /// Starts the sprite operation.
        /// </summary>
        public override void Begin()
        {
            this.SpriteBatch.Begin
                (this.SpriteOptions.SortMode, this.SpriteOptions.BlendState,
                this.SpriteOptions.SamplerState, this.SpriteOptions.DepthStencilState,
                this.SpriteOptions.RasterizerState, this.SpriteOptions.Effect,
                this.SpriteOptions.TransformMatrix);
        }

        /// <summary>
        /// Draws the child entities.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            this.Entities.Draw(this.SpriteBatch, gameTime);
        }

        /// <summary>
        /// Ends the current sprite operation.
        /// </summary>
        public override void End()
        {
            this.SpriteBatch.End();
        }

        /// <summary>
        /// Initializes the spritebatch.
        /// </summary>
        public override void Initialize()
        {
            // Initialize sprite.
            this.SpriteBatch = new SpriteBatch(this.Game.View.GraphicsDevice);

            // Create options.
            this.SpriteOptions = new SpriteBatchRenderOptions
                (SpriteSortMode.Deferred, null, null, null, null, null, this.Game.Camera);
        }

        /// <summary>
        /// Updates the entities.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            this.Entities.Update(gameTime);
        }
    }
}
