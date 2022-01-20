using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example01
{
    public class ResourceInfo<T>
    {
        public ResourceInfo(string name, string type, T resource)
        {
            this.Name = name;
            this.Type = type;
            this.Resource = resource;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public T Resource { get; set; }
    }
}
