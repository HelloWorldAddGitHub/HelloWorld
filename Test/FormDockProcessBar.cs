using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Module;
using WeifenLuo.WinFormsUI.Docking;

namespace Demo
{
    public partial class FormDockProcessBar : DockContent
    {
        private Project project;

        public FormDockProcessBar(Project project, string fileName = null)
        {
            this.project = project;
            InitializeComponent();

            if (fileName != null)
            {
                OpenFile(fileName);
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            string text = "流程1";

            for (int i = 1; ; i++)
            {
                if (!tabControl1.TabPages.ContainsKey("流程" + i))
                {
                    text = "流程" + i;
                    break;
                }
            }

            ProcessControl tp = new ProcessControl(text, project);
            project.MainProcess = tp.VisualProcess;


            tabControl1.TabPages.Add(tp);
            tabControl1.SelectedTab = tp;
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count == 0)
            {
                return;
            }

            Process process = project[((ProcessControl)tabControl1.SelectedTab).Name];
            project.Items.Remove(process.Name);


            int index = tabControl1.TabPages.IndexOf(tabControl1.SelectedTab);
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);

            if (index == 0 || tabControl1.TabPages.Count == 0)
            {
                return;
            }

            tabControl1.SelectedIndex = index - 1;

        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count == 0)
            {
                return;
            }

            FormSetProcessBar ttef = new FormSetProcessBar(tabControl1);
            ttef.ShowDialog();
        }


        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            Process process = ((ProcessControl)e.Control).VisualProcess;
            project.Items.Add(process.Name, process);
        }

        private void tabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            Process process = ((ProcessControl)e.Control).VisualProcess;
            project.Items.Remove(process.Name);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessControl pc = (ProcessControl)tabControl1.SelectedTab;
            if (pc != null)
            {
                project.MainProcess = ((ProcessControl)tabControl1.SelectedTab).VisualProcess;
            }
            else
            {
                project.MainProcess = null;
            }
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {

        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = $"{Application.StartupPath}\\projects";
            ofd.Filter = "vision文件|*.vs|所有文件|*.*";

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            OpenFile(ofd.FileName);
        }

        private void OpenFile(string fileName)
        {
            // 加载当前应用程序目录下的指定程序集
            DirectoryInfo info = new DirectoryInfo(Application.StartupPath);
            FileInfo[] files = info.GetFiles("*Module.dll");

            Dictionary<Type, Assembly> typeCollection = new Dictionary<Type, Assembly>();

            foreach (var file in files)
            {
                Assembly a = Assembly.LoadFile(file.FullName);
                Type[] types = a.GetExportedTypes();

                foreach (var type in types)
                {
                    ModuleAttribute attribute = (ModuleAttribute)type.GetCustomAttribute(typeof(ModuleAttribute));

                    if (attribute != null)
                    {
                        typeCollection.Add(type, a);
                    }
                }
            }


            // 清理所有流程
            tabControl1.TabPages.Clear();

            // 从XML加载流程
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlElement root = doc.DocumentElement;

            // 获取项目
            project.Items.Clear();
            project.Name = root.GetAttribute("name");

            // 流程节点循环加载
            foreach (XmlElement page in root.ChildNodes)
            {
                // 增加流程
                ProcessControl tp = new ProcessControl(page.GetAttribute("name"), project);
                tabControl1.TabPages.Add(tp);

                // 模块节点循环加载
                foreach (XmlElement unit in page.ChildNodes)
                {
                    foreach (var type in typeCollection)
                    {
                        // 获取类型自定义特性
                        ModuleAttribute des = (ModuleAttribute)type.Key.GetCustomAttribute(typeof(ModuleAttribute));

                        if (unit.GetAttribute("name") == des.Name)
                        {
                            // 增加模块
                            ModuleBase module = (ModuleBase)type.Value.CreateInstance(type.Key.FullName);
                            module.Owner = tp.VisualProcess;
                            module.Index = Convert.ToInt32(unit.GetAttribute("id"));
                            module.Load();

                            ModuleControl mc = new ModuleControl(module);
                            tp.AddModule(mc);
                        }
                    }

                }
            }

            // 设置主流程
            project.MainProcess = project[root.GetAttribute("main")];

            tabControl1.SelectTab(project.MainProcess.Name);
            //project.Text += "——" + project.Name;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            //if (filename == null)
            //{
            //    SaveFileDialog sfd = new SaveFileDialog();

            //    if (sfd.ShowDialog() == DialogResult.OK)
            //    {
            //        filename = sfd.FileName;
            //    }
            //}


            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Project");
            root.SetAttribute("name", project.Name);
            root.SetAttribute("main", project.MainProcess.Name);

            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            doc.AppendChild(dec);
            doc.AppendChild(root);

            foreach (var process in project.Items.Values)
            {
                XmlElement page = doc.CreateElement("Process");
                page.SetAttribute("name", process.Name);

                foreach (var module in process.Items)
                {
                    XmlElement unit = doc.CreateElement("Module");

                    unit.SetAttribute("id", module.Index.ToString());
                    unit.SetAttribute("name", module.Name);
                    //unit.SetAttribute("status", module.Status.ToString());

                    module.Save();

                    page.AppendChild(unit);
                }

                root.AppendChild(page);
            }


            doc.Save($"projects\\{project.Name}.vs");
        }
    }
}
