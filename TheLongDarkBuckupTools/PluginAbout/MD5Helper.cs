using System;

namespace TheLongDarkBuckupTools.PluginAbout
{
    public class MD5Helper
    {
        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetMD5HashFromFile(string filePath)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            using (var stream = System.IO.File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
            }
        }
    }
}