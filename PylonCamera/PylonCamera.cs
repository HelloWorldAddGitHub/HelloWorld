using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Basler.Pylon;
using HalconDotNet;

namespace PylonCamera
{
    [DefaultEvent("ImageGrabbed")]
    public class PylonCamera : Component
    {
        public Camera Instance;


        private int timeoutMs = 5000;

        [Description("处理图像采集完成之后的事件")]
        public event EventHandler<ImageGrabbedEventArgs> ImageGrabbed;


        public PylonCamera()
        {
            
        }

        public PylonCamera(ICameraInfo cameraInfo)
        {
            Instance = new Camera(cameraInfo);
        }


        public static void Find()
        {
            CameraInfo.Clear();
            CameraInfo = CameraFinder.Enumerate();
        }


        public static List<ICameraInfo> CameraInfo { get; private set; } = new List<ICameraInfo>();


        public bool Open()
        {
            if (Instance == null)
            {
                Find();

                if (CameraInfo.Count > 0)
                {
                    Instance = new Camera(CameraInfo[0]);
                }
                else
                {
                    return false;
                }
            }

            return Instance.Open(timeoutMs, TimeoutHandling.Return);
        }


        public bool GrabOne()
        {
            IGrabResult grabResult = Instance.StreamGrabber.GrabOne(timeoutMs, TimeoutHandling.Return);

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
            if (Instance.StreamGrabber.IsGrabbing)
            {
                return;
            }

            await Task.Run(() =>
            {
                Instance.StreamGrabber.Start();

                while (Instance.StreamGrabber.IsGrabbing)
                {
                    IGrabResult grabResult = Instance.StreamGrabber.RetrieveResult(timeoutMs, TimeoutHandling.Return);

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
            Instance.StreamGrabber.Stop();
        }

        public void Close()
        {
            Instance.Close();
        }
    }
}
