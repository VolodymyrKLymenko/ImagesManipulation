using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace bmp_images_manipulation.CommonExtensions
{
    public static class FileExtensions
    {
        public static byte[] readBytesFromFile(string source)
        {
            FileInfo fileInfo = new FileInfo(source);

            byte[] data = new byte[fileInfo.Length];

            using (FileStream imgStream = fileInfo.OpenRead())
            {
                imgStream.Read(data, 0, data.Length);
            }

            return data;
        }

        public static void writeBytesInFile(string dist, byte[] data, FileMode mode, int countToWrite = -1)
        {
            int count = countToWrite != -1 ? countToWrite : data.Length;

            FileStream outputFile = new FileStream(dist, mode, FileAccess.Write);
            outputFile.Write(data, 0, count);
            outputFile.Close();
        }

    }
}
