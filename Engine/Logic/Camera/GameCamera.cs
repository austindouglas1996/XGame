using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Logic.Camera
{
    /// <summary>
    /// Represents the base of a 2D camera.
    /// </summary>
    public abstract class GameCamera : ICamera
    {
        /// <summary>
        /// Max zoom allowed.
        /// </summary>
        private const float zoomMax = 1.5f;

        /// <summary>
        /// Minimum zoom.
        /// </summary>
        private const float zoomMinimum = -0.5f;

        /// <summary>
        /// Camera rotation.
        /// </summary>
        private float rotation = 0.0f;

        /// <summary>
        /// Default zoom.
        /// </summary>
        private float zoom = 1.0f;

        /// <summary>
        /// Provides the game view.
        /// </summary>
        private Viewport view;

        /// <summary>
        /// Center of camera.
        /// </summary>
        private Vector2 origin;

        /// <summary>
        /// The camera position.
        /// </summary>
        private Vector2 position = new Vector2(0, 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="GameCamera"/> class.
        /// </summary>
        /// <param name="view">Provides the game view.</param>
        public GameCamera(Viewport view)
        {
            // Set properties.
            this.view = view;

            // Set origin.
            this.origin = new Vector2(this.view.Width / 2.0f, this.view.Height / 2.0f);
        }

        /// <summary>
        /// Raised on zoom value change.
        /// </summary>
        public event EventHandler ZoomChange;

        /// <summary>
        /// Gets or sets the camera rotation.
        /// </summary>
        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        /// <summary>
        /// Gets or sets the camera zoom.
        /// </summary>
        public float Zoom
        {
            get { return this.zoom; }
            set
            {
                // Make sure zoom is not out of range.
                if (value > zoomMax)
                {
                    value = zoomMax;
                }
                else if (value < zoomMinimum)
                {
                    value = zoomMinimum;
                }

                this.zoom = value;

                this.OnZoomChange(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the max amount of zoom.
        /// </summary>
        public float ZoomMax
        {
            get { return zoomMax; }
        }

        /// <summary>
        /// Gets the minimum amount of zoom.
        /// </summary>
        public float ZoomMinimum
        {
            get { return zoomMinimum; }
        }

        /// <summary>
        /// Gets the camera height.
        /// </summary>
        public float Height
        {
            get { return this.view.Height; }
        }

        /// <summary>
        /// Gets the camera width.
        /// </summary>
        public float Width
        {
            get { return this.view.Width; }
        }

        /// <summary>
        /// Gets the camera center.
        /// </summary>
        public Vector2 Origin
        {
            get { return this.origin; }
        }

        /// <summary>
        /// Gets or sets the camera position.
        /// </summary>
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        public Viewport View
        {
            get { return this.view; }
            set { this.view = value; }
        }

        /// <summary>
        /// Provides the camera transformation.
        /// </summary>
        /// <returns>Matrix</returns>
        public abstract Matrix GetTransformation();

        /// <summary>
        /// Raised on zoom value change.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnZoomChange(EventArgs e)
        {
            if (this.ZoomChange != null)
            {
                this.ZoomChange(this, e);
            }
        }
    }
}
