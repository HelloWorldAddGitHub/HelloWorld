using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Halcon.Window;
using Halcon.Camera;
using HalconDotNet;

namespace test
{
    public partial class Form1 : Form
    {
        public HObject Image;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hWindowControlEx1.Init(GetImageSize,
                () =>
                {
                    if (Image != null && Image.IsInitialized())
                    {
                        hWindowControlEx1.DispObj(Image);
                    }
                });
        }

        public Size GetImageSize()
        {
            if (Image != null && Image.IsInitialized())
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(Image, out width, out height);
                return new Size(width, height);
            }
            else
            {
                return new Size();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开图像";
            //ofd.Multiselect = true;
            ofd.Filter = "图像文件|*.ima;*.tif;*.tiff;*.gif;*.bmp;*.jpg;*.jpeg;*.jp2;*.jxr;*.png;*.pcx;*.ras;*.xwd;*.pbm;*.pnm;*.pgm;*.ppm|所有文件|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                //for (int i = 0; i < fileNames.Count(); i++)
                //{
                //    fileNames[i] = fileNames[i].Replace("\\", "/");
                //}

                Image?.Dispose();
                HOperatorSet.ReadImage(out Image, fileName);
                hWindowControlEx1.ClearWindow();
                hWindowControlEx1.SetImagePartAdapt();
                hWindowControlEx1.DispObj(Image);
                //HOperatorSet.GetImageSize(Image, out imageWidth, out imageHeight);
                //HOperatorSet.GetImageType(Image, out imageType);
                //DispImagePart();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var info = CameraControl.Find();

            cameraControl1.Open();
            cameraControl1.Start((image) =>
            {
                Image = image;
                hWindowControlEx1.DispObj(image);
                //HOperatorSet.DispObj(image, hWindowControlEx1);
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cameraControl1.Close();
        }
    }
}
