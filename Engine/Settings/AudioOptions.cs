using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Settings
{
    /// <summary>
    /// Provides basic options for audio settings.
    /// </summary>
    public class AudioOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioOptions"/> struct.
        /// </summary>
        /// <param name="effectsVolume">Effects volume.</param>
        /// <param name="musicVolume">Music volume.</param>
        public AudioOptions(float effectsVolume = 1.0f, float musicVolume = 1.0f)
        {
            this.EffectsVolume = effectsVolume;
            this.MusicVolume = musicVolume;
            this.MasterVolume = 1.0f;
        }

        public AudioOptions()
        {

        }

        /// <summary>
        /// Gets or sets the volume for SoundEffects.
        /// </summary>
        public float EffectsVolume
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the master volume for all sounds.
        /// </summary>
        public float MasterVolume
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the music volume.
        /// </summary>
        public float MusicVolume
        {
            get;
            set;
        }
    }
}
