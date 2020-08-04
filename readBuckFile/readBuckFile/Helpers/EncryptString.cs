using System;
using System.Collections.Generic;
using System.Text;

namespace readBuckFile.Helpers
{
    /// <summary>
    /// 编码转换
    /// </summary>
    class EncryptString
    {
        /// <summary>
        /// 将字符串压缩为字节
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static byte[] CompressStringToBytes(string toEncrypt)
        {
            return CLZF.Compress(Encoding.UTF8.GetBytes(toEncrypt));
        }

        /// <summary>
        /// 将字节解压缩为字符串
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public static string DecompressBytesToString(byte[] toDecrypt)
        {
            try
            {
                return Encoding.UTF8.GetString(CLZF.Decompress(toDecrypt));
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
