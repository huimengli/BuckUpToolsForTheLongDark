using System;
using System.IO;
using System.Linq;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Helpers;
using TheLongDarkBuckupTools.MoreData;
using TheLongDarkBuckupTools.Serialization;

namespace SerialTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string slotPath = @"C:\Users\29133\AppData\Local\Hinterland\TheLongDark\autosave3";
            var gs = new GameSave();
            gs.LoadSave(slotPath);
            var inv = gs.Global.Inventory;
            Console.WriteLine("加载成功,物品数: " + inv.Items.Count);

            // 选一个带重量/堆叠/食物的物品克隆
            var src = inv.Items.FirstOrDefault(i => i.Gear != null && i.Gear.m_WeightKG > 0 && i.Gear.StackableItem != null);
            if (src == null) src = inv.Items.FirstOrDefault(i => i.Gear != null && i.Gear.m_WeightKG > 0);
            Console.WriteLine("选中克隆源: " + src.m_PrefabName + " 重量=" + src.Gear.m_WeightKG + " InstanceID=" + src.Gear.m_InstanceIDProxy);

            int before = inv.Items.Count;
            var clone = gs.CloneItem(src);
            inv.Items.Add(clone);
            Console.WriteLine("克隆完成,物品数: " + inv.Items.Count);
            Console.WriteLine("克隆: " + clone.m_PrefabName + " 重量=" + clone.Gear.m_WeightKG + " InstanceID=" + clone.Gear.m_InstanceIDProxy);
            bool idOk = clone.Gear.m_InstanceIDProxy != src.Gear.m_InstanceIDProxy;
            bool refOk = !object.ReferenceEquals(clone.Gear, src.Gear);
            Console.WriteLine("ID不同: " + idOk + "  对象独立: " + refOk);

            // 修改克隆物品的堆叠/重量,确认不影响原物品
            if (clone.Gear.StackableItem != null)
            {
                clone.Gear.StackableItem.m_UnitsProxy = 999;
                Console.WriteLine("改克隆堆叠后,原物品堆叠=" + (src.Gear.StackableItem != null ? src.Gear.StackableItem.m_UnitsProxy.ToString() : "null") + " 克隆堆叠=" + clone.Gear.StackableItem.m_UnitsProxy);
            }
            clone.Gear.m_WeightKG = 123456789f;
            Console.WriteLine("改克隆重量后,原物品重量=" + src.Gear.m_WeightKG + " 克隆重量=" + clone.Gear.m_WeightKG);

            // 保存后再重载验证
            string tmp = Path.Combine(Path.GetTempPath(), "autosave_clone_test");
            Directory.CreateDirectory(tmp);
            string tmpSlot = Path.Combine(tmp, "sandbox_clone_test");
            File.Copy(slotPath, tmpSlot, true);
            gs.path = tmpSlot;
            gs.Save();
            Console.WriteLine("保存成功");

            var gs2 = new GameSave();
            gs2.LoadSave(tmpSlot);
            var inv2 = gs2.Global.Inventory;
            Console.WriteLine("重载成功,物品数: " + inv2.Items.Count + " (应为 " + (before + 1) + ")");
            var loaded = inv2.Items.FirstOrDefault(i => i.Gear != null && i.Gear.m_WeightKG == 123456789f);
            Console.WriteLine("找到克隆物品(重量123456789): " + (loaded != null ? loaded.m_PrefabName + " InstanceID=" + loaded.Gear.m_InstanceIDProxy : "未找到!"));

            // 清理
            try { File.Delete(tmpSlot); Directory.Delete(tmp); } catch { }
            Console.WriteLine(before + 1 == inv2.Items.Count && loaded != null ? "=== 克隆端到端测试通过 ===" : "=== 测试失败 ===");
        }
    }
}
