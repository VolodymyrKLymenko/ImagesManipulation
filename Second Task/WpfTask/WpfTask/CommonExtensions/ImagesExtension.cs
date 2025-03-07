﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace WpfTask.CommonExtensions
{
    public class ImageExtension
    {
        public static Color[][] GetBitMapColorMatrix(string bitmapFilePath)
        {
            Bitmap b1 = new Bitmap(bitmapFilePath);

            int hight = b1.Height;
            int width = b1.Width;

            Color[][] colorMatrix = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j);
                }
            }
            return colorMatrix;
        }

        public static Color[][] GetolorMatrix(Bitmap bmp)
        {
            int hight = bmp.Height;
            int width = bmp.Width;

            Color[][] colorMatrix = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = bmp.GetPixel(i, j);
                }
            }
            return colorMatrix;
        }

        public static void SaveBitmap(Bitmap bitmap, string dist)
        {
            var bmp = new Bitmap(bitmap);

            bmp.Save(dist);
        }

        public static Bitmap GetBitmap(Color[][] colors)
        {
            Bitmap bmp = new Bitmap(colors.Length, colors[0].Length);

            for (int i = 0; i < colors.Length; i++)
                for (int j = 0; j < colors[0].Length; j++)
                    bmp.SetPixel(i, j, colors[i][j]);

            return bmp;
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
