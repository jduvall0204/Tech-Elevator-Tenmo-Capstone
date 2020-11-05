using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
     public interface IAccountsDAO
    {
        Account GetAccounts(string username);
        decimal? GetBalance(int accountID);
        bool GetTransfer(Transfer transfers);
        bool UpdateBalance(int userId, decimal amount);
    }
}
