namespace ZlCompress.ZlCompresss
{
    public class CompressItem
    {
        public byte[] Bytes { get; set; }

        public int CompressSize
        {
            get
            {
                var headerLength = 1 //版本号
                                   + 1 //字典长度字段
                                   + Group.Bytes.Length //字典内容字段
                                   + 4; //总元素长度【有效位】
                var compressByteCount = Bytes.Length - (Count * Group.Bytes.Length);
                var sumBit = (compressByteCount * 9) + (Count);
                return Bytes.Length - (headerLength + (((sumBit - 1) / 8) + 1));
            }
        }

        public ByteGroup Group { get; set; }
        /// <summary>
        /// 组合实际出现次数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 压缩数据中第一个出现组合的索引位置
        /// </summary>
        public int StartIndex { get; set; }

        public override int GetHashCode()
        {
            return Group.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var group = obj as CompressItem;
            if (group == null) return false;
            return Group.Equals(group.Group);
        }
    }
}
