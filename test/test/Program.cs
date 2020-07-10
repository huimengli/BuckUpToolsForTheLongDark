using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;
using System.Collections.Generic;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var jsonFile = File.ReadAllText(@"C:\Users\29133\Desktop\任务\BuckUpToolsForTheLongDark\TheLongDarkBackupTools\bin\Debug\bfFolder\sandbox4_bf1",Encoding.Default);

            //Console.WriteLine(jsonFile);

            //Image img = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            //Graphics g = Graphics.FromImage(img);
            //g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            //Clipboard.SetImage(img);
            //img.Save(savePath);

            //Console.WriteLine(g);

            //var p1 = JsonConvert.DeserializeObject(jsonFile);

            //Console.WriteLine(p1);

            var thisPath = Directory.GetCurrentDirectory();

            var path = @"C:\Users\29133\AppData\Local\Hinterland\TheLongDark\sandbox3";

            var file = new FileInfo(path);


            Console.WriteLine(file);

            Console.WriteLine(file.Name);

            Console.WriteLine(file.Directory);

            Console.Read();
        }

        /// <summary>
        /// 获取文件夹中最近修改的文件
        /// </summary>
        /// <param name="floderPath">文件夹路径</param>
        /// <returns></returns>
        public static string GetTheNewFile(string floderPath)
        {
            var ret = "";

            var folder = new DirectoryInfo(floderPath);
            var files = folder.GetFiles();

            var allTimes = new List<double>();

            foreach (var item in files)
            {
                allTimes.Add(item.LastAccessTime.ToFileTimeUtc());
            }

            var index = MaxIndex(allTimes);

            ret = files[index].Name;

            return ret;
        }

        #region 获取最大最小值

        /// <summary>
        /// 获取所有值中最小的
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double Min(params double[] numbers)
        {
            var mix = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (mix > numbers[i])
                {
                    mix = numbers[i];
                }
            }

            return mix;
        }

        /// <summary>
        /// 获取所有值中最小的
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double Min(List<double> numbers)
        {
            var mix = numbers[0];

            for (int i = 1; i < numbers.Count; i++)
            {
                if (mix > numbers[i])
                {
                    mix = numbers[i];
                }
            }

            return mix;
        }

        /// <summary>
        /// 获取所有值中最小的项数
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int MinIndex(params double[] numbers)
        {
            var index = 0;
            var mix = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                if (mix > numbers[i])
                {
                    mix = numbers[i];
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// 获取所有值中最小的项数
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int MinIndex(List<double> numbers)
        {
            var index = 0;
            var mix = numbers[0];

            for (int i = 0; i < numbers.Count; i++)
            {
                if (mix > numbers[i])
                {
                    mix = numbers[i];
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// 获取所有值中最大的
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double Max(params double[] numbers)
        {
            var max = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (max < numbers[i])
                {
                    max = numbers[i];
                }
            }

            return max;
        }

        /// <summary>
        /// 获取所有值中最大的
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double Max(List<double> numbers)
        {
            var max = numbers[0];

            for (int i = 1; i < numbers.Count; i++)
            {
                if (max < numbers[i])
                {
                    max = numbers[i];
                }
            }

            return max;
        }

        /// <summary>
        /// 获取所有值中最小的项数
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int MaxIndex(params double[] numbers)
        {
            var index = 0;
            var max = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                if (max < numbers[i])
                {
                    max = numbers[i];
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// 获取所有值中最小的项数
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int MaxIndex(List<double> numbers)
        {
            var index = 0;
            var max = numbers[0];

            for (int i = 0; i < numbers.Count; i++)
            {
                if (max < numbers[i])
                {
                    max = numbers[i];
                    index = i;
                }
            }

            return index;
        }

        #endregion
    }
}
