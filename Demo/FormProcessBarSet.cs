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
using Module;

namespace Demo
{
    public partial class FormProcessBarSet : Form
    {
        private FormDockProcessBar processBar;

        private string oldCelleValue;
        private List<string> addProCollection = new List<string>();
        private List<string> delProCollection = new List<string>();
        private Dictionary<string, string> renameProCollection = new Dictionary<string, string>();

        public FormProcessBarSet(FormDockProcessBar processBar)
        {
            InitializeComponent();
            this.processBar = processBar;
        }

        private void TabTextEditForm_Load(object sender, EventArgs e)
        {
            // 获取项目名称
            string name = Project.GetInstance().Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                txtProjectName.Text = "项目1";
                return;
            }
            txtProjectName.Text = name;

            // 获取流程
            foreach (ProcessTabPage control in processBar.TabProcesses.Controls)
            {
                dgvProcess.Rows.Add(new DataGridViewTextBoxCell(), new DataGridViewTextBoxCell());
                DataGridViewRow row = dgvProcess.Rows[dgvProcess.Rows.Count - 1];
                row.Cells[0].Value = dgvProcess.Rows.Count;
                row.Cells[1].Value = control.Text;

                cmbMainProcess.Items.Add(control.Text);
            }

            // 获取主流程
            cmbMainProcess.SelectedItem = Project.GetInstance().MainProcess.Name;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                MessageBox.Show("请输入项目名称");
                return;
            }

