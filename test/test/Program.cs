using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.GZip;
using System.Timers;
using System.Threading;

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
        public static string watcherFile;

        public static void Main(string[] args)
        {
            ////如果没有指定目录，则退出程序
            //if (args.Length != 1)
            //{
            //    //显示调用程序的正确方法
            //    Console.WriteLine("usage:Watcher.exe(directory)");
            //    return;
            //}

            watcherFile = Console.ReadLine();

            #region 文件夹监控测试
            //Console.WriteLine(watcherFile);
            ////创建一个新的FileSystemWatcher并设置其属性
            //FileSystemWatcher watcher = new FileSystemWatcher();
            //watcher.Path = watcherFile;
            ///*监视LastAcceSS和LastWrite时间的更改以及文件或目录的重命名*/
            //watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite |
            //       NotifyFilters.FileName | NotifyFilters.DirectoryName;
            ////只监视文本文件
            ////watcher.Filter = "*.txt";
            ////添加事件句柄
            ////当由FileSystemWatcher所指定的路径中的文件或目录的
            ////大小、系统属性、最后写时间、最后访问时间或安全权限
            ////发生更改时，更改事件就会发生
            //watcher.Changed += new FileSystemEventHandler(OnChanged);
            ////由FileSystemWatcher所指定的路径中文件或目录被创建时，创建事件就会发生
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Created += new FileSystemEventHandler(Copy);
            ////当由FileSystemWatcher所指定的路径中文件或目录被删除时，删除事件就会发生
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            ////当由FileSystemWatcher所指定的路径中文件或目录被重命名时，重命名事件就会发生
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);
            ////开始监视
            //watcher.EnableRaisingEvents = true;
            ////等待用户退出程序
            //Console.WriteLine("Press\'q\' to quit the sample.");
            //while (Console.Read() != 'q') ; 
            #endregion

            var file = new FileInfo(watcherFile);

            //ZipFile(watcherFile, file.DirectoryName + "\\2.zip", ZipEnum.BZIP2);
            UnZipFile(watcherFile, file.DirectoryName + "\\2.png", ZipEnum.BZIP2);
        }

        //定义事件处理程序
        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            //指定当文件被更改、创建或删除时要做的事
            Console.WriteLine("file:" + e.FullPath + " " + e.ChangeType);
        }

        public static void OnRenamed(object sender, RenamedEventArgs e)
        {
            //指定当文件被重命名时发生的动作
            Console.WriteLine("Fi]e:{0} renamed to{1}", e.OldFullPath, e.FullPath);
        }

        public static void Copy(object sender ,FileSystemEventArgs e)
        {
            var file = new FileInfo(e.FullPath);
            var time = DateTime.Now.ToFileTimeUtc();
            ZipFile(e.FullPath, watcherFile + "\\test\\"+file.Name+".zip", ZipEnum.GZIP);
            var copy = "copy /b \""+watcherFile+"\\test\\1.png\"+\""+watcherFile+"\\test\\"+file.Name+".zip"+"\" \"" + watcherFile+"\\test\\"+file.Name+ "_bf" + time.ToString() + ".png\"";
            while (true)
            {
                //Console.Write(copy);
                Thread.Sleep(3000);
                //Console.Write(copy);
                UseCmd(copy);
                break;
            }
            //var copy = "copy \"" + e.FullPath + "\" \"" + e.FullPath + "_bf" + time.ToString() + "\"";
            //UseCmd(copy);
            Console.WriteLine("拷贝完成");
        }

        public static void UnZip(object sender,FileSystemEventArgs e)
        {

        }

        /// <summary>
        /// 使用cmd命令
        /// </summary>
        /// <param name="cmdCode"></param>
        public static void UseCmd(string cmdCode)
        {
            System.Diagnostics.Process proIP = new System.Diagnostics.Process();
            proIP.StartInfo.FileName = "cmd.exe";
            proIP.StartInfo.UseShellExecute = false;
            proIP.StartInfo.RedirectStandardInput = true;
            proIP.StartInfo.RedirectStandardOutput = true;
            proIP.StartInfo.RedirectStandardError = true;
            proIP.StartInfo.CreateNoWindow = true;
            proIP.Start();
            proIP.StandardInput.WriteLine(cmdCode);
            proIP.StandardInput.WriteLine("exit");
            string strResult = proIP.StandardOutput.ReadToEnd();
            Console.WriteLine(strResult);
            proIP.Close();
        }

        /// <summary>
        /// 使用cmd命令
        /// </summary>
        /// <param name="cmdCode"></param>
        public static void UseCmd(params string[] cmdCodes)
        {
            System.Diagnostics.Process proIP = new System.Diagnostics.Process();
            proIP.StartInfo.FileName = "cmd.exe";
            proIP.StartInfo.UseShellExecute = false;
            proIP.StartInfo.RedirectStandardInput = true;
            proIP.StartInfo.RedirectStandardOutput = true;
            proIP.StartInfo.RedirectStandardError = true;
            proIP.StartInfo.CreateNoWindow = true;
            proIP.Start();
            foreach (var eachCmd in cmdCodes)
            {
                proIP.StandardInput.WriteLine(eachCmd);
            }
            proIP.StandardInput.WriteLine("exit");
            string strResult = proIP.StandardOutput.ReadToEnd();
            Console.WriteLine(strResult);
            proIP.Close();
        }

        #region 压缩工具

        /// <summary>  
        /// 压缩枚举  
        /// </summary>  
        public enum ZipEnum
        {
            /// <summary>
            /// 压缩时间长，压缩率高
            /// </summary>
            BZIP2,

            /// <summary>
            /// 压缩效率高，压缩率低
            /// </summary>
            GZIP
        }

        /// <summary>  
        /// 制作压缩包（单个文件压缩）  
        /// </summary>  
        /// <param name="srcFileName">原文件</param>  
        /// <param name="zipFileName">压缩文件</param>  
        /// <param name="zipEnum">压缩算法枚举</param>  
        /// <returns>压缩成功标志</returns>  
        public static bool ZipFile(string srcFileName, string zipFileName, ZipEnum zipEnum)
        {
            bool flag = true;
            try
            {
                switch (zipEnum)
                {
                    case ZipEnum.BZIP2:

                        FileStream inStream = File.OpenRead(srcFileName);
                        FileStream outStream = File.Open(zipFileName, FileMode.Create);

                        //参数true表示压缩完成后，inStream和outStream连接都释放  
                        BZip2.Compress(inStream, outStream, true, 4096);

                        inStream.Close();
                        outStream.Close();
                        
                        break;
                    case ZipEnum.GZIP:

                        FileStream srcFile = File.OpenRead(srcFileName);

                        GZipOutputStream zipFile = new GZipOutputStream(File.Open(zipFileName, FileMode.Create));

                        byte[] fileData = new byte[srcFile.Length];
                        srcFile.Read(fileData, 0, (int)srcFile.Length);
                        zipFile.Write(fileData, 0, fileData.Length);

                        srcFile.Close();
                        zipFile.Close();

                        break;
                    default: break;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static bool UnZipFile( string zipFileName, string srcFileName, ZipEnum zipEnum)
        {
            var ret = true;

            try
            {
                switch (zipEnum)
                {
                    case ZipEnum.BZIP2:

                        var inFile = File.OpenRead(zipFileName);
                        var outFile = File.Open(srcFileName, FileMode.Create);

                        BZip2.Decompress(inFile, outFile, true);

                        inFile.Close();
                        outFile.Close();

                        break;
                    case ZipEnum.GZIP:
                        
                        var srcFile = File.Open(srcFileName, FileMode.Create);
                        var zipFile = new GZipInputStream(File.OpenRead(zipFileName));

                        int buffersize = 2048;//设置缓冲区大小
                        byte[] fileData = new byte[buffersize];//创建缓冲数据

                        while (buffersize>0)//一直读取到文件末尾
                        {
                            buffersize = zipFile.Read(fileData, 0, buffersize);
                            srcFile.Write(fileData, 0, buffersize);//写入目标文件
                        }                        


                        srcFile.Close();
                        zipFile.Close();

                        break;
                    default:break;
                }
            }
            catch (Exception)
            {
                ret = false;
                throw;
            }

            return ret;
        }

        #endregion

    }
}
