using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Halcon.LoginSystem;

namespace Demo
{
    public partial class FormLogin : Form
    {
        public LoginSystem Login = new LoginSystem();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            cmbAccount.Items.AddRange(Enum.GetNames(typeof(AccountName)));
            cmbAccount.SelectedIndex = 0;

            Login.AccountChanged += ChangeAccount;

            label1.Text = Login.Account.ToString();
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            //AccountName name = (AccountName)Enum.Parse(typeof(AccountName), (string)cmbAccount.Text);

            if (Login.Login((string)cmbAccount.SelectedItem, txbPassword.Text))
            {
                MessageBox.Show("登录成功");
            }
            else
            {
                MessageBox.Show("失败了");
            }
        }

        private void butSetPassword_Click(object sender, EventArgs e)
        {
            if(Login.SetPassword(txbPassword.Text, txbNewPassord.Text))
            {
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("失败了");
            }
        }


        private void ChangeAccount(object send, AccountChangedEventArgs e)
        {
            label1.Text = e.Account.ToString();
        }
    }
}
