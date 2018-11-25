using System;
using HalconDotNet;

namespace Halcon.Camera
{
    public class HInterfaceInfo
    {
        public HInterfaceInfo()
        {

        }

        public HInterfaceInfo(HInterfaceName interfaceName)
        {
            try
            {
                InterfaceName = interfaceName;
                HTuple name = Enum.GetName(typeof(HInterfaceName), interfaceName);

                HTuple information;
                HOperatorSet.InfoFramegrabber(name, "info_boards", out information, out InfoBoards);

                if (InfoBoards.Length > 0)
                {
                    HOperatorSet.InfoFramegrabber(name, "horizontal_resolution", out information, out HorizontalResolution);
                    HOperatorSet.InfoFramegrabber(name, "vertical_resolution", out information, out VerticalResolution);
                    HOperatorSet.InfoFramegrabber(name, "image_width", out information, out ImageWidth);
                    HOperatorSet.InfoFramegrabber(name, "image_height", out information, out ImageHeight);
                    HOperatorSet.InfoFramegrabber(name, "start_row", out information, out StartRow);
                    HOperatorSet.InfoFramegrabber(name, "start_column", out information, out StartColumn);
                    HOperatorSet.InfoFramegrabber(name, "field", out information, out Field);
                    HOperatorSet.InfoFramegrabber(name, "bits_per_channel", out information, out BitsPerChannel);
                    HOperatorSet.InfoFramegrabber(name, "color_space", out information, out ColorSpace);
                    HOperatorSet.InfoFramegrabber(name, "generic", out information, out Generic);
                    HOperatorSet.InfoFramegrabber(name, "external_trigger", out information, out ExternalTrigger);
                    HOperatorSet.InfoFramegrabber(name, "camera_type", out information, out CameraType);
                    HOperatorSet.InfoFramegrabber(name, "device", out information, out Device);
                    HOperatorSet.InfoFramegrabber(name, "port", out information, out Port);
                    HOperatorSet.InfoFramegrabber(name, "line_in", out information, out LineIn);

                    HOperatorSet.InfoFramegrabber(name, "parameters", out information, out Parameters);
                    HOperatorSet.InfoFramegrabber(name, "parameters_readonly", out information, out ParametersReadonly);
                    HOperatorSet.InfoFramegrabber(name, "parameters_writeonly", out information, out ParametersWriteonly);

                    HOperatorSet.InfoFramegrabber(name, "defaults", out information, out Defaults);
                    HOperatorSet.InfoFramegrabber(name, "general", out information, out General);
                    HOperatorSet.InfoFramegrabber(name, "revision", out information, out Revision);
                }
            }
            catch (Exception)
            {

            }
        }

        public HInterfaceName InterfaceName;         //HALCON图像获取接口

        public HTuple HorizontalResolution; //水平分辨率
        public HTuple VerticalResolution;   //垂直分辨率
        public HTuple ImageWidth;           //图像宽度
        public HTuple ImageHeight;          //图像高度
        public HTuple StartRow;             //开始行
        public HTuple StartColumn;          //开始列
        public HTuple Field;                //半图像或全图像 "first", "second", "next", "interlaced", "progressive", "default" 
        public HTuple BitsPerChannel;       //每个像素的传输位和图像通道的数量
        public HTuple ColorSpace;           //颜色空间 "gray", "raw", "rgb", "yuv", "default" 
        public HTuple Generic;              //通用参数
        public HTuple ExternalTrigger;      //外触发 "true", "false", "default" 
        public HTuple CameraType;           //相机类型 "ntsc", "pal", "auto", "default" 
        public HTuple Device;               //设备
        public HTuple Port;                 //端口
        public HTuple LineIn;               //多路复用器的摄像机输入线

        public HTuple Parameters;           //读写参数
        public HTuple ParametersReadonly;   //只读参数
        public HTuple ParametersWriteonly;  //只写参数

        public HTuple Defaults;             //open_framegrabber的默认值
        public HTuple General;              //一般信息
        public HTuple InfoBoards;           //相机信息
        public HTuple Revision;             //版本号
    }
}
