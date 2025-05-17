using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLongDarkBuckupTools.Helpers;
using TheLongDarkBuckupTools.Serialization;

namespace TheLongDarkBuckupTools.GameData
{
    /// <summary>
    /// 游戏管理器数据类
    /// </summary>
    public class GameManagerData
    {
        /// <summary>
        /// 是否禁止休息功能
        /// </summary>
        public bool m_BlockAbilityToRest { get; set; }

        /// <summary>
        /// 禁止休息的位置标识ID
        /// </summary>
        public string m_BlockedRestLocID { get; set; }

        /// <summary>
        /// 场景过渡数据(使用自定义反序列化)
        /// </summary>
        [Deserialize("m_SceneTransitionDataSerialized", true)]
        public SceneTransitionData SceneTransition { get; set; }
    }


    /// <summary>
    /// 场景过渡数据类
    /// </summary>
    public class SceneTransitionData
    {
        /// <summary>
        /// 是否传送玩家到存档位置
        /// </summary>
        public bool m_TeleportPlayerSaveGamePosition { get; set; }

        /// <summary>
        /// 重生点名称
        /// </summary>
        public string m_SpawnPointName { get; set; }

        /// <summary>
        /// 强制加载的下个导航地图场景
        /// </summary>
        public string m_ForceSceneOnNextNavMapLoad { get; set; }

        /// <summary>
        /// 强制触发的下个场景加载场景
        /// </summary>
        public string m_ForceNextSceneLoadTriggerScene { get; set; }

        /// <summary>
        /// 进入室内场景前的位置坐标(x,y,z)
        /// </summary>
        public float[] m_PosBeforeInteriorLoad { get; set; }

        /// <summary>
        /// 当前场景存档文件名
        /// </summary>
        public string m_SceneSaveFilenameCurrent { get; set; }

        /// <summary>
        /// 下次加载的场景存档文件名
        /// </summary>
        public string m_SceneSaveFilenameNextLoad { get; set; }

        /// <summary>
        /// 待显示的场景位置本地化ID
        /// </summary>
        public string m_SceneLocationLocIDToShow { get; set; }

        /// <summary>
        /// 游戏随机种子
        /// </summary>
        public int m_GameRandomSeed { get; set; }

        /// <summary>
        /// 当前位置标识
        /// </summary>
        public string m_Location { get; set; }

        /// <summary>
        /// 最后记录的室外场景
        /// </summary>
        public string m_LastOutdoorScene { get; set; }
    }


    /// <summary>
    /// HUD管理器保存数据代理类
    /// </summary>
    public class HudManagerSaveDataProxy
    {
        /// <summary>
        /// 是否显示调试信息
        /// </summary>
        public bool m_ShowDebugInfo { get; set; }
    }


    /// <summary>
    /// 时间系统数据代理类
    /// </summary>
    public class TimeOfDaySaveDataProxy
    {
        /// <summary>
        /// 当前游戏时间（小时制浮点数）
        /// </summary>
        public float m_TimeProxy { get; set; }

        /// <summary>
        /// 实际游玩时长（暂停不计时）
        /// </summary>
        public float m_HoursPlayedNotPausedProxy { get; set; }

        /// <summary>
        /// UniStorm天气系统的累计天数
        /// </summary>
        public int m_UniStormDayCounterProxy { get; set; }

        /// <summary>
        /// UniStorm月相索引
        /// </summary>
        public int m_UniStormMoonPhaseIndexProxy { get; set; }

        /// <summary>
        /// UniStorm当前日期编号
        /// </summary>
        public int m_UniStormDayNumberProxy { get; set; }

        /// <summary>
        /// 上次播放黎明音效的日期
        /// </summary>
        public int m_DayLastDawnStingerAudioPlayed { get; set; }

        /// <summary>
        /// 上次播放夜晚音效的日期
        /// </summary>
        public int m_DayLastNightStingerAudioPlayed { get; set; }

        /// <summary>
        /// 上次播放黎明语音的日期
        /// </summary>
        public int m_DayLastDawnVoiceOverPlayed { get; set; }

        /// <summary>
        /// 上次播放夜晚语音的日期
        /// </summary>
        public int m_DayLastNightVoiceOverPlayed { get; set; }

        /// <summary>
        /// 4天生存挑战的当前天数
        /// </summary>
        public int m_4DONCurrentDay { get; set; }

        /// <summary>
        /// 是否锁定时间系统
        /// </summary>
        public bool m_LockedTOD { get; set; }
    }


    /// <summary>
    /// 风力系统数据代理类
    /// </summary>
    public class WindSaveDataProxy
    {
        /// <summary>
        /// 数据版本号
        /// </summary>
        public int m_Version { get; set; }

        /// <summary>
        /// 当前风向代理（使用枚举包装类）
        /// </summary>
        public EnumWrapper<WindDirection> m_windDirectionProxy { get; set; }

        /// <summary>
        /// 当前风力强度代理（使用枚举包装类）
        /// </summary>
        public EnumWrapper<WindStrength> m_windStrengthProxy { get; set; }

        /// <summary>
        /// 当前风速（英里/小时）
        /// </summary>
        public float m_windMPHProxy { get; set; }

        /// <summary>
        /// 是否已设置初始阶段
        /// </summary>
        public bool m_FirstPhaseSetProxy { get; set; }

        /// <summary>
        /// 当前阶段已持续时间（秒）
        /// </summary>
        public float m_PhaseElapsedTODSecondsProxy { get; set; }

        /// <summary>
        /// 当前阶段总持续时间（小时）
        /// </summary>
        public float m_PhaseDurationHoursProxy { get; set; }

        /// <summary>
        /// 过渡时间（秒）
        /// </summary>
        public float m_TransitionTimeTODSecondsProxy { get; set; }

        /// <summary>
        /// 当前激活的风力设置（序列化字符串）
        /// </summary>
        public string m_ActiveSettingsSerialized { get; set; }

        /// <summary>
        /// 源风力设置（序列化字符串）
        /// </summary>
        public string m_SourceSettingsSerialized { get; set; }

        /// <summary>
        /// 目标风力设置（序列化字符串）
        /// </summary>
        public string m_TargetSettingsSerialized { get; set; }
    }


    /// <summary>
    /// 活动风力设置类，包含风力系统的各项参数配置
    /// </summary>
    public class ActiveWindSettings
    {
        /// <summary>
        /// 风向角度（0-360度）
        /// </summary>
        public float m_Angle { get; set; }

        /// <summary>
        /// 风速（单位：米/秒）
        /// </summary>
        public float m_Velocity { get; set; }

        /// <summary>
        /// 阵风强度（0-1表示强度比例）
        /// </summary>
        public float m_Gustiness { get; set; }

        /// <summary>
        /// 侧向风力强度（影响物体左右摇晃程度）
        /// </summary>
        public float m_LateralBluster { get; set; }

        /// <summary>
        /// 垂直风力强度（影响物体上下浮动程度）
        /// </summary>
        public float m_VerticalBluster { get; set; }
    }



    /// <summary>
    /// 天气系统数据代理类，用于保存和代理天气相关数据
    /// </summary>
    public class WeatherSaveDataProxy
    {
        /// <summary>
        /// 上一帧记录的体温值（摄氏度）
        /// </summary>
        public float m_PrevBodyTempProxy { get; set; }

        /// <summary>
        /// 当日最高气温（摄氏度）
        /// </summary>
        public float m_TempHighProxy { get; set; }

        /// <summary>
        /// 当日最低气温（摄氏度）
        /// </summary>
        public float m_TempLowProxy { get; set; }

        /// <summary>
        /// 当前天气阶段（使用枚举包装类）
        /// </summary>
        public EnumWrapper<WeatherStage> m_WeatherStageProxy { get; set; }

        /// <summary>
        /// UniStorm系统已运行时长（小时）
        /// </summary>
        public float m_UniStormElapsedHoursProxy { get; set; }

        /// <summary>
        /// 距离下次天气变化的剩余时间（小时）
        /// </summary>
        public float m_UniStormNextWeatherChangeElapsedHoursProxy { get; set; }

        /// <summary>
        /// 是否使用最低气温限制
        /// </summary>
        public bool m_UseMinAirTemperature { get; set; }

        /// <summary>
        /// 最低气温阈值（摄氏度）
        /// </summary>
        public int m_MinAirTemperature { get; set; }
    }


    /// <summary>
    /// 天气过渡状态数据代理类
    /// 用于保存和管理天气系统过渡期间的中间状态数据
    /// </summary>
    public class WeatherTransitionSaveDataProxy
    {
        /// <summary>
        /// 是否使用非托管天气阶段（绕过系统自动管理）
        /// </summary>
        public bool m_UseUnmanagedWeatherStage;

        /// <summary>
        /// 手动指定的非托管天气阶段（当m_UseUnmanagedWeatherStage为true时生效）
        /// </summary>
        public EnumWrapper<WeatherStage> m_UnmanagedWeatherStage;

        /// <summary>
        /// 当前天气配置名称标识
        /// </summary>
        public string m_CurrentWeatherSetName { get; set; }

        /// <summary>
        /// 当前天气过渡进度（0-1表示完成百分比）
        /// </summary>
        public float m_CurrentWeatherSetProgressFrac { get; set; }

        /// <summary>
        /// 当前天气配置序列化数据（JSON/二进制格式）
        /// </summary>
        public string m_CurrentWeatherSetSerialized { get; set; }

        /// <summary>
        /// 当前天气配置类型标识符
        /// </summary>
        public int m_CurrentWeatherSetType { get; set; }

        /// <summary>
        /// 先前天气配置类型标识符（用于回滚/过渡参考）
        /// </summary>
        public int m_PreviousWeatherSetType { get; set; }
    }


    /// <summary>
    /// 天气集合实例保存数据类
    /// 用于记录天气集合实例的运行状态和时序信息
    /// </summary>
    public class WeatherSetInstanceSaveData
    {
        /// <summary>
        /// 当前天气阶段的索引值
        /// </summary>
        public int m_CurrentIndex { get; set; }

        /// <summary>
        /// 当前天气阶段已持续时间（单位：秒）
        /// </summary>
        public float m_CurrentStageElapsedTime { get; set; }

        /// <summary>
        /// 各天气阶段的持续时间数组（单位：秒）
        /// </summary>
        public float[] m_StageDurations { get; set; }

        /// <summary>
        /// 各天气阶段之间的过渡时间数组（单位：秒）
        /// </summary>
        public float[] m_StageTransitionTimes { get; set; }
    }



    /// <summary>
    /// 角色状态数据代理类
    /// 采用代理模式封装角色生命状态和语音控制逻辑
    /// </summary>
    public class ConditionSaveDataProxy
    {
        /// <summary>
        /// 当前生命值（百分比0-1）
        /// </summary>
        public float m_CurrentHPProxy { get; set; }

        /// <summary>
        /// 距离上次语音播报的间隔秒数
        /// </summary>
        public float m_NumSecondsSinceLastVoiceOver { get; set; }

        /// <summary>
        /// 不死模式开关（调试用）
        /// </summary>
        public bool m_NeverDieProxy { get; set; }

        /// <summary>
        /// 无敌状态标记（免疫伤害）
        /// </summary>
        public bool m_Invulnerable { get; set; }

        /// <summary>
        /// 是否隐藏伤害事件提示
        /// </summary>
        public bool m_HideDamageEvents { get; set; }

        /// <summary>
        /// 强制解除蹲伏状态
        /// </summary>
        public bool m_FoceUncrouched { get; set; }

        /// <summary>
        /// 是否允许播放濒死音乐
        /// </summary>
        public bool m_CanPlayNearDeathMusic { get; set; }

        /// <summary>
        /// 上次语音播报时的状态等级（枚举包装类）
        /// </summary>
        public EnumWrapper<ConditionLevel> m_ConditionLevelForPreviousVoiceOver { get; set; }

        /// <summary>
        /// 语音播报抑制标记（紧急状态时禁用语音）
        /// </summary>
        public bool m_SuppressVoiceOver { get; set; }
    }


    /// <summary>
    /// 负重状态数据代理类
    /// 用于管理角色负重状态及相关的语音提示逻辑
    /// </summary>
    public class EncumberSaveDataProxy
    {
        /// <summary>
        /// 日志中是否标记为负重状态
        /// </summary>
        public bool m_EncumberedInLog { get; set; }

        /// <summary>
        /// 距离上次语音提示的间隔时间（单位：秒）
        /// </summary>
        public float m_NumSecondsSinceLastVoiceOver { get; set; }

        /// <summary>
        /// 上次语音提示时的负重等级（枚举包装类）
        /// 注意：属性名拼写应为m_EncumberLevelForPreviousVoiceOver
        /// </summary>
        public EnumWrapper<EncumberLevel> m_EcumberLevelForPreviousVoiceOver { get; set; }
    }


    /// <summary>
    /// 饥饿状态数据代理类
    /// 管理角色营养状态及饥饿相关游戏机制
    /// </summary>
    public class HungerSaveDataProxy
    {
        /// <summary>
        /// 当前储备卡路里量（单位：千卡）
        /// 影响角色行动能力和生命值恢复:ml-citation{ref="1,4" data="citationList"}
        /// </summary>
        public float m_CurrentReserveCaloriesProxy { get; set; }

        /// <summary>
        /// 距离上次语音提示的间隔时间（单位：秒）
        /// 用于控制饥饿状态语音频率:ml-citation{ref="2,3" data="citationList"}
        /// </summary>
        public float m_NumSecondsSinceLastVoiceOver { get; set; }

        /// <summary>
        /// 日志中是否标记为饥饿状态
        /// 用于任务系统判定和成就解锁:ml-citation{ref="5" data="citationList"}
        /// </summary>
        public bool m_StarvingInLog { get; set; }

        /// <summary>
        /// 持续饥饿时间（单位：小时）
        /// 影响角色属性衰减曲线:ml-citation{ref="1,4" data="citationList"}
        /// </summary>
        public float m_NumHoursStarving { get; set; }

