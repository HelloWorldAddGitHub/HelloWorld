namespace Test.Module
{
    partial class FindShapeModelForm
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
            this.cmbProcess = new System.Windows.Forms.ComboBox();
            this.cmbModule = new System.Windows.Forms.ComboBox();
            this.cmbHObject = new System.Windows.Forms.ComboBox();
            this.cmbHTuple = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmbProcess
            // 
            this.cmbProcess.FormattingEnabled = true;
            this.cmbProcess.Location = new System.Drawing.Point(64, 88);
            this.cmbProcess.Name = "cmbProcess";
            this.cmbProcess.Size = new System.Drawing.Size(121, 20);
            this.cmbProcess.TabIndex = 1;
            this.cmbProcess.TextChanged += new System.EventHandler(this.cmbProcess_TextChanged);
            // 
            // cmbModule
            // 
            this.cmbModule.FormattingEnabled = true;
            this.cmbModule.Location = new System.Drawing.Point(191, 88);
            this.cmbModule.Name = "cmbModule";
            this.cmbModule.Size = new System.Drawing.Size(121, 20);
            this.cmbModule.TabIndex = 1;
            this.cmbModule.TextChanged += new System.EventHandler(this.cmbModule_TextChanged);
            // 
            // cmbHObject
            // 
            this.cmbHObject.FormattingEnabled = true;
            this.cmbHObject.Location = new System.Drawing.Point(318, 88);
            this.cmbHObject.Name = "cmbHObject";
            this.cmbHObject.Size = new System.Drawing.Size(121, 20);
            this.cmbHObject.TabIndex = 1;
            this.cmbHObject.TextChanged += new System.EventHandler(this.cmbHObject_TextChanged);
            // 
            // cmbHTuple
            // 
            this.cmbHTuple.FormattingEnabled = true;
            this.cmbHTuple.Location = new System.Drawing.Point(318, 114);
            this.cmbHTuple.Name = "cmbHTuple";
            this.cmbHTuple.Size = new System.Drawing.Size(121, 20);
            this.cmbHTuple.TabIndex = 1;
            this.cmbHTuple.TextChanged += new System.EventHandler(this.cmbHTuple_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(375, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // FindShapeModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 393);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cmbHTuple);
            this.Controls.Add(this.cmbHObject);
            this.Controls.Add(this.cmbModule);
            this.Controls.Add(this.cmbProcess);
            this.Name = "FindShapeModelForm";
            this.Text = "模板匹配设置";
            this.Load += new System.EventHandler(this.FindShapeModelForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbProcess;
        private System.Windows.Forms.ComboBox cmbModule;
        private System.Windows.Forms.ComboBox cmbHObject;
        private System.Windows.Forms.ComboBox cmbHTuple;
        private System.Windows.Forms.TextBox textBox1;
    }
}