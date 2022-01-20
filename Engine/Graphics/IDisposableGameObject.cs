using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Graphics
{
    /// <summary>
    /// Defines the needed properties for a disposable object.
    /// </summary>
    public interface IDisposableGameObject : IDisposable
    {
        /// <summary>
        /// Has the object been disposed already?
        /// </summary>
        bool IsDisposed { get; }
    }
}
