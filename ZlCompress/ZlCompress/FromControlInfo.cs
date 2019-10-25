using System;
using System.Windows.Forms;

namespace ZlCompress
{
    /// <summary>
    /// 界面控制对象
    /// </summary>
    public class FromControlInfo
    {
        /// <summary>
        /// 内容Box
        /// </summary>
        public GroupBox ContentBox { get; set; }

        /// <summary>
        /// 操作Box
        /// </summary>
        public GroupBox OperationBox { get; set; }

        /// <summary>
        /// 得到保存的路径
        /// </summary>
        public Func<SaveFileInfo,string> GetSavePathFunc { get; set; }

        /// <summary>
        /// 设置保存的路径
        /// </summary>
        public Action<string> SetSavePathAction { get; set; }

        /// <summary>
        /// 显示进度
        /// </summary>
        public Action<double> SetScheduleAction { get; set; }
    }
}