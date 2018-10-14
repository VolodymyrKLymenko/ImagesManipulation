using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTask.CommonExtensions;

namespace WpfTask.ViewModel
{
    public class EcvalizeCalc
    {
       /* public static Bitmap EkvilizeBitMao(Bitmap bmp, ColorChannel chanel)
        {
            var middleVale = 0.0;

            int[] R = new int[256];
            byte[] N = new byte[256];
            byte[] left = new byte[256];
            byte[] right = new byte[256];

            for (int i = 0; i < N.Length - 1; i++)
                N[i] = 0;

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

        }*/

        public static Bitmap equalizing(Bitmap bitmap)
        {
            Bitmap bmp = new Bitmap(bitmap);

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] grayValues = new byte[bytes];
            int[] R = new int[256];
            byte[] N = new byte[256];
            byte[] left = new byte[256];
            byte[] right = new byte[256];
            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);
            for (int i = 0; i < grayValues.Length; i++) ++R[grayValues[i]];
            int z = 0;
            int Hint = 0;
            int Havg = grayValues.Length / R.Length;
            for (int i = 0; i < N.Length - 1; i++)
            {
                N[i] = 0;
            }
            for (int j = 0; j < R.Length; j++)
            {
                if (z > 255) left[j] = 255;
                else left[j] = (byte)z;
                Hint += R[j];
                while (Hint > Havg)
                {
                    Hint -= Havg;
                    z++;
                }
                if (z > 255) right[j] = 255;
                else right[j] = (byte)z;

                N[j] = (byte)((left[j] + right[j]) / 2);
            }
            for (int i = 0; i < grayValues.Length; i++)
            {
                if (left[grayValues[i]] == right[grayValues[i]]) grayValues[i] = left[grayValues[i]];
                else grayValues[i] = N[grayValues[i]];
            }

            System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
    }
}