        /// <summary>
        /// 疲劳惩罚系数（0-1范围）
        /// 作用于移动速度和攻击力等属性:ml-citation{ref="7" data="citationList"}
        /// </summary>
        public float m_FatiguePenalty { get; set; }

        /// <summary>
        /// 上次语音提示时的饥饿等级
        /// 用于状态变化时的语音差异化处理:ml-citation{ref="2,3" data="citationList"}
        /// </summary>
        public EnumWrapper<HungerLevel> m_HungerLevelForPreviousVoiceOver { get; set; }

        /// <summary>
        /// 今日累计摄入卡路里量
        /// 用于营养平衡统计和成就系统:ml-citation{ref="1,4" data="citationList"}
        /// </summary>
        public float m_CaloriesEatenToday { get; set; }

        /// <summary>
        /// 语音提示抑制标记
        /// 紧急状态下临时禁用饥饿提示:ml-citation{ref="3" data="citationList"}
        /// </summary>
        public bool m_SuppressVoiceOver { get; set; }
    }



    /// <summary>
    /// 饥渴状态数据代理类
    /// 管理角色水分状态及相关游戏机制
    /// </summary>
    public class ThirstSaveDataProxy
    {
        /// <summary>
        /// 当前饥渴值（0-1范围）
        /// 0表示完全缺水，1表示水分充足
        /// </summary>
        public float m_CurrentThirstProxy { get; set; }

        /// <summary>
        /// 距离上次语音提示的间隔秒数
        /// 用于控制饥渴提示频率
        /// </summary>
        public float m_NumSecondsSinceLastVoiceOver { get; set; }

        /// <summary>
        /// 日志中是否标记为脱水状态
        /// 影响角色属性和任务判定
        /// </summary>
        public bool m_DehydratedInLog { get; set; }

        /// <summary>
        /// 上次语音提示时的饥渴等级
        /// 用于状态变化时的差异化处理
        /// </summary>
        public EnumWrapper<ThirstLevel> m_ThirstLevelForPreviousVoiceOver { get; set; }

        /// <summary>
        /// 语音提示抑制标记
        /// 紧急状态下临时禁用饥渴提示
        /// </summary>
        public bool m_SuppressVoiceOver { get; set; }
    }



    /// <summary>
    /// 疲劳状态数据代理类
    /// 管理角色疲劳状态及相关游戏机制
    /// </summary>
    public class FatigueSaveDataProxy
    {
        /// <summary>
        /// 当前疲劳值（0-1范围）
        /// 0表示精力充沛，1表示完全疲劳
        /// </summary>
        public float m_CurrentFatigueProxy { get; set; }

        /// <summary>
        /// 距离上次语音提示的间隔秒数
        /// 用于控制疲劳提示频率
        /// </summary>
        public float m_NumSecondsSinceLastVoiceOver { get; set; }

        /// <summary>
        /// 日志中是否标记为精疲力竭状态
        /// 影响角色属性和任务判定
        /// </summary>
        public bool m_ExhaustedInLog { get; set; }

        /// <summary>
        /// 上次语音提示时的疲劳等级
        /// 用于状态变化时的差异化处理
        /// </summary>
        public EnumWrapper<FatigueLevel> m_FatigueLevelForPreviousVoiceOver { get; set; }

        /// <summary>
        /// 语音提示抑制标记
        /// 战斗等特殊状态下临时禁用疲劳提示
        /// </summary>
        public bool m_SuppressVoiceOver { get; set; }
    }



    /// <summary>
    /// 寒冷状态数据代理类
    /// 管理角色体温状态及相关游戏机制
    /// </summary>
    public class FreezingSaveDataProxy
    {
        /// <summary>
        /// 当前寒冷值（0-1范围）
        /// 0表示温暖舒适，1表示严重失温
        /// </summary>
        public float m_CurrentFreezingProxy { get; set; }

        /// <summary>
        /// 距离上次语音提示的间隔秒数
        /// 用于控制寒冷提示频率
        /// </summary>
        public float m_NumSecondsSinceLastVoiceOver { get; set; }

        /// <summary>
        /// 日志中是否标记为失温状态
        /// 影响角色属性和任务判定
        /// </summary>
        public bool m_FreezingInLog { get; set; }

        /// <summary>
        /// 上次语音提示时的寒冷等级
        /// 用于状态变化时的差异化处理
        /// </summary>
        public EnumWrapper<FreezingLevel> m_FreezingLevelForPreviousVoiceOver { get; set; }

        /// <summary>
        /// 奔跑获得的热量加成（0-0.3范围）
        /// 动态影响寒冷值计算
        /// </summary>
        public float m_TemperatureBonusFromRunning { get; set; }

        /// <summary>
        /// 语音提示抑制标记
        /// 特殊状态下临时禁用寒冷提示
        /// </summary>
        public bool m_SuppressVoiceOver { get; set; }
    }


    /// <summary>
    /// 意志力状态数据代理类
    /// 管理角色意志力倒计时机制
    /// </summary>
    public class WillpowerSaveDataProxy
    {
        /// <summary>
        /// 剩余意志力持续时间（秒）
        /// 0表示意志力耗尽，正值表示剩余持续时间
        /// 用于特殊技能/生存状态的倒计时机制
        /// </summary>
        public float m_TimeRemainingSecondsProxy { get; set; }
    }


    /// <summary>
    /// 知识管理系统存档数据类
    /// 管理游戏中的知识体系、信任关系和故事进度状态
    /// </summary>
    public class KnowledgeManagerSaveData
    {
        /// <summary>
        /// 序列化的信任关系字典（JSON格式）
        /// 存储NPC之间的信任度数据
        /// </summary>
        public string m_TrustDictSerialized { get; set; }

        /// <summary>
        /// 序列化的知识字典（JSON格式）
        /// 存储玩家已解锁的知识条目
        /// </summary>
        public string m_KnowledgeDictSerialized { get; set; }

        /// <summary>
        /// 序列化的名称引用字典（JSON格式）
        /// 用于本地化文本的动态替换
        /// </summary>
        public string m_NameRefDictSerialized { get; set; }

        /// <summary>
        /// 雪地庇护所是否在剧情中解锁
        /// 关键剧情进度标记
        /// </summary>
        public bool m_SnowSheltersUnlockedInStory { get; set; }
    }


    /// <summary>
    /// 收藏品管理系统存档数据类
    /// 管理游戏中各类收集品的解锁状态
    /// </summary>
    public class CollectionManagerSaveData
    {
        /// <summary>
        /// 序列化的石堆标记解锁字典（JSON格式）
        /// 存储玩家已发现的导航石堆位置和状态
        /// </summary>
        public string m_UnlockedCairnsDictSerialized { get; set; }

        /// <summary>
        /// 序列化的极光套装解锁集合（JSON格式）
        /// 存储玩家已收集的极光主题装备/皮肤
        /// </summary>
        public string m_UnlockedAuroraSetSerialized { get; set; }
    }


    #region Inventory

    /// <summary>
    /// 库存系统存档数据代理类
    /// 管理玩家物品栏状态和特殊消耗品标记
    /// </summary>
    [Serializable]
    public class InventorySaveDataProxy
    {
        /// <summary>
        /// 物品集合（带反序列化映射）
        /// 使用ObservableCollection实现数据变更通知
        /// </summary>
        [Deserialize("m_SerializedItems")]
        public ObservableCollection<InventoryItemSaveData> Items { get; set; } = new ObservableCollection<InventoryItemSaveData>();

        /// <summary>
        /// 快捷栏物品实例ID数组
        /// 对应玩家快捷栏槽位的物品引用
        /// </summary>
        public int[] m_QuickSelectInstanceIDs { get; set; } = Array.Empty<int>();

        /// <summary>
        /// 重量强制覆盖标记
        /// 用于调试或特殊游戏模式
        /// </summary>
        public bool m_ForceOverrideWeight { get; set; }

        /// <summary>
        /// 覆盖后的重量值（千克）
        /// 当m_ForceOverrideWeight为true时生效
        /// </summary>
        public float m_OverridedWeight { get; set; }

        // 以下为消耗品使用状态标记
        /// <summary>
        /// 消耗咖啡状态标记
        /// </summary>
        public bool m_ConsumedCoffee { get; set; }
        /// <summary>
        /// 消耗玫瑰果茶状态标记
        /// </summary>
        public bool m_ConsumedRosehipTea { get; set; }
        /// <summary>
        /// 消耗枫灵芝茶状态标记
        /// </summary>
        public bool m_ConsumedReishiTea { get; set; }
        /// <summary>
        /// 消耗胡须地衣状态标记
        /// </summary>
        public bool m_ConsumedOldMansBeardDressing { get; set; }

        /// <summary>
        /// 是否隐藏气味指示器
        /// 用于特殊道具或技能效果
        /// </summary>
        public bool m_SuppressScentIndicator { get; set; }
    }


    /// <summary>
    /// 库存物品存档数据类
    /// 存储单个物品的持久化数据和运行时属性
    /// </summary>
    [Serializable]
    public class InventoryItemSaveData
    {
        /// <summary>
        /// 预制体资源名称
        /// 用于从资源库加载对应物品
        /// </summary>
        public string m_PrefabName { get; set; }

        /// <summary>
        /// 装备物品数据代理（带反序列化映射）
        /// 使用自定义反序列化逻辑处理装备特殊属性
        /// </summary>
        [Deserialize("m_SerializedGear", true)]
        public GearItemSaveDataProxy Gear { get; set; }

        /// <summary>
        /// 物品分类（运行时动态获取）
        /// 通过物品字典查询分类避免数据冗余
        /// </summary>
        [JsonIgnore]
        public ItemCategory Category => ItemDictionary.GetCategory(m_PrefabName);

        /// <summary>
        /// 游戏内显示名称（运行时动态获取）
        /// 支持多语言本地化查询
        /// </summary>
        [JsonIgnore]
        public string InGameName => ItemDictionary.GetInGameName(m_PrefabName);
    }


    /// <summary>
    /// 装备物品保存数据代理类
    /// </summary>
    public class GearItemSaveDataProxy : BindableBase
    {
        /// <summary>
        /// 已游玩小时数
        /// </summary>
        public float m_HoursPlayed { get; set; }

        /// <summary>
        /// 物品位置坐标数组
        /// </summary>
        public float[] m_Position { get; set; }

        /// <summary>
        /// 物品旋转四元数数组
        /// </summary>
        public float[] m_Rotation { get; set; }

        /// <summary>
        /// 物品实例ID代理
        /// </summary>
        public int m_InstanceIDProxy { get; set; }

        /// <summary>
        /// 当前生命值代理
        /// </summary>
        public float m_CurrentHPProxy { get; set; }

        /// <summary>
        /// 标准化条件值
        /// </summary>
        public float m_NormalizedCondition;

        /// <summary>
        /// 标准化条件属性(Json忽略)
        /// </summary>
        [JsonIgnore]
        public float NormalizedCondition
        {
            get { return m_NormalizedCondition; }
            set
            {
                SetProperty(ref m_NormalizedCondition, value);
            }
        }

        /// <summary>
        /// 是否曾在玩家物品栏中
        /// </summary>
        public bool m_BeenInPlayerInventoryProxy { get; set; }

        /// <summary>
        /// 是否曾在容器中
        /// </summary>
        public bool m_BeenInContainerProxy { get; set; }

        /// <summary>
        /// 是否已被检查过
        /// </summary>
        public bool m_BeenInspectedProxy { get; set; }

        /// <summary>
        /// 物品是否被掠夺过
        /// </summary>
        public bool m_ItemLootedProxy { get; set; }

        /// <summary>
        /// 是否曾被玩家拥有过
        /// </summary>
        public bool m_HasBeenOwnedByPlayer { get; set; }

        /// <summary>
        /// 是否已滚动生成几率
        /// </summary>
        public bool m_RolledSpawnChanceProxy { get; set; }

        /// <summary>
        /// 是否已磨损
        /// </summary>
        public bool m_WornOut { get; set; }

        /// <summary>
        /// 可堆叠物品序列化数据
        /// </summary>
        [Deserialize("m_StackableItemSerialized", true)]
        public StackableItemSaveDataProxy StackableItem { get; set; }

        /// <summary>
        /// 食物物品序列化数据
        /// </summary>
        [Deserialize("m_FoodItemSerialized", true)]
        public FoodItemSaveDataProxy FoodItem { get; set; }

        /// <summary>
        /// 液体物品序列化数据
        /// </summary>
        [Deserialize("m_LiquidItemSerialized", true)]
        public LiquidItemSaveDataProxy LiquidItem { get; set; }

        /// <summary>
        /// 信号弹物品序列化数据
        /// </summary>
        [Deserialize("m_FlareItemSerialized", true)]
        public FlareItemSaveDataProxy FlareItem { get; set; }

        /// <summary>
        /// 手电筒物品序列化数据
        /// </summary>
        [Deserialize("m_FlashlightItemSerialized", true)]
        public FlashlightItemSaveDataProxy FlashLightItem { get; set; }

        /// <summary>
        /// 煤油灯物品序列化数据
        /// </summary>
        [Deserialize("m_KeroseneLampItemSerialized", true)]
        public KeroseneLampItemSaveDataProxy KeroseneLampItem { get; set; }

        /// <summary>
        /// 衣物物品序列化数据
        /// </summary>
        [Deserialize("m_ClothingItemSerialized", true)]
        public ClothingItemSaveDataProxy ClothingItem { get; set; }

        /// <summary>
        /// 武器物品序列化数据
        /// </summary>
        [Deserialize("m_WeaponItemSerialized", true)]
        public GunItemSaveDataProxy WeaponItem { get; set; }

        /// <summary>
        /// 水源供应序列化数据
        /// </summary>
        [Deserialize("m_WaterSupplySerialized", true)]
        public WaterSupplySaveDataProxy WaterSupply { get; set; }

        /// <summary>
        /// 床铺序列化数据
        /// </summary>
        [Deserialize("m_BedSerialized", true)]
        public BedSaveDataProxy Bed { get; set; }

