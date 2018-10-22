namespace Demo
{
    partial class FormLogin
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
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.labAccount = new System.Windows.Forms.Label();
            this.labPassword = new System.Windows.Forms.Label();
            this.butLogin = new System.Windows.Forms.Button();
            this.txbNewPassord = new System.Windows.Forms.TextBox();
            this.butSetPassword = new System.Windows.Forms.Button();
            this.labNewPassword = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbAccount
            // 
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(109, 58);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(121, 20);
            this.cmbAccount.TabIndex = 0;
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(109, 94);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.Size = new System.Drawing.Size(121, 21);
            this.txbPassword.TabIndex = 1;
            // 
            // labAccount
            // 
            this.labAccount.AutoSize = true;
            this.labAccount.Location = new System.Drawing.Point(33, 58);
            this.labAccount.Name = "labAccount";
            this.labAccount.Size = new System.Drawing.Size(41, 12);
            this.labAccount.TabIndex = 2;
            this.labAccount.Text = "账号：";
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(33, 94);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(41, 12);
            this.labPassword.TabIndex = 2;
            this.labPassword.Text = "密码：";
            // 
            // butLogin
            // 
            this.butLogin.Location = new System.Drawing.Point(35, 189);
            this.butLogin.Name = "butLogin";
            this.butLogin.Size = new System.Drawing.Size(75, 23);
            this.butLogin.TabIndex = 3;
            this.butLogin.Text = "登录";
            this.butLogin.UseVisualStyleBackColor = true;
            this.butLogin.Click += new System.EventHandler(this.butLogin_Click);
            // 
            // txbNewPassord
            // 
            this.txbNewPassord.Location = new System.Drawing.Point(109, 133);
            this.txbNewPassord.Name = "txbNewPassord";
            this.txbNewPassord.Size = new System.Drawing.Size(121, 21);
            this.txbNewPassord.TabIndex = 4;
            // 
            // butSetPassword
            // 
            this.butSetPassword.Location = new System.Drawing.Point(155, 189);
            this.butSetPassword.Name = "butSetPassword";
            this.butSetPassword.Size = new System.Drawing.Size(75, 23);
            this.butSetPassword.TabIndex = 3;
            this.butSetPassword.Text = "修改密码";
            this.butSetPassword.UseVisualStyleBackColor = true;
            this.butSetPassword.Click += new System.EventHandler(this.butSetPassword_Click);
            // 
            // labNewPassword
            // 
            this.labNewPassword.AutoSize = true;
            this.labNewPassword.Location = new System.Drawing.Point(33, 136);
            this.labNewPassword.Name = "labNewPassword";
            this.labNewPassword.Size = new System.Drawing.Size(53, 12);
            this.labNewPassword.TabIndex = 2;
            this.labNewPassword.Text = "新密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.LimeGreen;
            this.label1.Location = new System.Drawing.Point(33, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "当前账户";
            // 
            // FormLogin
            // 
            this.AcceptButton = this.butLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbNewPassord);
            this.Controls.Add(this.butSetPassword);
            this.Controls.Add(this.butLogin);
            this.Controls.Add(this.labNewPassword);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.labAccount);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.cmbAccount);
            this.Name = "FormLogin";
            this.Text = "FormLogin";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbAccount;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label labAccount;
        private System.Windows.Forms.Label labPassword;
        private System.Windows.Forms.Button butLogin;
        private System.Windows.Forms.TextBox txbNewPassord;
        private System.Windows.Forms.Button butSetPassword;
        private System.Windows.Forms.Label labNewPassword;
        private System.Windows.Forms.Label label1;
    }
}