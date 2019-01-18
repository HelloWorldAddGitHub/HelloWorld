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
        public FormDockProcessBar(string fileName = null)
        {
            InitializeComponent();

            if (fileName != null)
            {
                Open(fileName);
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            // 判断项目是否存在
            if (string.IsNullOrWhiteSpace(Project.GetInstance().Name))
            {
                MessageBox.Show("请先新建项目");
                return;
            }

            // 初始化流程名称
            string name = "流程1";

            for (int i = 1; ; i++)
            {
                if (!TabProcesses.TabPages.ContainsKey("流程" + i))
                {
                    name = "流程" + i;
                    break;
                }
            }

            // 新建流程页
            ProcessTabPage tp = new ProcessTabPage(name);

            if (Project.GetInstance().MainProcess == null)
            {   // 设置主流程
                Project.GetInstance().MainProcess = Project.GetInstance()[name];
            }

            // 将流程页添加到TabControl
            TabProcesses.TabPages.Add(tp);
            TabProcesses.SelectedTab = tp;
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (TabProcesses.TabPages.Count <= 1)
            {
                return;
            }

            // 获得流程名称
            string name = ((ProcessTabPage)TabProcesses.SelectedTab).Text;

            if (Project.GetInstance().MainProcess.Name == name)
            {
                MessageBox.Show("不能移除主流程");
                return;
            }

            // 移除流程
            Project.GetInstance().Items.Remove(name);

            // 移除流程标签页
            int index = TabProcesses.TabPages.IndexOf(TabProcesses.SelectedTab);
            TabProcesses.TabPages.Remove(TabProcesses.SelectedTab);

            if (index == 0 || TabProcesses.TabPages.Count == 0)
            {
                return;
            }

            TabProcesses.SelectedIndex = index - 1;
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (TabProcesses.TabPages.Count == 0)
            {
                return;
            }

            FormProcessBarSet f = new FormProcessBarSet(this);
            f.ShowDialog();
        }


        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            //string name = ((ProcessTabPage)e.Control).Text;
            //Project.GetInstance().Items.Add(name, new Process(name));
        }

        private void tabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            //string name = ((ProcessTabPage)e.Control).Text;
            //Project.GetInstance().Items.Remove(name);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ProcessTabPage pc = (ProcessTabPage)TabProcesses.SelectedTab;
            //if (pc != null)
            //{
            //    Project.GetInstance().MainProcess = Project.GetInstance().Items[((ProcessTabPage)TabProcesses.SelectedTab).Text];
            //}
            //else
            //{
            //    Project.GetInstance().MainProcess = null;
            //}
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            FormProcessBarNew fpbn = new FormProcessBarNew(this);
            fpbn.ShowDialog();
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

            Open(ofd.FileName);
        }

        private void Open(string fileName)
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

            // 从XML加载流程
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlElement root = doc.DocumentElement;

            // 清理所有流程
            TabProcesses.TabPages.Clear();
            Project.GetInstance().Clear();
            Project.GetInstance().Name = root.GetAttribute("name");

            // 流程节点循环加载
            foreach (XmlElement page in root.ChildNodes)
            {
                // 增加流程
                ProcessTabPage tp = new ProcessTabPage(page.GetAttribute("name")/*, VisualProject*/);
                TabProcesses.TabPages.Add(tp);

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
                            module.Owner = Project.GetInstance()[tp.Text];
                            module.Index = Convert.ToInt32(unit.GetAttribute("id"));
                            module.Load();

                            ModuleControl mc = new ModuleControl(module);
                            tp.AddModule(mc);
                        }
                    }

                }
            }

            // 设置主流程
            Project.GetInstance().MainProcess = Project.GetInstance()[root.GetAttribute("main")];
            TabProcesses.SelectTab(Project.GetInstance().MainProcess.Name);
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Project.GetInstance().Name))
            {
                Save();
            }
            else
            {
                MessageBox.Show("没有项目");
            }
        }

        /// <summary>
        /// 保存项目
        /// </summary>
        /// <param name="isSaveModule">指示是否保存模块数据</param>
        public void Save(bool isSaveModule = true)
        {
            // 创建项目的XML文档
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Project");
            root.SetAttribute("name", Project.GetInstance().Name);
            root.SetAttribute("main", Project.GetInstance().MainProcess.Name);

            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            doc.AppendChild(dec);
            doc.AppendChild(root);

            // 添加流程节点
            foreach (var process in Project.GetInstance().Items.Values)
            {
                XmlElement page = doc.CreateElement("Process");
                page.SetAttribute("name", process.Name);

                // 添加模块节点
                foreach (var module in process.Items)
                {
                    XmlElement unit = doc.CreateElement("Module");

                    unit.SetAttribute("id", module.Index.ToString());
                    unit.SetAttribute("name", module.Name);

                    if (isSaveModule)
                    {   // 保存模块数据
                        module.Save();
                    }

                    page.AppendChild(unit);
                }

                root.AppendChild(page);
            }

            // 永久保存
            doc.Save($@".\projects\{Project.GetInstance().Name}.vs");
        }
    }
}