        /// <summary>
        /// 可破坏物品序列化数据
        /// </summary>
        [Deserialize("m_SmashableItemSerialized", true)]
        public SmashableItemSaveDataProxy SmashableItem { get; set; }

        /// <summary>
        /// 火柴物品序列化数据
        /// </summary>
        [Deserialize("m_MatchesItemSerialized", true)]
        public MatchesItemSaveDataProxy MatchesItem { get; set; }

        /// <summary>
        /// 陷阱物品序列化数据
        /// </summary>
        [Deserialize("m_SnareItemSerialized", true)]
        public SnareItemSaveDataProxy SnareItem { get; set; }

        /// <summary>
        /// 喷漆罐序列化数据
        /// </summary>
        [Deserialize("m_SprayPaintCanSerialized", true)]
        public SprayPaintSaveDataProxy SprayItem { get; set; }

        /// <summary>
        /// 刚体序列化数据
        /// </summary>
        [Deserialize("m_RigidBodySerialized", true)]
        public RigidBodySaveDataProxy RigidBody { get; set; }

        /// <summary>
        /// 粉末物品序列化数据
        /// </summary>
        [Deserialize("m_PowderItemSerialized", true)]
        public PowderItemSaveDataProxy PowderItem { get; set; }

        /// <summary>
        /// 可研磨物品序列化数据
        /// </summary>
        [Deserialize("m_MillableSerialized", true)]
        public MillableSaveDataProxy Millable { get; set; }

        /// <summary>
        /// 制作中物品序列化数据
        /// </summary>
        [Deserialize("m_InProgressItemSerialized", true)]
        public InProgressCraftItemSaveDataProxy InProgressItem { get; set; }

        /// <summary>
        /// 火炬物品序列化数据
        /// </summary>
        [Deserialize("m_TorchItemSerialized", true)]
        public TorchItemSaveDataProxy TorchItem { get; set; }

        /// <summary>
        /// 可收集笔记序列化数据
        /// </summary>
        public string m_CollectibleNoteSerialized { get; set; }

        /// <summary>
        /// 进化物品序列化数据
        /// </summary>
        [Deserialize("m_EvolveItemSerialized", true)]
        public EvolveItemSaveData EvolveItem { get; set; }

        /// <summary>
        /// 对象GUID序列化数据
        /// </summary>
        public string m_ObjectGuidSerialized { get; set; }

        /// <summary>
        /// 任务对象序列化数据
        /// </summary>
        public string m_MissionObjectSerialized { get; set; }

        /// <summary>
        /// 研究物品序列化数据
        /// </summary>
        [Deserialize("m_ResearchItemSerialized", true)]
        public ResearchItemSaveData ResearchItem { get; set; }

        /// <summary>
        /// 物品重量(千克)
        /// </summary>
        public float m_WeightKG { get; set; }

        /// <summary>
        /// 是否被玩家收获
        /// </summary>
        public bool m_HarvestedByPlayer { get; set; }

        /// <summary>
        /// 是否在背包中
        /// </summary>
        public bool m_IsInSatchel { get; set; }

        /// <summary>
        /// 背包索引
        /// </summary>
        public int m_SatchelIndex { get; set; }

        /// <summary>
        /// 所有权覆盖序列化数据
        /// </summary>
        public string m_OwnershipOverrideSerialized { get; set; }

        /// <summary>
        /// 尸体收获序列化数据
        /// </summary>
        [Deserialize("m_BodyHarvestSerialized", true)]
        public BodyHarvestSaveDataProxy BodyHarvest { get; set; }

        /// <summary>
        /// 是否锁定在容器中
        /// </summary>
        public bool m_LockedInContainer { get; set; }

        /// <summary>
        /// 装备物品保存版本
        /// </summary>
        public int m_GearItemSaveVersion { get; set; }

        /// <summary>
        /// 烹饪锅物品序列化数据
        /// </summary>
        public string m_CookingPotItemSerialized { get; set; }

        /// <summary>
        /// 放置点GUID序列化数据
        /// </summary>
        public string m_PlacePointGuidSerialized { get; set; }

        /// <summary>
        /// 放置点名称序列化数据
        /// </summary>
        public string m_PlacePointNameSerialized { get; set; }

        /// <summary>
        /// 是否非交互式
        /// </summary>
        public bool m_NonInteractive { get; set; }

        /// <summary>
        /// 是否曾被装备并使用过
        /// </summary>
        public bool m_HasBeenEquippedAndUsed { get; set; }

        /// <summary>
        /// 检查序列化数据
        /// </summary>
        public string m_InspectSerialized { get; set; }

        /// <summary>
        /// 石头物品是否被投掷
        /// </summary>
        public bool m_StoneItemThrown { get; set; }

        ///// <summary>
        ///// 创建新的装备物品保存数据
        ///// </summary>
        ///// <returns>新创建的装备物品保存数据代理对象</returns>
        //public static GearItemSaveDataProxy Create()
        //{
        //    var item = new GearItemSaveDataProxy();
        //    item.m_Rotation = new float[4];
        //    item.m_Position = new float[3];
        //    item.m_BeenInPlayerInventoryProxy = true;
        //    item.NormalizedCondition = 1;
        //    item.m_WornOut = false;
        //    item.m_HoursPlayed = MainWindow.Instance.CurrentSave.Global.TimeOfDay.m_HoursPlayedNotPausedProxy;
        //    var r = new Random();
        //    var id = r.Next();
        //    while (MainWindow.Instance.CurrentSave.Global.Inventory.Items.Any(i => i.Gear.m_InstanceIDProxy == id))
        //        id = r.Next();
        //    item.m_InstanceIDProxy = id;
        //    item.m_NonInteractive = false;
        //    item.m_HasBeenEquippedAndUsed = false;
        //    return item;
        //}

        /// <summary>
        /// 创建新的装备物品保存数据
        /// </summary>
        /// <returns>新创建的装备物品保存数据代理对象</returns>
        public static GearItemSaveDataProxy Create()
        {
            var item = new GearItemSaveDataProxy();
            item.m_Rotation = new float[4];
            item.m_Position = new float[3];
            item.m_BeenInPlayerInventoryProxy = true;
            item.NormalizedCondition = 1;
            item.m_WornOut = false;
            // item.m_HoursPlayed = MainWindow.Instance.CurrentSave.Global.TimeOfDay.m_HoursPlayedNotPausedProxy;
            var r = new Random();
            var id = r.Next();
            //while (MainWindow.Instance.CurrentSave.Global.Inventory.Items.Any(i => i.Gear.m_InstanceIDProxy == id))
            //    id = r.Next();
            item.m_InstanceIDProxy = id;
            item.m_NonInteractive = false;
            item.m_HasBeenEquippedAndUsed = false;
            return item;
        }

        //public static GearItemSaveDataProxy Create()
        //{
        //    var item = new GearItemSaveDataProxy();
        //    item.m_Rotation = new float[4];
        //    item.m_Position = new float[3];
        //    item.m_BeenInPlayerInventoryProxy = true;
        //    item.NormalizedCondition = 1;
        //    item.m_WornOut = false;

        //    // 替换 MainWindow.Instance 为你的 WinForms 主窗体实例访问方式
        //    // 假设你的主窗体类名为 MainForm
        //    var mainForm = Application.OpenForms.OfType<Main>().FirstOrDefault();

        //    if (mainForm != null && mainForm.CurrentSave != null)
        //    {
        //        item.m_HoursPlayed = mainForm.CurrentSave.Global.TimeOfDay.m_HoursPlayedNotPausedProxy;

        //        var r = new Random();
        //        var id = r.Next();
        //        while (mainForm.CurrentSave.Global.Inventory.Items.Any(i => i.Gear.m_InstanceIDProxy == id))
        //            id = r.Next();
        //        item.m_InstanceIDProxy = id;
        //    }
        //    else
        //    {
        //        // 如果没有找到主窗体或保存数据，设置默认值
        //        item.m_HoursPlayed = 0f;
        //        item.m_InstanceIDProxy = new Random().Next();
        //    }

        //    item.m_NonInteractive = false;
        //    item.m_HasBeenEquippedAndUsed = false;
        //    return item;
        //}
    }

    /// <summary>
    /// 可堆叠物品存档数据代理类
    /// 管理游戏中可堆叠物品的数量状态
    /// </summary>
    public class StackableItemSaveDataProxy
    {
        /// <summary>
        /// 物品堆叠数量代理值
        /// 用于同步实际物品的当前堆叠数
        /// </summary>
        public int m_UnitsProxy { get; set; }
    }


    /// <summary>
    /// 食物物品存档数据代理类
    /// 管理游戏中食物物品的状态和属性
    /// </summary>
    public class FoodItemSaveDataProxy
    {
        /// <summary>
        /// 剩余卡路里代理值
        /// 用于同步实际食物的当前营养值
        /// </summary>
        public float m_CaloriesRemainingProxy { get; set; }

        /// <summary>
        /// 总卡路里值
        /// 表示食物完全状态下的营养总量
        /// </summary>
        public float m_CaloriesTotal { get; set; }

        /// <summary>
        /// 是否已开封
        /// 影响食物保存时间和食用方式
        /// </summary>
        public bool m_Opened { get; set; }

        /// <summary>
        /// 加热百分比(0-1)
        /// 0表示完全冷，1表示完全加热
        /// </summary>
        public float m_HeatPercent { get; set; }

        /// <summary>
        /// 游戏内小时数
        /// 用于计算食物新鲜度
        /// </summary>
        public float m_HoursPlayed { get; set; }

        /// <summary>
        /// 是否由玩家采集
        /// 区分自然生成和玩家获取
        /// </summary>
        public bool m_HarvestedByPlayer { get; set; }

        /// <summary>
        /// 加热次数
        /// 影响食物品质和效果
        /// </summary>
        public int m_NumTimesHeatedUp { get; set; }

        /// <summary>
        /// 是否包装完好
        /// 影响食物保存条件和腐败速度
        /// </summary>
        public bool m_Packaged { get; set; }
    }


    /// <summary>
    /// 液体物品存档数据代理类
    /// 管理游戏中液体物品的状态和属性
    /// </summary>
    public class LiquidItemSaveDataProxy
    {
        /// <summary>
        /// 液体容量代理值(单位：升)
        /// 用于同步实际容器中的液体量
        /// </summary>
        public float m_LiquidLitersProxy { get; set; }

        /// <summary>
        /// 液体品质枚举包装器
        /// 使用泛型EnumWrapper实现类型安全的枚举值存储
        /// </summary>
        public EnumWrapper<LiquidQuality> m_LiquidQuality { get; set; }
    }


    /// <summary>
    /// 信号弹物品存档数据代理类
    /// 管理游戏中信号弹物品的状态和行为
    /// </summary>
    [Serializable]
    public class FlareItemSaveDataProxy
    {
        /// <summary>
        /// 游戏内已玩小时数
        /// 用于计算信号弹的保存时间和状态变化
        /// </summary>
        public float m_HoursPlayed { get; set; }

        /// <summary>
        /// 信号弹状态代理(枚举包装器)
        /// 包含未激活/燃烧中/已熄灭等状态
        /// </summary>
        public EnumWrapper<FlareState> m_StateProxy { get; set; }

        /// <summary>
        /// 已燃烧分钟数代理值
        /// 精确记录信号弹燃烧持续时间(分钟级精度)
        /// </summary>
        public float m_ElapsedBurnMinutesProxy { get; set; }
    }


    /// <summary>
    /// 手电筒物品存档数据代理类
    /// 管理游戏中手电筒物品的状态和电量系统
    /// </summary>
    [Serializable]
    public class FlashlightItemSaveDataProxy
    {
        /// <summary>
        /// 开关状态标识
        /// true表示手电筒当前处于开启状态
        /// </summary>
        public bool m_IsOn { get; set; }

        /// <summary>
        /// 强光模式标识
        /// true表示当前处于高亮度模式
        /// </summary>
        public bool m_IsHigh { get; set; }

        /// <summary>
        /// 当前电池电量(0-1)
        /// 0表示完全没电，1表示满电状态
        /// </summary>
        public float m_CurrentBatteryCharge { get; set; }
    }


    /// <summary>
    /// 煤油灯物品存档数据代理类
    /// 管理游戏中煤油灯物品的状态和燃料系统
    /// </summary>
    [Serializable]
    public class KeroseneLampItemSaveDataProxy
    {
        /// <summary>
        /// 游戏内已玩小时数
        /// 用于计算燃料消耗和物品耐久度
        /// </summary>
        public float m_HoursPlayed { get; set; }

        /// <summary>
        /// 当前燃料量(单位：升)
        /// 精确到小数点后两位的燃料量记录
        /// </summary>
        public float m_CurrentFuelLitersProxy { get; set; }

        /// <summary>
        /// 开关状态代理
        /// true表示灯处于点亮状态
        /// </summary>
        public bool m_OnProxy { get; set; }
    }


    /// <summary>
    /// 服装物品存档数据代理类
    /// 管理游戏中服装物品的状态和环境交互数据
    /// </summary>
    [Serializable]
    public class ClothingItemSaveDataProxy
    {
        /// <summary>
        /// 穿着状态代理
        /// true表示当前正被玩家穿着
        /// </summary>
        public bool m_WearingProxy { get; set; }

        /// <summary>
        /// 潮湿百分比(0-1)
        /// 0表示完全干燥，1表示完全湿透
        /// </summary>
        public float m_PercentWet { get; set; }

        /// <summary>
        /// 冰冻百分比(0-1)
        /// 0表示未结冰，1表示完全冻结
        /// </summary>
        public float m_PercentFrozen { get; set; }

        /// <summary>
        /// 游戏内已玩小时数
        /// 用于计算服装耐久度和状态变化
        /// </summary>
        public float m_HoursPlayed { get; set; }

        /// <summary>
        /// 靠近火源时的剩余烘干时间(小时)
        /// 负值表示不需要烘干
        /// </summary>
        public float m_HoursRemainingOnCloseFire { get; set; }

