using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TheLongDarkBuckupTools.Serialization
{
    /// <summary>
    /// 反射工具类,提供DynamicSerializable反序列化时所需的各种反射辅助方法
    /// </summary>
    public static class ReflectionUtil
    {
        /// <summary>
        /// 将Array转换为目标类型(数组、IList或HashSet)
        /// </summary>
        public static object ConvertArray(Array arr, Type t)
        {
            if (t.IsArray)
            {
                return Convert.ChangeType(arr, t);
            }
            else if ((typeof(IList).IsAssignableFrom(t)))
            {
                var list = (IList)Activator.CreateInstance(t);
                foreach (var item in arr)
                {
                    list.Add(item);
                }
                return list;
            }
            else if (t.IsGenericType && typeof(HashSet<>).IsAssignableFrom(t.GetGenericTypeDefinition()))
            {
                var set = Activator.CreateInstance(t);
                MethodInfo method = set.GetType().GetMethod("Add", new Type[] { t.GetGenericArguments()[0] });
                foreach (var item in arr)
                {
                    method.Invoke(set, new object[] { item });
                }
                return set;
            }
            throw new Exception("Unsupported collection type " + t);
        }

        /// <summary>
        /// 判断值是否被装箱(即对象实际类型是值类型但以接口或object引用)
        /// </summary>
        public static bool IsBoxed<T>(T value)
        {
            return
                (typeof(T).IsInterface || typeof(T) == typeof(object)) &&
                value != null &&
                value.GetType().IsValueType;
        }

        /// <summary>
        /// 判断类型是否实现了指定的泛型接口
        /// </summary>
        public static bool ImplementsGenericInterface(Type t, Type t2)
        {
            return t.GetInterfaces().Any(i =>
               i.IsGenericType &&
               i.GetGenericTypeDefinition() == t2);
        }

    }
}
