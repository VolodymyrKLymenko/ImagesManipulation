using bmp_images_manipulation.CommonExtensions;
using bmp_images_manipulation.ImagesFormatter;
using System;
using System.Drawing;
using System.IO;

namespace bmp_images_manipulation
{
    class Program
    {
        const string bmpImagesPath = @"D:\Education\University\4_semester\Images Manipulation\BMP images\";
        const int qualityComparison = 10;

        static void Main(string[] args)
        {
            Console.WriteLine("RLE is processing");
            RLE_compresser.compressBmpImage(bmpImagesPath + "test.bmp", bmpImagesPath + "test_compressed.bmp");

            Console.WriteLine("\nRLE/test.bmp image is comprassed to RLE Format  And Saved in RLE/test_compressed.bmp");
            Console.ReadLine();


            Console.WriteLine("LZW is processing");
            LZW_comprasser.compressBmpImage(bmpImagesPath + "LZW/test5.bmp", bmpImagesPath + "LZW/test_compressed.tiff");

            Console.WriteLine("\nLZW/test5.bmp image is comprassed to LZW Format  And Saved in LZW/test_compressed.bmp");
            Console.ReadLine();


            Console.WriteLine("JPEG is processing");
            JpegComprasser.compressBmpImage(bmpImagesPath + "JPEG/test.bmp", bmpImagesPath + "JPEG/test_compressed.jpg");

            Console.WriteLine("\nJPEG/test.bmp image is comprassed to LZW Format  And Saved in JPEG/test_compressed.jpeg");
            Console.ReadLine();



            Console.WriteLine("Done\n Start comparise Time of the same image");
            Console.ReadLine();
            DateTime startPoint = DateTime.Now;
            RLE_compresser.compressBmpImage(bmpImagesPath + "TimeComparison/COLLISN.bmp", bmpImagesPath + "TimeComparison/rle_compressed.bmp");
            DateTime endPoint = DateTime.Now;
            Console.WriteLine($"BMP TO RLE Take: {(endPoint - startPoint).TotalMilliseconds} milliseconds");

            startPoint = DateTime.Now;
            LZW_comprasser.compressBmpImage(bmpImagesPath + "TimeComparison/COLLISN.bmp", bmpImagesPath + "TimeComparison/lzw_compressed.tiff");
            endPoint = DateTime.Now;
            Console.WriteLine($"BMP TO LZW Take: {(endPoint - startPoint).TotalMilliseconds} milliseconds");

            startPoint = DateTime.Now;
            JpegComprasser.compressBmpImage(bmpImagesPath + "TimeComparison/COLLISN.bmp", bmpImagesPath + "TimeComparison/jpg_compressed.jpg");
            endPoint = DateTime.Now;
            Console.WriteLine($"BMP TO JPEG Take: {(endPoint - startPoint).TotalMilliseconds} milliseconds");
            startPoint = DateTime.Now;
            JpegComprasser.deCompressBmpImage(bmpImagesPath + "TimeComparison/jpg_compressed.jpg", bmpImagesPath + "TimeComparison/rle_decompressed.bmp");
            endPoint = DateTime.Now;
            Console.WriteLine($"JPEG TO BMP Take: {(endPoint - startPoint).TotalMilliseconds} milliseconds\n\n");



            Console.WriteLine("Done\n Start time comparison of the same image");
            Console.ReadLine();
            Console.WriteLine("JPEG minus BMP");
            var bmpBytesMatrix = ImageExtension.GetBitMapColorMatrix(bmpImagesPath + "TimeComparison/COLLISN.bmp");
            var jpegBytes = ImageExtension.GetBitMapColorMatrix(bmpImagesPath + "TimeComparison/jpg_compressed.jpg");
            var comResBmpJpg = CompareImages(bmpBytesMatrix, jpegBytes, qualityComparison);
            ImageExtension.SaveBitmap(ImageExtension.GetBitmap(comResBmpJpg), bmpImagesPath + "TimeComparison/comResBmpJpg.bmp");

            Console.WriteLine("Done\n Start time comparison of the same image");
            Console.ReadLine();
            Console.WriteLine("LZW minus BMP");
            bmpBytesMatrix = ImageExtension.GetBitMapColorMatrix(bmpImagesPath + "TimeComparison/COLLISN.bmp");
            var lzwBytes = ImageExtension.GetBitMapColorMatrix(bmpImagesPath + "TimeComparison/lzw_compressed.tiff");
            var comResBmpLZW = CompareImages(bmpBytesMatrix, lzwBytes, qualityComparison);
            ImageExtension.SaveBitmap(ImageExtension.GetBitmap(comResBmpLZW), bmpImagesPath + "TimeComparison/comResBmpLzw.bmp");

            Console.WriteLine("Done\n Start time comparison of the same image");
            Console.ReadLine();
            Console.WriteLine("RLE minus BMP");
            bmpBytesMatrix = ImageExtension.GetBitMapColorMatrix(bmpImagesPath + "TimeComparison/COLLISN.bmp");
            var rleBytes = ImageExtension.GetBitMapColorMatrix(bmpImagesPath + "TimeComparison/rle_compressed.bmp");
            var comResBmpRle = CompareImages(bmpBytesMatrix, rleBytes, 200);
            ImageExtension.SaveBitmap(ImageExtension.GetBitmap(comResBmpRle), bmpImagesPath + "TimeComparison/comResBmpRle.bmp");

            Console.ReadLine();
        }

        private static Color[][] CompareImages(Color[][] bmpBytes, Color[][] jpgBytes, byte quality)
        {
            int size_x = Math.Max(bmpBytes.Length, jpgBytes.Length);
            int size_y = Math.Max(bmpBytes[0].Length, jpgBytes[0].Length);

            Color[][] res = new Color[size_x][];
            for (int i = 0; i < size_x; i++)
            {
                res[i] = new Color[size_y];
            }

            for (int i = 0; i < size_x; i++)
            {
                for (int j = 0; j < size_y; j++)
                {
                    var R = (jpgBytes[i][j].R - bmpBytes[i][j].R) > 0 
                                ? ((jpgBytes[i][j].R - bmpBytes[i][j].R) * quality) < 256 ? (jpgBytes[i][j].R - bmpBytes[i][j].R) : 255
                                : 0;
                    var G = (jpgBytes[i][j].G - bmpBytes[i][j].G) > 0
                                ? ((jpgBytes[i][j].G - bmpBytes[i][j].G) * quality) < 256 ? (jpgBytes[i][j].G - bmpBytes[i][j].G) : 255
                                : 0;
                    var B = (jpgBytes[i][j].B - bmpBytes[i][j].B) > 0
                                ? ((jpgBytes[i][j].B - bmpBytes[i][j].B) * quality) < 256 ? (jpgBytes[i][j].B - bmpBytes[i][j].B) : 255
                                : 0;

                    res[i][j] = Color.FromArgb(R, G, B);
                }
            }

            return res;
        }
    }
}
