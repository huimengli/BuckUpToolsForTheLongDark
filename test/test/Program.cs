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
        //static void Main(string[] args)
        //{
            

        //    Console.Read();
        //}

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

    public class watcher
    {
        public static void Main(string[] args)
        {
            //如果没有指定目录，则退出程序
            if (args.Length != 1)
            {
                //显示调用程序的正确方法
                Console.WriteLine("usage:Watcher.exe(directory)");
                return;
            }
            //创建一个新的FileSystemWatcher并设置其属性
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = args[0];
            /*监视LastAcceSS和LastWrite时间的更改以及文件或目录的重命名*/
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                   NotifyFilters.FileName | NotifyFilters.DirectoryName;
            //只监视文本文件
            watcher.Filter = "*.txt";
            //添加事件句柄
            //当由FileSystemWatcher所指定的路径中的文件或目录的
            //大小、系统属性、最后写时间、最后访问时间或安全权限
            //发生更改时，更改事件就会发生
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //由FileSystemWatcher所指定的路径中文件或目录被创建时，创建事件就会发生
            watcher.Created += new FileSystemEventHandler(OnChanged);
            //当由FileSystemWatcher所指定的路径中文件或目录被删除时，删除事件就会发生
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //当由FileSystemWatcher所指定的路径中文件或目录被重命名时，重命名事件就会发生
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            //开始监视
            watcher.EnableRaisingEvents = true;
            //等待用户退出程序
            Console.WriteLine("Press\'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }
        //定义事件处理程序
        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            //指定当文件被更改、创建或删除时要做的事
            Console.WriteLine("file:" + e.FullPath + "" + e.ChangeType);
        }
        public static void OnRenamed(object sender, RenamedEventArgs e)
        {
            //指定当文件被重命名时发生的动作
            Console.WriteLine("Fi]e:{0} renamed to{1}", e.OldFullPath, e.FullPath);
        }
    }
}
