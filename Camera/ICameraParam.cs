using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camera
{
    public interface ICameraParam
    {
        IIntParam this[string name] { get; }
        //long Width { get; }
        //long Height { get; }
        //string PixelType { get; }
        //int ExposureTime { get; set; }//曝光时间
        //int Gain { get; set; }//增益
        //int BlackLevel { get; set; }//黑电平
        //int Gamma { get; set; }//伽马
    }
}
