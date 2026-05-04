using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Serialization;

namespace TheLongDarkBuckupTools.MoreData
{
    /// <summary>
    /// 启动存档格式类，用于存储游戏启动时的场景信息
    /// </summary>
    public class BootSaveGameFormat
    {
        /// <summary>
        /// 场景名称，使用EnumWrapper包装RegionsWithMap枚举类型
        /// </summary>
        public EnumWrapper<RegionsWithMap> m_SceneName { get; set; }
        /// <summary>
        /// 存档版本号，用于存档格式兼容性检查
        /// </summary>
        public int m_Version { get; set; }
    }
}
