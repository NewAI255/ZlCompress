using System.Linq;

namespace ZlCompress
{
    public static class Tool
    {
        /// <summary>
        /// 数据单位
        /// </summary>
        private static readonly string[] DataUnit = new string[]
        {
            "B", "KB", "MB", "GB", "TB"
        };
        /// <summary>
        /// 转换数据大小
        /// </summary>
        /// <param name="size">数据大小</param>
        /// <returns></returns>
        public static string ToDataString(long size)
        {
            var unitSize = 1L;
            foreach (var unit in DataUnit)
            {
                if (unitSize * 1024 >= size)
                {
                    return string.Format("{0:0.0} {1}", size / (double)unitSize, unit);
                }
                unitSize *= 1024;
            }
            return string.Format("{0:0.0} {1}", size / (double)unitSize, DataUnit.Last());
        }
    }
}
