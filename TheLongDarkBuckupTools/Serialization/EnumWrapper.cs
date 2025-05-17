using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLongDarkBuckupTools.Helpers;

namespace TheLongDarkBuckupTools.Serialization
{
    public abstract class EnumWrapper : BindableBase
    {
        public abstract string Value { get; set; }
        public abstract ObservableCollection<string> Values { get; }
    }

    public class EnumWrapper<T> : EnumWrapper where T : Enum
    {
        static class EnumValues<T2> where T2 : Enum
        {
            public static ObservableCollection<string> values = new ObservableCollection<string>(Enum.GetNames(typeof(T2)));
        }

        public EnumWrapper(string s)
        {
            Value = s;
        }

        private string _value;
        public override string Value
        {
            get { return _value; }
            set
            {
                if (!EnumValues<T>.values.Contains(value))
                {
                    EnumValues<T>.values.Add(value);
                }
                SetProperty(ref _value, value);
            }
        }

        public override ObservableCollection<string> Values
        {
            get
            {
                return EnumValues<T>.values;
            }
        }

        public void SetValue(T val)
        {
            Value = val.ToString();
        }

        public override string ToString()
        {
            return _value;
        }

    }
}
