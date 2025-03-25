using TheLongDarkBuckupTools.Helpers;
using TheLongDarkBuckupTools.AddFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLongDarkBuckupTools.Class
{
    /// <summary>
    /// 增强字典类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DictionaryEX<TKey, TValue> : Dictionary<TKey, TValue>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DictionaryEX() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dict"></param>
        public DictionaryEX(Dictionary<TKey, TValue> dict)
        {
            foreach (var item in dict)
            {
                this.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        public DictionaryEX(TKey[] keys, TValue[] values)
        {
            for (int i = 0; i < Math.Min(keys.Length, values.Length); i++)
            {
                this.Add(keys[i], values[i]);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        public DictionaryEX(List<TKey> keys, List<TValue> values) : this(keys.ToArray(), values.ToArray())
        {

        }

        /// <summary>
        /// 获取或设置与指定的键关联的值
        /// </summary>
        /// <param name="key">要获取或设置的值的键。</param>
        /// <returns>与指定的键相关联的值。 如果指定键未找到，则 Get 操作引发 System.Collections.Generic.KeyNotFoundException，而 Set 操作创建一个带指定键的新元素。</returns>
        /// <exception cref="System.ArgumentNullException">key 为 null</exception>
        public new TValue this[TKey key]
        {
            set
            {

            }
            get
            {
                try
                {
                    return base[key];
                }
                catch (KeyNotFoundException)
                {
                    return default(TValue);
                }
            }
        }

        #region 重写ToString
        /// <summary>
        /// 默认的 ToString 方法，返回字典的字符串表示。
        /// </summary>
        /// <returns>字典的字符串表示。</returns>
        public override string ToString()
        {
            return ToString(0, true, typeof(Dictionary<TKey, TValue>));
        }

        /// <summary>
        /// 带有缩进的 ToString 方法，根据给定的缩进值返回字典的字符串表示。
        /// </summary>
        /// <param name="indentation">缩进的空格数。</param>
        /// <returns>字典的字符串表示。</returns>
        public string ToString(int indentation)
        {
            return ToString(indentation, true, typeof(Dictionary<TKey, TValue>));
        }

        /// <summary>
        /// 带有缩进和简洁名称选项的 ToString 方法，返回字典的字符串表示。
        /// </summary>
        /// <param name="indentation">缩进的空格数。</param>
        /// <param name="isSimple">是否使用简洁类名（默认为true）。</param>
        /// <returns>字典的字符串表示。</returns>
        public string ToString(int indentation, bool isSimple)
        {
            return ToString(indentation, isSimple, typeof(Dictionary<TKey, TValue>));
        }

        /// <summary>
        /// 带有缩进和自定义类型名称的 ToString 方法，返回字典的字符串表示。
        /// </summary>
        /// <param name="indentation">缩进的空格数。</param>
        /// <param name="dictionaryName">字典的自定义名称。</param>
        /// <returns>字典的字符串表示。</returns>
        public string ToString(int indentation, string dictionaryName)
        {
            return ToString(indentation, true, dictionaryName);
        }

        /// <summary>
        /// 带有缩进、简洁名称和类型的 ToString 方法，返回字典的字符串表示。
        /// </summary>
        /// <param name="indentation">缩进的空格数。</param>
        /// <param name="isSimple">是否使用简洁类名（默认为true）。</param>
        /// <param name="type">字典类型。</param>
        /// <returns>字典的字符串表示。</returns>
        public string ToString(int indentation, bool isSimple, Type type)
        {
            string className = isSimple ? type.Name : type.FullName;
            return ToString(indentation, isSimple, className);
        }

        /// <summary>
        /// 核心的 ToString 方法，格式化字典的字符串表示。
        /// </summary>
        /// <param name="indentation">缩进的空格数。</param>
        /// <param name="isSimple">是否使用简洁类名（默认为true）。</param>
        /// <param name="dictionaryName">字典的自定义名称。</param>
        /// <returns>字典的字符串表示。</returns>
        public string ToString(int indentation, bool isSimple, string dictionaryName)
        {
            if (string.IsNullOrEmpty(dictionaryName))
            {
                dictionaryName = isSimple ? typeof(Dictionary<TKey, TValue>).Name : typeof(Dictionary<TKey, TValue>).FullName;
            }

            var ret = new StringBuilder();
            string indent = new string(' ', indentation);

            // 添加字典的类型名称
            ret.Append(dictionaryName);
            ret.Append("<");

            // 处理字典为空的情况
            if (this.Count == 0)
            {
                ret.Append("Object, Object");
            }
            else
            {
                // 获取字典中的第一个键值对的类型
                var firstKey = this.Keys.GetEnumerator();
                firstKey.MoveNext();
                var firstValue = this[firstKey.Current];

                ret.Append(isSimple ? firstKey.Current.GetType().Name : firstKey.Current.GetType().FullName);
                ret.Append(", ");
                ret.Append(isSimple ? firstValue.GetType().Name : firstValue.GetType().FullName);
            }

            ret.Append(">");
            ret.Append(" {");
            ret.Append("\n");

            // 遍历字典，格式化每个键值对
            foreach (var kvp in this)
            {
                ret.Append(indent);
                ret.Append(new string(' ', 4));

                ret.Append(kvp.Key != null ? kvp.Key.ToString() : "null");
                ret.Append(" : ");
                ret.Append(kvp.Value != null ? FormatValue(kvp.Value, indentation + 4, isSimple) : "null");

                ret.Append("\n");
            }

            ret.Append(indent);
            ret.Append("}");

            return ret.ToString();
        }

        /// <summary>
        /// 格式化字典中的值，根据不同类型的值返回相应的字符串表示。
        /// </summary>
        /// <param name="value">字典中的值。</param>
        /// <param name="indentation">缩进的空格数。</param>
        /// <param name="isSimple">是否使用简洁类名（默认为true）。</param>
        /// <returns>格式化后的值的字符串表示。</returns>
        private string FormatValue(object value, int indentation, bool isSimple)
        {
            if (value == null) return "null";

            // 处理不同类型的值
            if (value is DictionaryEX<object, object> dict)
            {
                return dict.ToString(indentation, isSimple, typeof(DictionaryEX<object, object>).FullName);
            }
            else if (value is DateTime dateTime)
            {
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (value is byte b)
            {
                return $"0x{b:X2}";
            }
            else if (value is IEnumerable<object> collection)
            {
                return string.Join(", ", collection);
            }
            else
            {
                return value.ToString();
            }
        }
        #endregion
    }

    /// <summary>
    /// 本地化存储追加类
    /// </summary>
    public static class DictionaryEXAdd
    {
        #region 保存

        /// <summary>
        /// 重写转为字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ToSave(this DictionaryEX<string, bool> dict)
        {
            var ret = new StringBuilder();
            ret.Append("{");
            foreach (var item in dict)
            {
                ret.Append(Base64.Encode(item.Key));
                ret.Append(':');
                ret.Append(item.Value ? 1 : 0);
                ret.Append(",");
            }
            ret.Append("}");
            return ret.ToString();
        }

        /// <summary>
        /// 重写转为字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ToSave(this DictionaryEX<string, int> dict)
        {
            var ret = new StringBuilder();
            ret.Append("{");
            foreach (var item in dict)
            {
                ret.Append(Base64.Encode(item.Key));
                ret.Append(':');
                ret.Append(item.Value);
                ret.Append(",");
            }
            ret.Append("}");
            return ret.ToString();
        }

        /// <summary>
        /// 重写转为字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ToSave(this DictionaryEX<string, string> dict)
        {
            var ret = new StringBuilder();
            ret.Append("{");
            foreach (var item in dict)
            {
                ret.Append(Base64.Encode(item.Key));
                ret.Append(':');
                ret.Append(Base64.Encode(item.Value));
                ret.Append(",");
            }
            ret.Append("}");
            return ret.ToString();
        }

        /// <summary>
        /// 重写转为字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ToSave(this Dictionary<string, bool> dict)
        {
            var ret = new StringBuilder();
            ret.Append("{");
            foreach (var item in dict)
            {
                ret.Append(Base64.Encode(item.Key));
                ret.Append(':');
                ret.Append(item.Value ? 1 : 0);
                ret.Append(",");
            }
            ret.Append("}");
            return ret.ToString();
        }

        /// <summary>
        /// 重写转为字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ToSave(this Dictionary<string, int> dict)
        {
            var ret = new StringBuilder();
            ret.Append("{");
            foreach (var item in dict)
            {
                ret.Append(Base64.Encode(item.Key));
                ret.Append(':');
                ret.Append(item.Value);
                ret.Append(",");
            }
            ret.Append("}");
            return ret.ToString();
        }

        /// <summary>
        /// 重写转为字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ToSave(this Dictionary<string, string> dict)
        {
            var ret = new StringBuilder();
            ret.Append("{");
            foreach (var item in dict)
            {
                ret.Append(Base64.Encode(item.Key));
                ret.Append(':');
                ret.Append(Base64.Encode(item.Value));
                ret.Append(",");
            }
            ret.Append("}");
            return ret.ToString();
        }

        #endregion

        #region 读取
        /// <summary>
        /// 读取字典格式数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Dictionary<string, bool> FromBoolDict(this string str)
        {
            var ret = new Dictionary<string, bool>();
            str = str.Replace("{", "").Replace("}", "");
            var values = str.Split(',');
            string[] value;
            ; string key;
            bool val;

            for (int i = 0; i < values.Length; i++)
            {
                value = values[i].Split(':');
                if (value.Length == 1)
                {
                    continue;
                }
                key = Base64.Decode(value[0]);
                val = value[1] == "1" ? true : false;
                ret.Add(key, val);
            }

            return ret;
        }

        /// <summary>
        /// 读取字典格式数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Dictionary<string, int> FromIntDict(this string str)
        {
            var ret = new Dictionary<string, int>();
            str = str.Replace("{", "").Replace("}", "");
            var values = str.Split(',');
            string[] value;
            string key;
            int val;

            for (int i = 0; i < values.Length; i++)
            {
                value = values[i].Split(':');
                if (value.Length == 1)
                {
                    continue;
                }
                key = Base64.Decode(value[0]);
                val = int.Parse(value[1]);
                ret.Add(key, val);
            }

            return ret;
        }

        /// <summary>
        /// 读取字典格式数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Dictionary<string, string> FromStringDict(this string str)
        {
            var ret = new Dictionary<string, string>();
            str = str.Replace("{", "").Replace("}", "");
            var values = str.Split(',');
            string[] value;
            string key;
            string val;

            for (int i = 0; i < values.Length; i++)
            {
                value = values[i].Split(':');
                if (value.Length == 1)
                {
                    continue;
                }
                key = Base64.Decode(value[0]);
                val = Base64.Decode(value[1]);
                ret.Add(key, val);
            }

            return ret;
        }

        /// <summary>
        /// 读取字典格式数据
        /// 转换成增强格式字典
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DictionaryEX<string, bool> FromBoolDictEX(this string str)
        {
            var ret = new DictionaryEX<string, bool>();
            str = str.Replace("{", "").Replace("}", "");
            var values = str.Split(',');
            string[] value;
            ; string key;
            bool val;

            for (int i = 0; i < values.Length; i++)
            {
                value = values[i].Split(':');
                if (value.Length == 1)
                {
                    continue;
                }
                key = Base64.Decode(value[0]);
                val = value[1] == "1" ? true : false;
                ret.Add(key, val);
            }

            return ret;
        }

        /// <summary>
        /// 读取字典格式数据
        /// 转换成增强格式字典
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DictionaryEX<string, int> FromIntDictEX(this string str)
        {
            var ret = new DictionaryEX<string, int>();
            str = str.Replace("{", "").Replace("}", "");
            var values = str.Split(',');
            string[] value;
            string key;
            int val;

            for (int i = 0; i < values.Length; i++)
            {
                value = values[i].Split(':');
                if (value.Length == 1)
                {
                    continue;
                }
                key = Base64.Decode(value[0]);
                val = int.Parse(value[1]);
                ret.Add(key, val);
            }

            return ret;
        }

        /// <summary>
        /// 读取字典格式数据
        /// 转换成增强格式字典
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DictionaryEX<string, string> FromStringDictEX(this string str)
        {
            var ret = new DictionaryEX<string, string>();
            str = str.Replace("{", "").Replace("}", "");
            var values = str.Split(',');
            string[] value;
            string key;
            string val;

            for (int i = 0; i < values.Length; i++)
            {
                value = values[i].Split(':');
                if (value.Length == 1)
                {
                    continue;
                }
                key = Base64.Decode(value[0]);
                val = Base64.Decode(value[1]);
                ret.Add(key, val);
            }

            return ret;
        }

        #endregion

        #region 重载ToString

        /// <summary>
        /// 重载ToString
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="tf"></param>
        /// <returns></returns>
        public static string ToString<TKey, TValue>(this Dictionary<TKey, TValue> dict, bool tf)
        {
            var ret = new StringBuilder();
            ret.Append("Dictionary<");
            ret.Append(typeof(TKey));
            ret.Append(", ");
            ret.Append(typeof(TValue));
            ret.Append(">(");
            ret.Append(dict.Count);
            ret.Append(") { ");
            foreach (var item in dict)
            {
                ret.Append("{ ");
                ret.Append("\"");
                ret.Append(item.Key.ToString());
                ret.Append("\", \"");
                ret.Append(item.Value.ToString());
                ret.Append("\" }, ");
            }
            ret.Append("}");
            return ret.ToString();
        }

        #endregion

        #region ToDictionaryEX
        /// <summary>
        /// 添加了转为增强型字典的函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="ts"></param>
        /// <param name="getKey"></param>
        /// <returns></returns>
        public static DictionaryEX<R, T> ToDictionaryEX<T, R>(this List<T> ts, Func<T, R> getKey)
        {
            var ret = new DictionaryEX<R, T>();

            ts.ForEach(t =>
            {
                ret.Add(getKey.Invoke(t), t);
            });

            return ret;
        }

        /// <summary>
        /// 添加了转为增强型字典的函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="ts"></param>
        /// <param name="getKey"></param>
        /// <returns></returns>
        public static DictionaryEX<R, T> ToDictionaryEX<T, R>(this List<T> ts, Func<T, int, R> getKey)
        {
            var ret = new DictionaryEX<R, T>();

            ts.ForEach((t, i) =>
            {
                ret.Add(getKey(t, i), t);
            });

            return ret;
        }
        #endregion

        #region 通用遍历功能
        /// <summary>
        /// 将字典中每个键值对进行遍历操作
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="action"></param>
        public static void ForEach<TKey, TValue>(
            this DictionaryEX<TKey, TValue> dict,
            Action<KeyValuePair<TKey, TValue>> action
        )
        {
            foreach (var item in dict)
            {
                action.Invoke(item);
            }
        }

        /// <summary>
        /// 将字典中的每个键值对进行遍历操作
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="action">操作方法</param>
        public static void ForEach<TKey, TValue>(
            this DictionaryEX<TKey, TValue> dict,
            Action<TKey, TValue> action
        )
        {
            foreach (var item in dict)
            {
                action.Invoke(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 将字典中的每个键值对进行遍历操作
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="action"></param>
        public static void ForEach<TKey, TValue>(
            this DictionaryEX<TKey, TValue> dict,
            Action<TKey, TValue, int> action
        )
        {
            var index = 0;
            foreach (var item in dict)
            {
                action.Invoke(item.Key, item.Value, index++);
            }
        }

        #endregion

        #region 遍历功能(KeyValuePair)

        ///// <summary>
        ///// 将字典中的每个键值对进行遍历操作
        ///// </summary>
        ///// <typeparam name="TKey"></typeparam>
        ///// <typeparam name="TValue"></typeparam>
        ///// <param name="dict"></param>
        ///// <param name="action"></param>
        //public static void ForEach<TKey,TValue>(
        //    this DictionaryEX<TKey,TValue> dict,
        //    Action<KeyValuePair<TKey,TValue>> action
        //){
        //    foreach (var item in dict)
        //    {
        //        action.Invoke(item);
        //    }
        //}

        /// <summary>
        /// 将字典中的每个键对值进行操作并转换
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="RKey"></typeparam>
        /// <typeparam name="RValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="func">操作方法</param>
        /// <returns></returns>
        public static DictionaryEX<RKey, RValue> Map<TKey, TValue, RKey, RValue>(
            this DictionaryEX<TKey, TValue> dict,
            Func<KeyValuePair<TKey, TValue>, KeyValuePair<RKey, RValue>> func
        )
        {
            var ret = new DictionaryEX<RKey, RValue>();
            dict.ForEach(item =>
            {
                var itemRet = func(item);
                ret.Add(itemRet.Key, itemRet.Value);
            });
            return ret;
        }

        /// <summary>
        /// 将字典中的每个键对值进行操作并转换
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="RKey"></typeparam>
        /// <typeparam name="RValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="func">操作方法</param>
        /// <returns></returns>
        public static DictionaryEX<RKey, RValue> Map<TKey, TValue, RKey, RValue>(
            this DictionaryEX<TKey, TValue> dict,
            Func<TKey, TValue, KeyValuePair<RKey, RValue>> func
        )
        {
            var ret = new DictionaryEX<RKey, RValue>();
            dict.ForEach((key, value) =>
            {
                var itemRet = func(key, value);
                ret.Add(itemRet.Key, itemRet.Value);
            });
            return ret;
        }

        /// <summary>
        /// 将字典中的每个键对值进行操作并转换
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="RKey"></typeparam>
        /// <typeparam name="RValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="func">操作方法</param>
        /// <returns></returns>
        public static DictionaryEX<RKey, RValue> Map<TKey, TValue, RKey, RValue>(
            this DictionaryEX<TKey, TValue> dict,
            Func<TKey, TValue, int, KeyValuePair<RKey, RValue>> func
        )
        {
            var ret = new DictionaryEX<RKey, RValue>();
            dict.ForEach((key, value, index) =>
            {
                var itemRet = func(key, value, index);
                ret.Add(itemRet.Key, itemRet.Value);
            });
            return ret;
        }
        #endregion
        
    }
}
