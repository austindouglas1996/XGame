using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using XGameEngine.Core;
using XGameEngine.Logic;
using XGameEngine.Settings;

namespace XGameEngine.Managers
{
    /// <summary>
    /// Reads or writes game setting to a file.
    /// </summary>
    public class GameSettings : GameObject
    {
        /// <summary>
        /// Provides primary game settings.
        /// </summary>
        private SettingsBase settings;

        /// <summary>
        /// Path of the setting file to be saved/read from.
        /// </summary>
        private string path = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSettings"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        public GameSettings(XGame game)
            : base(game, new Vector2(1,1))
        {
            // Set the path.
            path = string.Format("{0}/Content/Settings", game.ExecutionPath);

            // Initialize settings.
            this.settings = new SettingsBase(new AudioOptions(), new DisplayOptions(800, 600));
        }

        /// <summary>
        /// Get audio options for SoundEffects and Music files.
        /// </summary>
        public AudioOptions Audio
        {
            get { return this.settings.Audio; }
        }

        /// <summary>
        /// Gets display options for game textures and rendering.
        /// </summary>
        public DisplayOptions Display
        {
            get { return this.settings.Display; }
        }

        /// <summary>
        /// Load and gather game settings from file.
        /// </summary>
        public virtual void Load()
        {
            // If the file does not exist then create it.
            if (!File.Exists(this.path))
            {
                this.Save();
            }

            XmlSerializer serializer =
                new XmlSerializer(this.settings.GetType());

            using (Stream file = File.OpenRead(this.path))
            {
                this.settings =
                    (SettingsBase)serializer.Deserialize(file);
            }
        }

        /// <summary>
        /// Save current game settings within a file.
        /// </summary>
        public virtual void Save()
        {

            XmlSerializer serializer = 
                new XmlSerializer(this.settings.GetType());


            using (Stream file = new FileStream(this.path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(file, this.settings);
            }
        }
    }
}
