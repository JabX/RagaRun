using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Drawing
{
    public static class DrawingHelper
    {
        public static PrimitiveBatch primitiveBatch;

        public static void Initialize(GraphicsDevice device)
        {
            primitiveBatch = new PrimitiveBatch(device);
        }

        public static void Begin(PrimitiveType primitiveType)
        {
            primitiveBatch.Begin(primitiveType);
        }

        public static void End()
        {
            primitiveBatch.End();
        }

        public static void DrawPoint(Vector2 vertex, Color color)
        {
            primitiveBatch.AddVertex(vertex, color);
        }

        public static void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            primitiveBatch.AddVertex(start, color);
            primitiveBatch.AddVertex(end, color);
        }

        public static void DrawRectangle(Rectangle rect, Color color, bool fill)
        {
            if (fill)
            {
                primitiveBatch.Begin((PrimitiveType)1);
                primitiveBatch.AddVertex(rect.Left, rect.Top, color);
                primitiveBatch.AddVertex(rect.Right, rect.Top, color);
                primitiveBatch.AddVertex(rect.Left, rect.Bottom, color);
                primitiveBatch.AddVertex(rect.Right, rect.Bottom, color);
                primitiveBatch.End();
            }
            else
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)2);
                DrawingHelper.primitiveBatch.AddVertex((int)rect.X, (int)rect.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((int)(rect.X + rect.Width), (int)rect.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((int)(rect.X + rect.Width), (int)rect.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((int)(rect.X + rect.Width), (int)(rect.Y + rect.Height), color);
                DrawingHelper.primitiveBatch.AddVertex((int)(rect.X + rect.Width), (int)(rect.Y + rect.Height), color);
                DrawingHelper.primitiveBatch.AddVertex((int)rect.X, (int)(rect.Y + rect.Height), color);
                DrawingHelper.primitiveBatch.AddVertex((int)rect.X, (int)(rect.Y + rect.Height), color);
                DrawingHelper.primitiveBatch.AddVertex((int)rect.X, (int)rect.Y, color);
                DrawingHelper.primitiveBatch.End();
            }
        }

        public static void DrawRectangle(Vector2 position, Vector2 size, Color color, bool fill)
        {
            if (fill)
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)1);
                DrawingHelper.primitiveBatch.AddVertex((float)position.X, (float)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((float)(position.X + size.X), (float)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((float)position.X, (float)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((float)(position.X + size.X), (float)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.End();
            }
            else
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)2);
                DrawingHelper.primitiveBatch.AddVertex((float)position.X, (float)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((float)(position.X + size.X), (float)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((float)(position.X + size.X), (float)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((float)(position.X + size.X), (float)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((float)(position.X + size.X), (float)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((float)position.X, (float)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((float)position.X, (float)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((float)position.X, (float)position.Y, color);
                DrawingHelper.primitiveBatch.End();
            }
        }

        public static void DrawRectangle(Point position, Point size, Color color, bool fill)
        {
            if (fill)
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)1);
                DrawingHelper.primitiveBatch.AddVertex((int)position.X, (int)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((int)(position.X + size.X), (int)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((int)position.X, (int)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((int)(position.X + size.X), (int)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.End();
            }
            else
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)2);
                DrawingHelper.primitiveBatch.AddVertex((int)position.X, (int)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((int)(position.X + size.X), (int)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((int)(position.X + size.X), (int)position.Y, color);
                DrawingHelper.primitiveBatch.AddVertex((int)(position.X + size.X), (int)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((int)(position.X + size.X), (int)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((int)position.X, (int)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((int)position.X, (int)(position.Y + size.Y), color);
                DrawingHelper.primitiveBatch.AddVertex((int)position.X, (int)position.Y, color);
                DrawingHelper.primitiveBatch.End();
            }
        }

        public static void DrawRectangle(int x1, int y1, int x2, int y2, Color color, bool fill)
        {
            if (fill)
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)1);
                DrawingHelper.primitiveBatch.AddVertex(x1, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x1, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y2, color);
                DrawingHelper.primitiveBatch.End();
            }
            else
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)2);
                DrawingHelper.primitiveBatch.AddVertex(x1, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x1, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x1, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x1, y1, color);
                DrawingHelper.primitiveBatch.End();
            }
        }

        public static void DrawRectangle(float x1, float y1, float x2, float y2, Color color, bool fill)
        {
            if (fill)
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)1);
                DrawingHelper.primitiveBatch.AddVertex(x1, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x1, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y2, color);
                DrawingHelper.primitiveBatch.End();
            }
            else
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)2);
                DrawingHelper.primitiveBatch.AddVertex(x1, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y1, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x2, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x1, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x1, y2, color);
                DrawingHelper.primitiveBatch.AddVertex(x1, y1, color);
                DrawingHelper.primitiveBatch.End();
            }
        }

        public static void DrawCircle(Vector2 center, float radius, Color color, bool fill)
        {
            if (fill)
                DrawingHelper.DrawNGon(center, radius, 663, color, fill);
            else
                DrawingHelper.DrawNGon(center, radius, 997, color, fill);
        }

        public static void DrawNGon(Vector2 center, float radius, int numSides, Color color, bool fill)
        {
            if (numSides < 3)
                return;
            if ((double)radius == 0.0)
            {
                DrawingHelper.primitiveBatch.Begin((PrimitiveType)3);
                DrawingHelper.primitiveBatch.AddVertex(center, color);
            }
            else
            {
                if (fill)
                    DrawingHelper.primitiveBatch.Begin((PrimitiveType)1);
                else
                    DrawingHelper.primitiveBatch.Begin((PrimitiveType)3);
                float num1 = 6.283185f;
                float num2 = num1 / (float)numSides;
                int num3 = 0;
                float num4 = 0.0f;
                while ((double)num4 <= (double)num1)
                {
                    if (fill && num3 % 3 == 0)
                        DrawingHelper.primitiveBatch.AddVertex((float)center.X, (float)center.Y, color);
                    DrawingHelper.primitiveBatch.AddVertex((float)(center.X + (double)radius * Math.Cos((double)num4)), (float)(center.Y + (double)radius * Math.Sin((double)num4)), color);
                    ++num3;
                    num4 += num2;
                }
            }
            DrawingHelper.primitiveBatch.End();
        }

        public static void DrawFastLine(Vector2 start, Vector2 end, Color color)
        {
            DrawingHelper.primitiveBatch.Begin((PrimitiveType)2);
            DrawingHelper.primitiveBatch.AddVertex(start, color);
            DrawingHelper.primitiveBatch.AddVertex(end, color);
            DrawingHelper.primitiveBatch.End();
        }
    }
}
