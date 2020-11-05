using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransfersDAO
    {
<<<<<<< HEAD
        TransferWithDetails SendMoney(int senderId, int receiverId, decimal amount);
        List<TransferWithDetails> GetTransferHistory(int userId);
        TransferWithDetails GetTransfer(int transferId);
=======
        bool AddTransfer(Transfer transfers);
        List<Transfer> GetTransfers(string username);
        Transfer GetTransferFromID(int transferID);
        bool UpdateTransfer(Transfer transfers);
>>>>>>> 93e87783f04ba695286596bfcb6fe9f5fe6a0054
    }
}
