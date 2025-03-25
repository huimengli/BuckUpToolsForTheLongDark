using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLongDarkBuckupTools.Helpers
{
    /// <summary>
    /// 编码加密方式
    /// </summary>
    public enum CodingMode
    {
        /// <summary>
        /// 没有加密
        /// </summary>
        NoCoding,

        /// <summary>
        /// SHA256加密
        /// </summary>
        SHA256,

        /// <summary>
        /// 双层MD5加密
        /// </summary>
        MD5,
    }

    /// <summary>
    /// base64类
    /// </summary>
    public class Base64
    {
        /// <summary>
        /// base64原始密匙
        /// </summary>
        public static string _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

        /// <summary>
        /// 将输入内容以utf-8编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string _utf8_encode(string input)
        {
            //input = input.Replace("\r\n", "\n");
            var ret = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                var c = (int)input[i];
                if (c < 128)
                {
                    ret.Append((char)c);
                }
                else if (c > 127 && c < 2048)
                {
                    ret.Append((char)((c >> 6) | 192));
                    ret.Append((char)((c & 63) | 128));
                }
                else
                {
                    ret.Append((char)((c >> 12) | 224));
                    ret.Append((char)(((c >> 6) & 63) | 128));
                    ret.Append((char)((c & 63) | 128));
                }
            }
            return ret.ToString();
        }

        /// <summary>
        /// 将utf8编码的内容解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string _utf8_decode(string input)
        {
            var ret = new StringBuilder();
            var i = 0;
            int c = 0, c2 = 0, c3 = 0;
            while (i < input.Length)
            {
                c = (int)input[i++];
                if (c < 128)
                {
                    ret.Append((char)c);
                }
                else if (c > 191 && c < 224)
                {
                    c2 = (int)input[i++];
                    ret.Append((char)(((c & 31) << 6) | (c2 & 63)));
                }
                else
                {
                    c2 = (int)input[i++];
                    c3 = (int)input[i++];
                    ret.Append((char)(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63)));
                }
            }
            return ret.ToString();
        }

        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Encode(string input)
        {
            var ret = new StringBuilder();
            int? chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            var i = 0;
            input = _utf8_encode(input);
            while (i < input.Length)
            {
                chr1 = (int)input[i++];
                chr2 = input.GetChar(i++);
                chr3 = input.GetChar(i++);
                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2.OR(0) >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3.OR(0) >> 6);
                enc4 = chr3 & 63;
                if (chr2 == null)
                {
                    enc3 = enc4 = 64;
                }
                else if (chr3 == null)
                {
                    enc4 = 64;
                }
                ret.Append(_keyStr[enc1.Value]);
                ret.Append(_keyStr[enc2.Value]);
                ret.Append(_keyStr[enc3.Value]);
                ret.Append(_keyStr[enc4.Value]);
            }
            return ret.ToString();
        }

        /// <summary>
        /// base64解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Decode(string input)
        {
            var ret = new StringBuilder();
            int? chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            var i = 0;
            while (i < input.Length)
            {
                enc1 = _keyStr.IndexOf(input[i++]);
                enc2 = _keyStr.IndexOf(input[i++]);
                enc3 = _keyStr.IndexOf(input[i++]);
                enc4 = _keyStr.IndexOf(input[i++]);
                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;
                ret.Append((char)chr1.Value);
                if (enc3 != 64)
                {
                    ret.Append((char)chr2.Value);
                }
                if (enc4 != 64)
                {
                    ret.Append((char)chr3.Value);
                }
            }
            return _utf8_decode(ret.ToString());
        }

        /// <summary>
        /// 获取随机密匙
        /// </summary>
        /// <returns></returns>
        public static string RandomKey()
        {
            var key = new List<char>(_keyStr.ToArray<char>());
            var ret = new StringBuilder();
            var count = key.Count;
            for (int i = 0; i < count; i++)
            {
                ret.Append(key.GetRandomOne(true));
            }
            return ret.ToString();
        }

        /// <summary>
        /// base64编码
        /// 使用特殊密匙
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encode(string input, string key)
        {
            var ret = new StringBuilder();
            int? chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            var i = 0;
            input = _utf8_encode(input);
            while (i < input.Length)
            {
                chr1 = (int)input[i++];
                chr2 = input.GetChar(i++);
                chr3 = input.GetChar(i++);
                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2.OR(0) >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3.OR(0) >> 6);
                enc4 = chr3 & 63;
                if (chr2 == null)
                {
                    enc3 = enc4 = 64;
                }
                else if (chr3 == null)
                {
                    enc4 = 64;
                }
                ret.Append(key[enc1.Value]);
                ret.Append(key[enc2.Value]);
                ret.Append(key[enc3.Value]);
                ret.Append(key[enc4.Value]);
            }
            return ret.ToString();
        }

        /// <summary>
        /// base64解码
        /// 使用特殊密匙
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decode(string input, string key)
        {
            var ret = new StringBuilder();
            int? chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            var i = 0;
            while (i < input.Length)
            {
                enc1 = key.IndexOf(input[i++]);
                enc2 = key.IndexOf(input[i++]);
                enc3 = key.IndexOf(input[i++]);
                enc4 = key.IndexOf(input[i++]);
                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;
                ret.Append((char)chr1.Value);
                if (enc3 != 64)
                {
                    ret.Append((char)chr2.Value);
                }
                if (enc4 != 64)
                {
                    ret.Append((char)chr3.Value);
                }
            }
            return _utf8_decode(ret.ToString());
        }

        /// <summary>
        /// 获取密匙
        /// </summary>
        /// <returns></returns>
        public static string GetKey()
        {
            return RandomKey();
        }

        /// <summary>
        /// 获取密匙
        /// (默认SHA256)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetKey(string input)
        {
            return GetKey(input, CodingMode.SHA256);
        }

        /// <summary>
        /// 获取密匙
        /// </summary>
        /// <param name="input"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string GetKey(string input, CodingMode mode)
        {
            switch (mode)
            {
                case CodingMode.NoCoding:
                    return _keyStr;
                case CodingMode.SHA256:
                    input = Item.SHA256(input);
                    var temp = input;
                    var keys = "ABCDEF0123456789";
                    var count = 0;
                    var keyss = new List<int>();
                    var indexs = new Dictionary<string, int>();
                    for (int i = 0; i < keys.Length; i++)
                    {
                        var x = keys[i].ToString();
                        count = temp.Length;
                        temp = temp.Replace(x, "");
                        keyss.Add(count - temp.Length);
                        indexs.Add(x, 0);
                    }
                    var key = new Dictionary<string, string>();
                    var key2 = _keyStr.Substring(0, 64);
                    for (int i = 0; i < keyss.Count; i++)
                    {
                        var x = key2.Substring(0, keyss[i]);
                        if (x.Length == 0)
                        {
                            continue;
                        }
                        key2 = key2.Replace(x, "");
                        key.Add(keys[i].ToString(), x);
                    }
                    var ret = new StringBuilder();
                    for (int i = 0; i < input.Length; i++)
                    {
                        var x = input[i].ToString();
                        ret.Append(key[x][indexs[x]]);
                        indexs[x]++;
                    }
                    ret.Append('=');
                    return ret.ToString();
                case CodingMode.MD5:
                    throw new Exception("这里我准备使用双层MD5编码\n不过内容尚未完成,等以后再写");
                default:
                    throw new Exception("没有这种编码模式!");
            }
        }


    }

    /// <summary>
    /// base64类追加函数
    /// </summary>
    public static class Base64Add
    {
        /// <summary>
        /// 获取一个字符对象编码值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index">指针</param>
        /// <returns></returns>
        public static int? GetChar(this string str, int index)
        {
            index = index < 0 ? str.Length - index : index;
            if (index >= str.Length || index < 0)
            {
                return null;
            }
            else
            {
                return (int)str[index];
            }
        }

        /// <summary>
        /// 或者
        /// </summary>
        /// <param name="num"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int OR(this int? num, int defaultValue)
        {
            if (num == null)
            {
                return defaultValue;
            }
            else
            {
                return num.Value;
            }
        }

        /// <summary>
        /// 随机取出一个对象
        /// </summary>
        /// <param name="list"></param>
        /// <param name="isDelete">是否删除</param>
        /// <returns></returns>
        public static T GetRandomOne<T>(this List<T> list, bool isDelete)
        {
            var r = new Random();
            T ret = list[r.Next(list.Count)];
            if (isDelete)
            {
                list.Remove(ret);
            }
            return ret;
        }

        /// <summary>
        /// 随机取出一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T GetRandomOne<T>(this List<T> list)
        {
            return list.GetRandomOne<T>(false);
        }
    }
}
