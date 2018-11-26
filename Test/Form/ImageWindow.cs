using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Halcon.Window;
using WeifenLuo.WinFormsUI.Docking;

namespace Test
{
    public partial class ImageWindow : DockContent
    {
        public HalconWindow Window { get; set; } = new HalconWindow();

        public ImageWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
