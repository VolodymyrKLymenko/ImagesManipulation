using bmp_images_manipulation.CommonExtensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace bmp_images_manipulation.ImagesFormatter
{
    public class JpegComprasser
    {
        public static void compressBmpImage(string source, string dist)
        {
            Bitmap bmp = new Bitmap(source);

            saveJpeg(dist, bmp, 100);
        }

        public static void deCompressBmpImage(string source, string dist)
        {
            Bitmap bitmap;
            using (Stream bmpStream = System.IO.File.Open(source, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);
            
            }

            FileExtensions.writeBytesInFile(dist, ImageToByte(bitmap), FileMode.OpenOrCreate);
        }

        public static byte[] ImageToByte(Bitmap img)
        {
            MemoryStream ms = new MemoryStream();

            // Save to memory using the Jpeg format
            img.Save(ms, ImageFormat.Jpeg);

            // read to end
            byte[] bmpBytes = ms.GetBuffer();

            img.Dispose();

            ms.Close();

            return bmpBytes;
        }

        private static void saveJpeg(string path, Bitmap img, long quality)
        {
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);

            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo getEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i<codecs.Length; i++)

            if (codecs[i].MimeType == mimeType)
                return codecs[i];

            return null;
        }
    }
}
