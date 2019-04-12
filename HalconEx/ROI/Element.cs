using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace HalconEx.ROI
{
    public enum ROITypes { Regions, XLDs, Path }

    public class Element<T>
    {
        public void a()
        {
            HTuple windowHandle = 3600, row1, column1, row2, column2;
            //HDrawingObject
            HOperatorSet.DrawLine(windowHandle, out row1, out column1, out row2, out column2);
            HTuple drawID;
            HOperatorSet.CreateDrawingObjectLine(row1, column1, row2, column2, out drawID);

            HDrawingObject d = new HDrawingObject();
            d.CreateDrawingObjectCircle(100, 100, 80);
        }

        public Func<HTuple> GetWindowHandle;
    }

    public struct Quantity<T> where T : struct
    {
        public T Value;
        public string Unit;

        public static implicit operator Quantity<T>(T value)
        {
            Quantity<T> q = new Quantity<T>();
            return q.Value = value;
        }

        public static implicit operator T(Quantity<T> value)
        {
            return value.Value;
        }
    }



}
