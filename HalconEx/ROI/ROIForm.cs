using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace HalconEx.ROI
{
    public partial class ROIForm : Form
    {
        public HWindow WindowHandle { get; private set; }

        public ROICollection Rois = new ROICollection();

        public ROIForm(HWindow windowHandle)
        {
            InitializeComponent();
            WindowHandle = windowHandle;
        }

        private TreeNode GetRootNode()
        {
            if (treeView1.SelectedNode == null)
            {
                return null;
            }

            TreeNode node = treeView1.SelectedNode;
            int lenght = node.Level;

            for (int i = 0; i < lenght; i++)
            {
                node = node.Parent;
            }

            return node;
        }

        private void ROI_Load(object sender, EventArgs e)
        {

        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; ; i++)
            {
                string name = $"ROI[{i}]";

                for (int j = 0; j <= treeView1.Nodes.Count; j++)
                {
                    if (j == treeView1.Nodes.Count)
                    {
                        treeView1.Nodes.Add(name);
                        Rois.Add(name, new List<Shape>());
                        return;
                    }

                    if (treeView1.Nodes[j].Text == name)
                    {
                        break;
                    }
                }
            }
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void tspDrawLine_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            HTuple row1, column1, row2, column2;
            HOperatorSet.SetColor(WindowHandle, "red");
            HOperatorSet.DrawLine(WindowHandle, out row1, out column1, out row2, out column2);
            HDrawingObject line = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.LINE, row1, column1, row2, column2);
            HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, line);

            node.Nodes.Add("线段");
            Rois[node.Text].Add(new Shape(line, ShapeTypes.line));
        }

        private void tsbDrawCircle_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            HTuple row, column, radius;
            HOperatorSet.SetColor(WindowHandle, "red");
            HOperatorSet.DrawCircle(WindowHandle, out row, out column, out radius);
            HDrawingObject circle = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.CIRCLE, row, column, radius);
            HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, circle);

            node.Nodes.Add("圆形");
            Rois[node.Text].Add(new Shape(circle, ShapeTypes.circle));
        }

        private void tsbDrawArc_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            HTuple row, column, radius;
            HOperatorSet.SetColor(WindowHandle, "red");
            HOperatorSet.DrawCircle(WindowHandle, out row, out column, out radius);
            HDrawingObject circleArc = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.CIRCLE_SECTOR, row, column, radius, 0, 3.14159);
            HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, circleArc);

            node.Nodes.Add("圆弧");
            Rois[node.Text].Add(new Shape(circleArc, ShapeTypes.circ_arc));
        }

        private void tsbDrawEllipse_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            HTuple row, column, phi, radius1, radius2;
            HOperatorSet.SetColor(WindowHandle, "red");
            HOperatorSet.DrawEllipse(WindowHandle, out row, out column, out phi, out radius1, out radius2);
            HDrawingObject ellipse = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.ELLIPSE, row, column, phi, radius1, radius2);
            HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, ellipse);

            node.Nodes.Add("椭圆");
            Rois[node.Text].Add(new Shape(ellipse, ShapeTypes.ellipse));
        }

        private void tsbDrawEllipseArc_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            HTuple row, column, phi, radius1, radius2;
            HOperatorSet.SetColor(WindowHandle, "red");
            HOperatorSet.DrawEllipse(WindowHandle, out row, out column, out phi, out radius1, out radius2);
            HDrawingObject ellipseArc = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.ELLIPSE_SECTOR, row, column, phi, radius1, radius2, 0, 3.14159);
            HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, ellipseArc);

            node.Nodes.Add("椭圆弧");
            Rois[node.Text].Add(new Shape(ellipseArc, ShapeTypes.ellip_arc));
        }

        private void tsbDrawRectangle1_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            HTuple row1, column1, row2, column2;
            HOperatorSet.SetColor(WindowHandle, "red");
            HOperatorSet.DrawRectangle1(WindowHandle, out row1, out column1, out row2, out column2);
            HDrawingObject rectangle1 = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.RECTANGLE1, row1, column1, row2, column2);
            HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, rectangle1);

            node.Nodes.Add("轴平行矩形");
            Rois[node.Text].Add(new Shape(rectangle1, ShapeTypes.rect1));
        }

        private void tsbDrawRectangle2_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            HTuple row, column, phi, lenght1, lenght2;
            HOperatorSet.SetColor(WindowHandle, "red");
            HOperatorSet.DrawRectangle2(WindowHandle, out row, out column, out phi, out lenght1, out lenght2);
            HDrawingObject rectangle2 = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.RECTANGLE2, row, column, phi, lenght1, lenght2);
            HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, rectangle2);

            node.Nodes.Add("旋转矩形");
            Rois[node.Text].Add(new Shape(rectangle2, ShapeTypes.rect2));
        }

        private void tsbDrawPolygon_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            HObject contOut;
            HTuple rows, columns;
            HOperatorSet.SetColor(WindowHandle, "red");
            HOperatorSet.DrawXld(out contOut, WindowHandle, "true", "true", "true", "true");
            HOperatorSet.GetContourXld(contOut, out rows, out columns);
            contOut.Dispose();
            HDrawingObject xld = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.XLD_CONTOUR, rows, columns);
            HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, xld);

            node.Nodes.Add("任意XLD轮廓");
            Rois[node.Text].Add(new Shape(xld, ShapeTypes.arb_xld));
        }

        private void tsbDrawNurbs_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            HObject contOut;
            HTuple rows, columns, weights;
            HOperatorSet.SetColor(WindowHandle, "red");
            HOperatorSet.DrawNurbs(out contOut, WindowHandle, "true", "true", "true", "true", 3, out rows, out columns, out weights);
            HOperatorSet.GetContourXld(contOut, out rows, out columns);
            contOut.Dispose();
            HDrawingObject nurbs = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.XLD_CONTOUR, rows, columns);
            HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, nurbs);

            node.Nodes.Add("NURBS曲线");
            Rois[node.Text].Add(new Shape(nurbs, ShapeTypes.nurbs));
        }

        private void tsbDrawNurbsInterp_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            CreateDrawObject(ShapeTypes.nurbs_interp);
            node.Nodes.Add("可内插的NURBS曲线");
        }



        private void CreateDrawObject(ShapeTypes type, HDrawingObject mod = null)
        {
            Shape shape;
            HObject obj;
            HDrawingObject draw;

            HOperatorSet.SetColor(WindowHandle, "red");

            switch (type)
            {
                case ShapeTypes.line:
                    break;
                case ShapeTypes.circle:
                    break;
                case ShapeTypes.circ_arc:
                    break;
                case ShapeTypes.ellipse:
                    break;
                case ShapeTypes.ellip_arc:
                    break;
                case ShapeTypes.rect1:
                    break;
                case ShapeTypes.rect2:
                    break;
                case ShapeTypes.arb_xld:
                    break;
                case ShapeTypes.nurbs:
                    HTuple rows, columns, weights;
                    HOperatorSet.DrawNurbs(out obj, WindowHandle, "true", "true", "true", "true", 3, out rows, out columns, out weights);
                    shape = new Shape(obj, type);
                    break;
                case ShapeTypes.nurbs_interp:
                    HTuple controlRows, controlCols, knots, tangents;
                    HOperatorSet.DrawNurbsInterp(out obj, WindowHandle, "true", "true", "true", "true", 3, out controlRows, out controlCols, out knots, out rows, out columns, out tangents);
                    shape = new Shape(obj, type);
                    break;
                default:
                    break;
            }
        }
    }
}
