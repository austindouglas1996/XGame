using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example01
{
    public class UFOResource
    {
        public UFOResource(Texture2D text)
        {
            this.Texture = text;
        }

        public Texture2D Texture { get; set; }
        public ProjectileResource Projectile { get; set; }
    }
}
