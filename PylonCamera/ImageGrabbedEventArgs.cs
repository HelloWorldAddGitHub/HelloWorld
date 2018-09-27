using System;
using System.Drawing;
using HalconDotNet;

namespace PylonCamera
{
    public class ImageGrabbedEventArgs : EventArgs
    {
        public ImageGrabbedEventArgs(HObject image)
        {
            Image = image;
        }

        public HObject Image { get; }

        public Size Size
        {
            get
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(Image, out width, out height);
                return new Size(width, height);
            }
        }

        public string Type
        {
            get
            {
                HTuple type;
                HOperatorSet.GetImageType(Image, out type);
                return type;
            }
        }

        public DateTime Time
        {
            get
            {
                HTuple MSecond, second, minute, hour, day, YDay, month, year;
                HOperatorSet.GetImageTime(Image, 
                    out MSecond, out second, out minute, out hour, out day, out YDay, out month, out year);
                return new DateTime(year, month, day, hour, minute, second, MSecond);
            }
        }
    }
}
