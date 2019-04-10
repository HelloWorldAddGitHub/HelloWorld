using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using HalconDotNet;

namespace Halcon.Camera
{
    public partial class CameraControl : Component
    {
        #region 属性

        [Browsable(true)]
        [DefaultValue(HInterfaceName.File)]
        [Category("打开相机参数")]
        [Description("HALCON接口名称")]
        public HInterfaceName InterfaceName { get; set; } = HInterfaceName.File;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("水平分辨率")]
        public int HorizontalResolution { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("垂直分辨率")]
        public int VerticalResolution { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("图像宽度")]
        public int ImageWidth { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("图像高度")]
        public int ImageHeight { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("开始行")]
        public int StartRow { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("开始行")]
        public int StartColumn { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(FieldMode.@default)]
        [Category("打开相机参数")]
        [Description("半图像或全图像")]
        public FieldMode Field { get; set; } = FieldMode.@default;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("每个像素的传输位和图像通道的数量")]
        public int BitsPerChannel { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(ColorSpaceMode.@default)]
        [Category("打开相机参数")]
        [Description("颜色空间")]
        public ColorSpaceMode ColorSpace { get; set; } = ColorSpaceMode.@default;

        [Browsable(true)]
        [DefaultValue(-1.0)]
        [Category("打开相机参数")]
        [Description("通用参数")]
        public double Generic { get; set; } = -1.0;

        [Browsable(true)]
        [DefaultValue(ExternalTriggerMode.@default)]
        [Category("打开相机参数")]
        [Description("外触发")]
        public ExternalTriggerMode ExternalTrigger { get; set; } = ExternalTriggerMode.@default;

        [Browsable(true)]
        [DefaultValue(CameraTypeMode.@default)]
        [Category("打开相机参数")]
        [Description("相机类型")]
        public CameraTypeMode CameraType { get; set; } = CameraTypeMode.@default;

        [Browsable(true)]
        [DefaultValue("default")]
        [Category("打开相机参数")]
        [Description("设备")]
        public string Device { get; set; } = "default";

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("端口")]
        public int Port { get; set; } = -1;

        [Browsable(true)]
        [DefaultValue(-1)]
        [Category("打开相机参数")]
        [Description("多路复用器的摄像机输入线")]
        public int LineIn { get; set; } = -1;


        [Browsable(true)]
        [DefaultValue(-1.0)]
        [Description("最大延迟")]
        public double MaxDelay { get; set; } = -1.0;


        [Browsable(false)]
        [Description("获取相机是否打开")]
        public bool IsOpen;

        #endregion

        #region 私有字段

        private HTuple acqHandle;
        private CancellationTokenSource grabCancelToken;

        #endregion

        #region 构造方法

        public CameraControl()
        {
            InitializeComponent();
        }


        public CameraControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        public CameraControl(HCameraInfo info)
        {
            InterfaceName = info.Interface;
            HorizontalResolution = info.HorizontalResolution;
            VerticalResolution = info.VerticalResolution;
            ImageWidth = info.ImageWidth;
            ImageHeight = info.ImageHeight;
            StartRow = info.ImageHeight;
            StartColumn = info.StartColumn;
            Field = info.Field;
            BitsPerChannel = info.BitsPerChannel;
            ColorSpace = info.ColorSpace;
            Generic = info.Generic;
            ExternalTrigger = info.ExternalTrigger;
            CameraType = info.CameraType;
            Device = info.Device;
            Port = info.Port;
            LineIn = info.LineIn;

            InitializeComponent();
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 发现相机
        /// </summary>
        public static List<HInterfaceInfo> Find()
        {
            List<HInterfaceInfo> infoList = new List<HInterfaceInfo>();
            string[] names = Enum.GetNames(typeof(HInterfaceName));
            //string[] names = HOperatorSet.GetParamInfo("open_framegrabber", "Name", "values").SArr;

            foreach (var name in names)
            {
                try
                {
                    //string name = item.Replace("OTNF", "1394").Replace("PP", "++").Replace("_", "-");
                    HInterfaceInfo info = new HInterfaceInfo();
                    HTuple information;
                    HOperatorSet.InfoFramegrabber(name, "info_boards", out information, out info.InfoBoards);

                    if (info.InfoBoards.Length > 0)
                    {
                        info.InterfaceName = (HInterfaceName)Enum.Parse(typeof(HInterfaceName), name);

                        HOperatorSet.InfoFramegrabber(name, "horizontal_resolution", out information, out info.HorizontalResolution);
                        HOperatorSet.InfoFramegrabber(name, "vertical_resolution", out information, out info.VerticalResolution);
                        HOperatorSet.InfoFramegrabber(name, "image_width", out information, out info.ImageWidth);
                        HOperatorSet.InfoFramegrabber(name, "image_height", out information, out info.ImageHeight);
                        HOperatorSet.InfoFramegrabber(name, "start_row", out information, out info.StartRow);
                        HOperatorSet.InfoFramegrabber(name, "start_column", out information, out info.StartColumn);
                        HOperatorSet.InfoFramegrabber(name, "field", out information, out info.Field);
                        HOperatorSet.InfoFramegrabber(name, "bits_per_channel", out information, out info.BitsPerChannel);
                        HOperatorSet.InfoFramegrabber(name, "color_space", out information, out info.ColorSpace);
                        HOperatorSet.InfoFramegrabber(name, "generic", out information, out info.Generic);
                        HOperatorSet.InfoFramegrabber(name, "external_trigger", out information, out info.ExternalTrigger);
                        HOperatorSet.InfoFramegrabber(name, "camera_type", out information, out info.CameraType);
                        HOperatorSet.InfoFramegrabber(name, "device", out information, out info.Device);
                        HOperatorSet.InfoFramegrabber(name, "port", out information, out info.Port);
                        HOperatorSet.InfoFramegrabber(name, "line_in", out information, out info.LineIn);

                        HOperatorSet.InfoFramegrabber(name, "parameters", out information, out info.Parameters);
                        HOperatorSet.InfoFramegrabber(name, "parameters_readonly", out information, out info.ParametersReadonly);
                        HOperatorSet.InfoFramegrabber(name, "parameters_writeonly", out information, out info.ParametersWriteonly);

                        HOperatorSet.InfoFramegrabber(name, "defaults", out information, out info.Defaults);
                        //HOperatorSet.InfoFramegrabber(name, "general", out information, out info.General);
                        //HOperatorSet.InfoFramegrabber(name, "revision", out information, out info.Revision);

                        infoList.Add(info);
                    }
                }
                catch (Exception)
                {

                }
            }

            return infoList;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="info">相机信息</param>
        /// <returns></returns>
        public bool Open(HCameraInfo info)
        {
            InterfaceName = info.Interface;
            HorizontalResolution = info.HorizontalResolution;
            VerticalResolution = info.VerticalResolution;
            ImageWidth = info.ImageWidth;
            ImageHeight = info.ImageHeight;
            StartRow = info.ImageHeight;
            StartColumn = info.StartColumn;
            Field = info.Field;
            BitsPerChannel = info.BitsPerChannel;
            ColorSpace = info.ColorSpace;
            Generic = info.Generic;
            ExternalTrigger = info.ExternalTrigger;
            CameraType = info.CameraType;
            Device = info.Device;
            Port = info.Port;
            LineIn = info.LineIn;

            return Open();
        }


        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                HOperatorSet.OpenFramegrabber(
                        Enum.GetName(typeof(HInterfaceName), InterfaceName),
                        HorizontalResolution,
                        VerticalResolution,
                        ImageWidth,
                        ImageHeight,
                        StartRow,
                        StartColumn,
                        Enum.GetName(typeof(FieldMode), Field),
                        BitsPerChannel,
                        Enum.GetName(typeof(ColorSpaceMode), ColorSpace),
                        Generic,
                        Enum.GetName(typeof(ExternalTriggerMode), ExternalTrigger),
                        Enum.GetName(typeof(CameraTypeMode), CameraType),
                        Device,
                        Port,
                        LineIn,
                        out acqHandle);

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
        public HObject GrabOne()
        {
            try
            {
                HObject image;
                HOperatorSet.GrabImage(out image, acqHandle);
                return image;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// 同步采集一帧图像
        /// </summary>
        /// <returns></returns>
        public void GrabOne(Action<HObject> action)
        {
            try
            {
                HObject image;
                HOperatorSet.GrabImage(out image, acqHandle);
                action(image);
            }
            catch (Exception)
            {
                return;
            }
        }


        /// <summary>
        /// 异步连续采集图像
        /// </summary>
        public void Start(Action<HObject> action)
        {
            grabCancelToken?.Cancel();
            grabCancelToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    HOperatorSet.GrabImageStart(acqHandle, MaxDelay);

                    while (!grabCancelToken.IsCancellationRequested)
                    {
                        HObject image;
                        HOperatorSet.GrabImageAsync(out image, acqHandle, MaxDelay);
                        action(image);
                    }
                }
                catch (Exception)
                {

                }
            });
        }


        /// <summary>
        /// 停止采集
        /// </summary>
        public void Stop()
        {
            grabCancelToken.Cancel();
        }


        /// <summary>
        /// 关闭相机
        /// </summary>
        public void Close()
        {
            if (IsOpen)
            {
                grabCancelToken?.Cancel();
                IsOpen = false;

                HOperatorSet.CloseFramegrabber(acqHandle);
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
        public HTuple GetParam(HTuple param)
        {
            try
            {
                HTuple value;
                HOperatorSet.GetFramegrabberParam(acqHandle, param, out value);
                return value;
            }
            catch (Exception)
            {
                return null;
            }

            #region
            //switch (value.Length)
            //{
            //    case 1:
            //        switch (value.Type)
            //        {
            //            case HTupleType.INTEGER: return value.I;
            //            case HTupleType.LONG: return value.L;
            //            case HTupleType.DOUBLE: return value.D;
            //            case HTupleType.STRING: return value.S;
            //            default: return null;
            //        }
            //    default:
            //        switch (value.Type)
            //        {
            //            case HTupleType.INTEGER: return value.IArr;
            //            case HTupleType.LONG: return value.LArr;
            //            case HTupleType.DOUBLE: return value.DArr;
            //            case HTupleType.STRING: return value.SArr;
            //            case HTupleType.MIXED: return value;
            //            default: return null;
            //        }
            //}
            #endregion
        }


        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="value">值</param>
        public void SetParam(HTuple param, HTuple value)
        {
            try
            {
                HOperatorSet.SetFramegrabberParam(acqHandle, param, value);
            }
            catch (Exception)
            {

            }
        }

        #endregion
    }
}
