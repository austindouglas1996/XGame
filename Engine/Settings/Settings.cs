using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Core;

namespace XGameEngine.Settings
{
    /// <summary>
    /// Provides the game settings.
    /// </summary>
    public struct SettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> struct.
        /// </summary>
        public SettingsBase(AudioOptions audio = new AudioOptions(), DisplayOptions display = null)
            : this()
        {
            this.Audio = audio;

            if (display == null)
                display = new DisplayOptions(800,400);

            this.Display = display;
        }

        /// <summary>
        /// Provides audio options.
        /// </summary>
        public AudioOptions Audio { get; set; }

        /// <summary>
        /// Provides display and render options.
        /// </summary>
        public DisplayOptions Display { get; set; }
    }
}