        /// <summary>
        /// 室内丢弃标识
        /// true表示物品最后是在室内环境被丢弃
        /// </summary>
        public bool m_DroppedIndoors { get; set; }

        /// <summary>
        /// 装备数据存在标识
        /// true表示该服装有额外的装备数据
        /// </summary>
        public bool m_HasEquippedData { get; set; }

        /// <summary>
        /// 装备层级索引
        /// 决定服装在角色模型上的渲染层级
        /// </summary>
        public int m_EquippedLayerIndex { get; set; }
    }


    /// <summary>
    /// 枪械物品存档数据代理类
    /// 管理游戏中枪械物品的弹药和状态数据
    /// </summary>
    [Serializable]
    public class GunItemSaveDataProxy
    {
        /// <summary>
        /// 弹匣剩余弹药量代理
        /// 有效范围0-弹匣最大容量
        /// </summary>
        public int m_RoundsInClipProxy { get; set; }

        /// <summary>
        /// 卡弹状态标识
        /// true表示枪械当前处于故障状态
        /// </summary>
        public bool m_IsJammed { get; set; }
    }


    /// <summary>
    /// 水源供应存档数据代理类
    /// 管理游戏中液体容器的水量状态数据
    /// </summary>
    [Serializable]
    public class WaterSupplySaveDataProxy
    {
        /// <summary>
        /// 当前水量代理(单位：升)
        /// </summary>
        public float m_VolumeProxy { get; set; }
    }


    /// <summary>
    /// 床铺存档数据代理类
    /// 用于序列化存储床铺的展开/收起状态
    /// </summary>
    public class BedSaveDataProxy
    {
        /// <summary>
        /// 床铺卷起状态代理属性
        /// 使用EnumWrapper确保枚举值的版本兼容性
        /// </summary>
        public EnumWrapper<BedRollState> m_BedRollState { get; set; }
    }


    /// <summary>
    /// 可破坏物品存档数据代理类
    /// 用于记录游戏中可破坏物品的状态信息
    /// </summary>
    public class SmashableItemSaveDataProxy
    {
        /// <summary>
        /// 物品是否已被破坏的状态标识
        /// true表示物品已被破坏，false表示物品完好
        /// </summary>
        public bool m_HasBeenSmashed { get; set; }
    }


    /// <summary>
    /// 火柴物品存档数据代理类
    /// 用于保存火柴物品的燃烧状态和耐久度信息
    /// </summary>
    public class MatchesItemSaveDataProxy
    {
        /// <summary>
        /// 火柴总燃烧时间(游戏内秒数)
        /// 表示该火柴从点燃到完全燃烧完毕的总时长
        /// </summary>
        public float m_BurnTimeGametimeSeconds { get; set; }

        /// <summary>
        /// 已燃烧时间(游戏内秒数)
        /// 记录火柴当前已经燃烧的时间长度
        /// </summary>
        public float m_ElapsedBurnGametimeSeconds { get; set; }

        /// <summary>
        /// 是否已点燃状态
        /// true表示火柴当前处于燃烧状态
        /// </summary>
        public bool m_Ignited { get; set; }

        /// <summary>
        /// 是否为全新状态
        /// true表示火柴未被使用过
        /// </summary>
        public bool m_IsFresh { get; set; }
    }


    /// <summary>
    /// 陷阱物品存档数据代理类
    /// 用于保存陷阱物品的使用状态和时间信息
    /// </summary>
    public class SnareItemSaveDataProxy
    {
        /// <summary>
        /// 陷阱已使用总时长(小时)
        /// 记录陷阱从部署开始累计的使用时间
        /// </summary>
        public float m_HoursPlayed { get; set; }

        /// <summary>
        /// 上次触发时的使用时长(小时)
        /// 记录陷阱在上次触发时的累计使用时间
        /// </summary>
        public float m_HoursAtLastRoll { get; set; }

        /// <summary>
        /// 陷阱当前状态
        /// 使用EnumWrapper确保枚举值的版本兼容性
        /// </summary>
        public EnumWrapper<SnareState> m_State { get; set; }
    }


    /// <summary>
    /// 喷漆存档数据代理类
    /// 用于保存喷漆物品的颜色状态信息
    /// </summary>
    public class SprayPaintSaveDataProxy
    {
        /// <summary>
        /// 喷漆当前颜色
        /// 使用EnumWrapper<SprayColor>确保颜色枚举值的版本兼容性
        /// </summary>
        public EnumWrapper<SprayColor> m_Colour { get; set; }
    }


    /// <summary>
    /// 刚体物理组件存档代理类
    /// 用于保存游戏对象的物理状态数据
    /// </summary>
    public class RigidBodySaveDataProxy
    {
        // 预留字段用于未来扩展刚体物理特性
    }

    /// <summary>
    /// 粉末物品存档代理类 
    /// 用于保存粉末类物品的基础状态
    /// </summary>
    public class PowderItemSaveDataProxy
    {
        // 预留字段用于粉末特殊属性
    }

    /// <summary>
    /// 可研磨物品存档代理类
    /// 用于保存可研磨物品的加工状态
    /// </summary>
    public class MillableSaveDataProxy
    {
        // 预留字段用于研磨进度等
    }

    /// <summary>
    /// 制作中物品存档代理类
    /// 用于保存物品制作进度状态
    /// </summary>
    public class InProgressCraftItemSaveDataProxy
    {
        /// <summary>
        /// 制作完成百分比(0.0-1.0)
        /// </summary>
        public float m_PercentComplete { get; set; }

        /// <summary>
        /// 物品当前重量(单位：千克)
        /// </summary>
        public float m_Weight { get; set; }
    }

    /// <summary>
    /// 火把物品存档代理类
    /// 用于保存火把的燃烧状态
    /// </summary>
    public class TorchItemSaveDataProxy
    {
        /// <summary>
        /// 已使用总时长(游戏小时)
        /// </summary>
        public float m_HoursPlayed { get; set; }

        /// <summary>
        /// 火把当前状态(燃烧/熄灭等)
        /// </summary>
        public EnumWrapper<TorchState> m_StateProxy { get; set; }

        /// <summary>
        /// 已燃烧时长(游戏分钟)
        /// </summary>
        public float m_ElapsedBurnMinutesProxy { get; set; }
    }

    /// <summary>
    /// 进化物品存档数据类
    /// 用于保存物品进化状态
    /// </summary>
    public class EvolveItemSaveData
    {
        /// <summary>
        /// 物品存在总时长(游戏小时)
        /// </summary>
        public float m_HoursPlayed { get; set; }

        /// <summary>
        /// 已进化时长(游戏小时)
        /// </summary>
        public float m_TimeSpentEvolvingGameHours { get; set; }

        /// <summary>
        /// 是否禁用自动进化标志
        /// </summary>
        public bool m_ForceNoAutoEvolve;
    }


    /// <summary>
    /// 任务对象标识存档代理类
    /// 用于唯一标识和保存任务相关对象的状态
    /// </summary>
    public class MissionObjectIdentifierSaveProxy
    {
        /// <summary>
        /// 对象唯一标识符
        /// 格式通常为GUID或特定命名规则生成的字符串
        /// </summary>
        public string m_Id { get; set; }

        /// <summary>
        /// 任务对象分类枚举
        /// 使用EnumWrapper保证版本兼容性
        /// </summary>
        public EnumWrapper<MissionObjectClass> m_ObjectClass { get; set; }

        /// <summary>
        /// 活动标签字符串
        /// 用于标记对象参与的活动类型，多个标签可用分隔符连接
        /// </summary>
        public string m_ActivityTags { get; set; }

        /// <summary>
        /// 任务完成后是否销毁标志
        /// true表示任务完成后自动销毁该对象
        /// </summary>
        public bool m_DestroyAfterMission { get; set; }
    }


    /// <summary>
    /// 研究物品存档数据类
    /// 用于保存研究类物品的进度状态
    /// </summary>
    public class ResearchItemSaveData
    {
        /// <summary>
        /// 已进行研究的时间(单位：游戏小时)
        /// 用于计算研究进度和完成度
        /// </summary>
        public float m_ElapsedHours { get; set; }
    }


    /// <summary>
    /// 动物尸体采集存档代理类
    /// 用于保存可采集动物尸体的状态和资源数据
    /// </summary>
    public class BodyHarvestSaveDataProxy
    {
        /// <summary>
        /// 可采集肉类总量(单位：千克)
        /// 受尸体新鲜度和采集程度影响
        /// </summary>
        public float m_MeatAvailableKG { get; set; }

        /// <summary>
        /// 可采集毛皮数量(单位：张)
        /// 完整毛皮通常为1单位
        /// </summary>
        public int m_HideAvailableUnits { get; set; }

        /// <summary>
        /// 可采集内脏数量(单位：份)
        /// 用于制作陷阱诱饵等
        /// </summary>
        public int m_GutAvailableUnits { get; set; }

        /// <summary>
        /// 是否处于冷冻状态
        /// 冷冻状态下腐败速度大幅降低
        /// </summary>
        public bool m_Frozen { get; set; }

        /// <summary>
        /// 是否被其他动物啃食过
        /// 影响可采集资源数量
        /// </summary>
        public bool m_Ravaged { get; set; }

        /// <summary>
        /// 尸体完整度(0.0-1.0)
        /// 影响资源产出效率
        /// </summary>
        public float m_Condition { get; set; }

        /// <summary>
        /// 是否已计算生成概率
        /// 防止重复计算采集物生成
        /// </summary>
        public bool m_RolledSpawnChance { get; set; }

        /// <summary>
        /// 是否允许自然腐败
        /// 关闭后尸体不会随时间腐败
        /// </summary>
        public bool m_AllowDecay { get; set; }

        /// <summary>
        /// 尸体存在时长(游戏小时)
        /// 用于计算腐败程度
        /// </summary>
        public float m_HoursPlayed { get; set; }

        /// <summary>
        /// 冷冻程度百分比(0.0-1.0)
        /// 0表示完全解冻，1表示完全冷冻
        /// </summary>
        public float m_PercentFrozen { get; set; }

        /// <summary>
        /// 近火源剩余时间(游戏小时)
        /// 用于加速解冻或防止冻结
        /// </summary>
        public float m_HoursRemainingOnCloseFire { get; set; }

        /// <summary>
        /// 每小时腐败损失量(千克/小时)
        /// 动态计算的腐败速率
        /// </summary>
        public float m_DecimationKGPerHour { get; set; }

        /// <summary>
        /// 剩余腐败时间(游戏小时)
        /// 当腐败完成后尸体消失
        /// </summary>
        public float m_DecimationHoursRemaining { get; set; }

        /// <summary>
        /// 四分之一处理耗时(游戏分钟)
        /// 将尸体处理成可携带部分的时间
        /// </summary>
        public float m_QuarterDurationMinutes { get; set; }

        /// <summary>
        /// 是否已被采集过
        /// 防止重复采集标记
        /// </summary>
        public bool m_HasHarvested { get; set; }

        /// <summary>
        /// 上次采集时间(游戏小时)
        /// 用于计算资源再生
        /// </summary>
        public float m_LastHarvestTimeHours { get; set; }

        /// <summary>
        /// 四分之一袋装损耗系数
        /// 处理成便携包时的重量损失
        /// </summary>
        public float m_QuarterBagWasteMultiplier { get; set; }

        /// <summary>
        /// 关联任务ID序列化字符串
        /// 用于任务系统关联
        /// </summary>
        public string m_MissionIdSerialized { get; set; }

        /// <summary>
        /// 熊猎AI序列化数据
        /// 保存特殊狩猎行为数据
        /// </summary>
        public string m_BearHuntAiSerialized { get; set; }

        /// <summary>
        /// 熊猎AI扩展序列化数据
        /// 保存补充狩猎行为数据
        /// </summary>
        public string m_BearHuntAiReduxSerialized { get; set; }

        /// <summary>
        /// 伤害来源方向枚举包装
        /// 记录致命伤来源方向
        /// </summary>
        public EnumWrapper<DamageSide> m_DamageSide { get; set; }
    }



    /// <summary>
    /// 烹饪锅物品存档代理类
    /// 用于保存烹饪锅的工作状态和烹饪进度数据
    /// </summary>
    public class CookingPotItemSaveDataProxy
    {
        /// <summary>
        /// 正在使用的火源GUID
        /// 为空表示未放置火源上
        /// </summary>
        public string m_FireBeingUsedGUID { get; set; }

        /// <summary>
        /// 正在烹饪的物品GUID
        /// 记录锅中的食物或原料
        /// </summary>
        public string m_GearItemBeingCookedGUID { get; set; }

        /// <summary>
        /// 已烹饪时长(游戏小时)
        /// 用于计算食物熟度
        /// </summary>
        public float m_CookingElapsedHours { get; set; }

        /// <summary>
        /// 宽限时长(游戏小时)
        /// 烹饪完成后可维持不烧焦的时间
        /// </summary>
        public float m_GracePeriodElapsedHours { get; set; }

        /// <summary>
        /// 火源燃烧时间(游戏小时)
        /// 基于游戏内时间系统(TOD)
        /// </summary>
        public float m_FireBurningTimeTODHours { get; set; }

        /// <summary>
        /// 序列化时的游戏总时长
        /// 用于计算离线烹饪进度
        /// </summary>
        public float m_HoursPlayedWhenSerialized { get; set; }

        /// <summary>
        /// 正在融化的雪量(升)
        /// 雪水转化功能
        /// </summary>
        public float m_LitersSnowBeingMelted { get; set; }

        /// <summary>
        /// 正在煮沸的水量(升)
        /// 净水处理功能
        /// </summary>
        public float m_LitersWaterBeingBoiled { get; set; }

        /// <summary>
        /// 是否仅能加热食物
        /// true时无法进行完整烹饪
        /// </summary>
        public bool m_CanOnlyWarmUpFood { get; set; }
    }


    #endregion

    /// <summary>
    /// 玩家管理存档代理类
    /// 保存玩家角色核心状态和游戏进度数据
    /// </summary>
    public class PlayerManagerSaveDataProxy
    {
        /// <summary>
        /// 已知密码列表
        /// 存储已破解的保险箱/门禁密码
        /// </summary>
        public List<int> m_KnownCodes { get; set; }

