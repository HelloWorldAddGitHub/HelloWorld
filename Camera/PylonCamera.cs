using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Basler.Pylon;
using HalconDotNet;

namespace Camera
{
    public class PylonCamera : Camera
    {
        private Basler.Pylon.Camera camera;
        private int timeoutMs = 5000;

        public override bool IsOpen
        {
            get { return camera.IsOpen; }
        }

        public PylonCamera()
        {
            camera = new Basler.Pylon.Camera();
        }

        public PylonCamera(CameraInfo<ICameraInfo> cameraInfo)
        {
            camera = new Basler.Pylon.Camera(cameraInfo.Instance);
        }


        /// <summary>
        /// 枚举相机
        /// </summary>
        /// <returns></returns>
        public static new CameraInfoList<ICameraInfo> Enumerate()
        {
            return Basler.Pylon.CameraFinder.Enumerate();
        }


        public override bool Open()
        {
            return camera.Open(timeoutMs, TimeoutHandling.Return);
        }


        public override HObject GrabOne()
        {
            IGrabResult grabResult = camera.StreamGrabber.GrabOne(timeoutMs, TimeoutHandling.Return);

            if (grabResult.GrabSucceeded)
            {
                // 获取图像缓存的指针
                byte[] buffer = grabResult.PixelData as byte[];
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                IntPtr ptr = handle.AddrOfPinnedObject();

                // 深拷贝指针指向的缓存并生成图像
                HColorFormat? colorFormat;
                HPixelType pixelType;
                GetHColorFormatAndHPixelType(grabResult.PixelTypeValue, out pixelType, out colorFormat);
                HObject image = GenHImage(grabResult.Width, grabResult.Height, pixelType, colorFormat, ptr);

                // 释放内存
                handle.Free();

                if (IsImageProcess)
                {
                    // 图像处理
                    ImageProcess(new ImageGrabbedEventArgs(image));
                }
                
                return image;
            }

            return null;
        }

        public override async void GrabStartAsync()
        {
            if (camera.StreamGrabber.IsGrabbing)
            {
                return;
            }

            await Task.Run(() =>
            {
                camera.StreamGrabber.Start();

                while (camera.StreamGrabber.IsGrabbing)
                {
                    IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(timeoutMs, TimeoutHandling.Return);

                    if (grabResult.GrabSucceeded)
                    {
                        // 获取图像缓存的指针
                        byte[] buffer = grabResult.PixelData as byte[];
                        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                        IntPtr ptr = handle.AddrOfPinnedObject();

                        // 深拷贝指针指向的缓存并生成图像
                        HColorFormat? colorFormat;
                        HPixelType pixelType;
                        GetHColorFormatAndHPixelType(grabResult.PixelTypeValue, out pixelType, out colorFormat);
                        HObject image = GenHImage(grabResult.Width, grabResult.Height, pixelType, colorFormat, ptr);

                        // 释放内存
                        handle.Free();

                        if (IsImageProcess)
                        {
                            // 图像处理
                            ImageProcess(new ImageGrabbedEventArgs(image));
                        }
                    }
                }
            });
        }

        public override void GrabStop()
        {
            camera.StreamGrabber.Stop();
        }

        public override void Close()
        {
            camera.Close();
        }


