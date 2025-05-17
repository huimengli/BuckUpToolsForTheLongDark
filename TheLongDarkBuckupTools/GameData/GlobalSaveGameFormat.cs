using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLongDarkBuckupTools.Serialization;

namespace TheLongDarkBuckupTools.GameData
{
    public class GlobalSaveGameFormat
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public int m_Version { get; set; }

        /// <summary>
        /// 游戏管理器序列化数据
        /// </summary>
        [Deserialize("m_GameManagerSerialized", true)]
        public GameManagerData GameManagerData { get; set; }

        /// <summary>
        /// HUD管理器序列化数据
        /// </summary>
        [Deserialize("m_HudManagerSerialized", true)]
        public HudManagerSaveDataProxy HudManager { get; set; }

        /// <summary>
        /// 时间系统序列化数据
        /// </summary>
        [Deserialize("m_TimeOfDay_Serialized", true)]
        public TimeOfDaySaveDataProxy TimeOfDay { get; set; }

        /// <summary>
        /// 风力系统序列化数据
        /// </summary>
        [Deserialize("m_Wind_Serialized", true)]
        public WindSaveDataProxy Wind { get; set; }

        /// <summary>
        /// 天气系统序列化数据
        /// </summary>
        [Deserialize("m_Weather_Serialized", true)]
        public WeatherSaveDataProxy Weather { get; set; }

        /// <summary>
        /// 天气过渡序列化数据
        /// </summary>
        [Deserialize("m_WeatherTransition_Serialized", true)]
        public WeatherTransitionSaveDataProxy WeatherTransition { get; set; }

        /// <summary>
        /// 角色状态序列化数据
        /// </summary>
        [Deserialize("m_Condition_Serialized", true)]
        public ConditionSaveDataProxy Condition { get; set; }

        /// <summary>
        /// 负重系统序列化数据
        /// </summary>
        [Deserialize("m_Encumber_Serialized", true)]
        public EncumberSaveDataProxy Encumber { get; set; }

        /// <summary>
        /// 饥饿系统序列化数据
        /// </summary>
        [Deserialize("m_Hunger_Serialized", true)]
        public HungerSaveDataProxy Hunger { get; set; }

        /// <summary>
        /// 口渴系统序列化数据
        /// </summary>
        [Deserialize("m_Thirst_Serialized", true)]
        public ThirstSaveDataProxy Thirst { get; set; }

        /// <summary>
        /// 疲劳系统序列化数据
        /// </summary>
        [Deserialize("m_Fatigue_Serialized", true)]
        public FatigueSaveDataProxy Fatigue { get; set; }

        /// <summary>
        /// 冷冻系统序列化数据
        /// </summary>
        [Deserialize("m_Freezing_Serialized", true)]
        public FreezingSaveDataProxy Freezing { get; set; }

        /// <summary>
        /// 意志力系统序列化数据
        /// </summary>
        [Deserialize("m_Willpower_Serialized", true)]
        public WillpowerSaveDataProxy WillPower { get; set; }

        /// <summary>
        /// 物品栏序列化数据
        /// </summary>
        [Deserialize("m_Inventory_Serialized", true)]
        public InventorySaveDataProxy Inventory { get; set; }

        /// <summary>
        /// 沙盒管理器序列化数据
        /// </summary>
        public string m_SandboxManagerSerialized { get; set; }

        /// <summary>
        /// 故事管理器序列化数据
        /// </summary>
        public string m_StoryManagerSerialized { get; set; }

        /// <summary>
        /// 玩家管理器序列化数据
        /// </summary>
        [Deserialize("m_PlayerManagerSerialized", true)]
        public PlayerManagerSaveDataProxy PlayerManager { get; set; }

        /// <summary>
        /// 玩家攀爬绳索序列化数据
        /// </summary>
        [Deserialize("m_PlayerClimbRopeSerialized", true)]
        public PlayerClimbRopeProxy PlayerClimbRope { get; set; }

        /// <summary>
        /// 玩家技能序列化数据
        /// </summary>
        [Deserialize("m_PlayerSkillsSerialized", true)]
        public PlayerSkillsSaveData PlayerSkills { get; set; }

