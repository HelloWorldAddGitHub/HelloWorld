using System;
using HalconDotNet;

namespace Halcon.Camera
{
    public class HCameraInfo
    {
        public HCameraInfo(HInterfaceName name)
        {
            HTuple information, defaults;
            string item = Enum.GetName(typeof(HInterfaceName), name);
            //item = item.Replace("OTNF", "1394").Replace("PP", "++").Replace("_", "-");
            HOperatorSet.InfoFramegrabber(item, "defaults", out information, out defaults);

            Interface = name;
            HorizontalResolution = defaults[0];
            VerticalResolution = defaults[1];
            ImageWidth = defaults[2];
            ImageHeight = defaults[3];
            StartRow = defaults[4];
            StartColumn = defaults[5];
            Field = (FieldMode)Enum.Parse(typeof(FieldMode), defaults[6]);
            BitsPerChannel = defaults[7];
            ColorSpace = (ColorSpaceMode)Enum.Parse(typeof(ColorSpaceMode), defaults[8]);
            Generic = defaults[9];
            ExternalTrigger = (ExternalTriggerMode)Enum.Parse(typeof(ExternalTriggerMode), defaults[10]);
            CameraType = (CameraTypeMode)Enum.Parse(typeof(CameraTypeMode), defaults[11]);
            Device = defaults[12];
            Port = defaults[13];
            LineIn = defaults[14];
        }

        public HInterfaceName Interface { get; set; }             //HALCON图像获取接口名
        public int HorizontalResolution { get; set; }//水平分辨率
        public int VerticalResolution { get; set; }  //垂直分辨率
        public int ImageWidth { get; set; }          //图像宽度
        public int ImageHeight { get; set; }         //图像高度
        public int StartRow { get; set; }            //开始行
        public int StartColumn { get; set; }         //开始列
        public FieldMode Field { get; set; }            //半图像或全图像 "first", "second", "next", "interlaced", "progressive", "default" 
        public int BitsPerChannel { get; set; }      //每个像素的传输位和图像通道的数量
        public ColorSpaceMode ColorSpace { get; set; }       //颜色空间 "gray", "raw", "rgb", "yuv", "default" 
        public double Generic { get; set; }          //通用参数
        public ExternalTriggerMode ExternalTrigger { get; set; }  //外触发 "true", "false", "default" 
        public CameraTypeMode CameraType { get; set; }       //相机类型 "ntsc", "pal", "auto", "default" 
        public string Device { get; set; }           //设备
        public int Port { get; set; }                //端口
        public int LineIn { get; set; }              //多路复用器的摄像机输入线
    }
}
