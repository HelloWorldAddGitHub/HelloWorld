using System.Collections.Generic;

namespace Camera
{
    public class CameraInfoList<T> where T : class
    {
        public List<CameraInfo<T>> Cameras { get; } = new List<CameraInfo<T>>();

        public CameraInfoList(List<T> info)
        {
            foreach (var item in info)
            {
                Cameras.Add(item);
            }
        }

        public CameraInfoList(T[] info)
        {
            foreach (var item in info)
            {
                Cameras.Add(item);
            }
        }

        static public implicit operator CameraInfoList<T>(List<T> info)
        {
            return new CameraInfoList<T>(info);
        }

        static public implicit operator CameraInfoList<T>(T[] info)
        {
            return new CameraInfoList<T>(info);
        }
    }
}
