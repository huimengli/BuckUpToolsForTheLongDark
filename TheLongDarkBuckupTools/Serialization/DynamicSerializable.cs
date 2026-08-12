using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using TheLongDarkBuckupTools.Helpers;

namespace TheLongDarkBuckupTools.Serialization
{
    /// <summary>
    /// 动态序列化引擎(移植自TLD-Save-Editor)
    /// 根据类上的 [Deserialize("字段名", json)] 特性,将存档JSON中的字段映射到类属性,
    /// 未知字段会被保留,序列化时原样写回,保证存档数据不丢失。
    /// </summary>
    /// <typeparam name="T">目标数据类型</typeparam>
    public class DynamicSerializable<T> where T : new()
    {

        private static JsonLoadSettings jsonLoadSettings = new JsonLoadSettings() { LineInfoHandling = LineInfoHandling.Ignore };

        /// <summary>
        /// 序列化设置:TLD存档中存在非标准JSON的 Infinity/-Infinity/NaN 值,
        /// 使用 Symbol 格式输出为无引号的 Infinity(与游戏原版格式一致),
        /// 避免默认 String 格式把浮点特殊值写成带引号的 "Infinity" 字符串,导致重载时报错。
        /// </summary>
        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { FloatFormatHandling = FloatFormatHandling.Symbol };

        private Dictionary<object, List<KeyValuePair<string, JToken>>> extraFields = new Dictionary<object, List<KeyValuePair<string, JToken>>>();
        public T Obj { get; private set; }

        public DynamicSerializable(string json)
        {
            Obj = Parse(JObject.Parse(json, jsonLoadSettings), typeof(T));

        }

