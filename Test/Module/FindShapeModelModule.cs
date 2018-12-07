using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public HObject GetHObject(string path)
        {
            string[] array = path.Split('.');

            if (array.Length != 3 || array[2] == "(集合)")
            {
                return null;
            }

            string process = array[0];
            int index = Convert.ToInt32(array[1].Split('[', ']')[1]) - 1;
            string name = array[2];

            foreach (var item in Process.Project)
            {
                if (item.Name == process)
                {
                    return item[index].OutHObjectParams[name];
                }
            }

            return null;
        }
    }
}
