namespace ZlCompress
{
    /// <summary>
    /// 逻辑控制对象
    /// </summary>
    public abstract class BaseLogicControl
    {
        /// <summary>
        /// 控制信息
        /// </summary>
        protected FromControlInfo ControlInfo { get; private set; }
        protected BaseLogicControl(FromControlInfo controlInfo)
        {
            ControlInfo = controlInfo;
        }
        /// <summary>
        /// 逻辑控制名称
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 校验是否使用当前对象进行操作
        /// </summary>
        /// <param name="info">用户操作</param>
        /// <returns>是否支持处理</returns>
        public abstract bool VerifyData(FileInfoDto info);

        /// <summary>
        /// 显示内容和操作界面
        /// </summary>
        /// <param name="info">操作数据</param>
        public abstract void Show(FileInfoDto info);

        /// <summary>
        /// 隐藏操作数据
        /// </summary>
        public abstract void Hide();
    }
}