using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace HalconEx.ROI
{
    public class ROICollection : Dictionary<string, List<Shape>>
    {
        public ROITypes Type { get; set; }
    }


    public class Shape
    {
        public Shape()
        {

        }

        public Shape(object draw, ShapeTypes type)
        {
            if (draw is HDrawingObject)
            {
                HDrawingObject = draw as HDrawingObject;
            }
            else if (draw is HObject)
            {
                HObject = draw as HObject;
            }

        }

        public Shape(object draw, ShapeTypes type, Operations operation)
        {
            if (draw is HDrawingObject)
            {
                HDrawingObject = draw as HDrawingObject;
            }
            else if (draw is HObject)
            {
                HObject = draw as HObject;
            }

            Operation = operation;
        }

        public Shape(object draw, ShapeTypes type, Operations operation, bool complement)
        {
            if (draw is HDrawingObject)
            {
                HDrawingObject = draw as HDrawingObject;
            }
            else if (draw is HObject)
            {
                HObject = draw as HObject;
            }

            Operation = operation;
            IsComplement = complement;
        }

        public ShapeTypes Type { get; set; }

        public Operations Operation { get; set; }

        public bool IsComplement { get; set; }

        public HObject HObject;

        public HDrawingObject HDrawingObject;
    }


    public enum ROITypes { region, xld, path }

    public enum Operations { union, intersection, difference, xor, none }

    public enum ShapeTypes { line, circle, circ_arc, ellipse, ellip_arc, rect1, rect2, arb_xld, nurbs, nurbs_interp }
}
