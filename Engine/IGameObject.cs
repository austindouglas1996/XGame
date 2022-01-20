using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGameEngine
{
    /// <summary>
    /// Represents the base properties required for a <see cref="GameObject"/> instance.
    /// </summary>
    public interface IGameObject
    {
        int ID { get; }
        bool IsActive { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch sprite, GameTime gameTime);
    }
}
