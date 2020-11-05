using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransfersDAO
    {
        bool AddTransfer(Transfer transfers);
        List<Transfer> GetTransfers(string username);
        Transfer GetTransferFromID(int transferID);
        bool UpdateTransfer(Transfer transfers);
    }
}
