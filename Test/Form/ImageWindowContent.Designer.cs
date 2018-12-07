namespace Test
{
    partial class ImageWindowContent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Window = new Halcon.Window.HalconWindow();
            this.SuspendLayout();
            // 
            // halconWindow1
            // 
            this.Window.BackColor = System.Drawing.Color.Black;
            this.Window.BorderColor = System.Drawing.Color.Black;
            this.Window.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Window.EnableContextMenu = true;
            this.Window.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.Window.Location = new System.Drawing.Point(0, 0);
            this.Window.Name = "halconWindow1";
            this.Window.Size = new System.Drawing.Size(400, 300);
            this.Window.TabIndex = 0;
            this.Window.WindowSize = new System.Drawing.Size(400, 300);
            // 
            // ImageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.Window);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ImageWindow";
            this.Text = "图像窗口";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public Halcon.Window.HalconWindow Window;
    }
}