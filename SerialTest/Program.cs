using System;
using TheLongDarkBuckupTools.Helpers;

namespace SerialTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MaxLiters = " + ItemWeights.MaxLiters.ToString("R"));
            Console.WriteLine("MaxLiters * 1e9 = " + (ItemWeights.MaxLiters * ItemWeights.StorageMultiplier).ToString("R"));
            Console.WriteLine("int.MaxValue = " + int.MaxValue);
            Console.WriteLine("存储值是否在 int 范围内: " + (ItemWeights.MaxLiters * ItemWeights.StorageMultiplier <= int.MaxValue));

            float[] test = { 0.5f, 1f, 2f, 2.147f, 2.1474836f, 2.15f, 3f, 5f, 20f };
            foreach (float L in test)
            {
                float storage = ItemWeights.ToStorageLiters(L);
                bool overflow = storage > int.MaxValue;
                Console.WriteLine($"{L}L -> 存储 {storage.ToString("R")} {(overflow ? "[!!溢出!!]" : "[OK]")} (游戏内 {(overflow ? "显示-214748.36升" : "正常")})");
            }

            Console.WriteLine();
            Console.WriteLine("结论: 输入 <= 2.147 升安全; 超过则游戏内 int 溢出");
            bool ok = ItemWeights.MaxLiters * ItemWeights.StorageMultiplier <= int.MaxValue
                   && ItemWeights.MaxLiters > 2.147f;
            Console.WriteLine(ok ? "=== 上限校验正确 ===" : "=== 上限校验错误 ===");
        }
    }
}
