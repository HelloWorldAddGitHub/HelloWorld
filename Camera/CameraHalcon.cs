using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace Camera
{
    /// <summary>
    /// 接口名称
    /// </summary>
    public enum HInterfaceName
    {
        /*1394IIDC, 1394IIDC-2,*/ /*ABS, ADLINK, Andor, BitFlow, Crevis, DahengCAM,*/
        DirectFile, DirectShow, File, GenICamTL, GigEVision, /*Ginga++,*/ GingaDG,
        INSPECTA, INSPECTA5, LinX, LPS36, LuCam, MatrixVisionAcquire, MILLite,
        MultiCam, Opteon, p3i2, PcEyeCL, PixeLINK, pylon, SaperaLT, Sentech,
        /*SICK-3DCamera,*/ SiliconSoftware, /*SonyXCI-2,*/ TWAIN, uEye, VRmUsbCam
    }

    /// <summary>
    /// 接口信息
    /// </summary>
    public struct HInterfaceInfo
    {
        public HInterfaceName Name;         //HALCON图像获取接口

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

    /// <summary>
    /// 打开相机时所需信息参数
    /// </summary>
    public struct HCameraInfo
    {
        public HCameraInfo(HInterfaceName name)
        {
            HTuple information, defaults;
            Name = Enum.GetName(typeof(HInterfaceName), name);
            HOperatorSet.InfoFramegrabber(Name, "defaults", out information, out defaults);
            
            HorizontalResolution = defaults[0];
            VerticalResolution = defaults[1];
            ImageWidth = defaults[2];
            ImageHeight = defaults[3];
            StartRow = defaults[4];
            StartColumn = defaults[5];
            Field = defaults[6];
            BitsPerChannel = defaults[7];
            ColorSpace = defaults[8];
            Generic = defaults[9];
            ExternalTrigger = defaults[10];
            CameraType = defaults[11];
            Device = defaults[12];
            Port = defaults[13];
            LineIn = defaults[14];
        }

        public string Name;             //HALCON图像获取接口名
        public int HorizontalResolution;//水平分辨率
        public int VerticalResolution;  //垂直分辨率
        public int ImageWidth;          //图像宽度
        public int ImageHeight;         //图像高度
        public int StartRow;            //开始行
        public int StartColumn;         //开始列
        public string Field;            //半图像或全图像 "first", "second", "next", "interlaced", "progressive", "default" 
        public int BitsPerChannel;      //每个像素的传输位和图像通道的数量
        public string ColorSpace;       //颜色空间 "gray", "raw", "rgb", "yuv", "default" 
        public double Generic;          //通用参数
        public string ExternalTrigger;  //外触发 "true", "false", "default" 
        public string CameraType;       //相机类型 "ntsc", "pal", "auto", "default" 
        public string Device;           //设备
        public int Port;                //端口
        public int LineIn;              //多路复用器的摄像机输入线
    }



    public class CameraHalcon : HFramegrabber, ICamera
    {
        private HCameraInfo info;
        private HImage Image;
        private double maxDelay = -1;


        public bool IsOpen { get; private set; }
        public bool IsGrabbing { get; private set; }


        /// <summary>
        /// 图像处理事件
        /// </summary>
        public event EventHandler<EventArgs> ImageProcessing;
        


        public CameraHalcon(HCameraInfo info)
        {
            this.info = info;
        }


        ~CameraHalcon()
        {
            Close();
        }
        


        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                OpenFramegrabber(
                    info.Name,
                    info.HorizontalResolution,
                    info.VerticalResolution,
                    info.ImageWidth,
                    info.ImageHeight,
                    info.StartRow,
                    info.StartColumn,
                    info.Field,
                    info.BitsPerChannel,
                    info.ColorSpace,
                    info.Generic,
                    info.ExternalTrigger,
                    info.CameraType,
                    info.Device,
                    info.Port,
                    info.LineIn);
                
                IsOpen = true;
            }
            catch (Exception)
            {
                IsOpen = false;
            }

            return IsOpen;
        }


        /// <summary>
        /// 同步采集一帧图像
        /// </summary>
        /// <returns></returns>
        public bool GrabOne()
        {
            try
            {
                Image = GrabImage();
                ImageProcessing(Image, null);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// 异步连续采集图像
        /// </summary>
        public async void GrabStartAsync()
        {
            if (IsGrabbing)
            {
                return;
            }
            else
            {
                IsGrabbing = true;
            }

            await Task.Run(() =>
            {
                try
                {
                    GrabImageStart(maxDelay);

                    while (IsGrabbing)
                    {
                        Image = GrabImageAsync(maxDelay);
                        ImageProcessing(Image, null);
                    }
                }
                catch (Exception)
                {
                    IsGrabbing = false;
                }
            });
        }


        /// <summary>
        /// 停止采集
        /// </summary>
        public void GrabStop()
        {
            IsGrabbing = false;
        }

        
        /// <summary>
        /// 关闭相机
        /// </summary>
        public void Close()
        {
            if (IsOpen)
            {
                if (IsGrabbing)
                {
                    IsGrabbing = false;
                }

                IsOpen = false;
                ClearHandleResource();
            }
        }


        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="param">
        /// 参数后缀：
        /// -----_access：访问权限，'ro', 'wo', 'rw'
        /// -----_category：类别
        /// -----_description：工具提示
        /// -----_displayname：显示名称
        /// -----_range：范围，[min, max, step, default]，可选'auto' or 'manual'
        /// -----_type：类型
        /// -----_values：有效值列表，['enable','disable']
        /// -----_visibility：可见性，'beginner', 'expert', 'guru'
        /// </param>
        /// <returns>值</returns>
        public dynamic GetParam(string param)
        {
            try
            {
                HTuple value = GetFramegrabberParam(param);

                switch (value.Length)
                {
                    case 1:
                        switch (value.Type)
                        {
                            case HTupleType.INTEGER: return value.I;
                            case HTupleType.LONG: return value.L;
                            case HTupleType.DOUBLE: return value.D;
                            case HTupleType.STRING: return value.S;
                            default: return null;
                        }
                    default:
                        switch (value.Type)
                        {
                            case HTupleType.INTEGER: return value.IArr;
                            case HTupleType.LONG: return value.LArr;
                            case HTupleType.DOUBLE: return value.DArr;
                            case HTupleType.STRING: return value.SArr;
                            case HTupleType.MIXED: return value;
                            default: return null;
                        }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="value">值</param>
        public void SetParam(string param, dynamic value)
        {
            try
            {
                SetFramegrabberParam(param, value);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
