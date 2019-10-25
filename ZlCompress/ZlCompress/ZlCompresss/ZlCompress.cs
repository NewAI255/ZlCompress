using System;
using System.Collections.Generic;
using System.Linq;

namespace ZlCompress.ZlCompresss
{
    /// <summary>
    /// ZL压缩
    /// </summary>
    public static class ZlCompressHelper
    {
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="compBytes">压缩的Bytes</param>
        /// <returns></returns>
        public static byte[] Decompression(byte[] compBytes)
        {
            var ver = compBytes[0];//得到版本号
            //得到字典长度
            var dicCount = compBytes[1];
            //得到字典内容
            var dicBytes = new byte[dicCount];
            Array.Copy(compBytes, 2, dicBytes, 0, dicCount);
            //得到总元素长度
            var length = BitConverter.ToInt32(compBytes, 1 + 1 + dicCount);

            var decompressionBytes = new byte[length];//解压数组

            var index = 1 + 1 + dicCount + 4; //索引存入数据索引位置
            var bitIndex = 7;//Bit索引位置

            //循环解压
            for (int i = 0; i < length; )
            {
                //读取1Bit判断是否压缩
                if (ReadBit(compBytes, ref index, ref bitIndex))
                {
                    Array.Copy(dicBytes, 0, decompressionBytes, i, dicCount);
                    i += dicCount;
                }
                else
                {
                    decompressionBytes[i] = ReadByte(compBytes, ref index, bitIndex);
                    i++;
                }
            }
            return decompressionBytes;
        }

        /// <summary>
        /// 读取一个字节
        /// </summary>
        /// <param name="compBytes">解压字节数组</param>
        /// <param name="index">索引位置</param>
        /// <param name="bitIndex">Bit位置</param>
        /// <returns>字节值</returns>
        private static byte ReadByte(byte[] compBytes, ref int index, int bitIndex)
        {
            if (bitIndex == 7)
                return compBytes[index++];
            var left = 7 - bitIndex;
            var right = 8 - left;
            var b = (byte)(compBytes[index] << left);
            index++;
            b |= (byte)(compBytes[index] >> right);
            return b;
        }

        /// <summary>
        /// 读取一个Bit位
        /// </summary>
        /// <param name="compBytes">解压字节数组</param>
        /// <param name="index">索引位置</param>
        /// <param name="bitIndex">Bit位置</param>
        /// <returns>Bit值</returns>
        private static bool ReadBit(byte[] compBytes, ref int index, ref int bitIndex)
        {
            var left = 7 - bitIndex;
            var bit = ((((byte)(compBytes[index] << left))) >> (bitIndex + left)) == 1;
            bitIndex--;
            if (bitIndex < 0)
            {
                index++;
                bitIndex = 7;
            }

            return bit;
        }

        /// <summary>
        /// 压缩方法
        /// </summary>
        /// <param name="source">压缩数据</param>
        /// <param name="item">进行压缩的组合</param>
        /// <returns>压缩后的结果</returns>
        public static byte[] Compress(byte[] source, CompressItem item)
        {
            var headerLength = 1 //版本号
                               + 1 //字典长度字段
                               + item.Group.Bytes.Length //字典内容字段
                               + 4; //总元素长度【有效位】

            var group = item.Group;
            var compressByteCount = source.Length - (item.Count * group.Bytes.Length);
            var sumBit = (compressByteCount * 9) + (item.Count);
            var bytes = new byte[headerLength + (((sumBit - 1) / 8) + 1)];

            bytes[0] = 0;//版本号为0
            bytes[1] = (byte)group.Bytes.Length; //字典长度
            Array.Copy(group.Bytes, 0, bytes, 2, group.Bytes.Length); //字典内容字段
            Array.Copy(BitConverter.GetBytes(source.Length), 0,
                bytes, group.Bytes.Length + 1 + 1, 4); //总元素长度【有效位】
            //循环压缩内容
            var index = headerLength;//索引存入数据索引位置
            var bitIndex = 0;//Bit索引位置

            var groupSize = group.Bytes.Length;
            for (int i = 0; i < source.Length; )
            {
                //判断是否可以压缩组合
                if (group.EqArray(source, i))
                {
                    AddBit(bytes, ref index, ref bitIndex, true);
                    i += groupSize;
                }
                else
                {
                    AddBit(bytes, ref index, ref bitIndex, false);
                    AddByte(bytes, ref index, bitIndex, source[i]);
                    i++;
                }
            }

            if (bitIndex != 0)
                bytes[index] <<= (8 - bitIndex); //将最后一位位置挪到最高位，方便解压
            return bytes;
        }

