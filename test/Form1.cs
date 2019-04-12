using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconEx.Window;
using HalconEx.Camera;
using HalconDotNet;
using HalconEx;

namespace test
{
    public partial class Form1 : Form
    {
        public HObject Image;
        HDevelopExport h = new HDevelopExport();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            h.InitHalcon();

            hWindowControlEx1.Init(
                () =>
                {
                    if (h.ho_Image != null && h.ho_Image.IsInitialized())
                    {
                        HTuple width, height;
                        HOperatorSet.GetImageSize(h.ho_Image, out width, out height);
                        return new Size(width, height);
                    }
                    else
                    {
                        return new Size(640, 480);
                    }
                },

                () =>
                {
                    if (h.ho_Image != null && h.ho_Image.IsInitialized())
                    {
                        HOperatorSet.SetColor(hWindowControlEx1, "green");
                        HOperatorSet.DispObj(h.ho_Image, hWindowControlEx1);
                        //HOperatorSet.DispObj(h.ho_ContoursTrans, hWindowControlEx1);
                        hWindowControlEx1.DispObj();
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
            h.ho_Image = HalconExtension.OpenImageDialog();
            hWindowControlEx1.ClearWindow();
            hWindowControlEx1.SetImagePartAdapt();
            hWindowControlEx1.DispObj(h.ho_Image);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var info = CameraControl.Find();
            hWindowControlEx1.ClearWindow();
            cameraControl1.Open();
            cameraControl1.Start((image) =>
            {
                h.ho_Image = image;
                hWindowControlEx1.DispObj(h.ho_Image);
            });
            hWindowControlEx1.SetImagePartAdapt();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cameraControl1.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            h.RunHalcon(hWindowControlEx1);
        }
    }
}
