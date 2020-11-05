using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransfersDAO
    {
        bool AddTransfer(Transfers transfers);
        List<Transfers> GetTransfers(string username);
        Transfers GetTransferFromID(int transferID);
        bool UpdateTransfer(Transfers transfers);
    }
}
