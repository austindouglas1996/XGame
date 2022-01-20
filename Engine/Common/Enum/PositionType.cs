using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine
{
    /// <summary>
    /// Specifies the position types.
    /// </summary>
    public enum PositionType
    {
        /// <summary>
        /// Position is not relative to the parent.
        /// </summary>
        Absolute,

        /// <summary>
        /// Position is relative to the parent.
        /// </summary>
        Relative
    }
}
