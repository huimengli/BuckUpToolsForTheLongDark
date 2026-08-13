using System;
using System.Globalization;
using System.IO;
using System.Linq;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Helpers;
using TheLongDarkBuckupTools.Serialization;

namespace TheLongDarkBuckupTools.MoreData
{
    /// <summary>
    /// 存档管理类(移植自TLD-Save-Editor的GameSave)
    /// 负责加载/保存漫漫长夜(slot)存档,boot/global数据
    /// </summary>
    public class GameSave
    {
        /// <summary>
        /// 最大备份数量
        /// </summary>
        public static int MAX_BACKUPS = 20;

        /// <summary>
        /// 最后保存时间(Ticks)
        /// </summary>
        public long LastSaved { get; set; }

        /// <summary>
        /// 存档文件路径
        /// </summary>
        public string path;

        private DynamicSerializable<SlotData> dynamicSlotData;
        /// <summary>
        /// slot数据
        /// </summary>
        public SlotData SlotData { get { return dynamicSlotData.Obj; } }

        private DynamicSerializable<BootSaveGameFormat> dynamicBoot;
        /// <summary>
        /// boot数据(区域、版本)
        /// </summary>
        public BootSaveGameFormat Boot { get { return dynamicBoot.Obj; } }

        private DynamicSerializable<GlobalSaveGameFormat> dynamicGlobal;
        /// <summary>
        /// global数据(玩家、背包、技能、病痛等)
        /// </summary>
        public GlobalSaveGameFormat Global { get { return dynamicGlobal.Obj; } }

        /// <summary>
        /// 病痛容器
        /// </summary>
        public AfflictionsContainer Afflictions { get; set; }

        /// <summary>
        /// 原始区域(用于检测换区域)
        /// </summary>
        public string OriginalRegion { get; set; }

        /// <summary>
        /// 加载存档
        /// </summary>
        /// <param name="path">存档文件路径</param>
        public void LoadSave(string path)
        {
            this.path = path;
            string slotJson = EncryptString.DecompressBytesToString(File.ReadAllBytes(path));
            dynamicSlotData = new DynamicSerializable<SlotData>(slotJson);

            var bootJson = EncryptString.DecompressBytesToString(SlotData.m_Dict.boot);
            dynamicBoot = new DynamicSerializable<BootSaveGameFormat>(bootJson);
            OriginalRegion = Boot.m_SceneName.Value;

            var globalJson = EncryptString.DecompressBytesToString(SlotData.m_Dict.global);
            dynamicGlobal = new DynamicSerializable<GlobalSaveGameFormat>(globalJson);

            Afflictions = new AfflictionsContainer(Global);
        }

        /// <summary>
        /// 深拷贝一个背包物品(完整复制全部字段,含未知字段),
        /// 并分配新的唯一实例ID。用于"复制物品并添加进背包"。
        /// </summary>
        public InventoryItemSaveData CloneItem(InventoryItemSaveData src)
        {
            var newGear = (GearItemSaveDataProxy)dynamicGlobal.CloneObject(src.Gear);
            if (newGear == null) throw new Exception("复制物品数据失败");

            // 分配唯一实例ID
            var rnd = new Random();
            int id = rnd.Next();
            while (Global.Inventory.Items.Any(i => i.Gear != null && i.Gear.m_InstanceIDProxy == id))
                id = rnd.Next();
            newGear.m_InstanceIDProxy = id;

            return new InventoryItemSaveData
            {
                m_PrefabName = src.m_PrefabName,
                Gear = newGear
            };
        }

        /// <summary>
        /// 保存存档
        /// </summary>
        public void Save()
        {
            LastSaved = DateTime.Now.Ticks;
            var bootSerialized = dynamicBoot.Serialize();
            SlotData.m_Dict.boot = EncryptString.CompressStringToBytes(bootSerialized);

            if (Boot.m_SceneName.Value != OriginalRegion)
            {
                Global.GameManagerData.SceneTransition.m_ForceNextSceneLoadTriggerScene = null;
            }
            Global.GameManagerData.SceneTransition.m_SceneSaveFilenameCurrent = Boot.m_SceneName.Value;
            Global.GameManagerData.SceneTransition.m_SceneSaveFilenameNextLoad = Boot.m_SceneName.Value;
            Global.PlayerManager.m_CheatsUsed = true;
            Afflictions.SerializeTo(Global);

            var globalSerialized = dynamicGlobal.Serialize();
            SlotData.m_Dict.global = EncryptString.CompressStringToBytes(globalSerialized);

            SlotData.m_Timestamp = DateTime.Now;
            var slotDataSerialized = dynamicSlotData.Serialize();
            File.WriteAllBytes(this.path, EncryptString.CompressStringToBytes(slotDataSerialized));
        }
    }
}
