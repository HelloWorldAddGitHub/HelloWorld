using System;
using System.IO;
using System.Windows.Forms;
using HalconDotNet;
using Microsoft.Win32;
using Module;
using WeifenLuo.WinFormsUI.Docking;

namespace Demo
{
    public partial class FormMain : Form
    {
        public Project VisualProject { get; set; }


        public FormDockImageWindow imageWindow;
        public FormDockToolBox toolBox;
        public FormDockProcessBar processBar;


        //private DeserializeDockContent m_deserializeDockContent;


        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            toolStripButton5.Image = toolStripMenuItem4.Image;
            toolStripButton5.Text = toolStripMenuItem4.Text;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            toolStripButton5.Image = toolStripMenuItem5.Image;
            toolStripButton5.Text = toolStripMenuItem5.Text;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            toolStripButton5.Image = toolStripMenuItem6.Image;
            toolStripButton5.Text = toolStripMenuItem6.Text;
        }

        public FormMain(string[] args)
        {
            //RegFileExt();

            VisualProject = new Project("Demo");

            InitializeComponent();

            if (args.Length > 0)
            {
                imageWindow = new FormDockImageWindow();
                HDevWindowStack.Push(imageWindow.Window.HalconWindow);
                processBar = new FormDockProcessBar(VisualProject, args[0]);
            }

            

            //m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            //m_deserializeDockContent += GetContentFromPersistString;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            SetTheme(new VS2015BlueTheme());

            CreateContents();

            LoadContents();
        }


        private void RegFileExt()
        {
            string extName = ".vs";
            string typeName = "Vision";
            string iconPath = Application.StartupPath + "\\vision.ico";
            string commandPath = Application.ExecutablePath + " \"%1\"";
            

            RegistryKey key = Registry.ClassesRoot.OpenSubKey(typeName);

            if (key == null)
            {
                RegistryKey regExt = Registry.ClassesRoot.CreateSubKey(extName);
                regExt.SetValue("", typeName);

                RegistryKey regType = Registry.ClassesRoot.CreateSubKey(typeName);
                RegistryKey regIcon = regType.CreateSubKey("DefaultIcon");
                regIcon.SetValue("", iconPath);

                RegistryKey regCom = regType.CreateSubKey("Shell\\Open\\Command");
                regCom.SetValue("", commandPath);
            }
            else
            {
                RegistryKey reg = key.OpenSubKey("Shell\\Open\\Command", true);

                if (reg != null && (reg.GetValue("") == null || reg.GetValue("").ToString() != commandPath))
                {
                    RegistryKey regIcon = key.OpenSubKey("DefaultIcon", true);
                    regIcon.SetValue("", iconPath);
                    reg.SetValue("", commandPath);
                }
                
            }
            
        }


        private void SetTheme(ThemeBase themeBase)
        {
            dockPanel1.Theme = themeBase;
            visualStudioToolStripExtender1.SetStyle(toolStrip1, VisualStudioToolStripExtender.VsVersion.Vs2015, dockPanel1.Theme);
            visualStudioToolStripExtender1.SetStyle(statusStrip1, VisualStudioToolStripExtender.VsVersion.Vs2015, dockPanel1.Theme);
            statusStrip1.BackColor = dockPanel1.Theme.ColorPalette.MainWindowStatusBarDefault.Background;
        }

        private void LoadContents()
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
            {
                dockPanel1.LoadFromXml(configFile, (persistString) =>
                {
                    foreach (var field in GetType().GetFields())
                    {
                        if (field.FieldType.ToString() == persistString)
                        {
                            return (IDockContent)field.GetValue(this);
                        }
                    }

                    return null;
                });
            }

            foreach (var field in GetType().GetFields())
            {
                if (field.FieldType.ToString().IndexOf("Dock") > 0)
                {
                    DockContent dc = (DockContent)field.GetValue(this);

                    if (dc.DockPanel == null)
                    {
                        dc.Show(this.dockPanel1, DockState.DockBottom);
                    }
                    else
                    {
                        dc.Show();
                    }
                }
            }
        }

        private void CreateContents()
        {
            if (processBar == null)
            {
                imageWindow = new FormDockImageWindow();
                HDevWindowStack.Push(imageWindow.Window.HalconWindow);
                processBar = new FormDockProcessBar(VisualProject, null);
            }
            
            toolBox = new FormDockToolBox(this);
        }

        //private IDockContent GetContentFromPersistString(string persistString)
        //{
        //    FieldInfo[] info = GetType().GetFields();

        //    foreach (var item in info)
        //    {
        //        if (item.FieldType.ToString() == persistString)
        //        {
        //            return (IDockContent)item.GetValue(this);
        //        }
        //    }

        //    return null;
        //}


        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            dockPanel1.SaveAsXml(configFile);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            VisualProject.MainProcess.Start();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            VisualProject.MainProcess.Stop();
        }

        private void tsbRunOne_Click(object sender, EventArgs e)
        {
            VisualProject.MainProcess.RunOne();
        }
    }
}
