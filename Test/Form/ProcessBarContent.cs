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
using WeifenLuo.WinFormsUI.Docking;

namespace Test
{
    public partial class ProcessBarContent : DockContent
    {
        //private Project project;

        private MainForm mainForm;

        //private string filename = "demo.vs";

        public ProcessBarContent(MainForm form, string fileName)
        {
            mainForm = form;
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

            ProcessControl tp = new ProcessControl(text, mainForm);
            mainForm.Projects.MainProcess = tp.Processes;


            tabControl1.TabPages.Add(tp);
            tabControl1.SelectedTab = tp;
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count == 0)
            {
                return;
            }

            mainForm.Projects.Remove(mainForm.Projects[((ProcessControl)tabControl1.SelectedTab).Name]);

            int index = tabControl1.TabPages.IndexOf(tabControl1.SelectedTab);
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            
            if (index == 0 || tabControl1.TabPages.Count == 0)
            {
                return;
            }

            tabControl1.SelectedIndex = index - 1;

            //project.CurrentProcess = project[((ProcessControl)tabControl1.SelectedTab).Name];
            

        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count == 0)
            {
                return;
            }

            ProcessBarSetForm ttef = new ProcessBarSetForm(tabControl1);
            ttef.ShowDialog();
        }


        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            mainForm.Projects.Add(((ProcessControl)e.Control).Processes);
        }

        private void tabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            mainForm.Projects.Remove(((ProcessControl)e.Control).Processes);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessControl pc = (ProcessControl)tabControl1.SelectedTab;
            if (pc != null)
            {
                mainForm.Projects.MainProcess = ((ProcessControl)tabControl1.SelectedTab).Processes;
            }
            else
            {
                mainForm.Projects.MainProcess = null;
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
            //filename = fileName;

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlElement root = doc.DocumentElement;
            
            

            mainForm.Projects.Clear();
            mainForm.Projects.Name = root.GetAttribute("name");

            foreach (XmlElement page in root.ChildNodes)
            {
                ProcessControl tp = new ProcessControl(page.GetAttribute("name"), mainForm);
                tabControl1.TabPages.Add(tp);

                mainForm.Projects.Add(tp.Processes);

                foreach (XmlElement unit in page.ChildNodes)
                {
                    Type[] types = Assembly.GetExecutingAssembly().GetTypes();

                    foreach (var item in types)
                    {
                        if (item.Name.IndexOf("Module") > 0)
                        {
                            ModuleAttribute des = (ModuleAttribute)item.GetCustomAttribute(typeof(ModuleAttribute));

                            if (unit.GetAttribute("name") == des.Name)
                            {
                                ModuleBase module = (ModuleBase)Assembly.GetExecutingAssembly().CreateInstance(item.FullName);
                                module.Process = tp.Processes;
                                module.HWindow = mainForm.imageWindow.Window;

                                module.Index = Convert.ToInt32(unit.GetAttribute("id"));
                                module.Status = (ModuleStatus)Enum.Parse(typeof(ModuleStatus), unit.GetAttribute("status"));

                                module.Load();

                                ModuleControl mc = new ModuleControl(module);

                                tp.AddModule(mc);
                            }
                        }
                    }

                }
            }

            mainForm.Projects.MainProcess = mainForm.Projects.Find((p) =>
            {
                if (p.Name == root.GetAttribute("main"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

            tabControl1.SelectTab(mainForm.Projects.MainProcess.Name);
            mainForm.Text += "——" + mainForm.Projects.Name;
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
            root.SetAttribute("name", mainForm.Projects.Name);
            root.SetAttribute("main", mainForm.Projects.MainProcess.Name);
            
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            doc.AppendChild(dec);
            doc.AppendChild(root);
            
            foreach (var process in mainForm.Projects)
            {
                XmlElement page = doc.CreateElement("Process");
                page.SetAttribute("name", process.Name);

                foreach (var module in process)
                {
                    XmlElement unit = doc.CreateElement("Module");
                    
                    unit.SetAttribute("id", module.Index.ToString());
                    unit.SetAttribute("name", module.Name);
                    unit.SetAttribute("status", module.Status.ToString());

                    module.Save();

                    page.AppendChild(unit);
                }

                root.AppendChild(page);
            }


            doc.Save($"projects\\{mainForm.Projects.Name}.vs");
        }
    }
}
