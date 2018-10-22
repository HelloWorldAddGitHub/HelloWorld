using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HalconDotNet;

namespace Halcon.Window
{
    public class HalconWindow : HWindowControl
    {
        private ContextMenuStrip cmsMain;
        private IContainer components;
        private ToolStripMenuItem tsmiFullDisp;
        private ToolStripMenuItem tsmiCenterDisp;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmiOpenImage;
        private ToolStripMenuItem tsmiSaveImage;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem tsmiDispCrosshair;
        private ToolStripMenuItem tsmiDispGrid;


        // 读入图像的所有文件名和当前索引
        private string[] fileNames;
        private int currentIndex;

        // 上一次鼠标位置和图像显示区域
        private Point oldMousePoint;
        private Rectangle oldImagePart;

        // 图像显示区域横纵比
        private double imagePartAspectRatio;


        // 图像
        private HObject image;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HObject Image
        {
            get
            {
                return image;
            }
            set
            {
                image?.Dispose();
                image = value;

                if (image != null)
                {
                    HOperatorSet.GetImageSize(image, out imageWidth, out imageHeight);
                    HOperatorSet.GetImageType(image, out imageType);
                    DispImagePart(ImagePart.X, ImagePart.Y, ImagePart.Width, ImagePart.Height);
                }
            }
        }

        private HTuple imageWidth, imageHeight, imageType;
        private ToolStripMenuItem tsmiDumpWindow;
        private ToolStripMenuItem tsmiLastImage;
        private ToolStripMenuItem tsmiNextImage;
        private ToolStripSeparator toolStripSeparator3;

        public enum WindowParam
        {
            save_depth_buffer, plot_quality, interactive_plot,
            axis_captions, caption_color, scale_plot, angle_of_view, display_grid,
            display_axes, window_title, background_color
        }


        public enum HColor
        {
            black, white, red, green, blue, cyan, magenta, yellow, dim_gray, gray,
            light_gray, medium_slate_blue, coral, slate_blue, spring_green,
            orange_red, orange, dark_olive_green, pink, cadet_blue
        }



