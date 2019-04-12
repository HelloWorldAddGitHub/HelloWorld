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
            this.components = new System.ComponentModel.Container();
            this.dataTreeListView1 = new BrightIdeasSoftware.DataTreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.dataTreeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTreeListView1
            // 
            this.dataTreeListView1.AllColumns.Add(this.olvColumn1);
            this.dataTreeListView1.CellEditUseWholeCell = false;
            this.dataTreeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.dataTreeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataTreeListView1.DataSource = null;
            this.dataTreeListView1.Location = new System.Drawing.Point(12, 12);
            this.dataTreeListView1.Name = "dataTreeListView1";
            this.dataTreeListView1.RootKeyValueString = "";
            this.dataTreeListView1.ShowGroups = false;
            this.dataTreeListView1.Size = new System.Drawing.Size(510, 359);
            this.dataTreeListView1.TabIndex = 0;
            this.dataTreeListView1.UseCompatibleStateImageBehavior = false;
            this.dataTreeListView1.View = System.Windows.Forms.View.Details;
            this.dataTreeListView1.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.Text = "Names";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 422);
            this.Controls.Add(this.dataTreeListView1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTreeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.DataTreeListView dataTreeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
    }
}