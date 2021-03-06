﻿namespace Halcon.Camera
{
    /// <summary>
    /// HALCON接口名称
    /// </summary>
    public enum HInterfaceName
    {
        /*1394IIDC, 1394IIDC-2,*/ /*ABS, ADLINK, Andor, BitFlow, Crevis, DahengCAM,*/
        DirectFile, DirectShow, File, GenICamTL, GigEVision, /*Ginga++,*/ GingaDG,
        INSPECTA, INSPECTA5, LinX, LPS36, LuCam, MatrixVisionAcquire, MILLite,
        MultiCam, Opteon, p3i2, PcEyeCL, PixeLINK, pylon, SaperaLT, Sentech, /*SICK-3DCamera,*/
        SiliconSoftware, /*SonyXCI-2,*/ TWAIN, uEye, VRmUsbCam
    }

    public enum FieldMode { @default, first, second, next, interlaced, progressive }

    public enum ColorSpaceMode { @default, gray, raw, rgb, yuv }

    public enum ExternalTriggerMode { @default, @true, @false }

    public enum CameraTypeMode { @default, ntsc, pal, auto }

    public enum HPixelType { @byte, int1, int2, uint2, int4, real, direction, cyclic, }

    public enum HColorFormat
    {
        rgb, bgr, rgbx, bgrx, rgb48, bgr48, rgbx64, bgrx64,
        rgb555, bgr555, rgb565, bgr565, rgb555le, bgr555le,
        rgb565le, bgr565le, rgb555be, bgr555be, rgb565be, bgr565be,
        rgb5551, bgr5551, rgb5551le, bgr5551le, rgb5551be, bgr5551be
    }
}
