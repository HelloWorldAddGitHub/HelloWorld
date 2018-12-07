using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Test.Module;
using System.Reflection;

namespace Test
{
    public partial class ToolBoxContent : DockContent
    {
        private MainForm mainForm;

        public ToolBoxContent(MainForm form)
        {
            mainForm = form;
            InitializeComponent();
        }
        

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ModuleControl).ToString(), true))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void ToolBox_Load(object sender, EventArgs e)
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            

            foreach (var item in types)
            {
                if (item.Name.IndexOf("Module") > 0)
                {
                    ModuleAttribute des = (ModuleAttribute)item.GetCustomAttribute(typeof(ModuleAttribute));
                    
                    if (treeView1.Nodes.Find(des.Category, false).Count() == 0)
                    {
                        treeView1.Nodes.Add(des.Category, des.Category);
                        treeView1.Nodes.Find(des.Category, false)[0].ImageIndex = 1;
                        treeView1.Nodes.Find(des.Category, false)[0].SelectedImageIndex = 1;
                    }

                    treeView1.Nodes.Find(des.Category, false)[0].Nodes.Add(des.Name);
                }
            }
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && treeView1.SelectedNode != null)
            {
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();


                foreach (var item in types)
                {
                    if (item.Name.IndexOf("Module") > 0)
                    {
                        ModuleAttribute des = (ModuleAttribute)item.GetCustomAttribute(typeof(ModuleAttribute));

                        if (des.Name == treeView1.SelectedNode.Text)
                        {
                            ModuleBase module = (ModuleBase)Assembly.GetExecutingAssembly().CreateInstance(item.FullName);
                            module.HWindow = mainForm.imageWindow.Window;
                            treeView1.DoDragDrop(new ModuleControl(module), DragDropEffects.Move);
                            return;
                        }
                    }
                }
            }
        }
    }
}
