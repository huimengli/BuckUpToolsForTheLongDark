using System.Collections.ObjectModel;
using System.Windows.Input;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Helpers;

namespace TheLongDarkBuckupTools.MoreData
{

    /// <summary>
    /// 表示一个负面状态的类，用于管理游戏中角色可能受到的各种负面效果
    /// </summary>
    public class Affliction
    {

        /// <summary>
        /// 获取或设置负面状态的类型
        /// </summary>
        public AfflictionType AfflictionType { get; set; }
        /// <summary>
        /// 获取或设置负面状态的部位位置（如果适用）
        /// </summary>
        public int Location { get; set; }
        /// <summary>
        /// 获取或设置用于移除此负面状态的命令
        /// </summary>
        public ICommand RemoveCommand { get; set; }

        /// <summary>
        /// 构造函数，初始化一个新的负面状态实例
        /// </summary>
        /// <param name="collection">包含此负面状态的集合，用于后续的移除操作</param>
        public Affliction(ObservableCollection<Affliction> collection)
        {
            // 初始化移除命令，当执行时会从此集合中移除当前负面状态
            RemoveCommand = new CommandHandler(() =>
            {
                collection.Remove(this);
            });
        }
    }

    /// <summary>
    /// Hypothermia类，表示一种疾病状态（失温症）
    /// 继承自Affliction基类
    /// </summary>
    public class Hypothermia : Affliction
    {
        /// <summary>
        /// 构造函数，初始化失温症实例
        /// </summary>
        /// <param name="collection">疾病状态集合</param>
        public Hypothermia(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 静态字段，表示导致失温症所需暴露在寒冷环境中的小时数
        /// </summary>
        private static int hoursSpentFreezingRequired = 10;
        /// <summary>
        /// 属性：已暴露在寒冷环境中的小时数
        /// </summary>
        public float ElapsedHours { get; set; }
        /// <summary>
        /// 属性：已处于温暖环境中的小时数
        /// </summary>
        public float ElapsedWarmHours { get; set; }
        /// <summary>
        /// 属性：失温症的患病风险几率
        /// 计算方式：已暴露小时数 / 所需暴露小时数
        /// </summary>
        public float RiskChance
        {
            get { return ElapsedHours / hoursSpentFreezingRequired; }
        }
        /// <summary>
        /// 属性：失温症的病因
        /// </summary>
        public string Cause { get; set; }
    }

    /// <summary>
    /// Frostbite类，表示一种负面状态效果（冻伤），继承自Affliction基类
    /// </summary>
    public class Frostbite : Affliction
    {
        /// <summary>
        /// 构造函数，初始化Frostbite实例
        /// </summary>
        /// <param name="collection">用于存储负面状态效果的ObservableCollection集合</param>
        public Frostbite(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 静态数组，表示身体各部位的HP值
        /// 每个元素代表一个身体部位的HP值，初始值均为100
        /// </summary>
        private static int[] bodyAreaHP = new int[] { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };

        /// <summary>
        /// 伤害百分比属性
        /// 根据当前伤害值和身体部位HP值计算伤害百分比
        /// 设置值时，会根据身体部位HP值计算实际伤害值
        /// </summary>
        public float DamagePercentage
        {
            get { return Damage / bodyAreaHP[Location]; }  // 获取伤害百分比：当前伤害值/身体部位HP值
            set { Damage = bodyAreaHP[Location] * value; } // 设置伤害值：身体部位HP值 * 伤害百分比
        }
        /// <summary>
        /// 伤害值属性
        /// 表示当前造成的伤害值
        /// </summary>
        public float Damage { get; set; }
    }

    /// <summary>
    /// 食物中毒类，继承自疾病(Affliction)基类
    /// 表示角色在游戏中可能遭遇的食物中毒状态
    /// </summary>
    public class FoodPoisoning : Affliction
    {
        /// <summary>
        /// 构造函数，初始化食物中毒状态
        /// </summary>
        /// <param name="collection">疾病集合，用于添加当前疾病实例</param>
        public FoodPoisoning(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 已过去的小时数，表示食物中毒持续的时间
        /// </summary>
        public float ElapsedHours { get; set; }
        /// <summary>
        /// 总持续时间（小时），表示食物中毒预计持续的总时间
        /// </summary>
        public float DurationHours { get; set; }
        /// <summary>
        /// 是否已服用抗生素，用于判断治疗状态
        /// </summary>
        public bool AntibioticsTaken { get; set; }
        /// <summary>
        /// 已休息的时间，用于表示角色恢复状态
        /// </summary>
        public float ElapsedRest { get; set; }
        /// <summary>
        /// 中毒原因，记录导致食物中毒的具体原因
        /// </summary>
        public string Cause { get; set; }
    }

