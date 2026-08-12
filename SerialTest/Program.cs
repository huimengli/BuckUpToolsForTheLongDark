using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;
using TheLongDarkBuckupTools.GameData;
using TheLongDarkBuckupTools.Serialization;

namespace SerialTest
{
    class Program
    {
        static string DecompressBytesToString(byte[] data)
        {
            var t = typeof(SlotData).Assembly.GetType("TheLongDarkBuckupTools.Helpers.EncryptString");
            var m = t.GetMethod("DecompressBytesToString", BindingFlags.Static | BindingFlags.Public);
            return (string)m.Invoke(null, new object[] { data });
        }

        static void Main(string[] args)
        {
            string slotPath = @"C:\Users\29133\AppData\Local\Hinterland\TheLongDark\autosave3";
            string slotJson = DecompressBytesToString(File.ReadAllBytes(slotPath));
            var slot = JObject.Parse(slotJson);
            string globalB64 = (string)slot["m_Dict"]["global"];
            string globalJson = DecompressBytesToString(Convert.FromBase64String(globalB64));

            Console.WriteLine("=== 端到端:加载真实global → 序列化 → 再加载 ===");
            var dyn1 = new DynamicSerializable<GlobalSaveGameFormat>(globalJson);
            Console.WriteLine("第1次加载 OK");
            string out1 = dyn1.Serialize();
            Console.WriteLine("序列化 OK, 长度 " + out1.Length);

            try
            {
                var dyn2 = new DynamicSerializable<GlobalSaveGameFormat>(out1);
                Console.WriteLine("第2次加载 OK!");
                Console.WriteLine("Condition HP = " + dyn2.Obj.Condition?.m_CurrentHPProxy);
                Console.WriteLine("Encumber = " + (dyn2.Obj.Encumber == null ? "null" : dyn2.Obj.Encumber.m_NumSecondsSinceLastVoiceOver.ToString()));
                Console.WriteLine("WellFed = " + (dyn2.Obj.WellFed == null ? "null" : dyn2.Obj.WellFed.m_ElapsedHoursNotStarving.ToString()));
                Console.WriteLine("成功!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("第2次加载失败: " + ex);
                return;
            }

            // 再测:第2次结果再序列化再加载(连续保存)
            Console.WriteLine();
            Console.WriteLine("=== 再序列化一次(连续保存模拟) ===");
            string out2 = dyn1.Serialize();
            try
            {
                var dyn3 = new DynamicSerializable<GlobalSaveGameFormat>(out2);
                Console.WriteLine("第3次加载 OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("第3次加载失败: " + ex);
            }
        }
    }
}