        /// <summary>
        /// 玩家游戏统计序列化数据
        /// </summary>
        [Deserialize("m_PlayerGameStatsSerialized", true)]
        public PlayerGameStatsProxy PlayerGameStats { get; set; }

        /// <summary>
        /// 低温症系统序列化数据
        /// </summary>
        [Deserialize("m_HypothermiaSerialized", true)]
        public HypothermiaSaveDataProxy Hypothermia { get; set; }

        /// <summary>
        /// 饱食状态序列化数据
        /// </summary>
        [Deserialize("m_WellFedSerialized", true)]
        public WellFedSaveDataProxy WellFed { get; set; }

        /// <summary>
        /// 冻伤系统序列化数据
        /// </summary>
        [Deserialize("m_FrostbiteSerialized", true)]
        public FrostbiteSaveDataProxy FrostBite { get; set; }

        /// <summary>
        /// 食物中毒系统序列化数据
        /// </summary>
        [Deserialize("m_FoodPoisoningSerialized", true)]
        public FoodPoisoningSaveDataProxy FoodPoisoning { get; set; }

        /// <summary>
        /// 痢疾系统序列化数据
        /// </summary>
        [Deserialize("m_DysenterySerialized", true)]
        public DysenterySaveDataProxy Dysentery { get; set; }

        /// <summary>
        /// 脚踝扭伤系统序列化数据
        /// </summary>
        [Deserialize("m_SprainedAnkleSerialized", true)]
        public SprainedAnkleSaveDataProxy SprainedAnkle { get; set; }

        /// <summary>
        /// 手腕扭伤系统序列化数据
        /// </summary>
        [Deserialize("m_SprainedWristSerialized", true)]
        public SprainedWristSaveDataProxy SprainedWrist { get; set; }

        /// <summary>
        /// 烧伤系统序列化数据
        /// </summary>
        [Deserialize("m_BurnsSerialized", true)]
        public BurnsSaveDataProxy Burns { get; set; }

        /// <summary>
        /// 电击烧伤系统序列化数据
        /// </summary>
        [Deserialize("m_BurnsElectricSerialized", true)]
        public BurnsElectricSaveDataProxy BurnsElectric { get; set; }

        /// <summary>
        /// 失血系统序列化数据
        /// </summary>
        [Deserialize("m_BloodLossSerialized", true)]
        public BloodLossSaveDataProxy BloodLoss { get; set; }

        /// <summary>
        /// 肋骨骨折系统序列化数据
        /// </summary>
        [Deserialize("m_BrokenRibSerialized", true)]
        public BrokenRibSaveDataProxy BrokenRibs { get; set; }

        /// <summary>
        /// 感染系统序列化数据
        /// </summary>
        [Deserialize("m_InfectionSerialized", true)]
        public InfectionSaveDataProxy Infection { get; set; }

        /// <summary>
        /// 感染风险系统序列化数据
        /// </summary>
        [Deserialize("m_InfectionRiskSerialized", true)]
        public InfectionRiskSaveDataProxy InfectionRisk { get; set; }

        /// <summary>
        /// 日志系统序列化数据
        /// </summary>
        [Deserialize("m_LogSerialized", true)]
        public LogSaveDataProxy Log { get; set; }

        /// <summary>
        /// 休息系统序列化数据
        /// </summary>
        [Deserialize("m_RestSerialized", true)]
        public RestSaveDataProxy Rest { get; set; }

        /// <summary>
        /// 过场动画序列化数据
        /// </summary>
        [Deserialize("m_FlyOverSerialized", true)]
        public FlyoverDataProxy FlyOver { get; set; }

        /// <summary>
        /// 成就管理器序列化数据
        /// </summary>
        [Deserialize("m_AchievementManagerSerialized", true)]
        public AchievementSaveData AchievementManager { get; set; }

        /// <summary>
        /// 体验模式管理器序列化数据
        /// </summary>
        [Deserialize("m_ExperienceModeManagerSerialized", true)]
        public ExperienceModeManagerSaveDataProxy ExperienceModeManager { get; set; }

