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
        /// 保存存档
        /// </summary>
        public void Save()
        {
            Backup();

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

        /// <summary>
        /// 备份存档到backups目录
        /// </summary>
        public void Backup()
        {
            var backupDirectory = Path.Combine(Path.GetDirectoryName(this.path), "backups");
            Directory.CreateDirectory(backupDirectory);

            var oldBackups = new DirectoryInfo(backupDirectory).GetFiles().OrderByDescending(x => x.LastWriteTime).Skip(MAX_BACKUPS);
            foreach (var file in oldBackups)
            {
                File.Delete(file.FullName);
            }

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss", CultureInfo.InvariantCulture);
            var i = 1;
            var backupPath = Path.Combine(backupDirectory, timestamp + "-" + Path.GetFileName(this.path) + ".backup");
            while (File.Exists(backupPath))
            {
                backupPath = Path.Combine(backupDirectory, timestamp + "-" + Path.GetFileName(this.path) + "(" + i++ + ")" + ".backup");
            }
            File.Copy(this.path, backupPath);
        }
    }
}
