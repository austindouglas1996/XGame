using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Logic.Input
{
    /// <summary>
    /// Provides options when checking for user input.
    /// </summary>
    public enum StateOptions
    {
        /// <summary>
        /// Search only the current state.
        /// </summary>
        Current,

        /// <summary>
        /// Search only the previous state.
        /// </summary>
        Previous,

        /// <summary>
        /// Search both states but previous must have the opposite value.
        /// </summary>
        CurrentFavor,

        /// <summary>
        /// Search both states but current must have the opposite value.
        /// </summary>
        PreviousFavor
    }
}
