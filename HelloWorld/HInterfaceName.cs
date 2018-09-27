namespace Halcon.Camera
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
}