    /// <summary>
    /// 痢疾类，继承自Affliction基类，表示游戏中的一种疾病状态
    /// </summary>
    public class Dysentery : Affliction
    {
        // 构造函数，接收一个Affliction类型的ObservableCollection集合作为参数
        public Dysentery(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 疾病已持续的小时数
        /// </summary>
        public float ElapsedHours { get; set; }
        /// <summary>
        /// 疾病的总持续时间（小时）
        /// </summary>
        public float DurationHours { get; set; }
        /// <summary>
        /// 是否已服用抗生素
        /// </summary>
        public bool AntibioticsTaken { get; set; }
        /// <summary>
        /// 已休息的时间
        /// </summary>
        public float ElapsedRest { get; set; }
        /// <summary>
        /// 已消耗的清洁水量
        /// </summary>
        public float CleanWaterConsumed { get; set; }
    }

    /// <summary>
    /// 扭伤类，继承自Affliction基类，用于表示角色在游戏中可能遭遇的扭伤状态
    /// </summary>
    public class SprainAffliction : Affliction
    {
        /// <summary>
        /// 构造函数，初始化扭伤状态
        /// </summary>
        /// <param name="collection">扭伤状态集合，用于管理多个扭伤状态</param>
        public SprainAffliction(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 扭伤原因的位置标识符ID
        /// </summary>
        public string CauseLocID { get; set; }
        /// <summary>
        /// 扭伤的总持续时间（单位：小时）
        /// </summary>
        public float Duration { get; set; }
        /// <summary>
        /// 扭伤已经过去的时间（单位：小时）
        /// </summary>
        public float ElapsedHours { get; set; }
        /// <summary>
        /// 扭伤过程中已经休息的时间（单位：小时）
        /// </summary>
        public float ElapsedRest { get; set; }
    }

    /// <summary>
    /// 烧伤类，继承自Affliction（疾病/状况）基类
    /// 表示角色在游戏中受到的烧伤状况
    /// </summary>
    public class Burns : Affliction
    {
        /// <summary>
        /// 构造函数，初始化烧伤状况
        /// </summary>
        /// <param name="collection">疾病/状况集合的引用</param>
        public Burns(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 烧伤已持续的小时数
        /// </summary>
        public float ElapsedHours { get; set; }
        /// <summary>
        /// 烧伤持续的总小时数
        /// </summary>
        public float DurationHours { get; set; }
        /// <summary>
        /// 是否已服用止痛药
        /// </summary>
        public bool PainKillersTaken { get; set; }
        /// <summary>
        /// 是否已包扎伤口
        /// </summary>
        public bool BandageApplied { get; set; }
        /// <summary>
        /// 烧伤原因的位置标识符
        /// </summary>
        public string CauseLocID { get; set; }
    }

    /// <summary>
    /// BurnsElectric 类继承自 Affliction 类，表示电击灼伤状态
    /// </summary>
    public class BurnsElectric : Affliction
    {
        /// <summary>
        /// 构造函数，初始化电击灼伤状态
        /// </summary>
        /// <param name="collection">灼伤状态集合</param>
        public BurnsElectric(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 电击灼伤已持续时间（小时）
        /// </summary>
        public float ElapsedHours { get; set; }
        /// <summary>
        /// 电击灼伤总持续时间（小时）
        /// </summary>
        public float DurationHours { get; set; }
        /// <summary>
        /// 是否已服用止痛药
        /// </summary>
        public bool PainKillersTaken { get; set; }
        /// <summary>
        /// 是否已包扎处理
        /// </summary>
        public bool BandageApplied { get; set; }
    }

    /// <summary>
    /// 血量损失类，继承自Affliction（疾病/状态）基类
    /// 表示角色在游戏中受到的血量损失效果
    /// </summary>
    public class BloodLoss : Affliction
    {
        /// <summary>
        /// 构造函数，初始化血量损失状态
        /// </summary>
        /// <param name="collection">疾病/状态集合，用于存储和管理所有状态效果</param>
        public BloodLoss(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 获取或设置造成血量损失的部位标识ID
        /// 用于标识是身体哪个部位受伤导致的血量损失
        /// </summary>
        public string CauseLocID { get; set; }
        /// <summary>
        /// 获取或设置血量损失已经持续的时间（小时）
        /// 表示当前血量损失效果已经持续了多长时间
        /// </summary>
        public float ElapsedHours { get; set; }
        /// <summary>
        /// 获取或设置血量损失的总持续时间（小时）
        /// 表示该血量损失效果总共会持续多长时间
        /// </summary>
        public float DurationHours { get; set; }
    }


    /// <summary>
    /// 感染类，继承自疾病类(Affliction)
    /// 表示角色在游戏中可能受到的感染状态
    /// </summary>
    public class Infection : Affliction
    {
        /// <summary>
        /// 构造函数，初始化感染状态
        /// </summary>
        /// <param name="collection">疾病集合，用于存储所有疾病状态</param>
        public Infection(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 感染原因的位置ID
        /// </summary>
        public string CauseLocID { get; set; }
        /// <summary>
        /// 感染已经持续的小时数
        /// </summary>
        public float ElapsedHours { get; set; }
        /// <summary>
        /// 感染的总持续时间（小时）
        /// </summary>
        public float DurationHours { get; set; }
        /// <summary>
        /// 是否已服用抗生素
        /// </summary>
        public bool AntibioticsTaken { get; set; }
        /// <summary>
        /// 感染期间已经休息的时间
        /// </summary>
        public float ElapsedRest { get; set; }
    }

