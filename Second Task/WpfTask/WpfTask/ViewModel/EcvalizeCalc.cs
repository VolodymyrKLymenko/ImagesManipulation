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
