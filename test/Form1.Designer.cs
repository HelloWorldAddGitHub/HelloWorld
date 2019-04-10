namespace test
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.hWindowControlEx1 = new Halcon.Window.HWindowControlEx();
            this.button1 = new System.Windows.Forms.Button();
            this.cameraControl1 = new Halcon.Camera.CameraControl(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hWindowControlEx1
            // 
            this.hWindowControlEx1.BackColor = System.Drawing.Color.Black;
            this.hWindowControlEx1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControlEx1.EnableContextMenu = true;
            this.hWindowControlEx1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControlEx1.Location = new System.Drawing.Point(12, 12);
            this.hWindowControlEx1.Name = "hWindowControlEx1";
            this.hWindowControlEx1.Size = new System.Drawing.Size(320, 240);
            this.hWindowControlEx1.TabIndex = 0;
            this.hWindowControlEx1.WindowSize = new System.Drawing.Size(320, 240);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(381, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cameraControl1
            // 
            this.cameraControl1.InterfaceName = Halcon.Camera.HInterfaceName.DirectShow;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(488, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(488, 51);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 407);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hWindowControlEx1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Halcon.Window.HWindowControlEx hWindowControlEx1;
        private System.Windows.Forms.Button button1;
        private Halcon.Camera.CameraControl cameraControl1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

