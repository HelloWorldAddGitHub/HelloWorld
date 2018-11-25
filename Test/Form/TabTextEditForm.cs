using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class TabTextEditForm : Form
    {
        TabControl tab;

        public TabTextEditForm(TabControl tab)
        {
            InitializeComponent();
            this.tab = tab;
        }

        private void TabTextEditForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = tab.SelectedTab.Text;
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tab.TabPages.ContainsKey(textBox1.Text) || textBox1.Text == tab.SelectedTab.Text)
            {
                tab.SelectedTab.Name = tab.SelectedTab.Text = textBox1.Text;
                Close();
            }
            else
            {
                MessageBox.Show("名称重复");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
