using bmp_images_manipulation.CommonExtensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace bmp_images_manipulation.ImagesFormatter
{
    public class RLE_compresser
    {
        public static void compressBmpImage(string source, string dist)
        {
            Bitmap bitMapImage = null;
            using (var image = new Bitmap(source))
            {
                bitMapImage = new Bitmap(image);
            }

            var codecs = ImageCodecInfo.GetImageDecoders();
            var codecInfo = codecs.FirstOrDefault(codec => codec.FormatID == ImageFormat.Bmp.Guid);
            var encoder = System.Drawing.Imaging.Encoder.Compression;
            var encoderParameters = new EncoderParameters(1);

            var parameter = new EncoderParameter(encoder, (byte)EncoderValue.CompressionRle);
            encoderParameters.Param[0] = parameter;
            bitMapImage.Save(dist, codecInfo, encoderParameters);
        }

        public static void compressCustomBmpImage(string source, string dist)
        {
            byte[] data = FileExtensions.readBytesFromFile(source);
            byte[] compressedData = new byte[data.Length * 2];

            int count = 1;
            int newSize = 0;
            byte value = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] == value)
                {
                    count++;
                }
                else
                {
                    compressedData[newSize++] = value;
                    compressedData[newSize++] = (byte)count;
                    value = data[i];
                    count = 1;
                }

                if (i == data.Length - 1)
                {
                    compressedData[newSize++] = value;
                    compressedData[newSize++] = (byte)count;
                }
            }

            FileExtensions.writeBytesInFile(dist, compressedData, FileMode.OpenOrCreate, newSize);
        }

        public static void deCompressBmpImage(string source, string dist)
        {
            byte[] data = FileExtensions.readBytesFromFile(source);

            List<byte> decomprassedData = new List<byte>();
            for (int i = 0; i < data.Length; i+=2)
            {
                for (int j = 0; j < data[i+1]; j++)
                {
                    decomprassedData.Add(data[i]);
                }
            }

            FileExtensions.writeBytesInFile(dist, decomprassedData.ToArray(), FileMode.OpenOrCreate);
        }
    }
}
