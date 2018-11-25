using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace Test
{
    public class ReadImage : ModuleBase
    {
        public HObject Image = new HObject();
        public HTuple fileName { get; set; } = new HTuple();


        public override string Name { get; set; } = "读入图像";

        //public override Form SetupForm { get; set; }

        public override bool Run()
        {
            HOperatorSet.ReadImage(out Image, fileName);

            return true;
        }

        public override void ShowDialog()
        {
            ReadImageForm rif = new ReadImageForm(this);
            rif.ShowDialog();
        }
    }
}
