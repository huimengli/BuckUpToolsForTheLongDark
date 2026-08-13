using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Helpers;
using TheLongDarkBuckupTools.MoreData;
using TheLongDarkBuckupTools.Serialization;

namespace TheLongDarkBuckupTools
{
    public partial class ChangeFile2 : Form
    {
        /// <summary>
        /// 存档文件路径
        /// </summary>
        public string filePath;

        /// <summary>
        /// 存档文件信息
        /// </summary>
        private FileInfo fileInfo;

        /// <summary>
        /// 基础数据
        /// </summary>
        private string baseDataValue = "";

        /// <summary>
        /// 数据
        /// </summary>
        private SlotData data;

        /// <summary>
        /// 当前存档管理对象(包含boot/global解析)
        /// </summary>
        private GameSave currentSave;

        /// <summary>
        /// 档案对象(徽章/4DON)
        /// </summary>
        private Profile profile;

        /// <summary>
        /// 档案文件路径
        /// </summary>
        private string profilePath;

        /// <summary>
        /// 是否正在加载数据(抑制事件触发写回)
        /// </summary>
        private bool isLoading = false;

        /// <summary>
        /// 背包下拉框条目
        /// </summary>
        private class ItemEntry
        {
            public string Id;
            public string DisplayName;
            public override string ToString() { return DisplayName; }
        }

        /// <summary>
        /// 分类下拉框条目
        /// </summary>
        private class CategoryEntry
        {
            public ItemCategory? Category;
            public string Display;
            public override string ToString() { return Display; }
        }

        /// <summary>
        /// 分类中文名映射
        /// </summary>
        private static readonly KeyValuePair<ItemCategory, string>[] categoryNames = new[]
        {
            new KeyValuePair<ItemCategory, string>(ItemCategory.FirstAid, "急救"),
            new KeyValuePair<ItemCategory, string>(ItemCategory.Clothing, "衣物"),
            new KeyValuePair<ItemCategory, string>(ItemCategory.Food, "食物"),
            new KeyValuePair<ItemCategory, string>(ItemCategory.Tools, "工具"),
            new KeyValuePair<ItemCategory, string>(ItemCategory.Materials, "材料"),
            new KeyValuePair<ItemCategory, string>(ItemCategory.Collectible, "收藏品"),
            new KeyValuePair<ItemCategory, string>(ItemCategory.Books, "书籍"),
            new KeyValuePair<ItemCategory, string>(ItemCategory.Hidden, "隐藏"),
            new KeyValuePair<ItemCategory, string>(ItemCategory.Unknown, "其他"),
        };

        /// <summary>
        /// 设计器所需的无参构造函数
        /// </summary>
        public ChangeFile2()
        {
            InitializeComponent();
        }

        public ChangeFile2(string filePath)
        {
            InitializeComponent();

            this.filePath = filePath;
            this.Text = "存档修改工具";
        }

