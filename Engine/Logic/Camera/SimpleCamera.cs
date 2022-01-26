using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Logic.Camera
{
    /// <summary>
    /// Provides a simple 2D camera.
    /// </summary>
    public class SimpleCamera : GameCamera
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCamera"/> class.
        /// </summary>
        /// <param name="view">Provides the game view.</param>
        public SimpleCamera(Viewport view)
            : base(view)
        {

        }

        /// <summary>
        /// Provides the camera transformation.
        /// </summary>
        /// <returns>Matrix</returns>
        public override Matrix GetTransformation()
        {
            return Matrix.CreateTranslation(new Vector3(-this.Position.X, -this.Position.Y, 0))
                * Matrix.CreateTranslation(new Vector3(-Origin, 0.0f))
                * Matrix.CreateRotationZ(Rotation)
                * Matrix.CreateScale(new Vector3(this.Zoom, this.Zoom, 1f))
                * Matrix.CreateTranslation(new Vector3(this.Width * 0.5f, this.Height * 0.5f, 0));
        }
    }
}
