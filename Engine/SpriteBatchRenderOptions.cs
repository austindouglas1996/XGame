using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Logic.Camera;

namespace XGameEngine
{
    /// <summary>
    /// Provides options for SpriteBatch rendering.
    /// </summary>
    public class SpriteBatchRenderOptions
    {
        /// <summary>
        /// The camera to be used when rendering.
        /// </summary>
        private GameCamera camera;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteBatchRenderOptions"/> struct.
        /// </summary>
        /// <param name="sortMode">Sprite drawing order.</param>
        /// <param name="blendState">Blending options.</param>
        /// <param name="samplerState">Texture sampling options.</param>
        /// <param name="depthStencilState">Depth and stencil options.</param>
        /// <param name="rasterizerState">Rasterization options.</param>
        /// <param name="effect">Effect state options.</param>
        /// <param name="camera">Holds the transformation matrix for scale, rotate, translate options.</param>
        public SpriteBatchRenderOptions(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, 
            DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, 
            GameCamera camera)
        {
            this.SortMode = sortMode;
            this.BlendState = blendState;
            this.SamplerState = samplerState;
            this.DepthStencilState = depthStencilState;
            this.RasterizerState = rasterizerState;
            this.Effect = effect;
            this.camera = camera;
        }

        /// <summary>
        /// Blending options.
        /// </summary>
        public BlendState BlendState { get; set; }

        /// <summary>
        /// Depth and stencil options.
        /// </summary>
        public DepthStencilState DepthStencilState { get; set; }

        /// <summary>
        /// Gets or sets the camera for matrix transformatin.
        /// </summary>
        public GameCamera Camera
        {
            get { return this.camera; }
            set { this.camera = value; }
        }

        /// <summary>
        /// Effect state options.
        /// </summary>
        public Effect Effect { get; set; }

        /// <summary>
        /// Transformation matrix for scale, rotate, translate options.
        /// </summary>
        public Matrix TransformMatrix
        {
            get 
            { 
                if (this.camera == null)
                {
                    return Matrix.Identity;
                }

                return this.camera.GetTransformation(); 
            }
        }

        /// <summary>
        /// Rasterization options.
        /// </summary>
        public RasterizerState RasterizerState { get; set; }

        /// <summary>
        /// Texture sampling options.
        /// </summary>
        public SamplerState SamplerState { get; set; }

        /// <summary>
        /// Sprite drawing order.
        /// </summary>
        public SpriteSortMode SortMode { get; set; }
    }
}
