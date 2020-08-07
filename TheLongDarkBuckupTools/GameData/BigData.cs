using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace TheLongDarkBuckupTools.GameData
{
    /// <summary>
    /// 转大数据
    /// </summary>
    public class BigData
    {
        #region 数据部分
        /// <summary>
        /// 项
        /// </summary>
        private string title;

        /// <summary>
        /// 值
        /// </summary>
        private string value;

        /// <summary>
        /// 项
        /// </summary>
        public string Title
        {
            set
            {
                title = Base64Encrypt(value);
            }
            get
            {
                return Base64Decrypt(title);
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            set
            {
                this.value = Base64Encrypt(value);
            }
            get
            {
                return Base64Decrypt(value);
            }
        }

        /// <summary>
        /// 真实的项
        /// </summary>
        public string TrueTitle
        {
            get
            {
                return title;
            }
        }

        /// <summary>
        /// 真实的值
        /// </summary>
        public string TrueValue
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// 获取数据的正则表达式
        /// </summary>
        private readonly Regex GetData = new Regex(@"([^:;]*):([^:;]*);");

        #endregion

        #region Base64加密解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 空无构造
        /// </summary>
        public BigData()
        {
            Title = "";
            Value = "";
        }

        /// <summary>
        /// 仅有项的构造
        /// </summary>
        /// <param name="title"></param>
        public BigData(string title)
        {
            Title = title;
            Value = "";
        }

        /// <summary>
        /// 完全构造
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        public BigData(string title, string value)
        {
            Title = title;
            Value = value;
        }

        /// <summary>
        /// 包含自己的构造
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        public BigData(string title, BigData value) : this(title, value.ToString())
        {
        }

        /// <summary>
        /// 包含其他的构造
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        public BigData(string title, object value) : this(title, value.ToString())
        {

        }

        #endregion

        #region 功能模块
        /// <summary>
        /// 重写转化为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var ret = title + ":" + value + ";";
            return ret;
        }

        /// <summary>
        /// 从字符串转化为转大数据
        /// </summary>
        /// <param name="val">数据</param>
        /// <returns></returns>
        public BigData Parse(string val)
        {
            var data = GetData.Match(val);
            if (string.IsNullOrEmpty(data.Groups[0].ToString()))
            {
                return new BigData();
            }
            return new BigData(data.Groups[1].ToString(), data.Groups[2]);
        }

        /// <summary>
        /// 从字符串转化为多个转大数据
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public BigData[] Parses(string val)
        {
            var data = GetData.Matches(val);
            if (data.Count == 0)
            {
                return new BigData[0];
            }
            var ret = new List<BigData>();
            foreach (Match item in data)
            {
                ret.Add(new BigData(item.Groups[1].ToString(), item.Groups[2]));
            }
            return ret.ToArray();
        }

        #endregion
    }

    /// <summary>
    /// 转大数据添加功能
    /// </summary>
    public static class BigDataAdd
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="datas">数据组</param>
        /// <param name="data">添加的数据</param>
        /// <returns></returns>
        public static BigData[] AddData(this BigData[] datas, BigData data)
        {
            var ret = new List<BigData>();
            foreach (var item in datas)
            {
                ret.Add(item);
            }
            ret.Add(data);
            return ret.ToArray();
        }

        /// <summary>
        /// 添加多个数据
        /// </summary>
        /// <param name="datas">元数据</param>
        /// <param name="datasAdd">添加数据</param>
        /// <returns></returns>
        public static BigData[] AddDatas(this BigData[] datas, BigData[] datasAdd)
        {
            var ret = new List<BigData>();
            foreach (var item in datas)
            {
                ret.Add(item);
            }
            foreach (var item in datasAdd)
            {
                ret.Add(item);
            }
            return ret.ToArray();
        }

        /// <summary>
        /// 查找单个数据
        /// </summary>
        /// <param name="datas">数据组</param>
        /// <param name="title">数据标题</param>
        /// <returns></returns>
        public static BigData SearchData(this BigData[] datas, string title)
        {
            BigData ret = null;
            foreach (var item in datas)
            {
                if (item.Title == title)
                {
                    ret = item;
                }
            }
            return ret;
        }

        /// <summary>
        /// 查找多个数据
        /// </summary>
        /// <param name="datas">数据组</param>
        /// <param name="title">数据标题</param>
        /// <returns></returns>
        public static BigData[] SearckDatas(this BigData[] datas, string title)
        {
            var ret = new List<BigData>();
            foreach (var item in datas)
            {
                if (item.Title == title)
                {
                    ret.Add(item);
                }
            }
            return ret.ToArray();
        }

        /// <summary>
        /// 转化为字符串
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static string ToString(this BigData[] datas,bool torf)
        {
            if (torf)
            {
                var ret = "";

                foreach (var item in datas)
                {
                    ret += item.ToString();
                }

                return ret;
            }
            else
            {
                return datas.ToString();
            }
        }

        /// <summary>
        /// 反序列化(不会写)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static T DeserializeObject<T>(this BigData[] datas)where T:class
        {
            if (datas.Length==0)
            {
                return null;
            }
            else
            {
                return (T)new object();
            }
        }

        /// <summary>
        /// 插槽数据转大数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BigData[] ToBigDatas(this SlotData data,FileInfo file)
        {
            var ret = new BigData[7];
            ret[0] = new BigData("m_FileName", file.Name);
            ret[1] = new BigData("m_Name", data.m_Name);
            ret[2] = new BigData("m_DisplayName", data.m_DisplayName);
            ret[3] = new BigData("m_GameMode", data.m_GameMode);
            ret[4] = new BigData("m_VersionChangelistNumber", data.m_VersionChangelistNumber);
            ret[5] = new BigData("m_Timestamp", data.m_Timestamp.ToFileTimeUtc());
            ret[6] = new BigData("m_FileTime", file.LastWriteTime.ToFileTimeUtc());
            return ret;
        }

        /// <summary>
        /// 插槽数据转大数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BigData[] ToBigDatas(this SlotData data,FileInfo file,bool isRead)
        {
            var ret = new BigData[7];
            ret[0] = new BigData("m_FileName", file.Name);
            ret[1] = new BigData("m_Name", data.m_Name);
            ret[2] = new BigData("m_DisplayName", data.m_DisplayName);
            ret[3] = new BigData("m_GameMode", data.m_GameMode);
            ret[4] = new BigData("m_VersionChangelistNumber", data.m_VersionChangelistNumber);
            ret[5] = new BigData("m_Timestamp", data.m_Timestamp.ToFileTimeUtc());
            ret[6] = isRead ? new BigData("m_FileTime", file.LastWriteTime.ToFileTimeUtc()) : new BigData("m_FileTime","无");
            return ret;
        }
    }
}
