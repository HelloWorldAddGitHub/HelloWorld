using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basler.Pylon;
using Camera;

namespace Demo
{
    public partial class Form1 : Form
    {
        PylonCamera cam = new PylonCamera();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            halconWindow1.OpenImageDialog();

            
        }

        //private void Cam_ImageGrabbed(object sender, Camera.ImageGrabbedEventArgs e)
        //{
        //    halconWindow1.Image = e.Image;
        //}

        //private void Camera_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
        //{
        //    halconWindow1.Image = e.Image;
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "打开相机")
            {
                //halconCamera1.Open();
                //halconCamera1.GrabStartAsync();

                //cam.Open();

                button2.Text = "关闭相机";
            }
            else
            {
                //halconCamera1.GrabStop();
                //halconCamera1.Close();
                //cam.Close();


                button2.Text = "打开相机";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cam.ImageGrabbed += Cam_ImageGrabbed;

            //List<ICameraInfo> camList = Camera.CameraFinder.Find(Camera.CameraFinder.Finder.Pylon);

            //cam = new PylonCamera(camList[0]);

            CameraInfoList<ICameraInfo> eee = PylonCamera.Enumerate();
            CameraInfo<ICameraInfo> fff = eee.Cameras[0];


            CameraInfoList<ICameraInfo> info = Basler.Pylon.CameraFinder.Enumerate();

            //CameraInfo<int> xx = 2;

            int[] a = new int[] { 1, 2, 3 };

            
        }
    }
}
