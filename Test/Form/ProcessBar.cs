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
        //List<ProcessUnit> unit = new List<ProcessUnit>();

        //public ProcessUnit CurrentUnit;

        public ProcessBar()
        {
            InitializeComponent();

            
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            ProcessControl tp = new ProcessControl();

            for (int i = 1; ; i++)
            {
                if (!tabControl1.TabPages.ContainsKey("流程" + i))
                {
                    tp.Name = tp.Text = "流程" + i;
                    break;
                }
            }
            
            
            tabControl1.TabPages.Add(tp);
            tabControl1.SelectedTab = tp;
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count == 0)
            {
                return;
            }

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

            TabTextEditForm ttef = new TabTextEditForm(tabControl1);
            ttef.ShowDialog();
        }
    }
}