        /// <summary>
        /// 存档时玩家位置坐标(x,y,z)
        /// 使用可观察集合实现位置变化通知
        /// </summary>
        public ObservableCollection<float> m_SaveGamePosition { get; set; }

        /// <summary>
        /// 存档时玩家旋转角度
        /// 欧拉角表示[Pitch,Yaw,Roll]
        /// </summary>
        public float[] m_SaveGameRotation { get; set; }

        /// <summary>
        /// 初始装备是否已发放
        /// 防止重复发放新手装备
        /// </summary>
        public bool m_StartGearAppliedProxy { get; set; }

        /// <summary>
        /// 手持物品实例ID
        /// 0表示空手状态
        /// </summary>
        public int m_ItemInHandsInstanceID { get; set; }

        /// <summary>
        /// 最后卸下的物品实例ID
        /// 用于快速装备切换
        /// </summary>
        public int m_LastUnequippedItemInstanceID { get; set; }

        /// <summary>
        /// 是否处于奔跑模式
        /// 影响体力消耗速率
        /// </summary>
        public bool m_InRunMode { get; set; }

        /// <summary>
        /// 幽灵模式状态
        /// 开启后无视碰撞体积
        /// </summary>
        public bool m_Ghost { get; set; }

        /// <summary>
        /// 上帝模式状态
        /// 开启后无敌状态
        /// </summary>
        public bool m_God { get; set; }

        /// <summary>
        /// 是否使用过作弊码
        /// 用于成就系统判定
        /// </summary>
        public bool m_CheatsUsed { get; set; }

        /// <summary>
        /// 语音角色枚举包装
        /// 存储玩家选择的角色语音类型
        /// </summary>
        public EnumWrapper<VoicePersona> m_VoicePersona { get; set; }

        /// <summary>
        /// 今日采集的卡路里量
        /// 用于统计生存数据
        /// </summary>
        public float m_CaloriesHarvestedToday { get; set; }

        /// <summary>
        /// 冻结速率系数
        /// 值越大体温下降越快
        /// </summary>
        public float m_FreezingRateScale { get; set; }

        /// <summary>
        /// 疲劳速率系数
        /// 值越大体力消耗越快
        /// </summary>
        public float m_FatigueRateScale { get; set; }

        /// <summary>
        /// 状态百分比加成
        /// 影响生命值恢复效率
        /// </summary>
        public float m_ConditonPercentBonus { get; set; }

        /// <summary>
        /// 疲劳增益剩余时间(游戏小时)
        /// 特殊状态持续时间
        /// </summary>
        public float m_FatigueBuffHoursRemaining { get; set; }

        /// <summary>
        /// 抗寒增益剩余时间(游戏小时)
        /// 特殊状态持续时间
        /// </summary>
        public float m_FreezingBuffHoursRemaining { get; set; }

        /// <summary>
        /// 休息恢复增益剩余时间(游戏小时)
        /// 特殊状态持续时间
        /// </summary>
        public float m_ConditionRestBuffHoursRemaining { get; set; }

        /// <summary>
        /// 起始区域名称
        /// 记录玩家出生点区域
        /// </summary>
        public string m_StartingRegionName { get; set; }

        /// <summary>
        /// 工作台选中蓝图索引
        /// 保存最后操作的工作台配方
        /// </summary>
        public int m_SelectedBlueprintItemIndexWorkbench { get; set; }

        /// <summary>
        /// 熔炉选中蓝图索引
        /// 保存最后操作的熔炉配方
        /// </summary>
        public int m_SelectedBlueprintItemIndexForge { get; set; }

        /// <summary>
        /// 是否在载具中
        /// 影响移动方式和碰撞检测
        /// </summary>
        public bool m_PlayerInVehicle { get; set; }

        /// <summary>
        /// 载具内摄像机位置
        /// 第三人称视角偏移量[x,y,z]
        /// </summary>
        public float[] m_PlayerInVehicleCameraPos { get; set; }

        /// <summary>
        /// 是否在雪屋中
        /// 影响保暖效果
        /// </summary>
        public bool m_PlayerInSnowShelter { get; set; }

        /// <summary>
        /// 南瓜派增益剩余时间(游戏小时)
        /// 节日特殊增益持续时间
        /// </summary>
        public float m_PumpkinPieBuffHoursRemaining { get; set; }

        /// <summary>
        /// 南瓜派抗寒系数
        /// 节日特殊增益效果强度
        /// </summary>
        public float m_PumpkinPieFreezingRateScale { get; set; }

        /// <summary>
        /// 篝火限制区域边界
        /// 序列化的空间边界数据
        /// </summary>
        public SerializableBounds m_LimitCampfiresToBounds { get; set; }

        /// <summary>
        /// 状态条是否锁定
        /// 防止UI自动隐藏
        /// </summary>
        public bool m_StatusBarsLocked { get; set; }
    }


    /// <summary>
    /// 玩家攀爬绳索代理类
    /// 保存玩家攀爬绳索时的实时状态数据
    /// </summary>
    public class PlayerClimbRopeProxy
    {
        /// <summary>
        /// 当前攀爬的绳索GUID
        /// 唯一标识场景中的绳索实例
        /// </summary>
        public string m_RopeGuid { get; set; }

        /// <summary>
        /// 在绳索样条线上的位置参数(0.0-1.0)
        /// 0表示底部，1表示顶部
        /// </summary>
        public float m_SplineT { get; set; }

        /// <summary>
        /// 体力耗尽计时器(秒)
        /// 归零时触发坠落判定
        /// </summary>
        public float m_NoStaminaTimerSeconds { get; set; }

        /// <summary>
        /// 下次打滑判定时间(秒)
        /// 基于游戏实时时间系统
        /// </summary>
        public float m_NextSlipRollSeconds { get; set; }

        /// <summary>
        /// 下次打滑概率(0.0-1.0)
        /// 受疲劳值和天气影响
        /// </summary>
        public float m_NextSlipChance { get; set; }

        /// <summary>
        /// 下次坠落概率(0.0-1.0)
        /// 连续打滑时概率递增
        /// </summary>
        public float m_NextFallChance { get; set; }
    }


    /// <summary>
    /// 玩家技能存档数据类
    /// 记录角色各项生存技能的熟练度(0.0-1.0)
    /// </summary>
    public class PlayerSkillsSaveData
    {
        /// <summary>
        /// [已废弃] 生火技能熟练度
        /// 被新版火焰管理系统替代
        /// </summary>
        [Obsolete("Replaced by FireStartingSystem in v2.4")]
        public float m_FireStartingSkill { get; set; }

        /// <summary>
        /// [已废弃] 修理技能熟练度
        /// 合并到装备维护系统
        /// </summary>
        [Obsolete("Merged into EquipmentMaintenanceSystem in v3.1")]
        public float m_RepairSkill { get; set; }

        /// <summary>
        /// 清洁技能熟练度
        /// 影响武器保养效果和疾病预防
        /// </summary>
        public float m_CleanSkill { get; set; }

        /// <summary>
        /// 磨刀技能熟练度
        /// 决定工具锋利度保持时长
        /// </summary>
        public float m_SharpenSkill { get; set; }

        /// <summary>
        /// [已废弃] 制作技能熟练度
        /// 拆分为具体工艺子系统
        /// </summary>
        [Obsolete("Split into specialized subsystems in v4.0")]
        public float m_CraftingSkill { get; set; }
    }


    /// <summary>
    /// 玩家游戏统计代理类
    /// 记录角色生存活动的关键生理指标数据
    /// </summary>
    public class PlayerGameStatsProxy
    {
        /// <summary>
        /// 累计燃烧卡路里(千卡)
        /// 包含基础代谢和运动消耗
        /// </summary>
        public float m_CaloriesBurned { get; set; }

        /// <summary>
        /// 累计摄入卡路里(千卡)
        /// 记录所有食物来源
        /// </summary>
        public float m_CaloriesEaten { get; set; }

        /// <summary>
        /// 当日最高体温(摄氏度)
        /// 用于低温症风险预警
        /// </summary>
        public float m_BodyTempHigh { get; set; }

        /// <summary>
        /// 当日最低体温(摄氏度)
        /// 用于冻伤风险预警
        /// </summary>
        public float m_BodyTempLow { get; set; }

        /// <summary>
        /// 日间移动距离(米)
        /// 仅计算6:00-18:00时段
        /// </summary>
        public float m_DistanceTravelledDay { get; set; }

        /// <summary>
        /// 夜间移动距离(米)
        /// 仅计算18:00-次日6:00时段
        /// </summary>
        public float m_DistanceTravelledNight { get; set; }

        /// <summary>
        /// 状态恢复总量(百分比)
        /// 睡眠/治疗等恢复数值累计
        /// </summary>
        public float m_ConditionGained { get; set; }

        /// <summary>
        /// 状态损失总量(百分比)
        /// 受伤/疾病等损耗数值累计
        /// </summary>
        public float m_ConditionLost { get; set; }

        /// <summary>
        /// 今日总消耗卡路里(千卡)
        /// 每日0点自动清零
        /// </summary>
        public float m_CaloriesExpendedToday { get; set; }
    }


    /// <summary>
    /// 低温症状态存档代理类
    /// 记录角色低温症发作的详细状态数据
    /// </summary>
    public class HypothermiaSaveDataProxy
    {
        /// <summary>
        /// 是否处于活跃状态
        /// true表示正在经历低温症症状
        /// </summary>
        public bool m_Active { get; set; }

        /// <summary>
        /// 症状持续时长(小时)
        /// 累计未解除状态的时间
        /// </summary>
        public float m_ElapsedHours { get; set; }

        /// <summary>
        /// 回暖累计时长(小时)
        /// 在温暖环境中恢复的时间
        /// </summary>
        public float m_ElapsedWarmTime { get; set; }

        /// <summary>
        /// 病因本地化键值
        /// 对应多语言系统的文本ID
        /// </summary>
        public string m_CauseLocID { get; set; }
    }


    /// <summary>
    /// 冻伤状态存档代理类
    /// 记录角色身体各部位冻伤状态及风险数据
    /// </summary>
    public class FrostbiteSaveDataProxy
    {
        /// <summary>
        /// 已发生冻伤的身体部位索引列表
        /// 对应BodyLocation枚举值(0:头部 1:躯干 2:左手...)
        /// </summary>
        public List<int> m_LocationsWithActiveFrostbite { get; set; }

        /// <summary>
        /// 存在冻伤风险的身体部位索引列表
        /// 当暴露值超过阈值时加入此列表
        /// </summary>
        public List<int> m_LocationsWithFrostbiteRisk { get; set; }

        /// <summary>
        /// 各部位当前冻伤伤害值(0-1)
        /// 索引与部位对应，0表示无伤害
        /// </summary>
        public List<float> m_LocationsCurrentFrostbiteDamage { get; set; }
    }


    /// <summary>
    /// 食物中毒状态存档代理类
    /// 记录角色食物中毒症状的详细状态数据
    /// </summary>
    public class FoodPoisoningSaveDataProxy
    {
        /// <summary>
        /// 是否处于中毒状态
        /// true表示正在经历中毒症状
        /// </summary>
        public bool m_Active { get; set; }

        /// <summary>
        /// 症状持续时长(小时)
        /// 从中毒开始累计的时间
        /// </summary>
        public float m_ElapsedHours { get; set; }

        /// <summary>
        /// 预计总持续时间(小时)
        /// 未经治疗的自然病程时长
        /// </summary>
        public float m_DurationHours { get; set; }

        /// <summary>
        /// 是否服用抗生素
        /// true表示已使用药物治疗
        /// </summary>
        public bool m_AntibioticsTaken { get; set; }

        /// <summary>
        /// 休息累计时长(小时)
        /// 躺卧状态的恢复时间累计
        /// </summary>
        public float m_ElapsedRest { get; set; }

        /// <summary>
        /// 中毒原因本地化键值
        /// 对应多语言系统的文本ID
        /// </summary>
        public string m_CauseLocID { get; set; }
    }


    /// <summary>
    /// 痢疾状态存档代理类
    /// 记录角色痢疾症状的详细状态数据
    /// </summary>
    public class DysenterySaveDataProxy
    {
        /// <summary>
        /// 是否处于活跃状态
        /// true表示正在经历痢疾症状
        /// </summary>
        public bool m_Active { get; set; }

        /// <summary>
        /// 症状持续时长(小时)
        /// 从发病开始累计的时间
        /// </summary>
        public float m_ElapsedHours { get; set; }

        /// <summary>
        /// 预计总持续时间(小时)
        /// 未经治疗的自然病程时长
        /// </summary>
        public float m_DurationHours { get; set; }

        /// <summary>
        /// 是否服用抗生素
        /// true表示已使用药物治疗
        /// </summary>
        public bool m_AntibioticsTaken { get; set; }

        /// <summary>
        /// 休息累计时长(小时)
        /// 躺卧状态的恢复时间累计
        /// </summary>
        public float m_ElapsedRest { get; set; }

        /// <summary>
        /// 净水消耗量(升)
        /// 治疗期间饮用的清洁水量
        /// </summary>
        public float m_CleanWaterConsumedLiters { get; set; }
    }


    /// <summary>
    /// 脚踝扭伤状态存档代理类
    /// 记录角色脚踝扭伤的详细状态数据
    /// </summary>
    public class SprainedAnkleSaveDataProxy
    {
        /// <summary>
        /// 上次疼痛音效播放后的秒数
        /// 用于控制疼痛音效的播放频率
        /// </summary>
        public float m_SecondsSinceLastPainAudio { get; set; }

        /// <summary>
        /// 下次疼痛音效播放前的剩余秒数
        /// 动态计算的下次音效触发时间
        /// </summary>
        public float m_SecondsUntilNextPainAudio { get; set; }

        /// <summary>
        /// 扭伤原因本地化键值列表
        /// 支持多原因记录的国际文本ID
        /// </summary>
        public List<string> m_CausesLocIDs { get; set; }