        private dynamic Parse(JToken token, Type t, DeserializeAttribute attr = null)
        {
            bool deserialize = attr?.Json ?? false;
            bool deserializeItems = attr?.JsonItems ?? false;

            if (token.Type == JTokenType.Object)
            {
                if (typeof(IDictionary).IsAssignableFrom(t))
                    return ParseDictionary((JObject)token, t, deserializeItems);
                return ParseObject((JObject)token, t);
            }
            else if (token.Type == JTokenType.Array)
            {
                return ParseArray((JArray)token, t, deserializeItems);
            }
            else if (token.Type == JTokenType.Boolean)
            {
                if (t == typeof(string))
                    return token.ToString();
                return token.Value<bool>();
            }
            else if (token.Type == JTokenType.Integer || token.Type == JTokenType.Float)
            {
                if (t == typeof(string))
                    return token.ToString();
                if (t == typeof(object))
                    return ((JValue)token).Value;
                if (t == typeof(byte))
                    return token.Value<byte>();
                else if (t == typeof(short))
                    return token.Value<short>();
                else if (t == typeof(ushort))
                    return token.Value<ushort>();
                else if (t == typeof(int))
                    return token.Value<int>();
                else if (t == typeof(uint))
                    return token.Value<uint>();
                else if (t == typeof(long))
                    return token.Value<long>();
                else if (t == typeof(ulong))
                    return token.Value<ulong>();
                else if (t == typeof(float))
                    return token.Value<float>();
                else if (t == typeof(decimal))
                    return token.Value<decimal>();
                else if (t == typeof(double))
                    return token.Value<double>();
                else
                    throw new Exception("Unsupported type " + t.FullName);
            }
            else if (token.Type == JTokenType.String || t == typeof(string))
            {
                string s = token.Value<string>();
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(EnumWrapper<>))
                {
                    return Activator.CreateInstance(t, s);
                }
                else if (t == typeof(DateTime))
                {
                    return DateTime.Parse(s);
                }
                else if (t == typeof(byte[]))
                {
                    // TLD存档中m_Dict等字段为base64字符串,需转为byte[]
                    return Convert.FromBase64String(s);
                }
                // 防御性处理:某些工具/旧版本可能把浮点特殊值写成带引号的字符串(如 "Infinity"),
                // 目标为数值类型时尝试解析,失败则返回原始字符串
                else if (t == typeof(float))
                {
                    float f;
                    if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out f))
                        return f;
                    return s;
                }
                else if (t == typeof(double))
                {
                    double d;
                    if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
                        return d;
                    return s;
                }
                else if (t == typeof(decimal))
                {
                    decimal m;
                    if (decimal.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out m))
                        return m;
                    return s;
                }
                if (!deserialize)
                    return s;
                try
                {
                    return Parse(JToken.Parse(s, jsonLoadSettings), t);
                }
                catch
                {
                    // 嵌入的JSON字符串无法解析(如纯文本/非标准格式)时,回退为原始字符串
                    return s;
                }
            }
            else if (token.Type == JTokenType.Null)
            {
                return null;
            }
            else if (token.Type == JTokenType.Date && t == typeof(DateTime))
            {
                return token.Value<DateTime>();
            }
            else
            {
                throw new Exception("Invalid token type " + token.Type);
            }
        }

        private object ParseObject(JObject obj, Type t)
        {
            var result = Activator.CreateInstance(t);
            var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                 .Where(p => p.GetCustomAttributeCached<JsonIgnoreAttribute>(false) == null)
                .ToDictionary(p => MemberToName(p));
            var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public)
                 .Where(f => f.GetCustomAttributeCached<JsonIgnoreAttribute>(false) == null)
                .ToDictionary(p => MemberToName(p));

            foreach (var child in obj)
            {
                if (props.ContainsKey(child.Key))
                {
                    var prop = props[child.Key];
                    var attr = prop.GetCustomAttributeCached<DeserializeAttribute>(false);
                    var childType = prop.PropertyType;
                    var childVal = Parse(child.Value, childType, attr);
                    if (childVal == null && childType.IsValueType)
                        continue; // JSON null 赋给值类型时跳过,保留默认值
                    prop.SetValue(result, childVal);
                }
                else if (fields.ContainsKey(child.Key))
                {
                    var field = fields[child.Key];
                    var attr = field.GetCustomAttributeCached<DeserializeAttribute>(false);
                    var childType = field.FieldType;
                    var childVal = Parse(child.Value, childType, attr);
                    if (childVal == null && childType.IsValueType)
                        continue; // JSON null 赋给值类型时跳过,保留默认值
                    field.SetValue(result, childVal);
                }
                else
                {
                    if (!extraFields.ContainsKey(result))
                        extraFields.Add(result, new List<KeyValuePair<string, JToken>>());
                    extraFields[result].Add(child);
                }
            }
            return result;
        }

        private object ParseArray(JArray obj, Type t, bool deserializeItems)
        {
            Type elemType = t.GetElementType();
            if (!t.IsArray)
                elemType = t.GetGenericArguments().Single();
            Array result = Array.CreateInstance(elemType, obj.Count);
            int i = 0;
            foreach (var child in obj)
            {
                var val = Parse(child, elemType, new DeserializeAttribute(null, deserializeItems));
                if (val == null && elemType.IsValueType)
                    val = Activator.CreateInstance(elemType); // JSON null 元素用默认值填充
                result.SetValue(val, i++);
            }
            return ReflectionUtil.ConvertArray(result, t);
        }

        private object ParseDictionary(JObject obj, Type t, bool deserializeItems)
        {
            var dict = (IDictionary)Activator.CreateInstance(t);
            var keyType = t.GetGenericArguments()[0];
            var valType = t.GetGenericArguments()[1];
            foreach (var child in obj)
            {
                object key = Parse(child.Key, keyType);
                if (key is string && keyType.IsValueType && keyType != typeof(string))
                {
                    // JSON键始终是字符串,值类型键需转换
                    try { key = Convert.ChangeType(key, keyType); } catch { /* 忽略无法转换的键 */ }
                }
                dict.Add(key, Parse(child.Value, valType, new DeserializeAttribute(null, deserializeItems)));
            }
            return dict;
        }

        public string Serialize()
        {
            var res = ReconstructObject(Obj);
            return JsonConvert.SerializeObject(res, jsonSerializerSettings);
        }

        /// <summary>
        /// 深拷贝对象:通过"重构→序列化→重新解析"完整复制对象的全部字段,
        /// 包括已知字段(属性/字段)和未知字段(extraFields 保留的数据),
        /// 确保复制出的对象与原对象完全独立,保存时数据不丢失。
        /// </summary>
        public object CloneObject(object o)
        {
            var res = ReconstructObject(o);
            var json = JsonConvert.SerializeObject(res, jsonSerializerSettings);
            return Parse(JObject.Parse(json, jsonLoadSettings), o.GetType());
        }

        public object Reconstruct(object o, DeserializeAttribute attr = null)
        {
            object result = null;
            if (o == null)
                result = null;
            else
            {
                Type t = o.GetType();
                if (o is DateTime)
                {
                    // 与TLD存档的时间格式保持一致
                    result = ((DateTime)o).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else if (ReflectionUtil.IsBoxed(o) || o is string)
                {
                    result = o;
                }
                else if (o is IDictionary)
                {
                    result = ReconstructDictionary((IDictionary)o, attr?.JsonItems ?? false);
                }
                else if (o is ICollection || ReflectionUtil.ImplementsGenericInterface(o.GetType(), typeof(ICollection<>)))
                {
                    result = ReconstructCollection(o, attr?.JsonItems ?? false);
                }
                else if (o.GetType().IsGenericType && o.GetType().GetGenericTypeDefinition() == typeof(EnumWrapper<>))
                {
                    return o.ToString();
                }
                else
                {
                    result = ReconstructObject(o);
                }
            }

            if (attr != null && result != null && attr.Json)
                return JsonConvert.SerializeObject(result, jsonSerializerSettings);
            return result;
        }

        public object ReconstructObject(object o)
        {
            var res = new Dictionary<string, dynamic>();
            var t = o.GetType();
            var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttributeCached<JsonIgnoreAttribute>(false) == null).ToArray();
            var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttributeCached<JsonIgnoreAttribute>(false) == null).ToArray();

            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttributeCached<DeserializeAttribute>(false);
                var name = attr?.From ?? prop.Name;
                res.Add(name, Reconstruct(prop.GetValue(o), attr));
            }
            foreach (var field in fields)
            {
                var attr = field.GetCustomAttributeCached<DeserializeAttribute>(false);
                var name = attr?.From ?? field.Name;
                res.Add(name, Reconstruct(field.GetValue(o), attr));
            }
            if (extraFields.ContainsKey(o))
            {
                foreach (var kvp in extraFields[o])
                {
                    res.Add(kvp.Key, kvp.Value);
                }
            }
            return res;
        }

        public dynamic ReconstructCollection(dynamic col, bool serializeItems)
        {
            if (col.GetType() == typeof(byte[]))
            {
                return col;
            }
            var result = new List<object>();
            foreach (var item in col)
            {
                result.Add(Reconstruct(item, new DeserializeAttribute(null, serializeItems)));
            }
            return result.ToArray();
        }

        public IDictionary ReconstructDictionary(IDictionary dict, bool serializeItems)
        {
            var res = new Dictionary<object, object>();
            foreach (var key in dict.Keys)
            {
                res.Add(Reconstruct(key), Reconstruct(dict[key], new DeserializeAttribute(null, serializeItems)));
            }
            return res;
        }

        private string MemberToName(MemberInfo m)
        {
            var attr = m.GetCustomAttributeCached<DeserializeAttribute>(false);
            if (attr != null)
                return attr.From;
            return m.Name;
        }

    }
}
