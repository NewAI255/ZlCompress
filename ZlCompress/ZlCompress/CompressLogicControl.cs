using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZlCompress.ZlCompresss;

namespace ZlCompress
{
    /// <summary>
    /// 压缩控制对象
    /// </summary>
    public class CompressLogicControl : BaseLogicControl
    {
        #region 内容部分控件

        private Label labOriginalSize = new Label();
        private Label labCompressSize = new Label();
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
            // labCompressSize
            // 
            this.labCompressSize.AutoSize = true;
            this.labCompressSize.Location = new System.Drawing.Point(150, 31);
            this.labCompressSize.Name = "labCompressSize";
            this.labCompressSize.Size = new System.Drawing.Size(91, 15);
            this.labCompressSize.TabIndex = 1;
            this.labCompressSize.Text = "压缩后：";
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
            this.labOriginalSize.Text = "压缩前：";

            ControlInfo.ContentBox.Controls.AddRange(new Control[]
            {
                labOriginalSize,
                labCompressSize,
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
            labCompressSize.Visible = visible;
            labCompressionRatio.Visible = visible;
            label6.Visible = visible;
            txtLog.Visible = visible;
        }

        #endregion

        #region 操作部分控件

        private System.Windows.Forms.Button butCompress = new Button();
        private System.Windows.Forms.Button butAnalyze = new Button();
        private System.Windows.Forms.Label label4 = new Label();
        private System.Windows.Forms.ComboBox cbFindCompressGroupQty = new ComboBox();
        private System.Windows.Forms.ComboBox cbCompressThreadQty = new ComboBox();
        private System.Windows.Forms.Label label3 = new Label();

        private void InitOperationControl()
        {
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "压缩线程数量";
            // 
            // cbCompressThreadQty
            // 
            this.cbCompressThreadQty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompressThreadQty.FormattingEnabled = true;
            for (int i = 1; i <= Environment.ProcessorCount; i++)
            {
                this.cbCompressThreadQty.Items.Add(i);
            }

            this.cbCompressThreadQty.SelectedItem = Math.Max(Environment.ProcessorCount / 2, 1); //默认使用核心数的一半
            this.cbCompressThreadQty.Location = new System.Drawing.Point(9, 41);
            this.cbCompressThreadQty.Name = "cbCompressThreadQty";
            this.cbCompressThreadQty.Size = new System.Drawing.Size(94, 23);
            this.cbCompressThreadQty.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "搜索组合数量";
            // 
            // cbFindCompressGroupQty
            // 
            this.cbFindCompressGroupQty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFindCompressGroupQty.FormattingEnabled = true;
            for (int i = 1; i < 256; i++)
            {
                this.cbFindCompressGroupQty.Items.Add(i);
            }

            cbFindCompressGroupQty.SelectedItem = 8;//默认选择搜索8个组合
            this.cbFindCompressGroupQty.Location = new System.Drawing.Point(9, 89);
            this.cbFindCompressGroupQty.Name = "cbFindCompressGroupQty";
            this.cbFindCompressGroupQty.Size = new System.Drawing.Size(94, 23);
            this.cbFindCompressGroupQty.TabIndex = 1;
            // 
            // butAnalyze
            // 
            this.butAnalyze.Location = new System.Drawing.Point(9, 118);
            this.butAnalyze.Name = "butAnalyze";
            this.butAnalyze.Size = new System.Drawing.Size(94, 34);
            this.butAnalyze.TabIndex = 3;
            this.butAnalyze.Text = "分析数据";
            this.butAnalyze.UseVisualStyleBackColor = true;
            // 
            // butCompress
            // 
            this.butCompress.Location = new System.Drawing.Point(9, 157);
            this.butCompress.Name = "butCompress";
            this.butCompress.Size = new System.Drawing.Size(94, 34);
            this.butCompress.TabIndex = 3;
            this.butCompress.Text = "压缩";
            this.butCompress.UseVisualStyleBackColor = true;

            ControlInfo.OperationBox.Controls.AddRange(new Control[]
            {
                butCompress,
                butAnalyze,
                label4,
                cbFindCompressGroupQty,
                cbCompressThreadQty,
                label3
            });
        }

        /// <summary>
        /// 操作控件显示效果
        /// </summary>
        /// <param name="visible"></param>
        private void OperationControlVisible(bool visible)
        {
            butCompress.Visible = visible;
            butAnalyze.Visible = visible;
            label4.Visible = visible;
            cbFindCompressGroupQty.Visible = visible;
            cbCompressThreadQty.Visible = visible;
            label3.Visible = visible;
        }

        #endregion

        public override string Name
        {
            get { return "压缩"; }
        }

        public CompressLogicControl(FromControlInfo controlInfo) 
            : base(controlInfo)
        {
            InitContentControl();
            InitOperationControl();
            ContentControlVisible(false);
            OperationControlVisible(false);
            InitEvent();
        }

        /// <summary>
        /// 设置的搜索组合数量
        /// </summary>
        public int CompressGroupQty
        {
            get
            {
                return (int) (
                    cbFindCompressGroupQty.Invoke(new Func<object>(() => cbFindCompressGroupQty.SelectedItem)) ?? 0);
            }
        }

        /// <summary>
        /// 设置的压缩线程数量
        /// </summary>
        public int CompressThreadQty
        {
            get
            {
                return (int)(
                    cbCompressThreadQty.Invoke(new Func<object>(() => cbCompressThreadQty.SelectedItem)) ?? 0);
            }
        }

        /// <summary>
        /// 压缩前控件文本
        /// </summary>
        public string OriginalSizeText
        {
            get { return labOriginalSize.Invoke(new Func<string>(() => labOriginalSize.Text)) as string; }
            set { labOriginalSize.Invoke(new Action(() => labOriginalSize.Text = value)); }
        }

        /// <summary>
        /// 压缩后控件文本
        /// </summary>
        public string CompressSizeText
        {
            get { return labCompressSize.Invoke(new Func<string>(() => labCompressSize.Text)) as string; }
            set { labCompressSize.Invoke(new Action(() => labCompressSize.Text = value)); }
        }

        /// <summary>
        /// 压缩率控件文本
        /// </summary>
        public string CompressionRatioText
        {
            get { return labCompressionRatio.Invoke(new Func<string>(() => labCompressionRatio.Text)) as string; }
            set { labCompressionRatio.Invoke(new Action(() => labCompressionRatio.Text = value)); }
        }

        /// <summary>
        /// 压缩数据
        /// </summary>
        private FileInfoDto FileInfo { get; set; }

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvent()
        {
            //分析压缩数据
            butAnalyze.Click += (s, e) => Task.Factory.StartNew(AnalyzeData);
            //压缩数据
            butCompress.Click += (s, e) => Task.Factory.StartNew(Compress);
        }

        /// <summary>
        /// 压缩方法
        /// </summary>
        private void Compress()
        {
            if (FileInfo == null) return;
            LockControl(true);//锁定控件
            ControlInfo.SetSavePathAction(string.Empty);//清除下保存文本
            ResetContent(IgnoreCompressResetContentType.OriginalSizeText);//重设置文本
            var countList = new int[CompressGroupQty];
            for (int i = 1; i <= countList.Length; i++)
            {
                countList[i - 1] = i;
            }

            double sumCount = countList.Length + 1;//总数
            var count = 0;//执行到数
            var fileContent = FileInfo.FileContent;
            var task = new TaskScheduling<int, CompressItem>(countList, CompressThreadQty)
            {
                TaskFunc = size =>
                {
                    var item = ZlCompressHelper.GetCompressItem(fileContent, size);
                    ControlInfo.SetScheduleAction(Interlocked.Increment(ref count) / sumCount * 100); //设置进度
                    return item;
                },
                ExceptionFunc = (size, ex) =>
                {
                    ConsoleWriteLine(string.Format("分析压缩数据出现错误！{0}", ex.Message));//输出错误
                    return null;
                }
            };
            var max = task.Execute().Where(w => w != null).OrderByDescending(b => b.CompressSize)
                .FirstOrDefault(); //得到压缩价值最大的项
            if (max == null)
            {
                ConsoleWriteLine("没有查询到一个组合的压缩数据！全部出现异常！"); //输出错误
            }
            else if (max.CompressSize < 1)
            {
                ConsoleWriteLine("没有有价值的压缩组合！请调大组合大小！"); //输出错误
            }
            else
            {
                ConsoleWriteLine(string.Format("采用{0}字节组合[{1}]", max.Group.Bytes.Length,
                    string.Join(",", max.Group.Bytes)));
                OriginalSizeText = string.Format("压缩前：{0}", Tool.ToDataString(fileContent.Length));
                CompressSizeText = string.Format("压缩后：{0}", Tool.ToDataString(fileContent.Length - max.CompressSize));
                CompressionRatioText = string.Format("压缩率：{0:0.00}%",
                    (((double)(fileContent.Length - max.CompressSize) / fileContent.Length) * 100));
                var compressBytes = ZlCompressHelper.Compress(fileContent, max); //压缩处理
                ControlInfo.SetScheduleAction(100);//设置执行完成
                var path = ControlInfo.GetSavePathFunc(new SaveFileInfo
                {
                    DefaultExt = ".zl",
                    FileName = string.Format("{0}.zl", FileInfo.Info.Name),
                    Title = "请设置保存的压缩文件",
                });
                if (!string.IsNullOrEmpty(path))
                {
                    File.WriteAllBytes(path, compressBytes);
                    MessageBox.Show("保存成功！");
                }
            }

            ControlInfo.SetScheduleAction(100);//设置执行完成
            LockControl(false);//解锁控件
        }

        /// <summary>
        /// 分析压缩数据
        /// </summary>
        public void AnalyzeData()
        {
            if (FileInfo == null) return;
            LockControl(true);//锁定控件
            ResetContent(IgnoreCompressResetContentType.OriginalSizeText);//重设置文本
            var countList = new int[CompressGroupQty];
            for (int i = 1; i <= countList.Length; i++)
            {
                countList[i - 1] = i;
            }

            var count = 0;
            var fileContent = FileInfo.FileContent;
            var task = new TaskScheduling<int, CompressItem>(countList, CompressThreadQty)
            {
                TaskFunc = size =>
                {
                    var item = ZlCompressHelper.GetCompressItem(fileContent, size);
                    ControlInfo.SetScheduleAction(Interlocked.Increment(ref count) / (double) countList.Length *
                                                  100); //设置进度
                    return item;
                },
                ExceptionFunc = (size, ex) =>
                {
                    ConsoleWriteLine(string.Format("分析压缩数据出现错误！{0}", ex.Message));//输出错误
                    return null;
                }
            };
            var max = task.Execute().Where(w => w != null).OrderByDescending(b => b.CompressSize)
                .FirstOrDefault(); //得到压缩价值最大的项
            if (max == null)
            {
                ConsoleWriteLine("没有查询到一个组合的压缩数据！全部出现异常！"); //输出错误
            }
            else if (max.CompressSize < 1)
            {
                ConsoleWriteLine("没有有价值的压缩组合！请调大组合大小！"); //输出错误
            }
            else
            {
                ConsoleWriteLine(string.Format("采用{0}字节组合[{1}]，组合出现次数{2}次", max.Group.Bytes.Length,
                    string.Join(",", max.Group.Bytes), max.Count));
                OriginalSizeText = string.Format("压缩前：{0}", Tool.ToDataString(fileContent.Length));
                CompressSizeText = string.Format("压缩后：{0}", Tool.ToDataString(fileContent.Length - max.CompressSize));
                CompressionRatioText = string.Format("压缩率：{0:0.00}%",
                    (((double) (fileContent.Length - max.CompressSize) / fileContent.Length) * 100));
            }
            LockControl(false);//解锁控件
        }

        /// <summary>
        /// 锁定控件
        /// </summary>
        private void LockControl(bool lockControl)
        {
            butCompress.Invoke(new Action(() =>
            {
                butCompress.Enabled = !lockControl;
                butAnalyze.Enabled = !lockControl;
                cbFindCompressGroupQty.Enabled = !lockControl;
                cbCompressThreadQty.Enabled = !lockControl;
            }));
        }

        /// <summary>
        /// 重新设置内容
        /// </summary>
        private void ResetContent(IgnoreCompressResetContentType ignore = IgnoreCompressResetContentType.None)
        {
            if (!ignore.HasFlag(IgnoreCompressResetContentType.OriginalSizeText))
                OriginalSizeText = "压缩前：";
            if (!ignore.HasFlag(IgnoreCompressResetContentType.CompressSizeText))
                CompressSizeText = "压缩后：";
            if (!ignore.HasFlag(IgnoreCompressResetContentType.CompressionRatioText))
                CompressionRatioText = "压缩率：";
            if (!ignore.HasFlag(IgnoreCompressResetContentType.Schedule))
                ControlInfo.SetScheduleAction(0); //设置进度为0
            if (!ignore.HasFlag(IgnoreCompressResetContentType.Log))
                txtLog.Invoke(new Action(() => { txtLog.Clear(); }));
        }

        /// <summary>
        /// 校验是否处理
        /// </summary>
        public override bool VerifyData(FileInfoDto info)
        {
            return !info.Info.Extension.ToLower().Equals(".zl");
        }

        /// <summary>
        /// 显示页面
        /// </summary>
        /// <param name="info"></param>
        public override void Show(FileInfoDto info)
        {
            FileInfo = info;
            ControlInfo.SetSavePathAction(string.Empty);//清除下保存文本
            ResetContent();//重设置文本
            ContentControlVisible(true); //显示内容控件
            OperationControlVisible(true);//显示操作控件
            OriginalSizeText = string.Format("压缩前：{0}", Tool.ToDataString(FileInfo.Info.Length));
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
    public enum IgnoreCompressResetContentType
    {
        None = 0,
        OriginalSizeText = 1,
        CompressSizeText = 2,
        CompressionRatioText = 4,
        Schedule = 8,
        Log = 16,
    }
}