        /// <summary>
        /// 受伤部位索引列表
        /// 对应BodyLocation枚举值(0:左脚踝 1:右脚踝)
        /// </summary>
        public List<int> m_Locations { get; set; }

        /// <summary>
        /// 各部位症状持续时长列表(小时)
        /// 与受伤部位索引一一对应
        /// </summary>
        public List<float> m_ElapsedHoursList { get; set; }

        /// <summary>
        /// 各部位预计恢复时长列表(小时)
        /// 包含不同严重程度的恢复时间
        /// </summary>
        public List<float> m_DurationHoursList { get; set; }

        /// <summary>
        /// 各部位休息累计时长列表(小时)
        /// 用于计算恢复进度加成
        /// </summary>
        public List<float> m_ElapsedRestList { get; set; }
    }


    /// <summary>
    /// 手腕扭伤状态存档代理类
    /// 记录角色手腕扭伤的详细状态数据
    /// </summary>
    public class SprainedWristSaveDataProxy
    {
        /// <summary>
        /// 扭伤原因本地化键值列表
        /// 支持多原因记录的国际文本ID
        /// </summary>
        public List<string> m_CausesLocIDs { get; set; }

        /// <summary>
        /// 受伤部位索引列表
        /// 对应BodyLocation枚举值(0:左手腕 1:右手腕)
        /// </summary>
        public List<int> m_Locations { get; set; }

        /// <summary>
        /// 各部位症状持续时长列表(小时)
        /// 与受伤部位索引一一对应
        /// </summary>
        public List<float> m_ElapsedHoursList { get; set; }

        /// <summary>
        /// 各部位预计恢复时长列表(小时)
        /// 包含不同严重程度的恢复时间
        /// </summary>
        public List<float> m_DurationHoursList { get; set; }

        /// <summary>
        /// 各部位休息累计时长列表(小时)
        /// 用于计算恢复进度加成
        /// </summary>
        public List<float> m_ElapsedRestList { get; set; }

        /// <summary>
        /// 是否强制解除扭伤状态
        /// 开发者调试/紧急恢复功能开关
        /// </summary>
        public bool m_IsNoSprainWristForced { get; set; }
    }


    /// <summary>
    /// 烧伤状态存档代理类
    /// 记录角色烧伤的详细状态数据
    /// </summary>
    public class BurnsSaveDataProxy
    {
        /// <summary>
        /// 是否处于活跃状态
        /// true表示当前存在烧伤症状
        /// </summary>
        public bool m_Active { get; set; }

        /// <summary>
        /// 症状持续时长(小时)
        /// 从受伤开始累计的时间
        /// </summary>
        public float m_ElapsedHours { get; set; }

        /// <summary>
        /// 预计总持续时间(小时)
        /// 未经治疗的自然愈合时长
        /// </summary>
        public float m_DurationHours { get; set; }

        /// <summary>
        /// 是否服用止痛药
        /// true表示已使用药物缓解疼痛
        /// </summary>
        public bool m_PainKillersTaken { get; set; }

        /// <summary>
        /// 是否已包扎
        /// true表示伤口已进行专业处理
        /// </summary>
        public bool m_BandageApplied { get; set; }

        /// <summary>
        /// 已播放的烧伤提醒次数
        /// 用于控制UI提醒频率
        /// </summary>
        public int m_NumBurnRemindersPlayed { get; set; }

        /// <summary>
        /// 下次烧伤提醒剩余秒数
        /// 动态计算的提醒间隔时间
        /// </summary>
        public float m_SecondsUntilNextBurnReminder { get; set; }

        /// <summary>
        /// 受伤原因本地化键值
        /// 支持多语言的原因描述ID
        /// </summary>
        public string m_CauseLocID;
    }


    /// <summary>
    /// 电击烧伤状态存档代理类
    /// 记录特殊类型烧伤的持续状态和治疗情况
    /// </summary>
    public class BurnsElectricSaveDataProxy
    {
        /// <summary>
        /// 是否处于电击伤害激活状态
        /// 区别于普通烧伤的独立状态标记
        /// </summary>
        public bool m_Active { get; set; }

        /// <summary>
        /// 电击持续时间(小时)
        /// 包含肌肉痉挛等后效持续时间
        /// </summary>
        public float m_ElapsedHours { get; set; }

        /// <summary>
        /// 预计症状总持续时间(小时)
        /// 考虑电击伤害的特殊恢复曲线
        /// </summary>
        public float m_DurationHours { get; set; }

        /// <summary>
        /// 是否服用神经性止痛药
        /// 电击伤害需特殊药物处理
        /// </summary>
        public bool m_PainKillersTaken { get; set; }

        /// <summary>
        /// 是否进行绝缘包扎
        /// 防止二次触电的特殊处理标记
        /// </summary>
        public bool m_BandageApplied { get; set; }

        /// <summary>
        /// 电击警示提示播放次数
        /// 包含心跳检测等特殊提醒
        /// </summary>
        public int m_NumBurnRemindersPlayed { get; set; }

        /// <summary>
        /// 下次警示提示剩余秒数
        /// 动态调整的心律失常提醒间隔
        /// </summary>
        public float m_SecondsUntilNextBurnReminder { get; set; }
    }


    /// <summary>
    /// 失血状态存档代理类
    /// 记录角色失血伤害的详细状态数据
    /// </summary>
    public class BloodLossSaveDataProxy
    {
        /// <summary>
        /// 失血原因本地化键值列表
        /// 支持多语言的原因描述ID集合
        /// </summary>
        public List<string> m_CausesLocIDs { get; set; }

        /// <summary>
        /// 受伤部位索引列表
        /// 对应BodyLocation枚举值(0:头部 1:躯干 2:四肢等)
        /// </summary>
        public List<int> m_Locations { get; set; }

        /// <summary>
        /// 各部位失血持续时间列表(小时)
        /// 记录不同伤口的持续失血时间
        /// </summary>
        public List<float> m_ElapsedHoursList { get; set; }

        /// <summary>
        /// 各部位自然止血所需时间列表(小时)
        /// 包含不同严重程度的恢复时间
        /// </summary>
        public List<float> m_DurationHoursList { get; set; }
    }



    /// <summary>
    /// 肋骨骨折状态存档代理类
    /// 记录多发性肋骨骨折的详细状态数据
    /// </summary>
    public class BrokenRibSaveDataProxy
    {
        /// <summary>
        /// 骨折原因本地化键值列表
        /// 支持多语言的原因描述ID集合
        /// </summary>
        public List<string> m_CausesLocIDs { get; set; }

        /// <summary>
        /// 骨折部位索引列表
        /// 对应RibLocation枚举值(0-11表示12对肋骨)
        /// </summary>
        public List<int> m_Locations { get; set; }

        /// <summary>
        /// 止痛药使用剂量列表(mg)
        /// 记录各部位对应的药物剂量
        /// </summary>
        public List<int> m_PainKillersTaken { get; set; }

        /// <summary>
        /// 固定绷带使用数量列表
        /// 记录各部位使用的绷带数量
        /// </summary>
        public List<int> m_BandagesApplied { get; set; }

        /// <summary>
        /// 各部位静养时间列表(小时)
        /// 记录骨折后的实际休息时长
        /// </summary>
        public List<float> m_ElapsedRestList { get; set; }

        /// <summary>
        /// 各部位痊愈所需静养时间列表(小时)
        /// 根据骨折严重程度动态计算
        /// </summary>
        public List<float> m_NumHoursRestForCureList { get; set; }
    }



    /// <summary>
    /// 感染状态存档代理类
    /// 记录多部位感染的详细状态和治疗情况
    /// </summary>
    public class InfectionSaveDataProxy
    {
        /// <summary>
        /// 感染原因本地化键值列表
        /// 支持多语言的原因描述ID集合
        /// </summary>
        public List<string> m_CausesLocIDs { get; set; }

        /// <summary>
        /// 感染部位索引列表
        /// 对应BodyLocation枚举值(0:伤口 1:内脏 2:呼吸道等)
        /// </summary>
        public List<int> m_Locations { get; set; }

        /// <summary>
        /// 各部位感染持续时间列表(小时)
        /// 记录从感染开始计算的时间
        /// </summary>
        public List<float> m_ElapsedHoursList { get; set; }

        /// <summary>
        /// 各部位预计痊愈时间列表(小时)
        /// 根据感染类型和严重程度计算
        /// </summary>
        public List<float> m_DurationHoursList { get; set; }

        /// <summary>
        /// 抗生素使用状态列表
        /// 记录各部位是否已使用抗生素
        /// </summary>
        public List<bool> m_AntibioticsTakenList { get; set; }

        /// <summary>
        /// 各部位治疗后休息时间列表(小时)
        /// 记录用药后的恢复时长
        /// </summary>
        public List<float> m_ElapsedRestList { get; set; }
    }



    /// <summary>
    /// 感染风险评估数据代理类
    /// 记录多部位感染风险的动态变化和治疗干预数据
    /// </summary>
    public class InfectionRiskSaveDataProxy
    {
        /// <summary>
        /// 最后评估时间戳(游戏内小时)
        /// 记录上次风险计算的时间点
        /// </summary>
        public float m_CommentTime;

        /// <summary>
        /// 风险原因本地化键值列表
        /// 支持多语言的风险因素描述
        /// </summary>
        public List<string> m_CausesLocIDs { get; set; }

        /// <summary>
        /// 风险部位索引列表
        /// 对应BodyPart枚举值(0:伤口 1:内脏等)
        /// </summary>
        public List<int> m_Locations { get; set; }

        /// <summary>
        /// 各部位风险持续时间(小时)
        /// 从风险产生开始计时
        /// </summary>
        public List<float> m_ElapsedHoursList { get; set; }

        /// <summary>
        /// 各部位风险持续周期(小时)
        /// 根据伤口类型和环境因素计算
        /// </summary>
        public List<float> m_DurationHoursList { get; set; }

        /// <summary>
        /// 消毒剂使用状态列表
        /// 记录各部位是否已进行消毒处理
        /// </summary>
        public List<bool> m_AntisepticTakenList { get; set; }

        /// <summary>
        /// 当前感染概率列表(0-1范围)
        /// 动态计算的实时感染风险值
        /// </summary>
        public List<float> m_CurrentInfectionChanceList { get; set; }

        /// <summary>
        /// 持续影响索引列表
        /// 关联ConstantAfflictionData的ID集合
        /// </summary>
        public List<int> m_ConstantAfflictionIndices { get; set; }
    }


    /// <summary>
    /// 幽居症状态存档代理类
    /// 记录玩家因长期室内活动导致的心理状态变化
    /// </summary>
    public class CabinFeverSaveDataProxy
    {
        /// <summary>
        /// 当前是否处于幽居症发作状态
        /// true表示症状已激活
        /// </summary>
        public bool m_Active { get; set; }

        /// <summary>
        /// 是否处于风险警戒状态
        /// true表示接近发作临界点
        /// </summary>
        public bool m_RiskActive { get; set; }

        /// <summary>
        /// 症状持续时间(小时)
        /// 从症状激活开始累计
        /// </summary>
        public float m_ElapsedHours { get; set; }

        /// <summary>
        /// 室内时间追踪记录表(小时)
        /// 按游戏日记录的室内停留时长
        /// </summary>
        public List<float> m_IndoorTimeTracked { get; set; }

        /// <summary>
        /// 最后一帧的小时数记录
        /// 用于计算时间增量
        /// </summary>
        public int m_HourLastFrame { get; set; }

        /// <summary>
        /// 万圣节事件修复标记
        /// 默认true表示已完成特殊事件处理
        /// </summary>
        public bool m_DoneHalloweenEventFix { get; set; } = true;

    }


    /// <summary>
    /// 肠道寄生虫感染状态代理类
    /// 记录寄生虫感染风险和治疗过程的关键数据
    /// </summary>
    public class IntestinalParasitesSaveDataProxy
    {
        /// <summary>
        /// 当前是否感染寄生虫
        /// true表示已确诊感染
        /// </summary>
        public bool m_HasParasites { get; set; }

        /// <summary>
        /// 是否处于感染风险状态
        /// true表示存在感染风险因素
        /// </summary>
        public bool m_HasParasiteRisk { get; set; }

        /// <summary>
        /// 当前感染概率(0-1范围)
        /// 根据卫生条件和饮食行为动态计算
        /// </summary>
        public float m_CurrentInfectionChance { get; set; }

        /// <summary>
        /// 感染持续时间(小时)
        /// 从确诊感染开始累计
        /// </summary>
        public float m_ParasitesElapsedHours { get; set; }

        /// <summary>
        /// 风险状态持续时间(小时)
        /// 从风险产生开始计时
        /// </summary>
        public float m_RiskElapsedHours { get; set; }

        /// <summary>
        /// 风险周期总时长(小时)
        /// 根据环境因素设定的风险窗口期
        /// </summary>
        public float m_RiskDurationHours { get; set; }

        /// <summary>
        /// 已服用驱虫药剂次数
        /// 当前治疗周期内的用药计数
        /// </summary>
        public int m_NumDosesTaken { get; set; }

        /// <summary>
        /// 今日是否已服药
        /// 防止重复用药的日级标记
        /// </summary>
        public bool m_HasTakenDoseToday { get; set; }

        /// <summary>
        /// 允许下次用药的游戏日
        /// 基于用药间隔规则计算
        /// </summary>
        public int m_DayToAllowNextDose { get; set; }

        /// <summary>
        /// 当前风险周期摄入污染食物计数
        /// 统计高风险饮食行为次数
        /// </summary>
        public int m_NumPiecesEatenThisRiskCycle { get; set; }
    }


    #region Log
    /// <summary>
    /// 游戏日志存档代理类
    /// 管理每日日志记录和全局笔记的持久化存储
    /// </summary>
    public class LogSaveDataProxy
    {
        /// <summary>
        /// 通用笔记文本
        /// 存储玩家自定义的长期记录
        /// </summary>
        public string m_GeneralNotes { get; set; }

