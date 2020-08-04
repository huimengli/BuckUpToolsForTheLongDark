using readBuckFile.Helpers;
using System;
using System.IO;

namespace readBuckFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\study\git\BuckUpToolsForTheLongDark\readBuckFile\readBuckFile\sandbox5";
            var allFile = File.Create(new FileInfo(path).DirectoryName + "\\all.txt");
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