    /// <summary>
    /// 感染风险类，继承自疾病(Affliction)类，用于管理角色可能面临的感染风险状态
    /// </summary>
    public class InfectionRisk : Affliction
    {
        /// <summary>
        /// 构造函数，初始化感染风险实例
        /// </summary>
        /// <param name="collection">疾病集合的ObservableCollection，用于跟踪所有疾病状态</param>
        public InfectionRisk(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 获取或设置感染原因的位置ID
        /// </summary>
        public string CauseLocID { get; set; }
        /// <summary>
        /// 获取或设置感染已经持续的小时数
        /// </summary>
        public float ElapsedHours { get; set; }
        /// <summary>
        /// 获取或设置感染持续的总小时数
        /// </summary>
        public float DurationHours { get; set; }
        /// <summary>
        /// 获取或设置是否已经使用消毒剂
        /// </summary>
        public bool AntisepticTaken { get; set; }
        /// <summary>
        /// 获取或设置当前的感染概率
        /// </summary>
        public float CurrentInfectionChance { get; set; }
        /// <summary>
        /// 获取或设置感染风险是否为持续性的
        /// </summary>
        public bool Constant { get; set; }
    }

    /// <summary>
    /// 表示一种名为"幽闭恐惧症"的疾病状态类，继承自Affliction基类
    /// </summary>
    public class CabinFever : Affliction
    {
        /// <summary>
        /// 构造函数，用于初始化幽闭恐惧症实例
        /// </summary>
        /// <param name="collection">可观察的疾病集合，用于存储疾病状态</param>
        public CabinFever(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 获取或设置已过去的小时数，用于跟踪疾病持续时间
        /// </summary>
        public float ElapsedHours { get; set; }
    }

    /// <summary>
    /// 肠道寄生虫类，继承自疾病(Affliction)类
    /// 用于模拟和管理角色感染肠道寄生虫的状况
    /// </summary>
    public class IntestinalParasites : Affliction
    {
        // 构造函数，初始化肠道寄生虫疾病
        // 参数：疾病集合，用于跟踪所有当前疾病
        public IntestinalParasites(ObservableCollection<Affliction> collection) : base(collection) { }

        // 当前感染几率，表示角色被感染的可能性
        public float CurrentInfectionChance { get; set; }
        // 寄生虫已持续存在的小时数，用于计算感染时间
        public float ParasitesElapsedHours { get; set; }
        // 风险已持续的小时数，用于计算风险周期
        public float RiskElapsedHours { get; set; }
        // 风险持续的总小时数，表示一个风险周期的长度
        public float RiskDurationHours { get; set; }
        // 已服用的药物剂量数量，用于跟踪治疗进度
        public int DosesTaken { get; set; }
        // 今日是否已服用药物，用于控制每日服药次数
        public bool HasTakenDoseToday { get; set; }
        // 允许下次服药的日期，用于控制服药间隔
        public int DayToAllowNextDose { get; set; }
        // 当前风险周期内已食用的食物数量，可能影响感染风险
        public int PiecesEatenThisRiskCycle { get; set; }

    }

    /// <summary>
    /// 折断的肋骨类，继承自Affliction基类
    /// 表示游戏中角色可能遭遇的一种身体状况
    /// </summary>
    public class BrokenRib : Affliction
    {
        public BrokenRib(ObservableCollection<Affliction> collection) : base(collection) { } // 构造函数，通过基类构造函数初始化

        /// <summary>
        /// 导致肋骨受伤的位置ID
        /// </summary>
        public string CauseLocID { get; set; }
        /// <summary>
        /// 已服用的止痛药数量
        /// </summary>
        public int PainKillersTaken { get; set; }
        /// <summary>
        /// 已应用的绷带数量
        /// </summary>
        public int BandagesApplied { get; set; }
        /// <summary>
        /// 已经过的休息时间（单位：小时）
        /// </summary>
        public float ElapsedRest { get; set; }
        /// <summary>
        /// 治愈所需的休息总时间（单位：小时）
        /// </summary>
        public float NumHoursRestForCure { get; set; }
    }

    /// <summary>
    /// 表示角色处于饱食状态的类，继承自Affliction基类
    /// </summary>
    public class WellFed : Affliction
    {
        /// <summary>
        /// 构造函数，初始化WellFed状态
        /// </summary>
        /// <param name="collection"> affliction集合，用于管理各种状态效果 </param>
        public WellFed(ObservableCollection<Affliction> collection) : base(collection) { }

        /// <summary>
        /// 属性：记录角色不处于饥饿状态已经持续的时间（小时）
        /// </summary>
        public float ElapsedHoursNotStarving { get; set; }
    }
}
