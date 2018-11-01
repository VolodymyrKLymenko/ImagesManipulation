using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTask.CommonExtensions;

namespace WpfTask.ViewModel
{
    public static class MaskApplier
    {
        public static double[][] privetMaskVertical =
        {
            new double[] { 1, 0, -1 },
            new double[] { 1, 0, -1 },
            new double[] { 1, 0, -1 }
        };
        public static double[][] privetMaskHorizontal =
        {
            new double[] { -1, -2, -1 },
            new double[] { 0, 0, 0 },
            new double[] { 1, 2, 1 }
        };

        public static double[][] sobelMaskVertical =
        {
            new double[] { 1, 0, -1 },
            new double[] { 2, 0, -2 },
            new double[] { 1, 0, -1 }
        };
        public static double[][] sobelMaskHorizontal =
        {
            new double[] { -1, -1, -1 },
            new double[] { 0, 0, 0 },
            new double[] { 1, 1, 1 }
        };

        public static double[][] robertsMaskVertical =
        {
            new double[] { 0, -1 },
            new double[] { 1,  0 }
        };
        public static double[][] robertsMaskHorizontal =
        {
            new double[] { -1, 0 },
            new double[] { 0,  1 }
        };

        public static Bitmap ApplyMaskForAllChanales(Bitmap img, double[][] mask, int treshe = 0)
        {
            Bitmap resBmp = new Bitmap(img);
            var colors = ImageExtension.GetolorMatrix(img);
            int r_tr = 0;
            int g_tr = 0;
            int b_tr = 0;

            for (int i = 0; i < colors.Length; i++)
            {
                for (int j = 0; j < colors[0].Length; j++)
                {
                    if (colors[i][j].R > r_tr) { r_tr = colors[i][j].R; }
                    if (colors[i][j].G > g_tr) { g_tr = colors[i][j].G; }
                    if (colors[i][j].B > b_tr) { b_tr = colors[i][j].B; }
                }
            }

            r_tr /= 2;
            g_tr /= 2;
            b_tr /= 2;

            if (treshe != 0)
            {
                r_tr = treshe;
                g_tr = treshe;
                b_tr = treshe;
            }

            for (int i = 0; i < colors.Length; i++)
                for (int j = 0; j < colors[0].Length; j++)
                {
                    var itemMatrix = getItemMatrix(i, j, colors, mask.Length);
                    var newRValue = ItemCalculation(itemMatrix, mask, r_tr, ColorChannel.Red);
                    var bval = (byte)newRValue;
                    var newGValue = (byte)ItemCalculation(itemMatrix, mask, g_tr, ColorChannel.Green);
                    var newBValue = (byte)ItemCalculation(itemMatrix, mask, b_tr, ColorChannel.Blue);

                    var newVal = (newRValue > 0 || newGValue > 0 || newBValue > 0) ? 255 : 0;

                    Color newColor;
                    newColor = Color.FromArgb(colors[i][j].A, (byte)newVal, newVal, newVal);

                    resBmp.SetPixel(i, j, newColor);
                }

            return resBmp;
        }

        public static Bitmap ApplyMask(Bitmap img, double[][] mask, ColorChannel colorChannel, int treshe = 0)
        {
            Bitmap resBmp = new Bitmap(img);

            var col = (int)colorChannel;
            var colors = ImageExtension.GetolorMatrix(img);
            int r_tr = 0;
            int g_tr = 0;
            int b_tr = 0;

            for (int _i = 0; _i < colors.Length; _i++)
            {
                for (int _j = 0; _j < colors[0].Length; _j++)
                {
                    if (colors[_i][_j].R > r_tr) { r_tr = colors[_i][_j].R; }
                    if (colors[_i][_j].G > g_tr) { g_tr = colors[_i][_j].G; }
                    if (colors[_i][_j].B > b_tr) { b_tr = colors[_i][_j].B; }
                }
            }

            r_tr /= 2;
            g_tr /= 2;
            b_tr /= 2;
            int tr = (treshe != 0) ? treshe : (r_tr + g_tr + b_tr) / 3;

            for (int i = 0; i < colors.Length; i++)
                for (int j = 0; j < colors[0].Length; j++)
                {
                    var itemMatrix = getItemMatrix(i, j, colors, mask.Length);
                    var newValue = (byte)ItemCalculation(itemMatrix, mask, treshe, colorChannel);

                    Color newColor;

                    switch ((int)colorChannel)
                    {
                        //blue
                        case 0:
                            {
                                newColor = Color.FromArgb(colors[i][j].A, colors[i][j].R, colors[i][j].G, newValue);
                                break;
                            }
                        // green
                        case 1:
                            {
                                newColor = Color.FromArgb(colors[i][j].A, colors[i][j].R, newValue, colors[i][j].B);
                                break;
                            }
                        // red
                        case 2:
                            {
                                newColor = Color.FromArgb(colors[i][j].A, newValue, colors[i][j].G, colors[i][j].B);
                                break;
                            }
                        default:
                            {
                                newColor = Color.FromArgb(colors[i][j].A, colors[i][j].R, colors[i][j].G, colors[i][j].B);
                                break;
                            }
                    }

                    resBmp.SetPixel(i, j, newColor);
                }

            return resBmp;
        }

        private static Color[][] getItemMatrix(int _i, int _j, Color[][] matrix, int dimension)
        {
            Color[][] res = new Color[dimension][];
            for (int i = 0; i < dimension; i++)
                res[i] = new Color[dimension];

            for (int i = 0, ii = _i - (dimension/2); i < dimension; i++, ii++)
            {
                for (int j = 0, jj = _j -(dimension/2); j < dimension; j++, jj++)
                {
                    var __i = ((matrix.Length - 1) > Math.Abs(ii))
                        ? Math.Abs(ii)
                        : ((matrix.Length - 1) - (Math.Abs(ii) - (matrix.Length - 1)));
                    var __j = ((matrix[0].Length - 1) > Math.Abs(jj))
                        ? Math.Abs(jj)
                        : ((matrix[0].Length - 1) - (Math.Abs(jj) - (matrix[0].Length - 1)));

                    var R = matrix[Math.Abs(__i)][Math.Abs(__j)].R;
                    var G = matrix[Math.Abs(__i)][Math.Abs(__j)].G;
                    var B = matrix[Math.Abs(__i)][Math.Abs(__j)].B;

                    res[i][j] = Color.FromArgb(R, G, B);
                }
            }

            return res;
        }

        private static double ItemCalculation(Color[][] itemMatrix, double[][] mask, int treshe, ColorChannel channel)
        {
            treshe -= 10;

            switch ((int)channel)
            {
                //blue
                case 0:
                    {
                        var res = 0.0;
                        for (int i = 0; i < mask.Length; i++)
                        {
                            for (int j = 0; j < mask.Length; j++)
                            {
                                res += ((double)itemMatrix[i][j].B) * mask[j][i];
                            }
                        }
                        return res > treshe ? 255 : 0;
                    }
                // green
                case 1:
                    {
                        var res = 0.0;
                        for (int i = 0; i < mask.Length; i++)
                        {
                            for (int j = 0; j < mask.Length; j++)
                            {
                                res += ((double)itemMatrix[i][j].G) * mask[j][i];
                            }
                        }
                        return res > treshe ? 255 : 0;
                    }
                // red
                case 2:
                    {
                        var res = 0.0;
                        for (int i = 0; i < mask.Length; i++)
                        {
                            for (int j = 0; j < mask.Length; j++)
                            {
                                res += itemMatrix[i][j].R * mask[j][i];
                            }
                        }
                        return res > treshe ? 255 : 0;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
    }
}
