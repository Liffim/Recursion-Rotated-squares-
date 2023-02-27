using System;
using System.Drawing;



namespace ConsoleApp7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Drawing Render = new Drawing("Result", 1000, 1000, 0xFFFFFFFF);
            DrawRotatedSquare(Render, 400, 400, 200, 200, 0);
            DrawSurroundingSquares(Render, 500, 500, 50,50 ,50, 3);
            Render.Write();
        }
      
        private static void DrawRotatedSquare(Drawing Render, int x, int y,int Width, int Height, int size)
        {
            // Draw the rotated square
            Render.DrawRotatedFilledSquare((x - size / 2), (y - size / 2), Width, Height);//c1 | n^2
        }
        //T(n) = n^2

        private static void DrawSurroundingSquares(Drawing Render, int x, int y,  int Width, int Height, int size, int depth)
        {
            if (depth == 0)
            {
                return;
            }//                                         c1,c2 | 1,1

            // Calculate the coordinates of the smaller squares
            float sqrt2 = (float)Math.Sqrt(2);//c3 | 1
            int dist = (int)(size * sqrt2);//c4 | 1
            int x1 = (int)(x - dist * 3);//c5 | 1
            int y1 = (int)(y - dist * 3);//c6 | 1
            int x2 = (int)(x + dist * 3);//c7 | 1
            int y2 = (int)(y - dist * 3);//c8 | 1
            int x3 = (int)(x );//c9 | 1
            int y3 = (int)(y + dist * 5);//c10 | 1
            int x4 = (int)(x - dist * 4.5);//c11 | 1
            int y4 = (int)(y + dist * 1.5);//c12 | 1
            int x5 = (int)(x + dist * 4.5);//c13 | 1
            int y5 = (int)(y + dist * 1.5);//c14 | 1


            // Draw the surrounding squares rotated 45 degrees
            DrawRotatedSquare(Render, x1, y1, Width , Height , size );//c15 | n^2
            DrawRotatedSquare(Render, x2, y2, Width , Height , size );//c16 | n^2
            DrawRotatedSquare(Render, x3, y3, Width , Height , size);//c17 | n^2
            DrawRotatedSquare(Render, x4, y4, Width , Height , size);//c18 | n^2
            DrawRotatedSquare(Render, x5, y5, Width , Height , size);//c19 | n^2

            // Recursively draw smaller squares around each of the surrounding squares
            DrawSurroundingSquares(Render, x1, y1, Width /4, Height/4, size / 4, depth - 1);// T(n/4)
            DrawSurroundingSquares(Render, x2, y2, Width / 4, Height / 4, size / 4, depth - 1);// T(n/4)
            DrawSurroundingSquares(Render, x3, y3, Width / 4, Height /4, size / 4, depth - 1);// T(n/4)
            DrawSurroundingSquares(Render, x4, y4, Width / 4, Height / 4, size / 4, depth - 1);// T(n/4)
            DrawSurroundingSquares(Render, x5, y5, Width / 4, Height / 4, size / 4, depth - 1);// T(n/4)

        }// T(n) = n^2



    }
}
