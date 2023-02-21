using System;
using System.Drawing;



namespace SimpleREngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Renderer Render = new Renderer("Result", 1000, 1000, 0xFF808080);

            // Render.DrawCircle(500, 600, 80, 0.5, 0);
            //Render.DrawFilledCircle(100, 100, 50, 0.5, 0xFFFF0000);
            // Render.DrawFilledSquare(100, 100, 100, 100, 0xFFFF0000);
            //Render.DrawRotatedFilledSquare(500, 500, 100, 100, 0xFFFF0000);
            //Render.DrawSquare(100, 100, 500, 500, 0xFFFF0000);

            //Recursive(Render, 500, 500, 50, 0, naujas);
            //Recursive(Render, 500, 500, 50, 2, naujas);
            //Recursive(Render, 500, 500, 100, 3, naujas);
            //RecursiveSquare(Render, 500, 500, 500, 1);
            DrawRotatedSquare(Render, 400, 400, 200, 200, 0, 0);
            DrawSurroundingSquares(Render, 500, 500, 100,100 ,60, 45, 3);
            Render.Write();
        }
      
        private static void DrawRotatedSquare(Renderer Render, int x, int y,int Width, int Height, int size, float angle)
        {
            // Calculate the points of the rotated square
            PointF[] points = new PointF[4];
            //points[0] = RotatePoint(new PointF(x - size / 2, y - size / 2), new PointF(x, y), angle);
            //points[1] = RotatePoint(new PointF(x + size / 2, y - size / 2), new PointF(x, y), angle);
            //points[2] = RotatePoint(new PointF(x + size / 2, y + size / 2), new PointF(x, y), angle);
           // points[3] = RotatePoint(new PointF(x - size / 2, y + size / 2), new PointF(x, y), angle);

            // Draw the rotated square
            Render.DrawRotatedFilledSquare((x - size / 2), (y - size / 2), Width, Height);
        }

        private static void DrawSurroundingSquares(Renderer Render, int x, int y,  int Width, int Height, int size, float angle, int depth)
        {
            if (depth == 0)
            {
                return;
            }

            // Calculate the coordinates of the smaller squares
            float sqrt2 = (float)Math.Sqrt(2);
            int dist = (int)(size * sqrt2);
            int x1 = (int)(x - dist * 4);
            int y1 = (int)(y - dist * 2);
            int x2 = (int)(x + dist * 4);
            int y2 = (int)(y - dist * 2);
            int x3 = (int)(x);
            int y3 = (int)(y - dist * 4.5);
            int x4 = (int)(x - dist * 3);
            int y4 = (int)(y + dist * 3);
            int x5 = (int)(x + dist * 3);
            int y5 = (int)(y + dist * 3);

            // Draw the surrounding squares rotated 45 degrees
            DrawRotatedSquare(Render, x1, y1, Width , Height , size, angle);
            DrawRotatedSquare(Render, x2, y2, Width , Height , size, angle);
            DrawRotatedSquare(Render, x3, y3, Width , Height , size, angle);
            DrawRotatedSquare(Render, x4, y4, Width , Height , size, angle);
            DrawRotatedSquare(Render, x5, y5, Width , Height , size, angle);

            // Recursively draw smaller squares around each of the surrounding squares
            DrawSurroundingSquares(Render, x1, y1, Width /3, Height/3, size / 3, angle, depth - 1);
            DrawSurroundingSquares(Render, x2, y2, Width / 3, Height / 3, size / 3, angle, depth - 1);
            DrawSurroundingSquares(Render, x3, y3, Width / 3, Height / 3, size / 3, angle, depth - 1);
            DrawSurroundingSquares(Render, x4, y4, Width / 3, Height / 3, size / 3, angle, depth - 1);
            DrawSurroundingSquares(Render, x5, y5, Width / 3, Height / 3, size / 3, angle, depth - 1);

        }



    }
}
