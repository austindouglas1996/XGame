using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Graphics
{
    /// <summary>
    /// Defines a interface for a game texture.
    /// </summary>
    public interface ITexture
    {
        /// <summary>
        /// Texture color.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Texture depth.
        /// </summary>
        float Depth { get; set; }

        /// <summary>
        /// Texture rotation.
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// Texture scale.
        /// </summary>
        float Scale { get; set; }

        /// <summary>
        /// Gets the actual texture height based on propertie.s
        /// </summary>
        int ActualHeight { get; }

        /// <summary>
        /// Gets the actual texture width based on propertie.s
        /// </summary>
        int ActualWidth { get; }

        /// <summary>
        /// Texture height.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Texture width.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Texture bounds.
        /// </summary>
        Rectangle Bounds { get; }

        /// <summary>
        /// Texture effects.
        /// </summary>
        SpriteEffects Effects { get; set; }

        /// <summary>
        /// Texture.
        /// </summary>
        Texture2D Texture { get; set; }

        /// <summary>
        /// Texture center.
        /// </summary>
        Vector2 Center { get; }

        /// <summary>
        /// Texture position.
        /// </summary>
        Vector2 Position { get; set; }
    }
}
