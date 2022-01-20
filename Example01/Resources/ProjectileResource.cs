using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Example01
{
    public class ProjectileResource
    {
        public ProjectileResource(Texture2D texture)
        {
            this.Texture = texture;
        }

        public ProjectileResource()
        {

        }

        public string TextureName { get; set; }
        public string DestroyedTextureName { get; set; }
        public string ShotSoundName { get; set; }
        public string DestroySoundName { get; set; }

        public Texture2D Texture { get; set; }
        public Texture2D DestroyedTexture { get; set; }
        public Song ShotSound { get; set; }
        public Song DestroyedSound { get; set; }
    }
}
