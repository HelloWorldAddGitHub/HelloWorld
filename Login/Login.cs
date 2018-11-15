using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Login
{
    //public enum AccountName { Administrator, Adjustor, Operator }
    public enum AccountName { 管理员, 调试员, 操作员 }


    public class Login
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

        public string AccountFileName { get; set; } = "AccountInfo.dat";

        public Dictionary<AccountName, string> AccountInfo = new Dictionary<AccountName, string>();



        public event EventHandler<AccountChangedEventArgs> AccountChanged;



        public Login()
        {
            Init();
        }


        public void Init()
        {
            if (File.Exists(AccountFileName))
            {
                FileStream fstream = new FileStream(AccountFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryFormatter bf = new BinaryFormatter();
                AccountInfo = (Dictionary<AccountName, string>)bf.Deserialize(fstream);
                fstream.Close();
            }
            else
            {
                AccountInfo.Add(AccountName.管理员, "");
                AccountInfo.Add(AccountName.调试员, "");
                AccountInfo.Add(AccountName.操作员, "");
            }
        }

        [Conditional("DEBUG")]
        private void DeveloperMode()
        {
            Account = AccountName.管理员;
        }

        public bool Enter(string account, string password)
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


        public bool SetPassword(string oldPassword, string newPassword, string account = null)
        {
            AccountName accountKey = Account;

            if (account != null && Enum.IsDefined(typeof(AccountName), account))
            {
                AccountName name = (AccountName)Enum.Parse(typeof(AccountName), account);

                if (Account <= name)
                {
                    accountKey = name;
                }
                else
                {
                    return false;
                }
            }

            if (AccountInfo[accountKey] == oldPassword)
            {
                AccountInfo[accountKey] = newPassword;

                FileStream fstream = new FileStream(AccountFileName, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fstream, AccountInfo);
                fstream.Close();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