        /// <summary>
        /// 历史日志日信息列表
        /// 按游戏日存储的结构化日志数据
        /// </summary>
        public List<LogDayInfo> m_LogDayInfoList { get; set; }

        /// <summary>
        /// 当日日志信息
        /// 当前游戏日的实时记录数据
        /// </summary>
        public LogDayInfo m_TodayLogDayInfo { get; set; }

        /// <summary>
        /// 日志结算日标记
        /// 标识需要执行日结算的游戏日编号
        /// </summary>
        public int m_DayToLogEndOfDayInfo { get; set; }
    }


    /// <summary>
    /// 每日日志信息数据结构
    /// 记录单个游戏日的完整活动轨迹和状态快照
    /// </summary>
    public class LogDayInfo
    {
        /// <summary>
        /// 游戏日编号 
        /// 对应游戏内日历系统的绝对天数
        /// </summary>
        public int m_DayNumber { get; set; }

        /// <summary>
        /// 自由格式日记内容
        /// 支持玩家输入的多行文本记录
        /// </summary>
        public string m_Notes { get; set; }

        /// <summary>
        /// 世界探索进度(%)
        /// 0-100表示已探索区域比例
        /// </summary>
        public int m_WorldExplored { get; set; }

        /// <summary>
        /// 休息时长(小时)
        /// 当日睡眠/休憩总时间
        /// </summary>
        public int m_HoursRested { get; set; }

        /// <summary>
        /// 最低身体状况值
        /// 当日生命值/体力值的最低记录
        /// </summary>
        public int m_ConditionLow { get; set; }

        /// <summary>
        /// 最高身体状况值
        /// 当日生命值/体力值的峰值记录
        /// </summary>
        public int m_ConditionHigh { get; set; }

        /// <summary>
        /// 卡路里消耗量
        /// 当日活动消耗的总能量值
        /// </summary>
        public int m_CaloriesBurned { get; set; }

        /// <summary>
        /// 患病状态记录表
        /// 使用EnumWrapper包装的疾病类型枚举集合
        /// </summary>
        public List<EnumWrapper<AfflictionType>> m_Afflictions { get; set; }

        /// <summary>
        /// 到访地点ID列表
        /// 存储Location系统的唯一标识符
        /// </summary>
        public List<string> m_LocationLocIDs { get; set; }

        /// <summary>
        /// 探索区域ID列表
        /// 存储Region系统的区域标识符
        /// </summary>
        public List<string> m_RegionLocIDs { get; set; }

        /// <summary>
        /// 区域场景名称列表
        /// 对应Unity场景资源的名称记录
        /// </summary>
        public List<string> m_RegionSceneNames { get; set; }
    }


    #endregion

    /// <summary>
    /// 休息系统存档代理类
    /// 管理睡眠数据和游戏时间流逝状态
    /// </summary>
    public class RestSaveDataProxy
    {
        /// <summary>
        /// 上次唤醒时显示的游戏日
        /// 用于检测跨日醒来的特殊情况
        /// </summary>
        public int m_LastDisplayedDayNumberOnAwake { get; set; }

        /// <summary>
        /// 最近24小时睡眠记录(分钟)
        /// 环形数组存储每小时段的睡眠时长
        /// </summary>
        public int[] m_LastTwentyFourHoursOfSleep { get; set; }

        /// <summary>
        /// 时间流逝锁定状态
        /// true时禁止任何形式的时间推进
        /// </summary>
        public bool m_PassTimeIsLocked { get; set; }
    }


    /// <summary>
    /// 飞越事件计时代理类
    /// 管理周期性飞越动画/事件的触发间隔
    /// </summary>
    public class FlyoverDataProxy
    {
        /// <summary>
        /// 距上次飞越的累计时间(秒)
        /// 实时更新的浮点计时器
        /// </summary>
        public float m_SecondsSinceLastFlyover { get; set; }

        /// <summary>
        /// 飞越事件触发间隔(秒)
        /// 可动态调整的间隔配置参数
        /// </summary>
        public float m_SecondsBetweenFlyovers { get; set; }
    }


    /// <summary>
    /// 成就保存数据类
    /// </summary>
    public class AchievementSaveData
    {
        /// <summary>
        /// 数据版本号
        /// </summary>
        public int m_Version { get; set; }

        /// <summary>
        /// 已生存的天数
        /// </summary>
        public int m_NumDaysSurvived { get; set; }

        /// <summary>
        /// 连续生存的夜晚数
        /// </summary>
        public int m_ConsecutiveNightsSurvived { get; set; }

        /// <summary>
        /// 完全收获的鹿数量
        /// </summary>
        public int m_FullyHarvestedDeer { get; set; }

        /// <summary>
        /// 是否在夜晚开始时在户外
        /// </summary>
        public bool m_StartedNightOutside { get; set; }

        /// <summary>
        /// 是否在该夜晚进入室内
        /// </summary>
        public bool m_WentInsideThisNight { get; set; }

        /// <summary>
        /// 是否开过枪
        /// </summary>
        public bool m_HasFiredGun { get; set; }

        /// <summary>
        /// 是否杀死过生物
        /// </summary>
        public bool m_HasKilledSomething { get; set; }

        /// <summary>
        /// 是否探索过湖区所有室内地点
        /// </summary>
        public bool m_LakeRegionAllInteriors { get; set; }

        /// <summary>
        /// 是否探索过沿海地区所有室内地点
        /// </summary>
        public bool m_CoastalRegionAllInteriors { get; set; }

        /// <summary>
        /// 靠土地生活的天数
        /// </summary>
        public int m_NumDaysLivingOffLand { get; set; }

        /// <summary>
        /// 是否使用过玫瑰果茶
        /// </summary>
        public bool m_UsedRosehipTea { get; set; }

        /// <summary>
        /// 是否使用过灵芝茶
        /// </summary>
        public bool m_UsedReishiTea { get; set; }

        /// <summary>
        /// 是否使用过老人须敷料
        /// </summary>
        public bool m_UsedOldMansBeardDressing { get; set; }

        /// <summary>
        /// 收获的玫瑰果植物数量
        /// </summary>
        public int m_NumRosehipPlantsHarvested { get; set; }

        /// <summary>
        /// 收获的灵芝植物数量
        /// </summary>
        public int m_NumReishiPlantsHarvested { get; set; }

        /// <summary>
        /// 收获的老人须植物数量
        /// </summary>
        public int m_NumOldMansPlantsHarvested { get; set; }

        /// <summary>
        /// 收获的香蒲植物数量
        /// </summary>
        public int m_NumCatTailPlantsHarvested { get; set; }

        /// <summary>
        /// 卡路里储备大于零的天数
        /// </summary>
        public int m_NumDaysCalorieStoreAboveZero { get; set; }

        /// <summary>
        /// 阅读的弓箭书籍数量
        /// </summary>
        public int m_NumArcheryBooksRead { get; set; }

        /// <summary>
        /// 阅读的动物尸体处理书籍数量
        /// </summary>
        public int m_NumCarcassHarvestingBooksRead { get; set; }

        /// <summary>
        /// 阅读的烹饪书籍数量
        /// </summary>
        public int m_NumCookingBooksRead { get; set; }

        /// <summary>
        /// 阅读的生火书籍数量
        /// </summary>
        public int m_NumFireStartingBooksRead { get; set; }

        /// <summary>
        /// 阅读的冰钓书籍数量
        /// </summary>
        public int m_NumIceFishingBooksRead { get; set; }

        /// <summary>
        /// 阅读的缝补书籍数量
        /// </summary>
        public int m_NumMendingBooksRead { get; set; }

        /// <summary>
        /// 阅读的步枪高级书籍数量
        /// </summary>
        public int m_NumRifleFirearmAdvancedBooksRead { get; set; }

        /// <summary>
        /// 阅读的步枪基础书籍数量
        /// </summary>
        public int m_NumRifleFirearmBooksRead { get; set; }

        /// <summary>
        /// 制作的简易小刀数量
        /// </summary>
        public int m_NumImprovisedKnivesCrafted { get; set; }

        /// <summary>
        /// 制作的简易斧头数量
        /// </summary>
        public int m_NumImprovisedHatchetsCrafted { get; set; }

        /// <summary>
        /// 最长的营火燃烧时间(小时)
        /// </summary>
        public int m_LongestBurningCampFire { get; set; }

        /// <summary>
        /// 是否找到第一章所有藏匿处
        /// </summary>
        public bool m_FoundAllCachesEpisodeOne { get; set; }

        /// <summary>
        /// 是否找到第二章所有藏匿处
        /// </summary>
        public bool m_FoundAllCachesEpisodeTwo { get; set; }

        /// <summary>
        /// 已绘制地图的区域字典(键为区域名称，值为是否已绘制)
        /// </summary>
        public Dictionary<string, bool> m_MappedRegions { get; set; }
    }

    /// <summary>
    /// 游戏体验模式管理器保存数据代理类
    /// </summary>
    public class ExperienceModeManagerSaveDataProxy
    {
        /// <summary>
        /// 当前游戏体验模式类型包装器
        /// <para>包含游戏当前所处的体验模式类型(如朝圣者、潜行者等)</para>
        /// </summary>
        public EnumWrapper<ExperienceModeType> m_CurrentModeType { get; set; }

        /// <summary>
        /// 自定义模式配置字符串
        /// <para>当选择自定义模式时，存储具体的自定义配置参数</para>
        /// </summary>
        public string m_CustomModeString { get; set; }
    }

    /// <summary>
    /// 玩家移动系统保存数据代理类
    /// </summary>
    public class PlayerMovementSaveDataProxy
    {
        /// <summary>
        /// 当前冲刺体力值
        /// <para>表示玩家当前可用于冲刺的体力值，范围通常为0-100</para>
        /// </summary>
        public float m_SprintStamina { get; set; }

        /// <summary>
        /// 强制移动状态包装器
        /// <para>包含玩家当前被强制执行的移动状态(如强制蹲下、强制跛行等)</para>
        /// </summary>
        public EnumWrapper<ForcedMovement> m_ForcedMovement { get; set; }

        /// <summary>
        /// 是否强制无扭伤状态
        /// <para>为true时表示玩家当前不会发生扭伤</para>
        /// </summary>
        public bool m_ForceNoSprain { get; set; }

        /// <summary>
        /// 是否处于蹲伏状态
        /// <para>表示玩家当前是否处于蹲伏移动状态</para>
        /// </summary>
        public bool m_IsCrouching { get; set; }
    }

    /// <summary>
    /// 音乐事件存档数据结构
    /// 管理游戏内情绪化背景音乐的触发计时
    /// </summary>
    public class MusicEventSaveData
    {
        /// <summary>
        /// 上次播放成功喜悦音效的游戏时间(秒)
        /// 用于控制同类音效的最小播放间隔
        /// </summary>
        public float m_HappySuccessLastPlayTime { get; set; }

        /// <summary>
        /// 上次播放悲伤情绪音效的游戏时间(秒)
        /// 防止悲伤音效频繁重复触发
        /// </summary>
        public float m_SorrowLastPlayTime { get; set; }

        /// <summary>
        /// 上次播放攀爬紧张音效的游戏时间(秒)
        /// 绳索攀爬场景专用音效记录
        /// </summary>
        public float m_RopeClimbStressLastPlayTime { get; set; }

        /// <summary>
        /// 上次播放被追踪音效的游戏时间(秒)
        /// 野兽追踪状态的特殊音效标记
        /// </summary>
        public float m_BeingStalkedLastPlayTime { get; set; }

        /// <summary>
        /// 带负面状态累计时长(小时)
        /// 影响疾病/伤痛相关的背景音乐强度
        /// </summary>
        public float m_NumHoursWithAffliction { get; set; }
    }


    /// <summary>
    /// 紧急兴奋剂效果参数配置
    /// 控制兴奋剂使用后的生理状态变化和冷却机制
    /// </summary>
    public class EmergencyStimParams
    {
        /// <summary>
        /// 上次使用时的游戏内时间(小时)
        /// 用于计算使用冷却时间
        /// </summary>
        public float m_LastUsageTimeInGameHours { get; set; }

        /// <summary>
        /// 使用后触发喘息音效的延迟秒数
        /// 模拟药物生效的生理反应时间
        /// </summary>
        public float m_PlayCatchBreathSecondsAfterBegin { get; set; }

        /// <summary>
        /// 注射后的有效刺激时长(小时)
        /// 决定药效持续时间
        /// </summary>
        public float m_HoursStimulatedWhenInjected { get; set; }

        /// <summary>
        /// 两次使用最小间隔(小时)
        /// 防止药物滥用机制
        /// </summary>
        public float m_MinHoursBetweenUsage { get; set; }

        /// <summary>
        /// 初始脉冲频率(Hz)
        /// 模拟心跳加速的起始强度
        /// </summary>
        public float m_StimPulseFrequencyStart { get; set; }

        /// <summary>
        /// 结束脉冲频率(Hz)
        /// 药效衰退时的心跳变化
        /// </summary>
        public float m_StimPulseFrequencyEnd { get; set; }

        /// <summary>
        /// 药效结束后的疲劳度增加值
        /// 模拟药物副作用
        /// </summary>
        public float m_FatigueIncreaseWhenComplete { get; set; }

        /// <summary>
        /// 药效结束后的耐力减少值
        /// 透支体能的代价
        /// </summary>
        public float m_StaminaDecreaseWhenComplete { get; set; }

        /// <summary>
        /// 注射音效资源ID
        /// 关联音频系统的唯一标识
        /// </summary>
        public uint m_AudioIDEmergencyStim { get; set; }
    }


    /// <summary>
    /// 雪景管理系统数据代理
    /// 负责同步不同场景的积雪状态记录
    /// </summary>
    public class SnowfallManagerSaveDataProxy
    {
        /// <summary>
        /// 已激活雪景效果的场景名称列表
        /// 元素示例: "CoastalHighway_Scene"
        /// </summary>
        public List<string> m_SceneNames { get; set; } = new List<string>();

