using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLongDarkBuckupTools.Helpers;

namespace TheLongDarkBuckupTools.PluginAbout
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// 内嵌的BepInEx.zip文件
        /// </summary>
        public static string InnerZip = @"PluginAbout.BepInEx_win_x64.zip";

        /// <summary>
        /// 内嵌的插件文件
        /// </summary>
        public static string InnerPlugin = @"PluginAbout.SavePlugin.dll";

        /// <summary>
        /// BepInEx文件列表:MD5值
        /// </summary>
        private static Dictionary<string, string> _bepInExFiles = new Dictionary<string, string>
        {
            { "BepInEx_win_x64.zip", "75F49185BE471B886000320873B4E18A" },
        };

        /// <summary>
        /// 插件文件列表:MD5值
        /// </summary>
        private static Dictionary<string,string> _pluginFiles = new Dictionary<string, string>
        {
            { "SavePlugin.dll", "6C48ACBEF88B8A3964C3CD72B61596E5" },
        };

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public static string Test()
        {
            string testPath = "D:\\git\\BuckUpToolsForTheLongDark\\SavePlugin\\dist\\SavePlugin.dll";
            string fileMd5 = MD5Helper.GetMD5HashFromFile(testPath);
            return $"文件签名: {fileMd5}, 插件签名: {_pluginFiles["SavePlugin.dll"]}, 是否一致: {fileMd5 == _pluginFiles["SavePlugin.dll"]}";
        }

        /// <summary>
        /// 检查BepInEx文件MD5
        /// 默认在BepInEx文件列表中的文件,才会进行检测
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool CheckFileMd5(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                string fileMd5 = MD5Helper.GetMD5HashFromFile(filePath);
                string md5 = _bepInExFiles[file.Name];
                return fileMd5 == md5;
            }
            {
                return false;
            }
        }

        /// <summary>
        /// 检查目录种BepInEx是否存在
        /// (这个方法感觉没用)
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static bool CheckBepInExExist(string rootPath)
        {
            return false;
        }

        /// <summary>
        /// 部署BepInEx到根目录
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static bool DeployBepInEx(string rootPath)
        {
            try
            {
                return Extractor.ExtractEmbeddedZip(InnerZip, rootPath);
            }
            catch (Exception)
            {
#if DEBUG
                throw;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 部署插件到根目录
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static bool DeployPlugin(string rootPath)
        {
            try
            {
                return Extractor.ExtractFile(
                    InnerPlugin, 
                    Path.Combine(rootPath,"plugins\\SavePlugin.dll")
                );
            }
            catch (Exception)
            {
#if DEBUG
                throw;
#else
                return false;
#endif
            }
        }
    }
}
