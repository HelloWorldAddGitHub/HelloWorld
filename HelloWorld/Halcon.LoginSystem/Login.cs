using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Halcon.LoginSystem
{
    //public enum AccountName { Administrator, Adjustor, Operator }
    public enum AccountName { 管理员, 调试员, 操作员 }


    public class LoginSystem
    {
        private AccountName account = AccountName.操作员;
        public AccountName Account
        {
            get
            {
                return account;
            }
            set
            {
                if (account != value)
                {
                    account = value;
                    AccountChanged?.Invoke(this, new AccountChangedEventArgs(account));
                }
            }
        }
        //public string Password { get; }

        public Dictionary<AccountName, string> AccountInfo = new Dictionary<AccountName, string>();

        //bool DeveloperMode;


        public event EventHandler<AccountChangedEventArgs> AccountChanged;



        public LoginSystem()
        {
            Init();
        }


        public void Init()
        {
            try
            {
                FileStream fsream = new FileStream("password.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                MemoryStream stream = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                AccountInfo = (Dictionary<AccountName, string>)bf.Deserialize(fsream);
                stream.Close();
                fsream.Close();
            }
            catch (Exception)
            {
                AccountInfo.Add(AccountName.管理员, "");
                AccountInfo.Add(AccountName.调试员, "");
                AccountInfo.Add(AccountName.操作员, "");
            }
            //finally
            //{
            //    DeveloperMode();
            //}
        }

        [Conditional("DEBUG")]
        private void DeveloperMode()
        {
            Account = AccountName.管理员;
        }

        public bool Login(string account, string password)
        {
            if (Enum.IsDefined(typeof(AccountName), account))
            {
                AccountName name = (AccountName)Enum.Parse(typeof(AccountName), account);

                if (password == AccountInfo[name])
                {
                    Account = name;
                    return true;
                }
            }
            return false;
        }


        public void Exit()
        {
            Account = AccountName.操作员;
        }


        public bool SetPassword(string oldPassword, string newPassword)
        {
            if (AccountInfo[Account] == oldPassword)
            {
                AccountInfo[Account] = newPassword;

                FileStream fsream = new FileStream("password.dat", FileMode.Create);
                MemoryStream stream = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fsream, AccountInfo);
                stream.Close();
                fsream.Close();

                return true;
            }

            return false;
        }
    }
}
