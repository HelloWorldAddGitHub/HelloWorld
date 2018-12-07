using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace Test.Module
{
    public partial class FindShapeModelForm : Form
    {
        FindShapeModelModule fsm;

        public FindShapeModelForm(FindShapeModelModule findShapeModel)
        {
            fsm = findShapeModel;
            InitializeComponent();
        }

        private void FindShapeModelForm_Load(object sender, EventArgs e)
        {
            //foreach (var process in fsm.Process.Project)
            //{
            //    cmbProcess.Items.Add(process.Name);
            //}

            //if (cmbProcess.Items.Count > 0)
            //{
            //    cmbProcess.SelectedIndex = 0;
            //}

            cmbProcess.DataSource = fsm.Process.Project;
            cmbProcess.DisplayMember = "Name";
            //cmbProcess.ValueMember = "Process";
        }

        private void cmbProcess_TextChanged(object sender, EventArgs e)
        {
            //cmbModule.Items.Clear();

            //foreach (var item in fsm.Process.Project[cmbProcess.Text])
            //{
            //    cmbModule.Items.Add(item.Name);
            //}

            //if (cmbModule.Items.Count > 0)
            //{
            //    cmbModule.SelectedIndex = 0;
            //}
            //else
            //{
            //    cmbModule.Items.Add("null");
            //    cmbModule.SelectedIndex = 0;
            //}


            cmbModule.DataSource = cmbProcess.SelectedValue as Process;
            cmbModule.DisplayMember = "Name";
        }

        private void cmbModule_TextChanged(object sender, EventArgs e)
        {
            ModuleBase mb = cmbModule.SelectedValue as ModuleBase;

            BindingSource bso = new BindingSource();
            BindingSource bsv = new BindingSource();

            bso.DataSource = mb.OutHObjectParams;
            bsv.DataSource = mb.OutHTupleParams;

            cmbHObject.DataSource = bso;
            cmbHTuple.DataSource = bsv;

            cmbHObject.DisplayMember = "Key";
            //cmbHObject.ValueMember = "Value";

            cmbHTuple.DisplayMember = "Key";
            //cmbHTuple.ValueMember = "Value";


            //cmbHObject.Items.Clear();
            //cmbHTuple.Items.Clear();

            //ModuleBase mb = fsm.Process.Project[cmbProcess.Text][cmbModule.SelectedIndex];

            //foreach (var item in mb.OutHObjectParams)
            //{
            //    cmbHObject.Items.Add(item.Key);
            //}

            //foreach (var item in mb.OutHTupleParams)
            //{
            //    cmbHTuple.Items.Add(item.Key);
            //}

            //if (cmbHObject.Items.Count > 0)
            //{
            //    cmbHObject.SelectedIndex = 0;
            //}
            //else
            //{
            //    cmbHObject.Items.Add("null");
            //    cmbHObject.SelectedIndex = 0;
            //}

            //if (cmbHTuple.Items.Count > 0)
            //{
            //    cmbHTuple.SelectedIndex = 0;
            //}
            //else
            //{
            //    cmbHTuple.Items.Add("null");
            //    cmbHTuple.SelectedIndex = 0;
            //}
        }

        private void cmbHObject_TextChanged(object sender, EventArgs e)
        {
            //fsm.Image = cmbHObject.SelectedValue as HObject;

            textBox1.Text = ((Process)cmbProcess.SelectedValue).Name +
            ".[" + ((ModuleBase)cmbModule.SelectedValue).Index + "]" + ((ModuleBase)cmbModule.SelectedValue).Name +
            "." + cmbHObject.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //fsm.FileName = textBox1.Text;

            //if (cmbHObject.Text.IndexOf(']') == -1)
            {
                fsm.GetHObject(textBox1.Text);
            }

           
        }

        private void cmbHTuple_TextChanged(object sender, EventArgs e)
        {
            //textBox1.Text = ((Process)cmbProcess.SelectedValue).Name + ".[" 
            //    + ((ModuleBase)cmbModule.SelectedValue).Index + "]" + ((ModuleBase)cmbModule.SelectedValue).Name 
            //    + "." + cmbHTuple.Text;
        }
    }
}
