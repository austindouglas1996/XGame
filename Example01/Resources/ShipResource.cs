using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Example01
{
    public class ShipResource
    {
        public string TextureName { get; set; }
        public string LifeIconName { get; set; }
       
        [XmlIgnore]
        public Texture2D Texture { get; set; }

        [XmlIgnore]
        public Texture2D LifeIcon { get; set; }
    }
}
