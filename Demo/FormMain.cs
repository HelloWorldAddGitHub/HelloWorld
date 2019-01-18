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
        public FormDockImageWindow imageWindow;
        public FormDockToolBox toolBox;
        public FormDockProcessBar processBar;

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
            InitializeComponent();

            if (args.Length > 0)
            {
                imageWindow = new FormDockImageWindow();
                HDevWindowStack.Push(imageWindow.Window.HalconWindow);
                processBar = new FormDockProcessBar(args[0]);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // 设置主题
            SetTheme(new VS2015BlueTheme());

            // 创建窗口
            CreateContents();

            // 加载配置
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

        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="themeBase"></param>
        private void SetTheme(ThemeBase themeBase)
        {
            dockPanel1.Theme = themeBase;
            visualStudioToolStripExtender1.SetStyle(toolStrip1, VisualStudioToolStripExtender.VsVersion.Vs2015, dockPanel1.Theme);
            visualStudioToolStripExtender1.SetStyle(statusStrip1, VisualStudioToolStripExtender.VsVersion.Vs2015, dockPanel1.Theme);
            statusStrip1.BackColor = dockPanel1.Theme.ColorPalette.MainWindowStatusBarDefault.Background;
        }

        /// <summary>
        /// 加载DockPanel.config
        /// </summary>
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

        /// <summary>
        /// 创建DockContext
        /// </summary>
        private void CreateContents()
        {
            if (processBar == null)
            {
                imageWindow = new FormDockImageWindow();
                HDevWindowStack.Push(imageWindow.Window.HalconWindow);
                processBar = new FormDockProcessBar();
            }

            toolBox = new FormDockToolBox(this);
        }


        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            dockPanel1.SaveAsXml(configFile);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Project.GetInstance().MainProcess.ExecuteStart();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Project.GetInstance().MainProcess.ExecuteStop();
        }

        private void tsbRunOne_Click(object sender, EventArgs e)
        {
            Project.GetInstance().MainProcess.ExecuteOne();
        }
    }
}
