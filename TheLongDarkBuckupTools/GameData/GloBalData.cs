using System;
using System.Collections.Generic;

namespace TheLongDarkBuckupTools.GameData
{
    /// <summary>
    /// 插槽数据
    /// </summary>
    public class SlotData
    {
        #region 数据部分
        /// <summary>
        /// 存档名称
        /// </summary>
        public string m_Name { get; set; }
        /// <summary>
        /// 基名称?
        /// </summary>
        public string m_BaseName { get; set; }
        /// <summary>
        /// 玩家给存档的命名
        /// </summary>
        public string m_DisplayName { get; set; }
        /// <summary>
        /// 存档上次修改时间
        /// </summary>
        public DateTime m_Timestamp { get; set; }
        /// <summary>
        /// 游戏模式
        /// sandbox:沙盒;
        /// story:剧情;
        /// ...
        /// </summary>
        public string m_GameMode { get; set; }
        /// <summary>
        /// 存档指数(这是该模式下的第几个存档)
        /// </summary>
        public string m_GameId { get; set; }
        /// <summary>
        /// 片段/插曲?
        /// </summary>
        public string m_Episode { get; set; }
        /// <summary>
        /// 玩家,地图,状态...的数据
        /// </summary>
        public Dictionary<string, byte[]> m_Dict { get; set; }
        /// <summary>
        /// 存档版本
        /// </summary>
        public int m_VersionChangelistNumber { get; set; }

        #endregion

        #region 功能模块
        /// <summary>
        /// 获取存档的游戏模式
        /// </summary>
        /// <param name="data">存档插槽数据</param>
        /// <returns></returns>
        public static string GetGameMode(SlotData data)
        {
            switch ((GameMode)Enum.Parse(typeof(GameMode),data.m_GameMode))
            {
                case GameMode.CHECKPOINT:
                    return "检查点";
                case GameMode.SANDBOX:
                    return "沙盒模式";
                case GameMode.STORY:
                    return "剧情模式";
                default:
                    return data.m_GameMode;
            }
        }

        /// <summary>
        /// 获取存档的游戏模式
        /// </summary>
        /// <returns></returns>
        public string GetGameMode()
        {
            return GetGameMode(this);
        }



        #endregion
    }

    /// <summary>
    /// 截图数据
    /// </summary>
    public class Screenshot
    {
        /// <summary>
        /// 解码
        /// </summary>
        public string m_Encoded { get; set; }

        /// <summary>
        /// 重写转字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{\"m_Encoded\":\"" + m_Encoded.ToString() + "\"}";
        }

        /// <summary>
        /// 截图数据转化为图片
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Image ToImage()
        {
            return (System.Drawing.Image)Item.GetImageFromBase64(this.m_Encoded);
        }
    }
}
