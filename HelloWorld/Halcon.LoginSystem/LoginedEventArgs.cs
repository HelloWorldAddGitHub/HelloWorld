using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halcon.LoginSystem
{
    public class AccountChangedEventArgs : EventArgs
    {
        public AccountChangedEventArgs(AccountName account)
        {
            Account = account;
        }

        public AccountName Account { get; }
    }
}