        private void ChangeFile2_Load(object sender, EventArgs e)
        {
            // 导出按钮只有debug可见
#if !DEBUG
            this.export.Visible = false;
#endif        

            this.fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists == false)
            {
                if (MessageBox.Show("文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    Close();
                    return;
                }
            }
            try
            {
                // 使用GameSave加载(自动解析boot/global/病痛)
                currentSave = new GameSave();
                currentSave.LoadSave(filePath);
                this.data = currentSave.SlotData;
                this.baseDataValue = EncryptString.DecompressBytesToString(File.ReadAllBytes(filePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show("存档加载失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
#if DEBUG
                throw ex;
#else
                return;
#endif
            }

            // 加载档案(徽章)
            LoadProfile();

            // 初始化下拉框
            InitCombos();

            // 绑定事件
            HookEvents();

            // 显示数据
            ShowData(data);
        }

        /// <summary>
        /// 加载档案文件(user*文件,最新修改优先)
        /// </summary>
        private void LoadProfile()
        {
            try
            {
                var dir = Path.GetDirectoryName(filePath);
                var userFile = Directory.GetFiles(dir, "user*")
                    .Where(f => !f.EndsWith(".backup", StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                    .FirstOrDefault();
                if (userFile != null)
                {
                    profilePath = userFile;
                    profile = new Profile(userFile);
                    labelProfileInfo.Text = "档案文件:" + Path.GetFileName(userFile);
                }
                else
                {
                    labelProfileInfo.Text = "未找到档案文件(user*)";
                }
            }
            catch (Exception ex)
            {
                labelProfileInfo.Text = "档案加载失败:" + ex.Message;
            }
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitCombos()
        {
            comboMode.Items.AddRange(Enum.GetNames(typeof(ExperienceModeType)));
            comboRegion.Items.AddRange(Enum.GetNames(typeof(RegionsWithMap)));

            // 分类下拉框
            comboCategory.BeginUpdate();
            comboCategory.Items.Clear();
            comboCategory.Items.Add(new CategoryEntry { Category = null, Display = "全部" });
            foreach (var kv in categoryNames)
            {
                comboCategory.Items.Add(new CategoryEntry { Category = kv.Key, Display = kv.Value });
            }
            comboCategory.EndUpdate();
            if (comboCategory.Items.Count > 0) comboCategory.SelectedIndex = 0;

            // 背包物品下拉框(按分类过滤)
            RefreshItemCombo();
        }

        /// <summary>
        /// 根据当前分类刷新物品下拉框
        /// </summary>
        private void RefreshItemCombo()
        {
            comboItem.BeginUpdate();
            comboItem.Items.Clear();
            var selected = comboCategory.SelectedItem as CategoryEntry;
            foreach (var kv in ItemDictionary.itemInfo)
            {
                if (kv.Value.hide) continue;
                if (selected != null && selected.Category.HasValue && kv.Value.category != selected.Category.Value) continue;
                comboItem.Items.Add(new ItemEntry { Id = kv.Key, DisplayName = ItemDictionary.GetInGameName(kv.Key) });
            }
            comboItem.EndUpdate();
            if (comboItem.Items.Count > 0) comboItem.SelectedIndex = 0;
        }

        /// <summary>
        /// 分类变化时刷新物品下拉框 + 背包列表(按分类过滤)
        /// </summary>
        private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            RefreshItemCombo();
            ShowInventoryTab();
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void HookEvents()
        {
            // 徽章Tab
            this.btnSaveProfile.Click += btnSaveProfile_Click;
            this.check4DON2018.CheckedChanged += check4DON2018_CheckedChanged;
            this.check4DON2019.CheckedChanged += check4DON2019_CheckedChanged;

            // 玩家Tab
            this.comboMode.SelectedIndexChanged += comboMode_SelectedIndexChanged;
            this.trackHP.ValueChanged += (s, e) => lblHPValue.Text = trackHP.Value + "%";
            this.trackThirst.ValueChanged += (s, e) => lblThirstValue.Text = trackThirst.Value + "%";
            this.trackFatigue.ValueChanged += (s, e) => lblFatigueValue.Text = trackFatigue.Value + "%";
            this.trackFreezing.ValueChanged += (s, e) => lblFreezingValue.Text = trackFreezing.Value + "%";

            // 病痛Tab
            this.btnCureSelected.Click += btnCureSelected_Click;
            this.btnCureAll.Click += btnCureAll_Click;
            this.btnRefreshAfflictions.Click += btnRefreshAfflictions_Click;

            // 背包Tab
            this.comboCategory.SelectedIndexChanged += comboCategory_SelectedIndexChanged;
            this.btnAddItem.Click += btnAddItem_Click;
            this.btnDeleteItem.Click += btnDeleteItem_Click;
            this.btnRepairAll.Click += btnRepairAll_Click;
            this.btnRemoveAll.Click += btnRemoveAll_Click;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                currentSave = new GameSave();
                currentSave.LoadSave(filePath);
                this.data = currentSave.SlotData;
                this.baseDataValue = EncryptString.DecompressBytesToString(File.ReadAllBytes(filePath));
                LoadProfile();
                ShowData(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("刷新失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 显示存档数据
        /// </summary>
        private void ShowData(SlotData data)
        {
            isLoading = true;
            try
            {
                // 总概
                textBox1.Text = fileInfo.Name;
                textBox2.Text = data.m_Name ?? data.m_InternalName;
                textBox3.Text = data.m_DisplayName;
                textBox4.Text = data.GetGameMode();
                textBox5.Text = data.m_VersionChangelistNumber == 0 ? data.m_Changelist.ToString() : data.m_VersionChangelistNumber.ToString();
                textBox6.Text = data.m_Timestamp.ToString();

                // 截图
                try
                {
                    var img = ((Screenshot)Item.DeserializeObject<Screenshot>(EncryptString.DecompressBytesToString(data.m_Dict.screenshot))).ToImage() ?? null;
                    pictureBox1.Visible = true;
                    pictureBox1.Image = img;
                }
                catch { }

                // 各Tab
                ShowFeatsTab();
                ShowPlayerTab();
                ShowSkillsTab();
                ShowAfflictionsTab();
                ShowInventoryTab();
            }
            finally
            {
                isLoading = false;
            }
        }

#region 徽章Tab

        /// <summary>
        /// 显示徽章数据
        /// </summary>
        private void ShowFeatsTab()
        {
            if (profile == null || profile.State.Feats == null) return;
            var feats = profile.State.Feats;

            SetFeatTrack(trackBookSmarts, lblBookSmarts, feats.BookSmarts?.m_HoursResearch ?? 0, "研究小时数");
            SetFeatTrack(trackColdFusion, lblColdFusion, (int)(feats.ColdFusion?.m_ElapsedDays ?? 0), "经过天数");
            SetFeatTrack(trackEfficientMachine, lblEfficientMachine, (int)(feats.EfficientMachine?.m_ElapsedHours ?? 0), "机器运行小时");
            SetFeatTrack(trackFireMaster, lblFireMaster, feats.FireMaster?.m_NumFiresStarted ?? 0, "生火次数");
            SetFeatTrack(trackFreeRunner, lblFreeRunner, (int)(feats.FreeRunner?.m_ElapsedKilometers ?? 0), "奔跑公里数");
            SetFeatTrack(trackSnowWalker, lblSnowWalker, (int)(feats.SnowWalker?.m_ElapsedKilometers ?? 0), "行走公里数");
            SetFeatTrack(trackExpertTrapper, lblExpertTrapper, feats.ExpertTrapper?.m_RabbitSnaredCount ?? 0, "捕兔数");
            SetFeatTrack(trackStraightToHeart, lblStraightToHeart, feats.StraightToHeart?.m_ItemConsumedCount ?? 0, "消耗物品数");
            SetFeatTrack(trackBlizzardWalker, lblBlizzardWalker, (int)(feats.BlizzardWalker?.m_BlizzardHoursOutside ?? 0), "暴风雪外小时");

            // 4DON
            var don2018 = profile.State.m_DaysCompleted4DON;
            var don2019 = profile.State.m_DaysCompleted4DON2019;
            check4DON2018.Checked = don2018 != null && don2018.Count > 0 && don2018.All(x => x);
            check4DON2019.Checked = don2019 != null && don2019.Count > 0 && don2019.All(x => x);
        }

        private void SetFeatTrack(TrackBar track, Label lbl, int value, string unit)
        {
            track.Value = Math.Max(track.Minimum, Math.Min(track.Maximum, value));
            lbl.Text = unit + ":" + value + " / " + track.Maximum;
        }

        private void check4DON2018_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoading || profile == null) return;
            if (profile.State.m_DaysCompleted4DON == null) profile.State.m_DaysCompleted4DON = new List<bool>();
            var list = profile.State.m_DaysCompleted4DON;
            while (list.Count < 7) list.Add(false);
            for (int i = 0; i < 7; i++) list[i] = check4DON2018.Checked;
        }

        private void check4DON2019_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoading || profile == null) return;
            if (profile.State.m_DaysCompleted4DON2019 == null) profile.State.m_DaysCompleted4DON2019 = new List<bool>();
            var list = profile.State.m_DaysCompleted4DON2019;
            while (list.Count < 7) list.Add(false);
            for (int i = 0; i < 7; i++) list[i] = check4DON2019.Checked;
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (profile == null)
            {
                MessageBox.Show("未找到档案文件,无法保存!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                profile.Save();
                MessageBox.Show("档案保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("档案保存失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

#endregion

#region 玩家Tab

        /// <summary>
        /// 显示玩家数据
        /// </summary>
        private void ShowPlayerTab()
        {
            var global = currentSave.Global;

            // 模式
            var modeType = global.ExperienceModeManager?.m_CurrentModeType;
            var modeName = modeType?.Value;
            int idx = Array.IndexOf(Enum.GetNames(typeof(ExperienceModeType)), modeName);
            comboMode.SelectedIndex = idx >= 0 ? idx : 0;
            textCustomMode.Text = global.ExperienceModeManager?.m_CustomModeString ?? "";
            comboMode_SelectedIndexChanged(null, EventArgs.Empty);

            // 区域
            var sceneName = currentSave.Boot.m_SceneName?.Value;
            int ridx = Array.IndexOf(Enum.GetNames(typeof(RegionsWithMap)), sceneName);
            comboRegion.SelectedIndex = ridx >= 0 ? ridx : 0;

            // 位置(X=pos[0], Y=pos[2], Z=pos[1])
            var pos = global.PlayerManager?.m_SaveGamePosition;
            if (pos == null || pos.Count < 3)
            {
                textPosX.Text = "0"; textPosY.Text = "0"; textPosZ.Text = "0";
            }
            else
            {
                textPosX.Text = pos[0].ToString("0.0");
                textPosY.Text = pos[2].ToString("0.0");
                textPosZ.Text = pos[1].ToString("0.0");
            }

            // 状态(代理值本身为0~100,直接绑定,clamp防止存档异常值越界)
            trackHP.Value = Clamp((int)(global.Condition?.m_CurrentHPProxy ?? 0), 0, trackHP.Maximum);
            lblHPValue.Text = trackHP.Value + "%";
            textCalories.Text = ((int)(global.Hunger?.m_CurrentReserveCaloriesProxy ?? 0)).ToString();
            trackThirst.Value = Clamp((int)(global.Thirst?.m_CurrentThirstProxy ?? 0), 0, trackThirst.Maximum);
            lblThirstValue.Text = trackThirst.Value + "%";
            trackFatigue.Value = Clamp((int)(global.Fatigue?.m_CurrentFatigueProxy ?? 0), 0, trackFatigue.Maximum);
            lblFatigueValue.Text = trackFatigue.Value + "%";
            trackFreezing.Value = Clamp((int)(global.Freezing?.m_CurrentFreezingProxy ?? 0), 0, trackFreezing.Maximum);
            lblFreezingValue.Text = trackFreezing.Value + "%";

            // 作弊
            checkNeverDie.Checked = global.Condition?.m_NeverDieProxy ?? false;
            checkInvulnerable.Checked = global.Condition?.m_Invulnerable ?? false;
            checkInfiniteCarry.Checked = global.Inventory?.m_ForceOverrideWeight ?? false;
            checkGod.Checked = global.PlayerManager?.m_God ?? false;
        }

        private void comboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboMode.SelectedIndex < 0) return;
            var name = (string)comboMode.SelectedItem;
            textCustomMode.Enabled = (name == ExperienceModeType.Custom.ToString());
        }

#endregion

#region 技能Tab

        /// <summary>
        /// 显示技能数据
        /// </summary>
        private void ShowSkillsTab()
        {
            var skills = currentSave.Global.SkillsManager;
            if (skills == null) return;

            numSkill1.Value = Clamp((int)(skills.Firestarting?.m_Points ?? 0), (int)numSkill1.Minimum, (int)numSkill1.Maximum);
            numSkill2.Value = Clamp((int)(skills.CarcassHarvesting?.m_Points ?? 0), (int)numSkill2.Minimum, (int)numSkill2.Maximum);
            numSkill3.Value = Clamp((int)(skills.Cooking?.m_Points ?? 0), (int)numSkill3.Minimum, (int)numSkill3.Maximum);
            numSkill4.Value = Clamp((int)(skills.IceFishing?.m_Points ?? 0), (int)numSkill4.Minimum, (int)numSkill4.Maximum);
            numSkill5.Value = Clamp((int)(skills.Rifle?.m_Points ?? 0), (int)numSkill5.Minimum, (int)numSkill5.Maximum);
            numSkill6.Value = Clamp((int)(skills.Archery?.m_Points ?? 0), (int)numSkill6.Minimum, (int)numSkill6.Maximum);
            numSkill7.Value = Clamp((int)(skills.ClothingRepair?.m_Points ?? 0), (int)numSkill7.Minimum, (int)numSkill7.Maximum);
            numSkill8.Value = Clamp((int)(skills.Revolver?.m_Points ?? 0), (int)numSkill8.Minimum, (int)numSkill8.Maximum);
            numSkill9.Value = Clamp((int)(skills.Gunsmith?.m_Points ?? 0), (int)numSkill9.Minimum, (int)numSkill9.Maximum);
        }

        private int Clamp(int v, int min, int max)
        {
            return Math.Max(min, Math.Min(max, v));
        }

#endregion

#region 病痛Tab

        /// <summary>
        /// 显示病痛数据
        /// </summary>
        private void ShowAfflictionsTab()
        {
            listViewAfflictions.Items.Clear();
            if (currentSave.Afflictions == null) return;

            foreach (var aff in currentSave.Afflictions.Negative)
            {
                var item = new ListViewItem(new[] { GetAfflictionName(aff), GetAfflictionDetail(aff), GetAfflictionTime(aff) });
                item.Tag = aff;
                listViewAfflictions.Items.Add(item);
            }
            lblAfflictionCount.Text = "当前负面状态数量:" + currentSave.Afflictions.Negative.Count;
        }

        private string GetAfflictionName(Affliction a)
        {
            return a.AfflictionType.ToString();
        }

        private string GetAfflictionDetail(Affliction a)
        {
            switch (a)
            {
                case Hypothermia h: return "寒冷暴露 " + h.ElapsedHours.ToString("0.0") + " 小时";
                case Frostbite f: return "部位" + f.Location + " 伤害 " + f.Damage.ToString("0.0");
                case FoodPoisoning fp: return "已持续 " + fp.ElapsedHours.ToString("0.0") + " 小时";
                case Dysentery d: return "已持续 " + d.ElapsedHours.ToString("0.0") + " 小时";
                case SprainAffliction s: return "部位" + s.Location + " 已持续 " + s.ElapsedHours.ToString("0.0") + " 小时";
                case Burns b: return "已持续 " + b.ElapsedHours.ToString("0.0") + " 小时";
                case BurnsElectric be: return "已持续 " + be.ElapsedHours.ToString("0.0") + " 小时";
                case BloodLoss bl: return "已持续 " + bl.ElapsedHours.ToString("0.0") + " 小时";
                case Infection i: return "已持续 " + i.ElapsedHours.ToString("0.0") + " 小时";
                case InfectionRisk ir: return "感染几率 " + (ir.CurrentInfectionChance * 100).ToString("0") + "%";
                case CabinFever cf: return "已持续 " + cf.ElapsedHours.ToString("0.0") + " 小时";
                case IntestinalParasites ip: return "已持续 " + ip.ParasitesElapsedHours.ToString("0.0") + " 小时";
                case BrokenRib br: return "已持续 " + br.ElapsedRest.ToString("0.0") + " 小时";
                case WellFed wf: return "已持续 " + wf.ElapsedHoursNotStarving.ToString("0.0") + " 小时";
                default: return "";
            }
        }

        private string GetAfflictionTime(Affliction a)
        {
            switch (a)
            {
                case Hypothermia h: return h.ElapsedHours.ToString("0.0") + "h";
                case Frostbite f: return "";
                case FoodPoisoning fp: return fp.ElapsedHours.ToString("0.0") + "/" + fp.DurationHours.ToString("0.0") + "h";
                case Dysentery d: return d.ElapsedHours.ToString("0.0") + "/" + d.DurationHours.ToString("0.0") + "h";
                case SprainAffliction s: return s.ElapsedHours.ToString("0.0") + "/" + s.Duration.ToString("0.0") + "h";
                case Burns b: return b.ElapsedHours.ToString("0.0") + "/" + b.DurationHours.ToString("0.0") + "h";
                case BurnsElectric be: return be.ElapsedHours.ToString("0.0") + "/" + be.DurationHours.ToString("0.0") + "h";
                case BloodLoss bl: return bl.ElapsedHours.ToString("0.0") + "/" + bl.DurationHours.ToString("0.0") + "h";
                case Infection i: return i.ElapsedHours.ToString("0.0") + "/" + i.DurationHours.ToString("0.0") + "h";
                case InfectionRisk ir: return ir.ElapsedHours.ToString("0.0") + "/" + ir.DurationHours.ToString("0.0") + "h";
                case CabinFever cf: return cf.ElapsedHours.ToString("0.0") + "h";
                case IntestinalParasites ip: return ip.ParasitesElapsedHours.ToString("0.0") + "h";
                case BrokenRib br: return br.ElapsedRest.ToString("0.0") + "h";
                case WellFed wf: return wf.ElapsedHoursNotStarving.ToString("0.0") + "h";
                default: return "";
            }
        }

        private void btnCureSelected_Click(object sender, EventArgs e)
        {
            if (listViewAfflictions.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择要治愈的病痛!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var toRemove = new List<Affliction>();
            foreach (ListViewItem item in listViewAfflictions.SelectedItems)
            {
                if (item.Tag is Affliction aff) toRemove.Add(aff);
            }
            foreach (var aff in toRemove)
            {
                aff.RemoveCommand.Execute(null);
            }
            ShowAfflictionsTab();
        }

        private void btnCureAll_Click(object sender, EventArgs e)
        {
            if (currentSave.Afflictions == null) return;
            var toRemove = currentSave.Afflictions.Negative.ToList();
            foreach (var aff in toRemove)
            {
                aff.RemoveCommand.Execute(null);
            }
            ShowAfflictionsTab();
        }

        private void btnRefreshAfflictions_Click(object sender, EventArgs e)
        {
            ShowAfflictionsTab();
        }

#endregion

#region 背包Tab

        /// <summary>
        /// 显示背包数据(按当前分类下拉框过滤,选中"全部"时显示所有)
        /// </summary>
        private void ShowInventoryTab()
        {
            var inventory = currentSave.Global.Inventory;
            if (inventory == null) return;

            // 当前选中的分类过滤条件
            var selectedCat = comboCategory.SelectedItem as CategoryEntry;
            ItemCategory? filterCat = selectedCat?.Category;

            if (dataGridViewItems.Columns.Count == 0)
            {
                dataGridViewItems.Columns.Add("colName", "名称");
                dataGridViewItems.Columns.Add("colID", "物品ID");
                dataGridViewItems.Columns.Add("colUnits", "数量");
                dataGridViewItems.Columns.Add("colLiters", "容量(L)");
                dataGridViewItems.Columns.Add("colCond", "耐久");
                dataGridViewItems.Columns.Add("colWeight", "重量(kg)");
                dataGridViewItems.Columns.Add("colCal", "卡路里");
                foreach (DataGridViewColumn col in dataGridViewItems.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            dataGridViewItems.Rows.Clear();
            double totalWeight = 0;
            foreach (var item in inventory.Items)
            {
                var gear = item.Gear;
                if (gear == null) continue;

                // 分类过滤:选中非"全部"分类时,只显示匹配分类的物品
                if (filterCat.HasValue && ItemDictionary.GetCategory(item.m_PrefabName) != filterCat.Value) continue;

                string name = ItemDictionary.GetInGameName(item.m_PrefabName);
                int units = gear.StackableItem?.m_UnitsProxy ?? 1;
                // 液体容量:过氧化氢/灯油等用 LiquidItem,水壶等用 WaterSupply(存档均为 实际升×1e9,需换算)
                float liters = 0;
                if (gear.LiquidItem != null) liters = ItemWeights.ToLiters(gear.LiquidItem.m_LiquidLitersProxy);
                else if (gear.WaterSupply != null) liters = ItemWeights.ToLiters(gear.WaterSupply.m_VolumeProxy);
                string litersText = liters > 0 ? liters.ToString("0.00") : "";
                string cond = (gear.NormalizedCondition * 100).ToString("0") + "%";
                // 粉末物品(盐/面粉等)重量由 PowderItem.m_WeightKG 决定,优先取它
                float weightKg = (gear.PowderItem != null && gear.PowderItem.m_WeightKG > 0)
                    ? ItemWeights.ToKg(gear.PowderItem.m_WeightKG)
                    : ItemWeights.ToKg(gear.m_WeightKG);
                string weight = weightKg.ToString("0.00");
                string cal = gear.FoodItem != null ? ((int)gear.FoodItem.m_CaloriesRemainingProxy).ToString() : "";
                totalWeight += weightKg;

                int rowIdx = dataGridViewItems.Rows.Add(name, item.m_PrefabName, units, litersText, cond, weight, cal);
                dataGridViewItems.Rows[rowIdx].Tag = item;
            }

            var carryWeight = Math.Round(totalWeight, 2);
            lblWeightInfo.Text = "负重:" + carryWeight.ToString("0.00") + "kg 共" + dataGridViewItems.Rows.Count + "件";
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (comboItem.SelectedItem == null)
            {
                MessageBox.Show("请先选择要添加的物品!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var entry = (ItemEntry)comboItem.SelectedItem;
            if (!ItemDictionary.itemInfo.ContainsKey(entry.Id))
            {
                MessageBox.Show("物品不存在于字典:" + entry.Id, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var info = ItemDictionary.itemInfo[entry.Id];
                var gear = new GearItemSaveDataProxy();
                JsonConvert.PopulateObject(info.defaultSerialized, gear);
                gear.m_HoursPlayed = currentSave.Global.TimeOfDay?.m_HoursPlayedNotPausedProxy ?? 0;
                gear.m_BeenInPlayerInventoryProxy = true;
                gear.NormalizedCondition = 1;

                // 液体容量换算:defaultSerialized 中为实际升,存档存储需 ×1e9(过氧化氢/灯油/水壶等)
                if (gear.LiquidItem != null && gear.LiquidItem.m_LiquidLitersProxy > 0)
                    gear.LiquidItem.m_LiquidLitersProxy = ItemWeights.ToStorageLiters(gear.LiquidItem.m_LiquidLitersProxy);
                if (gear.WaterSupply != null && gear.WaterSupply.m_VolumeProxy > 0)
                    gear.WaterSupply.m_VolumeProxy = ItemWeights.ToStorageLiters(gear.WaterSupply.m_VolumeProxy);

                // 设置默认重量(存档内单位为 kg×1e9):优先查重量表,其次复制背包中同ID物品,最后默认0.5kg
                float defaultKg;
                if (ItemWeights.TryGetDefaultWeight(entry.Id, out defaultKg))
                {
                    gear.m_WeightKG = ItemWeights.ToStorage(defaultKg);
                }
                else
                {
                    var sameItem = currentSave.Global.Inventory.Items.FirstOrDefault(i => i.m_PrefabName == entry.Id && i.Gear != null && i.Gear.m_WeightKG > 0);
                    if (sameItem != null)
                        gear.m_WeightKG = sameItem.Gear.m_WeightKG;
                    else
                        gear.m_WeightKG = ItemWeights.ToStorage(0.5f);
                }

                // 粉末物品(盐/盐袋/面粉/燕麦等):游戏读取 PowderItem.m_WeightKG 计算重量,需同步设置,否则游戏内显示 0.00kg
                if (gear.PowderItem != null)
                    gear.PowderItem.m_WeightKG = gear.m_WeightKG;

                // 分配唯一实例ID
                var rnd = new Random();
                int id = rnd.Next();
                while (currentSave.Global.Inventory.Items.Any(i => i.Gear != null && i.Gear.m_InstanceIDProxy == id))
                    id = rnd.Next();
                gear.m_InstanceIDProxy = id;

                var item = new InventoryItemSaveData
                {
                    m_PrefabName = entry.Id,
                    Gear = gear
                };
                currentSave.Global.Inventory.Items.Add(item);
                ShowInventoryTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加物品失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的物品!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var toRemove = new List<InventoryItemSaveData>();
            foreach (DataGridViewRow row in dataGridViewItems.SelectedRows)
            {
                if (row.Tag is InventoryItemSaveData item) toRemove.Add(item);
            }
            foreach (var item in toRemove)
            {
                currentSave.Global.Inventory.Items.Remove(item);
            }
            ShowInventoryTab();
        }

        private void btnRepairAll_Click(object sender, EventArgs e)
        {
            foreach (var item in currentSave.Global.Inventory.Items)
            {
                var gear = item.Gear;
                if (gear == null) continue;
                gear.NormalizedCondition = 1;
                gear.m_WornOut = false;
                if (gear.FlareItem != null && gear.FlareItem.m_StateProxy != null)
                    gear.FlareItem.m_StateProxy.SetValue(FlareState.Fresh);
                if (gear.TorchItem != null && gear.TorchItem.m_StateProxy != null)
                    gear.TorchItem.m_StateProxy.SetValue(TorchState.Fresh);
            }
            ShowInventoryTab();
            MessageBox.Show("已修复全部物品!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定移除全部物品吗?(任务关键物品将保留)", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            for (int i = currentSave.Global.Inventory.Items.Count - 1; i >= 0; i--)
            {
                var item = currentSave.Global.Inventory.Items[i];
                if (ItemDictionary.itemInfo.ContainsKey(item.m_PrefabName) && ItemDictionary.itemInfo[item.m_PrefabName].preventDelete)
                    continue;
                currentSave.Global.Inventory.Items.RemoveAt(i);
            }
            ShowInventoryTab();
            MessageBox.Show("已移除全部物品!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 获取当前选中行的装备数据
        /// </summary>
        private GearItemSaveDataProxy GetSelectedGear()
        {
            if (dataGridViewItems.SelectedRows.Count == 0) return null;
            var row = dataGridViewItems.SelectedRows[0];
            var item = row.Tag as InventoryItemSaveData;
            return item?.Gear;
        }

        /// <summary>
        /// 右键菜单打开时按选中行启用/禁用菜单项
        /// </summary>
        private void contextMenuStripItems_Opening(object sender, CancelEventArgs e)
        {
            bool hasRow = dataGridViewItems.SelectedRows.Count > 0;
            var gear = GetSelectedGear();
            bool hasCal = hasRow && gear != null && gear.FoodItem != null;
            // 液体物品:过氧化氢/灯油等(LiquidItem)或水壶(WaterSupply)可改容量
            bool hasLiquid = hasRow && gear != null && (gear.LiquidItem != null || gear.WaterSupply != null);

            copyBoth.Enabled = hasRow;
            cloneItem.Enabled = hasRow;
            setUnits.Enabled = hasRow && gear != null && gear.StackableItem != null;
            setLiters.Enabled = hasLiquid;
            setCalories.Enabled = hasCal;
            setWeight.Enabled = hasRow;
            translateOnline.Enabled = hasRow;
            searchOnline.Enabled = hasRow;
        }

        /// <summary>
        /// 复制物品ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyID_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows.Count == 0) return;
            var row = dataGridViewItems.SelectedRows[0];
            var id = row.Cells["colID"].Value?.ToString() ?? "";
            Clipboard.SetText(id);
        }

        /// <summary>
        /// 复制物品名称+物品ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyBoth_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows.Count == 0) return;
            var row = dataGridViewItems.SelectedRows[0];
            var name = row.Cells["colName"].Value?.ToString() ?? "";
            var id = row.Cells["colID"].Value?.ToString() ?? "";
            Clipboard.SetText(name + "  " + id);
        }

        /// <summary>
        /// 复制当前选中物品(深拷贝全部数据)并添加进背包
        /// </summary>
        private void cloneItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows.Count == 0) return;
            var row = dataGridViewItems.SelectedRows[0];
            var src = row.Tag as InventoryItemSaveData;
            if (src == null || src.Gear == null) return;

            try
            {
                var newItem = currentSave.CloneItem(src);
                currentSave.Global.Inventory.Items.Add(newItem);
                ShowInventoryTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show("复制物品失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 修改物品数量(堆叠数)
        /// </summary>
        private void setUnits_Click(object sender, EventArgs e)
        {
            var gear = GetSelectedGear();
            if (gear == null || gear.StackableItem == null) return;

            var box = new InputBox("修改数量", "输入新的数量(当前:" + gear.StackableItem.m_UnitsProxy + "):");
            box.ShowDialog();
            string result = InputBox.returnValue;
            if (string.IsNullOrEmpty(result)) return;

            int v;
            if (!int.TryParse(result, out v) || v < 1)
            {
                MessageBox.Show("请输入有效的正整数!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            gear.StackableItem.m_UnitsProxy = v;
            ShowInventoryTab();
        }

        /// <summary>
        /// 修改液体容量(升),支持 LiquidItem(过氧化氢/灯油)和 WaterSupply(水壶)。
        /// 存档存储为 实际升×1e9,需换算。
        /// </summary>
        private void setLiters_Click(object sender, EventArgs e)
        {
            var gear = GetSelectedGear();
            if (gear == null || (gear.LiquidItem == null && gear.WaterSupply == null)) return;

            float cur = gear.LiquidItem != null ? gear.LiquidItem.m_LiquidLitersProxy : gear.WaterSupply.m_VolumeProxy;
            var box = new InputBox("修改容量", "输入新的液体容量(升)(当前:" + ItemWeights.ToLiters(cur).ToString("0.00") + "):");
            box.ShowDialog();
            string result = InputBox.returnValue;
            if (string.IsNullOrEmpty(result)) return;

            float v;
            if (!float.TryParse(result, out v) || v < 0)
            {
                MessageBox.Show("请输入有效的非负数值!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (v > ItemWeights.MaxLiters)
            {
                MessageBox.Show("容量超出游戏上限!\n\n游戏内部容量单位为 Int64(1升 = 1e9 units),最大支持约 " + ItemWeights.MaxLiters.ToString("0.00") + " 升。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (gear.LiquidItem != null) gear.LiquidItem.m_LiquidLitersProxy = ItemWeights.ToStorageLiters(v);
            else gear.WaterSupply.m_VolumeProxy = ItemWeights.ToStorageLiters(v);
            ShowInventoryTab();
        }

        private void setCalories_Click(object sender, EventArgs e)
        {
            var gear = GetSelectedGear();
            if (gear == null || gear.FoodItem == null) return;

            var box = new InputBox("修改卡路里", "输入新的卡路里数值(当前:" + ((int)gear.FoodItem.m_CaloriesRemainingProxy) + "):");
            box.ShowDialog();
            string result = InputBox.returnValue;
            if (string.IsNullOrEmpty(result)) return;

            float v;
            if (!float.TryParse(result, out v) || v < 0)
            {
                MessageBox.Show("请输入有效的非负数值!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            gear.FoodItem.m_CaloriesRemainingProxy = v;
            ShowInventoryTab();
        }

        private void setWeight_Click(object sender, EventArgs e)
        {
            var gear = GetSelectedGear();
            if (gear == null) return;

            var box = new InputBox("修改重量", "输入新的重量(kg)(当前:" + ItemWeights.ToKg(gear.m_WeightKG).ToString("0.00") + "):");
            box.ShowDialog();
            string result = InputBox.returnValue;
            if (string.IsNullOrEmpty(result)) return;

            float v;
            if (!float.TryParse(result, out v) || v < 0)
            {
                MessageBox.Show("请输入有效的非负数值!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            gear.m_WeightKG = ItemWeights.ToStorage(v);
            // 粉末物品(盐/面粉等)重量由 PowderItem.m_WeightKG 决定,需同步设置,否则游戏内不生效
            if (gear.PowderItem != null)
                gear.PowderItem.m_WeightKG = gear.m_WeightKG;
            ShowInventoryTab();
        }

        /// <summary>
        /// 在线翻译道具名称(打开Bing翻译,把英文名翻译成中文)
        /// </summary>
        private void translateOnline_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows.Count == 0) return;
            var row = dataGridViewItems.SelectedRows[0];
            var name = row.Cells["colName"].Value?.ToString() ?? "";
            var id = row.Cells["colID"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(id)) return;

            try
            {
                // 优先用中文名称,没有中文名时用物品ID
                //var query = Uri.EscapeDataString(string.IsNullOrEmpty(name) ? id : name);
                // 直接搜索物品ID
                var query = Uri.EscapeDataString(id);
                System.Diagnostics.Process.Start("https://www.bing.com/translator/?from=en&to=zh-Hans&text=" + query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开浏览器失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 在线搜索道具名称(打开浏览器搜索该物品)
        /// </summary>
        private void searchOnline_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows.Count == 0) return;
            var row = dataGridViewItems.SelectedRows[0];
            var id = row.Cells["colID"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(id)) return;

            try
            {
                var query = Uri.EscapeDataString("\"The Long Dark\" " + id);
                System.Diagnostics.Process.Start("https://www.bing.com/search?q=" + query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开浏览器失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

#endregion

#region 保存/备份

        /// <summary>
        /// 保存按钮
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                CollectUI();
                currentSave.Save();
                textBox6.Text = data.m_Timestamp.ToString();
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 备份按钮
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                currentSave.Backup();
                MessageBox.Show("备份成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 收集界面改动写回存档对象
        /// </summary>
        private void CollectUI()
        {
            var global = currentSave.Global;

            // 模式
            if (comboMode.SelectedIndex >= 0)
            {
                var modeName = (string)comboMode.SelectedItem;
                var mode = (ExperienceModeType)Enum.Parse(typeof(ExperienceModeType), modeName);
                if (global.ExperienceModeManager == null) global.ExperienceModeManager = new ExperienceModeManagerSaveDataProxy();
                if (global.ExperienceModeManager.m_CurrentModeType == null)
                    global.ExperienceModeManager.m_CurrentModeType = new EnumWrapper<ExperienceModeType>(mode.ToString());
                else
                    global.ExperienceModeManager.m_CurrentModeType.SetValue(mode);
                global.ExperienceModeManager.m_CustomModeString = textCustomMode.Text;
            }

            // 玩家区域/位置暂时不允许修改,仅显示(如需修改,后续再开放)

            // 状态(代理值为0~100,与读取保持一致,直接写回)
            if (global.Condition == null) global.Condition = new ConditionSaveDataProxy();
            global.Condition.m_CurrentHPProxy = trackHP.Value;
            if (global.Hunger == null) global.Hunger = new HungerSaveDataProxy();
            global.Hunger.m_CurrentReserveCaloriesProxy = ParseFloat(textCalories.Text);
            if (global.Thirst == null) global.Thirst = new ThirstSaveDataProxy();
            global.Thirst.m_CurrentThirstProxy = trackThirst.Value;
            if (global.Fatigue == null) global.Fatigue = new FatigueSaveDataProxy();
            global.Fatigue.m_CurrentFatigueProxy = trackFatigue.Value;
            if (global.Freezing == null) global.Freezing = new FreezingSaveDataProxy();
            global.Freezing.m_CurrentFreezingProxy = trackFreezing.Value;

            // 作弊
            global.Condition.m_NeverDieProxy = checkNeverDie.Checked;
            global.Condition.m_Invulnerable = checkInvulnerable.Checked;
            if (global.Inventory == null) global.Inventory = new InventorySaveDataProxy();
            global.Inventory.m_ForceOverrideWeight = checkInfiniteCarry.Checked;
            global.PlayerManager.m_God = checkGod.Checked;

            // 技能
            var skills = global.SkillsManager;
            if (skills == null) global.SkillsManager = skills = new SkillsManagerSaveData();
            if (skills.Firestarting == null) skills.Firestarting = new Skill_FirestartingSaveData();
            skills.Firestarting.m_Points = (int)numSkill1.Value;
            if (skills.CarcassHarvesting == null) skills.CarcassHarvesting = new Skill_CarcassHarvestingSaveData();
            skills.CarcassHarvesting.m_Points = (int)numSkill2.Value;
            if (skills.Cooking == null) skills.Cooking = new Skill_CookingSaveData();
            skills.Cooking.m_Points = (int)numSkill3.Value;
            if (skills.IceFishing == null) skills.IceFishing = new Skill_IceFishingSaveData();
            skills.IceFishing.m_Points = (int)numSkill4.Value;
            if (skills.Rifle == null) skills.Rifle = new Skill_RifleSaveData();
            skills.Rifle.m_Points = (int)numSkill5.Value;
            if (skills.Archery == null) skills.Archery = new Skill_ArcherySaveData();
            skills.Archery.m_Points = (int)numSkill6.Value;
            if (skills.ClothingRepair == null) skills.ClothingRepair = new Skill_ClothingRepairSaveData();
            skills.ClothingRepair.m_Points = (int)numSkill7.Value;
            if (skills.Revolver == null) skills.Revolver = new Skill_RevolverSaveData();
            skills.Revolver.m_Points = (int)numSkill8.Value;
            if (skills.Gunsmith == null) skills.Gunsmith = new Skill_GunsmithingSaveData();
            skills.Gunsmith.m_Points = (int)numSkill9.Value;

            // 徽章/档案
            if (profile != null)
            {
                var feats = profile.State.Feats;
                if (feats != null)
                {
                    if (feats.BookSmarts == null) feats.BookSmarts = new Feat_BookSmartsSaveData();
                    feats.BookSmarts.m_HoursResearch = trackBookSmarts.Value;
                    if (feats.ColdFusion == null) feats.ColdFusion = new Feat_ColdFusionSaveData();
                    feats.ColdFusion.m_ElapsedDays = trackColdFusion.Value;
                    if (feats.EfficientMachine == null) feats.EfficientMachine = new Feat_EfficientMachineSaveData();
                    feats.EfficientMachine.m_ElapsedHours = trackEfficientMachine.Value;
                    if (feats.FireMaster == null) feats.FireMaster = new Feat_FireMasterSaveData();
                    feats.FireMaster.m_NumFiresStarted = trackFireMaster.Value;
                    if (feats.FreeRunner == null) feats.FreeRunner = new Feat_FreeRunnerSaveData();
                    feats.FreeRunner.m_ElapsedKilometers = trackFreeRunner.Value;
                    if (feats.SnowWalker == null) feats.SnowWalker = new Feat_SnowWalkerSaveData();
                    feats.SnowWalker.m_ElapsedKilometers = trackSnowWalker.Value;
                    if (feats.ExpertTrapper == null) feats.ExpertTrapper = new Feat_ExpertTrapperSaveData();
                    feats.ExpertTrapper.m_RabbitSnaredCount = trackExpertTrapper.Value;
                    if (feats.StraightToHeart == null) feats.StraightToHeart = new Feat_StraightToHeartSaveData();
                    feats.StraightToHeart.m_ItemConsumedCount = trackStraightToHeart.Value;
                    if (feats.BlizzardWalker == null) feats.BlizzardWalker = new Feat_BlizzardWalkerSaveData();
                    feats.BlizzardWalker.m_BlizzardHoursOutside = trackBlizzardWalker.Value;
                }
                // 保存档案
                profile.Save();
            }
        }

        private float ParseFloat(string s)
        {
            float v;
            if (float.TryParse(s, out v)) return v;
            return 0;
        }

        #endregion

        private void export_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentSave?.Global?.Inventory == null || currentSave.Global.Inventory.Items == null)
                {
                    MessageBox.Show("请先加载存档!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 收集所有未收录于物品字典(无名称/无清单条目)的物品,按 ID 去重
                var unknown = new Dictionary<string, GearItemSaveDataProxy>();
                foreach (var item in currentSave.Global.Inventory.Items)
                {
                    if (item.Gear == null) continue;
                    if (ItemDictionary.itemInfo.ContainsKey(item.m_PrefabName)) continue; // 已收录的跳过
                    if (!unknown.ContainsKey(item.m_PrefabName))
                        unknown[item.m_PrefabName] = item.Gear;
                }

                if (unknown.Count == 0)
                {
                    MessageBox.Show("当前存档中的物品都已收录,没有可导出的未收录物品!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 构建导出结构:key = 物品ID,value = 子组件结构(可直接作为 ItemDictionary 的 defaultSerialized 使用)
                var export = new Dictionary<string, object>();
                foreach (var kv in unknown)
                    export[kv.Key] = ExtractItemStructure(kv.Value);

                using (var dlg = new SaveFileDialog())
                {
                    dlg.Title = "导出未收录物品结构";
                    dlg.Filter = "JSON 文件 (*.json)|*.json";
                    dlg.FileName = "未收录物品结构_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json";
                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    var settings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        FloatFormatHandling = FloatFormatHandling.Symbol,
                        Converters = { new IntegralFloatConverter() }
                    };
                    var json = JsonConvert.SerializeObject(export, settings);
                    File.WriteAllText(dlg.FileName, json, new UTF8Encoding(true));

                    MessageBox.Show("已导出 " + unknown.Count + " 个未收录物品的结构到:\n" + dlg.FileName, "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 提取物品 Gear 的非空子组件结构(与 ItemDictionary.defaultSerialized 格式一致)。
        /// 液体/重量换算为实际单位(升/kg),以便直接作为添加物品清单的默认结构。
        /// </summary>
        private static object ExtractItemStructure(GearItemSaveDataProxy gear)
        {
            var result = new Dictionary<string, object>();

            foreach (var prop in typeof(GearItemSaveDataProxy).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                var attr = prop.GetCustomAttributeCached<DeserializeAttribute>(false);
                if (attr == null || !attr.Json) continue; // 仅处理 json=true 的子组件
                var val = prop.GetValue(gear);
                if (val == null) continue;
                result[prop.Name] = ToPlain(val);
            }

            // 液体容量换算为实际升(defaultSerialized 中为实际升,添加时工具自动 ×1e9)
            if (result.TryGetValue("LiquidItem", out var li) && li is Dictionary<string, object> liDict && liDict.TryGetValue("m_LiquidLitersProxy", out var llp) && llp is float llpf)
                liDict["m_LiquidLitersProxy"] = ItemWeights.ToLiters(llpf);
            if (result.TryGetValue("WaterSupply", out var ws) && ws is Dictionary<string, object> wsDict && wsDict.TryGetValue("m_VolumeProxy", out var vp) && vp is float vpf)
                wsDict["m_VolumeProxy"] = ItemWeights.ToLiters(vpf);

            // 重量换算为实际 kg;粉末物品优先取 PowderItem.m_WeightKG(游戏实际读取值)
            float realWeightKg = (gear.PowderItem != null && gear.PowderItem.m_WeightKG > 0)
                ? ItemWeights.ToKg(gear.PowderItem.m_WeightKG)
                : ItemWeights.ToKg(gear.m_WeightKG);
            result["m_WeightKG"] = realWeightKg;
            return result;
        }

        /// <summary>
        /// 把对象递归转换为纯数据(字典/列表/基本类型),EnumWrapper → {"Value": ...},与游戏序列化格式一致。
        /// </summary>
        private static object ToPlain(object o)
        {
            if (o == null) return null;
            var t = o.GetType();

            if (o is EnumWrapper ew)
                return new Dictionary<string, object> { ["Value"] = ew.Value };
            if (o is string || t.IsPrimitive || o is decimal || o is DateTime)
                return o;

            if (t.IsArray)
            {
                var list = new List<object>();
                foreach (var item in (Array)o) list.Add(ToPlain(item));
                return list;
            }
            if (o is System.Collections.IEnumerable col)
            {
                var list = new List<object>();
                foreach (var item in col) list.Add(ToPlain(item));
                return list;
            }

            var dict = new Dictionary<string, object>();
            foreach (var prop in t.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                if (prop.GetCustomAttributeCached<JsonIgnoreAttribute>(false) != null) continue;
                if (prop.GetIndexParameters().Length > 0) continue;
                dict[prop.Name] = ToPlain(prop.GetValue(o));
            }
            foreach (var field in t.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                if (field.GetCustomAttributeCached<JsonIgnoreAttribute>(false) != null) continue;
                dict[field.Name] = ToPlain(field.GetValue(o));
            }
            return dict;
        }
    }
}
