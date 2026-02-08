using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheLongDarkBuckupTools.Class
{
    /// <summary>
    /// 增强List类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class ListEX<T> : List<T>
    {
        /// <summary>
        /// 查询字符串默认报错
        /// </summary>
        private const string QueryErrorMessage =
            "查询字符串格式错误。正确格式: \"start:end\" 或 \"start:end:step\"。示例: \"1:3\", \"-2:-1\", \"1:\", \":5\"";

        // 查询字符串的正则表达式，支持以下几种格式：
        // "1:3"     - 从索引1到3（包含1，不包含3）
        // "1:"      - 从索引1到末尾
        // ":3"      - 从开始到索引3
        // ":"       - 整个列表
        // "-2:-1"   - 使用负索引
        // "1:10:2"  - 从1到10，步长为2
        private static readonly Regex ReadQuery = new Regex(
            @"^\s*(-?\d*)\s*:\s*(-?\d*)(?::\s*(-?\d+))?\s*$",
            RegexOptions.Compiled
        );

        public ListEX(int capacity) : base(capacity)
        {
        }

        public ListEX() : base()
        {
        }

        public ListEX(IEnumerable<T> collection) : base(collection)
        {

        }

        /// <summary>
        /// 重写索引器，支持负索引和自动扩容
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new T this[int index]
        {
            get => base[NormalizeIndex(index, nameof(index))];
            set
            {
                // 标准化索引
                int normalizedIndex;
                if (index < 0)
                {
                    normalizedIndex = Count + index;
                    if (normalizedIndex < 0)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(index),
                            index,
                            $"索引 {index} 超出范围。有效索引范围：{-Count} 到 {Count - 1}"
                        );
                    }
                }
                else
                {
                    normalizedIndex = index;
                }

                // 如果需要扩容
                if (normalizedIndex >= Count)
                {
                    int itemsToAdd = normalizedIndex - Count + 1;
                    for (int i = 0; i < itemsToAdd; i++)
                    {
                        Add(default);
                    }
                }

                base[normalizedIndex] = value;
            }
        }

        /// <summary>
        /// 字符串查询索引器，支持切片语法
        /// </summary>
        /// <param name="query">查询字符串，格式: "start:end" 或 "start:end:step"</param>
        /// <returns>切片后的新列表</returns>
        public ListEX<T> this[string query]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(query))
                    throw new ArgumentNullException(nameof(query), QueryErrorMessage);

                var m = ReadQuery.Match(query);
                if (!m.Success)
                    throw new ArgumentException(QueryErrorMessage, nameof(query));

                // 解析起始位置
                int start = 0;
                if (m.Groups[1].Success && !string.IsNullOrEmpty(m.Groups[1].Value))
                {
                    start = int.Parse(m.Groups[1].Value);
                    if (start < 0) start = Count + start; // 处理负索引
                }

                // 解析结束位置
                int end = Count;
                if (m.Groups[2].Success && !string.IsNullOrEmpty(m.Groups[2].Value))
                {
                    end = int.Parse(m.Groups[2].Value);
                    if (end < 0) end = Count + end; // 处理负索引
                    if (end < 0) end = 0; // 确保不小于0
                }

                // 解析步长（可选）
                int step = 1;
                if (m.Groups[3].Success && !string.IsNullOrEmpty(m.Groups[3].Value))
                {
                    step = int.Parse(m.Groups[3].Value);
                    if (step == 0) throw new ArgumentException("步长不能为0", nameof(query));
                }

                // 边界检查
                if (start < 0) start = 0;
                if (end > Count) end = Count;

                var result = new ListEX<T>();

                // 处理步长
                if (step > 0)
                {
                    // 正向步长：从start到end-1
                    for (int i = start; i < end; i += step)
                    {
                        if (i >= 0 && i < Count)
                            result.Add(this[i]);
                    }
                }
                else
                {
                    // 负向步长：从start到end+1（反向）
                    // 注意：当step为负数时，start应该大于end
                    if (start <= end)
                    {
                        // 如果start不大于end，返回空列表
                        return result;
                    }

                    for (int i = start; i > end; i += step) // step为负数，所以i会递减
                    {
                        if (i >= 0 && i < Count)
                            result.Add(this[i]);
                    }
                }

                return result;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(query))
                    throw new ArgumentNullException(nameof(query), QueryErrorMessage);

                var m = ReadQuery.Match(query);
                if (!m.Success)
                    throw new ArgumentException(QueryErrorMessage, nameof(query));

                // 解析起始位置
                int start = 0;
                if (m.Groups[1].Success && !string.IsNullOrEmpty(m.Groups[1].Value))
                {
                    start = int.Parse(m.Groups[1].Value);
                    if (start < 0) start = Count + start; // 处理负索引
                }

                // 解析结束位置
                int end = Count;
                if (m.Groups[2].Success && !string.IsNullOrEmpty(m.Groups[2].Value))
                {
                    end = int.Parse(m.Groups[2].Value);
                    if (end < 0) end = Count + end; // 处理负索引
                    if (end < 0) end = 0; // 确保不小于0
                }

                // 解析步长（可选）
                int step = 1;
                if (m.Groups[3].Success && !string.IsNullOrEmpty(m.Groups[3].Value))
                {
                    step = int.Parse(m.Groups[3].Value);
                    if (step == 0) throw new ArgumentException("步长不能为0", nameof(query));
                }

                // 边界检查
                if (start < 0) start = 0;
                if (end > Count) end = Count;

                // 验证输入
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                // 处理步长为1的情况（最常见）
                if (step == 1)
                {
                    // 计算要替换的元素数量
                    int rangeLength = end - start;

                    // 如果范围长度与输入列表长度不匹配，需要调整
                    if (rangeLength != value.Count)
                    {
                        // 先移除范围内的元素
                        if (rangeLength > 0)
                        {
                            RemoveRange(start, rangeLength);
                        }

                        // 插入新元素
                        InsertRange(start, value);
                    }
                    else
                    {
                        // 直接替换
                        for (int i = 0; i < rangeLength; i++)
                        {
                            if (start + i < Count)
                                this[start + i] = value[i];
                        }
                    }
                }
                else if (step > 1)
                {
                    // 带步长的赋值：只替换指定步长的位置
                    int targetIndex = 0;
                    for (int i = start; i < end && targetIndex < value.Count; i += step)
                    {
                        if (i >= 0 && i < Count)
                        {
                            this[i] = value[targetIndex];
                            targetIndex++;
                        }
                    }
                }
                else // step < 0
                {
                    // 负步长的赋值
                    if (start <= end) return; // 无效范围

                    int targetIndex = 0;
                    for (int i = start; i > end && targetIndex < value.Count; i += step) // step为负
                    {
                        if (i >= 0 && i < Count)
                        {
                            this[i] = value[targetIndex];
                            targetIndex++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 特殊索引器
        /// <br />
        /// 取值:
        /// 支持负索引, 当正索引超出长度时,会返回默认值
        /// <br />
        /// 赋值:
        /// 支持负索引, 当正索引超出长度时,会自动扩展
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[long idx]
        {
            get
            {
                int index = (int)idx;
                if (index < 0)
                {
                    return this[index];
                }
                else
                {
                    try
                    {
                        return this[index];
                    }
                    catch (ArgumentOutOfRangeException ae)
                    {
                        return default;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            set
            {
                int index = (int)idx;

                // 标准化索引
                int normalizedIndex;
                if (index < 0)
                {
                    normalizedIndex = Count + index;
                    if (normalizedIndex < 0)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(index),
                            index,
                            $"索引 {index} 超出范围。有效索引范围：{-Count} 到 {Count - 1}"
                        );
                    }
                }
                else
                {
                    normalizedIndex = index;
                }

                // 如果需要扩容
                if (normalizedIndex >= Count)
                {
                    int itemsToAdd = normalizedIndex - Count + 1;
                    for (int i = 0; i < itemsToAdd; i++)
                    {
                        Add(default);
                    }
                }

                base[normalizedIndex] = value;
            }
        }

        /// <summary>
        /// 将可能为负的索引转换为正索引
        /// </summary>
        private int NormalizeIndex(int index, string paramName)
        {
            if (index < 0)
            {
                int positiveIndex = Count + index;
                if (positiveIndex < 0 || positiveIndex >= Count)
                {
                    throw new ArgumentOutOfRangeException(
                        paramName,
                        index,
                        $"索引 {index} 超出范围。有效索引范围：{-Count} 到 {Count - 1}"
                    );
                }
                return positiveIndex;
            }

            if (index >= Count)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    index,
                    $"索引 {index} 超出范围。列表大小为 {Count}"
                );
            }

            return index;
        }

        /// <summary>
        /// 在指定索引处插入元素，支持负索引
        /// </summary>
        /// <param name="index">插入位置的索引</param>
        /// <param name="item">要插入的元素</param>
        public new void Insert(int index, T item)
        {
            int normalizedIndex = NormalizeIndexForInsert(index, nameof(index));
            base.Insert(normalizedIndex, item);
        }

        /// <summary>
        /// 移除指定索引处的元素，支持负索引
        /// </summary>
        /// <param name="index">要移除的元素的索引</param>
        public new void RemoveAt(int index)
        {
            int normalizedIndex = NormalizeIndex(index, nameof(index));
            base.RemoveAt(normalizedIndex);
        }

        /// <summary>
        /// 为Insert方法标准化索引
        /// </summary>
        private int NormalizeIndexForInsert(int index, string paramName)
        {
            // 处理负索引
            if (index < 0)
            {
                int positiveIndex = Count + index;
                if (positiveIndex < 0 || positiveIndex > Count)
                {
                    throw new ArgumentOutOfRangeException(
                        paramName,
                        $"索引 {index} 超出范围。有效插入位置：{-Count} 到 {Count}"
                    );
                }
                return positiveIndex;
            }

            // 检查正索引是否越界
            if (index > Count)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    index,
                    $"索引 {index} 超出范围。列表大小为 {Count}"
                );
            }

            return index;
        }

        /// <summary>
        /// 获取列表的一部分，支持负索引
        /// </summary>
        /// <param name="start">起始索引（包含）</param>
        /// <param name="count">要获取的元素数量</param>
        /// <returns>新的ListEX实例</returns>
        public new ListEX<T> Slice(int start, int count)
        {
            int normalizedStart = NormalizeIndex(start, nameof(start));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "数量不能为负数");
            if (normalizedStart + count > Count)
                throw new ArgumentOutOfRangeException(nameof(count), "指定的范围超出列表边界");

            var result = new ListEX<T>();
            for (int i = normalizedStart; i < normalizedStart + count; i++)
            {
                result.Add(this[i]);
            }
            return result;
        }

        /// <summary>
        /// 获取列表的一部分，从指定索引到末尾
        /// </summary>
        /// <param name="start">起始索引（包含）</param>
        /// <returns>新的ListEX实例</returns>
        public ListEX<T> Slice(int start)
        {
            int normalizedStart = NormalizeIndex(start, nameof(start));
            return Slice(normalizedStart, Count - normalizedStart);
        }

        /// <summary>
        /// 获取最后几个元素
        /// </summary>
        /// <param name="count">要获取的元素数量</param>
        /// <returns>新的ListEX实例</returns>
        public ListEX<T> TakeLast(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "数量不能为负数");
            if (count > Count)
                count = Count;

            return Slice(-count, count);
        }

        /// <summary>
        /// 获取第一个元素，如果列表为空则返回默认值
        /// </summary>
        public T FirstOrDefault()
        {
            return Count > 0 ? this[0] : default(T);
        }

        /// <summary>
        /// 获取最后一个元素，如果列表为空则返回默认值
        /// </summary>
        public T LastOrDefault()
        {
            return Count > 0 ? this[-1] : default(T);
        }
    }
}
