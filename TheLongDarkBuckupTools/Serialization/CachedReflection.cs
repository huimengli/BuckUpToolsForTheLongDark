using System;
using System.Collections.Generic;
using System.Reflection;

namespace TheLongDarkBuckupTools.Serialization
{
    /// <summary>
    /// 成员自定义特性缓存类,按是否继承分别缓存
    /// </summary>
    static class MemberCustomAttributeCache<T>
    {
        public static Dictionary<MemberInfo, T> cache = new Dictionary<MemberInfo, T>();
        public static Dictionary<MemberInfo, T> cacheNoInherit = new Dictionary<MemberInfo, T>();
    }

    /// <summary>
    /// 带缓存的反射特性获取扩展方法,避免重复反射提升性能
    /// </summary>
    static class CachedReflection
    {

        public static T GetCustomAttributeCached<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            var cache = inherit ? MemberCustomAttributeCache<T>.cache : MemberCustomAttributeCache<T>.cacheNoInherit;
            if (cache.ContainsKey(memberInfo))
            {
                return cache[memberInfo];
            }
            var attr = memberInfo.GetCustomAttribute<T>(false);
            cache.Add(memberInfo, attr);
            return attr;
        }
    }
}
