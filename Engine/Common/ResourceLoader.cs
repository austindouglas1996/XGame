using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Core;
using XGameEngine.Logic;

namespace XGameEngine
{
    /// <summary>
    /// Helps loaded managed files from the assembly.
    /// </summary>
    public class ResourceLoader : GameObject
    {
        /// <summary>
        /// Holds the types and locations of resources.
        /// </summary>
        private readonly Dictionary<Type, string> types = new Dictionary<Type, string>()
        {
            {typeof(Song), "Sounds//"},
            {typeof(SoundEffect), "Sounds//"},
            {typeof(SpriteFont), "Fonts//"},
            {typeof(Texture2D), "Textures//"},
            {typeof(Texture3D), "Models//"}
        };

        /// <summary>
        /// Holds a dummy texture for non-texture objects.
        /// </summary>
        private static Texture2D dummy;

        /// <summary>
        /// Gathers the game content.
        /// </summary>
        private ContentManager content;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoader"/> class.
        /// </summary>
        /// <param name="game">The currently running game.</param>
        /// <param name="rootDirectory">The primary location of resources.</param>
        public ResourceLoader(XGame game, string rootDirectory)
            : base(game, new Vector2(1,1))
        {
            this.content = new ContentManager(game.Services);
            this.content.RootDirectory = rootDirectory;
        }

        /// <summary>
        /// Gets or sets the resource directory.
        /// </summary>
        public string RootDirectory
        {
            get { return this.content.RootDirectory; }
            set { this.content.RootDirectory = value; }
        }

        /// <summary>
        /// Gets a dummy texture with no values.
        /// </summary>
        public Texture2D Dummy
        {
            get
            {
                // Due to a bug when the game is being closed and somewhere
                // Dummy is called GraphicsDevice will be null so we will only
                // create the dummy texture if needed.
                if (dummy == null & this.Game.GraphicsDevice != null)
                {
                    // Create texture.
                    dummy = new Texture2D(this.Game.GraphicsDevice, 1, 1);
                    dummy.SetData<Color>(new Color[] { Color.White });
                }

                return dummy;
            }
        }

        /// <summary>
        /// Load a resource from the assembly.
        /// </summary>
        /// <typeparam name="T">Type of resource.</typeparam>
        /// <param name="assetName">Name of the resource.</param>
        /// <returns>T</returns>
        public T Load<T>(string assetName, bool directPath = false)
        {
            // Holds the returned value.
            T value = default(T);

            string modifiedName = "";

            if (directPath == false)
            {
                // Get the root directory of the asset.
                string root = this.types[typeof(T)];

                // Add to the string.
                modifiedName = assetName.Insert(0, root);
            }
            else
                modifiedName = assetName;

            // Try to load content based on path.
            value = this.content.Load<T>(modifiedName);

            // Check if the asset was unable to be found.
            if (value == null)
            {
                // We will try to load the file with the original path.
                return this.content.Load<T>(assetName);
            }

            return value;
        }
    }
}
