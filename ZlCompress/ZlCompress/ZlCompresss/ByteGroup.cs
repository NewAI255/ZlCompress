using System;

namespace ZlCompress.ZlCompresss
{

    public class ByteGroup
    {
        public ByteGroup(byte[] bytes)
        {
            Bytes = bytes;
        }

        public override int GetHashCode()
        {
            var valBytes = new byte[4];
            for (int i = 0; i < Bytes.Length; i++)
            {
                valBytes[i % 4] ^= Bytes[i];
            }

            return BitConverter.ToInt32(valBytes, 0);
        }

        public byte[] Bytes { get; private set; }

        public override bool Equals(object obj)
        {
            var group = obj as ByteGroup;
            if (group == null) return false;
            return EqArray(group.Bytes, 0);
        }

        public bool EqArray(byte[] bytes, int start)
        {
            if (bytes.Length - start < Bytes.Length) return false;//剩余长度不足就返回false
            for (int i = start, index = 0; i < bytes.Length && index < Bytes.Length; i++, index++)
            {
                if (Bytes[index] != bytes[i]) return false;
            }

            return true;
        }

        public static ByteGroup Create(byte[] bytes, int index, int groupSize)
        {
            var byteList = new byte[groupSize];
            Array.Copy(bytes, index, byteList, 0, groupSize);
            return new ByteGroup(byteList);
        }

        public override string ToString()
        {
            return string.Join(",", Bytes);
        }
    }
}
