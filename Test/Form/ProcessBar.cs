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

namespace Test
{
    public partial class ProcessBar : DockContent
    {
        //private Project project;

        private MainForm mainForm;

        public ProcessBar(MainForm form)
        {
            mainForm = form;
            InitializeComponent();
        }

        private void tsbNew_Click(object sender, EventArgs e)
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
            mainForm.Projects.CurrentProcess = tp.Procedure;


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

            TabTextEditForm ttef = new TabTextEditForm(tabControl1);
            ttef.ShowDialog();
        }


        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            mainForm.Projects.Add(((ProcessControl)e.Control).Procedure);
        }

        private void tabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            mainForm.Projects.Remove(((ProcessControl)e.Control).Procedure);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessControl pc = (ProcessControl)tabControl1.SelectedTab;
            if (pc != null)
            {
                mainForm.Projects.CurrentProcess = ((ProcessControl)tabControl1.SelectedTab).Procedure;
            }
            else
            {
                mainForm.Projects.CurrentProcess = null;
            }
        }
    }
}
