using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Core.Geometry
{
    /// <summary>
    /// Represents a circle.
    /// </summary>
    public class Circle
    {
        /// <summary>
        /// Radius of the circle.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Center of the circle.
        /// </summary>
        public Vector2 Center { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> struct.
        /// </summary>
        /// <param name="radius">Radius of the circle.</param>
        /// <param name="center">Center of the circle.</param>
        public Circle(float radius, Vector2 center)
        {
            this.Radius = radius;
            this.Center = center;
        }

        /// <summary>
        /// Gets a bool indicating whether a point is within the circle.
        /// </summary>
        /// <param name="point">Point of the object.</param>
        /// <returns>bool</returns>
        public bool Contains(Vector2 point)
        {
            // Get the relative position.
            Vector2 relativePos = point - this.Center;

            // Get the length.
            float distance = relativePos.Length();

            // Check if distance is less or equal to radius.
            if (distance <= this.Radius)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a bool indicating whether another circle intersects this circle.
        /// </summary>
        /// <param name="circle">Circle to be checked.</param>
        /// <returns>bool</returns>
        public bool Intersects(Circle circle)
        {
            // Get the relative position of both circles.
            Vector2 relativePos = circle.Center - this.Center;

            // Get the distance.
            float distance = relativePos.Length();

            // Get the radius.
            float radius = circle.Radius + this.Radius;

            // Check if the distance is smaller or equal to radius.
            if (distance <= radius)
            {
                return true;
            }

            return false;
        }
    }
}
