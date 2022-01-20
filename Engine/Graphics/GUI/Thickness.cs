using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Defines the thickness of a extended element.
    /// </summary>
    public struct Thickness
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> struct.
        /// </summary>
        /// <param name="thickness">Thickness for all values.</param>
        public Thickness(float thickness)
            : this()
        {
            this.Right = thickness;
            this.Bottom = thickness;
            this.Left = thickness;
            this.Top = thickness;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> struct.
        /// </summary>
        /// <param name="right">Right thickness.</param>
        /// <param name="bottom">Bottom thickness.</param>
        /// <param name="left">Left thickness.</param>
        /// <param name="top">Top thickness.</param>
        /// <param name="spacing">Amount of spacing between each thickness.</param>
        public Thickness(float right, float bottom, float left, float top, float spacing = 0f)
            : this()
        {
            this.Right = right;
            this.Bottom = bottom;
            this.Left = left;
            this.Top = top;
            this.Spacing = spacing;
        }

        /// <summary>
        /// Right thickness.
        /// </summary>
        public float Right { get; set; }

        /// <summary>
        /// Bottom thickness.
        /// </summary>
        public float Bottom { get; set; }

        /// <summary>
        /// Left thickness.
        /// </summary>
        public float Left { get; set; }

        /// <summary>
        /// Spacing thickness.
        /// </summary>
        public float Spacing { get; set; }

        /// <summary>
        /// Top thickness.
        /// </summary>
        public float Top { get; set; }
    }
}
