using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camera
{
    public abstract class Camera : ICamera
    {
        public long Width { get; protected set; }
        public long Height { get; protected set; }
        public string PixelType { get; protected set; }
        public int ExposureTime { get; set; }//曝光时间
        public int Gain { get; set; }//增益
        public int BlackLevel { get; set; }//黑电平
        public int Gamma { get; set; }//伽马

        public bool IsOpen { get; protected set; } = false;
        public bool IsGrabbing { get; protected set; } = false;



        public event EventHandler<EventArgs> ImageGrabbed;



        

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        public abstract bool Open();
        
        /// <summary>
        /// 同步采集一帧图像
        /// </summary>
        /// <returns></returns>
        public abstract bool GrabOne();

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
    }
}
