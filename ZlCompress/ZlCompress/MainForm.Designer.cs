namespace ZlCompress
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.butSelectSourcePath = new System.Windows.Forms.Button();
            this.butSelectSavePath = new System.Windows.Forms.Button();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbSchedule = new System.Windows.Forms.Label();
            this.pbSchedule = new System.Windows.Forms.ProgressBar();
            this.gbOperation = new System.Windows.Forms.GroupBox();
            this.gbContent = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "原文件";
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.AllowDrop = true;
            this.txtSourcePath.Location = new System.Drawing.Point(76, 27);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.ReadOnly = true;
            this.txtSourcePath.Size = new System.Drawing.Size(269, 25);
            this.txtSourcePath.TabIndex = 1;
            // 
            // butSelectSourcePath
            // 
            this.butSelectSourcePath.AllowDrop = true;
            this.butSelectSourcePath.Location = new System.Drawing.Point(353, 27);
            this.butSelectSourcePath.Name = "butSelectSourcePath";
            this.butSelectSourcePath.Size = new System.Drawing.Size(42, 23);
            this.butSelectSourcePath.TabIndex = 2;
            this.butSelectSourcePath.Text = "....";
            this.butSelectSourcePath.UseVisualStyleBackColor = true;
            // 
            // butSelectSavePath
            // 
            this.butSelectSavePath.Location = new System.Drawing.Point(353, 59);
            this.butSelectSavePath.Name = "butSelectSavePath";
            this.butSelectSavePath.Size = new System.Drawing.Size(42, 23);
            this.butSelectSavePath.TabIndex = 5;
            this.butSelectSavePath.Text = "....";
            this.butSelectSavePath.UseVisualStyleBackColor = true;
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(76, 59);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.ReadOnly = true;
            this.txtSavePath.Size = new System.Drawing.Size(269, 25);
            this.txtSavePath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "保存文件";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.butSelectSavePath);
            this.groupBox1.Controls.Add(this.txtSourcePath);
            this.groupBox1.Controls.Add(this.txtSavePath);
            this.groupBox1.Controls.Add(this.butSelectSourcePath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "路径";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbSchedule);
            this.groupBox2.Controls.Add(this.pbSchedule);
            this.groupBox2.Location = new System.Drawing.Point(13, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 66);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "进度";
            // 
            // lbSchedule
            // 
            this.lbSchedule.AutoSize = true;
            this.lbSchedule.Location = new System.Drawing.Point(335, 29);
            this.lbSchedule.Name = "lbSchedule";
            this.lbSchedule.Size = new System.Drawing.Size(39, 15);
            this.lbSchedule.TabIndex = 1;
            this.lbSchedule.Text = "0.0%";
            // 
            // pbSchedule
            // 
            this.pbSchedule.Location = new System.Drawing.Point(18, 24);
            this.pbSchedule.Maximum = 10000;
            this.pbSchedule.Name = "pbSchedule";
            this.pbSchedule.Size = new System.Drawing.Size(311, 23);
            this.pbSchedule.TabIndex = 0;
            // 
            // gbOperation
            // 
            this.gbOperation.Location = new System.Drawing.Point(300, 200);
            this.gbOperation.Name = "gbOperation";
            this.gbOperation.Size = new System.Drawing.Size(114, 198);
            this.gbOperation.TabIndex = 8;
            this.gbOperation.TabStop = false;
            this.gbOperation.Text = "操作";
            // 
            // gbContent
            // 
            this.gbContent.Location = new System.Drawing.Point(13, 200);
            this.gbContent.Name = "gbContent";
            this.gbContent.Size = new System.Drawing.Size(281, 198);
            this.gbContent.TabIndex = 9;
            this.gbContent.TabStop = false;
            this.gbContent.Text = "内容";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(426, 410);
            this.Controls.Add(this.gbContent);
            this.Controls.Add(this.gbOperation);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZL压缩软件";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSourcePath;
        private System.Windows.Forms.Button butSelectSourcePath;
        private System.Windows.Forms.Button butSelectSavePath;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbSchedule;
        private System.Windows.Forms.ProgressBar pbSchedule;
        private System.Windows.Forms.GroupBox gbOperation;
        private System.Windows.Forms.GroupBox gbContent;
    }
}