        /// <summary>
        /// 获取颜色格式和像素类型
        /// </summary>
        /// <param name="type">Basler相机像素类型</param>
        /// <param name="pixelType">像素类型</param>
        /// <param name="colorFormat">颜色格式</param>
        protected override void GetHColorFormatAndHPixelType(object type, out HPixelType pixelType, out HColorFormat? colorFormat)
        {
            PixelType pylonPixelType = (PixelType)type;

            switch (pylonPixelType)
            {
                case PixelType.Undefined:
                    break;
                case PixelType.Mono1packed:
                    break;
                case PixelType.Mono2packed:
                    break;
                case PixelType.Mono4packed:
                    break;
                case PixelType.Mono8:
                    pixelType = HPixelType.@byte;
                    colorFormat = null;
                    return;
                case PixelType.Mono8signed:
                    pixelType = HPixelType.int1;
                    colorFormat = null;
                    return;
                case PixelType.Mono10:
                    break;
                case PixelType.Mono10packed:
                    break;
                case PixelType.Mono10p:
                    break;
                case PixelType.Mono12:
                    break;
                case PixelType.Mono12packed:
                    break;
                case PixelType.Mono12p:
                    break;
                case PixelType.Mono16:
                    pixelType = HPixelType.uint2;
                    colorFormat = null;
                    return;
                case PixelType.BayerGR8:
                    break;
                case PixelType.BayerRG8:
                    break;
                case PixelType.BayerGB8:
                    break;
                case PixelType.BayerBG8:
                    break;
                case PixelType.BayerGR10:
                    break;
                case PixelType.BayerRG10:
                    break;
                case PixelType.BayerGB10:
                    break;
                case PixelType.BayerBG10:
                    break;
                case PixelType.BayerGR12:
                    break;
                case PixelType.BayerRG12:
                    break;
                case PixelType.BayerGB12:
                    break;
                case PixelType.BayerBG12:
                    break;
                case PixelType.RGB8packed:
                    pixelType = HPixelType.@byte;
                    colorFormat = HColorFormat.rgb;
                    return;
                case PixelType.BGR8packed:
                    pixelType = HPixelType.@byte;
                    colorFormat = HColorFormat.bgr;
                    return;
                case PixelType.RGBA8packed:
                    pixelType = HPixelType.@byte;
                    colorFormat = HColorFormat.rgbx;
                    return;
                case PixelType.BGRA8packed:
                    pixelType = HPixelType.@byte;
                    colorFormat = HColorFormat.bgrx;
                    return;
                case PixelType.RGB10packed:
                    break;
                case PixelType.BGR10packed:
                    break;
                case PixelType.RGB12packed:
                    break;
                case PixelType.BGR12packed:
                    break;
                case PixelType.RGB16packed:
                    pixelType = HPixelType.uint2;
                    colorFormat = HColorFormat.rgb48;
                    return;
                case PixelType.BGR10V1packed:
                    break;
                case PixelType.BGR10V2packed:
                    break;
                case PixelType.YUV411packed:
                    break;
                case PixelType.YUV422packed:
                    break;
                case PixelType.YUV444packed:
                    break;
                case PixelType.RGB8planar:
                    pixelType = HPixelType.@byte;
                    colorFormat = null;
                    return;
                case PixelType.RGB10planar:
                    break;
                case PixelType.RGB12planar:
                    break;
                case PixelType.RGB16planar:
                    pixelType = HPixelType.uint2;
                    colorFormat = null;
                    return;
                case PixelType.YUV444planar:
                    break;
                case PixelType.YUV422planar:
                    break;
                case PixelType.YUV420planar:
                    break;
                case PixelType.YUV422_YUYV_Packed:
                    break;
                case PixelType.BayerGR12Packed:
                    break;
                case PixelType.BayerRG12Packed:
                    break;
                case PixelType.BayerGB12Packed:
                    break;
                case PixelType.BayerBG12Packed:
                    break;
                case PixelType.BayerGR10pp:
                    break;
                case PixelType.BayerRG10pp:
                    break;
                case PixelType.BayerGB10pp:
                    break;
                case PixelType.BayerBG10pp:
                    break;
                case PixelType.BayerGR12p:
                    break;
                case PixelType.BayerRG12p:
                    break;
                case PixelType.BayerGB12p:
                    break;
                case PixelType.BayerBG12p:
                    break;
                case PixelType.BayerGR16:
                    break;
                case PixelType.BayerRG16:
                    break;
                case PixelType.BayerGB16:
                    break;
                case PixelType.BayerBG16:
                    break;
                case PixelType.RGB12V1packed:
                    break;
                case PixelType.Double:
                    break;
                default:
                    break;
            }

            pixelType = HPixelType.@byte;
            colorFormat = null;
        }



        #region 获取或设置参数
        // Width
        // Height
        // PixelType 像素类型
        // ExposureTime 曝光时间
        // Gain 增益
        // BlackLevel 黑电平
        // Gamma 伽马

        public override void GetParam(string name, out bool? value)
        {
            if (Enum.IsDefined(typeof(BooleanName), name))
            {
                BooleanName booleanName = (BooleanName)Enum.Parse(typeof(BooleanName), name);

                if (!camera.Parameters[booleanName].IsEmpty && camera.Parameters[booleanName].IsReadable)
                {
                    value = camera.Parameters[booleanName].GetValue();
                }
                else
                {
                    value = null;
                }
            }
            else if (Enum.IsDefined(typeof(CommandName), name))
            {
                CommandName commandName = (CommandName)Enum.Parse(typeof(CommandName), name);

                if (!camera.Parameters[commandName].IsEmpty)
                {
                    value = camera.Parameters[commandName].IsExecuting();
                }
                else
                {
                    value = null;
                }
            }
            else
            {
                value = null;
            }
        }

