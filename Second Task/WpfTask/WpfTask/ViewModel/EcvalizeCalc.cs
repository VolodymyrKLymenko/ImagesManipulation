using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTask.CommonExtensions;

namespace WpfTask.ViewModel
{
    public static class EcvalizeCalc
    {
        private const int CountColors = 256;

        public static Bitmap EkvilizeCustom(Bitmap bmp, int[] histogramValues, ColorChannel chanel)
        {
            Bitmap resultBmp = new Bitmap(bmp);

            int[] cdf = new int[histogramValues.Length];
            int cdfMin = 0;

            for (int i = 0; i < histogramValues.Length; i++)
            {
                cdf[i] = (i == 0) ? 0 : cdf[i - 1];
                cdf[i] += histogramValues[i];

                if (cdf[i] > 0 && (cdfMin == 0 || cdf[i] < cdfMin))
                    cdfMin = cdf[i];
            }

            Color[][] colors = ImageExtension.GetolorMatrix(bmp);
            int countPixels = colors.Length * colors[0].Length;

            for (int i = 0; i < colors.Length; i++)
            {
                for (int j = 0; j < colors[i].Length; j++)
                {
                    byte R = colors[i][j].R;
                    byte G = colors[i][j].G;
                    byte B = colors[i][j].B;

                    switch (chanel)
                    {
                        case ColorChannel.Blue:
                            {
                                B = (byte)((int)((((double)(cdf[colors[i][j].B] - cdfMin))/((double)countPixels))
                                    *((double)(CountColors -1))));
                                break;
                            }
                        case ColorChannel.Green:
                            {
                                G = (byte)((int)((((double)(cdf[colors[i][j].G] - cdfMin)) / ((double)countPixels))
                                    * ((double)(CountColors - 1))));
                                break;
                            }
                        case ColorChannel.Red:
                            {
                                R = (byte)((int)((((double)(cdf[colors[i][j].R] - cdfMin)) / ((double)countPixels))
                                    * ((double)(CountColors - 1))));
                                break;
                            }
                        default:
                            break;
                    }

                    resultBmp.SetPixel(i, j, Color.FromArgb(colors[i][j].A, R, G, B));
                }
            }

            return resultBmp;
        }

        public static Bitmap equalizing(Bitmap bitmap)
        {
            Bitmap bmp = new Bitmap(bitmap);

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int countBytes = bmpData.Stride * bmp.Height;

            byte[] bmpValues = new byte[countBytes];
            byte[] aproxResults = new byte[256];
            int [] byteCounts = new int[256];

            byte[] left = new byte[256];
            byte[] right = new byte[256];

             // read bytes from bitmap in bmpValues
            System.Runtime.InteropServices.Marshal.Copy(ptr, bmpValues, 0, countBytes);

             // count of each bytes
            for (int i = 0; i < bmpValues.Length; i++) ++byteCounts[bmpValues[i]];

            int z = 0;
            int hint = 0;
            int avarage = bmpValues.Length / byteCounts.Length;
            for (int i = 0; i < aproxResults.Length - 1; i++) aproxResults[i] = 0;

            for (int j = 0; j < byteCounts.Length; j++)
            {
                if (z > 255)
                    left[j] = 255;
                else
                    left[j] = (byte)z;

                hint += byteCounts[j];
                while (hint > avarage)
                {
                    hint -= avarage;
                    z++;
                }

                if (z > 255) right[j] = 255;
                else right[j] = (byte)z;

                aproxResults[j] = (byte)((left[j] + right[j]) / 2);
            }
            for (int i = 0; i < bmpValues.Length; i++)
            {
                if (left[bmpValues[i]] == right[bmpValues[i]])
                    bmpValues[i] = left[bmpValues[i]];
                else
                    bmpValues[i] = aproxResults[bmpValues[i]];
            }

            System.Runtime.InteropServices.Marshal.Copy(bmpValues, 0, ptr, countBytes);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
    }
}
