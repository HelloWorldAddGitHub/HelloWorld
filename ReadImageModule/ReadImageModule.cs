using Halcon.Window;
using HalconDotNet;

namespace Module
{
    [Module("读入图像", "图像采集")]
    public class ReadImageModule : ModuleBase
    {
        public HObject Image = new HObject();
        public HTuple fileName { get; set; } = new HTuple();
        private int index;
        

        public override string Name { get; set; } = "读入图像";

        //public override Form SetupForm { get; set; }

        public ReadImageModule()
        {

        }

        public ReadImageModule(Process process, HalconWindow window) : base(process, window)
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
            //ImageWindow.OpenImageDialog();

            if (index >= fileName.TupleLength())
            {
                index = 0;
            }

            Image?.Dispose();
            HWindow.ReadImage(fileName[index++]);

            return true;
        }

        public override void ShowDialog()
        {
            ReadImageForm rif = new ReadImageForm(this);
            rif.ShowDialog();
        }
    }
}
