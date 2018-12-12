namespace Test
{
    partial class ModuleControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleControl));
            this.labText = new System.Windows.Forms.Label();
            this.labIndex = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.imgStatus = new System.Windows.Forms.ImageList(this.components);
            this.labIcon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labText
            // 
            this.labText.AllowDrop = true;
            this.labText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(64, 12);
            this.labText.Margin = new System.Windows.Forms.Padding(0);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(74, 21);
            this.labText.TabIndex = 2;
            this.labText.Text = "单元模块";
            this.labText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labText.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragDrop);
            this.labText.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragEnter);
            this.labText.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseDoubleClick);
            this.labText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseMove);
            // 
            // labIndex
            // 
            this.labIndex.AllowDrop = true;
            this.labIndex.Location = new System.Drawing.Point(0, 0);
            this.labIndex.Margin = new System.Windows.Forms.Padding(0);
            this.labIndex.Name = "labIndex";
            this.labIndex.Size = new System.Drawing.Size(32, 48);
            this.labIndex.TabIndex = 1;
            this.labIndex.Text = "0";
            this.labIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labIndex.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragDrop);
            this.labIndex.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragEnter);
            this.labIndex.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseDoubleClick);
            this.labIndex.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseMove);
            // 
            // labStatus
            // 
            this.labStatus.AllowDrop = true;
            this.labStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.labStatus.ImageList = this.imgStatus;
            this.labStatus.Location = new System.Drawing.Point(224, 0);
            this.labStatus.Margin = new System.Windows.Forms.Padding(0);
            this.labStatus.MaximumSize = new System.Drawing.Size(0, 48);
            this.labStatus.MinimumSize = new System.Drawing.Size(32, 48);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(32, 48);
            this.labStatus.TabIndex = 3;
            this.labStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragDrop);
            this.labStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragEnter);
            this.labStatus.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseDoubleClick);
            this.labStatus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseMove);
            // 
            // imgStatus
            // 
            this.imgStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgStatus.ImageStream")));
            this.imgStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imgStatus.Images.SetKeyName(0, "Disable");
            this.imgStatus.Images.SetKeyName(1, "OK");
            this.imgStatus.Images.SetKeyName(2, "NG");
            // 
            // labIcon
            // 
            this.labIcon.AllowDrop = true;
            this.labIcon.Image = ((System.Drawing.Image)(resources.GetObject("labIcon.Image")));
            this.labIcon.Location = new System.Drawing.Point(32, 0);
            this.labIcon.Margin = new System.Windows.Forms.Padding(0);
            this.labIcon.Name = "labIcon";
            this.labIcon.Size = new System.Drawing.Size(32, 48);
            this.labIcon.TabIndex = 2;
            this.labIcon.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragDrop);
            this.labIcon.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragEnter);
            this.labIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseDoubleClick);
            this.labIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseMove);
            // 
            // ModuleControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labText);
            this.Controls.Add(this.labIcon);
            this.Controls.Add(this.labIndex);
            this.Controls.Add(this.labStatus);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Location = new System.Drawing.Point(0, -1);
            this.Name = "ModuleControl";
            this.Size = new System.Drawing.Size(256, 48);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModuleBase_DragEnter);
            this.DragLeave += new System.EventHandler(this.ModuleControl_MouseLeave);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseDoubleClick);
            this.MouseLeave += new System.EventHandler(this.ModuleControl_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModuleBase_MouseMove);
            this.ParentChanged += new System.EventHandler(this.ModuleBase_ParentChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labText;
        private System.Windows.Forms.Label labIndex;
        private System.Windows.Forms.Label labIcon;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.ImageList imgStatus;
    }
}
