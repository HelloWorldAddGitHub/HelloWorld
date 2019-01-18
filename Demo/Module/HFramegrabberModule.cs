using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Halcon.Camera;

namespace Test.Module
{
    [Module("Halcon采集", "图像采集")]
    public class HFramegrabberModule : ModuleBase
    {
        public HalconCamera cam = new HalconCamera();

        public override string Name { get; set; } = "Halcon采集";


        public HFramegrabberModule()
        {
            cam.ImageGrabbed += Cam_ImageGrabbed;
        }

        private void Cam_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            HWindow.DispObj(e.Image);
        }

        public override bool Run()
        {
            cam.GrabOne();
            //Thread.Sleep(1000);
            return true;
        }

        public override void ShowDialog()
        {
            HFramegrabberForm f = new HFramegrabberForm(this);
            f.ShowDialog();
        }
    }
}
