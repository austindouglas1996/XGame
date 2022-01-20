using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGameEngine.Common
{
    public enum PositionType
    {
        /// <summary>
        /// If this element is a child, it's position will be drawn relative to the parent.
        /// This means that child will be drawn like:
        /// 
        /// ParentA.Position = 100,100
        /// ChildA.Position = 50,50
        /// 
        /// ChildA will be rendered at 150,150
        /// </summary>
        Relative,

        /// <summary>
        /// If this element is a child, it's position will be drawn absolute to the parent.
        /// This means that child will be drawn like:
        /// 
        /// ParentA.Position = 100,100
        /// ChildA.Position = 50,50
        /// 
        /// ChildA will be rendered at 50,50
        /// </summary>
        Absolute
    }
}
