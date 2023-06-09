using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Logic.Camera;

namespace XGameEngine.Graphics
{
    /// <summary>
    /// Helper that can draw lines and triangles.
    /// </summary>
    public class PrimitiveBatch
    {
        /// <summary>
        /// Controls how large the vertice buffers are.
        /// </summary>
        private const int defaultBufferSize = 1000;

        /// <summary>
        /// Holds the shaders used to draw the vertices.
        /// </summary>
        private BasicEffect basicEffect;

        /// <summary>
        /// Has Begin() been called?
        /// </summary>
        private bool hasBegun = false;

        /// <summary>
        /// Has the resources been disposed of yet?
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// The device used to make draw calls with.
        /// </summary>
        private GraphicsDevice device;

        /// <summary>
        /// How many vertices per primitive?
        /// </summary>
        /// <remarks>
        /// Points = 1
        /// Lines = 2
        /// Triangles = 3
        /// </remarks>
        private int numVertsPerPrimitive;

        /// <summary>
        /// Current position within the buffer.
        /// </summary>
        private int positionInBuffer = 0;

        /// <summary>
        /// This will help us know what type of primitive to draw.
        /// </summary>
        private PrimitiveType primitiveType;

        /// <summary>
        /// An array of vertices.
        /// </summary>
        private VertexPositionColor[] vertices = new VertexPositionColor[defaultBufferSize];

        /// <summary>
        /// Initializes a new instnace of the <see cref="PrimitiveBatch"/> class.
        /// </summary>
        /// <param name="device">The device to be used for draw calls.</param>
        /// <param name="camera">The camera the the primitivebatch should work around.</param>
        public PrimitiveBatch(GraphicsDevice device)
        {
            // Make sure device is not null.
            if (device == null)
            {
                //errorLog.WriteLine("PrimitiveBatch: Device cannot be null.");
                return;
            }

            // Set device.
            this.device = device;

            // Initialize effect.
            this.basicEffect = new BasicEffect(this.device);
            this.basicEffect.VertexColorEnabled = true;
            
            // Set projection as a orthographic.
            this.basicEffect.Projection = Matrix.CreateOrthographicOffCenter
                (0, this.device.Viewport.Width,
                this.device.Viewport.Height, 0, 0, 1);
        }

        /// <summary>
        /// Add another vertex to be rendered.
        /// To draw a point, 'AddVertex' must be called once.
        /// To draw a line, 'AddVertex' must be called twice.
        /// To draw a triangle 'AddVertex' must be called three times.
        /// </summary>
        /// <param name="vertex">Position</param>
        /// <param name="color">Color</param>
        /// <remarks>This function can only be called once <see cref="Begin"/> has been called.</remarks>
        public void AddVertex(Vector2 vertex, Color color)
        {
            // Check for invalid value.
            if (!this.hasBegun)
            {
                throw new InvalidOperationException
                    ("Begin must be called before AddVertex can be called.");
            }

            // Are we starting a new primitive? If so, and there will not be enough room for a whole primitive. flush.
            bool newPrimitive = ((this.positionInBuffer % this.numVertsPerPrimitive) == 0);

            // Check if we need to flush.
            if (newPrimitive &&
                (this.positionInBuffer + this.numVertsPerPrimitive) >= this.vertices.Length)
            {
                this.Flush();
            }

            // Now that we know we have enough room, set the vertex within the buffer, and increase position.
            this.vertices[this.positionInBuffer].Position = new Vector3(vertex, 0);
            this.vertices[this.positionInBuffer].Color = color;
            this.positionInBuffer++;
        }

        /// <summary>
        /// Begin to tell the primitiveBatch what kind of PrimitiveBatch will be drawn, and prepare
        /// graphics card to render these primitives.
        /// </summary>
        /// <param name="primitiveType">The type of primitive to be drawn.</param>
        public void Begin(PrimitiveType primitiveType)
        {
            // Check for illigal value.
            if (this.hasBegun)
            {
                throw new InvalidOperationException
                    ("End must be called before Begin can be called again.");
            }

            // There is threee types of reuse vertices, We cannot flush them properly without more
            // complex logic. Since that's a bit too complicated for right now, we'll simply disallow them.
            // TODO: Allow reuse vertices.
            if (primitiveType == PrimitiveType.LineStrip ||
                primitiveType == PrimitiveType.TriangleStrip)
            {
                throw new NotSupportedException
                    ("The specified primitiveType is not supported by PrimitiveBatch");
            }

            // Set primitive type.
            this.primitiveType = primitiveType;

            // Get how many verts we will be using for each of these primitives.
            this.numVertsPerPrimitive = NumVertsPerPrimitive(primitiveType);

            // Begin basicEffect.
            this.basicEffect.CurrentTechnique.Passes[0].Apply();

            // We have begun. User can not call 'AddVertex', 'Flush', and 'End'.
            this.hasBegun = true;
        }

        /// <summary>
        /// Dispose the class resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Send all primitives to be drawn using AddVertex, Flush will automatically be called to submit the draw
        /// to the graphics card and then tell basicEffect to end.
        /// </summary>
        public void End()
        {
            // Check for invalid value.
            if (!this.hasBegun)
            {
                throw new InvalidOperationException
                    ("Begin must be called before End can be called.");
            }

            // Flush whatever the user wanted us to draw.
            this.Flush();

            // Reset.
            this.hasBegun = false;
        }

        /// <summary>
        /// Issue the draw call to the graphics card. Once the draw call is made. Reset the positionInBuffer, so that
        /// AddVertex can start over at the beginning. End will call this to draw the primitives that the user requested.
        /// </summary>
        /// <remarks>AddVertex will call this if there is not enough room within the buffer.</remarks>
        public void Flush()
        {
            // Check for invalid value.
            if (!this.hasBegun)
            {
                throw new InvalidOperationException
                    ("Begin must be called before Flush can be called.");
            }

            // Make sure there is work to do.
            if (this.positionInBuffer == 0)
            {
                return;
            }

            // How many are we going to draw?
            int primitiveCount = this.positionInBuffer / this.numVertsPerPrimitive;

            // Submit the draw call to the graphics card.
            this.device.DrawUserPrimitives<VertexPositionColor>
                (this.primitiveType, this.vertices, 0, primitiveCount);

            // Reset buffer.
            this.positionInBuffer = 0;

            // no work to do
            if (positionInBuffer == 0)
            {
                return;
            }
        }

        /// <summary>
        /// Dispose of the class resources.
        /// </summary>
        /// <param name="disposing">Are we disposing of the resources?</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check if we're disposing.
            if (disposing && !this.isDisposed)
            {
                // Check if basicEffect is still running.
                if (this.basicEffect != null)
                {
                    this.basicEffect.Dispose();
                }

                // Change value.
                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Tells how many vertices will be needed to draw the primitive.
        /// </summary>
        /// <param name="primitive">Type of primitive.</param>
        /// <returns>The amount of vertices needed to draw.</returns>
        private static int NumVertsPerPrimitive(PrimitiveType primitive)
        {
            // Switch primitive.
            // (Allow them to fall through.)
            switch (primitive)
            {
                case PrimitiveType.LineList:
                    return 2;
                case PrimitiveType.TriangleList:
                    return 3;
                default:
                    throw new InvalidOperationException("primitive is not valid.");
            }
        }
    }
}
