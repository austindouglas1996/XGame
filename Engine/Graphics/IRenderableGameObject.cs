using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Graphics
{
    /// <summary>
    /// Defines the needed properties for a renderable game object.
    /// </summary>
    public interface IRenderableGameObject : IGameObject, IDisposableGameObject
    {
        /// <summary>
        /// Gets a value indicating whether Initialize has been called.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Gets or sets the object display state.
        /// </summary>
        Visibility Visible { get; set; }

        /// <summary>
        /// Initialize and setup the object.
        /// </summary>
        void Initialize();
    }
}