        [DefaultValue(false)]
        [Description("设置是否启用右键菜单")]
        public bool EnableContextMenu
        {
            get
            {
                if (this.ContextMenuStrip == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                if (value)
                {
                    this.ContextMenuStrip = this.cmsMain;
                }
                else
                {
                    this.ContextMenuStrip = null;
                }
            }
        }




        // 十字线、颜色
        [DefaultValue(false)]
        [Description("获取或设置是否启用十字线")]
        public bool Crosshair { get; set; }

        [DefaultValue(HColor.medium_slate_blue)]
        [Description("获取或设置十字线颜色")]
        public HColor CrosshairColor { get; set; } = HColor.medium_slate_blue;


        // 网格、颜色、增量、网格中心位置
        [DefaultValue(false)]
        [Description("获取或设置是否启用网格")]
        public bool Grid { get; set; }

        [DefaultValue(HColor.dim_gray)]
        [Description("获取或设置网格颜色")]
        public HColor GridColor { get; set; } = HColor.dim_gray;

        [DefaultValue(500)]
        [Description("获取或设置网格间距")]
        public int GridIncrement { get; set; } = 500;

        [DefaultValue(GridCenterMode.ImaegCenter)]
        [Description("获取或设置网格中心位置")]
        public GridCenterMode GridCenter { get; set; }


        public enum GridCenterMode { ImaegCenter, ImageOrigin }





        public HalconWindow() : base()
        {
            InitializeComponent();
        }


        ~HalconWindow()
        {
            image?.Dispose();
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCenterDisp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFullDisp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDispCrosshair = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDispGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLastImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNextImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDumpWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsMain
            // 
            this.cmsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenImage,
            this.tsmiSaveImage,
            this.tsmiDumpWindow,
            this.toolStripSeparator1,
            this.tsmiLastImage,
            this.tsmiNextImage,
            this.toolStripSeparator2,
            this.tsmiCenterDisp,
            this.tsmiFullDisp,
            this.toolStripSeparator3,
            this.tsmiDispCrosshair,
            this.tsmiDispGrid});
            this.cmsMain.Name = "menu";
            this.cmsMain.Size = new System.Drawing.Size(199, 307);
            // 
            // tsmiOpenImage
            // 
            this.tsmiOpenImage.Name = "tsmiOpenImage";
            this.tsmiOpenImage.Size = new System.Drawing.Size(198, 28);
            this.tsmiOpenImage.Text = "打开图像";
            this.tsmiOpenImage.Click += new System.EventHandler(this.tsmiOpenImage_Click);
            // 
            // tsmiSaveImage
            // 
            this.tsmiSaveImage.Name = "tsmiSaveImage";
            this.tsmiSaveImage.Size = new System.Drawing.Size(198, 28);
            this.tsmiSaveImage.Text = "保存图像";
            this.tsmiSaveImage.Click += new System.EventHandler(this.tsmiSaveImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiCenterDisp
            // 
            this.tsmiCenterDisp.Name = "tsmiCenterDisp";
            this.tsmiCenterDisp.Size = new System.Drawing.Size(198, 28);
            this.tsmiCenterDisp.Text = "居中显示";
            this.tsmiCenterDisp.Click += new System.EventHandler(this.menuCenterDisp_Click);
            // 
            // tsmiFullDisp
            // 
            this.tsmiFullDisp.Name = "tsmiFullDisp";
            this.tsmiFullDisp.Size = new System.Drawing.Size(198, 28);
            this.tsmiFullDisp.Text = "填充显示";
            this.tsmiFullDisp.Click += new System.EventHandler(this.menuFullDisp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiDispCrosshair
            // 
            this.tsmiDispCrosshair.CheckOnClick = true;
            this.tsmiDispCrosshair.Name = "tsmiDispCrosshair";
            this.tsmiDispCrosshair.Size = new System.Drawing.Size(198, 28);
            this.tsmiDispCrosshair.Text = "显示十字线";
            this.tsmiDispCrosshair.Click += new System.EventHandler(this.tsmiDispCrosshair_Click);
            // 
            // tsmiDispGrid
            // 
            this.tsmiDispGrid.CheckOnClick = true;
            this.tsmiDispGrid.Name = "tsmiDispGrid";
            this.tsmiDispGrid.Size = new System.Drawing.Size(198, 28);
            this.tsmiDispGrid.Text = "显示网格";
            this.tsmiDispGrid.Click += new System.EventHandler(this.tsmiDispGrid_Click);
            // 
            // tsmiLastImage
            // 
            this.tsmiLastImage.Name = "tsmiLastImage";
            this.tsmiLastImage.Size = new System.Drawing.Size(198, 28);
            this.tsmiLastImage.Text = "上一帧图像";
            this.tsmiLastImage.Click += new System.EventHandler(this.tsmiLastImage_Click);
            // 
            // tsmiNextImage
            // 
            this.tsmiNextImage.Name = "tsmiNextImage";
            this.tsmiNextImage.Size = new System.Drawing.Size(198, 28);
            this.tsmiNextImage.Text = "下一帧图像";
            this.tsmiNextImage.Click += new System.EventHandler(this.tsmiNextImage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiDumpWindow
            // 
            this.tsmiDumpWindow.Name = "tsmiDumpWindow";
            this.tsmiDumpWindow.Size = new System.Drawing.Size(198, 28);
            this.tsmiDumpWindow.Text = "保存窗口";
            this.tsmiDumpWindow.Click += new System.EventHandler(this.tsmiDumpWindow_Click);
            // 
            // HalconWindow
            // 
            this.Name = "HalconWindow";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HalconWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HalconWindow_MouseMove);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.HalconWindow_MouseWheel);
            this.Resize += new System.EventHandler(this.HalconWindow_Resize);
            this.cmsMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        private void HalconWindow_MouseDown(object sender, MouseEventArgs e)
        {
            // 记录鼠标按下时的坐标和图像显示区域
            oldMousePoint = new Point(e.X, e.Y);
            oldImagePart = ImagePart;
        }


        private void HalconWindow_MouseMove(object sender, MouseEventArgs e)
        {
            // 使鼠标位置在窗口内操作有效
            if (e.X > 0 && e.X < WindowSize.Width && e.Y > 0 && e.Y < WindowSize.Height)
            {
                if (e.Button == MouseButtons.Left && image != null)
                {
                    // 图像显示大小和窗口大小的比例
                    double scaleWidth = 1.0 * ImagePart.Width / WindowSize.Width;
                    double scaleHeight = 1.0 * ImagePart.Height / WindowSize.Height;

                    // 相对上一个鼠标位置的图像坐标的偏移量
                    int offsetX = (int)((e.X - oldMousePoint.X) * scaleWidth);
                    int offsetY = (int)((e.Y - oldMousePoint.Y) * scaleHeight);

                    // 图像显示起点坐标
                    int x = oldImagePart.X - offsetX;
                    int y = oldImagePart.Y - offsetY;

                    // 显示部分图像
                    DispImagePart(x, y, ImagePart.Width, ImagePart.Height);
                }
            }
        }


        private void HalconWindow_MouseWheel(object sender, MouseEventArgs e)
        {
            // 使鼠标位置在窗口内操作有效
            if (e.X > 0 && e.X < WindowSize.Width && e.Y > 0 && e.Y < WindowSize.Height)
            {
                if (image == null)
                {
                    return;
                }

                if (imagePartAspectRatio == 0)
                {
                    imagePartAspectRatio = 1.0 * ImagePart.Width / ImagePart.Height;
                }

                // 比例因子
                double zoom;
                int x, y, partWidth, partHeight;

                // 根据滚轮方向设置比例系数
                if (e.Delta > 0)
                {
                    zoom = 1.2;
                }
                else
                {
                    zoom = 0.8;
                }

                // 重新计算显示图像的宽高
                partWidth = (int)(ImagePart.Width * zoom);// 宽 = 宽 * 缩放系数
                partHeight = (int)(partWidth / imagePartAspectRatio);// 高 = 宽 / 宽高比

                // 重新计算X和Y，使图像居中
                x = (int)(ImagePart.X - (partWidth - ImagePart.Width) * (1.0 * e.X / WindowSize.Width));
                y = (int)(ImagePart.Y - (partHeight - ImagePart.Height) * (1.0 * e.Y / WindowSize.Height));

                // 图像显示过大或过小时不再进行缩放
                if (partWidth < 5 || partHeight < 5 || partWidth > imageWidth * 50 || partHeight > imageHeight * 50)
                {
                    return;
                }

                // 显示部分图像
                DispImagePart(x, y, partWidth, partHeight);
            }
        }


        private void HalconWindow_Resize(object sender, EventArgs e)
        {
            if (image != null)
            {
                DispImagePart(ImagePart.X, ImagePart.Y, ImagePart.Width, ImagePart.Height);
            }
        }



        /// <summary>
        /// 显示对象
        /// </summary>
        /// <param name="objectVal"></param>
        public void DispObj(HObject objectVal)
        {
            image?.Dispose();
            image = objectVal;
            HOperatorSet.GetImageSize(image, out imageWidth, out imageHeight);
            HOperatorSet.GetImageType(image, out imageType);
            DispImagePart(ImagePart.X, ImagePart.Y, ImagePart.Width, ImagePart.Height);
        }


        /// <summary>
        /// 显示指定区域图像，如果所有参数为默认参数，则填充显示图像，并重新设置图像横纵比
        /// </summary>
        /// <param name="x">起始点X坐标</param>
        /// <param name="y">起始点Y坐标</param>
        /// <param name="width">显示区域的宽</param>
        /// <param name="height">显示区域的高</param>
        /// <param name="setAspectRatio">是否设置图像的横纵比</param>
        private void DispImagePart(int x = 0, int y = 0, int width = -1, int height = -1, bool setAspectRatio = false)
        {
            try
            {
                // 设置图像显示的横纵比
                if (setAspectRatio)
                {
                    imagePartAspectRatio = 1.0 * width / height;
                }

                // 设置默认大小
                if (width == -1 || height == -1)
                {
                    if (imageWidth == null)
                    {
                        width = ImagePart.Width;
                        height = ImagePart.Height;
                    }
                    else
                    {
                        width = imageWidth;
                        height = imageHeight;
                    }

                    imagePartAspectRatio = 1.0 * width / height;
                }

                // 显示图像部分到窗口
                ImagePart = new Rectangle(x, y, width, height);
                HOperatorSet.SetSystem("flush_graphic", "false");
                HOperatorSet.ClearWindow(HalconWindow);
                HOperatorSet.SetSystem("flush_graphic", "true");
                HOperatorSet.DispObj(image, HalconWindow);

                // 显示网格
                if (Grid)
                {
                    DispGrid();
                }

                // 显示十字线
                if (Crosshair)
                {
                    DispCrosshair();
                }
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 打开图像对话框
        /// </summary>
        public void OpenImageDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开图像";
            ofd.Multiselect = true;
            ofd.Filter = "图像文件|*.ima;*.tif;*.tiff;*.gif;*.bmp;*.jpg;*.jpeg;*.jp2;*.jxr;*.png;*.pcx;*.ras;*.xwd;*.pbm;*.pnm;*.pgm;*.ppm|所有文件|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileNames = ofd.FileNames;
                for (int i = 0; i < fileNames.Count(); i++)
                {
                    fileNames[i] = fileNames[i].Replace("\\", "/");
                }

                image?.Dispose();
                HOperatorSet.ReadImage(out image, fileNames[currentIndex = 0]);
                HOperatorSet.GetImageSize(image, out imageWidth, out imageHeight);
                HOperatorSet.GetImageType(image, out imageType);
                DispImagePart();
            }
        }


        /// <summary>
        /// 保存图像对话框
        /// </summary>
        public void SaveImageDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存图像";
            sfd.Filter = "可移植网络图形格式(*.png)|*.png|Tag图像文件格式(*.tiff)|*.tiff|设备无关位图(*.bmp)|*.bmp|文件交换格式(*.jpeg)|*.jpeg";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName.Replace("\\", "/");
                string ext = Path.GetExtension(sfd.FileName).Replace(".", "");
                HOperatorSet.WriteImage(image, ext, 0, fileName);
            }
        }


