using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Module;

namespace Demo
{
    public partial class FormProcessBarNew : Form
    {
        private FormDockProcessBar processBar;

        public FormProcessBarNew(FormDockProcessBar processBar)
        {
            this.processBar = processBar;
            InitializeComponent();
        }

        private void FormProcessBarNew_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                Project.GetInstance().Clear();
                processBar.TabProcesses.Controls.Clear();
                Project.GetInstance().Name = txtName.Text;
                processBar.TabProcesses.Controls.Add(new ProcessTabPage("流程1"));

                processBar.Text = $"流程栏——{txtName.Text}";
            }
            else
            {
                MessageBox.Show("名称无效");
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
