using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleREngine
{
    internal class Renderer
    {
        public Renderer(string OutputName, uint Width, uint Height, uint FillingColor) // Color format is ARGB (to define recomended hex: 0xAARRGGBB), coordinates start from bottom left corner, 1 unit is 1 pixel, only 256 colors can be used
        {
            this.Width = Width;
            this.Height = Height;
            this.Buffer = new byte[Width * Height];
            this.Colors = new List<uint>();

            this.Colors.Add(FillingColor);

            Array.Fill<byte>(this.Buffer, 0);

            this.OutputName = OutputName;
            if (!OutputName.Contains(".bmp"))
                this.OutputName += ".bmp";
        }
        public uint GetWidth()
        {
            return this.Width;
        }
        public uint GetHeight()
        {
            return this.Height;
        }
        public void DrawFilledCircle(double X, double Y, double R, double Precision = 0.5, uint Color = 0)
        {
            for (double i = 90; i < 270; i += Precision)
            {
                double Angle = ToRadian(i);

                double CX = (Math.Cos(Angle) * R) + X;
                double CY = (Math.Sin(Angle) * R) + Y;

                double EX = (Math.Cos(Math.PI - Angle) * R) + X;

                for (; CX < EX; CX++)
                    SetPixel(CX, CY, Color);
            }
        }


        public void DrawCircle(double X, double Y, double R, double Precision = 0.5, uint Color = 0)
        {
            for (double i = 0; i < 360; i += Precision)
            {
                double Angle = ToRadian(i);

                double CX = (Math.Cos(Angle) * R) + X;
                double CY = (Math.Sin(Angle) * R) + Y;

                SetPixel(CX, CY, Color);
            }
        }

        public void DrawFilledSquare(double X, double Y, double Width, double Height, uint Color = 0)
        {
            for (double SY = 0; SY < Height; SY++)
            {
                for (double SX = 0; SX < Width; SX++)
                {
                    SetPixel(SX + X, SY + Y, Color);
                }
            }
        }

        public void DrawSquare(double X, double Y, double Width, double Height, uint Color)
        {
            DrawLine(X, Y, X + Width, Y, 1, Color);
            DrawLine(X, Y, X, Y + Height, 1, Color);
            DrawLine(X + Width, Y, X + Width, Y + Height, 1, Color);
            DrawLine(X, Y + Height, X + Width, Y + Height, 1, Color);
        }

        public void DrawFilledTriangle(double X0, double Y0, double X1, double Y1, double X2, double Y2, double Precision = 0.5, uint Color = 0)
        {
            double Length = Math.Sqrt(Math.Pow(X1 - X0, 2) + Math.Pow(Y1 - Y0, 2));

            double XStep = (X1 - X0) / (Length / Precision);
            double YStep = (Y1 - Y0) / (Length / Precision);

            double XRun = X0;
            double YRun = Y0;
            for (double i = 0; i < Length; i += Precision)
            {
                XRun += XStep;
                YRun += YStep;

                DrawLine(XRun, YRun, X2, Y2, Precision, Color);
            }
        }
    
        public void DrawRotatedFilledSquare(double X, double Y, double Width, double Height, uint Color = 0)
        {
            // Define center point of square
            double Cx = X + Width / 2;
            double Cy = Y + Height / 2;

            // Rotate each pixel in square by 45 degrees and set color
            for (double SY = 0; SY < Height; SY++)
            {
                for (double SX = 0; SX < Width; SX++)
                {
                    // Calculate new pixel location after rotation
                    double Xp = SX - Width / 2;
                    double Yp = SY - Height / 2;
                    double Xnew = Xp * Math.Cos(Math.PI / 4) - Yp * Math.Sin(Math.PI / 4) + Cx;
                    double Ynew = Xp * Math.Sin(Math.PI / 4) + Yp * Math.Cos(Math.PI / 4) + Cy;

                    SetPixel(Xnew, Ynew, Color);
                }
            }
        }




        public void DrawLine(double X0, double Y0, double X1, double Y1, double Precision = 0.5, uint Color = 0)
        {
            double Length = Math.Sqrt(Math.Pow(X0 - X1, 2) + Math.Pow(Y0 - Y1, 2));

            double XStep = (X1 - X0) / (Length / Precision);
            double YStep = (Y1 - Y0) / (Length / Precision);

            double XRun = X0;
            double YRun = Y0;
            for (double i = 0; i < Length; i += Precision)
            {
                XRun += XStep;
                YRun += YStep;

                SetPixel(XRun, YRun, Color);
            }
        }

        private static double ToRadian(double Angle)
        {
            return Angle * (Math.PI / 180);
        }

        private void SetPixel(double X, double Y, uint Color)
        {
            int Pixel = GetPixel(X, Y);
            if (Pixel < 0)
                return;

            int CIdx = Colors.IndexOf(Color);
            if (CIdx < 0)
            {
                Colors.Add(Color);
                CIdx = Colors.Count - 1;
            }

            Buffer[Pixel] = (byte)CIdx;
        }

        private int GetPixel(double X, double Y)
        {
            int Pixel = ((int)Math.Round(Y) * (int)Width) + (int)Math.Round(X);
            if (Pixel > Buffer.Length)
                return -1;

            if (X < 0)
                return -1;
            else if (X > Width)
                return -1;

            return Pixel;
        }

        public void Write()
        {
            using (FileStream File = new FileStream(OutputName, FileMode.Create, FileAccess.Write))
            {
                File.Write(new byte[] { 0x42, 0x4D }); // BM
                File.Write(BitConverter.GetBytes(Height * Width * sizeof(byte) + sizeof(uint) * 255 + 0x36)); // Size
                File.Write(BitConverter.GetBytes(0)); // Reserved (0s)
                File.Write(BitConverter.GetBytes(0x1A)); // Image Offset (size of the header)

                File.Write(BitConverter.GetBytes(40)); // Header size (size is 40 bytes)
                File.Write(BitConverter.GetBytes(Width)); // Width
                File.Write(BitConverter.GetBytes(Height)); // Height
                File.Write(BitConverter.GetBytes((ushort)1)); // Color plane
                File.Write(BitConverter.GetBytes((ushort)8)); // bits per pixel
                File.Write(BitConverter.GetBytes((uint)0)); // Compression
                File.Write(BitConverter.GetBytes((uint)0)); // Image size (set 0 due to compression)
                File.Write(BitConverter.GetBytes((uint)0)); // Horizontal pixels per meter
                File.Write(BitConverter.GetBytes((uint)0)); // Vertical pixels per meter
                File.Write(BitConverter.GetBytes((uint)0)); // Used colors (0 all colors used)
                File.Write(BitConverter.GetBytes((uint)0)); // Important Colors (0 all colors are important)

                byte[] Pallete = new byte[256 * sizeof(uint)];

                int RunColor = 0;
                foreach (uint Color in Colors)
                {
                    Pallete[RunColor + 0] = (byte)((Color >> 16) & 0xFF);
                    Pallete[RunColor + 1] = (byte)((Color >> 8) & 0xFF);
                    Pallete[RunColor + 2] = (byte)((Color >> 0) & 0xFF);
                    Pallete[RunColor + 3] = 0;

                    RunColor += 4;
                }

                File.Write(Pallete);
                File.Write(Buffer);
                File.Close();
            }
        }

        private readonly uint Width;
        private readonly uint Height;
        private readonly byte[] Buffer;
        private readonly List<uint> Colors;
        private readonly string OutputName;
    }
}
