using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZlCompress.ZlCompresss;

namespace ZlCompress
{
    /// <summary>
    /// 解压逻辑控制对象
    /// </summary>
    public class DecompressionLogicControl : BaseLogicControl
    {
        #region 内容部分控件

        private Label labOriginalSize = new Label();
        private Label labDecompressuin = new Label();
        private Label labCompressionRatio = new Label();
        private Label label6 = new Label();
        private TextBox txtLog = new TextBox();

        private void InitContentControl()
        {
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(18, 95);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(247, 97);
            this.txtLog.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "日志";
            // 
            // labDecompressuin
            // 
            this.labDecompressuin.AutoSize = true;
            this.labDecompressuin.Location = new System.Drawing.Point(150, 31);
            this.labDecompressuin.Name = "labDecompressuin";
            this.labDecompressuin.Size = new System.Drawing.Size(91, 15);
            this.labDecompressuin.TabIndex = 1;
            this.labDecompressuin.Text = "解压后：";
            // 
            // labCompressionRatio
            // 
            this.labCompressionRatio.AutoSize = true;
            this.labCompressionRatio.Location = new System.Drawing.Point(150, 68);
            this.labCompressionRatio.Name = "labCompressionRatio";
            this.labCompressionRatio.Size = new System.Drawing.Size(115, 15);
            this.labCompressionRatio.TabIndex = 0;
            this.labCompressionRatio.Text = "压缩率：0.00%";
            // 
            // labOriginalSize
            // 
            this.labOriginalSize.AutoSize = true;
            this.labOriginalSize.Location = new System.Drawing.Point(15, 31);
            this.labOriginalSize.Name = "labOriginalSize";
            this.labOriginalSize.Size = new System.Drawing.Size(91, 15);
            this.labOriginalSize.TabIndex = 0;
            this.labOriginalSize.Text = "解压前：";

            ControlInfo.ContentBox.Controls.AddRange(new Control[]
            {
                labOriginalSize,
                labDecompressuin,
                labCompressionRatio,
                label6,
                txtLog
            });
        }

        /// <summary>
        /// 内容控件显示效果
        /// </summary>
        /// <param name="visible"></param>
        private void ContentControlVisible(bool visible)
        {
            labOriginalSize.Visible = visible;
            labDecompressuin.Visible = visible;
            labCompressionRatio.Visible = visible;
            label6.Visible = visible;
            txtLog.Visible = visible;
        }

        #endregion


        #region 操作部分控件

        private System.Windows.Forms.Button butDecompressuin = new Button();

        private void InitOperationControl()
        {
            // 
            // butDecompressuin
            // 
            this.butDecompressuin.Location = new System.Drawing.Point(9, 157);
            this.butDecompressuin.Name = "butDecompressuin";
            this.butDecompressuin.Size = new System.Drawing.Size(94, 34);
            this.butDecompressuin.TabIndex = 3;
            this.butDecompressuin.Text = "解压";
            this.butDecompressuin.UseVisualStyleBackColor = true;

            ControlInfo.OperationBox.Controls.AddRange(new Control[]
            {
                butDecompressuin,
            });
        }

        /// <summary>
        /// 操作控件显示效果
        /// </summary>
        /// <param name="visible"></param>
        private void OperationControlVisible(bool visible)
        {
            butDecompressuin.Visible = visible;
        }

        #endregion

        /// <summary>
        /// 解压前控件文本
        /// </summary>
        public string OriginalSizeText
        {
            get { return labOriginalSize.Invoke(new Func<string>(() => labOriginalSize.Text)) as string; }
            set { labOriginalSize.Invoke(new Action(() => labOriginalSize.Text = value)); }
        }


        /// <summary>
        /// 解压后控件文本
        /// </summary>
        public string CompressSizeText
        {
            get { return labDecompressuin.Invoke(new Func<string>(() => labDecompressuin.Text)) as string; }
            set { labDecompressuin.Invoke(new Action(() => labDecompressuin.Text = value)); }
        }

        /// <summary>
        /// 压缩率控件文本
        /// </summary>
        public string CompressionRatioText
        {
            get { return labCompressionRatio.Invoke(new Func<string>(() => labCompressionRatio.Text)) as string; }
            set { labCompressionRatio.Invoke(new Action(() => labCompressionRatio.Text = value)); }
        }

        public override string Name
        {
            get { return "解压"; }
        }

        /// <summary>
        /// 解压数据
        /// </summary>
        private FileInfoDto FileInfo { get; set; }

