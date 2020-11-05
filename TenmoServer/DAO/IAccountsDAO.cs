﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountsDAO
    {
        Accounts GetAccounts(string username);
        decimal? GetBalance(int accountID);
        bool DoTransfer(Transfers transfers);
    }
}
