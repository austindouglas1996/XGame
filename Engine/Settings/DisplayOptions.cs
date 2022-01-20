﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Settings
{
    /// <summary>
    /// Provides basic options for game texture rendering.
    /// </summary>
    public class DisplayOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayOptions"/> class.
        /// </summary>
        /// <param name="height">Height of the window.</param>
        /// <param name="width">Width of the window.</param>
        public DisplayOptions(int height, int width)
        {
            this.PreferFullScreen = false;
            this.PreferMultiSampling = true;
            this.Height = height;
            this.MultiSampleCount = 16;
            this.Width = width;
            //this.SamplerState = SamplerState.AnisotropicClamp;
        }

        public DisplayOptions()
        {

        }

        /// <summary>
        /// Allow the game to run fullscreen.
        /// </summary>
        public bool PreferFullScreen { get; set; }

        /// <summary>
        /// A value indicating whether to enable a multisampled back buffer.
        /// </summary>
        public bool PreferMultiSampling { get; set; }

        /// <summary>
        /// Height of the window.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Number of passes through the antilias filter.
        /// </summary>
        public int MultiSampleCount { get; set; }

        /// <summary>
        /// Width of the window.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Determines textures filter and details.
        /// </summary>
        //public SamplerState SamplerState { get; set; }
    }
}