            // 获得项目名称
            string name = Project.GetInstance().Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = txtProjectName.Text;
            }

            // 移动项目文件夹
            if (name != txtProjectName.Text)
            {
                string oldDir = $@".\projects\{name}";
                string newDir = $@".\projects\{txtProjectName.Text}";

                if (Directory.Exists(oldDir))
                {
                    Directory.Move(oldDir, newDir);
                }

                string oldFile = $@".\projects\{name}.vs";
                string newFile = $@".\projects\{txtProjectName.Text}.vs";

                if (File.Exists(oldFile))
                {
                    File.Move(oldFile, newFile);
                }

                Project.GetInstance().Name = txtProjectName.Text;
                processBar.Text = $"流程栏——{name}";
            }

            // 删除流程
            foreach (var item in delProCollection)
            {
                processBar.TabProcesses.DeselectTab(item);
                Project.GetInstance().Items.Remove(item);

                string path = $@".\projects\{Project.GetInstance().Name}\{item}";
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }

            // 重命名
            foreach (var item in renameProCollection)
            {
                // 更改流程控件名称
                var control = processBar.TabProcesses.Controls.Find(item.Key, false)[0];
                control.Text = item.Value;

                // 更改流程名称
                Project.GetInstance().Items[item.Key].Name = item.Value;
                Process process = Project.GetInstance().Items[item.Key];

                // 重新添加该流程
                Project.GetInstance().Items.Remove(item.Key);
                Project.GetInstance().Items.Add(item.Value, process);

                // 移动文件夹
                string oldPath = $@".\projects\{Project.GetInstance().Name}\{item.Key}";
                string nawPath = $@".\projects\{Project.GetInstance().Name}\{item.Value}";
                if (Directory.Exists(oldPath))
                {
                    Directory.Move(oldPath, nawPath);
                }
            }

            // 增加流程
            foreach (var item in addProCollection)
            {
                processBar.TabProcesses.TabPages.Add(new ProcessTabPage(item));
            }

            // TabPage控件排序
            int count = dgvProcess.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                string value = (string)dgvProcess.Rows[i].Cells[1].FormattedValue;
                TabPage control = processBar.TabProcesses.TabPages[value];
                processBar.TabProcesses.TabPages.Remove(control);
                processBar.TabProcesses.TabPages.Insert(i, control);
            }

            // 设置主流程
            cmbMainProcess_DropDown(this, EventArgs.Empty);
            Project.GetInstance().MainProcess = Project.GetInstance().Items[cmbMainProcess.Text];

            if (ckbDefault.Checked)
            {

            }
            else
            {

            }

            processBar.Save(false);

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            dgvProcess.Rows.Add(new DataGridViewTextBoxCell(), new DataGridViewTextBoxCell());
            DataGridViewRow row = dgvProcess.Rows[dgvProcess.Rows.Count - 1];
            row.Cells[0].Value = dgvProcess.Rows.Count;

            // 初始化流程名称
            for (int i = 1; ; i++)
            {
                string name = "流程" + i;

                foreach (DataGridViewRow item in dgvProcess.Rows)
                {
                    if ((string)item.Cells[1].FormattedValue == name)
                    {
                        name = string.Empty;
                        break;
                    }
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }
                else
                {
                    row.Cells[1].Value = name;
                    addProCollection.Add(name);// 记录需要新增的流程
                    break;
                }
            }

            // 选中新增的行
            dgvProcess.CurrentCell = dgvProcess.Rows[dgvProcess.Rows.Count - 1].Cells[0];
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            if (dgvProcess.Rows.Count == 0)
            {
                return;
            }

            int index = dgvProcess.SelectedRows[0].Index;
            string name = (string)dgvProcess.SelectedRows[0].Cells[1].FormattedValue;

            // 记录删除流程
            if (addProCollection.Contains(name))
            {// 删除新增的记录
                addProCollection.Remove(name);
            }
            else
            {// 记录需要删除的流程
                delProCollection.Add(name);
            }

            // 删除row
            dgvProcess.Rows.Remove(dgvProcess.Rows[index]);

            // 重新设置序号
            for (int i = index; i < dgvProcess.Rows.Count; i++)
            {
                dgvProcess.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void butUp_Click(object sender, EventArgs e)
        {
            if (dgvProcess.Rows.Count == 0)
            {
                return;
            }

            int index = dgvProcess.SelectedRows[0].Index;
            if (index == 0)
            {
                return;
            }

            string text = (string)dgvProcess.Rows[index].Cells[1].FormattedValue;
            dgvProcess.Rows[index].Cells[1].Value = dgvProcess.Rows[index - 1].Cells[1].Value;
            dgvProcess.Rows[index - 1].Cells[1].Value = text;
            dgvProcess.Rows[index - 1].Selected = true;
        }

        private void butDown_Click(object sender, EventArgs e)
        {
            if (dgvProcess.Rows.Count == 0)
            {
                return;
            }

            int index = dgvProcess.SelectedRows[0].Index;
            if (index == dgvProcess.Rows.Count - 1)
            {
                return;
            }

            string text = (string)dgvProcess.Rows[index].Cells[1].FormattedValue;
            dgvProcess.Rows[index].Cells[1].Value = dgvProcess.Rows[index + 1].Cells[1].Value;
            dgvProcess.Rows[index + 1].Cells[1].Value = text;
            dgvProcess.Rows[index + 1].Selected = true;
        }

        private void cmbMainProcess_DropDown(object sender, EventArgs e)
        {
            string text = (string)cmbMainProcess.SelectedItem;
            cmbMainProcess.Items.Clear();

            foreach (DataGridViewRow row in dgvProcess.Rows)
            {
                cmbMainProcess.Items.Add(row.Cells[1].FormattedValue);
            }

            if (cmbMainProcess.FindString(text) != -1)
            {
                cmbMainProcess.SelectedItem = text;
            }
            else
            {
                cmbMainProcess.SelectedIndex = 0;
            }
        }

        private void dgvProcess_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProcess_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (oldCelleValue != null && e.ColumnIndex == 1)
            {
                string name = (string)dgvProcess.Rows[e.RowIndex].Cells[1].FormattedValue;

                // 记录删除流程
                if (addProCollection.Contains(oldCelleValue))
                {// 删除新增的记录
                    addProCollection.Remove(oldCelleValue);
                    addProCollection.Add(name);
                }
                else
                {
                    // 判断名称变更情况
                    if (renameProCollection.ContainsValue(oldCelleValue))
                    {
                        foreach (var item in renameProCollection)
                        {
                            if (item.Value == oldCelleValue)
                            {
                                string key = item.Key;
                                renameProCollection.Remove(key);
                                renameProCollection.Add(key, name);
                                break;
                            }
                        }
                    }
                    else
                    {
                        renameProCollection.Add(oldCelleValue, name);
                    }
                }

                oldCelleValue = null;
            }
        }

        private void dgvProcess_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProcess_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                oldCelleValue = (string)dgvProcess.Rows[e.RowIndex].Cells[1].FormattedValue;
            }
        }
    }
}
