using System;
using System.IO;
using System.Linq;
using System.Text;
using TheLongDarkBuckupTools.Helpers;
using TheLongDarkBuckupTools.MoreData;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Serialization;
using Newtonsoft.Json;

namespace SerialTest
{
    class Program
    {
        // 与 DynamicSerializable.jsonSerializerSettings 一致的设置(含 IntegralFloatConverter 修复)
        static JsonSerializerSettings ToolSettings()
        {
            return new JsonSerializerSettings()
            {
                FloatFormatHandling = FloatFormatHandling.Symbol,
                Converters = { new IntegralFloatConverter() }
            };
        }
        static void Main(string[] args)
        {
            string[] saves = new[] {
                @"C:\Users\29133\AppData\Local\Hinterland\TheLongDark\autosave3",
            };
            if (args.Length > 0 && System.IO.File.Exists(args[0])) saves = new[] { args[0] };

            var method = typeof(TheLongDarkBuckupTools.Helpers.CLZF).Assembly
                .GetType("TheLongDarkBuckupTools.Helpers.EncryptString")
                .GetMethod("DecompressBytesToString", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

            foreach (var savePath in saves)
            {
                Console.WriteLine("========================================================");
                Console.WriteLine("SAVE: " + savePath);
                try
                {
                    var gs = new GameSave();
                    gs.LoadSave(savePath);
                    string globalJson = (string)method.Invoke(null, new object[] { gs.SlotData.m_Dict.global });
                    Console.WriteLine("global JSON length: " + globalJson.Length);
                    Console.WriteLine();

                    // 1) 打印所有液体条目全文(含负值/异常值上下文)
                    Console.WriteLine("--- 所有液体条目(含负值/异常值上下文) ---");
                    DumpLiquidItemsFull(globalJson);

                    // 2) 模拟工具修改: 找到第一个过氧化氢,序列化看格式
                    Console.WriteLine();
                    Console.WriteLine("--- 模拟工具修改测试 ---");
                    TestToolWrite(gs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LOAD ERROR: " + ex);
                }
                Console.WriteLine();
            }
        }

        static void DumpLiquidItemsFull(string json)
        {
        // 打印每个物品的完整 Gear 序列化块(含 m_PrefabName + m_WeightKG + 液体对象), 标记负值
        string gearStart = "m_SerializedGear\":\"";
        int pos = 0; int n = 0;
        while (n < 60)
        {
            int gidx = json.IndexOf(gearStart, pos, StringComparison.Ordinal);
            if (gidx < 0) break;
            // 找这个物品的 m_PrefabName
            int pidx = json.LastIndexOf("\"m_PrefabName\"", gidx, StringComparison.Ordinal);
            string prefab = "?";
            if (pidx >= 0 && gidx - pidx < 3000)
            {
                int v1 = json.IndexOf('"', pidx + 15);
                int v2 = json.IndexOf('"', v1 + 1);
                if (v1 > 0 && v2 > v1) prefab = json.Substring(v1 + 1, v2 - v1 - 1);
            }
            // 提取完整 Gear JSON 字符串值(转义后的对象,直到未转义引号)
            int s = gidx + gearStart.Length;
            int e = s;
            while (e < json.Length)
            {
                if (json[e] == '\\') { e += 2; continue; }
                if (json[e] == '"') break;
                e++;
            }
            string gearJson = json.Substring(s, e - s);
            bool hasNeg = gearJson.Contains("-500000000") || gearJson.Contains("m_LiquidLitersProxy\":-5");
            bool hasLiquid = gearJson.Contains("LiquidItem");
            if (hasLiquid)
            {
                // 提取液体对象
                int li = gearJson.IndexOf("m_LiquidItemSerialized");
                string liquid = li >= 0 ? gearJson.Substring(li, Math.Min(120, gearJson.Length - li)) : "";
                Console.WriteLine($"#{n} [{prefab}] neg={hasNeg}  {liquid}");
                if (hasNeg)
                    Console.WriteLine($"      FULL: {gearJson.Substring(0, Math.Min(400, gearJson.Length))}");
            }
            pos = e + 1;
            n++;
        }
        Console.WriteLine("(液体条目数: " + n + ")");
    }

    // 模拟工具写入: 找到液体物品,序列化看 JSON 格式
    static void TestToolWrite(GameSave gs)
        {
            try
            {
                InventoryItemSaveData src = null;
                foreach (var item in gs.Global.Inventory.Items)
                {
                    if (item.Gear != null && item.Gear.LiquidItem != null)
                    {
                        src = item;
                        break;
                    }
                }
                if (src == null) { Console.WriteLine("  (没有液体物品)"); return; }

                Console.WriteLine("  源物品: " + src.m_PrefabName + "  m_LiquidLitersProxy(原始float) = " + src.Gear.LiquidItem.m_LiquidLitersProxy);
                Console.WriteLine("  原始序列化片段(Symbol 格式):");
                string origJson = JsonConvert.SerializeObject(src.Gear.LiquidItem, ToolSettings());
                Console.WriteLine("    " + origJson);

                // 模拟 CloneItem(深拷贝)
                var cloned = gs.CloneItem(src);
                Console.WriteLine("  CloneItem 后 m_LiquidLitersProxy = " + cloned.Gear.LiquidItem.m_LiquidLitersProxy + " (类型 " + cloned.Gear.LiquidItem.m_LiquidLitersProxy.GetType().Name + ")");
                Console.WriteLine("  CloneItem 后序列化:");
                string cloneJson = JsonConvert.SerializeObject(cloned.Gear.LiquidItem, ToolSettings());
                Console.WriteLine("    " + cloneJson);

                // 模拟 setLiters(0.5)
                cloned.Gear.LiquidItem.m_LiquidLitersProxy = ItemWeights.ToStorageLiters(0.5f);
                Console.WriteLine("  setLiters(0.5) 后 m_LiquidLitersProxy = " + cloned.Gear.LiquidItem.m_LiquidLitersProxy + " (" + cloned.Gear.LiquidItem.m_LiquidLitersProxy.GetType().Name + ")");
                Console.WriteLine("  setLiters(0.5) 后序列化(模拟工具保存):");
                string litJson = JsonConvert.SerializeObject(cloned.Gear.LiquidItem, ToolSettings());
                Console.WriteLine("    " + litJson);

                // 模拟 DynamicSerializable 的完整序列化链路(同样用 Symbol 设置)
                Console.WriteLine("  JsonConvert.SerializeObject(src.Gear) (Symbol) 结果:");
                string reJson = JsonConvert.SerializeObject(src.Gear, ToolSettings());
                Console.WriteLine("    " + reJson.Substring(0, Math.Min(reJson.Length, 300)));

                // 大值测试
                foreach (var liters in new float[] { 2.5f, 5f, 40f })
                {
                    float v = ItemWeights.ToStorageLiters(liters);
                    Console.WriteLine($"  ToStorageLiters({liters}) = {v} ({v.GetType().Name})  -> JSON: " +
                        JsonConvert.SerializeObject(v, ToolSettings()));
                }

                // float 精度测试
                float f = 98154920f;
                Console.WriteLine("  float 98154920 -> ToLiters = " + ItemWeights.ToLiters(f));
                Console.WriteLine("  float 40000000000f = " + 40000000000f + " -> JSON: " +
                    JsonConvert.SerializeObject(40000000000f, ToolSettings()));
                Console.WriteLine("  float 500000000f = " + 500000000f + " -> JSON: " +
                    JsonConvert.SerializeObject(500000000f, ToolSettings()));

                // ===== 关键测试: double 类型序列化格式 =====
                Console.WriteLine();
                Console.WriteLine("  === double 类型序列化测试(与游戏整数格式对比) ===");
                double d500 = 500000000.0;
                double d98154920 = 98154920.0;
                double d40L = 40000000000.0;
                double d2_5L = 2500000000.0;
                Console.WriteLine("  double 500000000.0   -> JSON: " +
                    JsonConvert.SerializeObject(d500, ToolSettings()));
                Console.WriteLine("  double 98154920.0    -> JSON: " +
                    JsonConvert.SerializeObject(d98154920, ToolSettings()));
                Console.WriteLine("  double 40000000000.0 -> JSON: " +
                    JsonConvert.SerializeObject(d40L, ToolSettings()));
                Console.WriteLine("  double 2500000000.0  -> JSON: " +
                    JsonConvert.SerializeObject(d2_5L, ToolSettings()));
                Console.WriteLine("  游戏存档格式(对照): 500000000 / 98154920 / 40000000000 (整数, 无小数点无E)");

                // double 精度测试: 游戏消耗后值
                Console.WriteLine();
                Console.WriteLine("  === double 精度验证 ===");
                Console.WriteLine("  double 98154920 / 1e9 = " + (98154920.0 / 1000000000.0) + " L");
                Console.WriteLine("  double 40000000000 / 1e9 = " + (40000000000.0 / 1000000000.0) + " L");
                Console.WriteLine("  float 98154920f / 1e9f = " + (98154920f / 1000000000f) + " L (float精度丢失?)");
                Console.WriteLine("  float 40000000000f / 1e9f = " + (40000000000f / 1000000000f) + " L (float精度丢失?)");

                // ===== 关键测试: 修复前后对比(IntegralFloatConverter) =====
                Console.WriteLine();
                Console.WriteLine("  === IntegralFloatConverter 修复验证 ===");
                var oldSettings = new JsonSerializerSettings() { FloatFormatHandling = FloatFormatHandling.Symbol };
                var newSettings = ToolSettings();
                float[] cases = new float[] { 500000000f, 98154920f, 2500000000f, 40000000000f, 0.5f, 0.09815492f, 1f, -500000000f, 0f };
                foreach (var fc in cases)
                {
                    Console.WriteLine("    " + fc + " (float)");
                    Console.WriteLine("      修复前: " + JsonConvert.SerializeObject(fc, oldSettings));
                    Console.WriteLine("      修复后: " + JsonConvert.SerializeObject(fc, newSettings));
                }
                double[] dcases = new double[] { 500000000.0, 40000000000.0, 0.5, 0.09815492, -500000000.0 };
                foreach (var d in dcases)
                {
                    Console.WriteLine("    " + d + " (double)");
                    Console.WriteLine("      修复前: " + JsonConvert.SerializeObject(d, oldSettings));
                    Console.WriteLine("      修复后: " + JsonConvert.SerializeObject(d, newSettings));
                }
                // 特殊值
                Console.WriteLine("    NaN (float):  修复后: " + JsonConvert.SerializeObject(float.NaN, newSettings));
                Console.WriteLine("    +Inf (float): 修复后: " + JsonConvert.SerializeObject(float.PositiveInfinity, newSettings));
                Console.WriteLine("    -Inf (float): 修复后: " + JsonConvert.SerializeObject(float.NegativeInfinity, newSettings));
                Console.WriteLine("    NaN (double): 修复后: " + JsonConvert.SerializeObject(double.NaN, newSettings));

                // ===== 完整物品序列化对比(走 DynamicSerializable 真实链路) =====
                Console.WriteLine();
                Console.WriteLine("  === DynamicSerializable 完整链路验证 ===");
                try
                {
                    var dyn = new TheLongDarkBuckupTools.Serialization.DynamicSerializable<GearItemSaveDataProxy>(
                        JsonConvert.SerializeObject(src.Gear, new JsonSerializerSettings() { FloatFormatHandling = FloatFormatHandling.Symbol }));
                    string serialized = dyn.Serialize();
                    // 提取液体字段看格式
                    int li = serialized.IndexOf("m_LiquidLitersProxy");
                    Console.WriteLine("    Serialize() 液体片段: " + (li >= 0 ? serialized.Substring(li, Math.Min(60, serialized.Length - li)) : "(无液体)"));
                    int wi = serialized.IndexOf("m_WeightKG");
                    Console.WriteLine("    Serialize() 重量片段: " + (wi >= 0 ? serialized.Substring(wi, Math.Min(60, serialized.Length - wi)) : "(无重量)"));
                    Console.WriteLine("    期望: m_LiquidLitersProxy:500000000 (整数token)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("    DYNAMIC TEST ERROR: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("  TEST ERROR: " + ex);
            }
        }
    }
}
