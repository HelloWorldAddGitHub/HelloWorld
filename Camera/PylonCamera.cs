using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Basler.Pylon;
using HalconDotNet;

namespace HalconCamera
{
    public class PylonCamera : Camera, ICamera
    {
        //public struct IntegerParameter
        //{
        //    public long Increment { get; }
        //    public long Maximum { get; }
        //    public long Minimum { get; }
        //    public long Value { get; set; }
        //}


        //IntegerParameter w = new IntegerParameter();
        //public int Index { get; set; }
        //public string Name { get; set; }
        //public string SerialNumber { get; set; }

        //public long Width
        //{
        //    get { return Parameters[PLCamera.Width].GetValue(); }
        //    set { Parameters[PLCamera.Width].SetValue(value); }
        //}
        //public long Height
        //{
        //    get { return Parameters[PLCamera.Height].GetValue(); }
        //    set { Parameters[PLCamera.Height].SetValue(value); }
        //}
        //public string PixelType { get; set; }
        //public long ExposureTime { get; set; }//曝光时间
        //public long Gain { get; set; }//增益
        //public long BlackLevel { get; set; }//黑电平
        //public long Gamma { get; set; }//伽马


        private int timeoutMs = 5000;
        

        public event EventHandler<ImageGrabbedEventArgs> ImageGrabbed;



        public PylonCamera(ICameraInfo cameraInfo) : base(cameraInfo)
        {
            
        }


        public new bool Open()
        {
            return Open(timeoutMs, TimeoutHandling.Return);
        }


        public bool GrabOne()
        {
            IGrabResult grabResult = StreamGrabber.GrabOne(timeoutMs, TimeoutHandling.Return);

            if (!grabResult.GrabSucceeded)
            {
                return false;
            }

            if (grabResult.PixelTypeValue == PixelType.Mono8)
            {
                byte[] buffer = grabResult.PixelData as byte[];
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                IntPtr ptr = handle.AddrOfPinnedObject();
                HImage image = new HImage("byte", grabResult.Width, grabResult.Height, ptr);
                handle.Free();

                ImageGrabbed?.Invoke(this, new ImageGrabbedEventArgs(image));
            }
            
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
                        if (grabResult.PixelTypeValue == PixelType.Mono8)
                        {
                            byte[] buffer = grabResult.PixelData as byte[];
                            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                            IntPtr ptr = handle.AddrOfPinnedObject();
                            HImage image = new HImage("byte", grabResult.Width, grabResult.Height, ptr);
                            handle.Free();

                            ImageGrabbed?.Invoke(this, new ImageGrabbedEventArgs(image));
                        }
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
