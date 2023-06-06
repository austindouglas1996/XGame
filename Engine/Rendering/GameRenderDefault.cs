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
            SpriteBatch = new List<SpriteBatch>();
            SpriteOptions = new List<SpriteBatchRenderOptions>();
        }

        /// <summary>
        /// Gets or sets the spritebatch that draws the entities.
        /// </summary>
        public override List<SpriteBatch> SpriteBatch { get; set; }

        /// <summary>
        /// Gets or sets the sprite options.
        /// </summary>
        public override List<SpriteBatchRenderOptions> SpriteOptions { get; set; }

        /// <summary>
        /// Starts the sprite operation.
        /// </summary>
        public override void Begin(int layer)
        {
            this.SpriteBatch[layer].Begin
                (this.SpriteOptions[layer].SortMode, this.SpriteOptions[layer].BlendState,
                this.SpriteOptions[layer].SamplerState, this.SpriteOptions[layer].DepthStencilState,
                this.SpriteOptions[layer].RasterizerState, this.SpriteOptions[layer].Effect,
                this.SpriteOptions[layer].TransformMatrix);
        }

        /// <summary>
        /// Draws the child entities.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(int layer, GameTime gameTime)
        {
            this.Entities.Draw(gameTime);
        }

        /// <summary>
        /// Ends the current sprite operation.
        /// </summary>
        public override void End(int layer)
        {
            this.SpriteBatch[layer].End();
        }

        /// <summary>
        /// Initializes the spritebatch.
        /// </summary>
        public override void Initialize()
        {
            // Initialize sprite.
            this.SpriteBatch.Add(new SpriteBatch(this.Game.GraphicsDevice));

            // Create options.
            this.SpriteOptions.Add(new SpriteBatchRenderOptions
                (SpriteSortMode.Deferred, null, null, null, null, null, null));

            // Initialize sprite.
            this.SpriteBatch.Add(new SpriteBatch(this.Game.GraphicsDevice));

            // Create options.
            this.SpriteOptions.Add(new SpriteBatchRenderOptions
                (SpriteSortMode.Deferred, null, null, null, null, null, this.Game.Camera));
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
