using Example01.Logic;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Example01
{
    public class MeteorResource
    {
        public MeteorSize Size { get; set; }
        public MeteorColour Colour { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public Texture2D Texture { get; set; }
    }
}
