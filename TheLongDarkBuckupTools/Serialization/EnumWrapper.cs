using System.Collections.ObjectModel;
using System;
using TheLongDarkBuckupTools.Helpers;

namespace TheLongDarkBuckupTools.Serialization
{
    /// <summary>
    /// EnumWrapper 是一个抽象基类，继承自 BindableBase
    /// 它用于封装枚举类型的功能，并提供数据绑定支持
    /// </summary>
    public abstract class EnumWrapper : BindableBase
    {
        /// <summary>
        /// 获取或设置当前选中的枚举值
        /// 这是一个抽象属性，需要在派生类中实现
        /// </summary>
        public abstract string Value { get; set; }
        /// <summary>
        /// 获取所有可用的枚举值集合
        /// 这是一个抽象属性，需要在派生类中实现
        /// 返回一个 ObservableCollection<string> 类型的集合，支持数据绑定和通知
        /// </summary>
        public abstract ObservableCollection<string> Values { get; }
    }

    /// <summary>
    /// 泛型枚举包装器类，用于处理枚举类型的相关操作
    /// </summary>
    /// <typeparam name="T">必须是枚举类型的泛型参数</typeparam>
    public class EnumWrapper<T> : EnumWrapper where T : Enum
    {
        /// <summary>
        /// 静态内部类，用于存储特定枚举类型的值集合
        /// </summary>
        /// <typeparam name="T2">必须是枚举类型的泛型参数</typeparam>
        static class EnumValues<T2> where T2 : Enum
        {
            /// <summary>
            /// 存储枚举名称的ObservableCollection集合
            /// </summary>
            public static ObservableCollection<string> values = new ObservableCollection<string>(Enum.GetNames(typeof(T2)));
        }

        /// <summary>
        /// 构造函数，初始化EnumWrapper实例
        /// </summary>
        /// <param name="s">初始值字符串</param>
        public EnumWrapper(string s)
        {
            Value = s;
        }

        private string _value;
        /// <summary>
        /// 获取或设置当前枚举值
        /// </summary>
        public override string Value
        {
            get { return _value; }
            set
            {
                // 如果新值不在集合中，则添加到集合
                if (!EnumValues<T>.values.Contains(value))
                {
                    EnumValues<T>.values.Add(value);
                }
                // 使用SetProperty方法更新值（假设这是一个实现了INotifyPropertyChanged接口的基类方法）
                SetProperty(ref _value, value);
            }
        }

        /// <summary>
        /// 获取所有可用的枚举值集合
        /// </summary>
        public override ObservableCollection<string> Values
        {
            get
            {
                return EnumValues<T>.values;
            }
        }

        /// <summary>
        /// 通过枚举值设置当前包装器的值
        /// </summary>
        /// <param name="val">要设置的枚举值</param>
        public void SetValue(T val)
        {
            Value = val.ToString();
        }

        /// <summary>
        /// 返回当前值的字符串表示
        /// </summary>
        /// <returns>当前值的字符串</returns>
        public override string ToString()
        {
            return _value;
        }

    }
}