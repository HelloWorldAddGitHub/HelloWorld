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
    public partial class ToolBox : DockContent
    {
        private MainForm mainForm;

        public ToolBox(MainForm form)
        {
            mainForm = form;
            InitializeComponent();
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && treeView1.SelectedNode != null)
            {
                ReadImage ri = new ReadImage();
                ri.ImageWindow = mainForm.imageWindow.Window;
                treeView1.DoDragDrop(new ModuleControl(ri), DragDropEffects.Move);
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
