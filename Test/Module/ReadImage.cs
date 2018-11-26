using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Halcon.Window;
using HalconDotNet;

namespace Test
{
    public class ReadImage : ModuleBase
    {
        public HObject Image = new HObject();
        public HTuple fileName { get; set; } = new HTuple();


        public override string Name { get; set; } = "读入图像";

        //public override Form SetupForm { get; set; }

        public ReadImage()
        {

        }

        public ReadImage(Process process, HalconWindow window) : base(process, window)
        {

        }

        public override bool Run()
        {
            ////Image.Dispose();
            //HOperatorSet.ReadImage(out Image, fileName);
            ////ImageWindow?.DispObj(Image);
            //HOperatorSet.DispObj(Image, ImageWindow.HalconWindow);

            //ImageWindow.ReadImage(fileName);
            //ImageWindow.DispObj(Image);
            //ImageWindow.Select();
            ImageWindow.OpenImageDialog();

            return true;
        }

        public override void ShowDialog()
        {
            ReadImageForm rif = new ReadImageForm(this);
            rif.ShowDialog();
        }
    }
}