        public DecompressionLogicControl(FromControlInfo controlInfo) : base(controlInfo)
        {
            InitContentControl();
            InitOperationControl();
            ContentControlVisible(false);
            OperationControlVisible(false);
            InitEvent();
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvent()
        {
            //解压数据
            butDecompressuin.Click += (s, e) => Task.Factory.StartNew(Decompressuin);
        }

        /// <summary>
        /// 重新设置内容
        /// </summary>
        private void ResetContent(IgnoreDecompressionResetContentType ignore = IgnoreDecompressionResetContentType.None)
        {
            if (!ignore.HasFlag(IgnoreDecompressionResetContentType.OriginalSizeText))
                OriginalSizeText = "解压前：";
            if (!ignore.HasFlag(IgnoreDecompressionResetContentType.CompressSizeText))
                CompressSizeText = "解压后：";
            if (!ignore.HasFlag(IgnoreDecompressionResetContentType.CompressionRatioText))
                CompressionRatioText = "压缩率：";
            if (!ignore.HasFlag(IgnoreDecompressionResetContentType.Schedule))
                ControlInfo.SetScheduleAction(0); //设置进度为0
            if (!ignore.HasFlag(IgnoreDecompressionResetContentType.Log))
                txtLog.Invoke(new Action(() => { txtLog.Clear(); }));
        }

        /// <summary>
        /// 解压数据
        /// </summary>
        private void Decompressuin()
        {
            if (FileInfo == null) return;
            LockControl(true);
            ControlInfo.SetSavePathAction(string.Empty);//清除下保存文本
            ConsoleWriteLine("开始解压");
            ResetContent(IgnoreDecompressionResetContentType.OriginalSizeText |
                         IgnoreDecompressionResetContentType.CompressSizeText |
                         IgnoreDecompressionResetContentType.CompressionRatioText);//重设置文本
            ControlInfo.SetScheduleAction(0);//设置解压开始
            var decompression = ZlCompressHelper.Decompression(FileInfo.FileContent);//解压
            ControlInfo.SetScheduleAction(100);//设置解压完成
            ConsoleWriteLine("解压完成");
            var path = ControlInfo.GetSavePathFunc(new SaveFileInfo
            {
                FileName = string.Format("{0}", Path.GetFileNameWithoutExtension(FileInfo.Info.FullName)),
                Title = "请设置保存的解压文件",
            });
            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllBytes(path, decompression);
                MessageBox.Show("保存成功！");
            }
            LockControl(false);
        }

        public override bool VerifyData(FileInfoDto info)
        {
            return info.Info.Extension.ToLower().Equals(".zl");
        }

        /// <summary>
        /// 锁定控件
        /// </summary>
        private void LockControl(bool lockControl)
        {
            butDecompressuin.Invoke(new Action(() =>
            {
                butDecompressuin.Enabled = !lockControl;
            }));
        }

        public override void Show(FileInfoDto info)
        {
            FileInfo = info;
            ResetContent();//重设置文本
            ControlInfo.SetSavePathAction(string.Empty);//清除下保存文本
            ContentControlVisible(true); //显示内容控件
            OperationControlVisible(true);//显示操作控件
            var length = FileInfo.Info.Length;
            OriginalSizeText = string.Format("解压前：{0}", Tool.ToDataString(length));
            var decompressionSize = ZlCompressHelper.GetDecompressionSize(FileInfo.FileContent);//得到解压后大小
            CompressSizeText = string.Format("解压后：{0}", Tool.ToDataString(decompressionSize));
            CompressionRatioText = string.Format("压缩率：{0:0.00}%", (((double)length / decompressionSize) * 100));
        }

        public override void Hide()
        {
            ContentControlVisible(false); //隐藏内容控件
            OperationControlVisible(false);//隐藏操作控件
        }
        /// <summary>
        /// 向控制台输出一行文字
        /// </summary>
        /// <param name="text">写入的数据</param>
        private void ConsoleWriteLine(string text)
        {
            ConsoleWrite(string.Format("{0}\r\n", text));
        }

        /// <summary>
        /// 向控制台输出数据
        /// </summary>
        /// <param name="text">写入的数据</param>
        private void ConsoleWrite(string text)
        {
            txtLog.Invoke(new Action(() =>
            {
                txtLog.AppendText(text);
                txtLog.SelectionStart = txtLog.TextLength - 1;
            }));
        }
    }

    [Flags]
    public enum IgnoreDecompressionResetContentType
    {
        None = 0,
        OriginalSizeText = 1,
        CompressSizeText = 2,
        CompressionRatioText = 4,
        Schedule = 8,
        Log = 16,
    }
}