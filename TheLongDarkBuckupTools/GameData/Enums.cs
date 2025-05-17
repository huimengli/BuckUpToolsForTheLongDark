namespace TheLongDarkBuckupTools.GameData
{
    /// <summary>
    /// 游戏模式
    /// </summary>
    public enum GameMode
    {
        /// <summary>
        /// 故事模式
        /// </summary>
        STORY,
        /// <summary>
        /// 检查点
        /// </summary>
        CHECKPOINT,
        /// <summary>
        /// 沙盒模式
        /// </summary>
        SANDBOX,
    }

    // ----- My enums

    /// <summary>
    /// 物品类别
    /// </summary>
    public enum ItemCategory
    {
        /// <summary>
        /// 急救物品
        /// </summary>
        FirstAid,
        /// <summary>
        /// 衣物
        /// </summary>
        Clothing,
        /// <summary>
        /// 食物
        /// </summary>
        Food,
        /// <summary>
        /// 工具
        /// </summary>
        Tools,
        /// <summary>
        /// 材料
        /// </summary>
        Materials,
        /// <summary>
        /// 收藏品
        /// </summary>
        Collectible,
        /// <summary>
        /// 书籍
        /// </summary>
        Books,
        /// <summary>
        /// 隐藏物品
        /// </summary>
        Hidden,
        /// <summary>
        /// 未知物品
        /// </summary>
        Unknown
    }

    /// <summary>
    /// 有地图的区域
    /// </summary>
    public enum RegionsWithMap
    {
        /// <summary>
        /// 沿海地区
        /// </summary>
        CoastalRegion,
        /// <summary>
        /// 湖区
        /// </summary>
        LakeRegion,
        /// <summary>
        /// 捕鲸站地区
        /// </summary>
        WhalingStationRegion,
        /// <summary>
        /// 乡村地区
        /// </summary>
        RuralRegion,
        /// <summary>
        /// 坠机山区
        /// </summary>
        CrashMountainRegion,
        /// <summary>
        /// 沼泽地区
        /// </summary>
        MarshRegion,
        /// <summary>
        /// 峡谷过渡区
        /// </summary>
        RavineTransitionZone,
        /// <summary>
        /// 公路过渡区
        /// </summary>
        HighwayTransitionZone,
        /// <summary>
        /// 铁轨地区
        /// </summary>
        TracksRegion,
        /// <summary>
        /// 河谷地区
        /// </summary>
        RiverValleyRegion,
        /// <summary>
        /// 山镇地区
        /// </summary>
        MountainTownRegion,
        /// <summary>
        /// 罐头厂地区
        /// </summary>
        CanneryRegion,
        /// <summary>
        /// 灰烬峡谷地区
        /// </summary>
        AshCanyonRegion
    }

    // -----

    /// <summary>
    /// 风向
    /// </summary>
    public enum WindDirection
    {
        /// <summary>
        /// 北
        /// </summary>
        North,
        /// <summary>
        /// 南
        /// </summary>
        South,
        /// <summary>
        /// 西
        /// </summary>
        West,
        /// <summary>
        /// 东
        /// </summary>
        East,
        /// <summary>
        /// 西北
        /// </summary>
        NorthWest,
        /// <summary>
        /// 东北
        /// </summary>
        NorthEast,
        /// <summary>
        /// 西南
        /// </summary>
        SouthWest,
        /// <summary>
        /// 东南
        /// </summary>
        SouthEast,
    }

    /// <summary>
    /// 风力强度
    /// </summary>
    public enum WindStrength
    {
        /// <summary>
        /// 平静
        /// </summary>
        Calm,
        /// <summary>
        /// 微风
        /// </summary>
        SlightlyWindy,
        /// <summary>
        /// 大风
        /// </summary>
        Windy,
        /// <summary>
        /// 强风
        /// </summary>
        VeryWindy,
        /// <summary>
        /// 暴风雪
        /// </summary>
        Blizzard,
    }

    /// <summary>
    /// 天气阶段
    /// </summary>
    public enum WeatherStage
    {
        /// <summary>
        /// 浓雾
        /// </summary>
        DenseFog = 0,
        /// <summary>
        /// 小雪
        /// </summary>
        LightSnow = 1,
        /// <summary>
        /// 大雪
        /// </summary>
        HeavySnow = 2,
        /// <summary>
        /// 局部多云
        /// </summary>
        PartlyCloudy = 3,
        /// <summary>
        /// 晴朗
        /// </summary>
        Clear = 4,
        /// <summary>
        /// 多云
        /// </summary>
        Cloudy = 5,
        /// <summary>
        /// 薄雾
        /// </summary>
        LightFog = 6,
        /// <summary>
        /// 暴风雪
        /// </summary>
        Blizzard = 7,
        /// <summary>
        /// 极光晴朗
        /// </summary>
        ClearAurora = 8,
        /// <summary>
        /// 数量(占位符)
        /// </summary>
        Num = 9,
        /// <summary>
        /// 未定义
        /// </summary>
        Undefined = 9,
    }

    /// <summary>
    /// 身体状况等级
    /// </summary>
    public enum ConditionLevel
    {
        /// <summary>
        /// 无伤
        /// </summary>
        NoInjuries,
        /// <summary>
        /// 轻伤
        /// </summary>
        SlightlyInjured,
        /// <summary>
        /// 受伤
        /// </summary>
        Injured,
        /// <summary>
        /// 重伤
        /// </summary>
        VeryInjured,
        /// <summary>
        /// 濒死
        /// </summary>
        NearDeath,
    }

    /// <summary>
    /// 负重等级
    /// </summary>
    public enum EncumberLevel
    {
        /// <summary>
        /// 无负重
        /// </summary>
        None,
        /// <summary>
        /// 轻度负重
        /// </summary>
        Low,
        /// <summary>
        /// 中度负重
        /// </summary>
        Medium,
        /// <summary>
        /// 重度负重
        /// </summary>
        High,
    }

    /// <summary>
    /// 饥饿等级
    /// </summary>
    public enum HungerLevel
    {
        /// <summary>
        /// 饱腹
        /// </summary>
        Full,
        /// <summary>
        /// 轻微饥饿
        /// </summary>
        SlightlyHungry,
        /// <summary>
        /// 饥饿
        /// </summary>
        Hungry,
        /// <summary>
        /// 非常饥饿
        /// </summary>
        VeryHungry,
        /// <summary>
        /// 饥饿致死
        /// </summary>
        Starving,
    }

    /// <summary>
    /// 口渴等级
    /// </summary>
    public enum ThirstLevel
    {
        /// <summary>
        /// 水分充足
        /// </summary>
        Hydrated,
        /// <summary>
        /// 轻微口渴
        /// </summary>
        SlightlyThirsty,
        /// <summary>
        /// 口渴
        /// </summary>
        Thirsty,
        /// <summary>
        /// 非常口渴
        /// </summary>
        VeryThirsty,
        /// <summary>
        /// 脱水
        /// </summary>
        Dehydrated,
    }

    /// <summary>
    /// 疲劳等级
    /// </summary>
    public enum FatigueLevel
    {
        /// <summary>
        /// 休息充足
        /// </summary>
        Rested,
        /// <summary>
        /// 轻微疲劳
        /// </summary>
        SlightlyTired,
        /// <summary>
        /// 疲劳
        /// </summary>
        Tired,
        /// <summary>
        /// 非常疲劳
        /// </summary>
        VeryTired,
        /// <summary>
        /// 精疲力尽
        /// </summary>
        Exhausted,
    }

    /// <summary>
    /// 寒冷等级
    /// </summary>
    public enum FreezingLevel
    {
        /// <summary>
        /// 温暖
        /// </summary>
        Warm,
        /// <summary>
        /// 微冷
        /// </summary>
        SlightlyCold,
        /// <summary>
        /// 寒冷
        /// </summary>
        Cold,
        /// <summary>
        /// 非常寒冷
        /// </summary>
        VeryCold,
        /// <summary>
        /// 冻僵
        /// </summary>
        Freezing,
    }

    /// <summary>
    /// 液体质量
    /// </summary>
    public enum LiquidQuality
    {
        /// <summary>
        /// 可饮用
        /// </summary>
        Potable,
        /// <summary>
        /// 不可饮用
        /// </summary>
        NonPotable,
    }

    /// <summary>
    /// 信号弹状态
    /// </summary>
    public enum FlareState
    {
        /// <summary>
        /// 全新
        /// </summary>
        Fresh,
        /// <summary>
        /// 燃烧中
        /// </summary>
        Burning,
        /// <summary>
        /// 燃尽
        /// </summary>
        BurnedOut,
        /// <summary>
        /// 潮湿
        /// </summary>
        Wet
    }

    /// <summary>
    /// 睡袋状态
    /// </summary>
    public enum BedRollState
    {
        /// <summary>
        /// 卷起
        /// </summary>
        Rolled,
        /// <summary>
        /// 放置
        /// </summary>
        Placed,
    }

    /// <summary>
    /// 陷阱状态
    /// </summary>
    public enum SnareState
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default,
        /// <summary>
        /// 已设置
        /// </summary>
        Set,
        /// <summary>
        /// 损坏
        /// </summary>
        Broken,
        /// <summary>
        /// 捕获兔子
        /// </summary>
        WithRabbit,
    }

    /// <summary>
    /// 火炬状态
    /// </summary>
    public enum TorchState
    {
        /// <summary>
        /// 全新
        /// </summary>
        Fresh,
        /// <summary>
        /// 燃烧中
        /// </summary>
        Burning,
        /// <summary>
        /// 被吹灭
        /// </summary>
        BlownOut,
        /// <summary>
        /// 燃尽
        /// </summary>
        BurnedOut,
        /// <summary>
        /// 熄灭
        /// </summary>
        Extinguished,
        /// <summary>
        /// 潮湿
        /// </summary>
        Wet
    }

    /// <summary>
    /// 任务对象类别
    /// </summary>
    public enum MissionObjectClass
    {
        /// <summary>
        /// 一般
        /// </summary>
        General,
        /// <summary>
        /// 触发器
        /// </summary>
        Trigger,
        /// <summary>
        /// 玩家
        /// </summary>
        Player,
        /// <summary>
        /// 可交互NPC
        /// </summary>
        InteractiveNPC,
        /// <summary>
        /// 可交互对象
        /// </summary>
        InteractiveObject,
        /// <summary>
        /// 无效
        /// </summary>
        Invalid,
    }

    /// <summary>
    /// 语音角色
    /// </summary>
    public enum VoicePersona
    {
        /// <summary>
        /// 男性
        /// </summary>
        Male,
        /// <summary>
        /// 女性
        /// </summary>
        Female,
    }

    /// <summary>
    /// 喷雾颜色
    /// </summary>
    public enum SprayColor
    {
        /// <summary>
        /// 橙色
        /// </summary>
        Orange
    }

    /// <summary>
    /// 疾病类型
    /// </summary>
    public enum AfflictionType
    {
        /// <summary>
        /// 失血
        /// </summary>
        BloodLoss,
        /// <summary>
        /// 烧伤
        /// </summary>
        Burns,
        /// <summary>
        /// 痢疾
        /// </summary>
        Dysentery,
        /// <summary>
        /// 感染
        /// </summary>
        Infection,
        /// <summary>
        /// 食物中毒
        /// </summary>
        FoodPoisioning,
        /// <summary>
        /// 脚踝扭伤
        /// </summary>
        SprainedAnkle,
        /// <summary>
        /// 感染风险
        /// </summary>
        InfectionRisk,
        /// <summary>
        /// 手腕扭伤
        /// </summary>
        SprainedWrist,
        /// <summary>
        /// 冻伤
        /// </summary>
        Frostbite,
        /// <summary>
        /// 冻伤伤害
        /// </summary>
        FrostbiteDamage,
        /// <summary>
        /// 低温症
        /// </summary>
        Hypothermia,
        /// <summary>
        /// 减少疲劳
        /// </summary>
        ReducedFatigue,
        /// <summary>
        /// 改善休息
        /// </summary>
        ImprovedRest,
        /// <summary>
        /// 升温中
        /// </summary>
        WarmingUp,
        /// <summary>
        /// 低温症风险
        /// </summary>
        HypothermiaRisk,
        /// <summary>
        /// 幽居病
        /// </summary>
        CabinFever,
        /// <summary>
        /// 肠道寄生虫风险
        /// </summary>
        IntestinalParasitesRisk,
        /// <summary>
        /// 肠道寄生虫
        /// </summary>
        IntestinalParasites,
        /// <summary>
        /// 严重手腕扭伤
        /// </summary>
        SprainedWristMajor,
        /// <summary>
        /// 幽居病风险
        /// </summary>
        CabinFeverRisk,
        /// <summary>
        /// 冻伤风险
        /// </summary>
        FrostbiteRisk,
        /// <summary>
        /// 电击烧伤
        /// </summary>
        BurnsElectric,
        /// <summary>
        /// 肋骨骨折
        /// </summary>
        BrokenRib,
        /// <summary>
        /// 饱食状态
        /// </summary>
        WellFed
    }

    /// <summary>
    /// 体验模式类型
    /// </summary>
    public enum ExperienceModeType
    {
        /// <summary>
        /// 朝圣者(简单)
        /// </summary>
        Pilgrim,
        /// <summary>
        /// 旅行者(中等)
        /// </summary>
        Voyageur,
        /// <summary>
        /// 潜行者(困难)
        /// </summary>
        Stalker,
        /// <summary>
        /// 故事模式
        /// </summary>
        Story,
        /// <summary>
        /// 挑战:救援
        /// </summary>
        ChallengeRescue,
        /// <summary>
        /// 挑战:被猎杀
        /// </summary>
        ChallengeHunted,
        /// <summary>
        /// 挑战:白茫
        /// </summary>
        ChallengeWhiteout,
        /// <summary>
        /// 挑战:游牧
        /// </summary>
        ChallengeNomad,
        /// <summary>
        /// 挑战:被猎杀2
        /// </summary>
        ChallengeHuntedPart2,
        /// <summary>
        /// 闯入者(极难)
        /// </summary>
        Interloper,
        /// <summary>
        /// 自定义
        /// </summary>
        Custom,
        /// <summary>
        /// 故事模式:新手
        /// </summary>
        StoryFresh,
        /// <summary>
        /// 故事模式:老手
        /// </summary>
        StoryHardened,
        /// <summary>
        /// 四日之夜
        /// </summary>
        FourDaysOfNight,
        /// <summary>
        /// 挑战:档案管理员
        /// </summary>
        ChallengeArchivist,
        /// <summary>
        /// 挑战:行尸走肉
        /// </summary>
        ChallengeDeadManWalk,
    }

    /// <summary>
    /// 统计ID
    /// </summary>
    public enum StatID
    {
        /// <summary>
        /// 生存小时数
        /// </summary>
        HoursSurvived,
        /// <summary>
        /// 探索地点数
        /// </summary>
        LocationsExplored,
        /// <summary>
        /// 世界探索百分比
        /// </summary>
        WorldExploredPercentage,
        /// <summary>
        /// 消耗总卡路里
        /// </summary>
        TotalCaloriesExpended,
        /// <summary>
        /// 日均卡路里消耗
        /// </summary>
        AverageCaloriesPerDay,
        /// <summary>
        /// 旅行距离
        /// </summary>
        DistanceTravelled,
        /// <summary>
        /// 清醒小时数
        /// </summary>
        HoursAwake,
        /// <summary>
        /// 睡眠小时数
        /// </summary>
        HoursAsleep,
        /// <summary>
        /// 室内小时数
        /// </summary>
        HoursIndoors,
        /// <summary>
        /// 室外小时数
        /// </summary>
        HoursOutdoors,
        /// <summary>
        /// 户外休息时间
        /// </summary>
        TimeRestedOutdoors,
        /// <summary>
        /// 神秘湖小时数
        /// </summary>
        HoursInMysteryLake,
        /// <summary>
        /// 怡人山谷小时数
        /// </summary>
        HoursInPleasantValley,
        /// <summary>
        /// 沿海公路小时数
        /// </summary>
        HoursInCoastalHighway,
        /// <summary>
        /// 荒凉据点小时数
        /// </summary>
        HoursInDesolationPoint,
        /// <summary>
        /// 坠机山区小时数
        /// </summary>
        HoursInCrashMountainRegion,
        /// <summary>
        /// 成功修理次数
        /// </summary>
        SuccessfulRepairs,
        /// <summary>
        /// 弓箭射击次数
        /// </summary>
        BowShot,
        /// <summary>
        /// 弓箭命中次数
        /// </summary>
        SuccessfulHits_Bow,
        /// <summary>
        /// 步枪射击次数
        /// </summary>
        RifleShot,
        /// <summary>
        /// 步枪命中次数
        /// </summary>
        SuccessfulHits_Rifle,
        /// <summary>
        /// 信号枪射击次数
        /// </summary>
        DistressPistolShot,
        /// <summary>
        /// 信号枪命中次数
        /// </summary>
        SuccessfulHits_DistressPistol,
        /// <summary>
        /// 狼近距离遭遇次数
        /// </summary>
        WolfCloseEncounters,
        /// <summary>
        /// 与狼搏斗次数
        /// </summary>
        WolfStruggles,
        /// <summary>
        /// 狼搏斗胜利次数
        /// </summary>
        WolfStrugglesWon,
        /// <summary>
        /// 击杀狼数
        /// </summary>
        WolvesKilled,
        /// <summary>
        /// 用诱饵分散狼注意力次数
        /// </summary>
        WolvesDistactedByDecoys,
        /// <summary>
        /// 熊遭遇幸存次数
        /// </summary>
        BearEncountersSurvived,
        /// <summary>
        /// 击杀熊数
        /// </summary>
        BearsKilled,
        /// <summary>
        /// 击杀雄鹿数
        /// </summary>
        StagsKilled,
        /// <summary>
        /// 击杀兔子数
        /// </summary>
        RabbitsKilled,
        /// <summary>
        /// 陷阱捕获兔子数
        /// </summary>
        RabbitsSnared,
        /// <summary>
        /// 食用肉量
        /// </summary>
        MeatConsumed,
        /// <summary>
        /// 食用鱼量
        /// </summary>
        FishConsumed,
        /// <summary>
        /// 捕获鱼数
        /// </summary>
        FishCaught,
        /// <summary>
        /// 收获肉量
        /// </summary>
        MeatHarvested,
        /// <summary>
        /// 收获肠子数
        /// </summary>
        GutsHarvested,
        /// <summary>
        /// 收获兽皮数
        /// </summary>
        HidesHarvested,
        /// <summary>
        /// 搜刮物品数
        /// </summary>
        ItemsLooted,
        /// <summary>
        /// 制作物品数
        /// </summary>
        ItemsCrafted,
        /// <summary>
        /// 拆解物品数
        /// </summary>
        ItemsBrokenDown,
        /// <summary>
        /// 采集植物数
        /// </summary>
        PlantsHarvested,
        /// <summary>
        /// 开启罐头数
        /// </summary>
        CansOpened,
        /// <summary>
        /// 找到开罐器数
        /// </summary>
        CanOpenersFound,
        /// <summary>
        /// 生火次数
        /// </summary>
        FiresStarted,
        /// <summary>
        /// 最长燃烧时间
        /// </summary>
        LongestBurningFire,
        /// <summary>
        /// 手腕扭伤次数
        /// </summary>
        Sprains_Wrist,
        /// <summary>
        /// 脚踝扭伤次数
        /// </summary>
        Sprains_Ankle,
        /// <summary>
        /// 食物中毒次数
        /// </summary>
        FoodPoisoning,
        /// <summary>
        /// 痢疾次数
        /// </summary>
        Dysentry,
        /// <summary>
        /// 感染次数
        /// </summary>
        Infection,
        /// <summary>
        /// 低温症次数
        /// </summary>
        Hypothermia,
        /// <summary>
        /// 失血次数
        /// </summary>
        BloodLoss,
        /// <summary>
        /// 跌倒次数
        /// </summary>
        FallCount,
        /// <summary>
        /// 暴风雪次数
        /// </summary>
        Blizzards,
        /// <summary>
        /// 绳索滑落次数
        /// </summary>
        NumRopeSlips,
        /// <summary>
        /// 绳索跌落次数
        /// </summary>
        NumRopeFalls,
        /// <summary>
        /// 绳索旅行距离
        /// </summary>
        DistanceTravelledOnRope,
        /// <summary>
        /// 幽居病次数
        /// </summary>
        CabinFever,
        /// <summary>
        /// 肠道寄生虫次数
        /// </summary>
        IntestinalParasites,
        /// <summary>
        /// 冻伤次数
        /// </summary>
        Frostbite,
        /// <summary>
        /// 沼泽地区小时数
        /// </summary>
        HoursInMarshRegion,
        /// <summary>
        /// 铁轨地区小时数
        /// </summary>
        HoursInTracksRegion,
        /// <summary>
        /// 肋骨骨折次数
        /// </summary>
        BrokenRib,
        /// <summary>
        /// 章节进度
        /// </summary>
        EpisodeProgress,
        /// <summary>
        /// 山镇地区小时数
        /// </summary>
        HoursInMountainTownRegion,
        /// <summary>
        /// 驼鹿遭遇幸存次数
        /// </summary>
        MooseEncountersSurvived,
        /// <summary>
        /// 击杀驼鹿数
        /// </summary>
        MooseKilled,
        /// <summary>
        /// 河谷地区小时数
        /// </summary>
        HoursInRiverValleyRegion,
        /// <summary>
        /// 统计数量(占位符)
        /// </summary>
        NumStats,
    }

    /// <summary>
    /// 自定义管理对象状态
    /// </summary>
    public enum CustomManagedObjectState
    {
        /// <summary>
        /// 初始激活
        /// </summary>
        InitialActive = 1,
        /// <summary>
        /// 管理激活
        /// </summary>
        ManagedActive = 2,
        /// <summary>
        /// 初始未知
        /// </summary>
        InitialUnknown = 4,
    }

    /// <summary>
    /// 游戏区域
    /// </summary>
    public enum GameRegion
    {
        /// <summary>
        /// 湖区
        /// </summary>
        LakeRegion,
        /// <summary>
        /// 沿海地区
        /// </summary>
        CoastalRegion,
        /// <summary>
        /// 捕鲸站地区
        /// </summary>
        WhalingStationRegion,
        /// <summary>
        /// 乡村地区
        /// </summary>
        RuralRegion,
        /// <summary>
        /// 坠机山区
        /// </summary>
        CrashMountainRegion,
        /// <summary>
        /// 沼泽地区
        /// </summary>
        MarshRegion,
        /// <summary>
        /// 随机区域
        /// </summary>
        RandomRegion,
        /// <summary>
        /// 未来区域
        /// </summary>
        FutureRegion,
        /// <summary>
        /// 山镇地区
        /// </summary>
        MountainTownRegion,
        /// <summary>
        /// 铁轨地区
        /// </summary>
        TracksRegion,
        /// <summary>
        /// 河谷地区
        /// </summary>
        RiverValleyRegion,
        /// <summary>
        /// 罐头厂地区
        /// </summary>
        CanneryRegion,
        /// <summary>
        /// 灰烬峡谷地区
        /// </summary>
        AshCanyonRegion
    }

    /// <summary>
    /// 追加销售类型
    /// </summary>
    public enum UpSell
    {
        /// <summary>
        /// 主菜单-挑战
        /// </summary>
        MainMenu_Challenges,
        /// <summary>
        /// 主菜单-日志
        /// </summary>
        MainMenu_Logs,
        /// <summary>
        /// 主菜单-徽章
        /// </summary>
        MainMenu_Badges,
    }

    /// <summary>
    /// 图形模式
    /// </summary>
    public enum GraphicsMode
    {
        /// <summary>
        /// 全屏
        /// </summary>
        Fullscreen,
        /// <summary>
        /// 窗口
        /// </summary>
        Window,
    }

    /// <summary>
    /// 计量单位
    /// </summary>
    public enum MeasurementUnits
    {
        /// <summary>
        /// 公制
        /// </summary>
        Metric,
        /// <summary>
        /// 英制
        /// </summary>
        Imperial,
    }

    /// <summary>
    /// HUD偏好设置
    /// </summary>
    public enum HudPref
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        DebugInfo,
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled,
    }

    /// <summary>
    /// 字幕状态
    /// </summary>
    public enum SubtitlesState
    {
        /// <summary>
        /// 关闭
        /// </summary>
        Off,
        /// <summary>
        /// 开启
        /// </summary>
        On,
        /// <summary>
        /// 隐藏式字幕
        /// </summary>
        ClosedCaptioning,
    }

    /// <summary>
    /// 语言状态
    /// </summary>
    public enum LanguageState
    {
        /// <summary>
        /// 英语
        /// </summary>
        English,
        /// <summary>
        /// 德语
        /// </summary>
        German,
        /// <summary>
        /// 俄语
        /// </summary>
        Russian,
    }

    /// <summary>
    /// 专长类型
    /// </summary>
    public enum FeatType
    {
        /// <summary>
        /// 书本智慧
        /// </summary>
        BookSmarts,
        /// <summary>
        /// 冷聚变
        /// </summary>
        ColdFusion,
        /// <summary>
        /// 高效机器
        /// </summary>
        EfficientMachine,
        /// <summary>
        /// 火焰大师
        /// </summary>
        FireMaster,
        /// <summary>
        /// 自由奔跑者
        /// </summary>
        FreeRunner,
        /// <summary>
        /// 雪地行者
        /// </summary>
        SnowWalker,
    }

    /// <summary>
    /// 强制移动类型
    /// </summary>
    public enum ForcedMovement
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 强制蹲下
        /// </summary>
        ForceCrouch,
        /// <summary>
        /// 强制行走
        /// </summary>
        ForceWalk,
        /// <summary>
        /// 强制跛行
        /// </summary>
        ForceLimp,
        /// <summary>
        /// 强制缓慢跛行
        /// </summary>
        ForceLimpSlow,
    }

    /// <summary>
    /// 知识类别
    /// </summary>
    public enum KnowledgeCateogry
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 人物
        /// </summary>
        People,
        /// <summary>
        /// 地点
        /// </summary>
        Places,
        /// <summary>
        /// 物品
        /// </summary>
        Things,
        /// <summary>
        /// 行动
        /// </summary>
        Actions,
    }

    /// <summary>
    /// 任务目标计数类型
    /// </summary>
    public enum MissionObjectiveCountType
    {
        /// <summary>
        /// 无单位
        /// </summary>
        NoUnits,
        /// <summary>
        /// 重量
        /// </summary>
        Weight,
        /// <summary>
        /// 体积
        /// </summary>
        Volume,
    }

    /// <summary>
    /// 存档槽类型
    /// </summary>
    public enum SaveSlotType
    {
        /// <summary>
        /// 未知
        /// </summary>
        UNKNOWN,
        /// <summary>
        /// 挑战
        /// </summary>
        CHALLENGE,
        /// <summary>
        /// 检查点
        /// </summary>
        CHECKPOINT,
        /// <summary>
        /// 沙盒
        /// </summary>
        SANDBOX,
        /// <summary>
        /// 故事
        /// </summary>
        STORY,
        /// <summary>
        /// 自动存档
        /// </summary>
        AUTOSAVE,
    }

    /// <summary>
    /// 章节
    /// </summary>
    public enum Episode
    {
        /// <summary>
        /// 第一章
        /// </summary>
        One,
        /// <summary>
        /// 第二章
        /// </summary>
        Two,
        /// <summary>
        /// 第三章
        /// </summary>
        Three,
        /// <summary>
        /// 第四章
        /// </summary>
        Four,
        /// <summary>
        /// 第五章
        /// </summary>
        Five,
    }

    /// <summary>
    /// 成就
    /// </summary>
    public enum Achievement
    {
        /// <summary>
        /// 生存10天
        /// </summary>
        Survival_10_Days = 1,
        /// <summary>
        /// 生存50天
        /// </summary>
        Survival_50_Days = 6,
        /// <summary>
        /// 探索湖岸与沿海公路所有室内地点
        /// </summary>
        LakeCoastalInteriors = 7,
        /// <summary>
        /// 收获10只鹿
        /// </summary>
        Harvest_10_Deer = 8,
        /// <summary>
        /// 生存1晚
        /// </summary>
        Survival_1_Night = 9,
        /// <summary>
        /// 生存3晚
        /// </summary>
        Survival_3_Nights = 10,
        /// <summary>
        /// 不使用枪械生存50天
        /// </summary>
        NoGun_50_Days = 11,
        /// <summary>
        /// 不杀死动物生存25天
        /// </summary>
        NoKill_25_Days = 12,
        /// <summary>
        /// 穿着全套兽皮服装
        /// </summary>
        Wrapped_Fur = 13,
        /// <summary>
        /// 捕获一条5公斤以上的鱼
        /// </summary>
        Big_Fish = 14,
        /// <summary>
        /// 连续25天食用自己捕获的猎物
        /// </summary>
        Living_Off_Land = 15,
        /// <summary>
        /// 不使用人造药物治疗所有疾病
        /// </summary>
        Natural_Healer = 16,
        /// <summary>
        /// 收获100个植物资源
        /// </summary>
        Happy_Harvester = 17,
        /// <summary>
        /// 生存1天
        /// </summary>
        Survival_1_Days = 18,
        /// <summary>
        /// 生存100天
        /// </summary>
        Survival_100_Days = 20,
        /// <summary>
        /// 生存500天
        /// </summary>
        Survival_500_Days = 22,
        /// <summary>
        /// 用弓箭击杀50只动物
        /// </summary>
        StoneAgeSniper = 23,
        /// <summary>
        /// 所有技能达到5级
        /// </summary>
        SkilledSurvivor = 24,
        /// <summary>
        /// 食用所有类型的食物
        /// </summary>
        TasteTheImpossible = 25,
        /// <summary>
        /// 保持饱食状态25天
        /// </summary>
        WellNourished = 26,
        /// <summary>
        /// 绘制所有地图区域
        /// </summary>
        FaithfulCartographer = 27,
        /// <summary>
        /// 制作所有服装物品
        /// </summary>
        ResoluteOutfitter = 28,
        /// <summary>
        /// 阅读所有技能书
        /// </summary>
        PenitentScholar = 29,
        /// <summary>
        /// 探索驼鹿山地区
        /// </summary>
        TimberwolfMountain = 30,
        /// <summary>
        /// 探索荒凉据点地区
        /// </summary>
        DesolationPoint = 31,
        /// <summary>
        /// 探索深林地区
        /// </summary>
        DeepForest = 32,
        /// <summary>
        /// 第一章:你的旅程开始
        /// </summary>
        EP1_YourJourneyBegins = 33,
        /// <summary>
        /// 第一章:失乐园
        /// </summary>
        EP1_ParadiseLost = 34,
        /// <summary>
        /// 第一章:漫长寒冬
        /// </summary>
        EP1_TheLongWinter = 35,
        /// <summary>
        /// 第一章:失去一个孩子就像...
        /// </summary>
        EP1_LosingAChildIsLike = 36,
        /// <summary>
        /// 第一章:告别旧世界
        /// </summary>
        EP1_LeavingTheOldWorldBehind = 37,
        /// <summary>
        /// 第二章:老猎人
        /// </summary>
        EP2_TheOldTrapper = 38,
        /// <summary>
        /// 第二章:天空之光
        /// </summary>
        EP2_LightsInTheSky = 39,
        /// <summary>
        /// 第二章:毕业日
        /// </summary>
        EP2_GraduationDay = 40,
        /// <summary>
        /// 第二章:仇恨与饥饿的货运列车
        /// </summary>
        EP2_FreightTrainOfHateAndHunger = 41,
        /// <summary>
        /// 第二章:你很快就会和她在一起
        /// </summary>
        EP2_YouWillBeWithHerSoon = 42,
        /// <summary>
        /// 解锁米尔顿所有保险箱
        /// </summary>
        SM_UnlockAllMiltonDepositBoxes = 43,
        /// <summary>
        /// 找到所有森林行者藏匿处
        /// </summary>
        SM_FoundAllForestTalkerCaches = 44,
        /// <summary>
        /// 找到所有隐藏藏匿处
        /// </summary>
        SM_FoundAllHiddenCaches = 45,
        /// <summary>
        /// 完成所有挑战
        /// </summary>
        ChallengeMastery = 46,
    }

    /// <summary>
    /// 伤害来源方向
    /// </summary>
    public enum DamageSide
    {
        /// <summary>
        /// 无特定方向
        /// </summary>
        DamageSideNone = -1,
        /// <summary>
        /// 左侧伤害
        /// </summary>
        DamageSideLeft = 0,
        /// <summary>
        /// 右侧伤害
        /// </summary>
        DamageSideRight = 1,
    }

    /// <summary>
    /// HUD界面尺寸
    /// </summary>
    public enum HudSize
    {
        /// <summary>
        /// 小尺寸
        /// </summary>
        Small,
        /// <summary>
        /// 常规尺寸
        /// </summary>
        Regular,
        /// <summary>
        /// 大尺寸
        /// </summary>
        Large,
    }

    /// <summary>
    /// HUD显示类型
    /// </summary>
    public enum HudType
    {
        /// <summary>
        /// 完全关闭
        /// </summary>
        Off,
        /// <summary>
        /// 上下文相关显示
        /// </summary>
        Contextual,
        /// <summary>
        /// 持续显示
        /// </summary>
        AlwaysOn,
    }

    /// <summary>
    /// 身体受影响区域
    /// </summary>
    public enum AfflictionBodyArea
    {
        /// <summary>
        /// 头部
        /// </summary>
        Head,
        /// <summary>
        /// 颈部
        /// </summary>
        Neck,
        /// <summary>
        /// 左臂
        /// </summary>
        ArmLeft,
        /// <summary>
        /// 左手
        /// </summary>
        HandLeft,
        /// <summary>
        /// 右臂
        /// </summary>
        ArmRight,
        /// <summary>
        /// 右手
        /// </summary>
        HandRight,
        /// <summary>
        /// 胸部
        /// </summary>
        Chest,
        /// <summary>
        /// 腹部
        /// </summary>
        Stomach,
        /// <summary>
        /// 左腿
        /// </summary>
        LegLeft,
        /// <summary>
        /// 左脚
        /// </summary>
        FootLeft,
        /// <summary>
        /// 右腿
        /// </summary>
        LegRight,
        /// <summary>
        /// 右脚
        /// </summary>
        FootRight,
    }
}
