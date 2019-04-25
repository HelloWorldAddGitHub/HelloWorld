namespace HalconEx.ROI
{
    partial class ROIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ROIForm));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.新建NToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.打开OToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tspDrawLine = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawCircle = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tsbDrawArc = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawEllipse = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawEllipseArc = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawRectangle1 = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawRectangle2 = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawPolygon = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawNurbs = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawNurbsInterp = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(200, 417);
            this.treeView1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建NToolStripButton,
            this.打开OToolStripButton,
            this.保存SToolStripButton,
            this.toolStripSeparator,
            this.tspDrawLine,
            this.tsbDrawCircle,
            this.tsbDrawArc,
            this.tsbDrawEllipse,
            this.tsbDrawEllipseArc,
            this.tsbDrawRectangle1,
            this.tsbDrawRectangle2,
            this.tsbDrawPolygon,
            this.tsbDrawNurbs,
            this.tsbDrawNurbsInterp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(624, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // 新建NToolStripButton
            // 
            this.新建NToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.新建NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("新建NToolStripButton.Image")));
            this.新建NToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.新建NToolStripButton.Name = "新建NToolStripButton";
            this.新建NToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.新建NToolStripButton.Text = "新建(&N)";
            this.新建NToolStripButton.Click += new System.EventHandler(this.新建NToolStripButton_Click);
            // 
            // 打开OToolStripButton
            // 
            this.打开OToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.打开OToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripButton.Image")));
            this.打开OToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打开OToolStripButton.Name = "打开OToolStripButton";
            this.打开OToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.打开OToolStripButton.Text = "打开(&O)";
            this.打开OToolStripButton.Click += new System.EventHandler(this.打开OToolStripButton_Click);
            // 
            // 保存SToolStripButton
            // 
            this.保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripButton.Image")));
            this.保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripButton.Name = "保存SToolStripButton";
            this.保存SToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.保存SToolStripButton.Text = "保存(&S)";
            this.保存SToolStripButton.Click += new System.EventHandler(this.保存SToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tspDrawLine
            // 
            this.tspDrawLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspDrawLine.Image = ((System.Drawing.Image)(resources.GetObject("tspDrawLine.Image")));
            this.tspDrawLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspDrawLine.Name = "tspDrawLine";
            this.tspDrawLine.Size = new System.Drawing.Size(23, 22);
            this.tspDrawLine.Text = "绘制线段";
            this.tspDrawLine.Click += new System.EventHandler(this.tspDrawLine_Click);
            // 
            // tsbDrawCircle
            // 
            this.tsbDrawCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawCircle.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawCircle.Image")));
            this.tsbDrawCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawCircle.Name = "tsbDrawCircle";
            this.tsbDrawCircle.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawCircle.Text = "绘制圆形";
            this.tsbDrawCircle.Click += new System.EventHandler(this.tsbDrawCircle_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(624, 417);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(420, 417);
            this.dataGridView1.TabIndex = 0;
            // 
            // tsbDrawArc
            // 
            this.tsbDrawArc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawArc.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawArc.Image")));
            this.tsbDrawArc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawArc.Name = "tsbDrawArc";
            this.tsbDrawArc.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawArc.Text = "绘制圆弧";
            this.tsbDrawArc.Click += new System.EventHandler(this.tsbDrawArc_Click);
            // 
            // tsbDrawEllipse
            // 
            this.tsbDrawEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawEllipse.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawEllipse.Image")));
            this.tsbDrawEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawEllipse.Name = "tsbDrawEllipse";
            this.tsbDrawEllipse.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawEllipse.Text = "绘制椭圆";
            this.tsbDrawEllipse.Click += new System.EventHandler(this.tsbDrawEllipse_Click);
            // 
            // tsbDrawEllipseArc
            // 
            this.tsbDrawEllipseArc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawEllipseArc.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawEllipseArc.Image")));
            this.tsbDrawEllipseArc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawEllipseArc.Name = "tsbDrawEllipseArc";
            this.tsbDrawEllipseArc.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawEllipseArc.Text = "绘制椭圆弧";
            this.tsbDrawEllipseArc.Click += new System.EventHandler(this.tsbDrawEllipseArc_Click);
            // 
            // tsbDrawRectangle1
            // 
            this.tsbDrawRectangle1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawRectangle1.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawRectangle1.Image")));
            this.tsbDrawRectangle1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawRectangle1.Name = "tsbDrawRectangle1";
            this.tsbDrawRectangle1.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawRectangle1.Text = "绘制轴平行矩形";
            this.tsbDrawRectangle1.Click += new System.EventHandler(this.tsbDrawRectangle1_Click);
            // 
            // tsbDrawRectangle2
            // 
            this.tsbDrawRectangle2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawRectangle2.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawRectangle2.Image")));
            this.tsbDrawRectangle2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawRectangle2.Name = "tsbDrawRectangle2";
            this.tsbDrawRectangle2.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawRectangle2.Text = "绘制旋转矩形";
            this.tsbDrawRectangle2.Click += new System.EventHandler(this.tsbDrawRectangle2_Click);
            // 
            // tsbDrawPolygon
            // 
            this.tsbDrawPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawPolygon.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawPolygon.Image")));
            this.tsbDrawPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawPolygon.Name = "tsbDrawPolygon";
            this.tsbDrawPolygon.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawPolygon.Text = "绘制任意区域";
            this.tsbDrawPolygon.Click += new System.EventHandler(this.tsbDrawPolygon_Click);
            // 
            // tsbDrawNurbs
            // 
            this.tsbDrawNurbs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawNurbs.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawNurbs.Image")));
            this.tsbDrawNurbs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawNurbs.Name = "tsbDrawNurbs";
            this.tsbDrawNurbs.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawNurbs.Text = "绘制NURBS曲线";
            this.tsbDrawNurbs.Click += new System.EventHandler(this.tsbDrawNurbs_Click);
            // 
            // tsbDrawNurbsInterp
            // 
            this.tsbDrawNurbsInterp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDrawNurbsInterp.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawNurbsInterp.Image")));
            this.tsbDrawNurbsInterp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawNurbsInterp.Name = "tsbDrawNurbsInterp";
            this.tsbDrawNurbsInterp.Size = new System.Drawing.Size(23, 22);
            this.tsbDrawNurbsInterp.Text = "绘制内插的NURBS曲线";
            this.tsbDrawNurbsInterp.Click += new System.EventHandler(this.tsbDrawNurbsInterp_Click);
            // 
            // ROIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ROIForm";
            this.Text = "ROI";
            this.Load += new System.EventHandler(this.ROI_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton 新建NToolStripButton;
        private System.Windows.Forms.ToolStripButton 打开OToolStripButton;
        private System.Windows.Forms.ToolStripButton 保存SToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton tspDrawLine;
        private System.Windows.Forms.ToolStripButton tsbDrawCircle;
        private System.Windows.Forms.ToolStripButton tsbDrawArc;
        private System.Windows.Forms.ToolStripButton tsbDrawEllipse;
        private System.Windows.Forms.ToolStripButton tsbDrawEllipseArc;
        private System.Windows.Forms.ToolStripButton tsbDrawRectangle1;
        private System.Windows.Forms.ToolStripButton tsbDrawRectangle2;
        private System.Windows.Forms.ToolStripButton tsbDrawPolygon;
        private System.Windows.Forms.ToolStripButton tsbDrawNurbs;
        private System.Windows.Forms.ToolStripButton tsbDrawNurbsInterp;
    }
}