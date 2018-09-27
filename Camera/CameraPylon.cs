using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using HalconDotNet;
using Basler.Pylon;

namespace Camera
{
    public class CameraPylon : Basler.Pylon.Camera, ICamera
    {
        //public struct IntegerParameter
        //{
        //    public long Increment { get; }
        //    public long Maximum { get; }
        //    public long Minimum { get; }
        //    public long Value { get; set; }
        //}


        //IntegerParameter w = new IntegerParameter();
        public int Index { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }

        public long Width
        {
            get { return Parameters[PLCamera.Width].GetValue(); }
            set { Parameters[PLCamera.Width].SetValue(value); }
        }
        public long Height
        {
            get { return Parameters[PLCamera.Height].GetValue(); }
            set { Parameters[PLCamera.Height].SetValue(value); }
        }
        public string PixelType { get; set; }
        public long ExposureTime { get; set; }//曝光时间
        public long Gain { get; set; }//增益
        public long BlackLevel { get; set; }//黑电平
        public long Gamma { get; set; }//伽马


        int timeoutMs = 5000;


        public event EventHandler<EventArgs> ImageProcessing;


        public CameraPylon()
        {

        }


        public CameraPylon(string serialNumber) : base(serialNumber)
        {

        }


        public new bool Open()
        {
            if (IsOpen)
            {
                if (Open(timeoutMs, TimeoutHandling.Return))
                {
                    PixelType = Parameters[PLCamera.PixelFormat].GetValue();
                    Width = Parameters[PLCamera.Width].GetValue();
                    Height = Parameters[PLCamera.Height].GetValue();
                }
            }

            return IsOpen;
        }


        public bool GrabOne()
        {
            IGrabResult grabResult = StreamGrabber.GrabOne(timeoutMs, TimeoutHandling.Return);

            if (!grabResult.GrabSucceeded)
            {
                return false;
            }

            byte[] buffer = grabResult.PixelData as byte[];
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            HImage Image = new HImage("byte", grabResult.Width, grabResult.Height, ptr);
            ImageProcessing(Image, null);
            handle.Free();

            return true;
        }

        public async void GrabStartAsync()
        {
            if (StreamGrabber.IsGrabbing)
            {
                return;
            }

            await Task.Run(() =>
            {
                StreamGrabber.Start();

                while (StreamGrabber.IsGrabbing)
                {
                    IGrabResult grabResult = StreamGrabber.RetrieveResult(timeoutMs, TimeoutHandling.Return);

                    if (grabResult.GrabSucceeded)
                    {
                        byte[] buffer = grabResult.PixelData as byte[];
                        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                        IntPtr ptr = handle.AddrOfPinnedObject();
                        HImage Image = new HImage("byte", grabResult.Width, grabResult.Height, ptr);
                        ImageProcessing(Image, null);
                        handle.Free();
                    }
                }
            });
        }

        public void GrabStop()
        {
            StreamGrabber.Stop();
        }


        public dynamic GetParam(string name)
        {
            return null;
        }

        public void SetParam(string name, dynamic value)
        {

        }
    }
}
