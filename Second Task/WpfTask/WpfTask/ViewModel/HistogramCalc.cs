using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTask.CommonExtensions;

namespace WpfTask.ViewModel
{
    public class HistogramCalc
    {
        public static long[] CustomGetHistogram(Bitmap bmp, ColorChannel colorChannel)
        {
            long[] myHistogram = new long[256];
            for (int i = 0; i < myHistogram.Length; i++)
                myHistogram[i] = 0;

            var col = (int)colorChannel;
            var colors = ImageExtension.GetolorMatrix(bmp);

            for (int i = 0; i < colors.Length; i++)
                for (int j = 0; j < colors[0].Length; j++)
                {
                    switch (col)
                    {
                        case 0:
                            {
                                ++myHistogram[colors[i][j].B];
                                break;
                            }
                        case 1:
                            {
                                ++myHistogram[colors[i][j].G];
                                break;
                            }
                        case 2:
                            {
                                ++myHistogram[colors[i][j].R];
                                break;
                            }
                        case 3:
                            {
                                ++myHistogram[colors[i][j].A];
                                break;
                            }
                        default:
                            continue;
                    }
                }

            return myHistogram;
        }

        public static long[] GetHistogram(Bitmap b, ColorChannel colorChannel)
        {
            long[] myHistogram = new long[256];
            BitmapData bmData = null;

            int col = (int)colorChannel;

            try
            {
                //Lock it fixed with 24bpp
                bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                int scanline = bmData.Stride;
                System.IntPtr Scan0 = bmData.Scan0;
                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;
                    int nWidth = b.Width;
                    int nHeight = b.Height;
                    for (int y = 0; y < nHeight; y++)
                    {
                        if (y == 192)
                            break;
                        for (int x = 0; x < nWidth; x++)
                        {
                                long Temp = 0;
                                Temp += p[col];

                                myHistogram[Temp]++;
                                //we do not need to use any offset, we always can increment by pixelsize when
                                //locking in 32bppArgb - mode
                                p += 4;
                        }
                    }
                }
                b.UnlockBits(bmData);
            }
            catch
            {
                try
                {
                    b.UnlockBits(bmData);
                }
                catch
                {
                }
            }
            return myHistogram;
        }
    }
}
