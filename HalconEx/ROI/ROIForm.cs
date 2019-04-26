using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            Call = Operator;
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

            //HTuple row1, column1, row2, column2;
            //HOperatorSet.SetColor(WindowHandle, "red");
            //HOperatorSet.DrawLine(WindowHandle, out row1, out column1, out row2, out column2);
            //HDrawingObject line = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.LINE, row1, column1, row2, column2);
            //HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, line);

            Shape shape = CreateShape(ShapeTypes.line);

            if (Rois[node.Text].Count == 0)
            {
                shape.Operation = Operations.none;
            }
            Rois[node.Text].Add(shape);

            node.Nodes.Add("线段");
            //Rois[node.Text].Add(new Shape(line, ShapeTypes.line));
        }

        private void tsbDrawCircle_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            //HTuple row, column, radius;
            //HOperatorSet.SetColor(WindowHandle, "red");
            //HOperatorSet.DrawCircle(WindowHandle, out row, out column, out radius);
            //HDrawingObject circle = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.CIRCLE, row, column, radius);
            //HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, circle);

            Shape shape = CreateShape(ShapeTypes.circle);

            if (Rois[node.Text].Count == 0)
            {
                shape.Operation = Operations.none;
            }
            Rois[node.Text].Add(shape);

            node.Nodes.Add("圆形");
            //Rois[node.Text].Add(new Shape(circle, ShapeTypes.circle));
        }

        private void tsbDrawArc_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            //HTuple row, column, radius;
            //HOperatorSet.SetColor(WindowHandle, "red");
            //HOperatorSet.DrawCircle(WindowHandle, out row, out column, out radius);
            //HDrawingObject circleArc = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.CIRCLE_SECTOR, row, column, radius, 0, 3.14159);
            //HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, circleArc);

            Shape shape = CreateShape(ShapeTypes.circ_arc);

            if (Rois[node.Text].Count == 0)
            {
                shape.Operation = Operations.none;
            }
            Rois[node.Text].Add(shape);

            node.Nodes.Add("圆弧");
            //Rois[node.Text].Add(new Shape(circleArc, ShapeTypes.circ_arc));
        }

        private void tsbDrawEllipse_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            //HTuple row, column, phi, radius1, radius2;
            //HOperatorSet.SetColor(WindowHandle, "red");
            //HOperatorSet.DrawEllipse(WindowHandle, out row, out column, out phi, out radius1, out radius2);
            //HDrawingObject ellipse = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.ELLIPSE, row, column, phi, radius1, radius2);
            //HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, ellipse);

            Shape shape = CreateShape(ShapeTypes.ellipse);

            if (Rois[node.Text].Count == 0)
            {
                shape.Operation = Operations.none;
            }
            Rois[node.Text].Add(shape);

            node.Nodes.Add("椭圆");
            //Rois[node.Text].Add(new Shape(ellipse, ShapeTypes.ellipse));
        }

        private void tsbDrawEllipseArc_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            //HTuple row, column, phi, radius1, radius2;
            //HOperatorSet.SetColor(WindowHandle, "red");
            //HOperatorSet.DrawEllipse(WindowHandle, out row, out column, out phi, out radius1, out radius2);
            //HDrawingObject ellipseArc = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.ELLIPSE_SECTOR, row, column, phi, radius1, radius2, 0, 3.14159);
            //HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, ellipseArc);

            Shape shape = CreateShape(ShapeTypes.ellip_arc);

            if (Rois[node.Text].Count == 0)
            {
                shape.Operation = Operations.none;
            }
            Rois[node.Text].Add(shape);

            node.Nodes.Add("椭圆弧");
            //Rois[node.Text].Add(new Shape(ellipseArc, ShapeTypes.ellip_arc));
        }

        private void tsbDrawRectangle1_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            //HTuple row1, column1, row2, column2;
            //HOperatorSet.SetColor(WindowHandle, "red");
            //HOperatorSet.DrawRectangle1(WindowHandle, out row1, out column1, out row2, out column2);
            //HDrawingObject rectangle1 = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.RECTANGLE1, row1, column1, row2, column2);
            //HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, rectangle1);

            Shape shape = CreateShape(ShapeTypes.rect1);

            if (Rois[node.Text].Count == 0)
            {
                shape.Operation = Operations.none;
            }
            Rois[node.Text].Add(shape);

            node.Nodes.Add("轴平行矩形");
            //Rois[node.Text].Add(new Shape(rectangle1, ShapeTypes.rect1));
        }

        private void tsbDrawRectangle2_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            //HTuple row, column, phi, lenght1, lenght2;
            //HOperatorSet.SetColor(WindowHandle, "red");
            //HOperatorSet.DrawRectangle2(WindowHandle, out row, out column, out phi, out lenght1, out lenght2);
            //HDrawingObject rectangle2 = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.RECTANGLE2, row, column, phi, lenght1, lenght2);
            //HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, rectangle2);

            Shape shape = CreateShape(ShapeTypes.rect2);

            if (Rois[node.Text].Count == 0)
            {
                shape.Operation = Operations.none;
            }
            Rois[node.Text].Add(shape);

            node.Nodes.Add("旋转矩形");
            //Rois[node.Text].Add(new Shape(rectangle2, ShapeTypes.rect2));
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

            //HObject contOut;
            //HTuple rows, columns, weights;
            //HOperatorSet.SetColor(WindowHandle, "red");
            //HOperatorSet.DrawNurbs(out contOut, WindowHandle, "true", "true", "true", "true", 3, out rows, out columns, out weights);
            //HOperatorSet.GetContourXld(contOut, out rows, out columns);
            //contOut.Dispose();
            //HDrawingObject nurbs = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.XLD_CONTOUR, rows, columns);
            //HOperatorSet.AttachDrawingObjectToWindow(WindowHandle, nurbs);

            Shape shape = CreateShape(ShapeTypes.nurbs);
            Rois[node.Text].Add(shape);
            node.Nodes.Add("NURBS曲线");
            //Rois[node.Text].Add(new Shape(nurbs, ShapeTypes.nurbs));
        }

        private void tsbDrawNurbsInterp_Click(object sender, EventArgs e)
        {
            TreeNode node = GetRootNode();
            if (node == null)
            {
                return;
            }

            Shape shape = CreateShape(ShapeTypes.nurbs_interp);
            Rois[node.Text].Add(shape);
            node.Nodes.Add("可内插的NURBS曲线");
        }



        private Shape CreateShape(ShapeTypes type, HDrawingObject mod = null)
        {
            Shape shape;
            HDrawingObject draw = null;
            HOperatorSet.SetColor(WindowHandle, "red");

            switch (type)
            {
                case ShapeTypes.line:
                    HTuple row1, column1, row2, column2;
                    HOperatorSet.DrawLine(WindowHandle, out row1, out column1, out row2, out column2);
                    draw = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.LINE, row1, column1, row2, column2);
                    //shape = new Shape(draw, type);
                    //WindowHandle?.AttachDrawingObjectToWindow(draw);
                    break;
                case ShapeTypes.circle:
                    HTuple row, column, radius;
                    HOperatorSet.DrawCircle(WindowHandle, out row, out column, out radius);
                    draw = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.CIRCLE, row, column, radius);
                    //shape = new Shape(draw, type);
                    //WindowHandle?.AttachDrawingObjectToWindow(draw);
                    break;
                case ShapeTypes.circ_arc:
                    HOperatorSet.DrawCircle(WindowHandle, out row, out column, out radius);
                    draw = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.CIRCLE_SECTOR, row, column, radius, 0, 3.14159);
                    //shape = new Shape(draw, type);
                    //WindowHandle?.AttachDrawingObjectToWindow(draw);
                    break;
                case ShapeTypes.ellipse:
                    HTuple phi, radius1, radius2;
                    HOperatorSet.DrawEllipse(WindowHandle, out row, out column, out phi, out radius1, out radius2);
                    draw = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.ELLIPSE, row, column, phi, radius1, radius2);
                    //shape = new Shape(draw, type);
                    //WindowHandle?.AttachDrawingObjectToWindow(draw);
                    break;
                case ShapeTypes.ellip_arc:
                    HOperatorSet.DrawEllipse(WindowHandle, out row, out column, out phi, out radius1, out radius2);
                    draw = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.ELLIPSE_SECTOR, row, column, phi, radius1, radius2, 0, 3.14159);
                    //shape = new Shape(draw, type);
                    //WindowHandle?.AttachDrawingObjectToWindow(draw);
                    break;
                case ShapeTypes.rect1:
                    HOperatorSet.DrawRectangle1(WindowHandle, out row1, out column1, out row2, out column2);
                    draw = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.RECTANGLE1, row1, column1, row2, column2);
                    //shape = new Shape(draw, type);
                    //WindowHandle?.AttachDrawingObjectToWindow(draw);
                    break;
                case ShapeTypes.rect2:
                    HTuple lenght1, lenght2;
                    HOperatorSet.DrawRectangle2(WindowHandle, out row, out column, out phi, out lenght1, out lenght2);
                    draw = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.RECTANGLE2, row, column, phi, lenght1, lenght2);
                    //shape = new Shape(draw, type);
                    //WindowHandle?.AttachDrawingObjectToWindow(draw);
                    break;
                case ShapeTypes.arb_xld:
                    HObject obj;
                    HTuple rows, columns;
                    HOperatorSet.DrawXld(out obj, WindowHandle, "true", "true", "true", "true");
                    //HOperatorSet.GetContourXld(obj, out rows, out columns);
                    
                    //shape = new Shape(obj, type);
                    //WindowHandle?.DispObj(obj);
                    break;
                case ShapeTypes.nurbs:
                    HTuple weights;
                    HOperatorSet.DrawNurbs(out obj, WindowHandle, "true", "true", "true", "true", 3, out rows, out columns, out weights);
                    
                    //shape = new Shape(obj, type);
                    //WindowHandle?.DispObj(obj);
                    break;
                case ShapeTypes.nurbs_interp:
                    HTuple controlRows, controlCols, knots, tangents;
                    HOperatorSet.DrawNurbsInterp(out obj, WindowHandle, "true", "true", "true", "true", 3, out controlRows, out controlCols, out knots, out rows, out columns, out tangents);
                    
                    //shape = new Shape(obj, type);
                    //WindowHandle?.DispObj(obj);
                    break;
                default:
                    shape = null;
                    break;
            }
            //shape.HDrawingObject.SetDrawingObjectXld(new HXLDCont(new HTuple(0, 1, 2, 2, 2), new HTuple(0, 0, 0, 1, 2)));
            //shape.HDrawingObject.SetDrawingObjectXld(null);

            //shape.HDrawingObject.SetDrawingObjectCallback(new HTuple("resize"), new HTuple(call));



            shape = new Shape(draw, type);
            WindowHandle?.AttachDrawingObjectToWindow(draw);

            shape.HDrawingObject.OnAttach(Call);
            shape.HDrawingObject.OnDrag(Call);
            shape.HDrawingObject.OnResize(Call);
            shape.HDrawingObject.OnSelect(Call);

            //IntPtr ptrCall = Marshal.GetFunctionPointerForDelegate(Call);
            //shape.HDrawingObject.SetDrawingObjectCallback("on_attach", ptrCall);
            //shape.HDrawingObject.SetDrawingObjectCallback("on_drag", ptrCall);
            //shape.HDrawingObject.SetDrawingObjectCallback("on_resize", ptrCall);
            //shape.HDrawingObject.SetDrawingObjectCallback("on_select", ptrCall);
            return shape;
        }

        HDrawingObject.HDrawingObjectCallbackClass Call;
        
        public void Operator(HDrawingObject drawid, HWindow window, string type)
        {
            //HDrawingObject obj = new HDrawingObject(drawid);
            //HRegion region = new HRegion(drawid.GetDrawingObjectIconic());
            HOperatorSet.SetSystem("flush_graphic", "false");
            WindowHandle?.ClearWindow();
            foreach (var roi in Rois.Values)
            {
                HObject region1 = null;
                //HOperatorSet.SetColor(WindowHandle, "gray");
                WindowHandle?.SetColor("khaki");
                //foreach (var shape in roi)
                for (int i = 0; i < roi.Count; i++)
                {
                    Shape shape = roi[i];
                    //HRegion region = new HRegion(shape.HDrawingObject.GetDrawingObjectIconic());
                    shape.HDrawingObject.SetDrawingObjectParams("color", "khaki");
                    HObject region2 = shape.HDrawingObject.GetDrawingObjectIconic();

                    if (Rois.Type == ROITypes.region && shape.Complement)
                    {
                        HOperatorSet.Complement(region2, out region2);
                    }

                    if (i != 0)
                    {
                        switch (shape.Operation)
                        {
                            case Operations.union:
                                HOperatorSet.Union2(region1, region2, out region2);
                                break;
                            case Operations.intersection:
                                HOperatorSet.Intersection(region1, region2, out region2);
                                break;
                            case Operations.difference:
                                HOperatorSet.Difference(region1, region2, out region2);
                                break;
                            case Operations.xor:
                                HOperatorSet.SymmDifference(region1, region2, out region2);
                                break;
                            case Operations.none:
                                break;
                            default:
                                break;
                        }
                    }

                    region1 = region2;
                }

                //HOperatorSet.SetColored(WindowHandle, 12);
                drawid.SetDrawingObjectParams("color", "gold");
                //WindowHandle?.SetColored(12);
                //WindowHandle?.SetColor("gold");
                WindowHandle?.DispObj(region1);
                HOperatorSet.SetSystem("flush_graphic", "true");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dataGridView1.Rows.Clear();

            if (treeView1.SelectedNode.Level == 0)
            {

            }
            else
            {
                TreeNode node = GetRootNode();
                Shape shape = Rois[node.Text][node.Index];


                DataGridViewTextBoxCell name = new DataGridViewTextBoxCell();
                name.Value = nameof(shape.Operation);

                DataGridViewComboBoxCell value = new DataGridViewComboBoxCell();
                value.DataSource = Enum.GetNames(typeof(Operations));
                value.Value = Enum.GetName(typeof(Operations), shape.Operation);
                
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.AddRange(name, value);
                dataGridView1.Rows.Add(row);


                name = new DataGridViewTextBoxCell();
                name.Value = nameof(shape.Complement);

                value = new DataGridViewComboBoxCell();
                value.DataSource = new string[] { "True", "False" };
                value.Value = shape.Complement ? "True" : "False";
                
                row = new DataGridViewRow();
                row.Cells.AddRange(name, value);
                dataGridView1.Rows.Add(row);
            }
        }
    }
}
