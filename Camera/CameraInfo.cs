namespace Camera
{
    public class CameraInfo<T> where T : class
    {
        public T Instance { get; }

        public CameraInfo(T info)
        {
            Instance = info;
        }

        static public implicit operator CameraInfo<T>(T info)
        {
            return new CameraInfo<T>(info);
        }
    }
}
