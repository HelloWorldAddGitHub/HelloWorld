namespace Demo
{
    partial class FormProcessBarSet
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMainProcess = new System.Windows.Forms.ComboBox();
            this.dgvProcess = new System.Windows.Forms.DataGridView();
            this.txtIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtProcess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butDown = new System.Windows.Forms.Button();
            this.butUp = new System.Windows.Forms.Button();
            this.butDel = new System.Windows.Forms.Button();
            this.butAdd = new System.Windows.Forms.Button();
            this.ckbDefault = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcess)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目名称";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(70, 12);
            this.txtProjectName.Margin = new System.Windows.Forms.Padding(2);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(198, 21);
            this.txtProjectName.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(516, 428);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(598, 428);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "主流程";
            // 
            // cmbMainProcess
            // 
            this.cmbMainProcess.FormattingEnabled = true;
            this.cmbMainProcess.Location = new System.Drawing.Point(70, 38);
            this.cmbMainProcess.Name = "cmbMainProcess";
            this.cmbMainProcess.Size = new System.Drawing.Size(198, 20);
            this.cmbMainProcess.TabIndex = 4;
            this.cmbMainProcess.DropDown += new System.EventHandler(this.cmbMainProcess_DropDown);
            // 
            // dgvProcess
            // 
            this.dgvProcess.AllowUserToAddRows = false;
            this.dgvProcess.AllowUserToDeleteRows = false;
            this.dgvProcess.AllowUserToOrderColumns = true;
            this.dgvProcess.AllowUserToResizeRows = false;
            this.dgvProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtIndex,
            this.txtProcess});
            this.dgvProcess.Location = new System.Drawing.Point(6, 20);
            this.dgvProcess.MultiSelect = false;
            this.dgvProcess.Name = "dgvProcess";
            this.dgvProcess.RowTemplate.Height = 23;
            this.dgvProcess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProcess.Size = new System.Drawing.Size(307, 385);
            this.dgvProcess.TabIndex = 5;
            this.dgvProcess.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcess_CellDoubleClick);
            this.dgvProcess.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcess_CellEnter);
            this.dgvProcess.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcess_CellLeave);
            this.dgvProcess.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcess_CellValueChanged);
            // 
            // txtIndex
            // 
            this.txtIndex.HeaderText = "序号";
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.ReadOnly = true;
            this.txtIndex.Width = 75;
            // 
            // txtProcess
            // 
            this.txtProcess.HeaderText = "流程";
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtProcess.Width = 150;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butDown);
            this.groupBox1.Controls.Add(this.butUp);
            this.groupBox1.Controls.Add(this.butDel);
            this.groupBox1.Controls.Add(this.butAdd);
            this.groupBox1.Controls.Add(this.dgvProcess);
            this.groupBox1.Location = new System.Drawing.Point(273, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 411);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "流程列表";
            // 
            // butDown
            // 
            this.butDown.Location = new System.Drawing.Point(319, 107);
            this.butDown.Name = "butDown";
            this.butDown.Size = new System.Drawing.Size(75, 23);
            this.butDown.TabIndex = 6;
            this.butDown.Text = "下移";
            this.butDown.UseVisualStyleBackColor = true;
            this.butDown.Click += new System.EventHandler(this.butDown_Click);
            // 
            // butUp
            // 
            this.butUp.Location = new System.Drawing.Point(319, 78);
            this.butUp.Name = "butUp";
            this.butUp.Size = new System.Drawing.Size(75, 23);
            this.butUp.TabIndex = 6;
            this.butUp.Text = "上移";
            this.butUp.UseVisualStyleBackColor = true;
            this.butUp.Click += new System.EventHandler(this.butUp_Click);
            // 
            // butDel
            // 
            this.butDel.Location = new System.Drawing.Point(319, 49);
            this.butDel.Name = "butDel";
            this.butDel.Size = new System.Drawing.Size(75, 23);
            this.butDel.TabIndex = 6;
            this.butDel.Text = "删除";
            this.butDel.UseVisualStyleBackColor = true;
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // butAdd
            // 
            this.butAdd.Location = new System.Drawing.Point(319, 20);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(75, 23);
            this.butAdd.TabIndex = 6;
            this.butAdd.Text = "添加";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // ckbDefault
            // 
            this.ckbDefault.AutoSize = true;
            this.ckbDefault.Location = new System.Drawing.Point(70, 68);
            this.ckbDefault.Name = "ckbDefault";
            this.ckbDefault.Size = new System.Drawing.Size(132, 16);
            this.ckbDefault.TabIndex = 7;
            this.ckbDefault.Text = "设置为默认启动项目";
            this.ckbDefault.UseVisualStyleBackColor = true;
            // 
            // FormProcessBarSet
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(684, 464);
            this.Controls.Add(this.ckbDefault);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbMainProcess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormProcessBarSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目设置";
            this.Load += new System.EventHandler(this.TabTextEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcess)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMainProcess;
        private System.Windows.Forms.DataGridView dgvProcess;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butDown;
        private System.Windows.Forms.Button butUp;
        private System.Windows.Forms.Button butDel;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtProcess;
        private System.Windows.Forms.CheckBox ckbDefault;
    }
}