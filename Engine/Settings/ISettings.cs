using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Settings
{
    /// <summary>
    /// Defines the needed classes when handling game settings.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Provides audio options.
        /// </summary>
        AudioOptions Audio { get; }

        /// <summary>
        /// Provides display options and render settings.
        /// </summary>
        DisplayOptions Display { get; }
    }
}
