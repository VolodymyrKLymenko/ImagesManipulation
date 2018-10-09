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
    public class LzwStringTable
    {
        private Dictionary<string, int> table = new Dictionary<string, int>();
        private int nextAvailableCode = 256;

        public LzwStringTable()
        {
        }

        public void AddCode(string s)
        {
            if (s.Contains(",") && !table.ContainsKey(s))
                table[s] = nextAvailableCode++;
        }

        public int GetCode(string str)
        {
            if (!str.Contains(",") && Int32.Parse(str) <= 255)
                return Int32.Parse(str);
            else
                return table[str];
        }

        public bool Contains(string s)
        {
            return (!s.Contains(",") && Int32.Parse(s) <= 255) || table.ContainsKey(s);
        }
    }

    public class LZW_comprasser
    {
        private const int NumBytesPerCode = 1;


        public static void compressBmpImage(string source, string dist)
        {
            Bitmap bitmap = null;
            using (var image = new Bitmap(source))
            {
                bitmap = new Bitmap(image);
            }

            var codecs = ImageCodecInfo.GetImageDecoders();
            var codecInfo = codecs.FirstOrDefault(codec => codec.FormatID == ImageFormat.Bmp.Guid);
            var encoder = System.Drawing.Imaging.Encoder.Compression;
            var encoderParameters = new EncoderParameters(1);
            string pathToTiffFile = dist;
            var parameter = new EncoderParameter(encoder, (long)EncoderValue.CompressionLZW);
            encoderParameters.Param[0] = parameter;
            bitmap.Save(pathToTiffFile, codecInfo, encoderParameters);
        }

        public static void compressCustomBmpImage(string source, string dist)
        {
            LzwStringTable table = new LzwStringTable();
            byte[] data = FileExtensions.readBytesFromFile(source);
            string compressedImg = "";

            byte firstChar = data[0];
            string match = firstChar.ToString();

            for (int i = 1; i < data.Length; i++)
            {
                string nextMatch = match + "," + data[i].ToString();

                if (table.Contains(nextMatch))
                {
                    match = nextMatch;
                }
                else
                {
                    compressedImg += (table.GetCode(match).ToString() + ",");
                    table.AddCode(nextMatch);
                    match = data[i].ToString();
                }
            }

            compressedImg += (table.GetCode(match).ToString() + ",");

            StreamWriter writer = new StreamWriter(dist);
            writer.WriteLine(compressedImg);
            writer.Close();
        }

        public static void deCompressBmpImage(string source, string dist)
        {
            byte[] dataBytes = FileExtensions.readBytesFromFile(source);
            string dataString = System.Text.Encoding.UTF8.GetString(dataBytes);
            dataString = dataString.Substring(0, dataString.LastIndexOf(","));

            int[] data = dataString.ToIntArray();

            List<string> table = new List<string>();
            List<byte> result = new List<byte>();

            for (int i = 0; i < 256; i++)
            {
                string ch = i.ToString();
                table.Add(ch.ToString());
            }

            int firstCode = data[0];
            string matchChar = firstCode.ToString();
            string match = matchChar.ToString();
            string c = "";
            string nextMatch = "";

            result.Add(byte.Parse(match));

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] < table.Count)
                    nextMatch = table[data[i]];
                else
                {
                    nextMatch = nextMatch + "," + match.Split(",")[0];
                    //nextMatch += "," + c;
                }

                var curItems = nextMatch.Split(",");
                foreach (var item in curItems)
                {
                    result.Add(byte.Parse(item));
                }

                table.Add(match + "," + nextMatch.Split(",")[0]);
                c = match;
                match = nextMatch;
            }

            FileExtensions.writeBytesInFile(dist, result.ToArray(), FileMode.OpenOrCreate);
        }
    }

    public static class StringExtension
    {
        public static int[] ToIntArray(this string str)
        {
            string[] splittedStr = str.Split(",");
            List<int> res = new List<int>();

            foreach (var item in splittedStr)
            {
                res.Add(Int32.Parse(item));
            }

            return res.ToArray();
        }
    }
}
