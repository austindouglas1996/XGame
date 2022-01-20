using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Common;
using XGameEngine.Graphics;

namespace XGameEngine.Rendering
{
    /// <summary>
    /// Defines needed properties for creating a <see cref="GameRenderBase"/> instance.
    /// </summary>
    public interface IGameRenderer
    {
        /// <summary>
        /// The game object we're rendering for.
        /// </summary>
        XGame Game { get; }

        /// <summary>
        /// Contains a list of entities.
        /// </summary>
        ListRepository<GameObject> Entities { get; set; }

        /// <summary>
        /// SpriteBatch to render game objects.
        /// </summary>
        SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// Options for rendering sprites.
        /// </summary>
        SpriteBatchRenderOptions SpriteOptions { get; set; }

        /// <summary>
        /// Start batch operations.
        /// </summary>
        void Begin();

        /// <summary>
        /// End the current batch operations.
        /// </summary>
        void End();  
        
        /// <summary>                          
        /// Draw the object.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void Draw(GameTime gameTime);

        /// <summary>
        /// Initialize and setup the object.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Update the object content.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void Update(GameTime gameTime);
    }
}
