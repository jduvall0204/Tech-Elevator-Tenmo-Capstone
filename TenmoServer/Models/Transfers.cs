using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfers
    {
        public int TransferId { get; set; }
        public int TransferTypeId { get; set; }
        public int TransferStatusId { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return TransferId.ToString() + TransferTypeId.ToString() + TransferStatusId.ToString() + AccountFrom.ToString() + AccountTo.ToString() + Amount.ToString();
        }
    }
}