        /// <summary>
        /// 玩家移动系统序列化数据
        /// </summary>
        [Deserialize("m_PlayerMovementSerialized", true)]
        public PlayerMovementSaveDataProxy PlayerMovement { get; set; }

        /// <summary>
        /// 玩家挣扎系统序列化数据
        /// </summary>
        public string m_PlayerStruggleSerialized { get; set; }

        /// <summary>
        /// 面板统计序列化数据
        /// </summary>
        public string m_PanelStatsSerialized { get; set; }

        /// <summary>
        /// 紧急刺激剂参数序列化数据
        /// </summary>
        [Deserialize("m_EmergencyStimSerialized", true)]
        public EmergencyStimParams EmergencyStim { get; set; }

        /// <summary>
        /// 音乐事件管理器序列化数据
        /// </summary>
        public string m_MusicEventManagerSerialized { get; set; }

        /// <summary>
        /// 烟囱数据序列化数据
        /// </summary>
        public string m_ChimneyDataSerialized { get; set; }

        /// <summary>
        /// 幽居病系统序列化数据
        /// </summary>
        [Deserialize("m_CabinFeverSerialized", true)]
        public CabinFeverSaveDataProxy CabinFever { get; set; }

        /// <summary>
        /// 肠道寄生虫系统序列化数据
        /// </summary>
        [Deserialize("m_IntestinalParasitesSerialized", true)]
        public IntestinalParasitesSaveDataProxy IntestinalParasites { get; set; }

        /// <summary>
        /// 雪地补丁管理器序列化数据
        /// </summary>
        public string m_SnowPatchManagerSerialized { get; set; }

        /// <summary>
        /// 玩家动画序列化数据
        /// </summary>
        public string m_PlayerAnimationSerialized { get; set; }

        /// <summary>
        /// 技能管理器序列化数据
        /// </summary>
        [Deserialize("m_SkillsManagerSerialized", true)]
        public SkillsManagerSaveData SkillsManager { get; set; }

        /// <summary>
        /// 锁定同伴序列化数据
        /// </summary>
        public string m_LockCompanionsSerialized { get; set; }

        /// <summary>
        /// 已启用特性追踪序列化数据
        /// </summary>
        [Deserialize("m_FeatsEnabledSerialized", true)]
        public FeatEnabledTrackerSaveData FeatsEnabled { get; set; }

        /*/// <summary>
        /// 信任管理器序列化数据
        /// </summary>
        public string m_TrustManagerSerialized { get; set; }
        
        /// <summary>
        /// 世界地图数据序列化数据
        /// </summary>
        public string m_WorldMapDataSerialized { get; set; }
        
        /// <summary>
        /// 地图数据序列化数据
        /// </summary>
        public string m_MapDataSerialized { get; set; }
        
        /// <summary>
        /// 熊猎系统序列化数据
        /// </summary>
        public string m_BearHuntSerialized { get; set; }
        
        /// <summary>
        /// 熊猎重置系统序列化数据
        /// </summary>
        public string m_BearHuntReduxSerialized { get; set; }
        
        /// <summary>
        /// 知识管理器序列化数据
        /// </summary>
        public string m_KnowledgeManagerSerialized { get; set; }
        
        /// <summary>
        /// 已解锁蓝图序列化数据
        /// </summary>
        public string m_UnlockedBlueprintsSerialized { get; set; }
        
        /// <summary>
        /// 收藏管理器序列化数据
        /// </summary>
        public string m_CollectionManagerSerialized { get; set; }
        
        /// <summary>
        /// 极光屏幕管理器序列化数据
        /// </summary>
        public string m_AuroraScreenManagerSerialized { get; set; }
        
        /// <summary>
        /// 故事任务数据序列化数据
        /// </summary>
        public string m_StoryMissionDataSerialized { get; set; }
        
        /// <summary>
        /// 当前章节是否完成
        /// </summary>
        public bool m_CurrentEpisodeComplete { get; set; }*/
    }
}