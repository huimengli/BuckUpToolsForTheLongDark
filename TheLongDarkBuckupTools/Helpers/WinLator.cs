using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TheLongDarkBuckupTools.AddFunc;
using TheLongDarkBuckupTools.Class;

namespace TheLongDarkBuckupTools.Helpers
{
    /// <summary>
    /// WinLator运行此程序需要的相关函数
    /// </summary>
    public static class WinLator
    {
        // 声明 ntdll.dll 中的函数
        [DllImport("ntdll.dll", EntryPoint = "wine_get_version", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern IntPtr wine_get_version();

        /// <summary>
        /// 获取Wine版本
        /// </summary>
        /// <returns></returns>
        private static string GetWineVersion()
        {
            try
            {
                // 调用了 ntdll 里的 wine_get_version 函数
                return Marshal.PtrToStringAnsi(wine_get_version());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 判断是否在Wine下运行
        /// </summary>
        /// <returns></returns>
        public static bool IsRunningOnWine()
        {
            string version = GetWineVersion();
            return !string.IsNullOrEmpty(version);
        }

        /// <summary>
        /// 判断是否在WinLator下运行
        /// </summary>
        public static bool IsRunningOnWinLator
        {
            get
            {
                return IsRunningOnWine();
            }
        }

        /// <summary>
        /// 将Watcher检测到的路径转换为正常的文件路径
        /// 兼容window模式和WinLator模式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string WatcherPathToFilePath(string path)
        {
            string[] pathPart = path.Split('\\');
            ListEX<string> reversePathPart = new ListEX<string>();
            for (int i = pathPart.Length - 1; i >= 0; i--)
            {
                reversePathPart.Add(pathPart[i]);
                if (pathPart[i].EndsWith(":"))
                {
                    break;
                }
            }
            reversePathPart.Reverse();
            return reversePathPart.Join("\\");
        }
    }
}
