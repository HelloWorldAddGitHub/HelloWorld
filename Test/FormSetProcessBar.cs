using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class FormSetProcessBar : Form
    {
        TabControl tab;

        public FormSetProcessBar(TabControl tab)
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
                string oldName = $@"{System.Environment.CurrentDirectory}\projects\{tab.SelectedTab.Text}";
                string newName = $@"{System.Environment.CurrentDirectory}\projects\{textBox1.Text}";

                if (!Directory.Exists(newName))
                {
                    Directory.CreateDirectory(newName);
                }

                DirectoryInfo di = new DirectoryInfo(oldName);
                di.MoveTo(newName);

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
