using readBuckFile.Helpers;
using System;
using System.IO;

namespace readBuckFile
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"E:\study\git\BuckUpToolsForTheLongDark\readBuckFile\readBuckFile\sandbox1";
            string path = AppDomain.CurrentDomain.BaseDirectory;
            var paths = path.Split('\\');
            path = "";
            for (int i = 0; i < paths.Length-3; i++)
            {
                path += paths[i];
                path += "\\";
            }
            path += "readBuckFile\\sandbox1";
            var allFile = File.Create(new FileInfo(path).DirectoryName + "\\all2.txt");
            byte[] all = File.ReadAllBytes(path);
            //Console.WriteLine(BitConverter.ToString(all));
            var all2 = EncryptString.DecompressBytesToString(all);
            //Console.WriteLine(all2);
            StreamWriter sr = new StreamWriter(allFile);
            sr.WriteLine(all2);
            sr.Close();
            allFile.Close();
            Console.Read();
        }
    }
}
