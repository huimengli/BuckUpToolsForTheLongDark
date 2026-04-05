using System;
using System.Collections.Generic;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Serialization;

namespace TheLongDarkBuckupTools.MoreData
{
/// <summary>
/// 配置状态类，用于存储和管理游戏的各种配置信息
/// </summary>
    public class ProfileState
    {

    // 输入设备映射相关
        public List<string> m_RewiredKeyboardMap { get; set; } // 键盘映射列表
        public List<string> m_RewiredMouseMap { get; set; } // 鼠标映射列表

    
    // 游戏记录相关
        public List<SandboxRecord> m_SandboxRecords { get; set; } // 沙盒记录列表
        public List<EnumWrapper<UpSell>> m_UpsellsViewed { get; set; } // 已查看的推销列表
        public List<bool> m_DaysCompleted4DON { get; set; } // 已完成的每日任务列表(DON)
        public List<bool> m_DaysCompleted4DON2019 { get; set; } // 已完成的每日任务列表(DON2019)

    
    // 游戏版本和界面设置
        public int m_Version { get; set; } // 游戏版本号
        public bool m_ShowTimeOfDaySlider { get; set; } // 是否显示时间滑块
        public bool m_ShowFrametime { get; set; } // 是否显示帧时间

    
    // 音量设置
        public float m_MasterVolume { get; set; } // 主音量
        public float m_SoundVolume { get; set; } // 音效音量
        public float m_MusicVolume { get; set; } // 背景音乐音量
        public float m_VoiceVolume { get; set; } // 语音音量

    
    // 图形设置
        public int m_QualityLevel { get; set; } // 质量等级
        public EnumWrapper<GraphicsMode> m_GraphicsMode { get; set; } // 图形模式
        public int m_DisplayNumber { get; set; } // 显示器编号
        public Resolution m_Resolution { get; set; } // 分辨率设置
        public bool m_SSAOEnabled { get; set; } // 是否启用SSAO效果

    
    // 单位和界面偏好设置
        public EnumWrapper<MeasurementUnits> m_Units { get; set; } // 单位设置
        public EnumWrapper<HudPref> m_HudPref { get; set; } // HUD偏好设置
        public EnumWrapper<HudSize> m_HudSize { get; set; } // HUD大小设置
        public EnumWrapper<HudType> m_HudType { get; set; } // HUD类型设置

    
    // 控制设置
        public bool m_InvertY { get; set; } // 是否反转Y轴
        public bool m_InvertX { get; set; } // 是否反转X轴
        public bool m_LockMouseToScreen { get; set; } // 是否锁定鼠标到屏幕
        public bool m_EnableGamepad { get; set; } // 是否启用手柄
    // 灵敏度设置（已弃用的属性）
        [Obsolete("Use m_MouseSensitivityPercentage instead")]
        public float[] m_MouseSensitivity { get; set; } // 鼠标灵敏度（已弃用）
        [Obsolete("Use m_ZoomSensitivityPercentage instead")]
        public float[] m_ZoomSensitivity { get; set; } // 缩放灵敏度（已弃用）
        [Obsolete("Use m_GamepadCameraSensitivityPercentage instead")]
        public float[] m_AnalogSticksSensitivity { get; set; } // 摇杆灵敏度（已弃用）

    
    // 新的灵敏度设置
        public float m_MouseSensitivityPercentage { get; set; } // 鼠标灵敏度百分比
        public float m_ZoomSensitivityPercentage { get; set; } // 缩放灵敏度百分比
        public float m_GamepadCameraSensitivityPercentage { get; set; } // 手柄相机灵敏度百分比

    
    // 游戏设置
        public bool m_ConsoleUnlocked { get; set; } // 是否解锁控制台
        public float m_FieldOfView { get; set; } // 视野角度
        public int m_NumGamesPlayed { get; set; } // 已玩游戏数量

    
    // 语音和区域设置
        public EnumWrapper<VoicePersona> m_VoicePersona { get; set; } // 语音角色
        public EnumWrapper<GameRegion> m_StartRegion { get; set; } // 起始区域
        public Dictionary<string, string> m_KeyBindings { get; set; } // 按键绑定

    
    // 显示设置
        public bool m_VsyncEnabled { get; set; } // 是否启用垂直同步
        public EnumWrapper<SubtitlesState> m_SubtitlesState { get; set; } // 字幕状态
        public EnumWrapper<LanguageState> m_LanguageState { get; set; } // 语言状态
        public string m_Language { get; set; } // 语言设置

    
    // 区域解锁状态
        public bool m_CoastalRegionLocked { get; set; } // 海岸区域是否锁定
        public bool m_RuralRegionLocked { get; set; } // 乡村区域是否锁定
        public bool m_WhalingStationRegionLocked { get; set; } // 捕鲸站区域是否锁定
        public bool m_CrashMountainRegionLocked { get; set; } // 坠机山脉区域是否锁定

    
    // 其他设置
        public bool m_FrameDumperUnlocked { get; set; } // 是否解锁帧转储器
        public bool m_HasSeenIntroVideo { get; set; } // 是否看过介绍视频
        public bool m_NoResumeSave { get; set; } // 是否禁用继续保存
        public string m_AllTimeStats { get; set; } // 全部时间统计

    
    // 最佳时间记录
        public float m_BestTimeHunted { get; set; } // 最佳狩猎时间
        public float m_BestTimeRescue { get; set; } // 最佳救援时间
        public float m_BestTimeWhiteout { get; set; } // 最佳白化时间
        public float m_BestTimeNomad { get; set; } // 最佳游牧时间
        public float m_BestTimeHunted2 { get; set; } // 最佳狩猎时间2
        public float m_BestTimeArchivist { get; set; } // 最佳档案管理员时间

    
    // 游戏模式设置
        public EnumWrapper<ExperienceModeType> m_MostRecentSandboxMode { get; set; } // 最近沙盒模式
        public EnumWrapper<ExperienceModeType> m_MostRecentChallengeMode { get; set; } // 最近挑战模式
        public EnumWrapper<ExperienceModeType> m_MostRecentEpisodeMode { get; set; } // 最近剧集模式

    
    // 显示设置
        public float m_Brightness { get; set; } // 亮度设置
        public bool m_DoneBrightnessAdjustment { get; set; } // 是否完成亮度调整

    
    // 成就和徽章
        public List<string> m_UnlockedBadgesViewed { get; set; } // 已查看的解锁徽章
        public HashSet<string> m_CinematicsViewed { get; set; } // 已观看的过场动画
        [Deserialize("m_FeatsSerialized", true)]
        public FeatsManagerSaveData Feats { get; set; } // 成就管理器保存数据

    
    // 序列化数据
        public string m_EpisodeManagerSerialized { get; set; } // 剧集管理器序列化数据
        public string m_QualityLevelSettingsSerialized { get; set; } // 质量等级设置序列化数据

    
    // 游戏设置
        public bool m_DisableClickHold { get; set; } // 是否禁用点击按住
        public int m_AutosaveMinutes { get; set; } // 自动保存间隔（分钟）
        public string m_NewGameCustomModeString { get; set; } // 新游戏自定义模式字符串

    
    // 缓存发现状态
        public bool m_FoundAllCachesEpisodeOne { get; set; } // 是否找到所有第一集缓存
        public bool m_FoundAllCachesEpisodeTwo { get; set; } // 是否找到所有第二集缓存

    
    // 成就设置
        public List<EnumWrapper<Achievement>> m_UnlockedAchievements { get; set; } // 已解锁成就

    
    // 显示设置
        public bool m_ReduceCameraMotion { get; set; } // 是否减少相机运动
        public bool m_LargeSubtitles { get; set; } // 是否使用大字幕

    
    // 通知设置
        public HashSet<string> m_ViewedNotifications { get; set; } // 已查看的通知
    }

/// <summary>
/// 分辨率类，用于存储显示设备的分辨率信息
/// 包含宽度、高度和刷新率三个属性
/// </summary>
    public class Resolution
    {
    /// <summary>
    /// 获取或设置显示设备的宽度（以像素为单位）
    /// </summary>
        public int m_Width { get; set; }
    /// <summary>
    /// 获取或设置显示设备的高度（以像素为单位）
    /// </summary>
        public int m_Height { get; set; }
    /// <summary>
    /// 获取或设置显示设备的刷新率（以赫兹Hz为单位）
    /// </summary>
        public int m_RefreshRate { get; set; }
    }

/// <summary>
/// 特性管理器保存数据类，用于存储游戏中各种特性的保存数据
/// </summary>
    public class FeatsManagerSaveData
    {
    /// <summary>
    /// 博学特性保存数据，通过[Deserialize]特性标记反序列化时的属性名
    /// </summary>
        [Deserialize("m_Feat_BookSmartsSerialized", true)]
        public Feat_BookSmartsSaveData BookSmarts { get; set; }
    /// <summary>
    /// 冷聚变特性保存数据，通过[Deserialize]特性标记反序列化时的属性名
    /// </summary>
        [Deserialize("m_Feat_ColdFusionSerialized", true)]
        public Feat_ColdFusionSaveData ColdFusion { get; set; }
    /// <summary>
    /// 高效机器特性保存数据，通过[Deserialize]特性标记反序列化时的属性名
    /// </summary>
        [Deserialize("m_Feat_EfficientMachineSerialized", true)]
        public Feat_EfficientMachineSaveData EfficientMachine { get; set; }
    /// <summary>
    /// 火焰大师特性保存数据，通过[Deserialize]特性标记反序列化时的属性名
    /// </summary>
        [Deserialize("m_Feat_FireMasterSerialized", true)]
        public Feat_FireMasterSaveData FireMaster { get; set; }
    /// <summary>
    /// 自由奔跑者特性保存数据，通过[Deserialize]特性标记反序列化时的属性名
    /// </summary>
        [Deserialize("m_Feat_FreeRunnerSerialized", true)]
        public Feat_FreeRunnerSaveData FreeRunner { get; set; }
    /// <summary>
    /// 雪地行走者特性保存数据，通过[Deserialize]特性标记反序列化时的属性名
    /// </summary>
        [Deserialize("m_Feat_SnowWalkerSerialized", true)]
        public Feat_SnowWalkerSaveData SnowWalker { get; set; }
        [Deserialize("m_Feat_ExpertTrappererialized", true)]
        public Feat_ExpertTrapperSaveData ExpertTrapper { get; set; }
        [Deserialize("m_Feat_StraightToHeartSerialized", true)]
        public Feat_StraightToHeartSaveData StraightToHeart { get; set; }
        [Deserialize("m_Feat_BlizzardWalkerSerialized", true)]
        public Feat_BlizzardWalkerSaveData BlizzardWalker { get; set; }
    }

/// <summary>
/// 用于存储"书呆子"（BookSmarts）特性的数据类
/// 该特性可能与角色在研究或学习方面的发展有关
/// </summary>
    public class Feat_BookSmartsSaveData
    {
    /// <summary>
    /// 存储角色进行研究的总小时数
    /// 这个属性用于追踪或记录角色在游戏中的研究进度
    /// </summary>
        public int m_HoursResearch { get; set; }
    }

/// <summary>
/// 冷聚变保存数据类，用于存储与冷聚变相关的游戏进度数据
/// </summary>
    public class Feat_ColdFusionSaveData
    {
    /// <summary>
    /// 已过去的天数，用于记录游戏内经过的时间
    /// </summary>
        public float m_ElapsedDays { get; set; }
    /// <summary>
    /// 累计的小时数，用于精确计算游戏内经过的时间
    /// </summary>
        public float m_HoursAccumulator { get; set; }
    }

/// <summary>
/// 高效机器存档数据类
/// 用于存储高效机器的运行时间和累计时间数据
/// </summary>
    public class Feat_EfficientMachineSaveData
    {
    /// <summary>
    /// 已消耗的小时数
    /// 表示机器已运行的总小时数
    /// </summary>
        public float m_ElapsedHours { get; set; }
    /// <summary>
    /// 累计小时数
    /// 用于存储或计算某种累计值的小时数
    /// </summary>
        public float m_HoursAccumulator { get; set; }
    }

/// <summary>
/// 火焰大师技能的存档数据类
/// 用于记录玩家在火焰大师技能方面的游戏进度数据
/// </summary>
    public class Feat_FireMasterSaveData
    {
    /// <summary>
    /// 玩家已点燃的火焰数量
    /// 用于追踪玩家使用火焰大师技能的次数
    /// </summary>
        public int m_NumFiresStarted { get; set; }
    }

/// <summary>
/// 自由奔跑者(Free Runner)游戏功能的保存数据类
/// 用于存储玩家在自由奔跑模式中的累计数据
/// </summary>
    public class Feat_FreeRunnerSaveData
    {
    /// <summary>
    /// 已跑过的总公里数
    /// </summary>
        public float m_ElapsedKilometers { get; set; }
    /// <summary>
    /// 米数累加器
    /// 用于精确计算距离的小数部分，当达到1000米时将增加1公里
    /// </summary>
        public float m_MetersAccumulator { get; set; }
    }

/// <summary>
/// 雪行者(Snow Walker)特性的保存数据类
/// 用于记录和保存雪行者特性的相关数据
/// </summary>
    public class Feat_SnowWalkerSaveData
    {
    /// <summary>
    /// 已经过的公里数
    /// 用于累计角色行走的总距离（以公里为单位）
    /// </summary>
        public float m_ElapsedKilometers { get; set; }
    /// <summary>
    /// 米数累加器
    /// 用于精确计算行走距离的米数部分（与公里数结合使用）
    /// </summary>
        public float m_MetersAccumulator { get; set; }
    }

/// <summary>
/// 专家陷阱师(Expert Trapper)功能的保存数据类
/// 用于追踪和存储玩家在游戏中使用陷阱的相关数据
/// </summary>
    public class Feat_ExpertTrapperSaveData
    {
    /// <summary>
    /// 已设置的陷阱数量
    /// 用于记录玩家成功设置的兔子陷阱总数
    /// </summary>
        public int m_RabbitSnaredCount { get; set; }
    }
/// <summary>
/// 表示"直击心灵"功能的保存数据类
/// 用于存储和追踪与"直击心灵"功能相关的数据
/// </summary>
    public class Feat_StraightToHeartSaveData
    {
    /// <summary>
    /// 已消耗的物品数量
    /// 用于记录在"直击心灵"功能中已使用的物品总数
    /// </summary>
        public int m_ItemConsumedCount { get; set; }
    }
/// <summary>
/// 暴雪行者(Blizzard Walker)特性的存档数据类
/// 用于保存和加载角色在暴雪环境下的行走相关数据
/// </summary>
    public class Feat_BlizzardWalkerSaveData
    {
    /// <summary>
    /// 角色在暴雪环境外的累计小时数
    /// 用于计算角色离开暴雪环境的时间
    /// </summary>
        public float m_BlizzardHoursOutside { get; set; }
    /// <summary>
    /// 角色在暴雪环境外的小时数累加器
    /// 用于累计计算角色离开暴雪环境的总时间
    /// </summary>
        public float m_BlizzardHoursOutsideAccumulator { get; set; }
    }

/// <summary>
/// 统计数据容器类，用于存储和管理各种统计信息
/// </summary>
    public class StatContainer
    {
    /// <summary>
    /// 缓存的哈希ID数组，用于存储标识符的集合
    /// </summary>
        public int[] m_CachedHashIds { get; set; }
    /// <summary>
    /// 统计数据的字典，键为字符串类型的标识，值为字符串类型的统计值
    /// </summary>
        public Dictionary<string, string> m_StatsDictionary { get; set; }
    /// <summary>
    /// 沿海地区被烧毁房屋的数量统计
    /// </summary>
        public int m_NumBurntHousesInCoastal { get; set; }
    /// <summary>
    /// 标记是否已完成沿海地区被烧毁房屋的检查
    /// </summary>
        public bool m_HasDoneCoastalBurntHouseCheck { get; set; }
    /// <summary>
    /// 标记是否已完成正确被烧毁房屋的检查
    /// </summary>
        public bool m_HasDoneCorrectBurntHouseCheck { get; set; }
    }

/// <summary>
/// 沙盒记录类，用于存储一个沙盒游戏会话的所有相关信息
/// </summary>
    public class SandboxRecord
    {
    /// <summary>
    /// 沙盒名称
    /// </summary>
        public string m_SandboxName { get; set; }
    /// <summary>
    /// 已用游戏时间（小时）
    /// </summary>
        public float m_ElapsedHours { get; set; }
    /// <summary>
    /// 结束日期
    /// </summary>
        public string m_EndDate { get; set; }
    /// <summary>
    /// 开始区域（使用枚举包装器）
    /// </summary>
        public EnumWrapper<GameRegion> m_StartRegion { get; set; }
    /// <summary>
    /// 结束区域
    /// </summary>
        public string m_EndRegion { get; set; }
    /// <summary>
    /// 经验模式类型（使用枚举包装器）
    /// </summary>
        public EnumWrapper<ExperienceModeType> m_ExperienceModeType { get; set; }
    /// <summary>
    /// 声音人格（使用枚举包装器）
    /// </summary>
        public EnumWrapper<VoicePersona> m_VoicePersona { get; set; }
    /// <summary>
    /// 死亡原因的位置ID
    /// </summary>
        public string m_CauseOfDeathLocId { get; set; }
    /// <summary>
    /// 通用备注
    /// </summary>
        public string m_GeneralNotes { get; set; }
    /// <summary>
    /// 每日日志信息列表
    /// </summary>
        public List<LogDayInfo> m_LogDayInfoList { get; set; }
        // TODO: check if dynamic works correctly
        public List<dynamic> m_CollectibleList { get; set; }
        public List<string> m_CollectibleNotesList { get; set; }
        public List<CairnInfo> m_CollectibleCairnInfoList { get; set; }
        public StatContainer m_Stats { get; set; }
    }
}
