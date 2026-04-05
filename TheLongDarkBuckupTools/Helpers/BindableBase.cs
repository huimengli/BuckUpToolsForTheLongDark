using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TheLongDarkBuckupTools.Helpers
{
    /// <summary>
    /// 提供属性变更通知功能的基础抽象类
    /// 实现了INotifyPropertyChanged接口，用于数据绑定时的属性变更通知
    /// </summary>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性变更事件
        /// 当属性值改变时触发，用于通知数据绑定属性已更新
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
