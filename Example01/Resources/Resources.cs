using Example01.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;

namespace Example01
{
    public static class Resources
    {
        public static Color MenuSelectedForecolor = new Color(255, 5, 130);

        public static readonly float TitleScale = 0.9f;
        public static readonly float MenuItemScale = 0.5f;

        public static SpriteFont TitleFont;

        public static List<ShipResource> Ships = new List<ShipResource>();
        public static List<ProjectileResource> Projectiles = new List<ProjectileResource>();
        public static List<MeteorResource> Meteors = new List<MeteorResource>();
        public static List<UFOResource> UFOs = new List<UFOResource>();

        public static List<ResourceInfo<Texture2D>> ShipTextures = new List<ResourceInfo<Texture2D>>();
        public static List<ResourceInfo<Texture2D>> ShipIconTextures = new List<ResourceInfo<Texture2D>>();
        public static List<ResourceInfo<Texture2D>> ProjectileTextures = new List<ResourceInfo<Texture2D>>();
        public static List<ResourceInfo<Texture2D>> UFOTextures = new List<ResourceInfo<Texture2D>>();
        public static List<ResourceInfo<Texture2D>> MeteorTextures = new List<ResourceInfo<Texture2D>>();
        public static List<ResourceInfo<Texture2D>> NumberTextures = new List<ResourceInfo<Texture2D>>();

        public static List<Song> ProjectileSounds = new List<Song>();


        /// <summary>
        /// Load game content.
        /// </summary>
        /// <param name="resource"></param>
        public static void LoadContent(ResourceLoader resource)
        {
            string ExeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Load font.
            TitleFont = resource.Load<SpriteFont>("Title");

            // Load textures.
            ShipTextures = LoadTextures(resource, ExeDir + @"\Content\Textures\Ships\", "PlayerShip");
            ShipIconTextures = LoadTextures(resource, ExeDir + @"\Content\\Textures\UI\Icons\", "PlayerShipLifeIcon");
            ProjectileTextures = LoadTextures(resource, ExeDir + @"\Content\Textures\Projectiles\", "Projectiles");
            MeteorTextures = LoadTextures(resource, ExeDir + @"\Content\Textures\Meteors\", "Meteors");
            UFOTextures = LoadTextures(resource, ExeDir + @"\Content\Textures\Ships\UFO\", "UFOShip");
            NumberTextures = LoadTextures(resource, ExeDir + @"\Content\Textures\UI\Numbers\", "Numbers");

            // Load projectile sound.
            ProjectileSounds.Add(resource.Load<Song>(@"sfx_laser1"));

            // Create resources.
            CreateShipResources();
            CreateProjectileResources();
            CreateMeteorResources();
            CreateUFOResources();
        }

        /// <summary>
        /// Load textures from a directory path. Loads all files and returns the resource information.
        /// </summary>
        /// <param name="resource">The loader to help with loading.</param>
        /// <param name="directoryPath">The path of the assets.</param>
        /// <param name="type">Type of resource to be put in <see cref="ResourceInfo{T}"/></param>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        private static List<ResourceInfo<Texture2D>> LoadTextures(ResourceLoader resource, string directoryPath, string type)
        {
            // Make sure it exists.
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException(directoryPath);

            List<ResourceInfo<Texture2D>> resources = new List<ResourceInfo<Texture2D>>();

            // Go through the directory. Don't grab a certain extension everything should be the same.
            foreach (var text in Directory.GetFiles(directoryPath))
            {
                string filename = Path.GetFileNameWithoutExtension(text);
                Texture2D texture = resource.Load<Texture2D>(
                    directoryPath + filename, true);

                ResourceInfo<Texture2D> info = new ResourceInfo<Texture2D>(filename, type, texture);
                resources.Add(info);
            }

            return resources;
        }

        /// <summary>
        /// Get a <see cref="MeteorResource"/> based on certain properties.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="colour"></param>
        /// <returns></returns>
        public static MeteorResource GetMeteorResourceFromProp(MeteorSize size, MeteorColour colour)
        {
            List<MeteorResource> resources = Meteors.Where(m => m.Size == size).ToList();

            return resources[Game1.Rand.Next(0, resources.Count)];
        }

        /// <summary>
        /// Combine resources and create ship resources.
        /// </summary>
        private static void CreateShipResources()
        {
            for (int i = 0; i < ShipTextures.Count; i++)
            {
                ShipResource ship = new ShipResource();
                ship.TextureName = ShipTextures[i].Name;
                ship.Texture = ShipTextures[i].Resource;
                ship.LifeIconName = ShipIconTextures[i].Name;
                ship.LifeIcon = ShipIconTextures[i].Resource;

                if (ship.Texture == null ||
                    ship.LifeIcon == null)
                    throw new ArgumentException("Texture is null.");

                Ships.Add(ship);
            }
        }

        /// <summary>
        /// Combine resources and create projectile resources.
        /// </summary>
        private static void CreateProjectileResources()
        {
            for (int i = 0; i < ProjectileTextures.Count; i++)
            {
                ProjectileResource resource = new ProjectileResource();
                resource.TextureName = ProjectileTextures[i].Name;
                resource.Texture = ProjectileTextures[i].Resource;
                resource.ShotSoundName = ProjectileSounds[0].Name;
                resource.ShotSound = ProjectileSounds[0]; // We only have one sound.

                Projectiles.Add(resource);
            }
        }

        /// <summary>
        /// Combine resources and create the resources needed for making meteors.
        /// </summary>
        private static void CreateMeteorResources()
        {
            for (int i = 0; i < MeteorTextures.Count; i++)
            {
                string textName = MeteorTextures[i].Name.ToLower();
                MeteorResource resource = new MeteorResource();

                // Get color.
                if (textName.Contains("brown"))
                {
                    resource.Colour = MeteorColour.Brown;
                }
                else if (textName.Contains("grey"))
                {
                    resource.Colour = MeteorColour.Gray;
                }
                else
                {
                    throw new ArgumentException("Unable to find texture");
                }

                // Get size.
                if (textName.Contains("big"))
                    resource.Size = MeteorSize.Big;
                else if (textName.Contains("med"))
                    resource.Size = MeteorSize.Medium;
                else if (textName.Contains("small"))
                    resource.Size = MeteorSize.Small;
                else if (textName.Contains("tiny"))
                    resource.Size = MeteorSize.Tiny;

                // Set properties.
                resource.Name = textName;
                resource.Texture = MeteorTextures[i].Resource;

                // Add.
                Meteors.Add(resource);
            }
        }

        /// <summary>
        /// Create UFO resources.
        /// </summary>
        private static void CreateUFOResources()
        {
            foreach (ResourceInfo<Texture2D> text in UFOTextures)
            {
                UFOResource resource = new UFOResource(text.Resource);
                resource.Projectile = Projectiles[2];
                UFOs.Add(resource);
            }
        }
    }
}