        /// <summary>
        /// 添加一个byte字节
        /// </summary>
        /// <param name="bytes">添加进入的byte</param>
        /// <param name="index">数据索引位置</param>
        /// <param name="bitIndex">bit位置</param>
        /// <param name="b">添加进入的byte</param>
        private static void AddByte(byte[] bytes, ref int index, int bitIndex, byte b)
        {
            //如果满BIT位就直接赋值
            if (bitIndex == 0)
            {
                bytes[index] = b;
                index++;
                return;
            }
            var start = 8 - bitIndex;
            var end = bitIndex;
            var oldV = bytes[index] << start;
            bytes[index] = (byte)(oldV | (b >> end)); //赋值头元素
            index++;
            bytes[index] = (byte)((byte)(b << start) >> start); //赋值尾元素
        }

        /// <summary>
        /// 添加一个Bit位
        /// </summary>
        /// <param name="bytes">元素值</param>
        /// <param name="index">byte位置</param>
        /// <param name="bitIndex">bit位置</param>
        /// <param name="bit">添加的bit位</param>
        private static void AddBit(byte[] bytes, ref int index, ref int bitIndex, bool bit)
        {
            var b = (bytes[index] << 1);
            if (bit) b |= 1;
            bytes[index] = (byte)b;
            bitIndex = (bitIndex + 1) % 8;
            if (bitIndex == 0) index++;
        }

        /// <summary>
        /// 获取指定组合数量中最有价值的组合
        /// </summary>
        /// <param name="bytes">查询数据</param>
        /// <param name="groupSize">组合大小</param>
        /// <returns>最有价值的组合</returns>
        public static CompressItem GetCompressItem(byte[] bytes, int groupSize)
        {
            var endIndex = bytes.Length - groupSize; //得到结束位索引
            var compressItemDic = new Dictionary<CompressItem, int>(); //判断是否存在
            for (int i = 0; i <= endIndex; i++)
            {
                var group = ByteGroup.Create(bytes, i, groupSize); //得到Group
                var item = new CompressItem
                {
                    Group = group,
                    Bytes = bytes,
                    StartIndex = i
                };
                if (compressItemDic.ContainsKey(item))
                {
                    compressItemDic[item]++;
                }
                else
                {
                    compressItemDic[item] = 1;
                }
            }

            var items = compressItemDic.OrderByDescending(b => b.Value).ThenByDescending(b => b.Key.StartIndex)
                .ToArray(); //得到理论上最多组合数排序的结果
            var maxItem = items.First().Key;//得到第一个理论上最多组合结果
            maxItem.Count = GetCompressGroup(bytes, maxItem.Group, maxItem.StartIndex);//得到理论上第一个的实际可用组合数
            for (var i = 1; i < items.Length; i++)
            {
                var item = items[i];
                if (item.Value <= maxItem.Count) break;//如果理论组合数还小于等于目前最多的实际组合数就没有必要继续搜索下去了
                item.Key.Count = GetCompressGroup(bytes, item.Key.Group, item.Key.StartIndex);//得到实际组合数量
                //判断是否实际组合数量大于现在最多的
                if (item.Key.Count > maxItem.Count)
                {
                    maxItem = item.Key;//赋值目前为最多实际组合结果
                }
            }

            return maxItem;
        }

        /// <summary>
        /// 验证可压缩组合个数
        /// </summary>
        /// <param name="bytes">查询数据</param>
        /// <param name="group">组合对象</param>
        /// <returns>组合数量</returns>
        private static int GetCompressGroup(byte[] bytes, ByteGroup group, int start)
        {
            var groupSize = group.Bytes.Length;
            var endIndex = bytes.Length - groupSize; //得到结束位索引
            var count = 0;
            for (var i = start; i <= endIndex; )
            {
                if (group.EqArray(bytes, i))
                {
                    count++;
                    i += groupSize;
                }
                else
                {
                    i++;
                }
            }

            return count;
        }

        /// <summary>
        /// 得到解压后大小
        /// </summary>
        /// <param name="compBytes">内容</param>
        /// <returns>解压后大小</returns>
        public static int GetDecompressionSize(byte[] compBytes)
        {
            //得到字典长度
            var dicCount = compBytes[1];
            //得到总元素长度
            return BitConverter.ToInt32(compBytes, 1 + 1 + dicCount);
        }
    }
}
