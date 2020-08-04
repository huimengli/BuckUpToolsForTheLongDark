using System;
using System.Collections.Generic;
using System.Text;

namespace readBuckFile.GameData
{
    class GloBalData
    {
    }

    /// <summary>
    /// 插槽数据
    /// </summary>
    public class SlotData
    {
        public string m_Name { get; set; }
        public string m_BaseName { get; set; }
        public string m_DisplayName { get; set; }
        public DateTime m_Timestamp { get; set; }
        public string m_GameMode { get; set; }
        public string m_GameId { get; set; }
        public string m_Episode { get; set; }
        public Dictionary<string, byte[]> m_Dict { get; set; }
    }

}
