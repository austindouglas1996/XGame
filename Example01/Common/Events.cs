using Example01.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example01
{
    public delegate void SpaceGameObjectHandler(SpaceGameObjectEventArgs e);
    public class SpaceGameObjectEventArgs : EventArgs
    {
        public SpaceGameObjectEventArgs(SpaceGameObject gameObj)
        {
            this.Sender = gameObj;
        }

        public SpaceGameObject Sender;
    }
}
