using System;
using System.Collections;
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
    public class GlobalDataFormat:IEnumerable<string>
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
            throw new NotImplementedException();
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

    //public class GlobalSaveGameData
    //{
    //    public int Version { get; set; }
    //    public SceneTransitionData SceneTransistion { get; set; }
    //    public HudManagerSaveDataProxy HudManager { get; set; }
    //    public TimeOfDaySaveDataProxy TimeOfDay { get; set; }
    //    public WindSaveData Wind { get; set; }
    //    public WeatherSaveDataProxy Weather { get; set; }
    //    public WeatherTransitionSaveDataProxy WeatherTransistion { get; set; }
    //    public ConditionSaveDataProxy Condition { get; set; }
    //    public EncumberSaveDataProxy Encumber { get; set; }
    //    public HungerSaveDataProxy Hunger { get; set; }
    //    public ThirstSaveDataProxy Thirst { get; set; }
    //    public FatigueSaveDataProxy Fatigue { get; set; }
    //    public FreezingSaveDataProxy Freezing { get; set; }
    //    public WillpowerSaveDataProxy Willpower { get; set; }
    //    public Inventory Inventory { get; set; }
    //    public MissionServicesManagerSaveProxy SandboxManager { get; set; }
    //    public MissionServicesManagerSaveProxy StoryManager { get; set; }
    //    public PlayerManagerSaveDataProxy PlayerManager { get; set; }
    //    public PlayerClimbRopeProxy PlayerClimbRope { get; set; }
    //    public PlayerSkillsSaveData PlayerSkills { get; set; }
    //    public PlayerGameStatsProxy PlayerGameStats { get; set; }
    //    public Afflictions Afflictions { get; set; }
    //    public LogSaveDataProxy Log { get; set; }
    //    public RestSaveDataProxy Rest { get; set; }
    //    public FlyoverDataProxy FlyOver { get; set; }
    //    public AchievementSaveData AchievementManager { get; set; }
    //    public ExperienceModeManagerSaveDataProxy ExperienceModeManager { get; set; }
    //    public string AuroraManager { get; set; }
    //    public PlayerMovementSaveDataProxy PlayerMovement { get; set; }
    //    public string PlayerStruggle { get; set; }
    //    public string PanelStats { get; set; } // StatContainer
    //    public EmergencyStimParams EmergencyStim { get; set; }
    //    public MusicEventSaveData MusicEventManager { get; set; }
    //    public SnowfallManagerSaveDataProxy SnowPatchManager { get; set; }
    //    public SerializedParams PlayerAnimation { get; set; }
    //    public SkillsManager SkillsManager { get; set; }
    //    public ObservableCollection<string> UnlockedCompanions { get; set; }
    //    public FeatEnabledTrackerSaveData EnabledFeats { get; set; }
    //    public TrustManagerSaveData TrustManager { get; set; }
    //    public MapDetailSaveData MapDetailManager { get; set; }
    //    public WorldMapSaveData WorldMapData { get; set; }
    //    public MapSaveData MapData { get; set; }
    //    public BearHuntSaveData BearHunt { get; set; }
    //    public KnowledgeManagerSaveData KnowledgeManager { get; set; }
    //    public List<string> UnlockedBlueprints { get; set; }
    //    public string CollectionManagerSerialized { get; set; }
    //    public string StoryMissionDataSerialized { get; set; }
    //    public bool CurrentEpisodeCompleted { get; set; }

    //    //public GlobalSaveGameData(string data)
    //    //{
    //    //    System.Diagnostics.Debug.WriteLine(data);
    //    //    var proxy = JsonConvert.DeserializeObject<GlobalSaveGameFormat>(data);

    //    //    Version = proxy.m_Version;
    //    //    SceneTransistion = Item.DeserializeObject<SceneTransitionData>(proxy.m_GameManagerSerialized);
    //    //    HudManager = Item.DeserializeObject<HudManagerSaveDataProxy>(proxy.m_HudManagerSerialized);
    //    //    TimeOfDay = Item.DeserializeObject<TimeOfDaySaveDataProxy>(proxy.m_TimeOfDay_Serialized);
    //    //    Wind = new WindSaveData(proxy.m_Wind_Serialized);
    //    //    Weather = Item.DeserializeObject<WeatherSaveDataProxy>(proxy.m_Weather_Serialized);
    //    //    WeatherTransistion = Item.DeserializeObject<WeatherTransitionSaveDataProxy>(proxy.m_WeatherTransition_Serialized);
    //    //    Condition = Item.DeserializeObject<ConditionSaveDataProxy>(proxy.m_Condition_Serialized);
    //    //    Encumber = Item.DeserializeObject<EncumberSaveDataProxy>(proxy.m_Encumber_Serialized);
    //    //    Hunger = Item.DeserializeObject<HungerSaveDataProxy>(proxy.m_Hunger_Serialized);
    //    //    Thirst = Item.DeserializeObject<ThirstSaveDataProxy>(proxy.m_Thirst_Serialized);
    //    //    Fatigue = Item.DeserializeObject<FatigueSaveDataProxy>(proxy.m_Fatigue_Serialized);
    //    //    Freezing = Item.DeserializeObject<FreezingSaveDataProxy>(proxy.m_Freezing_Serialized);
    //    //    Willpower = Item.DeserializeObject<WillpowerSaveDataProxy>(proxy.m_Willpower_Serialized);
    //    //    Inventory = new Inventory(proxy.m_Inventory_Serialized);
    //    //    SandboxManager = Item.DeserializeObject<MissionServicesManagerSaveProxy>(proxy.m_SandboxManagerSerialized);
    //    //    StoryManager = Item.DeserializeObject<MissionServicesManagerSaveProxy>(proxy.m_StoryManagerSerialized);
    //    //    PlayerManager = Item.DeserializeObject<PlayerManagerSaveDataProxy>(proxy.m_PlayerManagerSerialized);
    //    //    PlayerClimbRope = Item.DeserializeObject<PlayerClimbRopeProxy>(proxy.m_PlayerClimbRopeSerialized);
    //    //    PlayerSkills = Item.DeserializeObject<PlayerSkillsSaveData>(proxy.m_PlayerSkillsSerialized);
    //    //    PlayerGameStats = Item.DeserializeObject<PlayerGameStatsProxy>(proxy.m_PlayerGameStatsSerialized);
    //    //    Afflictions = new Afflictions(proxy);
    //    //    Log = Item.DeserializeObject<LogSaveDataProxy>(proxy.m_LogSerialized);
    //    //    Rest = Item.DeserializeObject<RestSaveDataProxy>(proxy.m_RestSerialized);
    //    //    FlyOver = Item.DeserializeObject<FlyoverDataProxy>(proxy.m_FlyOverSerialized);
    //    //    AchievementManager = Item.DeserializeObject<AchievementSaveData>(proxy.m_AchievementManagerSerialized);
    //    //    ExperienceModeManager = Item.DeserializeObject<ExperienceModeManagerSaveDataProxy>(proxy.m_ExperienceModeManagerSerialized);
    //    //    AuroraManager = proxy.m_AuroraManagerSerialized;
    //    //    PlayerMovement = Item.DeserializeObject<PlayerMovementSaveDataProxy>(proxy.m_PlayerMovementSerialized);
    //    //    PlayerStruggle = proxy.m_PlayerStruggleSerialized;

    //    //    // Do not deserialize, invalid JSON (integers as keys)
    //    //    PanelStats = proxy.m_PanelStatsSerialized;

    //    //    EmergencyStim = Item.DeserializeObject<EmergencyStimParams>(proxy.m_EmergencyStimSerialized);
    //    //    MusicEventManager = Item.DeserializeObject<MusicEventSaveData>(proxy.m_MusicEventManagerSerialized);
    //    //    SnowPatchManager = Item.DeserializeObject<SnowfallManagerSaveDataProxy>(proxy.m_SnowPatchManagerSerialized);
    //    //    PlayerAnimation = Item.DeserializeObject<SerializedParams>(proxy.m_PlayerAnimationSerialized);
    //    //    SkillsManager = new SkillsManager(proxy.m_SkillsManagerSerialized);
    //    //    UnlockedCompanions = Item.DeserializeObject<ObservableCollection<string>>(proxy.m_LockCompanionsSerialized);
    //    //    EnabledFeats = Item.DeserializeObject<FeatEnabledTrackerSaveData>(proxy.m_FeatsEnabledSerialized);
    //    //    TrustManager = Item.DeserializeObject<TrustManagerSaveData>(proxy.m_TrustManagerSerialized);
    //    //    MapDetailManager = Item.DeserializeObject<MapDetailSaveData>(proxy.m_MapDetailManagerSerialized);
    //    //    WorldMapData = Item.DeserializeObject<WorldMapSaveData>(proxy.m_WorldMapDataSerialized);
    //    //    MapData = Item.DeserializeObject<MapSaveData>(proxy.m_MapDataSerialized);
    //    //    BearHunt = Item.DeserializeObject<BearHuntSaveData>(proxy.m_BearHuntSerialized);
    //    //    KnowledgeManager = Item.DeserializeObject<KnowledgeManagerSaveData>(proxy.m_KnowledgeManagerSerialized);
    //    //    UnlockedBlueprints = Item.DeserializeObject<List<string>>(proxy.m_UnlockedBlueprintsSerialized);
    //    //    CollectionManagerSerialized = proxy.m_CollectionManagerSerialized;
    //    //    StoryMissionDataSerialized = proxy.m_StoryMissionDataSerialized;
    //    //    CurrentEpisodeCompleted = proxy.m_CurrentEpisodeComplete;
    //    //}

    //    //public string Serialize()
    //    //{
    //    //    var proxy = new GlobalSaveGameFormat();
    //    //    proxy.m_Version = Version;
    //    //    proxy.m_GameManagerSerialized = Item.SerializeObject(SceneTransistion);
    //    //    proxy.m_HudManagerSerialized = Item.SerializeObject(HudManager);
    //    //    proxy.m_TimeOfDay_Serialized = Item.SerializeObject(TimeOfDay);
    //    //    proxy.m_Wind_Serialized = Wind.Serialize();
    //    //    proxy.m_Weather_Serialized = Item.SerializeObject(Weather);
    //    //    proxy.m_WeatherTransition_Serialized = Item.SerializeObject(WeatherTransistion);
    //    //    proxy.m_Condition_Serialized = Item.SerializeObject(Condition);
    //    //    proxy.m_Encumber_Serialized = Item.SerializeObject(Encumber);
    //    //    proxy.m_Hunger_Serialized = Item.SerializeObject(Hunger);
    //    //    proxy.m_Thirst_Serialized = Item.SerializeObject(Thirst);
    //    //    proxy.m_Fatigue_Serialized = Item.SerializeObject(Fatigue);
    //    //    proxy.m_Freezing_Serialized = Item.SerializeObject(Freezing);
    //    //    proxy.m_Willpower_Serialized = Item.SerializeObject(Willpower);
    //    //    proxy.m_Inventory_Serialized = Inventory.Serialize();
    //    //    proxy.m_SandboxManagerSerialized = Item.SerializeObject(SandboxManager);
    //    //    proxy.m_StoryManagerSerialized = Item.SerializeObject(StoryManager);
    //    //    proxy.m_PlayerManagerSerialized = Item.SerializeObject(PlayerManager);
    //    //    proxy.m_PlayerClimbRopeSerialized = Item.SerializeObject(PlayerClimbRope);
    //    //    proxy.m_PlayerSkillsSerialized = Item.SerializeObject(PlayerSkills);
    //    //    proxy.m_PlayerGameStatsSerialized = Item.SerializeObject(PlayerGameStats);
    //    //    Afflictions.SerializeTo(proxy);
    //    //    proxy.m_LogSerialized = Item.SerializeObject(Log);
    //    //    proxy.m_RestSerialized = Item.SerializeObject(Rest);
    //    //    proxy.m_FlyOverSerialized = Item.SerializeObject(FlyOver);
    //    //    proxy.m_AchievementManagerSerialized = Item.SerializeObject(AchievementManager);
    //    //    proxy.m_ExperienceModeManagerSerialized = Item.SerializeObject(ExperienceModeManager);
    //    //    proxy.m_AuroraManagerSerialized = AuroraManager;
    //    //    proxy.m_PlayerMovementSerialized = Item.SerializeObject(PlayerMovement);
    //    //    proxy.m_PlayerStruggleSerialized = PlayerStruggle;

    //    //    proxy.m_PanelStatsSerialized = PanelStats;

    //    //    proxy.m_EmergencyStimSerialized = Item.SerializeObject(EmergencyStim);
    //    //    proxy.m_MusicEventManagerSerialized = Item.SerializeObject(MusicEventManager);
    //    //    proxy.m_SnowPatchManagerSerialized = Item.SerializeObject(SnowPatchManager);
    //    //    proxy.m_PlayerAnimationSerialized = Item.SerializeObject(PlayerAnimation);
    //    //    proxy.m_SkillsManagerSerialized = SkillsManager.Serialize();
    //    //    proxy.m_LockCompanionsSerialized = Item.SerializeObject(UnlockedCompanions);
    //    //    proxy.m_FeatsEnabledSerialized = Item.SerializeObject(EnabledFeats);
    //    //    proxy.m_TrustManagerSerialized = Item.SerializeObject(TrustManager);
    //    //    proxy.m_MapDetailManagerSerialized = Item.SerializeObject(MapDetailManager);
    //    //    proxy.m_WorldMapDataSerialized = Item.SerializeObject(WorldMapData);
    //    //    proxy.m_MapDataSerialized = Item.SerializeObject(MapData);
    //    //    proxy.m_BearHuntSerialized = Item.SerializeObject(BearHunt);
    //    //    proxy.m_KnowledgeManagerSerialized = Item.SerializeObject(KnowledgeManager);
    //    //    proxy.m_UnlockedBlueprintsSerialized = Item.SerializeObject(UnlockedBlueprints);
    //    //    proxy.m_CollectionManagerSerialized = CollectionManagerSerialized;
    //    //    proxy.m_StoryMissionDataSerialized = StoryMissionDataSerialized;
    //    //    proxy.m_CurrentEpisodeComplete = CurrentEpisodeCompleted;

    //    //    return Item.SerializeObject(proxy);
    //    //}
    //}
}
