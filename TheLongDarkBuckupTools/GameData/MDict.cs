using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLongDarkBuckupTools.GameData
{
    /// <summary>
    /// 玩家,地图,状态...的数据
    /// </summary>
    public class MDict
    {
        /// <summary>
        /// 
        /// </summary>
        public byte[] boot { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] global { get; set; }

        /// <summary>
        /// 存档截图
        /// </summary>
        public byte[] screenshot { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] sandbox_global { get; set; }
    }
}
