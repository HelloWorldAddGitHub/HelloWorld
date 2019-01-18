using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Module;
using WeifenLuo.WinFormsUI.Docking;

namespace Demo
{
    public partial class FormDockToolBox : DockContent
    {
        /// <summary>
        /// 模块集合
        /// </summary>
        private Dictionary<string, Assembly> assembly = new Dictionary<string, Assembly>();

        public FormDockToolBox(FormMain form)
        {
            InitializeComponent();
        }

        private void ToolBox_Load(object sender, EventArgs e)
        {
            // 获取当前文件夹信息，提取模块动态库文件
            DirectoryInfo info = new DirectoryInfo(Application.StartupPath);
            FileInfo[] files = info.GetFiles("*Module.dll");

            // 加载程序集
            foreach (var file in files)
            {
                Assembly a = Assembly.LoadFile(file.FullName);
                Type[] types = a.GetExportedTypes();

                // 更新到界面
                foreach (var type in types)
                {
                    // 获取类型自定义特性
                    ModuleAttribute attribute = (ModuleAttribute)type.GetCustomAttribute(typeof(ModuleAttribute));

                    if (attribute != null)
                    {
                        // 添加到集合
                        assembly.Add(attribute.Name, a);

                        // 添加分类节点
                        if (treeView1.Nodes.Find(attribute.Category, false).Count() == 0)
                        {
                            treeView1.Nodes.Add(attribute.Category, attribute.Category);
                            treeView1.Nodes.Find(attribute.Category, false)[0].ImageIndex = 1;
                            treeView1.Nodes.Find(attribute.Category, false)[0].SelectedImageIndex = 1;
                        }

                        // 添加模块节点
                        treeView1.Nodes.Find(attribute.Category, false)[0].Nodes.Add(attribute.Name);
                    }
                }
            }
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
                        // 创建模块实例
                        ModuleBase module = (ModuleBase)a.CreateInstance(type.FullName);

                        // 开始拖放操作
                        treeView1.DoDragDrop(new ModuleControl(module), DragDropEffects.Move);
                    }
                }
            }
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ModuleControl).ToString(), true))
            {
                e.Effect = DragDropEffects.Move;
            }
        }
    }
}
