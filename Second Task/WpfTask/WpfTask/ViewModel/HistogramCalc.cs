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
        public static int[] GetHistogram(Bitmap bmp, ColorChannel colorChannel)
        {
            int[] myHistogram = new int[256];
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
    }
}
