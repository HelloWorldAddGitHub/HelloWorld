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
using HalconDotNet;
using WeifenLuo.WinFormsUI.Docking;

namespace Test
{
    public partial class MainForm : Form
    {
        public Project Projects { get; set; }


        public ImageWindowContent imageWindow;
        public ToolBoxContent toolBox;
        public ProcessBarContent processBar;


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

        public MainForm()
        {
            Projects = new Project("Demo");

            InitializeComponent();

            //m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            //m_deserializeDockContent += GetContentFromPersistString;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            SetTheme(new VS2015BlueTheme());

            CreateContents();

            LoadContents();
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
                if (field.FieldType.ToString().IndexOf("Content") > 0)
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
            imageWindow = new ImageWindowContent();

            processBar = new ProcessBarContent(this);
            toolBox = new ToolBoxContent(this);
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
            Projects.CurrentProcess.Start();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Projects.CurrentProcess.Stop();
        }

        private void tsbRunOne_Click(object sender, EventArgs e)
        {
            Projects.CurrentProcess.RunOne();
        }
    }
}
