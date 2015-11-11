using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Drawing
{
    public class PrimitiveBatch : IDisposable
    {
        private VertexPositionColor[] vertices = new VertexPositionColor[1000];
        public const int DefaultBufferSize = 1000;
        private int positionInBuffer;
        private BasicEffect basicEffect;
        private GraphicsDevice device;
        private PrimitiveType primitiveType;
        private int numVertsPerPrimitive;
        private bool hasBegun;
        private bool isDisposed;

        public PrimitiveBatch(GraphicsDevice graphicsDevice)
        {
            if (graphicsDevice == null)
                throw new ArgumentNullException("graphicsDevice");

            device = graphicsDevice;

            basicEffect = new BasicEffect(graphicsDevice)
            {
                VertexColorEnabled = true,
                LightingEnabled = false,
                Projection = Matrix.CreateOrthographicOffCenter(0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 0, 1)
            };

            device.BlendState = new BlendState
            {
                AlphaSourceBlend = Blend.SourceAlpha,
                AlphaBlendFunction = BlendFunction.Add,
                AlphaDestinationBlend = Blend.InverseSourceAlpha
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || isDisposed)
                return;
            if (basicEffect != null)
                basicEffect.Dispose();
            isDisposed = true;
        }

        public void Begin(PrimitiveType primitiveType)
        {
            if (hasBegun)
                throw new InvalidOperationException("End must be called before Begin can be called again.");

            this.primitiveType = primitiveType;
            numVertsPerPrimitive = NumVertsPerPrimitive(primitiveType);
            basicEffect.CurrentTechnique.Passes[0].Apply();
            hasBegun = true;
        }

        public void AddVertex(Vector2 vertex, Color color)
        {
            if (!hasBegun)
                throw new InvalidOperationException("Begin must be called before AddVertex can be called.");

            if (positionInBuffer % numVertsPerPrimitive == 0 && positionInBuffer + numVertsPerPrimitive >= vertices.Length)
                Flush();

            vertices[positionInBuffer].Position = new Vector3(vertex, 0.0f);
            vertices[positionInBuffer].Color = color;
            ++positionInBuffer;
        }

        public void AddVertex(int x, int y, Color color)
        {
            AddVertex(new Vector2(x, y), color);
        }

        public void AddVertex(float x, float y, Color color)
        {
            AddVertex(new Vector2(x, y), color);
        }

        public void End()
        {
            if (!hasBegun)
                throw new InvalidOperationException("Begin must be called before End can be called.");

            Flush();
            hasBegun = false;
        }

        private void Flush()
        {
            if (!hasBegun)
                throw new InvalidOperationException("Begin must be called before Flush can be called.");

            if (positionInBuffer == 0)
                return;

            int num = 0;
            switch (primitiveType)
            {
                case PrimitiveType.TriangleList:
                    num = positionInBuffer / 3;
                    break;
                case PrimitiveType.TriangleStrip:
                    num = positionInBuffer - 2;
                    break;
                case PrimitiveType.LineList:
                    num = positionInBuffer / 2;
                    break;
                case PrimitiveType.LineStrip:
                    num = positionInBuffer - 1;
                    break;
            }

            device.DrawUserPrimitives(primitiveType, vertices, 0, num);

            positionInBuffer = 0;
        }

        private static int NumVertsPerPrimitive(PrimitiveType primitive)
        {
            switch (primitive)
            {
                case PrimitiveType.TriangleList:
                case PrimitiveType.TriangleStrip:
                    return 3;
                default:
                    return 2;
            }
        }
    }
}
