using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
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

    public class NewTransfer
    {
        public NewTransfer(int receiverId, decimal amount)
        {
            ReceiverAccount = receiverId;
            Amount = amount;
        }
        public int ReceiverAccount { get; set; }
        public decimal Amount { get; set; }
    }
    public class TransferWithDetails
    {
        public int TransferId { get; set; }
        public string TransferType { get; set; }
        public string TransferStatus { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public decimal Amount { get; set; }
    }
}
