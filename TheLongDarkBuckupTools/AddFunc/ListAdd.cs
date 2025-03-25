using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLongDarkBuckupTools.AddFunc
{
    /// <summary>
    /// 列表类型追加功能
    /// </summary>
    public static class ListAdd
    {
        /// <summary>
        /// 将列表转为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects"></param>
        /// <param name="tf"></param>
        /// <returns></returns>
        public static string ToString<T>(this T[] objects, bool tf)
        {
            if (tf)
            {
                var ret = typeof(T).ToString();
                ret += "[" + objects.Length + "] { ";
                for (int i = 0; i < objects.Length; i++)
                {
                    ret += objects[i].ToString();
                    if (i < objects.Length - 1)
                    {
                        ret += ", ";
                    }
                }
                ret += " }";
                return ret;
            }
            else
            {
                return objects.ToString();
            }
        }

        /// <summary>
        /// 将列表转为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="tf"></param>
        /// <returns></returns>
        public static string ToString<T>(this List<T> ts, bool tf)
        {
            if (tf == false)
            {
                return ts.ToString();
            }
            else
            {
                var ret = "List<" + typeof(T).ToString() + "> ";
                ret += "[" + ts.Count + "] { ";
                for (int i = 0; i < ts.Count; i++)
                {
                    ret += ts[i].ToString();
                    if (i < ts.Count - 1)
                    {
                        ret += ", ";
                    }
                }
                ret += " }";
                return ret;
            }
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static string Join<T>(this List<T> ts, string add)
        {
            var ret = new StringBuilder();
            for (int i = 0; i < ts.Count - 1; i++)
            {
                ret.Append(ts[i]);
                ret.Append(add);
            }
            ret.Append(ts[ts.Count - 1]);
            return ret.ToString();
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static string Join<T>(this T[] ts, string add)
        {
            var ret = new StringBuilder();
            for (int i = 0; i < ts.Length - 1; i++)
            {
                ret.Append(ts[i]);
                ret.Append(add);
            }
            ret.Append(ts[ts.Length - 1]);
            return ret.ToString();
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerator<T> ts, string add)
        {
            if (ts == null || !ts.MoveNext())
            {
                // 如果枚举器为null或没有元素，返回空字符串
                return string.Empty;
            }

            var ret = new StringBuilder();
            ret.Append(ts.Current); // 先添加第一个元素

            while (ts.MoveNext()) // 检查是否有更多元素
            {
                ret.Append(add); // 先添加分隔符
                ret.Append(ts.Current); // 再添加元素
            }

            return ret.ToString();
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> ts, string add)
        {
            var ret = new StringBuilder();
            using (var enumerator = ts.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return string.Empty;  // 处理空集合
                }

                ret.Append(enumerator.Current);  // 添加第一个元素，避免在它之前添加分隔符

                while (enumerator.MoveNext())
                {
                    ret.Append(add);  // 在元素之间添加分隔符
                    ret.Append(enumerator.Current);
                }
            }

            return ret.ToString();
        }

        /// <summary>
        /// 将列表平铺
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> Join<T>(this List<List<T>> list)
        {
            var ret = new List<T>();
            list.ForEach(l =>
            {
                ret.AddRange(l);
            });
            return ret;
        }

        /// <summary>
        /// 将列表进行转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="ts"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<R> Amplify<T, R>(this List<T> ts, Func<T, R> func)
        {
            var ret = new List<R>();

            ts.ForEach(t =>
            {
                ret.Add(func(t));
            });

            return ret;
        }

        /// <summary>
        /// 将列表遍历转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="ts"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<R> Map<T,R>(this List<T> ts,Func<T,int,R> func)
        {
            var ret = new List<R>();
            ts.ForEach((t, i) =>
            {
                ret.Add(func(t, i));
            });
            return ret;
        }

        /// <summary>
        /// 对List的每个元素进行指定操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this List<T> ts, Action<T, int> action)
        {
            for (int i = 0; i < ts.Count; i++)
            {
                action.Invoke(ts[i], i);
            }
        }

        /// <summary>
        /// 反转当前这个列表顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static List<T> ReverseThis<T>(this List<T> ts)
        {
            ts.Reverse();
            return ts;
        }

        /// <summary>
        /// 转为字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static Dictionary<int,T> ToDict<T>(this List<T> ts)
        {
            var ret = new Dictionary<int, T>();
            ts.ForEach((t, i) =>
            {
                ret.Add(i, t);
            });
            return ret;
        }

        /// <summary>
        /// 转为字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="getKey">获取Key方法</param>
        /// <returns></returns>
        public static Dictionary<TKey,T> ToDict<TKey,T>(this List<T> ts,Func<T,TKey> getKey)
        {
            var ret = new Dictionary<TKey, T>();
            ts.ForEach(t =>
            {
                ret.Add(getKey(t), t);
            });
            return ret;
        }
    }
}
