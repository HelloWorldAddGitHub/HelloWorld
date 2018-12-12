using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Module
{
    public partial class ReadImageForm : Form
    {
        ReadImageModule ri;
        public ReadImageForm(ReadImageModule module)
        {
            ri = module;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //ri.fileName = ofd.FileNames;
                textBox1.Text = ofd.FileNames[0];
                //ri.fileName = ofd.FileName.Replace("\\", "/");

                ri.fileName = new HalconDotNet.HTuple();
                foreach (var item in ofd.FileNames)
                {
                    ri.fileName.Append(item.Replace("\\", "/"));
                }

            }
        }
    }
}
