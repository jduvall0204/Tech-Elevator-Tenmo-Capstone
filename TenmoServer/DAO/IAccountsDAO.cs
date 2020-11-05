using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
     public interface IAccountsDAO
    {
<<<<<<< HEAD
        Accounts GetAccounts(int id);
=======
        Account GetAccounts(string username);
        decimal? GetBalance(int accountID);
        bool GetTransfer(Transfer transfers);
        bool UpdateBalance(int userId, decimal amount);
>>>>>>> 93e87783f04ba695286596bfcb6fe9f5fe6a0054
    }
}