        public override long[] GetParam(string name, out long? value)
        {
            if (Enum.IsDefined(typeof(IntegerName), name))
            {
                IntegerName integerName = (IntegerName)Enum.Parse(typeof(IntegerName), name);

                if (!camera.Parameters[integerName].IsEmpty && camera.Parameters[integerName].IsReadable)
                {
                    long[] param = new long[3];
                    value = camera.Parameters[integerName].GetValue();
                    param[0] = camera.Parameters[integerName].GetMinimum();
                    param[1] = camera.Parameters[integerName].GetMaximum();
                    param[2] = camera.Parameters[integerName].GetIncrement();
                    return param;
                }
                else
                {
                    value = null;
                    return null;
                }
            }
            else
            {
                value = null;
                return null;
            }
        }

        public override double?[] GetParam(string name, out double? value)
        {
            if (Enum.IsDefined(typeof(FloatName), name))
            {
                FloatName floatName = (FloatName)Enum.Parse(typeof(FloatName), name);

                if (!camera.Parameters[floatName].IsEmpty && camera.Parameters[floatName].IsReadable)
                {
                    double?[] param = new double?[3];
                    value = camera.Parameters[floatName].GetValue();
                    param[0] = camera.Parameters[floatName].GetMinimum();
                    param[1] = camera.Parameters[floatName].GetMaximum();
                    param[2] = camera.Parameters[floatName].GetIncrement();
                    return param;
                }
                else
                {
                    value = null;
                    return null;
                }
            }
            else
            {
                value = null;
                return null;
            }
        }

        public override string[] GetParam(string name, out string value)
        {
            if (Enum.IsDefined(typeof(StringName), name))
            {
                StringName stringName = (StringName)Enum.Parse(typeof(StringName), name);

                if (!camera.Parameters[stringName].IsEmpty && camera.Parameters[stringName].IsReadable)
                {
                    value = camera.Parameters[stringName].GetValue();
                    return null;
                }
                else
                {
                    value = null;
                    return null;
                }
            }
            else if (Enum.IsDefined(typeof(EnumName), name))
            {
                EnumName enumName = (EnumName)Enum.Parse(typeof(EnumName), name);

                if (!camera.Parameters[enumName].IsEmpty && camera.Parameters[enumName].IsReadable)
                {
                    List<string> param = new List<string>();
                    IEnumerator<string> enumerator = camera.Parameters[enumName].GetAllValues().GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        param.Add(enumerator.Current);
                    }

                    value = camera.Parameters[enumName].GetValue();
                    return param.ToArray();
                }
                else
                {
                    value = null;
                    return null;
                }
            }
            else
            {
                value = null;
                return null;
            }
        }


        public override void SetParam(string name)
        {
            if (Enum.IsDefined(typeof(CommandName), name))
            {
                CommandName commandName = (CommandName)Enum.Parse(typeof(CommandName), name);

                if (!camera.Parameters[commandName].IsEmpty && !camera.Parameters[commandName].IsExecuting())
                {
                    camera.Parameters[commandName].Execute();
                }
            }
        }

        public override void SetParam(string name, bool value)
        {
            if (Enum.IsDefined(typeof(BooleanName), name))
            {
                BooleanName booleanName = (BooleanName)Enum.Parse(typeof(BooleanName), name);

                if (!camera.Parameters[booleanName].IsEmpty && camera.Parameters[booleanName].IsWritable)
                {
                    camera.Parameters[booleanName].SetValue(value);
                }
            }
        }

        public override void SetParam(string name, long value)
        {
            if (Enum.IsDefined(typeof(IntegerName), name))
            {
                IntegerName integerName = (IntegerName)Enum.Parse(typeof(IntegerName), name);

                if (!camera.Parameters[integerName].IsEmpty && camera.Parameters[integerName].IsWritable)
                {
                    camera.Parameters[integerName].SetValue(value);
                }
            }
        }

        public override void SetParam(string name, double value)
        {
            if (Enum.IsDefined(typeof(FloatName), name))
            {
                FloatName floatName = (FloatName)Enum.Parse(typeof(FloatName), name);

                if (!camera.Parameters[floatName].IsEmpty && camera.Parameters[floatName].IsWritable)
                {
                    camera.Parameters[floatName].SetValue(value);
                }
            }
        }

        public override void SetParam(string name, string value)
        {
            if (Enum.IsDefined(typeof(StringName), name))
            {
                StringName stringName = (StringName)Enum.Parse(typeof(StringName), name);

                if (!camera.Parameters[stringName].IsEmpty && camera.Parameters[stringName].IsWritable)
                {
                    camera.Parameters[stringName].SetValue(value);
                }
            }
            else if (Enum.IsDefined(typeof(EnumName), name))
            {
                EnumName enumName = (EnumName)Enum.Parse(typeof(EnumName), name);

                if (!camera.Parameters[enumName].IsEmpty && camera.Parameters[enumName].CanSetValue(value))
                {
                    camera.Parameters[enumName].SetValue(value);
                }
            }
        }
        #endregion
    }
}
