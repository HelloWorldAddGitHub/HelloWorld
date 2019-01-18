using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Halcon.Camera;

namespace Test.Module
{
    public partial class HFramegrabberForm : Form
    {
        List<HInterfaceInfo> cameras;
        HFramegrabberModule framegrabber;

        //HalconCamera cam = new HalconCamera();

        public HFramegrabberForm(HFramegrabberModule f)
        {
            framegrabber = f;
            InitializeComponent();
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            //cmbInterface.Items.AddRange(HalconCamera.Find().ToArray());
            cmbInterface.Items.Clear();
            this.Cursor = Cursors.WaitCursor;
            cameras = HalconCamera.Find();

            foreach (var item in cameras)
            {
                cmbInterface.Items.Add(item.InterfaceName);
            }

            if (cmbInterface.Items.Count != 0)
            {
                cmbInterface.SelectedIndex = 0;
            }



            this.Cursor = Cursors.Default;
        }

        private void cmbInterface_TextChanged(object sender, EventArgs e)
        {
            HInterfaceInfo info = cameras.Find((f) =>
             {
                 if (f.InterfaceName.ToString() == cmbInterface.Text)
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
             });


            cmbCamera.Items.Clear();
            foreach (var item in info.Device.ToSArr())
            {
                cmbCamera.Items.Add(item.ToString());
            }

            if (cmbCamera.Items.Count != 0)
            {
                cmbCamera.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            framegrabber.cam.InterfaceName = (HInterfaceName)Enum.Parse(typeof(HInterfaceName), cmbInterface.Text);
            framegrabber.cam.Device = cmbCamera.Text;
            framegrabber.cam.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            framegrabber.cam.GrabOne();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "实时")
            {
                button3.Text = "停止";
                framegrabber.cam.GrabStartAsync();
            }
            else
            {
                button3.Text = "实时";
                framegrabber.cam.GrabStop();
            }
        }
    }
}