        /// <summary>
        /// 场景积雪状态记录集合
        /// 格式说明: 
        /// 每条记录为"场景名|积雪厚度|最后更新时间"
        /// 示例: "MountainTown_Scene|0.35|2025.03.12-14:30"
        /// </summary>
        public List<string> m_Records { get; set; } = new List<string>();
    }


    /// <summary>
    /// 单场景雪况记录数据代理
    /// 存储当前场景的积雪深度状态
    /// </summary>
    public class SnowfallRecordSaveDataProxy
    {
        /// <summary>
        /// 当前积雪深度（单位：米）
        /// 取值范围：0.0f（无雪）~ 2.0f（齐腰深雪）
        /// 特殊值：-1.0f 表示场景未初始化雪况
        /// </summary>
        public float m_CurrentSnowDepth;
    }


    /// <summary>
    /// 任务服务管理器数据代理
    /// 负责序列化任务系统的运行时状态
    /// </summary>
    public class MissionServicesManagerSaveProxy
    {
        /// <summary>
        /// 序列化的任务数据集合
        /// 格式：JSON序列化的Mission对象数组
        /// </summary>
        public List<string> m_SerializedMissions { get; set; } = new List<string>();

        /// <summary>
        /// 序列化的并发任务关系图
        /// 描述任务间的依赖关系和执行顺序
        /// </summary>
        public List<string> m_SerializedConcurrentGraphs { get; set; } = new List<string>();

        /// <summary>
        /// 序列化的任务计时器状态
        /// 保存倒计时/限时任务的剩余时间
        /// </summary>
        public List<string> m_SerializedTimers { get; set; } = new List<string>();

        /// <summary>
        /// 任务对象过滤标签集合
        /// 用于快速检索特定类型的任务
        /// </summary>
        public List<string> m_MissionObjectFilterTags { get; set; } = new List<string>();

        /// <summary>
        /// 自定义托管对象标识列表
        /// 关联任务系统的扩展功能模块
        /// </summary>
        public List<string> m_CustomManagedObjects { get; set; } = new List<string>();

        /// <summary>
        /// 自定义托管对象状态集合
        /// 使用EnumWrapper实现枚举值的序列化
        /// </summary>
        public List<EnumWrapper<CustomManagedObjectState>> m_CustomManagedObjectStates { get; set; } = new List<EnumWrapper<CustomManagedObjectState>>();

        /// <summary>
        /// 全局黑板数据序列化字符串
        /// 存储任务系统的共享变量和决策数据
        /// </summary>
        public string m_SerializedGlobalBlackboard;

        /// <summary>
        /// 当前可见的任务计时器ID
        /// 用于UI层显示特定任务的倒计时
        /// </summary>
        public string m_VisibleMissionTimer { get; set; }
    }


    /// <summary>
    /// NPC信任管理系统数据容器
    /// 存储游戏内所有社交关系的动态状态
    /// </summary>
    public class TrustManagerSaveData
    {
        /// <summary>
        /// NPC信任值字典
        /// Key: NPC唯一标识符 Value: 当前信任等级(0-100)
        /// </summary>
        public Dictionary<string, int> m_TrustDictionary { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// NPC违规记录字典
        /// Key: NPC唯一标识符 Value: 累计违规次数
        /// </summary>
        public Dictionary<string, int> m_StrikesDictionary { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// 违规计时器字典
        /// Key: NPC唯一标识符 Value: 上次违规后的冷却时间(秒)
        /// </summary>
        public Dictionary<string, float> m_StrikeTimersDictionary { get; set; } = new Dictionary<string, float>();

        /// <summary>
        /// 序列化的需求追踪器
        /// Key: NPC类型 Value: JSON序列化的需求状态
        /// </summary>
        public Dictionary<string, string> m_NeedTrackersSerialized { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 序列化的可解锁内容追踪器
        /// Key: 内容分类 Value: JSON序列化的解锁进度
        /// </summary>
        public Dictionary<string, string> m_UnlockableTrackersSerialized { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 信任衰减速率字典
        /// Key: NPC唯一标识符 Value: 每日信任值衰减百分比(0.01-0.5)
        /// </summary>
        public Dictionary<string, float> m_TrustDecayDictionary { get; set; } = new Dictionary<string, float>();

        /// <summary>
        /// 宽限期计时器字典
        /// Key: 关系类型 Value: 剩余宽限时间(小时)
        /// </summary>
        public Dictionary<string, float> m_GracePeriodTimersDictionary { get; set; } = new Dictionary<string, float>();

        /// <summary>
        /// 宽限期阈值字典
        /// Key: 关系类型 Value: 触发宽限的信任阈值
        /// </summary>
        public Dictionary<string, float> m_GracePeriodValuesDictionary { get; set; } = new Dictionary<string, float>();
    }


    /// <summary>
    /// 地图细节解锁状态容器
    /// 记录每个地图元素的解锁情况
    /// </summary>
    public class MapDetailSaveData
    {
        /// <summary>
        /// 地图细节解锁状态字典
        /// Key: 细节元素ID Value: 是否已解锁
        /// </summary>
        public Dictionary<string, bool> m_MapDetailUnlockedStates { get; set; } = new Dictionary<string, bool>();
    }

    /// <summary>
    /// 世界地图全局状态容器
    /// 存储跨场景的地图解锁信息
    /// </summary>
    public class WorldMapSaveData
    {
        /// <summary>
        /// 已解锁的地图细节ID列表
        /// 使用List优化序列化性能
        /// </summary>
        public List<string> m_UnlockedDetails { get; set; } = new List<string>();
    }

    /// <summary>
    /// 地图系统核心数据容器
    /// 整合地图探索和区域解锁状态
    /// </summary>
    public class MapSaveData
    {
        /// <summary>
        /// 地图存档数据字典
        /// Key: 地图区域ID Value: JSON序列化的区域数据
        /// </summary>
        public Dictionary<string, string> m_MapSaveDataDict { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 细节调查位置记录
        /// Key: 调查点ID Value: 三维坐标字符串(x,y,z)
        /// </summary>
        public Dictionary<string, string> m_DetailSurveyPositions { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 细节调查时间戳
        /// Key: 调查点ID Value: 最后调查时间(Unix时间戳)
        /// </summary>
        public Dictionary<string, float> m_DetailSurveyLastUpdateTimes { get; set; } = new Dictionary<string, float>();

        /// <summary>
        /// 已解锁区域名称列表
        /// 使用List优化高频访问性能
        /// </summary>
        public List<string> m_UnlockedRegionNames { get; set; } = new List<string>();
    }


    /// <summary>
    /// 技能管理器存档数据容器
    /// 采用特性标记实现自定义反序列化逻辑
    /// </summary>
    public class SkillsManagerSaveData
    {
        /// <summary>
        /// 生火技能数据
        /// 特性参数：序列化字段名 + 是否必需
        /// </summary>
        [Deserialize("m_Skill_FirestartingSerialized", true)]
        public Skill_FirestartingSaveData Firestarting { get; set; }

        /// <summary>
        /// 动物尸体处理技能数据
        /// 对应生存类游戏的资源采集系统
        /// </summary>
        [Deserialize("m_Skill_CarcassHarvestingSerialized", true)]
        public Skill_CarcassHarvestingSaveData CarcassHarvesting { get; set; }

        /// <summary>
        /// 烹饪技能数据
        /// 存储食谱解锁状态和熟练度
        /// </summary>
        [Deserialize("m_Skill_CookingSerialized", true)]
        public Skill_CookingSaveData Cooking { get; set; }

        /// <summary>
        /// 冰钓技能数据
        /// 包含特殊环境下的技能参数
        /// </summary>
        [Deserialize("m_Skill_IceFishingSerialized", true)]
        public Skill_IceFishingSaveData IceFishing { get; set; }

        /// <summary>
        /// 步枪使用技能数据
        /// 记录武器精度和装弹速度等参数
        /// </summary>
        [Deserialize("m_Skill_RifleSerialized", true)]
        public Skill_RifleSaveData Rifle { get; set; }

        /// <summary>
        /// 射箭技能数据
        /// 包含弓箭拉力和准度修正值
        /// </summary>
        [Deserialize("m_Skill_ArcherySerialized", true)]
        public Skill_ArcherySaveData Archery { get; set; }

        /// <summary>
        /// 衣物修理技能数据
        /// 影响装备耐久度恢复效果
        /// </summary>
        [Deserialize("m_Skill_ClothingRepairSerialized", true)]
        public Skill_ClothingRepairSaveData ClothingRepair { get; set; }

        /// <summary>
        /// 左轮手枪技能数据
        /// 存储快速射击相关参数
        /// </summary>
        [Deserialize("m_Skill_RevolverSerialized", true)]
        public Skill_RevolverSaveData Revolver { get; set; }

        /// <summary>
        /// 枪械制造技能数据
        /// 控制武器改装解锁进度
        /// </summary>
        [Deserialize("m_Skill_GunsmithingSerialized", true)]
        public Skill_GunsmithingSaveData Gunsmith { get; set; }
    }


    /// <summary>
    /// 生火技能存档数据容器
    /// 记录玩家生火技能的进度状态
    /// </summary>
    public class Skill_FirestartingSaveData
    {
        /// <summary>
        /// 当前技能点数
        /// 范围：0-100（初级到大师级）
        /// 每10点解锁新技能等级
        /// </summary>
        public int m_Points { get; set; } = 0;
    }


    /// <summary>
    /// 动物尸体采集技能存档数据
    /// 实现基于时间投入的技能成长系统
    /// </summary>
    public class Skill_CarcassHarvestingSaveData
    {
        /// <summary>
        /// 当前技能点数（0-100）
        /// 影响可获取的物资数量和质量
        /// 等级划分：
        ///  0-20 新手（基础解剖）
        /// 21-50 熟练（获得稀有部位）
        /// 51-80 专家（双倍产出）
        /// 81-100 大师（完美处理）
        /// </summary>
        public int m_Points { get; set; } = 0;

        /// <summary>
        /// 待转换的技能经验小时数
        /// 每累计1.0小时自动转换为1技能点
        /// 采用浮点型保证时间累计精度
        /// </summary>
        public float m_NumHoursToConvertToSkillPoints { get; set; } = 0f;
    }


    /// <summary>
    /// 烹饪技能存档数据
    /// </summary>
    public class Skill_CookingSaveData
    {
        /// <summary>
        /// 当前技能点数
        /// </summary>
        public int m_Points { get; set; }
    }

    /// <summary>
    /// 冰钓技能存档数据
    /// </summary>
    public class Skill_IceFishingSaveData
    {
        /// <summary>
        /// 当前技能点数
        /// </summary>
        public int m_Points { get; set; }
    }

    /// <summary>
    /// 步枪技能存档数据
    /// </summary>
    public class Skill_RifleSaveData
    {
        /// <summary>
        /// 当前技能点数
        /// </summary>
        public int m_Points { get; set; }
    }

    /// <summary>
    /// 射箭技能存档数据
    /// </summary>
    public class Skill_ArcherySaveData
    {
        /// <summary>
        /// 当前技能点数
        /// </summary>
        public int m_Points { get; set; }
    }

    /// <summary>
    /// 衣物修理技能存档数据
    /// </summary>
    public class Skill_ClothingRepairSaveData
    {
        /// <summary>
        /// 当前技能点数
        /// </summary>
        public int m_Points { get; set; }
    }

    /// <summary>
    /// 左轮手枪技能存档数据
    /// </summary>
    public class Skill_RevolverSaveData
    {
        /// <summary>
        /// 当前技能点数
        /// </summary>
        public int m_Points { get; set; }
    }

    /// <summary>
    /// 枪械制造技能存档数据
    /// </summary>
    public class Skill_GunsmithingSaveData
    {
        /// <summary>
        /// 当前技能点数
        /// </summary>
        public int m_Points { get; set; }
    }


    /// <summary>
    /// 记录当前沙盒模式中已激活的特殊能力
    /// </summary>
    public class FeatEnabledTrackerSaveData
    {
        /// <summary>
        /// 本沙盒中已激活的能力枚举列表
        /// </summary>
        public List<EnumWrapper<FeatType>> m_FeatsEnabledThisSandbox { get; set; }
    }

    /// <summary>
    /// 石碑捐助者信息数据
    /// </summary>
    public class CairnInfo
    {
        /// <summary>
        /// 捐助者唯一识别编号
        /// </summary>
        public string m_BackerLookupNum { get; set; }

        /// <summary>
        /// 对应的日记条目编号
        /// </summary>
        public int m_JournalEntryNumber { get; set; }
    }

    /// <summary>
    /// 第一人称视角参数序列化
    /// </summary>
    public class SerializedParams
    {
        /// <summary>
        /// 是否启用手部模型显示
        /// </summary>
        public bool m_EnableFirstPersonHands { get; set; }

        /// <summary>
        /// 手部网格状态标识
        /// </summary>
        public string m_HandMeshState { get; set; }
    }

    /// <summary>
    /// 饱食状态数据代理
    /// </summary>
    public class WellFedSaveDataProxy
    {
        /// <summary>
        /// 是否处于饱食状态
        /// </summary>
        public bool m_Active { get; set; }

        /// <summary>
        /// 持续未饥饿的小时数
        /// </summary>
        public float m_ElapsedHoursNotStarving { get; set; }
    }

    /// <summary>
    /// 盒型碰撞体数据
    /// </summary>
    public class BoxCollider
    {
        /// <summary>
        /// 碰撞体中心坐标(x,y,z)
        /// </summary>
        public float[] center { get; set; }

        /// <summary>
        /// 碰撞体尺寸(width,height,depth)
        /// </summary>
        public float[] size { get; set; }

        /// <summary>
        /// 碰撞体扩展边界
        /// </summary>
        public float[] extents { get; set; }
    }

    /// <summary>
    /// 可序列化边界数据
    /// </summary>
    public class SerializableBounds
    {
        /// <summary>
        /// 边界中心点坐标
        /// </summary>
        public float[] m_Center { get; set; }

        /// <summary>
        /// 边界尺寸数据
        /// </summary>
        public float[] m_Size { get; set; }
    }

}
