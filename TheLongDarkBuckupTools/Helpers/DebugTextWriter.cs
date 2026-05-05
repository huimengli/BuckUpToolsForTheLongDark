using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLongDarkBuckupTools.Helpers
{
    public class DebugTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string value)
        {
            // 关键：将内容输出到VS的“输出”窗口
            Trace.WriteLine(value);
            // 如果需要同时保留原样输出到控制台（如果存在），可调用base.WriteLine(value)
        }
    }
}
