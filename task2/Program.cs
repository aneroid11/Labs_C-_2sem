using System;
using System.Runtime.InteropServices;

namespace Lab4_task2
{
    static class PPM
    {
        public struct Color
        {
            public byte r, g, b;
        }

        public struct Image 
        {
            public int width, height;
            public IntPtr pixels;
        }

        [DllImport("libppm.so", EntryPoint = "RGB", CallingConvention = CallingConvention.FastCall)]
        public static extern Color RGB(byte r, byte g, byte b);

        [DllImport("libppm.so", EntryPoint = "PPMImage_Create", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void Create(Image* img, int w, int h);

        [DllImport("libppm.so", EntryPoint = "PPMImage_Destroy", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void Destroy(Image* img);

        [DllImport("libppm.so", EntryPoint = "PPMImage_LoadFromFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void LoadFromFile(Image* img, string fileName);

        [DllImport("libppm.so", EntryPoint = "PPMImage_WriteInFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void WriteInFile(Image* img, string fileName);

        [DllImport("libppm.so", EntryPoint = "PPMImage_PutPixel", CallingConvention = CallingConvention.FastCall)]
        public static extern unsafe void PutPixel(Image* img, int x, int y, Color cl);

        [DllImport("libppm.so", EntryPoint = "PPMImage_GetPixel", CallingConvention = CallingConvention.FastCall)]
        public static extern unsafe Color GetPixel(Image* img, int x, int y);

        [DllImport("libppm.so", EntryPoint = "PPMImage_FlipHorizontally", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void FlipHorizontally(Image* img);

        [DllImport("libppm.so", EntryPoint = "PPMImage_FlipVertically", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void FlipVertically(Image* img);
    }

    class MainClass
    {
        public static unsafe int Main(string[] args)
        {
            PPM.Image image;
            PPM.LoadFromFile(&image, "image.ppm");

            if (image.width != 1024 || image.height != 1024)
            {
                Console.WriteLine("image.ppm should be 1024x1024!");
                Console.WriteLine(image.width);
                PPM.Destroy(&image);
                return 1;
            }

            PPM.FlipHorizontally(&image);

            for (int x = 0; x < 1024; x++)
            {
                for (int y = 0; y < 1024; y++)
                {
                    PPM.Color pixel = PPM.GetPixel(&image, x, y);
                    pixel.r = pixel.g = pixel.b;
                    PPM.PutPixel(&image, x, y, pixel);
                }
            }

            PPM.WriteInFile(&image, "ppm_image.ppm");
            PPM.Destroy(&image);
            return 0;
        }
    }
}
