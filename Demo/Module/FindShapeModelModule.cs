using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Halcon.Window;
using HalconDotNet;

namespace Test.Module
{
    [Module("模板匹配", "图像处理")]
    public class FindShapeModelModule : ModuleBase
    {
        public override string Name { get; set; } = "模板匹配";

        #region 输入参数

        public HObject Image { get; set; }
        
        #endregion

        #region 输出参数

        public HTuple Row, Column, Angle, Score;

        #endregion

        #region 内部参数

        //public string FileName;

        private HTuple fileName, modelID, angleStart, angleExtent,
                minScore, numMatches, maxOverlap, subPixel, numLevels, greediness;

        #endregion

        //public FindShapeModelModule(Process process, HalconWindow window) : base(process, window)
        //{

        //}

        public override bool Run()
        {
            if (modelID == null)
            {
                HOperatorSet.ReadShapeModel(fileName, out modelID);
            }



            HOperatorSet.FindShapeModel(Image, modelID, angleStart, angleExtent, 
                minScore, numMatches, maxOverlap, subPixel, numLevels, greediness, 
                out Row, out Column, out Angle, out Score);

            return true;
        }

        public override void ShowDialog()
        {
            FindShapeModelForm f = new FindShapeModelForm(this);
            f.ShowDialog();
        }
    }
}
