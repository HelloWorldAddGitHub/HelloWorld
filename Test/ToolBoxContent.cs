using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Module;
using WeifenLuo.WinFormsUI.Docking;

namespace Test
{
    public partial class ToolBoxContent : DockContent
    {
        private MainForm mainForm;

        Dictionary<string, Assembly> assembly = new Dictionary<string, Assembly>();

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
            DirectoryInfo info = new DirectoryInfo(Application.StartupPath);
            FileInfo[] files = info.GetFiles("*Module.dll");

            foreach (var file in files)
            {
                Assembly a = Assembly.LoadFile(file.FullName);

                foreach (var type in a.GetExportedTypes())
                {
                    ModuleAttribute attribute = (ModuleAttribute)type.GetCustomAttribute(typeof(ModuleAttribute));

                    if (attribute != null)
                    {
                        assembly.Add(attribute.Name, a);

                        if (treeView1.Nodes.Find(attribute.Category, false).Count() == 0)
                        {
                            treeView1.Nodes.Add(attribute.Category, attribute.Category);
                            treeView1.Nodes.Find(attribute.Category, false)[0].ImageIndex = 1;
                            treeView1.Nodes.Find(attribute.Category, false)[0].SelectedImageIndex = 1;
                        }

                        treeView1.Nodes.Find(attribute.Category, false)[0].Nodes.Add(attribute.Name);

                        break;
                    }
                }
            }











            //Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            

            //foreach (var item in types)
            //{
            //    if (item.Name.IndexOf("Module") > 0)
            //    {
            //        ModuleAttribute des = (ModuleAttribute)item.GetCustomAttribute(typeof(ModuleAttribute));
                    
            //        if (treeView1.Nodes.Find(des.Category, false).Count() == 0)
            //        {
            //            treeView1.Nodes.Add(des.Category, des.Category);
            //            treeView1.Nodes.Find(des.Category, false)[0].ImageIndex = 1;
            //            treeView1.Nodes.Find(des.Category, false)[0].SelectedImageIndex = 1;
            //        }

            //        treeView1.Nodes.Find(des.Category, false)[0].Nodes.Add(des.Name);
            //    }
            //}
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && treeView1.SelectedNode != null && treeView1.SelectedNode.Level > 0)
            {
                Assembly a = assembly[treeView1.SelectedNode.Text];
                Type[] types = a.GetExportedTypes();

                foreach (var type in types)
                {
                    ModuleAttribute attribute = (ModuleAttribute)type.GetCustomAttribute(typeof(ModuleAttribute));

                    if (attribute != null)
                    {
                        ModuleBase module = (ModuleBase)a.CreateInstance(type.FullName);
                        module.HWindow = mainForm.imageWindow.Window;
                        treeView1.DoDragDrop(new ModuleControl(module), DragDropEffects.Move);
                        return;
                    }
                }



                //Type[] types = Assembly.GetExecutingAssembly().GetTypes();


                //foreach (var item in types)
                //{
                //    if (item.Name.IndexOf("Module") > 0)
                //    {
                //        ModuleAttribute des = (ModuleAttribute)item.GetCustomAttribute(typeof(ModuleAttribute));

                //        if (des.Name == treeView1.SelectedNode.Text)
                //        {
                //            ModuleBase module = (ModuleBase)Assembly.GetExecutingAssembly().CreateInstance(item.FullName);
                //            module.HWindow = mainForm.imageWindow.Window;
                //            treeView1.DoDragDrop(new ModuleControl(module), DragDropEffects.Move);
                //            return;
                //        }
                //    }
                //}
            }
        }
    }
}
