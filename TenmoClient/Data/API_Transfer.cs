using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class API_Transfer
    {
        public int TransferID { get; set; }
        public int TransferTypeID { get; set; }
        public string TransferTypeDescription { get; set; }
        public int TransferStatusID { get; set; }
        public string TransferStatusDescription { get; set; }
        public int UserFromID { get; set; }
        public string UsernameFrom { get; set; }
        public int UserToID { get; set; }
        public string UsernameTo { get; set; }
        public decimal TransferAmount { get; set; }
    }

    //    public int TransferId { get; set; }
    //    public int TransferTypeId { get; set; }
    //    public int TransferStatusId { get; set; }
    //    public int AccountFrom { get; set; }
    //    public int AccountTo { get; set; }
    //    public decimal Amount { get; set; }
    //}
    //public class NewTransfer
    //{
    //    public NewTransfer(int receiverId, decimal amount)
    //    {
    //        ReceiverAccount = receiverId;
    //        Amount = amount;
    //    }
    //    public int ReceiverAccount { get; set; }
    //    public decimal Amount { get; set; }
    //}
    //public class TransferDetails
    //{
    //    public int TransferId { get; set; }
    //    public string TransferType { get; set; }
    //    public string TransferStatus { get; set; }
    //    public string FromUser { get; set; }
    //    public string ToUser { get; set; }
    //    public decimal Amount { get; set; }
    //}

}

