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
    /// Creates the base object for managing <see cref="XGame"/> object rendering.
    /// </summary>
    public abstract class GameRenderBase : IGameRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderBase"/> class.
        /// </summary>
        public GameRenderBase(XGame game)
        {
            this.Game = game;
            this._Entities = new ListRepository<GameObject>();
        }

        /// <summary>
        /// Returns the game object we're rendering for.
        /// </summary>
        public XGame Game { get; private set; }

        /// <summary>
        /// Gets or sets the list of objects to render.
        /// </summary>
        public ListRepository<GameObject> Entities
        {
            get { return _Entities; }
            set { _Entities = value; }
        }
        private ListRepository<GameObject> _Entities;

        /// <summary>
        /// Object to manage drawing.
        /// </summary>
        public abstract List<SpriteBatch> SpriteBatch { get; set; }

        /// <summary>
        /// Rendering options.
        /// </summary>
        public abstract List<SpriteBatchRenderOptions> SpriteOptions { get; set; }

        /// <summary>
        /// Start draw operation.
        /// </summary>
        public abstract void Begin(int layer);

        /// <summary>
        /// Draw child entries.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Draw(int layer, GameTime gameTime);

        /// <summary>
        /// End the current draw.
        /// </summary>
        public abstract void End(int layer);

        /// <summary>
        /// Initialize components.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Update child entries.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);
    }
}
