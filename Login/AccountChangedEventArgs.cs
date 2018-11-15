using System;

namespace Login
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
