using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class ProcessControl : TabPage
    {
        public ProcessBase Process { get; set; }

        public ProcessControl()
        {
            InitializeComponent();
        }

        private void TabProcess_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ModuleControl)))
            {
                e.Effect = e.AllowedEffect;
            }
        }

        private void TabProcess_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ModuleControl)))
            {
                #region
                //ModuleControl sourceControl = e.Data.GetData(typeof(ModuleControl)) as ModuleControl;

                //if (this.Controls.Count == 0)
                //{   // 没有控件
                //    sourceControl.UnitIndex = 1;
                //    sourceControl.Location = new Point(0, 0);
                //    sourceControl.Parent = this;

                //    return;
                //}

                //// 最后的控件
                //ModuleControl lastControl = this.Controls[0] as ModuleControl;
                //foreach (ModuleControl item in this.Controls)
                //{
                //    if (lastControl.Location.Y < item.Location.Y)
                //    {
                //        lastControl = item;
                //    }
                //}

                //if (lastControl.Location.Y > sourceControl.Location.Y  && sourceControl.Location.Y != -1)
                //{   // 移动控件
                //    foreach (ModuleControl item in this.Controls)
                //    {
                //        if (item.Location.Y > sourceControl.Location.Y)
                //        {
                //            item.UnitIndex--;
                //            item.Location = new Point(item.Location.X, item.Location.Y - item.Height);
                //        }
                //    }
                //    sourceControl.UnitIndex = lastControl.UnitIndex + 1;
                //    sourceControl.Location = new Point(sourceControl.Location.X, lastControl.Location.Y + lastControl.Height);
                //}
                //else if (sourceControl.Parent == null)
                //{   // 增加控件
                //    sourceControl.UnitIndex = lastControl.UnitIndex + 1;
                //    sourceControl.Location = new Point(sourceControl.Location.X, lastControl.Location.Y + lastControl.Height);
                //    sourceControl.Parent = this;

                //}
                #endregion


                ModuleControl sourceControl = e.Data.GetData(typeof(ModuleControl)) as ModuleControl;

                if (sourceControl.Parent == null)
                {
                    AddModule(sourceControl);
                }
                else
                {
                    MoveModule(sourceControl);
                }

            }
        }

        public void AddModule(ModuleControl sourceControl, ModuleControl targetControl = null)
        {
            if (targetControl == null)
            {
                ModuleControl lastControl = LastModule();

                if (lastControl == null)
                {
                    sourceControl.Index = 1;
                    sourceControl.Location = new Point(0, 0);
                }
                else
                {
                    sourceControl.Index = lastControl.Index + 1;
                    sourceControl.Location = new Point(sourceControl.Location.X, lastControl.Location.Y + lastControl.Height);
                }

                sourceControl.Parent = this;

                //Process.Add(sourceControl.Module);
            }
            else
            {
                foreach (ModuleControl item in Controls)
                {
                    if (item.Location.Y >= targetControl.Location.Y)
                    {
                        item.Index++;
                        item.Location = new Point(item.Location.X, item.Location.Y + item.Height);
                    }
                }
                sourceControl.Index = targetControl.Index - 1;
                sourceControl.Location = new Point(sourceControl.Location.X, targetControl.Location.Y - targetControl.Height);

                sourceControl.Parent = this;

                //Process.Insert(sourceControl.UnitIndex, sourceControl.Module);
            }
            
        }

        public void MoveModule(ModuleControl sourceControl, ModuleControl targetControl = null)
        {
            if (targetControl == null)
            {
                ModuleControl lastControl = LastModule();
                
                if (lastControl != sourceControl)
                {
                    foreach (ModuleControl item in Controls)
                    {
                        if (item.Location.Y > sourceControl.Location.Y)
                        {
                            item.Index--;
                            item.Location = new Point(item.Location.X, item.Location.Y - item.Height);
                        }
                    }

                    sourceControl.Index = lastControl.Index + 1;
                    sourceControl.Location = new Point(sourceControl.Location.X, lastControl.Location.Y + lastControl.Height);
                    
                }
            }
            else
            {
                if (targetControl.Location.Y < sourceControl.Location.Y)
                {   // 源控件上移
                    foreach (ModuleControl item in Controls)
                    {
                        if (item.Location.Y >= targetControl.Location.Y && item.Location.Y < sourceControl.Location.Y)
                        {
                            item.Index++;
                            item.Location = new Point(item.Location.X, item.Location.Y + item.Height);
                        }
                    }
                    sourceControl.Index = targetControl.Index - 1;
                    sourceControl.Location = new Point(sourceControl.Location.X, targetControl.Location.Y - targetControl.Height);
                }
                else if (targetControl.Location.Y > sourceControl.Location.Y)
                {   // 源控件下移
                    foreach (ModuleControl item in Controls)
                    {
                        if (item.Location.Y <= targetControl.Location.Y && item.Location.Y > sourceControl.Location.Y)
                        {
                            item.Index--;
                            item.Location = new Point(item.Location.X, item.Location.Y - item.Height);
                        }
                    }
                    sourceControl.Index = targetControl.Index + 1;
                    sourceControl.Location = new Point(sourceControl.Location.X, targetControl.Location.Y + targetControl.Height);
                }
            }

            //Process.Sort();
        }

        public void RemoveModule(ModuleControl sourceControl)
        {
            foreach (ModuleControl item in this.Controls)
            {
                if (sourceControl.Location.Y < item.Location.Y)
                {
                    item.Index--;
                    item.Location = new Point(item.Location.X, item.Location.Y - item.Height);
                }
            }

            //Process.Remove(sourceControl.Module);

            sourceControl.Dispose();
        }

        public ModuleControl LastModule()
        {
            if (this.Controls.Count == 0)
            {
                return null;
            }

            ModuleControl lastControl = Controls[0] as ModuleControl;
            foreach (ModuleControl item in Controls)
            {
                if (lastControl.Location.Y < item.Location.Y)
                {
                    lastControl = item;
                }
            }

            return lastControl;
        }

        private void ProcessControl_SizeChanged(object sender, EventArgs e)
        {
            int width = Width;
            if (this.VerticalScroll.Visible)
            {
                width -= 20;
            }
            
            foreach (ModuleControl item in Controls)
            {
                item.Size = new Size(width, item.Height);
            }
        }
    }
}
