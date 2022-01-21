using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace XGameEngine.Helpers
{
    /// <summary>
    /// Creates a series of random numbers.
    /// </summary>
    public static class RandomNum
    {
        /// <summary>
        /// Creates a series of random numbers.
        /// </summary>
        private static RandomNumberGenerator random =
            RandomNumberGenerator.Create();

        /// <summary>
        /// Gets a non-negative value less than the maximum.
        /// </summary>
        /// <param name="max">Maximum value.</param>
        /// <returns>Integer</returns>
        public static int Next(int max)
        {
            return (int)Math.Round(GetNextDouble() * (max - int.MinValue - 1) + int.MinValue);
        }

        /// <summary>
        /// Gets a non-negative value less than the maximum and above minimum.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>Integer</returns>
        public static int Next(int min, int max)
        {
            return (int)Math.Round(GetNextDouble() * (max - min - 1) + min);
        }

        /// <summary>
        /// Gets a random double.
        /// </summary>
        /// <returns></returns>
        public static double GetNextDouble()
        {
            // Create new bytes.
            byte[] bytes = new byte[4];

            // Set.
            random.GetBytes(bytes);

            // Return.
            return (double)BitConverter.ToUInt32(bytes, 0) / UInt32.MaxValue;
        }

        /// <summary>
        /// Gets a random value between min and max.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>double</returns>
        public static double GetNextDouble(double min, double max)
        {
            return GetNextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Gets a random float.
        /// </summary>
        /// <returns></returns>
        public static float GetRandomFloat(float min, float max)
        {
            return (float)Math.Round(GetNextDouble() * (max - min - 1)) + min;
        }

        /// <summary>
        /// Gets a random Int32.
        /// </summary>
        /// <returns>int</returns>
        public static int GetRandomInt(int min, int max)
        {
            return (int)Math.Round(GetNextDouble() * (max - min - 1)) + min;
        }

        /// <summary>
        /// Gets a random percent.
        /// </summary>
        /// <returns>int</returns>
        public static int GetRandomPercent(int min, int max)
        {
            // Get values.
            int value1 = GetRandomInt(min, max);
            int value2 = GetRandomInt(value1, max);

            // Get value by float.
            float value = (float)value1 / (float)value2 * 100;

            return (int)value;
        }
    }
}
