using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLongDarkBuckupTools.Serialization
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DeserializeAttribute : Attribute
    {
        public string From { get; private set; }
        public bool Json { get; private set; }
        public bool JsonItems { get; private set; }

        public DeserializeAttribute(string from, bool json = false, bool jsonItems = false)
        {
            From = from;
            Json = json;
            JsonItems = jsonItems;
        }
    }
}
