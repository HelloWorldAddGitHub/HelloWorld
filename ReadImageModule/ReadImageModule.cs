using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Halcon.Window;
using HalconDotNet;

namespace Module
{
    [Module("读入图像", "图像采集")]
    public class ReadImageModule : ModuleBase
    {
        public HObject Image = new HObject();

        public HTuple FileName { get; set; } = new HTuple();


        private int index;


        public override string Name { get; set; } = "读入图像";

        public ReadImageModule()
        {

        }

        public ReadImageModule(Process process) : base(process)
        {

        }

        public override bool Execute()
        {
            if (index >= FileName.TupleLength())
            {
                index = 0;
            }

            Image?.Dispose();
            HOperatorSet.ReadImage(out Image, FileName[index++]);

            if (HDevWindowStack.IsOpen())
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(Image, out width, out height);
                HOperatorSet.SetPart(HDevWindowStack.GetActive(), 0, 0, height - 1, width - 1);
                HOperatorSet.DispImage(Image, HDevWindowStack.GetActive());
            }

            return true;
        }

        public override void ShowDialog()
        {
            ReadImageForm rif = new ReadImageForm(this);
            rif.ShowDialog();
        }
    }
}
