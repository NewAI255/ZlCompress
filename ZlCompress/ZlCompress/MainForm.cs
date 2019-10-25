using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ZlCompress
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 界面控制对象
        /// </summary>
        private readonly FromControlInfo ControlInfo;

        /// <summary>
        /// 逻辑操作对象集合
        /// </summary>
        private readonly BaseLogicControl[] _logicControls;

        /// <summary>
        /// 使用的控制对象
        /// </summary>
        private BaseLogicControl UseLogicControl { get; set; }

        public MainForm()
        {
            InitializeComponent();
            ControlInfo = CreateFromControlInfo(); //得到界面控制对象
            _logicControls = new BaseLogicControl[]
            {
                new CompressLogicControl(ControlInfo), //压缩逻辑控制对象
                new DecompressionLogicControl(ControlInfo), //解压逻辑控制对象
            };
            InitEvent();
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvent()
        {
            //选择原文件
            butSelectSourcePath.Click += (s, e) =>
            {
                var path = SelectFile("请选择ZL压缩文件或者需要压缩的文件！");
                if (!string.IsNullOrEmpty(path))
                {
                    txtSourcePath.Text = path;
                    SelectSourceFile();//选择了文件触发的事件
                }
            };
            //选择保存的文件
            butSelectSavePath.Click += (s, e) =>
            {
                var path = GetSavePath(new SaveFileInfo
                {
                    Title = "请选择保存的文件位置和名称！"
                });
                if (!string.IsNullOrEmpty(path))
                    txtSavePath.Text = path;
            };

            //拖拽文件事件
            label1.DragEnter += SourceDragEnter;
            label1.DragDrop += SourceDragDrop;
            txtSourcePath.DragEnter += SourceDragEnter;
            txtSourcePath.DragDrop += SourceDragDrop;
            butSelectSourcePath.DragEnter += SourceDragEnter;
            butSelectSourcePath.DragDrop += SourceDragDrop;
        }

        /// <summary>将元素拖到显示文件夹显示表框中触发的事件
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceDragEnter(object sender, DragEventArgs e)
        {
            //更改鼠标将元素拖入更改鼠标形状
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                ((Control) sender).Cursor = System.Windows.Forms.Cursors.Arrow;  //指定鼠标形状（更好看）  
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>将元素脱放到显示文件信息表控件上并松开触发的事件
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceDragDrop(object sender, DragEventArgs e)
        {
            //GetValue(0) 为第1个文件全路径  
            //DataFormats 数据的格式，下有多个静态属性都为string型，除FileDrop格式外还有Bitmap,Text,WaveAudio等格式  
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (!string.IsNullOrEmpty(path))
            {
                txtSourcePath.Text = path;
                SelectSourceFile();//选择了文件触发的事件
            }
            //将鼠标在此控件中还原为默认形状
            ((Control)sender).Cursor = System.Windows.Forms.Cursors.AppStarting; //还原鼠标形状  
        }

        /// <summary>
        /// 选择了原文件
        /// </summary>
        private void SelectSourceFile()
        {
            var path = txtSourcePath.Text;
            var info = new FileInfoDto(new FileInfo(path));
            var logic = _logicControls.FirstOrDefault(f => f.VerifyData(info));
            if (logic == null) return;
            if (UseLogicControl != null)
                UseLogicControl.Hide(); //隐藏
            logic.Show(info);//显示界面内容
            UseLogicControl = logic;//显示控制对象
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="title">显示的标题</param>
        private string SelectFile(string title)
        {
            var form = new OpenFileDialog {Title = title};
            if (form.ShowDialog() != DialogResult.OK) return null;
            return form.FileName;
        }

        /// <summary>
        /// 创建界面控制对象
        /// </summary>
        private FromControlInfo CreateFromControlInfo()
        {
            return new FromControlInfo
            {
                ContentBox = gbContent,
                OperationBox = gbOperation,
                GetSavePathFunc = GetSavePath,
                SetScheduleAction = SetSchedule,
                SetSavePathAction = SetSavePath
            };
        }

        /// <summary>
        /// 设置保存路径
        /// </summary>
        /// <param name="path"></param>
        private void SetSavePath(string path)
        {
            this.Invoke(new Action(() =>
            {
                txtSavePath.Text = path;
            }));
        }

        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="schedule">进度</param>
        private void SetSchedule(double schedule)
        {
            if (schedule > 100) schedule = 100;
            else if (schedule < 0) schedule = 0;
            this.Invoke(new Action(() =>
            {
                lbSchedule.Text = string.Format("{0:0.00}%", schedule);
                pbSchedule.Value = (int) (schedule * 100); //扩大100倍精度
            }));
        }

        /// <summary>
        /// 得到保存的路径
        /// </summary>
        private string GetSavePath(SaveFileInfo info)
        {
            return this.Invoke(new Func<string>(() =>
            {
                if (string.IsNullOrEmpty(txtSavePath.Text))
                {
                    var savePathForm = new SaveFileDialog
                    {
                        Title = info.Title,
                        AddExtension = true,
                        DefaultExt = info.DefaultExt,
                        FileName = info.FileName
                    };
                    if (savePathForm.ShowDialog() != DialogResult.OK)
                    {
                        return null;
                    }

                    txtSavePath.Text = savePathForm.FileName;
                }

                return txtSavePath.Text;
            })) as string;
        }
    }
}