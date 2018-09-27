using System;
using HalconDotNet;

namespace Camera
{
    public abstract class Camera
    {
        public virtual bool IsOpen { get; } = false;
        public virtual bool IsImageProcess { get; set; } = true;



        public event EventHandler<ImageGrabbedEventArgs> ImageGrabbed;

        protected void ImageProcess(ImageGrabbedEventArgs e)
        {
            ImageGrabbed?.Invoke(this, e);
        }



        /// <summary>
        /// 获取像素类型和颜色格式
        /// </summary>
        /// <param name="type">需要转换的像素类型</param>
        /// <param name="pixelType">像素类型</param>
        /// <param name="colorFormat">颜色格式</param>
        protected virtual void GetHColorFormatAndHPixelType(object type, out HPixelType pixelType, out HColorFormat? colorFormat)
        {
            pixelType = HPixelType.@byte;
            colorFormat = null;
        }


        /// <summary>
        /// 生成HObject类型图像
        /// </summary>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <param name="pixelType">像素类型</param>
        /// <param name="colorFormat">颜色格式（planar = null， packed = HColorFormat）</param>
        /// <param name="pixelPointers">像素指针（单通道或RGB顺序 + [清理内存函数指针]）</param>
        /// <returns></returns>
        protected HObject GenHImage(int width, int height, HPixelType pixelType, HColorFormat? colorFormat, params IntPtr[] pixelPointers)
        {
            HObject image = null;
            string type = Enum.GetName(typeof(HPixelType), pixelType);

            if (colorFormat == null)
            {
                if (pixelPointers.Length == 1)
                {
                    HOperatorSet.GenImage1(out image, type, width, height, pixelPointers[0]);
                }
                else if (pixelPointers.Length == 2)
                {
                    HOperatorSet.GenImage1Extern(out image, type, width, height, pixelPointers[0], pixelPointers[1]);
                }
                else if (pixelPointers.Length == 3)
                {
                    HOperatorSet.GenImage3(out image, type, width, height,
                        pixelPointers[0], pixelPointers[1], pixelPointers[2]);
                }
                else if (pixelPointers.Length == 4)
                {
                    HOperatorSet.GenImage3Extern(out image, type, width, height,
                        pixelPointers[0], pixelPointers[1], pixelPointers[2], pixelPointers[3]);
                }
            }
            else
            {
                if (pixelPointers.Length == 1)
                {
                    HOperatorSet.GenImageInterleaved(out image, pixelPointers[0],
                        Enum.GetName(typeof(HColorFormat), colorFormat),
                        width, height, -1, type, 0, 0, 0, 0, -1, 0);
                }
            }

            return image;
        }



        /// <summary>
        /// 枚举相机
        /// </summary>
        public virtual void Enumerate() { }


        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns>相机打开成功，返回true，否则返回false</returns>
        public abstract bool Open();

        /// <summary>
        /// 同步采集一帧图像
        /// </summary>
        /// <returns>图像采集成功，返回true，否则返回false</returns>
        public abstract HObject GrabOne();

        /// <summary>
        /// 异步连续采集图像
        /// </summary>
        public abstract void GrabStartAsync();

        /// <summary>
        /// 停止采集图像
        /// </summary>
        public abstract void GrabStop();

        /// <summary>
        /// 关闭相机
        /// </summary>
        public abstract void Close();


        #region 获取或设置参数
        /// <summary>
        /// 获取布尔参数或获取命令参数执行状态
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        public virtual void GetParam(string name, out bool? value)
        {
            value = null;
        }

        /// <summary>
        /// 获取整型参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>long[Minimum, Maximum, Increment]</returns>
        public virtual long[] GetParam(string name, out long? value)
        {
            value = null;
            return null;
        }

        /// <summary>
        /// 获取浮点参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>double[Minimum, Maximum, Increment?]</returns>
        public virtual double?[] GetParam(string name, out double? value)
        {
            value = null;
            return null;
        }

        /// <summary>
        /// 获取枚举参数或字符串参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>参数可以设置的值</returns>
        public virtual string[] GetParam(string name, out string value)
        {
            value = null;
            return null;
        }


        /// <summary>
        /// 执行命令参数
        /// </summary>
        /// <param name="name">参数名称</param>
        public virtual void SetParam(string name)
        {

        }

        /// <summary>
        /// 设置布尔参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        public virtual void SetParam(string name, bool value)
        {

        }

        /// <summary>
        /// 设置整型参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        public virtual void SetParam(string name, long value)
        {

        }

        /// <summary>
        /// 设置浮点参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        public virtual void SetParam(string name, double value)
        {

        }

        /// <summary>
        /// 设置枚举参数或设置字符串参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        public virtual void SetParam(string name, string value)
        {

        }
        #endregion
    }
}
