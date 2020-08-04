namespace TheLongDarkBuckupTools.Helpers
{
    /*
    *C#LibLZF端口的改进版本：
    *版权所有（c）2010 Roman Atachiants<kelindar@gmail.com>
    *
    *原始CLZF端口：
    *版权所有（c）2005 Oren J.Maurice<oymaurice@hazorea.org.il>
    *
    *原始LibLZF库算法：
    *版权所有（c）2000-2008 Marc Alexander Lehmann<schmorp@schmorp.de>
    *
    *以源代码和二进制形式重新分发和使用，有无修改-
    *如果满足以下条件，则允许：
    *
    *1。源代码的再分配必须保留上述版权声明，
    *此条件列表和以下免责声明。
    *
    *2。二进制形式的再分配必须复制上述版权
    *注意，此条件列表和以下免责声明
    *分发时提供的文件和/或其他材料。
    *
    *3。作者的姓名不得用于代言或推销产品
    *未经事先书面许可而从本软件中派生。
    *
    *本软件由作者“按原样”提供，任何明示或暗示
    *保证，包括但不限于MER的默示保证-
    *不承认对特定目的的吟诵和适合性。在没有
    *作者应为任何间接事件负责-
    *社会、惩戒性或后果性损害（包括但不限于，
    *采购替代货物或服务；丧失使用、数据或利润；
    *或营业中断），无论其原因如何，根据任何责任理论，
    *无论是合同、严格责任还是侵权行为（包括过失或其他）-
    *ERWISE）以任何方式因使用本软件而产生的，即使得到通知
    *这种损害的可能性。
    *
    *或者，本文件的内容可根据
    *GNU通用公共许可证版本2（“GPL”），在这种情况下
    *适用GPL的规定，而不是上述规定。如果你愿意的话
    *仅允许在
    *不允许其他人在
    *BSD许可证，通过删除上述条款和
    *将其替换为GPL要求的通知和其他规定。如果
    *您不删除上述规定，收件人可以使用您的版本
    *在BSD或GPL下。
    */
    using System;

    /*使用Alice29坎特伯雷语料库进行基准测试
        ---------------------------------------
        （压缩）原始CLZF C#
        原始=152089，压缩=101092
        82924743毫秒。
        ---------------------------------------
        （压缩）My LZF C#
        原始=152089，压缩=101092
        330019毫秒。
        ---------------------------------------
        （压缩）使用SharpZipLib的Zlib
        原始=152089，压缩=54388
        83894799毫秒。
        ---------------------------------------
        （压缩）QuickLZ C#
        原始=152089，压缩=83494
        800046毫秒。
        ---------------------------------------
        （解压缩）原始CLZF C#
        解压缩=152089
        160009毫秒。
        ---------------------------------------
        （解压）My LZF C#
        解压缩=152089
        150009毫秒。
        ---------------------------------------
        （解压缩）使用SharpZipLib的Zlib
        解压缩=152089
        35772046毫秒。
        ---------------------------------------
        （解压缩）QuickLZ C#
        解压缩=152089
        210012毫秒。
		*/


    /// <summary>
    /// 改进的C#LZF压缩器，一个非常小的数据压缩库。压缩算法非常快。
    /// </summary>
    public static class CLZF
    {
        private static readonly uint HLOG = 14;
        private static readonly uint HSIZE = (1 << 14);
        private static readonly uint MAX_LIT = (1 << 5);
        private static readonly uint MAX_OFF = (1 << 13);
        private static readonly uint MAX_REF = ((1 << 8) + (1 << 3));

        /// <summary>
        /// 哈希表，只能分配一次
        /// </summary>
        private static readonly long[] HashTable = new long[HSIZE];

        /// <summary>
        /// 压缩输入字节
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] inputBytes)
        {
            // 开始猜测，如果需要，以后再增加
            int outputByteCountGuess = inputBytes.Length * 2;
            byte[] tempBuffer = new byte[outputByteCountGuess];
            int byteCount = lzf_compress(inputBytes, ref tempBuffer);

            // 如果字节数为0，则增加缓冲区并重试
            while (byteCount == 0)
            {
                outputByteCountGuess *= 2;
                tempBuffer = new byte[outputByteCountGuess];
                byteCount = lzf_compress(inputBytes, ref tempBuffer);
            }

            byte[] outputBytes = new byte[byteCount];
            Buffer.BlockCopy(tempBuffer, 0, outputBytes, 0, byteCount);
            return outputBytes;
        }

        /// <summary>
        /// 解压缩输出字节
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] inputBytes)
        {
            // 开始猜测，如果需要，以后再增加
            int outputByteCountGuess = inputBytes.Length * 2;
            byte[] tempBuffer = new byte[outputByteCountGuess];
            int byteCount = lzf_decompress(inputBytes, ref tempBuffer);

            // 如果字节数为0，则增加缓冲区并重试
            while (byteCount == 0)
            {
                outputByteCountGuess *= 2;
                tempBuffer = new byte[outputByteCountGuess];
                byteCount = lzf_decompress(inputBytes, ref tempBuffer);
            }

            byte[] outputBytes = new byte[byteCount];
            Buffer.BlockCopy(tempBuffer, 0, outputBytes, 0, byteCount);
            return outputBytes;
        }

        /// <summary>
        /// 使用LibLZF算法压缩数据
        /// </summary>
        /// <param name="input">对要压缩的数据的引用</param>
        /// <param name="output">对包含压缩数据的缓冲区的引用</param>
        /// <returns>输出缓冲区中压缩档案的大小</returns>
        public static int lzf_compress(byte[] input, ref byte[] output)
        {
            int inputLength = input.Length;
            int outputLength = output.Length;

            Array.Clear(HashTable, 0, (int)HSIZE);

            long hslot;
            uint iidx = 0;
            uint oidx = 0;
            long reference;

            uint hval = (uint)(((input[iidx]) << 8) | input[iidx + 1]); // FRST(in_data, iidx);
            long off;
            int lit = 0;

            for (; ; )
            {
                if (iidx < inputLength - 2)
                {
                    hval = (hval << 8) | input[iidx + 2];
                    hslot = ((hval ^ (hval << 5)) >> (int)(((3 * 8 - HLOG)) - hval * 5) & (HSIZE - 1));
                    reference = HashTable[hslot];
                    HashTable[hslot] = (long)iidx;


                    if ((off = iidx - reference - 1) < MAX_OFF
                        && iidx + 4 < inputLength
                        && reference > 0
                        && input[reference + 0] == input[iidx + 0]
                        && input[reference + 1] == input[iidx + 1]
                        && input[reference + 2] == input[iidx + 2]
                        )
                    {
                        /* 在*参考处找到匹配++ */
                        uint len = 2;
                        uint maxlen = (uint)inputLength - iidx - len;
                        maxlen = maxlen > MAX_REF ? MAX_REF : maxlen;

                        if (oidx + lit + 1 + 3 >= outputLength)
                            return 0;

                        do
                            len++;
                        while (len < maxlen && input[reference + len] == input[iidx + len]);

                        if (lit != 0)
                        {
                            output[oidx++] = (byte)(lit - 1);
                            lit = -lit;
                            do
                                output[oidx++] = input[iidx + lit];
                            while ((++lit) != 0);
                        }

                        len -= 2;
                        iidx++;

                        if (len < 7)
                        {
                            output[oidx++] = (byte)((off >> 8) + (len << 5));
                        }
                        else
                        {
                            output[oidx++] = (byte)((off >> 8) + (7 << 5));
                            output[oidx++] = (byte)(len - 7);
                        }

                        output[oidx++] = (byte)off;

                        iidx += len - 1;
                        hval = (uint)(((input[iidx]) << 8) | input[iidx + 1]);

                        hval = (hval << 8) | input[iidx + 2];
                        HashTable[((hval ^ (hval << 5)) >> (int)(((3 * 8 - HLOG)) - hval * 5) & (HSIZE - 1))] = iidx;
                        iidx++;

                        hval = (hval << 8) | input[iidx + 2];
                        HashTable[((hval ^ (hval << 5)) >> (int)(((3 * 8 - HLOG)) - hval * 5) & (HSIZE - 1))] = iidx;
                        iidx++;
                        continue;
                    }
                }
                else if (iidx == inputLength)
                    break;

                /* 我们必须多拷贝一个字节 */
                lit++;
                iidx++;

                if (lit == MAX_LIT)
                {
                    if (oidx + 1 + MAX_LIT >= outputLength)
                        return 0;

                    output[oidx++] = (byte)(MAX_LIT - 1);
                    lit = -lit;
                    do
                        output[oidx++] = input[iidx + lit];
                    while ((++lit) != 0);
                }
            }

            if (lit != 0)
            {
                if (oidx + lit + 1 >= outputLength)
                    return 0;

                output[oidx++] = (byte)(lit - 1);
                lit = -lit;
                do
                    output[oidx++] = input[iidx + lit];
                while ((++lit) != 0);
            }

            return (int)oidx;
        }


        /// <summary>
        /// 使用LibLZF算法解压缩数据
        /// </summary>
        /// <param name="input">引用要解压缩的数据</param>
        /// <param name="output">对将包含解压缩数据的缓冲区的引用</param>
        /// <returns>返回解压缩后的大小</returns>
        public static int lzf_decompress(byte[] input, ref byte[] output)
        {
            int inputLength = input.Length;
            int outputLength = output.Length;

            uint iidx = 0;
            uint oidx = 0;

            do
            {
                uint ctrl = input[iidx++];

                if (ctrl < (1 << 5)) /* 文字运行 */
                {
                    ctrl++;

                    if (oidx + ctrl > outputLength)
                    {
                        //SET_ERRNO (E2BIG);
                        return 0;
                    }

                    do
                        output[oidx++] = input[iidx++];
                    while ((--ctrl) != 0);
                }
                else /* 反向参考 */
                {
                    uint len = ctrl >> 5;

                    int reference = (int)(oidx - ((ctrl & 0x1f) << 8) - 1);

                    if (len == 7)
                        len += input[iidx++];

                    reference -= input[iidx++];

                    if (oidx + len + 2 > outputLength)
                    {
                        //SET_ERRNO (E2BIG);
                        return 0;
                    }

                    if (reference < 0)
                    {
                        //SET_ERRNO (EINVAL);
                        return 0;
                    }

                    output[oidx++] = output[reference++];
                    output[oidx++] = output[reference++];

                    do
                        output[oidx++] = output[reference++];
                    while ((--len) != 0);
                }
            }
            while (iidx < inputLength);

            return (int)oidx;
        }

    }
}
