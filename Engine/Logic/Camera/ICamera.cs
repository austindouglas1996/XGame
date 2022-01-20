using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Logic.Camera
{
    /// <summary>
    /// Defines an interface for a game camera.
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// Camera rotation.
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// Camera zoom.
        /// </summary>
        float Zoom { get; set; }

        /// <summary>
        /// Camera center.
        /// </summary>
        Vector2 Origin { get; }

        /// <summary>
        /// Position of the camera.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Provides the game view.
        /// </summary>
        Viewport View { get; set; }

        /// <summary>
        /// Gets the camera transformation.
        /// </summary>
        /// <returns>Matrix</returns>
        Matrix GetTransformation();
    }
}
