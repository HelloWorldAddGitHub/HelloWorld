using System;

namespace HalconCamera
{
    #region PixelType
    //switch (PixelType)
    //{
    //    //“byte”:
    //    //1字节每像素，（0..255）

    //    //“int1”:
    //    //1字节每像素，有符号

    //    //“int2”:
    //    //每个像素2字节，有符号

    //    //“uint2”:
    //    //每个像素2字节，无符号

    //    //“int4”:
    //    //每个像素4字节，有符号

    //    //“int8”:
    //    //8字节每像素，有符号，仅在64位系统上可用

    //    //“real”:
    //    //4字节每像素，浮点数

    //    //“complex”
    //    //“实数”的两个矩阵

    //    //“vector_field_relative”:
    //    //包含向量的“实数”的两个矩阵

    //    //“vector_field_absolute”:
    //    //包含绝对坐标的“实数”的两个矩阵

    //    //“dir”:
    //    //1字节每像素（0..180）

    //    //“cyclic”:
    //    //1字节每像素; 循环算法(0..255)

    //    case "Mono8":
    //        HImage hImage = new HImage("byte", (int)Width, (int)Height, ptr);
    //        break;

    //    default:
    //        break;
    //}
    #endregion

    public interface ICamera
    {
        //int Index { get; set; }
        //string Name { get; set; }

        
        //string PixelType { get; set; }//像素类型
        //long Width { get; set; }//图像的宽
        //long Height { get; set; }//图像的高
        //long ExposureTime { get; set; }//曝光时间
        //long Gain { get; set; }//增益
        //long BlackLevel { get; set; }//黑电平
        //long Gamma { get; set; }//伽马


        /// <summary>
        /// 是否打开相机
        /// </summary>
        bool IsOpen { get; }


        /// <summary>
        /// 图像处理事件
        /// </summary>
        event EventHandler<ImageGrabbedEventArgs> ImageGrabbed;


        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        bool Open();

        /// <summary>
        /// 同步采集一帧图像
        /// </summary>
        /// <returns></returns>
        bool GrabOne();
        
        /// <summary>
        /// 异步连续采集图像
        /// </summary>
        void GrabStartAsync();

        /// <summary>
        /// 停止采集
        /// </summary>
        void GrabStop();

        /// <summary>
        /// 关闭相机
        /// </summary>
        void Close();

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="name">参数</param>
        /// <returns>值</returns>
        dynamic GetParam(string name);

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="name">参数</param>
        /// <param name="value">值</param>
        void SetParam(string name, dynamic value);
    }
}