        /// <summary>
        /// 保存窗口内容对话框
        /// </summary>
        private void DumpWindowDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存窗口";
            sfd.Filter = "可移植网络图形格式(*.png)|*.png|Tag图像文件格式(*.tiff)|*.tiff|设备无关位图(*.bmp)|*.bmp|文件交换格式(*.jpeg)|*.jpeg";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName.Replace("\\", "/");
                string ext = Path.GetExtension(sfd.FileName).Replace(".", "");
                HOperatorSet.DumpWindow(HalconWindow, ext, fileName);
            }
        }


        /// <summary>
        /// 读取图像
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public void ReadImage(string fileName)
        {
            image?.Dispose();
            HOperatorSet.ReadImage(out image, fileName.Replace("\\", "/"));
            HOperatorSet.GetImageSize(image, out imageWidth, out imageHeight);
            HOperatorSet.GetImageType(image, out imageType);
            DispImagePart();
        }


        /// <summary>
        /// 写入图像
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public void WriteImage(string fileName)
        {
            string name = fileName.Replace("\\", "/");
            string ext = Path.GetExtension(fileName).Replace(".", "");
            HOperatorSet.WriteImage(image, ext, 0, name);
        }


        /// <summary>
        /// 写入窗口内容
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public void DumpWindow(string fileName)
        {
            string name = fileName.Replace("\\", "/");
            string ext = Path.GetExtension(fileName).Replace(".", "");
            HOperatorSet.DumpWindow(HalconWindow, ext, name);
        }


        /// <summary>
        /// 写入窗口内容到图像
        /// </summary>
        /// <returns>窗口图像</returns>
        public void DumpWindowImage()
        {
            HOperatorSet.DumpWindowImage(out image, HalconWindow);
        }


        /// <summary>
        /// 显示上一帧图像
        /// </summary>
        public void SetLastImage()
        {
            if (currentIndex > 0)
            {
                image?.Dispose();
                HOperatorSet.ReadImage(out image, fileNames[--currentIndex]);
                HOperatorSet.GetImageSize(image, out imageWidth, out imageHeight);
                HOperatorSet.GetImageType(image, out imageType);
                DispImagePart();
            }
        }


        /// <summary>
        /// 显示下一帧图像
        /// </summary>
        public void SetNextImage()
        {
            if (currentIndex < fileNames?.Count() - 1)
            {
                image?.Dispose();
                HOperatorSet.ReadImage(out image, fileNames[++currentIndex]);
                HOperatorSet.GetImageSize(image, out imageWidth, out imageHeight);
                HOperatorSet.GetImageType(image, out imageType);
                DispImagePart();
            }
        }


        /// <summary>
        /// 填充显示图像
        /// </summary>
        public void SetFullImagePart()
        {
            if (image == null)
            {
                return;
            }

            // 显示部分图像
            DispImagePart();
        }


        /// <summary>
        /// 居中显示图像
        /// </summary>
        public void SetCenterImagePart()
        {
            if (image == null)
            {
                return;
            }

            // ImagePart 参数
            int x, y, width, height;

            // 控件宽高比
            double windowScale = 1.0 * WindowSize.Width / WindowSize.Height;

            // 图像宽高与控件宽高比
            double widthScale = imageWidth.D / WindowSize.Width;
            double heightScale = imageHeight.D / WindowSize.Height;

            if (widthScale > heightScale)
            {
                // 以图像宽度为基准对图像等比例缩放
                width = imageWidth;
                height = (int)(imageWidth.D / windowScale);
                x = 0;
                y = (imageHeight - height) / 2;
            }
            else
            {
                // 以图像高度为基准对图像等比例缩放
                width = (int)(imageHeight.D * windowScale);
                height = imageHeight;
                x = (imageWidth - width) / 2;
                y = 0;
            }

            // 显示部分图像
            DispImagePart(x, y, width, height, true);
        }


        /// <summary>
        /// 窗口适应图像显示
        /// </summary>
        /// <param name="zoom">窗口相对图像大小的比例因子</param>
        public void SetWindowAdaptedImage(double zoom = 1)
        {
            try
            {
                if (image == null)
                {
                    return;
                }

                // 设置窗口适应图像的大小
                WindowSize = new Size((int)(imageWidth.D * zoom), (int)(imageHeight.D * zoom));

                DispImagePart();
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 显示十字线
        /// </summary>
        /// <param name="size">大小</param>
        /// <param name="lineWidth">线宽</param>
        /// <param name="color">颜色</param>
        public void DispCrosshair()
        {
            try
            {
                if (imageWidth == null)
                {
                    imageWidth = ImagePart.Width;
                    imageHeight = ImagePart.Height;
                }

                double centerRow = imageHeight.D / 2;
                double centerColumn = imageWidth.D / 2;

                // 设置线宽和颜色
                HOperatorSet.SetLineWidth(HalconWindow, 1);
                HOperatorSet.SetColor(HalconWindow, Enum.GetName(typeof(HColor), CrosshairColor).Replace("_", " "));

                // 设置大小
                double[] distance = new double[4];
                distance[0] = centerColumn - ImagePart.Left;
                distance[1] = ImagePart.Right - centerColumn;
                distance[2] = centerRow - ImagePart.Top;
                distance[3] = ImagePart.Bottom - centerRow;

                double size = distance.Max() * 2;
                //int sizeMax = (int)(Math.Pow(2, 15) - 1);

                size = size < 32767 ? size : 32767;// 2^15 - 1 = 32767

                // 显示十字线
                HOperatorSet.DispCross(HalconWindow, centerRow, centerColumn, size, 0);
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 显示网格
        /// </summary>
        public void DispGrid()
        {
            try
            {
                // 中心坐标
                double row = 0, column = 0;

                if (GridCenter == GridCenterMode.ImaegCenter)
                {
                    if (imageWidth == null)
                    {
                        imageWidth = ImagePart.Width;
                        imageHeight = ImagePart.Height;
                    }

                    row = imageHeight.D / 2;
                    column = imageWidth.D / 2;
                }

                // 设置线宽和颜色
                HOperatorSet.SetLineWidth(HalconWindow, 1);
                HOperatorSet.SetColor(HalconWindow, Enum.GetName(typeof(HColor), GridColor).Replace("_", " "));

                // left
                for (double left = column; left > ImagePart.Left; left -= GridIncrement)
                {
                    double row1 = ImagePart.Top;
                    double column1 = left;
                    double row2 = ImagePart.Bottom;
                    double column2 = left;

                    HOperatorSet.DispLine(HalconWindow, row1, column1, row2, column2);
                }

                // right
                for (double right = column + GridIncrement; right < ImagePart.Right; right += GridIncrement)
                {
                    double row1 = ImagePart.Top;
                    double column1 = right;
                    double row2 = ImagePart.Bottom;
                    double column2 = right;

                    HOperatorSet.DispLine(HalconWindow, row1, column1, row2, column2);
                }

                // top
                for (double top = row; top > ImagePart.Top; top -= GridIncrement)
                {
                    double row1 = top;
                    double column1 = ImagePart.Left;
                    double row2 = top;
                    double column2 = ImagePart.Right;

                    HOperatorSet.DispLine(HalconWindow, row1, column1, row2, column2);
                }

                // bottom
                for (double bottom = row + GridIncrement; bottom < ImagePart.Bottom; bottom += GridIncrement)
                {
                    double row1 = bottom;
                    double column1 = ImagePart.Left;
                    double row2 = bottom;
                    double column2 = ImagePart.Right;

                    HOperatorSet.DispLine(HalconWindow, row1, column1, row2, column2);
                }
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 获取像素灰度值
        /// </summary>
        /// <param name="row">像素行坐标</param>
        /// <param name="column">像素列坐标</param>
        /// <returns></returns>
        public HTuple GetGrayval(int row, int column)
        {
            try
            {
                if (image == null)
                {
                    return null;
                }

                // 在图像内获取像素灰度值
                if (row >= 0 && row < imageHeight && column >= 0 && column < imageWidth)
                {
                    HTuple grayval;
                    HOperatorSet.GetGrayval(image, row, column, out grayval);
                    return grayval;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// 获取窗口参数
        /// </summary>
        /// <param name="param">参数</param>
        public HTuple GetWindowParam(WindowParam param)
        {
            HTuple value = null;
            HOperatorSet.GetWindowParam(HalconWindow, Enum.GetName(typeof(WindowParam), param), out value);
            return value;
        }


        /// <summary>
        /// 设置窗口参数
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="value">值</param>
        public void SetWindowParam(WindowParam param, HTuple value)
        {
            HOperatorSet.SetWindowParam(HalconWindow, Enum.GetName(typeof(WindowParam), param), value);
        }


        /// <summary>
        /// 清理窗口
        /// </summary>
        public void ClearWindow()
        {
            HOperatorSet.ClearWindow(HalconWindow);
        }





        private void tsmiOpenImage_Click(object sender, EventArgs e)
        {
            OpenImageDialog();
        }

        private void tsmiSaveImage_Click(object sender, EventArgs e)
        {
            SaveImageDialog();
        }

        private void menuFullDisp_Click(object sender, EventArgs e)
        {
            SetFullImagePart();
        }

        private void menuCenterDisp_Click(object sender, EventArgs e)
        {
            SetCenterImagePart();
        }

        private void tsmiLastImage_Click(object sender, EventArgs e)
        {
            SetLastImage();
        }

        private void tsmiNextImage_Click(object sender, EventArgs e)
        {
            SetNextImage();
        }

        private void tsmiDumpWindow_Click(object sender, EventArgs e)
        {
            DumpWindowDialog();
        }

        private void tsmiDispCrosshair_Click(object sender, EventArgs e)
        {
            if (tsmiDispCrosshair.Checked)
            {
                Crosshair = true;
            }
            else
            {
                Crosshair = false;
            }

            if (image != null)
            {
                DispImagePart(ImagePart.X, ImagePart.Y, ImagePart.Width, ImagePart.Height);
            }
        }

        private void tsmiDispGrid_Click(object sender, EventArgs e)
        {
            if (tsmiDispGrid.Checked)
            {
                Grid = true;
            }
            else
            {
                Grid = false;
            }

            if (image != null)
            {
                DispImagePart(ImagePart.X, ImagePart.Y, ImagePart.Width, ImagePart.Height);
            }
        }
    }
}
