/* ---------------------------------------------------------------
 * Names: Frame Counter
 * By: craftworkgames
 * Link: http://stackoverflow.com/questions/20676185/xna-monogame-getting-the-frames-per-second
 * Describtion: I did not know how to calculate FPS thanks to craftWorkGames I can learn an idea
 * on how to do such a thing. Until then we will a modified class of his.
 * ---------------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Logic.Utility
{
    /// <summary>
    /// Gets the amount of frames per second.
    /// </summary>
    public class FPSCounter
    {
        /// <summary>
        /// Max amount of samples to be tested.
        /// </summary>
        public const int MaxSamples = 100;

        /// <summary>
        /// A line holding the samples.
        /// </summary>
        private Queue<float> sampleBuffer = new Queue<float>();

        /// <summary>
        /// Gets the total frames processed.
        /// </summary>
        public long TotalFrames
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Total seconds recorded.
        /// </summary>
        public float TotalSeconds
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the the average frames per second.
        /// </summary>
        public float AverageFPS
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current amount of frames per second.
        /// </summary>
        public float CurrentFPS
        {
            get;
            private set;
        }

        /// <summary>
        /// Update the frames counter.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool Update(float time)
        {
            // Get the current FPS
            this.CurrentFPS = 1.0f / time;

            // Add.
            this.sampleBuffer.Enqueue(CurrentFPS);

            // Check if we went over count.
            if (this.sampleBuffer.Count > MaxSamples)
            {
                // Remove.
                this.sampleBuffer.Dequeue();

                // Set average.
                this.AverageFPS = this.sampleBuffer.Average(i => i);
            }
            else
            {
                // Set average.
                this.AverageFPS = CurrentFPS;
            }

            // Add time.
            this.TotalFrames++;
            this.TotalSeconds += time;

            // Return true.
            return true;
        }
    }
}
