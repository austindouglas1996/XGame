using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example01
{
    public static class GlobalKeys
    {
        public static Keys[] RaiseKey = new Keys[] { Keys.W, Keys.Up };
        public static Keys[] LowerKey = new Keys[] { Keys.S, Keys.Down };
        public static Keys[] RightSideKey = new Keys[] { Keys.D, Keys.Right };
        public static Keys[] LeftSideKey = new Keys[] { Keys.A, Keys.Left };

        public static Keys ShootKey = Keys.X;
        public static Keys ActionKey = Keys.Space;
    }
}
