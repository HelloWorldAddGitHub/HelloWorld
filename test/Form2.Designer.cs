namespace test
{
    partial class Form2
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
            this.hWindowControlEx1 = new HalconEx.Window.HWindowControlEx();
            this.SuspendLayout();
            // 
            // hWindowControlEx1
            // 
            this.hWindowControlEx1.BackColor = System.Drawing.Color.Black;
            this.hWindowControlEx1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControlEx1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControlEx1.LeftMode = HalconEx.Window.LeftModes.Select;
            this.hWindowControlEx1.Location = new System.Drawing.Point(12, 12);
            this.hWindowControlEx1.Name = "hWindowControlEx1";
            this.hWindowControlEx1.Size = new System.Drawing.Size(510, 357);
            this.hWindowControlEx1.TabIndex = 0;
            this.hWindowControlEx1.WindowSize = new System.Drawing.Size(510, 357);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 422);
            this.Controls.Add(this.hWindowControlEx1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private HalconEx.Window.HWindowControlEx hWindowControlEx1;
    }
}