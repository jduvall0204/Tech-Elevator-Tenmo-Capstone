using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransfersDAO
    {
        TransferWithDetails SendMoney(int senderId, int receiverId, decimal amount);
        List<TransferWithDetails> GetTransferHistory(int userId);
        TransferWithDetails GetTransfer(int transferId);

    }
}
