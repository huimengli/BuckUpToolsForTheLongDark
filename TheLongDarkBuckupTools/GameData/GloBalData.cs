using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TheLongDarkBuckupTools.Helpers;

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
        /// 老版本名称
        /// </summary>
        public string m_Name { get; set; }
        /// <summary>
        /// 存档名称
        /// 新版本名称
        /// </summary>
        public string m_InternalName { get; set; }
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
        public MDict m_Dict { get; set; }
        /// <summary>
        /// 存档版本(老版本存档数据名称)
        /// 游戏版本号
        /// </summary>
        public int m_VersionChangelistNumber { get; set; }
        /// <summary>
        /// 存档版本(新版本存档数据名称)
        /// 游戏版本号
        /// </summary>
        public int m_Changelist { get; set; }

        /// <summary>
        /// 未知内容
        /// </summary>
        public int m_Version { get; set; }

        /// <summary>
        /// 已安装选项内容
        /// 已安装的DLC内容
        /// </summary>

        public List<string> m_InstalledOptionalContent { get; set; }

        #endregion

        #region 功能模块
        /// <summary>
        /// 获取存档的游戏模式
        /// </summary>
        /// <param name="data">存档插槽数据</param>
        /// <returns></returns>
        public static string GetGameMode(SlotData data)
        {
            switch ((GameMode)Enum.Parse(typeof(GameMode), data.m_GameMode))
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

        public static explicit operator Screenshot(Image v)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 场景信息
    /// </summary>
    public class BootData
    {
        /// <summary>
        /// unity场景名称
        /// </summary>
        public string m_SceneName { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public int m_Version { get; set; }
    }

    /// <summary>
    /// 全局数据格式
    /// </summary>
    public class GlobalDataFormat : IEnumerable<string>
    {
        #region 数据模块
        /// <summary>
        /// 版本
        /// </summary>
        public int m_Version { get; set; }
        /// <summary>
        /// 游戏管理器标准化
        /// </summary>
        public string m_GameManagerSerialized { get; set; }
        /// <summary>
        /// Hud管理器已序列化
        /// </summary>
        public string m_HudManagerSerialized { get; set; }
        /// <summary>
        /// 每日时间序列
        /// </summary>
        public string m_TimeOfDay_Serialized { get; set; }
        /// <summary>
        /// 风序列化
        /// </summary>
        public string m_Wind_Serialized { get; set; }
        /// <summary>
        /// 天气序列化
        /// </summary>
        public string m_Weather_Serialized { get; set; }
        /// <summary>
        /// 天气变化序列化
        /// </summary>
        public string m_WeatherTransition_Serialized { get; set; }
        /// <summary>
        /// 条件序列化
        /// </summary>
        public string m_Condition_Serialized { get; set; }
        /// <summary>
        /// 超重序列化
        /// </summary>
        public string m_Encumber_Serialized { get; set; }
        /// <summary>
        /// 饥饿序列化
        /// </summary>
        public string m_Hunger_Serialized { get; set; }
        /// <summary>
        /// 口渴序列化
        /// </summary>
        public string m_Thirst_Serialized { get; set; }
        /// <summary>
        /// 疲劳序列化
        /// </summary>
        public string m_Fatigue_Serialized { get; set; }
        /// <summary>
        /// 寒冷序列化
        /// </summary>
        public string m_Freezing_Serialized { get; set; }
        /// <summary>
        /// 精力序列化
        /// </summary>
        public string m_Willpower_Serialized { get; set; }
        /// <summary>
        /// 库存序列化
        /// </summary>
        public string m_Inventory_Serialized { get; set; }
        /// <summary>
        /// 沙盒管理器系列化
        /// </summary>
        public string m_SandboxManagerSerialized { get; set; }
        /// <summary>
        /// 故事管理器系列化
        /// </summary>
        public string m_StoryManagerSerialized { get; set; }
        /// <summary>
        /// 玩家管理者系列化
        /// </summary>
        public string m_PlayerManagerSerialized { get; set; }
        /// <summary>
        /// 玩家爬绳系列化
        /// </summary>
        public string m_PlayerClimbRopeSerialized { get; set; }
        /// <summary>
        /// 玩家技能系列化
        /// </summary>
        public string m_PlayerSkillsSerialized { get; set; }
        /// <summary>
        /// 玩家游戏统计数据序列化
        /// </summary>
        public string m_PlayerGameStatsSerialized { get; set; }
        /// <summary>
        /// 低温序列化
        /// </summary>
        public string m_HypothermiaSerialized { get; set; }
        /// <summary>
        /// 冻伤序列化
        /// </summary>
        public string m_FrostbiteSerialized { get; set; }
        /// <summary>
        /// 食物中毒序列化
        /// </summary>
        public string m_FoodPoisoningSerialized { get; set; }
        /// <summary>
        /// 痢疾序列化
        /// </summary>
        public string m_DysenterySerialized { get; set; }
        /// <summary>
        /// 扭伤脚踝序列化
        /// </summary>
        public string m_SprainedAnkleSerialized { get; set; }
        /// <summary>
        /// 手腕扭伤序列化
        /// </summary>
        public string m_SprainedWristSerialized { get; set; }
        /// <summary>
        /// 腕关节扭伤序列化
        /// </summary>
        public string m_SprainedWristMajorSerialized { get; set; }
        /// <summary>
        /// 烧伤序列化
        /// </summary>
        public string m_BurnsSerialized { get; set; }
        /// <summary>
        /// 电烧伤序列化
        /// </summary>
        public string m_BurnsElectricSerialized { get; set; }
        /// <summary>
        /// 失血序列化
        /// </summary>
        public string m_BloodLossSerialized { get; set; }
        /// <summary>
        /// 肋骨断裂序列化
        /// </summary>
        public string m_BrokenRibSerialized { get; set; }
        /// <summary>
        /// 感染序列化
        /// </summary>
        public string m_InfectionSerialized { get; set; }
        /// <summary>
        /// 感染风险序列化
        /// </summary>
        public string m_InfectionRiskSerialized { get; set; }
        /// <summary>
        /// 日志序列化
        /// </summary>
        public string m_LogSerialized { get; set; }
        /// <summary>
        /// 休息序列化
        /// </summary>
        public string m_RestSerialized { get; set; }
        /// <summary>
        /// 飞越系列化
        /// </summary>
        public string m_FlyOverSerialized { get; set; }
        /// <summary>
        /// 成就管理序列化
        /// </summary>
        public string m_AchievementManagerSerialized { get; set; }
        /// <summary>
        /// 体验模式管理器已序列化
        /// </summary>
        public string m_ExperienceModeManagerSerialized { get; set; }
        /// <summary>
        /// 极光管理序列化
        /// </summary>
        public string m_AuroraManagerSerialized { get; set; }
        /// <summary>
        /// 玩家移动序列化
        /// </summary>
        public string m_PlayerMovementSerialized { get; set; }
        /// <summary>
        /// 玩家挣扎序列化
        /// </summary>
        public string m_PlayerStruggleSerialized { get; set; }
        /// <summary>
        /// 面板统计信息序列化
        /// </summary>
        public string m_PanelStatsSerialized { get; set; }
        /// <summary>
        /// 紧急刺激序列化
        /// </summary>
        public string m_EmergencyStimSerialized { get; set; }
        /// <summary>
        /// 音乐活动管理器序列化
        /// </summary>
        public string m_MusicEventManagerSerialized { get; set; }
        /// <summary>
        /// 烟囱数据序列化
        /// </summary>
        public string m_ChimneyDataSerialized { get; set; }
        /// <summary>
        /// 幽闭症序列化
        /// </summary>
        public string m_CabinFeverSerialized { get; set; }
        /// <summary>
        /// 肠道寄生虫序列化
        /// </summary>
        public string m_IntestinalParasitesSerialized { get; set; }
        /// <summary>
        /// 雪地管理器序列化
        /// </summary>
        public string m_SnowPatchManagerSerialized { get; set; }
        /// <summary>
        /// 玩家动画序列化
        /// </summary>
        public string m_PlayerAnimationSerialized { get; set; }
        /// <summary>
        /// 技能系列序列化
        /// </summary>
        public string m_SkillsManagerSerialized { get; set; }
        /// <summary>
        /// 锁定伙伴序列化
        /// </summary>
        public string m_LockCompanionsSerialized { get; set; }
        /// <summary>
        /// 专长启用序列化
        /// </summary>
        public string m_FeatsEnabledSerialized { get; set; }
        /// <summary>
        /// 信任关系管理序列化
        /// </summary>
        public string m_TrustManagerSerialized { get; set; }
        /// <summary>
        /// 地图详细信息管理序列化
        /// </summary>
        public string m_MapDetailManagerSerialized { get; set; }
        /// <summary>
        /// 世界地图数据序列化
        /// </summary>
        public string m_WorldMapDataSerialized { get; set; }
        /// <summary>
        /// 地图数据序列化
        /// </summary>
        public string m_MapDataSerialized { get; set; }
        /// <summary>
        /// 猎杀熊序列化
        /// </summary>
        public string m_BearHuntSerialized { get; set; }
        /// <summary>
        /// 知识管理序列化
        /// </summary>
        public string m_KnowledgeManagerSerialized { get; set; }
        /// <summary>
        /// 解锁的蓝图序列化
        /// </summary>
        public string m_UnlockedBlueprintsSerialized { get; set; }
        /// <summary>
        /// 收集品管理序列化
        /// </summary>
        public string m_CollectionManagerSerialized { get; set; }
        /// <summary>
        /// 故事任务数据系列化
        /// </summary>
        public string m_StoryMissionDataSerialized { get; set; }
        /// <summary>
        /// 当前剧情完成度
        /// </summary>
        public bool m_CurrentEpisodeComplete { get; set; }

        public IEnumerator<string> GetEnumerator()
        {
            return GetList().GetEnumerator();
        }

        #endregion

        #region 功能模块

        public override string ToString()
        {
            var ret = "";
            foreach (var item in this)
            {
                ret += item;
            }
            return ret;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetList()
        {
            var list = new List<string>();

            list.Add(this.m_AchievementManagerSerialized);
            list.Add(this.m_AuroraManagerSerialized);
            list.Add(this.m_BearHuntSerialized);
            list.Add(this.m_BloodLossSerialized);
            list.Add(this.m_BrokenRibSerialized);
            list.Add(this.m_BurnsElectricSerialized);
            list.Add(this.m_BurnsSerialized);
            list.Add(this.m_CabinFeverSerialized);
            list.Add(this.m_ChimneyDataSerialized);
            list.Add(this.m_CollectionManagerSerialized);
            list.Add(this.m_Condition_Serialized);
            list.Add(this.m_CurrentEpisodeComplete.ToString());
            list.Add(this.m_DysenterySerialized);
            list.Add(this.m_EmergencyStimSerialized);
            list.Add(this.m_Encumber_Serialized);
            list.Add(this.m_ExperienceModeManagerSerialized);
            list.Add(this.m_Fatigue_Serialized);
            list.Add(this.m_FeatsEnabledSerialized);
            list.Add(this.m_FlyOverSerialized);
            list.Add(this.m_FoodPoisoningSerialized);
            list.Add(this.m_Freezing_Serialized);
            list.Add(this.m_FrostbiteSerialized);
            list.Add(this.m_GameManagerSerialized);
            list.Add(this.m_HudManagerSerialized);
            list.Add(this.m_Hunger_Serialized);
            list.Add(this.m_HypothermiaSerialized);
            list.Add(this.m_InfectionRiskSerialized);
            list.Add(this.m_InfectionSerialized);
            list.Add(this.m_IntestinalParasitesSerialized);
            list.Add(this.m_Inventory_Serialized);
            list.Add(this.m_KnowledgeManagerSerialized);
            list.Add(this.m_LockCompanionsSerialized);
            list.Add(this.m_LogSerialized);
            list.Add(this.m_MapDataSerialized);
            list.Add(this.m_MapDetailManagerSerialized);
            list.Add(this.m_MusicEventManagerSerialized);
            list.Add(this.m_PanelStatsSerialized);
            list.Add(this.m_PlayerAnimationSerialized);
            list.Add(this.m_PlayerClimbRopeSerialized);
            list.Add(this.m_PlayerGameStatsSerialized);
            list.Add(this.m_PlayerManagerSerialized);
            list.Add(this.m_PlayerMovementSerialized);
            list.Add(this.m_PlayerSkillsSerialized);
            list.Add(this.m_PlayerStruggleSerialized);
            list.Add(this.m_RestSerialized);
            list.Add(this.m_SandboxManagerSerialized);
            list.Add(this.m_SkillsManagerSerialized);
            list.Add(this.m_SnowPatchManagerSerialized);
            list.Add(this.m_SprainedAnkleSerialized);
            list.Add(this.m_SprainedWristMajorSerialized);
            list.Add(this.m_SprainedWristSerialized);
            list.Add(this.m_StoryManagerSerialized);
            list.Add(this.m_StoryMissionDataSerialized);
            list.Add(this.m_Thirst_Serialized);
            list.Add(this.m_TimeOfDay_Serialized);
            list.Add(this.m_TrustManagerSerialized);
            list.Add(this.m_UnlockedBlueprintsSerialized);
            list.Add(this.m_Version.ToString());
            list.Add(this.m_WeatherTransition_Serialized);
            list.Add(this.m_Weather_Serialized);
            list.Add(this.m_Willpower_Serialized);
            list.Add(this.m_Wind_Serialized);
            list.Add(this.m_WorldMapDataSerialized);

            return list;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetList().GetEnumerator();
        }

        #endregion
    }
}