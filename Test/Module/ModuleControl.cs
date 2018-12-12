using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Test
{
    public partial class ModuleControl : UserControl
    {
        public ModuleBase Module { get; set; }

        [Browsable(true)]
        [Description("模块名称")]
        public override string Text
        {
            get { return labText.Text; }
            set { labText.Text = value; }
        }

        [Browsable(true)]
        [Description("模块图标")]
        public Image Image
        {
            get { return labIcon.Image; }
            set { labIcon.Image = value; }
        }

        [Browsable(false)]
        public int Index
        {
            get
            {
                return Convert.ToInt32(labIndex.Text);
                //return Module.Index;
            }
            set
            {
                labIndex.Text = value.ToString();
                Module.Index = value;
            }
        }

        [Browsable(false)]
        public ModuleStatus Status
        {
            get
            {
                return Module.Status;

                //if (labStatus.Image.Equals(imgStatus.Images["Disable"]))
                //{
                //    return ModuleStatus.Disable;
                //}
                //else if (labStatus.Image.Equals(imgStatus.Images["OK"]))
                //{
                //    return ModuleStatus.OK;
                //}
                //else if (labStatus.Image.Equals(imgStatus.Images["NG"]))
                //{
                //    return ModuleStatus.NG;
                //}
                //else
                //{
                //    return ModuleStatus.Null;
                //}
            }
            set
            {
                Module.Status = value;

                switch (value)
                {
                    case ModuleStatus.Null:
                        this.BackColor = SystemColors.Control;
                        labStatus.Image = null;
                        break;
                    case ModuleStatus.Disable:
                        this.BackColor = SystemColors.ControlDark;
                        labStatus.Image = imgStatus.Images["Disable"];
                        break;
                    case ModuleStatus.OK:
                        labStatus.Image = imgStatus.Images["OK"];
                        break;
                    case ModuleStatus.NG:
                        labStatus.Image = imgStatus.Images["NG"];
                        break;
                    default:
                        break;
                }
            }
        }

        

        


        public ModuleControl(ModuleBase module)
        {
            Module = module;
            InitializeComponent();
            Text = Module.Name;
            Index = module.Index;
            Status = module.Status;
            //Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        }
        
        private void ModuleBase_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ModuleControl)))
            {
                e.Effect = e.AllowedEffect;

                this.BackColor = Color.LightBlue;
            }
        }

        private void ModuleBase_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ModuleControl)))
            {
                ModuleControl sourceControl = e.Data.GetData(typeof(ModuleControl)) as ModuleControl;

                if (sourceControl.Parent == null)
                {
                    ((ProcessControl)this.Parent).AddModule(sourceControl, this);
                }
                else
                {
                    ((ProcessControl)this.Parent).MoveModule(sourceControl, this);
                }

                #region
                //if (sourceControl.Parent == null)
                //{   // 新建控件
                //    foreach (ModuleControl item in ((ProcessControl)Parent).Controls)
                //    {
                //        if (item.Location.Y >= this.Location.Y)
                //        {
                //            item.UnitIndex++;
                //            item.Location = new Point(item.Location.X, item.Location.Y + item.Height);
                //        }
                //    }
                //    sourceControl.UnitIndex = this.UnitIndex - 1;
                //    sourceControl.Location = new Point(sourceControl.Location.X, this.Location.Y - this.Height);
                //    sourceControl.Parent = this.Parent;
                //}
                //else if (this.Location.Y < sourceControl.Location.Y)
                //{   // 源控件上移
                //    foreach (ModuleControl item in ((ProcessControl)Parent).Controls)
                //    {
                //        if (item.Location.Y >= this.Location.Y && item.Location.Y < sourceControl.Location.Y)
                //        {
                //            item.UnitIndex++;
                //            item.Location = new Point(item.Location.X, item.Location.Y + item.Height);
                //        }
                //    }
                //    sourceControl.UnitIndex = this.UnitIndex - 1;
                //    sourceControl.Location = new Point(sourceControl.Location.X, this.Location.Y - this.Height);
                //}
                //else if (this.Location.Y > sourceControl.Location.Y)
                //{   // 源控件下移
                //    foreach (ModuleControl item in ((ProcessControl)Parent).Controls)
                //    {
                //        if (item.Location.Y <= this.Location.Y && item.Location.Y > sourceControl.Location.Y)
                //        {
                //            item.UnitIndex--;
                //            item.Location = new Point(item.Location.X, item.Location.Y - item.Height);
                //        }
                //    }
                //    sourceControl.UnitIndex = this.UnitIndex + 1;
                //    sourceControl.Location = new Point(sourceControl.Location.X, this.Location.Y + this.Height);
                //}
                #endregion
            }
        }

        private void ModuleBase_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.DoDragDrop(this, DragDropEffects.Move);
            }

            if (e.X < 0 || e.X > this.Width || e.Y < 0 || e.Y > this.Height)
            {
                this.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                this.BackColor = Color.LightYellow;
            }
        }

        private void ModuleBase_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Module.ShowDialog();
        }

        private void ModuleBase_ParentChanged(object sender, EventArgs e)
        {
            int width = Parent.Width;
            if (((ProcessControl)Parent).VerticalScroll.Visible)
            {
                width -= 20;
            }

            Size = new Size(width, Size.Height);
        }

        private void ModuleControl_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.Control;
        }
    }
}
